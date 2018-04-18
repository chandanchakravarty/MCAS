/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 27/05/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the user defined Question DETAIL  module
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
using System.Text;

namespace Cms.CmsWeb.UserDefined
{
    /// <summary>
    /// Summary description for GridQuation.
    /// </summary>
    public class GridQuestion : Cms.CmsWeb.cmsbase  
    {
        #region CONTROL REFERENCE
        protected System.Web.UI.WebControls.Label lblError;
        protected System.Web.UI.WebControls.Panel pnlMessage;
        protected System.Web.UI.WebControls.Label lblQuestion;
        protected System.Web.UI.WebControls.TextBox txtQuestion;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqtxtQuestion;
        protected System.Web.UI.WebControls.CustomValidator CVtxtQuestionDesc;
        protected System.Web.UI.WebControls.Label lblQuestionType;
        protected System.Web.UI.WebControls.DropDownList ddlQuesType;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqddlQuesType;
        protected System.Web.UI.WebControls.Label lblTotalField;
        protected System.Web.UI.WebControls.CheckBox chkTotal;
        protected System.Web.UI.WebControls.Panel pntTotalField;
        protected System.Web.UI.WebControls.Label lblAnsType;
        protected System.Web.UI.WebControls.DropDownList ddlAnswerType;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqddlAnswerType;
        protected System.Web.UI.WebControls.Panel pnlDdlAnsType;
        protected System.Web.UI.WebControls.Label lblList;
        protected System.Web.UI.WebControls.DropDownList ddlList;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqddlListType;
        protected System.Web.UI.WebControls.Panel pnlDdlList;
        protected System.Web.UI.WebControls.Label lblDescRequired;
        protected System.Web.UI.WebControls.CheckBox chkDesc;
        protected System.Web.UI.WebControls.Panel pnlChkDescription;
        protected System.Web.UI.WebControls.Label lblSpecify;
        protected System.Web.UI.WebControls.ListBox lstOptionList;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqlstOptionList;
        protected System.Web.UI.WebControls.Panel pnlDdlDescription;
        protected System.Web.UI.WebControls.Label lblIsMandatory;
        protected System.Web.UI.WebControls.DropDownList ddlMandatory;
        protected System.Web.UI.WebControls.Label lblSuffix;
        protected System.Web.UI.WebControls.TextBox txtsuffix;
        protected System.Web.UI.WebControls.Label lblsfxex;
        protected System.Web.UI.WebControls.Label lblPrefix;
        protected System.Web.UI.WebControls.TextBox txtprefix;
        protected System.Web.UI.WebControls.Label lblprefixex;
        protected System.Web.UI.WebControls.Label lblQuestionNotes;
        protected System.Web.UI.WebControls.TextBox txtQuestionNotes;
        protected System.Web.UI.WebControls.CustomValidator CvQuesNotes;
        protected System.Web.UI.WebControls.Panel pnlCommonQuestion;
        
        
        protected System.Web.UI.WebControls.TextBox txtDeactivateVal;
        protected System.Web.UI.WebControls.Label lblTemplateID;
        protected System.Web.UI.WebControls.Label lblBackID;
        protected System.Web.UI.WebControls.Label lblTabID;
        protected System.Web.UI.WebControls.Label lblGroupID;
        protected System.Web.UI.WebControls.Label lblGroupStatus;
        protected System.Web.UI.WebControls.Label lblordexist;
        protected System.Web.UI.WebControls.Label lblupdate;
        protected System.Web.UI.WebControls.Label lblerrmsg;
        protected System.Web.UI.WebControls.Label lblinsert;
        protected System.Web.UI.WebControls.Label lblGridBtn;
        protected System.Web.UI.WebControls.Label lblSaveBtn;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputButton  btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
         
        #endregion
        

        #region LOCAL VARIABLES
        public int grpID=-1;
        protected string gStrReqQuestion;
        protected string gStrReqOrder;
        protected string gStrReqQuestype;
        protected string gStrReqAnstype;
        protected string lStrTemplateID;
        protected string lStrTabID;
        protected string lStrScreenID="";
        protected string lStrGroup,cssFolder;
        protected string lStrGroupID="-1";
        protected string gStrCalledFrom;
        protected int gIntTabID,gIntQID;
        protected string lStrRequired="";
        protected string lStrActive="";
        protected int lIntGroupID=0;
        protected int gIntQOrderNo=0;
        protected string gstrType;		
        protected string gStrCarrierID="";
        protected string gStrStyle="";
        protected string lstrbtnGridText="";
        protected int gIntInsertUpdateFlag;
        protected int gIntReturn;
        protected int gIntGridQuestion;        
        protected string lStrAddLink1="";
        protected string lStrAddLink2="";
        protected string lStrAddLink3="";
        protected string lStrViewLink1="";
        protected string lStrViewLink2="";
        protected string lStrViewLink3="";
        protected string  gStrViewMsg="";
        protected string  gStrSaveMsgText,gStrMandatoryMsgText,gStrTabClickMsgText,gStrTitleMsgText;
        protected string gStrExists="";        
        protected int gintLayout;	
        protected string gStrSecure="";
        protected string gStrdrpmanone="";
        protected string gStrdrpmantwo="";
        private ClsUserDefinedTwo lObjSubmit;                
        private string lStrTotalField="";
        private System.Resources.ResourceManager aobjResMang;
        private DataSet dsQuestionType;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidScreenID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidGroupID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQid;
		protected System.Web.UI.WebControls.TextBox txtRepatableColumns;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRepeatCol;
		protected System.Web.UI.WebControls.CompareValidator cvRepCols;
		protected System.Web.UI.WebControls.RangeValidator rvRepeatColumns;
		protected System.Web.UI.WebControls.TextBox txtHeight;
		protected System.Web.UI.WebControls.DropDownList ddlHeightMsrment;
		protected System.Web.UI.WebControls.CompareValidator cvHeight;
		protected System.Web.UI.WebControls.TextBox txtWidth;
		protected System.Web.UI.WebControls.DropDownList ddlWidthMsrment;
		protected System.Web.UI.WebControls.CompareValidator cvWidth;
		protected System.Web.UI.WebControls.TextBox txtMaxLength;
		protected System.Web.UI.WebControls.CompareValidator cvMaxChars;
		protected System.Web.UI.WebControls.DropDownList ddlValidation;
		protected System.Web.UI.WebControls.Button btnDeactiveQuestion;
		protected System.Web.UI.HtmlControls.HtmlTableRow trHeightStyle;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCharacterStyle;
		protected System.Web.UI.WebControls.Label lblHeight;
		protected System.Web.UI.WebControls.Label lblWidth;
		protected System.Web.UI.WebControls.Label lblColumnSpan;
		protected System.Web.UI.WebControls.Label lblValidationType;
		protected System.Web.UI.WebControls.Label lblMaxCharacters;
		protected System.Web.UI.WebControls.TextBox txtDepQuesDescription;
		protected System.Web.UI.WebControls.DropDownList ddlDepQuesType;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDepQuesTYpe;
		protected System.Web.UI.WebControls.DropDownList ddlDepAnswerType;
		protected System.Web.UI.WebControls.RequiredFieldValidator reqDepAnswerType;
		protected System.Web.UI.WebControls.DropDownList ddlDepList;
		protected System.Web.UI.WebControls.RequiredFieldValidator reqDepList;
		protected System.Web.UI.WebControls.Label lblDepList;
        
        private DataSet dsAnswerType;
        

        #endregion
        
        private void SetCaptions()
        {
            aobjResMang=new System.Resources.ResourceManager("Cms.CmsWeb.UserDefined.GridQuestion" ,System.Reflection.Assembly.GetExecutingAssembly()); 
            lblQuestion.Text = aobjResMang.GetString("lblQuestionText");		// Populating the labels.								
            lblQuestionType.Text = aobjResMang.GetString("lblQuestionTypeText");
            lblAnsType.Text = aobjResMang.GetString("lblAnsTypeText");
            lblList.Text= aobjResMang.GetString("lblListTypeText");				
            lblSuffix.Text = aobjResMang.GetString("lblSuffixText");
            lblPrefix.Text = aobjResMang.GetString("lblPrefixText");
            lblQuestionNotes.Text = aobjResMang.GetString("lblQuestionNotesText");
         
            lblSaveBtn.Text = aobjResMang.GetString("btnSaveText");	
            reqtxtQuestion.ErrorMessage=aobjResMang.GetString("reqtxtQuestionText");					
            reqddlQuesType.ErrorMessage=aobjResMang.GetString("reqddlQuesTypeText");
            reqddlAnswerType.ErrorMessage=aobjResMang.GetString("reqddlAnswerTypeText");				
            reqddlListType.ErrorMessage=aobjResMang.GetString("reqddlListTypeText");
            reqlstOptionList.ErrorMessage=aobjResMang.GetString("reqddlListOptionText");				
            //btnCancel.Value=aobjResMang.GetString("btnCancelText");				
            lblupdate.Text=aobjResMang.GetString("strMsgEdit");	
            lblerrmsg.Text=aobjResMang.GetString("strErrorSave");
            lblinsert.Text=aobjResMang.GetString("strMsgInsert");
            lblSpecify.Text= aobjResMang.GetString("lblSpecify");
            lblDescRequired.Text=aobjResMang.GetString("lblDescRequired");
            lStrAddLink1=aobjResMang.GetString("AddNewLinkText1");
            lStrAddLink2=aobjResMang.GetString("AddNewLinkText2");
            lStrAddLink3=aobjResMang.GetString("AddNewLinkText3");
            lStrViewLink1=aobjResMang.GetString("ViewLinkText1");
            lStrViewLink2=aobjResMang.GetString("ViewLinkText2");
            lStrViewLink3=aobjResMang.GetString("ViewLinkText3");
            lblTotalField.Text =aobjResMang.GetString("lblTotal");
            gStrViewMsg=aobjResMang.GetString("MsgViewText");
            gStrSaveMsgText=aobjResMang.GetString("lblSaveMessage");								
            gStrTitleMsgText=aobjResMang.GetString("strScreendetails");
            gStrdrpmanone=aobjResMang.GetString("drpdownmandatoryone");
            gStrdrpmantwo=aobjResMang.GetString("drpdownmandatorytwo");
            lblIsMandatory.Text=aobjResMang.GetString("lblIsMandatoryText");				
           
            CVtxtQuestionDesc.Text=aobjResMang.GetString("chkquestiondesc");
            CvQuesNotes.Text=aobjResMang.GetString("chkquestionnotes");
            lblsfxex.Text=aobjResMang.GetString("strsufixex");
            lblprefixex.Text=aobjResMang.GetString("strprefixex");

			//changed by manab on 29 june 2005
			lblColumnSpan.Text				=aobjResMang.GetString("strColSpan");
			lblHeight.Text					=aobjResMang.GetString("strHeight");
			lblWidth.Text					=aobjResMang.GetString("strWidth");
			lblMaxCharacters.Text			=aobjResMang.GetString("strMaxChars");
			lblValidationType.Text			=aobjResMang.GetString("strValidationType");

			cvHeight.ErrorMessage			=aobjResMang.GetString("cvHeight");
			cvWidth.ErrorMessage			=aobjResMang.GetString("cvWidth");
			cvMaxChars.ErrorMessage			=aobjResMang.GetString("cvMaxChars");
			rfvRepeatCol.ErrorMessage		=aobjResMang.GetString("rfvRepeatCol");
			cvRepCols.ErrorMessage			=aobjResMang.GetString("cvRepCols");
			rvRepeatColumns.ErrorMessage	=aobjResMang.GetString("rvRepeatColumns");


			//fecthing the tab columns
			if (Request["TabID"] != null)
			{
				UserDefinedOne loUserDefined = new UserDefinedOne();
				int TabColumns =1;
				DataRow drTabdet =  loUserDefined.fnGetTabData(int.Parse(Request["TabID"]),int.Parse(Request["ScreenID"]));
				if (drTabdet["REPEATCONTROLS"] != DBNull.Value)
				{
					TabColumns = int.Parse(drTabdet["REPEATCONTROLS"].ToString());
					rvRepeatColumns.ErrorMessage += TabColumns.ToString();
					rvRepeatColumns.MaximumValue = TabColumns.ToString();

				}
			}
         
        }


        private void FillCombo()
        {
            lObjSubmit= new ClsUserDefinedTwo();
            dsQuestionType=new DataSet(); 
            dsAnswerType=new DataSet(); 

            dsQuestionType=lObjSubmit.fnGetQuestionType(1);
            ddlQuesType.DataSource = dsQuestionType;
            ddlQuesType.DataTextField="QUESTIONTYPENAME";
            ddlQuesType.DataValueField="QUESTIONTYPEID";			
            ddlQuesType.DataBind();						
            ddlQuesType.Items.Insert(0, new ListItem(reqddlQuesType.InitialValue, reqddlQuesType.InitialValue));					
            ddlQuesType.SelectedIndex = 0;

            dsAnswerType=lObjSubmit.fnGetQuestionType(2);
            ddlAnswerType.DataSource = dsAnswerType;
            ddlAnswerType.DataTextField="QUESTIONTYPENAME";
            ddlAnswerType.DataValueField="QUESTIONTYPEID";			
            ddlAnswerType.DataBind();							
            ddlAnswerType.Items.Insert(0, new ListItem(reqddlAnswerType.InitialValue, ""));				
            ddlAnswerType.SelectedIndex = 0;

            ddlList.DataSource = lObjSubmit.fnList("").Tables[0];
            ddlList.DataTextField="NAME";
            ddlList.DataValueField="LISTID";			
            ddlList.DataBind();							
            ddlList.Items.Insert(0, new ListItem(reqddlListType.InitialValue, ""));				
            ddlList.SelectedIndex = 0;						
												
            ddlMandatory.Items.Add(new ListItem(gStrdrpmantwo,"N"));
            ddlMandatory.Items.Add(new ListItem(gStrdrpmanone,"Y"));

        }
        private void Page_Load(object sender, System.EventArgs e)
        {

            btnReset.Attributes.Add("onclick","javascript:ResetScreenForm();");

            base.ScreenId="623";

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
             btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
            btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

            btnSave.CmsButtonClass	=	CmsButtonType.Write;
            btnSave.PermissionString		=	gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************

            try
            {
                lObjSubmit= new ClsUserDefinedTwo();
            
                gStrCarrierID="1";
                gstrType = "";				

                SetCaptions();
                aobjResMang=new System.Resources.ResourceManager("Cms.CmsWeb.UserDefined.GridQuestion" ,System.Reflection.Assembly.GetExecutingAssembly()); 
                //Initializing the object to populate dropdownlist for group,question type and answer type.				
					
                gStrCalledFrom = Request["CalledFrom"];
                hidCalledFrom.Value = gStrCalledFrom;
              

                if(!IsPostBack)
                {
                    InitializeScreenDetails();
                }	//	End for IsPostBack 	
            }

            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                  
            }
            finally
            {
                lObjSubmit.Dispose();				
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
			this.ddlQuesType.SelectedIndexChanged += new System.EventHandler(this.ddlQuesType_SelectedIndexChanged);
			this.ddlAnswerType.SelectedIndexChanged += new System.EventHandler(this.ddlAnswerType_SelectedIndexChanged);
			this.ddlList.SelectedIndexChanged += new System.EventHandler(this.ddlList_SelectedIndexChanged);
			this.chkDesc.CheckedChanged += new System.EventHandler(this.chkDesc_CheckedChanged);
			this.ddlDepQuesType.SelectedIndexChanged += new System.EventHandler(this.ddlDepQuesType_SelectedIndexChanged);
			this.ddlDepAnswerType.SelectedIndexChanged += new System.EventHandler(this.ddlDepAnswerType_SelectedIndexChanged);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnDeactiveGroup_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnReset.ServerClick += new System.EventHandler(this.btnReset_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
        #endregion	
        
        
        // This function is invoked when the Check box for required description is Checked/Unchecked
        private void chkDesc_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                lObjSubmit= new ClsUserDefinedTwo();		//Initializing the object  
                if(!chkDesc.Checked)
                {
                    pnlDdlDescription.Visible=false;
                    lstOptionList.Visible=false;
                    lblSpecify.Visible=false;
                    reqlstOptionList.Enabled=false;
                }
                else
                {
                    pnlDdlDescription.Visible=true;
                    lstOptionList.Visible=true;
                    lblSpecify.Visible=true;
                    reqlstOptionList.Enabled=true;
                    if(ddlList.SelectedItem.Value.Trim()!="")
                    {
                        lblSpecify.Visible=true;
                        lstOptionList.Visible=true;
                        DataSet dsOptionList = new DataSet();						
                        lstOptionList.DataSource = new DataView(lObjSubmit.fnOptionList(ddlList.SelectedItem.Value.ToString()).Tables[0]);													
                        lstOptionList.DataTextField="OPTIONNAME";
                        lstOptionList.DataValueField="OPTIONID";			
                        lstOptionList.DataBind();	
                        lstOptionList.Items.Insert(0, new ListItem(reqlstOptionList.InitialValue, ""));				
                    }
                }
                if(ddlAnswerType.SelectedItem.Value=="11")		// Checking if the List Dropdown should be populated on System Based List
                {
                    gstrType="S";
                }
                else if(ddlAnswerType.SelectedItem.Value=="12") // Checking if the List Dropdown should be populated on User Defined List
                {
                    gstrType="U";
                }
                else if(ddlAnswerType.SelectedItem.Value=="14")  // Checking if the List Dropdown should be populated on Direct List
                {
                    gstrType="D";
                }
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lObjSubmit.Dispose();				
            }
        }

        private void ddlQuesType_SelectedIndexChanged(object sender, System.EventArgs e)
        {	
			
			try
			{
				lObjSubmit= new ClsUserDefinedTwo();
				fnShowControls();
				//changed by manab added two more question type for radio button list and checkbox list
				if((ddlQuesType.SelectedItem.Value=="1") || (ddlQuesType.SelectedItem.Value=="2") || (ddlQuesType.SelectedItem.Value=="13") || (ddlQuesType.SelectedItem.Value=="14")) // If user selects Single list or multiple list from drop down list
				{					
					fnForQuestion();
					pnlDdlAnsType.Visible=true;
					reqddlAnswerType.Enabled=true;
					lblAnsType.Visible=true;
					ddlAnswerType.Visible=true;
					ddlAnswerType.Items.Clear();
					ddlAnswerType.DataSource = lObjSubmit.fnGetQuestionType(2);
					ddlAnswerType.DataTextField="QUESTIONTYPENAME";
					ddlAnswerType.DataValueField="QUESTIONTYPEID";			
					ddlAnswerType.DataBind();							
					ddlAnswerType.Items.Insert(0, new ListItem("", ""));				
					ddlAnswerType.SelectedIndex = 0;

					
					pnlCommonQuestion.Visible=true;
				}
				else
				{
					pnlDdlAnsType.Visible=false;
					reqddlAnswerType.Enabled=false;
					lblAnsType.Visible=false;
					ddlAnswerType.Visible=false;					
					lblList.Visible=false;
					ddlList.Visible=false;
				}
				
				if((ddlQuesType.SelectedItem.Value=="8") || (ddlQuesType.SelectedItem.Value=="9")) // If user selects HGrid or VGrid from drop down list
				{
					fnHideControls();					
					//btnSave.Text= lblGridBtn.Text;	
					pnlCommonQuestion.Visible=false;
					btnSave.Text= aobjResMang.GetString("btnGridText");	
					reqddlAnswerType.Enabled=false;
					//if(ddlQuesType.SelectedItem.Value=="8")
						//pntTotalField.Visible=true;
				}
				else
				{
					btnSave.Text=lblSaveBtn.Text;
					pntTotalField.Visible=false;
				}

				if(ddlQuesType.SelectedItem.Value=="10") // If user selects Heading from drop down list
				{
					fnHideControls();
					pnlCommonQuestion.Visible=false;
					reqddlAnswerType.Enabled=false;
				}
				if(ddlQuesType.SelectedItem.Value=="4" || ddlQuesType.SelectedItem.Value=="5" || ddlQuesType.SelectedItem.Value=="6" || ddlQuesType.SelectedItem.Value=="7")
				{	
					fnForQuestion();
					pnlCommonQuestion.Visible=true;	
					//fnShowControls();
				}

				//single select list
				if(ddlQuesType.SelectedItem.Value=="1" || ddlQuesType.SelectedItem.Value=="2" || ddlQuesType.SelectedItem.Value=="13" || ddlQuesType.SelectedItem.Value=="14" || ddlQuesType.SelectedItem.Value=="5")
				{
					trCharacterStyle.Visible=false;
					if (ddlQuesType.SelectedItem.Value=="13" || ddlQuesType.SelectedItem.Value=="14"  || ddlQuesType.SelectedItem.Value=="5")
					{
						trHeightStyle.Visible=false;
					}
				}
				if (ddlQuesType.SelectedItem.Value=="4" || ddlQuesType.SelectedItem.Value=="6" || ddlQuesType.SelectedItem.Value=="7")
				{
					trCharacterStyle.Visible=true;
					trHeightStyle.Visible=true;
				}
				if (ddlQuesType.SelectedItem.Value=="4")
				{
					if (ddlValidation.Items.FindByText("Number")!= null)
					{
						ddlValidation.SelectedIndex=-1;
						ddlValidation.Items.FindByText("Number").Selected=true;
					}
					ddlValidation.Enabled=false;
				}

				
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lObjSubmit.Dispose();				
			}	
	
        }
	

        /* This fucntion is hides the form controls based on the selection of question.
          */
        public void fnForQuestion()
        {
            try
            {
                pnlDdlAnsType.Visible=false;
                pnlDdlList.Visible=false;
                pnlChkDescription.Visible=false;
                pnlDdlDescription.Visible=false;				
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
				
            }	
        }
        /* This fucntion is hides the form controls based on the selection of question.
                  */
        public void fnForAnswer()
        {
            try
            {				
                pnlDdlList.Visible=false;
                pnlChkDescription.Visible=false;
                pnlDdlDescription.Visible=false;				
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
				
            }	
        }
        // This function is invoked whenever the Answer type drop down is changed.
        private void ddlAnswerType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
			
            try
            {	
                lObjSubmit= new ClsUserDefinedTwo();		//Initializing the object  
                if(ddlAnswerType.SelectedItem.Value=="11")		// Checking if the List Dropdown should be populated on System Based List
                {
                    gstrType="S";
                }
                else if(ddlAnswerType.SelectedItem.Value=="12") // Checking if the List Dropdown should be populated on User Defined List
                {
                    gstrType="U";
                }
                else if(ddlAnswerType.SelectedItem.Value=="14")  // Checking if the List Dropdown should be populated on Direct List
                {
                    gstrType="D";
                }
                fnForAnswer();
                if(ddlAnswerType.SelectedItem.Value!="")
                {				
                    pnlDdlList.Visible=true;
                    reqddlListType.Enabled=true;
                    lblList.Visible =true;
                    ddlList.Visible=true;					
                    ddlList.DataSource = new DataView(lObjSubmit.fnList(gstrType).Tables[0]);
                    ddlList.DataTextField="NAME";
                    ddlList.DataValueField="LISTID";			
                    ddlList.DataBind();							
                    ddlList.Items.Insert(0, new ListItem(reqddlListType.InitialValue, ""));				
                    ddlList.SelectedIndex = 0;
                }
                else
                {
                    pnlDdlList.Visible=false;
                    reqddlListType.Enabled=false;
                    lblList.Visible =false;
                    ddlList.Visible=false;
                }
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lObjSubmit.Dispose();				
            }	
        }

        // This function is saves the group detail.
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            
            try
            {
                string lStrFlagDesc="";
                string lStrFlagValue="";
                string lStrActive="";
                string lStrMandatory="";
          
                lObjSubmit= new ClsUserDefinedTwo();

				//adding logic to create depenedent question
				//manab
				string lsDepQuestionText = "";
				string lsDepQuesType = "";
				string lsDepAnsType = "";
				string lsDepQuesList= "";


				if(chkDesc.Checked)
                {
                    lStrFlagDesc="Y";	// If the user has selected the Mandatory check box.
                    lStrFlagValue=Request["lstOptionList"];	// Value of those value where mandatory is required. 					
                    reqlstOptionList.Enabled=true;

					lsDepQuestionText=txtDepQuesDescription.Text;
					lsDepQuesType = ddlDepQuesType.SelectedItem.Value;
					if ( ddlDepQuesType.SelectedItem.Value == "1" || ddlDepQuesType.SelectedItem.Value == "2" || ddlDepQuesType.SelectedItem.Value == "13" || ddlDepQuesType.SelectedItem.Value == "14")
					{
						lsDepAnsType = ddlDepAnswerType.SelectedItem.Value;
						lsDepQuesList = ddlDepList.SelectedItem.Value;
					}
                }
                else
                {
                    lStrFlagDesc="N";
                    lStrFlagValue="";
                    reqlstOptionList.Enabled=false;
                }



                if(chkTotal.Checked)
                {
                    lStrTotalField="Y";	
                }
                else
                {
                    lStrTotalField="N";
                }
                // setting the value for mandatory drop down
            	
                lStrMandatory=ddlMandatory.SelectedItem.Value;				

				//manab is adding logic 
				//modified the code on 29 june 2005
				#region Logic  fo generating  style ,validtion type,max length

				StringBuilder lsStyle= new StringBuilder();
				string lsValidationType="";
				string lsMaxLength = "";
				string lsColSpan = "";
				if (txtHeight.Text != "" && txtHeight.Text.Trim() != "0")
				{
					lsStyle.Append("height:");
					lsStyle.Append(txtHeight.Text);
					lsStyle.Append(ddlHeightMsrment.SelectedItem.Value);
				}
				if (txtWidth.Text != "" && txtWidth.Text.Trim() != "0")
				{
					if (lsStyle.Length != 0)
						lsStyle.Append(";");
					
					lsStyle.Append("width:");
					lsStyle.Append(txtWidth.Text);
					lsStyle.Append(ddlWidthMsrment.SelectedItem.Value);
				}
				if (ddlQuesType.SelectedItem.Value != "4" && ddlValidation.SelectedIndex != -1 && ddlValidation.SelectedItem.Value != "")
				{
					lsValidationType = ddlValidation.SelectedItem.Value;
				}
				if (txtMaxLength.Text != "" && txtMaxLength.Text.Trim() != "0")
				{
					lsMaxLength = txtMaxLength.Text;
				}

				if (txtRepatableColumns.Text != "" && txtRepatableColumns.Text.Trim() != "0")
				{
					lsColSpan = txtRepatableColumns.Text;
				}
				#endregion
		


				if(lblTemplateID.Text=="-1") // To check if it is an insert or an update
				{
					///Insert
            		if((int.Parse(ddlQuesType.SelectedItem.Value)==8) || (int.Parse(ddlQuesType.SelectedItem.Value)==9))  // if the questionype is not single list or mulitple list.
					{
						reqddlListType.Enabled=false;	
						lStrActive="N";
						gIntReturn=lObjSubmit.fnSaveQuestion(txtQuestion.Text.Trim(),int.Parse(lblTabID.Text),lblGroupID.Text,int.Parse(ddlQuesType.SelectedItem.Value),"0","0",lStrMandatory,lStrActive,txtsuffix.Text.Trim(),txtprefix.Text.Trim(),txtQuestionNotes.Text.Trim(),lStrFlagDesc,lStrFlagValue,lStrTotalField,lsStyle.ToString(),lsValidationType,lsMaxLength,lsColSpan,int.Parse(GetUserId()),int.Parse(hidScreenID.Value),out grpID,lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);
						gIntGridQuestion = 1;
						gintLayout=int.Parse(ddlQuesType.SelectedItem.Value);
					}
					else if(int.Parse(ddlQuesType.SelectedItem.Value)==10) // if the questionype is Heading.
					{
						reqddlListType.Enabled=false;	
						lStrActive="Y";
						gIntReturn=lObjSubmit.fnSaveQuestion(txtQuestion.Text.Trim(),int.Parse(lblTabID.Text),lblGroupID.Text,int.Parse(ddlQuesType.SelectedItem.Value),"0","0",lStrMandatory,lStrActive,"","","",lStrFlagDesc,lStrFlagValue,lStrTotalField,lsStyle.ToString(),lsValidationType,lsMaxLength,lsColSpan,int.Parse(GetUserId()),int.Parse(hidScreenID.Value),out grpID,lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);
            			
					}
					else if((int.Parse(ddlQuesType.SelectedItem.Value)>2) && (int.Parse(ddlQuesType.SelectedItem.Value) != 13 && int.Parse(ddlQuesType.SelectedItem.Value) != 14) && ((int.Parse(ddlQuesType.SelectedItem.Value)!=8) || (int.Parse(ddlQuesType.SelectedItem.Value)!=9) || (int.Parse(ddlQuesType.SelectedItem.Value)!=10)))  // if the questionype is not single list or mulitple list.
					{
						lStrActive="Y";
						reqddlListType.Enabled=false;
						gIntReturn=lObjSubmit.fnSaveQuestion(txtQuestion.Text.Trim(),int.Parse(lblTabID.Text),lblGroupID.Text,int.Parse(ddlQuesType.SelectedItem.Value),"0","0",lStrMandatory,lStrActive,txtsuffix.Text.Trim(),txtprefix.Text.Trim(),txtQuestionNotes.Text.Trim(),lStrFlagDesc,lStrFlagValue,lStrTotalField,lsStyle.ToString(),lsValidationType,lsMaxLength,lsColSpan,int.Parse(GetUserId()),int.Parse(hidScreenID.Value),out grpID,lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);
					}
					else
					{
						lStrActive="Y";
						gIntReturn=lObjSubmit.fnSaveQuestion(txtQuestion.Text.Trim(),int.Parse(lblTabID.Text),lblGroupID.Text,int.Parse(ddlQuesType.SelectedItem.Value),ddlAnswerType.SelectedItem.Value,ddlList.SelectedItem.Value,lStrMandatory,lStrActive,txtsuffix.Text.Trim(),txtprefix.Text.Trim(),txtQuestionNotes.Text.Trim(),lStrFlagDesc,lStrFlagValue,lStrTotalField,lsStyle.ToString(),lsValidationType,lsMaxLength,lsColSpan,int.Parse(GetUserId()),int.Parse(hidScreenID.Value),out grpID,lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);
            			
					}
					//lIntUpdateTab=lObjSubmit.fnUpdateTabStatus(int.Parse(lblTabID.Text));	
					lblTemplateID.Text=gIntReturn.ToString();
				}
				else
				{
					///Update
            		
					if(ddlQuesType.SelectedItem.Value!="")
					{

						string ansID=ddlAnswerType.SelectedItem.Value;
						string lstID=ddlList.SelectedItem.Value;
						if((int.Parse(ddlQuesType.SelectedItem.Value)==8) || (int.Parse(ddlQuesType.SelectedItem.Value)==9))  // if the questionype is not single list or mulitple list.
						{
							ansID=ddlAnswerType.SelectedItem.Value=="" ? "-1" : ddlAnswerType.SelectedItem.Value;
							lstID=ddlList.SelectedItem.Value==""? "-1" : ddlList.SelectedItem.Value;						
							reqddlAnswerType.Enabled=false;
							reqddlListType.Enabled=false;
							gIntReturn=lObjSubmit.fnUpdateQuestion(int.Parse(lblTemplateID.Text),txtQuestion.Text.Trim(),int.Parse(lblTabID.Text),int.Parse(ddlQuesType.SelectedItem.Value),ansID,lstID,lStrMandatory,txtsuffix.Text.Trim(),txtprefix.Text.Trim(),txtQuestionNotes.Text.Trim(),lStrFlagDesc,lStrFlagValue,lStrTotalField,lsStyle.ToString(),lsValidationType,lsMaxLength,lsColSpan,int.Parse(lblGroupID.Text), int.Parse(hidScreenID.Value),int.Parse(GetUserId()),lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);
							gIntGridQuestion = 1;
							gintLayout=int.Parse(ddlQuesType.SelectedItem.Value);
						}
						else if(int.Parse(ddlQuesType.SelectedItem.Value)==10)  // if the questionype is Heading.
						{
							ansID=ddlAnswerType.SelectedItem.Value=="" ? "-1" : ddlAnswerType.SelectedItem.Value;
							lstID=ddlList.SelectedItem.Value==""? "-1" : ddlList.SelectedItem.Value;						
							reqddlAnswerType.Enabled=false;
							reqddlListType.Enabled=false;

							gIntReturn=lObjSubmit.fnUpdateQuestion(int.Parse(lblTemplateID.Text),txtQuestion.Text.Trim(),int.Parse(lblTabID.Text),int.Parse(ddlQuesType.SelectedItem.Value),ansID,lstID,lStrMandatory,txtsuffix.Text.Trim(),txtprefix.Text.Trim(),txtQuestionNotes.Text.Trim(),lStrFlagDesc,lStrFlagValue,lStrTotalField,lsStyle.ToString(),lsValidationType,lsMaxLength,lsColSpan,int.Parse(lblGroupID.Text),int.Parse(hidScreenID.Value),int.Parse(GetUserId()),lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);
						}
						else if((int.Parse(ddlQuesType.SelectedItem.Value)>2) && int.Parse(ddlQuesType.SelectedItem.Value) != 13 && int.Parse(ddlQuesType.SelectedItem.Value) != 14 && ((int.Parse(ddlQuesType.SelectedItem.Value)!=8) || (int.Parse(ddlQuesType.SelectedItem.Value)!=9) || (int.Parse(ddlQuesType.SelectedItem.Value)!=10)))  // if the questionype is not single list or mulitple list.
						{
							ansID="-1";
							lstID="-1";						
							reqddlAnswerType.Enabled=false;
							reqddlListType.Enabled=false;
						}
						gIntReturn=lObjSubmit.fnUpdateQuestion(int.Parse(lblTemplateID.Text),txtQuestion.Text.Trim(),int.Parse(lblTabID.Text),int.Parse(ddlQuesType.SelectedItem.Value),ansID,lstID,lStrMandatory,txtsuffix.Text.Trim(),txtprefix.Text.Trim(),txtQuestionNotes.Text.Trim(),lStrFlagDesc,lStrFlagValue,lStrTotalField,lsStyle.ToString(),lsValidationType,lsMaxLength,lsColSpan,int.Parse(lblGroupID.Text),int.Parse(hidScreenID.Value),int.Parse(GetUserId()),lsDepQuestionText,lsDepQuesType,lsDepAnsType,lsDepQuesList);
					}

					grpID=int.Parse(lblGroupID.Text);							
				}
                
                if(ddlAnswerType.SelectedItem.Value=="12") // Storing the value to make the add/edit list link if the Answertype is User Defined List
                {
                    gstrType="U";
                }

                if(gIntReturn==-1)
                {				
                    lblError.Text=lblerrmsg.Text;
                    lblError.Visible=true;
                }
                else if(gIntReturn==0)
                {				
                    lblError.Text=lblerrmsg.Text;
                    lblError.Visible=true;
                }
                else if(gIntReturn>0)
                {
                    lStrScreenID = lblBackID.Text;
					hidScreenID.Value = lStrScreenID;
                    lStrTabID=lblTabID.Text;
					hidTabID.Value = lStrTabID;
                    pnlMessage.Visible=true;
                    lblError.Text=lblinsert.Text;	// Transferring the message from resource file to label.
                    lblError.Visible=true;
					lblGroupID.Text = grpID.ToString();
                    lStrGroupID=lblGroupID.Text;
					hidGroupID.Value = lStrGroupID;
                    gIntInsertUpdateFlag=1;
                    gIntReturn=int.Parse(lblTemplateID.Text);
                    txtDeactivateVal.Text=lStrActive;
                    btnActivateDeactivate.Visible =true;
                    btnActivateDeactivate.Text =  aobjResMang.GetString("lStrDeActivate");
                }
                /* else if(lIntsaveStatus==3)
                {
                    lblError.Text=lblupdate.Text;
                    lblError.Visible=true;
                }*/
     
            }
            catch(Exception ex)
            {
				lblerrmsg.Text = ex.Message;
				lblerrmsg.Visible = true;

            }
            finally
            {
                lObjSubmit.Dispose();				
            }	
        }
        
        // This function will be invoked whenever the user activates/deactivates the question.
        private void btnDeactiveGroup_Click(object sender, System.EventArgs e)
        {
				
            try
            {
                string lStrIsActive;
                lObjSubmit= new ClsUserDefinedTwo();
                int lIntQuestionId=int.Parse(lblTemplateID.Text);		
                lStrIsActive=txtDeactivateVal.Text.Trim().ToUpper();
				if (lStrIsActive.Equals("Y"))
                {
                    lStrIsActive=txtDeactivateVal.Text="N";
                    gIntReturn=lObjSubmit.fnDeactivateQuestion(lIntQuestionId,lStrIsActive,int.Parse(hidScreenID.Value),int.Parse(hidTabID.Value),int.Parse(hidGroupID.Value),int.Parse(GetUserId()));										
                    btnActivateDeactivate.Text= aobjResMang.GetString("lStrActivate");
                    pnlMessage.Visible=true;
                    lblError.Visible=true;
                    lblError.Text=aobjResMang.GetString("ScrDeactiveMsg");
                }
                else if (lStrIsActive.Equals("N"))
                {
                    lStrIsActive=txtDeactivateVal.Text="Y";
                    gIntReturn=lObjSubmit.fnDeactivateQuestion(lIntQuestionId,lStrIsActive,int.Parse(hidScreenID.Value),int.Parse(hidTabID.Value),int.Parse(hidGroupID.Value),int.Parse(GetUserId()));
                    btnActivateDeactivate.Text= aobjResMang.GetString("lStrDeActivate");
                    pnlMessage.Visible=true;
                    lblError.Visible=true;
                    lblError.Text=aobjResMang.GetString("ScrActiveMsg");
                }
				grpID=int.Parse(lblGroupID.Text);
				gIntReturn=int.Parse(lblTemplateID.Text);
				lStrScreenID = lblBackID.Text;
				lStrTabID=lblTabID.Text;
                gIntInsertUpdateFlag=1;				
               
            }
            catch(Exception ex)
            {
              throw ex;
            }
            finally
            {
                lObjSubmit.Dispose();			
            }
        }
    
      
        /* This fucntion is hides the form controls based on the selection made by the user.
        */
        public void fnHideControls()
        {
            try
            {
                pnlDdlAnsType.Visible=false;
                pnlChkDescription.Visible=false;
                pnlDdlDescription.Visible=false;
                pnlDdlList.Visible=false;
                pnlCommonQuestion.Visible=false;	
                lblAnsType.Visible=false;
                ddlAnswerType.Visible=false;
                lblList.Visible=false;
                ddlList.Visible=false;			
                lblIsMandatory.Visible=false;
                ddlMandatory.Visible=false;
                lblSuffix.Visible=false;
                txtsuffix.Visible=false;
                lblPrefix.Visible=false;
                txtprefix.Visible=false;
                lblQuestionNotes.Visible=false;
                txtQuestionNotes.Visible=false;
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
				
            }	
			
        }
        /* This fucntion is displays the form controls based on the selection made by the user.
          */
        public void fnShowControls()
        {
            try
            {
                lblQuestionType.Visible=true;
                ddlQuesType.Visible=true;
                lblAnsType.Visible=true;
                ddlAnswerType.Visible=true;
                lblList.Visible=true;
                ddlList.Visible=true;			
                lblIsMandatory.Visible=true;
                ddlMandatory.Visible=true;
                lblSuffix.Visible=true;
                txtsuffix.Visible=true;
                lblPrefix.Visible=true;
                txtprefix.Visible=true;
                lblQuestionNotes.Visible=true;
                txtQuestionNotes.Visible=true;		
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
				
            }	
        }
        /* This fucntion is hides the form controls based on the selection of question.
          */

        // This function is invoked whenever the list for the single select item is changed.
        private void ddlList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                lObjSubmit= new ClsUserDefinedTwo();
                if(ddlAnswerType.SelectedItem.Value=="11")		// Checking if the List Dropdown should be populated on System Based List
                {
                    gstrType="S";
                }
                else if(ddlAnswerType.SelectedItem.Value=="12") // Checking if the List Dropdown should be populated on User Defined List
                {
                    gstrType="U";
                }
                else if(ddlAnswerType.SelectedItem.Value=="14")  // Checking if the List Dropdown should be populated on Direct List
                {
                    gstrType="D";
                }
                if(ddlQuesType.SelectedItem.Value=="1") 
                {	
                    pnlChkDescription.Visible=true;
                    lblDescRequired.Visible=true;
                    chkDesc.Visible=true;
                    chkDesc.Checked=false;
                }
                else
                {
                    pnlChkDescription.Visible=false;
                    lblDescRequired.Visible=false;
                    chkDesc.Visible=false;
                }
                if(chkDesc.Checked && ddlList.SelectedItem.Value.Trim()!="")
                {
                    lblSpecify.Visible=true;
                    lstOptionList.Visible=true;
                    DataSet dsOptionList = new DataSet();
					
                    lstOptionList.DataSource = new DataView(lObjSubmit.fnOptionList(ddlList.SelectedItem.Value.ToString()).Tables[0]);													
                    lstOptionList.DataTextField="OPTIONNAME";
                    lstOptionList.DataValueField="OPTIONID";			
                    lstOptionList.DataBind();	
                    lstOptionList.Items.Insert(0, new ListItem(reqlstOptionList.InitialValue, ""));				
                }
                else
                {
                    pnlDdlDescription.Visible=false;
                }
				
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lObjSubmit.Dispose();				
            }
        }

		private void ddlDepAnswerType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
			try
			{	
				lObjSubmit= new ClsUserDefinedTwo();		//Initializing the object  
				if(ddlDepAnswerType.SelectedItem.Value=="11")		// Checking if the List Dropdown should be populated on System Based List
				{
					gstrType="S";
				}
				else if(ddlDepAnswerType.SelectedItem.Value=="12") // Checking if the List Dropdown should be populated on User Defined List
				{
					gstrType="U";
				}
				else if(ddlDepAnswerType.SelectedItem.Value=="14")  // Checking if the List Dropdown should be populated on Direct List
				{
					gstrType="D";
				}
				lblDepList.Visible=true;
				ddlDepList.Visible = true;
				reqDepList.Enabled=true;

				if(ddlDepAnswerType.SelectedItem.Value != "")
				{				
								
					ddlDepList.DataSource = new DataView(lObjSubmit.fnList(gstrType).Tables[0]);
					ddlDepList.DataTextField="NAME";
					ddlDepList.DataValueField="LISTID";			
					ddlDepList.DataBind();							
					ddlDepList.Items.Insert(0, new ListItem(reqddlListType.InitialValue, ""));				
					ddlDepList.SelectedIndex = 0;
				}
				else
				{
					lblDepList.Visible=true;
					ddlDepList.Visible = true;
					reqDepList.Enabled=false;
				}
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lObjSubmit.Dispose();				
			}	
		}

		private void ddlDepQuesType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
			if (ddlDepQuesType.SelectedItem.Value == "1" || ddlDepQuesType.SelectedItem.Value == "2" || ddlDepQuesType.SelectedItem.Value == "13" || ddlDepQuesType.SelectedItem.Value == "14")
			{
				//for dependent question
				ddlDepAnswerType.Items.Clear();
				ddlDepAnswerType.DataSource = lObjSubmit.fnGetQuestionType(2);
				ddlDepAnswerType.DataTextField="QUESTIONTYPENAME";
				ddlDepAnswerType.DataValueField="QUESTIONTYPEID";			
				ddlDepAnswerType.DataBind();							
				ddlDepAnswerType.Items.Insert(0, new ListItem("", ""));				
				ddlDepAnswerType.SelectedIndex = 0;
					
					
				lblDepList.Visible=false;
				ddlDepList.Visible=false;
				reqDepList.Enabled=false;

			}
		}

		private void InitializeScreenDetails()
		{
			int lIntAnswerTypeID=0;
			int lIntQListID=0;
			int lIntQuestionTypeID=0;
			string lStrFlagDesc="";				
			string lStrFlagValue="";	
			gstrType="";
			//string lStrResourceName="";
			string lStrInitialText="";	

			if(Request["QID"]!=null)
				lStrTemplateID = Request["QID"];
			else
				lStrTemplateID = "-1";

			hidQid.Value = lStrTemplateID;

			lblTemplateID.Text = lStrTemplateID;
			lStrTabID= Request["TabID"];
			lblTabID.Text=lStrTabID;					
			hidTabID.Value=lStrTabID;
			lStrScreenID = Request["ScreenID"];							
			hidScreenID.Value= lStrScreenID; 
			lStrGroupID= Request["GroupID"];	
			lblGroupID.Text = lStrGroupID;
			hidGroupID.Value = lStrGroupID;
			lblBackID.Text = lStrScreenID;										
					 
			if(lblTemplateID.Text.Trim()!="")	
			{						
				reqddlQuesType.InitialValue=aobjResMang.GetString("reqQuestionTypeInitialMsg");	 // Initial text for Question Type Required validator
				lStrInitialText=aobjResMang.GetString("StrGroupInitialText");					 // Initial text for group dropdown					
				reqddlAnswerType.Text=aobjResMang.GetString("reqddlAnswerTypeText");						
				FillCombo();
				if(lblTemplateID.Text!="-1" && lStrTemplateID!="") // If the user has clicked on the grid to view/edit the detail.
				{
					//btnActivateDeactivate.Visible=true;

					DataRow lDRquestion = lObjSubmit.fnGetQuesData(int.Parse(lStrTemplateID),int.Parse(hidScreenID.Value),int.Parse(lblTabID.Text),int.Parse(lblGroupID.Text==""?"-1":lblGroupID.Text));

							
					if(lDRquestion["QDESC"].ToString() != "-1" && lDRquestion["QDESC"].ToString() != "")
					{							
						txtQuestion.Text =lDRquestion["QDESC"].ToString();
					}
					if(lDRquestion["ISACTIVE"].ToString() != "-1" && lDRquestion["QDESC"].ToString() != "")
					{	
						txtDeactivateVal.Text=lDRquestion["ISACTIVE"].ToString();
					}
					if (txtDeactivateVal.Text.Trim().ToUpper().Equals("Y"))
					{
						btnActivateDeactivate.Text =  aobjResMang.GetString("lStrActivate");
					}
					else
					{
						btnActivateDeactivate.Text =  aobjResMang.GetString("lStrDeActivate");
					}	

							
					lblTabID.Text=lDRquestion["TABID"].ToString();
								
					if(lDRquestion["SUFFIX"].ToString() != "-1" && lDRquestion["SUFFIX"].ToString() != "")
					{							
						txtsuffix.Text=lDRquestion["SUFFIX"].ToString();
					}
								
					if(lDRquestion["PREFIX"].ToString() != "-1" && lDRquestion["PREFIX"].ToString() != "")
					{							
						txtprefix.Text=lDRquestion["PREFIX"].ToString();						
					}

					if(lDRquestion["QNOTES"].ToString() != "-1" && lDRquestion["QNOTES"].ToString() != "")
					{							
						txtQuestionNotes.Text=lDRquestion["QNOTES"].ToString();
					}


					//manab changed the code as per the modification in screen
					if(lDRquestion["STYLE"] != DBNull.Value && lDRquestion["STYLE"].ToString() != "")
					{
						string lsStyle = lDRquestion["STYLE"].ToString();
						string[] stylesheet = lsStyle.Split(';');
						foreach(string lsStyleCom in stylesheet)
						{
							string[] styleParam =  lsStyleCom.Split(':');
							string lsMsrment = "0";
							string lsUnit = "";

							if (styleParam.Length > 0)
							{
								if (styleParam[1].IndexOf("%") != -1)
								{
									lsMsrment = styleParam[1].Substring(0,styleParam[1].IndexOf("%"));
									lsUnit = "%";
								}
								else if (styleParam[1].ToLower().IndexOf("px") != -1)
								{
									lsMsrment = styleParam[1].Substring(0,styleParam[1].ToLower().IndexOf("px"));
									lsUnit = "Px";
								}
								else if (styleParam[1].ToLower().IndexOf("pt") != -1)
								{
									lsMsrment = styleParam[1].Substring(0,styleParam[1].ToLower().IndexOf("pt"));
									lsUnit = "Pt";
								}
							}


							if (styleParam[0].Trim().ToLower() == "height")
							{
								txtHeight.Text = lsMsrment;
								if (ddlHeightMsrment.Items.FindByValue(lsUnit) != null)
								{
									ddlHeightMsrment.Items.FindByValue(lsUnit) .Selected=true;
								}
										
							}
							if (styleParam[0].Trim().ToLower() == "width")
							{
								txtWidth.Text = lsMsrment;
								if (ddlWidthMsrment.Items.FindByValue(lsUnit) != null)
								{
									ddlWidthMsrment.Items.FindByValue(lsUnit).Selected=true;
								}
							}
						}
					}

					if(lDRquestion["VALIDATIONTYPE"] != DBNull.Value && lDRquestion["VALIDATIONTYPE"].ToString() != "")
					{
						string lsValType = lDRquestion["VALIDATIONTYPE"].ToString();
						if (ddlValidation.Items.FindByValue(lsValType) != null)
						{
							ddlValidation.SelectedIndex=-1;
							ddlValidation.Items.FindByValue(lsValType).Selected=true;
						}
					}
					if(lDRquestion["MAXLENGTH"] != DBNull.Value && lDRquestion["MAXLENGTH"].ToString() != "")
					{
						txtMaxLength.Text = lDRquestion["MAXLENGTH"].ToString();
					}
					if(lDRquestion["COLSPAN"] != DBNull.Value && lDRquestion["COLSPAN"].ToString() != "")
					{
						txtRepatableColumns.Text = lDRquestion["COLSPAN"].ToString();
					}
					//**************************************************************


								
					string lsDepQuestionText = "";
					string lsDepQuesType = "";
					string lsDepAnsType = "";
					string lsDepQuesList= "";
 
					if(lDRquestion["QUESTIONTYPEID"].ToString() != "-1" && lDRquestion["QUESTIONTYPEID"].ToString() != "")
					{							
						lIntQuestionTypeID=int.Parse(lDRquestion["QUESTIONTYPEID"].ToString());
								
						if(lIntQuestionTypeID==1)
						{
							pnlChkDescription.Visible=true;
							lblDescRequired.Visible=true;
							chkDesc.Visible=true;
									
							if (lDRquestion["DEPQUESTIONTEXT"] != DBNull.Value && lDRquestion["DEPQUESTIONTEXT"] != null)
								lsDepQuestionText = lDRquestion["DEPQUESTIONTEXT"].ToString();

							if (lDRquestion["DEPQUESTYPE"] != DBNull.Value && lDRquestion["DEPQUESTYPE"] != null)
								lsDepQuesType = lDRquestion["DEPQUESTYPE"].ToString();
									
							if (lDRquestion["DEPANSTYPE"] != DBNull.Value && lDRquestion["DEPANSTYPE"] != null)
								lsDepAnsType = lDRquestion["DEPANSTYPE"].ToString();

							if (lDRquestion["DEPQUESTIONLIST"] != DBNull.Value && lDRquestion["DEPQUESTIONLIST"] != null)
								lsDepQuesList = lDRquestion["DEPQUESTIONLIST"].ToString();

							txtDepQuesDescription.Text = lsDepQuestionText;
							if (ddlDepQuesType.Items.FindByValue(lsDepQuesType) != null)
							{
								ddlDepQuesType.Items.FindByValue(lsDepQuesType).Selected=true;
							}
							if (ddlDepQuesType.SelectedItem.Value == "1" || ddlDepQuesType.SelectedItem.Value == "2" || ddlDepQuesType.SelectedItem.Value == "13" || ddlDepQuesType.SelectedItem.Value == "14")
							{
								ddlDepAnswerType.Items.Clear();
								ddlDepAnswerType.DataSource = lObjSubmit.fnGetQuestionType(2);
								ddlDepAnswerType.DataTextField="QUESTIONTYPENAME";
								ddlDepAnswerType.DataValueField="QUESTIONTYPEID";			
								ddlDepAnswerType.DataBind();							
								ddlDepAnswerType.Items.Insert(0, new ListItem("", ""));				
								ddlDepAnswerType.SelectedIndex = 0;
								ddlDepAnswerType.Visible=true;

								ddlDepList.DataSource = new DataView(lObjSubmit.fnList(gstrType).Tables[0]);
								ddlDepList.DataTextField="NAME";
								ddlDepList.DataValueField="LISTID";			
								ddlDepList.DataBind();							
								ddlDepList.Items.Insert(0, new ListItem(reqddlListType.InitialValue, ""));				
								ddlDepList.SelectedIndex = 0;
								lblDepList.Visible=true;
								ddlDepList.Visible=true;


								if (ddlDepAnswerType.Items.FindByValue(lsDepAnsType) != null)
								{
									ddlDepAnswerType.SelectedIndex=-1;
									ddlDepAnswerType.Items.FindByValue(lsDepAnsType).Selected=true;
								}
								if (ddlDepList.Items.FindByValue(lsDepQuesList) != null)
								{
									ddlDepList.SelectedIndex=-1;
									ddlDepList.Items.FindByValue(lsDepQuesList).Selected=true;
								}

							}
									

						}
						else
						{
							pnlChkDescription.Visible=false;
							lblDescRequired.Visible=false;
							chkDesc.Visible=false;
						}

						if(lDRquestion["FLGQUESDESC"].ToString() != "-1" && lDRquestion["FLGQUESDESC"].ToString() != "")
						{
							lStrFlagDesc=lDRquestion["FLGQUESDESC"].ToString();
							if(lStrFlagDesc=="Y")
							{
								chkDesc.Checked=true;
								pnlDdlDescription.Visible=true;
							}
						}
						lStrTotalField = lDRquestion["REQTOTAL"].ToString();
						if(lStrTotalField=="Y")
							chkTotal.Checked=true;

						if(lDRquestion["FLGCOMPVALUE"].ToString() != "-1" && lDRquestion["FLGCOMPVALUE"].ToString() != "")
						{
							lStrFlagValue=lDRquestion["FLGCOMPVALUE"].ToString();
						}


						if((lIntQuestionTypeID==1) || (lIntQuestionTypeID==2) || (lIntQuestionTypeID==13) || (lIntQuestionTypeID==14))	// if question type is either single or multiple list
						{
							pnlDdlAnsType.Visible=true;
							lblAnsType.Visible=true;
							ddlAnswerType.Visible=true;								
							ddlAnswerType.Items.Clear();
							ddlAnswerType.DataSource = dsAnswerType;
							ddlAnswerType.DataTextField="QUESTIONTYPENAME";
							ddlAnswerType.DataValueField="QUESTIONTYPEID";			
							ddlAnswerType.Items.Insert(0, new ListItem("", ""));			
							ddlAnswerType.SelectedIndex = 0;
							ddlAnswerType.DataBind();							
						}
						if(lIntQuestionTypeID==10)	// if question type is Heading
						{
							fnHideControls();									
						}
						if((lIntQuestionTypeID==8) || (lIntQuestionTypeID==9))	// if question type is either HGrid or VGrid
						{
							fnHideControls();
							//if(lIntQuestionTypeID==8)
							//  pntTotalField.Visible=true;
							btnSave.Text= aobjResMang.GetString("btnGridText");	
						}
					}
								
					if(lDRquestion["ANSWERTYPEID"].ToString() != "-1" && lDRquestion["ANSWERTYPEID"].ToString() != "")
					{
						lIntAnswerTypeID=int.Parse(lDRquestion["ANSWERTYPEID"].ToString());
						if(lIntAnswerTypeID==11)	// For System List
						{
							gstrType="S";	// Storing the value which will be used in the aspx page.
						}
						else if(lIntAnswerTypeID==12)	// For User Defined List
						{
							gstrType="U";
                                    
						}
						else if(lIntAnswerTypeID==14)	// For Direct List
						{
							gstrType="D";
						}
						pnlDdlList.Visible=true;
					}
					if(lDRquestion["QUESTIONLISTID"].ToString() != "-1" && lDRquestion["QUESTIONLISTID"].ToString() != "")
					{
						lIntQListID=int.Parse(lDRquestion["QUESTIONLISTID"].ToString());							
						lstOptionList.DataSource = new DataView(lObjSubmit.fnOptionList(lIntQListID.ToString()).Tables[0]);													
						lstOptionList.DataTextField="OPTIONNAME";
						lstOptionList.DataValueField="OPTIONID";			
						lstOptionList.DataBind();	
						lstOptionList.Items.Insert(0, new ListItem(reqlstOptionList.InitialValue, ""));				
								
						if(lStrFlagValue.Trim()!="")
						{
							string [] strarrCommaValue = lStrFlagValue.Split(new char[]{','});
							for(int lintCommaCounter=0;lintCommaCounter <= strarrCommaValue.Length -1;lintCommaCounter++)
							{
								for(int k=0;k<=lstOptionList.Items.Count - 1;k++)	// To populate List Type
								{
									//if(lstOptionList.Items[k].Value.ToString() == strarrCommaValue[lintCommaCounter].ToString())
									if(strarrCommaValue[lintCommaCounter].ToString()==lstOptionList.Items[k].Value.ToString())
									{
										lstOptionList.Items[k].Selected=true;
										break;
									}
								}
							}
						}
						lblSpecify.Visible=true;
						lstOptionList.Visible=true;
					}													
					if(lDRquestion["ISREQ"].ToString()!="")
					{
						lStrRequired=lDRquestion["ISREQ"].ToString();
					}
								
					int j=0;
						
					if(lStrRequired=="Y")	// For IsRequired Dropdown
					{
						ddlMandatory.SelectedIndex=1;
					}							
					else
					{
						ddlMandatory.SelectedIndex=0;
					}

							
				                
					for(j=0;j<=ddlQuesType.Items.Count - 1;j++)	// To populate QuestionType 
					{
						if(ddlQuesType.Items[j].Value.ToString() == lIntQuestionTypeID.ToString())
						{
							ddlQuesType.SelectedIndex = j;
							break;
						}
					}
				                
					for(j=0;j<=ddlAnswerType.Items.Count - 1;j++)	// To populate AnswerType
					{
						if(ddlAnswerType.Items[j].Value.ToString() == lIntAnswerTypeID.ToString())
						{
							ddlAnswerType.SelectedIndex = j;
							break;
						}
					}
					if((lIntAnswerTypeID==12) ||(lIntAnswerTypeID==11))
					{
						lblList.Visible=true;
						ddlList.Visible=true;
					}
					for(j=0;j<=ddlList.Items.Count - 1;j++)	// To populate List Type
					{
						if(ddlList.Items[j].Value.ToString() == lIntQListID.ToString())
						{
							ddlList.SelectedIndex = j;
							break;
						}
					}

					for(j=0;j<=lstOptionList.Items.Count - 1;j++)	// To populate List Type
					{
						if(lstOptionList.Items[j].Value.ToString() == lStrFlagValue.ToString())
						{
							lstOptionList.SelectedIndex = j;
							break;
						}
					}	// End For 


				}	// End for Reading values from DB
				else
				{
					btnActivateDeactivate.Visible=false;
				}

			}	//	End For Add New

			//manab adding the dependant question logic
		}

		public void btnReset_ServerClick(object sender, System.EventArgs e)
		{
			//InitializeScreenDetails();
			Response.Redirect(Request.Url.ToString());
		}
    }
}
