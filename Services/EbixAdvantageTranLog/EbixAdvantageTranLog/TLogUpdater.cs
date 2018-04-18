using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.Configuration;
using System.Xml;


namespace EbixAdvantageTranLog
{
    //Delegate to store store event references
    public delegate void LogUpdateListeners(object sender, LogEventArgs e);
	
    class TLogUpdater: IDisposable
    {
        		//Event that is fired when log updation to database is finished or some error occurs.
		public event LogUpdateListeners LogUpdated;

		// code changed to do away with static dataset
		//static DataSet dsFields=null;		//data set to hold fields information from SVC_TRANSACTION_LOG_FIELDS
        DataSet dsFields=null;		//data set to hold fields information from SVC_TRANSACTION_LOG_FIELDS
		// code change ends

		DataSet dsTransXML=null;//dataset to hold invalid transaction log info.
        public TLogUpdater()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		///
		///Example XML: <LabelFieldMapping>
		///<Map label="Mobile" field="CUSTOMER_MOBILE" OldValue="" NewValue="(414)785-2011" />
		///<Map label="Type" field="CUSTOMER_TYPE" OldValue="" NewValue="11109" />
		///<Map label="Last Name" field="CUSTOMER_LAST_NAME" OldValue="" NewValue="null" />
		///<Map label="Business Description" field="CUSTOMER_BUSINESS_DESC" OldValue="" NewValue="this is a customer for testing customer transaction log data" /><Map label="Zip" field="CUSTOMER_ZIP" OldValue="" NewValue="09888" /><Map label="Pager" field="CUSTOMER_PAGER_NO" OldValue="" NewValue="null" /><Map label="City" field="CUSTOMER_CITY" OldValue="" NewValue="Noida" /><Map label="State" field="CUSTOMER_STATE" OldValue="" NewValue="5" /><Map label="Home Phone" field="CUSTOMER_HOME_PHONE" OldValue="" NewValue="(789)654-1230" /><Map label="Website" field="CUSTOMER_WEBSITE" OldValue="" NewValue="null" /><Map label="Middle Name" field="CUSTOMER_MIDDLE_NAME" OldValue="" NewValue="null" /><Map label="Reason Code 3" field="CUSTOMER_REASON_CODE3" OldValue="" NewValue="11030" /><Map label="Address1" field="CUSTOMER_ADDRESS1" OldValue="" NewValue="B-59 A" /><Map label="Ext" field="CUSTOMER_EXT" OldValue="" NewValue="null" /><Map label="Reason Code 4" field="CUSTOMER_REASON_CODE4" OldValue="" NewValue="11035" /><Map label="Code" field="CUSTOMER_CODE" OldValue="" NewValue="Tran000001" /><Map label="Reason Code 2" field="CUSTOMER_REASON_CODE2" OldValue="" NewValue="11029" /><Map label="Contact Name" field="CUSTOMER_CONTACT_NAME" OldValue="" NewValue="rajan" /><Map label="Suffix" field="CUSTOMER_SUFFIX" OldValue="" NewValue="RA" /><Map label="Fax" field="CUSTOMER_FAX" OldValue="" NewValue="(896)532-1470" /><Map label="Business Type" field="CUSTOMER_BUSINESS_TYPE" OldValue="" NewValue="1761" /><Map label="Address2" field="CUSTOMER_ADDRESS2" OldValue="" NewValue="Sector 60" /><Map label="Reason Code 1" field="CUSTOMER_REASON_CODE" OldValue="" NewValue="11028" /><Map label="First Name" field="CUSTOMER_FIRST_NAME" OldValue="" NewValue="Transaction Log Customer" />
		public void UpdateLog()
		{
			string originalXml = null;

			int index=-1;
			try
			{				
				LoadData();
				XmlDocument xmldoc = new XmlDocument();	
				for(int i=0;i<dsTransXML.Tables[0].Rows.Count;i++)
				{
					index=i;
					originalXml = dsTransXML.Tables[0].Rows[i]["TRANS_DETAILS"].ToString();
					if(dsTransXML.Tables[0].Rows[i]["TRANS_DETAILS"].ToString()!= "" && dsTransXML.Tables[0].Rows[i]["TRANS_DETAILS"]!=null )	
					{
						xmldoc.LoadXml(dsTransXML.Tables[0].Rows[i]["TRANS_DETAILS"].ToString().Replace("&","&amp;") );
						XmlNodeList xlist = xmldoc.SelectNodes("//Map");
						foreach(XmlNode xNode in xlist)
						{
							int response = IsValidNode(xNode);
							if(response==3)
							{
//								if(xmldoc.ChildNodes.Count>1)
//								{
//									foreach(XmlNode xNode in xlist)
//									{
//										XmlNode xParNode=new XmlNode();
//										xParNode=xNode.ParentNode; 
//										xParNode.RemoveChild() 
// 
//									}
//								}
//								else
								if(xmldoc.InnerXml.ToString().IndexOf("<root")==-1)
								{
									xmldoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(xNode);
								}
//								else 
//								{
//									xmldoc.SelectSingleNode("root/LabelFieldMapping").RemoveChild(xNode);
//								}
							}
							else if(response>0)
								ReplaceIdsWithValues(xNode,response,dsTransXML.Tables[0].Rows[i]["LANG_ID"].ToString());
							else
								continue;
						}
						dsTransXML.Tables[0].Rows[i]["TRANS_DETAILS"]=xmldoc.InnerXml;
						dsTransXML.Tables[0].Rows[i]["IS_VALIDXML"] = 'Y';
					}
                    					
				}
                
				UpdateDatabase();
				LogUpdated(this,new LogEventArgs(LogEventArgs.LogStatus.SUCCESS));
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message);
				dsTransXML.Tables[0].Rows[index]["TRANS_DETAILS"] = originalXml ;
				dsTransXML.Tables[0].Rows[index]["IS_VALIDXML"]   = 'E';
				UpdateDatabase();
				LogUpdated(this,new LogEventArgs(LogEventArgs.LogStatus.ERROR,ex));
			}
			finally
			{
				if(dsFields!=null)
					dsFields=null;

				if(dsTransXML!=null)
					dsTransXML=null;
			}
		}

		/// <summary>
		/// Finally this method updates the database with actual data in transaction xml.
		/// </summary>
		private void UpdateDatabase()
		{
			SqlDataAdapter objDataAdapter=null;
			try
			{
				string updateSql = "update MNT_TRANSACTION_XML set TRANS_DETAILS=@TRANS_DETAILS,IS_VALIDXML=@IS_VALIDXML where TRANS_ID=@TRANS_ID";
				objDataAdapter = new SqlDataAdapter();
				objDataAdapter.UpdateCommand = new SqlCommand(updateSql,new SqlConnection(ConfigurationSettings.AppSettings["DB_CON_STRING"].ToString()));
				SqlParameter workParm = objDataAdapter.UpdateCommand.Parameters.Add("@TRANS_ID", SqlDbType.Int);
				workParm.SourceColumn = "TRANS_ID";
				workParm.SourceVersion = DataRowVersion.Original;

				workParm = objDataAdapter.UpdateCommand.Parameters.Add("@TRANS_DETAILS", SqlDbType.VarChar);
				workParm.SourceColumn = "TRANS_DETAILS";
			

				workParm = objDataAdapter.UpdateCommand.Parameters.Add("@IS_VALIDXML", SqlDbType.VarChar);
				workParm.SourceColumn = "IS_VALIDXML";
			

				objDataAdapter.Update(dsTransXML);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message);
				throw ex;
			}
			finally
			{
				if(objDataAdapter!=null)
				{
					if(objDataAdapter.UpdateCommand!=null && objDataAdapter.UpdateCommand.Connection!=null)
						objDataAdapter.UpdateCommand.Connection.Dispose();
					objDataAdapter.Dispose();
				}
			}

		}
		/// <summary>
		/// Recieves an xm node in the format Map label="Mobile" field="CUSTOMER_MOBILE" OldValue="" NewValue="(414)785-2011" />
		/// Returns 0 if node is text only for both old and new attributes, so no replacement of id with value required.
		/// Returns 1 if new value is to be replaced
		/// Returns 2 if old value is to be replaced
		/// Returns 3 if node is to be deleted
		/// </summary>
		/// <param name="xNode"></param>
		/// <returns></returns>
		private int IsValidNode(XmlNode xNode)
		{
			int response = 0;
			
			if(CheckIfNodeToDelete(xNode))
				return 3;

			try
			{
				//int.Parse(xNode.Attributes["NewValue"].Value);
				response++;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message);
			}
			
			try
			{
				//int.Parse(xNode.Attributes["OldValue"].Value);
				response++;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message);
			}
			return response;
		}

		/// <summary>
		/// Recieves a node object and returns true if node is to be deleted from transaction xml
		/// </summary>
		/// <param name="xNode"></param>
		/// <returns></returns>
		private bool CheckIfNodeToDelete(XmlNode xNode)
		{
			string oldValue = "";
			string newValue="";
			try
			{
				oldValue = xNode.Attributes["OldValue"].Value;
				newValue = xNode.Attributes["NewValue"].Value;
			}	
			catch (Exception ex)
			{
				return true;
			}
			//(oldValue.Length==0 && newValue=="0") ||		
			if((oldValue=="0" && newValue=="0") ||
				(oldValue=="0" && newValue.Length==0) ||
				(oldValue=="0" && newValue.ToLower()=="null") ||				
				(oldValue.Length==0 && newValue.ToLower()=="null") ||
				(oldValue.ToLower()=="null" && newValue=="0") ||
				(oldValue.ToLower()=="null" && newValue.Length==0) ||
				(oldValue.ToLower()=="null" && newValue=="-1") ||
				(oldValue=="-1" && newValue=="-1") ||
				(oldValue.Length==0 && newValue=="-1")

				)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		/// <summary>
		/// If a node is valid i.e. contains some primary keys then there value is replaced by this functrion.
		/// </summary>
		/// <param name="xNode"></param>
		/// <param name="response"></param>
		private void ReplaceIdsWithValues(XmlNode xNode,int response,string LangId)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				objDataWrapper = new DataWrapper(System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"],CommandType.Text);
				DataTable dtFields =  dsFields.Tables[0];
				dtFields.DefaultView.Sort = "FIELD_NAME";
				
				int index = dtFields.DefaultView.Find(xNode.Attributes["field"].Value);
				
				
				string[] ids = {null,null};
				
				
				if(index>=0)
				{
					//newvalue
					ids[0] = xNode.Attributes["NewValue"].Value;
					//oldvalue
					if(response==2)//replacement is required for both old and new vals;
						ids[1] = xNode.Attributes["OldValue"].Value;
					//DataTable objDataTable = GetIDValue(dtFields.DefaultView[index],ids);
					DataSet objDataSet = GetIDValue(dtFields.DefaultView[index],ids,LangId);

					if(objDataSet!=null && objDataSet.Tables.Count>0)
					{
						if(objDataSet.Tables[0].Rows.Count>0)
						{
							xNode.Attributes["NewValue"].Value = objDataSet.Tables[0].Rows[0]["Description"].ToString();
						}

						if(objDataSet.Tables.Count>1)
						if(objDataSet.Tables[1].Rows.Count>0)
						{
							xNode.Attributes["OldValue"].Value = objDataSet.Tables[1].Rows[0]["Description"].ToString();
						}							
					}
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message);
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}
		}

		/// <summary>
		/// this function recieves a data row of SVC_TRANSACTION_LOG_FIELDS table that 
		/// is exact match with field name in tran xml and coreesponding id(s); based on all this 
		/// information actual value of id is retrieved from database and is returnened in a datatable
		/// </summary>
		/// <param name="row"></param>
		/// <param name="ids"></param>
		/// <returns></returns>
		private DataSet GetIDValue(DataRowView row,string[] ids,string LangId)
		{
			DataWrapper objDataWrapper=null;
			//DataTable objTable=null;
			DataSet objDataSet=null;
			try
			{
				objDataWrapper = new DataWrapper(System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"],CommandType.Text);
				int idsCount=0;
				string sql ="";

				for(idsCount=0;idsCount<ids.Length;idsCount++)
				{
					if(ids[idsCount] != null && ids[idsCount] != "")
					{
						sql += "select top 1 "+row["VALUE_DESC1"];
						if(row["VALUE_DESC2"]!=DBNull.Value && row["VALUE_DESC2"].ToString().Length>0)
							sql+= "+' '+"+row["VALUE_DESC2"];
						sql+=" as Description ";
						if(LangId=="" || LangId=="1")
							sql+= " from "+row["TABLE_NAME"]+" where ";					
						else
						{
							if(row["M_TABLE_NAME"]!=DBNull.Value && row["M_TABLE_NAME"].ToString() !="")
								sql+= " from "+row["M_TABLE_NAME"]+" where LANG_ID=" + LangId + " and ";
							else
								sql+= " from "+row["TABLE_NAME"]+" where ";					
						}
						//sql+= " from "+row["TABLE_NAME"]+" where ";					
						sql+= row["VALUE_ID"]+" = '"+ids[idsCount] + "'" ; 
						if(row["VALUE_ID1"]!=DBNull.Value && row["VALUE_ID1"].ToString().Length>0)	
							sql+= " and " + row["VALUE_ID1"]	+ " ; ";
						else 
							sql+= " ; ";

					}
				}

				if(!sql.Equals(""))
					objDataSet =  objDataWrapper.ExecuteDataSet(sql);	

			}
			catch(Exception ex){System.Diagnostics.Debug.Write(ex.Message);}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}
			return objDataSet;
		}

		/// <summary>
		/// initialises datasets of invalid transaction xml and of fields information
		/// </summary>
		private void LoadData()
		{
			DataWrapper objDataWrapper=null;
			try
			{
				objDataWrapper = new DataWrapper(System.Configuration.ConfigurationSettings.AppSettings["DB_CON_STRING"].ToString().Trim()  ,CommandType.Text);
				string recCount=System.Configuration.ConfigurationSettings.AppSettings["RECORD_COUNT"].ToString(); 
				//dsTransXML = objDataWrapper.ExecuteDataSet("select top " + recCount + " * from MNT_TRANSACTION_XML where (isnull(IS_VALIDXML,'N') <> 'Y' AND isnull(IS_VALIDXML,'N') <> 'E') and (trans_details is not null )");
				dsTransXML = objDataWrapper.ExecuteDataSet("select top " + recCount + " TXML.*,TLOG.LANG_ID from MNT_TRANSACTION_XML TXML (nolock) join MNT_TRANSACTION_LOG TLOG  (nolock) ON TXML.TRANS_ID=TLOG.TRANS_ID where (isnull(IS_VALIDXML,'N') <> 'Y' AND isnull(IS_VALIDXML,'N') <> 'E') and (trans_details is not null )");
				if(dsFields==null)
					dsFields = objDataWrapper.ExecuteDataSet("select * from SVC_TRANSACTION_LOG_FIELDS where is_active='Y'");
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message);
				throw ex;
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}
		}

		public void Dispose()
		{
			if(dsTransXML!=null)
				dsTransXML=null;
		}
    }
}
