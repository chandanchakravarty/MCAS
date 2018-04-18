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
    public partial class InvoiceDetailQQ : Cms.CmsWeb.cmsbase
    {
        int intCUSTOMER_ID = 0;
        int intCUST_PART_ID = 0;
        int intQQ_ID = 0;

        //protected Cms.CmsWeb.Controls.CmsButton btnReset;
        //protected Cms.CmsWeb.Controls.CmsButton btnSave;
        public const string ALL_SECURITY = @"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
        public const string READ_SECURITY = @"<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";

        private void Page_Load(object sender, EventArgs e)
        {            
            base.ScreenId = "134_0";
            Ajax.Utility.RegisterTypeForAjax(typeof(InvoiceDetailQQ));
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
            txtINVOICE_AMOUNT.Attributes.Add("onBlur", "this.value=formatCurrency(this.value);"); 
            setFormValue();
            SetErrorMessages();
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
        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            try
            {
                csvMARK_UP_RATE_PERC.ErrorMessage = "Mark Up Rate % should be between 0-100";
                revINVOICE_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "116");
                revMARK_UP_RATE_PER.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
                revMARK_UP_RATE_PER.ErrorMessage = ClsMessages.FetchGeneralMessage("611");
                revINVOICE_AMOUNT.ValidationExpression = aRegExpDoublePositiveNonZero;
            }
            catch (Exception ex)
            {
                throw (ex);
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

            intCUST_PART_ID = objQQuote.GetInvoiceParticularID(intCUSTOMER_ID, intQQ_ID);
            if (GetLOBID() == "13")
            {
                hidCUST_PART_ID.Value = intCUST_PART_ID.ToString();
            }
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

            DataTable dt;// = Cms.CmsWeb.ClsFetcher.AllCountry;
           

            DataSet dsLookup = null;

            dsLookup = ClsCustomer.GetLookups();

           
            dt = dsLookup.Tables[2].Select("", "LOOKUP_VALUE_DESC").CopyToDataTable<DataRow>();
            cmbCUSTOMER_TYPE.DataSource = dt;
            cmbCUSTOMER_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
            cmbCUSTOMER_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_TYPE.DataBind();
            cmbCUSTOMER_TYPE.Items.Insert(0, "");
            cmbCUSTOMER_TYPE.SelectedIndex = -1;
            cmbCUSTOMER_TYPE.SelectedIndex = 1;
            cmbCUSTOMER_TYPE.Enabled = false;
          
            cmbAPPLICANT_OCCU.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC");
            cmbAPPLICANT_OCCU.DataTextField = "LookupDesc";
            cmbAPPLICANT_OCCU.DataValueField = "LookupID";
            cmbAPPLICANT_OCCU.DataBind();
            cmbAPPLICANT_OCCU.Items.Insert(0, "");

            cmbCURRENCY_TYPE.DataSource = Cms.CmsWeb.ClsFetcher.Currency;
            cmbCURRENCY_TYPE.DataTextField = "CURR_DESC";
            cmbCURRENCY_TYPE.DataValueField = "CURRENCY_ID";
            cmbCURRENCY_TYPE.DataBind();
            cmbCURRENCY_TYPE.SelectedIndex = 2;

            cmbBILLING_CURRENCY.DataSource = Cms.CmsWeb.ClsFetcher.Currency;
            cmbBILLING_CURRENCY.DataTextField = "CURR_DESC";
            cmbBILLING_CURRENCY.DataValueField = "CURRENCY_ID";
            cmbBILLING_CURRENCY.DataBind();
            cmbBILLING_CURRENCY.SelectedIndex = 2;

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
            Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ objCustDetails = new Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ();
            DataSet dsCustomer = objCustDetails.FetchInvoiceParticularDetail(intCUSTOMER_ID, intQQ_ID);

            DataTable dt = dsCustomer.Tables[0];
            if (dt.Rows.Count != 0)
            {
                string strFNAME = "";
                string strMNAME = "";
                string strLNAME = "";
                if (dt.Rows[0]["COMPANY_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_FIRST_NAME.Text = dt.Rows[0]["COMPANY_NAME"].ToString();
                   // txtCUSTOMER_FIRST_NAME.ReadOnly = true;
                }
              
                if (dt.Rows[0]["BUSINESS_TYPE"] != System.DBNull.Value)
                {
                    cmbAPPLICANT_OCCU.SelectedValue = dt.Rows[0]["BUSINESS_TYPE"].ToString();
                }
                if (dt.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
                {
                    cmbCUSTOMER_TYPE.SelectedValue = dt.Rows[0]["CUSTOMER_TYPE"].ToString();
                }
                if (dt.Rows[0]["OPEN_COVER_NO"] != System.DBNull.Value)
                {
                    txtOPEN_COVER_NO.Text = dt.Rows[0]["OPEN_COVER_NO"].ToString();
                }
                if (dt.Rows[0]["INVOICE_AMOUNT"] != System.DBNull.Value)
                {
                    txtINVOICE_AMOUNT.Text = dt.Rows[0]["INVOICE_AMOUNT"].ToString();
                }
                if (dt.Rows[0]["INVOICE_TYPE"] != System.DBNull.Value)
                {
                    txtINVOICE_TYPE.Text = dt.Rows[0]["INVOICE_TYPE"].ToString();
                }
                if (dt.Rows[0]["CURRENCY_TYPE"] != System.DBNull.Value)
                {
                    cmbCURRENCY_TYPE.SelectedValue = dt.Rows[0]["CURRENCY_TYPE"].ToString();
                }
                if (dt.Rows[0]["BILLING_CURRENCY"] != System.DBNull.Value)
                {
                    cmbBILLING_CURRENCY.SelectedValue = dt.Rows[0]["BILLING_CURRENCY"].ToString();
                }
                if (dt.Rows[0]["MARK_UP_RATE_PERC"] != System.DBNull.Value)
                {
                    txtMARK_UP_RATE_PERC.Text = dt.Rows[0]["MARK_UP_RATE_PERC"].ToString();
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
         
            cmbAPPLICANT_OCCU.SelectedIndex = -1;
           
            cmbCUSTOMER_TYPE.SelectedIndex = -1;
         
          

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
                Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ objCustomer = new Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ();
                objCustomer.TransactionLogRequired = true;

                //Creating the Model object for holding the New data
                //Model.Client.ClsCustomerInfo objNewCustomer = new Cms.Model.Client.ClsCustomerInfo();
                Model.Quote.ClsInvoiceDetailQQInfo objNewCustomer = new Cms.Model.Quote.ClsInvoiceDetailQQInfo();
                


                //Creating the Model object for holding the Old data
                //Model.Client.ClsCustomerInfo objOldCustomer = new Cms.Model.Client.ClsCustomerInfo();
                Model.Quote.ClsInvoiceDetailQQInfo objOldCustomer = new Cms.Model.Quote.ClsInvoiceDetailQQInfo();

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
                    Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ objCustomer1 = new Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ();

                    //customer id for the record which is being updated
                    intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
                    SetCustomerID(hidCUSTOMER_ID.Value.ToString());


                    //Setting  the Page details(all the control values) into the Model Object
                    //base.PopulateModelObject(this,objNewCustomer);

                    //Creating the Model object for holding the New data
                    Model.Quote.ClsInvoiceDetailQQInfo objNewCustomer1 = getFormValue();

                    //Creating the Model object for holding the Old data
                    Model.Quote.ClsInvoiceDetailQQInfo objOldCustomer1 = new Model.Quote.ClsInvoiceDetailQQInfo();


                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    //base.PopulateModelObject(objOldCustomer1, hidOldXML.Value);

                    //Setting those values into the Model object which are not in the page
                    //objNewCustomer.CustomerId				=	intCUSTOMER_ID;
                    objNewCustomer1.CUSTOMER_ID = intCUSTOMER_ID;
                    if(GetLOBID()!="13")
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
        /// //Modified by Kuldeep on 17-Mar-2012
        private ClsInvoiceDetailQQInfo getFormValue()
        {
            Model.Quote.ClsInvoiceDetailQQInfo objInvoiceInfo = new Model.Quote.ClsInvoiceDetailQQInfo();

            //objCustomerInfo.CUSTOMER_ID = 0;
            if (hidCUST_PART_ID.Value != "" && hidCUST_PART_ID.Value != null && hidCUST_PART_ID.Value != "New" && int.Parse(hidCUST_PART_ID.Value) != 0)
                objInvoiceInfo.ID = int.Parse(hidCUST_PART_ID.Value);

            objInvoiceInfo.COMPANY_NAME = (txtCUSTOMER_FIRST_NAME.Text==null?"": txtCUSTOMER_FIRST_NAME.Text);
            objInvoiceInfo.CUSTOMER_TYPE =(cmbCUSTOMER_TYPE.SelectedValue==null?0: int.Parse(cmbCUSTOMER_TYPE.SelectedValue));
            objInvoiceInfo.OPEN_COVER_NO =(txtOPEN_COVER_NO.Text==null?"": txtOPEN_COVER_NO.Text);
            objInvoiceInfo.BUSINESS_TYPE =(cmbAPPLICANT_OCCU.SelectedValue==null?0: int.Parse(cmbAPPLICANT_OCCU.SelectedValue));
            objInvoiceInfo.INVOICE_TYPE = (txtINVOICE_TYPE.Text==null?"": txtINVOICE_TYPE.Text);
            objInvoiceInfo.INVOICE_AMOUNT =(txtINVOICE_AMOUNT.Text==null?0: double.Parse(txtINVOICE_AMOUNT.Text));
            objInvoiceInfo.CURRENCY_TYPE = (cmbCURRENCY_TYPE.SelectedValue==null?0: int.Parse(cmbCURRENCY_TYPE.SelectedValue));
            objInvoiceInfo.BILLING_CURRENCY = (cmbBILLING_CURRENCY.SelectedValue == null ? 0 : int.Parse(cmbBILLING_CURRENCY.SelectedValue));
            objInvoiceInfo.MARK_UP_RATE_PERC=(txtMARK_UP_RATE_PERC.Text==null? 0: double.Parse(txtMARK_UP_RATE_PERC.Text));
            objInvoiceInfo.POLICY_ID = int.Parse(GetPolicyID());
            objInvoiceInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());


           
            string strSystemID = GetSystemId();


            return objInvoiceInfo;

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
