using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using MCAS.Globalisation;
using MCAS.Controllers;
using MCAS.Entity;
using System.Web.SessionState;
using System.Web.Http.Validation.Providers;
namespace MCAS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ModelMetadataProviders.Current = new CachedDataAnnotationsModelMetadataProvider();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            ViewEngines.Engines.Insert(0, new LocalizedViewEngine());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            ////-------Added by Ashish Rai for mapping angular model with c# viewModel----------////
            GlobalConfiguration.Configuration.Services.RemoveAll(
typeof(System.Web.Http.Validation.ModelValidatorProvider),
v => v is InvalidModelValidatorProvider);
            ////-------Added by Ashish Rai for mapping angular model with c# viewModel----------////

        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        //------Added by Ashish Rai for getting session in ServiceController i.e. APIController------//
        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
        //------Added by Ashish Rai for getting session in ServiceController i.e. APIController------//

        public static void RegisterRoutes(RouteCollection routes)
        {
            const string defautlRouteUrl = "{controller}/{action}/{id}";

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteValueDictionary defaultRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Login", id = UrlParameter.Optional });
            routes.Add("DefaultGlobalised", new GlobalisedRoute(defautlRouteUrl, defaultRouteValueDictionary));
            routes.Add("Default", new Route(defautlRouteUrl, defaultRouteValueDictionary, new MvcRouteHandler()));
            routes.MapRoute(
                "Default2",
                "{culture}/{controller}/{action}/{id}",
                new
                {
                    culture = "en",
                    controller = "Home",//ControllerName
                    action = "Login",//ActionName
                    id = UrlParameter.Optional
                }
            ).RouteHandler = new LocalizedMvcRouteHandler();
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            (new BaseController()).ErrorLog(exception.Message, exception.InnerException);
            Server.ClearError();

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("exception", exception);

            if (exception.GetType() == typeof(HttpException))
            {
                routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
            }
            else
            {
                routeData.Values.Add("statusCode", 500);
            }

            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Response.End();

            //var exception = Server.GetLastError();
            /*if (exception is HttpUnhandledException)
            {
                Server.Transfer("~/Error.cshtml");
            }
            if (exception != null)
            {
                Server.Transfer("~/Error.cshtml");
            }
            try
            {
                // This is to stop a problem where we were seeing "gibberish" in the
                // chrome and firefox browsers
                HttpApplication app = sender as HttpApplication;
                app.Response.Filter = null;
            }
            catch
            {
            }*/
        }
        protected void Session_End(Object sender, EventArgs e)
        {
            if (Session["LoginAuditLogId"] != null)
            {
                MCASEntities obj = new MCASEntities();
                obj.Proc_InsertLogOutTime(Int32.Parse(Session["LoginAuditLogId"].ToString()));
                obj.Dispose();
            }
        }
        
    }

}