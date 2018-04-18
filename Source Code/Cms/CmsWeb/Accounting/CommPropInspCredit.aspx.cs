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
	
	public class CommPropInspCredit :  Cms.CmsWeb.cmsbase
	{
		#region "Control variables"
		protected System.Web.UI.WebControls.Label capSTATE_ID;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.Label capTERM;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.Label capCOMMISSION_PERCENT;
        protected System.Web.UI.WebControls.Label capMaindatory;
        protected System.Web.UI.WebControls.Label capCountry;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		
	
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbTERM;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.TextBox txtCOMMISSION_PERCENT;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMMISSION_PERCENT;
		protected System.Web.UI.WebControls.CustomValidator csvCOMMISSION_PERCENT;

		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOMMISSION_PERCENT;
		protected System.Web.UI.WebControls.Label lblMessage;
       
		#endregion

		#region Local form variables
		string oldXML;
		System.Resources.ResourceManager objResourceMgr;
		private string strRowIden, strFormSaved;
		protected System.Web.UI.WebControls.Label lblCOUNTRY_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOUNTRY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLobXMLForClass;

        //const string COUNTRYID = "1";//countryid hard coded for USA for save case //
        string COUNTRYID = "1";//Added by Amit Mishra for Tfs bug# 836

        protected System.Web.UI.WebControls.CustomValidator csvEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		protected System.Web.UI.WebControls.HyperLink hlkFROM_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTO_DATE;
     
        ///const string COUNTRY_NAME="USA";
        string COUNTRY_NAME = "USA";//Added By Amit Mishra for tfs bug #836 
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;//Added By Amit Mishra for tfs bug #836 
		
        protected System.Web.UI.HtmlControls.HtmlGenericControl tbForm;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		public string strCalledFrom="";
		ClsRegCommSetup_Agency  objRegCommSetup_Agency ;
		#endregion

		

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			base.ScreenId="180_0";
			if(Request.QueryString["COMMISSION_TYPE"]!=null && Request.QueryString["COMMISSION_TYPE"].Length>0)
			{
				strCalledFrom = Request.QueryString["COMMISSION_TYPE"];
			}
			lblMessage.Visible = false;
			

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
            //Added by Amit mishra for Tfs Bug #836
            if (GetLanguageID() == "3")
            {
                COUNTRYID = "7";
                COUNTRY_NAME = "Singapore";
            }
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.AddRegCommSetup_Agency" ,System.Reflection.Assembly.GetExecutingAssembly());
			hlkTO_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_REG_COMM_SETUP.txtEFFECTIVE_TO_DATE,document.ACT_REG_COMM_SETUP.txtEFFECTIVE_TO_DATE)");
			hlkFROM_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_REG_COMM_SETUP.txtEFFECTIVE_FROM_DATE,document.ACT_REG_COMM_SETUP.txtEFFECTIVE_FROM_DATE)");
			btnCopy.Attributes.Add("onClick","javascript:CopyRecords();return false;");
			if(!Page.IsPostBack)
			{
                FillDropDowns();
                SetErrorMessages();
                SetCaptions();

                string strSysID = GetSystemId();
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID, "CommPropInspCredit.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/CommPropInspCredit.xml");

                
				if(Request.QueryString["COMM_ID"] !=null)
					hidCOM_ID.Value = Request.QueryString["COMM_ID"];
				
                if(hidCOM_ID.Value !="" && hidCOM_ID.Value !=null && hidCOM_ID.Value!="0")
				{
					hidOldData.Value =  ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOM_ID.Value);
                   //Added By Amit k mishra for tfs Bug #836
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(hidOldData.Value);
                    System.Xml.XmlNodeList xnList = doc.SelectNodes("/NewDataSet/Table");

                    foreach (System.Xml.XmlNode xn in xnList)
                    {
                        cmbSTATE_ID.SelectedValue = xn["STATE_ID"].InnerText;
                        cmbLOB_ID.SelectedValue = xn["LOB_ID"].InnerText;
                    }


                    //System.Xml.XmlNode nod = doc.SelectSingleNode("NewDataSet/Table/STATE_ID");
                    //if (nod != null)
                    //{
                    //    cmbSTATE_ID.SelectedValue = nod.InnerText;
                        
                    //}
                    //System.Xml.XmlNode LOBnod = doc.SelectSingleNode("NewDataSet/Table/LOB_ID");
                    //if(LOBnod!=null)
                    //{
                    //    cmbLOB_ID.SelectedValue = LOBnod.InnerText;
                    //}
                    //nod = null;
                    //LOBnod = null;
                    doc = null;

				}
				
              
			}
            Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
            hidLobXMLForClass.Value = objGenInfo.GetLOBBYSTATEID().GetXml();
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
            if (hidLOB_ID.Value != null && hidLOB_ID.Value.Length > 0)//Added By Amit Mishra for Tfs bug 836
                objRegCommSetup_AgencyInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
            //objRegCommSetup_AgencyInfo.LOB_ID				=	int.Parse(cmbLOB_ID.SelectedValue);//changed By Amit k mishra
			objRegCommSetup_AgencyInfo.TERM					=	cmbTERM.SelectedValue;
			objRegCommSetup_AgencyInfo.EFFECTIVE_FROM_DATE	=	DateTime.Parse(txtEFFECTIVE_FROM_DATE.Text);
			objRegCommSetup_AgencyInfo.EFFECTIVE_TO_DATE	=	DateTime.Parse(txtEFFECTIVE_TO_DATE.Text);
			objRegCommSetup_AgencyInfo.COMMISSION_PERCENT	=	double.Parse(txtCOMMISSION_PERCENT.Text);
			objRegCommSetup_AgencyInfo.COMMISSION_TYPE		=	Request.QueryString["COMMISSION_TYPE"];
			objRegCommSetup_AgencyInfo.REMARKS				=	txtREMARKS.Text;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			oldXML			=	hidOldData.Value;
			strRowIden		=	hidCOM_ID.Value;
			//Returning the model object
			return objRegCommSetup_AgencyInfo;
		}
		#endregion

		#region "Web Event Handlers ( SAVE, ACTIVATE/DEACTIVATE)"
	
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
						hidCOM_ID.Value				= objRegCommSetup_AgencyInfo.COMM_ID.ToString();
                        hidLOB_ID.Value = objRegCommSetup_AgencyInfo.LOB_ID.ToString();
						lblMessage.Text				= ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			= "1";
						hidIS_ACTIVE.Value			= "Y";
						hidOldData.Value			=  ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOM_ID.Value);
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
						hidOldData.Value		=   ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOM_ID.Value);

                        hidLOB_ID.Value = objRegCommSetup_AgencyInfo.LOB_ID.ToString();
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
					objRegCommSetup_Agency.ActivateDeactivate(hidCOM_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
					tbForm.Visible=false;
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objRegCommSetup_Agency.TransactionInfoParams = objStuTransactionInfo;
					objRegCommSetup_Agency.ActivateDeactivate(hidCOM_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					tbForm.Visible=true;
				}
				hidOldData.Value =  ClsRegCommSetup_Agency.GetXmlForPageControls(hidCOM_ID.Value);
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
		#endregion

		#region Set Captions
		private void SetCaptions()
		{
			capSTATE_ID.Text								=		objResourceMgr.GetString("cmbSTATE_ID");
			capLOB_ID.Text									=		objResourceMgr.GetString("cmbLOB_ID");
			capTERM.Text									=		objResourceMgr.GetString("cmbTERM");
			capEFFECTIVE_FROM_DATE.Text						=		objResourceMgr.GetString("txtEFFECTIVE_FROM_DATE");
			capEFFECTIVE_TO_DATE.Text						=		objResourceMgr.GetString("txtEFFECTIVE_TO_DATE");
			capREMARKS.Text									=		objResourceMgr.GetString("txtREMARKS");
			capCOMMISSION_PERCENT.Text						=		objResourceMgr.GetString("txtCOMMISSION_PERCENT");
            capMaindatory.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            btnCopy.Text = objResourceMgr.GetString("btnCopy");
            capCountry.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2011");
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
			rfvCOMMISSION_PERCENT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			
			revEFFECTIVE_FROM_DATE.ValidationExpression	=	aRegExpDate;
			revEFFECTIVE_TO_DATE.ValidationExpression	=	aRegExpDate;
			revCOMMISSION_PERCENT.ValidationExpression	=	aRegExpCurrencyformat;
			revEFFECTIVE_FROM_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revEFFECTIVE_TO_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revCOMMISSION_PERCENT.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

            csvEFFECTIVE_TO_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
			csvCOMMISSION_PERCENT.ErrorMessage          =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
		}
		#endregion

		#region "Fill DropDowns"
		private void FillDropDowns()
		{
            /*Changed By Amit Mishra For Tfs Bug #836
             *cmbSTATE_ID.Items.Insert(0,"");
            cmbSTATE_ID.Items.Insert(1,"Indiana");
            cmbSTATE_ID.Items.FindByText("Indiana").Value = (((int)enumState.Indiana).ToString());
            cmbSTATE_ID.Items.Insert(2,"Michigan");
            cmbSTATE_ID.Items.FindByText("Michigan").Value = (((int)enumState.Michigan).ToString());
            */
            DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
            cmbSTATE_ID.DataSource = dt;
            cmbSTATE_ID.DataTextField = "State_Name";
            cmbSTATE_ID.DataValueField = "State_Id";
            cmbSTATE_ID.DataBind();
            cmbSTATE_ID.Items.Insert(0, "");

            //cmbLOB_ID.Items.Insert(0,"");
            //cmbLOB_ID.Items.Insert(1,"Homeowners");
            //cmbLOB_ID.Items.FindByText("Homeowners").Value = (((int)enumLOB.HOME).ToString());
            //cmbLOB_ID.Items.Insert(2,"Rental");
            //cmbLOB_ID.Items.FindByText("Rental").Value = (((int)enumLOB.REDW).ToString());

            cmbTERM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TERM");
            cmbTERM.DataTextField = "LookupDesc";
            cmbTERM.DataValueField = "LookupCode";
            cmbTERM.DataBind();
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
