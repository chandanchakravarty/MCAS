/******************************************************************************************
<Author				: -	Nidhi
<Start Date			: -	4/28/2005 11:59:31 AM
<End Date			: -	
<Description		: -	Class file for Vehicle Information
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - June 24, 2005
<Modified By		: - Anshuman
<Purpose			: - While saving vehicle info, save autoid info as well

<Modified Date		: - 27/09/2005
<Modified By		: - Mohit
<Purpose			: - Added function GetStateIdForApplication

<Modified Date		: - 29/09/2005
<Modified By		: - Pradeep
<Purpose			: - Removed code for adding and updating from AUTO ID info

<Modified Date		: - 18-10-2005
<Modified By		: - Vijay Arora
<Purpose			: - Change in the AddMotorCycle and UpdateMotorcycle Function (Added a field)

<Modified Date		: - 24-10-2005
<Modified By		: - Mohit Gupta
<Purpose			: - Changes in functions SetUmbVehicleClassType()& SetVehicleClassType().

<Modified Date		: - 03-11-2005
<Modified By		: - Vijay Arora
<Purpose			: - Added the Policy Functions.

<Modified Date		: - 11-24-2005
<Modified By		: - Pradeep
<Purpose			: - Added code to remove coverages

*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlApplication ;
using Cms.BusinessLayer.BlCommon ;
using Cms.Model.Application;
using Cms.Model.Application.PrivatePassenger;
using Cms.DataLayer;
using Cms.Model.Policy;
using System.Globalization;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsVehicleInformation : clsapplication 
	{
		private const	string		APP_VEHICLES			=	"APP_VEHICLES";
		private const string strStoredProcAN			= "Proc_UpdateUmbrellaRemarks";		
		public const string UMB_VEHICLE_TYPE_AUTO = "11956";
		public const string UMB_VEHICLE_TYPE_MOTOR = "11957";
		public const string UMB_VEHICLE_TYPE_MOTOR_HOME = "11958";
		public const string UMB_VEHICLE_TYPE_OTHER = "11959";		
		public const string MOTOR_NO_ENDOR_DATE = "05/01/2003";
		public const string CALLED_FROM_MOTORCYCLE = "MOT";
		public const int returnResult=0;
	
		#region Private Instance Variables
		private			bool		boolTransactionRequired;
		
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_VEHICLES";
		
		//DataSet dsVehicleInfo;
		//DataSet dsPolicyVehicleInfo;

		#endregion

		#region Public Properties
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

		# region GetNextCompanyIDNumber
		public static int GetNextCompanyIDNumber(int customerID, int appID, 
			int appVersionID)
		{
			string	strStoredProc =	"Proc_GetNextUmbrellaVehicleNumber";
			
			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@APP_ID",appID);
			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			
			int intVehicleNumber = 0;

			try
			{
				intVehicleNumber = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
			return intVehicleNumber;

		}

		# endregion

		# region GetNextPolCompanyIDNumber
		public static int GetNextPolCompanyIDNumber(int customerID, int polID, 
			int polVersionID)
		{
			string	strStoredProc =	"Proc_GetNextPolUmbrellaVehicleNumber";
			
			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@POLICY_ID",polID);
			sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID",polVersionID);
			
			int intVehicleNumber = 0;

			try
			{
				intVehicleNumber = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
			return intVehicleNumber;

		}

		# endregion

		#region Utility Functions
		public static int GetNewInsuredVehicleNumber(string sFROM ,string sCustomerID,string sAppID, string sAppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);				
				objDataWrapper.AddParameter("@FROM",sFROM,SqlDbType.VarChar,ParameterDirection.Input ,5);
				objDataWrapper.AddParameter("@CUSTOMER_ID",int.Parse (sCustomerID),SqlDbType.Int );
				objDataWrapper.AddParameter("@APP_ID",int.Parse(sAppID) ,SqlDbType.Int );
				objDataWrapper.AddParameter("@APP_VERSION_ID", int.Parse(sAppVersionID) ,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetNewInsuredVehicleNumber");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public static int GetApplicationLOBID(string sCustomerID,string sAppID, string sAppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",int.Parse (sCustomerID),SqlDbType.Int );
				objDataWrapper.AddParameter("@APP_ID",int.Parse(sAppID) ,SqlDbType.Int );
				objDataWrapper.AddParameter("@APP_VERSION_ID", int.Parse(sAppVersionID) ,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetApplicationLOB");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public static DataSet FetchYearsFromVINMASTER()
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetYearsFromVINMASTER");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}

		public static DataSet FetchMakeFromVINMASTER(string ModelYear)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@MODEL_YEAR",ModelYear,SqlDbType.NVarChar,ParameterDirection.Input ,5);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetMakeFromVINMASTER");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}

		public static DataSet FetchModelFromVINMASTER(string MakeCode, string ModelYear)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@MODEL_YEAR",ModelYear,SqlDbType.NVarChar,ParameterDirection.Input ,5);
				objDataWrapper.AddParameter("@MAKE_CODE",MakeCode,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetModelFromVINMASTER");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public static DataSet FetchBodyTypeFromVINMASTER(string Model,string MakeCode, string ModelYear)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@MODEL_YEAR",ModelYear,SqlDbType.NVarChar,ParameterDirection.Input ,5 );
				objDataWrapper.AddParameter("@MAKE_CODE",MakeCode,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				objDataWrapper.AddParameter("@SERIES_NAME",Model,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetBodyTypeFromVINMASTER");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		public static DataSet FetchBodyTypeFromVINMASTER(string MakeCode, string ModelYear)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@MODEL_YEAR",ModelYear,SqlDbType.NVarChar,ParameterDirection.Input ,5 );
				objDataWrapper.AddParameter("@MAKE_CODE",MakeCode,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				objDataWrapper.AddParameter("@SERIES_NAME",null,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetBodyTypeFromVINMASTER");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public static DataSet FetchVINFromVINMASTER(string Model,string MakeCode, string ModelYear, string BodyType)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@MODEL_YEAR",ModelYear,SqlDbType.NVarChar,ParameterDirection.Input ,5 );
				objDataWrapper.AddParameter("@MAKE_CODE",MakeCode,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				objDataWrapper.AddParameter("@SERIES_NAME",Model,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				objDataWrapper.AddParameter("@BODY_TYPE",BodyType,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetVINFromVINMASTER");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		//Added for Itrack Issue 5831 on 13 May 2009
		public static DataSet FetchVINFromVINMASTER(string Model,string MakeCode, string ModelYear)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@MODEL_YEAR",ModelYear,SqlDbType.NVarChar,ParameterDirection.Input ,5 );
				objDataWrapper.AddParameter("@MAKE_CODE",MakeCode,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				objDataWrapper.AddParameter("@SERIES_NAME",Model,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				objDataWrapper.AddParameter("@BODY_TYPE",null,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetVINFromVINMASTER");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public static DataSet FetchVINFromVINMASTER(string MakeCode, string ModelYear)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@MODEL_YEAR",ModelYear,SqlDbType.NVarChar,ParameterDirection.Input ,5 );
				objDataWrapper.AddParameter("@MAKE_CODE",MakeCode,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				objDataWrapper.AddParameter("@SERIES_NAME",null,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				objDataWrapper.AddParameter("@BODY_TYPE",null,SqlDbType.NVarChar,ParameterDirection.Input ,150);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetVINFromVINMASTER");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public static int GetVehicleCount(int CustomerID,int AppID, int AppVersionID,  int  VehID )
		{
			string		strStoredProc	=	"Proc_GetVehicleCount";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
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
		
		public static int GetPolVehicleCount(int CustomerID,int PolID, int PolVersionID,  int  VehID )
		{
			string		strStoredProc	=	"Proc_GetPolVehicleCount";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
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
		
		public static int GetInceptionDate(int CustomerID,int AppID, int AppVersionID)
		{
			string		strStoredProc	=	"Proc_GetAppInceptionDate";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataSet dsTemp = new DataSet();
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
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
		
		public static int GetPolInceptionDate(int CustomerID,int PolID, int PolVersionID)
		{
			string		strStoredProc	=	"Proc_GetPolInceptionDate";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			DataSet dsTemp = new DataSet();
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
				dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
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
		//METHOD COMMENTED BY PAWAN AS NOT USED
		/*public static string GetNewVehicleID(string sFROM)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@FROM",sFROM,SqlDbType.VarChar,ParameterDirection.Input ,5);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetNewVehicleID");
			
				return dsTemp.Tables[0].Rows[0][0].ToString();
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}*/
		
		public static string FetchVehicleXMLFromAPPVehicleTable(int CustomerID,int AppID, int AppVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppVehicleInformation");
			
				//return dsTemp.GetXml();
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		

		public static string FetchUmbrellaVehicleXML(int CustomerID,int AppID, int AppVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaVehicleInformation");
			
				return dsTemp.GetXml();
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		
		public DataSet GetUmbrellaVehicleInformation(int CustomerID,int AppID, 
			int AppVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaVehicleInformation");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}


		#region Policy FetchCycleFromPOLVehicleTable
		public static DataSet FetchCycleFromPOLVehicleTable(int CustomerID,
			int polID, 
			int polVersionID,
			int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYID",polID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYVERSIONID",polVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyCycleInformation");
			
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}




		#endregion
		
		public static string FetchCycleXMLFromAPPVehicleTable(int CustomerID,int AppID, int AppVersionID, int VehicleID)
		{
			try
			{
				
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppCycleInformation");
		
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		public static string FetchVehicleXMLFromCustVehicleTable(int CustomerID,int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustVehicleInformation");
			
				return dsTemp.GetXml();
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		
		public static DataSet FetchVINMasterDetailsFromVIN(string VIN)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@VIN",VIN,SqlDbType.VarChar );
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GetDetailsFromVIN");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}


		public static int GetVehicleCount(ClsVehicleInfo objOldVehicleInfo,DataWrapper objDataWrapper,int intVEH_ID)
		{
			string		strStoredProc	=	"Proc_GetVehicleCount";			
			
			try
			{
				objDataWrapper.ClearParameteres(); 
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID );
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID );
				objDataWrapper.AddParameter("@VEHICLE_ID",intVEH_ID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);				
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);				
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsVehicleInformation()
		{
			boolTransactionRequired = base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objOldVehicleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddVehicle(ClsVehicleInfo objOldVehicleInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertVEHICLE_INFO_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			
			//You are not passing the foreign key values from fromt end
			//LIke customer id and vehilcle id

		
			objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
			//objDataWrapper.AddParameter	("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
			objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
			objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
			objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
			objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
			objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
			objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
			objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
			objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
			objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
			objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
			objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
			objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
			objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
			objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
			objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
			objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);
			
			//add by kranti for com class description
			objDataWrapper.AddParameter("@CLASS_DESCRIPTION",objOldVehicleInfo.CLASS_DESCRIPTION);



			objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
			objDataWrapper.AddParameter("@MOTORCYCLE_TYPE_CODE",objOldVehicleInfo.MOTORCYCLE_TYPE_CODE);

			if(objOldVehicleInfo.REGN_PLATE_NUMBER!="")
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
			else
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",null);
			objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
			objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
			objDataWrapper.AddParameter("@VEHICLE_TYPE_CODE",objOldVehicleInfo.VEH_TYPE_CODE);
			objDataWrapper.AddParameter("@AMOUNT",DefaultValues.GetDoubleNullFromNegative(objOldVehicleInfo.AMOUNT));
				
				 
			if(objOldVehicleInfo.SYMBOL >= 0)
			{
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
			}
				 
			objDataWrapper.AddParameter("@MILES_TO_WORK",objOldVehicleInfo.MILES_TO_WORK);	
				 
			objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
			if(objOldVehicleInfo.RADIUS_OF_USE==0)
				objDataWrapper.AddParameter("@RADIUS_OF_USE",null);
			else
				objDataWrapper.AddParameter("@RADIUS_OF_USE",objOldVehicleInfo.RADIUS_OF_USE);


			// CALCULATE AND SAVE AGE - was here.
			/*int vehicleAge =0;
			string strYear = objOldVehicleInfo.VEHICLE_YEAR ;
			if (strYear.Trim() !="" && strYear.Trim() !="0" && strYear.Trim() !="0.0" )
			{
				int vehicleYear = Convert.ToInt16(strYear);
				int currentYear = DateTime.Now.Year;
					

				vehicleAge  = currentYear - vehicleYear;
				vehicleAge ++;
			}

			if(vehicleAge!=0)
			{
				objDataWrapper.AddParameter("@VEHICLE_AGE",vehicleAge);				
			}*/


			string strAge = objOldVehicleInfo.VEHICLE_AGE.ToString();
			int vehicleYear = Convert.ToInt32(strAge);

			objDataWrapper.AddParameter("@VEHICLE_AGE",vehicleYear);				

			objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",objOldVehicleInfo.PASSIVE_SEAT_BELT);
			objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
			objDataWrapper.AddParameter("@PURCHASE_DATE",DefaultValues.GetDateNull(objOldVehicleInfo.PURCHASE_DATE));
			objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
			if(objOldVehicleInfo.ANNUAL_MILEAGE==-1)
				objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
			else															
				objDataWrapper.AddParameter("@ANNUAL_MILEAGE",DefaultValues.GetDoubleNullFromNegative(objOldVehicleInfo.ANNUAL_MILEAGE));
			objDataWrapper.AddParameter("@VEHICLE_CC",DefaultValues.GetIntNullFromNegative(objOldVehicleInfo.VEHICLE_CC));
			objDataWrapper.AddParameter("@USE_VEHICLE_CODE",objOldVehicleInfo.USE_VEHICLE_CODE);
			objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);
			objDataWrapper.AddParameter("@APP_VEHICLE_CLASS_CODE",objOldVehicleInfo.CLASS);
			objDataWrapper.AddParameter("@CAR_POOL",objOldVehicleInfo.CAR_POOL);
			objDataWrapper.AddParameter("@SNOWPLOW_CODE",objOldVehicleInfo.SNOWPLOW_CONDS);
			objDataWrapper.AddParameter("@MISC_AMT",objOldVehicleInfo.MISC_AMT);
			objDataWrapper.AddParameter("@MISC_DESC_AMT",objOldVehicleInfo.MISC_EQUIP_DESC);
			objDataWrapper.AddParameter("@IS_SUSPENDED",objOldVehicleInfo.IS_SUSPENDED);
			objDataWrapper.AddParameter("@COMPRH_ONLY",objOldVehicleInfo.COMPRH_ONLY);
  

			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

			int returnResult = 0;
			int VEHICLE_ID;
			if(TransactionLogRequired)
			{
				objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\VehicleInfo.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.CLIENT_ID = objOldVehicleInfo.CUSTOMER_ID;
				objTransactionInfo.APP_ID = objOldVehicleInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objOldVehicleInfo.APP_VERSION_ID;
				objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"New vehicle is added from Quick Quote";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				//Executing the query to save vehicle info
				returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
					
		
				// inserting transaction log
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);

				/*int intVehCount;
				if (VEHICLE_ID>0) 
					intVehCount = GetVehicleCount(objOldVehicleInfo,objDataWrapper,VEHICLE_ID);
				else
					intVehCount = GetVehicleCount(objOldVehicleInfo,objDataWrapper,0);*/
		
				objDataWrapper.ClearParameteres();
				//set vehicle id of autoid
				//objAutoIDInfo.VEHICLE_ID	=	VEHICLE_ID;
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
			}
				
			objDataWrapper.ClearParameteres();

		
				
			/*
				// populating AutoId Info simultaneously to update APP_AUTO_ID_CARD_INFO
				ClsAutoIDInfo objAutoIDInfo	=	new ClsAutoIDInfo();
				PopulateAutoID(objAutoIDInfo,objOldVehicleInfo);
				
				objAutoIDInfo.VEHICLE_ID = VEHICLE_ID;

				
				ClsAutoIDInformation objAutoIDInformation = new ClsAutoIDInformation();
				//if(!ClsAutoIDInformation.isUpdated(objAutoIDInfo.CUSTOMER_ID,objAutoIDInfo.APP_ID,objAutoIDInfo.APP_VERSION_ID,objAutoIDInfo.VEHICLE_ID))
				//{
				//	objDataWrapper.ClearParameteres();
					returnResult = objAutoIDInformation.Save(objAutoIDInfo,objDataWrapper);
				//}
				*/
				
			//			//Insert/delete relevant COVERAGE/ENDORSEMENT *********************	
			//			objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
			//			objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
			//			objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
			//			objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
			//
			//			objDataWrapper.ExecuteNonQuery("Proc_Update_VEHICLE_COVERAGES");
			//			//***********************

			return VEHICLE_ID;
		}

		/// <summary>
		/// Update Multicar in vehicle according to the Vehcile Count.
		/// </summary>
		/// 
		public void UpdateMultiCarVehicle(int CustomerID,int AppID,int AppVersionID,DataWrapper objWrapper)
		{
			if ( objWrapper.CommandParameters.Length > 0 )
			{
				objWrapper.ClearParameteres();
			}

			string		strStoredProc	=	"Proc_UpdateMultiCarVehicleInfo";			
			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@APP_ID",AppID);
				objWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);

				objWrapper.ExecuteNonQuery(strStoredProc);				
						
				
			}
			catch(Exception ex)
			{
				throw(ex);				
			}
			finally
			{
				//if(objWrapper != null) objWrapper.Dispose();
			}	

		}
		#region Add/update/get/delete/ activate Deactivate Aviation Vehicle
		public int AddAviationPolicyVehicle(Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objVehicleInfo, string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_InsertAviationPolicyVehicleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
				//objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MAKE_OTHER",objVehicleInfo.MAKE_OTHER);
				objDataWrapper.AddParameter("@MODEL",objVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@MODEL_OTHER",objVehicleInfo.MODEL_OTHER);
				
				objDataWrapper.AddParameter("@USE_VEHICLE",objVehicleInfo.USE_VEHICLE);
				objDataWrapper.AddParameter("@COVG_PERIMETER",objVehicleInfo.COVG_PERIMETER);
				objDataWrapper.AddParameter("@REG_NUMBER",objVehicleInfo.REG_NUMBER);
				objDataWrapper.AddParameter("@SERIAL_NUMBER",objVehicleInfo.SERIAL_NUMBER);
				objDataWrapper.AddParameter("@CERTIFICATION",objVehicleInfo.CERTIFICATION);
				objDataWrapper.AddParameter("@REGISTER",objVehicleInfo.REGISTER);
				objDataWrapper.AddParameter("@ENGINE_TYPE",objVehicleInfo.ENGINE_TYPE);
				objDataWrapper.AddParameter("@WING_TYPE",objVehicleInfo.WING_TYPE);

				objDataWrapper.AddParameter("@CREW",objVehicleInfo.CREW);
				objDataWrapper.AddParameter("@PAX",objVehicleInfo.PAX);
				objDataWrapper.AddParameter("@REMARKS",objVehicleInfo.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objVehicleInfo.IS_ACTIVE);
				 
				objDataWrapper.AddParameter("@CREATED_BY",objVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objVehicleInfo.CREATED_DATETIME);			
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int VEHICLE_ID;
				if(TransactionLogRequired)
				{
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\aviation\AddpolicyVehicle.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objVehicleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID			  = objVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1521", "");// "New Aviation Policy vehicle is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query to save vehicle info
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
					
					objDataWrapper.ClearParameteres();
					// inserting transaction log
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
					//set vehicle id of autoid
					//objAutoIDInfo.VEHICLE_ID	=	VEHICLE_ID;
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
				}
				
				objDataWrapper.ClearParameteres();
				
				//Insert default coverages*************
				ClsVehicleCoverages objCov = new ClsVehicleCoverages("AVIATION");
				objCov.SaveDefaultCoveragesPolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,VEHICLE_ID);

			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
					
				return VEHICLE_ID;
				
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
		public int UpdateAviationPolicyVehicle(Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objOldVehicleInfo,Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objVehicleInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdateAviationPolicyVehicleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@USE_VEHICLE",objVehicleInfo.USE_VEHICLE);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MAKE_OTHER",objVehicleInfo.MAKE_OTHER);
				objDataWrapper.AddParameter("@MODEL",objVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@MODEL_OTHER",objVehicleInfo.MODEL_OTHER);
				objDataWrapper.AddParameter("@COVG_PERIMETER",objVehicleInfo.COVG_PERIMETER);
				objDataWrapper.AddParameter("@REG_NUMBER",objVehicleInfo.REG_NUMBER);
				objDataWrapper.AddParameter("@SERIAL_NUMBER",objVehicleInfo.SERIAL_NUMBER);
				objDataWrapper.AddParameter("@CERTIFICATION",objVehicleInfo.CERTIFICATION);
				objDataWrapper.AddParameter("@REGISTER",objVehicleInfo.REGISTER);
				objDataWrapper.AddParameter("@ENGINE_TYPE",objVehicleInfo.ENGINE_TYPE);
				objDataWrapper.AddParameter("@WING_TYPE",objVehicleInfo.WING_TYPE);
				objDataWrapper.AddParameter("@CREW",objVehicleInfo.CREW);
				objDataWrapper.AddParameter("@PAX",objVehicleInfo.PAX);
				objDataWrapper.AddParameter("@REMARKS",objVehicleInfo.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objVehicleInfo.IS_ACTIVE);
				 
				objDataWrapper.AddParameter("@MODIFIED_BY",objVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objVehicleInfo.LAST_UPDATED_DATETIME);	

				if(TransactionRequired) 
				{
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\aviation\addPolicyVehicle.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo,objVehicleInfo);

					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
						objTransactionInfo.POLICY_ID			=	objVehicleInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objVehicleInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Aviation Policy Vehicle is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
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
		public int DeleteAviationPolicyVehicle(Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objVehicleInfo, string strCustomInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeleteAviationPolicyVehicle";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);

				if(TransactionRequired) 
				{			

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID			=	objVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Aviation Policy Vehicle is Deleted";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
				{
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}
		public void ActivateDeactivateAviationPolicyVehicle(Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objVehicleInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAviationPolicyVehicle";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string strVehicleName="Aviation Policy Vehicle";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/aviation/AddPolicyVehicle.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID			=	objVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	strVehicleName + " is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	strVehicleName+ " is Deactivated";
					
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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
		/*public int AddAviationVehicle(Model.Application.Aviation.ClsAviationVehicleInfo objVehicleInfo, string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_InsertAviationVehicleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				//objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MAKE_OTHER",objVehicleInfo.MAKE_OTHER);
				objDataWrapper.AddParameter("@MODEL",objVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@MODEL_OTHER",objVehicleInfo.MODEL_OTHER);
				
				objDataWrapper.AddParameter("@USE_VEHICLE",objVehicleInfo.USE_VEHICLE);
				objDataWrapper.AddParameter("@COVG_PERIMETER",objVehicleInfo.COVG_PERIMETER);
				objDataWrapper.AddParameter("@REG_NUMBER",objVehicleInfo.REG_NUMBER);
				objDataWrapper.AddParameter("@SERIAL_NUMBER",objVehicleInfo.SERIAL_NUMBER);
				objDataWrapper.AddParameter("@CERTIFICATION",objVehicleInfo.CERTIFICATION);
				objDataWrapper.AddParameter("@REGISTER",objVehicleInfo.REGISTER);
				objDataWrapper.AddParameter("@ENGINE_TYPE",objVehicleInfo.ENGINE_TYPE);
				objDataWrapper.AddParameter("@WING_TYPE",objVehicleInfo.WING_TYPE);

				objDataWrapper.AddParameter("@CREW",objVehicleInfo.CREW);
				objDataWrapper.AddParameter("@PAX",objVehicleInfo.PAX);
				objDataWrapper.AddParameter("@REMARKS",objVehicleInfo.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objVehicleInfo.IS_ACTIVE);
				 
				objDataWrapper.AddParameter("@CREATED_BY",objVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objVehicleInfo.CREATED_DATETIME);			
				


				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int VEHICLE_ID;
				if(TransactionLogRequired)
				{
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\aviation\AddVehicle.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objVehicleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New aviation vehicle is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					//Executing the query to save vehicle info
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
					
					objDataWrapper.ClearParameteres();
					// inserting transaction log
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);

					//set vehicle id of autoid
					//objAutoIDInfo.VEHICLE_ID	=	VEHICLE_ID;
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
				}
				
				objDataWrapper.ClearParameteres();
				
				//Insert default coverages*************
				ClsVehicleCoverages objCov = new ClsVehicleCoverages("AVIATION");
				objCov.SaveDefaultCoveragesApp(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,VEHICLE_ID);

			
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
					
				return VEHICLE_ID;
				
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
		public int UpdateAviationVehicle(Model.Application.Aviation.ClsAviationVehicleInfo objOldVehicleInfo,Model.Application.Aviation.ClsAviationVehicleInfo objVehicleInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strXML;

			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdateAviationVehicleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@USE_VEHICLE",objVehicleInfo.USE_VEHICLE);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MAKE_OTHER",objVehicleInfo.MAKE_OTHER);
				objDataWrapper.AddParameter("@MODEL",objVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@MODEL_OTHER",objVehicleInfo.MODEL_OTHER);
				objDataWrapper.AddParameter("@COVG_PERIMETER",objVehicleInfo.COVG_PERIMETER);
				objDataWrapper.AddParameter("@REG_NUMBER",objVehicleInfo.REG_NUMBER);
				objDataWrapper.AddParameter("@SERIAL_NUMBER",objVehicleInfo.SERIAL_NUMBER);
				objDataWrapper.AddParameter("@CERTIFICATION",objVehicleInfo.CERTIFICATION);
				objDataWrapper.AddParameter("@REGISTER",objVehicleInfo.REGISTER);
				objDataWrapper.AddParameter("@ENGINE_TYPE",objVehicleInfo.ENGINE_TYPE);
				objDataWrapper.AddParameter("@WING_TYPE",objVehicleInfo.WING_TYPE);
				objDataWrapper.AddParameter("@CREW",objVehicleInfo.CREW);
				objDataWrapper.AddParameter("@PAX",objVehicleInfo.PAX);
				objDataWrapper.AddParameter("@REMARKS",objVehicleInfo.REMARKS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objVehicleInfo.IS_ACTIVE);
				 
				objDataWrapper.AddParameter("@MODIFIED_BY",objVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objVehicleInfo.LAST_UPDATED_DATETIME);	

				if(TransactionRequired) 
				{
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\aviation\addVehicle.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo,objVehicleInfo);

					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Aviation Vehicle is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
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
		public static string FetchAviationVehicleXML(int CustomerID,int AppID, int AppVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppAviationVehicleInformation");
			
				//return dsTemp.GetXml();
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}*/
		public static string FetchPolAviationVehicleXML(int CustomerID,int polID, int PolVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",polID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolAviationVehicleInformation");
				
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
		public static DataSet FetchAviationVehicleModel()
		{
			try
			{
				DataSet dsTemp = new DataSet();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAviationVehicleModel");
				
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
        /*
		public int DeleteAviationVehicle(Model.Application.Aviation.ClsAviationVehicleInfo objVehicleInfo, string strCustomInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeleteAviationVehicle";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);

				if(TransactionRequired) 
				{			

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Aviation Vehicle is Deleted";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
				{
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}
		public void ActivateDeactivateAviationVehicle(Model.Application.Aviation.ClsAviationVehicleInfo objVehicleInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAviationVehicle";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string strVehicleName="Aviation Vehicle";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/AddVehicle.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	strVehicleName + " is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	strVehicleName+ " is Deactivated";
					
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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

		}*/
		#endregion
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objOldVehicleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/// 
		public int Add(ClsVehicleInfo objOldVehicleInfo)
		{
			string		strStoredProc	=	"Proc_InsertVehicleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			// populating AutoId Info simultaneously to update APP_AUTO_ID_CARD_INFO
			// ClsAutoIDInfo objAutoIDInfo	=	new ClsAutoIDInfo();
			// PopulateAutoIDInfo(objAutoIDInfo,objOldVehicleInfo);

			//You are not passing the foreign key values from fromt end
			//LIke customer id and vehilcle id

			try
			{
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				//objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);
				objDataWrapper.AddParameter("@BUSS_PERM_RESI",objOldVehicleInfo.BUSS_PERM_RESI);
				//Added by swarup  on 15-Dec-2006
				objDataWrapper.AddParameter("@SNOWPLOW_CONDS",objOldVehicleInfo.SNOWPLOW_CONDS);
				objDataWrapper.AddParameter("@CAR_POOL",objOldVehicleInfo.CAR_POOL);
				//objDataWrapper.AddParameter("@SAFETY_BELT",objOldVehicleInfo.SAFETY_BELT);
				objDataWrapper.AddParameter("@AUTO_POL_NO",objOldVehicleInfo.AUTO_POL_NO);
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				
				if(objOldVehicleInfo.REGN_PLATE_NUMBER!="")
					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				else
					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",null);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				
				if(objOldVehicleInfo.AMOUNT !=0)
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				 
				//if(objOldVehicleInfo.SYMBOL!=0)
				//{
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				//}
				 

				if(objOldVehicleInfo.VEHICLE_AGE!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);				
				}
				 
				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);			
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);

				//added by anurag verma on 22/09/2005 for merging personal vehicle info screen
				objDataWrapper.AddParameter("@IS_OWN_LEASE",objOldVehicleInfo.IS_OWN_LEASE);
				
				// Commented & Added by Mohit
				//objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);
				
				if(objOldVehicleInfo.PURCHASE_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);                
				else
					objDataWrapper.AddParameter("@PURCHASE_DATE",System.DBNull.Value);

				// End

				
				objDataWrapper.AddParameter("@IS_NEW_USED",objOldVehicleInfo.IS_NEW_USED);
				objDataWrapper.AddParameter("@MILES_TO_WORK",objOldVehicleInfo.MILES_TO_WORK);
				objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
				objDataWrapper.AddParameter("@VEH_PERFORMANCE",objOldVehicleInfo.VEH_PERFORMANCE);
				objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);
				if(objOldVehicleInfo.ANNUAL_MILEAGE==0 || objOldVehicleInfo.ANNUAL_MILEAGE==0.0 ||objOldVehicleInfo.ANNUAL_MILEAGE < 0)
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",objOldVehicleInfo.ANNUAL_MILEAGE);

				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",DefaultValues.GetStringNull(objOldVehicleInfo.PASSIVE_SEAT_BELT));
				objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
							
				//objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",objOldVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE);				
				//objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNDERINS_MOTOR_INJURY_COVE);				
				//objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNINS_MOTOR_INJURY_COVE);	
			
				//modified by vj on 10-10-2005 to set vehicle use to personal or commercial
				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);	

				if (objOldVehicleInfo.CLASS_PER == 0)
				{
					objDataWrapper.AddParameter("@CLASS_PER",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@CLASS_PER", objOldVehicleInfo.CLASS_PER);	
				}
				

				if (objOldVehicleInfo.CLASS_COM == 0)
				{
					objDataWrapper.AddParameter("@CLASS_COM",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@CLASS_COM",objOldVehicleInfo.CLASS_COM);	
				}
				

				if (objOldVehicleInfo.VEHICLE_TYPE_PER == 0)
				{
					objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",objOldVehicleInfo.VEHICLE_TYPE_PER);	
				}
				if (objOldVehicleInfo.IS_SUSPENDED == 0)
				{
					objDataWrapper.AddParameter("@IS_SUSPENDED",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@IS_SUSPENDED",objOldVehicleInfo.IS_SUSPENDED);	
				}				

				if (objOldVehicleInfo.VEHICLE_TYPE_COM == 0)
				{
					objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",objOldVehicleInfo.VEHICLE_TYPE_COM);	
				}
				if(objOldVehicleInfo.RADIUS_OF_USE==0)
					objDataWrapper.AddParameter("@RADIUS_OF_USE",null);
				else
					objDataWrapper.AddParameter("@RADIUS_OF_USE",objOldVehicleInfo.RADIUS_OF_USE);
				objDataWrapper.AddParameter("@TRANSPORT_CHEMICAL",objOldVehicleInfo.TRANSPORT_CHEMICAL);
				objDataWrapper.AddParameter("@COVERED_BY_WC_INSU",objOldVehicleInfo.COVERED_BY_WC_INSU);
				objDataWrapper.AddParameter("@CLASS_DESCRIPTION",objOldVehicleInfo.CLASS_DESCRIPTION);


				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int VEHICLE_ID;
				if(TransactionLogRequired)
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\VehicleInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objOldVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objOldVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1499", "");//"New vehicle is added";					
					//Executing the query
					//Executing the query to save vehicle info
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
					objOldVehicleInfo.INSURED_VEH_NUMBER = VEHICLE_ID;
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					//Remove nodes 
					//For Personal use, remove nodes for commercial user					
					if(objOldVehicleInfo.USE_VEHICLE==11332)
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_COM']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='CLASS_COM']");					
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='CLASS_PER' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_PER' and @NewValue='0']","NewValue","null");
					}
					else //Vehicle is for commercial use, remove nodes for personal user
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='MULTI_CAR']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_USE']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='BUSS_PERM_RESI']");

						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_PER']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='CLASS_PER']");					
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='CLASS_COM' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_COM' and @NewValue='0']","NewValue","null");
					}
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='BUSS_PERM_RESI' and @NewValue='0']","NewValue","null");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objDataWrapper.ClearParameteres();
					// inserting transaction log
					objTransactionInfo.CUSTOM_INFO = ";Insured Vehicle Number = " + VEHICLE_ID + ";Make = " + objOldVehicleInfo.MAKE + ";Model = " + objOldVehicleInfo.MODEL;
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);

					//set vehicle id of autoid
					//objAutoIDInfo.VEHICLE_ID	=	VEHICLE_ID;
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
				}
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
				returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetAssgndDrvrType");

				objDataWrapper.ClearParameteres();

				//				//Insert/delete relevant coverages*********************	
				//				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				//				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				//				objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				//
				//
				//				objDataWrapper.ExecuteNonQuery("Proc_Update_VEHICLE_COVERAGES");
				//				//***********************
				//				
				//				//Update Driver endorsements
				//				Cms.BusinessLayer.BlApplication.ClsDriverDetail objDriver = new ClsDriverDetail();
				//				objDriver.UpdateDriverEndorsements(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,VEHICLE_ID,objDataWrapper);
				//				//End of endorsements
				//
				//				//Ravindra (04-28-06) To update Endorsment's linked with Policy Level coverages
				//				Cms.BusinessLayer.BlApplication.ClsVehicleCoverages.UpdateApplicationEndorsements(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,VEHICLE_ID,objDataWrapper);
				////////////////////
				///
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.SaveDefaultCoveragesApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,VEHICLE_ID);
				objDataWrapper.ClearParameteres();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,RuleType.AutoDriverDep,VEHICLE_ID);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if(VEHICLE_ID>0)
				{
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					objDriverDetail.UpdateVehicleClassNew(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID, objOldVehicleInfo.APP_VERSION_ID,objDataWrapper);
				}
				
					
				return VEHICLE_ID;
				
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


		public int Add(ClsVehicleInfo objOldVehicleInfo, string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_InsertVehicleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			// populating AutoId Info simultaneously to update APP_AUTO_ID_CARD_INFO
			// ClsAutoIDInfo objAutoIDInfo	=	new ClsAutoIDInfo();
			// PopulateAutoIDInfo(objAutoIDInfo,objOldVehicleInfo);

			//You are not passing the foreign key values from fromt end
			//LIke customer id and vehilcle id

			try
			{
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				//objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);
				
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				
				if(objOldVehicleInfo.REGN_PLATE_NUMBER!="")
					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				else
					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",null);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				
				if(objOldVehicleInfo.AMOUNT !=0)
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				 
				//if(objOldVehicleInfo.SYMBOL!=0)
				//{
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				//}
				 

				if(objOldVehicleInfo.VEHICLE_AGE!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);				
				}
				 
				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);			
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);

				//added by anurag verma on 22/09/2005 for merging personal vehicle info screen
				objDataWrapper.AddParameter("@IS_OWN_LEASE",objOldVehicleInfo.IS_OWN_LEASE);
				
				// Commented & Added by Mohit
				//objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);
				
				if(objOldVehicleInfo.PURCHASE_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);                
				else
					objDataWrapper.AddParameter("@PURCHASE_DATE",System.DBNull.Value);

				// End

				
				objDataWrapper.AddParameter("@IS_NEW_USED",objOldVehicleInfo.IS_NEW_USED);
				objDataWrapper.AddParameter("@MILES_TO_WORK",objOldVehicleInfo.MILES_TO_WORK);
				objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
				objDataWrapper.AddParameter("@VEH_PERFORMANCE",objOldVehicleInfo.VEH_PERFORMANCE);
				objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);
				if(objOldVehicleInfo.ANNUAL_MILEAGE==-1)
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",objOldVehicleInfo.ANNUAL_MILEAGE);

				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",DefaultValues.GetStringNull(objOldVehicleInfo.PASSIVE_SEAT_BELT));
				objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
							
				objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",objOldVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE);				
				objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNDERINS_MOTOR_INJURY_COVE);				
				objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNINS_MOTOR_INJURY_COVE);	
			
				//modified by vj on 10-10-2005 to set vehicle use to personal or commercial
				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);	
				objDataWrapper.AddParameter("@CLASS_PER",objOldVehicleInfo.CLASS_PER);	
				objDataWrapper.AddParameter("@CLASS_COM",objOldVehicleInfo.CLASS_COM);	
				objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",objOldVehicleInfo.VEHICLE_TYPE_PER);	
				objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",objOldVehicleInfo.VEHICLE_TYPE_COM);	


				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int VEHICLE_ID;
				if(TransactionLogRequired)
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\VehicleInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID = objOldVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objOldVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1499", ""); //"New vehicle is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					//Executing the query
					//Executing the query to save vehicle info
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
					
					objDataWrapper.ClearParameteres();
					// inserting transaction log
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);

					//set vehicle id of autoid
					//objAutoIDInfo.VEHICLE_ID	=	VEHICLE_ID;
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
				}
				
				objDataWrapper.ClearParameteres();
				
				//Insert/delete relevant coverages*********************	
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);

				objDataWrapper.ExecuteNonQuery("Proc_Update_VEHICLE_COVERAGES");
				//***********************
				
				//Update Driver endorsements
				Cms.BusinessLayer.BlApplication.ClsDriverDetail objDriver = new ClsDriverDetail();
				objDriver.UpdateDriverEndorsements(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,VEHICLE_ID,objDataWrapper);
				//End of endorsements

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
					
				return VEHICLE_ID;
				
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



		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objOldVehicleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddMotorCycle(ClsVehicleInfo objOldVehicleInfo)
		{
			string		strStoredProc	=	"Proc_InsertMotorcycleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			//ClsAutoIDInfo objAutoIDInfo	=	new ClsAutoIDInfo();
			//PopulateAutoIDInfo(objAutoIDInfo,objOldVehicleInfo);

			//You are not passing the foreign key values from fromt end
			//LIke customer id and vehilcle id

			try
			{
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);			
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
			
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				
				if (objOldVehicleInfo.AMOUNT==-1)
				{
					objDataWrapper.AddParameter("@AMOUNT",System.DBNull.Value);					
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				if(objOldVehicleInfo.VEHICLE_AGE!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",0);
				}

				if(objOldVehicleInfo.VEHICLE_CC!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_CC",objOldVehicleInfo.VEHICLE_CC);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_CC",null);
				}
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);
				//Nov 30,2005:Sumit Chhabra:Commented the is_active parameter as default value of 'Y' will be taken 
				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);

				//added by vj on 18-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",objOldVehicleInfo.APP_VEHICLE_CLASS);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int  VEHICLE_ID;
				if(TransactionLogRequired)
				{
					
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMotorcycleInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New motorcycle is added";
					
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());

					objDataWrapper.ClearParameteres();

					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
				}

				objDataWrapper.ClearParameteres();
				
				/*
				objAutoIDInfo.VEHICLE_ID =VEHICLE_ID;
				ClsAutoIDInformation objAutoIDInformation = new ClsAutoIDInformation();
				if(!ClsAutoIDInformation.isUpdated(objAutoIDInfo.CUSTOMER_ID,objAutoIDInfo.APP_ID,objAutoIDInfo.APP_VERSION_ID,objAutoIDInfo.VEHICLE_ID))
				{
					objDataWrapper.ClearParameteres();
					returnResult = objAutoIDInformation.Add(objAutoIDInfo,objDataWrapper);
				}				
				objDataWrapper.ClearParameteres();
				*/
				
				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				return VEHICLE_ID;
				
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
		
		

		public int AddMotorCycle(ClsVehicleInfo objOldVehicleInfo, string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_InsertMotorcycleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			//ClsAutoIDInfo objAutoIDInfo	=	new ClsAutoIDInfo();
			//PopulateAutoIDInfo(objAutoIDInfo,objOldVehicleInfo);

			//You are not passing the foreign key values from fromt end
			//LIke customer id and vehilcle id

			try
			{
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);			
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
			
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);				

				if (objOldVehicleInfo.AMOUNT==-1)
				{
					objDataWrapper.AddParameter("@AMOUNT",System.DBNull.Value);					
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}				
				if(objOldVehicleInfo.VEHICLE_AGE!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",0);
				}

				if(objOldVehicleInfo.VEHICLE_CC!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_CC",objOldVehicleInfo.VEHICLE_CC);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_CC",null);
				}
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);
				//Nov 30,2005:Sumit Chhabra:Commented the is_active parameter as default value of 'Y' will be taken 
				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);

				//added by vj on 18-10-2005
				//objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",objOldVehicleInfo.APP_VEHICLE_CLASS);
				if(objOldVehicleInfo.APP_VEHICLE_CLASS==0)
					objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",objOldVehicleInfo.APP_VEHICLE_CLASS);
				//if(objOldVehicleInfo.CYCL_REGD_ROAD_USE==0 || objOldVehicleInfo.CYCL_REGD_ROAD_USE==-1)
				//Praveen(12-10-2008): For option NO, value "0" is saved.
				if(objOldVehicleInfo.CYCL_REGD_ROAD_USE==-1)
					objDataWrapper.AddParameter("@CYCL_REGD_ROAD_USE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CYCL_REGD_ROAD_USE",objOldVehicleInfo.CYCL_REGD_ROAD_USE);
				objDataWrapper.AddParameter("@COMPRH_ONLY",objOldVehicleInfo.COMPRH_ONLY);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int  VEHICLE_ID;
				if(TransactionLogRequired)
				{
					int stateID = ClsVehicleInformation.GetStateIdForApplication(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID);
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMotorcycleInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New motorcycle is added";					
					//objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;				
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
					objOldVehicleInfo.INSURED_VEH_NUMBER = VEHICLE_ID;
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					if(stateID != 14)
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='COMPRH_ONLY']");
					}
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					
					objDataWrapper.ClearParameteres();
					//objTransactionInfo.CUSTOM_INFO		=	";Insured Motorcycle Number = " + VEHICLE_ID + ";Make = " + objOldVehicleInfo.MAKE + ";Model = " + objOldVehicleInfo.MODEL;

					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
				}
				//Added by Manoj Rathore itrack # 5799
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
				returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetAssgndDrvrType");
				
				//***********************************
				objDataWrapper.ClearParameteres();
				
				//Modified By Shafi 15-9-2006
				//Insert/delete relevant coverages*********************		
				ClsVehicleCoverages objCov = new ClsVehicleCoverages("MOTOR");
				objCov.SaveDefaultCoveragesApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,VEHICLE_ID);

				
				//	objCov.UpdateMotorcycleCoverages(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,VEHICLE_ID,objDataWrapper);
				//End of coverages

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				//				if(VEHICLE_ID>0)
				//				{
				//					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				//					objDriverDetail.UpdateMotorVehicleClassNew(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID, objOldVehicleInfo.APP_VERSION_ID);
				//				}
				//				
					
				return VEHICLE_ID;
				
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


		

		
			
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objOldVehicleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddToUmbrella(ClsVehicleInfo objOldVehicleInfo, string Called_For)
		{
			string		strStoredProc	=	"Proc_InsertUmbrellaVehicleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",null);
				objDataWrapper.AddParameter("@BODY_TYPE",null);
				objDataWrapper.AddParameter("@GRG_ADD1",null);
				objDataWrapper.AddParameter("@GRG_ADD2",null);
				objDataWrapper.AddParameter("@GRG_CITY",null);
				objDataWrapper.AddParameter("@GRG_COUNTRY",null);
				objDataWrapper.AddParameter("@GRG_STATE",null);
				objDataWrapper.AddParameter("@GRG_ZIP",null);
				objDataWrapper.AddParameter("@REGISTERED_STATE",null);
				objDataWrapper.AddParameter("@TERRITORY",null);
				objDataWrapper.AddParameter("@CLASS",null);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);
				//objDataWrapper.AddParameter("@VEHICLE_TYPE",null);
				//objDataWrapper.AddParameter("@SAFETY_BELT",null);
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",null);					
				objDataWrapper.AddParameter("@AMOUNT",null);
				objDataWrapper.AddParameter("@SYMBOL",null);
				objDataWrapper.AddParameter("@VEHICLE_AGE",null);
				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);				
				//objDataWrapper.AddParameter("@USE_VEHICLE",null);				
				objDataWrapper.AddParameter("@CLASS_PER",null);				
				objDataWrapper.AddParameter("@CLASS_COM",null);				
				objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",null);				
				objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",null);
				objDataWrapper.AddParameter("@IS_OWN_LEASE",null);
				objDataWrapper.AddParameter("@PURCHASE_DATE",System.DBNull.Value);
				objDataWrapper.AddParameter("@IS_NEW_USED",null);
				objDataWrapper.AddParameter("@VEHICLE_USE",null);
				objDataWrapper.AddParameter("@MULTI_CAR",null);
				objDataWrapper.AddParameter("@ANNUAL_MILEAGE",null);
				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",null);
				objDataWrapper.AddParameter("@AIR_BAG",null);
				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",null);
				objDataWrapper.AddParameter("@MILES_TO_WORK",null);
				objDataWrapper.AddParameter("@IS_EXCLUDED",objOldVehicleInfo.IS_EXCLUDED);
				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);
				objDataWrapper.AddParameter("@OTHER_POLICY",objOldVehicleInfo.OTHER_POLICY);	

				//added by manoj at 20 dec 2006
				if(Called_For=="MOT")
					objDataWrapper.AddParameter("@CALLED_FOR","Motor Home" );
				else
					objDataWrapper.AddParameter("@CALLED_FOR","Auto" );
				//************//
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int  VEHICLE_ID;
				if(TransactionLogRequired)
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddUmbVehicleInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1499", ""); //"New vehicle is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;					
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
				}
				VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return VEHICLE_ID;
				
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
		//		public int AddToUmbrella(ClsVehicleInfo objOldVehicleInfo,string strCustomInfo)
		//		{
		//			string		strStoredProc	=	"Proc_InsertUmbrellaVehicleInfo";
		//			DateTime	RecordDate		=	DateTime.Now;
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//
		//			try
		//			{
		//				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
		//				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
		//				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
		//				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
		//				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
		//				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
		//				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
		//				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
		//				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
		//				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
		//				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
		//				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
		//				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
		//				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
		//				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
		//				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
		//				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);	
		//				objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);	
		//
		//				if(objOldVehicleInfo.REGN_PLATE_NUMBER!="")
		//					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
		//				else
		//					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",null);
		//				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
		//					
		//				if(objOldVehicleInfo.AMOUNT!=0)
		//				{
		//					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
		//				}
		//				//if(objOldVehicleInfo.SYMBOL!=0)
		//				//{
		//					objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
		//				//}
		//				if(objOldVehicleInfo.VEHICLE_AGE!=0)
		//				{
		//					objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
		//				}
		//				//Nov21,2005:Sumit Chhabra:Following parameter has been commented as default value of 'Y' should be used while inserting new value
		//				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);
		//				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
		//				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);
		//				objDataWrapper.AddParameter("@MODIFIED_BY",null);
		//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
		//
		//				//modified by vj on 11-10-2005 to set vehicle use to personal or commercial
		//				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);	
		//				objDataWrapper.AddParameter("@CLASS_PER",objOldVehicleInfo.CLASS_PER);	
		//				objDataWrapper.AddParameter("@CLASS_COM",objOldVehicleInfo.CLASS_COM);	
		//				objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",objOldVehicleInfo.VEHICLE_TYPE_PER);	
		//				objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",objOldVehicleInfo.VEHICLE_TYPE_COM);
		//
		//				//New parameters being added by Sumit on Oct 24, 2005
		//				objDataWrapper.AddParameter("@IS_OWN_LEASE",objOldVehicleInfo.IS_OWN_LEASE);
		//					
		//				// Commented & Added by mohit
		//				//objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);
		//				if(objOldVehicleInfo.PURCHASE_DATE!=DateTime.MinValue)
		//					objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);                
		//				else
		//					objDataWrapper.AddParameter("@PURCHASE_DATE",System.DBNull.Value);
		//
		//				// End
		//				objDataWrapper.AddParameter("@IS_NEW_USED",objOldVehicleInfo.IS_NEW_USED);
		//				objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
		//				objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);
		//				if(objOldVehicleInfo.ANNUAL_MILEAGE==-1)
		//					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
		//				else
		//					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",objOldVehicleInfo.ANNUAL_MILEAGE);
		//				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",DefaultValues.GetStringNull(objOldVehicleInfo.PASSIVE_SEAT_BELT));
		//				objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
		//				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);
		//				objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNINS_MOTOR_INJURY_COVE);
		//				objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",objOldVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE);
		//				objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNDERINS_MOTOR_INJURY_COVE);
		//
		//				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);
		//
		//				int returnResult = 0;
		//				if(TransactionLogRequired)
		//				{
		//					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\VehicleInfo.aspx.resx");
		//					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		//					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
		//					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//					objTransactionInfo.TRANS_TYPE_ID	=	1;
		//					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
		//					objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
		//					objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
		//					objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
		//					objTransactionInfo.TRANS_DESC		=	"New vehicle is added";
		//					objTransactionInfo.CHANGE_XML		=	strTranXML;
		//					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
		//					//Executing the query
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//				}
		//				else
		//				{
		//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
		//				}
		//				int  VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
		//				
		//				objDataWrapper.ClearParameteres();
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		//				return VEHICLE_ID;
		//				
		//			}
		//			catch(Exception ex)
		//			{
		//				throw(ex);
		//			}
		//			finally
		//			{
		//				if(objDataWrapper != null) objDataWrapper.Dispose();
		//			}
		//		}
		#endregion

		#region Update method
		
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldOldVehicleInfo">Model object having old information</param>
		/// <param name="objOldVehicleInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateVehicle(ClsVehicleInfo objOldVehicleInfo, DataWrapper objDataWrapper)
		{
			//string strTranXML;
			int returnResult = 0;
			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			
			string		strStoredProc	=	"Proc_UpdateVehicleInformation_Acord";
			
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			
			objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
			objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
			objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
			objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
			objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
			objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
			objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
			objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
			objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
			objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
			objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
			objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
			objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
			objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
			objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
			objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
			objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
			objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
			objDataWrapper.AddParameter("@VEHICLE_TYPE_CODE",objOldVehicleInfo.VEH_TYPE_CODE);
			objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);

			objDataWrapper.AddParameter("@AMOUNT",DefaultValues.GetDoubleNullFromNegative(objOldVehicleInfo.AMOUNT_COST_NEW));
				
			//if(objOldVehicleInfo.SYMBOL!=0)
			//{
			objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
			//}
			objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
			objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
			objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",DateTime.Now);
			objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",DefaultValues.GetStringNull(objOldVehicleInfo.PASSIVE_SEAT_BELT));
			objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
			objDataWrapper.AddParameter("@PURCHASE_DATE",DefaultValues.GetDateNull(objOldVehicleInfo.PURCHASE_DATE));
			objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
			if(objOldVehicleInfo.ANNUAL_MILEAGE==-1)
				objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
			else
				objDataWrapper.AddParameter("@ANNUAL_MILEAGE",DefaultValues.GetDoubleNullFromNegative(objOldVehicleInfo.ANNUAL_MILEAGE));
			//objDataWrapper.AddParameter("@ANNUAL_MILEAGE",DefaultValues.GetDoubleNullFromNegative(objOldVehicleInfo.ANNUAL_MILEAGE));
			objDataWrapper.AddParameter("@VEHICLE_CC",DefaultValues.GetIntNullFromNegative(objOldVehicleInfo.VEHICLE_CC));
			objDataWrapper.AddParameter("@USE_VEHICLE_CODE",objOldVehicleInfo.USE_VEHICLE_CODE);
			objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);

			returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			objDataWrapper.ClearParameteres();
				
		

			return returnResult;
			
		}

		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldOldVehicleInfo">Model object having old information</param>
		/// <param name="objOldVehicleInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsVehicleInfo objOldOldVehicleInfo,ClsVehicleInfo objOldVehicleInfo)
		{
			string strTranXML;
			int returnResult = 0;
			//string strXML;

			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdateVehicleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				if(objOldVehicleInfo.AMOUNT!=0)
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				//if(objOldVehicleInfo.SYMBOL!=0)
				//{
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				//}
				
				objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				//Nov 21,2005:Sumit Chhabra:Parameter is_active has been removed from being updated from here
				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);
				//added by anurag verma on 22/09/2005 for merging personal vehicle info screen
				objDataWrapper.AddParameter("@IS_OWN_LEASE",objOldVehicleInfo.IS_OWN_LEASE);
				
				// Added & Commented by mohit.
				//objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);
				if(objOldVehicleInfo.PURCHASE_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);                
				else
					objDataWrapper.AddParameter("@PURCHASE_DATE",System.DBNull.Value);
			
				// End
				objDataWrapper.AddParameter("@IS_NEW_USED",objOldVehicleInfo.IS_NEW_USED);
				objDataWrapper.AddParameter("@MILES_TO_WORK",objOldVehicleInfo.MILES_TO_WORK);
				objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
				objDataWrapper.AddParameter("@VEH_PERFORMANCE",objOldVehicleInfo.VEH_PERFORMANCE);
				objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);
				if(objOldVehicleInfo.ANNUAL_MILEAGE==-1)
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",objOldVehicleInfo.ANNUAL_MILEAGE);
				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",DefaultValues.GetStringNull(objOldVehicleInfo.PASSIVE_SEAT_BELT));
				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);

				objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
				
				objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",objOldVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE);				
				objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNDERINS_MOTOR_INJURY_COVE);				
				objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNINS_MOTOR_INJURY_COVE);				

				//added by vj on 10-10-2005 to add the vehicle use, class and type
				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);				
				objDataWrapper.AddParameter("@CLASS_PER",objOldVehicleInfo.CLASS_PER);				
				objDataWrapper.AddParameter("@CLASS_COM",objOldVehicleInfo.CLASS_COM);				
				objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",objOldVehicleInfo.VEHICLE_TYPE_PER);				
				objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",objOldVehicleInfo.VEHICLE_TYPE_COM);				
				

				if(TransactionRequired) 
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\VehicleInfo.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldOldVehicleInfo,objOldVehicleInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Vehicle is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

					}
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				//Add/ Delete coverages/ endorsements based on attributes of vehicle
				
				objDataWrapper.ClearParameteres();
				//				
				//				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				//				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				//				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				//
				//				objDataWrapper.ExecuteNonQuery("Proc_Update_VEHICLE_COVERAGES");
				//				
				//
				//				//Update Driver endorsements
				//				Cms.BusinessLayer.BlApplication.ClsDriverDetail objDriver = new ClsDriverDetail();
				//				objDriver.UpdateDriverEndorsements(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,objOldVehicleInfo.VEHICLE_ID,objDataWrapper);
				//End of endorsements
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,RuleType.RiskDependent,objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.ClearParameteres();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,RuleType.MiscEquipment,objOldVehicleInfo.VEHICLE_ID);

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

		public int Update(ClsVehicleInfo objOldOldVehicleInfo,ClsVehicleInfo objOldVehicleInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			//string strXML;

			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdateVehicleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
				objDataWrapper.AddParameter("@BUSS_PERM_RESI",objOldVehicleInfo.BUSS_PERM_RESI);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				objDataWrapper.AddParameter("@SNOWPLOW_CONDS",objOldVehicleInfo.SNOWPLOW_CONDS);
				objDataWrapper.AddParameter("@CAR_POOL",objOldVehicleInfo.CAR_POOL);
				//objDataWrapper.AddParameter("@SAFETY_BELT",objOldVehicleInfo.SAFETY_BELT);
				objDataWrapper.AddParameter("@AUTO_POL_NO",objOldVehicleInfo.AUTO_POL_NO);
				//objDataWrapper.AddParameter("@RADIUS_OF_USE",objOldVehicleInfo.RADIUS_OF_USE);
				if(objOldVehicleInfo.RADIUS_OF_USE==0)
					objDataWrapper.AddParameter("@RADIUS_OF_USE",null);
				else
					objDataWrapper.AddParameter("@RADIUS_OF_USE",objOldVehicleInfo.RADIUS_OF_USE);
				objDataWrapper.AddParameter("@TRANSPORT_CHEMICAL",objOldVehicleInfo.TRANSPORT_CHEMICAL);
				objDataWrapper.AddParameter("@COVERED_BY_WC_INSU",objOldVehicleInfo.COVERED_BY_WC_INSU);
				objDataWrapper.AddParameter("@CLASS_DESCRIPTION",objOldVehicleInfo.CLASS_DESCRIPTION);

							
				if(objOldVehicleInfo.AMOUNT!=0)
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				//if(objOldVehicleInfo.SYMBOL!=0)
				//{
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				//}
				
				objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				//Nov 21,2005:Sumit Chhabra:Parameter is_active has been removed from being updated from here
				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);
				//added by anurag verma on 22/09/2005 for merging personal vehicle info screen
				objDataWrapper.AddParameter("@IS_OWN_LEASE",objOldVehicleInfo.IS_OWN_LEASE);
				
				// Added & Commented by mohit.
				//objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);
				if(objOldVehicleInfo.PURCHASE_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);                
				else
					objDataWrapper.AddParameter("@PURCHASE_DATE",System.DBNull.Value);
			
				// End
				objDataWrapper.AddParameter("@IS_NEW_USED",objOldVehicleInfo.IS_NEW_USED);
				objDataWrapper.AddParameter("@MILES_TO_WORK",objOldVehicleInfo.MILES_TO_WORK);
				objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
				objDataWrapper.AddParameter("@VEH_PERFORMANCE",objOldVehicleInfo.VEH_PERFORMANCE);
				objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);
				if(objOldVehicleInfo.ANNUAL_MILEAGE==0 || objOldVehicleInfo.ANNUAL_MILEAGE==0.0 ||objOldVehicleInfo.ANNUAL_MILEAGE < 0)
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",objOldVehicleInfo.ANNUAL_MILEAGE);

				//objDataWrapper.AddParameter("@ANNUAL_MILEAGE",DefaultValues.GetDoubleNullFromNegative(objOldVehicleInfo.ANNUAL_MILEAGE));
				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",DefaultValues.GetStringNull(objOldVehicleInfo.PASSIVE_SEAT_BELT));
				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);

				objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
				
				//objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",objOldVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE);				
				//objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNDERINS_MOTOR_INJURY_COVE);				
				//objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNINS_MOTOR_INJURY_COVE);				

				//added by vj on 10-10-2005 to add the vehicle use, class and type
				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);				
				/*objDataWrapper.AddParameter("@CLASS_PER",objOldVehicleInfo.CLASS_PER);				
				objDataWrapper.AddParameter("@CLASS_COM",objOldVehicleInfo.CLASS_COM);				
				objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",objOldVehicleInfo.VEHICLE_TYPE_PER);				
				objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",objOldVehicleInfo.VEHICLE_TYPE_COM);*/				

				if (objOldVehicleInfo.CLASS_PER == 0)
				{
					objDataWrapper.AddParameter("@CLASS_PER",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@CLASS_PER", objOldVehicleInfo.CLASS_PER);	
				}
				

				if (objOldVehicleInfo.CLASS_COM == 0)
				{
					objDataWrapper.AddParameter("@CLASS_COM",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@CLASS_COM",objOldVehicleInfo.CLASS_COM);	
				}
				

				if (objOldVehicleInfo.VEHICLE_TYPE_PER == 0)
				{
					objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",objOldVehicleInfo.VEHICLE_TYPE_PER);	
				}
				if (objOldVehicleInfo.IS_SUSPENDED == 0)
				{
					objDataWrapper.AddParameter("@IS_SUSPENDED",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@IS_SUSPENDED",objOldVehicleInfo.IS_SUSPENDED);	
				}

				if (objOldVehicleInfo.VEHICLE_TYPE_COM == 0)
				{
					objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",System.DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",objOldVehicleInfo.VEHICLE_TYPE_COM);	
				}
				


				if(TransactionRequired) 
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\VehicleInfo.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldOldVehicleInfo,objOldVehicleInfo);
					if(objOldVehicleInfo.USE_VEHICLE==11332)
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_COM']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='CLASS_COM']");											
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='CLASS_PER' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_PER' and @NewValue='0']","NewValue","null");
					}
					else //Vehicle is for commercial use, remove nodes for personal user
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='MULTI_CAR']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_USE']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='BUSS_PERM_RESI']");

						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_PER']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='CLASS_PER']");					
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='CLASS_COM' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_COM' and @NewValue='0']","NewValue","null");
					}
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Vehicle is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				//Add/ Delete coverages/ endorsements based on attributes of vehicle
				
				objDataWrapper.ClearParameteres();
				// IF VEHICLE CHANGE FROM SUSPENDED YES TO NO iTRACK 5755 -bY pRAVESH K CHANDEL ON 27 APRIL 2009
				if ( ((objOldOldVehicleInfo.IS_SUSPENDED!=objOldVehicleInfo.IS_SUSPENDED) && objOldVehicleInfo.IS_SUSPENDED==10964)
					||objOldOldVehicleInfo.USE_VEHICLE!=objOldVehicleInfo.USE_VEHICLE)//Added by Charles on 10-Jul-09 for Itrack 6082
				{
					objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
					objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
					
					//objDataWrapper.ExecuteNonQuery("Proc_Update_VEHICLE_COVERAGES");
					objDataWrapper.ExecuteNonQuery("Proc_UPDATE_POLICY_LEVEL_COVERAGES_APP");
					
					objDataWrapper.ClearParameteres();
				}
				//				
				//				//Update Driver endorsements
				//				Cms.BusinessLayer.BlApplication.ClsDriverDetail objDriver = new ClsDriverDetail();
				//				objDriver.UpdateDriverEndorsements(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,objOldVehicleInfo.VEHICLE_ID,objDataWrapper);
				//				//End of endorsements
				
				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,RuleType.RiskDependent,objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.ClearParameteres();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,RuleType.AutoDriverDep,objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.ClearParameteres();
				objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,RuleType.MiscEquipment,objOldVehicleInfo.VEHICLE_ID);
				
				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				objDriverDetail.UpdateVehicleClassNew(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID, objOldVehicleInfo.APP_VERSION_ID,objDataWrapper);
				
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

		
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldOldVehicleInfo">Model object having old information</param>
		/// <param name="objOldVehicleInfo">Model object having new information(form control's value)</param>
		/// 		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateMotorcycle(ClsVehicleInfo objOldOldVehicleInfo,ClsVehicleInfo objOldVehicleInfo)
		{
			string strTranXML;
			int returnResult = 0;
			
			/*
			ClsAutoIDInfo	objOldAutoIdInfo = new ClsAutoIDInfo();
			ClsAutoIDInfo	objNewAutoIdInfo = new ClsAutoIDInfo();
			PopulateAutoIDInfo(objNewAutoIdInfo,objOldVehicleInfo);
			objNewAutoIdInfo.VEHICLE_ID		=	objOldVehicleInfo.VEHICLE_ID;
			string strXML;
			ClsAutoIDInformation objAutoIDInformation = new ClsAutoIDInformation();
			DataSet dsOldAutoIDInfo	= objAutoIDInformation.FillAdditionalInterestDetails(objOldVehicleInfo.CUSTOMER_ID,
				objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,objOldVehicleInfo.VEHICLE_ID,out strXML);
			//populate old autoid info from dataset
			populateAutoIDInfoDataSet(objOldAutoIdInfo,dsOldAutoIDInfo);
			objOldAutoIdInfo.MODIFIED_BY = objOldVehicleInfo.MODIFIED_BY;
			objOldAutoIdInfo.LAST_UPDATED_DATETIME = objOldOldVehicleInfo.LAST_UPDATED_DATETIME;
			objOldAutoIdInfo.IS_ACTIVE			=	"Y";
			*/	

			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdateMotorcycleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				if (objOldVehicleInfo.AMOUNT==-1)
				{
					objDataWrapper.AddParameter("@AMOUNT",System.DBNull.Value);					
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}	
				objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				objDataWrapper.AddParameter("@VEHICLE_CC",objOldVehicleInfo.VEHICLE_CC);
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);
				//Nov 30,2005:Sumit Chhabra:Commented as value of is_active will not be altered with now
				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);

				//added by vj on 18-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",objOldVehicleInfo.APP_VEHICLE_CLASS);

				if(TransactionRequired) 
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMotorcycleInformation.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldOldVehicleInfo,objOldVehicleInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Motorcycle is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				/*
				//updating records in AutoID
				if(!ClsAutoIDInformation.isUpdated(objNewAutoIdInfo.CUSTOMER_ID,objNewAutoIdInfo.APP_ID,objNewAutoIdInfo.APP_VERSION_ID,objNewAutoIdInfo.VEHICLE_ID))
				{
					objDataWrapper.ClearParameteres();
					if(dsOldAutoIDInfo.Tables[0].Rows.Count > 0)
						returnResult = objAutoIDInformation.Update(objOldAutoIdInfo,objNewAutoIdInfo,objDataWrapper);
					else
						returnResult = objAutoIDInformation.Add(objNewAutoIdInfo,objDataWrapper);
				}
				*/

				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

		public int UpdateMotorcycle(ClsVehicleInfo objOldOldVehicleInfo,ClsVehicleInfo objOldVehicleInfo, string strCustomInfo)
		{
			string strTranXML;
			int returnResult = 0;
			
			

			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdateMotorcycleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
			 
				if (objOldVehicleInfo.AMOUNT==-1)
				{
					objDataWrapper.AddParameter("@AMOUNT",System.DBNull.Value);					
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}	
				objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				objDataWrapper.AddParameter("@VEHICLE_CC",objOldVehicleInfo.VEHICLE_CC);
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);
				//Nov 30,2005:Sumit Chhabra:Commented as value of is_active will not be altered with now
				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				objDataWrapper.AddParameter("@COMPRH_ONLY",objOldVehicleInfo.COMPRH_ONLY);

				//added by vj on 18-10-2005
				//objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",objOldVehicleInfo.APP_VEHICLE_CLASS);
				if(objOldVehicleInfo.APP_VEHICLE_CLASS==0)
					objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",objOldVehicleInfo.APP_VEHICLE_CLASS);
				//if(objOldVehicleInfo.CYCL_REGD_ROAD_USE==0 || objOldVehicleInfo.CYCL_REGD_ROAD_USE==-1)
				//Praveen(12-10-2008): For option NO, value "0" is saved.
				if(objOldVehicleInfo.CYCL_REGD_ROAD_USE==-1)
					objDataWrapper.AddParameter("@CYCL_REGD_ROAD_USE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CYCL_REGD_ROAD_USE",objOldVehicleInfo.CYCL_REGD_ROAD_USE);
	

				if(TransactionRequired) 
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddMotorcycleInformation.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldOldVehicleInfo,objOldVehicleInfo,out strTranXML);					
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Motorcycle is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.ClearParameteres();
				// IF VEHICLE CHANGE FROM comp YES TO NO -BY pRAVESH K CHANDEL 
				if ((objOldOldVehicleInfo.COMPRH_ONLY!=objOldVehicleInfo.COMPRH_ONLY) && objOldVehicleInfo.COMPRH_ONLY==10964)
				{
					objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
					objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
					objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
					
					objDataWrapper.ExecuteNonQuery("Proc_UPDATE_POLICY_LEVEL_COVERAGES_APP");
					objDataWrapper.ClearParameteres();
				}
				//Modified By Shafi 15-9-2006
				//Update coverages/////////////			
				ClsVehicleCoverages objCov = new ClsVehicleCoverages("MOTOR");
				objCov.UpdateCoveragesByRuleApp(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,RuleType.RiskDependent,objOldVehicleInfo.VEHICLE_ID);

				//objCov.UpdateMotorcycleCoverages(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,objOldVehicleInfo.VEHICLE_ID,objDataWrapper);
				//Update coverages//////////////

				if(returnResult > 0)
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					objDriverDetail.UpdateMotorVehicleClassNew(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.APP_ID, objOldVehicleInfo.APP_VERSION_ID);				
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

		
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldOldVehicleInfo">Model object having old information</param>
		/// <param name="objOldVehicleInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateCustomerVehicle(ClsVehicleInfo objOldOldVehicleInfo,ClsVehicleInfo objOldVehicleInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			
			string		strStoredProc	=	"Proc_UpdateVehicleInformationOfCustomer";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
				
				if(objOldVehicleInfo.AMOUNT!=0)
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				//if(objOldVehicleInfo.SYMBOL!=0)
				//{
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				//}
				objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);
				if(TransactionRequired) 
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\VehicleInfo.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldOldVehicleInfo,objOldVehicleInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Vehicle is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
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
		

		

			
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldOldVehicleInfo">Model object having old information</param>
		/// <param name="objOldVehicleInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		//		public int UpdateUmbrellaVehicleInfo(ClsVehicleInfo objOldOldVehicleInfo,ClsVehicleInfo objOldVehicleInfo)
		//		{
		//			string strTranXML;
		//			int returnResult = 0;
		//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
		//			string		strStoredProc	=	"Proc_UpdateUmbrellaVehicleInfo";
		//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
		//			try 
		//			{
		//				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
		//				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
		//				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
		//				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
		//				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
		//				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
		//				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
		//				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
		//				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
		//				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
		//				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
		//				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
		//				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
		//				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
		//				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
		//				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
		//				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
		//				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
		//				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
		//				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
		//				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
		//				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
		//				
		//				if (objOldVehicleInfo.AMOUNT!=0)
		//				{
		//					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
		//				}
		//				//if (objOldVehicleInfo.SYMBOL!=0)
		//				//{
		//					objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
		//				//}
		//				objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
		//				//Nov 21,2005:Sumit Chhabra:Value of is_active should not be changed while updating
		//				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
		//				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
		//				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);
		//
		//				//added by vj on 11-10-2005 to add the vehicle use, class and type
		//				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);				
		//				objDataWrapper.AddParameter("@CLASS_PER",objOldVehicleInfo.CLASS_PER);				
		//				objDataWrapper.AddParameter("@CLASS_COM",objOldVehicleInfo.CLASS_COM);				
		//				objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",objOldVehicleInfo.VEHICLE_TYPE_PER);				
		//				objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",objOldVehicleInfo.VEHICLE_TYPE_COM);
		//				//New parameters being added by Sumit on Oct 24, 2005
		//				objDataWrapper.AddParameter("@IS_OWN_LEASE",objOldVehicleInfo.IS_OWN_LEASE);
		//
		//				// Added & Commented by mohit.
		//				//objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);
		//				if(objOldVehicleInfo.PURCHASE_DATE!=DateTime.MinValue)
		//					objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);                
		//				else
		//					objDataWrapper.AddParameter("@PURCHASE_DATE",System.DBNull.Value);
		//			
		//				// End
		//					
		//					
		//				objDataWrapper.AddParameter("@IS_NEW_USED",objOldVehicleInfo.IS_NEW_USED);
		//				objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
		//				objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);
		//				if(objOldVehicleInfo.ANNUAL_MILEAGE==-1)
		//					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
		//				else
		//					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",objOldVehicleInfo.ANNUAL_MILEAGE);
		//				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",DefaultValues.GetStringNull(objOldVehicleInfo.PASSIVE_SEAT_BELT));
		//				objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
		//				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);
		//				objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNINS_MOTOR_INJURY_COVE);
		//				objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",objOldVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE);
		//				objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNDERINS_MOTOR_INJURY_COVE);
		//				objDataWrapper.AddParameter("@MILES_TO_WORK",objOldVehicleInfo.MILES_TO_WORK);
		//					
		//
		//				if(TransactionRequired) 
		//				{
		//					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\VehicleInfo.aspx.resx");
		//					strTranXML = objBuilder.GetTransactionLogXML(objOldOldVehicleInfo,objOldVehicleInfo);
		//					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
		//						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
		//					else				
		//					{	
		//						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
		//						objTransactionInfo.TRANS_TYPE_ID	=	3;
		//						objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
		//						objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
		//						objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
		//						objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
		//						objTransactionInfo.TRANS_DESC		=	"Vehicle is modified";
		//						objTransactionInfo.CHANGE_XML		=	strTranXML;
		//						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
		//					}
		//
		//				}
		//				else
		//				{
		//					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
		//				}
		//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
		//				return returnResult;
		//			}
		//			catch(Exception ex)
		//			{
		//				throw(ex);
		//			}
		//			finally
		//			{
		//				if(objDataWrapper != null) 
		//				{
		//					objDataWrapper.Dispose();
		//				}
		//				if(objBuilder != null) 
		//				{
		//					objBuilder = null;
		//				}
		//			}
		//		}

		public int UpdateUmbrellaVehicleInfo(ClsVehicleInfo objOldOldVehicleInfo,ClsVehicleInfo objOldVehicleInfo, string strCustomInfo,string Called_For)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdateUmbrellaVehicleInfo";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objOldVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objOldVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);				
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",null);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",null);
				objDataWrapper.AddParameter("@BODY_TYPE",null);
				objDataWrapper.AddParameter("@GRG_ADD1",null);
				objDataWrapper.AddParameter("@GRG_ADD2",null);
				objDataWrapper.AddParameter("@GRG_CITY",null);
				objDataWrapper.AddParameter("@GRG_COUNTRY",null);
				objDataWrapper.AddParameter("@GRG_STATE",null);
				objDataWrapper.AddParameter("@GRG_ZIP",null);
				objDataWrapper.AddParameter("@REGISTERED_STATE",null);
				objDataWrapper.AddParameter("@TERRITORY",null);
				objDataWrapper.AddParameter("@CLASS",null);
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",null);
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);
				//objDataWrapper.AddParameter("@SAFETY_BELT",null);
				objDataWrapper.AddParameter("@AMOUNT",null);
				objDataWrapper.AddParameter("@SYMBOL",null);
				objDataWrapper.AddParameter("@VEHICLE_AGE",null);
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);
				//				objDataWrapper.AddParameter("@USE_VEHICLE",null);
				objDataWrapper.AddParameter("@CLASS_PER",null);
				objDataWrapper.AddParameter("@CLASS_COM",null);
				objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",null);
				objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",null);
				objDataWrapper.AddParameter("@IS_OWN_LEASE",null);
				objDataWrapper.AddParameter("@PURCHASE_DATE",null);
				objDataWrapper.AddParameter("@IS_NEW_USED",null);
				objDataWrapper.AddParameter("@VEHICLE_USE",null);
				objDataWrapper.AddParameter("@MULTI_CAR",null);
				objDataWrapper.AddParameter("@ANNUAL_MILEAGE",null);
				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",null);
				objDataWrapper.AddParameter("@AIR_BAG",null);
				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",null);				
				objDataWrapper.AddParameter("@MILES_TO_WORK",null);	
				objDataWrapper.AddParameter("@IS_EXCLUDED",objOldVehicleInfo.IS_EXCLUDED);
				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);
				objDataWrapper.AddParameter("@OTHER_POLICY",objOldVehicleInfo.OTHER_POLICY);	


				//Added by Manoj at 21 dec 2006 
				if(Called_For == "MOT")
					objDataWrapper.AddParameter("@CALLED_FOR","Motor Home");
				else
					objDataWrapper.AddParameter("@CALLED_FOR","Auto");

				//********

				if(TransactionRequired) 
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\AddUmbVehicleInfo.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldOldVehicleInfo,objOldVehicleInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objOldVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objOldVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Vehicle is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
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

		#region FillAppVehicleInfo
		public static void FillAppVehicleInfo(System.Web.UI.WebControls.DropDownList cmbCombo,int intCustomrId, int intAppId, int intAppVersionId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);

				DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillAppVehicleDropDown").Tables[0];
				cmbCombo.DataSource = objDataTable;
				cmbCombo.DataTextField = "MODEL_MAKE";
				cmbCombo.DataValueField = "VEHICLE_ID";
				cmbCombo.DataBind();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		public static string FillAppVehicle(int intCustomrId, int intAppId,int intAppVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				return objDataWrapper.ExecuteDataSet("Proc_FillAppVehicleDropDown").GetXml();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		public static DataTable GetAppVehicle(int intCustomrId, int intAppId,int intAppVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet dsTemp = new DataSet();	
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_FillAppVehicleDropDown");
				return dsTemp.Tables[0];
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		public static string FillPolVehicle(int intCustomrId, int intPolId,int intPolVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POLICY_ID", intPolId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				return objDataWrapper.ExecuteDataSet("Proc_FillPolVehicleDropDown").GetXml();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		public static DataTable GetPolVehicle(int intCustomrId, int intPolId,int intPolVersionId, int intDriverId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet dsTemp = new DataSet();	
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POLICY_ID", intPolId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_FillPolVehicleDropDown");
				return dsTemp.Tables[0];
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}
		#endregion

		#region Fill Umbrella Assign Section Dropdowns
		public static void FillUmbrellaAppVehicleInfo(System.Web.UI.WebControls.DropDownList cmbCombo,int intCustomrId, int intAppId, int intAppVersionId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);

				DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillUmbrellaAppVehicleInfo").Tables[0];
				cmbCombo.DataSource = objDataTable;
				cmbCombo.DataTextField = "MODEL_MAKE";
				cmbCombo.DataValueField = "VEHICLE_ID";
				cmbCombo.DataBind();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}
		public static DataSet FetchVehiclesForUmb(int intCustomrId,int intAppId,int intAppVersionId)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);				
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_FillUmbrellaAppVehicleInfo");
				if(dsTemp!=null)
					return dsTemp;
				else
					return null;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}		

		#endregion
		
		#region Populate AutoID
		private void PopulateAutoID(ClsAutoIDInfo objAutoIDInfo, ClsVehicleInfo objVehicleInfo)
		{
			try
			{
				objAutoIDInfo.CUSTOMER_ID		=	objVehicleInfo.CUSTOMER_ID;
				objAutoIDInfo.APP_ID			=	objVehicleInfo.APP_ID;
				objAutoIDInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
				objAutoIDInfo.ADDRESS1			=	objVehicleInfo.GRG_ADD1;
				objAutoIDInfo.ADDRESS2			=	objVehicleInfo.GRG_ADD2;
				objAutoIDInfo.CITY				=	objVehicleInfo.GRG_CITY;
				objAutoIDInfo.STATE				=	objVehicleInfo.GRG_STATE;
				objAutoIDInfo.ZIP				=	objVehicleInfo.GRG_ZIP;
				
				// Fetching application effective date and expiration date
				/*
				DataSet		dsApplication		=	ClsGeneralInformation.FetchApplication(objVehicleInfo.CUSTOMER_ID,
					objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID);
			
				if(dsApplication.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"] != DBNull.Value)
				{
					objAutoIDInfo.ID_EFFECTIVE_DATE		=	Convert.ToDateTime(dsApplication.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]);
				}
				if(dsApplication.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != DBNull.Value)
				{
					objAutoIDInfo.ID_EXPITATION_DATE	=	Convert.ToDateTime(dsApplication.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]);
				}*/

				objAutoIDInfo.NAME_TYPE			=	"1";		// first option of autoid info screen
				objAutoIDInfo.NAME_ID			=	1;	// first option's text of autoid info screen
				objAutoIDInfo.IS_ACTIVE			=	"Y";
				objAutoIDInfo.CREATED_BY		=	objVehicleInfo.CREATED_BY;
				objAutoIDInfo.CREATED_DATETIME	=	DateTime.Now;
				objAutoIDInfo.MODIFIED_BY		=	objVehicleInfo.MODIFIED_BY;
				objAutoIDInfo.LAST_UPDATED_DATETIME = DateTime.Now;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void PopulateAutoIDInfo(ClsAutoIDInfo objAutoIDInfo, ClsVehicleInfo objVehicleInfo)
		{
			try
			{
				objAutoIDInfo.CUSTOMER_ID		=	objVehicleInfo.CUSTOMER_ID;
				objAutoIDInfo.APP_ID			=	objVehicleInfo.APP_ID;
				objAutoIDInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
				objAutoIDInfo.ADDRESS1			=	objVehicleInfo.GRG_ADD1;
				objAutoIDInfo.ADDRESS2			=	objVehicleInfo.GRG_ADD2;
				objAutoIDInfo.CITY				=	objVehicleInfo.GRG_CITY;
				objAutoIDInfo.STATE				=	objVehicleInfo.GRG_STATE;
				objAutoIDInfo.ZIP				=	objVehicleInfo.GRG_ZIP;
				// Fetching application effective date and expiration date
				
				DataSet		dsApplication		=	ClsGeneralInformation.FetchApplication(objVehicleInfo.CUSTOMER_ID,
					objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID);
			
				if(dsApplication.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"] != DBNull.Value)
				{
					objAutoIDInfo.ID_EFFECTIVE_DATE		=	Convert.ToDateTime(dsApplication.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]);
				}
				if(dsApplication.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != DBNull.Value)
				{
					objAutoIDInfo.ID_EXPITATION_DATE	=	Convert.ToDateTime(dsApplication.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]);
				}
				
				objAutoIDInfo.NAME_TYPE			=	"1";		// first option of autoid info screen
				objAutoIDInfo.NAME_ID			=	1;	// first option's text of autoid info screen
				objAutoIDInfo.IS_ACTIVE			=	"Y";
				objAutoIDInfo.CREATED_BY		=	objVehicleInfo.CREATED_BY;
				objAutoIDInfo.CREATED_DATETIME	=	DateTime.Now;
				objAutoIDInfo.MODIFIED_BY		=	objVehicleInfo.MODIFIED_BY;
				objAutoIDInfo.LAST_UPDATED_DATETIME = DateTime.Now;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void populateAutoIDInfoDataSet(ClsAutoIDInfo objAutoIDInfo, DataSet oldData)
		{
			DataTable dtOldData			=	oldData.Tables[0];
			if(dtOldData.Rows.Count > 0)
			{
				objAutoIDInfo.CUSTOMER_ID	=	int.Parse(dtOldData.Rows[0]["CUSTOMER_ID"].ToString());
				objAutoIDInfo.APP_ID		=	int.Parse(dtOldData.Rows[0]["APP_ID"].ToString());
				objAutoIDInfo.APP_VERSION_ID	=	int.Parse(dtOldData.Rows[0]["APP_VERSION_ID"].ToString());
				objAutoIDInfo.VEHICLE_ID	=	int.Parse(dtOldData.Rows[0]["VEHICLE_ID"].ToString());
				objAutoIDInfo.AUTO_CARD_ID	=	int.Parse(dtOldData.Rows[0]["AUTO_CARD_ID"].ToString());
				if(dtOldData.Rows[0]["ID_EFFECTIVE_DATE"] != null)
					objAutoIDInfo.ID_EFFECTIVE_DATE	=	Convert.ToDateTime(dtOldData.Rows[0]["ID_EFFECTIVE_DATE"]);
				if(dtOldData.Rows[0]["ID_EXPITATION_DATE"] != null)
					objAutoIDInfo.ID_EXPITATION_DATE	=	Convert.ToDateTime(dtOldData.Rows[0]["ID_EXPITATION_DATE"]);
				if(dtOldData.Rows[0]["ADDRESS1"] != null)
					objAutoIDInfo.ADDRESS1	=	dtOldData.Rows[0]["ADDRESS1"].ToString();
				if(dtOldData.Rows[0]["ADDRESS2"] != null)
					objAutoIDInfo.ADDRESS2	=	dtOldData.Rows[0]["ADDRESS2"].ToString();
				if(dtOldData.Rows[0]["CITY"] != null)
					objAutoIDInfo.CITY	=	dtOldData.Rows[0]["CITY"].ToString();
				if(dtOldData.Rows[0]["STATE"] != null)
					objAutoIDInfo.STATE	=	dtOldData.Rows[0]["STATE"].ToString();
				if(dtOldData.Rows[0]["ZIP"] != null)
					objAutoIDInfo.ZIP	=	dtOldData.Rows[0]["ZIP"].ToString();
				if(dtOldData.Rows[0]["NAME_TYPE"] != null)
					objAutoIDInfo.NAME_TYPE	=	dtOldData.Rows[0]["NAME_TYPE"].ToString();
				if(dtOldData.Rows[0]["NAME_ID"] != null)
					objAutoIDInfo.NAME_ID	=	int.Parse(dtOldData.Rows[0]["NAME_ID"].ToString());
			}
		}

		#endregion


		/// <summary>
		/// Method to retrive data from the Clt_Customer_Vehicle table
		/// for the customer_id passed as the parameter.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public static DataTable FetchVehicleFromCustVehicleTable(int CustomerID,int AppID,int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustVehicleInfoForCoverage");
				return dsTemp.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}


		
		/// <summary>
		/// Method to retrive data from the umbrella_vehicle table
		/// for the customer_id passed as the parameter.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public static DataTable FetchUmbrellaVehicle(int CustomerID,int AppID,int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbrellaVehiclesForCoverage");
				return dsTemp.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}
			
		
		/// <summary>
		///	 For coping data in App_Vehicles table from Clt_Customer_Vehicles and in
		///	 app_vehicle_coverages from clt_customer_vehicle_coverages.	 
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public static void InsertVehicleFromCustVehicleToUmbrellaVehicle(DataTable dtSelectedVehicles,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id,char coverage_To_Be_Copy)
		{
			string	strStoredProc = "Proc_InsertCustomerVehicleToUmbrellaVehicle";
			try
			{				
				for(int i=0;i < dtSelectedVehicles.Rows.Count;i++)
				{
					DataRow dr=dtSelectedVehicles.Rows[i];					
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_VEHICLE_ID",int.Parse(dr["VehicleID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",from_App_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",from_App_Version_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@COVERAGE_TO_BE_COPY",coverage_To_Be_Copy,SqlDbType.Char);	
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				}							
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}


		//called in else
		public void InsertVehicleFromCustVehicleTable(ClsVehicleInfo objVehicleInfo, DataTable dtSelectedVehicles, string strCalledFrom)
		{
			string	strStoredProc = "Proc_InsertCustomerVehicleToAppVehicle";						
			string VehicleInfo="";			
			string strTransDesc="";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{				
				for(int i=0;i < dtSelectedVehicles.Rows.Count;i++)
				{
					DataRow dr=dtSelectedVehicles.Rows[i];						
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@FROM_VEHICLE_ID",int.Parse(dr["VehicleID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",objVehicleInfo.APP_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",objVehicleInfo.CREATED_BY,SqlDbType.Int);	
					objDataWrapper.AddParameter("@COVERAGE_TO_BE_COPY",null);
					//Output variable
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@NEW_VEH_ID",SqlDbType.Int,ParameterDirection.Output);

					/*if(strCalledFrom=="PPA")
					{
						VehicleInfo+=";Insured Vehicle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Vehicle is copied";
					}
					else
					{
						VehicleInfo+=";Motorcycle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Motorcycle is copied";
					}*/

					int returnResult = 0;
					if(TransactionLogRequired && i==(dtSelectedVehicles.Rows.Count-1))
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						objDataWrapper.ClearParameteres();
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
							objDataWrapper.ClearParameteres();
							objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
							objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
							objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
							objDataWrapper.AddParameter("@VEHICLE_ID",objSqlParameter.Value.ToString());
							returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetAssgndDrvrType");
							objDataWrapper.ClearParameteres();
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}

						
						objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/CustomerVehicle.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objVehicleInfo.CREATED_BY;
						//objTransactionInfo.TRANS_DESC		=	"Motorcycle is copied";
						objTransactionInfo.TRANS_DESC		=	strTransDesc;
						objTransactionInfo.CUSTOM_INFO		=	VehicleInfo;
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
						objDataWrapper.ClearParameteres();
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
							objDataWrapper.ClearParameteres();
							objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
							objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
							objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
							objDataWrapper.AddParameter("@VEHICLE_ID",objSqlParameter.Value.ToString());
							returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetAssgndDrvrType");
							objDataWrapper.ClearParameteres();
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}
					}									
					objDataWrapper.ClearParameteres();

					//Update Default Coverages//////////////////////
			
					//Insert/delete relevant coverages for Auto*********************	
					if ( strCalledFrom == "PPA" )
					{
						//Modified By Shafi 11-10-2006
						int vehicleID = Convert.ToInt32(objSqlParameter.Value);
						//						objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
						//						objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
						//						objDataWrapper.AddParameter("@VEHICLE_ID",vehicleID);
						//						objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
						//
						//						objDataWrapper.ExecuteNonQuery("Proc_Update_VEHICLE_COVERAGES");
						//						objDataWrapper.ClearParameteres();
						//
						//						//Ravindra (05-23-06) To update Endorsment's linked with Policy Level coverages
						//						Cms.BusinessLayer.BlApplication.ClsVehicleCoverages.UpdateApplicationEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,vehicleID,objDataWrapper);
						//						////////////////////
						//						//***********************
						//
						//						//Update Driver endorsements
						//						Cms.BusinessLayer.BlApplication.ClsDriverDetail objDriver = new ClsDriverDetail();
						//
						//						objDriver.UpdateDriverEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,vehicleID,objDataWrapper);
						ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
						objCoverage.SaveDefaultCoveragesApp(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,vehicleID);
						objDataWrapper.ClearParameteres();
						objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,RuleType.AutoDriverDep,vehicleID );
						objDataWrapper.ClearParameteres();
					}
					else if ( strCalledFrom == "MOT" )
					{
						//Modified By Shafi 10-10-2006 Call Common Function for inserting Defalut Coverage
						//						objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
						//						objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
						//						objDataWrapper.AddParameter("@VEHICLE_ID",Convert.ToInt32(objSqlParameter.Value));
						//						objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
						//
						//						objDataWrapper.ExecuteNonQuery("Proc_Update_MOTORCYCLE_COVERAGES");
						
						//Insert/delete relevant coverages*********************		
						ClsVehicleCoverages objCov = new ClsVehicleCoverages("MOTOR");
						ClsDriverDetail objDriverDetail = new ClsDriverDetail();
						//objDriverDetail.UpdateMotorVehicleClassNew(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID);
						
						objCov.SaveDefaultCoveragesApp(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();
					}

					//***********************	
					////////////////////////////////////////////////////
					
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					objDataWrapper.ClearParameteres();
									
				}	
					
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

			/*string	strStoredProc = "Proc_InsertCustomerVehicleToAppVehicle";
			try
			{					
				for(int i=0;i < dtSelectedVehicles.Rows.Count;i++)
				{
					DataRow dr=dtSelectedVehicles.Rows[i];	
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_VEHICLE_ID",int.Parse(dr["VehicleID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",from_App_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",from_App_Version_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);	
					objDataWrapper.AddParameter("@COVERAGE_TO_BE_COPY",null);	
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				}							
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}*/
		}

		public void ActivateDeactivateAutoVehicleForUmbrella(ClsVehicleInfo objVehicleInfo, string strStatus,string strCustomInfo)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAutoVehicleForUmbrella";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/VehicleInfo.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Vehicle is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Vehicle is Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				if(returnResult>0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,clsapplication.CALLED_FROM_APPLICATION,ClsUmbSchRecords.CALLED_FROM_VEHICLES);
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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
		

		public int GetDriverCountForAssignedVehicle(ClsVehicleInfo objVehicleInfo, string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_GetDriverCountForAssignedVehicle";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
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
		

		#region Activate/Deactivate
		//Following procedure will be used for automobile/ppa
		public void ActivateDeactivateAutoMotorVehicle(ClsVehicleInfo objVehicleInfo, string strStatus,string strCustomInfo)
		{
			#region H I D E - P O R T I O N (Hide by Swarup on 14-Mar-2007)
			/*string		strStoredProc	=	"Proc_ActivateDeactivateAutoVehicle";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/VehicleInfo.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Vehicle is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Vehicle is Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);		
				if(returnResult>0)
				{
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					objDriverDetail.UpdateVehicleClassNew(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID);
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
			*/
			#endregion
			ActivateDeactivateAutoMotorVehicle(objVehicleInfo,strStatus,strCustomInfo,"");
		}


		//Following procedure will be used for motorcycle
		public void ActivateDeactivateAutoMotorVehicle(ClsVehicleInfo objVehicleInfo, string strStatus,string strCustomInfo,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAutoVehicle";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string strVehicleName="Vehicle";
			if(strCalledFrom==CALLED_FROM_MOTORCYCLE)
				strVehicleName="Motorcycle ";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom.ToUpper());
				

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/VehicleInfo.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	strVehicleName + " is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	strVehicleName+ " is Deactivated";
					
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				if(strCalledFrom!=CALLED_FROM_MOTORCYCLE)
				{
					ClsDriverDetail objDriverDetails = new ClsDriverDetail();
					objDriverDetails.UpdateVehicleClassNew(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,objDataWrapper);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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

		//Following method will be called for auto vehicle, for motor vehicle, the overloaded method will be called
		public void ActivateDeactivateAutoMotorVehiclePolicy(Cms.Model.Policy.ClsPolicyVehicleInfo  objVehicleInfo, string strStatus)
		{
			ActivateDeactivateAutoMotorVehiclePolicy(objVehicleInfo,strStatus,"");
		}

		//Added by Swastika on 9th Mar'06 for Pol iss #57
		public void ActivateDeactivateAutoMotorVehiclePolicy(Cms.Model.Policy.ClsPolicyVehicleInfo  objVehicleInfo, string strStatus, string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAutoVehiclePolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string strVehicleName="Policy Vehicle";
			if(strCalledFrom==CALLED_FROM_MOTORCYCLE)
				strVehicleName="Policy Motorcycle ";
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/Motorcycle/PolicyMotorCycleInformation.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID		=	objVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID=	objVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	strVehicleName + " is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	strVehicleName+ " is Deactivated";
					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();				
				if(returnResult>0 && strCalledFrom!=CALLED_FROM_MOTORCYCLE)
				{
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					objDriverDetail.UpdateVehicleClassPolNew(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,objDataWrapper);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);		
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


		public void ActivateDeactivateUmbrellaVehiclePolicy(Cms.Model.Policy.Umbrella.ClsVehicleInfo   objVehicleInfo, string strStatus)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateUmbrellaVehiclePolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/Motorcycle/PolicyMotorCycleInformation.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID		=	objVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID=	objVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Vehicle is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Vehicle is Deactivated";
					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}	
				if(returnResult>0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_VEHICLES);
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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

		#endregion

		/*	public void ActivateDeactivateMotorVehicle(ClsVehicleInfo objVehicleInfo, string strStatus,string strCustomInfo)
			{
				string		strStoredProc	=	"Proc_ActivateDeactivateAutoVehicle";			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				try
				{
					objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
					objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
					objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
					objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

					int returnResult = 0;
					if(TransactionLogRequired)
					{																			
						objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/VehicleInfo.aspx.resx");
					
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
						if(strStatus.ToUpper()=="Y")
							objTransactionInfo.TRANS_DESC		=	"Vehicle is Activated";
						else
							objTransactionInfo.TRANS_DESC		=	"Vehicle is Deactivated";
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
						//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}				
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
				}
				catch(Exception ex)
				{
					throw(ex);				
				}
				finally
				{
					if(objDataWrapper != null) objDataWrapper.Dispose();
				}	
				*/



		public void InsertVehicleFromCustVehicleTable(ClsVehicleInfo objVehicleInfo, DataTable dtSelectedVehicles,char coverage_To_Be_Copy)
		{
			string	strStoredProc = "Proc_InsertCustomerVehicleToAppVehicle";						
			string VehicleInfo="";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{				
				for(int i=0;i < dtSelectedVehicles.Rows.Count;i++)
				{
					DataRow dr=dtSelectedVehicles.Rows[i];						
					objDataWrapper.AddParameter("@FROM_VEHICLE_ID",int.Parse(dr["VehicleID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",objVehicleInfo.APP_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",objVehicleInfo.CREATED_BY,SqlDbType.Int);	
					objDataWrapper.AddParameter("@COVERAGE_TO_BE_COPY",coverage_To_Be_Copy,SqlDbType.Char);						
					VehicleInfo+=";Motorcycle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
					//DriverInfo +=";Driver Name: " + dr["ApplicantName"].ToString();

					int returnResult = 0;
					if(TransactionLogRequired && i==(dtSelectedVehicles.Rows.Count-1))
					{
						
						objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/CustomerVehicle.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objVehicleInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Motorcycle is copied";
						objTransactionInfo.CUSTOM_INFO		=	VehicleInfo;
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					}									
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
									
				}	
					
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

		}
	
	
		public void InsertVehicleFromCustVehicleTable(ClsVehicleInfo objVehicleInfo, DataTable dtSelectedVehicles,char coverage_To_Be_Copy,string strCalledFrom)
		{
			string	strStoredProc = "Proc_InsertCustomerVehicleToAppVehicle";						
			string VehicleInfo="";
			string strTransDesc="";
			//int vehicleID;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{				
				for(int i=0;i < dtSelectedVehicles.Rows.Count;i++)
				{
					DataRow dr=dtSelectedVehicles.Rows[i];	
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@FROM_VEHICLE_ID",int.Parse(dr["VehicleID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",objVehicleInfo.APP_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",objVehicleInfo.CREATED_BY,SqlDbType.Int);	
					objDataWrapper.AddParameter("@COVERAGE_TO_BE_COPY",coverage_To_Be_Copy,SqlDbType.Char);	
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@NEW_VEH_ID",SqlDbType.Int,ParameterDirection.Output);
					
					/*if(strCalledFrom=="PPA")
					{
						VehicleInfo+=";Insured Vehicle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Vehicle is copied";
					}
					else
					{
						VehicleInfo+=";Motorcycle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Motorcycle is copied";
					}*/

					//DriverInfo +=";Driver Name: " + dr["ApplicantName"].ToString();

					int returnResult = 0;
					if(TransactionLogRequired && i==(dtSelectedVehicles.Rows.Count-1))
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						//Fetch the newly generated vehicles numbers for transaction log
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
							objDataWrapper.ClearParameteres();
							objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
							objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
							objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
							objDataWrapper.AddParameter("@VEHICLE_ID",objSqlParameter.Value.ToString());
							returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetAssgndDrvrType");
							objDataWrapper.ClearParameteres();
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}
						objDataWrapper.ClearParameteres();
						objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/CustomerVehicle.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objVehicleInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	strTransDesc;
						objTransactionInfo.CUSTOM_INFO		=	VehicleInfo;						
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						//returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
						objDataWrapper.ClearParameteres();
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
							objDataWrapper.ClearParameteres();
							objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
							objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
							objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
							objDataWrapper.AddParameter("@VEHICLE_ID",objSqlParameter.Value.ToString());
							returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetAssgndDrvrType");
							objDataWrapper.ClearParameteres();
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}

					}									
					objDataWrapper.ClearParameteres();

					if ( coverage_To_Be_Copy == 'Y')
					{
						//Modified By Shafi
						//Insert/delete relevant coverages*********************	
						//						objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
						//						objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
						//						objDataWrapper.AddParameter("@VEHICLE_ID",Convert.ToInt32(objSqlParameter.Value));
						//						objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
						//
						//						objDataWrapper.ExecuteNonQuery("Proc_Update_VEHICLE_COVERAGES");
						//						objDataWrapper.ClearParameteres();
						ClsVehicleCoverages objCoverage;
						if(strCalledFrom=="PPA")//Added by Charles on 1-Sep-09 for Itrack 6151
							objCoverage=new ClsVehicleCoverages();
						else
							objCoverage=new ClsVehicleCoverages("MOTOR");//Added by Charles on 1-Sep-09 for Itrack 6151
						objCoverage.SaveDefaultCoveragesApp(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();
						objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,RuleType.AutoDriverDep,Convert.ToInt32(objSqlParameter.Value) );
						objDataWrapper.ClearParameteres();
						if(strCalledFrom=="MOT")//If added on 1-Sep-09 for Itrack 6151
						{
							objCoverage.UpdateCoveragesByRuleApp(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,RuleType.RiskDependent,Convert.ToInt32(objSqlParameter.Value) );
							objDataWrapper.ClearParameteres();
						}
					}
					
					//					//Ravindra (05-23-06) To update Endorsment's linked with Policy Level coverages
					//					vehicleID =Convert.ToInt32(objSqlParameter.Value); 
					//					Cms.BusinessLayer.BlApplication.ClsVehicleCoverages.UpdateApplicationEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,vehicleID,objDataWrapper);
					//					////////////////////
					//					//***********************					
					//
					//					//Update driver endorsements
					//					ClsDriverDetail objDriver = new ClsDriverDetail();
					//					objDriver.UpdateDriverEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID,objVehicleInfo.APP_VERSION_ID,objDataWrapper);
					//
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					objDataWrapper.ClearParameteres();
									
				}	
					
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

		}
	
		public static int FetchTerritoryForZip(string zipId,int lobId)
		{		
			try
			{
				DataSet dsTemp = new DataSet();
				int result;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				objDataWrapper.AddParameter("@ZIP_ID",zipId);
				objDataWrapper.AddParameter("@LOBID",lobId);				
				dsTemp=objDataWrapper.ExecuteDataSet("Proc_FetchTerritoryForZip");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					result=int.Parse((dsTemp.Tables[0].Rows[0][0]).ToString());
				}
				else
				{	
					result=0;				
				}
				return result;				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}	
		}


		#region REMARKS
		public int UpdateRemarks(ClsVehicleInfo objOldVehicleInfo,ClsVehicleInfo ObjVehicleInfo)
		{
			/*SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int returnResult = 0;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);

			objDataWrapper.AddParameter("@CUSTOMER_ID",ObjVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_ID",ObjVehicleInfo.APP_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",ObjVehicleInfo.APP_VERSION_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@VEHICLE_ID",ObjVehicleInfo.VEHICLE_ID,SqlDbType.Int);
			if(ObjVehicleInfo.REMARKS=="")
				objDataWrapper.AddParameter("@REMARKS",System.DBNull.Value);
			else
				objDataWrapper.AddParameter("@REMARKS",ObjVehicleInfo.REMARKS);

			
			try
			{
				
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN);
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
			}*/
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int returnResult = 0;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
			try
			{

				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",ObjVehicleInfo.APP_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",ObjVehicleInfo.APP_VERSION_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLE_ID",ObjVehicleInfo.VEHICLE_ID,SqlDbType.Int);
				if(ObjVehicleInfo.REMARKS=="")
					objDataWrapper.AddParameter("@REMARKS",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@REMARKS",ObjVehicleInfo.REMARKS);
				/*try
				{
				
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN);
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					returnResult= int.Parse(objSqlParameter.Value.ToString());					
					return returnResult;
				}*/
				if(TransactionLogRequired) 
				{
					ObjVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/UmbrellaRemarks.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo,ObjVehicleInfo);                   
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID			=	ObjVehicleInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	ObjVehicleInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	ObjVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	ObjVehicleInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Remarks is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProcAN);
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
		/// <summary>
		/// Function for viewing the details of Remarks
		/// </summary>
		/// <returns></returns>
		public DataSet ViewRemarks(string strCustomerID,string strAppID,string strAppVerID,string strVehicleID)
		{
			string strProcName = "Proc_GetUmbrellaRemarks";
			DataSet dsRemarks = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_ID",strAppID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@Vehicle_ID",strVehicleID,SqlDbType.Int);
			dsRemarks = objDataWrapper.ExecuteDataSet(strProcName);
			
			return dsRemarks;
		}
		
		#endregion

		#region Delete
		public int Delete(int intCustomerId, int intAppId, int intAppVerId, int intVehicleId, string strCalledFrom)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"PROC_DELETEVEHICLE";
			objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objDataWrapper.AddParameter("@APP_ID",intAppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",intAppVerId);
			objDataWrapper.AddParameter("@VEHICLE_ID",intVehicleId);
			objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			return intResult;
		
		}

		public int Delete(ClsVehicleInfo objVehicleInfo, string strCalledFrom, string strCustomInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"PROC_DELETEVEHICLE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
				

				if(TransactionRequired) 
				{			

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Vehicle is Deleted";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
				{
					objDataWrapper.ClearParameteres();
					if(strCalledFrom.ToUpper()!="UMB")
					{
						ClsDriverDetail objDriverDetail = new ClsDriverDetail();
						objDriverDetail.UpdateVehicleClassNew(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.APP_ID, objVehicleInfo.APP_VERSION_ID,objDataWrapper);
					}
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}

		public int Delete(ClsVehicleInfo objVehicleInfo, string strCustomInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeleteMotorCycle";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objVehicleInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objVehicleInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				
				

				if(TransactionRequired) 
				{			

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objVehicleInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objVehicleInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Motorcycle is Deleted";
					objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}
		
		public static int Delete(string strCustomerID, string AppID, string AppVersionID, string strVehicleID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeleteMotorCycle";
			objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerID);			
			objDataWrapper.AddParameter("@APP_ID",AppID);			
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);			
			objDataWrapper.AddParameter("@VEHICLE_ID",strVehicleID);			
			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			return intResult;
		}
		
	
		#endregion

		public int CheckAppVehicleExistence(ClsVehicleInfo objInfo,DataWrapper objDataWrapper)
		{
			string strStoredProc =	"Proc_CheckAppVehicleExistence";

			objDataWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@VIN",objInfo.VIN);
			
			SqlParameter retVal = (SqlParameter)objDataWrapper.AddParameter("@VEHICLE_ID",SqlDbType.Int,ParameterDirection.Output);

			objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			int returnResult = Convert.ToInt32(retVal.Value);

			return returnResult;

		}
		#region Fetch State ID(App/Pol)
		public static int GetStateIdForApplication(int custId,int appId,int appVerId)
		{
			string strStoredProc =	"Proc_GetStateIdForApplication";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@custId",custId);
			objDataWrapper.AddParameter("@appId",appId);
			objDataWrapper.AddParameter("@appVerId",appVerId);
						
			SqlParameter retVal = (SqlParameter)objDataWrapper.AddParameter("@STATE_ID",SqlDbType.Int,ParameterDirection.Output);

			objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			return int.Parse(retVal.Value.ToString());
				
		}

		/// <summary>
		/// function for fetching state_id. 
		/// </summary>
		/// <param name="custId"></param>
		/// <param name="appId"></param>
		/// <param name="appVerId"></param>
		/// <returns></returns>
		public static int GetStateIdForpolicy(int custId,string appId,string appVerId)
		{
			string strStoredProc =	"Proc_GetPolicyState";
			int stId=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",custId);
			objDataWrapper.AddParameter("@POL_ID",appId==""?0:int.Parse(appId));
			objDataWrapper.AddParameter("@POL_VERSION_ID",appVerId==""?0:int.Parse(appVerId));
			
			DataSet ds=objDataWrapper.ExecuteDataSet(strStoredProc) ;
			if(ds!=null)
				if(ds.Tables[0].Rows.Count>0)
					stId=Convert.ToInt32(ds.Tables[0].Rows[0]["STATE_ID"].ToString());   

					
			return stId;
				
		}
		
		#endregion

		public DataSet GetVehicleInfo(int customerID,int appID, int appVersionID, int vehicleID)
		{
			DataSet dsTemp = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMERID",customerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APPID",appID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APPVERSIONID",appVersionID,SqlDbType.Int);
			objDataWrapper.AddParameter("@VEHICLEID",vehicleID,SqlDbType.Int);

			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppVehicleInformation");
			
			return dsTemp;
		}

		/// <summary>
		/// Fetching Vehicle Information from POL_VEHICLES tables
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public DataSet GetPolicyVehicleInfo(int customerID,int polID, int polVersionID, int vehicleID)
		{
			DataSet dsTemp = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMERID",customerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICYID",polID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICYVERSIONID",polVersionID,SqlDbType.Int);
			objDataWrapper.AddParameter("@VEHICLEID",vehicleID,SqlDbType.Int);

			dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyVehicleInformation");
			
			return dsTemp;
		}
		
		public void SetVehicleClassType(int CustomerID, int AppID, int AppVersionID, int VehicleID, DropDownList objVehicleUse, DropDownList objPerClass, DropDownList objPerType, DropDownList objComClass, DropDownList objComType)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID,SqlDbType.Int);
				
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetVehicleUseClassType");

				// Changes done by mohit on 24/10/2005.
				// Database table field names are changed for displaying the saved values.
				
				//objVehicleUse.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"].ToString();
				objVehicleUse.SelectedValue = dsTemp.Tables[0].Rows[0]["USE_VEHICLE"].ToString();
				
				//				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"] != null)
				//				{
				//					objPerClass.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString();
				//				}

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["CLASS_PER"]) != 0 && dsTemp.Tables[0].Rows[0]["CLASS_PER"].ToString() != "" && dsTemp.Tables[0].Rows[0]["CLASS_PER"] != null && Convert.ToInt32(dsTemp.Tables[0].Rows[0]["CLASS_PER"]) != -1)
				{
					objPerClass.SelectedValue = dsTemp.Tables[0].Rows[0]["CLASS_PER"].ToString();
				}

				//				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"] != null)
				//				{
				//					objComClass.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString();
				//				}

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["CLASS_COM"]) != 0 && dsTemp.Tables[0].Rows[0]["CLASS_COM"].ToString() != "" && dsTemp.Tables[0].Rows[0]["CLASS_COM"] != null && Convert.ToInt32(dsTemp.Tables[0].Rows[0]["CLASS_COM"]) != -1)
				{
					objComClass.SelectedValue = dsTemp.Tables[0].Rows[0]["CLASS_COM"].ToString();
				}

				//				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"] != null)
				//				{
				//					objPerType.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString();
				//				}

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"]) != 0 && dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"].ToString() != "" && dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"] != null && Convert.ToInt32(dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"]) != -1)
				{
					string strPerVeh  = dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"].ToString();
					ListItem Li =  objPerType.Items.FindByValue(strPerVeh);

					if(Li != null)
						objPerType.SelectedValue = strPerVeh;
					else
						objPerType.SelectedIndex = -1;
				}


				//				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"] != null)
				//				{
				//					objComType.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString();
				//				}

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"]) != 0 && dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"].ToString() != "" && dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"] != null && Convert.ToInt32(dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"]) != -1)
				{
					objComType.SelectedValue = dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"].ToString();
				}
				//--------------------------------------End------------------------------.
				
				
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		public void SetUmbVehicleClassType(int CustomerID, int AppID, int AppVersionID, int VehicleID, DropDownList objVehicleUse, DropDownList objPerClass, DropDownList objPerType, DropDownList objComClass, DropDownList objComType)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID,SqlDbType.Int);
				
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbVehicleUseClassType");
				
				
				// Changes done by mohit on 24/10/2005.
				// Database table field names are changed for displaying the saved values.
						

				//objVehicleUse.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"].ToString();

				objVehicleUse.SelectedValue = dsTemp.Tables[0].Rows[0]["USE_VEHICLE"].ToString();
				
				//				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"] != null)
				//				{
				//					objPerClass.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString();
				//				}

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["CLASS_PER"]) != 0 && dsTemp.Tables[0].Rows[0]["CLASS_PER"].ToString() != "" && dsTemp.Tables[0].Rows[0]["CLASS_PER"] != null)
				{
					objPerClass.SelectedValue = dsTemp.Tables[0].Rows[0]["CLASS_PER"].ToString();
				}

				//				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"] != null)
				//				{
				//					objComClass.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString();
				//				}

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["CLASS_COM"]) != 0 && dsTemp.Tables[0].Rows[0]["CLASS_COM"].ToString() != "" && dsTemp.Tables[0].Rows[0]["CLASS_COM"] != null)
				{
					objComClass.SelectedValue = dsTemp.Tables[0].Rows[0]["CLASS_COM"].ToString();
				}


				//				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"] != null)
				//				{
				//					objPerType.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString();
				//				}

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"]) != 0 && dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"].ToString() != "" && dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"] != null)
				{
					objPerType.SelectedValue = dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_PER"].ToString();
				}

				//				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"] != null)
				//				{
				//					objComType.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString();
				//				}

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"]) != 0 && dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"].ToString() != "" && dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"] != null)
				{
					objComType.SelectedValue = dsTemp.Tables[0].Rows[0]["VEHICLE_TYPE_COM"].ToString();
				}

				//---------------------------------End----------------------------.
				
				
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		/// <summary>
		/// Updates the class of specified vehicle
		/// </summary>
		// <param name="AppID">Application identification no</param>
		/// <param name="AppVerId">Application version identification no</param>
		/// <param name="CustomerId">Customeridentification no</param>
		/// <param name="VehicleID">Vehicle identification number</param>
		/// <param name="VehicleClass">Class of vehicle</param>
		internal void UpdateVehicleClass(DataWrapper objDataWrapper, int AppId, int AppVerID, int CustomerId, int VehicleID, string VehicleClass)
		{
			objDataWrapper.ExecuteNonQuery("UPDATE CLASS = '" + VehicleClass + "' WHERE "
				+ " CUSTOMER_ID = '" + CustomerId.ToString() 
				+ "' APP_ID = '" + AppId.ToString() 
				+ "' APP_VERSION_ID = '" + AppVerID.ToString() 
				+ "' VEHICLE_ID = '" + VehicleID.ToString() + "'");
		}
	 
		public static DataTable GetClassAndTypeForMotorcycle(string Manufacturer, string Model)
		{			
			DataSet dsTransDesc=new DataSet();
			dsTransDesc=DataWrapper.ExecuteDataset(ConnStr,"Proc_GetTypeAndClassForMotorcycle",Manufacturer,Model);
			return dsTransDesc.Tables[0];
		}

		public static DataSet FetchVINDataFromVINMASTER(string VinNumber)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@VINNumber",VinNumber);				
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetVINDataFromVINMASTER");			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
	

		#region Policy Functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objOldVehicleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicyVehicle(ClsPolicyVehicleInfo objOldVehicleInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyVehicleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
				objDataWrapper.AddParameter("@ANTI_LCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);
				objDataWrapper.AddParameter("@BUSS_PERM_RESI",objOldVehicleInfo.BUSS_PERM_RESI);
				objDataWrapper.AddParameter("@SNOWPLOW_CONDS",objOldVehicleInfo.SNOWPLOW_CONDS);
				objDataWrapper.AddParameter("@CAR_POOL",objOldVehicleInfo.CAR_POOL);
				objDataWrapper.AddParameter("@AUTO_POL_NO",objOldVehicleInfo.AUTO_POL_NO);
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				
				if(objOldVehicleInfo.REGN_PLATE_NUMBER!="")
					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				else
					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",null);

				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				
				if(objOldVehicleInfo.AMOUNT !=0)
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				 
				//if(objOldVehicleInfo.SYMBOL!=0)
				//{
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				//}
				 

				if(objOldVehicleInfo.VEHICLE_AGE!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);				
				}
				 
				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);			
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@IS_OWN_LEASE",objOldVehicleInfo.IS_OWN_LEASE);
				objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);
				objDataWrapper.AddParameter("@IS_NEW_USED",objOldVehicleInfo.IS_NEW_USED);
				objDataWrapper.AddParameter("@MILES_TO_WORK",objOldVehicleInfo.MILES_TO_WORK);
				objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
				objDataWrapper.AddParameter("@VEH_PERFORMANCE",objOldVehicleInfo.VEH_PERFORMANCE);
				objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);
				//objDataWrapper.AddParameter("@SAFETY_BELT",objOldVehicleInfo.SAFETY_BELT);

				//if(objOldVehicleInfo.ANNUAL_MILEAGE==0)
				if(objOldVehicleInfo.ANNUAL_MILEAGE==-1)
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",objOldVehicleInfo.ANNUAL_MILEAGE);

				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",objOldVehicleInfo.PASSIVE_SEAT_BELT);
				objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
							
				//objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",objOldVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE);				
				//objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNDERINS_MOTOR_INJURY_COVE);				
				//objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNINS_MOTOR_INJURY_COVE);	
			
				objDataWrapper.AddParameter("@APP_USE_VEHICLE_ID",objOldVehicleInfo.APP_USE_VEHICLE_ID);	
				objDataWrapper.AddParameter("@APP_VEHICLE_PERCLASS_ID",objOldVehicleInfo.APP_VEHICLE_PERCLASS_ID);	
				objDataWrapper.AddParameter("@APP_VEHICLE_COMCLASS_ID",objOldVehicleInfo.APP_VEHICLE_COMCLASS_ID);	
				objDataWrapper.AddParameter("@APP_VEHICLE_PERTYPE_ID",objOldVehicleInfo.APP_VEHICLE_PERTYPE_ID);	
				objDataWrapper.AddParameter("@APP_VEHICLE_COMTYPE_ID",objOldVehicleInfo.APP_VEHICLE_COMTYPE_ID);						
				if(objOldVehicleInfo.RADIUS_OF_USE==0)
					objDataWrapper.AddParameter("@RADIUS_OF_USE",null);
				else
					objDataWrapper.AddParameter("@RADIUS_OF_USE",objOldVehicleInfo.RADIUS_OF_USE);
				objDataWrapper.AddParameter("@TRANSPORT_CHEMICAL",objOldVehicleInfo.TRANSPORT_CHEMICAL);
				objDataWrapper.AddParameter("@COVERED_BY_WC_INSU",objOldVehicleInfo.COVERED_BY_WC_INSU);
				objDataWrapper.AddParameter("@CLASS_DESCRIPTION",objOldVehicleInfo.CLASS_DESCRIPTION);
				objDataWrapper.AddParameter("@IS_SUSPENDED",objOldVehicleInfo.IS_SUSPENDED);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int VEHICLE_ID;
				if(TransactionLogRequired)
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\aspx\PolicyVehicleInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objOldVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objOldVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1502", "");//"New Policy Vehicle is Added";
					
					//Executing the query
					//Executing the query to save vehicle info
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
					objOldVehicleInfo.INSURED_VEH_NUMBER = VEHICLE_ID;
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					//Remove nodes 
					//For Personal use, remove nodes for commercial user					
					if(objOldVehicleInfo.APP_USE_VEHICLE_ID==11332)
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_COM']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='CLASS_COM']");					
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='CLASS_PER' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_PER' and @NewValue='0']","NewValue","null");
					}
					else //Vehicle is for commercial use, remove nodes for personal user
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='MULTI_CAR']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_USE']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='BUSS_PERM_RESI']");

						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_PER']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='CLASS_PER']");					
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='CLASS_COM' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_COM' and @NewValue='0']","NewValue","null");
					}
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='BUSS_PERM_RESI' and @NewValue='0']","NewValue","null");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objDataWrapper.ClearParameteres();
					// inserting transaction log
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
				returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetPolAssgndDrvrType");

				objDataWrapper.ClearParameteres();
			
				//Insert/delete relevant policy coverages*********************	
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				//				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				//				objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
				//				
				//				objDataWrapper.ExecuteNonQuery("Proc_Update_POLICY_VEHICLE_COVERAGES");
				//				//***********************
				//				//Update Driver endorsements  // Swastika
				//				Cms.BusinessLayer.BlApplication.ClsDriverDetail objDriver = new ClsDriverDetail();
				//				objDriver.UpdatePolicyDriverEndorsements(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,VEHICLE_ID,objDataWrapper);
				//				//End of endorsements
				//				
				//				//Ravindra (04-28-06) To update Endorsment's linked with Policy Level coverages
				//				Cms.BusinessLayer.BlApplication.ClsVehicleCoverages.UpdatePolicyEndorsements(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,VEHICLE_ID,objDataWrapper);
				////////////////////
				///

				//ClsVehicleCoverages obCoverage= new ClsVehicleCoverages();
				//obCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,RuleType.RiskDependent,objOldVehicleInfo.VEHICLE_ID);

				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.SaveDefaultCoveragesPolicy(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID ,objOldVehicleInfo.POLICY_VERSION_ID,VEHICLE_ID);
				objDataWrapper.ClearParameteres();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep,VEHICLE_ID);
				

				

				if(VEHICLE_ID > 0)
				{
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					objDriverDetail.UpdateVehicleClassPolNew(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,objDataWrapper);
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);

				
				return VEHICLE_ID;
				
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


		public int AddUmbrellaPolicyVehicle(Cms.Model.Policy.Umbrella.ClsVehicleInfo  objOldVehicleInfo, string Called_For)
		{
			string		strStoredProc	=	"Proc_InsertPolicyUmbrellaVehicleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper	=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",null);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",null);
				objDataWrapper.AddParameter("@GRG_ADD1",null);
				objDataWrapper.AddParameter("@GRG_ADD2",null);
				objDataWrapper.AddParameter("@GRG_CITY",null);
				objDataWrapper.AddParameter("@GRG_COUNTRY",null);
				objDataWrapper.AddParameter("@GRG_STATE",null);
				objDataWrapper.AddParameter("@GRG_ZIP",null);
				objDataWrapper.AddParameter("@REGISTERED_STATE",null);
				objDataWrapper.AddParameter("@TERRITORY",null);
				objDataWrapper.AddParameter("@CLASS",null);
				objDataWrapper.AddParameter("@ANTI_LCK_BRAKES",null);				
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);				
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);				
				objDataWrapper.AddParameter("@ST_AMT_TYPE",null);								
				objDataWrapper.AddParameter("@AMOUNT",null);
				objDataWrapper.AddParameter("@SYMBOL",null);				
				objDataWrapper.AddParameter("@VEHICLE_AGE",null);				
				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);							
				objDataWrapper.AddParameter("@IS_OWN_LEASE",null);
				objDataWrapper.AddParameter("@PURCHASE_DATE",null);
				objDataWrapper.AddParameter("@IS_NEW_USED",null);
				objDataWrapper.AddParameter("@MILES_TO_WORK",null);
				objDataWrapper.AddParameter("@VEHICLE_USE",null);
				objDataWrapper.AddParameter("@VEH_PERFORMANCE",null);
				objDataWrapper.AddParameter("@MULTI_CAR",null);
				objDataWrapper.AddParameter("@ANNUAL_MILEAGE",null);
				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",null);
				objDataWrapper.AddParameter("@AIR_BAG",null);
				objDataWrapper.AddParameter("@APP_USE_VEHICLE_ID",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_PERCLASS_ID",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_COMCLASS_ID",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_PERTYPE_ID",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_COMTYPE_ID",null);
				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);
				objDataWrapper.AddParameter("@IS_EXCLUDED",objOldVehicleInfo.IS_EXCLUDED);
				objDataWrapper.AddParameter("@OTHER_POLICY",objOldVehicleInfo.OTHER_POLICY);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);
				if(Called_For=="MOT")
					objDataWrapper.AddParameter("@CALLED_FOR","Motor Home" );
				else
					objDataWrapper.AddParameter("@CALLED_FOR","Auto" );
				int returnResult = 0;
				int VEHICLE_ID;
				if(TransactionLogRequired)
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\AddPolUmbVehicleInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					//string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objOldVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objOldVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1502", ""); //"New Policy Vehicle is Added";
					
					//Executing the query
					//Executing the query to save vehicle info
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
					objOldVehicleInfo.INSURED_VEH_NUMBER = VEHICLE_ID;
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objDataWrapper.ClearParameteres();
					// inserting transaction log
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID=int.Parse(objSqlParameter.Value.ToString());
				}
				objDataWrapper.ClearParameteres();
				
			
				//Insert/delete relevant policy coverages*********************	
				/*objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
				
				objDataWrapper.ExecuteNonQuery("Proc_Update_POLICY_VEHICLE_COVERAGES");*/
				//***********************

				if(returnResult > 0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_VEHICLES);	
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				return VEHICLE_ID;
				
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



		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldOldVehicleInfo">Model object having old information</param>
		/// <param name="objOldVehicleInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicyVehicle(ClsPolicyVehicleInfo objOldOldVehicleInfo,ClsPolicyVehicleInfo objOldVehicleInfo)
		{
			string strTranXML;
			int returnResult = 0;
			//string strXML;

			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdatePolicyVehicleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				objDataWrapper.AddParameter("@BODY_TYPE",objOldVehicleInfo.BODY_TYPE);
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				objDataWrapper.AddParameter("@CLASS",objOldVehicleInfo.CLASS);
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",objOldVehicleInfo.ST_AMT_TYPE);
				objDataWrapper.AddParameter("@VEHICLE_TYPE",objOldVehicleInfo.VEHICLE_TYPE);
				objDataWrapper.AddParameter("@BUSS_PERM_RESI",objOldVehicleInfo.BUSS_PERM_RESI);
				objDataWrapper.AddParameter("@SNOWPLOW_CONDS",objOldVehicleInfo.SNOWPLOW_CONDS);
				objDataWrapper.AddParameter("@CAR_POOL",objOldVehicleInfo.CAR_POOL);
				objDataWrapper.AddParameter("@AUTO_POL_NO",objOldVehicleInfo.AUTO_POL_NO);				
				if(objOldVehicleInfo.AMOUNT!=0)
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				//if(objOldVehicleInfo.SYMBOL!=0)
				//{
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				//}
				
				objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				//objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@IS_OWN_LEASE",objOldVehicleInfo.IS_OWN_LEASE);
				objDataWrapper.AddParameter("@PURCHASE_DATE",objOldVehicleInfo.PURCHASE_DATE);
				objDataWrapper.AddParameter("@IS_NEW_USED",objOldVehicleInfo.IS_NEW_USED);
				objDataWrapper.AddParameter("@MILES_TO_WORK",objOldVehicleInfo.MILES_TO_WORK);
				objDataWrapper.AddParameter("@VEHICLE_USE",objOldVehicleInfo.VEHICLE_USE);
				objDataWrapper.AddParameter("@VEH_PERFORMANCE",objOldVehicleInfo.VEH_PERFORMANCE);
				objDataWrapper.AddParameter("@MULTI_CAR",objOldVehicleInfo.MULTI_CAR);

				//objDataWrapper.AddParameter("@SAFETY_BELT",objOldVehicleInfo.SAFETY_BELT);

				if(objOldVehicleInfo.ANNUAL_MILEAGE==-1)
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@ANNUAL_MILEAGE",objOldVehicleInfo.ANNUAL_MILEAGE);
				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",objOldVehicleInfo.PASSIVE_SEAT_BELT);
				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",objOldVehicleInfo.ANTI_LOCK_BRAKES);

				objDataWrapper.AddParameter("@AIR_BAG",objOldVehicleInfo.AIR_BAG);
				
				//objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",objOldVehicleInfo.UNINS_PROPERTY_DAMAGE_COVE);				
				//objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNDERINS_MOTOR_INJURY_COVE);				
				//objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",objOldVehicleInfo.UNINS_MOTOR_INJURY_COVE);				

				objDataWrapper.AddParameter("@APP_USE_VEHICLE_ID",objOldVehicleInfo.APP_USE_VEHICLE_ID);				
				objDataWrapper.AddParameter("@APP_VEHICLE_PERCLASS_ID",objOldVehicleInfo.APP_VEHICLE_PERCLASS_ID);				
				objDataWrapper.AddParameter("@APP_VEHICLE_COMCLASS_ID",objOldVehicleInfo.APP_VEHICLE_COMCLASS_ID);				
				objDataWrapper.AddParameter("@APP_VEHICLE_PERTYPE_ID",objOldVehicleInfo.APP_VEHICLE_PERTYPE_ID);				
				objDataWrapper.AddParameter("@APP_VEHICLE_COMTYPE_ID",objOldVehicleInfo.APP_VEHICLE_COMTYPE_ID);								

				if(objOldVehicleInfo.RADIUS_OF_USE==0)
					objDataWrapper.AddParameter("@RADIUS_OF_USE",null);
				else
					objDataWrapper.AddParameter("@RADIUS_OF_USE",objOldVehicleInfo.RADIUS_OF_USE);
				objDataWrapper.AddParameter("@TRANSPORT_CHEMICAL",objOldVehicleInfo.TRANSPORT_CHEMICAL);
				objDataWrapper.AddParameter("@COVERED_BY_WC_INSU",objOldVehicleInfo.COVERED_BY_WC_INSU);
				objDataWrapper.AddParameter("@CLASS_DESCRIPTION",objOldVehicleInfo.CLASS_DESCRIPTION);
				objDataWrapper.AddParameter("@IS_SUSPENDED",objOldVehicleInfo.IS_SUSPENDED);
				if(TransactionRequired) 
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyVehicleInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldOldVehicleInfo,objOldVehicleInfo);
					if(objOldVehicleInfo.APP_USE_VEHICLE_ID==11332)
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_COM']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='CLASS_COM']");											
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='CLASS_PER' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_PER' and @NewValue='0']","NewValue","null");
					}
					else //Vehicle is for commercial use, remove nodes for personal user
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='MULTI_CAR']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_USE']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='BUSS_PERM_RESI']");

						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_PER']");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='CLASS_PER']");					
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='CLASS_COM' and @NewValue='0']","NewValue","null");
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='VEHICLE_TYPE_COM' and @NewValue='0']","NewValue","null");
					}
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
						objTransactionInfo.POLICY_ID 		=	objOldVehicleInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID =	objOldVehicleInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1495", "");//"Policy Vehicle is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				objDataWrapper.ClearParameteres();
				//Insert/delete relevant policy coverages*********************	
				// IF VEHICLE CHANGE FROM SUSPENDED YES TO NO iTRACK 5755 -bY pRAVESH K CHANDEL ON 27 APRIL 2009
				if( ((objOldOldVehicleInfo.IS_SUSPENDED!=objOldVehicleInfo.IS_SUSPENDED) && objOldVehicleInfo.IS_SUSPENDED==10964)
					||objOldOldVehicleInfo.APP_USE_VEHICLE_ID!=objOldVehicleInfo.APP_USE_VEHICLE_ID //Added by Charles on 10-Jul-09 for Itrack 6082
					)
				{
					objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
					objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
					objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
					
					//objDataWrapper.ExecuteNonQuery("Proc_Update_POLICY_VEHICLE_COVERAGES");
					objDataWrapper.ExecuteNonQuery("Proc_UPDATE_POLICY_LEVEL_COVERAGES_POL");
					objDataWrapper.ClearParameteres();
				}
				//***********************

				ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
				objCoverage.SaveDefaultCoveragesPolicy(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID ,objOldVehicleInfo.POLICY_VERSION_ID,objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.ClearParameteres();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep,objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.ClearParameteres();
				objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,RuleType.MiscEquipment,objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.ClearParameteres();
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);


				//				//Update Driver endorsements  // Swastika
				//				Cms.BusinessLayer.BlApplication.ClsDriverDetail objDriver = new ClsDriverDetail();
				//				objDriver.UpdatePolicyDriverEndorsements(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,objOldVehicleInfo.VEHICLE_ID,objDataWrapper);
				//End of endorsements

				
				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
				objDriverDetail.UpdateVehicleClassPolNew(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID ,objOldVehicleInfo.POLICY_VERSION_ID,objDataWrapper);
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


		public int UpdatePolicyUmbrellaVehicle(Cms.Model.Policy.Umbrella.ClsVehicleInfo  objOldOldVehicleInfo,
			Cms.Model.Policy.Umbrella.ClsVehicleInfo  objOldVehicleInfo)
		{
			string strTranXML;
			int returnResult = 0;			

			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdatePolicyUmbrellaVehicleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);				
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",null);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",null);
				objDataWrapper.AddParameter("@BODY_TYPE",null);
				objDataWrapper.AddParameter("@GRG_ADD1",null);
				objDataWrapper.AddParameter("@GRG_ADD2",null);
				objDataWrapper.AddParameter("@GRG_CITY",null);
				objDataWrapper.AddParameter("@GRG_COUNTRY",null);
				objDataWrapper.AddParameter("@GRG_STATE",null);
				objDataWrapper.AddParameter("@GRG_ZIP",null);
				objDataWrapper.AddParameter("@REGISTERED_STATE",null);
				objDataWrapper.AddParameter("@TERRITORY",null);
				objDataWrapper.AddParameter("@CLASS",null);
				objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",objOldVehicleInfo.REGN_PLATE_NUMBER);
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);
				objDataWrapper.AddParameter("@ST_AMT_TYPE",null);
				objDataWrapper.AddParameter("@AMOUNT",null);
				objDataWrapper.AddParameter("@SYMBOL",null);				
				objDataWrapper.AddParameter("@VEHICLE_AGE",null);				
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@IS_OWN_LEASE",null);
				objDataWrapper.AddParameter("@PURCHASE_DATE",null);
				objDataWrapper.AddParameter("@IS_NEW_USED",null);
				objDataWrapper.AddParameter("@MILES_TO_WORK",null);
				objDataWrapper.AddParameter("@VEHICLE_USE",null);
				objDataWrapper.AddParameter("@VEH_PERFORMANCE",null);
				objDataWrapper.AddParameter("@MULTI_CAR",null);
				objDataWrapper.AddParameter("@ANNUAL_MILEAGE",null);
				objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",null);
				objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",null);
				objDataWrapper.AddParameter("@AIR_BAG",null);				
				objDataWrapper.AddParameter("@APP_USE_VEHICLE_ID",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_PERCLASS_ID",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_COMCLASS_ID",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_PERTYPE_ID",null);
				objDataWrapper.AddParameter("@APP_VEHICLE_COMTYPE_ID",null);
				objDataWrapper.AddParameter("@USE_VEHICLE",objOldVehicleInfo.USE_VEHICLE);
				objDataWrapper.AddParameter("@IS_EXCLUDED",objOldVehicleInfo.IS_EXCLUDED);
				objDataWrapper.AddParameter("@OTHER_POLICY",objOldVehicleInfo.OTHER_POLICY);
				if(TransactionRequired) 
				{
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\AddPolUmbVehicleInfo.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldOldVehicleInfo,objOldVehicleInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
						objTransactionInfo.POLICY_ID 		=	objOldVehicleInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID =	objOldVehicleInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1495", ""); //"Policy Vehicle is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				objDataWrapper.ClearParameteres();

				//Insert/delete relevant policy coverages*********************	
				/*objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				
				objDataWrapper.ExecuteNonQuery("Proc_Update_POLICY_VEHICLE_COVERAGES");*/
				//***********************

				if(returnResult > 0)
				{
					ClsUmbrellaRecrVeh objUmbRecVeh = new ClsUmbrellaRecrVeh();
					objUmbRecVeh.UmbrellaEndorsementRule(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,clsapplication.CALLED_FROM_POLICY,ClsUmbSchRecords.CALLED_FROM_VEHICLES);	
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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


		/// <summary>
		/// It will fetch policy vehicle information.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="VehicleID"></param>
		/// <returns></returns>
		public static string FetchPolicyVehicleXML(int CustomerID,int PolicyID, int PolicyVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYVERSIONID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyVehicleInformation");
			
				//return dsTemp.GetXml();
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}
	
		public static string FetchPolicyUmbrellaVehicleXML(int CustomerID,int PolicyID, int PolicyVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYVERSIONID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyUmbrellaVehicleInformation");
			
				//return dsTemp.GetXml();
				return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}		


		/// <summary>
		/// It will fill the policy vehicle class type.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="VehicleID"></param>
		/// <param name="objVehicleUse"></param>
		/// <param name="objPerClass"></param>
		/// <param name="objPerType"></param>
		/// <param name="objComClass"></param>
		/// <param name="objComType"></param>
		public void SetPolicyVehicleClassType(int CustomerID, int PolicyID, int PolicyVersionID, int VehicleID, DropDownList objVehicleUse, DropDownList objPerClass, DropDownList objPerType, DropDownList objComClass, DropDownList objComType)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID,SqlDbType.Int);
				
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyVehicleUseClassType");
			
				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"] != null)
					objVehicleUse.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"].ToString();
				
				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"] != null)
					objPerClass.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString();

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"] != null)
					objComClass.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString();


				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"] != null)
					
				{		
					string strPerVeh  =  dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString();
					ListItem Li =  objPerType.Items.FindByValue(strPerVeh);
					if(Li != null)
						objPerType.SelectedValue = strPerVeh;
					else
						objPerType.SelectedIndex = -1;
				}
				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"] != null)
					objComType.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString();

			
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}


		public void SetPolicyUmbrellaVehicleClassType(int CustomerID, int PolicyID, int PolicyVersionID, int VehicleID, DropDownList objVehicleUse, DropDownList objPerClass, DropDownList objPerType, DropDownList objComClass, DropDownList objComType)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID,SqlDbType.Int);
				
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyUmbrellaVehicleUseClassType");
			
				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"] != null)
					objVehicleUse.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_USE_VEHICLE_ID"].ToString();
				
				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"] != null)
					objPerClass.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERCLASS_ID"].ToString();

				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"] != null)
					objComClass.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMCLASS_ID"].ToString();


				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"] != null)
					objPerType.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString();


				if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"]) != 0 && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString() != "" && dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"] != null)
					objComType.SelectedValue = dsTemp.Tables[0].Rows[0]["APP_VEHICLE_COMTYPE_ID"].ToString();

			
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

		/// <summary>
		/// It will fill the policy vehicle details.
		/// </summary>
		/// <param name="intCustomerId"></param>
		/// <param name="intPolicyID"></param>
		/// <param name="intPolicyVersionID"></param>
		/// <param name="intVehicleId"></param>
		/// <returns></returns>
		//Following method will be called for auto vehicle, for motor vehicle, the overloaded method will be called		
		public int DeletePolicyVehicle(Cms.Model.Policy.ClsPolicyVehicleInfo objVehicleInfo, string strCustomInfo)
		{
			return DeletePolicyVehicle(objVehicleInfo, strCustomInfo,"");
		}
		public int DeletePolicyVehicle(Cms.Model.Policy.ClsPolicyVehicleInfo objVehicleInfo, string strCustomInfo, string strCalledFrom)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeletePolicyVehicle";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string strVehicleName="Policy Vehicle";
			if(strCalledFrom==CALLED_FROM_MOTORCYCLE)
				strVehicleName="Policy Motorcycle";

			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				
				

				if(TransactionRequired) 
				{			

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objVehicleInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID			=	objVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	strVehicleName + " is Deleted";
					//objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0 && strCalledFrom!=CALLED_FROM_MOTORCYCLE)
				{
					ClsDriverDetail objDriverDetails = new ClsDriverDetail();
					objDriverDetails.UpdateVehicleClassPolNew(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,objDataWrapper);
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
			}
			//			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			//			string	strStoredProc =	"Proc_DeletePolicyVehicle";
			//			objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			//			objDataWrapper.AddParameter("@POLICY_ID",intPolicyID);
			//			objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionID);
			//			objDataWrapper.AddParameter("@VEHICLE_ID",intVehicleId);
			//			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			//			if(intResult>0)
			//			{
			//				ClsDriverDetail objDriverDetail = new ClsDriverDetail();
			//				objDriverDetail.UpdateVehicleClassPolNew(intCustomerId,intPolicyID,intPolicyVersionID);
			//			}
			//			return intResult;
		
		}
		public int DeletePolicyUmbrellaVehicle(int intCustomerId, int intPolicyID, int intPolicyVersionID, int intVehicleId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			string	strStoredProc =	"Proc_DeletePolicyUmbrellaVehicle";
			objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",intPolicyID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVersionID);
			objDataWrapper.AddParameter("@VEHICLE_ID",intVehicleId);
			int intResult = objDataWrapper.ExecuteNonQuery(strStoredProc);	
			return intResult;
		
		}

		/// <summary>
		/// It will fill the Policy Vehicle information
		/// </summary>
		/// <param name="cmbCombo"></param>
		/// <param name="intCustomrId"></param>
		/// <param name="intPolicyId"></param>
		/// <param name="intPolicyVersionId"></param>
		public static void FillPolicyVehicleInfo(System.Web.UI.WebControls.DropDownList cmbCombo,int intCustomrId, int intPolicyId, int intPolicyVersionId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POLICY_ID", intPolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolicyVersionId);

				DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillPolicyVehicleDropDown").Tables[0];
				cmbCombo.DataSource = objDataTable;
				cmbCombo.DataTextField = "MODEL_MAKE";
				cmbCombo.DataValueField = "VEHICLE_ID";
				cmbCombo.DataBind();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}


		public static void FillPolicyUmbrellaVehicleInfo(System.Web.UI.WebControls.DropDownList cmbCombo,int intCustomrId, int intPolicyId, int intPolicyVersionId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POLICY_ID", intPolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolicyVersionId);

				DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillPolicyUmbrellaVehicleDropDown").Tables[0];
				cmbCombo.DataSource = objDataTable;
				cmbCombo.DataTextField = "MODEL_MAKE";
				cmbCombo.DataValueField = "VEHICLE_ID";
				cmbCombo.DataBind();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}

		public static DataSet FillPolUmbrellaVehicleInfo(int intCustomrId, int intPolicyId, int intPolicyVersionId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@POLICY_ID", intPolicyId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolicyVersionId);

				DataSet dsAssign = objDataWrapper.ExecuteDataSet("Proc_FillPolicyUmbrellaVehicleDropDown");
				if(dsAssign!=null && dsAssign.Tables.Count>0)
					return dsAssign;
				else
					return null;
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}


		//Commented By Ravindra (03/27/06)
		//Because another overload is there  and this method is no longer used 
		//Added by swastika on 9th Mar'06 for Pol Iss #57
		/*
		public int GetDriverCountForAssignedVehiclePolicy(Cms.Model.Policy.ClsPolicyVehicleInfo objVehicleInfo)
		{
			string		strStoredProc	=	"Proc_GetDriverCountForAssignedVehiclePolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objVehicleInfo.VEHICLE_ID);
				//objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
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

		}*/

		// Added by Swastika on 24th Mar'06 for Pol Iss # 60
		public void InsertVehicleFromCustVehicleTablePolicy(ClsPolicyVehicleInfo objVehicleInfo, DataTable dtSelectedVehicles,char coverage_To_Be_Copy,string strCalledFrom)
		{
			string	strStoredProc = "Proc_InsertCustomerVehicleToPolVehicle";						
			string VehicleInfo="";
			string strTransDesc="";
			//int vehicleID;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{				
				for(int i=0;i < dtSelectedVehicles.Rows.Count;i++)
				{
					DataRow dr=dtSelectedVehicles.Rows[i];	
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@FROM_VEHICLE_ID",int.Parse(dr["VehicleID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_ID",int.Parse(dr["PolID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_VERSION_ID",int.Parse(dr["PolVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_ID",objVehicleInfo.POLICY_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",objVehicleInfo.CREATED_BY,SqlDbType.Int);	
					objDataWrapper.AddParameter("@COVERAGE_TO_BE_COPY",coverage_To_Be_Copy,SqlDbType.Char);	
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@NEW_VEH_ID",SqlDbType.Int,ParameterDirection.Output);
					/*if(strCalledFrom=="PPA")
					{
						VehicleInfo+=";Insured Vehicle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Vehicle is copied";
					}
					else
					{
						VehicleInfo+=";Motorcycle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Motorcycle is copied";
					}*/

					//DriverInfo +=";Driver Name: " + dr["ApplicantName"].ToString();

					int returnResult = 0;
					if(TransactionLogRequired && i==(dtSelectedVehicles.Rows.Count-1))
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						//Fetch the newly generated vehicles numbers for transaction log
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
							objDataWrapper.ClearParameteres();
							objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
							objDataWrapper.AddParameter("@POL_ID",objVehicleInfo.POLICY_ID);
							objDataWrapper.AddParameter("@POL_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
							objDataWrapper.AddParameter("@VEHICLE_ID",objSqlParameter.Value.ToString());
							returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetPolAssgndDrvrType");
							objDataWrapper.ClearParameteres();
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
						}
						objDataWrapper.ClearParameteres();
						objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyCustomerVehicle.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID			=	objVehicleInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID=	objVehicleInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objVehicleInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	strTransDesc;
						objTransactionInfo.CUSTOM_INFO		=	VehicleInfo;						
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						//returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
						objDataWrapper.ClearParameteres();
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
							objDataWrapper.ClearParameteres();
							objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
							objDataWrapper.AddParameter("@POL_ID",objVehicleInfo.POLICY_ID);
							objDataWrapper.AddParameter("@POL_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
							objDataWrapper.AddParameter("@VEHICLE_ID",objSqlParameter.Value.ToString());
							returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetPolAssgndDrvrType");
							objDataWrapper.ClearParameteres();
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}

					}									
					objDataWrapper.ClearParameteres();

					if ( coverage_To_Be_Copy == 'Y')
					{
						//Insert/delete relevant coverages*********************	
						//						objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
						//						objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
						//						objDataWrapper.AddParameter("@VEHICLE_ID",Convert.ToInt32(objSqlParameter.Value));
						//						objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
						//
						//						objDataWrapper.ExecuteNonQuery("Proc_Update_POLICY_VEHICLE_COVERAGES");
						ClsVehicleCoverages objCoverage;
						if(strCalledFrom=="PPA")//If added by Charles on 1-Sep-09 for Itrack 6151
							objCoverage=new ClsVehicleCoverages();
						else
							objCoverage=new ClsVehicleCoverages("MOTOR");//Added by Charles on 1-Sep-09 for Itrack 6151
						objCoverage.SaveDefaultCoveragesPolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID ,objVehicleInfo.POLICY_VERSION_ID,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();
						objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();
						if(strCalledFrom=="MOT")//Added by Charles on 1-Sep-09 for Itrack 6151
						{
							objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,RuleType.RiskDependent,Convert.ToInt32(objSqlParameter.Value));
							objDataWrapper.ClearParameteres();
						}
						//***********************
					}
					//Ravindra (05-23-06) To update Endorsment's linked with Policy Level coverages
					//					vehicleID =Convert.ToInt32(objSqlParameter.Value); 
					//					Cms.BusinessLayer.BlApplication.ClsVehicleCoverages.UpdatePolicyEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID ,objVehicleInfo.POLICY_VERSION_ID ,vehicleID,objDataWrapper);
					//					////////////////////
					//					//***********************
					//					//Update driver endorsements
					//					ClsDriverDetail objDriver = new ClsDriverDetail();
					//					objDriver.UpdateDriverEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,objDataWrapper);

					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					objDataWrapper.ClearParameteres();
									
				}	
					
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

		}
		//end

		// Added by Mohit Agarwal on 13th Nov'06 for Gen Iss # 3531
		public void InsertVehicleFromUmbVehicleTablePolicy(ClsPolicyVehicleInfo objVehicleInfo, DataTable dtSelectedVehicles,char coverage_To_Be_Copy,string strCalledFrom)
		{
			string	strStoredProc = "Proc_InsertPolCustomerVehicleToUmbrellaVehicle";						
			string VehicleInfo="";
			string strTransDesc="";
			//int vehicleID;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{				
				for(int i=0;i < dtSelectedVehicles.Rows.Count;i++)
				{
					DataRow dr=dtSelectedVehicles.Rows[i];	
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@FROM_VEHICLE_ID",int.Parse(dr["VehicleID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_ID",int.Parse(dr["PolID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_VERSION_ID",int.Parse(dr["PolVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_ID",objVehicleInfo.POLICY_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",objVehicleInfo.CREATED_BY,SqlDbType.Int);	
					//objDataWrapper.AddParameter("@COVERAGE_TO_BE_COPY",coverage_To_Be_Copy,SqlDbType.Char);	
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@NEW_VEH_ID",SqlDbType.Int,ParameterDirection.Output);
					/*if(strCalledFrom=="PPA")
					{
						VehicleInfo+=";Insured Vehicle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Vehicle is copied";
					}
					else
					{
						VehicleInfo+=";Motorcycle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Motorcycle is copied";
					}*/

					//DriverInfo +=";Driver Name: " + dr["ApplicantName"].ToString();

					int returnResult = 0;
					if(TransactionLogRequired && i==(dtSelectedVehicles.Rows.Count-1))
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						//Fetch the newly generated vehicles numbers for transaction log
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}
						objDataWrapper.ClearParameteres();
						objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyCustomerVehicle.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID			=	objVehicleInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID=	objVehicleInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objVehicleInfo.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	strTransDesc;
						objTransactionInfo.CUSTOM_INFO		=	VehicleInfo;						
						//objTransactionInfo.CHANGE_XML		=	strTranXML;
						//Executing the query
						//returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
						objDataWrapper.ClearParameteres();
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}

					}									
					objDataWrapper.ClearParameteres();

					if ( coverage_To_Be_Copy == 'Y')
					{
						//Insert/delete relevant coverages*********************	
						//						objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
						//						objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
						//						objDataWrapper.AddParameter("@VEHICLE_ID",Convert.ToInt32(objSqlParameter.Value));
						//						objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
						//
						//						objDataWrapper.ExecuteNonQuery("Proc_Update_POLICY_VEHICLE_COVERAGES");
						ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
						objCoverage.SaveDefaultCoveragesPolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID ,objVehicleInfo.POLICY_VERSION_ID,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();
						objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();
						//***********************
					}
					//Ravindra (05-23-06) To update Endorsment's linked with Policy Level coverages
					//					vehicleID =Convert.ToInt32(objSqlParameter.Value); 
					//					Cms.BusinessLayer.BlApplication.ClsVehicleCoverages.UpdatePolicyEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID ,objVehicleInfo.POLICY_VERSION_ID ,vehicleID,objDataWrapper);
					//					////////////////////
					//					//***********************
					//					//Update driver endorsements
					//					ClsDriverDetail objDriver = new ClsDriverDetail();
					//					objDriver.UpdateDriverEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,objDataWrapper);

					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					objDataWrapper.ClearParameteres();
									
				}	
					
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

		}
		//end

		//// Added by Swastika on 24th Mar'06 for Pol Iss # 60

		public void InsertVehicleFromCustVehicleTablePolicy(ClsPolicyVehicleInfo objVehicleInfo, DataTable dtSelectedVehicles, string strCalledFrom)
		{
			string	strStoredProc = "Proc_InsertCustomerVehicleToPolVehicle";						
			string VehicleInfo="";			
			string strTransDesc="";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{				
				for(int i=0;i < dtSelectedVehicles.Rows.Count;i++)
				{
					DataRow dr=dtSelectedVehicles.Rows[i];						
					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@FROM_VEHICLE_ID",int.Parse(dr["VehicleID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_ID",int.Parse(dr["PolID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_POLICY_VERSION_ID",int.Parse(dr["PolVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_ID",objVehicleInfo.POLICY_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID,SqlDbType.Int);					
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",objVehicleInfo.CREATED_BY,SqlDbType.Int);	
					objDataWrapper.AddParameter("@COVERAGE_TO_BE_COPY",null);
					//Output variable
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@NEW_VEH_ID",SqlDbType.Int,ParameterDirection.Output);

					/*if(strCalledFrom=="PPA")
					{
						VehicleInfo+=";Insured Vehicle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Vehicle is copied";
					}
					else
					{
						VehicleInfo+=";Motorcycle Number = " + dr["INSURED_VEH_NUMBER"].ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
						strTransDesc="Motorcycle is copied";
					}*/

					int returnResult = 0;
					if(TransactionLogRequired && i==(dtSelectedVehicles.Rows.Count-1))
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						objDataWrapper.ClearParameteres();
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
							objDataWrapper.ClearParameteres();
							objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
							objDataWrapper.AddParameter("@POL_ID",objVehicleInfo.POLICY_ID);
							objDataWrapper.AddParameter("@POL_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
							objDataWrapper.AddParameter("@VEHICLE_ID",objSqlParameter.Value.ToString());
							returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetPolAssgndDrvrType");
							objDataWrapper.ClearParameteres();
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}

						
						objVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyCustomerVehicle.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDriverDetailsInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.POLICY_ID			=	objVehicleInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objVehicleInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID			=	objVehicleInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY			=	objVehicleInfo.CREATED_BY;
						//objTransactionInfo.TRANS_DESC			=	"Motorcycle is copied";
						objTransactionInfo.TRANS_DESC			=	strTransDesc;
						objTransactionInfo.CUSTOM_INFO			=	VehicleInfo;
						//objTransactionInfo.CHANGE_XML			=	strTranXML;
						//Executing the query
						objDataWrapper.ExecuteNonQuery(objTransactionInfo);
						objDataWrapper.ClearParameteres();
					}
					else
					{
						returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
						if(strCalledFrom=="PPA")
						{
							VehicleInfo+=";Insured Vehicle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Vehicle is copied";
							objDataWrapper.ClearParameteres();
							objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
							objDataWrapper.AddParameter("@POL_ID",objVehicleInfo.POLICY_ID);
							objDataWrapper.AddParameter("@POL_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
							objDataWrapper.AddParameter("@VEHICLE_ID",objSqlParameter.Value.ToString());
							returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetPolAssgndDrvrType");
							objDataWrapper.ClearParameteres();
						}
						else
						{
							VehicleInfo+=";Motorcycle Number = " + objSqlParameter.Value.ToString() +  ", Make = " + dr["Make"].ToString() + ", Model =" + dr["Model"].ToString();
							strTransDesc="Motorcycle is copied";
						}
					}									
					objDataWrapper.ClearParameteres();

					//Update Default Coverages//////////////////////
			
					//Insert/delete relevant coverages for Auto*********************	
					if ( strCalledFrom == "PPA" )
					{
						//						int vehicleID = Convert.ToInt32(objSqlParameter.Value);
						//						objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
						//						objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
						//						objDataWrapper.AddParameter("@VEHICLE_ID",vehicleID);
						//						objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
						//
						//						objDataWrapper.ExecuteNonQuery("Proc_Update_POLICY_VEHICLE_COVERAGES");
						//						objDataWrapper.ClearParameteres();
						//
						//						//Ravindra (05-23-06) To update Endorsment's linked with Policy Level coverages
						//						Cms.BusinessLayer.BlApplication.ClsVehicleCoverages.UpdatePolicyEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID ,objVehicleInfo.POLICY_VERSION_ID ,vehicleID,objDataWrapper);
						//						////////////////////
						//						///
						//						//Update Driver endorsements
						//						Cms.BusinessLayer.BlApplication.ClsDriverDetail objDriver = new ClsDriverDetail();
						//						// CHECK THIS AGAIN
						//						objDriver.UpdatePolicyDriverEndorsements(objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,objDataWrapper);
						//						objDataWrapper.ClearParameteres();
						ClsVehicleCoverages objCoverage=new ClsVehicleCoverages();
						objCoverage.SaveDefaultCoveragesPolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID ,objVehicleInfo.POLICY_VERSION_ID,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();
						objCoverage.UpdateCoveragesByRulePolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID,objVehicleInfo.POLICY_VERSION_ID,RuleType.AutoDriverDep,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();

					}
					else if ( strCalledFrom == "MOT" )
					{
						//						objDataWrapper.AddParameter("@POLICY_ID",objVehicleInfo.POLICY_ID);
						//						objDataWrapper.AddParameter("@POLICY_VERSION_ID",objVehicleInfo.POLICY_VERSION_ID);
						//						objDataWrapper.AddParameter("@VEHICLE_ID",Convert.ToInt32(objSqlParameter.Value));
						//						objDataWrapper.AddParameter("@CUSTOMER_ID",objVehicleInfo.CUSTOMER_ID);
						//
						//						objDataWrapper.ExecuteNonQuery("Proc_Update_MOTORCYCLE_COVERAGES_POLICY");
						ClsVehicleCoverages objCoverage=new ClsVehicleCoverages("MOTOR");
						objCoverage.SaveDefaultCoveragesPolicy(objDataWrapper,objVehicleInfo.CUSTOMER_ID,objVehicleInfo.POLICY_ID ,objVehicleInfo.POLICY_VERSION_ID,Convert.ToInt32(objSqlParameter.Value));
						objDataWrapper.ClearParameteres();
					}

				
					
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					objDataWrapper.ClearParameteres();
									
				}	
					
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}

		}

		// END 2

		//Modified By Ravindra(03-27-2006)
		//Changed Signature of methos previously passing Model object for VehicleInfo

		public int GetDriverCountForAssignedVehiclePolicy(int intCustomerID,int intPolicyID,
			int intPolicyVerID,int intVehicleID, string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_GetDriverCountForAssignedVehiclePolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",intCustomerID);
				objDataWrapper.AddParameter("@POL_ID",intPolicyID );
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",intPolicyVerID );
				objDataWrapper.AddParameter("@VEHICLE_ID",intVehicleID);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN",null,SqlDbType.Int ,ParameterDirection.ReturnValue);				
				objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();
				int returnResult = Convert.ToInt32(objSqlParameter.Value);
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
		//Changes By Ravindra Ends Here
		#endregion	

		# region Motorcycle Policy 
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objOldVehicleInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddPolicyMotorCycle(ClsPolicyVehicleInfo objOldVehicleInfo)
		{
			string		strStoredProc	=	"Proc_InsertPolicyMotorcycleInfo";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			//ClsAutoIDInfo objAutoIDInfo	=	new ClsAutoIDInfo();
			//PopulateAutoIDInfo(objAutoIDInfo,objOldVehicleInfo);

			//You are not passing the foreign key values from fromt end
			//LIke customer id and vehilcle id

			try
			{
				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);			
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER); //Insured Vehicle No
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR); // Year of Registration
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE); //Make
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL); // Model
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN); // VIN/Engine No
			
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				if (objOldVehicleInfo.AMOUNT==-1)
				{
					objDataWrapper.AddParameter("@AMOUNT",System.DBNull.Value);	 // Cost of Vehicle				
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				
				if(objOldVehicleInfo.VEHICLE_AGE!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE); // Age of Vehicle
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_AGE",0);
				}

				if(objOldVehicleInfo.VEHICLE_CC!=0)
				{
					objDataWrapper.AddParameter("@VEHICLE_CC",objOldVehicleInfo.VEHICLE_CC);
				}
				else
				{
					objDataWrapper.AddParameter("@VEHICLE_CC",null); // Vehicle Capacity
				}
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE); // Type of Vehicle

				objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objOldVehicleInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);

                //Added by Agniswar for Singapore Implementation
                objDataWrapper.AddParameter("@CHASIS_NUMBER", objOldVehicleInfo.CHASIS_NUMBER); // Chasis Number
                objDataWrapper.AddParameter("@TRANSMISSION_TYPE", objOldVehicleInfo.TRANSMISSION_TYPE); // Trasmission Type
                objDataWrapper.AddParameter("@FUEL_TYPE", objOldVehicleInfo.FUEL_TYPE); // Fuel Type
                objDataWrapper.AddParameter("@TOTAL_DRIVERS", objOldVehicleInfo.TOTAL_DRIVERS); // No of Passengers
                objDataWrapper.AddParameter("@BODY_TYPE", objOldVehicleInfo.BODY_TYPE); // Paint Type
                objDataWrapper.AddParameter("@REGN_PLATE_NUMBER", objOldVehicleInfo.REGN_PLATE_NUMBER); // Registration Number
                objDataWrapper.AddParameter("@RISK_CURRENCY", objOldVehicleInfo.RISK_CURRENCY); // RISK_CURRENCY 
                objDataWrapper.AddParameter("@VEHICLE_COVERAGE", objOldVehicleInfo.VEHICLE_COVERAGE); // VEHICLE_COVERAGE

				//added by vj on 18-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",objOldVehicleInfo.APP_VEHICLE_CLASS);
				//added by shafi 07-02-06
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				if(objOldVehicleInfo.CYCL_REGD_ROAD_USE==-1)
					objDataWrapper.AddParameter("@CYCL_REGD_ROAD_USE",null);
				else
					objDataWrapper.AddParameter("@CYCL_REGD_ROAD_USE",objOldVehicleInfo.CYCL_REGD_ROAD_USE);
				objDataWrapper.AddParameter("@COMPRH_ONLY",objOldVehicleInfo.COMPRH_ONLY);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID,SqlDbType.Int ,ParameterDirection.Output);

				int returnResult = 0;
				int  VEHICLE_ID;
				if(TransactionLogRequired)
				{
					int stateID = ClsVehicleInformation.GetStateIdForpolicy(objOldVehicleInfo.CUSTOMER_ID,Convert.ToString(objOldVehicleInfo.POLICY_ID),Convert.ToString(objOldVehicleInfo.POLICY_VERSION_ID));
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\Motorcycle\PolicyMotorCycleInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldVehicleInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID = objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.POLICY_ID = objOldVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objOldVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Policy Motorcycle is Added";
					if(stateID != 14)
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='COMPRH_ONLY']");
					}
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					//Executing the query to save vehicle info
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());					
					objDataWrapper.ClearParameteres();
					// inserting transaction log
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					VEHICLE_ID		=	int.Parse(objSqlParameter.Value.ToString());
				}

				//Added by Manoj Rathore on 27th May 2009 Itrack # 5799
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",VEHICLE_ID);
				returnResult	= objDataWrapper.ExecuteNonQuery("Proc_SetPolAssgndDrvrType");

				objDataWrapper.ClearParameteres();				
			
				//Update coverages/////////////			
				//ClsVehicleCoverages objCov = new ClsVehicleCoverages();
				//objCov.UpdatePolicyMotorcycleCoverages(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,VEHICLE_ID,objDataWrapper);
				ClsVehicleCoverages objCov = new ClsVehicleCoverages("MOTOR");
				objCov.SaveDefaultCoveragesPolicy(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID ,objOldVehicleInfo.POLICY_VERSION_ID,VEHICLE_ID);
				
				//Update coverages//////////////

				
				if(VEHICLE_ID > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				return VEHICLE_ID;
				
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

		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldOldVehicleInfo">Model object having old information</param>
		/// <param name="objOldVehicleInfo">Model object having new information(form control's value)</param>
		/// 		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicyMotorcycle(ClsPolicyVehicleInfo objOldOldVehicleInfo,ClsPolicyVehicleInfo objOldVehicleInfo)
		{
			string strTranXML;
			int returnResult = 0;
			
			/*
			ClsAutoIDInfo	objOldAutoIdInfo = new ClsAutoIDInfo();
			ClsAutoIDInfo	objNewAutoIdInfo = new ClsAutoIDInfo();
			PopulateAutoIDInfo(objNewAutoIdInfo,objOldVehicleInfo);
			objNewAutoIdInfo.VEHICLE_ID		=	objOldVehicleInfo.VEHICLE_ID;
			string strXML;
			ClsAutoIDInformation objAutoIDInformation = new ClsAutoIDInformation();
			DataSet dsOldAutoIDInfo	= objAutoIDInformation.FillAdditionalInterestDetails(objOldVehicleInfo.CUSTOMER_ID,
				objOldVehicleInfo.APP_ID,objOldVehicleInfo.APP_VERSION_ID,objOldVehicleInfo.VEHICLE_ID,out strXML);
			//populate old autoid info from dataset
			populateAutoIDInfoDataSet(objOldAutoIdInfo,dsOldAutoIDInfo);
			objOldAutoIdInfo.MODIFIED_BY = objOldVehicleInfo.MODIFIED_BY;
			objOldAutoIdInfo.LAST_UPDATED_DATETIME = objOldOldVehicleInfo.LAST_UPDATED_DATETIME;
			objOldAutoIdInfo.IS_ACTIVE			=	"Y";
			*/	

			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string		strStoredProc	=	"Proc_UpdatePolicyMotorcycleInformation";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",objOldVehicleInfo.INSURED_VEH_NUMBER);
				objDataWrapper.AddParameter("@VEHICLE_YEAR",objOldVehicleInfo.VEHICLE_YEAR);
				objDataWrapper.AddParameter("@MAKE",objOldVehicleInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objOldVehicleInfo.MODEL);
				objDataWrapper.AddParameter("@VIN",objOldVehicleInfo.VIN);
				
				objDataWrapper.AddParameter("@GRG_ADD1",objOldVehicleInfo.GRG_ADD1);
				objDataWrapper.AddParameter("@GRG_ADD2",objOldVehicleInfo.GRG_ADD2);
				objDataWrapper.AddParameter("@GRG_CITY",objOldVehicleInfo.GRG_CITY);
				objDataWrapper.AddParameter("@GRG_COUNTRY",objOldVehicleInfo.GRG_COUNTRY);
				objDataWrapper.AddParameter("@GRG_STATE",objOldVehicleInfo.GRG_STATE);
				objDataWrapper.AddParameter("@GRG_ZIP",objOldVehicleInfo.GRG_ZIP);
				objDataWrapper.AddParameter("@REGISTERED_STATE",objOldVehicleInfo.REGISTERED_STATE);
				objDataWrapper.AddParameter("@TERRITORY",objOldVehicleInfo.TERRITORY);
				if (objOldVehicleInfo.AMOUNT==-1)
				{
					objDataWrapper.AddParameter("@AMOUNT",System.DBNull.Value);					
				}
				else
				{
					objDataWrapper.AddParameter("@AMOUNT",objOldVehicleInfo.AMOUNT);
				}
				objDataWrapper.AddParameter("@VEHICLE_AGE",objOldVehicleInfo.VEHICLE_AGE);
				objDataWrapper.AddParameter("@VEHICLE_CC",objOldVehicleInfo.VEHICLE_CC);
				objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",objOldVehicleInfo.MOTORCYCLE_TYPE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objOldVehicleInfo.IS_ACTIVE);			
				objDataWrapper.AddParameter("@MODIFIED_BY",objOldVehicleInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objOldVehicleInfo.LAST_UPDATED_DATETIME);

                //Added by Agniswar for Singapore Implementation
                objDataWrapper.AddParameter("@CHASIS_NUMBER", objOldVehicleInfo.CHASIS_NUMBER); // Chasis Number
                objDataWrapper.AddParameter("@TRANSMISSION_TYPE", objOldVehicleInfo.TRANSMISSION_TYPE); // Trasmission Type
                objDataWrapper.AddParameter("@FUEL_TYPE", objOldVehicleInfo.FUEL_TYPE); // Fuel Type
                objDataWrapper.AddParameter("@TOTAL_DRIVERS", objOldVehicleInfo.TOTAL_DRIVERS); // No of Passengers
                objDataWrapper.AddParameter("@BODY_TYPE", objOldVehicleInfo.BODY_TYPE); // Paint Type
                objDataWrapper.AddParameter("@REGN_PLATE_NUMBER", objOldVehicleInfo.REGN_PLATE_NUMBER); // Registration Number
                objDataWrapper.AddParameter("@RISK_CURRENCY", objOldVehicleInfo.RISK_CURRENCY); // RISK_CURRENCY 
                objDataWrapper.AddParameter("@VEHICLE_COVERAGE", objOldVehicleInfo.VEHICLE_COVERAGE); // VEHICLE_COVERAGE

				//added by vj on 18-10-2005
				objDataWrapper.AddParameter("@APP_VEHICLE_CLASS",objOldVehicleInfo.APP_VEHICLE_CLASS);
				//Added By Shafi 07-02-06
				objDataWrapper.AddParameter("@SYMBOL",objOldVehicleInfo.SYMBOL);
				if(objOldVehicleInfo.CYCL_REGD_ROAD_USE==-1)
					objDataWrapper.AddParameter("@CYCL_REGD_ROAD_USE",null);
				else
					objDataWrapper.AddParameter("@CYCL_REGD_ROAD_USE",objOldVehicleInfo.CYCL_REGD_ROAD_USE);
				objDataWrapper.AddParameter("@COMPRH_ONLY",objOldVehicleInfo.COMPRH_ONLY);

				if(TransactionRequired) 
				{
					int stateID = ClsVehicleInformation.GetStateIdForpolicy(objOldVehicleInfo.CUSTOMER_ID,Convert.ToString(objOldVehicleInfo.POLICY_ID),Convert.ToString(objOldVehicleInfo.POLICY_VERSION_ID));
					objOldVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\Motorcycle\PolicyMotorCycleInformation.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldOldVehicleInfo,objOldVehicleInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objOldVehicleInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID 		=	objOldVehicleInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objOldVehicleInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objOldVehicleInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Policy Motorcycle is modified";
					if(stateID != 14)
					{
						strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='COMPRH_ONLY']");
					}
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.ClearParameteres();
				//Insert/delete relevant policy coverages*********************	
				// IF VEHICLE CHANGE FROM COMP ONLY YES TO NO -BY pRAVESH K CHANDEL ON 28 MAY 2009
				if((objOldOldVehicleInfo.COMPRH_ONLY!=objOldVehicleInfo.COMPRH_ONLY) && objOldVehicleInfo.COMPRH_ONLY==10964)
				{
					objDataWrapper.AddParameter("@CUSTOMER_ID",objOldVehicleInfo.CUSTOMER_ID);
					objDataWrapper.AddParameter("@POLICY_ID",objOldVehicleInfo.POLICY_ID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objOldVehicleInfo.POLICY_VERSION_ID);
					objDataWrapper.AddParameter("@VEHICLE_ID",objOldVehicleInfo.VEHICLE_ID);
					
					objDataWrapper.ExecuteNonQuery("Proc_UPDATE_POLICY_LEVEL_COVERAGES_POL");
					objDataWrapper.ClearParameteres();
				}
				//***********************
				//Update coverages/////////////			
				ClsVehicleCoverages objCov = new ClsVehicleCoverages("MOTOR");
				objCov.UpdateCoveragesByRulePolicy(objDataWrapper,objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID,objOldVehicleInfo.POLICY_VERSION_ID,RuleType.RiskDependent,objOldVehicleInfo.VEHICLE_ID);
				//Update coverages//////////////


				if(returnResult > 0)
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					ClsDriverDetail objDriverDetail = new ClsDriverDetail();
					objDriverDetail.UpdateMotorVehicleClassPOL(objOldVehicleInfo.CUSTOMER_ID,objOldVehicleInfo.POLICY_ID, objOldVehicleInfo.POLICY_VERSION_ID);				
				}
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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


		/// <summary>
		/// It will get the policy vehicle information
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="VehicleID"></param>
		/// <returns></returns>
		public static string FetchCycleXMLFromPolicyVehicleTable(int CustomerID,int PolicyID, int PolicyVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICYVERSIONID",PolicyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VEHICLEID",VehicleID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyCycleInformation");
			
				return dsTemp.GetXml();
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{}
		}

        //Added by Agniswar 
        public static DataSet FetchCycleInfoFromPolicyVehicleTable(int CustomerID, int PolicyID, int PolicyVersionID, int VehicleID)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMERID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICYID", PolicyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@VEHICLEID", VehicleID, SqlDbType.Int);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyCycleInformation");

                return dsTemp;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }

		/// <summary>
		/// It will fill the driver information for motorcycle lob.
		/// </summary>
		/// <param name="cmbCombo"></param>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		public static void FillPolicyMotorDriverVehicleInfo(System.Web.UI.WebControls.DropDownList cmbCombo,int customerID, int policyID, int policyVersionID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
				objDataWrapper.AddParameter("@POLICY_ID", policyID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", policyVersionID);

				DataTable  objDataTable = objDataWrapper.ExecuteDataSet("Proc_FillPolicyMotorVehicleDropDown").Tables[0];
				cmbCombo.DataSource = objDataTable;
				cmbCombo.DataTextField = "MODEL_MAKE";
				cmbCombo.DataValueField = "VEHICLE_ID";
				cmbCombo.DataBind();
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			finally
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
			}
		}
		/// <summary>
		/// Method to retrive policy vehicles specific to LOB,Customer & state.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// 
		// Added by Swastika on 24th Mar'06 for Pol Iss # 60
		public static DataTable FetchVehicleFromCustVehicleTablePolicy(int CustomerID,int PolID,int PolVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLID",PolID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLVERSIONID",PolVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustVehicleInfoPolicy");
				return dsTemp.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}
		// end
		
		/// <summary>
		/// Method to retrive policy vehicles specific to LOB,Customer & state.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// 
		// Added by Mohit Agarwal on 13th Nov '06 for Gen Iss # 3531
		public static DataTable FetchVehicleFromUmbVehicleTablePolicy(int CustomerID,int PolID,int PolVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLID",PolID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLVERSIONID",PolVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUmbVehicleInfoPolicy");
				return dsTemp.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{}
		}
		// end
		
			
		#endregion

		#region Save Function for Misc. Equipment Screen
		public int SaveMiscEquipDetails(System.Collections.ArrayList alMiscEquip)
		{
			
			string	strStoredProc =	"";
			int customerId=0;
			int appId=0;
			int appVersionId=0;
			int riskId=0;


			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			try
			{
				strStoredProc =	"Proc_InsertAPP_MISCELLANEOUS_EQUIPMENT_VALUES";
				for(int i = 0; i < alMiscEquip.Count; i++ )
				{
					Cms.Model.Application.clsAppMiscEqptValuesInfo objNew = (Cms.Model.Application.clsAppMiscEqptValuesInfo)alMiscEquip[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					//objWrapper.AddParameter("@ITEM_ID",objNew.ITEM_ID);
					//Added by Sibin on 11 Dec 08 for Itrack Issue 5114
					if(objNew.ITEM_ID == 0 || objNew.ITEM_ID == - 1) 
					{
						objWrapper.AddParameter("@ITEM_ID",DBNull.Value );
					}
					else
					{
						objWrapper.AddParameter("@ITEM_ID",objNew.ITEM_ID);
					}
					objWrapper.AddParameter("@ITEM_VALUE",objNew.ITEM_VALUE);
					objWrapper.AddParameter("@ITEM_DESCRIPTION",objNew.ITEM_DESCRIPTION);					
					objWrapper.AddParameter("@CREATED_BY",objNew.CREATED_BY);					
					objWrapper.AddParameter("@CREATED_DATETIME",objNew.CREATED_DATETIME);	
					customerId =objNew.CUSTOMER_ID;
					appId      =objNew.APP_ID ;
					appVersionId =objNew.APP_VERSION_ID ;
					riskId  =objNew.VEHICLE_ID;

					
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					//if (i==alMiscEquip.Count-1)
					if (objNew.ITEM_DESCRIPTION != null)
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AppMiscellaneousEquipmentValuesDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Miscellaneous Equipment added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
						objWrapper.ExecuteNonQuery(strStoredProc);					
					
					objWrapper.ClearParameteres();

				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);				
				throw(ex);
			} 		
			ClsVehicleCoverages objCoverages=new ClsVehicleCoverages();
			objCoverages.UpdateCoveragesByRuleApp(objWrapper,customerId,appId ,appVersionId,RuleType.MiscEquipment,riskId);

			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			
			return 1;
		}



		/// <summary>
		/// To delete Equipment Details from Application Level - Added by Sibin on 30 Oct 08
		/// </summary>
		/// <param name="alMiscEquip"></param>
		/// <returns></returns>
		public int DeleteMiscEquipDetails(System.Collections.ArrayList alMiscEquip)
		{
			
			string	strStoredProc =	"";
			int customerId=0;
			int appId=0;
			int appVersionId=0;
			int riskId=0;


			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			try
			{
				strStoredProc =	"Proc_DeleteAPP_MISCEQUIP_ROW";
				for(int i = 0; i < alMiscEquip.Count; i++ )
				{
					Cms.Model.Application.clsAppMiscEqptValuesInfo objNew = (Cms.Model.Application.clsAppMiscEqptValuesInfo)alMiscEquip[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ITEM_VALUE",objNew.ITEM_VALUE);
					objWrapper.AddParameter("@ITEM_DESCRIPTION",objNew.ITEM_DESCRIPTION);					
					customerId =objNew.CUSTOMER_ID;
					appId      =objNew.APP_ID ;
					appVersionId =objNew.APP_VERSION_ID ;
					riskId  =objNew.VEHICLE_ID;

					
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					//if (i==alMiscEquip.Count-1)
					if (objNew.ITEM_DESCRIPTION != null)
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/AppMiscellaneousEquipmentValuesDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Miscellaneous Equipment deleted.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
						objWrapper.ExecuteNonQuery(strStoredProc);					
					
					objWrapper.ClearParameteres();

				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);				
				throw(ex);
			} 		
			ClsVehicleCoverages objCoverages=new ClsVehicleCoverages();
			objCoverages.UpdateCoveragesByRuleApp(objWrapper,customerId,appId ,appVersionId,RuleType.MiscEquipment,riskId);

			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}

		public int SavePolMiscEquipDetails(System.Collections.ArrayList alMiscEquip)
		{
			
			string	strStoredProc =	"";
			int customerId=0;
			int polId=0;
			int polVersionId=0;
			int riskId=0;

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			try
			{
				strStoredProc =	"Proc_InsertPOL_MISCELLANEOUS_EQUIPMENT_VALUES";
				for(int i = 0; i < alMiscEquip.Count; i++ )
				{
					Cms.Model.Policy.clsPolMiscEqptValuesInfo objNew = (Cms.Model.Policy.clsPolMiscEqptValuesInfo)alMiscEquip[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);				
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					//objWrapper.AddParameter("@ITEM_ID",objNew.ITEM_ID);
					//Added by Sibin on 11 Dec 08 for Itrack Issue 5114
					if(objNew.ITEM_ID == 0 || objNew.ITEM_ID == - 1) 
					{
						objWrapper.AddParameter("@ITEM_ID",DBNull.Value );
					}
					else
					{
						objWrapper.AddParameter("@ITEM_ID",objNew.ITEM_ID);
					}
					objWrapper.AddParameter("@ITEM_VALUE",objNew.ITEM_VALUE);
					objWrapper.AddParameter("@ITEM_DESCRIPTION",objNew.ITEM_DESCRIPTION);					
					objWrapper.AddParameter("@CREATED_BY",objNew.CREATED_BY);					
					objWrapper.AddParameter("@CREATED_DATETIME",objNew.CREATED_DATETIME);
					customerId =objNew.CUSTOMER_ID;
					polId      =objNew.POLICY_ID ;
					polVersionId =objNew.POLICY_VERSION_ID ;
					riskId  =objNew.VEHICLE_ID;

					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if (objNew.ITEM_DESCRIPTION != null)
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolMiscellaneousEquipmentValuesDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Miscellaneous Equipment added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
						objWrapper.ExecuteNonQuery(strStoredProc);					
					
					objWrapper.ClearParameteres();
				}
			}

			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);				
				throw(ex);
			}
            objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			ClsVehicleCoverages objCoverages=new ClsVehicleCoverages();
			objCoverages.UpdateCoveragesByRulePolicy(objWrapper,customerId ,polId  ,polVersionId,RuleType.MiscEquipment,riskId);

			
			return 1;
		}
		

		/// <summary>
		/// To delete Equipment Details from Policy Level - Added by Sibin on 30 Oct 08
		/// </summary>
		/// <param name="alMiscEquip"></param>
		/// <returns></returns>
		public int DeletePolMiscEquipDetails(System.Collections.ArrayList alMiscEquip)
		{
			
			string	strStoredProc =	"";
			int customerId=0;
			int polId=0;
			int polVersionId=0;
			int riskId=0;

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			try
			{
				strStoredProc =	"Proc_DeletePOL_MISCEQUIP_ROW";
				for(int i = 0; i < alMiscEquip.Count; i++ )
				{
					Cms.Model.Policy.clsPolMiscEqptValuesInfo objNew = (Cms.Model.Policy.clsPolMiscEqptValuesInfo)alMiscEquip[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					//objWrapper.AddParameter("@ITEM_ID",objNew.ITEM_ID);
					objWrapper.AddParameter("@ITEM_VALUE",objNew.ITEM_VALUE);
					objWrapper.AddParameter("@ITEM_DESCRIPTION",objNew.ITEM_DESCRIPTION);					
					customerId =objNew.CUSTOMER_ID;
					polId     =objNew.POLICY_ID ;
					polVersionId =objNew.POLICY_VERSION_ID ;
					riskId  =objNew.VEHICLE_ID;
	
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if (objNew.ITEM_DESCRIPTION != null)
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolMiscellaneousEquipmentValuesDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Miscellaneous Equipment deleted.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					else
						objWrapper.ExecuteNonQuery(strStoredProc);					
					
					objWrapper.ClearParameteres();

				}
			}

			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);				
				throw(ex);
			} 		
			ClsVehicleCoverages objCoverages=new ClsVehicleCoverages();
			objCoverages.UpdateCoveragesByRulePolicy(objWrapper,customerId ,polId  ,polVersionId,RuleType.MiscEquipment,riskId);

			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return 1;
		}

		public DataTable GetMiscEquipOldData(Cms.Model.Application.clsAppMiscEqptValuesInfo objAppMiscEqptValuesInfo)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objAppMiscEqptValuesInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objAppMiscEqptValuesInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@APP_ID",objAppMiscEqptValuesInfo.APP_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objAppMiscEqptValuesInfo.VEHICLE_ID);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAPP_MISCELLANEOUS_EQUIPMENT_VALUES");			
				if(dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
					return dsTemp.Tables[0];
				else
					return null;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		



		public DataTable GetPolMiscEquipOldData(Cms.Model.Policy.clsPolMiscEqptValuesInfo objPolMiscEqptValuesInfo)
		{
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objPolMiscEqptValuesInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objPolMiscEqptValuesInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objPolMiscEqptValuesInfo.POLICY_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objPolMiscEqptValuesInfo.VEHICLE_ID);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPOL_MISCELLANEOUS_EQUIPMENT_VALUES");			
				if(dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count>0)
					return dsTemp.Tables[0];
				else
					return null;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public static DataTable GetAppVehicle(DataWrapper objDataWrapper,int intCustomrId, int intAppId,int intAppVersionId, int intDriverId)
		{
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				DataSet dsTemp = new DataSet();	
				objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomrId);
				objDataWrapper.AddParameter("@APP_ID", intAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionId);
				objDataWrapper.AddParameter("@DRIVER_ID", intDriverId);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_FillAppVehicleDropDown");
				return dsTemp.Tables[0];
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
		}





		#endregion	
	
	}

}

