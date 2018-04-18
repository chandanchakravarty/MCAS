/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 15/04/2005
	<End Date				: - > 
	<Description			: - > This is used for insertion of default messages.
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: 24 May 2005- >
	<Modified By			: Gaurav Tyagi- >
	<Purpose				: Added SetXml() Function, return xml- >
    
    <Modified Date			: - >13 June 2005
	<Modified By			: - >Anurag Verma
	<Purpose				: - >Chnaged Screen ID
    
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
using System.Reflection;
using System.Resources;
using Cms.Model.Maintenance;  
using Cms.BusinessLayer.BlCommon;  


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// This class is used for modelling default message functionality
	/// </summary>
	public class AddDefaultMessage : Cms.CmsWeb.cmsbase  
	{
        #region CONTROL REFERENCE
            protected System.Web.UI.WebControls.Label lblMessage;        
            protected Cms.CmsWeb.Controls.CmsButton btnReset;
            protected Cms.CmsWeb.Controls.CmsButton btnSave;
            protected System.Web.UI.WebControls.CheckBox chkApplyClient;
            protected System.Web.UI.WebControls.CheckBox chkApplyAgency;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidMSG_ID;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidMSG_TYPE;
            protected System.Web.UI.WebControls.Label capMSG_CODE;
            protected System.Web.UI.WebControls.TextBox txtMSG_CODE;
            protected System.Web.UI.WebControls.RequiredFieldValidator rfvMSG_CODE;

            protected System.Web.UI.WebControls.Label capMSG_POSITION;
            protected System.Web.UI.WebControls.DropDownList cmbMSG_POSITION;
            protected System.Web.UI.WebControls.Label capMSG_DESC;
            protected System.Web.UI.WebControls.TextBox txtMSG_DESC;
            protected System.Web.UI.WebControls.RequiredFieldValidator rfvMSG_DESC;
            protected System.Web.UI.WebControls.Label capMSG_TEXT;
            protected System.Web.UI.WebControls.TextBox txtMSG_TEXT;
            protected System.Web.UI.WebControls.RequiredFieldValidator rfvMSG_TEXT;
            protected System.Web.UI.WebControls.Label capMSG_APPLY_TO;          
            protected System.Web.UI.HtmlControls.HtmlInputHidden hidMsgType;
            protected System.Web.UI.WebControls.CustomValidator csvMSG_DESC;
            protected System.Web.UI.WebControls.CustomValidator csvMSG_TEXT;    

            protected System.Web.UI.WebControls.Label capApplyClient;
            protected System.Web.UI.WebControls.Label capApplyAgency;
            
        #endregion
        
        #region GLOBAL VARIABLES
            protected ResourceManager objRescMgr; 
            private string strhidRowId              =   "";
		string strPageMode=""; // Holds Mode for the page
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMSG_APPLY_TO;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
          
            protected ClsDefaultMessage objDefaultMessage;
        #endregion        

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
         
            base.ScreenId="39_0"; 
                
            #region SETTING PERMISSION ACCESS TO BUTTONS
                btnSave.CmsButtonClass			        =	Cms.CmsWeb.Controls.CmsButtonType.Write;
                btnSave.PermissionString			    =	gstrSecurityXML;
            
                btnReset.CmsButtonClass	                =	Cms.CmsWeb.Controls.CmsButtonType.Write;
                btnReset.PermissionString		        =	gstrSecurityXML;	
                #endregion    

            btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "');");
    
			if(hidMSG_ID.Value != null && hidMSG_ID.Value.ToString().Length > 0)
				strPageMode = "Edit";
			else
				strPageMode = "Add";

            if(Page.IsPostBack==false)
            {
                SetLabels();
        
                SetDropDown();


                if(Request.QueryString["msg_type"]!=null)
                    hidMSG_TYPE.Value = Request.QueryString["msg_type"];

				//Setting xml for the page to be displayed in page controls
				if(Request.QueryString["MSG_ID"]!=null && Request.QueryString["MSG_ID"].ToString().Length>0)
				{
					SetXml(Request.QueryString["MSG_ID"],hidMSG_TYPE.Value);
					strPageMode = "Edit";
				}
				else if(hidMSG_ID.Value != null && hidMSG_ID.Value.ToString().Length > 0)
				{
					SetXml(hidMSG_ID.Value.ToString(),hidMSG_TYPE.Value.ToString());
					strPageMode = "Edit";
				}
				else
					strPageMode = "Add";
            }


		}

        /// <summary>
        /// This function will return Xml for PopulateXML() function
        /// </summary>
        /// <param name="strMessageId"></param>
        /// <param name="strMessageType"></param>
		private void SetXml(string strMessageId,string strMessageType)
		{
			hidOldData.Value = ClsDefaultMessage.GetXmlForPageControls(strMessageId,strMessageType);
		}
		/// <summary>
        /// Setting page labels and caption
        /// </summary>
        private void SetLabels() 
        {
            objRescMgr              = new ResourceManager("Cms.CmsWeb.Maintenance.AddDefaultMessage",Assembly.GetExecutingAssembly());   
            capMSG_CODE.Text        = objRescMgr.GetString("txtMSG_CODE"); 
            capMSG_DESC.Text        = objRescMgr.GetString("txtMSG_DESC"); 
            capMSG_TEXT.Text        = objRescMgr.GetString("txtMSG_TEXT"); 
            capMSG_POSITION.Text    = objRescMgr.GetString("cmbMSG_POSITION"); 
            capMSG_APPLY_TO.Text    = objRescMgr.GetString("txtMSG_APPLY_TO"); 
            capApplyAgency.Text     = objRescMgr.GetString("chkApplyAgency");   
            capApplyClient.Text     = objRescMgr.GetString("chkApplyClient");   


            rfvMSG_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"88");
            rfvMSG_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"89");
            rfvMSG_TEXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"90");

            csvMSG_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"92");
            csvMSG_TEXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"91");

        }

        /// <summary>
        /// Filling dropdown
        /// </summary>
        private void SetDropDown() 
        {
            ListItem liCenter                   =   new ListItem("Center","C"); 
            ListItem liLeft                     =   new ListItem("Left Justify","L"); 
            ListItem liRight                    =   new ListItem("Right Justify","R"); 

            cmbMSG_POSITION.Items.Add(liCenter);
            cmbMSG_POSITION.Items.Add(liLeft);
            cmbMSG_POSITION.Items.Add(liRight);  
        }


        /// <summary>
        /// Fetch form's value and stores into variables.
        /// </summary>
        private ClsDefaultMessageInfo GetFormValue()
        {
            //Code for retreiving the forms valus will go here		
            ClsDefaultMessageInfo objModelInfo;
            objModelInfo                     = new ClsDefaultMessageInfo()  ;

            strhidRowId                      =   hidMSG_ID.Value;
            objModelInfo.MSG_TYPE            =   hidMSG_TYPE.Value;  
            if(hidMSG_ID.Value!="NEW")
                objModelInfo.MSG_ID          =   int.Parse(hidMSG_ID.Value);
            objModelInfo.MSG_CODE            =   txtMSG_CODE.Text.Trim(); 
            objModelInfo.MSG_DESC            =   txtMSG_DESC.Text.Trim();
            objModelInfo.MSG_TEXT            =   txtMSG_TEXT.Text.Trim();
            objModelInfo.MSG_POSITION        =   cmbMSG_POSITION.SelectedItem.Value==""?"":cmbMSG_POSITION.SelectedItem.Value;
            objModelInfo.CREATED_BY          =   int.Parse(GetUserId());
            objModelInfo.MODIFIED_BY         =   int.Parse(GetUserId());

            if(chkApplyClient.Checked)
                objModelInfo.MSG_APPLY_TO    =   "Y";
            else
                objModelInfo.MSG_APPLY_TO    =   "N";

            if(objModelInfo.MSG_TYPE=="S")
            if(chkApplyAgency.Checked)
                objModelInfo.MSG_APPLY_TO    +=   ",Y";
            else
                objModelInfo.MSG_APPLY_TO    +=   ",N";

            return objModelInfo;
            
        }


        /// <summary>
        /// Saves the posted data from form
        /// </summary>
        private void SaveFormValue()
        {
            try
            {
                 objDefaultMessage  = new Cms.BusinessLayer.BlCommon.ClsDefaultMessage(); 
                 objRescMgr         = new ResourceManager("Cms.Cmsweb.Maintenance.AddDefaultMessage",Assembly.GetExecutingAssembly());  
                 strhidRowId        = hidMSG_ID.Value;      

                 ClsDefaultMessageInfo objDefModelInfo= GetFormValue();

 
                if(strhidRowId == "NEW")	//Checking the form contains the new record or existing record
                {
                    //Adding a new record to database using the agency object
                    int intReturnVal            =   objDefaultMessage.Add(objDefModelInfo);
                  
                    if (intReturnVal > 0)
                    {
                        lblMessage.Text	        =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");  
                        hidFormSaved.Value  	=   "1";
						hidMSG_ID.Value  		=   intReturnVal.ToString();    
						SetXml(hidMSG_ID.Value.ToString(),hidMSG_TYPE.Value);
                                         
                    }
                    else if (intReturnVal == -1)
                    {
                        //Dulicate code found
                        lblMessage.Text     	=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value  	=    "2";
                    }
                    else
                    {
                        //some error occurred 
                        lblMessage.Text	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                        // btnActivateDeactivate.Enabled = false;
                        hidFormSaved.Value	= "2";
                    }
                
                }
                else
                {
                    int returnResult                            = -1; //to store result of the update operation
                    
                    ClsDefaultMessageInfo objDefOldModelInfo    = new ClsDefaultMessageInfo();    
                    base.PopulateModelObject(objDefOldModelInfo,hidOldData.Value);  
                    returnResult                                = objDefaultMessage.Update(objDefOldModelInfo,objDefModelInfo);
                    if(returnResult>0)
                    {
                        //information saved successfully
                        lblMessage.Visible                      = true; 
                        lblMessage.Text	                        = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31"); 
                        hidFormSaved.Value	                    = "1";
						SetXml(hidMSG_ID.Value.ToString(),hidMSG_TYPE.Value);
                    }
                    else if (returnResult == -1)
                    {
                        //Some error occured
                        lblMessage.Text	                        = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("19");                        
                        hidFormSaved.Value	                    = "2";
                    }				    
                }	
		         lblMessage.Visible = true;
            }
            catch(Exception ex)
            {
                //Error occured and message is shown with the error message
                lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("21") + "\n" + ex.Message;
                lblMessage.Visible		=	true;
                hidFormSaved.Value	    =   "2";
                //Publishing the exception using the static method of Exception manager class
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
            }
            finally
            {
                if(objDefaultMessage!= null) 
                    objDefaultMessage.Dispose();               
            }
        }
		
        /// <summary>
        /// Event handler to for save button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //calling save method
            SaveFormValue();		
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}


}
