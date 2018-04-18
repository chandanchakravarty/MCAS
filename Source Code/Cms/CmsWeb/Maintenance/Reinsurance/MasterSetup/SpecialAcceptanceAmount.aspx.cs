/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: April 18, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for SPECIAL ACCEPTANCE AMOUNT REINSURANCE. 
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

namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for SpecialAcceptanceAmount.
	/// </summary>
	public class SpecialAcceptanceAmount :Cms.CmsWeb.cmsbase // System.Web.UI.Page
	{
		
		# region D E C L A R A T I O N
		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capSPECIAL_ACCEPTANCE_LIMIT;
		
		protected System.Web.UI.WebControls.TextBox txtSPECIAL_ACCEPTANCE_LIMIT;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSPECIAL_ACCEPTANCE_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSPECIAL_ACCEPTANCE_LIMIT;
		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid_SPECIAL_ACCEPTANCE_LIMIT_ID;

		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;

		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected System.Web.UI.WebControls.Label capMessages;

		ClsReinsuranceSpecialAcceptanceAmount objReinsuranceSpecialAcceptanceAmount;

		System.Resources.ResourceManager objResourceMgr;
		
		
		private string strRowId, strFormSaved;
		protected string oldXML;
        
		# endregion 
		#region PAGE LOAD
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//objClsReinsuranceSpecialAcceptanceAmount=new ClsReinsuranceSpecialAcceptanceAmount();
			
			try
			{
				//this.txtSpecialAcceptanceLimit.Text=objClsReinsuranceSpecialAcceptanceAmount.PersistValue();
				//objReinsuranceContractType = new ClsReinsuranceContractType();
				//objReinsuranceContractName = new ClsReinsuranceContractName();
				//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

				objReinsuranceSpecialAcceptanceAmount=new ClsReinsuranceSpecialAcceptanceAmount();
				btnReset.Attributes.Add("onclick","javascript:return Reset();");
				hlkEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT.txtEFFECTIVE_DATE,document.MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT.txtEFFECTIVE_DATE)");

				txtSPECIAL_ACCEPTANCE_LIMIT.Attributes.Add("onBlur","FormatAmount(document.getElementById('txtSPECIAL_ACCEPTANCE_LIMIT'));");

				base.ScreenId = "402";
				lblMessage.Visible = false;
				
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass		=	CmsButtonType.Write; //Permission made Write instead of Execute - Done by Sibin on 14 Jan 09 for Itrack Issue 4798
				btnReset.PermissionString	=	gstrSecurityXML;

				btnSave.CmsButtonClass		=	CmsButtonType.Write; //Permission made Write instead of Execute - Done by Sibin on 14 Jan 09 for Itrack Issue 4798
				btnSave.PermissionString	=	gstrSecurityXML;
				
				btnActivateDeactivate.CmsButtonClass	= CmsButtonType.Write;
				btnActivateDeactivate.PermissionString	= gstrSecurityXML;

				//test.CmsButtonClass		=	CmsButtonType.Execute;
				//test.PermissionString	=	gstrSecurityXML;

				
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.SpecialAcceptanceAmount" ,System.Reflection.Assembly.GetExecutingAssembly());

				if(!Page.IsPostBack)
				{
					if(Request.QueryString["SPECIAL_ACCEPTANCE_LIMIT_ID"]!=null && Request.QueryString["SPECIAL_ACCEPTANCE_LIMIT_ID"].ToString().Length>0)
						hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value = Request.QueryString["SPECIAL_ACCEPTANCE_LIMIT_ID"].ToString();
                    
                    SetErrorMessages();
					SetCaptions();
					fillDropDowns();
                    
                    if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "SpecialAcceptanceAmount.xml"))
                    {
                        setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/SpecialAcceptanceAmount.xml");
                    }

					GetDataForEditMode();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);			
			}

		}

		#endregion Page_Load
		# region  S E T   P A G E   V A L I D A T I O N S   E R R O R   M E S S A G E S 

		private void SetErrorMessages()
		{
			
			/*try
			{
				rfvSPECIAL_ACCEPTANCE_LIMIT.ErrorMessage			=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}*/
			
			//Regular Expression for validation
			//revSPECIAL_ACCEPTANCE_LIMIT.ValidationExpression				=	aRegExpCurrencyformat;
			//revSPECIAL_ACCEPTANCE_LIMIT.ValidationExpression				=	aRegExpDoublePositiveWithZero;
			//revSPECIAL_ACCEPTANCE_LIMIT.ValidationExpression				=	aRegExpDoublePositiveNonZeroStartNotZero;
			revSPECIAL_ACCEPTANCE_LIMIT.ValidationExpression				=	aRegExpDoublePositiveWithMoreThanOneDecimalAndComma;
			//rfvSPECIAL_ACCEPTANCE_LIMIT.ErrorMessage						=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
//			rfvEFFECTIVE_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
//			rfvLOB_ID.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("865");
			revEFFECTIVE_DATE.ValidationExpression =aRegExpDate;
			revEFFECTIVE_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
            rfvSPECIAL_ACCEPTANCE_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6"); //sneha

		}

		# endregion
		# region  S E T   L A B E L   C A P T I O N S 

		private void SetCaptions()
		{
			try
			{
				capSPECIAL_ACCEPTANCE_LIMIT.Text	=	objResourceMgr.GetString("txtSPECIAL_ACCEPTANCE_LIMIT");
                capEFFECTIVE_DATE.Text = objResourceMgr.GetString("txtEFFECTIVE_DATE");
                capLOB_ID.Text = objResourceMgr.GetString("cmbLOB_ID");
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
		}

		# endregion
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
		}
		#endregion
		

		# region S A V E   F O R M   D A T A 
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objReinsuranceSpecialAcceptanceAmount = new ClsReinsuranceSpecialAcceptanceAmount();


				ClsReinsuranceSpecialAcceptanceAmountInfo  objReinsuranceSpecialAcceptanceAmountInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //Add Mode
				{
					objReinsuranceSpecialAcceptanceAmountInfo.CREATED_BY = int.Parse(GetUserId());
					objReinsuranceSpecialAcceptanceAmountInfo.CREATED_DATETIME = DateTime.Now;
					intRetVal = objReinsuranceSpecialAcceptanceAmount.Add(objReinsuranceSpecialAcceptanceAmountInfo);

					

					if(intRetVal>0)
					{
						hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value		=	objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT_ID.ToString();
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"3");
						hidFormSaved.Value				=	"1";
						btnActivateDeactivate.Text="Deactivate";
						hidOldData.Value				=   objReinsuranceSpecialAcceptanceAmount.GetDataForPageControls(hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value).GetXml();
						hidIS_ACTIVE.Value				=	"Y";						
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"2");
						hidFormSaved.Value		=	"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"4");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
				
				else // Edit Mode
				{
					//Creating the Model object for holding the Old data
					ClsReinsuranceSpecialAcceptanceAmountInfo  objOldReinsuranceSpecialAcceptanceAmountInfo = new ClsReinsuranceSpecialAcceptanceAmountInfo();

					objReinsuranceSpecialAcceptanceAmountInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReinsuranceSpecialAcceptanceAmountInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceSpecialAcceptanceAmountInfo, hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT_ID		=int.Parse(strRowId);
					
					intRetVal	= objReinsuranceSpecialAcceptanceAmount.Update(objOldReinsuranceSpecialAcceptanceAmountInfo,objReinsuranceSpecialAcceptanceAmountInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=	objReinsuranceSpecialAcceptanceAmount.GetDataForPageControls(strRowId).GetXml();
					}
					else if(intRetVal == -1)	
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"4");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"2") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objReinsuranceSpecialAcceptanceAmount!= null)
					objReinsuranceSpecialAcceptanceAmount.Dispose();
			}
		}
		
		# endregion 

		#region G E T   F O R M   V A L U E S 
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		
		private ClsReinsuranceSpecialAcceptanceAmountInfo GetFormValue()
		{
			
			//Creating the Model object for holding the New data
			Cms.Model.Maintenance.Reinsurance.ClsReinsuranceSpecialAcceptanceAmountInfo objReinsuranceSpecialAcceptanceAmountInfo = new ClsReinsuranceSpecialAcceptanceAmountInfo();
			
			
			if(txtSPECIAL_ACCEPTANCE_LIMIT.Text != "")
				objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT			= txtSPECIAL_ACCEPTANCE_LIMIT.Text;
			if (txtEFFECTIVE_DATE.Text.Trim() != "")
				objReinsuranceSpecialAcceptanceAmountInfo.EFFECTIVE_DATE = Convert.ToDateTime(txtEFFECTIVE_DATE.Text.Trim());
			
			if (cmbLOB_ID.SelectedValue != null && cmbLOB_ID.SelectedValue.Trim() != "")
				objReinsuranceSpecialAcceptanceAmountInfo.LOB_ID = int.Parse(cmbLOB_ID.SelectedValue);
			if(hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value!="New")
				objReinsuranceSpecialAcceptanceAmountInfo.SPECIAL_ACCEPTANCE_LIMIT_ID = int.Parse(hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value);
			else 
				hidIS_ACTIVE.Value = "Y";
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value;
			oldXML			=	hidOldData.Value;
			//Returning the model object
			
			return objReinsuranceSpecialAcceptanceAmountInfo;
			
		}
		
		#endregion

		#region GET DATA FOR EDIT MODE

		private void GetDataForEditMode()
		{
			try
			{
				objReinsuranceSpecialAcceptanceAmount=new ClsReinsuranceSpecialAcceptanceAmount();
				DataSet oDs=objReinsuranceSpecialAcceptanceAmount.GetDataForPageControls(this.hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value);
				if(oDs.Tables[0].Rows.Count > 0)
				{
					DataRow oDr=oDs.Tables[0].Rows[0];
					if(oDr["SPECIAL_ACCEPTANCE_LIMIT"].ToString() !="")
					this.txtSPECIAL_ACCEPTANCE_LIMIT.Text=oDr["SPECIAL_ACCEPTANCE_LIMIT"].ToString();
					this.txtEFFECTIVE_DATE.Text =ConvertToDateCulture(Convert.ToDateTime(oDr["EFFECTIVE_DATE"].ToString()));
					this.cmbLOB_ID.SelectedIndex = cmbLOB_ID.Items.IndexOf(cmbLOB_ID.Items.FindByValue(oDr["LOB_ID"].ToString()));
					if(oDr["IS_ACTIVE"].ToString()=="Y")
					{
						btnActivateDeactivate.Text = "Deactivate";
						hidIS_ACTIVE.Value = "Y";
					}
					else if(oDr["IS_ACTIVE"].ToString()=="N")
					{
						hidIS_ACTIVE.Value = "N";
						btnActivateDeactivate.Text = "Activate";
					}
					hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value=oDr["SPECIAL_ACCEPTANCE_LIMIT_ID"].ToString();
					hidOldData.Value=oDs.GetXml();
				}
				

			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}
		# endregion

		private void fillDropDowns()
		{
			//LOBs
			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			cmbLOB_ID.DataSource			= dtLOBs;
			cmbLOB_ID.DataTextField		= "LOB_DESC";
			cmbLOB_ID.DataValueField		= "LOB_ID";
			cmbLOB_ID.DataBind();

			//Remove General Liability
			ListItem Li = cmbLOB_ID.Items.FindByValue("7");//"7" -> General Liability
			if (Li != null)	
			{
				cmbLOB_ID.Items.Remove(Li);	
			}
			cmbLOB_ID.Items.Insert(0,"");
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
		
			objReinsuranceSpecialAcceptanceAmount=new ClsReinsuranceSpecialAcceptanceAmount();
			ClsReinsuranceSpecialAcceptanceAmountInfo  objReinsuranceSpecialAcceptanceAmountInfo = GetFormValue();

			try
			{
				//Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo();
				
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objReinsuranceSpecialAcceptanceAmount.ActivateDeactivateSpecialAcceptance(objReinsuranceSpecialAcceptanceAmountInfo,"N");
					lblMessage.Text = ClsMessages.FetchGeneralMessage("41");
					btnActivateDeactivate.Text="Activate";
					hidIS_ACTIVE.Value="N";
				}
				else
				{					
					objReinsuranceSpecialAcceptanceAmount.ActivateDeactivateSpecialAcceptance(objReinsuranceSpecialAcceptanceAmountInfo,"Y");
					lblMessage.Text = ClsMessages.FetchGeneralMessage("40");
					btnActivateDeactivate.Text="Deactivate";
					hidIS_ACTIVE.Value="Y";
				}
				hidOldData.Value = objReinsuranceSpecialAcceptanceAmount.GetDataForPageControls(this.hid_SPECIAL_ACCEPTANCE_LIMIT_ID.Value).GetXml();
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
				if(objReinsuranceSpecialAcceptanceAmount!= null)
					objReinsuranceSpecialAcceptanceAmount.Dispose();
			}
				
		}
	}
}
