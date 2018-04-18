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
	public class ReserveDetails : Cms.Claims.ClaimBase
	{
		protected Cms.CmsWeb.WebControls.ClaimActivityTop cltClaimActivityTop;
		protected string ActivityClientID,ActivityTotalPaymentClientID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldTotalRiReserve;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldTotalOutstanding;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_PAYMENT;
		protected System.Web.UI.WebControls.DropDownList cmbDrAccts;
		protected System.Web.UI.WebControls.DropDownList cmbCrAccts;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCrAccts;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.TextBox txtGrossTotal;
		protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;			
		protected System.Web.UI.WebControls.DataGrid dgCoverages;				
		protected System.Web.UI.WebControls.DataGrid dgPolicyCoverages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleRowCount;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNewReserve;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;						
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyRowCount;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDummyPolicyCoverageRowCount;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;				
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRANSACTION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRANSACTION_CATEGORY;
		protected System.Web.UI.WebControls.Label lblTitle;	
		System.Resources.ResourceManager objResourceMgr;	
		
		
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;		
		protected System.Web.UI.WebControls.Label capACTIVITY_DATE;
		protected System.Web.UI.WebControls.Label lblACTIVITY_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected Cms.CmsWeb.Controls.CmsButton btnReserveBreakdown;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.Label capMessage;
		//protected System.Web.UI.WebControls.Label capTOTAL_DEDUCTIBLE;
		//protected System.Web.UI.WebControls.TextBox txtTOTAL_DEDUCTIBLE;
		//protected System.Web.UI.WebControls.Label capTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_OUTSTANDING;
		//protected System.Web.UI.WebControls.Label capTOTAL_RI_RESERVE;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_RI_RESERVE;
		protected System.Web.UI.WebControls.Label capRECOVERY;		
		//protected System.Web.UI.WebControls.Label capPAYMENT_AMOUNT;		
		protected System.Web.UI.WebControls.Label capEXPENSES;
		protected System.Web.UI.WebControls.TextBox txtEXPENSES;
		
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		//protected double TotalDeductible=0;
		protected System.Web.UI.WebControls.TextBox txtRECOVERY;
		protected System.Web.UI.WebControls.RangeValidator rngRECOVERY;
		protected System.Web.UI.WebControls.RangeValidator rngEXPENSES;		
		protected System.Web.UI.WebControls.RegularExpressionValidator revRECOVERY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPENSES;
		//protected System.Web.UI.WebControls.TextBox txtPAYMENT_AMOUNT;
		//protected System.Web.UI.WebControls.RangeValidator rngPAYMENT_AMOUNT;
		protected System.Web.UI.WebControls.Label lblPolicyCaption;
		protected System.Web.UI.WebControls.Label Label8;
        protected System.Web.UI.WebControls.Label lblaccdb;
        protected System.Web.UI.WebControls.Label lblacccr;
		//IList iLookUp;		
		const string SubHeading = "SubHeading";
		const string VehicleText = "Vehicle # : ";
		public static string VehicleCoverageGridID = "";
		public static string PolicyCoverageGridID = "";
		const int POLICY_COVERAGE_TABLE = 0;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPolicyrow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPolicyGridRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehicleRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehicleGridRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTotalRow;		
		const int VEHICLE_COVERAGE_TABLE = 1;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_MCCA_APPLIES;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdFirst;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdSecond;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdThird;
		public int mccaAttachmentPoint = 0;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			string strRedirectPage = CheckLOBAndRedirect();
			if(strRedirectPage!=string.Empty && strRedirectPage!="")
				Server.Transfer(strRedirectPage,true);
			base.ScreenId="306_0_1";	
	
			
			btnReserveBreakdown.CmsButtonClass		=	CmsButtonType.Read;
			btnReserveBreakdown.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.ReserveDetails"  ,System.Reflection.Assembly.GetExecutingAssembly());
			GetQueryStringValues();
			SetClaimTop();			
			// Put user code to initialize the page here			
			if ( !Page.IsPostBack)
			{
				VehicleCoverageGridID = dgCoverages.ID;
				PolicyCoverageGridID = dgPolicyCoverages.ID;
				SetSaveVisibility();
				SetNewReserveFlag();
				LoadAcntgDropDowns();
				SetCaptions();
				SetErrorMessages();	
				btnBack.Attributes.Add("onClick","javascript: return GoToClaimDetailPage();");
				btnSave.Attributes.Add("onClick","javascript: return CompareAllLimitAndOutstanding();");
				GetClaimDetails();
				GetPredefineAttachmentPoint();
				//btnReserveBreakdown.Attributes.Add("onClick","javascript: return OpenWindow();");
				txtRECOVERY.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				txtEXPENSES.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				//txtPAYMENT_AMOUNT.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");				
				//Check whether the current record being viewed belongs to a dummy policy
				
				/* ***** Comment Block Starts ***** 
				Commented by Asfa(06-Dec-2007) - iTrack issue #3141
				//Display an appropriate message for dummy policy
//				if((hidCustomerID.Value=="" || hidCustomerID.Value=="0") && (hidPolicyID.Value=="" || hidPolicyID.Value=="0") && (hidPolicyVersionID.Value=="" || hidPolicyVersionID.Value=="0"))
//				{
//					lblMessage.Text = ClsMessages.FetchGeneralMessage("805");
//					lblMessage.Visible = true;
//					trBody.Attributes.Add("style","display:none");
//					return;
//				}
				/* ***** Comment Block Ends ***** 
				
				//Let us remove the check..let it be working as it is..
				/*if(hidACTIVITY_ID.Value=="" || hidACTIVITY_ID.Value=="0")
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("803") + "<br><a href='javascript:GoBack();'>Click here</a> to add an Activity</a>";
					lblMessage.Visible = true;
					trBody.Attributes.Add("style","display:none");
					return;
				}*/
				GetActivityData();
				GetOldData();	
			}
			ActivityClientID = cltClaimActivityTop.PanelClientID;			
			ActivityTotalPaymentClientID = cltClaimActivityTop.PanelPaymentClientID;
			SetClaimActivityTop();
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

		private void LoadAcntgDropDowns()
		{
			ClsActivityRecovery objActivityRecovery = new ClsActivityRecovery();
			if(objActivityRecovery.LoadAcntgDropDowns(cmbDrAccts,cmbCrAccts,hidACTION_ON_PAYMENT.Value)==-1)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("931");
				lblMessage.Visible = true;
			}
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
				hidSTATE_ID.Value = dr["STATE_ID"].ToString();
			}
		}

		private void SetErrorMessages()
		{
			rngRECOVERY.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rngEXPENSES.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			//rngPAYMENT_AMOUNT.ErrorMessage			  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
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
					//Done for Itrack Issue 5548 on 28 May 09
					if(hidACTIVITY_ID.Value!="" && dsAccts!=null && dsAccts.Tables.Count>0)//Added condition for Itrack Issue 5548 on 2 June 2009
					{
						if(hidACTIVITY_ID.Value == "1")
						{
							dsOldData = ClsReserveDetails.GetOldDataForPage(hidCLAIM_ID.Value,0);
						}
						else if(hidACTIVITY_ID.Value == "0")
							dsOldData = ClsReserveDetails.GetOldDataForPage(hidCLAIM_ID.Value);	//FETCHING LAST RESERVE INFO
						else
							dsOldData = ClsReserveDetails.GetOldDataForPage(hidCLAIM_ID.Value,int.Parse(hidACTIVITY_ID.Value));	//FETCHING CURRENT RESERVE INFO
					}
					else
						dsOldData = ClsReserveDetails.GetOldDataForPage(hidCLAIM_ID.Value);	//FETCHING LAST RESERVE INFO
					if(dsOldData!=null && dsOldData.Tables.Count>POLICY_COVERAGE_TABLE)
					{
						if(dsOldData.Tables[POLICY_COVERAGE_TABLE].Rows.Count>0)
						{
							if(dsOldData.Tables[POLICY_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"]!=null && dsOldData.Tables[POLICY_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"].ToString()!="")
								hidACTION_ON_PAYMENT.Value = dsOldData.Tables[POLICY_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"].ToString();
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

					//txtRECOVERY.Text=String.Format("{0:,#,###.##}",dsClaimActivity.Tables[0].Rows[0]["RECOVERY"]);
					//txtRECOVERY.Text=String.Format("{0:,#,###}",Convert.ToInt64(dsClaimActivity.Tables[0].Rows[0]["RECOVERY"]));
					//if(dsClaimActivity.Tables[0].Rows[0]["PAYMENT_AMOUNT"]!=null && dsClaimActivity.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString()!="")
					//	txtPAYMENT_AMOUNT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dsClaimActivity.Tables[0].Rows[0]["PAYMENT_AMOUNT"]));					
					/*if(dsClaimActivity.Tables[0].Rows[0]["EXPENSES"]!=null && dsClaimActivity.Tables[0].Rows[0]["EXPENSES"].ToString()!="")
								txtEXPENSES.Text= Double.Parse(dsClaimActivity.Tables[0].Rows[0]["EXPENSES"].ToString()).ToString("N");
							  */
					if(dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_EXPENSES"]!=null && dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_EXPENSES"].ToString()!="" && dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_EXPENSES"].ToString()!="0")
						txtEXPENSES.Text= Double.Parse(dsClaimActivity.Tables[2].Rows[0]["ESTIMATION_EXPENSES"].ToString()).ToString("N");

					if(dsClaimActivity.Tables[1].Rows.Count>0 && dsClaimActivity.Tables[1].Rows[0]["CLAIM_RESERVE_AMOUNT"]!=null && dsClaimActivity.Tables[1].Rows[0]["CLAIM_RESERVE_AMOUNT"].ToString()!="")
						hidOldTotalOutstanding.Value = dsClaimActivity.Tables[1].Rows[0]["CLAIM_RESERVE_AMOUNT"].ToString();

					if(dsClaimActivity.Tables[1].Rows.Count>0 && dsClaimActivity.Tables[1].Rows[0]["CLAIM_RI_RESERVE"]!=null && dsClaimActivity.Tables[1].Rows[0]["CLAIM_RI_RESERVE"].ToString()!="")
						hidOldTotalRiReserve.Value = dsClaimActivity.Tables[1].Rows[0]["CLAIM_RI_RESERVE"].ToString();


					//txtEXPENSES.Text=String.Format("{0:,#,###.##}",dsClaimActivity.Tables[0].Rows[0]["EXPENSES"]);
						
				}
				BindGrid(dsOldData);
				
				
				/*if(hidPolicyRowCount.Value=="" || hidPolicyRowCount.Value=="0")
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("804");
					lblMessage.Visible = true;
					trBody.Attributes.Add("style","display:none");
					return;
				}*/
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally{}
				

		}

		public void GetActivityData()
		{
			if (hidCLAIM_ID.Value != "" && hidACTIVITY_ID.Value != "")
			{
				ClsActivity objActivity = new ClsActivity();
				DataSet dsTemp = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
				if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
				{
					DataRow dr = dsTemp.Tables[0].Rows[0];					
					lblACTIVITY_DATE.Text = dr["ACTIVITY_DATE"].ToString();					

				}
			}
		}

		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
             capMessage.Text = objResourceMgr.GetString("capMessage");
             lblaccdb.Text = objResourceMgr.GetString("lblaccdb");
             lblacccr.Text = objResourceMgr.GetString("lblacccr");
             lblTitle.Text              =       objResourceMgr.GetString("lblTitle");
             lblPolicyCaption.Text      =       objResourceMgr.GetString("lblPolicyCaption");
             Label8.Text                =       objResourceMgr.GetString("Label8");
             txtGrossTotal.Text         =       objResourceMgr.GetString("txtGrossTotal");
			 capACTIVITY_DATE.Text 		=		objResourceMgr.GetString("lblACTIVITY_DATE");			
			//capTOTAL_DEDUCTIBLE.Text	=		objResourceMgr.GetString("txtTOTAL_DEDUCTIBLE");			
			//capTOTAL_RI_RESERVE.Text	=		objResourceMgr.GetString("txtTOTAL_RI_RESERVE");			
			//capTOTAL_OUTSTANDING.Text	=		objResourceMgr.GetString("txtTOTAL_OUTSTANDING");			
			capRECOVERY.Text			=		objResourceMgr.GetString("txtRECOVERY");			
			//capPAYMENT_AMOUNT.Text	=		objResourceMgr.GetString("txtPAYMENT_AMOUNT");			
			capEXPENSES.Text			=		objResourceMgr.GetString("txtEXPENSES");			
		}
		#endregion

		
		
		private void BindGrid(DataSet dsData)
		{
			//if(dsData.Tables[0].Rows.Count == 0)//Done for Itrack issue 5548
			//Check for reserves in both Policy and Vehicle Level Coverages
			if(dsData.Tables[0].Rows.Count == 0 && dsData.Tables[1].Rows.Count == 0)
				IS_RESERVE_ADDED = "0";
			else
				IS_RESERVE_ADDED = "1";

			int position=0;

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
						trPolicyrow.Attributes.Add("style","display:none");
						trPolicyGridRow.Attributes.Add("style","display:none");
						dgPolicyCoverages.Visible = false;
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
				trPolicyrow.Attributes.Add("style","display:none");
				trVehicleRow.Attributes.Add("style","display:none");
				string strFirstWidth,strSecondWidth,strThirdWidth;
				HideShowColumns(dgCoverages,MCCA_ATTACHMENT_TEXT,false);
				HideShowColumns(dgPolicyCoverages,MCCA_ATTACHMENT_TEXT,false);
				HideShowColumns(dgCoverages,MCCA_RESERVE_TEXT,false);
				HideShowColumns(dgPolicyCoverages,MCCA_RESERVE_TEXT,false);					
				strFirstWidth = "45%";
				strSecondWidth = "23%";
				strThirdWidth = "37%";
				
				tdFirst.Attributes.Add("width",strFirstWidth);
				tdSecond.Attributes.Add("width",strSecondWidth);
				tdThird.Attributes.Add("width",strThirdWidth);
				dgCoverages.DataBind();
			}
			else
			{
				//When the old data does not exist (ie first time record is being looked up, then we will fetch the
				//data from policy coverage table..when the record has been saved once, then data from reserve will
				//be used thereafter
				if(dsData==null || dsData.Tables.Count<VEHICLE_COVERAGE_TABLE || (dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count<1 && dsData.Tables[POLICY_COVERAGE_TABLE].Rows.Count<1))
				{							
					ClsReserveDetails  objReserveDetails = ClsReserveDetails.CreateReserveObject(hidLOB_ID.Value);				
					dsData = objReserveDetails.GetPolicyCoveragesForReserve(hidCLAIM_ID.Value);
					if(dsData==null) 
						return;
					//				if(hidLOB_ID.Value == ((int)enumLOB.AUTOP).ToString()  || hidLOB_ID.Value == ((int)enumLOB.CYCL).ToString() || hidLOB_ID.Value == ((int)enumLOB.BOAT).ToString())
					//					dsData =  objReserveDetails.GetAutoMotorBoatPolicyCoveragesForReserve(hidCLAIM_ID.Value).Tables[0];
					//				else if (hidLOB_ID.Value == ((int)enumLOB.HOME).ToString() || hidLOB_ID.Value == ((int)enumLOB.REDW).ToString() )
					//					dsData = objReserveDetails.GetHomeRentalPolicyCoveragesForReserve(hidCLAIM_ID.Value).Tables[0];
					//				else if (hidLOB_ID.Value == ((int)enumLOB.UMB).ToString())
					//					dsData = objReserveDetails.GetUmbPolicyCoveragesForReserve(hidCLAIM_ID.Value).Tables[0];
				
				}			
				DataView dvPolicyCoverages=new DataView();
				DataView dvCoverages=new DataView();
				if(dsData.Tables[POLICY_COVERAGE_TABLE].Rows.Count>0)
				{
					//if(hidOldData.Value=="" || hidOldData.Value=="0")
					if(IS_RESERVE_ADDED == "0")
					{

						//DataTable dt = dsData.Tables[POLICY_COVERAGE_TABLE];
						//DataRow[] dr = dt.Select("COV_ID='" + PIP_COV_ID + "'");
						DataRow drow=null;
						//if(dr!=null && dr.Length>0)
						//{
						position = 0;
						/*DataView view = new DataView(dt,null,"RESERVE_ID",DataViewRowState.CurrentRows);
							DataRowView drv = view[view.Find(dr[0]["RESERVE_ID"])];
							position = (((IList)view).IndexOf(drv)); */
						position = GetRowPosition(dsData.Tables[POLICY_COVERAGE_TABLE],PIP_COV_ID);

						
						int ActualRiskID = Convert.ToInt32(dsData.Tables[POLICY_COVERAGE_TABLE].Rows[0]["ACTUAL_RISK_ID"]);
						string ActualRiskType= dsData.Tables[POLICY_COVERAGE_TABLE].Rows[0]["ACTUAL_RISK_TYPE"].ToString(); 


						if(position!=0)
						{

							//Adding Medical Coverage
							drow = dsData.Tables[POLICY_COVERAGE_TABLE].NewRow();
							drow["COV_DESC"] = "Medical";
							drow["COV_ID"]=MEDICAL_COV_ID;
							
							drow["ACTUAL_RISK_ID"] = ActualRiskID ; 
							drow["ACTUAL_RISK_TYPE"] = ActualRiskType;

							dsData.Tables[POLICY_COVERAGE_TABLE].Rows.InsertAt(drow,position++);
							drow=null;

							//Adding Work Loss Coverage
							drow = dsData.Tables[POLICY_COVERAGE_TABLE].NewRow();
							drow["COV_DESC"] = "Work Loss";
							drow["COV_ID"]=WORK_LOSS_COV_ID;

							drow["ACTUAL_RISK_ID"] = ActualRiskID ; 
							drow["ACTUAL_RISK_TYPE"] = ActualRiskType;

							dsData.Tables[POLICY_COVERAGE_TABLE].Rows.InsertAt(drow,position++);
							drow=null;

						
							drow = dsData.Tables[POLICY_COVERAGE_TABLE].NewRow();
							drow["COV_DESC"] = "Death Benefits";
							drow["COV_ID"]=DEATH_BENEFITS_COV_ID;

							drow["ACTUAL_RISK_ID"] = ActualRiskID ; 
							drow["ACTUAL_RISK_TYPE"] = ActualRiskType;

							dsData.Tables[POLICY_COVERAGE_TABLE].Rows.InsertAt(drow,position++);
							drow=null;

						
							drow = dsData.Tables[POLICY_COVERAGE_TABLE].NewRow();
							drow["COV_DESC"] = "Survivor's Benefits";
							drow["COV_ID"]=SURVIVOR_COV_ID;

							drow["ACTUAL_RISK_ID"] = ActualRiskID ; 
							drow["ACTUAL_RISK_TYPE"] = ActualRiskType;

							dsData.Tables[POLICY_COVERAGE_TABLE].Rows.InsertAt(drow,position++);
							drow=null;
						}
						//}					
						/*InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],SLL_COV_ID,int.Parse(SLL_BI_ID),int.Parse(SLL_PD_ID));
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],RLCSL_COV_ID,int.Parse(SLL_BI_ID),int.Parse(SLL_PD_ID));
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],UNINSURED_MOTORISTS_COV_ID_IN,int.Parse(UNINSURED_MOTORISTS_BI_ID),int.Parse(UNINSURED_MOTORISTS_PD_ID));
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],UNINSURED_MOTORISTS_COV_ID_MI,int.Parse(UNINSURED_MOTORISTS_BI_ID),int.Parse(UNINSURED_MOTORISTS_PD_ID));
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],UNDERINSURED_MOTORISTS_COV_ID_IN,int.Parse(UNDERINSURED_MOTORISTS_BI_ID),int.Parse(UNDERINSURED_MOTORISTS_PD_ID));
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],UNDERINSURED_MOTORISTS_COV_ID_MI,int.Parse(UNDERINSURED_MOTORISTS_BI_ID),int.Parse(UNDERINSURED_MOTORISTS_PD_ID));
						*/
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],SLL_COV_ID,int.Parse(SLL_BI_ID_IN),int.Parse(SLL_PD_ID_IN), ActualRiskID, ActualRiskType);
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],RLCSL_COV_ID,int.Parse(SLL_BI_ID_MI),int.Parse(SLL_PD_ID_MI), ActualRiskID, ActualRiskType);
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],UNINSURED_MOTORISTS_COV_ID_IN,int.Parse(UNINSURED_MOTORISTS_BI_ID_IN),int.Parse(UNINSURED_MOTORISTS_PD_ID_IN), ActualRiskID, ActualRiskType);
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],UNINSURED_MOTORISTS_COV_ID_MI,int.Parse(UNINSURED_MOTORISTS_BI_ID_MI),int.Parse(UNINSURED_MOTORISTS_PD_ID_MI), ActualRiskID, ActualRiskType);
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],UNDERINSURED_MOTORISTS_COV_ID_IN,int.Parse(UNDERINSURED_MOTORISTS_BI_ID_IN),int.Parse(UNDERINSURED_MOTORISTS_PD_ID_IN), ActualRiskID, ActualRiskType);
						InsertBD_PI_DataRows(dsData.Tables[POLICY_COVERAGE_TABLE],UNDERINSURED_MOTORISTS_COV_ID_MI,int.Parse(UNDERINSURED_MOTORISTS_BI_ID_MI),int.Parse(UNDERINSURED_MOTORISTS_PD_ID_MI), ActualRiskID, ActualRiskType);
					}
					hidPolicyRowCount.Value = dsData.Tables[POLICY_COVERAGE_TABLE].Rows.Count.ToString();
					dvPolicyCoverages = new DataView(dsData.Tables[POLICY_COVERAGE_TABLE]);				
					this.dgPolicyCoverages.DataSource = dvPolicyCoverages;
					dgPolicyCoverages.DataBind();
				}
				else
				{
					trPolicyrow.Attributes.Add("style","display:none");
					trPolicyGridRow.Attributes.Add("style","display:none");
					dgPolicyCoverages.Visible = false;
				}

				dvPolicyCoverages = new DataView(dsData.Tables[VEHICLE_COVERAGE_TABLE]);

				//Add new temporary rows for vehicle sub-heading
				if(dsData!=null && dsData.Tables.Count>VEHICLE_COVERAGE_TABLE && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count>0)
				{
					//ankit #itrack 7642
					ArrayList arrLstRisk = new ArrayList();
					//Code for adding new subheading comes here
					string curVehicle,prevVehicle;
					TableRow row = new TableRow();
					int i=0;
					curVehicle = prevVehicle = "";
					for(i=0;i<dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count;i++)
					{
						StRisk objStRisk = new StRisk();
						curVehicle = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[i]["VEHICLE_ID"].ToString();
						if(curVehicle !=  prevVehicle)
						{
							prevVehicle = curVehicle;
							DataRow dr = dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
							dr["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[i]["VEHICLE"].ToString();
							//dr["COV_DESC"] = "SubHead";
							dr["LIMIT"] = SubHeading;
							dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,i);
							//ankit #itrack 7642	
							objStRisk.rowId=i+1;
							objStRisk.riskId=Convert.ToString(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[i+1]["ACTUAL_RISK_ID"]);
							objStRisk.riskType = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[i+1]["ACTUAL_RISK_TYPE"].ToString();
							
							objStRisk.riskVehicleId=Convert.ToInt32(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[i+1]["Vehicle_Id"]);
							arrLstRisk.Add(objStRisk);
						}				
					} 
					//ankit #itrack 7642						
					for(int j=0;j<arrLstRisk.Count;j++)
					{
						StRisk objStRisks = (StRisk)arrLstRisk[j];
						Insert_Collision_DataRows(dsData.Tables[VEHICLE_COVERAGE_TABLE],LLGC_IN_COV_ID,int.Parse(LLGC_IN_Collision),int.Parse(LLGC_IN_Other_Than_Collision), objStRisks.rowId, 
						objStRisks.riskId, objStRisks.riskType,"Loan/Lease (PP 03 35) - Collision","Loan/Lease (PP 03 35) - Other Than Collision",objStRisks.riskVehicleId);
						Insert_Collision_DataRows(dsData.Tables[VEHICLE_COVERAGE_TABLE],LLGC_MI_COV_ID,int.Parse(LLGC_MI_Collision),int.Parse(LLGC_MI_Other_Than_Collision), objStRisks.rowId, objStRisks.riskId, 
							objStRisks.riskType,"Loan/Lease (PP 03 35) - Collision","Loan/Lease (PP 03 35) - Other Than Collision",objStRisks.riskVehicleId);
					}
					hidVehicleRowCount.Value = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count.ToString();
					//dvCoverages = new DataView(dsData.Tables[VEHICLE_COVERAGE_TABLE]);				
					//this.dgCoverages.DataSource = dvCoverages;
					this.dgCoverages.DataSource = dsData.Tables[VEHICLE_COVERAGE_TABLE]; //itrack:5116
					dgCoverages.DataBind();
				}
				else
				{
					trVehicleRow.Attributes.Add("style","display:none");
					trVehicleGridRow.Attributes.Add("style","display:none");
					dgCoverages.Visible = false;
				}
			
		
				//When both grids are hidden, hide the totals row also
				if(dgCoverages.Visible == false && dgPolicyCoverages.Visible == false)
				{
					trTotalRow.Attributes.Add("style","display:none");
					trBody.Attributes.Add("style","display:none");
					btnSave.Visible = false;
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("804");
					lblMessage.Visible = true;
				}
				else
				{
					string strFirstWidth,strSecondWidth,strThirdWidth;
					if(hidSTATE_ID.Value == ((int)enumState.Indiana).ToString())
					{
						HideShowColumns(dgCoverages,MCCA_ATTACHMENT_TEXT,false);
						HideShowColumns(dgPolicyCoverages,MCCA_ATTACHMENT_TEXT,false);
						HideShowColumns(dgCoverages,MCCA_RESERVE_TEXT,false);
						HideShowColumns(dgPolicyCoverages,MCCA_RESERVE_TEXT,false);					
						strFirstWidth = "45%";
						strSecondWidth = "23%";
						strThirdWidth = "37%";
					}
					else
					{
						strFirstWidth = "23%";
						strSecondWidth = "16%";
						strThirdWidth = "47%";
					}
					tdFirst.Attributes.Add("width",strFirstWidth);
					tdSecond.Attributes.Add("width",strSecondWidth);
					tdThird.Attributes.Add("width",strThirdWidth);
				}
			}
			//Adding rows ends here			
			
			//txtTOTAL_DEDUCTIBLE.Text = TotalDeductible.ToString();
			//if(TotalDeductible!=0 && TotalDeductible!=0.0)
			//	txtTOTAL_DEDUCTIBLE.Text=String.Format("{0:,#,###}",Convert.ToInt64(TotalDeductible.ToString()));
			


		}
		//ankit #itrack 7642
		private struct StRisk
		{
			public int rowId;
			public string riskId;
			public string riskType;
			public int riskVehicleId;
		}
		//ankit #itrack 7642
		private int GetRowPositionWithRisk(DataTable dtTemp,string strCOV_ID,int rowId,string risk_Id,string risk_Type,out string LimitValue)
		{
			int counter=0;
			foreach(DataRow dr in dtTemp.Rows)
			{
				counter++;
				if(dr["COV_ID"]!=null && dr["COV_ID"].ToString()==strCOV_ID && dr["ACTUAL_RISK_ID"].ToString()==risk_Id && dr["ACTUAL_RISK_TYPE"].ToString()==risk_Type)
				{
					LimitValue =dr["Limit"].ToString();
					return counter;				
				}				
			}
			LimitValue="";
			return 0;
		}

		private int GetRowPosition(DataTable dtTemp,string strCOV_ID)
		{
			int counter=0;
			foreach(DataRow dr in dtTemp.Rows)
			{
				counter++;
				if(dr["COV_ID"]!=null && dr["COV_ID"].ToString()==strCOV_ID)
					return counter;				
							
			}
			return 0;
		}
		//ankit #itrack 7642
		private void Insert_Collision_DataRows(DataTable dt,string COV_ID,int 
			BI_COV_ID,int PD_COV_ID,int rowId, string ActualRiskID , string ActualRiskType,string 
			CollisionName,string OtherCollisionName,int VehicleID)
		  {
			int position=0;			
			DataRow drow=null;
			string LimitValue="";
			position = GetRowPositionWithRisk(dt,COV_ID,rowId,ActualRiskID.ToString(),ActualRiskType,out LimitValue);
			if(position!=0)
			{
				
				drow = dt.NewRow();

				drow["COV_DESC"] = CollisionName;
				drow["COV_ID"]=BI_COV_ID;

				drow["ACTUAL_RISK_ID"] = ActualRiskID ;
				drow["ACTUAL_RISK_TYPE"] = ActualRiskType;
				drow["Vehicle_ID"]= VehicleID;
				drow["Limit"]=LimitValue;
				position--;
				dt.Rows.RemoveAt(position);
				dt.Rows.InsertAt(drow,position++);
				drow=null;

				//Adding PD Coverage
				drow = dt.NewRow();
				drow["COV_DESC"] = OtherCollisionName;
				drow["COV_ID"]=PD_COV_ID;
				//				if(dt.Rows[0]["ACTUAL_RISK_ID"].ToString()!="")
				//					drow["ACTUAL_RISK_ID"] = int.Parse(dt.Rows[0]["ACTUAL_RISK_ID"].ToString());
				//				if(dt.Rows[0]["ACTUAL_RISK_TYPE"].ToString()!="")
				//					drow["ACTUAL_RISK_TYPE"] = 	dt.Rows[0]["ACTUAL_RISK_TYPE"].ToString();
				drow["ACTUAL_RISK_ID"] = ActualRiskID ;
				drow["ACTUAL_RISK_TYPE"] = ActualRiskType;
				drow["Vehicle_ID"]= VehicleID;
				drow["Limit"]=LimitValue;
				dt.Rows.InsertAt(drow,position++);
				drow=null;
			}
			
		}

		private void InsertBD_PI_DataRows(DataTable dt,string COV_ID,int BI_COV_ID,int PD_COV_ID, int ActualRiskID , string ActualRiskType)
		{
			int position=0;
			//DataRow[] dr = dt.Select("COV_ID='" + COV_ID + "'");
			DataRow drow=null;
			//if(dr!=null && dr.Length>0)
			//{
				//DataView view = new DataView(dt,null,"RESERVE_ID",DataViewRowState.CurrentRows);
				//DataRowView drv = view[view.Find(dr[0]["RESERVE_ID"])];
				//position = (((IList)view).IndexOf(drv)); 
				position = GetRowPosition(dt,COV_ID);
				if(position!=0)
				{
					//Adding BI Coverage
					drow = dt.NewRow();
					drow["COV_DESC"] = "BI";
					drow["COV_ID"]=BI_COV_ID;

					drow["ACTUAL_RISK_ID"] = ActualRiskID ;
					drow["ACTUAL_RISK_TYPE"] = ActualRiskType;

					dt.Rows.InsertAt(drow,position++);
					drow=null;

					//Adding PD Coverage
					drow = dt.NewRow();
					drow["COV_DESC"] = "PD";
					drow["COV_ID"]=PD_COV_ID;
					if(dt.Rows[0]["ACTUAL_RISK_ID"].ToString()!="")
						drow["ACTUAL_RISK_ID"] = int.Parse(dt.Rows[0]["ACTUAL_RISK_ID"].ToString());
					if(dt.Rows[0]["ACTUAL_RISK_TYPE"].ToString()!="")
						drow["ACTUAL_RISK_TYPE"] = dt.Rows[0]["ACTUAL_RISK_TYPE"].ToString();
					dt.Rows.InsertAt(drow,position++);
					drow=null;
				}
			//}
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
			this.dgPolicyCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPolicyCoverages_ItemDataBound);
			this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
			//if(txtPAYMENT_AMOUNT.Text.Trim()!="")
			//	objActivityInfo.PAYMENT_AMOUNT = Convert.ToDouble(txtPAYMENT_AMOUNT.Text.Trim());
			if(txtTOTAL_RI_RESERVE.Text.Trim()!="")
				TotalRiReserve = Convert.ToDouble(txtTOTAL_RI_RESERVE.Text.Trim());			
						
			if(hidTRANSACTION_CATEGORY.Value=="Reinsurance")
				objActivityInfo.RI_RESERVE = TotalRiReserve - double.Parse(hidOldTotalRiReserve.Value);
			else
				objActivityInfo.RI_RESERVE = 0;
			
			if(txtEXPENSES.Text.Trim()!="")
				objActivityInfo.EXPENSES = Convert.ToDouble(txtEXPENSES.Text.Trim());
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
				PopulateArray(aList,dgPolicyCoverages);
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

		private void PopulateArray(ArrayList aList, DataGrid dtGrid)
		{
			
			foreach(DataGridItem dgi in dtGrid.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					if(dtGrid.ID.ToUpper().Equals(VehicleCoverageGridID.ToUpper()))
					{
						Label lblCOV_DESC = (Label)(dgi.FindControl("lblCOV_DESC"));
						if(lblCOV_DESC!=null && lblCOV_DESC.Text.IndexOf(VehicleText)!=-1)
							continue;
					}
					
					//Model Object
					ClsReserveDetailsInfo objReserveDetailsInfo = new ClsReserveDetailsInfo();
					//Get the Outstanding
					//TextBox txtPRIMARY_EXCESS = (TextBox)(dgi.FindControl("txtPRIMARY_EXCESS"));
					if(hidSTATE_ID.Value != ((int)enumState.Indiana).ToString())
					{
						TextBox txtMCCA_ATTACHMENT_POINT = (TextBox)(dgi.FindControl("txtMCCA_ATTACHMENT_POINT"));
						if(txtMCCA_ATTACHMENT_POINT.Text.Trim()!="")
							objReserveDetailsInfo.MCCA_ATTACHMENT_POINT = Convert.ToDouble(txtMCCA_ATTACHMENT_POINT.Text.Trim());
						TextBox txtMCCA_APPLIES = (TextBox)(dgi.FindControl("txtMCCA_APPLIES"));
						if(txtMCCA_APPLIES.Text.Trim()!="")
							objReserveDetailsInfo.MCCA_APPLIES = Convert.ToDouble(txtMCCA_APPLIES.Text.Trim());
					}
					//DropDownList cmbMCCA_APPLIES = (DropDownList)(dgi.FindControl("cmbMCCA_APPLIES"));					
					TextBox txtOutstanding = (TextBox)(dgi.FindControl("txtOUTSTANDING"));
					//TextBox txtATTACHMENT_POINT = (TextBox)(dgi.FindControl("txtATTACHMENT_POINT"));
					TextBox txtRI_RESERVE = (TextBox)(dgi.FindControl("txtRI_RESERVE"));
					DropDownList cmbREINSURANCE_CARRIER = (DropDownList)(dgi.FindControl("cmbREINSURANCE_CARRIER"));					
					Label lblCOV_ID = (Label)(dgi.FindControl("lblCOV_ID"));
					Label lblVEHICLE_ID = (Label)(dgi.FindControl("lblVEHICLE_ID"));
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));
					//Added for Itrack Issue 7663 on 19 Aug 2010
					Label lblACTUAL_RISK_ID = (Label)(dgi.FindControl("lblACTUAL_RISK_ID"));
					Label lblACTUAL_RISK_TYPE = (Label)(dgi.FindControl("lblACTUAL_RISK_TYPE"));
					
					
					//if(txtPRIMARY_EXCESS.Text.Trim()!="")
					//	objReserveDetailsInfo.PRIMARY_EXCESS = txtPRIMARY_EXCESS.Text.replace(/\,/g,'');

					//if(cmbMCCA_APPLIES.SelectedItem!=null && cmbMCCA_APPLIES.SelectedItem.Value!="")
					//	objReserveDetailsInfo.MCCA_APPLIES = cmbMCCA_APPLIES.SelectedItem.Value;

					

					//if(txtATTACHMENT_POINT.Text.Trim()!="")
					//	objReserveDetailsInfo.ATTACHMENT_POINT = Convert.ToDouble(txtATTACHMENT_POINT.Text.Trim());

					if(txtRI_RESERVE.Text.Trim()!="" && hidTRANSACTION_CATEGORY.Value=="Reinsurance")
						objReserveDetailsInfo.RI_RESERVE = Convert.ToDouble(txtRI_RESERVE.Text.Trim());

					if(cmbREINSURANCE_CARRIER!=null && cmbREINSURANCE_CARRIER.Visible==true && cmbREINSURANCE_CARRIER.SelectedItem!=null && cmbREINSURANCE_CARRIER.SelectedItem.Value!="")
						objReserveDetailsInfo.REINSURANCE_CARRIER = int.Parse(cmbREINSURANCE_CARRIER.SelectedItem.Value);

					if(txtOutstanding.Text!="" && hidTRANSACTION_CATEGORY.Value=="General")
						objReserveDetailsInfo.OUTSTANDING = Convert.ToDouble(txtOutstanding.Text.Trim());

					if(lblCOV_ID.Text!="")
						objReserveDetailsInfo.COVERAGE_ID = int.Parse(lblCOV_ID.Text);
					if(lblVEHICLE_ID!=null && lblVEHICLE_ID.Text!="")
						objReserveDetailsInfo.VEHICLE_ID = int.Parse(lblVEHICLE_ID.Text);
					objReserveDetailsInfo.IS_ACTIVE="Y";
					objReserveDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objReserveDetailsInfo.CREATED_DATETIME = System.DateTime.Now;
					objReserveDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReserveDetailsInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
					objReserveDetailsInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					//Added for Itrack Issue 7663 on 19 Aug 2010
					objReserveDetailsInfo.ACTUAL_RISK_ID = int.Parse(lblACTUAL_RISK_ID.Text);
					objReserveDetailsInfo.ACTUAL_RISK_TYPE = lblACTUAL_RISK_TYPE.Text;
					//Done for Itrack Issue 6835 on 16 Dec 09
					if(hidACTIVITY_ID.Value !="1")
						objReserveDetailsInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
					else
						objReserveDetailsInfo.ACTIVITY_ID = 0;
					if(cmbDrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
						objReserveDetailsInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedValue);
					if(hidACTION_ON_PAYMENT.Value!="")
						objReserveDetailsInfo.ACTION_ON_PAYMENT = int.Parse(hidACTION_ON_PAYMENT.Value);

					if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
						objReserveDetailsInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedValue);					

					if(lblRESERVE_ID.Text.Trim()!="")
						objReserveDetailsInfo.RESERVE_ID = int.Parse(lblRESERVE_ID.Text.Trim());
					
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
			OnItemDataBound(sender,e,dgCoverages.ID);
		}	
		private void dgPolicyCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			OnItemDataBound(sender,e,dgPolicyCoverages.ID);
		}	
		private void OnItemDataBound(object sender,DataGridItemEventArgs e,string dataGridID)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				
				//((TextBox)(e.Item.FindControl("txtPRIMARY_EXCESS"))).Attributes.Add("onBlur","javascript: this.value = formatCurrencyWithCents(this.value);");

				
				//Modified by Asfa (23-Apr-2008) - iTrack issue #4102
				if(hidSTATE_ID.Value != ((int)enumState.Indiana).ToString() && hidCustomerID.Value!="" && hidCustomerID.Value!="0")
				{
					TextBox txtMCCA_ATTACHMENT_POINT = (TextBox)(e.Item.FindControl("txtMCCA_ATTACHMENT_POINT"));
					txtMCCA_ATTACHMENT_POINT.Attributes.Add("onBlur","javascript: this.value = formatCurrencyWithCents(this.value);");
					TextBox txtMCCA_APPLIES = (TextBox)(e.Item.FindControl("txtMCCA_APPLIES"));
					txtMCCA_APPLIES.Attributes.Add("onBlur","javascript: CalculateTotalMCCA_APPLIES();");

					if(txtMCCA_ATTACHMENT_POINT!=null)
					{
						if (DataBinder.Eval(e.Item.DataItem,"MCCA_ATTACHMENT_POINT") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"MCCA_ATTACHMENT_POINT"))!="0")
							txtMCCA_ATTACHMENT_POINT.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"MCCA_ATTACHMENT_POINT"));
						else
							txtMCCA_ATTACHMENT_POINT.Text = "";
					}

					if(txtMCCA_APPLIES!=null)
					{
						if (DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES"))!="0")
							txtMCCA_APPLIES.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES"));
						else
							txtMCCA_APPLIES.Text = "";
					}
				}
				Label lblCOV_ID = (Label)(e.Item.FindControl("lblCOV_ID"));
				//DropDownList cmbMCCA_APPLIES = (DropDownList)(e.Item.FindControl("cmbMCCA_APPLIES"));				
				//TextBox txtPRIMARY_EXCESS = (TextBox)(e.Item.FindControl("txtPRIMARY_EXCESS"));
				Label lblMCCA_APPLIES = (Label)(e.Item.FindControl("lblMCCA_APPLIES"));
				Label lblMCCA_ATTACHMENT_POINT = (Label)(e.Item.FindControl("lblMCCA_ATTACHMENT_POINT"));
				//txtPRIMARY_EXCESS.Attributes.Add("onBlur","javascript:ShowHideMCCA(" + txtPRIMARY_EXCESS.ClientID + "," + txtMCCA_ATTACHMENT_POINT.ClientID + "," + lblMCCA_ATTACHMENT_POINT.ClientID + "," + txtMCCA_APPLIES.ClientID + "," + lblMCCA_APPLIES.ClientID + "," + lblCOV_ID.ClientID + ");");								
				((TextBox)(e.Item.FindControl("txtATTACHMENT_POINT"))).Attributes.Add("onBlur","javascript: this.value = formatCurrencyWithCents(this.value);");
				
				/*Label lblDEDUCTIBLE = (Label)(e.Item.FindControl("lblDEDUCTIBLE"));
				if(lblDEDUCTIBLE.Text.Trim()!="" && lblDEDUCTIBLE.Text.Trim()!="0")
				{
					int CommaPosition = lblDEDUCTIBLE.Text.IndexOf(" ");
					if(CommaPosition!=-1)
					{
						string strDeduct = lblDEDUCTIBLE.Text.Substring(0,CommaPosition);
						TotalDeductible += Convert.ToDouble(strDeduct);
					}
				}*/				

				//revRECOVERY.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				revRECOVERY.ValidationExpression = aRegExpDoublePositiveZero;
				revRECOVERY.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				//revEXPENSES.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				revEXPENSES.ValidationExpression = aRegExpDoublePositiveZero;
				revEXPENSES.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				((RegularExpressionValidator)(e.Item.FindControl("revOUTSTANDING"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				((RegularExpressionValidator)(e.Item.FindControl("revMCCA_ATTACHMENT_POINT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				((RegularExpressionValidator)(e.Item.FindControl("revATTACHMENT_POINT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				((RegularExpressionValidator)(e.Item.FindControl("revMCCA_APPLIES"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				((RegularExpressionValidator)(e.Item.FindControl("revRI_RESERVE"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385				

				//((RegularExpressionValidator)(e.Item.FindControl("revOUTSTANDING"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				((RegularExpressionValidator)(e.Item.FindControl("revOUTSTANDING"))).ValidationExpression = aRegExpDoublePositiveZero;
				//((RegularExpressionValidator)(e.Item.FindControl("revMCCA_ATTACHMENT_POINT"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 10 Feb 09 for Itrack Issue 5385
				((RegularExpressionValidator)(e.Item.FindControl("revMCCA_ATTACHMENT_POINT"))).ValidationExpression = aRegExpDoublePositiveZero;
				//((RegularExpressionValidator)(e.Item.FindControl("revATTACHMENT_POINT"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 10 Feb 09 for Itrack Issue 5385			
				((RegularExpressionValidator)(e.Item.FindControl("revATTACHMENT_POINT"))).ValidationExpression = aRegExpDoublePositiveZero;
				//((RegularExpressionValidator)(e.Item.FindControl("revMCCA_APPLIES"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 10 Feb 09 for Itrack Issue 5385			
				((RegularExpressionValidator)(e.Item.FindControl("revMCCA_APPLIES"))).ValidationExpression = aRegExpDoublePositiveZero;
				//((RegularExpressionValidator)(e.Item.FindControl("revRI_RESERVE"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385			
				((RegularExpressionValidator)(e.Item.FindControl("revRI_RESERVE"))).ValidationExpression = aRegExpDoublePositiveZero;

				((RangeValidator)(e.Item.FindControl("rngOUTSTANDING"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				((RangeValidator)(e.Item.FindControl("rngMCCA_ATTACHMENT_POINT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				((RangeValidator)(e.Item.FindControl("rngATTACHMENT_POINT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				((RangeValidator)(e.Item.FindControl("rngMCCA_APPLIES"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				((RangeValidator)(e.Item.FindControl("rngRI_RESERVE"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				
				//((RangeValidator)(e.Item.FindControl("rngPRIMARY_EXCESS"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				
				
				

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
				//End of Addition.
				
				if ( DataBinder.Eval(e.Item.DataItem,"REINSURANCE_CARRIER") != System.DBNull.Value) 
				{
					ClsCommon.SelectValueInDDL(cmbREINSURANCE_CARRIER,DataBinder.Eval(e.Item.DataItem,"REINSURANCE_CARRIER"));
							
				}	
				/*Display the MCCA Attachment and Reserve fields only when coverage is of
				Personal Injury Protection --cov_id-116 and state is michigan..
				we are adding the check only for coverage_id as the coverage itself is defined for the 
				state of michigan only				
				*/
				lblCOV_ID.Attributes.Add("style","display:none");
				/*if ( DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES") != System.DBNull.Value) 
				{
					ClsCommon.SelectValueInDDL(cmbMCCA_APPLIES,DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES"));
							
				}*/	
				//txtMCCA_APPLIES.Attributes.Add("style","display:none");
				//txtMCCA_ATTACHMENT_POINT.Attributes.Add("style","display:none");
				Label lblLIMIT = (Label)e.Item.FindControl("lblLIMIT");
				if(dataGridID.ToUpper().Equals(VehicleCoverageGridID.ToUpper()))
				{
										
					if(lblLIMIT.Text.Equals(SubHeading))					
					{
						e.Item.Cells[0].ColumnSpan=e.Item.Cells.Count;
						for(int j=e.Item.Cells.Count-1;j>0;j--)
							e.Item.Cells.RemoveAt(j);
						
						e.Item.Cells[0].Font.Bold = true;
						e.Item.Cells[0].Attributes.Add("align","left");
					}				
				}
				else
				{
					switch(lblCOV_ID.Text)
					{
						//As per discussion with Gagan on 27-Sept-2007 "Outstanding and Reserve Textboxes should be shown for PIP too."
						//case PIP_COV_ID:
						case SLL_COV_ID:
						case RLCSL_COV_ID:
						case UNINSURED_MOTORISTS_COV_ID_IN:
						case UNINSURED_MOTORISTS_COV_ID_MI:
						case UNDERINSURED_MOTORISTS_COV_ID_IN:
						case UNDERINSURED_MOTORISTS_COV_ID_MI:
							((TextBox)(e.Item.FindControl("txtOUTSTANDING"))).Attributes.Add("style","display:none");
							((TextBox)(e.Item.FindControl("txtRI_RESERVE"))).Attributes.Add("style","display:none");
							//txtMCCA_APPLIES.Attributes.Add("style","display:inline");
							//txtMCCA_ATTACHMENT_POINT.Attributes.Add("style","display:inline");
							break;
						case MEDICAL_COV_ID_1:
						case WORK_LOSS_COV_ID_1:
						case DEATH_BENEFITS_COV_ID_1:
						case SURVIVOR_COV_ID_1:
						//Commented and changed by Shikha as codes were needed seperately for Michigan and Indiana 
						//to avoid any ambiguity.
						/*case SLL_BI_ID:
						case SLL_PD_ID:
						case UNINSURED_MOTORISTS_BI_ID:
						case UNINSURED_MOTORISTS_PD_ID:		
						case UNDERINSURED_MOTORISTS_BI_ID:
						case UNDERINSURED_MOTORISTS_PD_ID:*/
						case SLL_BI_ID_MI:
						case SLL_PD_ID_MI:
						case SLL_BI_ID_IN:
						case SLL_PD_ID_IN:
						case UNINSURED_MOTORISTS_BI_ID_MI:
						case UNINSURED_MOTORISTS_PD_ID_MI:
						case UNINSURED_MOTORISTS_BI_ID_IN:
						case UNINSURED_MOTORISTS_PD_ID_IN:
						case UNDERINSURED_MOTORISTS_BI_ID_MI:
						case UNDERINSURED_MOTORISTS_PD_ID_MI:
						case UNDERINSURED_MOTORISTS_BI_ID_IN:
						case UNDERINSURED_MOTORISTS_PD_ID_IN:
							//txtPRIMARY_EXCESS.Attributes.Add("style","display:none");
							//((TextBox)(e.Item.FindControl("txtRI_RESERVE"))).Attributes.Add("style","display:none");
							//cmbREINSURANCE_CARRIER.Attributes.Add("style","display:none");
							cmbREINSURANCE_CARRIER.Visible = false;
							break;						
						default:
							break;
					}	
				}
				/*string LimitAmt="0";
				if(lblLIMIT.Text.Trim()!="")
				{
					LimitAmt = lblLIMIT.Text.Trim().replace(/\,/g,'');
					if(lblLIMIT.Text.Trim().LastIndexOf("/")>0)					
						LimitAmt = LimitAmt.Split('/')[0];					
				}*/
				Label lblDeductible = (Label)e.Item.FindControl("lblDEDUCTIBLE");
				if(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))!="" && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))!="0")
					lblDeductible.Text= Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))).ToString("N");
				else
					lblDeductible.Text = "";
				
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
					txtOUTSTANDING.Attributes.Add("onBlur","javascript:CompareLimitAndOutstanding(" + lblLIMIT.ClientID + "," + txtOUTSTANDING.ClientID + ");");
					if(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING"))!="0")
						txtOUTSTANDING.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"OUTSTANDING"));															
					else
						txtOUTSTANDING.Text = "";
				}

				//Added For Itrack Issue #5556 Note 3.
				if(hidTRANSACTION_CATEGORY.Value.ToUpper() =="GENERAL")
				{				
					txtRI_RESERVE.Enabled = false;
				}				
				else if (hidTRANSACTION_CATEGORY.Value.ToUpper() =="REINSURANCE")
				{
					txtOUTSTANDING.Enabled = false;				 
				}
				
			}
		}
		private string CheckLOBAndRedirect()
		{
			string strLOB_ID=string.Empty;
			string strReservePage=string.Empty;
			if(GetLOBID()!=string.Empty)
				strLOB_ID = GetLOBID();
			if(strLOB_ID==string.Empty)
				return "";
			else if(strLOB_ID==((int)enumLOB.AUTOP).ToString())
				strReservePage=string.Empty;
			else if(strLOB_ID==((int)enumLOB.BOAT).ToString())
				strReservePage=WATERCRAFT_RESERVE_PAGE;
			else if(strLOB_ID==((int)enumLOB.REDW).ToString())
				strReservePage=RENTAL_RESERVE_PAGE;
			else if(strLOB_ID==((int)enumLOB.HOME).ToString())
				strReservePage=HOME_RESERVE_PAGE;
			else if(strLOB_ID==((int)enumLOB.CYCL).ToString())
				strReservePage = MOTOR_RESERVE_PAGE;				
			else if(strLOB_ID==((int)enumLOB.UMB).ToString())
				strReservePage = UMB_RESERVE_PAGE;				
			else
				strReservePage = "";
			return strReservePage;
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

		private void SetNewReserveFlag()
		{
			int retVal = 0;
			retVal =  int.Parse(ClsClaimsNotification.CheckForReservesAdded(hidCLAIM_ID.Value));
			if (retVal == 1)
				hidNewReserve.Value = "1";
			else
				hidNewReserve.Value = "0";
		}

		private void SetSaveVisibility()
		{
			DataSet ds = ClsReserveDetails.CheckClaimStatus(hidCLAIM_ID.Value, hidACTIVITY_ID.Value);
			if ((ds.Tables[0].Rows.Count >0 && ds.Tables[0].Rows[0]["ACTIVITY_STATUS"].ToString() == "11801") || ds.Tables[1].Rows[0]["CLAIM_STATUS"].ToString() == CLAIM_STATUS_CLOSED ) // COMPLETE
				btnSave.Visible = false;
			else
				btnSave.Visible = true;	
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

		private void GetPredefineAttachmentPoint()
		{
			mccaAttachmentPoint = ClsReserveDetails.GetPredefineAttachmentPoint(hidCLAIM_ID.Value);
		}

	}
}
