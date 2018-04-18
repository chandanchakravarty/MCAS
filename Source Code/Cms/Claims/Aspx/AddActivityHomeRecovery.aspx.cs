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
	public class AddActivityHomeRecovery : Cms.Claims.ClaimBase
	{
		
		System.Resources.ResourceManager objResourceMgr;	
		
		//protected double TotalDeductible=0;		
		//IList iLookUp;		
		const string SubHeading = "SubHeading";
		const string DwellingText = "Dwelling # : ";
		const string BoatText = "Boat # : ";
		public static string Section1CoverageGridID = "";
		public static string Section2CoverageGridID = "";	
		public static string ScheduledItemsCoverageGridID = "";
		public static string WatercraftCoverageGridID = "";		
		private double TotalOutstanding=0;
		private double TotalRIReserve=0;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblMessage;
		//protected System.Web.UI.WebControls.Label capACTIVITY_DATE;
		//protected System.Web.UI.WebControls.Label lblACTIVITY_DATE;
		protected System.Web.UI.WebControls.Label lblSection1Coverages;
		protected System.Web.UI.WebControls.Label lblSection2Coverages;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NUMBER;
		protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_DESCRIPTION;		
		protected System.Web.UI.WebControls.TextBox txtTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_RI_RESERVE;		
		protected System.Web.UI.WebControls.Label capCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvCLAIM_DESCRIPTION;
		//protected System.Web.UI.WebControls.RangeValidator rngRECOVERY;
//		protected System.Web.UI.WebControls.Label capACTION_ON_RECOVERY;
//		protected System.Web.UI.WebControls.DropDownList cmbACTION_ON_RECOVERY;
		protected System.Web.UI.WebControls.DropDownList cmbDrAccts;
		protected System.Web.UI.WebControls.DropDownList cmbCrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCrAccts;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCHECK_NUMBER;
		//protected System.Web.UI.WebControls.RangeValidator rngEXPENSES;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSection1CoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSection1CoveragesGridRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSection2CoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSection2CoveragesGridRow;

		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftCoveragesGridRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftCoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trScheduledItemsCoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trScheduledItemsCoveragesGridRow;
		
		protected System.Web.UI.HtmlControls.HtmlTableRow trTotalPayments;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSection1RowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidScheduledItemRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWatercraftRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web .UI.HtmlControls.HtmlInputHidden hidTRANSACTION_CATEGORY;   
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSection2RowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNewReserve;
		protected System.Web.UI.WebControls.DataGrid dgSection1CoveragesGrid;
		protected System.Web.UI.WebControls.DataGrid dgSection2CoveragesGrid;
		//protected System.Web.UI.WebControls.DataGrid dgScheduledItemsCoveragesGrid;				
		const int SECTION1_COVERAGE_TABLE = 0;
		const int SECTION2_COVERAGE_TABLE = 1;
		const int WATERCRAFT_COVERAGE_TABLE = 2;
		const int SCHEDULED_ITEMS_COVERAGE_TABLE = 3;
		protected System.Web.UI.WebControls.Label lblScheduledItemsCoverages;
		protected System.Web.UI.WebControls.Label lblWatercraftCoverages;
		protected System.Web.UI.WebControls.DataGrid dgWatercraftCoveragesGrid;
		
		protected System.Web.UI.WebControls.PlaceHolder plcScheduledCovg;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidScheduledRecrID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidScheduledRsvID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidScheduledCovgID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_RECOVERY;

		//Done for IItrack Issue 7663
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRecVehCoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRecVehCoveragesGridRow;
		protected System.Web.UI.WebControls.DataGrid dgRecVehCoveragesGrid;
		protected System.Web.UI.WebControls.Label lblRecVehCoverages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRecVehRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRecVehCovgID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRecVehRsvID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftEquipmentCoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftEquipmentCoveragesGridRow;
		protected System.Web.UI.WebControls.Label lblWatercraftEquipmentCoverages;
		protected System.Web.UI.WebControls.PlaceHolder plcWatercraftEquipmentCovg;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipItemRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipCovgID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipRsvID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipRecrID;
		//Added for Itrack Issue 7663 on 19 Aug 2010
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSch_ACTUAL_RISK_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSch_ACTUAL_RISK_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquip_ACTUAL_RISK_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquip_ACTUAL_RISK_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSchDWELLING_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquipDWELLING_ID;
		public static string WaterEquipItemsCoverageGridID = "";
		public static string RecVehCoverageGridID = "";
		const int WATER_EQUIPMENT_COVERAGE_TABLE = 4;
		const int REC_VEH_COVERAGE_TABLE = 5;
		const string RecVehText = "Recreational Vehicle # : ";
		private int TrailerCovID;
		const string TrailerText = "Trailer # : ";
		string strWATER_EQUIPMENT_COVERAGE_ID = "";

		protected string LabelPrefix = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="309_13";	
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddActivityHomeRecovery"  ,System.Reflection.Assembly.GetExecutingAssembly());
			GetQueryStringValues();			
			// Put user code to initialize the page here			
			
			string strClaimStatus = GetClaimStatus();
			if(strClaimStatus == CLAIM_STATUS_CLOSED)
				btnSave.Visible=false;
			else
				btnSave.Visible=true;

			if ( !Page.IsPostBack)
			{
				Section1CoverageGridID = dgSection1CoveragesGrid.ID;
				Section2CoverageGridID = dgSection2CoveragesGrid.ID;
				//ScheduledItemsCoverageGridID = dgScheduledItemsCoveragesGrid.ID;
				WatercraftCoverageGridID = dgWatercraftCoveragesGrid.ID;
				RecVehCoverageGridID = dgRecVehCoveragesGrid.ID;//Added for Itrack Issue 7663

				GetActivityDescription();
				SetCaptions();
				SetErrorMessages();	
				btnBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");
				cmbPAYMENT_METHOD.Attributes.Add("onChange","javascript:return cmbPAYMENT_METHOD_Change();");
				GetClaimDetails();		
				LoadDropDowns();				
				//txtRECOVERY.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				//txtEXPENSES.Attributes.Add("onChange","javascript:this.value = formatCurrencyWithCents(this.value);");
				
				//Display an appropriate message for dummy policy
				if((hidCustomerID.Value=="" || hidCustomerID.Value=="0") && (hidPolicyID.Value=="" || hidPolicyID.Value=="0") && (hidPolicyVersionID.Value=="" || hidPolicyVersionID.Value=="0"))
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("805");
					lblMessage.Visible = true;
					trBody.Attributes.Add("style","display:none");
					return;
				}
				//Function CompareAllOutstandingAndPayment() Uncommented For Itrack Issue #5359. 
				btnSave.Attributes.Add("onClick","javascript: return CompareAllOutstandingAndPayment();");
				GetOldData();				
			}		
		}
		private void GetActivityDescription()
		{
			ClsActivity objActivity = new ClsActivity();
			DataSet dsActivity = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			if(dsActivity.Tables[0].Rows[0]["DESCRIPTION"]!=null)
				txtCLAIM_DESCRIPTION.Text = dsActivity.Tables[0].Rows[0]["DESCRIPTION"].ToString();
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
				hidSTATE_ID.Value = dr["STATE_ID"].ToString();//Added for Itrack Issue 7663
			}
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

		private void SetErrorMessages()
		{
			rfvPAYMENT_METHOD.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
			csvCLAIM_DESCRIPTION.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("983");
			revCHECK_NUMBER.ValidationExpression  = aRegExpDoublePositiveNonZeroStartWithZeroForFedId;
			revCHECK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			//rngRECOVERY.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			//rngEXPENSES.ErrorMessage				  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");			
		}

		private void GetOldData()
		{
			try
			{
				DataSet dsOldData = ClsActivityRecovery.GetOldDataForHome(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
				//if(dsOldData!=null && dsOldData.Tables.Count>WATERCRAFT_COVERAGE_TABLE && (dsOldData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count>0 || dsOldData.Tables[SECTION2_COVERAGE_TABLE].Rows.Count>0 || dsOldData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE].Rows.Count>0 || dsOldData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count>0))
				if(dsOldData!=null && dsOldData.Tables.Count>WATERCRAFT_COVERAGE_TABLE && (dsOldData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count>0 || dsOldData.Tables[SECTION2_COVERAGE_TABLE].Rows.Count>0 || dsOldData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count>0))
				{
					ClsActivity objActivity = new ClsActivity();
					//hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[SECTION1_COVERAGE_TABLE]);	
					if(dsOldData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count>0)
						hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[SECTION1_COVERAGE_TABLE]);
					else if(dsOldData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count>0)
						hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[WATERCRAFT_COVERAGE_TABLE]);
					else
						hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[SECTION2_COVERAGE_TABLE]);	
					
					//LoadData(dsOldData.Tables[SECTION1_COVERAGE_TABLE]);
					if(dsOldData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count>0)
						LoadData(dsOldData.Tables[SECTION1_COVERAGE_TABLE]);
					else
						LoadData(dsOldData.Tables[SECTION2_COVERAGE_TABLE]);
					/*DataSet dsClaimActivity = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,"0");
					if(dsClaimActivity!=null && dsClaimActivity.Tables.Count>0 &&  dsClaimActivity.Tables[0].Rows.Count>0)
					{
						if(dsClaimActivity.Tables[0].Rows[0]["RECOVERY"]!=null && dsClaimActivity.Tables[0].Rows[0]["RECOVERY"].ToString()!="")
							txtRECOVERY.Text=String.Format("{0:,#,###}",Convert.ToInt64(dsClaimActivity.Tables[0].Rows[0]["RECOVERY"]));
						if(dsClaimActivity.Tables[0].Rows[0]["EXPENSES"]!=null && dsClaimActivity.Tables[0].Rows[0]["EXPENSES"].ToString()!="")
							txtEXPENSES.Text=String.Format("{0:,#,###}",Convert.ToInt64(dsClaimActivity.Tables[0].Rows[0]["EXPENSES"]));					
					}*/
				}
				else			
					hidOldData.Value = "";
				BindGrid(dsOldData);
				
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally{}
				

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
			//capACTIVITY_DATE.Text 		=		objResourceMgr.GetString("lblACTIVITY_DATE");			
			//capTOTAL_OUTSTANDING.Text	=		objResourceMgr.GetString("txtTOTAL_OUTSTANDING");
			//capTOTAL_PAYMENT.Text 		=		objResourceMgr.GetString("txtTOTAL_PAYMENT");
			capCLAIM_DESCRIPTION.Text 		=		objResourceMgr.GetString("txtCLAIM_DESCRIPTION");
			//capPAYEE.Text 				=		objResourceMgr.GetString("cmbPAYEE");
//			capACTION_ON_RECOVERY.Text 	=		objResourceMgr.GetString("cmbACTION_ON_RECOVERY");			
		}
		#endregion

		public DataTable AddDwellingRow(DataTable dtTemp,string strText)
		{
			string curDwelling,prevDwelling;
			TableRow row = new TableRow();
			int i=0,TableRowCount;
			curDwelling = prevDwelling = "";						
			if(dtTemp!=null && dtTemp.Rows.Count>0)
			{
				TableRowCount = dtTemp.Rows.Count;
				for(i=0;i<TableRowCount ;i++)
				{
					curDwelling = dtTemp.Rows[i]["DWELLING_ID"].ToString();
					if(curDwelling !=  prevDwelling)
					{
						prevDwelling = curDwelling;
						DataRow dr = dtTemp.NewRow();
						dr["COV_DESC"]= strText + dtTemp.Rows[i]["DWELLING"];					
						dr["LIMIT"] = SubHeading;
						dtTemp.Rows.InsertAt(dr,i);		
						++TableRowCount;
					}				
				}
			}

			return dtTemp;
		}

		
		
		private void BindGrid(DataSet dsData)
		{
			//if(dsData==null || dsData.Tables.Count<WATERCRAFT_COVERAGE_TABLE || (dsData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count<1 && dsData.Tables[SECTION2_COVERAGE_TABLE].Rows.Count<1 && dsData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE].Rows.Count<1 && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count<1))
			if(dsData==null || dsData.Tables.Count<WATERCRAFT_COVERAGE_TABLE || (dsData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count<1 && dsData.Tables[SECTION2_COVERAGE_TABLE].Rows.Count<1 && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count<1 ))
			{
				ClsReserveDetails objReserveDetails = ClsReserveDetails.CreateReserveObject(hidLOB_ID.Value);
				dsData = objReserveDetails.GetOldData(hidCLAIM_ID.Value);
				if(dsData!=null && dsData.Tables.Count>0)
				{
					//Add columns for Payment Amount and Action on Payment as they are not present at reserve table..
					for(int i=0;i<dsData.Tables.Count;i++)
					{
						DataColumn dtCol = new DataColumn("AMOUNT");
						dsData.Tables[i].Columns.Add(dtCol);
						dtCol = new DataColumn("ACTION_ON_RECOVERY");
						dsData.Tables[i].Columns.Add(dtCol);
						dtCol = new DataColumn("RECOVERY_ID");
						dsData.Tables[i].Columns.Add(dtCol);				
					}
				}
			}

			if(dsData!=null)
			{
				if(dsData.Tables.Count>SECTION1_COVERAGE_TABLE && dsData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count>0)
				{
					dgSection1CoveragesGrid.DataSource = AddDwellingRow(dsData.Tables[SECTION1_COVERAGE_TABLE],DwellingText);
					dgSection1CoveragesGrid.DataBind();
					hidSection1RowCount.Value = dgSection1CoveragesGrid.Items.Count.ToString();
				}
				else
				{
					dgSection1CoveragesGrid.Visible = false;
					trSection1CoveragesGridRow.Attributes.Add("style","display:none");
					trSection1CoveragesRow.Attributes.Add("style","display:none");
				}
				if(dsData.Tables.Count>SECTION2_COVERAGE_TABLE  && hidCustomerID.Value!="" && hidCustomerID.Value!="0" && hidPolicyID.Value!="" && hidPolicyID.Value!="0" && hidPolicyVersionID.Value!="" && hidPolicyVersionID.Value!="0" && dsData.Tables[SECTION2_COVERAGE_TABLE].Rows.Count>0)
				{
					dgSection2CoveragesGrid.DataSource = AddDwellingRow(dsData.Tables[SECTION2_COVERAGE_TABLE],DwellingText);
					dgSection2CoveragesGrid.DataBind();
					hidSection2RowCount.Value = dgSection2CoveragesGrid.Items.Count.ToString();
				}
				else
				{
					dgSection2CoveragesGrid.Visible = false;
					trSection2CoveragesGridRow.Attributes.Add("style","display:none");
					trSection2CoveragesRow.Attributes.Add("style","display:none");
				}
//				if(dsData.Tables.Count>SCHEDULED_ITEMS_COVERAGE_TABLE && dsData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE].Rows.Count>0)
//				{
//					dgScheduledItemsCoveragesGrid.DataSource = dsData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE];
//					dgScheduledItemsCoveragesGrid.DataBind();
//					hidScheduledItemRowCount.Value = dgScheduledItemsCoveragesGrid.Items.Count.ToString();
//				}
//				else
//				{
//					dgScheduledItemsCoveragesGrid.Visible = false;
//					trScheduledItemsCoveragesGridRow.Attributes.Add("style","display:none");
//					trScheduledItemsCoveragesRow.Attributes.Add("style","display:none");
//				}
				if(dsData.Tables.Count>WATERCRAFT_COVERAGE_TABLE && hidCustomerID.Value!="" && hidCustomerID.Value!="0" && hidPolicyID.Value!="" && hidPolicyID.Value!="0" && hidPolicyVersionID.Value!="" && hidPolicyVersionID.Value!="0" && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count>0)
				{
					//Done for Itrack Issue 7663
					// dgWatercraftCoveragesGrid.DataSource = AddDwellingRow(dsData.Tables[WATERCRAFT_COVERAGE_TABLE],BoatText);
					// dgWatercraftCoveragesGrid.DataBind();
					// hidWatercraftRowCount.Value = dgWatercraftCoveragesGrid.Items.Count.ToString();

					SetTrailerCoverageID();
					string curVehicle,prevVehicle;
					int position=0 , BoatCounter = 1; 
					curVehicle = prevVehicle = "";

					int RowCount = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count;
					DataSet dsFinal = dsData.Clone();
					int FinalPosition = 0 ; 
					bool AddRow = true; 

					while(position < RowCount)
					{
						AddRow = true;
						curVehicle = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["DWELLING_ID"].ToString();

						if(prevVehicle == "")
						{
							DataRow newRow = dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].NewRow();
							newRow["COV_DESC"]= BoatText + dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["DWELLING"];							
							newRow["LIMIT"] = SubHeading;
							dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.InsertAt(newRow,FinalPosition);
							FinalPosition++;
							prevVehicle = curVehicle ; 
						}

						if(prevVehicle != curVehicle)
						{

							DataRow newRow = dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].NewRow();
							newRow["COV_DESC"]= BoatText + dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["DWELLING"];							
							newRow["LIMIT"] = SubHeading;
							dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.InsertAt(newRow,FinalPosition);
							prevVehicle = curVehicle ; 
							FinalPosition++;
							BoatCounter = 1; 
						}

						//ACTUAL_RISK_TYPE
						if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["ACTUAL_RISK_TYPE"].ToString() == "TR")
						{
							string ActualRiskId = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["ACTUAL_RISK_ID"].ToString();
							
							DataSet dsTrailer		= ClsReserveDetails.GetTrailerDetails(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,	curVehicle,ActualRiskId );
							DataRow HeaderRow		= dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].NewRow();
							HeaderRow["COV_DESC"]	= TrailerText + " #: " + BoatCounter.ToString() ;						
							HeaderRow["LIMIT"]		= SubHeading;
							
							DataRow TrailerRow = dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].NewRow();

							TrailerRow["COV_DESC"]	= "Section 1 - Covered Property Damage - Actual Cash Value";
							TrailerRow["COV_ID"]	= TrailerCovID ;
							TrailerRow["LIMIT"]		= dsTrailer.Tables[0].Rows[0]["INSURED_VALUE"].ToString();
							TrailerRow["DEDUCTIBLE"]= dsTrailer.Tables[0].Rows[0]["TRAILER_DED"].ToString();
							TrailerRow["DWELLING_ID"]= curVehicle ;
							TrailerRow["ACTUAL_RISK_ID"] = ActualRiskId; 
							TrailerRow["ACTUAL_RISK_TYPE"] = "TR";
							TrailerRow["OUTSTANDING"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["OUTSTANDING"];
							TrailerRow["AMOUNT"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["AMOUNT"];


							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["PRIMARY_EXCESS"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["PRIMARY_EXCESS"] != DBNull.Value)
							{
								TrailerRow["PRIMARY_EXCESS"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["PRIMARY_EXCESS"];
							}

							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["ATTACHMENT_POINT"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["ATTACHMENT_POINT"] != DBNull.Value)
							{
								TrailerRow["ATTACHMENT_POINT"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["ATTACHMENT_POINT"];
							}

							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RI_RESERVE"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RI_RESERVE"] != DBNull.Value)
							{
								TrailerRow["RI_RESERVE"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RI_RESERVE"]; 
							}

							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RESERVE_ID"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RESERVE_ID"] != DBNull.Value)
							{
								TrailerRow["RESERVE_ID"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RESERVE_ID"];
							}

							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["REINSURANCE_CARRIER"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["REINSURANCE_CARRIER"] != DBNull.Value)
							{
								TrailerRow["REINSURANCE_CARRIER"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["REINSURANCE_CARRIER"];
							}

							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["REINSURANCECARRIER"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["REINSURANCECARRIER"] != DBNull.Value)
							{
								TrailerRow["REINSURANCECARRIER"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["REINSURANCECARRIER"];
							}

							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"] != DBNull.Value)
							{
								TrailerRow["MCCA_ATTACHMENT_POINT"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"];
							}

							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["MCCA_APPLIES"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["MCCA_APPLIES"] != DBNull.Value)
							{
								TrailerRow["MCCA_APPLIES"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["MCCA_APPLIES"];
							}

							if(dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RECOVERY_ID"] != null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RECOVERY_ID"] != DBNull.Value)
							{
								TrailerRow["RECOVERY_ID"] = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position]["RECOVERY_ID"];
							}


							
							dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.InsertAt(HeaderRow, FinalPosition );
							FinalPosition++;

							dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.InsertAt(TrailerRow , FinalPosition );

							BoatCounter++;
							AddRow = false;
						}

						if(AddRow)
						{
							DataRow drExisting = dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows[position];
							
							dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].ImportRow(drExisting);

						}
						position++;
						FinalPosition++;

					}

					hidWatercraftRowCount.Value = dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count.ToString();
					dgWatercraftCoveragesGrid.DataSource = dsFinal.Tables[WATERCRAFT_COVERAGE_TABLE];
					dgWatercraftCoveragesGrid.DataBind();
				}
				else
				{
					dgWatercraftCoveragesGrid.Visible = false;
					trWatercraftCoveragesGridRow.Attributes.Add("style","display:none");
					trWatercraftCoveragesRow.Attributes.Add("style","display:none");
				}
				//Done for Itrack Issue 7784 on 17 Aug 2010
				if(dsData.Tables.Count>SECTION1_COVERAGE_TABLE && dsData.Tables[SECTION1_COVERAGE_TABLE]!=null && dsData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count>0 && dsData.Tables.Count>SCHEDULED_ITEMS_COVERAGE_TABLE && dsData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE]!=null && dsData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE].Rows.Count>0)
				{
					//dgScheduledItemsCoveragesGrid.DataSource = dsData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE];
					//dgScheduledItemsCoveragesGrid.DataBind();
					//hidScheduledItemRowCount.Value = dgScheduledItemsCoveragesGrid.Items.Count.ToString();
					hidScheduledItemRowCount.Value = dsData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE].Rows.Count.ToString();
					LoadOldScheduledCovgData(dsData.Tables[SCHEDULED_ITEMS_COVERAGE_TABLE]);
				}
				else
				{
					//dgScheduledItemsCoveragesGrid.Visible = false;
					//plcScheduledCovg.Visible = false;
					trScheduledItemsCoveragesGridRow.Attributes.Add("style","display:none");
					trScheduledItemsCoveragesRow.Attributes.Add("style","display:none");
				}	
				//Added for Itrack Issue 7663
				//Done for Itrack Issue 7784 on 17 Aug 2010
				if(dsData.Tables.Count>WATERCRAFT_COVERAGE_TABLE && dsData.Tables[WATERCRAFT_COVERAGE_TABLE]!=null && dsData.Tables[WATERCRAFT_COVERAGE_TABLE].Rows.Count>0 && dsData.Tables.Count>WATER_EQUIPMENT_COVERAGE_TABLE && dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE]!=null && dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE].Rows.Count>0)
				{
					hidWaterEquipItemRowCount.Value = dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE].Rows.Count.ToString();
					LoadWaterEquipCovgData(dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE]);
				}
				else
				{
					trWatercraftEquipmentCoveragesGridRow.Attributes.Add("style","display:none");
					trWatercraftEquipmentCoveragesRow.Attributes.Add("style","display:none");
				}
				//Done for Itrack Issue 7784 on 17 Aug 2010
				if(dsData.Tables.Count>SECTION1_COVERAGE_TABLE && dsData.Tables[SECTION1_COVERAGE_TABLE]!=null && dsData.Tables[SECTION1_COVERAGE_TABLE].Rows.Count>0 && dsData.Tables.Count>REC_VEH_COVERAGE_TABLE && dsData.Tables[REC_VEH_COVERAGE_TABLE]!=null && dsData.Tables[REC_VEH_COVERAGE_TABLE].Rows.Count>0)
				{
					dgRecVehCoveragesGrid.DataSource = AddDwellingRow(dsData.Tables[REC_VEH_COVERAGE_TABLE],RecVehText);
					dgRecVehCoveragesGrid.DataBind();
					hidRecVehRowCount.Value = dgRecVehCoveragesGrid.Items.Count.ToString();
				}
				else
				{
					dgRecVehCoveragesGrid.Visible = false;
					trRecVehCoveragesGridRow.Attributes.Add("style","display:none");
					trRecVehCoveragesRow.Attributes.Add("style","display:none");
				}

				if(TotalOutstanding!=0 && TotalOutstanding!=0.0)
					txtTOTAL_OUTSTANDING.Text=Double.Parse(TotalOutstanding.ToString()).ToString("N");
				if(TotalRIReserve!=0 && TotalRIReserve!=0.0)
					txtTOTAL_RI_RESERVE.Text=Double.Parse(TotalRIReserve.ToString()).ToString("N");

//				if(TotalOutstanding!=0 && TotalOutstanding!=0.0)
//					txtTOTAL_OUTSTANDING.Text=String.Format("{0:,#,###}",Convert.ToInt64(TotalOutstanding.ToString()));
//				if(TotalRIReserve!=0 && TotalRIReserve!=0.0)
//					txtTOTAL_RI_RESERVE.Text=String.Format("{0:,#,###}",Convert.ToInt64(TotalRIReserve.ToString()));
			
				if(dgSection1CoveragesGrid.Visible==false &&  dgSection2CoveragesGrid.Visible == false && dgWatercraftCoveragesGrid.Visible == false)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("804");
					btnSave.Visible = false;
					lblMessage.Visible = true;
					trBody.Attributes.Add("style","display:none");
				}
			}
			else
			{
				trBody.Attributes.Add("style","display:none");
				btnSave.Visible = false;
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("804");
				lblMessage.Visible = true;
			}
		}
		
		//Function added For Itrack Issue #5359.
		private void set_label()
		{
			hidTRANSACTION_CATEGORY.Value = ClsReserveDetails.GetTransactionCategory(hidACTION_ON_RECOVERY.Value);  			
			if(hidTRANSACTION_CATEGORY.Value !=null && hidTRANSACTION_CATEGORY.Value !="" && hidTRANSACTION_CATEGORY.Value.ToUpper() == "GENERAL")
			{
				LabelPrefix = "_lblOutstanding";
			}
			else if( hidTRANSACTION_CATEGORY.Value.ToUpper().Trim() == "REINSURANCE")
			{   
				LabelPrefix = "_lblRI_Reserve";
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
			this.dgSection1CoveragesGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgSection1CoveragesGrid_ItemDataBound);
			this.dgSection2CoveragesGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgSection2CoveragesGrid_ItemDataBound);
			//this.dgScheduledItemsCoveragesGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgScheduledItemsCoveragesGrid_ItemDataBound);
			this.dgWatercraftCoveragesGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgWatercraftCoveragesGrid_ItemDataBound);
			this.dgRecVehCoveragesGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgRecVehCoveragesGrid_ItemDataBound);//Added for Itrack Issue 7663
			
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

			if(Request["ACTION_ON_RECOVERY"]!=null && Request["ACTION_ON_RECOVERY"].ToString()!="")
				hidACTION_ON_RECOVERY.Value = Request["ACTION_ON_RECOVERY"].ToString();
			else
				hidACTION_ON_RECOVERY.Value = "0";

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

		}		
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				int RECOVERY_ACTION;				
				ClsActivityRecovery objActivityRecovery = new ClsActivityRecovery();				
				ArrayList PaymentArrayList = new ArrayList();
				PopulateArray(PaymentArrayList,dgSection1CoveragesGrid);
				PopulateArray(PaymentArrayList,dgSection2CoveragesGrid);
				//PopulateArray(PaymentArrayList,dgScheduledItemsCoveragesGrid);
				PopulateArray(PaymentArrayList,dgWatercraftCoveragesGrid);
				PopulateScheduleArray(PaymentArrayList);
				//Added for Itrack Issue 7663
				PopulateArray(PaymentArrayList,dgRecVehCoveragesGrid);
				PopulateWaterEquipArray(PaymentArrayList);

				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					intRetVal = objActivityRecovery.Add(PaymentArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");						
					//RegisterStartupScript("ReLoadTab","<script>GoBack('RecoveryTabs.aspx');</script>");			
				}
				else
				{
					intRetVal = objActivityRecovery.Update(PaymentArrayList);
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
		private void SetStartUpScript(string FunctionName)
		{
			ClientScript.RegisterStartupScript(this.GetType(),"ReLoadClaimsTab","<script>" + FunctionName + "();</script>");			
		}

		private void PopulateArray(ArrayList aPaymentList,DataGrid dtGrid)
		{
			foreach(DataGridItem dgi in dtGrid.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					if(!(dtGrid.ID.ToUpper().Equals(ScheduledItemsCoverageGridID.ToUpper()) || dtGrid.ID.ToUpper().Equals(WaterEquipItemsCoverageGridID.ToUpper())))
					{
						Label lblCOV_DESC = (Label)(dgi.FindControl("lblCOV_DESC"));
						if(lblCOV_DESC!=null && (lblCOV_DESC.Text.IndexOf(DwellingText)!=-1 || lblCOV_DESC.Text.IndexOf(BoatText)!=-1  || lblCOV_DESC.Text.IndexOf(TrailerText)!=-1) || lblCOV_DESC.Text.IndexOf(RecVehText)!=-1)
							continue;
					}
					//Model Object
					ClsActivityRecoveryInfo objActivityRecoveryInfo = new	 ClsActivityRecoveryInfo();
					
					TextBox txtAMOUNT = (TextBox)(dgi.FindControl("txtAMOUNT"));
					
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));
					Label lblRECOVERY_ID = (Label)(dgi.FindControl("lblRECOVERY_ID"));
					Label lblVEHICLE_ID = (Label)(dgi.FindControl("lblDWELLING_ID"));
					//Added for Itrack Issue 7663 on 19 Aug 2010
					Label lblACTUAL_RISK_ID = (Label)(dgi.FindControl("lblACTUAL_RISK_ID"));
					Label lblACTUAL_RISK_TYPE = (Label)(dgi.FindControl("lblACTUAL_RISK_TYPE"));
					
					if(hidACTION_ON_RECOVERY.Value!="")
						objActivityRecoveryInfo.ACTION_ON_RECOVERY = int.Parse(hidACTION_ON_RECOVERY.Value);

					objActivityRecoveryInfo.IS_ACTIVE="Y";
					objActivityRecoveryInfo.CREATED_BY = int.Parse(GetUserId());
					objActivityRecoveryInfo.CREATED_DATETIME = System.DateTime.Now;
					objActivityRecoveryInfo.MODIFIED_BY = int.Parse(GetUserId());
					objActivityRecoveryInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
					objActivityRecoveryInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					objActivityRecoveryInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);	
					//Added for Itrack Issue 7663 on 19 Aug 2010
					objActivityRecoveryInfo.ACTUAL_RISK_ID = int.Parse(lblACTUAL_RISK_ID.Text);
					objActivityRecoveryInfo.ACTUAL_RISK_TYPE = lblACTUAL_RISK_TYPE.Text;

					if(lblRESERVE_ID.Text.Trim()!="")
						objActivityRecoveryInfo.RESERVE_ID = int.Parse(lblRESERVE_ID.Text.Trim());
					if(txtAMOUNT.Text.Trim()!="")
						objActivityRecoveryInfo.AMOUNT = Convert.ToDouble(txtAMOUNT.Text.Trim());
					if(lblRECOVERY_ID.Text.Trim()!="")
						objActivityRecoveryInfo.RECOVERY_ID = int.Parse(lblRECOVERY_ID.Text.Trim());					
					if(lblVEHICLE_ID!=null && lblVEHICLE_ID.Text.Trim()!="")
						objActivityRecoveryInfo.VEHICLE_ID = int.Parse(lblVEHICLE_ID.Text.Trim());

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
		private void PopulateScheduleArray(ArrayList aList)
		{
			if(hidScheduledCovgID.Value=="" || hidScheduledCovgID.Value=="0" || hidScheduledCovgID.Value.Length<1)
				return;

			if(hidScheduledRsvID.Value=="" || hidScheduledRsvID.Value=="0" || hidScheduledRsvID.Value.Length<1)
				return;

			string [] ScheduleCovArray = hidScheduledCovgID.Value.Split('^');
			if(ScheduleCovArray==null || ScheduleCovArray.Length<1)
				return;

			string [] ScheduleRsvArray = hidScheduledRsvID.Value.Split('^');
			string [] ScheduleRecrArray = hidScheduledRecrID.Value.Split('^');
			//Added for Itrack Issue 7663 on 19 Aug 2010
			string [] ScheduleActual_Risk_IdArray = hidSch_ACTUAL_RISK_ID.Value.Split('^');
			string [] ScheduleDwelling_IdArray = hidSchDWELLING_ID.Value.Split('^');

			if(ScheduleRsvArray==null || ScheduleRsvArray.Length<1)
				return;

			StringBuilder strTextBoxID = new StringBuilder();
			int i=2;
			for(int iCounter=0;iCounter<ScheduleCovArray.Length;iCounter++)
			{
				ClsActivityRecoveryInfo objActivityRecoveryInfo = new ClsActivityRecoveryInfo();
				//string txtOut_Standing_ID = "txtOUT_STANDING_" + ScheduleCovArray[iCounter];
				strTextBoxID.Length=0;
				//strTextBoxID.Append("txtAMOUNT_" + ScheduleCovArray[iCounter]);
				strTextBoxID.Append("txtAMOUNT_" + i.ToString());
				i++;
				//TextBox txtOut_Standing = (TextBox)(Form1.FindControl(strTextBoxID.ToString()));

				if (Request.Form[strTextBoxID.ToString()]!= null && Request.Form[strTextBoxID.ToString()].ToString()!="")				
					objActivityRecoveryInfo.AMOUNT = Convert.ToDouble(Request.Form[strTextBoxID.ToString()].ToString());
				
				if(hidACTION_ON_RECOVERY.Value!="")
					objActivityRecoveryInfo.ACTION_ON_RECOVERY = int.Parse(hidACTION_ON_RECOVERY.Value);


				
				objActivityRecoveryInfo.RESERVE_ID = int.Parse(ScheduleRsvArray[iCounter].ToString());
				if(ScheduleRecrArray!=null && ScheduleRecrArray[iCounter]!=null && ScheduleRecrArray[iCounter]!="")
					objActivityRecoveryInfo.RECOVERY_ID = int.Parse(ScheduleRecrArray[iCounter].ToString());
				
				objActivityRecoveryInfo.IS_ACTIVE="Y";
				objActivityRecoveryInfo.CREATED_BY = int.Parse(GetUserId());
				objActivityRecoveryInfo.CREATED_DATETIME = System.DateTime.Now;
				objActivityRecoveryInfo.MODIFIED_BY = int.Parse(GetUserId());
				objActivityRecoveryInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
				objActivityRecoveryInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
				objActivityRecoveryInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
				//Added for Itrack Issue 7663 on 19 Aug 2010
				objActivityRecoveryInfo.VEHICLE_ID = int.Parse(ScheduleDwelling_IdArray[iCounter]);
				objActivityRecoveryInfo.ACTUAL_RISK_ID = int.Parse(ScheduleActual_Risk_IdArray[iCounter]);
				objActivityRecoveryInfo.ACTUAL_RISK_TYPE = hidSch_ACTUAL_RISK_TYPE.Value;

				//Done for Itrack Issue 6635 on 29 Oct 09
				if(cmbDrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
					objActivityRecoveryInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedValue);
				
				if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
					objActivityRecoveryInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedValue);

				
				aList.Add(objActivityRecoveryInfo);				
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
			string [] ScheduleRecrArray = hidWaterEquipRecrID.Value.Split('^');
			//Added for Itrack Issue 7663 on 19 Aug 2010
			string [] EquipActual_Risk_IdArray = hidEquip_ACTUAL_RISK_ID.Value.Split('^');
			string [] EquipDwelling_IdArray = hidEquipDWELLING_ID.Value.Split('^');

			if(ScheduleRsvArray==null || ScheduleRsvArray.Length<1)
				return;

			StringBuilder strTextBoxID = new StringBuilder();
			int i=2;
			for(int iCounter=0;iCounter<ScheduleCovArray.Length;iCounter++)
			{
				ClsActivityRecoveryInfo objActivityRecoveryInfo = new ClsActivityRecoveryInfo();
				//string txtOut_Standing_ID = "txtOUT_STANDING_" + ScheduleCovArray[iCounter];
				strTextBoxID.Length=0;
				//strTextBoxID.Append("txtAMOUNT_" + ScheduleCovArray[iCounter]);
				strTextBoxID.Append("txtWATER_EQUIP_RECOVERY_AMOUNT_" + i.ToString());
				i++;
				//TextBox txtOut_Standing = (TextBox)(Form1.FindControl(strTextBoxID.ToString()));

				if (Request.Form[strTextBoxID.ToString()]!= null && Request.Form[strTextBoxID.ToString()].ToString()!="")				
					objActivityRecoveryInfo.AMOUNT = Convert.ToDouble(Request.Form[strTextBoxID.ToString()].ToString());
				
				if(hidACTION_ON_RECOVERY.Value!="")
					objActivityRecoveryInfo.ACTION_ON_RECOVERY = int.Parse(hidACTION_ON_RECOVERY.Value);


				
				objActivityRecoveryInfo.RESERVE_ID = int.Parse(ScheduleRsvArray[iCounter].ToString());
				if(ScheduleRecrArray!=null && ScheduleRecrArray[iCounter]!=null && ScheduleRecrArray[iCounter]!="")
					objActivityRecoveryInfo.RECOVERY_ID = int.Parse(ScheduleRecrArray[iCounter].ToString());
				
				objActivityRecoveryInfo.IS_ACTIVE="Y";
				objActivityRecoveryInfo.CREATED_BY = int.Parse(GetUserId());
				objActivityRecoveryInfo.CREATED_DATETIME = System.DateTime.Now;
				objActivityRecoveryInfo.MODIFIED_BY = int.Parse(GetUserId());
				objActivityRecoveryInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
				objActivityRecoveryInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
				objActivityRecoveryInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);	
				//Added for Itrack Issue 7663 on 19 Aug 2010
				objActivityRecoveryInfo.VEHICLE_ID = int.Parse(EquipDwelling_IdArray[iCounter]);
				objActivityRecoveryInfo.ACTUAL_RISK_ID = int.Parse(EquipActual_Risk_IdArray[iCounter]);
				objActivityRecoveryInfo.ACTUAL_RISK_TYPE = hidEquip_ACTUAL_RISK_TYPE.Value;

				//Done for Itrack Issue 6635 on 29 Oct 09
				if(cmbDrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
					objActivityRecoveryInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedValue);
				
				if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
					objActivityRecoveryInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedValue);

				
				aList.Add(objActivityRecoveryInfo);				
			}			
		}

		private TextBox CreateAmountTextBox(string strTextBoxID,Object objValue,string strRI_RESERVE,bool EnableTextBox)//Done for Itrack Issue 6635 on 29 Oct 09
		{
			TextBox tbAmount = new TextBox();			
			tbAmount.CssClass = "INPUTCURRENCY";
			tbAmount.MaxLength = 8;
			tbAmount.Attributes.Add("Size","15");
			tbAmount.ID = strTextBoxID;			
			//Done for Itrack Issue 6635 on 29 Oct 09
			//tbAmount.Attributes.Add("onBlur","javascript:this.value=formatCurrencyWithCents(this.value);");	
			tbAmount.Attributes.Add("onBlur","javascript:this.value=formatCurrencyWithCents(this.value);CalculateTotalPayment();CompareOutstandingAndPayment('" + strRI_RESERVE + "','" + tbAmount.ID + "');");
			if(objValue!=null && objValue.ToString()!="" && objValue.ToString()!="0")
				tbAmount.Text = Double.Parse(objValue.ToString()).ToString("N");
				//tbAmount.Text = String.Format("{0:,#,###}",Convert.ToInt64(objValue));		
			tbAmount.Enabled = EnableTextBox;	
			Form1.Controls.Add(tbAmount);
			//Page.Controls.Add(tbAmount);

			return tbAmount;
		}

		private RangeValidator CreateAmountRangeValidator(string strTextBoxID,string strRangeValidatorID)
		{
			RangeValidator rngAmount = new RangeValidator();
			rngAmount.MinimumValue="0";//Done for Itrack Issue 7702(Note 5)
			rngAmount.MaximumValue="9999999999";
			rngAmount.Type=ValidationDataType.Currency;
			rngAmount.Display = ValidatorDisplay.Dynamic;
			rngAmount.ControlToValidate=strTextBoxID;
			rngAmount.ErrorMessage = "<br/>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done for Itrack Issue 6635 on 29 Oct 09
			rngAmount.ID = strRangeValidatorID;
			return rngAmount;
		}

		private void LoadOldScheduledCovgData(DataTable dtTemp)
		{
			//ClsClaimsNotification objClaimsNotification = new ClsClaimsNotification();
			/*ClsReserveDetails objReserveDetails = new ClsReserveDetails();
			hidCLAIM_ID.Value = "570";
			DataTable dtTemp = objReserveDetails.GetSchRecords(hidCLAIM_ID.Value);*/
			Table objTable = new Table();	
			TableCell objTableCell; 
			TableRow objTableRow;
			objTable.Width = Unit.Percentage(100);
			bool EnableRecoveryTextBox = false;
			
			if(dtTemp!=null && dtTemp.Rows.Count>0)
			{
							
				hidScheduledCovgID.Value = "";
				objTableRow = new TableRow();
				objTableRow.Attributes.Add("class","headereffectWebGrid");
				objTable.Rows.Add(objTableRow);

				objTableCell = new TableCell();
//				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.ColumnSpan=4;
				objTableCell.Width = Unit.Percentage(31);
				objTableCell.Text = "Coverage";
				objTableRow.Cells.Add(objTableCell);

				//Done for Itrack Issue 6635 on 28 Oct 09
				objTableCell = new TableCell();
				//objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Amount";
				objTableCell.Width  = Unit.Percentage(14);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
//				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Deductible";
				objTableCell.Width  = Unit.Percentage(15);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
//				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Outstanding";
				objTableCell.Width  = Unit.Percentage(9);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
//				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "RI Reserve";
				objTableCell.Width  = Unit.Percentage(9);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
//				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Reinsurance Carrier";//Done for Itrack Issue 6635 on 28 Oct 09
				objTableCell.Width  = Unit.Percentage(9);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
//				objTableCell.Attributes.Add("class","headereffectWebGrid");
				objTableCell.Text = "Recovery Amount";
				objTableCell.Width  = Unit.Percentage(21);
				objTableRow.Cells.Add(objTableCell);

				//				IList iLookUp = null;
				//				if(iLookUp==null || iLookUp.Count<0)
				//				{
				//					iLookUp = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLRC");
				//				}				
				
				string strPrevCarID="";
				StringBuilder strTextBoxID = new StringBuilder();
				hidScheduledRecrID.Value = "";
				hidScheduledCovgID.Value = "";
				hidScheduledRsvID.Value = "";
				hidSch_ACTUAL_RISK_ID.Value = "";
				hidSch_ACTUAL_RISK_TYPE.Value = "";
				hidSchDWELLING_ID.Value = "";

				int i=2;
				foreach(DataRow dRow in dtTemp.Rows)
				{
					if (dRow["ITEM_ID"].ToString() != strPrevCarID)
					{
						objTableRow = new TableRow();							

						//ITEM_DESCRIPTION
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						objTableCell.Text = dRow["CATEGORY_DESC"].ToString();
						objTableCell.ColumnSpan=4;
						objTableRow.Cells.Add(objTableCell);

						//Done for Itrack Issue 6635 on 28 Oct 09
						//ITEM_INSURING_VALUE
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");					
						//objTableCell.Text=String.Format("{0:,#,###}",Convert.ToInt64(dRow["ITEM_INSURING_VALUE"]));					
						//Done for Itrack Issue 6635 on 25 Nov 09
						if(dRow["CATEGORY_TOTAL"]!=null && dRow["CATEGORY_TOTAL"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["CATEGORY_TOTAL"].ToString()).ToString("N");
						objTableRow.Cells.Add(objTableCell);

						//Commented for Itrack Issue 6635 on 14 Jan 2010
//						objTableCell = new TableCell();
//						objTableCell.Attributes.Add("class","midcolora");						
//						objTableCell.Text="&nbsp;";
//						objTableRow.Cells.Add(objTableCell);

						//Moved here for Itrack Issue 6635 on 14 Jan 2010
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");						
						//objTableCell.Text=String.Format("{0:,#,###.##}",dRow["LIMIT_DEDUC_AMOUNT"]);											
						if(dRow["LIMIT_DEDUC_AMOUNT"]!=null && dRow["LIMIT_DEDUC_AMOUNT"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["LIMIT_DEDUC_AMOUNT"].ToString()).ToString("N");
						objTableRow.Cells.Add(objTableCell);

						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");						
						//objTableCell.Text=String.Format("{0:,#,###}",Convert.ToInt64(dRow["OUTSTANDING"]));
						if(dRow["OUTSTANDING"]!=null && dRow["OUTSTANDING"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["OUTSTANDING"].ToString()).ToString("N");
						objTableRow.Cells.Add(objTableCell);

						if(dRow["OUTSTANDING"]!=null)
						{
							TotalOutstanding+=Convert.ToDouble(dRow["OUTSTANDING"].ToString());
							EnableRecoveryTextBox = true;
						}

						
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");		
						string strRI_RESERVE = objTableCell.ID = "tdRI_RESERVE_" + i.ToString();//Done for Itrack Issue 6635 on 28 Oct 09
						//objTableCell.Text=String.Format("{0:,#,###}",Convert.ToInt64(dRow["RI_RESERVE"]));											
						if(dRow["RI_RESERVE"]!=null && dRow["RI_RESERVE"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["RI_RESERVE"].ToString()).ToString("N");
						objTableRow.Cells.Add(objTableCell);


						//Done for Itrack Issue 6635 on 26 Oct 09
						if(hidACTION_ON_RECOVERY.Value == "168" && hidACTION_ON_RECOVERY.Value == "169" && hidACTION_ON_RECOVERY.Value == "170")
						{
							DropDownList cmbREINSURANCE_CARRIER = new DropDownList();
							DataTable objDataTable=new DataTable();
							objDataTable= Cms.BusinessLayer.BlCommon.ClsCommon.GetReinsuranceCompanyNames();
							cmbREINSURANCE_CARRIER.ID = "cmbREINSURANCE_CARRIER_" + dRow["ITEM_ID"].ToString();
							cmbREINSURANCE_CARRIER.DataSource = objDataTable;
							cmbREINSURANCE_CARRIER.DataTextField="REIN_COMAPANY_NAME";//"LookupDesc";
							cmbREINSURANCE_CARRIER.DataValueField="REIN_COMAPANY_ID";//"LookupID";
							cmbREINSURANCE_CARRIER.DataBind();
							ClsCommon.SelectValueInDDL(cmbREINSURANCE_CARRIER,dRow["REINSURANCE_CARRIER"]);
						}
						else
						{
							objTableCell = new TableCell();
							objTableCell.Attributes.Add("class","midcolora");
							objTableCell.Text="&nbsp;";
							objTableCell.Text = "";		
							objTableRow.Cells.Add(objTableCell);
						}

						objTableCell = new TableCell();						
						strTextBoxID.Length=0;
						//strTextBoxID.Append("txtAMOUNT_" + dRow["ITEM_ID"].ToString());
						strTextBoxID.Append("txtAMOUNT_" + i.ToString());
						i++;

						//Done for Itrack Issue 6635 on 28 Oct 09
						objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["AMOUNT"],strRI_RESERVE,EnableRecoveryTextBox));
						//objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["AMOUNT"],EnableRecoveryTextBox));
						objTableCell.Controls.Add(CreateAmountRangeValidator(strTextBoxID.ToString(),strTextBoxID.Replace("txt","rng").ToString()));
						objTableCell.Attributes.Add("class","midcolora");												
						objTableRow.Cells.Add(objTableCell);


						objTable.Rows.Add(objTableRow);	
						hidScheduledCovgID.Value+="^" + dRow["ITEM_ID"].ToString();
						hidScheduledRsvID.Value+="^" + dRow["RESERVE_ID"].ToString();
						hidScheduledRecrID.Value+="^" + dRow["RECOVERY_ID"].ToString();
						strPrevCarID=dRow["ITEM_ID"].ToString();

						//Added for Itrack Issue 7663 on 19 Aug 2010
						hidSch_ACTUAL_RISK_ID.Value += "^" + dRow["ACTUAL_RISK_ID"].ToString();
						hidSch_ACTUAL_RISK_TYPE.Value = dRow["ACTUAL_RISK_TYPE"].ToString();
						hidSchDWELLING_ID.Value +=  "^" + dRow["DWELLING_ID"].ToString();
					}

					objTableRow = new TableRow();		
			
					objTableCell = new TableCell();
					objTableCell.Attributes.Add("class","midcolora");					
					objTableCell.Text="&nbsp;";
					objTableRow.Cells.Add(objTableCell);

					//ITEM_DESCRIPTION
					objTableCell = new TableCell();
					objTableCell.Attributes.Add("class","midcolora");
					objTableCell .ColumnSpan=3;
					objTableCell.Text=dRow["ITEM_DESCRIPTION"].ToString();					
					objTableRow.Cells.Add(objTableCell);

					//Commented for Itrack Issue 6635 on 14 Jan 2010
//					objTableCell = new TableCell();
//					objTableCell.Attributes.Add("class","midcolora");			
//					objTableCell.Text="&nbsp;";
//					objTableRow.Cells.Add(objTableCell);
//	
//					objTableCell = new TableCell();
//					objTableCell.Attributes.Add("class","midcolora");						
//					//objTableCell.Text=String.Format("{0:,#,###.##}",dRow["LIMIT_DEDUC_AMOUNT"]);											
//					if(dRow["LIMIT_DEDUC_AMOUNT"]!=null && dRow["LIMIT_DEDUC_AMOUNT"].ToString()!="")
//						objTableCell.Text= Double.Parse(dRow["LIMIT_DEDUC_AMOUNT"].ToString()).ToString("N");
//					objTableRow.Cells.Add(objTableCell);

					//Done for Itrack Issue 6635 on 14 Jan 2010
					objTableCell = new TableCell();
					objTableCell.Attributes.Add("class","midcolora");			
					objTableCell.Text=Double.Parse(dRow["ITEM_INSURING_VALUE"].ToString()).ToString("N").Replace(".00","");//Done for Itrack Issue 6635 on 23 Dec 09
					objTableRow.Cells.Add(objTableCell);

					objTableCell = new TableCell();
					objTableCell.Attributes.Add("class","midcolora");			
					objTableCell.Text="&nbsp;";
					objTableRow.Cells.Add(objTableCell);
					
					objTableCell = new TableCell();
					objTableCell.Attributes.Add("class","midcolora");		
					objTableCell.Text="&nbsp;";	
					objTableCell.ColumnSpan=4;
					objTableRow.Cells.Add(objTableCell);

					objTable.Rows.Add(objTableRow);		
					//hidScheduledCovgID.Value+="^" + dRow["ITEM_ID"].ToString();
				}
				plcScheduledCovg.Controls.Add(objTable);
				if(hidScheduledCovgID.Value!="0" && hidScheduledCovgID.Value!="" && hidScheduledCovgID.Value.Length>0)
					hidScheduledCovgID.Value = hidScheduledCovgID.Value.Substring(1,hidScheduledCovgID.Value.Length-1);

				if(hidScheduledRsvID.Value!="0" && hidScheduledRsvID.Value!="" && hidScheduledRsvID.Value.Length>0)
					hidScheduledRsvID.Value = hidScheduledRsvID.Value.Substring(1,hidScheduledRsvID.Value.Length-1);

				if(hidScheduledRecrID.Value!="0" && hidScheduledRecrID.Value!="" && hidScheduledRecrID.Value.Length>0)
					hidScheduledRecrID.Value = hidScheduledRecrID.Value.Substring(1,hidScheduledRecrID.Value.Length-1);

				//Added for Itrack Issue 7663 on 19 Aug 2010
				if(hidSch_ACTUAL_RISK_ID.Value!="0" && hidSch_ACTUAL_RISK_ID.Value!="" && hidSch_ACTUAL_RISK_ID.Value.Length>0)
					hidSch_ACTUAL_RISK_ID.Value = hidSch_ACTUAL_RISK_ID.Value.Substring(1,hidSch_ACTUAL_RISK_ID.Value.Length-1);

				if(hidSchDWELLING_ID.Value!="0" && hidSchDWELLING_ID.Value!="" && hidSchDWELLING_ID.Value.Length>0)
					hidSchDWELLING_ID.Value = hidSchDWELLING_ID.Value.Substring(1,hidSchDWELLING_ID.Value.Length-1);
			}
			else
			{
				lblMessage.Text = "No records found";
				lblMessage.Visible = true;
			}
		}	

		//Added for Itrack Issue 7663
		private void LoadWaterEquipCovgData(DataTable dtTemp)
		{
			Table objTable = new Table();	
			TableCell objTableCell; 
			TableRow objTableRow;
			objTable.Width = Unit.Percentage(100);
			bool EnableRecoveryTextBox = false;
			
			if(dtTemp!=null && dtTemp.Rows.Count>0)
			{
				objTableRow = new TableRow();
				objTableRow.Attributes.Add("class","headereffectWebGrid");
				objTable.Rows.Add(objTableRow);

				objTableCell = new TableCell();
				objTableCell.ColumnSpan=4;
				objTableCell.Width = Unit.Percentage(31);
				objTableCell.Text = "Coverage";
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Text = "Amount";
				objTableCell.Width  = Unit.Percentage(14);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Text = "Deductible";
				objTableCell.Width  = Unit.Percentage(15);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Text = "Outstanding";
				objTableCell.Width  = Unit.Percentage(9);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Text = "RI Reserve";
				objTableCell.Width  = Unit.Percentage(9);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Text = "Reinsurance Carrier";
				objTableCell.Width  = Unit.Percentage(9);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Text = "Recovery Amount";
				objTableCell.Width  = Unit.Percentage(21);
				objTableRow.Cells.Add(objTableCell);

				string strPrevCarID="";
				StringBuilder strTextBoxID = new StringBuilder();
				hidWaterEquipRecrID.Value = "";
				hidWaterEquipCovgID.Value = "";
				hidWaterEquipRsvID.Value = "";
				hidEquip_ACTUAL_RISK_ID.Value = "";
				hidEquip_ACTUAL_RISK_TYPE.Value = "";
				hidEquipDWELLING_ID.Value = "";

				int i=2;
				foreach(DataRow dRow in dtTemp.Rows)
				{
					if (dRow["EQUIP_ID"].ToString() != strPrevCarID)
					{
						objTableRow = new TableRow();							

						//ITEM_DESCRIPTION
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						objTableCell.Text = dRow["EQUIPMENT_DESC"].ToString() + " (" + dRow["EQUIP_DESC"].ToString() + ")";
						objTableCell.ColumnSpan=4;
						objTableRow.Cells.Add(objTableCell);

						//ITEM_INSURING_VALUE
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						if(dRow["INSURED_VALUE"]!=null && dRow["INSURED_VALUE"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["INSURED_VALUE"].ToString()).ToString("N");
						objTableRow.Cells.Add(objTableCell);

						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");		
						if(dRow["EQUIP_AMOUNT"]!=null && dRow["EQUIP_AMOUNT"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["EQUIP_AMOUNT"].ToString()).ToString("N");
						objTableRow.Cells.Add(objTableCell);

						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						if(dRow["OUTSTANDING"]!=null && dRow["OUTSTANDING"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["OUTSTANDING"].ToString()).ToString("N");
						objTableRow.Cells.Add(objTableCell);

						if(dRow["OUTSTANDING"]!=null && dRow["OUTSTANDING"]!= System.DBNull.Value)
						{
							TotalOutstanding+=Convert.ToDouble(dRow["OUTSTANDING"].ToString());
							EnableRecoveryTextBox = true;
						}

						
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");		
						string strRI_RESERVE = objTableCell.ID = "tdWATER_EQUIP_RI_RESERVE_" + i.ToString();											
						if(dRow["RI_RESERVE"]!=null && dRow["RI_RESERVE"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["RI_RESERVE"].ToString()).ToString("N");
						objTableRow.Cells.Add(objTableCell);


						if(hidACTION_ON_RECOVERY.Value == "168" && hidACTION_ON_RECOVERY.Value == "169" && hidACTION_ON_RECOVERY.Value == "170")
						{
							DropDownList cmbREINSURANCE_CARRIER = new DropDownList();
							DataTable objDataTable=new DataTable();
							objDataTable= Cms.BusinessLayer.BlCommon.ClsCommon.GetReinsuranceCompanyNames();
							cmbREINSURANCE_CARRIER.ID = "cmbREINSURANCE_CARRIER_" + dRow["EQUIP_ID"].ToString();
							cmbREINSURANCE_CARRIER.DataSource = objDataTable;
							cmbREINSURANCE_CARRIER.DataTextField="REIN_COMAPANY_NAME";//"LookupDesc";
							cmbREINSURANCE_CARRIER.DataValueField="REIN_COMAPANY_ID";//"LookupID";
							cmbREINSURANCE_CARRIER.DataBind();
							ClsCommon.SelectValueInDDL(cmbREINSURANCE_CARRIER,dRow["REINSURANCE_CARRIER"]);
						}
						else
						{
							objTableCell = new TableCell();
							objTableCell.Attributes.Add("class","midcolora");
							objTableCell.Text="&nbsp;";
							objTableCell.Text = "";		
							objTableRow.Cells.Add(objTableCell);
						}

						objTableCell = new TableCell();						
						strTextBoxID.Length=0;
						strTextBoxID.Append("txtWATER_EQUIP_RECOVERY_AMOUNT_" + i.ToString());
						i++;

						objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["AMOUNT"],strRI_RESERVE,EnableRecoveryTextBox));
						objTableCell.Controls.Add(CreateAmountRangeValidator(strTextBoxID.ToString(),strTextBoxID.Replace("txt","rng").ToString()));
						objTableCell.Attributes.Add("class","midcolora");												
						objTableRow.Cells.Add(objTableCell);


						objTable.Rows.Add(objTableRow);	
						hidWaterEquipCovgID.Value+="^" + dRow["EQUIP_ID"].ToString();
						hidWaterEquipRsvID.Value+="^" + dRow["RESERVE_ID"].ToString();
						hidWaterEquipRecrID.Value+="^" + dRow["RECOVERY_ID"].ToString();
						strPrevCarID= strWATER_EQUIPMENT_COVERAGE_ID;

						//Added for Itrack Issue 7663 on 19 Aug 2010
						hidEquip_ACTUAL_RISK_ID.Value += "^" + dRow["ACTUAL_RISK_ID"].ToString();
						hidEquip_ACTUAL_RISK_TYPE.Value = dRow["ACTUAL_RISK_TYPE"].ToString();
						hidEquipDWELLING_ID.Value +=  "^" + dRow["DWELLING_ID"].ToString();
					}					
				}
				plcWatercraftEquipmentCovg.Controls.Add(objTable);
				if(hidWaterEquipCovgID.Value!="0" && hidWaterEquipCovgID.Value!="" && hidWaterEquipCovgID.Value.Length>0)
					hidWaterEquipCovgID.Value = hidWaterEquipCovgID.Value.Substring(1,hidWaterEquipCovgID.Value.Length-1);

				if(hidWaterEquipRsvID.Value!="0" && hidWaterEquipRsvID.Value!="" && hidWaterEquipRsvID.Value.Length>0)
					hidWaterEquipRsvID.Value = hidWaterEquipRsvID.Value.Substring(1,hidWaterEquipRsvID.Value.Length-1);

				if(hidWaterEquipRecrID.Value!="0" && hidWaterEquipRecrID.Value!="" && hidWaterEquipRecrID.Value.Length>0)
					hidWaterEquipRecrID.Value = hidWaterEquipRecrID.Value.Substring(1,hidWaterEquipRecrID.Value.Length-1);

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

		private void dgSection1CoveragesGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			OnItemDataBound(sender,e,dgSection1CoveragesGrid.ID);
		}	

		private void dgSection2CoveragesGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			OnItemDataBound(sender,e,dgSection2CoveragesGrid.ID);
		}	

//		private void dgScheduledItemsCoveragesGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
//		{
//			OnItemDataBound(sender,e,dgScheduledItemsCoveragesGrid.ID);
//		}	
		private void dgWatercraftCoveragesGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			OnItemDataBound(sender,e,dgWatercraftCoveragesGrid.ID);
		}	
		//Added for Itrack Issue 7663
		private void dgRecVehCoveragesGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			OnItemDataBound(sender,e,dgRecVehCoveragesGrid.ID);
		}
		private void OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e,string dataGridID)
		{

			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				Label lblOutstanding = (Label)e.Item.FindControl("lblOUTSTANDING");								
				Label lblRI_Reserve = (Label)e.Item.FindControl("lblRI_RESERVE");				
				
				TextBox txtAMOUNT = (TextBox)(e.Item.FindControl("txtAMOUNT"));

								
				if(txtAMOUNT!=null)
				{
					//Function and condition  added For Itrack Issue #5359.
					set_label();               
					
					if(hidTRANSACTION_CATEGORY.Value !=null && hidTRANSACTION_CATEGORY.Value !="" && hidTRANSACTION_CATEGORY.Value == "General")
					{
						txtAMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();CompareOutstandingAndPayment('" + lblOutstanding.ClientID + "','" + txtAMOUNT.ClientID + "');");
					}
					else if(hidTRANSACTION_CATEGORY.Value == "Reinsurance")
					{
						txtAMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();CompareOutstandingAndPayment('" + lblRI_Reserve.ClientID + "','" + txtAMOUNT.ClientID + "');");
					}
					//txtAMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();CompareOutstandingAndPayment('" + lblOutstanding.ClientID + "','" + txtAMOUNT.ClientID + "');");
					if (DataBinder.Eval(e.Item.DataItem,"AMOUNT") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"AMOUNT"))!="0" && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"AMOUNT"))!="0.00") 
						txtAMOUNT.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"AMOUNT"));
					else
						txtAMOUNT.Text = "";
					//Line Commented For Itrack Issue #5359.
					//txtAMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();");
					//((RangeValidator)(e.Item.FindControl("rngAMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				
					//((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 11 Feb 09 for Itrack Issue 5385-TO MOVE TO LOCAL VSS
					//Done for Itrack Issue 7016 on 3 Feb 2010
					//((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpDoublePositiveZero;
					((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpDoublePositiveNonZero;
					((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 11 Feb 09 for Itrack Issue 5385
				}
				if(!(dataGridID.ToUpper().Equals(ScheduledItemsCoverageGridID.ToUpper()) || dataGridID.ToUpper().Equals(WaterEquipItemsCoverageGridID.ToUpper())))
				{
					Label lblLIMIT = (Label)e.Item.FindControl("lblLIMIT");					
					if(lblLIMIT!=null && lblLIMIT.Text.Equals(SubHeading))					
					{
						e.Item.Cells[0].ColumnSpan=e.Item.Cells.Count;
						for(int j=e.Item.Cells.Count-1;j>0;j--)
							e.Item.Cells.RemoveAt(j);
						
						e.Item.Cells[0].Font.Bold = true;
						e.Item.Cells[0].Attributes.Add("align","left");
					}				
				}					
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
					 
				/*else
				{
					if(txtAMOUNT!=null)
						txtAMOUNT.Attributes.Add("readOnly","true");
				}*/
			}
		}

		//Added for Itrack Issue 7663
		private void SetTrailerCoverageID()
		{
			if(hidSTATE_ID.Value == ((int)enumState.Indiana).ToString())
				TrailerCovID= TrailerCovID_IN;
			else if(hidSTATE_ID.Value == ((int)enumState.Michigan).ToString())
				TrailerCovID= TrailerCovID_MI;
			else if(hidSTATE_ID.Value == ((int)enumState.Wisconsin).ToString())
				TrailerCovID= TrailerCovID_WI;
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
