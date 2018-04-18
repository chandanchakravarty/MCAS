using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using System.IO;
using System.Xml;
using Cms.DataLayer;
using Cms.Model.Quote;


namespace Cms.CmsWeb.aspx
{
    public partial class QQMarineRiskDetails : cmsbase
    {
        int intCUSTOMER_ID;
        int intPOLICY_ID;
        int intPOLICY_VERSION_ID;
        int intQQ_ID;
        
        
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

                hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
                SetCustomerID(hidCUSTOMER_ID.Value);
                intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

                hidPOLICY_ID.Value = Request.Params["POLICY_ID"];
                SetPolicyID(hidPOLICY_ID.Value);
                intPOLICY_ID = int.Parse(hidPOLICY_ID.Value);

                hidPOLICY_VERSION_ID.Value = Request.Params["POLICY_VERSION_ID"];
                SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
                intPOLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

                string strQUOTENUM = Request.Params["QUOTE_NUM"];
                ClsQuickQuote objQQuote = new ClsQuickQuote();
                intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQUOTENUM);

                hlkVoyageFromDate.Attributes.Add("OnClick", "fPopCalendar(document.QQMarineRiskDetails.txtVoyageFromDate,document.QQMarineRiskDetails.txtVoyageFromDate)");

                //btnReset.Attributes.Add("onclick", "javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
               // btnSave.Attributes.Add("onclick", "javascript:validatorForInsuranceConditions();");

                FillDropdowns();
                LoadData();

            }
        }

        private ClsQQMarineRiskDetailsInfo getFormValue()
        {
            Model.Quote.ClsQQMarineRiskDetailsInfo objQQMarineRiskDetailsInfo = new Model.Quote.ClsQQMarineRiskDetailsInfo();

            objQQMarineRiskDetailsInfo.VOYAGE_TO = int.Parse(cmbVoyageTo.SelectedValue);
            objQQMarineRiskDetailsInfo.VOYAGE_FROM = int.Parse(cmbVoyageFrom.SelectedValue);
            objQQMarineRiskDetailsInfo.THENCE_TO = int.Parse(cmbThenceTo.SelectedValue);
            objQQMarineRiskDetailsInfo.VESSEL = int.Parse(cmbVessel.SelectedValue);
            if(chkAircraftNo.Checked)
            {
                objQQMarineRiskDetailsInfo.AIRCRAFT_NUMBER = txtAircraftNo.Text;
            }
            
            if(chkLandTransport.Checked)
            {
                 objQQMarineRiskDetailsInfo.LAND_TRANSPORT = txtLandTransport.Text;
            }

            objQQMarineRiskDetailsInfo.VOYAGE_FROM_DATE = txtVoyageFromDate.Text != "" ? DateTime.Parse(txtVoyageFromDate.Text) : DateTime.Parse("1/1/1900 00:00:00");
           
            objQQMarineRiskDetailsInfo.QUANTITY_DESCRIPTION = txtQtyAndDesc.Text;

            if ((rdlInsuranceCondition1.SelectedItem.Text != "") && (rdlInsuranceCondition1.SelectedItem.Text != null))
            {
                objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS1 = 0.34;
                objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS1_SELECTION = rdlInsuranceCondition1.SelectedIndex.ToString(); 
            }
            else
            {
                objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS1 = 0.00;
                objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS1_SELECTION = "";
            }                       

            if(chkInsuranceCondition2.Checked)
            {
                 objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS2 = 0.05;                
            }
            else
            {
                objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS2 = 0.00;
            }

            if(chkInsuranceCondition3.Checked)
            {
                 objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS3 = 0.03;                
            }
            else
            {
                objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS3 = 0.00;
            }

            objQQMarineRiskDetailsInfo.MARINE_RATE = objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS1 + objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS2 + objQQMarineRiskDetailsInfo.INSURANCE_CONDITIONS3;            

            return objQQMarineRiskDetailsInfo;

        }



        private void SaveFormValue()
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
                string strQQID = "";
                Cms.BusinessLayer.BlQuote.ClsQQMarineRiskDetails objQQMarineDetails = new Cms.BusinessLayer.BlQuote.ClsQQMarineRiskDetails();
                objQQMarineDetails.TransactionLogRequired = true;

                //Creating the Model object for holding the New data               
                Model.Quote.ClsQQMarineRiskDetailsInfo objNewQQMarineRiskInfo = new Cms.Model.Quote.ClsQQMarineRiskDetailsInfo();
                
            
                //Creating the Model object for holding the Old data
                Model.Quote.ClsQQMarineRiskDetailsInfo objOldQQMarineRiskInfo = new Cms.Model.Quote.ClsQQMarineRiskDetailsInfo();
                //Model.Quote.ClsQuoteDetailsInfo objOldQuoteInfo = new Cms.Model.Quote.ClsQuoteDetailsInfo();

                lblMessage.Visible = true;

                if (this.hid_ID.Value.ToUpper() == "NEW")
                {
                    //Mapping feild and Lebel to maintain the transction log into the database.
                    //objNewQuoteInfo.TransactLabel	=	MapTransactionLabel(objResourceMgr,this);

                    //objNewQuoteInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/client/aspx/AddCustomerQQ.aspx.resx");

                    //Setting  the Page details(all the control values) into the Model Object
                    //base.PopulateModelObject(this,objNewQuoteInfo);

                    objNewQQMarineRiskInfo = this.getFormValue();
                    
                    //Setting those values into the Model object which are not in the page
                    objNewQQMarineRiskInfo.CUSTOMER_ID = int.Parse(GetCustomerID());
                    objNewQQMarineRiskInfo.POLICY_ID = int.Parse(GetPolicyID());
                    objNewQQMarineRiskInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
                    

                    hidCUSTOMER_ID.Value = GetCustomerID();
                    strQQID = GetQQ_ID();
                    ClsQuickQuote objQQuote = new ClsQuickQuote();
                    intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQQID);
                    objNewQQMarineRiskInfo.QUOTE_ID = intQQ_ID;
                   
                    objNewQQMarineRiskInfo.CREATED_BY = int.Parse(GetUserId());
                    objNewQQMarineRiskInfo.CREATED_DATETIME = DateTime.Now;
                    objNewQQMarineRiskInfo.IS_ACTIVE = "Y";
                                                                          
                    intRetVal = objQQMarineDetails.Add(objNewQQMarineRiskInfo);


                    if (intRetVal > 0)			// Value inserted successfully.
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2050");
                        hidFormSaved.Value = "1";
                        SetCustomerID(hidCUSTOMER_ID.Value.ToString());
                        hid_ID.Value = intRetVal.ToString();
                        hidIS_ACTIVE.Value = "Y";
                        hidRefreshTabIndex.Value = "Y";
                        hidSaveMsg.Value = "Insert";
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
                        Cms.BusinessLayer.BlQuote.ClsQQMarineRiskDetails  objMarineRisk = new Cms.BusinessLayer.BlQuote.ClsQQMarineRiskDetails();

                        //customer id for the record which is being updated
                        intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
                        SetCustomerID(hidCUSTOMER_ID.Value.ToString());

                     //Creating the Model object for holding the New data
                        Model.Quote.ClsQQMarineRiskDetailsInfo objNewMarineRiskInfo = getFormValue();

                    //Creating the Model object for holding the Old data
                        Model.Quote.ClsQQMarineRiskDetailsInfo objOldMarineRiskInfo = new Model.Quote.ClsQQMarineRiskDetailsInfo();


                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                        //base.PopulateModelObject(objOldMarineRiskInfo, hidOldXML.Value);//Comment By Kuldeep on 22-0-2012

                    //Setting those values into the Model object which are not in the page
                        objNewMarineRiskInfo.CUSTOMER_ID = intCUSTOMER_ID;
                        objNewMarineRiskInfo.MODIFIED_BY = int.Parse(GetUserId());
                        objNewMarineRiskInfo.LAST_UPDATED_DATETIME = DateTime.Now;

                        SetCustomerID(intCUSTOMER_ID.ToString());
                                           
                        intRetVal = objMarineRisk.Update(objNewMarineRiskInfo);

                        if (intRetVal > 0)			// update successfully performed
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2051");
                            hidFormSaved.Value = "1";
                            hidRefreshTabIndex.Value = "Y";
                            hidSaveMsg.Value = "Update";


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
                        LoadData();
                    }

                   



                }
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void LoadData()
        {
            Cms.BusinessLayer.BlQuote.ClsQQMarineRiskDetails objMarineDetails = new Cms.BusinessLayer.BlQuote.ClsQQMarineRiskDetails();
            DataSet dsMarineCargo = objMarineDetails.GetQQ_MarineCargo_Risk_Details(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID, intQQ_ID);
            if (dsMarineCargo.Tables.Count > 0)
            {
                DataTable dt = dsMarineCargo.Tables[0];

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
                    if (dt.Rows[0]["VESSEL"] != System.DBNull.Value)
                    {
                        cmbVessel.SelectedValue = dt.Rows[0]["VESSEL"].ToString();
                    }
                    if (dt.Rows[0]["AIRCRAFT_NUMBER"] != System.DBNull.Value)
                    {
                        txtAircraftNo.Text = dt.Rows[0]["AIRCRAFT_NUMBER"].ToString();
                    }
                    if (dt.Rows[0]["LAND_TRANSPORT"] != System.DBNull.Value)
                    {
                        txtLandTransport.Text = dt.Rows[0]["LAND_TRANSPORT"].ToString();
                    }

                    DataTable dtDate = ClsQQMarineRiskDetails.GetAppEffectiveDate(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID, intQQ_ID).Tables[0];
                    if ((dtDate.Rows.Count > 0) && (dtDate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value))
                    {
                        
                        txtVoyageFromDate.Text = Convert.ToDateTime(dtDate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                    }

                    if (dt.Rows[0]["QUANTITY_DESCRIPTION"] != System.DBNull.Value)
                    {
                        txtQtyAndDesc.Text = dt.Rows[0]["QUANTITY_DESCRIPTION"].ToString();
                    }
                    if (dt.Rows[0]["INSURANCE_CONDITIONS1_SELECTION"] != System.DBNull.Value)
                    {
                        rdlInsuranceCondition1.SelectedIndex =int.Parse(dt.Rows[0]["INSURANCE_CONDITIONS1_SELECTION"].ToString());
                    }
                    if (dt.Rows[0]["INSURANCE_CONDITIONS2"] != System.DBNull.Value)
                    {
                        chkInsuranceCondition2.Checked = true;
                    }
                    if (dt.Rows[0]["INSURANCE_CONDITIONS3"] != System.DBNull.Value)
                    {
                        chkInsuranceCondition3.Checked = true;
                    }

                    hid_ID.Value = "old";
                }

            }
            else
            {
                DataTable dtDate = ClsQQMarineRiskDetails.GetAppEffectiveDate(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID, intQQ_ID).Tables[0];
                if ((dtDate.Rows.Count > 0) && (dtDate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value))
                {
                    txtVoyageFromDate.Text = dtDate.Rows[0]["APP_EFFECTIVE_DATE"].ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Saving the values into the database
            SaveFormValue();
            //requiredfieldvalidatorForInsuranceConditions();
        }


        //private void requiredfieldvalidatorForInsuranceConditions()
        //{
        //    if ((rdlInsuranceCondition1.SelectedItem.Text == "") && (!chkInsuranceCondition2.Checked) && (!chkInsuranceCondition3.Checked))
        //    {

        //    }
        //}

        private void SetSessionValues()
        {
            hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
            SetCustomerID(hidCUSTOMER_ID.Value);
            //intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

            hidPOLICY_ID.Value = Request.Params["POLICY_ID"];
            SetPolicyID(hidPOLICY_ID.Value);
            //intPOLICY_ID = int.Parse(hidPOLICY_ID.Value);

            hidPOLICY_VERSION_ID.Value = Request.Params["POLICY_VERSION_ID"];
            SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
            //intPOLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
        }


        private void FillDropdowns()
        {            
           
            DataTable dtQQMarine1 = ClsQQMarineRiskDetails.GetCountryForPageControls().Tables[0];
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

            DataTable dtQQMarine2 = ClsQQMarineRiskDetails.GetVesselNameForPageControls().Tables[0];
            cmbVessel.DataSource = dtQQMarine2;
            cmbVessel.DataTextField = "VESSEL_NAME";
            cmbVessel.DataValueField = "VESSEL_ID";
            cmbVessel.DataBind();
            cmbVessel.Items.Insert(0, "");
         
        }

       

      

       
    }
}