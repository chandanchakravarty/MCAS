/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	5/18/2005 5:05:36 PM
<End Date				: -	
<Description				: - 	Code behind for GL Accounts.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - Code behind for GL Accounts.

<Modified Date			: - 25/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Applying Null Check for buttons on aspx page 

<Modified Date			: - 05-10-2005
<Modified By			: - Vijay Arora
<Purpose				: - Add the budget category combo box applied only incase if the 
<                           subtype is income or expense                            
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

namespace Cms.CmsWeb.Accounting
{

	/// <summary>
	/// Code behind for GL Accounts.
	/// </summary>
	public class AddGlAccountInformation :  Cms.CmsWeb.cmsbase//System.Web.UI.Page//
	{
			#region Page controls declaration
			protected System.Web.UI.WebControls.DropDownList cmbCATEGORY_TYPE;
			protected System.Web.UI.WebControls.DropDownList cmbGROUP_TYPE;
			protected System.Web.UI.WebControls.DropDownList cmbACC_PARENT_ID;
			protected System.Web.UI.WebControls.TextBox txtACC_NUMBER;
			protected System.Web.UI.WebControls.DropDownList cmbACC_LEVEL_TYPE;
			protected System.Web.UI.WebControls.TextBox txtACC_DESCRIPTION;
			protected System.Web.UI.WebControls.DropDownList cmbACC_TOTALS_LEVEL;
			protected System.Web.UI.WebControls.DropDownList cmbACC_JOURNAL_ENTRY;

			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidACCOUNT_ID;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidACC_TYPE_ID;

			protected Cms.CmsWeb.Controls.CmsButton btnReset;
			protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
			protected Cms.CmsWeb.Controls.CmsButton btnSave;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvACC_NUMBER;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvACC_LEVEL_TYPE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvACC_DESCRIPTION;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvACC_JOURNAL_ENTRY;
            protected System.Web.UI.WebControls.Label capLabl;
			protected System.Web.UI.WebControls.Label lblMessage;
			
			public string cashType=""; // VARIABLE TO STORE TYPE WHICH IS USED TO DETERMINE IF THIRD TAB OF BANK INFO IS TO BE DISPLAYED OR NOT.
			#endregion


			#region Local form variables
			//START:*********** Local form variables *************
			string oldXML;
			//creating resource manager object (used for reading field and label mapping)
			System.Resources.ResourceManager objResourceMgr;
			private string strRowId, strFormSaved;
			//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capCATEGORY_TYPE;
		protected System.Web.UI.WebControls.Label capGROUP_TYPE;
		protected System.Web.UI.WebControls.Label capACC_PARENT_ID;
		protected System.Web.UI.WebControls.Label capACC_NUMBER;
		protected System.Web.UI.WebControls.Label capACC_LEVEL_TYPE;
		protected System.Web.UI.WebControls.Label capACC_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capACC_TYPE_ID;
		protected System.Web.UI.WebControls.Label capACC_JOURNAL_ENTRY;
		protected System.Web.UI.WebControls.Label capACC_CASH_ACCOUNT;
		protected System.Web.UI.WebControls.CheckBox chkACC_CASH_ACCOUNT;
		protected System.Web.UI.WebControls.Label capACC_CASH_ACC_TYPE;
		protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPEO;
		protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPET;
		protected System.Web.UI.WebControls.Label lblRange;
		protected System.Web.UI.WebControls.TextBox txtACC_TYPE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGROUP_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACC_PARENT_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revACC_NUMBERAcNum;
		protected System.Web.UI.WebControls.RegularExpressionValidator revACC_NUMBERSubAc;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.ListBox lstUnselectedLevels;
		protected System.Web.UI.WebControls.ListBox lstSelectedLevels;
		protected System.Web.UI.WebControls.Label capACC_TOTALS_LEVEL;
		protected System.Web.UI.WebControls.ListBox lstACC_TOTALS_LEVEL;
		protected Cms.CmsWeb.Controls.CmsButton btnSelect;
		protected Cms.CmsWeb.Controls.CmsButton btnDeselect;
		protected System.Web.UI.WebControls.ListBox lstACC_TOTALS_LEVEL_Selected;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACC_TOTALS_LEVEL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSelectedTotals;
		protected System.Web.UI.WebControls.Label capRELATES_TO_TYPE;
        protected System.Web.UI.WebControls.Label capMANDATORY; //sneha
		protected System.Web.UI.WebControls.DropDownList cmbACC_RELATES_TO_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRELATES_TO_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbBUDGET_CATEGORY_ID;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trbody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACC_JOURNAL_ENTRY;
		protected System.Web.UI.WebControls.Label capWOLVERINE_USER_ID;
		protected System.Web.UI.WebControls.Label capBUDGET_CATEGORY_ID;
		protected System.Web.UI.WebControls.DropDownList cmbWOLVERINE_USER_ID;
		//protected System.Web.UI.WebControls.TextBox hidACC_TYPE_ID;
			//Defining the business layer class object
			ClsGlAccounts  objGlAccounts ;
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
			//rfvGROUP_TYPE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			//rfvACC_PARENT_ID.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvACC_NUMBER.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvACC_LEVEL_TYPE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvACC_DESCRIPTION.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvACC_TOTALS_LEVEL.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvACC_JOURNAL_ENTRY.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			//rfvACC_TYPE_ID.ErrorMessage       =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvACC_PARENT_ID.ErrorMessage       =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvGROUP_TYPE.ErrorMessage 			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			rfvRELATES_TO_TYPE.ErrorMessage 	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");

			revACC_NUMBERAcNum.ValidationExpression	=	 aRegExpAcNumber;
			revACC_NUMBERSubAc.ValidationExpression =    aRegExpSubAcNumber; 
			
			
			revACC_NUMBERAcNum.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			revACC_NUMBERSubAc.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
		}
			#endregion

			#region PageLoad event
			private void Page_Load(object sender, System.EventArgs e)
			{
				base.ScreenId="125_1_0";

				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				btnSave.Attributes.Add("onclick","javascript:return CheckAcNoInRange();");
				btnSelect.Attributes.Add("onclick","javascript:return SelectTotalLevels();");
				btnDeselect.Attributes.Add("onclick","javascript:return DeselectTotalLevels();");

				// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
				lblMessage.Visible = false;
				SetErrorMessages();


				
				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass					=	CmsButtonType.Write;
				btnReset.PermissionString				=	gstrSecurityXML;

				btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
				btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

				btnDelete.CmsButtonClass				=	CmsButtonType.Delete;
				btnDelete.PermissionString				=	gstrSecurityXML;

				btnSave.CmsButtonClass					=	CmsButtonType.Write;
				btnSave.PermissionString				=	gstrSecurityXML;

				btnSelect.CmsButtonClass					=	CmsButtonType.Read;
				btnSelect.PermissionString				=	gstrSecurityXML;

				btnDeselect.CmsButtonClass					=	CmsButtonType.Read;
				btnDeselect.PermissionString				=	gstrSecurityXML;

				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Accounting.AddGlAccountInformation" ,System.Reflection.Assembly.GetExecutingAssembly());
				if(!Page.IsPostBack)
				{
					cmbACC_TOTALS_LEVEL.Items.Add("");
					for(int i=1;i<=25;i++)				
					{
						cmbACC_TOTALS_LEVEL.Items.Add(new ListItem(i.ToString(),i.ToString()));
					}
					lstACC_TOTALS_LEVEL.Items.Add("");
					for(int i=1;i<=25;i++)				
					{
						lstACC_TOTALS_LEVEL.Items.Add(new ListItem(i.ToString(),i.ToString()));
					}
					ClsGLAccountRanges.GetSubGroupsInDropDown(cmbGROUP_TYPE);
					ClsGlAccounts.GetParentAcsInDropDown(cmbACC_PARENT_ID);
					if(Request.QueryString["ACCOUNT_ID"]!=null && Request.QueryString["ACCOUNT_ID"].ToString().Length>0)
					{
						SetXML(Request.QueryString["ACCOUNT_ID"].ToString());

					}
					//-- fetching from lookup
					cmbACC_RELATES_TO_TYPE.DataSource		=	ClsCommon.GetLookup("ACPOST");
					cmbACC_RELATES_TO_TYPE.DataTextField		=	"LookupDesc";
					cmbACC_RELATES_TO_TYPE.DataValueField	=	"LookupID";
					cmbACC_RELATES_TO_TYPE.DataBind();
					cmbACC_RELATES_TO_TYPE.Items.Insert(0,"");

					trError.Visible = false;
					//-- fetching from lookup

					//vj
					//call the budget category fill drop down function
					FillBudgetCategoryDropDown();
					FillControls();
                    FillAccounts();
                    FillAccountsLevel();
                    SetCaptions();
                  
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.ToString()));

				}
				DataTable objDataTable =  ClsGLAccountRanges.GetSubRangesForAccounts();
				Response.Write("<script>fromRange = new Array();toRange = new Array(); parentType = new Array();\n");
				for(int i=0;i<objDataTable.Rows.Count;i++)
				{
					Response.Write(" fromRange["+i.ToString()+"] = '");
					Response.Write(objDataTable.Rows[i]["RANGE_FROM"]+"';\n");
					Response.Write("toRange["+i.ToString()+"] = '");
					Response.Write(objDataTable.Rows[i]["RANGE_TO"]+"';\n");
					Response.Write("parentType["+i.ToString()+"] = '");
					Response.Write(objDataTable.Rows[i]["PARENT_CATEGORY_ID"]+"';\n");
				}
				//getting main account types
				objDataTable = ClsGLAccountRanges.GetGroups();
				Response.Write("groups = new Array();\n");
				for(int i=0;i<objDataTable.Rows.Count;i++)
				{
					Response.Write("groups["+objDataTable.Rows[i]["ACC_TYPE_ID"]+"] = '");
					Response.Write(objDataTable.Rows[i]["ACC_TYPE_DESC"]+"';\n");
				}

				//getting parent A/c groups
				objDataTable = ClsGlAccounts.GetParentAcsGroups();
				Response.Write("parentAcGroupType = new Array();\n");
				for(int i=0;i<objDataTable.Rows.Count;i++)
				{
					Response.Write("parentAcGroupType["+objDataTable.Rows[i]["ACCOUNT_ID"]+"] = '");
					Response.Write(objDataTable.Rows[i]["ACC_TYPE_ID"]+"';\n");
				}

				Response.Write("</script>");
			}//end pageload
			#endregion
			
			#region GetFormValue
			/// <summary>
			/// Fetch form's value and stores into model class object and return that object.
			/// </summary>
			private Model.Maintenance.Accounting.ClsGlAccountsInfo GetFormValue()
			{
				//Creating the Model object for holding the New data
				ClsGlAccountsInfo objGlAccountsInfo;
				objGlAccountsInfo = new ClsGlAccountsInfo();
				
				//objGlAccountsInfo.GL_ID = int.Parse(Request.QueryString["GL_ID"]);
                if (Session["GL_ID"] != null && Session["GL_ID"].ToString() != "")
                {
                    objGlAccountsInfo.GL_ID = int.Parse(Session["GL_ID"].ToString());
                }
                else
                {
                    objGlAccountsInfo.GL_ID = 1;
                }
				//objGlAccountsInfo.FISCAL_ID = int.Parse(Request.QueryString["FISCAL_ID"]);

				objGlAccountsInfo.CATEGORY_TYPE			=	int.Parse(cmbCATEGORY_TYPE.SelectedValue);
				if(cmbCATEGORY_TYPE.SelectedIndex==1)
				{
					string acNum = cmbACC_PARENT_ID.SelectedItem.Text;
					//acNum        =  acNum.Substring(0,acNum.IndexOf('.')); Commented by Aditya For TFS BUG # 1844
					objGlAccountsInfo.ACC_DISP_NUMBER	=  acNum+'.'+txtACC_NUMBER.Text;
				}
				else
				{
					string acNum = txtACC_NUMBER.Text;
					objGlAccountsInfo.ACC_DISP_NUMBER	=  acNum+".00";
				}
				if(cmbGROUP_TYPE.SelectedValue!="")
				objGlAccountsInfo.GROUP_TYPE			=	int.Parse(cmbGROUP_TYPE.SelectedValue);
				objGlAccountsInfo.ACC_TYPE_ID			=	int.Parse(hidACC_TYPE_ID.Value);
				if(cmbACC_PARENT_ID.SelectedValue!="")
				objGlAccountsInfo.ACC_PARENT_ID			=	int.Parse(cmbACC_PARENT_ID.SelectedValue);
				
				objGlAccountsInfo.ACC_NUMBER			=	double.Parse(txtACC_NUMBER.Text);

				objGlAccountsInfo.ACC_LEVEL_TYPE		=	cmbACC_LEVEL_TYPE.SelectedValue;
				objGlAccountsInfo.ACC_DESCRIPTION		=	txtACC_DESCRIPTION.Text;
				
				objGlAccountsInfo.ACC_RELATES_TO_TYPE   =   int.Parse(cmbACC_RELATES_TO_TYPE.SelectedValue.ToString());

                if (objGlAccountsInfo.ACC_LEVEL_TYPE.Equals("14460"))//Tfs bug #837... Changed by Amit Mishra on 22nd Sept,2011 
                {
                //if(objGlAccountsInfo.ACC_LEVEL_TYPE.ToUpper().Equals("TOTAL"))//Tfs bug #837... Changed by Amit Mishra on 22nd September,2011 
                //{
					string strTotals=hidSelectedTotals.Value;//Request.Form["lstACC_TOTALS_LEVEL_Selected"].ToString();
			/*		for(int x=0;x<lstACC_TOTALS_LEVEL_Selected.Items.Count;x++)
					{
						strTotals+=lstACC_TOTALS_LEVEL_Selected.Items[x].Text;
						if(x<(lstACC_TOTALS_LEVEL_Selected.Items.Count-1))
							strTotals+=",";
					}*/
					objGlAccountsInfo.ACC_TOTALS_LEVEL	= strTotals.Trim();
                }
                else if (objGlAccountsInfo.ACC_LEVEL_TYPE.Equals("14457"))//Tfs bug #837... changed by amit Mishra on 22nd Sept,2011
                {//else if (objGlAccountsInfo.ACC_LEVEL_TYPE.ToUpper().Equals("AS"))
                    objGlAccountsInfo.ACC_TOTALS_LEVEL = cmbACC_TOTALS_LEVEL.SelectedValue;
                }
				if(hidACC_JOURNAL_ENTRY.Value!= "-1")
				{
					if(cmbACC_JOURNAL_ENTRY.SelectedValue=="10963")
						objGlAccountsInfo.ACC_JOURNAL_ENTRY		=	"Y";
					else if(cmbACC_JOURNAL_ENTRY.SelectedValue=="10964")
						objGlAccountsInfo.ACC_JOURNAL_ENTRY		=	"N";
				}
							
				if(chkACC_CASH_ACCOUNT.Checked)
				{
					objGlAccountsInfo.ACC_CASH_ACCOUNT  =	"Y";
					if(rdbACC_CASH_ACC_TYPEO.Checked)//Checking
						//objGlAccountsInfo.ACC_CASH_ACC_TYPE =	 "O";
						//Itrack No.2080 ....Changed by Manoj Rathore On 3 Aug 2007 
						objGlAccountsInfo.ACC_CASH_ACC_TYPE =	 "C";
					else if(rdbACC_CASH_ACC_TYPET.Checked)//Saving
						//objGlAccountsInfo.ACC_CASH_ACC_TYPE =	 "T";
						//Itrack No.2080 ....Changed by Manoj Rathore On 3 Aug 2007 
						objGlAccountsInfo.ACC_CASH_ACC_TYPE =	 "S";
				}	
//				else
//				{
//					objGlAccountsInfo.ACC_CASH_ACCOUNT  =	"N";
//				}

				//vj
				if(cmbBUDGET_CATEGORY_ID.SelectedValue!="")
				{
					objGlAccountsInfo.BUDGET_CATEGORY_ID = Convert.ToInt32(cmbBUDGET_CATEGORY_ID.SelectedValue);
				}
				
				if(cmbWOLVERINE_USER_ID.SelectedValue!="")
				{
					objGlAccountsInfo.WOLVERINE_USER_ID = Convert.ToInt32(cmbWOLVERINE_USER_ID.SelectedValue);
				}

				//These  assignments are common to all pages.
				strFormSaved							=	hidFormSaved.Value;
				strRowId								=	hidACCOUNT_ID.Value;
				oldXML									=   hidOldData.Value;
				//Returning the model object

				return objGlAccountsInfo;
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
				this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
				this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
					 objGlAccounts = new  ClsGlAccounts(true);

					//Retreiving the form values into model class object
					ClsGlAccountsInfo objGlAccountsInfo = GetFormValue();

					if(strRowId.ToUpper().Equals("NEW")) //save case
					{
						objGlAccountsInfo.CREATED_BY = int.Parse(GetUserId());
						objGlAccountsInfo.CREATED_DATETIME = DateTime.Now;
						objGlAccountsInfo.IS_ACTIVE = "Y";

						//Calling the add method of business layer class
						intRetVal = objGlAccounts.Add(objGlAccountsInfo);

						if(intRetVal>0)
						{
							hidACCOUNT_ID.Value = objGlAccountsInfo.ACCOUNT_ID.ToString();
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"29");
							hidFormSaved.Value			=		"1";
							hidIS_ACTIVE.Value = "Y";
							SetXML(objGlAccountsInfo.ACCOUNT_ID.ToString());
                            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objGlAccountsInfo.IS_ACTIVE.ToString());
						}
						else if(intRetVal == -1)
						{
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"1");
							hidFormSaved.Value			=		"2";
						}
						else
						{
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"20");
							hidFormSaved.Value			=		"2";
						}
						lblMessage.Visible = true;
					} // end save case
					else //UPDATE CASE
					{

						//Creating the Model object for holding the Old data
						ClsGlAccountsInfo objOldGlAccountsInfo;
						objOldGlAccountsInfo = new ClsGlAccountsInfo();

						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldGlAccountsInfo,hidOldData.Value);

						if(objOldGlAccountsInfo.ACC_JOURNAL_ENTRY=="10963")
							objOldGlAccountsInfo.ACC_JOURNAL_ENTRY		=	"Y";
						else if(objOldGlAccountsInfo.ACC_JOURNAL_ENTRY=="10964")
							objOldGlAccountsInfo.ACC_JOURNAL_ENTRY		=	"N";

						//Setting those values into the Model object which are not in the page
						objGlAccountsInfo.ACCOUNT_ID = int.Parse(strRowId);
						objGlAccountsInfo.MODIFIED_BY = int.Parse(GetUserId());
						objGlAccountsInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						objGlAccountsInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
						string Category = cmbCATEGORY_TYPE.Items[cmbCATEGORY_TYPE.SelectedIndex].Text;
						string SubType = cmbGROUP_TYPE.Items[cmbGROUP_TYPE.SelectedIndex].Text;
						string ParentAccount=cmbACC_PARENT_ID.Items[cmbACC_PARENT_ID.SelectedIndex].Text;
						//Updating the record using business layer class object
						intRetVal	= objGlAccounts.Update(objOldGlAccountsInfo,objGlAccountsInfo,Category,SubType,ParentAccount);
						if( intRetVal > 0 )			// update successfully performed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							SetXML(objGlAccountsInfo.ACCOUNT_ID.ToString());
						}
						else if(intRetVal == -1)	// Duplicate code exist, update failed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
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
					if(objGlAccounts!= null)
						objGlAccounts.Dispose();
				}
			}

			/// <summary>
			/// Activates and deactivates  .
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
			{
				try
				{
					Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
					objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
					objStuTransactionInfo.loggedInUserName = GetUserName();
					ClsGlAccountsInfo objGlAccountsInfo = GetFormValue();
					objGlAccounts =  new ClsGlAccounts();
					string CategoryType = cmbCATEGORY_TYPE.Items[cmbCATEGORY_TYPE.SelectedIndex].Text;
					string SubType = cmbGROUP_TYPE.Items[cmbGROUP_TYPE.SelectedIndex].Text;
					string CustomerDesc ="; Category :"+ CategoryType;
 
					if(cmbGROUP_TYPE.Items[cmbGROUP_TYPE.SelectedIndex].Text!="")
					{
						CustomerDesc+="; Sub Type :" + SubType ;
					}

					CustomerDesc+="; Account Number:" + objGlAccountsInfo.ACC_NUMBER;

					if(cmbACC_PARENT_ID.Items[cmbACC_PARENT_ID.SelectedIndex].Text!="")
					{
						CustomerDesc+=";Parent Account:" + cmbACC_PARENT_ID.Items[cmbACC_PARENT_ID.SelectedIndex].Text;
					}

					if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
					{
						objStuTransactionInfo.transactionDescription = "GL Account Deactivated Successfully.";
						objGlAccounts.TransactionInfoParams = objStuTransactionInfo;
						objGlAccounts.ActivateDeactivate(hidACCOUNT_ID.Value,"N",CustomerDesc);
                        objGlAccountsInfo.IS_ACTIVE = "N";
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objGlAccountsInfo.IS_ACTIVE.ToString().Trim());
						hidIS_ACTIVE.Value="N";
					}
					else
					{
						objStuTransactionInfo.transactionDescription = "GL Account Activated Successfully.";
						objGlAccounts.TransactionInfoParams = objStuTransactionInfo;
						objGlAccounts.ActivateDeactivate(hidACCOUNT_ID.Value,"Y",CustomerDesc);
                        objGlAccountsInfo.IS_ACTIVE = "Y";
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objGlAccountsInfo.IS_ACTIVE.ToString().Trim());
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
						hidIS_ACTIVE.Value="Y";
					}
					hidFormSaved.Value			=	"1";
					SetXML(hidACCOUNT_ID.Value);
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
					if(objGlAccounts!= null)
						objGlAccounts.Dispose();
				}
			}
			#endregion

			private void SetXML(string accountID)
			{
				DataSet objDataSet=null;
				objDataSet = ClsGlAccounts.GetXmlForPageControls(accountID);
				if(objDataSet.Tables[0].Rows.Count<=0)
					hidOldData.Value = "";
				else
				{
					cashType = objDataSet.Tables[0].Rows[0]["ACC_CASH_ACCOUNT"].ToString();
					Session["ACCOUNT_ID"] = objDataSet.Tables[0].Rows[0]["ACCOUNT_ID"].ToString();
					hidOldData.Value = objDataSet.GetXml();
				}
			}
		private void ShowMessage(string strMessage)
		{
			trbody.Attributes.Add("style","display:none");
			lblError.Text = strMessage;
			trError.Visible = true;
			//return;
		}

			private void SetCaptions()
			{

				capCATEGORY_TYPE.Text						=		objResourceMgr.GetString("cmbCATEGORY_TYPE");
				capGROUP_TYPE.Text							=		objResourceMgr.GetString("cmbGROUP_TYPE");
				capACC_TYPE_ID.Text							=		objResourceMgr.GetString("txtACC_TYPE_DESC");
				capACC_PARENT_ID.Text						=		objResourceMgr.GetString("cmbACC_PARENT_ID");
				capACC_NUMBER.Text							=		objResourceMgr.GetString("txtACC_NUMBER");
				capACC_LEVEL_TYPE.Text						=		objResourceMgr.GetString("cmbACC_LEVEL_TYPE");
				capACC_DESCRIPTION.Text						=		objResourceMgr.GetString("txtACC_DESCRIPTION");
				capACC_TOTALS_LEVEL.Text					=		objResourceMgr.GetString("cmbACC_TOTALS_LEVEL");
				capACC_TOTALS_LEVEL.Text					=		objResourceMgr.GetString("cmbACC_TOTALS_LEVEL");
				capACC_JOURNAL_ENTRY.Text					=		objResourceMgr.GetString("cmbACC_JOURNAL_ENTRY");
				capACC_CASH_ACCOUNT.Text					=		objResourceMgr.GetString("chkACC_CASH_ACCOUNT");
				capACC_CASH_ACC_TYPE.Text					=		objResourceMgr.GetString("cmbACC_CASH_ACC_TYPE");
				capRELATES_TO_TYPE.Text						=		objResourceMgr.GetString("cmbACC_RELATES_TO_TYPE");
				capWOLVERINE_USER_ID.Text					=		objResourceMgr.GetString("cmbWOLVERINE_USER_ID");
                capMANDATORY.Text                           =       Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); //sneha
                rdbACC_CASH_ACC_TYPEO.Text = objResourceMgr.GetString("rdbACC_CASH_ACC_TYPEO");
                rdbACC_CASH_ACC_TYPET.Text = objResourceMgr.GetString("rdbACC_CASH_ACC_TYPET");
                capLabl.Text = objResourceMgr.GetString("capLabl");
			}


			private void btnDelete_Click(object sender, System.EventArgs e)
		   {
			int intRetVal = new ClsGlAccounts().Delete(hidACCOUNT_ID.Value);
			if( intRetVal > 0 )			// delete successfully performed
			{
                ShowMessage(Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"127")) ;
				//lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"127");
				hidFormSaved.Value		=	"1";
				hidOldData.Value		=	"";
				hidDelete.Value         =   "1";
			}
			else if(intRetVal == -1)	// delete can not be performed as account is used in transactions
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"12");
				hidFormSaved.Value		=	"2";
			}
			else 

			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}

			lblMessage.Visible = true;
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			ClsGlAccountsInfo objGlAccountsInfo = GetFormValue();
			string trans_desc ="GL Accounts has been Deleted.";
			string CategoryType = cmbCATEGORY_TYPE.Items[cmbCATEGORY_TYPE.SelectedIndex].Text;
			string SubType = cmbGROUP_TYPE.Items[cmbGROUP_TYPE.SelectedIndex].Text;
			string trans_custom ="; Category :"+ CategoryType +"; Sub Type :" + SubType +"; Account Number:" + objGlAccountsInfo.ACC_NUMBER;
			objGen.WriteTransactionLog(0, 0, 0, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");

		}

		#region Fill Controls
		private void FillControls()
		{
			cmbACC_JOURNAL_ENTRY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbACC_JOURNAL_ENTRY.DataTextField="LookupDesc";
			cmbACC_JOURNAL_ENTRY.DataValueField="LookupID";
			cmbACC_JOURNAL_ENTRY.DataBind();
			cmbACC_JOURNAL_ENTRY.Items.Insert(0,"");	
			
		}
        private void FillAccounts()
        {
            cmbCATEGORY_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CTG");
            cmbCATEGORY_TYPE.DataTextField = "LookupDesc";
            cmbCATEGORY_TYPE.DataValueField = "LookupID";
            cmbCATEGORY_TYPE.DataBind();
        }
        private void FillAccountsLevel()
        {
            cmbACC_LEVEL_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LVLTYP");
            cmbACC_LEVEL_TYPE.DataTextField = "LookupDesc";
            cmbACC_LEVEL_TYPE.DataValueField = "LookupID";
            cmbACC_LEVEL_TYPE.DataBind();
        }
       
		#endregion
		//vj
		#region Fill Drop Down List of Budget Category
		private void FillBudgetCategoryDropDown()
		{
			ClsBudgetCategory objBudgetCategory = new ClsBudgetCategory();
			objBudgetCategory.GetBudgetCategoryInDropdownGLActs(cmbBUDGET_CATEGORY_ID);
						
			cmbWOLVERINE_USER_ID.DataSource		= ClsBudgetCategory.GetWolverineUsers(Cms.CmsWeb.cmsbase.CarrierSystemID);
			cmbWOLVERINE_USER_ID.DataTextField	= "WOLVERINE_USERS";
			cmbWOLVERINE_USER_ID.DataValueField	= "WOLVERINE_USER_ID";
			cmbWOLVERINE_USER_ID.DataBind();
			cmbWOLVERINE_USER_ID.Items.Insert(0,"");	
		}
		#endregion
		//end vj

			
		}
	}
