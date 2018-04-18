/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	 26-10-2005
<End Date			: -	 
<Description		: -  Class to display the policy tabs named Issuing Information,Named Insured,Attachment 
<Review Date		: - 
<Reviewed By		: - 	
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
using Cms.CmsWeb;
using Cms.BusinessLayer.BlCommon;
using System.Reflection;
using System.Resources;
using System.Text;
namespace Cms.Policies.Aspx
{
    /// <summary>
    /// Summary description for PolicyTab.
    /// </summary>
    public class PolicyTab : Cms.Policies.policiesbase
    {
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabNumber;

        private string strCustomerId, strAppId, strAppVersionId, strPolicyID, strPolicyVersionID, strLobID;
        protected System.Web.UI.HtmlControls.HtmlForm PolicyInformation;
        //string strOldLobString = "";

        private string strStatus = "";
        private string strAPP_STATUS = "";
        private ClsGeneralInformation objGeneralInformation = null;

        private const string CALLED_FROM_CLIENT = "CLT";
        private const string CALLED_FROM_INNER_CLIENT = "InCLT";
        private const string CALLED_FROM_APP = "APP";

        private ResourceManager objResourceManager = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            OpenTreeNaveegation();//for treeviewNavigation Change
            GetCustomerIDAppIDAppVersionIDValues();
            try
            {
                SetSessionValues();

                GetPolicyIDPolicyVersionIDLobIDValues();

                objGeneralInformation = new ClsGeneralInformation();
                if (strPolicyID != "" && strPolicyVersionID != "")
                {
                    strStatus = objGeneralInformation.GetStatusOfPolicy(int.Parse(strCustomerId), int.Parse(strPolicyID), int.Parse(strPolicyVersionID));
                    strAPP_STATUS = objGeneralInformation.GetStatusOfApplication(int.Parse(strCustomerId), int.Parse(strPolicyID), int.Parse(strPolicyVersionID));
                }
                //Added By Pradeep Kushwaha on 06-07-2010
                if (strStatus != null && strStatus.Trim() != "")
                    SetPolicyStatus(strStatus);
                else
                    SetPolicyStatus("");
                if (strAPP_STATUS != null && strAPP_STATUS.Trim() != "")
                    SetApplicationStatus(strAPP_STATUS);
                else
                    SetApplicationStatus("");

                if ((strAPP_STATUS == null || strAPP_STATUS.Trim() == "") && Request.Params["CalledFrom"] != CALLED_FROM_CLIENT && Request.Params["CalledFrom"] != CALLED_FROM_INNER_CLIENT && Request.Params["CalledFrom"] != CALLED_FROM_APP)
                {
                    callDeletePolicyAlert();
                    return;
                }
                //Added till here

                //ShowClientTopControl(int.Parse(strCustomerId));

                if (strCustomerId != null && strCustomerId.Trim() != "")
                {
                    if (strCustomerId != null && strAppId != null && strAppVersionId != null && strAppId != "" && strAppVersionId != "")
                    {
                        ShowClientTopControl(int.Parse(strCustomerId), "POL");
                        SetWorkFlow();
                    }
                    else
                    {
                        ShowClientTopControl(int.Parse(strCustomerId), "CUST");
                    }

                    SetTabs();
                }
                else
                {
                    cltClientTop.Visible = false;
                    TabCtl.TabURLs = "PolicyGeneralInfo.aspx?CALLEDFROM=" + Request.Params["CalledFrom"];

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objGeneralInformation = null;
            }
        }

        private void callDeletePolicyAlert()
        {
            SetCultureThread(GetLanguageCode());
            objResourceManager = new ResourceManager("Cms.Policies.Aspx.PolicyTab", Assembly.GetExecutingAssembly());
            string strScript = "<script>alert('" + objResourceManager.GetString("lblDeleteMessage") + "'); top.location.href = '/cms/cmsweb/aspx/index.aspx?&';</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "GotoMainpag", strScript);
            SetAppID("");
            SetAppVersionID("");
            SetPolicyID("");
            SetPolicyVersionID("");
            SetLOBID("");
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
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

        }
        #endregion

        private void SetTabs()
        {
            string url;
            TabCtl.TabLength = 166;
            //TabCtl.SubTabLength = 8;


            if (strCustomerId != null && strAppId != null && strAppVersionId != null && strAppId != "" && strAppVersionId != "")
            {

                if ((Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != String.Empty && Request.QueryString["CalledFrom"].ToString().ToUpper() == CALLED_FROM_APP) || GetPolicyStatus().Trim().ToUpper() == "")
                {
                    url = "PolicyGeneralInfo.aspx?CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVersionId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                    TabCtl.TabURLs = url;
                    TabCtl.TabScreenIDs = "224_1";
                    TabCtl.TabTitles = ClsMessages.GetTabTitles("", "ExistingApplication");
                }
                else if (GetPolicyStatus().Trim().ToUpper() == "" && strAPP_STATUS.Trim().ToUpper() == "APPLICATION")
                {
                    url = "PolicyGeneralInfo.aspx?CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVersionId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                    TabCtl.TabURLs = url; SetPolicyStatus("");
                    TabCtl.TabScreenIDs = "224_1";
                    TabCtl.TabTitles = ClsMessages.GetTabTitles("", "ExistingApplication");
                }//changed By Lalit Feb 25,2011,i-track # 862,946
                else if (GetPolicyStatus().Trim().ToUpper() == ClsCommon.POLICY_STATUS_REJECT
                    || GetPolicyStatus().Trim().ToUpper() == ClsCommon.POLICY_STATUS_UNDER_ISSUE
                    || GetPolicyStatus().Trim().ToUpper() == ClsCommon.POLICY_STATUS_UNDER_RENEW //Modified By Lalit March 24,2011.i-track # 946
                    )
                {
                    url = "PolicyGeneralInfo.aspx?CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVersionId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                    TabCtl.TabURLs = url; SetPolicyStatus("");
                    TabCtl.TabScreenIDs = "224_1";
                    TabCtl.TabTitles = ClsMessages.GetTabTitles("", "ExistingApplication");
                }
                else
                {
                    url = "PolicyInformation.aspx?CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVersionId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                    TabCtl.TabURLs = url;
                    TabCtl.TabScreenIDs = "224_1";
                    TabCtl.TabTitles = ClsMessages.GetTabTitles("", "ExistingPolicy");
                }

                url = "PolicyNameIns.aspx?CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVersionId + "&POLICY_LOB=" + strLobID + "&";
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_7";

                url = "/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=POLICY&EntityType=Application&EntityId=0&CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVersionId + "&POLICY_LOB=" + strLobID + "&";
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_8";

                url = "PolicyProcessLogIndex.aspx";
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_9";

                url = "PolicyEndorsementLogIndex.aspx";
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_10";


                if (strLobID != "" && GetTransaction_Type() == MASTER_POLICY)//GetProduct_Type(int.Parse(strLobID)) == MASTER_POLICY)// for master policy 
                {
                    url = "MasterPolicyBillingInfo.aspx?CUSTOMER_ID=" + strCustomerId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_11";
                }
                else
                {
                    //Changed for Singapore replaced below url 
                   //url = "BillingInfo.aspx?CUSTOMER_ID=" + strCustomerId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                    url = "BillingDetails.aspx?CUSTOMER_ID=" + strCustomerId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                    //with this one by kuldeep on 16-jan-2012
                   // url = "/Cms/CmsWeb/Construction.html";
                    TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                    TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_11";

                }

                //url = "InstallmentInfo.aspx?CALLEDFOR=Install&CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVersionId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                //TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

                //url = "PolicyReinsuranceInquiry.aspx?CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                //TabCtl.TabURLs = TabCtl.TabURLs + "," + url;

                //adeed by Chetna on 17 March,10
                url = "PolicyLocationIndex.aspx?calledfrom=POLICY&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_0";

                url = "AddCoinsurance.aspx?calledfrom=POLICY&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_25";

                url = "PolicyRemunerationIndex.aspx?calledfrom=POLICY&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_26";

                url = "PolicyClauses.aspx?calledfrom=POLICY&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_28";

               
                url = "AddDiscountSurcharge.aspx?calledfrom=POLICY&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                //url = "BillingDetails.aspx?CUSTOMER_ID=" + strCustomerId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_29";

                url = "AddPolicyReinsurer.aspx?calledfrom=POLICY&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_30";

                //url = "BoletoRePrint.aspx?calledfrom=POLICY&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyID + "&POLICY_VERSION_ID=" + strPolicyVersionID + "&POLICY_LOB=" + strLobID;
                //TabCtl.TabURLs = TabCtl.TabURLs + "," + url;
                //TabCtl.TabScreenIDs = TabCtl.TabScreenIDs + "," + "224_38";

                //if(GetPolicyStatus().Trim().ToUpper() == "APPLICATION")
                //    TabCtl.TabTitles = ClsMessages.GetTabTitles("", "ExistingApplication");                
                //else
                //    TabCtl.TabTitles = ClsMessages.GetTabTitles("", "ExistingPolicy");
            }
            else
            {
                url = "PolicyGeneralInfo.aspx?CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVersionId + "&CALLEDFROM=" + Request.Params["CalledFrom"] + "&";
                TabCtl.TabURLs = url;

                TabCtl.TabTitles = ClsMessages.GetTabTitles("", "NewPolicy");
                //if (TabCtl.TabTitles == "")
                //    TabCtl.TabTitles = "Applicant Information";               
            }
        }
        private void SetSessionValues()
        {
            Cms.BusinessLayer.BlProcess.ClsReinstatementProcess objReinstatementProcess = new Cms.BusinessLayer.BlProcess.ClsReinstatementProcess();

            try
            {
                //try
                //{
                //    if (GetLOBString() != null)
                //        strOldLobString = GetLOBString();
                //}
                //catch
                //{}

                if (Request["POLICY_ID"] != null && Request["POLICY_VERSION_ID"] != null && Request["CUSTOMER_ID"] != null && Request["POLICY_ID"].ToString() != "" && Request["POLICY_VERSION_ID"].ToString() != "" && Request["CUSTOMER_ID"].ToString() != "")
                {
                    SetCustomerID(Request["CUSTOMER_ID"].ToString());
                    SetPolicyID(Request["POLICY_ID"].ToString());
                    SetPolicyVersionID(Request["POLICY_VERSION_ID"].ToString());
                    SetLOBID(objReinstatementProcess.GetLOBID(Convert.ToInt32(strCustomerId), Convert.ToInt32(Request["POLICY_ID"].ToString()), Convert.ToInt32(Request["POLICY_VERSION_ID"].ToString())).ToString());
                    //strLobID = GetLOBID();                                 

                    //dsTemp = new DataSet();
                    //dsTemp = objGeneralInformation.GetPolicyLOBString(Convert.ToInt32(strLobID));
                    //SetLOBString(dsTemp.Tables[0].Rows[0][0].ToString());
                    string customer_id = Request["CUSTOMER_ID"].ToString();
                    string policy_id = Request["POLICY_ID"].ToString();
                    string policy_version_id = Request["POLICY_VERSION_ID"].ToString();
                    objGeneralInformation = new ClsGeneralInformation();
                    DataSet dsTemp = objGeneralInformation.GetPolicyDetails(Convert.ToInt32(customer_id), 0, 0, Convert.ToInt32(policy_id), Convert.ToInt32(policy_version_id));
                    if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                        SetTransaction_Type(dsTemp.Tables[0].Rows[0]["TRANSACTION_TYPE"].ToString());

                }
                else
                {

                    if (Request["CUSTOMER_ID"] != null)
                    {
                        objGeneralInformation = new ClsGeneralInformation();

                        DataSet dsTemp = objGeneralInformation.GetPolicyDetails(Convert.ToInt32(strCustomerId), Convert.ToInt32(strAppId), Convert.ToInt32(strAppVersionId));
                        //GetPolicyVersionID();

                        SetCustomerID(Request["CUSTOMER_ID"].ToString());
                        if (dsTemp.Tables[0].Rows.Count > 0)
                        {
                            SetPolicyID(dsTemp.Tables[0].Rows[0]["POLICY_ID"].ToString());
                            SetPolicyVersionID(dsTemp.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
                            SetLOBID(dsTemp.Tables[0].Rows[0]["POLICY_LOB"].ToString());
                            //Added By Lalit Nov 08,2010
                            SetTransaction_Type(dsTemp.Tables[0].Rows[0]["TRANSACTION_TYPE"].ToString());
                        }
                    }
                    //dsTemp = new DataSet();
                    //dsTemp = objGeneralInformation.GetPolicyLOBString(Convert.ToInt32(GetLOBID()));
                    //SetLOBString(dsTemp.Tables[0].Rows[0][0].ToString());
                }

                strLobID = "";
                try
                {
                    if (Request["POLICY_LOB"] != null)
                    {
                        strLobID = Request["POLICY_LOB"].ToString();
                    }
                    else
                        strLobID = GetLOBID();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

                switch (strLobID)
                {
                    case LOB_HOME:
                        SetLOBString("HOME");
                        base.ScreenId = "224_1";
                        break;
                    case LOB_PRIVATE_PASSENGER:
                        SetLOBString("PPA");
                        base.ScreenId = "224_2";
                        break;
                    case LOB_MOTORCYCLE:
                        SetLOBString("MOT");
                        base.ScreenId = "224_3";
                        break;
                    case LOB_WATERCRAFT:
                        SetLOBString("WAT");
                        base.ScreenId = "224_4";
                        break;
                    case LOB_RENTAL_DWELLING:
                        SetLOBString("RENT");
                        base.ScreenId = "224_6";
                        break;
                    case LOB_UMBRELLA:
                        SetLOBString("UMB");
                        base.ScreenId = "224_5";
                        break;
                    case LOB_GENERAL_LIABILITY:
                        SetLOBString("GEN");
                        base.ScreenId = "224_7";
                        break;
                    case LOB_AVIATION:
                        SetLOBString("AVIATION");
                        base.ScreenId = "224_13";
                        break;
                    case LOB_ALL_RISKS_AND_NAMED_PERILS:
                        SetLOBString("ARPERIL");
                        base.ScreenId = "224_14";
                        break;
                    case LOB_CIVIL_LIABILITY_TRANSPORTATION:
                        SetLOBString("CVLIABTR");
                        base.ScreenId = "224_23";
                        break;
                    case LOB_COMPREHENSIVE_COMPANY:
                        SetLOBString("COMPCOMPY");
                        base.ScreenId = "224_16";
                        break;
                    case LOB_COMPREHENSIVE_CONDOMINIUM:
                        SetLOBString("COMPCONDO");
                        base.ScreenId = "224_15";
                        break;
                    case LOB_DIVERSIFIED_RISKS:
                        SetLOBString("DRISK");
                        base.ScreenId = "224_19";
                        break;
                    case LOB_DWELLING:
                        SetLOBString("DWELLING");
                        base.ScreenId = "224_24";
                        break;
                    case LOB_FACULTATIVE_LIABILITY:
                        SetLOBString("FACLIAB");
                        base.ScreenId = "224_22";
                        break;
                    case LOB_GENERAL_CIVIL_LIABILITY:
                        SetLOBString("GENCVLLIB");
                        base.ScreenId = "224_17";
                        break;
                    case LOB_INDIVIDUAL_PERSONAL_ACCIDENT:
                        SetLOBString("INDPA");
                        base.ScreenId = "224_20";
                        break;
                    case LOB_MARITIME:
                        SetLOBString("MTIME");
                        base.ScreenId = "224_18";
                        break;
                    case LOB_ROBBERY:
                        SetLOBString("ROBBERY");
                        base.ScreenId = "224_21";
                        break;
                    case LOB_NATIONAL_AND_INTERNATIONAL_TRANSPORTATION:
                        SetLOBString("NATNTR");
                        base.ScreenId = "224_27";
                        break;
                    //Added by pradeep Kushwaha on 28-April-2010  
                    case GROUP_PASSENGER_PERSONAL_ACCIDENT:
                        SetLOBString("CPCACC");
                        base.ScreenId = "224_35";
                        break;
                    case PASSENGER_PERSONAL_ACCIDENT:
                        SetLOBString("PAPEACC");
                        base.ScreenId = "224_36";
                        break;
                    case INTERNATIONAL_CARGO_TRANSPORT:
                        SetLOBString("INTERN");
                        base.ScreenId = "224_37";
                        break;
                    case TRADITIONAL_FIRE:
                        SetLOBString("TFIRE");
                        base.ScreenId = "224_48";
                        break;
                    case ENGENEERING_RISKS:
                        SetLOBString("ERISK");
                        base.ScreenId = "224_46";
                        break;
                    case GROUP_LIFE:
                        SetLOBString("GRPLF");
                        base.ScreenId = "224_39";
                        break;
                    case MORTGAGE:
                        SetLOBString("MRTG");
                        base.ScreenId = "224_40";
                        break;
                    case DPVAT:
                        SetLOBString("DPVA");
                        base.ScreenId = "224_41";
                        break;
                    case JUDICIAL_GUARANTEE:
                        SetLOBString("JDLGR");
                        base.ScreenId = "224_42";
                        break;
                    case GLOBAL_OF_BANK:
                        SetLOBString("GLBANK");
                        base.ScreenId = "224_43";
                        break;
                    case MOTOR:
                        SetLOBString("MTOR");
                        base.ScreenId = "224_45";
                        break;
                    case AERONAUTIC:
                        SetLOBString("AERO");
                        base.ScreenId = "224_44";
                        break;
                    case CARGO_TRANSPORTATION_CIVIL_LIABILITY:
                        SetLOBString("CTCL");
                        base.ScreenId = "224_47";
                        break;
                    case RURAL_LIEN:
                        SetLOBString("RLLE");
                        base.ScreenId = "224_49";
                        break;
                    case DPVAT2:
                        SetLOBString("DPVAT2");
                        base.ScreenId = "224_50";
                        break;
                    case RETSURTY:
                        SetLOBString("RETSURTY");
                        base.ScreenId = "224_51";
                        break;



                }

                //Loading the policy menu 
                //if (strOldLobString.ToUpper() != GetLOBString().ToUpper())

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objReinstatementProcess = null;
            }
        }

        private void GetCustomerIDAppIDAppVersionIDValues()
        {
            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
            {
                strCustomerId = Request["CUSTOMER_ID"].ToString();
            }
            else
            {
                strCustomerId = GetCustomerID();
            }

            if (Request.QueryString["app_id"] != null)
            {
                strAppId = Request.QueryString["app_id"];
                SetAppID(Request.QueryString["app_id"]);
            }
            else
                strAppId = GetAppID();

            if (Request.QueryString["app_version_id"] != null)
            {
                strAppVersionId = Request.QueryString["app_version_id"];
                SetAppVersionID(Request.QueryString["app_version_id"]);
            }
            else
                strAppVersionId = GetAppVersionID();

            if (Request.Params["CalledFrom"] == CALLED_FROM_CLIENT || Request.Params["CalledFrom"] == CALLED_FROM_INNER_CLIENT)
            {
                strCustomerId = GetCustomerID();
                strAppId = Request.Params["APP_ID"];
                strAppVersionId = Request.Params["APP_VERSION_ID"];
            }

            if (Request.Params["TabNumber"] != null && Request.Params["TabNumber"] != "")
            {
                hidTabNumber.Value = Request.Params["TabNumber"].ToString();
            }

        }

        private void GetPolicyIDPolicyVersionIDLobIDValues()
        {
            if (Request["POLICY_ID"] != null && Request["POLICY_ID"].ToString() != "")
            {
                strPolicyID = Request["POLICY_ID"].ToString();
            }
            else
            {
                strPolicyID = GetPolicyID();
            }

            if (Request["POLICY_VERSION_ID"] != null && Request["POLICY_VERSION_ID"].ToString() != "")
            {
                strPolicyVersionID = Request["POLICY_VERSION_ID"].ToString();
            }
            else
            {
                strPolicyVersionID = GetPolicyVersionID();
            }

            if (Request["POLICY_VERSION_ID"] != null && Request["POLICY_ID"] != null && Request["POLICY_ID"].ToString() != "" && Request["POLICY_VERSION_ID"].ToString() != "")
            {
                Cms.BusinessLayer.BlProcess.ClsReinstatementProcess objReinstatementProcess = new Cms.BusinessLayer.BlProcess.ClsReinstatementProcess();
                SetLOBID(objReinstatementProcess.GetLOBID(Convert.ToInt32(strCustomerId), Convert.ToInt32(Request["POLICY_ID"].ToString()), Convert.ToInt32(Request["POLICY_VERSION_ID"].ToString())).ToString());
                strLobID = GetLOBID();
                objReinstatementProcess = null;
            }
            else
            {
                strLobID = GetLOBID();
            }
        }

        private void ShowClientTopControl(int intCustomerId, string strAPP)
        {
            if (strAPP.Equals("POL"))
            {
                cltClientTop.CustomerID = intCustomerId;
                cltClientTop.PolicyID = strPolicyID == "" ? 0 : int.Parse(strPolicyID);
                cltClientTop.PolicyVersionID = strPolicyVersionID == "" ? 0 : int.Parse(strPolicyVersionID);
                cltClientTop.ShowHeaderBand = "Policy";
                cltClientTop.Visible = true;
            }
            else
            {
                if (intCustomerId != 0)
                {
                    cltClientTop.CustomerID = intCustomerId;
                    cltClientTop.Visible = true;
                    cltClientTop.ShowHeaderBand = "Client";
                }
                else
                {
                    cltClientTop.Visible = false;
                }
            }

        }

        private void SetWorkFlow()
        {
            //if (base.ScreenId == "224_1" || base.ScreenId == "224_2" || base.ScreenId == "224_3" || base.ScreenId == "224_4" || base.ScreenId == "224_5" || base.ScreenId == "224_6" || base.ScreenId == "224_7" || base.ScreenId == "224_13")
            if (base.ScreenId.Contains("224_"))
            {
                myWorkFlow.ScreenID = base.ScreenId;
                myWorkFlow.AddKeyValue("CUSTOMER_ID", strCustomerId);
                myWorkFlow.AddKeyValue("POLICY_ID", GetPolicyID());
                myWorkFlow.AddKeyValue("POLICY_VERSION_ID", GetPolicyVersionID());
                myWorkFlow.WorkflowModule = "POL";
            }
            else
            {
                myWorkFlow.Display = false;
            }

        }
        private void OpenTreeNaveegation()
        {
            string Navigation = string.Empty;
            string iCustomer_id = string.Empty;
            string iPolicy_id = string.Empty;
            string iPolicy_Version_id = string.Empty;
            string iLob_id = string.Empty;
            string iLob_String = string.Empty;
            //if (Request.QueryString["NAVIGATION"] != null && Request.QueryString["NAVIGATION"].ToString() != "")
            //{
            //    Navigation = Request.QueryString["NAVIGATION"].ToString();
            //}
            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
            {
                iCustomer_id = Request.QueryString["CUSTOMER_ID"].ToString();
            }
            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
            {
                iPolicy_id = Request.QueryString["POLICY_ID"].ToString();
            }
            else if (Request.QueryString["APP_ID"] != null && Request.QueryString["APP_ID"].ToString() != "")
            {
                iPolicy_id = Request.QueryString["APP_ID"].ToString();
            }

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
            {
                iPolicy_Version_id = Request.QueryString["POLICY_VERSION_ID"].ToString();
            }
            else if (Request.QueryString["APP_VERSION_ID"] != null && Request.QueryString["APP_VERSION_ID"].ToString() != "")
            {
                iPolicy_Version_id = Request.QueryString["APP_VERSION_ID"].ToString();
            }

            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
            {
                iLob_id = Request.QueryString["LOB_ID"].ToString();
            }
            else if (Request.QueryString["POLICY_LOB"] != null && Request.QueryString["POLICY_LOB"].ToString() != "")
            {
                iLob_id = Request.QueryString["POLICY_LOB"].ToString();
            }


            if (iCustomer_id != "")
                SetCustomerID(iCustomer_id);
            if (iPolicy_id != "")
                SetPolicyID(iPolicy_id);
            if (iPolicy_Version_id != "")
                SetPolicyVersionID(iPolicy_Version_id);
            if (iLob_id == "" && iCustomer_id != "" && iPolicy_id != "" && iPolicy_Version_id != "")
            {
                DataSet ds = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.FetchApplicationLobID(int.Parse(iCustomer_id), int.Parse(iPolicy_id), int.Parse(iPolicy_Version_id));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    iLob_id = ds.Tables[0].Rows[0]["LOB_ID"].ToString();
                }
                ds.Dispose();

            }

            if (iLob_id != "")
                SetLOBID(iLob_id);

            Navigation = GetSysNavigation();

            if (iLob_id != "")
            {
                SetLOBString(ClsGeneralInformation.GetLOBCodeFromID(int.Parse(iLob_id)));
                iLob_String = GetLOBString();
            }
            if (Navigation.ToUpper() == "TREENAVIGATION" && iPolicy_id != "" && iPolicy_Version_id != "")
            {
                string QueryString = "CUSTOMER_ID=" + iCustomer_id + "&POLICY_ID=" + iPolicy_id + "&POLICY_VERSION_ID=" + iPolicy_Version_id + "&LOB_ID=" + iLob_id + "&Lob_String=" + iLob_String;
                Response.Redirect(@"/Cms/Policies/Aspx/Default.aspx?" + QueryString); //+ iCustomer_id + "&POLICY_ID=" + iPolicy_id + "&POLICY_VERSION_ID=" + iPolicy_Version_id + "&LOB_ID=" + iLob_id + "&iLob_String=" + iLob_String);
            }
        }
    }

}

