using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.Xml;	




namespace Cms.BusinessLayer.BlApplication
{
	
	public class ClsUmbSchRecords : Cms.BusinessLayer.BlApplication.clsapplication
	{
		public const string CALLED_FROM_REC_VEH = "REC_VEH";
		public const string CALLED_FROM_DRIVER = "DRIVERS";
		public const string CALLED_FROM_LOCATIONS = "LOCATIONS";
		//Added at 21 dec 2006 by Manoj Rathore
		public const string CALLED_FROM_MOTORCYCLE = "MOTORCYCLE";
		//********
		public const string CALLED_FROM_VEHICLES = "VEHICLES";
		public const string CALLED_FROM_BOAT = "BOAT";
		public const string DRIVERS_PARENT_NODE = "DriversForCopy";
		public const string LOCATIONS_PARENT_NODE = "LocationsForCopy";
		public const string REC_VEH_PARENT_NODE = "RecVehiclesForCopy";
		public const string VEHICLES_PARENT_NODE = "VehiclesForCopy";
		public const string BOAT_PARENT_NODE = "BoatForCopy";
		public ClsUmbSchRecords()
		{
			
		}

		public static DataTable FetchDrivers(int CustomerID,int AppID, int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_DISTINCT_DRIVER_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  

		public static DataTable FetchPolDrivers(int CustomerID,int PolID, int PolVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_POL_DISTINCT_DRIVER_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  

		public int SaveAppUmbrellaVehiclesSOU(XmlDocument XmlDoc,string called_for)
		{
			DataWrapper objDataWrapper=null;
			try
			{		
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.VEHICLES_PARENT_NODE);
				if(xNode==null)
					return -1;				

				string strCustID = xNode.Attributes.Item(0).Value;	
				string strAppID  = xNode.Attributes.Item(1).Value;
				string strAppVerID  = xNode.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@APP_ID",strAppID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVerID);
					objDataWrapper.AddParameter("@VEHICLE_ID",null);
					objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",xNode.ChildNodes[i].SelectSingleNode("INSURED_VEH_NUMBER").InnerText.ToString());
					objDataWrapper.AddParameter("@VEHICLE_YEAR",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_YEAR").InnerText.ToString());
					objDataWrapper.AddParameter("@MAKE",xNode.ChildNodes[i].SelectSingleNode("MAKE").InnerText.ToString());
					objDataWrapper.AddParameter("@MODEL",xNode.ChildNodes[i].SelectSingleNode("MODEL").InnerText.ToString());
					objDataWrapper.AddParameter("@VIN",xNode.ChildNodes[i].SelectSingleNode("VIN").InnerText.ToString());
					objDataWrapper.AddParameter("@BODY_TYPE",xNode.ChildNodes[i].SelectSingleNode("BODY_TYPE").InnerText.ToString());
					objDataWrapper.AddParameter("@GRG_ADD1",xNode.ChildNodes[i].SelectSingleNode("GRG_ADD1").InnerText.ToString());
					objDataWrapper.AddParameter("@GRG_ADD2",xNode.ChildNodes[i].SelectSingleNode("GRG_ADD2").InnerText.ToString());
					objDataWrapper.AddParameter("@GRG_CITY",xNode.ChildNodes[i].SelectSingleNode("GRG_CITY").InnerText.ToString());
					objDataWrapper.AddParameter("@GRG_COUNTRY",xNode.ChildNodes[i].SelectSingleNode("GRG_COUNTRY").InnerText.ToString());
					objDataWrapper.AddParameter("@GRG_STATE",xNode.ChildNodes[i].SelectSingleNode("GRG_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@GRG_ZIP",xNode.ChildNodes[i].SelectSingleNode("GRG_ZIP").InnerText.ToString());
					objDataWrapper.AddParameter("@REGISTERED_STATE",xNode.ChildNodes[i].SelectSingleNode("REGISTERED_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@TERRITORY",xNode.ChildNodes[i].SelectSingleNode("TERRITORY").InnerText.ToString());
					objDataWrapper.AddParameter("@CLASS",xNode.ChildNodes[i].SelectSingleNode("CLASS").InnerText.ToString());					
					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",xNode.ChildNodes[i].SelectSingleNode("REGN_PLATE_NUMBER").InnerText.ToString());					
					objDataWrapper.AddParameter("@ST_AMT_TYPE",xNode.ChildNodes[i].SelectSingleNode("ST_AMT_TYPE").InnerText.ToString());					
					if(xNode.ChildNodes[i].SelectSingleNode("AMOUNT")!=null && xNode.ChildNodes[i].SelectSingleNode("AMOUNT").InnerText.ToString()!="")
						objDataWrapper.AddParameter("@AMOUNT",xNode.ChildNodes[i].SelectSingleNode("AMOUNT").InnerText.ToString());	
					objDataWrapper.AddParameter("@SYMBOL",xNode.ChildNodes[i].SelectSingleNode("SYMBOL").InnerText.ToString());
					if(xNode.ChildNodes[i].SelectSingleNode("VEHICLE_AGE")!=null && xNode.ChildNodes[i].SelectSingleNode("VEHICLE_AGE").InnerText.ToString()!="")
						objDataWrapper.AddParameter("@VEHICLE_AGE",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_AGE").InnerText.ToString());
					objDataWrapper.AddParameter("@CREATED_BY",xNode.ChildNodes[i].SelectSingleNode("CREATED_BY").InnerText.ToString());
					//objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);
					objDataWrapper.AddParameter("@MODIFIED_BY",null);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);										
					objDataWrapper.AddParameter("@USE_VEHICLE",xNode.ChildNodes[i].SelectSingleNode("USE_VEHICLE").InnerText.ToString());
					objDataWrapper.AddParameter("@CLASS_PER",xNode.ChildNodes[i].SelectSingleNode("CLASS_PER").InnerText.ToString());
					objDataWrapper.AddParameter("@CLASS_COM",xNode.ChildNodes[i].SelectSingleNode("CLASS_COM").InnerText.ToString());
					objDataWrapper.AddParameter("@VEHICLE_TYPE_PER",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_TYPE_PER").InnerText.ToString());
					objDataWrapper.AddParameter("@VEHICLE_TYPE_COM",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_TYPE_COM").InnerText.ToString());					
					objDataWrapper.AddParameter("@IS_OWN_LEASE",xNode.ChildNodes[i].SelectSingleNode("IS_OWN_LEASE").InnerText.ToString());	
					//Added by 21 dec 2006 by Manoj Rathore//
					if(called_for=="MOTOR")
						objDataWrapper.AddParameter("@CALLED_FOR","Motor Home");
					else
						 objDataWrapper.AddParameter("@CALLED_FOR","AUTO ");
					//*****************//
					if(xNode.ChildNodes[i].SelectSingleNode("PURCHASE_DATE")!=null && xNode.ChildNodes[i].SelectSingleNode("PURCHASE_DATE").InnerText.ToString()!="")
						objDataWrapper.AddParameter("@PURCHASE_DATE",xNode.ChildNodes[i].SelectSingleNode("PURCHASE_DATE").InnerText.ToString());	
					else
					objDataWrapper.AddParameter("@PURCHASE_DATE",null);				
					objDataWrapper.AddParameter("@IS_NEW_USED",xNode.ChildNodes[i].SelectSingleNode("IS_NEW_USED").InnerText.ToString());
					objDataWrapper.AddParameter("@VEHICLE_USE",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_USE").InnerText.ToString());
					objDataWrapper.AddParameter("@MULTI_CAR",xNode.ChildNodes[i].SelectSingleNode("MULTI_CAR").InnerText.ToString());
					//if(xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE")!=null && xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE").InnerText.ToString())!="")
					//	objDataWrapper.AddParameter("@ANNUAL_MILEAGE",xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE").InnerText.ToString());	
					//else
					//	objDataWrapper.AddParameter("@ANNUAL_MILEAGE",null);
					if(xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE")!=null && xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE").InnerText.ToString()!="")
						objDataWrapper.AddParameter("@ANNUAL_MILEAGE",xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE").InnerText.ToString());	
					else
						objDataWrapper.AddParameter("@ANNUAL_MILEAGE",null);
					objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",xNode.ChildNodes[i].SelectSingleNode("PASSIVE_SEAT_BELT").InnerText.ToString());
					objDataWrapper.AddParameter("@AIR_BAG",xNode.ChildNodes[i].SelectSingleNode("AIR_BAG").InnerText.ToString());
					objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",xNode.ChildNodes[i].SelectSingleNode("ANTI_LOCK_BRAKES").InnerText.ToString());
					objDataWrapper.AddParameter("@SAFETY_BELT",xNode.ChildNodes[i].SelectSingleNode("SAFETY_BELT").InnerText.ToString());					
					objDataWrapper.AddParameter("@MILES_TO_WORK",xNode.ChildNodes[i].SelectSingleNode("MILES_TO_WORK").InnerText.ToString());					
					objDataWrapper.AddParameter("@VEHICLE_TYPE",xNode.ChildNodes[i].SelectSingleNode("MOTORCYCLE_TYPE").InnerText.ToString());					
					//objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",xNode.ChildNodes[i].SelectSingleNode("UNINS_MOTOR_INJURY_COVE").InnerText.ToString());
					//objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",xNode.ChildNodes[i].SelectSingleNode("UNINS_PROPERTY_DAMAGE_COVE").InnerText.ToString());
					//objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",xNode.ChildNodes[i].SelectSingleNode("UNDERINS_MOTOR_INJURY_COVE").InnerText.ToString());

					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaVehicleInfo");
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 1;
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            
				return 0;
			}
			finally
			{}
		}


		public int SaveAppUmbrellaBoatsSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{		
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.BOAT_PARENT_NODE);
				if(xNode==null)
					return -1;				

				string strCustID = xNode.Attributes.Item(0).Value;	
				string strAppID  = xNode.Attributes.Item(1).Value;
				string strAppVerID  = xNode.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@APP_ID",strAppID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVerID);
					objDataWrapper.AddParameter("@BOAT_NO",null);
					objDataWrapper.AddParameter("@BOAT_NAME",null);
					objDataWrapper.AddParameter("@YEAR",xNode.ChildNodes[i].SelectSingleNode("YEAR").InnerText.ToString());
					objDataWrapper.AddParameter("@MAKE",xNode.ChildNodes[i].SelectSingleNode("MAKE").InnerText.ToString());
					objDataWrapper.AddParameter("@MODEL",xNode.ChildNodes[i].SelectSingleNode("MODEL").InnerText.ToString());
					objDataWrapper.AddParameter("@HULL_ID_NO",xNode.ChildNodes[i].SelectSingleNode("HULL_ID_NO").InnerText.ToString());
					objDataWrapper.AddParameter("@STATE_REG",null);
					objDataWrapper.AddParameter("@REG_NO",null);					
					objDataWrapper.AddParameter("@POWER",null);
					objDataWrapper.AddParameter("@HULL_TYPE",null);
					objDataWrapper.AddParameter("@HULL_MATERIAL",null);
					objDataWrapper.AddParameter("@FUEL_TYPE",xNode.ChildNodes[i].SelectSingleNode("FUEL_TYPE").InnerText.ToString());
					objDataWrapper.AddParameter("@HULL_DESIGN",null);

					objDataWrapper.AddParameter("@INCHES",xNode.ChildNodes[i].SelectSingleNode("INCHES").InnerText.ToString());
					objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",xNode.ChildNodes[i].SelectSingleNode("LORAN_NAV_SYSTEM").InnerText.ToString());
					objDataWrapper.AddParameter("@DIESEL_ENGINE",xNode.ChildNodes[i].SelectSingleNode("DIESEL_ENGINE").InnerText.ToString());
					objDataWrapper.AddParameter("@SHORE_STATION",xNode.ChildNodes[i].SelectSingleNode("SHORE_STATION").InnerText.ToString());
					

					objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",xNode.ChildNodes[i].SelectSingleNode("HALON_FIRE_EXT_SYSTEM").InnerText.ToString());
					objDataWrapper.AddParameter("@DUAL_OWNERSHIP",xNode.ChildNodes[i].SelectSingleNode("DUAL_OWNERSHIP").InnerText.ToString());
					objDataWrapper.AddParameter("@REMOVE_SAILBOAT",xNode.ChildNodes[i].SelectSingleNode("REMOVE_SAILBOAT").InnerText.ToString());															


					objDataWrapper.AddParameter("@LOCATION_ADDRESS",xNode.ChildNodes[i].SelectSingleNode("LOCATION_ADDRESS").InnerText.ToString());
					objDataWrapper.AddParameter("@LOCATION_CITY",xNode.ChildNodes[i].SelectSingleNode("LOCATION_CITY").InnerText.ToString());
					objDataWrapper.AddParameter("@LOCATION_STATE",xNode.ChildNodes[i].SelectSingleNode("LOCATION_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@LOCATION_ZIP",xNode.ChildNodes[i].SelectSingleNode("LOCATION_ZIP").InnerText.ToString());
					objDataWrapper.AddParameter("@COV_TYPE_BASIS",xNode.ChildNodes[i].SelectSingleNode("COV_TYPE_BASIS").InnerText.ToString());
			
					if(xNode.ChildNodes[i].SelectSingleNode("DATE_PURCHASED").InnerText.ToString()!="")
					{
						objDataWrapper.AddParameter("@DATE_PURCHASED",xNode.ChildNodes[i].SelectSingleNode("DATE_PURCHASED").InnerText.ToString());
					}
					objDataWrapper.AddParameter("@LENGTH",xNode.ChildNodes[i].SelectSingleNode("LENGTH").InnerText.ToString());
					if(xNode.ChildNodes[i].SelectSingleNode("MAX_SPEED")!=null && xNode.ChildNodes[i].SelectSingleNode("MAX_SPEED").InnerText.ToString()!="" && Double.Parse(xNode.ChildNodes[i].SelectSingleNode("MAX_SPEED").InnerText.ToString())!=0.0)
						objDataWrapper.AddParameter("@MAX_SPEED",xNode.ChildNodes[i].SelectSingleNode("MAX_SPEED").InnerText.ToString());
					else
						objDataWrapper.AddParameter("@MAX_SPEED",null);

					objDataWrapper.AddParameter("@COST_NEW",null);
					objDataWrapper.AddParameter("@PRESENT_VALUE",null);
					objDataWrapper.AddParameter("@BERTH_LOC",xNode.ChildNodes[i].SelectSingleNode("BERTH_LOC").InnerText.ToString());
					objDataWrapper.AddParameter("@WATERS_NAVIGATED",xNode.ChildNodes[i].SelectSingleNode("WATERS_NAVIGATED").InnerText.ToString());
					objDataWrapper.AddParameter("@TERRITORY",xNode.ChildNodes[i].SelectSingleNode("TERRITORY").InnerText.ToString());
					objDataWrapper.AddParameter("@CREATED_BY",xNode.ChildNodes[i].SelectSingleNode("CREATED_BY").InnerText.ToString());					
					objDataWrapper.AddParameter("@CREATED_DATETIME",System.DateTime.Now);
					if(xNode.ChildNodes[i].SelectSingleNode("INSURING_VALUE")!=null && xNode.ChildNodes[i].SelectSingleNode("INSURING_VALUE").InnerText.ToString()!="" && int.Parse(xNode.ChildNodes[i].SelectSingleNode("INSURING_VALUE").InnerText.ToString())!=0)
						objDataWrapper.AddParameter("@INSURING_VALUE",xNode.ChildNodes[i].SelectSingleNode("INSURING_VALUE").InnerText.ToString());
					else
						objDataWrapper.AddParameter("@INSURING_VALUE",null);
					
					if(xNode.ChildNodes[i].SelectSingleNode("WATERCRAFT_HORSE_POWER").InnerText.ToString()!="" &&int.Parse(xNode.ChildNodes[i].SelectSingleNode("WATERCRAFT_HORSE_POWER").InnerText.ToString())==0)
					{
						objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
					}
					else		 
						objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",xNode.ChildNodes[i].SelectSingleNode("WATERCRAFT_HORSE_POWER").InnerText.ToString());
					objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",xNode.ChildNodes[i].SelectSingleNode("TYPE_OF_WATERCRAFT").InnerText.ToString());
					objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",xNode.ChildNodes[i].SelectSingleNode("DESC_OTHER_WATERCRAFT").InnerText.ToString());
					objDataWrapper.AddParameter("@BOAT_ID",null);										

					objDataWrapper.AddParameter("@TWIN_SINGLE",xNode.ChildNodes[i].SelectSingleNode("TWIN_SINGLE").InnerText.ToString());					
					objDataWrapper.AddParameter("@USED_PARTICIPATE",null);
					objDataWrapper.AddParameter("@WATERCRAFT_CONTEST",null);										
					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaWatercraft");
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 1;
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            
				return 0;
			}
			finally
			{}
		}


		public int SavePolUmbrellaBoatsSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{		
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.BOAT_PARENT_NODE);
				if(xNode==null)
					return -1;				

				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
				string strPolID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@POLICY_ID",strPolID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",strVerID);				
					objDataWrapper.AddParameter("@BOAT_NO",null);
					objDataWrapper.AddParameter("@BOAT_NAME",null);
					objDataWrapper.AddParameter("@YEAR",xNode.ChildNodes[i].SelectSingleNode("YEAR").InnerText.ToString());
					objDataWrapper.AddParameter("@MAKE",xNode.ChildNodes[i].SelectSingleNode("MAKE").InnerText.ToString());
					objDataWrapper.AddParameter("@MODEL",xNode.ChildNodes[i].SelectSingleNode("MODEL").InnerText.ToString());
					objDataWrapper.AddParameter("@HULL_ID_NO",xNode.ChildNodes[i].SelectSingleNode("HULL_ID_NO").InnerText.ToString());
					objDataWrapper.AddParameter("@STATE_REG",null);
					//objDataWrapper.AddParameter("@REG_NO",null);					
					//objDataWrapper.AddParameter("@POWER",null);
					///objDataWrapper.AddParameter("@HULL_TYPE",null);
					objDataWrapper.AddParameter("@HULL_MATERIAL",null);
					objDataWrapper.AddParameter("@FUEL_TYPE",xNode.ChildNodes[i].SelectSingleNode("FUEL_TYPE").InnerText.ToString());
					//objDataWrapper.AddParameter("@HULL_DESIGN",null);

					objDataWrapper.AddParameter("@INCHES",xNode.ChildNodes[i].SelectSingleNode("INCHES").InnerText.ToString());
					objDataWrapper.AddParameter("@LORAN_NAV_SYSTEM",xNode.ChildNodes[i].SelectSingleNode("LORAN_NAV_SYSTEM").InnerText.ToString());
					objDataWrapper.AddParameter("@DIESEL_ENGINE",xNode.ChildNodes[i].SelectSingleNode("DIESEL_ENGINE").InnerText.ToString());
					objDataWrapper.AddParameter("@SHORE_STATION",xNode.ChildNodes[i].SelectSingleNode("SHORE_STATION").InnerText.ToString());
					

					objDataWrapper.AddParameter("@HALON_FIRE_EXT_SYSTEM",xNode.ChildNodes[i].SelectSingleNode("HALON_FIRE_EXT_SYSTEM").InnerText.ToString());
					objDataWrapper.AddParameter("@DUAL_OWNERSHIP",xNode.ChildNodes[i].SelectSingleNode("DUAL_OWNERSHIP").InnerText.ToString());
					objDataWrapper.AddParameter("@REMOVE_SAILBOAT",xNode.ChildNodes[i].SelectSingleNode("REMOVE_SAILBOAT").InnerText.ToString());															


					objDataWrapper.AddParameter("@LOCATION_ADDRESS",xNode.ChildNodes[i].SelectSingleNode("LOCATION_ADDRESS").InnerText.ToString());
					objDataWrapper.AddParameter("@LOCATION_CITY",xNode.ChildNodes[i].SelectSingleNode("LOCATION_CITY").InnerText.ToString());
					objDataWrapper.AddParameter("@LOCATION_STATE",xNode.ChildNodes[i].SelectSingleNode("LOCATION_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@LOCATION_ZIP",xNode.ChildNodes[i].SelectSingleNode("LOCATION_ZIP").InnerText.ToString());
					objDataWrapper.AddParameter("@COV_TYPE_BASIS",xNode.ChildNodes[i].SelectSingleNode("COV_TYPE_BASIS").InnerText.ToString());
			
					if(xNode.ChildNodes[i].SelectSingleNode("DATE_PURCHASED").InnerText.ToString()!="")
					{
						objDataWrapper.AddParameter("@DATE_PURCHASED",xNode.ChildNodes[i].SelectSingleNode("DATE_PURCHASED").InnerText.ToString());
					}
					objDataWrapper.AddParameter("@LENGTH",xNode.ChildNodes[i].SelectSingleNode("LENGTH").InnerText.ToString());
					if(xNode.ChildNodes[i].SelectSingleNode("MAX_SPEED")!=null && xNode.ChildNodes[i].SelectSingleNode("MAX_SPEED").InnerText.ToString()!="" && Double.Parse(xNode.ChildNodes[i].SelectSingleNode("MAX_SPEED").InnerText.ToString())!=0.0)
						objDataWrapper.AddParameter("@MAX_SPEED",xNode.ChildNodes[i].SelectSingleNode("MAX_SPEED").InnerText.ToString());
					else
						objDataWrapper.AddParameter("@MAX_SPEED",null);
					//objDataWrapper.AddParameter("@COST_NEW",null);
					//objDataWrapper.AddParameter("@PRESENT_VALUE",null);
					objDataWrapper.AddParameter("@BERTH_LOC",xNode.ChildNodes[i].SelectSingleNode("BERTH_LOC").InnerText.ToString());
					objDataWrapper.AddParameter("@WATERS_NAVIGATED",xNode.ChildNodes[i].SelectSingleNode("WATERS_NAVIGATED").InnerText.ToString());
					objDataWrapper.AddParameter("@TERRITORY",xNode.ChildNodes[i].SelectSingleNode("TERRITORY").InnerText.ToString());
					objDataWrapper.AddParameter("@CREATED_BY",xNode.ChildNodes[i].SelectSingleNode("CREATED_BY").InnerText.ToString());					
					objDataWrapper.AddParameter("@CREATED_DATETIME",System.DateTime.Now);
					if(xNode.ChildNodes[i].SelectSingleNode("INSURING_VALUE")!=null && xNode.ChildNodes[i].SelectSingleNode("INSURING_VALUE").InnerText.ToString()!="" && int.Parse(xNode.ChildNodes[i].SelectSingleNode("INSURING_VALUE").InnerText.ToString())!=0)
						objDataWrapper.AddParameter("@INSURING_VALUE",xNode.ChildNodes[i].SelectSingleNode("INSURING_VALUE").InnerText.ToString());
					else
						objDataWrapper.AddParameter("@INSURING_VALUE",null);
					
					if(xNode.ChildNodes[i].SelectSingleNode("WATERCRAFT_HORSE_POWER").InnerText.ToString()!="" &&int.Parse(xNode.ChildNodes[i].SelectSingleNode("WATERCRAFT_HORSE_POWER").InnerText.ToString())==0)
					{
						objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",null);
					}
					else		 
						objDataWrapper.AddParameter("@WATERCRAFT_HORSE_POWER",xNode.ChildNodes[i].SelectSingleNode("WATERCRAFT_HORSE_POWER").InnerText.ToString());
					objDataWrapper.AddParameter("@TYPE_OF_WATERCRAFT",xNode.ChildNodes[i].SelectSingleNode("TYPE_OF_WATERCRAFT").InnerText.ToString());
					objDataWrapper.AddParameter("@DESC_OTHER_WATERCRAFT",xNode.ChildNodes[i].SelectSingleNode("DESC_OTHER_WATERCRAFT").InnerText.ToString());
					objDataWrapper.AddParameter("@BOAT_ID",null);										

					objDataWrapper.AddParameter("@TWIN_SINGLE",xNode.ChildNodes[i].SelectSingleNode("TWIN_SINGLE").InnerText.ToString());					
					objDataWrapper.AddParameter("@USED_PARTICIPATE",null);
					objDataWrapper.AddParameter("@WATERCRAFT_CONTEST",null);										
					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyUmbrellaWatercraft");
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 1;
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            
				return 0;
			}
			finally
			{}
		}


		public int SavePolUmbrellaDriversSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{		
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.DRIVERS_PARENT_NODE);
				if(xNode==null)
					return -1;				

				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
				string strPolID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@POLICY_ID",strPolID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",strVerID);					
					objDataWrapper.AddParameter("@DRIVER_FNAME",xNode.ChildNodes[i].SelectSingleNode("DRIVER_FNAME").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_MNAME",xNode.ChildNodes[i].SelectSingleNode("DRIVER_MNAME").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_LNAME",xNode.ChildNodes[i].SelectSingleNode("DRIVER_LNAME").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_CODE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_CODE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_DOB",xNode.ChildNodes[i].SelectSingleNode("DRIVER_DOB").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_ADD1",xNode.ChildNodes[i].SelectSingleNode("DRIVER_ADD1").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_ADD2",xNode.ChildNodes[i].SelectSingleNode("DRIVER_ADD2").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_CITY",xNode.ChildNodes[i].SelectSingleNode("DRIVER_CITY").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_STATE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_ZIP",xNode.ChildNodes[i].SelectSingleNode("DRIVER_ZIP").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_COUNTRY",xNode.ChildNodes[i].SelectSingleNode("DRIVER_COUNTRY").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_HOME_PHONE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_BUSINESS_PHONE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_EXT",xNode.ChildNodes[i].SelectSingleNode("DRIVER_EXT").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_MOBILE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_MOBILE").InnerText.ToString());					
					objDataWrapper.AddParameter("@DRIVER_SSN",xNode.ChildNodes[i].SelectSingleNode("DRIVER_SSN").InnerText.ToString());					
					objDataWrapper.AddParameter("@DRIVER_SEX",xNode.ChildNodes[i].SelectSingleNode("DRIVER_SEX").InnerText.ToString());					
					objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_DRIV_TYPE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_LIC_STATE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_LIC_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@DATE_LICENSED",xNode.ChildNodes[i].SelectSingleNode("DATE_LICENSED").InnerText.ToString());
					objDataWrapper.AddParameter("@FORM_F95",xNode.ChildNodes[i].SelectSingleNode("FORM_F95").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",xNode.ChildNodes[i].SelectSingleNode("DRIVER_DRIV_LIC").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_MART_STAT",xNode.ChildNodes[i].SelectSingleNode("DRIVER_MART_STAT").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_ID",null);
					objDataWrapper.AddParameter("@INSERTUPDATE","I");					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaPolicyDriver");
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 1;
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            
				return 0;
			}
			finally
			{}
		}


		public int SaveUmbrellaDriversSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{		
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.DRIVERS_PARENT_NODE);
				if(xNode==null)
					return -1;				

				string strCustID = xNode.Attributes.Item(0).Value;	
				string strAppID  = xNode.Attributes.Item(1).Value;
				string strAppVerID  = xNode.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@APP_ID",strAppID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVerID);					
					objDataWrapper.AddParameter("@DRIVER_FNAME",xNode.ChildNodes[i].SelectSingleNode("DRIVER_FNAME").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_MNAME",xNode.ChildNodes[i].SelectSingleNode("DRIVER_MNAME").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_LNAME",xNode.ChildNodes[i].SelectSingleNode("DRIVER_LNAME").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_CODE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_CODE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_DOB",xNode.ChildNodes[i].SelectSingleNode("DRIVER_DOB").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_ADD1",xNode.ChildNodes[i].SelectSingleNode("DRIVER_ADD1").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_ADD2",xNode.ChildNodes[i].SelectSingleNode("DRIVER_ADD2").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_CITY",xNode.ChildNodes[i].SelectSingleNode("DRIVER_CITY").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_STATE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_ZIP",xNode.ChildNodes[i].SelectSingleNode("DRIVER_ZIP").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_COUNTRY",xNode.ChildNodes[i].SelectSingleNode("DRIVER_COUNTRY").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_HOME_PHONE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_BUSINESS_PHONE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_MOBILE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_MOBILE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_SSN",xNode.ChildNodes[i].SelectSingleNode("DRIVER_SSN").InnerText.ToString());					
					objDataWrapper.AddParameter("@DRIVER_SEX",xNode.ChildNodes[i].SelectSingleNode("DRIVER_SEX").InnerText.ToString());					
					objDataWrapper.AddParameter("@DRIVER_DRIV_TYPE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_DRIV_TYPE").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_LIC_STATE",xNode.ChildNodes[i].SelectSingleNode("DRIVER_LIC_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@DATE_LICENSED",xNode.ChildNodes[i].SelectSingleNode("DATE_LICENSED").InnerText.ToString());
					objDataWrapper.AddParameter("@FORM_F95",xNode.ChildNodes[i].SelectSingleNode("FORM_F95").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_DRIV_LIC",xNode.ChildNodes[i].SelectSingleNode("DRIVER_DRIV_LIC").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_MART_STAT",xNode.ChildNodes[i].SelectSingleNode("DRIVER_MART_STAT").InnerText.ToString());
					objDataWrapper.AddParameter("@DRIVER_ID",null);
					objDataWrapper.AddParameter("@INSERTUPDATE","I");
					
					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaDriverDetails");
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return 1;
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            
				return 0;
			}
			finally
			{}
		}


		public int SavePolUmbrellaVehiclesSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{		
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.VEHICLES_PARENT_NODE);
				if(xNode==null)
					return -1;				

				string strCustID = xNode.Attributes.Item(0).Value;	
				string strPolID  = xNode.Attributes.Item(1).Value;
				string strPolVerID  = xNode.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@POLICY_ID",strPolID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",strPolVerID);
					objDataWrapper.AddParameter("@VEHICLE_ID",null);
					objDataWrapper.AddParameter("@INSURED_VEH_NUMBER",(xNode.ChildNodes[i].SelectSingleNode("INSURED_VEH_NUMBER").InnerText.ToString()));
					objDataWrapper.AddParameter("@VEHICLE_YEAR",(xNode.ChildNodes[i].SelectSingleNode("VEHICLE_YEAR").InnerText.ToString()));
					objDataWrapper.AddParameter("@MAKE",(xNode.ChildNodes[i].SelectSingleNode("MAKE").InnerText.ToString()));
					objDataWrapper.AddParameter("@MODEL",(xNode.ChildNodes[i].SelectSingleNode("MODEL").InnerText.ToString()));
					objDataWrapper.AddParameter("@VIN",(xNode.ChildNodes[i].SelectSingleNode("VIN").InnerText.ToString()));
					objDataWrapper.AddParameter("@BODY_TYPE",(xNode.ChildNodes[i].SelectSingleNode("BODY_TYPE").InnerText.ToString()));
					objDataWrapper.AddParameter("@GRG_ADD1",(xNode.ChildNodes[i].SelectSingleNode("GRG_ADD1").InnerText.ToString()));
					objDataWrapper.AddParameter("@GRG_ADD2",(xNode.ChildNodes[i].SelectSingleNode("GRG_ADD2").InnerText.ToString()));
					objDataWrapper.AddParameter("@GRG_CITY",(xNode.ChildNodes[i].SelectSingleNode("GRG_CITY").InnerText.ToString()));
					objDataWrapper.AddParameter("@GRG_COUNTRY",(xNode.ChildNodes[i].SelectSingleNode("GRG_COUNTRY").InnerText.ToString()));
					objDataWrapper.AddParameter("@GRG_STATE",(xNode.ChildNodes[i].SelectSingleNode("GRG_STATE").InnerText.ToString()));
					objDataWrapper.AddParameter("@GRG_ZIP",(xNode.ChildNodes[i].SelectSingleNode("GRG_ZIP").InnerText.ToString()));
					objDataWrapper.AddParameter("@REGISTERED_STATE",(xNode.ChildNodes[i].SelectSingleNode("REGISTERED_STATE").InnerText.ToString()));
					objDataWrapper.AddParameter("@TERRITORY",(xNode.ChildNodes[i].SelectSingleNode("TERRITORY").InnerText.ToString()));
					objDataWrapper.AddParameter("@CLASS",(xNode.ChildNodes[i].SelectSingleNode("CLASS").InnerText.ToString()));					
					objDataWrapper.AddParameter("@REGN_PLATE_NUMBER",(xNode.ChildNodes[i].SelectSingleNode("REGN_PLATE_NUMBER").InnerText.ToString()));					
					objDataWrapper.AddParameter("@ST_AMT_TYPE",(xNode.ChildNodes[i].SelectSingleNode("ST_AMT_TYPE").InnerText.ToString()));					
					if(xNode.ChildNodes[i].SelectSingleNode("AMOUNT")!=null && xNode.ChildNodes[i].SelectSingleNode("AMOUNT").InnerText.ToString()!="")
						objDataWrapper.AddParameter("@AMOUNT",(xNode.ChildNodes[i].SelectSingleNode("AMOUNT").InnerText.ToString()));
					objDataWrapper.AddParameter("@SYMBOL",(xNode.ChildNodes[i].SelectSingleNode("SYMBOL").InnerText.ToString()));
					if(xNode.ChildNodes[i].SelectSingleNode("VEHICLE_AGE")!=null && xNode.ChildNodes[i].SelectSingleNode("VEHICLE_AGE").InnerText.ToString()!="")
						objDataWrapper.AddParameter("@VEHICLE_AGE",(xNode.ChildNodes[i].SelectSingleNode("VEHICLE_AGE").InnerText.ToString()));
					objDataWrapper.AddParameter("@CREATED_BY",(xNode.ChildNodes[i].SelectSingleNode("CREATED_BY").InnerText.ToString()));
					//objDataWrapper.AddParameter("@CREATED_DATETIME",objOldVehicleInfo.CREATED_DATETIME);					

					objDataWrapper.AddParameter("@APP_USE_VEHICLE_ID",(xNode.ChildNodes[i].SelectSingleNode("USE_VEHICLE").InnerText.ToString()));
					objDataWrapper.AddParameter("@APP_VEHICLE_PERCLASS_ID",(xNode.ChildNodes[i].SelectSingleNode("CLASS_PER").InnerText.ToString()));
					objDataWrapper.AddParameter("@APP_VEHICLE_COMCLASS_ID",(xNode.ChildNodes[i].SelectSingleNode("CLASS_COM").InnerText.ToString()));
					objDataWrapper.AddParameter("@APP_VEHICLE_PERTYPE_ID",(xNode.ChildNodes[i].SelectSingleNode("VEHICLE_TYPE_PER").InnerText.ToString()));
					objDataWrapper.AddParameter("@APP_VEHICLE_COMTYPE_ID",(xNode.ChildNodes[i].SelectSingleNode("VEHICLE_TYPE_COM").InnerText.ToString()));									


					objDataWrapper.AddParameter("@IS_OWN_LEASE",(xNode.ChildNodes[i].SelectSingleNode("IS_OWN_LEASE").InnerText.ToString()));					
					if(xNode.ChildNodes[i].SelectSingleNode("PURCHASE_DATE")!=null && (xNode.ChildNodes[i].SelectSingleNode("PURCHASE_DATE").InnerText.ToString())!="")
						objDataWrapper.AddParameter("@PURCHASE_DATE",(xNode.ChildNodes[i].SelectSingleNode("PURCHASE_DATE").InnerText.ToString()));	
					else
						objDataWrapper.AddParameter("@PURCHASE_DATE",null);
					objDataWrapper.AddParameter("@IS_NEW_USED",(xNode.ChildNodes[i].SelectSingleNode("IS_NEW_USED").InnerText.ToString()));
					objDataWrapper.AddParameter("@VEHICLE_USE",(xNode.ChildNodes[i].SelectSingleNode("VEHICLE_USE").InnerText.ToString()));
					objDataWrapper.AddParameter("@MULTI_CAR",(xNode.ChildNodes[i].SelectSingleNode("MULTI_CAR").InnerText.ToString()));
					if(xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE")!=null && (xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE").InnerText.ToString())!="")
						objDataWrapper.AddParameter("@ANNUAL_MILEAGE",(xNode.ChildNodes[i].SelectSingleNode("ANNUAL_MILEAGE").InnerText.ToString()));	
					else
						objDataWrapper.AddParameter("@ANNUAL_MILEAGE",null);
					objDataWrapper.AddParameter("@PASSIVE_SEAT_BELT",(xNode.ChildNodes[i].SelectSingleNode("PASSIVE_SEAT_BELT").InnerText.ToString()));
					objDataWrapper.AddParameter("@AIR_BAG",(xNode.ChildNodes[i].SelectSingleNode("AIR_BAG").InnerText.ToString()));
					//objDataWrapper.AddParameter("@ANTI_LOCK_BRAKES",(xNode.ChildNodes[i].SelectSingleNode("ANTI_LOCK_BRAKES").InnerText.ToString()));
					objDataWrapper.AddParameter("@SAFETY_BELT",(xNode.ChildNodes[i].SelectSingleNode("SAFETY_BELT").InnerText.ToString()));					
					objDataWrapper.AddParameter("@MILES_TO_WORK",(xNode.ChildNodes[i].SelectSingleNode("MILES_TO_WORK").InnerText.ToString()));					
					objDataWrapper.AddParameter("@MOTORCYCLE_TYPE",(xNode.ChildNodes[i].SelectSingleNode("MOTORCYCLE_TYPE").InnerText.ToString()));					
					//objDataWrapper.AddParameter("@UNINS_MOTOR_INJURY_COVE",xNode.ChildNodes[i].SelectSingleNode("UNINS_MOTOR_INJURY_COVE").InnerText.ToString()));
					//objDataWrapper.AddParameter("@UNINS_PROPERTY_DAMAGE_COVE",xNode.ChildNodes[i].SelectSingleNode("UNINS_PROPERTY_DAMAGE_COVE").InnerText.ToString()));
					//objDataWrapper.AddParameter("@UNDERINS_MOTOR_INJURY_COVE",xNode.ChildNodes[i].SelectSingleNode("UNDERINS_MOTOR_INJURY_COVE").InnerText.ToString()));

					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertPolicyUmbrellaVehicleInfo");
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return returnResult;
			}
			catch
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
            
				return 0;
			}
			finally
			{}
		}

//		public int SavePolUmbrellaDriversSOU(XmlDocument XmlDoc)
//		{
//			DataWrapper objDataWrapper=null;
//			try
//			{		
//				int returnResult;
//				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
//				string strPolID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
//				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;
//
//				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//
//				for (int i=0; i<XmlDoc.FirstChild.ChildNodes.Count;i++)
//				{
//					objDataWrapper.ClearParameteres();
//					DateTime	RecordDate		=	DateTime.Now;
//					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
//					objDataWrapper.AddParameter("@POLICY_ID",strPolID);
//					objDataWrapper.AddParameter("@POLICY_VERSION_ID",strVerID);						
//					objDataWrapper.AddParameter("@DRIVER_FNAME",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_FNAME").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_MNAME",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_MNAME").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_LNAME",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_LNAME").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_CODE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_CODE").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_DOB",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_DOB").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_ADD1",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ADD1").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_ADD2",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ADD2").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_CITY",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_CITY").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_STATE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_STATE").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_ZIP",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ZIP").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_COUNTRY",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_COUNTRY").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_HOME_PHONE").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_BUSINESS_PHONE").InnerXml));
//					//objDataWrapper.AddParameter("@DRIVER_EXT",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_EXT").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_MOBILE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_MOBILE").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_SSN",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_SSN").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_SEX",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_SEX").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_ID",0,SqlDbType.Int,ParameterDirection.Output);
//					objDataWrapper.AddParameter("@INSERTUPDATE","I");
//					
//					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaPolicyDriver");
//				}
//
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//
//				return 1;
//			}
//			catch(Exception exc)
//			{
//				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
//            
//				return 0;
//			}
//			finally
//			{}
//		}
		public static DataTable FetchLocations(int CustomerID,int AppID, int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_APP_LOCATIONS_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  
		public static DataTable FetchPolLocations(int CustomerID,int PolID, int PolVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID,SqlDbType.Int);				
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_POL_LOCATIONS_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  

		public static DataTable FetchRecVehicles(int CustomerID,int AppID, int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_DISTINCT_APP_REC_VEHICLES_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  
		public static DataTable FetchVehicles(int CustomerID,int AppID, int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_DISTINCT_APP_VEHICLES_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  
		public static DataTable FetchBoats(int CustomerID,int AppID, int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_DISTINCT_APP_BOATS_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  
		public static DataTable FetchPolBoats(int CUSTOMER_ID,int POLICY_ID, int POLICY_VERSION_ID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",POLICY_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_DISTINCT_POL_BOATS_FOR_UMBRELLA");
				return dsTemp.Tables[0];				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  
		public static DataTable FetchPolVehicles(int CUSTOMER_ID,int POLICY_ID, int POLICY_VERSION_ID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",POLICY_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_DISTINCT_POL_VEHICLES_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  
		public static DataTable FetchPolRecVehicles(int CustomerID,int PolID, int PolVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID,SqlDbType.Int);	
				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GET_DISTINCT_POL_REC_VEHICLES_FOR_UMBRELLA");
				return dsTemp.Tables[0];
				 
			}
			catch(Exception exc)
			{throw (exc);}
		}  

		public int SaveUmbrellaLocationsSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.LOCATIONS_PARENT_NODE);
				if(xNode==null)
					return -1;
//				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
//				string strAppID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
//				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;

				string strCustID = xNode.Attributes.Item(0).Value;	
				string strAppID  = xNode.Attributes.Item(1).Value;
				string strVerID  = xNode.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				//for (int i=0; i<XmlDoc.FirstChild.ChildNodes.Count;i++)
				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@APP_ID",strAppID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",strVerID);
					objDataWrapper.AddParameter("@ADDRESS_1",xNode.ChildNodes[i].SelectSingleNode("ADDRESS_1").InnerText.ToString());
					objDataWrapper.AddParameter("@ADDRESS_2",xNode.ChildNodes[i].SelectSingleNode("ADDRESS_2").InnerText.ToString());
					objDataWrapper.AddParameter("@CITY",xNode.ChildNodes[i].SelectSingleNode("CITY").InnerText.ToString());
					objDataWrapper.AddParameter("@STATE",xNode.ChildNodes[i].SelectSingleNode("STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@ZIPCODE",xNode.ChildNodes[i].SelectSingleNode("ZIPCODE").InnerText.ToString());
					objDataWrapper.AddParameter("@PHONE_NUMBER",xNode.ChildNodes[i].SelectSingleNode("PHONE_NUMBER").InnerText.ToString());
					objDataWrapper.AddParameter("@COUNTY",xNode.ChildNodes[i].SelectSingleNode("LOC_COUNTY").InnerText.ToString());
					objDataWrapper.AddParameter("@FAX_NUMBER",xNode.ChildNodes[i].SelectSingleNode("FAX_NUMBER").InnerText.ToString());
					objDataWrapper.AddParameter("@CREATED_BY",xNode.ChildNodes[i].SelectSingleNode("CREATED_BY").InnerText.ToString());
					//objDataWrapper.AddParameter("@BUSS_FARM_PURSUITS_DESC",xNode.ChildNodes[i].SelectSingleNode("BUSS_FARM_PURSUITS_DESC").InnerText.ToString());
					//objDataWrapper.AddParameter("@LOCATION_NUMBER",xNode.ChildNodes[i].SelectSingleNode("LOCATION_NUMBER").InnerText.ToString());					

					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_CopyAppUmbrellaSchLocations");
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}
		}


		public int SaveUmbrellaPolLocationsSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.LOCATIONS_PARENT_NODE);
				if(xNode==null)
					return -1;
				//				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
				//				string strAppID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
				//				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;

				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
				string strPolID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
				string strPolVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				//for (int i=0; i<XmlDoc.FirstChild.ChildNodes.Count;i++)
				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@POLICY_ID",strPolID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",strPolVerID);	
					objDataWrapper.AddParameter("@ADDRESS_1",xNode.ChildNodes[i].SelectSingleNode("ADDRESS_1").InnerText.ToString());
					objDataWrapper.AddParameter("@ADDRESS_2",xNode.ChildNodes[i].SelectSingleNode("ADDRESS_2").InnerText.ToString());
					objDataWrapper.AddParameter("@CITY",xNode.ChildNodes[i].SelectSingleNode("CITY").InnerText.ToString());
					objDataWrapper.AddParameter("@STATE",xNode.ChildNodes[i].SelectSingleNode("STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@ZIPCODE",xNode.ChildNodes[i].SelectSingleNode("ZIPCODE").InnerText.ToString());
					objDataWrapper.AddParameter("@PHONE_NUMBER",xNode.ChildNodes[i].SelectSingleNode("PHONE_NUMBER").InnerText.ToString());
					objDataWrapper.AddParameter("@FAX_NUMBER",xNode.ChildNodes[i].SelectSingleNode("FAX_NUMBER").InnerText.ToString());
					objDataWrapper.AddParameter("@CREATED_BY",xNode.ChildNodes[i].SelectSingleNode("CREATED_BY").InnerText.ToString());
					//objDataWrapper.AddParameter("@LOCATION_NUMBER",xNode.ChildNodes[i].SelectSingleNode("LOCATION_NUMBER").InnerText.ToString());					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_CopyPolUmbrellaSchLocations");
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}
		}

		

		public int SaveUmbrellaRecVehiclesSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode(ClsUmbSchRecords.REC_VEH_PARENT_NODE);
				if(xNode==null)
					return -1;
				//				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
				//				string strAppID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
				//				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;

				string strCustID = xNode.Attributes.Item(0).Value;	
				string strAppID  = xNode.Attributes.Item(1).Value;
				string strVerID  = xNode.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				//for (int i=0; i<XmlDoc.FirstChild.ChildNodes.Count;i++)
				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@APP_ID",strAppID);
					objDataWrapper.AddParameter("@APP_VERSION_ID",strVerID);					
					objDataWrapper.AddParameter("@COMPANY_ID_NUMBER",xNode.ChildNodes[i].SelectSingleNode("COMPANY_ID_NUMBER").InnerText.ToString());
					objDataWrapper.AddParameter("@YEAR",xNode.ChildNodes[i].SelectSingleNode("YEAR").InnerText.ToString());
					objDataWrapper.AddParameter("@MAKE",xNode.ChildNodes[i].SelectSingleNode("MAKE").InnerText.ToString());
					objDataWrapper.AddParameter("@MODEL",xNode.ChildNodes[i].SelectSingleNode("MODEL").InnerText.ToString());
					objDataWrapper.AddParameter("@SERIAL",xNode.ChildNodes[i].SelectSingleNode("SERIAL").InnerText.ToString());
					objDataWrapper.AddParameter("@STATE_REGISTERED",xNode.ChildNodes[i].SelectSingleNode("STATE_REGISTERED").InnerText.ToString());
					objDataWrapper.AddParameter("@MANUFACTURER_DESC",xNode.ChildNodes[i].SelectSingleNode("MANUFACTURER_DESC").InnerText.ToString());
					objDataWrapper.AddParameter("@HORSE_POWER",xNode.ChildNodes[i].SelectSingleNode("HORSE_POWER").InnerText.ToString());
					objDataWrapper.AddParameter("@DISPLACEMENT",xNode.ChildNodes[i].SelectSingleNode("DISPLACEMENT").InnerText.ToString());
					objDataWrapper.AddParameter("@REMARKS",xNode.ChildNodes[i].SelectSingleNode("REMARKS").InnerText.ToString());
					objDataWrapper.AddParameter("@USED_IN_RACE_SPEED",xNode.ChildNodes[i].SelectSingleNode("USED_IN_RACE_SPEED").InnerText.ToString());
					objDataWrapper.AddParameter("@PRIOR_LOSSES",xNode.ChildNodes[i].SelectSingleNode("PRIOR_LOSSES").InnerText.ToString());
					objDataWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",xNode.ChildNodes[i].SelectSingleNode("IS_UNIT_REG_IN_OTHER_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",xNode.ChildNodes[i].SelectSingleNode("RISK_DECL_BY_OTHER_COMP").InnerText.ToString());
					objDataWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",xNode.ChildNodes[i].SelectSingleNode("DESC_RISK_DECL_BY_OTHER_COMP").InnerText.ToString());
					objDataWrapper.AddParameter("@VEHICLE_MODIFIED",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_MODIFIED").InnerText.ToString());
					objDataWrapper.AddParameter("@VEHICLE_TYPE",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_TYPE").InnerText.ToString());					
					objDataWrapper.AddParameter("@CREATED_BY",xNode.ChildNodes[i].SelectSingleNode("CREATED_BY").InnerText.ToString());
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_CopyAppUmbrellaSchRecVehicles");
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}
		}


		public int SavePolUmbrellaPolRecVehiclesSOU(XmlDocument XmlDoc)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int returnResult=-1;
				XmlNode xNode= XmlDoc.SelectSingleNode("RecVehiclesForCopy");
				if(xNode==null)
					return -1;
				//				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
				//				string strAppID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
				//				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;

				string strCustID = xNode.Attributes.Item(0).Value;	
				string strPolID  = xNode.Attributes.Item(1).Value;
				string strPolVerID  = xNode.Attributes.Item(2).Value;


				if(!xNode.HasChildNodes)
					return 1;

				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

				//for (int i=0; i<XmlDoc.FirstChild.ChildNodes.Count;i++)
				for (int i=0; i<xNode.ChildNodes.Count;i++)
				{
					objDataWrapper.ClearParameteres();
					DateTime	RecordDate		=	DateTime.Now;
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
					objDataWrapper.AddParameter("@POLICY_ID",strPolID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",strPolVerID);					
					objDataWrapper.AddParameter("@COMPANY_ID_NUMBER",xNode.ChildNodes[i].SelectSingleNode("COMPANY_ID_NUMBER").InnerText.ToString());
					objDataWrapper.AddParameter("@YEAR",xNode.ChildNodes[i].SelectSingleNode("YEAR").InnerText.ToString());
					objDataWrapper.AddParameter("@MAKE",xNode.ChildNodes[i].SelectSingleNode("MAKE").InnerText.ToString());
					objDataWrapper.AddParameter("@MODEL",xNode.ChildNodes[i].SelectSingleNode("MODEL").InnerText.ToString());
					objDataWrapper.AddParameter("@SERIAL",xNode.ChildNodes[i].SelectSingleNode("SERIAL").InnerText.ToString());
					objDataWrapper.AddParameter("@STATE_REGISTERED",xNode.ChildNodes[i].SelectSingleNode("STATE_REGISTERED").InnerText.ToString());
					objDataWrapper.AddParameter("@MANUFACTURER_DESC",xNode.ChildNodes[i].SelectSingleNode("MANUFACTURER_DESC").InnerText.ToString());
					objDataWrapper.AddParameter("@HORSE_POWER",xNode.ChildNodes[i].SelectSingleNode("HORSE_POWER").InnerText.ToString());
					objDataWrapper.AddParameter("@DISPLACEMENT",xNode.ChildNodes[i].SelectSingleNode("DISPLACEMENT").InnerText.ToString());
					objDataWrapper.AddParameter("@REMARKS",xNode.ChildNodes[i].SelectSingleNode("REMARKS").InnerText.ToString());
					objDataWrapper.AddParameter("@USED_IN_RACE_SPEED",xNode.ChildNodes[i].SelectSingleNode("USED_IN_RACE_SPEED").InnerText.ToString());
					objDataWrapper.AddParameter("@PRIOR_LOSSES",xNode.ChildNodes[i].SelectSingleNode("PRIOR_LOSSES").InnerText.ToString());
					objDataWrapper.AddParameter("@IS_UNIT_REG_IN_OTHER_STATE",xNode.ChildNodes[i].SelectSingleNode("IS_UNIT_REG_IN_OTHER_STATE").InnerText.ToString());
					objDataWrapper.AddParameter("@RISK_DECL_BY_OTHER_COMP",xNode.ChildNodes[i].SelectSingleNode("RISK_DECL_BY_OTHER_COMP").InnerText.ToString());
					objDataWrapper.AddParameter("@DESC_RISK_DECL_BY_OTHER_COMP",xNode.ChildNodes[i].SelectSingleNode("DESC_RISK_DECL_BY_OTHER_COMP").InnerText.ToString());
					objDataWrapper.AddParameter("@VEHICLE_MODIFIED",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_MODIFIED").InnerText.ToString());
					objDataWrapper.AddParameter("@VEHICLE_TYPE",xNode.ChildNodes[i].SelectSingleNode("VEHICLE_TYPE").InnerText.ToString());					
					objDataWrapper.AddParameter("@CREATED_BY",xNode.ChildNodes[i].SelectSingleNode("CREATED_BY").InnerText.ToString());					
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_CopyPolUmbrellaSchRecVehicles");
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{}
		}


//		public int SaveUmbrellaDriversSOU(XmlDocument XmlDoc)
//		{
//			DataWrapper objDataWrapper=null;
//			try
//			{		
//				int returnResult=-1;
//				string strCustID = XmlDoc.FirstChild.Attributes.Item(0).Value;
//				string strAppID  = XmlDoc.FirstChild.Attributes.Item(1).Value;
//				string strVerID  = XmlDoc.FirstChild.Attributes.Item(2).Value;
//
//				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//
//				for (int i=0; i<XmlDoc.FirstChild.ChildNodes.Count;i++)
//				{
//					objDataWrapper.ClearParameteres();
//					DateTime	RecordDate		=	DateTime.Now;
//					objDataWrapper.AddParameter("@CUSTOMER_ID",strCustID);
//					objDataWrapper.AddParameter("@APP_ID",strAppID);
//					objDataWrapper.AddParameter("@APP_VERSION_ID",strVerID);	
//					objDataWrapper.AddParameter("@DRIVER_FNAME",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_FNAME").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_MNAME",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_MNAME").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_LNAME",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_LNAME").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_CODE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_CODE").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_DOB",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_DOB").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_ADD1",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ADD1").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_ADD2",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ADD2").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_CITY",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_CITY").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_STATE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_STATE").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_ZIP",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_ZIP").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_COUNTRY",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_COUNTRY").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_HOME_PHONE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_HOME_PHONE").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_BUSINESS_PHONE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_BUSINESS_PHONE").InnerXml));
//					//objDataWrapper.AddParameter("@DRIVER_EXT",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_EXT").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_MOBILE",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_MOBILE").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_SSN",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_SSN").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_SEX",(XmlDoc.SelectSingleNode("DriversForCopy").SelectSingleNode("Driver").SelectSingleNode("DRIVER_SEX").InnerXml));
//					objDataWrapper.AddParameter("@DRIVER_ID",0,SqlDbType.Int,ParameterDirection.Output);
//					objDataWrapper.AddParameter("@INSERTUPDATE","I");
//					
//					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_InsertUmbrellaDriverDetails");
//				}
//
//				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				return returnResult;
//			}
//			catch(Exception exc)
//			{
//				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
//            
//				return 0;
//			}
//			finally
//			{}
//		}

		public void InsertExistingDriver(DataTable dtSelectedDriver,int from_Customer_ID,int from_App_ID,int from_App_Version_ID,int from_User_Id, string strCalledFrom)
		{			
			string	strStoredProc = "Proc_InsertScheduleOfUnderlyingDrivers";
			string DriverInfo = "";
			int returnResult;

			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{				
				for(int i=0;i < dtSelectedDriver.Rows.Count;i++)
				{
					DataRow dr=dtSelectedDriver.Rows[i];					
					//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@FROM_CUSTOMER_ID",int.Parse(dr["CustomerID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_ID",int.Parse(dr["AppID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_APP_VERSION_ID",int.Parse(dr["AppVersionID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_CUSTOMER_ID",from_Customer_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_ID",from_App_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TO_APP_VERSION_ID",from_App_Version_ID,SqlDbType.Int);
					objDataWrapper.AddParameter("@FROM_DRIVER_ID",int.Parse(dr["DriverID"].ToString()),SqlDbType.Int);
					objDataWrapper.AddParameter("@CREATED_BY_USER_ID",from_User_Id,SqlDbType.Int);		
					objDataWrapper.AddParameter("@Called_From",strCalledFrom,SqlDbType.VarChar);							
					DriverInfo +=";Driver Name = " + dr["DriverName"].ToString() + ", Driver Code = " + dr["DriverCode"].ToString();
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					objDataWrapper.ClearParameteres();					
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);	
			
			}
			catch(Exception exc)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);	
				throw (exc);
			}
			finally
			{}
		}	
	}
}
