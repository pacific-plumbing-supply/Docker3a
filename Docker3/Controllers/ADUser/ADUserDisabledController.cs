/*
 * 
 * 
 * 
 *  Revision History
 *      2020-0518 DVS   Coded and debugged.  Approved for production
 * 
 * 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.DirectoryServices.AccountManagement;

namespace RESTDemo.Controllers
{
    public class ADUserDisabledController : ApiController
    {
        // GET: api/ADUserDisabled?username=<ad sam account name>
        public Boolean Get(string username) {

            using (PrincipalContext domainContext = new PrincipalContext(ContextType.Domain)) {
                using (UserPrincipal foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, username)) {
                    if (foundUser != null) {
                        if (foundUser.Enabled == false) return true;
                        else return false;

                    } else return false;
                    //return foundUser != null;
                }
            }
        }


    }
}