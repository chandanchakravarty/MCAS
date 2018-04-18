﻿/******************************************************************************************
	<Author					: - Pravesh K Chandel>
	<Start Date				: -	March 26, 2010>
	<End Date				: - >
	<Description			: - A wrapper to clsdatalyer, which will handle transaction>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;


namespace Cms.EbixDataLayer
{
	public class DataWrapper: SqlHelper,IDisposable
	{
		public delegate void SetTransId(int TransID);
		public static SetTransId SetID;
		public delegate void SetEndorsementTransIds(string TransIDs);
		public static SetEndorsementTransIds SetEndorsementIDs;
	
		//Ravindra(07-02-2008); To set CommandTimeOut

		/// <summary>
		/// Get or set Command Time out for DataAccess. Default value is "0" which means system will wait 
		/// infinitely for command exectuion. If modified further execution using DAL will use new value
		/// throughout the application domain. 
		/// </summary>
		public int CommandTimeout
		{
			set
			{
				mCommandTimeOut = value;
			}
			get
			{
				return mCommandTimeOut ;
			}

		}

		#region "Enums"
		public enum MaintainTransaction{YES,NO};
		public enum EDispose{YES,NO};
		public enum CloseConnection{YES,NO};
		public enum SetAutoCommit{ON,OFF};
		#endregion

		#region "Private Instance Variables"
		private string strConString;
        private static string strDBConString;
		private CommandType objCommandType;
		private Hashtable transactionCache;
		private ArrayList parameterCollection;
		private const string DEFAULT_TRANSACTION_NAME="Default Transaction";
		private MaintainTransaction transactionRequired=MaintainTransaction.NO;
		private SqlConnection objSqlConnection;
		private SetAutoCommit autoCommit=SetAutoCommit.OFF;
		private SqlParameter[] commandParameters;
		private string sbTransactionIDs;
		private SqlDataAdapter sqlDataAdapter;
		#endregion

		#region "Public Properties"
        public static string ConnString
        {
            get { return strDBConString; }
            set { strDBConString = value; }
        }
		public string ConnectionString
		{
			get	{return strConString;}
			set	{strConString = value;}
		}
		public CommandType CommandType
		{
			get	{return objCommandType;}
			set	{objCommandType = value;}
		}
		public SqlConnection WrapperSqlConnection
		{
			get	{return objSqlConnection;}
			set	{objSqlConnection = value;}
		}
		public MaintainTransaction TransactionRequired
		{
			get{return TransactionRequired;}
			set{transactionRequired=value;}
		}
		public SetAutoCommit AutoCommit
		{
			get{return autoCommit; }
			set{autoCommit=value;}
		}
		public string TransactionIDs
		{
			get{return sbTransactionIDs; }
			set{sbTransactionIDs = value ;}
		}
		public SqlParameter[] CommandParameters
		{
			get
			{
				try
				{
					object[] objParams= parameterCollection.ToArray();
					commandParameters=new SqlParameter[objParams.Length];
					for(int i=0;i<objParams.Length;i++)
						commandParameters[i]=(SqlParameter)objParams[i];
					return commandParameters;
				}catch(Exception objException)
				{
					throw new Exception("Error while executing non-Query!",objException);
				}
			}
			set
			{
				try
				{
					commandParameters=value;
					for(int i=0;i<commandParameters.Length;i++)
						parameterCollection.Add(commandParameters[i]);
				}catch(Exception objException)
				{
					throw new Exception("Error while executing non-Query!",objException);
				}
			}
		}
		#endregion

		#region "Constructors & EDispose"
		/// <summary>
		/// Is used initialize data wrapper object if a non-transaction query(s) is/are to be excuted.
		/// </summary>
		/// <param name="strConString">Conection string</param>
		/// <param name="objCommandType"></param>
		public DataWrapper(string strConString,CommandType objCommandType)//case 1 & 4
		{
            if (strConString == null || strConString == "")
                strConString = ConnString; //System.Configuration.ConfigurationSettings.AppSettings.Get("DB_CON_STRING");
			ConnectionString = strConString;
			CommandType = objCommandType;
			transactionCache =  new Hashtable();
			parameterCollection =  new ArrayList();

		}
		/// <summary>
		/// Is used initialize data wrapper object if a transactional query(s) is/are to be excuted.
		/// Uses a default system default transaction Name.
		/// </summary>
		/// <param name="strConString"></param>
		/// <param name="objCommandType"></param>
		/// <param name="transactionRequired"></param>
		/// <param name="AutoCommit"></param>
		//case:2
		public DataWrapper(string strConString,CommandType objCommandType,MaintainTransaction transactionRequired,
						   SetAutoCommit AutoCommit):this(strConString,objCommandType,transactionRequired,
			               DEFAULT_TRANSACTION_NAME,AutoCommit){ }

		/// <summary>
		/// Is used initialize data wrapper object if a transactional query(s) is/are to be excuted.
		/// Transaction Name(s) is/are specified by user(Business logic).
		/// This method is used in case multiple transactions are to be maintained simultaneously.
		/// </summary>
		/// <param name="strConString"></param>
		/// <param name="objCommandType"></param>
		/// <param name="transactionRequired"></param>
		/// <param name="transactionName"></param>
		/// <param name="AutoCommit"></param>
		public DataWrapper(string strConString,CommandType objCommandType,MaintainTransaction transactionRequired,
			               string transactionName,SetAutoCommit AutoCommit):this(strConString,objCommandType)
		{
			this.TransactionRequired=transactionRequired;
			//getTransactionObject(transactionName);
			this.AutoCommit=AutoCommit;
		}
		public void Dispose()
		{
			CloseSqlConnection(EDispose.YES);
		}
		#endregion

		#region "Constants"
		const string TRANSACTIONSTOREDPROC = "Proc_InsertTransactionLog";
		#endregion

		#region "Execute Non-query"
		//Case 1: Single query/SP with no -child or transaction dependencies.
		//Case 4: Multiple insert/update queries/SPs with both parent-child and transaction dependencies between queries.
		//Case four is also catered here as dependecy has to be maintained by BL and only single query is executed at one time.
		/// <summary>
		/// Executes the Non-Query (insert/update/SP) passesd to it.
		/// </summary>
		/// <param name="sql">sql is the Any Non-Query (insert/update) or stored procedure name.</param>
		/// <returns>No. of rows affeted by the Non-Query.</returns>
		public int ExecuteNonQuery(string sql)
		{
			string SPName = "";
			try
			{
				if(objCommandType == CommandType.StoredProcedure)
					SPName = sql;
				int intResult;
				OpenConnection();
				if(transactionRequired==MaintainTransaction.YES)
				{
					//extracting default transaction from cahce this logic is hidden form BL programmer.
					intResult = ExecuteNonQuery(GetTransactionObject(DEFAULT_TRANSACTION_NAME),CommandType,sql);
					//transaction can not be auto commited here for further calls.
					if(AutoCommit==SetAutoCommit.ON) CommitTransaction(CloseConnection.YES);
				}
				else
				{
					intResult = ExecuteNonQuery(WrapperSqlConnection,CommandType,sql);
					CommitTransaction(CloseConnection.YES);
					//CloseSqlConnection(EDispose.YES);
				}
		
				return intResult;
			}
			catch(Exception objException)
			{
				CloseSqlConnection(EDispose.YES);

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objException);
				throw new Exception("Error while executing non-Query! Store procedure :  " + SPName ,objException);
			}
		}
		
// THIS METHOD IS REMOVED AFTER ASHISH SUGGESTED THAT PARALLEL TRANSACTIONS ARE NOT FEASIBLE AND SHOULD NOT BE USED.
//		/// <summary>
//		/// Executes non-query in transaction, saves transactionName in cache for further call. 
//		/// TransactionRequired property must be set to true for transaction mangamenmet.
//		/// </summary>
//		/// <param name="sql">sql is the Any Non-Query (insert/update) or stored procedure name.</param>
//		/// <param name="transactionName">Name of the transaction in which query will execute.</param>
//		/// <returns>No. of rows affeted by the Non-Query.</returns>
//		public int ExecuteNonQuery(string sql,string transactionName)//Case 1 & 4+ cache is required:
//		{
//			try
//			{
//				OpenConnection();
//				int intResult;
//				if(transactionRequired==MaintainTransaction.YES)
//				{
//					// since transaction NAME is being passed to this method it is Obivious that transaction is required.
//					//extraction transaction from cache and executing query 
//					//transaction object is maintained in cache for further calls
//					intResult = ExecuteNonQuery(GetTransactionObject(transactionName),CommandType,sql);
//					if(AutoCommit==SetAutoCommit.ON) CommitTransaction(CloseConnection.YES);
//				}
//				else
//					intResult =	ExecuteNonQuery(sql);
//				return intResult;
//			}catch(Exception objException)
//			{
//				CloseSqlConnection(EDispose.YES);
//				throw new Exception("Error while executing non-Query!",objException);
//			}
//		}

		//Case 2: Multiple insert/update queries/SPs with no parent-child or transaction dependencies.
		//Case 3: Multiple insert/update queries/SPs with no parent-child but transaction dependency between queries.
		/// <summary>
		/// Recieves an array of SQL strings and executes them in batch.
		/// </summary>
		/// <param name="sql">sqlArray is array of multiple SQL strings to be executed in a batch(transactional or non-transactional).</param>
        public void ExecuteNonQuery(string[] sqlArray)
		{
			string SPName = "";
			try
			{
				OpenConnection();
				if(transactionRequired==MaintainTransaction.YES)
				{
					//A string builder can not be used else transaction can not be maintained between queries.
					for(int i=0;i<sqlArray.Length;i++)
					{
						if(objCommandType == CommandType.StoredProcedure)
							SPName = sqlArray[i];
						ExecuteNonQuery(GetTransactionObject(DEFAULT_TRANSACTION_NAME),CommandType,sqlArray[i]);
					}
					if(AutoCommit==SetAutoCommit.ON) CommitTransaction(CloseConnection.YES);
				}
				else
				{
					StringBuilder strTemp = new StringBuilder();
					for(int i=0;i<sqlArray.Length;i++)
						strTemp.Append(sqlArray[i]+";");
					ExecuteNonQuery(WrapperSqlConnection,CommandType,strTemp.ToString());
					CommitTransaction(CloseConnection.YES);
					//CloseSqlConnection(EDispose.YES);
				}	
			}catch(Exception objException)
			{
				CloseSqlConnection(EDispose.YES);
				throw new Exception("Error while executing non-Query! Store Proc : " + SPName ,objException);
			}
				
		}
			
		
// THIS METHOD IS REMOVED AFTER ASHISH SUGGESTED THAT PARALLEL TRANSACTIONS ARE NOT FEASIBLE AND SHOULD NOT BE USED.
//		/// <summary>
//		/// Recieves an array of SQL strings and executes them in batch. 
//		/// </summary>
//		/// <param name="sql"> SQl string array.</param>
//		/// <param name="transactionName">Name of the transaction in which all queries will execute.</param>
//		public void ExecuteNonQuery(string[] sql,string transactionName)//case 2 & 3 + cache is required:
//		{
//			try
//			{
//				OpenConnection();
//				if(transactionRequired==MaintainTransaction.YES)
//				{
//					//A string builder can not be used else transaction can not be maintained between queries.
//					for(int i=0;i<sql.Length;i++)
//						ExecuteNonQuery(GetTransactionObject(transactionName),CommandType,sql[i]);
//					if(AutoCommit==SetAutoCommit.ON) CommitTransaction(CloseConnection.YES);
//				}
//				else //since transactionRequired property is set to false queries will be executed non-transactional.
//				{
//					ExecuteNonQuery(sql);
//				}
//			}catch(Exception objException)
//			{
//				CloseSqlConnection(EDispose.YES);
//				throw new Exception("Error while executing non-Query!",objException);
//			}
//			
//		}

		/// <summary>
		/// Executes a  Stored Procedure or any No-Query and executes a stored procedure to maintain transaction log.
		/// </summary>
		/// <param name="sql"></param> Name of Stored Procedure of any No-Query
		/// <param name="transactionTypeId"></param>
		/// <param name="description"></param>
		/// <param name="userId"></param>
		/// <param name="changeXML"></param>
		/// <returns></returns>
		/*public int ExecuteNonQuery(string sql,Cms.Model.Maintenance.ClsTransactionInfo objTransaction)
		{
			int result;
			//Executing user defined Non-Query
			result=ExecuteNonQuery(sql);
			if(result>0)
			{
				//if(objTransaction.CHANGE_XML!=null && (objTransaction.CHANGE_XML=="" || objTransaction.CHANGE_XML=="<LabelFieldMapping></LabelFieldMapping>"))
				//	return result;
				ExecuteNonQuery(objTransaction);
			}
			return result;
		}
        */

        //public void ExecuteNonQuery(Cms.Model.Maintenance.ClsTransactionInfo objTransaction)
        //{
        //    //clearing parameters that may be set by user to pass to No-Query
        //    this.ClearParameteres();

        //    //storing command type set by user in temporary variable.
        //    CommandType cmdType = this.CommandType;

        //    //Executing transaction log stored procedure
        //    this.CommandType = CommandType.StoredProcedure;
        //    DateTime	RecordDate		=	DateTime.Now;

        //    /*this.AddParameter("@Tran_Id",objTransaction.TRANS_TYPE_ID);
        //        this.AddParameter("@log_time",RecordDate);
        //        this.AddParameter("@user_id",objTransaction.RECORDED_BY);
        //        this.AddParameter("@description",objTransaction.TRANS_DESC);
        //        this.AddParameter("@changes_xml",objTransaction.CHANGE_XML);*/
				
        //    //code commented and added by Ashwani
        //    this.AddParameter("@TRANS_TYPE_ID",objTransaction.TRANS_TYPE_ID);
        //    this.AddParameter("@RECORD_DATE_TIME",RecordDate);
        //    this.AddParameter("@CLIENT_ID",objTransaction.CLIENT_ID);
        //    this.AddParameter("@TRANS_DESC",objTransaction.TRANS_DESC);
        //    if(objTransaction.CHANGE_XML !=null)
        //    {
        //        if(!objTransaction.CHANGE_XML.Trim().Equals(""))
        //            this.AddParameter("@CHANGE_XML",objTransaction.CHANGE_XML);
        //        else
        //            this.AddParameter("@CHANGE_XML",System.DBNull.Value);
        //    }
        //    else
        //            this.AddParameter("@CHANGE_XML",objTransaction.CHANGE_XML);

        //    this.AddParameter("@POLICY_ID",objTransaction.POLICY_ID);
        //    this.AddParameter("@POLICY_VER_TRACKING_ID",objTransaction.POLICY_VER_TRACKING_ID);
        //    this.AddParameter("@RECORDED_BY",objTransaction.RECORDED_BY);
        //    this.AddParameter("@RECORDED_BY_NAME",objTransaction.RECORDED_BY_NAME);
        //    this.AddParameter("@ENTITY_ID",objTransaction.ENTITY_ID);
        //    this.AddParameter("@ENTITY_TYPE",objTransaction.ENTITY_TYPE);
        //    this.AddParameter("@IS_COMPLETED",objTransaction.IS_COMPLETED);
        //    this.AddParameter("@APP_ID",objTransaction.APP_ID);
        //    this.AddParameter("@APP_VERSION_ID",objTransaction.APP_VERSION_ID);
        //    this.AddParameter("@QUOTE_ID",objTransaction.QUOTE_ID);
        //    this.AddParameter("@QUOTE_VERSION_ID",objTransaction.QUOTE_VERSION_ID);
        //    this.AddParameter("@CUSTOM_INFO",objTransaction.CUSTOM_INFO);
        //    SqlParameter objParam = (SqlParameter) this.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);
        //    ExecuteNonQuery(TRANSACTIONSTOREDPROC);

        //    objTransaction.TransID = Convert.ToInt32(objParam.Value);
			
			
        //    if (SetID != null)
        //    {
        //        SetID(objTransaction.TransID);
        //    }
        //        this.TransactionIDs=objTransaction.TransID.ToString();
        //    if (SetEndorsementIDs!=null)
        //        SetEndorsementIDs(this.TransactionIDs);
				
	
        //    //Restoring command type set by user
        //    this.CommandType = cmdType;
        //}
  		#endregion

		#region "Execute DataSet"
		/// <summary>
		/// Executes a dataset for the query or queries passed in sql parameter.
		/// User can add parameters using add parameter method.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public DataSet ExecuteDataSet(string sql)	
		{
			DataSet objDataSet;
			//Ravindra(03-18-2008): If wrapper has already open connection use it else create new
			//if always creating a new conncetion this will not be use same transaction label
			//some SPs which returns RecordSet also requires to updated some tables.
			if(CommandParameters==null || CommandParameters.Length==0)
			{
				if(WrapperSqlConnection != null && WrapperSqlConnection.State != ConnectionState.Closed)
				{
					objDataSet = SqlHelper.ExecuteDataset(WrapperSqlConnection,GetTransactionObject(DEFAULT_TRANSACTION_NAME),this.CommandType,sql);
				}
				else
				{
					objDataSet=SqlHelper.ExecuteDataset(ConnectionString ,this.CommandType, sql);
				}
			}
			else
			{
				if(WrapperSqlConnection != null && WrapperSqlConnection.State != ConnectionState.Closed)
				{
					objDataSet = SqlHelper.ExecuteDataset(WrapperSqlConnection,GetTransactionObject(DEFAULT_TRANSACTION_NAME),this.CommandType,sql,commandParameters );
				}
				else
				{
					objDataSet=SqlHelper.ExecuteDataset(ConnectionString,this.CommandType,sql,CommandParameters);
				}
				
			}
			return objDataSet;
		}
		/// <summary>
		/// Executes a dataset for the query or queries passed in sql array.
		/// User can add parameters using add parameter method.
		/// </summary>
		/// <param name="sqlArray"></param>
		/// <returns></returns>
		public DataSet ExecuteDataSet(string[] sqlArray)	
		{
			string sql ="";
			for(int i=0;i<sqlArray.Length;i++)
				sql += sqlArray[i]+";";
			return ExecuteDataSet(sql);
		}
		#endregion
		//by pravesh
		#region fill dataset
		public  void FillDataset(string commandText, DataSet dataSet, string[] tableNames)
		{

			//objDataSet=SqlHelper.ExecuteDataset(ConnectionString,this.CommandType,commandText,CommandParameters);
			//return objDataSet;
			SqlCommand command = new SqlCommand();
			bool mustCloseConnection = false;
			SqlTransaction objTransaction=GetTransactionObject(DEFAULT_TRANSACTION_NAME);
           	//SqlHelper.FillDataset(connection,commandText,dataSet,tableNames,commandParameters);
			SqlHelper.PrepareCommand(command, this.WrapperSqlConnection	,objTransaction , this.CommandType, commandText, CommandParameters, out mustCloseConnection );
    		sqlDataAdapter=new SqlDataAdapter(command);
			// Add the table mappings specified by the user
			if (tableNames != null && tableNames.Length > 0)
			{
				string tableName = "Table";
				for (int index=0; index < tableNames.Length; index++)
				{
					if( tableNames[index] == null || tableNames[index].Length == 0 ) throw new ArgumentException( "The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames" );
					sqlDataAdapter.TableMappings.Add(tableName, tableNames[index]);
					tableName += (index + 1).ToString();
				}
			}	
		
			sqlDataAdapter.Fill(dataSet);
			//SqlHelper.FillDataset(ConnectionString,commandType,commandText,dataSet,tableNames,CommandParameters);
		}
		#endregion
		#region  updatedateSet
		public int UpdateDataset(DataSet dsChangedDataSet,string strTableName)	
		{
			try
			{
				SqlCommandBuilder objCmdBuider = new SqlCommandBuilder(sqlDataAdapter);
				
				//sqlDataAdapter.Update(dsChangedDataSet,strTableName);
				//dsChangedDataSet.AcceptChanges();
				SqlCommand insertCommand =objCmdBuider.GetInsertCommand();
				SqlCommand UpdateCommand =objCmdBuider.GetUpdateCommand();
				SqlCommand DeleteCommand =objCmdBuider.GetDeleteCommand();
				SqlHelper.UpdateDataset(insertCommand,DeleteCommand,UpdateCommand,dsChangedDataSet,strTableName);
				return 1;
			}
			catch(Exception ex)
			{
				throw(ex);
				//return -1;
			}
			
		}

		#endregion

		#region "Public Utility Methods"
		/// <summary>
		/// Commits only default Transaction started by application.
		/// </summary>
		public void CommitTransaction(CloseConnection closeConnection)
		{
			CommitTransaction(DEFAULT_TRANSACTION_NAME,closeConnection);
		}
		/// <summary>
		/// Commits the specified Transaction, closes the connection & removes transaction form cache.
		/// </summary>
		/// <param name="transactionName">Name of the transaction to commit.</param>
		public void CommitTransaction(string transactionName,CloseConnection closeConnection)
		{
			try
			{
				SqlTransaction objSqlTransaction = (SqlTransaction) transactionCache[transactionName];
				if(objSqlTransaction!=null)
				{
					objSqlTransaction.Commit();
					transactionCache.Remove(transactionName);
				}
				if(closeConnection==CloseConnection.YES)
					CloseSqlConnection(EDispose.YES);				
			}
			catch(Exception objException)
			{
				throw new Exception("CAN NOT COMMIT! "+objException.ToString(),objException);
			}
		}
		/// <summary>
		/// Explicitely Rolls Back the system's default Transaction, closes the connection & removes transaction form cache.
		/// </summary>
		public void RollbackTransaction(CloseConnection closeConnection)
		{
			RollbackTransaction(DEFAULT_TRANSACTION_NAME,closeConnection);
		}
		/// <summary>
		/// Explicitely Rolls Back the specified Transaction, closes the connection & removes transaction form cache.
		/// </summary>
		/// <param name="transactionName"></param>
		public void RollbackTransaction(string transactionName,CloseConnection closeConnection)
		{
			try
			{
				SqlTransaction objSqlTransaction = (SqlTransaction) transactionCache[transactionName];
				if(objSqlTransaction!=null)
				{
					objSqlTransaction.Rollback();
					transactionCache.Remove(transactionName);
				}
				if(closeConnection==CloseConnection.YES)
					CloseSqlConnection(EDispose.YES);
			}
			catch(Exception objException)
			{
				throw new Exception("Error while Rollback! "+objException.ToString(),objException);
			}
		}
		/// <summary>
		/// adds a new sqlParameter to arraylist object: parameterCollection.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="pvalue"></param>
		public void AddParameter(string name,object pvalue)
		{
			parameterCollection.Add(new SqlParameter(name,pvalue));
		}
		/// <summary>
		/// adds a new sqlParameter to arraylist object: parameterCollection.		/// </summary>
		/// <param name="name"></param>
		/// <param name="pValue"></param>
		/// <param name="dbType"></param>
		public void AddParameter(string name,object pValue,SqlDbType dbType)
		{
			SqlParameter objSqlParameter  = new SqlParameter(name,dbType);
			objSqlParameter.Value=pValue;
			parameterCollection.Add(objSqlParameter);
		}
		/// <summary>
		/// adds a new sqlParameter to arraylist object: parameterCollection.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="pvalue"></param>
		/// <param name="direction"></param>
		public object AddParameter(string name,object pvalue,ParameterDirection direction)
		{
			SqlParameter objSqlParameter  = new SqlParameter(name,pvalue);
			objSqlParameter.Direction=direction;
			parameterCollection.Add(objSqlParameter);
			return objSqlParameter;
		}
		/// <summary>
		/// adds a new sqlParameter to arraylist object: parameterCollection.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="pValue"></param>
		/// <param name="dbType"></param>
		/// <param name="direction"></param>
		public object AddParameter(string name,object pValue,SqlDbType dbType,ParameterDirection direction)
		{
			SqlParameter objSqlParameter  = new SqlParameter(name,dbType);
			objSqlParameter.Value=pValue;
			objSqlParameter.Direction=direction;
			parameterCollection.Add(objSqlParameter);
			return objSqlParameter;
		}

		/// <summary>
		/// adds a new OUTPUT sqlParameter to arraylist object: parameterCollection.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="dbType"></param>
		/// <param name="direction"></param>
		public object AddParameter(string name,SqlDbType dbType,ParameterDirection direction)
		{
			SqlParameter objSqlParameter  = new SqlParameter(name,dbType);
			objSqlParameter.Direction=direction;
			parameterCollection.Add(objSqlParameter);
			return objSqlParameter;
		}

		/// <summary>
		/// adds a new sqlParameter to arraylist object: parameterCollection.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="pValue"></param>
		/// <param name="dbType"></param>
		/// <param name="direction"></param>
		/// <param name="size"></param>
		public object AddParameter(string name,object pValue,SqlDbType dbType,ParameterDirection direction,int size)
		{
			SqlParameter objSqlParameter  = new SqlParameter(name,dbType);
			objSqlParameter.Value=pValue;
			objSqlParameter.Direction=direction;
			objSqlParameter.Size=size;
			parameterCollection.Add(objSqlParameter);
			return objSqlParameter;
		}
		public object GetParameterValue(string parameterName)
		{
			object obj = null;
			SqlParameter[] objSqlParameter = commandParameters;
			for(int i=0;i<objSqlParameter.Length;i++)
			{
				if(objSqlParameter[i].ParameterName.ToUpper().Equals(parameterName.ToUpper()) && (objSqlParameter[i].Direction == ParameterDirection.Output || objSqlParameter[i].Direction == ParameterDirection.InputOutput || objSqlParameter[i].Direction == ParameterDirection.ReturnValue))
				{
					obj = objSqlParameter[i].Value;
				}
			}
			return obj;
		}
		/// <summary>
		/// reinitializes sql parameter collection.
		/// </summary>
		public void ClearParameteres()
		{
			commandParameters=null;
			parameterCollection=new ArrayList();
		}
		#endregion

		#region "Private Utility Methods"
		/// <summary>
		/// Crerate a new object of connection if not already created.
		/// opens a new connection if not already opened.
		/// </summary>
		private void OpenConnection()
		{
			if (WrapperSqlConnection==null)
				objSqlConnection = new SqlConnection(ConnectionString);
			if(WrapperSqlConnection.State==ConnectionState.Closed)
				WrapperSqlConnection.Open();
		}
		/// <summary>
		/// Closes and nullifies a connection.
		/// </summary>
		/// <param name="disposeAfterClosing"></param>
		private void CloseSqlConnection(EDispose disposeAfterClosing)
		{
			if(WrapperSqlConnection!=null)
			{
				RollbackTransaction(CloseConnection.NO);
				if(WrapperSqlConnection.State!=ConnectionState.Closed)
					WrapperSqlConnection.Close();
				if(disposeAfterClosing==EDispose.YES)
					WrapperSqlConnection.Dispose();
				WrapperSqlConnection=null;
			}
		}
		/// <summary>
		/// Begins a transaction if not already began, and adds to the transaction cache.
		/// </summary>
		/// <param name="transactionName"></param>
		/// <returns>Transaction object form cache.</returns>
		private SqlTransaction GetTransactionObject(string transactionName)
		{
			//If no transaction name is specified a default transaction is started/extracted
			if (transactionName==null || transactionName.Length==0) transactionName=DEFAULT_TRANSACTION_NAME; 
			if(!transactionCache.ContainsKey(transactionName))
				transactionCache.Add(transactionName,WrapperSqlConnection.BeginTransaction(transactionName));
			return (SqlTransaction)transactionCache[transactionName];
		}
		/// <summary>
		/// Is interface to sqlHelper, calls appropriate methods of SQL helper depending upon, 
		/// if the parameters are defined for current query or not. 
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <returns></returns>
		private int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
		{
			if(CommandParameters==null || CommandParameters.Length==0)
				return SqlHelper.ExecuteNonQuery(connection,commandType,commandText);
			else
				return SqlHelper.ExecuteNonQuery(connection,commandType,commandText,CommandParameters);
		}
		/// <summary>
		/// this method is used if transaction is required.
		/// Is interface to sqlHelper, calls appropriate methods of SQL helper depending upon, 
		/// if the parameters are defined for current query or not. 
		/// </summary>
		/// <param name="transaction"></param>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <returns></returns>
		private int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
		{
			if(CommandParameters.Length==0)
				return SqlHelper.ExecuteNonQuery(transaction,commandType,commandText);
			else
				return SqlHelper.ExecuteNonQuery(transaction,commandType,commandText,CommandParameters);
		}
	
		#endregion
	}
}