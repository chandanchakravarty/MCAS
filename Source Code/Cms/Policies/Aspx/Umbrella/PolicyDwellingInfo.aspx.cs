/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-21-2006
	<End Date				: ->
	<Description			: -> Page to add umbrella Dwelling Info(Policy)
	<Review Date			: ->
	<Reviewed By			: ->
	
	Modification History
******************************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.Umbrella ;


namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyDwellingInfo.
	/// </summary>
	public class PolicyDwellingInfo :Cms.Policies.policiesbase 
	{
		
		#region PageControls Declaration
		protected System.Web.UI.WebControls.Repeater Repeater1;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capDWELLING_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtDWELLING_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDWELLING_NUMBER;
		protected System.Web.UI.WebControls.RangeValidator rngDWELLING_NUMBER;
		protected System.Web.UI.WebControls.Label capLOCATION_ID;
		protected System.Web.UI.WebControls.Label lblLOCATION_ID;
		protected System.Web.UI.WebControls.Label capYEAR_BUILT;
		protected System.Web.UI.WebControls.TextBox txtYEAR_BUILT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR_BUILT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEAR_BUILT;
		protected System.Web.UI.WebControls.CustomValidator csvYEAR_BUILT;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR_BUILT;
		protected System.Web.UI.WebControls.Label capPURCHASE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtPURCHASE_YEAR;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPURCHASE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvPurchase_YEAR;
		protected System.Web.UI.WebControls.RangeValidator rngPURCHASE_YEAR;
		protected System.Web.UI.WebControls.Label capPURCHASE_PRICE;
		protected System.Web.UI.WebControls.TextBox txtPURCHASE_PRICE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPURCHASE_PRICE;
		protected System.Web.UI.WebControls.Label capMARKET_VALUE;
		protected System.Web.UI.WebControls.TextBox txtMARKET_VALUE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMARKET_VALUE;
		protected System.Web.UI.WebControls.CustomValidator csvMARKET_VALUE;
		protected System.Web.UI.WebControls.Label capREPLACEMENT_COST;
		protected System.Web.UI.WebControls.TextBox txtREPLACEMENT_COST;
		protected System.Web.UI.WebControls.CompareValidator cmpREPLACEMENT_COST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREPLACEMENT_COST;
		protected System.Web.UI.WebControls.Label capREPAIR_COST;
		protected System.Web.UI.WebControls.TextBox txtREPAIR_COST;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREPAIR_COST;
		protected System.Web.UI.WebControls.Label capBUILDING_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbBUILDING_TYPE;
		protected System.Web.UI.WebControls.Label capOCCUPANCY;
		protected System.Web.UI.WebControls.DropDownList cmbOCCUPANCY;
		protected System.Web.UI.WebControls.Label capNEED_OF_UNITS;
		protected System.Web.UI.WebControls.TextBox txtNEED_OF_UNITS;
		protected System.Web.UI.WebControls.RangeValidator rngNEED_OF_UNITS;
		protected System.Web.UI.WebControls.Label capNEIGHBOURS_VISIBLE;
		protected System.Web.UI.WebControls.DropDownList cmbNEIGHBOURS_VISIBLE;
		protected System.Web.UI.WebControls.Label capOCCUPIED_DAILY;
		protected System.Web.UI.WebControls.DropDownList cmbOCCUPIED_DAILY;
		protected System.Web.UI.WebControls.Label capNO_WEEKS_RENTED;
		protected System.Web.UI.WebControls.TextBox txtNO_WEEKS_RENTED;
		protected System.Web.UI.WebControls.RangeValidator rngNO_WEEKS_RENTED;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDWELLING_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPercent;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOCATION_ID;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		#endregion

		#region Private variables declaration
		
		private int intDwellingID;
		private string strCustomInfo;


		#endregion

		#region PageLoad Event
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId ="274_3";
			btnReset.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;	
				
			btnSave.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;	
								
			hidCustomerID.Value	 =  GetCustomerID();
			hidPolicyID.Value = GetPolicyID ();
			hidPolicyVersionID.Value = GetPolicyVersionID ();
			hidLOCATION_ID.Value = Request.Params["LOCATION_ID"];

			if ( !Page.IsPostBack )
			{

				if ( hidCustomerID.Value == "" ||
					hidPolicyID .Value == "" || 
					hidPolicyVersionID .Value == "" 
					)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","118");
					return;
				}
							    
				AddAttributes();
				SetValidators();
				
				int intCustomerID = Convert.ToInt32(hidCustomerID.Value);
				int intPolicyID = Convert.ToInt32(hidPolicyID .Value);
				int intPolicyVersionID = Convert.ToInt32(hidPolicyVersionID .Value);
				int intLocationID=Convert.ToInt32 (Request.Params["LOCATION_ID1"]);
				
				intDwellingID=Convert.ToInt32(ClsUmbrellaDwelling.GetPolicyDwellingID(intCustomerID,intPolicyID,intPolicyVersionID ,intLocationID));
				hidDWELLING_ID.Value =intDwellingID.ToString ();							
				
				LoadDropdowns();
				SetLocationLabel();
								
				//Dwelling already exists for this location
				if ( intDwellingID != -1 )
				{
					LoadData();
									
				}
					//No dwelling is there -Add new
				else
				{
					trError.Visible = false;
					
					//Set focus to control
					this.SetFocus("txtDWELLING_NUMBER");

					//Get the next dwelliing number
					int nextDwellingNumber = ClsUmbrellaDwelling.GetNextPolicyDwellingNumber (intCustomerID,intPolicyID,intPolicyVersionID);
					if ( nextDwellingNumber == -1 )
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"18");

						return;
					}

					this.txtDWELLING_NUMBER.Text = nextDwellingNumber.ToString();
				}
				SetWorkflow();
								
				hidCheckDelete.Value = "0";
				hidPercent.Value = "100";
					
				//Set the text in the labels
				SetCaptions();
				
			}
		}
		#endregion

		#region  SetWorkflow Function 
		
		private void SetWorkflow()
		{

			if(base.ScreenId == "274_3")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule ="POL";
				if(hidDWELLING_ID.Value!="0" && hidDWELLING_ID.Value.Trim() != "")
				{
					myWorkFlow.AddKeyValue("DWELLING_ID",hidDWELLING_ID.Value);					
					myWorkFlow.AddKeyValue("REMARKS","isnull(REMARKS,0)");
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}

			
		}
		#endregion

		#region SetLocationLabel Function
		private void SetLocationLabel()
		{		
			
			if(this.hidLOCATION_ID.Value.Trim ()!="")
			{
				ClsUmbrellaDwelling objDwelling = new ClsUmbrellaDwelling();
				DataTable dtLoc = objDwelling.GetPolicyLocationDetails(
					Convert.ToInt32(this.hidLOCATION_ID.Value.Trim ()) );
				this.lblLOCATION_ID.Text =dtLoc.Rows[0][0].ToString ();
			}
			
		}
		#endregion


		#region ShowMessage Function
		private void ShowMessage(string strMessage)
		{
			lblError.Text = strMessage;
			trError.Visible = true;
		}
		#endregion

		#region AdAttributes Function 
		private void AddAttributes()
		{
			//Reset button
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
				
			//Attributes for amount fields////			
			this.txtMARKET_VALUE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtPURCHASE_PRICE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtREPLACEMENT_COST.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtREPAIR_COST.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			
			/////
		}
		#endregion

		#region SetCaptions Function
		/// <summary>
		/// Sets the text of the labels
		/// </summary>
		private void SetCaptions()
		{
			System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyDwellingInfo",System.Reflection.Assembly.GetExecutingAssembly());

			capDWELLING_NUMBER.Text	=	objResourceMgr.GetString("txtDWELLING_NUMBER");
			capLOCATION_ID.Text	= objResourceMgr.GetString("lblLOCATION_ID");
			capYEAR_BUILT.Text = objResourceMgr.GetString("txtYEAR_BUILT");
			capPURCHASE_YEAR.Text	= objResourceMgr.GetString("txtPURCHASE_YEAR");
			capPURCHASE_PRICE.Text = objResourceMgr.GetString("txtPURCHASE_PRICE");
			capMARKET_VALUE.Text = objResourceMgr.GetString("txtMARKET_VALUE");
			capBUILDING_TYPE.Text =	objResourceMgr.GetString("cmbBUILDING_TYPE");
			capOCCUPANCY.Text =	objResourceMgr.GetString("cmbOCCUPANCY");
			capREPAIR_COST.Text =	objResourceMgr.GetString("txtREPAIR_COST");
			capNEIGHBOURS_VISIBLE.Text = objResourceMgr.GetString("cmbNEIGHBOURS_VISIBLE");
			capOCCUPIED_DAILY.Text	= objResourceMgr.GetString("cmbOCCUPIED_DAILY");
			capNO_WEEKS_RENTED.Text	= objResourceMgr.GetString("txtNO_WEEKS_RENTED");
																									 
			capREPLACEMENT_COST.Text =	objResourceMgr.GetString("txtREPLACEMENT_COST");
			
		}
		#endregion

		#region LoadDropdowns Function
		/// <summary>
		/// Load the various dropdowns in the page
		/// </summary>
		private void LoadDropdowns()
		{
			//Building type
			Cms.BusinessLayer.BlCommon.ClsCommon .BindLookupDDL(this.cmbBUILDING_TYPE,"HBLDTY",null);

			//Occupancy
			Cms.BusinessLayer.BlCommon.ClsCommon .BindLookupDDL(this.cmbOCCUPANCY,"OCCUPA",null);
					
		}
		#endregion

		#region SetValidators Function
		/// <summary>
		/// Sets the validator error messages
		/// </summary>
		private void SetValidators()
		{
			//Set max year for validators
			this.rngYEAR_BUILT.MaximumValue = DateTime.Now.Year.ToString();
			this.rfvYEAR_BUILT.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"12");
			this.revYEAR_BUILT.ValidationExpression            =  aRegExpInteger;
			this.revYEAR_BUILT.ErrorMessage	   = Cms.CmsWeb.ClsMessages.GetMessage("G","516");
			this.rngYEAR_BUILT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("677");
			
			this.rngPURCHASE_YEAR.MaximumValue =DateTime.Now.Year .ToString ();
			
			this.rfvDWELLING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"1");
			this.rngDWELLING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"9");
			this.rfvREPLACEMENT_COST.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"13");
			this.cmpREPLACEMENT_COST.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.revMARKET_VALUE.ValidationExpression = aRegExpDoublePositiveZero;
			this.revMARKET_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.csvMARKET_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"22");			
			this.revPURCHASE_YEAR.ValidationExpression   =  aRegExpInteger;
			this.revREPAIR_COST.ValidationExpression =aRegExpDoublePositiveZero;
			this.revREPAIR_COST.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.revPURCHASE_PRICE.ValidationExpression = aRegExpDoublePositiveZero;
			this.revPURCHASE_PRICE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.revPURCHASE_YEAR.ErrorMessage	   = Cms.CmsWeb.ClsMessages.GetMessage("G","516");
			this.rngPURCHASE_YEAR.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("677");
			this.rngNO_WEEKS_RENTED.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"7");
			this.rngNEED_OF_UNITS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"11");
			
		}
		#endregion


		#region Private Save Function
		/// <summary>
		/// Saves the record in the database
		/// </summary>	
		/// <returns>-1 for dup Location
		///			 -2 for dup Client dwelling number
		/// </returns>
		private int Save()
		{
			ClsDwellingInfo   objNewInfo = new  ClsDwellingInfo ();
			ClsUmbrellaDwelling objDwelling = new ClsUmbrellaDwelling (); 

			objNewInfo.DWELLING_NUMBER = Convert.ToInt32(this.txtDWELLING_NUMBER.Text.Trim());
			objNewInfo.POLICY_ID = Convert.ToInt32(hidPolicyID .Value); 
			objNewInfo.POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID .Value);
			objNewInfo.CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);

			if ( this.cmbBUILDING_TYPE.SelectedItem.Value != "" )
			{
				objNewInfo.BUILDING_TYPE = Convert.ToInt32(this.cmbBUILDING_TYPE.SelectedItem.Value);
			}
			
			
			objNewInfo.LOCATION_ID= Convert.ToInt32(this.hidLOCATION_ID.Value);				
			
			
			if( this.txtMARKET_VALUE.Text.Trim() != "" )
			{
				objNewInfo.MARKET_VALUE = Convert.ToDouble(this.txtMARKET_VALUE.Text.Trim());
			}
			
			if ( txtNEED_OF_UNITS.Text.Trim() != "" )
			{
				objNewInfo.NEED_OF_UNITS = Convert.ToInt32(txtNEED_OF_UNITS.Text.Trim());
			}


			if ( this.cmbNEIGHBOURS_VISIBLE.SelectedItem.Value != "" )
			{
				objNewInfo.NEIGHBOURS_VISIBLE = this.cmbNEIGHBOURS_VISIBLE.SelectedItem.Value;
			}
			
			if ( this.txtNO_WEEKS_RENTED.Text.Trim() != "" )
			{
				objNewInfo.NO_WEEKS_RENTED = Convert.ToInt32(this.txtNO_WEEKS_RENTED.Text.Trim());
			}
			
			if ( this.cmbOCCUPIED_DAILY.SelectedItem.Value!= "" )
			{
				objNewInfo.OCCUPIED_DAILY = this.cmbOCCUPIED_DAILY.SelectedItem.Value;			
			}

			if ( this.cmbOCCUPANCY.SelectedItem.Value!= "" )
			{
				objNewInfo.OCCUPANCY = Convert.ToInt32(this.cmbOCCUPANCY.SelectedItem.Value);			
			}

			if ( this.txtPURCHASE_PRICE.Text.Trim()!= "" )
			{
				objNewInfo.PURCHASE_PRICE = Convert.ToDouble(this.txtPURCHASE_PRICE.Text.Trim());
			}
			
			if ( this.txtPURCHASE_YEAR.Text.Trim() != "" )
			{
				objNewInfo.PURCHASE_YEAR = Convert.ToInt32(this.txtPURCHASE_YEAR.Text.Trim());
			}
			
			if ( this.txtYEAR_BUILT.Text.Trim()!= "" )
			{
				objNewInfo.YEAR_BUILT = Convert.ToInt32(this.txtYEAR_BUILT.Text.Trim());
			}
          
			if ( this.txtREPLACEMENT_COST.Text.Trim() != "" )
			{
				objNewInfo.REPLACEMENT_COST = Convert.ToDouble(this.txtREPLACEMENT_COST.Text.Trim());
			}
			
			

			strCustomInfo=";Location = " + this.lblLOCATION_ID.Text;
			
			int intRetVal = 0;

			try
			{
				//Add new
				if ( hidDWELLING_ID.Value == "0" || hidDWELLING_ID.Value == "-1" )
				{

					objNewInfo.CREATED_BY = Convert.ToInt32(GetUserId());
					objNewInfo.IS_ACTIVE="Y";
					
					int intDwellingID = objDwelling.AddPolicy(objNewInfo,strCustomInfo);
					
					intRetVal = intDwellingID;

					if ( intRetVal > 0 )
					{
						hidDWELLING_ID.Value = intDwellingID.ToString();
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
						hidIS_ACTIVE.Value = "Y";
						base.OpenEndorsementDetails();
						SetWorkflow();  
					}

				}
				else
				{
					objNewInfo.MODIFIED_BY = Convert.ToInt32(GetUserId());
					objNewInfo.DWELLING_ID = Convert.ToInt32(hidDWELLING_ID.Value);
					
					objNewInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Populate old object
					ClsDwellingInfo   objOldInfo = new ClsDwellingInfo();
					base.PopulateModelObject(objOldInfo,hidOldData.Value);

					intRetVal = objDwelling.UpdatePolicy(objOldInfo,objNewInfo,strCustomInfo);
					if(intRetVal > 0)
					{					
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
						base.OpenEndorsementDetails();
						SetWorkflow();  
					}
									
					
				}
			}
			catch(Exception ex)
			{
				lblMessage.Visible = true;
				lblMessage.Text = ex.Message;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;
				}

				return -4;
			}

			return intRetVal;


		}
		#endregion

		#region LoadData Function
		/// <summary>
		/// Stores the XML for the record to be updated
		/// </summary>
		private void LoadData()
		{
			int intCustomerID = Convert.ToInt32(hidCustomerID.Value);
			int intPolicyID = Convert.ToInt32(hidPolicyID .Value);
			int intPolicyVersionID = Convert.ToInt32(hidPolicyVersionID.Value);
			ListItem listItem;
			
			//Add attribute
			

			ClsUmbrellaDwelling  objdwelling = new  ClsUmbrellaDwelling ();

			DataSet dsDwelling = objdwelling.GetPolicyDwellingInfoByID(intCustomerID,intPolicyID ,intPolicyVersionID ,intDwellingID);
			
			DataTable dt = dsDwelling.Tables[0];
			
			if ( dt.Rows.Count == 0 )
			{
				ShowMessage("No record is available.");
				return;
			}

			hidOldData.Value =Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dt);

			txtDWELLING_NUMBER.Text = dt.Rows[0]["DWELLING_NUMBER"].ToString();

			hidIS_ACTIVE.Value=dt.Rows[0]["IS_ACTIVE"].ToString();
			
			if ( dt.Rows[0]["YEAR_BUILT"] != System.DBNull.Value )
			{
				txtYEAR_BUILT.Text = dt.Rows[0]["YEAR_BUILT"].ToString();
			}
			
			if ( dt.Rows[0]["PURCHASE_YEAR"] != System.DBNull.Value )
			{

				txtPURCHASE_YEAR.Text = dt.Rows[0]["PURCHASE_YEAR"].ToString();
			}
			
			if ( dt.Rows[0]["PURCHASE_PRICE"] != System.DBNull.Value )
			{
				txtPURCHASE_PRICE.Text = Convert.ToDecimal(dt.Rows[0]["PURCHASE_PRICE"]).ToString("N");
				txtPURCHASE_PRICE.Text = txtPURCHASE_PRICE.Text.Substring(0,txtPURCHASE_PRICE.Text.LastIndexOf("."));
			}
			
			if ( dt.Rows[0]["MARKET_VALUE"] != System.DBNull.Value )
			{
				txtMARKET_VALUE.Text = Convert.ToDecimal(dt.Rows[0]["MARKET_VALUE"]).ToString("N");
				txtMARKET_VALUE.Text = txtMARKET_VALUE.Text.Substring(0,txtMARKET_VALUE.Text.LastIndexOf("."));
			}
			
			
			if ( dt.Rows[0]["NO_WEEKS_RENTED"] != System.DBNull.Value )
			{

				txtNO_WEEKS_RENTED.Text = dt.Rows[0]["NO_WEEKS_RENTED"].ToString();
			}
			
			if ( dt.Rows[0]["NEED_OF_UNITS"] != System.DBNull.Value )
			{
				txtNEED_OF_UNITS.Text = dt.Rows[0]["NEED_OF_UNITS"].ToString();
			}
			
			
			if ( dt.Rows[0]["LOCATION_ID"] != System.DBNull.Value )
			{
				DataTable dtLoc= objdwelling.GetPolicyLocationDetails(Convert.ToInt32 (dt.Rows[0]["LOCATION_ID"].ToString ()));
				this.lblLOCATION_ID.Text =dtLoc.Rows[0][0].ToString ();
			}
			
			
			if ( dt.Rows[0]["NEIGHBOURS_VISIBLE"] != System.DBNull.Value )
			{
				listItem = cmbNEIGHBOURS_VISIBLE.Items.FindByValue(Convert.ToString(dt.Rows[0]["NEIGHBOURS_VISIBLE"]));
				cmbNEIGHBOURS_VISIBLE.SelectedIndex= cmbNEIGHBOURS_VISIBLE.Items.IndexOf(listItem);	
			}
			
			if ( dt.Rows[0]["OCCUPIED_DAILY"] != System.DBNull.Value )
			{
				listItem = cmbOCCUPIED_DAILY.Items.FindByValue(Convert.ToString(dt.Rows[0]["OCCUPIED_DAILY"]));
				cmbOCCUPIED_DAILY.SelectedIndex= cmbOCCUPIED_DAILY.Items.IndexOf(listItem);	
			}

			if ( dt.Rows[0]["OCCUPANCY"] != System.DBNull.Value )
			{
				listItem = cmbOCCUPANCY.Items.FindByValue(Convert.ToString(dt.Rows[0]["OCCUPANCY"]));
				cmbOCCUPANCY.SelectedIndex= cmbOCCUPANCY.Items.IndexOf(listItem);	
			}

			if ( dt.Rows[0]["BUILDING_TYPE"] != System.DBNull.Value )
			{
				listItem = cmbBUILDING_TYPE.Items.FindByValue(Convert.ToString(dt.Rows[0]["BUILDING_TYPE"]));
				cmbBUILDING_TYPE.SelectedIndex= cmbBUILDING_TYPE.Items.IndexOf(listItem);	
			}

			if ( dt.Rows[0]["REPLACEMENT_COST"] != System.DBNull.Value )
			{
				txtREPLACEMENT_COST.Text = Convert.ToDecimal(dt.Rows[0]["REPLACEMENT_COST"]).ToString("N");
				txtREPLACEMENT_COST.Text = txtREPLACEMENT_COST.Text.Substring(0,txtREPLACEMENT_COST.Text.LastIndexOf("."));
			}
						

		}


		#endregion

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

		#region WebEvent Handler  btnSave_Click
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int retVal = Save();
			
			hidFormSaved.Value = "1";

			lblMessage.Visible = true;

			//Duplicate location
			if ( retVal == -1 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId ,"10");
				hidFormSaved.Value = "2";
				return;
			}
			
			//Duplicate Client dwelling number
			if ( retVal == -2 )
			{
				
				lblMessage.Text = "This Customer dwelling number already exists. Please enter another Customer dwelling number.";
				hidFormSaved.Value = "2";
				return;
			}

			if (retVal < 0)
			{
				//Error occured
				hidFormSaved.Value = "2";
			}

			hidFormSaved.Value = "2";
			if ( retVal > 0 )
			{
				hidFormSaved.Value = "1";
			}
		}
		#endregion
	}
}
