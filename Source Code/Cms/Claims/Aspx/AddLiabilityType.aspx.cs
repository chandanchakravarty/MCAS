/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> May 09,2006
	<End Date				: - >
	<Description			: - > Page is used to display liability types
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History


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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;




namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddLiabilityType : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		private const int EXPERT_SERVICE_PROVIDER_ID=9;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capPREMISES_INSURED;
		protected System.Web.UI.WebControls.DropDownList cmbPREMISES_INSURED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPREMISES_INSURED;
		protected System.Web.UI.WebControls.Label capOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capTYPE_OF_PREMISES;
		protected System.Web.UI.WebControls.TextBox txtTYPE_OF_PREMISES;		
		protected Cms.CmsWeb.Controls.CmsButton btnReset;		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLIABILITY_TYPE_ID;
		protected System.Web.UI.WebControls.CustomValidator csvTYPE_OF_PREMISES;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{				
			base.ScreenId="300";

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			

			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddLiabilityType" ,System.Reflection.Assembly.GetExecutingAssembly());
			

			if(!Page.IsPostBack)
			{		
				lblMessage.Visible = false;
				GetQueryStringValues();
				LoadDropDowns();
				GetOldDataXML(true);
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();								
			}
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML(bool LoadDataFlag)
		{
			if(hidLIABILITY_TYPE_ID.Value!="" && hidLIABILITY_TYPE_ID.Value!="0")
			{
				DataTable dtData = ClsLiabilityType.GetXmlForPageControls(int.Parse(hidLIABILITY_TYPE_ID.Value),int.Parse(hidCLAIM_ID.Value));
				hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtData);
				if(LoadDataFlag)				
					LoadData(dtData);
			}
			else
				hidOldData.Value	=	"";
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request.QueryString["LIABILITY_TYPE_ID"]!=null && Request.QueryString["LIABILITY_TYPE_ID"].ToString()!="")
			{
				hidLIABILITY_TYPE_ID.Value = Request.QueryString["LIABILITY_TYPE_ID"].ToString();
			}
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
			{
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			}
		}

		

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvOTHER_DESCRIPTION.ErrorMessage		=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("550");
			rfvPREMISES_INSURED.ErrorMessage		=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("747");
			csvTYPE_OF_PREMISES.ErrorMessage		=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("759");
		}

		#endregion

		private void LoadData(DataTable dtLoadData)
		{
			if(dtLoadData==null || dtLoadData.Rows.Count<1)
				return;

			if(dtLoadData.Rows[0]["PREMISES_INSURED"]!=null && dtLoadData.Rows[0]["PREMISES_INSURED"].ToString()!="" && dtLoadData.Rows[0]["PREMISES_INSURED"].ToString()!="0")
				cmbPREMISES_INSURED.SelectedValue = dtLoadData.Rows[0]["PREMISES_INSURED"].ToString();
			txtOTHER_DESCRIPTION.Text = dtLoadData.Rows[0]["OTHER_DESCRIPTION"].ToString();
			txtTYPE_OF_PREMISES.Text  = dtLoadData.Rows[0]["TYPE_OF_PREMISES"].ToString();
					
			
		}

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsLiabilityType objLiabilityType = new ClsLiabilityType();				

				//Retreiving the form values into model class object
				ClsLiabilityTypeInfo objLiabilityTypeInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objLiabilityTypeInfo.CREATED_BY = int.Parse(GetUserId());
					objLiabilityTypeInfo.CREATED_DATETIME = DateTime.Now;
					objLiabilityTypeInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objLiabilityType.Add(objLiabilityTypeInfo);

					if(intRetVal>0)
					{
						hidLIABILITY_TYPE_ID.Value = objLiabilityTypeInfo.LIABILITY_TYPE_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(false);
					}
					else if(intRetVal == -1) //Duplicate Authority Limit
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsLiabilityTypeInfo objOldLiabilityTypeInfo = new ClsLiabilityTypeInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldLiabilityTypeInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objLiabilityTypeInfo.MODIFIED_BY = int.Parse(GetUserId());
					objLiabilityTypeInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objLiabilityType.Update(objOldLiabilityTypeInfo,objLiabilityTypeInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML(false);
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
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
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}

		
		private void SetCaptions()
		{
			capPREMISES_INSURED.Text				=		objResourceMgr.GetString("cmbPREMISES_INSURED");
			capOTHER_DESCRIPTION.Text				=		objResourceMgr.GetString("txtOTHER_DESCRIPTION");
			capTYPE_OF_PREMISES.Text			=		objResourceMgr.GetString("txtTYPE_OF_PREMISES");						
		}



	

		#region GetFormValue
		private ClsLiabilityTypeInfo GetFormValue()
		{
			ClsLiabilityTypeInfo objLiabilityTypeInfo = new ClsLiabilityTypeInfo();
			if(cmbPREMISES_INSURED.SelectedItem!=null && cmbPREMISES_INSURED.SelectedItem.Value!="")
				objLiabilityTypeInfo.PREMISES_INSURED = int.Parse(cmbPREMISES_INSURED.SelectedItem.Value);
			/*Possible values for Premises Insured Dropdown
			9809>Other
			9807>Owner
			9808>Tenant*/
			if(objLiabilityTypeInfo.PREMISES_INSURED == 9809)
				objLiabilityTypeInfo.OTHER_DESCRIPTION = txtOTHER_DESCRIPTION.Text.Trim();
			else
				objLiabilityTypeInfo.OTHER_DESCRIPTION = string.Empty;
			objLiabilityTypeInfo.TYPE_OF_PREMISES = txtTYPE_OF_PREMISES.Text.Trim();
			objLiabilityTypeInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
			if(hidLIABILITY_TYPE_ID.Value.ToUpper()=="NEW" || hidLIABILITY_TYPE_ID.Value=="0" || hidLIABILITY_TYPE_ID.Value=="")
				strRowId="NEW";
			else
			{
				strRowId=hidLIABILITY_TYPE_ID.Value;
				objLiabilityTypeInfo.LIABILITY_TYPE_ID		=	int.Parse(hidLIABILITY_TYPE_ID.Value);
			}
			return objLiabilityTypeInfo;
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			cmbPREMISES_INSURED.DataSource	=	Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PREINS");
			cmbPREMISES_INSURED.DataTextField=	"LookupDesc";
			cmbPREMISES_INSURED.DataValueField=	"LookupID";
			cmbPREMISES_INSURED.DataBind();
			cmbPREMISES_INSURED.SelectedIndex=2;
			
		}
		#endregion
	}
}
