using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controller.Services
{
    public class BulkManager
    {
        #region thread-safe, lazy singleton

        public static BulkManager Instance
        {
            get
            {
                return Nested._BulkManager;
            }
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            internal static readonly BulkManager _BulkManager;

            static Nested()
            {
                _BulkManager = new BulkManager();
            }
        }

        #endregion

        #region Bulk Manager

        private Task ProcessBulkServiceTask = null;
        private CancellationTokenSource m_ProcessBulkServiceThreadKillFlag = new CancellationTokenSource();
        internal bool isStartedSuccessfully;

        internal void ManageBulkOnceBackEndUp()
        {
            StartProcessBulkServiceTask();
        }

        public bool StartProcessBulkServiceTask()
        {
            try
            {
                if (ProcessBulkServiceTask == null)
                {
                    ProcessBulkServiceTask = Task.Run(async () => await BulkTransactionAsync());
                }
            }
            catch (Exception ex)
            {                
                return false;
            }

            isStartedSuccessfully = true;
            return isStartedSuccessfully;
        }

        Int32 TotalProceed = 0;
        BulkControlService RunnerService = new BulkControlService();

        private async Task BulkTransactionAsync()
        {
            await Task.Delay(35 * 1000);//wait for 35 seconds
            await Task.Run(async () => await RuningBulkTransactionAsync());
        }

        private async Task RuningBulkTransactionAsync()
        {
            await Task.Delay(5 * 1000);//wait for 5 seconds

            do
            {
                try
                {
                    if (TotalProceed > 0)
                    {
                        await Task.Run(async () => await RuningBulkTransactionAsync());
                    }
                    await Task.Delay(5 * 1000);//wait for 5 seconds

                    string BatchID = string.Empty;
                    string BranchCode = string.Empty;
                    //IEnumerable<BULK_ITEMS> dto;
                    var token = m_ProcessBulkServiceThreadKillFlag.Token;
                    //(dto, BatchID, BranchCode) = await RunnerService.FindPendingItemsAsync(token);

                    //if (dto != null)
                    //{
                    //    TotalProceed = dto.Count();
                    //    int maxDegreeOfParallelism = Environment.ProcessorCount > 1 ? (int)(Environment.ProcessorCount / 2) : 1;

                    //    await dto.ForEachAsync(async row =>
                    //    {
                    //        if (!await RunnerService.IsBulkCanceledAsync(row.BATCH_ID, token))//Check bulk if stoped to proceed.
                    //        {
                    //            await ProcessOneRowAsync(row, BranchCode, RunnerService, token);
                    //            TotalProceed = TotalProceed - 1;
                    //        }
                    //        else
                    //        {
                    //            // Bulk has been stoped to proceed.
                    //            ProcessBulkServiceTask.Dispose();
                    //        }
                    //    }, maxDegreeOfParallelism);
                    //    //Update Bulk Control status
                    //    double numberofPending = await RunnerService.NumberOfPendingItemsAsync(BatchID, token);
                    //    if (numberofPending == 0)
                    //    {
                    //        await RunnerService.UpdateBulkControlAsync(BatchID, token);
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    //ServiceFactory.Instance.Resolve<ITraceLogClient>().SendMessage(LogLevel.Error, "RuningBulkTransactionAsync", ex.Message);
                }
            } while (true);
        }
        //private async Task ProcessOneRowAsync(BULK_ITEMS item, string BranchCode, BulkControlService RunnerService, CancellationToken token = default)
        //{
        //    try
        //    {
        //        using (var scope = CreateTransactionScope())
        //        {
        //            item.REQUEST_TIME = DateTime.Now;
        //            if (item.ITEMS == null)
        //            {
        //                item.STATUS = (short)enumFileStatus.Failed;
        //                item.REMARK = "Message is empty.";
        //            }
        //            else
        //            {
        //                item = await new ServiseBulkRunTransaction().BulkRunTransactionAsync(item, BranchCode, token);
        //            }
        //            if (item != null)
        //            {
        //                item.REPLAY_TIME = DateTime.Now;
        //                await RunnerService.UpdateBulkItemAsync(item, token);
        //            }

        //            scope?.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        item.STATUS = (short)enumFileStatus.Failed;
        //        item.REMARK = ex.Message;
        //        item.ERROR = Dns.GetHostName();
        //        await RunnerService.UpdateBulkItemAsync(item, token);
        //    }
        //}
        //protected virtual System.Transactions.TransactionScope CreateTransactionScope()
        //{
        //    System.Transactions.TransactionScope scope = null;
        //    //If we already have transaction Scope, we will not create nested and Just return null.
        //    if (System.Transactions.Transaction.Current == null)
        //    {
        //        var transactionOptions = GetTransactionOptions();
        //        scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        //    }
        //    return scope;
        //}
        //protected virtual TransactionOptions GetTransactionOptions()
        //{
        //    var transactionOptions = new System.Transactions.TransactionOptions();
        //    transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
        //    transactionOptions.Timeout = TimeSpan.FromMinutes(AppSettingManager.Instance.Get<double>("TransactionTimeout", 20));//default timeout is 20 minutes
        //    return transactionOptions;
        //}
        #endregion Bulk Manager


        // Call parallel Thred

        //public static class IEnumerableExtension
        //{
        //    public static async Task ForEachAsync<T>(this IEnumerable<T> collection, Func<T, Task> action, int maxDegreeOfConcurrency = 1)
        //    {
        //        var activeTasks = new List<Task>(maxDegreeOfConcurrency);
        //        foreach (var task in collection.Select(action))
        //        {
        //            activeTasks.Add(task);
        //            if (activeTasks.Count == maxDegreeOfConcurrency)
        //            {
        //                await Task.WhenAny(activeTasks.ToArray());
        //                var faultedTask = activeTasks.FirstOrDefault(t => t.IsFaulted);
        //                if (faultedTask != null)
        //                {
        //                    // will throw one of the exceptions
        //                    await Task.WhenAll(activeTasks);
        //                }
        //                activeTasks.RemoveAll(t => t.IsCompleted);
        //            }
        //        }
        //        {
        //            await Task.WhenAll(activeTasks.ToArray());
        //            var faultedTask = activeTasks.FirstOrDefault(t => t.IsFaulted);
        //            if (faultedTask != null)
        //            {
        //                // will throw one of the exceptions
        //                await Task.WhenAll(activeTasks);
        //            }
        //        }
        //    }
        //}
    }
}
