/******************************************************************************************
<Author					: - Amar
<Start Date				: -	5/1/2006 5:31:07 PM
<End Date				: -	5/3/2006 3:53:07 PM
<Description			: - This web page enables user to add/edit Insured vehicle details	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using Cms.ExceptionPublisher.ExceptionManagement;

namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddInsuredVehicle : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
	
		protected System.Web.UI.HtmlControls.HtmlTableRow trPURPOSE_OF_USE;
		//Added for Itrack Issue 5833 on 20 May 2009
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.TextBox txtVIN;
		protected System.Web.UI.WebControls.TextBox txtBODY_TYPE;
		//		protected System.Web.UI.WebControls.TextBox txtPLATE_NUMBER;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbNON_OWNED_VEHICLE;
		protected System.Web.UI.WebControls.DropDownList cmbVehicle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicy_Vehicle_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSURED_VEHICLE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE;//Added for Itrack Issue 5833 on 20 May 2009
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPURPOSE_OF_USE;//Added for Itrack Issue 5833 on 20 May 2009
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;//Added for Itrack Issue 5833 on 20 May 2009
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNON_OWNED_VEHICLE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVehicle;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVIN;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL;
		protected System.Web.UI.WebControls.RangeValidator rngVEHICLE_YEAR;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capNON_OWNED_VEHICLE;
		protected System.Web.UI.WebControls.Label capVEHICLE_YEAR;
		protected System.Web.UI.WebControls.Label capVehicle;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.Label capVIN;
		protected System.Web.UI.WebControls.Label capBODY_TYPE;
		//		protected System.Web.UI.WebControls.Label capPLATE_NUMBER;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.Label lblDelete;//Added for Itrack Issue 5833 on 20 May 2009
	
		#endregion

		#region Local form variables
		string oldXML;
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		ClsInsuredVehicle  oInsuredVehicle ;
		protected System.Web.UI.WebControls.Label capWHERE_VEHICLE_SEEN;
		protected System.Web.UI.WebControls.TextBox txtWHERE_VEHICLE_SEEN;
		protected System.Web.UI.WebControls.Label capWHEN_VEHICLE_SEEN;
		protected System.Web.UI.WebControls.TextBox txtWHEN_VEHICLE_SEEN;
		protected System.Web.UI.WebControls.Label capPURPOSE_OF_USE;
		protected System.Web.UI.WebControls.DropDownList cmbPURPOSE_OF_USE;
		protected System.Web.UI.WebControls.Label capUSED_WITH_PERMISSION;
		protected System.Web.UI.WebControls.DropDownList cmbUSED_WITH_PERMISSION;
		protected System.Web.UI.WebControls.Label capDESCRIBE_DAMAGE;
		protected System.Web.UI.WebControls.TextBox txtDESCRIBE_DAMAGE;
		protected System.Web.UI.WebControls.Label capESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.RangeValidator rngESTIMATE_AMOUNT;
		protected System.Web.UI.WebControls.Label capOTHER_VEHICLE_INSURANCE;
		protected System.Web.UI.WebControls.TextBox txtOTHER_VEHICLE_INSURANCE;		
		#endregion
		#region Set Validators ErrorMessages
		private void SetErrorMessages()
		{
			rfvNON_OWNED_VEHICLE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvVehicle.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvVIN.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvVEHICLE_YEAR.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvMAKE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvMODEL.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rngESTIMATE_AMOUNT.ErrorMessage			=		Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");			
			string sMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			rngVEHICLE_YEAR.MaximumValue		= (DateTime.Now.Year+1).ToString();
			rngVEHICLE_YEAR.MinimumValue		= aAppMinYear  ;
			rngVEHICLE_YEAR.ErrorMessage		=  string.Format(sMessage, new object[]{aAppMinYear,(DateTime.Now.Year+1)});
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			Ajax.Utility.RegisterTypeForAjax(typeof(AddInsuredVehicle)); 
			 //Set Screen ID
			base.ScreenId="306_2_0";
			lblMessage.Visible = false;
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Execute;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Execute;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Execute;
			btnSave.PermissionString=	gstrSecurityXML;
			//Added for Itrack Issue 5833 on 20 May 2009
			btnDelete.CmsButtonClass	=	CmsButtonType.Execute;
			btnDelete.PermissionString=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddInsuredVehicle" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				SetErrorMessages();
				SetCaptions();
				//Done for Itrack Issue 6327 on 16 Sept 09
				//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				btnReset.Attributes.Add("onclick","javascript:ResetTheForm();");
				GetQueryStringValues();
				LoadDropDowns();
				//				txtPLATE_NUMBER.Attributes.Add("onKeyPress","javascript: return (this.value = this.value.toUpperCase());");
				//				txtPLATE_NUMBER.Attributes.Add("onBlur","javascript: return (this.value = this.value.toUpperCase());");
				txtESTIMATE_AMOUNT.Attributes.Add("onBlur","javascript: this.value = formatCurrencyWithCents(this.value);");

				if(hidINSURED_VEHICLE_ID.Value.ToUpper()!= "NEW" && hidINSURED_VEHICLE_ID.Value!="0")
					hidOldData.Value = ClsInsuredVehicle.GetXmlForPageControls(hidCLAIM_ID.Value,hidINSURED_VEHICLE_ID.Value);	
				//Done for Itrack Issue 5833 on 18 June 2009
				if(hidOldData.Value!="")
				{
					btnActivateDeactivate.Enabled = true;
					//btnReset.Enabled = false;//Done for Itrack Issue 6327 on 16 Sept 09
				}
				else
				{
					btnActivateDeactivate.Enabled = false;
					//btnReset.Enabled = true;//Done for Itrack Issue 6327 on 16 Sept 09
				}

			}
			//Added for Itrack Issue 5833 on 20 May 2009
			ClsInsuredVehicle obj = new ClsInsuredVehicle();
			if(hidINSURED_VEHICLE_ID.Value.ToUpper()!= "NEW" && hidINSURED_VEHICLE_ID.Value.ToUpper()!= "0")//Done for Itrack Issue 5833 on 21 July 2009
			{
				int intReserve = obj.CheckClaimActivityReserve(int.Parse(GetClaimID()),int.Parse(hidINSURED_VEHICLE_ID.Value),"AUTO_MOTOR");//Done for Itrack Issue 5833 on 21 July 2009
				if(intReserve == 1)
				{
					btnDelete.Visible=false;
				}
				else
				{
					btnDelete.Visible= true;
				}
			}
			else
			{
				btnDelete.Visible= true;
				//btnDelete.Enabled = false;//Done for Itrack Issue 5833 on 21 July 2009
			}

		}//end pageload
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			ClsInsuredVehicle objInsuredVehicle = new ClsInsuredVehicle();
			DataTable dtVehicles = objInsuredVehicle.GetVehiclesForClaim(int.Parse(hidCLAIM_ID.Value));
			if (dtVehicles.Rows.Count > 0)
			{
				cmbVehicle.DataSource = dtVehicles;
				cmbVehicle.DataTextField = "VIN_NUMBER";
				cmbVehicle.DataValueField = "VEHICLE_ID";
				cmbVehicle.DataBind();
				cmbVehicle.Items.Insert(0,"");
			}

			cmbUSED_WITH_PERMISSION.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("YESNO");
			cmbUSED_WITH_PERMISSION.DataTextField="LookupDesc";
			cmbUSED_WITH_PERMISSION.DataValueField="LookupID";
			cmbUSED_WITH_PERMISSION.DataBind();

			if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString())
			{
				cmbPURPOSE_OF_USE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("VHUCP");
				cmbPURPOSE_OF_USE.DataTextField="LookupDesc";
				cmbPURPOSE_OF_USE.DataValueField="LookupID";
				cmbPURPOSE_OF_USE.DataBind();				
			}
			else
				trPURPOSE_OF_USE.Attributes.Add("style","display:none");
			
			//Fetch the state values
			DataTable dtState = Cms.CmsWeb.ClsFetcher.State;
			cmbSTATE.DataSource		= dtState;
			cmbSTATE.DataTextField	= "STATE_NAME";
			cmbSTATE.DataValueField	= "STATE_ID";
			cmbSTATE.DataBind();
			cmbSTATE.Items.Insert(0,new ListItem("",""));
			cmbSTATE.SelectedIndex=0;
		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsInsuredVehicleInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsInsuredVehicleInfo objInsuredVehicleInfo;
			objInsuredVehicleInfo = new ClsInsuredVehicleInfo();

			objInsuredVehicleInfo.NON_OWNED_VEHICLE	=	cmbNON_OWNED_VEHICLE.SelectedValue;
			objInsuredVehicleInfo.VEHICLE_YEAR		=	txtVEHICLE_YEAR.Text;
			objInsuredVehicleInfo.MAKE				=	txtMAKE.Text;
			objInsuredVehicleInfo.MODEL				=	txtMODEL.Text;
			objInsuredVehicleInfo.VIN				=	txtVIN.Text;
			objInsuredVehicleInfo.BODY_TYPE			=	txtBODY_TYPE.Text;
			//			objInsuredVehicleInfo.PLATE_NUMBER		=	txtPLATE_NUMBER.Text;
			objInsuredVehicleInfo.CLAIM_ID			=   int.Parse(hidCLAIM_ID.Value);
			//Added for Itrack Issue 5833 on 20 May 2009
			if(hidSTATE.Value =="0")
			{
				if(cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedItem.Value!="")
					objInsuredVehicleInfo.STATE				=	int.Parse(cmbSTATE.SelectedValue);
			}
			else
				objInsuredVehicleInfo.STATE		   =	int.Parse(hidSTATE.Value);
			objInsuredVehicleInfo.WHERE_VEHICLE_SEEN = txtWHERE_VEHICLE_SEEN.Text.Trim();
			objInsuredVehicleInfo.WHEN_VEHICLE_SEEN = txtWHEN_VEHICLE_SEEN.Text.Trim();
			if(hidPURPOSE_OF_USE.Value == "0")//Added for Itrack Issue 5833 on 20 May 2009
			{
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() && cmbPURPOSE_OF_USE.SelectedItem!=null && cmbPURPOSE_OF_USE.SelectedItem.Value!="")
					objInsuredVehicleInfo.PURPOSE_OF_USE = cmbPURPOSE_OF_USE.SelectedItem.Value;
			}
			else
				objInsuredVehicleInfo.PURPOSE_OF_USE = hidPURPOSE_OF_USE.Value;

			if(cmbUSED_WITH_PERMISSION.SelectedItem!=null && cmbUSED_WITH_PERMISSION.SelectedItem.Value!="")
				objInsuredVehicleInfo.USED_WITH_PERMISSION = int.Parse(cmbUSED_WITH_PERMISSION.SelectedItem.Value);
			objInsuredVehicleInfo.DESCRIBE_DAMAGE = txtDESCRIBE_DAMAGE.Text.Trim();
			if(txtESTIMATE_AMOUNT.Text.Trim()!="")
				objInsuredVehicleInfo.ESTIMATE_AMOUNT = Convert.ToDouble(txtESTIMATE_AMOUNT.Text.Trim());
			objInsuredVehicleInfo.OTHER_VEHICLE_INSURANCE = txtOTHER_VEHICLE_INSURANCE.Text.Trim();
			if(objInsuredVehicleInfo.NON_OWNED_VEHICLE.ToUpper().Equals("N") && hidPolicy_Vehicle_ID.Value!="")
			{
				objInsuredVehicleInfo.POLICY_VEHICLE_ID = int.Parse(hidPolicy_Vehicle_ID.Value);
			}

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCLAIM_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objInsuredVehicleInfo;
		}
		#endregion

		private void GetQueryStringValues()
		{
			if(Request.QueryString["INSURED_VEHICLE_ID"]!=null && Request.QueryString["INSURED_VEHICLE_ID"].ToString()!="")
			{
				hidINSURED_VEHICLE_ID.Value = Request.QueryString["INSURED_VEHICLE_ID"].ToString();
			}
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
			{
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();				 
			}			
			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
			{
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();				 
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
			this.cmbVehicle.SelectedIndexChanged += new System.EventHandler(this.cmbVehicle_OnChange);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);//Added for Itrack Issue 5833 on 20 May 2009
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);//Done for Itrack Issue 5833 on 18 June 2009
		}
		#endregion

		private void cmbVehicle_OnChange(object sender, System.EventArgs e)
		{
			int VehicleID = 0;

			if (cmbVehicle.SelectedIndex > 0)
				VehicleID = int.Parse(cmbVehicle.SelectedValue);

			ClsInsuredVehicle objInsuredVehicle = new ClsInsuredVehicle();
			DataTable dtVehicle = objInsuredVehicle.GetVehiclesForClaim(int.Parse(hidCLAIM_ID.Value),VehicleID);			

			if (dtVehicle.Rows.Count > 0)
			{
				DataRow drTemp = dtVehicle.Rows[0];
				hidFormSaved.Value = "3";
				if(drTemp["VIN"]!=null && drTemp["VIN"].ToString()!="")
				{
					txtVIN.Text = drTemp["VIN"].ToString();
					txtVIN.Enabled = false;
				}
				if(drTemp["VEHICLE_YEAR"]!=null && drTemp["VEHICLE_YEAR"].ToString()!="")
				{
					txtVEHICLE_YEAR.Text = drTemp["VEHICLE_YEAR"].ToString();
					txtVEHICLE_YEAR.Enabled = false;
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
				if(drTemp["VEHICLE_ID"]!=null && drTemp["VEHICLE_ID"].ToString()!="")
				{
					hidPolicy_Vehicle_ID.Value = drTemp["VEHICLE_ID"].ToString();
				}	
				if(hidLOB_ID.Value==((int)enumLOB.AUTOP).ToString() &&  drTemp["APP_USE_VEHICLE_ID"]!=null && drTemp["APP_USE_VEHICLE_ID"].ToString()!="")
				{
					cmbPURPOSE_OF_USE.SelectedValue = drTemp["APP_USE_VEHICLE_ID"].ToString();
					cmbPURPOSE_OF_USE.Enabled = false;
					//cmbPURPOSE_OF_USE.Attributes.Add("disabled","true");
				}	
				if(drTemp["BODY_TYPE"]!=null && drTemp["BODY_TYPE"].ToString()!="")
				{
					txtBODY_TYPE.Text = drTemp["BODY_TYPE"].ToString();
					txtBODY_TYPE.Enabled = false;
				}	
				if(drTemp["REG_STATE"]!=null && drTemp["REG_STATE"].ToString()!="")
				{
					cmbSTATE.SelectedValue = drTemp["REG_STATE"].ToString();
					cmbSTATE.Enabled = false;
				}
				//Added for Itrack Issue 5833 on 20 May 2009
				if(hidOldData.Value == "" || hidOldData.Value== null)
				{
					btnDelete.Enabled = false;
				}

			}
			else
			{
				txtVIN.Enabled = true;
				txtVEHICLE_YEAR.Enabled = true;
				txtMAKE.Enabled = true;
				txtMODEL.Enabled = true;
				cmbPURPOSE_OF_USE.Enabled = true;
				//cmbPURPOSE_OF_USE.Attributes.Add("disabled","false");
			}
		}

		#region "Web Event Handlers"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		[Ajax.AjaxMethod()]
		public string AjaxGetDetailsFromVIN(string VIN)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.GetDetailsFromVIN(VIN);
			return result;
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				oInsuredVehicle = new ClsInsuredVehicle();

				//Retreiving the form values into model class object
				ClsInsuredVehicleInfo objInsuredVehicleInfo = GetFormValue();
				
				if(hidINSURED_VEHICLE_ID.Value.ToString().ToUpper().Equals("NEW")) //save case
				{
					objInsuredVehicleInfo.CREATED_BY = int.Parse(GetUserId());
					objInsuredVehicleInfo.CREATED_DATETIME = DateTime.Now;
					objInsuredVehicleInfo.IS_ACTIVE="Y";

					//Calling the add method of business layer class
					intRetVal = oInsuredVehicle.Add(objInsuredVehicleInfo);

					if(intRetVal>0)
					{
						hidINSURED_VEHICLE_ID.Value	= objInsuredVehicleInfo.INSURED_VEHICLE_ID.ToString();
						lblMessage.Text				= ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			= "1";						
						hidOldData.Value = ClsInsuredVehicle.GetXmlForPageControls(hidCLAIM_ID.Value, objInsuredVehicleInfo.INSURED_VEHICLE_ID.ToString());
						hidIS_ACTIVE.Value = "Y";
						btnDelete.Enabled = true;//Added for Itrack Issue 5833 on 20 May 2009
						//btnReset.Enabled = false;//Done for Itrack Issue 5833 on 18 June 2009
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				= ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			= "2";
					}
					else
					{
						lblMessage.Text				= ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			= "2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsInsuredVehicleInfo objOldInsuredVehicleInfo;
					objOldInsuredVehicleInfo = new ClsInsuredVehicleInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldInsuredVehicleInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objInsuredVehicleInfo.INSURED_VEHICLE_ID = int.Parse(hidINSURED_VEHICLE_ID.Value);
					objInsuredVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
					objInsuredVehicleInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					intRetVal	= oInsuredVehicle.Update(objOldInsuredVehicleInfo,objInsuredVehicleInfo);
					if(intRetVal > 0)			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		 = ClsInsuredVehicle.GetXmlForPageControls(hidCLAIM_ID.Value, hidINSURED_VEHICLE_ID.Value);
						//btnReset.Enabled = false;//Done for Itrack Issue 5833 on 18 June 2009
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
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
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(oInsuredVehicle!= null)
					oInsuredVehicle.Dispose();
			}
		}
		//Added for Itrack Issue 5833 on 20 May 2009
		private void btnDelete_Click(object sender, System.EventArgs e)
			{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				oInsuredVehicle = new ClsInsuredVehicle();

				intRetVal = oInsuredVehicle.DeleteInsuredVehicle(int.Parse(GetClaimID()),int.Parse(hidINSURED_VEHICLE_ID.Value),"AUTO_MOTOR",int.Parse(GetUserId()));//Done for Itrack Issue 6932 on 10 Feb 2010
				//Retreiving the form values into model class object
				if(intRetVal>0)
				{
					hidFormSaved.Value		=	"1";
					lblDelete.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
					trBody.Attributes.Add("style","display:none");
				}
				else if(intRetVal == 0)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				}
				else if(intRetVal < 0)//Done for Itrack Issue 6053 on 9 Sept 2009
				{
					lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"12");
					lblMessage.Visible = true;
				}
				lblDelete.Visible = true;
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
				if(oInsuredVehicle!= null)
					oInsuredVehicle.Dispose();
			}
			}
		//Done for Itrack Issue 5833 on 18 June 2009
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			try
			{
				oInsuredVehicle = new ClsInsuredVehicle();
				if(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE",hidOldData.Value) == "Y")
				{
					intRetVal = oInsuredVehicle.ActivateDeactivateInsuredVehicle(int.Parse(hidCLAIM_ID.Value),int.Parse(hidINSURED_VEHICLE_ID.Value),"N","AUTO_MOTOR",int.Parse(GetUserId()));//Done for Itrack Issue 6932 on 10 Feb 2010
					if(intRetVal >0)
					{
						lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"41");
						lblMessage.Visible=true;
					}
					else if(intRetVal <1)//Done for Itrack Issue 6053 on 9 Sept 2009
					{
						lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"13");
						lblMessage.Visible=true;
					}
				}
				else
				{
					intRetVal= oInsuredVehicle.ActivateDeactivateInsuredVehicle(int.Parse(GetClaimID()),int.Parse(hidINSURED_VEHICLE_ID.Value),"Y","AUTO_MOTOR",int.Parse(GetUserId()));//Done for Itrack Issue 6932 on 10 Feb 2010
					lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"40");
					lblMessage.Visible=true;
				}
				hidOldData.Value = ClsInsuredVehicle.GetXmlForPageControls(hidCLAIM_ID.Value,hidINSURED_VEHICLE_ID.Value);
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidINSURED_VEHICLE_ID.Value + ");</script>");
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
				if(oInsuredVehicle!= null)
					oInsuredVehicle.Dispose();
			}
		}

		#endregion

		private void SetCaptions()
		{
			capNON_OWNED_VEHICLE.Text			=		objResourceMgr.GetString("cmbNON_OWNED_VEHICLE");
			capVEHICLE_YEAR.Text				=		objResourceMgr.GetString("txtVEHICLE_YEAR");
			capMAKE.Text						=		objResourceMgr.GetString("txtMAKE");
			capMODEL.Text						=		objResourceMgr.GetString("txtMODEL");
			capVIN.Text							=		objResourceMgr.GetString("txtVIN");
			capBODY_TYPE.Text					=		objResourceMgr.GetString("txtBODY_TYPE");
			//			capPLATE_NUMBER.Text				=		objResourceMgr.GetString("txtPLATE_NUMBER");
			capSTATE.Text						=		objResourceMgr.GetString("cmbSTATE");
			capVehicle.Text						=		objResourceMgr.GetString("cmbVehicle");
			capWHERE_VEHICLE_SEEN.Text			=		objResourceMgr.GetString("txtWHERE_VEHICLE_SEEN");			
			capWHEN_VEHICLE_SEEN.Text			=		objResourceMgr.GetString("txtWHEN_VEHICLE_SEEN");	
			capPURPOSE_OF_USE.Text					=		objResourceMgr.GetString("cmbPURPOSE_OF_USE");			
			capUSED_WITH_PERMISSION.Text			=		objResourceMgr.GetString("cmbUSED_WITH_PERMISSION");			
			capDESCRIBE_DAMAGE.Text					=		objResourceMgr.GetString("txtDESCRIBE_DAMAGE");			
			capESTIMATE_AMOUNT.Text					=		objResourceMgr.GetString("txtESTIMATE_AMOUNT");			
			capOTHER_VEHICLE_INSURANCE.Text			=		objResourceMgr.GetString("txtOTHER_VEHICLE_INSURANCE");			
			
		}
		
	}
}
