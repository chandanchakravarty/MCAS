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
    public partial class TreeControlForRatingXml : System.Web.UI.UserControl
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
                    cls.Value = ds.Tables[0].Rows[iRow]["ID"].ToString();
                    cls.Parent = ds.Tables[0].Rows[iRow]["PRODUCT_Id"].ToString();
                    lstP.Add(cls);

                }

                for (int iRow = 0; iRow < ds.Tables[1].Rows.Count; iRow++)
                {

                    FACTOR cls = new FACTOR();
                    cls.Name = ds.Tables[1].Columns[0].ColumnName;
                    cls.Value = ds.Tables[1].Rows[iRow]["ID"].ToString();
                    cls.ID = ds.Tables[1].Rows[iRow]["FACTOR_Id"].ToString();
                    cls.Parent = ds.Tables[1].Rows[iRow]["PRODUCT_Id"].ToString();
                    lstF.Add(cls);

                }

                for (int iRow = 0; iRow < ds.Tables[2].Rows.Count; iRow++)
                {

                    NODE cls = new NODE();
                    cls.Name = ds.Tables[2].Columns[0].ColumnName;
                    cls.Value = ds.Tables[2].Rows[iRow]["ID"].ToString();
                    cls.ID = ds.Tables[2].Rows[iRow]["NODE_Id"].ToString();
                    cls.Parent = ds.Tables[2].Rows[iRow]["FACTOR_Id"].ToString();
                    lstN.Add(cls);

                }

                ddlFactor.DataTextField = "Value";
                ddlFactor.DataValueField = "Value";
                ddlFactor.DataSource = lstF;
                ddlFactor.DataBind();
                ddlFactor.Items.Add(new ListItem("All"));


                ddlNode.DataTextField = "Value";
                ddlNode.DataValueField = "Value";
                ddlNode.DataSource = lstN;
                ddlNode.DataBind();
                ddlNode.Items.Add(new ListItem("All"));
            }
        }
        protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item is RepeaterItem)
            {
                Repeater rpt1 = (Repeater)e.Item.FindControl("rpt1");
                PRODUCT dv = (PRODUCT)e.Item.DataItem;
                if (ddlFactor.Text != "All")
                {
                    rpt1.DataSource = lstF.Where(t => t.Parent.Equals(dv.Parent.ToString()) && t.Value.Equals(ddlFactor.Text)).Select(t => t).ToList(); ;
                    rpt1.DataBind();
                }
                else
                {
                    rpt1.DataSource = lstF.Where(t => t.Parent.Equals(dv.Parent.ToString())).Select(t => t).ToList(); ;
                    rpt1.DataBind();
                }

            }
        }
        protected void rpt1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item is RepeaterItem)
            {
                Repeater rpt2 = (Repeater)e.Item.FindControl("rpt2");
                FACTOR dv = (FACTOR)e.Item.DataItem;
                if (ddlNode.Text != "All")
                {
                    rpt2.DataSource = lstN.Where(t => t.Parent.Equals(dv.ID.ToString()) && t.Value.Equals(ddlNode.Text)).Select(t => t).ToList(); ;
                    rpt2.DataBind();
                }
                else
                {
                    rpt2.DataSource = lstN.Where(t => t.Parent.Equals(dv.ID.ToString())).Select(t => t).ToList(); ;
                    rpt2.DataBind();
                }
            }
        }
        protected void rpt2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item is RepeaterItem)
            {
                Repeater rpt3 = (Repeater)e.Item.FindControl("rpt3");
                NODE dv = (NODE)e.Item.DataItem;
                rpt3.DataSource = lstA.Where(t => t.Parent.Equals(dv.ID.ToString())).Select(t => t).ToList();
                rpt3.DataBind();
            }
        }
        protected void rpt3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item is RepeaterItem)
            {
                LinkButton lnkIndex = (LinkButton)e.Item.FindControl("lnkIndex");
                lnkIndex.Text = e.Item.ItemIndex.ToString();
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(Session["XmlStream"].ToString());

            MemoryStream stream = new MemoryStream(byteArray);

            ds.ReadXml(stream);

            Session["dataset"] = ds;

            for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
            {

                PRODUCT cls = new PRODUCT();
                cls.Name = ds.Tables[0].Columns[0].ColumnName;
                cls.Value = ds.Tables[0].Rows[iRow]["ID"].ToString();
                cls.Parent = ds.Tables[0].Rows[iRow]["PRODUCT_Id"].ToString();
                lstP.Add(cls);

            }

            for (int iRow = 0; iRow < ds.Tables[1].Rows.Count; iRow++)
            {

                FACTOR cls = new FACTOR();
                cls.Name = ds.Tables[1].Columns[0].ColumnName;
                cls.Value = ds.Tables[1].Rows[iRow]["ID"].ToString();
                cls.ID = ds.Tables[1].Rows[iRow]["FACTOR_Id"].ToString();
                cls.Parent = ds.Tables[1].Rows[iRow]["PRODUCT_Id"].ToString();
                lstF.Add(cls);

            }

            for (int iRow = 0; iRow < ds.Tables[2].Rows.Count; iRow++)
            {

                NODE cls = new NODE();
                cls.Name = ds.Tables[2].Columns[0].ColumnName;
                cls.Value = ds.Tables[2].Rows[iRow]["ID"].ToString();
                cls.ID = ds.Tables[2].Rows[iRow]["NODE_Id"].ToString();
                cls.Parent = ds.Tables[2].Rows[iRow]["FACTOR_Id"].ToString();
                lstN.Add(cls);

            }

            for (int iRow = 0; iRow < ds.Tables[3].Rows.Count; iRow++)
            {

                ATTRIBUTE cls = new ATTRIBUTE();
                cls.Name = ds.Tables[3].Columns[0].ColumnName;
                cls.Value = ds.Tables[3].Rows[iRow][0].ToString();
                cls.ID = ds.Tables[3].Rows[iRow][1].ToString();
                cls.Parent = ds.Tables[3].Rows[iRow]["NODE_Id"].ToString();
                lstA.Add(cls);

            }

            rpt.DataSource = lstP;
            rpt.DataBind();

        }
        protected void lnkValue_Click(object sender, EventArgs e)
        {
            string strClickedItem = ((LinkButton)sender).Text;
            XmlDocument xdoc = new XmlDocument();

            xdoc.LoadXml(Session["XmlStream"].ToString());

            string xPathExpression = "//PRODUCT/FACTOR/NODE[@ID ='" + strClickedItem + "']/ATTRIBUTES";
            XmlNodeList xNode = xdoc.SelectNodes(xPathExpression);

            if (trvHandler != null)
            {
                trvHandler(xNode);
            }

        }
    }
}