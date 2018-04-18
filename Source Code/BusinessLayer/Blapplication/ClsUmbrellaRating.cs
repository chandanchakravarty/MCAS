/******************************************************************************************
<Author					: -  Ravindra Kumar Gupta
<Start Date				: -	 02/21/2006
<End Date				: -	
<Description			: -  Business Logic for UMBRELLA RATING
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -	Ravindra	
<Modified By			: - 03-22-206
<Purpose				: - To Add Policy Level Functions
************************************************************************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsUmbrellaRatingInfo.
	/// </summary>
	public class ClsUmbrellaRating : Cms.BusinessLayer.BlApplication.clsapplication
	{

		#region Private Instance Variables

		private			bool		boolTransactionLog;
			
		#endregion

		#region Constructors

		public ClsUmbrellaRating()
		{
			boolTransactionLog=base.TransactionLogRequired ;
		}

		#endregion

		#region Add Function

		public int Add(ClsUmbrellaRatingInfo objRatingInfo,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_INSERT_UMBRELLA_RATING_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objRatingInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objRatingInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objRatingInfo.PROT_CLASS);

				//Home construction
				
				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objRatingInfo.NO_OF_FAMILIES));
				objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objRatingInfo.ROOF_OTHER_DESC);

				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objRatingInfo.MONTH_OCC_EACH_YEAR);
				
				
				if(objRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
								
				if(objRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);

			
                
				//Prot devices
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@TEMPERATURE",objRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objRatingInfo.FIRE_PLACES);

				
				if (objRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objRatingInfo.NO_OF_WOOD_STOVES);				
				}
				
				if (objRatingInfo.IS_UNDER_CONSTRUCTION =="1")
				{
					if(objRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objRatingInfo.DWELLING_CONST_DATE );
					else
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				else
				{
					objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objRatingInfo.TWO_MORE_FIRE);
				
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/AddUmbrellaRatingInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objRatingInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objRatingInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objRatingInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objRatingInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objRatingInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Umbrella rating information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO =strCustomInfo;
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
									
				
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
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		#endregion

		#region AddPolicy Function

		public int AddPolicy(Cms.Model.Policy.Umbrella.ClsRatingInfo  objRatingInfo,string strCustomInfo)
		{
			string		strStoredProc	=	"ProcUM_InsertPolicyRatingInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objRatingInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objRatingInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objRatingInfo.PROT_CLASS);

				//Home construction
				
				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objRatingInfo.NO_OF_FAMILIES));
				objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objRatingInfo.ROOF_OTHER_DESC);

				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objRatingInfo.MONTH_OCC_EACH_YEAR);
				
				
				if(objRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
								
				if(objRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);

			
                
				//Prot devices
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@TEMPERATURE",objRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objRatingInfo.FIRE_PLACES);

				
				if (objRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objRatingInfo.NO_OF_WOOD_STOVES);				
				}
				
				if (objRatingInfo.IS_UNDER_CONSTRUCTION =="1")
				{
					if(objRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objRatingInfo.DWELLING_CONST_DATE );
					else
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				else
				{
					objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objRatingInfo.TWO_MORE_FIRE);
								
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyRatingInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objRatingInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objRatingInfo.POLICY_VERSION_ID ;
					objTransactionInfo.POLICY_ID  = objRatingInfo.POLICY_ID;
					objTransactionInfo.CLIENT_ID = objRatingInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objRatingInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Umbrella rating information is added in policy";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO =strCustomInfo;
					
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
									
				
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
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		#endregion


		#region IsRecordExists Function
		public static int  IsRecordExists(int customerID, int appID, int appVersionID,int appDwellingID)
		{
			string	strStoredProc =	"Proc_UM_IS_RATING_EXISTS";
			int intRetResult;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@APP_DWELLING_ID",appDwellingID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			if(intCount==0)
			{
				intRetResult=0;
			}
			else
			{
				intRetResult=1;
			}

			
			return intRetResult;
		}
		#endregion

		#region IsRecordExistsPolicy Function
		public static int  IsRecordExistsPolicy(int customerID, int policyID, int policyVersionID,int dwellingID)
		{
			string	strStoredProc =	"ProcUM_IsRatingExistsPolicy";
			int intRetResult;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			if(intCount==0)
			{
				intRetResult=0;
			}
			else
			{
				intRetResult=1;
			}

			
			return intRetResult;
		}
		#endregion

		#region GetUmbrellaRatingLookUp function
		/// <summary>
		/// Function to fetch LookUp values
		/// Same as For home Rating (SP is also same)
		/// </summary>
		/// <returns>
		/// DataSet containing LookupValues
		///</returns>
		public static DataSet GetUmbrellaRatingLookup()
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			DataSet dsLookup = objDataWrapper.ExecuteDataSet("Proc_GetHomeRatingLookups");

			return dsLookup;
		}
		#endregion

		#region GetUmbrellaRatingInfo Function
        public static DataSet GetUmbrellaRatingInfo(int appId,int customerId,int appVersionId,int dwellingId)
		{
			string		strStoredProc	=	"Proc_GetUmbrellaRatingInformation";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingId,SqlDbType.Int);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount;
		}
		#endregion

		#region GetPolicyRatingInfo Function
		public static DataSet GetPolicyRatingInfo(int customerId,int policyId,int policyVersionId,int dwellingId)
		{
			string		strStoredProc	=	"ProcUM_GetPolicyRatingInfo";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingId,SqlDbType.Int);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount;
		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldRatingInfo">Model object having old information</param>
		/// <param name="objNewRatingInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsUmbrellaRatingInfo objOldRatingInfo,ClsUmbrellaRatingInfo objNewRatingInfo,string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UPDATE_APP_UMBRELLA_RATING_INFO";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOldRatingInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldRatingInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objOldRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objNewRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objNewRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objNewRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objNewRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objNewRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objNewRatingInfo.PROT_CLASS);				
                
				if(objNewRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objNewRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objNewRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objNewRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objNewRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objNewRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objNewRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objNewRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objNewRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objNewRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objNewRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objNewRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objNewRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objNewRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objNewRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objNewRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objNewRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objNewRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objNewRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objNewRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);
						

				//Home construction
			
				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objNewRatingInfo.NO_OF_FAMILIES));
				if(objNewRatingInfo.CONSTRUCTION_CODE==-1)
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objNewRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objNewRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objNewRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objNewRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objNewRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objNewRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objNewRatingInfo.ROOF_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objNewRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objNewRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objNewRatingInfo.MONTH_OCC_EACH_YEAR);

				//Prot devices
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objNewRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objNewRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objNewRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@TEMPERATURE",objNewRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objNewRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objNewRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objNewRatingInfo.FIRE_PLACES);
				if (objNewRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objNewRatingInfo.NO_OF_WOOD_STOVES);				
				}
				
				if (objNewRatingInfo.IS_UNDER_CONSTRUCTION == "1")
				{
					if(objNewRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
					{
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objNewRatingInfo.DWELLING_CONST_DATE );
					}
					else
					{
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
					}
				}
				else
				{
					objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objNewRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objNewRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objNewRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objNewRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objNewRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objNewRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objNewRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objNewRatingInfo.TWO_MORE_FIRE);
								
				if(TransactionLogRequired) 
				{
					objNewRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/AddUmbrellaRatingInfo.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldRatingInfo,objNewRatingInfo);                   
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID			=	objNewRatingInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objNewRatingInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objNewRatingInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNewRatingInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Umbrella rating information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO =strCustomInfo;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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
		#endregion

		#region UpdatePolicy method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldRatingInfo">Model object having old information</param>
		/// <param name="objNewRatingInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicy(Cms.Model.Policy.Umbrella.ClsRatingInfo  objOldRatingInfo,
			Cms.Model.Policy.Umbrella.ClsRatingInfo  objNewRatingInfo,string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="ProcUM_UpdatePolicyRatingInfo";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objOldRatingInfo.POLICY_ID );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldRatingInfo.POLICY_VERSION_ID );
				objDataWrapper.AddParameter("@DWELLING_ID",objOldRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objNewRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objNewRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objNewRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objNewRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objNewRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objNewRatingInfo.PROT_CLASS);				
                
				if(objNewRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objNewRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objNewRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objNewRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objNewRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objNewRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objNewRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objNewRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objNewRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objNewRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objNewRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objNewRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objNewRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objNewRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objNewRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objNewRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objNewRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objNewRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objNewRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objNewRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);
						

				//Home construction
			
				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objNewRatingInfo.NO_OF_FAMILIES));
				if(objNewRatingInfo.CONSTRUCTION_CODE==-1)
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objNewRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objNewRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objNewRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objNewRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objNewRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objNewRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objNewRatingInfo.ROOF_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objNewRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objNewRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objNewRatingInfo.MONTH_OCC_EACH_YEAR);

				//Prot devices
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objNewRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objNewRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objNewRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@TEMPERATURE",objNewRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objNewRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objNewRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objNewRatingInfo.FIRE_PLACES);
				if (objNewRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objNewRatingInfo.NO_OF_WOOD_STOVES);				
				}
				
				if (objNewRatingInfo.IS_UNDER_CONSTRUCTION == "1")
				{
					if(objNewRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
					{
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objNewRatingInfo.DWELLING_CONST_DATE );
					}
					else
					{
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
					}
				}
				else
				{
					objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objNewRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objNewRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objNewRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objNewRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objNewRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objNewRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objNewRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objNewRatingInfo.TWO_MORE_FIRE);
								
				if(TransactionLogRequired) 
				{
					objNewRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/Aspx/Umbrella/PolicyRatingInfo.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldRatingInfo,objNewRatingInfo);                   
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.POLICY_ID 		=	objNewRatingInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objNewRatingInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objNewRatingInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNewRatingInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Umbrella rating information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO =strCustomInfo;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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
		#endregion
	}
}
