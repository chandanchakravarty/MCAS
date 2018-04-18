/******************************************************************************************
<Author				: -   Mohit
<Start Date				: -	05/06/2005 11:59:31 AM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Collections;
using Cms.DataLayer;
using Cms.BusinessLayer.BlClient;

namespace Cms.BusinessLayer.BlClient
{
	/// <summary>
	/// This class is used for coping data from the app_vehicles to the clt_customer_vehicles.
	/// </summary>
	public class ClsCustomerVehicle :ClsClient
	{
		public ClsCustomerVehicle()
		{
			//
			// TODO: Add constructor logic here
			//
		}
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
		///	 For coping data in App_Vehicles table from Clt_Customer_Vehicles and in
		///	 app_vehicle_coverages from clt_customer_vehicle_coverages.	 
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <returns></returns>
		public static void InsertVehicleFromCustVehicleTable(DataTable dtSelectedVehicles,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id,char coverage_To_Be_Copy)
		{
			string	strStoredProc = "Proc_InsertCustomerVehicleToAppVehicle";
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
		/// <summary>
		/// For coping only data in App_Vehicles table from Clt_Customer_Vehicles.
		/// </summary>
		/// <param name="arrSelectedVehicles"></param>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		public static void InsertVehicleFromCustVehicleTable(DataTable dtSelectedVehicles,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id)
		{
			string	strStoredProc = "Proc_InsertCustomerVehicleToAppVehicle";
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
	}
}
