/******************************************************************************************
	<Author					: Mohit Gupta
	<Start Date				: 04/05/2005	>
	<End Date				: - >
	<Description			: - >
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
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for TransactionLogIndex.
	/// </summary>
	public class TransactionLogIndex1 : cmsbase
	{
		protected System.Web.UI.WebControls.Literal objectWindowsGrid;
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.HtmlControls.HtmlForm Department;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.Menu bottomMenu;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_NO;
        public String hidHeader;
        private ResourceManager objResourceMgr = null;
		public string strCssClass="";
		protected string strCalledFrom;
		private string strCustomerID,strAppId,strAppVersionId;
		private int custID;
		
		private void SetCookieValue ()
		{
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strSystemID = GetSystemId();
			if(strCarrierSystemID.ToUpper()!=strSystemID.ToUpper())
                Response.Cookies["LastVisitedTab"].Value = "5"; //Changed from 7 for Policy Page Implementation
			else
                Response.Cookies["LastVisitedTab"].Value = "5"; //Changed from 8 for Policy Page Implementation
			
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
            
			SetCookieValue();
			if(strCalledFrom == "MNT")
				base.ScreenId="395_0";
			else
				base.ScreenId ="120_6";

            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.TransactionLogIndex1", Assembly.GetExecutingAssembly());
            hidHeader = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1995");
			//Retreiving the query string
			strCalledFrom = Request.Params["CalledFrom"];
			if(strCalledFrom.ToUpper()!="INCLT" && strCalledFrom.ToUpper()!="MNT")
			{
				custID=int.Parse(GetCustomerID());
				bottomMenu.Visible=true;
				strCssClass="tableWidth";
			}			
			else if(strCalledFrom.ToUpper()!="MNT")
			{
				custID=int.Parse(Request.Params["CUSTOMER_ID"].ToString());
				bottomMenu.Visible=false;
				strCssClass="tableWidthHeader";
			}
			else
			{
				bottomMenu.Visible=true;
				strCssClass="tableWidth";
			}

			string strWhereClause = GetWhereClause(strCalledFrom);
			
			if (strCalledFrom == "CLT")
			{
				strAppId = "";
				strAppVersionId = "";
				TabCtl.TabURLs="TransactionLogDetail.aspx?CalledFrom=" + strCalledFrom +"&";
                TabCtl.TabTitles = objResourceMgr.GetString("TabTitles"); // Added by Charles on 19-Apr-2010 for Multilingual Implementation
				
			}
			else if (strCalledFrom == "InCLT")
			{
				strAppId = "";
				strAppVersionId = "";
				TabCtl.TabURLs="TransactionLogDetail.aspx?CalledFrom=" + strCalledFrom +"&CUSTOMER_ID="+custID+"&";
                TabCtl.TabTitles = objResourceMgr.GetString("TabTitles"); // Added by Charles on 19-Apr-2010 for Multilingual Implementation
			}
			else if(strCalledFrom == "MNT")
			{
				TabCtl.TabURLs="TransactionLogDetail.aspx?CalledFrom=" + strCalledFrom +"&";
                TabCtl.TabTitles = objResourceMgr.GetString("TabTitles"); // Added by Charles on 19-Apr-2010 for Multilingual Implementation
			}
			else
			{
				strAppId = base.GetAppID();
				strAppVersionId = base.GetAppVersionID();
				TabCtl.TabURLs="TransactionLogDetail.aspx?CalledFrom=" + strCalledFrom 
					+ "&CUSTOMER_ID=" + custID
					+ "&APP_ID=" + strAppId 
					+ "&APP_VERSION_ID=" + strAppVersionId + "&";
                TabCtl.TabTitles = objResourceMgr.GetString("TabTitles"); // Added by Charles on 19-Apr-2010 for Multilingual Implementation
			}
			
			if (!CanShow())
			{
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");
				capMessage.Visible=true;
				return;
			}
			else
			{
				int flag=0;
				if(strCalledFrom.ToUpper()!="INCLT" && strCalledFrom.ToUpper()!="MNT")
				{
					cltClientTop.CustomerID = int.Parse(GetCustomerID());
				}
				else if(strCalledFrom.ToUpper()=="MNT")
				{
					cltClientTop.Visible = false;        
					flag=-1;
				}
				else
				{
					cltClientTop.CustomerID = custID;
					cltClientTop.Visible = false;        
				}


				if(GetAppID()!="" && GetAppID()!=null && GetAppID()!="0")
				{
					cltClientTop.ApplicationID = int.Parse(GetAppID());
					flag=1;
				}

				if(GetAppVersionID()!="" && GetAppVersionID()!=null && GetAppVersionID()!="0")
				{
					cltClientTop.AppVersionID = int.Parse(GetAppVersionID());
					flag=2;
				}
            
				if(flag>0)
					cltClientTop.ShowHeaderBand ="Application";
				else if(flag==-1)
				{
					
				}
				else
					cltClientTop.ShowHeaderBand ="Client";

				if(strCalledFrom.ToUpper()!="INCLT" && strCalledFrom.ToUpper()!="MNT")
					cltClientTop.Visible = true;        
			}

			// Put user code to initialize the page here
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
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				bool ShowGrid =true;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				//objWebGrid.SelectClause = "MNT_TRANSACTION_LOG.TRANS_ID,MNT_TRANSACTION_LOG.TRANS_TYPE_ID as TransactionType,CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME+' '+ CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME as CustomerName,MNT_TRANSACTION_LOG.RECORDED_BY as RecordedBy,Convert(varchar(10),MNT_TRANSACTION_LOG.RECORD_DATE_TIME,101) as RecordedDate,MNT_TRANSACTION_LOG.TRANS_DESC as Description";
                
				//objWebGrid.SelectClause = "MNT_TRANSACTION_LOG.TRANS_ID,MNT_TRANSACTION_LOG.TRANS_TYPE_ID as TransactionType,RTRIM(RTRIM(isnull(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'')+' '+ isnull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'')) + ' ' + isnull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'')) as CustomerName,RTRIM(isnull(MNT_USER_LIST.USER_FNAME,'')+' '+ isnull(MNT_USER_LIST.USER_LNAME,'')) AS RecordedBy ,MNT_TRANSACTION_LOG.RECORD_DATE_TIME as RecordedDate,MNT_TRANSACTION_LOG.TRANS_DESC as Description";
				//objWebGrid.SelectClause = "MNT_TRANSACTION_LOG.TRANS_ID,MNT_TRANSACTION_LOG.TRANS_TYPE_ID as TransactionType,RTRIM(isnull(MNT_USER_LIST.USER_FNAME,'')+' '+ isnull(MNT_USER_LIST.USER_LNAME,'')) AS RecordedBy ,MNT_TRANSACTION_LOG.RECORD_DATE_TIME as RecordedDate,MNT_TRANSACTION_LOG.TRANS_DESC as Description,ISNULL(AL.APP_NUMBER,'') AS AppNumber ,ISNULL(PCL.POLICY_NUMBER,'') AS PolicyNumber";
				if(!strCalledFrom.Equals("MNT"))
				{
                    objWebGrid.SelectClause = "MNT_TRANSACTION_LOG.TRANS_ID,MNT_TRANSACTION_LOG.TRANS_TYPE_ID as TransactionType,RTRIM(isnull(MNT_USER_LIST.USER_FNAME,'')+' '+ isnull(MNT_USER_LIST.USER_LNAME,'')) AS RecordedBy ,ISNULL(Convert(varchar,MNT_TRANSACTION_LOG.RECORD_DATE_TIME,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end)+' '+convert(char(8),MNT_TRANSACTION_LOG.RECORD_DATE_TIME,8),'') as RecordedDate,MNT_TRANSACTION_LOG.TRANS_DESC as Description,CASE ISNULL(PCL.APP_ID,'') 	WHEN  '' THEN  ISNULL(AL.APP_NUMBER,'')  	WHEN 0 THEN  ISNULL(AL.APP_NUMBER,'') 	ELSE PCL.APP_NUMBER END  AppNumber , ISNULL(PCL.POLICY_NUMBER,'') AS PolicyNumber";
				
					//objWebGrid.FromClause = "MNT_TRANSACTION_LOG LEFT OUTER JOIN CLT_CUSTOMER_LIST 	ON MNT_TRANSACTION_LOG.CLIENT_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID 	LEFT JOIN APP_LIST AL ON AL.APP_ID = MNT_TRANSACTION_LOG.APP_ID AND AL.CUSTOMER_ID = MNT_TRANSACTION_LOG.CLIENT_ID 	AND AL.APP_VERSION_ID = MNT_TRANSACTION_LOG.APP_VERSION_ID LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL 	ON PCL.POLICY_ID = MNT_TRANSACTION_LOG.POLICY_ID AND PCL.CUSTOMER_ID = MNT_TRANSACTION_LOG.CLIENT_ID 	AND PCL.POLICY_VERSION_ID = MNT_TRANSACTION_LOG.POLICY_VER_TRACKING_ID ,MNT_USER_LIST";				
					objWebGrid.FromClause = "MNT_TRANSACTION_LOG (NOLOCK) LEFT OUTER JOIN CLT_CUSTOMER_LIST (NOLOCK) ON MNT_TRANSACTION_LOG.CLIENT_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID LEFT JOIN APP_LIST AL (NOLOCK) ON AL.APP_ID = MNT_TRANSACTION_LOG.APP_ID AND AL.CUSTOMER_ID = MNT_TRANSACTION_LOG.CLIENT_ID AND AL.APP_VERSION_ID = MNT_TRANSACTION_LOG.APP_VERSION_ID LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL (NOLOCK) ON PCL.POLICY_ID = MNT_TRANSACTION_LOG.POLICY_ID AND PCL.CUSTOMER_ID = MNT_TRANSACTION_LOG.CLIENT_ID AND PCL.POLICY_VERSION_ID = MNT_TRANSACTION_LOG.POLICY_VER_TRACKING_ID LEFT OUTER JOIN MNT_USER_LIST (NOLOCK)	ON MNT_USER_LIST.USER_ID=ISNULL(MNT_TRANSACTION_LOG.RECORDED_BY,0)";
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//Trans ID Lesser Than^Description^Recorded By^Recorded Date^Application #^Policy #";			
					//objWebGrid.SearchColumnNames = "MNT_TRANSACTION_LOG.TRANS_ID^MNT_TRANSACTION_LOG.TRANS_DESC^isnull(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'') ! isnull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'') ! isnull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'')^isnull(MNT_USER_LIST.USER_FNAME,'') ! isnull(MNT_USER_LIST.USER_LNAME,'')^MNT_TRANSACTION_LOG.RECORD_DATE_TIME";
                    //objWebGrid.SearchColumnNames = "MNT_TRANSACTION_LOG.TRANS_ID^MNT_TRANSACTION_LOG.TRANS_DESC^isnull(MNT_USER_LIST.USER_FNAME,'') ! isnull(MNT_USER_LIST.USER_LNAME,'')^MNT_TRANSACTION_LOG.RECORD_DATE_TIME^AL.APP_NUMBER^PCL.POLICY_NUMBER";
                    objWebGrid.SearchColumnNames = "MNT_TRANSACTION_LOG.TRANS_ID^MNT_TRANSACTION_LOG.TRANS_DESC^isnull(MNT_USER_LIST.USER_FNAME,'') ! isnull(MNT_USER_LIST.USER_LNAME,'')^MNT_TRANSACTION_LOG.RECORD_DATE_TIME^PCL.APP_NUMBER^PCL.POLICY_NUMBER";
					//objWebGrid.SearchColumnType = "L^Tx^T^T^D";				
					objWebGrid.SearchColumnType = "LT^Tx^T^D^T^T";				
					//objWebGrid.DisplayColumnNumbers = "1^6^3^4^5";				
					objWebGrid.DisplayColumnNumbers = "1^5^3^4^6^7";				
					//objWebGrid.DisplayColumnNames = "TRANS_ID^Description^CustomerName^RecordedBy^RecordedDate";
					objWebGrid.DisplayColumnNames = "TRANS_ID^Description^RecordedBy^RecordedDate^AppNumber^PolicyNumber";
					//objWebGrid.DisplayColumnHeadings = "Trans ID^Description^Customer Name^Recorded By^Recorded Date";
					objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//Trans ID^Description^Recorded By^Recorded Date^Application #^Policy #";
					//objWebGrid.DisplayTextLength = "30^70^50^50^50";
					objWebGrid.DisplayTextLength = "25^50^30^30^30^30";
					//objWebGrid.DisplayColumnPercent = "8^35^15^20^15";
					objWebGrid.DisplayColumnPercent = "8^25^15^22^15^15";
					objWebGrid.ColumnTypes = "B^B^B^B^B^B";
				}
				else if(strCalledFrom.Equals("MNT"))
				{
                    objWebGrid.SelectClause = "MNT_TRANSACTION_LOG.TRANS_ID,MNT_TRANSACTION_LOG.TRANS_TYPE_ID as TransactionType,RTRIM(isnull(MNT_USER_LIST.USER_FNAME,'')+' '+ isnull(MNT_USER_LIST.USER_LNAME,'')) AS RecordedBy ,ISNULL(Convert(varchar,MNT_TRANSACTION_LOG.RECORD_DATE_TIME,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end)+' '+convert(char(8),MNT_TRANSACTION_LOG.RECORD_DATE_TIME,8),'') as RecordedDate,MNT_TRANSACTION_LOG.TRANS_DESC as Description";
					objWebGrid.FromClause = "MNT_TRANSACTION_LOG (NOLOCK) LEFT OUTER JOIN MNT_USER_LIST (NOLOCK) ON MNT_USER_LIST.USER_ID=ISNULL(MNT_TRANSACTION_LOG.RECORDED_BY,0)";

                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsNonCarr");// Trans ID Lesser Than^Description^Recorded By^Recorded Date			
					objWebGrid.SearchColumnNames = "MNT_TRANSACTION_LOG.TRANS_ID^MNT_TRANSACTION_LOG.TRANS_DESC^isnull(MNT_USER_LIST.USER_FNAME,'') ! isnull(MNT_USER_LIST.USER_LNAME,'')^MNT_TRANSACTION_LOG.RECORD_DATE_TIME";
					objWebGrid.SearchColumnType = "LT^Tx^T^D";				
					
					objWebGrid.DisplayColumnNumbers = "1^5^3^4";				
					objWebGrid.DisplayColumnNames = "TRANS_ID^Description^RecordedBy^RecordedDate";
					objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsNonCarr");//Trans ID^Description^Recorded By^Recorded Date";
					objWebGrid.DisplayTextLength = "25^50^30^30";
					objWebGrid.DisplayColumnPercent = "15^25^15^15";
					objWebGrid.ColumnTypes = "B^B^B^B";
				}
				objWebGrid.WhereClause = this.GetWhereClause(strCalledFrom);
				
				//objWebGrid.OrderByClause = "MNT_TRANSACTION_LOG.TRANS_ID ASC";
				
				objWebGrid.OrderByClause = "TRANS_ID DESC";
				//objWebGrid.SearchColumnHeadings = "Trans ID^Description^Customer Name^Recorded By^Recorded Date";			
				
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "MNT_TRANSACTION_LOG.TRANS_ID";
				//objWebGrid.ColumnTypes = "B^B^B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "2";
				objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//objWebGrid.ExtraButtons = "1^Add New^0";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString =objResourceMgr.GetString("HeaderString");//Transaction Log
				objWebGrid.SelectClass = colors;
				objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//Include";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "TRANS_ID";
				
				//Adding to gridholder
				if (ShowGrid)
				{				
					GridHolder.Controls.Add(objWebGrid);
				}				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			#endregion
		}
		/// <summary>
		/// Sets the where calsuse to be used for showing data on grid
		/// </summary>
		/// <param name="strCalledFrom">CalledFrom Querys string value</param>
		/// <returns>string of where caluse</returns>
		private string GetWhereClause(string strCalledFrom)
		{
			//System.Text.StringBuilder strWhere =  new System.Text.StringBuilder("MNT_TRANSACTION_LOG.RECORDED_BY = MNT_USER_LIST.[USER_ID]");
			System.Text.StringBuilder strWhere =  new System.Text.StringBuilder("1=1");
			try
			{
				switch(strCalledFrom)
				{
					case "APP":
						//Showing the transaction of selected application only, which is is session
						strWhere.Append(" AND CLIENT_ID = " + GetCustomerID());
						strWhere.Append(" AND APP_ID = " + GetAppID());
						strWhere.Append(" AND APP_VERSION_ID = " + GetAppVersionID());
						break;

					case "CLT":
						//Showing the transaction of selected client only, which is is session
						strWhere.Append(" AND CLIENT_ID = " + GetCustomerID());
						break;
					case "InCLT":
						//Showing the transaction of selected client only, which is is session
						strWhere.Append(" AND CLIENT_ID = " + custID);
						break;
					case "MNT":
						strWhere.Append(" AND CLIENT_ID=0 AND POLICY_ID=0 AND POLICY_VER_TRACKING_ID=0 AND APP_ID=0 AND QUOTE_ID=0 AND QUOTE_VERSION_ID=0 AND APP_VERSION_ID=0");
						break;
				
				}
                if (Request.QueryString["CALLEDFOR"] != null && Request.QueryString["CALLEDFOR"].ToString() == "WORKFLOW")
                {
                    string stringwhere = "";
                    stringwhere = this.GetpolicyDetails(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()));
                    if (stringwhere != "")
                        strWhere.Append(stringwhere);
                }
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
			return strWhere.ToString();
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

		private bool CanShow()
		{
			//Checking whether customer id exits in database or not
			if (strCustomerID == "")
			{
				return false;
			}

			//Checking whether customer id exits in database or not
			if ((!strCalledFrom.Trim().ToUpper().Equals("CLT")) && (!strCalledFrom.Trim().ToUpper().Equals("INCLT")) && (!strCalledFrom.Trim().ToUpper().Equals("MNT")))
			{
				if (strAppId.Equals("") || strAppVersionId.Equals(""))
				{
					return false;
				}
			}
			return true;
		}
        private string GetpolicyDetails(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID)
        {
            ClsGeneralInformation objGenralInfo = new ClsGeneralInformation();
            DataSet ds = new DataSet();
            string POLICY_STATUS = "";
            string WhereClause="";
            ds = objGenralInfo.GetPolicyDetails(CUSTOMER_ID, 0, 0, POLICY_ID, POLICY_VERSION_ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["POLICY_NUMBER"] != null && ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString() != "")
                    hidPOLICY_NO.Value = ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
                else
                    hidPOLICY_NO.Value = ds.Tables[0].Rows[0]["APP_NUMBER"].ToString();

                POLICY_STATUS = ds.Tables[0].Rows[0]["APP_STATUS"].ToString();

                if(POLICY_STATUS.Trim().ToUpper() == "APPLICATION")
                    WhereClause = " AND PCL.APP_NUMBER = '" + hidPOLICY_NO.Value + "'";
                else
                    WhereClause = " AND PCL.POLICY_NUMBER = '" + hidPOLICY_NO.Value + "'";
            }

            return WhereClause;

        }
	}
}
