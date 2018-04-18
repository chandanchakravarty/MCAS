/******************************************************************************************
<Author				: -		Sneha
<Start Date			: -		28-10-2011
<End Date			: -	
<Description		: - 	
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


namespace Cms.CmsWeb.Maintenance
{
    public partial class MasterValuesIndex : Cms.CmsWeb.cmsbase
    {
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        private string TypeID = "";
        ResourceManager objResourceMgr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Setting screen id and Type ID
            if (Request.QueryString["TYPE_ID"] != null && Request.QueryString["TYPE_ID"] != "")
            {
                TypeID = Request.QueryString["TYPE_ID"];
            }

            switch (TypeID)
            {
                case "2":
                    base.ScreenId = "565";
                    TabCtl.TabTitles = "Recovery Master Details";
                    break;

                case "3":
                    base.ScreenId = "566";
                    TabCtl.TabTitles = "Fee Master Details";
                    break;
            }

            #endregion
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.MasterValuesIndex", Assembly.GetExecutingAssembly());

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

                string XmlFullFilePath = ClsCommon.WebAppUNCPath + "/cmsweb/support/PageXml/" + strSysID + "/MasterValuesIndex.xml";
                SetDBGrid(objWebGrid, XmlFullFilePath, TypeID);
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.SelectClass = colors;

                //Adding to controls to gridholder
                GridHolder.Controls.Add(objWebGrid);

                TabCtl.TabURLs = "AddMasterValues.aspx?TYPE_ID=" + TypeID + "&";
                TabCtl.TabLength = 150;
            }
            catch (Exception ex)
            { throw (ex); }
            #endregion
        }
      public int GetTabLength(string TypeID)
        {
            switch (TypeID)
            {
                case "1":
                case "4":
                case "5":
                case "8":
                case "10":
                    return 225;
                default: return 150;
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}