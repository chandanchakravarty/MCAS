/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 15/06/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement option detail page for the user defined module
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
	/// Summary description for PostGridQuestion.
	/// </summary>
	public class PostGridQuestion : Cms.CmsWeb.cmsbase  
	{
        protected System.Web.UI.WebControls.TextBox txtQuestion;		
        protected System.Web.UI.WebControls.DropDownList ddlGroupid;
        protected System.Web.UI.WebControls.DropDownList ddlQuesType,ddllayout;
        protected System.Web.UI.WebControls.DropDownList ddlAnswerType,ddlList;
        protected System.Web.UI.WebControls.DataGrid DataGrid1;
        //protected System.Web.UI.HtmlControls.HtmlInputButton btnCancel;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnReset;	
        protected System.Web.UI.WebControls.DropDownList ddlMandatory;
        protected System.Web.UI.WebControls.DropDownList ddlActive;		
        protected System.Web.UI.WebControls.Button btnSave;		
        protected System.Web.UI.WebControls.Label lblAnsType,lblQuestion,lblList;		
        protected System.Web.UI.WebControls.Label lblQuestionType,lblIsMandatory;
        protected System.Web.UI.WebControls.Label lblIsActive,lblLayout;
        protected System.Web.UI.WebControls.Label lblTemplateID,lblTabID,lblQID,lblBackID,lblgroup;
        protected System.Web.UI.WebControls.Label lblTemplate,lblGridID;
        protected System.Web.UI.WebControls.Label lblupdate,lblerrmsg,lblinsert,lblGridType;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqtxtQuestion;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqddlQuesType,reqddlAnswerType;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqddlListType,reqddlLayout;		
        protected System.Web.UI.WebControls.CustomValidator CVtxtQuestionDesc;
        protected System.Web.UI.WebControls.Label lblError;
        protected System.Web.UI.WebControls.Panel pnlMessage,pnlAnsType,pnlDdlList,pnlLayout,pnlQuesType;
        protected string gStrReqQuestion;		
        protected string gStrReqQuestype;
        protected string gStrReqAnstype;
        protected string lStrTemplateID;
        protected string lStrQID;
        protected string lStrTabID;
        protected string lStrScreenID;
        protected string lStrGroup;
        protected string lStrGroupID;
        protected string GridLayoutID;
        protected int gIntTabID,gIntQID;
        protected int gIntInsertUpdateFlag;
        protected int gIntReturn;
        protected string gStrRequired="";
        protected string gStrActive="";
        protected int lIntGroupID=0;	
        protected string lStrlayout;
        public string gStrType;
        protected string gStrCarrierID="";
        protected string gStrStyle,cssFolder;
        protected int lintQuestionType=0;
        protected string gStrAddLink1="";
        protected string gStrAddLink2="";
        protected string gStrAddLink3="";
        protected string gStrViewLink1="";
        protected string gStrViewLink2="";
        protected string gStrViewLink3="";
        protected string gStrViewMsg="";
        public string gStrSecure="";
        protected string  gStrSaveMsgText,gStrMandatoryMsgText,gStrTabClickMsgText,gStrTitleMsgText;
        protected string gStrExists="";
        private ClsUserDefinedTwo lObjSubmit;
        protected string gStrdrpmanone="";
        protected string gStrdrpmantwo="";
        protected string gStrCalledFrom="";
        
        protected System.Web.UI.WebControls.Panel Panel1;
        protected int gIntScreenID=26;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidScreenID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidGroupID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQid;
        protected System.Web.UI.WebControls.Label lblGroupID;
        private System.Resources.ResourceManager aobjResMang;
        private DataSet dsQuestionType;

         private DataSet dsAnswerType;
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

            ddllayout.Items.Insert(0, new ListItem(reqddlLayout.InitialValue, reqddlLayout.InitialValue));			
            ddllayout.SelectedIndex = 0;
        }


        private void SetCaptions()
        {
            aobjResMang=new System.Resources.ResourceManager("Cms.CmsWeb.UserDefined.PostGridQuestion" ,System.Reflection.Assembly.GetExecutingAssembly()); 
            lblQuestion.Text = aobjResMang.GetString("lblQuestionText");		// Populating the labels.
						
            lblQuestionType.Text = aobjResMang.GetString("lblQuestionTypeText");
            lblAnsType.Text = aobjResMang.GetString("lblAnsTypeText");
            lblList.Text= aobjResMang.GetString("lblListTypeText");
            lblIsMandatory.Text = aobjResMang.GetString("lblIsMandatoryText");
            //lblIsActive.Text = aobjResMang.GetString("lblIsActiveText");					
            btnSave.Text= aobjResMang.GetString("btnSaveText");	
            //btnCancel.Value= aobjResMang.GetString("btnCancelText");	
            lblLayout.Text=aobjResMang.GetString("lblLayout");	
            reqtxtQuestion.ErrorMessage=aobjResMang.GetString("reqtxtQuestionText");						
            reqddlQuesType.ErrorMessage=aobjResMang.GetString("reqddlQuesTypeText");
            reqddlAnswerType.ErrorMessage=aobjResMang.GetString("reqddlAnswerTypeText");				
            reqddlListType.ErrorMessage=aobjResMang.GetString("reqddlListTypeText");
            lblupdate.Text=aobjResMang.GetString("strMsgEdit");	
            lblerrmsg.Text=aobjResMang.GetString("strErrorSave");
            lblinsert.Text=aobjResMang.GetString("strMsgInsert");
            reqddlLayout.Text=aobjResMang.GetString("reqtxtLayoutText");
            reqddlLayout.InitialValue=aobjResMang.GetString("reqddlLayoutInitialmsg");
            gStrAddLink1=aobjResMang.GetString("AddNewLinkText1");
            gStrAddLink2=aobjResMang.GetString("AddNewLinkText2");
            gStrAddLink3=aobjResMang.GetString("AddNewLinkText3");
            gStrViewLink1=aobjResMang.GetString("ViewLinkText1");
            gStrViewLink2=aobjResMang.GetString("ViewLinkText2");
            gStrViewLink3=aobjResMang.GetString("ViewLinkText3");	
            gStrViewMsg=aobjResMang.GetString("MsgViewText");
            reqddlQuesType.InitialValue=aobjResMang.GetString("reqQuestionTypeInitialMsg");	 // Initial text for Question Type Required validator
            gStrSaveMsgText=aobjResMang.GetString("lblSaveMessage");								
            gStrTitleMsgText=aobjResMang.GetString("strScreendetails");
            gStrdrpmanone=aobjResMang.GetString("drpdownmandatoryone");
            gStrdrpmantwo=aobjResMang.GetString("drpdownmandatorytwo");
            btnReset.Value=aobjResMang.GetString("btnReset");
            CVtxtQuestionDesc.Text=aobjResMang.GetString("chkquestiondesc");
        }
        private void Page_Load(object sender, System.EventArgs e)
		{
            btnReset.Attributes.Add("onclick","javascript:ResetScreenForm();");

            lObjSubmit= new ClsUserDefinedTwo();	//Initializing the object to populate dropdownlist for group,question type and answer type.	
            DataSet dsQType = new DataSet();			
            try
            {
              
                
                int lIntAnswerTypeID=0;
                int lIntQListID=0;
                int lIntQuestionTypeID=0;
                gStrType="";
				
                //string lStrResourceName="";
             
                SetCaptions();
                if(!IsPostBack)
                {
                    if(Request["QGRIDOPTIONID"]!=null)
                        lStrTemplateID = Request["QGRIDOPTIONID"];
                    else
                        lStrTemplateID = "-1";

                    hidQid.Value = Request["QID"];

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
                   
                    int grpid=-1;
                    if(lblTemplateID.Text.Trim()!="")
                    {
                        dsQType = lObjSubmit.fnGetQuestionTypeID(hidQid.Value,int.Parse(hidGroupID.Value),int.Parse(hidTabID.Value),int.Parse(hidScreenID.Value));
                        if(dsQType!=null)
                        if(dsQType.Tables[0].Rows.Count > 0)
                        {
                            lintQuestionType = int.Parse(dsQType.Tables[0].Rows[0].ItemArray[0].ToString());
                            grpid=int.Parse(dsQType.Tables[0].Rows[0].ItemArray[1].ToString());
                            if(lintQuestionType==9)	// For Horizontal Grid
                            {
                                pnlLayout.Visible=false;
                                lblLayout.Visible=false;
                                ddllayout.Visible=false;
                                lblGridType.Text="9";
                            }
                            else	// For Vertical Grid
                            {
                                lblIsMandatory.Visible=false;
                                ddlMandatory.Visible=false;
                                pnlLayout.Visible=true;						
                            }
                        }		
						
                        FillCombo();

                        //if(lblTemplateID.Text!="-1") // If the user has clicked on the grid t view/edit the detail.
                        if(lblTemplateID.Text!="-1")
                        {
                            DataRow lDRquestion = lObjSubmit.fnGetQuesOptionData(int.Parse(hidQid.Value),int.Parse(lStrTemplateID));

							
                            if(lDRquestion["OPTIONTEXT"].ToString() != "-1" && lDRquestion["OPTIONTEXT"].ToString() != "")
                            {							
                                txtQuestion.Text =lDRquestion["OPTIONTEXT"].ToString();
                            }
							
                            lStrlayout=lDRquestion["GRIDOPTIONLAYOUT"].ToString();
                            if(lStrlayout=="V")
                            {
                                pnlQuesType.Visible=false;
                                lblQuestionType.Visible=false;
                                ddlQuesType.Visible=false;
                                reqddlQuesType.Enabled=false;
                                reqddlAnswerType.Enabled=false;
                                reqddlListType.Enabled=false;
                                lblIsMandatory.Visible=false;
                                ddlMandatory.Visible=false;
                            }
                            if(lDRquestion["OPTIONTYPEID"].ToString() != "-1" && lDRquestion["OPTIONTYPEID"].ToString() != "")
                            {							
                                lIntQuestionTypeID=int.Parse(lDRquestion["OPTIONTYPEID"].ToString());							
                                if((lIntQuestionTypeID==1) || (lIntQuestionTypeID==2))	// if question type is either single or multiple list
                                {
                                    pnlAnsType.Visible=true;
                                    lblAnsType.Visible=true;
                                    ddlAnswerType.Visible=true;								
                                    ddlAnswerType.Items.Clear();
                                    ddlAnswerType.DataSource = new DataView(lObjSubmit.fnGetQuestionType(2).Tables[0]);
                                    ddlAnswerType.DataTextField="QUESTIONTYPENAME";
                                    ddlAnswerType.DataValueField="QUESTIONTYPEID";			
                                    ddlAnswerType.Items.Insert(0, new ListItem("", ""));			
                                    ddlAnswerType.SelectedIndex = 0;
                                    ddlAnswerType.DataBind();							
                                }
                            }
							
                            if(lDRquestion["ANSWERTYPEID"].ToString() != "-1" && lDRquestion["ANSWERTYPEID"].ToString() != "")
                            {
                                lIntAnswerTypeID=int.Parse(lDRquestion["ANSWERTYPEID"].ToString());
                                if(lIntAnswerTypeID==11)	// For System List
                                {
                                    gStrType="S";	// Storing the value which will be used in the aspx page.
                                }
                                else if(lIntAnswerTypeID==12)	// For User Defined List
                                {
                                    gStrType="U";
                                }
                                else if(lIntAnswerTypeID==14)	// For Direct List
                                {
                                    gStrType="D";
                                }
                            }
                            if(lDRquestion["LISTID"].ToString() != "-1" && lDRquestion["LISTID"].ToString() != "")
                            {
                                lIntQListID=int.Parse(lDRquestion["LISTID"].ToString());							
                            }					

                            if(lStrlayout=="H")	// For IsActive Dropdown
                            {
                                ddllayout.SelectedIndex=1;
                                pnlQuesType.Visible=true;
                                if(lDRquestion["ISREQUIRED"].ToString()!="")
                                {
                                    gStrRequired=lDRquestion["ISREQUIRED"].ToString();
                                }
                                if(gStrRequired=="Y")	// For IsRequired Dropdown
                                {
                                    ddlMandatory.SelectedIndex=1;
                                }							
                                else
                                {
                                    ddlMandatory.SelectedIndex=0;
                                }	
                            }
                            else
                            {
                                pnlQuesType.Visible=false;
                                ddllayout.SelectedIndex=2;
                            }
							
                            int j=0;

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
                            if((lIntAnswerTypeID==12) || (lIntAnswerTypeID==11))
                            {
                                lblList.Visible=true;
                                ddlList.Visible=true;
                                pnlDdlList.Visible=true;
                            }
                            for(j=0;j<=ddlList.Items.Count - 1;j++)	// To populate List Type
                            {
                                if(ddlList.Items[j].Value.ToString() == lIntQListID.ToString())
                                {
                                    ddlList.SelectedIndex = j;
                                    break;
                                }
                            }	// End of For Loop
                        }		// End of Edit Mode
                    }			// End of Add Mode
                }				// End of IsPostBack
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
			this.Load += new System.EventHandler(this.Page_Load);
            this.ddlQuesType.SelectedIndexChanged += new System.EventHandler(this.ddlQuesType_SelectedIndexChanged);
            this.ddlAnswerType.SelectedIndexChanged += new System.EventHandler(this.ddlAnswerType_SelectedIndexChanged);
            
            this.ddllayout.SelectedIndexChanged += new System.EventHandler(this.ddllayout_SelectedIndexChanged);   
   
		}
		#endregion

        // This function is invoked whenever the value of questiontype drop down is changed.
        private void ddlQuesType_SelectedIndexChanged(object sender, System.EventArgs e)
        {	
			
            try
            {
                lObjSubmit= new ClsUserDefinedTwo();
                if((ddlQuesType.SelectedItem.Value=="1") || (ddlQuesType.SelectedItem.Value=="2")) // If user selects Single list or multiple list from drop down list
                {
					
                    pnlAnsType.Visible=true;
                    reqddlAnswerType.Enabled=true;
                    lblAnsType.Visible=true;
                    ddlAnswerType.Visible=true;
                    ddlAnswerType.Items.Clear();
                    ddlAnswerType.DataSource =  lObjSubmit.fnGetQuestionType(2);
                    ddlAnswerType.DataTextField="QUESTIONTYPENAME";
                    ddlAnswerType.DataValueField="QUESTIONTYPEID";			
                    ddlAnswerType.DataBind();							
                    ddlAnswerType.Items.Insert(0, new ListItem("", ""));				
                    ddlAnswerType.SelectedIndex = 0;
                }
                else
                {
                    pnlDdlList.Visible=false;
                    pnlAnsType.Visible=false;
                    reqddlAnswerType.Enabled=false;
                    lblAnsType.Visible=false;
                    ddlAnswerType.Visible=false;					
                    lblList.Visible=false;
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

        /* This function saves the entered data from the controls to the DB. An object of 
         * Business object is created and the function of that object handles rest of the 
         * functionality.
         */
        public void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                //int lIntActiveStatus;	
                //int lIntIsReqStatus;	
                string lstrLayoutType;
                string lStrActive="";
                string lStrMandatory="";
                if(lblGridType.Text=="9")
                {
                    lstrLayoutType="H";
                    reqddlLayout.Enabled=false;
                }
                else
                {
                    lstrLayoutType = ddllayout.SelectedItem.Value;
                    reqddlLayout.Enabled=true;
                }
                lObjSubmit= new ClsUserDefinedTwo();	
				
			
                /*if(ddlActive.SelectedItem.Value!="")
                {
                    lStrActive=ddlActive.SelectedItem.Value;
                }
                else
                {
                    lStrActive="";
                }*/
                lStrActive="Y";
				
                lStrMandatory=ddlMandatory.SelectedItem.Value;
				

                if(lblTemplateID.Text=="-1") // To check if it is an insert or an update
                {
                    ///Insert
                    if(ddllayout.SelectedItem.Value!="V")
                    {
					
                        if(int.Parse(ddlQuesType.SelectedItem.Value)>2)  // if the questionype is not single list or mulitple list.
                        {
                            reqddlListType.Enabled=false;
                            gIntReturn=lObjSubmit.fnSaveGridQuestion(int.Parse(hidQid.Value),txtQuestion.Text.Trim(),int.Parse(ddlQuesType.SelectedItem.Value),"","",lStrMandatory,lStrActive,lstrLayoutType,int.Parse(GetUserId()),int.Parse(hidGroupID.Value),int.Parse(hidTabID.Value),int.Parse(hidScreenID.Value));
                        }
                        else
                        {
                            gIntReturn=lObjSubmit.fnSaveGridQuestion(int.Parse(hidQid.Value),txtQuestion.Text.Trim(),int.Parse(ddlQuesType.SelectedItem.Value),ddlAnswerType.SelectedItem.Value,ddlList.SelectedItem.Value,lStrMandatory,lStrActive,lstrLayoutType,int.Parse( GetUserId()),int.Parse(hidGroupID.Value),int.Parse(hidTabID.Value),int.Parse(hidScreenID.Value));				
                        }
                    }
                    else
                    {
                        gIntReturn=lObjSubmit.fnSaveGridQuestion(int.Parse(hidQid.Value),txtQuestion.Text.Trim(),0,ddlAnswerType.SelectedItem.Value,ddlList.SelectedItem.Value,lStrMandatory,lStrActive,lstrLayoutType,int.Parse(GetUserId()),int.Parse(hidGroupID.Value),int.Parse(hidTabID.Value),int.Parse(hidScreenID.Value));				
                    }
                    lblTemplateID.Text=gIntReturn.ToString();
                    //lIntActiveStatus=lObjSubmit.fnUpdateQuestionStatus(int.Parse(lblQID.Text));
                    //lIntIsReqStatus=lObjSubmit.fnUpdateQuestionReq(int.Parse(lblQID.Text));	// To update the IsRequired field in UserQuestions for the selected QID
                }
                else
                {
                    ///Update
				
                    if(ddllayout.SelectedItem.Value!="V")
                    {
                        if(ddlQuesType.SelectedItem.Value!="")
                        {
                            if(int.Parse(ddlQuesType.SelectedItem.Value)>2)	// if the question type is neither single nor multiselect question
                            {
                                ddlAnswerType.SelectedItem.Value="";
                                ddlList.SelectedItem.Value="";									
                                reqddlAnswerType.Enabled=false;
                                reqddlListType.Enabled=false;
                            }
                            gIntReturn=lObjSubmit.fnUpdateGridQuestion(int.Parse(lblTemplateID.Text),int.Parse(hidQid.Value),txtQuestion.Text.Trim(),int.Parse(ddlQuesType.SelectedItem.Value),ddlAnswerType.SelectedItem.Value,ddlList.SelectedItem.Value,lStrMandatory,lStrActive,lstrLayoutType,int.Parse(GetUserId()),int.Parse(hidGroupID.Value),int.Parse(hidTabID.Value),int.Parse(hidScreenID.Value));
                        }
                    }
                    else
                    {
                        gIntReturn=lObjSubmit.fnUpdateGridQuestion(int.Parse(lblTemplateID.Text),int.Parse(hidQid.Value),txtQuestion.Text.Trim(),0,ddlAnswerType.SelectedItem.Value,ddlList.SelectedItem.Value,lStrMandatory,lStrActive,lstrLayoutType,int.Parse(GetUserId()),int.Parse(hidGroupID.Value),int.Parse(hidTabID.Value),int.Parse(hidScreenID.Value));
                    }
                    // The follwoing update is for IsRequired field in UserQuestions for the selected QID
                    if(lStrMandatory=="Y")
                    {
                       // lIntIsReqStatus=lObjSubmit.fnUpdateQuestionReq(int.Parse(lblQID.Text));
                    }
                }
                if(ddlAnswerType.SelectedItem.Value=="12") // if the answer type is user defined then keep the add/edit link visible.
                {
                    gStrType="U";
                }
                if(gIntReturn==0)
                {				
                    lblError.Text=lblerrmsg.Text;
                    lblError.Visible=true;
                }
                else if(gIntReturn>0)
                {
                    lblError.Text=lblupdate.Text;	// Transferring the message from resource file to label.
                    lblError.Visible=true;
                    pnlMessage.Visible=true;
                    gIntInsertUpdateFlag=1;
                }
                /*else if(lIntsaveStatus==2)
                {
                    lblError.Text=lblupdate.Text;
                    lblError.Visible=true;
                }*/
              
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

        // This function hides some controls based on the some condition of the form.
        public void fnHideControls()
        {
            try
            {
                lblQuestionType.Visible=false;
                ddlQuesType.Visible=false;
                lblAnsType.Visible=false;
                ddlAnswerType.Visible=false;
                lblList.Visible=false;
                ddlList.Visible=false;
                //lblIsActive.Visible=false;
                //ddlActive.Visible=false;
                lblIsMandatory.Visible=false;
                ddlMandatory.Visible=false;
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
				
            }				
        }
        // This function unhides some controls based on the some condition of the form.
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
                //lblIsActive.Visible=true;
                //ddlActive.Visible=true;
                lblIsMandatory.Visible=true;
                ddlMandatory.Visible=true;
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
				
            }				
        }
        // This function is invoked whenever the value of the answer type drop down gets changed.
        private void ddlAnswerType_SelectedIndexChanged(object sender, System.EventArgs e)
        {			
            try
            {				
                lObjSubmit= new ClsUserDefinedTwo();		//Initializing the object.	
                if(ddlAnswerType.SelectedItem.Value=="11")		// Checking if the List Dropdown should be populated on System Based List
                {
                    gStrType="S";
                }
                else if(ddlAnswerType.SelectedItem.Value=="12") // Checking if the List Dropdown should be populated on User Defined List
                {
                    gStrType="U";
                }
                else if(ddlAnswerType.SelectedItem.Value=="14")  // Checking if the List Dropdown should be populated on Direct List
                {
                    gStrType="D";
                }
				
                if(ddlAnswerType.SelectedItem.Value!="")
                {				
                    pnlDdlList.Visible=true;
                    reqddlListType.Enabled=true;
                    lblList.Visible =true;
                    ddlList.Visible=true;													
                    ddlList.DataSource = new DataView(lObjSubmit.fnList(gStrType).Tables[0]);
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
        // This function hides some controls when the question type selected from the last page was vertical grid.
        public void fnHideForVerticalLayout()
        {
            try
            {
                lblQuestionType.Visible=false;
                ddlQuesType.Visible=false;
                pnlAnsType.Visible=false;
                pnlDdlList.Visible=false;
                lblAnsType.Visible=false;
                ddlAnswerType.Visible=false;
                lblList.Visible=false;
                ddlList.Visible=false;
                lblIsMandatory.Visible=false;
                ddlMandatory.Visible=false;
                //lblIsActive.Visible=false;
                //ddlActive.Visible=false;
                reqddlQuesType.Enabled=false;
                reqddlAnswerType.Enabled=false;
                reqddlListType.Enabled=false;
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
				
            }	
        }
        // This function unhides some controls which were hidden since the question type was vertical grid.
        public void fnShowForVerticalLayout()
        {
            try
            {
                lblQuestionType.Visible=true;
                ddlQuesType.Visible=true;
                pnlDdlList.Visible=true;
                pnlAnsType.Visible=true;
                lblAnsType.Visible=true;
                ddlAnswerType.Visible=true;
                lblList.Visible=true;
                ddlList.Visible=true;
                lblIsMandatory.Visible=true;
                ddlMandatory.Visible=true;
                //lblIsActive.Visible=true;
                //ddlActive.Visible=true;
                reqddlQuesType.Enabled=true;
                reqddlAnswerType.Enabled=true;
                reqddlListType.Enabled=true;
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
			
            }	
        }
        // This function is invoked when the layout type is changed in the form.
        private void ddllayout_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if(ddllayout.SelectedItem.Value.ToString()=="V")
                {
                    lblQuestionType.Visible=false;
                    ddlQuesType.Visible=false;
                    reqddlQuesType.Enabled=false;
                    reqddlAnswerType.Enabled=false;
                    reqddlListType.Enabled=false;
                    pnlQuesType.Visible=false;
                }
                else
                {
                    lblQuestionType.Visible=true;
                    ddlQuesType.Visible=true;
                    reqddlQuesType.Enabled=true;
                    reqddlAnswerType.Enabled=true;
                    reqddlListType.Enabled=true;
                    pnlQuesType.Visible=true;
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
       
   
    }

}
