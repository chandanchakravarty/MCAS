/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 15/06/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the user defined screen module
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb; 

namespace Cms.CmsWeb.UserDefined
{
	/// <summary>
	/// Summary description for SubmitGridQuestion.
	/// </summary>
	public class SubmitGridQuestion : Cms.CmsWeb.cmsbase  
	{

        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Table tblReport;
        protected System.Web.UI.WebControls.Panel pnlReport;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlForm SubmitScreen;		// Stores the RGB value for grid Base
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTemplateID;   
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabID;   
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidGroupID;
        protected System.Web.UI.HtmlControls.HtmlForm SubmitQuestion;   
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;   
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQid;   
        protected string lblQID;
		private void Page_Load(object sender, System.EventArgs e)
		{
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

            string lStrTabID=Request["TabID"];
            string lStrGroupID = Request["GroupID"];
            string lStrTemplateID = Request["ScreenID"];
            string lStrCalledFrom = Request["CalledFrom"];

            hidTemplateID.Value     = lStrTemplateID;
            hidTabID.Value          = lStrTabID;
            hidGroupID.Value        = lStrGroupID;  
            hidCalledFrom.Value     = lStrCalledFrom;  
            lblQID             = Request.QueryString["QID"]; 
            hidQid.Value = lblQID;

            #region loading web grid control
            Control c1= LoadControl("../webcontrols/BaseDataGrid.ascx");
            try
            {
                
                /*************************************************************************/
                ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
                /************************************************************************/
                //specifying webservice URL
                ((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                //specifying columns for select query
                ((BaseDataGrid)c1).SelectClause ="QD.QGRIDOPTIONID, QD.OPTIONTEXT, QD.ISACTIVE,QD.OPTIONTYPEID,convert(varchar(15),QD.LASTMODIFIEDDATE,101) LASTMODIFIEDDATE,UL.USER_FNAME + ' ' + UL.USER_LNAME NAME";
                //specifying tables for from clause

                ((BaseDataGrid)c1).FromClause="QUESTIONGRID QD LEFT OUTER JOIN MNT_USER_LIST UL ON UL.USER_ID = QD.LASTMODIFIEDBY";
                
				//manab modified the code added tabid and screen id in login
				((BaseDataGrid)c1).WhereClause="QD.QID =" + lblQID;

				//((BaseDataGrid)c1).WhereClause="QD.QID =" + lblQID + " and groupid=" + hidGroupID.Value;

                
                
                
                //specifying Text to be shown in combo box
                ((BaseDataGrid)c1).SearchColumnHeadings="Option Description^Amended On^Amended By";
                //specifying column to be used for combo box
                ((BaseDataGrid)c1).SearchColumnNames="QD.OPTIONTEXT^QD.LASTMODIFIEDDATE^NAME";
                //search column data type specifying data type of the column to be used for combo box
                ((BaseDataGrid)c1).SearchColumnType="T^D^T";
                //specifying column for order by clause
                ((BaseDataGrid)c1).OrderByClause="OPTIONTEXT asc";
                //specifying column numbers of the query to be displyed in grid
                ((BaseDataGrid)c1).DisplayColumnNumbers="2^5^6";
                //specifying column names from the query
                ((BaseDataGrid)c1).DisplayColumnNames="OPTIONTEXT^LASTMODIFIEDDATE^NAME";
                //specifying text to be shown as column headings
                ((BaseDataGrid)c1).DisplayColumnHeadings="Option Description^Amended On^Amended By";
                //specifying column heading display text length
                ((BaseDataGrid)c1).DisplayTextLength="150^50^50";
                //specifying width percentage for columns
                ((BaseDataGrid)c1).DisplayColumnPercent="50^25^25";
                //specifying primary column number
                ((BaseDataGrid)c1).PrimaryColumns="1";
                //specifying primary column name
                ((BaseDataGrid)c1).PrimaryColumnsName="QD.QGRIDOPTIONID^QD.QID";
                //specifying column type of the data grid
                ((BaseDataGrid)c1).ColumnTypes="B^B^B";
                //specifying links pages 
                //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
                //specifying if double click is allowed or not
                ((BaseDataGrid)c1).AllowDBLClick="true"; 
                //specifying which columns are to be displayed on first tab
                ((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
                //specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                //specifying buttons to be displayed on grid
                if(lStrGroupID=="")
                    ((BaseDataGrid)c1).ExtraButtons="2^Add New~Back To Questions^0~1^addRecord~fnMovePrev";
                else
                    ((BaseDataGrid)c1).ExtraButtons="2^Add New~Back To Question^0~1^addRecord~fnMovePrev";
                //specifying number of the rows to be shown
                ((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());
                //specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
                //specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                //specifying heading
                ((BaseDataGrid)c1).HeaderString = "Search Grid Questions";
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
                ((BaseDataGrid)c1).QueryStringColumns ="QGRIDOPTIONID^QID";
                ((BaseDataGrid)c1).DefaultSearch="Y";   
                  
                // to show completed task we have to give check box
                GridHolder.Controls.Add(c1);
            }
            catch
            {}
        
            #endregion

            TabCtl.TabURLs = @"PostGridQuestion.aspx?QID=" + lblQID +"&ScreenID=" +  lStrTemplateID + "&TabID=" + lStrTabID + "&GroupID=" + lStrGroupID + "&CalledFrom=" + lStrCalledFrom + "&";
            

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
