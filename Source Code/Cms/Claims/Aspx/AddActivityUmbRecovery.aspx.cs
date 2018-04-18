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
	public class AddActivityUmbRecovery : Cms.Claims.ClaimBase		
	{
		
		System.Resources.ResourceManager objResourceMgr;	
		private double TotalOutstanding=0;
		private double TotalRIReserve=0;
		public static string RecoveryDataGridID="";		
		const int POLICY_RECOVERY_TABLE=0;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgRecovery;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtTotalRI_Reserve;	
		protected System.Web.UI.WebControls.TextBox txtTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
		protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvCLAIM_DESCRIPTION;
//		protected System.Web.UI.WebControls.Label capACTION_ON_RECOVERY;
//		protected System.Web.UI.WebControls.DropDownList cmbACTION_ON_RECOVERY;
		protected System.Web.UI.WebControls.DropDownList cmbDrAccts;
		protected System.Web.UI.WebControls.DropDownList cmbCrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCrAccts;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected Cms.CmsWeb.Controls.CmsButton btnPaymentBreakdown;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehicleGridRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTotalPayments;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRecovery;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyRowCount;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPayeeID;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_RECOVERY;
		
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="309_5";	

			btnPaymentBreakdown.CmsButtonClass		=	CmsButtonType.Read;
			btnPaymentBreakdown.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			//btnGoBack.CmsButtonClass		=	CmsButtonType.Read;
			//btnGoBack.PermissionString		=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddActivityUmbRecovery"  ,System.Reflection.Assembly.GetExecutingAssembly());
			// Put user code to initialize the page here
			
			string strClaimStatus = GetClaimStatus();
			if(strClaimStatus == CLAIM_STATUS_CLOSED)
				btnSave.Visible=false;
			else
				btnSave.Visible=true;


			if ( !Page.IsPostBack)
			{
				RecoveryDataGridID = dgRecovery.ID;				
				SetCaptions();				
				GetQueryStringValues();
				GetClaimDetails();
				LoadDropDowns();
				btnBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");
				cmbPAYMENT_METHOD.Attributes.Add("onChange","javascript:return cmbPAYMENT_METHOD_Change();");
				//btnGoBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");				
				//Temp
				//btnSave.Attributes.Add("onClick","javascript: return CompareAllOutstandingAndPayment();");
				//btnPaymentBreakdown.Attributes.Add("onClick","javascript: return OpenWindow();");				
				GetOldData();	
				//GetActivityDate();
				GetActivityDescription();				
			}		
			//SetClaimTop();
		}
		
		private void GetClaimDetails()
		{
			ClsActivity objActivity = new ClsActivity();
			DataSet dsTemp = objActivity.GetClaimDetails(int.Parse(hidCLAIM_ID.Value));
			if (dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0)
			{
				DataRow dr = dsTemp.Tables[0].Rows[0];
				hidCustomerID.Value = dr["CUSTOMER_ID"].ToString();
				hidPolicyID.Value = dr["POLICY_ID"].ToString();
				hidPolicyVersionID.Value = dr["POLICY_VERSION_ID"].ToString();
				hidLOB_ID.Value = dr["POLICY_LOB"].ToString();
				//hidSTATE_ID.Value = dr["STATE_ID"].ToString();
			}
		}

		private void GetActivityDescription()
		{
			ClsActivity objActivity = new ClsActivity();
			DataSet dsActivity = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			if(dsActivity.Tables[0].Rows[0]["DESCRIPTION"]!=null)
				txtCLAIM_DESCRIPTION.Text = dsActivity.Tables[0].Rows[0]["DESCRIPTION"].ToString();
		}

		private void LoadDropDowns()
		{
			ClsActivityRecovery objActivityRecovery = new ClsActivityRecovery();
			if(objActivityRecovery.LoadAcntgDropDowns(cmbDrAccts,cmbCrAccts,hidACTION_ON_RECOVERY.Value)==-1)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("931");
				lblMessage.Visible = true;
			}
			ClsPayment objPayment = new ClsPayment();
			objPayment.PopulatePaymentMethodDropDown(cmbPAYMENT_METHOD,"PYMTD",((int)Cms.CmsWeb.cmsbase.enumPaymentMethod.EFT).ToString());
			/*DataTable dtTransactionCodes = ClsDefaultValues.GetDefaultValuesDetails((int)enumClaimDefaultValues.CLAIM_TRANSACTION_CODE,(int)enumTransactionLookup.RECOVERY);  
			if(dtTransactionCodes!=null && dtTransactionCodes.Rows.Count>0)
			{
				cmbACTION_ON_RECOVERY.DataSource = dtTransactionCodes;
				cmbACTION_ON_RECOVERY.DataTextField="DETAIL_TYPE_DESCRIPTION";
				cmbACTION_ON_RECOVERY.DataValueField="DETAIL_TYPE_ID";
				cmbACTION_ON_RECOVERY.DataBind();
				cmbACTION_ON_RECOVERY.SelectedIndex = 1 ;//Set listbox selected to Reduce Reserves by payment amount 
			}*/
		}	

		

		private void GetOldData()
		{
			DataSet dsOldData = ClsActivityRecovery.GetOldDataForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			//when the claim id is null/0/'', we have no data at claim payment table, 
			//we need to fetch data from reserve table as the user has come to this page for the first time
			if(dsOldData==null ||  dsOldData.Tables.Count<1 || dsOldData.Tables[0]==null || dsOldData.Tables[0].Rows.Count<1)
			{
				ClsReserveDetails  objReserveDetails = ClsReserveDetails.CreateReserveObject(hidLOB_ID.Value);
				dsOldData = objReserveDetails.GetOldData(hidCLAIM_ID.Value);
				if(dsOldData==null || dsOldData.Tables.Count<1)
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
					lblMessage.Visible = true;
					dgRecovery.Visible = false;										
					trTotalPayments.Attributes.Add("style","display:none");
					trRecovery.Attributes.Add("style","display:none");
					trBody.Attributes.Add("style","display:none");
					btnSave.Visible = false;
					return;
				}
				//Add columns for Payment Amount and Action on Payment as they are not present at reserve table..
				for(int i=0;i<dsOldData.Tables.Count;i++)
				{
					DataColumn dtCol = new DataColumn("AMOUNT");
					dsOldData.Tables[i].Columns.Add(dtCol);
					dtCol = new DataColumn("ACTION_ON_RECOVERY");
					dsOldData.Tables[i].Columns.Add(dtCol);
					dtCol = new DataColumn("RECOVERY_ID");
					dsOldData.Tables[i].Columns.Add(dtCol);				
				}
				hidOldData.Value = "";
			}	
			else
			{
				hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);				
				LoadData(dsOldData.Tables[0]);
			}

			BindGrid(dsOldData);
		}

		
		private void LoadData(DataTable dtOldData)
		{
			if(dtOldData!=null && dtOldData.Rows.Count>0)
			{
				if(dtOldData.Rows[0]["ACTION_ON_RECOVERY"]!=null && dtOldData.Rows[0]["ACTION_ON_RECOVERY"].ToString()!="")
					hidACTION_ON_RECOVERY.Value = dtOldData.Rows[0]["ACTION_ON_RECOVERY"].ToString();				

				if(dtOldData.Rows[0]["DrAccts"]!=null && dtOldData.Rows[0]["DrAccts"].ToString()!="")
					cmbDrAccts.SelectedValue = dtOldData.Rows[0]["DrAccts"].ToString();

				if(dtOldData.Rows[0]["CrAccts"]!=null && dtOldData.Rows[0]["CrAccts"].ToString()!="")
					cmbCrAccts.SelectedValue = dtOldData.Rows[0]["CrAccts"].ToString();
				if(dtOldData.Rows[0]["RECOVERY_PAYMENT_METHOD"]!=null && dtOldData.Rows[0]["RECOVERY_PAYMENT_METHOD"].ToString()!="")
					cmbPAYMENT_METHOD.SelectedValue = dtOldData.Rows[0]["RECOVERY_PAYMENT_METHOD"].ToString();
				if(dtOldData.Rows[0]["CHECK_NUMBER"]!=null && dtOldData.Rows[0]["CHECK_NUMBER"].ToString()!="")
					txtCHECK_NUMBER.Text = dtOldData.Rows[0]["CHECK_NUMBER"].ToString();
			}
		}

		
		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			capPAYMENT_METHOD.Text	=		objResourceMgr.GetString("cmbPAYMENT_METHOD");
			capCLAIM_DESCRIPTION.Text 		=		objResourceMgr.GetString("txtCLAIM_DESCRIPTION");			
			rfvPAYMENT_METHOD.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
//			capACTION_ON_RECOVERY.Text 	=		objResourceMgr.GetString("cmbACTION_ON_RECOVERY");						
			
		}
		#endregion

		
		
		private void BindGrid(DataSet dsData)
		{
			if(dsData==null || dsData.Tables.Count<1 || dsData.Tables[POLICY_RECOVERY_TABLE]==null || dsData.Tables[POLICY_RECOVERY_TABLE].Rows.Count<1)
			{
				lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
				lblMessage.Visible = true;
				dgRecovery.Visible = false;
				return;
			}			

			if(dsData!=null && dsData.Tables.Count>POLICY_RECOVERY_TABLE && dsData.Tables[POLICY_RECOVERY_TABLE]!=null && dsData.Tables[POLICY_RECOVERY_TABLE].Rows.Count>0)
			{
				dgRecovery.DataSource = dsData.Tables[POLICY_RECOVERY_TABLE];
				dgRecovery.DataBind();	
				hidPolicyRowCount.Value = dsData.Tables[POLICY_RECOVERY_TABLE].Rows.Count.ToString();
			}
			else
			{
				dgRecovery.Visible = false;
				trRecovery.Attributes.Add("style","display:none");
			}

			if(TotalOutstanding!=0 && TotalOutstanding!=0.0)
				txtTOTAL_OUTSTANDING.Text=Double.Parse(TotalOutstanding.ToString()).ToString("N");
				//txtTOTAL_OUTSTANDING.Text=String.Format("{0:,#,###}",Convert.ToInt64(TotalOutstanding.ToString()));

			if(TotalRIReserve!=0 && TotalRIReserve!=0.0)
				txtTotalRI_Reserve.Text=Double.Parse(TotalRIReserve.ToString()).ToString("N");
			
			
		}

		private void dgRecovery_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				Label lblOutstanding = (Label)e.Item.FindControl("lblOUTSTANDING");								
				Label lblRI_Reserve = (Label)e.Item.FindControl("lblRI_RESERVE");
				
				TextBox txtAMOUNT = (TextBox)(e.Item.FindControl("txtAMOUNT"));
				if(txtAMOUNT!=null)
				{
					//txtAMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();CompareOutstandingAndPayment('" + lblOutstanding.ClientID + "','" + txtAMOUNT.ClientID + "');");
					if (DataBinder.Eval(e.Item.DataItem,"AMOUNT") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"AMOUNT"))!="0") 
						txtAMOUNT.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"AMOUNT"));
					else
						txtAMOUNT.Text = "";
					txtAMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();");
					//((RangeValidator)(e.Item.FindControl("rngAMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				
					//((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 11 Feb 09 for Itrack Issue 5385
					//Done for Itrack Issue 7016 on 3 Feb 2010
					//((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpDoublePositiveZero;
					((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpDoublePositiveNonZero;
					((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 11 Feb 09 for Itrack Issue 5385
				}
				((RangeValidator)(e.Item.FindControl("rngAMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				
				
				if (DataBinder.Eval(e.Item.DataItem,"RI_RESERVE") != System.DBNull.Value)
					lblRI_Reserve.Text = Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"RI_RESERVE"))).ToString("N");
				else
					lblRI_Reserve.Text = "";


				//if(lblOutstanding.Text.Trim()!="" && int.Parse(lblOutstanding.Text.replace(/\,/g,''))>0)
				if(lblOutstanding!=null && DataBinder.Eval(e.Item.DataItem,"OUTSTANDING") != System.DBNull.Value)
				{
					//string strOutstanding = lblOutstanding.Text.replace(/\,/g,'');
					//lblOutstanding.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING"));
					lblOutstanding.Text = Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING"))).ToString("N");
					TotalOutstanding += Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING")));					
				}
				else
				{
					txtAMOUNT.Attributes.Add("readOnly","true");
					txtAMOUNT.Attributes.Add("style","background-color:#C0C0C0;");
				}			
				if(lblRI_Reserve!=null && lblRI_Reserve.Text.Trim()!="")
				{
					string strRI_RESERVE = lblRI_Reserve.Text.Replace(",","");
					TotalRIReserve += Convert.ToDouble(strRI_RESERVE);					
				}					
			}
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
			this.dgRecovery.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgRecovery_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")			
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();				
			else
				hidCLAIM_ID.Value = "0";

			if(Request["ACTIVITY_ID"]!=null && Request["ACTIVITY_ID"].ToString()!="")
				hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();
			else
				hidACTIVITY_ID.Value = "0";

			if(Request["ACTION_ON_RECOVERY"]!=null && Request["ACTION_ON_RECOVERY"].ToString()!="")
				hidACTION_ON_RECOVERY.Value = Request["ACTION_ON_RECOVERY"].ToString();
			else
				hidACTION_ON_RECOVERY.Value = "0";

			if(GetLOBID()!=null && GetLOBID()!="")
				hidLOB_ID.Value = GetLOBID();								

		}		

		private void PopulateArray(ArrayList aPaymentList,DataGrid dtGrid)
		{
			foreach(DataGridItem dgi in dtGrid.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					
					//Model Object
					ClsActivityRecoveryInfo objActivityRecoveryInfo = new	 ClsActivityRecoveryInfo();
					
					TextBox txtAMOUNT = (TextBox)(dgi.FindControl("txtAMOUNT"));
					
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));
					Label lblRECOVERY_ID = (Label)(dgi.FindControl("lblRECOVERY_ID"));
					Label lblVEHICLE_ID = (Label)(dgi.FindControl("lblVEHICLE_ID"));
					
					if(hidACTION_ON_RECOVERY.Value!="")
						objActivityRecoveryInfo.ACTION_ON_RECOVERY = int.Parse(hidACTION_ON_RECOVERY.Value);

					objActivityRecoveryInfo.IS_ACTIVE="Y";
					objActivityRecoveryInfo.CREATED_BY = int.Parse(GetUserId());
					objActivityRecoveryInfo.CREATED_DATETIME = System.DateTime.Now;
					objActivityRecoveryInfo.MODIFIED_BY = int.Parse(GetUserId());
					objActivityRecoveryInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
					objActivityRecoveryInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					objActivityRecoveryInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);					

					if(lblRESERVE_ID!=null && lblRESERVE_ID.Text.Trim()!="")
						objActivityRecoveryInfo.RESERVE_ID = int.Parse(lblRESERVE_ID.Text.Trim());
					if(txtAMOUNT!=null && txtAMOUNT.Text.Trim()!="")
						objActivityRecoveryInfo.AMOUNT = Convert.ToDouble(txtAMOUNT.Text.Trim());
					if(lblRECOVERY_ID!=null && lblRECOVERY_ID.Text.Trim()!="")
						objActivityRecoveryInfo.RECOVERY_ID = int.Parse(lblRECOVERY_ID.Text.Trim());										

					if(cmbCrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
						objActivityRecoveryInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedItem.Value);

					if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
						objActivityRecoveryInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedItem.Value);
					if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="")
						objActivityRecoveryInfo.PAYMENT_METHOD = Convert.ToInt32(cmbPAYMENT_METHOD.SelectedValue);
					
					if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="" && cmbPAYMENT_METHOD.SelectedItem.Value=="11979")
						objActivityRecoveryInfo.CHECK_NUMBER = txtCHECK_NUMBER.Text.Trim() ;					

					aPaymentList.Add(objActivityRecoveryInfo);
				}
			}
		}

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				int RECOVERY_ACTION;
				//string strPAYEE;						
				ClsActivityRecovery objRecovery = new ClsActivityRecovery();
				ArrayList ExpenseArrayList = new ArrayList();				
				PopulateArray(ExpenseArrayList,dgRecovery);

				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					intRetVal = objRecovery.Add(ExpenseArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");						
				}
				else
				{
					intRetVal = objRecovery.Update(ExpenseArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"31");					
				}					
				if(intRetVal>0)
				{			
					//ClsClaimsNotification.UpdateClaimDescription(hidCLAIM_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					ClsActivity objActivity = new ClsActivity();
					objActivity.UpdateActivityDescription(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					if(hidACTION_ON_RECOVERY.Value!="")
						RECOVERY_ACTION = int.Parse(hidACTION_ON_RECOVERY.Value);
					else
						RECOVERY_ACTION = -1;
					ClsActivityRecovery.UpdateRecoveryActivity(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);					
					GetOldData();
					
				}				
				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
					
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
