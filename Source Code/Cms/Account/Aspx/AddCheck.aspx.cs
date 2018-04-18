#region FileInfo
/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  6/30/2005 12:18:30 PM
<End Date				: -	
<Description			: -   Code behind for add check.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: -   Code behind for add check.
*******************************************************************************************/ 
#endregion

#region Namespaces
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
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
#endregion

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Code behind for add check.
	/// </summary>
	public class AddCheck : Cms.Account.AccountBase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbCHECK_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		protected System.Web.UI.WebControls.TextBox txtCHECK_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtCHECK_NOTE;
		protected System.Web.UI.WebControls.TextBox txtPAYEE_ENTITY_ID;
		protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ID;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_ID;
		protected System.Web.UI.WebControls.DropDownList cmbCHECKSIGN_1;
		protected System.Web.UI.WebControls.DropDownList cmbCHECKSIGN_2;
		protected System.Web.UI.WebControls.TextBox txtCHECK_MEMO;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID_HID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID_HID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYEE_ENTITY_ID_HID;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHECK_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE_ENTITY_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHECK_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCHECK_AMOUNT;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;

		public const string VENDOR = "9938";
		public const string GL_ID = "1";
		public const string LIABILITYTYPE = "2";
		

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		public string URL="";
		protected System.Web.UI.WebControls.Label capACCOUNT_ID;
		protected System.Web.UI.WebControls.Label capCHECK_TYPE;
		protected System.Web.UI.WebControls.Label capPAYEE_ENTITY_ID;
		protected System.Web.UI.WebControls.Label capCHECK_DATE;
		protected System.Web.UI.WebControls.Label capCHECK_NUMBER;
		protected System.Web.UI.WebControls.Label capCHECK_MEMO;
		protected System.Web.UI.WebControls.Label capCHECK_AMOUNT;
		protected System.Web.UI.WebControls.Label capCHECK_NOTE;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ID;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.Label capCHECKSIGN_1;
		protected System.Web.UI.WebControls.Label capCHECKSIGN_2;
		protected System.Web.UI.HtmlControls.HtmlImage Img1;
		protected System.Web.UI.HtmlControls.HtmlImage Img2;
		protected System.Web.UI.HtmlControls.HtmlImage Img3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCHECK_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnDistribute;
		protected System.Web.UI.HtmlControls.HtmlImage imgPayee;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_APP_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnCommit;
		protected System.Web.UI.WebControls.Label capPAYEE_ADD1;
		protected System.Web.UI.WebControls.TextBox txtPAYEE_ADD1;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE_ADD1;
		protected System.Web.UI.WebControls.Label capPAYEE_ADD2;
		protected System.Web.UI.WebControls.TextBox txtPAYEE_ADD2;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE_ADD2;
		protected System.Web.UI.WebControls.Label capPAYEE_CITY;
		protected System.Web.UI.WebControls.TextBox txtPAYEE_CITY;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE_CITY;
		protected System.Web.UI.WebControls.Label capPAYEE_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbPAYEE_STATE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE_STATE;
		protected System.Web.UI.WebControls.Label capPAYEE_ZIP;
		protected System.Web.UI.WebControls.TextBox txtPAYEE_ZIP;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYEE_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPAYEE_ZIP;
		protected System.Web.UI.WebControls.CustomValidator csvCHECK_NOTE;
		protected System.Web.UI.WebControls.Label lblCHECK_TYPE;
		protected System.Web.UI.WebControls.Label lblCheckNumber;
		protected System.Web.UI.WebControls.Label CapChkDate;
		protected System.Web.UI.WebControls.Label lblChkDate;
		
		//Defining the business layer class object
		ClsChecks  objCheck ;
		//END:*********** Local variables *************

		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			cmbCHECK_TYPE.Attributes.Add("style","display:none");
			btnReset.Attributes.Add("onclick","javascript:return Reset();");
			btnDistribute.Attributes.Add("onclick","javascript:return OpenDistributeCheck();");
			btnSave.Attributes.Add("onclick","javascript:return SaveClicked();");
			// Added by Swarup on 30-mar-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtPAYEE_ADD1,txtPAYEE_ADD2
				, txtPAYEE_CITY, cmbPAYEE_STATE, txtPAYEE_ZIP);

			
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="204_0";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			btnCommit.CmsButtonClass		=	CmsButtonType.Execute;
			btnCommit.PermissionString		=	gstrSecurityXML;

			btnDistribute.CmsButtonClass	=	CmsButtonType.Execute;
			btnDistribute.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddCheck" ,System.Reflection.Assembly.GetExecutingAssembly());
			//hlkCHECK_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_CHECK_INFORMATION.txtCHECK_DATE,document.ACT_CHECK_INFORMATION.txtCHECK_DATE)");
			URL = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupWindowURL();
			string chkType ="";
			if(!Page.IsPostBack)
			{
				string ACCOUNT_ID="";
				if(Request.QueryString["CHECK_ID"]!=null && Request.QueryString["CHECK_ID"].ToString().Length>0)
				{
					hidOldData.Value=ClsChecks.GetXmlForEditPageControls(Request.QueryString["CHECK_ID"].ToString(),ref ACCOUNT_ID);
					hidCHECK_ID.Value = Request.QueryString["CHECK_ID"].ToString().Trim(); 

					string chkNumber = ClsCommon.FetchValueFromXML("CHECK_NUMBER",hidOldData.Value.ToString());
					string paymentmode = ClsCommon.FetchValueFromXML("PAYMENT_MODE",hidOldData.Value.ToString());
					chkType = ClsCommon.FetchValueFromXML("CHECK_TYPE",hidOldData.Value.ToString());
					
					if(chkNumber!="")
					{
						lblCheckNumber.Text = chkNumber;
					}
					else if(paymentmode=="11788" || paymentmode=="11976")//vendor or Agencf EFT process
					{
						lblCheckNumber.Text="";
					}
					else
						lblCheckNumber.Text=ClsMessages.GetMessage(base.ScreenId,"26");
						//lblCheckNumber.Text = "To be assigned at printing.";

					FillSignatureDropDowns(ACCOUNT_ID);
					GetCheckStatus(Request.QueryString["CHECK_ID"].ToString());
				}

				if(Request.QueryString["CalledFrom"]!=null && Request.QueryString["CalledFrom"] == "Register")
				{
					ClientScript.RegisterStartupScript(this.GetType(),"CheckInfo","<script>ShwDisabledCheckInfo();</script>");
				}

//				if(chkType == VENDOR)
//					ClsGlAccounts.GetAccountsInDropDown(cmbACCOUNT_ID,GL_ID,LIABILITYTYPE);//Fetch form Liability section in Posting Interface.
//				else

				ClsGlAccounts.GetCashAccountsInDropDown(cmbACCOUNT_ID);

				//ClsCommon.BindLookupDDL(ref cmbTRAN_TYPE,"TTFC");
				ClsCommon.BindLookupDDL(ref cmbCHECK_TYPE,"CPAYTP");
				cmbCHECK_TYPE.Items.Insert(0,"");
				//Changes against itrack #5615.
				if(Request.QueryString["CHECK_TYPE_ID"].ToString()!="0" && Request.QueryString["CHECK_TYPE_ID"].ToString()!="1")//i.e. !=All
				{
					cmbCHECK_TYPE.Enabled=false;
					cmbCHECK_TYPE.SelectedValue = Request.QueryString["CHECK_TYPE_ID"].ToString();
				}
				SetCaptions();
				lblChkDate.Text = ClsCommon.FetchValueFromXML("CHECK_DATE",hidOldData.Value.ToString());
				txtCHECK_AMOUNT.Attributes.Add("onblur","javascript:FormatAmount(this)");
				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.State;
				cmbPAYEE_STATE.DataSource		= dt;
				cmbPAYEE_STATE.DataTextField		= "State_Name";
				cmbPAYEE_STATE.DataValueField	= "State_Id";
				cmbPAYEE_STATE.DataBind();
				cmbPAYEE_STATE.Items.Insert(0,"");
				#endregion//Loading singleton
			}
		}//end pageload
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.cmbACCOUNT_ID.SelectedIndexChanged += new System.EventHandler(this.cmbACCOUNT_ID_SelectedIndexChanged);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region "Web Event Handlers (Save / Commit / Delete)"
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objCheck = new  ClsChecks(true);

				//Retreiving the form values into model class object
				ClsChecksInfo objCheckInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objCheckInfo.CREATED_BY = int.Parse(GetUserId());
					objCheckInfo.CREATED_DATETIME = DateTime.Now;
					objCheckInfo.MODIFIED_BY = int.Parse(GetUserId());
					objCheckInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objCheck.Add(objCheckInfo);

					if(intRetVal>0)
					{
						hidCHECK_ID.Value = objCheckInfo.CHECK_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidOldData.Value=ClsChecks.GetXmlForEditPageControls(hidCHECK_ID.Value);
						GetCheckStatus(hidCHECK_ID.Value);
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"13");
						hidFormSaved.Value			=		"2";
						
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"14");
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
					ClsChecksInfo objOldCheckInfo;
					objOldCheckInfo = new ClsChecksInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldCheckInfo,hidOldData.Value);
					try
					{
						objOldCheckInfo.PAYEE_ENTITY_ID = int.Parse(ClsCommon.FetchValueFromXML("PAYEE_ENTITY_ID_HID",hidOldData.Value.ToString()));
						objOldCheckInfo.CUSTOMER_ID = int.Parse(ClsCommon.FetchValueFromXML("CUSTOMER_ID_HID",hidOldData.Value.ToString()));
						objOldCheckInfo.POLICY_ID = int.Parse(ClsCommon.FetchValueFromXML("POLICY_ID_HID",hidOldData.Value.ToString()));
					}
					catch//(Exception ex)
					{
					}
					//Setting those values into the Model object which are not in the page
					objCheckInfo.CHECK_ID = int.Parse(strRowId);
					objCheckInfo.MODIFIED_BY = int.Parse(GetUserId());
					objCheckInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objCheckInfo.CHECK_DATE = objOldCheckInfo.CHECK_DATE;
					//Updating the record using business layer class object
					intRetVal	= objCheck.Update(objOldCheckInfo,objCheckInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value=ClsChecks.GetXmlForEditPageControls(hidCHECK_ID.Value);
						GetCheckStatus(hidCHECK_ID.Value);
					}
					else if(intRetVal == -1)	//check number already assigned to another check. update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"21");
						hidFormSaved.Value		=	"2";
					}
					else if(intRetVal == -2)	//check number exceeds the max limit. update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"22");
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
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objCheck!= null)
					objCheck.Dispose();
			}
		}

		private void btnCommit_Click(object sender, System.EventArgs e)
		{
			if(CommitCheck())
			{
				//print logic here
			}
		}
		
		private bool CommitCheck()
		{
			bool flag=false;
			try
			{
				objCheck = new  ClsChecks(true);
				ClsChecksInfo objCheckInfo;
				objCheckInfo = new ClsChecksInfo();
				base.PopulateModelObject(objCheckInfo,hidOldData.Value);
				System.Xml.XmlDocument objXMLDoc= new System.Xml.XmlDocument();
				objXMLDoc.LoadXml(hidOldData.Value);
				double dblCheckAmount = double.Parse(ClsCommon.GetNodeValue(objXMLDoc,"//CHECK_AMOUNT"));
				int	intRetVal;
				if(dblCheckAmount > 0)
				{
					//Setting those values into the Model object which are not in the page
					objCheckInfo.CHECK_ID = int.Parse(hidCHECK_ID.Value);
					objCheckInfo.MODIFIED_BY = int.Parse(GetUserId());
					objCheckInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					objCheckInfo.IS_COMMITED		=	"Y";
					objCheckInfo.DATE_COMMITTED	=	DateTime.Now;
					
					intRetVal	= objCheck.Commit(objCheckInfo);
				}
				else
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"23");
					lblMessage.Visible		=	true;
					return false;;
				}
				if( intRetVal > 0 )			// update successfully performed
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"8");
					hidFormSaved.Value		=	"1";
					GetCheckStatus(hidCHECK_ID.Value);
					flag = true;
					hidOldData.Value = "";
					hidFormSaved.Value = "5";
					tblBody.Attributes.Add("style", "display:none");
				}
				else if( intRetVal == -1 )			// Record not existed, hence exiting 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"10");
					hidFormSaved.Value		=	"1";
					GetCheckStatus(hidCHECK_ID.Value);
				}
				else if( intRetVal == -2 )			// Record already commited hence exiting
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
					hidFormSaved.Value		=	"1";
					GetCheckStatus(hidCHECK_ID.Value);
				}
				else if( intRetVal == -3 )			//Invoice not distributed fully, therefore exiting
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"25");
					hidFormSaved.Value		=	"1";
					GetCheckStatus(hidCHECK_ID.Value);
				}
				else if( intRetVal == -6 )			//Check not distributed fully, therefore exiting
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
					hidFormSaved.Value		=	"1";
					GetCheckStatus(hidCHECK_ID.Value);
				}
				else if( intRetVal == -7 )			
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"27");
					hidFormSaved.Value		=	"1";
					GetCheckStatus(hidCHECK_ID.Value);
				}
				else if( intRetVal == -10 )			
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"28");
					hidFormSaved.Value		=	"1";
					GetCheckStatus(hidCHECK_ID.Value);
				} 

				else 
				{
					lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"9");
					hidFormSaved.Value		=	"1";
				}
				lblMessage.Visible = true;
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objCheck!= null)
					objCheck.Dispose();
			}
			return flag;
		}

		private void cmbACCOUNT_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(Request.Form["IsSaveClicked"].ToString().Equals("False") && cmbACCOUNT_ID.SelectedIndex>0)
			{
				FillSignatureDropDowns(cmbACCOUNT_ID.SelectedValue);
				hidFormSaved.Value = "3";
			}
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal = new ClsChecks().Delete(hidCHECK_ID.Value,int.Parse(GetUserId()));
			if( intRetVal > 0 )			// delete successfully performed
			{

				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"127");
				hidFormSaved.Value		=	"5";
				hidOldData.Value		=	"";
				tblBody.Attributes.Add("style", "display:none");
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
		}

	
		#endregion
		
		#region Other Functions(GetCheckStatus / SetCaptions / FillCombos / Set Err Msgs / GetFormValue 
		/// <summary>
		/// N: Not Distributed
		/// D:Distributed 
		/// A:approved it is implicit that invoice is distributed.
		/// C:committed , it is implicit that invoice is distributed and approved.
		/// </summary>
		/// <param name="CHECK_ID"></param>
		/// N-Not distributed, 
		/// D-Distributed, 
		/// C-Committed
		private void GetCheckStatus(string CHECK_ID)
		{
			string status = ClsChecks.GetCheckStatus(CHECK_ID);
			switch(status)
			{
				case "N":
					//					btnDistribute.Enabled	=	true;
					btnCommit.Enabled	=	false;
					break;					
				case "D":
					//					btnDistribute.Enabled	=	false;
					btnCommit.Enabled	=	true;
					break;
				case "C":
					//					btnDistribute.Enabled	=	false;
					btnCommit.Enabled	=	false;
					btnSave.Enabled = false;
					break;
			}
			btnDistribute.Enabled	=	true;
		}
		private void SetCaptions()
		{
			capCHECK_TYPE.Text						=		objResourceMgr.GetString("cmbCHECK_TYPE");
			capACCOUNT_ID.Text						=		objResourceMgr.GetString("cmbACCOUNT_ID");
			capCHECK_NUMBER.Text					=		objResourceMgr.GetString("txtCHECK_NUMBER");
			//capCHECK_DATE.Text						=		objResourceMgr.GetString("txtCHECK_DATE");
			capCHECK_AMOUNT.Text					=		objResourceMgr.GetString("txtCHECK_AMOUNT");
			capCHECK_NOTE.Text						=		objResourceMgr.GetString("txtCHECK_NOTE");
			capPAYEE_ENTITY_ID.Text					=		objResourceMgr.GetString("txtPAYEE_ENTITY_ID");
			capCUSTOMER_ID.Text						=		objResourceMgr.GetString("txtCUSTOMER_ID");
			capPOLICY_ID.Text						=		objResourceMgr.GetString("txtPOLICY_ID");
			capCHECKSIGN_1.Text						=		objResourceMgr.GetString("cmbCHECKSIGN_1");
			capCHECKSIGN_2.Text						=		objResourceMgr.GetString("cmbCHECKSIGN_2");
			capCHECK_MEMO.Text						=		objResourceMgr.GetString("txtCHECK_MEMO");
			//capTRAN_TYPE.Text						=		objResourceMgr.GetString("cmbTRAN_TYPE");
			//capIS_DISPLAY_ON_STUB.Text				=		objResourceMgr.GetString("chkIS_DISPLAY_ON_STUB");
			capPAYEE_ADD1.Text						=		objResourceMgr.GetString("txtPAYEE_ADD1");
			capPAYEE_ADD2.Text						=		objResourceMgr.GetString("txtPAYEE_ADD2");
			capPAYEE_CITY.Text						=		objResourceMgr.GetString("txtPAYEE_CITY");
			capPAYEE_STATE.Text						=		objResourceMgr.GetString("cmbPAYEE_STATE");
			capPAYEE_ZIP.Text						=		objResourceMgr.GetString("txtPAYEE_ZIP");

		}

		private void FillSignatureDropDowns(string ACCOUNT_ID)
		{
			ClsGlAccounts.GetAccountAttachmentsInDropDown(cmbCHECKSIGN_1,int.Parse(ACCOUNT_ID));
			ClsGlAccounts.GetAccountAttachmentsInDropDown(cmbCHECKSIGN_2,int.Parse(ACCOUNT_ID));
		
		}
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsChecksInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsChecksInfo objCheckInfo;
			objCheckInfo = new ClsChecksInfo();
			
			objCheckInfo.PAYEE_ADD1  =	txtPAYEE_ADD1.Text;
			objCheckInfo.PAYEE_ADD2  =	txtPAYEE_ADD2.Text;
			objCheckInfo.PAYEE_CITY  =	txtPAYEE_CITY.Text;
			objCheckInfo.PAYEE_STATE =	cmbPAYEE_STATE.SelectedValue;
			objCheckInfo.PAYEE_ZIP   =	txtPAYEE_ZIP.Text;


			objCheckInfo.CHECK_TYPE			=	cmbCHECK_TYPE.SelectedValue;
			objCheckInfo.ACCOUNT_ID			=	int.Parse(cmbACCOUNT_ID.SelectedValue);
			//objCheckInfo.CHECK_NUMBER		=	txtCHECK_NUMBER.Text;
			//objCheckInfo.CHECK_DATE			=	DateTime.Parse(lblChkDate.Text);
			objCheckInfo.CHECK_AMOUNT		=	double.Parse(txtCHECK_AMOUNT.Text.Replace(",",""));
			objCheckInfo.CHECK_NOTE			=	txtCHECK_NOTE.Text;
			if(hidPAYEE_ENTITY_ID_HID.Value.Length>0)
				objCheckInfo.PAYEE_ENTITY_ID	=	int.Parse(hidPAYEE_ENTITY_ID_HID.Value);
			
			if(hidCUSTOMER_ID_HID.Value.Length>0)
				objCheckInfo.CUSTOMER_ID		=	int.Parse(hidCUSTOMER_ID_HID.Value);

			if(hidPOLICY_ID_HID.Value.Length>0)
				objCheckInfo.POLICY_ID			=	int.Parse(hidPOLICY_ID_HID.Value);
			
			if(hidPOLICY_VERSION_ID.Value.Length>0)
				objCheckInfo.POLICY_VER_TRACKING_ID =	int.Parse(hidPOLICY_VERSION_ID.Value);
			if(cmbCHECKSIGN_1.SelectedIndex>0)
				objCheckInfo.CHECKSIGN_1		=	cmbCHECKSIGN_1.SelectedValue;
			if(cmbCHECKSIGN_2.SelectedIndex>0)
				objCheckInfo.CHECKSIGN_2		=	cmbCHECKSIGN_2.SelectedValue;
			objCheckInfo.CHECK_MEMO				=	txtCHECK_MEMO.Text;
			
			objCheckInfo.PAYEE_ENTITY_NAME		=   Request.Form["txtPAYEE_ENTITY_ID"].ToString();
			objCheckInfo.MANUAL_CHECK = "Y";


			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCHECK_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objCheckInfo;
		}

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvACCOUNT_ID.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvCHECK_TYPE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvPAYEE_ENTITY_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			//rfvCHECK_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvCHECK_AMOUNT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");

			//revCHECK_DATE.ValidationExpression		=	aRegExpDate;
			revCHECK_AMOUNT.ValidationExpression	=	aRegExpCurrencyformat;

			//revCHECK_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			revCHECK_AMOUNT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");

			//			rfvPAYEE_ADD1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			//			rfvPAYEE_ADD2.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			//			rfvPAYEE_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
			//			rfvPAYEE_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"18");
			//			rfvPAYEE_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			revPAYEE_ZIP.ValidationExpression	=  aRegExpZip;
			revPAYEE_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
			csvCHECK_NOTE.ErrorMessage =		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("441");
		
		}
		#endregion
		#endregion
	
	}
}
