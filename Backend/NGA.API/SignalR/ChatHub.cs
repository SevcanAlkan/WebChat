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

        public async Task SendMessage(MessageVM message)
        {
            var model = _mapper.Map<MessageAddVM>(message);
            await _service.Add(model, message.UserId);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

    }


    public interface IChatHub
    {
        Task SendMessage();
        List<MessageVM> GetHistory(Guid userId);
    }


    
}
