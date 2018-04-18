/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> May 19,2006
	<End Date				: - >
	<Description			: - > Page is used to display watercraft company records
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History


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
using System.Resources; 
using System.Reflection; 
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;




namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddWatercraftCompany : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMERIDIEM;
		protected System.Web.UI.WebControls.Label capACCIDENT_HOUR;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capNAIC_CODE;
		protected System.Web.UI.WebControls.TextBox txtNAIC_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAIC_CODE;
		protected System.Web.UI.WebControls.Label capREFERENCE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtREFERENCE_NUMBER;
		protected System.Web.UI.WebControls.Label capCAT_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCAT_NUMBER;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capEXPIRATION_DATE;
		protected System.Web.UI.WebControls.TextBox txtEXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capCONTACT_NAME;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_NAME;
		protected System.Web.UI.WebControls.Label capCONTACT_ADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_ADDRESS1;
		protected System.Web.UI.WebControls.Label capCONTACT_ADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_ADDRESS2;
		protected System.Web.UI.WebControls.Label capCONTACT_CITY;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_CITY;
		protected System.Web.UI.WebControls.Label capCONTACT_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbCONTACT_COUNTRY;
		protected System.Web.UI.WebControls.Label capCONTACT_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbCONTACT_STATE;
		protected System.Web.UI.WebControls.Label capCONTACT_ZIP;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_ZIP;
		protected System.Web.UI.WebControls.Label capCONTACT_HOMEPHONE;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_HOMEPHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_HOMEPHONE;
		protected System.Web.UI.WebControls.Label capCONTACT_WORKPHONE;
		protected System.Web.UI.WebControls.TextBox txtCONTACT_WORKPHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_WORKPHONE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSURED_CONTACT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOMPANY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;

		
		protected System.Web.UI.WebControls.Label capINSURED_CONTACT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbINSURED_CONTACT_ID;		
		protected System.Web.UI.WebControls.Label capPREVIOUSLY_REPORTED;
		protected System.Web.UI.WebControls.DropDownList cmbPREVIOUSLY_REPORTED;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_TYPE;
		protected System.Web.UI.WebControls.Label capPOLICY_TYPE;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;
		protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
		private string strRowId;
		//protected System.Web.UI.WebControls.Image imgEFFECTIVE_DATE;
		//protected System.Web.UI.WebControls.Image imgEXPIRATION_DATE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revEXPIRATION_DATE;
		//protected System.Web.UI.WebControls.CustomValidator csvEXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capACCIDENT_DATE;
		protected System.Web.UI.WebControls.TextBox txtACCIDENT_DATE;
		//protected System.Web.UI.WebControls.HyperLink hlkACCIDENT_DATE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCIDENT_DATE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revACCIDENT_DATE;
		//protected System.Web.UI.WebControls.CustomValidator csvACCIDENT_DATE;
		protected System.Web.UI.WebControls.Label capACCIDENT_TIME;
		//protected System.Web.UI.WebControls.TextBox txtACCIDENT_HOUR;
		protected System.Web.UI.WebControls.Label lblACCIDENT_HOUR;
		//protected System.Web.UI.WebControls.TextBox txtACCIDENT_MINUTE;
		protected System.Web.UI.WebControls.Label capACCIDENT_MINUTE;
		protected System.Web.UI.WebControls.Label lblACCIDENT_MINUTE;
		protected System.Web.UI.WebControls.DropDownList cmbMERIDIEM;
		protected System.Web.UI.HtmlControls.HtmlTableRow trINSURED_CONTACT;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCIDENT_HOUR;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCIDENT_MINUTE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMERIDIEM;
		//protected System.Web.UI.WebControls.RangeValidator rngACCIDENT_HOUR;
		//protected System.Web.UI.WebControls.RangeValidator rngACCIDENT_MINUTE;
		private bool LOAD_OLD_DATA = true;
		
		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="306_9_0";
			
			lblMessage.Visible = false;
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddWatercraftCompany" ,System.Reflection.Assembly.GetExecutingAssembly());
			

			if(!Page.IsPostBack)
			{			
				this.cmbCONTACT_COUNTRY.SelectedIndex = int.Parse(aCountry);
				GetQueryStringValues();
				LoadDropDowns();				
				GetOldDataXML(LOAD_OLD_DATA);								
				//cmbINSURED_CONTACT_ID.Attributes.Add("onChange","javascript: return LoadInsuredContact();");
				//hlkACCIDENT_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_CLAIM_COMPANY.txtACCIDENT_DATE,document.CLM_CLAIM_COMPANY.txtACCIDENT_DATE)"); //Javascript Implementation for Date				
				//imgEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_CLAIM_COMPANY.txtEFFECTIVE_DATE,document.CLM_CLAIM_COMPANY.txtEFFECTIVE_DATE)"); //Javascript Implementation 
				//imgEXPIRATION_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_CLAIM_COMPANY.txtEXPIRATION_DATE,document.CLM_CLAIM_COMPANY.txtEXPIRATION_DATE)"); //Javascript Implementation for Date				
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();								
			}
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML(bool LOAD_DATA)
		{
			DataTable dtOldData;
			if(hidCOMPANY_ID.Value!="" && hidCOMPANY_ID.Value!="0")
			{
				dtOldData =  ClsWatercraftCompany.GetOldDataCLM_CLAIM_COMPANY(hidCLAIM_ID.Value,hidAGENCY_ID.Value,hidCOMPANY_ID.Value);
				hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
				trINSURED_CONTACT.Attributes.Add("style","display:none");
				if(LOAD_DATA)
					LoadData(dtOldData);
			}
			else
				hidOldData.Value	=	"";
		}
		#endregion

		#region LoadData
		private void LoadData(DataTable dtLoadData)
		{
			if(dtLoadData!=null && dtLoadData.Rows.Count>0)
			{
				txtNAIC_CODE.Text = dtLoadData.Rows[0]["NAIC_CODE"].ToString();
				if(dtLoadData.Rows[0]["CONTACT_STATE"]!=null && dtLoadData.Rows[0]["CONTACT_STATE"].ToString()!="" && dtLoadData.Rows[0]["CONTACT_STATE"].ToString()!="0")
					cmbCONTACT_STATE.SelectedValue = dtLoadData.Rows[0]["CONTACT_STATE"].ToString();
				txtREFERENCE_NUMBER.Text = dtLoadData.Rows[0]["REFERENCE_NUMBER"].ToString();
				txtCAT_NUMBER.Text  = dtLoadData.Rows[0]["CAT_NUMBER"].ToString();
				txtEFFECTIVE_DATE.Text = dtLoadData.Rows[0]["EFFECTIVE_DATE"].ToString().Trim();
				txtEXPIRATION_DATE.Text  = dtLoadData.Rows[0]["EXPIRATION_DATE"].ToString().Trim()	;
				txtCONTACT_NAME.Text = dtLoadData.Rows[0]["CONTACT_NAME"].ToString();
				txtCONTACT_ADDRESS1.Text  = dtLoadData.Rows[0]["CONTACT_ADDRESS1"].ToString();
				txtCONTACT_ADDRESS2.Text = dtLoadData.Rows[0]["CONTACT_ADDRESS2"].ToString();
				if(dtLoadData.Rows[0]["CONTACT_COUNTRY"]!=null && dtLoadData.Rows[0]["CONTACT_COUNTRY"].ToString()!="" && dtLoadData.Rows[0]["CONTACT_COUNTRY"].ToString()!="0")
					cmbCONTACT_COUNTRY.SelectedValue = dtLoadData.Rows[0]["CONTACT_COUNTRY"].ToString();				
				txtCONTACT_CITY.Text  = dtLoadData.Rows[0]["CONTACT_CITY"].ToString();
				txtCONTACT_ZIP.Text = dtLoadData.Rows[0]["CONTACT_ZIP"].ToString();				
				txtCONTACT_HOMEPHONE.Text = dtLoadData.Rows[0]["CONTACT_HOMEPHONE"].ToString();
				txtCONTACT_WORKPHONE.Text = dtLoadData.Rows[0]["CONTACT_WORKPHONE"].ToString();				
				if(dtLoadData.Rows[0]["INSURED_CONTACT_ID"]!=null && dtLoadData.Rows[0]["INSURED_CONTACT_ID"].ToString()!="")
					cmbINSURED_CONTACT_ID.SelectedValue = dtLoadData.Rows[0]["INSURED_CONTACT_ID"].ToString();
				if(dtLoadData.Rows[0]["PREVIOUSLY_REPORTED"]!=null && dtLoadData.Rows[0]["PREVIOUSLY_REPORTED"].ToString()!="")
					cmbPREVIOUSLY_REPORTED.SelectedValue = dtLoadData.Rows[0]["PREVIOUSLY_REPORTED"].ToString();

				if (dtLoadData.Rows[0]["ACCIDENT_DATE"] != DBNull.Value)
				{
					txtACCIDENT_DATE.Text = dtLoadData.Rows[0]["ACCIDENT_DATE"].ToString().Trim();
				}
				if (dtLoadData.Rows[0]["ACCIDENT_TIME"] != DBNull.Value)
				{
					int hour = 0;
					hour = Convert.ToInt32(Convert.ToDateTime(dtLoadData.Rows[0]["ACCIDENT_TIME"]).Hour.ToString().Trim());
					
					if (hour > 12)
					{
						hour = hour - 12;
					}
					
					//txtACCIDENT_HOUR.Text =   hour.ToString().Trim();
					lblACCIDENT_HOUR.Text =   hour.ToString().Trim();

					//txtACCIDENT_MINUTE.Text = Convert.ToDateTime(dtLoadData.Rows[0]["ACCIDENT_TIME"]).Minute.ToString().Trim();
					lblACCIDENT_MINUTE.Text = Convert.ToDateTime(dtLoadData.Rows[0]["ACCIDENT_TIME"]).Minute.ToString().Trim();
					if(dtLoadData.Rows[0]["LOSS_TIME_AM_PM"]!=null && dtLoadData.Rows[0]["LOSS_TIME_AM_PM"].ToString()!="")
					{
						cmbMERIDIEM.SelectedValue = dtLoadData.Rows[0]["LOSS_TIME_AM_PM"].ToString();
						lblMERIDIEM.Text = cmbMERIDIEM.SelectedItem.Text;
					}	
				}
				DisableContactAddress();
			}
			
		}
		#endregion



		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{	
			this.cmbINSURED_CONTACT_ID.SelectedIndexChanged += new System.EventHandler(this.cmbINSURED_CONTACT_ID_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			revCONTACT_ZIP.ValidationExpression				=		  aRegExpZip;
			revCONTACT_ZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			revCONTACT_WORKPHONE.ValidationExpression		=		  aRegExpPhone;
			revCONTACT_WORKPHONE.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			revCONTACT_HOMEPHONE.ValidationExpression		=		  aRegExpPhone;
			revCONTACT_HOMEPHONE.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");									
			rfvNAIC_CODE.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("757");
			//revEXPIRATION_DATE.ValidationExpression			=		 aRegExpDate;
			//revEXPIRATION_DATE.ErrorMessage					=	    Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			//revEFFECTIVE_DATE.ValidationExpression			=		aRegExpDate;
			//revEFFECTIVE_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			//csvEXPIRATION_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("758");
			//revACCIDENT_DATE.ValidationExpression			=		aRegExpDate;
			//revACCIDENT_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			//rfvACCIDENT_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			//rfvACCIDENT_HOUR.ErrorMessage						=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2") + "<br>";
			//rfvACCIDENT_MINUTE.ErrorMessage						=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2") + "<br>";
			//csvACCIDENT_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			//rngACCIDENT_HOUR.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("771") + "<br>";
			//rngACCIDENT_MINUTE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("772") + "<br>";			
		}

		#endregion

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsWatercraftCompany objWatercraftCompany = new ClsWatercraftCompany();				

				//Retreiving the form values into model class object
				ClsWatercraftCompanyInfo objWatercraftCompanyInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objWatercraftCompanyInfo.CREATED_BY = int.Parse(GetUserId());
					objWatercraftCompanyInfo.CREATED_DATETIME = DateTime.Now;
					objWatercraftCompanyInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objWatercraftCompany.Add(objWatercraftCompanyInfo);

					if(intRetVal>0)
					{
						hidCOMPANY_ID.Value = objWatercraftCompanyInfo.COMPANY_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(!LOAD_OLD_DATA);
					}
					else if(intRetVal == -1) //Duplicate Authority Limit
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsWatercraftCompanyInfo objOldWatercraftCompanyInfo = new ClsWatercraftCompanyInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldWatercraftCompanyInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objWatercraftCompanyInfo.MODIFIED_BY = int.Parse(GetUserId());
					objWatercraftCompanyInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objWatercraftCompany.Update(objOldWatercraftCompanyInfo,objWatercraftCompanyInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML(!LOAD_OLD_DATA);
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
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
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}
		private void SetCaptions()
		{
			capCONTACT_NAME.Text				=		objResourceMgr.GetString("txtCONTACT_NAME");
			capCONTACT_ADDRESS1.Text			=		objResourceMgr.GetString("txtCONTACT_ADDRESS1");
			capCONTACT_ADDRESS2.Text			=		objResourceMgr.GetString("txtCONTACT_ADDRESS2");
			capCONTACT_CITY.Text				=		objResourceMgr.GetString("txtCONTACT_CITY");
			capCONTACT_STATE.Text				=		objResourceMgr.GetString("cmbCONTACT_STATE");
			capCONTACT_ZIP.Text					=		objResourceMgr.GetString("txtCONTACT_ZIP");
			capNAIC_CODE.Text					=		objResourceMgr.GetString("txtNAIC_CODE");
			capREFERENCE_NUMBER.Text			=		objResourceMgr.GetString("txtREFERENCE_NUMBER");
			capCONTACT_HOMEPHONE.Text			=		objResourceMgr.GetString("txtCONTACT_HOMEPHONE");
			capCONTACT_WORKPHONE.Text			=		objResourceMgr.GetString("txtCONTACT_WORKPHONE");						
			capCONTACT_COUNTRY.Text				=		objResourceMgr.GetString("cmbCONTACT_COUNTRY");
			capCAT_NUMBER.Text					=		objResourceMgr.GetString("txtCAT_NUMBER");
			capEFFECTIVE_DATE.Text				=		objResourceMgr.GetString("txtEFFECTIVE_DATE");
			capEXPIRATION_DATE.Text				=		objResourceMgr.GetString("txtEXPIRATION_DATE");
			capPREVIOUSLY_REPORTED.Text			=		objResourceMgr.GetString("txtPREVIOUSLY_REPORTED");
			capPOLICY_NUMBER.Text				=		objResourceMgr.GetString("txtPOLICY_NUMBER");
			capPOLICY_TYPE.Text					=		objResourceMgr.GetString("txtPOLICY_TYPE");
			capINSURED_CONTACT_ID.Text			=		objResourceMgr.GetString("cmbINSURED_CONTACT_ID");
			capACCIDENT_DATE.Text					=		objResourceMgr.GetString("txtACCIDENT_DATE");			
			capACCIDENT_TIME.Text					=		objResourceMgr.GetString("txtACCIDENT_TIME");			
			
			
		}
	

		#region GetFormValue
		private ClsWatercraftCompanyInfo GetFormValue()
		{
			ClsWatercraftCompanyInfo objWatercraftCompanyInfo = new ClsWatercraftCompanyInfo();
			objWatercraftCompanyInfo.NAIC_CODE = txtNAIC_CODE.Text.Trim();
			objWatercraftCompanyInfo.REFERENCE_NUMBER = txtREFERENCE_NUMBER.Text.Trim();
			objWatercraftCompanyInfo.CAT_NUMBER = txtCAT_NUMBER.Text.Trim();
			if(txtEFFECTIVE_DATE.Text.Trim()!="" && IsDate(txtEFFECTIVE_DATE.Text.Trim()))
				objWatercraftCompanyInfo.EFFECTIVE_DATE = Convert.ToDateTime(txtEFFECTIVE_DATE.Text.Trim());
			if(txtEXPIRATION_DATE.Text.Trim()!="" && IsDate(txtEXPIRATION_DATE.Text.Trim()))
				objWatercraftCompanyInfo.EXPIRATION_DATE = Convert.ToDateTime(txtEXPIRATION_DATE.Text.Trim());
			objWatercraftCompanyInfo.CONTACT_NAME = txtCONTACT_NAME.Text.Trim();
			objWatercraftCompanyInfo.CONTACT_ADDRESS1 = txtCONTACT_ADDRESS1.Text.Trim();
			objWatercraftCompanyInfo.CONTACT_ADDRESS2 = txtCONTACT_ADDRESS2.Text.Trim();
			objWatercraftCompanyInfo.CONTACT_CITY	  = txtCONTACT_CITY.Text.Trim();
			if(cmbCONTACT_COUNTRY.SelectedItem!=null && cmbCONTACT_COUNTRY.SelectedItem.Value!="")
				objWatercraftCompanyInfo.CONTACT_COUNTRY = int.Parse(cmbCONTACT_COUNTRY.SelectedItem.Value);
			if(cmbCONTACT_STATE.SelectedItem!=null && cmbCONTACT_STATE.SelectedItem.Value!="")
				objWatercraftCompanyInfo.CONTACT_STATE = int.Parse(cmbCONTACT_STATE.SelectedItem.Value);
			objWatercraftCompanyInfo.CONTACT_ZIP = txtCONTACT_ZIP.Text.Trim();
			objWatercraftCompanyInfo.CONTACT_HOMEPHONE = txtCONTACT_HOMEPHONE.Text.Trim();
			objWatercraftCompanyInfo.CONTACT_WORKPHONE = txtCONTACT_WORKPHONE.Text.Trim();

			objWatercraftCompanyInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
			if(cmbPREVIOUSLY_REPORTED.SelectedItem!=null && cmbPREVIOUSLY_REPORTED.SelectedItem.Value!="")
				objWatercraftCompanyInfo.PREVIOUSLY_REPORTED = cmbPREVIOUSLY_REPORTED.SelectedItem.Value;
			if(cmbINSURED_CONTACT_ID.SelectedItem!=null && cmbINSURED_CONTACT_ID.SelectedItem.Value!="-1")
			{
				objWatercraftCompanyInfo.INSURED_CONTACT_ID = int.Parse(cmbINSURED_CONTACT_ID.SelectedItem.Value);
			}
			//objWatercraftCompanyInfo.AGENCY_ID = int.Parse(hidAGENCY_ID.Value);
			//Temporarily set to 1 as work on agency page is in progress
			objWatercraftCompanyInfo.AGENCY_ID = 1;

			if (txtACCIDENT_DATE.Text != "")
			{
				DateTime LossDate = ConvertToDate(txtACCIDENT_DATE.Text);

				//int Hr = int.Parse(txtACCIDENT_HOUR.Text);
				int Hr = int.Parse(lblACCIDENT_HOUR.Text);
				
				if(cmbMERIDIEM.SelectedIndex == 1)
				{
					Hr+=12;
				}
				if(Hr==24)
				{
					Hr=00;
				}

				objWatercraftCompanyInfo.ACCIDENT_DATE_TIME = new DateTime(LossDate.Year, LossDate.Month, LossDate.Day
					, Hr, int.Parse(lblACCIDENT_MINUTE.Text)
					, 0);
				if(cmbMERIDIEM.SelectedItem!=null && cmbMERIDIEM.SelectedItem.Value!="")
					objWatercraftCompanyInfo.LOSS_TIME_AM_PM = int.Parse(cmbMERIDIEM.SelectedItem.Value);

			}

			if(hidCOMPANY_ID.Value=="" || hidCOMPANY_ID.Value=="0" || hidCOMPANY_ID.Value.ToUpper()=="NEW")
			{
				strRowId = "NEW";
			}
			else
			{
				objWatercraftCompanyInfo.COMPANY_ID = int.Parse(hidCOMPANY_ID.Value);
				strRowId = hidCOMPANY_ID.Value;
			}
			
			return objWatercraftCompanyInfo;
		}
		#endregion

		#region Get Query String Values
		private void GetQueryStringValues()
		{
			if(Request.QueryString["COMPANY_ID"]!=null && Request.QueryString["COMPANY_ID"].ToString()!="")
				hidCOMPANY_ID.Value = Request.QueryString["COMPANY_ID"].ToString();
			else
				hidCOMPANY_ID.Value = "0";

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			else
				hidCLAIM_ID.Value = "0";

			if(Request.QueryString["AGENCY_ID"]!=null && Request.QueryString["AGENCY_ID"].ToString()!="")
				hidAGENCY_ID.Value = Request.QueryString["AGENCY_ID"].ToString();
			else
				hidAGENCY_ID.Value = "0";
			
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			
			cmbCONTACT_STATE.DataSource		= Cms.CmsWeb.ClsFetcher.State;;
			cmbCONTACT_STATE.DataTextField	= "State_Name";
			cmbCONTACT_STATE.DataValueField	= "State_Id";
			cmbCONTACT_STATE.DataBind();
			cmbCONTACT_STATE.Items.Insert(0,"");

			
			cmbCONTACT_COUNTRY.DataSource		= Cms.CmsWeb.ClsFetcher.Country;
			cmbCONTACT_COUNTRY.DataTextField	= "Country_Name";
			cmbCONTACT_COUNTRY.DataValueField	= "Country_Id";
			cmbCONTACT_COUNTRY.DataBind();

			cmbMERIDIEM.Items.Insert(0,"AM");
			cmbMERIDIEM.Items[0].Value = "0";
			cmbMERIDIEM.Items.Insert(1,"PM");
			cmbMERIDIEM.Items[1].Value = "1";

			//Blank values being passed for customer_id,policy_id and policy_version_id..
			//We will get the result using only claim_id also
			ClsClaims objClaims = new ClsClaims();
			DataSet dsPolicyData = objClaims.GetPolicyClaimDataSet("","","",hidCLAIM_ID.Value,int.Parse(GetLanguageID()));
			if(dsPolicyData!=null && dsPolicyData.Tables.Count>0 && dsPolicyData.Tables[0].Rows.Count>0)
			{
				txtPOLICY_NUMBER.Text = dsPolicyData.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
				txtPOLICY_TYPE.Text	  = dsPolicyData.Tables[0].Rows[0]["POLICY_STATUS"].ToString();
				txtEFFECTIVE_DATE.Text = dsPolicyData.Tables[0].Rows[0]["POLICY_EFFECTIVE_DATE"].ToString();
				txtEXPIRATION_DATE.Text = dsPolicyData.Tables[0].Rows[0]["POLICY_EXPIRATION_DATE"].ToString();
			}
			//Fetch data for date/time of accident from claim info table
			dsPolicyData = ClsClaimsNotification.GetClaimsNotification(int.Parse(hidCLAIM_ID.Value));
			if(dsPolicyData!=null && dsPolicyData.Tables.Count>0 && dsPolicyData.Tables[0].Rows.Count>0)
			{
				if (dsPolicyData.Tables[0].Rows[0]["LOSS_TIME"] != DBNull.Value)
				{
					int hour = 0;
					hour = Convert.ToInt32(Convert.ToDateTime(dsPolicyData.Tables[0].Rows[0]["LOSS_TIME"]).Hour.ToString().Trim());
					
					if (hour > 12)
					{
						hour = hour - 12;
					}
					
					//txtACCIDENT_HOUR.Text =   hour.ToString().Trim();
					lblACCIDENT_HOUR.Text =   hour.ToString().Trim();
					//txtACCIDENT_MINUTE.Text = Convert.ToDateTime(dsPolicyData.Tables[0].Rows[0]["LOSS_TIME"]).Minute.ToString().Trim();
					lblACCIDENT_MINUTE.Text = Convert.ToDateTime(dsPolicyData.Tables[0].Rows[0]["LOSS_TIME"]).Minute.ToString().Trim();
					if(dsPolicyData.Tables[0].Rows[0]["LOSS_TIME_AM_PM"]!=null && dsPolicyData.Tables[0].Rows[0]["LOSS_TIME_AM_PM"].ToString()!="")
					{
						cmbMERIDIEM.SelectedValue = dsPolicyData.Tables[0].Rows[0]["LOSS_TIME_AM_PM"].ToString();
						lblMERIDIEM.Text = cmbMERIDIEM.SelectedItem.Text;
					}
				}

				if (dsPolicyData.Tables[0].Rows[0]["LOSS_DATE"] != DBNull.Value)
				{
					txtACCIDENT_DATE.Text = dsPolicyData.Tables[0].Rows[0]["LOSS_DATE"].ToString().Trim();
				}
			}			
			/*We will be using the drop-down as well as label for meridiem value.
			Dropdown will be hidden while the label will be displayed to the user. Label will contain the
			SelectedText property of the dropdown. Dropdown is not discarded altoghether because we need to 
			maintain two values for meridiem, one for display purposes (AM/PM) and another (0/1) for 
			storing the value in the database. Label will display the text of meridiem while dropdown will
			be used to store the actual value.
			*/
			cmbMERIDIEM.Attributes.Add("style","display:none");
			//Blank values being passed for customer_id,policy_id,policy_version_id and vehicle_owner..
			//We will get the result using only claim_id also
			DataTable dtNAMED_INSURED = ClsOwnerDetails.GetNamedInsured(0,0,0,0,int.Parse(hidCLAIM_ID.Value),0);//Added for Itrack Issue 6053 on 31 July 2009
			if(dtNAMED_INSURED!=null && dtNAMED_INSURED.Rows.Count>0)
			{
				/*cmbINSURED_CONTACT_ID.Items.Add(new ListItem("","-1"));

				foreach(DataRow dtRow in dtNAMED_INSURED.Rows)
				{
					string sVal = "";
					for(int i=0;i<dtNAMED_INSURED.Columns.Count;i++)
						sVal+= dtRow[i].ToString() + "^";

					sVal=sVal.Substring(0,sVal.Length-1).Trim();

					ListItem lItem = new ListItem(dtRow["NAMED_INSURED"].ToString(),sVal);
					cmbINSURED_CONTACT_ID.Items.Add(lItem);
				}*/
				cmbINSURED_CONTACT_ID.DataSource = dtNAMED_INSURED;
				cmbINSURED_CONTACT_ID.DataTextField = "NAMED_INSURED";
				cmbINSURED_CONTACT_ID.DataValueField = "NAMED_INSURED_ID";
				cmbINSURED_CONTACT_ID.DataBind();
				cmbINSURED_CONTACT_ID.Items.Insert(0,"");
			}
		}
		#endregion

		private void DisableContactAddress()
		{
			txtCONTACT_ADDRESS1.Enabled = false;
			txtCONTACT_ADDRESS2.Enabled = false;			
		}

		private void cmbINSURED_CONTACT_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtNAMED_INSURED = new DataTable();
			try
			{
				if(cmbINSURED_CONTACT_ID.SelectedItem!=null && cmbINSURED_CONTACT_ID.SelectedItem.Value!="")
				{
					dtNAMED_INSURED = ClsOwnerDetails.GetNamedInsured(0,0,0,0,int.Parse(hidCLAIM_ID.Value),"",cmbINSURED_CONTACT_ID.SelectedItem.Value,0);//Added for Itrack Issue 6053 on 31 July 2009
					if(dtNAMED_INSURED!=null && dtNAMED_INSURED.Rows.Count>0)
					{
						txtCONTACT_NAME.Text = dtNAMED_INSURED.Rows[0]["NAMED_INSURED"].ToString();
						txtCONTACT_ADDRESS1.Text = dtNAMED_INSURED.Rows[0]["ADDRESS1"].ToString();
						txtCONTACT_ADDRESS2.Text = dtNAMED_INSURED.Rows[0]["ADDRESS2"].ToString();
						txtCONTACT_CITY.Text = dtNAMED_INSURED.Rows[0]["CITY"].ToString();
						txtCONTACT_ZIP.Text = dtNAMED_INSURED.Rows[0]["ZIP_CODE"].ToString();
						txtCONTACT_HOMEPHONE.Text = dtNAMED_INSURED.Rows[0]["PHONE"].ToString();
						txtCONTACT_WORKPHONE.Text = dtNAMED_INSURED.Rows[0]["WORK_PHONE"].ToString();
						if(dtNAMED_INSURED.Rows[0]["STATE"]!=null && dtNAMED_INSURED.Rows[0]["STATE"].ToString()!="")
							cmbCONTACT_STATE.SelectedValue = dtNAMED_INSURED.Rows[0]["STATE"].ToString();
						if(dtNAMED_INSURED.Rows[0]["COUNTRY"]!=null && dtNAMED_INSURED.Rows[0]["COUNTRY"].ToString()!="")
							cmbCONTACT_COUNTRY.SelectedValue = dtNAMED_INSURED.Rows[0]["COUNTRY"].ToString();
						DisableContactAddress();
					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(dtNAMED_INSURED!=null)
					dtNAMED_INSURED.Dispose();
			}
		
		}
		
	}
}
