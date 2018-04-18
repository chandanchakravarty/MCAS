/******************************************************************************************
<Author					: -   shafi
<Start Date				: -	  22 Feb 06  
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

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for Coverages.
	/// </summary>
	public class PolicyCoverages_Section2 : Cms.Policies.policiesbase
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
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		//	protected Cms.CmsWeb.Controls.CmsButton btnReset;
		//protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPol_LOB;
		string calledFrom = "";
		
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
		private System.DateTime  AppEffectiveDate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		private int All_Data_Valid;

		private void Page_Load(object sender, System.EventArgs e)
		{
			//hidOldXml.Value ="<Root><Home><coverage COV_ID='438' COV_CODE='OS' Name='B'><dependancy><coverage COV_ID='437' COV_CODE='DWELL' Enable='false' Perc='10' /></dependancy></coverage><coverage COV_ID='439' COV_CODE='EBUSPP' Name='C'><dependancy><coverage COV_ID='437' COV_CODE='EXP' Enable='false' Perc='50' Form='3' /><coverage COV_ID='437' COV_CODE='EXP' Enable='false' Perc='50' Form='2' /><coverage COV_ID='437' COV_CODE='EXP' Enable='false' Perc='70' Form='5' /></dependancy></coverage><coverage COV_ID='440' COV_CODE='LOSUR' Name='D'><dependancy><coverage COV_ID='437' COV_CODE='EXP' Enable='false' Perc='30' /></dependancy></coverage></Home></Root>";

			trError.Visible=false;
			// if called from private passenger automobile, otherwise use if else
			string strCalledFrom = "";
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();	
				
			}
			
					base.ScreenId	=	"239_5";
			#endregion



			//btnReset.Attributes.Add("onClick","javascript:return Reset();");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//	btnReset.CmsButtonClass		=	CmsButtonType.Write;
			//	btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

//			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
//			btnDelete.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

			if ( Request.QueryString["CalledFrom"] != null )
			{
				calledFrom =  Request.QueryString["CalledFrom"].ToString();
				hidCalledFrom.Value = calledFrom;
				
				//strLobId= Request.QueryString["LOB_ID"].ToString();
			}

			// Put user code to initialize the page here
			if ( !Page.IsPostBack)
			{

				hidCustomerID.Value	 =  GetCustomerID();
				hidAppID.Value = GetAppID();
				hidAppVersionID.Value = GetAppVersionID();
				hidPolID.Value        = GetPolicyID();
				hidPolVersionID.Value = GetPolicyVersionID(); 

				//Get LOB ID on basis of Custmoer id, Application id, Application Version Id
				//hidAPP_LOB.Value = ClsVehicleInformation.GetApplicationLOBID(hidCustomerID.Value,hidAppID.Value,hidAppVersionID.Value).ToString();
				
				this.hidREC_VEH_ID.Value = Request.QueryString["DWELLINGID"].ToString();

				//Added by Charles on 17-Aug-09 for Itrack 6210
				btnSave.Attributes.Add("onClick","javascript:Page_ClientValidate();return Page_IsValid;");
				//btnReset.Attributes.Add("onClick","javascript:return Reset();");
				
				if (Request.QueryString["PageTitle"] != null)
				{
					lblTitle.Text = Request.QueryString["PageTitle"].ToString();
				}

//				ViewState["CurrentPageIndex"] = 1;
				BindGrid(calledFrom);


			}
			SetWorkFlowControl();
		}

		private void BindGrid(string calledFrom)
		{
//			ClsCoverages objHome = new ClsCoverages();
//			
//			int pageSize = 0;
//
//			if ( System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"] == null )
//			{
//				pageSize = 10;
//			}
//			else
//			{
//				pageSize = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"]);
//			}
//		
//			int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);
//			switch(calledFrom.ToUpper())
//			{	
//				case "HOME":
					ClsHomeCoverages   	objCovInformation = new ClsHomeCoverages();
					dsCoverages=objCovInformation.GetHomeSection2CoveragesForPolicy(Convert.ToInt32(hidCustomerID.Value),
						Convert.ToInt32(hidPolID.Value),
						Convert.ToInt32(hidPolVersionID.Value),
						Convert.ToInt32(hidREC_VEH_ID.Value),
						"N","S2");

					if(dsCoverages.Tables[2].Rows.Count>0)
						hidPolcyType.Value = dsCoverages.Tables[2].Rows[0]["LOOKUP_VALUE_CODE"].ToString();
					else
					{
						trBody.Attributes.Add("style","display:none");
						lblError.Text = "Policy Type is not entered,Please <a href='#' onclick='LocationPolicy();'>click here</a> ";
						trError.Visible = true;
						return;
					}
//					
//					break;
//			
//				default:
//					break;
//
//			}
			
			// Get App Effective Date
			AppEffectiveDate=(DateTime)dsCoverages.Tables[8].Rows[0]["APP_EFFECTIVE_DATE"];
			if(dsCoverages.Tables[8].Rows[0]["ALL_DATA_VALID"] != DBNull.Value)
			{
				All_Data_Valid=Convert.ToInt32(dsCoverages.Tables[8].Rows[0]["ALL_DATA_VALID"].ToString());
			
				if(All_Data_Valid == 2)
				{
					lblMessage.Visible=true;
					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("761");
					this.lblMessage.Attributes.Add("style","display:inline");
				}
			}
			
			dgCoverages.DataSource = dsCoverages.Tables[0];
			dgCoverages.DataBind();
			this.hidROW_COUNT.Value = 	dgCoverages.Items.Count.ToString();
			
			DataTable dataTable = dsCoverages.Tables[0];

			hidOldData.Value =  ClsCommon.GetXMLEncoded(dataTable);
			//RegisterScript();


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
				RegularExpressionValidator revValidator1 = (RegularExpressionValidator) e.Item.FindControl("revLIMIT_DEDUC_AMOUNT");
				RangeValidator rngValidator1=(RangeValidator)e.Item.FindControl("rngtxtLIMIT"); //Added by Charles on 6-Oct-09 for Itrack 6463
				revValidator1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
				revValidator1.ValidationExpression = aRegExpDouble;

				//Populate the coverage ranges for each coverage
				DataTable dtRanges = this.dsCoverages.Tables[1];
				
				DataRowView drvItem = (DataRowView)e.Item.DataItem;
				
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

				int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"COV_ID"));
				string strCOV_CODE = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"COV_CODE"));

				string xpath="/Root/Home/coverage[@COV_ID='" + intCOV_ID.ToString()  + "']";
				string onClickScript = "OnClickCheck("+(e.Item.ItemIndex+2).ToString() + ");";
				string disableScript = "";
				string onCheckCode = "";
				
				//Incidental Office , Private School or Studio - On Premises (HO-42)
				if (strCOV_CODE == "IOPSS" )
				{
					onCheckCode = "onCheck('" + cbDelete.ClientID + "')" ;
				}
				
				if ( disableScript == "" )
				{
					cbDelete.Attributes.Add("onClick","javascript:" + onClickScript + disableScript + ";" + onCheckCode);
				}
				else
				{
					cbDelete.Attributes.Add("onClick","javascript:" + onClickScript + ";" + onCheckCode);
				}

				if (sbScript.ToString() == "" )
				{
					sbScript.Append(onClickScript + disableScript + ";" + onCheckCode);
				}
				else
				{
					sbScript.Append(";" + onClickScript + disableScript + ";" + onCheckCode);
				}

				e.Item.Attributes.Add("id","Row_" + intCOV_ID.ToString() );   
				//cbDelete.Attributes.Add("onClick","javascript:OnClickCheck("+(e.Item.ItemIndex+2).ToString()+");EnableDisableControls('" + disable + "',this);");

				
				
				DataView dvLimitsRanges = new DataView(dtRanges);
				DataView dvDedRanges = new DataView(dtRanges);

				dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
				dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";

				//Select the ranges applicable to this Coverage
				DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());
				
				
				//DropDownList ddlLimit = (DropDownList)e.Item.FindControl("ddlLIMIT");
				HtmlSelect ddlDed = (HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE");

				string strLimitType = drvItem["LIMIT_TYPE"].ToString();
				string strDedType = drvItem["DEDUCTIBLE_TYPE"].ToString();
				
				if ( strLimitType != "" )
				{
					//ddlLimit.Visible = true;
				}
			
				if ( strDedType != "" )
				{
					ddlDed.Visible = true;
				}
				
				Label lblDeductible = (Label)e.Item.FindControl("lblDeductible");
				Label lblLimit = (Label)e.Item.FindControl("lblLimit");
				
				switch(strLimitType)
				{
					case "1":
						//Flat
						if(strCOV_CODE == "PL" || strCOV_CODE =="MEDPM")
						{
							lblDeductible.Visible = false;
							ddlDed.Visible = true;
				
							ClsHomeCoverages.BindDropDown(ddlDed,dvLimitsRanges,"LIMIT_1_DISPLAY","LIMIT_DEDUC_ID",AppEffectiveDate); 
							ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));
			
						}
						else
						{
							lblLimit.Visible = true;
						}

						break;
					case "2":
						//Split
						lblLimit.Visible = true;
						break;
					case "0":
					case "3":
						//Open
						lblLimit.Visible = true;
						break;
				}

				switch(strDedType)
				{
					case "1":
						//Flat
						lblDeductible.Visible = false;
						ddlDed.Visible = true;

						ClsHomeCoverages.BindDropDown(ddlDed,dvLimitsRanges,"LIMIT_DEDUC_AMOUNT","LIMIT_DEDUC_ID",AppEffectiveDate); 
						ClsCoverages.SelectValueInDropDown(ddlDed,DataBinder.Eval(e.Item.DataItem,"LIMIT_ID"));

						/*ddlDed.DataTextField = "LIMIT_DEDUC_AMOUNT";
						ddlDed.DataSource = dvDedRanges;
						ddlDed.DataBind();
						if ( DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1") != System.DBNull.Value 
							) 
						{
							//string strSplitAmt = DataBinder.Eval(e.Item.DataItem,"LIMIT_1") + "/" + DataBinder.Eval(e.Item.DataItem,"LIMIT_2");
							ClsCommon.SelectTextinDDL(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1"));

						}*/	

						break;
					case "2":
						//Split
						lblDeductible.Visible = false;
						ddlDed.Visible = true;

						/*ddlDed.DataTextField = "LIMIT_DEDUC_AMOUNT";
						ddlDed.DataSource = dvDedRanges;
						ddlDed.DataBind();
						if ( DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1") != System.DBNull.Value &&
							DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_2") != System.DBNull.Value 	) 
						{
							string strSplitAmt = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1")) + "/" + 
								Convert.ToString(DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_2"));
							ClsCommon.SelectTextinDDL(ddlDed,strSplitAmt);

						}*/					

						break;
					case "0":
					case "3":
						//Open
						if(strCOV_CODE == "PL" || strCOV_CODE =="MEDPM")
						{
							ddlDed.Visible = true;
						}
						else
						{
							lblDeductible = (Label)e.Item.FindControl("lblDeductible");
							lblDeductible.Visible = true;
							ddlDed.Visible = false;
						}
						break;
				}
				
				TextBox txtLimit =  (TextBox)e.Item.FindControl("txtLIMIT");

				//Make text box visible/invisible according to coverage
				switch(strCOV_CODE.ToUpper() )
				{
					case "REEMN":
						//Residence Employees (number)

					case "APOBI":
						//Additional Premises (Number of Premises) -Occupied by Insured

					case "APOLR":
						//Additional Premises (Number of Premises) -Other Location -Rented to Others (1 Famiy)

					//case "APLOR":   //has been removed form database by pravesh
						//Additional Premises (Number of Premises) -Other Location -Rented to Others (1 Famiy)

					//case "APOLO":  //has been removed form database by pravesh
						//Additional Premises (Number of Premises) -Other Location -Rented to Others (2 Famiy)
					
					case "FLOFO":
						//Farm Liability (number of locations) Owned Farms Operated by Insured's Employees(HO-73)
				
					case "FLOFR":
						//Farm Liability (number of locations) Owned Farms Rented to Others(HO-73)
					case "AROF1":
						//Additional Residence - Other Locations - Rented to others - 1 family(HO-70)
					case "AROF2":
						//Additional Residence - Other Locations - Rented to others - 2 family(HO-70)
						txtLimit.Visible = true;
						//txtLimit.Attributes.Add("style:display","none");
						break;

					case "APRPR": //Moved here by Charles on 6-Oct-09 for Itrack 6463
						//Additional Premises (Number of Premises) -Residence Premises - Rented to Others
						txtLimit.Visible = true;
						rngValidator1.Enabled=true;//Added by Charles on 6-Oct-09 for Itrack 6463
						break;

					default:
						//All others
						txtLimit.Visible = false;
						break;
						//txtLimit.Attributes.Add("style:display","inline");

				}
			}
			
		}
//
//		private void RegisterScript()
//		{
//			if ( this.sbScript.ToString() == "" ) return;
//
//			if (!Page.IsStartupScriptRegistered("Test"))
//			{
//				string strCode = @"<script>" + this.sbScript.ToString() + "</script>";
//
//				Page.RegisterStartupScript("Test",strCode);
//
//			}
//
//		}

//		private void btnPrevious_Click(object sender, System.EventArgs e)
//		{
//			int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);
//
//			ViewState["CurrentPageIndex"] = currentPageIndex - 1;
//
//			if ( Convert.ToInt32(ViewState["CurrentPageIndex"]) == 1 )
//			{
//				//btnPrevious.Enabled = false;
//			}
//
//			BindGrid(calledFrom);
//			SetWorkFlowControl();
//		}

//		private void btnNext_Click(object sender, System.EventArgs e)
//		{
//			int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);
//
//			ViewState["CurrentPageIndex"] = currentPageIndex + 1;
//
//			BindGrid(calledFrom);
//			SetWorkFlowControl();
//		}

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
//						ClsPolicyCoveragesInfo  objInfo = new ClsPolicyCoveragesInfo();
//
//						objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
//						objInfo.POLICY_VERSION_ID  = Convert.ToInt32(this.hidPolVersionID.Value);
//						objInfo.POLICY_ID  = Convert.ToInt32(this.hidPolID.Value);
//						objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
//
//						alDelete.Add(objInfo);
//					}
//
//				}
//			}
//			
//
//			if ( alDelete.Count > 0 )
//			{
//				ClsHomeCoverages  objCoverages = new ClsHomeCoverages();
//				
//				try
//				{
//					switch(calledFrom)
//					{
//						case "UMB":
//						/**	objCoverages.DeleteUmbrellaCoverages(alDelete);**/
//							break;
//						default:
//							objCoverages.DeleteCoveragesForPolicy(alDelete);
//							break;
//					}
//				}
//				catch(Exception ex)
//				{
//					return -2;
//				}
//
//				return 2;
//
//			}
//			
//			return 1;
//			
//		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblMessage.Visible = true;

			int retVal = Save();
			
			if(retVal >0)
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
			
			foreach(DataGridItem dgi in dgCoverages.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");

					HtmlSelect ddlLimit = ((HtmlSelect)dgi.FindControl("ddlLimit"));
					HtmlSelect ddlDeductible = ((HtmlSelect)dgi.FindControl("ddlDeductible"));

					ClsPolicyCoveragesInfo  objInfo = new ClsPolicyCoveragesInfo();	

					objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
					objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
					objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
					objInfo.RISK_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
					Label lblCOV_DESC = ((Label)dgi.FindControl("lblCOV_DESC"));					
					
					objInfo.COV_DESC = lblCOV_DESC.Text.Trim();
					
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

					if ( cbDelete.Checked )
					{		
						objInfo.COVERAGE_CODE_ID = Convert.ToInt32(((Label)dgi.FindControl("lblCOV_ID")).Text);

						string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
						string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;
						if(((Label)dgi.FindControl("lblLIMIT")).Text!="")
						{
							objInfo.LIMIT_1 = Convert.ToDouble(((Label)dgi.FindControl("lblLIMIT")).Text);
						}
						if(((TextBox)dgi.FindControl("txtLIMIT")).Text.Trim()!="")//Added Trim for Itrack 6210 on 4-Aug-09, Charles
						{
							objInfo.LIMIT_1 = Convert.ToDouble(((TextBox)dgi.FindControl("txtLIMIT")).Text.Trim());//Added Trim for Itrack 6210 on 4-Aug-09, Charles
						}
						switch(strLimitType)
						{
							case "1":
								//Flat
								//10    170    171     13
								//For Personal Liability & MEdical Payment
								if(objInfo.COVERAGE_CODE_ID == 10 || objInfo.COVERAGE_CODE_ID == 170 ||
									objInfo.COVERAGE_CODE_ID ==171||objInfo.COVERAGE_CODE_ID ==13)
								{
									if (ddlDeductible.Items.Count > 0 &&  ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "" )
									{
										if ( ddlDeductible.Visible == true )
										{
											string amount = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Substring(0,ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.IndexOf(" "));
											string text = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Substring(ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.IndexOf(" "));
											objInfo.LIMIT_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);
											if ( amount.Trim() != "" )
											{
												objInfo.LIMIT_1 = Convert.ToDouble(amount);
											}
											else
												objInfo.LIMIT_1 =-1;
											objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();
										}
									}

								}
								else
								{
									if (ddlLimit.Items.Count > 0 &&  ddlLimit.Items[ddlLimit.SelectedIndex].Value != "" )
									{
										if ( ddlLimit.Visible == true )
										{
											string amount = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(0,ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
											string text = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
											objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);
											if ( amount.Trim() != "" )
											{
												objInfo.LIMIT_1 = Convert.ToDouble(amount);
											}

											objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();
										}
									}
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
							case "3":
								//Open
							
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
								if ( ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "" )
								{
									string[] strValues = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split('/');
									objInfo.DEDUCTIBLE_1 =  Convert.ToDouble(strValues[0]);
									objInfo.DEDUCTIBLE_2 =  Convert.ToDouble(strValues[1]);
								}

								break;
							case "0":
							case "3":
								//Open
							
								break;
						}

						alRecr.Add(objInfo);

					}
					else
					{
						//ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	

						if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
							objInfo.POLICY_VERSION_ID  = Convert.ToInt32(this.hidPolVersionID.Value);
							objInfo.POLICY_ID  = Convert.ToInt32(this.hidPolID.Value);
							objInfo.RISK_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
							objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
							Label lblCOV_DES =  ((Label)dgi.FindControl("lblCOV_DESC"));
							objInfo.COV_DESC = lblCOV_DES.Text.Trim();
							objInfo.ACTION = "D";
							alRecr.Add(objInfo);
						}
					}
					
				}
				
			}
			
			ClsHomeCoverages  objCoverages = new ClsHomeCoverages();
			
			int retVal = 1;
			
			int customerID = Convert.ToInt32(hidCustomerID.Value);
			int polID = Convert.ToInt32(hidPolID.Value);
			int polVersionID = Convert.ToInt32(hidPolVersionID.Value);
			int dwellingID = Convert.ToInt32(hidREC_VEH_ID.Value);

			try
			{
				//retVal = objCoverages.SaveHomeCoveragesNew(alRecr,hidOldData.Value,"S2",customerID,appID,appVersionID,dwellingID);						
				retVal = objCoverages.SaveHomeCoveragesNewForPolicy(alRecr,hidOldData.Value,"S2",customerID,polID,polVersionID,dwellingID,hidCustomInfo.Value);						
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return -4;
			}
	
			return retVal;
				//Get the relevant coverages
//				switch(calledFrom)
//				{
//					case "UMB":
//					/**	retVal = objCoverages.SaveUmbrellaVehicleCoverages(alRecr,hidOldData.Value);**/
//						break;
//						//					case "PPA":
//						//					case "VEH":
//						//						retVal = objCoverages.SaveVehicleCoverages(alRecr,hidOldData.Value);
//						//						break;
//						//					case "APP":
//						//						retVal = objCoverages.SaveAppCoverages(alRecr,hidOldData.Value);
//						//						break;
//					case "Rental":
//					case "Home":
						//retVal = objCoverages.SaveHomeCoverages(alRecr,hidOldData.Value,"S2");
						//retVal = objCoverages.SaveHomeCoveragesNew(alRecr,hidOldData.Value,"S2");
			
//						break;
//					default:
//						//dtCoverages = ClsAppCoverages.GetCoverages("HCVCD",0,0);
//						break;
//
//				}
//
//				if ( alDelete.Count > 0 )
//				{
//					//ClsCoverages objCoverages = new ClsCoverages();
//				
//					switch(calledFrom)
//					{
//						case "UMB":
//						/**	objCoverages.DeleteUmbrellaCoverages(alDelete);**/
//							break;
//						case "Rental":
//						case "Home":
//							//objCoverages.DeleteHomeCoverages(alDelete);							
//							break;
//						default:
//							objCoverages.DeleteCoveragesForPolicy(alDelete);
//							break;
//					}
//					
//					return 2;
//
//				}

			
		}
		
	
		
		/// <summary>
		/// This function shows the proper control on limit conlumn on grid
		/// </summary>
		/// <param name="intCoveId"></param>
		
//		private void BindComboBox(DropDownList ComboBox, int intCoveId, string type)
//		{
//			int i;
//			DataTable dt = ClsCoverageRange.GetCoverageRangeXml(intCoveId, "N", type,0, 100,out i);
//			ComboBox.DataSource = dt;
//			ComboBox.DataValueField = "LIMIT_DEDUC_ID";
//			ComboBox.DataTextField = "LIMIT_DEDUC_AMOUNT";
//			ComboBox.DataBind();
//
//		}
		
		private void SetWorkFlowControl()
		{
			if(base.ScreenId	==	"239_5" || base.ScreenId == "81_1" || base.ScreenId == "48_1" || base.ScreenId == "61_7" || base.ScreenId == "159_7" || base.ScreenId == "44_2")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPolID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPolVersionID.Value);
				myWorkFlow.AddKeyValue("DWELLING_ID",hidREC_VEH_ID.Value);
				//myWorkFlow.AddKeyValue("COVERAGE_TYPE","'S2'");
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		/*	private void btnReset_Click(object sender, System.EventArgs e)
			{
				BindGrid(calledFrom);
			}	 */

		

	}
}
