/******************************************************************************************
<Author					: -   Sumit Chhabra
<Start Date				: -	  05 June , 2006
<End Date				: -	
<Description			: -   Payment Breakdown
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Claims;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// Summary description for Coverages.
	/// </summary>
	public class PaymentBreakDownDetails : Cms.Claims.ClaimBase
	{
		
		private string strRowId;
		private bool LOAD_OLD_DATA = true;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblMessage;
		//protected System.Web.UI.WebControls.Label capACTIVITY_ID;
		//protected System.Web.UI.WebControls.TextBox txtACTIVITY_ID;
		//protected System.Web.UI.WebControls.Label capACTIVITY_DATE;
		//protected System.Web.UI.WebControls.TextBox txtACTIVITY_DATE;
		//protected System.Web.UI.WebControls.Label capPAYMENT_AMOUNT;
		//protected System.Web.UI.WebControls.TextBox txtPAYMENT_AMOUNT;
		protected System.Web.UI.WebControls.Label capTRANSACTION_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbTRANSACTION_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSACTION_CODE;
		//protected System.Web.UI.WebControls.Label capCOVERAGE_ID;
		//protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_ID;
		protected System.Web.UI.WebControls.Label capPAID_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtPAID_AMOUNT;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYMENT_BREAKDOWN_ID;
		//protected System.Web.UI.WebControls.Label capACTIVITY_id;
		//protected System.Web.UI.WebControls.TextBox txtACTIVITY_id;
		
		
		
		System.Resources.ResourceManager objResourceMgr;		

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="308_0";	

			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Write;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.PaymentBreakDownDetails"  ,System.Reflection.Assembly.GetExecutingAssembly());
			// Put user code to initialize the page here
			if ( !Page.IsPostBack)
			{
				GetQueryStringValues();
				SetCaptions();
				SetErrorMessages();
				LoadDropDowns();
				//GetActivityData();
				GetOldDataXML(LOAD_OLD_DATA);
				btnBack.Attributes.Add("onClick","javascript: return GoBack();");				
				txtPAID_AMOUNT.Attributes.Add("onBlur","javascript: this.value=formatCurrency(this.value);");				
			}			
		}

//		private void GetActivityData()
//		{
//			DataTable dtActivity = ClsPaymentBreakDown.GetActivityData(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
//			if(dtActivity!=null && dtActivity.Rows.Count>0)
//			{
//				if(dtActivity.Rows[0]["ACTIVITY_DATE"]!=null && dtActivity.Rows[0]["ACTIVITY_DATE"].ToString()!="")
//					txtACTIVITY_DATE.Text = dtActivity.Rows[0]["ACTIVITY_DATE"].ToString();
//				if(dtActivity.Rows[0]["PAYMENT_AMOUNT"]!=null && dtActivity.Rows[0]["PAYMENT_AMOUNT"].ToString()!="")
//					txtPAYMENT_AMOUNT.Text = txtPAYMENT_AMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtActivity.Rows[0]["PAYMENT_AMOUNT"]));
//			}
//			txtACTIVITY_ID.Text = hidACTIVITY_ID.Value;
//		}

		private void SetErrorMessages()
		{
			rfvTRANSACTION_CODE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");						
		}
		

		private ClsPaymentBreakDownInfo GetFormValue()
		{
			ClsPaymentBreakDownInfo objPaymentBreakDownInfo = new ClsPaymentBreakDownInfo();
			if(cmbTRANSACTION_CODE.SelectedItem!=null && cmbTRANSACTION_CODE.SelectedItem.Value!="")
				objPaymentBreakDownInfo.TRANSACTION_CODE = int.Parse(cmbTRANSACTION_CODE.SelectedItem.Value);
			//if(cmbCOVERAGE_ID.SelectedItem!=null && cmbCOVERAGE_ID.SelectedItem.Value!="")
			//	objPaymentBreakDownInfo.COVERAGE_ID = int.Parse(cmbCOVERAGE_ID.SelectedItem.Value);
			
			if(txtPAID_AMOUNT.Text.Trim()!="")
				objPaymentBreakDownInfo.PAID_AMOUNT = Convert.ToDouble(txtPAID_AMOUNT.Text.Trim());

			objPaymentBreakDownInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
			objPaymentBreakDownInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
			if(hidPAYMENT_BREAKDOWN_ID.Value!="" && hidPAYMENT_BREAKDOWN_ID.Value!="0")
			{
				objPaymentBreakDownInfo.PAYMENT_BREAKDOWN_ID = int.Parse(hidPAYMENT_BREAKDOWN_ID.Value);
				strRowId = hidPAYMENT_BREAKDOWN_ID.Value;
			}
			else
				strRowId = "NEW";

			return objPaymentBreakDownInfo;
			
		}

		

		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			//capACTIVITY_ID.Text					=		objResourceMgr.GetString("txtACTIVITY_ID");	
			//capACTIVITY_DATE.Text				=		objResourceMgr.GetString("txtACTIVITY_DATE");	
			//capPAYMENT_AMOUNT.Text				=		objResourceMgr.GetString("txtPAYMENT_AMOUNT");				
			capTRANSACTION_CODE.Text			=		objResourceMgr.GetString("cmbTRANSACTION_CODE");	
			//capCOVERAGE_ID.Text					=		objResourceMgr.GetString("cmbCOVERAGE_ID");	
			capPAID_AMOUNT.Text					=		objResourceMgr.GetString("capPAID_AMOUNT");				
		}
		#endregion

		private void LoadDropDowns()
		{
			DataTable dtTransactionCodes = ClsDefaultValues.GetDefaultValuesDetails((int)enumClaimDefaultValues.CLAIM_TRANSACTION_CODE,(int)enumTransactionLookup.CLAIM_PAYMENT);  
			if(dtTransactionCodes!=null && dtTransactionCodes.Rows.Count>0)
			{
				cmbTRANSACTION_CODE.DataSource	=	dtTransactionCodes;
				cmbTRANSACTION_CODE.DataTextField=	"DETAIL_TYPE_DESCRIPTION";
				cmbTRANSACTION_CODE.DataValueField=	"DETAIL_TYPE_ID";
				cmbTRANSACTION_CODE.DataBind();
			}
			/*DataSet dsCoverages = ClsClaims.GetClaimCoverages(hidCLAIM_ID.Value);
			if(dsCoverages!=null && dsCoverages.Tables.Count>0 && dsCoverages.Tables[0].Rows.Count>0)
			{
				cmbCOVERAGE_ID.DataSource	=	dsCoverages;
				cmbCOVERAGE_ID.DataTextField=	"DESCRIPTION";
				cmbCOVERAGE_ID.DataValueField=	"ID";
				cmbCOVERAGE_ID.DataBind();
			}*/
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			if(Request.QueryString["ACTIVITY_ID"]!=null && Request.QueryString["ACTIVITY_ID"].ToString()!="")
				hidACTIVITY_ID.Value = Request.QueryString["ACTIVITY_ID"].ToString();
			if(Request.QueryString["PAYMENT_BREAKDOWN_ID"]!=null && Request.QueryString["PAYMENT_BREAKDOWN_ID"].ToString()!="")
				hidPAYMENT_BREAKDOWN_ID.Value = Request.QueryString["PAYMENT_BREAKDOWN_ID"].ToString();
			else
				hidPAYMENT_BREAKDOWN_ID.Value = "0";

			
		}

		private void GetOldDataXML(bool LOAD_DATA_FLAG)
		{
			DataTable dtOldData = new DataTable();
			if(hidCLAIM_ID.Value!="" && hidACTIVITY_ID.Value!="" && hidPAYMENT_BREAKDOWN_ID.Value!="" && hidCLAIM_ID.Value!="0" && hidACTIVITY_ID.Value!="0" && hidPAYMENT_BREAKDOWN_ID.Value!="0")
			{
				dtOldData = ClsPaymentBreakDown.GetXmlForPageControls(hidCLAIM_ID.Value, hidACTIVITY_ID.Value,hidPAYMENT_BREAKDOWN_ID.Value);
				if(dtOldData!=null && dtOldData.Rows.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
					if(LOAD_DATA_FLAG)
						LoadData(dtOldData);
				}
				else
					hidOldData.Value = "";
			}
			else
				hidOldData.Value = "";
		}

		private void LoadData(DataTable dtLoadData)
		{
			if(dtLoadData!=null && dtLoadData.Rows.Count>0)
			{
				if(dtLoadData.Rows[0]["TRANSACTION_CODE"]!=null && dtLoadData.Rows[0]["TRANSACTION_CODE"].ToString()!="")
					cmbTRANSACTION_CODE.SelectedValue = dtLoadData.Rows[0]["TRANSACTION_CODE"].ToString();
				//if(dtLoadData.Rows[0]["COVERAGE_ID"]!=null && dtLoadData.Rows[0]["COVERAGE_ID"].ToString()!="")
				//	cmbCOVERAGE_ID.SelectedValue = dtLoadData.Rows[0]["COVERAGE_ID"].ToString();
				txtPAID_AMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtLoadData.Rows[0]["PAID_AMOUNT"]));				
			}
		}

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsPaymentBreakDown objPaymentBreakDown = new ClsPaymentBreakDown();				

				//Retreiving the form values into model class object
				ClsPaymentBreakDownInfo objPaymentBreakDownInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objPaymentBreakDownInfo.CREATED_BY = int.Parse(GetUserId());
					objPaymentBreakDownInfo.CREATED_DATETIME = DateTime.Now;
					objPaymentBreakDownInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objPaymentBreakDown.Add(objPaymentBreakDownInfo);

					if(intRetVal>0)
					{
						hidPAYMENT_BREAKDOWN_ID.Value = objPaymentBreakDownInfo.PAYMENT_BREAKDOWN_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");						
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(!LOAD_OLD_DATA);
					}	
					else if(intRetVal==-1)
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("816");
					}	
					else if(intRetVal==-2)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"5");
					}	
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");						
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsPaymentBreakDownInfo objOldPaymentBreakDownInfo = new ClsPaymentBreakDownInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldPaymentBreakDownInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objPaymentBreakDownInfo.MODIFIED_BY = int.Parse(GetUserId());
					objPaymentBreakDownInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    
	
					//Updating the record using business layer class object
					intRetVal	= objPaymentBreakDown.Update(objOldPaymentBreakDownInfo,objPaymentBreakDownInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");						
						GetOldDataXML(!LOAD_OLD_DATA);
					}		
					else if(intRetVal==-1)
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("816");					
					}	
					else if(intRetVal==-2)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"5");
					}	
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");						
					}					
				}
				lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			    
			}
			finally
			{
				
			}
		}
	}
}
