using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NGA.Data.ViewModel;
using NGA.Domain;

namespace NGA.MonolithAPI.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            #region Parameter
            CreateMap<Parameter, ParameterVM>();
            CreateMap<Parameter, ParameterAddVM>();
            CreateMap<Parameter, ParameterUpdateVM>();

            CreateMap<ParameterVM, Parameter>();
            CreateMap<ParameterVM, ParameterAddVM>();
            CreateMap<ParameterVM, ParameterUpdateVM>();

            CreateMap<ParameterAddVM, Parameter>();
            CreateMap<ParameterAddVM, ParameterVM>();
            CreateMap<ParameterAddVM, ParameterUpdateVM>();

            CreateMap<ParameterUpdateVM, Parameter>();
            CreateMap<ParameterUpdateVM, ParameterVM>();
            CreateMap<ParameterUpdateVM, ParameterAddVM>();
            #endregion

            #region Group
            CreateMap<Group, GroupVM>().ForMember(x => x.Users, opt => opt.Ignore());
            CreateMap<Group, GroupAddVM>().ForMember(x => x.Users, opt => opt.Ignore());
            CreateMap<Group, GroupUpdateVM>().ForMember(x => x.Users, opt => opt.Ignore());

            CreateMap<GroupVM, Group>().ForMember(x => x.Users, opt => opt.Ignore());
            CreateMap<GroupVM, GroupAddVM>();
            CreateMap<GroupVM, GroupUpdateVM>();

            CreateMap<GroupAddVM, Group>().ForMember(x => x.Users, opt => opt.Ignore());
            CreateMap<GroupAddVM, GroupVM>();
            CreateMap<GroupAddVM, GroupUpdateVM>();

            CreateMap<GroupUpdateVM, Group>().ForMember(x => x.Users, opt => opt.Ignore());
            CreateMap<GroupUpdateVM, GroupVM>();
            CreateMap<GroupUpdateVM, GroupAddVM>();
            #endregion

            #region Message
            CreateMap<GroupUser, GroupUserVM>();
            CreateMap<GroupUser, GroupUserAddVM>();
            CreateMap<GroupUser, GroupUserUpdateVM>();

            CreateMap<GroupUserVM, GroupUser>();
            CreateMap<GroupUserVM, GroupUserAddVM>();
            CreateMap<GroupUserVM, GroupUserUpdateVM>();

            CreateMap<GroupUserAddVM, GroupUser>();
            CreateMap<GroupUserAddVM, GroupUserVM>();
            CreateMap<GroupUserAddVM, GroupUserUpdateVM>();

            CreateMap<GroupUserUpdateVM, GroupUser>();
            CreateMap<GroupUserUpdateVM, GroupUserVM>();
            CreateMap<GroupUserUpdateVM, GroupUserAddVM>();
            #endregion

            #region Message
            CreateMap<Message, MessageVM>();
            CreateMap<Message, MessageAddVM>();
            CreateMap<Message, MessageUpdateVM>();

            CreateMap<MessageVM, Message>();
            CreateMap<MessageVM, MessageAddVM>();
            CreateMap<MessageVM, MessageUpdateVM>();

            CreateMap<MessageAddVM, Message>();
            CreateMap<MessageAddVM, MessageVM>();
            CreateMap<MessageAddVM, MessageUpdateVM>();

            CreateMap<MessageUpdateVM, Message>();
            CreateMap<MessageUpdateVM, MessageVM>();
            CreateMap<MessageUpdateVM, MessageAddVM>();
            #endregion

            #region User
            CreateMap<User, UserVM>();
            CreateMap<User, UserAddVM>();
            CreateMap<User, UserUpdateVM>();
            CreateMap<User, UserAuthenticateVM>();

            CreateMap<UserVM, User>();
            CreateMap<UserVM, UserAddVM>();
            CreateMap<UserVM, UserUpdateVM>();
            CreateMap<UserVM, UserAuthenticateVM>();

            CreateMap<UserAddVM, User>();
            CreateMap<UserAddVM, UserVM>();
            CreateMap<UserAddVM, UserUpdateVM>();
            CreateMap<UserAddVM, UserAuthenticateVM>();

            CreateMap<UserUpdateVM, User>();
            CreateMap<UserUpdateVM, UserVM>();
            CreateMap<UserUpdateVM, UserAddVM>();
            CreateMap<UserUpdateVM, UserAuthenticateVM>();

            CreateMap<UserAuthenticateVM, User>();
            CreateMap<UserAuthenticateVM, UserVM>();
            CreateMap<UserAuthenticateVM, UserAddVM>();
            CreateMap<UserAuthenticateVM, UserUpdateVM>();
            #endregion

        }
    }
}
