/******************************************************************************************
	<Author					: Avijit Goswami    
	<Start Date				: 19/03/2010
	<End Date				: - >
	<Description			: - > 
	<Review Date			: - >
	<Reviewed By			: - >
	
Modification History

	<Modified Date			: - > 
	<Modified By			: - > 
	<Purpose				: - >
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
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
using System.Xml;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlApplication;

namespace Cms.Policies.Aspx.MariTime
{
    public partial class PolicyUWQMarine : Cms.Policies.policiesbase
    {
        ClsPolicyUWQMarine objClsPolicyUWQMarine;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        public string strCalledFrom = "";
        private string strRowId, strFormSaved;
        string oldXML;        
        protected System.Web.UI.WebControls.TextBox txtREMARKS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckMakeSubmit;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;			
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;

        System.Resources.ResourceManager objResourceMgr;

        private ClsPolicyUWQMarineInfo GetFormValue() 
        {
            ClsPolicyUWQMarineInfo objClsPolicyUWQMarineInfo;
            objClsPolicyUWQMarineInfo = new ClsPolicyUWQMarineInfo();
            objClsPolicyUWQMarineInfo.ANY_FARMING_BUSINESS_COND = cmbANY_PRE_INSPECTION_DONE.SelectedValue;

            objClsPolicyUWQMarineInfo.REMARKS = txtTYPEOFPACKAGING.Text;
            objClsPolicyUWQMarineInfo.POLICY_ID = hidPolicyID.Value == "" ? 0 : int.Parse(hidPolicyID.Value);
            objClsPolicyUWQMarineInfo.POLICY_VERSION_ID = hidPolicyVersionID.Value == "" ? 0 : int.Parse(hidPolicyVersionID.Value);
            objClsPolicyUWQMarineInfo.CUSTOMER_ID = hidCustomerID.Value == "" ? 0 : int.Parse(hidCustomerID.Value);			
			strFormSaved	=	hidFormSaved.Value;
            GenerateXML();
			oldXML		= hidOldData.Value;
            if (hidOldData.Value != "" && hidOldData.Value != "0")
                strRowId = hidCustomerID.Value;  
            else
                strRowId = "NEW";

            return objClsPolicyUWQMarineInfo;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            base.ScreenId = "572";
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.MariTime.PolicyUWQMarine", System.Reflection.Assembly.GetExecutingAssembly());
            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme = GetColorScheme();
            string colors = "";
            switch (int.Parse(colorScheme))
            {
                case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();
                    break;
                case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();
                    break;
                case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();
                    break;
                case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();
                    break;
            }
            if (colors != "")
            {
                string[] baseColor = colors.Split(new char[] { ',' });
                if (baseColor.Length > 0)
                    colors = "#" + baseColor[0];
            }
            #endregion 
            btnReset.Attributes.Add("onclick", "javascript:return Reset();");
            lblMessage.Visible = false;
            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;
            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            
            hidPolicyID.Value = GetPolicyID();
            hidPolicyVersionID.Value = GetPolicyVersionID();
            hidCustomerID.Value = GetCustomerID();
            
            if (!Page.IsPostBack)
            {          
                PopulateComboBox();
                LoadData();
                ClsPolicyUWQMarine objClsPolicyUWQMarine = new ClsPolicyUWQMarine();
                try
                {
                    DataSet ds = new DataSet();                    
                    ds = objClsPolicyUWQMarine.FetchPolicyMotorGenInfoData(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));
                    if (ds.Tables[0].Rows.Count > 0)
                        hidOldData.Value = ds.GetXml();
                    else
                        hidOldData.Value = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                    lblMessage.Visible = true;
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    hidFormSaved.Value = "2";
                }
                finally
                {
                    if (objClsPolicyUWQMarine != null)
                        objClsPolicyUWQMarine.Dispose();
                }
            }
            TabCtl.TabURLs = "PolicyUWQMarine.aspx?";
            TabCtl.TabTitles = "Underwriting Questions";
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void PopulateComboBox()
        {
            IList objIlist = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbANY_PRE_INSPECTION_DONE.DataSource = objIlist;
            cmbANY_PRE_INSPECTION_DONE.DataTextField = "LookupDesc";
            cmbANY_PRE_INSPECTION_DONE.DataValueField = "LookupCode";
            cmbANY_PRE_INSPECTION_DONE.DataBind();            
        }

        private void LoadData()
        {
            ClsPolicyUWQMarine objClsPolicyUWQMarine = new ClsPolicyUWQMarine();
            DataSet ds = objClsPolicyUWQMarine.FetchPolicyMotorGenInfoData(int.Parse(hidCustomerID.Value)
                , int.Parse(hidPolicyID.Value)
                , int.Parse(hidPolicyVersionID.Value));

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dt = ds.Tables[0].Rows[0];
                this.txtTYPEOFPACKAGING.Text = dt["REMARKS"].ToString();

                ListItem li = new ListItem();
                li = this.cmbANY_PRE_INSPECTION_DONE.Items.FindByValue(dt["ANY_FARMING_BUSINESS_COND"].ToString());
                cmbANY_PRE_INSPECTION_DONE.SelectedIndex = cmbANY_PRE_INSPECTION_DONE.Items.IndexOf(li);
            }
        }

        private void GenerateXML()
		{
			ClsPolicyUWQMarine objClsPolicyUWQMarine=new ClsPolicyUWQMarine();  			
			try
			{
				DataSet ds=new DataSet();
                ds = objClsPolicyUWQMarine.FetchPolicyMotorGenInfoData(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));
                if (ds.Tables[0].Rows.Count > 0)
                    hidOldData.Value = ds.GetXml();
                else
                    hidOldData.Value = "";
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";                    
			}
			finally
			{
				if(objClsPolicyUWQMarine!= null)
					objClsPolicyUWQMarine.Dispose();
			}                 
		}  
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                int intRetVal;
                objClsPolicyUWQMarine = new ClsPolicyUWQMarine();
                //Retreiving the form values into model class object
                ClsPolicyUWQMarineInfo objClsPolicyUWQMarineInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objClsPolicyUWQMarineInfo.CREATED_BY = int.Parse(GetUserId());
                    objClsPolicyUWQMarineInfo.CREATED_DATETIME = DateTime.Now;
                    objClsPolicyUWQMarineInfo.IS_ACTIVE = "Y";
                    //Calling the add method of business layer class                    
                    intRetVal = objClsPolicyUWQMarine.Add(objClsPolicyUWQMarineInfo);
                    if (intRetVal > 0)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";                        
                        GenerateXML();
                        hidCustomerID.Value = objClsPolicyUWQMarineInfo.CUSTOMER_ID.ToString();
                        strRowId = hidCustomerID.Value;
                        hidDETAIL_TYPE_ID.Value = hidCustomerID.Value;                        
                        base.OpenEndorsementDetails();
                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                }
                else //UPDATE CASE
				{
                    ClsPolicyUWQMarineInfo objOldClsPolicyUWQMarineInfo;
                    objOldClsPolicyUWQMarineInfo = new ClsPolicyUWQMarineInfo();
					
                    base.PopulateModelObject(objOldClsPolicyUWQMarineInfo, hidOldData.Value);
					          
                    objClsPolicyUWQMarineInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objClsPolicyUWQMarineInfo.LAST_UPDATED_DATETIME = DateTime.Now;           
					//Updating the record using business layer class object
                    intRetVal = objClsPolicyUWQMarine.Update(objOldClsPolicyUWQMarineInfo, objClsPolicyUWQMarineInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GenerateXML();						
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
                        lblMessage.Text = "";
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";              
			}
			finally
			{
                if (objClsPolicyUWQMarine != null)
                    objClsPolicyUWQMarine.Dispose();
			}
		} 
    }
}