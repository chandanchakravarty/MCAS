/******************************************************************************************
<Author					: -   Ashwani
<Start Date				: -	4/25/2005 6:53:05 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using Cms.BusinessLayer.BlClient;
//using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using Cms.Model.Client;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Client.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddCustomerNotes : Cms.Client.clientbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtNOTES_SUBJECT;
		protected System.Web.UI.WebControls.DropDownList cmbNOTES_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_ID;
		protected System.Web.UI.WebControls.DropDownList cmbCLAIMS_ID;
		protected System.Web.UI.WebControls.TextBox txtNOTES_DESC;
		//protected System.Web.UI.WebControls.CheckBox chkVISIBLE_TO_AGENCY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPinkSlipNotesTypeID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedPolicy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidClaimsPopUp;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
//		protected System.Web.UI.WebControls.DropDownList cmbPRIORITY;
//		protected System.Web.UI.WebControls.Label  capPRIORITY;
		protected System.Web.UI.WebControls.Label  capTOUSERID;
		protected System.Web.UI.WebControls.DropDownList cmbTOUSERID;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label Caplook;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNOTES_SUBJECT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNOTES_DESC;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.RegularExpressionValidator  revFOLLOW_UP_DATE;
		 protected System.Web.UI.WebControls.CustomValidator  csvFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.Label capNOTES_SUBJECT;
		protected System.Web.UI.WebControls.Label  capNOTES_TYPE;	

		protected System.Web.UI.WebControls.Label  capPOLICY_ID;
		protected System.Web.UI.WebControls.Label  capCLAIMS_ID;
		protected System.Web.UI.WebControls.Label  capNOTES_DESC;	
		
		protected System.Web.UI.WebControls.Label capDIARY_ITEM_REQ;
        protected System.Web.UI.WebControls.DropDownList cmbDIARY_ITEM_REQ;
		protected System.Web.UI.WebControls.Label capFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.TextBox txtFOLLOW_UP_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkFOLLOW_UP_DATE;		
		//private string ClaimID = "";
		protected string strCalledFor="";
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidClaimID;
		//protected System.Web.UI.WebControls.Label  capVISIBLE_TO_AGENCY;


		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		private string []strVersionId;		// Holds the value for Application Id and Application Version Id
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNOTES_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_ID;
		protected System.Web.UI.WebControls.CustomValidator csvNOTES_DESC;
		
		protected int intToUserID=0; // holds the value of touserid	
		//private int			intFromUserId;//holds the value of the user id adding entry in data(loggedin user)
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidApp_Version_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy_Version_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFollowUpDate;

		string strAppVersion;
		//Defining the business layer class object
		Cms.BusinessLayer.BlClient.clsCustomerNotes  objAddCustomerNotes ;
		//END:*********** Local variables *************

		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvNOTES_SUBJECT.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvNOTES_DESC.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			csvNOTES_DESC.ErrorMessage =		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("442");
			//revFOLLOW_UP_DATE
			revFOLLOW_UP_DATE.ValidationExpression			= aRegExpDate;
			revFOLLOW_UP_DATE.ErrorMessage					=  "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			csvFOLLOW_UP_DATE.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvFOLLOW_UP_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				hlkFOLLOW_UP_DATE.Attributes.Add("OnClick","fPopCalendar(document.CLT_CUSTOMER_NOTES.txtFOLLOW_UP_DATE,document.CLT_CUSTOMER_NOTES.txtFOLLOW_UP_DATE)"); //Javascript Implementation for Calender		
				//hlkFOLLOW_UP_DATE.Attributes.Add("Onclick","fPopCalendar(document.getElementById('txtFOLLOW_UP_DATE'),document.getElementById('txtFOLLOW_UP_DATE'))"); //Javascript Implementation for Calender		
				cmbDIARY_ITEM_REQ.Attributes.Add("Onclick","javascript:return ShowFOLLOW_UP_DATE();");   
				cmbDIARY_ITEM_REQ.Attributes.Add("Onblur","javascript:return ShowFOLLOW_UP_DATE();");   
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

				// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
				if(GetCalledFor()!="CLAIM")
					base.ScreenId="108_0";
				else
					base.ScreenId="313_0";
				lblMessage.Visible = false;
				SetErrorMessages();


				if(Request.QueryString["cmbTOUSERID"]!=null)
					intToUserID=int.Parse(Request.QueryString["cmbTOUSERID"].ToString());
				else
					intToUserID=int.Parse(GetUserId().ToString());

              

				//intFromUserId                   =   intToUserID;
				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass	=	CmsButtonType.Write;
				btnReset.PermissionString =	gstrSecurityXML;

				btnSave.CmsButtonClass	= CmsButtonType.Write;
				btnSave.PermissionString =	gstrSecurityXML;

				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
				objResourceMgr = new System.Resources.ResourceManager("Cms.Client.Aspx.AddCustomerNotes",System.Reflection.Assembly.GetExecutingAssembly());
				if(!Page.IsPostBack)
				{
					GetSessionValues();
					GetQueryString();					
					cmbPOLICY_ID.AutoPostBack = true;  
					hidPinkSlipNotesTypeID.Value = clsCustomerNotes.PinkSlipNotesTypeID;
					//<Gaurav Tyagi> 30 May 2005 : START: Added to get Applicaiton numbers, BUG NO<546>
					string selVal = "";
					clsCustomerNotes.GetCustomerApplication(cmbPOLICY_ID,int.Parse(hidCustomer_ID.Value.ToString()),int.Parse(GetLanguageID()));
					strCalledFor=GetCalledFor();
					if(strCalledFor=="POLICY")
					{
						if(hidPolicy_ID!= null && hidPolicy_Version_ID!= null && hidPolicy_ID.Value!= "" && hidPolicy_Version_ID.Value!= "")
						{
							selVal = hidPolicy_ID.Value + "-" +hidPolicy_Version_ID.Value + "-POL";

						}
					}
					else if(strCalledFor=="APPLICATION")
					{
						if (hidApp_ID!= null && hidApp_Version_ID != null && hidApp_ID.Value!= "" && hidApp_Version_ID.Value != "")
						 {
							 selVal = hidApp_ID.Value + "-" +hidApp_Version_ID.Value + "-APP";
				
						 }
					}
					else if(strCalledFor=="CLAIM")
					{
						if(hidPolicy_ID!= null && hidPolicy_Version_ID!= null && hidPolicy_ID.Value!= "" && hidPolicy_Version_ID.Value!= "")
						{
							selVal = hidPolicy_ID.Value + "-" +hidPolicy_Version_ID.Value + "-POL";

						}
					}
					//else 
					cmbPOLICY_ID.Items.Insert(0,new ListItem("",""));
					ListItem li = cmbPOLICY_ID.Items.FindByValue(selVal);
					if(li!=null)
						li.Selected = true;
					//cmbPOLICY_ID.SelectedIndex =cmbPOLICY_ID.Items.IndexOf(cmbPOLICY_ID.Items.FindByValue(selVal));
					//<Gaurav Tyagi> 30 May 2005 : END: Added to get Applicaiton numbers, BUG NO<546>
					SetCaptions();
					GetOldDataXML();
					#region "Loading singleton"
					#endregion//Loading singleton
					loadDropDowns();

					if (Request["CalledFrom"] != null && Request["CalledFrom"] != "")
					{
						hidCalledFrom.Value = Request["CalledFrom"].ToString().Trim().ToUpper();
						if (hidCalledFrom.Value == "CLAIMS")
						{
//							cmbCLAIMS_ID.Attributes.Add("style","display:none");
//							capCLAIMS_ID.Attributes.Add("style","display:none");
							cmbCLAIMS_ID.SelectedValue = GetClaimID();
							hidClaimID.Value = GetClaimID();											
						}
					}
					else
					{
//						cmbCLAIMS_ID.Attributes.Add("style","display:inline");
//						capCLAIMS_ID.Attributes.Add("style","display:inline");
						hidClaimID.Value = "";
					}

				}
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}//end pageload
		#endregion

		#region Function doValidationCheck()
		/// <summary>
		/// validate posted data from form
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool doValidationCheck()
		{
			try
			{
				return true;
			}
			catch (Exception )
			{
				return false;
			}
		}
		
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Client.ClsCustomerNotesInfo GetFormValue()
		{
			
			//Creating the Model object for holding the New data
			ClsCustomerNotesInfo objCustomerNotesInfo;
			objCustomerNotesInfo = new ClsCustomerNotesInfo();

			objCustomerNotesInfo.NOTES_SUBJECT	=	txtNOTES_SUBJECT.Text;
			//objCustomerNotesInfo.NOTES_TYPE	=	int.Parse(cmbNOTES_TYPE.SelectedValue);
			objCustomerNotesInfo.CREATED_BY = int.Parse(GetUserId());
			objCustomerNotesInfo.CREATED_DATETIME = DateTime.Now;

			objCustomerNotesInfo.MODIFIED_BY = int.Parse(GetUserId());
			objCustomerNotesInfo.LAST_UPDATED_DATETIME = DateTime.Now;
			
			//Added by kranti for saving To_Folloup_id in table
			objCustomerNotesInfo.TO_FOLLOWUP_ID   =   int.Parse(cmbTOUSERID.SelectedValue);  

			if(hidCalledFrom.Value=="CLAIMS" && hidClaimsPopUp.Value=="1")
			{
				objCustomerNotesInfo.NOTES_TYPE = int.Parse(clsCustomerNotes.PinkSlipNotesTypeID);
				cmbPOLICY_ID.SelectedIndex = -1;
				ListItem li = cmbPOLICY_ID.Items.FindByValue(hidSelectedPolicy.Value);
				if(li!=null)
					li.Selected = true;
			}
			else
			{
				if (cmbNOTES_TYPE.SelectedValue.Trim() != "")
				{
					objCustomerNotesInfo.NOTES_TYPE	=	int.Parse(cmbNOTES_TYPE.SelectedValue);
				}				
			}	
		
			if (cmbPOLICY_ID.SelectedValue.Trim() != "")
			{
				strVersionId = cmbPOLICY_ID.SelectedValue.Trim().Split('-');
				objCustomerNotesInfo.POLICY_ID	=	int.Parse(strVersionId[0].ToString());
				objCustomerNotesInfo.POLICY_VER_TRACKING_ID = int.Parse(strVersionId[1].ToString());
				objCustomerNotesInfo.QQ_APP_POL = strVersionId[2].ToString();
				if(objCustomerNotesInfo.QQ_APP_POL == "QQ")
				{
					strAppVersion = strVersionId[3].ToString();
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGen=new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
					DataSet dsPolicy= objGen.GetPolicyDetails(int.Parse(this.GetCustomerID()),objCustomerNotesInfo.POLICY_VER_TRACKING_ID, int.Parse(strAppVersion));
					if (dsPolicy.Tables[0].Rows.Count>0)
					{
			        strAppVersion=strAppVersion+ "^" + dsPolicy.Tables[0].Rows[0]["POLICY_ID"].ToString();
					strAppVersion=strAppVersion+ "^" + dsPolicy.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
					dsPolicy.Clear();
					dsPolicy.Dispose();
					}
				}
			}
				
			if (cmbCLAIMS_ID.SelectedValue.Trim() != "")
			{
				objCustomerNotesInfo.CLAIMS_ID	=	int.Parse(cmbCLAIMS_ID.SelectedValue);
			}
			
			objCustomerNotesInfo.NOTES_DESC	=	txtNOTES_DESC.Text;
				//objCustomerNotesInfo.CUSTOMER_ID = 1;
			objCustomerNotesInfo.CUSTOMER_ID = int.Parse(this.GetCustomerID());
			objCustomerNotesInfo.DIARY_ITEM_REQ =cmbDIARY_ITEM_REQ.SelectedValue;
			if (cmbDIARY_ITEM_REQ.SelectedValue == "1")
					objCustomerNotesInfo.FOLLOW_UP_DATE=ConvertToDate(txtFOLLOW_UP_DATE.Text);
           
			
//			else
//					objCustomerNotesInfo.FOLLOW_UP_DATE = DBNull.Value ;   
//			if(chkVISIBLE_TO_AGENCY.Checked == true)
//				objCustomerNotesInfo.VISIBLE_TO_AGENCY	= "1";
//			else 
//				objCustomerNotesInfo.VISIBLE_TO_AGENCY	= "0";
			//These  assignments are common to all pages.
			strFormSaved =	hidFormSaved.Value;
			strRowId =	hidNOTES_ID.Value;
			oldXML	= hidOldData.Value;
			//Returning the model object
			return objCustomerNotesInfo;
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.cmbPOLICY_ID.SelectedIndexChanged += new EventHandler(this.cmbPOLICY_ID_SelectedIndexChanged);    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//Added For Itrack Issue #6007
		private void cmbPOLICY_ID_SelectedIndexChanged(object sender, EventArgs e)
		{			
			string  Policy_Id = "";
			string App_ID = "";
			cmbCLAIMS_ID.Items.Clear();
			if(cmbPOLICY_ID.SelectedValue!="")
			{
			  
				//loadDropDowns(); 		
				string Polic_numb = cmbPOLICY_ID.SelectedValue.ToString(); 				
				string[]Application_details = Polic_numb.Split('-');
			
				if(Application_details!= null && Application_details[0] != "")
				{
            
					if(Application_details[2].ToUpper().ToString() != "QQ")
					{
						Policy_Id = Application_details[0].ToString();			
					}
					else
					{
						Policy_Id = Application_details[1].ToString();		
					}
			
			
					if(Policy_Id!=null && Policy_Id != "")
					{
						App_ID    = Application_details[2].ToUpper().ToString();  
					}			
				}
						
				//Fill 1	
				FillClaims(hidCustomer_ID.Value,Policy_Id,App_ID.ToUpper().ToString());
                
				
			}
			else //In case of Blank
			{
				FillClaims(hidCustomer_ID.Value,Policy_Id,App_ID.ToUpper().ToString());	
			}

			//cmbCLAIMS_ID.DataTextField = "CLAIM_NUMBER";
			//cmbCLAIMS_ID.DataValueField = "CLAIM_ID";
			//cmbCLAIMS_ID.DataBind();
			
			cmbCLAIMS_ID.Items.Insert(0,"");
			cmbCLAIMS_ID.Items[0].Value = "0";
		}
       

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
				int intRetVal;	//For retreiving the return value of business class save function
				objAddCustomerNotes = new  Cms.BusinessLayer.BlClient.clsCustomerNotes();
				//Retreiving the form values into model class object
				ClsCustomerNotesInfo objCustomerNotesInfo = GetFormValue();				
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					
					//Calling the add method of business layer class
					intRetVal = objAddCustomerNotes.Add(objCustomerNotesInfo,strAppVersion);
					if(intRetVal>0)
					{
						hidNOTES_ID.Value = objCustomerNotesInfo.NOTES_ID.ToString();
						lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value	=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text	= ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value	= "2";
					}
					else
					{
						lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value =	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsCustomerNotesInfo objOldCustomerNotesInfo;
					objOldCustomerNotesInfo = new ClsCustomerNotesInfo();
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldCustomerNotesInfo,hidOldData.Value);
					//Setting those values into the Model object which are not in the page
					objCustomerNotesInfo.NOTES_ID = int.Parse(strRowId);
					

					//Updating the record using business layer class object
					intRetVal	= objAddCustomerNotes.Update(objOldCustomerNotesInfo,objCustomerNotesInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objAddCustomerNotes!= null)
					objAddCustomerNotes.Dispose();
			}
		}

	
		
		#endregion
 
		#region Function SetCaptions()
		// <summary>
		/// Set the the caption name from the Resource file
		/// </summary>
		private void SetCaptions()
		{
			try
			{
               
				capNOTES_SUBJECT.Text					=		objResourceMgr.GetString("txtNOTES_SUBJECT");
				capNOTES_TYPE.Text						=		objResourceMgr.GetString("cmbNOTES_TYPE");
				capPOLICY_ID.Text						=		objResourceMgr.GetString("cmbPOLICY_ID");
				capCLAIMS_ID.Text						=		objResourceMgr.GetString("cmbCLAIMS_ID");
                capNOTES_DESC.Text = objResourceMgr.GetString("txtNOTES_DESC");
				capDIARY_ITEM_REQ.Text					=		objResourceMgr.GetString("txtDIARY_ITEM_REQ");
				capFOLLOW_UP_DATE.Text					=		objResourceMgr.GetString("txtFOLLOW_UP_DATE");
				
				capTOUSERID.Text						=		objResourceMgr.GetString("cmbTOUSERID") ;
                Caplook.Text = objResourceMgr.GetString("Caplook");
//				capPRIORITY.Text						=		objResourceMgr.GetString("cmbPRIORITY") ;
				//capVISIBLE_TO_AGENCY.Text				=		objResourceMgr.GetString("chkVISIBLE_TO_AGENCY");
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
            capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
		}
		#endregion
		
		#region GetOldDataXML
		// <summary>
		/// Fetch old data from database on the basis of parameters passed to the page
		/// </summary>
		private void GetOldDataXML()
		{
			try
			{
				if(hidNOTES_ID.Value != "")
				{
					this.hidOldData.Value	= Cms.BusinessLayer.BlClient.clsCustomerNotes.GetCustomerNotesInfo(
						int.Parse(hidNOTES_ID.Value),int.Parse(hidCustomer_ID.Value));
                    //  ADDED BY PRAVEEN KUMAR(12-12-2008):ITRACK 5171
                    btnSave.Visible = false;
                    btnSave.Enabled = false;
                    //  END PRAVEEN KUMAR
                    if (ClsCommon.FetchValueFromXML("FOLLOW_UP_DATE", hidOldData.Value) != "")
                    {
                        string followupdate = ClsCommon.FetchValueFromXML("FOLLOW_UP_DATE", hidOldData.Value);

                        DateTime followupdatenew = Convert.ToDateTime(followupdate);
                        hidFollowUpDate.Value = followupdatenew.ToShortDateString();

                    }
					
				}
				else
				{
					hidNOTES_ID.Value = "";
					hidOldData.Value  = "";

				}
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}

		}
		#endregion

		private void GetQueryString()
		{
			this.hidNOTES_ID.Value	= Request.Params["NOTES_ID"];
			if(Request["ClaimsPopUp"]!=null && Request["ClaimsPopUp"].ToString()!="")
				hidClaimsPopUp.Value = Request["ClaimsPopUp"].ToString();
			else
				hidClaimsPopUp.Value = "-1";
			if(Request["SelectedPolicy"]!=null && Request["SelectedPolicy"].ToString()!="")
				hidSelectedPolicy.Value = Request["SelectedPolicy"].ToString();
			else
				hidSelectedPolicy.Value = "";
			
		}

		private void GetSessionValues()
		{
			this.hidCustomer_ID.Value = GetCustomerID();
			this.hidApp_ID.Value = GetAppID();
			this.hidApp_Version_ID.Value =GetAppVersionID();
			this.hidPolicy_ID.Value = GetPolicyID();
			this.hidPolicy_Version_ID.Value=GetPolicyVersionID();
		}
		
//		public static DataTable GetUserList()
//		{
//			try
//			{
//				DataSet	objUserData		=	DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_SelectUser");
//				return objUserData.Tables[0];
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//		}

		#region Function loadDropDowns()
		// <summary>
		/// Load all the dropdowns at the page load event
		/// </summary>
		private void  loadDropDowns()
		{
			try
			{
				int UserId		=	intToUserID == 0 ? -1 : intToUserID;
//				//PRIORITY
//				cmbPRIORITY.Items.Add(new ListItem("Low","L"));
//				cmbPRIORITY.Items.Add(new ListItem("Medium","M"));
//				cmbPRIORITY.Items.Add(new ListItem("High","H"));
//				cmbPRIORITY.Items.Insert(0,"");
//				
				//TO
				cmbTOUSERID.DataSource		=	ClsCommon.GetUserList();
				cmbTOUSERID.DataTextField	=	USERNAME;
				cmbTOUSERID.DataValueField	=	USERID;
				cmbTOUSERID.DataBind();

				ListItem li=new ListItem(); 
				li=cmbTOUSERID.Items.FindByValue(intToUserID.ToString());   
				if(li!=null)
					li.Selected=true;

                cmbDIARY_ITEM_REQ.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
                cmbDIARY_ITEM_REQ.DataTextField="LookupDesc";
                cmbDIARY_ITEM_REQ.DataValueField ="LookupCode";
                cmbDIARY_ITEM_REQ.DataBind();
                cmbDIARY_ITEM_REQ.Items.Insert(0, "");


				cmbNOTES_TYPE.DataTextField = "LookupDesc";
				cmbNOTES_TYPE.DataValueField = "LookupID";
				cmbNOTES_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("NOTES");
				cmbNOTES_TYPE.DataBind();
				cmbNOTES_TYPE.Items.Insert(0,"");
				//cmbNOTES_TYPE.SelectedIndex = -1;

				clsCustomerNotes objCN = new clsCustomerNotes();
				//Change For Itrack Issue #6007
				if(strCalledFor !="CLAIM")
				{
					cmbCLAIMS_ID.DataSource = objCN.GetCustomerClaimNumber(int.Parse(hidCustomer_ID.Value),hidPolicy_ID.Value,"");
				}
				else
				{
				  	cmbCLAIMS_ID.DataSource = objCN.GetCustomerClaimNumber(int.Parse(hidCustomer_ID.Value),hidPolicy_ID.Value,"CLAIM");
				}
				cmbCLAIMS_ID.DataTextField = "CLAIM_NUMBER";
				cmbCLAIMS_ID.DataValueField = "CLAIM_ID";
				cmbCLAIMS_ID.DataBind();
				cmbCLAIMS_ID.Items.Insert(0,"");
				cmbCLAIMS_ID.Items[0].Value = "0";


                //cmbPOLICY_ID.DataTextField = "LookupDesc";
                //cmbPOLICY_ID.DataValueField = "LookupID";
                //cmbPOLICY_ID.DataSource = Cms.BusinessLayer.BlCommon.GetLookup("NOTES");
                //cmbPOLICY_ID.DataBind();   //ashish


			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}

			

	}
		#endregion
        //Function Created For Itrack Issue #6007
		private void FillClaims(string cust_ID,string Policy_Id,string App_ID)
		{
			clsCustomerNotes objCn = new clsCustomerNotes();	
			try
			{
												
				DataTable dt = objCn.GetCustomerClaimNumber(int.Parse(cust_ID),Policy_Id,App_ID.ToUpper().ToString());		 				
				cmbCLAIMS_ID.DataSource = dt;
				cmbCLAIMS_ID.DataTextField = "CLAIM_NUMBER";
				cmbCLAIMS_ID.DataValueField = "CLAIM_ID";
				cmbCLAIMS_ID.DataBind();


				if(App_ID.ToUpper().ToString() != "APP")
				{	
					if(dt!=null && dt.Rows.Count > 0 )
					{
						cmbCLAIMS_ID.DataSource = dt;
						//cmbCLAIMS_ID.DataSource = objCn.GetCustomerClaimNumber(int.Parse(hidCustomer_ID.Value),Policy_Id,App_ID.ToUpper().ToString());		 					
							
						cmbCLAIMS_ID.DataTextField = "CLAIM_NUMBER";
						cmbCLAIMS_ID.DataValueField = "CLAIM_ID";
						cmbCLAIMS_ID.DataBind();
					}				
					//cmbCLAIMS_ID.DataSource = objCn.GetCustomerClaimNumber(int.Parse(hidCustomer_ID.Value),int.Parse(hidPolicy_ID.Value));
				}
				else if(App_ID.ToString() == "" )
				{
					if(dt.Rows.Count > 0 )
					{
						///		cmbCLAIMS_ID.DataSource = objCn.GetCustomerClaimNumber(int.Parse(hidCustomer_ID.Value),"","");
						cmbCLAIMS_ID.DataSource = dt;
						cmbCLAIMS_ID.DataTextField = "CLAIM_NUMBER";
						cmbCLAIMS_ID.DataValueField = "CLAIM_ID";
						cmbCLAIMS_ID.DataBind();
					}
					else
					{
						cmbCLAIMS_ID.DataSource = null;
						cmbCLAIMS_ID.DataBind();
					}

				}
			}
			catch(Exception ex)
			{
				throw(ex);

			}
		}

        protected void cmbNOTES_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbPOLICY_ID_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void txtNOTES_DESC_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
