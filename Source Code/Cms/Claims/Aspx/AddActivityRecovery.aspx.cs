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
	public class AddActivityRecovery : Cms.Claims.ClaimBase
	{
		
		System.Resources.ResourceManager objResourceMgr;	
		
		
		
		protected int TotalDeductible=0;
		protected System.Web.UI.WebControls.Label lblTitle;
		//protected Cms.CmsWeb.WebControls.ClaimTop cltClaimTop;			
		protected System.Web.UI.WebControls.Label lblMessage;
		//protected System.Web.UI.WebControls.Label capACTIVITY_DATE;
		//protected System.Web.UI.WebControls.Label lblACTIVITY_DATE;
		protected System.Web.UI.WebControls.DataGrid dgPolicyPayment;
		protected System.Web.UI.WebControls.DataGrid dgPayment;		
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		//protected Cms.CmsWeb.Controls.CmsButton btnGoBack;
		protected Cms.CmsWeb.Controls.CmsButton btnPaymentBreakdown;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidPayeeID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyRowCount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web .UI.HtmlControls.HtmlInputHidden hidTRANSACTION_CATEGORY;   
		//protected System.Web.UI.WebControls.Label capTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtTotalRI_Reserve;		
		//protected System.Web.UI.WebControls.Label capTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NUMBER;
		protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;
		protected System.Web.UI.WebControls.Label capCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvCLAIM_DESCRIPTION;
		//protected System.Web.UI.WebControls.Label capPAYEE;
		//protected System.Web.UI.WebControls.ListBox cmbPAYEE;
		protected System.Web.UI.WebControls.Label capACTION_ON_RECOVERY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCHECK_NUMBER;
//		protected System.Web.UI.WebControls.DropDownList cmbACTION_ON_RECOVERY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE;
		private double TotalOutstanding=0;
		private double TotalRIReserve=0;
		const string SubHeading = "SubHeading";
		const string VehicleText = "Vehicle # : ";
		public static string PaymentDataGridID="";
		public static string PolicyPaymentDataGridID="";
		const int POLICY_RECOVERY_TABLE=0;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPolicyPayment;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehiclePayment;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTotalPayments;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPayeeID;
		const int VEHICLE_RECOVERY_TABLE=1;
		protected System.Web.UI.WebControls.DropDownList cmbDrAccts;
		protected System.Web.UI.WebControls.DropDownList cmbCrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCrAccts;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_RECOVERY;
		public DataTable gDtOldData=null;
	
		protected string LabelPrefix = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="309_7";	

			btnPaymentBreakdown.CmsButtonClass		=	CmsButtonType.Read;
			btnPaymentBreakdown.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			//btnGoBack.CmsButtonClass		=	CmsButtonType.Read;
			//btnGoBack.PermissionString		=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddActivityRecovery"  ,System.Reflection.Assembly.GetExecutingAssembly());
			// Put user code to initialize the page here
			
			string strClaimStatus = GetClaimStatus();
			if(strClaimStatus == CLAIM_STATUS_CLOSED)
				btnSave.Visible=false;
			else
				btnSave.Visible=true;


			if ( !Page.IsPostBack)
			{
				PaymentDataGridID = dgPayment.ID;
				PolicyPaymentDataGridID = dgPolicyPayment.ID;
				SetCaptions();
				SetErrorMessages();
				GetQueryStringValues();
				GetClaimDetails();
				LoadDropDowns();
				btnBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");
				//btnGoBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");				
				//Function CompareAllOutstandingAndPayment() Uncommented For Itrack Issue #5359. 
				btnSave.Attributes.Add("onClick","javascript: return CompareAllOutstandingAndPayment();");
				btnPaymentBreakdown.Attributes.Add("onClick","javascript: return OpenWindow();");				
				cmbPAYMENT_METHOD.Attributes.Add("onChange","javascript:return cmbPAYMENT_METHOD_Change();");
				GetOldData();	
				//GetActivityDate();
				GetActivityDescription();		
//				cmbACTION_ON_RECOVERY_SelectedIndexChanged(null,null);
				LoadData(gDtOldData);
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
				hidSTATE_ID.Value = dr["STATE_ID"].ToString();
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
//			DataTable dtTransactionCodes = ClsDefaultValues.GetDefaultValuesDetails((int)enumClaimDefaultValues.CLAIM_TRANSACTION_CODE,(int)enumTransactionLookup.RECOVERY);  
//			if(dtTransactionCodes!=null && dtTransactionCodes.Rows.Count>0)
//			{
//				cmbACTION_ON_RECOVERY.DataSource = dtTransactionCodes;
//				cmbACTION_ON_RECOVERY.DataTextField="DETAIL_TYPE_DESCRIPTION";
//				cmbACTION_ON_RECOVERY.DataValueField="DETAIL_TYPE_ID";
//				cmbACTION_ON_RECOVERY.DataBind();
//				cmbACTION_ON_RECOVERY.SelectedIndex = 1 ;//Set listbox selected to Reduce Reserves by payment amount 
//			}
			/*ClsActivityExpense objExpense = new ClsActivityExpense();			
			cmbPAYEE.DataSource =  objExpense.GetClaimAllParties(hidCLAIM_ID.Value);
			cmbPAYEE.DataTextField="NAME";
			cmbPAYEE.DataValueField="PARTY_ID";
			cmbPAYEE.DataBind();*/
		}

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

		private void SetErrorMessages()
		{
			rfvPAYMENT_METHOD.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
			revCHECK_NUMBER.ValidationExpression  = aRegExpDoublePositiveNonZeroStartWithZeroForFedId;			
			csvCLAIM_DESCRIPTION.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("983");
            revCHECK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163"); 
			//rfvPAYEE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("821");
		}

		private void GetOldData()
		{
			DataSet dsOldData = ClsActivityRecovery.GetOldDataForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			//when the claim id is null/0/'', we have no data at claim payment table, 
			//we need to fetch data from reserve table as the user has come to this page for the first time
			if(dsOldData==null ||  dsOldData.Tables.Count<2 || dsOldData.Tables[0].Rows.Count<1 && dsOldData.Tables[1].Rows.Count<1)
			{
				dsOldData = ClsReserveDetails.GetOldDataForPage(hidCLAIM_ID.Value);
				if(dsOldData==null || dsOldData.Tables.Count<1)
				{
					lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
					lblMessage.Visible = true;
					dgPayment.Visible = false;
					dgPolicyPayment.Visible = false;
					trPolicyPayment.Attributes.Add("style","display:none");
					trTotalPayments.Attributes.Add("style","display:none");
					trVehiclePayment.Attributes.Add("style","display:none");
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
				if(dsOldData.Tables[0].Rows.Count>0)
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);
				else
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[1]);
				/*if(dsOldData.Tables.Count>2 && dsOldData.Tables[2].Rows.Count>0 && dsOldData.Tables[2].Rows[0]["PAYEE_PARTIES_ID"]!=null && dsOldData.Tables[2].Rows[0]["PAYEE_PARTIES_ID"].ToString()!="")
				{
					hidPayeeID.Value = dsOldData.Tables[2].Rows[0]["PAYEE_PARTIES_ID"].ToString();
					SelectPayees(hidPayeeID.Value);
				}*/

				//LoadData(dsOldData.Tables[0]);
				if(dsOldData.Tables[0].Rows.Count>0)
					LoadData(dsOldData.Tables[0]);
				else
					LoadData(dsOldData.Tables[1]);

				//gDtOldData=dsOldData.Tables[0];
				if(dsOldData.Tables[0].Rows.Count>0)
					gDtOldData=dsOldData.Tables[0];
				else
					gDtOldData=dsOldData.Tables[1];
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
//			capACTION_ON_RECOVERY.Text 	=		objResourceMgr.GetString("cmbACTION_ON_RECOVERY");
		}
		#endregion

		
		
		private void BindGrid(DataSet dsData)
		{
			if(dsData==null || dsData.Tables.Count<VEHICLE_RECOVERY_TABLE || (dsData.Tables[POLICY_RECOVERY_TABLE].Rows.Count<0 && dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows.Count<0))
			{
				lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
				lblMessage.Visible = true;
				dgPayment.Visible = false;
				return;
			}
			if(dsData.Tables[POLICY_RECOVERY_TABLE].Rows.Count>0)
			{
				if(dsData.Tables[POLICY_RECOVERY_TABLE].Rows[0]["STATE_ID"]!=null && dsData.Tables[POLICY_RECOVERY_TABLE].Rows[0]["STATE_ID"].ToString()!="")
					hidSTATE_ID.Value = dsData.Tables[POLICY_RECOVERY_TABLE].Rows[0]["STATE_ID"].ToString();
				hidPolicyRowCount.Value = dsData.Tables[POLICY_RECOVERY_TABLE].Rows.Count.ToString();			
				dgPolicyPayment.DataSource = dsData.Tables[POLICY_RECOVERY_TABLE];
				dgPolicyPayment.DataBind();	
			}
			else
			{
				dgPolicyPayment.Visible = false;
				trPolicyPayment.Attributes.Add("style","display:none");
			}

			if(dsData!=null && hidCustomerID.Value!="" && hidCustomerID.Value!="0" && hidPolicyID.Value!="" && hidPolicyID.Value!="0" && hidPolicyVersionID.Value!="" && hidPolicyVersionID.Value!="0" && dsData.Tables.Count>VEHICLE_RECOVERY_TABLE && dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows.Count>0)
			{
				if(hidSTATE_ID.Value!="" && dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows[0]["STATE_ID"]!=null && dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows[0]["STATE_ID"].ToString()!="")
					hidSTATE_ID.Value = dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows[0]["STATE_ID"].ToString();
				//Code for adding new subheading comes here
				string curVehicle,prevVehicle;
				TableRow row = new TableRow();
				int i=0;
				curVehicle = prevVehicle = "";
				for(i=0;i<dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows.Count;i++)
				{
					curVehicle = dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows[i]["VEHICLE_ID"].ToString();
					if(curVehicle !=  prevVehicle)
					{
						prevVehicle = curVehicle;
						DataRow dr = dsData.Tables[VEHICLE_RECOVERY_TABLE].NewRow();
						dr["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows[i]["VEHICLE"];
						//dr["COV_DESC"] = "SubHead";
						dr["LIMIT"] = SubHeading;
						dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows.InsertAt(dr,i);						
					}
				}
				hidVehicleRowCount.Value = dsData.Tables[VEHICLE_RECOVERY_TABLE].Rows.Count.ToString();
				dgPayment.DataSource = dsData.Tables[VEHICLE_RECOVERY_TABLE];
				dgPayment.DataBind();	
			}
			else
			{
				dgPayment.Visible = false;
				trVehiclePayment.Attributes.Add("style","display:none");
			}

			if(TotalOutstanding!=0 && TotalOutstanding!=0.0)
				txtTOTAL_OUTSTANDING.Text=Double.Parse(TotalOutstanding.ToString()).ToString("N");
				//txtTOTAL_OUTSTANDING.Text=String.Format("{0:,#,###}",Convert.ToInt64(TotalOutstanding.ToString()));
			if(TotalRIReserve!=0 && TotalRIReserve!=0.0)
				txtTotalRI_Reserve.Text=Double.Parse(TotalRIReserve.ToString()).ToString("N");
				//txtTotalRI_Reserve.Text=String.Format("{0:,#,###}",Convert.ToInt64(TotalRIReserve.ToString()));
			
			if(dgPayment.Visible==false && dgPolicyPayment.Visible==false)
			{
				trTotalPayments.Attributes.Add("style","display:none");
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("792");				
				lblMessage.Visible = true;
			}
			else
			{
				//Modified by Asfa (23-Apr-2008) - iTrack issue #4102
				if(hidSTATE_ID.Value == ((int)enumState.Indiana).ToString() || hidCustomerID.Value =="" || hidCustomerID.Value=="0")
				{
					HideShowColumns(dgPayment,MCCA_ATTACHMENT_TEXT,false);
					HideShowColumns(dgPolicyPayment,MCCA_ATTACHMENT_TEXT,false);
					HideShowColumns(dgPayment,MCCA_RESERVE_TEXT,false);
					HideShowColumns(dgPolicyPayment,MCCA_RESERVE_TEXT,false);
				}
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

		private void dgPayment_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			OnItemDataBound(sender,e,PaymentDataGridID.ToUpper());
		}

		private void dgPolicyPayment_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			OnItemDataBound(sender,e,PolicyPaymentDataGridID.ToUpper());
		}

		private void OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e,string dataGridID)
		{

			if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				Label lblOutstanding = (Label)e.Item.FindControl("lblOUTSTANDING");								
				Label lblRI_Reserve = (Label)e.Item.FindControl("lblRI_RESERVE");								
				Label lblMCCA_Attachment_Point = (Label)e.Item.FindControl("lblMCCA_ATTACHMENT_POINT");								
				Label lblMCCA_Applies = (Label)e.Item.FindControl("lblMCCA_APPLIES");
				
				
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
					
					if (DataBinder.Eval(e.Item.DataItem,"AMOUNT") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"AMOUNT"))!="0") 
						txtAMOUNT.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"AMOUNT"));
					else
						txtAMOUNT.Text = "";
					//Line Commented For Itrack Issue #5359.
					//txtAMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();");
					//((RangeValidator)(e.Item.FindControl("rngAMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");				
					//((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpCurrencyformat;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
					//Done for Itrack Issue 7016 on 3 Feb 2010
					//((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpDoublePositiveZero;
					((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ValidationExpression = aRegExpDoublePositiveNonZero;
					((RegularExpressionValidator)(e.Item.FindControl("revAMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				}
				if(dataGridID.ToUpper().Equals(PaymentDataGridID.ToUpper()))
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
				else
				{
					Label lblCOV_ID = (Label)e.Item.FindControl("lblCOV_ID");
					if(lblCOV_ID!=null)
					{
						switch(lblCOV_ID.Text)
						{
							case PIP_COV_ID:
							case SLL_COV_ID:
							case RLCSL_COV_ID:
							case UNINSURED_MOTORISTS_COV_ID_IN:
							case UNINSURED_MOTORISTS_COV_ID_MI:
							case UNDERINSURED_MOTORISTS_COV_ID_IN:
							case UNDERINSURED_MOTORISTS_COV_ID_MI:
								txtAMOUNT.Attributes.Add("style","display:none");
								break;
							default:
								break;
						}
					}					
				}
				if (DataBinder.Eval(e.Item.DataItem,"RI_RESERVE") != System.DBNull.Value)
					lblRI_Reserve.Text = Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"RI_RESERVE"))).ToString("N");
				else
					lblRI_Reserve.Text = "";

				if(lblMCCA_Attachment_Point!=null)
				{
					if (DataBinder.Eval(e.Item.DataItem,"MCCA_ATTACHMENT_POINT") != System.DBNull.Value)
						lblMCCA_Attachment_Point.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"MCCA_ATTACHMENT_POINT"));
					else
						lblMCCA_Attachment_Point.Text = "";
				}

				if(lblMCCA_Applies!=null)
				{
					if (DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES") != System.DBNull.Value)
						lblMCCA_Applies.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"MCCA_APPLIES"));
					else
						lblMCCA_Applies.Text = "";
				}

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
			this.dgPolicyPayment.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPolicyPayment_ItemDataBound);
			this.dgPayment.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPayment_ItemDataBound);
//			this.cmbACTION_ON_RECOVERY.SelectedIndexChanged += new System.EventHandler(this.cmbACTION_ON_RECOVERY_SelectedIndexChanged);
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

			if(Request["ACTION_ON_RECOVERY"]!=null && Request["ACTION_ON_RECOVERY"].ToString()!="")
				hidACTION_ON_RECOVERY.Value = Request["ACTION_ON_RECOVERY"].ToString();
			else
				hidACTION_ON_RECOVERY.Value = "0";          
           
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
					}
					//Model Object
					ClsActivityRecoveryInfo objActivityRecoveryInfo = new	 ClsActivityRecoveryInfo();
					
					TextBox txtAMOUNT = (TextBox)(dgi.FindControl("txtAMOUNT"));
					
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));
					Label lblRECOVERY_ID = (Label)(dgi.FindControl("lblRECOVERY_ID"));
					Label lblVEHICLE_ID = (Label)(dgi.FindControl("lblVEHICLE_ID"));
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

		/*private void SetClaimTop()
		{
			if(GetCustomerID()!=string.Empty)
				cltClaimTop.CustomerID = int.Parse(GetCustomerID());
			if(GetPolicyID()!=string.Empty)
				cltClaimTop.PolicyID = int.Parse(GetPolicyID());
			if(GetPolicyVersionID()!=string.Empty)
				cltClaimTop.PolicyVersionID = int.Parse(GetPolicyVersionID());
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
				cltClaimTop.ClaimID = int.Parse(hidCLAIM_ID.Value);
			if(GetLOBID()!=string.Empty)
				cltClaimTop.LobID = int.Parse(GetLOBID());        

			cltClaimTop.ShowHeaderBand ="Claim";
			cltClaimTop.Visible = true;        
		}*/

		/*private string GetPayees()
		{
			string strPAYEE="";
			if(cmbPAYEE.Items.Count>0)
			{
				foreach(ListItem li in cmbPAYEE.Items)
				{
					if(li.Selected)
					{
						strPAYEE = strPAYEE + li.Value + ",";
					}
				}
				if(strPAYEE.Length>0)
					strPAYEE = strPAYEE.Substring(0,(strPAYEE.Length-1));
			}
			return strPAYEE;
		}*/		
		
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				int RECOVERY_ACTION;
				//string strPAYEE;
				ClsActivityRecovery objActivityRecovery = new ClsActivityRecovery();
				ArrayList PaymentArrayList = new ArrayList();
				PopulateArray(PaymentArrayList,dgPolicyPayment);
				PopulateArray(PaymentArrayList,dgPayment);

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
					//strPAYEE = GetPayees();
					//ClsClaimsNotification.UpdateClaimDescription(hidCLAIM_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					ClsActivity objActivity = new ClsActivity();
					objActivity.UpdateActivityDescription(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					if(hidACTION_ON_RECOVERY.Value!="")					
						RECOVERY_ACTION = int.Parse(hidACTION_ON_RECOVERY.Value);
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
