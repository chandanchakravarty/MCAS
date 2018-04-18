using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        /*try
        {
            if (FileUpload1.HasFile)
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load((Stream)FileUpload1.PostedFile.InputStream);
                XmlNodeList xNode = xdoc.SelectNodes("//PageElement");
                Session["XmlStream"] = xdoc.OuterXml;
                if (xNode.Count > 0)
                {
                    Response.Redirect("PageXML.aspx");
                }
                else
                {
                    xNode = xdoc.SelectNodes("//FACTOR");
                    if (xNode.Count > 0)
                    {
                        Response.Redirect("RatingXML.aspx");
                    }
                    else
                    {
                        xNode = xdoc.SelectNodes("//screen");

                        if (xNode.Count > 0)
                        {
                            Response.Redirect("MessageXML.aspx");
                        }
                        else
                        {
                            xNode = xdoc.SelectNodes("//HeaderString");
                            if (xNode.Count > 0)
                            {
                                Response.Redirect("GridXML.aspx");
                            }
                            else
                            {
                                Label1.Text = "Application does not support Selected XML";

                            }
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        {
            Label1.Text = "Application does not support Selected File";
        }*/
    }
}