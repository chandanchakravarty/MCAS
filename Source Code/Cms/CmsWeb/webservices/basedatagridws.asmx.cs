/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 
	<End Date				: - >
	<Description			: - > webservice to implement webgrid
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

    
   <Modified Date			: - > 08/04/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changes made for showing record in insert and update mode
	
	<Modified Date			: - > 11/04/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changes made for look up functionality

   	<Modified Date			: - > 13/04/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Cleaning up unwanted code, giving proper commenting, moving database related function to clscommon.cs

   	<Modified Date			: - > 27/06/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > working for grouping of records

   	<Modified Date			: - > 27/06/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > grouping of records have been done with seperate querystring ID but with hardcoded display names.


   	<Modified Date			: - > 8/07/2005
	<Modified By			: - > Anurag Verma
	<Purpose				: - > functionality for delete checkbox 
	
	<Modified Date			: - > 07/August/2006
    <Modified By			: - > Sumit Chhabra
	<Purpose				: - > Defined another parameter for GetSortedData function that will take the value
								  for RequireNormalCursor property. If the value is set	to Y then normal mouse
								  pointer will come and there will not be any change to the color of selected row.
								  
 	<Modified Date			: - > 09/08/2007
	<Modified By			: - > Praveen Kasana
	<Purpose				: - > functionality for decrypting Data for Index DataGrid Pages.

*******************************************************************************************/ 

using System;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Data.SqlClient ;
using System.Web.UI.WebControls; 
using System.Web.UI; 
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.WebServices
{
	/// <summary>
	/// Summary description for BaseDataGridWS.
	/// </summary>
	public class BaseDataGridWS : System.Web.Services.WebService
	{
		private Hashtable oHashTab;
		public string sHashOrderByColName="";
		const char SEPARATOR = '~';
		const char INTERNALSEPARATOR = '^';
		private static double DEFAULT_COLUMN_WIDTH=20;
		string IMAGESPATH=System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() +  "/Images/";
		private string allDblClick="";
		private string [] sdispTextLen={};
		private string [] strDisplayTextLen={};
		private string [] gArrColActualNames={};
		private Hashtable gobjHashTab;
		private string gstrLblSearchBtn; 
		private string gstrLblAdvSearchBtn; 
		private string gstrLblBasicSearchBtn; 
		private string gstrSearchTxt; 
		private string gstrAdvSearchTxt; 
		private string gstrSearchOptionTxt;
		private string sSearchCritTxt; 
		private string sErrorTxt,sNoMatchesTxt;
		private string sItemAnd;
		private string sItemOr;
		private string[]	strArrayButtonId;
		private string xmlData="";
		private string filterColumn=""; // using for storing filter column name value
		private int rowId=0;
		private string headText="";
		private string insertRowId=""; //to store inserted primary key id
		//private string gLPrimaryCol=""; //to store primary column name
		private string isUnq="";
		private string sPrimCol="";
		public string strDefGroupNo;
		//private DataGridItem _prevItem; 
		public string oldText="";
		public string oldText1="";
		public string oldText2="";
		public string oldText3="";
		public string oldText4="";
		private string grouping="";
		private string colGrps="";
		private string reqCheckbox="";
		private string reqNormalCursor="";
		private string reqDropDownList="";
		private string rowNum="";
		private string decrypt_val = "";
		private string decrypt_select = "";
		private int decrypt_position = 0;
		int flagDropdown=0;
		//Will hold the look up details in this format: 
		//LookupIDColName^LookupDescColName^LookupIDControlID^LookupDescControlID
		private string strLookUpDetails;
		public Hashtable ht=new Hashtable() ;
		//Will contain the values of the Primary key columns
		private string[] primaryKeyColumns;
		private string[] strArrayColTypes;
		private int iImgColPos=-1;
		private string strImgLnkColName="";
		public string strRightAlignCols="";

		private const int DefaultCommandTimeOut = 120;

		public BaseDataGridWS()
		{
			gstrLblSearchBtn = "Search";
			gstrLblAdvSearchBtn = "Advanced Search";
			gstrLblBasicSearchBtn = "Basic Search";
			//gstrSearchTxt = "Please select the Search Option, enter Search Criteria, and click on the Search button";
			//gstrAdvSearchTxt = "Please select the Search Option, enter Search Criteria, and click on the Search button";
			gstrSearchOptionTxt = "Search Option";
			sSearchCritTxt = "Search Criteria";
			sErrorTxt = "Some Error found";
			sNoMatchesTxt = "No Matches found";
			sItemAnd = "And";
			sItemOr = "Or";
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		[WebMethod (EnableSession=true)]
		private void SetSession(string srchCrit,string pageID)
		{
	
			if (Session["SrchCrit"] == null) 
			{
				if(srchCrit!="")
				{
					if(!ht.Contains(pageID))  
						ht.Add(pageID,srchCrit); 
					Session["SrchCrit"] = ht;
				}				
				else
					Session["SrchCrit"] = ht;
			}
			else
			{
		
				if(!ht.Contains(pageID))  
					ht.Add(pageID,srchCrit); 
				else
					ht[pageID]=srchCrit;
		
				Session["SrchCrit"] = ht;
			}			
		}


		[WebMethod (EnableSession=true)]
		private string GetSession(string pageID)
		{
			ht =(Hashtable) Session["SrchCrit"];
			string schcrit="";
			IDictionaryEnumerator myEnumerator = ht.GetEnumerator() ;
			while ( myEnumerator.MoveNext() )
			{
				//If no where condtion is set
				if (myEnumerator.Key.ToString()== pageID ) 
					schcrit= myEnumerator.Value.ToString() ; 
				else
					continue;
			}

			
			return   schcrit;			
		}


		[WebMethod (EnableSession=true)]
		public string GetSortedData(string sQueryGroup, string sOrderClause, string sDispColGroup, string sPrimaryCols, string sSearchColGroup, string sFilterGroup, bool bMultiSelect, string sSysColName, bool bShowExcluded, int iPageSize, int iPageNo, string sSearchCriteria, bool bAdvSearch, int iRowNos,string sSearchName,string sColumnType,string sPrimaryColsName,string sColsLink,string sDisplayColsName,bool sSearch,string sallowDBLClick,string searchText,string ExtraButtons,string sCacheSize,string ImagePath,string colHeader,string dispTextLen,string headerString,string unqId,string lstId,string strFilterValue,string lookUpDetails,string searchType,string DropdownCols,string needGrouping,string colQueryGroups,string requireCheckbox,string securityRights,string totRecords,string requireDropdownList,string sesFlag,string screenIDString,string requireNormalCursor,string sCellHorizontalAlign)
		{

			strRightAlignCols=sCellHorizontalAlign;
			string strDataRowClass="",strAlternatingDataRowClass="";
			//Assign the look up details to the instance variable
			grouping=needGrouping; 
			colGrps=colQueryGroups;
			reqCheckbox=requireCheckbox;	
			reqNormalCursor = requireNormalCursor;
			reqDropDownList=requireDropdownList;					
       
			strLookUpDetails = Server.UrlDecode(lookUpDetails).ToString();

			string sSysDefined = "";
			DataTable lobj_GridTable=null;
			DataSet lds=null;
			StringBuilder tmpTree = new StringBuilder();
			int lIntMainCount=0;
			sSysDefined = (sSysColName.IndexOf(".") > 0) ? sSysColName.Substring(sSysColName.IndexOf(".")+1).Trim() : sSysColName.Trim();
			try
			{
				int iMaxColNo = 0;
				if(lstId!="" && lstId!=null)
				{
					insertRowId=lstId;
				}
				if(unqId!="" && unqId!=null)
				{
					isUnq=unqId;
				}

				allDblClick=sallowDBLClick;

				sdispTextLen=(string [])Server.UrlDecode(dispTextLen).Split(new char []{INTERNALSEPARATOR});
				strDisplayTextLen=(string [])Server.UrlDecode(dispTextLen).Split(new char []{INTERNALSEPARATOR});
                

				/*for customizing display of search text*/
				searchText=searchText.Replace("%5E","^");
				string[] sArrSearch=searchText.Split(new Char[] {INTERNALSEPARATOR});
 
				if(sArrSearch.Length >= 2)
				{
					gstrSearchTxt = sArrSearch[0];
					gstrAdvSearchTxt=sArrSearch[1];
				}


				string[] arrDropdown=Server.UrlDecode(DropdownCols).Split(new char[]{INTERNALSEPARATOR}); //split the display col group string
				int iDrp=0;
                
				for(iDrp=0;iDrp<arrDropdown.Length;iDrp++)
					if(arrDropdown[iDrp].ToString().Trim() !="")
						flagDropdown=1;

				string[] sArrColGroup = Server.UrlDecode(sDispColGroup).Split(new char[]{SEPARATOR}); //split the display col group string
				sdispTextLen=(string [])sArrColGroup[0].Split(new char []{INTERNALSEPARATOR});
				string[] sArrColNos = sArrColGroup[3].Split(new char[]{INTERNALSEPARATOR}); //split the display col nos string
				string[] sArrColHeads = sArrColGroup[0].Split(new char[]{INTERNALSEPARATOR});//split the display col headings string
				string[] sArrTextLen = sArrColGroup[1].Split(new char[]{INTERNALSEPARATOR});//split the display col text length string
				strDisplayTextLen=sArrTextLen;		
				string[] sArrColType=Server.UrlDecode(sColumnType).Split(new Char[]{INTERNALSEPARATOR}); //Split the Column Type;

				strArrayColTypes = sArrColType;
				
				string[] sArrColsLink=Server.UrlDecode(sColsLink).Split(new Char[]{INTERNALSEPARATOR});//Split the Columns Link
				
				string[] sArrDispColsName=Server.UrlDecode(sDisplayColsName).Split(new Char[]{INTERNALSEPARATOR}); //Split the Column which will bind to the grid

				string[] sAddSpace=Server.UrlDecode(colHeader).Split(new char[]{INTERNALSEPARATOR});  
							
                				
				
				if(sArrColHeads.Length!=sArrColType.Length)
				{
					throw new Exception("Parameters on the aspx.cs are not setting properly,Please ensure the database columns,display column names, columns type are setting properly");
				}
				
				int iGroupColumns = int.Parse(sArrColGroup[4].Trim())-1 ;
				strDefGroupNo=sArrColGroup[4].ToString();
				string[] sArrColActualNames = sArrColGroup[2].Split(new char[]{INTERNALSEPARATOR});//split the actual column names string
				gArrColActualNames = sArrColActualNames;
				oHashTab = new Hashtable(sArrColActualNames.Length);
                
				sArrColGroup = null; 

				string[] sArrSearchGroup = Server.UrlDecode(sSearchColGroup).Split(new Char[]{SEPARATOR}); //split the search column group string

				string[] sArrSearchCols = sArrSearchGroup[0].Split(new Char[]{INTERNALSEPARATOR}); //split the search column string to get column names
				string[] sArrSearchHeads = sArrSearchGroup[1].Split(new Char[]{INTERNALSEPARATOR});//split the search heading string
				sArrSearchGroup = null;

				string[] sArrPrimaryCols = Server.UrlDecode(sPrimaryCols).Split(new Char[]{INTERNALSEPARATOR}); //split the primary column string
				sPrimCol=sPrimaryColsName;		
				//Get the values of the PK columns
				primaryKeyColumns = Server.UrlDecode(sPrimaryColsName).Split(new Char[]{INTERNALSEPARATOR});;

				//Bhuv
				//string sDefaultSortCol = sArrColNos[0].Trim(); //assume that the first display column is to be used for sorting purpose
				string sDefaultSortCol = sArrDispColsName[0].Trim(); //assume that the first display column is to be used for sorting purpose
				
				string sSearchCol = ""; //value to be set by CreateQuery fn
				string sSearchVal = ""; //value to be set by CreateQuery fn
				string sFilterCol = ""; //value to be set by CreateQuery fn
				string sFilterLabel = ""; //value to be set by CreateQuery fn
				string sSortColumn = ""; //value to be set by CreateQuery fn
				bool bSortAsc = true; //value to be set by CreateQuery fn
				string sSQLQuery="";// value to retrieve query to be inserted
				string sqlQuery = "";

				/***************************************************************************/
				///    for grouping purpose
				/**************************************************************************/

				string sAutoOrderClause = "";
				//find out the max no of the column to be displayed
				for(int i=0; i<sArrColNos.Length; i++) 
				{
					if (Int32.Parse(sArrColNos[i].Trim()) > iMaxColNo) iMaxColNo = Int16.Parse(sArrColNos[i].Trim());

					//store the col number with the column name in the hash table
					//to be used while preparing the ORDER BY clause based on the col number
					oHashTab.Add(sArrColNos[i],sArrColActualNames[i]);
					if(sArrColActualNames[i].Trim() == "")
						sAutoOrderClause += sArrColNos[i].Trim() + ",";
					else
						sAutoOrderClause += sArrColActualNames[i].Trim() + ",";
				}
				sAutoOrderClause = sAutoOrderClause.Substring(0,sAutoOrderClause.Length-1);

				//				string pageID=screenIDString;
				//					
				//				if(Session["SrchCrit"]!=null)
				//					if(sesFlag != "Y" && sSearchCriteria == GetSession(pageID))
				//					{
				//						sSearchCriteria = GetSession(pageID);
				//					}
				//					else if(sesFlag == "Y" )
				//					{
				//						if(GetSession(pageID)!="")
				//							sSearchCriteria = GetSession(pageID);
				//					}
				//
				//
				//				if(sesFlag != "Y")
				//					SetSession(sSearchCriteria,pageID);

				sQueryGroup = DecryptData(sQueryGroup.Trim());

				
				if (sSearchCriteria == null || sSearchCriteria == "")
					sqlQuery = CreateGroupQuery(sQueryGroup+sOrderClause, sAutoOrderClause, sFilterGroup, bShowExcluded, "", sDefaultSortCol, out sFilterCol, out sFilterLabel, out sSortColumn, out bSortAsc, out sSearchCol, out sSearchVal,out sSQLQuery,sSearchName,iPageNo,sPrimaryColsName,sCacheSize,unqId,lstId,strFilterValue,searchType,sArrSearchCols,totRecords,iPageSize.ToString() );
				else
					sqlQuery = CreateGroupQuery(sQueryGroup+sOrderClause, sAutoOrderClause, sFilterGroup, bShowExcluded, sSearchCriteria, sDefaultSortCol, out sFilterCol, out sFilterLabel, out sSortColumn, out bSortAsc, out sSearchCol, out sSearchVal,out sSQLQuery,sSearchName,iPageNo,sPrimaryColsName,sCacheSize,unqId,lstId,strFilterValue,searchType,sArrSearchCols,totRecords,iPageSize.ToString() );

         			
				int lIntStart=0;
				int totalRec=-1;
				if(iPageNo==0) iPageNo=0;
				else lIntStart=(iPageNo-1)*iPageSize;

				int pageNum=0;
				if(decrypt_val == "")
				{
					if(unqId!="" && unqId!=null)
						lds = getGridRecords(sqlQuery,0,iPageSize,out totalRec,out pageNum,sPrimaryColsName);
					else
						lds = getGridRecords(sqlQuery,lIntStart,iPageSize,out totalRec,out pageNum,sPrimaryColsName);
				}
				else
				//if(decrypt_val != "")
				//Added by Mohit Agarwal 29-Oct-08 ITrack 4953
				{
					SqlConnection lobjCon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"].ToString());
					SqlCommand lobjCmd=new SqlCommand();
					SqlDataAdapter lobjDA=new SqlDataAdapter();
					lds = new DataSet();
					try
					{
				
						lobjCon.Open();
						lobjCmd.Connection=lobjCon;
						lobjCmd.CommandTimeout = DefaultCommandTimeOut;
						lobjCmd.CommandText=sqlQuery;
						lobjCmd.CommandType=CommandType.Text;
						lobjDA.SelectCommand=lobjCmd;

						lobjDA.Fill(lds,"temp1");

						totalRec = lds.Tables[0].Rows.Count;
						for(int index = lds.Tables[0].Rows.Count-1; index >= 0 ; index--)
						{
							DataRow ldr = lds.Tables[0].Rows[index];
							try
							{
								string db_decrypt_val = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(ldr[decrypt_position].ToString());
								if(db_decrypt_val.IndexOf(decrypt_val) < 0)
								{
									lds.Tables[0].Rows[index].Delete();
									totalRec--;
									//ldr.Delete();
									//index--;
								}
							}
							catch(Exception)
							{}
						}
						pageNum = 1;
					}
					catch(Exception lEx)
					{
						throw lEx;
					}
					finally
					{
						if(lobjDA!=null)lobjDA.Dispose();
						if(lobjCmd!=null)lobjCmd.Dispose();
						if(lobjCon!=null)lobjCon.Dispose();
					}
				}

				//Added by Mohit Agarwal 29-Oct-08 ITrack 4953
				if(decrypt_select != "")
				{
					foreach(DataRow ldr in lds.Tables[0].Rows)
					{
						try
						{
							ldr[decrypt_select] = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(ldr[decrypt_select].ToString());
							string strFEDERAL_ID = ldr[decrypt_select].ToString();
							if(strFEDERAL_ID != "")
							{
								string strvaln = "";
								for(int len=0; len < strFEDERAL_ID.Length-4; len++)
									strvaln += "x";

								strvaln += strFEDERAL_ID.Substring(strvaln.Length, strFEDERAL_ID.Length - strvaln.Length);
								ldr[decrypt_select] = strvaln;
							}
						}
						catch(Exception ignore)
                        { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ignore); }
					}
				}

				iPageNo=pageNum;
				xmlData=lds.GetXml(); 
				
				string lStrDataGridHtml = null;
				DataGrid lGrdGeneric = new DataGrid();

				//-----------------------Set Design Properties----------------------------------
				lGrdGeneric.AutoGenerateColumns = false;
				lGrdGeneric.Width=System.Web.UI.WebControls.Unit.Percentage(100);
				if(requireNormalCursor!="" && requireNormalCursor.ToUpper()=="Y")
				{
					strDataRowClass = "DataRow2";
					strAlternatingDataRowClass = "AlternateDataRow2";					
				}
				else
				{
					strDataRowClass = "DataRow";
					strAlternatingDataRowClass = "AlternateDataRow";					
				}
				lGrdGeneric.ItemStyle.CssClass=strDataRowClass;
				lGrdGeneric.AlternatingItemStyle.CssClass=strAlternatingDataRowClass;
				lGrdGeneric.PagerStyle.CssClass=strAlternatingDataRowClass;
				lGrdGeneric.AllowPaging=true;
				lGrdGeneric.AllowCustomPaging=true;
				lGrdGeneric.PagerStyle.PageButtonCount=iPageSize;
				lGrdGeneric.PagerStyle.Position=PagerPosition.Bottom;				
				lGrdGeneric.HeaderStyle.CssClass="headereffectWebGrid";
				lGrdGeneric.HeaderStyle.VerticalAlign=VerticalAlign.Top;  

				BoundColumn lObjGrdColB			= null;
				ButtonColumn lObjGrdColBT		= null;
				HyperLinkColumn lObjGrdColHL	= null;
				HyperLinkColumn lObjGrdColA		= null;
				TemplateColumn lObjGrdColT		= null;

				/* Column Type------------
				 * 1 BoundColumn	-> B
				 * 2 ButtonColumn	-> BT
				 * 3 HyperLinkColumn-> HL
				 * 4 TemplateColun	-> T
				 *-----------------------*/
				string lStrHeaderText="";
				string lstrColsType="";
				double lIntColWidth=0;

				if(reqCheckbox=="Y")
				{
					lObjGrdColT  = new TemplateColumn();
					lObjGrdColT.HeaderText = "<input type=checkbox onclick='selectAll()'>";
					//lObjGrdColT.SortExpression=sArrColHeads[lIntMainCount];
					lObjGrdColT.ItemStyle.Wrap=true;
					//lObjGrdColT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
					lObjGrdColT.ItemTemplate =new CreateItemTemplateCK("ck_"+lIntMainCount);
					lObjGrdColT.ItemStyle.VerticalAlign=VerticalAlign.Top; 
					lObjGrdColT.HeaderStyle.VerticalAlign =  VerticalAlign.Top; 
					lGrdGeneric.Columns.Add(lObjGrdColT);
					lObjGrdColT = null;
				}

				             
				for (lIntMainCount=0; lIntMainCount < sArrColActualNames.GetLength(0); lIntMainCount++)
				{
					lstrColsType=sArrColType[lIntMainCount];
					if(isNumeric(sArrTextLen[lIntMainCount])) 
						lIntColWidth=Convert.ToDouble(sArrTextLen[lIntMainCount].ToString());
					else lIntColWidth=DEFAULT_COLUMN_WIDTH;
					if(lstrColsType.Length==0)lstrColsType="B";
					
					//Modified by Asfa(18-June-2008) - iTrack #3906(Note: 1)
					if (sSortColumn.ToLower().Equals(sArrDispColsName[lIntMainCount].Trim().ToLower()) && bSortAsc) //if the sort column no. matches with the column no
						lStrHeaderText="<a   href=\"javascript: changeSort(1, '"+sArrDispColsName[lIntMainCount].Trim() +"', false, "+bAdvSearch.ToString().ToLower()+ ", '" + lstrColsType +"');\"><div id=\"ColHead_"+sArrDispColsName[lIntMainCount].Trim()+"\" >&nbsp;<font color=white  >"+ sArrColHeads[lIntMainCount].Trim() + "</font> <img src=\"" + IMAGESPATH + "asc.gif\" border=0></div></a>";
					else if (sSortColumn.ToLower().Equals(sArrDispColsName[lIntMainCount].Trim().ToLower()) && !bSortAsc)
						lStrHeaderText="<a href=\"javascript: changeSort(1, '"+sArrDispColsName[lIntMainCount].Trim()+"', true, "+bAdvSearch.ToString().ToLower()+ ", '" + lstrColsType +"');\"><div  id=\"ColHead_"+sArrDispColsName[lIntMainCount].Trim()+"\" >&nbsp;<font color=white >"+ sArrColHeads[lIntMainCount].Trim() + "</font> <img src=\"" + IMAGESPATH + "desc.gif\" border=0></div></a>";
					else if ((sSortColumn.ToLower().IndexOf(sArrDispColsName[lIntMainCount].Trim().ToLower()) > 0) && bSortAsc) //if the sort column no. matches with the column no
						lStrHeaderText="<a   href=\"javascript: changeSort(1, '"+sArrDispColsName[lIntMainCount].Trim() +"', false, "+bAdvSearch.ToString().ToLower()+ ", '" + lstrColsType +"');\"><div id=\"ColHead_"+sArrDispColsName[lIntMainCount].Trim()+"\" >&nbsp;<font color=white  >"+ sArrColHeads[lIntMainCount].Trim() + "</font> <img src=\"" + IMAGESPATH + "asc.gif\" border=0></div></a>";
					else if ((sSortColumn.ToLower().IndexOf(sArrDispColsName[lIntMainCount].Trim().ToLower()) > 0) && !bSortAsc)
						lStrHeaderText="<a href=\"javascript: changeSort(1, '"+sArrDispColsName[lIntMainCount].Trim()+"', true, "+bAdvSearch.ToString().ToLower()+ ", '" + lstrColsType +"');\"><div  id=\"ColHead_"+sArrDispColsName[lIntMainCount].Trim()+"\" >&nbsp;<font color=white >"+ sArrColHeads[lIntMainCount].Trim() + "</font> <img src=\"" + IMAGESPATH + "desc.gif\" border=0></div></a>";
					else
						lStrHeaderText="<a href=\"javascript: changeSort(1, '"+sArrDispColsName[lIntMainCount].Trim()+"', true, "+bAdvSearch.ToString().ToLower()+ ", '" + lstrColsType +"');\"><div  id=\"ColHead_"+sArrDispColsName[lIntMainCount].Trim()+"\" >&nbsp;<font color=white >"+ sArrColHeads[lIntMainCount].Trim()   + "</font></div></a>";

					switch(lstrColsType)
					{
						case "N": //Added by Asfa(18-June-2008) - iTrack #3906(Note: 1)
						case "B":
							lObjGrdColB  = new BoundColumn();
							lObjGrdColB.HeaderText = lStrHeaderText;
							lObjGrdColB.HeaderStyle.VerticalAlign=VerticalAlign.Top;
							lObjGrdColB.ItemStyle.VerticalAlign=VerticalAlign.Top;   
							lObjGrdColB.DataField = sArrDispColsName[lIntMainCount];
							lObjGrdColB.ItemStyle.Wrap=true;
							lObjGrdColB.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lGrdGeneric.Columns.Add(lObjGrdColB);
							lObjGrdColB = null;
							break;
						case "BT":
							lObjGrdColBT  = new ButtonColumn();
							lObjGrdColBT.ButtonType=ButtonColumnType.PushButton;
							lObjGrdColBT.HeaderText = lStrHeaderText ;
							lObjGrdColBT.CommandName=sArrColHeads[lIntMainCount];
							lObjGrdColBT.DataTextField = sArrDispColsName[lIntMainCount];
							lObjGrdColBT.ItemStyle.Wrap=true;
							lObjGrdColBT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lGrdGeneric.Columns.Add(lObjGrdColBT);
							lObjGrdColBT = null;
							break;
						case "HL":
							lObjGrdColHL  = new HyperLinkColumn();
							lObjGrdColHL.DataNavigateUrlField = "UniqueGrdId";
							lObjGrdColHL.DataNavigateUrlFormatString="javascript:void(0) onclick='image_click(" + sArrColsLink[0]+"&{0}')";
							lObjGrdColHL.NavigateUrl=Server.UrlEncode(lObjGrdColHL.DataNavigateUrlFormatString);
							lObjGrdColHL.Target="botframe";    
							lObjGrdColHL.ItemStyle.CssClass="midcolora";
							lObjGrdColHL.HeaderText = lStrHeaderText;
							lObjGrdColHL.DataTextField = sArrDispColsName[lIntMainCount];							
							lObjGrdColHL.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColHL.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lGrdGeneric.Columns.Add(lObjGrdColHL);
							lObjGrdColHL = null;
							break;
						case "HA":
							lObjGrdColA  = new HyperLinkColumn();
							lObjGrdColA.DataNavigateUrlField = "UniqueGrdId";
							lObjGrdColA.DataNavigateUrlFormatString="javascript:"+sArrColsLink[lIntMainCount]+"('{0}')";
							lObjGrdColA.HeaderText = lStrHeaderText;
							lObjGrdColA.DataTextField = sArrDispColsName[lIntMainCount];
							lObjGrdColA.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColA.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lGrdGeneric.Columns.Add(lObjGrdColA);
							lObjGrdColA = null;
							break;
						case "LBL":
							lObjGrdColT  = new TemplateColumn();
							lObjGrdColT.HeaderText = lStrHeaderText;
							lObjGrdColT.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColT.ItemStyle.Wrap=true;
							lObjGrdColT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lObjGrdColT.ItemTemplate =new CreateItemTemplateLabel("hl_"+lIntMainCount,sArrColsLink[lIntMainCount]);
							lObjGrdColT.ItemStyle.VerticalAlign=VerticalAlign.Top;
							lGrdGeneric.Columns.Add(lObjGrdColT);
							lObjGrdColT = null;
							break;

						case "T": //pending 
							lObjGrdColT  = new TemplateColumn();
							lObjGrdColT.HeaderText = lStrHeaderText;
							lObjGrdColT.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColT.ItemStyle.Wrap=true;
							lObjGrdColT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lObjGrdColT.ItemTemplate =new CreateItemTemplateHL("hl_"+lIntMainCount,sArrColsLink[lIntMainCount]);
							lObjGrdColT.ItemStyle.VerticalAlign=VerticalAlign.Top;
							//lObjGrdColT.ItemStyle.HorizontalAlign=HorizontalAlign.Center;
							//lObjGrdColT.ItemStyle.BorderWidth=System.Web.UI.WebControls.Unit.Pixel(0) ; 
							//lObjGrdColT.ItemStyle.BorderStyle=BorderStyle.None;   	
							//lObjGrdColT.ItemStyle.CssClass =   "DataRow";
							lGrdGeneric.Columns.Add(lObjGrdColT);
							lObjGrdColT = null;
							break;
						case "DL": //dropdownlist and a label
							lObjGrdColT  = new TemplateColumn();
							lObjGrdColT.HeaderText = lStrHeaderText;
							lObjGrdColT.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColT.ItemStyle.Wrap=true;
							lObjGrdColT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lObjGrdColT.ItemTemplate =new CreateItemTemplateDDLAndText("ddl_"+lIntMainCount,"lbl_"+lIntMainCount);
							lObjGrdColT.ItemStyle.VerticalAlign=VerticalAlign.Top;
							lGrdGeneric.Columns.Add(lObjGrdColT);
							lObjGrdColT = null;
							break;
						case "D": //dropdownlist
							lObjGrdColT  = new TemplateColumn();
							lObjGrdColT.HeaderText = lStrHeaderText;
							lObjGrdColT.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColT.ItemStyle.Wrap=true;
							lObjGrdColT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lObjGrdColT.ItemTemplate =new CreateItemTemplateDDL("ddl_"+lIntMainCount);
							lObjGrdColT.ItemStyle.VerticalAlign=VerticalAlign.Top;
							lGrdGeneric.Columns.Add(lObjGrdColT);
							lObjGrdColT = null;
							break;
						case "BUT": //button
							lObjGrdColT  = new TemplateColumn();
							lObjGrdColT.HeaderText = lStrHeaderText;
							lObjGrdColT.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColT.ItemStyle.Wrap=true;
							lObjGrdColT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lObjGrdColT.ItemTemplate =new CreateItemTemplateBUT("but_"+lIntMainCount);
							lObjGrdColT.ItemStyle.VerticalAlign=VerticalAlign.Top;
							lGrdGeneric.Columns.Add(lObjGrdColT);
							lObjGrdColT = null;
							break;
						case "IMG": //Image
							lObjGrdColT  = new TemplateColumn();
							lObjGrdColT.HeaderText = lStrHeaderText;
							lObjGrdColT.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColT.ItemStyle.Wrap=true;
							lObjGrdColT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lObjGrdColT.ItemTemplate =new CreateItemTemplateIMG("img_"+lIntMainCount);
							lObjGrdColT.ItemStyle.VerticalAlign=VerticalAlign.Top;
							lGrdGeneric.Columns.Add(lObjGrdColT);
							lObjGrdColT = null;
							break;
						case "IMGLNK":
							lObjGrdColT  = new TemplateColumn();
							lObjGrdColT.HeaderText = lStrHeaderText;
							lObjGrdColT.SortExpression=sArrColHeads[lIntMainCount];
							lObjGrdColT.ItemStyle.Wrap=true;
							lObjGrdColT.ItemStyle.Width=System.Web.UI.WebControls.Unit.Percentage(lIntColWidth);
							lObjGrdColT.ItemTemplate =new CreateItemTemplateHL("ImgLnk_"+lIntMainCount,sArrColsLink[lIntMainCount]);
							lObjGrdColT.ItemStyle.VerticalAlign=VerticalAlign.Top;
							//lObjGrdColT.ItemStyle.HorizontalAlign=HorizontalAlign.Center;
							//lObjGrdColT.ItemStyle.BorderWidth=System.Web.UI.WebControls.Unit.Pixel(0) ; 
							//lObjGrdColT.ItemStyle.BorderStyle=BorderStyle.None;   	
							//lObjGrdColT.ItemStyle.CssClass =   "DataRow";
							lGrdGeneric.Columns.Add(lObjGrdColT);
							lObjGrdColT = null;
							break;
							
					}
				}

				#region PAGING IMPLEMENTATION
				string lStrPagingPrev="";
				string lStrPagingNext="";
				string lStrTotalPages="";
				string lsGoToPageText ="";
			
				int iTotalPages = -1;
				if(lds.Tables[0].Rows.Count>0)
					iTotalPages = (int) Math.Ceiling((double) int.Parse(totalRec.ToString() )/iPageSize);

				if (iPageSize > 0 && iTotalPages > 0) 
				{
					lStrTotalPages="Page " + iPageNo.ToString() + " of " + iTotalPages.ToString()+"&nbsp;";
					if (iPageNo == 1 || iTotalPages == 1)
						lStrPagingPrev="<img align=absbottom src='" + IMAGESPATH + "firstoff.gif' border='0'>&nbsp;&nbsp;<img align=absbottom src='" + IMAGESPATH + "prevoff.gif' border='0'>&nbsp;&nbsp;&nbsp;&nbsp;";
					else
						lStrPagingPrev="<a href=\"javascript:changePage(1, '"+sOrderClause+"', "+bAdvSearch.ToString().ToLower()+");\">&nbsp;&nbsp;<img align=absbottom src='" + IMAGESPATH + "first.gif' border='0'></a>&nbsp;<a href=\"javascript:changePage(" + (iPageNo-1)+ ", '"+sOrderClause+"', "+bAdvSearch.ToString().ToLower()+");\"><img align=absbottom src='" + IMAGESPATH + "prev.gif' border='0'></a>&nbsp;&nbsp;&nbsp;&nbsp;";

					if (iPageNo == iTotalPages)
						lStrPagingNext="<img align=absbottom src='" + IMAGESPATH + "nextoff.gif' border='0'>&nbsp;&nbsp;<img align=absbottom src='" + IMAGESPATH + "lastoff.gif' border='0'>&nbsp;&nbsp;&nbsp;&nbsp;";
					else
						lStrPagingNext="<a href=\"javascript:changePage("+(iPageNo+1)+", '"+sOrderClause+"', "+bAdvSearch.ToString().ToLower()+");\"><img align=absbottom src='" + IMAGESPATH + "next.gif' border='0'></a>&nbsp;&nbsp;<a href=\"javascript:changePage(" + iTotalPages+ ", '"+sOrderClause+"', "+bAdvSearch.ToString().ToLower()+");\"><img align=absbottom src='" + IMAGESPATH + "last.gif' border='0'></a>&nbsp;&nbsp;&nbsp;&nbsp;";
					
					lGrdGeneric.PagerStyle.PrevPageText=lStrPagingPrev+lStrTotalPages;
					lGrdGeneric.PagerStyle.NextPageText=lStrPagingNext + lsGoToPageText;
					lGrdGeneric.PagerStyle.HorizontalAlign=HorizontalAlign.Center;   
				}

				lGrdGeneric.PageSize=iPageSize;
				if(lds.Tables[0].Rows.Count>0)
					lGrdGeneric.VirtualItemCount=totalRec;
				//lGrdGeneric.VirtualItemCount=int.Parse(lds.Tables[1].Rows[0].ItemArray[0].ToString());
				//---------------------------------------
				 
				if(lds.Tables[0].Rows.Count<=0 || (unqId!="" && unqId!=null))                        
					lGrdGeneric.AllowPaging=false; 
				#endregion

				#region BINDING DATAGRID
				lGrdGeneric.ItemDataBound += new DataGridItemEventHandler(this.GridBinding);
				lds.Tables[0].TableName="DataTable";  
				lGrdGeneric.DataSource = lds.Tables[0];
				lGrdGeneric.DataBind();
				#endregion
		    	
				#region RENDERING DATAGRID
				StringBuilder lObjStringBuilder = new StringBuilder();
				StringWriter lObjStringWriter = new StringWriter(lObjStringBuilder);
				HtmlTextWriter lObjHtmlTextWriter = new HtmlTextWriter(lObjStringWriter);
				lGrdGeneric.RenderControl(lObjHtmlTextWriter);
				lStrDataGridHtml = lObjStringBuilder.ToString();
				#endregion				
				
				#region SEARCH OPTION
				if(sSearch)
				{
					tmpTree.Append("<table id=\"headerTable\" cellSpacing=0 cellPadding=0 width=100% align=center border=0>");

					if(Server.UrlDecode(sSearchCriteria).IndexOf(SEPARATOR)>0 )
					{
						string[] safterDecode = Server.UrlDecode(sSearchCriteria).Split(new char[]{SEPARATOR}); 
						if(safterDecode[1]=="*#)(")
							tmpTree.Append("<tr><td class=headereffectCenter colspan=1 align=center width='100%'><b> " + headerString + " </b></td></tr>");
						else
							//BELOW LINE IS WITH IMAGE CODE---DO NOT DELETE
							//tmpTree.Append("<tr><td class=headereffectCenter colspan=2 align=center width='98%'><b>" + headerString + " </b></td><td width='2%' class=HeaderEffectCenter align='center'><img src='" + ImagePath + "' id=\"imgCollapse\"   onclick='col_exp();' style=\"display:inline\"></td></tr>");
							tmpTree.Append("<tr><td class=headereffectCenter colspan=3 align=center width='100%'><b>" + headerString + " </b></td></tr>");
					}
					else if(sSearchCriteria=="")
					{
						//BELOW LINE IS WITH IMAGE CODE---DO NOT DELETE
						//tmpTree.Append("<tr><td class=headereffectCenter colspan=2 align=center width='98%'><b>" + headerString + " </b></td><td width='2%' class=HeaderEffectCenter align='center'><img src='" + ImagePath + "' id=\"imgCollapse\"   onclick='col_exp();' style=\"display:inline\"></td></tr>");
						tmpTree.Append("<tr><td class=headereffectCenter colspan=3 align=center width='100%'><b>" + headerString + " </b></td></tr>");
					}

					tmpTree.Append("</table><table id=\"mainTable\" cellSpacing=0 cellPadding=0 width=100% align=center border=0>");
					
					
					tmpTree.Append("<tr><td width=2% class=SearchRow>&nbsp;<input type=hidden name=txtTotalRecords value='"+lGrdGeneric.VirtualItemCount+"'></td><td colspan=3 class=SearchRow width=78%>");

					if (!bAdvSearch) 
						tmpTree.Append(gstrSearchTxt);
					else
						tmpTree.Append(gstrAdvSearchTxt);
					tmpTree.Append("</td>");

					if (sFilterLabel != "" && bShowExcluded)
					{
						tmpTree.Append("<td width=\"20%\" valign=right  align=right class=SearchRow>"+sFilterLabel + "<input type=checkbox name=ShowExcluded value=1 CHECKED onclick=\"javascript: checkSearch(1,"+bAdvSearch.ToString().ToLower()+");\"><td></tr>");
					}
					else if (sFilterLabel != "")
					{
						tmpTree.Append("<td width=\"20%\" valign=right align=right class=SearchRow>"+sFilterLabel + "<input type=checkbox name=ShowExcluded value=1 onclick=\"javascript: checkSearch(1,"+bAdvSearch.ToString().ToLower()+");\"><td></tr>");
					}
					else
					{
						tmpTree.Append("<td width=\"20%\" align=right class=SearchRow>&nbsp;<td></tr>");
					}

					tmpTree.Append("</tr><tr><td width=2% class=SearchRow>&nbsp;</td><td colspan=4 align=center class=SearchRow width=98%>");
					if (!bAdvSearch) 
					{
						bool bSearchColSelected = false;

						if(flagDropdown==1)
							tmpTree.Append("<br><b>"+gstrSearchOptionTxt + ":&nbsp;</b><select id=SearchCol onchange=\"_bdgPopulateSearch();ShowCalendar();\">");
						else
							tmpTree.Append("<br><b>"+gstrSearchOptionTxt + ":&nbsp;</b><select id=SearchCol onchange=\"_bdgPopulateSearch();ShowCalendar();\">");

						for(int i=0; i<sArrSearchCols.Length; i++) 
						{
							tmpTree.Append("<option value=\""+ sArrSearchCols[i].Trim() +"\"");
							if (sSearchCol.ToLower().Equals(sArrSearchCols[i].Trim().ToLower())) 
							{ 
								tmpTree.Append(" SELECTED");
								bSearchColSelected = true;
							}
							tmpTree.Append(">"+ sArrSearchHeads[i].Trim() +"</option>\n");
						}
						tmpTree.Append("</select>");
						if (!bSearchColSelected) sSearchVal = "";
						if(Server.HtmlEncode(sSearchVal)=="*#)(")
							sSearchVal="";

						tmpTree.Append("&nbsp;&nbsp;<b>" + sSearchCritTxt + ":&nbsp;</b><span id=_bdgSearchDiv><input type=text onBlur=\"ChangeDate();FormatAmountSearchGrid()\" id=SearchVal size=20  value=\"" + Server.HtmlEncode(sSearchVal) + "\"></span>&nbsp;");
						
						//tmpTree.Append("<a id=\"hlkEXPDT_DATE\" CssClass=\"HotSpot\" style=\"display:none;\" onclick=\"fPopCalendar(document.all('SearchVal'),document.all('SearchVal'));\"><img id=\"imgEXPDT_DATE\" style=\"display:none;\" src=\"../../cmsweb/Images/CalendarPicker.gif\"></a>");													
						tmpTree.Append("<input type=\"button\" class=\"clsButton\" value=\""+gstrLblSearchBtn+"\" id=\"SearchButton\" onclick=\"initSearch(1, false);\">");
						tmpTree.Append("&nbsp;<input type=button class=clsButton value=\""+gstrLblAdvSearchBtn+"\"  onclick=\"getDefaultPage(1, true);\"  size=3>");
					}

					if (bAdvSearch) tmpTree.Append(CreateAdvSearch(sArrSearchCols, sArrSearchHeads, sSearchCriteria, iRowNos));
					tmpTree.Append("</td></tr>");
					
				}
				else
				{
					tmpTree.Append("<table id=\"table11\" cellSpacing=0 cellPadding=0 width=100% align=center border=0 style='display:none'>");
					tmpTree.Append("<tr><td class=formrow1 colspan=3>&nbsp;<input type=hidden name=txtTotalRecords value='"+lGrdGeneric.VirtualItemCount+"'></td></tr>");
					tmpTree.Append("<tr><td width=2% class=formrow1>&nbsp;</td><td class=formrow1 width=80%>");

					if (!bAdvSearch) 
						tmpTree.Append(gstrSearchTxt);
					else
						tmpTree.Append(gstrAdvSearchTxt);

					if (sFilterLabel != "" && bShowExcluded)
						tmpTree.Append("<td width=18% align=right class=formrow1>"+sFilterLabel + "<input type=checkbox name=ShowExcluded value=1 CHECKED onclick=\"javascript: initSearch(1,"+bAdvSearch.ToString().ToLower()+");\"><td></tr>");
					else if (sFilterLabel != "")
						tmpTree.Append("<td width=18% align=right class=formrow1>"+sFilterLabel + "<input type=checkbox name=ShowExcluded value=1 onclick=\"javascript: initSearch(1, '"+bAdvSearch.ToString().ToLower()+"');\"><td></tr>");

					tmpTree.Append("<tr><td width=2% class=formrow1>&nbsp;</td><td colspan=2 align=center class=formrow1 width=98%>");
					if (!bAdvSearch) 
					{
						bool bSearchColSelected = false;
						tmpTree.Append("<br><b>"+gstrSearchOptionTxt + ":&nbsp;</b><select id=SearchCol class=SearchList onChange=\"ShowCalendar();\">");
						for(int i=0; i<sArrSearchCols.Length; i++) 
						{
							tmpTree.Append("<option value=\""+ sArrSearchCols[i].Trim() +"\"");
							if (sSearchCol.Equals(sArrSearchCols[i].Trim())) 
							{ 
								tmpTree.Append(" SELECTED");
								bSearchColSelected = true;
							}
							tmpTree.Append(">"+ sArrSearchHeads[i].Trim() +"</option>\n");
						}
						tmpTree.Append("</select>");
						if (!bSearchColSelected) sSearchVal = "";
						tmpTree.Append("&nbsp;&nbsp;<b>" + sSearchCritTxt + ":&nbsp;</b><span id=_bdgSearchDiv><input type=text id=SearchVal size=20 class=SearchText onBlur=\"ChangeDate();FormatAmountSearchGrid()\" value=\"" + Server.HtmlEncode(sSearchVal) + "\"></span>&nbsp;<input type=\"button\" class=\"clsButton\" value=\""+gstrLblSearchBtn+"\" id=\"SearchButton\" onclick=\"initSearch(1, false);\">");
						tmpTree.Append("&nbsp;<input type=button class=clsButton value=\""+gstrLblAdvSearchBtn+"\"  onclick=\"getDefaultPage(1, true);\" size=3>");
					}

					if (bAdvSearch) tmpTree.Append(CreateAdvSearch(sArrSearchCols, sArrSearchHeads, sSearchCriteria, iRowNos));
					tmpTree.Append("</td></tr>");
					tmpTree.Append("<tr><td colspan=3 class=formrow1>&nbsp;</td></tr>");

				}
				#endregion				

				string []srhCriteria=Server.UrlDecode(sSearchCriteria.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
				
				tmpTree.Append("<tr><td colspan=5  class=SearchRow>&nbsp;</td></tr></table>" + lStrDataGridHtml);
				if(unqId!="" && unqId!=null)
				{
					lStrTotalPages="<table id=\"pageTable\" style=\"display:none\" border=0 width=100% align=center><tr class=" + strDataRowClass + "><td width=100% colspan=5>&nbsp;</td></tr><tr class=" + strDataRowClass + "><td width=100% align=center colspan=5><font size=2 face='Verdana, Arial, Helvetica, sans-serif' color=black>Page 0 of 0</font></td></tr></table>";				
					tmpTree.Append(lStrTotalPages);	
				}
				else
				{
					if(srhCriteria.Length>1)
					{
						if(srhCriteria[1]=="*#)(")
						{
							if(lds.Tables[0].Rows.Count==0)
							{
								lStrTotalPages="<table id=\"pageTable\" cellspacing=0 cellpadding=0 border=0 width=100% align=center><tr class=" + strDataRowClass + "><td width=100% align=center colspan=5>Please click search button to perform search</td></tr></table>";				
								tmpTree.Append(lStrTotalPages);	
							}
						}
						else
						{
							if(lds.Tables[0].Rows.Count==0)
							{
								lStrTotalPages="<table id=\"pageTable\" cellspacing=0 cellpadding=0 border=0 width=100% align=center><tr class=" + strDataRowClass + "><td width=100% align=center colspan=5>No Record Found</td></tr></table>";				
								tmpTree.Append(lStrTotalPages);	
							}                            
						}
					}
					else
					{
						if(lds.Tables[0].Rows.Count==0)
						{
							lStrTotalPages="<table id=\"pageTable\" cellspacing=0 cellpadding=0 border=0 width=100% align=center><tr class=" + strDataRowClass + "><td width=100% align=center colspan=5>No Record Found</td></tr></table>";				
							tmpTree.Append(lStrTotalPages);	
						}
					}
				}

				#region extra buttons
                
				string[] strArrayButtonText;	// Stores the button text for extra button
				string[] btnArray;				// splits the param ExtraButton
				int numberOfExtraButton;		// stores no. of extra buttons to be added.
				string[] strArrayFunctionText;	// Stores the function name for extra button
                

                        

				if(ExtraButtons.Trim() != "")
				{
					ExtraButtons=ExtraButtons.Replace("%5E","^");
					ExtraButtons=ExtraButtons.Replace("%7E","~");
					ExtraButtons=ExtraButtons.Replace("%20"," ");
					
					string[] security=securityRights.Replace("%7E","~").Split(SEPARATOR);
					string write="Y";
					string delete="Y";
					string execute="Y";
					int secArray=0;
					for(secArray=0;secArray<security.Length ;secArray++)
					{
						if(secArray==1)
							write=security[secArray].ToString() ;
						else if(secArray == 2)
							delete= security[secArray].ToString();
						else if(secArray == 3)
							execute= security[secArray].ToString();
					}

 
					btnArray				=	ExtraButtons.Split(INTERNALSEPARATOR);
					numberOfExtraButton		=	Int32.Parse(btnArray[0]);
					if(numberOfExtraButton > 0)
					{
						strArrayButtonText	=	btnArray[1].Split(SEPARATOR);
						strArrayButtonId	=	btnArray[2].Split(SEPARATOR);
						//if(btnArray.Length>3 )
						strArrayFunctionText=   btnArray[3].Split(SEPARATOR);

						//if(unqId!="" && unqId!=null)
						//{
						//    tmpTree.Append("<TABLE style=\"display:none\" width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\n");
						// }
						// else
						// {
						tmpTree.Append("<TABLE width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\n");
						//}
                   
						//tmpTree.Append("<TR class=\"DataRow\">");
						tmpTree.Append("<TR class=\"" + strDataRowClass + "\">");
						tmpTree.Append("<TD align=\"left\" valign=\"middle\">");

						for(int buttonCounter=0; buttonCounter<numberOfExtraButton; buttonCounter++)
						{

							/*if(numberOfExtraButton==1)
							{
//								string secXML=Cms.CmsWeb.cmsbase.gstrSecurityXML;
								if(strArrayButtonText[buttonCounter] != "Save"||strArrayButtonText[buttonCounter] != "Add"||strArrayButtonText[buttonCounter] != "Add New" && strArrayButtonText[buttonCounter] != "Delete") 
								if(write=="Y")	
								{
									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");
									tmpTree.Append(strArrayFunctionText[buttonCounter].ToString() + "()" );
									tmpTree.Append("\">&nbsp;");
								}
								else if (delete=="Y")
								{
									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");
									tmpTree.Append(strArrayFunctionText[buttonCounter].ToString() + "()" );
									tmpTree.Append("\">&nbsp;");
								}
								else if (execute=="Y")
								{
									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");
									tmpTree.Append(strArrayFunctionText[buttonCounter].ToString() + "()" );
									tmpTree.Append("\">&nbsp;");
								}
							}
							else
							{*/
								if(strArrayButtonText[buttonCounter] == "Save"||strArrayButtonText[buttonCounter] == "Add"||strArrayButtonText[buttonCounter] == "Add New") 	
								{
									if(write=="Y")
									{
										tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");
										tmpTree.Append(strArrayFunctionText[buttonCounter].ToString() + "()" );
										tmpTree.Append("\">&nbsp;");
									}
								}

								else if (strArrayButtonText[buttonCounter] == "Delete")	
								{
									if(delete=="Y")
									{
										tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");
										tmpTree.Append(strArrayFunctionText[buttonCounter].ToString() + "()" );
										tmpTree.Append("\">&nbsp;");
									}
								}
								else
								{
									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");
									tmpTree.Append(strArrayFunctionText[buttonCounter].ToString() + "()" );
									tmpTree.Append("\">&nbsp;");
								}


							//}
//							if(write=="Y")	
//								tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");
//							else if(write=="N")
//								tmpTree.Append("&nbsp;<input type=\"button\" style=\"display:none\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");
//							else
//								tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");

//						}

/*							if(numberOfExtraButton==1)
							{
								string secXML=Cms.CmsWeb.cmsbase.gstrSecurityXML;
								if(write=="Y" ||delete=="Y"||execute=="Y")	
								{
									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[0] + "\" id=\"ExtraButton0" + "\"  onclick=\"");
									tmpTree.Append(strArrayFunctionText[0].ToString() + "()" );
									tmpTree.Append("\">&nbsp;");
								}
							}
							if(numberOfExtraButton==2)
							{
								if(write=="Y")	
									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[0] + "\" id=\"ExtraButton0" + "\"  onclick=\"");
								tmpTree.Append(strArrayFunctionText[0].ToString() + "()" );
								tmpTree.Append("\">&nbsp;");

								if(delete=="Y")	
									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[1] + "\" id=\"ExtraButton1" + "\"  onclick=\"");
								tmpTree.Append(strArrayFunctionText[1].ToString() + "()" );
								tmpTree.Append("\">&nbsp;");

							}*/
//							if(numberOfExtraButton>1)
//							{
//								if(write=="Y")	
//									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[0] + "\" id=\"ExtraButton0" + "\"  onclick=\"");
//								if(delete=="Y")	
//									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[1] + "\" id=\"ExtraButton1" + "\"  onclick=\"");
//								if(execute=="Y")	
//									tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[2] + "\" id=\"ExtraButton2" + "\"  onclick=\"");
//							}
//							else
//								tmpTree.Append("&nbsp;<input type=\"button\"  class=\"clsButton\" value=\"" + strArrayButtonText[buttonCounter] + "\" id=\"ExtraButton" +  buttonCounter + "\"  onclick=\"");


							

							
//							if(btnArray.Length>3 )
//								tmpTree.Append(strArrayFunctionText[buttonCounter].ToString() + "()" );
//							else
//								tmpTree.Append("AddRecord()");
//							

							

							//switch(buttonCounter)
							//{
							//   case 0 :
                                    
							//       break;
							//   case 1 :
							//tmpTree.Append("javascript:alert('Record could not be deleted');");
							//tmpTree.Append("javascript:return DeleteRow();");
							//       break;
							//   default :
							//       tmpTree.Append("");
							//       break;
							//}
								
//							tmpTree.Append("\">&nbsp;");
//						for(int buttonCounter=0; buttonCounter<numberOfExtraButton; buttonCounter++)
//						{
							if (buttonCounter == 4 || buttonCounter == 8)
							{
								tmpTree.Append("</TD>\n</TR>\n<TR>\n<TD align=\"left\" valign=\"middle\">");
							}
						}
						tmpTree.Append("</TD>\n</TR>\n</TABLE>\n");
					}
				}
				#endregion				

				
			}
			catch(Exception e)
			{
				if(e.InnerException!=null) 
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(e.InnerException);
					throw e.InnerException; 
				}
				else
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(e);				
					throw e;
				}
				//return "ERROR~" + e.Message;
			}
			finally
			{
				if (lobj_GridTable != null ) lobj_GridTable.Dispose() ;
				lobj_GridTable= null;
			}
			return tmpTree.ToString() + "^^#@" + xmlData + "^^#@" +  rowNum;
		}

		private DataSet GetExtraQuery(string sql)
		{
			return ExecuteInlineGridRecords(sql);

		}
		/// <summary>
		/// This function is used to truncate cell data according to column widt percentage given
		/// </summary>
		/// <param name="cellIndex">index of the column</param>
		/// <param name="sText">text of the cell</param>
		/// <returns>proper string</returns>
		private string TruncateText(int cellIndex,string sText)
		{
			string lStrText     = sText; // using this for storing cell text			
			string[] lArrHText  = strDisplayTextLen;  // using for assigning array containing column width percentage
         
			// if cell text is blank
			if(sText!="")
			{
				// if array has no subscript
				if(lArrHText.Length>0)
				{
					// if length of cell text is greater than column width (in percentage)
					if(sText.Length>=(int.Parse(lArrHText[cellIndex])))
					{
						//truncate till the column width length and add "..." and store in local variable
						lStrText=sText.Substring(0,int.Parse(lArrHText[cellIndex])) + "...";     
					}
				}                
			}
            
			return lStrText;

		}

		private void GridBinding(object sender,DataGridItemEventArgs e)
		{
			
			if (e.Item.ItemType  == ListItemType.Header)
			{
				headText=e.Item.Cells[0].Text;
			}

			//Ravindra(03-09-2006) 
			//Added new grouping code Z to enable grouping for Lable columns
			//LBL template is added for same and 
			//In binding UpdateHyperLink or UpdateLable is called based on the 
			//Template selected and grouping flag(i.e Y or Z)
			


			string queryString = "";
			if (e.Item.ItemType  == ListItemType.Item || e.Item.ItemType  == ListItemType.AlternatingItem)
			{
				#region GROUPING CODE
				if(grouping=="Y" || grouping == "Z")
				{
                                 
					if(colGrps!="")
					{
						string [] arrMajor= Server.UrlDecode(colGrps).Split(new char[]{INTERNALSEPARATOR});
						if(arrMajor.Length>0)
						{
							int iMaj=0;
							queryString="";
							for(iMaj=0;iMaj<arrMajor.Length;iMaj++)
							{
								string [] arrMinor=arrMajor[iMaj].Split(new char[]{SEPARATOR});
								if(arrMinor.Length>0)
								{
									int iMin=0;
									for(iMin=0;iMin<arrMinor.Length;iMin++)
									{
										if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains(arrMinor[iMin]) == true )
										{
											queryString+=arrMinor[iMin] + "=" + DataBinder.Eval(e.Item.DataItem,arrMinor[iMin]) + "&"  ;
										}
									}

									if(queryString.IndexOf("&")!=-1)
									{
										queryString = queryString.Substring(0,queryString.Length-1);
									}

									WebControl ctrl = null;
									if (e.Item.Cells[0].FindControl("hl_" + iMaj) != null)
									{
										ctrl =  (WebControl) e.Item.Cells[0].FindControl("hl_" + iMaj);
										ctrl.CssClass = "a1";
										ctrl.ForeColor=System.Drawing.Color.Black;
									}

									switch(iMaj)
									{
										case 0:
										{		
											if (ctrl is HyperLink)
											{
												UpdateCtrlHyperlink(ctrl,queryString, oldText, e, iMaj);
												oldText=queryString;
											}
											else if (ctrl is Label)
											{
												UpdateCtrlLabel(ctrl,queryString, oldText, e, iMaj);
												oldText = Convert.ToString(DataBinder.Eval(e.Item.DataItem,gArrColActualNames[iMaj]));
											}
											
											queryString="";
											break;
										}
										case 1:
										{
											if (ctrl is HyperLink)
											{
												UpdateCtrlHyperlink(ctrl,queryString, oldText1, e, iMaj);
												oldText1=queryString;
											}
											else if (ctrl is Label)
											{
												UpdateCtrlLabel(ctrl,queryString, oldText1, e, iMaj);
												oldText1 = Convert.ToString(DataBinder.Eval(e.Item.DataItem,gArrColActualNames[iMaj]));
											}
											
											queryString="";
											break;
										}
										case 2:
										{
											if (ctrl is HyperLink)
											{
												UpdateCtrlHyperlink(ctrl,queryString, oldText2, e, iMaj);
												oldText2=queryString;
											}
											else if (ctrl is Label)
											{
												UpdateCtrlLabel(ctrl,queryString, oldText2, e, iMaj);
												oldText2 = Convert.ToString(DataBinder.Eval(e.Item.DataItem,gArrColActualNames[iMaj]));
											}
											
											queryString="";
											break;
										}
										case 3:
										{
											if (ctrl is HyperLink)
											{
												UpdateCtrlHyperlink(ctrl,queryString, oldText3, e, iMaj);
												oldText3=queryString;
											}
											else if (ctrl is Label)
											{
												UpdateCtrlLabel(ctrl,queryString, oldText3, e, iMaj);
												oldText3 = Convert.ToString(DataBinder.Eval(e.Item.DataItem,gArrColActualNames[iMaj]));
											}
											
											queryString="";
											break;
										}
										case 4:
										{
											if (ctrl is HyperLink)
											{
												UpdateCtrlHyperlink(ctrl,queryString, oldText4, e, iMaj);
												oldText4=queryString;
											}
											else if (ctrl is Label)
											{
												UpdateCtrlLabel(ctrl,queryString, oldText4, e, iMaj);
												oldText4 = Convert.ToString(DataBinder.Eval(e.Item.DataItem,gArrColActualNames[iMaj]));
											}
											
											queryString="";
											break;
										}
									}//end of switch
								}//end if of arrMinor which is seperated by ~
							}//end of for of arrMajor which is seperated with ^
						}//end if of arrMajor
					}//end if of colGrps==""                   
				}// end if if of grouping=="y"          
				#endregion

				
				#region CHECKBOX
				/************************************************************/
				// code for check box    
				/************************************************************/
				if(reqCheckbox=="Y")
				{
					CheckBox ck=(CheckBox) e.Item.Cells[0].FindControl("ck_0");   

					if(ck!=null)
					{
						ck.ID="ck_" + rowId.ToString();
						//ck.Text="ck_" + rowId.ToString();     
						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("UniqueGrdID") == true )
						{
							ck.Attributes.Add("onClick","javascript:checkboxCheck();");
							e.Item.Attributes.Add("uniqueID",Convert.ToString(DataBinder.Eval(e.Item.DataItem,"UniqueGrdID")));
						}
						//ck.
					}
				}
				#endregion

				#region DROPDOWNLIST
				/************************************************************/
				// code for DropdownList    
				/************************************************************/
				//by pravesh change ddl_8 to ddl_9 as a new column (Change Eff. date) is added in Cuastomer Asst. Policy Tab
				DropDownList ddl=(DropDownList) e.Item.Cells[0].FindControl("ddl_9");   

				if(ddl!=null)
				{
					Label lbl = (Label) e.Item.Cells[0].FindControl("lbl_9");   

					ddl.ID="ddl_" + rowId.ToString();

					if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("policy_status") == true )
					{
						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("process_value") == true )
						{
							
							


							//Adding the process in drop down
							int intCustomer_ID= int.Parse(DataBinder.Eval(e.Item.DataItem,"customer_id").ToString());
							int intPolicy_ID=int.Parse(DataBinder.Eval(e.Item.DataItem,"policy_id").ToString());
							int intPolicy_Version_ID=int.Parse(DataBinder.Eval(e.Item.DataItem,"policy_version_id").ToString());
							int appVerId=int.Parse(DataBinder.Eval(e.Item.DataItem,"app_version_id").ToString());
							int appId=int.Parse(DataBinder.Eval(e.Item.DataItem,"app_id").ToString());
							int LOB_ID=int.Parse(DataBinder.Eval(e.Item.DataItem,"LOB_ID").ToString());

							HyperLink hlk = (HyperLink) e.Item.Cells[0].FindControl("hl_0"); 
							if (hlk != null)
							{
								
								if(queryString!=oldText)
								{
									//hlk.Text = DataBinder.Eval(e.Item.DataItem,"policy_number").ToString();
								}
								else
								{
									//hlk.Text="&nbsp;";
								}
								
								


								hlk.NavigateUrl += "&customer_id=" + intCustomer_ID.ToString() 
									+ "&Policy_id=" + intPolicy_ID.ToString() 
									+ "&policy_version_id=" + intPolicy_Version_ID.ToString()
									+ "&app_version_id=" + appVerId.ToString() 
									+ "&app_id=" + appVerId.ToString()
									+ "&lob_id=" + LOB_ID.ToString(); 

								hlk.CssClass = "midcolora";
								hlk.Target="botframe";

							}


							ClsCommon objClsCommon = new ClsCommon();
							DataSet ds =new DataSet();

							ds = objClsCommon.GetEligibleProcess(intCustomer_ID,intPolicy_ID,intPolicy_Version_ID);

							if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
							{
								ddl.DataSource =ds.Tables[0];
								ddl.DataTextField="Process_Desc";
								ddl.DataValueField="Process_ShortName";							
								ddl.DataBind();
								lbl.Visible = false;
							}
							else
							{
								ddl.Visible = false;
								lbl.Visible = true;
								lbl.Text = "N.A.";
								lbl.CssClass = "LabelFont";

								if (e.Item.Cells[0].FindControl("but_9") != null)
									((Button) e.Item.Cells[0].FindControl("but_9")).Visible = false; //by pravesh change but_8 to but_9 as a new column (Change Eff. date) is added in Cuastomer Asst. Policy Tab

							}

 
						}
						//if (Convert.ToString(DataBinder.Eval(e.Item.DataItem,"policy_status")).ToUpper()  == "SUSPENDED")
						//{
						//							ddl.Items.Add("ENDORSEMENT");		
						//							ddl.Items.Add("RENEWAL");		
						//							ddl.Items.Add("ISSUE POLICY");		
						//							ddl.Items.Add("REINSTATEMENT");		
						//							ddl.Items.Add("NON RENEWAL");		
						//							ddl.Items.Add("NEGATE");		
						//							ddl.Items.Add("CANCEL");		
						//}
						/*if (Convert.ToString(DataBinder.Eval(e.Item.DataItem,"policy_status")).ToUpper()  == "RENEWAL")
						{
							ddl.Items.Add("ENDORSEMENT");		
							ddl.Items.Add("RENEWAL");		
							ddl.Items.Add("ISSUE POLICY");		
							ddl.Items.Add("REINSTATEMENT");		
							ddl.Items.Add("NON RENEWAL");		
							ddl.Items.Add("NEGATE");		
							ddl.Items.Add("CANCEL");		
						}
						if (Convert.ToString(DataBinder.Eval(e.Item.DataItem,"policy_status")).ToUpper()  == "NORMAL")
						{
							ddl.Items.Add("ENDORSEMENT");		
							ddl.Items.Add("RENEWAL");		
							ddl.Items.Add("ISSUE POLICY");		
							ddl.Items.Add("REINSTATEMENT");		
							ddl.Items.Add("NON RENEWAL");		
							ddl.Items.Add("NEGATE");		
							ddl.Items.Add("CANCEL");		
						}*/
					}

					
					ddl.Items.Insert(0,"");  
					if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("UniqueGrdID") == true )
					{
						string unqID=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"UniqueGrdID")) + "&rowid=" + rowId.ToString();	
						ddl.Attributes.Add("onchange","dropdownChange('"+ unqID +"');");  								
					}		
				}
				#endregion
			
				#region BUTTON
				/************************************************************/
				// code for button
				// BUT_8 ID is used because this is 8th column on policy index page. and button's ID is assigned as
				// but_8 for every button
				/************************************************************/
				//by pravesh change but_9 to but_10 as a new column (Change Eff. date) is added in Cuastomer Asst. Policy Tab
				Button  but=(Button) e.Item.Cells[0].FindControl("but_10");   

				if(but!=null)
				{
					but.ID="but_" + rowId.ToString();
					
					if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("UniqueGrdID") == true )
					{
						string lob="",app_id="",appVersionId="";
						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("policy_lob") == true )
							lob=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"policy_lob"));

						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("app_id") == true )
							app_id=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"app_id"));

						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("app_version_id") == true )
							appVersionId=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"app_version_id"));
	
						string unqID=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"UniqueGrdID")) + "&rowid=" + rowId.ToString() + "&LOB=" + lob + "&app_id=" + app_id + "&app_version_id=" + appVersionId;	
						

						but.Attributes.Add("onclick","return button_Click('"+ unqID +"');");
					}
				}
				#endregion				

				/*****************************************************************/


				#region QUOTE IMAGE
				/************************************************************/
				// code for Image
				// BUT_8 ID is used because this is 8th column on policy index page. and button's ID is assigned as
				// but_8 for every button
				/************************************************************/
				//HyperLink hlk1 = (HyperLink) e.Item.Cells[0].FindControl("hl_7");				
				if(iImgColPos==-1 && strArrayColTypes!=null && strArrayColTypes.Length>0)				
				{
					for(int iTemp=0;iTemp<strArrayColTypes.Length;iTemp++)
					{
						if(strArrayColTypes[iTemp]!="" && strArrayColTypes[iTemp].ToString()!="" && strArrayColTypes[iTemp].ToString()=="IMGLNK")
						{
							iImgColPos = iTemp;							
							strImgLnkColName = "ImgLnk_" + iImgColPos.ToString();							
							break;
						}
						else
							iImgColPos = 0;
					}
				}
				HyperLink hlk1 = (HyperLink) e.Item.Cells[0].FindControl(strImgLnkColName);
				if(hlk1!=null)
				{
					hlk1.ID="hlk1_" + rowId.ToString();
					
					if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("UniqueGrdID") == true )
					{
						string lob="",app_id="",appVersionId="",customerid="";//,policy_id="",policy_version_id="";
						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("customer_id") == true )
							customerid=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"customer_id"));

						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("app_id") == true )
							app_id=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"app_id"));

						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("app_version_id") == true )
							appVersionId=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"app_version_id"));

						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("lob_id") == true )
							lob=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"lob_id"));

						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("quote_xml") == true )
							hlk1.Text=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"quote_xml"));

						string unqID=Convert.ToString(DataBinder.Eval(e.Item.DataItem,"UniqueGrdID")) ;//+ "&rowid=" + rowId.ToString() + "&LOB_id=" + lob + "&app_id=" + app_id + "&app_version_id=" + appVersionId + "&customer_id=" + customerid;	
						
						e.Item.HorizontalAlign=  HorizontalAlign.Center; 
						hlk1.Attributes.Add("onclick","return image_Click('"+ unqID +"');");
					}
				}
				#endregion				

				/*****************************************************************/

                
				if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains(filterColumn) == true )
				{
					if (Convert.ToString(DataBinder.Eval(e.Item.DataItem,filterColumn)).Trim() == "N")
						e.Item.ForeColor=System.Drawing.Color.Red;
				}
				
				//RPSINGH - 28 Aug 2006
				if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("VEHICLE_TYPE_TOOL_TIP") == true && ((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("VEHICLE_CLASS_TOOL_TIP") == true)
				{
					//Class
					e.Item.Cells[3].ToolTip = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"VEHICLE_CLASS_TOOL_TIP")).Trim();
					//Type
					e.Item.Cells[4].ToolTip = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"VEHICLE_TYPE_TOOL_TIP")).Trim();
				}
				//End of addition by RPSINGH


				//Swastika - start
				//Right Align Amt Field
				if(strRightAlignCols !="")
				{
					foreach (string str in Server.UrlDecode(strRightAlignCols).Split('^'))
					{
						e.Item.Cells[Convert.ToInt32(str)].HorizontalAlign = HorizontalAlign.Right;
					}
				}
				
				//code added by vj on 05-07-2006
				//Hard corded value for checking the Activity_Status column and change the color of row.
				//If the value is incomplete then change the color of the row.
				if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains("ACTIVITY_STATUS") == true )
				{
					if (Convert.ToString(DataBinder.Eval(e.Item.DataItem,"ACTIVITY_STATUS")).Trim().ToUpper() == "INCOMPLETE")
						e.Item.ForeColor=System.Drawing.Color.Red;
				}
				//end of coded added by vj on 05-07-2006

				//if(isUnq=="")
				string[] lArrPrimaryColsName=Server.UrlDecode(sPrimCol).Split(new Char[]{INTERNALSEPARATOR});
				string[] pkValues=Server.UrlDecode(insertRowId).Split(new Char[]{INTERNALSEPARATOR});
                
				if(insertRowId!="" && insertRowId!="0")
				{
					
					//code added by vj on 05-04-2006
						
					if (HttpContext.Current.Request.Cookies["newRowAddedFlag"] != null)
					{
						if (HttpContext.Current.Request.Cookies["newRowAddedFlag"].Value == "0")					
							HttpContext.Current.Response.Cookies["GridClickRowNumber"].Value = rowId.ToString();
					}
					//end of code added by vj on 05-04-2006

					
					int cntPK=0;                        
					int pCnt=0;
					for(pCnt=0;pCnt<lArrPrimaryColsName.Length;pCnt++)
					{
						string lstr=lArrPrimaryColsName[pCnt];                    
                        
						if(lstr.IndexOf(".")!=-1 )
							lstr=lstr.Substring(lstr.IndexOf(".")+1);

						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains(lstr) == true )
						{
							if (Convert.ToString(DataBinder.Eval(e.Item.DataItem,lstr)) == pkValues[pCnt].ToString())
							{
								cntPK+=1;                                
							}
						}
					}

					if(cntPK==lArrPrimaryColsName.Length)
					{
						// Previous code.
						// Commented by mohit on 10/11/2005.
						//---------------------------------
						//e.Item.ForeColor=System.Drawing.Color.FromArgb(0,102,204);
						//rowNum=rowId.ToString();
						//---------------------------------
						// End
						
						// added by mohit
						// if the record is Inactive than chnage fore color to red.
						if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains(filterColumn) == true )
						{
							if (Convert.ToString(DataBinder.Eval(e.Item.DataItem,filterColumn)).Trim() == "N")
							{
								e.Item.ForeColor=System.Drawing.Color.Red;
							}
							else
							{
								e.Item.ForeColor=System.Drawing.Color.FromArgb(0,102,204);
							}
						}	
						else
						{
							e.Item.ForeColor=System.Drawing.Color.FromArgb(0,102,204);
						}
						rowNum=rowId.ToString();

					}

				}
                
				/*if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains(gLPrimaryCol) == true )
				{
					if (Convert.ToString(DataBinder.Eval(e.Item.DataItem,gLPrimaryCol)) == insertRowId.ToString())
						e.Item.ForeColor=System.Drawing.Color.FromArgb(0,102,204) ;    
				  //  else 
					//    e.Item.ForeColor=System.Drawing.Color.Black; 
				}
				*/
				//int cellCnt=0;

				//for(cellCnt=0;cellCnt<e.Item.Cells.Count;cellCnt++)
				//  e.Item.Cells[cellCnt].Text=TruncateText(cellCnt,e.Item.Cells[cellCnt].Text);

				  
				e.Item.Attributes.Add("ID","Row" + "_" + rowId.ToString() ) ; 

				string pkValue = "";
				
				//May 05, 2005, Pradeep////////Output teh values of the primary keys as an attribute
				for(int i = 0; i<primaryKeyColumns.Length; i++ )
				{
					if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains(primaryKeyColumns[i]) == true )
					{
						if ( pkValue == "" )
						{
							pkValue = Convert.ToString(DataBinder.Eval(e.Item.DataItem,primaryKeyColumns[i]));
						}
						else
						{
							pkValue = pkValue + "," + Convert.ToString(DataBinder.Eval(e.Item.DataItem,primaryKeyColumns[i]));
						}
					}
				}
				///////////////

				if(allDblClick=="true")
				{
					//Parse  the look up info/////////////////////////////
					//LookupIDColName^LookupDescColName^LookupIDControlID^LookupDescControlID
					string[] arLookupInfo = strLookUpDetails.Split("^".ToCharArray());
				
					//Add attribute for double click, if look up parameters are present
					//JS function : function OnDoubleClick(TextFieldID,ValueFieldID,TextFieldValue,ValueFieldValue)
					if ( arLookupInfo.Length == 4 )
					{
						string strDataValue = Convert.ToString(DataBinder.Eval(e.Item.DataItem,arLookupInfo[0]));
						string strDataText = Convert.ToString(DataBinder.Eval(e.Item.DataItem,arLookupInfo[1]));
						string strJSFunction = "OnDoubleClick('" + arLookupInfo[3] + "','" + arLookupInfo[2] +
							"','" + strDataText + "','" + strDataValue + "')";
						e.Item.Attributes.Add("onDblClick",strJSFunction);
					}
					else
					{
						//e.Item.Attributes.Add("onDblClick","highlightrow(this)");
					}
				}
				
				e.Item.Attributes.Add("pkvalues",pkValue);
				//if(reqDropDownList!="Y")				
				if(reqNormalCursor=="" || reqNormalCursor.ToUpper()!="Y")
					e.Item.Attributes.Add("onClick","highlightrowClick(this)");
				//e.Item.Attributes.Add("onmouseover","changecursor(this)");
			}
			rowId++;
		}
		
		//Ravindra Gupta(03-09-2006)
		// Function To Update Lable Or Hyperlink based on column type(T  or LBL ) in case of grouping
		private void UpdateCtrlHyperlink(Control ctrl,string queryString,string oldText, DataGridItemEventArgs e,int iMaj)
		{
			if (ctrl == null)
				return;

			HyperLink hlk = (HyperLink) ctrl;
			if(hlk != null)
			{
				hlk.NavigateUrl+= queryString;

				if(queryString != oldText)
				{	
					hlk.Text=Convert.ToString(DataBinder.Eval(e.Item.DataItem,gArrColActualNames[iMaj]));
				}
				else
				{
					hlk.Text="&nbsp;";
				}
			}
		}

		private void UpdateCtrlLabel(Control ctrl,string queryString,string oldText, DataGridItemEventArgs e,int iMaj)
		{
			if (ctrl == null)
				return;

			Label lbl = (Label)ctrl;
			if(lbl != null)
			{
				if(Convert.ToString(DataBinder.Eval(e.Item.DataItem,gArrColActualNames[iMaj])) != oldText)
				{	
					lbl.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,gArrColActualNames[iMaj]));
				}
				else
				{
					lbl.Text="&nbsp;";
				}
			}
		}

		//Changes By Ravindra ends
		private string CreateQuery(string sQueryGroup, string sFilterGroup, bool bShowExcluded, string sSearchCriteria, string sDefaultSortCol, out string sFilterCol, out string sFilterLabel, out string sColName, out bool bAsc, out string sSearchCol, out string sSearchVal,out string sSQLQuery,string sSearchName,int iPageNo,string sPrimaryColsName,string sCacheSize,string uniqId,string lst,string strFilterValue,string searchType,string [] sSearchArrCol) 
		{
			// code added by Krishan
			string sDefaultSearchString="";			
			//string sISNormalSearch="Y";
			// end of code
			string sFilterClause = "";
			string sSearchClause = "";
			sColName = "";
			string sOrderClause = "";
			bAsc = true;
			sFilterCol = "";
			sFilterLabel = "";
			sSearchCol = "";
			sSearchVal = "";
			StringBuilder sSqlQuery = new StringBuilder();
			bool bWhereAdded = false;
			try 
			{
				//prepare the filter clause to be used in the WHERE clause of the query if filter group is non-empty and contains the SEPARATOR
				if (sFilterGroup != null && Server.UrlDecode(sFilterGroup.Replace("+", "%2B")).IndexOf(SEPARATOR) > 0)
				{
					string[] sArrFilterClauses = Server.UrlDecode(sFilterGroup.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
					if(bShowExcluded) sFilterClause = "";
					else 
					{
						if(strFilterValue.IndexOf("IS")==-1)
						{
							if(!isNumeric(strFilterValue))
								sFilterClause = "(" + sArrFilterClauses[0].Trim() +  "='" +  strFilterValue +   "')";
							else
								sFilterClause = "(" + sArrFilterClauses[0].Trim() +  "=" +  strFilterValue +   ")";
						}
						else
						{
							sFilterClause = "(" + sArrFilterClauses[0].Trim() +  strFilterValue.Replace("%20"," ") +   ")";
						}                
					}

                    
					//sFilterClause = "(UPPER(" + sArrFilterClauses[0].Trim() + ")='1')";

					sFilterCol = (sArrFilterClauses[0].IndexOf(".") > 0) ? sArrFilterClauses[0].Substring(sArrFilterClauses[0].IndexOf(".")+1).Trim() : sArrFilterClauses[0].Trim();
					filterColumn=sFilterCol ;
					sFilterLabel = sArrFilterClauses[1].Trim();
					sArrFilterClauses = null;
				}


				//array for data type of the column
				string[] sArrSearchType = Server.UrlDecode(searchType).Split(new char[]{INTERNALSEPARATOR}); //split the display col group string

				//prepare the search clause to be used in the WHERE clause of the query if search criteria is non-empty and contains the SEPARATOR
				if (sSearchCriteria != null && Server.UrlDecode(sSearchCriteria.Replace("+", "%2B")).IndexOf(SEPARATOR) > 0)
				{
					//code added by Krishan to store the data in a table.
					sDefaultSearchString=Server.UrlDecode(sSearchCriteria.Replace("+", "%2B"));
					
					string[] sArrSearchCriteria = Server.UrlDecode(sSearchCriteria.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
					if (sArrSearchCriteria.Length == 2) //only a basic search
					{
						sSearchCol = sArrSearchCriteria[0].Trim();
						sSearchVal = sArrSearchCriteria[1].Trim();
						int iSrchCol=0; // loop counter which traverse array of search columns name
						for(iSrchCol=0;iSrchCol<sSearchArrCol.Length;iSrchCol++)
							if(sSearchArrCol[iSrchCol].Replace("!","+").ToString().ToLower() == sSearchCol.Replace("!","+").ToString().ToLower())
								break;

						if(sArrSearchType.Length>0)
							if(sArrSearchType[iSrchCol]=="T")
								sSearchClause = "((" + sArrSearchCriteria[0].Replace("!","+").Trim() + ") LIKE ('%" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", "''") + "%'))";
							else if (sArrSearchType[iSrchCol]=="D")
							{
								string [] twodates = sArrSearchCriteria[1].Trim().Replace("'", "''").Split('-');
								if(twodates.Length > 1)
								{
                                    try { twodates[1] = Convert.ToDateTime(twodates[1]).AddDays(1).AddMilliseconds(-1).ToString(); }
                                    catch (Exception ex) { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
									sSearchClause = "(" + sArrSearchCriteria[0].Trim() + " >= '" + twodates[0]  + "' AND " + sArrSearchCriteria[0].Trim() + " <= '" + twodates[1]  + "')";
								}
							}
					}
					else //advanced search
					{
						sSearchClause = "(";
						for (int i = 0; i < sArrSearchCriteria.Length; i+=3) //last array element would always be empty i.e. "" (as specified from client side)
						{
							int iSrchCol=0; // loop counter which traverse array of search columns name
							for(iSrchCol=0;iSrchCol<sSearchArrCol.Length;iSrchCol++)
								if(sSearchArrCol[iSrchCol].Replace("!","+").ToString().ToLower() == sArrSearchCriteria[i].Replace("!","+").ToString().ToLower())
									break;

							if(sArrSearchType.Length>0)
								if(sArrSearchType[iSrchCol]=="T")
									sSearchClause += " " + sArrSearchCriteria[i].Replace("!","+").Trim() + " LIKE UPPER('%" + sArrSearchCriteria[i+1].Replace("!","+").Trim().Replace("'", "''") + "%') " + sArrSearchCriteria[i+2].Trim();
								else if(sArrSearchType[iSrchCol]=="D")
								{
									string [] twodates = sArrSearchCriteria[i+1].Trim().Replace("'", "''").Split('-');
									if(twodates.Length > 1)
									{
										try { twodates[1] = Convert.ToDateTime(twodates[1]).AddDays(1).AddMilliseconds(-1).ToString(); }
                                        catch (Exception ex) { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
										sSearchClause = "(" + sArrSearchCriteria[i].Trim() + " >= '" + twodates[0]  + "' AND " + sArrSearchCriteria[i].Trim() + " <= '" + twodates[1]  + "')" + sArrSearchCriteria[i+2].Trim();
									}
								}

						}
						sSearchClause += ")";
					}
					sArrSearchCriteria = null;
				}
				
				sSQLQuery="";
				int lIntMainCount=0;
				string lstr=null,lStrCol;

				string[] lArrPrimaryColsName=Server.UrlDecode(sPrimaryColsName).Split(new Char[]{INTERNALSEPARATOR});
				//                for(lIntMainCount=0;lIntMainCount < lArrPrimaryColsName.GetLength(0); lIntMainCount++)
				//                {
				//                    if(lIntMainCount==0)
				//                    {
				//                        lstr=lArrPrimaryColsName[lIntMainCount];                    
				//
				//                        if(lstr.IndexOf(".")!=-1 )
				//                            gLPrimaryCol=lstr.Substring(lstr.IndexOf(".")+1);
				//                        else
				//                            gLPrimaryCol=lstr;
				//                        break;
				//                    }
				//                }
				// gLPrimaryCol=lstr;
				//prepare the ORDER BY clause
				sQueryGroup = DecryptData(sQueryGroup.Trim());
				string[] sArrQueryClauses = Server.UrlDecode(sQueryGroup.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
				if (sArrQueryClauses[5] != null && sArrQueryClauses[5].Trim() != "") //Get the non-empty ORDER BY clause from the query group
				{
					sOrderClause = sArrQueryClauses[5].Trim();
					string[] sArrOrderCriteria = sArrQueryClauses[5].Split(new char[]{','});
					sColName = sArrOrderCriteria[0].Substring(0, sArrOrderCriteria[0].IndexOf(" ")).Trim(); //return the first column name of the ORDER BY clause
					bAsc = (sArrOrderCriteria[0].Substring(sArrOrderCriteria[0].IndexOf(" ")).Trim().ToUpper() == "ASC") ? true : false; //return the sort order of the first column name
					sArrOrderCriteria = null;
				}
				else //use the default sort column no (assumed in the calling function) to sort the result set
				{
					sOrderClause = lstr + " ASC";
					sColName = lstr; //return the first column no
				}
				if(gobjHashTab.ContainsKey(sColName) && gobjHashTab[sColName].ToString().Trim() != "") 
				{
					sOrderClause = sOrderClause.Replace(sColName, gobjHashTab[sColName].ToString());
				}

				//To make the Grid UniqueID Column for the Hyperlink By Bhusingh on 14th Jan 2004
				
				StringBuilder lStrGrdUniqueId=new StringBuilder();
				
				for(lIntMainCount=0;lIntMainCount < lArrPrimaryColsName.GetLength(0); lIntMainCount++)
				{
					lstr=lArrPrimaryColsName[lIntMainCount];
					if(lstr.Length>0)
					{
						if(lstr.ToLower().IndexOf("convert",0)!=-1)
						{
							lStrCol=lstr.Split(',')[1];
							lStrCol=lStrCol.Replace(")","");
							if(lStrCol.IndexOf(".",0)!=-1)lStrCol=lStrCol.Substring(lStrCol.IndexOf(".",0)+1,lStrCol.Length-lStrCol.IndexOf(".",0)-1);
							
							if(lStrGrdUniqueId.Length==0)
								lStrGrdUniqueId.Append("'"+lStrCol+"='+replace("+lArrPrimaryColsName[lIntMainCount]+ ",'/','~')");
							else
								lStrGrdUniqueId.Append("+'&"+lStrCol+"='+replace("+lArrPrimaryColsName[lIntMainCount]+ ",'/','~')");
						}
						else
						{
							if(lstr.IndexOf(".",0)!=-1)lstr=lstr.Substring(lstr.IndexOf(".",0)+1,lstr.Length-lstr.IndexOf(".",0)-1);
							if(lStrGrdUniqueId.Length==0)
								lStrGrdUniqueId.Append("'"+lstr+"='+cast("+lArrPrimaryColsName[lIntMainCount]+ " as varchar(8000))");
							else
								lStrGrdUniqueId.Append("+'&"+lstr+"='+cast("+lArrPrimaryColsName[lIntMainCount]+ " as varchar(8000))");
						}
						//						if(lstr.IndexOf(".",0)!=-1)lstr=lstr.Substring(lstr.IndexOf(".",0)+1,lstr.Length-lstr.IndexOf(".",0)-1);
						//						if(lStrGrdUniqueId.Length==0)
						//							lStrGrdUniqueId.Append("'"+lstr+"='+cast(replace("+lArrPrimaryColsName[lIntMainCount]+ ",'/','~') as varchar)");
						//						else
						//							lStrGrdUniqueId.Append("+'&"+lstr+"='+cast(replace("+lArrPrimaryColsName[lIntMainCount]+ ",'/','~') as varchar)");
					}
				}
				if(lStrGrdUniqueId.Length!=0) lStrGrdUniqueId.Append(" as UniqueGrdId");
				if(lstr!=null)lstr=null;
				//-------------------------------------------------------------------------------


				//prepare the query using the clauses prepared above and query group
				if(lStrGrdUniqueId.Length>0)//sStrGrdUniqueId is added by Bhusing on 14 Jan 2004 for Unique Grid Id
					sSqlQuery.Append("SELECT top " + sCacheSize + " " + sArrQueryClauses[0].Trim() + ","+lStrGrdUniqueId+ " FROM " + sArrQueryClauses[1].Trim());
				else
					sSqlQuery.Append("SELECT top " + sCacheSize + " " + sArrQueryClauses[0].Trim() + " FROM " + sArrQueryClauses[1].Trim());
				
				if (sArrQueryClauses[2] != null && sArrQueryClauses[2].Trim() != "") 
				{
					sSqlQuery.Append(" WHERE " + sArrQueryClauses[2].Trim());
					bWhereAdded = true;
				}

				if (sFilterClause != "" && bWhereAdded)
					sSqlQuery.Append(" AND " + sFilterClause);
				else if(sFilterClause != "") 
				{
					sSqlQuery.Append(" WHERE " + sFilterClause);
					bWhereAdded = true;
				}                

				if (sSearchClause != "" && bWhereAdded)
				{
					sSqlQuery.Append(" AND " + sSearchClause);               
				}
				else if(sSearchClause != "") 
				{
					sSqlQuery.Append(" WHERE " + sSearchClause);
					bWhereAdded = true;
				}
                
                
				if(sSqlQuery.ToString().IndexOf("WHERE",0)!=-1 )
				{
					if(uniqId!="" && uniqId!=null)
					{
						sSqlQuery.Append(" and " + uniqId + "=" + int.Parse(lst) );
					}
				}
				else
				{
					if(uniqId!="" && uniqId!=null)
					{                        
						sSqlQuery.Append(" where " + uniqId + "=" + int.Parse(lst) );
					}
				}
                                            

				if (sArrQueryClauses[3] != null && sArrQueryClauses[3].Trim() != "") 
					sSqlQuery.Append(" GROUP BY " + sArrQueryClauses[3].Trim());

				if (sArrQueryClauses[4] != null && sArrQueryClauses[4].Trim() != "") 
					sSqlQuery.Append(" HAVING " + sArrQueryClauses[4].Trim());

				if (sOrderClause != "")
					sSqlQuery.Append(" ORDER BY " + sOrderClause);

		
				string lstrFromClause=sArrQueryClauses[0];
				string lstrRep="";
				string lstrSQL=sSqlQuery.ToString();

				if(lStrGrdUniqueId.Length>0) lstrSQL=lstrSQL.Replace(","+lStrGrdUniqueId,"");
				lstrSQL=lstrSQL.Replace(lstrFromClause," count(1) ");
				lstrRep="ORDER BY "+sOrderClause;
				lstrSQL=lstrSQL.Replace(lstrRep,"");
				sSqlQuery.Append(";"+lstrSQL);
				//-------------------------------------------------------------------------
			}
			catch(Exception oEx) 
			{
				throw oEx;
			}

			return sSqlQuery.ToString();
		}
		/// <summary>
		///	To decryption of data to avoid any manipulation in data if this Control called from outside the application
		/// </summary>
		/// <param name="pStrData"></param>
		/// <returns> decrypted data string </returns>
		private string DecryptData(string pStrData)
		{
			string data = null;
			//data = System.Web.HttpUtility.UrlDecode(pStrData.Trim());
			pStrData = pStrData.Replace("%3D","=");
			data = cmsbase.DecryptMessage(pStrData.Trim());
			return data;
		}
		
		private string CreateAdvSearch(string[] sArrSearchCols, string[] sArrSearchHeads, string sSearchCriteria, int iRowNos) 
		{
			StringBuilder tmpSearch = new StringBuilder();
			string[] sTmpArray = Server.UrlDecode(sSearchCriteria.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
			string[] sArrSearchCriteria = new string[iRowNos*3];
			for(int i=0;i<sTmpArray.Length; i++)
				sArrSearchCriteria[i] = (sTmpArray[i] == null ? "" : sTmpArray[i]);
			for(int i=sTmpArray.Length; i<sArrSearchCriteria.Length; i++)
				sArrSearchCriteria[i] = "";
			sTmpArray = null;
			tmpSearch.Append("<TABLE width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\n");
			tmpSearch.Append("<TR class=\"SearchRow\"><TD colspan=\"5\" height=\"1\" background=\"" + IMAGESPATH + "dot_line.gif\"></TD></TR>\n");
			tmpSearch.Append("<TR class=\"SearchRow\"><TD colspan=\"5\" height=\"5\"></TD></TR>\n");
			
			for (int i=1; i<=iRowNos; i++) 
			{
				if(i==1)
				{
					tmpSearch.Append("<TR class=\"SearchRow\"><TD width=10%>&nbsp;</TD><TD width=20% align=\"right\"  NOWRAP class=formrow1><b>" + gstrSearchOptionTxt + ":&nbsp;</font></b></TD>\n");
					 
					if(flagDropdown==1)
						tmpSearch.Append("<TD width=10% align=\"right\"><select id=\"AdvSearchCols\" onchange=\"_bdgPopulateSearch();ShowCalendar();\" class=\"SearchList\">");
					else
						tmpSearch.Append("<TD width=10% align=\"right\"><select id=\"AdvSearchCols\" class=\"SearchList\" onChange=\"_bdgPopulateSearch();ShowCalendar();\">");
					for(int k=0; k<sArrSearchCols.Length; k++) 
					{
						tmpSearch.Append("<option value=\""+ sArrSearchCols[k].Trim() +"\"");
						if (sArrSearchCriteria[(i-1)*3].Trim().Equals(sArrSearchCols[k].Trim())) tmpSearch.Append(" SELECTED");
						tmpSearch.Append(">"+ sArrSearchHeads[k].Trim() +"</option>\n");
					}
					tmpSearch.Append("</select></TD>\n");	
					tmpSearch.Append("<TD align=\"center\" class=formrow1 NOWRAP><b>" + sSearchCritTxt + ":&nbsp;</font></b></TD>\n");
					if(Server.HtmlEncode(sArrSearchCriteria[(i-1)*3 + 1])!="*#)(")
						tmpSearch.Append("<TD NOWRAP><span id=\"_bdgAdvSearchDiv\">&nbsp;<input type=\"text\" id=\"AdvSearchVals\" size=\"15\" onBlur=\"ChangeDate()\" class=\"SearchText\" value=\"" + Server.HtmlEncode(sArrSearchCriteria[(i-1)*3 + 1]).Trim() + "\"></span></TD>\n");
					else
						tmpSearch.Append("<TD NOWRAP><span id=\"_bdgAdvSearchDiv\">&nbsp;<input type=\"text\" id=\"AdvSearchVals\" size=\"15\" onBlur=\"ChangeDate()\" class=\"SearchText\" value=''></span></TD>\n");
					            			
				}
				else
				{
					tmpSearch.Append("<TR class=\"SearchRow\"><TD width=10%>&nbsp;</TD><TD width=20% align=\"right\" NOWRAP class=formrow1>&nbsp;</td><TD width=12% align=\"right\">&nbsp;");
					if(flagDropdown==1)
						tmpSearch.Append("<select id=\"AdvSearchCols\" class=\"SearchList\" onchange=\"_bdgPopulateSearch();ShowCalendar();\">");
					else
						tmpSearch.Append("<select id=\"AdvSearchCols\" class=\"SearchList\" onChange=\"_bdgPopulateSearch();ShowCalendar();\" >");

					for(int k=0; k<sArrSearchCols.Length; k++) 
					{
						tmpSearch.Append("<option value=\""+ sArrSearchCols[k].Trim() +"\"");
						if (sArrSearchCriteria[(i-1)*3].Trim().Equals(sArrSearchCols[k].Trim())) tmpSearch.Append(" SELECTED");
						tmpSearch.Append(">"+ sArrSearchHeads[k].Trim() +"</option>\n");
					}
					tmpSearch.Append("</select></TD>\n<TD align=\"center\" class=formrow1 NOWRAP>&nbsp;</td>");
					if(Server.HtmlEncode(sArrSearchCriteria[(i-1)*3 + 1])!="*#)(")
						tmpSearch.Append("<TD NOWRAP><span id=\"_bdgAdvSearchDiv\">&nbsp;<input type=\"text\" id=\"AdvSearchVals\" size=\"15\" onBlur=\"ChangeDate()\" class=\"SearchText\" value=\"" + Server.HtmlEncode(sArrSearchCriteria[(i-1)*3 + 1]).Trim() + "\"></span></TD>\n");
					else
						tmpSearch.Append("<TD NOWRAP><span id=\"_bdgAdvSearchDiv\">&nbsp;<input type=\"text\" id=\"AdvSearchVals\" size=\"15\" onBlur=\"ChangeDate()\" class=\"SearchText\" value=''></span></TD>\n");
				}
				
				if (i < iRowNos) 
				{
					tmpSearch.Append("<TD align=\"right\" ><select id=\"AdvSearchOpt\" class=\"SearchList\">");
					tmpSearch.Append("<option value=\"AND\"" + (sArrSearchCriteria[(i-1)*3+2].Trim().Equals("AND") ? "SELECTED" : " ") + ">" + sItemAnd + "</option>");
					tmpSearch.Append("<option value=\"OR\"" + (sArrSearchCriteria[(i-1)*3+2].Trim().Equals("OR") ? "SELECTED" : " ") + ">" + sItemOr + "</option>");
					tmpSearch.Append("</select>");
					tmpSearch.Append("</TD>\n");
					if(i==1)
					{
						tmpSearch.Append("<TD align=\"left\" width=10%>&nbsp;<input type=\"button\" class=\"clsButton\" value=\"" + gstrLblSearchBtn + "\" id=\"AdvSearchButton\" onclick=\"initSearch(1, true);\"></TD>\n");
						tmpSearch.Append("<TD align=\"left\" width=10%>&nbsp;<input type=\"button\" class=\"clsButton\" value=\"" + gstrLblBasicSearchBtn + " \"  onclick=\"getDefaultPage(1, false);\" size=15></TD>\n");
					}
					else
					{
						tmpSearch.Append("<TD align=\"left\" width=10%>&nbsp;</TD>\n");
						tmpSearch.Append("<TD align=\"left\" width=10%>&nbsp;</TD>\n");
					
					}
				}
				tmpSearch.Append("<TD width=7%>&nbsp;</TD><TD width=10%>&nbsp;</TD></TR>\n");
			}
			tmpSearch.Append("<TR class=\"SearchRow\"><TD colspan=\"5\" height=\"5\"></TD></TR>\n");
			tmpSearch.Append("<TR class=\"SearchRow\"><TD colspan=\"5\" height=\"1\" background=\"" + IMAGESPATH + "dot_line.gif\"></TD></TR>\n");
			tmpSearch.Append("<TR class=\"SearchRow\"><TD colspan=\"5\" height=\"5\"></TD></TR>\n</TABLE>\n");
			sArrSearchCriteria = null;
			return tmpSearch.ToString();
		}
	
		/// <summary>
		/// To check a value its a integer or not
		/// </summary>
		/// <param name="pObj">object containing value</param>
		/// <returns>bool</returns>
		public bool isNumeric(object pObj)
		{
			bool lBool = false;
			try
			{
				double ldblOut = 0;
				// check if it is valid int value
				System.Globalization.NumberFormatInfo lObjProvider = new System.Globalization.NumberFormatInfo();
				lBool = double.TryParse(pObj.ToString(), System.Globalization.NumberStyles.Integer, lObjProvider ,out ldblOut);
			}
			catch(Exception lEx)
			{
				throw lEx;
			}
			return lBool;
		}

		/// <summary>
		/// executing sql query for grid records.
		/// </summary>
		/// <param name="pStrSQL">sql query as a string</param>
		/// <param name="pIntStartRecord">record pointer from where the record has to be fetched</param>
		/// <param name="pIntPageSize">number of records to be fetched</param>
		/// <param name="totalRec">total record count</param>
		/// <param name="pageNum">number of the page</param>
		/// <returns>Dataset contianing records</returns>
		public DataSet getGridRecords(string pStrSQL,int pIntStartRecord,int pIntPageSize,out int totalRec,out int pageNum,string sPrimaryCols)
		{
			DataSet ldsUser;
			ClsCommon lobj_basedata = new ClsCommon();  
			int totRec=-1;
			try
			{               				
				//ldsUser=lobj_basedata.ExecuteInlineGridRecords(pStrSQL,pIntStartRecord,pIntPageSize,out totRec,out pageNum);
				ldsUser=ExecuteInlineGridRecords(pStrSQL,pIntStartRecord,pIntPageSize,out totRec,out pageNum,sPrimaryCols);
				totalRec=totRec;
			}
			catch(Exception lEx)
			{
				throw lEx;
			}
			finally
			{
				
			}
			return ldsUser;

		}

		public DataTable ExecuteInlineQuery(string pStrSQL)
		{
			DataTable ldt=new DataTable();
			SqlConnection lobjCon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"].ToString());
			SqlCommand lobjCmd=new SqlCommand();
			SqlDataAdapter lobjDA=new SqlDataAdapter();
			try
			{
				lobjCon.Open();
				lobjCmd.Connection=lobjCon;
				lobjCmd.CommandText=pStrSQL;
				lobjCmd.CommandTimeout = DefaultCommandTimeOut;
				lobjCmd.CommandType=CommandType.Text;
				lobjDA.SelectCommand=lobjCmd;

				lobjDA.Fill(ldt);
				lobjCon.Close();
			}
			catch(Exception lEx)
			{
				throw lEx;
			}
			finally
			{
				if(lobjDA!=null)lobjDA.Dispose();
				if(lobjCmd!=null)lobjCmd.Dispose();
				if(lobjCon!=null)lobjCon.Dispose();
			}
			return ldt; 
		}
		
		public DataSet ExecuteInlineGridRecords(string pStrSQL,int pIntStartRecord,int pIntPageSize,out int totRec,out int pageNum,string sPrimaryCols)
		{
			if (pStrSQL.IndexOf("DISTINCT") >0)
			{
				pStrSQL = pStrSQL.Replace("DISTINCT","");
				pStrSQL = pStrSQL.Replace("SELECT","SELECT DISTINCT");
			}
			DataSet lds=new DataSet();
			DataSet lds1=new DataSet();

			SqlConnection lobjCon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"].ToString());
			SqlCommand lobjCmd=new SqlCommand();
			SqlDataAdapter lobjDA=new SqlDataAdapter();
			try
			{
				
				lobjCon.Open();
				lobjCmd.Connection=lobjCon;
				lobjCmd.CommandTimeout = DefaultCommandTimeOut;
				lobjCmd.CommandText=pStrSQL;
				lobjCmd.CommandType=CommandType.Text;
				lobjDA.SelectCommand=lobjCmd;

				lobjDA.Fill(lds1,"temp1");

				int iLoop=0; // loop counter
				string[] lArrPrimaryColsName=Server.UrlDecode(sPrimaryCols).Split(new Char[]{INTERNALSEPARATOR});
				string[] pkValues=Server.UrlDecode(insertRowId).Split(new Char[]{INTERNALSEPARATOR});
                
				if(insertRowId!="" && insertRowId!="0")
				{
					int foundRow=0; //using to store row number of the primary key given
					int cntPK=0;
					for(iLoop=0;iLoop<lds1.Tables[0].Rows.Count;iLoop++)
					{
						int pCnt=0;
						for(pCnt=0;pCnt<lArrPrimaryColsName.Length;pCnt++)
						{
							string lstr=lArrPrimaryColsName[pCnt];                    
                            
							if(lstr.IndexOf(".")!=-1 )
								lstr=lstr.Substring(lstr.IndexOf(".")+1);
                            
							if(lds1.Tables[0].Rows[iLoop][lstr].ToString()==pkValues[pCnt].ToString())
							{
								cntPK+=1;    
							}
						}
                        
						foundRow+=1;
						if(cntPK==lArrPrimaryColsName.Length)
						{
							break; // all the primary key are matched because foundRow 
						}
						cntPK=0;
                        
						/*if(lds1.Tables[0].Rows[iLoop][gLPrimaryCol].ToString()==insertRowId.ToString())
						{
							foundRow=iLoop + 1;
							break;
						}*/
					}

					int quotient=0; // to store quotient of the division
					int remainder=0; // to store remainder of the division

					if(foundRow!=-1)
					{
						quotient=foundRow / pIntPageSize;
						remainder=foundRow % pIntPageSize;

						if(quotient==0)
						{
							pIntStartRecord=quotient;
						}
						if(quotient>0)
						{
							if(remainder==0)
							{
								pIntStartRecord=pIntPageSize * (quotient-1);
							}
							else if(remainder>0)
							{
								pIntStartRecord=pIntPageSize * quotient;
							}
						}                    
					}                  
				}
				if(pIntStartRecord>0)
				{
					pageNum=(pIntStartRecord / pIntPageSize)+1;
				}
				else
					pageNum=1;

				//commented and added by vj on 12-01-2006
				//-----Start ---------
				//lobjDA.Fill(lds,pIntStartRecord,pIntPageSize,"temp");				

				lds = new DataSet();
				DataTable dt = new DataTable();	
				dt = lds1.Tables[0].Clone();	
				lds.Tables.Add(dt);	
				lds.Tables[0].TableName = "temp";
				int upToRecords = 0;

				upToRecords = pIntStartRecord + pIntPageSize;
				for (int rowCount = 0;rowCount < lds1.Tables[0].Rows.Count; rowCount++)
				{
					if (rowCount >= pIntStartRecord && rowCount <= upToRecords)
					{
						DataRow dr = lds.Tables[0].NewRow();

						for (int colCount = 0;colCount < lds1.Tables[0].Columns.Count;colCount++)
						{
							dr[colCount] = lds1.Tables[0].Rows[rowCount][colCount];
						}

						lds.Tables[0].Rows.Add(dr);
					}
				}

				//-------End---------
				totRec=lds1.Tables[0].Rows.Count;   
				lobjCon.Close();
			}
			catch(Exception lEx)
			{
				throw lEx;
			}
			finally
			{
				if(lobjDA!=null)lobjDA.Dispose();
				if(lobjCmd!=null)lobjCmd.Dispose();
				if(lobjCon!=null)lobjCon.Dispose();
			}
			return lds; 
		}

		private string CreateGroupQuery(string sQueryGroup, string sAutoOrderClause, string sFilterGroup, bool bShowExcluded, string sSearchCriteria, string sDefaultSortCol, out string sFilterCol, out string sFilterLabel, out string sColName, out bool bAsc, out string sSearchCol, out string sSearchVal,out string sSQLQuery,string sSearchName,int iPageNo,string sPrimaryColsName,string sCacheSize,string uniqId,string lst,string strFilterValue,string searchType,string [] sSearchArrCol,string totrecords,string iPageSize) 
		{
			// code added by Krishan
			string sDefaultSearchString="";			
			//string sISNormalSearch="Y";
			// end of code
			string sFilterClause = "";
			string sSearchClause = "";
			sColName = "";
			string sOrderClause = "";
			bAsc = true;
			sFilterCol = "";
			sFilterLabel = "";
			sSearchCol = "";
			sSearchVal = "";
			StringBuilder sSqlQuery = new StringBuilder();
			bool bWhereAdded = false;
			
			try 
			{
				//prepare the filter clause to be used in the WHERE clause of the query if filter group is non-empty and contains the SEPARATOR
				if (sFilterGroup != null && Server.UrlDecode(sFilterGroup.Replace("+", "%2B")).IndexOf(SEPARATOR) > 0)
				{
					string[] sArrFilterClauses = Server.UrlDecode(sFilterGroup.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
					if(bShowExcluded) sFilterClause = "";
					else 
					{
						if(strFilterValue.IndexOf("IS")==-1)
						{
							if(!isNumeric(strFilterValue))
								sFilterClause = "(" + sArrFilterClauses[0].Trim() +  "='" +  strFilterValue +   "')";
							else
								sFilterClause = "(" + sArrFilterClauses[0].Trim() +  "=" +  strFilterValue +   ")";
						}
						else
						{
							sFilterClause = "(" + sArrFilterClauses[0].Trim() +  strFilterValue.Replace("%20"," ") +   ")";
						}                
					}

                    
					//sFilterClause = "(UPPER(" + sArrFilterClauses[0].Trim() + ")='1')";

					sFilterCol = (sArrFilterClauses[0].IndexOf(".") > 0) ? sArrFilterClauses[0].Substring(sArrFilterClauses[0].IndexOf(".")+1).Trim() : sArrFilterClauses[0].Trim();
					filterColumn=sFilterCol ;
					sFilterLabel = sArrFilterClauses[1].Trim();
					sArrFilterClauses = null;
				}


				//array for data type of the column
				string[] sArrSearchType = Server.UrlDecode(searchType).Split(new char[]{INTERNALSEPARATOR}); //split the display col group string
				
				//prepare the search clause to be used in the WHERE clause of the query if search criteria is non-empty and contains the SEPARATOR
				if (sSearchCriteria != null && Server.UrlDecode(sSearchCriteria.Replace("+", "%2B")).IndexOf(SEPARATOR) > 0)
				{
					//code added by Krishan to store the data in a table.
					sDefaultSearchString=Server.UrlDecode(sSearchCriteria.Replace("+", "%2B"));
					
					string[] sArrSearchCriteria = Server.UrlDecode(sSearchCriteria.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
					if (sArrSearchCriteria.Length == 2) //only a basic search
					{
						sSearchCol = sArrSearchCriteria[0].Trim();
						sSearchVal = sArrSearchCriteria[1].Trim();
						int iSrchCol=0; // loop counter which traverse array of search columns name
						for(iSrchCol=0;iSrchCol<sSearchArrCol.Length;iSrchCol++)
							if(sSearchArrCol[iSrchCol].Replace("!","+").ToString().ToLower().Trim()  == sSearchCol.Replace("!","+").ToString().ToLower().Trim() )
								break;

						if(sArrSearchType.Length>0)
						{
							if(sArrSearchType[iSrchCol]=="T" || sArrSearchType[iSrchCol]=="P")
								//Asfa (21-Apr-2008) - iTrack issue #4065 - In claim checks Void or Print, search does not work if amount is in 1000s.
								//sSearchClause = "((REPLACE(" + sArrSearchCriteria[0].Replace("!","+").Trim() + ",' ','') LIKE ('%" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", "''").Replace(" ","") + "%')))";
								sSearchClause = "((REPLACE(" + sArrSearchCriteria[0].Replace("!","+").Trim() + ",' ','') LIKE ('%" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", "''").Replace(" ","") + "%')) OR (REPLACE(" + sArrSearchCriteria[0].Replace("!","+").Trim() + ",' ','') LIKE REPLACE('%" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", "''").Replace(" ","") + "%',',','')))";
							else if (sArrSearchType[iSrchCol]=="D")
							{
								string [] twodates = sArrSearchCriteria[1].Trim().Replace("'", "''").Split('-');
								if(twodates.Length > 1)
								{
									try { twodates[1] = Convert.ToDateTime(twodates[1]).AddDays(1).AddMilliseconds(-1).ToString(); }
                                    catch (Exception ex) { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
									sSearchClause = "((" + sArrSearchCriteria[0].Trim() + " >= '" + twodates[0]  + "' AND " + sArrSearchCriteria[0].Trim() + " <= '" + twodates[1]  + "'))";
								}
							}
							else if(sArrSearchType[iSrchCol]=="L")
							{
								if(sArrSearchCriteria[1].Replace(" ","") !="")
									sSearchClause = "((" + sArrSearchCriteria[0].Replace("!","+").Trim() + ") = (" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", " ")+ "))";
							}
							else if(sArrSearchType[iSrchCol]=="LT")
							{
								if(sArrSearchCriteria[1].Replace(" ","") !="")
									sSearchClause = "((" + sArrSearchCriteria[0].Replace("!","+").Trim() + ") <= (" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", " ")+ "))";
							}
							else if(sArrSearchType[iSrchCol]=="Tx")
							{
								sSearchClause = "((" + sArrSearchCriteria[0].Replace("!","+").Trim() + " LIKE ('%" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", "''") + "%')))";
							}
							//Added by Mohit Agarwal 29-Oct-08 ITrack 4953
							if(sArrSearchCriteria[0].StartsWith("$DECRYPT"))
							{
								sSearchClause = "";
								decrypt_val = sArrSearchCriteria[1];
								string []decrypt_arr = sArrSearchCriteria[0].Split('_');

								if(decrypt_arr.Length > 1)
								{
									decrypt_position = int.Parse(decrypt_arr[1]);
								}
							}
						}
					}
					else //advanced search
					{
						sSearchClause = "(";
						for (int i = 0; i < sArrSearchCriteria.Length; i+=3) //last array element would always be empty i.e. "" (as specified from client side)
						{
							int iSrchCol=0; // loop counter which traverse array of search columns name
							for(iSrchCol=0;iSrchCol<sSearchArrCol.Length;iSrchCol++)
								if(sSearchArrCol[iSrchCol].Replace("!","+").ToString().ToLower() == sArrSearchCriteria[i].Replace("!","+").ToString().ToLower())
									break;

							//Added by Mohit Agarwal 29-Oct-08 ITrack 4953
							if(sArrSearchCriteria[i].StartsWith("$DECRYPT"))
							{
								decrypt_val = sArrSearchCriteria[i+1];
								string []decrypt_arr = sArrSearchCriteria[i].Split('_');

								if(decrypt_arr.Length > 1)
								{
									decrypt_position = int.Parse(decrypt_arr[1]);
								}
								sSearchClause += "(" + 1 + " = " + 1 + ")" + sArrSearchCriteria[i+2].Trim();

								continue;
							}

							if(sArrSearchType.Length>0)
								if(sArrSearchType[iSrchCol]=="T" || sArrSearchType[iSrchCol]=="T")
									//Asfa (21-Apr-2008) - iTrack issue #4065 - In claim checks Void or Print, search does not work if amount is in 1000s.
									//sSearchClause = "((REPLACE(" + sArrSearchCriteria[0].Replace("!","+").Trim() + ",' ','') LIKE ('%" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", "''").Replace(" ","") + "%')) OR (REPLACE(" + sArrSearchCriteria[0].Replace("!","+").Trim() + ",' ','') LIKE REPLACE('%" + sArrSearchCriteria[1].Replace("!","+").Trim().Replace("'", "''").Replace(" ","") + "%',',','')))";
									sSearchClause += "((REPLACE(" + sArrSearchCriteria[i].Replace("!","+").Trim() + ",' ','') LIKE UPPER('%" + sArrSearchCriteria[i+1].Replace("!","+").Trim().Replace("'", "''").Replace(" ","") + "%')) OR (REPLACE(" + sArrSearchCriteria[i].Replace("!","+").Trim() + ",' ','') LIKE UPPER(REPLACE('%" + sArrSearchCriteria[i+1].Replace("!","+").Trim().Replace("'", "''").Replace(" ","") + "%',',',''))))" + sArrSearchCriteria[i+2].Trim();
								else if(sArrSearchType[iSrchCol]=="D")
								{
									string [] twodates = sArrSearchCriteria[i+1].Trim().Replace("'", "''").Split('-');
									if(twodates.Length > 1)
									{
										try { twodates[1] = Convert.ToDateTime(twodates[1]).AddDays(1).AddMilliseconds(-1).ToString(); }
                                        catch (Exception ex) { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
										sSearchClause += "((" + sArrSearchCriteria[i].Trim() + " >= '" + twodates[0]  + "' AND " + sArrSearchCriteria[i].Trim() + " <= '" + twodates[1]  + "'))" + sArrSearchCriteria[i+2].Trim();
									}
								}
								else if(sArrSearchType[iSrchCol]=="L")
								{
									if(sArrSearchCriteria[i+1].Replace(" ","") !="")
										sSearchClause += "((" + sArrSearchCriteria[i].Replace("!","+").Trim() + ") = (" + sArrSearchCriteria[i+1].Replace("!","+").Trim().Replace("'", " ") + "))" + sArrSearchCriteria[i+2].Trim();
								}
								else if(sArrSearchType[iSrchCol]=="LT")
								{
									if(sArrSearchCriteria[i+1].Replace(" ","") !="")
										sSearchClause += "((" + sArrSearchCriteria[i].Replace("!","+").Trim() + ") <= (" + sArrSearchCriteria[i+1].Replace("!","+").Trim().Replace("'", " ") + "))" + sArrSearchCriteria[i+2].Trim();
								}
								else if(sArrSearchType[iSrchCol]=="Tx")
								{
									sSearchClause += "(" + sArrSearchCriteria[i].Replace("!","+").Trim() + " LIKE ('%" + sArrSearchCriteria[i+1].Replace("!","+").Trim().Replace("'", "''") + "%'))" + sArrSearchCriteria[i+2].Trim();
								}
						}
						sSearchClause += ")";
					}
					sArrSearchCriteria = null;
				}
				
				sSQLQuery="";
				int lIntMainCount=0;
				string lstr=null,lStrCol;

				string[] lArrPrimaryColsName=Server.UrlDecode(sPrimaryColsName).Split(new Char[]{INTERNALSEPARATOR});
				//                for(lIntMainCount=0;lIntMainCount < lArrPrimaryColsName.GetLength(0); lIntMainCount++)
				//                {
				//                    if(lIntMainCount==0)
				//                    {
				//                        lstr=lArrPrimaryColsName[lIntMainCount];                    
				//
				//                        if(lstr.IndexOf(".")!=-1 )
				//                            gLPrimaryCol=lstr.Substring(lstr.IndexOf(".")+1);
				//                        else
				//                            gLPrimaryCol=lstr;
				//                        break;
				//                    }
				//                }
				// gLPrimaryCol=lstr;
				//prepare the ORDER BY clause
				/************************************************************************************************/
				string[] sArrQueryClauses = Server.UrlDecode(sQueryGroup.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
				if (sArrQueryClauses[5] != null && sArrQueryClauses[5].Trim() != "") //Get the non-empty ORDER BY clause from the query group
				{
					if(grouping=="Y")
					{
						string sActualColName = "";
						string strLocalGroupNo="";
						string sArrOrderCriteria = sArrQueryClauses[5].Trim();
						sColName = sArrOrderCriteria.Substring(0, sArrOrderCriteria.IndexOf(" ")).Trim(); //return the first column name of the ORDER BY clause
						bAsc = (sArrOrderCriteria.Substring(sArrOrderCriteria.IndexOf(" ")).Trim().ToUpper() == "ASC") ? true : false; //return the sort order of the first column name
						if(oHashTab.ContainsKey(sColName) && oHashTab[sColName].ToString().Trim() != "") 
						{ 
							//sHashOrderByColName=" UPPER(" + oHashTab[sColName].ToString().Trim()+ " )";
							sHashOrderByColName = oHashTab[sColName].ToString().Trim();
							//sArrOrderCriteria = sArrOrderCriteria.Replace(sColName, oHashTab[sColName].ToString());
							sArrOrderCriteria = sArrOrderCriteria.Replace(sColName, sHashOrderByColName);
							sActualColName = oHashTab[sColName].ToString();
							//sActualColName =sHashOrderByColName;
						}
						sOrderClause = ","+sAutoOrderClause+",";
						// The following code added by Rajul,to change the order of fields in order by
						if (sOrderClause !="" )
						{
							if(sOrderClause.IndexOf(","+strDefGroupNo+",")>= 0)
							{
								sOrderClause=sOrderClause.Remove(sOrderClause.IndexOf(","+strDefGroupNo+","),strDefGroupNo.Length+1);
								strLocalGroupNo=","+strDefGroupNo+"";
							}
							if(sOrderClause.IndexOf(","+sActualColName+",")>= 0)
							{
								sOrderClause=sOrderClause.Remove(sOrderClause.IndexOf(","+sActualColName+","),sActualColName.Length+1);
								sOrderClause=","+sActualColName+"" + sOrderClause;
							}
							if (strLocalGroupNo !="")
							{
								sOrderClause=strLocalGroupNo+sOrderClause;
							}
						}
						// end of code added by Rajul,to change the order of fields in order by
						if(sActualColName == "")
							sOrderClause = sOrderClause.Replace(","+sColName+",", ","+sArrOrderCriteria+",");
						else
							sOrderClause = sOrderClause.Replace(","+sActualColName+",", ","+sArrOrderCriteria+",");
						sOrderClause = sOrderClause.Substring(1,sOrderClause.Length-2);
						sArrOrderCriteria = null;
					}
					else
					{
						// gArrColActualNames -------- Stores all the display column names.
						// sArrSearchType	  -------- Stores all the search column type 
						// sSearchArrCol	  -------- Stored all the columns names in search columns
						sOrderClause = sArrQueryClauses[5].Trim();

						string[] sArrAutoOrder=sAutoOrderClause.Split(new char[]{','});  
						string sArrOrderCriteria = sArrQueryClauses[5].Trim() ;
						int iTx=0;
						if(sArrOrderCriteria.IndexOf(" ")!=-1)
						{
							sColName = sArrOrderCriteria.Substring(0, sArrOrderCriteria.IndexOf(" ")).Trim(); //return the first column name of the ORDER BY clause
							bAsc = (sArrOrderCriteria.Substring(sArrOrderCriteria.IndexOf(" ")).Trim().ToUpper() == "ASC") ? true : false; //return the sort order of the first column name
						}
						else
						{
							sColName = sArrOrderCriteria.Trim(); //return the first column name of the ORDER BY clause
						}

						//Added by Asfa(18-june-2008) - iTrack #3906(Note: 1)
						if(sColName !="")
						{
							if(sColName.LastIndexOf("_1") > 0)
								sColName = sColName.Substring(0,sColName.LastIndexOf("_1"));
						}

						for(iTx=0;iTx<gArrColActualNames.Length;iTx++)
						{
							if(gArrColActualNames[iTx]==sColName)
							{	
								if(iTx<sArrSearchType.Length)
								{
									if(sArrSearchType[iTx]=="Tx")
									{
										if(bAsc)
											sOrderClause = "cast(" + sSearchArrCol[iTx] + " as varchar)" + " ASC" ;
										else
											sOrderClause = "cast(" + sSearchArrCol[iTx] + " as varchar)" + " DESC" ;
										break;
									}							
								}
								int i=0;
								for(;i<sSearchArrCol.Length;i++)
								{
									if(sSearchArrCol[i].IndexOf(sColName)!=-1)
										break;

								}

								if(i<sArrSearchType.Length)
								{
									if(sArrSearchType[i]=="D")
									{
										if(bAsc)
											sOrderClause = "convert(datetime," + sSearchArrCol[i] + ")" + " ASC" ;
										else
											sOrderClause = "convert(datetime," + sSearchArrCol[i] + ")" + " DESC" ;
										break;
									}
								}
								//}
							}

							
							/*if(sArrSearchType[iTx]=="Tx")
							{
								textColumn=gArrColActualNames[iTx] ;
								if(textColumn==sColName)
								{
									sColName="cast(" + sColName + " as varchar)";
									if(bAsc)
										sOrderClause = sColName + " ASC" ;
									else
										sOrderClause = sColName + " DESC" ;
									break;
								}
							}*/								
						}
						sArrOrderCriteria = null;
					}
				}
				else //use the default sort column no (assumed in the calling function) to sort the result set
				{
					sColName = sDefaultSortCol; //return the first column no
					sOrderClause = sColName + " ASC " ;
				}
				/***************************************************************************************************************/
				/*string[] sArrQueryClauses = Server.UrlDecode(sQueryGroup.Replace("+", "%2B")).Split(new char[]{SEPARATOR});
				if (sArrQueryClauses[5] != null && sArrQueryClauses[5].Trim() != "") //Get the non-empty ORDER BY clause from the query group
				{
					sOrderClause = sArrQueryClauses[5].Trim();
					string[] sArrOrderCriteria = sArrQueryClauses[5].Split(new char[]{','});
					sColName = sArrOrderCriteria[0].Substring(0, sArrOrderCriteria[0].IndexOf(" ")).Trim(); //return the first column name of the ORDER BY clause
					bAsc = (sArrOrderCriteria[0].Substring(sArrOrderCriteria[0].IndexOf(" ")).Trim().ToUpper() == "ASC") ? true : false; //return the sort order of the first column name
					sArrOrderCriteria = null;
				}
				else //use the default sort column no (assumed in the calling function) to sort the result set
				{
					sOrderClause = lstr + " ASC";
					sColName = lstr; //return the first column no
				}


				if(gobjHashTab.ContainsKey(sColName) && gobjHashTab[sColName].ToString().Trim() != "") 
				{
					sOrderClause = sOrderClause.Replace(sColName, gobjHashTab[sColName].ToString());
				}*/

				//To make the Grid UniqueID Column for the Hyperlink By Bhusingh on 14th Jan 2004
				
				StringBuilder lStrGrdUniqueId=new StringBuilder();
				
				for(lIntMainCount=0;lIntMainCount < lArrPrimaryColsName.GetLength(0); lIntMainCount++)
				{
					lstr=lArrPrimaryColsName[lIntMainCount];
					if(lstr.Length>0)
					{
						if(lstr.ToLower().IndexOf("convert",0)!=-1)
						{
							lStrCol=lstr.Split(',')[1];
							lStrCol=lStrCol.Replace(")","");
							if(lStrCol.IndexOf(".",0)!=-1)lStrCol=lStrCol.Substring(lStrCol.IndexOf(".",0)+1,lStrCol.Length-lStrCol.IndexOf(".",0)-1);
							
							if(lStrGrdUniqueId.Length==0)
								lStrGrdUniqueId.Append("'"+lStrCol+"='+replace("+lArrPrimaryColsName[lIntMainCount]+ ",'/','~')");
							else
								lStrGrdUniqueId.Append("+'&"+lStrCol+"='+replace("+lArrPrimaryColsName[lIntMainCount]+ ",'/','~')");
						}
						else
						{
							if(lstr.IndexOf(".",0)!=-1)lstr=lstr.Substring(lstr.IndexOf(".",0)+1,lstr.Length-lstr.IndexOf(".",0)-1);
							if(lStrGrdUniqueId.Length==0)
								lStrGrdUniqueId.Append("'"+lstr+"='+isnull(cast("+lArrPrimaryColsName[lIntMainCount]+ " as varchar(8000)),0)");
							else
								lStrGrdUniqueId.Append("+'&"+lstr+"='+isnull(cast("+lArrPrimaryColsName[lIntMainCount]+ " as varchar(8000)),0)");
						}
						//						if(lstr.IndexOf(".",0)!=-1)lstr=lstr.Substring(lstr.IndexOf(".",0)+1,lstr.Length-lstr.IndexOf(".",0)-1);
						//						if(lStrGrdUniqueId.Length==0)
						//							lStrGrdUniqueId.Append("'"+lstr+"='+cast(replace("+lArrPrimaryColsName[lIntMainCount]+ ",'/','~') as varchar)");
						//						else
						//							lStrGrdUniqueId.Append("+'&"+lstr+"='+cast(replace("+lArrPrimaryColsName[lIntMainCount]+ ",'/','~') as varchar)");
					}
				}

				if(lStrGrdUniqueId.Length!=0) lStrGrdUniqueId.Append(" as UniqueGrdId");
				if(lstr!=null)lstr=null;
				//-------------------------------------------------------------------------------


				string [] select_arr = sArrQueryClauses[0].Trim().Split(',');
				for(int index = 0; index < select_arr.Length; index++)
				{
					if(select_arr[index].IndexOf("$DECRYPT_") == 0)
					{
						//decrypt_position = index;
						decrypt_select = select_arr[index].Replace("$DECRYPT_","");
						sArrQueryClauses[0] = sArrQueryClauses[0].Replace("$DECRYPT_","");
					}
				}
				//prepare the query using the clauses prepared above and query group
				if(lStrGrdUniqueId.Length>0)//sStrGrdUniqueId is added by Bhusing on 14 Jan 2004 for Unique Grid Id
					sSqlQuery.Append("SELECT top " + (int.Parse(totrecords) * int.Parse(iPageSize))  + " " + sArrQueryClauses[0].Trim() + ","+lStrGrdUniqueId+ " FROM " + sArrQueryClauses[1].Trim());
				else
					sSqlQuery.Append("SELECT top " + (int.Parse(totrecords) * int.Parse(iPageSize))  + " " + sArrQueryClauses[0].Trim() + " FROM " + sArrQueryClauses[1].Trim());
				
				if (sArrQueryClauses[2] != null && sArrQueryClauses[2].Trim() != "") 
				{
					sSqlQuery.Append(" WHERE " + sArrQueryClauses[2].Trim());
					bWhereAdded = true;
				}

				if (sFilterClause != "" && bWhereAdded)
					sSqlQuery.Append(" AND " + sFilterClause);
				else if(sFilterClause != "") 
				{
					sSqlQuery.Append(" WHERE " + sFilterClause);
					bWhereAdded = true;
				}                

				if (sSearchClause != "" && bWhereAdded)
				{
					sSqlQuery.Append(" AND " + sSearchClause);               
				}
				else if(sSearchClause != "") 
				{
					sSqlQuery.Append(" WHERE " + sSearchClause);
					bWhereAdded = true;
				}
                
                
				if(sSqlQuery.ToString().IndexOf("WHERE",0)!=-1 )
				{
					if(uniqId!="" && uniqId!=null)
					{
						sSqlQuery.Append(" and " + uniqId + "=" + int.Parse(lst) );
					}
				}
				else
				{
					if(uniqId!="" && uniqId!=null)
					{                        
						sSqlQuery.Append(" where " + uniqId + "=" + int.Parse(lst) );
					}
				}
                                            

				if (sArrQueryClauses[3] != null && sArrQueryClauses[3].Trim() != "") 
					sSqlQuery.Append(" GROUP BY " + sArrQueryClauses[3].Trim());

				if (sArrQueryClauses[4] != null && sArrQueryClauses[4].Trim() != "") 
					sSqlQuery.Append(" HAVING " + sArrQueryClauses[4].Trim());

				if (sOrderClause != "")
					sSqlQuery.Append(" ORDER BY " + sOrderClause);
				
		
			}
			catch(Exception oEx) 
			{
				throw oEx;
			}

              
			return sSqlQuery.ToString();
		}



	
		public DataSet ExecuteInlineGridRecords(string pStrSQL)
		{
			DataSet lds=new DataSet();
			return lds; 
		}
	
	}


	public class CreateItemTemplateLabel : ITemplate
	{
		private string strControlId="";
        
		private string strControlURL="";
        

		public CreateItemTemplateLabel(string strID,string navURL)
		{
			strControlId=strID; 
			strControlURL=navURL;
			//strControlText=strText;
		}

		public void InstantiateIn(Control objContainer)
		{
			Label hl=new Label();  
			//hl.Enabled = false;
			hl.ID=strControlId;
			//hl.Text=strControlText;
			objContainer.Controls.Add(hl);
		}
	}


	public class CreateItemTemplateHL : ITemplate
	{
		private string strControlId="";
        
		private string strControlURL="";
        

		public CreateItemTemplateHL(string strID,string navURL)
		{
			strControlId=strID; 
			strControlURL=navURL;
			//strControlText=strText;
		}

		public void InstantiateIn(Control objContainer)
		{
			HyperLink hl=new HyperLink();  
			hl.ID=strControlId;
			hl.NavigateUrl=strControlURL;
			//hl.Text=strControlText;
			objContainer.Controls.Add(hl);
		}
	}

	public class CreateItemTemplateCK : ITemplate
	{
		private string strControlId="";
        
		//private string strControlURL="";
        

		public CreateItemTemplateCK(string strID)
		{
			strControlId=strID;             
		}

		public void InstantiateIn(Control objContainer)
		{
			CheckBox ck=new CheckBox(); 
			ck.ID=strControlId;            
			objContainer.Controls.Add(ck);
		}
	}

	public class CreateItemTemplateDDLAndText : ITemplate
	{
		private string strDDLId="";
		private string strLblID = "";
		//private string strControlURL="";
        

		public CreateItemTemplateDDLAndText(string strDdlID,string strLabelID)
		{
			strDDLId=strDdlID;    
			strLblID=strLabelID;
		}

		public void InstantiateIn(Control objContainer)
		{
			DropDownList ddl=new DropDownList(); 
			ddl.ID=strDDLId;            
			objContainer.Controls.Add(ddl);

			Label lbl = new Label();
			lbl.ID = strLblID;
			objContainer.Controls.Add(lbl);

		}

	}

	public class CreateItemTemplateDDL : ITemplate
	{
		private string strControlId="";
        
		//private string strControlURL="";
        

		public CreateItemTemplateDDL(string strID)
		{
			strControlId=strID;             
		}

		public void InstantiateIn(Control objContainer)
		{
			DropDownList ddl=new DropDownList(); 
			ddl.ID=strControlId;            
			objContainer.Controls.Add(ddl);
		}
	}

	public class CreateItemTemplateBUT : ITemplate
	{
		private string strControlId="";
        
		//private string strControlURL="";
        

		public CreateItemTemplateBUT(string strID)
		{
			strControlId=strID;             
		}

		public void InstantiateIn(Control objContainer)
		{
			Button but=new Button(); 
			but.ID=strControlId;            
			but.Text="GO"; 
			
			objContainer.Controls.Add(but);
		}
	}

	public class CreateItemTemplateIMG : ITemplate
	{
		private string strControlId="";
        
		//private string strControlURL="";
        

		public CreateItemTemplateIMG(string strID)
		{
			strControlId=strID;             
		}

		public void InstantiateIn(Control objContainer)
		{
			Image i1=new Image(); 
			i1.ID=strControlId;            
			//i1.Text="GO"; 
			
			objContainer.Controls.Add(i1);
		}
	}

}
