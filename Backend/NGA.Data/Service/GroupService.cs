using AutoMapper;
using AutoMapper.QueryableExtensions;
using NGA.Core;
using NGA.Core.Helper;
using NGA.Core.Model;
using NGA.Core.Validation;
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


        public List<GroupVM> GetByUserId(Guid userId)
        {
            if (Validation.IsNullOrEmpty(userId))
            {
                return new List<GroupVM>();
            }

            var groups = this.Repository.Query().Where(a => !a.IsPrivate || (a.Users.Any(x => x.UserId == userId))).ProjectTo<GroupVM>().ToList();

            return groups;
        }
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
            if(model.IsMain && Repository.Query().Any(a=> a.Id != id && a.IsMain))
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
        List<GroupVM> GetByUserId(Guid userId);
    }
}
