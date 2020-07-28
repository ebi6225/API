using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller.Services.Thread
{
    internal class BulkControlService
    {
        //internal <(IEnumerable<BULK_ITEMS>, string BatchID, string BranchCode)> FindPendingItemsAsync(CancellationToken token = default)
        //{
        //    string BatchID;
        //    string BranchCode;
        //    try
        //    {
        //        BatchID = string.Empty;
        //        BranchCode = string.Empty;


        //        var control = (await ServiceFactory.Instance.Resolve<IRepository<BULK_CONTROL>>().SelectAsync(x => x.STATUS == (short)enumFileStatus.Pending &&
        //                                                                                                           x.IS_CANCEL == false &&
        //                                                                                                           x.POSTING_DATE <= SystemInfo.BusinessDate &&
        //                                                                                                           x.IS_REVERSAL == false, token))
        //                                                                                          .OrderByDescending(o => o.ID).FirstOrDefault();
        //        if (control != null)
        //        {
        //            BatchID = control.BATCH_ID;
        //            BranchCode = await ServiceFactory.Instance.Resolve<ICoreRepository>().GetTopOrganizationCodeAsync(token);

        //            return ((await ServiceFactory.Instance.Resolve<IRepository<BULK_ITEMS>>()
        //                                                   .SelectAsync(x => x.BATCH_ID == control.BATCH_ID &&
        //                                                                x.IS_REVERSAL == false &&
        //                                                                x.STATUS == (short)enumFileStatus.Pending, token))
        //                                                                .Take(1000), BatchID, BranchCode);
        //        }
        //        else
        //            return (null, BatchID, BranchCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceFactory.Instance.Resolve<ITraceLogClient>().SendMessage(LogLevel.Error, "FindPendingItems", ex.Message);
        //        throw ex;
        //    }
        //}

        #region Bulk Manager        
        //internal async Task<double> NumberOfPendingItemsAsync(string BATCH_ID, CancellationToken token = default)
        //{
        //    var control = (await ServiceFactory.Instance.Resolve<IRepository<BULK_CONTROL>>().SelectAsync(x => x.BATCH_ID == BATCH_ID &&
        //                                                                                                       x.IS_CANCEL == false &&
        //                                                                                                       x.STATUS == (short)enumFileStatus.Pending, token))
        //                                                                                      .OrderByDescending(o => o.ID).FirstOrDefault();
        //    if (control != null)
        //    {
        //        return (await ServiceFactory.Instance.Resolve<IRepository<BULK_ITEMS>>().SelectAsync(x => x.BATCH_ID == control.BATCH_ID &&
        //                                                                                      x.STATUS == (short)enumFileStatus.Pending, token)).Count();
        //    }
        //    return 0;
        //}
        //internal async Task UpdateBulkControlAsync(string BATCH_ID, CancellationToken token = default)
        //{
        //    try
        //    {
        //        await ServiceFactory.Instance.Resolve<IRepository<BULK_CONTROL>>().UpdateAsync(UpdateQueryBuilder.From<BULK_CONTROL>()
        //        .Set(p => new BULK_CONTROL { STATUS = (short)enumFileStatus.Success })
        //        .Where(p => p.BATCH_ID == BATCH_ID &&
        //                    p.IS_CANCEL == false &&
        //                    p.STATUS == (short)enumFileStatus.Pending)
        //        .Query, token: token);
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceFactory.Instance.Resolve<IContext>().Logger.SendMessage(LogLevel.Error, "UpdateBulkControl", ex.Message);
        //        throw ex;
        //    }
        //}
        //internal async Task UpdateBulkItemAsync(BULK_ITEMS dto, CancellationToken token = default)
        //{
        //    try
        //    {
        //        var control = await ServiceFactory.Instance.Resolve<IRepository<BULK_ITEMS>>().UpdateAsync(dto, false, token);
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceFactory.Instance.Resolve<ITraceLogClient>().SendMessage(LogLevel.Error, "UpdateBulkControlItem", ex.Message);
        //        throw ex;
        //    }
        //}
        //internal async Task<bool> IsBulkCanceledAsync(string BATCH_ID, CancellationToken token = default)
        //{
        //    try
        //    {
        //        var dto = (await ServiceFactory.Instance.Resolve<IRepository<BULK_CONTROL>>().SelectAsync(x => x.BATCH_ID == BATCH_ID &&
        //                                                                                          x.IS_CANCEL == true, token))
        //                                                                                         .OrderByDescending(o => o.ID).FirstOrDefault();
        //        if (dto == null)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceFactory.Instance.Resolve<ITraceLogClient>().SendMessage(LogLevel.Error, "IsBulkCanceled", ex.Message);
        //        throw ex;
        //    }
        //}
        #endregion Bulk Manager 

        //public async Task InsertBulkControlItemAsync(BulkControlItem bulkControlItem, CancellationToken token = default)
        //{
        //    BULK_CONTROL bulkControlEntities = new BULK_CONTROL();
        //    BULK_ITEMS bulkItemEntities = new BULK_ITEMS();
        //    string BATCH_ID = string.Empty;
        //    try
        //    {
        //        if (bulkControlItem != null)
        //        {
        //            if (string.IsNullOrEmpty(bulkControlItem.BATCH_ID))
        //            {
        //                BATCH_ID = new ServiseBulkRunTransaction().GenerateNextSequenceNo().ToString();
        //                bulkControlItem.BATCH_ID = BATCH_ID;
        //            }

        //            bulkControlEntities = RepositoryTools.ConvertMsgDTOToRepObj<BULK_CONTROL>(bulkControlItem);
        //            await ServiceFactory.Instance.Resolve<IRepository<BULK_CONTROL>>().InsertAsync(bulkControlEntities, token);

        //            if (bulkControlItem.BulkItemList != null && bulkControlItem.BulkItemList.Count > 0)
        //            {
        //                foreach (BulkItem bulkItem in bulkControlItem.BulkItemList)
        //                {
        //                    if (string.IsNullOrEmpty(bulkItem.BATCH_ID))
        //                        bulkItem.BATCH_ID = BATCH_ID;

        //                    bulkItemEntities = RepositoryTools.ConvertMsgDTOToRepObj<BULK_ITEMS>(bulkItem);
        //                    await ServiceFactory.Instance.Resolve<IRepository<BULK_ITEMS>>().InsertAsync(bulkItemEntities, token);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceFactory.Instance.Resolve<ITraceLogClient>().SendMessage(LogLevel.Error, "InsertBulkControlItem", ex.Message);
        //        throw ex;
        //    }
        //}
    }
}
