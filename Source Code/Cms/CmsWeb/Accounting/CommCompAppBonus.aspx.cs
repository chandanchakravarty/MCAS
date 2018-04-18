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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher.ExceptionManagement; 
#endregion

namespace Cms.CmsWeb.Maintenance.Accounting
{
	
	public class CommCompAppBonus :  Cms.CmsWeb.cmsbase
	{
		#region "Control variables"
		protected System.Web.UI.WebControls.Label capSTATE_ID;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.Label capTERM;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_TO_DATE;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOMM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;

	
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbTERM;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_TO_DATE;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_TO_DATE;

		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.Label lblMessage;

		#endregion

		#region Local form variables
		string oldXML;
		System.Resources.ResourceManager objResourceMgr;
		private string strRowIden, strFormSaved;
		protected System.Web.UI.WebControls.Label lblCOUNTRY_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOUNTRY_ID;

		//const string COUNTRYID = "1";//countryid hard coded for USA for save case //
        string COUNTRYID = "1";//Added by Amit Mishra for Tfs bug# 836
        protected System.Web.UI.WebControls.CustomValidator csvCHECK_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkFROM_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTO_DATE;
		///const string COUNTRY_NAME="USA";
        string COUNTRY_NAME = "USA";//Added By Amit Mishra for tfs bug #836
		protected System.Web.UI.HtmlControls.HtmlGenericControl tbForm;
		protected System.Web.UI.WebControls.RadioButtonList rdblAMOUNT_TYPE;
		protected System.Web.UI.WebControls.Label capAMOUNT_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT_TYPE;
		protected System.Web.UI.WebControls.Label capCOMMISSION_PERCENT;
		protected System.Web.UI.WebControls.TextBox txtCOMMISSION_PERCENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMMISSION_PERCENT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMMISSION_PERCENT;
		protected System.Web.UI.WebControls.RangeValidator rngCOMMISSION_PERCENT;
		protected System.Web.UI.WebControls.CustomValidator csvCOMMISSION_PERCENT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLobXMLForClass;

		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		public string strCalledFrom="";
		ClsRegCommSetup_Agency  objRegCommSetup_Agency ;
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
		
			base.ScreenId="385_1";
			if(Request.QueryString["COMMISSION_TYPE"]!=null && Request.QueryString["COMMISSION_TYPE"].Length>0)
			{
				strCalledFrom = Request.QueryString["COMMISSION_TYPE"];
			}
			lblMessage.Visible = false;
			SetErrorMessages();

            //Added by Amit mishra for Tfs Bug #836
            if (GetLanguageID() == "3")
            {
                COUNTRYID = "7";
                COUNTRY_NAME = "Singapore";
            }

			#region Button Permissions

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass					=	CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;

			btnCopy.CmsButtonClass					=	CmsButtonType.Write;
			btnCopy.PermissionString				=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			#endregion

			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.CommCompAppBonus" ,System.Reflection.Assembly.GetExecutingAssembly());
			hlkTO_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_REG_COMM_SETUP.txtEFFECTIVE_TO_DATE,document.ACT_REG_COMM_SETUP.txtEFFECTIVE_TO_DATE)");
			hlkFROM_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_REG_COMM_SETUP.txtEFFECTIVE_FROM_DATE,document.ACT_REG_COMM_SETUP.txtEFFECTIVE_FROM_DATE)");
			txtCOMMISSION_PERCENT.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtCOMMISSION_PERCENT'));");
			btnCopy.Attributes.Add("onClick","javascript:CopyRecords();return false;");
			for(int i=1;i<rdblAMOUNT_TYPE.Items.Count;i++)
				rdblAMOUNT_TYPE.Items[i].Attributes.Add("onchange","ValidateAmount();");
			if(!Page.IsPostBack)
			{
				FillDropDowns();
				SetCaptions();
               
                string strSysID = GetSystemId();
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID, "CommCompAppBonus.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/CommCompAppBonus.xml");
				if(Request.QueryString["COMM_ID"] !=null)
					hidCOMM_ID.Value = Request.QueryString["COMM_ID"];
				if(hidCOMM_ID.Value !="" && hidCOMM_ID.Value !=null && hidCOMM_ID.Value !="0")
				{
					hidOldData.Value =  ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOMM_ID.Value);
					//Selecting the state id and then filling the associated line of busieness for that state
					
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(hidOldData.Value);
					
					System.Xml.XmlNode nod = doc.SelectSingleNode("/NewDataSet/Table/STATE_ID");
					if (nod != null)
					{
						cmbSTATE_ID.SelectedValue = nod.InnerText;
						cmbSTATE_ID_SelectedIndexChanged(null, null);
					//	hidFormSaved.Value = "1";
					}
					nod = null;
					doc = null;
				}
			}
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo=new Cms.BusinessLayer.BlApplication.ClsGeneralInformation(); 
			hidLobXMLForClass.Value = objGenInfo.GetLOBBYSTATEID().GetXml();
//			rdblAMOUNT_TYPE.Items.Add(new ListItem("Percentage(%)","P"));
//			rdblAMOUNT_TYPE.Items.Add(new ListItem("Fixed","F"));
			hidCOUNTRY_ID.Value = COUNTRYID;
			lblCOUNTRY_NAME.Text = COUNTRY_NAME;
		}//end pageload
		#endregion

		#region GetFormValue
		protected ClsRegCommSetup_AgencyInfo GetFormValue()
		{
			
			ClsRegCommSetup_AgencyInfo objRegCommSetup_AgencyInfo;
			objRegCommSetup_AgencyInfo = new ClsRegCommSetup_AgencyInfo();

			objRegCommSetup_AgencyInfo.COUNTRY_ID			=	int.Parse(hidCOUNTRY_ID.Value);
			objRegCommSetup_AgencyInfo.STATE_ID				=	int.Parse(cmbSTATE_ID.SelectedValue);
			if(hidLOB_ID.Value!=null && hidLOB_ID.Value.Length>0)
				objRegCommSetup_AgencyInfo.LOB_ID			=	int.Parse(hidLOB_ID.Value);
			//objRegCommSetup_AgencyInfo.LOB_ID				=	int.Parse(cmbLOB_ID.SelectedValue);
			objRegCommSetup_AgencyInfo.TERM					=	cmbTERM.SelectedValue;
			objRegCommSetup_AgencyInfo.EFFECTIVE_FROM_DATE	=	DateTime.Parse(txtEFFECTIVE_FROM_DATE.Text);
			objRegCommSetup_AgencyInfo.EFFECTIVE_TO_DATE	=	DateTime.Parse(txtEFFECTIVE_TO_DATE.Text);
			objRegCommSetup_AgencyInfo.AMOUNT_TYPE			=	rdblAMOUNT_TYPE.SelectedValue;
			objRegCommSetup_AgencyInfo.COMMISSION_TYPE		=	Request.QueryString["COMMISSION_TYPE"];
			objRegCommSetup_AgencyInfo.COMMISSION_PERCENT	=	double.Parse(txtCOMMISSION_PERCENT.Text);

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			oldXML			=	hidOldData.Value;
			strRowIden		=	hidCOMM_ID.Value;
			//Returning the model object
			return objRegCommSetup_AgencyInfo;
		}
		#endregion

		#region Event Handlers ( SAVE, ACTIVATE/DEACTIVATE, STATE SELECTEDINDEXCHANGED )
	
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				objRegCommSetup_Agency = new  ClsRegCommSetup_Agency(true);
				ClsRegCommSetup_AgencyInfo objRegCommSetup_AgencyInfo = GetFormValue();
				
				if(strRowIden.ToUpper().Equals("NEW")) //save case
				{
					objRegCommSetup_AgencyInfo.CREATED_BY = int.Parse(GetUserId());
					objRegCommSetup_AgencyInfo.CREATED_DATETIME = DateTime.Now;
					objRegCommSetup_AgencyInfo.MODIFIED_BY = objRegCommSetup_AgencyInfo.CREATED_BY;
					objRegCommSetup_AgencyInfo.LAST_UPDATED_DATETIME = objRegCommSetup_AgencyInfo.CREATED_DATETIME;
					objRegCommSetup_AgencyInfo.IS_ACTIVE = "Y";
	
					intRetVal = objRegCommSetup_Agency.Add(objRegCommSetup_AgencyInfo);
					if(intRetVal>0)
					{
						hidCOMM_ID.Value			= objRegCommSetup_AgencyInfo.COMM_ID.ToString();
						lblMessage.Text				= ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			= "1";
						hidIS_ACTIVE.Value			= "Y";
						hidOldData.Value			=  ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOMM_ID.Value);
						ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidCOMM_ID.Value + ");</script>");
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("883");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("883");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{
					ClsRegCommSetup_AgencyInfo objOldRegCommSetup_AgencyInfo;
					objOldRegCommSetup_AgencyInfo = new ClsRegCommSetup_AgencyInfo();
					base.PopulateModelObject(objOldRegCommSetup_AgencyInfo,hidOldData.Value);
					objRegCommSetup_AgencyInfo.COMM_ID = int.Parse(strRowIden);
					objRegCommSetup_AgencyInfo.MODIFIED_BY = int.Parse(GetUserId());
					objRegCommSetup_AgencyInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					intRetVal	= objRegCommSetup_Agency.Update(objOldRegCommSetup_AgencyInfo,objRegCommSetup_AgencyInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=   ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOMM_ID.Value);
						ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidCOMM_ID.Value + ");</script>");
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("883");
						hidFormSaved.Value		=	"2";
					}

					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("883");
						hidFormSaved.Value			=		"2";
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
			}
			finally
			{
				if(objRegCommSetup_Agency!= null)
					objRegCommSetup_Agency.Dispose();
			}
		}

	
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objRegCommSetup_Agency =  new ClsRegCommSetup_Agency();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objRegCommSetup_Agency.TransactionInfoParams = objStuTransactionInfo;
					objRegCommSetup_Agency.ActivateDeactivate(hidCOMM_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
					tbForm.Visible=false;
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objRegCommSetup_Agency.TransactionInfoParams = objStuTransactionInfo;
					objRegCommSetup_Agency.ActivateDeactivate(hidCOMM_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					tbForm.Visible=true;
				}
				hidOldData.Value =  ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOMM_ID.Value);
				hidFormSaved.Value			=	"1";
				
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
				if(objRegCommSetup_Agency!= null)
					objRegCommSetup_Agency.Dispose();
			}
		}


		private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			/*try
			{
				int stateID;
				
				Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo=new Cms.BusinessLayer.BlApplication.ClsGeneralInformation(); 
				DataSet dsLOB=new DataSet(); 
				stateID=cmbSTATE_ID.SelectedItem==null?-1:int.Parse(cmbSTATE_ID.SelectedItem.Value); 
				if(stateID!=-1)
				{
					
					dsLOB=objGenInfo.GetLOBBYSTATEID(stateID);
					cmbLOB_ID.DataSource=dsLOB;
					cmbLOB_ID.DataTextField="LOB_DESC";
					cmbLOB_ID.DataValueField="LOB_ID"; 
					cmbLOB_ID.DataBind(); 
					cmbLOB_ID.Items.Insert(0,"");
					hidState.Value =stateID.ToString();
				}
				
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}*/
		}
		#endregion

		#region Set Captions
		private void SetCaptions()
		{
			capSTATE_ID.Text								=		objResourceMgr.GetString("cmbSTATE_ID");
			capLOB_ID.Text									=		objResourceMgr.GetString("cmbLOB_ID");
			capTERM.Text									=		objResourceMgr.GetString("cmbTERM");
			capEFFECTIVE_FROM_DATE.Text						=		objResourceMgr.GetString("txtEFFECTIVE_FROM_DATE");
			capEFFECTIVE_TO_DATE.Text						=		objResourceMgr.GetString("txtEFFECTIVE_TO_DATE");
			capAMOUNT_TYPE.Text								=		objResourceMgr.GetString("rdblAMOUNT_TYPE");
            btnCopy.Text                                    =       objResourceMgr.GetString("btnCopy");
		}

		#endregion

		#region Set Validators ErrorMessages
		private void SetErrorMessages()
		{
			rfvSTATE_ID.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvLOB_ID.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvTERM.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvEFFECTIVE_FROM_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvEFFECTIVE_TO_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvAMOUNT_TYPE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			revCOMMISSION_PERCENT.ValidationExpression	=	aRegExpCurrencyformat;
			revEFFECTIVE_FROM_DATE.ValidationExpression	=	aRegExpDate;
			revEFFECTIVE_TO_DATE.ValidationExpression	=	aRegExpDate;
			revEFFECTIVE_FROM_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revEFFECTIVE_TO_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revCOMMISSION_PERCENT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			csvCHECK_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvCOMMISSION_PERCENT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("335");
		}
		#endregion

		#region "Fill DropDowns"
		private void FillDropDowns()
		{
			DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
			cmbSTATE_ID.DataSource		= dt;
			cmbSTATE_ID.DataTextField	= "State_Name";
			cmbSTATE_ID.DataValueField	= "State_Id";
			cmbSTATE_ID.DataBind();
			cmbSTATE_ID.Items.Insert(0,"");
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
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
		
		}

		
	}
}
