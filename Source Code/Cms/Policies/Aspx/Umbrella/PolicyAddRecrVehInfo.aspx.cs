/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	03/27/2006
<End Date				: -	
<Description			: - Add/Edit for Policy Umbrella Recreational Vehicles
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
//using Cms.Application;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
//using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.Umbrella;
using System.Resources;
using Cms.CmsWeb;

namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for AddUmbrellaRecrVehIndex.
	/// </summary>
	public class PolicyAddRecrVehInfo : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label capIS_BOAT_EXCLUDED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_BOAT_EXCLUDED;
		protected System.Web.UI.WebControls.Label capOTHER_POLICY;
		protected System.Web.UI.WebControls.DropDownList cmbOTHER_POLICY;
		protected System.Web.UI.WebControls.Label capC44;
		protected System.Web.UI.WebControls.DropDownList cmbC44;

		protected System.Web.UI.WebControls.Label lblInfo;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capVEHICLE_MODIFIED_DETAILS;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_MODIFIED_DETAILS;
		protected System.Web.UI.WebControls.Label capCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_MODIFIED_DETAILS;
		protected System.Web.UI.WebControls.RangeValidator rngCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.Label capYEAR;
		protected System.Web.UI.WebControls.TextBox txtYEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL;
		protected System.Web.UI.WebControls.Label capSERIAL;
		protected System.Web.UI.WebControls.TextBox txtSERIAL;
		protected System.Web.UI.WebControls.Label capSTATE_REGISTERED;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_REGISTERED;
		protected System.Web.UI.WebControls.Label capVEHICLE_TYPE;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_TYPE_NAME;
		protected System.Web.UI.WebControls.Label capMANUFACTURER_DESC;
		protected System.Web.UI.WebControls.TextBox txtMANUFACTURER_DESC;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMANUFACTURER_DESC;
		protected System.Web.UI.WebControls.Label capHORSE_POWER;
		protected System.Web.UI.WebControls.TextBox txtHORSE_POWER;
		//protected System.Web.UI.WebControls.Label capDISPLACEMENT;
		//protected System.Web.UI.WebControls.TextBox txtDISPLACEMENT;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		//protected System.Web.UI.WebControls.Label capPRIOR_LOSSES;
		//protected System.Web.UI.WebControls.DropDownList cmbPRIOR_LOSSES;
		//protected System.Web.UI.WebControls.Label capIS_UNIT_REG_IN_OTHER_STATE;
		//protected System.Web.UI.WebControls.DropDownList cmbIS_UNIT_REG_IN_OTHER_STATE;
		//protected System.Web.UI.WebControls.Label capRISK_DECL_BY_OTHER_COMP;
		//protected System.Web.UI.WebControls.DropDownList cmbRISK_DECL_BY_OTHER_COMP;
		//protected System.Web.UI.WebControls.Label capDESC_RISK_DECL_BY_OTHER_COMP;
		//protected System.Web.UI.WebControls.TextBox txtDESC_RISK_DECL_BY_OTHER_COMP;
		protected System.Web.UI.WebControls.Label capVEHICLE_MODIFIED;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_MODIFIED;
		protected System.Web.UI.WebControls.Label capUSED_IN_RACE_SPEED;
		protected System.Web.UI.WebControls.DropDownList cmbUSED_IN_RACE_SPEED;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMessage;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_TYPE_NAME;
		protected System.Web.UI.WebControls.CustomValidator csvREMARKS;
		//protected System.Web.UI.WebControls.CustomValidator csvDESC_RISK_DECL_BY_OTHER_COMP;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOOKUP_UNIQUE_ID;
		protected System.Web.UI.WebControls.CustomValidator csvYEAR;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEAR;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		protected System.Web.UI.WebControls.Label capREC_VEH_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbREC_VEH_TYPE;
		protected System.Web.UI.WebControls.Label capREC_VEH_TYPE_DESC;
		protected System.Web.UI.WebControls.TextBox txtREC_VEH_TYPE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREC_VEH_TYPE_DESC;

		protected System.Web.UI.WebControls.Label capUSED_IN_RACE_SPEED_CONTEST;
		protected System.Web.UI.WebControls.TextBox txtUSED_IN_RACE_SPEED_CONTEST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSED_IN_RACE_SPEED_CONTEST;
		protected System.Web.UI.WebControls.Label capVEH_LIC_ROAD;
		protected System.Web.UI.WebControls.DropDownList cmbVEH_LIC_ROAD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEH_LIC_ROAD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREC_VEH_TYPE;
		private int iOther = 11949;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			// Put user code to initialize the page here
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//Copy Button being set to hide as this feature is being implemented policy wide
			//btnCopy.Visible = false;
			base.ScreenId			= "276_0";//can be Alpha Numeric 

			btnReset.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;	
				
			btnSave.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;	
			
			btnCopy.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnCopy.PermissionString				=	gstrSecurityXML;
			
			btnDelete.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString				=	gstrSecurityXML;
			
			btnActivateDeactivate.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;	
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			if ( !Page.IsPostBack )
			{
				trMessage.Visible = false;

				hidCustomerID.Value	 =  GetCustomerID();
				hidPolicyID.Value = GetPolicyID();
				hidPolicyVersionID.Value = GetPolicyVersionID();

				if (	hidCustomerID.Value == "" ||
					hidPolicyID.Value == "" || 
					hidPolicyVersionID.Value == "" 
					)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","118");
					return;
				}

				SetCaptions();

				SetValidators();
				
				LoadDropdowns();
														   
				this.SetFocus("txtCOMPANY_ID_NUMBER");

				#region set workflowcontrol
				SetWorkFlow();
				#endregion
				
				//Reset button
				btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
				cmbVEH_LIC_ROAD.Attributes.Add("onChange","javascript:cmbVEH_LIC_ROAD_Change();");	
				btnSave.Attributes.Add("onClick","javascript:return cmbVEH_LIC_ROAD_Change(true);");								
				cmbVEHICLE_MODIFIED.Attributes.Add("onChange","javascript:cmbVEHICLE_MODIFIED_Change();");
				cmbREC_VEH_TYPE.Attributes.Add("onChange","javascript:cmbREC_VEH_TYPE_Change();");
				cmbUSED_IN_RACE_SPEED.Attributes.Add("onChange","javascript:cmbUSED_IN_RACE_SPEED_Change();");
				this.txtREMARKS.Attributes.Add("OnKeyPress","javascript:MaxLength(this,100);");
				this.txtMANUFACTURER_DESC.Attributes.Add("OnKeyPress","javascript:MaxLength(this,100);");
				//this.txtDESC_RISK_DECL_BY_OTHER_COMP.Attributes.Add("OnKeyPress","javascript:MaxLength(this,100);");
				
				string url = ClsCommon.GetLookupURL();
				//string strCopyWindow = rootPath + 

				imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','LOOKUP_UNIQUE_ID','LOOKUP_VALUE_DESC','hidLOOKUP_UNIQUE_ID','txtVEHICLE_TYPE_NAME','LookupTable','Vehicle Types',\"@LOOKUP_NAME='CBDCD'\")");

				if ( Request.QueryString["REC_VEH_ID"] != null )
				{
					this.hidREC_VEH_ID.Value = Request.QueryString["REC_VEH_ID"].ToString();
					LoadData();

				}
				else
				{
					//Add new
					this.btnActivateDeactivate.Visible = false;
					this.btnCopy.Visible = false;
					this.btnDelete.Visible = false;

					//Set the next CompanyID number
					int intCompanyNumber = ClsUmbrellaRecrVeh.GetNextPolUmbrellaCompanyIDNumber
						(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(this.hidPolicyID.Value),
						Convert.ToInt32(this.hidPolicyVersionID.Value)
						);
					
					if ( intCompanyNumber == -1 )
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"7");
						return;
					}
					else
					{
						this.txtCOMPANY_ID_NUMBER.Text = intCompanyNumber.ToString();
					}
				}
				
			}
		}
		
		/// <summary>
		/// Load the various dropdowns in the page
		/// </summary>
		private void LoadDropdowns()
		{
			//States
			DataTable dt = Cms.CmsWeb.ClsFetcher.State;
			this.cmbSTATE_REGISTERED.DataSource = dt;
			this.cmbSTATE_REGISTERED.DataTextField = STATE_NAME;
			this.cmbSTATE_REGISTERED.DataValueField = STATE_ID;
			this.cmbSTATE_REGISTERED.DataBind();			
			this.cmbSTATE_REGISTERED.Items.Insert(0,new ListItem("",""));

			cmbVEHICLE_MODIFIED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbVEHICLE_MODIFIED.DataTextField="LookupDesc"; 
			cmbVEHICLE_MODIFIED.DataValueField="LookupCode";
			cmbVEHICLE_MODIFIED.DataBind();
			cmbVEHICLE_MODIFIED.Items.Insert(0,"");

			cmbIS_BOAT_EXCLUDED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_BOAT_EXCLUDED.DataTextField="LookupDesc"; 
			cmbIS_BOAT_EXCLUDED.DataValueField="LookupID";
			cmbIS_BOAT_EXCLUDED.DataBind();
			cmbIS_BOAT_EXCLUDED.Items.Insert(0,"");

			
			cmbVEH_LIC_ROAD.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbVEH_LIC_ROAD.DataTextField="LookupDesc"; 
			cmbVEH_LIC_ROAD.DataValueField="LookupCode";
			cmbVEH_LIC_ROAD.DataBind();
			//cmbVEH_LIC_ROAD.Items.Insert(0,"");

			cmbUSED_IN_RACE_SPEED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbUSED_IN_RACE_SPEED.DataTextField="LookupDesc"; 
			cmbUSED_IN_RACE_SPEED.DataValueField="LookupCode";
			cmbUSED_IN_RACE_SPEED.DataBind();
			cmbUSED_IN_RACE_SPEED.Items.Insert(0,"");

			cmbREC_VEH_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RCVTYP",null,"Y");
			cmbREC_VEH_TYPE.DataTextField="LookupDesc"; 
			cmbREC_VEH_TYPE.DataValueField="LookupID";
			cmbREC_VEH_TYPE.DataBind();		
	
			cmbC44.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbC44.DataTextField="LookupDesc"; 
			cmbC44.DataValueField="LookupID";
			cmbC44.DataBind();
			cmbC44.Items.Insert(0,"");
	
			ClsUmbrellaRecrVeh objUmbrellaRecrVeh = new ClsUmbrellaRecrVeh();
			dt = objUmbrellaRecrVeh.GetPolSelectedOtherPolicies(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),ClsUmbSchRecords.CALLED_FROM_REC_VEH,"");
			if(dt!=null && dt.Rows.Count>0)
			{
				cmbOTHER_POLICY.DataSource = dt;
				cmbOTHER_POLICY.DataTextField = "POLICY_NUMBER_LOB";
				cmbOTHER_POLICY.DataValueField = "POLICY_NUMBER";
				cmbOTHER_POLICY.DataBind();
				cmbOTHER_POLICY.Items.Insert(0,new ListItem("",""));

			}


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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// Sets the text of the labels
		/// </summary>
		private void SetCaptions()
		{
			ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyAddRecrVehInfo",System.Reflection.Assembly.GetExecutingAssembly());

			capCOMPANY_ID_NUMBER.Text =	objResourceMgr.GetString("txtCOMPANY_ID_NUMBER");
			capYEAR.Text =	objResourceMgr.GetString("txtYEAR");
			capMAKE.Text =	objResourceMgr.GetString("txtMAKE");
			capMODEL.Text =	objResourceMgr.GetString("txtMODEL");
			capSERIAL.Text = objResourceMgr.GetString("txtSERIAL");
			capSTATE_REGISTERED.Text =	objResourceMgr.GetString("cmbSTATE_REGISTERED");
			capVEHICLE_TYPE.Text =	objResourceMgr.GetString("txtVEHICLE_TYPE");
			capMANUFACTURER_DESC.Text =	objResourceMgr.GetString("txtMANUFACTURER_DESC");
			capHORSE_POWER.Text =	objResourceMgr.GetString("txtHORSE_POWER");
			//capDISPLACEMENT.Text = 	objResourceMgr.GetString("txtDISPLACEMENT");
			capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
			capUSED_IN_RACE_SPEED.Text = objResourceMgr.GetString("cmbUSED_IN_RACE_SPEED");
			//capPRIOR_LOSSES.Text =	objResourceMgr.GetString("cmbPRIOR_LOSSES");
			//capIS_UNIT_REG_IN_OTHER_STATE.Text = objResourceMgr.GetString("cmbIS_UNIT_REG_IN_OTHER_STATE");
			//capRISK_DECL_BY_OTHER_COMP.Text = objResourceMgr.GetString("cmbRISK_DECL_BY_OTHER_COMP");
			//capDESC_RISK_DECL_BY_OTHER_COMP.Text =	objResourceMgr.GetString("txtDESC_RISK_DECL_BY_OTHER_COMP");
			capVEHICLE_MODIFIED.Text = objResourceMgr.GetString("cmbVEHICLE_MODIFIED");
			capVEHICLE_MODIFIED_DETAILS.Text = objResourceMgr.GetString("txtVEHICLE_MODIFIED_DETAILS");
			capVEH_LIC_ROAD.Text = objResourceMgr.GetString("cmbVEH_LIC_ROAD");
			capREC_VEH_TYPE.Text = objResourceMgr.GetString("cmbREC_VEH_TYPE");
			capREC_VEH_TYPE_DESC.Text = objResourceMgr.GetString("txtREC_VEH_TYPE_DESC");
			capUSED_IN_RACE_SPEED_CONTEST.Text = objResourceMgr.GetString("txtUSED_IN_RACE_SPEED_CONTEST");
			capC44.Text = objResourceMgr.GetString("cmbC44");
			capOTHER_POLICY.Text = objResourceMgr.GetString("cmbOTHER_POLICY");
			capIS_BOAT_EXCLUDED.Text = objResourceMgr.GetString("cmbIS_BOAT_EXCLUDED");

		}

		/// <summary>
		/// Sets the validator error messages
		/// </summary>
		private void SetValidators()
		{
			revYEAR.ValidationExpression            =  aRegExpInteger;
			//this.rfvDWELLING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			this.rfvCOMPANY_ID_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			this.rngCOMPANY_ID_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			this.rfvVEHICLE_MODIFIED_DETAILS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"12");
			
			rfvREC_VEH_TYPE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("966"); 
			
			this.rfvYEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");
			
			//this.rngYEAR.MaximumValue = DateTime.Now.Year.ToString();
			//rngYEAR.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"516");

			rngYEAR.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngYEAR.MinimumValue = aAppMinYear  ;
			rngYEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("221");




			this.rfvMAKE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");
			this.rfvVEHICLE_TYPE_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"10");
			this.rfvMODEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"9");
			//this.rfvMANUFACTURER_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
			this.revYEAR.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"516");
			this.csvYEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"517");
			csvREMARKS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"441");
			//csvDESC_RISK_DECL_BY_OTHER_COMP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"441");
			rfvREC_VEH_TYPE_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"13");
			rfvUSED_IN_RACE_SPEED_CONTEST.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"14");
			hidMessage.Value  = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"15");
			rfvVEH_LIC_ROAD.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"16");
		}

		/// <summary>
		/// Populates form fields and Stores the XML for the record to be updated
		/// </summary>
		private void LoadData()
		{
			ClsUmbrellaRecrVeh objVehicle = new ClsUmbrellaRecrVeh();
			
			DataSet dsVehicles = objVehicle.GetPolicyUmbrellaRecrVehicleByID(Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(this.hidPolicyID.Value),
				Convert.ToInt32(this.hidPolicyVersionID.Value),
				Convert.ToInt32(this.hidREC_VEH_ID.Value)
				);
			
			if ( dsVehicles.Tables[0].Rows.Count == 0 ) 
			{
				return;
			}
			
			this.btnActivateDeactivate.Visible = true;
			this.btnCopy.Visible = true;
			this.btnDelete.Visible = true;

			string url = "'" + ClsCommon.GetApplicationPath() + "/policies/aspx/homeowner/CopyRecrVehicles.aspx?REC_VEH_ID=" + this.hidREC_VEH_ID.Value + "&CalledFrom=Umbrella'";
			btnCopy.Attributes.Add("onclick","javascript: return OpenPopupWindow(" + url + ");");


			ListItem listItem;

			//this.hidOldData.Value = dsVehicles.GetXml();
			
			this.hidOldData.Value = ClsCommon.GetXMLEncoded(dsVehicles.Tables[0]);

			DataTable dtVehicle = dsVehicles.Tables[0];

			txtCOMPANY_ID_NUMBER.Text = Convert.ToString(dtVehicle.Rows[0]["COMPANY_ID_NUMBER"]);
			txtYEAR.Text = Convert.ToString(dtVehicle.Rows[0]["YEAR"]);
			txtMAKE.Text = Convert.ToString(dtVehicle.Rows[0]["MAKE"]);
			txtMODEL.Text = Convert.ToString(dtVehicle.Rows[0]["MODEL"]);
			txtSERIAL.Text = Convert.ToString(dtVehicle.Rows[0]["SERIAL"]);

			if ( dtVehicle.Rows[0]["STATE_REGISTERED"] != System.DBNull.Value )
			{
				listItem = cmbSTATE_REGISTERED.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["STATE_REGISTERED"]));
				cmbSTATE_REGISTERED.SelectedIndex= cmbSTATE_REGISTERED.Items.IndexOf(listItem);	
			}
			
			/*
			if ( dtVehicle.Rows[0]["VEHICLE_TYPE"] != System.DBNull.Value )
			{
				listItem = cmbVEHICLE_TYPE.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE"]));
				cmbVEHICLE_TYPE.SelectedIndex= cmbSTATE_REGISTERED.Items.IndexOf(listItem);	
			}*/
			
			if ( dtVehicle.Rows[0]["VEHICLE_TYPE"] != System.DBNull.Value )
			{
				this.hidLOOKUP_UNIQUE_ID.Value = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE"]);
				this.txtVEHICLE_TYPE_NAME.Text =  Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE_NAME"]) ;
			}

			txtMANUFACTURER_DESC.Text = Convert.ToString(dtVehicle.Rows[0]["MANUFACTURER_DESC"]);
			txtHORSE_POWER.Text = Convert.ToString(dtVehicle.Rows[0]["HORSE_POWER"]);
			//txtDISPLACEMENT.Text = Convert.ToString(dtVehicle.Rows[0]["DISPLACEMENT"]);
			txtREMARKS.Text = Convert.ToString(dtVehicle.Rows[0]["REMARKS"]);
			
			if ( dtVehicle.Rows[0]["USED_IN_RACE_SPEED"] != System.DBNull.Value )
			{
				listItem = cmbUSED_IN_RACE_SPEED.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["USED_IN_RACE_SPEED"]));
				cmbUSED_IN_RACE_SPEED.SelectedIndex= cmbUSED_IN_RACE_SPEED.Items.IndexOf(listItem);	
			}

			/*if ( dtVehicle.Rows[0]["PRIOR_LOSSES"] != System.DBNull.Value )
			{
				listItem = cmbPRIOR_LOSSES.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["PRIOR_LOSSES"]));
				cmbPRIOR_LOSSES.SelectedIndex= cmbPRIOR_LOSSES.Items.IndexOf(listItem);	
			}*/

			/*if ( dtVehicle.Rows[0]["IS_UNIT_REG_IN_OTHER_STATE"] != System.DBNull.Value )
			{
				listItem = cmbIS_UNIT_REG_IN_OTHER_STATE.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["IS_UNIT_REG_IN_OTHER_STATE"]));
				cmbIS_UNIT_REG_IN_OTHER_STATE.SelectedIndex= cmbIS_UNIT_REG_IN_OTHER_STATE.Items.IndexOf(listItem);	
			}*/

			/*if ( dtVehicle.Rows[0]["RISK_DECL_BY_OTHER_COMP"] != System.DBNull.Value )
			{
				listItem = cmbRISK_DECL_BY_OTHER_COMP.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["RISK_DECL_BY_OTHER_COMP"]));
				cmbRISK_DECL_BY_OTHER_COMP.SelectedIndex = cmbRISK_DECL_BY_OTHER_COMP.Items.IndexOf(listItem);	
			}*/
			
			if ( dtVehicle.Rows[0]["VEHICLE_MODIFIED"] != System.DBNull.Value )
			{
				listItem = cmbVEHICLE_MODIFIED.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["VEHICLE_MODIFIED"]));
				cmbVEHICLE_MODIFIED.SelectedIndex = cmbVEHICLE_MODIFIED.Items.IndexOf(listItem);	
			}
			if(dtVehicle.Rows[0]["VEHICLE_MODIFIED_DETAILS"]!=null && dtVehicle.Rows[0]["VEHICLE_MODIFIED_DETAILS"].ToString()!="")
				txtVEHICLE_MODIFIED_DETAILS.Text = dtVehicle.Rows[0]["VEHICLE_MODIFIED_DETAILS"].ToString();


			if(dtVehicle.Rows[0]["VEH_LIC_ROAD"]!=null && dtVehicle.Rows[0]["VEH_LIC_ROAD"].ToString()!="")
				cmbVEH_LIC_ROAD.SelectedValue = dtVehicle.Rows[0]["VEH_LIC_ROAD"].ToString();
			if(dtVehicle.Rows[0]["REC_VEH_TYPE"]!=null && dtVehicle.Rows[0]["REC_VEH_TYPE"].ToString()!="")
				cmbREC_VEH_TYPE.SelectedValue = dtVehicle.Rows[0]["REC_VEH_TYPE"].ToString();
			if(dtVehicle.Rows[0]["REC_VEH_TYPE_DESC"]!=null && dtVehicle.Rows[0]["REC_VEH_TYPE_DESC"].ToString()!="")
				txtREC_VEH_TYPE_DESC.Text= dtVehicle.Rows[0]["REC_VEH_TYPE_DESC"].ToString();
			if(dtVehicle.Rows[0]["USED_IN_RACE_SPEED_CONTEST"]!=null && dtVehicle.Rows[0]["USED_IN_RACE_SPEED_CONTEST"].ToString()!="")
				txtUSED_IN_RACE_SPEED_CONTEST.Text= dtVehicle.Rows[0]["USED_IN_RACE_SPEED_CONTEST"].ToString();
			//txtDESC_RISK_DECL_BY_OTHER_COMP.Text = Convert.ToString(dtVehicle.Rows[0]["DESC_RISK_DECL_BY_OTHER_COMP"]);


			if(dtVehicle.Rows[0]["OTHER_POLICY"]!=null && dtVehicle.Rows[0]["OTHER_POLICY"].ToString()!="")
				cmbOTHER_POLICY.SelectedValue = dtVehicle.Rows[0]["OTHER_POLICY"].ToString();

			if(dtVehicle.Rows[0]["C44"]!=null && dtVehicle.Rows[0]["C44"].ToString()!="" && dtVehicle.Rows[0]["C44"].ToString()!="0")
				cmbC44.SelectedValue = dtVehicle.Rows[0]["C44"].ToString();

			if ( dtVehicle.Rows[0]["ACTIVE"] != System.DBNull.Value )
			{
				this.hidIS_ACTIVE.Value = dtVehicle.Rows[0]["ACTIVE"].ToString();

				if ( this.hidIS_ACTIVE.Value == "Y" )
				{
					this.btnActivateDeactivate.Text = "Deactivate";
				}
				else
				{
					this.btnActivateDeactivate.Text = "Activate";
				}
			}

		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblMessage.Visible = true;

			int retVal = Save();
			
			if ( retVal == -1 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"6");
				return;
			}

			if ( retVal > 0 )
			{
				if (hidOldData.Value == "")
				{
					//Information is saved
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
				}
				else
				{
					//Information is updated
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
				}
				base.OpenEndorsementDetails();
				LoadData();
				RegisterScript();
				SetWorkFlow();
			}

		}
		
		/// <summary>
		/// Saves the record in the database
		/// </summary>
		/// <returns></returns>
		private int Save()
		{
			//BL class
			ClsUmbrellaRecrVeh objVehicle = new ClsUmbrellaRecrVeh();
			
			Cms.Model.Policy.Umbrella.ClsRecrVehiclesInfo objNewInfo = new ClsRecrVehiclesInfo();			

			objNewInfo.POLICY_ID = Convert.ToInt32(hidPolicyID.Value);
			objNewInfo.POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID.Value);
			objNewInfo.CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);
			objNewInfo.COMPANY_ID_NUMBER = Convert.ToInt32(this.txtCOMPANY_ID_NUMBER.Text.Trim());
			objNewInfo.CREATED_BY = Convert.ToInt32(GetUserId());
			objNewInfo.MODIFIED_BY = Convert.ToInt32(GetUserId());

			//objNewInfo.DESC_RISK_DECL_BY_OTHER_COMP = this.txtDESC_RISK_DECL_BY_OTHER_COMP.Text.Trim();
			//objNewInfo.DISPLACEMENT = this.txtDISPLACEMENT.Text.Trim();
			objNewInfo.HORSE_POWER = this.txtHORSE_POWER.Text.Trim();

			//objNewInfo.IS_UNIT_REG_IN_OTHER_STATE = this.cmbIS_UNIT_REG_IN_OTHER_STATE.SelectedItem.Value;
			objNewInfo.MAKE = this.txtMAKE.Text.Trim();
			objNewInfo.MANUFACTURER_DESC = this.txtMANUFACTURER_DESC.Text.Trim();
			objNewInfo.MODEL = this.txtMODEL.Text.Trim();
			
			//objNewInfo.PRIOR_LOSSES = this.cmbPRIOR_LOSSES.SelectedItem.Value;
			objNewInfo.REMARKS = this.txtREMARKS.Text.Trim();
			//objNewInfo.RISK_DECL_BY_OTHER_COMP = this.cmbRISK_DECL_BY_OTHER_COMP.SelectedItem.Value;
			
			objNewInfo.SERIAL = this.txtSERIAL.Text.Trim();
			if(cmbIS_BOAT_EXCLUDED.SelectedItem!=null && cmbIS_BOAT_EXCLUDED.SelectedItem.Value!="")
				objNewInfo.IS_BOAT_EXCLUDED = int.Parse(cmbIS_BOAT_EXCLUDED.SelectedItem.Value);
			
		    
			if (this.cmbSTATE_REGISTERED.SelectedItem.Value =="")
			{
				objNewInfo.STATE_REGISTERED = 0;
			}
			else
			{
				objNewInfo.STATE_REGISTERED = Convert.ToInt32(this.cmbSTATE_REGISTERED.SelectedItem.Value);
			}

			objNewInfo.USED_IN_RACE_SPEED = this.cmbUSED_IN_RACE_SPEED.SelectedItem.Value;

			objNewInfo.VEHICLE_MODIFIED = this.cmbVEHICLE_MODIFIED.SelectedItem.Value;

			if(objNewInfo.VEHICLE_MODIFIED!="" && objNewInfo.VEHICLE_MODIFIED.ToString()!="" && objNewInfo.VEHICLE_MODIFIED.ToString().ToUpper()=="1" && txtVEHICLE_MODIFIED_DETAILS.Text.Trim()!="")
				objNewInfo.VEHICLE_MODIFIED_DETAILS = txtVEHICLE_MODIFIED_DETAILS.Text.Trim();
			
			if ( this.hidLOOKUP_UNIQUE_ID.Value.Trim() != "" )
			{
				objNewInfo.VEHICLE_TYPE = Convert.ToInt32(this.hidLOOKUP_UNIQUE_ID.Value.Trim());
			}

			objNewInfo.YEAR = Convert.ToInt32(this.txtYEAR.Text.Trim());

			if(cmbVEH_LIC_ROAD.SelectedItem!=null && cmbVEH_LIC_ROAD.SelectedItem.Value!="")
				objNewInfo.VEH_LIC_ROAD = int.Parse(cmbVEH_LIC_ROAD.SelectedItem.Value);
			if(cmbREC_VEH_TYPE.SelectedItem!=null && cmbREC_VEH_TYPE.SelectedItem.Value!="")
				objNewInfo.REC_VEH_TYPE = int.Parse(cmbREC_VEH_TYPE.SelectedItem.Value);
			if(objNewInfo.REC_VEH_TYPE==iOther && txtREC_VEH_TYPE_DESC.Text.Trim()!="")
				objNewInfo.REC_VEH_TYPE_DESC = txtREC_VEH_TYPE_DESC.Text.Trim();
			if(objNewInfo.USED_IN_RACE_SPEED=="1" && txtUSED_IN_RACE_SPEED_CONTEST.Text.Trim()!="")
				objNewInfo.USED_IN_RACE_SPEED_CONTEST = txtUSED_IN_RACE_SPEED_CONTEST.Text.Trim();

			if(cmbOTHER_POLICY.SelectedItem!=null && cmbOTHER_POLICY.SelectedItem.Value!="")
				objNewInfo.OTHER_POLICY = cmbOTHER_POLICY.SelectedItem.Value;
			if(cmbC44.SelectedItem!=null && cmbC44.SelectedItem.Value!="")
				objNewInfo.C44 = int.Parse(cmbC44.SelectedItem.Value);
			
			int intRetVal;

			try
			{
				//Add new
				if ( this.hidREC_VEH_ID.Value == "0" )
				{
					int recVehID = objVehicle.AddPolicyUmbrellaRecVeh(objNewInfo);
					
					if ( recVehID == -1 ) 
					{
						return -1;
					}

					intRetVal = recVehID;
					this.hidREC_VEH_ID.Value = intRetVal.ToString();

				}
				else
				{
					objNewInfo.REC_VEH_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);

					//Populate old object
					Cms.Model.Policy.Umbrella.ClsRecrVehiclesInfo objOldInfo = new ClsRecrVehiclesInfo();
					base.PopulateModelObject(objOldInfo,hidOldData.Value);

					intRetVal = objVehicle.UpdatePolicyUmbrellaRecVeh(objOldInfo,objNewInfo);
				}
			}
			catch(Exception ex)
			{
				
				lblMessage.Text = ex.Message;

				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;
				}

				return -2;
			}

			return intRetVal;
						
		}
		
		/// <summary>
		/// Adds script to refresh web grid
		/// </summary>
		private void RegisterScript()
		{
			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				string keyValue = this.hidREC_VEH_ID.Value;

				string strCode = @"<script>RefreshWebGrid(1," + keyValue + ")</script>";

                ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strCode);

			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			//BL class
			ClsUmbrellaRecrVeh objVehicle = new ClsUmbrellaRecrVeh();
			
			lblMessage.Visible = true;
			
			int intRetVal = 0;

			try
			{
				intRetVal = objVehicle.DeletePolicyUmbrellaRecVeh(Convert.ToInt32(hidCustomerID.Value),
					Convert.ToInt32(this.hidPolicyID.Value),
					Convert.ToInt32(this.hidPolicyVersionID.Value),
					Convert.ToInt32(this.hidREC_VEH_ID.Value)
					);
					base.OpenEndorsementDetails();
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				return;
			}

			if ( intRetVal == 1 )
			{
				trMessage.Visible = true;
				trBody.Attributes.Add("style","display:none");
				lblInfo.Text = ClsMessages.GetMessage("G","127");
				this.hidOldData.Value = "";
				SetWorkFlow();

                if (!ClientScript.IsStartupScriptRegistered("Refresh"))
				{
					//string keyValue = this.hidREC_VEH_ID.Value;

					string strCode = @"<script>RefreshWebGrid(1,1)</script>";

                    ClientScript.RegisterStartupScript(this.GetType(),"Refresh", strCode);

				}
			}
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			string strAction;
			string strMessage;

			if ( this.hidIS_ACTIVE.Value == "Y" )
			{
				strAction = "N";
				strMessage = ClsMessages.GetMessage("G","41");
			}
			else
			{
				strAction = "Y";
				strMessage = ClsMessages.GetMessage("G","40");
			}

			lblMessage.Visible = true;

			
			try
			{
				int retVal = ClsUmbrellaRecrVeh.ActivateDeactivatePolicyUmbrellaRecVeh(Convert.ToInt32(hidCustomerID.Value),
					Convert.ToInt32(this.hidPolicyID.Value),
					Convert.ToInt32(this.hidPolicyVersionID.Value),
					Convert.ToInt32(this.hidREC_VEH_ID.Value),
					strAction);
			
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return ;
			}

			lblMessage.Visible = true;

			lblMessage.Text = strMessage;	
			RegisterScript();

			LoadData();

		}
		private void SetWorkFlow()
		{
			if(base.ScreenId == "276_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				if(hidREC_VEH_ID.Value!="0" && hidREC_VEH_ID.Value.Trim() != "")
				{
					myWorkFlow.AddKeyValue("REC_VEH_ID",hidREC_VEH_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}		

	}											 
}
