using Microsoft.AspNetCore.SignalR;
using TestProduct.DB;

namespace TestProduct.TestNewMethod
{
    public class ChatHub: Hub
    {
        public ChatHub()
        {

        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }

        public async Task GetRequst()
        {
            await Clients.All.SendAsync("AnswerRequst", 
                (ApplicationContext db) => db.RequestRepair.ToList());
        }
    }
}
