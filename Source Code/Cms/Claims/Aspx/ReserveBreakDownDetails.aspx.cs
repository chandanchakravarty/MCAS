/******************************************************************************************
<Author					: -   Sumit Chhabra
<Start Date				: -	  29 May , 2006
<End Date				: -	
<Description			: -  Scheduled Items / Coverages for Inland Marine (Policy) at Claims
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
	public class ReserveBreakDownDetails : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESERVE_BREAKDOWN_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;		
		protected System.Web.UI.WebControls.Label capTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.Label lblTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.Label capTRANSACTION_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbTRANSACTION_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSACTION_CODE;
		protected System.Web.UI.WebControls.Label capBASIS;
		protected System.Web.UI.WebControls.DropDownList cmbBASIS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBASIS;
		protected System.Web.UI.WebControls.Label capVALUE;
		protected System.Web.UI.WebControls.TextBox txtVALUE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVALUE;
		protected System.Web.UI.WebControls.Label capAMOUNT;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		private string strRowId;
		private bool LOAD_OLD_DATA = true;
		protected System.Web.UI.WebControls.RangeValidator rngVALUE;
		
		System.Resources.ResourceManager objResourceMgr;		

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="308_0";	

			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Write;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.ReserveBreakDownDetails"  ,System.Reflection.Assembly.GetExecutingAssembly());
			// Put user code to initialize the page here
			if ( !Page.IsPostBack)
			{
				SetCaptions();
				SetErrorMessages();
				LoadDropDowns();
				GetQueryStringValues();
				GetOldDataXML(LOAD_OLD_DATA);
				btnBack.Attributes.Add("onClick","javascript: return GoBack();");
				cmbBASIS.Attributes.Add("onChange","javascript: return CalculateAmount();");
				txtVALUE.Attributes.Add("onBlur","javascript: return CalculateAmount();");				
				GetTotalOutstanding();		
			}			
		}

		private void SetErrorMessages()
		{
			rfvTRANSACTION_CODE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvBASIS.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvVALUE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rngVALUE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
		}

		private void GetTotalOutstanding()
		{
			ClsActivity objActivity = new ClsActivity();
			DataSet dsOutstanding =  objActivity.GetValuesForPageControls(hidCLAIM_ID.Value, hidACTIVITY_ID.Value);
			if(dsOutstanding!=null && dsOutstanding.Tables.Count>0 && dsOutstanding.Tables[0].Rows.Count>0 && dsOutstanding.Tables[0].Rows[0]["RESERVE_AMOUNT"]!=null && dsOutstanding.Tables[0].Rows[0]["RESERVE_AMOUNT"].ToString()!="")
				lblTOTAL_OUTSTANDING.Text=String.Format("{0:,#,###}",Convert.ToInt64(dsOutstanding.Tables[0].Rows[0]["RESERVE_AMOUNT"]));
		}

		private ClsReserveBreakDownInfo GetFormValue()
		{
			ClsReserveBreakDownInfo objReserveBreakDownInfo = new ClsReserveBreakDownInfo();
			if(cmbTRANSACTION_CODE.SelectedItem!=null && cmbTRANSACTION_CODE.SelectedItem.Value!="")
				objReserveBreakDownInfo.TRANSACTION_CODE = int.Parse(cmbTRANSACTION_CODE.SelectedItem.Value);
			if(cmbBASIS.SelectedItem!=null && cmbBASIS.SelectedItem.Value!="")
				objReserveBreakDownInfo.BASIS = int.Parse(cmbBASIS.SelectedItem.Value);
			if(txtVALUE.Text.Trim()!="")
				objReserveBreakDownInfo.VALUE = Convert.ToDouble(txtVALUE.Text.Trim());
			if(txtAMOUNT.Text.Trim()!="")
				objReserveBreakDownInfo.AMOUNT = Convert.ToDouble(txtAMOUNT.Text.Trim());

			objReserveBreakDownInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
			objReserveBreakDownInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
			if(hidRESERVE_BREAKDOWN_ID.Value!="" && hidRESERVE_BREAKDOWN_ID.Value!="0")
			{
				objReserveBreakDownInfo.RESERVE_BREAKDOWN_ID = int.Parse(hidRESERVE_BREAKDOWN_ID.Value);
				strRowId = hidRESERVE_BREAKDOWN_ID.Value;
			}
			else
				strRowId = "NEW";

			return objReserveBreakDownInfo;
			
		}

		

		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			capTOTAL_OUTSTANDING.Text			=		objResourceMgr.GetString("lblTOTAL_OUTSTANDING");	
			capTRANSACTION_CODE.Text			=		objResourceMgr.GetString("cmbTRANSACTION_CODE");	
			capBASIS.Text						=		objResourceMgr.GetString("cmbBASIS");	
			capVALUE.Text						=		objResourceMgr.GetString("txtVALUE");	
			capAMOUNT.Text						=		objResourceMgr.GetString("txtAMOUNT");	
		}
		#endregion

		private void LoadDropDowns()
		{
			DataTable dtTransactionCodes = ClsDefaultValues.GetDefaultValuesDetails((int)enumClaimDefaultValues.CLAIM_TRANSACTION_CODE,(int)enumTransactionLookup.RESERVE_UPDATE);  
			if(dtTransactionCodes!=null && dtTransactionCodes.Rows.Count>0)
			{
				cmbTRANSACTION_CODE.DataSource	=	dtTransactionCodes;
				cmbTRANSACTION_CODE.DataTextField=	"DETAIL_TYPE_DESCRIPTION";
				cmbTRANSACTION_CODE.DataValueField=	"DETAIL_TYPE_ID";
				cmbTRANSACTION_CODE.DataBind();
			}

			cmbBASIS.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLMBS");
			cmbBASIS.DataTextField="LookupDesc";
			cmbBASIS.DataValueField="LookupID";
			cmbBASIS.DataBind();
			cmbBASIS.Items.Insert(0,"");
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
			if(Request.QueryString["RESERVE_BREAKDOWN_ID"]!=null && Request.QueryString["RESERVE_BREAKDOWN_ID"].ToString()!="")
				hidRESERVE_BREAKDOWN_ID.Value = Request.QueryString["RESERVE_BREAKDOWN_ID"].ToString();
			else
				hidRESERVE_BREAKDOWN_ID.Value = "0";

			
		}

		private void GetOldDataXML(bool LOAD_DATA_FLAG)
		{
			DataTable dtOldData = ClsReserveBreakDown.GetXmlForPageControls(hidCLAIM_ID.Value, hidACTIVITY_ID.Value,hidRESERVE_BREAKDOWN_ID.Value);
			if(dtOldData!=null && dtOldData.Rows.Count>0)
			{
				hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
				if(LOAD_DATA_FLAG)
					LoadData(dtOldData);
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
				if(dtLoadData.Rows[0]["BASIS"]!=null && dtLoadData.Rows[0]["BASIS"].ToString()!="")
					cmbBASIS.SelectedValue = dtLoadData.Rows[0]["BASIS"].ToString();
				txtVALUE.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtLoadData.Rows[0]["VALUE"]));
				txtAMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtLoadData.Rows[0]["AMOUNT"]));				
			}
		}

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsReserveBreakDown objReserveBreakDown = new ClsReserveBreakDown();				

				//Retreiving the form values into model class object
				ClsReserveBreakDownInfo objReserveBreakDownInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objReserveBreakDownInfo.CREATED_BY = int.Parse(GetUserId());
					objReserveBreakDownInfo.CREATED_DATETIME = DateTime.Now;
					objReserveBreakDownInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objReserveBreakDown.Add(objReserveBreakDownInfo);

					if(intRetVal>0)
					{
						hidRESERVE_BREAKDOWN_ID.Value = objReserveBreakDownInfo.RESERVE_BREAKDOWN_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");						
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(!LOAD_OLD_DATA);
					}	
					else if(intRetVal==-1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"4");						
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
					ClsReserveBreakDownInfo objOldReserveBreakDownInfo = new ClsReserveBreakDownInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReserveBreakDownInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objReserveBreakDownInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReserveBreakDownInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    
	
					//Updating the record using business layer class object
					intRetVal	= objReserveBreakDown.Update(objOldReserveBreakDownInfo,objReserveBreakDownInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");						
						GetOldDataXML(!LOAD_OLD_DATA);
					}		
					else if(intRetVal==-1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"4");						
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
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}
	}
}
