using System;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;
using Microsoft.WindowsAzure.Storage.Table;
using System.Text;
using Newtonsoft.Json;

namespace CloudBread.DataObjects
{
    public class TodoItem : StorageData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}
