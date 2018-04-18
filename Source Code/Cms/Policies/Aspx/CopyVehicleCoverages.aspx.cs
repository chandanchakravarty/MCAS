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
using Cms.Model.Policy;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;

namespace  Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for CopyVehicleCoverages.
	/// </summary>
	public class CopyVehicleCoverages : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.DataGrid dgVehicles;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageXML;
		//ADDED BY PRAVEEN KUMAR(4-02-2009):ITRACK 5281
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUserID;
		//END
		string calledFrom = "";
		string pageFrom = "";
		

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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		}
		#endregion

		
		private void Page_Load(object sender, System.EventArgs e)
		{
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

			
			btnSave.Attributes.Add("onClick","javascript:return onSave();");
			if ( !Page.IsPostBack)
			{

				hidCustomerID.Value	 =  GetCustomerID();
				hidPolID.Value = GetPolicyID();
				hidPolVersionID.Value = GetPolicyVersionID();
				hidUserID.Value			  = GetUserId();
				
				if ( Request.QueryString["VEHICLE_ID"] != null )
				{
					this.hidREC_VEH_ID.Value = Request.QueryString["VEHICLE_ID"].ToString();
				}

				BindGrid();
			}
		}

		private void BindGrid()
		{
			DataTable dtVehicles = null;
			ClsVehicleCoverages objVeh = new ClsVehicleCoverages();


			switch(calledFrom.ToUpper())
			{
				case "UMB":
				case "PPA":
				case "MOT":
				case "VEH":
					dtVehicles = objVeh.GetPolicyVehiclesToCopy(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPolID.Value),
						Convert.ToInt32(hidPolVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						calledFrom.ToUpper()
						);

					break;
				case "WAT":
					break;
			}

			if ( dtVehicles != null)
			{
				//Disable the Save button when there are no records to be saved
				if(dtVehicles.Rows.Count<1)
					btnSave.Enabled=false;
				this.dgVehicles.DataSource = dtVehicles;
				dgVehicles.DataBind();
			}

		}

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
                base.OpenEndorsementDetails();
				this.lblMessage.Text = "Coverages copied successfully.";
				RegisterScript();
			}


		}
		
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

		private int Save()
		{
			int vehicleID = 0;

			foreach(DataGridItem dgi in this.dgVehicles.Items)
			{
				if ( dgi.ItemType == ListItemType.AlternatingItem || dgi.ItemType == ListItemType.Item )
				{
					System.Web.UI.HtmlControls.HtmlInputRadioButton rb = (HtmlInputRadioButton)dgi.FindControl("rdbSELECT");
				
					if ( rb.Checked)
					{
						vehicleID = Convert.ToInt32(((Label)dgi.FindControl("lblVEHICLE_ID")).Text);
						break;

					}

				}
			}
			
			ClsVehicleCoverages objVeh = new ClsVehicleCoverages();

			if ( vehicleID != 0 )
			{
				DataSet dsCoverages = null;


				switch(calledFrom.ToUpper())
				{
					case "UMB":
					case "PPA":
					case "MOT":
					case "VEH":
						//Get the coverages to copy
						dsCoverages = objVeh.GetPolicyCoveragesToCopy(Convert.ToInt32(hidCustomerID.Value),
							Convert.ToInt32(hidPolID.Value),
							Convert.ToInt32(hidPolVersionID.Value),
							vehicleID,
							calledFrom.ToUpper()
							);
						break;
				}

					
					
				//No coverages available for copying
				if ( dsCoverages.Tables[0].Rows.Count == 0 )
				{
					this.lblMessage.Text = "No coverages are available in the selected vehicle.";
					return -2;
				}


				//Have the coverages to be removed from the dataset
				Cms.BusinessLayer.BlApplication.clsapplication objApp = new clsapplication();
					
				DataSet dsNew = null;

				switch(calledFrom.ToUpper())
				{
					case "UMB":
						dsNew = dsCoverages;
						break;
					case "MOT":
						
						dsNew = objVeh.GetPolicyMotorcycleCoverages(dsCoverages,
								Convert.ToInt32(hidCustomerID.Value),
							Convert.ToInt32(hidPolID.Value),
							Convert.ToInt32(hidPolVersionID.Value),
							Convert.ToInt32(hidREC_VEH_ID.Value),"N"
							);
						break;
					case "PPA":
						dsNew = objVeh.GetPolicyVehicleCoverages(dsCoverages,Convert.ToInt32(hidCustomerID.Value),
							Convert.ToInt32(hidPolID.Value),
							Convert.ToInt32(hidPolVersionID.Value),
							Convert.ToInt32(hidREC_VEH_ID.Value),"N");
						break;
				}

				if ( dsNew != null )
				{
					ArrayList alRecr = new ArrayList();


					//Copy the coverages
					DataTable dtCov = dsNew.Tables[0];
						
					foreach(DataRow dr in dtCov.Rows)
					{
						ClsPolicyCoveragesInfo objInfo = new ClsPolicyCoveragesInfo();	

						objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
						objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
						objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
						objInfo.RISK_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
						objInfo.COVERAGE_CODE_ID = Convert.ToInt32(dr["COVERAGE_CODE_ID"]);
						objInfo.COV_DESC = dr["COV_DESC"].ToString();
							
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
							
						if ( dr["DEDUCTIBLE2_AMOUNT_TEXT"] != DBNull.Value )
						{
							objInfo.DEDUCTIBLE2_AMOUNT_TEXT = Convert.ToString(dr["DEDUCTIBLE2_AMOUNT_TEXT"]);
						}

						
						//ADDED BY PRAVEEN KUMAR(19-01-2009):Itrack 5281

						if ( dr["LIMIT_ID"] != DBNull.Value )
						{
							objInfo.LIMIT_ID = Convert.ToInt32(dr["LIMIT_ID"]);
						}

						if ( dr["DEDUC_ID"] != DBNull.Value )
						{
							objInfo.DEDUC_ID = Convert.ToInt32(dr["DEDUC_ID"]);
						}

						// End Praveen Kumar
						//by pravesh on 2 march 09
						if ( dr["ADD_INFORMATION"] != DBNull.Value )
						{
							objInfo.ADD_INFORMATION = dr["ADD_INFORMATION"].ToString();
						}	
						if ( dr["SIGNATURE_OBTAINED"] != DBNull.Value )
						{
							objInfo.SIGNATURE_OBTAINED = dr["SIGNATURE_OBTAINED"].ToString();
						}
						alRecr.Add(objInfo);
					}
						
						
					ClsVehicleCoverages objCoverages ;//= new ClsVehicleCoverages();
					if (calledFrom.ToUpper()=="MOT")
						objCoverages = new ClsVehicleCoverages("MOTOR");
					else
						objCoverages = new ClsVehicleCoverages();

					int retVal = 1;
			
						
					//Get the relevant coverages
					switch(calledFrom.ToUpper())
					{
						case "UMB":
							/*
							retVal = objCoverages.CopyUmbrellaVehicleCoverages(alRecr,
								Convert.ToInt32(hidCustomerID.Value),
								Convert.ToInt32(hidAppID.Value),
								Convert.ToInt32(hidAppVersionID.Value),
								Convert.ToInt32(hidREC_VEH_ID.Value),
								hidOldData.Value
								);*/
							break;
			
						case "PPA":
						case "VEH":
						case "MOT":
							retVal = objCoverages.CopyPolicyVehicleCoverages(alRecr,
								Convert.ToInt32(hidCustomerID.Value),
								Convert.ToInt32(hidPolID.Value),
								Convert.ToInt32(hidPolVersionID.Value),
								Convert.ToInt32(hidREC_VEH_ID.Value),
								hidOldData.Value,
								Convert.ToInt32(hidUserID.Value)
								);

							break;
						case "WAT":
							//retVal = objCoverages.SaveWatercraftCoverages(alRecr,hidOldData.Value);
							break;

						case "APP":
							//retVal = objCoverages.SaveAppCoverages(alRecr,hidOldData.Value);
							break;
						default:
							//dtCoverages = ClsAppCoverages.GetCoverages("HCVCD",0,0);
							break;

					}


				}


			}
		

			return 1;

		}

	}
}
