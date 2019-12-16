using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using NGA.Core.Validation;
using NGA.Data;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGA.API.SignalR
{
    [EnableCors("CorsPolicy")]
    [AllowAnonymous]
    public class DataHub : Hub
    {
        //internal static List<Connection> Connections = new List<Connection>();
      
        //private IMessageService _service;
        //private readonly IMapper _mapper;
        public DataHub()//IMessageService service, IMapper mapper, NGADbContext con)
        {
            //_service = service;
            //_mapper = mapper;
            //_con = con;
        }

        public override async Task OnConnectedAsync()
        {
            //var httpContext = Context.GetHttpContext();
            //var userIdStr = httpContext.Request.Query["UserId"].FirstOrDefault();
            //Guid userId = new Guid(userIdStr);

            //var user = _con.Users.SingleOrDefault(s => s.Id == userId);

            //if (user == null)
            //    return;

            //Connection connection = new Connection();
            //connection.ConnectionID = Context.ConnectionId;
            //connection.Connected = true;
            //connection.UserId = userId;

            //Connections.Add(connection);

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //var connection = Connections.Find(a => a.ConnectionID == Context.ConnectionId);
            //if (connection != null)
            //    Connections.Remove(connection);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateGroupOnAllClients(Guid groupId) 
        {
            await Clients.All.SendAsync("ReceiveGroupUpdateMessages", groupId);
        }

        public async Task UpdateUserOnAllClients(Guid userId) 
        {
            await Clients.All.SendAsync("UpdateUser", userId);
        }


        //User veya Group ID buraya gelecek ve tum clientlere iletilecek
        //ID eger listede yoksa sunucudan cekilip listeye eklenecek
        //ID eger varsa listeden silinip sunucudan tekrar cekilecek
    }


    public interface IDataHub
    {
        Task UpdateGroupOnAllClients(Guid groupId);
        Task UpdateUserOnAllClients(Guid userId);
    }



}
