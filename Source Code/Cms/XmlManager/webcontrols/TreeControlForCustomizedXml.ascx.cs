
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;
using System.Text;
using Cms.BusinessLayer.BlCommon;

namespace Cms.webcontrols
{

    public partial class TreeControlForCustomizedXml : System.Web.UI.UserControl
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
            if (!IsPostBack)
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Session["XmlStream"].ToString());

                MemoryStream stream = new MemoryStream(byteArray);

                ds.ReadXml(stream);

                Session["dataset"] = ds;

                for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                {

                    PRODUCT cls = new PRODUCT();
                    cls.Name = ds.Tables[0].Columns[0].ColumnName;
                    cls.Value = ds.Tables[0].Rows[iRow]["Code"].ToString();
                    cls.Parent = ds.Tables[0].Rows[iRow]["Culture_Id"].ToString();
                    lstP.Add(cls);

                }

                for (int iRow = 0; iRow < ds.Tables[1].Rows.Count; iRow++)
                {

                    FACTOR cls = new FACTOR();
                    cls.Name = ds.Tables[1].Columns[0].ColumnName;
                    cls.Value = ds.Tables[1].Rows[iRow]["screenid"].ToString();
                    cls.ID = ds.Tables[1].Rows[iRow]["screen_Id"].ToString();
                    cls.Parent = ds.Tables[1].Rows[iRow]["Culture_Id"].ToString();
                    lstF.Add(cls);

                }


                ddlScreen.DataTextField = "Value";
                ddlScreen.DataValueField = "Value";
                ddlScreen.DataSource = lstF;
                ddlScreen.DataBind();
            }
        }
        protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item is RepeaterItem)
            {
                Repeater rpt1 = (Repeater)e.Item.FindControl("rpt1");
                PRODUCT dv = (PRODUCT)e.Item.DataItem;
                rpt1.DataSource = lstF.Where(t => t.Parent.Equals(dv.Parent.ToString()) && t.Value.Equals(ddlScreen.Text)).Select(t => t).ToList(); ;
                rpt1.DataBind();
            }
        }
        protected void rpt1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item is RepeaterItem)
            {
                Repeater rpt2 = (Repeater)e.Item.FindControl("rpt2");
                FACTOR dv = (FACTOR)e.Item.DataItem;
                rpt2.DataSource = lstN.Where(t => t.Parent.Equals(dv.ID.ToString())).Select(t => t).ToList(); ;
                rpt2.DataBind();
            }
        }
        protected void rpt2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            ds = (DataSet)Session["dataset"];
            //ds.ReadXml(Server.MapPath("CustomizedMessages.xml"));

            for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
            {

                PRODUCT cls = new PRODUCT();
                cls.Name = ds.Tables[0].Columns[0].ColumnName;
                cls.Value = ds.Tables[0].Rows[iRow]["Code"].ToString();
                cls.Parent = ds.Tables[0].Rows[iRow]["Culture_Id"].ToString();
                lstP.Add(cls);

            }

            for (int iRow = 0; iRow < ds.Tables[1].Rows.Count; iRow++)
            {

                FACTOR cls = new FACTOR();
                cls.Name = ds.Tables[1].Columns[0].ColumnName;
                cls.Value = ds.Tables[1].Rows[iRow]["screenid"].ToString();
                cls.ID = ds.Tables[1].Rows[iRow]["screen_Id"].ToString();
                cls.Parent = ds.Tables[1].Rows[iRow]["Culture_Id"].ToString();
                lstF.Add(cls);

            }

            for (int iRow = 0; iRow < ds.Tables[2].Rows.Count; iRow++)
            {

                NODE cls = new NODE();
                cls.Name = ds.Tables[2].Columns[0].ColumnName;
                cls.Value = ds.Tables[2].Rows[iRow]["message_Text"].ToString();
                cls.ID = ds.Tables[2].Rows[iRow]["messageid"].ToString();
                cls.Parent = ds.Tables[2].Rows[iRow]["screen_Id"].ToString();
                lstN.Add(cls);

            }

            for (int iRow = 0; iRow < ds.Tables[3].Rows.Count; iRow++)
            {

                NODE cls = new NODE();
                cls.Name = ds.Tables[3].Columns[0].ColumnName;
                cls.Value = ds.Tables[3].Rows[iRow]["tab_Text"].ToString();
                cls.ID = ds.Tables[3].Rows[iRow]["tabid"].ToString();
                cls.Parent = ds.Tables[3].Rows[iRow]["screen_Id"].ToString();
                lstN.Add(cls);

            }
            if (ddlCulture.Text != "All")
            {
                rpt.DataSource = lstP.Where(t => t.Value.Equals(ddlCulture.Text)).Select(t => t).ToList();
                rpt.DataBind();
            }
            else
            {
                rpt.DataSource = lstP;
                rpt.DataBind();
            }
        }
        public void lnkValue_Click(object sender, EventArgs e)
        {
            string strClickedItem = ((LinkButton)sender).Text;
            XmlDocument xdoc = new XmlDocument();

            xdoc.LoadXml(Session["XmlStream"].ToString());

            string xPathExpression = "//Culture/screen[@screenid ='" + strClickedItem + "']/message";
            XmlNodeList xNode = xdoc.SelectNodes(xPathExpression);

            if (trvHandler != null)
            {
                trvHandler(xNode);
            }

        }
    }
}