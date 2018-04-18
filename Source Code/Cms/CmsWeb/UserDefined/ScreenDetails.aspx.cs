/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> May 25,2005
	<End Date				: - >
	<Description			: - > This file is used to implement functionality for user defined screen details 
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
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 

 


namespace Cms.CmsWeb.User_Defined
{
	/// <summary>
	/// Summary description for ScreenDetails.
	/// </summary>
	public class ScreenDetails : Cms.CmsWeb.cmsbase  
	{
         
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Panel pnlMsg;
        protected System.Web.UI.WebControls.Label lblClass;
        protected System.Web.UI.WebControls.DropDownList ddlClass;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqddlClass;
        protected System.Web.UI.WebControls.Label lblSubClass;
        protected System.Web.UI.WebControls.DropDownList ddlSubClass;
        //protected System.Web.UI.WebControls.RequiredFieldValidator reqddlSubClass; Changed by amit k mishra for tfs bug #836
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSubClass;//Added by Amit K MISHRA FOR TFS BUG #836
        protected System.Web.UI.WebControls.Label lblScreenName;
        protected System.Web.UI.WebControls.TextBox txtScreenName;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqtxtScreenDetails;

        protected System.Web.UI.WebControls.RegularExpressionValidator regScreenDetails;
        protected System.Web.UI.WebControls.Label lblDispName;
        protected System.Web.UI.WebControls.TextBox txtdispName;
        protected System.Web.UI.WebControls.RequiredFieldValidator ReqDispScreen;
        protected System.Web.UI.WebControls.RegularExpressionValidator RegDispScreen;

        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.TextBox txtDeactivateVal;
        protected System.Web.UI.WebControls.Label lblHidTempID;
        //protected System.Web.UI.HtmlControls.HtmlInputButton  btnReset;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;

        #region VARIABLE DECLARATION
         
            protected string gStrStyle,cssFolder;
            protected string gStrExists="";
            protected int gIntInsertUpdateFlag;
            protected int gIntReturn;
            public string gStrClassInitText="";
            public string gStrProfInitText="";
            public string gStrScreenID="";
            public string gStrScreenName="";
            public string gStrScreenTitle="";		
            public string gStrSaveMsgText="";
            public string gStrTitleMsgText="";
            //private UserDefinedOne objSubmitScreen;
            protected System.Web.UI.WebControls.Label Label1;
            protected int gIntScreenID=26;		
            public string gStrTemplateID="";		
            public string gStrSecure="",gStrUserID="0";
            protected int gIntShowReset = 0,gIntSavedScreenID=0,gIntRefresh=0;		
            private System.Resources.ResourceManager aobjResMang;
            protected UserDefinedOne objSubmitScreen;
            protected DataSet dtSUBLOB;
			private int lIntClassID;
			private int lIntSubClassID;
        #endregion
    
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            //btnReset.Attributes.Add("onclick","javascript:ResetScreenForm();");

               base.ScreenId="128_0";

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
     

            btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
            btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

            btnSave.CmsButtonClass	=	CmsButtonType.Write;
            btnSave.PermissionString		=	gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			gStrUserID=GetUserId();

            SetCaptions();
            try
            {
                objSubmitScreen= new UserDefinedOne();
        
				lIntClassID=0;
				lIntSubClassID=0;
				if(!IsPostBack)
                {
                    InitializeScreenDetails();
                    if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/UserDefined/support/PageXML/" + GetSystemId(), "ScreenDetails.xml"))
                    {
                        setPageControls(Page, @Request.PhysicalApplicationPath + "CmsWeb/UserDefined/support/PageXML/" + GetSystemId() + "/ScreenDetails.xml");
                    }
                }
											
            }				
                // Put user code to initialize the page here
            catch
            {
                
                //throw new BritAmazonException(ex);
            }
            finally
            {
                objSubmitScreen.Dispose();				
            }	
		}


        /*This function is executed on change of the class drop down and corresponding sub class details
         * are populated.
         */
        private void ddlClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            objSubmitScreen= new UserDefinedOne();
            try
            {
                if (ddlClass.SelectedItem.Value!="")
                {
                    // Populating the subclasses for the selected class
                    ddlSubClass.DataSource = objSubmitScreen.fnGetSubClass(int.Parse(ddlClass.SelectedItem.Value));									
                    ddlSubClass.Items.Clear();
                    ddlSubClass.DataTextField="SUB_LOB_DESC";
                    ddlSubClass.DataValueField="SUB_LOB_ID";			
                    ddlSubClass.DataBind();
                    //ddlSubClass.Items.Insert(0, new ListItem(reqddlSubClass.InitialValue,reqddlSubClass.InitialValue));// Changed by Amit k mishra for tfs Bug 836
                    ddlSubClass.Items.Insert(0, new ListItem(rfvSubClass.InitialValue, rfvSubClass.InitialValue));// Added by Amit k mishra for tfs Bug 836
                    ddlSubClass.SelectedIndex=0;
                }	
                else
                {
                    ddlSubClass.Items.Clear();
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            finally
            {
					
            }	
        }

        private void SetCaptions()
        {
            aobjResMang = new System.Resources.ResourceManager("Cms.CmsWeb.User_Defined.ScreenDetails", System.Reflection.Assembly.GetExecutingAssembly());

            lblScreenName.Text = aobjResMang.GetString("lblScreenText");

            reqtxtScreenDetails.ErrorMessage = aobjResMang.GetString("reqScreenText");
            reqddlClass.InitialValue = aobjResMang.GetString("classInitText");

            lblMessage.Text = aobjResMang.GetString("strSuccessMsg");
            lblClass.Text = aobjResMang.GetString("lblClassText");		// Populating the labels.
            lblSubClass.Text = aobjResMang.GetString("lblSubClassText");

            gStrClassInitText = aobjResMang.GetString("classInitText");
            gStrProfInitText = aobjResMang.GetString("profInitText");
            gStrScreenID = aobjResMang.GetString("strScreenID");
            gStrScreenName = aobjResMang.GetString("strScreenName");
            gStrScreenTitle = aobjResMang.GetString("strScreendetails");
            reqddlClass.Text = aobjResMang.GetString("reqddlclassmsg");
            //reqddlSubClass.Text = aobjResMang.GetString("reqddlsubClassmsg");
            rfvSubClass.Text = aobjResMang.GetString("reqddlsubClassmsg");
            regScreenDetails.ValidationExpression = aRegExpAlpha;
            regScreenDetails.Text = aobjResMang.GetString("regScreentxt");
            gStrSaveMsgText = aobjResMang.GetString("lblSaveMessage");
            gStrTitleMsgText = aobjResMang.GetString("strScreendetails");

            lblDispName.Text = aobjResMang.GetString("lblDispName");
            ReqDispScreen.Text = aobjResMang.GetString("reqDispName");
            RegDispScreen.ValidationExpression = aRegExpAlpha;
            RegDispScreen.Text = aobjResMang.GetString("regScreentxt");
            btnReset.Value = aobjResMang.GetString("btnReset");
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
			this.ddlClass.SelectedIndexChanged += new System.EventHandler(this.ddlClass_SelectedIndexChanged);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnReset.ServerClick += new System.EventHandler(this.btnReset_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        /*This function will save the Screen Detail in the Database.
        */
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                objSubmitScreen=new UserDefinedOne();
                if(lblHidTempID.Text!="-1" && lblHidTempID.Text!="")
                {
                    // Updating the Screen details
                   gIntReturn = objSubmitScreen.fnUpdateScreenData(txtScreenName.Text,lblHidTempID.Text,ddlClass.SelectedItem.Value,ddlSubClass.SelectedItem.Value,txtdispName.Text,int.Parse(gStrUserID));
                }
                else
                {
                    // Inserting the Screen details
                    string strSubClass = ddlSubClass.SelectedValue != "" ? ddlSubClass.SelectedValue : "0";
                    gIntReturn = objSubmitScreen.fnInsertScreenData(txtScreenName.Text,ddlClass.SelectedItem.Value,strSubClass, txtdispName.Text,int.Parse(gStrUserID));//Changed by Amit k Mishra for Tfs Bug #836
                    lblHidTempID.Text = gIntReturn.ToString();
					btnActivateDeactivate.Text =  aobjResMang.GetString("lStrDeActivate");
					btnActivateDeactivate.Visible =true;
                    txtDeactivateVal.Text="Y";
                }
				if(gIntReturn>0)
				{
					pnlMsg.Visible=true;
					lblMessage.Visible=true;
					gIntInsertUpdateFlag=1;
					gIntSavedScreenID = int.Parse(lblHidTempID.Text);
					gIntRefresh=1;
				}
				else
				{
					if (gIntReturn == -1)
					{
						pnlMsg.Visible=true;
						lblMessage.Text = "For the particular combination of LOB and Sub-LOB A Screen already exists. ";
						lblMessage.Visible=true;
					}
					gIntRefresh=0;
				}
                //ContextUtil.SetComplete();
            }
            catch
            {
                //ContextUtil.SetAbort();
                //throw new BritAmazonException(ex);
            }
            finally
            {
                objSubmitScreen.Dispose();			
            }	
        }

        /* This function is used to activate/Deactivate the Screen.
     */ 
        public void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
			
            try
            {
                string lStrIsActive;			
                int lIntScreenId=-1;
                if(lblHidTempID.Text!="")
                 lIntScreenId=int.Parse(lblHidTempID.Text);			
                objSubmitScreen=new UserDefinedOne();
                lStrIsActive=txtDeactivateVal.Text.Trim();
                if (lStrIsActive.Equals("Y"))
                {
                   lStrIsActive=txtDeactivateVal.Text="N";
                   btnActivateDeactivate.Text= aobjResMang.GetString("lStrActivate");                    
                   lblMessage.Text=aobjResMang.GetString("ScrDeactiveMsg");
                }
                else if (lStrIsActive.Equals("N"))
                {
                   lStrIsActive=txtDeactivateVal.Text="Y";
                   btnActivateDeactivate.Text= aobjResMang.GetString("lStrDeActivate");                  
                   lblMessage.Text=aobjResMang.GetString("ScrActiveMsg");
                }
				gIntReturn=objSubmitScreen.fnDeactivateScreen(lblHidTempID.Text,lStrIsActive.Trim(),int.Parse (gStrUserID));										

                gIntInsertUpdateFlag=1;
				pnlMsg.Visible=true;
				lblMessage.Visible=true;
 

            }
            catch
            {
               
//                throw  new BritAmazonException(ex);
            }
            finally
            {
                objSubmitScreen.Dispose();				
            }

        }

		public void btnReset_ServerClick(object sender, System.EventArgs e)
		{
			//InitializeScreenDetails();
			Response.Redirect(Request.Url.ToString());
		}

		private void InitializeScreenDetails()
		{
			gStrTemplateID = Request["SCREENID"]==null?"":Request["SCREENID"];		
					
			lblHidTempID.Text=gStrTemplateID;										

			// Begining the Security Test

			//Getting the lobs
			ClsStates objState = new ClsStates();
			DataTable dt = objState.PoplateLob().Tables[0];
			
			ddlClass.DataSource = dt;	// Populating the Class drop down list				
			ddlClass.DataTextField="LOB_DESC";
			ddlClass.DataValueField="LOB_ID";			
			ddlClass.DataBind();

			ddlClass.Items.Insert(0, new ListItem("",""));
			ddlClass.SelectedIndex=0;

			//PopulateProfessionDDL(); //populating the Business dropdown
			if(gStrTemplateID.ToString()!="-1" && gStrTemplateID!="")
			{
				btnActivateDeactivate.Visible=true;
							
				DataRow lDRScreen= objSubmitScreen.fnGetScreenData(int.Parse(gStrTemplateID));	// Retrieve data from DB for that ScreenID
  										
				if(lDRScreen["SCREENNAME"].ToString() != "-1" && lDRScreen["SCREENNAME"].ToString() != "")
				{							
					txtScreenName.Text =lDRScreen["SCREENNAME"].ToString();
				}

				if(lDRScreen["DISPLAYNAME"].ToString() != "-1" && lDRScreen["DISPLAYNAME"].ToString() != "")
				{							
					txtdispName.Text =lDRScreen["DISPLAYNAME"].ToString();
				}
							
				txtDeactivateVal.Text=lDRScreen["ISACTIVE"].ToString();
				if (txtDeactivateVal.Text.Equals("Y"))
				{
					btnActivateDeactivate.Text =  aobjResMang.GetString("lStrDeActivate");
				}
				else
				{
					btnActivateDeactivate.Text =  aobjResMang.GetString("lStrActivate");
				}			

				if(lDRScreen["CLASSID"].ToString() != "-1" && lDRScreen["CLASSID"].ToString() != "")
				{							
					lIntClassID=int.Parse(lDRScreen["CLASSID"].ToString());
					
					objState = new ClsStates();
					ddlClass.DataSource = objState.PoplateLob();
					ddlClass.DataTextField="LOB_DESC";
					ddlClass.DataValueField="LOB_ID";			
					ddlClass.DataBind();
					ddlClass.Items.Insert(0, new ListItem(reqddlClass.InitialValue,reqddlClass.InitialValue));							
				}

				ddlClass.ClearSelection(); 
				ListItem li=new ListItem();
				li=ddlClass.Items.FindByValue(lIntClassID.ToString());
				if(li!=null)
					li.Selected=true; 

				if(lDRScreen["SUBCLASSID"].ToString() != "-1" && lDRScreen["SUBCLASSID"].ToString() != "")
				{							
					lIntSubClassID=int.Parse(lDRScreen["SUBCLASSID"].ToString());								
					ddlSubClass.Items.Clear();
					dtSUBLOB=objSubmitScreen.fnGetSubClass(lIntClassID);
					ddlSubClass.DataSource = dtSUBLOB;
					ddlSubClass.DataTextField="SUB_LOB_DESC";
					ddlSubClass.DataValueField="SUB_LOB_ID";			
					ddlSubClass.DataBind();
					//ddlSubClass.Items.Insert(0, new ListItem(reqddlSubClass.InitialValue,reqddlSubClass.InitialValue));//Changed by Amit k Mishra for tfs bug#836
                    ddlSubClass.Items.Insert(0, new ListItem(rfvSubClass.InitialValue, rfvSubClass.InitialValue));//Added by Amit k Mishra for tfs bug#836
					ddlSubClass.SelectedIndex=0;
				}

				ddlSubClass.ClearSelection();
				ListItem li1=new ListItem();
				li1=ddlSubClass.Items.FindByValue(lIntSubClassID.ToString());
				if(li1!=null)
					li1.Selected=true;                      
            }
			else
			{
				btnActivateDeactivate.Visible=false;
														
			}
		}
	}
}
