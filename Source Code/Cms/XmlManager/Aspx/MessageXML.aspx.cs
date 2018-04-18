using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Cms.XmlManager.Aspx
{
    public partial class MessageXML : Cms.CmsWeb.cmsbase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            trControlForCustomizedXml.trvHandler += new webcontrols.TreeControlForCustomizedXml.LinkClicked(TreeControlCustomized_trvHandler);
        }
        private void TreeControlCustomized_trvHandler(XmlNodeList strValue)
        {
            rpt.DataSource = strValue;
            rpt.DataBind();
        }
        protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item is RepeaterItem)
            {
                TextBox txtName = (TextBox)e.Item.FindControl("txtName");
                TextBox txtValue = (TextBox)e.Item.FindControl("txtValue");
                XmlNode dv = (XmlNode)e.Item.DataItem;
                txtName.Text = (dv).Attributes[0].Value;
                txtValue.Text = (dv).InnerText;
                lblName.Text = dv.ParentNode.Attributes["screenid"].Value;
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int iCount = 0;
                XmlDocument xdom = new XmlDocument();
                xdom.LoadXml(Session["XmlStream"].ToString());
                string xPathExpression = "//Culture/screen[@screenid ='" + lblName.Text + "']/message";
                XmlNodeList xNode = xdom.SelectNodes(xPathExpression);
                foreach (RepeaterItem rptItem in rpt.Items)
                {
                    TextBox txtName = (TextBox)rptItem.FindControl("txtName");
                    TextBox txtValue = (TextBox)rptItem.FindControl("txtValue");
                    xNode[iCount].InnerText = txtValue.Text;
                    iCount++;
                }
                Session["XmlStream"] = xdom.OuterXml;
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("Default.aspx", true);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Response.Redirect("PreviewXML.aspx", true);
        }
    }
}