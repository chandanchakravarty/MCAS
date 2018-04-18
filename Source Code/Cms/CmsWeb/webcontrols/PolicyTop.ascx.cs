/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		08-12-2005
<End Date				: -	
<Description			: - 	Provide the Policy Details on the Policy Process.
<Review Date			: - 
<Reviewed By			: - 	
*******************************************************************************************/ 

namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Resources;
	using System.Reflection;
	using Cms.BusinessLayer;
	using System.Globalization;
	using Cms.BusinessLayer.BlApplication;
	using Cms.BusinessLayer.BlCommon;
	using Cms.BusinessLayer.BlAccount;
	
	/// <summary>
	/// Provide the policy details.
	/// </summary>
	public abstract class PolicyTop : System.Web.UI.UserControl
	{
		
		public string att_note="";
		protected int intCustomerID;
		protected int intPolicyID;
		protected int intPolicyVersionID;
		public System.Text.StringBuilder sbClaimLink = new System.Text.StringBuilder();
	
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Image AspImageNote1;
				
		protected System.Web.UI.WebControls.Label capName;
		protected System.Web.UI.WebControls.Label capFullName;
		protected System.Web.UI.WebControls.Label capNumber;
		protected System.Web.UI.WebControls.Label capPolicyNumber;
		protected System.Web.UI.WebControls.Label capVersion;
		protected System.Web.UI.WebControls.Label capPolicyVersion;
		protected System.Web.UI.WebControls.Label capEffectiveDate;
		protected System.Web.UI.WebControls.Label capPolicyEffectiveDate;
		protected System.Web.UI.WebControls.Label capExpirationDate;
		protected System.Web.UI.WebControls.Label capPolicyExpirationDate;
		protected System.Web.UI.WebControls.Label capInceptionDate;
		protected System.Web.UI.WebControls.Label capPolicyInceptionDate;
		protected System.Web.UI.WebControls.Label capAgency;
		protected System.Web.UI.WebControls.Label capPolicyAgency;
		protected System.Web.UI.WebControls.Label capStatus;
		protected System.Web.UI.WebControls.Label capPolicyStatus;
		protected System.Web.UI.WebControls.Label capState;
		protected System.Web.UI.WebControls.Label capPolicyState;
		protected System.Web.UI.WebControls.Label capLOB;
		protected System.Web.UI.WebControls.Label capPolicyLOB;
		protected System.Web.UI.WebControls.Label capSLOB;
		protected System.Web.UI.WebControls.Label capPolicySLOB;
		protected System.Web.UI.WebControls.Label capTermMonths;
		protected System.Web.UI.WebControls.Label capPolicyTermsMonths;
		protected System.Web.UI.WebControls.Label capCSR;
		protected System.Web.UI.WebControls.Label capPolicyCSR;
		protected System.Web.UI.WebControls.Label capBillType;
		protected System.Web.UI.WebControls.Label capPolicyBillType;
		protected System.Web.UI.WebControls.Label capSignature;
		protected System.Web.UI.WebControls.Label capPolicySignature;
		protected System.Web.UI.WebControls.Label capUnderWriter;
		protected System.Web.UI.WebControls.Label capPolicyUnderWriter;
		protected System.Web.UI.WebControls.Label capInstallmentPlan;
		protected System.Web.UI.WebControls.Label capPolicyInstallmentPlan;
		protected System.Web.UI.WebControls.Label capChargeOffPremium;
		protected System.Web.UI.WebControls.Label capPolicyChargeOffPremium;
		protected System.Web.UI.WebControls.Label capReceivedPremium;
		protected System.Web.UI.WebControls.Label capPolicyReceivedPremium;
		protected System.Web.UI.WebControls.Label capBonus;
		protected System.Web.UI.WebControls.Label capPolicyBonus;
		protected System.Web.UI.WebControls.Label capYear;
		protected System.Web.UI.WebControls.Label capPolicyYear;
		protected System.Web.UI.WebControls.Label capClaimText;
        protected System.Web.UI.WebControls.Label capheader;
		
		protected System.Web.UI.WebControls.DropDownList cmbPolicyTermMethods;
		protected System.Web.UI.WebControls.DropDownList cmbBillType;
		protected System.Web.UI.WebControls.DropDownList cmbUnderWriter;
		protected System.Web.UI.WebControls.DropDownList cmbInstallmentPlan;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAgencyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyTermMethods;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBillType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUnderWriter;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidInstallPlanID;
		protected System.Web.UI.WebControls.Image CustomerDetail;
		protected System.Web.UI.HtmlControls.HtmlTableRow trClaimRow;
		protected string colorScheme;

		//Used for flagging whether policy,customer and version should be retreived from request or not
		public bool UseRequestVariables = true;		//By default it will be retreived from request

		

		/// <summary>
		/// Set the Customer ID. 
		/// </summary>
		public  int CustomerID
		{
			get
			{
				return intCustomerID;
			}
			set 
			{
				intCustomerID = value;
			}
		}

		public string GetSystemId()
		{
			if(Session["systemId"] == null)
			{
				return "";
			}
			return Session["systemId"].ToString(); 
				
		}

		/// <summary>
		/// Set the Policy ID.
		/// </summary>
		public  int PolicyID
		{
			
			get
			{
				return intPolicyID;
			}
			set 
			{
				intPolicyID = value;
			}
		}

		/// <summary>
		/// Set the Policy Version ID.
		/// </summary>
		public  int PolicyVersionID 
		{
			get
			{
				return intPolicyVersionID;
			}
			set
			{
				intPolicyVersionID = value; 
			}
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
            CallPageLoad();		}

		/// <summary>
		/// It will set the functions in page load event.
		/// </summary>
		public void CallPageLoad()
		{
			if (UseRequestVariables)
				SetRequestVariable();	//Retreiving the values of policy , version and customer from request
      

			SetCaptions();
			ShowPolicyHeaderDetails();
			SetPolicyStatusInSession();
			//FillDropDownAndDisplay();
            CustomerDetail.ToolTip = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1930");
			//Setting the Image of Customer Assistant
			colorScheme =((cmsbase)this.Page).GetColorScheme();
			CustomerDetail.ImageUrl="~/cmsweb/Images" + colorScheme  + "/Customer_Ass.gif";
		}

		public void RefreshPolicy()
		{
			SetCaptions();
			ShowPolicyHeaderDetails();
			SetPolicyStatusInSession();
			//FillDropDownAndDisplay();
		}

		/// <summary>
		/// Shows the details of policy on policy top control
		/// </summary>
		private void ShowPolicyHeaderDetails()
		{
			
			Cms.BusinessLayer.BlClient.ClsCustomer objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();
			
			DataSet ds = objCustomer.GetPolicyHeaderDetails(intCustomerID, intPolicyID, intPolicyVersionID);
			
			if ( ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				System.Data.DataRow dr = ds.Tables[0].Rows[0];
				
				hidLOB.Value=dr["POLICY_LOB"].ToString(); 
				hidAgencyID.Value = dr["AGENCY_ID"].ToString(); 
				hidBillType.Value =  dr["BILL_TYPE"].ToString();
				hidPolicyTermMethods.Value =  dr["APP_TERMS"].ToString();
				hidUnderWriter.Value =  dr["UNDERWRITER"].ToString();
				hidInstallPlanID.Value = dr["INSTALL_PLAN_ID"].ToString();

				capFullName.Text = dr["CUSTOMER_FULLNAME"].ToString();
				capPolicyStatus.Text = dr["POLICY_STATUS"].ToString(); 
				capPolicyState.Text = dr["STATE_NAME"].ToString(); 
				capPolicyLOB.Text = dr["LOB_DESC"].ToString(); 
				capPolicySLOB.Text = dr["SUB_LOB_DESC"].ToString();
				if (dr["SUB_LOB_DESC"] != System.DBNull.Value && capPolicySLOB.Text!="" )  
				{
					capPolicySLOB.Text = dr["SUB_LOB_DESC"].ToString();
				}
				else
				{
					capPolicySLOB.Text = "N.A.";
				}
				capPolicyBillType.Text= dr["BILL_TYPE"].ToString();
				capPolicyNumber.Text =  dr["POLICY_NUMBER"].ToString();
				capPolicyVersion.Text = dr["POLICY_DISP_VERSION"].ToString();
				capPolicyEffectiveDate.Text = dr["APP_EFFECTIVE_DATE"].ToString();
				capPolicyExpirationDate.Text = dr["APP_EXPIRATION_DATE"].ToString();
				capPolicyInceptionDate.Text = dr["APP_INCEPTION_DATE"].ToString();
				capPolicyAgency.Text = dr["AGENCY_DISPLAY_NAME"].ToString();
                				
				if (dr["CSRNAME"] != System.DBNull.Value && dr["CSRNAME"].ToString().Trim()!="")//Empty String check added by Charles on 10-Sep-09 for Itrack 6377
				{
					capPolicyCSR.Text = dr["CSRNAME"].ToString();
				}			
				else
				{
					capPolicyCSR.Text = "N.A.";
				}

				
				capPolicySignature.Text = dr["PROXY_SIGN_OBTAINED"].ToString(); 
				capPolicyChargeOffPremium.Text = dr["CHARGE_OFF_PRMIUM"].ToString(); 
				
				if (dr["RECEIVED_PRMIUM"] != System.DBNull.Value)  
				{
					capPolicyReceivedPremium.Text = dr["RECEIVED_PRMIUM"].ToString(); 
				}			
				else
				{
					capPolicyReceivedPremium.Text = "N.A.";
				}
				
				
				capPolicyBonus.Text = dr["COMPLETE_APP"].ToString(); 
				
				if (dr["YEAR_AT_CURR_RESI"] != System.DBNull.Value && Convert.ToDouble(dr["YEAR_AT_CURR_RESI"])!=0.0)  //0 check added by Charles for Itrack 6377
				{
					capPolicyYear.Text = dr["YEAR_AT_CURR_RESI"].ToString();
				}			
				else
				{
					capPolicyYear.Text = "N.A.";
				}

				if(dr["CUSTOMER_ATTENTION_NOTE"].ToString()!="0")
				{
					att_note="1";
					Image1.ImageUrl="~/cmsweb/images/att-ecs.gif"; 
				}
				else
				{
					Image1.ImageUrl="~/cmsweb/images/att-ecs-grey.gif";
				}
				#region Display Claims against a policy	
				ClsGeneralInformation objApplication = new ClsGeneralInformation();
				//Modified by Asfa(09-July-2008) - iTrack #4459
				string SystemId=GetSystemId();
				sbClaimLink = objApplication.GetClaimNumberAsLinkForPolicy(ds, SystemId);	
				if(sbClaimLink.Length==0)				
					trClaimRow.Attributes.Add("style","display:none");
				else
				{
					trClaimRow.Attributes.Add("style","display:inline");
					sbClaimLink.Remove(sbClaimLink.Length-2,2);
				}
				#endregion

                ClsMessages.SetCustomizedXml(Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_CULTURE);
                if (dr["APP_TERMS"].ToString() != "") 
                {
                    capPolicyTermsMonths.Text = dr["APP_TERMS"].ToString() + " " + ClsMessages.FetchGeneralMessage("1385");//objResourceMgr.GetString("DAYS");
                }

                //ds = ClsGeneralInformation.GetLOBTerms(int.Parse(hidLOB.Value));
                //System.Data.DataRow [] drAray;
                //if (hidPolicyTermMethods.Value != null && hidPolicyTermMethods.Value != "" && hidPolicyTermMethods.Value != "0")
                //{
                //    drAray = ds.Tables[0].Select("LOOKUP_VALUE_CODE='" + hidPolicyTermMethods.Value + "'");
                //    if (drAray.Length > 0)
                //    {
                //        capPolicyTermsMonths.Text = drAray[0]["LOOKUP_VALUE_DESC"].ToString();
                //    }
                //}
                else
                {
                    capPolicyTermsMonths.Text = "N.A.";
                }

				//Ravindra(09-11-2009)
				capPolicyInstallmentPlan.Text = dr["PLAN_CODE"].ToString();
				capPolicyUnderWriter.Text = dr["ASSIGNED_UNDERWRITER"].ToString();
				
//				//Installment plan
//				ds = ClsDepositDetails.FetchInstallmentPlans();
//				if (hidInstallPlanID.Value  != null && hidInstallPlanID.Value != "" && hidInstallPlanID.Value != "0")
//				{
//					drAray = ds.Tables[0].Select("INSTALL_PLAN_ID='" + hidInstallPlanID.Value + "'");
//					if (drAray.Length > 0)
//					{
//						capPolicyInstallmentPlan.Text = drAray[0]["PLAN_CODE"].ToString();
//					}
//				}
//				else
//				{
//					capPolicyInstallmentPlan.Text = "N.A.";
//				}
//
//
//				//Retreiving the underwritter
//				Cms.BusinessLayer.BlCommon.ClsAgency objAgency = new ClsAgency();
//				DataSet dsRecieve =new DataSet();
//				dsRecieve =objAgency.PopulateassignedUnderWriter(Convert.ToInt32(hidAgencyID.Value),Convert.ToInt32(hidLOB.Value));
//
//				if(dsRecieve.Tables.Count > 0) 
//				{
//					
//					if (hidUnderWriter.Value != null && hidUnderWriter.Value != "" && hidUnderWriter.Value != "0")
//					{
//
//						drAray = dsRecieve.Tables[0].Select(dsRecieve.Tables[0].Columns[0].ColumnName + "='" + hidUnderWriter.Value + "'");
//
//						if (drAray.Length > 0)
//						{
//							capPolicyUnderWriter.Text = drAray[0][1].ToString();
//						}
//					}
//					else
//					{
//						capPolicyUnderWriter.Text = "N.A.";
//					}
//				}
//				else
//				{
//					capPolicyUnderWriter.Text = "N.A.";
//				}				
			}
		}


		/// <summary>
		/// Sets the captions of the labels.
		/// </summary>
		private void SetCaptions()
		{
			
			//creating resource manager object (used for reading field and label mapping)
            System.Resources.ResourceManager objResourceMgr;

            objResourceMgr = new System.Resources.ResourceManager("Cms.cmsweb.webcontrols.PolicyTop", System.Reflection.Assembly.GetExecutingAssembly());
			
			capName.Text							=		objResourceMgr.GetString("capName");
			capNumber.Text							=		objResourceMgr.GetString("capNumber");
			capVersion.Text							=		objResourceMgr.GetString("capVersion");
			capEffectiveDate.Text					=		objResourceMgr.GetString("capEffectiveDate");
			capExpirationDate.Text					=		objResourceMgr.GetString("capExpirationDate");
			capInceptionDate.Text					=		objResourceMgr.GetString("capInceptionDate");
			capAgency.Text							=		objResourceMgr.GetString("capAgency");
			capStatus.Text							=		objResourceMgr.GetString("capStatus");
			capState.Text							=		objResourceMgr.GetString("capState");
			capLOB.Text								=		objResourceMgr.GetString("capLOB");
			capSLOB.Text							=		objResourceMgr.GetString("capSLOB");
			capTermMonths.Text						=		objResourceMgr.GetString("capTermMonths");
			capBillType.Text						=		objResourceMgr.GetString("capBillType");
			capSignature.Text						=		objResourceMgr.GetString("capSignature");
			capUnderWriter.Text						=		objResourceMgr.GetString("capUnderWriter");
			capInstallmentPlan.Text					=		objResourceMgr.GetString("capInstallmentPlan");
			capChargeOffPremium.Text				=		objResourceMgr.GetString("capChargeOffPremium");
			capReceivedPremium.Text					=		objResourceMgr.GetString("capReceivedPremium");
			capBonus.Text							=		objResourceMgr.GetString("capBonus");
			capYear.Text							=		objResourceMgr.GetString("capYear");
			capCSR.Text								=		objResourceMgr.GetString("capCSR");
			capClaimText.Text								=		objResourceMgr.GetString("lblClaimList");
            capheader.Text = objResourceMgr.GetString("capheader");
			
		}

		/// <summary>
		/// Sets the Request Variables.
		/// </summary>
		private void SetRequestVariable()
		{
			if (Request["Customer_ID"] != null )
			{
				intCustomerID = Convert.ToInt32(Request["Customer_ID"]);
			}

			if (Request["Policy_ID"] != null )
			{
				intPolicyID = Convert.ToInt32(Request["Policy_ID"]);
			}

			if (Request["PolicyVersionID"] != null )
			{
				intPolicyVersionID = Convert.ToInt32(Request["PolicyVersionID"]);
			}
		
		}


		/// <summary>
		/// Fill the Drop Down Lists and Display
		/// </summary>
		private void FillDropDownAndDisplay()
		{
			ClsGeneralInformation.GetBillType(cmbBillType,Convert.ToInt32(hidLOB.Value));		
			
			if (hidBillType.Value != null  && hidBillType.Value != "" && hidBillType.Value != "0")
			{
				cmbBillType.SelectedValue = hidBillType.Value;  
				capPolicyBillType.Text = cmbBillType.SelectedItem.Text;  
			}
			else
			{
				capPolicyBillType.Text = "N.A.";
			}
			
			System.Data.DataSet ds = ClsGeneralInformation.GetLOBTerms(int.Parse(hidLOB.Value));


			cmbPolicyTermMethods.DataSource = ClsGeneralInformation.GetLOBTerms(int.Parse(hidLOB.Value));
			cmbPolicyTermMethods.DataTextField = "LOOKUP_VALUE_DESC";
			cmbPolicyTermMethods.DataValueField = "LOOKUP_VALUE_CODE";
			cmbPolicyTermMethods.DataBind();
			cmbPolicyTermMethods.Items.Insert(0,"");
			
			if (hidPolicyTermMethods.Value != null && hidPolicyTermMethods.Value != "" && hidPolicyTermMethods.Value != "0")
			{
				System.Data.DataRow [] dr = ds.Tables[0].Select("LOOKUP_VALUE_CODE='" + hidPolicyTermMethods.Value + "'");
				if (dr.Length > 0)
				{
					capPolicyTermsMonths.Text = dr[0]["LOOKUP_VALUE_DESC"].ToString();
				}
			}
			else
			{
				capPolicyTermsMonths.Text = "N.A.";
			}
				
			 
			cmbInstallmentPlan.DataSource =ClsDepositDetails.FetchInstallmentPlans();
			cmbInstallmentPlan.DataTextField = "PLAN_CODE";
			cmbInstallmentPlan.DataValueField = "INSTALL_PLAN_ID"; 
			cmbInstallmentPlan.DataBind();
			cmbInstallmentPlan.Items.Insert(0,"");
			
			if (hidInstallPlanID.Value  != null && hidInstallPlanID.Value != "" && hidInstallPlanID.Value != "0")
			{
				cmbInstallmentPlan.SelectedValue = hidInstallPlanID.Value; 
				capPolicyInstallmentPlan.Text = cmbInstallmentPlan.SelectedItem.Text; 
			}
			else
			{
				capPolicyInstallmentPlan.Text = "N.A.";
			}
				

			Cms.BusinessLayer.BlCommon.ClsAgency objAgency = new ClsAgency();
			DataSet dsRecieve =new DataSet();
			dsRecieve=objAgency.PopulateassignedUnderWriter(Convert.ToInt32(hidAgencyID.Value),Convert.ToInt32(hidLOB.Value));

			if(dsRecieve.Tables.Count > 0) 
			{
				cmbUnderWriter.DataSource = dsRecieve.Tables[0];
				cmbUnderWriter.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
				cmbUnderWriter.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
				cmbUnderWriter.DataBind();
				cmbUnderWriter.Items.Insert(0,"");
				cmbUnderWriter.SelectedIndex =0;
			

				if (hidUnderWriter.Value != null && hidUnderWriter.Value != "" && hidUnderWriter.Value != "0")
				{
					cmbUnderWriter.SelectedValue = hidUnderWriter.Value; 
					capPolicyUnderWriter.Text = cmbUnderWriter.SelectedItem.Text;
				}
				else
				{
					capPolicyUnderWriter.Text = "N.A.";
				}
			}
			else
			{
				capPolicyUnderWriter.Text = "N.A.";
			}
		}
		
		/// <summary>
		/// Set the Policy Status in Session
		/// </summary>
		private void SetPolicyStatusInSession()
		{
			try
			{
				ClsGeneralInformation objGen = new ClsGeneralInformation();
				Cms.CmsWeb.cmsbase objBase = new cmsbase();
				objBase.SetPolicyStatus(objGen.GetStatusOfPolicy(intCustomerID,intPolicyID,intPolicyVersionID));
			}
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				throw new Exception("Policy Status can not set in the Session");
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
