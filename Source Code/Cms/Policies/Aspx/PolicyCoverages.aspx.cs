/******************************************************************************************
<Author					: -   Anurag verma
<Start Date				: -	  04 Nov 05  
<End Date				: -	
<Description			: -  Policy Coverages Information screen
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
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;
using Cms.Model.Policy;   

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyCoverages.
	/// </summary>
	public class PolicyCoverages : Cms.Policies.policiesbase
	{
		#region Page Controls Declaration 
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgCoverages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIsunder25; 
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected System.Web.UI.WebControls.Label lblPolicyCaption;
		protected System.Web.UI.WebControls.DataGrid dgPolicyCoverages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ROW_COUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMotorcycleType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_WATERCRAFT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRejectUMPD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidControlXML;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUseVehicle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleMake;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidA9;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMEDPM2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMIS_COUNT;
		
		
		#endregion 

		#region Local Variables Declaration 
		public string calledFrom = "";
		string polStatus = "";
		string pageFrom = "";
		DataSet dsCoverages = null;
		XmlDocument xmldoc = new XmlDocument();
		StringBuilder sbScript = new StringBuilder();
		StringBuilder sbDisableScript = new StringBuilder();
		string strLOBState = "";
		string strUnderInsuredSplit = "";
		XmlDocument doc = new XmlDocument();
		private int flgMessage;
		//private String strPolicyStatus;
		StringBuilder sbCtrlXML = new StringBuilder();
		private System.DateTime  AppEffectiveDate;
		//private int All_Data_Valid;
		protected System.Web.UI.WebControls.Label lblHealthCare;
		protected System.Web.UI.WebControls.TextBox txtHealthCare;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHealthCare;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHealthCare;
		protected System.Web.UI.WebControls.Label lbltrNoPersons;
		protected System.Web.UI.WebControls.TextBox txtNoPersons;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNoPersons;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNoPersons;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAddInformation;
		protected System.Web.UI.HtmlControls.HtmlTableRow trHealthCare;
		protected System.Web.UI.HtmlControls.HtmlTableRow trNoPersons;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleAmount;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPipValue;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidClaims;
		int intMisSum=0;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidattachUmb;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidComp;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSymbol;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMotorType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCC;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPOLICY_LEVEL_GRID;
		int rowCount = 0;
		#endregion 

		#region Page Load Function 
		private void Page_Load(object sender, System.EventArgs e)
		{
			// if called from private passenger automobile, otherwise use if else
			
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				calledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			switch(calledFrom.ToUpper())
			{
				case "PPA" :
					base.ScreenId	=	"227_1";
					break;
				case "UMB" :
					base.ScreenId	=	"81_1";
					break;
				case "MOT" :
					base.ScreenId	=	"231_1";
					break;
				case "WAT" :
					if(GetLOBString()=="UMB")
					{
						base.ScreenId	=	"83_2";
					}
					else
					{
						base.ScreenId	=	"246_2";
					}
					break;
				default :
					base.ScreenId	=	"44_2";
					break;
			}
			#endregion

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			btnCopy.CmsButtonClass		=	CmsButtonType.Write;
			btnCopy.PermissionString	=	gstrSecurityXML;
		
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			flgMessage=1;
			
			if ( Request.QueryString["PageFrom"] != null )
			{
				pageFrom =  Request.QueryString["PageFrom"].ToString();
			}
			
			hidCustomerID.Value	 =  GetCustomerID();
			hidPolID.Value = GetPolicyID();
			hidPolVersionID.Value = GetPolicyVersionID();

			revNoPersons.ValidationExpression  = aRegExpInteger ;
			revHealthCare.ValidationExpression = aRegExpAlphaNum;;

			/*polStatus=ClsPolicyCoverages.GetPolicyStatus
				(
				hidCustomerID.Value==""?0:int.Parse(hidCustomerID.Value),
				hidPolID.Value==""?0:int.Parse(hidPolID.Value),
				hidPolVersionID.Value==""?0:int.Parse(hidPolVersionID.Value)
				);*/
			polStatus=ClsCoverages.GetPolicyStatus
				(
				hidCustomerID.Value==""?0:int.Parse(hidCustomerID.Value),
				hidPolID.Value==""?0:int.Parse(hidPolID.Value),
				hidPolVersionID.Value==""?0:int.Parse(hidPolVersionID.Value)
				);
			switch(polStatus.ToUpper())
			{
				case "SUSPENDED":
					polStatus="N";
					break;
				case "RENEWAL":
					polStatus="R";
					break;
				default :
					polStatus="B";
					break;
			}
			
			//Fetching CoverageRule XML and putting it in Cache
			if(calledFrom == "PPA")
			{
				if(Cache["RuleXmlAuto"] == null)
				{
					string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/AutoCoverageRule.xml");
					doc.Load(filePath);

					System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(filePath);
					Cache.Insert("RuleXmlAuto",doc,dep);
				}
				else
				{
					doc=(XmlDocument)Cache["RuleXmlAuto"];
				}
			}
			else if(calledFrom == "MOT")
			{
				if(Cache["RuleXmlMotor"] == null)
				{
					string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/MotorCoverageRule.xml");
					doc.Load(filePath);

					System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(filePath);
					Cache.Insert("RuleXmlMotor",doc,dep);
				}
				else
				{
					doc=(XmlDocument)Cache["RuleXmlMotor"];
				}
			}
			//Fetching Coverage Rule XML Ends Here

			if ( !Page.IsPostBack)
			{
				this.hidREC_VEH_ID.Value = Request.QueryString["vehicleid"].ToString();
							
				switch(calledFrom.ToUpper())
				{
					case "PPA" :
					case "MOT" :
						btnCopy.Attributes.Add("onclick",
							"javascript:return OpenPopupWindow('CopyVehicleCoverages.aspx?CalledFrom=" + calledFrom + "&VEHICLE_ID=" + this.hidREC_VEH_ID.Value + "')");
						break;
					case "WAT" :
						btnCopy.Attributes.Add("onclick",
							"javascript:return OpenPopupWindow('Watercrafts/CopyWatercraftCoverages.aspx?CalledFrom=" + calledFrom + "&PageFrom=" + this.pageFrom + "&VEHICLE_ID=" + this.hidREC_VEH_ID.Value + "')");
						break;
					
				}
				if (Request.QueryString["PageTitle"] != null)
				{
					lblTitle.Text = Request.QueryString["PageTitle"].ToString();
				}
				
				BindGrid(calledFrom);
				//For the very first time, if no coverages has been saved
				//Promt user to save the values
//				if(flgMessage == 1)
//				{
//					lblMessage.Visible=true;
//					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("706");
//				}
				//Custom info for tran log
				
				//LoadVehicleDetails();
			}
			SetWorkFlowControl();
            btnSave.Attributes.Add("onclick", "javascript:AssignTextToHid('hidROW_COUNT');AssignTextToHid('hidPOLICY_ROW_COUNT');");
            

		}

		#endregion 

		#region LoadCustomInfo Function 
		/// <summary>
		/// Assigns the Custom info for Tran log
		/// </summary>
		private void LoadCustomInfo()
		{
			string make = "";
			string model = "";
			string insuredNo = "";
			string prefix = "";
			hidVehicleMake.Value ="";
			
			

			//Set the motorcycle type
			if ( dsCoverages.Tables.Count > 3 )
			{
				if ( dsCoverages.Tables[3].Rows.Count > 0 )
				{
					if ( dsCoverages.Tables[3].Rows[0]["MAKE"] != DBNull.Value)
					{
						make = Convert.ToString(dsCoverages.Tables[3].Rows[0]["MAKE"]);
						hidVehicleMake.Value =make;
					}

					
					if ( dsCoverages.Tables[3].Rows[0]["MODEL"] != DBNull.Value )
					{
						model = Convert.ToString(dsCoverages.Tables[3].Rows[0]["MODEL"]);
					}

					if ( dsCoverages.Tables[3].Rows[0]["INSURED_VEH_NUMBER"] != DBNull.Value )
					{
						insuredNo = Convert.ToString(dsCoverages.Tables[3].Rows[0]["INSURED_VEH_NUMBER"]);
					}
				}
			}
			if(model != "" && make != "")
			{
				//DataRow[]  dr=dsCoverages.Tables[3].Select("MAKE='CHEV' and  model like '%CORVETTE%'" );
				DataRow[]  dr=dsCoverages.Tables[3].Select("model in ('CORVETTE','CORVETTE Z06')" );
				foreach(DataRow dr1 in dr)
				{
					hidVehicleMake.Value ="true";
				}
			}
			if(hidVehicleMake.Value != "true")
			{		
				hidVehicleMake.Value ="false";

			}

			if(calledFrom == "PPA")
			{
				prefix = ";Vehicle # = ";
			}
			if(calledFrom == "MOT")
			{
				prefix = ";Motorcycle # = ";
			}

			this.hidCustomInfo.Value = prefix + insuredNo + ";Make = " + make + ";Model = " + model ;
			
		}

		#endregion 
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    			
			this.dgPolicyCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPolicyCoverages_ItemDataBound);
			this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		#region BindGrid Function 
		/// <summary>
		/// Binds the datagrid to the dataset
		/// </summary>
		/// <param name="calledFrom"></param>
		private void BindGrid(string calledFrom)
		{
			ClsVehicleCoverages objCoverages = new ClsVehicleCoverages();
			
			//Get the relevant coverages
			switch(calledFrom.ToUpper())
			{	
				case "MOT":
                case "MTIME" :
						dsCoverages=objCoverages.GetPolicyMotorcycleCoverages( Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPolID.Value),
						Convert.ToInt32(hidPolVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						"N"
						);
					break;

				case "PPA":
				case "VEH":
						dsCoverages=objCoverages.GetPolicyVehicleCoverages( Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPolID.Value),
						Convert.ToInt32(hidPolVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						"N"
						);

					break;
				case "RREC":
				case "HREC":
					Response.Redirect("../../cmsweb/Construction.html");
					break;
				
				default:
					break;

			}
			
			//Get the state details
			string lob = base.GetLOBString();
			DataTable dtState = dsCoverages.Tables[2];
			DataTable dtVehicle= dsCoverages.Tables[3];
			DataTable dtMis=dsCoverages.Tables[4];
			
			if ( dtVehicle != null && dtVehicle.Rows.Count > 0 )
			{
				if ( dtVehicle.Rows[0]["USE_VEHICLE"] != DBNull.Value )
				{
					hidUseVehicle.Value  = Convert.ToString(dtVehicle.Rows[0]["USE_VEHICLE"]);
				}
				if ( dtVehicle.Rows[0]["VEHICLE_TYPE_PER"] != DBNull.Value )
				{
					hidVehicleType.Value  = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE_PER"]);
				}
				if(hidUseVehicle.Value=="11333") // if commercial
				{
					if ( dtVehicle.Rows[0]["VEHICLE_TYPE_COM"] != DBNull.Value )
					{
						hidVehicleType.Value  = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE_COM"]);
					}
				}
				if ( dtVehicle.Rows[0]["AMOUNT"] != DBNull.Value )
				{
					hidVehicleAmount.Value  = Convert.ToString(dtVehicle.Rows[0]["AMOUNT"]);
				}
				if ( dtVehicle.Rows[0]["COMPRH_ONLY"] != DBNull.Value )
				{
					hidComp.Value  = Convert.ToString(dtVehicle.Rows[0]["COMPRH_ONLY"]);
				}
				if ( dtVehicle.Rows[0]["SYMBOL"] != DBNull.Value )
				{
					hidSymbol.Value  = Convert.ToString(dtVehicle.Rows[0]["SYMBOL"]);
				}
				if ( dtVehicle.Rows[0]["MOTORCYCLE_TYPE"] != DBNull.Value )
				{
					hidMotorcycleType.Value  = Convert.ToString(dtVehicle.Rows[0]["MOTORCYCLE_TYPE"]);
				}
				if ( dtVehicle.Rows[0]["VEHICLE_CC"] != DBNull.Value )
				{
					hidCC.Value  = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_CC"]);
				}
				
			}
			if ( dtMis != null && dtMis.Rows.Count > 0 )
			{
				if ( dtMis.Rows[0]["SUM_MIS"] != DBNull.Value )
				{
					intMisSum  = int.Parse(dtMis.Rows[0]["SUM_MIS"].ToString());
				}
				if ( dtMis.Rows[0]["MIS_COUNT"] != DBNull.Value )
				{
					hidMIS_COUNT.Value  = dtMis.Rows[0]["MIS_COUNT"].ToString();
				}
			}

			if(dsCoverages.Tables[5] != null && dsCoverages.Tables[5].Rows.Count > 0)
			{
				if ( dsCoverages.Tables[5].Rows[0]["NO_CLAIMS"] != DBNull.Value )
				{
					hidClaims.Value   = Convert.ToString(dsCoverages.Tables[5].Rows[0]["NO_CLAIMS"].ToString());
				}
			}

			//Get Umbrella attached to policy
			if(dsCoverages.Tables[6] != null && dsCoverages.Tables[6].Rows.Count > 0)
			{
				if ( dsCoverages.Tables[6].Rows[0]["UMB_POL"] != DBNull.Value )
				{
					hidattachUmb.Value   = Convert.ToString(dsCoverages.Tables[6].Rows[0]["UMB_POL"].ToString());
				}
				if ( dsCoverages.Tables[6].Rows[0]["UNDER_25_AGE"] != DBNull.Value )
				{
					hidIsunder25.Value   = Convert.ToString(dsCoverages.Tables[6].Rows[0]["UNDER_25_AGE"].ToString());
				}
			}

			
			//Custom info for tran log
			LoadCustomInfo();
            if (dtState.Rows.Count>0)
            {
                string state = dtState.Rows[0]["STATE_NAME"].ToString();
                strLOBState = lob + state;
            }
			hidLOBState.Value = strLOBState;

			// Get App Effective Date
            if(dsCoverages.Tables[2].Rows.Count>0)
			    AppEffectiveDate=(DateTime)dsCoverages.Tables[2].Rows[0]["APP_EFFECTIVE_DATE"];

			/////
			
			//			//Display/Hide Signature column based on LOB****
			//			if ( hidLOBState.Value == "PPAIndiana" || hidLOBState.Value == "MOTIndiana" || hidLOBState.Value == "MOTMichigan")
			//			{
			//				this.dgPolicyCoverages.Columns[4].Visible = true;
			//			}
			//			else
			//			{
			//				this.dgPolicyCoverages.Columns[4].Visible = false;
			//			}
			//********************************************
			
			//Get Old data XML
			DataTable dataTable = dsCoverages.Tables[0];
			hidOldData.Value =  ClsCommon.GetXMLEncoded(dataTable);

			/*Bind Policy level Coverages
			 * 1. Single Limits Liability CSL (BI and PD) SLL 
			 * 2.Bodily Injury Liability ( Split Limit) BISPL 
			 * 3.Property Damage Liability PD 
			 * 4.Medical Payments MP 
			 * 5.Uninsured Motorists (CSL) PUNCS 
			 * 6.Underinsured Motorists (CSL) UNCSL
			 * 7.Uninsured Motorists (BI Split Limit) PUMSP 
			 * 8.Underinsured Motorists (BI Split Limit) UNDSP 
			 * 9.Uninsured Motorists (PD) (A-21) UMPD 
			*/
			DataView dvPolicyCoverages = new DataView(dsCoverages.Tables[0]);
			string filter = "COVERAGE_TYPE = 'PL'";
			dvPolicyCoverages.RowFilter = filter;;

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
			//////////End of Policy level
			
			//Bind Risk level coverages
			DataView dvRiskCoverages = new DataView(dsCoverages.Tables[0]);
			string riskFilter = "COVERAGE_TYPE = 'RL'";
			dvRiskCoverages.RowFilter = riskFilter;;

			rowCount = dvRiskCoverages.Count;

			dgCoverages.DataSource = dvRiskCoverages;
			dgCoverages.DataBind();
			
			//End tag of control XML
			this.sbCtrlXML.Append("</Root>");
			this.hidControlXML.Value = sbCtrlXML.ToString();
			
			this.hidROW_COUNT.Value = 	dgCoverages.Items.Count.ToString();
			
			hidPOLICY_ROW_COUNT.Value = dgPolicyCoverages.Items.Count.ToString();
			
			//Set the motorcycle type
			if ( dsCoverages.Tables.Count > 3 )
			{
				if ( dsCoverages.Tables[3].Rows.Count > 0 )
				{
					if ( dsCoverages.Tables[3].Rows[0]["MOTORCYCLE_TYPE"] != DBNull.Value )
					{
						this.hidMotorcycleType.Value = Convert.ToString(dsCoverages.Tables[3].Rows[0]["MOTORCYCLE_TYPE"]);
					}
				}
			}
			//End of Mot type

			RegisterScript();
		}

		#endregion 
		
		#region OnItemDataBound Function for Binding Grid
		private void OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//Adding Style to Alternating Item
			e.Item.Attributes.Add("Class","midcolora");
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				Label lblCOV_ID = (Label)e.Item.FindControl("lblCOV_ID");
				DropDownList ddlSigObt = (DropDownList)e.Item.FindControl("ddlSignatureObtained");
				Label lblSigObt = (Label)e.Item.FindControl("lblSigObt");
				Label lblLIMIT_AMOUNT = (Label)e.Item.FindControl("lblLIMIT_AMOUNT");
				CheckBox cbDelete = (CheckBox)e.Item.FindControl("cbDelete");
				//DropDownList ddlLimit = (DropDownList)e.Item.FindControl("ddlLIMIT");
				HtmlSelect ddlLimit =(HtmlSelect)e.Item.FindControl("ddlLIMIT");
				CustomValidator  csvddlDEDUCTIBLE =(CustomValidator)e.Item.FindControl("csvddlDEDUCTIBLE");
				//DropDownList ddlDed = (DropDownList)e.Item.FindControl("ddlDEDUCTIBLE");
				HtmlSelect ddlDed=(HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE");
				Label lblCOV_DESC =(Label)e.Item.FindControl("lblCOV_DESC");

				Label lblDeductible = (Label)e.Item.FindControl("lblDeductible");
				Label lblLimit = (Label)e.Item.FindControl("lblLimit");
				
				TextBox txtDeductible = (TextBox)e.Item.FindControl("txtDeductible");
				TextBox txtLimit = (TextBox)e.Item.FindControl("txtLimit");
				
				
				Label lblLIMIT_TYPE  = (Label)e.Item.FindControl("lblLIMIT_TYPE");
				Label lblDED_TYPE  = (Label)e.Item.FindControl("lblDEDUCTIBLE_TYPE");
				Label lblLIMIT_APPL  = (Label)e.Item.FindControl("lblIS_LIMIT_APPLICABLE");
				Label lblDED_APPL  = (Label)e.Item.FindControl("lblIS_DEDUCT_APPLICABLE");
				RegularExpressionValidator revLIMIT = (RegularExpressionValidator)e.Item.FindControl("revLIMIT");
				CustomValidator 	csvddlDEDUCTIBLE1    = (CustomValidator)e.Item.FindControl("csvddlDEDUCTIBLE1");
				
				
				int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"COV_ID"));
				string strCov_code = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"COV_CODE"));
				
				//Appending Node with previx as value for each Coverage in control XML
				DataGrid dg = (DataGrid)sender;
				string prefix = dg.ID + "_ctl" + (e.Item.ItemIndex + 2).ToString("0#");
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
				
				DataRowView drvItem = (DataRowView)e.Item.DataItem;
				
				ddlSigObt.Visible =false;

				ddlSigObt.Attributes.Add("style","display:none");
				lblSigObt.Attributes.Add("style","display:inline");

				if( drvItem["ISADDDEDUCTIBLE_APP"] != DBNull.Value )
				{
					if(drvItem["ISADDDEDUCTIBLE_APP"].ToString() =="1" )
					{	
						
						ddlSigObt.Visible = true;
						ddlSigObt.Attributes.Add("style","display:none");
						lblSigObt.Attributes.Add("style","display:inline");
						if ( drvItem["SIGNATURE_OBTAINED"] != System.DBNull.Value )						
						{
							string strSigObt = drvItem["SIGNATURE_OBTAINED"].ToString();
 
							ListItem li = ddlSigObt.Items.FindByValue(strSigObt);
							if ( li!= null )
							{
								ddlSigObt.SelectedIndex = ddlSigObt.Items.IndexOf(li);
								ddlSigObt.Attributes.Add("style","display:inline");
								lblSigObt.Attributes.Add("style","display:none");
							}

						}

					}

				}
				

				
				//Popluate the signature obtained drop down according to data in the database
				

				//Checks the checkbox if this coverage is selected
				
				if ( drvItem["COVERAGE_ID"] != System.DBNull.Value )						
				{
					cbDelete.Checked = true;
					//Check if any of these coverages  SLL,RLCSL,BISPL,PD is saved
					//if not than Message has to be diaplayed to prompt user for default values
					if(strCov_code == "SLL" || strCov_code == "RLCSL" || strCov_code == "BISPL" || strCov_code == "PD")
					{
						flgMessage=0;
					}
					if(strCov_code=="RRUM")
					{
						hidA9.Value ="1";
					}
					if(strCov_code=="MEDPM2")
					{
						hidMEDPM2.Value ="1";
					}

					
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
				if ( strCov_code == "EECOMP" )
				{
					txtLimit.Attributes.Add("onBlur","this.value=formatCurrency(this.value);ValidatorOnChange();onLimitChange(this,'" + strCov_code + "');");
				}
				else
				{
					txtLimit.Attributes.Add("onBlur","this.value=formatCurrency(this.value);ValidatorOnChange();");
				}

				if(strCov_code == "PIP")
				{
					if( drvItem["ADD_INFORMATION"] != DBNull.Value )
					{
						txtHealthCare.Text =drvItem["ADD_INFORMATION"].ToString();
					}
				}
				if(strCov_code == "ENO")
				{
					if( drvItem["ADD_INFORMATION"] != DBNull.Value )
					{
						txtNoPersons.Text =drvItem["ADD_INFORMATION"].ToString();
					}
				}
				
				int intClaims = int.Parse(hidClaims.Value.ToString());
				/*
					If the number of Comprehensive Losses in the last 3 years 
					Then Comprehensive Coverage 
					Minimum deductible is  
					$150 if number of claims is 2 
					is $250 if number of claims is 3 
					is $500 if number of claims is 4+  
				*/
				//				if(strCov_code =="OTC" || strCov_code =="COMP")
				//				{
				//					csvddlDEDUCTIBLE1.Enabled=true;
				//					csvddlDEDUCTIBLE1.ClientValidationFunction ="CheckMinmumValue";
				//
				//					if(intClaims <= 1)
				//					{
				//						csvddlDEDUCTIBLE1.Enabled =false;
				//					}
				//					else if(intClaims == 2)
				//					{
				//						csvddlDEDUCTIBLE1.ErrorMessage ="As no of claims are 2, minimum deductible should be '$150'";
				//					}
				//					else if( intClaims == 3)
				//					{
				//						csvddlDEDUCTIBLE1.ErrorMessage ="As no of claims are 3, minimum deductible should be '$250'";
				//					}
				//					else if(intClaims >=4)
				//					{
				//						csvddlDEDUCTIBLE1.ErrorMessage ="As no of claims are greater than or equal to 4, minimum deductible should be '$500'";
				//					}
				//					else
				//					{
				//						csvddlDEDUCTIBLE1.Enabled =false;
				//					}
				//				}
				
				//				if(hidVehicleType.Value == "11336")
				//				{
				//					if(  strCov_code == "COLL")
				//					{
				//						csvddlDEDUCTIBLE.ClientValidationFunction ="MinCollisionComp";
				//						csvddlDEDUCTIBLE.Enabled =true;
				//						csvddlDEDUCTIBLE.Visible =true;
				//
				//					}
				//					if(  (strCov_code ==  "OTC" || strCov_code == "COMP") && Convert.ToDecimal(hidVehicleAmount.Value) > 50000)
				//					{
				//						csvddlDEDUCTIBLE.ClientValidationFunction ="MinCollisionComp";
				//						csvddlDEDUCTIBLE.Enabled =true;
				//						csvddlDEDUCTIBLE.Visible =true;
				//
				//					}
				//				}
				//				if(hidVehicleMake.Value == "true")
				//				{
				//					if( strCov_code == "OTC" || strCov_code == "COLL" || strCov_code =="COMP")
				//					{
				//						csvddlDEDUCTIBLE.ClientValidationFunction ="MinCollisionComp";
				//						csvddlDEDUCTIBLE.Enabled =true;
				//						csvddlDEDUCTIBLE.Visible =true;
				//
				//					}
				//
				//				}
				//*********Signature required ************
				//Hide show Sig obt drop down according to state, LOB and coverage
				//Show this ddl for these coverages:

				if (    hidLOBState.Value == "MOTIndiana" )
				{
					if ( strCov_code == "PUMSP" || strCov_code == "UMPD" || strCov_code == "PUNCS" || strCov_code == "UNCSL")
					{
						ddlSigObt.Attributes.Add("style","display:none");
						ddlSigObt.Visible = true;
					}
					else
					{
						ddlSigObt.Attributes.Add("style","display:none");
						ddlSigObt.Visible = false;
					}
				}

				if ( hidLOBState.Value == "MOTMichigan")
				{
					if ( strCov_code == "MEDPM1")
					{
						ddlSigObt.Attributes.Add("style","display:none");
						ddlSigObt.Visible = true;
					}
					else
					{
						ddlSigObt.Attributes.Add("style","display:none");
						ddlSigObt.Visible = false;
					}

				}
				//*****************************************

				//DataView for Limist and deductibles
				DataView dvLimitsRanges = new DataView(dtRanges);
				DataView dvDedRanges = new DataView(dtRanges);
				
				//Set Filter for limits and deductibles
				dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
				dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
				

				//Select the ranges applicable to this Coverage
				DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());
				
				
				lblLIMIT_TYPE.Attributes.Add("style","display:none");
				lblDED_TYPE.Attributes.Add("style","display:none");
				lblLIMIT_APPL.Attributes.Add("style","display:none");
				lblDED_APPL.Attributes.Add("style","display:none");
				
				//Set up validators, messages, regex etc
				RegularExpressionValidator revLimit = (RegularExpressionValidator) e.Item.FindControl("revLIMIT");
				revLimit.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
				/*Modified by Asfa (17-July-2008) - iTrack #4443
				revLimit.ValidationExpression = aRegExpDoublePositiveWithZero;
				*/
				revLimit.ValidationExpression = aRegExpCurrencyformat ;
				
				RegularExpressionValidator revDeductible = (RegularExpressionValidator) e.Item.FindControl("revDEDUCTIBLE");
				revDeductible.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
				/*Modified by Asfa (17-July-2008) - iTrack #4443
				revDeductible.ValidationExpression = aRegExpDoublePositiveWithZero;
				*/
				revDeductible.ValidationExpression = aRegExpCurrencyformat ;

				ddlLimit.Attributes.Add("COVERAGE_ID",intCOV_ID.ToString());
				//ddlLimit.Attributes.Add("COVERAGE_CODE",strCov_code);

				string strLimitApply = drvItem["IS_LIMIT_APPLICABLE"].ToString();
				string strDedApply = drvItem["IS_DEDUCT_APPLICABLE"].ToString();
				
				string strLimitType = drvItem["LIMIT_TYPE"].ToString();
				string strDedType = drvItem["DEDUCTIBLE_TYPE"].ToString();

				//Limits
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
							lblLimit.Text="No Coverages";
						}

						/*ddlLimit.DataTextField = "Limit_1_Display";
						ddlLimit.DataValueField = "LIMIT_DEDUC_ID";
						ddlLimit.DataSource = dvLimitsRanges;
						ddlLimit.DataBind();
						*/
						

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

						/*ClsCommon.SelectValueinDDL(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));*/
						ClsCoverages.SelectValueInDropDown(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));

						//						if ( hidLOBState.Value == "PPAMichigan" && strCov_code =="PUNCS")
						//						{
						//						   ddlLimit.Disabled =true;
						//						}

						break;
					case "2":
						//Split
						/*ddlLimit.DataTextField = "SplitAmount";
						ddlLimit.DataValueField = "LIMIT_DEDUC_ID";
						ddlLimit.DataSource = dvLimitsRanges;
						
						ddlLimit.DataBind();*/
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
							lblLimit.Text="No Coverages";
						}
						
						ClsCoverages.SelectValueInDropDown(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));
						break;
					case "0":
						txtLimit.Visible=false;
						revLIMIT.Enabled =false;
						revLIMIT.Visible =false;
						if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )
						{
							lblLimit.Text="No Coverages";
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
							lblLimit.Text="No Coverages";
						}
						
						//Open
						//For Part B - Property Protection Insurance, show Label with 1 million, 10,00000
						//Extra Equipment-Comprehensive Deductible 300, 301
						//Part C - Underinsured Motorists (BI Split Limit) (M-16)
						//302		EECOLL	Extra Equipment-Collision type Deductible
						//303		EECOLL	Extra Equipment-Collision type Deductible
						//Underinsured Motorists (CSL) 
						//Underinsured Motorists (BI Split Limits) 
						//Medical Payments -2 Party 
						revLIMIT.Enabled =true;
						revLIMIT.Visible =true;
						if ( intCOV_ID == 117 || intCOV_ID == 50 || intCOV_ID == 252 || 
							intCOV_ID == 302 || intCOV_ID ==  303 || intCOV_ID == 34 || intCOV_ID ==  121 || intCOV_ID == 214 ||
							intCOV_ID == 14 || intCOV_ID ==  304 || intCOV_ID ==  133 || intCOV_ID==843 || intCOV_ID ==300 || intCOV_ID==301 || intCOV_ID==118 )
						{
							txtLimit.BorderStyle = BorderStyle.None;
							txtLimit.ReadOnly = true;
							txtLimit.CssClass = "midcoloraReadOnlyTextBox";
							txtLimit.Font.Bold = true;
							revLIMIT.Enabled = false;
							//revLIMIT.Visible = false;
						}
						///
						if(strCov_code == "EECOMP")
						{
							if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )						
							{
								if(intMisSum !=0)
								{
									txtLimit.Text = String.Format("{0:,#,###}",intMisSum);
								}
							}
						}
						/// Added By Ravindra(4-26-2006)
						//Medical Payments -2 Party --Michigan
						if ( intCOV_ID == 843)
						{
							//If no record saved
							if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )						
							{
								txtLimit.Text = "1,000";
							}
							else
							{
								txtLimit.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
							}
							break;
						}
						///////////////////////////

						//Split amount to be shown in text box for UNDSP: Underinsured Motorists (BI Split Limits) 
						if ( intCOV_ID == 34 || intCOV_ID == 121 || intCOV_ID == 214 )
						{
							txtLimit.Text  = strUnderInsuredSplit;

							string strSplitAmount = "";

							if ( DataBinder.Eval(e.Item.DataItem,"LIMIT_1") != System.DBNull.Value &&
								DataBinder.Eval(e.Item.DataItem,"LIMIT_2") != System.DBNull.Value 
								) 
							{
								string limit1Amt = "";
								string limit2Amt = "";
	
								if ( DataBinder.Eval(e.Item.DataItem,"LIMIT1_AMOUNT_TEXT") != System.DBNull.Value)
								{
									if  ( Convert.ToString(DataBinder.Eval(e.Item.DataItem,"LIMIT1_AMOUNT_TEXT")) != "" )
									{
										limit1Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"LIMIT1_AMOUNT_TEXT"));
									}
								}
							
								if ( DataBinder.Eval(e.Item.DataItem,"LIMIT2_AMOUNT_TEXT") != System.DBNull.Value)
								{
									if  ( Convert.ToString(DataBinder.Eval(e.Item.DataItem,"LIMIT2_AMOUNT_TEXT")) != "" )
									{
										limit2Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"LIMIT2_AMOUNT_TEXT"));
									}
								}

								strSplitAmount = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"LIMIT_1")) + 
									" " + limit1Amt + 
									"/" + 
									String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"LIMIT_2")) + 
									" " + limit2Amt 
								
									;
								
								txtLimit.Text = strSplitAmount;
								
							}

							if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )						
							{
								//Default value
								txtLimit.Text = "25 /50000";
							}
							
							break;
						}

						//Default aamount for Part C - Underinsured Motorists (CSL)
						if ( intCOV_ID == 14 || intCOV_ID == 304 || intCOV_ID == 133 )
						{
							if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )						
							{
								//Default value
								txtLimit.Text = "50,000";
							}
							else
							{
								txtLimit.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
							}
							break;
						}
						
						//Part B - Property Protection Insurance
						if ( intCOV_ID == 117 )
						{
							//If no record saved
							if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )						
							{
								txtLimit.Text = "1,000,000";
							}
							else
							{
								txtLimit.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
							}
							break;
						}
						
						//$200 Sound Reproducing - Tapes (A-29)
						if ( intCOV_ID == 50 || intCOV_ID == 252 )
						{
							//If no record saved
							if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )						
							{
								txtLimit.Text = "200";
							}
							else
							{
								txtLimit.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
							}
							break;
						}

						if ( DataBinder.Eval(e.Item.DataItem,"LIMIT_1") != System.DBNull.Value )
						{
							txtLimit.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
							//txtLimit.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
						}

						break;
				}
				
				//Deductibles
				switch(strDedType)
				{
					case "1":
						//Flat
						/*ddlDed.DataTextField = "Limit_1_Display";
						ddlDed.DataValueField = "LIMIT_DEDUC_ID";
					
						ddlDed.DataSource = dvDedRanges;
						ddlDed.DataBind();
						
						ClsCommon.SelectValueinDDL(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));*/

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
							lblDeductible.Text="No Coverages";
						}

						ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));

						break;
					case "2":
						//Split
						/*ddlDed.DataTextField = "SplitAmount";
						ddlDed.DataValueField = "LIMIT_DEDUC_ID";
					
						ddlDed.DataSource = dvDedRanges;
						ddlDed.DataBind();
						ClsCommon.SelectValueinDDL(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));*/
						
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
							lblDeductible.Text="No Coverages";
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
							lblDeductible.Text="No Coverages";
						}
						
						if ( DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1") != System.DBNull.Value )
						{
							txtDeductible.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1"));
						}
						//Ravindra(04-21-2006)
						//For Personal Injury Protection & Medical Payments -2 Party --Michigan
						//TxtDeductible has to shown as lable
						if(intCOV_ID == 116 || intCOV_ID == 843 ||  intCOV_ID == 997 ||  intCOV_ID == 118 || strCov_code== "SPA8" || strCov_code=="EBM49" ||  strCov_code=="CEBM49") 
						{
							txtDeductible.BorderStyle=BorderStyle.None;
							txtDeductible.ReadOnly=true;
							txtDeductible.CssClass="midcoloraReadOnlyTextBox";
							txtDeductible.Font.Bold=true;
						}
						
						/// Added By Ravindra(4-26-2006)
						//Medical Payments -2 Party --Michigan
						if ( intCOV_ID == 843)
						{
							//If no record saved
							if ( drvItem["COVERAGE_ID"] == System.DBNull.Value )						
							{
								txtDeductible.Text = "50";
							}
							else
							{
								txtDeductible.Text = String.Format("{0:,#,###}",DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1"));
							}
						}
						break;
						///////////////////////////
				}
				
				

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
				
				
				//////////////////////////////////////////////////////////////
				
			}
		}

		#endregion
		

		/// <summary>
		/// Executed for each item of the datgrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//StringBuilder sbScript = new StringBuilder();
			OnItemDataBound(sender,e);
		}

		

		/// <summary>
		/// Fired for each row of Policy level coverages
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgPolicyCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//StringBuilder sbScript = new StringBuilder();
			
			OnItemDataBound(sender,e);

			
		}


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

		
		
		/// <summary>
		/// Deletes the Coverages from database
		/// </summary>
		/// <returns></returns>
		private int Delete()
		{
			
			ArrayList alDelete = new ArrayList();

			foreach(DataGridItem dgi in dgCoverages.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");

					if ( cbDelete.Checked )
					{
						ClsCoveragesInfo objInfo = new ClsCoveragesInfo();

						objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
						objInfo.APP_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
						objInfo.APP_ID = Convert.ToInt32(this.hidPolID.Value);
						objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);

						alDelete.Add(objInfo);
					}

				}
			}
			

			
			
			return 1;
			
		}

		
		/// <summary>
		/// Handles the Delete button event
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

		

		private void PopulateList(ArrayList alRecr,DataGrid dgCoverages)
		{
			//Vehicle Level Coverages
			foreach(DataGridItem dgi in dgCoverages.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					//Get the checkbox
					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");

			        //_hidLIMIT
                    HtmlInputHidden hidLIMIT = (HtmlInputHidden)dgi.FindControl("hidLIMIT");
                    //DropDownList ddlLimit = ((DropDownList)dgi.FindControl("ddlLimit"));
					//DropDownList ddlDeductible = ((DropDownList)dgi.FindControl("ddlDeductible"));

					HtmlSelect ddlLimit = ((HtmlSelect)dgi.FindControl("ddlLimit"));
					HtmlSelect ddlDeductible = ((HtmlSelect)dgi.FindControl("ddlDeductible"));

					DropDownList ddlSignatureObtained = ((DropDownList)dgi.FindControl("ddlSignatureObtained"));

					TextBox txtLimit = ((TextBox)dgi.FindControl("txtLimit"));
					TextBox txtDeductible = ((TextBox)dgi.FindControl("txtDEDUCTIBLE"));

					Label lblCOV_DESC =  ((Label)dgi.FindControl("lblCOV_DESC"));
					Label lblCOV_CODE = ((Label)dgi.FindControl("lblCOV_CODE"));
					Label lblCOV_ID = ((Label)dgi.FindControl("lblCOV_ID"));
					Label lblCOV_TYPE = ((Label)dgi.FindControl("lblCOV_TYPE"));
					
					HtmlInputHidden lblSIG_OB = (HtmlInputHidden)dgi.FindControl("hiddSigObt");
					Label lblAdd_IS_DEDUCT_APPLICABLE =((Label)dgi.FindControl("lblAdd_IS_DEDUCT_APPLICABLE"));

					Cms.Model.Policy.ClsPolicyCoveragesInfo objInfo = new Cms.Model.Policy.ClsPolicyCoveragesInfo();	

					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
					objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
					objInfo.RISK_ID= Convert.ToInt32(this.hidREC_VEH_ID.Value);
					objInfo.COV_DESC = ClsCommon.EncodeXMLCharacters(lblCOV_DESC.Text.Trim());
					objInfo.COVERAGE_CODE = lblCOV_CODE.Text.Trim();
					objInfo.COVERAGE_CODE_ID = Convert.ToInt32(lblCOV_ID.Text.Trim());

					//
					//objInfo.SIGNATURE_OBTAINED=ddlSignatureObtained.SelectedItem.Value;
					//
					if (lblAdd_IS_DEDUCT_APPLICABLE.Text == "1" && lblSIG_OB.Value  == "1" )
					{
						objInfo.SIGNATURE_OBTAINED=ddlSignatureObtained.SelectedItem.Value;
						//objInfo.SIGNATURE_OBTAINED=ddlSignatureObtained.Items[ddlSignatureObtained.SelectedIndex].Text.Trim();
					}
					else
					{
						objInfo.SIGNATURE_OBTAINED=null;
					}


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
					{
						isChecked = true;
					}
					else
					{
						isChecked = false;
					}

					if ( isChecked )
					{		
						objInfo.COVERAGE_CODE_ID = Convert.ToInt32(
							((Label)dgi.FindControl("lblCOV_ID")).Text
							);

						string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
						string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;
						if(lblCOV_CODE.Text.ToString().Trim() == "PIP")
						{
							if(hidPipValue.Value == "true")
							{
								objInfo.ADD_INFORMATION = txtHealthCare.Text.Trim();
							}
						}

						if(lblCOV_CODE.Text.ToString().Trim() == "ENO")
						{
							objInfo.ADD_INFORMATION =txtNoPersons.Text.Trim();
						
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
                                if (txtLimit.Text.Trim() != "" || hidLIMIT.Value != "")
								{
                                    string amount = txtLimit.Text.Trim() == "" ? hidLIMIT.Value : hidLIMIT.Value;

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
								if ( txtDeductible.Text.Trim() != "" )
								{
									objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtDeductible.Text.Trim());
								}

								break;
						}
						
						//INSERT 
						/*if ( objInfo.COVERAGE_ID == -1 )
						{
							if(hidOldData.Value == "")
							{
								//INSERT 
								objInfo.ACTION = "I";
							}
							else 
							{
								//UPDATE
								objInfo.ACTION = "U";
							}
						}*/
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
					else
					{
						//ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	

						if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							if ( objInfo.COVERAGE_ID != -1 )
							{
								objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
								objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
								objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
								objInfo.RISK_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
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

		/// <summary>
		/// Saves the Coverages
		/// </summary>
		/// <returns></returns>
		private int Save()
		{
			ArrayList alRecr = new ArrayList();
			ArrayList alDelete = new ArrayList();
			
			PopulateList(alRecr,this.dgPolicyCoverages);
			PopulateList(alRecr,this.dgCoverages);

			
			ClsVehicleCoverages objCoverages ;//= new ClsVehicleCoverages();
			if(calledFrom.ToUpper()=="MOT")
			{
				objCoverages = new ClsVehicleCoverages("MOTOR");
			}
			else
			{
				objCoverages = new ClsVehicleCoverages();

			}
			int retVal = 1;
			
			try
			{
				//Get the relevant coverages
				switch(calledFrom.ToUpper())
				{
					case "UMB":
						//retVal = objCoverages.SaveUmbrellaVehicleCoverages(alRecr,hidOldData.Value);
						//retVal = objCoverages.SaveUmbrellaVehicleCoveragesNew(alRecr,hidOldData.Value);
						break;
				
					case "PPA":
					case "VEH":
					case "MOT":
						//retVal = objCoverages.SaveVehicleCoverages(alRecr,hidOldData.Value);
						//retVal = objCoverages.SavePolicyVehicleCoverages(alRecr,hidOldData.Value);
						retVal = objCoverages.SavePolicyVehicleCoverages(alRecr,hidOldData.Value,hidCustomInfo.Value);
						break;
					
					default:
						//dtCoverages = ClsAppCoverages.GetCoverages("HCVCD",0,0);
						break;

				}

				

				

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
		/// Sets the workflow properties
		/// </summary>
		private void SetWorkFlowControl()
		{
			if (base.ScreenId == "227_1" || base.ScreenId == "231_1" || base.ScreenId == "246_2" )
			{
				myWorkFlow.IsTop = false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				
				//Setting other optional keys
				SetOtherWorkflowKEys();
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
		}

	
		/// <summary>
		/// Sets different keys of workflow other then customer, app id and veriosn id
		/// </summary>
		private void SetOtherWorkflowKEys()
		{
			switch(base.ScreenId)
			{
				case "231_1":	//For motor cycle
				case "227_1":	//For automobile
					if (hidREC_VEH_ID.Value != "" && hidREC_VEH_ID.Value != "0")
					{
						myWorkFlow.AddKeyValue("VEHICLE_ID", hidREC_VEH_ID.Value);
					}
					break;
				case "246_2":	//For Watercraft
					if (hidREC_VEH_ID.Value != "" && hidREC_VEH_ID.Value != "0")
					{
						myWorkFlow.AddKeyValue("BOAT_ID", hidREC_VEH_ID.Value);
					}
					break;

				
			}
		}

	}
}
