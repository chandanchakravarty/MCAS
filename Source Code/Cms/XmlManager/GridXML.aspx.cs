using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class GridXML : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TreeControlForGrid1.trvHandler += new TreeControlForGrid.LinkClicked(TreeControlCustomized1_trvHandler);
    }

    private void TreeControlCustomized1_trvHandler(XmlNodeList strValue)
    {
        //rpt.DataSource = strValue;
        //rpt.DataBind();
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        //if (e.Item is RepeaterItem)
        //{
        //    TextBox txtName = (TextBox)e.Item.FindControl("txtName");
        //    XmlNode dv = (XmlNode)e.Item.DataItem;
        //    txtName.Text = (dv).InnerXml;
        //    lblName.Text = dv.ParentNode.Name;
        //}

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    int iCount = 0;
        //    XmlDocument xdom = new XmlDocument();
        //    xdom.LoadXml(Session["XmlStream"].ToString());
        //    string xPathExpression = "//" + lblName.Text + "/value";
        //    XmlNodeList xNode = xdom.SelectNodes(xPathExpression);
        //    foreach (RepeaterItem rptItem in rpt.Items)
        //    {
        //        TextBox txtName = (TextBox)rptItem.FindControl("txtName");
        //        xNode[iCount].InnerText = txtName.Text;
        //        iCount++;
        //    }
        //    Session["XmlStream"] = xdom.OuterXml;
        //}
        //catch (Exception ex)
        //{

        //}

    }
}