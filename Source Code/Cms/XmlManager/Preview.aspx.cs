using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Preview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument booksDoc = new XmlDocument();
        Response.ContentType = "text/xml";
        try
        {
            booksDoc.PreserveWhitespace = true;
            booksDoc.LoadXml(Session["XmlStream"].ToString());
            //Write the XML onto the browser
            Response.Write(booksDoc.OuterXml);
        }
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