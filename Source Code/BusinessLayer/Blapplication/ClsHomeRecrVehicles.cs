/******************************************************************************************
<Author					: -  Pradeep Iyer
<Start Date				: -	 May 23, 2005
<End Date				: -	
<Description			: -  BL class for HOME OWNERS RECREATION VEHICLES
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.HomeOwners;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsHomeRecrVehicles.
	/// </summary>
	public class ClsHomeRecrVehicles : Cms.BusinessLayer.BlApplication.clsapplication 
	{
		public static DateTime NewBusinessDate = Convert.ToDateTime("04/01/2004");
		public static int MinDeductibleAmt = 500;
		public ClsHomeRecrVehicles()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public int DeleteCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DeleteHOME_OWNER_REC_VEH_COV";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sUniqueID = (SqlParameter)objWrapper.AddParameter("@COVERAGE_UNIQUE_ID",SqlDbType.Int,ParameterDirection.Input);
			
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sUniqueID.Value = Convert.ToInt32(alNewCoverages[i]);
					objWrapper.ExecuteNonQuery(strStoredProc);				
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}

		public int SaveCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_InsertAPP_HOME_OWNER_REC_VEH_COV";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionId = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sRecVehID = (SqlParameter)objWrapper.AddParameter("@REC_VEH_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sCoverageID = (SqlParameter)objWrapper.AddParameter("@COVERAGE_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sCoverageCode = (SqlParameter)objWrapper.AddParameter("@COVERAGE_CODE",SqlDbType.NVarChar,ParameterDirection.Input);
			SqlParameter sLimit = (SqlParameter)objWrapper.AddParameter("@LIMIT",SqlDbType.Decimal,ParameterDirection.Input);
			SqlParameter sDeductible = (SqlParameter)objWrapper.AddParameter("@DEDUCTIBLE",SqlDbType.Decimal,ParameterDirection.Input);
			SqlParameter sWritten = (SqlParameter)objWrapper.AddParameter("@WRITTEN_PREMIUM",SqlDbType.Decimal,ParameterDirection.Input);
			SqlParameter sFullTerm = (SqlParameter)objWrapper.AddParameter("@FULL_TERM_PREMIUM",SqlDbType.Decimal,ParameterDirection.Input);
			SqlParameter sCreatedBy = (SqlParameter)objWrapper.AddParameter("@CREATED_BY",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sUnique = (SqlParameter)objWrapper.AddParameter("@COVERAGE_UNIQUE_ID",SqlDbType.Int,ParameterDirection.Input);
			
			
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					ClsRecrVehCoveragesInfo objNew = (ClsRecrVehCoveragesInfo)alNewCoverages[i];
				
					sCustomerID.Value = objNew.CUSTOMER_ID;
					sAppID.Value = objNew.APP_ID;
					sAppVersionId.Value = objNew.APP_VERSION_ID;
					sRecVehID.Value = objNew.REC_VEH_ID;
					sCoverageID.Value = objNew.COVERAGE_ID;
					sCoverageCode.Value = objNew.COVERAGE_CODE;
					sLimit.Value = DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT);
					sDeductible.Value = DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE);
					sWritten.Value = DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM);
					sFullTerm.Value = DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM);
					sCreatedBy.Value = objNew.CREATED_BY;
					sUnique.Value = objNew.COVERAGE_UNIQUE_ID; 

					if ( objNew.COVERAGE_UNIQUE_ID == 0 )
					{
					}
					else
					{
					}
				
					objWrapper.ExecuteNonQuery(strStoredProc);

				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);

				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}

		public DataSet GetCoverages(int customerID, int appID, 
			int appVersionID, int recrVehicleID, int currentPageIndex, int pageSize)
		{
			string	strStoredProc =	"Proc_GetHOME_OWNER_REC_VEH_COVERAGES";
			
			/*
			int recCount = currentPageIndex * pageSize;

			string countSQL = "SELECT COUNT(*)  " + 
								"FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES " + 
								"WHERE CUSTOMER_ID = " + customerID.ToString() + " AND " + 
									"APP_ID = " + appID.ToString() + " AND " + 
									"APP_VERSION_ID = " + appVersionID.ToString() + " AND " + 
									"REC_VEH_ID = " + recrVehicleID.ToString();

			string finalSQL = "SELECT * FROM (" + 
							"SELECT TOP " + pageSize.ToString() + " * FROM " + 
							"(SELECT TOP " + recCount.ToString() + " * FROM" +  
							"(" + 
							"SELECT *  " + 
							"FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES " + 
							"WHERE CUSTOMER_ID = " + customerID.ToString() + " AND " + 
							"APP_ID = " + appID.ToString() + " AND " + 
							"APP_VERSION_ID = " + appVersionID.ToString() + " AND " +
							"REC_VEH_ID = " + recrVehicleID.ToString() + 
							 ") AS t0 " + 
							" ORDER BY COVERAGE_UNIQUE_ID ASC ) AS t1 " + 
							" ORDER BY COVERAGE_UNIQUE_ID DESC ) AS t2 " + 
							" ORDER BY COVERAGE_UNIQUE_ID " ;
			*/			

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			objWrapper.AddParameter("@PAGE_SIZE",pageSize);
			objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;

			
		}

		/// <summary>
		/// Copies a record the specified number of times.
		/// </summary>
		/// <param name="alInfo"></param>
		/// <returns></returns>
		public int Copy(ArrayList alInfo)
		{
			string	strStoredProc =	"Proc_CopyHOME_OWNER_RECREATION_VEHICLES";
			int retVal=0;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionId = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sRecVehID = (SqlParameter)objWrapper.AddParameter("@REC_VEH_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sSerialNumber = (SqlParameter)objWrapper.AddParameter("@SERIAL",SqlDbType.NVarChar,ParameterDirection.Input);
			
			SqlParameter sRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				for(int i = 0; i < 	alInfo.Count; i++ )
				{
					ClsRecrVehiclesInfo objInfo = (ClsRecrVehiclesInfo)alInfo[i];
				
					sCustomerID.Value = objInfo.CUSTOMER_ID;
					sAppID.Value = objInfo.APP_ID;
					sAppVersionId.Value = objInfo.APP_VERSION_ID;
					sRecVehID.Value = objInfo.REC_VEH_ID;
					sSerialNumber.Value = objInfo.SERIAL;
				
					objWrapper.ExecuteNonQuery(strStoredProc);

					retVal = Convert.ToInt32(sRetVal.Value);					
					if(retVal==-1)
						break;					
				
				}
				//CompanyID # exceeds max value
				if ( retVal == -1 )
				{
					objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return retVal;
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			
			return 1;

		}
		


		public int CopyPolicy(ArrayList alInfo)
		{
			string	strStoredProc =	"Proc_CopyHOME_OWNER_RECREATION_VEHICLES_POLICY";
			int retVal=0;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@POL_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionId = (SqlParameter)objWrapper.AddParameter("@POL_VERSION_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sRecVehID = (SqlParameter)objWrapper.AddParameter("@REC_VEH_ID",SqlDbType.SmallInt,ParameterDirection.Input);
			SqlParameter sSerialNumber = (SqlParameter)objWrapper.AddParameter("@SERIAL",SqlDbType.NVarChar,ParameterDirection.Input);
			
			SqlParameter sRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				for(int i = 0; i < 	alInfo.Count; i++ )
				{
					ClsRecrVehiclesInfo objInfo = (ClsRecrVehiclesInfo)alInfo[i];
				
					sCustomerID.Value = objInfo.CUSTOMER_ID;
					sAppID.Value = objInfo.APP_ID;
					sAppVersionId.Value = objInfo.APP_VERSION_ID;
					sRecVehID.Value = objInfo.REC_VEH_ID;
					sSerialNumber.Value = objInfo.SERIAL;
				
					objWrapper.ExecuteNonQuery(strStoredProc);

					retVal = Convert.ToInt32(sRetVal.Value);					
					if(retVal==-1)
						break;					
				
				}
				//CompanyID # exceeds max value
				if ( retVal == -1 )
				{
					objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return retVal;
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			
			return 1;

		}
		

		/// <summary>
		/// Gets the next CompanyID number in the sequence
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
//		public static int GetNextCompanyIDNumber(int customerID, int appID, 
//											int appVersionID)
//		{
//			string	strStoredProc =	"Proc_GetNextRecrCompany_ID_Number";
//			
//			SqlParameter[] sqlParams = new SqlParameter[3];
//
//			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
//			sqlParams[1] = new SqlParameter("@APP_ID",appID);
//			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionID);
//			
//			int intNextCompanyID = 0;
//
//			try
//			{
//				intNextCompanyID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
//			}
//			catch(Exception ex)
//			{
//				throw(ex);
//			}
//			
//			return intNextCompanyID;
//
//		}
		public static DataTable GetNextCompanyIDNumber(int customerID, int appID,int appVersionID)
		{
			string	strStoredProc =	"Proc_GetNextRecrCompany_ID_Number";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);						
			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@APP_ID",appID);
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);						
				DataSet dsTemp = objWrapper.ExecuteDataSet(strStoredProc);
				if(dsTemp!=null && dsTemp.Tables.Count>0)
					return dsTemp.Tables[0];
				else
					return null;				
			}            
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		
		/// <summary>
		/// Activates/Deactivates the current record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="recrVehicleID"></param>
		/// <param name="active"></param>
		/// <returns></returns>
		public static int ActivateDeactivate(int customerID, int appID, 
									 int appVersionID, int recrVehicleID, string active)
		{
			string	strStoredProc =	"Proc_ActivateDeactivateHOME_OWNER_RECREATIONAL_VEHICLES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			objWrapper.AddParameter("@ACTIVE",active);

			SqlParameter sqlParamRetVal = (SqlParameter)objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				
			int retVal = 0;

			try
			{
				objWrapper.ExecuteNonQuery(strStoredProc);
			}
            
			catch(Exception ex)
			{
				throw(ex);
			}

			ClsHomeCoverages objCoverage;
			objCoverage=new ClsHomeCoverages();
			objCoverage.UpdateCoveragesByRuleApp(objWrapper,customerID,appID ,appVersionID ,RuleType.OtherAppDependent);
			retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal; 

		}
		

		
		/// <summary>
		/// Deletes the current record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="recrVehicleID"></param>
		/// <returns></returns>
		public int Delete(int customerID, int appID, int appVersionID, int recrVehicleID)
		{
			string	strStoredProc =	"Proc_DeleteHOME_OWNER_RECREATIONAL_VEHICLES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			SqlParameter sqlParamRetVal = (SqlParameter)objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				
			int retVal = 0;

			try
			{
				objWrapper.ExecuteNonQuery(strStoredProc);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			ClsHomeCoverages objCoverage;
			objCoverage=new ClsHomeCoverages();
			objCoverage.UpdateCoveragesByRuleApp(objWrapper,customerID,appID ,appVersionID,RuleType.OtherAppDependent);

			retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal; 

		}
		

		
		/// <summary>
		/// Gets a single recreational vehicle record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="recrVehicleID"></param>
		/// <returns></returns>
		/// 
		//Function added For Itrack Issue #6710.
		public static  DataSet GetSection2CoveragesE_CovergaesF(int customerID, int appID, 
			int appVersionID, string calledFrom)
		{
			string	strStoredProc =	"Proc_GetRVCoverages";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_POL_ID",appID);
			objWrapper.AddParameter("@APP_POL_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@CALLEDFROM",calledFrom);		

			DataSet objDataSet = objWrapper.ExecuteDataSet(strStoredProc);
			
			return objDataSet;
		
		}

		public DataSet GetRecrVehicleByID(int customerID, int appID, 
										  int appVersionID, int recrVehicleID)
		{
			string	strStoredProc =	"Proc_GetHomeRecrVehiclesByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		

		public DataSet GetPolicyUmbrellaRecrVehicleByID(int customerID, int policyID, int policyVersionID, int recrVehicleID)
		{
			string	strStoredProc =	"Proc_GetPolicyUmbrellaRecrVehiclesByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		
		/// <summary>
		/// Inserts a record in the database
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Add(ClsRecrVehiclesInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertHOME_OWNER_RECREATION_VEHICLES";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddRecrVehicle.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@COMPANY_ID_NUMBER",objInfo.COMPANY_ID_NUMBER);
			objWrapper.AddParameter("@YEAR",objInfo.YEAR);
			objWrapper.AddParameter("@MAKE",objInfo.MAKE);
			objWrapper.AddParameter("@MODEL",objInfo.MODEL);
			objWrapper.AddParameter("@SERIAL",objInfo.SERIAL);
			objWrapper.AddParameter("@STATE_REGISTERED",objInfo.STATE_REGISTERED);
			objWrapper.AddParameter("@VEHICLE_TYPE",objInfo.VEHICLE_TYPE);
			objWrapper.AddParameter("@MANUFACTURER_DESC",objInfo.MANUFACTURER_DESC);
			objWrapper.AddParameter("@HORSE_POWER",objInfo.HORSE_POWER);
			// Commented by mohit on 7/11/2005
			//objWrapper.AddParameter("@DISPLACEMENT",objInfo.DISPLACEMENT);
			objWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objWrapper.AddParameter("@USED_IN_RACE_SPEED",objInfo.USED_IN_RACE_SPEED);
			objWrapper.AddParameter("@PRIOR_LOSSES",objInfo.PRIOR_LOSSES);
			objWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",objInfo.IS_UNIT_REG_IN_OTHER_STATE);
			objWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",objInfo.RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",objInfo.DESC_RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@VEHICLE_MODIFIED",objInfo.VEHICLE_MODIFIED);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			//Added by Manoj Rathore on 29th Mar 2007
			objWrapper.AddParameter("@UNIT_RENTED",objInfo.UNIT_RENTED);
			objWrapper.AddParameter("@UNIT_OWNED_DEALERS",objInfo.UNIT_OWNED_DEALERS);
			objWrapper.AddParameter("@YOUTHFUL_OPERATOR_UNDER_25",objInfo.YOUTHFUL_OPERATOR_UNDER_25);
			//**************************************
			//Added By Ravindra(06-09-2006)
			if(objInfo.INSURING_VALUE != 0)
			{
				objWrapper.AddParameter("@INSURING_VALUE",objInfo.INSURING_VALUE );
			}
			else
			{
				objWrapper.AddParameter("@INSURING_VALUE",DBNull.Value );
			}
			if(objInfo.DEDUCTIBLE != 0)
			{
				objWrapper.AddParameter("@DEDUCTIBLE",objInfo.DEDUCTIBLE);
			}
			else
			{
				objWrapper.AddParameter("@DEDUCTIBLE",DBNull.Value );
			}
			//added For itrack Issue #6710.
			objWrapper.AddParameter("@LIABILITY",objInfo.LIABILITY);						
			objWrapper.AddParameter("@MEDICAL_PAYMENTS",objInfo.MEDICAL_PAYMENTS);
			objWrapper.AddParameter("@PHYSICAL_DAMAGE",objInfo.PHYSICAL_DAMAGE);
			// till here..


			
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New recreational vehicle is added.";
					

					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}

				//Added for Itrack Issue 6737 on 20 Nov 09
				int recVehicleId = Convert.ToInt32(sqlParamRetVal.Value);

				#region Assigned Recreational Vehicles Process

				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
				objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
				objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
				objWrapper.AddParameter("@REC_VEH_ID",recVehicleId);
				objWrapper.ExecuteNonQuery("Proc_SetAssgndDriverRecreationalType");
				objWrapper.ClearParameteres();

				#endregion

				ClsHomeCoverages objCoverage;
				objCoverage=new ClsHomeCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,RuleType.OtherAppDependent);
				
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}
		

		
		/// <summary>
		/// Updates the record.
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Update(Cms.Model.Application.HomeOwners.ClsRecrVehiclesInfo objOldInfo,ClsRecrVehiclesInfo objInfo)
		{
			string	strStoredProc =	"Proc_UpdateHOME_OWNER_RECREATION_VEHICLES";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddRecrVehicle.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@REC_VEH_ID",objInfo.REC_VEH_ID);
			if(objInfo.COMPANY_ID_NUMBER == 0)
			{
				objWrapper.AddParameter("@COMPANY_ID_NUMBER","");
			}
			else
			{
				objWrapper.AddParameter("@COMPANY_ID_NUMBER",objInfo.COMPANY_ID_NUMBER);
			}
			objWrapper.AddParameter("@YEAR",objInfo.YEAR);
			objWrapper.AddParameter("@MAKE",objInfo.MAKE);
			objWrapper.AddParameter("@MODEL",objInfo.MODEL);
			objWrapper.AddParameter("@SERIAL",objInfo.SERIAL);
			objWrapper.AddParameter("@STATE_REGISTERED",objInfo.STATE_REGISTERED);
			objWrapper.AddParameter("@VEHICLE_TYPE",objInfo.VEHICLE_TYPE);
			objWrapper.AddParameter("@MANUFACTURER_DESC",objInfo.MANUFACTURER_DESC);
			objWrapper.AddParameter("@HORSE_POWER",objInfo.HORSE_POWER);
			// Commented by mohit on 7/11/2005.
			//objWrapper.AddParameter("@DISPLACEMENT",objInfo.DISPLACEMENT);
			objWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objWrapper.AddParameter("@USED_IN_RACE_SPEED",objInfo.USED_IN_RACE_SPEED);
			objWrapper.AddParameter("@PRIOR_LOSSES",objInfo.PRIOR_LOSSES);
			objWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",objInfo.IS_UNIT_REG_IN_OTHER_STATE);
			objWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",objInfo.RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",objInfo.DESC_RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@VEHICLE_MODIFIED",objInfo.VEHICLE_MODIFIED);
			objWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY);
			//Added by Manoj Rathore on 29th Mar 2007
			objWrapper.AddParameter("@UNIT_RENTED",objInfo.UNIT_RENTED);
			objWrapper.AddParameter("@UNIT_OWNED_DEALERS",objInfo.UNIT_OWNED_DEALERS);
			objWrapper.AddParameter("@YOUTHFUL_OPERATOR_UNDER_25",objInfo.YOUTHFUL_OPERATOR_UNDER_25);
			//**************************************

			//Added By Ravindra(06-09-2006)
			if(objInfo.INSURING_VALUE != 0)
			{
				objWrapper.AddParameter("@INSURING_VALUE",objInfo.INSURING_VALUE );
			}
			else
			{
				objWrapper.AddParameter("@INSURING_VALUE",DBNull.Value );
			}
			if(objInfo.DEDUCTIBLE != 0)
			{
				objWrapper.AddParameter("@DEDUCTIBLE",objInfo.DEDUCTIBLE);
			}
			else
			{
				objWrapper.AddParameter("@DEDUCTIBLE",DBNull.Value );
			}
			// added For itrack issue 6710
			objWrapper.AddParameter("@LIABILITY",objInfo.LIABILITY);						
			objWrapper.AddParameter("@MEDICAL_PAYMENTS",objInfo.MEDICAL_PAYMENTS);
			objWrapper.AddParameter("@PHYSICAL_DAMAGE",objInfo.PHYSICAL_DAMAGE);
			// add till here.. 
			
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Recreational vehicle is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}


		
		/// <summary>
		/// Gets Recreational Vehicle Ids from app
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Recreational Vehicle Ids as a carat separated string </returns>
		public static string  GetAppRecreationVehicleIDs(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetRecreationalVehicleIDs_App";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strRecreationalVehicleIDs="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strRecreationalVehicleIDs=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strRecreationalVehicleIDs = strRecreationalVehicleIDs + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strRecreationalVehicleIDs;
		}

		#region Claim Recreational Vehicles
		public int AddClaimRecVeh(Cms.Model.Application.HomeOwners.ClsRecrVehiclesInfo objRecrVehiclesInfo)
		{			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{				
				objDataWrapper.AddParameter("@COMPANY_ID_NUMBER",objRecrVehiclesInfo.COMPANY_ID_NUMBER);
				objDataWrapper.AddParameter("@CLAIM_ID",objRecrVehiclesInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@SERIAL",objRecrVehiclesInfo.SERIAL);
				objDataWrapper.AddParameter("@YEAR",objRecrVehiclesInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objRecrVehiclesInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objRecrVehiclesInfo.MODEL);
				objDataWrapper.AddParameter("@STATE_REGISTERED",objRecrVehiclesInfo.STATE_REGISTERED);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objRecrVehiclesInfo.VEHICLE_TYPE);
				objDataWrapper.AddParameter("@HORSE_POWER",objRecrVehiclesInfo.HORSE_POWER);
				objDataWrapper.AddParameter("@REMARKS",objRecrVehiclesInfo.REMARKS);				
				objDataWrapper.AddParameter("@CREATED_BY",objRecrVehiclesInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objRecrVehiclesInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@POL_REC_VEH_ID",objRecrVehiclesInfo.POL_REC_VEH_ID);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REC_VEH_ID",objRecrVehiclesInfo.REC_VEH_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objRecrVehiclesInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddRecrVehicle.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objRecrVehiclesInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objRecrVehiclesInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Recreational Info is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_InsertCLM_RECREATIONAL_VEHICLES",objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertCLM_RECREATIONAL_VEHICLES");
				

				int intREC_VEH_ID = 0;
				if (returnResult>0)
				{
					intREC_VEH_ID = int.Parse(objSqlParameter.Value.ToString());					
					objRecrVehiclesInfo.REC_VEH_ID = intREC_VEH_ID;
				}

				objDataWrapper.ClearParameteres();
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

		public int UpdateClaimRecVeh(Cms.Model.Application.HomeOwners.ClsRecrVehiclesInfo objOldRecrVehiclesInfo,Cms.Model.Application.HomeOwners.ClsRecrVehiclesInfo objRecrVehiclesInfo)
		{
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@COMPANY_ID_NUMBER",objRecrVehiclesInfo.COMPANY_ID_NUMBER);
				objDataWrapper.AddParameter("@CLAIM_ID",objRecrVehiclesInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@SERIAL",objRecrVehiclesInfo.SERIAL);
				objDataWrapper.AddParameter("@YEAR",objRecrVehiclesInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objRecrVehiclesInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objRecrVehiclesInfo.MODEL);
				objDataWrapper.AddParameter("@STATE_REGISTERED",objRecrVehiclesInfo.STATE_REGISTERED);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objRecrVehiclesInfo.VEHICLE_TYPE);
				objDataWrapper.AddParameter("@HORSE_POWER",objRecrVehiclesInfo.HORSE_POWER);
				objDataWrapper.AddParameter("@REMARKS",objRecrVehiclesInfo.REMARKS);				
				objDataWrapper.AddParameter("@MODIFIED_BY",objRecrVehiclesInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objRecrVehiclesInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@REC_VEH_ID",objRecrVehiclesInfo.REC_VEH_ID);
				if(TransactionLogRequired) 
				{
					objRecrVehiclesInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddRecrVehicle.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldRecrVehiclesInfo,objRecrVehiclesInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objRecrVehiclesInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Insured Vehicle is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdateCLM_RECREATIONAL_VEHICLES",objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_UpdateCLM_RECREATIONAL_VEHICLES");

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

		public DataTable GetClaimRecrVehicleByID(int ClaimID,int recrVehicleID)
		{
			string	strStoredProc =	"Proc_Get_ClaimRecrVehiclesByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",ClaimID);			
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}		
		public DataTable GetClaimPolRecrVehicleByID(int ClaimID,int recrVehicleID)
		{
			string	strStoredProc =	"Proc_GetPolicyRecVehList";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CLAIM_ID",ClaimID);			
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}		

		public DataTable GetPolRecVehList(int ClaimID)
		{
			string	strStoredProc =	"Proc_GetPolicyRecVehList";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);						
			try
			{
				objWrapper.AddParameter("@CLAIM_ID",ClaimID);				
				DataSet dsTemp = objWrapper.ExecuteDataSet(strStoredProc);
				if(dsTemp!=null && dsTemp.Tables.Count>0)
					return dsTemp.Tables[0];
				else
					return null;				
			}            
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		public int GetClaimNextCompanyIDNumber(int ClaimID)
		{
			string	strStoredProc =	"Proc_GetClaimRecVehNextCompanyID";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);						
			try
			{
				objWrapper.AddParameter("@CLAIM_ID",ClaimID);							
				SqlParameter sRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
				objWrapper.ExecuteNonQuery(strStoredProc);
				if(sRetVal!=null && sRetVal.Value.ToString()!="" && sRetVal.Value.ToString()!="0")
					return int.Parse(sRetVal.Value.ToString());
				else
					return -1;
			}            
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		#endregion



		#region "Policy Recreational Vehicles"

		/// <summary>
		/// Inserts a record in POL_HOME_OWNER_RECREATION_VEHICLES
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int AddPolicyRecrVeh(ClsPolRecrVehiclesInfo objInfo)
		{
			string	strStoredProc =	"Proc_Insert_POL_RECREATION_VEHICLES";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/AddRecrVehicle.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
			objWrapper.AddParameter("@COMPANY_ID_NUMBER",objInfo.COMPANY_ID_NUMBER);
			objWrapper.AddParameter("@YEAR",objInfo.YEAR);
			objWrapper.AddParameter("@MAKE",objInfo.MAKE);
			objWrapper.AddParameter("@MODEL",objInfo.MODEL);
			objWrapper.AddParameter("@SERIAL",objInfo.SERIAL);
			objWrapper.AddParameter("@STATE_REGISTERED",objInfo.STATE_REGISTERED);
			objWrapper.AddParameter("@VEHICLE_TYPE",objInfo.VEHICLE_TYPE);
			objWrapper.AddParameter("@MANUFACTURER_DESC",objInfo.MANUFACTURER_DESC);
			objWrapper.AddParameter("@HORSE_POWER",objInfo.HORSE_POWER);
			// Commented by mohit on 7/11/2005
			//objWrapper.AddParameter("@DISPLACEMENT",objInfo.DISPLACEMENT);
			objWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objWrapper.AddParameter("@USED_IN_RACE_SPEED",objInfo.USED_IN_RACE_SPEED);
			objWrapper.AddParameter("@PRIOR_LOSSES",objInfo.PRIOR_LOSSES);
			objWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",objInfo.IS_UNIT_REG_IN_OTHER_STATE);
			objWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",objInfo.RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",objInfo.DESC_RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@VEHICLE_MODIFIED",objInfo.VEHICLE_MODIFIED);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			//Added by Manoj Rathore on 29th Mar 2007
			objWrapper.AddParameter("@UNIT_RENTED",objInfo.UNIT_RENTED);
			objWrapper.AddParameter("@UNIT_OWNED_DEALERS",objInfo.UNIT_OWNED_DEALERS);
			objWrapper.AddParameter("@YOUTHFUL_OPERATOR_UNDER_25",objInfo.YOUTHFUL_OPERATOR_UNDER_25);
			//**************************************

			//Added By Shafi(06-09-2006)
			objWrapper.AddParameter("@INSURING_VALUE",DefaultValues.GetDoubleNullFromNegative(objInfo.INSURING_VALUE ));
			if(objInfo.DEDUCTIBLE != 0)
			{
				objWrapper.AddParameter("@DEDUCTIBLE",objInfo.DEDUCTIBLE);
			}
			else
			{
				objWrapper.AddParameter("@DEDUCTIBLE",DBNull.Value );
			}	
	        //Added For Itrack Issue #6710
			objWrapper.AddParameter("@LIABILITY",objInfo.LIABILITY);						
			objWrapper.AddParameter("@MEDICAL_PAYMENTS",objInfo.MEDICAL_PAYMENTS);
			objWrapper.AddParameter("@PHYSICAL_DAMAGE",objInfo.PHYSICAL_DAMAGE);
			//Add till here..
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					//Changes has been done for itrack issue #6710
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID  = objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New recreational vehicle is added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				//Added for Itrack Issue 6737 on 20 Nov 09
				int recVehicleId = Convert.ToInt32(sqlParamRetVal.Value);

				#region Assigned Recreational Vehicles Process

				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
				objWrapper.AddParameter("@POL_ID",objInfo.POLICY_ID);
				objWrapper.AddParameter("@POL_VERSION_ID",objInfo.POLICY_VERSION_ID);
				objWrapper.AddParameter("@REC_VEH_ID",recVehicleId);
				objWrapper.ExecuteNonQuery("Proc_SetAssgndDriverRecreationalType_Pol");
				objWrapper.ClearParameteres();

				#endregion

				ClsHomeCoverages objCoverage;
				objCoverage=new ClsHomeCoverages();
				objCoverage.UpdateCoveragesByRulePolicy(objWrapper,objInfo.CUSTOMER_ID,objInfo.POLICY_ID ,objInfo.POLICY_VERSION_ID,RuleType.OtherAppDependent);
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}
		

			
		/// <summary>
		/// Updates the record in POL_HOME_OWNER_RECREATION_VEHICLES.
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int UpdateRecrVeh(ClsPolRecrVehiclesInfo objOldInfo,ClsPolRecrVehiclesInfo objInfo)
		{
			string	strStoredProc =	"Proc_Update_POL_HOME_OWNER_RECREATION_VEHICLES";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/AddRecrVehicle.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
			objWrapper.AddParameter("@REC_VEH_ID",objInfo.REC_VEH_ID);
			if(objInfo.COMPANY_ID_NUMBER == 0)
			{
				objWrapper.AddParameter("@COMPANY_ID_NUMBER","");
			}
			else
			{
				objWrapper.AddParameter("@COMPANY_ID_NUMBER",objInfo.COMPANY_ID_NUMBER);
			}
			objWrapper.AddParameter("@YEAR",objInfo.YEAR);
			objWrapper.AddParameter("@MAKE",objInfo.MAKE);
			objWrapper.AddParameter("@MODEL",objInfo.MODEL);
			objWrapper.AddParameter("@SERIAL",objInfo.SERIAL);
			objWrapper.AddParameter("@STATE_REGISTERED",objInfo.STATE_REGISTERED);
			objWrapper.AddParameter("@VEHICLE_TYPE",objInfo.VEHICLE_TYPE);
			objWrapper.AddParameter("@MANUFACTURER_DESC",objInfo.MANUFACTURER_DESC);
			objWrapper.AddParameter("@HORSE_POWER",objInfo.HORSE_POWER);
			// Commented by mohit on 7/11/2005.
			//objWrapper.AddParameter("@DISPLACEMENT",objInfo.DISPLACEMENT);
			objWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objWrapper.AddParameter("@USED_IN_RACE_SPEED",objInfo.USED_IN_RACE_SPEED);
			objWrapper.AddParameter("@PRIOR_LOSSES",objInfo.PRIOR_LOSSES);
			objWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",objInfo.IS_UNIT_REG_IN_OTHER_STATE);
			objWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",objInfo.RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",objInfo.DESC_RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@VEHICLE_MODIFIED",objInfo.VEHICLE_MODIFIED);
			objWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY);
			//Added by Manoj Rathore on 29th Mar 2007
			objWrapper.AddParameter("@UNIT_RENTED",objInfo.UNIT_RENTED);
			objWrapper.AddParameter("@UNIT_OWNED_DEALERS",objInfo.UNIT_OWNED_DEALERS);
			objWrapper.AddParameter("@YOUTHFUL_OPERATOR_UNDER_25",objInfo.YOUTHFUL_OPERATOR_UNDER_25);
			//**************************************

			//Added By Shafi(06-09-2006)
			objWrapper.AddParameter("@INSURING_VALUE",DefaultValues.GetDoubleNullFromNegative(objInfo.INSURING_VALUE ));
			if(objInfo.DEDUCTIBLE != 0)
			{
				objWrapper.AddParameter("@DEDUCTIBLE",objInfo.DEDUCTIBLE);
			}
			else
			{
				objWrapper.AddParameter("@DEDUCTIBLE",DBNull.Value );
			}
			//added For itrack Issue #6710.
			objWrapper.AddParameter("@LIABILITY",objInfo.LIABILITY);						
			objWrapper.AddParameter("@MEDICAL_PAYMENTS",objInfo.MEDICAL_PAYMENTS);
			objWrapper.AddParameter("@PHYSICAL_DAMAGE",objInfo.PHYSICAL_DAMAGE);
			//add till here..
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RET_VAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			try
			{
				if ( strTranXML.Trim() == "" )
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//changes has been done for itrack issue 6710
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID  = objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Recreational vehicle is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			int intRetVal = Convert.ToInt32(sqlParamRetVal.Value);

			return intRetVal;
		}

		

		/// <summary>
		/// Deletes the current record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="recrVehicleID"></param>
		/// <returns></returns>
		public int DeletePolRecrVeh(int customerID, int policyID, 
			int policyVersionID, int recrVehicleID)
		{
			string	strStoredProc =	"Proc_Delete_POL_HOME_OWNER_RECREATIONAL_VEHICLES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			SqlParameter sqlParamRetVal = (SqlParameter)objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				
			int retVal = 0;

			try
			{
				objWrapper.ExecuteNonQuery(strStoredProc);
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			ClsHomeCoverages objCoverage;
			objCoverage=new ClsHomeCoverages();
			objCoverage.UpdateCoveragesByRulePolicy(objWrapper,customerID,policyID ,policyVersionID,RuleType.OtherAppDependent);
			retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal; 

		}
		

		/// <summary>
		/// Gets a single recreational vehicle record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="recrVehicleID"></param>
		/// <returns></returns>
		public DataSet GetPolRecrVehicleByID(int customerID, int policyID, 
			int policyVersionID, int recrVehicleID)
		{
			string	strStoredProc =	"Proc_Get_PolRecrVehiclesByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		
		/// <summary>
		/// Gets the next CompanyID number in the sequence
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
//		public static int GetNextPolCompanyIDNumber(int customerID, int policyID, 
//			int policyVersionID)
//		{
//			string	strStoredProc =	"Proc_GetNext_POL_RecrCompany_ID_Number";
//			
//			SqlParameter[] sqlParams = new SqlParameter[3];
//
//			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
//			sqlParams[1] = new SqlParameter("@POLICY_ID",policyID);
//			sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID",policyVersionID);
//			
//			int intNextCompanyID = 0;
//
//			try
//			{
//				intNextCompanyID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
//			}
//			catch(Exception ex)
//			{
//				throw(ex);
//			}
//			
//			return intNextCompanyID;
//
//		}

		public static DataTable GetNextPolCompanyIDNumber(int customerID, int policyID,int policyVersionID)
		{
			string	strStoredProc =	"Proc_GetNext_POL_RecrCompany_ID_Number";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);						
			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@POLICY_ID",policyID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);						
				DataSet dsTemp = objWrapper.ExecuteDataSet(strStoredProc);
				if(dsTemp!=null && dsTemp.Tables.Count>0)
					return dsTemp.Tables[0];
				else
					return null;				
			}            
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		
		

		/// <summary>
		/// Activates/Deactivates the current record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="recrVehicleID"></param>
		/// <param name="active"></param>
		/// <returns></returns>
		public static int ActivateDeactivatePolRecrVeh(int customerID, int policyID, 
			int policyVersionID, int recrVehicleID, string active)
		{
			string	strStoredProc =	"Proc_ActivateDeactivate_POL_HOME_OWNER_RECREATIONAL_VEHICLES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			objWrapper.AddParameter("@ACTIVE",active);

			SqlParameter sqlParamRetVal = (SqlParameter)objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				
			int retVal = 0;

			try
			{
				objWrapper.ExecuteNonQuery(strStoredProc);
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			ClsHomeCoverages objCoverage;
			objCoverage=new ClsHomeCoverages();
			objCoverage.UpdateCoveragesByRulePolicy(objWrapper,customerID,policyID ,policyVersionID,RuleType.OtherAppDependent);
			
			retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal; 

		}
		


		/// <summary>
		/// Gets Recreational Vehicle Ids from policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Recreational Vehicle Ids as a carat separated string </returns>
		public static string  GetPolRecreationVehicleIDs(int customerID, int policyID, int policyVersionID)
		{
			string	strStoredProc =	"Proc_GetRecreationalVehicleIDs_Pol";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",policyID);
			objWrapper.AddParameter("@POL_VERSION_ID",policyVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strRecreationalVehicleIDs="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strRecreationalVehicleIDs=ds.Tables[0].Rows[i][0].ToString();
				}
				else
				{
					strRecreationalVehicleIDs = strRecreationalVehicleIDs + '^'  + ds.Tables[0].Rows[i][0].ToString();
				}

				
			}
			return strRecreationalVehicleIDs;
		}

		#endregion

	}

}
