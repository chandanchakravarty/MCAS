/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	10-Nov-2011
<End Date			: -	
<Description		: - Display Initial load commited policy details list
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -   
<Purpose			: -   
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.Model.Account;
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{
    public partial class InitialLoadCommitedPolicyDetail : Cms.Account.AccountBase
    {
        
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "551_3";
            if (hidDelString.Value != "")
            {
                hidCALLED_FOR.Value = "";
                DeleteCommitedRecord();
                hidDelString.Value = "";
            }
            if (hidCALLED_FOR.Value != "" && hidCALLED_FOR.Value == "DEL_ALL")
            {
                hidDelString.Value = "";
                DeleteAllCommitedRecord();
                hidCALLED_FOR.Value = "";
            }
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

            #region loading web grid control
            SetCultureThread(GetLanguageCode());
            System.Resources.ResourceManager objResourceMgr = new ResourceManager("Cms.Account.Aspx.InitialLoadCommitedPolicyDetail", Assembly.GetExecutingAssembly());

            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            //hidErrormsg.Value = objResourceMgr.GetString("hidErrormsg");
            string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
            string IMPORT_REQUEST_ID = string.Empty;
            string pageMode = string.Empty;

            if (Request.QueryString["pageMode"] != null && Request.QueryString["pageMode"].ToString() != "")
                pageMode = Request.QueryString["pageMode"];
            if (Request.QueryString["IMPORT_REQUEST_ID"] != null && Request.QueryString["IMPORT_REQUEST_ID"].ToString() != "")
            {
                IMPORT_REQUEST_ID = Request.QueryString["IMPORT_REQUEST_ID"];
                hidIMPORT_REQUEST_ID.Value = Request.QueryString["IMPORT_REQUEST_ID"];
            }

            hidPageMode.Value = pageMode;
            #endregion
            if (pageMode == "EX")
            {
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                objWebGrid.SelectClause = "IMPORT_SERIAL_NO,IMPORT_REQUEST_ID,MIPD.CUSTOMER_ID,MIPD.POLICY_ID,MIPD.POLICY_VERSION_ID,POLICY_SEQUENCE_NO,ENDORSEMENT_SEQUENTIAL_NO,MIPD.POLICY_NUMBER AS IMPORTED_POLICY ";

                objWebGrid.FromClause = "MIG_IL_POLICY_DETAILS MIPD WITH(NOLOCK) ";
                                           


                objWebGrid.WhereClause = "HAS_ERRORS=0 AND HAS_COMMITED_ERROR=1 AND MIPD.IMPORT_REQUEST_ID=" + IMPORT_REQUEST_ID;
                //objWebGrid.WhereClause = "HAS_ERRORS=0 AND MIPD.IMPORT_REQUEST_ID=" + IMPORT_REQUEST_ID;


                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                objWebGrid.SearchColumnNames = "MIPD.POLICY_NUMBER";
                objWebGrid.SearchColumnType = "T";
                objWebGrid.OrderByClause = "IMPORT_SERIAL_NO desc";
                objWebGrid.DisplayColumnNumbers = "6^7^8";
                objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^ENDORSEMENT_SEQUENTIAL_NO^IMPORTED_POLICY";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");
                objWebGrid.DisplayTextLength = "30^30^40";
                objWebGrid.DisplayColumnPercent = "30^30^40";
                objWebGrid.ColumnTypes = "B^B^B";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "IMPORT_SERIAL_NO";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize()); ;
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                objWebGrid.RequireCheckbox = "Y";
                GridHolder.Controls.Add(objWebGrid);


                TabCtl.TabURLs = "InitialLoadApplicationDetails.aspx?pageMode=14939_E&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 200;
            }
            else
            {
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                objWebGrid.SelectClause = "CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,IMPORT_SERIAL_NO,IMPORT_REQUEST_ID,POLICY_SEQUENCE_NO,ENDORSEMENT_SEQUENTIAL_NO,POLICY_NUMBER AS IMPORTED_POLICY ";

                objWebGrid.FromClause = "MIG_IL_POLICY_DETAILS WITH(NOLOCK) ";
                                        


                objWebGrid.WhereClause = "HAS_ERRORS=0 AND HAS_COMMITED_ERROR=0 AND IMPORT_REQUEST_ID=" + IMPORT_REQUEST_ID ;
                //objWebGrid.WhereClause = "HAS_ERRORS=0 AND MIPD.IMPORT_REQUEST_ID=" + IMPORT_REQUEST_ID;


                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                objWebGrid.SearchColumnNames = "POLICY_NUMBER";
                objWebGrid.SearchColumnType = "T";
                objWebGrid.OrderByClause = "IMPORT_SERIAL_NO desc";
                objWebGrid.DisplayColumnNumbers = "6^7^8";
                objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^ENDORSEMENT_SEQUENTIAL_NO^IMPORTED_POLICY";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");
                objWebGrid.DisplayTextLength = "30^30^40";
                objWebGrid.DisplayColumnPercent = "30^30^40";
                objWebGrid.ColumnTypes = "B^B^B";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "IMPORT_SERIAL_NO";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize()); ;
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                GridHolder.Controls.Add(objWebGrid);


                TabCtl.TabURLs = "InitialLoadApplicationDetails.aspx?pageMode=" + pageMode + "&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 200;
                
            }

        }//protected void Page_Load(object sender, EventArgs e)

        private void DeleteCommitedRecord()
        {
            int result = 0;
            string []listIDInfo = hidDelString.Value.Replace("(","").Replace(")","").Replace(" OR ",",").Trim().Split(',');
            ClsAcceptedCOILoadDetails objAcceptedCOILoaddetails = new ClsAcceptedCOILoadDetails();
            ClsAcceptedCOILoadInfo objAcceptedCOILoadInfo = new ClsAcceptedCOILoadInfo();
            int IMPORT_REQUEST_ID = 0;               
            int IMPORT_SERIAL_NO = 0;
            IMPORT_REQUEST_ID = int.Parse(hidIMPORT_REQUEST_ID.Value);
            string strIMPORT_SERIAL_NO = string.Empty;

            for (int i = 0; i < listIDInfo.Length; i++)
            {
               if(int.TryParse(listIDInfo[i].Split('=')[1],out IMPORT_SERIAL_NO))
               {
                   strIMPORT_SERIAL_NO += "," + IMPORT_SERIAL_NO.ToString();

               }//if(int.TryParse(listIDInfo[i].Split('=')[1],out IMPORT_SERIAL_NO))

            }//for (int i = 0; i < listIDInfo.Length; i++)
            strIMPORT_SERIAL_NO=strIMPORT_SERIAL_NO.Remove(0, 1);
            result = objAcceptedCOILoaddetails.DeleteInialLoadPolicyCommit(objAcceptedCOILoadInfo, IMPORT_REQUEST_ID, strIMPORT_SERIAL_NO, "DEL");
            if (result > 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "<br />" + ClsMessages.FetchGeneralMessage("1094");//<br>Records have been deleted successfully ";
                this.hidErrMsg.Value = "1";
            }

        }//private void DeleteCommitedRecord()

        private void DeleteAllCommitedRecord()
        {
            int result = 0;
            ClsAcceptedCOILoadDetails objAcceptedCOILoaddetails = new ClsAcceptedCOILoadDetails();
            ClsAcceptedCOILoadInfo objAcceptedCOILoadInfo = new ClsAcceptedCOILoadInfo();

            int IMPORT_REQUEST_ID = 0;
            IMPORT_REQUEST_ID = int.Parse(hidIMPORT_REQUEST_ID.Value);
            result = objAcceptedCOILoaddetails.DeleteInialLoadPolicyCommit(objAcceptedCOILoadInfo,IMPORT_REQUEST_ID, "", "DEL_ALL");
            if (result > 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "<br />" + ClsMessages.FetchGeneralMessage("1094");//<br>Records have been deleted successfully ";
                this.hidErrMsg.Value = "1";
            }
        }//private void DeleteAllCommitedRecord()
    }
}