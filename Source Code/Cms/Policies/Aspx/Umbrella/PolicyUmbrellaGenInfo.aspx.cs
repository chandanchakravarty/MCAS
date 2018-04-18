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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Policy.Umbrella;

namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for UmbrellaGenInfo.
	/// </summary>
	public class PolicyUmbrellaGenInfo : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capANY_AIRCRAFT_OWNED_LEASED;
		protected System.Web.UI.WebControls.DropDownList cmbANY_AIRCRAFT_OWNED_LEASED;
		protected System.Web.UI.WebControls.Label capANY_OPERATOR_CON_TRAFFIC;
		protected System.Web.UI.WebControls.DropDownList cmbANY_OPERATOR_CON_TRAFFIC;
		protected System.Web.UI.WebControls.Label capANY_OPERATOR_IMPIRED;
		protected System.Web.UI.WebControls.DropDownList cmbANY_OPERATOR_IMPIRED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OPERATOR_IMPIRED;
		protected System.Web.UI.WebControls.Label capANY_SWIMMING_POOL;
		protected System.Web.UI.WebControls.DropDownList cmbANY_SWIMMING_POOL;
		protected System.Web.UI.WebControls.Label capREAL_STATE_VEHICLE_USED;
		protected System.Web.UI.WebControls.DropDownList cmbREAL_STATE_VEHICLE_USED;
		protected System.Web.UI.WebControls.Label capREAL_STATE_VEH_OWNED_HIRED;
		protected System.Web.UI.WebControls.DropDownList cmbREAL_STATE_VEH_OWNED_HIRED;
		protected System.Web.UI.WebControls.Label capENGAGED_IN_FARMING;
		protected System.Web.UI.WebControls.DropDownList cmbENGAGED_IN_FARMING;
		protected System.Web.UI.WebControls.Label capHOLD_NON_COMP_POSITION;
		protected System.Web.UI.WebControls.DropDownList cmbHOLD_NON_COMP_POSITION;
		protected System.Web.UI.WebControls.Label capANY_FULL_TIME_EMPLOYEE;
		protected System.Web.UI.WebControls.DropDownList cmbANY_FULL_TIME_EMPLOYEE;
		protected System.Web.UI.WebControls.Label capNON_OWNED_PROPERTY_CARE;
		protected System.Web.UI.WebControls.DropDownList cmbNON_OWNED_PROPERTY_CARE;
		protected System.Web.UI.WebControls.Label capBUSINESS_PROF_ACTIVITY;
		protected System.Web.UI.WebControls.DropDownList cmbBUSINESS_PROF_ACTIVITY;
		protected System.Web.UI.WebControls.Label capREDUCED_LIMIT_OF_LIBLITY;
		protected System.Web.UI.WebControls.DropDownList cmbREDUCED_LIMIT_OF_LIBLITY;
		protected System.Web.UI.WebControls.Label capANY_COVERAGE_DECLINED;
		protected System.Web.UI.WebControls.DropDownList cmbANY_COVERAGE_DECLINED;
		protected System.Web.UI.WebControls.Label capINSU_TRANSFERED_IN_AGENCY;
		protected System.Web.UI.WebControls.DropDownList cmbINSU_TRANSFERED_IN_AGENCY;
		protected System.Web.UI.WebControls.Label capPENDING_LITIGATIONS;
		protected System.Web.UI.WebControls.DropDownList cmbPENDING_LITIGATIONS;
		protected System.Web.UI.WebControls.Label capIS_TEMPOLINE;
		protected System.Web.UI.WebControls.DropDownList cmbIS_TEMPOLINE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.WebControls.Label capANIMALS_EXOTIC_PETS;
		protected System.Web.UI.WebControls.DropDownList cmbANIMALS_EXOTIC_PETS;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		private string strCustomerID, strPolicyId, strPolicyVersionId;
		protected System.Web.UI.WebControls.Label capANY_AIRCRAFT_OWNED_LEASED_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_AIRCRAFT_OWNED_LEASED_DESC;
		protected System.Web.UI.WebControls.Label lblANY_AIRCRAFT_OWNED_LEASED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_AIRCRAFT_OWNED_LEASED_DESC;
		protected System.Web.UI.WebControls.Label capANY_OPERATOR_CON_TRAFFIC_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_OPERATOR_CON_TRAFFIC_DESC;
		protected System.Web.UI.WebControls.Label lblANY_OPERATOR_CON_TRAFFIC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OPERATOR_CON_TRAFFIC_DESC;
		protected System.Web.UI.WebControls.Label capANY_OPERATOR_IMPIRED_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_OPERATOR_IMPIRED_DESC;
		protected System.Web.UI.WebControls.Label lblANY_OPERATOR_IMPIRED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OPERATOR_IMPIRED_DESC;
		protected System.Web.UI.WebControls.Label capANY_SWIMMING_POOL_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_SWIMMING_POOL_DESC;
		protected System.Web.UI.WebControls.Label lblANY_SWIMMING_POOL_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_SWIMMING_POOL_DESC;
		protected System.Web.UI.WebControls.Label capREAL_STATE_VEHICLE_USED_DESC;
		protected System.Web.UI.WebControls.TextBox txtREAL_STATE_VEHICLE_USED_DESC;
		protected System.Web.UI.WebControls.Label lblREAL_STATE_VEHICLE_USED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREAL_STATE_VEHICLE_USED_DESC;
		protected System.Web.UI.WebControls.Label capREAL_STATE_VEH_OWNED_HIRED_DESC;
		protected System.Web.UI.WebControls.TextBox txtREAL_STATE_VEH_OWNED_HIRED_DESC;
		protected System.Web.UI.WebControls.Label lblREAL_STATE_VEH_OWNED_HIRED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREAL_STATE_VEH_OWNED_HIRED_DESC;
		protected System.Web.UI.WebControls.Label capENGAGED_IN_FARMING_DESC;
		protected System.Web.UI.WebControls.TextBox txtENGAGED_IN_FARMING_DESC;
		protected System.Web.UI.WebControls.Label lblENGAGED_IN_FARMING_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvENGAGED_IN_FARMING_DESC;
		protected System.Web.UI.WebControls.Label capHOLD_NON_COMP_POSITION_DESC;
		protected System.Web.UI.WebControls.TextBox txtHOLD_NON_COMP_POSITION_DESC;
		protected System.Web.UI.WebControls.Label lblHOLD_NON_COMP_POSITION_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOLD_NON_COMP_POSITION_DESC;
		protected System.Web.UI.WebControls.Label capANY_FULL_TIME_EMPLOYEE_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_FULL_TIME_EMPLOYEE_DESC;
		protected System.Web.UI.WebControls.Label lblANY_FULL_TIME_EMPLOYEE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_FULL_TIME_EMPLOYEE_DESC;
		protected System.Web.UI.WebControls.Label capNON_OWNED_PROPERTY_CARE_DESC;
		protected System.Web.UI.WebControls.TextBox txtNON_OWNED_PROPERTY_CARE_DESC;
		protected System.Web.UI.WebControls.Label lblNON_OWNED_PROPERTY_CARE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNON_OWNED_PROPERTY_CARE_DESC;
		protected System.Web.UI.WebControls.Label capBUSINESS_PROF_ACTIVITY_DESC;
		protected System.Web.UI.WebControls.TextBox txtBUSINESS_PROF_ACTIVITY_DESC;
		protected System.Web.UI.WebControls.Label lblBUSINESS_PROF_ACTIVITY_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUSINESS_PROF_ACTIVITY_DESC;
		protected System.Web.UI.WebControls.Label capREDUCED_LIMIT_OF_LIBLITY_DESC;
		protected System.Web.UI.WebControls.TextBox txtREDUCED_LIMIT_OF_LIBLITY_DESC;
		protected System.Web.UI.WebControls.Label lblREDUCED_LIMIT_OF_LIBLITY_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREDUCED_LIMIT_OF_LIBLITY_DESC;
		protected System.Web.UI.WebControls.Label capANIMALS_EXOTIC_PETS_DESC;
		protected System.Web.UI.WebControls.TextBox txtANIMALS_EXOTIC_PETS_DESC;
		protected System.Web.UI.WebControls.Label lblANIMALS_EXOTIC_PETS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANIMALS_EXOTIC_PETS_DESC;
		protected System.Web.UI.WebControls.Label capANY_COVERAGE_DECLINED_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_COVERAGE_DECLINED_DESC;
		protected System.Web.UI.WebControls.Label lblANY_COVERAGE_DECLINED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_COVERAGE_DECLINED_DESC;
		protected System.Web.UI.WebControls.Label capPENDING_LITIGATIONS_DESC;
		protected System.Web.UI.WebControls.TextBox txtPENDING_LITIGATIONS_DESC;
		protected System.Web.UI.WebControls.Label lblPENDING_LITIGATIONS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPENDING_LITIGATIONS_DESC;
		protected System.Web.UI.WebControls.Label capINSU_TRANSFERED_IN_AGENCY_DESC;
		protected System.Web.UI.WebControls.TextBox txtINSU_TRANSFERED_IN_AGENCY_DESC;
		protected System.Web.UI.WebControls.Label lblINSU_TRANSFERED_IN_AGENCY_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSU_TRANSFERED_IN_AGENCY_DESC;
		protected System.Web.UI.WebControls.Label capIS_TEMPOLINE_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_TEMPOLINE_DESC;
		protected System.Web.UI.WebControls.Label lblIS_TEMPOLINE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_TEMPOLINE_DESC;
		protected System.Web.UI.WebControls.Label capHAVE_NON_OWNED_AUTO_POL;
		protected System.Web.UI.WebControls.DropDownList cmbHAVE_NON_OWNED_AUTO_POL;
		protected System.Web.UI.WebControls.Label capHAVE_NON_OWNED_AUTO_POL_DESC;
		protected System.Web.UI.WebControls.TextBox txtHAVE_NON_OWNED_AUTO_POL_DESC;
		protected System.Web.UI.WebControls.Label lblHAVE_NON_OWNED_AUTO_POL_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAVE_NON_OWNED_AUTO_POL_DESC;
		protected System.Web.UI.WebControls.Label capINS_DOMICILED_OUTSIDE;
		protected System.Web.UI.WebControls.DropDownList cmbINS_DOMICILED_OUTSIDE;
		protected System.Web.UI.WebControls.Label capINS_DOMICILED_OUTSIDE_DESC;
		protected System.Web.UI.WebControls.TextBox txtINS_DOMICILED_OUTSIDE_DESC;
		protected System.Web.UI.WebControls.Label lblINS_DOMICILED_OUTSIDE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINS_DOMICILED_OUTSIDE_DESC;
		protected System.Web.UI.WebControls.Label capHOME_DAY_CARE;
		protected System.Web.UI.WebControls.DropDownList cmbHOME_DAY_CARE;
		protected System.Web.UI.WebControls.Label capHOME_DAY_CARE_DESC;
		protected System.Web.UI.WebControls.TextBox txtHOME_DAY_CARE_DESC;
		protected System.Web.UI.WebControls.Label lblHOME_DAY_CARE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOME_DAY_CARE_DESC;
		protected System.Web.UI.WebControls.Label capHOME_RENT_DWELL;
		protected System.Web.UI.WebControls.DropDownList cmbHOME_RENT_DWELL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOME_RENT_DWELL;
		protected System.Web.UI.WebControls.Label capHOME_RENT_DWELL_DESC;
		protected System.Web.UI.WebControls.TextBox txtHOME_RENT_DWELL_DESC;
		protected System.Web.UI.WebControls.Label lblHOME_RENT_DWELL_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHOME_RENT_DWELL_DESC;
		protected System.Web.UI.WebControls.Label capWAT_DWELL;
		protected System.Web.UI.WebControls.DropDownList cmbWAT_DWELL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWAT_DWELL;
		protected System.Web.UI.WebControls.Label capWAT_DWELL_DESC;
		protected System.Web.UI.WebControls.TextBox txtWAT_DWELL_DESC;
		protected System.Web.UI.WebControls.Label lblWAT_DWELL_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWAT_DWELL_DESC;
		protected System.Web.UI.WebControls.Label capRECR_VEH;
		protected System.Web.UI.WebControls.DropDownList cmbRECR_VEH;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECR_VEH;
		protected System.Web.UI.WebControls.Label capRECR_VEH_DESC;
		protected System.Web.UI.WebControls.TextBox txtRECR_VEH_DESC;
		protected System.Web.UI.WebControls.Label lblRECR_VEH_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECR_VEH_DESC;
		protected System.Web.UI.WebControls.Label capAUTO_CYCL_TRUCKS;
		protected System.Web.UI.WebControls.DropDownList cmbAUTO_CYCL_TRUCKS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAUTO_CYCL_TRUCKS;
		protected System.Web.UI.WebControls.Label capAUTO_CYCL_TRUCKS_DESC;
		protected System.Web.UI.WebControls.TextBox txtAUTO_CYCL_TRUCKS_DESC;
		protected System.Web.UI.WebControls.Label lblAUTO_CYCL_TRUCKS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAUTO_CYCL_TRUCKS_DESC;
		protected System.Web.UI.WebControls.Label capAPPLI_UNDERSTAND_LIABILITY_EXCLUDED;
		protected System.Web.UI.WebControls.DropDownList cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPPLI_UNDERSTAND_LIABILITY_EXCLUDED;
		protected System.Web.UI.WebControls.Label capAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC;
		protected System.Web.UI.WebControls.TextBox txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC;
		protected System.Web.UI.WebControls.Label lblAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC;
		protected System.Web.UI.WebControls.Label capUND_REMARKS;
		protected System.Web.UI.WebControls.TextBox txtUND_REMARKS;
		protected System.Web.UI.WebControls.CustomValidator csvUND_REMARKS;
		protected System.Web.UI.WebControls.Label capCALCULATIONS;
		protected System.Web.UI.WebControls.TextBox txtCALCULATIONS;
		protected System.Web.UI.WebControls.CustomValidator csvCALCULATIONS;
		protected System.Web.UI.HtmlControls.HtmlForm POL_UMBRELLA_GEN_INFO;
		protected System.Web.UI.WebControls.Label capOFFICE_PREMISES;
		protected System.Web.UI.WebControls.DropDownList cmbOFFICE_PREMISES;
		protected System.Web.UI.WebControls.Label capRENTAL_DWELLINGS_UNIT;
		protected System.Web.UI.WebControls.DropDownList cmbRENTAL_DWELLINGS_UNIT;
		protected System.Web.UI.WebControls.Label capFAMILIES;
		protected System.Web.UI.WebControls.TextBox txtFAMILIES;
		protected System.Web.UI.WebControls.RangeValidator rngFAMILIES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFAMILIES;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			base.ScreenId = "280";//can be Alpha Numeric 
			GetSessionValues();

			btnReset.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString =	gstrSecurityXML;	
				
			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;	
			/////////////////////
			
			if ( !Page.IsPostBack )
			{
				btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
				
				//Get the relevant IDs
				hidCustomerID.Value = GetCustomerID();
				hidPolicyID.Value = GetPolicyID();
				hidPolicyVersionID.Value = GetPolicyVersionID();
			
				if ( hidCustomerID.Value != "" && hidCustomerID.Value != "0" && 
					hidPolicyID.Value != "" && hidPolicyID.Value != "0" &&
					hidPolicyVersionID.Value != "" && hidPolicyVersionID.Value != "0"
					)

				{
					//Set the labels
					SetCaptions();
					
					//Set messages for Validators
					SetErrorMessages();
					
					//Populate the fields
					LoadData();
					
					//Ravindra Gupta(02/15/2006) -- To display Client Control

					cltClientTop.PolicyID = int.Parse(strPolicyId);
					cltClientTop.CustomerID = int.Parse(strCustomerID);
					cltClientTop.PolicyVersionID = int.Parse(strPolicyVersionId);
					cltClientTop.ShowHeaderBand = "Policy";
					cltClientTop.Visible= true;

					//Set focus
					this.SetFocus("cmbANY_AIRCRAFT_OWNED_LEASED");
					#region set Workflow cntrol
					SetWorkFlow();
					
					#endregion
				}
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// Retrieves daa from database for update
		/// </summary>
		private void LoadData()
		{
			//BL class
			ClsUmbrellaGen objGen = new ClsUmbrellaGen();
 
			DataSet dsInfo  = objGen.GetPolUmbrellaGenInfo(Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(this.hidPolicyID.Value),
				Convert.ToInt32(this.hidPolicyVersionID.Value)
				);

			
			if ( dsInfo.Tables[0].Rows.Count == 0 )
			{
				return;
			}

			//this.hidOldData.Value = dsInfo.GetXml();
			
			this.hidOldData.Value = ClsCommon.GetXMLEncoded(dsInfo.Tables[0]);

			DataTable dtInfo = dsInfo.Tables[0];
			
			ClsCommon.SelectValueinDDL(cmbANY_AIRCRAFT_OWNED_LEASED,dtInfo.Rows[0]["ANY_AIRCRAFT_OWNED_LEASED"]);
			ClsCommon.SelectValueinDDL(cmbANY_COVERAGE_DECLINED,dtInfo.Rows[0]["ANY_COVERAGE_DECLINED"]);
			ClsCommon.SelectValueinDDL(cmbANY_FULL_TIME_EMPLOYEE,dtInfo.Rows[0]["ANY_FULL_TIME_EMPLOYEE"]);
			ClsCommon.SelectValueinDDL(cmbANY_OPERATOR_CON_TRAFFIC,dtInfo.Rows[0]["ANY_OPERATOR_CON_TRAFFIC"]);
			ClsCommon.SelectValueinDDL(cmbANY_OPERATOR_IMPIRED,dtInfo.Rows[0]["ANY_OPERATOR_IMPIRED"]);
			ClsCommon.SelectValueinDDL(cmbANY_SWIMMING_POOL,dtInfo.Rows[0]["ANY_SWIMMING_POOL"]);
			ClsCommon.SelectValueinDDL(cmbBUSINESS_PROF_ACTIVITY,dtInfo.Rows[0]["BUSINESS_PROF_ACTIVITY"]);
			ClsCommon.SelectValueinDDL(cmbENGAGED_IN_FARMING,dtInfo.Rows[0]["ENGAGED_IN_FARMING"]);
			ClsCommon.SelectValueinDDL(cmbHOLD_NON_COMP_POSITION,dtInfo.Rows[0]["HOLD_NON_COMP_POSITION"]);
			ClsCommon.SelectValueinDDL(cmbINSU_TRANSFERED_IN_AGENCY,dtInfo.Rows[0]["INSU_TRANSFERED_IN_AGENCY"]);
			ClsCommon.SelectValueinDDL(cmbIS_TEMPOLINE,dtInfo.Rows[0]["IS_TEMPOLINE"]);
			ClsCommon.SelectValueinDDL(cmbNON_OWNED_PROPERTY_CARE,dtInfo.Rows[0]["NON_OWNED_PROPERTY_CARE"]);
			ClsCommon.SelectValueinDDL(cmbPENDING_LITIGATIONS,dtInfo.Rows[0]["PENDING_LITIGATIONS"]);
			ClsCommon.SelectValueinDDL(cmbREAL_STATE_VEH_OWNED_HIRED,dtInfo.Rows[0]["REAL_STATE_VEH_OWNED_HIRED"]);
			ClsCommon.SelectValueinDDL(cmbREAL_STATE_VEHICLE_USED,dtInfo.Rows[0]["REAL_STATE_VEHICLE_USED"]);
			ClsCommon.SelectValueinDDL(cmbREDUCED_LIMIT_OF_LIBLITY,dtInfo.Rows[0]["REDUCED_LIMIT_OF_LIBLITY"]);
			ClsCommon.SelectValueinDDL(this.cmbANIMALS_EXOTIC_PETS,dtInfo.Rows[0]["ANIMALS_EXOTIC_PETS"]);
			ClsCommon.SelectValueinDDL(cmbHAVE_NON_OWNED_AUTO_POL,dtInfo.Rows[0]["HAVE_NON_OWNED_AUTO_POL"]);
			ClsCommon.SelectValueinDDL(cmbINS_DOMICILED_OUTSIDE,dtInfo.Rows[0]["INS_DOMICILED_OUTSIDE"]);
			ClsCommon.SelectValueinDDL(cmbHOME_DAY_CARE,dtInfo.Rows[0]["HOME_DAY_CARE"]);
			ClsCommon.SelectValueinDDL(cmbHOME_RENT_DWELL,dtInfo.Rows[0]["HOME_RENT_DWELL"]);
			ClsCommon.SelectValueinDDL(cmbWAT_DWELL,dtInfo.Rows[0]["WAT_DWELL"]);
			ClsCommon.SelectValueinDDL(cmbRECR_VEH,dtInfo.Rows[0]["RECR_VEH"]);
			ClsCommon.SelectValueinDDL(cmbAUTO_CYCL_TRUCKS,dtInfo.Rows[0]["AUTO_CYCL_TRUCKS"]);
			ClsCommon.SelectValueinDDL(cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED,dtInfo.Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED"]);
			//added 0n 5 th jan 2007 by Manoj
			ClsCommon.SelectValueinDDL(cmbOFFICE_PREMISES,dtInfo.Rows[0]["OFFICE_PREMISES"]);
			ClsCommon.SelectValueinDDL(cmbRENTAL_DWELLINGS_UNIT,dtInfo.Rows[0]["RENTAL_DWELLINGS_UNIT"]);
			
			
			this.txtHOLD_NON_COMP_POSITION_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["HOLD_NON_COMP_POSITION_DESC"]);
			this.txtANY_FULL_TIME_EMPLOYEE_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["ANY_FULL_TIME_EMPLOYEE_DESC"]);
			this.txtNON_OWNED_PROPERTY_CARE_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["NON_OWNED_PROPERTY_CARE_DESC"]);
			this.txtBUSINESS_PROF_ACTIVITY_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["BUSINESS_PROF_ACTIVITY_DESC"]);
			this.txtREDUCED_LIMIT_OF_LIBLITY_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["REDUCED_LIMIT_OF_LIBLITY_DESC"]);
			this.txtANIMALS_EXOTIC_PETS_DESC.Text			= Convert.ToString(dtInfo.Rows[0]["ANIMALS_EXOTIC_PETS_DESC"]);
			this.txtANY_COVERAGE_DECLINED_DESC.Text			= Convert.ToString(dtInfo.Rows[0]["ANY_COVERAGE_DECLINED_DESC"]);
			this.txtINSU_TRANSFERED_IN_AGENCY_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["INSU_TRANSFERED_IN_AGENCY_DESC"]);
			this.txtPENDING_LITIGATIONS_DESC.Text			= Convert.ToString(dtInfo.Rows[0]["PENDING_LITIGATIONS_DESC"]);
			this.txtIS_TEMPOLINE_DESC.Text					= Convert.ToString(dtInfo.Rows[0]["IS_TEMPOLINE_DESC"]);
			this.txtHAVE_NON_OWNED_AUTO_POL_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["HAVE_NON_OWNED_AUTO_POL_DESC"]);
			this.txtINS_DOMICILED_OUTSIDE_DESC.Text			= Convert.ToString(dtInfo.Rows[0]["INS_DOMICILED_OUTSIDE_DESC"]);
			this.txtHOME_DAY_CARE_DESC.Text					= Convert.ToString(dtInfo.Rows[0]["HOME_DAY_CARE_DESC"]);
			this.txtCALCULATIONS.Text						= Convert.ToString(dtInfo.Rows[0]["CALCULATIONS"]);
			this.txtHOME_RENT_DWELL_DESC.Text				= Convert.ToString(dtInfo.Rows[0]["HOME_RENT_DWELL_DESC"]);
			this.txtWAT_DWELL_DESC.Text						= Convert.ToString(dtInfo.Rows[0]["WAT_DWELL_DESC"]);
			this.txtRECR_VEH_DESC.Text						= Convert.ToString(dtInfo.Rows[0]["RECR_VEH_DESC"]);
			this.txtAUTO_CYCL_TRUCKS_DESC.Text				= Convert.ToString(dtInfo.Rows[0]["AUTO_CYCL_TRUCKS_DESC"]);
			this.txtUND_REMARKS.Text						= Convert.ToString(dtInfo.Rows[0]["UND_REMARKS"]);
			this.txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC"]);
			this.txtANY_AIRCRAFT_OWNED_LEASED_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["ANY_AIRCRAFT_OWNED_LEASED_DESC"]);
			this.txtANY_OPERATOR_CON_TRAFFIC_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["ANY_OPERATOR_CON_TRAFFIC_DESC"]);
			this.txtANY_OPERATOR_IMPIRED_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["ANY_OPERATOR_IMPIRED_DESC"]);
			this.txtANY_SWIMMING_POOL_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["ANY_SWIMMING_POOL_DESC"]);
			this.txtREAL_STATE_VEHICLE_USED_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["REAL_STATE_VEHICLE_USED_DESC"]);
			this.txtREAL_STATE_VEH_OWNED_HIRED_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["REAL_STATE_VEH_OWNED_HIRED_DESC"]);
			this.txtENGAGED_IN_FARMING_DESC.Text		= Convert.ToString(dtInfo.Rows[0]["ENGAGED_IN_FARMING_DESC"]);
			this.txtFAMILIES.Text		= Convert.ToString(dtInfo.Rows[0]["FAMILIES"]);
			
			
		}

		/// <summary>
		/// Sets the text of the labels
		/// </summary>
		private void SetCaptions()
		{	
			ResourceManager objResourceMgr = new ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyUmbrellaGenInfo",System.Reflection.Assembly.GetExecutingAssembly());
			
			capANY_AIRCRAFT_OWNED_LEASED.Text		= objResourceMgr.GetString("cmbANY_AIRCRAFT_OWNED_LEASED");
			capANY_OPERATOR_CON_TRAFFIC.Text		= objResourceMgr.GetString("cmbANY_OPERATOR_CON_TRAFFIC");
			capANY_OPERATOR_IMPIRED.Text			= objResourceMgr.GetString("cmbANY_OPERATOR_IMPIRED");
			capANY_SWIMMING_POOL.Text				= objResourceMgr.GetString("cmbANY_SWIMMING_POOL");
			capREAL_STATE_VEHICLE_USED.Text			= objResourceMgr.GetString("cmbREAL_STATE_VEHICLE_USED");
			capREAL_STATE_VEH_OWNED_HIRED.Text		= objResourceMgr.GetString("cmbREAL_STATE_VEH_OWNED_HIRED");
			capENGAGED_IN_FARMING.Text				= objResourceMgr.GetString("cmbENGAGED_IN_FARMING");
			capHOLD_NON_COMP_POSITION.Text			= objResourceMgr.GetString("cmbHOLD_NON_COMP_POSITION");
			capANY_FULL_TIME_EMPLOYEE.Text			= objResourceMgr.GetString("cmbANY_FULL_TIME_EMPLOYEE");
			capNON_OWNED_PROPERTY_CARE.Text			= objResourceMgr.GetString("cmbNON_OWNED_PROPERTY_CARE");
			capBUSINESS_PROF_ACTIVITY.Text			= objResourceMgr.GetString("cmbBUSINESS_PROF_ACTIVITY");
			capREDUCED_LIMIT_OF_LIBLITY.Text		= objResourceMgr.GetString("cmbREDUCED_LIMIT_OF_LIBLITY");
			capANY_COVERAGE_DECLINED.Text			= objResourceMgr.GetString("cmbANY_COVERAGE_DECLINED");
			capANIMALS_EXOTIC_PETS.Text				= objResourceMgr.GetString("cmbANIMALS_EXOTIC_PETS");
			capINSU_TRANSFERED_IN_AGENCY.Text		= objResourceMgr.GetString("cmbINSU_TRANSFERED_IN_AGENCY");
			capPENDING_LITIGATIONS.Text				= objResourceMgr.GetString("cmbPENDING_LITIGATIONS");
			capIS_TEMPOLINE.Text					= objResourceMgr.GetString("cmbIS_TEMPOLINE");
			capANY_AIRCRAFT_OWNED_LEASED_DESC.Text	= objResourceMgr.GetString("txtANY_AIRCRAFT_OWNED_LEASED_DESC");
			capANY_OPERATOR_CON_TRAFFIC_DESC.Text	= objResourceMgr.GetString("txtANY_OPERATOR_CON_TRAFFIC_DESC");
			capANY_OPERATOR_IMPIRED_DESC.Text		= objResourceMgr.GetString("txtANY_OPERATOR_IMPIRED_DESC");
			capANY_SWIMMING_POOL_DESC.Text			= objResourceMgr.GetString("txtANY_SWIMMING_POOL_DESC");
			capREAL_STATE_VEHICLE_USED_DESC.Text	= objResourceMgr.GetString("txtREAL_STATE_VEHICLE_USED_DESC");
			capREAL_STATE_VEH_OWNED_HIRED_DESC.Text	= objResourceMgr.GetString("txtREAL_STATE_VEH_OWNED_HIRED_DESC");
			capENGAGED_IN_FARMING_DESC.Text			= objResourceMgr.GetString("txtENGAGED_IN_FARMING_DESC");
			capHOLD_NON_COMP_POSITION_DESC.Text		= objResourceMgr.GetString("txtHOLD_NON_COMP_POSITION_DESC");
			capANY_FULL_TIME_EMPLOYEE_DESC.Text		= objResourceMgr.GetString("txtANY_FULL_TIME_EMPLOYEE_DESC");
			capNON_OWNED_PROPERTY_CARE_DESC.Text	= objResourceMgr.GetString("txtNON_OWNED_PROPERTY_CARE_DESC");
			capBUSINESS_PROF_ACTIVITY_DESC.Text		= objResourceMgr.GetString("txtBUSINESS_PROF_ACTIVITY_DESC");
			capREDUCED_LIMIT_OF_LIBLITY_DESC.Text	= objResourceMgr.GetString("txtREDUCED_LIMIT_OF_LIBLITY_DESC");
			capANIMALS_EXOTIC_PETS_DESC.Text		= objResourceMgr.GetString("txtANIMALS_EXOTIC_PETS_DESC");
			capANY_COVERAGE_DECLINED_DESC.Text		= objResourceMgr.GetString("txtANY_COVERAGE_DECLINED_DESC");
			capINSU_TRANSFERED_IN_AGENCY_DESC.Text	= objResourceMgr.GetString("txtINSU_TRANSFERED_IN_AGENCY_DESC");
			capPENDING_LITIGATIONS_DESC.Text		= objResourceMgr.GetString("txtPENDING_LITIGATIONS_DESC");
			capIS_TEMPOLINE_DESC.Text				= objResourceMgr.GetString("txtIS_TEMPOLINE_DESC");
			capHAVE_NON_OWNED_AUTO_POL.Text			= objResourceMgr.GetString("cmbHAVE_NON_OWNED_AUTO_POL");
			capHAVE_NON_OWNED_AUTO_POL_DESC.Text	= objResourceMgr.GetString("txtHAVE_NON_OWNED_AUTO_POL_DESC");
			capINS_DOMICILED_OUTSIDE.Text			= objResourceMgr.GetString("cmbINS_DOMICILED_OUTSIDE");
			capINS_DOMICILED_OUTSIDE_DESC.Text		= objResourceMgr.GetString("txtINS_DOMICILED_OUTSIDE_DESC");
			capHOME_DAY_CARE.Text					= objResourceMgr.GetString("cmbHOME_DAY_CARE");
			capHOME_DAY_CARE_DESC.Text				= objResourceMgr.GetString("txtHOME_DAY_CARE_DESC");
			capCALCULATIONS.Text					= objResourceMgr.GetString("txtCALCULATIONS");
			capHOME_RENT_DWELL.Text					= objResourceMgr.GetString("cmbHOME_RENT_DWELL");
			capHOME_RENT_DWELL_DESC.Text			= objResourceMgr.GetString("txtHOME_RENT_DWELL_DESC");
			capWAT_DWELL.Text						= objResourceMgr.GetString("cmbWAT_DWELL");
			capWAT_DWELL_DESC.Text					= objResourceMgr.GetString("txtWAT_DWELL_DESC");
			capRECR_VEH.Text						= objResourceMgr.GetString("cmbRECR_VEH");
			capRECR_VEH_DESC.Text					= objResourceMgr.GetString("txtRECR_VEH_DESC");
			capAUTO_CYCL_TRUCKS.Text				= objResourceMgr.GetString("cmbAUTO_CYCL_TRUCKS");
			capAUTO_CYCL_TRUCKS_DESC.Text			= objResourceMgr.GetString("txtAUTO_CYCL_TRUCKS_DESC");
			capUND_REMARKS.Text						= objResourceMgr.GetString("txtUND_REMARKS");
			capAPPLI_UNDERSTAND_LIABILITY_EXCLUDED.Text			= objResourceMgr.GetString("cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED");
			capAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC.Text	= objResourceMgr.GetString("txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC");
			capFAMILIES.Text						= objResourceMgr.GetString("txtFAMILIES");



		}

		
		/// <summary>
		/// Sets the validator error messages
		/// </summary>
		private void SetErrorMessages()
		{
			rfvANY_AIRCRAFT_OWNED_LEASED_DESC.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvANY_OPERATOR_CON_TRAFFIC_DESC.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvANY_OPERATOR_IMPIRED_DESC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvANY_SWIMMING_POOL_DESC.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvENGAGED_IN_FARMING_DESC.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvREAL_STATE_VEH_OWNED_HIRED_DESC.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvREAL_STATE_VEHICLE_USED_DESC.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvHOLD_NON_COMP_POSITION_DESC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			rfvANY_FULL_TIME_EMPLOYEE_DESC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvNON_OWNED_PROPERTY_CARE_DESC.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			rfvBUSINESS_PROF_ACTIVITY_DESC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			rfvREDUCED_LIMIT_OF_LIBLITY_DESC.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvANIMALS_EXOTIC_PETS_DESC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			rfvANY_COVERAGE_DECLINED_DESC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			rfvINSU_TRANSFERED_IN_AGENCY_DESC.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			rfvPENDING_LITIGATIONS_DESC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			rfvIS_TEMPOLINE_DESC.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
			rfvHAVE_NON_OWNED_AUTO_POL_DESC.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"18");
			rfvINS_DOMICILED_OUTSIDE_DESC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			rfvHOME_DAY_CARE_DESC.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
			rfvHOME_RENT_DWELL_DESC.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21");
			rfvWAT_DWELL_DESC.ErrorMessage						= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			rfvRECR_VEH_DESC.ErrorMessage						= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"23");
			rfvAUTO_CYCL_TRUCKS_DESC.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			rfvAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"25");
			rfvHOME_RENT_DWELL.ErrorMessage						= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"26");
			rfvWAT_DWELL.ErrorMessage							= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"27");
			rfvRECR_VEH.ErrorMessage							= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"28");
			rfvAUTO_CYCL_TRUCKS.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"29");
			rfvAPPLI_UNDERSTAND_LIABILITY_EXCLUDED.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"30");
			rngFAMILIES.ErrorMessage							= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"31");
			rfvFAMILIES.ErrorMessage							= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
		



		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblMessage.Visible = true;

			int retVal = Save();

			if ( retVal > 0 )
			{
				base.OpenEndorsementDetails();
				LoadData();
				//lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");	
				#region set Workflow cntrol
				SetWorkFlow();
				#endregion
			}
		}
		
		/// <summary>
		/// Saves the current record in the database
		/// </summary>
		/// <returns></returns>
		private int Save()
		{
			//BL class
			ClsUmbrellaGen objGen = new ClsUmbrellaGen();
			
			//Info class
			ClsUmbrellaGenInfo objNewInfo = new ClsUmbrellaGenInfo();

			objNewInfo.CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);
			objNewInfo.POLICY_ID = Convert.ToInt32(hidPolicyID.Value);
			objNewInfo.POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID.Value);
			objNewInfo.MODIFIED_BY = Convert.ToInt32(GetUserId());
			objNewInfo.CREATED_BY = Convert.ToInt32(GetUserId());
			
			// Are any aircraft owned, leased, chartered or furnished
			objNewInfo.ANY_AIRCRAFT_OWNED_LEASED		= this.cmbANY_AIRCRAFT_OWNED_LEASED.SelectedItem.Value;
			if(cmbANY_AIRCRAFT_OWNED_LEASED.SelectedValue == "N" || cmbANY_AIRCRAFT_OWNED_LEASED.SelectedValue == "")
				objNewInfo.ANY_AIRCRAFT_OWNED_LEASED_DESC =	"";
			else
				objNewInfo.ANY_AIRCRAFT_OWNED_LEASED_DESC = this.txtANY_AIRCRAFT_OWNED_LEASED_DESC.Text.Trim();
					
			// Traffic Violation Details
			objNewInfo.ANY_OPERATOR_CON_TRAFFIC			= this.cmbANY_OPERATOR_CON_TRAFFIC.SelectedItem.Value;
			if(cmbANY_OPERATOR_CON_TRAFFIC.SelectedValue == "N" || cmbANY_OPERATOR_CON_TRAFFIC.SelectedValue == "")
				objNewInfo.ANY_OPERATOR_CON_TRAFFIC_DESC =	"";
			else
				objNewInfo.ANY_OPERATOR_CON_TRAFFIC_DESC = this.txtANY_OPERATOR_CON_TRAFFIC_DESC.Text.Trim();
		
			//  physically or mentally impaired
			objNewInfo.ANY_OPERATOR_IMPIRED				= this.cmbANY_OPERATOR_IMPIRED.SelectedItem.Value;
			if(cmbANY_OPERATOR_IMPIRED.SelectedValue == "N" || cmbANY_OPERATOR_IMPIRED.SelectedValue == "")
				objNewInfo.ANY_OPERATOR_IMPIRED_DESC =	"";
			else
				objNewInfo.ANY_OPERATOR_IMPIRED_DESC	= this.txtANY_OPERATOR_IMPIRED_DESC.Text.Trim();
		
			// Pool/Tub or Spa
			objNewInfo.ANY_SWIMMING_POOL				= this.cmbANY_SWIMMING_POOL.SelectedItem.Value;
			if(cmbANY_SWIMMING_POOL.SelectedValue == "N" || cmbANY_SWIMMING_POOL.SelectedValue == "")
				objNewInfo.ANY_SWIMMING_POOL_DESC =	"";
			else
				objNewInfo.ANY_SWIMMING_POOL_DESC		= this.txtANY_SWIMMING_POOL_DESC.Text.Trim();
			
			// Commercial/Business Use 
			objNewInfo.REAL_STATE_VEHICLE_USED			= this.cmbREAL_STATE_VEHICLE_USED.SelectedItem.Value;
			if(cmbREAL_STATE_VEH_OWNED_HIRED.SelectedValue == "N" || cmbREAL_STATE_VEH_OWNED_HIRED.SelectedValue == "")
				objNewInfo.REAL_STATE_VEHICLE_USED_DESC =	"";
			else
				objNewInfo.REAL_STATE_VEHICLE_USED_DESC	= this.txtREAL_STATE_VEHICLE_USED_DESC.Text.Trim();
		
			// Any real estate, vehicles,  watercraft, aircraft owned, hired, leased 
			objNewInfo.REAL_STATE_VEH_OWNED_HIRED		= this.cmbREAL_STATE_VEH_OWNED_HIRED.SelectedItem.Value;
			if(cmbREAL_STATE_VEH_OWNED_HIRED.SelectedValue == "N" || cmbREAL_STATE_VEH_OWNED_HIRED.SelectedValue == "")
				objNewInfo.REAL_STATE_VEH_OWNED_HIRED_DESC =	"";
			else
				objNewInfo.REAL_STATE_VEH_OWNED_HIRED_DESC		= this.txtREAL_STATE_VEH_OWNED_HIRED_DESC.Text.Trim();
		
			//  Farming Operation Details 
			objNewInfo.ENGAGED_IN_FARMING	= this.cmbENGAGED_IN_FARMING.SelectedItem.Value;
			if(cmbENGAGED_IN_FARMING.SelectedValue == "N" || cmbENGAGED_IN_FARMING.SelectedValue == "")
				objNewInfo.ENGAGED_IN_FARMING_DESC =	"";
			else
				objNewInfo.ENGAGED_IN_FARMING_DESC		= this.txtENGAGED_IN_FARMING_DESC.Text.Trim();
			
			// Non compensated Position
			objNewInfo.HOLD_NON_COMP_POSITION	= this.cmbHOLD_NON_COMP_POSITION.SelectedItem.Value;
			if(cmbHOLD_NON_COMP_POSITION.SelectedValue == "N" || cmbHOLD_NON_COMP_POSITION.SelectedValue == "")
				objNewInfo.HOLD_NON_COMP_POSITION_DESC =	"";
			else
				objNewInfo.HOLD_NON_COMP_POSITION_DESC	= this.txtHOLD_NON_COMP_POSITION_DESC.Text.Trim();
			
			// Any full time employees
			objNewInfo.ANY_FULL_TIME_EMPLOYEE	= this.cmbANY_FULL_TIME_EMPLOYEE.SelectedItem.Value;
			if(cmbANY_FULL_TIME_EMPLOYEE.SelectedValue == "N" || cmbANY_FULL_TIME_EMPLOYEE.SelectedValue == "")
				objNewInfo.ANY_FULL_TIME_EMPLOYEE_DESC =	"";
			else
				objNewInfo.ANY_FULL_TIME_EMPLOYEE_DESC	= this.txtANY_FULL_TIME_EMPLOYEE_DESC.Text.Trim();
			
			//  Non-Owned Property
			objNewInfo.NON_OWNED_PROPERTY_CARE	= this.cmbNON_OWNED_PROPERTY_CARE.SelectedItem.Value;
			if(cmbNON_OWNED_PROPERTY_CARE.SelectedValue == "N" || cmbNON_OWNED_PROPERTY_CARE.SelectedValue == "")
				objNewInfo.NON_OWNED_PROPERTY_CARE_DESC =	"";
			else
				objNewInfo.NON_OWNED_PROPERTY_CARE_DESC	= this.txtNON_OWNED_PROPERTY_CARE_DESC.Text.Trim();
			
			// Business/Professional Primary
			objNewInfo.BUSINESS_PROF_ACTIVITY	= this.cmbBUSINESS_PROF_ACTIVITY.SelectedItem.Value;
			if(cmbBUSINESS_PROF_ACTIVITY.SelectedValue == "N" || cmbBUSINESS_PROF_ACTIVITY.SelectedValue == "")
				objNewInfo.BUSINESS_PROF_ACTIVITY_DESC =	"";
			else
				objNewInfo.BUSINESS_PROF_ACTIVITY_DESC	= this.txtBUSINESS_PROF_ACTIVITY_DESC.Text.Trim();
			
			// Reduced Limits 
			objNewInfo.REDUCED_LIMIT_OF_LIBLITY	= this.cmbREDUCED_LIMIT_OF_LIBLITY.SelectedItem.Value;
			if(cmbREDUCED_LIMIT_OF_LIBLITY.SelectedValue == "N" || cmbREDUCED_LIMIT_OF_LIBLITY.SelectedValue == "")
				objNewInfo.REDUCED_LIMIT_OF_LIBLITY_DESC =	"";
			else
				objNewInfo.REDUCED_LIMIT_OF_LIBLITY_DESC	= this.txtREDUCED_LIMIT_OF_LIBLITY_DESC.Text.Trim();
			
			// Exotic Pets
			objNewInfo.ANIMALS_EXOTIC_PETS	= this.cmbANIMALS_EXOTIC_PETS.SelectedItem.Value;
			if(cmbANIMALS_EXOTIC_PETS.SelectedValue == "N" || cmbANIMALS_EXOTIC_PETS.SelectedValue == "")
				objNewInfo.ANIMALS_EXOTIC_PETS_DESC =	"";
			else
				objNewInfo.ANIMALS_EXOTIC_PETS_DESC	= this.txtANIMALS_EXOTIC_PETS_DESC.Text.Trim();
			
			// Any coverage declined, cancelled or non-renewed
			objNewInfo.ANY_COVERAGE_DECLINED = this.cmbANY_COVERAGE_DECLINED.SelectedItem.Value;
			if(cmbANY_COVERAGE_DECLINED.SelectedValue == "N" || cmbANY_COVERAGE_DECLINED.SelectedValue == "")
				objNewInfo.ANY_COVERAGE_DECLINED_DESC =	"";
			else
				objNewInfo.ANY_COVERAGE_DECLINED_DESC	= this.txtANY_COVERAGE_DECLINED_DESC.Text.Trim();

			// Insurance been transferred within the agency
			objNewInfo.INSU_TRANSFERED_IN_AGENCY	= this.cmbINSU_TRANSFERED_IN_AGENCY.SelectedItem.Value;
			if(cmbINSU_TRANSFERED_IN_AGENCY.SelectedValue == "N" || cmbINSU_TRANSFERED_IN_AGENCY.SelectedValue == "")
				objNewInfo.INSU_TRANSFERED_IN_AGENCY_DESC =	"";
			else
				objNewInfo.INSU_TRANSFERED_IN_AGENCY_DESC	= this.txtINSU_TRANSFERED_IN_AGENCY_DESC.Text.Trim();
					
			// Pending litigations court  proceedings
			objNewInfo.PENDING_LITIGATIONS	= this.cmbPENDING_LITIGATIONS.SelectedItem.Value;
			if(cmbPENDING_LITIGATIONS.SelectedValue == "N" || cmbPENDING_LITIGATIONS.SelectedValue == "")
				objNewInfo.PENDING_LITIGATIONS_DESC =	"";
			else
				objNewInfo.PENDING_LITIGATIONS_DESC	= this.txtPENDING_LITIGATIONS_DESC.Text.Trim();
			
			// Trampoline on the premises
			objNewInfo.IS_TEMPOLINE	= this.cmbIS_TEMPOLINE.SelectedItem.Value;
			if(cmbIS_TEMPOLINE.SelectedValue == "N" || cmbIS_TEMPOLINE.SelectedValue == "")
				objNewInfo.IS_TEMPOLINE_DESC =	"";
			else
				objNewInfo.IS_TEMPOLINE_DESC = this.txtIS_TEMPOLINE_DESC.Text.Trim();

			// Non Owned Auto Policy
			objNewInfo.HAVE_NON_OWNED_AUTO_POL	= this.cmbHAVE_NON_OWNED_AUTO_POL.SelectedItem.Value;
			if(cmbHAVE_NON_OWNED_AUTO_POL.SelectedValue == "N" || cmbHAVE_NON_OWNED_AUTO_POL.SelectedValue == "")
				objNewInfo.HAVE_NON_OWNED_AUTO_POL_DESC =	"";
			else
				objNewInfo.HAVE_NON_OWNED_AUTO_POL_DESC	= this.txtHAVE_NON_OWNED_AUTO_POL_DESC.Text.Trim();

			// Permanently Domiciled
			objNewInfo.INS_DOMICILED_OUTSIDE = this.cmbINS_DOMICILED_OUTSIDE.SelectedItem.Value;
			if(cmbINS_DOMICILED_OUTSIDE.SelectedValue == "N" || cmbINS_DOMICILED_OUTSIDE.SelectedValue == "")
				objNewInfo.INS_DOMICILED_OUTSIDE_DESC =	"";
			else
				objNewInfo.INS_DOMICILED_OUTSIDE_DESC	= this.txtINS_DOMICILED_OUTSIDE_DESC.Text.Trim();

			// Home Day Care
			objNewInfo.HOME_DAY_CARE	= this.cmbHOME_DAY_CARE.SelectedItem.Value;
			if(cmbHOME_DAY_CARE.SelectedValue == "N" || cmbHOME_DAY_CARE.SelectedValue == "")
				objNewInfo.HOME_DAY_CARE_DESC =	"";
			else
				objNewInfo.HOME_DAY_CARE_DESC = this.txtHOME_DAY_CARE_DESC.Text.Trim();
		
			// Underlying Policies Remarks
			objNewInfo.UND_REMARKS		= this.txtUND_REMARKS.Text.Trim();

			// Calculations
			objNewInfo.CALCULATIONS		= this.txtCALCULATIONS.Text.Trim();

			// Home/Rental Dwellings
			objNewInfo.HOME_RENT_DWELL	= this.cmbHOME_RENT_DWELL.SelectedItem.Value;
			if(cmbHOME_RENT_DWELL.SelectedValue == "Y" || cmbHOME_RENT_DWELL.SelectedValue == "" || cmbHOME_RENT_DWELL.SelectedValue == "0")
				objNewInfo.HOME_RENT_DWELL_DESC =	"";
			else
				objNewInfo.HOME_RENT_DWELL_DESC = this.txtHOME_RENT_DWELL_DESC.Text.Trim();

			// Watercraft dwellings
			objNewInfo.WAT_DWELL	= this.cmbWAT_DWELL.SelectedItem.Value;
			if(cmbWAT_DWELL.SelectedValue == "Y" || cmbWAT_DWELL.SelectedValue == "" || cmbWAT_DWELL.SelectedValue == "0")
				objNewInfo.WAT_DWELL_DESC =	"";
			else
				objNewInfo.WAT_DWELL_DESC = this.txtWAT_DWELL_DESC.Text.Trim();

			// Recreational Vehicles Dwellings
			objNewInfo.RECR_VEH	= this.cmbRECR_VEH.SelectedItem.Value;
			if(cmbRECR_VEH.SelectedValue == "Y" || cmbRECR_VEH.SelectedValue == "" || cmbRECR_VEH.SelectedValue == "0")
				objNewInfo.RECR_VEH_DESC =	"";
			else
				objNewInfo.RECR_VEH_DESC = this.txtRECR_VEH_DESC.Text.Trim();

			// Auto/ Cycle/trucks
			objNewInfo.AUTO_CYCL_TRUCKS	= this.cmbAUTO_CYCL_TRUCKS.SelectedItem.Value;
			if(cmbAUTO_CYCL_TRUCKS.SelectedValue == "Y" || cmbAUTO_CYCL_TRUCKS.SelectedValue == "" || cmbAUTO_CYCL_TRUCKS.SelectedValue == "0")
				objNewInfo.AUTO_CYCL_TRUCKS_DESC =	"";
			else
				objNewInfo.AUTO_CYCL_TRUCKS_DESC = this.txtAUTO_CYCL_TRUCKS_DESC.Text.Trim();

			// Applicant Understand Coverage Details
			objNewInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED	= this.cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED.SelectedItem.Value;
			if(cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED.SelectedValue == "Y" || cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED.SelectedValue == "")
				objNewInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC =	"";
			else
				objNewInfo.APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC = this.txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC.Text.Trim();
		
			// Office Premises , Rental Dwellings
			if(cmbOFFICE_PREMISES.SelectedItem!=null && cmbOFFICE_PREMISES.SelectedItem.Value!="")
				objNewInfo.OFFICE_PREMISES = int.Parse(cmbOFFICE_PREMISES.SelectedItem.Value);

			if(cmbRENTAL_DWELLINGS_UNIT.SelectedItem!=null && cmbRENTAL_DWELLINGS_UNIT.SelectedItem.Value!="")
				objNewInfo.RENTAL_DWELLINGS_UNIT = int.Parse(cmbRENTAL_DWELLINGS_UNIT.SelectedItem.Value);
	
			//No of Families 
			objNewInfo.FAMILIES = int.Parse(txtFAMILIES.Text.Trim());

			int retVal;
			try
			{
				if ( hidOldData.Value == "" )
				{
					retVal = objGen.AddPolicyUmbrellaGenInfo(objNewInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
				}
				else
				{
					ClsUmbrellaGenInfo objOldInfo = new ClsUmbrellaGenInfo();

					base.PopulateModelObject(objOldInfo,this.hidOldData.Value);

					retVal = objGen.UpdatePolicyUmbrella(objOldInfo,objNewInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");

				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;
				}

				return -4;
			}

			return 1;

		}
		private void SetWorkFlow()
		{
			//Ravindra Gupta(02/16/2006) Added
			if(base.ScreenId == "280")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
			
			//Ravindra Gupta(02/16/2006) Commented
			/*
			if(base.ScreenId == "79_3")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}*/
		}		
		private void GetSessionValues()
		{
			strPolicyId = base.GetPolicyID();
			strPolicyVersionId = base.GetPolicyVersionID();
			strCustomerID = base.GetCustomerID();
		}
	}
}
