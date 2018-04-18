/******************************************************************************************
<Author					: - Pradeep Iyer
<Start Date				: -	11/04/2005
<End Date				: -	
<Description			: - This is a generic look up window. The details of all look up 
							forms need to entered in XML format in LookupForm.xml file.
							These are the arguments passed to this page in the querystring:
							DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,
							LookupCode,
							Arg1,Arg2,Arg3,Arg4 (Optional)
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using System.Text;
using Cms.BusinessLayer.BlCommon;
using System.Xml.XPath;

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class LookUpForm : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.DropDownList ddlSearchColumn;
		protected System.Web.UI.WebControls.TextBox txtSearch;
		protected System.Web.UI.WebControls.Button btnSearch;
		
		#region "Private members"
		
		private string lookUpForm = "";
		private string xmlGridConfigFile = "";
		private string filterString = "";
		private string select;
		private string orderBy = "";
		private Hashtable htWhere = new System.Collections.Hashtable();
		private XmlDocument xmlDoc = new XmlDocument();
		private ArrayList alFilter = new ArrayList();
		protected System.Web.UI.WebControls.Button btnReset;
		private int ReturnColIndex=0;
		protected System.Web.UI.WebControls.DataGrid dgLookup;
		private XmlElement formRoot; //Holds the root of the relevant look up form
		
		string dataValueFieldID = "";
		string dataTextFieldID  = "";
		string dataValueField = "";
		string dataTextField  = "";

		#endregion
		
		#region "Public properties"
		
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

		#endregion
		
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.dgLookup.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgLookup_ItemCommand);
			this.dgLookup.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgLookup_PageIndexChanged);
			this.dgLookup.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgLookup_Sort);
			this.dgLookup.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgLookup_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			dataValueFieldID = Request.QueryString["DataValueFieldID"].ToString();
			dataTextFieldID  = Request.QueryString["DataTextFieldID"].ToString();
			dataValueField = Request.QueryString["DataValueField"].ToString();
			dataTextField  = Request.QueryString["DataTextField"].ToString();

			xmlGridConfigFile = Server.MapPath("../Support/LookupForms.xml");
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
			
				//Save to ViewState
				this.LookupXML = formRoot.OuterXml;
			}
			else
			{
				xmlDoc.LoadXml(LookupXML);
				formRoot = xmlDoc.DocumentElement;
			}
			
			//Make provision for four where conditions in the Hashtable
			htWhere.Add("WhereColumn1",null);
			htWhere.Add("WhereColumn2",null);
			htWhere.Add("WhereColumn3",null);
			htWhere.Add("WhereColumn4",null);

			
			//Get the information from the XML file
			ParseXML();

			//Add where conditions from Query string////////////////////////////////////
			for(int i = 1; i < 5; i ++)
			{
				if ( Request.QueryString["Arg" + i.ToString()] != null )
				{
					if ( Request.QueryString["Arg" + i.ToString()].ToString() != "" && Request.QueryString["Arg" + i.ToString()].ToString() != "undefined")
					{
						this.AddWhere(i,Request.QueryString["Arg" + i.ToString()]);
					}
				}
			}

			////////////////////////////////////////////////////////////////////////////
			
			// Put user code to initialize the page here
			if ( !Page.IsPostBack )
			{
				
				//ViewState for Sort functionality
				ViewState["SortField"] = "";
				ViewState["SortDirection"] = "ASC";

				//Populate the Search DDL
				PopulateSearchDDL();
				
				//Generate and bind the grid
				GenerateGrid();
				BindGrid();
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
		
		/// <summary>
		/// Binds the datagrid to the data source
		/// </summary>
		private void BindGrid()
		{
			dgLookup.DataSource = this.GetDataSource();
			dgLookup.DataBind();
		}
		
		/// <summary>
		/// Adds the where clauses in the instance Hashtable
		/// </summary>
		/// <param name="index"></param>
		/// <param name="colValue"></param>
		public void AddWhere(int index,string colValue)
		{
			Column objCol = (Column)htWhere["WhereColumn" + index.ToString()];

			switch (objCol.Type)
			{
				case "string":
				case "datetime":
					objCol.Value = "'" + colValue + "'";
					break;
				default: 
					objCol.Value = colValue;
					break;
			}

		}
		
		/// <summary>
		/// Returns a data source by parsing the SQl query 
		/// </summary>
		/// <returns></returns>
		private DataView GetDataSource()
		{
			StringBuilder sbQuery = new StringBuilder();
			StringBuilder sbWhere = new StringBuilder();
			StringBuilder sbFilter = new StringBuilder();

			//sbQuery.Append(this.select);
			
			IDictionaryEnumerator myEnumerator = htWhere.GetEnumerator();
			
			//Build the where columns if any
			while ( myEnumerator.MoveNext() )
			{
				//If no where condtion is set
				if ( myEnumerator.Value == null ) continue;

				Column objColumn = (Column)myEnumerator.Value;
				

				//If the value ids empty
				if ( objColumn.Value.Trim() == "" ) continue;
				
				if ( sbWhere.ToString() == "" )
				{
					sbWhere.Append( objColumn.Name + " = " + objColumn.Value + @" " );
				}
				else
				{
					sbWhere.Append( " AND " + objColumn.Name + " = " + objColumn.Value + @" ");
				}

			}

			//Build the filter string if any
			if ( txtSearch.Text.Trim().Length > 0 )
			{
				string[] dataValue = this.ddlSearchColumn.SelectedValue.Split(",".ToCharArray());
				
				string colName = dataValue[0];
				string colType = dataValue[1];
				string searchValue = txtSearch.Text.Trim();
				
				switch(colType)
				{
					case "string":
						sbFilter.Append( colName + " LIKE '" + searchValue + "%' ");
						break;
					default:
						sbFilter.Append( colType + " = " + searchValue + @" " );
						break;

				}
			}		
				
			//Append the select clause
			sbQuery.Append(this.select + " " );
			
			//Append the where clause, if present
			if ( sbWhere.ToString().Trim().Length > 0 )
			{
				sbQuery.Append(" WHERE " + sbWhere.ToString());
			}
			
			//Append the Grop By clause, if present
			if ( this.orderBy.Trim().Length > 0 )
			{
				sbQuery.Append(" ORDER BY " + this.orderBy);
			}

			string sql = sbQuery.ToString();
			
			DataSet ds = ClsCommon.ExecuteDataSet(sql);
			
			DataView dataView = new DataView(ds.Tables[0]);			
			
			if ( sbFilter.ToString().Length > 0 )
			{
				dataView.RowFilter  = sbFilter.ToString();
			}
			
			if ( ViewState["SortField"].ToString().Length > 0 )
			{
				dataView.Sort = ViewState["SortField"] + " " + ViewState["SortDirection"];
			}

			return dataView;

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
				cols[i].DisplayName = gridColumns.ChildNodes[i].SelectSingleNode("heading").InnerXml.Trim();
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
			this.select = formRoot.SelectSingleNode("Select").InnerXml;
			
			//Get the Where columns collection
			XmlNode whereNode = formRoot.SelectSingleNode("WhereColumns");
			
			//Populate where columns Hashtable
			if ( whereNode != null )
			{
				foreach(XmlNode child in whereNode.ChildNodes )
				{
					int index = Convert.ToInt32(child.SelectSingleNode("index").InnerXml);

					//Add column name to hashtable
					Column objColumn = new Column();
					objColumn.Name = child.SelectSingleNode("name").InnerXml.Trim();
					objColumn.Type = child.SelectSingleNode("type").InnerXml.Trim();

					htWhere["WhereColumn" + index.ToString()] = objColumn;
				}
										
			}
			
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
					objColumn.Heading = child.SelectSingleNode("heading").InnerXml;

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
			if ( e.Item.ItemType == ListItemType.Header )
			{
				for(int i=0;i<=e.Item.Cells.Count-1;i++)
				{
					e.Item.Cells[i].CssClass="lockrow";	
				}

			}
			

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
					throw new Exception("The data text field and the data value field does not exist in the data source.");
				
				}

				string strJSFunction = "OnDoubleClick('" + dataTextFieldID + "','" + dataValueFieldID +
					"','" + 
					ReplaceInvalidCharacters(strDataText) + "','" + 
					ReplaceInvalidCharacters(strDataValue) + "')";

				e.Item.Attributes.Add("onDblClick",strJSFunction);
				e.Item.Attributes.Add("onmouseover","changecursor(this)");
			}
		}
		
		private string ReplaceInvalidCharacters(string str)
		{
			string str1 = str.Replace("'",@"\'");

			return str1;
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			GenerateGrid();
			BindGrid();
		}
		
		/// <summary>
		/// Clears the search field an re-binds the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_Click(object sender, System.EventArgs e)
		{
			txtSearch.Text = "";
			GenerateGrid();
			BindGrid();
		}

		/// <summary>
		/// Executes when the page index of the grid changes
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgLookup_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgLookup.CurrentPageIndex = e.NewPageIndex;
			GenerateGrid();
			BindGrid();
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
			BindGrid();
		}

		private void dgLookup_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			Response.Write("hello");
		}

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
			get { return Name + "," + Type ; }
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
