/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-22-2006
	<End Date				: ->
	<Description			: -> Page to add umbrella Excess Limit(Policy)
	<Review Date			: ->
	<Reviewed By			: ->
	
	Modification History
******************************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.Umbrella ;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyAddLimit1.
	/// </summary>
	public class PolicyAddLimit1 : Cms.Policies.policiesbase 
	{
		#region Page Controls Declaration
		protected System.Web.UI.WebControls.CustomValidator csvOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capPOLICY_LIMITS;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_LIMITS;
		protected System.Web.UI.WebControls.Label capRETENTION_LIMITS;
		protected System.Web.UI.WebControls.TextBox txtRETENTION_LIMITS;
		protected System.Web.UI.WebControls.RangeValidator rngRETENTION_LIMITS;
		protected System.Web.UI.WebControls.Label capUNINSURED_MOTORIST_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtUNINSURED_MOTORIST_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUNINSURED_MOTORIST_LIMIT;
		protected System.Web.UI.WebControls.Label capUNDERINSURED_MOTORIST_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtUNDERINSURED_MOTORIST_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUNDERINSURED_MOTORIST_LIMIT;
		protected System.Web.UI.WebControls.Label capOTHER_LIMIT;
		protected System.Web.UI.WebControls.TextBox txtOTHER_LIMIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOTHER_LIMIT;
		protected System.Web.UI.WebControls.Label capOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.TextBox txtOTHER_DESCRIPTION;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.DataGrid  dgCoverages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldCoverageXml;
		protected System.Web.UI.WebControls.Label capTERRITORY;
		protected System.Web.UI.WebControls.DropDownList cmbTERRITORY;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_LIMITS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRETENTION_LIMITS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERRITORY;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnPOLICY_LIMITS;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnRETENTION_LIMITS;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnTERRITORY;

		protected System.Web.UI.WebControls.Label capCLIENT_UPDATE_DATE;
		protected System.Web.UI.WebControls.TextBox txtCLIENT_UPDATE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkCLIENT_UPDATE_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCLIENT_UPDATE_DATE;


		System.Data.DataSet dsCoverage=null;
		#endregion
	
		#region Page Load Event Handler
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "273_0";

			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");

			btnReset.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString =	gstrSecurityXML;	
				
			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;	
						
			if ( !Page.IsPostBack )
			{
				hlkCLIENT_UPDATE_DATE.Attributes.Add("OnClick","fPopCalendar(document.POL_UMBRELLA_LIMITS.txtCLIENT_UPDATE_DATE,document.POL_UMBRELLA_LIMITS.txtCLIENT_UPDATE_DATE)");                     
				hidCustomerID.Value = GetCustomerID();
				hidPolicyID .Value = GetPolicyID ();
				hidPolicyVersionID .Value = GetPolicyVersionID ();
			
				if ( hidCustomerID.Value != "" && hidCustomerID.Value != "0" && 
					hidPolicyID .Value != "" && hidPolicyID.Value != "0" &&
					hidPolicyVersionID .Value != "" && hidPolicyVersionID .Value != "0"
					)

				{
					SetCaptions();
					SetValidators();
					FillControls();

					trError.Visible = false;

					LoadData();
				    Bindgrid();
					SetWorkFlow();
					this.txtOTHER_LIMIT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
					this.txtRETENTION_LIMITS.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
					this.txtUNDERINSURED_MOTORIST_LIMIT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
					this.txtUNINSURED_MOTORIST_LIMIT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
					txtRETENTION_LIMITS.Style["text-align"] = "right"; 
					
				}
				else
				{
					trBody.Attributes.Add("style","display:none");
					txtRETENTION_LIMITS.Text="250";
					lblError.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","118");
					trError.Visible = true;

				}


			}
		}
		#endregion

		#region  PopulateList  Function

		private void PopulateList(ArrayList alRecr,DataGrid dgCoverages)
		{
			
			//string hidtext="";
			//int limitId=0;

			foreach(DataGridItem dgi in dgCoverages.Items)
			{
				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
				{
					//Get the checkbox
					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");

					Label lblCOV_DESC =  ((Label)dgi.FindControl("lblCOV_DESC"));
					Label lblCOV_CODE = ((Label)dgi.FindControl("lblCOV_CODE"));
					Label lblCOV_ID = ((Label)dgi.FindControl("lblCOV_ID"));
					//Label lblCOV_TYPE = ((Label)dgi.FindControl("lblCOV_TYPE"));
					
					//Model object					
					Cms.Model.Policy.ClsPolicyCoveragesInfo objInfo = new Cms.Model.Policy.ClsPolicyCoveragesInfo();

					objInfo.CUSTOMER_ID = Convert.ToInt32(GetCustomerID());
					objInfo.POLICY_ID = Convert.ToInt32(GetPolicyID());
					objInfo.POLICY_VERSION_ID = Convert.ToInt32(GetPolicyVersionID());
					objInfo.COV_DESC = ClsCommon.EncodeXMLCharacters(lblCOV_DESC.Text.Trim());
					objInfo.COVERAGE_CODE = lblCOV_CODE.Text.Trim();
					objInfo.COVERAGE_CODE_ID = Convert.ToInt32(lblCOV_ID.Text.Trim());
					//objInfo.COVERAGE_TYPE = lblCOV_TYPE.Text.Trim();

					//int.Parse(GetUserId());
					objInfo.CREATED_BY = int.Parse(GetUserId());
					objInfo.MODIFIED_BY=int.Parse(GetUserId());
					objInfo.CREATED_DATETIME =DateTime.Now;
					objInfo.LAST_UPDATED_DATETIME =DateTime.Now ;
					if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
					{
						objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
					}
					
					int row = dgi.ItemIndex + 2;
					
					HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidcbDelete");
					
					bool isChecked = false;
					
					//if ( hidCbDelete.Value  == "true" )
					if (cbDelete.Checked)
					{
						isChecked = true;
					}
					else
					{
						isChecked = false;
					}

					
					
					if (isChecked)
					{		
						objInfo.COVERAGE_CODE_ID = Convert.ToInt32(
							((Label)dgi.FindControl("lblCOV_ID")).Text
							);
						
						if ( objInfo.COVERAGE_ID == -1 )
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
						}

						alRecr.Add(objInfo);

					}
					else	//checkbox.checked == false
					{


						if ( dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							if ( objInfo.COVERAGE_ID != -1 )
							{
								objInfo.CUSTOMER_ID = Convert.ToInt32(GetCustomerID());
								objInfo.POLICY_ID = Convert.ToInt32(GetPolicyID());
								objInfo.POLICY_VERSION_ID = Convert.ToInt32(GetPolicyVersionID());
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


		#endregion
		
		#region "Get The Coverage and Bind With Grid"
		private void Bindgrid()
		{
	        
			ClsUmbrellaCoverages objCoverages=new ClsUmbrellaCoverages();
			dsCoverage=objCoverages.GetPolicyUmbrellaCoverage(Convert.ToInt32(GetCustomerID()),Convert.ToInt32(GetPolicyID()),Convert.ToInt32(GetPolicyVersionID()),Convert.ToString("N"));    
			DataTable dataTable = dsCoverage.Tables[0];
			hidOldCoverageXml.Value =ClsCommon.GetXMLEncoded(dataTable);
			dgCoverages.DataSource = dsCoverage;
			dgCoverages.DataBind();
    
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
			if(e.Item.ItemType  == ListItemType.Item  || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				CheckBox cbDelete=(CheckBox)e.Item.FindControl("cbDelete");
				Label   lblCOV_CODE=(Label )e.Item.FindControl("lblCOV_CODE");
				DataRowView drvItem=(DataRowView)e.Item.DataItem;
				string strCovecode=(string)drvItem["COV_CODE"];
				if(drvItem["COVERAGE_ID"]!=System.DBNull.Value )
				{
					cbDelete.Checked =true;
				}
				else
				{
					cbDelete.Checked =false;
				}
					//This code is modified for manually saved coverages when checked remain checked.
			
					if(drvItem["HAS_MOTORIST_PROTECTION"]!=System.DBNull.Value )
					{
						if(drvItem["HAS_MOTORIST_PROTECTION"].ToString() =="1" && strCovecode == "UBEXUMCOV")
						{
							cbDelete.Checked =true;
							cbDelete.Enabled =false;
						}
						else if(drvItem["HAS_MOTORIST_PROTECTION"].ToString() =="0" && strCovecode == "UBEXUMCOV") 
						{
							//cbDelete.Checked =false;
							cbDelete.Enabled =true;

						}
					}
					if(drvItem["LOWER_LIMITS"]!=System.DBNull.Value )
					{
						if(drvItem["LOWER_LIMITS"].ToString() =="1" && strCovecode == "UBEXUMOT")
						{
							cbDelete.Checked =true;
							cbDelete.Enabled =false;
							
						}
						else if(drvItem["LOWER_LIMITS"].ToString() =="0" && strCovecode == "UBEXUMOT")
						{
							//cbDelete.Checked =false;
							cbDelete.Enabled =true;

						}
					}

					if(drvItem["IS_BOAT_EXCLUDED"]!=System.DBNull.Value )
					{
						if(drvItem["IS_BOAT_EXCLUDED"].ToString() =="1" && strCovecode == "UBDW")
						{
							cbDelete.Checked =true;
							cbDelete.Enabled =false;
							
						}
						else if(drvItem["IS_BOAT_EXCLUDED"].ToString() =="0" && strCovecode == "UBDW")
						{
							//cbDelete.Checked =false;
							cbDelete.Enabled =true;

						}
					}

					if(drvItem["LOC_EXCLUDED"]!=System.DBNull.Value )
					{
						if(drvItem["LOC_EXCLUDED"].ToString() =="1" && strCovecode == "UBAHAZ")
						{
							cbDelete.Checked =true;
							cbDelete.Enabled =false;
							
						}
						else if(drvItem["LOC_EXCLUDED"].ToString() =="0" && strCovecode == "UBAHAZ")
						{
							//cbDelete.Checked =false;
							cbDelete.Enabled =true;

						}
					}
					if(drvItem["IS_EXCLUDED"]!=System.DBNull.Value )
					{
						if(drvItem["IS_EXCLUDED"].ToString() =="1" && strCovecode == "UBEXDAE")
						{
							cbDelete.Checked =true;
							cbDelete.Enabled =false;
							
						}
						else if(drvItem["IS_EXCLUDED"].ToString() =="0" && strCovecode == "UBEXDAE")
						{
							//cbDelete.Checked =false;
							cbDelete.Enabled =true;

						}
					}
					if(drvItem["IS_RV_EXCLUDED"]!=System.DBNull.Value )
					{
						if(drvItem["IS_RV_EXCLUDED"].ToString() =="1" && strCovecode == "UBDRMV")
						{
							cbDelete.Checked =true;
							cbDelete.Enabled =false;
						}
						if(drvItem["IS_RV_EXCLUDED"].ToString() =="0" && strCovecode == "UBDRMV")
						{
							//cbDelete.Checked =false;
							cbDelete.Enabled =true;					
						}					
					}

					if(drvItem["AUTO_LIABILITY"]!=System.DBNull.Value )
					{
						if(drvItem["AUTO_LIABILITY"].ToString() =="1" && strCovecode == "UBAUTLIB")
						{
							cbDelete.Checked =true;
							cbDelete.Enabled =false;
						}
						if(drvItem["AUTO_LIABILITY"].ToString() =="0" && strCovecode == "UBAUTLIB")
						{
							//cbDelete.Checked =false;
							cbDelete.Enabled =true;					
						}					
					}
					
				
				///
				if(drvItem["IS_MANDATORY"]!=System.DBNull.Value )
				{
					if(drvItem["IS_MANDATORY"].ToString() =="1")
					{
						cbDelete.Enabled =false;
						//cbDelete.Checked =true;
					}
//					else
//					{
//						cbDelete.Enabled =true;
//
//					}
				}
			}
			
		}

		#region LoadData Function
		private void LoadData()
		{
			Cms.BusinessLayer.BlApplication.ClsInformationLimits objUmbrella = new Cms.BusinessLayer.BlApplication.ClsInformationLimits();

			DataSet dsUmbrella = objUmbrella.GetPolicyLimit1(
				Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(hidPolicyID.Value),
				Convert.ToInt32(hidPolicyVersionID.Value)
				);

			if ( dsUmbrella.Tables[0].Rows.Count == 0 )
			{
				this.txtRETENTION_LIMITS.Text = "250";
				return;
			}
			
			string strXML = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsUmbrella.Tables[0]);
			this.hidOldData.Value = strXML;
			
			DataTable dtUmbrella = dsUmbrella.Tables[0];
		
			/*if ( dtUmbrella.Rows[0]["POLICY_LIMITS"] != System.DBNull.Value )
			{
				if(Convert.ToDouble(dtUmbrella.Rows[0]["POLICY_LIMITS"])==1000000)
				{
					cmbPOLICY_LIMITS.SelectedIndex =1;
				}
				if(Convert.ToDouble(dtUmbrella.Rows[0]["POLICY_LIMITS"])==2000000)
				{
					cmbPOLICY_LIMITS.SelectedIndex =2;
				}

			}*/
			if(dtUmbrella.Rows[0]["POLICY_LIMITS"]!=null && dtUmbrella.Rows[0]["POLICY_LIMITS"].ToString()!="" && dtUmbrella.Rows[0]["POLICY_LIMITS"].ToString()!="0")
				cmbPOLICY_LIMITS.SelectedValue = dtUmbrella.Rows[0]["POLICY_LIMITS"].ToString();

			if(dtUmbrella.Rows[0]["TERRITORY"]!=null && dtUmbrella.Rows[0]["TERRITORY"].ToString()!="" && dtUmbrella.Rows[0]["TERRITORY"].ToString()!="0")
				cmbTERRITORY.SelectedIndex = cmbTERRITORY.Items.IndexOf(cmbTERRITORY.Items.FindByValue(dtUmbrella.Rows[0]["TERRITORY"].ToString()));
				//cmbTERRITORY.SelectedValue = dtUmbrella.Rows[0]["TERRITORY"].ToString();

			if ( dtUmbrella.Rows[0]["RETENTION_LIMITS"] != System.DBNull.Value )
			{
				this.txtRETENTION_LIMITS.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["RETENTION_LIMITS"]));
			}

			if ( dtUmbrella.Rows[0]["UNINSURED_MOTORIST_LIMIT"] != System.DBNull.Value )
			{
				this.txtUNINSURED_MOTORIST_LIMIT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["UNINSURED_MOTORIST_LIMIT"]));
			}

			if ( dtUmbrella.Rows[0]["UNDERINSURED_MOTORIST_LIMIT"] != System.DBNull.Value )
			{
				this.txtUNDERINSURED_MOTORIST_LIMIT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["UNDERINSURED_MOTORIST_LIMIT"]));
			}
			
			if ( dtUmbrella.Rows[0]["OTHER_LIMIT"] != System.DBNull.Value )
			{
				this.txtOTHER_LIMIT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["OTHER_LIMIT"]));
			}
			
			if ( dtUmbrella.Rows[0]["CLIENT_UPDATE_DATE"] != System.DBNull.Value )
			{
				this.txtCLIENT_UPDATE_DATE.Text=dtUmbrella.Rows[0]["CLIENT_UPDATE_DATE"].ToString();
			}

			this.txtOTHER_DESCRIPTION.Text = Convert.ToString(dtUmbrella.Rows[0]["OTHER_DESCRIPTION"]);
			
		}
		#endregion

		#region SetCaptions Function

		private void SetCaptions()
		{	
			System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyAddLimit1",System.Reflection.Assembly.GetExecutingAssembly());
			capPOLICY_LIMITS.Text = objResourceMgr.GetString("cmbPOLICY_LIMITS");
			capRETENTION_LIMITS.Text = objResourceMgr.GetString("txtRETENTION_LIMITS");
			capUNINSURED_MOTORIST_LIMIT.Text = objResourceMgr.GetString("txtUNINSURED_MOTORIST_LIMIT");
			capUNDERINSURED_MOTORIST_LIMIT.Text = objResourceMgr.GetString("txtUNDERINSURED_MOTORIST_LIMIT");
			capOTHER_LIMIT.Text	= objResourceMgr.GetString("txtOTHER_LIMIT");
			capOTHER_DESCRIPTION.Text = objResourceMgr.GetString("txtOTHER_DESCRIPTION");
			capCLIENT_UPDATE_DATE.Text = objResourceMgr.GetString("txtCLIENT_UPDATE_DATE");
		}
		#endregion

		#region SetValidators Function
		private void SetValidators()
		{
			this.rngRETENTION_LIMITS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"8");
			this.revUNINSURED_MOTORIST_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.revUNINSURED_MOTORIST_LIMIT.ValidationExpression = aRegExpDoublePositiveNonZero;
			this.revUNDERINSURED_MOTORIST_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.revUNDERINSURED_MOTORIST_LIMIT.ValidationExpression = aRegExpDoublePositiveNonZero;
			this.revOTHER_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","216");
			this.revOTHER_LIMIT.ValidationExpression = aRegExpDoublePositiveNonZero;
			this.csvOTHER_DESCRIPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("734");
			this.txtOTHER_DESCRIPTION.MaxLength=65;
			this.txtRETENTION_LIMITS.ReadOnly = true;
			this.rfvPOLICY_LIMITS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","956");
			this.rfvRETENTION_LIMITS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","957");
			this.rfvTERRITORY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("G","958");
			//by pravesh
			revCLIENT_UPDATE_DATE.ValidationExpression	= aRegExpDate;
			revCLIENT_UPDATE_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");	
		}
		#endregion

		#region SetWorkFlow Function
		private void SetWorkFlow()
		{
			if(base.ScreenId == "273_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule ="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
			
		}
		#endregion

		#region FillControls Function
		private void FillControls()
		{
			cmbPOLICY_LIMITS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("UMBRL",null,"S");
			cmbPOLICY_LIMITS.DataTextField="LookupDesc";
			cmbPOLICY_LIMITS.DataValueField ="LookupCode";
			cmbPOLICY_LIMITS.DataBind();			
			cmbPOLICY_LIMITS.Items.Insert(0,"");

			//Swarup (05/02/07)To populate cmbTERRITORY Dropdown
			//int state_Id=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.GetStateIdForApplication(int.Parse(hidCustomerID.Value.ToString()),int.Parse(hidPolicyID.Value.ToString()),int.Parse(hidPolicyVersionID.Value.ToString()));
			int state_Id=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.GetStateIdForpolicy(int.Parse(hidCustomerID.Value.ToString()),hidPolicyID.Value.ToString(),hidPolicyVersionID.Value.ToString());
			if (state_Id==14)
				cmbTERRITORY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TERIN",null,"S");
			else if(state_Id ==22)
				cmbTERRITORY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TERMI",null,"S");
			cmbTERRITORY.DataTextField="LookupDesc";
			cmbTERRITORY.DataValueField ="LookupID";
			cmbTERRITORY.DataBind();			
			cmbTERRITORY.Items.Insert(0,"");
			//cmbTERRITORY.SelectedIndex=0;

		}
		#endregion

		#region Private Function Save
		private int Save()
		{
			ClsInformationLimits objUmbrella = new ClsInformationLimits();
			objUmbrella.LoggedInUserId = int.Parse(GetUserId());
			ClsPolicyLimitsInfo  objNewInfo = new ClsPolicyLimitsInfo();
			
			objNewInfo.CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);
			objNewInfo.POLICY_ID   = Convert.ToInt32(hidPolicyID .Value);
			objNewInfo.POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID .Value);
			objNewInfo.MODIFIED_BY = Convert.ToInt32(GetUserId());
			objNewInfo.CREATED_BY = Convert.ToInt32(GetUserId());

			//if(cmbPOLICY_LIMITS.SelectedIndex>0)
				//objNewInfo.POLICY_LIMITS =Convert.ToDouble (cmbPOLICY_LIMITS.SelectedItem.Text.Trim ()); 

			if(cmbPOLICY_LIMITS.SelectedItem!=null && cmbPOLICY_LIMITS.SelectedItem.Value!="")
				objNewInfo.POLICY_LIMITS =Convert.ToDouble (cmbPOLICY_LIMITS.SelectedItem.Value);
			
			if (txtRETENTION_LIMITS.Text.Trim() != "")
				objNewInfo.RETENTION_LIMITS = Convert.ToDouble(this.txtRETENTION_LIMITS.Text.Trim());

			objNewInfo.OTHER_DESCRIPTION = txtOTHER_DESCRIPTION.Text.Trim();

			if ( this.txtUNINSURED_MOTORIST_LIMIT.Text.Trim() != "" )
			{
				objNewInfo.UNINSURED_MOTORIST_LIMIT = Convert.ToDouble(this.txtUNINSURED_MOTORIST_LIMIT.Text.Trim());
			}
			
			if ( this.txtUNDERINSURED_MOTORIST_LIMIT.Text.Trim() != "" )
			{
				objNewInfo.UNDERINSURED_MOTORIST_LIMIT = Convert.ToDouble(this.txtUNDERINSURED_MOTORIST_LIMIT.Text.Trim());
			}
			
			if ( this.txtOTHER_LIMIT.Text.Trim() != "" )
			{
				objNewInfo.OTHER_LIMIT = Convert.ToDouble(this.txtOTHER_LIMIT.Text.Trim());
			}
			if(cmbTERRITORY.SelectedItem!=null && cmbTERRITORY.SelectedItem.Value!="")
				objNewInfo.TERRITORY = Convert.ToInt32(cmbTERRITORY.SelectedItem.Value);
			if (txtCLIENT_UPDATE_DATE.Text!="")
				objNewInfo.CLIENT_UPDATE_DATE=Convert.ToDateTime(txtCLIENT_UPDATE_DATE.Text.Trim());

			int retVal = 0;
			
			try
			{
				if ( hidOldData.Value == "" )
				{
					retVal = objUmbrella.AddPolicyLimit1(objNewInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
				}
				else
				{
					ClsPolicyLimitsInfo  objOldInfo = new ClsPolicyLimitsInfo();
					base.PopulateModelObject(objOldInfo,this.hidOldData.Value);

					retVal = objUmbrella.UpdatePolicyLimit1(objOldInfo,objNewInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","31");

				}

			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

				if ( ex.InnerException != null )
				{
					lblMessage.Text = ex.InnerException.Message;
				}

				return -4;
			}

			return 1;
		}
		#endregion
		#region Save Coverages
		private int SaveCoverage()
		{
			ArrayList alRecr = new ArrayList();
			ArrayList alDelete = new ArrayList();
			
			//Populate the model objects for Policy level and Risk level
			PopulateList(alRecr,this.dgCoverages);
			
			Bindgrid();

			//BL class
			ClsUmbrellaCoverages  objCoverages;
			objCoverages = new ClsUmbrellaCoverages(); 
			int retVal = 1;
			try
			{
				//Get the relevant coverages
				
				//retVal = objCoverages.SavePolicyUmbrellaCoverages(alRecr,String(hidCustomerID.Value),String(hidPolicyID.Value),String(hidPolicyVersionID.Value));
				retVal = objCoverages.SavePolicyUmbrellaCoverages(alRecr,hidOldCoverageXml.Value,Convert.ToString(hidCustomerID.Value));
						
				Bindgrid(); 
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
		

		#endregion

		
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
			this.dgCoverages.ItemDataBound +=  new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);   

		}
		#endregion

		#region WebEvent Handler btnSave_Click
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			lblMessage.Visible = true;

			int intRetVal = Save();
			int intRetVal1 = SaveCoverage();
			if ( intRetVal > 0 && intRetVal1>0 )
			{
				base.OpenEndorsementDetails();
				LoadData();
				SetWorkFlow();
			}
		}
		#endregion
	}
}
