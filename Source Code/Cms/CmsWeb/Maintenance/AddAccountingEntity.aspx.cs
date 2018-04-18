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
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance; 
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;

/******************************************************************************************
	<Author					: Gaurav Tyagi- >
	<Start Date				: 14 April, 2005-	>
	<End Date				: 26 April, 2005- >
	<Description			: This file is used to add Accounting Entity details,show Accounting Entity details, update Accounting Entity details - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - 26/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page 
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// This class is used to add User details,show Accounting Entity details, update Accounting Entity details.
	/// </summary>
	public class AddAccountingEntity : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeptId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidProfitCenterId;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROFIT_CENTER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPT;
		protected System.Web.UI.WebControls.Label capEntityName;
		protected System.Web.UI.WebControls.Label lblEntityName;
        protected System.Web.UI.WebControls.Label capMARKEDFIELD; 
		protected System.Web.UI.WebControls.Label capDivison;
		protected System.Web.UI.WebControls.DropDownList cmbDivision;
		protected System.Web.UI.WebControls.Label capDepartment;
		protected System.Web.UI.WebControls.DropDownList cmbDepartment;
		protected System.Web.UI.WebControls.Label capProfitCenter;
		protected System.Web.UI.WebControls.DropDownList cmbProfitCenter;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
	

		#region Form Variable to Hold valid controls values

		// Creating object of ResourceManager Class
		ResourceManager objResorceManager;
		private const string strShowAll="N";
		//private int intRecordId;
		//private int intDivisionId;
		//private int intDepartmentId;
		//private int intProfitCenterId;
		//private string strIsActive;
		private int intLoggedInUser;
		//DateTime dtCreateDateTime;
		//DateTime dtUpdateDateTime;
		private string strEntityId; // Holds value for Entity Id
		private string strEntityType;	// Holds value for Entity Type
		private string strEntityName;	// Holds value for Entity Name

		private string	strRowId;
		private string strFormSaved;
		string strPageMode=""; // Holds Mode for the page
		private string strOldXml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDepartmentXml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidProfitCenterXml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected System.Web.UI.WebControls.CustomValidator cvDivision;
		protected System.Web.UI.WebControls.CustomValidator cvDepartment;
		protected System.Web.UI.WebControls.CustomValidator cvProfitCenter;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		private string strCalledFrom="";

		ClsAccountingEntity objAccountingEntity ;

		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			intLoggedInUser	= int.Parse(base.GetUserId());

			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			switch(strCalledFrom)
			{
				case "fin" :
				case "FIN" :
					base.ScreenId	=	"35_1_0";
					break;
				case "tax" :
				case "TAX" :
					base.ScreenId	=	"36_1_0";
					break;
				default :
					base.ScreenId	=	"36_1_0";
					break;
			}
			#endregion
            
			//Response.Write("<Script> Var deptXml='"+ClsDivision.GetXmlForDropDown()+"';</Script>");
			hidDepartmentXml.Value = ClsDivision.GetXmlForDepartmentDropDown();
			hidProfitCenterXml.Value = ClsDepartment.GetXmlForProfitCenterDropDown();
			//Calling Javascript function this will reset form, in edit mode
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			cmbDivision.Attributes.Add("OnChange","javascript:FillDepartment();");
			cmbDepartment.Attributes.Add("OnChange","javascript:FillProfitCenter();");
			cmbProfitCenter.Attributes.Add("OnChange","javascript:GetProfitCenterId();");
			
			SetErrorMessages();

			// Getting Entity Id and Type from query string variables
			strEntityId =  Request.Params["EntityId"];
			strEntityType = Request.Params["EntityType"];
			strEntityName = Request.Params["EntityName"];
			lblEntityName.Text = strEntityName;


			// Put user code to initialize the page here
			btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnReset.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;


			btnDelete.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;


			objResorceManager = new ResourceManager("Cms.Cmsweb.Maintenance.AddAccountingEntity",Assembly.GetExecutingAssembly());
           
			if(hidREC_ID.Value != null && hidREC_ID.Value.ToString().Length > 0)
				strPageMode = "Edit";
			else
				strPageMode = "Add";

			if(!Page.IsPostBack)
			{
				try
				{
					SetCaptions();
                  	ClsDivision.GetDivisionDropDown(cmbDivision);
                    cmbDivision.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "Selecione Divisão" : "Select Division"));
					//ClsDepartment.GetDepartmentDropDown(cmbDepartment);
					//ClsProfitCentre.GetProfitCenterDropDown(cmbProfitCenter);

					if(Request.QueryString["REC_ID"]!=null && Request.QueryString["REC_ID"].ToString().Length>0)
					{
						SetXml(Request.QueryString["REC_ID"],Request.QueryString["ENTITY_TYPE"]);
						strPageMode = "Edit";
					}
					else if(hidREC_ID.Value != null && hidREC_ID.Value.ToString().Length > 0)
					{
						SetXml(hidREC_ID.Value.ToString(),strEntityType);
						strPageMode = "Edit";
					}
					else
						strPageMode = "Add";
					 
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}

              
			}
           
		}
		private void SetErrorMessages()
		{			
			cvDivision.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"501");
			cvDepartment.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"502");
			cvProfitCenter.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"503");
		}		

		private void SetXml(string strAccountEntityId,string strEntityType)
		{
			hidOldData.Value = ClsAccountingEntity.GetXmlForPageControls(strAccountEntityId,strEntityType);
		}

		#region Set Captions

		private void SetCaptions()
		{
			capEntityName.Text		=	objResorceManager.GetString("lblEntityName");
			capDivison.Text			=	objResorceManager.GetString("cmbDivision");
			capDepartment.Text		=	objResorceManager.GetString("cmbDepartment");
			capProfitCenter.Text	=	objResorceManager.GetString("cmbProfitCenter");
            capMARKEDFIELD.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); 
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
            hidPROFIT_CENTER.Value = objResorceManager.GetString("hidPROFIT_CENTER");
            hidDEPT.Value = objResorceManager.GetString("hidDEPT");
		}
		#endregion

		private ClsAccountingInfo GetFormValues()
		{
			ClsAccountingInfo objAccountingEntityInfo  = new ClsAccountingInfo();
			//objAccountingEntityInfo.ENTITY_ID	=	"";
			//objAccountingEntityInfo.ENTITY_TYPE =  "";
			objAccountingEntityInfo.DIVISION_ID = int.Parse(cmbDivision.SelectedValue.Trim().ToString());
			objAccountingEntityInfo.DEPARTMENT_ID = int.Parse(hidDeptId.Value.ToString().Trim().Replace("'","''"));				//int.Parse(cmbDepartment.SelectedValue.Trim().ToString());
			objAccountingEntityInfo.PROFIT_CENTER_ID = int.Parse(hidProfitCenterId.Value.ToString().Trim().Replace("'","''"));	//int.Parse(cmbProfitCenter.SelectedValue.Trim().ToString());
			objAccountingEntityInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
			//objAccountingEntityInfo.CREATED_BY = intLoggedInUserID;
			//objAccountingEntityInfo.MODIFIED_BY = intLoggedInUserID;
			strFormSaved	=  hidFormSaved.Value;
			strRowId		=		hidREC_ID.Value;
			strOldXml		= hidOldData.Value;
			objAccountingEntityInfo.ENTITY_ID = int.Parse(strEntityId.ToString());
			objAccountingEntityInfo.ENTITY_TYPE= strEntityType;
			return objAccountingEntityInfo;

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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Methods to do form processing

		private void SaveFormValues()
		{
			try
			{
				ClsAccountingInfo objAccountingEntityInfo = GetFormValues();
				objAccountingEntity = new Cms.BusinessLayer.BlCommon.ClsAccountingEntity(true);
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					//Mapping feild and Lebel to maintain the transction log into the database.
					//objAccountingEntityInfo.TransactLabel = MapTransactionLabel(objResorceManager,this);
					//objAccountingEntityInfo.TransactLabel = MapTransactionLabel("AddAccountingEntity.aspx.resx");

					//Setting properties which do not corresponds to page controls.

					objAccountingEntityInfo.IS_ACTIVE = "Y";
					objAccountingEntityInfo.MODIFIED_BY = objAccountingEntityInfo.CREATED_BY = intLoggedInUser;
					objAccountingEntityInfo.CREATED_DATETIME = objAccountingEntityInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					if(objAccountingEntity.Add(objAccountingEntityInfo)>0)
					{
						hidREC_ID.Value = objAccountingEntity.RecordId.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						SetXml(hidREC_ID.Value.ToString(),strEntityType);
						hidIS_ACTIVE.Value = "Y";
					}
					else if( objAccountingEntity.RecordId == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
						btnActivateDeactivate.Enabled = false;
					}
					else
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{
					int intReturnValue;
					ClsAccountingInfo objOldAccountingInfo = new ClsAccountingInfo();
					//Setting  the Old Page details(XML File containing old details) into the Model Object

					//Mapping feild and Lebel to maintain the transction log into the database.
					//objAccountingEntityInfo.TransactLabel	=	MapTransactionLabel(objResorceManager,this);
                    /*==========================================================
                    * SANTOSH GAUTAM : BELOW LINE MODIFIED ON 28 OCT 2010
                    * 1. OLD VALUE =>objNewMortgage.TransactLabel = MapTransactionLabel("AddAccountingEntity.aspx.resx");
                    *==========================================================*/
                    objAccountingEntityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/AddAccountingEntity.aspx.resx");
					base.PopulateModelObject(objOldAccountingInfo,strOldXml);

					objOldAccountingInfo.REC_ID = objAccountingEntityInfo.REC_ID = int.Parse(hidREC_ID.Value);
					objAccountingEntityInfo.MODIFIED_BY = intLoggedInUser;
					objAccountingEntityInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					intReturnValue				=	objAccountingEntity.Update(objAccountingEntityInfo,objOldAccountingInfo);
					if(intReturnValue>0)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						SetXml(hidREC_ID.Value.ToString(),strEntityType);
					}
					else if(intReturnValue == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				
				}
					
			}
			catch(Exception ex)
			{
				hidFormSaved.Value			=	"2";
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+" - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				//ExceptionManager.Publish(ex);
			}
			finally
			{

			}
		}

		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFormValues();
		}
		
		#region activate deactivate
		/// <summary>
		/// This function is used to give activate and deactivate functionality.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			
			Cms.BusinessLayer.BlCommon.ClsAccountingEntity objAccountingEntity    = new Cms.BusinessLayer.BlCommon.ClsAccountingEntity();
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				
				objStuTransactionInfo.loggedInUserName = GetUserName();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Accounting Entity Deactivated Succesfully.";
					objAccountingEntity.TransactionInfoParams = objStuTransactionInfo;
					objAccountingEntity.ActivateDeactivate(hidREC_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Accounting Entity Activated Succesfully.";
					objAccountingEntity.TransactionInfoParams = objStuTransactionInfo;
					objAccountingEntity.ActivateDeactivate(hidREC_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
//				GenerateXML(hidLOSS_ID.Value);
				hidFormSaved.Value			=	"0";
				SetXml(hidREC_ID.Value.ToString(),strEntityType);
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objAccountingEntity!= null)
					objAccountingEntity.Dispose();
			}
		}

		#endregion

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			int intRecID = int.Parse(hidREC_ID.Value);
			objAccountingEntity = new Cms.BusinessLayer.BlCommon.ClsAccountingEntity();
			intRetVal = objAccountingEntity.Delete(intRecID);
			if(intRetVal>0)
			{
				lblMessage.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
				hidFormSaved.Value	 =	"5";
				hidOldData.Value	 =  "";
			}
			else if(intRetVal == -1)
			{
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblMessage.Visible = true;

		}
	}
}
