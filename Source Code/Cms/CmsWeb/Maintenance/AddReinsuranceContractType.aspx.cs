/* ***************************************************************************************
   Author		: Deepak Batra 
   Creation Date: 05/01/2006
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for Excess Layer for a reinsurance contract. 
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
	/// Summary description for AddReinsuranceContractType.
	/// </summary>
	public class AddReinsuranceContractType : Cms.CmsWeb.cmsbase
	{
		# region D E C L A R A T I O N
		
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCONTRACT_TYPE;
		
		protected System.Web.UI.WebControls.TextBox txtCONTRACT_TYPE;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTRACT_TYPE;
		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		public ClsReinsuranceSpecialAcceptanceAmount objDeActivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hid_CONTRACT_TYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidact;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hiddec;
        protected System.Web.UI.WebControls.Label capMessages;
		ClsReinsuranceContractType objReinsuranceContractType;
		
		System.Resources.ResourceManager objResourceMgr;
		
		private string strRowId, strFormSaved;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		//string oldXML;
        
		# endregion 

		# region P A G E   L O A D 

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				objReinsuranceContractType = new ClsReinsuranceContractType();
				objDeActivate=new ClsReinsuranceSpecialAcceptanceAmount();
				
				
				//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				btnReset.Attributes.Add("onclick","javascript:return Reset();");
				//btnSave.Attributes.Add("onclick","javascript:return SaveData();");

				base.ScreenId = "266"; //Screen- id set by Sibin on 14 Jan 09 for Itrack Issue 4798
				lblMessage.Visible = false;
				SetErrorMessages();

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass		=	CmsButtonType.Write;
				btnReset.PermissionString	=	gstrSecurityXML;

				btnSave.CmsButtonClass		=	CmsButtonType.Write;
				btnSave.PermissionString	=	gstrSecurityXML;
				
				//btnActivate.CmsButtonClass		=	CmsButtonType.Execute;
				//btnActivate.PermissionString	=	gstrSecurityXML;
				//Itrack No.2299 Added by Manoj Rathore 8 Aug 2007
				btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
				btnActivateDeactivate.PermissionString	=	gstrSecurityXML;
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddReinsuranceContractType" ,System.Reflection.Assembly.GetExecutingAssembly());

				if(!Page.IsPostBack)
				{
					if(Request.QueryString["CONTRACTTYPEID"]!=null && Request.QueryString["CONTRACTTYPEID"].ToString()!="")
					{
						hidFormSaved.Value = "1";
						hid_CONTRACT_TYPE_ID.Value = Request.QueryString["CONTRACTTYPEID"].ToString();
					}

					SetCaptions();

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
				rfvCONTRACT_TYPE.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
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
				capCONTRACT_TYPE.Text	=	objResourceMgr.GetString("txtCONTRACT_TYPE");
                hidact.Value =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");
                hiddec.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); 

				
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
		private Model.Maintenance.Reinsurance.ClsReinsuranceContractTypeInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsReinsuranceContractTypeInfo objReinsuranceContractTypeInfo = new ClsReinsuranceContractTypeInfo();

			if(txtCONTRACT_TYPE.Text != "")
				objReinsuranceContractTypeInfo.CONTRACT_TYPE_DESC	= txtCONTRACT_TYPE.Text;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hid_CONTRACT_TYPE_ID.Value;
			//oldXML			=	hidOldData.Value;
			//Returning the model object
			
			return objReinsuranceContractTypeInfo;
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
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
				objReinsuranceContractType = new ClsReinsuranceContractType();

				ClsReinsuranceContractTypeInfo objReinsuranceContractTypeInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //Add Mode
				{
					
					//objReinsuranceContractTypeInfo.IS_ACTIVE = "Y";
					
					objReinsuranceContractTypeInfo.CREATED_BY = int.Parse(GetUserId());
					objReinsuranceContractTypeInfo.CREATED_DATETIME = DateTime.Now;

					intRetVal = objReinsuranceContractType.Add(objReinsuranceContractTypeInfo);					

					if(intRetVal>0)
					{
						hid_CONTRACT_TYPE_ID.Value		=	objReinsuranceContractTypeInfo.CONTRACTTYPEID.ToString();
						lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"3");
						hidFormSaved.Value				=	"1";
						hidOldData.Value				=   objReinsuranceContractType.GetDataForPageControls(this.hid_CONTRACT_TYPE_ID.Value).GetXml();
						hidIS_ACTIVE.Value				=	"Y";
						//btnActivateDeactivate.Enabled = true;
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"2");
						hidFormSaved.Value		=	"2";
						//btnActivateDeactivate.Enabled = false;
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
					ClsReinsuranceContractTypeInfo objOldReinsuranceContractTypeInfo = new ClsReinsuranceContractTypeInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceContractTypeInfo, hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objReinsuranceContractTypeInfo.CONTRACTTYPEID	= int.Parse(strRowId);
					objReinsuranceContractTypeInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReinsuranceContractTypeInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					
					intRetVal	= objReinsuranceContractType.Update(objOldReinsuranceContractTypeInfo,objReinsuranceContractTypeInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=	objReinsuranceContractType.GetDataForPageControls(strRowId).GetXml();
					}
					else if(intRetVal == -1)	
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"4");
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
				if(objReinsuranceContractType!= null)
					objReinsuranceContractType.Dispose();
			}
		}

		# endregion 
   

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				objReinsuranceContractType = new ClsReinsuranceContractType();
				DataSet oDs = objReinsuranceContractType.GetDataForPageControls(hid_CONTRACT_TYPE_ID.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
					if(oDr["CONTRACT_TYPE_DESC"].ToString() != "")
						txtCONTRACT_TYPE.Text		= oDr["CONTRACT_TYPE_DESC"].ToString();
					if(oDr["IS_ACTIVE"].ToString()=="Y")
                        btnActivateDeactivate.Text = "Deactivate";
                    if (oDr["IS_ACTIVE"].ToString() == "N")
                        btnActivateDeactivate.Text ="Activate";
					hidIS_ACTIVE.Value= oDr["IS_ACTIVE"].ToString();
					this.hidOldData.Value	= oDs.GetXml();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}

		# endregion 
		
		
		
		/*private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			objReinsuranceContractType = new ClsReinsuranceContractType();
			
						
			if(btnActivateDeactivate.Text=="Deactivate")
			{
						  
				int harmanjeet=objReinsuranceContractType.GetDeactivateActivate(hid_CONTRACT_TYPE_ID.Value.ToString(),"N");
				btnActivateDeactivate.Text="Activate";
			}
			else
			{
				
				int harmanjeet=objReinsuranceContractType.GetDeactivateActivate(hid_CONTRACT_TYPE_ID.Value.ToString(),"Y");
				btnActivateDeactivate.Text="Deactivate";

			}
			
		}*/
		
		#region DEACTIVATE ACTIVATE BUTTON CLICK
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				//objDepartment =  new ClsDepartment();
				Cms.BusinessLayer.BlCommon.ClsReinsuranceContractType  objReinsuranceContractType= new ClsReinsuranceContractType();

				Model.Maintenance.Reinsurance.ClsReinsuranceContractTypeInfo objReinsuranceContractTypeInfo;
				objReinsuranceContractTypeInfo = GetFormValue();
				
				string strRetVal = "";
				string strCustomInfo ="";
				strCustomInfo = "Contract Type:" + objReinsuranceContractTypeInfo.CONTRACT_TYPE_DESC ;
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Contract Type Has Been Deactivated Successfully.";
					objReinsuranceContractType.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = objReinsuranceContractType.ActivateDeactivate(hid_CONTRACT_TYPE_ID.Value,"N",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Contract Type Has Been Activated Successfully.";
					objReinsuranceContractType.TransactionInfoParams = objStuTransactionInfo;
					objReinsuranceContractType.ActivateDeactivate(hid_CONTRACT_TYPE_ID.Value,"Y",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}

				//				if (strRetVal == "-1")
				//				{
				//					/*Profit Center is assigned*/
				//					lblMessage.Text =  ClsMessages.GetMessage(base.ScreenId,"513");
				//					lblDelete.Visible = false;
				//				}
				
				hidFormSaved.Value			=	"0";
				GetOldDataXML();
				//hidReset.Value="0";

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
				if(objReinsuranceContractType!= null)
					objReinsuranceContractType.Dispose();
			}
		}
		#endregion
		private void GetOldDataXML()
		{
			Cms.BusinessLayer.BlCommon.ClsReinsuranceContractType   objReinsuranceContractType = new ClsReinsuranceContractType();
			if (hid_CONTRACT_TYPE_ID.Value.ToString() != "" )
			{
				this.hidOldData.Value	= objReinsuranceContractType.GetDataForPageControls(strRowId).GetXml();
			}
		}
		
	}
}
