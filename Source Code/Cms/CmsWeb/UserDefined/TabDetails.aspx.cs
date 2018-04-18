/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> May 26,2005
	<End Date				: - >
	<Description			: - > This file is used to add agency details,show agency details, update agency details 
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History
	
	<Modified By			: - > Manabendra Roy
	<Modified Date			: - > 27 June 2005

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
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 

namespace Cms.CmsWeb.User_Defined
{
	/// <summary>
	/// Summary description for TabDetails.
	/// </summary>
	public class TabDetails : Cms.CmsWeb.cmsbase  
	{
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Panel pnlMsg;
        protected System.Web.UI.WebControls.Label lblTabName;
        protected System.Web.UI.WebControls.TextBox txtTabName;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqtxtTabName;
        protected System.Web.UI.WebControls.RegularExpressionValidator regTabName;
        protected System.Web.UI.WebControls.CustomValidator CVtxtTabDesc;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.TextBox txtDeactivateVal;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.WebControls.Label lblScreenID;
        protected System.Web.UI.WebControls.Label lblMessageDisp;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
          protected System.Web.UI.HtmlControls.HtmlInputButton  btnReset;

        #region VARIABLE DECLARATION
        private System.Resources.ResourceManager aobjResMang;
        protected UserDefinedOne objTab;
        protected int gIntInsertUpdateFlag;
        protected int gIntReturn,gIntSavedTabID,gIntRefresh=0,gIntUserID=0;
        protected string strBackID;
        protected string strTabName;
        protected string strScreenID;
        protected string StrSuccessMsg="";
        protected string gStrStyle,cssFolder;		
        protected string gStrTitleMsgText;
        protected string gStrSaveMsgText;		
        protected string gStrSecure="";
		protected System.Web.UI.WebControls.Label lblRepetableControls;
		protected System.Web.UI.WebControls.TextBox txtRepeatControls;
		protected System.Web.UI.WebControls.RequiredFieldValidator reqRepeatControls;
		protected System.Web.UI.WebControls.RangeValidator rngRepeatValidators;
		protected System.Web.UI.WebControls.CompareValidator cvRepeatableControls;
        	protected string strTemplateID;
        #endregion
    
		private void Page_Load(object sender, System.EventArgs e)
		{
			   base.ScreenId="128_1_0";
			// Put user code to initialize the page here
                objTab=new UserDefinedOne();
            try
            {
                btnReset.Attributes.Add("onclick","javascript:ResetScreenForm();");


                base.ScreenId="623";
				gIntUserID=int.Parse(GetUserId());

                //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
   
                btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
                btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

                btnSave.CmsButtonClass	=	CmsButtonType.Write;
                btnSave.PermissionString		=	gstrSecurityXML;

                //END:*********** Setting permissions and class (Read/write/execute/delete)  *************

               

                SetCaptions();
                if(!IsPostBack)
                {
                    //strBackID= Request["TemplateID"];
					
					strTemplateID = Request["TABID"]==null?"":Request["TABID"];
					lblTemplate.Text=strTemplateID;				
					strScreenID = Request["SCREENID"]==null?"":Request["SCREENID"];
					lblScreenID.Text=strScreenID;
					
                    
                    if(strTemplateID.ToString()!="-1" && lblTemplate.Text.Trim()!="")
                    {
						btnActivateDeactivate.Visible=true;
                        DataRow lDRTab= objTab.fnGetTabData(int.Parse(strTemplateID),int.Parse(strScreenID));
    						
                        if(lDRTab["TABNAME"].ToString() != "-1" && lDRTab["TABNAME"].ToString() != "")
                        {							
                            txtTabName.Text =lDRTab["TABNAME"].ToString();
                        }

						//setting the repetable controls text
						//changed by manab on 28 june 2005
						if(lDRTab["REPEATCONTROLS"].ToString() != "-1" && lDRTab["REPEATCONTROLS"].ToString() != "")
						{							
							txtRepeatControls.Text =lDRTab["REPEATCONTROLS"].ToString();
							
						}
						//************************************
    							
                        txtDeactivateVal.Text=lDRTab["ISACTIVE"].ToString();
                        if (txtDeactivateVal.Text.Trim().Equals("Y"))
                        {
                            btnActivateDeactivate.Text =  aobjResMang.GetString("lStrDeActivate");
                        }
                        else
                        {
                            btnActivateDeactivate.Text =  aobjResMang.GetString("lStrActivate");
                        }	
                        //btnDeactiveTab.Visible=true;
                    }
                    else
                    {
                        btnActivateDeactivate.Visible=false;
                    }
                }
            }
            catch
            {
               
            }
            finally
            {
                objTab.Dispose();				
            }	
}
        /*This function saves the Tab Details into the DataBase.
             * 
             */
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            objTab=new UserDefinedOne();	
            try
            {	
                
                if(lblTemplate.Text!="-1" && lblTemplate.Text!="")	//For Insert 
                {
					//changed by manab on 28 june 2005 updating repetable controls text
                    gIntReturn = objTab.UpdateTabData(1,int.Parse(lblTemplate.Text),txtTabName.Text,txtRepeatControls.Text,lblScreenID.Text,gIntUserID);
                }
                else		// For Update
                {
					//changed by manab on 28 june 2005 adding repetable controls text
                    gIntReturn = objTab.InsertTabData(1,txtTabName.Text,txtRepeatControls.Text, lblScreenID.Text,gIntUserID);
                    lblTemplate.Text=gIntReturn.ToString();		
					btnActivateDeactivate.Text =  aobjResMang.GetString("lStrDeActivate");
					btnActivateDeactivate.Visible=true;

				}
				if(gIntReturn>0)
				{
					pnlMsg.Visible=true;
					lblMessage.Visible=true;
					lblMessage.Text = lblMessageDisp.Text;
					gIntSavedTabID =int.Parse(lblTemplate.Text);
					gIntRefresh =1;
					gIntInsertUpdateFlag=1;
                    txtDeactivateVal.Text="Y";
				}
				else
				{
				gIntRefresh =0;
				}
               
            }
            catch
            {
               
            }
            finally
            {
                objTab.Dispose();				
            }	
        }
        // This function activates/deactivates the Tab based on the criteria.
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            string lStrIsActive;
			
           // int lIntTabId=int.Parse(Request.QueryString["transferdata"]);			
            try
            {
                objTab=new UserDefinedOne();
                lStrIsActive=txtDeactivateVal.Text.Trim();
                if (lStrIsActive.Trim().Equals("Y"))
                {
                    lStrIsActive=txtDeactivateVal.Text="N";                 								
                    btnActivateDeactivate.Text= aobjResMang.GetString("lStrActivate");                   
                    lblMessage.Text=aobjResMang.GetString("ScrDeactiveMsg");
                }
                else if (lStrIsActive.Trim().Equals("N"))
                {
                    lStrIsActive=txtDeactivateVal.Text="Y";                						
                    btnActivateDeactivate.Text= aobjResMang.GetString("lStrDeActivate");                   
                    lblMessage.Text=aobjResMang.GetString("ScrActiveMsg");
                }

				
				gIntReturn=objTab.DeactivateTab(1,lblTemplate.Text,lblScreenID.Text,lStrIsActive.Trim(),gIntUserID);				
				if(gIntReturn>0)
				{
					gIntRefresh =1;
				}
				else
				{
					gIntRefresh =0;
				}
				pnlMsg.Visible=true;
				lblMessage.Visible=true;
                gIntInsertUpdateFlag=1;

            }
            catch(Exception ex)
            {
                
                throw(ex);
            }
            finally
            {
                objTab.Dispose();
            }
       
        }
        
		private void SetCaptions()
        {
            aobjResMang=new System.Resources.ResourceManager("Cms.CmsWeb.User_Defined.TabDetails" ,System.Reflection.Assembly.GetExecutingAssembly()); 
            lblTabName.Text=aobjResMang.GetString("lblTabName");

            reqtxtTabName.ErrorMessage=aobjResMang.GetString("reqTabName");
            regTabName.ValidationExpression=aRegExpAlpha;	
            regTabName.Text=aobjResMang.GetString("regTabStrTxt");					
            gStrTitleMsgText=aobjResMang.GetString("strScreendetails");					
            //StrSuccessMsg=aobjResMang.GetString("strSuccessText");
            lblMessageDisp.Text = aobjResMang.GetString("strSuccessText");
           
            CVtxtTabDesc.Text=aobjResMang.GetString("chktabdesc");


			//manab is adding repetable controls text box 
			//setting the lable and validator properties through resource file
			lblRepetableControls.Text=aobjResMang.GetString("lblRepeatDesc");
			reqRepeatControls.ErrorMessage=aobjResMang.GetString("reqRepeatMsg");
			cvRepeatableControls.ErrorMessage=aobjResMang.GetString("cvRepeatMsg");
			rngRepeatValidators.ErrorMessage=aobjResMang.GetString("rngRepeatControls");
			
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
