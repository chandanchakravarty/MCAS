/******************************************************************************************
<Author					: -   Agniswar Das
<Start Date				: -	  17 Nov 2011  
<End Date				: -	
<Description			: -  Underwriter Question screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
*******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;

using System.Data;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.Model.Policy;
using System.Text;

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Xml;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace Cms.Policies.Aspx
{
    public partial class AddPolicyUWQuestion : Cms.Policies.policiesbase
    {
        DataSet dsUWQuestion = null;
        int rowCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //DropDownList ddltemp = new DropDownList();
            //foreach (Control c in this.Controls)
            //{
            //    if (c is DropDownList)
            //    {
            //        ddltemp = (DropDownList)c;
            //        ddltemp.Items.Add("Yes");
            //        ddltemp.Items.Add("No");
            //    }
            //}

            BindGrid();

        }

        private void BindGrid()
        {
            ClsPolicyUWQuestion objCoverages = new ClsPolicyUWQuestion();

            dsUWQuestion = objCoverages.GetUWQuestions(int.Parse(GetLOBID()));

            DataView dvPolicyUWQuestions = new DataView(dsUWQuestion.Tables[0]);
            rowCount = dvPolicyUWQuestions.Count;

            string riskFilter = null;
            riskFilter = "QUES_LEVEL = 'PL'";

            dvPolicyUWQuestions.RowFilter = riskFilter;

            this.dgCommercialUW.DataSource = dvPolicyUWQuestions;
            dgCommercialUW.DataBind();


            riskFilter = "QUES_LEVEL = 'RL'";

            dvPolicyUWQuestions.RowFilter = riskFilter;

            this.dgBOPUW.DataSource = dvPolicyUWQuestions;
            dgBOPUW.DataBind();


        }

        private void OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            e.Item.Attributes.Add("Class", "midcolora");
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblPARENT_QUES_ID = (Label)e.Item.FindControl("lblPARENT_QUES_ID");
                //Label lblPARENT_ID = (Label)e.Item.FindControl("lblPARENT_ID");
                Label lblPARENT_QUES_DESC = (Label)e.Item.FindControl("lblPARENT_QUES_DESC");

                Label lblQUES_LEVEL = (Label)e.Item.FindControl("lblQUES_LEVEL");

                DropDownList cmbYESNO = (DropDownList)e.Item.FindControl("cmbYESNO");
                //DropDownList cmbCHILD_QUES_DESC = (DropDownList)e.Item.FindControl("cmbCHILD_QUES_DESC");
                
                Label lblCHILD_QUES_ID = (Label)e.Item.FindControl("lblCHILD_QUES_ID");
                Label lblCHILD_ID = (Label)e.Item.FindControl("lblCHILD_ID");
                Label lblCHILD_QUES_DESC = (Label)e.Item.FindControl("lblCHILD_QUES_DESC");
                Label lblCHILD_QUES_TYPE = (Label)e.Item.FindControl("lblCHILD_QUES_TYPE");
                Label lblCHILD_QUES_DESC2 = (Label)e.Item.FindControl("lblCHILD_QUES_DESC2");

                //TextBox txtCHILD_QUES_DESC = (TextBox)e.Item.FindControl("txtCHILD_QUES_DESC");

                //CheckBox chkCHILD_QUES_DESC = (CheckBox)e.Item.FindControl("chkCHILD_QUES_DESC");

                HtmlInputHidden hidPARENT_QUES_ID = (HtmlInputHidden)e.Item.FindControl("hidPARENT_QUES_ID");
                HtmlInputHidden hidPARENT_ID = (HtmlInputHidden)e.Item.FindControl("hidPARENT_ID");
                HtmlInputHidden hidCHILD_QUES_ID = (HtmlInputHidden)e.Item.FindControl("hidCHILD_QUES_ID");
                HtmlInputHidden hidCHILD_ID = (HtmlInputHidden)e.Item.FindControl("hidCHILD_ID");
                HtmlInputHidden hidCHILD_QUES_TYPE = (HtmlInputHidden)e.Item.FindControl("hidCHILD_QUES_TYPE");

                HtmlInputHidden hidQUES_LEVEL = (HtmlInputHidden)e.Item.FindControl("hidQUES_LEVEL");

                cmbYESNO.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
                cmbYESNO.DataTextField = "LookupDesc";
                cmbYESNO.DataValueField = "LookupID";
                cmbYESNO.DataBind();
                cmbYESNO.Items.Insert(0, "");

                string strQUES_ANS = lblCHILD_QUES_TYPE.Text;

                string[] arrQATYPE = strQUES_ANS.Split('^');

                int itemCount = arrQATYPE.Length;

                if (itemCount > 0)
                {
                    string strControlType = arrQATYPE[0].ToString();

                    switch (strControlType)
                    {
                        case "DDL":
                            lblCHILD_QUES_DESC.Visible = true;
                            lblCHILD_QUES_DESC2.Visible = true;
                            
                            if (arrQATYPE.Length > 2)
                            {
                                lblCHILD_QUES_DESC.Text = arrQATYPE[1].ToString();

                                DropDownList ddlList = new DropDownList();
                                
                                for (int hr = 1; hr < 13; hr++)
                                {
                                    for (int mn = 0; mn < 60; mn = mn + 15)
                                    {
                                        string min = mn.ToString() == "0" ? "00" : mn.ToString();
                                        string strTime = hr.ToString() + ":" + min + "am";
                                        ddlList.Items.Add(strTime);
                                    }
                                }
                                for (int hr = 1; hr < 13; hr++)
                                {
                                    for (int mn = 0; mn < 60; mn = mn + 15)
                                    {
                                        string min = mn.ToString() == "0" ? "00" : mn.ToString();
                                        string strTime = hr.ToString() + ":" + min + "pm";
                                        ddlList.Items.Add(strTime);
                                    }
                                }
                                lblCHILD_QUES_DESC.Parent.Controls.Add(ddlList);

                                lblCHILD_QUES_DESC2.Text = arrQATYPE[2].ToString();

                                DropDownList ddlList1 = new DropDownList();

                                for (int hr = 1; hr < 13; hr++)
                                {
                                    for (int mn = 0; mn < 60; mn = mn + 15)
                                    {
                                        string min = mn.ToString() == "0" ? "00" : mn.ToString();
                                        string strTime = hr.ToString() + ":" + min + "am";
                                        ddlList1.Items.Add(strTime);
                                    }
                                }
                                for (int hr = 1; hr < 13; hr++)
                                {
                                    for (int mn = 0; mn < 60; mn = mn + 15)
                                    {
                                        string min = mn.ToString() == "0" ? "00" : mn.ToString();
                                        string strTime = hr.ToString() + ":" + min + "pm";
                                        ddlList1.Items.Add(strTime);
                                    }
                                }
                                lblCHILD_QUES_DESC2.Parent.Controls.Add(ddlList1);

                            }
                            else
                            {
                                lblCHILD_QUES_DESC.Text = arrQATYPE[1].ToString();

                                DropDownList ddlList = new DropDownList();
                                lblCHILD_QUES_DESC.Parent.Controls.Add(ddlList);
                            }
                            //txtCHILD_QUES_DESC.Visible = false;
                            //chkCHILD_QUES_DESC.Visible = false;
                            //cmbCHILD_QUES_DESC.Visible = true;
                            break;
                        case "TXT":
                            lblCHILD_QUES_DESC.Visible = true;
                            lblCHILD_QUES_DESC2.Visible = true;
                            if (arrQATYPE.Length > 2)
                            {                                
                                lblCHILD_QUES_DESC.Text = arrQATYPE[1].ToString();

                                TextBox textBox = new TextBox();
                                lblCHILD_QUES_DESC.Parent.Controls.Add(textBox);

                                lblCHILD_QUES_DESC2.Text = arrQATYPE[2].ToString();

                                TextBox textBox1 = new TextBox();
                                lblCHILD_QUES_DESC2.Parent.Controls.Add(textBox1);
                               
                            }
                            else
                            {
                                lblCHILD_QUES_DESC.Text = arrQATYPE[1].ToString();

                                TextBox textBox = new TextBox();
                                lblCHILD_QUES_DESC.Parent.Controls.Add(textBox);
                            }

                            //txtCHILD_QUES_DESC.Visible = true;
                            //chkCHILD_QUES_DESC.Visible = false;
                            //cmbCHILD_QUES_DESC.Visible = false;
                            break;
                        case "CHK":
                            lblCHILD_QUES_DESC.Visible = false;
                            lblCHILD_QUES_DESC2.Visible = false;
                            //txtCHILD_QUES_DESC.Visible = false;
                            //chkCHILD_QUES_DESC.Visible = false;
                            //cmbCHILD_QUES_DESC.Visible = false;
                            for (int i = 1; i < arrQATYPE.Length; i++)
                            {
                                CheckBox chkbox = new CheckBox();
                                chkbox.Text = arrQATYPE[i].ToString();
                                lblCHILD_QUES_DESC.Parent.Controls.Add(chkbox);
                            }
                            break;
                        default:
                            lblCHILD_QUES_DESC.Visible = false;
                            lblCHILD_QUES_DESC2.Visible = false;
                            //txtCHILD_QUES_DESC.Visible = false;
                            //chkCHILD_QUES_DESC.Visible = false;
                            //cmbCHILD_QUES_DESC.Visible = false;
                            //CheckBox chkbox = new CheckBox();
                            //chkbox.Text = "Inside the Grid";
                            //lblCHILD_QUES_DESC.Parent.Controls.Add(chkbox);
                            //dgBOPUW.Controls.Add(chkbox);
                            //dgCommercialUW
                            break;
                    }
                }

                //Label label = new Label();
                //label.Text = "Inside the Grid";
                //dgCommercialUW.Controls.Add(label);

                //TextBox textbox = new TextBox();
                //textbox.Text = "Inside the Grid";
                //dgCommercialUW.Controls.Add(textbox);

                

            }
        }


        private void dgCommercialUW_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            OnItemDataBound(sender, e);

        }

        private void dgBOPUW_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            OnItemDataBound(sender, e);

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.dgCommercialUW.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCommercialUW_ItemDataBound);
            this.dgBOPUW.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgBOPUW_ItemDataBound);

            //this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }
        #endregion
    }
}