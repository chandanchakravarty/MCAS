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
	public class PaymentDetails : Cms.Claims.ClaimBase//System.Web.UI.Page//
	{
		
		System.Resources.ResourceManager objResourceMgr;	
		
		
		
		protected int TotalDeductible=0;
		protected System.Web.UI.WebControls.Label lblTitle;
		//protected System.Web.UI.WebControls.TextBox txtTOTAL_RI_RESERVE;
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
		//protected System.Web.UI.WebControls.Label capTOTAL_OUTSTANDING;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_OUTSTANDING;
		//protected System.Web.UI.WebControls.Label capTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_PAYMENT;
		protected System.Web.UI.WebControls.Label capCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCLAIM_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvCLAIM_DESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTION_ON_PAYMENT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCHECK_NUMBER;
		//protected System.Web.UI.WebControls.Label capPAYEE;
		//protected System.Web.UI.WebControls.ListBox cmbPAYEE;
		//protected System.Web.UI.WebControls.Label capACTION_ON_PAYMENT;
//		protected System.Web.UI.WebControls.DropDownList cmbACTION_ON_PAYMENT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE;
		private double TotalOutstanding=0;
		const string SubHeading = "SubHeading";
		const string VehicleText = "Vehicle # : ";
		public static string PaymentDataGridID="";
		public static string PolicyPaymentDataGridID="";
		const int POLICY_PAYMENT_TABLE=0;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPolicyPayment;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehiclePayment;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTotalPayments;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPayeeID;
		protected System.Web.UI.WebControls.TemplateColumn objTemplateColumn;
		protected System.Web.UI.WebControls.DropDownList cmbDrAccts;
		protected System.Web.UI.WebControls.DropDownList cmbCrAccts;
//		public DataTable gDtOldData=null;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDrAccts;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCrAccts;
		const int VEHICLE_PAYMENT_TABLE=1;
		protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
		protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;		
		protected enum enumACTION_ON_PAYMENT
		{
			/*CLOSE_TO_ZERO=11783,
			MAINTAIN_RESERVES_AND_STATUS=11784,
			REDUCE_RESERVES_BY_PAYMENT_AMOUNT=11785,
			REVIEW_RESERVES_MANUALLY=11786*/
			PAID_LOSS_PARTIAL = 76,
			PAID_LOSS_FINAL = 77,
			PAID_LOSS_ADDITIONAL = 78
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			string strRedirectPage = CheckLOBAndRedirect();
			if(strRedirectPage!=string.Empty && strRedirectPage!="")
				Server.Transfer(strRedirectPage,true);
			base.ScreenId="309_1_0_0";	

			btnPaymentBreakdown.CmsButtonClass		=	CmsButtonType.Read;
			btnPaymentBreakdown.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnBack.CmsButtonClass		=	CmsButtonType.Read;
			btnBack.PermissionString		=	gstrSecurityXML;
			
			//btnGoBack.CmsButtonClass		=	CmsButtonType.Read;
			//btnGoBack.PermissionString		=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.PaymentDetails"  ,System.Reflection.Assembly.GetExecutingAssembly());
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

//				LoadDropDowns();
				btnBack.Attributes.Add("onClick","javascript: return GoBack('ActivityTab.aspx');");
				btnSave.Attributes.Add("onClick","javascript: return CompareAllOutstandingAndPayment();");
				btnPaymentBreakdown.Attributes.Add("onClick","javascript: return OpenWindow();");
				cmbPAYMENT_METHOD.Attributes.Add("onChange","javascript:return cmbPAYMENT_METHOD_Change();");
				LoadAcntgDropDowns();
				GetOldData();	
				GetActivityDescription();
//				LoadData(gDtOldData);
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
				hidSTATE_ID.Value = dr["STATE_ID"].ToString();
			}
		}

//		private void LoadDropDowns()
//		{
//			DataTable dtTransactionCodes = ClsDefaultValues.GetDefaultValuesDetails((int)enumClaimDefaultValues.CLAIM_TRANSACTION_CODE,(int)enumTransactionLookup.CLAIM_PAYMENT);  
//			if(dtTransactionCodes!=null && dtTransactionCodes.Rows.Count>0)
//			{
//				cmbACTION_ON_PAYMENT.DataSource = dtTransactionCodes;
//				cmbACTION_ON_PAYMENT.DataTextField="DETAIL_TYPE_DESCRIPTION";
//				cmbACTION_ON_PAYMENT.DataValueField="DETAIL_TYPE_ID";
//				cmbACTION_ON_PAYMENT.DataBind();
//				cmbACTION_ON_PAYMENT.SelectedIndex = 0 ;//Set listbox selected to Reduce Reserves by payment amount 
//			}
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

		private void SetErrorMessages()
		{
			rfvPAYMENT_METHOD.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("786");
			revCHECK_NUMBER.ValidationExpression  = aRegExpDoublePositiveNonZeroStartWithZeroForFedId;			
			revCHECK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");   			  
			csvCLAIM_DESCRIPTION.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("983");
			//rfvPAYEE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("821");
		}

		private void GetOldData()
		{
			DataSet dsOldData = ClsPayment.GetOldDataForPageControls(hidCLAIM_ID.Value,hidACTIVITY_ID.Value);
			//when the claim id is null/0/'', we have no data at claim payment table, 
			//we need to fetch data from reserve table as the user has come to this page for the first time
			if(dsOldData==null ||  dsOldData.Tables.Count<2 || dsOldData.Tables[0].Rows.Count<1 && dsOldData.Tables[1].Rows.Count<1)//Added condition for Itrack Issue 5548 on 22 June 2009
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
					trBody.Attributes.Add("style","display:none");
					btnSave.Visible = false;
					return;
				}
				//Add columns for Payment Amount and Action on Payment as they are not present at reserve table..
				for(int i=0;i<dsOldData.Tables.Count;i++)
				{
					DataColumn dtCol = new DataColumn("PAYMENT_AMOUNT");
					dsOldData.Tables[i].Columns.Add(dtCol);
					//dtCol = new DataColumn("ACTION_ON_PAYMENT");
					//dsOldData.Tables[i].Columns.Add(dtCol);
					dtCol = new DataColumn("PAYMENT_ID");
					dsOldData.Tables[i].Columns.Add(dtCol);				
				}
				hidOldData.Value = "";
			}	
			else
			{
				//hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);
				//dsOldData.Tables[0] refers to Policy Level Coverages. Earlier when dsOldData.Tables[0] was blank hidOldData would have been blank which is being used to set the Payee Details tab.So as a result Payee Tab was not coming
				if(dsOldData.Tables[0].Rows.Count>0)
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[0]);
				else
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsOldData.Tables[1]);
				/*if(dsOldData.Tables.Count>2 && dsOldData.Tables[2].Rows.Count>0 && dsOldData.Tables[2].Rows[0]["PAYEE_PARTIES_ID"]!=null && dsOldData.Tables[2].Rows[0]["PAYEE_PARTIES_ID"].ToString()!="")
				{
					hidPayeeID.Value = dsOldData.Tables[2].Rows[0]["PAYEE_PARTIES_ID"].ToString();
					SelectPayees(hidPayeeID.Value);
				}*/

				//LoadData(dsOldData.Tables[0])
				if(dsOldData.Tables[0].Rows.Count>0)
				 LoadData(dsOldData.Tables[0]);
				else
				 LoadData(dsOldData.Tables[1]);
//				gDtOldData=dsOldData.Tables[0];
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
				if(dtOldData.Rows[0]["ACTION_ON_PAYMENT"]!=null && dtOldData.Rows[0]["ACTION_ON_PAYMENT"].ToString()!="")
					hidACTION_ON_PAYMENT.Value = dtOldData.Rows[0]["ACTION_ON_PAYMENT"].ToString();
					//cmbACTION_ON_PAYMENT.SelectedValue = dtOldData.Rows[0]["ACTION_ON_PAYMENT"].ToString();

				if(dtOldData.Rows[0]["DrAccts"]!=null && dtOldData.Rows[0]["DrAccts"].ToString()!="")
					cmbDrAccts.SelectedValue = dtOldData.Rows[0]["DrAccts"].ToString();

				if(dtOldData.Rows[0]["CrAccts"]!=null && dtOldData.Rows[0]["CrAccts"].ToString()!="")
					cmbCrAccts.SelectedValue = dtOldData.Rows[0]["CrAccts"].ToString();

				if(dtOldData.Rows[0]["PAYMENT_METHOD"]!=null && dtOldData.Rows[0]["PAYMENT_METHOD"].ToString()!="")
					cmbPAYMENT_METHOD.SelectedValue = dtOldData.Rows[0]["PAYMENT_METHOD"].ToString();

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
			//capACTIVITY_DATE.Text 		=		objResourceMgr.GetString("lblACTIVITY_DATE");			
			//capTOTAL_OUTSTANDING.Text	=		objResourceMgr.GetString("txtTOTAL_OUTSTANDING");
			//capTOTAL_PAYMENT.Text 		=		objResourceMgr.GetString("txtTOTAL_PAYMENT");
			capCLAIM_DESCRIPTION.Text 		=		objResourceMgr.GetString("txtCLAIM_DESCRIPTION");
			capPAYMENT_METHOD.Text	=		objResourceMgr.GetString("txtPAYMENT_METHOD");
			//capPAYEE.Text 				=		objResourceMgr.GetString("cmbPAYEE");
			//capACTION_ON_PAYMENT.Text 	=		objResourceMgr.GetString("cmbACTION_ON_PAYMENT");
		}
		#endregion

		
		
		private void BindGrid(DataSet dsData)
		{
			if(dsData==null || dsData.Tables.Count<VEHICLE_PAYMENT_TABLE || (dsData.Tables[POLICY_PAYMENT_TABLE].Rows.Count<0 && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count<0))
			{
				lblMessage.Text = ClsMessages.FetchGeneralMessage("792");
				lblMessage.Visible = true;
				dgPayment.Visible = false;
				return;
			}
			if(dsData.Tables[POLICY_PAYMENT_TABLE].Rows.Count>0)
			{
				hidPolicyRowCount.Value = dsData.Tables[POLICY_PAYMENT_TABLE].Rows.Count.ToString();			
				dgPolicyPayment.DataSource = dsData.Tables[POLICY_PAYMENT_TABLE];
				dgPolicyPayment.DataBind();	
				if(dsData.Tables[POLICY_PAYMENT_TABLE].Rows[0]["STATE_ID"]!=null && dsData.Tables[POLICY_PAYMENT_TABLE].Rows[0]["STATE_ID"].ToString()!="")
					hidSTATE_ID.Value = dsData.Tables[POLICY_PAYMENT_TABLE].Rows[0]["STATE_ID"].ToString();
			}
			else
			{
				dgPolicyPayment.Visible = false;
				trPolicyPayment.Attributes.Add("style","display:none");
			}

			if(dsData!=null && hidCustomerID.Value!="" && hidCustomerID.Value!="0" && hidPolicyID.Value!="" && hidPolicyID.Value!="0" && hidPolicyVersionID.Value!="" && hidPolicyVersionID.Value!="0" && dsData.Tables.Count>VEHICLE_PAYMENT_TABLE && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count>0)
			{
				if(hidSTATE_ID.Value!="" && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[0]["STATE_ID"]!=null && dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[0]["STATE_ID"].ToString()!="")
					hidSTATE_ID.Value = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[0]["STATE_ID"].ToString();
				//Code for adding new subheading comes here
				string curVehicle,prevVehicle;
				TableRow row = new TableRow();
				int i=0;
				curVehicle = prevVehicle = "";
				for(i=0;i<dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count;i++)
				{
					curVehicle = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[i]["VEHICLE_ID"].ToString();
					if(curVehicle !=  prevVehicle)
					{
						prevVehicle = curVehicle;
						DataRow dr = dsData.Tables[VEHICLE_PAYMENT_TABLE].NewRow();
						dr["COV_DESC"]= VehicleText + dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows[i]["VEHICLE"];
						//dr["COV_DESC"] = "SubHead";
						dr["LIMIT"] = SubHeading;
						dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.InsertAt(dr,i);						
					}
				}
				hidVehicleRowCount.Value = dsData.Tables[VEHICLE_PAYMENT_TABLE].Rows.Count.ToString();
				dgPayment.DataSource = dsData.Tables[VEHICLE_PAYMENT_TABLE];
				dgPayment.DataBind();	
			}
			else
			{
				dgPayment.Visible = false;
				trVehiclePayment.Attributes.Add("style","display:none");
			}

			if(TotalOutstanding!=0 && TotalOutstanding!=0.0)
				txtTOTAL_OUTSTANDING.Text= Double.Parse(TotalOutstanding.ToString()).ToString("N");
				//txtTOTAL_OUTSTANDING.Text=String.Format("{0:,#,###.##}",TotalOutstanding.ToString());
			if(dgPayment.Visible==false && dgPolicyPayment.Visible==false)
			{
				trTotalPayments.Attributes.Add("style","display:none");
				trBody.Attributes.Add("style","display:none");
				btnSave.Visible = false;
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
				
				TextBox txtPAYMENT_AMOUNT = (TextBox)(e.Item.FindControl("txtPAYMENT_AMOUNT"));				
				if(txtPAYMENT_AMOUNT!=null)
				{
					txtPAYMENT_AMOUNT.Attributes.Add("onBlur","javascript:CalculateTotalPayment();CompareOutstandingAndPayment('" + lblOutstanding.ClientID + "','" + txtPAYMENT_AMOUNT.ClientID + "');");
					if (DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT"))!="0") 
						txtPAYMENT_AMOUNT.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"PAYMENT_AMOUNT"));
					else
						txtPAYMENT_AMOUNT.Text = "";
				}				

				((RangeValidator)(e.Item.FindControl("rngPAYMENT_AMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
				//Done for Itrack Issue 6516 on 15 Oct 09
				//((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ValidationExpression = aRegExpDoublePositiveZero;//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ValidationExpression = aRegExpDoublePositiveNonZero;
				((RegularExpressionValidator)(e.Item.FindControl("revPAYMENT_AMOUNT"))).ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");//Done by Sibin on 2 Feb 09 for Itrack Issue 5385
				
				//As per discussion with Gagan on 27-Sept-2007 "Deductible should be shown for Policy Coverages too."
				Label lblLIMIT = (Label)e.Item.FindControl("lblLIMIT");		
				Label lblDeductible = (Label)e.Item.FindControl("lblDEDUCTIBLE");

				if(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))!="" && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))!="0")
					lblDeductible.Text= Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))).ToString("N");
				else
					lblDeductible.Text = "";

				if(dataGridID.ToUpper().Equals(PaymentDataGridID.ToUpper()))
				{
//					Label lblLIMIT = (Label)e.Item.FindControl("lblLIMIT");		
//					Label lblDeductible = (Label)e.Item.FindControl("lblDEDUCTIBLE");					

//					if(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE") != System.DBNull.Value && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))!="" && Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))!="0")
//						lblDeductible.Text= Double.Parse(Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE"))).ToString("N");
//					else
//						lblDeductible.Text = "";



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
					Label lblCOV_ID = (Label)e.Item.FindControl("lblCOV_ID");
					if(lblCOV_ID!=null)
					{
						switch(lblCOV_ID.Text)
						{
							//case PIP_COV_ID:
							case SLL_COV_ID:
							case RLCSL_COV_ID:
							case UNINSURED_MOTORISTS_COV_ID_IN:
							case UNINSURED_MOTORISTS_COV_ID_MI:
							case UNDERINSURED_MOTORISTS_COV_ID_IN:
							case UNDERINSURED_MOTORISTS_COV_ID_MI:
								txtPAYMENT_AMOUNT.Attributes.Add("style","display:none");
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
					txtPAYMENT_AMOUNT.Attributes.Add("readOnly","true");
					txtPAYMENT_AMOUNT.Attributes.Add("style","background-color:#C0C0C0;");
				}				
				//TextBox txt = (TextBox)(e.Item.FindControl("txtRI_RESERVE"));
				//((TextBox)(e.Item.FindControl("txtRI_RESERVE"))).Attributes.Add("onBlur","javascript:CalculateTotalRIReserve();");
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
			//this.cmbACTION_ON_PAYMENT.SelectedIndexChanged += new System.EventHandler(this.cmbACTION_ON_PAYMENT_SelectedIndexChanged);
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
					ClsPaymentInfo objPaymentInfo = new	 ClsPaymentInfo();
					
					TextBox txtPAYMENT_AMOUNT = (TextBox)(dgi.FindControl("txtPAYMENT_AMOUNT"));
					
					Label lblRESERVE_ID = (Label)(dgi.FindControl("lblRESERVE_ID"));
					Label lblPAYMENT_ID = (Label)(dgi.FindControl("lblPAYMENT_ID"));
					Label lblVEHICLE_ID = (Label)(dgi.FindControl("lblVEHICLE_ID"));
					//Added for Itrack Issue 7663 on 19 Aug 2010
					Label lblACTUAL_RISK_ID = (Label)(dgi.FindControl("lblACTUAL_RISK_ID"));
					Label lblACTUAL_RISK_TYPE = (Label)(dgi.FindControl("lblACTUAL_RISK_TYPE"));
					
//					if(cmbACTION_ON_PAYMENT.SelectedItem!=null && cmbACTION_ON_PAYMENT.SelectedItem.Value!="")
//						objPaymentInfo.ACTION_ON_PAYMENT = int.Parse(cmbACTION_ON_PAYMENT.SelectedItem.Value);

					if(hidACTION_ON_PAYMENT.Value!="")
						objPaymentInfo.ACTION_ON_PAYMENT = int.Parse(hidACTION_ON_PAYMENT.Value);
					objPaymentInfo.IS_ACTIVE="Y";
					objPaymentInfo.CREATED_BY = int.Parse(GetUserId());
					objPaymentInfo.CREATED_DATETIME = System.DateTime.Now;
					objPaymentInfo.MODIFIED_BY = int.Parse(GetUserId());
					objPaymentInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
					objPaymentInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
					objPaymentInfo.ACTIVITY_ID = int.Parse(hidACTIVITY_ID.Value);
					//Added for Itrack Issue 7663 on 19 Aug 2010
					objPaymentInfo.ACTUAL_RISK_ID = int.Parse(lblACTUAL_RISK_ID.Text);
					objPaymentInfo.ACTUAL_RISK_TYPE = lblACTUAL_RISK_TYPE.Text;

					if(lblRESERVE_ID.Text.Trim()!="")
						objPaymentInfo.RESERVE_ID = int.Parse(lblRESERVE_ID.Text.Trim());
					if(txtPAYMENT_AMOUNT.Text.Trim()!="")
						objPaymentInfo.PAYMENT_AMOUNT = Convert.ToDouble(txtPAYMENT_AMOUNT.Text.Trim());
					if(lblPAYMENT_ID.Text.Trim()!="")
						objPaymentInfo.PAYMENT_ID = int.Parse(lblPAYMENT_ID.Text.Trim());					
					if(lblVEHICLE_ID!=null && lblVEHICLE_ID.Text.Trim()!="")
						objPaymentInfo.VEHICLE_ID = int.Parse(lblVEHICLE_ID.Text.Trim());

					if(cmbDrAccts.SelectedItem!=null && cmbDrAccts.SelectedItem.Value!="")
						objPaymentInfo.DRACCTS = Convert.ToInt32(cmbDrAccts.SelectedValue);

					if(cmbCrAccts.SelectedItem!=null && cmbCrAccts.SelectedItem.Value!="")
						objPaymentInfo.CRACCTS = Convert.ToInt32(cmbCrAccts.SelectedValue);					

					if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="")
						objPaymentInfo.PAYMENT_METHOD = Convert.ToInt32(cmbPAYMENT_METHOD.SelectedValue);					

					if(cmbPAYMENT_METHOD.SelectedItem!=null && cmbPAYMENT_METHOD.SelectedItem.Value!="" && cmbPAYMENT_METHOD.SelectedItem.Value=="11979")
						objPaymentInfo.CHECK_NUMBER = txtCHECK_NUMBER.Text.Trim() ;					

					aPaymentList.Add(objPaymentInfo);
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
				strReservePage=WATERCRAFT_PAYMENT_PAGE;
			else if(strLOB_ID==((int)enumLOB.REDW).ToString())
				strReservePage=RENTAL_PAYMENT_PAGE;
			else if(strLOB_ID==((int)enumLOB.HOME).ToString())
				strReservePage=HOME_PAYMENT_PAGE;
			else if(strLOB_ID==((int)enumLOB.CYCL).ToString())
				strReservePage=MOTOR_PAYMENT_PAGE;
			else if(strLOB_ID==((int)enumLOB.UMB).ToString())
				strReservePage=UMB_PAYMENT_PAGE;
			else
				strReservePage = "";
			return strReservePage;
		}
		
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				int PAYMENT_ACTION;
				//string strPAYEE;
				ClsPayment objPayment = new ClsPayment();
				ArrayList PaymentArrayList = new ArrayList();
				PopulateArray(PaymentArrayList,dgPolicyPayment);
				PopulateArray(PaymentArrayList,dgPayment);

				if(hidOldData.Value=="" || hidOldData.Value=="0")
				{
					intRetVal = objPayment.Add(PaymentArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");						
				}
				else
				{
					intRetVal = objPayment.Update(PaymentArrayList);
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"31");					
				}					
				if(intRetVal>0)
				{			
					//strPAYEE = GetPayees();
					//ClsClaimsNotification.UpdateClaimDescription(hidCLAIM_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
					ClsActivity objActivity = new ClsActivity();
					objActivity.UpdateActivityDescription(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,txtCLAIM_DESCRIPTION.Text.Trim());
//					if(cmbACTION_ON_PAYMENT.SelectedItem!=null && cmbACTION_ON_PAYMENT.SelectedItem.Value!="")
//						PAYMENT_ACTION = int.Parse(cmbACTION_ON_PAYMENT.SelectedItem.Value);
					if(hidACTION_ON_PAYMENT.Value!="")
						PAYMENT_ACTION = int.Parse(hidACTION_ON_PAYMENT.Value);
					else
						PAYMENT_ACTION = -1;
					ClsPayment.UpdateReserveActivity(hidCLAIM_ID.Value,hidACTIVITY_ID.Value,PAYMENT_ACTION.ToString());					
					//RegisterStartupScript("ReLoadTab","<script>GoBack('PaymentDetailsTab.aspx');</script>");			
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
			/*cmbPAYMENT_METHOD.DataSource =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PYMTD");  //Action on Payment Lookup
			cmbPAYMENT_METHOD.DataTextField="LookupDesc";
			cmbPAYMENT_METHOD.DataValueField="LookupID";
			cmbPAYMENT_METHOD.DataBind();*/
			ClsPayment objPayment = new ClsPayment();
			objPayment.PopulatePaymentMethodDropDown(cmbPAYMENT_METHOD,"PYMTD",((int)Cms.CmsWeb.cmsbase.enumPaymentMethod.EFT).ToString());
			//GetOldData();
		}		
	}
}
