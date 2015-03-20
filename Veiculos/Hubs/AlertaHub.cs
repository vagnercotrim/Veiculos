using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Veiculos.Hubs
{
    [HubName("alerta")]
    public class AlertaHub : Hub
    {
        public static void SendMessage(String texto)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<AlertaHub>();
            hubContext.Clients.All.newMessage(texto);
        }
    }
}