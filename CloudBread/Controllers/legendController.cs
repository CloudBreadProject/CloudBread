using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

//암호화를 위해 추가할 두가지
using System.Security.Cryptography;
using CloudBreadLib.BAL.Crypto;

namespace CloudBread.Controllers
{
    public class legendController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/legend
        public string Get(string plainText)
        {
            //Services.Log.Info("Hello from custom controller!");
            //MD5 처리 수행
            string cryptedText = Crypto.MD5Hash(plainText);
            return cryptedText;
        }

    }
}
