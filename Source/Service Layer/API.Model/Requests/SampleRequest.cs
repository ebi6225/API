using Base.API.Model.BaseMessages;

namespace API.Model.Requests
{
   public class SampleRequest: BaseMessageAPI
    {
        public string username { get; set; }

        public string sender_email { get; set; }
        public string sender_name { get; set; }
        public string sender_phone { get; set; }
        public string sender_message { get; set; }
    }
}