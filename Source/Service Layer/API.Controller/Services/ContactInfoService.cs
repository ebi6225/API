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


                return base.DoProcessBase(Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}