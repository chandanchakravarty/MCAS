/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 25/04/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the priorloss module
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 8/7/2005	
	<Modified By			: - > Anurag Verma
	<Purpose				: - > changing query -- adding isnull check for Loss column
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Application.PriorLoss
{
	/// <summary>
	/// Summary description for PriorLossIndex.
	/// </summary>
	public class PriorLossIndex : Cms.Application.appbase          
	{
        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Table tblReport;
        protected System.Web.UI.WebControls.Panel pnlReport;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.WebControls.Label capMessage;
        ResourceManager objResourceMgr = null;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;		// Stores the RGB value for grid Base
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        private void Page_Load(object sender, System.EventArgs e)
        {
			base.ScreenId = "118";
			/* This Security XML has been explicitly specified.
			 * It is done coz, when we go to this page, the ApplicationID session variable has a value.
			 * This in turn checks for a converted Application and then accordingly sets security XML,
			 * resulting in different Permission XML. Refer Support>Appbase.cs (Line no 70)
			*/
			//gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			
			//modified By Sibin on 29 dec 08 as per above Comment; to handle Rights of this page, this page will fetch SecurityXML again - Itrack Issue 5045
			SetSecurityXML(base.ScreenId, int.Parse(GetUserId()));
			//base.InitializeSecuritySettings();

            // Put user code to initialize the page here
            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme=GetColorScheme();
            string colors="";

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

            if(colors!="")
            {
                string [] baseColor=colors.Split(new char []{','});  
                if(baseColor.Length>0)
                    colors= "#" + baseColor[0];
            }
            #endregion         


            
            int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());
            objResourceMgr = new ResourceManager("Cms.Application.PriorLOss.PriorLossIndex", Assembly.GetExecutingAssembly());	
            if(customer_ID!=0)
            {
         
            #region loading web grid control
            Control c1= LoadControl("../../cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                
                    /*************************************************************************/
                    ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
                    /************************************************************************/
                    //specifying webservice URL
                    ((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                    //specifying columns for select query
					//Done for Itrack Issue 6271 on 19 Aug 2009
					//((BaseDataGrid)c1).SelectClause ="PL.CUSTOMER_ID,PL.LOSS_ID,CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101) OCCURENCE_DATE,PL.LOB,PL.LOSS_TYPE,CONVERT(VARCHAR(15),PL.AMOUNT_PAID,1) AMOUNT_PAID,CONVERT(VARCHAR(15),PL.AMOUNT_RESERVED,1) AMOUNT_RESERVED,PL.CLAIM_STATUS,PL.LOSS_DESC,PL.REMARKS,PL.MOD,PL.LOSS_RUN,PL.CAT_NO,PL.CLAIMID,PL.IS_ACTIVE,PL.CREATED_BY,PL.CREATED_DATETIME,PL.MODIFIED_BY,PL.LAST_UPDATED_DATETIME,isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101),'') + ' ' +  (CASE PL.LOB WHEN '-1' THEN 'Other' WHEN '' THEN '' ELSE LV.LOB_DESC END) LOSS,LV.LOB_DESC LOB1,LV.LOB_ID , MLV.LOOKUP_VALUE_DESC   ";
                    //((BaseDataGrid)c1).SelectClause = "PL.CUSTOMER_ID,PL.LOSS_ID,CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end) OCCURENCE_DATE,PL.OCCURENCE_DATE AS OCCURENCE_DATE1,PL.LOB,PL.LOSS_TYPE,CONVERT(VARCHAR(15),PL.AMOUNT_PAID,1) AMOUNT_PAID,CONVERT(VARCHAR(15),PL.AMOUNT_RESERVED,1) AMOUNT_RESERVED,PL.CLAIM_STATUS,PL.LOSS_DESC,PL.REMARKS,PL.MOD,PL.LOSS_RUN,PL.CAT_NO,PL.CLAIMID,PL.IS_ACTIVE,PL.CREATED_BY,PL.CREATED_DATETIME,PL.MODIFIED_BY,PL.LAST_UPDATED_DATETIME,isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'') + ' ' +  (CASE PL.LOB WHEN '-1' THEN 'Other' WHEN '' THEN '' ELSE LV.LOB_DESC END) LOSS,LV.LOB_DESC LOB1,LV.LOB_ID , LV.LOB_ID ,dbo.fun_GetLookupDesc(PL.LOSS_TYPE,1) LOOKUP_VALUE_DESC    ";
                    ((BaseDataGrid)c1).SelectClause = "PL.CUSTOMER_ID,PL.LOSS_ID,CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end) OCCURENCE_DATE,PL.OCCURENCE_DATE AS OCCURENCE_DATE1,PL.LOB,PL.LOSS_TYPE,dbo.fun_FormatCurrency(PL.AMOUNT_PAID," + ClsCommon.BL_LANG_ID + ") AMOUNT_PAID,CONVERT(VARCHAR(15),PL.AMOUNT_RESERVED,1) AMOUNT_RESERVED,PL.CLAIM_STATUS,PL.LOSS_DESC,PL.REMARKS,PL.MOD,PL.LOSS_RUN,PL.CAT_NO,PL.CLAIMID,PL.IS_ACTIVE,PL.CREATED_BY,PL.CREATED_DATETIME,PL.MODIFIED_BY,PL.LAST_UPDATED_DATETIME,isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'') + ' ' +  (CASE PL.LOB WHEN '-1' THEN 'Other' WHEN '' THEN '' ELSE isnull(LVM.LOB_DESC,LV.LOB_DESC) END) LOSS,ISNULL(LVM.LOB_DESC, LV.LOB_DESC) LOB1,LV.LOB_ID , LV.LOB_ID ,dbo.fun_GetLookupDesc(PL.LOSS_TYPE," + ClsCommon.BL_LANG_ID + ") LOOKUP_VALUE_DESC    ";
					//((BaseDataGrid)c1).SelectClause ="PL.CUSTOMER_ID,PL.LOSS_ID,CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101) OCCURENCE_DATE,PL.LOB,PL.LOSS_TYPE,CONVERT(VARCHAR(15),PL.AMOUNT_PAID,1) AMOUNT_PAID,CONVERT(VARCHAR(15),PL.AMOUNT_RESERVED,1) AMOUNT_RESERVED,PL.CLAIM_STATUS,PL.LOSS_DESC,PL.REMARKS,PL.MOD,PL.LOSS_RUN,PL.CAT_NO,PL.CLAIMID,PL.IS_ACTIVE,PL.CREATED_BY,PL.CREATED_DATETIME,PL.MODIFIED_BY,PL.LAST_UPDATED_DATETIME,isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101),'') + ' ' +  (CASE PL.LOB WHEN '-1' THEN 'Other' WHEN '' THEN '' ELSE LV.LOB_DESC END) LOSS,LV.LOB_DESC LOB1,LV.LOB_ID , MLV.LOOKUP_VALUE_DESC   ";
                    //specifying tables for from clause
                    ((BaseDataGrid)c1).FromClause=" APP_PRIOR_LOSS_INFO PL LEFT JOIN MNT_LOB_MASTER LV ON PL.LOB=CONVERT(VARCHAR,LV.LOB_ID)    LEFT JOIN MNT_LOOKUP_VALUES MLV ON PL.LOSS_TYPE=MLV.LOOKUP_UNIQUE_ID   LEFT JOIN MNT_LOB_MASTER_MULTILINGUAL  LVM ON PL.LOB=CONVERT(VARCHAR,LVM.LOB_ID)  AND LVM.LANG_ID="+GetLanguageID();
                    //specifying conditions for where clause
                    ((BaseDataGrid)c1).WhereClause=" PL.CUSTOMER_ID=" + customer_ID ;
                    //specifying Text to be shown in combo box
					//Done for Itrack Issue 6271 on 8 Sept 2009
				    //((BaseDataGrid)c1).SearchColumnHeadings="Losses^Date of Occurence^Line of Business";
					//Added for Itrack Issue 6733 on 11 Nov 09
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Date of Occurrence^Losses^Line of Business";
                    //((BaseDataGrid)c1).SearchColumnHeadings="Date of Occurence^Losses^Line of Business";
					 ((BaseDataGrid)c1).DropDownColumns="^^LOB";	
                    //specifying column to be used for combo box
					//Done for Itrack Issue 6271 on 8 Sept 2009
					//((BaseDataGrid)c1).SearchColumnNames="(isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,101),'') ! ' ' !  isnull(LV.LOB_DESC,''))^PL.OCCURENCE_DATE^LV.LOB_ID";
                     ((BaseDataGrid)c1).SearchColumnNames = "PL.OCCURENCE_DATE^(isnull(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'') ! ' ' !  isnull(LV.LOB_DESC,''))^LV.LOB_ID";
                    //search column data type specifying data type of the column to be used for combo box
                    //Done for Itrack Issue 6271 on 8 Sept 2009
					//((BaseDataGrid)c1).SearchColumnType="T^D^T";
					((BaseDataGrid)c1).SearchColumnType="D^T^T";
                    //specifying column for order by clause
					//Done for Itrack Issue 6271 on 19 Aug 2009
					//((BaseDataGrid)c1).OrderByClause="LOSS asc";
                    ((BaseDataGrid)c1).OrderByClause="OCCURENCE_DATE1 asc";
                    //specifying column numbers of the query to be displyed in grid
                    ((BaseDataGrid)c1).DisplayColumnNumbers="21^3^22^23^6";
                    //specifying column names from the query
                    ((BaseDataGrid)c1).DisplayColumnNames="LOSS_ID^LOSS^OCCURENCE_DATE^LOB1^LOOKUP_VALUE_DESC^AMOUNT_PAID";
                    //specifying text to be shown as column headings
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); //"Id^Losses^Date of Occurrence^Line of Business^Loss Type^Amount Paid";
                    //specifying column heading display text length
                    ((BaseDataGrid)c1).DisplayTextLength="5^20^20^15^20^20";
                    //specifying width percentage for columns
                    ((BaseDataGrid)c1).DisplayColumnPercent="5^20^20^15^20^20";
                    //specifying primary column number
                    ((BaseDataGrid)c1).PrimaryColumns="2";
                    //specifying primary column name
                    ((BaseDataGrid)c1).PrimaryColumnsName="PL.LOSS_ID^PL.CUSTOMER_ID";
                    //specifying column type of the data grid
                    ((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B";
                    //specifying links pages 
                    //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
                    //specifying if double click is allowed or not
                    ((BaseDataGrid)c1).AllowDBLClick="true"; 
                    //specifying which columns are to be displayed on first tab
                    ((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23";
                    //specifying message to be shown
                    ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage"); //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                    //specifying buttons to be displayed on grid
                    ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons"); // "1^Add^0^addRecord";
                    //specifying number of the rows to be shown
                    ((BaseDataGrid)c1).PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
                    //specifying cache size (use for top clause)
                    ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
                    //specifying image path
                    ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    //specifying heading
                    ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString"); //"Prior Loss";
                    ((BaseDataGrid)c1).SelectClass = colors;
                    //specifying text to be shown for filter checkbox
                    //((BaseDataGrid)c1).FilterLabel="Show Complete";
                    //specifying column to be used for filtering
                    //((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
                    //value of filtering record
                    //((BaseDataGrid)c1).FilterValue="Y";
                    // property indiacating whether query string is required or not
                    ((BaseDataGrid)c1).RequireQuery ="Y";
                    // column numbers to create query string
                    ((BaseDataGrid)c1).QueryStringColumns ="LOSS_ID^CUSTOMER_ID";
					((BaseDataGrid)c1).DefaultSearch="Y";
                    // to show completed task we have to give check box
                    GridHolder.Controls.Add(c1);
                    TabCtl.TabURLs = "AddPriorLoss.aspx?CUSTOMER_ID=" + customer_ID + "&";
                    TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                    TabCtl.TabLength = 150;
                }
            catch(Exception ex)
            {
			Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
        
            #endregion
                //cltClientTop.CustomerID = int.Parse(GetCustomerID());
                //cltClientTop.ShowHeaderBand ="Client";
            }
            else
            {
             capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("115");       
             capMessage.Visible=true; 
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion
	}
}
