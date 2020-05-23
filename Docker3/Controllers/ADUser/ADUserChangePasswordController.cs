using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.DirectoryServices.AccountManagement;
using System.Linq.Expressions;

namespace RESTDemo.Controllers {
    public class ADUserChangePasswordController : ApiController {

        public string Get(string username, string oldpassword, string newpassword) {

            // https://b606mgt/api/ADUserChangePassword?username=daves&oldpassword=myoldpassword&newpassword=mynewpassword
            // MUST be properly URL encoded strings for username, old and new password


            UserPrincipal up = null;
            PrincipalContext pc = null;

            // Validate user exists
            try {
                pc = new PrincipalContext(ContextType.Domain);
                up = UserPrincipal.FindByIdentity(pc, username);

                if (up == null) return "User does not exist!";

            } catch (Exception ex) {
                return "Error:  " + ex.Message.ToString();
            }
            
            // Validate user is enabled (not disabled)
            try {
                if (up.Enabled == false) {
                    return "User account is disabled";
                }
            } catch (Exception ex) {
                return "Error: " + ex.Message.ToString();
            }

            // Changer user password
            try { 
                up.ChangePassword(oldpassword, newpassword);
                return "Successfully changed password";

            } catch (PasswordException px) {
                return "Password Error: " + px.Message.ToString();
            } catch (Exception ex) {
                return "Error: " + ex.Message.ToString();
            }


            // TODO - do NOT RUN - If user is not valid, it will change your password, not the user name you looked up
            /*
            using (var context = new PrincipalContext(ContextType.Domain)) {
                using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username)) {
                    //user.SetPassword("newpassword");
                    // or
                    //user.ChangePassword("oldPassword", "newpassword");
                    //user.Save();
                    return "Success";
                }
            */
            /*
            * 
            *namespace PasswordChanger
            {
                using System;
                using System.DirectoryServices.AccountManagement;

                class Program
                {
                    static void Main(string[] args)
                    {
                        ChangePassword("domain", "user", "oldpassword", "newpassword");
                    }

                    public static void ChangePassword(string domain, string userName, string oldPassword, string newPassword)
                    {
                        try
                        {
                            using (var context = new PrincipalContext(ContextType.Domain, domain))
                            using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName))
                            {
                                user.ChangePassword(oldPassword, newPassword);
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
            } 
            */
        }
    }
}
