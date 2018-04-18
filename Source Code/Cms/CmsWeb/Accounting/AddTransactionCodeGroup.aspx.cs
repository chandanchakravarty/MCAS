/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	6/7/2005 9:01:40 PM
<End Date				: -	
<Description				: - 	Code behind for transaction code group.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - Code behind for transaction code group.
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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher.ExceptionManagement; 
using Cms.BusinessLayer.BlApplication;
namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Code behind for transaction code group.
	/// </summary>
	public class AddTransactionCodeGroup : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbSUB_LOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbCLASS_RISK;
		protected System.Web.UI.WebControls.RadioButton rdbPOLICY_TYPEM;
		protected System.Web.UI.WebControls.RadioButton rdbPOLICY_TYPEP;
		protected System.Web.UI.WebControls.CheckBox chkNEW_BUSINESS;
		protected System.Web.UI.WebControls.CheckBox chkCHANGE_IN_NEW_BUSINESS;
		protected System.Web.UI.WebControls.CheckBox chkRENEWAL;
		protected System.Web.UI.WebControls.CheckBox chkCHANGE_IN_RENEWAL;
		protected System.Web.UI.WebControls.CheckBox chkREINSTATE_SAME_TERM;
		protected System.Web.UI.WebControls.CheckBox chkREINSTATE_NEW_TERM;
		protected System.Web.UI.WebControls.CheckBox chkCANCELLATION;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_LOB_ID;

		protected System.Web.UI.WebControls.Label lblMessage;

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capSTATE_ID;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.Label capSUB_LOB_ID;
		protected System.Web.UI.WebControls.Label capCLASS_RISK;
		protected System.Web.UI.WebControls.Label capPOLICY_TYPE;
		protected System.Web.UI.WebControls.RadioButton opt;
		protected System.Web.UI.WebControls.Label capPROCESS_TRAN_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_GROUP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB_ID;
		protected System.Web.UI.WebControls.CheckBox chkPROCESS_TRAN_TYPE;
		protected System.Web.UI.WebControls.Label capNEW_BUSINESS;
		protected System.Web.UI.WebControls.Label capCHANGE_IN_NEW_BUSINESS;
		protected System.Web.UI.WebControls.Label capRENEWAL;
		protected System.Web.UI.WebControls.Label capCHANGE_IN_RENEWAL;
		protected System.Web.UI.WebControls.Label capREINSTATE_SAME_TERM;
		protected System.Web.UI.WebControls.Label capREINSTATE_NEW_TERM;
		protected System.Web.UI.WebControls.Label capCANCELLATION;
		protected System.Web.UI.WebControls.CustomValidator csvProcessTransactionType;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		//Defining the business layer class object
		ClsTransactionCodeGroup  objTransactionCodeGroup ;
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
			rfvLOB_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvSUB_LOB_ID.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			csvProcessTransactionType.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{

			btnReset.Attributes.Add("onclick","javascript:return ResetPage();");
			btnSave.Attributes.Add("onclick","javascript:return Validate();");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="186_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Execute;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass			=	CmsButtonType.Execute;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Execute;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Accounting.AddTransactionCodeGroup" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				//-- this code has to be chaged
				cmbCLASS_RISK.DataSource		=	ClsCommon.GetLookup("DRTCD");
				cmbCLASS_RISK.DataTextField		=	"LookupDesc";
				cmbCLASS_RISK.DataValueField	=	"LookupID";
				cmbCLASS_RISK.DataBind();
				cmbCLASS_RISK.Items.Insert(0,new ListItem("All","0"));
				//cmbCLASS_RISK.Items.RemoveAt(0);
				//-- this code has to be chaged

				//GetLobsInDropDown(cmbLOB_ID);
				//hidLOBXML.Value = Cms.BusinessLayer.BlApplication.clsPkgLobDetails.GetXmlForLobByState();
				hidLOBXML.Value = ClsCommon.GetXmlForLobByState();
				SetCaptions();
				if(Request.QueryString["TRAN_GROUP_ID"]!=null)
				{
					//SetGroupCodeToSession(Request.QueryString["TRAN_GROUP_ID"].ToString());
					DataSet objDataSet = ClsTransactionCodeGroup.GetXmlForPageControls(Request.QueryString["TRAN_GROUP_ID"].ToString());
					hidOldData.Value = objDataSet.GetXml();
					FillLobDropDown(int.Parse(objDataSet.Tables[0].Rows[0]["STATE_ID"].ToString()));
				}
				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
				cmbSTATE_ID.DataSource		= dt;
				DataRow row = dt.NewRow();
				row["State_Name"] = "All";
				row["State_Id"] = "0";
				dt.Rows.InsertAt(row,0);
				cmbSTATE_ID.DataTextField	= "State_Name";
				cmbSTATE_ID.DataValueField	= "State_Id";
				cmbSTATE_ID.DataBind();
				cmbSTATE_ID.Items.Insert(0,"");
				#endregion//Loading singleton
				hidFormSaved.Value = "0";
			}
		}//end pageload
		#endregion
		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsTransactionCodeGroupInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsTransactionCodeGroupInfo objTransactionCodeGroupInfo;
			objTransactionCodeGroupInfo = new ClsTransactionCodeGroupInfo();

			objTransactionCodeGroupInfo.STATE_ID	=	int.Parse(cmbSTATE_ID.SelectedValue);
			objTransactionCodeGroupInfo.LOB_ID		=	int.Parse(Request.Form["cmbLOB_ID"].ToString());
			objTransactionCodeGroupInfo.SUB_LOB_ID	=	int.Parse(Request.Form["cmbSUB_LOB_ID"].ToString());
			objTransactionCodeGroupInfo.CLASS_RISK	=	int.Parse(cmbCLASS_RISK.SelectedValue);
					
			if(rdbPOLICY_TYPEM.Checked)
				objTransactionCodeGroupInfo.POLICY_TYPE	=	"M"	;
			else if(rdbPOLICY_TYPEP.Checked)
				objTransactionCodeGroupInfo.POLICY_TYPE	=	"P"	;

			if(chkNEW_BUSINESS.Checked)
				objTransactionCodeGroupInfo.NEW_BUSINESS =	"Y";
			else
				objTransactionCodeGroupInfo.NEW_BUSINESS =	"N";
			
			if(chkCHANGE_IN_NEW_BUSINESS.Checked)
				objTransactionCodeGroupInfo.CHANGE_IN_NEW_BUSINESS =	"Y";
			else
				objTransactionCodeGroupInfo.CHANGE_IN_NEW_BUSINESS =	"N";

			if(chkRENEWAL.Checked)
				objTransactionCodeGroupInfo.RENEWAL =	"Y";
			else
				objTransactionCodeGroupInfo.RENEWAL =	"N";
			
			if(chkCHANGE_IN_RENEWAL.Checked)
				objTransactionCodeGroupInfo.CHANGE_IN_RENEWAL =	"Y";
			else
				objTransactionCodeGroupInfo.CHANGE_IN_RENEWAL =	"N";
		
			if(chkREINSTATE_SAME_TERM.Checked)
				objTransactionCodeGroupInfo.REINSTATE_SAME_TERM =	"Y";
			else
				objTransactionCodeGroupInfo.REINSTATE_SAME_TERM =	"N";
		
			if(chkREINSTATE_NEW_TERM.Checked)
				objTransactionCodeGroupInfo.REINSTATE_NEW_TERM =	"Y";
			else
				objTransactionCodeGroupInfo.REINSTATE_NEW_TERM =	"N";
		
			if(chkCANCELLATION.Checked)
				objTransactionCodeGroupInfo.CANCELLATION =	"Y";
			else
				objTransactionCodeGroupInfo.CANCELLATION =	"N";
					
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidTRAN_GROUP_ID.Value;
			oldXML			=	hidOldData.Value;
			//Returning the model object

			return objTransactionCodeGroupInfo;
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
			this.cmbSTATE_ID.SelectedIndexChanged += new System.EventHandler(this.cmbSTATE_ID_SelectedIndexChanged);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

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
				objTransactionCodeGroup = new  ClsTransactionCodeGroup(true);

				//Retreiving the form values into model class object
				ClsTransactionCodeGroupInfo objTransactionCodeGroupInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objTransactionCodeGroupInfo.CREATED_BY = int.Parse(GetUserId());
					objTransactionCodeGroupInfo.CREATED_DATETIME = DateTime.Now;
					objTransactionCodeGroupInfo.IS_ACTIVE = "Y";
					objTransactionCodeGroupInfo.MODIFIED_BY = int.Parse(GetUserId());
					objTransactionCodeGroupInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objTransactionCodeGroup.Add(objTransactionCodeGroupInfo);

					if(intRetVal>0)
					{
						hidTRAN_GROUP_ID.Value = objTransactionCodeGroupInfo.TRAN_GROUP_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidOldData.Value = ClsTransactionCodeGroup.GetXmlForPageControls(objTransactionCodeGroupInfo.TRAN_GROUP_ID.ToString()).GetXml();
						hidIS_ACTIVE.Value = "Y";
						//SetGroupCodeToSession(objTransactionCodeGroupInfo.TRAN_GROUP_ID.ToString());
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsTransactionCodeGroupInfo objOldTransactionCodeGroupInfo;
					objOldTransactionCodeGroupInfo = new ClsTransactionCodeGroupInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldTransactionCodeGroupInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objTransactionCodeGroupInfo.TRAN_GROUP_ID = int.Parse(strRowId);
					objTransactionCodeGroupInfo.MODIFIED_BY = int.Parse(GetUserId());
					objTransactionCodeGroupInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= objTransactionCodeGroup.Update(objOldTransactionCodeGroupInfo,objTransactionCodeGroupInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		 = ClsTransactionCodeGroup.GetXmlForPageControls(objTransactionCodeGroupInfo.TRAN_GROUP_ID.ToString()).GetXml();
						
						//SetGroupCodeToSession(objTransactionCodeGroupInfo.TRAN_GROUP_ID.ToString());
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
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
				if(objTransactionCodeGroup!= null)
					objTransactionCodeGroup.Dispose();
			}
		}
		#endregion
		private void SetCaptions()
		{
			capSTATE_ID.Text						=		objResourceMgr.GetString("cmbSTATE_ID");
			capLOB_ID.Text							=		objResourceMgr.GetString("cmbLOB_ID");
			capSUB_LOB_ID.Text						=		objResourceMgr.GetString("cmbSUB_LOB_ID");
			capCLASS_RISK.Text						=		objResourceMgr.GetString("cmbCLASS_RISK");
			capPOLICY_TYPE.Text						=		objResourceMgr.GetString("rdbPOLICY_TYPE");
			capNEW_BUSINESS.Text					=		objResourceMgr.GetString("chkNEW_BUSINESS");
			capCHANGE_IN_NEW_BUSINESS.Text			=		objResourceMgr.GetString("chkCHANGE_IN_NEW_BUSINESS");
			capRENEWAL.Text							=		objResourceMgr.GetString("chkRENEWAL");
			capCHANGE_IN_RENEWAL.Text				=		objResourceMgr.GetString("chkCHANGE_IN_RENEWAL");
			capREINSTATE_SAME_TERM.Text				=		objResourceMgr.GetString("chkREINSTATE_SAME_TERM");
			capREINSTATE_NEW_TERM.Text				=		objResourceMgr.GetString("chkREINSTATE_NEW_TERM");
			capCANCELLATION.Text					=		objResourceMgr.GetString("chkCANCELLATION");
		}
		#region "Fill DropDowns"
		public static void GetLobsInDropDown(DropDownList objDropDownList, string selectedValue)
		{
			
			DataTable  objDataTable =Cms.CmsWeb.ClsFetcher.LOBs;
			objDropDownList.Items.Clear();
			objDropDownList.Items.Add("");
			objDropDownList.Items.Add(new ListItem("ALL","0"));
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["LOB_DESC"].ToString(),objDataTable.DefaultView[i]["LOB_ID"].ToString()));
				if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["LOB_ID"].ToString().Equals(selectedValue))
					objDropDownList.SelectedIndex = i;
			}
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objTransactionCodeGroup = new  ClsTransactionCodeGroup(true);

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objTransactionCodeGroup.TransactionInfoParams = objStuTransactionInfo;
					objTransactionCodeGroup.ActivateDeactivate(hidTRAN_GROUP_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objTransactionCodeGroup.TransactionInfoParams = objStuTransactionInfo;
					objTransactionCodeGroup.ActivateDeactivate(hidTRAN_GROUP_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"1";
				hidOldData.Value		    =   ClsTransactionCodeGroup.GetXmlForPageControls(hidTRAN_GROUP_ID.Value).GetXml();
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objTransactionCodeGroup!= null)
					objTransactionCodeGroup.Dispose();
			}
		}


		private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				int stateID;
				
				stateID=cmbSTATE_ID.SelectedItem==null?-1:int.Parse(cmbSTATE_ID.SelectedItem.Value);     
				if(stateID!=-1)
				{
					FillLobDropDown(stateID);					 
					hidFormSaved.Value = "3";
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		private void FillLobDropDown(int stateID)
		{
			ClsGeneralInformation objGenInfo=new ClsGeneralInformation(); 
			DataSet dsLOB=new DataSet(); 
			dsLOB=objGenInfo.GetLOBBYSTATEID(stateID);
			cmbLOB_ID.DataSource=dsLOB;
			cmbLOB_ID.DataTextField="LOB_DESC";
			cmbLOB_ID.DataValueField="LOB_ID"; 
			cmbLOB_ID.DataBind();  
			cmbLOB_ID.Items.Insert(0,new ListItem("All","0"));
			cmbLOB_ID.Items.Insert(0,"");
			
		}
		public static void GetLobsInDropDown(DropDownList objDropDownList)
		{
			GetLobsInDropDown(objDropDownList,null);
		}
		#endregion

//		private void SetGroupCodeToSession(string groupCode)
//		{
//			Session["TRAN_GROUP_ID"] = groupCode;
//		}
	}
}
