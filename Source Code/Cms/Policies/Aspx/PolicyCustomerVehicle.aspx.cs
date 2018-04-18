/******************************************************************************************
<Author				: -		Swastika Gaur
<Start Date			: -		24-03-2006
<End Date			: -	
<Description		: - 	Display Policy Customer vehicles 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
*******************************************************************************************/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;

namespace Policies.Aspx
{
    /// <summary>
    /// Summary description for PolicyCustomerVehicle.
    /// </summary>
    public class PolicyCustomerVehicle : Cms.Policies.policiesbase
    {
        protected System.Web.UI.WebControls.CheckBox chkCopyCoverage;
        protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
        protected System.Web.UI.WebControls.DataGrid dgrCustVehicle;
        protected System.Web.UI.HtmlControls.HtmlTableRow trCOVERAGES;

        private int intFromUserId;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected int gIntSaved = 0;
        private static string CALLED_FROM_UMBRELLA = "UMB";
        protected Cms.CmsWeb.Controls.CmsButton btnClose;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSavedStatus;
        protected string strTitle = "";
        public string strCapInsuredVehicle = "Insured Vehicle Number";
        protected System.Web.UI.WebControls.Label lblHeader;
        private string strCalledFrom = "";
        private System.Resources.ResourceManager objResourceMgr;
        private void Page_Load(object sender, System.EventArgs e)
        {
            #region Setting screen id
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
            {
                strCalledFrom = Request.QueryString["CalledFrom"];
            }
            switch (strCalledFrom)
            {
                case "ppa":
                case "PPA":
                    base.ScreenId = "44_5";
                    break;
                case "mot":
                case "MOT":
                    base.ScreenId = "48_5";
                    break;
                case "umb":
                case "UMB":
                    base.ScreenId = "81_5";
                    break;
            }
            #endregion
            	objResourceMgr = new System.Resources.ResourceManager("Policies.Aspx.PolicyCustomerVehicle" ,System.Reflection.Assembly.GetExecutingAssembly());
            btnClose.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnClose.PermissionString = gstrSecurityXML;


            btnSubmit.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSubmit.PermissionString = gstrSecurityXML;

            intFromUserId = int.Parse(GetUserId());
            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
            {
                hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
            }
            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
            {
                hidPolID.Value = Request.QueryString["POLICY_ID"].ToString();
            }
            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
            {
                hidPolVersionID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
            }
            if (Request.QueryString["CalledFrom"] != null)
            {
                hidCalledFrom.Value = Request.QueryString["CalledFrom"].ToString();
            }

            if (hidCalledFrom.Value == "PPA")
            {
                lblHeader.Text = "Copy Vehicle";
                strTitle = "Copy Vehicle";
            }
            else if (hidCalledFrom.Value == "MOT")
            {
                lblHeader.Text = "Copy Motorcycle";
                strTitle = "Copy Motorcycle";
            }
            else
            {
                lblHeader.Text = "Copy Vehicle";
                strTitle = "Copy Vehicle";

            }

            if (!IsPostBack)
            {

                SetGridHeaderText();
                if (int.Parse(hidCustomerID.Value) != 0 && int.Parse(hidPolID.Value) != 0 && int.Parse(hidPolVersionID.Value) != 0)
                {
                    try
                    {
                        DataTable dtCustVehicle = new DataTable();

                        // Calling static function for retriving data from the Pol_Vehicles table for the customer.
                        if (hidCalledFrom != null && hidCalledFrom.Value.Trim().ToString().ToUpper() != CALLED_FROM_UMBRELLA)
                        {
                            dtCustVehicle = ClsVehicleInformation.FetchVehicleFromCustVehicleTablePolicy(int.Parse(hidCustomerID.Value), int.Parse(hidPolID.Value), int.Parse(hidPolVersionID.Value));
                            DataTable dtappstatus = new DataTable();
                            ClsCommon objCommon = new ClsCommon();
                            dtappstatus = objCommon.GetPolicyStatusInfo(int.Parse(hidCustomerID.Value), int.Parse(hidPolID.Value), int.Parse(hidPolVersionID.Value));
                            if (dtappstatus.Rows[0]["APP_STATUS"].ToString().ToUpper() == "APPLICATION")
                            {
                                dgrCustVehicle.Columns[3].HeaderText = objResourceMgr.GetString("lblAPP_NUMBER");
                                dgrCustVehicle.Columns[4].HeaderText = objResourceMgr.GetString("lblAPP_VERSION_ID");
                            }
                            else
                            {
                                dgrCustVehicle.Columns[3].HeaderText = objResourceMgr.GetString("lblPOL_NUMBER");
                                dgrCustVehicle.Columns[4].HeaderText = objResourceMgr.GetString("lblPOL_VERSION_ID");
                            }

                        }
                        else if (hidCalledFrom.Value.ToString().ToUpper().Trim() == CALLED_FROM_UMBRELLA)
                        {
                            trCOVERAGES.Visible = false;
                            dtCustVehicle = ClsVehicleInformation.FetchVehicleFromUmbVehicleTablePolicy(int.Parse(hidCustomerID.Value), int.Parse(hidPolID.Value), int.Parse(hidPolVersionID.Value));
                        }
                        if (dtCustVehicle.Rows.Count > 0)
                        {
                            // Populating data into the datagrid for display.
                            dgrCustVehicle.DataSource = dtCustVehicle.DefaultView;
                            dgrCustVehicle.DataBind();
                        }
                        else
                        {
                            lblMessage.Text = "No vehicle available for copy.";
                            lblMessage.Visible = true;
                        }
                        if (dtCustVehicle.Rows.Count > 0)
                        {
                            btnSubmit.Enabled = true;
                        }
                        else
                        {
                            btnSubmit.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                        lblMessage.Visible = true;
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    }
                }
            }
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
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        // start
        public void SetGridHeaderText()
        {

            if (Request.QueryString["CALLEDFROM"].ToString() == "MOT")
            {
                dgrCustVehicle.Columns[6].HeaderText = "Insured Motorcycle Number";
            }
            else if (Request.QueryString["CALLEDFROM"].ToString() == "UMB")
            {
                dgrCustVehicle.Columns[6].HeaderText = "Insured Vehicle Number";
            }
            else if (Request.QueryString["CALLEDFROM"].ToString() == "VEH")
            {
                dgrCustVehicle.Columns[6].HeaderText = "Insured Vehicle Number";
            }
            else
            {
                dgrCustVehicle.Columns[6].HeaderText = "Insured Vehicle Number";
            }
            
        }

        // end 

        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            CheckBox chkBox;
            DataTable dt = new DataTable();
            dt.Columns.Add("VehicleID", typeof(int));
            dt.Columns.Add("PolID", typeof(int));
            dt.Columns.Add("PolVersionID", typeof(int));
            dt.Columns.Add("Make", typeof(string));
            dt.Columns.Add("Model", typeof(string));
            dt.Columns.Add("INSURED_VEH_NUMBER", typeof(int));


            Cms.Model.Policy.ClsPolicyVehicleInfo objVehicleInfo = new Cms.Model.Policy.ClsPolicyVehicleInfo();
            ClsVehicleInformation objVehicleInformation = new ClsVehicleInformation();
            objVehicleInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
            objVehicleInfo.POLICY_VERSION_ID = int.Parse(hidPolVersionID.Value);
            objVehicleInfo.POLICY_ID = int.Parse(hidPolID.Value);
            objVehicleInfo.CREATED_BY = int.Parse(GetUserId());
            try
            {
                // Code for finding the checked vehicle in the datagrid.
                foreach (DataGridItem dgi in dgrCustVehicle.Items)
                {
                    chkBox = (CheckBox)dgi.FindControl("chkSelect");
                    if (chkBox != null && chkBox.Checked)
                    {
                        DataRow dr = dt.NewRow();
                        dr["VehicleID"] = int.Parse(dgrCustVehicle.DataKeys[dgi.ItemIndex].ToString());
                        dr["PolID"] = int.Parse(dgi.Cells[1].Text);
                        dr["PolVersionID"] = int.Parse(dgi.Cells[2].Text);
                        dr["Make"] = dgi.Cells[8].Text;
                        dr["Model"] = dgi.Cells[9].Text;
                        dr["INSURED_VEH_NUMBER"] = int.Parse(dgi.Cells[6].Text);

                        dt.Rows.Add(dr);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(hidCustomerID.Value) != 0 && int.Parse(hidPolID.Value) != 0 && int.Parse(hidPolVersionID.Value) != 0)
                    {
                        // Check whether coverage data also to be copied.
                        if (chkCopyCoverage.Checked)
                        {
                            // Calling static method for coping data in app_vehicles table 
                            // and in app_vehicle_coverages 
                            // 'Y' is passed as the last parameter to be checked in stored procedure for coping coverages data.
                            if (hidCalledFrom != null && hidCalledFrom.Value.Trim().ToString().ToUpper() != CALLED_FROM_UMBRELLA)
                            {
                                objVehicleInformation.InsertVehicleFromCustVehicleTablePolicy(objVehicleInfo, dt, 'Y', hidCalledFrom.Value.ToUpper());
                                base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 9 April 09
                            }
                            else if (hidCalledFrom.Value.ToString().ToUpper().Trim() == CALLED_FROM_UMBRELLA)
                            {
                                //ClsVehicleInformation.InsertVehicleFromCustVehicleToUmbrellaVehicle(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolID.Value),int.Parse(hidPolVersionID.Value),intFromUserId,'Y');
                                objVehicleInformation.InsertVehicleFromUmbVehicleTablePolicy(objVehicleInfo, dt, 'Y', hidCalledFrom.Value.ToUpper());
                                base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 9 April 09
                            }

                        }
                        else
                        {
                            // Only for coping vehicles data.
                            if (hidCalledFrom != null && hidCalledFrom.Value.Trim().ToString().ToUpper() != CALLED_FROM_UMBRELLA)
                            {

                                //ClsVehicleInformation.InsertVehicleFromCustVehicleTable(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolID.Value),int.Parse(hidPolVersionID.Value),intFromUserId);

                                objVehicleInformation.InsertVehicleFromCustVehicleTablePolicy(objVehicleInfo, dt, hidCalledFrom.Value.ToUpper());
                                base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 9 April 09
                            }
                            else if (hidCalledFrom.Value.ToString().ToUpper().Trim() == CALLED_FROM_UMBRELLA)
                            {
                                //ClsVehicleInformation.InsertVehicleFromCustVehicleToUmbrellaVehicle(dt,int.Parse(hidCustomerID.Value),int.Parse(hidPolID.Value),int.Parse(hidPolVersionID.Value),intFromUserId,'N');
                                objVehicleInformation.InsertVehicleFromUmbVehicleTablePolicy(objVehicleInfo, dt, 'N', hidCalledFrom.Value.ToUpper());
                                base.OpenEndorsementDetails();//Added for Itrack Issue 5655 on 9 April 09
                            }

                        }
                        lblMessage.Visible = true;
                        hidSavedStatus.Value = "1";
                        gIntSaved = 1;
                        if (hidCalledFrom.Value == "PPA")
                        {
                            lblMessage.Text = "Selected vehicles copied successfully.";
                        }
                        else if (hidCalledFrom.Value == "MOT")
                        {
                            lblMessage.Text = "Selected motorcycles copied successfully.";
                        }
                        else
                        {
                            lblMessage.Text = "Selected vehicles copied successfully.";
                        }



                        string strScript = @"<script>" +
                            //"window.opener.RefreshWebgrid(1,'',true);" +
                            "window.opener.RefreshWebgrid('');" +
                            "window.close();" +
                            "</script>";

                        if (!ClientScript.IsStartupScriptRegistered("Refresh"))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strScript);
                        }

                    }
                    else
                    {
                        // Message is to be displayed that Values passed in Query String can't be blank.					
                    }
                }
                else if (hidCalledFrom.Value == "PPA")
                {
                    //if (chkCopyCoverage.Checked)
                    //{
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please select vehicle.";
                    //}
                }

                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please select Motorcycle.";
                }

            }
            catch (Exception ex)
            {
                gIntSaved = 0;
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
            finally
            {
                //gIntSaved=1;				
            }


        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            string strScript = @"<script>" +
                //"window.opener.Refresh(1,1);" + 
                "window.opener.RefreshWebgrid(1,'',true);" +
                //"window.opener.document.getElementById('lblError').style.display='inline';" + 
                //"window.opener.document.getElementById('lblError').innerText= 'Selected  information saved successfully.';" +
                "window.close();" +
                "</script>"
                ;

            if (!ClientScript.IsStartupScriptRegistered("Refresh"))
            {
                ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strScript);
            }
        }

     

        //protected void dgrCustVehicle_ItemDataBound(object sender, DataGridItemEventArgs e)
        //{
            
        //string val;
        //if ((e.Item.ItemType.ToString() != "Header") && (e.Item.ItemType.ToString() != "Footer"))
        //{
        //    val = e.Item.Cells[11].Text;
        //}
        //}




    }
}
