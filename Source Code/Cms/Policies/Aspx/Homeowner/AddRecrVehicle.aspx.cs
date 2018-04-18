/******************************************************************************************
<Author					: -  Pradeep Iyer
<Start Date				: -	 Nov 10, 2005
<End Date				: -	
<Description			: - Add/Edit page for RECREATIONAL_VEHICLES
<Review Date			: - 
<Reviewed By			: - 	

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
using Cms.Model.Policy.HomeOwners;
using System.Resources;
using Cms.CmsWeb;
using Ajax;

namespace Cms.Policies.Aspx.Homeowner
{
	/// <summary>
	/// Summary description for AddRecrVehicle.
	/// </summary>
	public class AddRecrVehicle :  Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblInfo;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCOMPANY_ID_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_ID_NUMBER;
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
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSERIAL;
		protected System.Web.UI.WebControls.Label capSTATE_REGISTERED;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_REGISTERED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_REGISTERED;
		protected System.Web.UI.WebControls.Label capVEHICLE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_TYPE_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_TYPE_NAME;
		protected System.Web.UI.WebControls.Label capMANUFACTURER_DESC;
		protected System.Web.UI.WebControls.TextBox txtMANUFACTURER_DESC;
		protected System.Web.UI.WebControls.Label lblMANUFACTURER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMANUFACTURER_DESC;
		protected System.Web.UI.WebControls.Label capHORSE_POWER;
		protected System.Web.UI.WebControls.TextBox txtHORSE_POWER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHORSE_POWER;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		protected System.Web.UI.WebControls.CustomValidator csvREMARKS;
		protected System.Web.UI.WebControls.Label capPRIOR_LOSSES;
		//Added by Manoj Rathore on 29th Mar 2007
		protected System.Web.UI.WebControls.Label capUNIT_RENTED;
		protected System.Web.UI.WebControls.Label capUNIT_OWNED_DEALERS;
		protected System.Web.UI.WebControls.Label capYOUTHFUL_OPERATOR_UNDER_25;
		protected System.Web.UI.WebControls.DropDownList cmbUNIT_RENTED;
		protected System.Web.UI.WebControls.DropDownList cmbUNIT_OWNED_DEALERS;
		protected System.Web.UI.WebControls.DropDownList cmbYOUTHFUL_OPERATOR_UNDER_25;
		//***************************************
		protected System.Web.UI.WebControls.DropDownList cmbPRIOR_LOSSES;
		protected System.Web.UI.WebControls.Label capIS_UNIT_REG_IN_OTHER_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbIS_UNIT_REG_IN_OTHER_STATE;
		protected System.Web.UI.WebControls.Label capRISK_DECL_BY_OTHER_COMP;
		protected System.Web.UI.WebControls.DropDownList cmbRISK_DECL_BY_OTHER_COMP;
		protected System.Web.UI.WebControls.Label capUSED_IN_RACE_SPEED;
		protected System.Web.UI.WebControls.DropDownList cmbUSED_IN_RACE_SPEED;
		protected System.Web.UI.WebControls.Label capVEHICLE_MODIFIED;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_MODIFIED;
		protected System.Web.UI.WebControls.Label capDESC_RISK_DECL_BY_OTHER_COMP;
		protected System.Web.UI.WebControls.TextBox txtDESC_RISK_DECL_BY_OTHER_COMP;
		protected System.Web.UI.WebControls.CustomValidator csvDESC_RISK_DECL_BY_OTHER_COMP;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMessage;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.WebControls.CustomValidator csvDEDUCTIBLE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_INCEPTION_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOOKUP_UNIQUE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_TYPE_NAME;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.TextBox txtINSURING_VALUE;
		protected System.Web.UI.WebControls.Label capDEDUCTIBLE;
		protected System.Web.UI.WebControls.DropDownList cmbDEDUCTIBLE;
		protected System.Web.UI.WebControls.Label capINSURING_VALUE;
        //Added For Itrack Issue #6710
		protected System.Web.UI.WebControls.Label capLIABILITY;
		protected System.Web.UI.WebControls.TextBox txtLIABILITY_LIMIT;
		protected System.Web.UI.WebControls.CheckBox chkLIABILITY;
		protected System.Web.UI.WebControls.Label capMEDICAL_PAYMENTS;
		protected System.Web.UI.WebControls.CheckBox chkMEDICAL_PAYMENTS;
		protected System.Web.UI.WebControls.TextBox txtMEDICAL_PAYMENTS_LIMIT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidliability_limit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMedical_limit;
		protected System.Web.UI.WebControls.CheckBox chkPHYSICAL_DAMAGE;
		protected System.Web.UI.WebControls.RangeValidator rngPHYSICAL_DAMAGE;
        //Add till here.
		string strCalledFrom = "";
		protected System.Web.UI.WebControls.RegularExpressionValidator revHORSE_POWER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revINSURING_VALUE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeductibleXml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCompany;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEDUCTIBLE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURING_VALUE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_DATE;
		public string lob="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
				hidCalledFrom.Value=Request.QueryString["CalledFrom"].ToString();
			}
		
			//Setting screen Id.	
			switch (strCalledFrom.ToUpper()) 
			{
				case "HOME":
					base.ScreenId="243_0";
					lob="HREC";
					break;
				case "RENTAL":
					base.ScreenId="243_0";
					lob="RREC";
					break;
				default:
					base.ScreenId="243_0";
					break;
			}
			#endregion

			btnReset.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;	
				
			btnSave.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;	
			
			btnCopy.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write ;
			btnCopy.PermissionString				=	gstrSecurityXML;
			
			btnDelete.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString				=	gstrSecurityXML;
			
			btnActivateDeactivate.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;	

			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			

			//Reset button
				txtINSURING_VALUE.Attributes.Add ("onBlur","this.value=formatCurrency(this.value);");
			//txtINSURING_VALUE.Attributes.Add ("onBlur","this.value=formatCurrencyWithCents(this.value);");
			//Comment and added For Itrack Issue #6710.
			//txtINSURING_VALUE.Attributes.Add("onBlur","javascript:this.value = formatCurrencyWithCents(this.value);");
			//btnSave.Attributes.Add("onClick","javascript:this.value = formatCurrencyWithCents(this.value);");	
			//txtINSURING_VALUE.Attributes.Add("onkeypress","javascript:if (event.keyCode == 13){this.value = formatCurrencyWithCents(this.value)};");	
			
			if ( !Page.IsPostBack )
			{
				trMessage.Visible = false;
				Utility.RegisterTypeForAjax(typeof (AddRecrVehicle));
                //Added For Itrack Issue #6710 
				hidCustomerID.Value	 =  GetCustomerID();
				hidPolicyID.Value = GetPolicyID();
				hidPolicyVersionID.Value = GetPolicyVersionID();
                GetCovSecEF();
				//Add till here......
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
				
				this.txtREMARKS.Attributes.Add("OnKeyPress","javascript:MaxLength(this,100);");
				this.txtMANUFACTURER_DESC.Attributes.Add("OnKeyPress","javascript:MaxLength(this,100);");
				this.txtDESC_RISK_DECL_BY_OTHER_COMP.Attributes.Add("OnKeyPress","javascript:MaxLength(this,100);");

				

				string url = ClsCommon.GetLookupURL();
				//string strCopyWindow = rootPath + 

				// Commented by mohit on 7/11/2005
				//imgSelect.Attributes.Add("onclick","javascript:OpenLookup('" + url + "','LOOKUP_UNIQUE_ID','LOOKUP_VALUE_DESC','hidLOOKUP_UNIQUE_ID','txtVEHICLE_TYPE_NAME','LookupTable','Vehicle Types',\"@LOOKUP_NAME='CBDCD'\")");				
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
					int intCompanyNumber = -1;
					DataTable dtCompanyNumber = ClsHomeRecrVehicles.GetNextPolCompanyIDNumber
						(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(this.hidPolicyID.Value),
						Convert.ToInt32(this.hidPolicyVersionID.Value)
						);

					if(dtCompanyNumber!=null && dtCompanyNumber.Rows.Count>0)
					{
						intCompanyNumber = int.Parse(dtCompanyNumber.Rows[0]["NEXT_COMPANY_ID"].ToString());
						hidAPP_INCEPTION_DATE.Value = dtCompanyNumber.Rows[0]["APP_INCEPTION_DATE"].ToString().Trim();
					}
					
					if ( intCompanyNumber == -1 )
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"534");
						return;
					}
					else
					{
						this.txtCOMPANY_ID_NUMBER.Text = intCompanyNumber.ToString();
						hidCompany.Value =this.txtCOMPANY_ID_NUMBER.Text;
					}
				}
				#region Set Workflow Control
				SetWorkflow();
				#endregion
			}
			//Added For Itrack Issue #6710.
			if(chkLIABILITY.Checked == true)
			{
				txtLIABILITY_LIMIT.Attributes.Add("style","display:inline;");
			}
			else
			{
				txtLIABILITY_LIMIT.Attributes.Add("style","display:none;");
			}
			
			if(chkMEDICAL_PAYMENTS.Checked == true)
			{
				txtMEDICAL_PAYMENTS_LIMIT.Attributes.Add("style","display:inline;"); 
			}
			else
			{
				txtMEDICAL_PAYMENTS_LIMIT.Attributes.Add("style","display:none;"); 
			}

			if(chkPHYSICAL_DAMAGE.Checked == true)
			{
				txtINSURING_VALUE.Attributes.Add("style","display:inline;"); 
			}
			else
			{
				txtINSURING_VALUE.Attributes.Add("style","display:none;"); 
			}
		   //Add till here...   
		
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
			this.cmbVEHICLE_TYPE_NAME.SelectedIndexChanged += new System.EventHandler(this.cmbVEHICLE_TYPE_NAME_SelectedIndexChanged);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		private void cmbVEHICLE_TYPE_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cmbVEHICLE_TYPE_NAME.SelectedIndex >0)
			{
				SetDeductibles(cmbVEHICLE_TYPE_NAME.SelectedValue);
			}
		}
        
		private void SetDeductibles(string strVeh_Type)
		{
			string strFilePath=System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/coverages/RVDeductibles.xml");
			string strFilter="VehicleCode=" + strVeh_Type;
			DataSet dsDed = new DataSet();
			//Create New Table For Formating The Value In Dropdown
			DataTable dt=new DataTable() ;
			dt.Columns.Add("Value",typeof(int));
			dt.Columns.Add("VehicleCode",typeof(string));
		
			dsDed.ReadXml(strFilePath);
			//Copy data in new table
			foreach(DataRow dr in dsDed.Tables[0].Rows)
			{
				DataRow drNew=	dt.NewRow();
				drNew["Value"]=int.Parse(dr["Value"].ToString());
				drNew["VehicleCode"]=dr["VehicleCode"].ToString();
				dt.Rows.Add(drNew);
			}
			DataView dv = dt.DefaultView;
			dv.RowFilter  = strFilter;
			cmbDEDUCTIBLE.ClearSelection();
			cmbDEDUCTIBLE.DataSource=dv;
			//Format the value
			cmbDEDUCTIBLE.DataTextFormatString="{0:,#,###}";
			cmbDEDUCTIBLE.DataTextField  ="Value";
			cmbDEDUCTIBLE.DataValueField ="Value";
			cmbDEDUCTIBLE .DataBind();
			cmbDEDUCTIBLE.Items.Insert(0,"");

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

			
			// Added by mohit		
			cmbVEHICLE_TYPE_NAME.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CBDCD2");
			cmbVEHICLE_TYPE_NAME.DataTextField = "LookupDesc";
			cmbVEHICLE_TYPE_NAME.DataValueField = "LookupID";
			cmbVEHICLE_TYPE_NAME.DataBind();
			cmbVEHICLE_TYPE_NAME.Items.Insert(0,new ListItem("",""));
			cmbVEHICLE_TYPE_NAME.SelectedIndex=0;

		}

		/// <summary>
		/// Sets the text of the labels
		/// </summary>
		private void SetCaptions()
		{
			ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Homeowner.AddRecrVehicle",System.Reflection.Assembly.GetExecutingAssembly());

			capCOMPANY_ID_NUMBER.Text =	objResourceMgr.GetString("txtCOMPANY_ID_NUMBER");
			capYEAR.Text =	objResourceMgr.GetString("txtYEAR");
			capMAKE.Text =	objResourceMgr.GetString("txtMAKE");
			capMODEL.Text =	objResourceMgr.GetString("txtMODEL");
			capSERIAL.Text = objResourceMgr.GetString("txtSERIAL");
			capSTATE_REGISTERED.Text =	objResourceMgr.GetString("txtSTATE_REGISTERED");
			capVEHICLE_TYPE.Text =	objResourceMgr.GetString("txtVEHICLE_TYPE");
			capMANUFACTURER_DESC.Text =	objResourceMgr.GetString("txtMANUFACTURER_DESC");
			capHORSE_POWER.Text =	objResourceMgr.GetString("txtHORSE_POWER");
			// Commented by mohit on 7/11/2005.
			//capDISPLACEMENT.Text = 	objResourceMgr.GetString("txtDISPLACEMENT");
			capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
			capUSED_IN_RACE_SPEED.Text = objResourceMgr.GetString("cmbUSED_IN_RACE_SPEED");
			capPRIOR_LOSSES.Text =	objResourceMgr.GetString("cmbPRIOR_LOSSES");
			//Added by Mnaoj Rathore on 29th Mar 2007
			capUNIT_RENTED.Text			 =	objResourceMgr.GetString("cmbUNIT_RENTED");
			capUNIT_OWNED_DEALERS.Text	 =	objResourceMgr.GetString("cmbUNIT_OWNED_DEALERS");
			capYOUTHFUL_OPERATOR_UNDER_25.Text =	objResourceMgr.GetString("cmbYOUTHFUL_OPERATOR_UNDER_25");
			//**************************************
			capIS_UNIT_REG_IN_OTHER_STATE.Text = objResourceMgr.GetString("cmbIS_UNIT_REG_IN_OTHER_STATE");
			capRISK_DECL_BY_OTHER_COMP.Text = objResourceMgr.GetString("cmbRISK_DECL_BY_OTHER_COMP");
			capDESC_RISK_DECL_BY_OTHER_COMP.Text =	objResourceMgr.GetString("txtDESC_RISK_DECL_BY_OTHER_COMP");
			capVEHICLE_MODIFIED.Text = objResourceMgr.GetString("cmbVEHICLE_MODIFIED");
			capINSURING_VALUE.Text=objResourceMgr.GetString("txtINSURING_VALUE");
			capDEDUCTIBLE.Text =objResourceMgr.GetString("cmbDEDUCTIBLE");
			//Added For Itrack Issue #6710
			capLIABILITY.Text = objResourceMgr.GetString("chkLIABILITY");
			capMEDICAL_PAYMENTS.Text = objResourceMgr.GetString("chkMEDICAL_PAYMENTS"); 
			//Add till here.....

		}

		/// <summary>
		/// Sets the validator error messages
		/// </summary>
		private void SetValidators()
		{
			//this.rfvDWELLING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			//this.rfvCOMPANY_ID_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			this.rngCOMPANY_ID_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"102");
			
			
			this.rfvYEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"166");
			
			rngYEAR.MaximumValue = DateTime.Now.AddYears(1).Year.ToString();
			//rngYEAR.MinimumValue = "1000";//aAppMinYear  ;
			rngYEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("673");

			this.rfvMAKE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"168");

			// Commented by mohit on 7/11/2005
			//this.rfvtxtVEHICLE_TYPE_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"456");
			// Added by mohit message is to be changed. 
			rfvVEHICLE_TYPE_NAME.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"456");
			
			this.rfvMODEL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"169");
			this.rfvMANUFACTURER_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"457");
			csvREMARKS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"441");
			csvDESC_RISK_DECL_BY_OTHER_COMP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"445");

			// Added by mohit on 7/11/2005.
			rfvSERIAL.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"535");
			rfvSTATE_REGISTERED.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"536");
			rfvHORSE_POWER.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"537");
			this.revHORSE_POWER.ValidationExpression =aRegExpDoublePositiveWithZero;
			this.revHORSE_POWER.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage("G","217");
			revINSURING_VALUE.ValidationExpression=aRegExpDoublePositiveZero;
			revINSURING_VALUE.ErrorMessage      =	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
			rfvDEDUCTIBLE.ErrorMessage			=	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("460");	
			rfvINSURING_VALUE.ErrorMessage		=	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1057");	
			csvDEDUCTIBLE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("860");

		

			// End
		}
		//Added For Itrack Issue #6710.
		public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
		{
			Double result;
			return Double.TryParse(val,NumberStyle,
				System.Globalization.CultureInfo.CurrentCulture,out result);
		}
        //Added For Itrack Issue #6710.
		private void GetCovSecEF()
		{			
			DataSet objCov = new DataSet();
			objCov = BusinessLayer.BlApplication.ClsHomeRecrVehicles.GetSection2CoveragesE_CovergaesF(Convert.ToInt32(hidCustomerID.Value),Convert.ToInt32(hidPolicyID.Value),Convert.ToInt32(hidPolicyVersionID.Value),"POL");
			if(objCov.Tables[0].Rows.Count > 0)
			{		

				hidliability_limit.Value = objCov.Tables[0].Rows[0]["LIMIT_1"].ToString();
				bool check = isNumeric(hidliability_limit.Value, System.Globalization.NumberStyles.None);
				if(check == true)
				{
					string Libality  =  String.Format("{0:n}",Double.Parse(hidliability_limit.Value.ToString()));
					txtLIABILITY_LIMIT.Text =  Libality.Replace(".00",""); 
				}
				else
				{
				     txtLIABILITY_LIMIT.Text = hidliability_limit.Value;
				}

				hidMedical_limit.Value =  objCov.Tables[0].Rows[1]["LIMIT_1"].ToString();
				bool check_Medicallibality = isNumeric(hidMedical_limit.Value, System.Globalization.NumberStyles.None);
				
				if(check_Medicallibality == true)
				{
					string medical =  String.Format("{0:n}",Double.Parse(hidMedical_limit.Value.ToString())); 
					txtMEDICAL_PAYMENTS_LIMIT.Text = medical.Replace(".00",""); 
				}
				else
				{
					txtMEDICAL_PAYMENTS_LIMIT.Text = hidMedical_limit.Value;
				}

				
			   	 
			   	 
			}
		}
		
		/// <summary>
		/// Populates form fields and Stores the XML for the record to be updated
		/// </summary>
		private void LoadData()
		{
			ClsHomeRecrVehicles objVehicle = new ClsHomeRecrVehicles();
			
			DataSet dsVehicles = objVehicle.GetPolRecrVehicleByID(Convert.ToInt32(hidCustomerID.Value),
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

			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					btnCopy.Attributes.Add("onclick","javascript: return OpenPopupWindow('CopyRecrVehicles.aspx?CALLEDFROM=HOME&REC_VEH_ID=" + this.hidREC_VEH_ID.Value + "');");
					break;
				case "Rental":
				case "RENTAL":
					btnCopy.Attributes.Add("onclick","javascript: return OpenPopupWindow('CopyRecrVehicles.aspx?CALLEDFROM=RENTAL&REC_VEH_ID=" + this.hidREC_VEH_ID.Value + "');");
					break;
		
			}
			

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
				// commented by mohit on 7/11/2005
				//this.hidLOOKUP_UNIQUE_ID.Value = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE"]);
				//this.txtVEHICLE_TYPE_NAME.Text =  Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE_NAME"]) ;
				this.cmbVEHICLE_TYPE_NAME.SelectedValue=Convert.ToInt32(dtVehicle.Rows[0]["VEHICLE_TYPE"]).ToString();
				SetDeductibles(dtVehicle.Rows[0]["VEHICLE_TYPE"].ToString());
			}

			txtMANUFACTURER_DESC.Text = Convert.ToString(dtVehicle.Rows[0]["MANUFACTURER_DESC"]);
			txtHORSE_POWER.Text = Convert.ToString(dtVehicle.Rows[0]["HORSE_POWER"]);
			// Commented by mohit on 7/11/2005
			//txtDISPLACEMENT.Text = Convert.ToString(dtVehicle.Rows[0]["DISPLACEMENT"]);
			txtREMARKS.Text = Convert.ToString(dtVehicle.Rows[0]["REMARKS"]);
			
			if ( dtVehicle.Rows[0]["USED_IN_RACE_SPEED"] != System.DBNull.Value )
			{
				listItem = cmbUSED_IN_RACE_SPEED.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["USED_IN_RACE_SPEED"]));
				//cmbUSED_IN_RACE_SPEED.SelectedIndex = cmbUSED_IN_RACE_SPEED.Items.IndexOf(listItem);	
				if(listItem!=null)
					listItem.Selected=true; 
			}

			if ( dtVehicle.Rows[0]["PRIOR_LOSSES"] != System.DBNull.Value )
			{
				listItem = cmbPRIOR_LOSSES.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["PRIOR_LOSSES"]));
				cmbPRIOR_LOSSES.SelectedIndex= cmbPRIOR_LOSSES.Items.IndexOf(listItem);	
			}
			if ( dtVehicle.Rows[0]["UNIT_RENTED"] != System.DBNull.Value )
			{
				listItem = cmbUNIT_RENTED.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["UNIT_RENTED"]).Trim());
				cmbUNIT_RENTED.SelectedIndex= cmbUNIT_RENTED.Items.IndexOf(listItem);	
			}
			if ( dtVehicle.Rows[0]["UNIT_OWNED_DEALERS"] != System.DBNull.Value )
			{
				listItem = cmbUNIT_OWNED_DEALERS.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["UNIT_OWNED_DEALERS"]).Trim());
				cmbUNIT_OWNED_DEALERS.SelectedIndex= cmbUNIT_OWNED_DEALERS.Items.IndexOf(listItem);	
			}
			if ( dtVehicle.Rows[0]["YOUTHFUL_OPERATOR_UNDER_25"] != System.DBNull.Value )
			{
				listItem = cmbYOUTHFUL_OPERATOR_UNDER_25.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["YOUTHFUL_OPERATOR_UNDER_25"]).Trim());
				cmbYOUTHFUL_OPERATOR_UNDER_25.SelectedIndex= cmbYOUTHFUL_OPERATOR_UNDER_25.Items.IndexOf(listItem);	
			}
			//**************************************

			if ( dtVehicle.Rows[0]["IS_UNIT_REG_IN_OTHER_STATE"] != System.DBNull.Value )
			{
				listItem = cmbIS_UNIT_REG_IN_OTHER_STATE.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["IS_UNIT_REG_IN_OTHER_STATE"]));
				cmbIS_UNIT_REG_IN_OTHER_STATE.SelectedIndex= cmbIS_UNIT_REG_IN_OTHER_STATE.Items.IndexOf(listItem);	
			}

			if ( dtVehicle.Rows[0]["RISK_DECL_BY_OTHER_COMP"] != System.DBNull.Value )
			{
				listItem = cmbRISK_DECL_BY_OTHER_COMP.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["RISK_DECL_BY_OTHER_COMP"]));
				cmbRISK_DECL_BY_OTHER_COMP.SelectedIndex = cmbRISK_DECL_BY_OTHER_COMP.Items.IndexOf(listItem);	
			}
			if ( dtVehicle.Rows[0]["INSURING_VALUE"] != System.DBNull.Value )
			{
				//Commented and added For Itrack Issue 6710.
				txtINSURING_VALUE.Text=Convert.ToDecimal(dtVehicle.Rows[0]["INSURING_VALUE"]).ToString("N");
				txtINSURING_VALUE.Text= txtINSURING_VALUE.Text.Substring(0,txtINSURING_VALUE.Text.LastIndexOf("."));
				//txtINSURING_VALUE.Text = Convert.ToDouble(dtVehicle.Rows[0]["INSURING_VALUE"]).ToString();
			}
			else
			{
               txtINSURING_VALUE.Text ="";
			}
			if ( dtVehicle.Rows[0]["DEDUCTIBLE"] != System.DBNull.Value )
			{
				ClsCommon.SelectValueInDDL(cmbDEDUCTIBLE,dtVehicle.Rows[0]["DEDUCTIBLE"].ToString()); 

			}
			if ( dtVehicle.Rows[0]["VEHICLE_MODIFIED"] != System.DBNull.Value )
			{
				listItem = cmbVEHICLE_MODIFIED.Items.FindByValue(Convert.ToString(dtVehicle.Rows[0]["VEHICLE_MODIFIED"]));
				cmbVEHICLE_MODIFIED.SelectedIndex = cmbVEHICLE_MODIFIED.Items.IndexOf(listItem);	
			}

			txtDESC_RISK_DECL_BY_OTHER_COMP.Text = Convert.ToString(dtVehicle.Rows[0]["DESC_RISK_DECL_BY_OTHER_COMP"]);

			if(dtVehicle.Rows[0]["APP_INCEPTION_DATE"]!=null && dtVehicle.Rows[0]["APP_INCEPTION_DATE"].ToString()!="")
				hidAPP_INCEPTION_DATE.Value = dtVehicle.Rows[0]["APP_INCEPTION_DATE"].ToString().Trim();
            //Added For Itrack Issue #6710  
			if((Convert.ToString(dtVehicle.Rows[0]["LIABILITY"]) == "10963"))
			{
				chkLIABILITY.Checked = true; 
				txtLIABILITY_LIMIT.Attributes.Add("style","display:inline;");  
				
			}
			else
			{
				chkLIABILITY.Checked = false;            
				txtLIABILITY_LIMIT.Attributes.Add("style","display:none;");  
			}
			if((Convert.ToString(dtVehicle.Rows[0]["MEDICAL_PAYMENTS"]) == "10963"))
			{
				chkMEDICAL_PAYMENTS.Checked = true; 
				txtMEDICAL_PAYMENTS_LIMIT.Attributes.Add("style","display:inline;"); 
				
			}
			else
			{
				chkMEDICAL_PAYMENTS.Checked = false;
				txtMEDICAL_PAYMENTS_LIMIT.Attributes.Add("style","display:none;");  
			}
			if((Convert.ToString(dtVehicle.Rows[0]["PHYSICAL_DAMAGE"]) == "10963"))
			{
				chkPHYSICAL_DAMAGE.Checked = true; 
				txtINSURING_VALUE.Attributes.Add("style","display:inline;");  
				
			}
			else
			{
				chkPHYSICAL_DAMAGE.Checked = false;            
				txtINSURING_VALUE.Attributes.Add("style","display:none;");  
			}
            //Add tll here....
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
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"524");
				return;
			}
			
		
			//Setting the workflow
			SetWorkflow();

			if ( retVal > 0 )
			{
				LoadData();
				//Showing the endorsement popup window
				base.OpenEndorsementDetails();

				RegisterScript();

				//lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
			}

		}
		
		/// <summary>
		/// Saves the record in the database
		/// </summary>
		/// <returns></returns>
		private int Save()
		{
			//BL class
			ClsHomeRecrVehicles objVehicle = new ClsHomeRecrVehicles();
			
			ClsPolRecrVehiclesInfo objNewInfo = new ClsPolRecrVehiclesInfo();

			objNewInfo.POLICY_ID = Convert.ToInt32(hidPolicyID.Value);
			objNewInfo.POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID.Value);
			objNewInfo.CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);
			if(txtCOMPANY_ID_NUMBER.Text != "")
			{
				objNewInfo.COMPANY_ID_NUMBER = Convert.ToInt32(this.txtCOMPANY_ID_NUMBER.Text.Trim());
			}
			
			objNewInfo.CREATED_BY = Convert.ToInt32(GetUserId());
			objNewInfo.MODIFIED_BY = Convert.ToInt32(GetUserId());

			objNewInfo.DESC_RISK_DECL_BY_OTHER_COMP = this.txtDESC_RISK_DECL_BY_OTHER_COMP.Text.Trim();
			// Commented by mohit on 7/11/2005
			//objNewInfo.DISPLACEMENT = this.txtDISPLACEMENT.Text.Trim();
			objNewInfo.HORSE_POWER = this.txtHORSE_POWER.Text.Trim();

			objNewInfo.IS_UNIT_REG_IN_OTHER_STATE = this.cmbIS_UNIT_REG_IN_OTHER_STATE.SelectedItem.Value;
			objNewInfo.MAKE = this.txtMAKE.Text.Trim();
			if (cmbVEHICLE_TYPE_NAME.SelectedValue == "11434")
			{
				objNewInfo.MANUFACTURER_DESC = this.txtMANUFACTURER_DESC.Text.Trim();
			}
			else
			{
				objNewInfo.MANUFACTURER_DESC="";
			}
			objNewInfo.MODEL = this.txtMODEL.Text.Trim();
			
			objNewInfo.PRIOR_LOSSES = this.cmbPRIOR_LOSSES.SelectedItem.Value;
			//Added by Manoj Rathore on 29th Mar 2007
			objNewInfo.UNIT_RENTED = this.cmbUNIT_RENTED.SelectedItem.Value;
			objNewInfo.UNIT_OWNED_DEALERS = this.cmbUNIT_OWNED_DEALERS.SelectedItem.Value;
			objNewInfo.YOUTHFUL_OPERATOR_UNDER_25 = this.cmbYOUTHFUL_OPERATOR_UNDER_25.SelectedItem.Value;
			//***************************************
			objNewInfo.REMARKS = this.txtREMARKS.Text.Trim();
			objNewInfo.RISK_DECL_BY_OTHER_COMP = this.cmbRISK_DECL_BY_OTHER_COMP.SelectedItem.Value;
			
			objNewInfo.SERIAL = this.txtSERIAL.Text.Trim();
			if(cmbSTATE_REGISTERED.SelectedItem.Text != "")
				objNewInfo.STATE_REGISTERED = Convert.ToInt32(this.cmbSTATE_REGISTERED.SelectedItem.Value);

			objNewInfo.USED_IN_RACE_SPEED = this.cmbUSED_IN_RACE_SPEED.SelectedItem.Value;

			objNewInfo.VEHICLE_MODIFIED = this.cmbVEHICLE_MODIFIED.SelectedItem.Value;
			

			// Commented by mohit.	

			//			if ( this.hidLOOKUP_UNIQUE_ID.Value.Trim() != "" )
			//			{
			//				objNewInfo.VEHICLE_TYPE = Convert.ToInt32(this.hidLOOKUP_UNIQUE_ID.Value.Trim());
			//			}
			
             
			if ( cmbVEHICLE_TYPE_NAME.SelectedValue != null )
			{
				objNewInfo.VEHICLE_TYPE=Convert.ToInt32(cmbVEHICLE_TYPE_NAME.SelectedValue);
			}

			if(txtINSURING_VALUE.Text.Trim() != "")
			{
				//Changes For Itrack Issue #6710.
				objNewInfo.INSURING_VALUE=Convert.ToDouble(txtINSURING_VALUE.Text);
			}

			else
			{
				objNewInfo.INSURING_VALUE=0;
			}

			if(cmbDEDUCTIBLE.SelectedIndex >0)
			{
				objNewInfo.DEDUCTIBLE=Convert.ToDouble(cmbDEDUCTIBLE.SelectedItem.Value);
			}
			else
			{
				objNewInfo.DEDUCTIBLE=0;
			}
			
			objNewInfo.YEAR = Convert.ToInt32(this.txtYEAR.Text.Trim());
            //Added For Itrack Issue #6710. 
			if(chkLIABILITY.Checked == true)
			{
				objNewInfo.LIABILITY = "10963";
			}
			else
			{
				objNewInfo.LIABILITY = "10964";				
			}
			if(chkMEDICAL_PAYMENTS.Checked == true)
			{
				objNewInfo.MEDICAL_PAYMENTS  = "10963";
			}
			else
			{
				objNewInfo.MEDICAL_PAYMENTS  = "10964";				
			}
			
			if(chkPHYSICAL_DAMAGE.Checked == true)
			{
				objNewInfo.PHYSICAL_DAMAGE   = "10963";
			}
			else
			{
				objNewInfo.PHYSICAL_DAMAGE  = "10964";				
			}
			//Add till here.....			
			int intRetVal;

			try
			{
				//Add new
				if ( this.hidREC_VEH_ID.Value == "0" )
				{
					int recVehID = objVehicle.AddPolicyRecrVeh(objNewInfo);
					
					if ( recVehID == -1 ) 
					{
						return -1;
					}
					
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
					intRetVal = recVehID;
					if (intRetVal > 0)
					{
						hidFormSaved.Value="1";
					}
					else
					{
						hidFormSaved.Value="2";
					}
					this.hidREC_VEH_ID.Value = intRetVal.ToString();

				}
				else
				{
					objNewInfo.REC_VEH_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);

					//Populate old object
					ClsPolRecrVehiclesInfo objOldInfo = new ClsPolRecrVehiclesInfo();
					base.PopulateModelObject(objOldInfo,hidOldData.Value);

					intRetVal = objVehicle.UpdateRecrVeh(objOldInfo,objNewInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
					if (intRetVal > 0)
					{
						hidFormSaved.Value="1";
					}
					else
					{
						hidFormSaved.Value="2";
					}
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
				int retVal = ClsHomeRecrVehicles.ActivateDeactivatePolRecrVeh(Convert.ToInt32(hidCustomerID.Value),
					Convert.ToInt32(this.hidPolicyID.Value),
					Convert.ToInt32(this.hidPolicyVersionID.Value),
					Convert.ToInt32(this.hidREC_VEH_ID.Value),
					strAction);
				if (retVal > 0)
				{
					hidFormSaved.Value="1";

					//Showing the endorsement popup window
					base.OpenEndorsementDetails();
				}
				else
				{
					hidFormSaved.Value="2";
				}
			
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				
				return ;
			}

			lblMessage.Visible = true;

			lblMessage.Text = strMessage;		

			LoadData();

		}
		
		/// <summary>
		/// Deletes the current record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			//BL class
			ClsHomeRecrVehicles objVehicle = new ClsHomeRecrVehicles();
			
			lblMessage.Visible = true;
			
			int intRetVal = 0;

			try
			{
				intRetVal = objVehicle.DeletePolRecrVeh(Convert.ToInt32(hidCustomerID.Value),
					Convert.ToInt32(this.hidPolicyID.Value),
					Convert.ToInt32(this.hidPolicyVersionID.Value),
					Convert.ToInt32(this.hidREC_VEH_ID.Value)
					);
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
				base.OpenEndorsementDetails();
				
				this.hidOldData.Value = "";

				//Added by Charles on 28-Jul-09 for Itrack 6176 (Note# 1)
				this.hidFormSaved.Value = "5";	

				//Commented by Charles on 28-Jul-09 for Itrack 6176 (Note# 1)
				//if (!Page.IsStartupScriptRegistered("Refresh"))
				//{
				//	//string keyValue = this.hidREC_VEH_ID.Value;

				//	string strCode = @"<script>RefreshWebGrid(1,1)</script>";

				//	Page.RegisterStartupScript("Refresh",strCode);

				//}
			}

		}
	
		private void btnReset_Click(object sender, System.EventArgs e)
		{
			LoadData();
		}

		private void SetWorkflow()
		{
	
			if(base.ScreenId	==	"243_0" || base.ScreenId == "162_0" || base.ScreenId == "263")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("APP_ID",hidPolicyID.Value);
				myWorkFlow.AddKeyValue("APP_VERSION_ID",hidPolicyVersionID.Value);
				if ( this.hidREC_VEH_ID.Value == "0" )
				{
					myWorkFlow.AddKeyValue("REC_VEH_ID",hidREC_VEH_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
		
	}
}
