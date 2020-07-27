using Bse.API.Controller.Abstract;
using System;

namespace API.Controller.Services
{
    public abstract class AbstractServiceController : BaseAbstractService
    {
        public override object DoProcessBase(object Message)
        {
            try
            {
                return base.DoProcessBase(Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}