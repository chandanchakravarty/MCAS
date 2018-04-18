/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 26/05/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the user defined GROUP DETAIL  module
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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 



namespace Cms.CmsWeb.UserDefined
{
	/// <summary>
	/// Summary description for GroupDetails.
	/// </summary>
	public class GroupDetails : Cms.CmsWeb.cmsbase  
	{
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Panel pnlMsg;
        protected System.Web.UI.WebControls.Label lblMessageDisp;
        protected System.Web.UI.WebControls.Label lblGroupName;
        protected System.Web.UI.WebControls.TextBox txtGroupName;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqtxtGroupName;
        protected System.Web.UI.WebControls.CustomValidator CVtxtGroupDesc;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.TextBox txtDeactivateVal;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.WebControls.Label lblTabID;
        protected System.Web.UI.WebControls.Label lblBackID;

        #region
        private System.Resources.ResourceManager aobjResMang;
        protected UserDefinedOne ObjGroup;
   protected string lStrScreenID;		
        protected string lStrTemplateID;
        protected string strGroupName;
        protected int gIntInsertUpdateFlag,gIntRefresh=0,gIntSavedGroupID;
        protected int gIntReturn,gIntUserID;
        protected string gStrStyle,cssFolder;		
        protected string gStrTitleMsgText,gStrSaveMsgText;
        protected string strTabID;
        #endregion
    
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

            SetCaptions();
            btnReset.Attributes.Add("onclick","javascript:ResetScreenForm();");

            base.ScreenId="128_1_1_0";

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************

            btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
            btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

            btnSave.CmsButtonClass	=	CmsButtonType.Write;
            btnSave.PermissionString		=	gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            ObjGroup = new UserDefinedOne();
            try
            {
                if(!IsPostBack)
                {
                    lStrTemplateID = Request["GROUPID"]==null?"":Request["GROUPID"];
                    lblTemplate.Text = lStrTemplateID;
                    strTabID = Request["TabID"]==null?"":Request["TabID"];
                    lStrScreenID= Request["ScreenID"]==null?"":Request["ScreenID"];
                    lblTabID.Text = strTabID;
                    lblBackID.Text=lStrScreenID;
    				gIntUserID=int.Parse(GetUserId());

                    if(lStrTemplateID.ToString()!="-1" && lblTemplate.Text.Trim()!="")					
                    {	
						btnActivateDeactivate.Visible=true;	
                        DataRow lDRGroup= ObjGroup.fnGetGroupData(int.Parse(lStrTemplateID),int.Parse(lblTabID.Text),int.Parse(lblBackID.Text));
    						
                        if(lDRGroup["GROUPNAME"].ToString() != "-1" && lDRGroup["GROUPNAME"].ToString() != "")
                        {							
                            txtGroupName.Text =lDRGroup["GROUPNAME"].ToString();
                        }
    							
                        txtDeactivateVal.Text=lDRGroup["ISACTIVE"].ToString().Trim();
                        if (txtDeactivateVal.Text.Equals("Y"))
                        {
                            btnActivateDeactivate.Text =  aobjResMang.GetString("lStrDeActivate");
                        }
                        else
                        {
                            btnActivateDeactivate.Text =  aobjResMang.GetString("lStrActivate");
                        }	
                         											
                    }
                    else
                    {
                        btnActivateDeactivate.Visible=false;	
                    }
                }
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
            }
		}

        private void SetCaptions()
        {
            aobjResMang=new System.Resources.ResourceManager("Cms.CmsWeb.UserDefined.GroupDetails" ,System.Reflection.Assembly.GetExecutingAssembly()); 
            lblGroupName.Text=aobjResMang.GetString("lblGroupName");
            reqtxtGroupName.ErrorMessage=aobjResMang.GetString("reqGroupName");	
            gStrTitleMsgText=aobjResMang.GetString("strGroupdetails");
            gStrSaveMsgText=aobjResMang.GetString("lblSaveMessage");				
            lblMessageDisp.Text = aobjResMang.GetString("strSuccessText");
			CVtxtGroupDesc.Text=aobjResMang.GetString("chkgroupdesc");
			
        }


        // This function is saves the group detail.
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            ObjGroup=new UserDefinedOne();	
            try
            {
                
                if(lblTemplate.Text!="-1" && lblTemplate.Text!="")
                {
                    gIntReturn = ObjGroup.fnUpdateGroupData(1,int.Parse(lblBackID.Text.Trim()==""?"0":lblBackID.Text),lblTabID.Text.Trim(),lblTemplate.Text.Trim(), txtGroupName.Text,gIntUserID);
                }
                else
                {
                    gIntReturn = ObjGroup.fnInsertGroupData(1, int.Parse(lblBackID.Text.Trim()==""?"0":lblBackID.Text),lblTabID.Text.Trim(), txtGroupName.Text,gIntUserID );
                    lblTemplate.Text=gIntReturn.ToString();
					btnActivateDeactivate.Text =  aobjResMang.GetString("lStrDeActivate");
                }
                if(gIntReturn>0)
                {
					gIntSavedGroupID = int.Parse(lblTemplate.Text.Trim()==""?"":lblTemplate.Text.Trim());
                    gIntInsertUpdateFlag=1;
					btnActivateDeactivate.Visible =true;
					gIntRefresh=1;
					pnlMsg.Visible=true;
					lblMessage.Text = lblMessageDisp.Text;
                }
                
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                ObjGroup.Dispose();				
            }
        }
        // This function is invoked whenever the user activates/deactivates the group.
        private void btnDeactiveGroup_Click(object sender, System.EventArgs e)
        {
			
            try
            {
                string lStrIsActive;			
                int lIntGroupId=int.Parse(lblTemplate.Text);			
                ObjGroup=new UserDefinedOne();
                lStrIsActive=txtDeactivateVal.Text.Trim();
                
				if (btnActivateDeactivate.Text ==  aobjResMang.GetString("lStrDeActivate")) //if active then deactivate
                {
                    lStrIsActive=txtDeactivateVal.Text="N";
                    btnActivateDeactivate.Text= aobjResMang.GetString("lStrActivate");
                    lblMessage.Text=aobjResMang.GetString("ScrDeactiveMsg");
                }
                else if (btnActivateDeactivate.Text ==  aobjResMang.GetString("lStrActivate"))//if inactive then activate
                {
                    lStrIsActive=txtDeactivateVal.Text="Y";
                     btnActivateDeactivate.Text= aobjResMang.GetString("lStrDeActivate");
                    lblMessage.Text=aobjResMang.GetString("ScrActiveMsg");
                }
				gIntReturn=ObjGroup.DeactivateGroup(1,lblTabID.Text,lblBackID.Text,lblTemplate.Text,lStrIsActive.Trim(),gIntUserID);										

				pnlMsg.Visible=true;
				lblMessage.Visible=true;
                gIntInsertUpdateFlag=1;				
                
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                ObjGroup.Dispose();				
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnDeactiveGroup_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion
	}
}
