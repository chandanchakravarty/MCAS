/******************************************************************************************
<Author					: -  Sumit Chhabra
<Start Date				: -	 November 08, 2006
<End Date				: -	
<Description			: - Add/Edit page for CLM_RECREATIONAL_VEHICLES
<Review Date			: - 
<Reviewed By			: - 	
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application.HomeOwners;
using System.Resources;
using Cms.CmsWeb;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// Summary description for AddRecrVehicle.
	/// </summary>
	public class AddRecrVehicle : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.Label capYEAR;
		protected System.Web.UI.WebControls.TextBox txtYEAR;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR;
		protected System.Web.UI.WebControls.RangeValidator rngCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		private bool LOAD_OLD_DATA=true;
		protected System.Web.UI.WebControls.Label capSERIAL;
		protected System.Web.UI.WebControls.TextBox txtSERIAL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSERIAL;
		protected System.Web.UI.WebControls.Label capSTATE_REGISTERED;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_REGISTERED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_REGISTERED;
		protected System.Web.UI.WebControls.Label capVEHICLE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_TYPE;
		protected System.Web.UI.WebControls.Label capHORSE_POWER;
		protected System.Web.UI.WebControls.TextBox txtHORSE_POWER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHORSE_POWER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHORSE_POWER;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		protected System.Web.UI.WebControls.CustomValidator csvREMARKS;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPolicyRecVeh;	
		protected System.Web.UI.WebControls.Label capPOLICY_REC_VEH;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_REC_VEH;
		private string strRowId;	
		
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			//Done for Itrack Issue 6553 on 13 Oct 09
			base.ScreenId="306_3_0";

			btnReset.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;	
				
			btnSave.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;	
			lblMessage.Visible = false;
			if(!IsPostBack)
			{
				btnReset.Attributes.Add("onClick","javascript:return ResetTheForm();");				
				GetQueryStringValues();
				LoadDropdowns();
				SetValidators();
				SetCaptions();
				GetOldDataXML(LOAD_OLD_DATA);
			}
			
		}

		#region GetFormValue
		private ClsRecrVehiclesInfo GetFormValue()
		{
			ClsRecrVehiclesInfo objRecrVehiclesInfo		=		 new ClsRecrVehiclesInfo();

			objRecrVehiclesInfo.COMPANY_ID_NUMBER = int.Parse(txtCOMPANY_ID_NUMBER.Text.Trim());
			objRecrVehiclesInfo.YEAR = int.Parse(txtYEAR.Text.Trim());
			objRecrVehiclesInfo.MAKE = txtMAKE.Text.Trim();
			objRecrVehiclesInfo.MODEL = txtMODEL.Text.Trim();
			objRecrVehiclesInfo.SERIAL = txtSERIAL.Text.Trim();
			if(cmbSTATE_REGISTERED.SelectedItem!=null && cmbSTATE_REGISTERED.SelectedItem.Value!="")
				objRecrVehiclesInfo.STATE_REGISTERED = int.Parse(cmbSTATE_REGISTERED.SelectedItem.Value);
			if(cmbVEHICLE_TYPE.SelectedItem!=null && cmbVEHICLE_TYPE.SelectedItem.Value!="")
				objRecrVehiclesInfo.VEHICLE_TYPE = int.Parse(cmbVEHICLE_TYPE.SelectedItem.Value);
			objRecrVehiclesInfo.HORSE_POWER = txtHORSE_POWER.Text.Trim();
			objRecrVehiclesInfo.REMARKS = txtREMARKS.Text.Trim();
			objRecrVehiclesInfo.CLAIM_ID = int.Parse(hidCLAIM_ID.Value);
			if(cmbPOLICY_REC_VEH.SelectedItem!=null && cmbPOLICY_REC_VEH.SelectedItem.Value!="")
				objRecrVehiclesInfo.POL_REC_VEH_ID = int.Parse(cmbPOLICY_REC_VEH.SelectedItem.Value);
			
			if(hidREC_VEH_ID.Value.ToUpper()=="NEW" || hidREC_VEH_ID.Value=="0")
				strRowId="NEW";
			else
			{
				strRowId=hidREC_VEH_ID.Value;
				objRecrVehiclesInfo.REC_VEH_ID = int.Parse(hidREC_VEH_ID.Value);
			}
			return objRecrVehiclesInfo;
		}
		#endregion

		private void LoadData(DataTable dtOldData)
		{
			if(dtOldData!=null && dtOldData.Rows.Count>0)
			{
				if(dtOldData.Rows[0]["STATE_REGISTERED"]!=null && dtOldData.Rows[0]["STATE_REGISTERED"].ToString()!="" && dtOldData.Rows[0]["STATE_REGISTERED"].ToString()!="0")
					cmbSTATE_REGISTERED.SelectedValue		=		dtOldData.Rows[0]["STATE_REGISTERED"].ToString();
				
				txtCOMPANY_ID_NUMBER.Text		=		dtOldData.Rows[0]["COMPANY_ID_NUMBER"].ToString();	
				txtYEAR.Text					=		dtOldData.Rows[0]["YEAR"].ToString();
				txtMAKE.Text					=		dtOldData.Rows[0]["MAKE"].ToString();
				txtMODEL.Text					=		dtOldData.Rows[0]["MODEL"].ToString();				
				txtSERIAL.Text					=		dtOldData.Rows[0]["SERIAL"].ToString();				
				txtREMARKS.Text					=		dtOldData.Rows[0]["REMARKS"].ToString();	
				txtHORSE_POWER.Text				=		dtOldData.Rows[0]["HORSE_POWER"].ToString();	
				
				if(dtOldData.Rows[0]["VEHICLE_TYPE"]!=null && dtOldData.Rows[0]["VEHICLE_TYPE"].ToString()!="" && dtOldData.Rows[0]["VEHICLE_TYPE"].ToString()!="0")
					cmbVEHICLE_TYPE.SelectedValue		=		dtOldData.Rows[0]["VEHICLE_TYPE"].ToString();				
				
			}

		}

		private void GetQueryStringValues()
		{
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			if(Request.QueryString["REC_VEH_ID"]!=null && Request.QueryString["REC_VEH_ID"].ToString()!="")				
				hidREC_VEH_ID.Value = Request.QueryString["REC_VEH_ID"].ToString();
			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();		
		}

		private void GetOldDataXML(bool flag)
		{
			DataTable dtOldData = new DataTable();
			if(hidREC_VEH_ID.Value!="" && hidREC_VEH_ID.Value!="0")
			{
				ClsHomeRecrVehicles objHomeRecrVehicles = new ClsHomeRecrVehicles();
				dtOldData	=	objHomeRecrVehicles.GetClaimRecrVehicleByID(int.Parse(hidCLAIM_ID.Value),int.Parse(hidREC_VEH_ID.Value));
				if(dtOldData!=null && dtOldData.Rows.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
					trPolicyRecVeh.Attributes.Add("style","display:none");
					if(LOAD_OLD_DATA)
						LoadData(dtOldData);
				}
				else
					hidOldData.Value	=	"";
			}
			else
				hidOldData.Value	=	"";
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
			this.cmbPOLICY_REC_VEH.SelectedIndexChanged += new System.EventHandler(this.cmbPOLICY_REC_VEH_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		/// <summary>
		/// Load the various dropdowns in the page
		/// </summary>
		private void LoadDropdowns()
		{
			//States
			DataTable dt = Cms.CmsWeb.ClsFetcher.State;
			cmbSTATE_REGISTERED.DataSource = dt;
			cmbSTATE_REGISTERED.DataTextField = STATE_NAME;
			cmbSTATE_REGISTERED.DataValueField = STATE_ID;
			cmbSTATE_REGISTERED.DataBind();
			cmbSTATE_REGISTERED.Items.Insert(0,new ListItem("",""));			
			
			cmbVEHICLE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CBDCD2");
			cmbVEHICLE_TYPE.DataTextField = "LookupDesc";
			cmbVEHICLE_TYPE.DataValueField = "LookupID";
			cmbVEHICLE_TYPE.DataBind();
			cmbVEHICLE_TYPE.Items.Insert(0,new ListItem("",""));
			cmbVEHICLE_TYPE.SelectedIndex=0;

			if(hidREC_VEH_ID.Value=="" || hidREC_VEH_ID.Value=="0")
			{
				ClsHomeRecrVehicles objHomeRecrVehicles = new ClsHomeRecrVehicles();
				DataTable dtPolRecVeh = objHomeRecrVehicles.GetPolRecVehList(int.Parse(hidCLAIM_ID.Value));
				if(dtPolRecVeh!=null && dtPolRecVeh.Rows.Count>0)
				{
					cmbPOLICY_REC_VEH.DataSource = dtPolRecVeh;
					cmbPOLICY_REC_VEH.DataTextField = "REC_VEH";
					cmbPOLICY_REC_VEH.DataValueField = "POL_REC_VEH_ID";
					cmbPOLICY_REC_VEH.DataBind();				
					cmbPOLICY_REC_VEH.Items.Insert(0,"");
				}
				int NextCompanyNum = objHomeRecrVehicles.GetClaimNextCompanyIDNumber(int.Parse(hidCLAIM_ID.Value));
				if(NextCompanyNum!=-1 && NextCompanyNum<10000)
					txtCOMPANY_ID_NUMBER.Text = NextCompanyNum.ToString();
				else if (NextCompanyNum>=10000)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("534");				
					lblMessage.Visible = true;
				}
			}

			//ClsHomeRecrVehicles.GetClaimNextCompanyIDNumber

			
		}

		/// <summary>
		/// Sets the text of the labels
		/// </summary>
		private void SetCaptions()
		{
			ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddRecrVehicle",System.Reflection.Assembly.GetExecutingAssembly());

			capPOLICY_REC_VEH.Text	 =	objResourceMgr.GetString("cmbPOLICY_REC_VEH");
			capCOMPANY_ID_NUMBER.Text =	objResourceMgr.GetString("txtCOMPANY_ID_NUMBER");
			capYEAR.Text =	objResourceMgr.GetString("txtYEAR");
			capMAKE.Text =	objResourceMgr.GetString("txtMAKE");
			capMODEL.Text =	objResourceMgr.GetString("txtMODEL");
			capSERIAL.Text = objResourceMgr.GetString("txtSERIAL");
			capSTATE_REGISTERED.Text =	objResourceMgr.GetString("txtSTATE_REGISTERED");
			capVEHICLE_TYPE.Text =	objResourceMgr.GetString("cmbVEHICLE_TYPE");			
			capHORSE_POWER.Text =	objResourceMgr.GetString("txtHORSE_POWER");			
			capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
		}

		/// <summary>
		/// Sets the validator error messages
		/// </summary>
		private void SetValidators()
		{
			rfvCOMPANY_ID_NUMBER.ErrorMessage		=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("534");
			rfvYEAR.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("166");
			rfvMAKE.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("168");
			rfvMODEL.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("169");
			rngYEAR.MaximumValue					=		 (DateTime.Now.Year+1).ToString();
			rngYEAR.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("673");			
			rfvSERIAL.ErrorMessage					=			Cms.CmsWeb.ClsMessages.FetchGeneralMessage("535");
			rfvSTATE_REGISTERED.ErrorMessage		=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("536");
			rfvHORSE_POWER.ErrorMessage				=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("191");
			revHORSE_POWER.ValidationExpression     =aRegExpInteger;
			revHORSE_POWER.ErrorMessage             =	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("537");
			rfvVEHICLE_TYPE.ErrorMessage             =	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("205");
			csvREMARKS.ErrorMessage             =	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("441");
			rngCOMPANY_ID_NUMBER.ErrorMessage             =	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
		}
		
		/// <summary>
		/// Populates form fields and Stores the XML for the record to be updated
		/// </summary>
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function				
				ClsHomeRecrVehicles objHomeRecrVehicles = new ClsHomeRecrVehicles();				

				//Retreiving the form values into model class object
				ClsRecrVehiclesInfo objRecrVehiclesInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objRecrVehiclesInfo.CREATED_BY = int.Parse(GetUserId());
					objRecrVehiclesInfo.CREATED_DATETIME = DateTime.Now;
					objRecrVehiclesInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objHomeRecrVehicles.AddClaimRecVeh(objRecrVehiclesInfo);

					if(intRetVal>0)
					{
						hidREC_VEH_ID.Value = objRecrVehiclesInfo.REC_VEH_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(!LOAD_OLD_DATA);
					}
					else if(intRetVal == -1) //Duplicate RV #
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"524");
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
					ClsRecrVehiclesInfo objOldRecrVehiclesInfo = new ClsRecrVehiclesInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldRecrVehiclesInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objRecrVehiclesInfo.MODIFIED_BY = int.Parse(GetUserId());
					objRecrVehiclesInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objHomeRecrVehicles.UpdateClaimRecVeh(objOldRecrVehiclesInfo,objRecrVehiclesInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML(!LOAD_OLD_DATA);
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"524");
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
				
			}
		}

		
		private void cmbPOLICY_REC_VEH_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtRecVeh = new DataTable();
			try
			{
				if(cmbPOLICY_REC_VEH.SelectedItem!=null && cmbPOLICY_REC_VEH.SelectedItem.Value!="")
				{
					ClsHomeRecrVehicles objHomeRecrVehicles = new ClsHomeRecrVehicles();						
					dtRecVeh = objHomeRecrVehicles.GetClaimPolRecrVehicleByID(int.Parse(hidCLAIM_ID.Value),int.Parse(cmbPOLICY_REC_VEH.SelectedItem.Value));
					if(dtRecVeh!=null && dtRecVeh.Rows.Count>0)
					{
						DataRow drTemp = dtRecVeh.Rows[0];
//						if(drTemp["COMPANY_ID_NUMBER"]!=null && drTemp["COMPANY_ID_NUMBER"].ToString()!="")
//						{
//							txtCOMPANY_ID_NUMBER.Text = drTemp["COMPANY_ID_NUMBER"].ToString();
//							txtCOMPANY_ID_NUMBER.Enabled = false;
//						}
						if(drTemp["YEAR"]!=null && drTemp["YEAR"].ToString()!="")
						{
							txtYEAR.Text = drTemp["YEAR"].ToString();
							txtYEAR.Enabled = false;
						}
						if(drTemp["MAKE"]!=null && drTemp["MAKE"].ToString()!="")
						{
							txtMAKE.Text = drTemp["MAKE"].ToString();
							txtMAKE.Enabled = false;
						}
						if(drTemp["MODEL"]!=null && drTemp["MODEL"].ToString()!="")
						{
							txtMODEL.Text = drTemp["MODEL"].ToString();
							txtMODEL.Enabled = false;
						}
						if(drTemp["STATE_REGISTERED"]!=null && drTemp["STATE_REGISTERED"].ToString()!="" && drTemp["STATE_REGISTERED"].ToString()!="0")
						{
							cmbSTATE_REGISTERED.SelectedValue = drTemp["STATE_REGISTERED"].ToString();
							cmbSTATE_REGISTERED.Enabled = false;
						}
						if(drTemp["SERIAL"]!=null && drTemp["SERIAL"].ToString()!="")
						{
							txtSERIAL.Text = drTemp["SERIAL"].ToString();
							txtSERIAL.Enabled = false;
						}
						if(drTemp["HORSE_POWER"]!=null && drTemp["HORSE_POWER"].ToString()!="")
						{
							txtHORSE_POWER.Text = drTemp["HORSE_POWER"].ToString();
							txtHORSE_POWER.Enabled = false;
						}
						if(drTemp["REMARKS"]!=null && drTemp["REMARKS"].ToString()!="")
						{
							txtREMARKS.Text = drTemp["REMARKS"].ToString();
							txtREMARKS.Enabled = false;
						}
						if(drTemp["VEHICLE_TYPE"]!=null && drTemp["VEHICLE_TYPE"].ToString()!="" && drTemp["VEHICLE_TYPE"].ToString()!="0")
						{
							cmbVEHICLE_TYPE.SelectedValue = drTemp["VEHICLE_TYPE"].ToString();
							cmbVEHICLE_TYPE.Enabled = false;
						}						
					}					
				}
				else
				{
					txtCOMPANY_ID_NUMBER.Enabled = true;
					txtYEAR.Enabled = true;
					txtMAKE.Enabled = true;
					txtMODEL.Enabled = true;
					cmbSTATE_REGISTERED.Enabled = true;
					txtSERIAL.Enabled = true;					
					txtHORSE_POWER.Enabled = true;					
					cmbVEHICLE_TYPE.Enabled = true;
					txtREMARKS.Enabled = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(dtRecVeh!=null)
					dtRecVeh.Dispose();				 
			}
		}
	}
}
