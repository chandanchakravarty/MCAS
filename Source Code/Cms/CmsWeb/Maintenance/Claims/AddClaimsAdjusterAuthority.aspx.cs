/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 20,2006
	<End Date				: - >
	<Description			: - > Page is used to assign limits to authority
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History


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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;




namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// 
	/// </summary>
	public class AddClaimsAdjusterAuthority : Cms.CmsWeb.cmsbase  
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		private const int EXPERT_SERVICE_PROVIDER_ID=9;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
        private string strRowId;//, strFormSaved
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;		
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;		
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLIMIT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADJUSTER_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidADJUSTER_AUTHORITY_ID;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capLIMIT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLIMIT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLIMIT_ID;
		protected System.Web.UI.WebControls.Label capPAYMENT_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtPAYMENT_LIMIT;
		protected System.Web.UI.WebControls.Label capRESERVE_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtRESERVE_LIMIT;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.Label lblADJUSTER_NAME;
		protected System.Web.UI.WebControls.Label capADJUSTER_NAME;
		protected System.Web.UI.WebControls.Label capNOTIFY_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtNOTIFY_AMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNOTIFY_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNOTIFY_AMOUNT;
        protected System.Web.UI.WebControls.Label capMessages;

        protected System.Web.UI.WebControls.CustomValidator csvNOTIFY_AMOUNT;
        protected System.Web.UI.WebControls.CustomValidator csvNOTIFY_AMOUNT_MaxAmt;

        // Added by santosh kumar gautam on 03 march 2011 Itrack:440,441
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEffectiveDate;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNotifyAmount;
        
        
        //private int	intLoggedInUserID;
		
		
		
		

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{	
					
			if (Request.QueryString["ADJUSTER_NAME"] != null)
				lblADJUSTER_NAME.Text = Request.QueryString["ADJUSTER_NAME"].ToString();

			

			base.ScreenId="298_1_0";

			if(Request.QueryString["ADJUSTER_AUTHORITY_ID"]!=null && Request.QueryString["ADJUSTER_AUTHORITY_ID"].ToString()!="")
			{
				hidADJUSTER_AUTHORITY_ID.Value = Request.QueryString["ADJUSTER_AUTHORITY_ID"].ToString();					
				//Changing the security xml to view mode only
				//gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";  //Commented by Sibin on 22 Oct 08 for Assign Rights Underwriter manager Security
			}			
			
			lblMessage.Visible = false;
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write; //Permission made Write instead of Read by Sibin on 22 Oct 08
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.AddClaimsAdjusterAuthority" ,System.Reflection.Assembly.GetExecutingAssembly());
			
            try
            {
			if(!Page.IsPostBack)
			{
				if (Request.QueryString["ADJUSTER_NAME"] != null)
					lblADJUSTER_NAME.Text = Request.QueryString["ADJUSTER_NAME"].ToString();

				if(Request.QueryString["ADJUSTER_ID"]!=null && Request.QueryString["ADJUSTER_ID"].ToString()!="")
				{
					hidADJUSTER_ID.Value = Request.QueryString["ADJUSTER_ID"].ToString();									
				}
				hlkEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLM_ADJUSTER_AUTHORITY.txtEFFECTIVE_DATE,document.CLM_ADJUSTER_AUTHORITY.txtEFFECTIVE_DATE)"); //Javascript Implementation for Effective Date
				cmbLIMIT_ID.Attributes.Add("onChange","javascript: return GetLimitData(this);");


                txtNOTIFY_AMOUNT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value,2);");
				GetOldDataXML();	
				LoadDropDowns();
				LoadLOB();
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML()
		{
            if (hidADJUSTER_AUTHORITY_ID.Value != "" && hidADJUSTER_AUTHORITY_ID.Value != "0" && hidADJUSTER_AUTHORITY_ID.Value.ToUpper() != "NEW")
            {
                hidOldData.Value = ClsClaimsAdjusterAuthority.GetClaimsAdjusters(int.Parse(hidADJUSTER_AUTHORITY_ID.Value), int.Parse(hidADJUSTER_ID.Value));


                // Added by santosh kumar gautam on 03 march 2011 Itrack:440,441
                string EffectiveDate = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("EFFECTIVE_DATE", hidOldData.Value.Trim());
                string NotifyAmount = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("NOTIFY_AMOUNT", hidOldData.Value.Trim());
                string IsActive = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.Trim());
                if (EffectiveDate != "")
                    hidEffectiveDate.Value = ConvertDBDateToCulture(EffectiveDate);
                else
                    hidEffectiveDate.Value = "";

                if(IsActive=="Y")
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); //Deactivate";
                else
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314"); //Activate";

                NfiBaseCurrency.NumberDecimalDigits = 2;
                if (NotifyAmount != "")
                {
                    hidNotifyAmount.Value = Convert.ToDouble(NotifyAmount.Trim()).ToString("N", NfiBaseCurrency); 
                }


            }
            else
            {
                hidOldData.Value = "";
                txtEFFECTIVE_DATE.Text = System.DateTime.Now.Date.ToShortDateString().Trim();
            }
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
            try
            {
			rfvLOB_ID.ErrorMessage					=    Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			rfvLIMIT_ID.ErrorMessage				=    Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			revEFFECTIVE_DATE.ValidationExpression	=    aRegExpDate;						
			revEFFECTIVE_DATE.ErrorMessage			=    Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			rfvEFFECTIVE_DATE.ErrorMessage			=    Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
          
            //revNOTIFY_AMOUNT.ErrorMessage = ClsMessages.FetchGeneralMessage("224_25_6");			
			rfvNOTIFY_AMOUNT.ErrorMessage			=    Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
            revNOTIFY_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;
            csvNOTIFY_AMOUNT.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "6");
            csvNOTIFY_AMOUNT_MaxAmt.ErrorMessage = ClsMessages.FetchGeneralMessage("1972");
          //  csvNOTIFY_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
//			revEXPERT_SERVICE_ZIP.ValidationExpression				=		  aRegExpZip;
//			revEXPERT_SERVICE_ZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
//			revEXPERT_SERVICE_CONTACT_EMAIL.ValidationExpression	=		  aRegExpEmail;
//			revEXPERT_SERVICE_CONTACT_ErevNOTIFY_AMOUNTMAIL.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
//			rfvEXPERT_SERVICE_NAME.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("34");			
//			rfvEXPERT_SERVICE_ZIP.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");			
//			revEXPERT_SERVICE_CONTACT_PHONE.ValidationExpression	=		  aRegExpPhone;
//			revEXPERT_SERVICE_CONTACT_PHONE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");						
//			rfvEXPERT_SERVICE_ADDRESS1.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
//			rfvEXPERT_SERVICE_STATE.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
//			rfvEXPERT_SERVICE_VENDOR_CODE.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");
//			rfvEXPERT_SERVICE_TYPE.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
		}

		#endregion

		#region GetFormValue
		private ClsClaimsAdjusterAuthorityInfo GetFormValue()
		{
			ClsClaimsAdjusterAuthorityInfo objClaimsAdjusterAuthorityInfo = new ClsClaimsAdjusterAuthorityInfo();			
			if(cmbLOB_ID!=null && cmbLOB_ID.SelectedItem!=null && cmbLOB_ID.SelectedItem.Value!="")
				objClaimsAdjusterAuthorityInfo.LOB_ID = int.Parse(cmbLOB_ID.SelectedItem.Value);
			//if(cmbLIMIT_ID.SelectedItem!=null && cmbLIMIT_ID.SelectedItem.Value!="")
			//{
				//Get the limit id from hidden variable as combo's interval value contains concatenated string
				objClaimsAdjusterAuthorityInfo.LIMIT_ID = int.Parse(hidLIMIT_ID.Value);
			//}
			if(txtEFFECTIVE_DATE.Text.Trim()!="")
				objClaimsAdjusterAuthorityInfo.EFFECTIVE_DATE = Convert.ToDateTime(txtEFFECTIVE_DATE.Text.Trim());


			objClaimsAdjusterAuthorityInfo.ADJUSTER_ID = int.Parse(hidADJUSTER_ID.Value);

            if (txtNOTIFY_AMOUNT.Text.Trim() != "")
                objClaimsAdjusterAuthorityInfo.NOTIFY_AMOUNT = Convert.ToDouble(txtNOTIFY_AMOUNT.Text.Trim(), NfiBaseCurrency);
            else
                objClaimsAdjusterAuthorityInfo.NOTIFY_AMOUNT = 0;


           

			
			if(hidADJUSTER_AUTHORITY_ID.Value.ToUpper()=="0" || hidADJUSTER_AUTHORITY_ID.Value.ToUpper()=="NEW")
				strRowId = "NEW";
			else
			{
				strRowId = hidADJUSTER_AUTHORITY_ID.Value;
				objClaimsAdjusterAuthorityInfo.ADJUSTER_AUTHORITY_ID = int.Parse(hidADJUSTER_AUTHORITY_ID.Value);
			}
			return objClaimsAdjusterAuthorityInfo;
				
		}
		#endregion

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsClaimsAdjusterAuthority objClaimsAdjusterAuthority = new ClsClaimsAdjusterAuthority();				

				//Retreiving the form values into model class object
				ClsClaimsAdjusterAuthorityInfo objClaimsAdjusterAuthorityInfo = GetFormValue();
				
				string strAdjusterName="";
				if(Request.QueryString["ADJUSTER_NAME"] != null)
					strAdjusterName = Request.QueryString["ADJUSTER_NAME"].ToString();

				if(Request.QueryString["ADJUSTER_TYPE"] != null)
					strAdjusterName +="^" + Request.QueryString["ADJUSTER_TYPE"].ToString();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objClaimsAdjusterAuthorityInfo.CREATED_BY = int.Parse(GetUserId());
					objClaimsAdjusterAuthorityInfo.CREATED_DATETIME = DateTime.Now;
					objClaimsAdjusterAuthorityInfo.IS_ACTIVE="Y"; 
					//Calling the add method of business layer class
					intRetVal = objClaimsAdjusterAuthority.Add(objClaimsAdjusterAuthorityInfo,strAdjusterName);

					if(intRetVal>0)
					{
						hidADJUSTER_AUTHORITY_ID.Value = objClaimsAdjusterAuthorityInfo.ADJUSTER_AUTHORITY_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();
						//LoadLOB();
					}	
					else if(intRetVal==-1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"3");
						hidFormSaved.Value		=	"2";
					}	
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsClaimsAdjusterAuthorityInfo objOldClaimsAdjusterAuthorityInfo = new ClsClaimsAdjusterAuthorityInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldClaimsAdjusterAuthorityInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objClaimsAdjusterAuthorityInfo.MODIFIED_BY = int.Parse(GetUserId());
					objClaimsAdjusterAuthorityInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objClaimsAdjusterAuthority.Update(objOldClaimsAdjusterAuthorityInfo,objClaimsAdjusterAuthorityInfo,strAdjusterName);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
						//LoadLOB();
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}					
				}
				lblMessage.Visible = true;
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
			finally
			{
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}
		private void SetCaptions()
		{
			capLOB_ID.Text					=		objResourceMgr.GetString("cmbLOB_ID");
			capLIMIT_ID.Text				=		objResourceMgr.GetString("cmbLIMIT_ID");
			capPAYMENT_LIMIT.Text			=		objResourceMgr.GetString("txtPAYMENT_LIMIT");
			capRESERVE_LIMIT.Text			=		objResourceMgr.GetString("txtRESERVE_LIMIT");
			capEFFECTIVE_DATE.Text			=		objResourceMgr.GetString("txtEFFECTIVE_DATE");	
			capADJUSTER_NAME.Text			=		objResourceMgr.GetString("lblADJUSTER_NAME");	
			capNOTIFY_AMOUNT.Text						=objResourceMgr.GetString("txtNOTIFY_AMOUNT");
		}
	

		

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			DataTable dtAuthorityLimits = ClsClaimsAdjusterAuthority.FetchAuthorityLimits();
			if(dtAuthorityLimits!=null && dtAuthorityLimits.Rows.Count>0)
			{
				cmbLIMIT_ID.DataSource = dtAuthorityLimits;
				cmbLIMIT_ID.DataTextField = "AUTHORITY_LEVEL";
				cmbLIMIT_ID.DataValueField = "LIMIT_ID";
				cmbLIMIT_ID.DataBind();
				cmbLIMIT_ID.Items.Insert(0,"");
				cmbLIMIT_ID.SelectedIndex=0;
			}
		}

		private void LoadLOB()
		{
			DataSet dsRecieve = new DataSet();
			//Disable the LOB drop-down when the data has been entered (edit mode)
			//Now the LOB drop-down will not be disabled and multiple entries are allowed
//			if(hidOldData.Value=="" || hidOldData.Value=="0")
//				dsRecieve =ClsClaimsAdjusterAuthority.GetRemainingLOB(int.Parse(hidADJUSTER_ID.Value));														
//			else
//			{					
				Cms.BusinessLayer.BlCommon.ClsStates objState = new Cms.BusinessLayer.BlCommon.ClsStates();
				dsRecieve = objState.PoplateLob();	
//			}	

			cmbLOB_ID.DataSource =	dsRecieve.Tables[0];
			cmbLOB_ID.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
			cmbLOB_ID.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
			cmbLOB_ID.DataBind();
			
		}
		#endregion

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				ClsClaimsAdjusterAuthority objBL = new ClsClaimsAdjusterAuthority();				
				string strRetVal = "";
				string customerInfo="";
				if(Request.QueryString["ADJUSTER_NAME"] != null)
					customerInfo = "Adjuster Name=" + Request.QueryString["ADJUSTER_NAME"].ToString();
				if(Request.QueryString["ADJUSTER_NAME"] != null)
					customerInfo +="<br>" + " ;Adjuster Type=" + Request.QueryString["ADJUSTER_TYPE"].ToString();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Claims Adjuster Authority has been Deactivated Successfully.";
					objBL.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = objBL.ActivateDeactivate(hidADJUSTER_AUTHORITY_ID.Value,"N",customerInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Claims Adjuster Authority has been Activated Successfully.";
					objBL.TransactionInfoParams = objStuTransactionInfo;
					objBL.ActivateDeactivate(hidADJUSTER_AUTHORITY_ID.Value,"Y",customerInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				
				if (strRetVal == "-1")
				{
					/*Profit Center is assigned*/
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"514");
				}

				lblMessage.Visible = true;
				hidFormSaved.Value			=	"0";
				ClientScript.RegisterStartupScript(this.GetType(),"RefreshGRid","<script>RefreshWebGrid(1,1,true)</script>");
				GetOldDataXML();
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}

//		private void cmbLOB_ID_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			//Here we will fetch authority limits defined for this LOB
//			cmbLIMIT_ID.Items.Clear();
//			if(cmbLOB_ID.SelectedItem!=null && cmbLOB_ID.SelectedItem.Value!="")
//			{
//				DataTable dtAuthorityLimits = ClsClaimsAdjusterAuthority.FetchAuthorityLimits();
//				if(dtAuthorityLimits!=null && dtAuthorityLimits.Rows.Count>0)
//				{
//					cmbLIMIT_ID.DataSource = dtAuthorityLimits;
//					cmbLIMIT_ID.DataTextField = "AUTHORITY_LEVEL";
//					cmbLIMIT_ID.DataValueField = "LIMIT_ID";
//					cmbLIMIT_ID.DataBind();
//				}
//			}
//		}

//		private void cmbLIMIT_ID_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			//Here we will fetch payment limit and reserve limit correponding to lob and authority limit
//			txtPAYMENT_LIMIT.Text = "";
//			txtRESERVE_LIMIT.Text = "";
//			if(cmbLIMIT_ID.SelectedItem!=null && cmbLIMIT_ID.SelectedItem.Value!="")
//			{
//				DataTable dtAuthorityAmounts = ClsClaimsAdjusterAuthority.FetchAuthorityAmounts(int.Parse(cmbLIMIT_ID.SelectedItem.Value));
//				if(dtAuthorityAmounts!=null && dtAuthorityAmounts.Rows.Count>0)
//				{					
//					if(dtAuthorityAmounts.Rows[0]["PAYMENT_LIMIT"]!=null && dtAuthorityAmounts.Rows[0]["PAYMENT_LIMIT"].ToString()!="")
//						txtPAYMENT_LIMIT.Text = String.Format("{0:,#,###}",Convert.ToInt64(dtAuthorityAmounts.Rows[0]["PAYMENT_LIMIT"]));
//					if(dtAuthorityAmounts.Rows[0]["RESERVE_LIMIT"]!=null && dtAuthorityAmounts.Rows[0]["RESERVE_LIMIT"].ToString()!="")
//						txtRESERVE_LIMIT.Text = String.Format("{0:,#,###}",Convert.ToInt64(dtAuthorityAmounts.Rows[0]["RESERVE_LIMIT"]));							
//					hidFormSaved.Value="2";
//				}
//			}
//		}
	}
}
