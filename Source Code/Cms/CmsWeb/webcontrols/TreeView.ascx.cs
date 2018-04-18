using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Cms.CmsWeb.WebControls
{
    public partial class TreeView : System.Web.UI.UserControl
    {
        XmlDocument xdoc = new XmlDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            Cms.CmsWeb.cmsbase objBase = new cmsbase();
            string xmlFilepath = objBase.GetTreeLayoutXML();//@"D:\Projects\EAWBR\Development\Source Code\Cms\CmsWeb\webcontrols\TreeViewData.xml";
            xdoc.LoadXml(xmlFilepath);
            XmlDataSource.Data = xdoc.InnerXml;
           
            //TreeView1.Attributes.Add("onclick", "return OnTreeClick(event)");
            //TreeView1.Attributes(""
        }

        protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
        {
            //String NativeUrl = string.Empty;
            string imgpath = "/cms/cmsweb/images1/plussign.png";
            string Navpath = "";
            String value = string.Empty;
            //e.Node.Text = e.Node.Text + "&nbsp;<img src="+"/cms/cmsweb/images1/add_new.png" + " />";
            //NativeUrl = e.Node.NavigateUrl; /cms/cmsweb/Construction.html
            string strText = string.Empty;
            strText = e.Node.Text;
            value = e.Node.Value;
            //Value = NativeUrl;
            if (e.Node.Text == "Location/Premises")
            {
                //Navpath = "/cms/Policies/Aspx/AddPolicyLocations.aspx";
                Navpath = e.Node.NavigateUrl + "?CALLED_FROM=ABCD&Path=" + value;
                //e.Node.Text = e.Node.Text + "&nbsp;<a href=" + Navpath + "><img src=" + imgpath + " /></a>";
                e.Node.Text = e.Node.Text + "&nbsp;<a href=" + Navpath + "><img src=" + imgpath + " border=0 /></a>";
                e.Node.NavigateUrl = "";
            }

            else if (e.Node.Text == "Additional Interest")
            {
                //Navpath = "/cms/Policies/Aspx/AddPolicyLocations.aspx";
                Navpath = e.Node.NavigateUrl + "?CALLED_FROM=ABCD&Path=" + value;
                //e.Node.Text = e.Node.Text + "&nbsp;<a href=" + Navpath + "><img src=" + imgpath + " /></a>";
                e.Node.Text = e.Node.Text + "&nbsp;<a href=" + Navpath + "><img src=" + imgpath + " border=0 /></a>";
                e.Node.NavigateUrl = "";
            }                //Comment by kuldeep
            //else if (e.Node.Text == "Liability Coverages")
            //{
            //    e.Node.NavigateUrl = e.Node.NavigateUrl + "?CALLED_FROM=ABCD&Path=" + value + "COV_TYPE=PL";
            //}
            //else if (e.Node.Text == "Property Coverages")
            //{
            //    e.Node.NavigateUrl = e.Node.NavigateUrl + "?CALLED_FROM=ABCD&Path=" + value + "COV_TYPE=RL";
            //}
            else
            {
                e.Node.NavigateUrl = e.Node.NavigateUrl + "?CALLED_FROM=ABCD&Path=" + value;
            }
            
            
        }

       
    }
}
