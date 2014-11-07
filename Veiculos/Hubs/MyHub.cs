using System;
using System.Threading.Tasks;
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