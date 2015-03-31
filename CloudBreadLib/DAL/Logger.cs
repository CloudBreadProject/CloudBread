using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.StorageClient
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;

namespace CloudBreadLib.DAL.Logger
{
    public class CBLogMessage
    {
        public string DBConnectionString { get; set; }
        public string StorageConnectionString { get; set; }
        public string CloudBreadLoggerSetting { get; set; }
        public string memberID { get; set; }
        public string jobID { get; set; }
        public string Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }

    public class CBATSMessageEntity : TableEntity
    {
        public string DBConnectionString { get; set; }
        public string StorageConnectionString { get; set; }
        public string CloudBreadLoggerSetting { get; set; }
        public CBATSMessageEntity(string pkey, string rkey)
        {
            this.PartitionKey = pkey;
            this.RowKey = rkey;
        }
        public string MemberID { get; set; }
        public string jobID { get; set; }
        public string Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

    }

    //Loggers may be assigned levels. Levels are instances of the log4net.Core.Level class. The following levels are defined in order of increasing priority: 
        //• ALL 
        //• DEBUG 
        //• INFO 
        //• WARN 
        //• ERROR 
        //• FATAL 
        //• OFF 


    

    public class Logger
    {
        //SQL에 작성
        public static bool CBLogger(CBLogMessage message)
        {
            if (message.CloudBreadLoggerSetting != "")
            {
                if (string.IsNullOrEmpty(message.memberID))
                {
                    message.memberID = "";
                }

                // 로그 저장 try :: 개별 try 할까?
                try
                {
                    switch (message.CloudBreadLoggerSetting)
                    {
                        case "SQL":
                            //DB로 EF를 쓰지 않고 독립 저장
                            string strQuery = string.Format("insert into dbo.CloudBreadLog(memberid, jobID, [date], [Thread], [Level], [Logger], [Message], [Exception]) values({0},{1},{2},{3},{4},{5}, {6}, {7})", message.memberID, DateTimeOffset.UtcNow, message.Thread, message.Level, message.Logger, message.Message, message.Exception);
                            SqlConnection connection = new SqlConnection(message.DBConnectionString);
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(strQuery, connection);
                                int rowcount = command.ExecuteNonQuery();
                                connection.Close();
                                //Console.WriteLine(rowcount);
                                break;
                            }

                        case "ATS":
                        //ATS로 독립 저장
                            {
                                // 안전한 버퍼를 이용하는 방법 고민 필요
                                CloudStorageAccount storageAccountQ = CloudStorageAccount.Parse(message.StorageConnectionString);
                                CloudTableClient tableClient = storageAccountQ.CreateCloudTableClient();
                                var tableClient1 = storageAccountQ.CreateCloudTableClient();
                                CloudTable table = tableClient.GetTableReference("CloudBreadLog");
                                CBATSMessageEntity Message = new CBATSMessageEntity(message.memberID, Guid.NewGuid().ToString());
                                Message.jobID = message.jobID;
                                Message.Date = DateTimeOffset.UtcNow.ToString();
                                Message.Thread = message.Thread;
                                Message.Level = message.Level;
                                Message.Logger = message.Logger;
                                Message.Message = message.Message;
                                Message.Exception = message.Exception;
                                TableOperation insertOperation = TableOperation.Insert(Message);
                                table.Execute(insertOperation);
                                break;
                            }

                        case "AQS":
                            //Azure Queue Storage로 저장
                            {
                                // 안전한 버퍼를 이용하는 방법 고민 필요
                                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(message.StorageConnectionString);
                                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                                CloudQueue queue = queueClient.GetQueueReference("messagestolog");      // 반드시 소문자
                                
                                CBATSMessageEntity Message = new CBATSMessageEntity(message.memberID, Guid.NewGuid().ToString());
                                Message.jobID = message.jobID;
                                Message.Date = DateTimeOffset.UtcNow.ToString();
                                Message.Thread = message.Thread;
                                Message.Level = message.Level;
                                Message.Logger = message.Logger;
                                Message.Message = message.Message;
                                Message.Exception = message.Exception;

                                CloudQueueMessage Qmessage = new CloudQueueMessage(Message.ToString());
                                queue.AddMessage(Qmessage);

                                break;
                            }

                        case "DocDB":
                            //DocDB로 저장
                            break;

                        default:
                            //저장안함
                            break;
                        }
                }
                catch (Exception)
                {
                    // 로그 저장 실패시 오류를 throw :: 재시도 하는 로직을 넣을까?
                    throw;
                }               
            }
            return true;
        }
    }
}

