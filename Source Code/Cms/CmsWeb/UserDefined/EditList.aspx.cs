/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 31/05/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the user defined Edit List Screen
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
	/// Summary description for EditList.
	/// </summary>
	public class EditList : Cms.CmsWeb.cmsbase  
	{
        #region CONTROL REFERENCE
            protected System.Web.UI.WebControls.Label lblListtxt;
            protected System.Web.UI.WebControls.Label lblListName;
            protected System.Web.UI.WebControls.Label lblCodeToDisp;
            protected System.Web.UI.WebControls.DropDownList selDescp;
            protected System.Web.UI.WebControls.Panel pnlDdlDesc;
            protected System.Web.UI.WebControls.Label lblDesctxt;
            protected System.Web.UI.WebControls.TextBox txtDescp;
            protected System.Web.UI.WebControls.RequiredFieldValidator reqListName;
            protected Cms.CmsWeb.Controls.CmsButton btnAddNew;
            protected Cms.CmsWeb.Controls.CmsButton btnSave;
            protected Cms.CmsWeb.Controls.CmsButton btnClose;
            protected System.Web.UI.WebControls.Label lblListID;
            protected System.Web.UI.WebControls.Label lblInsertMode;
            protected System.Web.UI.WebControls.Label lblQID;
        #endregion
        

        #region LOCAL DECLARATION
            string strListID;
            protected string gstrQID ="";
            protected int lintInsertMode=0;
            protected string gStrStyle,cssFolder;
            private UserDefinedOne objEditList;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidListID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQidID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidScreenID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidGroupID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;	// Creating the object of the Business Class
            private System.Resources.ResourceManager aobjResMang;

        #endregion

        private void SetCaptions()
        {
          aobjResMang=new System.Resources.ResourceManager("Cms.CmsWeb.UserDefined.EditList" ,System.Reflection.Assembly.GetExecutingAssembly()); 
          lblListtxt.Text = aobjResMang.GetString("lblListText");		// Populating the labels.
          lblCodeToDisp.Text= aobjResMang.GetString("lblCodeToDisp");          
          reqListName.ErrorMessage=aobjResMang.GetString("reqListText");				
        }
    
		private void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            base.ScreenId="623";
           
            btnClose.Attributes.Add("onclick","Winclose()"); 
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnClose.CmsButtonClass	=	CmsButtonType.Write;
            btnClose.PermissionString		=	gstrSecurityXML;

            btnAddNew.CmsButtonClass	=	CmsButtonType.Write;
            btnAddNew.PermissionString		=	gstrSecurityXML;

            btnSave.CmsButtonClass	=	CmsButtonType.Write;
            btnSave.PermissionString		=	gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            SetCaptions();
            try
            {
                objEditList = new UserDefinedOne();
             			
                if(!IsPostBack)
                {					
                    gstrQID = Request["QID"];
                    lblQID.Text = gstrQID;           
                    strListID = Request["ListID"];				
                    lblListID.Text=strListID;
                    hidListID.Value=strListID ;
                    hidQidID.Value=gstrQID;
                    hidScreenID.Value=Request["screenID"];
                    hidTabID.Value =Request["TabID"];
                    hidGroupID.Value =Request["grpID"];
                    hidCalledFrom.Value =Request["calledFrom"];
					lblInsertMode.Text="-1";

                    if(strListID!="")
                    {
						lblListName.Text = objEditList.fnGetListData(strListID);																
                        selDescp.DataSource = new DataView(objEditList.fnGetList(strListID).Tables[0]);
                        selDescp.DataTextField="OPTIONNAME";
                        selDescp.DataValueField="OPTIONID";			
                        selDescp.DataBind();
                        selDescp.Items.Insert(0, new ListItem("","0"));
                        selDescp.SelectedIndex=0;									
                    }
					
                }
                else
                {
				    gstrQID = lblQID.Text;
                }
				//btnSave.Attributes.Add("onclick","Winclose();");
            }
            catch//(Exception ex)
            {
                			
            }
            finally
            {
                objEditList.Dispose();				
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
            this.selDescp.SelectedIndexChanged += new System.EventHandler(this.selDescp_SelectedIndexChanged);
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion

        // This function sets the insertion mode for Adding the list item. 
        private void btnAddNew_Click(object sender, System.EventArgs e)
        {
            try
            {
                //txtCode.Visible=true;
                selDescp.SelectedIndex=0;
				txtDescp.Text = "";
				lblInsertMode.Text="-1";
                
            }
            catch//(Exception ex)
            {

            }
            finally
            {
					
            }
        }

        
        // This function saves the newly created list option in the database.
        private void btnSave_Click(object sender, System.EventArgs e)
        {	
            try
            {
                objEditList = new UserDefinedOne();
                int lintReturnVal=-1;				
                
                if(lblInsertMode.Text=="-1")
                {	
                    // Saving data in QUESTIONLISTOPTIONMASTER table
                    lintReturnVal = objEditList.fnInsertListData(int.Parse(lblListID.Text),txtDescp.Text,1);				
                }
                else
                {
                    lintReturnVal = objEditList.fnUpdateListData(txtDescp.Text,int.Parse(lblListID.Text),int.Parse(selDescp.SelectedItem.Value));
                }
                if(lintReturnVal>0)
                {									
                    Response.Redirect("EditList.aspx?ListID="+lblListID.Text+"&QID="+lblQID.Text+"&screenID="+hidScreenID.Value+"&tabID="+hidTabID.Value+"&grpID="+hidGroupID.Value+"&calledFrom="+hidCalledFrom.Value+"&transferdata=",false);
                }
                
            }
            catch//(Exception ex)
            {
             
            }
            finally
            {
                objEditList.Dispose();							
            }
        }
        // This function is invoked on change of the list item and sets the selected item in the text box.
        private void selDescp_SelectedIndexChanged(object sender, System.EventArgs e)
        {	
            try
            {
                objEditList = new UserDefinedOne();
                if(selDescp.SelectedItem.Value!="0")
                {

					lblInsertMode.Text  = "";
					
                    // The function returns the optionname from the QUESTIONLISTOPTIONMASTER.
                    txtDescp.Text= objEditList.fnGetOptionName(lblListID.Text,selDescp.SelectedItem.Value);
                }			
            }
            catch//(Exception ex)
            {

            }
            finally
            {
                objEditList.Dispose();				
            }
        }
	}
}
