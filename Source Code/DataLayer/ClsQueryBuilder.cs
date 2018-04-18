	/******************************************************************************************
	<Author					: - Pradeep Iyer>
	<Start Date				: -	March 24, 2005>
	<End Date				: - >
	<Description			: - Generates an appropriate Update statement
								depending on current and old values
	>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Collections;
using Cms.Model;
using System.Xml;

namespace Cms.DataLayer
{
	
	
	public class SqlUpdateBuilder
	{
		private string tableName;
		private ArrayList alColumns = new ArrayList();
		private string whereClause;
		private string fromClause;

		public string TableName
		{
			get { return tableName; }
			set { tableName = value.ToUpper(); }
		}
		
		public string WhereClause
		{
			get { return whereClause; }
			set { whereClause = value; }
		}
			
		public string FromClause
		{
			get { return fromClause; }
			set { fromClause = value; }
		}
			
		/// <summary>
		/// Clears the internal column collection
		/// </summary>
		public void ClearColumns()
		{
			alColumns.Clear();
		}
			
		/// <summary>
		/// Replaces a single quote with two single quotes
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		private string ReplaceInvalidCharacter(string input)
		{
			return input.Replace("'","''");
		}
			
		/// <summary>
		/// Adds a SqlColumn object to the internal ArrayList
		/// </summary>
		/// <param name="columnName"></param>
		/// <param name="currentValue"></param>
		/// <param name="oldValue"></param>
		/// <param name="sqlType"></param>
		/// <param name="treatLiteral"></param>
		public void AddColumn(string columnName,string currentValue, string oldValue, MSSQLType sqlType, bool treatLiteral)
		{
			ClsSqlColumn objColumn = new ClsSqlColumn();
			
			objColumn.ColumnName = columnName.Trim().ToUpper();
			objColumn.CurrentValue = currentValue.Trim();
			objColumn.OldValue = oldValue.Trim();
			objColumn.SqlType = sqlType;
			objColumn.TreatLiteral = treatLiteral;

			alColumns.Add(objColumn);
		}
			
		/// <summary>
		/// Returns the dynamically created Update statement
		/// </summary>
		/// <returns></returns>
		public string GetUpdateSQL()
		{
			//Denotes the no of columns included in the update clause
			int appended = 0;

			System.Text.StringBuilder sbQuery = new StringBuilder();
				
			sbQuery.Append("UPDATE " + TableName + " SET ");
											
			StringBuilder sbUpdate = new StringBuilder();

			for(int i = 0; i < alColumns.Count ; i++ )
			{
				ClsSqlColumn objColumn = (ClsSqlColumn)alColumns[i];
				
				switch(objColumn.SqlType)
				{
					case MSSQLType.BigInt:
					case MSSQLType.Int:
					case MSSQLType.Decimal:
					case MSSQLType.Float:
					case MSSQLType.SmallInt:
					case MSSQLType.TinyInt:
					case MSSQLType.Bit:
							
							
						if ( objColumn.CurrentValue != objColumn.OldValue )
						{
							//Append a comma, after the first column has been included
							if ( appended > 0 )
							{	
								sbUpdate.Append(", ");	
							}

							sbUpdate.Append(objColumn.ColumnName + " = " + objColumn.CurrentValue + " " );
							appended++;
						}
							
							
						break;

					case MSSQLType.VarChar:
					case MSSQLType.NVarChar:
					case MSSQLType.DateTime:
							
						if ( objColumn.CurrentValue != objColumn.OldValue )
						{
							
							//Append a comma, after the first column has been included
							if ( appended > 0 )
							{
								
								sbUpdate.Append(", ");	
							}

							//For GetDate(), NULL and other functions, don't put a quote
							if ( objColumn.TreatLiteral )
							{
								sbUpdate.Append(objColumn.ColumnName + " = " + ReplaceInvalidCharacter(objColumn.CurrentValue) + " " );
									
							}
							else
							{
								sbUpdate.Append(objColumn.ColumnName + " = '" + ReplaceInvalidCharacter(objColumn.CurrentValue) + "' ") ;
									
							}

							appended++;
						}
							
						break;

					default: break;
				}

			}
			
			if ( sbUpdate.ToString() == "" ) return "";

			//sbUpdate.Append(Environment.NewLine);
				
			//Append from clause, if present
			if ( fromClause != "" )
			{
				sbUpdate.Append(fromClause + " " );
				//sbUpdate.Append(Environment.NewLine);
			}

			//Append where clause
			sbUpdate.Append(ReplaceInvalidCharacter(whereClause));

			return sbQuery.ToString() + sbUpdate.ToString();

		}
			
		/// <summary>
		/// Returns an appropriate Update statement for the new Model class approach
		/// </summary>
		/// <param name="objOld"></param>
		/// <param name="objNew"></param>
		/// <param name="strTranXML"></param>
		/// <returns></returns>
		/// 
		public string GetUpdateSQL(Cms.Model.IModelInfo objOld,IModelInfo objNew, out string strTranXML)
		{
			return GetUpdateSQL(objOld, objNew, out strTranXML, "Y");
		}

		public string GetUpdateSQL(Cms.Model.IModelInfo objOld,IModelInfo objNew, out string strTranXML, string showTime)
		{
			StringBuilder sbXML = new StringBuilder();
				
			XmlDocument doc = null; //to load the Transaction XML 
			XmlElement root = null; //holds the root of the transaction XML
				
			//Iniialize the output argument to empty
			strTranXML = "";
				
			//Get the Transaction XML from the object, if present
			if ( objNew.TransactLabel.Trim().Length > 0 )
			{
				doc = new XmlDocument();
				sbXML.Append(objNew.TransactLabel);
				doc.LoadXml(sbXML.ToString());
					
				//Get the root element
				root = doc.DocumentElement;	
			}

			//Denotes the no of columns included in the update clause
			int appended = 0;
				
			//Represents whether the old value is  equal to new value or not
			bool blnIsEqual = false;

			System.Text.StringBuilder sbQuery = new StringBuilder();
				
			DataTable dtOld = objOld.TableInfo;
			DataTable dtNew = objNew.TableInfo;
				
			sbQuery.Append("UPDATE " + dtOld.TableName + " SET ");
								
			StringBuilder sbUpdate = new StringBuilder();

			int colCount = dtOld.Columns.Count;
								
			for(int i = 0; i < colCount; i++ )
			{
				string colType = dtOld.Rows[0][i].GetType().ToString();
				string newColType = dtNew.Rows[0][i].GetType().ToString();

				object objOldVal = dtOld.Rows[0][i];
				object objNewVal = dtNew.Rows[0][i];

				//Compare the two values
				blnIsEqual = IsEqual(objOldVal,objNewVal);
					
				//Get the Transaction node, if found
				XmlNode node = null;
					
				//Add the old and the new values to the node
				if ( root != null )
				{
					node = root.SelectSingleNode("Map[@field='" + dtOld.Columns[i].ColumnName + "']");
				}

				switch(newColType)
				{
					case "System.UInt16":
					case "System.UInt32":
					case "System.UInt64":
					case "System.Int16":
					case "System.Int32":
					case "System.Int64":
					case "System.Single":
					case "System.Double":
					case "System.Decimal":
						if ( blnIsEqual == false)
						{
							//Append a comma, after the first column has been included
							if ( appended > 0 )
							{
								sbUpdate.Append(", ");	
							}
								
							sbUpdate.Append(dtOld.Columns[i].ColumnName + " = " + dtNew.Rows[0][i].ToString() + " " );
							appended++;

							//Append Tran info
							AppendAttribute(doc,node,objOldVal,objNewVal);

						}
						else
						{
							//Remove the node if it was found
							if ( node != null )
							{
								root.RemoveChild(node);
							}
						}
						break;

					case "System.Boolean":
					case "System.String":
					case "System.DateTime":
						if ( blnIsEqual == false)
						{
							//Append a comma, after the first column has been included
							if ( appended > 0 )
							{
								sbUpdate.Append(", ");	
							}
							sbUpdate.Append(dtOld.Columns[i].ColumnName + " = '" + ReplaceInvalidCharacter(dtNew.Rows[0][i].ToString()) + "' ") ;

							appended++;

                            
							if(showTime == "Y")
								AppendAttribute(doc,node,objOldVal ,objNewVal );
							else
								AppendAttribute(doc,node,objOldVal ,objNewVal, newColType, "Y", "Y" );
						}
						else
						{
							//Remove the node if it was found
							if ( node != null )
							{
								root.RemoveChild(node);
							}
						}
						break;
						
					case "System.DBNull":
							
						//Check the datatype of the old value
						//Include column in Update only if the value has changed
						if ( blnIsEqual == false)
						{
							//Append a comma, after the first column has been included
							if ( appended > 0 )
							{
								sbUpdate.Append(", ");	
							}
							sbUpdate.Append(dtOld.Columns[i].ColumnName + " = NULL " );

							appended++;

							AppendAttribute(doc,node,objOldVal ,"null");
						}
						else
						{
							//Remove the node if it was found
							if ( node != null )
							{
								root.RemoveChild(node);
							}
						}
						break;
						
					default: 
						break;
				}
			}
				
			if ( sbUpdate.ToString() == "" ) 
			{
				return "";
			}

			//Append from clause, if present
			if ( fromClause != "" )
			{
				sbUpdate.Append(fromClause + " " );
			}

			//Append where clause
			sbUpdate.Append(whereClause);
				
			//Set transaction XML
			if ( root!= null )
			{
				if ( root.HasChildNodes )
				{
					strTranXML = doc.InnerXml;
				}
			}
				
			return sbQuery.ToString() + sbUpdate.ToString();
		}
			
			
		/// <summary>
		/// Returns an XML with the values of the passed in model object in old value
		/// </summary>
		/// <param name="objOld"></param>
		/// <param name="objNew"></param>
		/// <returns></returns>
		public string GetDeleteTransactionLogXML(Cms.Model.IModelInfo objOld)
		{
				
			StringBuilder sbXML = new StringBuilder();
				
			XmlDocument doc = null; //to load the Transaction XML 
			XmlElement root = null; //holds the root of the transaction XML
				
			//Iniialize the output argument to empty
			string strTranXML = "";
				
			//Get the Transaction XML from the object, if present
			if ( objOld.TransactLabel.Trim().Length > 0 )
			{
				doc = new XmlDocument();
				sbXML.Append(objOld.TransactLabel);
				doc.LoadXml(sbXML.ToString());
					
				//Get the root element
				root = doc.DocumentElement;	
			}
				
			DataTable dtOld = objOld.TableInfo;
				
				
			int colCount = dtOld.Columns.Count;
								
			for(int i = 0; i < colCount; i++ )
			{
				object objOldVal = dtOld.Rows[0][i];

				//Get the Transaction node, if found
				XmlNode node = null;
				string ColumnName=dtOld.Columns[i].ColumnName;	
				//Add the current value of the column in the old value node
				if ( root != null )
				{
					node = root.SelectSingleNode("Map[@field='" + dtOld.Columns[i].ColumnName + "']");
					if (ColumnName=="COV_DESC"  && dtOld.Rows[0][i]==DBNull.Value && isColumnExist(dtOld,"COVERAGE_CODE"))
						AppendOldAttribute(doc,node,dtOld.Rows[0]["COVERAGE_CODE"]);
					else
						AppendOldAttribute(doc,node,objOldVal);
				}
			}
				
			//Set transaction XML
			if ( root!= null )
			{
				//Remove nodes which does not have OldValue, NewValue attributes
				foreach(XmlNode node in root.ChildNodes)
				{
					if ( node.Attributes["OldValue"] == null && node.Attributes["NewValue"] == null )
					{
						root.RemoveChild(node);
					}
				}

				if ( root.HasChildNodes )
				{
					strTranXML = doc.InnerXml;
				}
			}
				
			return strTranXML;
		}

		public string GetTransactionLogXML(Cms.Model.IModelInfo objOld,IModelInfo objNew)
		{
			StringBuilder sbXML = new StringBuilder();
				
			XmlDocument doc = null; //to load the Transaction XML 
			XmlElement root = null; //holds the root of the transaction XML
				
			//Iniialize the output argument to empty
			string strTranXML = "";
				
			//Get the Transaction XML from the object, if present
			if ( objNew.TransactLabel.Trim().Length > 0 )
			{
				doc = new XmlDocument();
				sbXML.Append(objNew.TransactLabel);
				doc.LoadXml(sbXML.ToString());
					
				//Get the root element
				root = doc.DocumentElement;	
			}

				
			//Represents whether the old value is  equal to new value or not
			bool blnIsEqual = false;

			DataTable dtOld = objOld.TableInfo;
			DataTable dtNew = objNew.TableInfo;
				
			int colCount = dtOld.Columns.Count;
								
			for(int i = 0; i < colCount; i++ )
			{
				string colType = dtOld.Rows[0][i].GetType().ToString();
				string newColType = dtNew.Rows[0][i].GetType().ToString();

				object objOldVal = dtOld.Rows[0][i];
				object objNewVal = dtNew.Rows[0][i];

					

				//Compare the two values
				blnIsEqual = IsEqual(objOldVal,objNewVal);
					
				//Get the Transaction node, if found
				XmlNode node = null;
					
				//Add the old and the new values to the node
				if ( root != null )
				{
					node = root.SelectSingleNode("Map[@field='" + dtOld.Columns[i].ColumnName + "']");
				}

				switch(newColType)
				{
					case "System.UInt16":
					case "System.UInt32":
					case "System.UInt64":
					case "System.Int16":
					case "System.Int32":
					case "System.Int64":
					case "System.Single":
					case "System.Double":
					case "System.Decimal":
					case "System.Boolean":
					case "System.String":
						if ( blnIsEqual == false)
						{
							//Append Tran info
							AppendAttribute(doc,node,objOldVal,objNewVal);

						}
						else
						{
							//Remove the node if it was found
							if ( node != null )
							{
								root.RemoveChild(node);
							}
						}
						break;
					case "System.DateTime":
						if ( blnIsEqual == false)
						{
							//Append Tran info
							string objOldFormatStr = "",objNewFormatStr ="";
							if(dtOld.Columns[i].ExtendedProperties["FORMAT_DATE"]!=null && dtOld.Columns[i].ExtendedProperties["FORMAT_DATE"].ToString()!="" )
								objOldFormatStr = dtOld.Columns[i].ExtendedProperties["FORMAT_DATE"].ToString();

							if(dtNew.Columns[i].ExtendedProperties["FORMAT_DATE"]!=null && dtNew.Columns[i].ExtendedProperties["FORMAT_DATE"].ToString()!="" )
								objNewFormatStr = dtNew.Columns[i].ExtendedProperties["FORMAT_DATE"].ToString();

								
							AppendAttribute(doc,node,objOldVal,objNewVal,newColType,objOldFormatStr,objNewFormatStr);

						}
						else
						{
							//Remove the node if it was found
							if ( node != null )
							{
								root.RemoveChild(node);
							}
						}
						break;
	
					case "System.DBNull":
							
						//Check the datatype of the old value
						//Include column in Update only if the value has changed
						if ( blnIsEqual == false)
						{
							//Added by Swarup & Mohit (11-July-2007)for not showing time in old value if date 
							//Itrack issue no 1968 
							if(colType == "System.DateTime")
							{
								int oldIndex=objOldVal.ToString().IndexOf(" ");	
								string oldValStr=objOldVal.ToString().Substring(0,oldIndex);
								objOldVal = oldValStr;
							}

							AppendAttribute(doc,node,objOldVal ,"null");
						}
						else
						{
							//Remove the node if it was found
							if ( node != null )
							{
								root.RemoveChild(node);
							}
						}
						break;
						
					default: 
						break;
				}
			}
				
				

			//Set transaction XML
			if ( root!= null )
			{
				//Remove nodes which does not have OldValue, NewValue attributes
				XmlNodeList Xlist=root.SelectNodes("//Map") ; 
				//int iCnt

				foreach(XmlNode node in Xlist)
				{
					if ( node.Attributes["OldValue"] == null && node.Attributes["NewValue"] == null )
					{
						root.RemoveChild(node);
					}
				}

				if ( root.HasChildNodes )
				{
					strTranXML = doc.InnerXml;
				}
			}
				
			//Getting the heading xml to be shown in heading during view
			string strHeadingXMl = GetHeadingsXml(dtNew);

			/////////////////////////////////////////////	
				
			if (doc != null && doc.ChildNodes.Count > 0)
			{
				if ( doc.ChildNodes[0].InnerXml.Trim() != "" )
				{
					return "<LabelFieldMapping>" + doc.ChildNodes[0].InnerXml + strHeadingXMl + "</LabelFieldMapping>";
				}
				else
				{
					return strTranXML;
				}

			}
			else
			{
				return strTranXML;
			}
		}
			
		/// <summary>
		/// Returns the xml of heading information for transaction log
		/// </summary>
		/// <param name="dtNew"></param>
		/// <returns></returns>
		private string GetHeadingsXml(DataTable dtNew)
		{
			System.Text.StringBuilder sbHeadXML = new System.Text.StringBuilder();
			string[] strValues;int ctr;
			IDictionaryEnumerator myEnumerator = dtNew.ExtendedProperties.GetEnumerator();

			/*Appending the heading labels, transaction lof xml*/
			while(myEnumerator.MoveNext() )
			{

				sbHeadXML.Append("<Heading>");
				sbHeadXML.Append("<" + myEnumerator.Key + ">");
				//sbHeadXML.Append();
				strValues = myEnumerator.Value.ToString().Split(';');
				for(ctr = 0; ctr < strValues.Length; ctr++)
				{
					if ( dtNew.Columns.Contains(strValues[ctr].Trim()) )
					{
						sbHeadXML.Append( " " + dtNew.Rows[0][strValues[ctr]]);
					}
				}

				sbHeadXML.Append("</" + myEnumerator.Key + ">");
				sbHeadXML.Append("</Heading>");

			}
			return sbHeadXML.ToString();
		}
			
		/// <summary>
		/// Appends the old and new value to a node 
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="node"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		public void AppendOldAttribute(XmlDocument doc,XmlNode node,object oldValue)
		{
			if ( doc!= null && node != null )
			{
				// Create an attribute hello with the value world.
				XmlAttribute OldValue = doc.CreateAttribute("OldValue");
				XmlAttribute NewValue = doc.CreateAttribute("NewValue");

				OldValue.Value = oldValue == System.DBNull.Value ? "null" : oldValue.ToString();
				node.Attributes.SetNamedItem(OldValue);
					
				NewValue.Value = "null";
				node.Attributes.SetNamedItem(NewValue);
			}
		}

		/// <summary>
		/// Appends the old and new value to a node 
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="node"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		public void AppendAttribute(XmlDocument doc,XmlNode node,object oldValue,object newValue)
		{
			if ( doc!= null && node != null )
			{
				// Create an attribute hello with the value world.
				XmlAttribute OldValue = doc.CreateAttribute("OldValue");
				XmlAttribute NewValue = doc.CreateAttribute("NewValue");

				OldValue.Value =GetIntNullFromNegative(oldValue) == System.DBNull.Value ? "null" : oldValue.ToString();// changed by praveer for TFS# 750/996
				node.Attributes.SetNamedItem(OldValue);
					
				NewValue.Value = newValue == System.DBNull.Value ? "null" : newValue.ToString();;
				node.Attributes.SetNamedItem(NewValue);
			}
		}
        public object GetIntNullFromNegative(object intValue)// changed by praveer for TFS# 750/996
        {
            if (intValue.ToString() == Int32.MinValue.ToString())
            {
                return System.DBNull.Value;
            }
            else
            {
                return intValue;
            }

        }
		/// <summary>
		/// Appends the old and new value to a node of Date Column
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="node"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		public void AppendAttribute(XmlDocument doc,XmlNode node,object oldValue,object newValue,string newColType,string oldFormat,string newFormat)
		{
			if ( doc!= null && node != null )
			{
				// Create an attribute hello with the value world.
				XmlAttribute OldValue = doc.CreateAttribute("OldValue");
				XmlAttribute NewValue = doc.CreateAttribute("NewValue");

				string oldValStr="",newValStr="";

				oldValStr=oldValue.ToString();
				newValStr=newValue.ToString();  
					
				//checking whether the datecolumn has to be formatted or not 
				switch (newColType)
				{
					case "System.DateTime":
						if(!oldFormat.Equals("N"))
							if(!oldValue.ToString().Equals(""))
								if(oldValue.ToString().IndexOf(" ")!=-1)
								{	
									int oldIndex=oldValue.ToString().IndexOf(" ");	
									oldValStr=oldValue.ToString().Substring(0,oldIndex); 
								}

						if(!newFormat.Equals("N"))
							if(!newValue.ToString().Equals(""))
								if(newValue.ToString().IndexOf(" ")!=-1)
								{
									int newIndex=newValue.ToString().IndexOf(" ");	
									newValStr=newValue.ToString().Substring(0,newIndex); 
								}
						break;

				}

				OldValue.Value = oldValue == System.DBNull.Value ? "null" : oldValStr;
				node.Attributes.SetNamedItem(OldValue);
					
				NewValue.Value = newValue == System.DBNull.Value ? "null" : newValStr;
				node.Attributes.SetNamedItem(NewValue);
			}
		}
			
		/// <summary>
		/// Gets the transaction XML for Inserts
		/// </summary>
		/// <param name="objModel"></param>
		/// <returns></returns>
		public string GetTransactionLogXML(IModelInfo objModel)
		{
			XmlDocument doc = new XmlDocument();
			
			//Get the Transaction XML from the object
			string strXML = objModel.TransactLabel;
				
			if ( strXML.Trim().Length == 0 )
			{
				throw new Exception("No entries found in the Transaction Log XML.");
			}

			doc.LoadXml(strXML);
					
			XmlElement root = doc.DocumentElement;

			//Get the Table information form the objects
			DataTable dtOld = objModel.TableInfo;
				
			int colCount = dtOld.Columns.Count;
				
			//Generate update statement by looping through all the columns and comparing the 
			//old and new value for each
			for(int i = 0; i < colCount; i++ )
			{
				string colType = dtOld.Rows[0][i].GetType().ToString();
				object objOldVal = dtOld.Rows[0][i];
				//Get the Transaction node, if found
				string ColumnName=dtOld.Columns[i].ColumnName;
				XmlNode node = root.SelectSingleNode("Map[@field='" + dtOld.Columns[i].ColumnName + "']");
				switch(colType)
				{
					case "System.DateTime":
						string objOldFormatStr = "";
						if(dtOld.Columns[i].ExtendedProperties["FORMAT_DATE"]!=null && dtOld.Columns[i].ExtendedProperties["FORMAT_DATE"].ToString()!="" )
							objOldFormatStr = dtOld.Columns[i].ExtendedProperties["FORMAT_DATE"].ToString();   
								
								
						AppendAttribute(doc,node,"" , dtOld.Rows[0][i],colType,"",objOldFormatStr);
						break;
					default:
						if (ColumnName=="COV_DESC"  && dtOld.Rows[0][i]==DBNull.Value && isColumnExist(dtOld,"COVERAGE_CODE"))
							AppendAttribute(doc,node,"" , dtOld.Rows[0]["COVERAGE_CODE"]);
						else
						   AppendAttribute(doc,node,"" , dtOld.Rows[0][i]);
						break;						
				}					
			}
				
			//Set Tran XML 
			if ( root.HasChildNodes )
			{
				//Remove nodes which does not have OldValue, NewValue attributes
				foreach(XmlNode node in root.ChildNodes)
				{
					if ( (node.Attributes["OldValue"] == null && node.Attributes["NewValue"] == null) ||  (node.Attributes["OldValue"].Value == "" && node.Attributes["NewValue"] == null)
						|| (node.Attributes["OldValue"].Value == "null" && node.Attributes["NewValue"].Value == "null") ||  (node.Attributes["OldValue"].Value == "" && node.Attributes["NewValue"].Value == "null")
						)
					{
						root.RemoveChild(node);
					}
				}

				strXML = doc.InnerXml;
			}
			else
			{
				//Set Tran XML to empty string
				strXML = "";
			}

			return strXML;

		}
		public bool isColumnExist(DataTable  myTable,string ColumnName)
		{
			foreach(DataColumn cl in myTable.Columns )
			{
				if (cl.ColumnName==ColumnName)
					return true;
			}
			return false;
		}
		/// <summary>
		/// Returns true if two values are equal; false otherwise
		/// </summary>
		/// <param name="o1"></param>
		/// <param name="o2"></param>
		/// <returns></returns>
		public static bool IsEqual(object obj1,object obj2)
		{

			if (obj1.ToString().Trim() == obj2.ToString().Trim() )
			{
				return true;
			}

			if ( (obj1.ToString().Trim() == "" &&  obj2.ToString().Trim() == "0") 
				|| (obj1.ToString().Trim() == "0" &&  obj2.ToString().Trim() == "") )
			{
				return true;
			}


			if ( obj2.GetType() != obj1.GetType() )
			{
				return false;
			}

			try
			{
				return obj1.Equals(Convert.ChangeType(obj2,obj1.GetType()));
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
	}

		/// <summary>
		/// 
		/// </summary>
		public class ClsSqlColumn
		{
			string columnName = string.Empty;
			string oldValue = string.Empty;
			string currentValue = string.Empty;
			MSSQLType sqlType;
			bool treatLiteral = false;

			public string ColumnName
			{
				get 
				{ 
					return columnName;
				}

				set 
				{ 
					if ( value == null || value.Trim() == String.Empty )
					{
						throw new Exception("Column name cannot be empty.");
					}

					columnName = value; 
				}

			}

			public string OldValue
			{
				get
				{
					return oldValue;
				}
				set
				{
					oldValue = value;
				}
			}

			public string CurrentValue
			{
				get
				{
					return currentValue;
				}
				set
				{
					currentValue = value;
				}
			}

			public MSSQLType SqlType
			{
				get
				{
					return sqlType;
				}
				set
				{
					sqlType = value;
				}
			}

			public bool TreatLiteral
			{
				get { return treatLiteral; }
				set { treatLiteral = value; }
			}

		}
		
		/// <summary>
		/// This Enum represents some of the commonly used types in SQL Server
		/// </summary>
		public enum MSSQLType
		{
			BigInt,
			Int,
			Decimal,
			Float,
			SmallInt,
			TinyInt,
			VarChar,
			NVarChar,
			DateTime,
			Bit
		}
		
	public class ClsSqlColumn1
	{
		string columnName = string.Empty;
		object oldValue = null;
		object currentValue = null;
		//Arun 20-May-2005 Commented labelName,tranLogRequired variable, they are not used anywahere in this class 
		//string labelName = "";
		//bool tranLogRequired = false;
		bool treatLiteral = false;

		public string ColumnName
		{
			get 
			{ 
				return columnName;
			}

			set 
			{ 
				if ( value == null || value.Trim() == String.Empty )
				{
					throw new Exception("Column name cannot be empty.");
				}

				columnName = value; 
			}

		}

		public object OldValue
		{
			get
			{
				return oldValue;
			}
			set
			{
				oldValue = value;
			}
		}

		public object CurrentValue
		{
			get
			{
				return currentValue;
			}
			set
			{
				currentValue = value;
			}
		}

		public bool TreatLiteral
		{
			get { return treatLiteral; }
			set { treatLiteral = value; }
		}
		
	}

	/// <summary>
	/// Returns default values for various data types
	/// </summary>
	public class DefaultValues
	{
		
		public static int GetIntFromString(string strInt)
		{
			if ( strInt.Trim() == "" )
			{
				return -1;
			}
			
			return int.Parse(strInt);
		}

		public static Decimal GetDecimalFromString(string strDecimal)
		{
			if ( strDecimal.Trim() == "" )
			{
				return -1;
			}
			
			return Decimal.Parse(strDecimal);
		}

		public static Double GetDoubleFromString(string strDouble)
		{
			if ( strDouble.Trim() == "" )
			{
				return -1;
			}
			
			return Double.Parse(strDouble);
		}

		public static DateTime GetDateFromString(string strDate)
		{
			if ( strDate.Trim() == "" )
			{
				return DateTime.MinValue;
			}
				
			return DateTime.Parse(strDate);
			
		}

		public static object GetDateNull(DateTime dt)
		{
			if  ( dt == DateTime.MinValue )
			{
				return System.DBNull.Value;
			}
			else
			{
				return dt;
			}

		}

		public static int GetInt(object o)
		{
			return o == DBNull.Value ? 0 : Convert.ToInt32(o);
		}

		public static string GetString(object o)
		{
			return o == DBNull.Value ? "" : Convert.ToString(o);
		}

		public static DateTime GetDateTime(object o)
		{
			return o == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(o);
		}
		
		public static object GetDoubleNull(double doubleValue)
		{
			if ( doubleValue == 0 || doubleValue == -1)
			{
				return System.DBNull.Value;
			}
			else
			{
				return doubleValue;
			}

		}

		public static object GetIntNull(int intValue)
		{
			if ( intValue == 0 || intValue == -1)
			{
				return System.DBNull.Value;
			}
			else
			{
				return intValue;
			}

		}
        public static object GetDoubleNullFromNegativeMiniValue(double doubleValue)
        {
            if (doubleValue == double.MinValue)
            {
                return System.DBNull.Value;
            }
            else
            {
                return doubleValue;
            }

        }
		public static object GetDoubleNullFromNegative(double doubleValue)
		{
			if ( doubleValue < 0 )
			{
				return System.DBNull.Value;
			}
			else
			{
				return doubleValue;
			}

		}

		public static object GetDecimalNullFromNegative(decimal decimalValue)
		{
			if ( decimalValue < 0 )
			{
				return System.DBNull.Value;
			}
			else
			{
				return decimalValue;
			}

		}
		public static object GetIntNullFromNegative(int intValue)
		{
			if ( intValue < 0 )
			{
				return System.DBNull.Value;
			}
			else
			{
				return intValue;
			}

		}

		public static object GetStringNull(string strValue)
		{
			if ( strValue == "" )
			{
				return System.DBNull.Value;
			}
			else
			{
				return strValue;
			}

		}
			
	}

}
