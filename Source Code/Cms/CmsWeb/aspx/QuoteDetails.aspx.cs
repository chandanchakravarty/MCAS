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
using System.Xml.Xsl;
using System.Xml.XPath;
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
using Cms.BusinessLayer.BlQuote; 


namespace Cms.CmsWeb.aspx
{
    public partial class QuoteDetails : Cms.CmsWeb.cmsbase
    {
        int intCUSTOMER_ID;
        int intPOLICY_ID;
        int intPOLICY_VERSION_ID;
        int intQQ_ID;

        //Added By ashish-----------
        eRateEngine objeRateEngine;
        QuoteRequestType objQuoteRequestType;
        QuoteResponseType objQuoteResponseType;
        //------------------------------

        #region AJAX Methods
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchVehicleModelType(int MakeID)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();

                DataTable dt1 = new DataTable();
                try
                {
                    dt1 = ClsLookup.GetVehicleModelByMake(MakeID);
                }
                catch
                { }
                ds.Tables.Add(dt1.Copy());
                ds.Tables[0].TableName = "VEHICLE_MODEL";

                DataTable dt2 = new DataTable();
                try
                {
                    dt2 = ClsLookup.GetVehicleTypeByMake(MakeID);
                }
                catch
                { }
                ds.Tables.Add(dt2.Copy());
                ds.Tables[1].TableName = "VEHICLE_TYPE";

               

                return ds;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        private void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "134_0";
            Ajax.Utility.RegisterTypeForAjax(typeof(QuoteDetails));

            //btnBack.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            //btnBack.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;
            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
         
            btnGetQuote.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnGetQuote.PermissionString = gstrSecurityXML;

            //btnPrint.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            //btnPrint.PermissionString = gstrSecurityXML;

            SetSessionValues();

            btnGetQuote.Visible = false;

            string strQUOTENUM = Request.Params["QUOTE_NUM"];
            ClsQuickQuote objQQuote = new ClsQuickQuote();
            intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQUOTENUM);

            //Added by Ruchika on 5-Jan-2012 for TFS # 1000
            Session["QUOTE_ID"] = intQQ_ID;


            if (!Page.IsPostBack)
            {
                string strSystemID = GetSystemId();
                int strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID);

                hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
                SetCustomerID(hidCUSTOMER_ID.Value);
                intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

                hidPOLICY_ID.Value = Request.Params["POLICY_ID"];
                SetPolicyID(hidPOLICY_ID.Value);
                intPOLICY_ID = int.Parse(hidPOLICY_ID.Value);

                hidPOLICY_VERSION_ID.Value = Request.Params["POLICY_VERSION_ID"];
                SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
                intPOLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

                //string strQUOTENUM = Request.Params["QUOTE_NUM"];
                //ClsQuickQuote objQQuote = new ClsQuickQuote();
                //intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQUOTENUM);

                FillDropdowns();
                LoadData();

                
            }
        }

        private void SetSessionValues()
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
        }


        private void FillDropdowns()
        {
            int currYear = DateTime.Now.Year;
            //cmbYEAR_OF_REG.Items.Add("-Select-");
            for (int i = currYear; i > 1940 - 1; i--)
            {
                cmbYEAR_OF_REG.Items.Add(i.ToString());
            }
            cmbYEAR_OF_REG.Items.Insert(0, "");
            cmbYEAR_OF_REG.SelectedIndex = -1;

            cmbMAKE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VIN");
            cmbMAKE.DataTextField = "LookupDesc";
            cmbMAKE.DataValueField = "LookupID";
            cmbMAKE.DataBind();
            cmbMAKE.Items.Insert(0, "");
            cmbMAKE.SelectedIndex = -1;

            //cmbMODEL -- Ajax call

            //cmbVEHICLE_TYPE -- Ajax call

            //cmbNO_OF_DRIVERS.Items.Add("-Select-");
            for (int i = 1; i < 11; i++)
            {
                cmbNO_OF_DRIVERS.Items.Add(i.ToString());
            }
            cmbNO_OF_DRIVERS.Items.Insert(0, "");
            cmbNO_OF_DRIVERS.SelectedIndex = -1;

            cmbANY_CLAIM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbANY_CLAIM.DataTextField = "LookupDesc";
            cmbANY_CLAIM.DataValueField = "LookupID";
            cmbANY_CLAIM.DataBind();
            cmbANY_CLAIM.Items.Insert(0, "");

            cmbCOVERAGE_TYPE.DataSource = ClsLookup.GetLookupVehicleCoverages(int.Parse(GetLOBID()));
            cmbCOVERAGE_TYPE.DataTextField = "COV_DES";
            cmbCOVERAGE_TYPE.DataValueField = "COV_ID";
            cmbCOVERAGE_TYPE.DataBind();
            cmbCOVERAGE_TYPE.Items.Insert(0, "");
            
            cmbNO_CLAIM_DISCOUNT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbNO_CLAIM_DISCOUNT.DataTextField = "LookupDesc";
            cmbNO_CLAIM_DISCOUNT.DataValueField = "LookupID";
            cmbNO_CLAIM_DISCOUNT.DataBind();
            cmbNO_CLAIM_DISCOUNT.Items.Insert(0, "");

        }

        private void LoadData()
        {
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQuoteDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            DataSet dsCustomer = objQuoteDetails.FetchCustomerPolicyDetail(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID);

            DataTable dt = dsCustomer.Tables[0];

            if (dt.Rows.Count != 0)
            {
                string strEffDate = "";
                string strExpDate = "";
                if (dt.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    strEffDate = dt.Rows[0]["APP_EFFECTIVE_DATE"].ToString();
                }

                if (dt.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    strExpDate = dt.Rows[0]["APP_EXPIRATION_DATE"].ToString();
                }

                DateTime dtEffDate = DateTime.Parse(strEffDate);
                DateTime dtExpDate = DateTime.Parse(strExpDate);

                txtFROM_DAY.Text = dtEffDate.Day.ToString();
                txtFROM_MONTH.Text = dtEffDate.Month.ToString();
                txtFROM_YEAR.Text = dtEffDate.Year.ToString();

                txtTO_DAY.Text = dtExpDate.Day.ToString();
                txtTO_MONTH.Text = dtExpDate.Month.ToString();
                txtTO_YEAR.Text = dtExpDate.Year.ToString();
            }

            DataSet dsRate = objQuoteDetails.FetchVehicleRatingDetail(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID, intQQ_ID);

            DataTable dtRate = dsRate.Tables[0];

            if (dtRate.Rows.Count != 0)
            {
                if (dtRate.Rows[0]["YEAR_OF_REG"] != System.DBNull.Value)
                {
                    cmbYEAR_OF_REG.SelectedValue = dtRate.Rows[0]["YEAR_OF_REG"].ToString();
                }

                if (dtRate.Rows[0]["MAKE"] != System.DBNull.Value)
                {
                    cmbMAKE.SelectedValue = dtRate.Rows[0]["MAKE"].ToString();
                }


                DataSet dsModel = AjaxFetchVehicleModelType(int.Parse(dtRate.Rows[0]["MAKE"].ToString()));

                //for (int i = 0; i < dsModel.Tables[0].Rows.Count; i++)
                //{
                //    cmbMODEL.DataValueField = dsModel.Tables[0].Rows[i]["ID"].ToString();
                //    cmbMODEL.DataTextField = dsModel.Tables[0].Rows[i]["MODEL"].ToString();
                //}

                cmbMODEL.DataSource = dsModel.Tables[0];
                cmbMODEL.DataTextField = "MODEL";
                cmbMODEL.DataValueField = "ID";
                cmbMODEL.DataBind();
                cmbMODEL.Items.Insert(0, "");
                //cmbMODEL.SelectedIndex = -1;

                //for (int i = 0; i < dsModel.Tables[1].Rows.Count; i++)
                //{
                //    cmbVEHICLE_TYPE.DataValueField = dsModel.Tables[0].Rows[i]["ID"].ToString();
                //    cmbVEHICLE_TYPE.DataTextField = dsModel.Tables[0].Rows[i]["MODEL_TYPE"].ToString();
                //}

                cmbVEHICLE_TYPE.DataSource = dsModel.Tables[1];
                cmbVEHICLE_TYPE.DataTextField = "MODEL_TYPE";
                cmbVEHICLE_TYPE.DataValueField = "ID";
                cmbVEHICLE_TYPE.DataBind();
                cmbVEHICLE_TYPE.Items.Insert(0, "");

                if (dtRate.Rows[0]["MODEL"] != System.DBNull.Value)
                {
                    cmbMODEL.SelectedValue = dtRate.Rows[0]["MODEL"].ToString();
                    hidMODEL.Value = dtRate.Rows[0]["MODEL"].ToString();
                    cmbMODEL.SelectedIndex = cmbMODEL.Items.IndexOf(cmbMODEL.Items.FindByValue(cmbMODEL.SelectedValue));
                }

                if (dtRate.Rows[0]["MODEL_TYPE"] != System.DBNull.Value)
                {
                    cmbVEHICLE_TYPE.SelectedValue = dtRate.Rows[0]["MODEL_TYPE"].ToString();
                    hidMODEL_TYPE.Value = dtRate.Rows[0]["MODEL_TYPE"].ToString();
                    cmbVEHICLE_TYPE.SelectedIndex = cmbVEHICLE_TYPE.Items.IndexOf(cmbVEHICLE_TYPE.Items.FindByValue(cmbVEHICLE_TYPE.SelectedValue));
                }

                if (dtRate.Rows[0]["ENG_CAPACITY"] != System.DBNull.Value)
                {
                    txtENG_CAPACITY.Text = dtRate.Rows[0]["ENG_CAPACITY"].ToString();
                }

                if (dtRate.Rows[0]["NO_OF_DRIVERS"] != System.DBNull.Value)
                {
                    cmbNO_OF_DRIVERS.SelectedValue = dtRate.Rows[0]["NO_OF_DRIVERS"].ToString();
                }

                if (dtRate.Rows[0]["ANY_CLAIM"] != System.DBNull.Value)
                {
                    string strClaim = dtRate.Rows[0]["ANY_CLAIM"].ToString();
                    if (strClaim == "1")
                    {
                        cmbANY_CLAIM.SelectedValue = "10963";
                    }
                    else
                    {
                        cmbANY_CLAIM.SelectedValue = "10964";
                        //trClaim.Visible = false;
                    }
                }

                if (dtRate.Rows[0]["NO_OF_CLAIM"] != System.DBNull.Value)
                {
                    txtNO_OF_CLAIMS.Text = dtRate.Rows[0]["NO_OF_CLAIM"].ToString();
                }

                if (dtRate.Rows[0]["TOTAL_CLAIM_AMT"] != System.DBNull.Value)
                {
                    txtTOTAL_CLAIM_AMT.Text = dtRate.Rows[0]["TOTAL_CLAIM_AMT"].ToString();
                }

                if (dtRate.Rows[0]["COVERAGE_TYPE"] != System.DBNull.Value)
                {
                    cmbCOVERAGE_TYPE.SelectedValue = dtRate.Rows[0]["COVERAGE_TYPE"].ToString();
                }

                if (dtRate.Rows[0]["NO_CLAIM_DISCOUNT"] != System.DBNull.Value)
                {
                    //cmbNO_CLAIM_DISCOUNT.SelectedValue = dtRate.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();
                    string strClaimDisc = dtRate.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();
                    if (strClaimDisc == "1")
                    {
                        cmbNO_CLAIM_DISCOUNT.SelectedValue = "10963";
                    }
                    else
                    {
                        cmbNO_CLAIM_DISCOUNT.SelectedValue = "10964";
                        //trClaim.Visible = false;
                    }
                }

                btnGetQuote.Enabled = true;

            }
            else
            {
                btnGetQuote.Enabled = false;
            }

        }

        #region Web control events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Saving the values into the database
            SaveFormValue();

            rfvVEHICLE_TYPE.Visible = false;
            rfvMODEL.Visible = false;

            
        }

        protected void btnGetQuote_Click(object sender, EventArgs e)
        {
            //string _strRatingEngineUrl = "http://192.168.90.38/eRateEngine/eRateEngine.asmx";
            //// Saving the values into the database
            //string strRequestXml = GetQuoteRateXML();
            //string strResponseXml = "";
            //Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQQDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();

            //objQQDetails.UpdateRateXML(intCUSTOMER_ID,intPOLICY_ID,intPOLICY_VERSION_ID,intQQ_ID,strRequestXml,"");

            //XmlDataDocument objInputXml = new XmlDataDocument();
            //objInputXml.LoadXml(strRequestXml);

            //objQuoteRequestType = new QuoteRequestType();
            //objQuoteResponseType = new QuoteResponseType();
            //objeRateEngine = new eRateEngine(_strRatingEngineUrl);
            //objQuoteRequestType.CallOuterWebservice = false;
            //objQuoteRequestType.CreateOutputXMLFile = false;
            //objQuoteRequestType.GetInputXMLFromPath = false;
            //objQuoteRequestType.CallerApplication = "ICLOSE";
            //objQuoteRequestType.InputFilePath = "";
            //objQuoteRequestType.InputXMLOBJ = objInputXml;
                       
            ////---Second Phase
            
            //objQuoteResponseType = objeRateEngine.GetQuote(objQuoteRequestType);
            //if (objQuoteResponseType.ReturnStatus == "100")
            //{
            //    XmlDataDocument objOutputXml = new XmlDataDocument();
            //    objOutputXml.LoadXml(objQuoteResponseType.OutputXMLOBJ.OuterXml);

            //    strResponseXml = objOutputXml.OuterXml;

            //    //SAVE OUTPUT XML
            //    //objOutputXml.Save(_strSessionDirPath + "\\04_Product.xml");
            //    objQQDetails.UpdateRateXML(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID, intQQ_ID, "", strResponseXml);

            //    //------------------------------------------
            //}
            //else
            //{
            //    lblMessage.Text = "Error generating rate.Following error occured: " + objQuoteResponseType.ReturnMessage;
            //}

            //if (strResponseXml != "")
            //{
            //    string strPremium = GetPremiumFromXml(strResponseXml);
            //    objQQDetails.UpdateQQPremium(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID, strPremium);

            //    string inputXML = objQQDetails.GetQuickQuoteXML(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID);

            //    //string cssnum = "";
            //    //cssnum = "<HEADER" + " CSSNUM=\"" + GetColorScheme() + "\"";
            //    //inputXML = inputXML.Replace("<HEADER", cssnum);
            //    //inputXML = inputXML.Replace("&gt;", ">");
            //    //inputXML = inputXML.Replace("&lt;", "<");
            //    //inputXML = inputXML.Replace("\"", "'");
            //    //inputXML = inputXML.Replace("\r", "");
            //    //inputXML = inputXML.Replace("\n", "");
            //    //inputXML = inputXML.Replace("\t", "");
            //    //inputXML = inputXML.Replace("<br>", "<!--br-->");              
            //    //inputXML = inputXML.Replace("<input type='button' id='btnPrint' value='" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505") + "' onClick='printReport();' vAlign='bottom' class='clsButton'>", "<input type='button' id='btnPrint' value='" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1505") + "' onClick='printReport();' vAlign='bottom' class='clsButton'/>");
            //    //inputXML = inputXML.Replace("<META", "<!-- <META");
            //    //inputXML = inputXML.Replace("rel='stylesheet'>", "rel='stylesheet'>-->");
            //    //inputXML = inputXML.Replace("</html>", "");
            //    //inputXML = inputXML.Replace("</span>", "</span></html>");
            //    XmlDocument xDoc = new XmlDocument();
            //    xDoc.LoadXml(inputXML);

            //    string finalQuoteXSLPath = ClsCommon.GetKeyValueWithIP("FilePathQQRatingXML");
                                
            //    XslTransform tr = new XslTransform();
            //    tr.Load(finalQuoteXSLPath);

            //    XPathNavigator nav = ((IXPathNavigable)xDoc).CreateNavigator();
            //    StringWriter swReport = new StringWriter();
            //    tr.Transform(nav, null, swReport);

            //    string strPremiumXml = swReport.ToString();
            //    //return strPremiumXml;

            //    strPremiumXml = strPremiumXml.Replace("&gt;", ">");
            //    strPremiumXml = strPremiumXml.Replace("&lt;", "<");
            //    strPremiumXml = strPremiumXml.Replace("\"", "'");
            //    strPremiumXml = strPremiumXml.Replace("\r", "");
            //    strPremiumXml = strPremiumXml.Replace("\n", "");
            //    strPremiumXml = strPremiumXml.Replace("\t", "");

                //Response.Write(strPremiumXml);
                Response.Redirect("/cms/Policies/Aspx/QuickQuoteFrame.aspx?Level=3&CUSTOMER_ID=" + intCUSTOMER_ID + "&POLICY_ID=" + intPOLICY_ID + "&POLICY_VERSION_ID=" + intPOLICY_VERSION_ID + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + intQQ_ID);
                //Response.Redirect("/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID=" + intCUSTOMER_ID + "&POLICY_ID=" + intPOLICY_ID + "&POLICY_VERSION_ID=" + intPOLICY_VERSION_ID + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + intQQ_ID + "&CALLEDFROM=QQUOTE" + "&SHOW=" + strPremiumXml);
            }         

        private string GetPremiumFromXml(string strResponseXml)
        {
            string strTotalPremium = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strResponseXml);

            if (doc.InnerText != null)
            {
                XmlNode nodePremium = doc.SelectSingleNode("Product/PolicyHeader/PolicyPremiums/FullTermPremiums");

                if (nodePremium != null)
                {
                    XmlNode currentNode = nodePremium.SelectSingleNode("AssessmentAmount");
                    strTotalPremium = currentNode.InnerText.ToString();
                }
            }
            return strTotalPremium;
        }




        #endregion

        #region Save function

        private void SaveFormValue()
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
                Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQQDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
                objQQDetails.TransactionLogRequired = true;

                //Creating the Model object for holding the New data
                //Model.Client.ClsCustomerInfo objNewQuoteInfo = new Cms.Model.Client.ClsCustomerInfo();
                Model.Quote.ClsQuoteDetailsInfo objNewQuoteInfo = new Cms.Model.Quote.ClsQuoteDetailsInfo();



                //Creating the Model object for holding the Old data
                //Model.Client.ClsCustomerInfo objOldCustomer = new Cms.Model.Client.ClsCustomerInfo();
                Model.Quote.ClsQuoteDetailsInfo objOldQuoteInfo = new Cms.Model.Quote.ClsQuoteDetailsInfo();

                lblMessage.Visible = true;

                if (this.hid_ID.Value.ToUpper() == "NEW")
                {
                    //Mapping feild and Lebel to maintain the transction log into the database.
                    //objNewQuoteInfo.TransactLabel	=	MapTransactionLabel(objResourceMgr,this);

                    //objNewQuoteInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/client/aspx/AddCustomerQQ.aspx.resx");

                    //Setting  the Page details(all the control values) into the Model Object
                    //base.PopulateModelObject(this,objNewQuoteInfo);

                    objNewQuoteInfo = this.getFormValue();

                    //Setting those values into the Model object which are not in the page
                    objNewQuoteInfo.CUSTOMER_ID = int.Parse(GetCustomerID());
                    objNewQuoteInfo.POLICY_ID = int.Parse(GetPolicyID());
                    objNewQuoteInfo.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
                    hidCUSTOMER_ID.Value = GetCustomerID();
                    objNewQuoteInfo.CREATED_BY = int.Parse(GetUserId());
                    objNewQuoteInfo.CREATED_DATETIME = DateTime.Now;
                    string strQQID = GetQQ_ID();

                    ClsQuickQuote objQQuote = new ClsQuickQuote();
                    intQQ_ID = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, strQQID);
                    objNewQuoteInfo.QUOTE_ID = intQQ_ID;

                    SetExcessAmounts(objNewQuoteInfo);
                    //Agni
                    intRetVal = objQQDetails.Add(objNewQuoteInfo);


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

                        rfvMODEL.Visible = false;
                        rfvVEHICLE_TYPE.Visible = false;
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
                    btnGetQuote.Enabled = true;
                }
                //Update Customer
                else
                {
                    // Creating Business layer object to do processing
                    Cms.BusinessLayer.BlQuote.ClsQuoteDetails objCustomer1 = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();

                    //customer id for the record which is being updated
                    intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
                    SetCustomerID(hidCUSTOMER_ID.Value.ToString());


                    //Setting  the Page details(all the control values) into the Model Object
                    //base.PopulateModelObject(this,objNewQuoteInfo);

                    //Creating the Model object for holding the New data
                    Model.Quote.ClsQuoteDetailsInfo objNewQuoteInfo1 = getFormValue();

                    //Creating the Model object for holding the Old data
                    Model.Quote.ClsQuoteDetailsInfo objOldCustomer1 = new Model.Quote.ClsQuoteDetailsInfo();


                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldCustomer1, hidOldXML.Value);

                    //Setting those values into the Model object which are not in the page
                    //objNewQuoteInfo.CustomerId				=	intCUSTOMER_ID;
                    objNewQuoteInfo1.MODIFIED_BY = int.Parse(GetUserId());
                    objNewQuoteInfo1.LAST_UPDATED_DATETIME = DateTime.Now;

                    SetCustomerID(intCUSTOMER_ID.ToString());
                    //Setting those values into the Model object which are not in the page
                    objOldCustomer1.CUSTOMER_ID = intCUSTOMER_ID;

                    //intRetVal						=		objCustomer.Update(objOldCustomer,objNewQuoteInfo);
                    //Agni
                    intRetVal = objCustomer1.Update(objNewQuoteInfo1);

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

                LoadData();



            }
            catch (Exception ex)
            {

            }
        }


        protected void SetExcessAmounts(ClsQuoteDetailsInfo objNewQuoteInfo)
        {
            Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails objCustDetails = new Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails();
            DataSet dsCustomer = objCustDetails.FetchCustomerParticularDetail(intCUSTOMER_ID, intQQ_ID);
            DataTable dt = dsCustomer.Tables[0];            
            
            string strCustType = "";
            string strIsClaim = "";
            string strMIN_CAPACITY = "";
            string strMAX_CAPACITY = "";

            if (dt.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
            {
                if (dt.Rows[0]["CUSTOMER_TYPE"].ToString() == "114304")
                {
                    strCustType = "INDIVIDUAL";
                }
                else
                {
                    strCustType = "CORPORATE";
                }
            }            

            string strEngCapacity = objNewQuoteInfo.ENG_CAPACITY.ToString();

            if (strCustType == "INDIVIDUAL")
            {
                if (int.Parse(strEngCapacity) >= 0 && int.Parse(strEngCapacity) <= 1599)
                {
                    strMIN_CAPACITY = "0";
                    strMAX_CAPACITY = "1599";
                }
                else if (int.Parse(strEngCapacity) >= 1600 && int.Parse(strEngCapacity) <= 1999)
                {
                    strMIN_CAPACITY = "1600";
                    strMAX_CAPACITY = "1999";
                }
                else if (int.Parse(strEngCapacity) >= 2000 && int.Parse(strEngCapacity) <= 2999)
                {
                    strMIN_CAPACITY = "2000";
                    strMAX_CAPACITY = "2999";
                }
                else if (int.Parse(strEngCapacity) >= 3000 && int.Parse(strEngCapacity) <= 10000)
                {
                    strMIN_CAPACITY = "3000";
                    strMAX_CAPACITY = "10000";
                }
            }
            else if (strCustType == "CORPORATE")
            {
                if (int.Parse(strEngCapacity) >= 0 && int.Parse(strEngCapacity) <= 999)
                {
                    strMIN_CAPACITY = "0";
                    strMAX_CAPACITY = "999";
                }
                else if (int.Parse(strEngCapacity) >= 1000 && int.Parse(strEngCapacity) <= 1599)
                {
                    strMIN_CAPACITY = "1000";
                    strMAX_CAPACITY = "1599";
                }
                else if (int.Parse(strEngCapacity) >= 1600 && int.Parse(strEngCapacity) <= 1999)
                {
                    strMIN_CAPACITY = "1600";
                    strMAX_CAPACITY = "1999";
                }
                else if (int.Parse(strEngCapacity) >= 2000 && int.Parse(strEngCapacity) <= 2999)
                {
                    strMIN_CAPACITY = "2000";
                    strMAX_CAPACITY = "2999";
                }
                else if (int.Parse(strEngCapacity) >= 3000 && int.Parse(strEngCapacity) <= 10000)
                {
                    strMIN_CAPACITY = "3000";
                    strMAX_CAPACITY = "10000";
                }
            }


            if (objNewQuoteInfo.ANY_CLAIM == "1")
            {
                strIsClaim = "EXCESSAMOUNTCLAIM";
            }
            else
            {
                strIsClaim = "EXCESSAMOUNTNOCLAIM";
            }

            XmlDocument xFactorDoc = new XmlDocument();

            //string strFactorPath = System.Configuration.ConfigurationManager.AppSettings["MotorRateFactorXmlPath"];
            string strFactorPath = ClsCommon.GetKeyValueWithIP("MotorRateFactorXmlPath");

            xFactorDoc.Load(strFactorPath);

//            XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/Effective_Year[@STARTDATE <='"+ Year.ToString() + "'"+ " and @ENDDATE >='" + Year.ToString() +"'"+ "]/VehicleType[@ID='" + VehicleType + "'"+ "and @TYPE='COMP'"+"]/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]");
//<Effective_Year STARTDATE="1900" ENDDATE="1975">

            // "PRODUCTMASTER/PRODUCT/FACTOR/Effective_Year/NODE[@ID='" +strIsClaim+ "']/ATTRIBUTES[@ENG_CAPACITY_MIN <= '" +strEngCapacity+ "' and @ENG_CAPACITY_MAX >= '" +strEngCapacity+ "']/@NAMEDDRIVER"

            //XmlNodeList dataNodeList = xFactorDoc.SelectNodes("PRODUCTMASTER/PRODUCT/FACTOR/Effective_Year/NODE[@ID='"+ strIsClaim +"']/ATTRIBUTES[@TYPE='"+ strCustType +"']");
            XmlNode dataNode = xFactorDoc.SelectSingleNode("PRODUCTMASTER/PRODUCT/FACTOR/Effective_Year");
            //XmlNode dataNode = null;

            if (dataNode.SelectSingleNode("NODE[@ID='" + strIsClaim + "']/ATTRIBUTES[@TYPE='" + strCustType + "' and @ENG_CAPACITY_MIN<='" + strEngCapacity + "' and @ENG_CAPACITY_MAX>='" + strEngCapacity + "']") != null)
            {
                objNewQuoteInfo.NAMED_DRIVER_AMT = dataNode.SelectSingleNode("NODE[@ID='" + strIsClaim + "']/ATTRIBUTES[@TYPE='" + strCustType + "' and @ENG_CAPACITY_MIN<='" + strEngCapacity + "' and @ENG_CAPACITY_MAX>='" + strEngCapacity + "']/@NAMEDDRIVER").InnerText.ToString();
                objNewQuoteInfo.UNNAMED_DRIVER_AMT = dataNode.SelectSingleNode("NODE[@ID='" + strIsClaim + "']/ATTRIBUTES[@TYPE='" + strCustType + "' and @ENG_CAPACITY_MIN<='" + strEngCapacity + "' and @ENG_CAPACITY_MAX>='" + strEngCapacity + "']/@UNNAMEDDRIVER").InnerText.ToString();
            }

            //if (dataNodeList != null)
            //{
            //    foreach (XmlNode node in dataNodeList)
            //    {
            //        //if (node.SelectSingleNode("ATTRIBUTES[@ENG_CAPACITY_MIN<='" + strEngCapacity + "'"+ " and @ENG_CAPACITY_MAX>='" + strEngCapacity + "']") != null)
            //        //if (node.SelectSingleNode("ATTRIBUTES[@ENG_CAPACITY_MIN='" + strMIN_CAPACITY + "'" + " and @ENG_CAPACITY_MAX='" + strMAX_CAPACITY + "']") != null)
            //        if (node.SelectSingleNode("NODE[@ID='" + strIsClaim + "']/ATTRIBUTES[@ENG_CAPACITY_MIN<='" + strEngCapacity + "' and @ENG_CAPACITY_MAX>='" + strEngCapacity + "']/@NAMEDDRIVER") != null)
            //        {
            //            objNewQuoteInfo.NAMED_DRIVER_AMT = node.SelectSingleNode("ATTRIBUTES[@ENG_CAPACITY_MIN='" + strMIN_CAPACITY + "' @ENG_CAPACITY_MAX='" + strMAX_CAPACITY + "']/@NAMEDDRIVER").InnerText.ToString();
            //            objNewQuoteInfo.UNNAMED_DRIVER_AMT = node.SelectSingleNode("ATTRIBUTES[@ENG_CAPACITY_MIN='" + strMIN_CAPACITY + "' @ENG_CAPACITY_MAX='" + strMAX_CAPACITY + "']/@UNNAMEDDRIVER").InnerText.ToString();
            //        }
            //    }
            //}

            
        }

        /// <summary>
        /// Setting the page values in to the Customer object
        /// </summary>
        /// <returns></returns>
        private ClsQuoteDetailsInfo getFormValue()
        {
            Model.Quote.ClsQuoteDetailsInfo objCustomerInfo = new Model.Quote.ClsQuoteDetailsInfo();

            //objCustomerInfo.CUSTOMER_ID = 0;
            objCustomerInfo.YEAR_OF_REG = cmbYEAR_OF_REG.SelectedItem.Value;
            objCustomerInfo.MAKE = cmbMAKE.SelectedItem.Value;
            if (hidMODEL.Value != "")
            {
                objCustomerInfo.MODEL = hidMODEL.Value;
            }
            else
            {
                objCustomerInfo.MODEL = cmbMODEL.SelectedValue;//hidMODEL.Value;
            }

            if (hidMODEL_TYPE.Value != "")
            {
                objCustomerInfo.MODEL_TYPE = hidMODEL_TYPE.Value;
            }
            else
            {
                objCustomerInfo.MODEL_TYPE = cmbVEHICLE_TYPE.SelectedValue;//hidMODEL_TYPE.Value;
            }
            objCustomerInfo.ENG_CAPACITY = int.Parse(txtENG_CAPACITY.Text);
            objCustomerInfo.NO_OF_DRIVERS = int.Parse(cmbNO_OF_DRIVERS.SelectedItem.Value);

            //Added by Ruchika Chauhan on 4-Jan-2012 for TFS# 1000 
            Session["TotalDrivers"] = cmbNO_OF_DRIVERS.SelectedItem.Value;


            string strClaim = cmbANY_CLAIM.SelectedItem.Value.ToString();
            if (strClaim == "10963")
            {
                objCustomerInfo.ANY_CLAIM = "1";
            }
            else
            {
                objCustomerInfo.ANY_CLAIM = "0";
            }

            objCustomerInfo.NO_OF_CLAIM = int.Parse(txtNO_OF_CLAIMS.Text);
            objCustomerInfo.TOTAL_CLAIM_AMT = txtTOTAL_CLAIM_AMT.Text;

            objCustomerInfo.COVERAGE_TYPE = int.Parse(cmbCOVERAGE_TYPE.SelectedItem.Value.ToString());

            //objCustomerInfo.NO_CLAIM_DISCOUNT = cmbNO_CLAIM_DISCOUNT.SelectedItem.Value.ToString();
            string strClaimDisc = cmbNO_CLAIM_DISCOUNT.SelectedItem.Value.ToString();
            if (strClaimDisc == "10963")
            {
                objCustomerInfo.NO_CLAIM_DISCOUNT = "1";
            }
            else
            {
                objCustomerInfo.NO_CLAIM_DISCOUNT = "0";
            }

            return objCustomerInfo;

        }

        #endregion

        #region Get Quote        

        private string GetQuoteRateXML()
        {
            Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQuoteDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
            DataSet dsRate = objQuoteDetails.FetchDataForQQRqXml(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID);

            DataTable dtRate = dsRate.Tables[0];

            if (dtRate.Rows.Count != 0)
            {
                XmlDocument doc = new XmlDocument();
                //XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                //doc.AppendChild(docNode);

                XmlNode productsNode = doc.CreateElement("Product");
                doc.AppendChild(productsNode);

                XmlNode productCode = doc.CreateElement("ProductCode");
                productsNode.AppendChild(productCode);
                productCode.InnerText = "AUTO";

                XmlNode productname = doc.CreateElement("ProductName");
                productsNode.AppendChild(productname);
                productname.InnerText = "Auto Product";

                XmlNode sessionName = doc.CreateElement("SessionActionCode");
                productsNode.AppendChild(sessionName);
                productname.InnerText = "NewBusiness";

                XmlNode sessionURL = doc.CreateElement("EndSessionURL");
                productsNode.AppendChild(sessionURL);

                XmlNode messageStatus = doc.CreateElement("MessageStatus");
                productsNode.AppendChild(messageStatus);

                XmlNode messageCode = doc.CreateElement("MessageStatusCode");
                messageStatus.AppendChild(messageCode);

                XmlNode messageDesc = doc.CreateElement("MessageStatusDescription");
                messageStatus.AppendChild(messageDesc);

                XmlNode msgExCode = doc.CreateElement("ExtendedStatusCode");
                messageStatus.AppendChild(msgExCode);

                XmlNode msgStatusDesc = doc.CreateElement("ExtendedStatusDescription");
                messageStatus.AppendChild(msgStatusDesc);

                XmlNode policyHeader = doc.CreateElement("PolicyHeader");
                productsNode.AppendChild(policyHeader);

                XmlNode languageCode = doc.CreateElement("DefaultLanguageCode");
                policyHeader.AppendChild(languageCode);
                languageCode.InnerText = "en-US";

                XmlNode currencyCode = doc.CreateElement("DefaultCurrencyCode");
                policyHeader.AppendChild(currencyCode);
                currencyCode.InnerText = "USD";

                XmlNode indicator = doc.CreateElement("IsTestIndicator");
                policyHeader.AppendChild(indicator);

                XmlNode contractDetails = doc.CreateElement("ContractDetails");
                policyHeader.AppendChild(contractDetails);

                XmlNode cStateCode = doc.CreateElement("ContractStateCode");
                contractDetails.AppendChild(cStateCode);
                cStateCode.InnerText = "NewBusiness";

                XmlNode rStateCode = doc.CreateElement("RevisionStateCode");
                contractDetails.AppendChild(rStateCode);

                XmlNode bStateCode = doc.CreateElement("BindStateCode");
                contractDetails.AppendChild(bStateCode);
                bStateCode.InnerText = "New";

                XmlNode closeIndicator = doc.CreateElement("IsClosedIndicator");
                contractDetails.AppendChild(closeIndicator);

                XmlNode uki = doc.CreateElement("UKI");
                contractDetails.AppendChild(uki);

                XmlNode cPaymentInd = doc.CreateElement("CloseOnPaymentIndicator");
                contractDetails.AppendChild(cPaymentInd);

                XmlNode cActionCode = doc.CreateElement("StartContractActionCode");
                contractDetails.AppendChild(cActionCode);

                XmlNode rActionCode = doc.CreateElement("StartRevisionActionCode");
                contractDetails.AppendChild(rActionCode);

                XmlNode renewInd = doc.CreateElement("LapseOnRenewalIndicator");
                contractDetails.AppendChild(renewInd);

                XmlNode insuranceTerm = doc.CreateElement("PeriodOfInsurance");
                policyHeader.AppendChild(insuranceTerm);

                XmlNode inceptionDate = doc.CreateElement("InceptionDateTime");
                insuranceTerm.AppendChild(inceptionDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    inceptionDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode effectiveDate = doc.CreateElement("EffectiveDateTime");
                insuranceTerm.AppendChild(effectiveDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    effectiveDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode expiryDate = doc.CreateElement("ExpiryDateTime");
                insuranceTerm.AppendChild(expiryDate); // To be filled from DB

                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    expiryDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString()).ToShortDateString();
                }

                XmlNode insproductCode = doc.CreateElement("InsurerProductCode");
                policyHeader.AppendChild(insproductCode);
                insproductCode.InnerText = "AUTO";

                XmlNode accNumId = doc.CreateElement("AccountNumberId");
                policyHeader.AppendChild(accNumId);

                XmlNode policyPremium1 = doc.CreateElement("PolicyPremiums");
                policyHeader.AppendChild(policyPremium1);

                XmlNode fullTermP1 = doc.CreateElement("FullTermPremiums");
                policyPremium1.AppendChild(fullTermP1);

                //
                XmlNode assessAmtFTP1 = doc.CreateElement("AssessmentAmount");
                fullTermP1.AppendChild(assessAmtFTP1);
                assessAmtFTP1.InnerText = "0.00";

                XmlNode fireServAmtFTP1 = doc.CreateElement("FireServicesLevyAmount");
                fullTermP1.AppendChild(fireServAmtFTP1);
                fireServAmtFTP1.InnerText = "0.00";

                XmlNode earthQkAmtFTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                fullTermP1.AppendChild(earthQkAmtFTP1);
                earthQkAmtFTP1.InnerText = "0.00";

                XmlNode stampAmtFTP1 = doc.CreateElement("StampDutyAmount");
                fullTermP1.AppendChild(stampAmtFTP1);
                stampAmtFTP1.InnerText = "0.00";

                XmlNode taxAmtFTP1 = doc.CreateElement("TaxAmount");
                fullTermP1.AppendChild(taxAmtFTP1);
                stampAmtFTP1.InnerText = "0.00";

                XmlNode commAmtFTP1 = doc.CreateElement("CommissionAmount");
                fullTermP1.AppendChild(commAmtFTP1);
                commAmtFTP1.InnerText = "0.00";

                XmlNode commTaxAmtFTP1 = doc.CreateElement("CommissionTaxAmount");
                fullTermP1.AppendChild(commTaxAmtFTP1);
                commTaxAmtFTP1.InnerText = "0.00";

                XmlNode commPctFTP1 = doc.CreateElement("CommissionPercent");
                fullTermP1.AppendChild(commPctFTP1);
                commPctFTP1.InnerText = "0.00";

                XmlNode uwFeeAmtFTP1 = doc.CreateElement("UnderwritingFeeAmount");
                fullTermP1.AppendChild(uwFeeAmtFTP1);
                uwFeeAmtFTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtFTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                fullTermP1.AppendChild(uwFeetaxAmtFTP1);
                uwFeetaxAmtFTP1.InnerText = "0.00";

                XmlNode interFeeAmtFTP1 = doc.CreateElement("IntermediaryFeeAmount");
                fullTermP1.AppendChild(interFeeAmtFTP1);
                interFeeAmtFTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtFTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                fullTermP1.AppendChild(interFeeTaxAmtFTP1);
                interFeeTaxAmtFTP1.InnerText = "0.00";

                XmlNode interDiscAmtFTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                fullTermP1.AppendChild(interDiscAmtFTP1);
                interDiscAmtFTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtFTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                fullTermP1.AppendChild(interDiscTaxAmtFTP1);
                interDiscTaxAmtFTP1.InnerText = "0.00";
                //

                XmlNode currTermP1 = doc.CreateElement("CurrentTermPremiums");
                policyPremium1.AppendChild(currTermP1);
                //
                XmlNode assessAmtCTP1 = doc.CreateElement("AssessmentAmount");
                currTermP1.AppendChild(assessAmtCTP1);
                assessAmtCTP1.InnerText = "0.00";

                XmlNode fireServAmtCTP1 = doc.CreateElement("FireServicesLevyAmount");
                currTermP1.AppendChild(fireServAmtCTP1);
                fireServAmtCTP1.InnerText = "0.00";

                XmlNode earthQkAmtCTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                currTermP1.AppendChild(earthQkAmtCTP1);
                earthQkAmtCTP1.InnerText = "0.00";

                XmlNode stampAmtCTP1 = doc.CreateElement("StampDutyAmount");
                currTermP1.AppendChild(stampAmtCTP1);
                stampAmtCTP1.InnerText = "0.00";

                XmlNode taxAmtCTP1 = doc.CreateElement("TaxAmount");
                currTermP1.AppendChild(taxAmtCTP1);
                taxAmtCTP1.InnerText = "0.00";

                XmlNode commAmtCTP1 = doc.CreateElement("CommissionAmount");
                currTermP1.AppendChild(commAmtCTP1);
                commAmtCTP1.InnerText = "0.00";

                XmlNode commTaxAmtCTP1 = doc.CreateElement("CommissionTaxAmount");
                currTermP1.AppendChild(commTaxAmtCTP1);
                commTaxAmtCTP1.InnerText = "0.00";

                XmlNode commPctCTP1 = doc.CreateElement("CommissionPercent");
                currTermP1.AppendChild(commPctCTP1);
                commPctCTP1.InnerText = "0.00";

                XmlNode uwFeeAmtCTP1 = doc.CreateElement("UnderwritingFeeAmount");
                currTermP1.AppendChild(uwFeeAmtCTP1);
                uwFeeAmtCTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtCTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                currTermP1.AppendChild(uwFeetaxAmtCTP1);
                uwFeetaxAmtCTP1.InnerText = "0.00";

                XmlNode interFeeAmtCTP1 = doc.CreateElement("IntermediaryFeeAmount");
                currTermP1.AppendChild(interFeeAmtCTP1);
                interFeeAmtCTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtCTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                currTermP1.AppendChild(interFeeTaxAmtCTP1);
                interFeeTaxAmtCTP1.InnerText = "0.00";

                XmlNode interDiscAmtCTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                currTermP1.AppendChild(interDiscAmtCTP1);
                interDiscAmtCTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtCTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                currTermP1.AppendChild(interDiscTaxAmtCTP1);
                interDiscTaxAmtCTP1.InnerText = "0.00";
                //

                XmlNode TransP1 = doc.CreateElement("TransactionPremiums");
                policyPremium1.AppendChild(TransP1);

                //
                XmlNode assessAmtTP1 = doc.CreateElement("AssessmentAmount");
                TransP1.AppendChild(assessAmtTP1);
                assessAmtTP1.InnerText = "0.00";

                XmlNode fireServAmtTP1 = doc.CreateElement("FireServicesLevyAmount");
                TransP1.AppendChild(fireServAmtTP1);
                fireServAmtTP1.InnerText = "0.00";

                XmlNode earthQkAmtTP1 = doc.CreateElement("EarthQuakeLevyAmount");
                TransP1.AppendChild(earthQkAmtTP1);
                earthQkAmtTP1.InnerText = "0.00";

                XmlNode stampAmtTP1 = doc.CreateElement("StampDutyAmount");
                TransP1.AppendChild(stampAmtTP1);
                stampAmtTP1.InnerText = "0.00";

                XmlNode taxAmtTP1 = doc.CreateElement("TaxAmount");
                TransP1.AppendChild(taxAmtTP1);
                taxAmtTP1.InnerText = "0.00";

                XmlNode commAmtTP1 = doc.CreateElement("CommissionAmount");
                TransP1.AppendChild(commAmtTP1);
                commAmtTP1.InnerText = "0.00";

                XmlNode commTaxAmtTP1 = doc.CreateElement("CommissionTaxAmount");
                TransP1.AppendChild(commTaxAmtTP1);
                commTaxAmtTP1.InnerText = "0.00";

                XmlNode commPctTP1 = doc.CreateElement("CommissionPercent");
                TransP1.AppendChild(commPctTP1);
                commPctTP1.InnerText = "0.00";

                XmlNode uwFeeAmtTP1 = doc.CreateElement("UnderwritingFeeAmount");
                TransP1.AppendChild(uwFeeAmtTP1);
                uwFeeAmtTP1.InnerText = "0.00";

                XmlNode uwFeetaxAmtTP1 = doc.CreateElement("UnderwritingFeeTaxAmount");
                TransP1.AppendChild(uwFeetaxAmtTP1);
                uwFeetaxAmtTP1.InnerText = "0.00";

                XmlNode interFeeAmtTP1 = doc.CreateElement("IntermediaryFeeAmount");
                TransP1.AppendChild(interFeeAmtTP1);
                interFeeAmtTP1.InnerText = "0.00";

                XmlNode interFeeTaxAmtTP1 = doc.CreateElement("IntermediaryFeeTaxAmount");
                TransP1.AppendChild(interFeeTaxAmtTP1);
                interFeeTaxAmtTP1.InnerText = "0.00";

                XmlNode interDiscAmtTP1 = doc.CreateElement("IntermediaryDiscountAmount");
                TransP1.AppendChild(interDiscAmtTP1);
                interDiscAmtTP1.InnerText = "0.00";

                XmlNode interDiscTaxAmtTP1 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                TransP1.AppendChild(interDiscTaxAmtTP1);
                interDiscTaxAmtTP1.InnerText = "0.00";
                //

                XmlNode billpartyCode = doc.CreateElement("BillingPartyCode");
                policyHeader.AppendChild(billpartyCode);

                XmlNode billMethodCode = doc.CreateElement("BillingMethodCode");
                policyHeader.AppendChild(billMethodCode);

                XmlNode createDate = doc.CreateElement("CreatedDateTime");
                policyHeader.AppendChild(createDate);

                XmlNode createdBy = doc.CreateElement("CreatedBy");
                policyHeader.AppendChild(createdBy);

                XmlNode lastSaveDate = doc.CreateElement("LastSavedDateTime");
                policyHeader.AppendChild(lastSaveDate);

                XmlNode lastSavedBy = doc.CreateElement("LastSavedBy");
                policyHeader.AppendChild(lastSavedBy);

                XmlNode nodeLOBs = doc.CreateElement("LOBs");
                productsNode.AppendChild(nodeLOBs);

                XmlNode nodeLOB = doc.CreateElement("LOB");
                nodeLOBs.AppendChild(nodeLOB);

                XmlNode nodeLOBCode = doc.CreateElement("LOBCode");
                nodeLOB.AppendChild(nodeLOBCode);
                nodeLOBCode.InnerText = "Vehicle";

                XmlNode nodeLOBId = doc.CreateElement("LOBID");
                nodeLOB.AppendChild(nodeLOBId);
                nodeLOBId.InnerText = "6";

                XmlNode nodeLOBVersion = doc.CreateElement("LOBVersion");
                nodeLOB.AppendChild(nodeLOBVersion);

                XmlNode nodeLOBName = doc.CreateElement("LOBName");
                nodeLOB.AppendChild(nodeLOBName);
                nodeLOBName.InnerText = "Vehicle Auto Product";

                XmlNode nodeLOBCoverCode = doc.CreateElement("CoverCode");
                nodeLOB.AppendChild(nodeLOBCoverCode);


                XmlNode policyPremium2 = doc.CreateElement("PolicyPremiums");
                nodeLOB.AppendChild(policyPremium2);

                XmlNode fullTermP2 = doc.CreateElement("FullTermPremiums");
                policyPremium2.AppendChild(fullTermP2);

                //
                XmlNode assessAmtFTP2 = doc.CreateElement("AssessmentAmount");
                fullTermP2.AppendChild(assessAmtFTP2);
                assessAmtFTP2.InnerText = "0.00";

                XmlNode fireServAmtFTP2 = doc.CreateElement("FireServicesLevyAmount");
                fullTermP2.AppendChild(fireServAmtFTP2);
                fireServAmtFTP2.InnerText = "0.00";

                XmlNode earthQkAmtFTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                fullTermP2.AppendChild(earthQkAmtFTP2);
                earthQkAmtFTP2.InnerText = "0.00";

                XmlNode stampAmtFTP2 = doc.CreateElement("StampDutyAmount");
                fullTermP2.AppendChild(stampAmtFTP2);
                stampAmtFTP2.InnerText = "0.00";

                XmlNode taxAmtFTP2 = doc.CreateElement("TaxAmount");
                fullTermP2.AppendChild(taxAmtFTP2);
                taxAmtFTP2.InnerText = "0.00";

                XmlNode commAmtFTP2 = doc.CreateElement("CommissionAmount");
                fullTermP2.AppendChild(commAmtFTP2);
                commAmtFTP2.InnerText = "0.00";

                XmlNode commTaxAmtFTP2 = doc.CreateElement("CommissionTaxAmount");
                fullTermP2.AppendChild(commTaxAmtFTP2);
                commTaxAmtFTP2.InnerText = "0.00";

                XmlNode commPctFTP2 = doc.CreateElement("CommissionPercent");
                fullTermP2.AppendChild(commPctFTP2);
                commPctFTP2.InnerText = "0.00";

                XmlNode uwFeeAmtFTP2 = doc.CreateElement("UnderwritingFeeAmount");
                fullTermP2.AppendChild(uwFeeAmtFTP2);
                uwFeeAmtFTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtFTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                fullTermP2.AppendChild(uwFeetaxAmtFTP2);
                uwFeetaxAmtFTP2.InnerText = "0.00";

                XmlNode interFeeAmtFTP2 = doc.CreateElement("IntermediaryFeeAmount");
                fullTermP2.AppendChild(interFeeAmtFTP2);
                interFeeAmtFTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtFTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                fullTermP2.AppendChild(interFeeTaxAmtFTP2);
                interFeeTaxAmtFTP2.InnerText = "0.00";

                XmlNode interDiscAmtFTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                fullTermP2.AppendChild(interDiscAmtFTP2);
                interDiscAmtFTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtFTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                fullTermP2.AppendChild(interDiscTaxAmtFTP2);
                interDiscTaxAmtFTP2.InnerText = "0.00";
                //

                XmlNode currTermP2 = doc.CreateElement("CurrentTermPremiums");
                policyPremium2.AppendChild(currTermP2);
                //
                XmlNode assessAmtCTP2 = doc.CreateElement("AssessmentAmount");
                currTermP2.AppendChild(assessAmtCTP2);
                assessAmtCTP2.InnerText = "0.00";

                XmlNode fireServAmtCTP2 = doc.CreateElement("FireServicesLevyAmount");
                currTermP2.AppendChild(fireServAmtCTP2);
                fireServAmtCTP2.InnerText = "0.00";

                XmlNode earthQkAmtCTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                currTermP2.AppendChild(earthQkAmtCTP2);
                earthQkAmtCTP2.InnerText = "0.00";

                XmlNode stampAmtCTP2 = doc.CreateElement("StampDutyAmount");
                currTermP2.AppendChild(stampAmtCTP2);
                stampAmtCTP2.InnerText = "0.00";

                XmlNode taxAmtCTP2 = doc.CreateElement("TaxAmount");
                currTermP2.AppendChild(taxAmtCTP2);
                taxAmtCTP2.InnerText = "0.00";

                XmlNode commAmtCTP2 = doc.CreateElement("CommissionAmount");
                currTermP2.AppendChild(commAmtCTP2);
                commAmtCTP2.InnerText = "0.00";

                XmlNode commTaxAmtCTP2 = doc.CreateElement("CommissionTaxAmount");
                currTermP2.AppendChild(commTaxAmtCTP2);
                commTaxAmtCTP2.InnerText = "0.00";

                XmlNode commPctCTP2 = doc.CreateElement("CommissionPercent");
                currTermP2.AppendChild(commPctCTP2);
                commPctCTP2.InnerText = "0.00";

                XmlNode uwFeeAmtCTP2 = doc.CreateElement("UnderwritingFeeAmount");
                currTermP2.AppendChild(uwFeeAmtCTP2);
                uwFeeAmtCTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtCTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                currTermP2.AppendChild(uwFeetaxAmtCTP2);
                uwFeetaxAmtCTP2.InnerText = "0.00";

                XmlNode interFeeAmtCTP2 = doc.CreateElement("IntermediaryFeeAmount");
                currTermP2.AppendChild(interFeeAmtCTP2);
                interFeeAmtCTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtCTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                currTermP2.AppendChild(interFeeTaxAmtCTP2);
                interFeeTaxAmtCTP2.InnerText = "0.00";

                XmlNode interDiscAmtCTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                currTermP2.AppendChild(interDiscAmtCTP2);
                interDiscAmtCTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtCTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                currTermP2.AppendChild(interDiscTaxAmtCTP2);
                interDiscTaxAmtCTP2.InnerText = "0.00";
                //

                XmlNode TransP2 = doc.CreateElement("TransactionPremiums");
                policyPremium2.AppendChild(TransP2);

                //
                XmlNode assessAmtTP2 = doc.CreateElement("AssessmentAmount");
                TransP2.AppendChild(assessAmtTP2);
                assessAmtTP2.InnerText = "0.00";

                XmlNode fireServAmtTP2 = doc.CreateElement("FireServicesLevyAmount");
                TransP2.AppendChild(fireServAmtTP2);
                fireServAmtTP2.InnerText = "0.00";

                XmlNode earthQkAmtTP2 = doc.CreateElement("EarthQuakeLevyAmount");
                TransP2.AppendChild(earthQkAmtTP2);
                earthQkAmtTP2.InnerText = "0.00";

                XmlNode stampAmtTP2 = doc.CreateElement("StampDutyAmount");
                TransP2.AppendChild(stampAmtTP2);
                stampAmtTP2.InnerText = "0.00";

                XmlNode taxAmtTP2 = doc.CreateElement("TaxAmount");
                TransP2.AppendChild(taxAmtTP2);
                taxAmtTP2.InnerText = "0.00";

                XmlNode commAmtTP2 = doc.CreateElement("CommissionAmount");
                TransP2.AppendChild(commAmtTP2);
                commAmtTP2.InnerText = "0.00";

                XmlNode commTaxAmtTP2 = doc.CreateElement("CommissionTaxAmount");
                TransP2.AppendChild(commTaxAmtTP2);
                commTaxAmtTP2.InnerText = "0.00";

                XmlNode commPctTP2 = doc.CreateElement("CommissionPercent");
                TransP2.AppendChild(commPctTP2);
                commPctTP2.InnerText = "0.00";

                XmlNode uwFeeAmtTP2 = doc.CreateElement("UnderwritingFeeAmount");
                TransP2.AppendChild(uwFeeAmtTP2);
                uwFeeAmtTP2.InnerText = "0.00";

                XmlNode uwFeetaxAmtTP2 = doc.CreateElement("UnderwritingFeeTaxAmount");
                TransP2.AppendChild(uwFeetaxAmtTP2);
                uwFeetaxAmtTP2.InnerText = "0.00";

                XmlNode interFeeAmtTP2 = doc.CreateElement("IntermediaryFeeAmount");
                TransP2.AppendChild(interFeeAmtTP2);
                interFeeAmtTP2.InnerText = "0.00";

                XmlNode interFeeTaxAmtTP2 = doc.CreateElement("IntermediaryFeeTaxAmount");
                TransP2.AppendChild(interFeeTaxAmtTP2);
                interFeeTaxAmtTP2.InnerText = "0.00";

                XmlNode interDiscAmtTP2 = doc.CreateElement("IntermediaryDiscountAmount");
                TransP2.AppendChild(interDiscAmtTP2);
                interDiscAmtTP2.InnerText = "0.00";

                XmlNode interDiscTaxAmtTP2 = doc.CreateElement("IntermediaryDiscountTaxAmount");
                TransP2.AppendChild(interDiscTaxAmtTP2);
                interDiscTaxAmtTP2.InnerText = "0.00";
                //

                XmlNode productRiskData = doc.CreateElement("ProductRiskData");
                nodeLOB.AppendChild(productRiskData);

                XmlNode nodeQQInfo = doc.CreateElement("QuickQuoteInformation");
                productRiskData.AppendChild(nodeQQInfo);

                XmlNode nodePolLOB = doc.CreateElement("POLICY_LOB");
                nodeQQInfo.AppendChild(nodePolLOB);

                if (dtRate.Rows[0]["POLICY_LOB"] != System.DBNull.Value)
                {
                    int LOB_UID = int.Parse(dtRate.Rows[0]["POLICY_LOB"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(LOB_UID);
                        nodePolLOB.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                    
                }

                XmlNode nodePOLCurr = doc.CreateElement("PolicyCurrencydropdown");
                nodeQQInfo.AppendChild(nodePOLCurr);

                if (dtRate.Rows[0]["POLICY_CURRENCY"] != System.DBNull.Value)
                {
                    int POL_CURR = int.Parse(dtRate.Rows[0]["POLICY_CURRENCY"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(POL_CURR);
                        nodePOLCurr.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeTerm = doc.CreateElement("APP_TERMS");
                nodeQQInfo.AppendChild(nodeTerm);

                if (dtRate.Rows[0]["APP_TERMS"] != System.DBNull.Value)
                {
                    nodeTerm.InnerText = dtRate.Rows[0]["APP_TERMS"].ToString() + " " + "Months";
                }

                XmlNode nodeEffDate = doc.CreateElement("APP_EFFECTIVE_DATE");
                nodeQQInfo.AppendChild(nodeEffDate);
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    nodeEffDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeExpDate = doc.CreateElement("APP_EXPIRATION_DATE");
                nodeQQInfo.AppendChild(nodeExpDate);
                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    nodeExpDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeBillType = doc.CreateElement("BILL_TYPE_ID");
                nodeQQInfo.AppendChild(nodeBillType);
                if (dtRate.Rows[0]["BILL_TYPE"] != System.DBNull.Value)
                {
                    string BillCode = dtRate.Rows[0]["BILL_TYPE"].ToString();
                    int BILL_UID = 0;
                    switch (BillCode)
                    {
                        case "AB":
                            BILL_UID = 8459;
                            break;
                        case "DB":
                            BILL_UID = 8460;
                            break;
                        case "DM":
                            BILL_UID = 11150;
                            break;
                        case "DI":
                            BILL_UID = 11191;
                            break;
                        case "MB":
                            BILL_UID = 11276;
                            break;
                        case "AM":
                            BILL_UID = 11277;
                            break;
                        case "IM":
                            BILL_UID = 11278;
                            break;
                        default:
                            BILL_UID = 8459;
                            break;
                    }
                    DataTable dt = new DataTable();
                    try
                    {                        
                        dt = ClsLookup.GetLookupValueFromUniqueID(BILL_UID);
                        nodeBillType.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeInstallType = doc.CreateElement("INSTALL_PLAN_ID");
                nodeQQInfo.AppendChild(nodeInstallType);
                if (dtRate.Rows[0]["INSTALL_PLAN_ID"] != System.DBNull.Value)
                {
                    //nodeInstallType.InnerText = dtRate.Rows[0]["INSTALL_PLAN_ID"].ToString();
                    nodeInstallType.InnerText = "Full Pay Plan (FULLPAY)-Check";
                }

                XmlNode nodeMultiple = doc.CreateElement("MULTIPLIER");
                nodeQQInfo.AppendChild(nodeMultiple);

                XmlNode nodeClientCode = doc.CreateElement("eProfessionalClientCode");
                nodeQQInfo.AppendChild(nodeClientCode);

                XmlNode nodeCustomerType = doc.CreateElement("CUSTOMER_TYPE");
                nodeQQInfo.AppendChild(nodeCustomerType);
                if (dtRate.Rows[0]["CUSTOMER_TYPE"] != System.DBNull.Value)
                {
                    int custTypeID = int.Parse(dtRate.Rows[0]["CUSTOMER_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(custTypeID);
                        nodeCustomerType.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeFName = doc.CreateElement("FirstName");
                nodeQQInfo.AppendChild(nodeFName);
                if (dtRate.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                {
                    nodeFName.InnerText = dtRate.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                }

                XmlNode nodeMName = doc.CreateElement("MiddleName");
                nodeQQInfo.AppendChild(nodeMName);
                if (dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                {
                    nodeMName.InnerText = dtRate.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                }

                XmlNode nodeLName = doc.CreateElement("LastName");
                nodeQQInfo.AppendChild(nodeLName);
                if (dtRate.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                {
                    nodeLName.InnerText = dtRate.Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                }

                XmlNode nodeGender = doc.CreateElement("GENDER");
                nodeQQInfo.AppendChild(nodeGender);
                if (dtRate.Rows[0]["GENDER"] != System.DBNull.Value)
                {
                    string strGender = dtRate.Rows[0]["GENDER"].ToString();
                    if (strGender == "M")
                    {
                        nodeGender.InnerText = "Male";
                    }
                    else
                    {
                        nodeGender.InnerText = "Female";
                    }
                }

                XmlNode nodeDOB = doc.CreateElement("DOB");
                nodeQQInfo.AppendChild(nodeDOB);
                if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    nodeDOB.InnerText = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString()).ToShortDateString();
                }

                XmlNode nodeNationality = doc.CreateElement("NationalityExcludeForeigner");
                nodeQQInfo.AppendChild(nodeNationality);
                if (dtRate.Rows[0]["NATIONALITY"] != System.DBNull.Value)
                {
                    string strCountry = dtRate.Rows[0]["NATIONALITY"].ToString();
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetCountryName(strCountry);
                        nodeNationality.InnerText = dt.Rows[0]["COUNTRY_NAME"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeOccupationType = doc.CreateElement("OCCUPATION");
                nodeQQInfo.AppendChild(nodeOccupationType);
                if (dtRate.Rows[0]["IS_HOME_EMPLOYEE"] != System.DBNull.Value)
                {
                    string strOCCType = dtRate.Rows[0]["IS_HOME_EMPLOYEE"].ToString();
                    if (strOCCType=="1")
                    {
                        nodeOccupationType.InnerText = "Indoor";
                    }
                    else
                    {
                        nodeOccupationType.InnerText = "Outdoor";
                    }
                }

                XmlNode nodeOccupation = doc.CreateElement("OccupationList");
                nodeQQInfo.AppendChild(nodeOccupation);
                if (dtRate.Rows[0]["CUSTOMER_OCCU"] != System.DBNull.Value)
                {
                    int strOccId = int.Parse(dtRate.Rows[0]["CUSTOMER_OCCU"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(strOccId);
                        nodeOccupation.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeDriveExp = doc.CreateElement("DRIVING_EXP");
                nodeQQInfo.AppendChild(nodeDriveExp);
                if (dtRate.Rows[0]["DRIVER_EXP_YEAR"] != System.DBNull.Value)
                {
                    nodeDriveExp.InnerText = dtRate.Rows[0]["DRIVER_EXP_YEAR"].ToString();
                }

                XmlNode nodeExistNCD = doc.CreateElement("EXISTING_NCD");
                nodeQQInfo.AppendChild(nodeExistNCD);
                if (dtRate.Rows[0]["EXISTING_NCD"] != System.DBNull.Value)
                {
                    string EXNCD = dtRate.Rows[0]["EXISTING_NCD"].ToString();
                    nodeExistNCD.InnerText = EXNCD.Remove(EXNCD.IndexOf("%"));
                }

                XmlNode nodeAnyClaim = doc.CreateElement("AnyClaimmade");
                nodeQQInfo.AppendChild(nodeAnyClaim);
                if (dtRate.Rows[0]["ANY_CLAIM"] != System.DBNull.Value)
                {
                    string strIsClaim = dtRate.Rows[0]["ANY_CLAIM"].ToString();
                    if (strIsClaim.Trim() == "1")
                    {
                        nodeAnyClaim.InnerText = "Yes";
                    }
                    else
                    {
                        nodeAnyClaim.InnerText = "No";
                    }
                }

                XmlNode nodeNCDLESS10 = doc.CreateElement("Isyourexisting");
                nodeQQInfo.AppendChild(nodeNCDLESS10);
                if (dtRate.Rows[0]["EXIST_NCD_LESS_10"] != System.DBNull.Value)
                {
                    string NCDLess = dtRate.Rows[0]["EXIST_NCD_LESS_10"].ToString();
                    if (NCDLess.Trim() == "1")
                    {
                        nodeNCDLESS10.InnerText = "Yes";
                    }
                    else
                    {
                        nodeNCDLESS10.InnerText = "No";
                    }
                }

                XmlNode nodeDemerit = doc.CreateElement("DemeritsPointsFreeDiscounts");
                nodeQQInfo.AppendChild(nodeDemerit);
                if (dtRate.Rows[0]["DEMERIT_DISCOUNT"] != System.DBNull.Value)
                {
                    string strDemerit = dtRate.Rows[0]["DEMERIT_DISCOUNT"].ToString();
                    if (strDemerit.Trim() == "1")
                    {
                        nodeDemerit.InnerText = "Yes";
                    }
                    else
                    {
                        nodeDemerit.InnerText = "No";
                    }
                }

                XmlNode nodeDriverAge = doc.CreateElement("Driver_AGE");
                nodeQQInfo.AppendChild(nodeDriverAge);
                
                
                int yearDOB = 0;
                int yearEff = 0;
                if (dtRate.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    DateTime dtDOB = Convert.ToDateTime(dtRate.Rows[0]["DATE_OF_BIRTH"].ToString());
                    yearDOB = dtDOB.Year;
                }
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    DateTime dtEff = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                    yearEff = dtEff.Year;
                }

                int dAge = yearEff - yearDOB;
                nodeDriverAge.InnerText = dAge.ToString();



                XmlNode nodeRiskInfo = doc.CreateElement("RiskInformation");
                productRiskData.AppendChild(nodeRiskInfo);

                XmlNode nodeYearReg = doc.CreateElement("YEAR_OF_REG");
                nodeRiskInfo.AppendChild(nodeYearReg);
                if (dtRate.Rows[0]["YEAR_OF_REG"] != System.DBNull.Value)
                {
                    nodeYearReg.InnerText = dtRate.Rows[0]["YEAR_OF_REG"].ToString();
                }

                XmlNode nodeMake = doc.CreateElement("MAKE");
                nodeRiskInfo.AppendChild(nodeMake);
                if (dtRate.Rows[0]["MAKE"] != System.DBNull.Value)
                {
                    int makeID = int.Parse(dtRate.Rows[0]["MAKE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetLookupValueFromUniqueID(makeID);
                        nodeMake.InnerText = dt.Rows[0]["LOOKUP_VALUE_DESC"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeModel = doc.CreateElement("MODEL");
                nodeRiskInfo.AppendChild(nodeModel);
                if (dtRate.Rows[0]["MODEL"] != System.DBNull.Value)
                {
                    int ModelID = int.Parse(dtRate.Rows[0]["MODEL"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetModelDesc(ModelID);
                        nodeModel.InnerText = dt.Rows[0]["MODEL"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeVehType = doc.CreateElement("VEHICLE_TYPE");
                nodeRiskInfo.AppendChild(nodeVehType);
                if (dtRate.Rows[0]["MODEL_TYPE"] != System.DBNull.Value)
                {
                    int TypeId = int.Parse(dtRate.Rows[0]["MODEL_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetModelTypeDesc(TypeId);
                        nodeVehType.InnerText = dt.Rows[0]["MODEL_TYPE"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeEngCapacity = doc.CreateElement("ENG_CAPACITY");
                nodeRiskInfo.AppendChild(nodeEngCapacity);
                if (dtRate.Rows[0]["ENG_CAPACITY"] != System.DBNull.Value)
                {
                    nodeEngCapacity.InnerText = dtRate.Rows[0]["ENG_CAPACITY"].ToString();
                }

                XmlNode nodeNumDriver = doc.CreateElement("NO_OF_DRIVERS");
                nodeRiskInfo.AppendChild(nodeNumDriver);
                if (dtRate.Rows[0]["NO_OF_DRIVERS"] != System.DBNull.Value)
                {
                    nodeNumDriver.InnerText = dtRate.Rows[0]["NO_OF_DRIVERS"].ToString();
                }

                XmlNode nodeClaims = doc.CreateElement("Areclaims");
                nodeRiskInfo.AppendChild(nodeClaims);
                if (dtRate.Rows[0]["CLAIM_RISK"] != System.DBNull.Value)
                {
                    string strRiskClaim = dtRate.Rows[0]["CLAIM_RISK"].ToString();
                    if (strRiskClaim.Trim() == "1")
                    {
                        nodeClaims.InnerText = "Yes";
                    }
                    else
                    {
                        nodeClaims.InnerText = "No";
                    }
                }

                XmlNode nodeNumClaim = doc.CreateElement("Noofclaims");
                nodeRiskInfo.AppendChild(nodeNumClaim);
                if (dtRate.Rows[0]["NO_OF_CLAIM"] != System.DBNull.Value)
                {
                    nodeNumClaim.InnerText = dtRate.Rows[0]["NO_OF_CLAIM"].ToString();
                }

                XmlNode nodeTotalClaimAmt = doc.CreateElement("TOTAL_CLAIM_AMT");
                nodeRiskInfo.AppendChild(nodeTotalClaimAmt);
                if (dtRate.Rows[0]["TOTAL_CLAIM_AMT"] != System.DBNull.Value)
                {
                    nodeTotalClaimAmt.InnerText = dtRate.Rows[0]["TOTAL_CLAIM_AMT"].ToString();
                }

                XmlNode nodeCoverage = doc.CreateElement("COVERAGE_TYPE");
                nodeRiskInfo.AppendChild(nodeCoverage);
                if (dtRate.Rows[0]["COVERAGE_TYPE"] != System.DBNull.Value)
                {
                    int COVID = int.Parse(dtRate.Rows[0]["COVERAGE_TYPE"].ToString());
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = ClsLookup.GetCoverageInfobyID(COVID);
                        nodeCoverage.InnerText = dt.Rows[0]["COV_DES"].ToString();
                    }
                    catch
                    { }
                }

                XmlNode nodeNoClaimDisc = doc.CreateElement("NoClaimsDiscount");
                nodeRiskInfo.AppendChild(nodeNoClaimDisc);
                if (dtRate.Rows[0]["NO_CLAIM_DISCOUNT"] != System.DBNull.Value)
                {
                    string strNoClaim = dtRate.Rows[0]["NO_CLAIM_DISCOUNT"].ToString();
                    if (strNoClaim.Trim() == "1")
                    {
                        nodeNoClaimDisc.InnerText = "Yes";
                    }
                    else
                    {
                        nodeNoClaimDisc.InnerText = "No";
                    }
                }

                XmlNode nodeFromDate = doc.CreateElement("Fromdate");
                nodeRiskInfo.AppendChild(nodeFromDate);
                if (dtRate.Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
                {
                    nodeFromDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EFFECTIVE_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeToDate = doc.CreateElement("ToDate");
                nodeRiskInfo.AppendChild(nodeToDate);
                if (dtRate.Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value)
                {
                    nodeToDate.InnerText = Convert.ToDateTime(dtRate.Rows[0]["APP_EXPIRATION_DATE"].ToString()).ToShortDateString();
                }

                XmlNode nodeCalEvoke = doc.CreateElement("CalculateEvokePage");
                productRiskData.AppendChild(nodeCalEvoke);

                XmlNode nodepremiumOprion = doc.CreateElement("PagePremiumOptionExist");
                productRiskData.AppendChild(nodepremiumOprion);

                XmlNode nodeDisplayCom = doc.CreateElement("DisplayComputation");
                productRiskData.AppendChild(nodeDisplayCom);

                XmlNode nodeMultiQuote = doc.CreateElement("MultiQuoteApplies");
                productRiskData.AppendChild(nodeMultiQuote);


                string strRqXML = doc.OuterXml.ToString();

                return strRqXML;

            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}
