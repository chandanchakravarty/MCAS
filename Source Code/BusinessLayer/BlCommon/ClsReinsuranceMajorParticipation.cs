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
using System.Web.UI.WebControls;

using Cms.DataLayer;
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsReinsuranceMajorParticipation.
	/// </summary>
	public class ClsReinsuranceMajorParticipation:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		# region D E C L A R A T I O N 

		private const string MNT_REINSURANCE_MAJORMINOR_PARTICIPATION = "MNT_REINSURANCE_MAJORMINOR_PARTICIPATION";
		
		private	bool boolTransactionLog;

		public ClsReinsuranceMajorParticipation()
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
		
		public int Add(ClsReinsuranceMajorParticipationInfo objReinsuranceMajorParticipationInfo)
		{
			string		strStoredProc	=	"Proc_MNT_REINSURANCE_INSERT_MAJOR_PARTICIPATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@REINSURANCE_COMPANY", objReinsuranceMajorParticipationInfo.REINSURANCE_COMPANY);
                objDataWrapper.AddParameter("@LAYER", objReinsuranceMajorParticipationInfo.LAYER);
                objDataWrapper.AddParameter("@NET_RETENTION", objReinsuranceMajorParticipationInfo.NET_RETENTION);
                objDataWrapper.AddParameter("@WHOLE_PERCENT", objReinsuranceMajorParticipationInfo.WHOLE_PERCENT);
                objDataWrapper.AddParameter("@MINOR_PARTICIPANTS", objReinsuranceMajorParticipationInfo.MINOR_PARTICIPANTS);
                objDataWrapper.AddParameter("@CONTRACT_ID", objReinsuranceMajorParticipationInfo.CONTRACT_ID);

                objDataWrapper.AddParameter("@CREATED_BY", objReinsuranceMajorParticipationInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objReinsuranceMajorParticipationInfo.CREATED_DATETIME);

                //SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PARTICIPATION_ID",objReinsuranceMajorParticipationInfo.PARTICIPATION_ID,SqlDbType.Int,ParameterDirection.Output);
                System.Data.SqlClient.SqlParameter objSqlParameter = (System.Data.SqlClient.SqlParameter)objDataWrapper.AddParameter("@PARTICIPATION_ID", SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objReinsuranceMajorParticipationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddReinsuranceMajorPart.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceMajorParticipationInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objReinsuranceMajorParticipationInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1781", "");//"New Major Participation Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    returnResult = int.Parse(objSqlParameter.Value.ToString());
                    
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                }

                objReinsuranceMajorParticipationInfo.PARTICIPATION_ID = returnResult;
                if (objSqlParameter.Value == System.DBNull.Value)
                {
                    return -2;
                }
                else
                    return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
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
		public int Update(ClsReinsuranceMajorParticipationInfo objOldReinsuranceMajorParticipationInfo,ClsReinsuranceMajorParticipationInfo objReinsuranceMajorParticipationInfo)
		{
			string	strStoredProc	=	"Proc_MNT_REINSURANCE_UPDATE_MAJOR_PARTICIPATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@PARTICIPATION_ID",objReinsuranceMajorParticipationInfo.PARTICIPATION_ID);
				objDataWrapper.AddParameter("@REINSURANCE_COMPANY",objReinsuranceMajorParticipationInfo.REINSURANCE_COMPANY);
				objDataWrapper.AddParameter("@LAYER",objReinsuranceMajorParticipationInfo.LAYER);
				objDataWrapper.AddParameter("@NET_RETENTION",objReinsuranceMajorParticipationInfo.NET_RETENTION);
				objDataWrapper.AddParameter("@WHOLE_PERCENT",objReinsuranceMajorParticipationInfo.WHOLE_PERCENT);
				objDataWrapper.AddParameter("@MINOR_PARTICIPANTS",objReinsuranceMajorParticipationInfo.MINOR_PARTICIPANTS);
                objDataWrapper.AddParameter("@CONTRACT_ID", objReinsuranceMajorParticipationInfo.CONTRACT_ID);
				objDataWrapper.AddParameter("@MODIFIED_BY",objReinsuranceMajorParticipationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReinsuranceMajorParticipationInfo.LAST_UPDATED_DATETIME);


                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@HAS_ERROR", objReinsuranceMajorParticipationInfo.PARTICIPATION_ID, SqlDbType.Int, ParameterDirection.Output);

				if(base.TransactionLogRequired) 
				{
					objReinsuranceMajorParticipationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddReinsuranceMajorPart.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReinsuranceMajorParticipationInfo, objReinsuranceMajorParticipationInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReinsuranceMajorParticipationInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1782", "");// "Major Participation Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                int HasError = 0;
                if(objSqlParameter.Value.ToString()!="")
                   HasError= int.Parse(objSqlParameter.Value.ToString());
                if (HasError == -4)
                    returnResult = HasError;
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

		public DataSet GetDataForPageControls(string PARTICIPATION_ID)
		{
			string strSql = "Proc_MNT_REINSURANCE_GETXML_MAJOR_PARTICIPATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@PARTICIPATION_ID",PARTICIPATION_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		#endregion
				
		#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

		public int GetDeactivateActivate(ClsReinsuranceMajorParticipationInfo objReinsuranceMajorParticipationInfo,string PARTICIPATION_ID,string Status_Check)
		{
			string strSql = "Proc_MNT_REINSURANCE_DEACTIVATE_ACTIVATE_MAJOR_PARTICIPATION";
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@PARTICIPATION_ID",PARTICIPATION_ID);
			objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

			if(base.TransactionLogRequired) 
			{
				objReinsuranceMajorParticipationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddReinsuranceMajorPart.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceMajorParticipationInfo);

				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objReinsuranceMajorParticipationInfo.MODIFIED_BY;
				if (Status_Check == "Y")
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1783","");//"Major Participation Has Been Activated";
				else
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1784", "");// "Major Participation Has Been Deactivated";
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

		#endregion

		# region GET COMPANY NAME
		public DataSet GetReinsurerBroker()
		{
			string strSql = "Proc_MNT_REINSURANCE_GETCOMPANYNAME_MAJOR_PARTICIPATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			//objDataWrapper.AddParameter("@PARTICIPATION_ID",PARTICIPATION_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;

		}

		# endregion GET COMPANY NAME

		# region DELETE THE RECORD
		public int Delete(string  PARTICIPATION_ID,ClsReinsuranceMajorParticipationInfo objReinsuranceMajorParticipationInfo)
		{
			string strSql="Proc_Delete_REIN_MAJOR_PARTICIPATION";
			int returnResult = 0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@PARTICIPATION_ID",PARTICIPATION_ID);
			objDataWrapper.AddParameter("@CONTRACT_ID",objReinsuranceMajorParticipationInfo.CONTRACT_ID);
			
			if(base.TransactionLogRequired) 
			{
				objReinsuranceMajorParticipationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddReinsuranceMajorPart.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objReinsuranceMajorParticipationInfo);

				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=	objReinsuranceMajorParticipationInfo.MODIFIED_BY;
                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1785", "");// "Major Participation Has Been deleted";
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


