using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using Cms.BusinessLayer.BlCommon;

public partial class TreeControlPage : System.Web.UI.UserControl
{
    DataSet ds = new DataSet();
    List<ATTRIBUTE> lstA = new List<ATTRIBUTE>();
    List<NODE> lstN = new List<NODE>();
    List<FACTOR> lstF = new List<FACTOR>();
    List<PRODUCT> lstP = new List<PRODUCT>();

    // Delegate declaration
    public delegate void LinkClicked(XmlNodeList xNode);

    // Event declaration
    public event LinkClicked trvHandler;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (!IsPostBack)
        {
            XmlDocument xdoc = new XmlDocument();

            xdoc.LoadXml(Session["XmlStream"].ToString());
            XmlNodeList xNode = xdoc.SelectNodes("//PageElement");
            ddlElement.Items.Add(new ListItem("All"));
            foreach (XmlNode p in xNode)
            {
                ddlElement.Items.Add(new ListItem(p.Attributes[0].Value));
            }

        }*/
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item is RepeaterItem)
        {
            LinkButton lblName = (LinkButton)e.Item.FindControl("lbl");
            XmlNode dv = (XmlNode)e.Item.DataItem;
            lblName.Text = (dv).Attributes[0].Value;
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        /*XmlDocument xdoc = new XmlDocument();

        xdoc.LoadXml(Session["XmlStream"].ToString());

        if (ddlElement.Text != "All")
        {
            string xPathExpression = "//PageElement[@name='" + ddlElement.Text + "']";
            XmlNodeList xNode = xdoc.SelectNodes(xPathExpression);
            rpt.DataSource = xNode;
            rpt.DataBind();
        }
        else
        {
            XmlNodeList xNode = xdoc.SelectNodes("//PageElement");
            rpt.DataSource = xNode;
            rpt.DataBind();
        }
        */
    }
    protected void lnkValue_Click(object sender, EventArgs e)
    {
        string strClickedItem = ((LinkButton)sender).Text;
        XmlDocument xdoc = new XmlDocument();

        xdoc.LoadXml(Session["XmlStream"].ToString());

        string xPathExpression = "//PageElement[@name ='" + strClickedItem + "']";
        XmlNodeList xNode = xdoc.SelectNodes(xPathExpression);

        if (trvHandler != null)
        {
            trvHandler(xNode);
        }

    }
}