using API.Entities.Model;
using API.Model.Requests;
using System;

namespace API.Controller.Services
{
    internal class ContactInfoService : AbstractServiceController
    {
        public override object DoProcessBase(object Message)
        {
            try
            {
                //Business will be inside this function

                SampleRequest sampleRequest = (SampleRequest)Message;

                ContactInfo dto = new ContactInfo
                {
                    SenderName = sampleRequest.sender_name,
                    SenderEmail = sampleRequest.sender_email,
                    SenderPhone = sampleRequest.sender_phone,
                    SenderMessage = sampleRequest.sender_message,
                    LastUpdatedTime = DateTime.Now,
                };

                QueryMaker.Instance.ContactInfo.Add(dto);
                var r = QueryMaker.Instance.SaveChanges();

                return base.DoProcessBase(Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}