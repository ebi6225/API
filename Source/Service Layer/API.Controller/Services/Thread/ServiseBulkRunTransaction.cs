using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller.Services
{
    public class ServiseBulkRunTransaction
    {
        //internal async Task<BULK_ITEMS> BulkRunTransactionAsync(BULK_ITEMS item, string BranchCode, CancellationToken token = default)
        //{
        //    var q = SelectQueryBuilder
        //                .From<TRAN_ITEM>()
        //                .Where((TrItem) => TrItem.TRAN_CODE == item.TRAN_CODE).Query;
        //    TranItem TraanItem = (await QueryHelper.ExecuteDataSetAsync(Host.Common.Modules.COR, q, token)).Tables[0].ConvertToList<TranItem>().FirstOrDefault();
        //    #region
        //    Type type = ProcessManager.Instance[TraanItem.PROCESS_UNIQUE_KEY];
        //    var processType = ServiceFactory.Instance.TryResolveType(type);
        //    var tranProcessAttribute = processType.GetCustomAttribute<TransactionProcessAttribute>(true);
        //    #endregion
        //    var request = JSONSerializerHelper.Deserialize(tranProcessAttribute.MessageType, item.ITEMS) as TransactionServiceMessageBase;

        //    //Following condition will use for BRC purpose
        //    if (string.IsNullOrEmpty(request.Header.BranchCode) || string.IsNullOrEmpty(request.Header.UserId))
        //    {
        //        request.Header.BranchCode = BranchCode;
        //        request.Header.UserId = AppSettingManager.Instance.Get<string>("SystemPostUserName");
        //    }

        //    request.Header.SequenceNo = GenerateNextSequenceNo();
        //    request.Header.TransactionStatus = TransactionStatus.New;
        //    request.Header.ProcessingMode = ProcessingMode.Batch;
        //    request.Header.TransactionTime = SystemInfo.CurrentDateTime;
        //    request.Header.BusinessDate = SystemInfo.BusinessDate;
        //    request.Header.ClientWorkstationName = "System";
        //    item.SEQUENCE_NO = request.Header.SequenceNo;

        //    var dto = await ServiceFactory.Instance.Resolve<ITransactionRunnerService>().RunTransactionAsync(item.TRAN_CODE, request, token: token);

        //    if (dto.HostReply.Code == (int)GeneralHostReplyCodes.Successful)
        //    {
        //        item.STATUS = (short)enumFileStatus.Success;
        //        item.REMARK = dto.HostReply.Description;
        //        item.ERROR = Dns.GetHostName() + "\r\n" + dto.HostReply.Description + "\r\n" + dto.HostMessages;
        //    }
        //    else if (dto.HostReply.Code == (int)GeneralHostReplyCodes.SuccessWithWarning)
        //    {
        //        item.STATUS = (short)enumFileStatus.SuccessWithWarning;
        //        item.REMARK = String.Join(System.Environment.NewLine, dto.HostMessages.Select(x => x.Description).ToList());
        //        item.ERROR = Dns.GetHostName() + "\r\n" + dto.HostReply.Description + "\r\n" + dto.HostMessages;
        //    }
        //    else
        //    {
        //        item.STATUS = (short)enumFileStatus.Failed;
        //        item.REMARK = String.Join(System.Environment.NewLine, dto.HostMessages.Select(x => x.Description).ToList());
        //        item.ERROR = Dns.GetHostName() + "\r\n" + dto.HostReply.Description + "\r\n" + dto.HostMessages;
        //    }

        //    return item;
        //}
        //public long GenerateNextSequenceNo()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    DateTime now = DateTime.Now;

        //    sb.Append(Thread.CurrentThread.ManagedThreadId);
        //    sb.Append(now.Year - 2000);
        //    sb.Append(now.Month);
        //    sb.Append(now.Day);
        //    sb.Append(now.Hour);
        //    sb.Append(now.Minute);
        //    sb.Append(now.Millisecond);
        //    return long.Parse(sb.ToString());
        //}
    }
}
