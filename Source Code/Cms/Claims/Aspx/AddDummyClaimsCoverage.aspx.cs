/******************************************************************************************
<Author					: - Mohit Agarwal
<Start Date				: -	5-Dec-2007
<End Date				: -	
<Description			: - Manages MNT_CLAIM_COVERAGE
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
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
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 

namespace Claims.Aspx
{
	/// <summary>
	/// Summary description for AddDummyClaimsCoverage.
	/// </summary>
	public class AddDummyClaimsCoverage : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCOV_DES;
		protected System.Web.UI.WebControls.TextBox txtCOV_DES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOV_DES;
		protected System.Web.UI.WebControls.Label capLIMIT_1;
		protected System.Web.UI.WebControls.TextBox txtLIMIT_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLIMIT_1;
		protected System.Web.UI.WebControls.Label capDEDUCTIBLE_1;
		protected System.Web.UI.WebControls.TextBox txtDEDUCTIBLE_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDEDUCTIBLE_1;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivate;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOV_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOV_ID_CLAIM;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationSec;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="304_9";

			try
			{
				btnSave.CmsButtonClass	=	CmsButtonType.Write;
				btnSave.PermissionString		=	gstrSecurityXML;

				btnActivate.CmsButtonClass	=	CmsButtonType.Write;
				btnActivate.PermissionString		=	gstrSecurityXML;

				btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
				btnDelete.PermissionString		=	gstrSecurityXML;

				if(!IsPostBack)
				{	
					hidIS_ACTIVE.Value = "Y";
					btnActivate.Enabled = false;
					btnSave.Attributes.Add("onClick","javascript:saveSet();");
					txtLIMIT_1.Attributes.Add("onblur","javascript:FormatAmount();");//Added for Itrack Issue 5639 on 29 April 2009
					txtDEDUCTIBLE_1.Attributes.Add("onblur","javascript:FormatAmount();");//Added for Itrack Issue 5639 on 21 May 2009
					if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
						hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
					if(Request.QueryString["COV_ID"]!=null && Request.QueryString["COV_ID"].ToString()!="")
						hidCOV_ID.Value = Request.QueryString["COV_ID"].ToString();
					if(hidCOV_ID.Value == "0")
						hidCOV_ID.Value = ClsDummyClaimsCoverage.GetCoverageId(hidCOV_ID_CLAIM.Value,hidCLAIM_ID.Value);
				
					GetClaimDetails();
					SetCaptions();
					SetErrorMessages();
					FillData();

					
				}
				if(hidCOV_ID.Value == "0")
					hidCOV_ID.Value = ClsDummyClaimsCoverage.GetCoverageId(hidCOV_ID_CLAIM.Value,hidCLAIM_ID.Value);
				
				
				if(hidCOV_ID.Value != "0" && hidFormSaved.Value != "" && hidFormSaved.Value != "2")
					FillData();
			}
			catch(Exception ex)
			{
				lblMessage.Text = "Error encountered: " + ex.Message + "! Try Again.";
				lblMessage.Visible = true;
			}

			// Put user code to initialize the page here
		}

		private void GetClaimDetails()
		{
			ClsActivity objActivity = new ClsActivity();
			DataSet dsTemp = objActivity.GetClaimDetails(int.Parse(hidCLAIM_ID.Value));
			if (dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
			{
				DataRow dr = dsTemp.Tables[0].Rows[0];
				hidLOB_ID.Value = dr["POLICY_LOB"].ToString();
				hidSTATE_ID.Value = dr["STATE_ID"].ToString();
			}
		}

		private void SetCaptions()
		{
		}

		private void SetErrorMessages()
		{
			revLIMIT_1.ValidationExpression		=	aRegExpDoublePositiveNonZero;
			revDEDUCTIBLE_1.ValidationExpression		=	aRegExpDoublePositiveNonZero;
		}

		private void FillData()
		{
			DataTable dtClmCov = ClsDummyClaimsCoverage.GetClaimCoverageDataForDummyPolicy(int.Parse(hidCOV_ID.Value));
			if(dtClmCov!=null && dtClmCov.Rows.Count>0)
			{
				txtCOV_DES.Text = dtClmCov.Rows[0]["COV_DES"].ToString();
				txtLIMIT_1.Text = dtClmCov.Rows[0]["LIMIT_1"].ToString().Trim();
				txtDEDUCTIBLE_1.Text = dtClmCov.Rows[0]["DEDUCTIBLE_1"].ToString().Trim();
				hidIS_ACTIVE.Value = dtClmCov.Rows[0]["IS_ACTIVE"].ToString().Trim();
				if(hidIS_ACTIVE.Value == "Y")
					btnActivate.Text = "Deactivate";
				else
					btnActivate.Text = "Activate";
				btnActivate.Enabled = true;
			}
		}

		private ClsDummyClaimsCoverageInfo GetFormValue()
		{
			ClsDummyClaimsCoverageInfo objDummyClaimsCoverageInfo = new ClsDummyClaimsCoverageInfo();
			objDummyClaimsCoverageInfo.COV_DES		=	txtCOV_DES.Text.Trim();	
			if(txtLIMIT_1.Text.Trim() != "")
				objDummyClaimsCoverageInfo.LIMIT_1	=	Convert.ToDouble(txtLIMIT_1.Text.Trim());		
			else
				objDummyClaimsCoverageInfo.LIMIT_1	= -1.0;
	
			if(txtDEDUCTIBLE_1.Text.Trim() != "")
				objDummyClaimsCoverageInfo.DEDUCTIBLE_1	=	Convert.ToDouble(txtDEDUCTIBLE_1.Text.Trim());		
			else
				objDummyClaimsCoverageInfo.DEDUCTIBLE_1	= -1.0;
//			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
//				objDummyClaimsCoverageInfo.LOB_ID = Request.QueryString["LOB_ID"].ToString();
//	
//			if(Request.QueryString["STATE_ID"]!=null && Request.QueryString["STATE_ID"].ToString()!="")
//				objDummyClaimsCoverageInfo.STATE_ID = Request.QueryString["STATE_ID"].ToString();

			objDummyClaimsCoverageInfo.LOB_ID = hidLOB_ID.Value;
			objDummyClaimsCoverageInfo.STATE_ID = hidSTATE_ID.Value;

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				objDummyClaimsCoverageInfo.CLAIM_ID = int.Parse(Request.QueryString["CLAIM_ID"].ToString());

			objDummyClaimsCoverageInfo.IS_ACTIVE = "Y";

			return objDummyClaimsCoverageInfo;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnActivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsDummyClaimsCoverage objDummyClaimsCoverage = new ClsDummyClaimsCoverage();
				if(hidCOV_ID.Value != "0")
				{
					int retVal=0;
					if(hidIS_ACTIVE.Value == "Y")
					{
						retVal = objDummyClaimsCoverage.Activate(int.Parse(hidCOV_ID.Value), "N");
						if(retVal == 0)
						{
							hidIS_ACTIVE.Value = "N";
							btnActivate.Text = "Activate";
							hidFormSaved.Value = "3";
							lblMessage.Text = "Coverage Deactivated successfully";
							lblMessage.Visible = true;
						}
						else
						{
							lblMessage.Text = "Coverage could not be Deactivated";
							lblMessage.Visible = true;
						}
					}
					else
					{
						retVal = objDummyClaimsCoverage.Activate(int.Parse(hidCOV_ID.Value), "Y");
						if(retVal == 0)
						{
							hidIS_ACTIVE.Value = "Y";
							hidFormSaved.Value = "3";
							btnActivate.Text = "Deactivate";
							lblMessage.Text = "Coverage Activated successfully";
							lblMessage.Visible = true;
						}
						else
						{
							lblMessage.Text = "Coverage could not be Activated";
							lblMessage.Visible = true;
						}
					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = "Error encountered: " + ex.Message + "! Try Again.";
				lblMessage.Visible = true;
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsDummyClaimsCoverage objDummyClaimsCoverage = new ClsDummyClaimsCoverage();
				if(hidCOV_ID.Value != "0")
				{
					int retVal = objDummyClaimsCoverage.Delete(int.Parse(hidCOV_ID.Value));
					if(retVal == 0)
					{
						lblDelete.Text = "Coverage deleted successfully";
						lblDelete.Visible = true;
						hidFormSaved.Value = "3";
					}
					else
					{
						lblMessage.Text = "Coverage could not be deleted";
						lblMessage.Visible = true;
					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = "Error encountered: " + ex.Message + "! Try Again.";
				lblMessage.Visible = true;
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsDummyClaimsCoverage objDummyClaimsCoverage = new ClsDummyClaimsCoverage();
				ClsDummyClaimsCoverageInfo objDummyClaimsCoverageInfo = GetFormValue();
				objDummyClaimsCoverageInfo.CREATED_BY	=	int.Parse(GetUserId());
				objDummyClaimsCoverageInfo.CREATED_DATETIME	=	System.DateTime.Now;
				if(hidCOV_ID.Value == "0")
				{
					int cov_id = objDummyClaimsCoverage.Add(objDummyClaimsCoverageInfo);
					if(cov_id > 0)
					{
						hidCOV_ID.Value = cov_id.ToString();
						FillData();
						lblMessage.Text = "New Coverage added successfully";
						lblMessage.Visible = true;
						hidFormSaved.Value = "1";
					}
					else
					{
						lblMessage.Text = "New Coverage could not be added";
						lblMessage.Visible = true;
					}
				}
				else
				{
					objDummyClaimsCoverageInfo.COV_ID = int.Parse(hidCOV_ID.Value);
					objDummyClaimsCoverageInfo.MODIFIED_BY	=	int.Parse(GetUserId());
					objDummyClaimsCoverageInfo.LAST_UPDATED_DATETIME	=	System.DateTime.Now;
					int cov_id = objDummyClaimsCoverage.Update(objDummyClaimsCoverageInfo);
					if(cov_id == 0)
					{
						FillData();
						lblMessage.Text = "Coverage updated successfully";
						lblMessage.Visible = true;
						hidFormSaved.Value = "1";
					}
					else
					{
						lblMessage.Text = "Coverage could not be updated";
						lblMessage.Visible = true;
					}
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = "Error encountered: " + ex.Message + "! Try Again.";
				lblMessage.Visible = true;
			}
		}
	}
}
