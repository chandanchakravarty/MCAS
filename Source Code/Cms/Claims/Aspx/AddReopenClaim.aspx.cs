/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> 07-07-2006
	<End Date				: - >
	<Description			: - > Page for Claims Re-Open Claims
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
	public class AddReopenClaim : Cms.Claims.ClaimBase
	{
		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREOPEN_COUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREOPEN_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREOPEN_BY;		
		protected System.Web.UI.WebControls.Label capREOPEN_DATE;
		protected System.Web.UI.WebControls.TextBox txtREOPEN_DATE;
		//protected System.Web.UI.WebControls.HyperLink hlkREOPEN_DATE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvREOPEN_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREASON;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revREOPEN_DATE;
		protected System.Web.UI.WebControls.Label capREOPEN_BY;
		protected System.Web.UI.WebControls.TextBox txtREOPEN_BY;
		protected System.Web.UI.WebControls.Label capREASON;
		protected System.Web.UI.WebControls.TextBox txtREASON;
		protected System.Web.UI.WebControls.CustomValidator csvREASON;
        protected System.Web.UI.WebControls.Label lblHeader;
		private bool LOAD_OLD_DATA = true;
		private ClsReopenClaim objReopenClaim = new ClsReopenClaim();
		
		
		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			
			base.ScreenId="306_10_0";
			
			lblMessage.Visible = false;
			
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddReopenClaim" ,System.Reflection.Assembly.GetExecutingAssembly());		

						
			
			if(!Page.IsPostBack)
			{		
				txtREOPEN_BY.Text = GetUserFLName();
				hidREOPEN_BY.Value = GetUserId();
				//hlkREOPEN_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_REOPEN_CLAIM.txtREOPEN_DATE,document.CLM_REOPEN_CLAIM.txtREOPEN_DATE)"); //Javascript Implementation for Date
				GetQueryStringValues();		
				SetCaptions();				
				SetErrorMessages();	
				GetOldData(LOAD_OLD_DATA);
                lblHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
				
			}
		}
		#endregion

		
		private void GetOldData(bool LOAD_DATA_FLAG)
		{
			if(hidREOPEN_ID.Value!="" && hidREOPEN_ID.Value!="0")
			{
				DataSet dsOldData = objReopenClaim.GetValuesForPageControls(hidCLAIM_ID.Value,hidREOPEN_ID.Value);
				if(dsOldData!=null && dsOldData.Tables.Count>0 && dsOldData.Tables[0].Rows.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);
					if(LOAD_DATA_FLAG)
						LoadData(dsOldData.Tables[0]);
				}
				else
					hidOldData.Value = "";
			}
			else
				txtREOPEN_DATE.Text = System.DateTime.Now.Date.ToShortDateString();
		}

		private void LoadData(DataTable dtLoadData)
		{
			if(dtLoadData!=null && dtLoadData.Rows.Count>0)
			{
				txtREOPEN_DATE.Text		=	dtLoadData.Rows[0]["REOPEN_DATE"].ToString();
				txtREOPEN_BY.Text		=	dtLoadData.Rows[0]["REOPEN_NAME"].ToString();
				txtREASON.Text			=	dtLoadData.Rows[0]["REASON"].ToString();
				if(dtLoadData.Rows[0]["REOPEN_COUNT"]!=null && dtLoadData.Rows[0]["REOPEN_COUNT"].ToString()!="")
					hidREOPEN_COUNT.Value = dtLoadData.Rows[0]["REOPEN_COUNT"].ToString();
			}
		}



		
		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();
			else	
				hidCLAIM_ID.Value = "";

			if(Request["REOPEN_ID"]!=null && Request["REOPEN_ID"].ToString()!="")
				hidREOPEN_ID.Value = Request["REOPEN_ID"].ToString();
			else	
				hidREOPEN_ID.Value = "";
			
		}
		

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
			//revREOPEN_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");			
			//revREOPEN_DATE.ValidationExpression			=		aRegExpDate;
			//rfvREOPEN_DATE.ErrorMessage					=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("826");			
			csvREASON.ErrorMessage						=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("442");
			rfvREASON.ErrorMessage						=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("763");			
		}
		#endregion

		#region FillDropDowns
		private void FillDropDowns()
		{
			
			
		}
		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
			capREOPEN_DATE.Text			=		objResourceMgr.GetString("txtREOPEN_DATE");
			capREOPEN_BY.Text			=		objResourceMgr.GetString("txtREOPEN_BY");
			capREASON.Text				=		objResourceMgr.GetString("txtREASON");
		}
		#endregion

		#region GetFormValue
		private ClsReopenClaimInfo GetFormValue()
		{
			ClsReopenClaimInfo objReopenClaimInfo = new ClsReopenClaimInfo();
			objReopenClaimInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
			if(txtREOPEN_DATE.Text.Trim()!="")
				objReopenClaimInfo.REOPEN_DATE = Convert.ToDateTime(txtREOPEN_DATE.Text.Trim());
			if(txtREOPEN_BY.Text.Trim()!="" && hidREOPEN_BY.Value!="")
				objReopenClaimInfo.REOPEN_BY = int.Parse(hidREOPEN_BY.Value);
			objReopenClaimInfo.REASON = txtREASON.Text.Trim();
			if(hidREOPEN_ID.Value=="" || hidREOPEN_ID.Value=="0")
				strRowId = "NEW";
			else
			{
				objReopenClaimInfo.REOPEN_ID = int.Parse(hidREOPEN_ID.Value);
				strRowId = hidREOPEN_ID.Value;
			}

			return objReopenClaimInfo;
		}
		#endregion		
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			//For retreiving the return value of business class save function				
			ClsReopenClaim objReopenClaim = new ClsReopenClaim();
			ClsReopenClaimInfo objReopenClaimInfo = new ClsReopenClaimInfo();

			try
			{
				int intRetVal;	
				//Retreiving the form values into model class object				
				objReopenClaimInfo = GetFormValue();
				
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objReopenClaimInfo.CREATED_BY = int.Parse(GetUserId());
					objReopenClaimInfo.CREATED_DATETIME = DateTime.Now;
					objReopenClaimInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objReopenClaim.Add(objReopenClaimInfo);

					if(intRetVal>0)
					{
						hidREOPEN_ID.Value = objReopenClaimInfo.REOPEN_ID.ToString();
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldData(!LOAD_OLD_DATA);						
					}					
					else
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data					
					ClsReopenClaimInfo objOldReopenClaimInfo = new  ClsReopenClaimInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReopenClaimInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objReopenClaimInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReopenClaimInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objReopenClaim.Update(objOldReopenClaimInfo,objReopenClaimInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldData(!LOAD_OLD_DATA);
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
			finally
			{
				if(objReopenClaim!=null)
					objReopenClaim.Dispose();
				if(objReopenClaimInfo!=null)
					objReopenClaimInfo = null;
			}
		}
	}
}
