/******************************************************************************************
<Author					: -   Pravesh K Chandel
<Start Date				: -	  14 Jan 2010
<End Date				: -	
<Description			: -  Policy Coverages Information screen
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
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;
using Cms.Model.Policy;   

namespace Cms.Policies.Aspx.Aviation
{
	/// <summary>
	/// Summary description for PolicyVehicleCoverageDetails.
	/// </summary>
	public class PolicyVehicleCoverageDetails : Cms.Policies.policiesbase
	{
		#region Page Controls Declaration 
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblPolicyCaption;
		protected System.Web.UI.WebControls.DataGrid dgPolicyCoverages;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPOLICY_LEVEL_GRID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ROW_COUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidControlXML;
		#endregion

		#region Local Variables Declaration 
		public string calledFrom = "";
		//string polStatus = "";
		//string pageFrom = "";
		DataSet dsCoverages = null;
		//XmlDocument xmldoc = new XmlDocument();
		StringBuilder sbScript = new StringBuilder();
		StringBuilder sbDisableScript = new StringBuilder();
		StringBuilder sbCtrlXML = new StringBuilder();
		string strLOBState = "";
		XmlDocument doc = new XmlDocument();
		//private String strPolicyStatus;
		private System.DateTime  AppEffectiveDate;
		//private int All_Data_Valid;
		int rowCount = 0;
		#endregion 
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region setting screen id
			base.ScreenId	=	"449_1";
			#endregion
			#region setting security Xml 
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			#endregion

			if(Cache["RuleXmlAviation"] == null)
			{
				string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/AviationCoverageRule.xml");
				doc.Load(filePath);

				System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(filePath);
				Cache.Insert("RuleXmlAviation",doc,dep);
			}
			else
			{
				doc=(XmlDocument)Cache["RuleXmlAviation"];
			}
			if ( !Page.IsPostBack)
			{
				hidCustomerID.Value	 =  GetCustomerID();
				hidPolID.Value = GetPolicyID();
				hidPolVersionID.Value = GetPolicyVersionID();
				this.hidVEHICLE_ID.Value = Request.QueryString["VehicleID"].ToString();
				BindGrid(); 
			}
			//SetWorkFlowControl();
		}
		private void BindGrid()
		{
			ClsVehicleCoverages objCoverages = new ClsVehicleCoverages();
			//Get the relevant coverages
			dsCoverages=objCoverages.GetPolicyAviationCoverages( Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPolID.Value),
						Convert.ToInt32(hidPolVersionID.Value),
						Convert.ToInt32(hidVEHICLE_ID.Value),
						"N"
						);
			//Get the state details
			string lob = base.GetLOBString();

			DataTable dtState = dsCoverages.Tables[2];
			
			//Custom info for tran log
			//LoadCustomInfo();
			string state = dtState.Rows[0]["STATE_NAME"].ToString();
			strLOBState = lob + state;
			hidLOBState.Value = strLOBState;
			// Get App Effective Date
			AppEffectiveDate=(DateTime)dsCoverages.Tables[2].Rows[0]["APP_EFF_DATE"];
			//Get Old data XML
			DataTable dataTable = dsCoverages.Tables[0];
			hidOldData.Value =  ClsCommon.GetXMLEncoded(dataTable);

			//Bind Policy level Coverages
			DataView dvPolicyCoverages = new DataView(dsCoverages.Tables[0]);
			//string filter = "COVERAGE_TYPE = 'PL'";
			//dvPolicyCoverages.RowFilter = filter;;

			rowCount = dvPolicyCoverages.Count;
			//Root tag of control xml 
			this.sbCtrlXML.Append("<Root>");

			this.dgPolicyCoverages.DataSource = dvPolicyCoverages;
			dgPolicyCoverages.DataBind();
			
			if (rowCount ==0)  
			{
				dgPolicyCoverages.Attributes.Add("style","display:none");   
				trPOLICY_LEVEL_GRID.Attributes.Add("style","display:none");   
				lblPolicyCaption.Attributes.Add("style","display:none");     
			}
	
			this.sbCtrlXML.Append("</Root>");
			this.hidControlXML.Value = sbCtrlXML.ToString();
			
			hidPOLICY_ROW_COUNT.Value = dgPolicyCoverages.Items.Count.ToString();
			
			RegisterScript();

		}
		private void OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//Adding Style to Alternating Item
			e.Item.Attributes.Add("Class","midcolora");
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				Label lblCOV_ID = (Label)e.Item.FindControl("lblCOV_ID");

				Label lblLIMIT_AMOUNT = (Label)e.Item.FindControl("lblLIMIT_AMOUNT");
				CheckBox cbDelete = (CheckBox)e.Item.FindControl("cbDelete");
				//DropDownList ddlLimit = (DropDownList)e.Item.FindControl("ddlLIMIT");
				HtmlSelect ddlLimit =(HtmlSelect)e.Item.FindControl("ddlLIMIT");
				CustomValidator  csvddlDEDUCTIBLE =(CustomValidator)e.Item.FindControl("csvddlDEDUCTIBLE");

				HtmlSelect ddlDed=(HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE");
				Label lblCOV_DESC =(Label)e.Item.FindControl("lblCOV_DESC");
				HtmlSelect ddlCovDesc =(HtmlSelect)e.Item.FindControl("ddlCOV_DESC");

				Label lblDeductible = (Label)e.Item.FindControl("lblDeductible");
				Label lblLimit = (Label)e.Item.FindControl("lblLimit");
				
				TextBox txtDeductible = (TextBox)e.Item.FindControl("txtDeductible");
				TextBox txtLimit = (TextBox)e.Item.FindControl("txtLimit");
				
				TextBox txtRATE = (TextBox)e.Item.FindControl("txtRATE");
				TextBox txtDescription = (TextBox)e.Item.FindControl("txtDESCRIPTION");
				TextBox txtPremium = (TextBox)e.Item.FindControl("txtPREMIUM");
				Label lblRATE  = (Label)e.Item.FindControl("lblRATE");
				Label lblDESCRIPTION  = (Label)e.Item.FindControl("lblDESCRIPTION");
				Label lblPREMIUM  = (Label)e.Item.FindControl("lblPREMIUM");

				Label lblLIMIT_TYPE  = (Label)e.Item.FindControl("lblLIMIT_TYPE");
				Label lblDED_TYPE  = (Label)e.Item.FindControl("lblDEDUCTIBLE_TYPE");
				Label lblLIMIT_APPL  = (Label)e.Item.FindControl("lblIS_LIMIT_APPLICABLE");
				Label lblDED_APPL  = (Label)e.Item.FindControl("lblIS_DEDUCT_APPLICABLE");
				RegularExpressionValidator revLIMIT = (RegularExpressionValidator)e.Item.FindControl("revLIMIT");
				CustomValidator 	csvddlDEDUCTIBLE1    = (CustomValidator)e.Item.FindControl("csvddlDEDUCTIBLE1");
				
				RegularExpressionValidator revRATE = (RegularExpressionValidator)e.Item.FindControl("revRATE");
				RegularExpressionValidator revPREMIUM = (RegularExpressionValidator)e.Item.FindControl("revPREMIUM");

				
				int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"COV_ID"));
				string strCov_code = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"COV_CODE"));
				
				//Appending Node with previx as value for each Coverage in control XML
				DataGrid dg = (DataGrid)sender;
				string prefix = dg.ID + "__ctl" + (e.Item.ItemIndex + 2).ToString();
				//this.sbCtrlXML.Append("<" + strCov_code + ">" + prefix + "</" + strCov_code + ">");
				this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code +   "\">" + prefix + "</COV_CODE>");

				#region Getting Rules From XML
				//Getting Coverage Rules From XML already loaded in cache	
				//////////////////////////////////////
				///
				//// For Checked = TRUE case
				HtmlInputHidden  hidCHECKD_DISABLE  = (HtmlInputHidden)e.Item.FindControl("hidCHECKDDISABLE");
				HtmlInputHidden  hidCHECKD_ENABLE  = (HtmlInputHidden)e.Item.FindControl("hidCHECKDENABLE");
				HtmlInputHidden  hidCHECKD_SELECT  = (HtmlInputHidden)e.Item.FindControl("hidCHECKDSELECT");
				HtmlInputHidden  hidCHECKD_DSELECT  = (HtmlInputHidden)e.Item.FindControl("hidCHECKDDSELECT");
				if(hidCHECKD_DISABLE != null)
				{
					
					StringBuilder sbXML = new StringBuilder();
					
					XmlNode node = doc.SelectSingleNode("Root/Coverages[@Code='" + strCov_code + "' and @Checked='true' ]");
	
					if ( node != null ) 
					{
						//Fetch Coverages to be disabled
						XmlNode disableNode = node.SelectSingleNode("ToDisable");
						if(disableNode != null)
						{
							XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
							foreach(XmlNode disabledNode in childNodes )
							{
								string covCode = disabledNode.Attributes["Code"].Value;
				
								if ( sbXML.ToString() == "" )
								{
									sbXML.Append(covCode);
								}
								else
								{
									sbXML.Append("," + covCode );
								}
							}
							hidCHECKD_DISABLE.Value =sbXML.ToString();
						}

						//Fetch coverages to be enabled
						sbXML.Remove(0,sbXML.Length);
						disableNode = node.SelectSingleNode("ToEnable");
						if(disableNode != null)
						{
							XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
							foreach(XmlNode disabledNode in childNodes )
							{
								string covCode = disabledNode.Attributes["Code"].Value;
				
								if ( sbXML.ToString() == "" )
								{
									sbXML.Append(covCode);
								}
								else
								{
									sbXML.Append("," + covCode );
								}
							}
							hidCHECKD_ENABLE.Value =sbXML.ToString();
						}
					
						//Fetch coverages to be unchecked
						sbXML.Remove(0,sbXML.Length);
						disableNode = node.SelectSingleNode("ToUncheck");
						if(disableNode != null)
						{
							XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
							foreach(XmlNode disabledNode in childNodes )
							{
								string covCode = disabledNode.Attributes["Code"].Value;
				
								if ( sbXML.ToString() == "" )
								{
									sbXML.Append(covCode);
								}
								else
								{
									sbXML.Append("," + covCode );
								}
							}
							hidCHECKD_DSELECT.Value =sbXML.ToString();
						}
						

						//fetch coverages to be checked
						sbXML.Remove(0,sbXML.Length);
						disableNode = node.SelectSingleNode("ToCheck");
						if(disableNode != null)
						{
							XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
							foreach(XmlNode disabledNode in childNodes )
							{
								string covCode = disabledNode.Attributes["Code"].Value;
				
								if ( sbXML.ToString() == "" )
								{
									sbXML.Append(covCode);
								}
								else
								{
									sbXML.Append("," + covCode );
								}
							}
							hidCHECKD_SELECT.Value =sbXML.ToString();
						}
						

					}
					else
					{
						hidCHECKD_DISABLE.Value ="";
						hidCHECKD_ENABLE.Value="";
						hidCHECKD_DSELECT.Value ="";
						hidCHECKD_SELECT.Value ="";
					}
				}

				//// For Checked = FALSE case
				HtmlInputHidden  hidUNCHECKD_DISABLE  = (HtmlInputHidden)e.Item.FindControl("hidUNCHECKDDISABLE");
				HtmlInputHidden  hidUNCHECKD_ENABLE  = (HtmlInputHidden)e.Item.FindControl("hidUNCHECKDENABLE");
				HtmlInputHidden  hidUNCHECKD_SELECT  = (HtmlInputHidden)e.Item.FindControl("hidUNCHECKDSELECT");
				HtmlInputHidden  hidUNCHECKD_DSELECT  = (HtmlInputHidden)e.Item.FindControl("hidUNCHECKDDSELECT");
				if(hidCHECKD_DISABLE != null)
				{
					
					StringBuilder sbXML = new StringBuilder();
					
					XmlNode node = doc.SelectSingleNode("Root/Coverages[@Code='" + strCov_code + "' and @Checked='false' ]");
	
					if ( node != null ) 
					{
						XmlNode disableNode = node.SelectSingleNode("ToDisable");
						if(disableNode != null)
						{
							XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
							foreach(XmlNode disabledNode in childNodes )
							{
								string covCode = disabledNode.Attributes["Code"].Value;
				
								if ( sbXML.ToString() == "" )
								{
									sbXML.Append(covCode);
								}
								else
								{
									sbXML.Append("," + covCode );
								}
							}
							hidUNCHECKD_DISABLE.Value =sbXML.ToString();
						}

						sbXML.Remove(0,sbXML.Length);
						disableNode = node.SelectSingleNode("ToEnable");
						if(disableNode != null)
						{
							XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
							foreach(XmlNode disabledNode in childNodes )
							{
								string covCode = disabledNode.Attributes["Code"].Value;
				
								if ( sbXML.ToString() == "" )
								{
									sbXML.Append(covCode);
								}
								else
								{
									sbXML.Append("," + covCode );
								}
							}
							hidUNCHECKD_ENABLE.Value =sbXML.ToString();
						}
					
						sbXML.Remove(0,sbXML.Length);
						disableNode = node.SelectSingleNode("ToUncheck");
						if(disableNode != null)
						{
							XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
							foreach(XmlNode disabledNode in childNodes )
							{
								string covCode = disabledNode.Attributes["Code"].Value;
				
								if ( sbXML.ToString() == "" )
								{
									sbXML.Append(covCode);
								}
								else
								{
									sbXML.Append("," + covCode );
								}
							}
							hidUNCHECKD_DSELECT.Value =sbXML.ToString();
						}
						

						sbXML.Remove(0,sbXML.Length);
						disableNode = node.SelectSingleNode("ToCheck");
						if(disableNode != null)
						{
							XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
							foreach(XmlNode disabledNode in childNodes )
							{
								string covCode = disabledNode.Attributes["Code"].Value;
				
								if ( sbXML.ToString() == "" )
								{
									sbXML.Append(covCode);
								}
								else
								{
									sbXML.Append("," + covCode );
								}
							}
							hidUNCHECKD_SELECT.Value =sbXML.ToString();
						}
						

					}
					else
					{
						hidUNCHECKD_DISABLE.Value ="";
						hidUNCHECKD_ENABLE.Value=""; 
						hidUNCHECKD_DSELECT.Value ="";
						hidUNCHECKD_SELECT.Value ="";
					}
				}
				//End of Getting Coverage Rules From XML
				///////////////////////////////////////
				#endregion

				lblCOV_ID.Attributes.Add("style","display:none");
				
				//Populate the coverage ranges for each coverage
				DataTable dtRanges = this.dsCoverages.Tables[1];
				DataTable dtCovTypes = this.dsCoverages.Tables[3];
				
				DataRowView drvItem = (DataRowView)e.Item.DataItem;
			
				//Checks the checkbox if this coverage is selected
				if ( drvItem["COVERAGE_ID"] != System.DBNull.Value )						
				{
					cbDelete.Checked = true;
				}	
				if ( drvItem["IS_MANDATORY"] != System.DBNull.Value )						
				{
					if ( drvItem["IS_MANDATORY"].ToString() == "Y" || drvItem["IS_MANDATORY"].ToString() == "1")
					{
						cbDelete.Enabled = false;
					}
				}
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
				//OnBlur function for Limit text box: Extra Equipment-Comprehensive (A-15) 
				txtLimit.Attributes.Add("onBlur","this.value=formatCurrency(this.value);ValidatorOnChange();");
				txtLimit.Attributes.Add("onchange","calculatePremium('" + cbDelete.ClientID + "');");
				txtRATE.Attributes.Add("onBlur","this.value=formatRate(this.value);ValidatorOnChange();");
				txtRATE.Attributes.Add("onchange","calculatePremium('" + cbDelete.ClientID + "');");

				if( drvItem["ADD_INFORMATION"] != DBNull.Value )
				{
					txtDescription.Text =drvItem["ADD_INFORMATION"].ToString();
				}
				if( drvItem["RATE"] != DBNull.Value )
				{
					txtRATE.Text =drvItem["RATE"].ToString();
				}
				if( drvItem["PREMIUM"] != DBNull.Value )
				{
					txtPremium.Text =drvItem["PREMIUM"].ToString();
				}
				if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
				{
					txtRATE.Attributes.CssStyle.Add("display","none");
					lblRATE.Attributes.CssStyle.Add("display","inline");
					lblRATE.Text="";
					txtDescription.Attributes.CssStyle.Add("display","none");
					lblDESCRIPTION.Attributes.CssStyle.Add("display","inline");
					lblDESCRIPTION.Text="";
					txtPremium.Attributes.CssStyle.Add("display","none");
					lblPREMIUM.Attributes.CssStyle.Add("display","inline");
					lblPREMIUM.Text="";
				}
				//Show this ddl for these coverages:
				//DataView for Limist and deductibles
				DataView dvLimitsRanges = new DataView(dtRanges);
				DataView dvDedRanges = new DataView(dtRanges);
				DataView dvCovTypes = new DataView(dtCovTypes);
				
				//Set Filter for limits and deductibles
				dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
				dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
				dvCovTypes.RowFilter = "COV_ID = " + intCOV_ID.ToString();

				//Select the ranges applicable to this Coverage
				DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());
				
				
				lblLIMIT_TYPE.Attributes.Add("style","display:none");
				lblDED_TYPE.Attributes.Add("style","display:none");
				lblLIMIT_APPL.Attributes.Add("style","display:none");
				lblDED_APPL.Attributes.Add("style","display:none");
				
				//Set up validators, messages, regex etc
				RegularExpressionValidator revLimit = (RegularExpressionValidator) e.Item.FindControl("revLIMIT");
				revLimit.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
				revLimit.ValidationExpression = aRegExpCurrencyformat ;

				revPREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
				revPREMIUM.ValidationExpression = aRegExpCurrencyformat ;
				
				revRATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
				revRATE.ValidationExpression = aRegExpCurrencyformat ;
				revRATE.Enabled =true;
				revRATE.Visible =true;
				revPREMIUM.Enabled=true;
				revPREMIUM.Visible=true;
				if ( drvItem["ISADDDEDUCTIBLE_APP"] != System.DBNull.Value )						
				{
					if (drvItem["ISADDDEDUCTIBLE_APP"].ToString() == "0")
					{
						txtRATE.Attributes.CssStyle.Add("display","none");
						lblRATE.Attributes.CssStyle.Add("display","inline");
						lblRATE.Text="";
						revRATE.Enabled=false;
					}
				}		
				RegularExpressionValidator revDeductible = (RegularExpressionValidator) e.Item.FindControl("revDEDUCTIBLE");
				revDeductible.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
				revDeductible.ValidationExpression = aRegExpCurrencyformat ;
				revDeductible.Enabled =true;
				revDeductible.Visible =true;
				ddlLimit.Attributes.Add("COVERAGE_ID",intCOV_ID.ToString());
				//ddlLimit.Attributes.Add("COVERAGE_CODE",strCov_code);
				if(dvCovTypes.Count>0)
				{
					ClsVehicleCoverages.BindDropDown(ddlCovDesc,dvCovTypes,"COV_TYPE_DESC","COVERAGE_TYPE_ID",AppEffectiveDate);
					if(ddlCovDesc.Items.Count>0) 
					{
						lblCOV_DESC.Attributes.CssStyle.Add("display","none");
						ddlCovDesc.Attributes.CssStyle.Add("display","inline");
						ClsCoverages.SelectValueInDropDown(ddlCovDesc,DataBinder.Eval(e.Item.DataItem,"COVERAGE_TYPE_ID"));
					}
					else
					{
						lblCOV_DESC.Attributes.CssStyle.Add("display","inline");
						ddlCovDesc.Attributes.CssStyle.Add("display","none");
					}
				}
				else
				{
					lblCOV_DESC.Attributes.CssStyle.Add("display","inline");
					ddlCovDesc.Attributes.CssStyle.Add("display","none");
				}
				string strLimitApply = drvItem["IS_LIMIT_APPLICABLE"].ToString();
				string strDedApply = drvItem["IS_DEDUCT_APPLICABLE"].ToString();
				string strAddDedApply = drvItem["ISADDDEDUCTIBLE_APP"].ToString();

				string strLimitType = drvItem["LIMIT_TYPE"].ToString();
				string strDedType = drvItem["DEDUCTIBLE_TYPE"].ToString();
				
				#region Limits
				switch(strLimitType)
				{
					case "1":
						//Flat

						ClsVehicleCoverages.BindDropDown(ddlLimit,dvLimitsRanges,"Limit_1_Display","LIMIT_DEDUC_ID",AppEffectiveDate);
						//Hide the Controls which are not relevant
						txtLimit.Visible=false;
						revLIMIT.Enabled =false;
						revLIMIT.Visible =false;
						if(ddlLimit.Items.Count>0) 
						{
							lblLimit.Attributes.CssStyle.Add("display","none");
						}
						if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
						{
							ddlLimit.Attributes.CssStyle.Add("display","none");
							lblLimit.Attributes.CssStyle.Add("display","inline");
							lblLimit.Text="";
						}

						DataRow[] drDef = dvLimitsRanges.Table.Select("IS_DEFAULT='Y'");
						
						if ( drDef != null && drDef.Length > 0 )
						{
							if ( drDef[0]["LIMIT_1_DISPLAY"] != System.DBNull.Value )
							{
								ListItem li = ddlLimit.Items.FindByText(drDef[0]["LIMIT_1_DISPLAY"].ToString());

								if ( li != null)
								{
									li.Selected = true;
								}
							}
						}

						ClsCoverages.SelectValueInDropDown(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));

						break;
					case "2":
						//Split
						//ClsCommon.SelectValueinDDL(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));
						ClsCoverages.BindDropDown(ddlLimit,dvLimitsRanges,"SplitAmount","LIMIT_DEDUC_ID",AppEffectiveDate);
						
						//Hide the Controls which are not relevant
						txtLimit.Visible=false;
						revLIMIT.Enabled =false;
						revLIMIT.Visible =false;

						if(ddlLimit.Items.Count>0) 
						{
							lblLimit.Attributes.CssStyle.Add("display","none");
						}
						if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
						{
							ddlLimit.Attributes.CssStyle.Add("display","none");
							lblLimit.Attributes.CssStyle.Add("display","inline");
							lblLimit.Text="";
						}
						
						ClsCoverages.SelectValueInDropDown(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));
						break;
					case "0":
						txtLimit.Visible=false;
						revLIMIT.Enabled =false;
						revLIMIT.Visible =false;

						if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
						{
							lblLimit.Text="";
						}
						ddlLimit.Visible=false;
						break;

					case "3":

						//Hide the Controls which are not relevant
						ddlLimit.Visible=false;
						lblLimit.Attributes.CssStyle.Add("display","none");
						if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
						{
							txtLimit.Attributes.CssStyle.Add("display","none");
							lblLimit.Attributes.CssStyle.Add("display","inline");
							lblLimit.Text="";
						}
						
						//Open
						revLIMIT.Enabled =true;
						revLIMIT.Visible =true;
						//Split amount to be shown in text box for UNDSP: Underinsured Motorists (BI Split Limits) 
						if ( DataBinder.Eval(e.Item.DataItem,"LIMIT_1") != System.DBNull.Value )
						{
							txtLimit.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
							//txtLimit.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
						}

						break;
				}
				#endregion
					#region Deductibles
				switch(strDedType)
				{
					case "1":
						//Flat
						ClsCoverages.BindDropDown(ddlDed,dvDedRanges,"Limit_1_Display","LIMIT_DEDUC_ID",AppEffectiveDate);
						//Hide the Controls which are not relevant
						txtDeductible.Visible=false;
						revDeductible.Enabled =false;
						revDeductible.Visible =false;
						if(ddlDed.Items.Count>0) 
						{
							lblDeductible .Attributes.CssStyle.Add("display","none");
						}
						if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
						{
							ddlDed.Attributes.CssStyle.Add("display","none");
							lblDeductible.Attributes.CssStyle.Add("display","inline");
							lblDeductible.Text="";
						}

						ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));

						break;
					case "2":
						//Split
						ClsCoverages.BindDropDown(ddlDed,dvDedRanges,"SplitAmount","LIMIT_DEDUC_ID",AppEffectiveDate);
						//Hide Controls which are not applicable
						txtDeductible.Visible=false;
						revDeductible.Enabled =false;
						revDeductible.Visible =false;

						if(ddlDed.Items.Count>0) 
						{
							lblDeductible .Attributes.CssStyle.Add("display","none");
						}
						if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
						{
							ddlDed.Attributes.CssStyle.Add("display","none");
							lblDeductible.Attributes.CssStyle.Add("display","inline");
							lblDeductible.Text="";
						}

						ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));
						break;
					case "0":
						txtDeductible.Visible=false;
						revDeductible.Enabled =false;
						revDeductible.Visible =false;

						lblDeductible .Attributes.CssStyle.Add("display","none");
						ddlDed.Visible=false;
						break;

					case "3":
						//Open
						
						ddlDed.Visible=false;
						lblDeductible .Attributes.CssStyle.Add("display","none");
						if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
						{
							txtDeductible.Attributes.CssStyle.Add("display","none");
							lblDeductible.Attributes.CssStyle.Add("display","inline");
							lblDeductible.Text="";
						}
						else if(lblDED_APPL.Text=="0")
						{
							txtDeductible.Attributes.CssStyle.Add("display","none");
							lblDeductible.Attributes.CssStyle.Add("display","inline");
							lblDeductible.Text="";
							txtDescription.Attributes.CssStyle.Add("display","none");
							lblDESCRIPTION.Attributes.CssStyle.Add("display","inline");
							lblDESCRIPTION.Text="";
						}
						if ( DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1") != System.DBNull.Value )
						{
							txtDeductible.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1"));
						}
						//TxtDeductible has to shown as lable
//						{
//							txtDeductible.BorderStyle=BorderStyle.None;
//							txtDeductible.ReadOnly=true;
//							txtDeductible.CssClass="midcoloraReadOnlyTextBox";
//							txtDeductible.Font.Bold=true;
//						}
						break;
				}
				#endregion
				#region hide/unhide limit/rate deductibles
				if(strLimitApply=="0" && strDedApply=="0" && strAddDedApply=="0")
				{
					ddlLimit.Attributes.CssStyle.Add("display","none");
					lblLimit.Attributes.CssStyle.Add("display","inline");
					lblLimit.Text="";
					txtLimit.Attributes.CssStyle.Add("display","none");

					ddlDed.Attributes.CssStyle.Add("display","none");
					lblDeductible.Attributes.CssStyle.Add("display","inline");
					lblDeductible.Text="";
					txtDeductible.Attributes.CssStyle.Add("display","none");
					    
					lblPREMIUM.Attributes.CssStyle.Add("display","inline");
					lblPREMIUM.Text="";
					txtPremium.Attributes.CssStyle.Add("display","none");

					lblDESCRIPTION.Attributes.CssStyle.Add("display","inline");
					lblDESCRIPTION.Text="";
					txtDescription.Attributes.CssStyle.Add("display","none");
				}
				#endregion
				//Enable / disable controls////////////////////////////////////
				string disable = "";
				string disable1 = "";
				string script = "";
				
				//Function to check business rules: enable /disable coverages
				script = "onButtonClick(document.getElementById('" + cbDelete.ClientID + "'),'" + rowCount.ToString() + "')";
				
				if (sbScript.ToString() == "" )
				{
					sbScript.Append(script);
				}
				else
				{
					sbScript.Append(";" + script);
				}
				cbDelete.Attributes.Add("IDs",disable);	
				cbDelete.Attributes.Add("Codes",disable1);	
				//Function to enable /disable controls based on Coverage type
				string disableScript = "DisableControls('" + cbDelete.ClientID + "')";
				if ( sbDisableScript.ToString() == "" )
				{
					sbDisableScript.Append(disableScript);
				}
				else
				{
					sbDisableScript.Append(";" + disableScript);
				}
				//Add on click attributes
				if ( script != "" )
				{
					cbDelete.Attributes.Add("onClick","javascript:" + script + ";" + disableScript);
				}
				else
				{
					cbDelete.Attributes.Add("onClick","javascript:" + disableScript);
				}
				e.Item.Attributes.Add("id","Row_" + strCov_code);   
				e.Item.Attributes.Add("code", strCov_code);   
				
			}
		}
		/// <summary>
		/// Handles the Save button event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblMessage.Visible = true;
			
			this.lblMessage.Attributes.Add("style","display:inline");

			int retVal = Save();
			
			if(retVal == 1)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");
				BindGrid();
				base.OpenEndorsementDetails();
				//SetWorkFlowControl();
				return;
			}

			if ( retVal == -2 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","332");
				//lblMessage.Text = "One of the Coverage code already exists. Please enter another code.";
				return;
			}
		}
		/// <summary>
		/// Saves the Coverages
		/// </summary>
		/// <returns></returns>
		private int Save()
		{
			ArrayList alRecr = new ArrayList();
			ArrayList alDelete = new ArrayList();
			
			//Populate the model objects for Policy level and Risk level
			PopulateList(alRecr,this.dgPolicyCoverages);
			//BL class
			ClsVehicleCoverages objCoverages;
			objCoverages = new ClsVehicleCoverages("AVIATION");
			
			int retVal = 1;
			
			try
			{
				retVal = objCoverages.SaveAviationPolicyVehicleCoverages(alRecr,hidOldData.Value,""); //hidCustomInfo.Value);
			}
			catch(Exception ex)
			{
				lblMessage.Visible = true;
				
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;
				}
				
				return -4;
			}
	
			return 1;
		}
		/// <summary>
		/// Poulates the passed in ArrayList with model objects
		/// </summary>
		/// <param name="alRecr"></param>
		/// <param name="dgCoverages"></param>
		private void PopulateList(ArrayList alRecr,DataGrid dgCoverages)
		{
			foreach(DataGridItem dgi in dgCoverages.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					//Get the checkbox
					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");
					HtmlSelect ddlLimit = (HtmlSelect)dgi.FindControl("ddlLimit");
					HtmlSelect ddlDeductible =(HtmlSelect)dgi.FindControl("ddlDeductible");

					HtmlSelect ddlCovDes = (HtmlSelect)dgi.FindControl("ddlCOV_DESC");
					TextBox txtLimit = ((TextBox)dgi.FindControl("txtLimit"));
					TextBox txtRate = ((TextBox)dgi.FindControl("txtRATE"));
					TextBox txtDESCRIPTION = ((TextBox)dgi.FindControl("txtDESCRIPTION"));
					TextBox txtPREMIUM = ((TextBox)dgi.FindControl("txtPREMIUM")); 

					TextBox txtDeductible = ((TextBox)dgi.FindControl("txtDEDUCTIBLE"));

					Label lblCOV_DESC =  ((Label)dgi.FindControl("lblCOV_DESC"));
					Label lblCOV_CODE = ((Label)dgi.FindControl("lblCOV_CODE"));
					Label lblCOV_ID = ((Label)dgi.FindControl("lblCOV_ID"));
					Label lblCOV_TYPE = ((Label)dgi.FindControl("lblCOV_TYPE"));
					
					Label lblAdd_IS_DEDUCT_APPLICABLE =((Label)dgi.FindControl("lblAdd_IS_DEDUCT_APPLICABLE"));
					
					//Model object lblAdd_IS_DEDUCT_APPLICABLE
					ClsPolicyCoveragesInfo objInfo = new ClsPolicyCoveragesInfo();	
					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
					objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
					objInfo.RISK_ID  = Convert.ToInt32(this.hidVEHICLE_ID.Value);
					objInfo.COV_DESC = ClsCommon.EncodeXMLCharacters(lblCOV_DESC.Text.Trim());
					objInfo.COVERAGE_CODE = lblCOV_CODE.Text.Trim();
					objInfo.COVERAGE_CODE_ID = Convert.ToInt32(lblCOV_ID.Text.Trim());
					objInfo.COVERAGE_TYPE = lblCOV_TYPE.Text.Trim();
									
					//int.Parse(GetUserId());
					objInfo.CREATED_BY = int.Parse(GetUserId());
					objInfo.MODIFIED_BY=int.Parse(GetUserId());
					if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
					{
						objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
					}
					int row = dgi.ItemIndex + 2;
					//HtmlGenericControl hidCbDelete= (HtmlGenericControl)dgi.FindControl("hidcbDelete");
					HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidcbDelete");
					bool isChecked = false;

					if ( hidCbDelete.Value  == "true" )
						isChecked = true;
					else
						isChecked = false;

					if ( isChecked )
					{		
						objInfo.COVERAGE_CODE_ID = Convert.ToInt32(
							((Label)dgi.FindControl("lblCOV_ID")).Text
							);

						string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
						string strLimitApply = ((Label)dgi.FindControl("lblIS_LIMIT_APPLICABLE")).Text;
						string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;
						string strDedApply = ((Label)dgi.FindControl("lblIS_DEDUCT_APPLICABLE")).Text;
						string strAddDedApply = ((Label)dgi.FindControl("lblAdd_IS_DEDUCT_APPLICABLE")).Text;
						if(strLimitApply=="0" && strDedApply=="0" && strAddDedApply=="0")	
						{
							//no other information for this Cover type
						}
						else
						{
							objInfo.ADD_INFORMATION =txtDESCRIPTION.Text.Trim();
							if(txtRate.Text.Trim()!="")
								objInfo.RATE =Convert.ToDouble(txtRate.Text.Trim()); 
							if(txtPREMIUM.Text.Trim()!="")
								objInfo.WRITTEN_PREMIUM =Convert.ToDouble(txtPREMIUM.Text.Trim()); 
							if ( ddlCovDes.Items.Count > 0 && ddlCovDes.Items[ddlCovDes.SelectedIndex].Value != "" )
							{
								string strCovtypes = ddlCovDes.Items[ddlCovDes.SelectedIndex].Value;
								objInfo.COVERAGE_TYPE_ID = Convert.ToInt32(strCovtypes);
							}
							else
							{
								objInfo.COVERAGE_TYPE_ID = 0;
							}
							#region populating Limit
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
										if ( amount.Trim() == "Reject")
										{
											objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();
											break;
										}
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
									if ( ddlLimit.Items.Count > 0 && ddlLimit.Items[ddlLimit.SelectedIndex].Value != "" )
									{
										string[] strValues = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Split('/');
										objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);
										if ( strValues[0].Trim() == "Reject" && strValues[1].Trim() == "Reject" )
										{
											objInfo.LIMIT1_AMOUNT_TEXT =  strValues[0].Trim();
											objInfo.LIMIT2_AMOUNT_TEXT =  strValues[0].Trim();
											break;
										}

								
										string[] strLimits1 = strValues[0].Split(' ');
										string[] strLimits2 = strValues[1].Split(' ');

										objInfo.LIMIT_1 =  Convert.ToDouble(strLimits1[0]);
										objInfo.LIMIT1_AMOUNT_TEXT =  strLimits1[1];

										objInfo.LIMIT_2 =  Convert.ToDouble(strLimits2[0]);
										objInfo.LIMIT2_AMOUNT_TEXT =  strLimits2[1];
								
									}

									break;
								case "0":
								case "3":
									//Open
									if ( txtLimit.Text.Trim() != "" )
									{
										string amount = txtLimit.Text.Trim();

										if ( amount.IndexOf("/") == -1 )
										{
											if ( txtLimit.Text.Trim() == "Reject" )
											{
												objInfo.LIMIT1_AMOUNT_TEXT = txtLimit.Text.Trim();
											}
											else
											{
												objInfo.LIMIT_1 = Convert.ToDouble(txtLimit.Text.Trim());
											}
										}
									
										if ( amount.IndexOf("/") != -1 )
										{
											string[] strValues = amount.Split('/');
											
											if ( strValues.Length == 2 )
											{
												if ( strValues[0].Trim() == "Reject" && strValues[1].Trim() == "Reject" )
												{
													objInfo.LIMIT1_AMOUNT_TEXT =   strValues[0].Trim();
													objInfo.LIMIT2_AMOUNT_TEXT =   strValues[1].Trim();
													break;
												}
											
											}

											string[] strLimits1 = strValues[0].Split(' ');
											string[] strLimits2 = strValues[1].Split(' ');
										
											if ( strLimits1[0] != null &&  strLimits1[0] != "")
											{
												objInfo.LIMIT_1 =  Convert.ToDouble(strLimits1[0]);
											}
										
											if ( strLimits1.Length > 1)
											{
												objInfo.LIMIT1_AMOUNT_TEXT =  strLimits1[1];
											}
										
											if ( strLimits2[0] != null &&  strLimits2[0] != "")
											{
												objInfo.LIMIT_2 =  Convert.ToDouble(strLimits2[0]);
											}
										
											if ( strLimits2.Length > 1)
											{
												objInfo.LIMIT2_AMOUNT_TEXT =  strLimits2[1];
											}
										}

									}
									break;
							}
							#endregion
							#region population deductible
							switch(strDedType)
							{
								case "1":
									//Flat
									if (ddlDeductible.Items.Count > 0 &&  ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "" )
									{
									
										string[] strArr = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split(' ');
									
										objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);

										if ( ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Trim() == "Limited" )
										{
											objInfo.DEDUCTIBLE1_AMOUNT_TEXT = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Trim();
											break;
										}

										if ( strArr.Length == 2 )
										{
											objInfo.DEDUCTIBLE_1 = Convert.ToDouble(strArr[0]);
											objInfo.DEDUCTIBLE1_AMOUNT_TEXT = strArr[1];
										}
									
										//objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.SelectedItem.Text);
									}
									break;
								case "2":
									//Split
									if ( ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "" )
									{
										string[] strValues = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split('/');

										string[] strDed1 = strValues[0].Split(' ');
										string[] strDed2 = strValues[1].Split(' ');

										objInfo.DEDUCTIBLE_1 =  Convert.ToDouble(strDed1[0]);
										objInfo.DEDUCTIBLE1_AMOUNT_TEXT =  strDed1[0];

										objInfo.DEDUCTIBLE_2 =  Convert.ToDouble(strDed2[0]);
										objInfo.DEDUCTIBLE2_AMOUNT_TEXT =  strDed2[1];

										objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);
									}

									break;
								case "0":
								case "3":
									//Open
									if ( txtDeductible.Text.Trim() != "" && strDedApply!="0")
									{
										objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtDeductible.Text.Trim());
									}

									break;
							}
							#endregion
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

						alRecr.Add(objInfo);

					}
					else	//checkbox.checked == false
					{
						if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							if ( objInfo.COVERAGE_ID != -1 )
							{
								objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
								objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
								objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
								objInfo.RISK_ID  = Convert.ToInt32(this.hidVEHICLE_ID.Value);
								objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
								Label lblCOV_DES =  ((Label)dgi.FindControl("lblCOV_DESC"));
								objInfo.COV_DESC = lblCOV_DES.Text.Trim();
								//DELETE
								objInfo.ACTION = "D";
								alRecr.Add(objInfo);
								
							}
							
						}
					}
					
				}
				
			}
		}
		private void dgPolicyCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//StringBuilder sbScript = new StringBuilder();
			OnItemDataBound(sender,e);
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.dgPolicyCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPolicyCoverages_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		}
		#endregion
		/// <summary>
		/// Registers javascript code with the page 
		/// </summary>
		private void RegisterScript()
		{
			//if ( this.sbScript.ToString() == "" ) return;

			if (!ClientScript.IsStartupScriptRegistered("Test"))
			{
				string strCode = @"<script>firstTime = true;" + this.sbScript.ToString() + "</script>";
				
				//string strDisable = @"<script>function DisableDDL(){" + this.sbDisableScript.ToString() + "} DisableDDL();firstTime = false;</script>" ; 
				string strDisable = @"<script>firstTime = false;</script>" ; 
				ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode + strDisable);
				//Page.RegisterStartupScript("Test",strCode);

			}
		}
	}
}
