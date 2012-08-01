using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Security;
using WebMatrix.WebData;

[assembly: WebActivator.PreApplicationStartMethod(typeof (TournamentReport.App_Start.SimpleMembershipMvc3), "Start")]
[assembly:
    WebActivator.PostApplicationStartMethod(typeof (TournamentReport.App_Start.SimpleMembershipMvc3), "Initialize")]

namespace TournamentReport.App_Start
{
    public static class SimpleMembershipMvc3
    {
        public static readonly string EnableSimpleMembershipKey = "enableSimpleMembership";

        public static bool SimpleMembershipEnabled
        {
            get { return IsSimpleMembershipEnabled(); }
        }

        public static void Initialize()
        {
            // Modify the settings below as appropriate for your application
            WebSecurity.InitializeDatabaseConnection(
                connectionStringName: "TournamentReport.TournamentContext",
                userTableName: "Users",
                userIdColumn: "ID",
                userNameColumn: "Name",
                autoCreateTables: true);

            // Comment the line above and uncomment these lines to use the IWebSecurityService abstraction
            //var webSecurityService = DependencyResolver.Current.GetService<IWebSecurityService>();
            //webSecurityService.InitializeDatabaseConnection(connectionStringName: "Default", userTableName: "Users", userIdColumn: "ID", userNameColumn: "Username", autoCreateTables: true);
            SeedMembershipData();
        }

        private static void SeedMembershipData()
        {
            if (!Roles.RoleExists("Administrators"))
            {
                Roles.CreateRole("Administrators");
            }
            if (!WebSecurity.UserExists("Admin"))
            {
                // TODO: Make sure password reset functionality works.
                WebSecurity.CreateUserAndAccount("Admin", GenerateRandomPassword());
            }
        }

        private static string GenerateRandomPassword()
        {
            var data = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }

        public static void Start()
        {
            if (SimpleMembershipEnabled)
            {
                MembershipProvider provider = Membership.Providers["AspNetSqlMembershipProvider"];
                if (provider != null)
                {
                    MembershipProvider currentDefault = provider;
                    SimpleMembershipProvider provider2 =
                        CreateDefaultSimpleMembershipProvider("AspNetSqlMembershipProvider", currentDefault);
                    Membership.Providers.Remove("AspNetSqlMembershipProvider");
                    Membership.Providers.Add(provider2);
                }
                Roles.Enabled = true;
                RoleProvider provider3 = Roles.Providers["AspNetSqlRoleProvider"];
                if (provider3 != null)
                {
                    RoleProvider provider6 = provider3;
                    SimpleRoleProvider provider4 = CreateDefaultSimpleRoleProvider("AspNetSqlRoleProvider", provider6);
                    Roles.Providers.Remove("AspNetSqlRoleProvider");
                    Roles.Providers.Add(provider4);
                }
            }
        }

        #region : Private Methods :

        private static bool IsSimpleMembershipEnabled()
        {
            bool flag;
            string str = ConfigurationManager.AppSettings[EnableSimpleMembershipKey];
            if (!string.IsNullOrEmpty(str) && bool.TryParse(str, out flag))
            {
                return flag;
            }
            return true;
        }

        private static SimpleMembershipProvider CreateDefaultSimpleMembershipProvider(string name,
                                                                                      MembershipProvider currentDefault)
        {
            MembershipProvider previousProvider = currentDefault;
            SimpleMembershipProvider provider = new SimpleMembershipProvider(previousProvider);
            NameValueCollection config = new NameValueCollection();
            provider.Initialize(name, config);
            return provider;
        }

        private static SimpleRoleProvider CreateDefaultSimpleRoleProvider(string name, RoleProvider currentDefault)
        {
            RoleProvider previousProvider = currentDefault;
            SimpleRoleProvider provider = new SimpleRoleProvider(previousProvider);
            NameValueCollection config = new NameValueCollection();
            provider.Initialize(name, config);
            return provider;
        }

        #endregion
    }
}