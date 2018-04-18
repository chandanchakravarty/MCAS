//------------------------------------------------------------------------------
// Created by Pradeep Kushwaha on 15-03-2010
// 
//------------------------------------------------------------------------------
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
using System.Resources;
using System.Reflection;

namespace CmsWeb.Accounting
{
    /// <summary>
    /// This is the partial index Class of bank inforamtion page 
    /// Use to display the bank information and provide the search criteria of the searchable fields
    /// Bank information add facility
    /// 
    /// </summary>
    public partial class BankInformationIndex : Cms.CmsWeb.cmsbase 
    {
        private int CacheSize = 1400;
        ResourceManager objResourceMgr = null;
     
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        /// <summary>
        /// This method is use to load the all required details on the page load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "451";
            objResourceMgr = new ResourceManager("CmsWeb.Accounting.BankInformationIndex", Assembly.GetExecutingAssembly());
            #region GETTING BASE COLOR FOR ROW SELECTION
            
            string colorScheme = GetColorScheme();
            string colors = "";

            switch (int.Parse(colorScheme))
            {
                case 1:
                    colors  = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();
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
            
            //BaseDataGrid object created 
            BaseDataGrid objBaseDataGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                //Set the BaseDataGrid Control Properties

                objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL"; ;
                objBaseDataGrid.SelectClause = "GL_ID,ACCOUNT_ID,BANK_ID,BANK_NUMBER,BANK_NAME,BANK_ADDRESS1";
                objBaseDataGrid.FromClause = "ACT_BANK_INFORMATION";
                objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Bank Number^Bank Name^Bank Address1";
                objBaseDataGrid.SearchColumnNames = "BANK_NUMBER^BANK_NAME^BANK_ADDRESS1";
                objBaseDataGrid.SearchColumnType = "T^T^T";
                objBaseDataGrid.OrderByClause = "BANK_ID desc";
                objBaseDataGrid.DisplayColumnNumbers = "1^2^3";
                objBaseDataGrid.DisplayColumnNames = "BANK_NUMBER^BANK_NAME^BANK_ADDRESS1";
                objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Bank Number^Bank Name^Bank Address1";
                objBaseDataGrid.DisplayTextLength = "100^100^100";
                objBaseDataGrid.DisplayColumnPercent = "15^20^20";
                objBaseDataGrid.PrimaryColumns = "1";
                objBaseDataGrid.PrimaryColumnsName = "BANK_ID";
                objBaseDataGrid.ColumnTypes = "B^B^B";
                objBaseDataGrid.AllowDBLClick = "true";
                objBaseDataGrid.FetchColumns = "1^2^3";
                objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                objBaseDataGrid.CacheSize = CacheSize;
                objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objBaseDataGrid.HImagePath= System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Bank Information";
                objBaseDataGrid.SelectClass = colors;
                objBaseDataGrid.RequireQuery = "Y";
                objBaseDataGrid.QueryStringColumns = "BANK_ID^GL_ID^ACCOUNT_ID";
                objBaseDataGrid.DefaultSearch = "Y";
                objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                objBaseDataGrid.FilterColumnName = "ISNULL(IS_ACTIVE,'Y')";
                objBaseDataGrid.FilterValue = "Y";

                GridHolder.Controls.Add(objBaseDataGrid);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            TabCtl.TabURLs = "AddBankInformation.aspx?CALLED_FOR=BANKINFO&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;

        }
    }
}
