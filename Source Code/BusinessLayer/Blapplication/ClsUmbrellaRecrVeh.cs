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
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using System.Xml;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUmrellaRecrVeh.
	/// </summary>
	public class ClsUmbrellaRecrVeh : clsapplication
	{
		public ClsUmbrellaRecrVeh()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public int UpdateRemarks(int customerID, int appID, int appVersionID, 
			int subInsuID,string remarks)
		{
			string	strStoredProc =	"Proc_UpdateUMBRELLA_REC_VEH_REMARKS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",subInsuID);
			objWrapper.AddParameter("@REMARKS",remarks);

			objWrapper.ExecuteNonQuery(strStoredProc);

			return 1;
		}
		public int CopyUmbPolicy(ArrayList alInfo)
		{
			string	strStoredProc =	"PROC_COPY_UMBRELLA_RECREATION_VEHICLES_POLICY";
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

		public string GetRemarks(int customerID, int appID, int appVersionID, int subInsuID)
		{
			string	strStoredProc =	"Proc_GetUMBRELLA_REC_VEH_REMARKS";

			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@APP_ID",appID);
			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			sqlParams[3] = new SqlParameter("@REC_VEH_ID",subInsuID);

			string remarks = "";

			remarks = (string)SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams);
			
			return remarks;
			
		}
		
		public string ReplaceXMLCharacters(string xml)
		{
			xml = xml.Replace("&","&amp;");
			xml = xml.Replace(">","&gt;");
			xml = xml.Replace("<","&lt;");

			return xml;
		}

		public int SaveCoverages(ArrayList alNewCoverages,string strOldXML)
		{
			
			string	strStoredProc =	"Proc_InsertUMBRELLA_REC_VEH_COV";

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
			
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

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
						//Insert
					}
					else
					{
						//Update	
						XmlNode node = xmlDoc.SelectSingleNode("NewDataSet/Table[COVERAGE_UNIQUE_ID=" + sUnique.Value.ToString() + "]");
						
						ClsRecrVehCoveragesInfo objOld = new ClsRecrVehCoveragesInfo();
						
						objOld.APP_ID = objNew.APP_ID;
						objOld.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
						objOld.REC_VEH_ID = objNew.CUSTOMER_ID;
						
						XmlNode element = null;

						element = node.SelectSingleNode("COVERAGE_ID");

						if ( element != null)
						{
							objOld.COVERAGE_ID = Convert.ToInt32(element.InnerXml);
						}
						
						element = node.SelectSingleNode("COVERAGE_CODE");
						
						if ( element != null )
						{
							objOld.COVERAGE_CODE = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
						}
						
						element = node.SelectSingleNode("LIMIT");
						
						if ( element != null )
						{
							string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
							if ( str != "" )
							{
								objOld.LIMIT = Convert.ToDouble(str);
							}
						}

						element = node.SelectSingleNode("DEDUCTIBLE");
						
						if ( element != null )
						{
							string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
							if ( str != "" )
							{
								objOld.DEDUCTIBLE = Convert.ToDouble(str);
							}
						}
						
						element = node.SelectSingleNode("WRITTEN_PREMIUM");
						if ( element != null )
						{
							string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
							if ( str != "" )
							{
								objOld.WRITTEN_PREMIUM = Convert.ToDouble(str);
							}
						}

						element = node.SelectSingleNode("FULL_TERM_PREMIUM");
						if ( element != null )
						{
							string str = ClsCommon.DecodeXMLCharacters(Convert.ToString(element.InnerXml));
							
							if ( str != "" )
							{
								objOld.FULL_TERM_PREMIUM = Convert.ToDouble(str);
							}
						}
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

		public int UmbrellaEndorsementRule(DataWrapper objWrapper, int iCustomerId, int iAppPolId, int iAppPolVersionId,string CalledFrom, string CalledFor)
		{
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",iCustomerId);
			objWrapper.AddParameter("@APP_POL_ID",iAppPolId);
			objWrapper.AddParameter("@APP_POL_VERSION_ID",iAppPolVersionId);			
			objWrapper.AddParameter("@CALLED_FROM",CalledFrom);
			objWrapper.AddParameter("@CALLED_FOR",CalledFor);
			SqlParameter sRetVal = (SqlParameter)objWrapper.AddParameter("@RETURN_VALUE",SqlDbType.Int,ParameterDirection.ReturnValue);
			objWrapper.ExecuteNonQuery("Proc_UmbrellaEndorsmentRule");
			if(sRetVal!=null && sRetVal.Value.ToString()!="")
				return int.Parse(sRetVal.Value.ToString());
			else
				return -1;
		}

		public DataTable GetSelectedOtherPolicies(int customerID, int appID, int appVersionID)
		{
			return GetSelectedOtherPolicies(customerID,appID,appVersionID,"","");
		}

		public DataTable GetSelectedOtherPolicies(int customerID, int appID, int appVersionID,string strCalledFrom,string strLOB_ID)
		{
			string	strStoredProc =	"Proc_GetSelectedOtherPolicyInformationForUMB";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);			
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
			objWrapper.AddParameter("@LOB",strLOB_ID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		public DataTable GetPolSelectedOtherPolicies(int customerID, int polID, int polVersionID)
		{
			return GetPolSelectedOtherPolicies(customerID,polID,polVersionID,"","");
		}
		public DataTable GetPolSelectedOtherPolicies(int customerID, int polID, int polVersionID,string strCalledFrom,string strLOB_ID)
		{
			string	strStoredProc =	"Proc_GetPolSelectedOtherPolicyInformationForUMB";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);			
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);	
			objWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
			objWrapper.AddParameter("@LOB",strLOB_ID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		public DataSet GetCoverages(int customerID, int appID, 
			int appVersionID, int recrVehicleID, int currentPageIndex, int pageSize)
		{
			string	strStoredProc =	"Proc_GetUMBRELLA_REC_VEH_COVERAGES";
			
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

		public int DeleteCoverages(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DeleteUMBRELLA_REC_VEH_COV";

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

		/// <summary>
		/// Copies a record the specified number of times.
		/// </summary>
		/// <param name="alInfo"></param>
		/// <returns></returns>
		public int Copy(ArrayList alInfo)
		{
			string	strStoredProc =	"Proc_CopyUMBRELLA_RECREATION_VEHICLES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
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

					int retVal = Convert.ToInt32(sRetVal.Value);
					
					//CompanyID # exceeds max value
					if ( retVal == -1 )
					{
						objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
						return retVal;
					}
				
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
		public static int GetNextCompanyIDNumber(int customerID, int appID, 
			int appVersionID)
		{
			string	strStoredProc =	"Proc_GetNextUMBRELLARecrCompany_ID_Number";
			
			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@APP_ID",appID);
			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			
			int intNextCompanyID = 0;

			try
			{
				intNextCompanyID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
			return intNextCompanyID;

		}
		

		public static int GetNextPolUmbrellaCompanyIDNumber(int customerID, int policyID, int policyVersionID)
		{
			string	strStoredProc =	"Proc_GetNextPolUMBRELLARecrCompany_ID_Number";
			
			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@POLICY_ID",policyID);
			sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID",policyVersionID);
			
			int intNextCompanyID = 0;

			try
			{
				intNextCompanyID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
			return intNextCompanyID;

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
			string	strStoredProc =	"Proc_ActivateDeactivateUMBRELLA_RECREATIONAL_VEHICLES";
			
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
				retVal = objWrapper.ExecuteNonQuery(strStoredProc);
				if(retVal>0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objWrapper,customerID,appID,appVersionID,clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_REC_VEH);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal; 

		}

		public static int ActivateDeactivatePolicyUmbrellaRecVeh(int customerID, int policyID,int policyVersionID, int recrVehicleID, string active)
		{
			string	strStoredProc =	"Proc_ActivateDeactivatePOL_UMBRELLA_RECREATIONAL_VEHICLES";
			
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
				retVal = objWrapper.ExecuteNonQuery(strStoredProc);

				if(retVal>0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objWrapper,customerID,policyID,policyVersionID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_REC_VEH);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}

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
		public int Delete(int customerID, int appID, 
			int appVersionID, int recrVehicleID)
		{
			string	strStoredProc =	"Proc_DeleteUMBRELLA_RECREATIONAL_VEHICLES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			SqlParameter sqlParamRetVal = (SqlParameter)objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				
			int retVal = 0;

			try
			{
				retVal = objWrapper.ExecuteNonQuery(strStoredProc);
				if(retVal>0)
				{
					UmbrellaEndorsementRule(objWrapper,customerID,appID,appVersionID,clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_REC_VEH);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}

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
		public DataSet GetRecrVehicleByID(int customerID, int appID, 
			int appVersionID, int recrVehicleID)
		{
			string	strStoredProc =	"Proc_GetUMBRELLA_RECR_VEHICLESByID";
			
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
		public int AddPolicyUmbrellaRecVeh(Cms.Model.Policy.Umbrella.ClsRecrVehiclesInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertPOL_UMBRELLA_RECREATIONAL_VEHICLES";

			string strTranXML = "";
			int intRetVal	=	-1;
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Umbrella/PolicyAddRecrVehInfo.aspx.resx");
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
			objWrapper.AddParameter("@DISPLACEMENT",objInfo.DISPLACEMENT);
			objWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objWrapper.AddParameter("@USED_IN_RACE_SPEED",objInfo.USED_IN_RACE_SPEED);
			objWrapper.AddParameter("@PRIOR_LOSSES",objInfo.PRIOR_LOSSES);
			objWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",objInfo.IS_UNIT_REG_IN_OTHER_STATE);
			objWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",objInfo.RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",objInfo.DESC_RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@VEHICLE_MODIFIED",objInfo.VEHICLE_MODIFIED);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objWrapper.AddParameter("@VEHICLE_MODIFIED_DETAILS",objInfo.VEHICLE_MODIFIED_DETAILS);		
			objWrapper.AddParameter("@VEH_LIC_ROAD",objInfo.VEH_LIC_ROAD);			
			objWrapper.AddParameter("@REC_VEH_TYPE",objInfo.REC_VEH_TYPE);			
			objWrapper.AddParameter("@REC_VEH_TYPE_DESC",objInfo.REC_VEH_TYPE_DESC);			
			objWrapper.AddParameter("@USED_IN_RACE_SPEED_CONTEST",objInfo.USED_IN_RACE_SPEED_CONTEST);			
			objWrapper.AddParameter("@OTHER_POLICY",objInfo.OTHER_POLICY);
			objWrapper.AddParameter("@C44",objInfo.C44);			
			objWrapper.AddParameter("@IS_BOAT_EXCLUDED",objInfo.IS_BOAT_EXCLUDED);

			SqlParameter objRetVal = (SqlParameter)objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					intRetVal	=	objWrapper.ExecuteNonQuery(strStoredProc);
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID			=	objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New recreational vehicle is added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					intRetVal	=	objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					if(intRetVal>0)
						UmbrellaEndorsementRule(objWrapper,objInfo.CUSTOMER_ID,objInfo.POLICY_ID,objInfo.POLICY_VERSION_ID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_REC_VEH);					
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			

			return Convert.ToInt32(objRetVal.Value);
		}
		public int UpdatePolicyUmbrellaRecVeh(Cms.Model.Policy.Umbrella.ClsRecrVehiclesInfo objOldInfo,Cms.Model.Policy.Umbrella.ClsRecrVehiclesInfo objInfo)
		{
			string	strStoredProc =	"Proc_UpdatePOL_UMBRELLA_RECREATIONAL_VEHICLES";

			string strTranXML	=	"";
			int intRetVal		=	-1;
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Umbrella/PolicyAddRecrVehInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",objInfo.POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",objInfo.POLICY_VERSION_ID);
			objWrapper.AddParameter("@REC_VEH_ID",objInfo.REC_VEH_ID);
			objWrapper.AddParameter("@COMPANY_ID_NUMBER",objInfo.COMPANY_ID_NUMBER);
			objWrapper.AddParameter("@YEAR",objInfo.YEAR);
			objWrapper.AddParameter("@MAKE",objInfo.MAKE);
			objWrapper.AddParameter("@MODEL",objInfo.MODEL);
			objWrapper.AddParameter("@SERIAL",objInfo.SERIAL);
			objWrapper.AddParameter("@STATE_REGISTERED",objInfo.STATE_REGISTERED);
			objWrapper.AddParameter("@VEHICLE_TYPE",objInfo.VEHICLE_TYPE);
			objWrapper.AddParameter("@MANUFACTURER_DESC",objInfo.MANUFACTURER_DESC);
			objWrapper.AddParameter("@HORSE_POWER",objInfo.HORSE_POWER);
			objWrapper.AddParameter("@DISPLACEMENT",objInfo.DISPLACEMENT);
			objWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objWrapper.AddParameter("@USED_IN_RACE_SPEED",objInfo.USED_IN_RACE_SPEED);
			objWrapper.AddParameter("@PRIOR_LOSSES",objInfo.PRIOR_LOSSES);
			objWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",objInfo.IS_UNIT_REG_IN_OTHER_STATE);
			objWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",objInfo.RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",objInfo.DESC_RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@VEHICLE_MODIFIED",objInfo.VEHICLE_MODIFIED);
			objWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY);
			objWrapper.AddParameter("@VEHICLE_MODIFIED_DETAILS",objInfo.VEHICLE_MODIFIED_DETAILS);	
			objWrapper.AddParameter("@VEH_LIC_ROAD",objInfo.VEH_LIC_ROAD);			
			objWrapper.AddParameter("@REC_VEH_TYPE",objInfo.REC_VEH_TYPE);			
			objWrapper.AddParameter("@REC_VEH_TYPE_DESC",objInfo.REC_VEH_TYPE_DESC);			
			objWrapper.AddParameter("@USED_IN_RACE_SPEED_CONTEST",objInfo.USED_IN_RACE_SPEED_CONTEST);			
			objWrapper.AddParameter("@OTHER_POLICY",objInfo.OTHER_POLICY);			
			objWrapper.AddParameter("@C44",objInfo.C44);		
			objWrapper.AddParameter("@IS_BOAT_EXCLUDED",objInfo.IS_BOAT_EXCLUDED);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					intRetVal	=	objWrapper.ExecuteNonQuery(strStoredProc);
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID			=	objInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Recreational vehicle is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					intRetVal	=	objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					if(intRetVal>0)
						UmbrellaEndorsementRule(objWrapper,objInfo.CUSTOMER_ID,objInfo.POLICY_ID,objInfo.POLICY_VERSION_ID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_REC_VEH);
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			return intRetVal;
		}
		public int DeletePolicyUmbrellaRecVeh(int customerID, int policyID, int policyVersionID, int recrVehicleID)
		{
			string	strStoredProc =	"Proc_DeletePOL_UMBRELLA_RECREATIONAL_VEHICLES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@REC_VEH_ID",recrVehicleID);
			SqlParameter sqlParamRetVal = (SqlParameter)objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				
			int retVal = 0;

			try
			{
				retVal = objWrapper.ExecuteNonQuery(strStoredProc);
				if(retVal>0)
				{
					UmbrellaEndorsementRule(objWrapper,customerID,policyID,policyVersionID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_REC_VEH);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal; 

		}
		

		
		/// <summary>
		/// Inserts a record in the database
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Add(ClsRecrVehiclesInfo objInfo)
		{
			string	strStoredProc =	"Proc_InsertUMBRELLA_RECREATION_VEHICLES";

			string strTranXML = "";
			int intRetVal	=	-1;
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaRecrVeh.aspx.resx");
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
			objWrapper.AddParameter("@DISPLACEMENT",objInfo.DISPLACEMENT);
			objWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objWrapper.AddParameter("@USED_IN_RACE_SPEED",objInfo.USED_IN_RACE_SPEED);
			objWrapper.AddParameter("@PRIOR_LOSSES",objInfo.PRIOR_LOSSES);
			objWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",objInfo.IS_UNIT_REG_IN_OTHER_STATE);
			objWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",objInfo.RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",objInfo.DESC_RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@VEHICLE_MODIFIED",objInfo.VEHICLE_MODIFIED);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objWrapper.AddParameter("@VEHICLE_MODIFIED_DETAILS",objInfo.VEHICLE_MODIFIED_DETAILS);			
			objWrapper.AddParameter("@VEH_LIC_ROAD",objInfo.VEH_LIC_ROAD);			
			objWrapper.AddParameter("@REC_VEH_TYPE",objInfo.REC_VEH_TYPE);			
			objWrapper.AddParameter("@REC_VEH_TYPE_DESC",objInfo.REC_VEH_TYPE_DESC);			
			objWrapper.AddParameter("@USED_IN_RACE_SPEED_CONTEST",objInfo.USED_IN_RACE_SPEED_CONTEST);			
			objWrapper.AddParameter("@OTHER_POLICY",objInfo.OTHER_POLICY);			
			objWrapper.AddParameter("@C44",objInfo.C44);			
			objWrapper.AddParameter("@IS_BOAT_EXCLUDED",objInfo.IS_BOAT_EXCLUDED);
			SqlParameter objRetVal = (SqlParameter)objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					intRetVal	=	objWrapper.ExecuteNonQuery(strStoredProc);
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID			=	objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New recreational vehicle is added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					intRetVal	=	objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					if(intRetVal >= 1)
					{
						UmbrellaEndorsementRule(objWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_REC_VEH);
					}
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			

			return Convert.ToInt32(objRetVal.Value);
		}
		
		/// <summary>
		/// Updates the record.
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Update(Cms.Model.Application.HomeOwners.ClsRecrVehiclesInfo objOldInfo,ClsRecrVehiclesInfo objInfo)
		{
			string	strStoredProc =	"Proc_UpdateUMBRELLA_RECREATION_VEHICLES";

			string strTranXML	=	"";
			int intRetVal		=	-1;
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AddUmbrellaRecrVeh.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@REC_VEH_ID",objInfo.REC_VEH_ID);
			objWrapper.AddParameter("@COMPANY_ID_NUMBER",objInfo.COMPANY_ID_NUMBER);
			objWrapper.AddParameter("@YEAR",objInfo.YEAR);
			objWrapper.AddParameter("@MAKE",objInfo.MAKE);
			objWrapper.AddParameter("@MODEL",objInfo.MODEL);
			objWrapper.AddParameter("@SERIAL",objInfo.SERIAL);
			objWrapper.AddParameter("@STATE_REGISTERED",objInfo.STATE_REGISTERED);
			objWrapper.AddParameter("@VEHICLE_TYPE",objInfo.VEHICLE_TYPE);
			objWrapper.AddParameter("@MANUFACTURER_DESC",objInfo.MANUFACTURER_DESC);
			objWrapper.AddParameter("@HORSE_POWER",objInfo.HORSE_POWER);
			objWrapper.AddParameter("@DISPLACEMENT",objInfo.DISPLACEMENT);
			objWrapper.AddParameter("@REMARKS",objInfo.REMARKS);
			objWrapper.AddParameter("@USED_IN_RACE_SPEED",objInfo.USED_IN_RACE_SPEED);
			objWrapper.AddParameter("@PRIOR_LOSSES",objInfo.PRIOR_LOSSES);
			objWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",objInfo.IS_UNIT_REG_IN_OTHER_STATE);
			objWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",objInfo.RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",objInfo.DESC_RISK_DECL_BY_OTHER_COMP);
			objWrapper.AddParameter("@VEHICLE_MODIFIED",objInfo.VEHICLE_MODIFIED);
			objWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY);
			objWrapper.AddParameter("@VEHICLE_MODIFIED_DETAILS",objInfo.VEHICLE_MODIFIED_DETAILS);
			objWrapper.AddParameter("@VEH_LIC_ROAD",objInfo.VEH_LIC_ROAD);			
			objWrapper.AddParameter("@REC_VEH_TYPE",objInfo.REC_VEH_TYPE);			
			objWrapper.AddParameter("@REC_VEH_TYPE_DESC",objInfo.REC_VEH_TYPE_DESC);			
			objWrapper.AddParameter("@USED_IN_RACE_SPEED_CONTEST",objInfo.USED_IN_RACE_SPEED_CONTEST);	
			objWrapper.AddParameter("@OTHER_POLICY",objInfo.OTHER_POLICY);						
			objWrapper.AddParameter("@C44",objInfo.C44);			
			objWrapper.AddParameter("@IS_BOAT_EXCLUDED",objInfo.IS_BOAT_EXCLUDED);

			try
			{
				if ( strTranXML.Trim() == "" )
				{
					intRetVal	=	objWrapper.ExecuteNonQuery(strStoredProc);
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID			=	objInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Recreational vehicle is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					intRetVal	=	objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					if(intRetVal >= 1)					
						UmbrellaEndorsementRule(objWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_REC_VEH);
					
				}
			
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			return intRetVal;
		}

	}
}
