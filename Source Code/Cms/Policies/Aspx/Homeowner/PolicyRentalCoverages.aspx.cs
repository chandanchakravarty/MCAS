/******************************************************************************************
<Author					: -  SHAFI
<Start Date				: -	 FEB 20, 2006
<End Date				: -	
<Description			: - Add/Edit page for Rental_covarage
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy;
using Cms.Model.Policy.HomeOwners;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for RentalCoverages.
	/// </summary>
	public class PolicyRentalCoverages :  Cms.Policies.policiesbase 
	{
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.DataGrid dgCoverages;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidALL_PERILL_DEDUCTIBLE_AMT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue2;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPol_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldXml;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolcyType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidControlXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateId;
		private StringBuilder sbCtrlXML = new StringBuilder();
		
		private System.DateTime  AppEffectiveDate;
		RuleData homeRuleData;
		//DataTable dtCoverages = null;
		
		public string calledFrom = "";
		//string strLobId="";
		//DataRow[] drDefault;
		//int currentRow = 0;
		//int totRecords = 0;
		//int currentRowCount = 0;
		
		DataSet dsCoverages = null;
		XmlDocument xmldoc=new XmlDocument() ;
		
		StringBuilder sbScript = new StringBuilder();
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid dgSection2;
		StringBuilder sbDisableScript = new StringBuilder();
		
		StringBuilder sbSection2Script = new StringBuilder();

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			#region "Screen ID"
			
			base.ScreenId	=	"259_6";
			
			
			#endregion

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

				btnSave.Attributes.Add("OnClick","SetMinSubsidence();");

			if ( !Page.IsPostBack)
			{

				hidCustomerID.Value	 =  GetCustomerID();
				hidPolID.Value = GetPolicyID();
				hidPolVersionID.Value = GetPolicyVersionID();

				//Get LOB ID on basis of Custmoer id, Pollication id, Pollication Version Id
				//hidPol_LOB.Value = ClsVehicleInformation.GetPollicationLOBID(hidCustomerID.Value,hidPolID.Value,hidPolVersionID.Value).ToString();
				
				this.hidREC_VEH_ID.Value = Request.QueryString["DWELLINGID"].ToString();

				//btnReset.Attributes.Add("onClick","javascript:return Reset();");
				
				if (Request.QueryString["PageTitle"] != null)
				{
					lblTitle.Text = Request.QueryString["PageTitle"].ToString();
				}

				BindGrid(calledFrom);
				
				SetWorkFlowControl();

			}

		}
		
		/// <summary>
		/// Binds the two grids to the data
		/// </summary>
		/// <param name="calledFrom"></param>
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
			

			//drDefault = dtCoverages.Select("IS_DEFAULT=1");
			
			
			Cms.BusinessLayer.BlApplication.ClsHomeCoverages  objCovInformation = new Cms.BusinessLayer.BlApplication.ClsHomeCoverages();

			int product=0;

			dsCoverages=objCovInformation.GetRentalCoveragesForPolicy(Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(hidPolID.Value),
				Convert.ToInt32(hidPolVersionID.Value),
				Convert.ToInt32(hidREC_VEH_ID.Value),
				"N","S1");

			if(dsCoverages.Tables[2].Rows.Count>0)
			{
				hidPolcyType.Value = dsCoverages.Tables[2].Rows[0]["LOOKUP_VALUE_CODE"].ToString();
				if ( dsCoverages.Tables[2].Rows[0]["POLICY_TYPE"] != System.DBNull.Value )
				{
					product = Convert.ToInt32(dsCoverages.Tables[2].Rows[0]["POLICY_TYPE"]);
				}
				if ( dsCoverages.Tables[2].Rows[0]["STATE_ID"] != System.DBNull.Value )
				{
					hidStateId.Value  = dsCoverages.Tables[2].Rows[0]["STATE_ID"].ToString();
				}
			}

			else
			{
				trBody.Attributes.Add("style","display:none");
				lblError.Text = "Policy Type is not entered,Please <a href='#' onclick='LocationPolicy();'>click here</a> ";
				trError.Visible = true;
				return;
			}
             


			//Fetch Rule Data and add script block to page
			homeRuleData = objHome.GetRuleDataRental(product,Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(hidPolID .Value),
				Convert.ToInt32(hidPolVersionID .Value),
				Convert.ToInt32(hidREC_VEH_ID .Value),"POLICY");
			if (!ClientScript.IsStartupScriptRegistered("ChangeRule"))
			{
				string strChange = @"<script>" + homeRuleData.OnBlurFunction + homeRuleData.OnClickFunction  + "</script>";
				
				ClientScript.RegisterStartupScript(this.GetType(), "ChangeRule",strChange );

			}
			// Get App Effective Date

			AppEffectiveDate=(DateTime)dsCoverages.Tables[4].Rows[0]["APP_EFFECTIVE_DATE"];
			
			DataTable dataTable = dsCoverages.Tables[0];
			hidOldData.Value =  ClsCommon.GetXMLEncoded(dataTable);
            this.sbCtrlXML.Append("<Root>");
			//Bind grid for Section2
			DataView dvSection2 = new DataView(dsCoverages.Tables[0]);
			dvSection2.RowFilter = "COVERAGE_TYPE = 'S2'";
			this.dgSection2.DataSource = dvSection2;
			dgSection2.DataBind();
			///

			///////////////////////////////////////
			
			

			//Remove Coverage E and Coverage F
			

			DataRow[] drRemove = dsCoverages.Tables[0].Select("COVERAGE_TYPE = 'S2'");

			foreach(DataRow dr in drRemove )
			{
				dsCoverages.Tables[0].Rows.Remove(dr);
			}


			dgCoverages.DataSource = dsCoverages.Tables[0];
			dgCoverages.DataBind();
			
			this.hidROW_COUNT.Value = 	dgCoverages.Items.Count.ToString();

			//End tag of control XML
			this.sbCtrlXML.Append("</Root>");
			sbCtrlXML.Replace("\\","");
			this.hidControlXML.Value =         sbCtrlXML.ToString();
			
			
			//DataTable dtCoverageLimits = null;

			

			RegisterScript();
		

		}

		/// <summary>
		/// Injects javascript code in to the page
		/// </summary>
		private void RegisterScript()
		{
			//if ( this.sbScript.ToString() == "" ) return;

			if (!ClientScript.IsStartupScriptRegistered("Test"))
			{
				string strCode = @"<script>" + this.sbScript.ToString() + "</script>";
				
				string strDisable = @"<script>function DisableDDL(){" + this.sbDisableScript.ToString() + ";" + sbSection2Script.ToString() + "} DisableDDL();</script>" ; 
				
				ClientScript.RegisterStartupScript(this.GetType(),"Test",strCode + strDisable);

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
			this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
			this.dgSection2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgSection2_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private int Save()
		{
			ArrayList alRecr = new ArrayList();
			ArrayList alDelete = new ArrayList();

			foreach(DataGridItem dgi in dgCoverages.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");

					//DropDownList ddlLimit = ((DropDownList)dgi.FindControl("ddlLimit"));
					HtmlSelect ddlDeductible = ((HtmlSelect)dgi.FindControl("ddlDeductible"));
					TextBox txtbox = ((TextBox)dgi.FindControl("txtDEDUCTIBLE_1_TYPE"));
					Label lblDEDUCTIBLE_AMOUNT = ((Label)dgi.FindControl("lblDEDUCTIBLE_AMOUNT"));
					System.Web.UI.HtmlControls.HtmlInputHidden hidlbl_DEDUCTIBLE_AMOUNT = (HtmlInputHidden)dgi.FindControl("hidlbl_DEDUCTIBLE_AMOUNT");
					
					TextBox txtLIMIT = ((TextBox)dgi.FindControl("txtLIMIT"));
					//ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	
					//Cms.Model.Policy.Homeowners.ClsPolicyDwellingSectionCoveragesInfo objInfo = new Cms.Model.Policy.Homeowners.ClsPolicyDwellingSectionCoveragesInfo();

					ClsPolicyCoveragesInfo objInfo=new ClsPolicyCoveragesInfo();
					HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidcbDelete");
					
					
					objInfo.RISK_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
					objInfo.MODIFIED_BY = int.Parse(GetUserId());
								
					Label lblCOV_CODE = ((Label)dgi.FindControl("lblCOV_CODE"));
					HtmlInputHidden hidLIMIT = ((HtmlInputHidden)dgi.FindControl("hidLIMIT"));

					HtmlSelect ddlAddDed = (HtmlSelect)dgi.FindControl("ddladdDEDUCTIBLE");
					TextBox txtaddDEDUCTIBLE =(TextBox)dgi.FindControl("txtaddDEDUCTIBLE");
					
					Label lblNoaddDEDUCTIBLE = (Label)dgi.FindControl("lblNoaddDEDUCTIBLE");
				
				

					

					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_VERSION_ID   = Convert.ToInt32(this.hidPolVersionID.Value);
					objInfo.POLICY_ID  = Convert.ToInt32(this.hidPolID.Value);
					objInfo.RISK_ID  = Convert.ToInt32(this.hidREC_VEH_ID.Value);
					objInfo.MODIFIED_BY = int.Parse(GetUserId());
					Label lblCOV_DESC = ((Label)dgi.FindControl("lblCOV_DESC"));					
					
					objInfo.COV_DESC = ClsCommon.EncodeXMLCharacters(lblCOV_DESC.Text.Trim());

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

					if ( hidCbDelete.Value == "true")
					{		
						objInfo.COVERAGE_CODE_ID = Convert.ToInt32(((Label)dgi.FindControl("lblCOV_ID")).Text);

						string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
						string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;
						string strAddDedType =((Label)dgi.FindControl("lblAddDEDUCTIBLE_TYPE")).Text;
						if(((System.Web.UI.HtmlControls.HtmlGenericControl)dgi.FindControl("lblLIMIT")).Visible == true)
						{

							if(hidLIMIT.Value.Trim() != "")
							{
								objInfo.LIMIT_1 = Convert.ToDouble(hidLIMIT.Value.Trim());

							}
						}
						

						switch(strLimitType)
						{
							case "1":
								//Flat
								/*
								if ( ddlLimit.Items.Count > 0 && ddlLimit.SelectedItem.Text.Trim() != "" )
								{
									objInfo.LIMIT_1 = Convert.ToDouble(ddlLimit.SelectedItem.Text);
								}*/

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
								if(((System.Web.UI.HtmlControls.HtmlGenericControl)dgi.FindControl("lblLIMIT")).InnerText.Trim() !="")
								{
									//objInfo.LIMIT_1 = Convert.ToDouble(((Label)dgi.FindControl("lblLIMIT")).Text);
									//objInfo.LIMIT_1 = Convert.ToDouble(((System.Web.UI.HtmlControls.HtmlGenericControl)dgi.FindControl("lblLIMIT")).InnerText.Trim());
									if( hidLIMIT.Value.Trim() != "" )
									{
										objInfo.LIMIT_1 = Convert.ToDouble(hidLIMIT.Value.Trim());
									}
								}
								break;
							case "3":
								//Open
								if ( txtLIMIT.Visible == true )
								{
									if( txtLIMIT.Text.Trim() != "" )
									{
										objInfo.LIMIT_1 = Convert.ToDouble(txtLIMIT.Text.Trim());
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
									objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.Items[ddlDeductible.SelectedIndex].Text);
								}
								break;
							case "2":
								//Split
								if ( ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value  != "" )
								{
									string[] strValues = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split('/');
									objInfo.DEDUCTIBLE_1 =  Convert.ToDouble(strValues[0]);
									objInfo.DEDUCTIBLE_2 =  Convert.ToDouble(strValues[1]);
								}

								break;
							case "0":
								
							case "3":
								//Open
								if(txtbox.Text.Trim()!="")
									objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtbox.Text.Trim());
							
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


						alRecr.Add(objInfo);

					}
					else
					{
						//ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	

						if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							objInfo.ACTION = "D";

							objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
							objInfo.POLICY_VERSION_ID  = Convert.ToInt32(this.hidPolVersionID.Value);
							objInfo.POLICY_ID  = Convert.ToInt32(this.hidPolID.Value);
							objInfo.RISK_ID  = Convert.ToInt32(this.hidREC_VEH_ID.Value);
							objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
							Label lblCOV_DES =  ((Label)dgi.FindControl("lblCOV_DESC"));
							objInfo.COV_DESC = lblCOV_DES.Text.Trim();
							alRecr.Add(objInfo);

							alDelete.Add(objInfo);
						}
					}
					
					

				}
				
			}

			//Save function for Section II coming at bottom
			
		foreach(DataGridItem dgi in dgSection2.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete2");
					
//					if  ( cbDelete.Checked == true && cbDelete.Enabled == false )
//					{
//						continue;
//					}
					HtmlSelect ddlLimit = (HtmlSelect)dgi.FindControl("ddlLimit");
					//DropDownList ddlDeductible = ((DropDownList)dgi.FindControl("ddlDeductible"));
					//TextBox txtbox = ((TextBox)dgi.FindControl("txtDEDUCTIBLE_1_TYPE"));
					//TextBox txtLIMIT = ((TextBox)dgi.FindControl("txtLIMIT"));
					//Label lblLIMIT = ((Label)dgi.FindControl("lblLIMIT"));
					Label lblCOV_DES = ((Label)dgi.FindControl("lblCov_Des"));
					Label lblCOV_CODE = ((Label)dgi.FindControl("lblCov_Des"));
					HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidcbDelete");

					string strCoverageCode = lblCOV_CODE.Text.Trim();

					
					//ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	
					
					Cms.Model.Policy.ClsPolicyCoveragesInfo  objInfo = new Cms.Model.Policy.ClsPolicyCoveragesInfo();

					objInfo.COV_DESC = ClsCommon.EncodeXMLCharacters(lblCOV_DES.Text.Trim());
					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_VERSION_ID  = Convert.ToInt32(this.hidPolVersionID.Value);
					objInfo.POLICY_ID  = Convert.ToInt32(this.hidPolID.Value);
					objInfo.RISK_ID= Convert.ToInt32(this.hidREC_VEH_ID.Value);
					objInfo.MODIFIED_BY = int.Parse(GetUserId());
					
					
					if ( dgSection2.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
					{
						objInfo.COVERAGE_ID = Convert.ToInt32(dgSection2.DataKeys[dgi.ItemIndex]);						
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
					if ( hidCbDelete.Value == "true" )
					{		
						objInfo.COVERAGE_CODE_ID = Convert.ToInt32(((Label)dgi.FindControl("lblCov_Id2")).Text);

						string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
						//string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;
						
						//if ( lblLIMIT.Visible == true )
						//{
						//	if( lblLIMIT.Text.Trim() != "" )
						//	{
						//		objInfo.LIMIT_1 = Convert.ToDouble(lblLIMIT.Text.Trim());
						//	}
						//}

						switch(strLimitType)
						{
								case "1":
									//Flat
									if ( ddlLimit.Items.Count > 0 && ddlLimit.Items[ddlLimit.SelectedIndex].Text.Trim() != "" )
									{
										string amount = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(0,ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
										string text = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
										objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);
	
										//string[] strArr = ddlLimit.SelectedItem.Text.Split(' ',
										if ( amount.Trim() != "" )
										{
											objInfo.LIMIT_1 = Convert.ToDouble(amount);
										}
	
										objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();
								 	}
									break;
		
						}
						
						alRecr.Add(objInfo);

					}

					else
					{
						//ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	

						if ( dgSection2.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
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
							
							alDelete.Add(objInfo);
						}
					}
					
					

				}
				
			}

			/////////////////////////////
			
			ClsHomeCoverages  objCoverages = new ClsHomeCoverages();
			
			int retVal = 1;
			
			try
			{
				
				//retVal = objCoverages.SaveHomeCoverages(alRecr,hidOldData.Value,"S1");
				//retVal = objCoverages.SaveRentalCoveragesForPolicy(alRecr,hidOldData.Value,"S1");
				retVal = objCoverages.SaveRentalCoveragesForPolicy(alRecr,hidOldData.Value,"S1",hidCustomInfo.Value);
				
					
				return 2;

			}
			catch//(Exception ex)
			{
				return -4;
			}
	
			return retVal;
		}
		

		private void SetWorkFlowControl()
		{		
			
	
			if(base.ScreenId	==	"44_2" || base.ScreenId == "81_1" || 
				base.ScreenId == "48_1" || base.ScreenId == "61_6" || 
				base.ScreenId == "259_6" || base.ScreenId == "44_2")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPolID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPolVersionID.Value);
				myWorkFlow.AddKeyValue("DWELLING_ID",hidREC_VEH_ID.Value);
				myWorkFlow.AddKeyValue("COVERAGE_TYPE","'S1'");
					

				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}


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
				
				SetWorkFlowControl();

				base.OpenEndorsementDetails();

				return;
			}

			if ( retVal == -2 )
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","332");
				//lblMessage.Text = "One of the Coverage code already exists. Please enter another code.";
				return;
			}
		}


		private void dgCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
			//Adding Style to Alternating Item
			e.Item.Attributes.Add("Class","midcolora");
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				CheckBox cbDelete = (CheckBox)e.Item.FindControl("cbDelete");
				//DropDownList ddlLimit = (DropDownList)e.Item.FindControl("ddlLIMIT");
				HtmlSelect ddlDed = (HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE");
				Label lblDeductible = (Label)e.Item.FindControl("lblDeductible");
				HtmlGenericControl lblLimit = (HtmlGenericControl)e.Item.FindControl("lblLimit");
				TextBox txtbox = (TextBox)e.Item.FindControl("txtDEDUCTIBLE_1_TYPE");
				TextBox txtLIMIT =(TextBox)e.Item.FindControl("txtLIMIT");
				Label lblNoDeductible = (Label)e.Item.FindControl("lblNoCoverage");
				Label lblNoLimit = (Label)e.Item.FindControl("lblNoCoverageLimit");
				
				
				System.Web.UI.HtmlControls.HtmlInputHidden hidlbl_DEDUCTIBLE_AMOUNT = (HtmlInputHidden)e.Item.FindControl("hidlbl_DEDUCTIBLE_AMOUNT");

				HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidcbDelete");

				RegularExpressionValidator revValidator1 = (RegularExpressionValidator) e.Item.FindControl("revLIMIT_DEDUC_AMOUNT");
				revValidator1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
				revValidator1.ValidationExpression = aRegExpCurrencyformat;
				RequiredFieldValidator rfvLIMIT = (RequiredFieldValidator)e.Item.FindControl("rfvLIMIT");
				RangeValidator rngDWELLING_LIMIT = (RangeValidator)e.Item.FindControl("rngDWELLING_LIMIT");
				RegularExpressionValidator revLIMIT = (RegularExpressionValidator) e.Item.FindControl("revLIMIT");
				CustomValidator csv =  (CustomValidator)e.Item.FindControl("csvLIMIT_DEDUC_AMOUNT");

				HtmlSelect ddlAddDed = (HtmlSelect)e.Item.FindControl("ddladdDEDUCTIBLE");
				TextBox txtaddDEDUCTIBLE =(TextBox)e.Item.FindControl("txtaddDEDUCTIBLE");
				Label lblDEDUCTIBLE_AMOUNT = (Label)e.Item.FindControl("lblDEDUCTIBLE_AMOUNT");
				Label lblNoaddDEDUCTIBLE = (Label)e.Item.FindControl("lblNoaddDEDUCTIBLE");
				


				RangeValidator rngDEDUCTIBLE = (RangeValidator)e.Item.FindControl("rngDEDUCTIBLE");
				
				//Populate the coverage ranges for each coverage
				DataGrid dg = (DataGrid)sender;
				string prefix = dg.ID + "__ctl" + (e.Item.ItemIndex + 2).ToString();
				
				

				//Populate the coverage ranges for each coverage
				DataTable dtRanges = this.dsCoverages.Tables[1];
				
				DataRowView drvItem = (DataRowView)e.Item.DataItem;
				
				if ( drvItem["COVERAGE_ID"] != System.DBNull.Value )						
				{
					cbDelete.Checked = true;
					hidCbDelete.Value ="true";
				}							
				else
				{
					hidCbDelete.Value ="false";
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
				if ( drvItem["IS_MANDATORY"] != System.DBNull.Value ) 
				{
					if ( drvItem["IS_MANDATORY"].ToString() == "Y" ||  drvItem["IS_MANDATORY"].ToString() == "1")
					{
						cbDelete.Enabled = false;
					}

				}													

				int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"COV_ID"));
				string strCov_code = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"COV_CODE"));
				this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code +   "\">" + prefix + "</COV_CODE>");
				
//				txtbox.Attributes.Add("onBlur","OnAdditionalChange('" + strCov_code + "',this)");
//			
//				//Earthquake
//				if ( strCov_code == "EDP469")
//				{
//					lblDEDUCTIBLE_AMOUNT.Text = "500";
//					hidlbl_DEDUCTIBLE_AMOUNT.Value = "500";
//					spnDEDUCTIBLE_AMOUNT_TEXT.InnerText = "5%-";
//				}

							
			

				string conditionalScript="";
				string script="";
				if (strCov_code == "MSC480")
				{
//					rngDEDUCTIBLE.MaximumValue ="200000";
//					rngDEDUCTIBLE.ErrorMessage ="Maximum Limit is 200,000";
//					rngDEDUCTIBLE.Enabled =true;
					csv.ClientValidationFunction ="CoverageMinValidation";
					csv.ErrorMessage = "Mine Subsidence should not be greater than Coverage A or 200,000.";
					csv.Enabled =true;

				}
				
				conditionalScript = "onCheck('" + cbDelete.ClientID + "')" ;

				string disableScript = "DisableItems('" + cbDelete.ClientID + "','" + lblNoLimit.ClientID + "','" + lblNoaddDEDUCTIBLE.ClientID + "','" + 
					lblDEDUCTIBLE_AMOUNT.ClientID + "','" + ddlAddDed.ClientID + "','" + txtaddDEDUCTIBLE.ClientID + "','" + 
					lblNoDeductible.ClientID + "','" + lblLimit.ClientID + "','" + ddlDed.ClientID + "','" + txtbox.ClientID + "','" + prefix + "')"
					;
				
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
					cbDelete.Attributes.Add("onClick","javascript:" + script + ";" + disableScript  + ";" + conditionalScript);
				}
				else
				{
					cbDelete.Attributes.Add("onClick","javascript:" + disableScript + ";" + conditionalScript);
				}

				

				//str = 437,438
				//str = 20,30
				e.Item.Attributes.Add("id","Row_" + intCOV_ID.ToString() );    
				//cbDelete.Attributes.Add("onClick","javascript:OnClickCheck("+(e.Item.ItemIndex+2).ToString()+");EnableDisableControls('" + disable + "',this);");

				
				
				DataView dvLimitsRanges = new DataView(dtRanges);
				DataView dvDedRanges = new DataView(dtRanges);
				DataView dvAddDedRanges =new DataView(dtRanges);

				//dvLimitsRanges = dtRanges.DefaultView;
				//dvDedRanges = dtRanges.DefaultView;

				dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
				dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
				dvAddDedRanges.RowFilter ="COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Addded'";

				//Select the ranges applicable to this Coverage
				DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());
				
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
				

				if(intCOV_ID==33 || intCOV_ID==140)
				{					
					cbDelete.Checked = true;
				}

				if(hidPolcyType.Value.Equals("HO-5 Premier") && intCOV_ID==196)
				{
					cbDelete.Checked = true;
				}

				switch(strLimitType)
				{
					case "3":
						txtLIMIT.Visible=true;
						lblLimit.Visible=false; 
						if(strCov_code == homeRuleData.BaseCoverage )
						{
							rfvLIMIT.Enabled=true;
							rfvLIMIT.ErrorMessage=homeRuleData.ErrRequired;

							revLIMIT.Enabled=true;
							revLIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
							/*Commented by Asfa (04-June-2008) - iTrack #3953
							//revLIMIT.ValidationExpression = aRegExpDoublePositiveWithZero;
							*/
							revLIMIT.ValidationExpression = aRegExpCurrencyformat;
				
							rngDWELLING_LIMIT.Enabled=true;
							rngDWELLING_LIMIT.ErrorMessage=homeRuleData.ErrorMessage;
							rngDWELLING_LIMIT.MinimumValue  = homeRuleData.MinValue;
							rngDWELLING_LIMIT.MaximumValue=homeRuleData.MaxValue;
							txtLIMIT.Attributes.Add("onBlur","javascript:OnCoverageChange();UpdateTreesShrubs();");
						}
						else
						{
							rngDWELLING_LIMIT.Enabled=false;
							revLIMIT.Enabled =false;
							txtLIMIT.Attributes.Add("readOnly","true");
						}
						break;

				}
			
				switch(strDedType)
				{
					case "1":
						//Flat
						ClsHomeCoverages.BindDropDown(ddlDed,dvDedRanges,"LIMIT_1_DISPLAY","LIMIT_DEDUC_ID",AppEffectiveDate);
						ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));


						break;
					case "2":
						//Split
						//Split
						ddlDed.Visible = true;
						ClsHomeCoverages.BindDropDown(ddlDed,dvDedRanges,"","",AppEffectiveDate);
						ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));					

						break;					
					case "3":
						//Open
						//lblDeductible.Visible = false;
						if(strCov_code =="MSC480")
						{
							txtbox.Attributes.Add("OnBlur","SetMinSubsidence();");
							//txtbox.ReadOnly =true;
						}
						//lblDeductible.Visible = false;
						if(strCov_code == "SD" || strCov_code == "BOSTR" )
						{
							txtbox.ReadOnly =true;
						}
						txtbox.Visible = true;
						//ddlDed.Visible = false;
						break;
					case "0":
						//txtbox.Visible = false;
						lblNoDeductible.Visible=true; 
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
						if(lblDed.Trim() == "" && strCov_code != "MSC480")
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
				//ddlLimit.DataSource = dv;
				//ddlLimit.DataBind();
				
				//ddlDed.DataSource = dv;
				//ddlDed.DataBind();
				
				
			}

			
		}

		private void dgSection2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//Adding Style to Alternating Item
			e.Item.Attributes.Add("Class","midcolora");
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{
				CheckBox cbDelete2 = (CheckBox)e.Item.FindControl("cbDelete2");
				Label lblNoLimit = (Label)e.Item.FindControl("lblNoLimit");
				Label lblLIMIT_AMOUNT = (Label)e.Item.FindControl("lblLIMIT_AMOUNT");
				HtmlSelect ddlLIMIT= (HtmlSelect)e.Item.FindControl("ddlLIMIT");

				HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidcbDelete");

				HtmlSelect  ddlLimit = (HtmlSelect)e.Item.FindControl("ddlLIMIT");

				DataRowView drvItem = (DataRowView)e.Item.DataItem;
				
				string strCov_code = "";
				
				DataGrid dg = (DataGrid)sender;
				string prefix = dg.ID + "__ctl" + (e.Item.ItemIndex + 2).ToString();

				if ( drvItem["COV_CODE"] != DBNull.Value )
				{
					strCov_code = Convert.ToString(drvItem["COV_CODE"]);
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
				//Appending Node with previx as value for each Coverage in control XML
				int intCOV_ID = Convert.ToInt32(drvItem["COV_ID"]);
				if ( drvItem["IS_MANDATORY"] != System.DBNull.Value ) 
				{
					if ( drvItem["IS_MANDATORY"].ToString() == "Y" ||  drvItem["IS_MANDATORY"].ToString() == "1")
					{
						cbDelete2.Enabled = false;
					}

				}													

				

				this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code +   "\">" + prefix + "</COV_CODE>");

				DataTable dtRanges = this.dsCoverages.Tables[1];
				DataView dvLimitsRanges = new DataView(dtRanges);

				string strLimitType = drvItem["LIMIT_TYPE"].ToString();
				string strDedType = drvItem["DEDUCTIBLE_TYPE"].ToString();
		
				dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";

				switch(strLimitType)
				{
					case "0":
						ddlLIMIT.Visible =false;
						lblNoLimit.Visible = true;
						lblLIMIT_AMOUNT.Visible = true;
						break;
					case "1":
						ddlLIMIT.Visible =true;
						lblLIMIT_AMOUNT.Visible = false;
						break;
				}

				if ( drvItem["COVERAGE_ID"] != System.DBNull.Value )						
				{
					cbDelete2.Checked = true;
					
					hidCbDelete.Value ="true";
					
					
					ClsHomeCoverages.BindDropDown(ddlLimit,dvLimitsRanges,"LIMIT_1_DISPLAY","LIMIT_DEDUC_ID",DateTime.Now); 
					ClsCoverages.SelectValueInDropDown(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));
	
				}
				else
				{	
					
					hidCbDelete.Value ="false";
						
				}
			
				

				string script = "DisableItemsForSection2('" + cbDelete2.ClientID + "','" +  strCov_code + "','" + prefix + "');";

				cbDelete2.Attributes.Add("onClick",script);
				
				sbSection2Script.Append(script);
				

				this.sbSection2Script.Append(script);
				
			}
		}

	}
}

