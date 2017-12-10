using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using ChatCode.Bot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace ChatCode.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public static string UserName;


        [ResponseType(typeof(void))]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            UserName = activity.From.Name;
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, MakeRootDialog); //for text dialogs
                //await Conversation.SendAsync(activity, () => new ReceiveAttachmentDialog()); //for image
            }

            else
            {
                HandleSystemMessage(activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
            }

            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
            }

            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
            }

            else if (message.Type == ActivityTypes.Typing)
            {
            }

            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        internal static IDialog<MessageModel> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(MessageModel.BuildForm));
        }
    }
}