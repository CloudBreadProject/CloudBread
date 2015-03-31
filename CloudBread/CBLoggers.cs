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
using System.Web.Configuration;
using Newtonsoft.Json;
using CloudBread.globals;

namespace Logger.Logging
{

    public class Logging {

        public class CBLoggers
        {
            public string memberID { get; set; }
            public string jobID { get; set; }       // Scheduled job 에서 사용
            public string Date { get; set; }
            public string Thread { get; set; }
            public string Level { get; set; }
            public string Logger { get; set; }
            public string Message { get; set; }
            public string Exception { get; set; }
        }

        public class CBATSMessageEntity : TableEntity
        {
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

        public static bool RunLog(CBLoggers message)
        {
            if (globalVal.CloudBreadLoggerSetting != "")
            {
                if (string.IsNullOrEmpty(message.memberID))
                {
                    message.memberID = "";
                }

                //ERROR는 바로 DB - CloudBreadErrorLog 로 저장
                if (message.Level.ToUpper() == "ERROR")
                {
                    try
                    {
                        string strQuery = string.Format("insert into dbo.CloudBreadErrorLog(memberid, jobID, [Thread], [Level], [Logger], [Message], [Exception]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                               message.memberID,
                               message.jobID,
                               message.Thread,
                               message.Level,
                               message.Logger,
                               message.Message,
                               message.Exception
                               );

                        SqlConnection connection = new SqlConnection(globalVal.DBConnectionString);
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(strQuery, connection);
                            int rowcount = command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    catch (Exception)
                    {
                        // DB 로깅이 실패했을 경우
                        throw;
                    }

                }
                else
                {
                    // 조건에 따라 사용자 로그 저장
                    try
                    {
                        switch (globalVal.CloudBreadLoggerSetting)
                        {
                            case "SQL":
                                //DB로 저장
                                string strQuery = string.Format("insert into dbo.CloudBreadLog(memberid, jobID, [Thread], [Level], [Logger], [Message], [Exception]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                               message.memberID,
                               message.jobID,
                               message.Thread,
                               message.Level,
                               message.Logger,
                               message.Message,
                               message.Exception
                               );
                                SqlConnection connection = new SqlConnection(globalVal.DBConnectionString);
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
                                    CloudStorageAccount storageAccountQ = CloudStorageAccount.Parse(globalVal.StorageConnectionString);
                                    CloudTableClient tableClient = storageAccountQ.CreateCloudTableClient();
                                    var tableClient1 = storageAccountQ.CreateCloudTableClient();
                                    CloudTable table = tableClient.GetTableReference("CloudBreadLog");
                                    CBATSMessageEntity Message = new CBATSMessageEntity(message.memberID, Guid.NewGuid().ToString());       //memberid를 파티션키로 쓴다.
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
                                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(globalVal.StorageConnectionString);
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

                                    CloudQueueMessage Qmessage = new CloudQueueMessage(JsonConvert.SerializeObject(Message));
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
                        // 로그 저장 실패시 오류를 throw :: 재시도 하는 로직?
                        throw;
                    }
                }
            }
            return true;
        }


    }
    
}

