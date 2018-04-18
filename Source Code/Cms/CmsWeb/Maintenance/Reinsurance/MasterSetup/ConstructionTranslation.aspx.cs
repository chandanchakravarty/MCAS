/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: April 30, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for ConstructionTranslation. 
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

using System.Resources; 
using System.Reflection; 

using Cms.CmsWeb;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon.Reinsurance;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for ConstructionTranslation.
	/// </summary>
	public class ConstructionTranslation : Cms.CmsWeb.cmsbase
	{
		# region DECALARATION

		System.Resources.ResourceManager objResourceMgr;

		ClsConstructionTranslation objConstructionTranslation;

		private string strRowId, strFormSaved;
		//string oldXML;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capREIN_EXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_EXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.Label capREIN_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capREIN_REPORT_CODE;
		protected System.Web.UI.WebControls.Label capREIN_NISS;
		protected System.Web.UI.WebControls.TextBox txtREIN_NISS;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_CONSTRUCTION_CODE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_EXTERIOR_CONSTRUCTION;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_DESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtREIN_REPORT_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_REPORT_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_NISS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_NISS;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.WebControls.Label capMessages;
		# endregion

		# region PAGE LOAD
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			btnReset.Attributes.Add("onclick","javascript:return Reset();");
			//this.cmbREIN_DESCRIPTION.Attributes.Add("onclick","setReinsuranceReportCode();");
			base.ScreenId="399";
			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
			# region *********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	   =	CmsButtonType.Write;
			btnReset.PermissionString  =	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	  =	 CmsButtonType.Write;
			btnSave.PermissionString  =	 gstrSecurityXML;

			
			this.btnDelete.CmsButtonClass =     CmsButtonType.Delete;
			this.btnDelete.PermissionString =	gstrSecurityXML;
			# endregion //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.ConstructionTranslation" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			# region POST BACK EVENT HANDLER

			if(!Page.IsPostBack)
			{																 
				
				if (Request.Params["REIN_CONSTRUCTION_CODE_ID"] != null)
				{
					if (Request.Params["REIN_CONSTRUCTION_CODE_ID"].ToString() != "")
					{
						this.hidREIN_CONSTRUCTION_CODE_ID.Value = Request.Params["REIN_CONSTRUCTION_CODE_ID"].ToString();
						GenerateXML(hidREIN_CONSTRUCTION_CODE_ID.Value);
					}
				}


				SetCaptions();
				SetDropdownList();   
				GetDataForEditMode();
				
				#region "Loading singleton"
				
				//START APRIL 11 HARMANJEET				
				this.cmbREIN_DESCRIPTION.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RCONCD");
				//cmbSEX.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEX");
				cmbREIN_DESCRIPTION.DataTextField="LookupDesc";
				cmbREIN_DESCRIPTION.DataValueField="LookupCode";
				cmbREIN_DESCRIPTION.DataBind();
				cmbREIN_DESCRIPTION.Items.Insert(0,"");	
				//END HARMANJEET

				this.cmbREIN_EXTERIOR_CONSTRUCTION.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CONTYP");
				//cmbSEX.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEX");
				cmbREIN_EXTERIOR_CONSTRUCTION.DataTextField="LookupDesc";
				cmbREIN_EXTERIOR_CONSTRUCTION.DataValueField="LookupCode";
				cmbREIN_EXTERIOR_CONSTRUCTION.DataBind();
				cmbREIN_EXTERIOR_CONSTRUCTION.Items.Insert(0,"");	
			
				

				#endregion//Loading singleton
			}

			# endregion
		}
		# endregion
		
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			
			
			this.revREIN_NISS.ValidationExpression="^[1-9]{1}";

            this.revREIN_NISS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1362");//"Please enter one digit number greater than zero.";
            rfvREIN_EXTERIOR_CONSTRUCTION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"4");
            rfvREIN_REPORT_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            rfvREIN_DESCRIPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvREIN_NISS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
		}

		#endregion

		# region SET DROPDOWNLIST
		private void SetDropdownList()
		{
			//cmbREIN_CONTACT_IS_BROKER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			//cmbREIN_CONTACT_IS_BROKER.DataTextField="LookupDesc"; 
			//cmbREIN_CONTACT_IS_BROKER.DataValueField="LookupCode";
			//cmbREIN_CONTACT_IS_BROKER.DataBind();
			//cmbREIN_CONTACT_IS_BROKER.Items.Insert(0,"");
		}
		# endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsConstructionTranslationInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Maintenance.Reinsurance.ClsConstructionTranslationInfo objConstructionTranslationInfo;
			objConstructionTranslationInfo = new ClsConstructionTranslationInfo();
			
			objConstructionTranslationInfo.REIN_EXTERIOR_CONSTRUCTION = this.cmbREIN_EXTERIOR_CONSTRUCTION.SelectedItem.Value;

			//if(this.txtREIN_TIV_EFFECTIVE_DATE.Text.Trim()!="")
			//	objTIVGroupInfo.REIN_TIV_EFFECTIVE_DATE =Convert.ToDateTime(this.txtREIN_TIV_EFFECTIVE_DATE.Text);
			objConstructionTranslationInfo.REIN_DESCRIPTION = this.cmbREIN_DESCRIPTION.SelectedItem.Value;
			objConstructionTranslationInfo.REIN_REPORT_CODE = this.txtREIN_REPORT_CODE.Text;
			objConstructionTranslationInfo.REIN_NISS=this.txtREIN_NISS.Text;


			//objReinsuranceContactInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			if(hidREIN_CONSTRUCTION_CODE_ID.Value=="" || hidREIN_CONSTRUCTION_CODE_ID.Value=="0")
				strRowId = "NEW";
			else
				strRowId		=	this.hidREIN_CONSTRUCTION_CODE_ID.Value;
			//oldXML		= hidOldData.Value;
			//Returning the model object

			return objConstructionTranslationInfo;
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
				objConstructionTranslation= new ClsConstructionTranslation();

				//Retreiving the form values into model class object
				ClsConstructionTranslationInfo objConstructionTranslationInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objConstructionTranslationInfo.CREATED_BY = int.Parse(GetUserId());
					objConstructionTranslationInfo.CREATED_DATETIME = DateTime.Now;
					//objTIVGroupInfo.IS_ACTIVE="Y"; 


					//Calling the add method of business layer class
					intRetVal = objConstructionTranslation.Add(objConstructionTranslationInfo);

					if(intRetVal>0)
					{
						this.hidREIN_CONSTRUCTION_CODE_ID.Value = objConstructionTranslationInfo.REIN_CONSTRUCTION_CODE_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						this.hidOldData.Value	= objConstructionTranslation.GetDataForPageControls(this.hidREIN_CONSTRUCTION_CODE_ID.Value).GetXml();
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
						hidIS_ACTIVE.Value = "Y";
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
                  // btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsConstructionTranslationInfo objOldConstructionTranslationInfo;
					objOldConstructionTranslationInfo = new ClsConstructionTranslationInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldConstructionTranslationInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					if(strRowId!="")
					objConstructionTranslationInfo.REIN_CONSTRUCTION_CODE_ID = int.Parse(strRowId);
					objConstructionTranslationInfo.MODIFIED_BY = int.Parse(GetUserId());
					objConstructionTranslationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    

					//Updating the record using business layer class object
					intRetVal	= objConstructionTranslation.Update(objOldConstructionTranslationInfo,objConstructionTranslationInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						this.hidOldData.Value	= objConstructionTranslation.GetDataForPageControls(strRowId).GetXml();
                   
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
                
			}
			finally
			{
				if(objConstructionTranslation!= null)
					objConstructionTranslation.Dispose();
			}

			
		}
		
		#endregion

		# region GENERATE XML
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string REIN_CONSTRUCTION_CODE_ID)
		{
			string strREIN_CONSTRUCTION_CODE_ID=REIN_CONSTRUCTION_CODE_ID;

            objConstructionTranslation=new ClsConstructionTranslation(); 
  			
			if(strREIN_CONSTRUCTION_CODE_ID!="" && strREIN_CONSTRUCTION_CODE_ID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objConstructionTranslation.GetDataForPageControls(strREIN_CONSTRUCTION_CODE_ID);
					hidOldData.Value=ds.GetXml(); 

					//hidFormSaved.Value="1"; 
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
					if(objConstructionTranslation!= null)
						objConstructionTranslation.Dispose();
				}  
                
			}
                
		}

		# endregion GENERATE XML

		# region SET CAPTIONS
		private void SetCaptions()
		{
			this.capREIN_EXTERIOR_CONSTRUCTION.Text		=objResourceMgr.GetString("cmbREIN_EXTERIOR_CONSTRUCTION");
			this.capREIN_DESCRIPTION.Text		=	objResourceMgr.GetString("cmbREIN_DESCRIPTION");
			this.capREIN_REPORT_CODE.Text					=	objResourceMgr.GetString("txtREIN_REPORT_CODE");
			this.capREIN_NISS.Text=	objResourceMgr.GetString("txtREIN_NISS");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
			
			//END Harmanjeet
		}

		# endregion SET CAPTIONS

		#region DEACTIVATE ACTIVATE BUTTON CLICK
		#region DEACTIVATE ACTIVATE BUTTON CLICK
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				//objDepartment =  new ClsDepartment();
				Cms.BusinessLayer.BlCommon.Reinsurance.ClsConstructionTranslation objConstructionTranslation = new ClsConstructionTranslation();

				Model.Maintenance.Reinsurance.ClsConstructionTranslationInfo objConstructionTranslationInfo;
				objConstructionTranslationInfo = GetFormValue();
				
				string strRetVal = "";
				string strCustomInfo ="";
				strCustomInfo = "Exterior Construction:" + cmbREIN_EXTERIOR_CONSTRUCTION.Items[cmbREIN_EXTERIOR_CONSTRUCTION.SelectedIndex].Text +"<br>"
					+"Reinsurance Description:" + cmbREIN_DESCRIPTION.Items[cmbREIN_DESCRIPTION.SelectedIndex].Text +"<br>"
					+"Reinsurance Report Code:" + objConstructionTranslationInfo.REIN_REPORT_CODE ;
				//	+"1st Split Coverage:" + cmbREIN_IST_SPLIT_COVERAGE.Items[cmbREIN_IST_SPLIT_COVERAGE.SelectedIndex].Text +"<br>"
				//	+"2nd Split Coverage:" + cmbREIN_2ND_SPLIT_COVERAGE.Items[cmbREIN_2ND_SPLIT_COVERAGE.SelectedIndex].Text;
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
                    objStuTransactionInfo.transactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1363");//"Construction Codes Has Been Deactivated Successfully.";
					objConstructionTranslation.TransactionInfoParams = objStuTransactionInfo;
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");
					strRetVal = objConstructionTranslation.ActivateDeactivate(hidREIN_CONSTRUCTION_CODE_ID.Value,"N",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
                    objStuTransactionInfo.transactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1364");//"Construction Codes Has Been Activated Successfully.";
					objConstructionTranslation.TransactionInfoParams = objStuTransactionInfo;
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
					objConstructionTranslation.ActivateDeactivate(hidREIN_CONSTRUCTION_CODE_ID.Value,"Y",strCustomInfo);
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
				hidReset.Value="0";

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
				if(objConstructionTranslation!= null)
					objConstructionTranslation.Dispose();
			}
		}
		#endregion
		private void GetOldDataXML()
		{
			Cms.BusinessLayer.BlCommon.Reinsurance.ClsConstructionTranslation objConstructionTranslation = new ClsConstructionTranslation();
			if (hidREIN_CONSTRUCTION_CODE_ID.Value.ToString() != "" )
			{
				this.hidOldData.Value	= objConstructionTranslation.GetDataForPageControls(strRowId).GetXml();
			}
		}
		/*private void btnActivate_Click(object sender, System.EventArgs e)
		{
		
			objConstructionTranslation = new ClsConstructionTranslation();
			
						
			if(this.btnActivate.Text=="Deactivate")
			{
						  
				int intStatusCheck=objConstructionTranslation.GetDeactivateActivate(this.hidREIN_CONSTRUCTION_CODE_ID.Value.ToString(),"N");
				btnActivate.Text="Activate";
				//this.btnDelete.Visible=false;
				lblMessage.Visible = true;
				lblMessage.Text ="Information deactivated successfully.";
				SetReadOnly();
			}
			else
			{
				
				int intStatusCheck=objConstructionTranslation.GetDeactivateActivate(this.hidREIN_CONSTRUCTION_CODE_ID.Value.ToString(),"Y");
				btnActivate.Text="Deactivate";
				//this.btnDelete.Visible=true;
				lblMessage.Visible = true;
				lblMessage.Text ="Information activated successfully.";
				SetReadOnlyfalse();
			}
		
		}
		*/
			
		#endregion

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				objConstructionTranslation = new ClsConstructionTranslation();
				DataSet oDs = objConstructionTranslation.GetDataForPageControls(this.hidREIN_CONSTRUCTION_CODE_ID.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
						
					this.txtREIN_REPORT_CODE.Text=oDr["REIN_REPORT_CODE"].ToString();
					this.txtREIN_NISS.Text=oDr["REIN_NISS"].ToString();
					if(oDr["REIN_NISS"]!=null && oDr["REIN_NISS"].ToString()!="")
					cmbREIN_DESCRIPTION.SelectedValue = oDr["REIN_DESCRIPTION"].ToString();
					this.cmbREIN_EXTERIOR_CONSTRUCTION.SelectedValue=oDr["REIN_EXTERIOR_CONSTRUCTION"].ToString();
					//this.txtREIN_TIV_TO.Text=oDr["REIN_TIV_TO"].ToString();
					//this.txtREIN_TIV_GROUP_CODE.Text=oDr["REIN_TIV_GROUP_CODE"].ToString();

//					if(oDr["IS_ACTIVE"].ToString()=="Y")
//					{
//						btnActivate.Text="Deactivate";
//						//this.btnDelete.Visible=true;
//						
//					}
//					if(oDr["IS_ACTIVE"].ToString()=="N")
//					{
//						btnActivate.Text="Activate";
//						//this.btnDelete.Visible=false;
//						
//					}
				//SetReadOnly();
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}
		
		private void SetReadOnly()
		{
			this.txtREIN_REPORT_CODE.ReadOnly=true;
			this.txtREIN_NISS.ReadOnly=true;
			//this.txtREIN_TIV_TO.ReadOnly=true;
			//this.txtREIN_TIV_GROUP_CODE.ReadOnly=true;
		}
		private void SetReadOnlyfalse()
		{
			this.txtREIN_REPORT_CODE.ReadOnly=false;
			this.txtREIN_NISS.ReadOnly=false;
			//this.txtREIN_TIV_TO.ReadOnly=false;
			//this.txtREIN_TIV_GROUP_CODE.ReadOnly=false;
		}
		# endregion G E T   D A T A   F O R   E D I T   M O D E
		
		# region DELETE RECORD
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			objConstructionTranslation = new ClsConstructionTranslation();
			int intStatusCheck=objConstructionTranslation.Delete(this.hidREIN_CONSTRUCTION_CODE_ID.Value.ToString());
			
			if (intStatusCheck > 0)
			{
				//Deleted successfully
				lblDelete.Visible = true;
                lblDelete.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1361");//"Record has been deleted successfully";
				hidREIN_CONSTRUCTION_CODE_ID.Value = "";
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
                trBody.Attributes.Add("style", "display:none");
                lblDelete.Visible = true;
			}
			else if(intStatusCheck == -2)
			{
				lblDelete.Text			=	ClsMessages.GetMessage(base.ScreenId,"6");
				hidFormSaved.Value		=	"2";
			}
			else
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "128");
				hidFormSaved.Value = "2";
			}
			lblDelete.Visible = true;
		
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

			
	}
}





