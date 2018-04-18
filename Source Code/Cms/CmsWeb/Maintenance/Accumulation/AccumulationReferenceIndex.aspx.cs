/******************************************************************************************
<Author				: -		Kuldeep Saxena
<Start Date			: -		20-10-2011
<End Date			: -	
<Description		: - 	Idex page for Accumulation Criteria
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


namespace Cms.CmsWeb.Maintenance.Accumulation
{
    public partial class AccumulationReferenceIndex : Cms.CmsWeb.cmsbase
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
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Setting screen id and Type ID
                 base.ScreenId = "568";
            #endregion
           

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
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

            try
            {

                //Added by Agniswar
                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                string XmlFullFilePath = ClsCommon.WebAppUNCPath + "/cmsweb/support/PageXml/" + strSysID + "/AccumulationReferenceIndex.xml ";

                SetDBGrid(objWebGrid, XmlFullFilePath, "");

                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.SelectClass = colors;

                //Adding to controls to gridholder
                GridHolder.Controls.Add(objWebGrid);

                TabCtl.TabURLs = "AddAccumulationReference.aspx?&";
                TabCtl.TabLength = 150;

                TabCtl.TabTitles = "Accumulation Reference";


            }
            catch (Exception ex)
            { throw (ex); }
            #endregion
        }
    }
}