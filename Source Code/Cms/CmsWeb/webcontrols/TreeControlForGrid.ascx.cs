using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using Cms.BusinessLayer.BlCommon;

public partial class TreeControlForGrid : System.Web.UI.UserControl
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
        /*
        if (!IsPostBack)
        {

            byte[] byteArray = Encoding.ASCII.GetBytes(Session["XmlStream"].ToString());

            MemoryStream stream = new MemoryStream(byteArray);

            ds.ReadXml(stream);

            Session["dataset"] = ds;

            for (int iRow = 0; iRow < ds.Tables.Count; iRow++)
            {

                PRODUCT cls = new PRODUCT();
                if (ds.Tables[iRow].TableName == "value")
                {
                    for (int iCol = 0; iCol < ds.Tables["value"].Rows.Count; iCol++)
                    {

                        NODE node = new NODE();
                        node.Name = "Value";
                        node.Value = ds.Tables["value"].Rows[iCol][0].ToString();
                        for (int i = 2; i < ds.Tables["value"].Columns.Count; i++)
                        {
                            if (ds.Tables["value"].Rows[iCol][i].ToString().Trim() != "")
                            {
                                node.Parent = ds.Tables["value"].Columns[i].ColumnName;
                            }
                        }
                        lstN.Add(node);
                    }
                }
                else
                {
                    if (ds.Tables[iRow].Rows[0][0].ToString() != "0")
                    {
                        NODE node = new NODE();
                        node.Name = "Value";
                        node.Value = ds.Tables[iRow].Rows[0][0].ToString();
                        node.Parent = ds.Tables[iRow].TableName;
                        lstN.Add(node);
                    }
                    cls.Name = ds.Tables[iRow].TableName;
                    lstP.Add(cls);
                }
            }


            ddlElement.DataTextField = "Name";
            ddlElement.DataValueField = "Name";
            ddlElement.DataSource = lstP;
            ddlElement.DataBind();
            ddlElement.Items.Add(new ListItem("All"));
        }*/
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item is RepeaterItem)
        {
            Repeater rpt1 = (Repeater)e.Item.FindControl("rpt1");
            PRODUCT dv = (PRODUCT)e.Item.DataItem;
            rpt1.DataSource = lstN.Where(t => t.Parent.Contains(dv.Name.ToString())).Select(t => t).ToList(); ;
            rpt1.DataBind();
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        /*byte[] byteArray = Encoding.ASCII.GetBytes(Session["XmlStream"].ToString());

        MemoryStream stream = new MemoryStream(byteArray);

        ds.ReadXml(stream);

        Session["dataset"] = ds;

        for (int iRow = 0; iRow < ds.Tables.Count; iRow++)
        {

            PRODUCT cls = new PRODUCT();
            if (ds.Tables[iRow].TableName == "value")
            {
                for (int iCol = 0; iCol < ds.Tables["value"].Rows.Count; iCol++)
                {

                    NODE node = new NODE();
                    node.Name = "Value";
                    node.Value = ds.Tables["value"].Rows[iCol][0].ToString();
                    for (int i = 2; i < ds.Tables["value"].Columns.Count; i++)
                    {
                        if (ds.Tables["value"].Rows[iCol][i].ToString().Trim() != "")
                        {
                            node.Parent = ds.Tables["value"].Columns[i].ColumnName;
                        }
                    }
                    lstN.Add(node);
                }
            }
            else
            {
                if (ds.Tables[iRow].Rows[0][0].ToString() != "0")
                {
                    NODE node = new NODE();
                    node.Name = "Value";
                    node.Value = ds.Tables[iRow].Rows[0][0].ToString();
                    node.Parent = ds.Tables[iRow].TableName;
                    lstN.Add(node);
                }
                cls.Name = ds.Tables[iRow].TableName;
                lstP.Add(cls);
            }
        }
        if (ddlElement.Text != "All")
        {
            rpt.DataSource = lstP.Where(t => t.Name.Equals(ddlElement.Text)).Select(t => t).ToList();
            rpt.DataBind();
        }
        else
        {
            rpt.DataSource = lstP;
            rpt.DataBind();
        }*/
    }
    protected void lnkValue_Click(object sender, EventArgs e)
    {
        string strClickedItem = ((LinkButton)sender).Text;
        XmlDocument xdoc = new XmlDocument();

        xdoc.LoadXml(Session["XmlStream"].ToString());

        string xPathExpression = "//" + strClickedItem + "/value";
        XmlNodeList xNode = xdoc.SelectNodes(xPathExpression);

        if (trvHandler != null)
        {
            trvHandler(xNode);
        }

    }
}