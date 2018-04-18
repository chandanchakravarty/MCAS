/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 30/05/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the user defined Add List Screen
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
	/// Summary description for AddList.
	/// </summary>
	public class AddList : Cms.CmsWeb.cmsbase  
	{
        protected System.Web.UI.WebControls.TextBox txtListName;
  
        protected System.Web.UI.WebControls.Label lblListName, lblQID;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqListName;
        protected string gStrStyle,cssFolder;
        protected string gstrQID;
        protected int gintInsertUpdate,gintListID;
        private UserDefinedOne objAddList;	// Creating the object of the Business Object.
        private System.Resources.ResourceManager aobjResMang;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidListID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQidID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidScreenID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidGroupID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;

		private void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                //string lStrResourceName;
             
                #region SETTING CAPTION
                aobjResMang=new System.Resources.ResourceManager("Cms.CmsWeb.UserDefined.AddList" ,System.Reflection.Assembly.GetExecutingAssembly()); 
                
                lblListName.Text = aobjResMang.GetString("lblListText");		// Populating the labels.
            
                reqListName.ErrorMessage=aobjResMang.GetString("reqListText");
                #endregion

                base.ScreenId="623";    

                hidQidID.Value = Request["qid"];
                hidScreenID.Value=Request["screenID"];
                hidTabID.Value =Request["TabID"];
                hidGroupID.Value =Request["grpID"];
                hidCalledFrom.Value =Request["calledFrom"];


                //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
                
                btnSave.CmsButtonClass	=	CmsButtonType.Write;
                btnSave.PermissionString		=	gstrSecurityXML;

                //END:*********** Setting permissions and class (Read/write/execute/delete)  *************

            }
            catch//(Exception ex)
            {
                		
            }
            finally
            {
					
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
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                objAddList=new UserDefinedOne();
                string strType;              
                strType = "U";

                hidQidID.Value = Request["qid"];
                hidScreenID.Value=Request["screenID"];
                hidTabID.Value =Request["TabID"];
                hidGroupID.Value =Request["grpID"];
                hidCalledFrom.Value =Request["calledFrom"];
                
                // Creating a new list which is saved in QUESTIONLISTMASTER
                gintListID = objAddList.fnInsertNewList(strType,txtListName.Text);		// The business object function being called to insert the data.
                if(gintListID > 0)
                {
                    gintInsertUpdate=1;
                    Response.Redirect("EditList.aspx?ListID="+gintListID+"&QID="+hidQidID.Value+"&screenID="+hidScreenID.Value+"&tabID="+hidTabID.Value+"&grpID="+hidGroupID.Value+"&calledFrom="+hidCalledFrom.Value+"&transferdata=",false);
                    //Response.Redirect("EditList.aspx?ListID="+ReturnValue);
                }
              
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
             
            }
            finally
            {
                objAddList.Dispose();				
            }	
        }	// End of Save function.

	}
}
