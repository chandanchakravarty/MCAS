/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date			: -	8/3/2005 2:18:09 PM 	
Modification History
<Modified Date			: - 22-Apr-2010
<Modified By			: - Charles Gomes
<Purpose				: - Added Multilingual Support for Activate/Deactivate Button
*******************************************************************************************/
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
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddLookup.
	/// </summary>
	public class AddLookup : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capLOOKUP_VALUE_CODE;
		protected System.Web.UI.WebControls.TextBox txtLOOKUP_VALUE_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOOKUP_VALUE_CODE;
		protected System.Web.UI.WebControls.Label capLOOKUP_VALUE_DESC;
		protected System.Web.UI.WebControls.TextBox txtLOOKUP_VALUE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOOKUP_VALUE_DESC;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
        protected System.Web.UI.WebControls.Label capMessages;
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookUp_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOOKUPUNIQUE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOOKUP_UNIQUE_ID;
		protected System.Web.UI.WebControls.Label capLOOKUP_DESCRIPTION;
		protected System.Web.UI.WebControls.DropDownList cmbLOOKUP_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOOKUP_ID;
		protected System.Web.UI.WebControls.Label lblCategoryCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookUp_Name;
		protected System.Web.UI.WebControls.Label capLOOKUP_NAME;
		protected System.Web.UI.WebControls.Label lblLooUpDesc;
		protected System.Web.UI.WebControls.Label lblLooKUpDesc;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookUp_Desc;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookUpValue_Desc;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledForEnter;
		//Defining the business layer class object
		ClsLookup ObjLookUp;
		//END:*********** Local variables *************

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvLOOKUP_VALUE_CODE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvLOOKUP_VALUE_DESC.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvLOOKUP_ID.ErrorMessage                   =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			
			base.ScreenId="209_0";
			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=CmsButtonType.Write;
			btnReset.PermissionString=gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass=CmsButtonType.Write;
			btnActivateDeactivate.PermissionString=gstrSecurityXML;

			btnSave.CmsButtonClass=CmsButtonType.Write;
			btnSave.PermissionString=gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddLookup" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{                
				SetHiddenFields();
                
				GetOldDataXML();
				SetCaptions();
				FillCombo();
				if (hidOldData.Value != "" || hidCalledForEnter.Value == "true")
				{
					DataTable dt;
					if (hidCalledForEnter.Value == "true")
					{
						rfvLOOKUP_ID.Enabled=false;
						dt=ClsLookup.GetLookupTableData(hidLookUp_Name.Value);
						hidLookUp_ID.Value=dt.Rows[0]["LOOKUP_ID"].ToString();
					}
					else
					{
						dt=ClsLookup.GetLookupTableData(int.Parse(hidLookUp_ID.Value));
					}
//					lblCategoryCode.Text=hidLookUp_Name.Value;LOOKUP_DESC
					//lblLooKUpDesc.Text=hidLookUp_Name.Value + " - " + hidLookUp_Desc.Value;
					lblLooKUpDesc.Text=dt.Rows[0]["LOOKUP_DESC"].ToString();
				}
			}
            //Added by Charles for Multilingual Support          
            if ((hidLOOKUP_UNIQUE_ID.Value.ToUpper().Trim() == "NEW" || hidLOOKUP_UNIQUE_ID.Value.ToUpper().Trim() == "0") && hidOldData.Value =="")
            {
                imgSelect.Attributes.Add("onclick", "javascript:ShowMultiLingualPopup('LOOKUP_UNIQUE_ID','" + 0 + "','MNT_LOOKUP_VALUES','MNT_LOOKUP_VALUES_MULTILINGUAL','LOOKUP_VALUE_DESC');return false;");
            }
            else
            {
                if (imgSelect.Attributes["onclick"]!=null)
                    imgSelect.Attributes.Remove("onclick");

                imgSelect.Attributes.Add("onclick", "javascript:ShowMultiLingualPopup('LOOKUP_UNIQUE_ID','" + hidLOOKUP_UNIQUE_ID.Value + "','MNT_LOOKUP_VALUES','MNT_LOOKUP_VALUES_MULTILINGUAL','LOOKUP_VALUE_DESC');return false;");
            }//Added till here

            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
            
		}
		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Maintenance.ClsLookupInfo GetFormValue()
		{
			ClsLookupInfo ObjLookUpInfo;
			ObjLookUpInfo = new ClsLookupInfo();
			
			ObjLookUpInfo.LOOKUP_VALUE_CODE=txtLOOKUP_VALUE_CODE.Text;
			ObjLookUpInfo.LOOKUP_VALUE_DESC=txtLOOKUP_VALUE_DESC.Text;

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidLOOKUP_UNIQUE_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			strFormSaved=hidFormSaved.Value;
			if (hidOldData.Value != "")
			{
				strRowId=hidLookUp_ID.Value;
				ObjLookUpInfo.LOOKUP_UNIQUE_ID=int.Parse(hidLOOKUP_UNIQUE_ID.Value);
			}
			else
			{
				strRowId="New";
				
				if (hidCalledForEnter.Value == "true")
				{
					ObjLookUpInfo.LOOKUP_ID=int.Parse(hidLookUp_ID.Value); 
				}
				else
				{
					ObjLookUpInfo.LOOKUP_ID=int.Parse(cmbLOOKUP_ID.SelectedValue);				
				}				
			}
			oldXML=hidOldData.Value;
			return ObjLookUpInfo;
		}
		#endregion

		private void FillCombo()
		{
			DataTable dt=ClsLookup.GetLookupTableData();
			cmbLOOKUP_ID.DataSource=dt.DefaultView;
			cmbLOOKUP_ID.DataTextField="LOOKUP_DESC";
			cmbLOOKUP_ID.DataValueField="LOOKUP_ID";
			cmbLOOKUP_ID.DataBind();	
			cmbLOOKUP_ID.Items.Insert(0,"");
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
				ObjLookUp = new ClsLookup();

				//Retreiving the form values into model class object
				ClsLookupInfo ObjLookUpInfo = GetFormValue();
				ObjLookUpInfo.IS_ACTIVE = "Y";

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					int CreatedBy= int.Parse(GetUserId());
					//ClsLookup.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = ObjLookUp.Add(ObjLookUpInfo,CreatedBy);

					if(intRetVal>0)
					{
						hidLOOKUP_UNIQUE_ID.Value = ObjLookUpInfo.LOOKUP_UNIQUE_ID.ToString();
						hidLookUp_ID.Value= ObjLookUpInfo.LOOKUP_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						hidOldData.Value =ClsLookup.GetLookUpDetailXml(int.Parse(hidLOOKUP_UNIQUE_ID.Value));

                        //Added by Charles for Multilingual Support    
                        if (imgSelect.Attributes["onclick"] != null)
                            imgSelect.Attributes.Remove("onclick");
                        imgSelect.Attributes.Add("onclick", "javascript:ShowMultiLingualPopup('LOOKUP_UNIQUE_ID','" + hidLOOKUP_UNIQUE_ID.Value + "','MNT_LOOKUP_VALUES','MNT_LOOKUP_VALUES_MULTILINGUAL','LOOKUP_VALUE_DESC');return false;");
                        //Added till here
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
						btnActivateDeactivate.Enabled = false;
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
					ClsLookupInfo objOldLookUpInfo;
					objOldLookUpInfo = new ClsLookupInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldLookUpInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					//ObjLookUpInfo.LOOKUP_UNIQUE_ID = strRowId;
					ObjLookUpInfo.MODIFIED_BY = int.Parse(GetUserId());
					ObjLookUpInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					ObjLookUpInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= ObjLookUp.Update(objOldLookUpInfo,ObjLookUpInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value =ClsLookup.GetLookUpDetailXml(int.Parse(hidLOOKUP_UNIQUE_ID.Value));
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
				if (hidCalledForEnter.Value == "true" && intRetVal > 0)
				{
					string strScript = @"<script>" + 
						"window.opener.document.APP_DRIVER_DETAILS.submit()" +					
						"</script>";

                    if (!ClientScript.IsStartupScriptRegistered("Refresh"))
					{
                        ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strScript);
					}	
				
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
				if(ObjLookUp!= null)
					ObjLookUp.Dispose();

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
			}
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{				
			ClsLookup objLookup=new ClsLookup();
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				//objUser =  new ClsUser();
				
				Model.Maintenance.ClsLookupInfo objLookupInfo;
				objLookupInfo = GetFormValue();
				string strCustomInfo ="";
				string lookupCode = "";
				if(cmbLOOKUP_ID.Visible == true)
					lookupCode = cmbLOOKUP_ID.Items[cmbLOOKUP_ID.SelectedIndex].Text;
				else
					lookupCode = lblLooKUpDesc.Text;
				strCustomInfo =  "; Category Code:" + lookupCode +"<br>"
								+"; Value Code:" + objLookupInfo.LOOKUP_VALUE_CODE +"<br>"
								+"; Value Description:" + objLookupInfo.LOOKUP_VALUE_DESC;
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "LookUp Deactivated Successfully.";
					objLookup.TransactionInfoParams = objStuTransactionInfo;
					objLookup.ActivateDeactivate(hidLOOKUP_UNIQUE_ID.Value,"N",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "LookUp Activated Successfully.";
					objLookup.TransactionInfoParams = objStuTransactionInfo;
					objLookup.ActivateDeactivate(hidLOOKUP_UNIQUE_ID.Value,"Y",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"1";
				GetOldDataXML();
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
				//				if(objUser!= null)
				//					objUser.Dispose();

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
			}
		}
		#endregion
		private void SetCaptions()
		{
			capLOOKUP_VALUE_CODE.Text=objResourceMgr.GetString("txtLOOKUP_VALUE_CODE");
			capLOOKUP_VALUE_DESC.Text=objResourceMgr.GetString("txtLOOKUP_VALUE_DESC");
			capLOOKUP_DESCRIPTION.Text=objResourceMgr.GetString("capLOOKUP_DESCRIPTION");
			capLOOKUP_NAME.Text=objResourceMgr.GetString("capLOOKUP_NAME");
		}
		private void GetOldDataXML()
		{
			hidOldData.Value =ClsLookup.GetLookUpDetailXml(int.Parse(hidLOOKUP_UNIQUE_ID.Value));
            if (hidOldData.Value!="" && hidOldData.Value!=null)
            {
                hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value);
            }            
		}
		private void SetHiddenFields()
		{
			if (Request.QueryString["LOOKUP_UNIQUE_ID"] != null && Request.QueryString["LOOKUP_UNIQUE_ID"].ToString() != "")
			{ 				
				hidLOOKUP_UNIQUE_ID.Value=Request.QueryString["LOOKUP_UNIQUE_ID"].ToString();                
			}			
			if (Request.QueryString["LookUpName"] != null && Request.QueryString["LookUpName"].ToString() != "")
			{
				hidLookUp_Name.Value=Request.QueryString["LookUpName"].ToString();
			}			
			if (Request.QueryString["LookUpDesc"] != null && Request.QueryString["LookUpDesc"].ToString() != "")
			{
				hidLookUp_Desc.Value=Request.QueryString["LookUpDesc"].ToString();
			}			
			if (Request.QueryString["LookUpValueDesc"] != null && Request.QueryString["LookUpValueDesc"].ToString() != "")
			{
				hidLookUpValue_Desc.Value=Request.QueryString["LookUpValueDesc"].ToString();
			}			
			if (Request.QueryString["LookUpID"] != null && Request.QueryString["LookUpID"].ToString() != "")
			{
				hidLookUp_ID.Value=Request.QueryString["LookUpID"].ToString();
			}
			if (Request.QueryString["CalledForEnter"] != null && Request.QueryString["CalledForEnter"].ToString() != "")
			{
				hidCalledForEnter.Value=Request.QueryString["CalledForEnter"].ToString();
			}
		}
	}
}
