using API.Model.Requests;
using API.Model.Responses;
using System;

namespace API.Controller.Services
{
    internal class MatchineNameService
    {
        internal SampleResponse GetMachineName(SampleRequest request)
        {
            SampleResponse dto = new SampleResponse
            {
                MatchineName = Environment.MachineName.ToString(),
                UserName = "wer",
            };

            return dto;
        }
    }
}