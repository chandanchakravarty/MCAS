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

namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyAddLimit2.
	/// </summary>
	public class PolicyAddLimit2 : Cms.Policies.policiesbase 
	{
		#region Page Controls Declaration
		protected System.Web.UI.WebControls.CustomValidator csvCALCULATIONS;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capBASIC;
		protected System.Web.UI.WebControls.TextBox txtBASIC;
		protected System.Web.UI.WebControls.RegularExpressionValidator revBASIC;
		protected System.Web.UI.WebControls.Label capRESIDENCES_OWNER_OCCUPIED;
		protected System.Web.UI.WebControls.TextBox txtRESIDENCES_OWNER_OCCUPIED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRESIDENCES_OWNER_OCCUPIED;
		protected System.Web.UI.WebControls.Label capNUM_OF_RENTAL_UNITS;
		protected System.Web.UI.WebControls.TextBox txtNUM_OF_RENTAL_UNITS;
		protected System.Web.UI.WebControls.RangeValidator rngNUM_OF_RENTAL_UNITS;
		protected System.Web.UI.WebControls.Label capRENTAL_UNITS;
		protected System.Web.UI.WebControls.TextBox txtRENTAL_UNITS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revRENTAL_UNITS;
		protected System.Web.UI.WebControls.Label capNUM_OF_AUTO;
		protected System.Web.UI.WebControls.TextBox txtNUM_OF_AUTO;
		protected System.Web.UI.WebControls.RangeValidator rngNUM_OF_AUTO;
		protected System.Web.UI.WebControls.Label capAUTOMOBILES;
		protected System.Web.UI.WebControls.TextBox txtAUTOMOBILES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUTOMOBILES;
		protected System.Web.UI.WebControls.Label capNUM_OF_OPERATORS;
		protected System.Web.UI.WebControls.TextBox txtNUM_OF_OPERATORS;
		protected System.Web.UI.WebControls.RangeValidator rngNUM_OF_OPERATORS;
		protected System.Web.UI.WebControls.Label capOPER_UNDER_AGE;
		protected System.Web.UI.WebControls.TextBox txtOPER_UNDER_AGE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOPER_UNDER_AGE;
		protected System.Web.UI.WebControls.Label capNUM_OF_UNLIC_RV;
		protected System.Web.UI.WebControls.TextBox txtNUM_OF_UNLIC_RV;
		protected System.Web.UI.WebControls.RangeValidator rngNUM_OF_UNLIC_RV;
		protected System.Web.UI.WebControls.Label capUNLIC_RV;
		protected System.Web.UI.WebControls.TextBox txtUNLIC_RV;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUNLIC_RV;
		protected System.Web.UI.WebControls.Label capNUM_OF_UNINSU_MOTORIST;
		protected System.Web.UI.WebControls.TextBox txtNUM_OF_UNINSU_MOTORIST;
		protected System.Web.UI.WebControls.RangeValidator rngNUM_OF_UNINSU_MOTORIST;
		protected System.Web.UI.WebControls.Label capUNISU_MOTORIST;
		protected System.Web.UI.WebControls.TextBox txtUNISU_MOTORIST;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUNISU_MOTORIST;
		protected System.Web.UI.WebControls.Label capUNDER_INSURED_MOTORIST;
		protected System.Web.UI.WebControls.TextBox txtUNDER_INSURED_MOTORIST;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUNDER_INSURED_MOTORIST;
		protected System.Web.UI.WebControls.Label capWATERCRAFT;
		protected System.Web.UI.WebControls.TextBox txtWATERCRAFT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWATERCRAFT;
		protected System.Web.UI.WebControls.Label capNUM_OF_OTHER;
		protected System.Web.UI.WebControls.TextBox txtNUM_OF_OTHER;
		protected System.Web.UI.WebControls.RangeValidator rngNUM_OF_OTHER;
		protected System.Web.UI.WebControls.Label capOTHER;
		protected System.Web.UI.WebControls.TextBox txtOTHER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOTHER;
		protected System.Web.UI.WebControls.Label capDEPOSIT;
		protected System.Web.UI.WebControls.TextBox txtDEPOSIT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDEPOSIT;
		protected System.Web.UI.WebControls.Label capESTIMATED_TOTAL_PRE;
		protected System.Web.UI.WebControls.TextBox txtESTIMATED_TOTAL_PRE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revESTIMATED_TOTAL_PRE;
		protected System.Web.UI.WebControls.Label capCALCULATIONS;
		protected System.Web.UI.WebControls.TextBox txtCALCULATIONS;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm POL_UMBRELLA_LIMITS2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		#endregion
	
		#region PageLoad Event Handler
		private void Page_Load(object sender, System.EventArgs e)
		{
		
			base.ScreenId = "273_1";

			btnReset.Attributes.Add("onclick","javascript:return ResetForm();");

			btnReset.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString =	gstrSecurityXML;	
				
			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;	
						
			if ( !Page.IsPostBack )
			{
				hidCustomerID.Value = GetCustomerID();
				hidPolicyID.Value = GetPolicyID ();
				hidPolicyVersionID.Value = GetPolicyVersionID();
				
				
				if ( hidCustomerID.Value != "" && hidCustomerID.Value != "0" && 
					hidPolicyID .Value != "" && hidPolicyVersionID .Value != "0" &&
					hidPolicyVersionID .Value != "" && hidPolicyVersionID .Value != "0"
					)
				{
					SetCaptions();
					SetValidators();
					AddAttributes();
					txtBASIC.Style["text-align"] = "right"; 
					txtRESIDENCES_OWNER_OCCUPIED.Style["text-align"] = "right"; 

				}

				LoadData();
				this.SetFocus("txtBASIC");
				SetWorkFlow();
							
			}
		}
		#endregion

		#region AddAttributes Function
		private void AddAttributes()
		{
			//Add amount attributes
			this.txtBASIC.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");		
			this.txtRESIDENCES_OWNER_OCCUPIED.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");					
			this.txtRENTAL_UNITS.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");					
			this.txtAUTOMOBILES.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");					
			this.txtUNLIC_RV.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");					
			this.txtUNDER_INSURED_MOTORIST.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtUNISU_MOTORIST.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtWATERCRAFT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtOTHER.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtDEPOSIT.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			this.txtESTIMATED_TOTAL_PRE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");					
			//this.txtCALCULATIONS.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
		}
		#endregion

		#region SetCaption function
		private void SetCaptions()
		{
			System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.PolicyAddLimit2",System.Reflection.Assembly.GetExecutingAssembly());
			capBASIC.Text =	objResourceMgr.GetString("txtBASIC");
			capRESIDENCES_OWNER_OCCUPIED.Text = objResourceMgr.GetString("txtRESIDENCES_OWNER_OCCUPIED");
			capNUM_OF_RENTAL_UNITS.Text = objResourceMgr.GetString("txtNUM_OF_RENTAL_UNITS");
			capRENTAL_UNITS.Text = objResourceMgr.GetString("txtRENTAL_UNITS");
			capNUM_OF_AUTO.Text = objResourceMgr.GetString("txtNUM_OF_AUTO");
			capAUTOMOBILES.Text = objResourceMgr.GetString("txtAUTOMOBILES");
			capNUM_OF_OPERATORS.Text = objResourceMgr.GetString("txtNUM_OF_OPERATORS"); 
			capOPER_UNDER_AGE.Text = objResourceMgr.GetString("txtOPER_UNDER_AGE");
			capNUM_OF_UNLIC_RV.Text = objResourceMgr.GetString("txtNUM_OF_UNLIC_RV");
			capUNLIC_RV.Text = objResourceMgr.GetString("txtUNLIC_RV");
			capNUM_OF_UNINSU_MOTORIST.Text = objResourceMgr.GetString("txtNUM_OF_UNINSU_MOTORIST");
			capUNISU_MOTORIST.Text = objResourceMgr.GetString("txtUNISU_MOTORIST");
			capUNDER_INSURED_MOTORIST.Text = objResourceMgr.GetString("txtUNDER_INSURED_MOTORIST");
			capWATERCRAFT.Text = objResourceMgr.GetString("txtWATERCRAFT");
			capNUM_OF_OTHER.Text = objResourceMgr.GetString("txtNUM_OF_OTHER"); 
			capOTHER.Text = objResourceMgr.GetString("txtOTHER");
			capDEPOSIT.Text = objResourceMgr.GetString("txtDEPOSIT");
			capESTIMATED_TOTAL_PRE.Text = objResourceMgr.GetString("txtESTIMATED_TOTAL_PRE");
			capCALCULATIONS.Text = objResourceMgr.GetString("txtCALCULATIONS");

		}
		#endregion

		#region SetValidators Function
		private void SetValidators()
		{
			
			this.revBASIC.ErrorMessage = ClsMessages.GetMessage("G","216");
			this.revRESIDENCES_OWNER_OCCUPIED.ErrorMessage = ClsMessages.GetMessage("G","216");
			this.revAUTOMOBILES.ErrorMessage = ClsMessages.GetMessage("G","216");							
			this.revUNLIC_RV.ErrorMessage = ClsMessages.GetMessage("G","216");					
			this.revUNISU_MOTORIST.ErrorMessage = ClsMessages.GetMessage("G","216");						
			this.revWATERCRAFT.ErrorMessage = ClsMessages.GetMessage("G","216");						
			this.revOTHER.ErrorMessage = ClsMessages.GetMessage("G","216");						
			this.revDEPOSIT.ErrorMessage = ClsMessages.GetMessage("G","216");								
			this.revESTIMATED_TOTAL_PRE.ErrorMessage = ClsMessages.GetMessage("G","216");					
			this.revRENTAL_UNITS.ErrorMessage = ClsMessages.GetMessage("G","216");		
			this.revOPER_UNDER_AGE.ErrorMessage = ClsMessages.GetMessage("G","216");
			this.revUNDER_INSURED_MOTORIST.ErrorMessage = ClsMessages.GetMessage("G","216");

			this.revBASIC.ValidationExpression = aRegExpDoublePositiveNonZero;				
			this.revRESIDENCES_OWNER_OCCUPIED.ValidationExpression = aRegExpDoublePositiveNonZero;		
			this.revAUTOMOBILES.ValidationExpression = aRegExpDoublePositiveZero;									
			this.revUNLIC_RV.ValidationExpression = aRegExpDoublePositiveZero;			
			this.revUNISU_MOTORIST.ValidationExpression = aRegExpDoublePositiveZero;			
			this.revWATERCRAFT.ValidationExpression = aRegExpDoublePositiveZero;			
			this.revOTHER.ValidationExpression = aRegExpDoublePositiveZero;				
			this.revDEPOSIT.ValidationExpression = aRegExpDoublePositiveZero;				
			this.revESTIMATED_TOTAL_PRE.ValidationExpression =aRegExpDoublePositiveZero;		
			this.revUNDER_INSURED_MOTORIST.ValidationExpression = aRegExpDoublePositiveZero;
			this.revWATERCRAFT.ValidationExpression = aRegExpDoublePositiveZero;
			this.revRENTAL_UNITS.ValidationExpression = aRegExpDoublePositiveZero;
			this.revOPER_UNDER_AGE.ValidationExpression =aRegExpDoublePositiveZero;

			rngNUM_OF_RENTAL_UNITS.ErrorMessage = ClsMessages.GetMessage("G","217");
			rngNUM_OF_OPERATORS.ErrorMessage = ClsMessages.GetMessage("G","217");
			rngNUM_OF_UNLIC_RV.ErrorMessage = ClsMessages.GetMessage("G","217");
			rngNUM_OF_UNINSU_MOTORIST.ErrorMessage = ClsMessages.GetMessage("G","217");
			rngNUM_OF_OTHER.ErrorMessage = ClsMessages.GetMessage("G","217");
			rngNUM_OF_AUTO.ErrorMessage =ClsMessages.GetMessage("G","217");
			this.csvCALCULATIONS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("735");
		}
		#endregion

		#region SetWorkFlow Function
		private void SetWorkFlow()
		{
			if(base.ScreenId == "273_1")
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

		#region LoadData Function 
		private void LoadData()
		{
			ClsInformationLimits objUmbrella = new ClsInformationLimits();
			//DataSet dsUmbrella=null;
			
			DataSet dsUmbrella = objUmbrella.GetPolicyLimit2( 
				Convert.ToInt32(hidCustomerID.Value),
				Convert.ToInt32(hidPolicyID .Value),
				Convert.ToInt32(hidPolicyVersionID .Value)
				);
			

			if ( dsUmbrella.Tables[0].Rows.Count == 0 )
			{
				return;
			}
			this.hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dsUmbrella.Tables[0]);
			DataTable dtUmbrella = dsUmbrella.Tables[0];
			
			if ( dtUmbrella.Rows[0]["BASIC"] != System.DBNull.Value )
			{														 
				this.txtBASIC.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["BASIC"]));
			}

			if ( dtUmbrella.Rows[0]["RESIDENCES_OWNER_OCCUPIED"] != System.DBNull.Value )
			{
				this.txtRESIDENCES_OWNER_OCCUPIED.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["RESIDENCES_OWNER_OCCUPIED"]));
			}
			
			if ( dtUmbrella.Rows[0]["CALCULATIONS"] != System.DBNull.Value )
			{
				this.txtCALCULATIONS.Text = dtUmbrella.Rows[0]["CALCULATIONS"].ToString();
			}
			
			if ( dtUmbrella.Rows[0]["DEPOSIT"] != System.DBNull.Value )
			{
				this.txtDEPOSIT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["DEPOSIT"]));
			}
			
			if ( dtUmbrella.Rows[0]["ESTIMATED_TOTAL_PRE"] != System.DBNull.Value )
			{
				this.txtESTIMATED_TOTAL_PRE.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["ESTIMATED_TOTAL_PRE"]));
			}
			
			if ( dtUmbrella.Rows[0]["NUM_OF_AUTO"] != System.DBNull.Value )
			{
				this.txtNUM_OF_AUTO.Text = Convert.ToString(dtUmbrella.Rows[0]["NUM_OF_AUTO"]);
			}
			
			if ( dtUmbrella.Rows[0]["NUM_OF_OPERATORS"] != System.DBNull.Value )
			{
				this.txtNUM_OF_OPERATORS.Text = Convert.ToString(dtUmbrella.Rows[0]["NUM_OF_OPERATORS"]);
			}
			
			if ( dtUmbrella.Rows[0]["NUM_OF_OTHER"] != System.DBNull.Value )
			{
				this.txtNUM_OF_OTHER.Text = Convert.ToString(dtUmbrella.Rows[0]["NUM_OF_OTHER"]);
			}
			
			if ( dtUmbrella.Rows[0]["NUM_OF_RENTAL_UNITS"] != System.DBNull.Value )
			{
				this.txtNUM_OF_RENTAL_UNITS.Text = Convert.ToString(dtUmbrella.Rows[0]["NUM_OF_RENTAL_UNITS"]);
			}
			
			if ( dtUmbrella.Rows[0]["NUM_OF_UNINSU_MOTORIST"] != System.DBNull.Value )
			{
				this.txtNUM_OF_UNINSU_MOTORIST.Text = Convert.ToString(dtUmbrella.Rows[0]["NUM_OF_UNINSU_MOTORIST"]);
			}
			
			if ( dtUmbrella.Rows[0]["NUM_OF_UNLIC_RV"] != System.DBNull.Value )
			{
				this.txtNUM_OF_UNLIC_RV.Text = Convert.ToString(dtUmbrella.Rows[0]["NUM_OF_UNLIC_RV"]);
			}
			
			if ( dtUmbrella.Rows[0]["OPER_UNDER_AGE"] != System.DBNull.Value )
			{
				this.txtOPER_UNDER_AGE.Text = Convert.ToString(dtUmbrella.Rows[0]["OPER_UNDER_AGE"]);
			}
			
			if ( dtUmbrella.Rows[0]["OTHER"] != System.DBNull.Value)
			{
				this.txtOTHER.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["OTHER"]));
			}
			
			if ( dtUmbrella.Rows[0]["RENTAL_UNITS"] != System.DBNull.Value )
			{
				this.txtRENTAL_UNITS.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["RENTAL_UNITS"]));
			}
			
			if ( dtUmbrella.Rows[0]["UNDER_INSURED_MOTORIST"] != System.DBNull.Value )
			{
				this.txtUNDER_INSURED_MOTORIST.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["UNDER_INSURED_MOTORIST"]));
			}
			
			if ( dtUmbrella.Rows[0]["UNISU_MOTORIST"] != System.DBNull.Value )
			{
				this.txtUNISU_MOTORIST.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["UNISU_MOTORIST"]));
			}
			
			
			if ( dtUmbrella.Rows[0]["UNLIC_RV"] != System.DBNull.Value )
			{
				this.txtUNLIC_RV.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["UNLIC_RV"]));
			}
			
			if ( dtUmbrella.Rows[0]["WATERCRAFT"] != System.DBNull.Value )
			{
				this.txtWATERCRAFT.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["WATERCRAFT"]));
			}

			if ( dtUmbrella.Rows[0]["AUTOMOBILES"] != System.DBNull.Value )
			{
				//this.txtAUTOMOBILES.Text = Convert.ToString(dtUmbrella.Rows[0]["AUTOMOBILES"]);
				this.txtAUTOMOBILES.Text=String.Format("{0:,#,###}",Convert.ToInt64(dtUmbrella.Rows[0]["AUTOMOBILES"]));
			}
				
		}
		#endregion

		#region Save Function 
		private int Save()
		{
			
			ClsInformationLimits objUmbrella = new ClsInformationLimits();
			objUmbrella.LoggedInUserId = int.Parse(GetUserId());
			ClsPolicyLimitsInfo objNewInfo = new ClsPolicyLimitsInfo();
			
			objNewInfo.CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);
			objNewInfo.POLICY_ID   = Convert.ToInt32(hidPolicyID.Value);
			objNewInfo.POLICY_VERSION_ID  = Convert.ToInt32(hidPolicyVersionID.Value);
			objNewInfo.MODIFIED_BY = Convert.ToInt32(GetUserId());
			objNewInfo.CREATED_BY = Convert.ToInt32(GetUserId());

			if ( txtAUTOMOBILES.Text.Trim() != "" )
			{
				objNewInfo.AUTOMOBILES = Convert.ToDouble(txtAUTOMOBILES.Text.Trim());
			}
			
			if ( txtBASIC.Text.Trim() != "" )
			{
				objNewInfo.BASIC = Convert.ToDouble(txtBASIC.Text.Trim());
			}
			
			if ( txtCALCULATIONS.Text.Trim() != "" )
			{
				objNewInfo.CALCULATIONS = txtCALCULATIONS.Text.Trim();
			}
					
			if ( txtDEPOSIT.Text.Trim() != "" )
			{
				objNewInfo.DEPOSIT = Convert.ToDouble(txtDEPOSIT.Text.Trim());
			}

			if ( txtESTIMATED_TOTAL_PRE.Text.Trim() != "" )
			{
				objNewInfo.ESTIMATED_TOTAL_PRE = Convert.ToDouble(txtESTIMATED_TOTAL_PRE.Text.Trim());
			}

			if ( txtNUM_OF_AUTO.Text.Trim() != "" )
			{
				objNewInfo.NUM_OF_AUTO = Convert.ToInt32(txtNUM_OF_AUTO.Text.Trim());
			}

			if ( txtNUM_OF_OPERATORS.Text.Trim() != "" )
			{
				objNewInfo.NUM_OF_OPERATORS = Convert.ToInt32(txtNUM_OF_OPERATORS.Text.Trim());
			}
			
			if ( txtNUM_OF_OTHER.Text.Trim() != "" )
			{
				objNewInfo.NUM_OF_OTHER = Convert.ToInt32(txtNUM_OF_OTHER.Text.Trim());
			}
			
			if ( txtNUM_OF_RENTAL_UNITS.Text.Trim() != "" )
			{
				objNewInfo.NUM_OF_RENTAL_UNITS = Convert.ToInt32(txtNUM_OF_RENTAL_UNITS.Text.Trim());
			}
			
			if ( txtNUM_OF_UNINSU_MOTORIST.Text.Trim() != "" )
			{
				objNewInfo.NUM_OF_UNINSU_MOTORIST = Convert.ToInt32(txtNUM_OF_UNINSU_MOTORIST.Text.Trim());
			}
			
			if ( txtNUM_OF_UNLIC_RV.Text.Trim() != "" )
			{
				objNewInfo.NUM_OF_UNLIC_RV = Convert.ToInt32(txtNUM_OF_UNLIC_RV.Text.Trim());
			}

			if ( txtOPER_UNDER_AGE.Text.Trim() != "" )
			{
				objNewInfo.OPER_UNDER_AGE = Convert.ToDouble(txtOPER_UNDER_AGE.Text.Trim());
			}
			
			if ( txtOTHER.Text.Trim() != "" )
			{
				objNewInfo.OTHER = Convert.ToDouble(txtOTHER.Text.Trim());
			}

			if ( txtRENTAL_UNITS.Text.Trim() != "" )
			{
				objNewInfo.RENTAL_UNITS = Convert.ToDouble(txtRENTAL_UNITS.Text.Trim());
			}
			
			if ( txtRESIDENCES_OWNER_OCCUPIED.Text.Trim() != "" )
			{
				objNewInfo.RESIDENCES_OWNER_OCCUPIED = Convert.ToDouble(txtRESIDENCES_OWNER_OCCUPIED.Text.Trim());
			}
			
		
			if ( txtUNDER_INSURED_MOTORIST.Text.Trim() != "" )
			{
				objNewInfo.UNDER_INSURED_MOTORIST = Convert.ToDouble(txtUNDER_INSURED_MOTORIST.Text.Trim());
			}

			if ( txtUNISU_MOTORIST.Text.Trim() != "" )
			{
				objNewInfo.UNISU_MOTORIST = Convert.ToDouble(txtUNISU_MOTORIST.Text.Trim());
			}
			
			if ( txtUNLIC_RV.Text.Trim() != "" )
			{
				objNewInfo.UNLIC_RV = Convert.ToDouble(txtUNLIC_RV.Text.Trim());
			}

			if ( txtWATERCRAFT.Text.Trim() != "" )
			{
				objNewInfo.WATERCRAFT =  Convert.ToDouble(txtWATERCRAFT.Text.Trim());
			}
			
			int intRetVal = 0;
			
			try
			{
				if ( hidOldData.Value == "" )
				{
					intRetVal = objUmbrella.AddPolicyLimit2 (objNewInfo);
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","29");
				}
				else
				{
					ClsPolicyLimitsInfo  objOldInfo = new ClsPolicyLimitsInfo();

					base.PopulateModelObject(objOldInfo,hidOldData.Value);
				
					intRetVal = objUmbrella.UpdatePolicyLimit2(objOldInfo,objNewInfo);
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
					return -4;
				}

			}

			return intRetVal;
		}
		#endregion

		#region Web Event Handler  btnSave_Click
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intRetVal = Save();
			
			lblMessage.Visible = true;
			
			if ( intRetVal > 0 )
			{
				base.OpenEndorsementDetails();
				LoadData();
				SetWorkFlow();
			}
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

		}
		#endregion

		
	}
}
