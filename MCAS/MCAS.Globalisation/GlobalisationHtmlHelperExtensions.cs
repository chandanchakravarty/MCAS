/******************************************************************************************
<Author				    : - Pravesh K Chandel
<Start Date				: -	15 Apr 2014
<End Date				: -	16 Apr 2014
<Description			: - This file is used to maintain HTML helper for Culture to Globlisation support
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
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
namespace MCAS.Globalisation
{
    public static class GlobalisationHtmlHelperExtensions
    {
        static void AddOtherValues(RouteData routeData, RouteValueDictionary destinationRoute)
        {
            foreach (var routeInformation in routeData.Values)
            {
                if (routeInformation.Key == GlobalisedRoute.CultureKey)
                    continue; //Do not re-add, it will throw, this is the old value anyway.
                destinationRoute.Add(routeInformation.Key, routeInformation.Value);
            }
        }
        public static MvcHtmlString GlobalisedRouteLink(this HtmlHelper htmlHelper, string linkText, string targetCultureName, RouteData routeData)
        {
            RouteValueDictionary globalisedRouteData = new RouteValueDictionary();
            globalisedRouteData.Add(GlobalisedRoute.CultureKey, targetCultureName);
            AddOtherValues(routeData, globalisedRouteData);
            return htmlHelper.RouteLink(linkText, globalisedRouteData);
        }
        public static MvcHtmlString GlobalisedRouteLink(this HtmlHelper htmlHelper, string linkText, string targetCultureName, RouteData routeData, object htmlAttributes)
        {
            RouteValueDictionary globalisedRouteData = new RouteValueDictionary();
            globalisedRouteData.Add(GlobalisedRoute.CultureKey, targetCultureName);
            AddOtherValues(routeData, globalisedRouteData);
            return htmlHelper.RouteLink(linkText, globalisedRouteData, htmlAttributes);
        }
    }
    // by PK Chandel to enforce security on querystring parameters
    #region encription of query string params
    public static class RouteEncryptDecrypt
    {
        public static string Encrypt(string plainText)
        {
            string key = "Mcas@Ebix#India";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        public static string Decrypt(string encryptedText)
        {
            string key = "Mcas@Ebix#India";
            byte[] DecryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[encryptedText.Length];

            DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = Convert.FromBase64String(encryptedText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        public static string getRouteString(object routeValues)
        {
            string RouteString = String.Empty;
            string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"];

            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        RouteString += isEncryptedParams.ToUpper().Equals("YES") ? "?" : "&";
                    }
                    RouteString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }
            return RouteString;
        }
    }

    public static class MvcExtensions
    {
        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
        {
            return EncodedActionLink(htmlHelper, linkText, actionName, "", routeValues, htmlAttributes);
        }
        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object htmlAttributes)
        {
            return EncodedActionLink(htmlHelper, linkText, actionName, controllerName, null, htmlAttributes);
        }
        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName="", object routeValues=null, object htmlAttributes=null)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            object myRoutes=null;
            //string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"];
            if (routeValues != null)
                myRoutes = UrlHelperExtension.getRoutValues(routeValues);
            string htmlActionLink = "";
            if (controllerName != string.Empty)
            {
                if (routeValues != null && htmlAttributes != null)
                    htmlActionLink = htmlHelper.ActionLink(linkText, actionName, controllerName, myRoutes, htmlAttributes).ToString();
                else if (routeValues == null && htmlAttributes != null)
                    htmlActionLink = htmlHelper.ActionLink(linkText, actionName, controllerName, htmlAttributes).ToString();
                else if (routeValues != null && htmlAttributes == null)
                    htmlActionLink = htmlHelper.ActionLink(linkText, actionName, controllerName, myRoutes, new { Class = "" }).ToString();
                else if (routeValues == null && htmlAttributes == null)
                    htmlActionLink = htmlHelper.ActionLink(linkText, actionName, controllerName, new { Class = "" }).ToString();
            }
            else
            {
                if (routeValues != null && htmlAttributes != null)
                    htmlActionLink = htmlHelper.ActionLink(linkText, actionName, myRoutes, htmlAttributes).ToString();
                else if (routeValues != null)
                    htmlActionLink = htmlHelper.ActionLink(linkText, actionName, myRoutes, new { Class = "" }).ToString();
                else if (htmlAttributes != null)
                    htmlActionLink = htmlHelper.ActionLink(linkText, actionName).ToString();
            }
            return new MvcHtmlString(htmlActionLink.ToString());
            /*if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "='" + d.Values.ElementAt(i) + "'";
                }
            }
            //<a href="/url?Param1=14">Link text??</a>
            StringBuilder ancor = new StringBuilder();
            ancor.Append("<a ");
            if (htmlAttributesString != string.Empty)
            {
                ancor.Append(htmlAttributesString);
            }
            ancor.Append(" href='");
            if (controllerName != string.Empty)
            {
                ancor.Append("/" + controllerName);
            }
            if (actionName != "Index")
            {
                ancor.Append("/" + actionName);
            }
            if (queryString != string.Empty)
            {
                //ancor.Append("?Q="+ Encrypt(queryString));
                if (isEncryptedParams.ToUpper().Equals("YES"))
                    ancor.Append("?Q=" + HttpUtility.UrlEncode(queryString));
                else
                    ancor.Append("?" + (queryString));
            }
            ancor.Append("'");
            ancor.Append(">");
            ancor.Append(linkText);
            ancor.Append("</a>");
            return new MvcHtmlString(ancor.ToString());
            */
        }

        public static MvcHtmlString RenderGrid(this HtmlHelper htmlHelper, IEnumerable<object> dataSource, EbixGrid.EbixGrid egrid)
        {
            egrid.DataSource = dataSource;
            string gridHtl = egrid.RenderGrid();
            return new MvcHtmlString(gridHtl);
        }

        public static System.Collections.Specialized.NameValueCollection MCASQueryString(this HtmlHelper htmlHelper, System.Collections.Specialized.NameValueCollection queryString)
        {
            System.Collections.Specialized.NameValueCollection decryptedParameters = new System.Collections.Specialized.NameValueCollection();
            string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"];
            if (isEncryptedParams.ToUpper().Equals("YES") && queryString.Get("Q") != null)
            {
                //if (Request.QueryString.Get("Q") != null)
                {
                    string encryptedQueryString = queryString.Get("Q");
                    string decrptedString = RouteEncryptDecrypt.Decrypt(encryptedQueryString.ToString());
                    string[] paramsArrs = decrptedString.Split('?');

                    for (int i = 0; i < paramsArrs.Length; i++)
                    {
                        string[] paramArr = paramsArrs[i].Split('=');
                        int intOut;
                        if (paramArr.Length != 2)
                        {
                            decryptedParameters.Add(paramArr[0], "0");
                        }
                       else if (int.TryParse(paramArr[1], out intOut))
                            decryptedParameters.Add(paramArr[0], intOut.ToString());
                        else
                            decryptedParameters.Add(paramArr[0], (paramArr[1]));
                    }
                }
            }
            else
            {
                foreach (var param in queryString.Keys)
                {
                    if (param == null) continue;
                    var paramVal = queryString[param.ToString()].ToString();
                    decryptedParameters.Add(param.ToString(), paramVal.ToString());
                }
            }

            return decryptedParameters;
        }
        public static MvcHtmlString RawHtml(this string original)
        {
            return MvcHtmlString.Create(original);
        }
        public static MvcHtmlString RenderDynamicHtml(this HtmlHelper htmlHelper, string divId, string ScreenID)
        {
            try
            {
                System.Web.UI.Page mypage = new System.Web.UI.Page();
                System.Web.UI.HtmlControls.HtmlGenericControl mDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
                mDiv.ID = divId + "_inner";
                mypage.EnableEventValidation = false;
                HtmlForm form = new HtmlForm();
                form.Name = "form1";
                form.ID = "form_" + divId;
                form.Method = "None";
                form.Action = "None";
                mypage.Controls.Add(form);
                form.Controls.Add(mDiv);
                EbixDynamicPageLib.EbixDynamicPageBase dynapg = new EbixDynamicPageLib.EbixDynamicPageBase(mypage, "en-US", 1);
                dynapg.DBConnectionString = "";
                dynapg.PageXmlPath = getPageXmlPath(ScreenID);
                dynapg.HtmlRequired = true;
                string pgHtml = dynapg.LoadPageControls("en-US", mDiv);
                //System.Web.UI.Control control = mypage.FindControl(divId);
                ////call RenderControl method to get the generated HTML

                //string pgHtml = RenderControl(mypage);

                TagBuilder div = new TagBuilder("div");
                div.MergeAttribute("Id", divId);
                //div.MergeAttribute("class", "maindiv");
                div.InnerHtml = pgHtml;
                mypage.Dispose();
                form.Dispose();
                return MvcHtmlString.Create(div.ToString());
            }
            catch (Exception ex)
            {
                return new MvcHtmlString(ex.Message);
            }
        }
        private static string getPageXmlPath(string ScreenId)
        {
            string path = "";
            if (ScreenId == "Country")
                path = @"\PageXml\Country.xml";
            return path;
        }
    }

    public static class UrlHelperExtension
    {
        /*public static string ActionEncoded(this UrlHelper helper, StpLibrary.RouteObject customLinkObject)
        {
            return HttpUtility.HtmlEncode(helper.Action(customLinkObject.Action, customLinkObject.Controller, customLinkObject.Routes));
        }*/
        public static string ActionEncoded(this UrlHelper helper, string action)
        {
            return HttpUtility.HtmlEncode(helper.Action(action));
        }
        public static string ActionEncoded(this UrlHelper helper, string action, object routeValues)
        {
            object objRoute = getRoutValues(routeValues);
            //string retVal = HttpUtility.HtmlEncode(helper.Action(action, objRoute));
            string retVal = helper.Action(action, objRoute);
            return retVal;
        }
        public static string ActionEncoded(this UrlHelper helper, string action, string controller, object routeValues = null)
        {
            object objRoute = null;
            if (routeValues != null) objRoute = getRoutValues(routeValues);
            //string retVal = HttpUtility.HtmlEncode(helper.Action(action, controller, objRoute));
            string retVal = routeValues == null ? helper.Action(action, controller) : helper.Action(action, controller, objRoute);
            return retVal;
        }

        internal static object getRoutValues(object routeValues)
        {
            string strRouteString = RouteEncryptDecrypt.getRouteString(routeValues);
            object objRoute;
            string isEncryptedParams = System.Configuration.ConfigurationManager.AppSettings["EncryptQueryParams"];
            if (isEncryptedParams.ToUpper().Equals("YES"))
            {
                strRouteString = RouteEncryptDecrypt.Encrypt(strRouteString);
                objRoute = new { Q = strRouteString };
            }
            else
                objRoute = routeValues;

            return objRoute;
        }
    }
    #endregion
}
