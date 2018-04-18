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
	

	# region  REINSURANCE TIV GROUP BUSINESS LAYER
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
		/// Summary description for ClsConstructionTranslation.
		/// </summary>
		public class ClsConstructionTranslation:Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
		{
			# region D E C L A R A T I O N 

			private const string MNT_REIN_TIV_GROUP="MNT_REIN_TIV_GROUP";
			private	bool boolTransactionLog;

			public ClsConstructionTranslation()
			{
				boolTransactionLog	= base.TransactionLogRequired;
				base.strActivateDeactivateProcedure	="Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION";
			}

			# endregion 

			#region A D D  (I N S E R T)
			/// <summary>
			/// Saves the information passed in model object to database.
			/// </summary>
			/// <param name="objReinsuranceInfo">Model class object.</param>
			/// <returns>No of records effected.</returns>
			
			public int Add(ClsConstructionTranslationInfo objConstructionTranslationInfo)
			{
				string		strStoredProc	=	"Proc_MNT_REIN_INSERT_CONSTRUCTIONTRANSLATION";
				DateTime	RecordDate		=	DateTime.Now;
				DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				try
				{
					objDataWrapper.AddParameter("@REIN_EXTERIOR_CONSTRUCTION",objConstructionTranslationInfo.REIN_EXTERIOR_CONSTRUCTION);
					objDataWrapper.AddParameter("@REIN_DESCRIPTION",objConstructionTranslationInfo.REIN_DESCRIPTION);
					objDataWrapper.AddParameter("@REIN_REPORT_CODE",objConstructionTranslationInfo.REIN_REPORT_CODE);
					objDataWrapper.AddParameter("@REIN_NISS",objConstructionTranslationInfo.REIN_NISS);
					objDataWrapper.AddParameter("@CREATED_BY",objConstructionTranslationInfo.CREATED_BY);
					objDataWrapper.AddParameter("@CREATED_DATETIME",objConstructionTranslationInfo.CREATED_DATETIME);				
				
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REIN_CONSTRUCTION_CODE_ID",objConstructionTranslationInfo.REIN_CONSTRUCTION_CODE_ID,SqlDbType.Int,ParameterDirection.Output);

					int returnResult = 0;
					if(TransactionLogRequired)
					{
						objConstructionTranslationInfo.TransactLabel = ClsCommon.MapTransactionLabel("cmsweb/maintenance/Reinsurance/MasterSetup/ConstructionTranslation.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objConstructionTranslationInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objConstructionTranslationInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"New Construction Translation Has Been Added";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
					int  intREIN_CONSTRUCTION_CODE_ID= int.Parse(objSqlParameter.Value.ToString());
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					if (intREIN_CONSTRUCTION_CODE_ID == -1)
					{
						return -1;
					}
					else
					{
						objConstructionTranslationInfo.REIN_CONSTRUCTION_CODE_ID= intREIN_CONSTRUCTION_CODE_ID;
						return intREIN_CONSTRUCTION_CODE_ID;
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
			public int Update(ClsConstructionTranslationInfo objOldConstructionTranslationInfo,ClsConstructionTranslationInfo objConstructionTranslationInfo)
			{
				string	strStoredProc	=	"Proc_MNT_REIN_UPDATE_CONSTRUCTIONTRANSLATION";
				string strTranXML;
				int returnResult = 0;
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try 
				{
					objDataWrapper.AddParameter("@REIN_CONSTRUCTION_CODE_ID",objConstructionTranslationInfo.REIN_CONSTRUCTION_CODE_ID);
					objDataWrapper.AddParameter("@REIN_EXTERIOR_CONSTRUCTION",objConstructionTranslationInfo.REIN_EXTERIOR_CONSTRUCTION);
					objDataWrapper.AddParameter("@REIN_DESCRIPTION",objConstructionTranslationInfo.REIN_DESCRIPTION);
					objDataWrapper.AddParameter("@REIN_REPORT_CODE",objConstructionTranslationInfo.REIN_REPORT_CODE);
					objDataWrapper.AddParameter("@REIN_NISS",objConstructionTranslationInfo.REIN_NISS);
					objDataWrapper.AddParameter("@MODIFIED_BY",objConstructionTranslationInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objConstructionTranslationInfo.LAST_UPDATED_DATETIME);				
				

					if(base.TransactionLogRequired) 
					{
						objConstructionTranslationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/Reinsurance/MasterSetup/ConstructionTranslation.aspx.resx");
						objBuilder.GetUpdateSQL(objOldConstructionTranslationInfo, objConstructionTranslationInfo,out strTranXML);

						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objConstructionTranslationInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Construction Translation Has Been Updated";
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

			public DataSet GetDataForPageControls(string REIN_CONSTRUCTION_CODE_ID)
			{
				string strSql = "Proc_MNT_REIN_GETXML_CONSTRUCTIONTRANSLATION";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@REIN_CONSTRUCTION_CODE_ID",REIN_CONSTRUCTION_CODE_ID);
				DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				return objDataSet;
			}

			#endregion
					
			#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

			public int GetDeactivateActivate(string REIN_CONSTRUCTION_CODE_ID,string Status_Check)
			{
				
				string strSql = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@REIN_CONSTRUCTION_CODE_ID",REIN_CONSTRUCTION_CODE_ID);
				objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

				//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				int returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
				return returnResult;
			}

			#endregion

			#region DELETE RECORDS FROM TABLE

			public int Delete(string REIN_CONSTRUCTION_CODE_ID)
			{
				string strSql = "Proc_MNT_REIN_DELETE_CONSTRUCTIONTRANSLATION";
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@REIN_CONSTRUCTION_CODE_ID",REIN_CONSTRUCTION_CODE_ID);
				//objDataWrapper.AddParameter("@STATUS_CHECK",Status_Check,System.Data.SqlDbType.NVarChar);

				//DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				int returnResult	= objDataWrapper.ExecuteNonQuery(strSql);
				return returnResult;
			}

			#endregion
			
		}
	}

# endregion



