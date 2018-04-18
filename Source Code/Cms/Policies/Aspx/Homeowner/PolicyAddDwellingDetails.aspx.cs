/******************************************************************************************
<Author					: -  SHAFI
<Start Date				: -	 FEB 16, 2006
<End Date				: -	
<Description			: - Add/Edit page for POL_DWELLINGS_INFO	
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
using System.Resources;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Policy.Homeowners;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;


namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for PolicyAddDwellingDetails.
	/// </summary>
	public class PolicyAddDwellingDetails : Cms.Policies.policiesbase 
	{
		protected System.Web.UI.WebControls.Label capDWELLING_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtDWELLING_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtMONTHS_RENTED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDWELLING_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUILDING_TYPE;	
		protected System.Web.UI.WebControls.Label capLOCATION_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION_ID;
		protected System.Web.UI.WebControls.Label capYEAR_BUILT;
		protected System.Web.UI.WebControls.Label capPURCHASE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtPURCHASE_YEAR;
		protected System.Web.UI.WebControls.Label capPURCHASE_PRICE;
		protected System.Web.UI.WebControls.TextBox txtPURCHASE_PRICE;
		protected System.Web.UI.WebControls.Label capMARKET_VALUE;
		//protected System.Web.UI.WebControls.CustomValidator csvMARKET_VALUE;
		protected System.Web.UI.WebControls.TextBox txtMARKET_VALUE;
		protected System.Web.UI.WebControls.Label capREPLACEMENT_COST;
		//protected System.Web.UI.WebControls.RangeValidator rngYEAR_BUILT;
		protected System.Web.UI.WebControls.TextBox txtREPLACEMENT_COST;
		protected System.Web.UI.WebControls.Label capBUILDING_TYPE;
		protected System.Web.UI.WebControls.Label capOCCUPANCY;
		//protected System.Web.UI.WebControls.Label capNEED_OF_UNITS;
		protected System.Web.UI.WebControls.Label capUSAGE;
		protected System.Web.UI.WebControls.Label capNEIGHBOURS_VISIBLE;
		//protected System.Web.UI.WebControls.Label capIS_VACENT_OCCUPY;
		//protected System.Web.UI.WebControls.Label capIS_RENTED_IN_PART;
		protected System.Web.UI.WebControls.Label capOCCUPIED_DAILY;
		protected System.Web.UI.WebControls.Label capNO_WEEKS_RENTED;
		protected System.Web.UI.WebControls.Label capREPLACEMENTCOST_COVA;
		protected System.Web.UI.WebControls.TextBox txtNO_WEEKS_RENTED;
		//protected System.Web.UI.WebControls.Label capIS_DWELLING_OWNED_BY_OTHER;
		protected System.Web.UI.WebControls.DropDownList cmbLOCATION_ID;
		protected System.Web.UI.WebControls.DropDownList cmbBUILDING_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbOCCUPANCY;
		protected System.Web.UI.WebControls.DropDownList cmbNEIGHBOURS_VISIBLE;
		
		protected System.Web.UI.HtmlControls.HtmlTableRow trReplace;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMARKET_VALUE;
		
		protected System.Web.UI.WebControls.DropDownList cmbOCCUPIED_DAILY;
		//protected System.Web.UI.WebControls.DropDownList cmbIS_DWELLING_OWNED_BY_OTHER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyType;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDWELLING_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		//protected System.Web.UI.WebControls.RangeValidator rngPURCHASE_YEAR;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMARKET_VALUE;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revREPLACEMENT_COST;
		protected System.Web.UI.WebControls.RangeValidator rngNO_WEEKS_RENTED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPURCHASE_PRICE;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected System.Web.UI.WebControls.RangeValidator rngDWELLING_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEffDate;
		//protected System.Web.UI.WebControls.RangeValidator rngNEED_OF_UNITS;
		//protected System.Web.UI.WebControls.TextBox txtNEED_OF_UNITS;
		protected System.Web.UI.WebControls.CheckBoxList cblUsages;
		protected System.Web.UI.WebControls.CheckBox cbREPLACEMENTCOST_COVA;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR_BUILT;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvREPLACEMENT_COST;
		 
		//protected System.Web.UI.WebControls.CompareValidator cmpPURCHASE_YEAR;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWhole;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		public string strCalledFrom="";
		protected System.Web.UI.WebControls.Label capDwellingOwned;
		//protected System.Web.UI.WebControls.TextBox txtDwellingOwned;
		protected System.Web.UI.WebControls.Label lblDwellingOwned;
		protected System.Web.UI.WebControls.CustomValidator csvYEAR_BUILT;
		protected System.Web.UI.WebControls.TextBox txtYEAR_BUILT;
		protected System.Web.UI.WebControls.CustomValidator csvDwellingOwned;
		//protected System.Web.UI.WebControls.CustomValidator csvREPLACEMENT_COST;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.CustomValidator csvPurchase_YEAR;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEAR_BUILT;
		protected System.Web.UI.WebControls.Repeater Repeater1;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revPURCHASE_YEAR;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		//protected System.Web.UI.WebControls.Label capREPAIR_COST;
		//protected System.Web.UI.WebControls.TextBox txtREPAIR_COST;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvREPLACEMENT_COSTren;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revREPAIR_COST;
		protected System.Web.UI.WebControls.CompareValidator cmpREPLACEMENT_COST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREPLACEMENT_COST;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPercent;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLVersionID;
		protected System.Web.UI.WebControls.CustomValidator csvDWELLING_NUMBER;
		protected System.Web.UI.WebControls.CompareValidator cmpPURCHASE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvREPLACEMENT_COST;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR_BUILT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPURCHASE_YEAR;
		protected System.Web.UI.WebControls.CustomValidator csvMARKET_VALUE;
		protected System.Web.UI.WebControls.CompareValidator cmpMARKET_VALUE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;

		//added by vj on 19-10-2005.
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckDelete;
		protected System.Web.UI.WebControls.CompareValidator cmpMONTHS_RENTED;
		protected System.Web.UI.WebControls.CustomValidator csvMONTHS_RENTED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOCCUPANCY;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppEffDate;// Added by Charles on 18-Dec-09 for Itrack 6681
		
		DataSet dtStatePolicy = null ;				

		private void Page_Load(object sender, System.EventArgs e)
		{
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
                
			#region Setting ScreenId
			/*if(Request.QueryString["CalledFrom"].ToString()== "Rental" || Request.QueryString["CalledFrom"].ToString()=="RENTAL")

			{
				
			
				capREPAIR_COST.Visible =true;
				
			}
			else
			{
				
				capREPAIR_COST.Visible =false;
			}*/
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
				hidCalledFrom.Value=Request.QueryString["CalledFrom"].ToString();
			}
		
			//Setting screen Id.	
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="239_0";
					//capNEED_OF_UNITS.Text="If HO-4/HO-6, #units/apts"  ;
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="259_0";
					//capNEED_OF_UNITS.Text="#Units/Apts"  ;
					break;
				default:
					base.ScreenId="261";
					break;
			}
			#endregion

			btnReset.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;	
				
			btnSave.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;	
			btnSave.Attributes.Add("OnClick","YearBuilt_OnBlur();");
			
			btnCopy.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write ;
			btnCopy.PermissionString				=	gstrSecurityXML;
			
			btnDelete.CmsButtonClass				=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString				=	gstrSecurityXML;

			
			btnActivateDeactivate.CmsButtonClass				=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnActivateDeactivate.PermissionString				=	gstrSecurityXML;
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
              
			// Put user code to initialize the page here
            //base.getcus
			hidCustomerID.Value	 = GetCustomerID();
			hidPOLID.Value = GetPolicyID();
			hidPOLVersionID.Value =GetPolicyVersionID();
			//lblMARKET_VALUE_MAND.Attributes.Add("style","display:none");
            				
			if ( !Page.IsPostBack )
			{

				if ( hidCustomerID.Value == "" ||
					hidPOLID.Value == "" || 
					hidPOLVersionID.Value == "" 
					)
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","118");
					return;
				}
				if(Request.QueryString["CalledFrom"].ToString()== "Rental" || Request.QueryString["CalledFrom"].ToString()=="RENTAL")
				{
					//SetCombo();			
				}
			    
				AddAttributes();
				
				SetValidators();

				if ( Request.QueryString["Dwelling_ID"] != null )
				{
					this.hidDWELLING_ID.Value = Request.QueryString["Dwelling_ID"].ToString();
				}

				LoadDropdowns();

				// -- Added by mohit on 27/10/2005.
				LoadLocationDropdown();

				if ( this.cmbLOCATION_ID.Items.Count == 0 )
				{
					ShowMessage("No locations are available. To add locations <a href='#' onclick='Location();'>click here</a> ");					
					return;
				}

				//Update
				if ( hidDWELLING_ID.Value != "0" )
				{
					hidDWELLING_ID.Value = Request.QueryString["Dwelling_ID"].ToString();
					LoadData();
					
					//string locSubLoc = this.cmbLOCATION_ID.SelectedItem.Value;

				
				}
				else
				{
					//Add
					btnDelete.Visible = false;
					btnCopy.Visible = false;
					
					ClsDwellingDetails objDwelling = new ClsDwellingDetails();

					//Check if any locations are available
					if ( GetRemainingLocations() == 0 )
					{
						ShowMessage(Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"16"));
						return;
					}

					////////////////
										
					trError.Visible = false;
					
					//Set focus to control
					this.SetFocus("txtDWELLING_NUMBER");

					//Get the next dwelliing number
					int nextDwellingNumber = ClsDwellingDetails.GetNextDwellingNumberForPolicy(
						Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPOLID.Value),
						Convert.ToInt32(hidPOLVersionID.Value));
					
					if ( nextDwellingNumber == -1 )
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"18");

						return;
					}

					this.txtDWELLING_NUMBER.Text = nextDwellingNumber.ToString();
				}
				SetWorkflow();
				
				//added by vj on 19-10-2005
				hidCheckDelete.Value = "0";
				btnDelete.Attributes.Add("onclick","javascript:SetRemoveTabValue();");
				hidPercent.Value = "100";
				//txtMARKET_VALUE.Attributes.Add("onBlur","javascript:DefaultRepairCost();");

				SetValidators();
				
				//Set the text in the labels
				SetCaptions();
				
			}

		}
		
		/// <summary>
		/// Sets the validators according to product for Home
		/// </summary>
		private void SetReplacementCostForHome()
		{
			//DataSet dtStatePolicy=new DataSet();
			
			ClsDwellingDetails objDwelling=new ClsDwellingDetails();
			
			if(dtStatePolicy!=null)
			{
				hidStateId.Value=dtStatePolicy.Tables[0].Rows[0]["State_Id"].ToString();
				//hidPolicyType.Value=dtStatePolicy.Tables[0].Rows[0]["Policy_Type"].ToString();
				////csvMARKET_VALUE.Enabled=false;
				if(hidStateId.Value!="")
				{
					string strREPLACEMENT_COST=ClsGeneralInformation.FetchEffectiveReplacementCostForHome(hidPolicyType.Value,hidAppEffDate.Value,hidStateId.Value); //Added by Charles on 18-Dec-09 for Itrack 6681
					//Changed from hard coded replacement value, Charles (18-Dec-09), Itrack 6681
					if(hidStateId.Value=="22")
					{
						switch(hidPolicyType.Value)
						{
							case "11409":	//HO-3 Premier								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost for HO-3 Premier must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);								
								//csvMARKET_VALUE.Enabled=false;
								
								break;
							case "11410":	//HO-5 Premier								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;  
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For HO-5 Premier it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								//csvMARKET_VALUE.Enabled=false;
								
								break;
							case "11402":	//HO-2 Replacement							
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For HO-2 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);

								
								break;
							case "11400":	//HO-3 Replacement								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For HO-3 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								break;
							case "11401":	//HO-5 Replacement								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST; //Changed from 125000, Charles (30-Nov-09), Itrack 6681
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For HO-5 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);								
								
								break;                            
							case "11403":	//HO-2 Repair Cost							
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								//cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost for HO-2 Repair Cost must be greater than or equal to 15,000";
								cmpREPLACEMENT_COST.ErrorMessage = "Repair cost  must be numeric, non-zero and non-negative. For HO-2 Repair Cost must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								//capREPLACEMENT_COST.Text = "Repair Cost";
								rfvREPLACEMENT_COST.ErrorMessage="Please enter repair cost.";
								csvMARKET_VALUE.Enabled=false;
								//csvMARKET_VALUE.Enabled=false;

								cmpMARKET_VALUE.Enabled = true;
								cmpMARKET_VALUE.ValueToCompare = strREPLACEMENT_COST;
								cmpMARKET_VALUE.ErrorMessage = "Market value/Repair Cost  must be numeric, non-zero and non-negative. For HO-2 Repair Cost must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								
								cmpREPLACEMENT_COST.Enabled = false;

								break;                            
							case "11404":	//HO-3 Repair Cost
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								//cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost for HO-3 Repair Cost must be greater than or equal to 40,000";
								cmpREPLACEMENT_COST.ErrorMessage ="Repair cost  must be numeric, non-zero and non-negative. For HO-3 Repair Cost must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								//capREPLACEMENT_COST.Text = "Repair Cost";
								rfvREPLACEMENT_COST.ErrorMessage="Please enter repair cost.";
								//csvMARKET_VALUE.Enabled=false;
								csvMARKET_VALUE.Enabled=false;
								cmpMARKET_VALUE.Enabled = true;
								cmpMARKET_VALUE.ValueToCompare = strREPLACEMENT_COST;
								cmpMARKET_VALUE.ErrorMessage = "Market value/Repair Cost  must be numeric, non-zero and non-negative. For HO-3 Repair Cost must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								
								//cmpREPLACEMENT_COST.Enabled = false;

								break;                            
							case "11405":	//HO-4 Tenant								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Contents Insurance Amount  must be numeric, non-zero and non-negative. For HO-4 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								csvMARKET_VALUE.Enabled=false;
								break;                            
							case "11406":	//HO-6 Unit Owners								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Contents Insurance Amount  must be numeric, non-zero and non-negative. For HO-6 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								csvMARKET_VALUE.Enabled=false;
								break;                            
								//Commented As In Active now
								//							case "HO-4 Deluxe":
								//								cmpREPLACEMENT_COST.ValueToCompare="25000";
								//								cmpREPLACEMENT_COST.ErrorMessage ="Contents Insurance Amount  must be numeric, non-zero and non-negative. For HO-4 Deluxe it must be greater than or equal to 25,000";
								//								break;                            
								//							case "HO-6 Deluxe":								
								//								cmpREPLACEMENT_COST.ValueToCompare="25000";
								//								cmpREPLACEMENT_COST.ErrorMessage ="Contents Insurance Amount  must be numeric, non-zero and non-negative. For HO-6 Deluxe it must be greater than or equal to 25,000";
								//								break;         
							default:
								cmpREPLACEMENT_COST.ValueToCompare="100000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For the current policy it must be greater than or equal to 100,000";
								break;         
						}
					}
					else
					{
						switch(hidPolicyType.Value)
						{
							case "11192":	//HO-2 Replacement								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;								
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For HO-2 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								
								break;                            
							case "11148":	//HO-3 Replacement								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For HO-3 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								
								break;                            
							case "11149":	//HO-5 Replacement								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For HO-5 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								
								break;                            
							case "11193":	//HO-2 Repair Cost
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								//cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost for HO-2 Repair Cost must be greater than or equal to 50,000";
								cmpREPLACEMENT_COST.ErrorMessage ="Repair cost  must be numeric, non-zero and non-negative and HO-2 Repair Cost must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								csvMARKET_VALUE.Enabled=false;
								//capREPLACEMENT_COST.Text = "Repair Cost";
								rfvREPLACEMENT_COST.ErrorMessage="Please enter repair cost.";
								//csvMARKET_VALUE.Enabled=false;

								cmpMARKET_VALUE.Enabled = true;
								cmpMARKET_VALUE.ValueToCompare = strREPLACEMENT_COST;
								cmpMARKET_VALUE.ErrorMessage = "Market Value/Repair Cost  must be numeric, non-zero and non-negative. For HO-2 Repair Cost must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								
								cmpREPLACEMENT_COST.Enabled = false;

								break;                            
							case "11194":	//HO-3 Repair Cost								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								//cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost for HO-3 Repair Cost must be greater than or equal to 50,000";
								cmpREPLACEMENT_COST.ErrorMessage ="Repair cost  must be numeric, non-zero and non-negative. For HO-3 Repair Cost must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								//capREPLACEMENT_COST.Text = "Repair Cost";
								rfvREPLACEMENT_COST.ErrorMessage="Please enter repair cost.";
								csvMARKET_VALUE.Enabled=false;

								cmpMARKET_VALUE.Enabled = true;
								cmpMARKET_VALUE.ValueToCompare = strREPLACEMENT_COST;
								cmpMARKET_VALUE.ErrorMessage = "Market Value/Repair Cost  must be numeric, non-zero and non-negative. For HO-3 Repair Cost must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								
								//cmpREPLACEMENT_COST.Enabled = false;

								break;                            
							case "11195":	//HO-4  Tenant								
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Contents Insurance Amount  must be numeric, non-zero and non-negative. For HO-4 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								csvMARKET_VALUE.Enabled=false;
								break;                            
							case "11196":	//HO-6 Unit Owners							
								cmpREPLACEMENT_COST.ValueToCompare=strREPLACEMENT_COST;
								cmpREPLACEMENT_COST.ErrorMessage ="Contents Insurance Amount  must be numeric, non-zero and non-negative. For HO-6 it must be greater than or equal to "+ClsCommon.FormatNumber(strREPLACEMENT_COST);
								csvMARKET_VALUE.Enabled=false;
								break;                            
								/*case "HO-4 Deluxe":
									cmpREPLACEMENT_COST.ValueToCompare="25000";
									cmpREPLACEMENT_COST.ErrorMessage ="Contents Insurance Amount  must be numeric, non-zero and non-negative. For HO-4 Deluxe it must be greater than or equal to 25,000";
									break;                            
								case "HO-6 Deluxe":
									cmpREPLACEMENT_COST.ValueToCompare="25000";
									cmpREPLACEMENT_COST.ErrorMessage ="Contents Insurance Amount  must be numeric, non-zero and non-negative. For HO-6 Deluxe it must be greater than or equal to 25,000";
									break;      */
							default:
								cmpREPLACEMENT_COST.ValueToCompare="100000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative. For the current policy, it must be greater than or equal to 100,000";
								break;         
						}
					}
				}
			}

		}


		/// <summary>
		/// Sets the validators according to product for Rental
		/// </summary>
		
		private void SetReplacementCostForRental()
		{
			/*
			DataSet dtStatePolicy=new DataSet();
			ClsDwellingDetails objDwelling=new ClsDwellingDetails();
			dtStatePolicy=objDwelling.GetPolicyState(hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value);
			*/


			if(dtStatePolicy!=null)
			{
				hidStateId.Value=dtStatePolicy.Tables[0].Rows[0]["State_Id"].ToString();
				//hidPolicyType.Value=dtStatePolicy.Tables[0].Rows[0]["Policy_Type"].ToString();
				if(hidStateId.Value!="")
				{
					if(hidStateId.Value=="22")
					{
						switch(hidPolicyType.Value)
						{
							case "11289":	//DP-2							
								cmpREPLACEMENT_COST.ValueToCompare="30000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For DP-2, Replacement Cost must be greater than or equal to 30,000";								
								break;
							case "11291":	//DP-3							
								cmpREPLACEMENT_COST.ValueToCompare="75000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For DP-3, Replacement Cost must be greater than or equal to 75,000";
								break;
							case "11290": //DP-2 Repair Cost								
								cmpREPLACEMENT_COST.ValueToCompare="30000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For DP-2 Repair Cost, Replacement Cost must be greater than or equal to 30,000";
								csvMARKET_VALUE.Enabled=false;

								cmpMARKET_VALUE.Enabled = true;
								cmpMARKET_VALUE.ValueToCompare = "30000";
								cmpMARKET_VALUE.ErrorMessage = "Market Value/Repair Cost must be numeric, non-zero and non-negative. For DP-2 Repair Cost, Market Value/Repair Cost must be greater than or equal to 30,000";
								
								cmpREPLACEMENT_COST.Enabled = false;

								break;
							case "11292":	//DP-3 Repair Cost								
								cmpREPLACEMENT_COST.ValueToCompare="75000";
								cmpREPLACEMENT_COST.ErrorMessage ="Market Value  must be numeric, non-zero and non-negative. For DP-3 Repair Cost must be greater than or equal to 75,000";
								csvMARKET_VALUE.Enabled=false;

								cmpMARKET_VALUE.Enabled = true;
								cmpMARKET_VALUE.ValueToCompare = "75000";
								cmpMARKET_VALUE.ErrorMessage = "Market Value/Repair Cost  must be numeric, non-zero and non-negative. For DP-3 Repair Cost, Market Value/Repair Cost must be greater than or equal to 75,000";
								
								cmpREPLACEMENT_COST.Enabled = false;

								break;
							case "11458":	 //DP-3 Premier							
								cmpREPLACEMENT_COST.ValueToCompare="75000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For DP-3 Premier, Replacement cost must be greater than or equal to 75,000";
								break;                            
							    
							default:
								cmpREPLACEMENT_COST.ValueToCompare="100000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For the current policy Replacement cost must be greater than or equal to 100,000";
								break;         
						}
					}
					else	//INDIANA
					{
						switch(hidPolicyType.Value)
						{
							case "11479":  //DP-2								
								cmpREPLACEMENT_COST.ValueToCompare="30000";								
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero amd non-negative. For DP-2 Replacement cost must be greater than or equal to 30,000";
								break;                            
							case "11481":	 //DP-3							
								cmpREPLACEMENT_COST.ValueToCompare="75000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For DP-3 Replacement cost must be greater than or equal to 75,000";
								break;                            
							case "11480":	//DP-2 Repair Cost							
								cmpREPLACEMENT_COST.ValueToCompare="30000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For DP-2 Repair Cost, Repair Cost must be greater than or equal to 30,000";
								cmpREPLACEMENT_COST.Enabled = false;

								cmpMARKET_VALUE.Enabled = true;
								cmpMARKET_VALUE.ValueToCompare = "30000";
								cmpMARKET_VALUE.ErrorMessage ="Market Value/Repair Cost must be numeric, non-zero and non-negative. For DP-2 Repair Cost, Market Value/Repair Cost must be greater than or equal to 30,000";

							
								break;                            
							case "11482":	//DP-3 Repair Cost
								cmpREPLACEMENT_COST.ValueToCompare="75000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For DP-3 Repair Cost must be greater than or equal to 75,000";
								cmpREPLACEMENT_COST.Enabled = false;

								cmpMARKET_VALUE.Enabled = true;
								cmpMARKET_VALUE.ValueToCompare = "75000";
								cmpMARKET_VALUE.ErrorMessage ="Market Value/Repair Cost must be numeric, non-zero and non-negative. For DP-3 Market Value/Repair Cost must be greater than or equal to 75,000";

								break;                            
						
							default:
								cmpREPLACEMENT_COST.ValueToCompare="100000";
								cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost must be numeric, non-zero and non-negative. For the current policy Replacement cost must be greater than or equal to 100,000";
								break;         
						}
					}
				}
			}

		}
	
		//Removed combo
		/*private void SetReplacementCost()
		{			
			int returnResult=-1;
			ClsDwellingDetails objDwelling = new ClsDwellingDetails();
			
			returnResult=objDwelling.GetReplacementCost(hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value);
			if(returnResult!=-1 || returnResult!=0)
			{
				cmbREPLACEMENT_COSTren.SelectedValue=Convert.ToString(returnResult);
			}				
			
		}*/

		private void SetPercentageValue()
		{			
			//int returnResult=-1;
			ClsDwellingDetails objDwelling = new ClsDwellingDetails();
			
			/*
			returnResult=objDwelling.GetPercentagePoints(hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value);			
			if(returnResult!=-1)
				hidPercent.Value=Convert.ToString(returnResult);
			else
				hidPercent.Value="0";
			*/

			hidPercent.Value = "100";

			
		}
		
		private int GetRemainingLocations()
		{
			ClsDwellingDetails objDwelling = new ClsDwellingDetails();

			//Check if any locations are available
			//Check if any locations are available
			DataTable dtLoc = objDwelling.GetRemainingLocationsPolicy(Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(hidPOLID.Value),
				Convert.ToInt32(hidPOLVersionID.Value)
				);
			return dtLoc.Rows.Count;

		}

		/// <summary>
		/// 
		/// </summary>
		///
		//Removed combo
		/*private void SetCombo()
		{
			ClsDwellingDetails objDwelling = new ClsDwellingDetails();
			int policytype_Id=objDwelling.Getpolicyid(Convert.ToInt32(hidCustomerID.Value),Convert.ToInt32(hidAppID.Value),Convert.ToInt32(hidAppVersionID.Value));
			cmbREPLACEMENT_COSTren.Items.Clear();
			
			if(policytype_Id==11458)
			{
				cmbREPLACEMENT_COSTren.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RDRPNP");
				cmbREPLACEMENT_COSTren.DataTextField="LookupDesc";
				cmbREPLACEMENT_COSTren.DataValueField="LookupID";
				cmbREPLACEMENT_COSTren.DataBind();
				cmbREPLACEMENT_COSTren.Items.Insert(0,"");
			}

			else
			{
				cmbREPLACEMENT_COSTren.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RDREPP",null,"Y");
				cmbREPLACEMENT_COSTren.DataTextField="LookupDesc";
				cmbREPLACEMENT_COSTren.DataValueField="LookupID";
				cmbREPLACEMENT_COSTren.DataBind();
				cmbREPLACEMENT_COSTren.Items.Insert(0,"");
			}



		}*/
		private void LoadLocationDropdown()
		{
			if (hidDWELLING_ID.Value != "0")
			{				
				DataTable dtLocations = ClsLocation.GetLocationsForPolicy(Convert.ToInt32(hidCustomerID.Value),Convert.ToInt32(hidPOLID.Value),Convert.ToInt32(hidPOLVersionID.Value),Convert.ToInt32(hidDWELLING_ID.Value));
                this.cmbLOCATION_ID.DataSource = dtLocations;
				this.cmbLOCATION_ID.DataTextField = "Address";
				//this.cmbLOCATION_ID.DataValueField = "LOCATION_ID";
				this.cmbLOCATION_ID.DataValueField = "LOCATIONID";
				this.cmbLOCATION_ID.DataBind();
			}
			else
			{
				ClsDwellingDetails objDwelling = new ClsDwellingDetails();
				DataTable dtLoc = objDwelling.GetRemainingLocationsPolicy(Convert.ToInt32(hidCustomerID.Value),Convert.ToInt32(hidPOLID.Value),Convert.ToInt32(hidPOLVersionID.Value));
				this.cmbLOCATION_ID.DataSource = dtLoc;
				this.cmbLOCATION_ID.DataTextField = "Address";
				//this.cmbLOCATION_ID.DataValueField = "LOCATION_ID";
				this.cmbLOCATION_ID.DataValueField = "LOCATIONID";
				this.cmbLOCATION_ID.DataBind();
			}
		}

		private void ShowMessage(string strMessage)
		{
			trBody.Attributes.Add("style","display:none");
			lblError.Text = strMessage;
			trError.Visible = true;
			//return;
		}

		private void AddAttributes()
		{
			//Reset button
			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");
				
			//Attributes for amount fields////			
			//this.txtMARKET_VALUE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtPURCHASE_PRICE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtREPLACEMENT_COST.Attributes.Add("onBlur","this.value=formatCurrency(this.value);SetMinMarketValue();");
			//this.txtREPAIR_COST.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtMARKET_VALUE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);ValidatorValidate(document.getElementById('csvMARKET_VALUE'));DefaultRepairCost();SetMinMarketValue();");
			this.txtPURCHASE_PRICE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtREPLACEMENT_COST.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			//this.txtREPAIR_COST.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			/////
		}

		/// <summary>
		/// Sets the text of the labels
		/// </summary>
		private void SetCaptions()
		{
			ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.aspx.HomeOwners.PolicyAddDwellingDetails",System.Reflection.Assembly.GetExecutingAssembly());

			capDWELLING_NUMBER.Text	=	objResourceMgr.GetString("txtDWELLING_NUMBER");
			capLOCATION_ID.Text	= objResourceMgr.GetString("cmbLOCATION_ID");
			capYEAR_BUILT.Text = objResourceMgr.GetString("txtYEAR_BUILT");
			capPURCHASE_YEAR.Text	= objResourceMgr.GetString("txtPURCHASE_YEAR");
			capPURCHASE_PRICE.Text = objResourceMgr.GetString("txtPURCHASE_PRICE");
			capMARKET_VALUE.Text = objResourceMgr.GetString("txtMARKET_VALUE");
			capBUILDING_TYPE.Text =	objResourceMgr.GetString("cmbBUILDING_TYPE");
			capOCCUPANCY.Text =	objResourceMgr.GetString("cmbOCCUPANCY");
			capNEIGHBOURS_VISIBLE.Text = objResourceMgr.GetString("cmbNEIGHBOURS_VISIBLE");
			capOCCUPIED_DAILY.Text	= objResourceMgr.GetString("cmbOCCUPIED_DAILY");
			capNO_WEEKS_RENTED.Text	= objResourceMgr.GetString("txtNO_WEEKS_RENTED");
			capREPLACEMENTCOST_COVA.Text = objResourceMgr.GetString("capREPLACEMENTCOST_COVA");

			//Set customer Validations as par Effective Date Itrack NO. 3281
			DataSet dtEffDate = null ;
			ClsDwellingDetails objDwelling = new ClsDwellingDetails();
			dtEffDate=objDwelling.GetEffectiveDate(hidCustomerID.Value,hidPOLID.Value,hidPOLVersionID.Value,"POL");
			hidEffDate.Value = dtEffDate.Tables[0].Rows[0]["EFF_DATE"].ToString();

			hidAppEffDate.Value = new Cms.BusinessLayer.BlApplication.clsWatercraftInformation().GetPolEffectiveDate(Convert.ToInt32(GetCustomerID()),Convert.ToInt32(GetPolicyID()),Convert.ToInt32(GetPolicyVersionID())); //Added by Charles on 18-Dec-09 for Itrack 6681

			//Set custom validations for HOME LOB
			if(strCalledFrom.ToUpper()=="HOME")
			{	
				if (dtEffDate.Tables[0].Rows[0]["EFF_DATE"] != null && dtEffDate.Tables[0].Rows[0]["EFF_DATE"].ToString() !="")
				{			
					//SetReplacementCostForHome();
				}
				else
				{
					cmpREPLACEMENT_COST.ValueToCompare="1";
					cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative.";													
				}
			}//Set custom validations for RENTAL LOB
			else if(strCalledFrom.ToUpper()=="RENTAL")
			{
				if (dtEffDate.Tables[0].Rows[0]["EFF_DATE"] != null && dtEffDate.Tables[0].Rows[0]["EFF_DATE"].ToString() !="")
				{		
					SetReplacementCostForRental();
				}
				else
				{
					cmpREPLACEMENT_COST.ValueToCompare="1";
					cmpREPLACEMENT_COST.ErrorMessage ="Replacement cost  must be numeric, non-zero and non-negative.";													
				}
			}

			string policyType = this.hidPolicyType.Value;

			// For HO-4 and HO- 6 Both state
			if ( policyType == "11405" || policyType == "11195"  || policyType == "11406" || policyType == "11196" )
			{
				capREPLACEMENT_COST.Text = "Contents Insurance Amount";
				rfvREPLACEMENT_COST.ErrorMessage = "Please enter Contents Insurance Amount.";
			}
				//else if(policyType == "HO-2 Repair Cost" || policyType == "HO-3 Repair Cost" || policyType == "DP-2 Repair Cost" || policyType == "DP-3 Repair Cost")
			else if(policyType == "11403" || policyType == "11193" ||
				policyType == "11404" || policyType == "11194" || 
				policyType == "11290" || policyType == "11480" || 
				policyType == "11292" || policyType == "11482" )
			{
				capREPLACEMENT_COST.Text = "Repair Cost";
			}
			else
			{																							 
				capREPLACEMENT_COST.Text =	objResourceMgr.GetString("txtREPLACEMENT_COST");
			}
			
			/*
			COVG ISSUE # 577
			----------------
			If Policy Type is HO-2 Repair or HO-3 Repair -> 
			Then change the Field - Market Value to Market Value/Repair Cost 
			and Delete existing repair Cost (DO NOT SHOW)
			
			GEN ISSUE 3086
			--------------
			Dwelling Details Tab for DP-2 Repair and DP-3 Repair 
			Change the Field Market Value to Market Value/Repair Cost 
			and remove the Repair Cost Field 
			
			*/
			// setting visibility of cov a check box
			if(policyType == "11402" || policyType == "11148" || policyType == "11400" || policyType == "11192" || policyType == "11289" || policyType == "11479" || policyType == "11291" || policyType == "11481")
			{
				capREPLACEMENTCOST_COVA.Visible=true;
				capREPLACEMENTCOST_COVA.Enabled = true;
				cbREPLACEMENTCOST_COVA.Visible=true;
				cbREPLACEMENTCOST_COVA.Enabled=true;
			}	
			else
			{
				capREPLACEMENTCOST_COVA.Visible=false;
				capREPLACEMENTCOST_COVA.Enabled = false;
				cbREPLACEMENTCOST_COVA.Visible=false;
				cbREPLACEMENTCOST_COVA.Enabled=false;
			}
			if (policyType == "11403" ||policyType == "11404" || policyType == "11193" || policyType == "11194" || policyType == "11290" || policyType == "11292" || policyType == "11480" || policyType == "11482")
			{
				capMARKET_VALUE.Text = "Market Value/Repair Cost <font color='red'>*</font>";
				txtREPLACEMENT_COST.Visible=false;
				capREPLACEMENT_COST.Visible=false;
				cmpREPLACEMENT_COST.Enabled =false;
				rfvREPLACEMENT_COST.Enabled =false;
				cmpREPLACEMENT_COST.Visible=false;
				rfvREPLACEMENT_COST.Visible=false;
				rfvREPLACEMENT_COST.Attributes.Add("enabled","false");
				cmpREPLACEMENT_COST.Attributes.Add("enabled","false");
				rfvMARKET_VALUE.Visible = true;
				rfvMARKET_VALUE.Enabled = true;
				rfvMARKET_VALUE.Attributes.Add("enabled","true");
				trReplace.Visible=false;
			}
			else
			{
				capMARKET_VALUE.Text = objResourceMgr.GetString("txtMARKET_VALUE");
				rfvMARKET_VALUE.Visible = false;
				rfvMARKET_VALUE.Enabled = false;
				rfvMARKET_VALUE.Attributes.Add("enabled","false");
			}
		}

		
		/// <summary>
		/// Load the various dropdowns in the page
		/// </summary>
		private void LoadDropdowns()
		{

			

			//Building type
			//ClsCommon.BindLookupDDL(this.cmbBUILDING_TYPE,"HBLDTY",null);
			cmbBUILDING_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HBLDTY",null,null);
			cmbBUILDING_TYPE.DataTextField="LookupDesc";
			cmbBUILDING_TYPE.DataValueField="LookupID";
			cmbBUILDING_TYPE.DataBind();
			string strValue="";
			cmbBUILDING_TYPE.Items.Insert(0,new ListItem("",strValue));

			//Occupancy
			ClsCommon.BindLookupDDL(this.cmbOCCUPANCY,"OCCUPA",null);
			//Remove option of blank field
			ListItem Li = cmbOCCUPANCY.Items.FindByValue("8961");//"8961" -> Blank
			if (Li != null)	
			{
				cmbOCCUPANCY.Items.Remove(Li);	
			}
			cmbOCCUPANCY.Items.Insert(0,new ListItem("",""));

			//RPSINGH -- July 04, 2006 -- Gen Issue 3094
			//Delete the option of Owner -- Remove in case of Rental only
			if (hidCalledFrom.Value.ToUpper() == "RENTAL")
			{
				ListItem LI = cmbOCCUPANCY.Items.FindByValue("8962");//"8962" -> Owner
				if (LI != null)	
				{
					cmbOCCUPANCY.Items.Remove(LI);	
				}
			}
			//End of Addition by RPSINGH -- July 04, 2006
			
			ClsDwellingDetails objDwelling=new ClsDwellingDetails();
			dtStatePolicy=objDwelling.GetPolicyStateForPolicy(hidCustomerID.Value,hidPOLID.Value,hidPOLVersionID.Value);
			
			if ( dtStatePolicy != null )
			{
				if ( dtStatePolicy.Tables.Count > 0 )
				{
					if ( dtStatePolicy.Tables[0].Rows[0]["POLICY_TYPE_ID"] != DBNull.Value )
					{
						this.hidPolicyType.Value = dtStatePolicy.Tables[0].Rows[0]["POLICY_TYPE_ID"].ToString();
					}

				}
			}

		}

		/// <summary>
		/// Sets the validator error messages
		/// </summary>
		private void SetValidators()
		{
			//Set max year for validators
			//this.rngYEAR_BUILT.MaximumValue = DateTime.Now.Year.ToString();
			//this.rngPURCHASE_YEAR.MaximumValue = DateTime.Now.Year.ToString();
			
			//this.rngPURCHASE_YEAR.ErrorMessage = String.Format(Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3"),this.rngPURCHASE_YEAR.MaximumValue);
			//this.rngYEAR_BUILT.ErrorMessage = String.Format(Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8"),this.rngYEAR_BUILT.MaximumValue);

			this.rfvDWELLING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvBUILDING_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"27");
			//rfvREPLACEMENT_COST.ErrorMessage	= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			this.rngDWELLING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			
			// <Gaurav Tyagi> 27 May 2005: START: <To make following field required> , BUG NO:<584>
			
			this.rfvYEAR_BUILT.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			this.csvYEAR_BUILT.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");			
			this.rfvREPLACEMENT_COST.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");			
			
			
			
			// <Gaurav Tyagi> 27 May 2005: END: <To make following field required> , BUG NO:<584>
			
			this.rfvLOCATION_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
	   
			//this.revMARKET_VALUE.ValidationExpression = aRegExpDoublePositiveZero;
			//this.revMARKET_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","217");

		    //Reg Exp changed by Ruchika Chauhan on 16-Feb-2012
            //this.revMARKET_VALUE.ValidationExpression =  aRegExpDoublePositiveNonZero;
            this.revMARKET_VALUE.ValidationExpression = aRegExpCurrencyformat;
			
            this.revMARKET_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","492");

			//ITrack 6146 27 July 2008 - Manoj Rathore
			if(hidPolicyType.Value =="11409" || hidPolicyType.Value =="11410" || hidPolicyType.Value =="11149" || hidPolicyType.Value == "11401")
			{
				this.csvMARKET_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1054");	
			}
			else
			{
				this.csvMARKET_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");			
			}

			this.revYEAR_BUILT.ValidationExpression            =  aRegExpInteger;
			//this.revPURCHASE_YEAR.ValidationExpression            =  aRegExpInteger;
			//this.revREPAIR_COST.ValidationExpression =aRegExpDoublePositiveZero;
			//this.revREPAIR_COST.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage("G","217");
			//this.revREPAIR_COST.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			//this.revPURCHASE_PRICE.ValidationExpression = aRegExpDoublePositiveZero;
			
			/*Commented by Asfa (04-June-2008) - iTrack #3953
			this.revPURCHASE_PRICE.ValidationExpression = Cms.Policies.policiesbase.aRegExpDoublePositiveZero;
			*/
			this.revPURCHASE_PRICE.ValidationExpression = aRegExpCurrencyformat;

			this.revPURCHASE_PRICE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","492");
			//this.revPURCHASE_PRICE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","217");
			this.revPURCHASE_PRICE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.revYEAR_BUILT.ErrorMessage	   = Cms.CmsWeb.ClsMessages.GetMessage("G","516");
			//this.revPURCHASE_YEAR.ErrorMessage	   = Cms.CmsWeb.ClsMessages.GetMessage("G","516");
			//this.rngYEAR_BUILT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("677");

			//this.revREPLACEMENT_COST.ValidationExpression = appbase.aRegExpDoublePositiveNonZero;
			//this.revREPLACEMENT_COST.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			
			this.rngNO_WEEKS_RENTED.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			/*if(Request.QueryString["CalledFrom"].ToString()== "Rental" || Request.QueryString["CalledFrom"].ToString()=="RENTAL")
				rfvREPLACEMENT_COSTren.ErrorMessage = "Please Select The Replacement Cost";
			else
			{
				this.rfvREPLACEMENT_COST.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
				this.csvREPLACEMENT_COST.ErrorMessage=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"19");
			}*/
			//rngNEED_OF_UNITS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			//this.cmpPURCHASE_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"17");
			//csvPurchase_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"522");
			this.csvPurchase_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"23");
			this.cmpPURCHASE_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"23");
			this.rfvOCCUPANCY.ErrorMessage	   =  Cms.CmsWeb.ClsMessages.GetMessage("G","964");

			
			


			//-------------------------------Added by Mohit on 29/09/2005----------------.
			
			

			//--------------------------------------End----------------------------------.

		}

		/// <summary>
		/// Stores the XML for the record to be updated
		/// </summary>
		private void LoadData()
		{
			int  intCustomerID = Convert.ToInt32(hidCustomerID.Value);
			int intPolID = Convert.ToInt32(hidPOLID.Value);
			int intPolVersionID = Convert.ToInt32(hidPOLVersionID.Value);
			int intDwellingID = Convert.ToInt32(hidDWELLING_ID.Value);
			ListItem listItem;
			

			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					btnCopy.Attributes.Add("onclick",
						"javascript:return OpenPopupWindow('PolicyLocationPopup.aspx?calledfrom=HOME&DWELLING_ID=" + hidDWELLING_ID.Value+ "');");
					break;
				case "Rental":
				case "RENTAL":
					btnCopy.Attributes.Add("onclick",
						"javascript:return OpenPopupWindow('PolicyLocationPopup.aspx?calledfrom=Rental&DWELLING_ID=" + hidDWELLING_ID.Value+ "');");
					break;			
			}
			//Add attribute
			

			ClsDwellingDetails objdwelling = new ClsDwellingDetails();
//DataSet dsDwelling = objdwelling.GetPolicyDwellingInfoByID(intCustomerID,intPolID,intPolVersionID,intDwellingID);
			DataSet dsDwelling = objdwelling.GetPolicyDwellingInfoByID(intCustomerID,intPolID,intPolVersionID,intDwellingID);
			
			//hidOldData.Value = dsDwelling.GetXml();

			DataTable dt = dsDwelling.Tables[0];
			
			if ( dt.Rows.Count == 0 )
			{
				ShowMessage("No record is available.");
				return;
			}

			hidOldData.Value = ClsCommon.GetXML(dt);

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
				//	txtPURCHASE_PRICE.Text = dt.Rows[0]["PURCHASE_PRICE"].ToString() ;
			}
			
			if ( dt.Rows[0]["MARKET_VALUE"] != System.DBNull.Value )
			{
				txtMARKET_VALUE.Text = Convert.ToDecimal(dt.Rows[0]["MARKET_VALUE"]).ToString("N");
				txtMARKET_VALUE.Text = txtMARKET_VALUE.Text.Substring(0,txtMARKET_VALUE.Text.LastIndexOf("."));
				//	txtMARKET_VALUE.Text = dt.Rows[0]["MARKET_VALUE"].ToString() ;
			}

			
			if ( dt.Rows[0]["MONTHS_RENTED"] != System.DBNull.Value )
			{
				txtMONTHS_RENTED.Text = dt.Rows[0]["MONTHS_RENTED"].ToString();
			}
			
			if ( dt.Rows[0]["NO_WEEKS_RENTED"] != System.DBNull.Value )
			{

				txtNO_WEEKS_RENTED.Text = dt.Rows[0]["NO_WEEKS_RENTED"].ToString();
			}
			
			//if ( dt.Rows[0]["NEED_OF_UNITS"] != System.DBNull.Value )
			//{
			//	txtNEED_OF_UNITS.Text = dt.Rows[0]["NEED_OF_UNITS"].ToString();
			//}
			
			
			if ( dt.Rows[0]["LOCATION_ID"] != System.DBNull.Value )
			{
				listItem = cmbLOCATION_ID.Items.FindByValue(Convert.ToString(dt.Rows[0]["LOCATION_ID"]));
				cmbLOCATION_ID.SelectedIndex= cmbLOCATION_ID.Items.IndexOf(listItem);	
			}
			
			/*
			if ( dt.Rows[0]["LOC_SUBLOC"] != System.DBNull.Value )
			{
				listItem = cmbLOCATION_ID.Items.FindByValue(Convert.ToString(dt.Rows[0]["LOC_SUBLOC"]));
				cmbLOCATION_ID.SelectedIndex= cmbLOCATION_ID.Items.IndexOf(listItem);	
			}*/

			if ( dt.Rows[0]["NEIGHBOURS_VISIBLE"] != System.DBNull.Value )
			{
				listItem = cmbNEIGHBOURS_VISIBLE.Items.FindByValue(Convert.ToString(dt.Rows[0]["NEIGHBOURS_VISIBLE"]));
				cmbNEIGHBOURS_VISIBLE.SelectedIndex= cmbNEIGHBOURS_VISIBLE.Items.IndexOf(listItem);	
			}
			
			//			if ( dt.Rows[0]["IS_VACENT_OCCUPY"] != System.DBNull.Value )
			//			{
			//				listItem = cmbIS_VACENT_OCCUPY.Items.FindByValue(Convert.ToString(dt.Rows[0]["IS_VACENT_OCCUPY"]));
			//				cmbIS_VACENT_OCCUPY.SelectedIndex= cmbIS_VACENT_OCCUPY.Items.IndexOf(listItem);	
			//			}
			
			//			if ( dt.Rows[0]["IS_RENTED_IN_PART"] != System.DBNull.Value )
			//			{
			//				listItem = cmbIS_RENTED_IN_PART.Items.FindByValue(Convert.ToString(dt.Rows[0]["IS_RENTED_IN_PART"]));
			//				cmbIS_RENTED_IN_PART.SelectedIndex= cmbIS_VACENT_OCCUPY.Items.IndexOf(listItem);	
			//			}

			if ( dt.Rows[0]["OCCUPIED_DAILY"] != System.DBNull.Value )
			{
				listItem = cmbOCCUPIED_DAILY.Items.FindByValue(Convert.ToString(dt.Rows[0]["OCCUPIED_DAILY"]));
				cmbOCCUPIED_DAILY.SelectedIndex= cmbOCCUPIED_DAILY.Items.IndexOf(listItem);	
			}

			//			if ( dt.Rows[0]["IS_DWELLING_OWNED_BY_OTHER"] != System.DBNull.Value )
			//			{
			//				listItem = cmbIS_DWELLING_OWNED_BY_OTHER.Items.FindByValue(Convert.ToString(dt.Rows[0]["IS_DWELLING_OWNED_BY_OTHER"]));
			//				cmbIS_DWELLING_OWNED_BY_OTHER.SelectedIndex= cmbIS_DWELLING_OWNED_BY_OTHER.Items.IndexOf(listItem);	
			//			}

			if ( dt.Rows[0]["OCCUPANCY"] != System.DBNull.Value )
			{
				listItem = cmbOCCUPANCY.Items.FindByValue(Convert.ToString(dt.Rows[0]["OCCUPANCY"]));

				if (listItem != null)
				{
					cmbOCCUPANCY.SelectedIndex= cmbOCCUPANCY.Items.IndexOf(listItem);	
				}
			}

			if ( dt.Rows[0]["BUILDING_TYPE"] != System.DBNull.Value )
			{
				listItem = cmbBUILDING_TYPE.Items.FindByValue(Convert.ToString(dt.Rows[0]["BUILDING_TYPE"]));
				cmbBUILDING_TYPE.SelectedIndex= cmbBUILDING_TYPE.Items.IndexOf(listItem);	
			}


			//Added By shafi

			//if(hidCalledFrom.Value.ToString().ToUpper()  == "RENTAL" || hidCalledFrom.Value.ToString().ToUpper()  == "Rental")
			//{
			/*	if (dt.Rows[0]["REPAIR_COST"] !=System.DBNull.Value )
				{
					txtREPAIR_COST.Text = Convert.ToDecimal(dt.Rows[0]["REPAIR_COST"]).ToString("N");
					txtREPAIR_COST.Text = txtREPAIR_COST.Text.Substring(0,txtREPAIR_COST.Text.LastIndexOf("."));
				}*/

				if ( dt.Rows[0]["REPLACEMENT_COST"] != System.DBNull.Value )
				{
					txtREPLACEMENT_COST.Text = Convert.ToDecimal(dt.Rows[0]["REPLACEMENT_COST"]).ToString("N");
					txtREPLACEMENT_COST.Text = txtREPLACEMENT_COST.Text.Substring(0,txtREPLACEMENT_COST.Text.LastIndexOf("."));
				}
				
			if (dt.Rows[0]["REPLACEMENTCOST_COVA"] !=  System.DBNull.Value )
			{
				if(dt.Rows[0]["REPLACEMENTCOST_COVA"].ToString()=="10963")
				{
					cbREPLACEMENTCOST_COVA.Checked=true;
				}
				else
				{
					cbREPLACEMENTCOST_COVA.Checked=false;
				}
			}
				//Removed combo
				/*if ( dt.Rows[0]["REPLACEMENT_COST"] != System.DBNull.Value )
				{
					ListItem li=new ListItem(); 
					li=cmbREPLACEMENT_COSTren.Items.FindByValue(  Convert.ToString(dt.Rows[0]["REPLACEMENT_COST"]));
					//listItem=cmbREPLACEMENT_COSTren.Items.FindByValue(Convert.ToString(dt.Rows[0]["REPLACEMENT_COST"]));
				  if(li!=null)
					  li.Selected=true; 
				}*/
			/*}
			else
			{
			   
				if ( dt.Rows[0]["REPLACEMENT_COST"] != System.DBNull.Value )
				{
					txtREPLACEMENT_COST.Text = Convert.ToDecimal(dt.Rows[0]["REPLACEMENT_COST"]).ToString("N");
					txtREPLACEMENT_COST.Text = txtREPLACEMENT_COST.Text.Substring(0,txtREPLACEMENT_COST.Text.LastIndexOf("."));
				}

			}*/
			//if (dt.Rows[0]["
			//			if ( dt.Rows[0]["COMMENTDWELLINGOWNED"] != System.DBNull.Value )
			//			{
			//				txtDwellingOwned.Text = dt.Rows[0]["COMMENTDWELLINGOWNED"].ToString();
			//			}

			

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
		/// 		/// </summary>
		private void InitializeComponent()
		{    
			this.cmbLOCATION_ID.SelectedIndexChanged += new System.EventHandler(this.cmbLOCATION_ID_SelectedIndexChanged);
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int retVal = Save();
			
			hidFormSaved.Value = "1";

			lblMessage.Visible = true;

			//Duplicate location/sublocation
			if ( retVal == -1 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"10");
				hidFormSaved.Value = "2";
				base.OpenEndorsementDetails();
				return;
			}
			
			//Duplicate Client dwelling number
			if ( retVal == -2 )
			{
				
				//lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(this.ScreenId,"10");
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
				
				//Showing the endorsement popup window
				base.OpenEndorsementDetails();

				LoadData();
				
				//Add script to refresh web grid
				//RegisterScript();

				if ( btnDelete.Visible == false ) btnDelete.Visible = true;
				if ( btnCopy.Visible == false ) btnCopy.Visible = true;

				//lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
					
				hidFormSaved.Value = "1";

			}
				
		}
		
		/// <summary>
		/// Saves the record in the database
		/// </summary>	
		/// <returns>-1 for dup LOcation/Sublocation
		///			 -2 for dup Client dwelling number
		/// </returns>
		private int Save()
		{
			Cms.Model.Policy.Homeowners.ClsPolicyDwellingDetailsInfo objNewInfo =new Cms.Model.Policy.Homeowners.ClsPolicyDwellingDetailsInfo();
			ClsDwellingDetails objDwelling = new ClsDwellingDetails();

			objNewInfo.DWELLING_NUMBER = Convert.ToInt32(this.txtDWELLING_NUMBER.Text.Trim());
			objNewInfo.POLICY_ID  = Convert.ToInt32(hidPOLID.Value);
			objNewInfo.POLICY_VERSION_ID  = Convert.ToInt32(hidPOLVersionID.Value);
			objNewInfo.CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);

			if (txtMONTHS_RENTED.Text.Trim().Equals("") == false)
				objNewInfo.MONTHS_RENTED	= Convert.ToInt32(txtMONTHS_RENTED.Text);

			if ( this.cmbBUILDING_TYPE.SelectedItem.Value != "" )
			{
				objNewInfo.BUILDING_TYPE = Convert.ToInt32(this.cmbBUILDING_TYPE.SelectedItem.Value);
			}
			
			//if ( this.cmbIS_DWELLING_OWNED_BY_OTHER.SelectedItem.Value != "" )
			//{
			//objNewInfo.IS_DWELLING_OWNED_BY_OTHER = this.cmbIS_DWELLING_OWNED_BY_OTHER.SelectedItem.Value;
			//}
			
			//if ( this.cmbIS_RENTED_IN_PART.SelectedItem.Value != "" )
			//{
			//	objNewInfo.IS_RENTED_IN_PART = this.cmbIS_RENTED_IN_PART.SelectedItem.Value;
			//}
			
			//if (  this.cmbIS_VACENT_OCCUPY.SelectedItem.Value != "" )
			//{
			//objNewInfo.IS_VACENT_OCCUPY = this.cmbIS_VACENT_OCCUPY.SelectedItem.Value;
			//}
			
			if ( this.cmbLOCATION_ID.Items.Count > 0 )
			{
				//Oct 4,2005: Sumit:The following lines have been commented and a new line at the end has
				// been added. Changes are being made as now only the location id will be used. Procedure to 
				// get the values of location id and sub-location id from the array has been done away with.
				/*string[] arLocSubLoc = this.cmbLOCATION_ID.SelectedItem.Value.Split(",".ToCharArray());

				objNewInfo.LOCATION_ID = Convert.ToInt32(arLocSubLoc[0]);

				if ( arLocSubLoc[1] != "0" )
				{
					objNewInfo.SUB_LOC_ID = Convert.ToInt32(arLocSubLoc[1]);
				}*/
				string[] strLocation=new string[2];
				strLocation=cmbLOCATION_ID.SelectedItem.Value.ToString().Split('^');
				objNewInfo.LOCATION_ID= Convert.ToInt32(strLocation[0]);					
				//objNewInfo.LOCATION_ID= Convert.ToInt32(this.cmbLOCATION_ID.SelectedItem.Value);	
			}
			
			/*
			if ( this.cmbSUB_LOC_ID.Items.Count > 0 )
			{
				objNewInfo.SUB_LOC_ID = Convert.ToInt32(this.cmbSUB_LOC_ID.SelectedItem.Value);
			}
			*/

			if( this.txtMARKET_VALUE.Text.Trim() != "" )
			{
				objNewInfo.MARKET_VALUE = Convert.ToDouble(this.txtMARKET_VALUE.Text.Trim());
			}
			
			//if ( txtNEED_OF_UNITS.Text.Trim() != "" )
			//{
			//	objNewInfo.NEED_OF_UNITS = Convert.ToInt32(txtNEED_OF_UNITS.Text.Trim());
			//}


			//if ( this.cmbNEIGHBOURS_VISIBLE.SelectedItem.Value != "" )
			//{
			objNewInfo.NEIGHBOURS_VISIBLE = this.cmbNEIGHBOURS_VISIBLE.SelectedItem.Value;
			//}
			
			if ( this.txtNO_WEEKS_RENTED.Text.Trim() != "" )
			{
				objNewInfo.NO_WEEKS_RENTED = Convert.ToInt32(this.txtNO_WEEKS_RENTED.Text.Trim());
			}
			
			//if ( this.cmbOCCUPIED_DAILY.SelectedItem.Value!= "" )
			//{
			objNewInfo.OCCUPIED_DAILY = this.cmbOCCUPIED_DAILY.SelectedItem.Value;			
			//}

			//if ( this.cmbOCCUPANCY.SelectedItem.Value!= "" )
			//{
			objNewInfo.OCCUPANCY = Convert.ToInt32(this.cmbOCCUPANCY.SelectedItem.Value);			
			//}

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
          
		
			//			if(cmbIS_DWELLING_OWNED_BY_OTHER.SelectedValue== "Y")
			//				objNewInfo.COMMENT_DWELLING_OWNED =	txtDwellingOwned.Text.Trim() ;
			//			else
			//				objNewInfo.COMMENT_DWELLING_OWNED =	"";
			//Added By Shafi
			//if(hidCalledFrom.Value.ToString().ToUpper()=="RENTAL")
			//{
		/*	if (txtREPAIR_COST.Text.Trim() != "")
			{
				objNewInfo.REPAIR_COST =double.Parse(txtREPAIR_COST.Text.Trim());
			} */
			//Removed combo
			/*if(cmbREPLACEMENT_COSTren.SelectedItem.Value  !="")
//When adding first time, take the custom info from the page itself										

					int len = cmbLOCATION_ID.SelectedItem.Text.Length;
					int start = cmbLOCATION_ID.SelectedItem.Text.IndexOf(", ") - 2;
					//string strAdd = cmbLOCATION_ID.SelectedItem.Text.Substring(start,len - start);
					string strAdd = cmbLOCATION_ID.Items[cmbLOCATION_ID.SelectedIndex].Text;
					hidCustomInfo.Value=";Dwelling # = " + txtDWELLING_NUMBER.Text.Trim() + ";Address = " + strAdd;

					//int intDwellingID = objDwelling.Add(objNewInfo);
					int intDwellingID = objDwelling.Add(objNewInfo,hidCustomInfo.Value,hidCalledFrom.Value.ToString());
								{
				objNewInfo.REPLACEMENT_COST=double.Parse(cmbREPLACEMENT_COSTren.SelectedItem.Value);
			}*/
			if ( this.txtREPLACEMENT_COST.Text.Trim() != "" )
			{
				objNewInfo.REPLACEMENT_COST = Convert.ToDouble(this.txtREPLACEMENT_COST.Text.Trim());
			}
			if (this.cbREPLACEMENTCOST_COVA.Checked)
			{
				objNewInfo.REPLACEMENTCOST_COVA = "10963";
			}
			else
			{
				objNewInfo.REPLACEMENTCOST_COVA = "10964";
			}
			/*}
			else
			{
			   
				if ( this.txtREPLACEMENT_COST.Text.Trim() != "" )
				{
					objNewInfo.REPLACEMENT_COST = Convert.ToDouble(this.txtREPLACEMENT_COST.Text.Trim());
				}
			}*/
			
			objNewInfo.CREATED_BY = Convert.ToInt32(GetUserId());
			objNewInfo.MODIFIED_BY = Convert.ToInt32(GetUserId());
			objNewInfo.IS_ACTIVE="Y";
			
			int intRetVal = 0;

			try
			{
				
				
				//int intDwellingID = objDwelling.Add(objNewInfo);
				//When adding first time, take the custom info from the page itself										

				int len = cmbLOCATION_ID.SelectedItem.Text.Length;
				int start = cmbLOCATION_ID.SelectedItem.Text.IndexOf(", ") - 2;
				//string strAdd = cmbLOCATION_ID.SelectedItem.Text.Substring(start,len - start);
				string strAdd = cmbLOCATION_ID.Items[cmbLOCATION_ID.SelectedIndex].Text;
				hidCustomInfo.Value=";Dwelling # = " + txtDWELLING_NUMBER.Text.Trim() + ";Location = " + strAdd;
				//Add new
				if ( hidDWELLING_ID.Value == "0" )
				{
					int intDwellingID = objDwelling.Add(objNewInfo,hidCustomInfo.Value,hidCalledFrom.Value.ToString());
					
					//int intDwellingID = objDwelling.Add(objNewInfo,hidCalledFrom.Value.ToString().Trim());
					
					/*
					if ( intDwellingID < 0 ) 
					{
						return intDwellingID;
					}
					*/
					intRetVal = intDwellingID;

					if ( intRetVal > 0 )
					{
						hidDWELLING_ID.Value = intDwellingID.ToString();
						
						//For repair cost products, Coverages adjusted message
						if ( hidPolicyType.Value == "HO-2 Repair Cost" || hidPolicyType.Value == "HO-3 Repair Cost" )
						{
							lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","703");
						}
						else
						{
							lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
						}
						
						hidIS_ACTIVE.Value = "Y";
						SetWorkflow();
					}

				}
				else
				{
					objNewInfo.DWELLING_ID = Convert.ToInt32(hidDWELLING_ID.Value);
					
					objNewInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Populate old object
					Cms.Model.Policy.Homeowners.ClsPolicyDwellingDetailsInfo objOldInfo= new Cms.Model.Policy.Homeowners.ClsPolicyDwellingDetailsInfo();
					
					base.PopulateModelObject(objOldInfo,hidOldData.Value);
					int intLoc = 0;
					intLoc = int.Parse(ClsCommon.FetchValueFromXML("LOCATION_ID",hidOldData.Value.ToString()));
					string strLoc = ClsCommon.FetchValueFromXML("LOCATION",hidOldData.Value.ToString());
					string strCustominfo  = "";
					if(intLoc != objNewInfo.LOCATION_ID)
					{
						strCustominfo = ";Location #  From = " + strLoc + ";To  = " + strAdd ;
					}
					
					

					intRetVal = objDwelling.UpdatePol(objOldInfo,objNewInfo,hidCalledFrom.Value.ToString().Trim(),strCustominfo);
					
					/*If Repair Cost has been changed, give message that Coverage/Limits will be updated
					 * Only for repair cost products 
					*/
					
					//For repair cost products, Coverages adjusted message
					if ( hidPolicyType.Value == "11403"   || hidPolicyType.Value == "11193" 
						 || hidPolicyType.Value == "11194"
						|| hidPolicyType.Value == "11290" || hidPolicyType.Value == "11480"
						|| hidPolicyType.Value == "11292" || hidPolicyType.Value == "11482")
					{
						if ( objNewInfo.MARKET_VALUE != objOldInfo.MARKET_VALUE )
						{
							lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","702");
						}
						else
						{
							lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
						}
					}
					else
					{
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
					}
					
					SetWorkflow();
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
		
		/// <summary>
		/// Copies the current record in the table
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			/*
			ClsDwellingDetails objDwelling = new ClsDwellingDetails();
			
			int dwellingID = 0;

			dwellingID = objDwelling.CopyDwellingDetails(Convert.ToInt32(hidCustomerID.Value),
														 Convert.ToInt32(hidAppID.Value),
														 Convert.ToInt32(hidAppVersionID.Value),
														 Convert.ToInt32(hidDWELLING_ID.Value)
														);
			
			if ( dwellingID > 0 )
			{
				lblMessage.Visible = true;
				lblMessage.Text = "Information copied successfully.";

				hidDWELLING_ID.Value = dwellingID.ToString();
				LoadData();

				RegisterScript();
			}
			*/
			

		}
		
		/// <summary>
		/// Adds script to refresh web grid
		/// </summary>
		private void RegisterScript()
		{
			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				string strCode = @"<script>
									RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDWELLING_ID').value)
									</script>";

				ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strCode);

			}

		}

		private void cmbLOCATION_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			if ( this.cmbLOCATION_ID.Items.Count == 0 ) return;

			//int locationID = Convert.ToInt32(this.cmbLOCATION_ID.SelectedItem.Value);
				
			//BindSublocations(locationID);
			
		}

		

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int retVal = Delete();
			
			if ( retVal == 1 )
			{
				//ShowMessage(Cms.CmsWeb.ClsMessages.GetMessage("G","127"));
				//lblMessage.Visible = true;
				//lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					
				
				//added by vj on 19-10-2005
				hidCheckDelete.Value = "1";
				hidFormSaved.Value = "5";
				base.OpenEndorsementDetails();

				/*string strCode = @"<script>
									RefreshWebGrid('1',document.getElementById('hidDWELLING_ID').value)
									</script>";

				Page.RegisterStartupScript("Refresh",strCode);
				
				this.hidDWELLING_ID.Value = "0";*/
			//	ShowMessage(Cms.CmsWeb.ClsMessages.GetMessage("G","127"));
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				trBody.Attributes.Add("style","display:none");
			
				/*
				if (!Page.IsStartupScriptRegistered("Hide"))
				{
					string strCode = @"<script>HideParentTab()</script>";

					Page.RegisterStartupScript("Hide",strCode);

				}*/

			}

		}
		
		/// <summary>
		/// Deletes the current record from the database
		/// </summary>
		/// <returns></returns>
		private int Delete()
		{
			ClsDwellingDetails objDwelling = new ClsDwellingDetails();
			ClsPolicyDwellingDetailsInfo objDwellingDetailsInfo=new ClsPolicyDwellingDetailsInfo();
			base.PopulateModelObject(objDwellingDetailsInfo,hidOldData.Value);
			objDwellingDetailsInfo.MODIFIED_BY=int.Parse(GetUserId());
			int retVal=objDwelling.DeleteDwellingForPolicy(objDwellingDetailsInfo);
			
			/*int  intCustomerID = Convert.ToInt32(hidCustomerID.Value);
			int intAppID = Convert.ToInt32(hidAppID.Value);
			int intAppVersionID = Convert.ToInt32(hidAppVersionID.Value);
			int intDwellingID = Convert.ToInt32(hidDWELLING_ID.Value);

			int retVal = objDwelling.Delete(intCustomerID,intAppID,intAppVersionID,intDwellingID);*/
			SetWorkflow();
			return retVal;
		}
		
		private void SetWorkflow()
		{
			if(base.ScreenId	==	"259_0" || base.ScreenId == "239_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOLID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOLVersionID.Value);
				if((hidDWELLING_ID.Value.Trim() != null) && (hidDWELLING_ID.Value.Trim() !=""))
				{
					if(hidDWELLING_ID.Value != "0")
					{
						myWorkFlow.AddKeyValue("DWELLING_ID",hidDWELLING_ID.Value);
					}
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{				
			ClsDwellingDetails objDwellingDetails=new ClsDwellingDetails();
			
			ClsPolicyDwellingDetailsInfo objDwellingDetailsInfo=new ClsPolicyDwellingDetailsInfo();
			int returnResult=0;
			try
			{
				//Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				//objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				//objStuTransactionInfo.loggedInUserName = GetUserName();
				base.PopulateModelObject(objDwellingDetailsInfo,hidOldData.Value);
				objDwellingDetailsInfo.MODIFIED_BY=int.Parse(GetUserId());
				//objUser =  new ClsUser();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					//objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					//objDwellingDetails.TransactionInfoParams = objStuTransactionInfo;
					//objDwellingDetails.ActivateDeactivate(hidDWELLING_ID.Value,"N");					
					returnResult=objDwellingDetails.ActivateDeactivateDwellings(objDwellingDetailsInfo,"N");
					if(returnResult>0)
					{
						lblMessage.Text =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
						trBody.Attributes.Add("style","display:none");
						base.OpenEndorsementDetails();
					}
				}
				else
				{
					//objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					//objDwellingDetails.TransactionInfoParams = objStuTransactionInfo;
					//objDwellingDetails.ActivateDeactivate(hidDWELLING_ID.Value,"Y");
					returnResult=objDwellingDetails.ActivateDeactivateDwellings(objDwellingDetailsInfo,"Y");
					if(returnResult>0)
					{
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"40");
						hidIS_ACTIVE.Value="Y";
						base.OpenEndorsementDetails();
					}
					else
					{
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21");
					}
				}
				hidFormSaved.Value			=	"1";
				LoadData();
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				//				if(objUser!= null)
				//					objUser.Dispose();
			}
		}		
	}
}
