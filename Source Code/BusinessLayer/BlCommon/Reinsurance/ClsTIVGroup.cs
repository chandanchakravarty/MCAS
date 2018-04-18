/* ***************************************************************************************
   Author		: Harmanjeet Singh 
   Creation Date: April 27, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: Class file for ClsTIVGroup 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/


	# region REINSURANCE TIV GROUP BUSINESS LAYER
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Text;
	using System.Xml;
	using System.Configuration;
	using Cms.DataLayer;
	using System.Web.UI.WebControls;
	using Cms.Model.Maintenance.Reinsurance;

	namespace Cms.BusinessLayer.BlCommon.Reinsurance
	{
		/// <summary>
		/// Summary description for ClsTIVGroup.
		/// </summary>
		public class ClsTIVGroup:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
		{
			# region D E C L A R A T I O N 

			private const string MNT_REIN_TIV_GROUP="MNT_REIN_TIV_GROUP";
			private	bool boolTransactionLog;

			public ClsTIVGroup()
			{
				boolTransactionLog	= base.TransactionLogRequired;
				base.strActivateDeactivateProcedure	="Proc_MNT_REIN_DEACTIVATE_ACTIVATE_TIVGROUP";
			}

			# endregion 

			#region A D D  (I N S E R T)
			/// <summary>
			/// Saves the information passed in model object to database.
			/// </summary>
			/// <param name="objReinsuranceInfo">Model class object.</param>
			/// <returns>No of records effected.</returns>
			
			public int Add(ClsTIVGroupInfo objTIVGroupInfo)
			{
				string		strStoredProc	=	"Proc_MNT_REIN_INSERT_TIVGROUP";
				DateTime	RecordDate		=	DateTime.Now;
				DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				try
				{
					objDataWrapper.AddParameter("@REIN_TIV_CONTRACT_NUMBER",objTIVGroupInfo.REIN_TIV_CONTRACT_NUMBER);
					objDataWrapper.AddParameter("@REIN_TIV_EFFECTIVE_DATE",objTIVGroupInfo.REIN_TIV_EFFECTIVE_DATE);
					objDataWrapper.AddParameter("@REIN_TIV_FROM",objTIVGroupInfo.REIN_TIV_FROM);
					objDataWrapper.AddParameter("@REIN_TIV_TO",objTIVGroupInfo.REIN_TIV_TO);
					objDataWrapper.AddParameter("@REIN_TIV_GROUP_CODE",objTIVGroupInfo.REIN_TIV_GROUP_CODE);
					objDataWrapper.AddParameter("@CREATED_BY",objTIVGroupInfo.CREATED_BY);
					objDataWrapper.AddParameter("@CREATED_DATETIME",objTIVGroupInfo.CREATED_DATETIME);				
				
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REIN_TIV_GROUP_ID",objTIVGroupInfo.REIN_TIV_GROUP_ID,SqlDbType.Int,ParameterDirection.Output);

					int returnResult = 0;
					if(TransactionLogRequired)
					{
						objTIVGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/Reinsurance/MasterSetup/TIVGroup.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objTIVGroupInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objTIVGroupInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"New Reinsurance TIV Group Has Been Added";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					int  intREIN_TIV_GROUP_ID= int.Parse(objSqlParameter.Value.ToString());
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					if (intREIN_TIV_GROUP_ID == -1)
					{
						return -1;
					}
					else
					{
						objTIVGroupInfo.REIN_TIV_GROUP_ID= intREIN_TIV_GROUP_ID;
						return intREIN_TIV_GROUP_ID;
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
			public int Update(ClsTIVGroupInfo objOldTIVGroupInfo,ClsTIVGroupInfo objTIVGroupInfo)
			{
				string	strStoredProc	=	"Proc_MNT_REIN_UPDATE_TIVGROUP";
				string strTranXML;
				int returnResult = 0;
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try 
				{
					objDataWrapper.AddParameter("@REIN_TIV_GROUP_ID",objTIVGroupInfo.REIN_TIV_GROUP_ID);
					objDataWrapper.AddParameter("@REIN_TIV_CONTRACT_NUMBER",objTIVGroupInfo.REIN_TIV_CONTRACT_NUMBER);
					objDataWrapper.AddParameter("@REIN_TIV_EFFECTIVE_DATE",objTIVGroupInfo.REIN_TIV_EFFECTIVE_DATE);
					objDataWrapper.AddParameter("@REIN_TIV_FROM",objTIVGroupInfo.REIN_TIV_FROM);
					objDataWrapper.AddParameter("@REIN_TIV_TO",objTIVGroupInfo.REIN_TIV_TO);
					objDataWrapper.AddParameter("@REIN_TIV_GROUP_CODE",objTIVGroupInfo.REIN_TIV_GROUP_CODE);
					objDataWrapper.AddParameter("@MODIFIED_BY",objTIVGroupInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objTIVGroupInfo.LAST_UPDATED_DATETIME);				
				

					if(base.TransactionLogRequired) 
					{
						objTIVGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/Reinsurance/MasterSetup/TIVGroup.aspx.resx");
						objBuilder.GetUpdateSQL(objOldTIVGroupInfo, objTIVGroupInfo,out strTranXML);

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objTIVGroupInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Reinsurance TIV Group Has Been Updated";
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

			public DataSet GetDataForPageControls(string REIN_TIV_GROUP_ID)
			{
				string strSql = "Proc_MNT_REIN_GETXML_TIVGROUP";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@REIN_TIV_GROUP_ID",REIN_TIV_GROUP_ID);
				DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				return objDataSet;
			}

			#endregion
					
			#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

			public int GetDeactivateActivate(string REIN_TIV_GROUP_ID,string Status_Check)
			{
				string strSql = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_TIVGROUP";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@REIN_TIV_GROUP_ID",REIN_TIV_GROUP_ID);
				objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

				//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				int returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
				return returnResult;
			}

			#endregion
			
			#region DELETE RECORDS FROM TABLE

			public int Delete(string REIN_TIV_GROUP_ID)
			{
				string strSql = "Proc_MNT_REIN_DELETE_TIVGROUP";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@REIN_TIV_GROUP_ID",REIN_TIV_GROUP_ID);
				//objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

				//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				int returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
				return returnResult;
			}

			#endregion

			# region GET CONTRACT NUMBER FOR DROPDOWN CONTROL

			public DataSet GetContractNumber()
			{
				string strSql = "Proc_MNT_REIN_SELECT_CONTRACT";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				//objDataWrapper.AddParameter("@REIN_TIV_GROUP_ID",REIN_TIV_GROUP_ID);
				DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				return objDataSet;
			}



			# endregion
			
		}
	}

# endregion
