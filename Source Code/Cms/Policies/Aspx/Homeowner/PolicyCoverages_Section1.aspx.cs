/******************************************************************************************
<Author					: -   shafi
<Start Date				: -	 21 feb 2006
<End Date				: -	
<Description			: -  Coverages Information screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
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
using Cms.Model.Policy;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

/*
 * MICHIGAN
 <option value=""></option>
			<option value="11402">HO-2</option>
			<option value="11403">HO-2 Repair Cost</option>
			<option value="11400">HO-3</option>
			<option value="11409">HO-3 Premier</option>
			<option value="11404">HO-3 Repair Cost</option>
			<option value="11405">HO-4</option>
			<option value="11407">HO-4 Deluxe</option>
			<option value="11401">HO-5</option>
			<option value="11410">HO-5 Premier</option>
			<option value="11406">HO-6</option>		
	<option value="11408">HO-6 Deluxe</option>
	
INDIANA

*/

namespace  Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for Coverages.
	/// </summary>
	public class PolicyCoverages_Section1 :Cms.Policies.policiesbase
	{
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue3;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgCoverages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWBSPOEXIST;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateId; //Added by Charles on 9-Dec-09 for Itrack 6647
		//	protected Cms.CmsWeb.Controls.CmsButton btnReset;
		//protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		//DataTable dtCoverages = null;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		public string calledFrom = "";
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldXml;
		DataSet dsCoverages = null;
		XmlDocument xmldoc=new XmlDocument() ;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolcyType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		StringBuilder sbScript = new StringBuilder();
		protected System.Web.UI.WebControls.Label Label1;
		StringBuilder sbDisableScript = new StringBuilder();
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageA;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidProduct;
		decimal coverageA = 0;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidcbWBSPO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidControlXML;
		private StringBuilder sbCtrlXML = new StringBuilder();

		public decimal replValue = 0;
		public decimal marketValue = 0;
		private int stateID ;
		int product = 0;
		RuleData homeRuleData ;
		private System.DateTime  AppEffectiveDate;
		private int All_Data_Valid;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREP_COST;
		public string strCalledFrom ="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			//hidOldXml.Value ="<Root><Home><coverage COV_ID='438' COV_CODE='OS' Name='B'><dependancy><coverage COV_ID='437' COV_CODE='DWELL' Enable='false' Perc='10' /></dependancy></coverage><coverage COV_ID='439' COV_CODE='EBUSPP' Name='C'><dependancy><coverage COV_ID='437' COV_CODE='EXP' Enable='false' Perc='50' Form='3' /><coverage COV_ID='437' COV_CODE='EXP' Enable='false' Perc='50' Form='2' /><coverage COV_ID='437' COV_CODE='EXP' Enable='false' Perc='70' Form='5' /></dependancy></coverage><coverage COV_ID='440' COV_CODE='LOSUR' Name='D'><dependancy><coverage COV_ID='437' COV_CODE='EXP' Enable='false' Perc='30' /></dependancy></coverage></Home></Root>";

			trError.Visible=false;
			// if called from private passenger automobile, otherwise use if else			
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				hidCalledFrom.Value = calledFrom;
				
			}
//			switch(strCalledFrom.ToUpper())
//			{
//				case "Home":
//				case "HOME" :
					base.ScreenId	=	"239_4";
//					break;
//				case "Rental":
//				case "RENTAL":
//					base.ScreenId	=	"159_6";
//					break;
//				default :
//					base.ScreenId	=	"61_6";
//					break;
//			}
			#endregion



			//	btnReset.Attributes.Add("onClick","javascript:return Reset();");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//btnReset.CmsButtonClass		=	CmsButtonType.Write;
			//btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			btnSave.Attributes.Add("Onclick","SetHO11();");//Added by Charles on 22-Oct-09 for Itrack 6604

//			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
//			btnDelete.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

		

			// Put user code to initialize the page here
			if ( !Page.IsPostBack)
			{

				hidCustomerID.Value	 =  GetCustomerID();
				hidAppID.Value = GetAppID();
				hidAppVersionID.Value = GetAppVersionID();
				hidPOLID.Value =GetPolicyID();
				hidPOLVersionID.Value =GetPolicyVersionID();


				//Get LOB ID on basis of Custmoer id, Application id, Application Version Id
				//hidAPP_LOB.Value = ClsVehicleInformation.GetApplicationLOBID(hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value).ToString();
				
				this.hidREC_VEH_ID.Value = Request.QueryString["DWELLINGID"].ToString();

				//btnReset.Attributes.Add("onClick","javascript:return Reset();");
				
				if (Request.QueryString["PageTitle"] != null)
				{
					lblTitle.Text = Request.QueryString["PageTitle"].ToString();
				}

//				ViewState["CurrentPageIndex"] = 1;
				
				BindGrid(calledFrom);
			}
			//BindGrid(calledFrom);
			SetWorkFlowControl();
		}

		private void BindGrid(string calledFrom)
		{
			ClsHomeCoverages objHome = new ClsHomeCoverages ();
			
			int pageSize = 0;

			if ( System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"] == null )
			{
				pageSize = 10;
			}
			else
			{
				pageSize = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"]);
			}
		
			int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);
			

						
			//Get the relevant coverages
//			switch(calledFrom.ToUpper())
//			{	
//				case "HOME":
					Cms.BusinessLayer.BlApplication.ClsHomeCoverages  objCovInformation = new Cms.BusinessLayer.BlApplication.ClsHomeCoverages();
					dsCoverages=objCovInformation.GetPolicyHomeCoverages(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPOLID.Value),
						Convert.ToInt32(hidPOLVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						"N","S1");

			if(dsCoverages.Tables[2].Rows.Count>0)
			{
				hidPolcyType.Value = dsCoverages.Tables[2].Rows[0]["LOOKUP_VALUE_CODE"].ToString();
				//Added by Charles on 9-Dec-09 for Itrack 6647
				stateID  = Convert.ToInt32(dsCoverages.Tables[2].Rows[0]["STATE_ID"]);
				hidStateId.Value  = stateID.ToString(); //Added till here
			}
			else
			{
				trBody.Attributes.Add("style","display:none");
				lblError.Text = "Policy Type is not entered,Please <a href='#' onclick='LocationPolicy();'>click here</a> ";
				trError.Visible = true;
				return;
			}
//					break;
//			}
			
			//Get the Coverage A Amount
			DataRow[] drCoverageA = dsCoverages.Tables[0].Select("COV_CODE = 'DWELL'");
            
			if ( drCoverageA != null && drCoverageA.Length > 0 )
			{
				if ( drCoverageA[0]["LIMIT_1"] != DBNull.Value )
				{
					this.coverageA = Convert.ToInt32(drCoverageA[0]["LIMIT_1"]);
					decimal perc = 0.10M;

					decimal tenPerc = coverageA * perc;
					//10% of Coverage A
					this.hidCoverageA.Value =Convert.ToInt32(tenPerc).ToString();;
				}
			}
			//Root tag of control xml 
			this.sbCtrlXML.Append("<Root>");

			if(dsCoverages.Tables[4] != null && dsCoverages.Tables[4].Rows.Count >0)
			{
				if(dsCoverages.Tables[4].Rows[0]["REP_COST"] != null)
				{
					hidREP_COST.Value=dsCoverages.Tables[4].Rows[0]["REP_COST"].ToString();
				}
			}


			DataTable dataTable = dsCoverages.Tables[0];
			DataTable dtProduct = this.dsCoverages.Tables[2];
			if ( dtProduct != null )
			{
				if ( dtProduct.Rows.Count > 0 )
				{
					if ( dtProduct.Rows[0]["POLICY_TYPE"] != System.DBNull.Value )
					{
						product = Convert.ToInt32(dtProduct.Rows[0]["POLICY_TYPE"]);
						this.hidProduct.Value = product.ToString();

					}
				}
			}
			hidOldData.Value =  ClsCommon.GetXMLEncoded(dataTable);

			// Get App Effective Date
			AppEffectiveDate=(DateTime)dsCoverages.Tables[6].Rows[0]["APP_EFFECTIVE_DATE"];
			if(dsCoverages.Tables[6].Rows[0]["ALL_DATA_VALID"] != DBNull.Value)
			{
				All_Data_Valid=Convert.ToInt32(dsCoverages.Tables[6].Rows[0]["ALL_DATA_VALID"].ToString());
			
				if(All_Data_Valid == 2)
				{
					lblMessage.Visible=true;
					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("761");
					this.lblMessage.Attributes.Add("style","display:inline");
				}
			}

			////////////////////////////
			//Fetch Rule Data and add script block to page
			homeRuleData = objHome.GetRuleData(product,Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(hidPOLID .Value),
				Convert.ToInt32(hidPOLVersionID .Value),
				Convert.ToInt32(hidREC_VEH_ID .Value),"POLICY");
            if (!ClientScript.IsStartupScriptRegistered("ChangeRule"))
			{
				string strChange = @"<script>" + homeRuleData.OnBlurFunction + homeRuleData.OnClickFunction  + "</script>";

                ClientScript.RegisterStartupScript(this.GetType(),"ChangeRule", strChange);

			}

			//DataBind
			dgCoverages.DataSource = dsCoverages.Tables[0];
			dgCoverages.DataBind();

			//End tag of control XML
			this.sbCtrlXML.Append("</Root>");
			sbCtrlXML.Replace("\\","");
			hidControlXML.Value  =         sbCtrlXML.ToString();

			this.hidROW_COUNT.Value = 	dgCoverages.Items.Count.ToString();

			

		
			///////////////////////////////////////
			///
			RegisterScript();
			
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
			this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//Adding Style to Alternating Item
			e.Item.Attributes.Add("Class","midcolora");
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				CheckBox cbDelete = (CheckBox)e.Item.FindControl("cbDelete");
				HtmlSelect ddlLimit = (HtmlSelect)e.Item.FindControl("ddlLIMIT");
				HtmlSelect ddlDed = (HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE");
				Label lblDeductible = (Label)e.Item.FindControl("lblDeductible");
				Label lblLimit = (Label)e.Item.FindControl("lblLimit");
				TextBox txtbox = (TextBox)e.Item.FindControl("txtDEDUCTIBLE_1_TYPE");
				Label lblNoDeductible = (Label)e.Item.FindControl("lblNoCoverage");
				Label lblNoLimit = (Label)e.Item.FindControl("lblNoCoverageLimit");
				Label lblDEDUCTIBLE_TYPE = (Label)e.Item.FindControl("lblDEDUCTIBLE_TYPE");
				TextBox txtLIMIT = (TextBox)e.Item.FindControl("txtLIMIT");
				

				int product = 0;

				int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"COV_ID"));
				string strCov_code = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"COV_CODE"));
				//Appending Node with previx as value for each Coverage in control XML
				DataGrid dg = (DataGrid)sender;
				string prefix = dg.ID + "__ctl" + (e.Item.ItemIndex + 2).ToString();
				this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code +   "\">" + prefix + "</COV_CODE>");


				RegularExpressionValidator revValidator1 = (RegularExpressionValidator) e.Item.FindControl("revLIMIT_DEDUC_AMOUNT");
				revValidator1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
				revValidator1.ValidationExpression = aRegExpDoublePositiveWithZero;
				
				RegularExpressionValidator revLIMIT = (RegularExpressionValidator) e.Item.FindControl("revLIMIT");
				revLIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
				revLIMIT.ValidationExpression = aRegExpDoublePositiveWithZero;

				CustomValidator csv =  (CustomValidator)e.Item.FindControl("csvLIMIT_DEDUC_AMOUNT");
				CustomValidator csvLimit =  (CustomValidator)e.Item.FindControl("csvLIMIT");

				RequiredFieldValidator rfvLIMIT = (RequiredFieldValidator)e.Item.FindControl("rfvLIMIT");
				RangeValidator rngDWELLING_LIMIT = (RangeValidator)e.Item.FindControl("rngDWELLING_LIMIT");
				RangeValidator rngDEDUCTIBLE = (RangeValidator)e.Item.FindControl("rngDEDUCTIBLE");
				HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidcbDelete");

				
				HtmlSelect ddlAddDed = (HtmlSelect)e.Item.FindControl("ddladdDEDUCTIBLE");
				TextBox txtaddDEDUCTIBLE =(TextBox)e.Item.FindControl("txtaddDEDUCTIBLE");
				Label lblDEDUCTIBLE_AMOUNT = (Label)e.Item.FindControl("lblDEDUCTIBLE_AMOUNT");
				Label lblNoaddDEDUCTIBLE = (Label)e.Item.FindControl("lblNoaddDEDUCTIBLE");


				

				//For firearms, enable custom validator////////
				if ( strCov_code == "EBCCSF" )
				{
					csv.ClientValidationFunction = "FirearmsValidations";
					csv.Enabled = true;
					
				}
				
				//For Reduction, enable Custmo validation
				if ( strCov_code == "REDUC" )
				{
					csvLimit.ClientValidationFunction = "ReductionLimitValidations";
					csvLimit.Enabled = true;
					csvLimit.ErrorMessage = "Coverage C may not be reduced to less than 40% of Coverage A Dwelling Amount.";

				}

				//For Coverage D, rounding off
				if ( strCov_code == "LOSUR" )
				{
					csv.ClientValidationFunction = "CoverageDValidations";
					csv.Enabled = true;
				}
				if ( strCov_code == "EBUSPP" )//Added by Charles on 14-Oct-09 for Itrack 6091
				{
					csv.ClientValidationFunction = "CoverageDValidations";
					csv.Enabled = true;
				}
				//for Coverage ho-50,rounding off
				if ( strCov_code == "EBPPOP" )
				{
					csv.ClientValidationFunction = "CoverageHO50Validations";
					csv.Enabled = true;
				}

				//Loss Assessment Coverage (HO-35) 
				//Credit Card and Depositors Forgery (HO-53) EBICC53
				if ( strCov_code == "LAC" || strCov_code == "EBICC53")
				{
					csv.ClientValidationFunction ="LossAssessment";
					csv.Enabled =true;
				}

				//HO-65 or (HO-211 for HO-5) Coverage C Increased Special Limits - Firearms 
				if ( strCov_code == "EBCCSF" )
				{
					csv.ClientValidationFunction = "CoverageFireArm";
					csv.Enabled = true;
				}
				if (strCov_code == "MIN##")
				{
					/*rngDEDUCTIBLE.MaximumValue ="200000";
					rngDEDUCTIBLE.ErrorMessage ="Maximum Limit is 200,000";
					rngDEDUCTIBLE.Enabled =true;*/
					csv.ClientValidationFunction ="CoverageMinValidation";
					csv.ErrorMessage = "<Br>" + "Mine Subsidence should not be greater than Coverage A or $200,000.";
					csv.Enabled =true;

				}

				////////////////
				///
				//Populate the coverage ranges for each coverage
				DataTable dtRanges = this.dsCoverages.Tables[1];
				DataTable dtProduct = this.dsCoverages.Tables[2];
				
				DataRowView drvItem = (DataRowView)e.Item.DataItem;
				
				string strLimitType = drvItem["LIMIT_TYPE"].ToString();
				string strDedType = drvItem["DEDUCTIBLE_TYPE"].ToString();
				string strAddDedType = drvItem["ADDDEDUCTIBLE_TYPE"].ToString();
				
				if ( strLimitType != "" )
				{
					//ddlLimit.Visible = true;
				}
			
				if ( strDedType != "" )
				{
					ddlDed.Visible = true;
				}

				if ( dtProduct != null )
				{
					if ( dtProduct.Rows.Count > 0 )
					{
						if ( dtProduct.Rows[0]["POLICY_TYPE"] != System.DBNull.Value )
						{
							product = Convert.ToInt32(dtProduct.Rows[0]["POLICY_TYPE"]);
							this.hidProduct.Value = product.ToString();

						}
					}
				}
				
				//HO-6----------
				//Show additional for A and D
				if ( product == 11406 || product == 11196)
				{
					if ( strCov_code == "DWELL" || strCov_code == "LOSUR" )
					{
						lblDEDUCTIBLE_TYPE.Text = "3"; 
					}

					//Show additional for Loss Assessment Coverage (HO-35)
					if ( strCov_code == "LAC")
					{
						lblDEDUCTIBLE_TYPE.Text = "3"; 
					}
				}
				//---------------
				
				//HO-4, HO-4 Deluxe, HO-6, HO-6 Deluxe
				//No deductible for Coverage C
				if ( product == 11405 || product == 11407 || 
					product == 11406 || product == 11408 ||
					product == 11195 || product == 11245 || 
					product == 11196 || product == 11246 	)
				{
					if ( strCov_code == "EBUSPP")
					{
						lblDEDUCTIBLE_TYPE.Text = "0"; 
					}
				}
	


				//HO-6 Deluxe----------
				//Show additional for A
				if ( product == 11408 || product == 11246)
				{
					if ( strCov_code == "DWELL")
					{
						lblDEDUCTIBLE_TYPE.Text = "3"; 
					}

					//Show additional for Loss Assessment Coverage (HO-35)
					if ( strCov_code == "LAC")
					{
						lblDEDUCTIBLE_TYPE.Text = "3"; 
					}
				}
				//---------------
				
				//For HO-5 and HO-5 Premier Michigan, 
				//no DDL for Unscheduled Jewelry, Money, Silverware
				//Indiana - HO 5 only
				if ( product == 11401 || product == 11410 || product == 11149)
				{
					if ( strCov_code == "EBCCSL" || strCov_code == "EBCCSM" || strCov_code == "EBCCSI")
					{
						ddlDed.Visible = false;
					}
				}
				//
				//
				if ( strCov_code == "EBP25")
				{
					hidWBSPOEXIST.Value ="true";
				}
				if ( drvItem["COVERAGE_ID"] != System.DBNull.Value )						
				{
					cbDelete.Checked = true;
					hidCbDelete.Value ="true";
					if(strCov_code=="WBSPO" && hidWBSPOEXIST.Value == "true")
					{
						hidWBSPOEXIST.Value ="EXIST";
					}
				}	
				else
				{
					hidCbDelete.Value ="false";
				}
					
				if ( drvItem["IS_MANDATORY"] != System.DBNull.Value ) 
				{
					if ( drvItem["IS_MANDATORY"].ToString() == "Y" ||  drvItem["IS_MANDATORY"].ToString() == "1")
					{
						cbDelete.Enabled = false;
					}

				}													

				/*
				string xpath="/Root/Home/coverage[@COV_ID='" + intCOV_ID.ToString()  + "']";
				string disable = "";
				*/

				string script="";
				
				//XmlNode a = xmldoc.SelectSingleNode(xpath);
				
				

				/*				
				string disableScript = "DisableItems('" + cbDelete.ClientID + "','" + lblNoLimit.ClientID + "','" + 
					lblNoDeductible.ClientID + "','" + lblLimit.ClientID + "','" + ddlDed.ClientID + "','" + txtbox.ClientID + "','" + prefix + "')"
					;
				*/

				
				
				///On click for Business rules////////////////////////
				///////////////////////////////////////////////////////
				string conditionalScript = "";
				
				//For HO-20 to HO-25 only one can be selected at a time.
				
					conditionalScript = "onCheck('" + cbDelete.ClientID + "')" ;
				
    			string disableScript = "DisableItems('" + cbDelete.ClientID + "')";

				if ( sbDisableScript.ToString() == "" )
				{
					sbDisableScript.Append(disableScript + ";" + conditionalScript);
				}
				else
				{
					sbDisableScript.Append(";" + disableScript + ";" + conditionalScript);
				}

				//Add on click attributes///////////////////////////////////////////////
				if ( script != "" )
				{
					cbDelete.Attributes.Add("onClick","javascript:" + script + ";" + disableScript + ";" + conditionalScript);
				}
				else
				{
					cbDelete.Attributes.Add("onClick","javascript:" + disableScript + ";" + conditionalScript);
				}
				///////////////////////////////////////////
				

				//str = 437,438
				//str = 20,30
				e.Item.Attributes.Add("id","Row_" + intCOV_ID.ToString() );   
				//cbDelete.Attributes.Add("onClick","javascript:OnClickCheck("+(e.Item.ItemIndex+2).ToString()+");EnableDisableControls('" + disable + "',this);");

				
				//To Change the color of row if Coverage is availavle due to GrandFathered
				if(drvItem["EFFECTIVE_FROM_DATE"] != DBNull.Value)
				{
					if(AppEffectiveDate < Convert.ToDateTime( drvItem["EFFECTIVE_FROM_DATE"]))
					{
						//e.Item.Attributes.CssStyle.Add("COLOR","Red");
						e.Item.Attributes.Add("Class","GrandFatheredCoverage");
					}
					else
					{
						e.Item.Attributes.Add("Class","midcolora");
					}
				}
				if( drvItem["EFFECTIVE_TO_DATE"] != DBNull.Value )
				{
					if(AppEffectiveDate > Convert.ToDateTime(drvItem["EFFECTIVE_TO_DATE"]) )
					{
						//e.Item.Attributes.CssStyle.Add("COLOR","Red");
						e.Item.Attributes.Add("Class","GrandFatheredCoverage");
					}
					else
					{
						e.Item.Attributes.Add("Class","midcolora");
					}

				}
				///////Change Colour Logic Ends Here
				
				
				DataView dvLimitsRanges = new DataView(dtRanges);
				DataView dvDedRanges = new DataView(dtRanges);
				DataView dvAddDedRanges = new DataView(dtRanges);

				//dvLimitsRanges = dtRanges.DefaultView;
				//dvDedRanges = dtRanges.DefaultView;

				dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
				dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
				dvAddDedRanges.RowFilter ="COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Addded'";

				//Select the ranges applicable to this Coverage
				DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());
						
				//To Add on click function for HO-34
				if(strCov_code == homeRuleData.OnClickCoverage)
				{
					cbDelete.Attributes.Add("onClick","javascript:" + disableScript + ";" + conditionalScript + ";onDependentClick();");
				}

				switch(strLimitType)
				{
					case "1":
						//Flat
						ddlLimit.Visible = true;

						ClsHomeCoverages.BindDropDown(ddlLimit,dvLimitsRanges,"LIMIT_1_DISPLAY","LIMIT_DEDUC_ID",AppEffectiveDate); 
						ClsCoverages.SelectValueInDropDown(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));

						break;
					case "3":
						//Open
						if ( strCov_code == "REDUC" ||strCov_code == "DWELL" ||strCov_code == "OS" ||strCov_code == "EBUSPP" ||strCov_code == "LOSUR")
						{
							//Ravindra(03-27-2007) Coverage B will be available in HO-4 and HO-6 
							//with Limit not applicable
							if(strCov_code == "OS" && (product == 11406 || product == 11196 || product == 11195 || product == 11405))
							{
								txtLIMIT.Visible = false;
								lblLimit.Visible = true;
								lblLimit.Text  = "N.A";
								ddlLimit.Visible=false;
							}
							else
							{
								txtLIMIT.Visible = true;
								lblLimit.Visible = false;
								ddlLimit.Visible=false;
							}
							if(strCov_code == homeRuleData.BaseCoverage )
							{
								rfvLIMIT.Enabled=true;
								rfvLIMIT.ErrorMessage=homeRuleData.ErrRequired;

								rngDWELLING_LIMIT.Enabled=true;
								rngDWELLING_LIMIT.ErrorMessage=homeRuleData.ErrorMessage;
								rngDWELLING_LIMIT.MinimumValue  = homeRuleData.MinValue;
								rngDWELLING_LIMIT.MaximumValue=homeRuleData.MaxValue;
								txtLIMIT.Attributes.Add("onBlur","javascript:OnCoverageChange();UpdateHo51();UpDateBuliding();SetHO11();");
							}
							else
							{
								rngDWELLING_LIMIT.Enabled=false;
                                //if(strCov_code != "REDUC")
                                //   txtLIMIT.Attributes.Add("readOnly","true");
							}
							
							//Ravindra Coverage B Will be available in HO-4 & HO-6 also but Limit Part is not 
							// applicable i.e. additional of Coverage B can be purchased in these products
							/*if(strCov_code == "OS" && (product == 11406 || product == 11196 || product == 11195 || product == 11405))
							{
								
								cbDelete.Enabled =true;

							}*/
							
						}
						else
						{
							txtLIMIT.Visible = false;
							lblLimit.Visible = true;
							ddlLimit.Visible=false;

						}
						
						break;
					
				}

				switch(strDedType)
				{
					case "1":
						//Flat
						ddlDed.Visible =true;
						ClsHomeCoverages.BindDropDown(ddlDed,dvDedRanges,"LIMIT_1_DISPLAY","LIMIT_DEDUC_ID",AppEffectiveDate);
						ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));

						break;
					case "2":
						//Split
						ddlDed.Visible = true;
						ClsHomeCoverages.BindDropDown(ddlDed,dvDedRanges,"LIMIT_DEDUC_AMOUNT","LIMIT_DEDUC_ID",AppEffectiveDate);
						ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));
						

						break;					
					case "3":
						//Open
						txtbox.Visible = true;
						if(strCov_code == "EBUSPP")
						{
							txtbox.Attributes.Add("onBlur","javascript:UpdateHo50();");

						}
						if(strCov_code == "EBOS40" || strCov_code== "EBOS489" || strCov_code == "EBSS490" || strCov_code=="HO48") //Added "HO48", Charles (4-Dec-09), Itrack 6405
						{
							txtbox.ReadOnly =true;
						}
						//For Mine Subsidence (HO-287)  MIN##
						if(strCov_code =="MIN##")
						{
							txtbox.Attributes.Add("OnBlur","SetMinSubsidence();");
							//txtbox.ReadOnly =true;
						}
						if(strCov_code == "LAC")
						{
							csv.ErrorMessage="Maximum coverage is $24,000";
							
						}
						if(strCov_code == "OS" || strCov_code == "DWELL")
						{
							txtbox.Attributes.Add("onBlur","javascript:UpDateBuliding();");
						}

						break;
					case "0":
						if(strCov_code == "ECOB" || strCov_code == "ECOC")
						{
							lblNoDeductible.Visible=true; 
							lblDeductible.Visible=true;
							lblDeductible.Attributes.Add("display","inline");
							
							//lblDeductible.Text="5%";
						}
						break;
						
				}
				switch(strAddDedType)
				{
					case "1":
						//Flat
						ClsHomeCoverages.BindDropDown(ddlAddDed,dvAddDedRanges,"LIMIT_1_DISPLAY","LIMIT_DEDUC_ID",DateTime.Now); 
						ClsCoverages.SelectValueInDropDown(ddlAddDed,DataBinder.Eval(e.Item.DataItem,"ADDDEDUCTIBLE_ID"));

						break;
					case "4":
						lblDEDUCTIBLE_AMOUNT.Visible =true;
						string lblDed="";
						lblDed=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_TEXT"));
						if(lblDed.Trim() == "" && strCov_code != "MIN##")
						{
							lblDEDUCTIBLE_AMOUNT.Text ="All Peril Deductible";
						}
						break;					
					case "3":
						//Open
						
						//lblDeductible.Visible = false;
						txtaddDEDUCTIBLE.Visible = true;
						//ddlDed.Visible = false;
						break;
					case "0":
						//txtbox.Visible = false;
						lblNoaddDEDUCTIBLE.Visible=true; 
						break;
						
				}
				
			}

			
		}

		private void RegisterScript()
		{
			//if ( this.sbScript.ToString() == "" ) return;

            if (!ClientScript.IsStartupScriptRegistered("Test"))
			{
				string strCode = @"<script>firstTime = true;" + this.sbScript.ToString() + "</script>";
				
				string strDisable = @"<script>function DisableDDL(){" + this.sbDisableScript.ToString() + "} DisableDDL();firstTime = false;</script>" ;

                ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode + strDisable);

			}

			

		}


		
//		private void btnDelete_Click(object sender, System.EventArgs e)
//		{
//			int retVal = Delete();
//			
//			lblMessage.Visible = true;
//
//			if ( retVal == 2 )
//			{
//				BindGrid(calledFrom);
//				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
//				base.OpenEndorsementDetails();
//				SetWorkFlowControl();
//			}
//		}
		
//		private int Delete()
//		{
//			
//			ArrayList alDelete = new ArrayList();
//
//			foreach(DataGridItem dgi in dgCoverages.Items)
//			{
//				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
//				{
//					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");
//
//					if ( cbDelete.Checked )
//					{
//						ClsPolicyCoveragesInfo   objInfo = new ClsPolicyCoveragesInfo();
//
//						objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
//						objInfo.POLICY_VERSION_ID  = Convert.ToInt32(this.hidPOLVersionID.Value);
//						objInfo.POLICY_ID  = Convert.ToInt32(this.hidPOLID.Value);
//						objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
//
//						alDelete.Add(objInfo);
//					}
//
//				}
//			}
//			
//
//			
//			
//			return 1;
//			
//		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblMessage.Visible = true;

			int retVal = Save();
			
			if(retVal > 0)
			{
				if ( Convert.ToInt32(ViewState["TotalRecords"]) == 0 )
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
					
				}
				else
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
				}

				BindGrid(calledFrom);
				base.OpenEndorsementDetails();
				
				SetWorkFlowControl();
				return;
			}

			if ( retVal == -2 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","332");
				//lblMessage.Text = "One of the Coverage code already exists. Please enter another code.";
				return;
			}
		}


		private int Save()
		{
			ArrayList alRecr = new ArrayList();
			//ArrayList alDelete = new ArrayList();

			foreach(DataGridItem dgi in dgCoverages.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");

				
					HtmlSelect ddlLimit = ((HtmlSelect)dgi.FindControl("ddlLimit"));
					HtmlSelect ddlDeductible = ((HtmlSelect)dgi.FindControl("ddlDeductible"));
					TextBox txtbox = ((TextBox)dgi.FindControl("txtDEDUCTIBLE_1_TYPE"));
					TextBox txtLIMIT = ((TextBox)dgi.FindControl("txtLIMIT"));
					Label lblLIMIT = ((Label)dgi.FindControl("lblLIMIT"));
					Label lblCOV_DESC = ((Label)dgi.FindControl("lblCOV_DESC"));
					HtmlInputHidden hidLIMIT = ((HtmlInputHidden)dgi.FindControl("hidLIMIT"));

					HtmlInputHidden hidDEDUCTIBLE = ((HtmlInputHidden)dgi.FindControl("hidDEDUCTIBLE"));
					HtmlSelect ddlAddDed = (HtmlSelect)dgi.FindControl("ddladdDEDUCTIBLE");
					TextBox txtaddDEDUCTIBLE =(TextBox)dgi.FindControl("txtaddDEDUCTIBLE");
					Label lblNoaddDEDUCTIBLE = (Label)dgi.FindControl("lblNoaddDEDUCTIBLE");
					Label lblDEDUCTIBLE_AMOUNT = ((Label)dgi.FindControl("lblDEDUCTIBLE_AMOUNT"));
					System.Web.UI.HtmlControls.HtmlInputHidden hidlbl_DEDUCTIBLE_AMOUNT = (HtmlInputHidden)dgi.FindControl("hidlbl_DEDUCTIBLE_AMOUNT");
					System.Web.UI.HtmlControls.HtmlGenericControl spnDEDUCTIBLE_AMOUNT_TEXT = (HtmlGenericControl)dgi.FindControl("spnDEDUCTIBLE_AMOUNT_TEXT");

					ClsPolicyCoveragesInfo  objInfo = new ClsPolicyCoveragesInfo();	
					
					objInfo.COV_DESC = ClsCommon.EncodeXMLCharacters(lblCOV_DESC.Text.Trim());
					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_VERSION_ID  = Convert.ToInt32(this.hidPOLVersionID.Value);
					objInfo.POLICY_ID  = Convert.ToInt32(this.hidPOLID.Value);
					objInfo.RISK_ID  = Convert.ToInt32(this.hidREC_VEH_ID.Value);
					objInfo.MODIFIED_BY = int.Parse(GetUserId());
					HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidcbDelete");
					
					
					if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
					{
						objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);						
					}
					//INSERT 
					if ( objInfo.COVERAGE_ID == -1 )
					{
						objInfo.ACTION = "I";
					}
					else
					{
						//UPDATE
						objInfo.ACTION = "U";
					}

					/*Water Backup and Sump Pump Overflow (HO-327)  Is Checked Then Fetch The Value from Hidden control*/
					int intWBSPO = Convert.ToInt32(((Label)dgi.FindControl("lblCOV_ID")).Text);
					
					if (hidCbDelete.Value == "true" )
					{
					    objInfo.COVERAGE_CODE_ID = Convert.ToInt32(((Label)dgi.FindControl("lblCOV_ID")).Text);

						string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
						string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;
						string strAddDedType =((Label)dgi.FindControl("lblAddDEDUCTIBLE_TYPE")).Text;
						
						if ( lblLIMIT.Visible == true )
						{
							/*
							if( lblLIMIT.Text.Trim() != "" )
							{
								objInfo.LIMIT_1 = Convert.ToDouble(lblLIMIT.Text.Trim());
							}*/

							if( hidLIMIT.Value.Trim() != "" )
							{
								objInfo.LIMIT_1 = Convert.ToDouble(hidLIMIT.Value.Trim());
							}
						}

						switch(strLimitType)
						{
							case "1":
								//Flat
								if ( ddlLimit.Items.Count > 0 && ddlLimit.Items[ddlLimit.SelectedIndex].Text.Trim() != "" )
								{
									//"100 Excess Medical"
									string amount = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(0,ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
									string text = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
									
									objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);

									//string[] strArr = ddlLimit.SelectedItem.Text.Split(' ',
									if ( amount.Trim() != "" )
									{
										objInfo.LIMIT_1 = Convert.ToDouble(amount);
									}

									objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();
									//objInfo.LIMIT_1 = Convert.ToDouble(ddlLimit.SelectedItem.Text);
								}

								break;
							case "2":
								//Split
								/*
								if ( ddlLimit.Items.Count > 0 && ddlLimit.SelectedItem.Value != "" )
								{
									string[] strValues = ddlLimit.SelectedItem.Text.Split('/');
									objInfo.LIMIT_1 =  Convert.ToDouble(strValues[0]);
									objInfo.LIMIT_2 =  Convert.ToDouble(strValues[1]);
								}*/

								break;
							case "0":
								/*
								if(((Label)dgi.FindControl("lblLIMIT")).Text!="")
								{
									objInfo.LIMIT_1 = Convert.ToDouble(((Label)dgi.FindControl("lblLIMIT")).Text);
								}*/

								if( hidLIMIT.Value.Trim() != "" )
								{
									objInfo.LIMIT_1 = Convert.ToDouble(hidLIMIT.Value.Trim());
								}

								break;
							case "3":
								//Open
								
								if ( txtLIMIT.Visible == true )
								{
									if( txtLIMIT.Text.Trim() != "" )
									{	//Changed:Itrack # 6093 -13 July 2009 -Manoj Rathore 
										objInfo.LIMIT_1 = Convert.ToDouble(txtLIMIT.Text.Trim().Replace("$",""));
										
									}
								}

								
								break;

						}

						switch(strDedType)
						{
							case "1":
								//Flat
								if (ddlDeductible.Items.Count > 0 &&  ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "" )
								{
									if ( ddlDeductible.Visible == true )
									{
										objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);
										objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.Items[ddlDeductible.SelectedIndex].Text);
									}
								}
								break;
							case "2":
								//Split
								if ( ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "" )
								{
									objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);
									string[] strValues = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split('/');
									objInfo.DEDUCTIBLE_1 =  Convert.ToDouble(strValues[0]);
									objInfo.DEDUCTIBLE_2 =  Convert.ToDouble(strValues[1]);
								}

								break;
							case "0":
								if(hidDEDUCTIBLE.Value.ToString().Trim() != "")
									objInfo.DEDUCTIBLE1_AMOUNT_TEXT =   hidDEDUCTIBLE.Value.ToString().Trim();
								break;
							case "3":
								//Open
								if(txtbox.Text.Trim()!="")
									 //Changed:Itrack # 6093 -13 July 2009 -Manoj Rathore 
									objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtbox.Text.Trim().Replace("$",""));
							
								break;
						}
						switch(strAddDedType)
						{
							case "1":
								//Flat
								
								if ( ddlAddDed.Items.Count > 0 && ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Trim() != "" )
								{
									string amount = ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Substring(0,ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.IndexOf(" "));
									string text = ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Substring(ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.IndexOf(" "));
									objInfo.ADDDEDUCTIBLE_ID = Convert.ToInt32(ddlAddDed.Items[ddlAddDed.SelectedIndex].Value);
                                     
	
									//string[] strArr = ddlLimit.SelectedItem.Text.Split(' ',
									if ( amount.Trim() != "" )
									{
										objInfo.DEDUCTIBLE = Convert.ToDouble(amount);
									}
	
									objInfo.DEDUCTIBLE_TEXT  = text.Trim();
								}
								break;
							case "2":
						
								break;					
							case "3":
								//Open
						
								//lblDeductible.Visible = false;
								objInfo.DEDUCTIBLE = Convert.ToDouble(txtaddDEDUCTIBLE.Text.Trim());
								//ddlDed.Visible = false;
								break;
							case "4":
								if(hidlbl_DEDUCTIBLE_AMOUNT.Value.ToString() != "")
								{
									string[] amountl = new string[2];
									amountl= hidlbl_DEDUCTIBLE_AMOUNT.Value.ToString().Split(' ');
									if ( amountl[0].Trim() != "")
									{
										objInfo.DEDUCTIBLE = Convert.ToDouble(amountl[0].ToString());
									}
									if(amountl.Length > 1)
                                        
									{
										objInfo.DEDUCTIBLE_TEXT  = amountl[1].ToString();
									}
								}
								break;
							case "0":
								//txtbox.Visible = false;
								break;	
								
						
						}

						alRecr.Add(objInfo);

					}
					else
					{
						//ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	

						if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							objInfo.ACTION = "D";
							
							/*
							objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
							objInfo.APP_VERSION_ID = Convert.ToInt32(this.hidAppVersionID.Value);
							objInfo.APP_ID = Convert.ToInt32(this.hidAppID.Value);
							objInfo.VEHICLE_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
							objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
							Label lblCOV_DES =  ((Label)dgi.FindControl("lblCOV_DESC"));
							objInfo.COV_DESC = lblCOV_DES.Text.Trim();*/
							alRecr.Add(objInfo);
						
						}
					}
					
					

				}
				
			}
			
			ClsHomeCoverages  objCoverages = new ClsHomeCoverages();
			
			int retVal = 1;
			
			int customerID = Convert.ToInt32(hidCustomerID.Value);
			int polID = Convert.ToInt32(hidPOLID.Value);
			int polVersionID = Convert.ToInt32(hidPOLVersionID.Value);
			int dwellingID = Convert.ToInt32(hidREC_VEH_ID.Value);

			try
			{
						retVal = objCoverages.SaveHomeCoveragesNewForPolicy(alRecr,hidOldData.Value,"S1",customerID,polID,polVersionID,dwellingID,hidCustomInfo.Value);						

			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;

				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;

				}
				return -4;
			}
	
			return retVal;
		}
		


	
		
		/// <summary>
		/// This function shows the proper control on limit conlumn on grid
		/// </summary>
		/// <param name="intCoveId"></param>
		private void ShowProperLimitControl(int intCoveId, DataGridItem item)
		{
			item.FindControl("ddlLIMIT_1_TYPE").Visible = false;
			item.FindControl("lblLIMIT_1_TYPE").Visible = true;
			//item.FindControl("txtLIMIT_1_TYPE").Visible = false;
			//item.FindControl("revLIMIT_1_TYPE").Visible = false;

			//Placing the proper controls for deductible
			item.FindControl("ddlDEDUCTIBLE_1_TYPE").Visible = false;
			item.FindControl("lblDEDUCTIBLE_1_TYPE").Visible = true;
			item.FindControl("txtDEDUCTIBLE_1_TYPE").Visible = false;
			//item.FindControl("revDEDUCTIBLE_1_TYPE").Visible = false;


			if (intCoveId == 0)
				return ;

			System.Xml.XmlDocument objDoc = new System.Xml.XmlDocument();
			string strXML = ClsCoverageDetails.GetCoverageDetailXml(intCoveId);
			objDoc.LoadXml(strXML);
			string strIsLimitApplicable = ClsCommon.GetNodeValue(objDoc, "//IsLimitApplicable");
			string strType = "";

			if (strIsLimitApplicable == "1")
			{
				strType = ClsCommon.GetNodeValue(objDoc, "//LIMIT_TYPE");
				if (strType == "1" || strType == "2")
				{
					//Flat , combobox required
					item.FindControl("ddlLIMIT_1_TYPE").Visible = true;
					item.FindControl("lblLIMIT_1_TYPE").Visible = false;
					//	item.FindControl("txtLIMIT_1_TYPE").Visible = false;
					//item.FindControl("revLIMIT_1_TYPE").Visible = false;

					//Binding the dorp down list
					BindComboBox((DropDownList)item.FindControl("ddlLIMIT_1_TYPE"), intCoveId, "LIMIT");
				}
				else if (strType == "3")
				{
					//Open , text box required
					item.FindControl("ddlLIMIT_1_TYPE").Visible = false;
					item.FindControl("lblLIMIT_1_TYPE").Visible = false;
					//item.FindControl("txtLIMIT_1_TYPE").Visible = true;
					//item.FindControl("revLIMIT_1_TYPE").Visible = true;
					//((RegularExpressionValidator)item.FindControl("revLIMIT_1_TYPE")).ValidationExpression = aRegExpInteger;
					//((RegularExpressionValidator)item.FindControl("revLIMIT_1_TYPE")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "163");
				}
			}

			
			if( ClsCommon.GetNodeValue(objDoc, "//IsDeductApplicable") == "1")
			{
				strType = ClsCommon.GetNodeValue(objDoc, "//DEDUCTIBLE_TYPE");
				if (strType == "1")
				{
					//Flat , text box required
					item.FindControl("ddlDEDUCTIBLE_1_TYPE").Visible = true;
					item.FindControl("lblDEDUCTIBLE_1_TYPE").Visible = false;
					item.FindControl("txtDEDUCTIBLE_1_TYPE").Visible = false;
					//item.FindControl("revDEDUCTIBLE_1_TYPE").Visible = false;

					//Binding the dorp down list
					BindComboBox((DropDownList)item.FindControl("ddlDEDUCTIBLE_1_TYPE"), intCoveId, "Deduct");
				}
				else if (strType == "2")
				{
					//For open text box should be visib;e
					item.FindControl("ddlDEDUCTIBLE_1_TYPE").Visible = false;
					item.FindControl("lblDEDUCTIBLE_1_TYPE").Visible = false;
					item.FindControl("txtDEDUCTIBLE_1_TYPE").Visible = true;
					//item.FindControl("revDEDUCTIBLE_1_TYPE").Visible = true;
					//((RegularExpressionValidator)item.FindControl("revDEDUCTIBLE_1_TYPE")).ValidationExpression = aRegExpInteger;
					//((RegularExpressionValidator)item.FindControl("revDEDUCTIBLE_1_TYPE")).ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "163");
				}
			}
		}
		private void BindComboBox(DropDownList ComboBox, int intCoveId, string type)
		{
			int i;
			DataTable dt = ClsCoverageRange.GetCoverageRangeXml(intCoveId, "N", type,0, 100,out i);
			ComboBox.DataSource = dt;
			ComboBox.DataValueField = "LIMIT_DEDUC_ID";
			ComboBox.DataTextField = "LIMIT_DEDUC_AMOUNT";
			ComboBox.DataBind();

		}

		private void SetWorkFlowControl()
		{		
			
			if(base.ScreenId	==	"44_2" || base.ScreenId == "81_1" || 
				base.ScreenId == "48_1" || base.ScreenId == "61_6" || 
				base.ScreenId == "239_4" || base.ScreenId == "44_2")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOLID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOLVersionID.Value);
				myWorkFlow.AddKeyValue("DWELLING_ID",hidREC_VEH_ID.Value);
				//myWorkFlow.AddKeyValue("COVERAGE_TYPE","'S1'");
					

				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		private void dgSection2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				CheckBox cbDelete2 = (CheckBox)e.Item.FindControl("cbDeleteSec2");
				int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"COV_ID"));
				string strCov_code = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"COV_CODE"));
				DataGrid dg = (DataGrid)sender;
				string prefix = dg.ID + "__ctl" + (e.Item.ItemIndex + 2).ToString();

				this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code +   "\">" + prefix + "</COV_CODE>");
				
				DataRowView drvItem = (DataRowView)e.Item.DataItem;

				if ( drvItem["COVERAGE_ID"] != System.DBNull.Value )						
				{
					cbDelete2.Checked = true;
				}
				
			}
		}

	

		/*	private void btnReset_Click(object sender, System.EventArgs e)
			{
				BindGrid(calledFrom);
			}   */

		

	}
}
