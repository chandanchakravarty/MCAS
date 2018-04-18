/******************************************************************************************
	<Author					: - > Vijay Arora
	<Start Date				: -	> 02-06-2006
	<End Date				: - >
	<Description			: - > Class for Claims Payee Details
	<Review Date			: - >
	<Reviewed By			: - >
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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.CmsWeb.Controls; 

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddRecoveryPayer : Cms.Claims.ClaimBase
	{
		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;
		public int AnyPayeeAdded=0;
		private bool LOAD_OLD_DATA = true;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
//		protected System.Web.UI.WebControls.Label capRECOVERY_TYPE;
//		protected System.Web.UI.WebControls.DropDownList cmbRECOVERY_TYPE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECOVERY_TYPE;
		protected System.Web.UI.WebControls.Label capRECEIVED_FROM;
		protected System.Web.UI.WebControls.TextBox txtRECEIVED_FROM;
		protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NUMBER;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;		
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDefaultValues;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPARTY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLED_FROM;
		protected System.Web.UI.WebControls.Label capRECEIVED_DATE;
		protected System.Web.UI.WebControls.TextBox txtRECEIVED_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkRECEIVED_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRECEIVED_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECEIVED_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator rfvCHEQUE;
		protected System.Web.UI.WebControls.CustomValidator csvRECEIVED_DATE;
		
		ClsRecoveryPayer objRecoveryPayer = new ClsRecoveryPayer();
		#endregion
	
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			
			base.ScreenId="309_1_0_3_0";//Done for Itrack Issue 6553 on 16 Oct 09
			
			lblMessage.Visible = false;
			
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddRecoveryPayer" ,System.Reflection.Assembly.GetExecutingAssembly());

			
			string strClaimStatus = GetClaimStatus();
			if(strClaimStatus == CLAIM_STATUS_CLOSED)
				btnSave.Visible=false;
			else
				btnSave.Visible=true;


			if(!Page.IsPostBack)
			{			
				GetQueryStringValues();
				if(hidPAYER_ID.Value=="") //Check for whether any payee has already been added 
				{
					ClsPayee objPayee = new ClsPayee();
					AnyPayeeAdded = objPayee.AnyPayeeForClaim(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
					if(AnyPayeeAdded>0)
					{
						lblDelete.Visible = true;
						lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("947");
						trBody.Attributes.Add("style","display:none");
						hidFormSaved.Value = "5";
						return;
					}
				}	
				hlkRECEIVED_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_ACTIVITY_RECOVERY_PAYER.txtRECEIVED_DATE,document.CLM_ACTIVITY_RECOVERY_PAYER.txtRECEIVED_DATE)"); //Javascript Implementation for Date								
				btnReset.Attributes.Add("onclick","javascript:return formReset();");								
//				FillDropDowns();
				SetCaptions();
				SetErrorMessages();	
				GetOldDataXML(LOAD_OLD_DATA);
			}
			
		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")			
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();						
			else	
				hidCLAIM_ID.Value = "";

			if(Request["ACTIVITY_ID"]!=null && Request["ACTIVITY_ID"].ToString()!="")			
				hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();												
			else	
				hidACTIVITY_ID.Value = "";


			if(Request["CALLED_FROM"]!=null && Request["CALLED_FROM"].ToString()!="")								
				hidCALLED_FROM.Value = Request["CALLED_FROM"].ToString();
			else	
				hidCALLED_FROM.Value = "";

			if(Request["PAYER_ID"]!=null && Request["PAYER_ID"].ToString()!="")
				hidPAYER_ID.Value = Request["PAYER_ID"].ToString();
			else
				hidPAYER_ID.Value = "";
		}
	
		#region GetOldDataXML
		private void GetOldDataXML(bool LOAD_DATA_FLAG)
		{
			if (hidCLAIM_ID.Value != "" && hidPAYER_ID.Value != "")
			{
				DataSet dsData	=	objRecoveryPayer.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,hidPAYER_ID.Value);
				if(dsData!=null && dsData.Tables.Count>0 && dsData.Tables[0].Rows.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsData.Tables[0]);
					if(LOAD_DATA_FLAG)
						LoadData(dsData.Tables[0]);
				}
				else
					hidOldData.Value	=	"";
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
//			rfvRECOVERY_TYPE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");						
			csvDESCRIPTION.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("831");
			rfvCHEQUE.ErrorMessage 							=	    Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");	
			rfvCHEQUE.ValidationExpression 					=	    aRegExpInteger;
			revRECEIVED_DATE.ValidationExpression			=		aRegExpDate;
			revRECEIVED_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			rfvRECEIVED_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("777");
			csvRECEIVED_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("629");
			
		}
		#endregion

//		#region FillDropDowns
//		private void FillDropDowns()
//		{
//			
//			cmbRECOVERY_TYPE.DataSource =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PYMTD");  //Action on Payment Lookup
//			cmbRECOVERY_TYPE.DataTextField="LookupDesc";
//			cmbRECOVERY_TYPE.DataValueField="LookupID";
//			cmbRECOVERY_TYPE.DataBind();
//			
//		}
//		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
//			capRECOVERY_TYPE.Text		=		objResourceMgr.GetString("txtRECOVERY_TYPE");			
			capDESCRIPTION.Text 		=		objResourceMgr.GetString("txtDESCRIPTION");						
			capCHECK_NUMBER.Text		=		objResourceMgr.GetString("txtCHECK_NUMBER");			
			capRECEIVED_DATE.Text 		=		objResourceMgr.GetString("txtRECEIVED_DATE");						
			capRECEIVED_FROM.Text 		=		objResourceMgr.GetString("txtRECEIVED_FROM");				
		}
		#endregion

		#region GetFormValue
		private ClsRecoveryPayerInfo GetFormValue()
		{
			ClsRecoveryPayerInfo objPInfo = new ClsRecoveryPayerInfo();
			
			if(hidCLAIM_ID.Value != "")
				objPInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);			
			objPInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);			
//			if(cmbRECOVERY_TYPE.SelectedItem!=null && cmbRECOVERY_TYPE.SelectedItem.Value!="")
//				objPInfo.RECOVERY_TYPE = int.Parse(cmbRECOVERY_TYPE.SelectedItem.Value);
			if(txtRECEIVED_DATE.Text.Trim()!="")
				objPInfo.RECEIVED_DATE = Convert.ToDateTime(txtRECEIVED_DATE.Text.Trim());
			objPInfo.RECEIVED_FROM = txtRECEIVED_FROM.Text.Trim();
			objPInfo.CHECK_NUMBER = txtCHECK_NUMBER.Text.Trim();
			objPInfo.DESCRIPTION = txtDESCRIPTION.Text.Trim();
			if(hidPAYER_ID.Value == "")
				strRowId="NEW";
			else
			{
				strRowId=hidPAYER_ID.Value; 
				objPInfo.PAYER_ID =	int.Parse(hidPAYER_ID.Value);	
			}
			return objPInfo;
		}
		#endregion

		

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	

				//Retreiving the form values into model class object
				ClsRecoveryPayerInfo objPInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objPInfo.CREATED_BY = int.Parse(GetUserId());
					objPInfo.CREATED_DATETIME = DateTime.Now;
					
					//Calling the add method of business layer class
					intRetVal = objRecoveryPayer.Add(objPInfo);

					if(intRetVal>0)
					{
						hidPAYER_ID.Value = objPInfo.PAYER_ID.ToString();
						lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value			= "Y";						
						GetOldDataXML(!LOAD_OLD_DATA);
					}
					else if(intRetVal == -1) 
					{
						lblMessage.Text				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("819");	
						hidFormSaved.Value			=		"2";
					}						
					else
					{
						lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsRecoveryPayerInfo objOldPayeeInfo = new ClsRecoveryPayerInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldPayeeInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objPInfo.MODIFIED_BY = int.Parse(GetUserId());
					objPInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					//Updating the record using business layer class object
					intRetVal	= objRecoveryPayer.Update(objOldPayeeInfo,objPInfo,hidCALLED_FROM.Value);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML(!LOAD_OLD_DATA);
					}
					else if(intRetVal == -1)	
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("819");	
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
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
		}

	
		#region LoadData
		private void LoadData(DataTable dtData)
		{
			if(dtData!=null && dtData.Rows.Count>0)
			{
//				if(dtData.Rows[0]["RECOVERY_TYPE"]!=null && dtData.Rows[0]["RECOVERY_TYPE"].ToString()!="")
//					cmbRECOVERY_TYPE.SelectedValue = dtData.Rows[0]["RECOVERY_TYPE"].ToString();
				txtRECEIVED_DATE.Text = dtData.Rows[0]["RECEIVED_DATE"].ToString().Trim();
				txtRECEIVED_FROM.Text = dtData.Rows[0]["RECEIVED_FROM"].ToString();
				txtCHECK_NUMBER.Text = dtData.Rows[0]["CHECK_NUMBER"].ToString();
				txtDESCRIPTION.Text = dtData.Rows[0]["DESCRIPTION"].ToString();
			}
		}
		#endregion		
	}
}
