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
	public class ReserveUmbDetails : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldTotalRiReserve;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldTotalOutstanding;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDummyPolicyCoverageRowCount;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRETENTION_LIMITS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRECOVERY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPENSES;
		protected Cms.CmsWeb.WebControls.ClaimActivityTop cltClaimActivityTop;
		protected string ActivityClientID,ActivityTotalPaymentClientID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_PAYMENT;
		protected System.Web.UI.WebControls.DropDownList cmbDrAccts;
		protected System.Web.UI.WebControls.DropDownList cmbCrAccts;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCrAccts;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNewReserve;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleRowCount;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;						
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyRowCount;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRANSACTION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRANSACTION_CATEGORY;
		protected System.Web.UI.WebControls.Label lblTitle;	
		protected System.Web.UI.WebControls.TextBox txtGrossTotal;
		System.Resources.ResourceManager objResourceMgr;	
		protected System.Web.UI.WebControls.Label capPOLICY_LIMITS;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_LIMITS;
		protected System.Web.UI.WebControls.Label capRETENTION_LIMITS;
		protected System.Web.UI.WebControls.TextBox txtRETENTION_LIMITS;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_LIMITS;
		
		
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;		
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected Cms.CmsWeb.Controls.CmsButton btnReserveBreakdown;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_OUTSTANDING;
		
		protected System.Web.UI.WebControls.TextBox txtTOTAL_RI_RESERVE;
		protected System.Web.UI.WebControls.Label capRECOVERY;		
		
		protected System.Web.UI.WebControls.Label capEXPENSES;
		protected System.Web.UI.WebControls.TextBox txtEXPENSES;
		
		protected System.Web.UI.HtmlControls.HtmlForm Form1;		
		protected System.Web.UI.WebControls.TextBox txtRECOVERY;
		protected System.Web.UI.WebControls.RangeValidator rngRECOVERY;
		protected System.Web.UI.WebControls.RangeValidator rngRETENTION_LIMITS;
		protected System.Web.UI.WebControls.RangeValidator rngEXPENSES;		
		
		protected System.Web.UI.WebControls.Label Label8;
		//IList iLookUp;				
		
		public static string CoverageGridID = "";
		
		const int COVERAGE_TABLE = 0;
		protected System.Web.UI.WebControls.DataGrid dgCoverages;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehicleRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehicleGridRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTotalRow;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="306_0_1_2";	

			btnReserveBreakdown.CmsButtonClass		=	CmsButtonType.Read;
			btnReserveBreakdown.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.ReserveUmbDetails"  ,System.Reflection.Assembly.GetExecutingAssembly());
			GetQueryStringValues();
			SetClaimTop();			
			// Put user code to initialize the page here			
			if ( !Page.IsPostBack)
			{
				CoverageGridID = dgCoverages.ID;
				
				SetSaveVisibility();
				SetNewReserveFlag();
				SetCaptions();
				SetErrorMessages();	
				LoadDropDown();
				LoadAcntgDropDowns();
				btnBack.Attributes.Add("onClick","javascript: return GoToClaimDetailPage();");
				//btnSave.Attributes.Add("onClick","javascript: return CompareAllLimitAndOutstanding();");
				GetClaimDetails();
				
				txtRECOVERY.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				txtEXPENSES.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				txtRETENTION_LIMITS.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				//Display an appropriate message for dummy policy
				/* ***** Comment Block Starts ***** 
				Commented by Asfa(06-Dec-2007) - iTrack issue #3141
				
				if((hidCustomerID.Value=="" || hidCustomerID.Value=="0") && (hidPolicyID.Value=="" || hidPolicyID.Value=="0") && (hidPolicyVersionID.Value=="" || hidPolicyVersionID.Value=="0"))
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("805");
					lblMessage.Visible = true;
					trBody.Attributes.Add("style","display:none");
					return;
				}
				/* ***** Comment Block Ends *****/				
				GetOldData();				
			}		
			ActivityClientID = cltClaimActivityTop.PanelClientID;			
			ActivityTotalPaymentClientID = cltClaimActivityTop.PanelPaymentClientID;
			SetClaimActivityTop();
		}

		private void LoadAcntgDropDowns()
		{
			ClsActivityRecovery objActivityRecovery = new ClsActivityRecovery();
			if(objActivityRecovery.LoadAcntgDropDowns(cmbDrAccts,cmbCrAccts,hidACTION_ON_PAYMENT.Value)==-1)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("931");
				lblMessage.Visible = true;
			}
		}		

		private void SetClaimActivityTop()
		{
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
			{
				cltClaimActivityTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
			}			
			if(hidACTIVITY_ID.Value!="" && hidACTIVITY_ID.Value!="0")
			{
				cltClaimActivityTop.ActivityID = int.Parse(hidACTIVITY_ID.Value);
			}			
			//cltClaimActivityTop.ActivityID = 1;
			//cltClaimActivityTop.ShowHeaderBand ="Claim";
			cltClaimActivityTop.Visible = true;        
		}

		private void LoadDropDown()
		{
			cmbPOLICY_LIMITS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("UMBRL",null,"S");
			cmbPOLICY_LIMITS.DataTextField="LookupDesc";
			cmbPOLICY_LIMITS.DataValueField ="LookupCode";
			cmbPOLICY_LIMITS.DataBind();			
			cmbPOLICY_LIMITS.Items.Insert(0,"");
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
			}
		}

		private void SetErrorMessages()
		{
			rngRECOVERY.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rngEXPENSES.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");			
			rngRETENTION_LIMITS.ErrorMessage		  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");						

			//revRECOVERY.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			revRECOVERY.ValidationExpression = aRegExpDoublePositiveZero;
			revRECOVERY.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			//revEXPENSES.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385;
			revEXPENSES.ValidationExpression = aRegExpDoublePositiveZero;
			revEXPENSES.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			//revRETENTION_LIMITS.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			revRETENTION_LIMITS.ValidationExpression = aRegExpCurrencyformat;
			revRETENTION_LIMITS.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			
		}

		private void GetOldData()
		{
			try
			{
				DataSet dsOldData;
				DataSet dsAccts = ClsReserveDetails.GetReserveAccounts(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,hidTRANSACTION_ID.Value);
				if(dsAccts!=null && dsAccts.Tables.Count>0)
				{
					if(dsAccts.Tables[0].Rows[0]["CRACCTS"]!=null && dsAccts.Tables[0].Rows[0]["CRACCTS"].ToString()!="")
						cmbCrAccts.SelectedValue = dsAccts.Tables[0].Rows[0]["CRACCTS"].ToString();
					if(dsAccts.Tables[0].Rows[0]["DRACCTS"]!=null && dsAccts.Tables[0].Rows[0]["DRACCTS"].ToString()!="")
						cmbDrAccts.SelectedValue = dsAccts.Tables[0].Rows[0]["DRACCTS"].ToString();
				}

				ClsReserveDetails objReserveDetails = ClsReserveDetails.CreateReserveObject(hidLOB_ID.Value);

				ClsActivity objActivity = new ClsActivity();
				//Fetching Old Claim Reserve records for Dummy Policy
				if((hidCustomerID.Value=="" || hidCustomerID.Value=="0") && (hidPolicyID.Value=="" || hidPolicyID.Value=="0") && (hidPolicyVersionID.Value=="" || hidPolicyVersionID.Value=="0"))
				{
					DataSet dsCoverage = ClsActivity.GetClaimCoverages(hidCLAIM_ID.Value);
					if(dsCoverage==null || dsCoverage.Tables[0].Rows.Count<1)
					{
						lblMessage.Text = ClsMessages.FetchGeneralMessage("1012");
						lblMessage.Visible = true;
						btnSave.Visible=false;
						trBody.Attributes.Add("style","display:none");
						return;
					}
					dsOldData = ClsReserveDetails.GetOldDataForDummyPolicyPage(hidCLAIM_ID.Value);
					if(dsOldData!=null && dsOldData.Tables[0].Rows.Count>0)
					{
						if(dsOldData.Tables[0].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"]!=null && dsOldData.Tables[0].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"].ToString()!="")
							hidACTION_ON_PAYMENT.Value = dsOldData.Tables[0].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"].ToString();


					}
				}
				else
				{
					dsOldData = objReserveDetails.GetOldData(hidCLAIM_ID.Value); //FETCHING LAST RESERVE INFO
					if(dsOldData!=null && dsOldData.Tables.Count>COVERAGE_TABLE)
					{
						if(dsOldData.Tables[COVERAGE_TABLE].Rows.Count>0)
						{
							//								if(dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_CRACCTS"]!=null && dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_CRACCTS"].ToString()!="")
							//									cmbCrAccts.SelectedValue = dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_CRACCTS"].ToString();
							//								if(dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_DRACCTS"]!=null && dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_DRACCTS"].ToString()!="")
							//									cmbDrAccts.SelectedValue = dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_DRACCTS"].ToString();
							if(dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"]!=null && dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"].ToString()!="")
								hidACTION_ON_PAYMENT.Value = dsOldData.Tables[COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"].ToString();
						}
						if(dsOldData.Tables[COVERAGE_TABLE].Rows.Count>0)
						{
							if(dsOldData.Tables[COVERAGE_TABLE].Rows[0]["POLICY_LIMITS"]!=null && dsOldData.Tables[COVERAGE_TABLE].Rows[0]["POLICY_LIMITS"].ToString()!="" && dsOldData.Tables[COVERAGE_TABLE].Rows[0]["POLICY_LIMITS"].ToString()!="0")
								cmbPOLICY_LIMITS.SelectedValue = dsOldData.Tables[COVERAGE_TABLE].Rows[0]["POLICY_LIMITS"].ToString();
							if(dsOldData.Tables[COVERAGE_TABLE].Rows[0]["RETENTION_LIMITS"]!=null && dsOldData.Tables[COVERAGE_TABLE].Rows[0]["RETENTION_LIMITS"].ToString()!="")
								txtRETENTION_LIMITS.Text= Double.Parse(dsOldData.Tables[COVERAGE_TABLE].Rows[0]["RETENTION_LIMITS"].ToString()).ToString("N");
						}
					}
				}
					DataSet dsClaimActivity = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
					if(dsClaimActivity!=null && dsClaimActivity.Tables.Count>0 &&  dsClaimActivity.Tables[0].Rows.Count>0)
					{
						/*if(dsClaimActivity.Tables[0].Rows[0]["RECOVERY"]!=null && dsClaimActivity.Tables[0].Rows[0]["RECOVERY"].ToString()!="")
								txtRECOVERY.Text= Double.Parse(dsClaimActivity.Tables[0].Rows[0]["RECOVERY"].ToString()).ToString("N");
						*/
						if(dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_RECOVERY"]!=null && dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_RECOVERY"].ToString()!="" && dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_RECOVERY"].ToString()!="0")
							txtRECOVERY.Text= Double.Parse(dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_RECOVERY"].ToString()).ToString("N");
						/*if(dsClaimActivity.Tables[0].Rows[0]["EXPENSES"]!=null && dsClaimActivity.Tables[0].Rows[0]["EXPENSES"].ToString()!="")
								txtEXPENSES.Text= Double.Parse(dsClaimActivity.Tables[0].Rows[0]["EXPENSES"].ToString()).ToString("N");
					    */
						if(dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_EXPENSES"]!=null && dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_EXPENSES"].ToString()!="" && dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_EXPENSES"].ToString()!="0")
							txtEXPENSES.Text= Double.Parse(dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_EXPENSES"].ToString()).ToString("N");

						if(dsClaimActivity.Tables[1].Rows.Count>0 && dsClaimActivity.Tables[1].Rows[0]["CLAIM_RESERVE_AMOUNT"]!=null && dsClaimActivity.Tables[1].Rows[0]["CLAIM_RESERVE_AMOUNT"].ToString()!="")
							hidOldTotalOutstanding.Value = dsClaimActivity.Tables[1].Rows[0]["CLAIM_RESERVE_AMOUNT"].ToString();

						if(dsClaimActivity.Tables[1].Rows.Count>0 && dsClaimActivity.Tables[1].Rows[0]["CLAIM_RI_RESERVE"]!=null && dsClaimActivity.Tables[1].Rows[0]["CLAIM_RI_RESERVE"].ToString()!="")
							hidOldTotalRiReserve.Value = dsClaimActivity.Tables[1].Rows[0]["CLAIM_RI_RESERVE"].ToString();

					}
				
				BindGrid(dsOldData);
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally{}
				

		}



		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			capRECOVERY.Text			=		objResourceMgr.GetString("txtRECOVERY");						
			capEXPENSES.Text			=		objResourceMgr.GetString("txtEXPENSES");						
			capRETENTION_LIMITS.Text	=		objResourceMgr.GetString("txtRETENTION_LIMITS");			
			capPOLICY_LIMITS.Text		=		objResourceMgr.GetString("cmbPOLICY_LIMITS");			
		}
		#endregion

		
		
		private void BindGrid(DataSet dsData)
		{
			if(dsData.Tables.Count == 0)
				IS_RESERVE_ADDED = "0";
			else
				IS_RESERVE_ADDED = "1";

			//Dummy Policy
			if((hidCustomerID.Value=="" || hidCustomerID.Value=="0") && (hidPolicyID.Value=="" || hidPolicyID.Value=="0") && (hidPolicyVersionID.Value=="" || hidPolicyVersionID.Value=="0"))
			{
				if(dsData==null || dsData.Tables[0].Rows.Count==0)
				{
					//When the old data does not exist (ie first time record is being looked up, then we will fetch the
					//data from MNT_CLAIM_COVERAGE table..when the record has been saved once, then data from 
					//CLM_ACTIVITY_RESERVE will be used thereafter
					dsData = ClsReserveDetails.GetDummyPolicyCoveragesForReserve(hidCLAIM_ID.Value);
					if(dsData==null)
					{
						trTotalRow.Attributes.Add("style","display:none");
						trBody.Attributes.Add("style","display:none");
						btnSave.Visible = false;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("804");
						lblMessage.Visible = true;
						return;
					}
				}
				hidDummyPolicyCoverageRowCount.Value = dsData.Tables[0].Rows.Count.ToString();
				DataView dvCoverages = new DataView(dsData.Tables[0]);				
				this.dgCoverages.DataSource = dvCoverages;
				trVehicleRow.Attributes.Add("style","display:none");
				HideShowColumns(dgCoverages,MCCA_ATTACHMENT_TEXT,false);
				HideShowColumns(dgCoverages,MCCA_RESERVE_TEXT,false);
				dgCoverages.DataBind();
			}
			else
			{
				//When the old data does not exist (ie first time record is being looked up, then we will fetch the
				//data from policy coverage table..when the record has been saved once, then data from reserve will
				//be used thereafter
				if(dsData==null || dsData.Tables.Count<=COVERAGE_TABLE || dsData.Tables[COVERAGE_TABLE]==null || dsData.Tables[COVERAGE_TABLE].Rows.Count<1)
				{							
					ClsReserveDetails  objReserveDetails = ClsReserveDetails.CreateReserveObject(hidLOB_ID.Value);				
					dsData = objReserveDetails.GetPolicyCoveragesForReserve(hidCLAIM_ID.Value);
					if(dsData==null) 
						return;				
				
				}
			
				if(dsData!=null && dsData.Tables.Count>COVERAGE_TABLE && dsData.Tables[COVERAGE_TABLE]!=null && dsData.Tables[COVERAGE_TABLE].Rows.Count>0)
				{
					dgCoverages.DataSource = dsData.Tables[COVERAGE_TABLE];
					dgCoverages.DataBind();
					hidVehicleRowCount.Value = dsData.Tables[COVERAGE_TABLE].Rows.Count.ToString();
				}
				else
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("804");
					lblMessage.Visible = true;
					trVehicleGridRow.Attributes.Add("style","display:none");
					trVehicleRow.Attributes.Add("style","display:none");
					trTotalRow.Attributes.Add("style","display:none");
					trBody.Attributes.Add("style","display:none");
					btnSave.Attributes.Add("style","display:none");
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
			this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")			
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();				
			else
				hidCLAIM_ID.Value = GetClaimID();

			if(Request["ACTIVITY_ID"]!=null && Request["ACTIVITY_ID"].ToString()!="")
				hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();
			else			
				hidACTIVITY_ID.Value = ClsReserveDetails.GetActivityID(hidCLAIM_ID.Value);

			//Added by Asfa (12-Oct-2007)
			if(hidTRANSACTION_ID.Value ==null || hidTRANSACTION_ID.Value=="0")
				hidTRANSACTION_ID.Value=ClsReserveDetails.GetTransactionID(hidCLAIM_ID.Value, hidACTIVITY_ID.Value);
			
			if(GetCustomerID()!=string.Empty)
				hidCustomerID.Value = GetCustomerID();
			else
				hidCustomerID.Value = "0";

			if(GetPolicyID()!=string.Empty)			
				hidPolicyID.Value = GetPolicyID();
			else
				hidPolicyID.Value = "0";
			if(GetPolicyVersionID()!=string.Empty)
				hidPolicyVersionID.Value = GetPolicyVersionID();
			else
				hidPolicyVersionID.Value = "0";

			if(GetLOBID()!=string.Empty)
				hidLOB_ID.Value = GetLOBID();
			else
				hidLOB_ID.Value = "0";		
			
			if(Request["ACTION_ON_PAYMENT"]!=null && Request["ACTION_ON_PAYMENT"].ToString()!="")
				hidACTION_ON_PAYMENT.Value = Request["ACTION_ON_PAYMENT"].ToString();
			else
				hidACTION_ON_PAYMENT.Value = ((int)enumClaimActionOnPayment.NEW_RESERVE).ToString();
			
			hidTRANSACTION_CATEGORY.Value= ClsReserveDetails.GetTransactionCategory(hidACTION_ON_PAYMENT.Value);
		}		

		private ClsActivityInfo GetFormActivityValues()
		{
			Double TotalOutstanding = 0,TotalRiReserve = 0;
			ClsActivityInfo objActivityInfo = new ClsActivityInfo();
			objActivityInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
			if(txtTOTAL_OUTSTANDING.Text.Trim()!="")
				TotalOutstanding = Convert.ToDouble(txtTOTAL_OUTSTANDING.Text.Trim());
						
			if(hidTRANSACTION_CATEGORY.Value=="General")
				objActivityInfo.RESERVE_AMOUNT = TotalOutstanding - double.Parse(hidOldTotalOutstanding.Value);
			else
				objActivityInfo.RESERVE_AMOUNT = 0;

			if(txtRECOVERY.Text.Trim()!="")
				objActivityInfo.RECOVERY = Convert.ToDouble(txtRECOVERY.Text.Trim());
			if(txtEXPENSES.Text.Trim()!="")
				objActivityInfo.EXPENSES = Convert.ToDouble(txtEXPENSES.Text.Trim());
			if(txtTOTAL_RI_RESERVE.Text.Trim()!="")
				TotalRiReserve = Convert.ToDouble(txtTOTAL_RI_RESERVE.Text.Trim());			
						
			if(hidTRANSACTION_CATEGORY.Value=="Reinsurance")
				objActivityInfo.RI_RESERVE = TotalRiReserve - double.Parse(hidOldTotalRiReserve.Value);
			else
				objActivityInfo.RI_RESERVE = 0;
			
			objActivityInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
			objActivityInfo.CREATED_BY =  objActivityInfo.MODIFIED_BY = int.Parse(GetUserId());
			objActivityInfo.LAST_UPDATED_DATETIME =  objActivityInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;			
			
			//Added by Asfa (18-Jan-2008) 
			objActivityInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
			objActivityInfo.POLICY_ID = int.Parse(hidPolicyID.Value);
			objActivityInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionID.Value);
			objActivityInfo.LOB_ID = int.Parse(hidLOB_ID.Value);

			return objActivityInfo;
		}
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				//int intTempNewReserveStatus=0;
				ClsReserveDetails objReserveDetails = new ClsReserveDetails();
				ClsReserveDetailsInfo objReserveDetailsInfo = new ClsReserveDetailsInfo();

				DataSet ds;
				//Done for Itrack Issue 6835 on 16 Dec 09
				if(hidACTIVITY_ID.Value !="1")
				{
					if((hidCustomerID.Value=="" || hidCustomerID.Value=="0") && (hidPolicyID.Value=="" || hidPolicyID.Value=="0") && (hidPolicyVersionID.Value=="" || hidPolicyVersionID.Value=="0"))
						ds= ClsReserveDetails.GetOldDataForDummyPolicyCurrentActivityReserve(hidCLAIM_ID.Value, hidACTIVITY_ID.Value, hidTRANSACTION_ID.Value);
					else
						ds= ClsReserveDetails.GetOldDataForCurrentActivityReserve(hidCLAIM_ID.Value, hidACTIVITY_ID.Value, hidTRANSACTION_ID.Value);
				}
				else
				{
					if((hidCustomerID.Value=="" || hidCustomerID.Value=="0") && (hidPolicyID.Value=="" || hidPolicyID.Value=="0") && (hidPolicyVersionID.Value=="" || hidPolicyVersionID.Value=="0"))
						ds= ClsReserveDetails.GetOldDataForDummyPolicyCurrentActivityReserve(hidCLAIM_ID.Value, "0", hidTRANSACTION_ID.Value);
					else
						ds= ClsReserveDetails.GetOldDataForCurrentActivityReserve(hidCLAIM_ID.Value, "0", hidTRANSACTION_ID.Value);
				}
				
				if(ds != null && ds.Tables.Count >0)
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
				else
					hidOldData.Value="";

				ArrayList aList = new ArrayList();
				//PopulateArray(aList,dgPolicyCoverages);
				PopulateArray(aList,dgCoverages);

				ClsClaimsNotficationInfo objClaimsNotficationInfo = new ClsClaimsNotficationInfo();				
				ClsActivityInfo objActivityInfo = new ClsActivityInfo();
				ClsActivity objActivity = new ClsActivity();	
				GetClaimDetails();
				objActivityInfo = GetFormActivityValues();
				if(hidNewReserve.Value=="" || hidNewReserve.Value=="0")
				{
					//AddNewReserveActivity(objActivityInfo);								
					objActivityInfo.ACTIVITY_REASON = (int)enumActivityReason.RESERVE_UPDATE;
					objActivityInfo.ACTIVITY_STATUS = (int)enumClaimActivityStatus.COMPLETE;
					objActivityInfo.ACTION_ON_PAYMENT = (int)enumClaimActionOnPayment.NEW_RESERVE;
					// Added by Rajan on 16 Apr 2008, it was not posting in case of new reserve. 
					objActivityInfo.GL_POSTING_REQUIRED = "Y"; 
				}
				objActivityInfo.GL_POSTING_REQUIRED = "Y"; 
				
				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					intRetVal = objActivity.AddReserveDetails(objActivityInfo,aList,int.Parse(hidNewReserve.Value));
					//intRetVal = objReserveDetails.Add(aList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");						
				}
				else
				{
					intRetVal = objReserveDetails.Update(aList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"31");					
				}
					
				if(intRetVal>0)
				{
					SetStartUpScript("GoToActivity");
					if(hidNewReserve.Value=="" || hidNewReserve.Value=="0")
					{
						objActivityInfo.ACTIVITY_REASON = (int)enumActivityReason.RESERVE_UPDATE;
						objActivityInfo.ACTIVITY_STATUS = (int)enumClaimActivityStatus.COMPLETE;
						objActivityInfo.ACTION_ON_PAYMENT = (int)enumClaimActionOnPayment.NEW_RESERVE;
					}
					objActivity.UpdateActivityReserve(objActivityInfo);									
					//GetOldData();					
				}
				else if(intRetVal == -1)
				{
					lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
					
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
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				
			}
		}		
		private void SetStartUpScript(string FunctionName)
		{
			ClientScript.RegisterStartupScript(this.GetType(),"ReLoadClaimsTab","<script>" + FunctionName + "();</script>");			
		}
		private int AddNewReserveActivity()
		{
			ClsActivity  objActivity = new ClsActivity();
			ClsActivityInfo objActivityInfo = new ClsActivityInfo();
			try
			{
				objActivityInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
				objActivityInfo.CREATED_BY = int.Parse(GetUserId());				
				objActivityInfo.ACTIVITY_REASON = (int)enumActivityReason.RESERVE_UPDATE;
				objActivityInfo.ACTIVITY_STATUS = (int)enumClaimActivityStatus.COMPLETE;
				objActivityInfo.ACTION_ON_PAYMENT = (int)enumClaimActionOnPayment.NEW_RESERVE;

				int intRetVal	= objActivity.Add(objActivityInfo);
				if( intRetVal > 0 )			// update successfully performed
				{
					//hidNewReserve.Value = "1";					
					hidACTIVITY_ID.Value = objActivityInfo.ACTIVITY_ID.ToString();
					return 1;
				}					
				else 
				{
					lblMessage.Text		=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");					
					lblMessage.Visible = true;							
				}				
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(objActivity!=null)
					objActivity.Dispose();
				if(objActivityInfo!=null)
					objActivityInfo = null;
			}
			return 0;
		}
		private void PopulateArray(ArrayList aList, DataGrid dtGrid)
		{
			foreach(DataGridItem dgi in dtGrid.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					
					
					//Model Object
					ClsReserveDetailsInfo objReserveDetailsInfo = new ClsReserveDetailsInfo();
					//Get the Outstanding					
					TextBox txtOutstanding = (TextBox)(dgi.FindControl("txtOUTSTANDING"));
					TextBox txtRI_RESERVE = (TextBox)(dgi.FindControl("txtRI_RESERVE"));
					DropDownList cmbREINSURANCE_CARRIER = (DropDownList)(dgi.FindControl("cmbREINSURANCE_CARRIER"));					
					Label lblCOV_ID = (Label)(dgi.FindControl("lblCOV_ID"));
					Label lblBOAT_ID = (Label)(dgi.FindControl("lblBOAT_ID"));
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));				

					
					if(txtRI_RESERVE.Text.Trim()!="" && hidTRANSACTION_CATEGORY.Value=="Reinsurance")
						objReserveDetailsInfo.RI_RESERVE = Convert.ToDouble(txtRI_RESERVE.Text.Trim());

					if(cmbREINSURANCE_CARRIER.SelectedItem!=null && cmbREINSURANCE_CARRIER.SelectedItem.Value!="")
						objReserveDetailsInfo.REINSURANCE_CARRIER = int.Parse(cmbREINSURANCE_CARRIER.SelectedItem.Value);

					if(txtOutstanding.Text!="" && hidTRANSACTION_CATEGORY.Value=="General")
						objReserveDetailsInfo.OUTSTANDING = Convert.ToDouble(txtOutstanding.Text.Trim());

					if(lblCOV_ID.Text!="")
						objReserveDetailsInfo.COVERAGE_ID = int.Parse(lblCOV_ID.Text);
					if(lblBOAT_ID!=null && lblBOAT_ID.Text!="")
						objReserveDetailsInfo.VEHICLE_ID = int.Parse(lblBOAT_ID.Text);
					objReserveDetailsInfo.IS_ACTIVE="Y";
					objReserveDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objReserveDetailsInfo.CREATED_DATETIME = System.DateTime.Now;
					objReserveDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReserveDetailsInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
					objReserveDetailsInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					//Done for Itrack Issue 6835 on 16 Dec 09
					if(hidACTIVITY_ID.Value !="1")
						objReserveDetailsInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
					else
						objReserveDetailsInfo.ACTIVITY_ID = 0;						

					if(lblRESERVE_ID.Text.Trim()!="")
						objReserveDetailsInfo.RESERVE_ID = int.Parse(lblRESERVE_ID.Text.Trim());

					if(cmbPOLICY_LIMITS.SelectedItem!=null && cmbPOLICY_LIMITS.SelectedItem.Value!="")
						objReserveDetailsInfo.POLICY_LIMITS = Convert.ToDouble(cmbPOLICY_LIMITS.SelectedItem.Value);
					if(txtRETENTION_LIMITS.Text.Trim()!="")
						objReserveDetailsInfo.RETENTION_LIMITS = Convert.ToDouble(txtRETENTION_LIMITS.Text.Trim());

					if(cmbDrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
						objReserveDetailsInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedValue);
					if(hidACTION_ON_PAYMENT.Value!="")
						objReserveDetailsInfo.ACTION_ON_PAYMENT = int.Parse(hidACTION_ON_PAYMENT.Value);

					if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
						objReserveDetailsInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedValue);					

					//Added by Asfa - (12-Oct-2007)
					objReserveDetailsInfo.TRANSACTION_ID = int.Parse(hidTRANSACTION_ID.Value);
					
					aList.Add(objReserveDetailsInfo);
				}
			}
		}

		private void SetClaimTop()
		{
			if(hidCustomerID.Value!="" && hidCustomerID.Value!="0")
			{
				cltClaimTop.CustomerID = int.Parse(hidCustomerID.Value);
			}			

			if(hidPolicyID.Value!="" && hidPolicyID.Value!="0")
			{
				cltClaimTop.PolicyID = int.Parse(hidPolicyID.Value);
			}

			if(hidPolicyVersionID.Value!="" && hidPolicyVersionID.Value!="0")
			{
				cltClaimTop.PolicyVersionID = int.Parse(hidPolicyVersionID.Value);
			}
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
			{
				cltClaimTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
			}
			if(hidLOB_ID.Value!="" && hidLOB_ID.Value!="0")
			{
				cltClaimTop.LobID = int.Parse(hidLOB_ID.Value);
			}
        
			cltClaimTop.ShowHeaderBand ="Claim";

			cltClaimTop.Visible = true;        
		}


		private void dgCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{	
				
				((RangeValidator)(e.Item.FindControl("rngOUTSTANDING"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				
				((RangeValidator)(e.Item.FindControl("rngRI_RESERVE"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				

				((RegularExpressionValidator)(e.Item.FindControl("revOUTSTANDING"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 10 Feb 09 for Itrack Issue 5385-TO MOVE TO LOCAL VSS	
				((RegularExpressionValidator)(e.Item.FindControl("revOUTSTANDING"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 10 Feb 09 for Itrack Issue 5385

				((RegularExpressionValidator)(e.Item.FindControl("revRI_RESERVE"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 10 Feb 09 for Itrack Issue 5385	
				((RegularExpressionValidator)(e.Item.FindControl("revRI_RESERVE"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 10 Feb 09 for Itrack Issue 5385
				

				DropDownList cmbREINSURANCE_CARRIER = (DropDownList)(e.Item.FindControl("cmbREINSURANCE_CARRIER"));
				
				/*if(iLookUp==null || iLookUp.Count<0)
				{
					iLookUp = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLRC",null,"S");
				}				
				cmbREINSURANCE_CARRIER.DataSource = iLookUp;
				cmbREINSURANCE_CARRIER.DataTextField="LookupDesc";
				cmbREINSURANCE_CARRIER.DataValueField="LookupID";
				cmbREINSURANCE_CARRIER.DataBind();*/

				//Added by Shikha Dixit
				DataTable objDataTable=new DataTable();
				objDataTable= Cms.BusinessLayer.BlCommon.ClsCommon.GetReinsuranceCompanyNames();
				cmbREINSURANCE_CARRIER.DataSource = objDataTable;
				cmbREINSURANCE_CARRIER.DataTextField="REIN_COMAPANY_NAME";//"LookupDesc";
				cmbREINSURANCE_CARRIER.DataValueField="REIN_COMAPANY_ID";//"LookupID";
				cmbREINSURANCE_CARRIER.DataBind();
				//End of addition.

				if ( DataBinder.Eval(e.Item.DataItem,"REINSURANCE_CARRIER") != System.DBNull.Value) 
				{
					ClsCommon.SelectValueInDDL(cmbREINSURANCE_CARRIER,DataBinder.Eval(e.Item.DataItem,"REINSURANCE_CARRIER"));
							
				}				
				
				
				TextBox txtRI_RESERVE = (TextBox)(e.Item.FindControl("txtRI_RESERVE"));		
				if (txtRI_RESERVE!=null)
				{
					txtRI_RESERVE.Attributes.Add("onBlur","javascript:CalculateTotalRIReserve();");					
					if(DataBinder.Eval(e.Item.DataItem,"RI_RESERVE") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"RI_RESERVE"))!="0")
						txtRI_RESERVE.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"RI_RESERVE"));
					else
						txtRI_RESERVE.Text = "";
				}	

				TextBox txtOUTSTANDING = ((TextBox)(e.Item.FindControl("txtOUTSTANDING")));
				if(txtOUTSTANDING!=null)
				{
					txtOUTSTANDING.Attributes.Add("onBlur","javascript:CalculateTotalRIReserve();");				
					if(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING"))!="0")
						txtOUTSTANDING.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING"));															
					else
						txtOUTSTANDING.Text = "";
				}

			}
			
			
		}
	
		
		private void SetNewReserveFlag()
		{
			int retVal = 0;
			retVal =  int.Parse(ClsClaimsNotification.CheckForReservesAdded(hidCLAIM_ID.Value));
			if (retVal == 1)
				hidNewReserve.Value = "1";
			else
				hidNewReserve.Value = "0";
		}

//		private void SetSaveVisibility()
//		{
//			int retVal = 0;
//			retVal =  int.Parse(ClsClaimsNotification.CheckForReservesAdded(hidCLAIM_ID.Value));
//			if (retVal == 1)
//			{
//				hidNewReserve.Value = "1";
//				btnSave.Visible = false;
//			}
//			else
//			{
//				hidNewReserve.Value = "0";
//				btnSave.Visible = true;	
//			}
//
//			if (Request["RESERVE_UPDATE"] != null && Request["RESERVE_UPDATE"].ToString().Trim() == "1")			
//				btnSave.Visible=true;			
//		}

		private void SetSaveVisibility()
		{
			DataSet ds = ClsReserveDetails.CheckClaimStatus(hidCLAIM_ID.Value, hidACTIVITY_ID.Value);
			if ((ds.Tables[0].Rows.Count >0 && ds.Tables[0].Rows[0]["ACTIVITY_STATUS"].ToString() == "11801") || ds.Tables[1].Rows[0]["CLAIM_STATUS"].ToString() == CLAIM_STATUS_CLOSED ) // COMPLETE
				btnSave.Visible = false;
			else
				btnSave.Visible = true;	
		}
	}
}
