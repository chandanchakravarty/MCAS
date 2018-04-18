/******************************************************************************************
<Author				    : - Pravesh K Chandel
<Start Date				: -	15 Apr 2014
<End Date				: -	16 Apr 2014
<Description			: - This file is used to maintain Culture routes for Globlisation support
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace MCAS.Globalisation
{
    public class CultureRouteConstraint : IRouteConstraint
    {

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
                return false;
            string potentialCultureName = (string)values[parameterName];
            return CultureFormatChecker.FormattedAsCulture(potentialCultureName);
        }
    }

    public class GlobalisationRouteHandler : MvcRouteHandler
    {
        string CultureValue
        {
            get
            {
                return (string)RouteDataValues[GlobalisedRoute.CultureKey];
            }
        }

        RouteValueDictionary RouteDataValues { get; set; }

        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            RouteDataValues = requestContext.RouteData.Values;
            CultureManager.SetCulture(CultureValue);
            return base.GetHttpHandler(requestContext);
        }

        public GlobalisationRouteHandler()
            : base()
        {

        }

        public GlobalisationRouteHandler(IControllerFactory controllerFactory)
            : base(controllerFactory)
        {

        }
    }

    public class GlobalisedRoute : Route
    {

        public const string CultureKey = "culture";

        static string CreateCultureRoute(string unGlobalisedUrl)
        {
            return string.Format("{{" + CultureKey + "}}/{0}", unGlobalisedUrl);
        }

        /// <summary>
        ///    Initializes a new instance of the System.Web.Routing.Route class, by using
        ///    the specified URL pattern, default parameter values, and handler class.
        /// </summary>
        /// <param name="unGlobalisedUrl">The URL pattern for the route, without the culture</param>
        /// <param name="defaults"The values to use for any parameters that are missing in the URL.></param>
        public GlobalisedRoute(string unGlobalisedUrl, RouteValueDictionary defaults) :
            base(CreateCultureRoute(unGlobalisedUrl),
                    defaults,
                    new RouteValueDictionary(new { culture = new CultureRouteConstraint() }),
                    new GlobalisationRouteHandler())
        {
        }
    }

    public class LocalizedMvcRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            CultureInfo ci = new CultureInfo(requestContext.RouteData.Values["culture"].ToString());

            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

            return base.GetHttpHandler(requestContext);
        }
    }

    public class LocalizedViewEngine : RazorViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            List<string> searched = new List<string>();

            if (!string.IsNullOrEmpty(partialViewName))
            {
                ViewEngineResult result;

                result = base.FindPartialView(controllerContext, string.Format("{0}.{1}", partialViewName, CultureInfo.CurrentUICulture.Name), useCache);

                if (result.View != null)
                {
                    return result;
                }

                searched.AddRange(result.SearchedLocations);

                result = base.FindPartialView(controllerContext, string.Format("{0}.{1}", partialViewName, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName), useCache);

                if (result.View != null)
                {
                    return result;
                }

                searched.AddRange(result.SearchedLocations);
            }

            return new ViewEngineResult(searched.Distinct().ToList());
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            List<string> searched = new List<string>();

            if (!string.IsNullOrEmpty(viewName))
            {
                ViewEngineResult result;

                result = base.FindView(controllerContext, string.Format("{0}.{1}", viewName, CultureInfo.CurrentUICulture.Name), masterName, useCache);

                if (result.View != null)
                {
                    return result;
                }

                searched.AddRange(result.SearchedLocations);

                result = base.FindView(controllerContext, string.Format("{0}.{1}", viewName, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName), masterName, useCache);

                if (result.View != null)
                {
                    return result;
                }

                searched.AddRange(result.SearchedLocations);
            }

            return new ViewEngineResult(searched.Distinct().ToList());
        }
    }
}
