/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 22/04/2005	
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
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
	/// Summary description for AddDefaultValue.
	/// </summary>
	public class AddDefaultValue : Cms.CmsWeb.cmsbase  
	{
        #region CONTROL REFERENCE
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capDEFV_ENTITY_NAME;
        protected System.Web.UI.WebControls.TextBox txtDEFV_ENTITY_NAME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEFV_ENTITY_NAME;
        protected System.Web.UI.WebControls.Label capDEFV_VALUE;
        protected System.Web.UI.WebControls.TextBox txtDEFV_VALUE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEFV_VALUE;
        protected System.Web.UI.WebControls.CustomValidator csvDEFV_VALUE;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;        
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEFV_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        #endregion

        #region GLOBAL VARIABLES
        protected ResourceManager objRescMgr; 
        private string strhidRowId              =   "";          
        protected ClsDefaultValue objDefaultValue;
		string strPageMode=""; // Holds Mode for the page
        #endregion        
    
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            base.ScreenId="56_0"; 
                
            #region SETTING PERMISSION ACCESS TO BUTTONS
            btnSave.CmsButtonClass			        =	Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString			    =	gstrSecurityXML;
            
            btnReset.CmsButtonClass	                =	Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString		        =	gstrSecurityXML;	
            #endregion    

            btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "');");
			if(hidDEFV_ID.Value != null && hidDEFV_ID.Value.ToString().Length > 0)
				strPageMode = "Edit";
			else
				strPageMode = "Add";
            if(Page.IsPostBack==false)
            {
                SetLabels();
				//Setting xml for the page to be displayed in page controls
				if(Request.QueryString["DEFV_ID"]!=null && Request.QueryString["DEFV_ID"].ToString().Length>0)
				{
					SetXml(Request.QueryString["DEFV_ID"]);
					strPageMode = "Edit";
				}
				else if(hidDEFV_ID.Value != null && hidDEFV_ID.Value.ToString().Length > 0)
				{
					SetXml(hidDEFV_ID.Value.ToString());
					strPageMode = "Edit";
				}
				else
					strPageMode = "Add";
            }
		}

		private void SetXml(string strDefaultValueId)
		{
			hidOldData.Value = ClsDefaultValue.GetXmlForPageControls(strDefaultValueId);
		}

        /// <summary>
        /// Setting page labels and caption
        /// </summary>
        private void SetLabels() 
        {
            objRescMgr              = new ResourceManager("Cms.CmsWeb.Maintenance.AddDefaultValue",Assembly.GetExecutingAssembly());   
            capDEFV_ENTITY_NAME.Text        = objRescMgr.GetString("txtDEFV_ENTITY_NAME"); 
            capDEFV_VALUE.Text        = objRescMgr.GetString("txtDEFV_VALUE"); 
            
            rfvDEFV_ENTITY_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"336");
            rfvDEFV_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"337");
			csvDEFV_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"91");

        }

        /// <summary>
        /// Fetch form's value and stores into variables.
        /// </summary>
        private ClsDefaultValueInfo GetFormValue(ClsDefaultValueInfo objModelInfo)
        {
            //Code for retreiving the forms valus will go here		
            strhidRowId                      =   hidDEFV_ID.Value;            
            if(hidDEFV_ID.Value!="NEW")
                objModelInfo.DEFV_ID         =   int.Parse(hidDEFV_ID.Value);

            objModelInfo.DEFV_ENTITY_NAME    =   txtDEFV_ENTITY_NAME.Text.Trim(); 
            objModelInfo.DEFV_VALUE          =   txtDEFV_VALUE.Text.Trim();            
            objModelInfo.CREATED_BY          =   int.Parse(GetUserId());
            objModelInfo.MODIFIED_BY         =   int.Parse(GetUserId());
            objModelInfo.IS_ACTIVE           =   "Y";
            return objModelInfo;
        }

        /// <summary>
        /// Saves the posted data from form
        /// </summary>
        private void SaveFormValue()
        {
            try
            {
                objDefaultValue    = new Cms.BusinessLayer.BlCommon.ClsDefaultValue();
                objRescMgr         = new ResourceManager("Cms.Cmsweb.Maintenance.AddDefaultValue",Assembly.GetExecutingAssembly());  
                strhidRowId        = hidDEFV_ID.Value;      
 
                if(strhidRowId == "NEW")	//Checking the form contains the new record or existing record
                {
                    ClsDefaultValueInfo objDefModelInfo=new ClsDefaultValueInfo();
                    //Retreiving the form values
                    objDefModelInfo=GetFormValue(objDefModelInfo);				

                    //Adding a new record to database using the agency object
                    int intReturnVal            =   objDefaultValue.Add(objDefModelInfo);
                  
                    if (intReturnVal > 0)
                    {
                        lblMessage.Text	        =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");  
                        hidFormSaved.Value  	=   "1";
                        hidDEFV_ID.Value  		=   intReturnVal.ToString(); 
						SetXml(hidDEFV_ID.Value.ToString());
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
                    ClsDefaultValueInfo objDefNewModelInfo    = new ClsDefaultValueInfo();
                    //Retreiving the form values
                    objDefNewModelInfo                          = GetFormValue(objDefNewModelInfo);	
                    int returnResult                            = -1; //to store result of the update operation
               
                    ClsDefaultValueInfo objDefOldModelInfo    = new ClsDefaultValueInfo();    
                    base.PopulateModelObject(objDefOldModelInfo,hidOldData.Value);  
                    returnResult                                = objDefaultValue.Update(objDefOldModelInfo,objDefNewModelInfo);
                    if(returnResult>0)
                    {
                        //information saved successfully
                        lblMessage.Visible                      = true; 
                        lblMessage.Text	                        = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31"); 
                        hidFormSaved.Value	                    = "1";
						SetXml(hidDEFV_ID.Value.ToString());
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
                if(objDefaultValue!= null) 
                    objDefaultValue.Dispose();               
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
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }
		#endregion
	}
}
