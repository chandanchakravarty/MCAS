using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Cms.BusinessLayer.BlCommon;

namespace Cms.XmlManager.Aspx
{
    public partial class RatingXML : Cms.CmsWeb.cmsbase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            trControlForRatingXml.trvHandler += new webcontrols.TreeControlForRatingXml.LinkClicked(TreeControlCustomized_trvHandler);
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
                Repeater rpt1 = (Repeater)e.Item.FindControl("rpt");
                XmlNode dv = (XmlNode)e.Item.DataItem;
                List<NODE> lst = new List<NODE>();
                for (int i = 0; i < dv.Attributes.Count; i++)
                {
                    NODE nd = new NODE();
                    nd.Name = dv.Attributes[i].Name;
                    nd.Value = dv.Attributes[i].Value;
                    lst.Add(nd);
                }
                lblName.Text = dv.ParentNode.Attributes["ID"].Value;
                rpt1.DataSource = lst;
                rpt1.DataBind();
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                int iCount = 0;
                XmlDocument xdom = new XmlDocument();
                xdom.LoadXml(Session["XmlStream"].ToString());
                string xPathExpression = "//PRODUCT/FACTOR/NODE[@ID ='" + lblName.Text + "']/ATTRIBUTES";
                XmlNodeList xNode = xdom.SelectNodes(xPathExpression);
                foreach (RepeaterItem rptItem in rpt.Items)
                {
                    Repeater rptSub = (Repeater)rptItem.FindControl("rpt");
                    foreach (RepeaterItem rptSubItems in rptSub.Items)
                    {
                        Label lblNames = (Label)rptSubItems.FindControl("lblName");
                        TextBox txtName = (TextBox)rptSubItems.FindControl("txtName");
                        xNode[iCount].Attributes[lblNames.Text].Value = txtName.Text;
                    }
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