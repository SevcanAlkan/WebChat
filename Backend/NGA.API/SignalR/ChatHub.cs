using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGA.API.SignalR
{
    [EnableCors("CorsPolicy")]
    [AllowAnonymous]
    public class ChatHub : Hub
    {
        static List<Guid> dic = new List<Guid>();

        private IMessageService _service;
        private readonly IMapper _mapper;
        public ChatHub(IMessageService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //public Task JoinGroup(Guid groupId)
        //{
        //    return Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
        //}

        //public Task LeaveGroup(Guid groupId)
        //{
        //    return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());
        //}

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var groupId = httpContext.Request.Query["GroupId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

            await base.OnConnectedAsync();
        }

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId);

        //    await base.OnDisconnectedAsync(exception);
        //}


        public async Task SendMessage(MessageVM message)
        {
            //if(Groups.Any)

            var model = _mapper.Map<MessageAddVM>(message);
            await _service.Add(model, message.UserId);

            await Clients.Group(message.GroupId.ToString()).SendAsync("ReceiveMessage", message);
        }

    }


    public interface IChatHub
    {
        Task SendMessage();
        List<MessageVM> GetHistory(Guid userId);
    }


    
}
