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
	public class AddActivityExpenseWatercraft : Cms.Claims.ClaimBase
	{
		
		System.Resources.ResourceManager objResourceMgr;	
		
		
		
		protected int TotalDeductible=0;
		protected System.Web.UI.WebControls.Label lblTitle;
		//protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;			
		protected System.Web.UI.WebControls.Label lblMessage;
		//protected System.Web.UI.WebControls.Label capACTIVITY_DATE;
		//protected System.Web.UI.WebControls.Label lblACTIVITY_DATE;
		protected System.Web.UI.WebControls.DataGrid dgPayment;		
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected Cms.CmsWeb.Controls.CmsButton btnPaymentBreakdown;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidPayeeID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleRowCount;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		//protected System.Web.UI.WebControls.Label capTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtGrossTotal;
		//protected System.Web.UI.WebControls.Label capTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
		protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.RegularExpressionValidator revADDITIONAL_EXPENSE;
		//protected System.Web.UI.WebControls.Label capPAYEE;
		//protected System.Web.UI.WebControls.ListBox cmbPAYEE;
		protected System.Web.UI.WebControls.Label capADDITIONAL_EXPENSE;
		protected System.Web.UI.WebControls.TextBox txtADDITIONAL_EXPENSE;
//		protected System.Web.UI.WebControls.Label capACTION_ON_PAYMENT;
//		protected System.Web.UI.WebControls.DropDownList cmbACTION_ON_PAYMENT;
		protected System.Web.UI.WebControls.DropDownList cmbDrAccts;
		protected System.Web.UI.WebControls.DropDownList cmbCrAccts;
//		public DataTable gDtOldData=null;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCrAccts;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_PAYMENT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCHECK_NUMBER;  
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;//Done for Itrack Issue 6299 on 26 Aug 09
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE;
		private double TotalOutstanding=0;
		const string SubHeading = "SubHeading";
		const string VehicleText = "Boat # : ";
		public static string PaymentDataGridID="";		
		const int POLICY_PAYMENT_TABLE=0;
		const int VEHICLE_PAYMENT_TABLE=1;
		//Done for Itrack Issue 6299 on 26 Aug 09
		//const int TrailerCovID = 50001;
		private int TrailerCovID;
		const string TrailerText = "Trailer # : ";
		const int PAYEE_TABLE=2;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftEquipmentCoveragesRow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWatercraftEquipmentCoveragesGridRow;
		protected System.Web.UI.WebControls.Label lblWatercraftEquipmentCoverages;
		protected System.Web.UI.WebControls.PlaceHolder plcWatercraftEquipmentCovg;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipItemRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipCovgID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipRsvID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterEquipPmntID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquip_ACTUAL_RISK_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquip_ACTUAL_RISK_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEquipDWELLING_ID;
		const int WATERCRAFT_COVERAGE_TABLE = 3;
		const int WATER_EQUIPMENT_COVERAGE_TABLE = 2;
		string strWATER_EQUIPMENT_COVERAGE_ID = "";

		protected enum enumACTION_ON_PAYMENT
		{
			CLOSE_TO_ZERO=11783,
			MAINTAIN_RESERVES_AND_STATUS=11784,
			REDUCE_RESERVES_BY_PAYMENT_AMOUNT=11785,
			REVIEW_RESERVES_MANUALLY=11786
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId="309_10";	

			btnPaymentBreakdown.CmsButtonClass		=	CmsButtonType.Read;
			btnPaymentBreakdown.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;			
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddActivityExpenseWatercraft"  ,System.Reflection.Assembly.GetExecutingAssembly());
			// Put user code to initialize the page here
			
			string strClaimStatus = GetClaimStatus();
			if(strClaimStatus == CLAIM_STATUS_CLOSED)
				btnSave.Visible=false;
			else
				btnSave.Visible=true;


			if ( !Page.IsPostBack)
			{
				PaymentDataGridID = dgPayment.ID;
				SetCaptions();
				SetErrorMessages();
				GetQueryStringValues();
				GetClaimDetails();
//				LoadDropDowns();
				btnBack.Attributes.Add("onClick","javascript: return GoBack();");
				btnSave.Attributes.Add("onClick","javascript: return CompareAllOutstandingAndPayment();");
				btnPaymentBreakdown.Attributes.Add("onClick","javascript: return OpenWindow();");				
				txtADDITIONAL_EXPENSE.Attributes.Add("onBlur","javascript:CalculateTotalPayment();");
				cmbPAYMENT_METHOD.Attributes.Add("onChange","javascript:return cmbPAYMENT_METHOD_Change();");
				LoadAcntgDropDowns();
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
				hidSTATE_ID.Value = dr["STATE_ID"].ToString();//Done for Itrack Issue 6299 on 26 Aug 09
			}
		}

		private void GetActivityDescription()
		{
			ClsActivity objActivity = new ClsActivity();
			DataSet dsActivity = objActivity.GetValuesForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			if(dsActivity.Tables[0].Rows[0]["DESCRIPTION"]!=null)
				txtCLAIM_DESCRIPTION.Text = dsActivity.Tables[0].Rows[0]["DESCRIPTION"].ToString();
		}

//		private void LoadDropDowns()
//		{
//			DataTable dtTransactionCodes = ClsDefaultValues.GetDefaultValuesDetails((int)enumClaimDefaultValues.CLAIM_TRANSACTION_CODE,(int)enumTransactionLookup.EXPENSE_PAYMENT);  
//			if(dtTransactionCodes!=null && dtTransactionCodes.Rows.Count>0)
//			{
//				cmbACTION_ON_PAYMENT.DataSource =  dtTransactionCodes;
//				cmbACTION_ON_PAYMENT.DataTextField="DETAIL_TYPE_DESCRIPTION";
//				cmbACTION_ON_PAYMENT.DataValueField="DETAIL_TYPE_ID";
//				cmbACTION_ON_PAYMENT.DataBind();
//				cmbACTION_ON_PAYMENT.SelectedIndex = 1 ;//Set listbox selected to Reduce Reserves by payment amount 
//			}
//
//			/*ClsActivityExpense objExpense = new ClsActivityExpense();			
//			cmbPAYEE.DataSource =  objExpense.GetClaimAllParties(hidCLAIM_ID.Value);
//			cmbPAYEE.DataTextField="NAME";
//			cmbPAYEE.DataValueField="PARTY_ID";
//			cmbPAYEE.DataBind();*/
//		}

		/*private void GetActivityDate()
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

		private void SetErrorMessages()
		{
			//rfvPAYEE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("821");
			//revADDITIONAL_EXPENSE.ValidationExpression	= aRegExpCurrencyformat;//Done by Sibin on 11 Feb 09 for Itrack Issue 5385
			revADDITIONAL_EXPENSE.ValidationExpression	= aRegExpDoublePositiveZero;
			revADDITIONAL_EXPENSE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			revCHECK_NUMBER.ValidationExpression  = aRegExpDoublePositiveNonZeroStartWithZeroForFedId;
			revCHECK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			rfvPAYMENT_METHOD.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
			csvCLAIM_DESCRIPTION.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("983");
		}

		private void GetOldData()
		{
			DataSet dsOldData = ClsActivityExpense.GetOldDataForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			//when the claim id is null/0/'', we have no data at claim payment table, 
			//we need to fetch data from reserve table as the user has come to this page for the first time
			if(dsOldData==null ||  dsOldData.Tables.Count<2 || dsOldData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count<1)
			{
				dsOldData = ClsReserveDetails.GetOldDataForPage(hidCLAIM_ID.Value);
				if(dsOldData==null || dsOldData.Tables.Count<1)
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
					lblMessage.Visible = true;
					dgPayment.Visible = false;
					return;
				}
				//Add columns for Payment Amount and Action on Payment as they are not present at reserve table..
				for(int i=0;i<dsOldData.Tables.Count;i++)
				{
					DataColumn dtCol = new DataColumn("PAYMENT_AMOUNT");
					dsOldData.Tables[i].Columns.Add(dtCol);
//					dtCol = new DataColumn("ACTION_ON_PAYMENT");
//					dsOldData.Tables[i].Columns.Add(dtCol);
					dtCol = new DataColumn("EXPENSE_ID");
					dsOldData.Tables[i].Columns.Add(dtCol);				
				}
				hidOldData.Value = "";
			}	
			else
			{
				hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[VEHICLE_PAYMENT_TABLE]);
				/*if(dsOldData.Tables.Count>PAYEE_TABLE && dsOldData.Tables[PAYEE_TABLE].Rows.Count>0 && dsOldData.Tables[PAYEE_TABLE].Rows[0]["PAYEE_PARTIES_ID"]!=null && dsOldData.Tables[PAYEE_TABLE].Rows[0]["PAYEE_PARTIES_ID"].ToString()!="")
				{
					hidPayeeID.Value = dsOldData.Tables[PAYEE_TABLE].Rows[0]["PAYEE_PARTIES_ID"].ToString();
					SelectPayees(hidPayeeID.Value);
				}*/
				LoadData(dsOldData.Tables[VEHICLE_PAYMENT_TABLE]);
			}

			BindGrid(dsOldData);
		}

		/*private void SelectPayees(string Payees)
		{
			string[] strPayees = Payees.Split(',');
			for(int i=0;i<strPayees.Length;i++)
			{
				for(int j=0;j<cmbPAYEE.Items.Count;j++)
				{
					if(cmbPAYEE.Items[j].Value == strPayees[i].ToString())
					{
						cmbPAYEE.Items[j].Selected = true;
						continue;
					}

				}
			}
			
		}*/		
		private void LoadData(DataTable dtOldData)
		{
			if(dtOldData!=null && dtOldData.Rows.Count>0)
			{
				if(dtOldData.Rows[0]["EXPENSE_ACTION_ON_PAYMENT"]!=null && dtOldData.Rows[0]["EXPENSE_ACTION_ON_PAYMENT"].ToString()!="")
					hidACTION_ON_PAYMENT.Value = dtOldData.Rows[0]["EXPENSE_ACTION_ON_PAYMENT"].ToString();
				if(dtOldData.Rows[0]["ADDITIONAL_EXPENSE"]!=null && dtOldData.Rows[0]["ADDITIONAL_EXPENSE"].ToString()!="")
					txtADDITIONAL_EXPENSE.Text = dtOldData.Rows[0]["ADDITIONAL_EXPENSE"].ToString();
				if(dtOldData.Rows[0]["DrAccts"]!=null && dtOldData.Rows[0]["DrAccts"].ToString()!="")
					cmbDrAccts.SelectedValue = dtOldData.Rows[0]["DrAccts"].ToString();

				if(dtOldData.Rows[0]["CrAccts"]!=null && dtOldData.Rows[0]["CrAccts"].ToString()!="")
					cmbCrAccts.SelectedValue = dtOldData.Rows[0]["CrAccts"].ToString();
				if(dtOldData.Rows[0]["EXPENSE_PAYMENT_METHOD"]!=null && dtOldData.Rows[0]["EXPENSE_PAYMENT_METHOD"].ToString()!="")
					cmbPAYMENT_METHOD.SelectedValue = dtOldData.Rows[0]["EXPENSE_PAYMENT_METHOD"].ToString();

				if(dtOldData.Rows[0]["CHECK_NUMBER"]!=null && dtOldData.Rows[0]["CHECK_NUMBER"].ToString()!="")
					txtCHECK_NUMBER.Text = dtOldData.Rows[0]["CHECK_NUMBER"].ToString();
			}
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
			capPAYMENT_METHOD.Text	=		objResourceMgr.GetString("cmbPAYMENT_METHOD");
			//capACTIVITY_DATE.Text 		=		objResourceMgr.GetString("lblACTIVITY_DATE");			
			//capTOTAL_OUTSTANDING.Text	=		objResourceMgr.GetString("txtTOTAL_OUTSTANDING");
			//capTOTAL_PAYMENT.Text 		=		objResourceMgr.GetString("txtTOTAL_PAYMENT");
			capCLAIM_DESCRIPTION.Text 		=		objResourceMgr.GetString("txtCLAIM_DESCRIPTION");
			//capPAYEE.Text 				=		objResourceMgr.GetString("cmbPAYEE");
//			capACTION_ON_PAYMENT.Text 	=		objResourceMgr.GetString("cmbACTION_ON_PAYMENT");
			capADDITIONAL_EXPENSE.Text 	=		objResourceMgr.GetString("txtADDITIONAL_EXPENSE");
		}
		#endregion

		
		
		private void BindGrid(DataSet dsData)
		{
			if(dsData==null || dsData.Tables.Count<PAYEE_TABLE || dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count<1)
			{
				lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
				lblMessage.Visible = true;
				dgPayment.Visible = false;
				trBody.Attributes.Add("style","display:none");
				btnSave.Visible = false;
				return;
			}
			//hidPolicyRowCount.Value = dsData.Tables[POLICY_PAYMENT_TABLE].Rows.Count.ToString();
			
			//dgPolicyPayment.DataSource = dsData.Tables[POLICY_PAYMENT_TABLE];
			//dgPolicyPayment.DataBind();	

			if(dsData!=null && hidCustomerID.Value!="" && hidCustomerID.Value!="0" && hidPolicyID.Value!="" && hidPolicyID.Value!="0" && hidPolicyVersionID.Value!="" && hidPolicyVersionID.Value!="0" && dsData.Tables.Count>VEHICLE_PAYMENT_TABLE && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count>0)
			{
				
				
				SetTrailerCoverageID();
				string curVehicle,prevVehicle;
				int position=0 , BoatCounter = 1; 
				curVehicle = prevVehicle = "";

				int RowCount = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count;
				DataSet dsFinal = dsData.Clone();
				int FinalPosition = 0 ; 
				bool AddRow = true; 

				while(position < RowCount)
				{
					AddRow = true;
					curVehicle = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["VEHICLE_ID"].ToString();

					if(prevVehicle == "")
					{
						DataRow newRow = dsFinal.Tables[VEHICLE_PAYMENT_TABLE].NewRow();
						newRow["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["VEHICLE"];							
						newRow["LIMIT"] = SubHeading;
						dsFinal.Tables[VEHICLE_PAYMENT_TABLE].Rows.InsertAt(newRow,FinalPosition);
						FinalPosition++;
						prevVehicle = curVehicle ; 
					}

					if(prevVehicle != curVehicle)
					{

						DataRow newRow = dsFinal.Tables[VEHICLE_PAYMENT_TABLE].NewRow();
						newRow["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["VEHICLE"];							
						newRow["LIMIT"] = SubHeading;
						dsFinal.Tables[VEHICLE_PAYMENT_TABLE].Rows.InsertAt(newRow,FinalPosition);
						prevVehicle = curVehicle ; 
						FinalPosition++;
						BoatCounter = 1; 
					}

					//ACTUAL_RISK_TYPE
					if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["ACTUAL_RISK_TYPE"].ToString() == "TR")
					{
						string ActualRiskId = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["ACTUAL_RISK_ID"].ToString();
							
						DataSet dsTrailer		= ClsReserveDetails.GetTrailerDetails(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,	curVehicle,ActualRiskId );
						DataRow HeaderRow		= dsFinal.Tables[VEHICLE_PAYMENT_TABLE].NewRow();
						HeaderRow["COV_DESC"]	= TrailerText + " #: " + BoatCounter.ToString() ;						
						HeaderRow["LIMIT"]		= SubHeading;
							
						DataRow TrailerRow = dsFinal.Tables[VEHICLE_PAYMENT_TABLE].NewRow();

						TrailerRow["COV_DESC"]	= "Section 1 - Covered Property Damage - Actual Cash Value";
						TrailerRow["COV_ID"]	= TrailerCovID ;
						TrailerRow["LIMIT"]		= dsTrailer.Tables[0].Rows[0]["INSURED_VALUE"].ToString();
						TrailerRow["DEDUCTIBLE"]= dsTrailer.Tables[0].Rows[0]["TRAILER_DED"].ToString();
						TrailerRow["VEHICLE_ID"]= curVehicle ;
						TrailerRow["ACTUAL_RISK_ID"] = ActualRiskId; 
						TrailerRow["ACTUAL_RISK_TYPE"] = "TR";
						TrailerRow["OUTSTANDING"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["OUTSTANDING"];

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["PAYMENT_AMOUNT"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["PAYMENT_AMOUNT"] != DBNull.Value)
						{
							TrailerRow["PAYMENT_AMOUNT"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["PAYMENT_AMOUNT"];
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["PRIMARY_EXCESS"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["PRIMARY_EXCESS"] != DBNull.Value)
						{
							TrailerRow["PRIMARY_EXCESS"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["PRIMARY_EXCESS"];
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["ATTACHMENT_POINT"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["ATTACHMENT_POINT"] != DBNull.Value)
						{
							TrailerRow["ATTACHMENT_POINT"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["ATTACHMENT_POINT"];
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["RI_RESERVE"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["RI_RESERVE"] != DBNull.Value)
						{
							TrailerRow["RI_RESERVE"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["RI_RESERVE"]; 
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["RESERVE_ID"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["RESERVE_ID"] != DBNull.Value)
						{
							TrailerRow["RESERVE_ID"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["RESERVE_ID"];
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["REINSURANCE_CARRIER"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["REINSURANCE_CARRIER"] != DBNull.Value)
						{
							TrailerRow["REINSURANCE_CARRIER"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["REINSURANCE_CARRIER"];
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["REINSURANCECARRIER"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["REINSURANCECARRIER"] != DBNull.Value)
						{
							TrailerRow["REINSURANCECARRIER"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["REINSURANCECARRIER"];
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"] != DBNull.Value)
						{
							TrailerRow["MCCA_ATTACHMENT_POINT"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["MCCA_ATTACHMENT_POINT"];
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["MCCA_APPLIES"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["MCCA_APPLIES"] != DBNull.Value)
						{
							TrailerRow["MCCA_APPLIES"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["MCCA_APPLIES"];
						}

						if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["EXPENSE_ID"] != null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["EXPENSE_ID"] != DBNull.Value)
						{
							TrailerRow["EXPENSE_ID"] = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position]["EXPENSE_ID"];
						}

							
						dsFinal.Tables[VEHICLE_PAYMENT_TABLE].Rows.InsertAt(HeaderRow, FinalPosition );
						FinalPosition++;

						dsFinal.Tables[VEHICLE_PAYMENT_TABLE].Rows.InsertAt(TrailerRow , FinalPosition );

						BoatCounter++;
						AddRow = false;
					}

					if(AddRow)
					{
						DataRow drExisting = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[position];
							
						dsFinal.Tables[VEHICLE_PAYMENT_TABLE].ImportRow(drExisting);

					}
					position++;
					FinalPosition++;

				}
				
				//				//Code for adding new subheading comes here
				//				string curVehicle,prevVehicle;
				//				TableRow row = new TableRow();
				//				int i=0;
				//				curVehicle = prevVehicle = "";
				//				int cntTrailer=0;
				//				for(i=0;i<dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count;i++)
				//				{
				//					curVehicle = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[i]["VEHICLE_ID"].ToString();
				//					if(curVehicle !=  prevVehicle)
				//					{
				//						cntTrailer = 0;
				//						prevVehicle = curVehicle;
				//						DataRow dr = dsData.Tables[VEHICLE_PAYMENT_TABLE].NewRow();
				//						dr["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[i]["VEHICLE"];
				//						//dr["COV_DESC"] = "SubHead";
				//						dr["LIMIT"] = SubHeading;
				//						dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.InsertAt(dr,i);						
				//					}
				//					if(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[i]["COV_ID"].ToString() !="")
				//					{
				//						SetTrailerCoverageID();//Done for Itrack Issue 6299 on 26 Aug 09
				//						if(Convert.ToInt32(dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[i]["COV_ID"])>=TrailerCovID)
				//						{
				//							DataSet dsTrailer = ClsReserveDetails.GetTrailerDataSet(hidCustomerID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,	curVehicle);
				//							if(dsTrailer != null && dsTrailer.Tables[0].Rows.Count>0)
				//							{
				//								dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[i]["LIMIT"] = dsTrailer.Tables[0].Rows[cntTrailer]["INSURED_VALUE"];
				//								dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[i]["DEDUCTIBLE"] = dsTrailer.Tables[0].Rows[cntTrailer]["TRAILER_DED"];
				//							}
				//							DataRow dr		= dsData.Tables[VEHICLE_PAYMENT_TABLE].NewRow();
				//							dr["COV_DESC"]	= TrailerText + (++cntTrailer) ;						
				//							dr["LIMIT"]		= SubHeading;
				//							//i++;
				//							dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.InsertAt(dr,i);
				//							dr=null;
				//							i++;
				//						}
				//					}
				//				}
				hidVehicleRowCount.Value = dsFinal.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count.ToString();
				dgPayment.DataSource = dsFinal.Tables[VEHICLE_PAYMENT_TABLE];
				dgPayment.DataBind();	
			}


			if(dsData.Tables.Count>WATER_EQUIPMENT_COVERAGE_TABLE && dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE]!=null && dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE].Rows.Count>0)
			{
				hidWaterEquipItemRowCount.Value = dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE].Rows.Count.ToString();
				LoadWaterEquipCovgData(dsData.Tables[WATER_EQUIPMENT_COVERAGE_TABLE]);
			}
			else
			{
				trWatercraftEquipmentCoveragesGridRow.Attributes.Add("style","display:none");
				trWatercraftEquipmentCoveragesRow.Attributes.Add("style","display:none");
			}

			if(TotalOutstanding!=0 && TotalOutstanding!=0.0)
				txtTOTAL_OUTSTANDING.Text=Double.Parse(TotalOutstanding.ToString()).ToString("N");

//			if(TotalOutstanding!=0 && TotalOutstanding!=0.0)
//				txtTOTAL_OUTSTANDING.Text=String.Format("{0:,#,###.##}",TotalOutstanding.ToString());
//			else 
//				txtTOTAL_OUTSTANDING.Text = "0";
			
		}

		private void dgPayment_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				Label lblOutstanding = (Label)e.Item.FindControl("lblOUTSTANDING");								
				Label lblRI_Reserve = (Label)e.Item.FindControl("lblRI_RESERVE");								
				
				
				TextBox txtPAYMENT_AMOUNT = (TextBox)(e.Item.FindControl("txtPAYMENT_AMOUNT"));
				if(txtPAYMENT_AMOUNT!=null)
				{
					txtPAYMENT_AMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();CompareOutstandingAndPayment('" + lblOutstanding.ClientID + "','" + txtPAYMENT_AMOUNT.ClientID + "');");
					if (DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT"))!="0") 
						txtPAYMENT_AMOUNT.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT"));
					else
						txtPAYMENT_AMOUNT.Text = "";
				}				
				//((RangeValidator)(e.Item.FindControl("rngPAYMENT_AMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				
				//((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 11 Feb 09 for Itrack Issue 5385-TO MOVE TO LOCAL VSS	
				//Done for Itrack Issue 6516 on 15 Oct 09
				//((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ValidationExpression = aRegExpDoublePositiveZero;
				((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ValidationExpression = aRegExpDoublePositiveNonZero;
				((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 11 Feb 09 for Itrack Issue 5385
				
				Label lblLIMIT = (Label)e.Item.FindControl("lblLIMIT");					
				if(lblLIMIT.Text.Equals(SubHeading))					
				{
					e.Item.Cells[0].ColumnSpan=e.Item.Cells.Count;
					for(int j=e.Item.Cells.Count-1;j>0;j--)
						e.Item.Cells.RemoveAt(j);
						
					e.Item.Cells[0].Font.Bold = true;
					e.Item.Cells[0].Attributes.Add("align","left");
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
					txtPAYMENT_AMOUNT.Attributes.Add("readOnly","true");
					txtPAYMENT_AMOUNT.Attributes.Add("style","background-color:#C0C0C0;");
				}			
				
				//We have to allow the user to enter values exceeding outstanding amount
//				else
//				{
//					txtPAYMENT_AMOUNT.Attributes.Add("readOnly","true");
//					txtPAYMENT_AMOUNT.Attributes.Add("style","background-color:#C0C0C0;");
//				}
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
			this.dgPayment.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPayment_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
			if(Request["ACTION_ON_PAYMENT"]!=null && Request["ACTION_ON_PAYMENT"].ToString()!="")
				hidACTION_ON_PAYMENT.Value = Request["ACTION_ON_PAYMENT"].ToString();
			else
				hidACTION_ON_PAYMENT.Value = "0";			
					

		}

		private void GetQueryStringValuesTemp()
		{
			hidCLAIM_ID.Value = "0";
			hidACTIVITY_ID.Value = "0";

		}

		private void PopulateArray(ArrayList aPaymentList,DataGrid dtGrid)
		{
			foreach(DataGridItem dgi in dtGrid.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					if(dtGrid.ID.ToUpper().Equals(PaymentDataGridID.ToUpper()))
					{
						Label lblCOV_DESC = (Label)(dgi.FindControl("lblCOV_DESC"));
						if(lblCOV_DESC!=null && lblCOV_DESC.Text.IndexOf(VehicleText)!=-1)
							continue;
						if(lblCOV_DESC!=null && lblCOV_DESC.Text.IndexOf(TrailerText)!=-1)
							continue;
					}
					//Model Object
					ClsActivityExpenseInfo objActivityExpenseInfo = new	 ClsActivityExpenseInfo();
					
					TextBox txtPAYMENT_AMOUNT = (TextBox)(dgi.FindControl("txtPAYMENT_AMOUNT"));
					
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));
					Label lblEXPENSE_ID = (Label)(dgi.FindControl("lblEXPENSE_ID"));
					Label lblBOAT_ID = (Label)(dgi.FindControl("lblBOAT_ID"));
					//Added for Itrack Issue 7663 on 19 Aug 2010
					Label lblACTUAL_RISK_ID = (Label)(dgi.FindControl("lblACTUAL_RISK_ID"));
					Label lblACTUAL_RISK_TYPE = (Label)(dgi.FindControl("lblACTUAL_RISK_TYPE"));
					
					if(hidACTION_ON_PAYMENT.Value!="")
						objActivityExpenseInfo.ACTION_ON_PAYMENT = int.Parse(hidACTION_ON_PAYMENT.Value);

					objActivityExpenseInfo.IS_ACTIVE="Y";
					objActivityExpenseInfo.CREATED_BY = int.Parse(GetUserId());
					objActivityExpenseInfo.CREATED_DATETIME = System.DateTime.Now;
					objActivityExpenseInfo.MODIFIED_BY = int.Parse(GetUserId());
					objActivityExpenseInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
					objActivityExpenseInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					objActivityExpenseInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);	
					//Added for Itrack Issue 7663 on 19 Aug 2010
					objActivityExpenseInfo.ACTUAL_RISK_ID = int.Parse(lblACTUAL_RISK_ID.Text);
					objActivityExpenseInfo.ACTUAL_RISK_TYPE = lblACTUAL_RISK_TYPE.Text;

					if(lblRESERVE_ID.Text.Trim()!="")
						objActivityExpenseInfo.RESERVE_ID = int.Parse(lblRESERVE_ID.Text.Trim());
					if(txtPAYMENT_AMOUNT.Text.Trim()!="")
						objActivityExpenseInfo.PAYMENT_AMOUNT = Convert.ToDouble(txtPAYMENT_AMOUNT.Text.Trim());
					if(lblEXPENSE_ID.Text.Trim()!="")
						objActivityExpenseInfo.EXPENSE_ID = int.Parse(lblEXPENSE_ID.Text.Trim());					
					if(lblBOAT_ID!=null && lblBOAT_ID.Text.Trim()!="")
						objActivityExpenseInfo.VEHICLE_ID = int.Parse(lblBOAT_ID.Text.Trim());
					if(txtADDITIONAL_EXPENSE.Text.Trim()!="")
						objActivityExpenseInfo.ADDITIONAL_EXPENSE = Convert.ToDouble(txtADDITIONAL_EXPENSE.Text.Trim());
					if(cmbDrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
						objActivityExpenseInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedValue);
					if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
						objActivityExpenseInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedValue);
					if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="")
						objActivityExpenseInfo.PAYMENT_METHOD = Convert.ToInt32(cmbPAYMENT_METHOD.SelectedValue);
					if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="" && cmbPAYMENT_METHOD.SelectedItem.Value=="11979")
						objActivityExpenseInfo.CHECK_NUMBER = txtCHECK_NUMBER.Text.Trim() ;					
					aPaymentList.Add(objActivityExpenseInfo);
				}
			}
		}
		private void LoadAcntgDropDowns()
		{
			//DataSet DS = BusinessLayer.BLClaims.ClsDefaultValues.GetLedgerAcctsForClaims(Convert.ToInt32(cmbACTION_ON_PAYMENT.SelectedValue));
			DataSet DS = BusinessLayer.BLClaims.ClsDefaultValues.GetLedgerAcctsForClaims(Convert.ToInt32(hidACTION_ON_PAYMENT.Value));
			if(DS==null || DS.Tables[0]==null || DS.Tables[0].Rows.Count<1)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("931");
				lblMessage.Visible = true;
				return;
			}
			//If more then one acct exist then add a BLANK Option and default BLANK.
			//If single account exists then default that and disable dropdown

			cmbDrAccts.DataSource=DS.Tables[0];
			cmbDrAccts.DataTextField="DESC_TO_SHOW";
			cmbDrAccts.DataValueField="ACCOUNT_ID";
			cmbDrAccts.DataBind();

			ListItem lItem = new ListItem("","");
			if (DS.Tables[0].Rows.Count > 1)
			{
				//ListItem LI_DR = new ListItem("","");
				//cmbDrAccts.Items.Add(LI_DR);
				cmbDrAccts.Items.Insert(0,lItem);
				//cmbDrAccts.ClearSelection();				
				//cmbDrAccts.Items.FindByValue("").Selected = true;
			}
			else
			{
				cmbDrAccts.Enabled=false;
			}

			
			cmbCrAccts.DataSource = DS.Tables[1];
			cmbCrAccts.DataTextField="DESC_TO_SHOW";
			cmbCrAccts.DataValueField="ACCOUNT_ID";
			cmbCrAccts.DataBind();


			if (DS.Tables[0].Rows.Count > 1)
			{
				//ListItem LI_CR = new ListItem();
				//cmbCrAccts.Items.Add(LI_CR);
				cmbCrAccts.Items.Insert(0,lItem);
				//cmbCrAccts.ClearSelection();
				//cmbCrAccts.Items.FindByValue("").Selected = true;
			}
			else
			{
				cmbCrAccts.Enabled=false;
			}
			ClsPayment objPayment = new ClsPayment();
			objPayment.PopulatePaymentMethodDropDown(cmbPAYMENT_METHOD,"PYMTD",((int)Cms.CmsWeb.cmsbase.enumPaymentMethod.EFT).ToString());
		}		

		

		
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				int PAYMENT_ACTION;
				//string strPAYEE;
				ClsActivityExpense objActivityExpense = new ClsActivityExpense();
				ArrayList PaymentArrayList = new ArrayList();				
				PopulateArray(PaymentArrayList,dgPayment);
				PopulateWaterEquipArray(PaymentArrayList);//Added for Itrack Issue 7663

				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					intRetVal = objActivityExpense.Add(PaymentArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
					//RegisterStartupScript("ReLoadTab","<script>ReLoadTab();</script>");
				}
				else
				{
					intRetVal = objActivityExpense.Update(PaymentArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"31");
				}
					
				if(intRetVal>0)
				{			
					//strPAYEE = GetPayees();
					//ClsClaimsNotification.UpdateClaimDescription(hidCLAIM_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					ClsActivity objActivity = new ClsActivity();
					objActivity.UpdateActivityDescription(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					if(hidACTION_ON_PAYMENT.Value!="")
						PAYMENT_ACTION = int.Parse(hidACTION_ON_PAYMENT.Value);
					else
						PAYMENT_ACTION = -1;
					ClsActivityExpense.UpdateReserveActivity(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,PAYMENT_ACTION.ToString());					
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
	
		private void LoadWaterEquipCovgData(DataTable dtTemp)
		{
			SetWaterEquipCoverageID();
			Table objTable = new Table();	
			TableCell objTableCell; 
			TableRow objTableRow;
			objTable.Width = Unit.Percentage(100);
			bool EnablePaymentTextBox = true;
			
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
				objTableCell.Text = "Reinsurance Carrier";//Done for Itrack Issue 6635 on 29 Oct 09
				objTableCell.Width  = Unit.Percentage(9);
				objTableRow.Cells.Add(objTableCell);

				objTableCell = new TableCell();
				objTableCell.Text = "Payment Amount";
				objTableCell.Width  = Unit.Percentage(21);
				objTableRow.Cells.Add(objTableCell);

				
				string strPrevCarID="";
				StringBuilder strTextBoxID = new StringBuilder();
				StringBuilder strLabelOutstandingID = new StringBuilder();
				hidWaterEquipCovgID.Value = "";
				hidWaterEquipRsvID.Value = "";
				hidWaterEquipPmntID.Value = "";
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
						//Done for Itrack Issue 6635 on 25 Nov 09
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
						strLabelOutstandingID.Length=0;
						strLabelOutstandingID.Append("lblOUTSTANDING_WATER_EQUIP_AMOUNT_" + i.ToString());
						objTableCell.Controls.Add(CreateAmountLabel(strLabelOutstandingID.ToString(),dRow["OUTSTANDING"]));
						
						objTableRow.Cells.Add(objTableCell);

						if(dRow["OUTSTANDING"]!=null && Convert.ToDouble(dRow["OUTSTANDING"].ToString())>0)
						{
							TotalOutstanding+=Convert.ToDouble(dRow["OUTSTANDING"].ToString());
							EnablePaymentTextBox = true;
						}

						
						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");		
						string strRI_RESERVE = objTableCell.ID = "tdWATER_EQUIP_RI_RESERVE_" + i.ToString();//Done for Itrack Issue 6635 on 29 Oct 09						
						if(dRow["RI_RESERVE"]!=null && dRow["RI_RESERVE"].ToString()!="")
							objTableCell.Text= Double.Parse(dRow["RI_RESERVE"].ToString()).ToString("N");											
						objTableRow.Cells.Add(objTableCell);


						

						objTableCell = new TableCell();
						objTableCell.Attributes.Add("class","midcolora");
						objTableCell.Text="&nbsp;";
						objTableCell.Text = "";//Done for Itrack Issue 6635 on 29 Oct 09		
						objTableRow.Cells.Add(objTableCell);

						objTableCell = new TableCell();						
						strTextBoxID.Length=0;
						//strTextBoxID.Append("txtPAYMENT_AMOUNT_" + dRow["ITEM_ID"].ToString());
						strTextBoxID.Append("txtWATER_EQUIP_PAYMENT_AMOUNT_" + i.ToString());
						i++;

						//Done for Itrack Issue 6635 on 27 Nov 09
						objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["PAYMENT_AMOUNT"],strLabelOutstandingID.ToString(),EnablePaymentTextBox));
						//objTableCell.Controls.Add(CreateAmountTextBox(strTextBoxID.ToString(),dRow["PAYMENT_AMOUNT"],EnablePaymentTextBox));
						objTableCell.Controls.Add(CreateAmountRangeValidator(strTextBoxID.ToString(),strTextBoxID.Replace("txt","rng").ToString()));
						objTableCell.Attributes.Add("class","midcolora");	
						
						Label lblItemId= new Label();
						lblItemId.Text = dRow["EQUIP_ID"].ToString();
						lblItemId.ID = "lbl_ " + dRow["EQUIP_ID"].ToString();
						lblItemId.Attributes.Add("style","display:none");
						objTableCell.Controls.Add(lblItemId);
						objTableRow.Cells.Add(objTableCell);


						objTable.Rows.Add(objTableRow);	
						hidWaterEquipCovgID.Value+="^" + strWATER_EQUIPMENT_COVERAGE_ID;
						hidWaterEquipRsvID.Value+="^" + dRow["RESERVE_ID"].ToString();
						hidWaterEquipPmntID.Value+="^" + dRow["EXPENSE_ID"].ToString();
						strPrevCarID=strWATER_EQUIPMENT_COVERAGE_ID;

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

				if(hidWaterEquipPmntID.Value!="0" && hidWaterEquipPmntID.Value!="" && hidWaterEquipPmntID.Value.Length>0)
					hidWaterEquipPmntID.Value = hidWaterEquipPmntID.Value.Substring(1,hidWaterEquipPmntID.Value.Length-1);

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
			string [] ScheduleExpArray = hidWaterEquipPmntID.Value.Split('^');
			//Added for Itrack Issue 7663 on 19 Aug 2010
			string [] EquipActual_Risk_IdArray = hidEquip_ACTUAL_RISK_ID.Value.Split('^');
			string [] EquipDwelling_IdArray = hidEquipDWELLING_ID.Value.Split('^');
			
			if(ScheduleRsvArray==null || ScheduleRsvArray.Length<1)
				return;

			StringBuilder strTextBoxID = new StringBuilder();
			int i=2;
			for(int iCounter=0;iCounter<ScheduleCovArray.Length;iCounter++)
			{
				ClsActivityExpenseInfo objActivityExpenseInfo = new ClsActivityExpenseInfo();
				//string txtOut_Standing_ID = "txtOUT_STANDING_" + ScheduleCovArray[iCounter];
				strTextBoxID.Length=0;
				//Commented by Asfa (23-Oct-2007)
				//strTextBoxID.Append("txtPAYMENT_AMOUNT_" + ScheduleCovArray[iCounter]);
				strTextBoxID.Append("txtWATER_EQUIP_PAYMENT_AMOUNT_" + i.ToString());
				i++;

				//TextBox txtOut_Standing = (TextBox)(Form1.FindControl(strTextBoxID.ToString()));

				if (Request.Form[strTextBoxID.ToString()]!= null && Request.Form[strTextBoxID.ToString()].ToString()!="")				
					objActivityExpenseInfo.PAYMENT_AMOUNT = Convert.ToDouble(Request.Form[strTextBoxID.ToString()].ToString());
				
				if(hidACTION_ON_PAYMENT.Value!="")
					objActivityExpenseInfo.ACTION_ON_PAYMENT = int.Parse(hidACTION_ON_PAYMENT.Value);


				
				objActivityExpenseInfo.RESERVE_ID = int.Parse(ScheduleRsvArray[iCounter].ToString());
				if(ScheduleExpArray!=null && ScheduleExpArray[iCounter]!=null && ScheduleExpArray[iCounter]!="")
					objActivityExpenseInfo.EXPENSE_ID = int.Parse(ScheduleExpArray[iCounter].ToString());
				
				objActivityExpenseInfo.IS_ACTIVE="Y";
				objActivityExpenseInfo.CREATED_BY = int.Parse(GetUserId());
				objActivityExpenseInfo.CREATED_DATETIME = System.DateTime.Now;
				objActivityExpenseInfo.MODIFIED_BY = int.Parse(GetUserId());
				objActivityExpenseInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
				objActivityExpenseInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
				objActivityExpenseInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
				//Added for Itrack Issue 7663 on 19 Aug 2010
				objActivityExpenseInfo.VEHICLE_ID = int.Parse(EquipDwelling_IdArray[iCounter]);
				objActivityExpenseInfo.ACTUAL_RISK_ID = int.Parse(EquipActual_Risk_IdArray[iCounter]);
				objActivityExpenseInfo.ACTUAL_RISK_TYPE = hidEquip_ACTUAL_RISK_TYPE.Value;

				if(txtADDITIONAL_EXPENSE.Text.Trim()!="")
					objActivityExpenseInfo.ADDITIONAL_EXPENSE = Convert.ToDouble(txtADDITIONAL_EXPENSE.Text.Trim());

				if(cmbDrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
					objActivityExpenseInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedValue);

				if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
					objActivityExpenseInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedValue);		
				if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="")
					objActivityExpenseInfo.PAYMENT_METHOD = Convert.ToInt32(cmbPAYMENT_METHOD.SelectedValue);

				aList.Add(objActivityExpenseInfo);
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

		private TextBox CreateAmountTextBox(string strTextBoxID,Object objValue,string strLabelOutstandingID,bool EnableTextBox)//Done for Itrack Issue 6635 on 27 Nov 09
		{
			TextBox tbAmount = new TextBox();			
			tbAmount.CssClass = "INPUTCURRENCY";
			tbAmount.MaxLength = 8;
			tbAmount.Width=Unit.Percentage(100);
			tbAmount.Attributes.Add("Size","10");
			tbAmount.ID = strTextBoxID;	
			//Done for Itrack Issue 6635 on 29 Oct 09
			//tbAmount.Attributes.Add("onBlur","javascript:this.value=formatCurrencyWithCents(this.value);");	
			tbAmount.Attributes.Add("onBlur","javascript:this.value=formatCurrencyWithCents(this.value);CalculateTotalPayment();CompareOutstandingAndPayment('" + strLabelOutstandingID + "','" + tbAmount.ID + "');");//Done for Itrack Issue 6635 on 27 Nov 09	
			if(objValue!=null && objValue.ToString()!="" && objValue.ToString()!="0")
				tbAmount.Text= Double.Parse(objValue.ToString()).ToString("N");
			//tbAmount.Text = String.Format("{0:,#,###}",Convert.ToInt64(objValue));			
			tbAmount.Enabled = EnableTextBox;
				
			Form1.Controls.Add(tbAmount);
			//Page.Controls.Add(tbAmount);

			return tbAmount;
		}

		private Label CreateAmountLabel(string strLabelOutstandingID,Object objValue)
		{
			Label lblAmount = new Label();			
			//lblAmount.CssClass = "midcolora";
			lblAmount.ID = strLabelOutstandingID;			
			if(objValue!=null && objValue.ToString()!="")
				lblAmount.Text = Double.Parse(objValue.ToString()).ToString("N");
				
			Form1.Controls.Add(lblAmount);
			return lblAmount;
		}

		private RangeValidator CreateAmountRangeValidator(string strTextBoxID,string strRangeValidatorID)
		{
			RangeValidator rngAmount = new RangeValidator();
			rngAmount.MinimumValue="0";
			rngAmount.MaximumValue="9999999999";
			rngAmount.Type=ValidationDataType.Currency;
			rngAmount.Display = ValidatorDisplay.Dynamic;
			rngAmount.ControlToValidate=strTextBoxID;
			rngAmount.ErrorMessage = "<br/>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done for Itrack Issue 6635 on 29 Oct 09
			rngAmount.ID = strRangeValidatorID;
			return rngAmount;
		}
	}
}
