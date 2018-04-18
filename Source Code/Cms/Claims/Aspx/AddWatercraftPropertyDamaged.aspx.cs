/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 20,2006
	<End Date				: - >
	<Description			: - > Page is used to assign limits to authority
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
	public class AddWatercraftPropertyDamaged : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		
		private string strRowId="";
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capDESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.WebControls.CustomValidator csvDESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_VEHICLE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_OWNER_NAME;		
		protected System.Web.UI.WebControls.Label capOTHER_VEHICLE;
		protected System.Web.UI.WebControls.DropDownList cmbOTHER_VEHICLE;
		protected System.Web.UI.WebControls.Label capOTHER_INSURANCE_NAME;
		protected System.Web.UI.WebControls.TextBox txtOTHER_INSURANCE_NAME;
		protected System.Web.UI.WebControls.Label capOTHER_OWNER_NAME;
		protected System.Web.UI.WebControls.TextBox txtOTHER_OWNER_NAME;
		protected System.Web.UI.WebControls.Label capADDRESS1;
		protected System.Web.UI.WebControls.TextBox txtADDRESS1;
		protected System.Web.UI.WebControls.Label capADDRESS2;
		protected System.Web.UI.WebControls.TextBox txtADDRESS2;
		protected System.Web.UI.WebControls.Label capCITY;
		protected System.Web.UI.WebControls.TextBox txtCITY;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.Label capZIP;
		protected System.Web.UI.WebControls.TextBox txtZIP;
		protected System.Web.UI.WebControls.Label capHOME_PHONE;
		protected System.Web.UI.WebControls.TextBox txtHOME_PHONE;
		protected System.Web.UI.WebControls.Label capWORK_PHONE;
		protected System.Web.UI.WebControls.TextBox txtWORK_PHONE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROPERTY_DAMAGED_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revGRG_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHOME_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWORK_PHONE;
		
		private bool LOAD_OLD_DATA=true;
		

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="306_7_0";
			
			lblMessage.Visible = false;
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddWatercraftPropertyDamaged" ,System.Reflection.Assembly.GetExecutingAssembly());
			

			if(!Page.IsPostBack)
			{			
				GetQueryStringValues();
				
				GetOldDataXML(LOAD_OLD_DATA);
				
				LoadDropDowns();				
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();								
			}
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML(bool flag)
		{
			DataTable dtOldData = new DataTable();;
			if(hidPROPERTY_DAMAGED_ID.Value!="" && hidPROPERTY_DAMAGED_ID.Value!="0")
			{
				dtOldData	=	ClsWatercraftPropertyDamaged.GetOldDataForPageControls(hidCLAIM_ID.Value,hidPROPERTY_DAMAGED_ID.Value);
				if(dtOldData!=null && dtOldData.Rows.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
					if(LOAD_OLD_DATA)
						LoadData(dtOldData);
				}
				else
					hidOldData.Value	=	"";
			}
			else
				hidOldData.Value	=	"";
		}
		#endregion

		private void LoadData(DataTable dtOldData)
		{
			if(dtOldData!=null && dtOldData.Rows.Count>0)
			{
				
				txtDESCRIPTION.Text				=		dtOldData.Rows[0]["DESCRIPTION"].ToString();
				txtOTHER_INSURANCE_NAME.Text	=		dtOldData.Rows[0]["OTHER_INSURANCE_NAME"].ToString();
				txtOTHER_OWNER_NAME.Text		=		dtOldData.Rows[0]["OTHER_OWNER_NAME"].ToString();
				if(dtOldData.Rows[0]["OTHER_VEHICLE"]!=null && dtOldData.Rows[0]["OTHER_VEHICLE"].ToString()!="" && dtOldData.Rows[0]["OTHER_VEHICLE"].ToString()!="0")
					cmbOTHER_VEHICLE.SelectedValue = dtOldData.Rows[0]["OTHER_VEHICLE"].ToString();
		
				txtADDRESS1.Text				=		dtOldData.Rows[0]["ADDRESS1"].ToString();
				txtADDRESS2.Text				=		dtOldData.Rows[0]["ADDRESS2"].ToString();
				txtCITY.Text					=		dtOldData.Rows[0]["CITY"].ToString();
				if(dtOldData.Rows[0]["STATE"]!=null && dtOldData.Rows[0]["STATE"].ToString()!="" && dtOldData.Rows[0]["STATE"].ToString()!="0")
					cmbSTATE.SelectedValue		=		dtOldData.Rows[0]["STATE"].ToString();
				txtZIP.Text						=		dtOldData.Rows[0]["ZIP"].ToString();
				txtHOME_PHONE.Text				=		dtOldData.Rows[0]["HOME_PHONE"].ToString();
				txtWORK_PHONE.Text				=		dtOldData.Rows[0]["WORK_PHONE"].ToString();
				
		
			}

		}


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
			rfvDESCRIPTION.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvOTHER_VEHICLE.ErrorMessage		=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvOTHER_OWNER_NAME.ErrorMessage	=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			csvDESCRIPTION.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			revGRG_ZIP.ValidationExpression	=  aRegExpZip;
			revGRG_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			revHOME_PHONE.ValidationExpression	= aRegExpPhone;
			revHOME_PHONE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			revWORK_PHONE.ValidationExpression	= aRegExpPhone;
			revWORK_PHONE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			
			
		}

		#endregion

		private void GetQueryStringValues()
		{
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			if(Request.QueryString["PROPERTY_DAMAGED_ID"]!=null && Request.QueryString["PROPERTY_DAMAGED_ID"].ToString()!="")
				hidPROPERTY_DAMAGED_ID.Value = Request.QueryString["PROPERTY_DAMAGED_ID"].ToString();
		}

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiADDRESS2g the return value of business class save function
				ClsWatercraftPropertyDamaged objWatercraftPropertyDamaged = new ClsWatercraftPropertyDamaged();				

				//RetreiADDRESS2g the form values into ADDRESS1 class object
				ClsWatercraftPropertyDamagedInfo objWatercraftPropertyDamagedInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objWatercraftPropertyDamagedInfo.CREATED_BY = int.Parse(GetUserId());
					objWatercraftPropertyDamagedInfo.CREATED_DATETIME = DateTime.Now;
					objWatercraftPropertyDamagedInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objWatercraftPropertyDamaged.Add(objWatercraftPropertyDamagedInfo);

					if(intRetVal>0)
					{
						hidPROPERTY_DAMAGED_ID.Value = objWatercraftPropertyDamagedInfo.PROPERTY_DAMAGED_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(!LOAD_OLD_DATA);
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
					//Creating the ADDRESS1 object for holding the Old data
					ClsWatercraftPropertyDamagedInfo objOldWatercraftPropertyDamagedInfo = new ClsWatercraftPropertyDamagedInfo();

					//Setting  the Old Page details(XML File containing old details) into the ADDRESS1 Object
					base.PopulateModelObject(objOldWatercraftPropertyDamagedInfo,hidOldData.Value);

					//Setting those values into the ADDRESS1 object which are not in the page					
					objWatercraftPropertyDamagedInfo.MODIFIED_BY = int.Parse(GetUserId());
					objWatercraftPropertyDamagedInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objWatercraftPropertyDamaged.Update(objOldWatercraftPropertyDamagedInfo,objWatercraftPropertyDamagedInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML(!LOAD_OLD_DATA);
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
			capSTATE.Text				=		objResourceMgr.GetString("cmbSTATE");
			capDESCRIPTION.Text				=		objResourceMgr.GetString("txtDESCRIPTION");
			capOTHER_VEHICLE.Text			=		objResourceMgr.GetString("cmbOTHER_VEHICLE");			
			capOTHER_OWNER_NAME.Text				=		objResourceMgr.GetString("txtOTHER_OWNER_NAME");			
			capHOME_PHONE.Text				=		objResourceMgr.GetString("txtHOME_PHONE");
			capOTHER_INSURANCE_NAME.Text	=		objResourceMgr.GetString("txtOTHER_INSURANCE_NAME");
			capADDRESS1.Text		=		objResourceMgr.GetString("txtADDRESS1");
			capADDRESS2.Text	=		objResourceMgr.GetString("txtADDRESS2");
			capCITY.Text	=		objResourceMgr.GetString("txtCITY");
			capZIP.Text		=		objResourceMgr.GetString("txtZIP");
			capWORK_PHONE.Text		=		objResourceMgr.GetString("txtWORK_PHONE");
			
		}
	

		#region GetFormValue
		private ClsWatercraftPropertyDamagedInfo GetFormValue()
		{
			ClsWatercraftPropertyDamagedInfo objWatercraftPropertyDamagedInfo		=		 new ClsWatercraftPropertyDamagedInfo();
			objWatercraftPropertyDamagedInfo.DESCRIPTION							=		 txtDESCRIPTION.Text.Trim();
			objWatercraftPropertyDamagedInfo.OTHER_INSURANCE_NAME					=		txtOTHER_INSURANCE_NAME.Text.Trim();
			objWatercraftPropertyDamagedInfo.OTHER_OWNER_NAME						=		txtOTHER_OWNER_NAME.Text.Trim();
			if(cmbOTHER_VEHICLE.SelectedItem!=null && cmbOTHER_VEHICLE.SelectedItem.Value!="")
				objWatercraftPropertyDamagedInfo.OTHER_VEHICLE						=		cmbOTHER_VEHICLE.SelectedItem.Value;
		
			objWatercraftPropertyDamagedInfo.ADDRESS1								=		txtADDRESS1.Text.Trim();
			objWatercraftPropertyDamagedInfo.ADDRESS2								=		txtADDRESS2.Text.Trim();
			objWatercraftPropertyDamagedInfo.CITY									=		txtCITY.Text.Trim();
			if(cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedItem.Value!="")
				objWatercraftPropertyDamagedInfo.STATE								=		int.Parse(cmbSTATE.SelectedItem.Value);
			objWatercraftPropertyDamagedInfo.ZIP									=		txtZIP.Text.Trim();
			objWatercraftPropertyDamagedInfo.HOME_PHONE								=		txtHOME_PHONE.Text.Trim();
			objWatercraftPropertyDamagedInfo.WORK_PHONE								=		txtWORK_PHONE.Text.Trim();
			objWatercraftPropertyDamagedInfo.CLAIM_ID								=		int.Parse(hidCLAIM_ID.Value);
		
			
			if(hidPROPERTY_DAMAGED_ID.Value.ToUpper()=="NEW" || hidPROPERTY_DAMAGED_ID.Value=="0")
				strRowId="NEW";
			else
			{
				strRowId=hidPROPERTY_DAMAGED_ID.Value;
				objWatercraftPropertyDamagedInfo.PROPERTY_DAMAGED_ID		=	int.Parse(hidPROPERTY_DAMAGED_ID.Value);
			}
			return objWatercraftPropertyDamagedInfo;
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			cmbSTATE.DataSource				= Cms.CmsWeb.ClsFetcher.State;
			cmbSTATE.DataTextField			= "State_Name";
			cmbSTATE.DataValueField			= "State_Id";
			cmbSTATE.DataBind();
			cmbSTATE.Items.Insert(0,"");
		}
		#endregion
	}
}
