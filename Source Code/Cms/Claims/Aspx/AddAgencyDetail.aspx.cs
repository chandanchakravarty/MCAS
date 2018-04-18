/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> May 26,2006
	<End Date				: - >
	<Description			: - > Page is used to add/edit agency records
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
	public class AddAgencyDetail : Cms.Claims.ClaimBase
	{
		
		protected string strRowId="NEW";
		private bool LOAD_OLD_DATA = true;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capAGENCY_SUB_CODE;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_SUB_CODE;
		protected System.Web.UI.WebControls.Label capAGENCY_CODE;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_CODE;
		protected System.Web.UI.WebControls.Label capAGENCY_CUSTOMER_ID;
		protected System.Web.UI.WebControls.Label capAGENCY_PHONE;
		protected System.Web.UI.WebControls.Label capAGENCY_FAX;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_FAX;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_CUSTOMER_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_PHONE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_CODE;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_CUSTOMER_ID;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_FAX;
		
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************	
		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="306_8_0";
			
			lblMessage.Visible = false;
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddAgencyDetail" ,System.Reflection.Assembly.GetExecutingAssembly());
			

			if(!Page.IsPostBack)
			{			
				GetQueryStringValues();
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				GetOldDataXML(LOAD_OLD_DATA);
				SetCaptions();
				SetErrorMessages();								
			}
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML(bool LOAD_DATA_FLAG)
		{
			if(hidAGENCY_ID.Value!="" && hidAGENCY_ID.Value!="0")
			{
				DataTable dtOldData = 	ClsAgencyDetails.GetOldDataForPageControls(hidCLAIM_ID.Value,hidAGENCY_ID.Value);
				hidOldData.Value	=	Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
				if(LOAD_DATA_FLAG)
					LoadData(dtOldData);
			}
			else
				hidOldData.Value	=	"";
		}
		#endregion

		#region Get Query String Values
		private void GetQueryStringValues()
		{
			if(Request["AGENCY_ID"]!=null && Request["AGENCY_ID"].ToString()!="")
				hidAGENCY_ID.Value = Request["AGENCY_ID"].ToString();

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="" )
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			
		}
		#endregion

		#region Load Old Data value into fields
		private void LoadData(DataTable dtLoadData)
		{
			if(dtLoadData!=null && dtLoadData.Rows.Count>0)
			{
				txtAGENCY_SUB_CODE.Text = dtLoadData.Rows[0]["AGENCY_SUB_CODE"].ToString();
				txtAGENCY_CODE.Text  = dtLoadData.Rows[0]["AGENCY_CODE"].ToString();
				txtAGENCY_CUSTOMER_ID.Text = dtLoadData.Rows[0]["AGENCY_CUSTOMER_ID"].ToString();
				txtAGENCY_PHONE.Text  = dtLoadData.Rows[0]["AGENCY_PHONE"].ToString();
				txtAGENCY_FAX.Text = dtLoadData.Rows[0]["AGENCY_FAX"].ToString();				
			}
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

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			revAGENCY_PHONE.ValidationExpression		=  aRegExpPhone;
			revAGENCY_FAX.ValidationExpression			=  aRegExpFax;
			revAGENCY_PHONE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			revAGENCY_FAX.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			rfvAGENCY_CODE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvAGENCY_CUSTOMER_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");			
			
		}

		#endregion

		

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsAgencyDetails objAgencyDetails = new ClsAgencyDetails();				

				//Retreiving the form values into model class object
				ClsAgencyDetailsInfo objAgencyDetailsInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objAgencyDetailsInfo.CREATED_BY = int.Parse(GetUserId());
					objAgencyDetailsInfo.CREATED_DATETIME = DateTime.Now;
					objAgencyDetailsInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objAgencyDetails.Add(objAgencyDetailsInfo);

					if(intRetVal>0)
					{
						hidAGENCY_ID.Value = objAgencyDetailsInfo.AGENCY_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(!LOAD_OLD_DATA);
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
					ClsAgencyDetailsInfo objOldAgencyDetailsInfo = new ClsAgencyDetailsInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldAgencyDetailsInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objAgencyDetailsInfo.MODIFIED_BY = int.Parse(GetUserId());
					objAgencyDetailsInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objAgencyDetails.Update(objOldAgencyDetailsInfo,objAgencyDetailsInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML(!LOAD_OLD_DATA);
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
			capAGENCY_SUB_CODE.Text									=		objResourceMgr.GetString("txtAGENCY_SUB_CODE");			
			capAGENCY_CODE.Text						=		objResourceMgr.GetString("txtAGENCY_CODE");			
			capAGENCY_CUSTOMER_ID.Text							=		objResourceMgr.GetString("txtAGENCY_CUSTOMER_ID");			
			capAGENCY_PHONE.Text						=		objResourceMgr.GetString("txtAGENCY_PHONE");			
			capAGENCY_FAX.Text							=		objResourceMgr.GetString("txtAGENCY_FAX");						
		}
	

		#region GetFormValue
		private ClsAgencyDetailsInfo GetFormValue()
		{
			ClsAgencyDetailsInfo objAgencyDetailsInfo = new ClsAgencyDetailsInfo();
			objAgencyDetailsInfo.AGENCY_CODE		=	txtAGENCY_CODE.Text.Trim();
			objAgencyDetailsInfo.AGENCY_SUB_CODE	=	txtAGENCY_SUB_CODE.Text.Trim();
			objAgencyDetailsInfo.AGENCY_CUSTOMER_ID =	txtAGENCY_CUSTOMER_ID.Text.Trim();
			objAgencyDetailsInfo.AGENCY_PHONE		=	txtAGENCY_PHONE.Text.Trim();
			objAgencyDetailsInfo.AGENCY_FAX			=	txtAGENCY_FAX.Text.Trim();
			objAgencyDetailsInfo.CLAIM_ID			=	int.Parse(hidCLAIM_ID.Value);

			if(hidAGENCY_ID.Value=="" || hidAGENCY_ID.Value=="0" || hidAGENCY_ID.Value.ToUpper()=="NEW")
				strRowId = "NEW";
			else
			{
				objAgencyDetailsInfo.AGENCY_ID = int.Parse(hidAGENCY_ID.Value);
				strRowId = hidAGENCY_ID.Value;
			}
			return objAgencyDetailsInfo;
		}
		#endregion

		
	}
}
