/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	7/5/2005 11:14:26 AM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 

using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Maintenance;
using Cms.DataLayer;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsCoverageRange.
	/// </summary>
	public class ClsCoverageRange :Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		MNT_COVERAGE_RANGES			=	"MNT_COVERAGE_RANGES";
		private bool boolTransactionRequired			= true;
		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateCoverage";
		#endregion

		#region Public Properties
		public bool TransactionLog
		{
			set
			{
				boolTransactionLog	=	value;
			}
			get
			{
				return boolTransactionLog;
			}
		}
		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}
			set
			{
				boolTransactionRequired=value;
			}
		}

		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsCoverageRange()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			//base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add/Update Method 
		public int Add(System.Collections.ArrayList arrRanges,string strOldXML)
		{
			string strSaveProc = "Proc_InsertCoverageRange";
			string strUpdateProc="Proc_UpdateCoverageRange";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

			try
			{
				for(int i=0;i<arrRanges.Count;i++)
				{
					ClsCoverageRangeInfo objInfo =(ClsCoverageRangeInfo) arrRanges[i];
					objDataWrapper.AddParameter("@COV_ID",objInfo.COV_ID);
					objDataWrapper.AddParameter("@LIMIT_DEDUC_TYPE",objInfo.LIMIT_DEDUC_TYPE );
					if(objInfo.RANK==0)
						objDataWrapper.AddParameter("@RANK",DBNull.Value);
					else
						objDataWrapper.AddParameter("@RANK",objInfo.RANK );
					if(objInfo.LIMIT_DEDUC_AMOUNT == 0)
						objDataWrapper.AddParameter("@LIMIT_DEDUC_AMOUNT",DBNull.Value);
					else
						objDataWrapper.AddParameter("@LIMIT_DEDUC_AMOUNT",objInfo.LIMIT_DEDUC_AMOUNT );
					if(objInfo.LIMIT_DEDUC_AMOUNT1 == 0)
						objDataWrapper.AddParameter("@LIMIT_DEDUC_AMOUNT1",DBNull.Value);
					else
						objDataWrapper.AddParameter("@LIMIT_DEDUC_AMOUNT1",objInfo.LIMIT_DEDUC_AMOUNT1);
					objDataWrapper.AddParameter("@LIMIT_DEDUC_AMOUNT_TEXT",objInfo.LIMIT_DEDUC_TEXT );
					objDataWrapper.AddParameter("@LIMIT_DEDUC_AMOUNT1_TEXT",objInfo.LIMIT_DEDUC_TEXT1 );
					objDataWrapper.AddParameter("@IS_ACTIVE",1);
					if(objInfo.EFFECTIVE_FROM_DATE.Ticks !=0)
						objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objInfo.EFFECTIVE_FROM_DATE);
					else
						objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",null);
					if(objInfo.EFFECTIVE_TO_DATE.Ticks != 0)
						objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objInfo.EFFECTIVE_TO_DATE);
					else
						objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",null);
					if(objInfo.DISABLED_DATE.Ticks !=0)
						objDataWrapper.AddParameter("@DISABLED_DATE",objInfo.DISABLED_DATE);
					else
						objDataWrapper.AddParameter("@DISABLED_DATE",null);

					
					string strTranXML = "";
					objInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddCoverageRange.aspx.resx");
					
					if(objInfo.LIMIT_DEDUC_ID == 0)
					{
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objInfo);
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Coverage range added";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						sbTranXML.Append(strTranXML);
						objDataWrapper.ExecuteNonQuery(strSaveProc );
					}
					else
					{
						objDataWrapper.AddParameter("@LIMIT_DEDUC_ID",objInfo.LIMIT_DEDUC_ID);
						
						strTranXML = this.GetTranXML(objInfo,strOldXML,objInfo.LIMIT_DEDUC_ID,root);
						
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY ;
						objTransactionInfo.TRANS_DESC		=	"Coverage range modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						sbTranXML.Append(strTranXML);
						objDataWrapper.ExecuteNonQuery(strUpdateProc);
					}
				
					objDataWrapper.ClearParameteres();

				
				}
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			} 
			
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}
		#endregion

		#region GetTranXML method to fetch transaction XML 
		protected string GetTranXML(ClsCoverageRangeInfo  objNew,string xml,int limit_deducID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[LIMIT_DEDUC_ID=" + limit_deducID.ToString() + "]");
						
			ClsCoverageRangeInfo objOld = new ClsCoverageRangeInfo();
			objOld.LIMIT_DEDUC_ID=objNew.LIMIT_DEDUC_ID;
						
			XmlNode element = null;
			
			element = node.SelectSingleNode("COV_ID");

			if ( element != null)
			{
				objOld.COV_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml)));
			}

			element = node.SelectSingleNode("RANK");

			if ( element != null)
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.RANK  = Convert.ToInt32(element.InnerXml);
				}
			}
						
			element = node.SelectSingleNode("LIMIT_DEDUC_AMOUNT");
						
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.LIMIT_DEDUC_AMOUNT  = Convert.ToDecimal  (ClsCommon.DecodeXMLCharacters(element.InnerXml));
				}
			}
					
			element = node.SelectSingleNode("LIMIT_DEDUC_AMOUNT1");
			if ( element != null )
			{
				string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
				if ( str != "" )
				{
					objOld.LIMIT_DEDUC_AMOUNT1  = Convert.ToDecimal(str);
				}
			}

			element = node.SelectSingleNode("LIMIT_DEDUC_AMOUNT_TEXT");
			if ( element != null )
			{
				objOld.LIMIT_DEDUC_TEXT = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
				
			}
			element = node.SelectSingleNode("LIMIT_DEDUC_AMOUNT1_TEXT");
			if ( element != null )
			{
				objOld.LIMIT_DEDUC_TEXT1  = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
				
			}
			element = node.SelectSingleNode("EFFECTIVE_FROM_DATE");
			if ( element != null )
			{
				string str= ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
				if(str != "")
				{
					objOld.EFFECTIVE_FROM_DATE =Convert.ToDateTime(str);

				}
				
			}
			element = node.SelectSingleNode("EFFECTIVE_TO_DATE");
			if ( element != null )
			{
				string str= ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
				if(str != "")
				{
					objOld.EFFECTIVE_TO_DATE =Convert.ToDateTime(str);

				}
				
			}
			element = node.SelectSingleNode("DISABLED_DATE");
			if ( element != null )
			{
				string str= ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
				if(str != "")
				{
					objOld.DISABLED_DATE  =Convert.ToDateTime(str);

				}
				
			}
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

			string strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);

			return strTranXML;
		}
		#endregion


		public static DataTable GetCoverageRangeXml(int covId,string isDeactiveInclude,string type,int currentPage,int pageSize,out int totalRecords)
		{			
			string strProcedure = "Proc_GetCoverageRangeXml";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				
				objDataWrapper.AddParameter("@COV_ID",covId);
				objDataWrapper.AddParameter("@Is_DeactiveInclude",isDeactiveInclude);
				objDataWrapper.AddParameter("@LIMIT_DEDUC_TYPE",type);
				objDataWrapper.AddParameter("@CurrentPage",currentPage);
				objDataWrapper.AddParameter("@PageSize",pageSize);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@TotalRecords",SqlDbType.Int,ParameterDirection.Output);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				totalRecords=int.Parse(objSqlParameter.Value.ToString());
				return	objDataSet.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}			
		}
		

		
		
		
		public static void ActivateDeactivateCoverageLimit(DataTable dt,string status)
		{		
			string	strStoredProc = "Proc_ActivateDeactivateCoverageRange";
			int result=0;
			try
			{				
				for(int i=0;i < dt.Rows.Count;i++)
				{
					DataRow dr=dt.Rows[i];
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@LIMIT_DEDUC_ID",int.Parse(dr["LIMIT_DEDUC_ID"].ToString()));
					objDataWrapper.AddParameter("@STATUS",status);
					result=objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				//return result;			
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}

		
	}
}

