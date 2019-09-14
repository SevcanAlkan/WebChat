using AutoMapper;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Core.Model;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGA.Data.Service
{
    public class GroupService : BaseService<GroupAddVM, GroupUpdateVM, GroupVM, Group>, IGroupService
    {
        #region Ctor

        public GroupService(UnitOfWork _uow, IMapper _mapper)
            : base(_uow, _mapper)
        {

        }

        #endregion

        #region Methods                

        public override Task<APIResultVM> Add(GroupAddVM model, Guid? userId = null, bool isCommit = true)
        {
            if (model.IsMain)
            {
                model.IsMain = false;
            }

            return base.Add(model, userId, isCommit);
        }

        public override Task<APIResultVM> Update(Guid id, GroupUpdateVM model, Guid? userId = null, bool isCommit = true)
        {
            if(model.IsMain && Repository.Query().Any(a=> a.Id != id))
            {
                return Task.Run(() =>
                {
                    return APIResult.CreateVM(false, id, AppStatusCode.WRG01005);
                });
            }

            return base.Update(id, model, userId, isCommit);
        }

        public override Task<APIResultVM> Delete(Guid id, Guid? userId = null, bool isCommit = true)
        {
            var group = GetById(id);
            if (group == null || group.IsMain)
            {
                return Task.Run(() =>
                {
                    return APIResult.CreateVM(false, id, AppStatusCode.WRG01005);
                });
            }

            return base.Delete(id, userId, isCommit);
        }

        #endregion
    }

    public interface IGroupService : IBaseService<GroupAddVM, GroupUpdateVM, GroupVM, Group>
    {
    }
}
