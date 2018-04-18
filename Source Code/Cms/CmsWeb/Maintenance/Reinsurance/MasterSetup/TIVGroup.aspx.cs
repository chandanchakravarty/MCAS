/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: April 30, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for REINSURANCE CONTACTS. 
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
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for TIVGroup.
	/// </summary>
	public class TIVGroup : Cms.CmsWeb.cmsbase
	{
		# region DECELARATION

		System.Resources.ResourceManager objResourceMgr;

		ClsTIVGroup objTIVGroup;

		private string strRowId, strFormSaved;
		//string oldXML;

		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_TIV_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvContract_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.Label capREIN_TIV_CONTRACT_NUMBER;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_TIV_CONTRACT_NUMBER;
		protected System.Web.UI.WebControls.Label capREIN_TIV_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtREIN_TIV_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_TIV_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capREIN_TIV_FROM;
		protected System.Web.UI.WebControls.TextBox txtREIN_TIV_FROM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_TIV_FROM;
		protected System.Web.UI.WebControls.Label capREIN_TIV_TO;
		protected System.Web.UI.WebControls.TextBox txtREIN_TIV_TO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_TIV_TO;
		protected System.Web.UI.WebControls.Label capREIN_TIV_GROUP_CODE;
		protected System.Web.UI.WebControls.TextBox txtREIN_TIV_GROUP_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_TIV_GROUP_CODE;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_TIV_GROUP_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
		protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_TIV_FROM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_TIV_TO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_TIV_GROUP_CODE;
        protected System.Web.UI.WebControls.Label capMessages;
		
		# endregion DECELARATION

		# region PAGE LOAD
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			this.hlkEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.TIVGroup.txtREIN_TIV_EFFECTIVE_DATE,document.TIVGroup.txtREIN_TIV_EFFECTIVE_DATE)");
			btnReset.Attributes.Add("onclick","javascript:return Reset();");

            txtREIN_TIV_FROM.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtREIN_TIV_TO.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");

			base.ScreenId="401_0";//Screen_id set to 401_0 by Sibin on 14 Jan 09 for Itrack Issue 4798
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
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.TIVGroup" ,System.Reflection.Assembly.GetExecutingAssembly());

			# region POST BACK EVENT HANDLER

			if(!Page.IsPostBack)
			{																 
				
				if (Request.Params["REIN_TIV_GROUP_ID"] != null)
				{
					if (Request.Params["REIN_TIV_GROUP_ID"].ToString() != "")
					{
						hidREIN_TIV_GROUP_ID.Value = Request.Params["REIN_TIV_GROUP_ID"].ToString();
						GenerateXML(hidREIN_TIV_GROUP_ID.Value);
					}
				}


				SetCaptions();
				SetDropdownList();
                
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "TIVGroup.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/TIVGroup.xml");
                }
				GetDataForEditMode();
				
				#region "Loading singleton"
				//using singleton object for country and state dropdown
				
				DataTable dt = objTIVGroup.GetContractNumber().Tables[0];
				this.cmbREIN_TIV_CONTRACT_NUMBER.DataSource		= dt;
				cmbREIN_TIV_CONTRACT_NUMBER.DataTextField	= "CONTRACT_NUMBER";
				cmbREIN_TIV_CONTRACT_NUMBER.DataValueField	= "CONTRACT_NUMBER";
				cmbREIN_TIV_CONTRACT_NUMBER.DataBind();
				cmbREIN_TIV_CONTRACT_NUMBER.Items.Insert(0,"");
				if(cmbREIN_TIV_CONTRACT_NUMBER.Items[0]!=null )
				{
					cmbREIN_TIV_CONTRACT_NUMBER.Items[0].Selected=true;  
				}
				
				/*
				cmbM_REIN_CONTACT_COUNTRY.DataSource		= dt;
				cmbM_REIN_CONTACT_COUNTRY.DataTextField	= "Country_Name";
				cmbM_REIN_CONTACT_COUNTRY.DataValueField	= "Country_Id";
				cmbM_REIN_CONTACT_COUNTRY.DataBind();
				cmbM_REIN_CONTACT_COUNTRY.Items[0].Selected=true; 

				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbREIN_CONTACT_STATE.DataSource		= dt;
				cmbREIN_CONTACT_STATE.DataTextField	= "STATE_NAME";
				cmbREIN_CONTACT_STATE.DataValueField	= "STATE_ID";
				cmbREIN_CONTACT_STATE.DataBind();
				cmbREIN_CONTACT_STATE.Items.Insert(0,"");
				
				//START APRIL 11 HARMANJEET
				//cmbREIN_CONTACT_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("REINTY");
				//cmbSEX.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEX");
				//cmbREIN_CONTACT_TYPE.DataTextField="LookupDesc";
				//cmbREIN_CONTACT_TYPE.DataValueField="LookupCode";
				//cmbREIN_CONTACT_TYPE.DataBind();
				//cmbREIN_CONTACT_TYPE.Items.Insert(0,"");	
				//END HARMANJEET

				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbM_REIN_CONTACT_STATE.DataSource		= dt;
				cmbM_REIN_CONTACT_STATE.DataTextField	= "STATE_NAME";
				cmbM_REIN_CONTACT_STATE.DataValueField	= "STATE_ID";
				cmbM_REIN_CONTACT_STATE.DataBind();
				cmbM_REIN_CONTACT_STATE.Items.Insert(0,"");
				*/

				#endregion//Loading singleton
			}

			# endregion
			

		}
		# endregion PAGE LOAD

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			
			/*
			this.revM_REIN_CONTACT_CITY.ValidationExpression="^[a-zA-Z0-9]{1,50}";
			this.revM_REIN_CONTACT_ADDRESS_1.ValidationExpression="^[a-zA-Z0-9]{1,50}";
			this.revM_REIN_CONTACT_ADDRESS_2.ValidationExpression="^[a-zA-Z0-9]{1,50}";

			this.revM_REIN_CONTACT_CITY.ErrorMessage= "Please eneter valid city";
			this.revM_REIN_CONTACT_ADDRESS_1.ErrorMessage="Please eneter valid address";
			this.revM_REIN_CONTACT_ADDRESS_2.ErrorMessage="Please eneter valid address";

			*/
//			this.revREIN_TIV_FROM.ValidationExpression="^[0-9]{13}";
//			this.revREIN_TIV_TO.ValidationExpression="^[0-9]{13}";
			this.revREIN_TIV_GROUP_CODE.ValidationExpression="^[0-9]{2}";
			this.revREIN_TIV_EFFECTIVE_DATE.ValidationExpression		= aRegExpDate;

//			this.revREIN_TIV_FROM.ErrorMessage="Please enter 10 digit number";
//			this.revREIN_TIV_TO.ErrorMessage="Please enter 10 digit number";
			this.revREIN_TIV_GROUP_CODE.ErrorMessage="Please enter 2 digit number";
			this.revREIN_TIV_EFFECTIVE_DATE.ErrorMessage	    =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

            revREIN_TIV_FROM.ValidationExpression = aRegExpBaseCurrencyformat; ;
			revREIN_TIV_FROM.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
            revREIN_TIV_TO.ValidationExpression = aRegExpBaseCurrencyformat ; ;
			revREIN_TIV_TO.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");


			rfvREIN_TIV_EFFECTIVE_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
			rfvContract_ID.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("988");
			rfvREIN_TIV_FROM.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("994");
			rfvREIN_TIV_TO.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("995");		
			rfvREIN_TIV_GROUP_CODE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("996");
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
		private ClsTIVGroupInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Maintenance.Reinsurance.ClsTIVGroupInfo objTIVGroupInfo;
			objTIVGroupInfo = new ClsTIVGroupInfo();
			
			objTIVGroupInfo.REIN_TIV_CONTRACT_NUMBER = this.cmbREIN_TIV_CONTRACT_NUMBER.SelectedValue;

			if(this.txtREIN_TIV_EFFECTIVE_DATE.Text.Trim()!="")
			objTIVGroupInfo.REIN_TIV_EFFECTIVE_DATE =ConvertToDate(this.txtREIN_TIV_EFFECTIVE_DATE.Text);
			objTIVGroupInfo.REIN_TIV_FROM = Convert.ToString(this.txtREIN_TIV_FROM.Text);
			objTIVGroupInfo.REIN_TIV_TO =Convert.ToString(this.txtREIN_TIV_TO.Text);
			objTIVGroupInfo.REIN_TIV_GROUP_CODE=this.txtREIN_TIV_GROUP_CODE.Text;


			//objReinsuranceContactInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	this.hidREIN_TIV_GROUP_ID.Value;
			//oldXML		= hidOldData.Value;
			//Returning the model object

			return objTIVGroupInfo;
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
				 objTIVGroup= new ClsTIVGroup();

				//Retreiving the form values into model class object
				ClsTIVGroupInfo objTIVGroupInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objTIVGroupInfo.CREATED_BY = int.Parse(GetUserId());
					objTIVGroupInfo.CREATED_DATETIME = DateTime.Now;
					//objTIVGroupInfo.IS_ACTIVE="Y"; 

					//Calling the add method of business layer class
					intRetVal = objTIVGroup.Add(objTIVGroupInfo);

					if(intRetVal>0)
					{
						this.hidREIN_TIV_GROUP_ID.Value = objTIVGroupInfo.REIN_TIV_GROUP_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						this.hidOldData.Value	= objTIVGroup.GetDataForPageControls(this.hidREIN_TIV_GROUP_ID.Value).GetXml();
                      
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
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsTIVGroupInfo  objOldTIVGroupInfo;
					objOldTIVGroupInfo = new ClsTIVGroupInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldTIVGroupInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					if(strRowId!="")
						objTIVGroupInfo.REIN_TIV_GROUP_ID = int.Parse(strRowId);
					objTIVGroupInfo.MODIFIED_BY = int.Parse(GetUserId());
					objTIVGroupInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    

					//Updating the record using business layer class object
					intRetVal	= objTIVGroup.Update(objOldTIVGroupInfo,objTIVGroupInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						this.hidOldData.Value	= objTIVGroup.GetDataForPageControls(strRowId).GetXml();
                   
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
				if(objTIVGroup!= null)
					objTIVGroup.Dispose();
			}

			
		}
	
		#endregion

		# region GENERATE XML
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string REIN_TIV_GROUP_ID)
		{
			string strREIN_TIV_GROUP_ID=REIN_TIV_GROUP_ID;
            
			objTIVGroup=new ClsTIVGroup(); 
  
			
			if(strREIN_TIV_GROUP_ID!="" && strREIN_TIV_GROUP_ID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objTIVGroup.GetDataForPageControls(strREIN_TIV_GROUP_ID);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        txtREIN_TIV_EFFECTIVE_DATE.Text = ConvertToDateCulture(Convert.ToDateTime(ds.Tables[0].Rows[0]["REIN_TIV_EFFECTIVE_DATE"].ToString()));
                    }
                   

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
					if(objTIVGroup!= null)
						objTIVGroup.Dispose();
				}  
                
			}
                
		}

		# endregion GENERATE XML

		# region SET CAPTIONS
		private void SetCaptions()
		{
			this.capREIN_TIV_CONTRACT_NUMBER.Text		=		objResourceMgr.GetString("cmbREIN_TIV_CONTRACT_NUMBER");
			this.capREIN_TIV_EFFECTIVE_DATE.Text		=		objResourceMgr.GetString("txtREIN_TIV_EFFECTIVE_DATE");
			this.capREIN_TIV_FROM.Text					=		objResourceMgr.GetString("txtREIN_TIV_FROM");
			this.capREIN_TIV_TO.Text					=		objResourceMgr.GetString("txtREIN_TIV_TO");
			this.capREIN_TIV_GROUP_CODE.Text			=		objResourceMgr.GetString("txtREIN_TIV_GROUP_CODE");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333"); //sneha
			//END Harmanjeet
		}

		# endregion SET CAPTIONS

//		#region DEACTIVATE ACTIVATE BUTTON CLICK
//
//		private void btnActivate_Click(object sender, System.EventArgs e)
//		{
//			objTIVGroup = new ClsTIVGroup();
//			
//						
//			if(this.btnActivate.Text=="Deactivate")
//			{
//						  
//				int intStatusCheck=objTIVGroup.GetDeactivateActivate(this.hidREIN_TIV_GROUP_ID.Value.ToString(),"N");
//				btnActivate.Text="Activate";
//				//this.btnDelete.Visible=false;
//				lblMessage.Visible = true;
//				lblMessage.Text ="Information deactivated successfully.";
//				SetReadOnly();
//			}
//			else
//			{
//				
//				int intStatusCheck=objTIVGroup.GetDeactivateActivate(this.hidREIN_TIV_GROUP_ID.Value.ToString(),"Y");
//				btnActivate.Text="Deactivate";
//				//this.btnDelete.Visible=true;
//				lblMessage.Visible = true;
//				lblMessage.Text ="Information activated successfully.";
//				SetReadOnlyfalse();
//
//			}
//		
//		}
//#endregion
		#region DEACTIVATE ACTIVATE BUTTON CLICK
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				//objDepartment =  new ClsDepartment();
				Cms.BusinessLayer.BlCommon.Reinsurance.ClsTIVGroup objTIVGroup = new ClsTIVGroup();
				Model.Maintenance.Reinsurance.ClsTIVGroupInfo objTIVGroupInfo;
				objTIVGroupInfo = GetFormValue();
				
				string strRetVal = "";
				string strCustomInfo ="";
				strCustomInfo = "Contract #:" + cmbREIN_TIV_CONTRACT_NUMBER.Items[cmbREIN_TIV_CONTRACT_NUMBER.SelectedIndex].Text +"<br>"
					+"Total Insurance Value From:" + objTIVGroupInfo.REIN_TIV_FROM+"<br>"
					+"Total Insurance Value To:" + objTIVGroupInfo.REIN_TIV_TO ;
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Reinsurance TIV Group Has Been Deactivated Successfully.";
					objTIVGroup.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = objTIVGroup.ActivateDeactivate(hidREIN_TIV_GROUP_ID.Value,"N",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Reinsurance TIV Group Has Been Activated Successfully.";
					objTIVGroup.TransactionInfoParams = objStuTransactionInfo;
					objTIVGroup.ActivateDeactivate(hidREIN_TIV_GROUP_ID.Value,"Y",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				
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
				if(objTIVGroup!= null)
					objTIVGroup.Dispose();
			}
		}
		#endregion
		private void GetOldDataXML()
		{
			Cms.BusinessLayer.BlCommon.Reinsurance.ClsTIVGroup objTIVGroup = new ClsTIVGroup();
			if (hidREIN_TIV_GROUP_ID.Value.ToString() != "" )
			{
				this.hidOldData.Value	= objTIVGroup.GetDataForPageControls(strRowId).GetXml();
			}
		}
		

		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				objTIVGroup = new ClsTIVGroup();
				DataSet oDs = objTIVGroup.GetDataForPageControls(this.hidREIN_TIV_GROUP_ID.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
						
					this.txtREIN_TIV_EFFECTIVE_DATE.Text=ConvertToDateCulture(Convert.ToDateTime(oDr["REIN_TIV_EFFECTIVE_DATE"].ToString()));
					this.txtREIN_TIV_FROM.Text=oDr["REIN_TIV_FROM"].ToString();
					this.txtREIN_TIV_TO.Text=oDr["REIN_TIV_TO"].ToString();
					this.txtREIN_TIV_GROUP_CODE.Text=oDr["REIN_TIV_GROUP_CODE"].ToString();

//					if(oDr["IS_ACTIVE"].ToString()=="Y")
//					{
//						btnActivateDeactivate.Text="Deactivate";
//						//this.btnDelete.Visible=true;
//						
//					}
//					if(oDr["IS_ACTIVE"].ToString()=="N")
//					{
//						btnActivate.Text="Activate";
//						//this.btnDelete.Visible=false;
//						SetReadOnly();
//					}
					
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
			this.txtREIN_TIV_EFFECTIVE_DATE.ReadOnly=true;
			this.txtREIN_TIV_FROM.ReadOnly=true;
			this.txtREIN_TIV_TO.ReadOnly=true;
			this.txtREIN_TIV_GROUP_CODE.ReadOnly=true;
		}
		private void SetReadOnlyfalse()
		{
			this.txtREIN_TIV_EFFECTIVE_DATE.ReadOnly=false;
			this.txtREIN_TIV_FROM.ReadOnly=false;
			this.txtREIN_TIV_TO.ReadOnly=false;
			this.txtREIN_TIV_GROUP_CODE.ReadOnly=false;
		}
		# endregion G E T   D A T A   F O R   E D I T   M O D E

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

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			objTIVGroup = new ClsTIVGroup();
			int intStatusCheck=objTIVGroup.Delete(this.hidREIN_TIV_GROUP_ID.Value.ToString());
			if (intStatusCheck > 0)
			{
				//Deleted successfully
				lblDelete.Visible = true;
				lblDelete.Text ="Record has been deleted successfully";
				hidREIN_TIV_GROUP_ID.Value = "";
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				tblBody.Attributes.Add("style","display:none");
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
	}
}








