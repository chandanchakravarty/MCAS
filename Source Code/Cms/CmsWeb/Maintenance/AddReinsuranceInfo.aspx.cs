/******************************************************************************************
<Author					: -   nidhi
<Start Date				: -	1/4/2006 6:23:02 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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

using System.Resources; 
using System.Reflection;

using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BlAccount; //For Reinsurance companies list  function

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class ReinsuranceInfo : Cms.CmsWeb.cmsbase
	{
		# region DECELARATION
		#region Page controls declaration

		protected System.Web.UI.WebControls.DropDownList cmbCONTRACT_TYPE;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.Label lblMessage;

		

		#endregion
		
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
        protected System.Web.UI.WebControls.Label capCASH_CALL_LIMIT;
        protected System.Web.UI.WebControls.TextBox txtCASH_CALL_LIMIT;
		protected System.Web.UI.WebControls.Label capCONTRACT_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_ID;
		protected System.Web.UI.WebControls.Label capLOSS_ADJUSTMENT_EXPENSE;
		protected System.Web.UI.WebControls.TextBox txtCONTRACT_DESC;
		protected System.Web.UI.WebControls.TextBox txtCONTRACT_NUMBER;
		protected System.Web.UI.WebControls.Label capCONTRACT_DESC;
		protected System.Web.UI.WebControls.Label capCONTRACT_NUMBER;
		protected System.Web.UI.WebControls.Label capCONTACT_YEAR;
		protected System.Web.UI.WebControls.Label capCOMMISSION;
		protected System.Web.UI.WebControls.Label capPER_OCCURRENCE_LIMIT;
		protected System.Web.UI.WebControls.Label capANNUAL_AGGREGATE;
		protected System.Web.UI.WebControls.Label capDEPOSIT_PREMIUMS;
		protected System.Web.UI.WebControls.Label capDEPOSIT_PREMIUM_PAYABLE;
        protected System.Web.UI.WebControls.Label CapAddLookup;
		protected System.Web.UI.WebControls.Label capMINIMUM_PREMIUM;
		protected System.Web.UI.WebControls.Label capSEQUENCE_NUMBER;
		protected System.Web.UI.WebControls.Label capORIGINAL_CONTACT_DATE;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.Label capPREMIUM_COMMISION; //sneha
        protected System.Web.UI.WebControls.Label capPREMIUN_SECTION; //sneha
        protected System.Web.UI.WebControls.Label capCOMMISSION_SECTION;//sneha
		protected System.Web.UI.WebControls.HyperLink hlkEffectiveDate;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capEXPIRATION_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationDate;
		protected System.Web.UI.WebControls.TextBox txtEXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capCALCULATION_BASE;
        protected System.Web.UI.WebControls.Label capMAX_NO_INSTALLMENT;
		protected System.Web.UI.WebControls.DropDownList cmbCALCULATION_BASE;
		protected System.Web.UI.WebControls.DropDownList cmbLOSS_ADJUSTMENT_EXPENSE;
        protected System.Web.UI.WebControls.DropDownList cmbMAX_NO_INSTALLMENT;  //Added by Aditya for TFS BUG # 2512
		protected System.Web.UI.WebControls.TextBox txtANNUAL_AGGREGATE;
		protected System.Web.UI.WebControls.TextBox txtSEQUENCE_NUMBER;
		protected System.Web.UI.WebControls.Label capTERMINATION_DATE;
		protected System.Web.UI.WebControls.Label capREINSURANCE_PREMIUM_ACCOUNT;
		protected System.Web.UI.WebControls.Label capREINSURANCE_PAYABLE_ACCOUNT;
		protected System.Web.UI.WebControls.Label capREINSURANCE_COMMISSION_ACCOUNT;
		protected System.Web.UI.WebControls.Label capREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT;
		protected System.Web.UI.WebControls.DropDownList cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT;
		protected System.Web.UI.WebControls.Label capTERMINATION_REASON;
		protected System.Web.UI.WebControls.Label capCOMMENTS;
		protected System.Web.UI.WebControls.Label capFOLLOW_UP_FIELDS;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label capCOMMISSION_APPLICABLE;
		protected System.Web.UI.WebControls.HyperLink hlkORIGINAL_CONTACT_DATE;
		protected System.Web.UI.WebControls.TextBox txtORIGINAL_CONTACT_DATE;
		protected System.Web.UI.WebControls.DropDownList cmbCONTACT_YEAR;
		protected System.Web.UI.WebControls.TextBox txtCOMMISSION;
		protected System.Web.UI.WebControls.TextBox txtPER_OCCURRENCE_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtDEPOSIT_PREMIUMS;
		protected System.Web.UI.WebControls.TextBox txtMINIMUM_PREMIUM;
		protected System.Web.UI.WebControls.TextBox txtTERMINATION_REASON;
		protected System.Web.UI.WebControls.HyperLink hlkTERMINATION_DATE;
		protected System.Web.UI.WebControls.TextBox txtTERMINATION_DATE;
		protected System.Web.UI.WebControls.TextBox txtCOMMENTS;
		protected System.Web.UI.WebControls.TextBox txtFOLLOW_UP_FIELDS;
		protected System.Web.UI.WebControls.DropDownList cmbCOMMISSION_APPLICABLE;
		protected System.Web.UI.WebControls.DropDownList cmbREINSURANCE_PREMIUM_ACCOUNT;
		protected System.Web.UI.WebControls.DropDownList cmbREINSURANCE_PAYABLE_ACCOUNT;
		protected System.Web.UI.WebControls.DropDownList cmbREINSURANCE_COMMISSION_ACCOUNT;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivate;
		protected System.Web.UI.WebControls.DropDownList cmbDEPOSIT_PREMIUM_PAYABLE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTRACT_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTRACT_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvORIGINAL_CONTACT_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMMISSION;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPER_OCCURRENCE_LIMIT;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPOSIT_PREMIUMS;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMINIMUM_PREMIUM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERMINATION_DATE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMMISSION_APPLICABLE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvREINSURANCE_PAYABLE_ACCOUNT;
	//	protected System.Web.UI.WebControls.RequiredFieldValidator rfvREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOSS_ADJUSTMENT_EXPENSE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPIRATION_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCALCULATION_BASE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvANNUAL_AGGREGATE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEPOSIT_PREMIUM_PAYABLE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSEQUENCE_NUMBER;
//      protected System.Web.UI.WebControls.RequiredFieldValidator rfvREINSURANCE_PREMIUM_ACCOUNT;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvREINSURANCE_COMMISSION_ACCOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPIRATION_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revORIGINAL_CONTACT_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTERMINATION_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATEAssignLossCodes;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvRSKAssignLossCodes;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOBAssignLossCodes;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAX_NO_INSTALLMENT;  //Added by Aditya for TFS BUG # 2512
		protected System.Web.UI.WebControls.CustomValidator csvRISK_EXPOSURE;
		protected System.Web.UI.WebControls.CustomValidator csvCONTRACT_LOB;
        protected System.Web.UI.WebControls.CustomValidator csvCOMMISSION;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLossCodes;
		protected System.Web.UI.WebControls.Label capLOBUnassignLossCodes;
		protected System.Web.UI.WebControls.Label capLOBAssignedLossCodes;
		protected System.Web.UI.WebControls.ListBox cmbSTATEAssignLossCodes;
		protected System.Web.UI.WebControls.ListBox cmbRSKAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRSKAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnRSKUnAssignLossCodes;
		protected System.Web.UI.WebControls.Label capRSKUnassignLossCodes;
		protected System.Web.UI.WebControls.Label capRSKAssignedLossCodes;
		protected System.Web.UI.WebControls.ListBox cmbLOBAssignLossCodes;
        protected System.Web.UI.WebControls.Label capSTATE_ID;
        protected System.Web.UI.WebControls.Label capSTATEAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlTableRow Tr1;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnLOBAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnLOBUnAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlTableRow Tr2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSTATEAssignLossCodes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSTATEUnAssignLossCodes;
		protected System.Web.UI.WebControls.ListBox cmbRISK_EXPOSURE;
		protected System.Web.UI.WebControls.ListBox cmbCONTRACT_LOB;
		protected System.Web.UI.WebControls.ListBox cmbSTATE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRISK_EXPOSURE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRISK_EXPOSURE_OLD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_LOB_OLD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID_OLD;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCASH_CALL_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMMISSION;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPER_OCCURRENCE_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revANNUAL_AGGREGATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDEPOSIT_PREMIUMS;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCASH_CALL_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMINIMUM_PREMIUM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSEQUENCE_NUMBER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFOLLOW_UP_FIELDS;
		protected System.Web.UI.WebControls.HyperLink hlkFOLLOW_UP_FIELDS;
        protected System.Web.UI.WebControls.CompareValidator cpvDate;  //Changed by Aditya for TFS bug # 923
		//protected System.Web.UI.WebControls.RangeValidator rngCOMMISSION;
		protected System.Web.UI.HtmlControls.HtmlTableRow 	tblBody;
        
		protected System.Web.UI.WebControls.Label capFOLLOW_UP_FOR;
		protected System.Web.UI.WebControls.DropDownList cmbFOLLOW_UP_FOR;
        protected System.Web.UI.WebControls.Label capMessages;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_XOL_TYPE;
		//Defining the business layer class object
		ClsReinsuranceInformation  objReinsuranceInformation ;
		//END:*********** Local variables *************

		#endregion
		# endregion DECELARATION

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region set attributs
			//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			btnReset.Attributes.Add("onclick","javascript:return Reset();");

			hlkEffectiveDate.Attributes.Add("OnClick","fPopCalendar(document.MNT_REINSURANCE_CONTRACT.txtEFFECTIVE_DATE,document.MNT_REINSURANCE_CONTRACT.txtEFFECTIVE_DATE)");
			hlkExpirationDate.Attributes.Add("OnClick","fPopCalendar(document.MNT_REINSURANCE_CONTRACT.txtEXPIRATION_DATE,document.MNT_REINSURANCE_CONTRACT.txtEXPIRATION_DATE);");

			this.hlkORIGINAL_CONTACT_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_REINSURANCE_CONTRACT.txtORIGINAL_CONTACT_DATE,document.MNT_REINSURANCE_CONTRACT.txtORIGINAL_CONTACT_DATE)");
			this.hlkTERMINATION_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_REINSURANCE_CONTRACT.txtTERMINATION_DATE,document.MNT_REINSURANCE_CONTRACT.txtTERMINATION_DATE)");
			this.hlkFOLLOW_UP_FIELDS.Attributes.Add("OnClick","fPopCalendar(document.MNT_REINSURANCE_CONTRACT.txtFOLLOW_UP_FIELDS,document.MNT_REINSURANCE_CONTRACT.txtFOLLOW_UP_FIELDS)");
			this.cmbRISK_EXPOSURE.Attributes.Add("ondblclick","javascript:RSKAssignLossCodes();");
			cmbRSKAssignLossCodes.Attributes.Add("ondblclick","javascript:RSKUnAssignLossCodes();");				
			this.cmbCONTRACT_LOB.Attributes.Add("ondblclick","javascript:LOBAssignLossCodes();");
			cmbLOBAssignLossCodes.Attributes.Add("ondblclick","javascript:LOBUnAssignLossCodes();");				
			this.cmbSTATE_ID.Attributes.Add("ondblclick","javascript:STATEAssignLossCodes();");
			cmbSTATEAssignLossCodes.Attributes.Add("ondblclick","javascript:STATEUnAssignLossCodes();");
            btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
			btnSave.Attributes.Add("onClick","RSKCountAssignLossCodes();LOBCountAssignLossCodes();STATECountAssignLossCodes();");

            txtPER_OCCURRENCE_LIMIT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtANNUAL_AGGREGATE.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtDEPOSIT_PREMIUMS.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtMINIMUM_PREMIUM.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
			
            txtCASH_CALL_LIMIT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");

            txtCOMMISSION.Attributes.Add("onBlur", "this.value=formatRateBase(this.value,4);");
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			#endregion
			string url = ClsCommon.GetLookupURL();
			//imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','COVERAGE_ID','COVERAGE_CODE','hidCOVERAGE_ID','txtCOVERAGE_CODE','CoverageCode','Coverage Code','');");
					
			base.ScreenId="262_0";
			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");

			# region START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			this.btnActivate.CmsButtonClass		=	CmsButtonType.Write;
			btnActivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			this.btnDelete.CmsButtonClass			=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			# endregion //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.ReinsuranceInfo" ,System.Reflection.Assembly.GetExecutingAssembly());
			# region POST BACK EVENT HANDLER
			if(!Page.IsPostBack)
			{
				if (Request.Params["CONTRACT_ID"] != null)
				{
					if (Request.Params["CONTRACT_ID"].ToString() != "")
					{
						
						hidCONTRACT_ID.Value = Request.Params["CONTRACT_ID"].ToString();
						GenerateXML(hidCONTRACT_ID.Value);
						this.btnActivate.Visible=true;
						this.btnDelete.Visible=true;
					}
				}

				FillCombos();
                BindInstallments(); //Added by Aditya for TFS BUG # 2512
				SetCaptions();

                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddReinsuranceinfo.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddReinsuranceinfo.xml");
                }
				GetDataForEditMode();
				//GetOldDataXML();
			}
			# endregion POST BACK EVENT HANDLER
		}//end pageload
		#endregion
		
		# region GENERATE XML
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string CONTRACT_ID)
		{
			string strCONTRACT_ID=CONTRACT_ID;
            
			objReinsuranceInformation=new ClsReinsuranceInformation(); 
  
			
			if(strCONTRACT_ID!="" && strCONTRACT_ID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objReinsuranceInformation.GetDataForPageControls(strCONTRACT_ID);
					hidOldData.Value=ds.GetXml();

                    if (ds.Tables[0].Rows[0]["CONTRACT_TYPE"] != null)
                    {
                        hidIS_XOL_TYPE.Value = ds.Tables[0].Rows[0]["CONTRACT_TYPE"].ToString();
                    }

                    NfiBaseCurrency.NumberDecimalDigits = 4;
                    if (ds.Tables[0].Rows[0]["COMMISSION"].ToString() != null && ds.Tables[0].Rows[0]["COMMISSION"].ToString() != "")
                        txtCOMMISSION.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["COMMISSION"].ToString()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();              
                    else
                        txtCOMMISSION.Text = "";
					//hidFormSaved.Value="1"; 
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
					if(objReinsuranceInformation!= null)
						objReinsuranceInformation.Dispose();
				}  
                
			}
                
		}

		# endregion GENERATE XML

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private DataSet RemoveDataTable(DataSet ds)
		{
			if(ds.Tables.Count >1)
			{
				int index = 0;

				foreach(DataTable dt in ds.Tables)
				{
					if(index != 0)
						ds.Tables.Remove(dt);
					index++;
				}
			}
			return ds;
		}

		private void GetDataForEditMode()
		{
			try
			{
				objReinsuranceInformation = new ClsReinsuranceInformation();
				DataSet oDs = objReinsuranceInformation.GetDataForPageControls(this.hidCONTRACT_ID.Value);
				//DataSet oDsTemp = objReinsuranceInformation.GetDataForPageControls(this.hidCONTRACT_ID.Value);
				
				
				/*int x=oDs.Tables.Count;
				foreach(DataTable tbl in oDs.Tables)
				{
					string str=tbl.TableName.ToString();
				}
				*/
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					//hidOldData.Value = oDs.GetXml();//RemoveDataTable(oDsTemp).GetXml();
					
					# region POPULATE TEXTBOX	
					this.txtCONTRACT_NUMBER.Text=oDr["CONTRACT_NUMBER"].ToString();
					this.txtCONTRACT_DESC.Text=oDr["CONTRACT_DESC"].ToString();
                    this.txtORIGINAL_CONTACT_DATE.Text=oDr["ORIGINAL_CONTACT_DATE"].ToString();//oDr["ORIGINAL_CONTACT_DATE"].ToString();
                    this.txtEFFECTIVE_DATE.Text=oDr["EFFECTIVE_DATE"].ToString();//oDr["EFFECTIVE_DATE"].ToString();
                    this.txtEXPIRATION_DATE.Text=oDr["EXPIRATION_DATE"].ToString();//oDr["EXPIRATION_DATE"].ToString();
					this.txtCOMMISSION.Text=oDr["COMMISSION"].ToString();
					this.txtPER_OCCURRENCE_LIMIT.Text=oDr["PER_OCCURRENCE_LIMIT"].ToString();
					this.txtANNUAL_AGGREGATE.Text=oDr["ANNUAL_AGGREGATE"].ToString();
					this.txtDEPOSIT_PREMIUMS.Text=oDr["DEPOSIT_PREMIUMS"].ToString();
					this.txtMINIMUM_PREMIUM.Text=oDr["MINIMUM_PREMIUM"].ToString();
					this.txtSEQUENCE_NUMBER.Text=oDr["SEQUENCE_NUMBER"].ToString();
                    this.txtTERMINATION_DATE.Text = oDr["TERMINATION_DATE"].ToString();
					this.txtTERMINATION_REASON.Text=oDr["TERMINATION_REASON"].ToString();
					this.txtCOMMENTS.Text=oDr["COMMENTS"].ToString();
					this.txtFOLLOW_UP_FIELDS.Text=oDr["FOLLOW_UP_FIELDS"].ToString();
                    if (oDr["CASH_CALL_LIMIT"].ToString() != "")
                    {
                        NfiBaseCurrency.NumberDecimalDigits = 2;
                        this.txtCASH_CALL_LIMIT.Text = Convert.ToDouble(oDr["CASH_CALL_LIMIT"]).ToString("N", NfiBaseCurrency);
                    }
					# endregion
					# region POPULATE COMBO BOX 
					//call the function to populate multiselect list item
					populateMultiSelect();

					ListItem li = new ListItem(); 

					li = this.cmbCONTRACT_TYPE.Items.FindByValue(oDr["CONTRACT_TYPE"].ToString());
					cmbCONTRACT_TYPE.SelectedIndex = cmbCONTRACT_TYPE.Items.IndexOf(li);
					
					li = this.cmbLOSS_ADJUSTMENT_EXPENSE.Items.FindByValue(oDr["LOSS_ADJUSTMENT_EXPENSE"].ToString());
					cmbLOSS_ADJUSTMENT_EXPENSE.SelectedIndex = cmbLOSS_ADJUSTMENT_EXPENSE.Items.IndexOf(li);
					
					if(this.cmbCONTACT_YEAR.Items.FindByValue(oDr["CONTACT_YEAR"].ToString())==null)
					{
						cmbCONTACT_YEAR.Items.Insert(0,new ListItem(oDr["CONTACT_YEAR"].ToString(),oDr["CONTACT_YEAR"].ToString())); 
					}
					li = this.cmbCONTACT_YEAR.Items.FindByValue(oDr["CONTACT_YEAR"].ToString());
					cmbCONTACT_YEAR.SelectedIndex = cmbCONTACT_YEAR.Items.IndexOf(li);
							
											
					li = this.cmbCALCULATION_BASE.Items.FindByValue(oDr["CALCULATION_BASE"].ToString());
					cmbCALCULATION_BASE.SelectedIndex = cmbCALCULATION_BASE.Items.IndexOf(li);
					
					li = this.cmbDEPOSIT_PREMIUM_PAYABLE.Items.FindByValue(oDr["DEPOSIT_PREMIUM_PAYABLE"].ToString());
					cmbDEPOSIT_PREMIUM_PAYABLE.SelectedIndex = cmbDEPOSIT_PREMIUM_PAYABLE.Items.IndexOf(li);
					
					li = this.cmbCOMMISSION_APPLICABLE.Items.FindByValue(oDr["COMMISSION_APPLICABLE"].ToString());
					cmbCOMMISSION_APPLICABLE.SelectedIndex = cmbCOMMISSION_APPLICABLE.Items.IndexOf(li);
					
					li = this.cmbREINSURANCE_PREMIUM_ACCOUNT.Items.FindByValue(oDr["REINSURANCE_PREMIUM_ACCOUNT"].ToString());
					cmbREINSURANCE_PREMIUM_ACCOUNT.SelectedIndex = cmbREINSURANCE_PREMIUM_ACCOUNT.Items.IndexOf(li);
					
					li = this.cmbREINSURANCE_PAYABLE_ACCOUNT.Items.FindByValue(oDr["REINSURANCE_PAYABLE_ACCOUNT"].ToString());
					cmbREINSURANCE_PAYABLE_ACCOUNT.SelectedIndex = cmbREINSURANCE_PAYABLE_ACCOUNT.Items.IndexOf(li);
					
					li = this.cmbREINSURANCE_COMMISSION_ACCOUNT.Items.FindByValue(oDr["REINSURANCE_COMMISSION_ACCOUNT"].ToString());
					cmbREINSURANCE_COMMISSION_ACCOUNT.SelectedIndex = cmbREINSURANCE_COMMISSION_ACCOUNT.Items.IndexOf(li);
					
					li = this.cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.Items.FindByValue(oDr["REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT"].ToString());
					cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.SelectedIndex = cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.Items.IndexOf(li);

					li = this.cmbFOLLOW_UP_FOR.Items.FindByValue(oDr["FOLLOW_UP_FOR"].ToString());
					cmbFOLLOW_UP_FOR.SelectedIndex = cmbFOLLOW_UP_FOR.Items.IndexOf(li);

                    li = this.cmbMAX_NO_INSTALLMENT.Items.FindByValue(oDr["MAX_NO_INSTALLMENT"].ToString());  //Added by Aditya for TFS BUG # 2512
                    cmbMAX_NO_INSTALLMENT.SelectedIndex = cmbMAX_NO_INSTALLMENT.Items.IndexOf(li);
					# endregion COMBO BOX
					# region IS ACTIVE SETTING
                    if (oDr["IS_ACTIVE"].ToString() == "Y")
                    {
                        btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");//"Deactivate";
                        this.btnDelete.Visible = true;
                       
                    }
                    if (oDr["IS_ACTIVE"].ToString() == "N")
                    {
                        btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");//"Activate";
                        this.btnDelete.Visible = false;
                       

                    }
					# endregion

					
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}
		
		
		# endregion G E T   D A T A   F O R   E D I T   M O D E

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
				objReinsuranceInformation = new ClsReinsuranceInformation();

				//Retreiving the form values into model class object
				ClsReinsuranceInfo objReinsuranceInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objReinsuranceInfo.CREATED_BY = int.Parse(GetUserId());
					objReinsuranceInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objReinsuranceInformation.Add(objReinsuranceInfo);

					if(intRetVal>0)
					{
                        hidIS_XOL_TYPE.Value = objReinsuranceInfo.CONTRACT_TYPE.ToString();
						hidCONTRACT_ID.Value = objReinsuranceInfo.CONTRACT_ID.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
						hidFormSaved.Value			=	"1";
						hidOldData.Value = ClsReinsuranceInformation.GetDataSetForPageControls(hidCONTRACT_ID.Value).GetXml();
						hidIS_ACTIVE.Value = "Y";
						this.btnActivate.Visible=true;
						this.btnDelete.Visible=true;
						populateMultiSelect();
					}
					else if(intRetVal == -1)
					{
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		"Information could not be Saved, Information rolled back";
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=ClsMessages.FetchGeneralMessage("29");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsReinsuranceInfo objOldReinsuranceInfo;
					objOldReinsuranceInfo = new ClsReinsuranceInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceInfo,hidOldData.Value);

					objOldReinsuranceInfo.RISK_EXPOSURE = hidRISK_EXPOSURE_OLD.Value;
					objOldReinsuranceInfo.CONTRACT_LOB = hidCONTRACT_LOB_OLD.Value;
					objOldReinsuranceInfo.STATE_ID = hidSTATE_ID_OLD.Value;

					//Setting those values into the Model object which are not in the page
					if(strRowId!="")
					objReinsuranceInfo.CONTRACT_ID = int.Parse(strRowId);
					objReinsuranceInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReinsuranceInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= objReinsuranceInformation.Update(objOldReinsuranceInfo,objReinsuranceInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
                        hidIS_XOL_TYPE.Value = objReinsuranceInfo.CONTRACT_TYPE.ToString();
                       
                        lblMessage.Text         = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value = ClsReinsuranceInformation.GetDataSetForPageControls(strRowId).GetXml();
						populateMultiSelect();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
                        lblMessage.Text         =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
                        lblMessage.Text         = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				
				if(objReinsuranceInformation!= null)
					objReinsuranceInformation.Dispose();
			}
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/*
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objReinsuranceInformation =  new ClsReinsuranceInformation();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objReinsuranceInformation.TransactionInfoParams = objStuTransactionInfo;
					objReinsuranceInformation.ActivateDeactivate(hidCONTRACT_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objReinsuranceInformation.TransactionInfoParams = objStuTransactionInfo;
					objReinsuranceInformation.ActivateDeactivate(hidCONTRACT_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"1";
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objReinsuranceInformation!= null)
					objReinsuranceInformation.Dispose();
			}
		}
		*/
		#endregion

		#region DEACTIVATE ACTIVATE BUTTON CLICK
		private void btnActivate_Click(object sender, System.EventArgs e)
		{
			
			objReinsuranceInformation = new  ClsReinsuranceInformation();
			ClsReinsuranceInfo objReinsuraneContractInfo= GetFormValue();
			objReinsuraneContractInfo.MODIFIED_BY = int.Parse(GetUserId());

			objReinsuraneContractInfo.RISK_TLOG = hidRISK_EXPOSURE_OLD.Value;
			objReinsuraneContractInfo.LOB_TLOG = hidCONTRACT_LOB_OLD.Value;
			objReinsuraneContractInfo.STATE_TLOG = hidSTATE_ID_OLD.Value;

            if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
            {

                int intStatusCheck = objReinsuranceInformation.GetDeactivateActivate(objReinsuraneContractInfo, this.hidCONTRACT_ID.Value.ToString(), "N");
                btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");//"Activate";
                this.btnDelete.Visible = false;
                lblMessage.Text = ClsMessages.GetMessage("G", "41");
                lblMessage.Visible = true;
                hidFormSaved.Value = "1";
                hidIS_ACTIVE.Value = "N";
            }

            else
            {

                int intStatusCheck = objReinsuranceInformation.GetDeactivateActivate(objReinsuraneContractInfo, this.hidCONTRACT_ID.Value.ToString(), "Y");
                btnActivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");//"Deactivate";
                this.btnDelete.Visible = true;
                lblMessage.Text = ClsMessages.GetMessage("G", "40");
                lblMessage.Visible = true;
                hidFormSaved.Value = "1";
                hidIS_ACTIVE.Value = "Y";

            }
		
		}
		
		
		#endregion

		# region DELETE RECORD
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				objReinsuranceInformation = new ClsReinsuranceInformation();
				ClsReinsuranceInfo objReinsuraneContractInfo= GetFormValue();
				objReinsuraneContractInfo.RISK_TLOG = hidRISK_EXPOSURE_OLD.Value;
				objReinsuraneContractInfo.LOB_TLOG = hidCONTRACT_LOB_OLD.Value;
				objReinsuraneContractInfo.STATE_TLOG = hidSTATE_ID_OLD.Value;
				objReinsuraneContractInfo.MODIFIED_BY= int.Parse(GetUserId());
				int intStatusCheck=objReinsuranceInformation.Delete(objReinsuraneContractInfo,this.hidCONTRACT_ID.Value.ToString());
                if (intStatusCheck == -4)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "37");
                    lblMessage.Visible = true;
                    hidFormSaved.Value = "2";
                }
                else
                {

                    lblDelete.Visible = true;
                    hidFormSaved.Value = "5";
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21");
                    lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "36");//"Record has been deleted successfully";
                    hidOldData.Value = "";
                    tblBody.Attributes.Add("style", "display:none");
                }
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	"Could not be deleted.Error while Deleting. Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objReinsuranceInformation!= null)
					objReinsuranceInformation.Dispose();
			}
		
		}
		
		# endregion

		#region UTILITY METHODS
		
		/// <summary>
		/// Sets the captions in the labels against each control of the page.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetCaptions()
        {
            capLOBUnassignLossCodes.Text = objResourceMgr.GetString("capLOBUnassignLossCodes");
            capLOBAssignedLossCodes.Text = objResourceMgr.GetString("capLOBAssignedLossCode");
			capCONTRACT_TYPE.Text						=		objResourceMgr.GetString("cmbCONTRACT_TYPE");
			capCONTRACT_NUMBER.Text						=		objResourceMgr.GetString("txtCONTRACT_NUMBER");
			capCONTRACT_DESC.Text						=		objResourceMgr.GetString("txtCONTRACT_DESC");
			this.capLOSS_ADJUSTMENT_EXPENSE.Text		=		objResourceMgr.GetString("cmbLOSS_ADJUSTMENT_EXPENSE");
			//this.capRISK_EXPOSURE.Text					=		objResourceMgr.GetString("cmbRISK_EXPOSURE");
			//capCONTRACT_LOB.Text						=		objResourceMgr.GetString("cmbCONTRACT_LOB");
			//capSTATE_ID.Text							=		objResourceMgr.GetString("cmbSTATE_ID");
			this.capORIGINAL_CONTACT_DATE.Text			=		objResourceMgr.GetString("txtORIGINAL_CONTACT_DATE");
			this.capCONTACT_YEAR.Text					=		objResourceMgr.GetString("cmbCONTACT_YEAR");
			this.capEFFECTIVE_DATE.Text					=		objResourceMgr.GetString("txtEFFECTIVE_DATE");
			this.capEXPIRATION_DATE.Text				=		objResourceMgr.GetString("txtEXPIRATION_DATE");
			this.capCOMMISSION.Text						=		objResourceMgr.GetString("txtCOMMISSION");
			this.capFOLLOW_UP_FOR.Text					=		objResourceMgr.GetString("cmbFOLLOW_UP_FOR");
			this.capCALCULATION_BASE.Text				=		objResourceMgr.GetString("cmbCALCULATION_BASE");
			this.capPER_OCCURRENCE_LIMIT.Text			=		objResourceMgr.GetString("txtPER_OCCURRENCE_LIMIT");
			this.capANNUAL_AGGREGATE.Text				=		objResourceMgr.GetString("txtANNUAL_AGGREGATE");
			this.capDEPOSIT_PREMIUMS.Text				=		objResourceMgr.GetString("txtDEPOSIT_PREMIUMS");
			this.capDEPOSIT_PREMIUM_PAYABLE.Text		=		objResourceMgr.GetString("cmbDEPOSIT_PREMIUM_PAYABLE");
			this.capMINIMUM_PREMIUM.Text				=		objResourceMgr.GetString("txtMINIMUM_PREMIUM");
			this.capSEQUENCE_NUMBER.Text				=		objResourceMgr.GetString("txtSEQUENCE_NUMBER");
			this.capTERMINATION_DATE.Text				=		objResourceMgr.GetString("txtTERMINATION_DATE");
			this.capTERMINATION_REASON.Text				=		objResourceMgr.GetString("txtTERMINATION_REASON");
			this.capCOMMENTS.Text						=		objResourceMgr.GetString("txtCOMMENTS");
			this.capFOLLOW_UP_FIELDS.Text				=		objResourceMgr.GetString("txtFOLLOW_UP_FIELDS");
			this.capCOMMISSION_APPLICABLE.Text			=		objResourceMgr.GetString("cmbCOMMISSION_APPLICABLE");
			this.capREINSURANCE_PREMIUM_ACCOUNT.Text	=		objResourceMgr.GetString("cmbREINSURANCE_PREMIUM_ACCOUNT");
			this.capREINSURANCE_PAYABLE_ACCOUNT.Text	=		objResourceMgr.GetString("cmbREINSURANCE_PAYABLE_ACCOUNT");
			this.capREINSURANCE_COMMISSION_ACCOUNT.Text	=		objResourceMgr.GetString("cmbREINSURANCE_COMMISSION_ACCOUNT");
			this.capREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.Text	=objResourceMgr.GetString("cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT");
            this.capCASH_CALL_LIMIT.Text = objResourceMgr.GetString("txtCASH_CALL_LIMIT");
            capRSKUnassignLossCodes.Text = objResourceMgr.GetString("capRSKUnassignLossCodes"); //sneha
            capRSKAssignedLossCodes.Text = objResourceMgr.GetString("capRSKAssignedLossCodes"); //sneha
            capSTATE_ID.Text = objResourceMgr.GetString("capSTATEUnassignLossCodes"); //sneha
            capSTATEAssignLossCodes.Text = objResourceMgr.GetString("capSTATEAssignedLossCodes"); //sneha
            capPREMIUM_COMMISION.Text = objResourceMgr.GetString("capPREMIUM_COMMISION"); //sneha
            capPREMIUN_SECTION.Text = objResourceMgr.GetString("capPREMIUN_SECTION"); //sneha
            capCOMMISSION_SECTION.Text =objResourceMgr.GetString("capCOMMISSION_SECTION"); //sneha
            CapAddLookup.Text = objResourceMgr.GetString("hidLookup");
            capMAX_NO_INSTALLMENT.Text = objResourceMgr.GetString("cmbMAX_NO_INSTALLMENT");
        }
		
		
		private void GetOldDataXML()
		{
			if ( Request.Params.Count != 0 ) 
			{
				if(Request.QueryString["CONTRACT_ID"]!=null && Request.QueryString["CONTRACT_ID"].ToString().Length>0)
				{
					//ShowData();				
				}
			}			
		}

		
		/// <summary>
		/// Fills all the combo controls of the page.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void FillCombos()
		{
			
			try
			{
				objReinsuranceInformation=new ClsReinsuranceInformation();
	
				DataTable tblAsset=objReinsuranceInformation.GetDifferentAccount("1","1");
				this.cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.DataSource=tblAsset;
				cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.DataTextField ="ACC_DESCRIPTION";
				cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.DataValueField ="ACCOUNT_ID";
				cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.DataBind();
				cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.Items.Insert(0,"");

				DataTable tblLiability=objReinsuranceInformation.GetDifferentAccount("1","2");
				this.cmbREINSURANCE_PAYABLE_ACCOUNT.DataSource=tblLiability;
				cmbREINSURANCE_PAYABLE_ACCOUNT.DataTextField ="ACC_DESCRIPTION";
				cmbREINSURANCE_PAYABLE_ACCOUNT.DataValueField ="ACCOUNT_ID";
				cmbREINSURANCE_PAYABLE_ACCOUNT.DataBind();
				cmbREINSURANCE_PAYABLE_ACCOUNT.Items.Insert(0,"");

				DataTable tblIncome=objReinsuranceInformation.GetDifferentAccount("1","4");
				this.cmbREINSURANCE_PREMIUM_ACCOUNT.DataSource=tblIncome;
				cmbREINSURANCE_PREMIUM_ACCOUNT.DataTextField ="ACC_DESCRIPTION";
				cmbREINSURANCE_PREMIUM_ACCOUNT.DataValueField ="ACCOUNT_ID";
				cmbREINSURANCE_PREMIUM_ACCOUNT.DataBind();
				cmbREINSURANCE_PREMIUM_ACCOUNT.Items.Insert(0,"");

				DataTable tblExpense=objReinsuranceInformation.GetDifferentAccount("1","5");
				this.cmbREINSURANCE_COMMISSION_ACCOUNT.DataSource=tblExpense;
				cmbREINSURANCE_COMMISSION_ACCOUNT.DataTextField ="ACC_DESCRIPTION";
				cmbREINSURANCE_COMMISSION_ACCOUNT.DataValueField ="ACCOUNT_ID";
				cmbREINSURANCE_COMMISSION_ACCOUNT.DataBind();
				cmbREINSURANCE_COMMISSION_ACCOUNT.Items.Insert(0,"");

				//select ACCOUNT_ID,convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION

				//State
				DataTable dt = Cms.CmsWeb.ClsFetcher.State;
				this.cmbSTATE_ID.DataSource		= dt;
				cmbSTATE_ID.DataTextField	= "State_Name";
				cmbSTATE_ID.DataValueField	= "State_Id";
				cmbSTATE_ID.DataBind();
				//cmbSTATE_ID.Items.Insert(0,"");

				//Reinsurance Contract Type
				//dt = Cms.CmsWeb.ClsFetcher.ReinsuranceContractType;
				DataTable tblReinsuranceContactType=objReinsuranceInformation.GetReinsuranceContractType();
				this.cmbCONTRACT_TYPE.DataSource		= tblReinsuranceContactType;
				cmbCONTRACT_TYPE.DataTextField	= "CONTRACT_TYPE_DESC";
				cmbCONTRACT_TYPE.DataValueField	= "CONTRACTTYPEID";
				cmbCONTRACT_TYPE.DataBind();
				cmbCONTRACT_TYPE.Items.Insert(0,"");
				
				cmbCONTACT_YEAR.Items.Add(new ListItem((DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString()));
				cmbCONTACT_YEAR.Items.Add(new ListItem((DateTime.Now.Year+1).ToString(), (DateTime.Now.Year+1).ToString()));
				cmbCONTACT_YEAR.Items.Add(new ListItem((DateTime.Now.Year+2).ToString(), (DateTime.Now.Year+2).ToString()));
				cmbCONTACT_YEAR.Items.Add(new ListItem((DateTime.Now.Year+3).ToString(), (DateTime.Now.Year+3).ToString()));
				cmbCONTACT_YEAR.Items.Add(new ListItem((DateTime.Now.Year+4).ToString(), (DateTime.Now.Year+4).ToString()));

				/*DataTable tblReinsuranceContactyear=objReinsuranceInformation.GetReinsuranceContractYear();
				this.cmbCONTACT_YEAR.DataSource		= tblReinsuranceContactyear;
				cmbCONTACT_YEAR.DataTextField	= "LOOKUP_VALUE_DESC";
				cmbCONTACT_YEAR.DataValueField	= "LOOKUP_VALUE_CODE";
				cmbCONTACT_YEAR.DataBind();
				cmbCONTACT_YEAR.Items.Insert(0,"");*/

				/*DataSet dsYear=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.FetchYearsFromVINMASTER();
				DataView dvYear= new DataView(dsYear.Tables[0]);
				dvYear.RowFilter="MODEL_YEAR>=2005";
				cmbCONTACT_YEAR.DataSource=dvYear;
			    cmbCONTACT_YEAR.DataTextField="MODEL_YEAR";
				cmbCONTACT_YEAR.DataValueField="MODEL_YEAR";
				cmbCONTACT_YEAR.DataBind();
				cmbCONTACT_YEAR.Items.Insert(0,new ListItem("",""));
				cmbCONTACT_YEAR.SelectedIndex=0;*/
 
				
				// Reinsurer/Broker
				//ClsChecks.GetReinsuranceCompaniesInDropDown(cmbBROKERID) ;
				//cmbBROKERID.Items.Insert(0,"");
			
				//Contract Name
				//ClsReinsuranceInformation.GetReinsuranceContractsInDropDown(cmbCONTRACT_NAME_ID);
				//cmbCONTRACT_NAME_ID.Items.Insert(0,"");

				//Line of Business
				dt = Cms.CmsWeb.ClsFetcher.LOBs ;
				cmbCONTRACT_LOB.DataSource		= dt;
				cmbCONTRACT_LOB.DataTextField	= "LOB_DESC";
				cmbCONTRACT_LOB.DataValueField	= "LOB_ID";
				cmbCONTRACT_LOB.DataBind();

				//Remove General Liability
				ListItem Li = cmbCONTRACT_LOB.Items.FindByValue("7");//"7" -> General Liability
				if (Li != null)	
				{
					cmbCONTRACT_LOB.Items.Remove(Li);	
				}
				//cmbCONTRACT_LOB.Items.Insert(0,"");

				//Subline
				/*
				dt = Cms.CmsWeb.ClsFetcher.SublineCode ;
				cmbSUBLINE_CODE.DataSource		= dt;
				cmbSUBLINE_CODE.DataTextField	= "SUBLINE";
				cmbSUBLINE_CODE.DataValueField	= "SUBLINE_CODE_ID";
				cmbSUBLINE_CODE.DataBind();
				cmbSUBLINE_CODE.Items.Insert(0,"");
				*/

				//Calculation Based On				
				cmbCALCULATION_BASE.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CALC"); 
				cmbCALCULATION_BASE.DataTextField	= "LookupDesc";
				cmbCALCULATION_BASE.DataValueField	= "LookupId";
				cmbCALCULATION_BASE.DataBind();
				cmbCALCULATION_BASE.Items.Insert(0,"");

				this.cmbLOSS_ADJUSTMENT_EXPENSE.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("REA"); 
				cmbLOSS_ADJUSTMENT_EXPENSE.DataTextField	= "LookupDesc";
				cmbLOSS_ADJUSTMENT_EXPENSE.DataValueField	= "LookupId";
				cmbLOSS_ADJUSTMENT_EXPENSE.DataBind();
				cmbLOSS_ADJUSTMENT_EXPENSE.Items.Insert(0,"");

				this.cmbDEPOSIT_PREMIUM_PAYABLE.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RDP"); 
				cmbDEPOSIT_PREMIUM_PAYABLE.DataTextField	= "LookupDesc";
				cmbDEPOSIT_PREMIUM_PAYABLE.DataValueField	= "LookupId";
				cmbDEPOSIT_PREMIUM_PAYABLE.DataBind();
				cmbDEPOSIT_PREMIUM_PAYABLE.Items.Insert(0,"");

				this.cmbCOMMISSION_APPLICABLE.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO"); 
				cmbCOMMISSION_APPLICABLE.DataTextField	= "LookupDesc";
				cmbCOMMISSION_APPLICABLE.DataValueField	= "LookupCode";
				cmbCOMMISSION_APPLICABLE.DataBind();
				cmbCOMMISSION_APPLICABLE.Items.Insert(0,"");

				this.cmbRISK_EXPOSURE.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RRE"); 
				cmbRISK_EXPOSURE.DataTextField	= "LookupDesc";
				cmbRISK_EXPOSURE.DataValueField	= "LookupId";
				cmbRISK_EXPOSURE.DataBind();
				//cmbRISK_EXPOSURE.Items.Insert(0,"");


				DataTable tblFollowUp=objReinsuranceInformation.GetCarrierUsers();
				this.cmbFOLLOW_UP_FOR.DataSource=tblFollowUp;
				cmbFOLLOW_UP_FOR.DataTextField ="USER_NAME";
				cmbFOLLOW_UP_FOR.DataValueField ="USER_ID";
				cmbFOLLOW_UP_FOR.DataBind();
				cmbFOLLOW_UP_FOR.Items.Insert(0,"");
				

			}
			finally
			{
			
			}

		}

        private void BindInstallments()  //Added by Aditya for TFS BUG # 2512
        {
            cmbMAX_NO_INSTALLMENT.Items.Add("");
            cmbMAX_NO_INSTALLMENT.Items.Add("1");
            cmbMAX_NO_INSTALLMENT.Items.Add("2");
            cmbMAX_NO_INSTALLMENT.Items.Add("3");
            cmbMAX_NO_INSTALLMENT.Items.Add("4");
            cmbMAX_NO_INSTALLMENT.Items.Add("5");
            cmbMAX_NO_INSTALLMENT.Items.Add("6");
            cmbMAX_NO_INSTALLMENT.Items.Add("7");
            cmbMAX_NO_INSTALLMENT.Items.Add("8");
            cmbMAX_NO_INSTALLMENT.Items.Add("9");
            cmbMAX_NO_INSTALLMENT.Items.Add("10");
            cmbMAX_NO_INSTALLMENT.Items.Add("11");
            cmbMAX_NO_INSTALLMENT.Items.Add("12");
        }
		
		/// <summary>
		/// Display page's values on the different controls of the page.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		/*
		private void ShowData()
		{
			try
			{

				DataSet dsContractDetails;
				dsContractDetails =ClsReinsuranceInformation.GetDataSetForPageControls(Request.QueryString["CONTRACT_ID"].ToString());
				if (dsContractDetails!=null)
				{
					DataTable dtContractDetails=dsContractDetails.Tables[0];
					hidOldData.Value = dsContractDetails.GetXml();
					hidCONTRACT_ID.Value = Default.GetString(dtContractDetails.Rows[0]["CONTRACT_ID"]);

					//Reinsurance Contract Type					 
					ClsCommon.SelectValueinDDL(cmbCONTRACT_TYPE,dtContractDetails.Rows[0]["CONTRACT_TYPE"]);

					//Description
					txtCONTRACT_DESC.Text = Default.GetString(dtContractDetails.Rows[0]["CONTRACT_DESC"]);

					//Contract Number
					txtCONTRACT_NUMBER.Text = Default.GetString(dtContractDetails.Rows[0]["CONTRACT_NUMBER"]);

					//Contract Name["CONTRACT_NAME_ID"].ToString());
					ClsCommon.SelectValueinDDL(cmbCONTRACT_NAME_ID,dtContractDetails.Rows[0]["CONTRACT_NAME_ID"]);


					//Reinsurer/Broker
					ClsCommon.SelectValueinDDL(cmbBROKERID,dtContractDetails.Rows[0]["BROKERID"]);


					//Line of Business
					ClsCommon.SelectValueinDDL(cmbCONTRACT_LOB,dtContractDetails.Rows[0]["CONTRACT_LOB"]);


					//State
					ClsCommon.SelectValueinDDL(cmbSTATE_ID,dtContractDetails.Rows[0]["STATE_ID"]);


					//Effective Date
					txtEFFECTIVE_DATE.Text = Default.GetDateFromString(dtContractDetails.Rows[0]["EFFECTIVE_DATE"].ToString()).ToString("MM/dd/yyyy");

					//Expiration Date
					txtEXPIRATION_DATE.Text = Default.GetDateFromString(dtContractDetails.Rows[0]["EXPIRATION_DATE"].ToString()).ToString("MM/dd/yyyy");


					//Reference #
					txtREINSURER_REFERENCE_NUM.Text = Default.GetString(dtContractDetails.Rows[0]["REINSURER_REFERENCE_NUM"]);

					//Underwriting Year
					txtUW_YEAR.Text = Default.GetString(dtContractDetails.Rows[0]["UW_YEAR"]);


					//ASLOB
					txtASLOB.Text = Default.GetString(dtContractDetails.Rows[0]["ASLOB"]);
					
					//Subline
					ClsCommon.SelectValueinDDL(cmbSUBLINE_CODE,dtContractDetails.Rows[0]["SUBLINE_CODE"]);

					//Coverage Code
					txtCOVERAGE_CODE.Text = Default.GetString(dtContractDetails.Rows[0]["COVERAGE_CODE"]);
					
					//Cession %
					txtCESSION.Text = Default.GetString(dtContractDetails.Rows[0]["CESSION"]);

					//Calculation Based On
					ClsCommon.SelectValueinDDL(cmbCALCULATION_BASE,dtContractDetails.Rows[0]["CALCULATION_BASE"]);
				}
			}
			finally{}
		
		}

		*/
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private ClsReinsuranceInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsReinsuranceInfo objReinsuranceInfo;
			objReinsuranceInfo = new ClsReinsuranceInfo();

			objReinsuranceInfo.CONTRACT_TYPE				=	Convert.ToInt32(cmbCONTRACT_TYPE.SelectedValue);
			objReinsuranceInfo.CONTRACT_NUMBER				=	txtCONTRACT_NUMBER.Text;
			objReinsuranceInfo.CONTRACT_DESC				=	txtCONTRACT_DESC.Text;
			objReinsuranceInfo.LOSS_ADJUSTMENT_EXPENSE		=	this.cmbLOSS_ADJUSTMENT_EXPENSE.SelectedValue;
			objReinsuranceInfo.RISK_EXPOSURE				=	this.hidRISK_EXPOSURE.Value;
			objReinsuranceInfo.CONTRACT_LOB					=	this.hidCONTRACT_LOB.Value;
            if (hidSTATE_ID.Value == "7")
                hidSTATE_ID.Value = "92";
			objReinsuranceInfo.STATE_ID						=	this.hidSTATE_ID.Value;

			objReinsuranceInfo.ORIGINAL_CONTACT_DATE		=	ConvertToDate(txtORIGINAL_CONTACT_DATE.Text);
			objReinsuranceInfo.CONTACT_YEAR					=	this.cmbCONTACT_YEAR.SelectedValue;
            objReinsuranceInfo.EFFECTIVE_DATE               = ConvertToDate(txtEFFECTIVE_DATE.Text);
            objReinsuranceInfo.EXPIRATION_DATE              = ConvertToDate(txtEXPIRATION_DATE.Text);
            if (txtCOMMISSION.Text != "")
                objReinsuranceInfo.COMMISSION               = Double.Parse(txtCOMMISSION.Text, NfiBaseCurrency);
         
			objReinsuranceInfo.CALCULATION_BASE				=	Convert.ToInt32(cmbCALCULATION_BASE.SelectedValue);
            if (txtPER_OCCURRENCE_LIMIT.Text != "")
                objReinsuranceInfo.PER_OCCURRENCE_LIMIT     = Double.Parse(txtPER_OCCURRENCE_LIMIT.Text, NfiBaseCurrency).ToString();
            else
                objReinsuranceInfo.PER_OCCURRENCE_LIMIT     = String.Empty;
            if (txtANNUAL_AGGREGATE.Text != "")
                objReinsuranceInfo.ANNUAL_AGGREGATE         = Double.Parse(txtANNUAL_AGGREGATE.Text, NfiBaseCurrency).ToString();
            else
                objReinsuranceInfo.ANNUAL_AGGREGATE         = String.Empty;
            
            if (txtDEPOSIT_PREMIUMS.Text != "")
                objReinsuranceInfo.DEPOSIT_PREMIUMS         = Double.Parse(txtDEPOSIT_PREMIUMS.Text, NfiBaseCurrency).ToString();
            else
                objReinsuranceInfo.DEPOSIT_PREMIUMS         = String.Empty;

			objReinsuranceInfo.DEPOSIT_PREMIUM_PAYABLE		= this.cmbDEPOSIT_PREMIUM_PAYABLE.SelectedValue;
            
            if (txtMINIMUM_PREMIUM.Text != "")
                objReinsuranceInfo.MINIMUM_PREMIUM          = Double.Parse(txtMINIMUM_PREMIUM.Text, NfiBaseCurrency).ToString();
            else
                objReinsuranceInfo.MINIMUM_PREMIUM          = String.Empty;

			objReinsuranceInfo.SEQUENCE_NUMBER				=	txtSEQUENCE_NUMBER.Text;
            objReinsuranceInfo.TERMINATION_DATE             = ConvertToDate(txtTERMINATION_DATE.Text.ToString());//Convert.ToDateTime(txtTERMINATION_DATE.Text);
            
			objReinsuranceInfo.TERMINATION_REASON			=	txtTERMINATION_REASON.Text;
			objReinsuranceInfo.COMMENTS						=	txtCOMMENTS.Text;
			objReinsuranceInfo.FOLLOW_UP_FIELDS				=	this.txtFOLLOW_UP_FIELDS.Text;
            if (cmbCOMMISSION_APPLICABLE.SelectedValue != "")
                objReinsuranceInfo.COMMISSION_APPLICABLE = Convert.ToInt32(this.cmbCOMMISSION_APPLICABLE.SelectedValue);
            else
                objReinsuranceInfo.COMMISSION_APPLICABLE = 0;

            

			objReinsuranceInfo.REINSURANCE_PREMIUM_ACCOUNT	=	this.cmbREINSURANCE_PREMIUM_ACCOUNT.SelectedValue;
			objReinsuranceInfo.REINSURANCE_PAYABLE_ACCOUNT	=	this.cmbREINSURANCE_PAYABLE_ACCOUNT.SelectedValue;
			objReinsuranceInfo.REINSURANCE_COMMISSION_ACCOUNT	=	this.cmbREINSURANCE_COMMISSION_ACCOUNT.SelectedValue;
			objReinsuranceInfo.REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT	=	this.cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.SelectedValue;
			if(this.cmbFOLLOW_UP_FOR.SelectedValue!="")
					objReinsuranceInfo.FOLLOW_UP_FOR				=	Convert.ToInt32(this.cmbFOLLOW_UP_FOR.SelectedValue);
            if(txtCASH_CALL_LIMIT.Text!="")
                objReinsuranceInfo.CASH_CALL_LIMIT = Double.Parse(txtCASH_CALL_LIMIT.Text, NfiBaseCurrency);

            if (cmbMAX_NO_INSTALLMENT.SelectedValue != "")  //Added by Aditya for TFS BUG # 2512
            {
                objReinsuranceInfo.MAX_NO_INSTALLMENT = Convert.ToInt32(cmbMAX_NO_INSTALLMENT.SelectedValue);
            }
									
			/*
			objReinsuranceInfo.REINSURER_REFERENCE_NUM		=	txtREINSURER_REFERENCE_NUM.Text;
			objReinsuranceInfo.UW_YEAR						=	Convert.ToInt32(txtUW_YEAR.Text);
			objReinsuranceInfo.ASLOB						=	Convert.ToInt32(txtASLOB.Text);
			objReinsuranceInfo.SUBLINE_CODE					=	cmbSUBLINE_CODE.SelectedValue;
			objReinsuranceInfo.COVERAGE_CODE				=	txtCOVERAGE_CODE.Text; 
			objReinsuranceInfo.CESSION						=	Convert.ToInt32(txtCESSION.Text);
			objReinsuranceInfo.CALCULATION_BASE				=	Convert.ToInt32(cmbCALCULATION_BASE.SelectedValue);
			objReinsuranceInfo.EFFECTIVE_DATE				=	Convert.ToDateTime(txtEFFECTIVE_DATE.Text);
			objReinsuranceInfo.EXPIRATION_DATE				=	Convert.ToDateTime(txtEXPIRATION_DATE.Text);
			*/
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCONTRACT_ID.Value;
			//oldXML		= hidOldData.Value;
			//Returning the model object

			return objReinsuranceInfo;
		}

			
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{/*
			rfvCONTRACT_TYPE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvCONTRACT_NUMBER.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvCONTRACT_NAME_ID.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvBROKERID.ErrorMessage							=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvCONTRACT_LOB.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvSTATE_ID.ErrorMessage							=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");

			rfvREINSURER_REFERENCE_NUM.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvUW_YEAR.ErrorMessage								=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			rfvASLOB.ErrorMessage								=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			rfvSUBLINE_CODE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvCOVERAGE_CODE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			rfvCESSION.ErrorMessage								=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			rfvCALCULATION_BASE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			revASLOB.ErrorMessage								=  Cms.CmsWeb.ClsMessages.GetMessage("G","163");
			revASLOB.ValidationExpression						=  cmsbase.aRegExpDoublePositiveNonZero;			
			revUW_YEAR.ErrorMessage								=  Cms.CmsWeb.ClsMessages.GetMessage("G","163");
			revUW_YEAR.ValidationExpression						=  cmsbase.aRegExpDoublePositiveNonZero;			
			revCESSION.ValidationExpression						=  aRegExpInteger ;
			revCESSION.ErrorMessage								=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"493");
			csvEXPIRATION_DATE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
			csvEFFECTIVE_DATE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
			csvCESSION.ErrorMessage								=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"18");
		*/
            rfvCONTRACT_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvCONTRACT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvSTATEAssignLossCodes.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");

            rfvSTATEAssignLossCodes.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvLOSS_ADJUSTMENT_EXPENSE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "30");
            rfvRSKAssignLossCodes.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "23");
            rfvLOBAssignLossCodes.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");
            rfvORIGINAL_CONTACT_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "25");
            rfvCOMMISSION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "31");
            rfvCALCULATION_BASE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "32");
            rfvTERMINATION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "26");
           // rfvCOMMISSION_APPLICABLE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "27");
           // rfvCOMMISSION_APPLICABLE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "27");
           // rfvREINSURANCE_PREMIUM_ACCOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "28");
           // rfvREINSURANCE_PAYABLE_ACCOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");
			revEFFECTIVE_DATE.ValidationExpression				=  aRegExpDate;
			revEXPIRATION_DATE.ValidationExpression 			=  aRegExpDate;
			revORIGINAL_CONTACT_DATE.ValidationExpression		= aRegExpDate;
			revTERMINATION_DATE.ValidationExpression			= aRegExpDate;
			revFOLLOW_UP_FIELDS.ValidationExpression			= aRegExpDate;
			revORIGINAL_CONTACT_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			revTERMINATION_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			revEFFECTIVE_DATE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			revEXPIRATION_DATE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			rfvEFFECTIVE_DATE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			rfvEXPIRATION_DATE.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
			revFOLLOW_UP_FIELDS.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			//revCOMMISSION modified by Sibin for Itrack Issue 5397 on 4 Feb 09
            revCOMMISSION.ValidationExpression                  = aRegExpBaseDoublePositiveNonZero;//aRegExpDoublePositiveNonZero;
			revCOMMISSION.ErrorMessage							=  Cms.CmsWeb.ClsMessages.GetMessage("G","163");
            revPER_OCCURRENCE_LIMIT.ValidationExpression        = aRegExpBaseDoublePositiveStartWithDecimal;//aRegExpDoublePositiveStartWithDecimal;
			revPER_OCCURRENCE_LIMIT.ErrorMessage				=Cms.CmsWeb.ClsMessages.GetMessage("G","163");
            revANNUAL_AGGREGATE.ValidationExpression            = aRegExpBaseDoublePositiveStartWithDecimal; // aRegExpDoublePositiveStartWithDecimal;
			revANNUAL_AGGREGATE.ErrorMessage					=Cms.CmsWeb.ClsMessages.GetMessage("G","163");
            revDEPOSIT_PREMIUMS.ValidationExpression            = aRegExpBaseDoublePositiveStartWithDecimal; //aRegExpDoublePositiveStartWithDecimal;
			revDEPOSIT_PREMIUMS.ErrorMessage					=Cms.CmsWeb.ClsMessages.GetMessage("G","163");
            revMINIMUM_PREMIUM.ValidationExpression             = aRegExpBaseDoublePositiveStartWithDecimal; //aRegExpDoublePositiveStartWithDecimal;
			revMINIMUM_PREMIUM.ErrorMessage						=Cms.CmsWeb.ClsMessages.GetMessage("G","163");
			revSEQUENCE_NUMBER.ValidationExpression				= aRegExpInteger;
			revSEQUENCE_NUMBER.ErrorMessage						=Cms.CmsWeb.ClsMessages.GetMessage("G","163");
            revCASH_CALL_LIMIT.ValidationExpression = aRegExpBaseDoublePositiveStartWithDecimal;
            revCASH_CALL_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G", "163");
            csvCOMMISSION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");
            //rngCOMMISSION.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21");
            rfvCASH_CALL_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");
            rfvCONTACT_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "33"); //sneha
            cpvDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1987");//"Effective date of Contract Must be less that Expiry Date of Contract"; // Changed by Aditya for TFS bug # 923
           // rfvREINSURANCE_COMMISSION_ACCOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "34"); //sneha
           // rfvREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "35"); //sneha
            rfvMAX_NO_INSTALLMENT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "38"); //Added by Aditya for TFS BUG # 2512
		 }
		
		#endregion

		# region POPULATING ASSIGNED MULTI SELECT LISTITEM
		private void populateMultiSelect()
		{
			objReinsuranceInformation = new ClsReinsuranceInformation();
			DataSet oDs = objReinsuranceInformation.GetDataForPageControls(this.hidCONTRACT_ID.Value);
			
			if(oDs.Tables[1].Rows.Count >0)
			{
				hidRISK_EXPOSURE_OLD.Value = hidRISK_EXPOSURE.Value="";
				foreach( DataRow oDrRSK in oDs.Tables[1].Rows)
				{
					hidRISK_EXPOSURE_OLD.Value += oDrRSK[0].ToString() + ",";
					hidRISK_EXPOSURE.Value += oDrRSK[2].ToString() + ",";
					//this.cmbRISK_EXPOSURE.Items.FindByValue(oDrRSK["RISK_EXPOSURE"].ToString());
					//cmbRISK_EXPOSURE.SelectedIndex = cmbRISK_EXPOSURE.Items.IndexOf(li);
					this.cmbRSKAssignLossCodes.DataSource		= oDs.Tables[1]; 
					cmbRSKAssignLossCodes.DataTextField	= "LookupDesc";
					cmbRSKAssignLossCodes.DataValueField	= "LookupId";
					cmbRSKAssignLossCodes.DataBind();
					//cmbRSKAssignLossCodes.Items.Insert(0,"");
				}
			}
			if(oDs.Tables[2].Rows.Count > 0)
            {
                //string lang_id = GetLanguageID();
				hidCONTRACT_LOB_OLD.Value = hidCONTRACT_LOB.Value ="";
				foreach(DataRow oDrLOB in oDs.Tables[2].Rows)
				{
					hidCONTRACT_LOB_OLD.Value += oDrLOB[1].ToString() + ",";
					hidCONTRACT_LOB.Value += oDrLOB[2].ToString() + ",";
					//li = this.cmbCONTRACT_LOB.Items.FindByValue(oDrLOB["CONTRACT_LOB"].ToString());
					//cmbCONTRACT_LOB.SelectedIndex = cmbCONTRACT_LOB.Items.IndexOf(li);
					this.cmbLOBAssignLossCodes.DataSource		= oDs.Tables[2]; 
					cmbLOBAssignLossCodes.DataTextField	= "LOB_DESC";
					cmbLOBAssignLossCodes.DataValueField ="LOB_ID";
					cmbLOBAssignLossCodes.DataBind();
					//cmbLOBAssignLossCodes.Items.Insert(0,"");
				}
			}
			if(oDs.Tables[3].Rows.Count > 0)
			{
				hidSTATE_ID_OLD.Value = hidSTATE_ID.Value = "";
				foreach(DataRow oDrState in oDs.Tables[3].Rows )
				{
					hidSTATE_ID_OLD.Value += oDrState[0].ToString() + ",";
					hidSTATE_ID.Value += oDrState[2].ToString() + ",";
					//li = this.cmbSTATE_ID.Items.FindByValue(oDrState["STATE_ID"].ToString());
					//cmbSTATE_ID.SelectedIndex = cmbSTATE_ID.Items.IndexOf(li);
					this.cmbSTATEAssignLossCodes.DataSource		=  oDs.Tables[3];
					cmbSTATEAssignLossCodes.DataTextField	= "State_Name";
					cmbSTATEAssignLossCodes.DataValueField	= "State_Id";
					cmbSTATEAssignLossCodes.DataBind();
					//cmbSTATEAssignLossCodes.Items.Insert(0,"");
				}
			}
		}	
		# endregion

		
	}
}
