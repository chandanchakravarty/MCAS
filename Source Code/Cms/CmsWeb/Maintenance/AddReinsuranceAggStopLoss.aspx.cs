/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for Aggregate Stop Loss for a reinsurance contract. 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/


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
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms;
using Cms.CmsWeb.Controls;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddReinsuranceAggStopLoss.
	/// </summary>
	public class AddReinsuranceAggStopLoss : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N
		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capREINSURANCE_COMPANY;
		protected System.Web.UI.WebControls.Label capACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.Label capCONTRACT_NUMBER;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capEXPIRATION_DATE;
		protected System.Web.UI.WebControls.Label capLINE_OF_BUSINESS;
		protected System.Web.UI.WebControls.Label capCOVERAGE_CODE;
		protected System.Web.UI.WebControls.Label capCLASS_CODE;
		protected System.Web.UI.WebControls.Label capPERIL;
		protected System.Web.UI.WebControls.Label capSTATED_AMOUNT;
		protected System.Web.UI.WebControls.Label capSPECIFIED_LOSS_RATIO;

		protected System.Web.UI.WebControls.TextBox txtACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCONTRACT_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtEXPIRATION_DATE;
		protected System.Web.UI.WebControls.TextBox txtCOVERAGE_CODE;
		protected System.Web.UI.WebControls.TextBox txtCLASS_CODE;
		protected System.Web.UI.WebControls.TextBox txtSTATED_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtSPECIFIED_LOSS_RATIO;

		protected System.Web.UI.WebControls.DropDownList cmbREINSURANCE_COMPANY;
		protected System.Web.UI.WebControls.DropDownList cmbLINE_OF_BUSINESS;
		protected System.Web.UI.WebControls.DropDownList cmbPERIL;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREINSURANCE_COMPANY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPIRATION_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLINE_OF_BUSINESS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCLASS_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPERIL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATED_AMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSPECIFIED_LOSS_RATIO;

		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEXPIRATION_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSTATED_AMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSPECIFIED_LOSS_RATIO;

		protected System.Web.UI.WebControls.CustomValidator csvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvEXPIRATION_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvSPECIFIED_LOSS_RATIO;
		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTRACT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOVERAGE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACCOUNT_NUMBER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGGREGATE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLASS_CODE;

		protected System.Web.UI.WebControls.HyperLink hlkEffectiveDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationDate;

		protected System.Web.UI.HtmlControls.HtmlImage imgClassCode;
		protected System.Web.UI.HtmlControls.HtmlImage imgAccountNumber;
		protected System.Web.UI.HtmlControls.HtmlImage imgCoverageCode;
		
		ClsAddReinsuranceAggStopLoss objAddReinsuranceAggStopLoss;
		System.Resources.ResourceManager objResourceMgr;

		private string strFormSaved, strAggregate_ID;
		protected string strContract_Name = "";
		
		
		protected string oldXML;
        
		# endregion 

		# region P A G E   L O A D 

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				objAddReinsuranceAggStopLoss = new ClsAddReinsuranceAggStopLoss();
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

				hlkEffectiveDate.Attributes.Add("OnClick","fPopCalendar(document.MNT_REINSURANCE_AGGREGATE.txtEFFECTIVE_DATE,document.MNT_REINSURANCE_AGGREGATE.txtEFFECTIVE_DATE)");
				hlkExpirationDate.Attributes.Add("OnClick","fPopCalendar(document.MNT_REINSURANCE_AGGREGATE.txtEXPIRATION_DATE,document.MNT_REINSURANCE_AGGREGATE.txtEXPIRATION_DATE)");
				
				//For Coverage Code LookUP 				
				string url = ClsCommon.GetLookupURL();
				imgCoverageCode.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','COVERAGE_ID','COVERAGE_CODE','hidCOVERAGE_ID','txtCOVERAGE_CODE','CoverageCode','Coverage Code','');");

				//For Account Number LookUP
				imgAccountNumber.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','REIN_COMAPANY_ID','REIN_COMAPANY_ACC_NUMBER','hidACCOUNT_NUMBER_ID','txtACCOUNT_NUMBER','ReinsuranceAccountNumber','Reinsurance Account Number','');");						

				//For Class Code LookUP
				imgClassCode.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','CLASS_CODE_ID','CLASS_CODE','hidCLASS_CODE','txtCLASS_CODE','ClassCode','Class Code','');");						

				base.ScreenId = "262_3";
				lblMessage.Visible = false;
				SetErrorMessages();

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass		=	CmsButtonType.Execute;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnSave.CmsButtonClass		=	CmsButtonType.Execute;
				btnSave.PermissionString	=	gstrSecurityXML;

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddReinsuranceAggStopLoss" ,System.Reflection.Assembly.GetExecutingAssembly());

				if(!Page.IsPostBack)
				{
					if(Request.QueryString["CONTRACT_ID"]!=null && Request.QueryString["CONTRACT_ID"].ToString().Length>0)
					{
						hidCONTRACT_ID.Value = Request.QueryString["CONTRACT_ID"].ToString();
						GetOldData();
					}
					
					SetCaptions();

					FillCombos();

					GetDataForEditMode();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);			
			}
		}

		# endregion 

		# region  S E T   P A G E   V A L I D A T I O N S   E R R O R   M E S S A G E S 

		private void SetErrorMessages()
		{
			try
			{
				rfvREINSURANCE_COMPANY.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				rfvACCOUNT_NUMBER.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
				rfvEFFECTIVE_DATE.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
				rfvEXPIRATION_DATE.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
				rfvLINE_OF_BUSINESS.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
				rfvCOVERAGE_CODE.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
				rfvCLASS_CODE.ErrorMessage						= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
				rfvPERIL.ErrorMessage							= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
				rfvSTATED_AMOUNT.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
				rfvSPECIFIED_LOSS_RATIO.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
				
				revEFFECTIVE_DATE.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
				revEXPIRATION_DATE.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
				revEFFECTIVE_DATE.ValidationExpression			=  aRegExpDate;
				revEXPIRATION_DATE.ValidationExpression 		=  aRegExpDate;
				revSTATED_AMOUNT.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
				revSTATED_AMOUNT.ValidationExpression			= aRegExpDoublePositiveNonZero;
				revSPECIFIED_LOSS_RATIO.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
				revSPECIFIED_LOSS_RATIO.ValidationExpression	= aRegExpDoublePositiveNonZero;

				csvEFFECTIVE_DATE.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
				csvEXPIRATION_DATE.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
				csvSPECIFIED_LOSS_RATIO.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion

		# region  S E T   L A B E L   C A P T I O N S 

		private void SetCaptions()
		{
			try
			{
				capREINSURANCE_COMPANY.Text		= objResourceMgr.GetString("cmbREINSURANCE_COMPANY");
				capACCOUNT_NUMBER.Text			= objResourceMgr.GetString("txtACCOUNT_NUMBER");
				capCONTRACT_NUMBER.Text			= objResourceMgr.GetString("txtCONTRACT_NUMBER");
				capEFFECTIVE_DATE.Text			= objResourceMgr.GetString("txtEFFECTIVE_DATE");
				capEXPIRATION_DATE.Text			= objResourceMgr.GetString("txtEXPIRATION_DATE");
				capLINE_OF_BUSINESS.Text		= objResourceMgr.GetString("cmbLINE_OF_BUSINESS");
				capCOVERAGE_CODE.Text			= objResourceMgr.GetString("txtCOVERAGE_CODE");
				capCLASS_CODE.Text				= objResourceMgr.GetString("txtCLASS_CODE");
				capPERIL.Text					= objResourceMgr.GetString("cmbPERIL");
				capSTATED_AMOUNT.Text			= objResourceMgr.GetString("txtSTATED_AMOUNT");
				capSPECIFIED_LOSS_RATIO.Text	= objResourceMgr.GetString("txtSPECIFIED_LOSS_RATIO");
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion

		#region G E T   F O R M   V A L U E S 
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsAddReinsuranceAggStopLossInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsAddReinsuranceAggStopLossInfo objAddReinsuranceAggStopLossInfo = new ClsAddReinsuranceAggStopLossInfo();

			if(hidCONTRACT_ID.Value != "")
				objAddReinsuranceAggStopLossInfo.CONTRACT_ID			= Convert.ToInt32(hidCONTRACT_ID.Value);
			if(cmbREINSURANCE_COMPANY.SelectedItem.Value != "")
				objAddReinsuranceAggStopLossInfo.REINSURANCE_COMPANY	= cmbREINSURANCE_COMPANY.SelectedItem.Value;
			if(txtACCOUNT_NUMBER.Text != "")
				objAddReinsuranceAggStopLossInfo.REINSURANCE_ACC_NUMBER	= txtACCOUNT_NUMBER.Text;
			if(txtEFFECTIVE_DATE.Text != "")
				objAddReinsuranceAggStopLossInfo.EFFECTIVE_DATE			= Convert.ToDateTime(txtEFFECTIVE_DATE.Text);
			if(txtEXPIRATION_DATE.Text != "")
				objAddReinsuranceAggStopLossInfo.EXPIRATION_DATE		= Convert.ToDateTime(txtEXPIRATION_DATE.Text);			
			if(cmbLINE_OF_BUSINESS.SelectedItem.Value != "")
				objAddReinsuranceAggStopLossInfo.LINE_OF_BUSINESS		= Convert.ToInt32(cmbLINE_OF_BUSINESS.SelectedItem.Value);			
			if(txtCOVERAGE_CODE.Text != "")
				objAddReinsuranceAggStopLossInfo.COVERAGE_CODE			= Convert.ToInt32(hidCOVERAGE_ID.Value);			
			if(txtCLASS_CODE.Text != "")
				objAddReinsuranceAggStopLossInfo.CLASS_CODE				= Convert.ToInt32(hidCLASS_CODE.Value);			
			if(cmbPERIL.SelectedItem.Value != "")
				objAddReinsuranceAggStopLossInfo.PERIL					= Convert.ToInt32(cmbPERIL.SelectedItem.Value);	
			if(txtSTATED_AMOUNT.Text != "")
				objAddReinsuranceAggStopLossInfo.STATED_AMOUNT			= Convert.ToDouble(txtSTATED_AMOUNT.Text);			
			if(txtSPECIFIED_LOSS_RATIO.Text != "")
				objAddReinsuranceAggStopLossInfo.SPECIFIC_LOSS_RATIO	= Convert.ToDouble(txtSPECIFIED_LOSS_RATIO.Text);			

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strAggregate_ID	=	hidAGGREGATE_ID.Value;
			//Returning the model object
			
			return objAddReinsuranceAggStopLossInfo;
		}
		#endregion

		#region W E B  F O R M   D E S I G N E R   G E N E R A T E D   C O D E

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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		# region S A V E   F O R M   D A T A 

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objAddReinsuranceAggStopLoss = new ClsAddReinsuranceAggStopLoss();

				ClsAddReinsuranceAggStopLossInfo objAddReinsuranceAggStopLossInfo = GetFormValue();

				if(hidOldData.Value == "0") //Add Mode
				{
					objAddReinsuranceAggStopLossInfo.CREATED_BY			= int.Parse(GetUserId());
					objAddReinsuranceAggStopLossInfo.CREATED_DATETIME	= DateTime.Now;	
					objAddReinsuranceAggStopLossInfo.IS_ACTIVE			= "Y";
					
					intRetVal = objAddReinsuranceAggStopLoss.Add(objAddReinsuranceAggStopLossInfo);

					if(intRetVal>0)
					{
						hidAGGREGATE_ID.Value	=	objAddReinsuranceAggStopLossInfo.AGGREGATE_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"14");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=   objAddReinsuranceAggStopLoss.GetDataForPageControls(hidCONTRACT_ID.Value).GetXml();
						hidIS_ACTIVE.Value		=	"Y";						
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"15");
						hidFormSaved.Value		=	"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"16");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
				else // Edit Mode
				{
					//Creating the Model object for holding the Old data
					ClsAddReinsuranceAggStopLossInfo objOldAddReinsuranceAggStopLossInfo = new ClsAddReinsuranceAggStopLossInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldAddReinsuranceAggStopLossInfo, hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objAddReinsuranceAggStopLossInfo.AGGREGATE_ID			= int.Parse(strAggregate_ID);
					objAddReinsuranceAggStopLossInfo.MODIFIED_BY			= int.Parse(GetUserId());
					objAddReinsuranceAggStopLossInfo.LAST_UPDATED_DATETIME	= DateTime.Now;
					
					intRetVal	= objAddReinsuranceAggStopLoss.Update(objOldAddReinsuranceAggStopLossInfo,objAddReinsuranceAggStopLossInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"17");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=	objAddReinsuranceAggStopLoss.GetDataForPageControls(hidCONTRACT_ID.Value).GetXml();
					}
					else if(intRetVal == -1)	
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"16");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"16");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"15") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objAddReinsuranceAggStopLoss!= null)
					objAddReinsuranceAggStopLoss.Dispose();
			}
		}

		# endregion 

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetOldData()
		{
			try
			{
				objAddReinsuranceAggStopLoss = new ClsAddReinsuranceAggStopLoss();
				DataSet oDs1 = objAddReinsuranceAggStopLoss.GetDataForPageControls(hidCONTRACT_ID.Value);
				DataTable oDt;

				if(oDs1.Tables[0].Rows.Count >0)
				{
					oDt = oDs1.Tables[0];
					hidOldData.Value = ClsCommon.GetXML(oDt);
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}

		private void GetDataForEditMode()
		{
			try
			{
				objAddReinsuranceAggStopLoss = new ClsAddReinsuranceAggStopLoss();
				DataSet oDs = objAddReinsuranceAggStopLoss.GetDataForPageControls(hidCONTRACT_ID.Value);
				
				strContract_Name	= objAddReinsuranceAggStopLoss.GetContractNameFromId(int.Parse(hidCONTRACT_ID.Value));
				strContract_Name	= strContract_Name.Substring(0,strContract_Name.IndexOf(":"));
				txtCONTRACT_NUMBER.Text = strContract_Name;
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
					if(oDr["REINSURANCE_COMPANY"].ToString() != "")
						ClsCommon.SelectValueinDDL(cmbREINSURANCE_COMPANY, oDr["REINSURANCE_COMPANY"].ToString());

					if(oDr["REINSURANCE_ACC_NUMBER"].ToString() != "")
						txtACCOUNT_NUMBER.Text		= oDr["REINSURANCE_ACC_NUMBER"].ToString();
					
					if(oDr["EFFECTIVE_DATE"].ToString() != "")
						txtEFFECTIVE_DATE.Text 		= Default.GetDateFromString(oDr["EFFECTIVE_DATE"].ToString()).ToString("MM/dd/yyyy");
					
					if(oDr["EXPIRATION_DATE"].ToString() != "")
						txtEXPIRATION_DATE.Text 	= Default.GetDateFromString(oDr["EXPIRATION_DATE"].ToString()).ToString("MM/dd/yyyy");
					
					if(oDr["LINE_OF_BUSINESS"].ToString() != "")
						ClsCommon.SelectValueinDDL(cmbLINE_OF_BUSINESS, oDr["LINE_OF_BUSINESS"].ToString());
					
					if(oDr["COVERAGE_CODE"].ToString() != "")
						txtCOVERAGE_CODE.Text 		= oDr["COVERAGE_CODE"].ToString();

					if(oDr["CLASS_CODE"].ToString() != "")
						txtCLASS_CODE.Text 			= oDr["CLASS_CODE"].ToString();

					if(oDr["PERIL"].ToString() != "")
						ClsCommon.SelectValueinDDL(cmbPERIL, oDr["PERIL"].ToString());

					if(oDr["STATED_AMOUNT"].ToString() != "")
						txtSTATED_AMOUNT.Text 		= oDr["STATED_AMOUNT"].ToString();

					if(oDr["SPECIFIC_LOSS_RATIO"].ToString() != "")
						txtSPECIFIED_LOSS_RATIO.Text 	= oDr["SPECIFIC_LOSS_RATIO"].ToString();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}

		# endregion 

		# region P O P U LA T E   D R O P D O W N S 

		private void FillCombos()
		{
			try
			{
				//For Reinsurance Company Names
				DataTable dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetReinsuranceCompanyNames();
				cmbREINSURANCE_COMPANY.DataSource		= dt;
				cmbREINSURANCE_COMPANY.DataTextField	= "REIN_COMAPANY_NAME";
				cmbREINSURANCE_COMPANY.DataValueField	= "REIN_COMAPANY_ID";
				cmbREINSURANCE_COMPANY.DataBind();
				cmbREINSURANCE_COMPANY.Items.Insert(0,"");

				//For Line of Business
				dt = Cms.CmsWeb.ClsFetcher.LOBs ;
				cmbLINE_OF_BUSINESS.DataSource		= dt;
				cmbLINE_OF_BUSINESS.DataTextField	= "LOB_DESC";
				cmbLINE_OF_BUSINESS.DataValueField	= "LOB_ID";
				cmbLINE_OF_BUSINESS.DataBind();
				cmbLINE_OF_BUSINESS.Items.Insert(0,"");

				//For Perils
				dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("%PERIL");
				cmbPERIL.DataSource		= dt;
				cmbPERIL.DataTextField	= "LookupDesc";
				cmbPERIL.DataValueField	= "LookupID";
				cmbPERIL.DataBind();
				cmbPERIL.Items.Insert(0,"");
			}
			finally
			{}
		}
 

		# endregion 
	}
}
