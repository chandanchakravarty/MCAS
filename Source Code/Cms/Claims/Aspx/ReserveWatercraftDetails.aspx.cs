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
	public class ReserveWatercraftDetails : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldTotalRiReserve;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldTotalOutstanding;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDummyPolicyCoverageRowCount;	
		protected System.Web.UI.WebControls.RegularExpressionValidator revRECOVERY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPENSES;
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
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;//Done for Itrack Issue 6299 on 26 Aug 09
		protected System.Web.UI.WebControls.Label lblTitle;	
		protected System.Web.UI.WebControls.TextBox txtGrossTotal;
		System.Resources.ResourceManager objResourceMgr;	
		protected Cms.CmsWeb.WebControls.ClaimActivityTop cltClaimActivityTop;
		protected string ActivityClientID,ActivityTotalPaymentClientID;
		
		
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;		
		//protected System.Web.UI.WebControls.Label capACTIVITY_DATE;
		//protected System.Web.UI.WebControls.Label lblACTIVITY_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected Cms.CmsWeb.Controls.CmsButton btnReserveBreakdown;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
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
		protected double TotalDeductible=0;
		protected System.Web.UI.WebControls.TextBox txtRECOVERY;
		protected System.Web.UI.WebControls.RangeValidator rngRECOVERY;
		protected System.Web.UI.WebControls.RangeValidator rngEXPENSES;		
		//protected System.Web.UI.WebControls.TextBox txtPAYMENT_AMOUNT;
		//protected System.Web.UI.WebControls.RangeValidator rngPAYMENT_AMOUNT;
		protected System.Web.UI.WebControls.Label Label8;
		//IList iLookUp;		
		const string SubHeading = "SubHeading";
		const string VehicleText = "Boat # : ";
		const string TrailerText = "Trailer # : ";
		//Done for Itrack Issue 6299 on 26 Aug 09
		//const int TrailerCovID = 50001;
		private int TrailerCovID;
		public static string VehicleCoverageGridID = "";
		//public static string PolicyCoverageGridID = "";
		const int POLICY_COVERAGE_TABLE = 0;
		protected System.Web.UI.WebControls.DataGrid dgCoverages;
		const int VEHICLE_COVERAGE_TABLE = 1;		
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehicleRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehicleGridRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTotalRow;

		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftEquipmentCoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftEquipmentCoveragesGridRow;
		protected System.Web.UI.WebControls.Label lblWatercraftEquipmentCoverages;
		protected System.Web.UI.WebControls.PlaceHolder plcWatercraftEquipmentCovg;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipItemRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipCovgID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipRsvID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquip_ACTUAL_RISK_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquip_ACTUAL_RISK_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquipDWELLING_ID;
		string strWATER_EQUIPMENT_COVERAGE_ID = "";
		const int WATER_EQUIPMENT_COVERAGE_TABLE = 2;
		const int WATERCRAFT_COVERAGE_TABLE = 1;
		//Done for IItrack Issue 7663 on 28 July 2010
		/*protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftEquipmentCoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftEquipmentCoveragesGridRow;
		protected System.Web.UI.WebControls.Label lblWatercraftEquipmentCoverages;
		protected System.Web.UI.WebControls.PlaceHolder plcWatercraftEquipmentCovg;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipItemRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipCovgID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipRsvID;
		string strWATER_EQUIPMENT_COVERAGE_ID = "";
		const int WATER_EQUIPMENT_COVERAGE_TABLE = 2;*/

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="306_0_1_1";	

			btnReserveBreakdown.CmsButtonClass		=	CmsButtonType.Read;
			btnReserveBreakdown.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.ReserveWatercraftDetails"  ,System.Reflection.Assembly.GetExecutingAssembly());
			GetQueryStringValues();
			SetClaimTop();			
			// Put user code to initialize the page here			
			if ( !Page.IsPostBack)
			{
				VehicleCoverageGridID = dgCoverages.ID;
				//PolicyCoverageGridID = dgPolicyCoverages.ID;
				SetSaveVisibility();
				SetNewReserveFlag();
				SetCaptions();
				SetErrorMessages();	
				LoadAcntgDropDowns();
				btnBack.Attributes.Add("onClick","javascript: return GoToClaimDetailPage();");
				btnSave.Attributes.Add("onClick","javascript: return CompareAllLimitAndOutstanding();");
				GetClaimDetails();
				//btnReserveBreakdown.Attributes.Add("onClick","javascript: return OpenWindow();");
				txtRECOVERY.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				txtEXPENSES.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				//txtPAYMENT_AMOUNT.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");				
				//Check whether the current record being viewed belongs to a dummy policy
				/* ***** Comment Block Starts ***** 
				Commented by Asfa(06-Dec-2007) - iTrack issue #3141
				//Display an appropriate message for dummy policy
				if((hidCustomerID.Value=="" || hidCustomerID.Value=="0") && (hidPolicyID.Value=="" || hidPolicyID.Value=="0") && (hidPolicyVersionID.Value=="" || hidPolicyVersionID.Value=="0"))
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("805");
					lblMessage.Visible = true;
					trBody.Attributes.Add("style","display:none");
					return;
				}
				/* ***** Comment Block Ends ***** 
				
				//Let us remove the check..let it be working as it is..
				/*if(hidACTIVITY_ID.Value=="" || hidACTIVITY_ID.Value=="0")
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("803") + "<br><a href='javascript:GoBack();'>Click here</a> to add an Activity</a>";
					lblMessage.Visible = true;
					trBody.Attributes.Add("style","display:none");
					return;
				}*/
				//GetActivityData();
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
				hidSTATE_ID.Value = dr["STATE_ID"].ToString();//Done for Itrack Issue 6299 on 26 Aug 09
			}
		}

		private void SetErrorMessages()
		{
			rngRECOVERY.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rngEXPENSES.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			//revRECOVERY.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			revRECOVERY.ValidationExpression = aRegExpDoublePositiveZero;
			revRECOVERY.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			//revEXPENSES.ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385;
			revEXPENSES.ValidationExpression = aRegExpDoublePositiveZero;
			revEXPENSES.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
			//rngPAYMENT_AMOUNT.ErrorMessage			  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
		}

		//Done for Itrack Issue 6299 on 26 Aug 09
		private void SetTrailerCoverageID()
		{
			if(hidSTATE_ID.Value == ((int)enumState.Indiana).ToString())
				TrailerCovID= TrailerCovID_IN;
			else if(hidSTATE_ID.Value == ((int)enumState.Michigan).ToString())
				TrailerCovID= TrailerCovID_MI;
			else if(hidSTATE_ID.Value == ((int)enumState.Wisconsin).ToString())
				TrailerCovID= TrailerCovID_WI;
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
					if(dsOldData!=null && dsOldData.Tables.Count>0)
					{
						if(dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count>0)
						{
							//						if(dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_CRACCTS"]!=null && dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_CRACCTS"].ToString()!="")
							//							cmbCrAccts.SelectedValue = dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_CRACCTS"].ToString();
							//						if(dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_DRACCTS"]!=null && dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_DRACCTS"].ToString()!="")
							//							cmbDrAccts.SelectedValue = dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_DRACCTS"].ToString();
							if(dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"]!=null && dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"].ToString()!="")
								hidACTION_ON_PAYMENT.Value = dsOldData.Tables[VEHICLE_COVERAGE_TABLE].Rows[0]["CLM_RESERVE_ACTION_ON_PAYMENT"].ToString();
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



		/*public void GetActivityData()
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
		}*/

		#region SetCaptions
		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			//capACTIVITY_DATE.Text 		=		objResourceMgr.GetString("lblACTIVITY_DATE");			
			//capTOTAL_DEDUCTIBLE.Text	=		objResourceMgr.GetString("txtTOTAL_DEDUCTIBLE");			
			//capTOTAL_RI_RESERVE.Text	=		objResourceMgr.GetString("txtTOTAL_RI_RESERVE");			
			//capTOTAL_OUTSTANDING.Text	=		objResourceMgr.GetString("txtTOTAL_OUTSTANDING");			
			capRECOVERY.Text			=		objResourceMgr.GetString("txtRECOVERY");			
			//capPAYMENT_AMOUNT.Text		=		objResourceMgr.GetString("txtPAYMENT_AMOUNT");			
			capEXPENSES.Text			=		objResourceMgr.GetString("txtEXPENSES");			
		}
		#endregion

		
		
		private void BindGrid(DataSet dsData)
		{
			//if(dsData.Tables.Count == 0)
			if(dsData==null || dsData.Tables[1].Rows.Count==0)
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
				if(dsData==null || dsData.Tables.Count<VEHICLE_COVERAGE_TABLE || dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count<1)
				{							
					ClsReserveDetails  objReserveDetails = ClsReserveDetails.CreateReserveObject(hidLOB_ID.Value);				
					dsData = objReserveDetails.GetPolicyCoveragesForReserve(hidCLAIM_ID.Value);
					if(dsData==null) 
						return;				
				
				}
			
				if(dsData!=null && dsData.Tables.Count>VEHICLE_COVERAGE_TABLE && dsData.Tables[VEHICLE_COVERAGE_TABLE]!=null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count>1)
				{
					//Code for adding new subheading comes here
					//Done for Itrack Issue 6299 on 26 Aug 09
					SetTrailerCoverageID();
					string curVehicle,prevVehicle;
					TableRow row = new TableRow();
					int position=0 , BoatCounter = 1; 
					curVehicle = prevVehicle = "";
				
					int RowCount = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count;

					DataSet dsFinal = dsData.Clone();
					int FinalPosition = 0 ; 
					bool AddRow = true; 
					while(position < RowCount)
					{
						AddRow = true;
						curVehicle = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["VEHICLE_ID"].ToString();

						if(prevVehicle == "")
						{
							DataRow newRow = dsFinal.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
							newRow["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["VEHICLE"];							
							newRow["LIMIT"] = SubHeading;
							dsFinal.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(newRow,FinalPosition);
							FinalPosition++;
							prevVehicle = curVehicle ; 
						}

						if(prevVehicle != curVehicle)
						{

							//Create Rows for Trailer 
							AddTrailer(dsFinal,prevVehicle,ref FinalPosition);

							DataRow newRow = dsFinal.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
							newRow["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["VEHICLE"];							
							newRow["LIMIT"] = SubHeading;
							dsFinal.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(newRow,FinalPosition);
							prevVehicle = curVehicle ; 
							FinalPosition++;
							BoatCounter = 1; 
						}

						//ACTUAL_RISK_TYPE
						if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["ACTUAL_RISK_TYPE"].ToString() == "TR")
						{
							string ActualRiskId = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["ACTUAL_RISK_ID"].ToString();
							
							DataSet dsTrailer		= ClsReserveDetails.GetTrailerDetails(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,	curVehicle,ActualRiskId );
							DataRow HeaderRow		= dsFinal.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
							HeaderRow["COV_DESC"]	= TrailerText + " #: " + BoatCounter.ToString() ;						
							HeaderRow["LIMIT"]		= SubHeading;
							
							DataRow TrailerRow = dsFinal.Tables[VEHICLE_COVERAGE_TABLE].NewRow();

							TrailerRow["COV_DESC"]	= "Section 1 - Covered Property Damage - Actual Cash Value";
							TrailerRow["COV_ID"]	= TrailerCovID ;
							TrailerRow["LIMIT"]		= dsTrailer.Tables[0].Rows[0]["INSURED_VALUE"].ToString();
							TrailerRow["DEDUCTIBLE"]= dsTrailer.Tables[0].Rows[0]["TRAILER_DED"].ToString();
							TrailerRow["VEHICLE_ID"]= curVehicle ;
							TrailerRow["ACTUAL_RISK_ID"] = ActualRiskId; 
							TrailerRow["ACTUAL_RISK_TYPE"] = "TR";
							TrailerRow["OUTSTANDING"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["OUTSTANDING"];
							
							if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["PRIMARY_EXCESS"] != null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["PRIMARY_EXCESS"] != DBNull.Value)
							{
								TrailerRow["PRIMARY_EXCESS"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["PRIMARY_EXCESS"];
							}

							if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["ATTACHMENT_POINT"] != null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["ATTACHMENT_POINT"] != DBNull.Value)
							{
								TrailerRow["ATTACHMENT_POINT"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["ATTACHMENT_POINT"];
							}

							if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["RI_RESERVE"] != null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["RI_RESERVE"] != DBNull.Value)
							{
								TrailerRow["RI_RESERVE"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["RI_RESERVE"]; 
							}

							if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["RESERVE_ID"] != null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["RESERVE_ID"] != DBNull.Value)
							{
								TrailerRow["RESERVE_ID"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["RESERVE_ID"];
							}

							if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["REINSURANCE_CARRIER"] != null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["REINSURANCE_CARRIER"] != DBNull.Value)
							{
								TrailerRow["REINSURANCE_CARRIER"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["REINSURANCE_CARRIER"];
							}

							if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["REINSURANCECARRIER"] != null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["REINSURANCECARRIER"] != DBNull.Value)
							{
								TrailerRow["REINSURANCECARRIER"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["REINSURANCECARRIER"];
							}

							if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"] != null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"] != DBNull.Value)
							{
								TrailerRow["MCCA_ATTACHMENT_POINT"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"];
							}

							if(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["MCCA_APPLIES"] != null && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["MCCA_APPLIES"] != DBNull.Value)
							{
								TrailerRow["MCCA_APPLIES"] = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["MCCA_APPLIES"];
							}


							dsFinal.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(HeaderRow, FinalPosition );
							FinalPosition++;

							dsFinal.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(TrailerRow , FinalPosition );

							BoatCounter++;
							AddRow = false;
						}

						if(AddRow)
						{
							DataRow drExisting = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position];
							
							dsFinal.Tables[VEHICLE_COVERAGE_TABLE].ImportRow(drExisting);

						}
						position++;
						FinalPosition++;

						if(position>= RowCount )
						{
							AddTrailer(dsFinal,prevVehicle,ref FinalPosition);
						}
					}


//					while(position<dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count)
//					{
//						DataRow dr;
//						curVehicle = dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["VEHICLE_ID"].ToString();
//						if(curVehicle !=  prevVehicle )
//						{
//							cntTrailer = 0;
//							if(prevVehicle == "")
//							{
//								//prevVehicle = curVehicle;
//								dr = dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
//								//position++;
//								dr["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["VEHICLE"];						
//								dr["LIMIT"] = SubHeading;
//								//position++;
//								dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,position);						
//							}
//						
//							//Added by Asfa (29-Feb-2008) - iTrack issue #3597
//							//Adding Trailer Information
//							if(prevVehicle != "")
//							{
//								if(Convert.ToInt32(IS_RESERVE_ADDED)==0)
//								{
//									DataTable dtClmBoat = ClsInsuredBoat.GetOldDataForPageControls(hidCLAIM_ID.Value, prevVehicle);
//									if(dtClmBoat != null && dtClmBoat.Rows.Count>0)
//									{
//										if(dtClmBoat.Rows[0]["INCLUDE_TRAILER"].ToString() == "10963")
//										{
//											dsTrailer = ClsReserveDetails.GetTrailerDataSet(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,	prevVehicle);
//											if(dsTrailer != null && dsTrailer.Tables[0].Rows.Count>0)
//											{
//												for(int cnt=0; cnt< dsTrailer.Tables[0].Rows.Count; cnt++)
//												{
//													dr		= dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
//													dr["COV_DESC"]	= TrailerText + (cnt+1) ;						
//													dr["LIMIT"]		= SubHeading;
//													//position++;
//													dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,position);
//													dr=null;
//
//													dr = dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
//													dr["COV_DESC"]	= "Section 1 - Covered Property Damage - Actual Cash Value";
//													dr["COV_ID"]	= varTrailerCovID ;
//													//dr["COV_ID"]	= 59;
//													dr["LIMIT"]		= dsTrailer.Tables[0].Rows[cnt]["INSURED_VALUE"].ToString();
//													dr["DEDUCTIBLE"]= dsTrailer.Tables[0].Rows[cnt]["TRAILER_DED"].ToString();
//													dr["VEHICLE_ID"]= prevVehicle;
//													//Added for Itrack Issue 7663 on 19 Aug 2010
//													dr["ACTUAL_RISK_ID"] = cnt + 1;
//													dr["ACTUAL_RISK_TYPE"] = "TR";
//													position++;
//													dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,position);
//													dr=null;
//													position++;
//												}
//											}
//										}
//									}
//								}
//								dr = dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
//								dr["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["VEHICLE"];						
//								dr["LIMIT"] = SubHeading;
//								dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,position);
//							}
//						
////								if(prevVehicle != "")
////								{
////									//prevVehicle = curVehicle;
////									dr = dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
////									//position++;
////									dr["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["VEHICLE"];						
////									dr["LIMIT"] = SubHeading;
////									//position++;
////									dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,position);						
////								}
//							prevVehicle = curVehicle;
//						}
//						if(Convert.ToInt32(IS_RESERVE_ADDED)==1 && dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["COV_ID"].ToString() !="")
//						{
//							if(Convert.ToInt32(dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["COV_ID"])>=TrailerCovID)
//							{
//								dsTrailer = ClsReserveDetails.GetTrailerDataSet(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,	curVehicle);
//								if(dsTrailer != null && dsTrailer.Tables[0].Rows.Count>0)
//								{
//									dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["LIMIT"] = dsTrailer.Tables[0].Rows[cntTrailer]["INSURED_VALUE"];
//									dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows[position]["DEDUCTIBLE"] = dsTrailer.Tables[0].Rows[cntTrailer]["TRAILER_DED"];
//								}
//								dr		= dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
//								dr["COV_DESC"]	= TrailerText + (++cntTrailer) ;						
//								dr["LIMIT"]		= SubHeading;
//								//position++;
//								dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,position);
//								dr=null;
//								position++;
//							}
//						}
//							
//						position++;
//					} 
					//Added by Asfa (29-Feb-2008) - iTrack issue #3597
					//Adding Trailer Information 

//				
//					if(Convert.ToInt32(IS_RESERVE_ADDED)==0)
//					{
//						DataTable dtClmBoat2 = ClsInsuredBoat.GetOldDataForPageControls(hidCLAIM_ID.Value, curVehicle);
//						if(dtClmBoat2 != null && dtClmBoat2.Rows.Count>0)
//						{
//							if(dtClmBoat2.Rows[0]["INCLUDE_TRAILER"].ToString() == "10963")
//							{
//								DataSet dsTrailer = dsTrailer = ClsReserveDetails.GetTrailerDataSet(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,	curVehicle);
//								if(dsTrailer != null && dsTrailer.Tables[0].Rows.Count>0)
//								{
//									for(int cnt=0; cnt< dsTrailer.Tables[0].Rows.Count; cnt++)
//									{
//										DataRow dr		= dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
//										dr["COV_DESC"]	= TrailerText + (cnt+1) ;						
//										dr["LIMIT"]		= SubHeading;
//										dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,position);
//										dr=null;
//
//										dr = dsData.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
//										dr["COV_DESC"]	= "Section 1 - Covered Property Damage - Actual Cash Value";
//										dr["COV_ID"]	= varTrailerCovID ;
//										//dr["COV_ID"]	= 59;
//										dr["LIMIT"]		= dsTrailer.Tables[0].Rows[cnt]["INSURED_VALUE"].ToString();
//										dr["DEDUCTIBLE"]= dsTrailer.Tables[0].Rows[cnt]["TRAILER_DED"].ToString();
//										dr["VEHICLE_ID"]= curVehicle;
//										//Added for Itrack Issue 7663 on 19 Aug 2010
//										dr["ACTUAL_RISK_ID"] = cnt + 1;
//										dr["ACTUAL_RISK_TYPE"] = "TR";
//										position++;
//										dsData.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(dr,position);
//										dr=null;
//										position++;
//									}
//								}
//							}
//						}
//					}
					//Code ends here
					hidVehicleRowCount.Value = dsFinal.Tables[VEHICLE_COVERAGE_TABLE].Rows.Count.ToString();
					dgCoverages.DataSource = dsFinal.Tables[VEHICLE_COVERAGE_TABLE];
					dgCoverages.DataBind();
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

				//Added for Itrack Issue 7663
				//Done for Itrack Issue 7784 on 17 Aug 2010
				if(dsData.Tables.Count>WATERCRAFT_COVERAGE_TABLE && dsData.Tables[WATERCRAFT_COVERAGE_TABLE]!=null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count>0 && dsData.Tables.Count>WATER_EQUIPMENT_COVERAGE_TABLE && dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE]!=null && dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE].Rows.Count>0)
				{
					hidWaterEquipItemRowCount.Value = dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE].Rows.Count.ToString();
					LoadOldWatercraftEquipCovgData(dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE]);
				}
				else
				{
					trWatercraftEquipmentCoveragesGridRow.Attributes.Add("style","display:none");
					trWatercraftEquipmentCoveragesRow.Attributes.Add("style","display:none");
				}

				//Added for Itrack Issue 7663
				/*if(dsData.Tables.Count>WATER_EQUIPMENT_COVERAGE_TABLE && dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE]!=null && dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE].Rows.Count>0)
				{
					hidWaterEquipItemRowCount.Value = dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE].Rows.Count.ToString();
					LoadOldWatercraftEquipCovgData(dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE]);
				}*/
			}
		}

		private void AddTrailer(DataSet dsFinal,string VehicleID, ref int FinalPosition)
		{
			if(Convert.ToInt32(IS_RESERVE_ADDED)==0)
			{
				if(ClsInsuredBoat.IncludeTrailer(hidCLAIM_ID.Value, VehicleID))
				{
					DataSet	dsTrailer = ClsReserveDetails.GetTrailerDataSet(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,	VehicleID);

					if(dsTrailer != null && dsTrailer.Tables[0].Rows.Count>0)
					{
						for(int cnt=0; cnt< dsTrailer.Tables[0].Rows.Count; cnt++)
						{
							DataRow HeaderRow			= dsFinal.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
							HeaderRow["COV_DESC"]		= TrailerText + (cnt+1) ;						
							HeaderRow["LIMIT"]			= SubHeading;
							dsFinal.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt(HeaderRow , FinalPosition);
												
							FinalPosition++;

							DataRow TrailerRow = dsFinal.Tables[VEHICLE_COVERAGE_TABLE].NewRow();
							TrailerRow["COV_DESC"]	= "Section 1 - Covered Property Damage - Actual Cash Value";
							TrailerRow["COV_ID"]	= TrailerCovID ;
							TrailerRow["LIMIT"]		= dsTrailer.Tables[0].Rows[cnt]["INSURED_VALUE"].ToString();
							TrailerRow["DEDUCTIBLE"]= dsTrailer.Tables[0].Rows[cnt]["TRAILER_DED"].ToString();
							TrailerRow["VEHICLE_ID"]= VehicleID;
													
							TrailerRow["ACTUAL_RISK_ID"] = dsTrailer.Tables[0].Rows[cnt]["TRAILER_ID"].ToString();
							TrailerRow["ACTUAL_RISK_TYPE"] = "TR";

							dsFinal.Tables[VEHICLE_COVERAGE_TABLE].Rows.InsertAt( TrailerRow ,FinalPosition );
												
							FinalPosition++;
						}
					}
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
				hidACTION_ON_PAYMENT.Value = "165";//New Reserve

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
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[1]);
				else
					hidOldData.Value="";

				ArrayList aList = new ArrayList();
				//PopulateArray(aList,dgPolicyCoverages);
				
				PopulateArray(aList,dgCoverages);
				
				PopulateWaterEquipArray(aList);//Added for Itrack Issue 7663

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
		private void LoadAcntgDropDowns()
		{
			ClsActivityRecovery objActivityRecovery = new ClsActivityRecovery();
			if(objActivityRecovery.LoadAcntgDropDowns(cmbDrAccts,cmbCrAccts,hidACTION_ON_PAYMENT.Value)==-1)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("931");
				lblMessage.Visible = true;
			}
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
					if(dtGrid.ID.ToUpper().Equals(VehicleCoverageGridID.ToUpper()))
					{
						Label lblCOV_DESC = (Label)(dgi.FindControl("lblCOV_DESC"));
						//if(lblCOV_DESC!=null && (lblCOV_DESC.Text.IndexOf(VehicleText)!=-1))
						//Added lblCOV_DESC.Text.IndexOf(TrailerText)!=-1 to prevent entry in database for Trailer Label("Trailer # : ").Earlier Cov_ID set to 0 for Trailer Label.Done on 11 Sept 09
						if(lblCOV_DESC!=null && (lblCOV_DESC.Text.IndexOf(VehicleText)!=-1 || lblCOV_DESC.Text.IndexOf(TrailerText)!=-1))
							continue;
					}
					
					//Model Object
					ClsReserveDetailsInfo objReserveDetailsInfo = new ClsReserveDetailsInfo();
					//Get the Outstanding
					//TextBox txtPRIMARY_EXCESS = (TextBox)(dgi.FindControl("txtPRIMARY_EXCESS"));
					//TextBox txtMCCA_ATTACHMENT_POINT = (TextBox)(dgi.FindControl("txtMCCA_ATTACHMENT_POINT"));
					//DropDownList cmbMCCA_APPLIES = (DropDownList)(dgi.FindControl("cmbMCCA_APPLIES"));
					TextBox txtOutstanding = (TextBox)(dgi.FindControl("txtOUTSTANDING"));
					//TextBox txtATTACHMENT_POINT = (TextBox)(dgi.FindControl("txtATTACHMENT_POINT"));
					TextBox txtRI_RESERVE = (TextBox)(dgi.FindControl("txtRI_RESERVE"));
					DropDownList cmbREINSURANCE_CARRIER = (DropDownList)(dgi.FindControl("cmbREINSURANCE_CARRIER"));					
					Label lblCOV_ID = (Label)(dgi.FindControl("lblCOV_ID"));
					Label lblBOAT_ID = (Label)(dgi.FindControl("lblBOAT_ID"));
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));
					//Added for Itrack Issue 7663 on 19 Aug 2010
					Label lblACTUAL_RISK_ID = (Label)(dgi.FindControl("lblACTUAL_RISK_ID"));
					Label lblACTUAL_RISK_TYPE = (Label)(dgi.FindControl("lblACTUAL_RISK_TYPE"));
//				
					//if(txtPRIMARY_EXCESS.Text.Trim()!="")
					//	objReserveDetailsInfo.PRIMARY_EXCESS = txtPRIMARY_EXCESS.Text.Trim();

					//if(txtMCCA_ATTACHMENT_POINT.Text.Trim()!="")
					//	objReserveDetailsInfo.MCCA_ATTACHMENT_POINT = Convert.ToDouble(txtMCCA_ATTACHMENT_POINT.Text.Trim());

					//if(cmbMCCA_APPLIES.SelectedItem!=null && cmbMCCA_APPLIES.SelectedItem.Value!="")
					//	objReserveDetailsInfo.MCCA_APPLIES = cmbMCCA_APPLIES.SelectedItem.Value;

					//if(txtATTACHMENT_POINT.Text.Trim()!="")
					//	objReserveDetailsInfo.ATTACHMENT_POINT = Convert.ToDouble(txtATTACHMENT_POINT.Text.Trim());

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
					//Added for Itrack Issue 7663 on 19 Aug 2010
					objReserveDetailsInfo.ACTUAL_RISK_ID = int.Parse(lblACTUAL_RISK_ID.Text);
					objReserveDetailsInfo.ACTUAL_RISK_TYPE = lblACTUAL_RISK_TYPE.Text;
					//Done for Itrack Issue 6835 on 16 Dec 09
					if(hidACTIVITY_ID.Value !="1")
						objReserveDetailsInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
					else
						objReserveDetailsInfo.ACTIVITY_ID = 0;					

					if(lblRESERVE_ID.Text.Trim()!="")
						objReserveDetailsInfo.RESERVE_ID = int.Parse(lblRESERVE_ID.Text.Trim());

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
				Label lblLIMIT = (Label)e.Item.FindControl("lblLIMIT");					
				if(lblLIMIT.Text.Equals(SubHeading))					
				{
					e.Item.Cells[0].ColumnSpan=e.Item.Cells.Count;
					for(int j=e.Item.Cells.Count-1;j>0;j--)
						e.Item.Cells.RemoveAt(j);
						
					e.Item.Cells[0].Font.Bold = true;
					e.Item.Cells[0].Attributes.Add("align","left");
				}

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
				
				//TextBox txtMCCA_ATTACHMENT_POINT = (TextBox)(e.Item.FindControl("txtMCCA_ATTACHMENT_POINT"));
				//DropDownList cmbMCCA_APPLIES = (DropDownList)(e.Item.FindControl("cmbMCCA_APPLIES"));
				//TextBox txtPRIMARY_EXCESS = (TextBox)(e.Item.FindControl("txtPRIMARY_EXCESS"));
				//Label lblMCCA_APPLIES = (Label)(e.Item.FindControl("lblMCCA_APPLIES"));
				//Label lblMCCA_ATTACHMENT_POINT = (Label)(e.Item.FindControl("lblMCCA_ATTACHMENT_POINT"));
				//txtPRIMARY_EXCESS.Attributes.Add("onBlur","javascript:ShowHideMCCA(" + txtPRIMARY_EXCESS.ClientID + "," + txtMCCA_ATTACHMENT_POINT.ClientID + "," + lblMCCA_ATTACHMENT_POINT.ClientID + "," + cmbMCCA_APPLIES.ClientID + "," + lblMCCA_APPLIES.ClientID +");");
				//txtMCCA_ATTACHMENT_POINT.Attributes.Add("onBlur","javascript: this.value = formatCurrencyWithCents(this.value);");
				//((TextBox)(e.Item.FindControl("txtATTACHMENT_POINT"))).Attributes.Add("onBlur","javascript: this.value = formatCurrencyWithCents(this.value);");
				
				//Label lblDEDUCTIBLE = (Label)(e.Item.FindControl("lblDEDUCTIBLE"));
				if(lblDeductible.Text.Trim()!="" && lblDeductible.Text.Trim()!="0")
				{
					int CommaPosition = lblDeductible.Text.IndexOf(" ");
					if(CommaPosition!=-1)
					{
						string strDeduct = lblDeductible.Text.Substring(0,CommaPosition);
						TotalDeductible += Convert.ToDouble(strDeduct);
					}
				}

				((RangeValidator)(e.Item.FindControl("rngOUTSTANDING"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				//((RangeValidator)(e.Item.FindControl("rngMCCA_ATTACHMENT_POINT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				//((RangeValidator)(e.Item.FindControl("rngATTACHMENT_POINT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				((RangeValidator)(e.Item.FindControl("rngRI_RESERVE"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				

				((RegularExpressionValidator)(e.Item.FindControl("revOUTSTANDING"))).ValidationExpression = aRegExpDoubleZeroToPositive;	
				((RegularExpressionValidator)(e.Item.FindControl("revOUTSTANDING"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

				((RegularExpressionValidator)(e.Item.FindControl("revRI_RESERVE"))).ValidationExpression = aRegExpDoubleZeroToPositive;	
				((RegularExpressionValidator)(e.Item.FindControl("revRI_RESERVE"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
				

				DropDownList cmbREINSURANCE_CARRIER = (DropDownList)(e.Item.FindControl("cmbREINSURANCE_CARRIER"));
				
				/*if(iLookUp==null || iLookUp.Count<0)
				{
					iLookUp = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLRC",null,"S");
				}				
				cmbREINSURANCE_CARRIER.DataSource = iLookUp;
				cmbREINSURANCE_CARRIER.DataTextField="LookupDesc";
				cmbREINSURANCE_CARRIER.DataValueField="LookupID";
				cmbREINSURANCE_CARRIER.DataBind();*/

				//Added by Shikha Dixit.
				DataTable objDataTable = new DataTable();
				objDataTable = Cms.BusinessLayer.BlCommon.ClsCommon.GetReinsuranceCompanyNames();
				cmbREINSURANCE_CARRIER.DataSource = objDataTable;
				cmbREINSURANCE_CARRIER.DataTextField = "REIN_COMAPANY_NAME";
				cmbREINSURANCE_CARRIER.DataValueField ="REIN_COMAPANY_ID";
				cmbREINSURANCE_CARRIER.DataBind();
				//End of Addition.

				if ( DataBinder.Eval(e.Item.DataItem,"REINSURANCE_CARRIER") != System.DBNull.Value) 
				{
					ClsCommon.SelectValueInDDL(cmbREINSURANCE_CARRIER,DataBinder.Eval(e.Item.DataItem,"REINSURANCE_CARRIER"));
							
				}				

				//if ( DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES") != System.DBNull.Value) 
				//{
				//	ClsCommon.SelectValueInDDL(cmbMCCA_APPLIES,DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES"));
				//}	
				//if(dataGridID.ToUpper().Equals(VehicleCoverageGridID.ToUpper()))
				//{
					
				
				//}	
				

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
		
		private TextBox CreateAmountTextBox(string strTextBoxID,Object objValue,bool ReadonlyReserveTextBox)//Done for Itrack Issue 6635 on 28 Oct 09
		{
			TextBox tbAmount = new TextBox();			
			tbAmount.CssClass = "INPUTCURRENCY";
			tbAmount.MaxLength = 8;
			tbAmount.Attributes.Add("Size","12");
			tbAmount.ID = strTextBoxID;
			//Done for Itrack Issue 6635 on 28 Oct 09
			if(ReadonlyReserveTextBox == false)
			{
				tbAmount.Attributes.Add("ReadOnly","true");
			}
			tbAmount.Attributes.Add("onBlur","javascript:this.value=formatCurrencyWithCents(this.value);");	
			if(objValue!=null && objValue.ToString()!="" && objValue.ToString()!="0")
				tbAmount.Text= Double.Parse(objValue.ToString()).ToString("N");
			//tbAmount.Text = String.Format("{0:,#,###}",Convert.ToInt64(objValue));				
				
			Form1.Controls.Add(tbAmount);
			//Page.Controls.Add(tbAmount);

			return tbAmount;
		}

		private RangeValidator CreateAmountRangeValidator(string strTextBoxID,string strRangeValidatorID)
		{
			RangeValidator rngAmount = new RangeValidator();
			rngAmount.MinimumValue="1";
			rngAmount.MaximumValue="9999999999";
			rngAmount.Type=ValidationDataType.Currency;
			rngAmount.Display = ValidatorDisplay.Dynamic;
			rngAmount.ControlToValidate=strTextBoxID;
			rngAmount.ErrorMessage = "<br/>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done for Itrack Issue 6635 on 29 Oct 09
			rngAmount.ID = strRangeValidatorID;
			return rngAmount;
		}


		//Added for Itrack Issue 7663
		private void LoadOldWatercraftEquipCovgData(DataTable dtTemp)
		{
			SetWaterEquipCoverageID();

			Table objTable = new Table();	
			TableCell objTableCell; 
			TableRow objTableRow;
			objTable.Width = Unit.Percentage(100);
			
			if(dtTemp!=null && dtTemp.Rows.Count>0)
			{
							
				hidWaterEquipCovgID.Value = "";
				hidEquip_ACTUAL_RISK_ID.Value = "";
				hidEquip_ACTUAL_RISK_TYPE.Value = "";
				hidEquipDWELLING_ID.Value = "";

				objTableRow = new TableRow();				
				objTable.Rows.Add(objTableRow);

				objTableCell = new TableCell();
				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.ColumnSpan=4;
				objTableCell.Width = Unit.Percentage(30);
				objTableCell.Text = "Coverage";
				objTableRow.Cells.Add(objTableCell);

				//Done for Itrack Issue 6635 on 28 Oct 09
				objTableCell = new TableCell();
				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Amount";
				objTableCell.Width  = Unit.Percentage(14);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Deductible";
				objTableCell.Width  = Unit.Percentage(14);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Outstanding";
				objTableCell.Width  = Unit.Percentage(14);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "RI Reserve";
				objTableCell.Width  = Unit.Percentage(14);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Reinsurance Carrier";//Done for Itrack Issue 6635 on 28 Oct 09
				objTableCell.Width  = Unit.Percentage(14);
				objTableRow.Cells.Add(objTableCell);

				IList iLookUp = null;
				if(iLookUp==null || iLookUp.Count<0)
				{
					iLookUp = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLRC",null,"S");
				}				
				
				string strPrevCarID="",strTransactionCategory = "";
				int i = 2;
				StringBuilder strTextBoxID = new StringBuilder();
				foreach(DataRow dRow in dtTemp.Rows)
				{
					if (dRow["EQUIP_ID"].ToString() != strPrevCarID)
					{
						objTableRow = new TableRow();							

						//ITEM_DESCRIPTION
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						objTableCell.Text = dRow["EQUIPMENT_DESC"].ToString() + " (" + dRow["EQUIP_DESC"].ToString() + ")" ;
						objTableCell.ColumnSpan=4;
						objTableRow.Cells.Add(objTableCell);

						//INSURING_VALUE
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						if(dRow["INSURED_VALUE"]!=null && dRow["INSURED_VALUE"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["INSURED_VALUE"].ToString()).ToString("N").Replace(".00","");//Done for Itrack Issue 6635 on 23 Dec 09
						objTableRow.Cells.Add(objTableCell);

						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");	
						if(dRow["EQUIP_AMOUNT"]!=null && dRow["EQUIP_AMOUNT"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["EQUIP_AMOUNT"].ToString()).ToString("N").Replace(".00","");
						objTableRow.Cells.Add(objTableCell);

						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						strTextBoxID.Length=0;
						strTextBoxID.Append("txtOUT_STANDING_WATER_EQUIP_" + i.ToString());
						
						if(hidTRANSACTION_CATEGORY.Value.ToUpper() =="GENERAL")
						{				
							strTransactionCategory = "General";
						}				
						else if (hidTRANSACTION_CATEGORY.Value.ToUpper() =="REINSURANCE")
						{
							strTransactionCategory = "Reinsurance";			 
						}
						
						if(strTransactionCategory =="General")
						{
							objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["OUTSTANDING"],true));
						}
						else
						{
							objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["OUTSTANDING"],false));
						}
						objTableCell.Controls.Add(CreateAmountRangeValidator(strTextBoxID.ToString(),strTextBoxID.Replace("txt","rng").ToString()));


						objTableCell.Attributes.Add("onBlur","CalculateTotalWaterEquipOutstanding();");
						objTableRow.Cells.Add(objTableCell);

						objTableCell = new TableCell();
						//objTableCell.Controls.Add(CreateAmountTextBox("txtReserve_" + dRow["ITEM_ID"].ToString()));						
						strTextBoxID.Length=0;
						//strTextBoxID.Append("txtRI_RESERVE_" + dRow["ITEM_ID"].ToString());
						strTextBoxID.Append("txtRI_RESERVE_WATER_EQUIP_" + i.ToString());
						i++;


						//Done for Itrack Issue 6635 on 25 Nov 09
						if(strTransactionCategory =="General")
						{
							objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["RI_RESERVE"],false));
						}
						else
						{
							objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["RI_RESERVE"],true));
						}
						objTableCell.Controls.Add(CreateAmountRangeValidator(strTextBoxID.ToString(),strTextBoxID.Replace("txt","rng").ToString()));
						objTableCell.Attributes.Add("onBlur","CalculateTotalWaterEquipRI_Reserve();");

						objTableCell.Attributes.Add("class","midcolora");
						objTableRow.Cells.Add(objTableCell);
						
						//Done for Itrack Issue 6635 on 26 Oct 09
						DropDownList cmbREINSURANCE_CARRIER = new DropDownList();
						DataTable objDataTable=new DataTable();
						objDataTable= Cms.BusinessLayer.BlCommon.ClsCommon.GetReinsuranceCompanyNames();
						cmbREINSURANCE_CARRIER.ID = "cmbREINSURANCE_CARRIER_" + dRow["EQUIP_ID"].ToString();
						cmbREINSURANCE_CARRIER.DataSource = objDataTable;
						cmbREINSURANCE_CARRIER.DataTextField="REIN_COMAPANY_NAME";//"LookupDesc";
						cmbREINSURANCE_CARRIER.DataValueField="REIN_COMAPANY_ID";//"LookupID";
						cmbREINSURANCE_CARRIER.DataBind();
						ClsCommon.SelectValueInDDL(cmbREINSURANCE_CARRIER,dRow["REINSURANCE_CARRIER"]);

						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						objTableCell.Text="&nbsp;";
						objTableCell.Controls.Add(cmbREINSURANCE_CARRIER);
						objTableRow.Cells.Add(objTableCell);


						objTable.Rows.Add(objTableRow);	
						hidWaterEquipCovgID.Value+="^" + strWATER_EQUIPMENT_COVERAGE_ID;
						
						//hidWaterEquipRsvID.Value+="^" + strWATER_EQUIPMENT_COVERAGE_ID;//Added for Itrack Issue 7633 on 14 July 2010
						
						if(dRow["RESERVE_ID"] != null && dRow["RESERVE_ID"] != DBNull.Value )
						{
							hidWaterEquipRsvID.Value+="^" +  dRow["RESERVE_ID"].ToString();
						}
						

						strPrevCarID=strWATER_EQUIPMENT_COVERAGE_ID;

						//Added for Itrack Issue 7663 on 19 Aug 2010
						hidEquip_ACTUAL_RISK_ID.Value += "^" + dRow["ACTUAL_RISK_ID"].ToString();
						hidEquip_ACTUAL_RISK_TYPE.Value = dRow["ACTUAL_RISK_TYPE"].ToString();
						hidEquipDWELLING_ID.Value +=  "^" + dRow["DWELLING_ID"].ToString();
					}
					objTable.Rows.Add(objTableRow);
					
				}

				plcWatercraftEquipmentCovg.Controls.Add(objTable);
				if(hidWaterEquipCovgID.Value!="0" && hidWaterEquipCovgID.Value!="" && hidWaterEquipCovgID.Value.Length>0)
					hidWaterEquipCovgID.Value = hidWaterEquipCovgID.Value.Substring(1,hidWaterEquipCovgID.Value.Length-1);

				if(hidWaterEquipRsvID.Value!="0" && hidWaterEquipRsvID.Value!="" && hidWaterEquipRsvID.Value.Length>0)
					hidWaterEquipRsvID.Value = hidWaterEquipRsvID.Value.Substring(1,hidWaterEquipRsvID.Value.Length-1);

				//Added for Itrack Issue 7663 on 19 Aug 2010
				if(hidEquip_ACTUAL_RISK_ID.Value!="0" && hidEquip_ACTUAL_RISK_ID.Value!="" && hidEquip_ACTUAL_RISK_ID.Value.Length>0)
					hidEquip_ACTUAL_RISK_ID.Value = hidEquip_ACTUAL_RISK_ID.Value.Substring(1,hidEquip_ACTUAL_RISK_ID.Value.Length-1);

				if(hidEquipDWELLING_ID.Value!="0" && hidEquipDWELLING_ID.Value!="" && hidEquipDWELLING_ID.Value.Length>0)
					hidEquipDWELLING_ID.Value = hidEquipDWELLING_ID.Value.Substring(1,hidEquipDWELLING_ID.Value.Length-1);

			}
			else
			{
				lblMessage.Text = "No records found";
				lblMessage.Visible = true;
			}
		}
		

		//Added for Itrack Issue 7663
		private void PopulateWaterEquipArray(ArrayList aList)
		{
			if(hidWaterEquipCovgID.Value=="" || hidWaterEquipCovgID.Value=="0" || hidWaterEquipCovgID.Value.Length<1)
				return;

			if(hidWaterEquipRsvID.Value=="" || hidWaterEquipRsvID.Value=="0" || hidWaterEquipRsvID.Value.Length<1)
				return;

			string [] ScheduleCovArray = hidWaterEquipCovgID.Value.Split('^');
			if(ScheduleCovArray==null || ScheduleCovArray.Length<1)
				return;

			string [] ScheduleRsvArray = hidWaterEquipRsvID.Value.Split('^');
			if(ScheduleRsvArray==null || ScheduleRsvArray.Length<1)
				return;
			//Added for Itrack Issue 7663 on 19 Aug 2010
			string [] EquipActual_Risk_IdArray = hidEquip_ACTUAL_RISK_ID.Value.Split('^');
			if(EquipActual_Risk_IdArray==null || EquipActual_Risk_IdArray.Length<1)
				return;

			string [] EquipDwelling_IdArray = hidEquipDWELLING_ID.Value.Split('^');
			if(EquipDwelling_IdArray==null || EquipDwelling_IdArray.Length<1)
				return;

			int i=2;
			StringBuilder strTextBoxID = new StringBuilder();
			for(int iCounter=0;iCounter<ScheduleCovArray.Length;iCounter++)
			{
				ClsReserveDetailsInfo objReserveDetailsInfo = new ClsReserveDetailsInfo();
				//string txtOut_Standing_ID = "txtOUT_STANDING_" + ScheduleCovArray[iCounter];
				strTextBoxID.Length=0;
				//Commented by Asfa (22-Oct-2007)
				//strTextBoxID.Append("txtOUT_STANDING_" + ScheduleCovArray[iCounter]);
				strTextBoxID.Append("txtOUT_STANDING_WATER_EQUIP_" + i.ToString());
				
				//TextBox txtOut_Standing = (TextBox)(Form1.FindControl(strTextBoxID.ToString()));

				if (Request.Form[strTextBoxID.ToString()]!= null && Request.Form[strTextBoxID.ToString()].ToString()!="" && hidTRANSACTION_CATEGORY.Value=="General")				
					objReserveDetailsInfo.OUTSTANDING = Convert.ToDouble(Request.Form[strTextBoxID.ToString()].ToString());
				
				strTextBoxID.Length=0;
				//strTextBoxID.Append("txtRI_RESERVE_" + ScheduleCovArray[iCounter]);
				strTextBoxID.Append("txtRI_RESERVE_WATER_EQUIP_" + i.ToString());
				//i++;	

				//TextBox txtRI_RESERVE = (TextBox)(Form1.FindControl(strTextBoxID.ToString()));
				//string txtRI_RESERVE_ID = "txtRI_RESERVE_" + ScheduleCovArray[iCounter];

				if (Request.Form[strTextBoxID.ToString()]!= null && Request.Form[strTextBoxID.ToString()]!="" && hidTRANSACTION_CATEGORY.Value=="Reinsurance")				
					objReserveDetailsInfo.RI_RESERVE = Convert.ToDouble(Request.Form[strTextBoxID.ToString()].ToString());

				//string cmbREINSURANCE_CARRIER_ID = "cmbREINSURANCE_CARRIER_" + ScheduleCovArray[iCounter];
				strTextBoxID.Length=0;
				strTextBoxID.Append("cmbREINSURANCE_CARRIER_" + ScheduleCovArray[iCounter]);
				//DropDownList cmbREINSURANCE_CARRIER = (DropDownList)(Form1.FindControl(strTextBoxID.ToString()));

				if (Request.Form[strTextBoxID.ToString()]!= null && Request.Form[strTextBoxID.ToString()].ToString()!="")				
					objReserveDetailsInfo.REINSURANCE_CARRIER = int.Parse(Request.Form[strTextBoxID.ToString()].ToString());
				
				objReserveDetailsInfo.COVERAGE_ID = int.Parse(ScheduleCovArray[iCounter].ToString());
				objReserveDetailsInfo.RESERVE_ID = int.Parse(ScheduleRsvArray[iCounter].ToString());
				
				objReserveDetailsInfo.IS_ACTIVE="Y";
				objReserveDetailsInfo.CREATED_BY = int.Parse(GetUserId());
				objReserveDetailsInfo.CREATED_DATETIME = System.DateTime.Now;
				objReserveDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
				objReserveDetailsInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
				objReserveDetailsInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
				//Done for Itrack Issue 6835 on 23 Dec 09
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
				//Added by Asfa - (22-Oct-2007)
				objReserveDetailsInfo.TRANSACTION_ID = int.Parse(hidTRANSACTION_ID.Value);
				//Added for Itrack Issue 7663 on 19 Aug 2010
				objReserveDetailsInfo.VEHICLE_ID = int.Parse(EquipDwelling_IdArray[iCounter]);
				objReserveDetailsInfo.ACTUAL_RISK_ID = int.Parse(EquipActual_Risk_IdArray[iCounter]);
				objReserveDetailsInfo.ACTUAL_RISK_TYPE = hidEquip_ACTUAL_RISK_TYPE.Value;

				aList.Add(objReserveDetailsInfo);		
				i++;
			}			
		}

		private void SetWaterEquipCoverageID()
		{
			if(hidSTATE_ID.Value == ((int)enumState.Indiana).ToString())
				strWATER_EQUIPMENT_COVERAGE_ID = "20004";
			else if(hidSTATE_ID.Value == ((int)enumState.Michigan).ToString())
				strWATER_EQUIPMENT_COVERAGE_ID = "20005";
			else if(hidSTATE_ID.Value == ((int)enumState.Wisconsin).ToString())
				strWATER_EQUIPMENT_COVERAGE_ID = "20006";
		}
	}
}
