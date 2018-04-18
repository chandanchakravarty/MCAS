/******************************************************************************************
Modification History

    
<Modified Date			: - > 30/10/2006
<Modified By			: - > Mohit Agarwal
<Purpose				: - > Changes made for selection process to be datewise also
							  Also changed support\LookupForms1.xml ARREPORT section for LOB, Name and date search
******************************************************************************************/
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
using System.Text;
using System.Resources;
using System.Reflection; 
using Cms.BusinessLayer.BlCommon;
using System.Xml.XPath;


namespace Cms.CmsWeb.aspx.lookup2
{
	/// <summary>
	/// Summary description for LookupForm2.
	/// </summary>
	public class LookupForm2 : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.DropDownList ddlSearchColumn;
		protected System.Web.UI.WebControls.TextBox txtSearch;
		protected System.Web.UI.WebControls.Button btnReset;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DataGrid dgLookup;
        System.Resources.ResourceManager objResourceMgr;
		private string lookUpForm = "";
		private string xmlGridConfigFile = "";
		private string filterString = "";
		
		private string sql;
		private string orderBy = "";
		private Hashtable htWhere = new System.Collections.Hashtable();
		private XmlDocument xmlDoc = new XmlDocument();
		private ArrayList alFilter = new ArrayList();
		private string jsFunctionToCall = "";
	
		private int ReturnColIndex=0;
	
		private XmlElement formRoot; //Holds the root of the relevant look up form
		
		string dataValueFieldID = "";
		string dataTextFieldID  = "";
		string dataValueField = "";
		string dataTextField  = "";

		protected System.Web.UI.WebControls.ImageButton btnFirst;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.Label lblCurrentPage;
        protected System.Web.UI.WebControls.Label capSearch_option;
        protected System.Web.UI.WebControls.Label capSEARCH_CRITERIA;
        //protected System.Web.UI.WebControls.Button btnReset;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ImageButton btnLast;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValueFieldID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataTextFieldID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidParam;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSearch;
        protected System.Web.UI.HtmlControls.HtmlInputButton btn_Close;
        protected System.Web.UI.HtmlControls.HtmlInputButton btn_ClearField;

		private bool isLastPage = false;
        protected System.Web.UI.WebControls.DropDownList cmbSearch;
		private int search_crit = 0;
		private string search_encrypt = "";
		
		public string FilterString
		{
			get { return filterString; }
			set { filterString = value; }
		}
		
		public string XmlGridConfigFile
		{
			get { return xmlGridConfigFile; }
			set { xmlGridConfigFile = value ; }
		}
		
		public string LookupForm
		{
			get { return lookUpForm; }
			set { lookUpForm = value; }
		}
		
		public string LookupXML
		{
			set { ViewState["LookupXML"] = value ; }
			get
			{

				return ( ViewState["LookupXML"] == null ? "" :  ViewState["LookupXML"].ToString() ) ; 
			}
		}
		
		public int CurrentPageIndex
		{
			set { ViewState["CurrentPageIndex"] = value;}
			get{ return Convert.ToInt32(ViewState["CurrentPageIndex"]); }

		}

		public int GridPageSize
		{
			set { ViewState["GridPageSize"] = value;}
			get{ return Convert.ToInt32(ViewState["GridPageSize"]); }

		}

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchDDL(string type)
        {
            try
            {
                ClsCommon objCommon = new ClsCommon();
                string [] arrType  = type.Split('^');
                DataSet dsTemp = objCommon.ExecuteGridQuery(arrType[0]);
                if (dsTemp != null)
                {
                    return dsTemp;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
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
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dgLookup.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgLookup_Sort);
			this.dgLookup.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgLookup_ItemDataBound);
			this.btnFirst.Click += new System.Web.UI.ImageClickEventHandler(this.btnFirst_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.btnLast.Click += new System.Web.UI.ImageClickEventHandler(this.btnLast_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_Prerender);           
		}        
		#endregion       

		private void Page_Load(object sender, System.EventArgs e)
		{
            Ajax.Utility.RegisterTypeForAjax(typeof(LookupForm2));
            SetCultureThread(GetLanguageCode());        
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.aspx.lookup2.LookupForm2", Assembly.GetExecutingAssembly());
			txtSearch.Attributes.Add("onblur","javascript:ChangeDateFormat();");
			try
			{
				if ( Request.QueryString["DataValueFieldID"] != null )
				{
					dataValueFieldID = Request.QueryString["DataValueFieldID"].ToString();
					hidDataValueFieldID.Value = Request.QueryString["DataValueFieldID"].ToString();
				}

				if ( Request.QueryString["DataTextFieldID"] != null )
				{
					dataTextFieldID  = Request.QueryString["DataTextFieldID"].ToString();
					hidDataTextFieldID.Value = Request.QueryString["DataTextFieldID"].ToString();
				}
			
				if ( Request.QueryString["DataValueField"] != null )
				{
					dataValueField = Request.QueryString["DataValueField"].ToString();
				}
			
				if ( Request.QueryString["DataTextField"] != null )
				{
					dataTextField  = Request.QueryString["DataTextField"].ToString();
				}

				if ( Request.QueryString["JSFunction"] != null )
				{
					jsFunctionToCall = Request.QueryString["JSFunction"].ToString();
				}

				xmlGridConfigFile = Server.MapPath("../Support/LookupForms1.xml");
				this.lookUpForm = Request.QueryString["LookupCode"].ToString();
			
		
				//Load the XML from the file for the first time
				//and store the relevant portion in the ViewState.
				if ( LookupXML.Trim() == "")
				{
					xmlDoc.Load(xmlGridConfigFile);
				
					//Get root
					XmlElement root = xmlDoc.DocumentElement;
			
					//Get the relevant look up form
					formRoot = (XmlElement)root.SelectSingleNode(lookUpForm);
					//formRoot = (XmlElement)root.SelectSingleNode(lookUpForm);
			
					//Save to ViewState
					if(formRoot != null)
					{
						this.LookupXML = formRoot.OuterXml;
					}
				}
				else
				{
					xmlDoc.LoadXml(LookupXML);
					formRoot = xmlDoc.DocumentElement;
				}
			
		
				//Get the information from the XML file
				ParseXML();

				//Add where conditions from Query string////////////////////////////////////
			
				string args = Request.QueryString["Args"];

				if ( args != null && args != "" & args != "undefined")
				{
					string[] strArray = Request.QueryString["Args"].Split(";".ToCharArray());

					for(int j = 0; j < strArray.Length; j++)
					{
						string[] strSplit = strArray[j].Split("=".ToCharArray());
						
						htWhere.Add(strSplit[0],strSplit[1]);
					}

				}
			
				this.SetFocus("txtSearch");

				////////////////////////////////////////////////////////////////////////////
			
				// Put user code to initialize the page here
				if ( !Page.IsPostBack )
				{
				
					//ViewState for Sort functionality
					ViewState["SortField"] = "";
					ViewState["SortDirection"] = "ASC";

					//Populate the Search DDL
					PopulateSearchDDL();
				
					this.CurrentPageIndex = 1;

					if ( System.Configuration.ConfigurationSettings.AppSettings["LOOKUP_PAGE_SIZE"] == null )
					{
						this.GridPageSize = 10;
					}
					else
					{
						this.GridPageSize = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["LOOKUP_PAGE_SIZE"]);
					}

					VirtualRecordCount objCount = this.GetTotalCount();

					//Generate and bind the grid
					GenerateGrid();
					BindGrid1(objCount);

				
				}
                SetCaptions();
			}
			catch(Exception ex)
			{
				string massage = ex.Message;	
			}
			
		}
		
		
		/// <summary>
		/// Populates the Search dropdown list with values from the Config file
		/// </summary>
		private void PopulateSearchDDL()
		{
			ddlSearchColumn.DataTextField = "Heading";
			ddlSearchColumn.DataValueField = "DataValue";

			ddlSearchColumn.DataSource = alFilter;
			ddlSearchColumn.DataBind();

		}
		
	
		private void BindGrid1(VirtualRecordCount objCount)
		{
			string origsql = GetSql();
			int recCount = this.GridPageSize * this.CurrentPageIndex;
			
			int recsToRetrieve = this.GridPageSize;

			if ( this.CurrentPageIndex == objCount.PageCount )
			{
				recsToRetrieve = objCount.RecordsInLastPage;
			}

			string finalSql = "SELECT * FROM (" + 
				"SELECT TOP " + recsToRetrieve.ToString() + " * FROM " + 
				"(SELECT TOP " + recCount.ToString() + " * FROM" +
                "(" + origsql + " ) AS t0 " + 
				" ORDER BY " + this.orderBy + " ASC ) AS t1 " + 
				" ORDER BY " + this.orderBy + " DESC ) AS t2 " + 
				" ORDER BY " + this.orderBy ;

			if(search_encrypt != "")
			{
				finalSql = "SELECT * FROM " + 
					"(" + origsql + ") AS t0 " + 
					" ORDER BY " + this.orderBy ;
				objCount.PageCount = 1;
			}

			try
			{
				DataSet ds = ClsCommon.ExecuteDataSet(finalSql);
				
				//Added by Mohit Agarwal 6-Oct 2008 ITRack 4847
				int encryptrows = 0;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && 
                     ds.Tables[0].Columns.Contains("DECRYPT"))
                { //Added by lalit July 15 2011. if table don't have "DECRYPT" dolumn then exception shoouldn't publish
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string decrypt_col = "";
                        try
                        {
                            if (dr["DECRYPT"] != null)
                            {
                                decrypt_col = dr["DECRYPT"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                        }
                        if (decrypt_col != "")
                        {
                            string oldval = "", newval = "";

                            try
                            {
                                oldval = dr[int.Parse(decrypt_col)].ToString();
                                dr[int.Parse(decrypt_col)] = BusinessLayer.BlCommon.ClsCommon.DecryptString(dr[int.Parse(decrypt_col)].ToString());
                            }
                            catch (Exception ignore)
                            {
                                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ignore);
                            }
                            try
                            {
                                if (search_encrypt != "")
                                {
                                    if (dr[int.Parse(decrypt_col)].ToString().IndexOf(search_encrypt) < 0)
                                    {
                                        dr.Delete();
                                        //continue;
                                    }
                                    else
                                        encryptrows++;

                                    //if(encryptrows > recsToRetrieve)
                                    //	dr.Delete();
                                }
                                string strFEDERAL_ID = dr[int.Parse(decrypt_col)].ToString();
                                if (strFEDERAL_ID != "")
                                {
                                    string strvaln = "";
                                    for (int len = 0; len < strFEDERAL_ID.Length - 4; len++)
                                        strvaln += "x";

                                    strvaln += strFEDERAL_ID.Substring(strvaln.Length, strFEDERAL_ID.Length - strvaln.Length);
                                    dr[int.Parse(decrypt_col)] = strvaln;
                                    newval = strvaln;
                                }

                                int colindex = 0;
                                foreach (DataColumn dc in ds.Tables[0].Columns)
                                {
                                    if (dr[colindex].ToString().IndexOf(oldval) >= 0)
                                        dr[colindex] = dr[colindex].ToString() + "^" + newval;
                                    colindex++;
                                }
                            }
                            catch
                            { }
                        }
                    }
                }
				dgLookup.DataSource = ds.Tables[0];
				dgLookup.DataBind();
			}
			catch
			{
				if(search_crit == 1)
				{
					search_crit = -1;
				}
			}
			int totRecords = objCount.RecordCount;
			int TotalPages = objCount.PageCount;
			
			if ( this.isLastPage )
			{
				this.CurrentPageIndex = TotalPages;
			}

			//bool isValidPage = (CurrentPageIndex >=0 && CurrentPageIndex <= TotalPages-1);
			bool isValidPage = (CurrentPageIndex >=1 && CurrentPageIndex <= TotalPages);
			bool canMoveBack = (CurrentPageIndex>1);
			bool canMoveForward = (CurrentPageIndex<TotalPages);
			
			this.btnFirst.ImageUrl = "~/cmsweb/images/firstoff.gif";
			this.btnPrevious.ImageUrl = "~/cmsweb/images/prevoff.gif";
			this.btnNext.ImageUrl = "~/cmsweb/images/nextoff.gif";
			this.btnLast.ImageUrl = "~/cmsweb/images/lastoff.gif";
			
			this.btnFirst.Attributes.Add("onclick","return OnClick();");
			this.btnNext.Attributes.Add("onclick","return OnClick();");
			this.btnPrevious.Attributes.Add("onclick","return OnClick();");
			this.btnLast.Attributes.Add("onclick","return OnClick();");
		
			if ( isValidPage && canMoveBack )
			{
				this.btnFirst.ImageUrl = "~/cmsweb/images/first.gif";
				this.btnPrevious.ImageUrl = "~/cmsweb/images/prev.gif";
				
				this.btnFirst.Attributes.Remove("onclick");
				this.btnPrevious.Attributes.Remove("onclick");
			}
			
			if ( isValidPage && canMoveForward)
			{
				this.btnNext.Attributes.Remove("onclick");
				this.btnNext.ImageUrl = "~/cmsweb/images/next.gif";

				this.btnLast.Attributes.Remove("onclick");
				this.btnLast.ImageUrl = "~/cmsweb/images/last.gif";
			}
			
			if ( totRecords == 0 )
			{
                this.lblCurrentPage.Text = objResourceMgr.GetString("lblCurrentPage"); //"No records found.";
				if(search_crit == -1)
					this.lblCurrentPage.Text = "0 records. Please check if date format is: mm/dd/yyyy";
			}
			else
			{
                this.lblCurrentPage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1399")+ " " + this.CurrentPageIndex.ToString()+ " " + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1400") +" "+ TotalPages.ToString();
			}

			return;
		}
		
		/// <summary>
		/// Returns a custom object with the total no of records for the query and other info
		/// </summary>
		/// <returns></returns>
        /// 

        private void SetCaptions()
        {
            capSearch_option.Text = objResourceMgr.GetString("capSearch_option");
            capSEARCH_CRITERIA.Text = objResourceMgr.GetString("capSEARCH_CRITERIA");
            btnReset.Text = objResourceMgr.GetString("btnReset");
            btnSearch.Text = objResourceMgr.GetString("btnSearch");
            btn_Close.Value = objResourceMgr.GetString("btn_Close");
            btn_ClearField.Value =objResourceMgr.GetString("btn_ClearField");
        }
		public VirtualRecordCount GetTotalCount()
		{
			string origsql = GetSql();

			string countSql = "SELECT COUNT(*) FROM (" + origsql + ")t";
		
			ClsCommon objCommon = new ClsCommon();
		
			int count = 0;
			try
			{
				count = Convert.ToInt32(objCommon.ExecuteScalar(countSql));
			}
			catch
			{	}
			
			int TotalPages = count / this.GridPageSize;
			int remainder = count % this.GridPageSize;

			VirtualRecordCount objCount = new VirtualRecordCount();
		
			objCount.RecordCount = count;
			objCount.RecordsInLastPage = this.GridPageSize;
		
			if ( remainder == 0 )
			{
				objCount.PageCount = TotalPages;
			}
			else
			{
				objCount.RecordsInLastPage = remainder;
				objCount.PageCount = TotalPages + 1;
			}
			return objCount;
		}
		
		
		public string GetSql()
		{
			string finalSQL = this.sql;
			
			StringBuilder sbFilter = new StringBuilder();

			//Append where column values
			IDictionaryEnumerator myEnumerator = htWhere.GetEnumerator();
			
			//Build the where columns if any
			while ( myEnumerator.MoveNext() )
			{
				//If no where condtion is set
				if ( myEnumerator.Value == null ) continue;
				
				finalSQL = finalSQL.Replace(myEnumerator.Key.ToString().Trim(),myEnumerator.Value.ToString().Trim());
				
			}
			
			//Build the filter string if any
            if (txtSearch.Text.Trim().Length > 0 || hidSearch.Value.Trim()!= "")
			{
				string[] dataValue = this.ddlSearchColumn.SelectedValue.Split(";".ToCharArray());
				
				string colName = dataValue[0];
				string colType = dataValue[1];
				string searchValue = txtSearch.Text.Trim();
				
				//Replace the spl characters
				searchValue = ClsCommon.EnocodeSqlCharacters(searchValue);

				search_encrypt = "";
				switch(colType)
				{
					case "string":
						sbFilter.Append( colName + " LIKE '%" + searchValue + "%' ");
						search_crit = 0;
						break;
					case "greaterthan":
						sbFilter.Append( colName + " >= '" + searchValue + "' ");
						search_crit = 1;
						break;
					case "string_encrypt":
						search_encrypt= searchValue;
						//sbFilter.Append( colName + " LIKE '%" + searchValue + "%' ");
						search_crit = 3;
						break;
                    case "intDDL":
                        sbFilter.Append(colName + " = " + hidSearch.Value.Trim() + @" ");
                        search_crit = 4;
                        break;
					default:
						sbFilter.Append( colType + " = " + searchValue + @" " );
						search_crit = 2;
						break;
				}

				///Append filter criteria
				string sql = finalSQL.ToUpper();

				int wherePos = sql.IndexOf("WHERE " );

				if(search_crit != 3)
				{
					if ( wherePos == -1 )
					{
						finalSQL = finalSQL + " WHERE " + sbFilter.ToString();
					}
					else
					{
						finalSQL = finalSQL + " AND " + sbFilter.ToString();
					}
				}

			}		
			
			if ( this.orderBy != "" )
			{
				//finalSQL = finalSQL + " " + this.orderBy;
			}
			
			return finalSQL;
		}
		
		/// <summary>
		/// Gets the Datagrid column info from the XML file
		/// </summary>
		/// <returns></returns>
		private GridColumn[] GetColumnInfo()
		{
			//Get the Grid columns collection
			XmlNode gridColumns = formRoot.SelectSingleNode("GridColumns");

			int columncount = gridColumns.ChildNodes.Count;

			GridColumn[] cols=new GridColumn[columncount];
			
			for(int i=0; i < gridColumns.ChildNodes.Count; i++)
			{
		
				cols[i].Name = gridColumns.ChildNodes[i].SelectSingleNode("datafield").InnerXml.Trim();
                XmlNodeList NodeL = gridColumns.ChildNodes[i].SelectNodes("heading");
                if (NodeL != null)
                {
                    foreach (XmlNode prnode in NodeL)
                    {
                        for (int a = 0; a < prnode.ChildNodes.Count; a++)
                        {
                            if (prnode.ChildNodes[a].Attributes["Id"].Value == (GetLanguageID().Trim() == "" ? "1" : GetLanguageID().Trim())) 
                            {
                                cols[i].DisplayName = prnode.ChildNodes[a].InnerXml.ToString().Trim(); 
               //cols[i].DisplayName = gridColumns.ChildNodes[i].SelectSingleNode("heading").InnerXml.Trim();
                            }
                        }
                    }
                }
				cols[i].FormattingExpression = gridColumns.ChildNodes[i].SelectSingleNode("dataformatstring").InnerXml.Trim();
				cols[i].Sortable=(gridColumns.ChildNodes[i].SelectSingleNode("sorting").InnerXml.Trim()=="true"?true:false);
				cols[i].Visible=(gridColumns.ChildNodes[i].SelectSingleNode("visible").InnerXml.Trim()=="true"?true:false);
				//cols[i].ReturnColumn=(gridColumns.ChildNodes[i].SelectSingleNode("visible").InnerXml.Trim()=="true"?true:false);
				
				if(cols[i].ReturnColumn)
					ReturnColIndex=i+1;

			}
			return cols;
		}
		
		/// <summary>
		/// Adds the relevant columns specified in teh XML file to the data grid
		/// </summary>
		public void GenerateGrid()
		{
			GridColumn[] cols=GetColumnInfo();
		
			for(int i=0;i<cols.Length;i++)
			{
				BoundColumn col=new BoundColumn();
				col.DataField=cols[i].Name;
				if(cols[i].FormattingExpression!="None")
					col.DataFormatString=cols[i].FormattingExpression;
				col.HeaderText=cols[i].DisplayName;
				if(cols[i].Sortable)
					col.SortExpression=cols[i].Name;
				col.Visible=cols[i].Visible;
				dgLookup.Columns.Add(col);
			}
		
		}
		
		/// <summary>
		/// Parses the relevant lookup information from the LookupForms.xml file
		/// </summary>
		public void ParseXML()
		{
			
			//Get the select clause
			this.sql = formRoot.SelectSingleNode("SQL").InnerXml;
			
			//Decode the XML characters
			this.sql  = ClsCommon.DecodeXMLCharacters(this.sql);

			//Get the Where columns collection
			XmlNode whereNode = formRoot.SelectSingleNode("WhereColumns");
			
			//Get the Order by clause, if present
			XmlNode orderByNode = formRoot.SelectSingleNode("OrderBy");

			if ( orderByNode != null )
			{
				this.orderBy = orderByNode.InnerXml.Trim();
			}		

			//Get the Search node
			XmlNode searchNode = formRoot.SelectSingleNode("SearchColumns");

			//Populate Filter columns arraylist
			if ( searchNode != null )
			{
				foreach(XmlNode child in searchNode.ChildNodes )
				{
	
					//Add column info to Arraylist
					Column objColumn = new Column();
					objColumn.Name = child.SelectSingleNode("name").InnerXml;
					objColumn.Type = child.SelectSingleNode("type").InnerXml;

                    XmlNodeList xmln = child.SelectNodes("heading");
                   if (xmln != null)
                   {
                    foreach (XmlNode prnode1 in xmln)

                    {

                        for (int b = 0; b < prnode1.ChildNodes.Count; b++)

                        {

                            if (prnode1.ChildNodes[b].Attributes["Id"].Value == (GetLanguageID().Trim() == "" ? "1" : GetLanguageID().Trim())) 

                            {

                                objColumn.Heading = prnode1.ChildNodes[b].InnerXml.ToString().Trim(); 

					//objColumn.Heading = child.SelectSingleNode("heading").InnerXml;
                            }

                        }

                    }

                   }


                    if (objColumn.Type == "intDDL")
                    {
                        ddlSearchColumn.Attributes.Add("onchange", "fillDropDown();");
                        hidParam.Value = child.SelectSingleNode("param").InnerXml;
                    }

					alFilter.Add(objColumn);
				}										
			}
		}

				
		/// <summary>
		/// Adds relevant javascript code to each row of the data grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgLookup_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item )
			{

				string strDataValue = "";			
				string strDataText = "";
				
				if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains(dataValueField) == true )
				{
					strDataValue = Convert.ToString(DataBinder.Eval(e.Item.DataItem,dataValueField));	
				}
				
				if (((DataRowView)e.Item.DataItem).Row.Table.Columns.Contains(dataTextField) == true )
				{
					strDataText = Convert.ToString(DataBinder.Eval(e.Item.DataItem,dataTextField));
				}
				
				if ( strDataValue == "" && strDataText == "" )
				{
					//throw new Exception("The data text field and the data value field does not exist in the data source.");
				
				}
				
				//function OnDoubleClick(TextFieldID,ValueFieldID,TextFieldValue,ValueFieldValue)
				string strJSFunction = "OnDoubleClick('" + dataTextFieldID + "','" + dataValueFieldID +
					"','" + 
					ReplaceInvalidCharacters(strDataText) + "','" + 
					ReplaceInvalidCharacters(strDataValue) + "','" + 
					this.jsFunctionToCall + "'" +  
					")";

				e.Item.Attributes.Add("onDblClick",strJSFunction);
				e.Item.Attributes.Add("onmouseover","changecursor(this)");
			}
		}
		
		private string ReplaceInvalidCharacters(string str)
		{
			string str1 = str.Replace("'",@"\'");
			//Added by Sibin on 21 Jan 09 for Itrack Issue 5055,5318s
			string str2 = str1.Replace("\r\n"," ");
			return str2;
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.CurrentPageIndex = 1;
			VirtualRecordCount objCount = this.GetTotalCount();
			GenerateGrid();
			BindGrid1(objCount);
			if(dgLookup!=null && dgLookup.Items.Count>0)
				dgLookup.Items[0].CssClass="SelectedDataRow";

            ClientScript.RegisterStartupScript(this.GetType(), "LOADDDL", "<script>fillDropDown();</script>");
		}
		
		/// <summary>
		/// Clears the search field an re-binds the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_Click(object sender, System.EventArgs e)
		{
			txtSearch.Text = "";
			
			this.CurrentPageIndex = 1;
			VirtualRecordCount objCount = this.GetTotalCount();
			GenerateGrid();
			BindGrid1(objCount);
		}

		
		private void dgLookup_Sort(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			ViewState["SortField"] = e.SortExpression;

			if ( ViewState["SortDirection"].ToString() == "ASC" )
			{
				ViewState["SortDirection"] = "DESC";
			}	
			else
			{
				ViewState["SortDirection"] = "ASC";
			}

			GenerateGrid();
			//BindGrid();
		}


		private void btnLast_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			isLastPage = true;
			//TextBox1.Text = GetPagedSql();
			VirtualRecordCount objCount = this.GetTotalCount();
			this.CurrentPageIndex = objCount.PageCount;
			GenerateGrid();
			BindGrid1(objCount);
		}

		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.CurrentPageIndex++;
			
			VirtualRecordCount objCount = this.GetTotalCount();
		
			//TextBox1.Text = GetPagedSql();
			GenerateGrid();
			BindGrid1(objCount);
		}

		private void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.CurrentPageIndex--;
			//TextBox1.Text = GetPagedSql();
			VirtualRecordCount objCount = this.GetTotalCount();
			GenerateGrid();
			BindGrid1(objCount);
		}

		private void btnFirst_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.CurrentPageIndex = 1;
			//TextBox1.Text = GetPagedSql();
			VirtualRecordCount objCount = this.GetTotalCount();			
			GenerateGrid();
			BindGrid1(objCount);
		}

		private void Page_Prerender(object sender, System.EventArgs e)
		{
			string js="<script language=javascript>"    +       
				"  function _fixsmartnav(){"    +       "     if(window.__smartNav!=null){"    +       "        var a=window.__smartNav.update.toString();"    +       "        a=a.replace('hdm.appendChild(k);','try{hdm.appendChild(k);}catch(e){}');"    +       "        eval('window.__smartNav.update='+a);"    +       "        document.detachEvent('onmousemove', _fixsmartnav);"    +       "     }"    +       "  }"    +       "  document.attachEvent('onmousemove', _fixsmartnav);"    +       
				"  document.body.onload=_fixsmartnav;SetTitle();"    +       
				"</script>"; 

			ClientScript.RegisterStartupScript(this.GetType(),"_CdgMnk_FixSmartNavBug",js);
		}

		

	
		
	}
	
	public class VirtualRecordCount
	{
		public int RecordCount;
		public int PageCount;
		public int RecordsInLastPage;
	}

	/// <summary>
	/// Represents the details of a database table column
	/// </summary>
	public class Column
	{
		private string name = "" ;
		private string strValue = "";
		private string type = "";
		private string heading = "";
		
		public string Name
		{
			get { return name ;}
			set { name = value.Trim(); }
		}

		public string Value
		{
			get { return strValue; }
			set { strValue = value.Trim(); }
		}
		
		public string Type
		{
			get { return type; }
			set { type = value.Trim() ; }
		}

		public string Heading
		{
			get { return heading; }
			set { heading = value; }
		}

		public string DataValue
		{
			get { return Name + ";" + Type ; }
		}

	}
	
	/// <summary>
	/// Represents the details of a grid column
	/// </summary>
	public struct GridColumn
	{
		public string Name;
		public string DisplayName;
		public string FormattingExpression;
		public bool   Visible;
		public bool   Sortable;
		public bool   ReturnColumn;
	}

	
}

