/******************************************************************************************
	<Author					: - > Vijay Arora 
	<Start Date				: -	> 06-06-2006
	<End Date				: - >
	<Description			: - > Class for Claims Expense Breakdown Activity
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
	public class AddActivityExpenseBreakdown : Cms.Claims.ClaimBase
	{
		#region Local form variables
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;		
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capTRANSACTION_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbTRANSACTION_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSACTION_CODE;
		protected System.Web.UI.WebControls.Label capCOVERAGE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_ID;
		protected System.Web.UI.WebControls.Label capPAID_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtPAID_AMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAID_AMOUNT;
		protected System.Web.UI.WebControls.RangeValidator rngPAID_AMOUNT;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPENSE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPENSE_BREAKDOWN_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		ClsActivityExpenseBreakdown objAEB = new ClsActivityExpenseBreakdown();
		#endregion
	
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="505_1";
			lblMessage.Visible = false;
			
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddActivityExpenseBreakdown" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			if(!Page.IsPostBack)
			{			
				btnReset.Attributes.Add("onclick","javascript:return formReset();");
				txtPAID_AMOUNT.Attributes.Add("onBlur","javascript: this.value = formatCurrency(this.value);");
				if(Request["CLAIM_ID"]!=null)
				{
					hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();
					hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();
				}
				else	
				{
					hidCLAIM_ID.Value = "";
					hidACTIVITY_ID.Value = "";
				}

				if (Request["EXPENSE_ID"] != null)
					hidEXPENSE_ID .Value = Request["EXPENSE_ID"].ToString();
				else
					hidEXPENSE_ID.Value = "";

				if (Request["EXPENSE_BREAKDOWN_ID"] != null)
					hidEXPENSE_BREAKDOWN_ID.Value = Request["EXPENSE_BREAKDOWN_ID"].ToString();
				else
					hidEXPENSE_BREAKDOWN_ID.Value = "";

				FillDropDowns();
				SetCaptions();
				SetErrorMessages();		

				if(Request["CLAIM_ID"]!=null && Request["EXPENSE_ID"]!=null && Request["EXPENSE_BREAKDOWN_ID"]!=null)
				{
					LoadData();
					GetOldDataXML();
				}

			}
			
		}
		#endregion


		
		#region GetOldDataXML
		private void GetOldDataXML()
		{
			if (hidCLAIM_ID.Value != "" && hidEXPENSE_ID.Value != "" && hidEXPENSE_BREAKDOWN_ID.Value != "" && hidACTIVITY_ID.Value != "")
				hidOldData.Value	=	objAEB.GetXmlForPageControls(hidCLAIM_ID.Value,hidEXPENSE_ID.Value,hidEXPENSE_BREAKDOWN_ID.Value,hidACTIVITY_ID.Value);
			else
				hidOldData.Value	=	"";
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
			rfvTRANSACTION_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("793");
			rfvCOVERAGE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("794");
			rfvPAID_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("795");
			rngPAID_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
 		}
		#endregion

		#region FillDropDowns
		private void FillDropDowns()
		{
			cmbTRANSACTION_CODE.DataSource =  ClsDefaultValues.GetDefaultValuesDetails(8,11774);  //Claim Transaction Code, Expense Payments
			cmbTRANSACTION_CODE.DataTextField="DETAIL_TYPE_DESCRIPTION";
			cmbTRANSACTION_CODE.DataValueField="DETAIL_TYPE_ID";
			cmbTRANSACTION_CODE.DataBind();
			cmbTRANSACTION_CODE.Items.Insert(0,"");

			DataSet dsTemp = ClsClaims.GetClaimCoverages(hidCLAIM_ID.Value);			
			if(dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
			{
				cmbCOVERAGE_ID.DataSource =  dsTemp.Tables[0];
				cmbCOVERAGE_ID.DataTextField="DESCRIPTION";
				cmbCOVERAGE_ID.DataValueField="ID";
				cmbCOVERAGE_ID.DataBind();
				cmbCOVERAGE_ID.Items.Insert(0,"");
			}
			else
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("804");
				lblMessage.Visible = true;
			}
		}
		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
			capTRANSACTION_CODE.Text	=		objResourceMgr.GetString("cmbTRANSACTION_CODE");
			capCOVERAGE_ID.Text			=		objResourceMgr.GetString("cmbCOVERAGE_ID");
			capPAID_AMOUNT.Text			=		objResourceMgr.GetString("txtPAID_AMOUNT");
		}
		#endregion

		#region GetFormValue
		private ClsActivityExpenseBreakdownInfo GetFormValue()
		{
			ClsActivityExpenseBreakdownInfo objAEBInfo = new ClsActivityExpenseBreakdownInfo();
			
			if(hidCLAIM_ID.Value != "")
				objAEBInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
				
			if (hidEXPENSE_ID.Value != "")
				objAEBInfo.EXPENSE_ID = int.Parse(hidEXPENSE_ID.Value);
		
			if (hidEXPENSE_BREAKDOWN_ID.Value != "")
				objAEBInfo.EXPENSE_BREAKDOWN_ID = int.Parse(hidEXPENSE_BREAKDOWN_ID.Value);
		
			if (hidACTIVITY_ID.Value != "")
				objAEBInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);

			if (cmbTRANSACTION_CODE.SelectedValue != "" && int.Parse(cmbTRANSACTION_CODE.SelectedValue) > 0) 
				objAEBInfo.TRANSACTION_CODE = int.Parse(cmbTRANSACTION_CODE.SelectedValue); 
							
			if (cmbCOVERAGE_ID.SelectedValue != "" && int.Parse(cmbCOVERAGE_ID.SelectedValue) > 0) 
				objAEBInfo.COVERAGE_ID = int.Parse(cmbCOVERAGE_ID.SelectedValue); 
				
			objAEBInfo.PAID_AMOUNT = Convert.ToDouble(txtPAID_AMOUNT.Text); 

					
			if(hidEXPENSE_BREAKDOWN_ID.Value == "")
				strRowId="NEW";
			else
			{
				strRowId=hidEXPENSE_BREAKDOWN_ID.Value; 
				objAEBInfo.EXPENSE_BREAKDOWN_ID  =	int.Parse(hidEXPENSE_BREAKDOWN_ID .Value);	
			}
			return objAEBInfo;
		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	

				//Retreiving the form values into model class object
				ClsActivityExpenseBreakdownInfo objAEBInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objAEBInfo.CREATED_BY = int.Parse(GetUserId());
					objAEBInfo.CREATED_DATETIME = DateTime.Now;
					
					//Calling the add method of business layer class
					intRetVal = objAEB.Add(objAEBInfo);

					if(intRetVal>0)
					{
						hidEXPENSE_BREAKDOWN_ID.Value = objAEBInfo.EXPENSE_BREAKDOWN_ID.ToString();
						lblMessage.Text				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value			= "Y";
						LoadData();
						GetOldDataXML();
					}
					else if(intRetVal == -1) //Duplicate 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("816");
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
					ClsActivityExpenseBreakdownInfo objOldAEBInfo = new ClsActivityExpenseBreakdownInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldAEBInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objAEBInfo.MODIFIED_BY = int.Parse(GetUserId());
					objAEBInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					//Updating the record using business layer class object
					intRetVal	= objAEB.Update(objOldAEBInfo,objAEBInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						LoadData();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("816");
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
		private void LoadData()
		{
			if (hidCLAIM_ID.Value != "" && hidEXPENSE_ID.Value != "" && hidEXPENSE_BREAKDOWN_ID.Value != "" && hidACTIVITY_ID.Value != "")
			{
				DataSet dsTemp =   objAEB.GetValuesForPageControls(hidCLAIM_ID.Value,hidEXPENSE_ID.Value,hidEXPENSE_BREAKDOWN_ID.Value, hidACTIVITY_ID.Value);
				if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
				{
					DataRow dr = dsTemp.Tables[0].Rows[0];
					
					if (int.Parse(dr["TRANSACTION_CODE"].ToString()) > 0)
						cmbTRANSACTION_CODE.SelectedValue = dr["TRANSACTION_CODE"].ToString();
					else
						cmbTRANSACTION_CODE.SelectedIndex = 0;

					if (int.Parse(dr["COVERAGE_ID"].ToString()) > 0)
						cmbCOVERAGE_ID.SelectedValue = dr["COVERAGE_ID"].ToString();
					else
						cmbCOVERAGE_ID.SelectedIndex = 0;

					if(dr["PAID_AMOUNT"]!=null && dr["PAID_AMOUNT"].ToString()!="")
						txtPAID_AMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dr["PAID_AMOUNT"]));

					//txtPAID_AMOUNT.Text = dr["PAID_AMOUNT"].ToString();

				}
			}
		}
		#endregion
		

	}
}
