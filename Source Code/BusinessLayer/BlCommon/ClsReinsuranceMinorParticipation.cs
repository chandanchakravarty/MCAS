/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: May 7, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for Special Acceptance Amount 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsReinsuranceMinorParticipation.
	/// </summary>
	public class ClsReinsuranceMinorParticipation : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private const string MNT_REIN_MINOR_PARTICIPATION = "MNT_REIN_MINOR_PARTICIPATION";
		
		private	bool boolTransactionLog;

		public ClsReinsuranceMinorParticipation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
		}

		# endregion 

		#region A D D  (I N S E R T)
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReinsuranceInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		
		public int Add(ClsReinsuranceMinorParticipationInfo objReinsuranceMinorParticipationInfo)
		{
			string		strStoredProc	=	"Proc_MNT_REIN_INSERT_MINOR_PARTICIPATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@MINOR_LAYER",objReinsuranceMinorParticipationInfo.MINOR_LAYER);
				objDataWrapper.AddParameter("@MAJOR_PARTICIPANTS",objReinsuranceMinorParticipationInfo.MAJOR_PARTICIPANTS);
				objDataWrapper.AddParameter("@MINOR_WHOLE_PERCENT",objReinsuranceMinorParticipationInfo.MINOR_WHOLE_PERCENT);
				objDataWrapper.AddParameter("@MINOR_PARTICIPANTS",objReinsuranceMinorParticipationInfo.MINOR_PARTICIPANTS);
				objDataWrapper.AddParameter("@CONTRACT_ID",objReinsuranceMinorParticipationInfo.CONTRACT_ID);

				objDataWrapper.AddParameter("@CREATED_BY",objReinsuranceMinorParticipationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReinsuranceMinorParticipationInfo.CREATED_DATETIME);				
			
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@MINOR_PARTICIPATION_ID",objReinsuranceMinorParticipationInfo.MINOR_PARTICIPATION_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReinsuranceMinorParticipationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddReinsuranceMinorPart.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceMinorParticipationInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceMinorParticipationInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Minor Participation Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int  intMINOR_PARTICIPATION_ID= int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (intMINOR_PARTICIPATION_ID == -1)
				{
					return -1;
				}
				else
				{
					objReinsuranceMinorParticipationInfo.MINOR_PARTICIPATION_ID= intMINOR_PARTICIPATION_ID;
					return intMINOR_PARTICIPATION_ID;
				}
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
			
		# endregion 

		#region U P D A T E   
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldReinsuranceInfo">Model object having old information</param>
		/// <param name="objReinsuranceInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsReinsuranceMinorParticipationInfo objOldReinsuranceMinorParticipationInfo,ClsReinsuranceMinorParticipationInfo objReinsuranceMinorParticipationInfo)
		{
			string	strStoredProc	=	"Proc_MNT_REIN_UPDATE_MINOR_PARTICIPATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@MINOR_PARTICIPATION_ID",objReinsuranceMinorParticipationInfo.MINOR_PARTICIPATION_ID);
				objDataWrapper.AddParameter("@MINOR_LAYER",objReinsuranceMinorParticipationInfo.MINOR_LAYER);
				objDataWrapper.AddParameter("@MAJOR_PARTICIPANTS",objReinsuranceMinorParticipationInfo.MAJOR_PARTICIPANTS);
				objDataWrapper.AddParameter("@MINOR_WHOLE_PERCENT",objReinsuranceMinorParticipationInfo.MINOR_WHOLE_PERCENT);
				objDataWrapper.AddParameter("@MINOR_PARTICIPANTS",objReinsuranceMinorParticipationInfo.MINOR_PARTICIPANTS);

				
				objDataWrapper.AddParameter("@MODIFIED_BY",objReinsuranceMinorParticipationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReinsuranceMinorParticipationInfo.LAST_UPDATED_DATETIME);				
			
				
				if(base.TransactionLogRequired) 
				{
					objReinsuranceMinorParticipationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddReinsuranceMinorPart.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceMinorParticipationInfo, objReinsuranceMinorParticipationInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceMinorParticipationInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Minor Participation Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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

		# endregion 

		#region G E T  D A T A   F O R   E D I T   M O D E

		public DataSet GetDataForPageControls(string MINOR_PARTICIPATION_ID)
		{
			string strSql = "Proc_MNT_REIN_GETXML_MINOR_PARTICIPATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@MINOR_PARTICIPATION_ID",MINOR_PARTICIPATION_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
        // Modified by santosh kumar gautam on 04 feb 2011 Itrack:462
        public static string GetTotalPercentage(int Contract_id, string strCalledfor, int intLayer)
        {
            return GetTotalPercentage(Contract_id, strCalledfor, intLayer,0);
        }
        // Modified by santosh kumar gautam on 04 feb 2011 Itrack:462
        public static string GetTotalPercentage(int Contract_id, string strCalledfor, int intLayer, int intPARTICIPATION_ID)
		{
            string TotalPercentage="";
			string strSql = "Proc_GetREIN_MAJOR_MINOR_TOTALPERCENTAGE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACT_ID",Contract_id);
			objDataWrapper.AddParameter("@CALLED_FROM",strCalledfor);
			objDataWrapper.AddParameter("@LAYER",intLayer);
            objDataWrapper.AddParameter("@PARTICIPATION_ID", intPARTICIPATION_ID);
            
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(objDataSet.Tables.Count>0)
            {
               foreach(DataRow dr in objDataSet.Tables[0].Rows)
               {
                  if(dr["OLDTOTALPERCENT"]!=null )
                      TotalPercentage=TotalPercentage+dr["OLDTOTALPERCENT"].ToString();
               }
                

            }
            return TotalPercentage;
		}

		#endregion
				
		#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

		public int GetDeactivateActivate(string MINOR_PARTICIPATION_ID,string Status_Check, ClsReinsuranceMinorParticipationInfo objReinsuranceMinorParticipationInfo)
		{
			string strSql = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_MINOR_PARTICIPATION";
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@MINOR_PARTICIPATION_ID",MINOR_PARTICIPATION_ID);
			objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

			//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			if(base.TransactionLogRequired) 
			{
				objReinsuranceMinorParticipationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddReinsuranceMinorPart.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceMinorParticipationInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objReinsuranceMinorParticipationInfo.MODIFIED_BY;
				if(Status_Check == "N")
					objTransactionInfo.TRANS_DESC		=	"Minor Participation Deactivated successfully";
				else
					objTransactionInfo.TRANS_DESC		=	"Minor Participation Activated successfully";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				returnResult = objDataWrapper.ExecuteNonQuery(strSql, objTransactionInfo);
			}
			else
			{
				returnResult = objDataWrapper.ExecuteNonQuery(strSql);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return returnResult;
		}

		#endregion

		# region GET COMPANY NAME
		/*
		public DataSet GetReinsurerBroker()
		{
			
			string strSql = "Proc_MNT_REINSURANCE_GETCOMPANYNAME_MAJOR_PARTICIPATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			//objDataWrapper.AddParameter("@PARTICIPATION_ID",PARTICIPATION_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
			
		}
		*/
		public static DataSet GetMajorMinorParticipents(int Contact_ID,string strMajorMinor)
		{
			
			string strSql = "proc_getREINSURANCE_MAJORMINOR_PARTICIPATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CONTRACT_ID",Contact_ID);
			objDataWrapper.AddParameter("@MAJOR_MINOR",strMajorMinor);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
			
		}
		
		# endregion GET COMPANY NAME

		# region DELETE THE RECORD
		public int Delete(string  MINOR_PARTICIPATION_ID,ClsReinsuranceMinorParticipationInfo objReinsuranceMinorParticipationInfo)
		{
			
			string strSql="Proc_Delete_REIN_MINOR_PARTICIPATION";
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@MINOR_PARTICIPATION_ID",MINOR_PARTICIPATION_ID);
			objDataWrapper.AddParameter("@CONTRACT_ID",objReinsuranceMinorParticipationInfo.CONTRACT_ID);
			
			if(base.TransactionLogRequired) 
			{
				objReinsuranceMinorParticipationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddReinsuranceMinorPart.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceMinorParticipationInfo);

				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objReinsuranceMinorParticipationInfo.MODIFIED_BY;
				objTransactionInfo.TRANS_DESC		=	"Minor Participation Has Been deleted";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				returnResult = objDataWrapper.ExecuteNonQuery(strSql, objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return returnResult;
		}

		# endregion


        

	}
}


