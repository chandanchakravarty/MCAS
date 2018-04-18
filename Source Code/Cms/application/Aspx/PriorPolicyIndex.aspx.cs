/******************************************************************************************
<Author				: -   Vijay Joshi
<Start Date				: -	4/25/2005 9:15:13 PM
<End Date				: -	
<Description				: - 	Show index page of Prior Policy
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  4/5/2005
<Modified By				: - Anurag Verma
<Purpose				: - Removing use of app_id and app_version_id
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
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
namespace Cms.Application.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class  PriorPolicyIndex: Cms.Application.appbase
	{

		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidmsg;
		//Done for Itrack Issue 6708 on 19 Nov 09
		protected int customer_ID;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"117";
            objResourceMgr = new ResourceManager("Cms.Application.Aspx.PriorPolicyIndex", Assembly.GetExecutingAssembly());

			/* This Security XML has been explicitly specified.
			 * It is done coz, when we go to this page, the ApplicationID session variable has a value.
			 * This in turn checks for a converted Application and then accordingly sets security XML,
			 * resulting in different Permission XML. Refer Support>Appbase.cs (Line no 70)
			*/
			//gstrSecurityXML = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
			
			//modified By Sibin on 29 dec 08 as per above Comment; to handle Rights of this page, this page will fetch SecurityXML again - Itrack Issue 5158
			SetSecurityXML(base.ScreenId, int.Parse(GetUserId()));
			//base.InitializeSecuritySettings();

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";
            hidmsg.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1"); //"Do you wish to Fetch Prior Policy Report?";
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


            //Done for Itrack Issue 6708 on 19 Nov 09
            customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());



            
            if(customer_ID!=0)
            {


				#region loading web grid control
			

			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{ 
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = "CUSTOMER_ID,APP_PRIOR_CARRIER_INFO_ID,CARRIER,OLD_POLICY_NUMBER,ISNULL(MNT_LOB_MASTER_MULTILINGUAL.LOB_DESC,MNT_LOB_MASTER.LOB_DESC) as LOB_DESC,Convert(Varchar,EFF_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end) As EFF_DATE,Convert(Varchar,EXP_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end) As EXP_DATE,MNT_LOB_MASTER.LOB_ID";/*LOB_DESC*/
				
				//objWebGrid.FromClause = "APP_PRIOR_CARRIER_INFO  LEFT JOIN MNT_LOOKUP_VALUES ON APP_PRIOR_CARRIER_INFO.LOB = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID";/*LEFT JOIN MNT_LOB_MASTER ON APP_PRIOR_CARRIER_INFO.LOB = MNT_LOB_MASTER.LOB_ID";*/
				objWebGrid.FromClause = "APP_PRIOR_CARRIER_INFO  LEFT JOIN MNT_LOB_MASTER ON APP_PRIOR_CARRIER_INFO.LOB = CONVERT(VARCHAR,MNT_LOB_MASTER.LOB_ID)  LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL ON APP_PRIOR_CARRIER_INFO.LOB=CONVERT(VARCHAR,MNT_LOB_MASTER_MULTILINGUAL.LOB_ID)AND MNT_LOB_MASTER_MULTILINGUAL.LANG_ID="+GetLanguageID();

				objWebGrid.WhereClause = " APP_PRIOR_CARRIER_INFO.CUSTOMER_ID=" + customer_ID;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Carrier^Old Policy Number^Line of Business";
				objWebGrid.SearchColumnNames = "CARRIER^OLD_POLICY_NUMBER^MNT_LOB_MASTER.LOB_ID";
				objWebGrid.DropDownColumns="^^LOB";
				objWebGrid.SearchColumnType = "T^T^T";
				objWebGrid.OrderByClause = "CARRIER asc";
				objWebGrid.DisplayColumnNumbers = "3^4^5^6^7";
				objWebGrid.DisplayColumnNames = "APP_PRIOR_CARRIER_INFO_ID^CARRIER^OLD_POLICY_NUMBER^LOB_DESC^EFF_DATE^EXP_DATE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); //"Id^Carrier^Old Policy Number^Line of Business^Effective Date^Expiration Date";
				objWebGrid.DisplayTextLength = "10^40^40^70^50^40";
				objWebGrid.DisplayColumnPercent = "5^15^20^20^20^15";
				objWebGrid.PrimaryColumns = "2";
				objWebGrid.PrimaryColumnsName = "APP_PRIOR_CARRIER_INFO_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^ ";
				//Done for Itrack Issue 6708 on 19 Nov 09
				//objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"2^Add New~Request Prior Policy^0~1^addRecord~FetchPriorPolicyReport";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());

                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Prior Policy" ;
				objWebGrid.SelectClass = colors;

				//objWebGrid.SelectClass= "";
				//objWebGrid.FilterLabel = "Show Complete";
				//objWebGrid.FilterColumnName = "";
				//objWebGrid.FilterValue = "";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "APP_PRIOR_CARRIER_INFO_ID";
				objWebGrid.DefaultSearch="Y";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddPriorPolicy.aspx?CUSTOMER_ID=" + customer_ID + "&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 150;
			}
			catch(Exception ex)
            {
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion

                int flag=0;
                //cltClientTop.CustomerID = int.Parse(GetCustomerID());

                //if(GetAppID()!="" && GetAppID()!=null && GetAppID()!="0")
                //{
                //    cltClientTop.ApplicationID = int.Parse(GetAppID());
                //    flag=1;
                //}

                //if(GetAppVersionID()!="" && GetAppVersionID()!=null && GetAppVersionID()!="0")
                //{
                //    cltClientTop.AppVersionID = int.Parse(GetAppVersionID());
                //    flag=2;
                //}

                if (GetPolicyID() != "" && GetPolicyID() != null && GetPolicyID() != "0")
                {
                    //cltClientTop.PolicyID = int.Parse(GetPolicyID());
                    flag = 1;
                }
                if (GetPolicyVersionID() != "" && GetPolicyVersionID() != null && GetPolicyVersionID() != "0")
                {
                    //cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID());
                    flag = 2;
                }

                //if (flag > 0)

                //    cltClientTop.ShowHeaderBand = "Policy";//"Application"; //changed by Lalit for policy tab link
                //else
                //    cltClientTop.ShowHeaderBand = "Client";
				//Oct 19,2005:Sumit:Commented by Sumit
                //cltClientTop.Visible = true;        
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