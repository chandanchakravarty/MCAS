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
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Ajax;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlClient;

namespace Policies.Aspx
{
	/// <summary>
	/// Summary description for CopyPolicyToNewClient.
	/// </summary>
	public class CopyPolicyToNewClient : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblHeader;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnSubmit;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		protected System.Web.UI.HtmlControls.HtmlForm COPYPOLICY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTitle;
		private string strCustomerID, strPolicyId, strPolicyVersionId;
		protected System.Web.UI.WebControls.Label lblCustomerCode;
		protected System.Web.UI.WebControls.Label lblAgency;
		protected System.Web.UI.WebControls.Label lblCustomerName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.WebControls.Label lblCUSTOMER_CODE;
		protected System.Web.UI.WebControls.Label lblCUSTOMER_NAME;
		protected System.Web.UI.WebControls.Label lblAGENCY_DISPLAY_NAME;
		protected System.Web.UI.WebControls.Label lblcustomermsg;
		protected System.Web.UI.WebControls.Label lblSTATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_CODE;
		protected System.Web.UI.WebControls.Label lblStateName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCSR_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPRODUCER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCopyMsg;
        protected System.Web.UI.WebControls.Label lblSTATE_NAME; 

        protected System.Web.UI.WebControls.Label lblCSR;
        protected System.Web.UI.WebControls.Label lblPRODUCER;
        protected System.Web.UI.WebControls.DropDownList cmbCSR;
        protected System.Web.UI.WebControls.DropDownList cmbPRODUCER;
        
        public string str;
        public string strMsg;
        public string head;
        ResourceManager objResourceMgr = null;
       
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(CopyPolicyToNewClient));
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Policies.Aspx.CopyPolicyToNewClient", Assembly.GetExecutingAssembly());
            SetCaptions();
            str = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1924");
            hidCopyMsg.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1923");
            strMsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1926");
//			strCustomerID = base.GetCustomerID();
			#region geting query string
			strCustomerID		=	Request.QueryString["CUSTOMER_ID"].ToString();
			strPolicyId			=	Request.QueryString["POLICY_ID"].ToString();
			strPolicyVersionId	=	Request.QueryString["POLICY_VERSION_ID"].ToString();
			hidLOBID.Value		=  	Request.QueryString["LOB_ID"].ToString();
			hidAPP_ID.Value		=	Request.QueryString["APP_ID"].ToString();
			hidAPP_VERSION_ID.Value		=	Request.QueryString["APP_VERSION_ID"].ToString();
			hidPolicyID.Value=strPolicyId;
           
			//Added by Manoj Rathore on 25 Jun. 2009 Itrack # 6011
			string strSystemID					=	GetSystemId();
            
			#endregion

			#region Button security
			btnSubmit.CmsButtonClass	=	CmsButtonType.Write;
			btnSubmit.PermissionString	=	"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			btnClose.CmsButtonClass		=	CmsButtonType.Write;
			btnClose.PermissionString	=	"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			#endregion
			btnClose.Attributes.Add("onclick","closewindow();");
			btnSubmit.Attributes.Add("onclick","javascript:return ConfirmTransfer();");
			//if (!IsPostBack)
			//{
				#region GETTING BASE COLOR FOR ROW SELECTION
				string colorScheme=GetColorScheme();
				string colors="";

				switch (int.Parse(colorScheme))
				{
					case 1:
						colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
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

				if(colors!="")
				{
					string [] baseColor=colors.Split(new char []{','});  
					if(baseColor.Length>0)
						colors= "#" + baseColor[0];
				}
				#endregion 

				#region loading web grid control
			
				Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;
                //objResourceMgr = new System.Resources.ResourceManager("Policies.Aspx.CopyPolicyToNewClient", System.Reflection.Assembly.GetExecutingAssembly());
				try
				{
					//Setting web grid control properties
					objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
					objWebGrid.SelectClause = 	"CUSTOMER_ID,CUSTOMER_CODE,isnull(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,	AG.AGENCY_DISPLAY_NAME,ST.STATE_CODE,ST.STATE_NAME,ISNULL(CCL.IS_ACTIVE,'Y') IS_ACTIVE,AG.AGENCY_ID";
					objWebGrid.FromClause = " CLT_CUSTOMER_LIST  CCL INNER JOIN MNT_AGENCY_LIST AG ON CCL.CUSTOMER_AGENCY_ID=AG.AGENCY_ID INNER JOIN MNT_COUNTRY_STATE_LIST ST ON ST.STATE_ID=CCL.CUSTOMER_STATE";
				    
					if(strSystemID != CarrierSystemID)//"W001") //AGENCY_CODE: Added by Manoj Rathore on 25 Jun. 2009 Itrack # 6011	
					{
						objWebGrid.WhereClause = " CCL.CUSTOMER_ID = '" + strCustomerID + "' AND AG.AGENCY_CODE = '" + strSystemID + "'"; 						
					}
					else 						
					{
						objWebGrid.WhereClause = " CCL.CUSTOMER_ID = '" + strCustomerID + "'";
					}


                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Name^First Name^Last Name^Middle Name^State";
					//objWebGrid.SearchColumnNames = "isnull(CUSTOMER_FIRST_NAME,'') ! ISNULL(CUSTOMER_MIDDLE_NAME,'') ! ISNULL(CUSTOMER_LAST_NAME,'')^isnull(CUSTOMER_FIRST_NAME,'') ! ISNULL(CUSTOMER_MIDDLE_NAME,'') ! ISNULL(CUSTOMER_LAST_NAME,'')^isnull(CUSTOMER_FIRST_NAME,'') ! ISNULL(CUSTOMER_MIDDLE_NAME,'') ! ISNULL(CUSTOMER_LAST_NAME,'')^isnull(CUSTOMER_FIRST_NAME,'') ! ISNULL(CUSTOMER_MIDDLE_NAME,'') ! ISNULL(CUSTOMER_LAST_NAME,'')^ST.STATE_NAME";
					objWebGrid.SearchColumnNames = "isnull(CUSTOMER_FIRST_NAME,'') ! ISNULL(CUSTOMER_MIDDLE_NAME,'') ! ISNULL(CUSTOMER_LAST_NAME,'')^isnull(CUSTOMER_FIRST_NAME,'')^ISNULL(CUSTOMER_LAST_NAME,'')^ISNULL(CUSTOMER_MIDDLE_NAME,'')^ST.STATE_NAME";
					objWebGrid.SearchColumnType = "T^T^T^T^T";
				
					objWebGrid.OrderByClause = "CUSTOMER_NAME ASC";

					objWebGrid.DisplayColumnNumbers = "2^3^4^6";
					objWebGrid.DisplayColumnNames = "CUSTOMER_CODE^CUSTOMER_NAME^AGENCY_DISPLAY_NAME^STATE_NAME";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Customer Code^Customer Name^Agency^State";

					objWebGrid.DisplayTextLength = "10^50^20^20";
					objWebGrid.DisplayColumnPercent = "10^50^20^20";
					objWebGrid.PrimaryColumns = "1^2";
					objWebGrid.PrimaryColumnsName = "CUSTOMER_ID^CUSTOMER_CODE";

					objWebGrid.ColumnTypes = "B^B^B^B";

					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3^4^5^6^7";

                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";

                    //objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
					objWebGrid.PageSize = int.Parse(GetPageSize());
					objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Customer Details";
					objWebGrid.SelectClass = colors;	
					objWebGrid.RequireQuery = "Y";
					objWebGrid.DefaultSearch = "Y";
					objWebGrid.QueryStringColumns = "CUSTOMER_ID";
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
					objWebGrid.FilterValue = "Y";
					//objWebGrid.RequireCheckbox="Y";
					objWebGrid.FilterColumnName ="CCL.IS_ACTIVE";
					//Adding to controls to gridholder
					GridHolder.Controls.Add(c1);
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
				#endregion
			//}
                this.BindPageControlValue(int.Parse(strCustomerID), int.Parse(strPolicyId), int.Parse(strPolicyVersionId));
			
		}
        private void BindPageControlValue(int CustomerID,int policy_id,int policy_version_id)
        {
            ClsCustomer objCustomer = new ClsCustomer();
            DataSet objDataSet = new DataSet();

            objDataSet = objCustomer.GetCustomerDetailsforCopyPolicy(CustomerID, policy_id, policy_version_id);

            if (objDataSet == null || objDataSet.Tables[0].Rows.Count == 0)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1807");// "No records found for this search criteria.";
                return;
            }
            else
            {
                hidCUSTOMER_ID.Value = CustomerID.ToString();
                lblCUSTOMER_CODE.Text = objDataSet.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString();
                lblCUSTOMER_NAME.Text = objDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
                lblAGENCY_DISPLAY_NAME.Text = objDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
                lblSTATE_NAME.Text = objDataSet.Tables[0].Rows[0]["STATE_NAME"].ToString();
                int Agency_id =int.Parse(objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString());
                String CSR = objDataSet.Tables[0].Rows[0]["CSR"].ToString();
                String PRODUCER = objDataSet.Tables[0].Rows[0]["PRODUCER"].ToString();
                this.BindDropDownValue(Agency_id, int.Parse(hidLOBID.Value));
                cmbCSR.SelectedIndex = cmbCSR.Items.IndexOf(cmbCSR.Items.FindByValue(CSR));
                cmbPRODUCER.SelectedIndex = cmbCSR.Items.IndexOf(cmbCSR.Items.FindByValue(PRODUCER));
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
        private void SetCaptions()
        {
            lblCustomerCode.Text = objResourceMgr.GetString("lblCustomerCode");
            lblAgency.Text = objResourceMgr.GetString("lblAgency");
            lblCSR.Text = objResourceMgr.GetString("lblCSR");
            lblCustomerName.Text = objResourceMgr.GetString("lblCustomerName");
            lblStateName.Text = objResourceMgr.GetString("lblStateName");
            lblPRODUCER.Text = objResourceMgr.GetString("lblPRODUCER");
            btnSubmit.Text = objResourceMgr.GetString("btnSubmit");
            lblHeader.Text = objResourceMgr.GetString("lblHeader");
            lblcustomermsg.Text = objResourceMgr.GetString("lblcustomermsg");
            head = objResourceMgr.GetString("head");
        }

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try
			{
			 if (hidCUSTOMER_ID.Value!="" && hidCUSTOMER_ID.Value!="0")
				{
				int intNewCustmerID=int.Parse(hidCUSTOMER_ID.Value);
				int intNewPolicyversionId=0,intNewPolicyID=0,intCSR=0,intProducer=0;
				string UserID="0";
				UserID=GetUserId().ToString(); 				
				intCSR=int.Parse(hidCSR_ID.Value);
				intProducer= int.Parse(hidPRODUCER_ID.Value);
				Cms.BusinessLayer.BlProcess.ClsPolicyProcess objProcess=new Cms.BusinessLayer.BlProcess.ClsPolicyProcess();
				
					objProcess.TransferPolicyToNewCustomer(int.Parse(strPolicyId),int.Parse(strPolicyVersionId),int.Parse(strCustomerID),intNewCustmerID,out intNewPolicyID,out intNewPolicyversionId,int.Parse(UserID),intCSR,intProducer);
                    lblcustomermsg.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1923");//"Policy has been transferred to new Client.";
					hidNEW_POLICY_VERSION_ID.Value	=intNewPolicyversionId.ToString();
					hidNEW_POLICY_ID.Value			=intNewPolicyID.ToString();
				  objProcess.Dispose();
				  int intAppID=0,intAppVersionID=0;
				  Cms.BusinessLayer.BlApplication.ClsGeneralInformation objgenInfo=new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
				  objgenInfo.GetAppDetailsFromPolicy(intNewCustmerID,int.Parse(hidNEW_POLICY_ID.Value),int.Parse(hidNEW_POLICY_VERSION_ID.Value),out intAppID,out intAppVersionID);
				  objgenInfo.Dispose();
				 hidAPP_ID.Value=intAppID.ToString();
				 hidAPP_VERSION_ID.Value=intAppVersionID.ToString();
				}
			 else
				{
                    lblcustomermsg.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1924");//"Please select Customer first.";
				}
			}
			catch(Exception ex )
			{
                lblcustomermsg.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1925") +ex.Message;//"Could not be transferred this policy." + ex.Message;
				hidCUSTOMER_ID.Value			="0";
				hidNEW_POLICY_VERSION_ID.Value	="0";
				hidNEW_POLICY_ID.Value			="0";
				hidCSR_ID.Value			="0";
				hidPRODUCER_ID.Value	="0";
			}

		}
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

		}
		#endregion
        private void BindDropDownValue(int AgencyId, int LOBID)
        {
            ClsUser objUser = new ClsUser();
            DataTable dtCSRProducers = objUser.GetCSRProducers(AgencyId, LOBID,  GetSystemId());
            DataView dv = dtCSRProducers.DefaultView;
            dv.Sort = "USER_NAME_ID";
            cmbPRODUCER.Items.Clear();
            cmbPRODUCER.DataSource = dv;
            cmbPRODUCER.DataTextField = "USER_NAME_ID";
            cmbPRODUCER.DataValueField = "USER_ID";
            cmbPRODUCER.DataBind();
            cmbPRODUCER.Items.Insert(0, "");
            cmbCSR.Items.Clear();
            cmbCSR.DataSource = dv;
            cmbCSR.DataTextField = "USER_NAME_ID";
            cmbCSR.DataValueField = "USER_ID";
            cmbCSR.DataBind();
            cmbCSR.Items.Insert(0, "");
          
             
        }
        [Ajax.AjaxMethod(HttpSessionStateRequirement.Read)]
        public string AjaxFetchAgencyCSRProducer(int AgencyId, int LOBID)
		{
			
			Cms.CmsWeb.webservices.ClsWebServiceCommon obj =  new Cms.CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
            result = obj.FetchAgencyCSRProducer(AgencyId, LOBID, GetSystemId());
			return result;
			
		}

	}
}
		
