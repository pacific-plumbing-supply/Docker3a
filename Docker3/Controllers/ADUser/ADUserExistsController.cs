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
    public class ADUserExistsController : ApiController
    {
        // GET: api/ADUserExists?username=<user samaccountname>
        public Boolean Get(string username) {

            using (var domainContext = new PrincipalContext(ContextType.Domain)) {
                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, username)) {
                    return foundUser != null;
                }
            }
        }
    }
}
