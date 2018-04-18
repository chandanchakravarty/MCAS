/******************************************************************************************
<Author					: -   Shafi
<Start Date				: -	  15/02/06  
<End Date				: -	
<Description			: -  Endorsements information screen
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

using Cms.Model.Policy;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;


namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for Endorsements.
	/// </summary>
	public class PolicyEndorsement : Cms.Policies.policiesbase 
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
		protected System.Web.UI.WebControls.DataGrid dgEndorsements;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageXML;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue2;
		public string calledFrom = "";
		public string pageFrom = "";
		DataSet dsCoverages = null;
		StringBuilder sbScript = new StringBuilder();
		private DateTime AppEffectiveDate;
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
				
				case "PPA" :
                    //Modified by Lalit for policy vehicles
                    base.ScreenId = "227_2";                    
                    //base.ScreenId	=	"44_4";
					break;
				case "UMB" :
					base.ScreenId	=	"81_3";
					break;
				case "MOT" :
					base.ScreenId	=	"48_3";
					break;
				default :
					if (GetLOBString() == "UMB")
					{
						base.ScreenId	=	"83_2";
					}
					else if(GetLOBString()=="HOME")
					{
						base.ScreenId	=	"251_3";
					}
					else if(GetLOBString()=="WAT")
					{
						base.ScreenId	=	"246_3";
					}
					else
					{
						base.ScreenId	=	"44_2";
					}
					
					break;
			}
			#endregion
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
		
			

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			if ( Request.QueryString["CalledFrom"] != null )
			{
				calledFrom =  Request.QueryString["CalledFrom"].ToString();
				//strLobId= Request.QueryString["LOB_ID"].ToString();
			}
			
			if ( Request.QueryString["PageFrom"] != null )
			{
				pageFrom =  Request.QueryString["PageFrom"].ToString();
				//strLobId= Request.QueryString["LOB_ID"].ToString();
			}

			if ( !Page.IsPostBack )
			{
				hidCustomerID.Value	 =  GetCustomerID();
				hidPolID.Value = GetPolicyID();
				hidPolVersionID.Value = GetPolicyVersionID();

				//Get LOB ID on basis of Custmoer id, Application id, Application Version Id
				//hidAPP_LOB.Value = ClsVehicleInformation.GetApplicationLOBID(hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value).ToString();
				
				this.hidREC_VEH_ID.Value = Request.QueryString["VehicleID"].ToString();
				
				BindGrid();	
			}

			//Setting the workflow
			SetWorkflow();
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
			ClsCoverages objHome = new ClsCoverages();
			ClsVehicleEndorsements objEnd = new ClsVehicleEndorsements();
			ClsWatercraftEndorsements objWater = new ClsWatercraftEndorsements();
			string newOrRenewal = "N";

			//DataSet dsCoverages = null;
			
			ClsGeneralInformation objGen = new ClsGeneralInformation();
			newOrRenewal = objGen.GetNewBusinessOrRenewal(Convert.ToInt32(hidCustomerID.Value),
							Convert.ToInt32(hidPolID.Value),
							Convert.ToInt32(hidPolVersionID.Value));
		

			//Get the relevant coverages
			switch(calledFrom)
			{
				case "WAT":

				switch(this.pageFrom)
				{
					case "RWAT":
					case "HWAT":
					case "WWAT":
						dsCoverages = objWater.GetWatercraftEndorsementsForPolicy(Convert.ToInt32(hidCustomerID.Value),
							Convert.ToInt32(hidPolID.Value),
							Convert.ToInt32(hidPolVersionID.Value),
							Convert.ToInt32(hidREC_VEH_ID.Value),
							newOrRenewal);	
						break;
					case "UWAT":
					//	dsCoverages = ClsVehicleEndorsements.GetUmbrellaWatercraftEndorsements(Convert.ToInt32(hidCustomerID.Value),
					//		Convert.ToInt32(hidAppID.Value),
						//	Convert.ToInt32(hidAppVersionID.Value),
						//	Convert.ToInt32(hidREC_VEH_ID.Value),
							//"N");		
						break;

				}
					
					break;

				case "PPA":
				case "MOT":
				case "VEH":
					dsCoverages = objEnd.GetPolicyVehicleEndorsements(Convert.ToInt32(hidCustomerID.Value),
					Convert.ToInt32(hidPolID.Value),
					Convert.ToInt32(hidPolVersionID.Value),
					Convert.ToInt32(hidREC_VEH_ID.Value),
					newOrRenewal
					);	
					break;
				case "UMB":
					
					
				///	dsCoverages = objEnd.GetUmbrellaVehicleEndorsements(Convert.ToInt32(hidCustomerID.Value),
					///	Convert.ToInt32(hidAppID.Value),
					///	Convert.ToInt32(hidAppVersionID.Value),
					///	Convert.ToInt32(hidREC_VEH_ID.Value),
					///	"N"
					///	);	
					break;
					
				default:
					/*
					dtCoverages = ClsCoverages.GetCoverages(Convert.ToInt32(hidCustomerID.Value), 
						Convert.ToInt32(hidAppID.Value), 
						Convert.ToInt32(hidAppVersionID.Value));*/
					break;

			}
			
		

			

			if(dsCoverages.Tables.Count>1)
			{
				if(dsCoverages.Tables[1].Rows.Count>0)
				{
					if(dsCoverages.Tables[1].Rows[0]["APP_EFF_DATE"] !=DBNull.Value)
					{
						AppEffectiveDate = Convert.ToDateTime(dsCoverages.Tables[1].Rows[0]["APP_EFF_DATE"]);
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
				if ( drv["VEHICLE_ENDORSEMENT_ID"] != System.DBNull.Value )
				{
					cbSelect.Checked = true;
	
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
					if ( drv["TYPE"] != System.DBNull.Value )
					{
						if ( drv["TYPE"].ToString() == "M" )
						{
							cbSelect.Enabled = false;	

						}
						else
						{
							cbSelect.Enabled = true;	
						}
					}

					txtRemarks.Enabled = true;
				}
				else
				{
					//Linked Coverages
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
				
				DataTable dtDates = this.dsCoverages.Tables[3];
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

				string disableScript = "DisableControls('" + cbSelect.ClientID + "','" + txtRemarks.ClientID +  "');";
				
				
				sbScript.Append(disableScript);
				
				cbSelect.Attributes.Add("onClick","javascript:" + disableScript);
				
				///////////

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
				base.OpenEndorsementDetails();
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
					HtmlSelect ddlEDITIONDATE		=(HtmlSelect)dgi.FindControl("ddlEDITIONDATE") ; 
					ClsPolicyVehicleEndorsementInfo objInfo = new ClsPolicyVehicleEndorsementInfo();
				
					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_VERSION_ID  = Convert.ToInt32(this.hidPolVersionID.Value);
					objInfo.POLICY_ID  = Convert.ToInt32(this.hidPolID.Value);
					objInfo.VEHICLE_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
					objInfo.ENDORSEMENT = lblEndorsement.Text;
					objInfo.REMARKS = txtRemarks.Text.Trim();
					objInfo.ENDORSEMENT_ID = Convert.ToInt32(lblEndID.Text);
					objInfo.CREATED_BY=int.Parse(GetUserId());
					objInfo.MODIFIED_BY=int.Parse(GetUserId());
					if (ddlEDITIONDATE.SelectedIndex !=-1 && ddlEDITIONDATE.Value !="")
						objInfo.EDITION_DATE =ddlEDITIONDATE.Items[ddlEDITIONDATE.SelectedIndex].Value;   
					if ( this.dgEndorsements.DataKeys[dgi.ItemIndex] == System.DBNull.Value )
					{
						objInfo.VEHICLE_ENDORSEMENT_ID = -1;	
						objInfo.ACTION="I";
					}
					else
					{
						objInfo.VEHICLE_ENDORSEMENT_ID = Convert.ToInt32(dgEndorsements.DataKeys[dgi.ItemIndex]);
						objInfo.ACTION="U";
					}
					
					if ( cbSelect.Checked )
					{						
						alEndorsements.Add(objInfo);
					}
					else
					{
						//Check for record existence
						if ( objInfo.VEHICLE_ENDORSEMENT_ID != -1 )
						{
							objInfo.ACTION="D";						
							alEndorsements.Add(objInfo);
							alDelete.Add(objInfo);
						}
					}


				}
			}
			
			ClsVehicleEndorsements objEndorsements = new ClsVehicleEndorsements();
			ClsWatercraftEndorsements objWater = new ClsWatercraftEndorsements();
		
			int retVal = 1;
			
			
			//Get the relevant coverages
			switch(calledFrom)
			{
				case "UMB":
					retVal = objEndorsements.SaveUmbrellaVehicleEndorsements(alEndorsements,hidOldData.Value);
					break;
				case "PPA":
				case "VEH":
				case "MOT":
					retVal = objEndorsements.SavePolicyVehicleEndorsements(alEndorsements,hidOldData.Value);
					break;
				case "WAT":
				switch(this.pageFrom)
				{
					case "RWAT":
					case "HWAT":
					case "WWAT":
						//retVal = objEndorsements.SaveWatercraftEndorsements(alEndorsements,hidOldData.Value);
						retVal = objWater.SaveWatercraftEndorsementsForPolicy(alEndorsements,hidOldData.Value,hidCustomInfo.Value);
						break;
					case "UWAT":
						//retVal = objEndorsements.SaveUmbrellaWatercraftEndorsements(alEndorsements,hidOldData.Value);	
						retVal = objEndorsements.SaveUmbrellaWatercraftEndorsementsNew(alEndorsements,hidOldData.Value,hidCustomInfo.Value);	
						break;
				}

						
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
			if(base.ScreenId == "44_4" || base.ScreenId == "48_3" || base.ScreenId == "81_3" || base.ScreenId == "148_4"
				|| base.ScreenId == "83_2" || base.ScreenId == "44_2" || base.ScreenId == "72_4" || base.ScreenId == "246_3" || base.ScreenId == "251_3")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;

				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				
				
				if ( hidREC_VEH_ID.Value != null && hidREC_VEH_ID.Value != "" )
				{
					if(base.ScreenId == "72_4" || base.ScreenId=="148_4" || base.ScreenId=="83_2" || base.ScreenId == "246_3")
						myWorkFlow.AddKeyValue("BOAT_ID",hidREC_VEH_ID.Value);
					else
						myWorkFlow.AddKeyValue("VEHICLE_ID",hidREC_VEH_ID.Value);
				}
				if(base.ScreenId=="81_3")
					myWorkFlow.AddKeyValue("REMARKS","isnull(REMARKS,-2)");
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
