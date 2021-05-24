using System.Web.Mvc;
using System.Web.Routing;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;

namespace GPPSU
{
    public class MvcApplication : WebApplication
    {
        public MvcApplication()
        {
            #region old setting App
            ApplicationSettings.Instance.Name = "Global Production & Logistics System";
            ApplicationSettings.Instance.Alias = "GPPSU";
            ApplicationSettings.Instance.OwnerName = "Toyota Motor Manufacturing Indonesia";
            ApplicationSettings.Instance.OwnerAlias = "TMMIN";
            ApplicationSettings.Instance.OwnerEmail = "tdk@toyota.co.id";
            //ApplicationSettings.Instance.Security.UnauthorizedController = "NotAuthorize"; //redirect controller if user login not allowed permission (controller must be exists in app)
            ApplicationSettings.Instance.Runtime.HomeController = "Home"; //default controller after login (controller must be exists in app)
            ApplicationSettings.Instance.Menu.Enabled = true; // option setting enable/disable all menu

            ApplicationSettings.Instance.Security.EnableAuthentication = true; // option setting authentication app  
            //ApplicationSettings.Instance.Security.IgnoreAuthorization = true; // option setting ignore or restrict controller
            //ApplicationSettings.Instance.Security.EnableSingleSignOn = false; // option setting using SSO service or not
            #endregion


            #region new setting app
            ApplicationSettings.Instance.DefaultDbSc = "SecurityCenter";    // default connfig key for DB SC
            ApplicationSettings.Instance.Menu.SecurityCenter = false;        // option setting data menu (true=get menu from sc, false =get data menu from xml)
            ApplicationSettings.Instance.Security.EnableTracking = false;    // option setting tracking (DB : SC , Table : TB_T_COUNTER)
            ApplicationSettings.Instance.Security.Encrypt = false;           // Option setting encryption password/ not
            #endregion

            BypassLogin(true);
            #region simulation user
            //ApplicationSettings.Instance.Security.SimulateAuthenticatedSession = false;

            #endregion
        }

        private void BypassLogin(bool isBypass)
        {
            if (isBypass)
            {
                ApplicationSettings.Instance.Security.IgnoreAuthorization = true;
                ApplicationSettings.Instance.Security.SimulateAuthenticatedSession = true;
                ApplicationSettings.Instance.Security.SimulatedAuthenticatedUser = new User()
                {
                    Username = "dummy",
                    Password = "toyota",
                    FirstName = "Anonymous",
                    LastName = "User",
                    RegistrationNumber = "123456789"
                };

                ApplicationSettings.Instance.Security.SimulatedAuthenticatedUser.Company = new Toyota.Common.CompanyInfo()
                {
                    Id = "2000"
                };



            }
            else
            {
                ApplicationSettings.Instance.Security.IgnoreAuthorization = false;
                ApplicationSettings.Instance.Security.SimulateAuthenticatedSession = false;
                ApplicationSettings.Instance.Security.EnableSingleSignOn = false;
            }
        }

        protected new void Application_Start()
        {
            base.Application_Start();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            //ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
        protected override void Startup()
        {
            ProviderRegistry.Instance.Register<IUserProvider>(typeof(DbUserProvider), DatabaseManager.Instance, ApplicationSettings.Instance.DefaultDbSc);
            ProviderRegistry.Instance.Register<ISingleSignOnProvider>(typeof(SingleSignOnProvider), ProviderRegistry.Instance.Get<IUserProvider>(), DatabaseManager.Instance, "SSO");

        }
    }
}