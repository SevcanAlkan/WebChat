using Microsoft.EntityFrameworkCore;
using NGA.Core.Enum;
using NGA.Core.Model;
using NGA.Core.Parameter;
using NGA.Core.Validation;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGA.Data.Logger
{
    /*
     
        web projesindeki sql kodlarini tasi
        
        MonitorController ile hem listede hemde detayda veri cek
        
     */
    public static class LogContext
    {
        public static List<Log> Logs { get; set; }
        public static List<LogError> Errors { get; set; }

        public static string CreateRequestRecord(string actionName, string controllerName,
            string httpMethodType, string path, string requestBody, string returnType)
        {
            if (!ParameterValue.SYS01001)
                return "";

            Log rec = new Log();
            rec.Id = Guid.NewGuid();
            rec.CreateDate = DateTime.Now;
            rec.RequestBody = requestBody;
            rec.Path = Validation.IsNull(path) ? "-" : path;
            rec.ActionName = Validation.IsNull(actionName) ? "-" : actionName;
            rec.ControllerName = Validation.IsNull(controllerName) ? "-" : controllerName;
            rec.ReturnTypeName = Validation.IsNull(returnType) ? "-" : returnType;
            rec.MethodType = Validation.IsNull(httpMethodType) ? HTTPMethodType.Unknown :
                        (httpMethodType.ToUpper() == "POST" ? HTTPMethodType.POST :
                        (httpMethodType.ToUpper() == "PUT" ? HTTPMethodType.PUT :
                        (httpMethodType.ToUpper() == "DELETE" ? HTTPMethodType.DELETE :
                        (httpMethodType.ToUpper() == "GET" ? HTTPMethodType.GET : HTTPMethodType.Unknown))));

            if (Logs == null) { Logs = new List<Log>(); }

            if (Logs.Any(a => a.Id == rec.Id))
                return Logs.Where(a => a.Id == rec.Id).Select(a => a.Id).FirstOrDefault().ToString();

            Logs.Add(rec);

            return rec.Id.ToString();
        }
        public static void UpdateRequest(Guid requestId)
        {
            if (!ParameterValue.SYS01001)
                return;

            Log rec = Logs.FirstOrDefault(a => a.Id == requestId);

            if (rec != null)
            {
                Logs.Remove(rec);

                rec.ResponseTime = (DateTime.Now - rec.CreateDate).Milliseconds;

                Logs.Add(rec);
            }
        }

        public static void AddError(string requestId, Exception ex)
        {
            if (!ParameterValue.SYS01001 || ex == null)
                return;

            if (Errors == null) { Errors = new List<LogError>(); }

            Errors.Add(ExceptionToLogError(ex, requestId));
        }
        public static void AddError(string requestId, APIErrorVM ex)
        {
            if (!ParameterValue.SYS01001 || ex == null)
                return;

            if (Errors == null) { Errors = new List<LogError>(); }

            Errors.Add(ErrorVMToLogError(ex, requestId));
        }
        public static void AddErrorRange(string requestId, List<APIErrorVM> errors)
        {
            if (errors == null)
                return;

            foreach (var item in errors)
            {
                AddError(requestId, item);
            }
        }

        public static void Save()
        {
            try
            {
                if (!ParameterValue.SYS01001)
                    return;

                if ((Errors != null && Errors.Count > 0) || (Logs != null && Logs.Count > 0))
                {
                    using (NGADbContext context = new NGADbContext())
                    {
                        UnitOfWork unitOfWork = new UnitOfWork(context);
                        DbSet<Log> _log = context.Set<Log>();
                        DbSet<LogError> _logError = context.Set<LogError>();

                        if (Logs != null)
                        {
                            foreach (var item in Logs)
                            {
                                _log.Add(item);
                            }
                        }

                        if (Errors != null)
                        {
                            foreach (var error in Errors)
                            {
                                _logError.Add(error);
                            }
                        }

                        context.SaveChanges();
                        Clean();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<LogVM> GetLogs()
        {
            List<Log> _logs = new List<Log>();
            List<LogError> _errors = new List<LogError>();

            using (NGADbContext context = new NGADbContext())
            {
                UnitOfWork unitOfWork = new UnitOfWork(context);
                DbSet<Log> _log = context.Set<Log>();
                DbSet<LogError> _logError = context.Set<LogError>();

                _logs = _log.ToList();
                _errors = _logError.ToList();
            }

            List<LogVM> result = new List<LogVM>();

            foreach (var item in _logs)
            {
                result.Add(new LogVM()
                {
                    ActionName = item.ActionName,
                    ControllerName = item.ControllerName,
                    CreateDate = item.CreateDate,
                    Id = item.Id,
                    MethodType = item.MethodType,
                    Path = item.Path,
                    RequestBody = item.RequestBody,
                    ResponseTime = item.ResponseTime,
                    ReturnTypeName = item.ReturnTypeName,
                    Errors = _errors.Where(a => a.RequestId == item.Id).ToList()
                });
            }

            return result;
        }
        public static LogVM GetLog(Guid requestId)
        {
            using (NGADbContext context = new NGADbContext())
            {
                UnitOfWork unitOfWork = new UnitOfWork(context);
                DbSet<Log> _log = context.Set<Log>();
                DbSet<LogError> _logError = context.Set<LogError>();

                Log request = _log.FirstOrDefault(a => a.Id == requestId);
                if (request != null)
                {
                    LogVM result = new LogVM();

                    result.ActionName = request.ActionName;
                    result.ControllerName = request.ControllerName;
                    result.CreateDate = request.CreateDate;
                    result.Id = request.Id;
                    result.MethodType = request.MethodType;
                    result.Path = request.Path;
                    result.RequestBody = request.RequestBody;
                    result.ResponseTime = request.ResponseTime;
                    result.ReturnTypeName = request.ReturnTypeName;
                    result.Errors = _logError.Where(a => a.RequestId == request.Id).ToList();

                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        private static void Clean()
        {
            Logs = null;
            Logs = new List<Log>();

            Errors = null;
            Errors = new List<LogError>();
        }

        private static LogError ErrorVMToLogError(APIErrorVM ex, string requestId)
        {
            LogError rec = new LogError();

            if (ex == null)
                return null;

            Guid RequestId = new Guid(requestId);
            if (RequestId == null)
                RequestId = Guid.Empty;

            rec.Id = ex.ErrorId;
            rec.RequestId = RequestId;

            rec.Message = ex.Message;
            rec.Source = ex.Source;
            rec.StackTrace = ex.StackTrace;
            rec.InnerException = ex.InnerException;
            rec.OrderNum = Errors != null ? (Errors.Count(c => c.RequestId == RequestId) + 1) : 0;

            return rec;
        }
        private static LogError ExceptionToLogError(Exception ex, string requestId)
        {
            LogError rec = new LogError();

            if (ex == null)
                return null;

            Guid RequestId = new Guid(requestId);
            if (RequestId == null)
                RequestId = Guid.Empty;

            rec.Id = Guid.NewGuid();
            rec.RequestId = RequestId;

            rec.Message = ex.Message;
            rec.Source = ex.Source;
            rec.StackTrace = ex.StackTrace;
            rec.OrderNum = Errors != null ? (Errors.Count(c => c.RequestId == RequestId) + 1) : 0;
            rec.InnerException = ex.InnerException != null ? (
                   "Message: " + (ex.InnerException.Message != null ? ex.InnerException.Message : "") +
                   "Source: " + (ex.InnerException.Source != null ? ex.InnerException.Source : "") +
                   "Stack Trace: " + (ex.InnerException.StackTrace != null ? ex.InnerException.StackTrace : "")
               ) : "";

            return rec;
        }
    }
}
