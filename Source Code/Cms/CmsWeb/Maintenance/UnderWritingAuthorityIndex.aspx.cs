/******************************************************************************************
<Author				: -		Amit Kr. Mishra
<Start Date			: -		03-11-2011
<End Date			: -	
<Description		: - 	Index page for underWriting Authority Cliams Limit
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
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
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;


namespace CmsWeb.Maintenance
{
    public partial class UnderWritingAuthorityIndex : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Table tblReport;
        protected System.Web.UI.WebControls.Panel pnlReport;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Literal litTextGrid;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.WebControls.Label lblError;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

        
        ResourceManager objResourceMgr = null;
        string strUSER_ID="";
        string strCalledFrom = "", strLobid="";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Setting screen id and Type ID
                 base.ScreenId = "25_3";
            #endregion
           

            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme = GetColorScheme();
            string colors = "";
            
            if (Request.QueryString["UserId"] != string.Empty)
            {
                strUSER_ID = Request.QueryString["UserId"].ToString();
            }
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
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

            try
            {

                //Added by Agniswar
                string strSysID = GetSystemId();
                string XmlFullFilePath = "";
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

               
               //Added by Ruchika on 6-Feb-2012 for TFS # 3322
                strCalledFrom = Request.QueryString["CalledFrom"].ToString();
              
                if (strCalledFrom == "UnderWriterAuth")
                {
                    XmlFullFilePath = ClsCommon.WebAppUNCPath + "/cmsweb/support/PageXml/" + strSysID + "/UnderWritingAuthorityIndex1.xml ";
                    strLobid = Request.QueryString["lobid"].ToString();
                }
                else
                {
                    XmlFullFilePath = ClsCommon.WebAppUNCPath + "/cmsweb/support/PageXml/" + strSysID + "/UnderWritingAuthorityIndex.xml ";
                }

                string current_date = DateTime.Now.ToString();
                SetDBGrid(objWebGrid, XmlFullFilePath, "");

                //Added by Ruchika on 6-Feb-2012 for TFS # 3322
                if (strCalledFrom == "UnderWriterAuth")
                {
                    objWebGrid.WhereClause = "USER_ID = '" + strUSER_ID + "'" + " and MUCL.LOB_ID = '" + strLobid+"'";
                }
                else
                {
                    //objWebGrid.WhereClause = "USER_ID = '" + strUSER_ID + "'";                    
                    objWebGrid.WhereClause = " MUCL.ASSIGN_ID in(select max(assign_id)  from MNT_UNDERWRITING_CLAIM_LIMITS where USER_ID='" + strUSER_ID + "'"+"  group by lob_id )";
                }
               

                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";                
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.SelectClass = colors;

                if (strCalledFrom == "UnderWriterAuth")
                {
                    objWebGrid.RequireQuery = "N";
                    TabCtl.TabURLs = "";
                    TabCtl.TabLength = 0;
                    objWebGrid.RequireNormalCursor = "Y";
                    TabCtl.TabTitles = "";
                }
                else
                {                    
                    TabCtl.TabURLs = "AddUnderWritingAuthority.aspx?UserId=" + strUSER_ID + "&";
                    TabCtl.TabLength = 250;
                    TabCtl.TabTitles = "Underwriting Limits";
                }
                //Adding to controls to gridholder
                GridHolder.Controls.Add(objWebGrid);
            }
            catch (Exception ex)
            { throw (ex); }
            #endregion
        }
    }
    }
