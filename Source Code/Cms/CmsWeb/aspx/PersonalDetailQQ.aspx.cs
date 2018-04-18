using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application;
using Cms.Model.Client;
using Cms.Model.Quote;
using Cms.CmsWeb.Utils;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlApplication;

namespace Cms.CmsWeb.aspx
{
    public partial class PersonalDetailQQ : Cms.CmsWeb.cmsbase
    {
        int intCUSTOMER_ID = 0;
        int intCUST_PART_ID = 0;
        int intQQ_ID = 0;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        public const string ALL_SECURITY = @"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
        public const string READ_SECURITY = @"<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";

        private void Page_Load(object sender, EventArgs e)
        {            
            base.ScreenId = "134_0";
            Ajax.Utility.RegisterTypeForAjax(typeof(PersonalDetailQQ));
            // set variable values
            intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            intCUST_PART_ID = int.Parse(hidCUST_PART_ID.Value);            
            intQQ_ID = int.Parse(hid_QUOTEID.Value);

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = ALL_SECURITY;
            
            //btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            //btnSave.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = ALL_SECURITY;

            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
            hlkDATE_OF_BIRTH.Attributes.Add("OnClick", "fPopCalendar(document.QQ_PERSONAL_DETAIL.txtDATE_OF_BIRTH,document.QQ_PERSONAL_DETAIL.txtDATE_OF_BIRTH)");
            hlkDATE_OF_BIRTH.Attributes.Add("onkeypress", "fPopCalendar(document.QQ_PERSONAL_DETAIL.txtDATE_OF_BIRTH,document.QQ_PERSONAL_DETAIL.txtDATE_OF_BIRTH)");
            //hlkDATE_OF_BIRTH.Attributes.Add("onBlur", "javascript:FormatDateofBirth");
            //txtDATE_OF_BIRTH.Attributes.Add("onBlur", "javascript:FormatDateofBirth");

            setFormValue();
            if (!Page.IsPostBack)
            {
                //btnSave.Attributes.Add("onclick", "javascript:return ValCheck();");

                string strSystemID = GetSystemId();
                int strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID);

                intCUSTOMER_ID = int.Parse(GetCustomerID());              

                FillDropdowns();

                if (intCUSTOMER_ID != -100)
                {
                    FillCustomerControl();
                }

                string strCarrierSystemID = System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();

                hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
                intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

                string strQUOTENUM = Request.Params["QUOTE_NUM"];
                ClsQuickQuote objQQuote = new ClsQuickQuote();
                intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQUOTENUM);
                hid_QUOTEID.Value = intQQ_ID.ToString();
                LoadData();

                

            }

            

        }

        private void setFormValue()
        {
            hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
            intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

            string strQUOTENUM = Request.Params["QUOTE_NUM"];
            ClsQuickQuote objQQuote = new ClsQuickQuote();
            intQQ_ID = objQQuote.GetQuickQuoteDetails(intCUSTOMER_ID.ToString(), strQUOTENUM);
            hid_QUOTEID.Value = intQQ_ID.ToString();

            intCUST_PART_ID = objQQuote.GetCustomerParticularID(intCUSTOMER_ID, intQQ_ID);

            if (intCUST_PART_ID > 0)
            {
                hidFormSaved.Value = "1";
            }
        }

        private void FillDropdowns()
        {
            DateTime strDOQ = DateTime.Now;
            lblDOQ.Text = String.Format("{0:dd/MM/yyyy}", strDOQ);

            string strSysID = GetSystemId();
            int intAgentID = this.GetAgencyCode(strSysID);
            lblAGENT.Text = this.GetAgencyDisplayName(intAgentID);

            DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;
            cmbCUSTOMER_COUNTRY.DataSource = dt;
            cmbCUSTOMER_COUNTRY.DataTextField = COUNTRY_NAME;
            cmbCUSTOMER_COUNTRY.DataValueField = COUNTRY_ID;
            cmbCUSTOMER_COUNTRY.DataBind();
            cmbCUSTOMER_COUNTRY.SelectedValue = "7";

            cmbGENDER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("Sex");
            cmbGENDER.DataTextField = "LookupDesc";
            cmbGENDER.DataValueField = "LookupCode";
            cmbGENDER.DataBind();
            cmbGENDER.Items.Insert(0, "");

            DataSet dsLookup = null;

            dsLookup = ClsCustomer.GetLookups();

            //dt = dsLookup.Tables[2].Select("", "LOOKUP_VALUE_DESC").CopyToDataTable<DataRow>();
            //cmbCUSTOMER_TYPE.DataSource = dt;
            //cmbCUSTOMER_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
            //cmbCUSTOMER_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
            //cmbCUSTOMER_TYPE.DataBind();
            //cmbCUSTOMER_TYPE.Items.Insert(0, "");
            //cmbCUSTOMER_TYPE.SelectedIndex = -1;
            dt = dsLookup.Tables[2].Select("", "LOOKUP_VALUE_DESC").CopyToDataTable<DataRow>();
            cmbCUSTOMER_TYPE.DataSource = dt;
            cmbCUSTOMER_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
            cmbCUSTOMER_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_TYPE.DataBind();
            cmbCUSTOMER_TYPE.Items.Insert(0, "");
            cmbCUSTOMER_TYPE.SelectedIndex = -1;

            //Comment by kuldeep to fetch all customer type as on customer page tfs 2713
            //cmbCUSTOMER_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CSTTYP");
            //cmbCUSTOMER_TYPE.DataTextField = "LookupDesc";
            //cmbCUSTOMER_TYPE.DataValueField = "LookupID";
            //cmbCUSTOMER_TYPE.DataBind();
            //cmbCUSTOMER_TYPE.Items.Insert(0, "");

            cmbAPPLICANT_OCCU.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC");
            cmbAPPLICANT_OCCU.DataTextField = "LookupDesc";
            cmbAPPLICANT_OCCU.DataValueField = "LookupID";
            cmbAPPLICANT_OCCU.DataBind();
            cmbAPPLICANT_OCCU.Items.Insert(0, "");

            //cmbDRIVING_EXP.Items.Add("-Select-");
            cmbDRIVING_EXP.Items.Insert(0, "");
            for (int i = 0; i < 100; i++)
			{
                cmbDRIVING_EXP.Items.Add(i.ToString());
			}
            cmbDRIVING_EXP.SelectedIndex = -1;

            //cmbEXISTING_NCD.Items.Add("-Select-");
            cmbEXISTING_NCD.Items.Insert(0, "");
            for (int i = 10 ; i < 51; i = i + 10)
            {
                cmbEXISTING_NCD.Items.Add(i.ToString()+ "%");
            }
            cmbEXISTING_NCD.SelectedIndex = -1;
            


        }

        private void FillCustomerControl()
        {            
            DataSet dsCust = ClsCustomer.GetCustomerDetails(intCUSTOMER_ID);

            DataTable dt = dsCust.Tables[0];
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_FIRST_NAME.Text = dt.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                    txtCUSTOMER_FIRST_NAME.ReadOnly = true;
                }
                if (dt.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_MIDDLE_NAME.Text = dt.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                    txtCUSTOMER_MIDDLE_NAME.ReadOnly = true;
                }
                if (dt.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_LAST_NAME.Text = dt.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                    //txtCUSTOMER_LAST_NAME.ReadOnly = true;
                }
                if (dt.Rows[0]["GENDER"] != System.DBNull.Value && dt.Rows[0]["GENDER"].ToString().Trim() != "")
                {
                    cmbGENDER.SelectedValue = dt.Rows[0]["GENDER"].ToString();
                    cmbGENDER.Enabled = false;
                }
                if (dt.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    txtDATE_OF_BIRTH.Text = dt.Rows[0]["DATE_OF_BIRTH"].ToString();
                    txtDATE_OF_BIRTH.ReadOnly = true;
                }
                if (dt.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
                {
                    cmbCUSTOMER_TYPE.SelectedValue = dt.Rows[0]["CUSTOMER_TYPE"].ToString();
                    cmbCUSTOMER_TYPE.Enabled = false;
                }
                //if (dt.Rows[0]["PREFIX"] != System.DBNull.Value)
                //{
                //    cmbAPPLICANT_OCCU.SelectedValue = dt.Rows[0]["PREFIX"].ToString();
                //    cmbAPPLICANT_OCCU.Enabled = false;
                //}
            }
        }

        private void LoadData()
        {
            Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails objCustDetails = new Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails();
            DataSet dsCustomer = objCustDetails.FetchCustomerParticularDetail(intCUSTOMER_ID, intQQ_ID);

            DataTable dt = dsCustomer.Tables[0];
            if (dt.Rows.Count != 0)
            {
                string strFNAME = "";
                string strMNAME = "";
                string strLNAME = "";
                if (dt.Rows[0]["CUSTOMER_CODE"] != System.DBNull.Value)
                {
                    txtCUSTOMER_CODE.Text = dt.Rows[0]["CUSTOMER_CODE"].ToString();
                    txtCUSTOMER_CODE.ReadOnly = true;
                }
                if (dt.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_FIRST_NAME.Text = dt.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();                    
                }
                if (dt.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_MIDDLE_NAME.Text = dt.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                }
                if (dt.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_LAST_NAME.Text = dt.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                }

                if (dt.Rows[0]["GENDER"] != System.DBNull.Value)
                {
                    cmbGENDER.SelectedValue = dt.Rows[0]["GENDER"].ToString();
                }

                if (dt.Rows[0]["NATIONALITY"] != System.DBNull.Value)
                {
                    cmbCUSTOMER_COUNTRY.SelectedValue = dt.Rows[0]["NATIONALITY"].ToString();
                }

                if (dt.Rows[0]["CUSTOMER_OCCU"] != System.DBNull.Value)
                {
                    cmbAPPLICANT_OCCU.SelectedValue = dt.Rows[0]["CUSTOMER_OCCU"].ToString();
                }

                if (dt.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
                {
                    cmbCUSTOMER_TYPE.SelectedValue = dt.Rows[0]["CUSTOMER_TYPE"].ToString();
                }

                if (dt.Rows[0]["CUSTOMER_AGENCY_ID"] != System.DBNull.Value)
                {
                    int AgentID = int.Parse(dt.Rows[0]["CUSTOMER_AGENCY_ID"].ToString());
                    lblAGENT.Text = this.GetAgencyDisplayName(AgentID);
                }

                lblDOQ.Text = DateTime.Now.ToShortDateString();

                if (dt.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    string strDOB = dt.Rows[0]["DATE_OF_BIRTH"].ToString();

                    strDOB = Convert.ToDateTime(strDOB).ToShortDateString();

                    txtDATE_OF_BIRTH.Text = strDOB;


                }

                if (dt.Rows[0]["IS_HOME_EMPLOYEE"] != System.DBNull.Value)
                {
                    string strISHOME = dt.Rows[0]["IS_HOME_EMPLOYEE"].ToString();

                    if (strISHOME.Trim() == "1")
                    {
                        rbOCCU_TYPE_INDOOR.Checked = true;
                        rbOCCU_TYPE_OUTDOOR.Checked = false;
                    }
                    else
                    {
                        rbOCCU_TYPE_INDOOR.Checked = false;
                        rbOCCU_TYPE_OUTDOOR.Checked = true;
                    }
                }

                if(dt.Rows[0]["DRIVER_EXP_YEAR"] != System.DBNull.Value)
                {
                    string strEXPYEAR = dt.Rows[0]["DRIVER_EXP_YEAR"].ToString();

                    cmbDRIVING_EXP.SelectedValue = strEXPYEAR;
                }

                if (dt.Rows[0]["ANY_CLAIM"] != System.DBNull.Value)
                {
                    string strCLAIM = dt.Rows[0]["ANY_CLAIM"].ToString();

                    if (strCLAIM.Trim() == "1")
                    {
                        rbCLAIM_YES.Checked = true;
                        rbCLAIM_NO.Checked = false;
                    }
                    else
                    {
                        rbCLAIM_YES.Checked = false;
                        rbCLAIM_NO.Checked = true;
                    }
                }

                if (dt.Rows[0]["EXIST_NCD_LESS_10"] != System.DBNull.Value)
                {
                    string strNCDLESS = dt.Rows[0]["EXIST_NCD_LESS_10"].ToString();

                    if (strNCDLESS.Trim() == "1")
                    {
                        rbNCD_YES.Checked = true;
                        rbNCD_NO.Checked = false;
                    }
                    else
                    {
                        rbNCD_YES.Checked = false;
                        rbNCD_NO.Checked = true;
                    }
                }

                if (dt.Rows[0]["EXISTING_NCD"] != System.DBNull.Value)
                {
                    string strEXNCD = dt.Rows[0]["EXISTING_NCD"].ToString();

                    cmbEXISTING_NCD.SelectedValue = strEXNCD;
                }

                if (dt.Rows[0]["DEMERIT_DISCOUNT"] != System.DBNull.Value)
                {
                    string strDEMERIT = dt.Rows[0]["DEMERIT_DISCOUNT"].ToString();

                    if (strDEMERIT.Trim() == "1")
                    {
                        rbDEMERIT_YES.Checked = true;
                        rbDEMERIT_NO.Checked = false;
                    }
                    else
                    {
                        rbDEMERIT_YES.Checked = false;
                        rbDEMERIT_NO.Checked = true;
                    }
                }

            }

        }



        #region Web control events
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Saving the values into the database
            SaveFormValue();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            // Reset the values 
            txtCUSTOMER_CODE.Text = "";
            txtCUSTOMER_FIRST_NAME.Text = "";
            txtCUSTOMER_LAST_NAME.Text = "";
            txtDATE_OF_BIRTH.Text = "";

            cmbAPPLICANT_OCCU.SelectedIndex = -1;
            cmbCUSTOMER_COUNTRY.SelectedIndex = -1;
            cmbCUSTOMER_TYPE.SelectedIndex = -1;
            cmbDRIVING_EXP.SelectedIndex = -1;
            cmbEXISTING_NCD.SelectedIndex = -1;
            cmbGENDER.SelectedIndex = -1;

            rbCLAIM_NO.Checked = true;
            rbDEMERIT_NO.Checked = true;
            rbNCD_NO.Checked = true;
            rbOCCU_TYPE_INDOOR.Checked = true;

        }

        #endregion

        #region Save function
        /// <summary>
        /// saves the posted data into table using the business layer class (clsCustomer)
        /// </summary>
        /// <returns>void </returns>
        private void SaveFormValue()
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function

                // Creating Business layer object to do processing
                Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails objCustomer = new Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails();
                objCustomer.TransactionLogRequired = true;

                //Creating the Model object for holding the New data
                //Model.Client.ClsCustomerInfo objNewCustomer = new Cms.Model.Client.ClsCustomerInfo();
                Model.Quote.ClsCustomerParticluarInfo objNewCustomer = new Cms.Model.Quote.ClsCustomerParticluarInfo();
                


                //Creating the Model object for holding the Old data
                //Model.Client.ClsCustomerInfo objOldCustomer = new Cms.Model.Client.ClsCustomerInfo();
                Model.Quote.ClsCustomerParticluarInfo objOldCustomer = new Cms.Model.Quote.ClsCustomerParticluarInfo();

                lblMessage.Visible = true;                

                //Insert new Customer
                //if(strRowId.ToUpper() == "NEW") 
                if (this.hidFormSaved.Value == "0")
                {
                    //Mapping feild and Lebel to maintain the transction log into the database.
                    //objNewCustomer.TransactLabel	=	MapTransactionLabel(objResourceMgr,this);

                    //objNewCustomer.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/client/aspx/AddCustomerQQ.aspx.resx");

                    //Setting  the Page details(all the control values) into the Model Object
                    //base.PopulateModelObject(this,objNewCustomer);

                    objNewCustomer = this.getFormValue();

                    int CustID;
                    //Setting those values into the Model object which are not in the page
                    objNewCustomer.CUSTOMER_ID = int.Parse(GetCustomerID());
                    hidCUSTOMER_ID.Value = GetCustomerID();
                    objNewCustomer.CREATED_BY = int.Parse(GetUserId());
                    objNewCustomer.CREATED_DATETIME = DateTime.Now;
                    objNewCustomer.DATE_OF_QUOTATION = DateTime.Now.ToShortDateString();
                    string strQQID = GetQQ_ID();

                    ClsQuickQuote objQQuote = new ClsQuickQuote();
                    intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQQID);
                    objNewCustomer.QUOTE_ID = intQQ_ID;
                    hid_QUOTEID.Value = intQQ_ID.ToString();
                                       
                    
                    //Agni
                    intRetVal = objCustomer.Add(objNewCustomer, out CustID);
                    

                    if (intRetVal > 0)			// Value inserted successfully.
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2050");
                        hidFormSaved.Value = "1";
                        hid_ID.Value = CustID.ToString();
                        hidCUST_PART_ID.Value = CustID.ToString();
                        //hid_ID.Value = "1";
                        intCUST_PART_ID = CustID;
                        SetCustomerID(hidCUSTOMER_ID.Value.ToString());
                        hidIS_ACTIVE.Value = "Y";
                        hidRefreshTabIndex.Value = "Y";
                        hidSaveMsg.Value = "Insert";
                        Session["Insert"] = "1";
                        SetAttachedCustomerID(CustID.ToString());
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
                //Update Customer
                else
                {
                    // Creating Business layer object to do processing
                    Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails objCustomer1 = new Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails();

                    //customer id for the record which is being updated
                    intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
                    SetCustomerID(hidCUSTOMER_ID.Value.ToString());


                    //Setting  the Page details(all the control values) into the Model Object
                    //base.PopulateModelObject(this,objNewCustomer);

                    //Creating the Model object for holding the New data
                    Model.Quote.ClsCustomerParticluarInfo objNewCustomer1 = getFormValue();

                    //Creating the Model object for holding the Old data
                    Model.Quote.ClsCustomerParticluarInfo objOldCustomer1 = new Model.Quote.ClsCustomerParticluarInfo();


                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    //base.PopulateModelObject(objOldCustomer1, hidOldXML.Value);

                    //Setting those values into the Model object which are not in the page
                    //objNewCustomer.CustomerId				=	intCUSTOMER_ID;
                    objNewCustomer1.CUSTOMER_ID = intCUSTOMER_ID;
                    objNewCustomer1.ID = intCUST_PART_ID;
                    objNewCustomer1.QUOTE_ID = intQQ_ID;
                    objNewCustomer1.MODIFIED_BY = int.Parse(GetUserId());
                    objNewCustomer1.LAST_UPDATED_DATETIME = DateTime.Now;

                    SetCustomerID(intCUSTOMER_ID.ToString());
                    //Setting those values into the Model object which are not in the page
                    objOldCustomer1.CUSTOMER_ID = intCUSTOMER_ID;

                    //intRetVal						=		objCustomer.Update(objOldCustomer,objNewCustomer);
                    //Agni
                    intRetVal = objCustomer1.Update(objNewCustomer1);

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

                }

            }
            catch (Exception ex)
            {
                // Show exception message
                lblMessage.Text = "Information could not be saved. Try again!";
                lblMessage.Visible = true;

                //Publishing the exception using the static method of Exception manager class
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                return;
            }            

            //CreateTransXML();
            LoadData();
        }


        /// <summary>
        /// Setting the page values in to the Customer object
        /// </summary>
        /// <returns></returns>
        private ClsCustomerParticluarInfo getFormValue()
        {
            Model.Quote.ClsCustomerParticluarInfo objCustomerInfo = new Model.Quote.ClsCustomerParticluarInfo();

            //objCustomerInfo.CUSTOMER_ID = 0;
            objCustomerInfo.CUSTOMER_FIRST_NAME = txtCUSTOMER_FIRST_NAME.Text;
            objCustomerInfo.CUSTOMER_MIDDLE_NAME = txtCUSTOMER_MIDDLE_NAME.Text;
            objCustomerInfo.CUSTOMER_LAST_NAME = txtCUSTOMER_LAST_NAME.Text;
            objCustomerInfo.CUSTOMER_CODE = txtCUSTOMER_CODE.Text;
            objCustomerInfo.CUSTOMER_TYPE = int.Parse(cmbCUSTOMER_TYPE.SelectedItem.Value.ToString());
            objCustomerInfo.DATE_OF_BIRTH = DateTime.Parse(txtDATE_OF_BIRTH.Text);

            if (cmbGENDER.SelectedValue.ToString() == "M")
                objCustomerInfo.GENDER = "M";
            else
                objCustomerInfo.GENDER = "F";

            objCustomerInfo.NATIONALITY = cmbCUSTOMER_COUNTRY.SelectedItem.Value.ToString();

            if(rbOCCU_TYPE_INDOOR.Checked)
                objCustomerInfo.IS_HOME_EMPLOYEE = "1";
            else
                objCustomerInfo.IS_HOME_EMPLOYEE = "0";

            objCustomerInfo.CUSTOMER_OCCU = int.Parse(cmbAPPLICANT_OCCU.SelectedItem.Value.ToString());
            objCustomerInfo.DRIVER_EXP_YEAR = int.Parse(cmbDRIVING_EXP.SelectedItem.Value.ToString());

            if (rbCLAIM_YES.Checked)
                objCustomerInfo.ANY_CLAIM = "1";
            else
                objCustomerInfo.ANY_CLAIM = "0";
            
            if(rbNCD_YES.Checked)
                objCustomerInfo.EXIST_NCD_LESS_10 = "1";
            else
                objCustomerInfo.EXIST_NCD_LESS_10 = "0";

            objCustomerInfo.EXISTING_NCD = cmbEXISTING_NCD.SelectedItem.Value.ToString();

            if(rbDEMERIT_YES.Checked)
                objCustomerInfo.DEMERIT_DISCOUNT = "1";
            else
                objCustomerInfo.DEMERIT_DISCOUNT = "0";

            string strSystemID = GetSystemId();
            objCustomerInfo.CUSTOMER_AGENCY_ID = this.GetAgencyCode(strSystemID);

            return objCustomerInfo;

        }

        private int GetAgencyCode(string agentCode)
        {
            int agentID = ClsAgency.GetAgencyIDFromCode(agentCode);
            return agentID;
        }

        private string GetAgencyDisplayName(int agentid)
        {
            string agentName = ClsAgency.GetAgencyCodeFromID(agentid);
            return agentName;
        }

        #endregion


        #region Web Form Designer generated code
        //override protected void OnInit(EventArgs e)
        //{
        //    InitializeComponent();
        //    base.OnInit(e);
        //}
        ////private void InitializeComponent()
        ////{
        ////    this.btnSave_Click += new System.EventHandler(this.btnSave_Click);
        ////    this.Load += new System.EventHandler(this.Page_Load);

        ////}
        #endregion
    }
}
