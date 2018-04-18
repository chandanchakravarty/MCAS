/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 
	<End Date				: - >
	<Description			: - > webcontrol to implement webgrid
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

    
   <Modified Date			: - >28/03/2005
	<Modified By			: - >Anurag Verma
	<Purpose				: - >Set a property to implement toggle image on header.
    
   <Modified Date			: - >30/03/2005
	<Modified By			: - >Anurag Verma
	<Purpose				: - >Incorporating feature for getting diary entry for different users
    
    <Modified Date			: - >08/04/2005
	<Modified By			: - >Anurag Verma
	<Purpose				: - >Changes made in save refresh and refreshgrid for record display in insert and update mode
    	
	<Modified Date			: - >11/04/2005
	<Modified By			: - >Pradeep Iyer
	<Purpose				: - >Added functionality for Look up
    
    <Modified Date			: - >11/04/2005
	<Modified By			: - >Anurag Verma
	<Purpose				: - >New property added for sending search column data type

    <Modified Date			: - > 25/04/2005
    <Modified By			: - > Anurag Verma
	<Purpose				: - > Making changes for query string 
	
    <Modified Date			: - > 8/07/2005
    <Modified By			: - > Anurag Verma
	<Purpose				: - > Making changes for checkbox 

    <Modified Date			: - > 16/11/2005
    <Modified By			: - > Anurag Verma
	<Purpose				: - > Making changes for dropdownlist
	
    <Modified Date			: - > 29/June/2006
    <Modified By			: - > RPSINGH
	<Purpose				: - > Adding a new property RequireFocus. If this propoerty is true
								  then focus the default search box.	
								  
	<Modified Date			: - > 07/August/2006
    <Modified By			: - > Sumit Chhabra
	<Purpose				: - > Added a new property RequireNormalCursor. If this propoerty is set to Y
								  then the cursor will be normal mouse pointer and selected row will have
								  black color, else it will default to
								  hand cursor with selected row having blue color.
								  
    <Modified Date			: - > 09/August/2007
    <Modified By			: - > Praveen Kasana
	<Purpose				: - > Added functionality for Encrypting data.
								  

*******************************************************************************************/ 



namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Text;
    using System.Collections; 
    using Cms.BusinessLayer.BlCommon;  

	//using BritAmazon.BusinessObjects;

	/// <summary>
	///		Summary description for BaseDataGrid.
	/// </summary>
	public  class BaseDataGrid : System.Web.UI.UserControl
	{
		const string SEPARATOR = "~";
        private int iGroupColumns = 1; //no of display columns to be used for grouping
		private string sWebServiceURL; //the path of the WebService
		public string SqlQuery; //to be removed
		private string sSelClause; //SQL query's select clause: mandatory
		private string sFromClause; //SQL query's from clause: mandatory
		private string sWhereClause; //SQL query's where clause: optional
		private string sGroupClause; //SQL query's group by clause: optional
		private string sHavingClause; //SQL query's having clause: optional
		private string sOrderClause; //SQL query's Order by clause: optional

		private string sFilterCol; //the name of the filter column with alias. Filter column shoud be type char, nchar etc. and must contain 'Y' and 'N' as valid values
		private string sFilterLbl; //label to be displayed in the grid
		private bool bShowExcluded = false; //if filter is to be switched off
        

		private string sSearchCols; //all the search column names with respective aliases
		private string sSearchColHeads; //search column heads
		private string sSearchColTypes = ""; //search types

		private string sDispColNos; //nos of the display columns starting with 1
		private string sDispColHeads; //headings to be used for display columns
		private string sDispTextLen;//max. text length for each column data
		private string sDispColLen = "100"; //the ^ separated list of the display column length in %age
		private string sDispColNames = "";
		private string sRequireFocus = "";
		private string sRequireNormalCursor = "";
		private string sPrimaryCols; //nos of the columns to be taken as primary starting with 1
		private bool bMultiSelect = false; //if checkboxes for multiple selection are to be displayed
		private string sSysColName = ""; //the name of the column that defines the system values as 'Y'
		private int iPageSize = 0; //rows to be returned per page
		private int iAdvSearchRows = 3; //no of dropdowns to be shown in the advanced search
		private string sSearchName=""; //Search type Name for NINC
		private bool bSearch=true;
		protected System.Web.UI.WebControls.Literal PublicProps;
		
		//public enum ColumnType {Bound,Button,HyperLink,Template};

		private string sColumnTypes="";
		private string sPrimaryColsName="";
		private string sColumnsLink="";
		private string sDisplayColumnNames="";
		private string sfetchColumns="";
		private string sallowDBLClick="";
		private string sSearchTxt="";
		private string strExtraButtons;				// Stores the value which param ExtraButtons Pass.
		private int icacheSize;
		private string iTotRecords=System.Configuration.ConfigurationSettings.AppSettings["Total_Record"].ToString();
		private string simgPath="";
        private string hImgPath="";
        private string strHeader="";
        private string selectedClass="";
        private string extraFeature="";
        private string filterVal=""; //private variable for storing filter value
        private string sSearchTypes=""; //private variable for storing data type of the column
		
		//Will contain tilde(^) separated values: 
		//LookupIDColName^LookupDescColName^LookupIDControlID^LookupDescControlID
		private string lookUpDetails;
        private string reqQuery="";
        private string queryStringColumns="";
        private string defSearch="";
        private string drpColumns="";
        private string needGrouping="";
        private string groupColumns="";
		private string sCellHorizontalAlign="";

		private string flagCheckbox="";
		private string flagDropdownList="";
		
		private string strRead="",strWrite="",strDelete="",strExecute="";

        
        public ClsCommon objCommon=new ClsCommon();

        protected String strFrom;
        protected String strTo;

		public string Read
		{
			get {
				strRead=((cmsbase)Page).IsRead; 
				return strRead;
			}
			
		}

		public string Write
		{
			get {
				strWrite=((cmsbase)Page).IsWrite;
				return strWrite;
			}
			
		}

		public string Delete
		{
			get {
				strDelete=((cmsbase)Page).IsDelete;
				return strDelete;
				}
			
		}

		public string Execute
		{
			get {
				strExecute=((cmsbase)Page).IsExecute;
				return strExecute;
			}
			
		}

		
		public string RequireFocus
		{
			get {return sRequireFocus;}
			set {sRequireFocus = value;}
		}

		public string RequireNormalCursor
		{
			get{return sRequireNormalCursor;}
			set{sRequireNormalCursor = value;}
		}
		
		public string GroupQueryColumns
        {
            get {return groupColumns;}
            set {groupColumns = value;}
        }
        
		public string RequireCheckbox
		{
			get {return flagCheckbox;}
			set {flagCheckbox = value;}
		}

		public string RequireDropdownList
		{
			get {return flagDropdownList;}
			set {flagDropdownList = value;}
		}
        
        
        public string Grouping
        {
            get {return needGrouping;}
            set {needGrouping = value;}
        }

        public string GroupColumnsCount
        {
            get {return drpColumns;}
            set {drpColumns = value;}
        }

        
        public string DropDownColumns
        {
            get {return drpColumns;}
            set {drpColumns = value;}
        }

        public string DefaultSearch
        {
            get {return defSearch;}
            set {defSearch = value;}
        }

        public string QueryStringColumns
        {
            get {return queryStringColumns;}
            set {queryStringColumns = value;}
        }
        
        public string RequireQuery
        {
            get {return reqQuery;}
            set {reqQuery = value;}
        }
        
        public string SearchColumnType
        {
            get {return sSearchTypes;}
            set {sSearchTypes = value;}
        }

        public string FilterValue
        {
            get {return filterVal;}
            set {filterVal = value;}
        }

        public string ExtraUserFeature
        {
            get {return extraFeature;}
            set {extraFeature = value;}
        }
		
        public string HImagePath
        {
            get {return hImgPath;}
            set {hImgPath = value;}
        }
        
        public string SelectClass
        {
            get {return selectedClass;}
            set {selectedClass = value;}
        }
        public string HeaderString
        {
            get {return strHeader;}
            set {strHeader = value;}
        }

		public string ImagePath
		{
			get {return simgPath;}
			set {simgPath = value;}
		}
		
		
		public int CacheSize
		{
			get {return icacheSize;}
			set {icacheSize = value;}
		}
		
		//Added by Mohit Agarwal 16-Sep -08
		public string TotRecords
		{
			get {return iTotRecords;}
			set {iTotRecords = value;}
		}

		public string AllowDBLClick
		{
			get {return sallowDBLClick;}
			set {sallowDBLClick = value;}
		}

		public string ExtraButtons
		{
			get
			{
				return strExtraButtons;
			}
			set
			{
				strExtraButtons = value;
			}
		}


		public string SearchMessage
		{
			get {return sSearchTxt;}
			set {sSearchTxt = value;}
		}
		
		public string FetchColumns
		{
			get {return sfetchColumns;}
			set {sfetchColumns = value;}
		}

		public string WebServiceURL
		{
			get {return sWebServiceURL;}
			set {sWebServiceURL = value;}
		}
		public string SelectClause
		{
			get {return sSelClause;}
			set {sSelClause = value;}
		}
		public string FromClause
		{
			get {return sFromClause;}
			set {sFromClause = value;}
		}
		public string WhereClause
		{
			get {return sWhereClause;}
			set {sWhereClause = value;}
		}
		public string GroupByClause
		{
			get {return sGroupClause;}
			set {sGroupClause = value;}
		}
		public string HavingClause
		{
			get {return sHavingClause;}
			set {sHavingClause = value;}
		}
		public string OrderByClause
		{
			get {return sOrderClause;}
			set {sOrderClause = value;}
		}
		public string FilterColumnName
		{
			get {return sFilterCol;}
			set {sFilterCol = value;}
		}
		public string FilterLabel
		{
			get {return sFilterLbl;}
			set {sFilterLbl = value;}
		}
		public bool ShowExcluded
		{
			get {return bShowExcluded;}
			set {bShowExcluded = value;}
		}
		public string SearchColumnNames
		{
			get {return sSearchCols;}
			set {sSearchCols = value;}
		}
		public string SearchColumnHeadings
		{
			get {return sSearchColHeads;}
			set {sSearchColHeads = value;}
		}
		public string SearchColumnTypes
		{
			get {return sSearchColTypes;}
			set {sSearchColTypes = value;}
		}
		public string DisplayColumnNumbers
		{
			get {return sDispColNos;}
			set {sDispColNos = value;}
		}
		//		public string DisplayColumnNames
		//		{
		//			get {return sDispColNames;}
		//			set {sDispColNames = value;}
		//		}
		public string DisplayColumnHeadings
		{
			get {return sDispColHeads;}
			set {sDispColHeads = value;}
		}
		public string DisplayTextLength
		{
			get {return sDispTextLen;}
			set {sDispTextLen = value;}
		}
		public string DisplayColumnPercent
		{
			get {return sDispColLen;}
			set {sDispColLen = value;}
		}
		public string PrimaryColumns
		{
			get {return sPrimaryCols;}
			set {sPrimaryCols = value;}
		}
		public bool MultiSelect
		{
			get {return bMultiSelect;}
			set {bMultiSelect = value;}
		}
		public string SystemColumnName
		{
			get {return sSysColName;}
			set {sSysColName = value;}
		}
		public int PageSize
		{
			get {return iPageSize;}
			set {iPageSize = value;}
		}
		public int AdvanceSearchRows
		{
			get {return iAdvSearchRows;}
			set {iAdvSearchRows = value;}
		}
		public string SearchName
		{
			get {return sSearchName;}
			set {sSearchName = value;}
		}
		public string ColumnTypes
		{
			get {return sColumnTypes;}
			set {sColumnTypes = value;}
		}

		public string PrimaryColumnsName
		{
			get {return sPrimaryColsName;}
			set {sPrimaryColsName = value;}
		}
		
		public string ColumnsLink
		{
			get {return sColumnsLink;}
			set {sColumnsLink = value;}
		}
		public string DisplayColumnNames
		{
			get {return sDisplayColumnNames;}
			set {sDisplayColumnNames = value;}
		}
		
		public bool Search
		{
			get {return bSearch;}
			set {bSearch=value;}
		}

		public string LookupDetails
		{
			get { return lookUpDetails; }
			set { lookUpDetails = value; }
		}
		public string CellHorizontalAlign
		{
			get{return sCellHorizontalAlign;}
			set{sCellHorizontalAlign = value;}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				Ajax.Utility.RegisterTypeForAjax(typeof(BaseDataGrid)); 

				if(sDispColNames == "") sDispColNames = sDispColNos;
				StringBuilder tmpJS = new StringBuilder("<script type=\"text/javascript\" language=\"JavaScript\">\n<!--\n");
				tmpJS.Append("var sSrvURL = \"" + sWebServiceURL + "\";\n"); //url of the web service
				tmpJS.Append("var sQg = \"" + EncryptData(sSelClause + SEPARATOR + sFromClause + SEPARATOR + sWhereClause + SEPARATOR + sGroupClause + SEPARATOR + sHavingClause + SEPARATOR) + "\";\n"); //query group
				//sOrderClause assign null for claim pages.
				if(sOrderClause == null)
					sOrderClause = "";
                
				tmpJS.Append("var sOc = \"" + EncryptData(sOrderClause) + "\";\n"); //order clause

				if (sFilterCol != null && sFilterCol.Trim() != "")
					tmpJS.Append("var sFg = \"" + sFilterCol + SEPARATOR + sFilterLbl + "\";\n"); //filter group
				else
					tmpJS.Append("var sFg = \"\";\n"); //filter group

				tmpJS.Append("var bSx = " + bShowExcluded.ToString().ToLower() + ";\n"); //show excluded
                tmpJS.Append("var bFValue = '" + filterVal + "';\n"); //show excluded

				tmpJS.Append("var sSg = \"" + sSearchCols + SEPARATOR + sSearchColHeads + "\";\n"); //search group
				//tmpJS.Append("var sDg = \"" + sDispColNos + SEPARATOR + sDispColHeads + SEPARATOR + sDispTextLen + SEPARATOR + sDispColLen + SEPARATOR + sDispColNames + "\";\n"); //display column group
				//tmpJS.Append("var sDg = \"" + sDispColHeads + SEPARATOR + sDispTextLen + SEPARATOR + sDispColLen + SEPARATOR + sDisplayColumnNames + "\";\n"); //display column group
				tmpJS.Append("var sDg = \"" + EncryptData(sDispColHeads + SEPARATOR + sDispColLen + SEPARATOR + sDisplayColumnNames + SEPARATOR + sDispColNos + SEPARATOR + iGroupColumns) + "\";\n"); //display column group
				tmpJS.Append("var msDg = \"" + sDispColNos + SEPARATOR + sDispColHeads + SEPARATOR + sDispTextLen + SEPARATOR + sDispColLen + SEPARATOR + sDispColNames + "\";\n"); //display column group
				if (sPrimaryCols==null) sPrimaryCols="";
				tmpJS.Append("var sPc = \"" + EncryptData(sPrimaryCols) + "\";\n"); // primary columns
				tmpJS.Append("var bMs = " + bMultiSelect.ToString().ToLower() + ";\n"); //multi select;
				tmpJS.Append("var sSys = \"" + sSysColName + "\";\n"); // system column name
				tmpJS.Append("var iPs = " + iPageSize + ";\n"); //page size
				tmpJS.Append("var iRn = " + iAdvSearchRows + ";\n"); //no of advance search dropdowns
				tmpJS.Append("var sDSS = \"" + sSearchName +"\";\n"); //default search name
				tmpJS.Append("var sColType = \"" + sColumnTypes +"\";\n"); //ColumnTypes
				tmpJS.Append("var sPColsName = \"" + sPrimaryColsName +"\";\n"); //Primary Columns Name
				tmpJS.Append("var sColsLink = \"" + sColumnsLink +"\";\n"); //Columns link
				tmpJS.Append("var sDisplayColsName = \"" + sDisplayColumnNames +"\";\n"); //Columns link
				tmpJS.Append("var sallowDBLClick=\""  + sallowDBLClick + "\";\n"); //whether double click is allowed
				tmpJS.Append("var sSearchMessage=\""  + sSearchTxt + "\";\n"); ///to customize messages
				tmpJS.Append("var ExtraButtons=\""  + strExtraButtons + "\";\n"); //default search name
				tmpJS.Append("var fetchColumns=\""  + sfetchColumns + "\";\n"); //columns to be fetched
				tmpJS.Append("var ImagePath=\""  + simgPath + "\";\n"); //image url to be fetched	
                tmpJS.Append("var HImagePath=\""  + hImgPath + "\";\n"); //image url to be fetched	
                tmpJS.Append("var HeaderString=\""  + strHeader + "\";\n"); //image url to be fetched				
				tmpJS.Append("var dispTextLen=\""  + sDispTextLen + "\";\n"); //image url to be fetched
				tmpJS.Append("var colHeader=\""  + createHeaderString(maximumLength(sDispColHeads)) + "\";\n"); //image url to be fetched
				tmpJS.Append("var sCacheSize=\""  + icacheSize.ToString()   + "\";\n"); //columns to be fetched				
				tmpJS.Append("var sSearch = \"" + bSearch.ToString().ToLower() +"\";\n"); //Columns link
                tmpJS.Append("var selColor= \"" + selectedClass +"\";\n"); //Selected Row Class
                tmpJS.Append("var extraUserFeature= \"" + extraFeature +"\";\n"); //Change for incorporating combo box
                tmpJS.Append("var lookUpDetails= \"" + lookUpDetails +"\";\n"); //Change for incorporating Lookup details
                tmpJS.Append("var searchType= \"" + sSearchTypes +"\";\n"); //Change for incorporating Lookup details
                tmpJS.Append("var strRequire= \"" + reqQuery +"\";\n"); //bollean whether query string is required or XML
                tmpJS.Append("var QueryColumns= \"" + queryStringColumns +"\";\n"); //column numbers for creating query string
                tmpJS.Append("var DefaultSearch= \"" + defSearch +"\";\n"); //boolean value for default search
                tmpJS.Append("var DropdownCols= \"" + drpColumns +"\";\n"); //column string for dropdown columns
                tmpJS.Append("var Grouping= \"" + needGrouping +"\";\n"); //column string for grouping
                tmpJS.Append("var ColQueryGrouping= \"" + groupColumns +"\";\n"); //column string seperated with ^ which will be binded with hyperlink column
			    tmpJS.Append("var requireCheckbox= \"" + flagCheckbox+"\";\n"); //flag which decides whether we require check box or not
				tmpJS.Append("var requireDropdownList= \"" + flagDropdownList +"\";\n"); //flag which decides whether we require DropdownList or not

				tmpJS.Append("var sCellHorizontalAlign= \"" + sCellHorizontalAlign +"\";\n"); //Horizontal alignment for bound column
				
				//RPSINGH
				tmpJS.Append(" var RequireFocus= \"" + sRequireFocus +"\";\n"); //flag which decides whether we require DropdownList or not
				//Added by Sumit to change the mouse cursor from default hand to normal pointer
				tmpJS.Append(" var requireNormalCursor= \"" + sRequireNormalCursor +"\";\n"); //flag which decides whether we should display cursor as hand or normal pointer

				//tmpJS.Append("var sTotRecords= \"" + System.Configuration.ConfigurationSettings.AppSettings["Total_Record"].ToString() +"\";\n"); //flag to fetch total record
				tmpJS.Append("var sTotRecords= \"" + iTotRecords +"\";\n"); //flag to fetch total record
				//passing security rights
				string sec=((cmsbase)Page).IsRead  + "~" + ((cmsbase)Page).IsWrite + "~" + ((cmsbase)Page).IsDelete + "~" + ((cmsbase)Page).IsExecute;
				tmpJS.Append("var securityrights= \"" + sec + "\";\n"); //flag which decides whether we require check box or not

				
				
                
                			    
				tmpJS.Append(CreateSelectString());
				tmpJS.Append("// -->\n</script>");
			
				PublicProps.Text = tmpJS.ToString(); //write the complete javascript to the client browser
               
                 strFrom  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1196");
                 strTo = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1197");
			}
			catch(Exception lEx)
			{
				throw lEx;
			}
		}
		[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
		public string  GetSortedData(string sQg, string sOc, string sDg, string sPc, string sSg , string sFg, string bMs, string sSys, string bSx, string iPs, string pageNo, string sSearchCriteria, string advSearch, string iRn,string sDSS,string sColType,string sPColsName,string sColsLink,string sDisplayColsName,string sSearch,string sallowDBLClick,string sSearchMessage,string ExtraButtons,string sCacheSize,string ImagePath,string colHeader,string dispTextLen,string HeaderString,string unqId,string lstId,string bFValue,string lookUpDetails,string searchType,string DropdownCols,string Grouping,string ColQueryGrouping,string requireCheckbox,string securityrights,string sTotRecords,string requireDropdownList,string sesFlag,string screenIDString,string requireNormalCursor, string sCellHorizontalAlign)
		{
			bool bMultiselect=false,bshowExcluded=false,bASearch=false,bsSearch=false;

			if (bMs.ToUpper()=="TRUE") bMultiselect=true;
			if (bSx.ToUpper()=="TRUE") bshowExcluded=true;
			if (advSearch.ToUpper()=="TRUE") bASearch=true;
			if (sSearch.ToUpper()=="TRUE") bsSearch=true;
			string strResponse = new CmsWeb.support.ClsWebGridHelper().GetSortedData(sQg, sOc, sDg, sPc, sSg, sFg,
				bMultiselect,sSys, bshowExcluded, 
				int.Parse(iPs),int.Parse(pageNo), sSearchCriteria, 
				bASearch,int.Parse(iRn),sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,
				bsSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,
				unqId,lstId,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,sesFlag,screenIDString,requireNormalCursor, sCellHorizontalAlign);
		
			return strResponse;
		}
		[Ajax.AjaxMethod()]
		public string  EncriptValue(string str)
		{
			return Cms.CmsWeb.cmsbase.EncryptMessage(str);
		}
		private string maximumLength(string dispTextLen)
		{
			int maxNos=0;
			if(dispTextLen.Length>0 )
			{
				string [] arrLen=dispTextLen.Split(new char[]{'^'}); 
				maxNos=arrLen[0].Length;
				int i;	
				for(i=1;i<arrLen.Length-1;i++)
				{
					if(arrLen[i].Length>maxNos)
						maxNos=arrLen[i].Length;
				}
			}

			return maxNos.ToString(); 
		}
		

		private string createHeaderString(string maxNos)
		{
			StringBuilder lsDispTextLen =new StringBuilder() ;
			string [] arrLen=sDispColHeads.Split(new char[]{'^'}); 
			int i;	
			for(i=0;i<arrLen.Length;i++)
			{
				lsDispTextLen.Append((int.Parse(maxNos)- arrLen[i].Length) + "^" );  
			}

			if(lsDispTextLen.Length>0 )
			{
				lsDispTextLen=lsDispTextLen.Remove(lsDispTextLen.Length-1,1); 
			}

			return lsDispTextLen.ToString(); 

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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	
        private string CreateSelectString()
        {
            string selectType="";
            string [] dropColumns = drpColumns.Split(new char[]{'^'});
			string [] SearchColumns = sSearchTypes.Split(new char[]{'^'});
            int iCol=0;
            DataSet al=new DataSet(); 
            for(iCol=0;iCol< dropColumns.Length;iCol++)
            {
                if(dropColumns[iCol].ToString()!="")
                {
                    al=objCommon.ExecuteGridQuery(dropColumns[iCol].ToString());                    
                    int i=0;
                    selectType+="var _bdgSearchParam" + iCol + "=\"<Select id='SearchVal'>";

                    for(i=0;i<al.Tables[0].Rows.Count;i++)    
                        selectType+="<option value=" + al.Tables[0].Rows[i][0].ToString()  + ">" +  al.Tables[0].Rows[i][1].ToString() + "</option>";

                    selectType+="</select>\";";
                }
                else if(SearchColumns.Length > iCol && SearchColumns[iCol].ToString()=="D")
					selectType+="var _bdgSearchParam" + iCol + "=\"DATE\";";
				else
					selectType+="var _bdgSearchParam" + iCol + "=\"TEXT\";";

                if(al!=null)
                    if(al.Tables.Count>0)
                        al.Tables.Clear();  
            }

			if(drpColumns == "")
				return CreateSearchPopulationStr() ;
            return selectType;
        }
        
        
    

        
        private string CreateSearchPopulationStr() 
		{
			StringBuilder tmpSearch = new StringBuilder();
			try
			{
				string[] sArrSearchCols = sSearchCols.Split(new Char[]{'^'});
				string[] sArrSearchTypes = sSearchTypes.Split(new Char[]{'^'});
				if (sArrSearchCols.Length == sArrSearchTypes.Length) 
				{
					for(int i=0; i<sArrSearchCols.Length; i++)
					{
						if (sArrSearchTypes[i].Trim().ToUpper() == "D")
							tmpSearch.Append("var _bdgSearchParam" + i + " = \"DATE\";\n");
						else //if(sArrSearchTypes[i].Trim() == "" || sArrSearchTypes[i].Trim().ToUpper() == "T")
							tmpSearch.Append("var _bdgSearchParam" + i + " = \"TEXT\";\n");
//						else 
//						{
//							// code modified to accomodate the replacement of 
//							sArrSearchTypes[i]=sArrSearchTypes[i].Replace("|:|","^");
//							string[] sArrDropdownValues = sArrSearchTypes[i].Split('^');
//							tmpSearch.Append("var _bdgSearchParam" + i + " = \"<SELECT id='SearchVal'><option value=''>&nbsp;</option>");
//							for(int k=0; k<sArrDropdownValues.Length; k+=2) 
//								tmpSearch.Append("<option value='"+sArrDropdownValues[k]+"'>" + sArrDropdownValues[k+1] + "</option>");
//							tmpSearch.Append("</SELECT>\";\n");
//						}
					}
				}
				else 
				{
					for(int i=0; i<sArrSearchCols.Length; i++)
						tmpSearch.Append("var _bdgSearchParam" + i + " = \"TEXT\";\n");
				}
			}
			catch(Exception lEx)
			{
				throw lEx;
			}
			return tmpSearch.ToString();
		}
		/// <summary>
		///	To encryption of data to avoid any manipulation in data if this Control called from outside the application
		/// </summary>
		/// <param name="pStrData"></param>
		/// <returns></returns>
		private string EncryptData(string pStrData)
		{
			string data = null;
			data = cmsbase.EncryptMessage(pStrData.Trim());
			return data;
		}
	}
}
