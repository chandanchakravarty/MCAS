/******************************************************************************************
<Author					: -   Gaurav
<Start Date				: -	  11 oct 05
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
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.BusinessLayer.BlCommon;
//using Cms.Model.Application.Watercrafts;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for Endorsements.
	/// </summary>
	public class PolicyHomeOwnerEndorsements :  Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
		protected System.Web.UI.WebControls.DataGrid dgEndorsements;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageXML;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		string calledFrom = "";
		int stateID=0;
		int product=0;
		DataSet dsCoverages = null;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		private DateTime AppEffectiveDate;
		StringBuilder sbScript = new StringBuilder();

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			// if called from private passenger automobile, otherwise use if else
			string strCalledFrom = "";
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			switch(strCalledFrom.ToUpper())
			{
				
				case "HOME" :
					base.ScreenId = "239_6";
					break;
				
				case "RENTAL" :
					base.ScreenId	=	"259_4";
					break;
								
				default :
					base.ScreenId	=	"61_8";
					break;
			}
			#endregion
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
		
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;
		
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			if ( Request.QueryString["CalledFrom"] != null )
			{
				calledFrom =  Request.QueryString["CalledFrom"].ToString();
				//strLobId= Request.QueryString["LOB_ID"].ToString();
			}

			if ( !Page.IsPostBack )
			{
				hidCustomerID.Value	 =  GetCustomerID();
				hidPOLID.Value       =  GetPolicyID();
				hidPOLVersionID.Value = GetPolicyVersionID();

				//Get LOB ID on basis of Custmoer id, Application id, Application Version Id
				//hidAPP_LOB.Value = ClsVehicleInformation.GetApplicationLOBID(hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value).ToString();
				
				this.hidREC_VEH_ID.Value = Request.QueryString["DWELLINGID"].ToString();
				this.btnReset.Attributes.Add("onclick","javascript: return ResetForm();");

				BindGrid();	
				SetWorkflow();
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
			this.dgEndorsements.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgEndorsements_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BindGrid()
		{
			
			
			//DataSet dsCoverages = null;
			
			 Cms.BusinessLayer.BlApplication.HomeOwners.ClsDwellingEndorsements objEndorsements = new ClsDwellingEndorsements();

			//Get the relevant coverages
			switch(calledFrom.ToUpper())
			{	
				case "UMB":
					/*
					dsCoverages = ClsVehicleEndorsements.GetUmbrellaVehicleCoverages(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidAppID.Value),
						Convert.ToInt32(hidAppVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						0,
						10);	*/
					break;

				
				case "HOME":
					dsCoverages = objEndorsements.GetDwellingEndorsementsForPolicy(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPOLID.Value),
						Convert.ToInt32(hidPOLVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						"N"
						);	
					
					break;
					
				case "RENTAL":
					
					dsCoverages =objEndorsements.GetRentalDwellingEndorsementsForPolicy(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPOLID.Value),
						Convert.ToInt32(hidPOLVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						"N"
						);	
					
					break;
					
				default:
				/**	dsCoverages = Cms.BusinessLayer.BlApplication.HomeOwners.ClsDwellingEndorsements.GetDwellingEndorsements(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPOLID.Value),
						Convert.ToInt32(hidPOLVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						"R"
						);	**/
					break;

			}
			
			//Get the state details
			/*
			string lob = base.GetLOBString();
			DataTable dtState = dsCoverages.Tables[2];
			
			string state = dtState.Rows[0]["STATE_NAME"].ToString();
			strLOBState = lob + state;
			*/
			DataTable dtHome = dsCoverages.Tables[1];
			

			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0)
				{
					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						product = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

				}
			}

			if(dsCoverages.Tables.Count>1)
			{
				if(dsCoverages.Tables[1].Rows.Count>0)
				{
					if(dsCoverages.Tables[1].Rows[0]["APP_EFFECTIVE_DATE"] !=DBNull.Value)
					{
						AppEffectiveDate = Convert.ToDateTime(dsCoverages.Tables[1].Rows[0]["APP_EFFECTIVE_DATE"]);
					}
				}
			}
			dgEndorsements.DataSource = dsCoverages.Tables[0];
			dgEndorsements.DataBind();
			
			this.hidROW_COUNT.Value = 	this.dgEndorsements.Items.Count.ToString();

			DataTable dataTable = dsCoverages.Tables[0];

			hidOldData.Value =  ClsCommon.GetXMLEncoded(dataTable);
			RegisterScript();
		}


		private void dgEndorsements_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//Adding Style to Alternating Item
			e.Item.Attributes.Add("Class","midcolora");
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				DataRowView drv = (DataRowView)e.Item.DataItem;
				
				CheckBox cbSelect = (CheckBox)e.Item.FindControl("cbSelect");
				TextBox txtRemarks = (TextBox)e.Item.FindControl("txtREMARKS");
				HtmlSelect ddlEDITIONDATE		=(HtmlSelect)e.Item.FindControl("ddlEDITIONDATE") ; 
				//For endorsements that are currently stored
				if ( drv["DWELLING_ENDORSEMENT_ID"] != System.DBNull.Value )
				{
					cbSelect.Checked = true;
				}
				
				//Mandatory endorsements, disabled
				if ( drv["TYPE"] != System.DBNull.Value )
				{
					if ( drv["TYPE"].ToString() == "M" )
					{
						cbSelect.Enabled = false;	
					}
					else
					{
						//Non mandatory
						cbSelect.Enabled = true;	
					}
				}

				//To Change the color of row if Coverage is availavle due to GrandFathered
				if(drv["EFFECTIVE_FROM_DATE"] != DBNull.Value)
				{
					if(AppEffectiveDate < Convert.ToDateTime( drv["EFFECTIVE_FROM_DATE"]))
					{
						//e.Item.Attributes.CssStyle.Add("COLOR","Red");
						e.Item.Attributes.Add("Class","GrandFatheredCoverage");
					}
					else
					{
						e.Item.Attributes.Add("Class","midcolora");
					}
				}
				if( drv["EFFECTIVE_TO_DATE"] != DBNull.Value )
				{
					if(AppEffectiveDate > Convert.ToDateTime(drv["EFFECTIVE_TO_DATE"]) )
					{
						//e.Item.Attributes.CssStyle.Add("COLOR","Red");
						e.Item.Attributes.Add("Class","GrandFatheredCoverage");
					}
					else
					{
						e.Item.Attributes.Add("Class","midcolora");
					}

				}
				//Non-linked coverages, everything should be enabled
				if ( drv["Selected"] == System.DBNull.Value )
				{
					txtRemarks.Enabled = true;	
				}
				else
				{
					//Linked Coverages, disable check box
					int id = Convert.ToInt32(drv["Selected"]);
					
					cbSelect.Enabled = false;

					//Related coverage not chosen
					if ( id == 0 )
					{
						//cbSelect.Checked = false;
						txtRemarks.Enabled = false;
					}
					else
					{
						//Related coverage chosen, so enable the remarks field
						txtRemarks.Enabled = true;
					}
		
				}
				//New code being added
				//New code being added
				/*HO-4 Deluxe,HO-4,HO-6,HO-6 Deluxe
				 * HO-48 Other Structures-Increased Limits Is Non Mandatory For these Products*/
				/* this Commented by Pravesh on 25 april as HO 48 is linked with Coverage B (OS) for Issue no 1005 of Covg
				if(stateID==22 &&  drv["ENDORSMENT_ID"].ToString() == "172")
				{
					if( product  == 11407 || product == 11405 || product == 11406 || product == 11408)
					{
						txtRemarks.Enabled = true;
						cbSelect.Enabled = true;
					}
				}
				if(stateID==14 &&  drv["ENDORSMENT_ID"].ToString() == "222")
				{
					if( product == 11195 || product == 11245 || product == 11196 || product == 11246)
					{
						txtRemarks.Enabled = true;	
						cbSelect.Enabled = true;

					}
				}
				*/
				//
				DataTable dtDates = this.dsCoverages.Tables[5];
				DataRowView drvItem = (DataRowView)e.Item.DataItem;
				int intENDOW_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"ENDORSMENT_ID"));
				DataView dvEditionDates = new DataView(dtDates);
				//Set Filter for limits and deductibles
				dvEditionDates.RowFilter = "ENDORSEMENT_ID = " + intENDOW_ID.ToString() + ""; // AND LIMIT_DEDUC_TYPE = 'Limit'";
				
				//Select the ranges applicable to this Coverage
				ClsVehicleCoverages.BindDropDown(ddlEDITIONDATE,dvEditionDates,"EDITION_DATE","ENDORSEMENT_ATTACH_ID",AppEffectiveDate);
				ClsCoverages.SelectValueInDropDown(ddlEDITIONDATE,DataBinder.Eval(e.Item.DataItem,"EDITION_DATE"));
				ClsCoverages.RemoveDisabledInDropDown(ddlEDITIONDATE,dvEditionDates,DataBinder.Eval(e.Item.DataItem,"EDITION_DATE"),AppEffectiveDate);  
				if (ddlEDITIONDATE.Items.Count ==0)
					ddlEDITIONDATE.Attributes.Add("style","display:none");  
				//

				string disableScript = "DisableControls('" + cbSelect.ClientID + "','" + txtRemarks.ClientID +  "');";
				
				
				sbScript.Append(disableScript);
				
				cbSelect.Attributes.Add("onClick","javascript:" + disableScript);
				

			}

		}

		private void RegisterScript()
		{
            if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				ClientScript.RegisterStartupScript(this.GetType(),"Refresh","<script>" + sbScript.ToString() + "</script>");

			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			int retVal = 0;
			lblMessage.Visible = true;

			try
			{
				retVal = Save();

				//Setting the workflow
				SetWorkflow();				
			}
			catch(Exception ex)
			{
				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;
					return;
				}
			}

			if ( retVal == 1 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
				BindGrid();
				base.OpenEndorsementDetails();
			}


		}
		
		private int Save()
		{
			ArrayList alEndorsements = new ArrayList();
			ArrayList alDelete = new ArrayList();
			
			foreach(DataGridItem dgi in this.dgEndorsements.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					//Get the checkbox
					CheckBox cbSelect = (CheckBox)dgi.FindControl("cbSelect");
					
					Label lblEndorsement = (Label)dgi.FindControl("lblENDORSEMENT");
					Label lblEndID = (Label)dgi.FindControl("lblEND_ID");
					TextBox txtRemarks = (TextBox)dgi.FindControl("txtRemarks");
					HtmlSelect ddlEDITIONDATE=(HtmlSelect)dgi.FindControl("ddlEDITIONDATE");
					Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo objInfo = new Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo();	
				
					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPOLVersionID.Value);
					objInfo.POLICY_ID  = Convert.ToInt32(this.hidPOLID.Value);
					objInfo.DWELLING_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
					objInfo.ENDORSEMENT = lblEndorsement.Text;
					objInfo.REMARKS = txtRemarks.Text.Trim();
					objInfo.ENDORSEMENT_ID = Convert.ToInt32(lblEndID.Text);
					if (ddlEDITIONDATE.SelectedIndex !=-1 && ddlEDITIONDATE.Value !="")
						objInfo.EDITION_DATE =ddlEDITIONDATE.Items[ddlEDITIONDATE.SelectedIndex].Value;   
			

					if ( this.dgEndorsements.DataKeys[dgi.ItemIndex] == System.DBNull.Value )
					{
						objInfo.DWELLING_ENDORSEMENT_ID = -1;
					}
					else
					{
						objInfo.DWELLING_ENDORSEMENT_ID = Convert.ToInt32(dgEndorsements.DataKeys[dgi.ItemIndex]);
					}
					
					if ( cbSelect.Checked )
					{
						//INSERT 
						if ( objInfo.DWELLING_ENDORSEMENT_ID == -1 )
						{
							objInfo.ACTION = "I";
						}
						else
						{
							//UPDATE
							objInfo.ACTION = "U";
						}

						alEndorsements.Add(objInfo);
					}
					else
					{
						//Only if existin record has been deleted
						if ( objInfo.DWELLING_ENDORSEMENT_ID > -1 )
						{
							objInfo.ACTION = "D";
							alEndorsements.Add(objInfo);
							alDelete.Add(objInfo);
						}
					}


				}
			}
			
			Cms.BusinessLayer.BlApplication.HomeOwners.ClsDwellingEndorsements objEndorsements = new ClsDwellingEndorsements();

		
			int retVal = 1;
			string currentUser = GetUserId();
			
			//Get the relevant coverages
			switch(calledFrom.ToUpper())
			{
				
				case "RENTAL":
				case "HOME":
					retVal = objEndorsements.SaveDwellingEndorsementsForPolicy(alEndorsements,hidOldData.Value,
						Convert.ToInt32(currentUser),
						Convert.ToInt32(this.hidCustomerID.Value),
						Convert.ToInt32(this.hidPOLID.Value),
						Convert.ToInt32(this.hidPOLVersionID.Value)
						);
					break;
				default:
					//dtCoverages = ClsAppCoverages.GetCoverages("HCVCD",0,0);
					break;

			}

			

			return 1;

			

		}

		#region workflow
		/// <summary>
		/// Sets the workflow properties
		/// </summary>
		private void SetWorkflow()
		{
			if(base.ScreenId == "239_6" || base.ScreenId == "159_8" )
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;

				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOLID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOLVersionID.Value);
				
				if ( hidREC_VEH_ID.Value != null && hidREC_VEH_ID.Value != "" )
				{
					myWorkFlow.AddKeyValue("DWELLING_ID",hidREC_VEH_ID.Value);
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
	}
}
