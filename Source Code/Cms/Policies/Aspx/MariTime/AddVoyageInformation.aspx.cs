using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb;
using Cms.BusinessLayer;
using Cms.BusinessLayer.BlApplication;
using Cms.DataLayer;
using System.Data;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx.MariTime
{
    public partial class AddVoyageInformation:cmsbase
    {
        int intCUSTOMER_ID;
        int intPOLICY_ID;
        int intPOLICY_VERSION_ID;
        int intVOYAGE_INFO_ID;

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {


            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //this.btnActivateDeactivate.Click += new EventHandler(this.btnActivateDeactivate_Click);
            //this.Load += new System.EventHandler(this.Page_Load);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "134_0";

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;
            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            
            if (!Page.IsPostBack)
            {

                //hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
                hidCUSTOMER_ID.Value = GetCustomerID();
                SetCustomerID(hidCUSTOMER_ID.Value);
                intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

                hidPOLICY_ID.Value = GetPolicyID();
                SetPolicyID(hidPOLICY_ID.Value);
                intPOLICY_ID = int.Parse(hidPOLICY_ID.Value);

                hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();                
                SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
                intPOLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

                //btnReset.Attributes.Add("onclick", "javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
                //string strQUOTENUM = Request.Params["QUOTE_NUM"];
                //ClsQuickQuote objQQuote = new ClsQuickQuote();
                //intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQUOTENUM);

                //hlkVoyageFromDate.Attributes.Add("OnClick", "fPopCalendar(document.QQMarineRiskDetails.txtVoyageFromDate,document.QQMarineRiskDetails.txtVoyageFromDate)");
                FillDropdowns();
                


                if (Request.QueryString["VOYAGE_INFO_ID"] != null && Request.QueryString["VOYAGE_INFO_ID"].ToString() != "" && Request.QueryString["VOYAGE_INFO_ID"].ToString() != "NEW")
                {

                    hidVOYAGE_INFO_ID.Value = Request.QueryString["VOYAGE_INFO_ID"].ToString();
                    //LoadData(Convert.ToInt32(Request.QueryString["VOYAGE_INFO_ID"].ToString());
                   // this.GetOldDataObject(Convert.ToInt32(Request.QueryString["MARITIME_ID"].ToString()));
                    intVOYAGE_INFO_ID = int.Parse(hidVOYAGE_INFO_ID.Value);
                    hid_ID.Value = "old";
                    LoadData();

                }

            }
        }

        private void LoadData()
        {

            Cms.BusinessLayer.BlApplication.ClsVoyageInformation objVoyage = new ClsVoyageInformation();
            string strXML;
            DataSet dsVoyage = objVoyage.GetPOL_MARINECARGO_VOYAGE_INFO(int.Parse(hidVOYAGE_INFO_ID.Value));
            if (dsVoyage.Tables.Count > 0)
            {
                DataTable dt = dsVoyage.Tables[0];

                strXML = ClsCommon.GetXMLEncoded(dt);
                hidOldXML.Value = strXML;

                if (dt.Rows.Count != 0)
                {

                    if (dt.Rows[0]["VOYAGE_FROM"] != System.DBNull.Value)
                    {
                        cmbVoyageFrom.SelectedValue = dt.Rows[0]["VOYAGE_FROM"].ToString();
                    }

                    if (dt.Rows[0]["VOYAGE_TO"] != System.DBNull.Value)
                    {
                        cmbVoyageTo.SelectedValue = dt.Rows[0]["VOYAGE_TO"].ToString();
                    }

                    if (dt.Rows[0]["THENCE_TO"] != System.DBNull.Value)
                    {
                        cmbThenceTo.SelectedValue = dt.Rows[0]["THENCE_TO"].ToString();
                    }

                    if (dt.Rows[0]["QUANTITY_DESCRIPTION"] != System.DBNull.Value)
                    {
                        txtQtyAndDesc.Text = dt.Rows[0]["QUANTITY_DESCRIPTION"].ToString();
                    }

                    hid_ID.Value = "old";
                }
            }
        }

        private void FillDropdowns()
        {

            DataTable dtQQMarine1 = ClsVoyageInformation.GetCountryForPageControls().Tables[0];
            cmbVoyageFrom.DataSource = dtQQMarine1;
            cmbVoyageFrom.DataTextField = "COUNTRY";
            cmbVoyageFrom.DataValueField = "PORT_CODE";
            cmbVoyageFrom.DataBind();
            cmbVoyageFrom.Items.Insert(0, "");

            cmbVoyageTo.DataSource = dtQQMarine1;
            cmbVoyageTo.DataTextField = "COUNTRY";
            cmbVoyageTo.DataValueField = "PORT_CODE";
            cmbVoyageTo.DataBind();
            cmbVoyageTo.Items.Insert(0, "");

            cmbThenceTo.DataSource = dtQQMarine1;
            cmbThenceTo.DataTextField = "COUNTRY";
            cmbThenceTo.DataValueField = "PORT_CODE";
            cmbThenceTo.DataBind();
            cmbThenceTo.Items.Insert(0, "");
          
        }

        
        private ClsVoyageInformationInfo  getFormValue()
        {
            ClsVoyageInformationInfo objVoyageInfo = new ClsVoyageInformationInfo();

            objVoyageInfo.VOYAGE_TO = int.Parse(cmbVoyageTo.SelectedValue);
            objVoyageInfo.VOYAGE_FROM = int.Parse(cmbVoyageFrom.SelectedValue);
            objVoyageInfo.THENCE_TO = int.Parse(cmbThenceTo.SelectedValue);
            objVoyageInfo.QUANTITY_DESCRIPTION = txtQtyAndDesc.Text;

            return objVoyageInfo;

        }


        private void saveFormValue()
        {
            try
            {
                //int voyage_info_id;
                int intRetVal;	//For retreiving the return value of business class save function
                string strQQID = "";
                Model.Policy.ClsVoyageInformationInfo objOldVoyageInfo = new ClsVoyageInformationInfo();
                Cms.BusinessLayer.BlApplication.ClsVoyageInformation objVoyage = new Cms.BusinessLayer.BlApplication.ClsVoyageInformation();
                objVoyage.TransactionLogRequired = true;

                //Creating the Model object for holding the New data               
                //Model.Quote.ClsQQMarineRiskDetailsInfo objNewQQMarineRiskInfo = new Cms.Model.Quote.ClsQQMarineRiskDetailsInfo();
                Model.Policy.ClsVoyageInformationInfo objNewVoyageInfo = new ClsVoyageInformationInfo();



                //Creating the Model object for holding the Old data
                //Model.Policy.ClsVoyageInformationInfo objOldVoyageInfo = new ClsVoyageInformationInfo();
                //Model.Quote.ClsQuoteDetailsInfo objOldQuoteInfo = new Cms.Model.Quote.ClsQuoteDetailsInfo();

                lblMessage.Visible = true;

                if (this.hid_ID.Value.ToUpper() == "NEW")
                {


                    objNewVoyageInfo = this.getFormValue();

                    //Setting those values into the Model object which are not in the page
                    objNewVoyageInfo.CUSTOMER_ID = int.Parse(GetCustomerID());
                    objNewVoyageInfo.POLICY_ID = int.Parse(GetPolicyID());
                    objNewVoyageInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());

                    hidCUSTOMER_ID.Value = GetCustomerID();
                   
                    objNewVoyageInfo.CREATED_BY = int.Parse(GetUserId());
                    objNewVoyageInfo.CREATED_DATETIME = DateTime.Now;
                    objNewVoyageInfo.IS_ACTIVE = "Y";

                    intRetVal = objVoyage.Add(objNewVoyageInfo, out intVOYAGE_INFO_ID);
                    //hidVOYAGE_INFO_ID.Value = voyage_info_id.ToString();
                    Session["VOYAGE_INFO_ID"] = intVOYAGE_INFO_ID;
 
                    if (intRetVal > 0)			// Value inserted successfully.
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2050");
                        hidFormSaved.Value = "1";
                        SetCustomerID(hidCUSTOMER_ID.Value.ToString());
                        hid_ID.Value = intRetVal.ToString();
                        hidIS_ACTIVE.Value = "Y";
                        //hidRefreshTabIndex.Value = "Y";
                        //hidSaveMsg.Value = "Insert";
                        Session["Insert"] = "1";

                    }
                    else if (intRetVal == -1)	// Duplicate code exist, insert failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2054");
                        hidFormSaved.Value = "2";
                        return;
                    }
                    else						// Error occured while processing, insert failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2055");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                }
                else
                {
                    //    Creating Business layer object to do processing
                    Cms.BusinessLayer.BlApplication.ClsVoyageInformation objVoyage1 = new ClsVoyageInformation();

                    //customer id for the record which is being updated
                    intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
                    SetCustomerID(hidCUSTOMER_ID.Value.ToString());

                    //Creating the Model object for holding the New data
                    Model.Policy.ClsVoyageInformationInfo objVoyageInfo = getFormValue();

                    //Creating the Model object for holding the Old data
                    //Model.Policy.ClsVoyageInformationInfo objOldVoyageInfo = new ClsVoyageInformationInfo();


                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    //base.PopulateModelObject(objOldVoyageInfo, hidOldXML.Value);//Comment By Kuldeep on 22-0-2012

                    //Setting those values into the Model object which are not in the page
                   
                    //objVoyageInfo.CUSTOMER_ID = intCUSTOMER_ID;
                    //objVoyageInfo.POLICY_ID = int.Parse(GetPolicyID());
                    //objVoyageInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
                    if (hidVOYAGE_INFO_ID.Value == "")
                    {
                        objVoyageInfo.VOYAGE_INFO_ID = int.Parse(Session["VOYAGE_INFO_ID"].ToString());
                    }
                    else
                    {
                        objVoyageInfo.VOYAGE_INFO_ID = int.Parse(hidVOYAGE_INFO_ID.Value);
                    }
                    objVoyageInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objVoyageInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    
                    //intVOYAGE_INFO_ID
                    SetCustomerID(intCUSTOMER_ID.ToString());

                    intRetVal = objVoyage1.Update(objVoyageInfo);


                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2051");
                        hidFormSaved.Value = "1";                      

                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2056");
                        hidFormSaved.Value = "2";
                        return;
                    }
                    else						// Error occured while processing, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2057");
                        hidFormSaved.Value = "2";
                    }
                    //LoadData();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveFormValue();
           
        }

    }
}