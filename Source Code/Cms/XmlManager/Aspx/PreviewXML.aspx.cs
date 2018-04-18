using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Cms.XmlManager.Aspx
{
    public partial class PreviewXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                XmlDocument booksDoc = new XmlDocument();
                booksDoc.LoadXml(Session["XmlStream"].ToString());
                string strUrl = HttpContext.Current.Server.MapPath("~\\XmlManager\\support") + "\\" + Guid.NewGuid().ToString() + ".xml";
                booksDoc.Save(strUrl);
                System.IO.FileInfo file = new System.IO.FileInfo(strUrl);
                string sFileName = file.Name;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + sFileName);
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/xml";
                Response.WriteFile(file.FullName);
                Response.Flush();
                System.IO.File.Delete(strUrl);
                Response.Clear();
                Response.End();


            }
            catch (System.Threading.ThreadAbortException e1)
            { }
            catch (XmlException xmlEx)
            {
                Response.Write("XmlException: " + xmlEx.Message);
            }
            catch (Exception ex)
            {
                Response.Write("Exception: " + ex.Message);
            }
        }
    }
}