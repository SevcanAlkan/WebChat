using Microsoft.EntityFrameworkCore;
using NGA.Core.Parameter;
using NGA.Core.Validation;
using NGA.Data.SubStructure;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;

namespace NGA.Data.Helper
{
    public static class ParameterHelperLoder
    {
        public static void LoadStaticValues(NGADbContext dbContext)
        {
            DbSet<NGA.Domain.Parameter> _param = dbContext.Set<NGA.Domain.Parameter>();

            List<NGA.Domain.Parameter> parameters = _param.Where(a => !a.IsDeleted).ToList();

            Type myType = typeof(ParameterValue);
            foreach (var item in parameters)
            {
                PropertyInfo myPropInfo = myType.GetProperty(item.Code);
                if (myPropInfo == null)
                    continue;

                var value = parameters.Where(a => a.Code == item.Code).Select(a => a.Value).FirstOrDefault();
                if (value == null || Validation.IsNullOrEmpty(value))
                    continue;

                if (myPropInfo.PropertyType == typeof(string))
                    myPropInfo.SetValue(null, value);
                else if (myPropInfo.PropertyType == typeof(Guid) || myPropInfo.PropertyType == typeof(Guid?))
                {
                    Guid? guidVal = Guid.Parse(value);

                    if (guidVal != null && guidVal != Guid.Empty)
                        myPropInfo.SetValue(null, guidVal.Value);
                }
                else if (myPropInfo.PropertyType == typeof(bool))
                    myPropInfo.SetValue(null, NGA.Core.Convert.ToBoolean(value));
                else if (myPropInfo.PropertyType == typeof(int))
                {
                    int num = Convert.ToInt32(value);
                    myPropInfo.SetValue(null, num);
                }
            }
        }
    }
}
