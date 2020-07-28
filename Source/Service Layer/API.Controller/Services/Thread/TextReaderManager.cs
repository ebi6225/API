//using Consolsys.CB.Host.Common;
//using Consolsys.CB.Host.Common.Abstract.Repository;
//using Consolsys.CB.Host.Common.Interfaces;
//using Consolsys.CB.Modules.Core;
//using Consolsys.CB.Modules.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Consolsys.CB.Modules.Core.Service.Business.Bulk
//{
//    public class TextReaderManager
//    {
//        #region         
//        public string ItemListJSONstring { get; set; }
//        public string HeaderJSONstring { get; set; }
//        public string FooterJSONstring { get; set; }
//        public string HeaderString { get; set; }
//        public string FooterString { get; set; }
//        #endregion

//        internal async Task<BULK_SCHEMA> GetBulkSchemaAsync(enumBulkSchemaType SchemaType, CancellationToken token = default)
//        {
//            var control = (await ServiceFactory.Instance.Resolve<IRepository<BULK_SCHEMA>>().SelectAsync(x => x.SCHEMA_TYPE == (short)SchemaType, token))
//                                                                                                  .FirstOrDefault();

//            if (control != null)
//                return control;
//            else
//                return null;
//        }

//        // ItemType typeof(object).ToString()
//        // AssemblyName typeof(object).Assembly.FullName
//        public async Task TextMaperAsync(List<string> FileLines, string HeaderType, List<SectionInfo> SectionInfoList, string FooterType, string ItemType, string AssemblyName, enumBulkSchemaType SchemaType, CancellationToken token = default)
//        {
//            try
//            {
//                TextFileReaderManager manager = new TextFileReaderManager();
//                manager.HeaderType = HeaderType;
//                manager.FooterType = FooterType;
//                manager.FileLines = FileLines;//Mandatory
//                manager.SectionInfoList = SectionInfoList;//Mandatory                
//                manager.AssemblyName = AssemblyName;//Mandatory
//                manager.ItemType = ItemType;//Mandatory
//                manager.SchemaType = SchemaType;

//                await manager.ExecuteAsync(token);

//                ItemListJSONstring = string.IsNullOrEmpty(manager.ItemListJSONstring) ? string.Empty : manager.ItemListJSONstring;
//                HeaderJSONstring = string.IsNullOrEmpty(manager.HeaderJSONstring) ? string.Empty : manager.HeaderJSONstring;
//                FooterJSONstring = string.IsNullOrEmpty(manager.FooterJSONstring) ? string.Empty : manager.FooterJSONstring;

//                HeaderString = string.IsNullOrEmpty(manager.HeaderString) ? string.Empty : manager.HeaderString;
//                FooterString = string.IsNullOrEmpty(manager.FooterString) ? string.Empty : manager.FooterString;
//            }
//            catch (Exception ex)
//            {
//                ServiceFactory.Instance.Resolve<IContext>().Logger.SendMessage(LogLevel.Error, "TextMaper", ex.Message);
//                throw;
//            }
//        }
//    }
//}