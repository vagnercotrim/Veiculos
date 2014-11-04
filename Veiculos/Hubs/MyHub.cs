using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Veiculos.Hubs
{
    public class MyHub : Hub
    {

        public MyHub()
        {
            var taskTimer = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    String timeNow = DateTime.Now.ToString();
                    Clients.All.SendServerTime(timeNow);
                    await Task.Delay(1000);
                }
            }, TaskCreationOptions.LongRunning
                );
        }

    }
}