using Base.API.Model.BaseMessages;

namespace API.Model.Responses
{
    public class SampleResponse: BaseMessageAPI
    {
        public string MatchineName { get; set; }
        public string UserName { get; set; }
    }
}