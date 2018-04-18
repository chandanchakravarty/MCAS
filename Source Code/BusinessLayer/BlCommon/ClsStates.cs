using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsStates.
	/// </summary>
	public class ClsStates : Cms.BusinessLayer.BlCommon.ClsCommon
	{
		public DataSet PoplateLob()
        {
			string getLobProc = "Proc_LobList";
			DataSet lobDs;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

			try
			{
                //Added by Charles on 25-May-2010 for Multilingual Support
                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
                lobDs = objDataWrapper.ExecuteDataSet(getLobProc);
                //Added till here
				//lobDs=Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr,getLobProc);
				return lobDs;
			}
			catch(Exception ex)
			{
				throw(ex);

			}

			
			//
			// TODO: Add constructor logic here
			//
		}
		public DataSet  PopLateAssignedState(int lobID)
		{
			string getAStateList="Proc_PoplateAssingnedState";
			DataSet assState;
			try
			{
				Object[] objParam=new object[1];
				objParam[0]=lobID;
				assState=Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr,getAStateList,objParam);
				return assState;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		public int Save(int intLobID,string arrStateId)
		{
			string	strStoredProc	 = "Proc_AssignStateToLob";
			string  strStoredProcDel = "Proc_AssignDeleteToLob";
			Cms.DataLayer.DataWrapper objDataWrapper = null;
			int intResult = 0;
			try
			{
			
				objDataWrapper = new Cms.DataLayer.DataWrapper(ConnStr,CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				objDataWrapper.AddParameter("@LobId",intLobID);
				objDataWrapper.CommandType = CommandType.StoredProcedure;
				objDataWrapper.ExecuteNonQuery(strStoredProcDel);
				
				//Adding the parameters, values will be passed inside the loop
				objDataWrapper.AddParameter("@StateId",arrStateId);
				
				
				
		
			
							
				//for(int i=1;i<arrStateId.Length;i++)
				//{
					objDataWrapper.CommandType = CommandType.StoredProcedure;
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				//}	
				objDataWrapper.CommitTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES );
				intResult = 1;
			}
			catch(Exception ex)
			{
				throw ex;			
			}	
			finally
			{
				if (objDataWrapper != null)
					objDataWrapper.Dispose();
			}
			return intResult;
		}
		public DataSet  PopLateUnassignedState(int lobID)
		{
			string getUnStateList="Proc_PopulateUnassignedState";
			DataSet unAssState;
			try
			{
				Object[] objParam=new object[1];
				objParam[0]=lobID;
				unAssState=Cms.DataLayer.DataWrapper.ExecuteDataset(ConnStr,getUnStateList,objParam);
				return unAssState;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
        public DataTable GetLOBCountryWiseState(int COUNTRY_ID, int LOB_ID)
        {
            string strSQL = "Proc_GetLOBCountryWiseState";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CountryID", COUNTRY_ID);
            objDataWrapper.AddParameter("@LobId", LOB_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSQL);
            if (objDataSet != null && objDataSet.Tables.Count > 0)
                return objDataSet.Tables[0];
            else
                return null;
        }

		#region Following methods are defined for AddStates page to add states to existing countries
		public string  GetOldData(int STATE_ID)
		{
			string strSql = "Proc_GetMNT_COUNTRY_STATE_LIST";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);			
			objDataWrapper.AddParameter("@STATE_ID",STATE_ID);			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0 && objDataSet.Tables[0]!=null && objDataSet.Tables[0].Rows.Count>0)
				return ClsCommon.GetXMLEncoded(objDataSet.Tables[0]);
			else
				return "";
		}

		public int AddState(ClsStateInfo objStateInfo)
		{
			string		strStoredProc	=	"Proc_InsertMNT_COUNTRY_STATE_LIST";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{
		
				objDataWrapper.AddParameter("@STATE_CODE",objStateInfo.STATE_CODE);
				objDataWrapper.AddParameter("@STATE_DESC",objStateInfo.STATE_DESC);
				objDataWrapper.AddParameter("@STATE_NAME",objStateInfo.STATE_NAME);
				objDataWrapper.AddParameter("@COUNTRY_ID",objStateInfo.COUNTRY_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objStateInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objStateInfo.CREATED_DATETIME);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@STATE_ID",objStateInfo.STATE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objStateInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/AddStates.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objStateInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objStateInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"New State is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					

				if (int.Parse(objSqlParameter.Value.ToString()) > 0)
				{
					objStateInfo.STATE_ID = int.Parse(objSqlParameter.Value.ToString());
				}			

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return returnResult;
					
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		public int UpdateState(ClsStateInfo objOldStateInfo,ClsStateInfo objStateInfo)
		{
			string		strStoredProc	=	"Proc_UpdateMNT_COUNTRY_STATE_LIST";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@STATE_CODE",objStateInfo.STATE_CODE);
				objDataWrapper.AddParameter("@STATE_DESC",objStateInfo.STATE_DESC);
				objDataWrapper.AddParameter("@STATE_NAME",objStateInfo.STATE_NAME);
				objDataWrapper.AddParameter("@COUNTRY_ID",objStateInfo.COUNTRY_ID);				
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objStateInfo.LAST_UPDATED_DATETIME);	
				objDataWrapper.AddParameter("@MODIFIED_BY",objStateInfo.MODIFIED_BY);				
				objDataWrapper.AddParameter("@STATE_ID",objStateInfo.STATE_ID);

				if(TransactionLogRequired) 
				{
					objStateInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/Maintenance/AddStates.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldStateInfo,objStateInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objStateInfo.MODIFIED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"State is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) 
				{
					objDataWrapper.Dispose();
				}
				if(objBuilder != null) 
				{
					objBuilder = null;
				}
			}
		}
		public DataSet GetStatesCountry(int COUNTRY_ID)
		{
			string strSql = "Proc_GetStateListForCountry";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);						
			objDataWrapper.AddParameter("@COUNTRY_ID",COUNTRY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}
		public static string GetStateList(string STATE_ID)
		{
			string strSql = "Proc_GetStateList";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);						
			objDataWrapper.AddParameter("@STATE",STATE_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0 && objDataSet.Tables[0].Rows.Count>0)
				return objDataSet.Tables[0].Rows[0]["STATE_NAME"].ToString();
			else
				return "";
		}
		public DataTable GetStatesForCountry(int COUNTRY_ID)
		{
			string strSql = "Proc_GetStateListForCountry";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);						
			objDataWrapper.AddParameter("@COUNTRY_ID",COUNTRY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		#endregion
	}
}
