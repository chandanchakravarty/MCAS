/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 04-03-2006
	<End Date				: ->
	<Description			: -> 
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
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Resources;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlApplication.GeneralLiability;
using Cms.Model.Policy.GeneralLiability;


namespace Cms.Policies.Aspx.GeneralLiability
{
	/// <summary>
	/// Summary description for PolicyGeneralLiabilityCoverages.
	/// </summary>
	public class PolicyGeneralLiabilityCoverages :Cms.Policies.policiesbase 
	{
		#region Page Controls Declaration
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capCOVERALE_L;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_L;
		protected System.Web.UI.WebControls.Label capAGGREGATE_L;
		protected System.Web.UI.WebControls.TextBox txtAGGREGATE_L;
		protected System.Web.UI.WebControls.Label capCOVERAGE_O;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_O;
		protected System.Web.UI.WebControls.Label capAGGREGATE_O;
		protected System.Web.UI.WebControls.TextBox txtAGGREGATE_O;
		protected System.Web.UI.WebControls.Label capCOVERAGE_M_EP;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_M_EP;
		protected System.Web.UI.WebControls.Label capAGGREGATE_TOTAL;
		protected System.Web.UI.WebControls.TextBox txtAGGREGATE_TOTAL;
		protected System.Web.UI.WebControls.Label capCOVERAGE_M_EA;
		protected System.Web.UI.WebControls.TextBox txtCOVERAGE_M_EA;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOVERAGE_M_EA;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		#endregion

		#region Private Variable Declaration

		protected int intCustomerId;
		protected int intPolicyId;
		protected int intPolicyVersionId;

		#endregion 
	
		#region PageLoad Function
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="283_1";
			this.btnReset.Attributes.Add("onclick","javascript:return ResetForm();");

			btnReset.CmsButtonClass			=	CmsButtonType.Write ;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	        =	CmsButtonType.Write ;
			btnSave.PermissionString		=	gstrSecurityXML;

			intPolicyId  =Convert.ToInt32 (GetPolicyID());
			intPolicyVersionId  =Convert.ToInt32 (GetPolicyVersionID ());
			intCustomerId=Convert.ToInt32 (GetCustomerID());

			if(!Page.IsPostBack)
			{
				SetCaptions();
				SetValidators();
				PopulateDropDowns();
				LoadData();
				SetWorkFlow();
			}			
		}
		#endregion

		#region SetCaptions Function
		private void SetCaptions()
		{	
			System.Resources.ResourceManager  objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.GeneralLiability.PolicyGeneralLiabilityCoverages",System.Reflection.Assembly.GetExecutingAssembly());
			
			capCOVERALE_L.Text = objResourceMgr.GetString("cmbCOVERAGE_L");
			capCOVERAGE_O.Text = objResourceMgr.GetString("cmbCOVERAGE_O");
			capCOVERAGE_M_EP .Text = objResourceMgr.GetString("cmbCOVERAGE_M_EP");
			capCOVERAGE_M_EA .Text = objResourceMgr.GetString("txtCOVERAGE_M_EA");
			capAGGREGATE_L.Text =  objResourceMgr.GetString("txtAGGREGATE_L");
			capAGGREGATE_O.Text =  objResourceMgr.GetString("txtAGGREGATE_O");
			capAGGREGATE_TOTAL.Text= objResourceMgr.GetString("txtAGGREGATE_TOTAL");
		}
		#endregion

		#region SetValidators Function
		private void SetValidators()
		{

		}
		#endregion

		#region PopulateDropDowns Function
		private void PopulateDropDowns()
		{
			DataTable dtCoverages = ClsGeneralLiabilityCoverages.GetPolicyCoverageForGL(intCustomerId,intPolicyId ,intPolicyVersionId ,"L");   

			cmbCOVERAGE_L.DataSource = dtCoverages;
			cmbCOVERAGE_L.DataTextField = "LIMIT_DEDUC_AMOUNT";
			cmbCOVERAGE_L.DataValueField = "LIMIT_DEDUC_ID";
			cmbCOVERAGE_L.DataBind();
			cmbCOVERAGE_L.Items.Insert(0,new ListItem("",""));
			
			dtCoverages = ClsGeneralLiabilityCoverages.GetPolicyCoverageForGL(intCustomerId,intPolicyId,intPolicyVersionId,"O");   
			cmbCOVERAGE_O.DataSource=dtCoverages; 
			cmbCOVERAGE_O.DataTextField="LIMIT_DEDUC_AMOUNT";
			cmbCOVERAGE_O.DataValueField="";
			cmbCOVERAGE_O.DataBind(); 
			cmbCOVERAGE_O.Items.Insert(0,new ListItem("","")); 

			dtCoverages = ClsGeneralLiabilityCoverages.GetPolicyCoverageForGL(intCustomerId,intPolicyId,intPolicyVersionId,"MEP");   
			cmbCOVERAGE_M_EP.DataSource=dtCoverages;
			cmbCOVERAGE_M_EP.DataTextField="LIMIT_DEDUC_AMOUNT";
			cmbCOVERAGE_M_EP.DataValueField="LIMIT_DEDUC_AMOUNT";
			cmbCOVERAGE_M_EP.DataBind(); 
			cmbCOVERAGE_M_EP.Items.Insert(0,new ListItem("","")); 

			dtCoverages = ClsGeneralLiabilityCoverages.GetPolicyCoverageForGL(intCustomerId,intPolicyId,intPolicyVersionId,"MEA");   
			hidCOVERAGE_M_EA.Value=dtCoverages.Rows[0]["LIMIT_DEDUC_AMOUNT"].ToString(); 
		}
		#endregion

		#region LoadData Function 
		private void LoadData()
		{
			ClsGeneralLiabilityCoverages objCoverages= new ClsGeneralLiabilityCoverages();
			DataTable dtCov = objCoverages.GetPolicyCoverageDetails(intCustomerId,intPolicyId ,intPolicyVersionId  );
			if(dtCov.Rows.Count <= 0)
				return;

			hidOldData.Value  = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtCov);
					
			int i=0;
			foreach( ListItem l in cmbCOVERAGE_L.Items )
			{
				if(l.Text == dtCov.Rows[0]["COVERAGE_L_AMOUNT"].ToString())
				{
					cmbCOVERAGE_L.SelectedIndex=i;
					break;
				}
				i++;
			}
			i=0;
			foreach( ListItem l in cmbCOVERAGE_O.Items )
			{
				if(l.Text == dtCov.Rows[0]["COVERAGE_O_AMOUNT"].ToString())
				{
					cmbCOVERAGE_O.SelectedIndex=i;
					break;
				}
				i++;
			}
			i=0;
			foreach( ListItem l in cmbCOVERAGE_M_EP.Items )
			{
				if(l.Text == dtCov.Rows[0]["COVERAGE_M_EACH_PERSON_AMOUNT"].ToString())
				{
					cmbCOVERAGE_M_EP.SelectedIndex=i;
					txtCOVERAGE_M_EA.Text=  dtCov.Rows[0]["COVERAGE_M_EACH_OCC_AMOUNT"].ToString();
					break;
				}
				i++;
			}
			txtAGGREGATE_L.Text = dtCov.Rows[0]["COVERAGE_L_AGGREGATE"].ToString();
			txtAGGREGATE_O.Text = dtCov.Rows[0]["COVERAGE_O_AGGREGATE"].ToString(); 
			txtAGGREGATE_TOTAL.Text = dtCov.Rows[0]["TOTAL_GENERAL_AGGREGATE"].ToString(); 
			

		}
		#endregion

		#region SetWorkFlow
		private void SetWorkFlow()
		{
			if(base.ScreenId == "283_1")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}

		}
		#endregion

		#region Event Handler btnSave_Click
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			ClsGeneralLiabilityCoverages objGeneralCoverages=new ClsGeneralLiabilityCoverages();
			ClsGeneralLiabilityCoveragesInfo  objInfo =new ClsGeneralLiabilityCoveragesInfo();
			if(cmbCOVERAGE_L.SelectedIndex > 0)
			{
				objInfo.COVERAGE_L_AMOUNT=Convert.ToDecimal(cmbCOVERAGE_L.SelectedItem.Text.Trim());
				objInfo.COVERAGE_L_ID =Convert.ToInt32(cmbCOVERAGE_L.SelectedValue); 
				objInfo.COVERAGE_L_AGGREGATE = Convert.ToDecimal(txtAGGREGATE_L.Text.Trim()); 
			}
			if(cmbCOVERAGE_O.SelectedIndex > 0)
			{
				objInfo.COVERAGE_O_AMOUNT = Convert.ToDecimal(cmbCOVERAGE_O.SelectedItem.Text.Trim());
				objInfo.COVERAGE_O_ID = Convert.ToInt32(cmbCOVERAGE_O.SelectedValue);
				objInfo.COVERAGE_O_AGGREGATE = Convert.ToDecimal(txtAGGREGATE_O.Text.Trim());
			}
			if(cmbCOVERAGE_M_EP.SelectedIndex > 0)
			{
				objInfo.COVERAGE_M_EACH_PERSON_AMOUNT = Convert.ToDecimal(cmbCOVERAGE_M_EP.SelectedItem.Text.Trim());
				objInfo.COVERAGE_M_EACH_PERSON_ID = Convert.ToInt32 (cmbCOVERAGE_M_EP.SelectedValue); 
				objInfo.COVERAGE_M_EACH_OCC_AMOUNT = Convert.ToDecimal(txtCOVERAGE_M_EA.Text.Trim());
			}
			if(txtAGGREGATE_TOTAL.Text != "")
				objInfo.TOTAL_GENERAL_AGGREGATE = Convert.ToDecimal(txtAGGREGATE_TOTAL.Text.Trim());

			objInfo.CUSTOMER_ID=intCustomerId;
			objInfo.POLICY_ID=intPolicyId ;
			objInfo.POLICY_VERSION_ID=intPolicyVersionId ;
			objInfo.CREATED_BY=Convert.ToInt32(GetUserId());

			try
			{
				int intResult;
				if(hidOldData.Value == "" || hidOldData.Value == "0") //Save Case
				{
					intResult= objGeneralCoverages.AddPolicy(objInfo);
					if(intResult == 1)
					{
						SetWorkFlow();
						lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");
					}
				}
				else //Update Case
				{
					ClsGeneralLiabilityCoveragesInfo objOldInfo = new ClsGeneralLiabilityCoveragesInfo();
					base.PopulateModelObject(objOldInfo,hidOldData.Value);
					objInfo.MODIFIED_BY=Convert.ToInt32(GetUserId());
					intResult= objGeneralCoverages.UpdatePolicy(objOldInfo,objInfo);
					if(intResult == 1)
					{
						SetWorkFlow();
						lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
					}

				}
				lblMessage.Visible=true;
			}
			catch(Exception ex)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible=true;
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		}
		#endregion
	}
}
