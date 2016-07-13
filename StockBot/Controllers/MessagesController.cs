using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace StockBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                // calculate something for us to return
                int length = (message.Text ?? string.Empty).Length;

                // return our reply to the user
                Message reply = message.CreateReplyMessage();
                reply.Text = $"Enviaste {message.Text} que tiene una longitud de {length} caracteres";

                //reply.Text = await GetStock(message.Text);
                //reply.Text = await GetStock(await GetStockUsingLuis(message.Text));
                return await Conversation.SendAsync(message, MakeRootDialog);

                return reply;
            }
            else
            {
                return HandleSystemMessage(message);
            }

        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                //Message reply = message.CreateReplyMessage();
                //reply.Type = "Ping";
                //return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                
            }
            else if (message.Type == "BotAddedToConversation")
            {
                Message reply = message.CreateReplyMessage("Hi user!");
                //reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "BotRemovedFromConversation")
            {

            }
            else if (message.Type == "UserAddedToConversation")
            {

            }
            else if (message.Type == "UserRemovedFromConversation")
            {

            }
            else if (message.Type == "EndOfConversation")
            {

            }

            return null;
        }

        private async Task<string> GetStock(string strStock)
        {
            string strRet = string.Empty;
            double? dblStock = await Yahoo.GetStockPriceAsync(strStock);
            // return our reply to the user
            if (null == dblStock)
            {
                strRet = string.Format("Stock {0} parece no ser valida", strStock.ToUpper());
            }
            else
            {
                strRet = string.Format("Stock: {0}, Valor: {1}", strStock.ToUpper(), dblStock);
            }

            return strRet;
        }

        private async Task<string> GetStockUsingLuis(string message)
        {
            StockLUIS m = await LUISStockClient.ParseUserInput(message);
            if (m.intents[0].intent == "precioAccion")
            {
                return m.entities[0].entity;
            }

            return null;
        }

        internal static IDialog<SandwichOrder> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(SandwichOrder.BuildForm));
        }


    }
}