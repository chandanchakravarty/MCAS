/******************************************************************************************
	<Author					: - > Anurag Verma
 	<Start Date				: -	> 25/04/2005    
	<End Date				: - > 
	<Description			: - > This is code behind file for adding and updating prior loss information
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - 25/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page 
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
using System.Reflection;
using System.Resources;  
using Cms.Model.Application.PriorLoss;  
using Cms.BusinessLayer.BlApplication;  
using Cms.CmsWeb; 
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlCommon;
//using System.Reflection;
//using System.Resources;


namespace Cms.Application.PriorLoss
{
	/// <summary>
	/// This class is being used to give functionality to Prior Loss module.
	/// </summary>
	public class AddPriorLoss : Cms.Application.appbase  
	{
        #region CONTROL REFERENCE
            protected System.Web.UI.WebControls.Label lblMessage;
            protected System.Web.UI.WebControls.Label capLosses;
            protected System.Web.UI.WebControls.Label capLOB;
            protected System.Web.UI.WebControls.DropDownList cmbLOB;
            protected Cms.CmsWeb.Controls.CmsButton btnSave;
            protected System.Web.UI.WebControls.Label capMessage;
            protected Cms.CmsWeb.Controls.CmsButton btnReset;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_XML;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
    
            protected System.Web.UI.WebControls.Label capOCCURENCE_DATE;
            protected System.Web.UI.WebControls.DropDownList cmbOCCURENCE_DATE;
            protected System.Web.UI.WebControls.DropDownList cmbCLAIM_DATE;
            protected System.Web.UI.WebControls.Label capLOSS_TYPE;
            protected System.Web.UI.WebControls.DropDownList cmbLOSS_TYPE;
            protected System.Web.UI.WebControls.Label capAMOUNT_PAID;
            protected System.Web.UI.WebControls.TextBox txtAMOUNT_PAID;
            protected System.Web.UI.WebControls.Label capCLAIM_STATUS;
            protected System.Web.UI.WebControls.DropDownList cmbCLAIM_STATUS;
            protected System.Web.UI.WebControls.Label capLOSS;
            protected System.Web.UI.WebControls.TextBox txtOCCURENCE_DATE;
            protected System.Web.UI.WebControls.TextBox txtLine_Of_Bussiness;
            protected System.Web.UI.WebControls.CustomValidator  csvREMARKS;
			protected System.Web.UI.WebControls.RequiredFieldValidator  rfvDRIVER_NAME;
			protected System.Web.UI.WebControls.RequiredFieldValidator  rfvRELATIONSHIP;
		    protected System.Web.UI.WebControls.RequiredFieldValidator rfvOCCURENCE_DATE;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOSS_ID;
			protected char STRING_DELIMITER = '^';

            protected System.Web.UI.WebControls.Image imgOCCURENCE_DATE;
            
            protected System.Web.UI.WebControls.RegularExpressionValidator revOCCURENCE_DATE;
            protected System.Web.UI.WebControls.Image imgCLAIM_DATE;  
            protected System.Web.UI.WebControls.HyperLink hlkOCCURENCE_DATE;        

            protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
 

            protected System.Web.UI.WebControls.Label lblLOSS;
			protected System.Web.UI.WebControls.CustomValidator csvOccurence_date;
            protected System.Web.UI.WebControls.RegularExpressionValidator revAMOUNT_PAID;
			protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
            protected System.Web.UI.HtmlControls.HtmlTableRow newRow;  

			protected System.Web.UI.WebControls.Label capAPLUS_REPORT_ORDERED;
			protected System.Web.UI.WebControls.DropDownList cmbAPLUS_REPORT_ORDERED;
			protected System.Web.UI.WebControls.Label capDRIVER_ID;
            protected System.Web.UI.WebControls.DropDownList cmbDRIVER_ID;
			protected System.Web.UI.WebControls.Label capDRIVER_NAME;
            protected System.Web.UI.WebControls.TextBox txtDRIVER_NAME;
			protected System.Web.UI.WebControls.Label capRELATIONSHIP;
            protected System.Web.UI.WebControls.DropDownList cmbRELATIONSHIP;
			protected System.Web.UI.WebControls.Label capCLAIMS_TYPE;
            protected System.Web.UI.WebControls.DropDownList cmbCLAIMS_TYPE;
			protected System.Web.UI.WebControls.Label capAT_FAULT;
            protected System.Web.UI.WebControls.DropDownList cmbAT_FAULT;
			protected System.Web.UI.WebControls.Label capCHARGEABLE;
            protected System.Web.UI.WebControls.DropDownList cmbCHARGEABLE;
			protected System.Web.UI.WebControls.Label capLOSS_ADD1;
			protected System.Web.UI.WebControls.Label capLOSS_ADD2;
			protected System.Web.UI.WebControls.Label capLOSS_CITY;
			protected System.Web.UI.WebControls.Label capLOSS_STATE;
			protected System.Web.UI.WebControls.Label capLOSS_ZIP;
			protected System.Web.UI.WebControls.Label capCURRENT_ADD1;
			protected System.Web.UI.WebControls.Label capCURRENT_ADD2;
			protected System.Web.UI.WebControls.Label capCURRENT_CITY;
			protected System.Web.UI.WebControls.Label capCURRENT_STATE;
			protected System.Web.UI.WebControls.Label capCURRENT_ZIP;
			protected System.Web.UI.WebControls.TextBox txtLOSS_ADD1;
			protected System.Web.UI.WebControls.TextBox txtLOSS_ADD2;
			protected System.Web.UI.WebControls.TextBox txtLOSS_CITY;
			protected System.Web.UI.WebControls.DropDownList cmbLOSS_STATE;
			protected System.Web.UI.WebControls.TextBox txtLOSS_ZIP;
			protected System.Web.UI.WebControls.TextBox txtCURRENT_ADD1;		
			protected System.Web.UI.WebControls.TextBox txtCURRENT_ADD2;		
			protected System.Web.UI.WebControls.TextBox txtCURRENT_CITY;		
			protected System.Web.UI.WebControls.DropDownList cmbCURRENT_STATE;
			protected System.Web.UI.WebControls.TextBox txtCURRENT_ZIP;
			protected System.Web.UI.WebControls.RegularExpressionValidator revLOSS_ZIP;
			protected System.Web.UI.WebControls.RegularExpressionValidator revCURRENT_ZIP;
			//Done for Itrack Issue 6723 on 27 Nov 09
			protected System.Web.UI.WebControls.Label capNAME_MATCH;
			protected System.Web.UI.WebControls.DropDownList cmbNAME_MATCH;
			//Added by Charles on 30-Nov-09 for Itrack 6647
			protected System.Web.UI.WebControls.Label capWaterBackup_SumpPump_Loss;
			protected System.Web.UI.WebControls.DropDownList cmbWaterBackup_SumpPump_Loss;
			//Added for Itrack 6647 on 9 dec 09
			protected System.Web.UI.WebControls.Label capWeather_Related_Loss;
			protected System.Web.UI.WebControls.DropDownList cmbWeather_Related_Loss;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvWeather_Related_Loss;


        #endregion

        #region GLOBAL DECLARATION
            protected ResourceManager objRescMgr; 
            private string strhidRowId              =   "";
            public string primaryKeyValues          =   "";
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label capDESC_OF_LOSS_AND_REMARKS;
		protected System.Web.UI.WebControls.TextBox txtDESC_OF_LOSS_AND_REMARKS;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.Image imgCurZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkCurZipLookup;
        protected System.Web.UI.WebControls.Label capLine_Of_Bussiness;
        protected System.Web.UI.WebControls.Label capRemarks_Description_of_Loss;
		protected System.Web.UI.WebControls.Label capLOSS_LOCATION;
		protected System.Web.UI.WebControls.TextBox txtLOSS_LOCATION;
		protected System.Web.UI.WebControls.CustomValidator  csvLOSS_LOCATION;
		protected System.Web.UI.WebControls.Label capCAUSE_OF_LOSS;
		protected System.Web.UI.WebControls.TextBox txtCAUSE_OF_LOSS;
		protected System.Web.UI.WebControls.CustomValidator  csvCAUSE_OF_LOSS;
		protected System.Web.UI.WebControls.Label capPOLICY_NUM;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUM;
		protected System.Web.UI.WebControls.CustomValidator  csvPOLICY_NUM;
		protected System.Web.UI.WebControls.Label capLOSS_CARRIER;
		protected System.Web.UI.WebControls.Label capOTHER_DESC;
		protected System.Web.UI.WebControls.TextBox txtLOSS_CARRIER;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DESC;
		protected System.Web.UI.WebControls.CustomValidator  csvLOSS_CARRIER;
		protected ClsPriorLoss objPriorLoss;
        #endregion
    
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            base.ScreenId="118_0"; 
                
			/* This Security XML has been explicitly specified.
			  * It is done coz, when we go to this page, the ApplicationID session variable has a value.
			  * This in turn checks for a converted Application and then accordingly sets security XML,
			  * resulting in different Permission XML. Refer Support>Appbase.cs (Line no 70)
			 */

			//Commented by Sibin on 07-10-08
			//gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			
			//modified By Pravesh on 11 dec 08 as per above Comment; to handle Rights of this page, this page will fetch SecurityXML again
			SetSecurityXML(base.ScreenId, int.Parse(GetUserId()));
			//base.InitializeSecuritySettings();

            #region SETTING PERMISSION ACCESS TO BUTTONS
            btnSave.CmsButtonClass			        =	Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString			    =	gstrSecurityXML;

            btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString	=	gstrSecurityXML;

            btnReset.CmsButtonClass	                =	Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString		        =	gstrSecurityXML;	


            #endregion    

            //btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "');");
			btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
            txtAMOUNT_PAID.Attributes.Add("onBlur", "this.value=formatAmount(this.value);SetChargeableField();"); 
        //  txtAMOUNT_RESERVED.Attributes.Add("onBlur","this.value=formatCurrency(this.value);"); 
            cmbCLAIM_STATUS.Attributes.Add("onFocus","SelectComboIndex('cmbCLAIM_STATUS');");   
            cmbLOB.Attributes.Add("onChange","ShowHideFields();SetChargeableField();");
			cmbDRIVER_ID.Attributes.Add("onChange","DisplayDriverFields(true);");
			cmbAT_FAULT.Attributes.Add("onChange","SetChargeableField();");			
            cmbLOSS_TYPE.Attributes.Add("onFocus","SelectComboIndex('cmbLOSS_TYPE')"); 
			//Done for Itrack Issue 6640 on 10 Dec 09
			btnSave.Attributes.Add("onkeypress","javascript:if(event.keycode==13){Weather_Related_Loss();}");
			btnSave.Attributes.Add("onClick","javascript:Weather_Related_Loss();");
			//cmbLOSS_TYPE.Attributes.Add("onChange","LOSS_TYPE();");
			// Added by Swarup on 05-apr-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtLOSS_ADD1,txtLOSS_ADD2
				, txtLOSS_CITY, cmbLOSS_STATE, txtLOSS_ZIP);
			imgCurZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkCurZipLookup, txtCURRENT_ADD1,txtCURRENT_ADD2
				, txtCURRENT_CITY, cmbCURRENT_STATE, txtCURRENT_ZIP); 
        

            #region code for calendar picker
                hlkOCCURENCE_DATE.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtOCCURENCE_DATE,document.Form1.txtOCCURENCE_DATE)"); //Javascript Implementation for Calender				
               // hlkCLAIM_DATE.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtCLAIM_DATE,document.Form1.txtCLAIM_DATE)"); //Javascript Implementation for Calender				
            #endregion
            
            if(Page.IsPostBack==false)
            {
                //btnActivateDeactivate.Enabled	= false;
				PopulateHiddenFields();
				SetErrorMessages();
                SetCaptions();
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                FillCombo();

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, "AddPriorLoss.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID + "/AddPriorLoss.xml");


                if(Request.QueryString["LOSS_ID"]!=null)
                {
                    GenerateXML(Request.QueryString["LOSS_ID"].ToString());
                }
                else
                {
                    newRow.Visible=false; 
                }


               
            }
		}

        #region activate deactivate
        /// <summary>
        /// This function is used to give activate and deactivate functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            Cms.BusinessLayer.BlApplication.ClsPriorLoss objPriorInfo    = new Cms.BusinessLayer.BlApplication.ClsPriorLoss();
            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				
                objStuTransactionInfo.loggedInUserName = GetUserName();

                if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    objStuTransactionInfo.transactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1366");//"Prior Loss Deactivated Succesfully.";
                    objPriorInfo.TransactionInfoParams = objStuTransactionInfo;
                    objPriorInfo.ActivateDeactivate(hidLOSS_ID.Value,"N");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
                    hidIS_ACTIVE.Value="N";
                }
                else
                {
                    objStuTransactionInfo.transactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1367");//"Prior Loss Activated Succesfully.";
                    objPriorInfo.TransactionInfoParams = objStuTransactionInfo;
                    objPriorInfo.ActivateDeactivate(hidLOSS_ID.Value,"Y");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
                    hidIS_ACTIVE.Value="Y";
                }
                GenerateXML(hidLOSS_ID.Value);
                hidFormSaved.Value			=	"1";
            }
            catch(Exception ex)
            {
                lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
                lblMessage.Visible	=	true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if(objPriorInfo!= null)
                    objPriorInfo.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// fetching data based on query string values
        /// </summary>
        private void GenerateXML(string lossID)
        {
            string strLossId=lossID;
                //Request.QueryString["LOSS_ID"].ToString();
            objPriorLoss=new ClsPriorLoss(); 
  
            if(strLossId!="" && strLossId!=null)
            {
             try
             {
                 DataSet ds=new DataSet(); 
                 ds=objPriorLoss.FetchData(int.Parse(strLossId),int.Parse(hidCUSTOMER_ID.Value));
                 lblLOSS.Text=ds.Tables[0].Rows[0]["LOSS"].ToString(); 
                 newRow.Visible=true; 
                 hidOldData.Value=ds.GetXml(); 

                //hidFormSaved.Value="1"; 
             }
             catch(Exception ex)
             {
                 lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
                 lblMessage.Visible	=	true;
                 Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                 hidFormSaved.Value			=	"2";                
                 newRow.Visible=false; 
             }
             finally
             {
                 if(objPriorLoss!= null)
                     objPriorLoss.Dispose();
             }  
                
            }
                
        }

        /// <summary>
        /// populating hidden fields
        /// </summary>
        private void PopulateHiddenFields()
        {
            hidCUSTOMER_ID.Value    = GetCustomerID();
            
        }

        /// <summary>
        /// Populating combo box from lookup tables
        /// </summary>
        private void FillCombo()
        {
			//Get the customer details
			DataSet dsCustomer = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerDetails(int.Parse(hidCUSTOMER_ID.Value));
			string customerType = dsCustomer.Tables[0].Rows[0]["CUSTOMER_TYPE_DESC"].ToString();
			if(customerType != "")
				customerType	=	customerType.Substring(0,1);

            cmbLOSS_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLOSS");
            cmbLOSS_TYPE.DataTextField="LookupDesc"; 
            cmbLOSS_TYPE.DataValueField="LookupID"; 
            cmbLOSS_TYPE.DataBind();
			cmbLOSS_TYPE.Items.Insert(0,"");

            DataTable dt = Cms.CmsWeb.ClsFetcher.LOBs;
           // cmbLOB.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LOBCD", customerType);
            cmbLOB.DataSource=dt.DefaultView;
			cmbLOB.DataTextField="Lob_Desc"; 
            cmbLOB.DataValueField="Lob_ID"; 
            cmbLOB.DataBind(); 
			cmbLOB.Items.Insert(0,"");			
			cmbLOB.Items.Add(new ListItem("Other","-1"));

            
            cmbCLAIM_STATUS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLAIM");
            cmbCLAIM_STATUS.DataTextField="LookupDesc"; 
            cmbCLAIM_STATUS.DataValueField="LookupID"; 
            cmbCLAIM_STATUS.DataBind();  

//			cmbAPLUS_REPORT_ORDERED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
//			cmbAPLUS_REPORT_ORDERED.DataTextField="LookupDesc"; 
//			cmbAPLUS_REPORT_ORDERED.DataValueField="LookupID"; 
//			cmbAPLUS_REPORT_ORDERED.DataBind();  		
	
			cmbAPLUS_REPORT_ORDERED.Items.Clear();
			ListItem list = new ListItem("Manual", "0");
			cmbAPLUS_REPORT_ORDERED.Items.Add(list);
			list = new ListItem("IIX", "1");
			cmbAPLUS_REPORT_ORDERED.Items.Add(list);
			list = new ListItem("EARS", "2");
			cmbAPLUS_REPORT_ORDERED.Items.Add(list);

			//Done for Itrack Issue 6723 on 27 Nov 09
            //cmbNAME_MATCH.Items.Clear();
            //ListItem listNAME_MATCH = new ListItem("", "0");
            //cmbNAME_MATCH.Items.Add(listNAME_MATCH);
            //listNAME_MATCH = new ListItem("No", "1");
            //cmbNAME_MATCH.Items.Add(listNAME_MATCH);
            //listNAME_MATCH = new ListItem("Yes", "2");
            //cmbNAME_MATCH.Items.Add(listNAME_MATCH);
          
            cmbNAME_MATCH.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YesNo");
            cmbNAME_MATCH.DataTextField = "LookupDesc";
            cmbNAME_MATCH.DataValueField ="LookupCode";
            cmbNAME_MATCH.DataBind();
            cmbNAME_MATCH.Items.Insert(2, "");


			cmbRELATIONSHIP.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DRACD");
			cmbRELATIONSHIP.DataTextField="LookupDesc"; 
			cmbRELATIONSHIP.DataValueField="LookupID"; 
			cmbRELATIONSHIP.DataBind();  			
			cmbRELATIONSHIP.Items.Insert(0,"");

			cmbCLAIMS_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CMTYPS");
			cmbCLAIMS_TYPE.DataTextField="LookupDesc"; 
			cmbCLAIMS_TYPE.DataValueField="LookupID"; 
			cmbCLAIMS_TYPE.DataBind();  
			cmbCLAIMS_TYPE.Items.Insert(0,"");

			cmbAT_FAULT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbAT_FAULT.DataTextField="LookupDesc"; 
			cmbAT_FAULT.DataValueField="LookupID"; 
			cmbAT_FAULT.DataBind(); 
			cmbAT_FAULT.Items.Insert(0,"");

			cmbCHARGEABLE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CHGL");
			cmbCHARGEABLE.DataTextField="LookupDesc"; 
			cmbCHARGEABLE.DataValueField="LookupID"; 
			cmbCHARGEABLE.DataBind();
			cmbCHARGEABLE.Items.Insert(0,"");

			ClsDriverDetail objDriverDetail = new ClsDriverDetail();
			DataSet dsDrivers = objDriverDetail.GetAutoDriversForCustomer(int.Parse(hidCUSTOMER_ID.Value));
			if(dsDrivers!=null && dsDrivers.Tables.Count>0 & dsDrivers.Tables[0].Rows.Count>0)
			{
				hidDRIVER_XML.Value = dsDrivers.GetXml();
				/*cmbDRIVER_ID.DataSource = dsDrivers;
				cmbDRIVER_ID.DataTextField = "DRIVER_NAME";
				cmbDRIVER_ID.DataValueField = "DRIVER_ID";
				cmbDRIVER_ID.DataBind();*/
			}
			cmbDRIVER_ID.Items.Add(new ListItem("Other","-1"));
			cmbDRIVER_ID.Items.Insert(0,"");
			DataTable dts = Cms.CmsWeb.ClsFetcher.State; 
			cmbLOSS_STATE.DataSource =dts;
			cmbLOSS_STATE.DataTextField   ="STATE_NAME";
			cmbLOSS_STATE.DataValueField  ="STATE_CODE";
			cmbLOSS_STATE.DataBind(); 
			cmbLOSS_STATE.Items.Insert(0,"");
			cmbCURRENT_STATE.DataSource =dts;
			cmbCURRENT_STATE.DataTextField   ="STATE_NAME";
			cmbCURRENT_STATE.DataValueField  ="STATE_CODE";
			cmbCURRENT_STATE.DataBind(); 
			cmbCURRENT_STATE.Items.Insert(0,"");
			//Added by Charles on 30-Nov-09 for Itrack 6647
			cmbWaterBackup_SumpPump_Loss.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbWaterBackup_SumpPump_Loss.DataTextField="LookupDesc"; 
			cmbWaterBackup_SumpPump_Loss.DataValueField="LookupID"; 
			cmbWaterBackup_SumpPump_Loss.DataBind(); 
			cmbWaterBackup_SumpPump_Loss.Items.Insert(0,"");
			//Added till here
			//Added for Itrack 6640 on 9 Dec 09
			cmbWeather_Related_Loss.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbWeather_Related_Loss.DataTextField="LookupDesc"; 
			cmbWeather_Related_Loss.DataValueField="LookupID"; 
			cmbWeather_Related_Loss.DataBind(); 
			cmbWeather_Related_Loss.Items.Insert(0,"");
			//Added till here
 
        }
		#region Set Validators ErrorMessages
		private void SetErrorMessages()
		{
			rfvOCCURENCE_DATE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1020");
			rfvWeather_Related_Loss.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1058");
		}
		#endregion
        /// <summary>
        /// Setting page labels and caption
        /// </summary>
        private void SetCaptions() 
        {
            
            objRescMgr                                  = new ResourceManager("Cms.Application.PriorLoss.AddPriorLoss",Assembly.GetExecutingAssembly());
            //capLine_Of_Bussiness.Text                   = objRescMgr.GetString("txtLINE_OF_BUSSINESS");
            //capRemarks_Description_of_Loss.Text         = objRescMgr.GetString("txtREMARKS_DESCRIPTION_OF_LOSS");
            //capLOSS.Text                                = objRescMgr.GetString("txtLOSSES");
            capLOB.Text                                 = objRescMgr.GetString("cmbLOB");
            capOCCURENCE_DATE.Text                      = objRescMgr.GetString("txtOCCURENCE_DATE");
         // capCLAIM_DATE.Text                          = objRescMgr.GetString("txtCLAIM_DATE");
            capLOSS_TYPE.Text                           = objRescMgr.GetString("cmbLOSS_TYPE");
         // capLOSS_DESC.Text                           = objRescMgr.GetString("txtLOSS_DESC");
            capAMOUNT_PAID.Text                         = objRescMgr.GetString("txtAMOUNT_PAID");
         // capAMOUNT_RESERVED.Text                     = objRescMgr.GetString("txtAMOUNT_RESERVED");
            capCLAIM_STATUS.Text                        = objRescMgr.GetString("cmbCLAIM_STATUS");
         // capMOD.Text                                 = objRescMgr.GetString("txtMOD");
            capDESC_OF_LOSS_AND_REMARKS.Text             = objRescMgr.GetString("txtDESC_OF_LOSS_AND_REMARKS");
         // capLOSS_RUN.Text                            = objRescMgr.GetString("chkLOSS_RUN");
         // capCAT_NO.Text                              = objRescMgr.GetString("txtCAT_NO");   
            
            csvREMARKS.ErrorMessage                     = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"429");
            revOCCURENCE_DATE.ValidationExpression		= aRegExpDate;
            revOCCURENCE_DATE.ErrorMessage              = ClsMessages.GetMessage(ScreenId, "22");	 
          //revCLAIM_DATE.ValidationExpression		    = aRegExpDate;
          //revCLAIM_DATE.ErrorMessage                  = ClsMessages.GetMessage(ScreenId, "22");	 
          //regCAT_NO.ValidationExpression              = aRegExpInteger;
          // regCAT_NO.ErrorMessage                      = ClsMessages.GetMessage(ScreenId, "102");	
          // revAMOUNT_RESERVED.ErrorMessage             = ClsMessages.FetchGeneralMessage("216"); 
          //revAMOUNT_RESERVED.ValidationExpression     = aRegExpDoublePositiveZero;  
            revAMOUNT_PAID.ErrorMessage                 = ClsMessages.FetchGeneralMessage("492"); 
            //Modified by Asfa(10-July-2008) - iTrack #4443
            revAMOUNT_PAID.ValidationExpression = aRegExpDoublePositiveNonZero;
          // csvCLAIM_DATE.ErrorMessage                  = ClsMessages.GetMessage(base.ScreenId,"1");
			csvOccurence_date.ErrorMessage				= ClsMessages.GetMessage(base.ScreenId,"508");

			capAPLUS_REPORT_ORDERED.Text				= objRescMgr.GetString("cmbAPLUS_REPORT_ORDERED");
			capDRIVER_ID.Text                           = objRescMgr.GetString("cmbDRIVER_ID");
			capDRIVER_NAME.Text                         = objRescMgr.GetString("txtDRIVER_NAME");
			capRELATIONSHIP.Text                        = objRescMgr.GetString("cmbRELATIONSHIP");
			capCLAIMS_TYPE.Text                         = objRescMgr.GetString("cmbCLAIMS_TYPE");
			capAT_FAULT.Text                            = objRescMgr.GetString("cmbAT_FAULT");
			capCHARGEABLE.Text                          = objRescMgr.GetString("cmbCHARGEABLE");

			rfvRELATIONSHIP.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDRIVER_NAME.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			
			revLOSS_ZIP.ValidationExpression			= aRegExpZip;
			revCURRENT_ZIP.ValidationExpression			= aRegExpZip;
			revLOSS_ZIP.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			revCURRENT_ZIP.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			capLOSS_ADD1.Text                           = objRescMgr.GetString("capLOSS_ADD1");
			capLOSS_ADD2.Text                           = objRescMgr.GetString("capLOSS_ADD2");
			capLOSS_CITY.Text                           = objRescMgr.GetString("capLOSS_CITY");
			capLOSS_STATE.Text                          = objRescMgr.GetString("capLOSS_STATE");
			capLOSS_ZIP.Text                            = objRescMgr.GetString("capLOSS_ZIP");
			capCURRENT_ADD1.Text                        = objRescMgr.GetString("capCURRENT_ADD1");
			capCURRENT_ADD2.Text                        = objRescMgr.GetString("capCURRENT_ADD2");
			capCURRENT_CITY.Text                        = objRescMgr.GetString("capCURRENT_CITY");
			capCURRENT_STATE.Text                       = objRescMgr.GetString("capCURRENT_STATE");
			capCURRENT_ZIP.Text                         = objRescMgr.GetString("capCURRENT_ZIP");
			capLOSS_LOCATION.Text						= objRescMgr.GetString("txtLOSS_LOCATION");
			capCAUSE_OF_LOSS.Text						= objRescMgr.GetString("txtCAUSE_OF_LOSS");
			capPOLICY_NUM.Text							= objRescMgr.GetString("txtPOLICY_NUM");
			capLOSS_CARRIER.Text					    = objRescMgr.GetString("txtLOSS_CARRIER");
			capOTHER_DESC.Text							= objRescMgr.GetString("txtOTHER_DESC");
			capNAME_MATCH.Text							= objRescMgr.GetString("cmbNAME_MATCH");//Done for Itrack Issue 6723 on 27 Nov 09
            capWaterBackup_SumpPump_Loss.Text			= objRescMgr.GetString("cmbWaterBackup_SumpPump_Loss");//Added by Charles on 30-Nov-09 for Itrack 6647
			capWeather_Related_Loss.Text				= objRescMgr.GetString("cmbWeather_Related_Loss");//Added for Itrack 6640 on 9 Dec 09
			csvLOSS_LOCATION.ErrorMessage               = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"429").Replace("50","70");
			csvCAUSE_OF_LOSS.ErrorMessage               = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"429");
			csvPOLICY_NUM.ErrorMessage                  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"429");
			csvLOSS_CARRIER.ErrorMessage                = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"429");
        }


        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsPriorLossInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsPriorLossInfo objPriorLossInfo;
            objPriorLossInfo                            = new ClsPriorLossInfo();

            //Code for retreiving the forms valus will go here		
            string oc_Date                              = txtOCCURENCE_DATE.Text;
          // string cl_Date                              = txtCLAIM_DATE.Text;


            strhidRowId                                 = hidLOSS_ID.Value;            
            if(hidLOSS_ID.Value!="New")
                objPriorLossInfo.LOSS_ID                = int.Parse(hidLOSS_ID.Value);

            if(oc_Date!="")
                objPriorLossInfo.OCCURENCE_DATE             = ConvertToDate(oc_Date);
            else
                objPriorLossInfo.OCCURENCE_DATE             = Convert.ToDateTime("1/1/1900");
  

           /* if(cl_Date!="")    
                objPriorLossInfo.CLAIM_DATE                 = Convert.ToDateTime(cl_Date);
            else
                objPriorLossInfo.CLAIM_DATE                 = Convert.ToDateTime("1/1/1900");	*/
  

            objPriorLossInfo.LOB                        = cmbLOB.SelectedValue;
            objPriorLossInfo.LOSS_TYPE                  = cmbLOSS_TYPE.SelectedValue.ToString()=="" ? 0 : int.Parse (cmbLOSS_TYPE.SelectedValue.ToString()) ;
            objPriorLossInfo.AMOUNT_PAID = txtAMOUNT_PAID.Text == "" ? 0 : double.Parse(txtAMOUNT_PAID.Text, numberFormatInfo);
          //objPriorLossInfo.AMOUNT_RESERVED            = txtAMOUNT_RESERVED.Text==""?0:double.Parse(txtAMOUNT_RESERVED.Text);
            objPriorLossInfo.CLAIM_STATUS               = cmbCLAIM_STATUS.SelectedValue;
        //  objPriorLossInfo.LOSS_DESC                  = txtLOSS_DESC.Text;
            objPriorLossInfo.REMARKS                    = txtDESC_OF_LOSS_AND_REMARKS.Text;
         // objPriorLossInfo.MOD                        = txtMOD.Text;
         // objPriorLossInfo.LOSS_RUN                   = chkLOSS_RUN.Checked == false? "N" : "Y" ;
         // objPriorLossInfo.CAT_NO                     = txtCAT_NO.Text;            
            objPriorLossInfo.CUSTOMER_ID                = hidCUSTOMER_ID.Value == "" ? 0 : int.Parse(hidCUSTOMER_ID.Value) ;

			objPriorLossInfo.LOSS_LOCATION              = txtLOSS_LOCATION.Text;
			objPriorLossInfo.CAUSE_OF_LOSS              = txtCAUSE_OF_LOSS.Text;
			objPriorLossInfo.POLICY_NUM                 = txtPOLICY_NUM.Text;
			objPriorLossInfo.LOSS_CARRIER               = txtLOSS_CARRIER.Text;
			objPriorLossInfo.OTHER_DESC					= txtOTHER_DESC.Text;

			if(cmbAPLUS_REPORT_ORDERED.SelectedItem!=null && cmbAPLUS_REPORT_ORDERED.SelectedItem.Value!="")
				objPriorLossInfo.APLUS_REPORT_ORDERED = int.Parse(cmbAPLUS_REPORT_ORDERED.SelectedItem.Value);

			//Done for Itrack Issue 6723 on 27 Nov 09
			if(cmbNAME_MATCH.SelectedItem!=null && cmbNAME_MATCH.SelectedItem.Value!="")
				objPriorLossInfo.NAME_MATCH = int.Parse(cmbNAME_MATCH.SelectedItem.Value);

			if(objPriorLossInfo.LOB == ((int)enumLOB.AUTOP).ToString() || objPriorLossInfo.LOB == ((int)enumLOB.CYCL).ToString())
			{
				//if(cmbDRIVER_ID.SelectedItem!=null && cmbDRIVER_ID.SelectedItem.Value!="")
				if(Request["cmbDRIVER_ID"]!=null && Request["cmbDRIVER_ID"].ToString()!="" && Request["cmbDRIVER_ID"].ToString()!="0")				
				{
					string DriverSelectedValue = Request["cmbDRIVER_ID"].ToString();
					//if(cmbDRIVER_ID.SelectedItem.Value=="-1")
					if(DriverSelectedValue=="-1")
					{
						objPriorLossInfo.DRIVER_ID = -1;
						hidDRIVER_ID.Value = "-1";
						objPriorLossInfo.DRIVER_NAME = txtDRIVER_NAME.Text.Trim();
						if(cmbRELATIONSHIP.SelectedItem!=null && cmbRELATIONSHIP.SelectedItem.Value!="")
							objPriorLossInfo.RELATIONSHIP = int.Parse(cmbRELATIONSHIP.SelectedItem.Value);
					}
					else
					{
						//int DriverID = int.Parse(cmbDRIVER_ID.SelectedItem.Value.Split(STRING_DELIMITER)[3].ToString());
						int DriverID = int.Parse(DriverSelectedValue.Split(STRING_DELIMITER)[3].ToString());
						objPriorLossInfo.DRIVER_ID = DriverID;
						//objPriorLossInfo.DRIVER_NAME = cmbDRIVER_ID.SelectedItem.Value;
						objPriorLossInfo.DRIVER_NAME = DriverSelectedValue;
						hidDRIVER_ID.Value = "0";
					}
				}
				

				if(cmbCLAIMS_TYPE.SelectedItem!=null && cmbCLAIMS_TYPE.SelectedItem.Value!="")
					objPriorLossInfo.CLAIMS_TYPE = int.Parse(cmbCLAIMS_TYPE.SelectedItem.Value);

				if(cmbAT_FAULT.SelectedItem!=null && cmbAT_FAULT.SelectedItem.Value!="")
					objPriorLossInfo.AT_FAULT = int.Parse(cmbAT_FAULT.SelectedItem.Value);

				if(cmbCHARGEABLE.SelectedItem!=null && cmbCHARGEABLE.SelectedItem.Value!="")
					objPriorLossInfo.CHARGEABLE = int.Parse(cmbCHARGEABLE.SelectedItem.Value);
				else
					objPriorLossInfo.CHARGEABLE = -1;
			}
            
                        
            return objPriorLossInfo;
            
        }
///for Home/Rental
///

		private ClsPriorLossInfo_Home GetFormValue_Home()
		{
			ClsPriorLossInfo_Home  objPriorLossInfoHome;
			objPriorLossInfoHome                            = new ClsPriorLossInfo_Home();

			strhidRowId                                 = hidLOSS_ID.Value;            
			if(hidLOSS_ID.Value!="New")
			objPriorLossInfoHome.LOSS_ID             = int.Parse(hidLOSS_ID.Value);
			objPriorLossInfoHome.LOCATION_ID                 = 0;
			objPriorLossInfoHome.CUSTOMER_ID                = hidCUSTOMER_ID.Value == "" ? 0 : int.Parse(hidCUSTOMER_ID.Value) ;
			if (txtLOSS_ADD1.Text!="" )
				objPriorLossInfoHome.LOSS_ADD1					=txtLOSS_ADD1.Text;   
			if (txtLOSS_ADD2.Text!="" )
				objPriorLossInfoHome.LOSS_ADD2					=txtLOSS_ADD2.Text;
			if (txtLOSS_CITY.Text!="" )
				objPriorLossInfoHome.LOSS_CITY 					=txtLOSS_CITY.Text;
			if (cmbLOSS_STATE.SelectedIndex  !=-1 && cmbLOSS_STATE.SelectedIndex  !=0 )
				objPriorLossInfoHome.LOSS_STATE					=cmbLOSS_STATE.SelectedValue;
			if (txtLOSS_ZIP.Text!="" )
				objPriorLossInfoHome.LOSS_ZIP 					=txtLOSS_ZIP.Text;
			if (txtCURRENT_ADD1.Text!="" )
				objPriorLossInfoHome.CURRENT_ADD1				=txtCURRENT_ADD1.Text;
			if (txtCURRENT_ADD2.Text!="" )
				objPriorLossInfoHome.CURRENT_ADD2				=txtCURRENT_ADD2.Text;
			if (txtCURRENT_CITY.Text!="" )
				objPriorLossInfoHome.CURRENT_CITY 				=txtCURRENT_CITY.Text;
			if (cmbCURRENT_STATE.SelectedIndex !=-1 && cmbCURRENT_STATE.SelectedIndex !=0)
				objPriorLossInfoHome.CURRENT_STATE 				=cmbCURRENT_STATE.SelectedValue;
			if (txtCURRENT_ZIP.Text!="" )
				objPriorLossInfoHome.CURRENT_ZIP 				=txtCURRENT_ZIP.Text;	
			//Added For Itrack Issue #6712.
			if (txtPOLICY_NUM.Text!="" )
				objPriorLossInfoHome.POLICY_NUMBER  			=txtPOLICY_NUM.Text;	

					    
			//Added by Charles on 30-Nov-09 for Itrack 6647
			if(cmbWaterBackup_SumpPump_Loss.SelectedItem!=null && cmbWaterBackup_SumpPump_Loss.SelectedItem.Value!="")
				objPriorLossInfoHome.WATERBACKUP_SUMPPUMP_LOSS = int.Parse(cmbWaterBackup_SumpPump_Loss.SelectedItem.Value);

			//Added for Itrack 6640 on 9 Dec 09
			if(cmbWeather_Related_Loss.SelectedItem!=null && cmbWeather_Related_Loss.SelectedItem.Value!="")
				objPriorLossInfoHome.WEATHER_RELATED_LOSS = int.Parse(cmbWeather_Related_Loss.SelectedItem.Value);
                        
			return objPriorLossInfoHome;
		}


        #endregion

        #region "Web Event Handlers"
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function
                objPriorLoss = new  ClsPriorLoss(); 

                //Retreiving the form values into model class object
                ClsPriorLossInfo objPriorLossInfo1 = GetFormValue();
				ClsPriorLossInfo_Home objPriorLossInfo_Home=GetFormValue_Home();;
				/*if (cmbLOB.SelectedValue == "1" || cmbLOB.SelectedValue =="6")
				{
					objPriorLossInfo_Home = GetFormValue_Home();
				}*/

                if(strhidRowId.ToUpper().Equals("NEW")) //save case
                {
                    objPriorLossInfo1.CREATED_BY = int.Parse(GetUserId());
                    objPriorLossInfo1.CREATED_DATETIME = DateTime.Now;
                    objPriorLossInfo1.MODIFIED_BY  = int.Parse(GetUserId());
                    objPriorLossInfo1.LAST_UPDATED_DATETIME  = DateTime.Now;
                    objPriorLossInfo1.IS_ACTIVE ="Y";

                    //Calling the add method of business layer class
                    if (cmbLOB.SelectedValue == "1" || cmbLOB.SelectedValue =="6")
						intRetVal = objPriorLoss.Add(objPriorLossInfo_Home,objPriorLossInfo1);
					else
						intRetVal = objPriorLoss.Add(objPriorLossInfo1);
                    if(intRetVal>0)
                    {
                        hidLOSS_ID.Value        = intRetVal.ToString() ; 
                        primaryKeyValues        = hidLOSS_ID.Value + "^" + hidCUSTOMER_ID.Value; 
                        lblMessage.Text			= ClsMessages.GetMessage(base.ScreenId,"29");
                        hidFormSaved.Value		= "1";
                        hidIS_ACTIVE.Value       = "Y";
						//Opening the endorsement details page - Added by Sibin on 3 March 09 for Itrack Issue 5453
						base.OpenEndorsementDetails();
                    }
                    else if(intRetVal == -1)
                    {
                        lblMessage.Text			= ClsMessages.GetMessage(base.ScreenId,"18");
                        hidFormSaved.Value		= "2";
                    }
                    else
                    {
                        lblMessage.Text			= ClsMessages.GetMessage(base.ScreenId,"20");
                        hidFormSaved.Value		= "2";
                    }
                    lblMessage.Visible          = true;
                } // end save case
                else //UPDATE CASE
                {
                    //Creating the Model object for holding the Old data
                    ClsPriorLossInfo objOldPriorLossInfo;
                    objOldPriorLossInfo = new ClsPriorLossInfo();
					//Added by Charles on 30-Nov-09 for Itrack 6647
					ClsPriorLossInfo_Home objOldPriorLossInfo_Home;
					objOldPriorLossInfo_Home = new ClsPriorLossInfo_Home();
					base.PopulateModelObject(objOldPriorLossInfo_Home,hidOldData.Value);
					//Added till here

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldPriorLossInfo,hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    objPriorLossInfo1.CREATED_BY = int.Parse(GetUserId());
                    objPriorLossInfo1.CREATED_DATETIME = DateTime.Now ;
                    objPriorLossInfo1.CLAIMID="1"; 
                    objPriorLossInfo1.LOSS_ID = int.Parse(strhidRowId);
                    objPriorLossInfo1.MODIFIED_BY = int.Parse(GetUserId());
                    objPriorLossInfo1.LAST_UPDATED_DATETIME = DateTime.Now;
                    objPriorLossInfo1.IS_ACTIVE = hidIS_ACTIVE.Value;

                    //Updating the record using business layer class object
					//Added by Charles on 30-Nov-09 for Itrack 6647
					objPriorLossInfo_Home.MODIFIED_BY = int.Parse(GetUserId());
					int intRetVal_Home;
					if (cmbLOB.SelectedValue == "1" || cmbLOB.SelectedValue =="6")
					{
						intRetVal	   = objPriorLoss.Update(objOldPriorLossInfo,objPriorLossInfo1);
						intRetVal_Home = objPriorLoss.Update(objOldPriorLossInfo_Home,objPriorLossInfo_Home);						
					}
					else //Added till here
						intRetVal	= objPriorLoss.Update(objOldPriorLossInfo,objPriorLossInfo1);
                    if( intRetVal > 0 )			// update successfully performed
                    {
                        lblMessage.Text			= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
                        primaryKeyValues        = hidLOSS_ID.Value + "^" + hidCUSTOMER_ID.Value;
                        hidFormSaved.Value		= "1";
                        hidLOSS_ID.Value        = objPriorLossInfo1.LOSS_ID.ToString();
						//Opening the endorsement details page - Added by Sibin on 3 March 09 for Itrack Issue 5453
						base.OpenEndorsementDetails();
                    }
                    else if(intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
                        hidFormSaved.Value		=	"1";
                    }
                    else 
                    {
                        lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
                        hidFormSaved.Value		=	"1";
                    }
                    lblMessage.Visible = true;
                }
                GenerateXML(hidLOSS_ID.Value);
            }
            catch(Exception ex)
            {
                lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible	=	true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value			=	"2";                
            }
            finally
            {
                if(objPriorLoss!= null)
                    objPriorLoss.Dispose();
            }
        }


        

        #endregion

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
			this.csvREMARKS.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.csvREMARKS_ServerValidate);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
            {
                int retValue = Delete();
                if (retValue > 0)
                {
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("338"); 
                    primaryKeyValues = "";
                    hidFormSaved.Value = "5";
                    hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
                }
                else if(retValue == -2)
                {
                    lblDelete.Text			=	ClsMessages.FetchGeneralMessage("334");
                    hidFormSaved.Value		=	"2";
                }
                else
                {
                    lblDelete.Text         =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("334"); 
                    hidFormSaved.Value      =   "2";
                }
                lblDelete.Visible = true;
            }
            catch (Exception objEx)
            {
                //lblMessage.Text = objEx.Message.ToString();
                //lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
            }
		}

		private void RegisterScript()
		{
			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				string strCode = @"<script>
									RefreshWebGrid(1,1)</script>";

				ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strCode);

			}

		}

		//Modified by Asfa (29-May-2008) - iTrack #4240
		private int Delete()
		{
			Cms.BusinessLayer.BlApplication.ClsPriorLoss objPriorLoss = new ClsPriorLoss();

			ClsPriorLossInfo objPriorLossInfo = GetFormValue();

			objPriorLossInfo.CUSTOMER_ID		= int.Parse(hidCUSTOMER_ID.Value);
			objPriorLossInfo.LOSS_ID 			= int.Parse(hidLOSS_ID.Value);
			objPriorLossInfo.CREATED_BY			= int.Parse(GetUserId());
			objPriorLossInfo.CREATED_DATETIME	= DateTime.Now;
			objPriorLossInfo.MODIFIED_BY		= int.Parse(GetUserId());
			objPriorLossInfo.LAST_UPDATED_DATETIME  = DateTime.Now;
			objPriorLossInfo.IS_ACTIVE			="Y";

			int retValue = objPriorLoss.Delete(objPriorLossInfo);
			return retValue;	
		}

		private void csvREMARKS_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
		{
		
		}
	}
}
