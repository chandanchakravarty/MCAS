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
//using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
namespace Policies.Aspx.Watercraft
{
	/// <summary>
	/// Summary description for PolicyCopyWatercraftCoverages.
	/// </summary>
	public class PolicyCopyWatercraftCoverages :Cms.Policies.policiesbase 
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgVehicles;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageXML;
		string calledFrom = "";
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
		int vehicleID=0;
		DataSet dsCoverages = null;
		protected System.Web.UI.WebControls.DataGrid dgCoveragesToCopy;
		protected System.Web.UI.WebControls.Label lblShowMessage;
		string pageFrom = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			// Put user code to initialize the page here
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
			
			#region setting screen id
			
			switch(calledFrom.ToUpper())
			{
				case "WAT" :
					base.ScreenId	=	"255_4";
					break;
				case "PPA" :
					base.ScreenId	=	"44_4";
					break;
				case "UMB" :
					base.ScreenId	=	"81_1";
					break;
				case "MOT" :
					base.ScreenId	=	"48_3";
					break;
				default :
					base.ScreenId	=	"44_2";
					break;
			}
			#endregion

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************	
			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			

			
			if ( !Page.IsPostBack)
			{

				hidCustomerID.Value	 =  GetCustomerID();
				hidPolID.Value  =GetPolicyID();
				hidPolVersionID.Value =GetPolicyVersionID();
				//hidAppID.Value = GetAppID();
				//hidAppVersionID.Value = GetAppVersionID();
				
				if ( Request.QueryString["VEHICLE_ID"] != null )
				{
					this.hidREC_VEH_ID.Value = Request.QueryString["VEHICLE_ID"].ToString();
				}

				BindGrid();
			}
			lblShowMessage.Visible=false;
			ShowApplcableCoverages();

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

		private void RegisterScript()
		{
			string strScript = @"<script>" + 
				"window.opener.Refresh();" + 
				"window.close();" + 
				"</script>" 
				;
	

			if (!ClientScript.IsStartupScriptRegistered("Refresh"))
			{
				ClientScript.RegisterStartupScript(this.GetType(),"Refresh",strScript);

			}
		}

		private void ShowApplcableCoverages()
		{
			vehicleID = 0;
			foreach(DataGridItem dgi in this.dgVehicles.Items)
			{
				if ( dgi.ItemType == ListItemType.AlternatingItem || dgi.ItemType == ListItemType.Item )
				{
					System.Web.UI.HtmlControls.HtmlInputRadioButton rb = (HtmlInputRadioButton)dgi.FindControl("rdbSELECT");
				
					if ( rb.Checked)
					{
						vehicleID = Convert.ToInt32(((Label)dgi.FindControl("lblBOAT_ID")).Text);
						break;

					}

				}
			}
			if (vehicleID == 0) return;
			ClsWatercraftCoverages objWater = new ClsWatercraftCoverages();

			
				
			switch(calledFrom.ToUpper())
			{
				case "WAT":
				switch (pageFrom)
				{
					case "HWAT":
					case "WWAT":	
						//Get The Applicable WaterCraft for destination from source
						dsCoverages = objWater.GetPolicyWatercraftCoveragesCopyDisplay(Convert.ToInt32(hidCustomerID.Value),
							Convert.ToInt32(hidPolID.Value),
							Convert.ToInt32(hidPolVersionID.Value),
							vehicleID,
							this.pageFrom.ToUpper(),Convert.ToInt32(hidREC_VEH_ID.Value)
							);
						lblShowMessage.Visible=true;
						if(dsCoverages.Tables[0].Rows.Count >0)
						{
							lblShowMessage.Text =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("752");
							dgCoveragesToCopy.DataSource = dsCoverages.Tables[0];
							dgCoveragesToCopy.DataBind();
						}
						else
						{
							lblShowMessage.Text =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("751");
						}

						break;
						
						
					case "RWAT":
						
						break;
				
				}
					break;
			}
            
			

			
  

		}
		private void BindGrid()
		{
			DataTable dtVehicles = null;
			
			ClsWatercraftCoverages objWater = new ClsWatercraftCoverages();

			switch(calledFrom.ToUpper())
			{
				case "WAT":
				switch (pageFrom)
				{
					case "WWAT":
					case "HWAT":
						
						dtVehicles = objWater.GetPolicyWatercraftsToCopy(Convert.ToInt32(hidCustomerID.Value),
							Convert.ToInt32(hidPolID.Value),
							Convert.ToInt32(hidPolVersionID.Value),
							Convert.ToInt32(hidREC_VEH_ID.Value),
							pageFrom.ToUpper()
							);
						break;
						break;
					case "RWAT":
						
						break;
				
				}
					break;
			}

			
             
			if ( dtVehicles != null)
			{
				if(!(dtVehicles.Rows.Count>0))
					btnSave.Visible=false;
				this.dgVehicles.DataSource = dtVehicles;
				dgVehicles.DataBind();
			}

		}
	
		private int Save()
		{
			if(vehicleID==0)
			{
				this.lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"724");
				return -2;

			}


			if ( dsCoverages.Tables[0].Rows.Count == 0 )
			{
				this.lblMessage.Text = "No coverages are available in the selected watercraft.";
				return -2;
			}

			if ( dsCoverages != null )
			{
				ArrayList alRecr = new ArrayList();

				ClsWatercraftCoverages objWater = new ClsWatercraftCoverages();
				//Copy the coverages
				DataTable dtCov = dsCoverages.Tables[0];
						
				foreach(DataRow dr in dtCov.Rows)
				{
					ClsPolicyCoveragesInfo  objInfo = new ClsPolicyCoveragesInfo();	

					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_VERSION_ID  = Convert.ToInt32(this.hidPolVersionID.Value);
					objInfo.POLICY_ID   = Convert.ToInt32(this.hidPolID.Value);
					objInfo.RISK_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
					objInfo.COVERAGE_CODE_ID = Convert.ToInt32(dr["COVERAGE_CODE_ID"]);
							
					if ( dr["LIMIT_1"] != DBNull.Value )
					{
						objInfo.LIMIT_1 = Convert.ToDouble(dr["LIMIT_1"]);
					}
							
					if ( dr["LIMIT_2"] != DBNull.Value )
					{
						objInfo.LIMIT_2 = Convert.ToDouble(dr["LIMIT_2"]);
					}
							
					if ( dr["DEDUCTIBLE_1"] != DBNull.Value )
					{
						objInfo.DEDUCTIBLE_1 = Convert.ToDouble(dr["DEDUCTIBLE_1"]);
					}
							
					if ( dr["DEDUCTIBLE_2"] != DBNull.Value )
					{
						objInfo.DEDUCTIBLE_2 = Convert.ToDouble(dr["DEDUCTIBLE_2"]);
					}
							
					if ( dr["LIMIT1_AMOUNT_TEXT"] != DBNull.Value )
					{
						objInfo.LIMIT1_AMOUNT_TEXT = Convert.ToString(dr["LIMIT1_AMOUNT_TEXT"]);
					}
							
					if ( dr["LIMIT2_AMOUNT_TEXT"] != DBNull.Value )
					{
						objInfo.LIMIT2_AMOUNT_TEXT = Convert.ToString(dr["LIMIT2_AMOUNT_TEXT"]);
					}
							
							
					if ( dr["DEDUCTIBLE1_AMOUNT_TEXT"] != DBNull.Value )
					{
						objInfo.DEDUCTIBLE1_AMOUNT_TEXT = Convert.ToString(dr["DEDUCTIBLE1_AMOUNT_TEXT"]);
					}
					if ( dr["DEDUC_ID"]  != DBNull.Value )
					{
						objInfo.DEDUC_ID  = Convert.ToInt32(dr["DEDUC_ID"]);
					}
					if ( dr["LIMIT_ID"]  != DBNull.Value )
					{
						objInfo.LIMIT_ID  = Convert.ToInt32(dr["LIMIT_ID"]);
					}
						
					alRecr.Add(objInfo);
				}

				
					
				
						
						
				//ClsCoverages objCoverages = new ClsCoverages();
			
				int retVal = 1;
			
				switch(calledFrom.ToUpper())
				{
					case "WAT":
					switch (pageFrom)
					{
						case "HWAT":
						case "WWAT":
						
							retVal = objWater.CopyPolicyWatercraftCoverages(alRecr,
								Convert.ToInt32(hidCustomerID .Value),
								Convert.ToInt32(hidPolID.Value),
								Convert.ToInt32(hidPolVersionID.Value),
								Convert.ToInt32(hidREC_VEH_ID.Value),
								hidOldData.Value
								);
							break;
							
							
						case "RWAT":
						
							break;
				
					}
						break;
				}
	
					
			}

			
			return 1;

		}
		
		/// <summary>
		/// Event handler for btnSave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			this.lblMessage.Visible = true;
			
			int retVal = 0;

			try
			{
				retVal = Save();
			}
			catch(Exception ex)
			{
				this.lblMessage.Text = ex.Message;
				return;
			}
			
			if ( retVal == 1 )
			{
				
				this.lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"725");
				RegisterScript();
			}
		}



	}
}
