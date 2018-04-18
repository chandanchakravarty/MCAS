/******************************************************************************************
 Modification History

 <Modified Date			: 02/03/2005- >
 <Modified By			: Gaurav Tyagi- >
 <Purpose				: Implement Css styles at run time using GetColorScheme() function- >
	
 <Modified Date			: 03/03/2005- >
 <Modified By			: Gaurav Tyagi- >
 <Purpose				: Implement Css styles at run time and implement Alternating row color in window grid- >
    
 <Modified Date			: - >28/03/2005
 <Modified By			: - >Anurag Verma
 <Purpose				: - >Set a property to implement toggle image on header.
    
 <Modified Date			: - >21/04/2005
 <Modified By			: - >Anurag Verma
 <Purpose				: - >Making changes according for implementing Model class 
    
 <Modified Date			: - > 23/06/2005
 <Modified By			: - > Anurag Verma
 <Purpose				: - > Making changes for incorporating other users diary entry
	
 <Modified Date			: - > 29/06/2005
 <Modified By			: - > Anshuman
 <Purpose				: - > Making changes for incorporating customer assistant todo list
    
 <Modified Date			: - > 4/07/2005
 <Modified By			: - > Anurag Verma
 <Purpose				: - > Making changes in  SearchColumnType property which has L for dropdown search criteria

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
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using Cms.Model.Diary;//Added for Itrack Issue 6548 on 22 Oct 09
using System.Reflection;

namespace Cms.CmsWeb.Diary
{	
    
    public class index : Cms.CmsWeb.cmsbase 
    {
	 
        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Table tblReport;
        protected System.Web.UI.WebControls.Panel pnlReport;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.DiaryCalendar userCalendar;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidKeyValues;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;		// Stores the RGB value for grid Base
        Control c1;
        protected System.Web.UI.WebControls.Label lblTopLabel;
        protected System.Web.UI.WebControls.DropDownList cmbAllDiary;
		protected Cms.CmsWeb.WebControls.Menu bottomMenu;
        string colors;
        public ClsCommon objCommon;
        protected string userID="";
        protected string user_ID="";
		protected string customer_ID="";
		public string strCalledFrom="";
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDelString;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLISTID;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidErrMsg;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSearchText;
		public string strCssClass="";

        ResourceManager objResourceMgr = null;
        private void Page_Load(object sender, System.EventArgs e)
        {
            
            if (hidDelString.Value != "")
            {
                Delete();
            }
            if (Request["LISTID"] != null && Request["LISTID"].ToString() != "")
            {
                hidLISTID.Value = Request["LISTID"].ToString();
            }

            if (Request["CLAIM_NUMBER"] != null && Request["CLAIM_NUMBER"].ToString() != "")
            {
                hidSearchText.Value = Request["CLAIM_NUMBER"].ToString();
            }
			if(Request.QueryString ["CalledFrom"] !=null)
			{
				strCalledFrom =Request.Params["CalledFrom"].ToString();
				if(strCalledFrom.ToUpper()=="INCLT")
				{
					base.ScreenId ="120_5";
					strCssClass="tableWidthHeader";
					bottomMenu.Visible = false;

					//Setting the cookie values to be used by customer manager index
					SetCookieValue();
				}
				else
				{
					base.ScreenId ="1";
					strCssClass="tableWidth";
					bottomMenu.Visible = true;
				}
			}
			else
			{
				base.ScreenId ="1";
				strCssClass="tableWidth";
				bottomMenu.Visible = true;
			}
			
            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme=GetColorScheme();
            colors="";
           objResourceMgr = new ResourceManager("Cms.CmsWeb.Diary.index", Assembly.GetExecutingAssembly());
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

            lblTopLabel.Text = objResourceMgr.GetString("lblTopLabel"); 
            TabCtl.TabTitles = objResourceMgr.GetString("TabCtl"); 

            if(colors!="")
            {
                string [] baseColor=colors.Split(new char []{','});  
                if(baseColor.Length>0)
                    colors= "#" + baseColor[0];
            }
            #endregion         
        
			if(!Page.IsPostBack)
			{
				objCommon=new ClsCommon();
				//cmbAllDiary.DataSource = objCommon.ExecuteGridQuery("user",Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyIDFromCode());
				cmbAllDiary.DataSource = objCommon.ExecuteGridQuery("user",GetSystemId());
                cmbAllDiary.DataTextField= "name";
				cmbAllDiary.DataValueField="user_id";
				cmbAllDiary.DataBind();
                cmbAllDiary.Items.Insert(0, objResourceMgr.GetString("lblAllEntries"));//"All Diary Entries");   
				cmbAllDiary.Items[0].Value="-1";
				ListItem li;
				if(Request["TOUSERID"]!=null && Request["TOUSERID"].ToString()!="")
					li=cmbAllDiary.Items.FindByValue(Request["TOUSERID"].ToString());				
				else
					li=cmbAllDiary.Items.FindByValue(GetUserId());
				if(li!=null)
					li.Selected=true;
			}
			user_ID=cmbAllDiary.SelectedItem.Value;
			if(user_ID != "-1")
				userID	=	" and t.Touserid=" + user_ID;
			else
				userID	=	"";

            if(Request.Params["CUSTOMER_ID"]!=null && Request.Params["CUSTOMER_ID"].ToString().Length>0)
			{
				customer_ID	= Request.Params["CUSTOMER_ID"];
				hidCUSTOMER_ID.Value = customer_ID;
				userID	+=	" and t.CUSTOMER_ID = " + Request.Params["CUSTOMER_ID"];
			}
			
			c1 = LoadControl("../webcontrols/BaseDataGrid.ascx");

			userCalendar.ColorScheme	=	GetColorScheme();
			userCalendar.UserId			=	user_ID;
			userCalendar.CustomerId		=	customer_ID;
			BindGrid();
           
		}
		
		private void SetCookieValue ()
		{
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strSystemID = GetSystemId();
			if(strCarrierSystemID.ToUpper()!=strSystemID.ToUpper())
				Response.Cookies["LastVisitedTab"].Value = "4";
			else
                Response.Cookies["LastVisitedTab"].Value = "4"; //Changed from 5 for Policy Page Implementation
			
			Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30,0,0,0,0));
		}
		#region get security
		/*private string getScreenSecurity()
		{
			string gstrSecurityXML		=	ClsSecurity.GetSecurityXML(int.Parse(GetUserId()),int.Parse(GetUserTypeId()),base.ScreenId);

			int read=0, write=0, delete=0, execute=0;
			if(gstrSecurityXML	!= "")
			{
				StringReader sRead=new StringReader(gstrSecurityXML);
				XmlTextReader xRead=new XmlTextReader(sRead);
				XmlDocument xmldoc = new XmlDocument();
				xmldoc.Load(xRead);
				foreach(XmlNode node in xmldoc.ChildNodes)
				{
					if(node.SelectSingleNode("Read").InnerText == "Y")
					{
						read =1;
						//return "Read";
						//IsRead=xRead.ReadString();							
					}
					if(node.SelectSingleNode("Write").InnerText == "Y")
					{
						write = 1;
						//return "Write";
						//IsWrite=xRead.ReadString();							
					}
					if(node.SelectSingleNode("Delete").InnerText == "Y")
					{
						delete =1;
						//return "Delete";
						//IsDelete=xRead.ReadString();							
					}
					if(node.SelectSingleNode("Execute").InnerText == "Y")
					{
						execute = 1;
						//return "Execute";
						//IsExecute=xRead.ReadString();							
					}
				} 
				if(execute == 1 ||(delete==1 && write==1))
					return "Execute";
				else if(delete!=1 && write==1)
					return "Write";
				else if(delete==1 && write!=1)
					return "Delete";
				else 
					return "Read";

			}
			return "";
		}*/
		#endregion
        private void BindGrid()
        {
            if ( GridHolder.Controls.Count > 0 )
            {
                GridHolder.Controls.Clear();
            }

            try
            {
                /*************************************************************************/
                ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
                /************************************************************************/
                //specifying webservice URL
                ((BaseDataGrid)c1).WebServiceURL="../webservices/BaseDataGridWS.asmx?WSDL";
                
				if(customer_ID != "")
				{
					//specifying columns for select query
                    ((BaseDataGrid)c1).SelectClause = "T.CUSTOMER_ID, ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'')+ ' ' + ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME, T.LISTID, T.TOUSERID, T.FROMUSERID,ISNULL(Convert(varchar,T.RECDATE,case when " + ClsCommon.BL_LANG_ID + "=3 then 103 else 101 end),'')AS RECDATE, ISNULL(Convert(varchar,T.FOLLOWUPDATE,case when " + ClsCommon.BL_LANG_ID + "=3 then 103 else 101 end),'')AS FOLLOWUPDATE, T.LISTTYPEID,T.SUBJECTLINE, T.NOTE, T.SYSTEMFOLLOWUPID, T.PRIORITY, datepart(hh,T.STARTTIME) as STARTTIMEHOUR, datepart(mi,T.STARTTIME) as STARTTIMEMINUTE, datepart(hh,T.ENDTIME) as ENDTIMEHOUR, datepart(mi,T.ENDTIME) as ENDTIMEMINUTE,  UL.USER_FNAME+' '+ UL.USER_LNAME  as FROMUSERNAME,T.LISTOPEN LISTOPEN,ISNULL (mntt.TYPEDESC , TT.TYPEDESC) as TYPEDESC,ISNULL(PL.APP_NUMBER,'') as  APP_NUMBER,PL.POLICY_NUMBER,MMT.MM_MODULE_NAME,T.MODULE_ID,QQ.QQ_NUMBER,CCI.CLAIM_NUMBER";
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause= " TODOLIST T left  JOIN CLT_QUICKQUOTE_LIST QQ ON QQ.QQ_ID = T.QUOTEID and T.CUSTOMER_ID = QQ.CUSTOMER_ID AND QQ.APP_ID = T.APP_ID AND QQ.APP_VERSION_ID = T.APP_VERSION_ID "
												 + " LEFT OUTER JOIN CLT_CUSTOMER_LIST CL ON T.CUSTOMER_ID = CL.CUSTOMER_ID LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PL ON T.CUSTOMER_ID = PL.CUSTOMER_ID AND T.POLICY_ID = PL.POLICY_ID AND T.POLICY_VERSION_ID = PL.POLICY_VERSION_ID LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON ISNULL(CCI.CLAIM_ID,0) = ISNULL(T.CLAIMID,0)"
                                                 + " LEFT OUTER JOIN MNT_MODULE_MASTER MMT ON T.MODULE_ID=MMT.MM_MODULE_ID, MNT_USER_LIST UL, TODOLISTTYPES TT LEFT OUTER JOIN TODOLISTTYPES_MULTILINGUAL mntt on TT.TYPEID =mntt.TYPEID and mntt.LANG_ID=" + GetLanguageID();
					//specifying Text to be shown in combo box					
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Application Number^FollowUp Date^Note^Policy Number^Claim Number^QQ Number^Record Date^Subject Line^Type";
					//specifying column to be used for combo box
					//((BaseDataGrid)c1).SearchColumnNames="T.RECDATE^T.FOLLOWUPDATE^T.SUBJECTLINE^T.NOTE^T.LISTTYPEID^AP.APP_NUMBER^PL.POLICY_NUMBER";
					//((BaseDataGrid)c1).SearchColumnNames="T.RECDATE^T.FOLLOWUPDATE^AP.APP_NUMBER^PL.POLICY_NUMBER^QQ.QQ_NUMBER^T.SUBJECTLINE^T.NOTE^T.LISTTYPEID^T.MODULE_ID";
					((BaseDataGrid)c1).SearchColumnNames="isnull(PL.APP_NUMBER,'')^T.FOLLOWUPDATE^T.NOTE^PL.POLICY_NUMBER^CCI.CLAIM_NUMBER^T.RECDATE^T.SUBJECTLINE^T.LISTTYPEID";
					//search column data type specifying data type of the column to be used for combo box
					//((BaseDataGrid)c1).SearchColumnType="D^D^T^T^T^T^T";
					//((BaseDataGrid)c1).SearchColumnType="D^D^T^T^T^T^T^T^D";
					// ((BaseDataGrid)c1).SearchColumnType="T^D^D^T^T^T^D^T^T";
					((BaseDataGrid)c1).SearchColumnType="T^D^T^T^T^D^T^T";
					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="LISTID^TOUSERID^MODULE_ID";
		
					//specifying column names from the query
					//((BaseDataGrid)c1).DisplayColumnNumbers="3^4^7^8^17";
					//((BaseDataGrid)c1).DisplayColumnNumbers="21^3^4^17^19^20^7^8^22";
					((BaseDataGrid)c1).DisplayColumnNumbers="4^3^7^17^19^21^20^8";
					//specifying column names from the query
					//((BaseDataGrid)c1).DisplayColumnNames="RECDATE^FOLLOWUPDATE^SUBJECTLINE^NOTE^TYPEDESC";
					((BaseDataGrid)c1).DisplayColumnNames="RECDATE^FOLLOWUPDATE^TYPEDESC^APP_NUMBER^POLICY_NUMBER^CLAIM_NUMBER^SUBJECTLINE^NOTE";
					//specifying text to be shown as column headings					
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Record Date^Followup Date^Type^App #^Policy #^Claim #^QQ #^Subject Line^Note";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="5^5^10^10^10^10^10^7";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="3^10^10^10^10^10^10^7";
					//((BaseDataGrid)c1).DisplayColumnPercent="5^5^10^10^10^10^10^10^30";
					//((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B^B";
					((BaseDataGrid)c1).FetchColumns="2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17";
					
					
					/*******************************************************************************/
					//((BaseDataGrid)c1).DropDownColumns="^^^^TYPE^^";
					//((BaseDataGrid)c1).DropDownColumns="^^MODULE^^^^^^TYPE";
					((BaseDataGrid)c1).DropDownColumns="^^^^^^^^TYPE";
					TabCtl.TabURLs = @"AddDiaryEntry.aspx?CalledFrom=" + strCalledFrom 
						+ "&CUSTOMER_ID=" + customer_ID.Trim()
						+ "&";
				}
				else
				{
					//specifying columns for select query
                    ((BaseDataGrid)c1).SelectClause = " T.CUSTOMER_ID, T.LISTID, T.TOUSERID, T.FROMUSERID, ISNULL(Convert(varchar,T.RECDATE,case when " + ClsCommon.BL_LANG_ID + "=3 then 103 else 101 end),'')AS RECDATE, ISNULL(Convert(varchar,T.FOLLOWUPDATE,case when " + ClsCommon.BL_LANG_ID + "=3 then 103 else 101 end),'')AS FOLLOWUPDATE, T.LISTTYPEID,T.SUBJECTLINE, T.NOTE, T.SYSTEMFOLLOWUPID, T.PRIORITY, datepart(hh,T.STARTTIME) as STARTTIMEHOUR, datepart(mi,T.STARTTIME) as STARTTIMEMINUTE, datepart(hh,T.ENDTIME) as ENDTIMEHOUR, datepart(mi,T.ENDTIME) as ENDTIMEMINUTE,  UL.USER_FNAME+' '+ UL.USER_LNAME  as FROMUSERNAME,T.LISTOPEN LISTOPEN,isnull(	mntt.TYPEDESC,TT.TYPEDESC  )as TYPEDESC,RTRIM(RTRIM(ISNULL(CL.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CL.CUSTOMER_MIDDLE_NAME,'')) + ' ' + ISNULL(CL.CUSTOMER_LAST_NAME,'')) as CUSTOMER_NAME,ISNULL(PL.APP_NUMBER,'')as APP_NUMBER,PL.POLICY_NUMBER,T.CUSTOMER_ID,MMT.MM_MODULE_NAME,T.MODULE_ID,QQ.QQ_NUMBER QQ_NUMBER,CCI.CLAIM_NUMBER";
					//specifying tables for from clause
                    ((BaseDataGrid)c1).FromClause = " TODOLIST T	left  JOIN CLT_QUICKQUOTE_LIST QQ ON QQ.QQ_ID = T.QUOTEID and T.CUSTOMER_ID = QQ.CUSTOMER_ID AND QQ.APP_ID = T.APP_ID AND QQ.APP_VERSION_ID = T.APP_VERSION_ID left outer join CLT_CUSTOMER_LIST CL on T.CUSTOMER_ID = CL.CUSTOMER_ID left	outer join POL_CUSTOMER_POLICY_LIST PL on T.CUSTOMER_ID = PL.CUSTOMER_ID AND T.POLICY_ID = PL.POLICY_ID AND T.POLICY_VERSION_ID = PL.POLICY_VERSION_ID LEFT OUTER JOIN CLM_CLAIM_INFO CCI ON ISNULL(CCI.CLAIM_ID,0) = ISNULL(T.CLAIMID,0) LEFT OUTER JOIN MNT_MODULE_MASTER MMT ON T.MODULE_ID=MMT.MM_MODULE_ID, MNT_USER_LIST UL, TODOLISTTYPES TT left outer join TODOLISTTYPES_MULTILINGUAL mntt on TT.TYPEID=mntt.TYPEID and mntt.LANG_ID = " + GetLanguageID();
					//specifying Text to be shown in combo box					
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsNonCarr");	 //"Application Number^Customer Name^FollowUp Date^Note^Policy Number^Claim Number^QQ Number^Record Date^Subject Line^Type";				//specifying column to be used for combo box
					//((BaseDataGrid)c1).SearchColumnNames="T.RECDATE^T.FOLLOWUPDATE^T.SUBJECTLINE^T.NOTE^T.LISTTYPEID^RTRIM(RTRIM(CL.CUSTOMER_FIRST_NAME ! ' ' ! CL.CUSTOMER_MIDDLE_NAME) ! CL.CUSTOMER_LAST_NAME)^AP.APP_NUMBER^PL.POLICY_NUMBER";
					//((BaseDataGrid)c1).SearchColumnNames="T.RECDATE^T.FOLLOWUPDATE^RTRIM(RTRIM(CL.CUSTOMER_FIRST_NAME ! ' ' ! CL.CUSTOMER_MIDDLE_NAME) ! ' ' ! CL.CUSTOMER_LAST_NAME)^AP.APP_NUMBER^PL.POLICY_NUMBER^T.SUBJECTLINE^T.NOTE^T.LISTTYPEID^T.MODULE_ID^QQ.QQ_NUMBER";
					//((BaseDataGrid)c1).SearchColumnNames="T.RECDATE	^T.FOLLOWUPDATE	^RTRIM(RTRIM(CL.CUSTOMER_FIRST_NAME ! ' ' ! CL.CUSTOMER_MIDDLE_NAME) ! ' ' ! CL.CUSTOMER_LAST_NAME)	^AP.APP_NUMBER^PL.POLICY_NUMBER^T.SUBJECTLINE^T.NOTE^T.LISTTYPEID^T.MODULE_ID^QQ.QQ_NUMBER";
					((BaseDataGrid)c1).SearchColumnNames="isnull(PL.APP_NUMBER,'')^(isnull(CL.CUSTOMER_FIRST_NAME,'') ! isnull(CL.CUSTOMER_MIDDLE_NAME,'') ! isnull(CL.CUSTOMER_LAST_NAME,''))^T.FOLLOWUPDATE^T.NOTE^PL.POLICY_NUMBER^CCI.CLAIM_NUMBER^T.RECDATE^T.SUBJECTLINE^T.LISTTYPEID";
					//search column data type specifying data type of the column to be used for combo box
					//((BaseDataGrid)c1).SearchColumnType="D^D^T^T^L^T^T^T";
					//((BaseDataGrid)c1).SearchColumnType="D^D^T^T^T^T^T^L^L^T";
					((BaseDataGrid)c1).SearchColumnType="T^T^D^T^T^T^D^T^L";
					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="LISTID^TOUSERID^MODULE_ID^QQ_NUMBER";
                    //specifying column numbers of the query to be displyed in grid
					//((BaseDataGrid)c1).DisplayColumnNumbers="3^4^7^8^17";
					((BaseDataGrid)c1).DisplayColumnNumbers="3^4^17^18^19^23^20^7^8";
					//specifying column names from the query
					//((BaseDataGrid)c1).DisplayColumnNames="RECDATE^FOLLOWUPDATE^SUBJECTLINE^NOTE^TYPEDESC";
					((BaseDataGrid)c1).DisplayColumnNames="RECDATE^FOLLOWUPDATE^TYPEDESC^CUSTOMER_NAME^APP_NUMBER^POLICY_NUMBER^CLAIM_NUMBER^SUBJECTLINE^NOTE";
					//specifying text to be shown as column headings					
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsNonCarr");//"Record Date^Followup Date^Type^Customer Name^App #^QQ Number^Policy #^Claim #^Subject Line^Note";
					//specifying column heading display text length
					//((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^75";
					//((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50^50^50^75";
					((BaseDataGrid)c1).DisplayTextLength="5^5^10^10^10^10^15^15^15";
					//specifying width percentage for columns
					//((BaseDataGrid)c1).DisplayColumnPercent="15^15^23^24^23";
					//((BaseDataGrid)c1).DisplayColumnPercent="10^10^15^15^15^15^10^10";
					((BaseDataGrid)c1).DisplayColumnPercent="5^5^10^10^10^10^15^15^15";
					//((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B^B^B";
					//((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16";
					((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^22^23^24";
					/*******************************************************************************/
					((BaseDataGrid)c1).DropDownColumns="^^^^^^^^^TYPE";
					TabCtl.TabURLs = @"AddDiaryEntry.aspx?";
				}
                /*******************************************************************************/

                if (GetSystemId().ToUpper() == CarrierSystemID.ToUpper())//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToUpper())
				{
					//If loged in agency is wolverine (whichever in web.config) , showing all diary entries

					//specifying conditions for where clause
					((BaseDataGrid)c1).WhereClause=" T.TOUSERID = UL.USER_ID and T.LISTTYPEID=TT.TYPEID " + userID;
				}
				else
				{
					//If loged in agency is not wolverine, showing diary entries of only loged in agency
					//specifying conditions for where clause
					((BaseDataGrid)c1).WhereClause=" T.TOUSERID = UL.USER_ID and T.LISTTYPEID=TT.TYPEID " + userID + " and UL.USER_SYSTEM_ID='" + GetSystemId() + "'";
				}

				//specifying column for order by clause
                ((BaseDataGrid)c1).OrderByClause="RECDATE asc";                
                //specifying primary column number
                ((BaseDataGrid)c1).PrimaryColumns="1";
                //specifying primary column name
                ((BaseDataGrid)c1).PrimaryColumnsName="LISTID";
                //specifying column type of the data grid                
                //specifying links pages 
                //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
                //specifying if double click is allowed or not
                ((BaseDataGrid)c1).AllowDBLClick="false"; 
                //specifying which columns are to be displayed on first tab
                
                //specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//specifying number of the rows to be shown
                ((BaseDataGrid)c1).PageSize=int.Parse(GetPageSize());
                //specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
                //specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                //specifying heading
                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Diary Information";
                ((BaseDataGrid)c1).SelectClass = colors;
                //specifying text to be shown for filter checkbox
                ((BaseDataGrid)c1).FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Complete"; 
                //specifying column to be used for filtering
                ((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
                //value of filtering record
                ((BaseDataGrid)c1).FilterValue="Y";
                // property indiacating whether query string is required or not
                ((BaseDataGrid)c1).RequireQuery ="Y";
				//specifying buttons to be displayed on grid
//				string security = getScreenSecurity();
//				if(security=="Read")
//					((BaseDataGrid)c1).ExtraButtons = "";
//				else if(security=="Write")
//					((BaseDataGrid)c1).ExtraButtons = "1^Add^0^addRecord";
//				else if(security=="Delete")
//					((BaseDataGrid)c1).ExtraButtons = "1^Delete^0^DeleteRecords";
//				else if(security=="Execute")
                ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"2^Add~Delete^0~1^addRecord~DeleteRecords"; 				
                
                ((BaseDataGrid)c1).DefaultSearch="Y"; 
				((BaseDataGrid)c1).RequireCheckbox ="Y";

              
                // to show completed task we have to give check box
                GridHolder.Controls.Add(c1);
            }
            catch(Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);    
            }
        }
		
        /// <summary>
        /// Delets a diary entry from the database
        /// </summary>
		private void Delete()
		{
			ClsDiary objDiary = new ClsDiary();
			TodolistInfo objDefModelInfo = new TodolistInfo();
			string listID = "",listDesc="";
			int result = 0;
			listID = hidDelString.Value;
			//Added for Itrack Issue 6548 on 22 Oct 09
			DataSet ds=new DataSet();
			ds=objDiary.FetchToDoListData(listID);
			string []listIDInfo = hidDelString.Value.Replace("(","").Replace(")","").Replace("OR",",").Trim().Split(',');
			for(int i=0;i<listIDInfo.Length;i++)
			{
				if (ds.Tables[0].Rows[i]["TYPEDESC"]!= null && ds.Tables[0].Rows[i]["TYPEDESC"].ToString().Trim() != "")
				{
					listDesc = ds.Tables[0].Rows[i]["TYPEDESC"].ToString();
				}
				if (ds.Tables[0].Rows[i]["CUSTOMER_NAME"] != null && ds.Tables[0].Rows[i]["CUSTOMER_NAME"].ToString().Trim() != "")
				{
					objDefModelInfo.CUSTOMER_NAME=ds.Tables[0].Rows[i]["CUSTOMER_NAME"].ToString();
				}
				if (ds.Tables[0].Rows[i]["CUSTOMER_ID"] != null && ds.Tables[0].Rows[i]["CUSTOMER_ID"].ToString().Trim() != "")
				{
					objDefModelInfo.CUSTOMER_ID = int.Parse(ds.Tables[0].Rows[i]["CUSTOMER_ID"].ToString());
				}
				
				if (ds.Tables[0].Rows[i]["APP_ID"] != null && ds.Tables[0].Rows[i]["APP_ID"].ToString().Trim() != "")
				{
					objDefModelInfo.APP_ID = int.Parse(ds.Tables[0].Rows[i]["APP_ID"].ToString());
				}
				if (ds.Tables[0].Rows[i]["APP_VERSION_ID"] != null && ds.Tables[0].Rows[i]["APP_VERSION_ID"].ToString().Trim() != "")
				{
					objDefModelInfo.APP_VERSION_ID = int.Parse(ds.Tables[0].Rows[i]["APP_VERSION_ID"].ToString());
				}
				if (ds.Tables[0].Rows[i]["POLICY_ID"] != null && ds.Tables[0].Rows[i]["POLICY_ID"].ToString().Trim() != "")
				{
					objDefModelInfo.POLICY_ID = int.Parse(ds.Tables[0].Rows[i]["POLICY_ID"].ToString());
				}
				if (ds.Tables[0].Rows[i]["POLICY_VERSION_ID"] != null && ds.Tables[0].Rows[i]["POLICY_VERSION_ID"].ToString().Trim() != "")
				{
					objDefModelInfo.POLICY_VERSION_ID = int.Parse(ds.Tables[0].Rows[i]["POLICY_VERSION_ID"].ToString());
				}
			
				objDefModelInfo.CREATED_BY	 = int.Parse(GetUserId());
				result=objDiary.DeleteDiaryEntry(objDefModelInfo,listIDInfo[i],"todolist",listDesc);
			}
			lblMessage.Visible=true;
            lblMessage.Text = "<br />" + ClsMessages.FetchGeneralMessage("1094");//<br>Records have been deleted successfully ";
			if(result >0)
				this.hidErrMsg.Value= "1";

			//BindGrid();
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
			this.cmbAllDiary.SelectedIndexChanged += new System.EventHandler(this.cmbAllDiary_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
        #endregion

        private void cmbAllDiary_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
        }
    }

 
}
