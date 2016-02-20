/**
* @file CBAuth.cs
* @brief Processing CloudBread auth related task class including 3rd party authentication. \n
* @author Dae Woo Kim
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Azure.Mobile.Server.Config;
using System.Security.Claims;

namespace CloudBread
{
    /**
    * @class CBAuth 
    * @brief Processing CloudBread auth related task class including 3rd party authentication. \n
    */
    public class CBAuth
    {

        /*
        * @brief Before setup the authentication provider in Azure Portal MobileApp, return passed memberID for dev and test purpose. \n
        * @param pMemberID
        * @param pClaim object
        */
        public static string getMemberID(string pMemberID, ClaimsPrincipal pClaim )
        {
            string sid;
            /// Get the SID of the current user.
            if (pClaim.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                sid = pMemberID;
            }
            else
            {
                /// @brief return SID from claim object
                sid = pClaim.FindFirst(ClaimTypes.NameIdentifier).Value.Replace("sid:", "");
            }

            return sid;

        }
    }
}