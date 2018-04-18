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
using Cms.BusinessLayer.BlCommon;




namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// 
	/// </summary>
	public class LimitsOfAuthorityDetails : Cms.CmsWeb.cmsbase  
	{
		
		#region Local form variables
		//START:*********** Local form variables *************
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
        private string strRowId = "";  //, strFormSaved = ""
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capAUTHORITY_LEVEL;
		protected System.Web.UI.WebControls.TextBox txtAUTHORITY_LEVEL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAUTHORITY_LEVEL;
		protected System.Web.UI.WebControls.Label capTITLE;
		protected System.Web.UI.WebControls.TextBox txtTITLE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTITLE;
		protected System.Web.UI.WebControls.Label capPAYMENT_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtPAYMENT_LIMIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_LIMIT;
		protected System.Web.UI.WebControls.Label capRESERVE_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtRESERVE_LIMIT;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected System.Web.UI.WebControls.Label capMessages;
		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.Label capCLAIM_ON_DUMMY_POLICY;
		protected System.Web.UI.WebControls.CheckBox chkCLAIM_ON_DUMMY_POLICY;
        protected System.Web.UI.WebControls.RangeValidator rngAUTHORITY_LEVEL;
        //protected System.Web.UI.WebControls.RangeValidator rngPAYMENT_LIMIT;
        //protected System.Web.UI.WebControls.RangeValidator rngRESERVE_LIMIT;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLIMIT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESERVE_AMOUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYMENT_AMOUNT;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREOPEN_CLAIM_LIMIT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidGRATIA_CLAIM_AMOUNT;


        protected System.Web.UI.WebControls.RegularExpressionValidator revRESERVE_LIMIT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPAYMENT_LIMIT;
        protected System.Web.UI.WebControls.CustomValidator csvRESERVE_LIMIT;
        protected System.Web.UI.WebControls.CustomValidator csvPAYMENT_LIMIT;
        protected System.Web.UI.WebControls.CustomValidator csvRESERVE_LIMIT_MaxAmt;
        protected System.Web.UI.WebControls.CustomValidator csvPAYMENT_LIMIT_MaxAmt;

        protected System.Web.UI.WebControls.Label capREOPEN_CLAIM_LIMIT;
        protected System.Web.UI.WebControls.Label capGRATIA_CLAIM_AMOUNT;
        protected System.Web.UI.WebControls.TextBox txtREOPEN_CLAIM_LIMIT;
        protected System.Web.UI.WebControls.TextBox txtGRATIA_CLAIM_AMOUNT;

        protected System.Web.UI.WebControls.RegularExpressionValidator revREOPEN_CLAIM_LIMIT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revGRATIA_CLAIM_AMOUNT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREOPEN_CLAIM_LIMIT;

        protected System.Web.UI.WebControls.CustomValidator csvREOPEN_CLAIM_LIMIT;
        protected System.Web.UI.WebControls.CustomValidator csvREOPEN_CLAIM_LIMIT_MaxAmt;
        protected System.Web.UI.WebControls.CustomValidator csvGRATIA_CLAIM_AMOUNT;
        protected System.Web.UI.WebControls.CustomValidator csvGRATIA_CLAIM_AMOUNT_MaxAmt;
       
        
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;	
		//private string isActiveValue = "";
		
		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="297_0";
			
			lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Claims.LimitsOfAuthorityDetails" ,System.Reflection.Assembly.GetExecutingAssembly());

            try   //Added by aditya on 18-08-2011 for Bug # 374
            {
                if (!Page.IsPostBack)
                {

                    string strSysID = GetSystemId();
                    if (strSysID == "ALBAUAT")
                        strSysID = "ALBA";

                    if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, "LimitOfAuthorityDetails.xml"))
                        setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/LimitOfAuthorityDetails.xml");
                                        
                    if (Request.QueryString["LIMIT_ID"] != null && Request.QueryString["LIMIT_ID"].ToString() != "")
                    {
                        hidLIMIT_ID.Value = Request.QueryString["LIMIT_ID"].ToString();
                        GetOldDataXML();
                        string tempStr = ActiveDeactiveValue(int.Parse(hidLIMIT_ID.Value));
                        if (tempStr != null)
                        {
                            if (tempStr.Trim().ToUpper() == "Y")
                                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); //"Deactivate";
                            else
                                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");


                        }

                        btnActivateDeactivate.Visible = true;

                    } 
                    else
                    {
                        txtAUTHORITY_LEVEL.Text = ClsLimitsOfAuthority.GetNextAuthorityLevel();
                        btnActivateDeactivate.Visible = false;
                    }
                    txtRESERVE_LIMIT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value,2);");
                    txtPAYMENT_LIMIT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value,2);");

                    //Added by Agniswar for Singapore Implementation.
                    txtREOPEN_CLAIM_LIMIT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value,2);");
                    txtGRATIA_CLAIM_AMOUNT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value,2);");

                    btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
                    SetCaptions();
                    SetErrorMessages();
                }
                }
            catch (Exception ex)          //Added by aditya on 18-08-2011 for Bug # 374
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
			

		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML()
		{
            try  //Added by aditya on 18-08-2011 for Bug # 374
            {
                if (hidLIMIT_ID.Value != "" && hidLIMIT_ID.Value != "0")
                    hidOldData.Value = ClsLimitsOfAuthority.GetLimitsOfAuthority(int.Parse(hidLIMIT_ID.Value));
                else
                    hidOldData.Value = "";


                string strPAYMENT_LIMIT = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("PAYMENT_LIMIT", hidOldData.Value);
                string strRESERVE_LIMIT = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("RESERVE_LIMIT", hidOldData.Value);

                string strREOPEN_CLAIM_LIMIT = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("REOPEN_CLAIM_LIMIT", hidOldData.Value);
                string strGRATIA_CLAIM_LIMIT = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("GRATIA_CLAIM_AMOUNT", hidOldData.Value);


                NfiBaseCurrency.NumberDecimalDigits = 2;
                if (strPAYMENT_LIMIT != null && strPAYMENT_LIMIT != "")
                    hidPAYMENT_AMOUNT.Value = Convert.ToDouble(strPAYMENT_LIMIT.Trim()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();              
                else
                    hidPAYMENT_AMOUNT.Value = "";

                if (strRESERVE_LIMIT != null && strRESERVE_LIMIT != "")
                    hidRESERVE_AMOUNT.Value = Convert.ToDouble(strRESERVE_LIMIT.Trim()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();
                else
                    hidRESERVE_AMOUNT.Value = "";

                //Added by Agniswar for Singapore Implementation
                if (strREOPEN_CLAIM_LIMIT != null && strREOPEN_CLAIM_LIMIT != "")
                    hidREOPEN_CLAIM_LIMIT.Value = Convert.ToDouble(strREOPEN_CLAIM_LIMIT.Trim()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();
                else
                    hidREOPEN_CLAIM_LIMIT.Value = "";


                if (strGRATIA_CLAIM_LIMIT != null && strGRATIA_CLAIM_LIMIT != "")
                    hidGRATIA_CLAIM_AMOUNT.Value = Convert.ToDouble(strGRATIA_CLAIM_LIMIT.Trim()).ToString("N", NfiBaseCurrency);//layerAmount.InnerXml.Trim();
                else
                    hidGRATIA_CLAIM_AMOUNT.Value = "";

                //txtREOPEN_CLAIM_LIMIT.Text = hidREOPEN_CLAIM_LIMIT.Value;
                //txtGRATIA_CLAIM_AMOUNT.Text = hidGRATIA_CLAIM_AMOUNT.Value;

            }
            catch (Exception ex)   //Added by aditya on 18-08-2011 for Bug # 374
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
      
            
             
		}
		#endregion


		private ClsLimitsOfAuthorityInfo GetFormValue()
		{
			ClsLimitsOfAuthorityInfo objLimitsOfAuthorityInfo = new ClsLimitsOfAuthorityInfo();
			objLimitsOfAuthorityInfo.AUTHORITY_LEVEL		  = int.Parse(txtAUTHORITY_LEVEL.Text.Trim());
			objLimitsOfAuthorityInfo.TITLE					  = txtTITLE.Text.Trim();

            if (txtPAYMENT_LIMIT.Text.Trim() != "")
			  objLimitsOfAuthorityInfo.PAYMENT_LIMIT		  = Convert.ToDouble(txtPAYMENT_LIMIT.Text,NfiBaseCurrency);
            else
              objLimitsOfAuthorityInfo.PAYMENT_LIMIT	      = 0;

			if(txtRESERVE_LIMIT.Text.Trim()!="")
				objLimitsOfAuthorityInfo.RESERVE_LIMIT		  = Convert.ToDouble(txtRESERVE_LIMIT.Text,NfiBaseCurrency);                    
			else
				objLimitsOfAuthorityInfo.RESERVE_LIMIT		  =	-1;

            //Added by Agniswar for Singapore Implementation
            if (txtREOPEN_CLAIM_LIMIT.Text.Trim() != "")
                objLimitsOfAuthorityInfo.REOPEN_CLAIM_LIMIT = Convert.ToDouble(txtREOPEN_CLAIM_LIMIT.Text, NfiBaseCurrency);
            else
                objLimitsOfAuthorityInfo.REOPEN_CLAIM_LIMIT = 0;

            if (txtGRATIA_CLAIM_AMOUNT.Text.Trim() != "")
                objLimitsOfAuthorityInfo.GRATIA_CLAIM_AMOUNT = Convert.ToDouble(txtGRATIA_CLAIM_AMOUNT.Text, NfiBaseCurrency);
            else
                objLimitsOfAuthorityInfo.GRATIA_CLAIM_AMOUNT = 0;

            //Till Here
				
			if(chkCLAIM_ON_DUMMY_POLICY.Checked)
				objLimitsOfAuthorityInfo.CLAIM_ON_DUMMY_POLICY	=	true;
			else
				objLimitsOfAuthorityInfo.CLAIM_ON_DUMMY_POLICY	=	false;
			
			if(hidLIMIT_ID.Value.ToUpper()!="NEW")
				objLimitsOfAuthorityInfo.LIMIT_ID = int.Parse(hidLIMIT_ID.Value);
			strRowId = hidLIMIT_ID.Value;
			return objLimitsOfAuthorityInfo;

		}
		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{			
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
		}
		#endregion

		#region Delete Button Feature
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			ClsLimitsOfAuthority objLimitsOfAuthority	=	new ClsLimitsOfAuthority();
			
			intRetVal = objLimitsOfAuthority.Delete(int.Parse(hidLIMIT_ID.Value));
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");				
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");				
			}
			else if(intRetVal == -1)
			{
				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblDelete.Visible = true;
			
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
            try     //Added by aditya on 18-08-2011 for Bug # 374
            {
			rfvAUTHORITY_LEVEL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvTITLE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvPAYMENT_LIMIT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");						
			rngAUTHORITY_LEVEL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"216");
            //rngPAYMENT_LIMIT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"216");
            //rngRESERVE_LIMIT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"216");
            revRESERVE_LIMIT.ErrorMessage = ClsMessages.FetchGeneralMessage("2065");
            revPAYMENT_LIMIT.ErrorMessage = ClsMessages.FetchGeneralMessage("2065");

            csvRESERVE_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065");
            csvPAYMENT_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2065");
           
            revRESERVE_LIMIT.ValidationExpression = aRegExpBaseCurrencyformat;
            revPAYMENT_LIMIT.ValidationExpression = aRegExpBaseCurrencyformat;

            csvPAYMENT_LIMIT_MaxAmt.ErrorMessage = ClsMessages.FetchGeneralMessage("1972");
            csvRESERVE_LIMIT_MaxAmt.ErrorMessage = ClsMessages.FetchGeneralMessage("1972");

            }
            catch (Exception ex)          //Added by aditya on 18-08-2011 for Bug # 374
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
		}

		#endregion

		#region "Web Event Handlers"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsLimitsOfAuthority objLimitsOfAuthority = new ClsLimitsOfAuthority();				

				//Retreiving the form values into model class object
				ClsLimitsOfAuthorityInfo objLimitsOfAuthorityInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objLimitsOfAuthorityInfo.CREATED_BY = int.Parse(GetUserId());
					objLimitsOfAuthorityInfo.CREATED_DATETIME = DateTime.Now;
					objLimitsOfAuthorityInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objLimitsOfAuthority.Add(objLimitsOfAuthorityInfo);

					if(intRetVal>0)
					{
						hidLIMIT_ID.Value = objLimitsOfAuthorityInfo.LIMIT_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();	
						btnActivateDeactivate.Visible = true;
					}
					else if(intRetVal == -1) //Duplicate Authority Limit
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"4");
						hidFormSaved.Value			=		"2";
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
					ClsLimitsOfAuthorityInfo objOldLimitsOfAuthorityInfo = new ClsLimitsOfAuthorityInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldLimitsOfAuthorityInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objLimitsOfAuthorityInfo.MODIFIED_BY = int.Parse(GetUserId());
					objLimitsOfAuthorityInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    
					
					int intAuthorityLevel = objLimitsOfAuthorityInfo.AUTHORITY_LEVEL;
					//Updating the record using business layer class object
					intRetVal	= objLimitsOfAuthority.Update(objOldLimitsOfAuthorityInfo,objLimitsOfAuthorityInfo,intAuthorityLevel);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"4");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
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

		#endregion

		private void SetCaptions()
		{
			capAUTHORITY_LEVEL.Text				=		objResourceMgr.GetString("txtAUTHORITY_LEVEL");
			capTITLE.Text						=		objResourceMgr.GetString("txtTITLE");
			capPAYMENT_LIMIT.Text				=		objResourceMgr.GetString("txtPAYMENT_LIMIT");
			capRESERVE_LIMIT.Text				=		objResourceMgr.GetString("txtRESERVE_LIMIT");						
			capCLAIM_ON_DUMMY_POLICY.Text		=		objResourceMgr.GetString("chkCLAIM_ON_DUMMY_POLICY");

            //capREOPEN_CLAIM_LIMIT.Text = objResourceMgr.GetString("txtREOPEN_CLAIM_LIMIT");
            //capGRATIA_CLAIM_AMOUNT.Text = objResourceMgr.GetString("txtGRATIA_CLAIM_AMOUNT");
		}

		/*private void btnActivateDeactive_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			string isActive = "";
			ClsLimitsOfAuthority objLimitsOfAuthority	=	new ClsLimitsOfAuthority();
			
			if (btnActivateDeactive.Text.ToUpper().Trim() == "ACTIVATE")
			{
				btnActivateDeactive.Text = "Deactivate";
				isActive = "Y";
				hidFormSaved.Value = "1";
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","7");	
			}
			else
			{
				btnActivateDeactive.Text = "Activate";
				isActive = "N";
				hidFormSaved.Value = "1";
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","9");	
			}

			intRetVal = objLimitsOfAuthority.ActivateDeactivate(int.Parse(hidLIMIT_ID.Value),isActive);
			if (intRetVal > 0)
				lblMessage.Visible = true;
			else
				lblMessage.Visible = false;

		}*/
		#region
		//added by manoj rathore
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			ClsLimitsOfAuthority objLimitsOfAuthority	=	new ClsLimitsOfAuthority();
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				string customInfo ="";
				customInfo +=	";Authority Level = " + txtAUTHORITY_LEVEL.Text;

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Limits of Authority has been Deactivated Successfully.";
					objLimitsOfAuthority.TransactionInfoParams = objStuTransactionInfo;
					string strRetVal = objLimitsOfAuthority.ActivateDeactivate(hidLIMIT_ID.Value,"N",customInfo);
					if(strRetVal == "0")
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
						GetOldDataXML();
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314"); //Activate";
					}
					else if(strRetVal == "-2")
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"948");
						hidIS_ACTIVE.Value="Y";
					}
                   
					//string strScript="<script>RefreshWebGrid('" + hidAGENCY_ID.Value + "','');</script>";
					//RegisterStartupScript("REFRESHGRID",strScript);
				}
				else
				{

					
					objStuTransactionInfo.transactionDescription = "Limits of Authority has been Activated Successfully.";
					objLimitsOfAuthority.TransactionInfoParams = objStuTransactionInfo;
					objLimitsOfAuthority.ActivateDeactivate(hidLIMIT_ID.Value,"Y",customInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					GetOldDataXML();
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); //"Deactivate";
					//string strScript="<script>RefreshWebGrid('','" + hidAGENCY_ID.Value + "');</script>";
					//RegisterStartupScript("REFRESHGRID",strScript);
				}
				hidFormSaved.Value			=	"0";
				//GenerateXML(hidLIMIT_ID.Value);
				//hidReset.Value="0";
				//GetOldDataXML();
				string strScript="<script>RefreshWebGrid('1','" + hidLIMIT_ID.Value + "');</script>";
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID",strScript);
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
                
			}
			finally
			{
				lblMessage.Visible = true;
				if(objLimitsOfAuthority!= null)
					objLimitsOfAuthority.Dispose();
			}
			//if(hidAGENCY_ID.Value.ToUpper()!="NEW")
			//GenerateXML(Request.QueryString["AGENCY_ID"].ToString());
			//	GenerateXML(hidAGENCY_ID.Value);

		}
		#endregion
//end

		private string ActiveDeactiveValue(int LimitID)
		{
			string returnStr = "";

			DataSet dsTemp = ClsLimitsOfAuthority.GetValuesLimitsOfAuthority(LimitID);

			if (dsTemp.Tables[0].Rows.Count > 0)
				returnStr = dsTemp.Tables[0].Rows[0]["IS_ACTIVE"].ToString();

			return returnStr;
			
		}


	}
}
