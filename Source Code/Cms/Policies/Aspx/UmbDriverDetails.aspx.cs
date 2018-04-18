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
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using System.Xml; 
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;
using Cms.Model.Diary;
using Cms.BusinessLayer.BlCommon;  

#endregion

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for UmbDriverDetails.
	/// </summary>
	public class UmbDriverDetails : Cms.Policies.policiesbase//System.Web.UI.Page	
	{

		
		#region Page controls declaration

		protected System.Web.UI.WebControls.TextBox txtDRIVER_FNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_MNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_LNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_DOB;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SSN;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_MART_STAT;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_SEX;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_DRIV_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_FNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_LNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SEX;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRIV_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_LICENSED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_DOB;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_SSN;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.ClientTop clientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMedLic;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState_id;
		
		#endregion

		#region Local form variables

		string oldXML;
		public string strWaiverBenefitsLimit = "60";
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected System.Web.UI.WebControls.Label capDRIVER_FNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_MNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_LNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_SUFFIX;
		protected System.Web.UI.WebControls.Label capDRIVER_DOB;
		protected System.Web.UI.WebControls.Label capDRIVER_SSN;
		protected System.Web.UI.WebControls.Label capDRIVER_MART_STAT;
		protected System.Web.UI.WebControls.Label capDRIVER_SEX;
		protected System.Web.UI.WebControls.Label capDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.Label capDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.Label capDRIVER_DRIV_TYPE;
		protected System.Web.UI.WebControls.Label capDRIVER_OCC_CODE;
		protected System.Web.UI.WebControls.Label capDRIVER_BROADEND_NOFAULT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionId;
		protected System.Web.UI.WebControls.HyperLink hlkOCCURENCE_DATE;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.CustomValidator csvDRIVER_DOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_INFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDiscTypeLen;

		ClsDriverDetail  objDriverDetail ;
		
		//protected System.Web.UI.WebControls.CustomValidator csvDATE_EXP_DOB;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnDATE_EXP_DOB;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		private const string CALLED_FROM_UMBRELLA="UMB";
		protected System.Web.UI.WebControls.Label capDISC_TYPE;    		
		protected System.Web.UI.WebControls.Label lblVehicleMsg;		
		protected System.Web.UI.WebControls.Label capDISC_DATE;
		protected System.Web.UI.WebControls.TextBox txtDISC_DATE;
		protected System.Web.UI.WebControls.HyperLink Hyperlink1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDISC_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvDISC_DATE;
		protected System.Web.UI.WebControls.Label capPERCENT_DRIVEN;
		protected System.Web.UI.WebControls.HyperLink hlkDiscount_DATE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehMsg;
		protected System.Web.UI.WebControls.Label capPremierDriver;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		public string strCalledFrom = "";
		public string strCalledFor = "";
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_DRIVER;
		protected System.Web.UI.WebControls.Label capDATE_LICENSED;
		protected System.Web.UI.WebControls.TextBox txtDATE_LICENSED;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_LICENSED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_LICENSED;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_LICENSED;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.Label lbl;
		protected System.Web.UI.HtmlControls.HtmlTableRow trOpVeh;
		protected System.Web.UI.WebControls.Label capOpVehicle;
		protected System.Web.UI.WebControls.Label capOpVEHICLE_ID;
		protected System.Web.UI.WebControls.Label capFORM_F95;
		protected System.Web.UI.WebControls.DropDownList cmbFORM_F95;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Table tblAssignedVeh; 
		protected System.Web.UI.WebControls.Label capVEHICLE_ID;
		protected System.Web.UI.WebControls.Label lblVEHICLE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.Label capAPP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehField;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnVEHICLE_ID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator2;
		protected System.Web.UI.HtmlControls.HtmlTableRow Tr1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Span1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSeletedData;
		protected System.Web.UI.WebControls.Label lblMotorMsg;
		protected System.Web.UI.WebControls.Label capMOT_VEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbMOT_VEHICLE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMOT_VEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbMOT_APP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.Label capMOT_APP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMOT_APP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.Label lblBoatMsg;
		protected System.Web.UI.WebControls.Label capOP_VEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbOP_VEHICLE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOP_VEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbOP_APP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.Label capOP_APP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOP_APP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMotorMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMotorField;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnMOT_VEHICLE_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBoatMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBoatField;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnOP_VEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_ID;		

		#endregion

		#region PageLoad event

		private void Page_Load(object sender, System.EventArgs e)
		{			

			#region setting screen id

			 if(Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
			{
				strCalledFrom =	Request.QueryString["CalledFrom"].ToString().ToUpper();

			}
			if(Request.QueryString["CalledFor"] != null && Request.QueryString["CalledFor"] != "")
			{
				strCalledFor =	Request.QueryString["CalledFor"];

			}
			base.ScreenId	=	"278_2";

			#endregion

			txtDRIVER_DOB.Attributes.Add("onBlur","javascript: CompareExpDateWithDOB();return EnableDisableControls();");
			txtDATE_LICENSED.Attributes.Add("onBlur","javascript: CompareExpDateWithDOB();");			
			btnSave.Attributes.Add("onClick","javascript: return SaveClientSide();");

			SetControlsAttributes();
			
			SetErrorMessages();

			
			#region Button Permissions
			
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;
			
			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			btnDelete.PermissionString =	gstrSecurityXML;

			#endregion

			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.UmbDriverDetails" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			if(!Page.IsPostBack)
			{
				GetQueryString();					
				int state_id=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.GetStateIdForpolicy(int.Parse(hidCUSTOMER_ID.Value.ToString()),hidPolicyId.Value.ToString(),hidPolicyVersionId.Value.ToString());
				hidState_id.Value =Convert.ToString(state_id);
//				fxnAssignedVehicle();
				GetDriverCount();
				GetOldDataXML();
				SetCaptions();
				PopulateComboBox();
				SetWorkFlowControl();

				DataSet dsTemp;
				dsTemp = ClsDriverDetail.FetchPolicyUmbrellaBoatInfo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyId.Value),int.Parse(hidPolicyVersionId.Value));
				rfvVEHICLE_ID.Enabled=false;
				rfvVEHICLE_DRIVER.Enabled=false;
				spnVEHICLE_ID.Attributes.Add("Style","Display:none");
				
			}
			SetWorkFlowControl();
			btnDelete.Enabled = false;
		}//end pageload
		#endregion

		#region Set Validators ErrorMessages

		private void SetErrorMessages()
		{
			rfvDRIVER_FNAME.ErrorMessage				=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"57");
			rfvDRIVER_LNAME.ErrorMessage				=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"58");
			rfvDRIVER_SEX.ErrorMessage					=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"189");
			rfvDRIVER_LIC_STATE.ErrorMessage			=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"111");
			rfvDRIVER_DRIV_TYPE.ErrorMessage			=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"112");
			revDRIVER_DOB.ValidationExpression			=	aRegExpDate;
			revDRIVER_SSN.ValidationExpression			=	aRegExpSSN;
			revDATE_LICENSED.ValidationExpression		=	aRegExpDate;
			revDRIVER_DOB.ErrorMessage					=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			revDRIVER_SSN.ErrorMessage					=   Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"130");
			revDATE_LICENSED.ErrorMessage				=   "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			csvDRIVER_DOB.ErrorMessage					=   "<br>" +  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"198");
			csvDATE_LICENSED.ErrorMessage				=   "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"199");
			//csvDATE_EXP_DOB.ErrorMessage				=	"<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("225");
			spnDATE_EXP_DOB.InnerHtml					=	"<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("225");
			rfvDRIVER_DOB.ErrorMessage					=	"<br>" +Cms.CmsWeb.ClsMessages.FetchGeneralMessage("162");
			rfvVEHICLE_ID.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvVEHICLE_DRIVER.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvDRIVER_DRIV_LIC.ErrorMessage				=	ClsMessages.FetchGeneralMessage("527");
			revDRIVER_DOB.ErrorMessage					=   "<br>" +ClsMessages.FetchGeneralMessage("179");
			rfvDATE_LICENSED.ErrorMessage				=	ClsMessages.FetchGeneralMessage("710");
			rfvVEHICLE_ID.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("562");
			rfvVEHICLE_DRIVER.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("563");
			rfvOP_APP_VEHICLE_PRIN_OCC_ID.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("563");			
			rfvMOT_APP_VEHICLE_PRIN_OCC_ID.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("563");			
			rfvOP_VEHICLE_ID.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("862");
			rfvMOT_VEHICLE_ID.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("863");			

		}
		#endregion

		#region Assigned Vehicle

//		protected void fxnAssignedVehicle()
//		{
//			int rowCnt;
//			int rowCtr;
//				
//			string strXML = ClsVehicleInformation.FillPolVehicle(int.Parse(hidCUSTOMER_ID.Value)
//				, int.Parse(hidPolicyId.Value)
//				, int.Parse(hidPolicyVersionId.Value)
//				, int.Parse(hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));
//
//			XmlDocument objXmlDoc = new XmlDocument();
//			objXmlDoc.LoadXml(strXML);
//				
//			int VehicleCount = objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Count;
//
//			if(VehicleCount < 1)
//			{
//				lblVehicleMsg.Text	=  "No vehicle added until now. Please click <a href='#' onclick='Redirect();'>here</a> to add vehicles";
//				trVehMsg.Attributes.Add ("style","display:inline;"); 
//				//trVehField.Attributes.Add ("style","display:none;"); 
//				//rfvVEHICLE_ID.Enabled=false;
//			}
//			else
//			{
//				//cmbVEHICLE_ID.Items.Insert(0,"");
//				trVehMsg.Attributes.Add ("style","display:none;"); 
//				//trVehField.Attributes.Add ("style","display:inline;"); 
//				//rfvVEHICLE_ID.Enabled=true;
//			}
//
//			rowCnt  = VehicleCount;
//				
//
//			IList objList = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHIDPO");
//
//			tblAssignedVeh.GridLines = GridLines.Both; 
//			tblAssignedVeh.Attributes.Add("TotalRows",VehicleCount.ToString());
//
//			for(rowCtr=1; rowCtr <= rowCnt; rowCtr++) 
//			{
//						
//				TableRow tRow = new TableRow();	
//				tRow.ID = "ID_" + rowCtr;
//				tblAssignedVeh.Rows.Add(tRow);		
//						
//				//Vech ID
//				//objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(0).InnerXml
//
//				//Model Make
//				//objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(1).InnerXml
//
//				//Assigned Vech ID
//				//objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(2).InnerXml
//
//				//Prin/Occ
//				//objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(3).InnerXml
//
//				TableCell tCellDrive  = new TableCell();
//				TableCell tCellVeh	  = new TableCell();
//				TableCell tCellAs     = new TableCell();
//				TableCell tCellDriver = new TableCell();
//
//				Label lblHidVehID	= new Label();
//				Label lblDrive		= new Label();
//				Label lblVehicle	= new Label();
//				Label lblAs			= new Label();
//				DropDownList drpDrv	= new DropDownList();
//
//				lblDrive.Text		= " Drive ";
//				lblHidVehID.Text	= objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(0).InnerXml;
//				lblAs.Text			= " as ";
//				lblVehicle.Text		= objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(1).InnerXml;
//				tRow.CssClass			= "midcolora";
//				tCellDrive.CssClass		= "midcolora";
//				tCellVeh.CssClass		= "midcolora";
//				tCellAs.CssClass		= "midcolora";
//				tCellDriver.CssClass	= "midcolora";
//						
//				tCellDrive.Width		= Unit.Percentage(10);
//				tCellVeh.Width			= Unit.Percentage(20);
//				tCellAs.Width			= Unit.Percentage(5);
//				tCellDriver.Width		= Unit.Percentage(32);
//
//				lblHidVehID.Visible	=  false;
//
//				drpDrv	= new DropDownList();
//				drpDrv.DataSource=objList;
//				drpDrv.DataTextField	= "LookupDesc";
//				drpDrv.DataValueField	= "LookupID";
//				drpDrv.DataBind();						
//
//				string strSelectedValInDrp = objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(3).InnerXml;
//				ListItem LI;
//				LI = drpDrv.Items.FindByValue(strSelectedValInDrp);
//				if (LI  != null)
//					LI.Selected=true;
//				else
//					drpDrv.SelectedIndex=-1;
//					
//				tCellDrive.Controls.Add (lblDrive);
//				tCellVeh.Controls.Add (lblVehicle);
//				tCellVeh.Controls.Add (lblHidVehID);
//				tCellAs.Controls.Add (lblAs);
//				tCellDriver.Controls.Add (drpDrv);
//
//				tRow.Cells.Add(tCellDrive);
//				tRow.Cells.Add(tCellVeh);
//				tRow.Cells.Add(tCellAs);
//				tRow.Cells.Add(tCellDriver);
//
//				tRow.Attributes.Add("RowVehID",lblHidVehID.Text);
//
//					
//			}
//		}


		#endregion

		#region Populate combo Box

		private void PopulateComboBox()
		{
			
			#region "Loading singleton"
			
			cmbDRIVER_LIC_STATE.DataSource = Cms.CmsWeb.ClsFetcher.State ;
			cmbDRIVER_LIC_STATE.DataTextField	= "State_Name";
			cmbDRIVER_LIC_STATE.DataValueField	= "State_Id";
			cmbDRIVER_LIC_STATE.DataBind();
			cmbDRIVER_LIC_STATE.Items.Insert(0,new ListItem("",""));
			cmbDRIVER_LIC_STATE.SelectedIndex =0;

			#endregion//Loading singleton

			cmbDRIVER_DRIV_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DRTCD");
			cmbDRIVER_DRIV_TYPE.DataTextField = "LookupDesc";
			cmbDRIVER_DRIV_TYPE.DataValueField = "LookupID";
			cmbDRIVER_DRIV_TYPE.DataBind();
			cmbDRIVER_DRIV_TYPE.Items.Insert(0,new ListItem("",""));
			cmbDRIVER_DRIV_TYPE.SelectedIndex=0;

			cmbDRIVER_MART_STAT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("Marst");
			cmbDRIVER_MART_STAT.DataTextField = "LookupDesc";
			cmbDRIVER_MART_STAT.DataValueField = "LookupCode";
			cmbDRIVER_MART_STAT.DataBind();
			cmbDRIVER_MART_STAT.Items.Insert(0,"");
			cmbDRIVER_MART_STAT.SelectedIndex=0;

			cmbFORM_F95.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbFORM_F95.DataTextField="LookupDesc"; 
			cmbFORM_F95.DataValueField="LookupID";
			cmbFORM_F95.DataBind();
			cmbFORM_F95.Items.Insert(0,"");

			cmbAPP_VEHICLE_PRIN_OCC_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataTextField	= "LookupDesc";
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataValueField	= "LookupID";
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataBind();
			cmbAPP_VEHICLE_PRIN_OCC_ID.Items.Insert(0,""); 
//			cmbAPP_VEHICLE_PRIN_OCC_ID.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHIDPO");
//			cmbAPP_VEHICLE_PRIN_OCC_ID.DataTextField="LookupDesc"; 
//			cmbAPP_VEHICLE_PRIN_OCC_ID.DataValueField="LookupID";
//			cmbAPP_VEHICLE_PRIN_OCC_ID.DataBind();
//			cmbAPP_VEHICLE_PRIN_OCC_ID.Items.Insert(0,"");

			cmbOP_APP_VEHICLE_PRIN_OCC_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");
			cmbOP_APP_VEHICLE_PRIN_OCC_ID.DataTextField	= "LookupDesc";
			cmbOP_APP_VEHICLE_PRIN_OCC_ID.DataValueField	= "LookupID";
			cmbOP_APP_VEHICLE_PRIN_OCC_ID.DataBind();
			cmbOP_APP_VEHICLE_PRIN_OCC_ID.Items.Insert(0,""); 

			cmbMOT_APP_VEHICLE_PRIN_OCC_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");
			cmbMOT_APP_VEHICLE_PRIN_OCC_ID.DataTextField	= "LookupDesc";
			cmbMOT_APP_VEHICLE_PRIN_OCC_ID.DataValueField	= "LookupID";
			cmbMOT_APP_VEHICLE_PRIN_OCC_ID.DataBind();
			cmbMOT_APP_VEHICLE_PRIN_OCC_ID.Items.Insert(0,""); 


//			ClsVehicleInformation.FillPolicyUmbrellaVehicleInfo(cmbVEHICLE_ID
//				, int.Parse(hidCUSTOMER_ID.Value)
//				, int.Parse(hidPolicyId.Value)
//				, int.Parse(hidPolicyVersionId.Value));

			DataSet dsAssign = ClsVehicleInformation.FillPolUmbrellaVehicleInfo(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value), int.Parse(hidPolicyVersionId.Value));
			if(dsAssign!=null && dsAssign.Tables.Count>0)
			{
				if(dsAssign.Tables[0]!=null && dsAssign.Tables[0].Rows.Count>0)
				{
					//Assigned Vehicle
					cmbVEHICLE_ID.DataSource =dsAssign.Tables[0];
					cmbVEHICLE_ID.DataTextField	= "MODEL_MAKE";
					cmbVEHICLE_ID.DataValueField	= "VEHICLE_ID";
					cmbVEHICLE_ID.DataBind();
					cmbVEHICLE_ID.Items.Insert(0,""); 
				}

				if(dsAssign.Tables[1]!=null && dsAssign.Tables[1].Rows.Count>0)
				{
					//Assigned Motor
					cmbMOT_VEHICLE_ID.DataSource =dsAssign.Tables[1];
					cmbMOT_VEHICLE_ID.DataTextField	= "MODEL_MAKE";
					cmbMOT_VEHICLE_ID.DataValueField	= "VEHICLE_ID";
					cmbMOT_VEHICLE_ID.DataBind();
					cmbMOT_VEHICLE_ID.Items.Insert(0,""); 
				}

				if(dsAssign.Tables[2]!=null && dsAssign.Tables[2].Rows.Count>0)
				{
					//Assigned Boat
					cmbOP_VEHICLE_ID.DataSource =dsAssign.Tables[2];;
					cmbOP_VEHICLE_ID.DataTextField	= "MODEL_MAKE";
					cmbOP_VEHICLE_ID.DataValueField	= "VEHICLE_ID";
					cmbOP_VEHICLE_ID.DataBind();
					cmbOP_VEHICLE_ID.Items.Insert(0,""); 		
				}
			}
		}

		#endregion

		#region GetFormValue
		
		private Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo GetFormValue()
		{
			Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objDriverDetailInfo = new Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo();

			objDriverDetailInfo.POLICY_ID = int.Parse(hidPolicyId.Value);
			objDriverDetailInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionId.Value);
			objDriverDetailInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objDriverDetailInfo.DRIVER_FNAME = txtDRIVER_FNAME.Text;
			objDriverDetailInfo.DRIVER_MNAME = txtDRIVER_MNAME.Text;
			objDriverDetailInfo.DRIVER_LNAME = txtDRIVER_LNAME.Text;
			objDriverDetailInfo.MODIFIED_BY	=	int.Parse(GetUserId());
			
			if (txtDRIVER_DOB.Text.Trim()!="")
				objDriverDetailInfo.DRIVER_DOB	=	ConvertToDate(txtDRIVER_DOB.Text);

			objDriverDetailInfo.DRIVER_SSN = txtDRIVER_SSN.Text;
			
			if(cmbDRIVER_MART_STAT.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_MART_STAT = cmbDRIVER_MART_STAT.SelectedValue;
			
			if(cmbDRIVER_SEX.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_SEX = cmbDRIVER_SEX.SelectedValue;
			
			     
			objDriverDetailInfo.DRIVER_DRIV_LIC = txtDRIVER_DRIV_LIC.Text;
			objDriverDetailInfo.DRIVER_LIC_STATE = cmbDRIVER_LIC_STATE.SelectedValue;
			
			if(cmbFORM_F95.SelectedValue !=null &&  cmbFORM_F95.SelectedValue != "")
				objDriverDetailInfo.FORM_F95 = int.Parse(cmbFORM_F95.SelectedValue);

			if(txtDATE_LICENSED.Text.Trim() != "")
				objDriverDetailInfo.DATE_LICENSED = ConvertToDate(txtDATE_LICENSED.Text);

			
			if(cmbDRIVER_DRIV_TYPE.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_DRIV_TYPE = cmbDRIVER_DRIV_TYPE.SelectedValue;
			
			if(objDriverDetailInfo.DRIVER_DRIV_TYPE=="11603")
			{
				if(cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Value!="") 
					objDriverDetailInfo.VEHICLE_ID=cmbVEHICLE_ID.SelectedItem.Value==""?Convert.ToInt32("0"):Convert.ToInt32(cmbVEHICLE_ID.SelectedValue);

				if(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem!=null && cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem.Value!="")
					objDriverDetailInfo.APP_VEHICLE_PRIN_OCC_ID = Convert.ToInt32(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedValue);  

				if(cmbOP_VEHICLE_ID.SelectedItem!=null && cmbOP_VEHICLE_ID.SelectedItem.Value!="")
					objDriverDetailInfo.OP_VEHICLE_ID = int.Parse(cmbOP_VEHICLE_ID.SelectedItem.Value);
				
				if(cmbOP_APP_VEHICLE_PRIN_OCC_ID.SelectedItem!=null && cmbOP_APP_VEHICLE_PRIN_OCC_ID.SelectedItem.Value!="")
					objDriverDetailInfo.OP_APP_VEHICLE_PRIN_OCC_ID = int.Parse(cmbOP_APP_VEHICLE_PRIN_OCC_ID.SelectedItem.Value);
	
				if(cmbMOT_VEHICLE_ID.SelectedItem!=null && cmbMOT_VEHICLE_ID.SelectedItem.Value!="")
					objDriverDetailInfo.MOT_VEHICLE_ID = int.Parse(cmbMOT_VEHICLE_ID.SelectedItem.Value);

				if(cmbMOT_APP_VEHICLE_PRIN_OCC_ID.SelectedItem!=null && cmbMOT_APP_VEHICLE_PRIN_OCC_ID.SelectedItem.Value!="")
					objDriverDetailInfo.MOT_APP_VEHICLE_PRIN_OCC_ID = int.Parse(cmbMOT_APP_VEHICLE_PRIN_OCC_ID.SelectedItem.Value);
			}
	
			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value!="New")
			{
				objDriverDetailInfo.DRIVER_ID=Int32.Parse(hidDRIVER_ID.Value);				
			}
			else
			{
				hidCustomInfo.Value=";Driver Name = " + txtDRIVER_FNAME.Text + " " + txtDRIVER_LNAME.Text;
			}

			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidDRIVER_ID.Value;
			oldXML		= hidOldData.Value;

			return objDriverDetailInfo;
		}

		#endregion

		#region "Save / ActivateDeactivate / Delete"
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objDriverDetail = new  ClsDriverDetail();
				objDriverDetail.LoggedInUserId	= int.Parse(GetUserId());
				//Retreiving the form values into model class object
				ClsPolicyDriverInfo objDriverDetailInfo = new ClsPolicyDriverInfo();
				Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objUmbrellaDriverDetailInfo = new Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo();					
				strRowId		=	hidDRIVER_ID.Value;								   
				if(hidDRIVER_ID.Value.ToUpper().Equals("NEW")) //save case
				{
						objUmbrellaDriverDetailInfo = GetFormValue();
						objUmbrellaDriverDetailInfo.CREATED_BY = objUmbrellaDriverDetailInfo.MODIFIED_BY = int.Parse(GetUserId());
						objUmbrellaDriverDetailInfo.CREATED_DATETIME = objUmbrellaDriverDetailInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
						/// Sumit Chhabra:03/12/2007
						/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
						intRetVal = objDriverDetail.AddPolicyUmbrellaDriverDetails(objUmbrellaDriverDetailInfo,hidCustomInfo.Value);
					
					if(intRetVal>0)
					{
						hidDRIVER_ID.Value		=	objUmbrellaDriverDetailInfo.DRIVER_ID.ToString();
						lblMessage.Text			= ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value		= "1";
						hidIS_ACTIVE.Value		= "Y";
						GetOldDataXML();
//						fxnAssignedVehicle();
						SetWorkFlowControl();
						int dateDiff=DateTime.Compare(System.DateTime.Now,Convert.ToDateTime(txtDRIVER_DOB.Text).AddYears(16));

						if(cmbDRIVER_DRIV_TYPE.SelectedValue=="3477"  )
							SetDiaryEntryForSetup("Ex");
							//SetDiaryEntryForExcluded();
						else if(cmbDRIVER_DRIV_TYPE.SelectedValue =="3478" && dateDiff != -1)
							SetDiaryEntryForSetup("Li");
							//SetDiaryEntryNotLiscned();

						//Opening the endorsement details page
						base.OpenEndorsementDetails();
						
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value		=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;

				} // end save case
				else //UPDATE CASE
				{

						//Creating the Model object for holding the Old data
						Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objOldUmbrellaDriverDetailInfo = new Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo();

						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldUmbrellaDriverDetailInfo,hidOldData.Value);
						objUmbrellaDriverDetailInfo = GetFormValue();
						objUmbrellaDriverDetailInfo.CREATED_BY = objUmbrellaDriverDetailInfo.MODIFIED_BY = int.Parse(GetUserId());
						objUmbrellaDriverDetailInfo.CREATED_DATETIME = objUmbrellaDriverDetailInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
						//Setting those values into the Model object which are not in the page
						objUmbrellaDriverDetailInfo.DRIVER_ID = int.Parse(hidDRIVER_ID.Value);
						/// Sumit Chhabra:03/12/2007
						/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
					
						objUmbrellaDriverDetailInfo.IS_ACTIVE = hidIS_ACTIVE.Value;						
						intRetVal	= objDriverDetail.UpdatePolicyUmbrellaDriver(objOldUmbrellaDriverDetailInfo,objUmbrellaDriverDetailInfo,hidCustomInfo.Value);
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";

						//Get old value of Driver Type						
						string strOldDriverType = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("DRIVER_DRIV_TYPE",hidOldData.Value);
						//Retreiving the old values in old xml
						GetOldDataXML();
//						fxnAssignedVehicle();
						SetWorkFlowControl();
						int dateDiff=DateTime.Compare(System.DateTime.Now,Convert.ToDateTime(txtDRIVER_DOB.Text).AddYears(16));
						//3478--Not Licensed..3477--Excluded Driver
						if(cmbDRIVER_DRIV_TYPE.SelectedValue=="3477" && cmbDRIVER_DRIV_TYPE.SelectedValue != strOldDriverType)
							SetDiaryEntryForSetup("Ex");
							//SetDiaryEntryForExcluded();
						else if(cmbDRIVER_DRIV_TYPE.SelectedValue =="3478"  && dateDiff != -1 &&  cmbDRIVER_DRIV_TYPE.SelectedValue != strOldDriverType)
							SetDiaryEntryForSetup("Li");
							//SetDiaryEntryNotLiscned();

						//Opening the endorsement details page
						base.OpenEndorsementDetails();
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
				if(objDriverDetail!= null)
					objDriverDetail.Dispose();
			}
			GetDriverCount();
		}
		
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				int qresult=0;
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				int intUserID = int.Parse(GetUserId());
				objDriverDetail =  new ClsDriverDetail();

				if(hidIS_ACTIVE.Value.ToString().Trim().ToUpper() == "Y")
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objDriverDetail.TransactionInfoParams = objStuTransactionInfo;
//					if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
					/// Sumit Chhabra:03/12/2007
					/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
						qresult  =  objDriverDetail.ActivateDeactivatePolicyDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value ), int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value), "N",intUserID,hidCustomInfo.Value);
//					else
//						qresult  =  objDriverDetail.ActivateDeactivateUmbrellaPolicyDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value ), int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value), "N",intUserID);
					if(qresult>0)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
						base.OpenEndorsementDetails();
					}
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objDriverDetail.TransactionInfoParams = objStuTransactionInfo;
					/// Sumit Chhabra:03/12/2007
					/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
//					if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
//						qresult  =  objDriverDetail.ActivateDeactivatePolicyDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value ), int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value), "Y",intUserID);
//					else
						qresult  =  objDriverDetail.ActivateDeactivateUmbrellaPolicyDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value ), int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value), "Y",intUserID,hidCustomInfo.Value);
					if(qresult>0)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
						hidIS_ACTIVE.Value="Y";
						base.OpenEndorsementDetails();
					}


					
				}
				
				//Opening the endorsement details page
				//hidFormSaved.Value			=	"1";
				hidFormSaved.Value			=	"0";
				
				//Generating the XML again
				GetOldDataXML();				

				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidDRIVER_ID.Value + ");</script>");

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
				if(objDriverDetail!= null)
					objDriverDetail.Dispose();
			}
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal = 0;	
			int intCustomerID = int.Parse(hidCUSTOMER_ID.Value);
			int intPolicyId=  int.Parse(hidPolicyId.Value);
			int intPolicyVersionId	= int.Parse(hidPolicyVersionId.Value);
			int intDriverId = int.Parse(hidDRIVER_ID.Value);
			int intUserID = int.Parse(GetUserId());
			string strCalledFrom=hidCalledFrom.Value;
			ClsDriverDetail objDriverDetail = new  ClsDriverDetail();
			/// Sumit Chhabra:03/12/2007
			/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
//			if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
				intRetVal = objDriverDetail.DeletePolicyDriver(intCustomerID,intPolicyId,intPolicyVersionId,intDriverId,intUserID, hidCustomInfo.Value);
//			else
//				intRetVal = objDriverDetail.DeletePolicyUmbrellaDriver(intCustomerID,intPolicyId,intPolicyVersionId,intDriverId,intUserID,hidCustomInfo.Value);
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
				SetWorkFlowControl();
				base.OpenEndorsementDetails();
			}
			else if(intRetVal == -1)
			{
				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblDelete.Visible = true;
		}
		#endregion

		#region Set Captions / Get Querystring / GetOldDataXML

		private void SetCaptions()
		{
			capDRIVER_FNAME.Text						=		objResourceMgr.GetString("txtDRIVER_FNAME");
			capDRIVER_MNAME.Text						=		objResourceMgr.GetString("txtDRIVER_MNAME");
			capDRIVER_LNAME.Text						=		objResourceMgr.GetString("txtDRIVER_LNAME");
			capDRIVER_DOB.Text							=		objResourceMgr.GetString("txtDRIVER_DOB");
			capDRIVER_SSN.Text							=		objResourceMgr.GetString("txtDRIVER_SSN");
			capDRIVER_MART_STAT.Text					=		objResourceMgr.GetString("cmbDRIVER_MART_STAT");
			capDRIVER_SEX.Text							=		objResourceMgr.GetString("cmbDRIVER_SEX");
			capDRIVER_DRIV_LIC.Text						=		objResourceMgr.GetString("txtDRIVER_DRIV_LIC");
			capDRIVER_LIC_STATE.Text					=		objResourceMgr.GetString("cmbDRIVER_LIC_STATE");
			capDATE_LICENSED.Text						=		objResourceMgr.GetString("txtDATE_LICENSED");
			capDRIVER_DRIV_TYPE.Text					=		objResourceMgr.GetString("cmbDRIVER_DRIV_TYPE");
			capVEHICLE_ID.Text							=		objResourceMgr.GetString("cmbVEHICLE_ID");  
			capAPP_VEHICLE_PRIN_OCC_ID.Text				=		objResourceMgr.GetString("cmbAPP_VEHICLE_PRIN_OCC_ID");  
			capMOT_APP_VEHICLE_PRIN_OCC_ID.Text			=		objResourceMgr.GetString("cmbAPP_VEHICLE_PRIN_OCC_ID");  			
			capMOT_VEHICLE_ID.Text						=		objResourceMgr.GetString("cmbVEHICLE_ID");  			
			capOP_APP_VEHICLE_PRIN_OCC_ID.Text			=		objResourceMgr.GetString("cmbOP_APP_VEHICLE_PRIN_OCC_ID");  			
			capOP_VEHICLE_ID.Text						=		objResourceMgr.GetString("cmbOP_VEHICLE_ID");  			
			capFORM_F95.Text							=		objResourceMgr.GetString("cmbFORM_F95"); 			
			
			
		}

		private void GetQueryString()
		{
			
			if (Request["CalledFrom"] != null && Request["CalledFrom"].ToString() != "")
				hidCalledFrom.Value		= Request.Params["CalledFrom"].ToString();
	
			if (Request["CUSTOMER_ID"] != null && Request["CUSTOMER_ID"].ToString() != "")
				hidCUSTOMER_ID.Value	= Request.Params["CUSTOMER_ID"].ToString();

			if (Request["POLICY_ID"] != null && Request["POLICY_ID"].ToString() != "")
				hidPolicyId.Value			= Request.Params["POLICY_ID"].ToString();

			if (Request["POLICY_VERSION_ID"] != null && Request["POLICY_VERSION_ID"].ToString() != "")
				hidPolicyVersionId.Value 	= Request.Params["POLICY_VERSION_ID"].ToString();

			if (Request["DRIVER_ID"] != null && Request["DRIVER_ID"].ToString() != "")
				hidDRIVER_ID.Value		= Request.Params["DRIVER_ID"];
			
						
		}

		private void GetOldDataXML()
		{
			if (hidDRIVER_ID.Value != "" && hidCUSTOMER_ID.Value != "")
			{				
					hidOldData.Value = Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetPolicyUmbrellaDriverDetailsXML(
						int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value) 
						, int.Parse(hidPolicyVersionId.Value), int.Parse(hidDRIVER_ID.Value));
			}
			else
			{
				hidOldData.Value = "";
			}
			if (hidCUSTOMER_ID.Value != "")
			{
				hidCUSTOMER_INFO.Value=Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetCustomerNameXML(int.Parse(hidCUSTOMER_ID.Value));
			}
		}


		#endregion

		#region Utility Functions

		private void SetDiaryEntryForExcluded()
		{
			TodolistInfo objTodo=new TodolistInfo();
			if(GetAppID()!="")
				objTodo.APP_ID=int.Parse(GetAppID());
			if(GetAppVersionID() !="")
				objTodo.APP_VERSION_ID =int.Parse(GetAppVersionID());
			objTodo.CUSTOMER_ID =int.Parse(hidCUSTOMER_ID.Value);
			objTodo.POLICY_ID =int.Parse(hidPolicyId.Value);
			objTodo.POLICY_VERSION_ID =int.Parse(hidPolicyVersionId.Value);
			objTodo.LISTTYPEID =12;
			objTodo.TOUSERID  =int.Parse(GetUserId());
			objTodo.CUSTOMER_NAME =GetUserName();
			objTodo.RECDATE =System.DateTime.Now;
			objTodo.PRIORITY ="M";
			objTodo.STARTTIME =System.DateTime.Now;
			objTodo.FOLLOWUPDATE =System.DateTime.Now;
			objTodo.ENDTIME  =  System.DateTime.Now;
			objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671");
			objTodo.NOTE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671") + " Driver Name = " + 
				txtDRIVER_FNAME.Text.Trim() + " " + txtDRIVER_LNAME.Text.Trim() + 
				", Date of Birth = " + txtDRIVER_DOB.Text.Trim();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary=new Cms.BusinessLayer.BlCommon.ClsDiary();
			try
			{
				int intResult=objDiary.AddPolicyEntry(objTodo);
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
					+ ex.Message + "\n Try again!";
					
				
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
		}

		/// <summary>
		/// This function is the new implementation of diary changes.
		/// </summary>
		/// <param name="whereString">this parameter is used for identifying whether the call has been made from
		/// ExcludedDriver or Not Licensed driver
		/// </param>
		private void SetDiaryEntryForSetup(string whereString)
		{
			TodolistInfo objTodo=new TodolistInfo();
			if(GetAppID()!="")
				objTodo.APP_ID=int.Parse(GetAppID());
			if(GetAppVersionID() !="")
				objTodo.APP_VERSION_ID =int.Parse(GetAppVersionID());
			objTodo.CUSTOMER_ID =int.Parse(hidCUSTOMER_ID.Value);
			objTodo.POLICY_ID =int.Parse(hidPolicyId.Value);
			objTodo.POLICY_VERSION_ID =int.Parse(hidPolicyVersionId.Value);
			
			objTodo.LISTTYPEID =(int)ClsDiary.enumDiaryType.FOLLOW_UPS;  
			objTodo.CUSTOMER_NAME =GetUserName();
			objTodo.RECDATE =System.DateTime.Now;
			objTodo.MODULE_ID=(int)ClsDiary.enumModuleMaster.POLICY;  
			objTodo.LISTOPEN ="Y";
			objTodo.FROMUSERID = int.Parse(GetUserId());
			objTodo.LOB_ID = int.Parse(GetLOBID()); 

			//if call has been made for excluded driver
			if(whereString.ToUpper().Equals("EX")) 
			{
				objTodo.NOTE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671") + " Driver Name = " + 
					txtDRIVER_FNAME.Text.Trim() + " " + txtDRIVER_LNAME.Text.Trim() + 
					", Date of Birth = " + txtDRIVER_DOB.Text.Trim();

				objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671");
			}
			//if call has been made for unlicensed driver
			else if (whereString.ToUpper().Equals("LI"))
			{
				objTodo.NOTE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672") + " Driver Name = " + 
					txtDRIVER_FNAME.Text.Trim() + " " + txtDRIVER_LNAME.Text.Trim() + 
					", Date of Birth = " + txtDRIVER_DOB.Text.Trim();

				objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672");
			}
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary=new Cms.BusinessLayer.BlCommon.ClsDiary();
			ArrayList alresult=new ArrayList(); 
			try
			{
				alresult=objDiary.DiaryEntryfromSetup(objTodo);
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
					+ ex.Message + "\n Try again!";
					
				
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}         

		}


		


		private void SetDiaryEntryNotLiscned()
		{
			TodolistInfo objTodo=new TodolistInfo();
			if(GetAppID()!="")
				objTodo.APP_ID=int.Parse(GetAppID());
			if(GetAppVersionID() !="")
				objTodo.APP_VERSION_ID =int.Parse(GetAppVersionID());
			objTodo.CUSTOMER_ID =int.Parse(hidCUSTOMER_ID.Value);
			objTodo.POLICY_ID =int.Parse(hidPolicyId.Value);
			objTodo.POLICY_VERSION_ID =int.Parse(hidPolicyVersionId.Value);
			objTodo.LISTTYPEID =12;
			objTodo.TOUSERID  =int.Parse(GetUserId());
			objTodo.CUSTOMER_NAME =GetUserName();
			objTodo.RECDATE =System.DateTime.Now;
			objTodo.PRIORITY ="M";
			objTodo.STARTTIME =System.DateTime.Now;
			objTodo.FOLLOWUPDATE =System.DateTime.Now;
			objTodo.ENDTIME  =  System.DateTime.Now;
			objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672");
			objTodo.NOTE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672") + " Driver Name = " + 
				txtDRIVER_FNAME.Text.Trim() + " " + txtDRIVER_LNAME.Text.Trim() + 
				", Date of Birth = " + txtDRIVER_DOB.Text.Trim();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary=new Cms.BusinessLayer.BlCommon.ClsDiary();
			try
			{
				int intResult=objDiary.AddPolicyEntry(objTodo);
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
					+ ex.Message + "\n Try again!";
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
		}

		
		private void GetDriverCount()
		{
			int driverCnt=Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetPolicyDriverCount(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value), int.Parse(hidPolicyVersionId.Value));
		}
	
		private void SetControlsAttributes()
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm1('" + Page.Controls[0].ID + "' );");
			hlkOCCURENCE_DATE.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtDRIVER_DOB, document.APP_DRIVER_DETAILS.txtDRIVER_DOB)");
			hlkDATE_LICENSED.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtDATE_LICENSED, document.APP_DRIVER_DETAILS.txtDATE_LICENSED)");
			
		}


		#endregion

		#region Workflow
		private void SetWorkFlowControl()
		{
			if(base.ScreenId == "278_2")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				if (Request.QueryString["DRIVER_ID"]!=null && Request.QueryString["DRIVER_ID"].ToString()!="")
				{
					myWorkFlow.AddKeyValue("DRIVER_ID",Request.QueryString["DRIVER_ID"]);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		#endregion

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
	}
}
