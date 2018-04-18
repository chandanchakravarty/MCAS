/******************************************************************************************
<Author					: -		Pradeep Iyer
<Start Date				: -		2/9/2005 1:56:35 PM
<End Date				: -	
<Description			: - 	Parses the contents of the Quick quote for Auto P.
<Review Date			: - 
<Reviewed By			: - 	

*******************************************************************************************/ 

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Xml;
//using System.Random ;
using System.Xml.XPath;
using Cms.Model;
using Cms.Model.Client;


using Cms.Model.Application;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application.Watercrafts;
using Cms.Model.Application.PrivatePassenger;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.DataLayer;

namespace Cms.CmsWeb
{


	public class CommonPaths
	{
		public const string ApplicantInfo = "PersPolicy/PersApplicationInfo";
		public const string PriorOrOtherPolicy = "PersPolicy/OtherOrPriorPolicy";
		public const string InsuredOrPrincipal = "InsuredOrPrincipal";

	}

	public class PPAPath
	{
		public const string ApplicationNodes = "PersAutoPolicyQuoteInqRq";
		public const string ApplicantInfo = "PersPolicy/PersApplicationInfo";
		public const string PersVeh = "PersAutoLineBusiness/PersVeh";
		public const string AccidentViolations = "PersPolicy/AccidentViolation"	;
		public const string PersDriver = "PersAutoLineBusiness/PersDriver";
	}

	
	
	public abstract class AcordBase
	{
		public ClsAgencyInfo objAgency;
		public Cms.Model.Client.ClsCustomerInfo objCustomer;
		public ClsGeneralInfo objApplication;
		public ClsPriorPolicyInfo objPriorPolicy;
		public ClsWatercraftGenInfo objWGenInfo;
		public ArrayList alLocation;
		public ArrayList alDriverViolations;
		public ArrayList alEquipments;
		public string UserID ;
		public int QQ_ID;
		

		protected DataWrapper objDataWrapper;
		/// <summary>
		/// Saves Customer details
		/// </summary>
		/// <returns></returns>
		public int SaveCustomer()
		{
			ClsCustomer objBLCustomer = new ClsCustomer();
			
			objBLCustomer.TransactionLogRequired = false;
			
			objCustomer.CustomerAgencyId = this.objAgency.AGENCY_ID;

			try
			{
				int customerID = objBLCustomer.CheckCustomerExistence(this.objCustomer,objDataWrapper);
				
				objDataWrapper.ClearParameteres();
				
				objCustomer.CustomerCountry = "1";
				objCustomer.LAST_UPDATED_DATETIME = DateTime.Now;
				objCustomer.CREATED_DATETIME = DateTime.Now;
				//objCustomer.CustomerType = "11110";

				if ( customerID == -1 )
				{
					//Insert
					if ( objCustomer.CustomerType == "11110" )
					{
						objCustomer.CustomerCode = objCustomer.CustomerFirstName.Substring(0,2) + objCustomer.CustomerLastName.Substring(0,2) + "000001";
					}

					int custID = objBLCustomer.AddCustomer(this.objCustomer,objDataWrapper);
					this.objCustomer.CustomerId = custID;

				}
				else
				{
					//Update
					objCustomer.CustomerType = "11110";
					this.objCustomer.CustomerId = customerID;
					objBLCustomer.UpdateCustomer(objCustomer,objDataWrapper);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
				//return -2 ;
			}
			
			

			return 1;
		}
		
		/// <summary>
		/// Saves Application details
		/// </summary>
		/// <returns></returns>
		public int SaveApplication()
		{
			ClsGeneralInformation objBLApp = new ClsGeneralInformation();
			
			int appID;
			int appVersionID;
			
			objApplication.COUNTRY_ID = 1;
			this.objApplication.CUSTOMER_ID = this.objCustomer.CustomerId;
			this.objApplication.APP_AGENCY_ID = this.objAgency.AGENCY_ID;
			this.objApplication.QQ_ID = QQ_ID;
			objApplication.MODIFIED_BY = Convert.ToInt32(this.UserID);
			objApplication.CREATED_BY = Convert.ToInt32(this.UserID);
			
			
			try
			{
						
				
				int retVal = objBLApp.CheckApplicationExistence(this.objApplication,this.objDataWrapper,out appID,out appVersionID);
				objDataWrapper.ClearParameteres();
				
				//objApplication.APP_LOB = "2";
				objApplication.APP_STATUS = "Incomplete";
				objApplication.CREATED_DATETIME = DateTime.Now;
				objApplication.LAST_UPDATED_DATETIME = DateTime.Now;

				
				if ( retVal == -1 )
				{
					
					objApplication.APP_VERSION_ID = 1;

					int val = objBLApp.Add(this.objApplication,objDataWrapper);
					

				}
				else
				{
					//Update 
					this.objApplication.APP_ID= appID;
					this.objApplication.APP_VERSION_ID= appVersionID;

					//objBLApp.UpdateApp(objApplication,objDataWrapper);
				}
			}
			catch(Exception ex)
			{
				//objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
			
	
			return 1;
		}
		
		/// <summary>
		/// Saves Prior policy
		/// </summary>
		/// <returns></returns>
		public int SavePriorPolicy()
		{
			ClsPriorPolicy objBLPolicy = new ClsPriorPolicy();
			
			this.objPriorPolicy.CUSTOMER_ID = this.objCustomer.CustomerId;
			this.objPriorPolicy.CREATED_DATETIME = DateTime.Now;
			this.objPriorPolicy.LAST_UPDATED_DATETIME = DateTime.Now;

			try
			{
				int priorPolicyID = objBLPolicy.CheckPriorPolicyExistence(this.objPriorPolicy,this.objDataWrapper);
				
				objDataWrapper.ClearParameteres();
				
				if ( priorPolicyID == -1 )
				{
					//int custID = 0;
					//Insert
					int val = objBLPolicy.AddPolicy(this.objPriorPolicy,objDataWrapper);
					//this.objCustomer.CustomerId = custID;

				}
				else
				{
					//Update 
					this.objPriorPolicy.APP_PRIOR_CARRIER_INFO_ID = priorPolicyID;

					objBLPolicy.UpdatePolicy(objPriorPolicy,objDataWrapper);
				}
			}
			catch(Exception ex)
			{
				//objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
			
	
			return 1;
		}
	
		
		public abstract ClsGeneralInfo Import();
		
	}


	public class AutoP
	{
		public ClsAgencyInfo objAgency;
		public Cms.Model.Client.ClsCustomerInfo objCustomer;
		public ClsGeneralInfo objApplication;
		public ClsPriorPolicyInfo objPriorPolicy;
		public ClsApplicantDetailsInfo objApplicant;
		public ClsPPGeneralInformationInfo objPPGenInfo; 
		public ClsUnderwritingTierInfo objPPUtier;
		public ArrayList alPersVehicle;
		public ArrayList alDrivers;
		public ArrayList alLocations;
		public ArrayList alDriverViolations;
		public string UserID ;
		DataWrapper objDataWrapper;
		public int QQ_ID;
		private const string ACCIDENT_INDIANA_AUTOP = "15046";
		private const string ACCIDENT_MICHIGAN_AUTOP = "15047";

		private const string ACCIDENT_INDIANA_MOTOR = "15048";
		private const string ACCIDENT_MICHIGAN_MOTOR = "15049";

		private bool Is_AnyPriorLoss = false;
		
	

		/// <summary>
		/// Saves the records in the database
		/// </summary>
		public ClsGeneralInfo Import()
		{
			objDataWrapper = new DataWrapper(ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			
			try
			{
//				int agencyID = ClsAgency.GetAgencyID(objAgency,objDataWrapper);
//				
//				if ( agencyID == -1 )
//				{
//					//System.Web.HttpContext.Current.Response.Write("<br>Agency not found in the database.");
//					throw new Exception("Agency not found in the database.");
//				}
//			
//				objDataWrapper.ClearParameteres();
//
//				objAgency.AGENCY_ID = agencyID;
								
				if ( this.objCustomer.CustomerId == -1 || this.objCustomer.CustomerId == 0 )
				{
					SaveCustomer();
			
					objDataWrapper.ClearParameteres();
				}

				SaveApplication();

				objDataWrapper.ClearParameteres();
				
				if ( this.objPriorPolicy != null)
				{
					SavePriorPolicy();
				
					objDataWrapper.ClearParameteres();
				}

				
				//Save vehicles
				if ( alPersVehicle != null & alPersVehicle.Count > 0 )
				{
					//Multi car discount :Commented on 20 feb 07 as options changed in QQ : 
					/*if ( alPersVehicle.Count > 1 )
					{
						for(int i = 0; i < alPersVehicle.Count; i++ )
						{
							ClsVehicleInfo objInfo = (ClsVehicleInfo)alPersVehicle[i];
							
							objInfo.MULTI_CAR = "10963";
						}
					}*/

					//Save Vehicles
					for(int i = 0; i < alPersVehicle.Count; i++ )
					{
						ClsVehicleInfo objInfo = (ClsVehicleInfo)alPersVehicle[i];
					
						//Save VehicleInfo
						SavePersVehicle(objInfo);

						objDataWrapper.ClearParameteres();
					}

									
					

					//Set Multicar
				//COMMENTED THIS LINE BY PRAVEEN KUMAR(27-02-2009):ITRACK 5407
					//MultiCarPersVehicle(this.objDataWrapper);
				//END COMMENT PRAVEEN KUMAR
					objDataWrapper.ClearParameteres();

					
					
				
				}
				
				if ( this.alDrivers != null && alDrivers.Count > 0 )
				{
					SaveDriverDetails();
				
					objDataWrapper.ClearParameteres();

					//Save Driver Violations
					SaveDriverViolations();

					objDataWrapper.ClearParameteres();

					//Save Driver specific endorsements A-94 and A-95
					//Commented on 9 July 2008 //Handeled in Auto Rule XML :
					//ClsDriverDetail objDriver = new ClsDriverDetail();
					//objDriver.UpdateDriverEndorsements(objApplication.CUSTOMER_ID,objApplication.APP_ID,objApplication.APP_VERSION_ID,objDataWrapper);
					
					objDataWrapper.ClearParameteres();

				}

				objDataWrapper.ClearParameteres();
				
				SaveCoverages();
				
				objDataWrapper.ClearParameteres();

					


//				if ( objApplicant != null )
//				{
//					SaveApplicantDetails();
//
//					objDataWrapper.ClearParameteres();
//				}
				
				
				SaveGenInfo();

				SaveUnderWritingTierInfo();
				
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

				if ( ex.InnerException != null )
				{
					throw(ex.InnerException);
				}
				else
				{
					throw(ex);
				}
				
			}

			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			//System.Web.HttpContext.Current.Response.Write("Data imported successfully.");

			return objApplication;
		}
		
		

		/// <summary>
		/// Saves records in Assign vehicles to Drivers
		/// </summary>
		/// <returns></returns>
//		public int SaveAssignVehicleToDriver()
//		{
//			ClsAssignedVehicle objBLL = new ClsAssignedVehicle();
//
//			for(int i = 0; i < this.alDrivers.Count; i++ )
//			{
//				ClsDriverDetailsInfo objDriver = (ClsDriverDetailsInfo)alDrivers[i];
//
//				for(int j = 0; j < this.alPersVehicle.Count; j++ )
//				{
//					ClsVehicleInfo objVeh = (ClsVehicleInfo)alPersVehicle[j];
//					if ( objDriver.VEHICLEID == objVeh.ID )
//					{
////						ClsAssignedVehicleInfo objAssign = new ClsAssignedVehicleInfo();
////
////						objAssign.APP_ID = this.objApplication.APP_ID;
////						objAssign.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
////						objAssign.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
////						objAssign.VEHICLE_ID = objVeh.VEHICLE_ID;
////						objAssign.DRIVER_ID = objDriver.DRIVER_ID;
////
////						objBLL.Save(objAssign,this.objDataWrapper);
////
////						objDataWrapper.ClearParameteres();
//					}
//				}
//
//			}
//
//			return 1;
//		}
		
		/// <summary>
		/// Saves Additional Interest
		/// </summary>
		/// <returns></returns>
		public int SaveAddInt()
		{
			ClsAdditionalInterest objBLL = new ClsAdditionalInterest();

			//Save Coverages info
			for(int i = 0; i < alPersVehicle.Count; i++ )
			{
				ClsVehicleInfo objInfo = (ClsVehicleInfo)alPersVehicle[i];
					
				ArrayList alAddInt = objInfo.GetAdditionalInterest();
				
				if ( alAddInt == null ) continue;

				for(int j = 0; j < alAddInt.Count; j++ )
				{
					Cms.Model.Application.ClsAdditionalInterestInfo objInt = (Cms.Model.Application.ClsAdditionalInterestInfo)alAddInt[j];
					
					if ( objInt == null ) continue;

					objInt.HOLDER_COUNTRY = "1";
					objInt.APP_ID = objInfo.APP_ID;
					objInt.APP_VERSION_ID  = objInfo.APP_VERSION_ID;
					objInt.CUSTOMER_ID = objInfo.CUSTOMER_ID;	
					objInt.VEHICLE_ID  = objInfo.VEHICLE_ID;

					int addIntID = objBLL.CheckInterestExistence(objInt,this.objDataWrapper);
					
					objDataWrapper.ClearParameteres();

					if ( addIntID == -1 )
					{
						objBLL.AddAcord(objInt,this.objDataWrapper);
					}
					else
					{
						objInt.ADD_INT_ID =  addIntID;
						objBLL.UpdateAcord(objInt,this.objDataWrapper);
					}

					objDataWrapper.ClearParameteres();

				}

				//objBLL.inalCoverages,objDataWrapper);
				
				
			}

			return 1;
		}

		/// <summary>
		/// Saves Coverages
		/// </summary>
		/// <returns></returns>
		public int SaveCoverages()
		{
			ClsVehicleCoverages objBLL = null ;
			
			if(objApplication.APP_LOB == "AUTOP")
				objBLL = new ClsVehicleCoverages();
			else 
				objBLL = new ClsVehicleCoverages("MOTOR");

			//Save Coverages info
			for(int i = 0; i < alPersVehicle.Count; i++ )
			{
				ClsVehicleInfo objInfo = (ClsVehicleInfo)alPersVehicle[i];
					
				ArrayList alCoverages = objInfo.GetCoverages();
				
				if ( alCoverages == null ) continue;

				for(int j = 0; j < alCoverages.Count; j++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objCoverage = (Cms.Model.Application.ClsCoveragesInfo)alCoverages[j];
					
					if ( objCoverage == null ) continue;

					objCoverage.APP_ID = objInfo.APP_ID;
					objCoverage.APP_VERSION_ID  = objInfo.APP_VERSION_ID;
					objCoverage.CUSTOMER_ID = objInfo.CUSTOMER_ID;	
					objCoverage.RISK_ID = objInfo.VEHICLE_ID;
					objCoverage.CREATED_BY  =Convert.ToInt32(this.UserID);
				}
				//Set UseriD for Transaction Log Entries:
				objBLL.createdby = Convert.ToInt32(this.UserID);

				objBLL.SaveDefaultCoveragesApp(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,
												objInfo.APP_VERSION_ID,objInfo.VEHICLE_ID);  
				
				
				//if(objApplication.APP_LOB == "AUTOP")
				//{
					objBLL.InvalidateInitialisation();
					objBLL.UpdateCoveragesByRuleApp(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,
						RuleType.MakeAppDependent,objInfo.VEHICLE_ID);
				//}

				int retVal = objBLL.SaveAcordVehicleCoverages(alCoverages,objDataWrapper);
				
				objBLL.InvalidateInitialisation();

				objBLL.UpdateCoveragesByRuleApp(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,
												RuleType.RiskDependent,objInfo.VEHICLE_ID);

				if(objApplication.APP_LOB == "MOTOR" || objApplication.APP_LOB == "CYCL")
				{
					objBLL.UpdateCoveragesByRuleApp(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,
						RuleType.UnderWriting,objInfo.VEHICLE_ID);
				}


				if(objApplication.APP_LOB == "AUTOP")
				{
					objBLL.UpdateCoveragesByRuleApp(objDataWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID,
						RuleType.AutoDriverDep,objInfo.VEHICLE_ID);
				}
				
				objDataWrapper.ClearParameteres();
				
				if ( retVal == -1 )
				{
					
					//System.Web.HttpContext.Current.Response.Write("Coverage code " + objCoverage
				}
			}

			return 1;

		}
	
		/// <summary>
		/// Saves Customer details
		/// </summary>
		/// <returns></returns>
		public int SaveCustomer()
		{
			ClsCustomer objBLCustomer = new ClsCustomer();
			
			objBLCustomer.TransactionLogRequired = false;
			
			objCustomer.CustomerAgencyId = this.objAgency.AGENCY_ID;

			try
			{
				int customerID = objBLCustomer.CheckCustomerExistence(this.objCustomer,objDataWrapper);
				
				objDataWrapper.ClearParameteres();
				
				objCustomer.CustomerCountry = "1";
				objCustomer.LAST_UPDATED_DATETIME = DateTime.Now;
				objCustomer.CREATED_DATETIME = DateTime.Now;
				//objCustomer.CustomerType = "11110";

				if ( customerID == -1 )
				{
					string firstName = objCustomer.CustomerFirstName;
					string lastName =  objCustomer.CustomerLastName;

					//Insert
					if ( objCustomer.CustomerType == "11110" )
					{
						
						if ( firstName.Length > 2 && lastName.Length > 2 )
						{
							objCustomer.CustomerCode = objCustomer.CustomerFirstName.Substring(0,2) + objCustomer.CustomerLastName.Substring(0,2) + "000001";
						}
					}

					int custID = objBLCustomer.AddCustomer(this.objCustomer,objDataWrapper);
					this.objCustomer.CustomerId = custID;

				}
				else
				{
					//Update
					objCustomer.CustomerType = "11110";
					this.objCustomer.CustomerId = customerID;
					objBLCustomer.UpdateCustomer(objCustomer,objDataWrapper);
				}
			}
			catch(Exception ex)
			{
				throw(ex);
				//return -2 ;
			}
			
			

			return 1;
		}
		
		/// <summary>
		/// Saves Application details
		/// </summary>
		/// <returns></returns>
		public int SaveApplication()
		{
			ClsGeneralInformation objBLApp = new ClsGeneralInformation();
			
			int appID;
			int appVersionID;
			
			
			objApplication.MODIFIED_BY = Convert.ToInt32(this.UserID);
			objApplication.CREATED_BY = Convert.ToInt32(this.UserID);
			objApplication.COUNTRY_ID = 1;
			this.objApplication.CUSTOMER_ID = this.objCustomer.CustomerId;
			this.objApplication.APP_AGENCY_ID = this.objAgency.AGENCY_ID;
			this.objApplication.QQ_ID = QQ_ID;
			
			if ( objApplication.APP_NUMBER == null || objApplication.APP_NUMBER == "" )
			{
				//System.Web.HttpContext.Current.Response.Write("<br>Application Number cannot be empty in XML.");
				//throw new Exception("Application Number cannot be empty in XML.");
			}
			
			if ( objApplication.APP_VERSION == null || objApplication.APP_VERSION == "" )
			{
				//System.Web.HttpContext.Current.Response.Write("<br>Application Version cannot be empty in XML.");
				//throw new Exception("Application Version cannot be empty in XML.");
			}

			

			try
			{
				int retVal = objBLApp.CheckApplicationExistence(this.objApplication,this.objDataWrapper,out appID,out appVersionID);
				
				objDataWrapper.ClearParameteres();
				
				
				objApplication.APP_STATUS = "Incomplete";
				objApplication.CREATED_DATETIME = DateTime.Now;
				objApplication.LAST_UPDATED_DATETIME = DateTime.Now;

				
				if ( retVal == -1 )
				{
					
					objApplication.APP_VERSION_ID = 1;

					int val = objBLApp.Add(this.objApplication,objDataWrapper);

				}
				else
				{
					//Update 
					this.objApplication.APP_ID= appID;
					this.objApplication.APP_VERSION_ID= appVersionID;
	
					
					objBLApp.UpdateApp(objApplication,objDataWrapper);
				}
			}
			catch(Exception ex)
			{
				//objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
			
	
			return 1;
		}
		
		/// <summary>
		/// Saves Prior policy
		/// </summary>
		/// <returns></returns>
		public int SavePriorPolicy()
		{
			ClsPriorPolicy objBLPolicy = new ClsPriorPolicy();
			
			this.objPriorPolicy.CUSTOMER_ID = this.objCustomer.CustomerId;
			this.objPriorPolicy.CREATED_DATETIME = DateTime.Now;
			this.objPriorPolicy.LAST_UPDATED_DATETIME = DateTime.Now;

			try
			{
				int priorPolicyID = objBLPolicy.CheckPriorPolicyExistence(this.objPriorPolicy,this.objDataWrapper);
				
				objDataWrapper.ClearParameteres();
				
				if ( priorPolicyID == -1 )
				{
					//int custID = 0;
					//Insert
					int val = objBLPolicy.AddPolicy(this.objPriorPolicy,objDataWrapper);
					//this.objCustomer.CustomerId = custID;

				}
				else
				{
					//Update 
					this.objPriorPolicy.APP_PRIOR_CARRIER_INFO_ID = priorPolicyID;

					objBLPolicy.UpdatePolicy(objPriorPolicy,objDataWrapper);
				}
			}
			catch(Exception ex)
			{
				//objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
			
	
			return 1;
		}
	
		/// <summary>
		/// Saves Personal Vehicle details
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int SavePersVehicle(ClsVehicleInfo objInfo)
		{
			ClsVehicleInformation objBLL = new ClsVehicleInformation();
			
			objInfo.CREATED_BY= Convert.ToInt32(this.UserID);
			objInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
			objInfo.APP_ID = this.objApplication.APP_ID;
			objInfo.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
			objInfo.GRG_ADD1 = this.objCustomer.CustomerAddress1;
			objInfo.GRG_ADD2 = this.objCustomer.CustomerAddress2;
			objInfo.GRG_CITY = this.objCustomer.CustomerCity;
			objInfo.GRG_COUNTRY = "1";
			objInfo.GRG_STATE = this.objCustomer.CustomerState;
			objInfo.REGISTERED_STATE = this.objCustomer.CustomerState;


			//Add Garaging information
			for ( int i = 0; i < this.alLocations.Count; i++ )
			{
				ClsLocationInfo objLoc = (ClsLocationInfo)alLocations[i];

				if ( objLoc.ID == objInfo.LOCATION_REF && objLoc.ADDR_TYPE == "GaragingAddress")
				{	
					objInfo.GRG_ZIP = objLoc.LOC_ZIP;
					//objInfo.TERRITORY = objLoc.TERRITORY;
				}
			}
			
			if ( objInfo.GRG_ZIP == null || objInfo.GRG_ZIP == "" )
			{
				objInfo.GRG_ZIP = this.objCustomer.CustomerZip;
			}

			try
			{
				//int vehicleID = objBLL.CheckAppVehicleExistence(objInfo,this.objDataWrapper);
				
				objDataWrapper.ClearParameteres();
				
				
				int val = objBLL.AddVehicle(objInfo,objDataWrapper);
				objInfo.VEHICLE_ID = val;
				
			}
			catch(Exception ex)
			{
				//objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -2 ;
			}
			
	
			return 1;


		}
		/// <summary>
		/// Set multicar :
		/// </summary>
		/// 
		public void MultiCarPersVehicle(DataWrapper objWrapper)
		{
			ClsVehicleInformation objBLL = new ClsVehicleInformation();
			objBLL.UpdateMultiCarVehicle(this.objApplication.CUSTOMER_ID,this.objApplication.APP_ID,this.objApplication.APP_VERSION_ID,objWrapper);
			
		}

		/// <summary>
		/// Saves Driver details
		/// </summary>
		/// <returns></returns>
		public int SaveDriverDetails()
		{
			ClsDriverDetail objBLL = new ClsDriverDetail();
			//Save Driver details
			
			for(int i = 0; i < this.alDrivers.Count; i++ )
			{
				ClsDriverDetailsInfo objInfo = (ClsDriverDetailsInfo)alDrivers[i];
					
				if ( objInfo.DRIVER_FNAME == null || objInfo.DRIVER_FNAME == "" )
				{
					System.Web.HttpContext.Current.Response.Write("<br>Driver first Name cannot be empty.");
					throw new Exception("Driver First Name cannot be empty.");
				}
				
				if ( objInfo.DRIVER_LNAME == null || objInfo.DRIVER_LNAME == "" )
				{
					System.Web.HttpContext.Current.Response.Write("<br>Driver Last Name cannot be empty.");
					throw new Exception("Driver Last Name cannot be empty.");
				}

				objInfo.APP_ID = this.objApplication.APP_ID;
				objInfo.APP_VERSION_ID  = objApplication.APP_VERSION_ID;
				objInfo.CUSTOMER_ID = objApplication.CUSTOMER_ID;	
				objInfo.DRIVER_ADD1 = this.objCustomer.CustomerAddress1;
				objInfo.DRIVER_ADD2 = this.objCustomer.CustomerAddress2;
				objInfo.DRIVER_CITY = this.objCustomer.CustomerCity;
				objInfo.DRIVER_STATE = this.objCustomer.CustomerState;
				objInfo.DRIVER_LIC_STATE = this.objCustomer.CustomerState;
				objInfo.DRIVER_ZIP = this.objCustomer.CustomerZip;
				objInfo.DRIVER_COUNTRY = "1";
				objInfo.DRIVER_US_CITIZEN = "1";
				objInfo.DRIVER_DRINK_VIOLATION = "0";
				objInfo.DRIVER_VOLUNTEER_POLICE_FIRE = "0";
				
				#region Customer Details
				/*Commented on 27 August 2009 Praveen Kasana: Not In Use*/
				//DataSet custds = ClsCustomer.GetCustomerDetails(objApplication.CUSTOMER_ID);
				/*if(custds.Tables[0].Rows.Count > 0)
				{
					objInfo.DRIVER_MOBILE = custds.Tables[0].Rows[0]["PER_CUST_MOBILE"].ToString();
					objInfo.DRIVER_SSN=custds.Tables[0].Rows[0]["SSN_NO"].ToString();
					objInfo.DRIVER_STATE=custds.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString();

				}*/
				#endregion

				//Assign vehicles
				if ( this.alPersVehicle != null && alPersVehicle.Count > 0 )
				{
					for(int j =0; j < alPersVehicle.Count; j++ )
					{	
						ClsVehicleInfo objVehInfo = (ClsVehicleInfo)alPersVehicle[j];

						//V0 - ID we gt From Acord XML while Parsing.
						//V0 - In case of Driver (Does Not Operate Cyle with a Vehicle Assigned)
						if(objInfo.VEHICLEID!="V0")
						{
							if ( objInfo.VEHICLEID == objVehInfo.ID )
							{
								objInfo.VEHICLEID = objVehInfo.VEHICLE_ID.ToString();
							}
						}
						else
							objInfo.VEHICLEID = "0"; //For Does Not rated Driver

					}
				}

				
				/*
				//Fill violations//////////////
				if ( this.alDriverViolations != null && alDriverViolations.Count > 0 )
				{
					for(int j =0; j < alDriverViolations.Count; j++ )
					{	
						DriverViolationInfo objViolation = (DriverViolationInfo)alDriverViolations[j];
						if ( objInfo.ID == objViolation.DriverRef )
						{
							if ( objViolation.AccidentViolationCd == "DBT" || 
								objViolation.AccidentViolationCd == "DOC" || 
								objViolation.AccidentViolationCd == "DPO" || 
								objViolation.AccidentViolationCd == "DTS" || 
								objViolation.AccidentViolationCd == "DWI" || 
								objViolation.AccidentViolationCd ==  "DWIDB"
								)
							{
								
								if ( objViolation.AccidentViolationDt != DateTime.MinValue )
								{
									DateTime dtDate = objViolation.AccidentViolationDt;

									int intViolYear = dtDate.Year;

									if ( intViolYear > ( DateTime.Now.Year - 5 ) )
									{
										objInfo.DRIVER_DRINK_VIOLATION = "1";
										//break;

									}

								}

							}
							
							if ( objViolation.AccidentViolationCd == "DBT" )
							{
								if ( objViolation.AccidentViolationDt != DateTime.MinValue )
								{
									DateTime dtDate = objViolation.AccidentViolationDt;

									int intViolYear = dtDate.Year;

									if ( intViolYear > ( DateTime.Now.Year - 5 ) )
									{
										objInfo.DRIVER_LIC_SUSPENDED = "1";
										//break;

									}
								}

							}


						}

					}
				}
				///////////////
				*/

				
				//for same driver names
				//driverID = -1 case : code will diff
				//for diff. driver name

				//code comment by kranti - Need to Modify this code
				int driverID = -1;//objBLL.CheckDriverExistence(objInfo,objDataWrapper);
				
				objDataWrapper.ClearParameteres();

				objInfo.DRIVER_ID = driverID;

				string firstName = objInfo.DRIVER_FNAME;
				string lastName = objInfo.DRIVER_LNAME;

				//add by kranti
				//setting driver_code based on first name ,last name and auto random number
				objInfo.DRIVER_CODE = GenerateRandomCode(firstName, lastName);

//				if ( firstName.Length > 2 && lastName.Length > 2 )
//				{
//					objInfo.DRIVER_CODE = objInfo.DRIVER_FNAME.Substring(0,2) + objInfo.DRIVER_LNAME.Substring(0,2) + "000001"  ;
//					
//				}
				objInfo.CREATED_BY = Convert.ToInt32(this.UserID);
				objBLL.SaveDriverDetailsAcord(objInfo,objDataWrapper);
				
				objDataWrapper.ClearParameteres();
			}

			return 1;
		}
		/// <summary>
		/// Generating DRIVER_CODE based on driver first name , last name 
		/// </summary>
		/// <param name="FirstName"></param>
		/// <param name="LastName"></param>
		/// <returns></returns>
		public string GenerateRandomCode(string FirstName, string LastName)
		{
			Random randam = new Random();
			string randomCode = randam.Next(100).ToString();
			if(FirstName.Length <1 && LastName.Length < 1)
				return "";
			string firstpart = FirstName;
			string secpart = LastName;

			if(firstpart.Length > 3)
				firstpart = firstpart.Substring(0,3);
			if(secpart.Length > 3)
				secpart = secpart.Substring(0,3);

			return firstpart + secpart + randomCode;
		}



		/// <summary>
		/// Saves the Driver violations from Quick quote in the database
		/// </summary>
		/// <returns></returns>
		public int SaveDriverViolations()
		{
			if ( this.alDriverViolations != null && alDriverViolations.Count > 0 )
			{
				for(int i =0; i < alDriverViolations.Count; i++ )
				{
					ClsMvrInfo objInfo = (ClsMvrInfo)alDriverViolations[i];
					
					objInfo.APP_ID = this.objApplication.APP_ID;
					objInfo.APP_VERSION_ID  = objApplication.APP_VERSION_ID;
					objInfo.CUSTOMER_ID = objApplication.CUSTOMER_ID;	
					objInfo.CREATED_BY = Convert.ToInt32(this.UserID);

					for(int j = 0; j < this.alDrivers.Count ; j++ )
					{
						ClsDriverDetailsInfo objDriver = (ClsDriverDetailsInfo)alDrivers[j];
							
						if ( objInfo.DRIVER_REF == objDriver.ID )
						{
							objInfo.DRIVER_ID = objDriver.DRIVER_ID;

							ClsMvrInformation objBLL = new ClsMvrInformation();

							#region INSERT PRIOR LOSS FOR ACCIDENTS
							//If Accident then Import to Prior Loss Tab Itrack # 
							
							if(
								   objInfo.VIOLATION_ID == int.Parse(ACCIDENT_INDIANA_AUTOP.ToString())
								 || objInfo.VIOLATION_ID == int.Parse(ACCIDENT_MICHIGAN_AUTOP.ToString())
								 || objInfo.VIOLATION_ID == int.Parse(ACCIDENT_INDIANA_MOTOR.ToString())
								 || objInfo.VIOLATION_ID == int.Parse(ACCIDENT_MICHIGAN_MOTOR.ToString())
								)
							{
								objBLL.SavePriorLossAccidentVehicle(objApplication.APP_LOB,objInfo,this.objDataWrapper);
								Is_AnyPriorLoss = true;
								this.objDataWrapper.ClearParameteres();
							}
							
							#endregion

							
							objBLL.SaveViolations(objInfo,this.objDataWrapper);
							
							this.objDataWrapper.ClearParameteres();
						}
					}


				}
			}

			return 1;
		}

		/// <summary>
		/// Saves Applicant details
		/// </summary>
		/// <returns></returns>
//		public int SaveApplicantDetails()
//		{
//			//ClsApplicantDetails objBLL = new ClsApplicantDetails();
//
////			this.objApplicant.APP_ID = this.objApplication.APP_ID;
////			objApplicant.APP_VERSION_ID = objApplication.APP_VERSION_ID;
////			objApplicant.CUSTOMER_ID = objApplication.CUSTOMER_ID;
////			
////			int retVal = objBLL.AddAutoApplicant(objApplicant,this.objDataWrapper);
////
////			return retVal;
//			
//		}

		/// <summary>
		/// Saves the General Information in the database
		/// </summary>
		/// <returns></returns>
		public int SaveGenInfo()
		{
			ClsPPGeneralInformation objPP = new ClsPPGeneralInformation();

			if ( this.objPPGenInfo != null )
			{
				this.objPPGenInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
				this.objPPGenInfo.APP_ID = this.objApplication.APP_ID;
				this.objPPGenInfo.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
				this.objPPGenInfo.CREATED_BY = Convert.ToInt32(this.UserID);
				
				objPPGenInfo.IS_COMMERCIAL_USE = "0"; 
				objPPGenInfo.IS_USEDFOR_RACING = "0";
				
			    objPPGenInfo.IS_MORE_WHEELS = "0";
				objPPGenInfo.IS_EXTENDED_FORKS = "0";
				objPPGenInfo.IS_LICENSED_FOR_ROAD = "0";
				objPPGenInfo.IS_MODIFIED_INCREASE_SPEED = "0";
                objPPGenInfo.IS_MODIFIED_KIT = "0";
				objPPGenInfo.IS_TAKEN_OUT = "0";
				objPPGenInfo.IS_CONVICTED_CARELESS_DRIVE = "0";
				objPPGenInfo.IS_CONVICTED_ACCIDENT = "0";
				objPPGenInfo.CURR_RES_TYPE ="Owned" ;
				if(Is_AnyPriorLoss == true)
				objPPGenInfo.ANY_PRIOR_LOSSES = "1";

				objPP.Save(objPPGenInfo,this.objDataWrapper);
			}

			return 1;
		}

		public int SaveUnderWritingTierInfo()
		{
			if(objApplication.APP_LOB == "AUTOP")
			{
				ClsPPGeneralInformation objPP = new ClsPPGeneralInformation();

				if ( this.objPPUtier!= null)
				{
				
					this.objPPUtier.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
					this.objPPUtier.APP_ID = this.objApplication.APP_ID;
					this.objPPUtier.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
					this.objPPUtier.CREATED_BY = Convert.ToInt32(this.UserID);

					objPP.SaveUnderwritingTier(objPPUtier,this.objDataWrapper);
				}
			}

			return 1;
		}
	}

	
	

	/// <summary>
	/// Summary description for AcordXmlParser.
	/// </summary>
	
	
	public abstract class AcordParserBase
	{
		protected XmlDocument xmlDoc;
		protected XmlNode dataNode;
		protected XmlNode currentNode;
		protected StringBuilder sbError = new StringBuilder();
		
		public string GetErrors()
		{
			return sbError.ToString();
		}

		public void LoadXML(string path)
		{
			xmlDoc = new XmlDocument();
			xmlDoc.Load(path);
		}
		
		public void LoadXmlString(string strXml)
		{
			xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(strXml);
		}

		public XmlNodeList GetApplicationNodeList()
		{
			XmlNodeList InsuranceSvcRq = xmlDoc.DocumentElement.SelectNodes("InsuranceSvcRq");

			return InsuranceSvcRq;
		}
		
		/// <summary>
		/// Returns an array list containing driver violation objects (ClsMvrInfo)
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ArrayList ParseDriverViolations(XmlNode node)
		{
			XmlNodeList violationList = node.SelectNodes("PersPolicy/AccidentViolation");
			//XmlNodeList violationList = node.SelectNodes(PPAPath.AccidentViolations);
				
			ArrayList alViol = new ArrayList();

			if ( violationList == null ) return null;

			foreach(XmlNode violNode in violationList )
			{
				ClsMvrInfo objDriver = new ClsMvrInfo();

				if ( violNode.Attributes["DriverRef"] != null )
				{
					objDriver.DRIVER_REF = violNode.Attributes["DriverRef"].Value;
				}

				dataNode = violNode.SelectSingleNode("AccidentViolationCd");

				if ( dataNode != null )
				{
					objDriver.VIOLATION_CODE= dataNode.InnerText.Trim();
				}
				//Modified 03 12 2007 : Case when Violation doesnt have Violation Types (TextBox)
				if(objDriver.VIOLATION_CODE == "")
				{
					dataNode = violNode.SelectSingleNode("AccidentViolationId");

					if ( dataNode != null )
					{
						objDriver.VIOLATION_ID= int.Parse(dataNode.InnerText.Trim());
					}
				}
					
					
				dataNode = violNode.SelectSingleNode("AccidentViolationDt");

				if ( dataNode != null )
				{
					objDriver.MVR_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				}
					
				dataNode = violNode.SelectSingleNode("DamageTotalAmt/Amt");
					
				if ( dataNode != null )
				{
					objDriver.MVR_AMOUNT = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
				}
					
				dataNode = violNode.SelectSingleNode("AccidentViolationDesc");
					
				if ( dataNode != null )
				{
					if ( dataNode.InnerText.Trim().ToLower() == "death" )
					{
						objDriver.MVR_DEATH = "Y";
					}
					else
					{
						objDriver.MVR_DEATH = "N";
					}

				}

				//Points Assigned
				dataNode = violNode.SelectSingleNode("PointsAssigned");

				if ( dataNode != null )
				{
					if(dataNode.InnerText!="")
					{
						objDriver.POINTS_ASSIGNED = int.Parse(dataNode.InnerText.ToString());
					}
				}

				alViol.Add(objDriver);
			}

			return alViol;

		}


		/// <summary>
		/// Returns an ArrayList containing LocationInfo objects.
		/// </summary>
		/// <param name="nodeApp"></param>
		/// <returns></returns>
		public ArrayList ParseLocations(XmlNode nodeApp)
		{
			XmlNodeList locList = nodeApp.SelectNodes("Location");
			
			if ( locList == null ) return null;

			ArrayList alLocations = new ArrayList();

			foreach(XmlNode locNode in locList)
			{
				ClsLocationInfo objLocInfo = new ClsLocationInfo();

				if ( locNode.Attributes["id"] != null )
				{
					objLocInfo.ID = locNode.Attributes["id"].Value;
				}

				Addr[] arAddr = this.ParseAddress(locNode,"Addr");

				if (  arAddr != null && arAddr.Length > 0)
				{
					foreach(Addr obj in arAddr)
					{
						if ( obj.AddrTypeCd == "StreetAddress")
						{
							objLocInfo.LOC_ADD1 = obj.Addr1;
							objLocInfo.LOC_ADD2 = obj.Addr2;
							objLocInfo.LOC_CITY = obj.City;
							objLocInfo.LOC_COUNTRY = "1";
							objLocInfo.LOC_ZIP = obj.PostalCode;
							objLocInfo.LOC_COUNTY = obj.County;
							objLocInfo.LOC_STATE = obj.StateProv;
						}

					}
				}

				dataNode = locNode.SelectSingleNode("LocationDesc");

				if ( dataNode != null )
				{
					objLocInfo.DESCRIPTION = dataNode.InnerText;
				}
				
				dataNode = locNode.SelectSingleNode("IsPrimary");

				if ( dataNode != null )
				{
					objLocInfo.IS_PRIMARY = dataNode.InnerText.Trim();	
				}
				
				dataNode = locNode.SelectSingleNode("Deductible");

				if ( dataNode != null )
				{				
					//objLocInfo.DEDUCTIBLE = dataNode.InnerText.Trim();
				}

				
				ArrayList alSubloc = null;
				if ( this.currentNode != null )
				{
					alSubloc = this.ParseSublocations(locNode);
				}
				
				if ( alSubloc != null )
				{
					objLocInfo.SetSublocations(alSubloc);
				}

				alLocations.Add(objLocInfo);
			}
			
			return alLocations;

		}
		
		/// <summary>
		/// Returns an Arraylist of sub locations for a location
		/// </summary>
		/// <param name="objNode"></param>
		/// <returns></returns>
		public ArrayList ParseSublocations(XmlNode objNode)
		{
			ArrayList alSubloc = null;//new ArrayList();
			
			XmlNodeList subLocList = objNode.SelectNodes("SubLocation");
			
			if ( subLocList == null ) return null;

			foreach(XmlNode subLocNode in subLocList)
			{
				this.dataNode = subLocNode.SelectSingleNode("SubLocationDesc");
				
				ClsSubLocationInfo objInfo = new ClsSubLocationInfo();
				
				if ( dataNode != null )
				{
					objInfo.SUB_LOC_DESC = dataNode.InnerText.Trim();
				}
				
				if ( objNode.Attributes["id"] != null )
				{
					objInfo.ID = objNode.Attributes["id"].Value;
				}

				if ( alSubloc == null )
				{
					alSubloc = new ArrayList();
				}

				alSubloc.Add(objInfo);

			}
			
			return alSubloc;

		}
		

		/// <summary>
		/// Parses Agency information from ACORD XML
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ClsAgencyInfo ParseProducer(XmlNode node)
		{
			XmlNode objProducer = node.SelectSingleNode("Producer");
			
			if ( objProducer == null )
			{
				throw new Exception("Producer node not found in ACORD XML.");
			}

			dataNode = objProducer.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialName");
			
			ClsAgencyInfo objInfo = new ClsAgencyInfo();

			if ( dataNode != null )
			{
				objInfo.AGENCY_DISPLAY_NAME = dataNode.InnerText.Trim();
			}
			dataNode = objProducer.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialId");
			if ( dataNode != null )
			{
				try
				{
					objInfo.AGENCY_ID = Convert.ToInt32(dataNode.InnerText.Trim());
				}
				catch(Exception ex)
				{
				}
			}
		

			return objInfo;

		}

		
		/// <summary>
		/// Parses Customer information
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ClsCustomerInfo ParseInsuredOrPrincipal(XmlNode node)
		{
			ClsCustomerInfo objCustomer = new ClsCustomerInfo();

			XmlNode objNode = node.SelectSingleNode("InsuredOrPrincipal");
				
			if ( objNode == null )
			{
				throw new Exception("InsuredOrPrincipal node not found in ACORD XML.");
			}
					
			bool isPersonal = false;
			bool isCommercial = false;

			//Getting the Personal name of customer/////////////
			currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName");

			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("Surname");

				if ( dataNode != null )
				{	
					objCustomer.CustomerLastName = dataNode.InnerText;
				}
			
				dataNode = currentNode.SelectSingleNode("GivenName");

				if ( dataNode != null )
				{	
					objCustomer.CustomerFirstName = dataNode.InnerText;
				}
				//End of name
					
					
					
			}
				 
			string commercialName = "";
			if (objCustomer.CustomerLastName=="" && objCustomer.CustomerFirstName =="")
			{
				//Getting Commercial Name 
				currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName");
				
				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("CommercialName");

					if ( dataNode != null)
					{
						commercialName = dataNode.InnerText.Trim();
						//objCustomer.CustomerFirstName = commercialName;

					}

					
				}
			}
				
			if (objCustomer.CustomerFirstName != "" && objCustomer.CustomerLastName != "" )																				
			{
				isPersonal = true;
				objCustomer.CustomerType = "11110";
			}
				
			if ( commercialName != "" )
			{
				objCustomer.CustomerFirstName = commercialName;
				isCommercial = true;
				objCustomer.CustomerType = "11109";	
			}

			if ( isPersonal && isCommercial )
			{
				throw new Exception("The Customer element has both Personal and Commercial name tags.");
			}
					
			if ( isPersonal )
			{
				if ( objCustomer.CustomerFirstName == null || objCustomer.CustomerFirstName == "" )
				{
					//System.Web.HttpContext.Current.Response.Write("<br>Customer First Name cannot be empty.");
					throw new Exception("Customer First Name cannot be empty.");
				}
			
				if ( objCustomer.CustomerLastName == null || objCustomer.CustomerLastName == "" )
				{
					//System.Web.HttpContext.Current.Response.Write("<br>Customer Last Name cannot be empty.");
					throw new Exception("Customer Last Name cannot be empty.");
				}
			}

			if ( isCommercial )
			{
				if ( commercialName == "" )
				{
					throw new Exception("Customer's Commercial name cannot be empty.");
				}
			}
				
			//Get the address details////////
			XmlNodeList nodeList = objNode.SelectNodes("GeneralPartyInfo/Addr");
			
			foreach(XmlNode addrNode in nodeList)
			{
				string addrType  = "";
				
				dataNode = addrNode.SelectSingleNode("AddrTypeCd");
				
				if ( dataNode != null )
				{
					addrType = dataNode.InnerText;
				}

				if ( addrType == "StreetAddress")
				{
					XmlNode addrDataNode = addrNode.SelectSingleNode("Addr1");

					if ( addrDataNode != null )
					{
						objCustomer.CustomerAddress1 = addrDataNode.InnerText;
					}
					
					addrDataNode = addrNode.SelectSingleNode("Addr2");
					
					if ( addrDataNode != null )
					{
						objCustomer.CustomerAddress2 = addrDataNode.InnerText;
					}
					
					addrDataNode = addrNode.SelectSingleNode("City");
					
					if ( addrDataNode != null )
					{
						objCustomer.CustomerCity = addrDataNode.InnerText;
					}

					addrDataNode = addrNode.SelectSingleNode("StateProvCd");
					
					if ( addrDataNode != null )
					{
						objCustomer.CustomerState = addrDataNode.InnerText;
					}
					
					addrDataNode = addrNode.SelectSingleNode("StateID");
					
					if ( addrDataNode != null )
					{
						objCustomer.CustomerState = addrDataNode.InnerText;
					}

					addrDataNode = addrNode.SelectSingleNode("PostalCode");
					
					if ( addrDataNode != null )
					{
						objCustomer.CustomerZip = addrDataNode.InnerText;
					}

					addrDataNode = addrNode.SelectSingleNode("CountryCd");
					
					if ( addrDataNode != null )
					{
						objCustomer.CustomerCountry = addrDataNode.InnerText;
					}
				}
			}
			//End of address

			//Get the phone details
			nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/PhoneInfo");
			
			foreach(XmlNode phoneNode in nodeList)
			{
				string phoneType  = "";
				
				dataNode = phoneNode.SelectSingleNode("PhoneTypeCd");
				
				if ( dataNode != null )
				{
					phoneType = dataNode.InnerText;
				}
				
				dataNode = phoneNode.SelectSingleNode("CommunicationUseCd");
				
				string commType = "";

				if ( dataNode  != null  )
				{
					commType = dataNode.InnerText;
				}

				switch(phoneType)
				{
					case "Phone":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objCustomer.CustomerHomePhone = dataNode.InnerText;
							}
						}
						
						if ( commType == "Business" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objCustomer.CustomerBusinessPhone = dataNode.InnerText;
							}
						}
						break;
					case "Cell":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objCustomer.CustomerMobile = dataNode.InnerText;
							}
						}

						break;
					case "Pager":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objCustomer.CustomerPagerNo = dataNode.InnerText;
							}
						}
						break;
					case "Fax":
						if ( commType == "Home" )
						{
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								objCustomer.CustomerFax = dataNode.InnerText;
							}
						}
						break;
				}
				
				//Get the Email details
				nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/EmailInfo");
			
				foreach(XmlNode emailNode in nodeList)
				{
					dataNode = emailNode.SelectSingleNode("EmailAddr");

					if ( dataNode != null )
					{
						objCustomer.CustomerEmail = dataNode.InnerText;
					}

				}
				
				//Website details
				nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/WebsiteInfo");
			
				foreach(XmlNode emailNode in nodeList)
				{
					dataNode = emailNode.SelectSingleNode("WebsiteURL");

					if ( dataNode != null )
					{
						objCustomer.CustomerWebsite = dataNode.InnerText;
					}
				}

			}
			//End of communications
				
			//Get Customer ID if it exists//////////////////////////
			currentNode = objNode.SelectSingleNode("InsuredOrPrincipalInfo");

			if ( currentNode != null )
			{
				if ( currentNode.Attributes["id"] != null )
				{
					objCustomer.CustomerId = Convert.ToInt32(currentNode.Attributes["id"].Value);
				}
			}
			
			return objCustomer;
		}

		/// <summary>
		/// Parses application details
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ClsGeneralInfo ParsePersPolicy(XmlNode node)
		{
			ClsGeneralInfo objApp = new ClsGeneralInfo();

			//Get the application details
			XmlNode objNode = node.SelectSingleNode("PersPolicy");
			
			dataNode = objNode.SelectSingleNode("PolicyNumber");

			if ( dataNode != null )
			{
				objApp.APP_NUMBER = dataNode.InnerText.Trim();
			}
			
			dataNode = objNode.SelectSingleNode("PolicyVersion");

			if ( dataNode != null )
			{
				objApp.APP_VERSION = dataNode.InnerText.Trim();
			}
			
			// NS -->
			dataNode = objNode.SelectSingleNode("PolicyVersion");

			if ( dataNode != null )
			{
				objApp.APP_VERSION = dataNode.InnerText.Trim();
			}
			// --> NS

			dataNode = objNode.SelectSingleNode("LOBCd");

			if ( dataNode != null )
			{
				objApp.APP_LOB = dataNode.InnerText.Trim();
			}

			dataNode = objNode.SelectSingleNode("PolicyStatusCd");

			if ( dataNode != null )
			{
				objApp.APP_STATUS = dataNode.InnerText.Trim();
			}
			
			dataNode = objNode.SelectSingleNode("ControllingStateProvCd");

			if ( dataNode != null )
			{
				objApp.STATE_CODE = dataNode.InnerText.Trim();
			}
			
			dataNode = objNode.SelectSingleNode("OriginalInceptionDt");

			if ( dataNode != null )
			{
				if ( dataNode.InnerText != "" )
				{
					objApp.APP_INCEPTION_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				}
			}
			
			currentNode = objNode.SelectSingleNode("ContractTerm");
			
			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("EffectiveDt");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						objApp.APP_EFFECTIVE_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					}
				}

				dataNode = currentNode.SelectSingleNode("ExpirationDt");
	
				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						objApp.APP_EXPIRATION_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					}
				}

				//add by kranti
				dataNode = currentNode.SelectSingleNode("AppTerms");

				if(dataNode != null)
				{
					if ( dataNode.InnerText != "" )
					{
						objApp.APP_TERMS = dataNode.InnerText.Trim();
					}
				}
				//end
			}


			
			return objApp;
		}

		
		/// <summary>
		/// Parse Prior Polict details
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ClsPriorPolicyInfo ParseOtherOrPriorPolicy(XmlNode node)
		{
			ClsPriorPolicyInfo objPrior= new ClsPriorPolicyInfo();

			XmlNode objNode = node.SelectSingleNode("PersPolicy/OtherOrPriorPolicy");
			
			if ( objNode == null ) return null;

			dataNode = objNode.SelectSingleNode("PolicyNumber");
				
			if ( dataNode != null )
			{
				objPrior.OLD_POLICY_NUMBER = dataNode.InnerText.Trim();
			}
			
			currentNode = objNode.SelectSingleNode("ContractTerm");
			
			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("EffectiveDt");

				if ( dataNode != null )
				{
					objPrior.EFF_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				}

				dataNode = currentNode.SelectSingleNode("ExpirationDt");

				if ( dataNode != null )
				{
					objPrior.EXP_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				}
				
			}

			return objPrior;
		}
		
		
		/// <summary>
		/// Parse Applicant info
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ClsApplicantDetailsInfo ParsePersApplicationInfo(XmlNode node)
		{
			ClsApplicantDetailsInfo objInfo = new ClsApplicantDetailsInfo();

			this.currentNode = node.SelectSingleNode("PersPolicy/PersApplicationInfo");

			dataNode = currentNode.SelectSingleNode("ResidenceOwnedRentedCd");

			if ( dataNode!= null )
			{
				if ( dataNode.InnerText != "" )
				{
					objInfo.CURR_RES_TYPE = dataNode.InnerText;
				}
			}

			XmlNode addrNode = currentNode.SelectSingleNode("Addr");
			
			/*
			Addr[] arAddr = this.ParseAddress(currentNode,"Addr");

			if (  arAddr != null && arAddr.Length > 0)
			{
				foreach(Addr obj in arAddr)
				{
					if ( obj.AddrTypeCd == "StreetAddress")
					{
						objCustomer.CustomerAddress1 = obj.Addr1;
						objCustomer.CustomerAddress2 = obj.Addr2;
						objCustomer.CustomerCity = obj.City;
						objCustomer.CustomerCountry = obj.CountryCd;
						objCustomer.CustomerZip = obj.PostalCode;
						objCustomer.CustomerCountry = obj.County;
						objCustomer.CustomerState = obj.StateProv;	
					}

				}
			}*/

			StringBuilder sbAddress = new StringBuilder();

			if ( addrNode != null )
			{
				dataNode = addrNode.SelectSingleNode("Addr1");

				if ( dataNode != null )
				{
					sbAddress.Append(dataNode.InnerText);
				}
					
				dataNode = addrNode.SelectSingleNode("Addr2");
					
				if ( dataNode != null )
				{
					sbAddress.Append(dataNode.InnerText);
				}
					
				dataNode = addrNode.SelectSingleNode("City");
					
				if ( dataNode != null )
				{
					sbAddress.Append(dataNode.InnerText);
				}

				dataNode = addrNode.SelectSingleNode("State");
					
				if ( dataNode != null )
				{
					sbAddress.Append(dataNode.InnerText);
				}

				dataNode = addrNode.SelectSingleNode("PostalCode");
					
				if ( dataNode != null )
				{
					sbAddress.Append(dataNode.InnerText);
				}

				dataNode = addrNode.SelectSingleNode("CountryCd");
					
				if ( dataNode != null )
				{
					sbAddress.Append(dataNode.InnerText);
				}
				
				objInfo.PREV_ADD = sbAddress.ToString();
			}
			
			return objInfo;
		}


		public ClsApplicantsInfo ParseApplicantInfo(XmlNode node)
		{
			ClsApplicantsInfo objInfo = new ClsApplicantsInfo();

			this.currentNode = node.SelectSingleNode("PersPolicy/PersApplicationInfo");
			
			if ( currentNode == null ) 
			{
				return null;
			}

			dataNode = currentNode.SelectSingleNode("ResidenceOwnedRentedCd");

			if ( dataNode!= null )
			{
				if ( dataNode.InnerText != "" )
				{
					//objInfo. = dataNode.InnerText;
				}
			}

			XmlNode addrNode = currentNode.SelectSingleNode("Addr");
			
			Addr[] arAddr = this.ParseAddress(currentNode,"Addr");

			if (  arAddr != null && arAddr.Length > 0)
			{
				foreach(Addr obj in arAddr)
				{
					if ( obj.AddrTypeCd == "StreetAddress")
					{
						objInfo.PREV_ADD1 = obj.Addr1;
						objInfo.PREV_ADD2 = obj.Addr2;
						objInfo.PREV_CITY = obj.City;
						//objCustomer.CustomerCountry = obj.CountryCd;
						objInfo.PREV_ZIP = obj.PostalCode;
						objInfo.PREV_STATE = obj.StateProv;	
					}

				}
			}

		
			
			return objInfo;
		}

		public Addr[] ParseAddress(XmlNode node, string xPath)
		{
			//Get the address details////////
			XmlNodeList nodeList = node.SelectNodes(xPath);
			
			ArrayList alAddress = new ArrayList();

			foreach(XmlNode addrNode in nodeList)
			{
				Addr objAddr = new Addr();

				dataNode = addrNode.SelectSingleNode("AddrTypeCd");
				
				if ( dataNode != null )
				{
					objAddr.AddrTypeCd = dataNode.InnerText;
				}

				dataNode = addrNode.SelectSingleNode("Addr1");

				if ( dataNode != null )
				{
					objAddr.Addr1 = dataNode.InnerText;
				}
				
				dataNode = addrNode.SelectSingleNode("Addr2");
				
				if ( dataNode != null )
				{
					objAddr.Addr2 = dataNode.InnerText;
				}
				
				dataNode = addrNode.SelectSingleNode("City");
				
				if ( dataNode != null )
				{
					objAddr.City = dataNode.InnerText;
				}

				dataNode = addrNode.SelectSingleNode("StateProvCd");
				
				if ( dataNode != null )
				{
					objAddr.StateProv = dataNode.InnerText;
				}

				dataNode = addrNode.SelectSingleNode("PostalCode");
				
				if ( dataNode != null )
				{
					objAddr.PostalCode = dataNode.InnerText;
				}
				
				dataNode = addrNode.SelectSingleNode("County");
				
				if ( dataNode != null )
				{
					objAddr.County = dataNode.InnerText;
				}

				alAddress.Add(objAddr);
			}

			return (Addr[])alAddress.ToArray(typeof(Addr));
		}

		
		/// <summary>
		/// Returns an array list containing ClsCoveragesInfo objects
		/// </summary>
		/// <param name="objNode"></param>
		/// <returns></returns>
		public ArrayList ParseCoverages(XmlNode objNode)
		{
			XmlNodeList objCoverageNodes = objNode.SelectNodes("Coverage");
			
			if ( objCoverageNodes == null ) return null;

			ArrayList alCoverages = new ArrayList();

			foreach(XmlNode objCovNode in objCoverageNodes )
			{
				Cms.Model.Application.ClsCoveragesInfo objCovInfo = new Cms.Model.Application.ClsCoveragesInfo();

				dataNode = objCovNode.SelectSingleNode("CoverageCd");

				if ( dataNode != null )
				{
					objCovInfo.COVERAGE_CODE = dataNode.InnerText.Trim();
				}
						
				if ( objCovInfo.COVERAGE_CODE == null || objCovInfo.COVERAGE_CODE == "")
				{
					throw new Exception("Covgerage code cannot be empty in ACORD XML");
				}
				//Signature Obtained

				XmlNode sigNode = objCovNode.SelectSingleNode("IsSigObt"); 
				if(sigNode != null)
				{
					objCovInfo.SIGNATURE_OBTAINED = sigNode.InnerText.Trim();
				}

				//Limits
				XmlNodeList limitNodes = objCovNode.SelectNodes("Limit");
					
				int i = 0;
				if ( limitNodes != null )
				{
					foreach(XmlNode limitNode in limitNodes)
					{
						if ( i < 2 )
						{
							dataNode = limitNode.SelectSingleNode("FormatCurrencyAmt/Amt");
							
							if ( i == 0 )
							{
								try
								{
									if ( dataNode != null )
									{
										if (dataNode.InnerText.ToUpper() != "NO COVERAGE")
										{
											string[] strLimit = dataNode.InnerXml.ToString().Split('/');
											objCovInfo.LIMIT_1 = DefaultValues.GetDoubleFromString(strLimit[0]);
											if(strLimit.Length >1)
												objCovInfo.LIMIT_2 = DefaultValues.GetDoubleFromString(strLimit[1]);
										}
										//objCovInfo.LIMIT_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);

									}
								}
								catch(FormatException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
								}				
								catch(InvalidCastException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
								}

								dataNode = limitNode.SelectSingleNode("Text");
								try
								{
			
									if ( dataNode != null )
									{
										objCovInfo.LIMIT1_AMOUNT_TEXT = dataNode.InnerText.Trim();
									}
								}
								catch(FormatException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
								}				
								catch(InvalidCastException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
								}
							}

							if ( i == 1 )
							{
								try
								{
									if ( dataNode != null )
									{
										//objCovInfo.LIMIT_2 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}
								}
								catch(FormatException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
								}				
								catch(InvalidCastException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
								}

								dataNode = limitNode.SelectSingleNode("Text");
								try
								{
									if ( dataNode != null )
									{
										objCovInfo.LIMIT2_AMOUNT_TEXT = dataNode.InnerText.Trim();
									}
								}
								catch(FormatException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
								}				
								catch(InvalidCastException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
								}
							}
						
							//alCoverages.Add(objCovInfo);
							i++;
						}
					}
				}
				i = 0;

				//Deductibles
				XmlNodeList dedNodes = objCovNode.SelectNodes("Deductible");
					
				if ( dedNodes != null )
				{
					foreach(XmlNode dedNode in dedNodes)
					{
						if ( i < 2 )
						{
							dataNode = dedNode.SelectSingleNode("FormatCurrencyAmt/Amt");
							
							if ( i == 0 )
							{
								try
								{
									if ( dataNode != null )
									{
										objCovInfo.DEDUCTIBLE_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}
								}
								catch(FormatException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
								}				
								catch(InvalidCastException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
								}
								dataNode = dedNode.SelectSingleNode("Text");
							 
								if ( dataNode != null )
								{
									objCovInfo.DEDUCTIBLE1_AMOUNT_TEXT = dataNode.InnerText.Trim();
								}
								 
							}

							if ( i == 1 )
							{
								try
								{
									if ( dataNode != null )
									{
										objCovInfo.DEDUCTIBLE_2 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}
								}
								catch(FormatException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
								}				
								catch(InvalidCastException ex)
								{
									//objCovInfo.LIMIT_1 = 0;
									throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
								}
								dataNode = dedNode.SelectSingleNode("Text");
									
								if ( dataNode != null )
								{
									objCovInfo.DEDUCTIBLE2_AMOUNT_TEXT = dataNode.InnerText.Trim();
								}

							}
						
						
							i++;
						}
					}
				}
				
				//Section
				dataNode = objCovNode.SelectSingleNode("Section");

				if ( dataNode != null )
				{
					objCovInfo.COVERAGE_TYPE = dataNode.InnerText.Trim();
				}

				alCoverages.Add(objCovInfo);
			}

			return alCoverages;
		}

		
		/// <summary>
		/// Returns an array list containing ClsDwellingSectionCoveragesInfo objects for dwelling section
		/// </summary>
		/// <param name="objNode"></param>
		/// <returns></returns>
		public ArrayList ParseSectionCoverages(XmlNode objNode)
		{
			XmlNodeList objCoverageNodes = objNode.SelectNodes("Coverage");
			
			if ( objCoverageNodes == null ) return null;

			ArrayList alCoverages = new ArrayList();

			foreach(XmlNode objCovNode in objCoverageNodes )
			{
				Cms.Model.Application.ClsCoveragesInfo  objCovInfo = new ClsCoveragesInfo();



				dataNode = objCovNode.SelectSingleNode("CoverageCd");

				if ( dataNode != null )
				{
					objCovInfo.COVERAGE_CODE = dataNode.InnerText.Trim();
				}
						
				if ( objCovInfo.COVERAGE_CODE == null || objCovInfo.COVERAGE_CODE == "")
				{
					throw new Exception("Covgerage code cannot be empty in ACORD XML");
				}

				//Added By Ravindra (07-18-2006)
				//For All Perile Deductible 
				//objCovInfo.DEDUCTIBLE =  
				XmlNode additionalNode = objCovNode.SelectSingleNode("Deductible");
				if(additionalNode != null)
				{
					XmlNode additionalDataNode = additionalNode.SelectSingleNode("FormatCurrencyAmt/Amt");
					if ( additionalDataNode != null )
					{
						objCovInfo.DEDUCTIBLE = DefaultValues.GetDoubleFromString(additionalDataNode.InnerXml);
					}
 
					additionalDataNode = additionalNode.SelectSingleNode("Text");
					try
					{
   
						if ( additionalDataNode != null )
						{
							objCovInfo.DEDUCTIBLE_TEXT= additionalDataNode.InnerText.Trim();
						}
					}
					catch(FormatException ex)
					{
						//objCovInfo.LIMIT_1 = 0;
						throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
					}
				}
 
				/////Added By Ravindra End Here

				//Included
				XmlNodeList limitNodes = objCovNode.SelectNodes("Included");
					
				int i = 0;
				if ( limitNodes != null )
				{
					foreach(XmlNode limitNode in limitNodes)
					{
						if ( i < 2 )
						{
							dataNode = limitNode.SelectSingleNode("FormatCurrencyAmt/Amt");
							
							try
							{
								if ( i == 0 )
								{
									if ( dataNode != null )
									{
										objCovInfo.LIMIT_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}

									dataNode = limitNode.SelectSingleNode("Text");
									try
									{
			
										if ( dataNode != null )
										{
											objCovInfo.LIMIT1_AMOUNT_TEXT = dataNode.InnerText.Trim();
										}
									}
									catch(FormatException ex)
									{
										//objCovInfo.LIMIT_1 = 0;
										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
									}				
//									catch(InvalidCastException ex)
//									{
//										//objCovInfo.LIMIT_1 = 0;
//										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
//									}

								}

								if ( i == 1 )
								{
									if ( dataNode != null )
									{
										objCovInfo.LIMIT_2 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}

									dataNode = limitNode.SelectSingleNode("Text");
									try
									{
			
										if ( dataNode != null )
										{
											objCovInfo.LIMIT2_AMOUNT_TEXT = dataNode.InnerText.Trim();
										}
									}
									catch(FormatException ex)
									{
										//objCovInfo.LIMIT_1 = 0;
										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
									}				
//									catch(InvalidCastException ex)
//									{
//										//objCovInfo.LIMIT_1 = 0;
//										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
//									}
								}
							}
							catch(FormatException ex)
							{
								throw new Exception("Limit in Invalid format. Coverage Code:" + objCovInfo.COVERAGE_CODE);
							}
							catch(Exception ex)
							{
								throw new Exception("Error while parsing Deductible. " + ex.Message);
							}
						
							//alCoverages.Add(objCovInfo);
							i++;
						}
					}
				}
				i = 0;

				//Additional
				XmlNodeList dedNodes = objCovNode.SelectNodes("Additional");
					
				if ( dedNodes != null )
				{
					foreach(XmlNode dedNode in dedNodes)
					{
						if ( i < 2 )
						{
							dataNode = dedNode.SelectSingleNode("FormatCurrencyAmt/Amt");
							
							try
							{
								if ( i == 0 )
								{
									if ( dataNode != null )
									{
										objCovInfo.DEDUCTIBLE_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}

									dataNode = dedNode.SelectSingleNode("Text");
									try
									{
			
										if ( dataNode != null )
										{
											objCovInfo.DEDUCTIBLE1_AMOUNT_TEXT = dataNode.InnerText.Trim();
										}
									}
									catch(FormatException ex)
									{
										//objCovInfo.LIMIT_1 = 0;
										throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
									}				
//									catch(InvalidCastException ex)
//									{
//										//objCovInfo.LIMIT_1 = 0;
//										throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
//									}
								}

								if ( i == 1 )
								{
									if ( dataNode != null )
									{
										objCovInfo.DEDUCTIBLE_2 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}

									dataNode = dedNode.SelectSingleNode("Text");
									try
									{
			
										if ( dataNode != null )
										{
											objCovInfo.DEDUCTIBLE2_AMOUNT_TEXT = dataNode.InnerText.Trim();
										}
									}
									catch(FormatException ex)
									{
										//objCovInfo.LIMIT_1 = 0;
										throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
									}				
//									catch(InvalidCastException ex)
//									{
//										//objCovInfo.LIMIT_1 = 0;
//										throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
//									}
								}
							}
							catch(FormatException ex)
							{
								throw new Exception("Deductible in invalid Format.");
							}
							catch(Exception ex)
							{
								throw new Exception("Error while parsing Deductible. Coverage Code:" + objCovInfo.COVERAGE_CODE + " " + ex.Message);
							}
						
							i++;
						}
					}
				}
				
				//Section
				dataNode = objCovNode.SelectSingleNode("Section");

				if ( dataNode != null )
				{
					objCovInfo.COVERAGE_TYPE = dataNode.InnerText.Trim();
				}

				alCoverages.Add(objCovInfo);
			}

			return alCoverages;
		}

		public abstract AcordBase Parse();
		
	}

	
	

		public class AcordXmlParser
		{
			XmlDocument xmlDoc;
			XmlNode dataNode;
			XmlNode currentNode;
			
			private const int UNIQUEID_FOR_NO_DEPENDANTS =11588 ;
			private const int UNIQUEID_FOR_MORE_THAN_1_DEPENDANTS =11589 ;
			private const int SUB_LOB_ID_FOR_TRAILBLAZER =1;

							
			public const string ViolationInformation = "PersPolicy/AccidentViolation";
			public enum SNOW_PLOW_CODE
			{
				INWOI=11914,INWI=11913,FT=11912
			};
			AutoP objApplication = null;

			public AcordXmlParser()
			{
				//
				// TODO: Add constructor logic here
				//
			}

			
			public void LoadXML(string path)
			{
				xmlDoc = new XmlDocument();
				xmlDoc.Load(path);
			}
		
			public void LoadXmlString(string strXml)
			{
				xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(strXml);
			}

			public XmlNodeList GetApplicationNodeList()
			{
				XmlNodeList InsuranceSvcRq = xmlDoc.DocumentElement.SelectNodes("InsuranceSvcRq");
				
				return InsuranceSvcRq;
			}


			public AutoP Parse()
			{
				//No of request in the XML
				XmlNodeList InsuranceSvcRq = GetApplicationNodeList();
				
				//AutoP objApplication = null;

				foreach(XmlNode node in InsuranceSvcRq)
				{
					XmlNodeList appNodeList = node.SelectNodes("PersAutoPolicyQuoteInqRq");
				
					objApplication = new AutoP();
					
					foreach(XmlNode nodeApp in appNodeList)
					{
						//Parse Agency info
						ClsAgencyInfo objInfo = this.ParseProducer(nodeApp);
						objApplication.objAgency = objInfo;

						//Parse customer details
						Cms.Model.Client.ClsCustomerInfo objCustomer = ParseInsuredOrPrincipal(nodeApp);
						objApplication.objCustomer = objCustomer;
					
						//Parse application info
						ClsGeneralInfo objApp = ParsePersPolicy(nodeApp);
						objApplication.objApplication = objApp;

						//Prior carrier info
						ClsPriorPolicyInfo objPriorPolicy = ParseOtherOrPriorPolicy(nodeApp);
						objApplication.objPriorPolicy = objPriorPolicy;
						
						//Parse locations
						ArrayList alVehLoc = this.ParseLocations(nodeApp);
						objApplication.alLocations = alVehLoc;

						//Parse personal vehicle info
						ArrayList alPersVehicle = ParseVehInfo(nodeApp);
						objApplication.alPersVehicle = alPersVehicle;
						
						//Parse Driver Details
						ArrayList alDrivers = this.ParsePersDriver(nodeApp);
						objApplication.alDrivers = alDrivers;
						
						//Parse Applicant details
						ClsApplicantDetailsInfo objApplicant = this.ParsePersApplicationInfo(nodeApp);
						objApplication.objApplicant = objApplicant;
						
						XmlNode lobNode = nodeApp.SelectSingleNode("PersAutoLineBusiness");

						//Driver Violations
						ArrayList alViolations = this.ParseDriverViolations(nodeApp);
						objApplication.alDriverViolations = alViolations;
						
						//General Information
						ClsPPGeneralInformationInfo objPP = this.ParseGenInfo(nodeApp);
						objApplication.objPPGenInfo = objPP;		
		
						//UnderTier Information
						ClsUnderwritingTierInfo objUT = this.ParseUnderTier(nodeApp);
						objApplication.objPPUtier = objUT;
						
					}

					//objApplication.Import();
				}

				return objApplication;

			}
			
			/// <summary>
			/// Returns an array list containing driver violation objects
			/// </summary>
			/// <param name="node"></param>
			/// <returns></returns>
			public ArrayList ParseDriverViolations(XmlNode node)
			{
				//XmlNodeList violationList = node.SelectNodes("PersPolicy/AccidentViolation");
				XmlNodeList violationList = node.SelectNodes(PPAPath.AccidentViolations);
				
				ArrayList alViol = new ArrayList();


				if ( violationList == null ) return null;

				foreach(XmlNode violNode in violationList )
				{
					ClsMvrInfo objDriver = new ClsMvrInfo();

					if ( violNode.Attributes["DriverRef"] != null )
					{
						objDriver.DRIVER_REF = violNode.Attributes["DriverRef"].Value;
					}

					dataNode = violNode.SelectSingleNode("AccidentViolationCd");

					if ( dataNode != null )
					{
						objDriver.VIOLATION_CODE= dataNode.InnerText.Trim();
					}
					//Modified 03 12 2007 : Case when Violation doesnt have Violation Types (TextBox)
					if(objDriver.VIOLATION_CODE == "")
					{
						dataNode = violNode.SelectSingleNode("AccidentViolationId");

						if ( dataNode != null )
						{
							objDriver.VIOLATION_ID= int.Parse(dataNode.InnerText.Trim());
						}
					}
					
					dataNode = violNode.SelectSingleNode("AccidentViolationDt");

					if ( dataNode != null )
					{
						objDriver.MVR_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					}
					
					dataNode = violNode.SelectSingleNode("DamageTotalAmt/Amt");
					
					if ( dataNode != null )
					{
						objDriver.MVR_AMOUNT = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
					}
					
					dataNode = violNode.SelectSingleNode("AccidentViolationDesc");
					
					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim().ToLower() == "death" )
						{
							objDriver.MVR_DEATH = "Y";
						}
						else
						{
							objDriver.MVR_DEATH = "N";
						}

					}
					//Points Assigned
					dataNode = violNode.SelectSingleNode("PointsAssigned");

					if ( dataNode != null )
					{
						if(dataNode.InnerText!="")
						{
							objDriver.POINTS_ASSIGNED = int.Parse(dataNode.InnerText.ToString());
						}
					}

					alViol.Add(objDriver);
				}

				return alViol;

			}


			public Addr[] ParseAddress(XmlNode node, string xPath)
			{
				//Get the address details////////
				XmlNodeList nodeList = node.SelectNodes(xPath);
			
				ArrayList alAddress = new ArrayList();

				foreach(XmlNode addrNode in nodeList)
				{
					Addr objAddr = new Addr();

					dataNode = addrNode.SelectSingleNode("AddrTypeCd");
				
					if ( dataNode != null )
					{
						objAddr.AddrTypeCd = dataNode.InnerText;
					}

					dataNode = addrNode.SelectSingleNode("Addr1");

					if ( dataNode != null )
					{
						objAddr.Addr1 = dataNode.InnerText;
					}
				
					dataNode = addrNode.SelectSingleNode("Addr2");
				
					if ( dataNode != null )
					{
						objAddr.Addr2 = dataNode.InnerText;
					}
				
					dataNode = addrNode.SelectSingleNode("City");
				
					if ( dataNode != null )
					{
						objAddr.City = dataNode.InnerText;
					}

					dataNode = addrNode.SelectSingleNode("StateProvCd");
				
					if ( dataNode != null )
					{
						objAddr.StateProv = dataNode.InnerText;
					}

					dataNode = addrNode.SelectSingleNode("PostalCode");
				
					if ( dataNode != null )
					{
						objAddr.PostalCode = dataNode.InnerText;
					}
				
					dataNode = addrNode.SelectSingleNode("County");
				
					if ( dataNode != null )
					{
						objAddr.County = dataNode.InnerText;
					}
						
					dataNode = addrNode.SelectSingleNode("TerritoryCd");
				
					if ( dataNode != null )
					{
						objAddr.TerritoryCd = dataNode.InnerText;
					}

					alAddress.Add(objAddr);
				}

				return (Addr[])alAddress.ToArray(typeof(Addr));
			}


			public ClsAgencyInfo ParseProducer(XmlNode node)
			{
				XmlNode objProducer = node.SelectSingleNode("Producer");
				
				if ( objProducer == null )
				{
					throw new Exception("Producer node not found in ACORD XML.");
				}

				dataNode = objProducer.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialName");
			
				ClsAgencyInfo objInfo = new ClsAgencyInfo();

				if ( dataNode != null )
				{
					objInfo.AGENCY_DISPLAY_NAME = dataNode.InnerText.Trim();
				}
				
				if ( objInfo.AGENCY_DISPLAY_NAME == null || objInfo.AGENCY_DISPLAY_NAME == "" )
				{
					throw new Exception("Agency Display name cannot be blank in ACORD XML.");
				}
				
				dataNode = objProducer.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialId");
				if ( dataNode != null )
				{					
						objInfo.AGENCY_ID = Convert.ToInt32(dataNode.InnerText.Trim());
					
				}
				
				return objInfo;

			}

		
			public ArrayList ParsePersDriver(XmlNode node)
			{
				ArrayList alDrivers = new ArrayList();
				XmlNode nodAllForViolation = node;

				//XmlNodeList objDriverList = node.SelectNodes("PersAutoLineBusiness/PersDriver");
				XmlNodeList objDriverList = node.SelectNodes(PPAPath.PersDriver);	
			
				if ( objDriverList == null ) return null;

				foreach(XmlNode objDriverNode in objDriverList)
				{
					ClsDriverDetailsInfo objDriver = new ClsDriverDetailsInfo();
					
					if ( objDriverNode.Attributes["id"] != null )
					{
						objDriver.ID = objDriverNode.Attributes["id"].Value;
					}

					//Getting the name of customer/////////////
					currentNode = objDriverNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName");

					//XmlNode dataNode;
					
					if ( currentNode != null )
					{
						dataNode = currentNode.SelectSingleNode("Surname");

						if ( dataNode != null )
						{	
							objDriver.DRIVER_LNAME = dataNode.InnerText.Trim();
						}
			
						dataNode = currentNode.SelectSingleNode("GivenName");

						if ( dataNode != null )
						{	
							objDriver.DRIVER_FNAME = dataNode.InnerText.Trim();
						}

						dataNode = currentNode.SelectSingleNode("OtherGivenName");

						if ( dataNode != null )
						{	
							objDriver.DRIVER_MNAME = dataNode.InnerText.Trim();
						}

						//End of name
					}
					
					if ( objDriver.DRIVER_LNAME == null || objDriver.DRIVER_LNAME == "" )
					{
						throw new Exception("Driver Last Name is empty in XML.");
					}
					
					if ( objDriver.DRIVER_FNAME == null || objDriver.DRIVER_FNAME == "" )
					{
						throw new Exception("Driver First Name is empty in XML.");
					}

					//Get the address details////////
					XmlNodeList nodeList = objDriverNode.SelectNodes("GeneralPartyInfo/Addr");
			
					foreach(XmlNode addrNode in nodeList)
					{
						string addrType  = "";
				
						dataNode = addrNode.SelectSingleNode("AddrTypeCd");
				
						if ( dataNode != null )
						{
							addrType = dataNode.InnerText;
						}

						if ( addrType == "StreetAddress")
						{
							XmlNode addrDataNode = addrNode.SelectSingleNode("Addr1");

							if ( addrDataNode != null )
							{
								objDriver.DRIVER_ADD1 = addrDataNode.InnerText;
							}
					
							addrDataNode = addrNode.SelectSingleNode("Addr2");
					
							if ( addrDataNode != null )
							{
								objDriver.DRIVER_ADD2 = addrDataNode.InnerText;
							}
					
							addrDataNode = addrNode.SelectSingleNode("City");
					
							if ( addrDataNode != null )
							{
								objDriver.DRIVER_CITY = addrDataNode.InnerText;
							}

							addrDataNode = addrNode.SelectSingleNode("State");
					
							if ( addrDataNode != null )
							{
								objDriver.DRIVER_STATE = addrDataNode.InnerText;
							}

							addrDataNode = addrNode.SelectSingleNode("PostalCode");
					
							if ( addrDataNode != null )
							{
								objDriver.DRIVER_ZIP = addrDataNode.InnerText;
							}

						}
			
					}
				
					//Person Info
					currentNode = objDriverNode.SelectSingleNode("DriverInfo/PersonInfo");
					
					if ( currentNode != null )
					{
						dataNode = currentNode.SelectSingleNode("GenderCd");
				
						if ( dataNode != null )
						{
							objDriver.DRIVER_SEX = dataNode.InnerText;
						}

						dataNode = currentNode.SelectSingleNode("BirthDt");
				
						if ( dataNode != null )
						{
							objDriver.DRIVER_DOB = DefaultValues.GetDateFromString(dataNode.InnerText);
						}

						dataNode = currentNode.SelectSingleNode("MaritalStatusCd");
				
						if ( dataNode != null )
						{
							objDriver.DRIVER_MART_STAT = dataNode.InnerText;
						}
				
						dataNode = currentNode.SelectSingleNode("OccupationClassCd");
				
						if ( dataNode != null )
						{
							objDriver.DRIVER_OCC_CODE = dataNode.InnerText;
						}

						dataNode = currentNode.SelectSingleNode("Income");
				
						if ( dataNode != null )
						{
							if ( dataNode.InnerText.Trim().ToLower() == "high" )
							{
								objDriver.DRIVER_INCOME = 11415;
							}
							if ( dataNode.InnerText.Trim().ToLower() == "low" )
							{
								objDriver.DRIVER_INCOME = 11414;
							}

						}

						// No of dependants
						dataNode = currentNode.SelectSingleNode("NoOfDependants");
				
						if ( dataNode != null )
						{

							if ( dataNode.InnerText.Trim().ToLower() == "1more" )
							{
								objDriver.NO_DEPENDENTS = 11589;
							}
							if ( dataNode.InnerText.Trim().ToLower() == "ndep" )
							{
								objDriver.NO_DEPENDENTS = 11588;
							}

							//Commented on 05 april 06 : 
							//int noOfDependants = Convert.ToInt32(dataNode.InnerText.Trim()==""?"0":dataNode.InnerText.Trim());
							//int noOfDependantsLookup=0;

						//	if (noOfDependants <1 )
						//	{
							//	noOfDependantsLookup = UNIQUEID_FOR_NO_DEPENDANTS; 
						//	}
						//	else
						//	{
						//		noOfDependantsLookup = UNIQUEID_FOR_MORE_THAN_1_DEPENDANTS;
						//	}

						//	objDriver.NO_DEPENDENTS = noOfDependantsLookup;
 
						}

					}

					//Licence Info
					currentNode = objDriverNode.SelectSingleNode("DriverInfo/DriversLicense");
					
					if ( currentNode != null )
					{
						dataNode = currentNode.SelectSingleNode("LicensedDt");
				
						if ( dataNode != null )
						{
							objDriver.DATE_LICENSED = DefaultValues.GetDateFromString(dataNode.InnerText);
						}
				
						dataNode = currentNode.SelectSingleNode("DriversLicenseNumber");
				
						if ( dataNode != null )
						{
							objDriver.DRIVER_DRIV_LIC = dataNode.InnerText;
						}

						dataNode = currentNode.SelectSingleNode("StateProv");
				
						if ( dataNode != null )
						{
							objDriver.DRIVER_LIC_STATE = dataNode.InnerText;
						}
					}

					//PersDriver Info
					currentNode = objDriverNode.SelectSingleNode("PersDriverInfo");
				
					if ( currentNode != null )
					{
						if ( currentNode.Attributes["VehPrincipallyDrivenRef"] != null )
						{
							objDriver.VEHICLEID = currentNode.Attributes["VehPrincipallyDrivenRef"].Value; 
						}
					
						dataNode = currentNode.SelectSingleNode("DriverRelationshipToApplicantCd");
				
						if ( dataNode != null )
						{
							objDriver.RELATIONSHIP_CODE = dataNode.InnerText;
						}
					
						//dataNode = currentNode.SelectSingleNode("DriverTypeCd");
						//Driver Type
						dataNode = currentNode.SelectSingleNode("OperatesCd");
				
						if ( dataNode != null )
						{
							if(dataNode.InnerText.ToUpper().Trim() == "OC")
								objDriver.DRIVER_DRIV_TYPE = "11941";
							else
								objDriver.DRIVER_DRIV_TYPE = "11942";
						}
//						objDriver.DRIVER_DRIV_TYPE = "11603";
						
						dataNode = currentNode.SelectSingleNode("GoodStudentCd");
				
						if ( dataNode != null )
						{
							if ( dataNode.InnerText.Trim() != "")
								objDriver.DRIVER_GOOD_STUDENT = "1";
						}
						
						dataNode = currentNode.SelectSingleNode("DriverTypeCd");
				
						if ( dataNode != null )
						{
							if ( dataNode.InnerText.ToUpper().Trim() == "NR")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11931;
							else if (dataNode.InnerText.ToUpper().Trim() == "OMPA")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11926;
							else if (dataNode.InnerText.ToUpper().Trim() == "OPA")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11925;
							else if (dataNode.InnerText.ToUpper().Trim() == "PNPA")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11399;
							else if (dataNode.InnerText.ToUpper().Trim() == "PPA")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11398;
							else if (dataNode.InnerText.ToUpper().Trim() == "YONPA")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11928;
							else if (dataNode.InnerText.ToUpper().Trim() == "YOPA")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11927;
							else if (dataNode.InnerText.ToUpper().Trim() == "YPNPA")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11930;
							else if (dataNode.InnerText.ToUpper().Trim() == "YPPA")
								objDriver.APP_VEHICLE_PRIN_OCC_ID = 11929;							
								
						}

					}
				
					//Get the phone details
					nodeList = objDriverNode.SelectNodes("GeneralPartyInfo/Communications/PhoneInfo");
			
					foreach(XmlNode phoneNode in nodeList)
					{
						string phoneType  = "";
				
						dataNode = phoneNode.SelectSingleNode("PhoneTypeCd");
				
						if ( dataNode != null )
						{
							phoneType = dataNode.InnerText;
						}
				
						dataNode = phoneNode.SelectSingleNode("CommunicationUseCd");
				
						string commType = "";

						if ( dataNode  != null  )
						{
							commType = dataNode.InnerText;
						}

						switch(phoneType)
						{
							case "Phone":
								if ( commType == "Home" )
								{
									dataNode = phoneNode.SelectSingleNode("PhoneNumber");

									if ( dataNode != null )
									{
										objDriver.DRIVER_HOME_PHONE = dataNode.InnerText;
									}
								}
						
								if ( commType == "Business" )
								{
									dataNode = phoneNode.SelectSingleNode("PhoneNumber");

									if ( dataNode != null )
									{
										objDriver.DRIVER_BUSINESS_PHONE = dataNode.InnerText;
									}
								}
								break;
							case "Cell":
							
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if ( dataNode != null )
								{
									objDriver.DRIVER_MOBILE = dataNode.InnerText;
								}
							

								break;
						
						}

						//Question answers///////////////////////////
						currentNode = objDriverNode.SelectSingleNode("DriverInfo/QuestionAnswer");

						if ( currentNode != null )
						{
							//Premier driver discount
							dataNode = currentNode.SelectSingleNode("PremierDriverDiscount");

							if ( dataNode != null )
							{
								if ( dataNode.InnerText.Trim().ToLower() == "true" )
								{
									objDriver.DRIVER_PREF_RISK = "0";
								}
								else if (dataNode.InnerText.Trim().ToLower() == "false") 
								{
									objDriver.DRIVER_PREF_RISK = "1";
								}
							}
							
							//Good student
							dataNode = currentNode.SelectSingleNode("GoodStudent");

							if ( dataNode != null )
							{
								if ( dataNode.InnerText.Trim().ToLower() == "true" )
								{
									objDriver.DRIVER_GOOD_STUDENT = "1";
								}
								else if (dataNode.InnerText.Trim().ToLower() == "false") 
								{
									objDriver.DRIVER_GOOD_STUDENT = "0";
								}
							}
							/////////////////////////////////
							//College student
							
							dataNode = currentNode.SelectSingleNode("CollegeStudent");
							if ( dataNode != null )
							{
								if ( dataNode.InnerText.Trim().ToLower() == "true" )
								{
									objDriver.DRIVER_STUD_DIST_OVER_HUNDRED = "1";
								}
								else if (dataNode.InnerText.Trim().ToLower() == "false") 
								{
									objDriver.DRIVER_STUD_DIST_OVER_HUNDRED = "0";
								}
							}
							/////////////////////////////////////////////

							//College student
							//Commented on 12 Mar 07
							dataNode = currentNode.SelectSingleNode("CollegeStudent");

							if ( dataNode != null )
							{
								if ( dataNode.InnerText.Trim().ToLower() == "yes" )
								{
									objDriver.COLL_STUD_AWAY_HOME = 1;
								}
								else if (dataNode.InnerText.Trim().ToLower() == "no") 
								{
									objDriver.COLL_STUD_AWAY_HOME = 0;
								}
							}

							//Cycle With You
							dataNode = currentNode.SelectSingleNode("CycleWithYou");

							if ( dataNode != null )
							{
								if ( dataNode.InnerText.Trim().ToLower() == "yes" )
								{
									objDriver.CYCL_WITH_YOU  = 1;
								}
								else if (dataNode.InnerText.Trim().ToLower() == "no") 
								{
									objDriver.CYCL_WITH_YOU = 0;
								}
							}


							//work loss waiver
							dataNode = currentNode.SelectSingleNode("WaiveWorkLoss");

							if ( dataNode != null )
							{
								if ( dataNode.InnerText.Trim().ToLower() == "true" )
								{
									objDriver.WAIVER_WORK_LOSS_BENEFITS  = "1";
								}
								else if (dataNode.InnerText.Trim().ToLower() == "false") 
								{
									objDriver.WAIVER_WORK_LOSS_BENEFITS = "0";
								}
							}
							////////////////////////////
							//No Cycle endorsement : 27 feb 2006
							dataNode = currentNode.SelectSingleNode("NoCycleEndorsement");

							if ( dataNode != null )
							{
								if ( dataNode.InnerText.Trim().ToUpper() == "Y" )
								{
									objDriver.NO_CYCLE_ENDMT  = "0";//""1"; Itrack 5712
								}
								else if (dataNode.InnerText.Trim().ToUpper() == "N") 
								{
									objDriver.NO_CYCLE_ENDMT = "1";//"0";Itrack 5712
								}
							}
							////////////////////////////
							//SAFE Driver Discount
							dataNode = currentNode.SelectSingleNode("SafeDriverRenewalDiscount");

							if ( dataNode != null )
							{
								if ( dataNode.InnerText.Trim().ToUpper() == "YES" )
								{
									objDriver.SAFE_DRIVER_RENEWAL_DISCOUNT  = "1";
								}
								else if (dataNode.InnerText.Trim().ToUpper() == "NO") 
								{
									objDriver.SAFE_DRIVER_RENEWAL_DISCOUNT = "0";
								}
							}


						}


					}
					
					//Added By Kranti Checking Violations				
					XmlAttribute tempAttr = objDriverNode.Attributes["id"];
					string tempattribute="";
					ClsCommon ObjClsCommon = new ClsCommon();
					if (tempAttr !=null)
						tempattribute = tempAttr.Value.ToString();
					string strTemp =ViolationInformation+"[@DriverRef='"+tempattribute +"']";

					XmlNodeList tempNodes = nodAllForViolation.SelectNodes(strTemp);
					if (tempNodes != null && tempNodes.Count >0)
					{
						//objDriver.VIOLATIONS = ObjClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES ;
						objDriver.VIOLATIONS = 10963;
					}
					else
					{
						//objDriver.VIOLATIONS = ObjClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO ;
						objDriver.VIOLATIONS = 10964;
					}
					//End kranti


					alDrivers.Add(objDriver);	
				}

				return alDrivers;
			}


			public ArrayList ParseVehInfo(XmlNode node)
			{
		
				
				//XmlNodeList objVehNodes = node.SelectNodes("PersAutoLineBusiness/PersVeh");
				XmlNodeList objVehNodes = node.SelectNodes(PPAPath.PersVeh);
			    
				if ( objVehNodes == null ) return null ;
			
				ArrayList alVehicles= new ArrayList();

				foreach(XmlNode objNode in objVehNodes)
				{
				
					ClsVehicleInfo objVehicle= new ClsVehicleInfo();
					 
					if ( objNode.Attributes["id"] != null )
					{
						objVehicle.ID = objNode.Attributes["id"].Value;
					}
					
					if ( objNode.Attributes["LocationRef"] != null )
					{
						objVehicle.LOCATION_REF = objNode.Attributes["LocationRef"].Value;
					}

					dataNode = objNode.SelectSingleNode("Manufacturer");
				
					if( dataNode != null)
					{
						objVehicle.MAKE =  ClsCommon.DecodeXMLCharacters (dataNode.InnerText.Trim());
					}
				
					dataNode = objNode.SelectSingleNode("Model");
				
					if( dataNode != null)
					{
						objVehicle.MODEL =  ClsCommon.DecodeXMLCharacters (dataNode.InnerText.Trim());
					}
					
					dataNode = objNode.SelectSingleNode("ModelYear");
				
					if( dataNode != null)
					{
						objVehicle.VEHICLE_YEAR =  dataNode.InnerText.Trim();
					}
					
					///////////
					if ( objVehicle.VEHICLE_YEAR == null || objVehicle.VEHICLE_YEAR == "" )
					{
						System.Web.HttpContext.Current.Response.Write("<br>Vehicle year cannot be empty.");
						throw new Exception("Vehicle year cannot be empty.");
					}
					
					if ( objVehicle.MAKE == null || objVehicle.MAKE == "" )
					{
						System.Web.HttpContext.Current.Response.Write("<br>Vehicle make cannot be empty.");
						throw new Exception("Vehicle make cannot be empty.");
					}

					if ( objVehicle.MODEL == null || objVehicle.MODEL == "" )
					{
						System.Web.HttpContext.Current.Response.Write("<br>Vehicle Model cannot be empty.");
						throw new Exception("Vehicle Model cannot be empty.");
					}
					///////////
					
					dataNode = objNode.SelectSingleNode("VehBodyTypeCd");
				
					if( dataNode != null)
					{
						objVehicle.BODY_TYPE = ClsCommon.DecodeXMLCharacters ( dataNode.InnerText.Trim());
					}
				
					dataNode = objNode.SelectSingleNode("VehIdentificationNumber");
				
					if( dataNode != null)
					{
						objVehicle.VIN =  ClsCommon.DecodeXMLCharacters (dataNode.InnerText.Trim());
					}

					dataNode = objNode.SelectSingleNode("VehSymbolCd");
				
					if( dataNode != null)
					{
						int intSymbol = 0;
						
						bool isError = false;

						try
						{
							intSymbol = DefaultValues.GetIntFromString(dataNode.InnerText.Trim());
						}
						catch(Exception ex)
						{
							isError  = true;
						}
						
						if ( isError == false )
						{
							objVehicle.SYMBOL = intSymbol;
						}

					}
					
					dataNode = objNode.SelectSingleNode("RateClassCd");
				
					if( dataNode != null)
					{
						objVehicle.CLASS= dataNode.InnerText.Trim();
						 
					}

					//Class Driver ID 16 April 2007

					dataNode = objNode.SelectSingleNode("RateClassId");
				
					if( dataNode != null && dataNode.InnerText.ToString() !="")
					{
						objVehicle.CLASS_DRIVERID= int.Parse(dataNode.InnerText.ToString().Trim());
						 
					}

					//dataNode = objNode.SelectSingleNode("VehicleValue"); //For CYCL
					dataNode = objNode.SelectSingleNode("VehicleValue");
					if( dataNode != null)
					{
						if(dataNode.InnerText!="" && dataNode.InnerText.ToString()!="$0")
							objVehicle.AMOUNT = Convert.ToDouble(dataNode.InnerText.Trim());
						 
					}
					//Value of node has been changed as the node name for vehicle amount as given in acord xml file
					//is <CostNewAmt><Amt></..> and not VehicleVelaue as used here
					dataNode = objNode.SelectSingleNode("CostNewAmt/Amt"); //For Auto
				
					if( dataNode != null)
					{
						if(dataNode.InnerText!="" && dataNode.InnerText.ToString()!="$0")
							objVehicle.AMOUNT = Convert.ToDouble(dataNode.InnerText.Trim());
						 
					}

					dataNode = objNode.SelectSingleNode("EstimatedAnnualDistance/NumUnits");
						
					if( dataNode != null)
					{
						try
						{
							objVehicle.ANNUAL_MILEAGE = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
						}
						catch(Exception ex)
						{
							throw new Exception("Estimated Annual Distance has invalid value.");
						}

					}

					//Added by RP - 5 SEP 2006
					dataNode = objNode.SelectSingleNode("RadiusOfUse");				
					if( dataNode != null)
						objVehicle.RADIUS_OF_USE= Convert.ToInt32(dataNode.InnerText.Trim());
						 
					dataNode = objNode.SelectSingleNode("CarPool");				

					if( dataNode != null)
					{
						if ( dataNode.InnerText.Trim().ToLower() == "yes" )
						{
							objVehicle.CAR_POOL = 10963;
						}
						else if (dataNode.InnerText.Trim().ToLower() == "no" )
						{
							objVehicle.CAR_POOL = 10964;
						}
					}

					


					/*
					 	<option value=""></option>
						<option value="11912">Full Time</option>
						<option value="11913">Incidental with Income</option>
						<option value="11914">Incidental without Income -- 'INWOI'</option> 
					 */
					dataNode = objNode.SelectSingleNode("SnowPlowCode");				
					if( dataNode != null)
					{
						if(dataNode.InnerText.Trim() != "")
						{
							if(dataNode.InnerText.Trim().Equals("INWOI"))
								objVehicle.SNOWPLOW_CONDS=(int) SNOW_PLOW_CODE.INWOI ;
							else if(dataNode.InnerText.Trim().Equals("INWI"))
								objVehicle.SNOWPLOW_CONDS=(int) SNOW_PLOW_CODE.INWI ;
							else if(dataNode.InnerText.Trim().Equals("FT"))
									 objVehicle.SNOWPLOW_CONDS=(int) SNOW_PLOW_CODE.FT  ;


						}
					}

					//Added on 12 Nov 2008
					dataNode = objNode.SelectSingleNode("IsSuspendedComp");				
					if( dataNode != null)
					{
						if(dataNode.InnerText.Trim()!= "")
						{
							if(dataNode.InnerText.ToUpper() == "YES")
								objVehicle.IS_SUSPENDED = 10963;
							else
								objVehicle.IS_SUSPENDED = 10964;
                            							
						}
					}

					//MotorCycle
					dataNode = objNode.SelectSingleNode("IsCompOnly");				
					if( dataNode != null)
					{
						if(dataNode.InnerText.Trim()!= "")
						{
							if(dataNode.InnerText.ToUpper() == "YES")
								objVehicle.COMPRH_ONLY = 10963;
							else
								objVehicle.COMPRH_ONLY = 10964;
                            							
						}
					}

//					dataNode = objNode.SelectSingleNode("VEHICLECLASS_COMM");				
//					if( dataNode != null)
//						objVehicle.CLASS_DESCRIPTION = ClsAuto.GetUniqueIdCommClass(dataNode.InnerText.Trim());
//					else
//						objVehicle.CLASS_DESCRIPTION = "0";
					
					
					//add by kranti for VEHICLECLASS_DESC_COMM
					dataNode = objNode.SelectSingleNode("RateClassDescCd");				
					if( dataNode != null)
						objVehicle.CLASS_DESCRIPTION = dataNode.InnerText.Trim();
					else
						objVehicle.CLASS_DESCRIPTION = "0";


					objVehicle.COVERED_BY_WC_INSU = "0";
					objVehicle.TRANSPORT_CHEMICAL = "0";
					//End of addition  by RP - 5 SEP 2006
					

					
					dataNode = objNode.SelectSingleNode("Displacement/NumUnits");					
					if( dataNode != null)
					{
						try
						{
							if ( dataNode.InnerText.Trim() != "" )
							{
								string strCC = dataNode.InnerText.Trim().Replace(",","");
								objVehicle.VEHICLE_CC= DefaultValues.GetIntFromString(strCC);

							}
						}
						catch(Exception ex)
						{
							throw new Exception("CC has invalid value.");
						}

					}

					//distance one way
					dataNode = objNode.SelectSingleNode("DistanceOneWay/NumUnits");
					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim() != "" )
						{
							objVehicle.MILES_TO_WORK = dataNode.InnerText.Trim();
						}
					}

					dataNode = objNode.SelectSingleNode("CostNewAmt/Amt");

					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim() != "" )
						{
							objVehicle.AMOUNT_COST_NEW = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
						}
					}

				
					dataNode = objNode.SelectSingleNode("AntilockBrakeDiscount");
				
					if( dataNode != null)
					{
						if ( dataNode.InnerText.Trim().ToLower() == "true" )
						{
							objVehicle.ANTI_LOCK_BRAKES = "10963";
						}
						else if (dataNode.InnerText.Trim().ToLower() == "false" )
						{
							objVehicle.ANTI_LOCK_BRAKES = "10964";
						}
					}
				
//					dataNode = objNode.SelectSingleNode("LicensePlateNumber");
//				
//					if( dataNode != null)
//					{
//						objVehicle.CAR_POOL  = dataNode.InnerText.Trim();
//					}
				
					dataNode = objNode.SelectSingleNode("GaragingPostalCode");				
					if( dataNode != null)
					{
						objVehicle.GRG_ZIP = dataNode.InnerText.Trim();
					}

					dataNode = objNode.SelectSingleNode("TerritoryCd");
				
					if( dataNode != null)
					{
						objVehicle.TERRITORY = dataNode.InnerText.Trim();
					}	
				
					dataNode = objNode.SelectSingleNode("VehTypeCd");
				
					if( dataNode != null)
					{
						objVehicle.VEH_TYPE_CODE = dataNode.InnerText.Trim();
						objVehicle.MOTORCYCLE_TYPE_CODE = dataNode.InnerText.Trim();
					}

					//For Age
					dataNode = objNode.SelectSingleNode("VehAge");
				
					if( dataNode != null)
					{
						if(dataNode.InnerText!="")
                            objVehicle.VEHICLE_AGE = Double.Parse(dataNode.InnerText.Trim());
						else
							objVehicle.VEHICLE_AGE = 0.0;
						
					}
				
					dataNode = objNode.SelectSingleNode("SeatBeltTypeCd");
				
					if( dataNode != null)
					{
						if ( dataNode.InnerText.Trim().ToLower() == "yes" )
						{
							objVehicle.PASSIVE_SEAT_BELT = "10963";
						}
						else if (dataNode.InnerText.Trim().ToLower() == "no" )
						{
							objVehicle.PASSIVE_SEAT_BELT = "10964";
						}
					}

							
					dataNode = objNode.SelectSingleNode("AirBagTypeCd");
				
					if( dataNode != null)
					{
						objVehicle.AIR_BAG = dataNode.InnerText.Trim();
					}
				
					dataNode = objNode.SelectSingleNode("VehUseCd");
				
					if( dataNode != null)
					{
						objVehicle.VEHICLE_USE = dataNode.InnerText.Trim();
					}
					
					dataNode = objNode.SelectSingleNode("VehicleUse");
				
					if( dataNode != null)
					{
						objVehicle.USE_VEHICLE_CODE = dataNode.InnerText.Trim();
					}
					
				
					
					dataNode = objNode.SelectSingleNode("MulticarDisount");
				
					if( dataNode != null)
					{
						if ( dataNode.InnerText.Trim().ToUpper() == "NA" )
						{
							objVehicle.MULTI_CAR = "11918";
						}
						else if (  dataNode.InnerText.Trim().ToUpper() == "OCTP" )
						{
							objVehicle.MULTI_CAR = "11919";
						}
						else if (  dataNode.InnerText.Trim().ToUpper() == "OPWW" )
						{
							objVehicle.MULTI_CAR = "11920";
						}
					}

					dataNode = objNode.SelectSingleNode("PurchaseDt");
				
					if( dataNode != null)
					{
						objVehicle.PURCHASE_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					}
				
					dataNode = objNode.SelectSingleNode("InsuranceAmountMiscEqupt");
				
					if( dataNode != null)
					{
						if(!dataNode.InnerText.Equals(""))
						{
							if(Convert.ToDouble(dataNode.InnerText)>0)
                                objVehicle.MISC_AMT = double.Parse(dataNode.InnerText.Trim());
						}
					}

					dataNode = objNode.SelectSingleNode("InsuranceDescMiscEqupt");
				
					if( dataNode != null)
					{
						if(!dataNode.InnerText.Equals(""))
							objVehicle.MISC_EQUIP_DESC  = dataNode.InnerText.Trim();
					}

					

					
					//Parse Addl interest
					ArrayList alAddInt = this.ParseAdditionalInterest(objNode);
					objVehicle.SetAdditionalInterest(alAddInt);
					//end of Addl Info
				
					//Parse coverages
					ArrayList alCoverages = this.ParseCoverages(objNode);
					objVehicle.SetCoverages(alCoverages);
					//end of coverages

					//objVehicle.MISC_AMT = 
			
					alVehicles.Add(objVehicle);

				}
				return alVehicles;
				//objNode.Attributes[
			}
		
			/// <summary>
			/// Returns an ArrayList containing ClsAdditionalInterestInfo objects
			/// </summary>
			/// <param name="objNode"></param>
			/// <returns></returns>
			public ArrayList ParseAdditionalInterest(XmlNode objNode)
			{
				//Parse Addl Interest Info///
				XmlNodeList objAddIntNodes = objNode.SelectNodes("AdditionalInterest");
			
				if ( objAddIntNodes == null ) return null;

				ArrayList alAddInt = new ArrayList();

				foreach(XmlNode objIntNode in objAddIntNodes)
				{
					ClsAdditionalInterestInfo onjIntInfo = new ClsAdditionalInterestInfo();

					//currentNode = objIntNode.SelectSingleNode("AdditionalInterest");
					
					dataNode = objIntNode.SelectSingleNode("AdditionalInterestInfo/NatureInterestCd");
					
					if ( dataNode != null )
					{
						onjIntInfo.NATURE_OF_INTEREST = dataNode.InnerText.Trim(); 
					}

					currentNode = objIntNode.SelectSingleNode("GeneralPartyInfo/NameInfo");
					
					if ( currentNode != null )
					{
						dataNode = currentNode.SelectSingleNode("CommlName/CommercialName");

						if ( dataNode != null )
						{
							onjIntInfo.HOLDER_NAME = ClsCommon.DecodeXMLCharacters (dataNode.InnerXml.Trim());	
						}
					}
					
					if ( onjIntInfo.HOLDER_NAME == null || onjIntInfo.HOLDER_NAME == "")
					{
						throw new Exception("Holder Name cannot be empty in ACORD XML");

					}

					currentNode = objIntNode.SelectSingleNode("GeneralPartyInfo/Addr");
				
					if ( currentNode != null )
					{
						dataNode = currentNode.SelectSingleNode("Addr1");

						if ( dataNode != null )
						{
							onjIntInfo.HOLDER_ADD1 = dataNode.InnerXml.Trim();	
						}

						dataNode = currentNode.SelectSingleNode("Addr2");

						if ( dataNode != null )
						{
							onjIntInfo.HOLDER_ADD2 = dataNode.InnerXml.Trim();	
						}

						dataNode = currentNode.SelectSingleNode("City");

						if ( dataNode != null )
						{
							onjIntInfo.HOLDER_CITY = dataNode.InnerXml.Trim();	
						}

						dataNode = currentNode.SelectSingleNode("StateProv");

						if ( dataNode != null )
						{
							onjIntInfo.HOLDER_STATE = dataNode.InnerXml.Trim();	
						}
					
						dataNode = currentNode.SelectSingleNode("PostalCode");

						if ( dataNode != null )
						{
							onjIntInfo.HOLDER_ZIP = dataNode.InnerXml.Trim();	
						}
					}
	
					alAddInt.Add(onjIntInfo);
				}

				return alAddInt;
			}
		
			/// <summary>
			/// Returns an array list containing ClsCoveragesInfo objects
			/// </summary>
			/// <param name="objNode"></param>
			/// <returns></returns>
			public ArrayList ParseCoverages(XmlNode objNode)
			{
				XmlNodeList objCoverageNodes = objNode.SelectNodes("Coverage");
			
				if ( objCoverageNodes == null ) return null;

				ArrayList alCoverages = new ArrayList();

				foreach(XmlNode objCovNode in objCoverageNodes )
				{
					Cms.Model.Application.ClsCoveragesInfo objCovInfo = new Cms.Model.Application.ClsCoveragesInfo();

					dataNode = objCovNode.SelectSingleNode("CoverageCd");

					if ( dataNode != null )
					{
						objCovInfo.COVERAGE_CODE = dataNode.InnerText.Trim();
					}
					
					dataNode = objCovNode.SelectSingleNode("CoverageDesc");

					if ( dataNode != null )
					{
						objCovInfo.COV_DESC = dataNode.InnerText.Trim();
					}

					if ( objCovInfo.COVERAGE_CODE == null || objCovInfo.COVERAGE_CODE == "")
					{
						throw new Exception("Coverage code cannot be empty in ACORD XML");
					}


					//Signature Obtained

					XmlNode sigNode = objCovNode.SelectSingleNode("IsSigObt"); 
					if(sigNode != null)
					{
						objCovInfo.SIGNATURE_OBTAINED = sigNode.InnerText.Trim();
					}

					//Limits
					XmlNodeList limitNodes = objCovNode.SelectNodes("Limit");
					
					int i = 0;
					if ( limitNodes != null )
					{
						foreach(XmlNode limitNode in limitNodes)
						{
							if ( i < 2 )
							{
								dataNode = limitNode.SelectSingleNode("FormatCurrencyAmt/Amt");
							
								if ( i == 0 )
								{
									if ( dataNode != null )
									{
										try
										{
											objCovInfo.LIMIT_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
										}
										catch(FormatException ex)
										{
											//objCovInfo.LIMIT_1 = 0;
											throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
										}				
										catch(InvalidCastException ex)
										{
											//objCovInfo.LIMIT_1 = 0;
											throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
										}
										
									}
									
									dataNode = limitNode.SelectSingleNode("Text");
									
									if ( dataNode != null )
									{
										objCovInfo.LIMIT1_AMOUNT_TEXT = dataNode.InnerText.Trim();
									}

								}

								if ( i == 1 )
								{
									if ( dataNode != null )
									{
										try
										{
											objCovInfo.LIMIT_2 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
										}
										catch(FormatException ex)
										{
											throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
										}				
										catch(InvalidCastException ex)
										{
											throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
										}
									}

									dataNode = limitNode.SelectSingleNode("Text");
									
									if ( dataNode != null )
									{
										objCovInfo.LIMIT2_AMOUNT_TEXT = dataNode.InnerText.Trim();
									}
								}
						
								//alCoverages.Add(objCovInfo);
								i++;
							}
						}
					}
					i = 0;

					//Deductibles
					XmlNodeList dedNodes = objCovNode.SelectNodes("Deductible");
					
					if ( dedNodes != null )
					{
						foreach(XmlNode dedNode in dedNodes)
						{
							if ( i < 2 )
							{
								dataNode = dedNode.SelectSingleNode("FormatCurrencyAmt/Amt");
							
								if ( i == 0 )
								{
									if ( dataNode != null )
									{
										try
										{
											objCovInfo.DEDUCTIBLE_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
										}
										catch(FormatException ex)
										{
											throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
										}				
										catch(InvalidCastException ex)
										{
											throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
										}
									}

									dataNode = dedNode.SelectSingleNode("Text");
									
									if ( dataNode != null )
									{
										objCovInfo.DEDUCTIBLE1_AMOUNT_TEXT = dataNode.InnerText.Trim();
									}

								}

								if ( i == 1 )
								{
									if ( dataNode != null )
									{
										try
										{
											objCovInfo.DEDUCTIBLE_2 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
										}
										catch(FormatException ex)
										{
											throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
										}			
										catch(InvalidCastException ex)
										{
											throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
										}
									}

									
									dataNode = dedNode.SelectSingleNode("Text");
									
									if ( dataNode != null )
									{
										objCovInfo.DEDUCTIBLE2_AMOUNT_TEXT = dataNode.InnerText.Trim();
									}
								}
						
						
								i++;
							}
						}
					}

					alCoverages.Add(objCovInfo);
				}

				return alCoverages;
			}


			public ClsPriorPolicyInfo ParseOtherOrPriorPolicy(XmlNode node)
			{
				ClsPriorPolicyInfo objPrior= new ClsPriorPolicyInfo();

				//XmlNode objNode = node.SelectSingleNode("PersPolicy/OtherOrPriorPolicy");
				XmlNode objNode = node.SelectSingleNode(CommonPaths.PriorOrOtherPolicy);

				if ( objNode == null ) return null;

				dataNode = objNode.SelectSingleNode("PolicyNumber");
				
				if ( dataNode != null )
				{
					objPrior.OLD_POLICY_NUMBER = dataNode.InnerText.Trim();
				}
				
				if ( objPrior.OLD_POLICY_NUMBER == null || objPrior.OLD_POLICY_NUMBER == "" )
				{
					return null;
				}

				currentNode = objNode.SelectSingleNode("ContractTerm");
			
				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("EffectiveDt");

					if ( dataNode != null )
					{
						objPrior.EFF_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					}

					dataNode = currentNode.SelectSingleNode("ExpirationDt");

					if ( dataNode != null )
					{
						objPrior.EXP_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					}
					
				}

				return objPrior;
			}
		
			/// <summary>
			/// parse the application nodes
			/// </summary>
			/// <param name="node"></param>
			/// <returns></returns>
			public ClsGeneralInfo ParsePersPolicy(XmlNode node)
			{
				ClsGeneralInfo objApp = new ClsGeneralInfo();

				//Get the application details
				XmlNode objNode = node.SelectSingleNode("PersPolicy");
				
				if ( objNode == null )
				{
					throw new Exception("PersPolicy node not found in ACORD XML.");
				}

				dataNode = objNode.SelectSingleNode("PolicyNumber");

				if ( dataNode != null )
				{
					objApp.APP_NUMBER = dataNode.InnerText.Trim();
				}
			
				dataNode = objNode.SelectSingleNode("PolicyVersion");

				if ( dataNode != null )
				{
					objApp.APP_VERSION = dataNode.InnerText.Trim();
				}
				
				dataNode = objNode.SelectSingleNode("LOBCd");

				if ( dataNode != null )
				{
					objApp.APP_LOB = dataNode.InnerText.Trim();
				}

				//sublob ..if qualifies trailblazer
				dataNode = objNode.SelectSingleNode("SubLOBCd");
				if ( dataNode != null )
				{
					string trailBlazer = dataNode.InnerText.Trim();
					if (trailBlazer.ToUpper().Trim() == "Y")
					{
						objApp.APP_SUBLOB = SUB_LOB_ID_FOR_TRAILBLAZER.ToString().Trim();
					}

				}

				dataNode = objNode.SelectSingleNode("PolicyStatusCd");

				if ( dataNode != null )
				{
					objApp.APP_STATUS = dataNode.InnerText.Trim();
				}
			
				dataNode = objNode.SelectSingleNode("ControllingStateProvCd");

				if ( dataNode != null )
				{
					objApp.STATE_CODE = dataNode.InnerText.Trim();
				}
			
				dataNode = objNode.SelectSingleNode("OriginalInceptionDt");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						objApp.APP_INCEPTION_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					}
				}

				currentNode = objNode.SelectSingleNode("ContractTerm");
			
				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("EffectiveDt");

					if ( dataNode != null )
					{
						if ( dataNode.InnerText != "" )
						{
							objApp.APP_EFFECTIVE_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
						}
					}

					dataNode = currentNode.SelectSingleNode("ExpirationDt");
	
					if ( dataNode != null )
					{
						if ( dataNode.InnerText != "" )
						{
							objApp.APP_EXPIRATION_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
						}
					}

					//add by kranti
					dataNode = currentNode.SelectSingleNode("AppTerms");

					if(dataNode != null)
					{
						if ( dataNode.InnerText != "" )
						{
							objApp.APP_TERMS = dataNode.InnerText.Trim();
						}
					}
					//end
				}
				
				

				//Insurance score/Credit score
				currentNode = objNode.SelectSingleNode("CreditScoreInfo");

				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("CreditScore");

					if ( dataNode != null )
					{
						if ( objApplication.objCustomer != null )
						{
							if(dataNode.InnerText == "NOHITNOSCORE")
								objApplication.objCustomer.CustomerInsuranceScore = -2;
							else
								objApplication.objCustomer.CustomerInsuranceScore = DefaultValues.GetDecimalFromString(dataNode.InnerText.Trim());
						}
					}

				}

				//Perform validations
				if ( objApp.APP_LOB == null || objApp.APP_LOB == "" )
				{
					
					throw new Exception("Application LOB cannot be empty in XML.");
				}
			
				if ( objApp.APP_EFFECTIVE_DATE == DateTime.MinValue )
				{
					
					throw new Exception("Application Effective date cannot be empty in XML.");
				}

				if ( objApp.APP_EXPIRATION_DATE == DateTime.MinValue )
				{
					
					throw new Exception("Application Expiration Date cannot be empty in XML.");
				}

				/////////////////

				return objApp;
			}


			public ClsCustomerInfo ParseInsuredOrPrincipal(XmlNode node)
			{
				ClsCustomerInfo objCustomer = new ClsCustomerInfo();

				//XmlNode objNode = node.SelectSingleNode("InsuredOrPrincipal");
				
				XmlNode objNode = node.SelectSingleNode(CommonPaths.InsuredOrPrincipal);

				if ( objNode == null )
				{
					throw new Exception("InsuredOrPrincipal node not found in ACORD XML.");
				}
					
				bool isPersonal = false;
				bool isCommercial = false;

				//Getting the Personal name of customer/////////////
				currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName");

				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("Surname");

					if ( dataNode != null )
					{	
						objCustomer.CustomerLastName = dataNode.InnerText;
					}
			
					dataNode = currentNode.SelectSingleNode("GivenName");

					if ( dataNode != null )
					{	
						objCustomer.CustomerFirstName = dataNode.InnerText;
					}
					//End of name
					
					
					
				}
				 
				string commercialName = "";
				if (objCustomer.CustomerLastName=="" && objCustomer.CustomerFirstName =="")
				{
					//Getting Commercial Name 
					currentNode = objNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName");
				
					if ( currentNode != null )
					{
						dataNode = currentNode.SelectSingleNode("CommercialName");

						if ( dataNode != null)
						{
							commercialName = dataNode.InnerText.Trim();
							//objCustomer.CustomerFirstName = commercialName;

						}

					
					}
				}
				
				if (objCustomer.CustomerFirstName != "" && objCustomer.CustomerLastName != "" )																				
				{
					isPersonal = true;
					objCustomer.CustomerType = "11110";
				}
				
				if ( commercialName != "" )
				{
					objCustomer.CustomerFirstName = commercialName;
					isCommercial = true;
					objCustomer.CustomerType = "11109";	
				}

				if ( isPersonal && isCommercial )
				{
					throw new Exception("The Customer element has both Personal and Commercial name tags.");
				}
					
				if ( isPersonal )
				{
					if ( objCustomer.CustomerFirstName == null || objCustomer.CustomerFirstName == "" )
					{
						//System.Web.HttpContext.Current.Response.Write("<br>Customer First Name cannot be empty.");
						throw new Exception("Customer First Name cannot be empty.");
					}
			
					if ( objCustomer.CustomerLastName == null || objCustomer.CustomerLastName == "" )
					{
						//System.Web.HttpContext.Current.Response.Write("<br>Customer Last Name cannot be empty.");
						throw new Exception("Customer Last Name cannot be empty.");
					}
				}

				if ( isCommercial )
				{
					if ( commercialName == "" )
					{
						throw new Exception("Customer's Commercial name cannot be empty.");
					}
				}
				
				//Get the address details////////
				XmlNodeList nodeList = objNode.SelectNodes("GeneralPartyInfo/Addr");
			
				foreach(XmlNode addrNode in nodeList)
				{
					string addrType  = "";
				
					dataNode = addrNode.SelectSingleNode("AddrTypeCd");
				
					if ( dataNode != null )
					{
						addrType = dataNode.InnerText;
					}

					if ( addrType == "StreetAddress")
					{
						XmlNode addrDataNode = addrNode.SelectSingleNode("Addr1");

						if ( addrDataNode != null )
						{
							objCustomer.CustomerAddress1 = addrDataNode.InnerText;
						}
					
						addrDataNode = addrNode.SelectSingleNode("Addr2");
					
						if ( addrDataNode != null )
						{
							objCustomer.CustomerAddress2 = addrDataNode.InnerText;
						}
					
						addrDataNode = addrNode.SelectSingleNode("City");
					
						if ( addrDataNode != null )
						{
							objCustomer.CustomerCity = addrDataNode.InnerText;
						}

						addrDataNode = addrNode.SelectSingleNode("StateProvCd");
					
						if ( addrDataNode != null )
						{
							objCustomer.CustomerState = addrDataNode.InnerText;
						}
						
						addrDataNode = addrNode.SelectSingleNode("StateID");
					
						if ( addrDataNode != null )
						{
							objCustomer.CustomerState = addrDataNode.InnerText;
						}

						addrDataNode = addrNode.SelectSingleNode("PostalCode");
					
						if ( addrDataNode != null )
						{
							objCustomer.CustomerZip = addrDataNode.InnerText;
						}

						addrDataNode = addrNode.SelectSingleNode("CountryCd");
					
						if ( addrDataNode != null )
						{
							objCustomer.CustomerCountry = addrDataNode.InnerText;
						}
					}
				}
				//End of address

				//Get the phone details
				nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/PhoneInfo");
			
				foreach(XmlNode phoneNode in nodeList)
				{
					string phoneType  = "";
				
					dataNode = phoneNode.SelectSingleNode("PhoneTypeCd");
				
					if ( dataNode != null )
					{
						phoneType = dataNode.InnerText;
					}
				
					dataNode = phoneNode.SelectSingleNode("CommunicationUseCd");
				
					string commType = "";

					if ( dataNode  != null  )
					{
						commType = dataNode.InnerText;
					}

					switch(phoneType)
					{
						case "Phone":
							if ( commType == "Home" )
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if ( dataNode != null )
								{
									objCustomer.CustomerHomePhone = dataNode.InnerText;
								}
							}
						
							if ( commType == "Business" )
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if ( dataNode != null )
								{
									objCustomer.CustomerBusinessPhone = dataNode.InnerText;
								}
							}
							break;
						case "Cell":
							if ( commType == "Home" )
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if ( dataNode != null )
								{
									objCustomer.CustomerMobile = dataNode.InnerText;
								}
							}

							break;
						case "Pager":
							if ( commType == "Home" )
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if ( dataNode != null )
								{
									objCustomer.CustomerPagerNo = dataNode.InnerText;
								}
							}
							break;
						case "Fax":
							if ( commType == "Home" )
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if ( dataNode != null )
								{
									objCustomer.CustomerFax = dataNode.InnerText;
								}
							}
							break;
					}
				
					//Get the Email details
					nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/EmailInfo");
			
					foreach(XmlNode emailNode in nodeList)
					{
						dataNode = emailNode.SelectSingleNode("EmailAddr");

						if ( dataNode != null )
						{
							objCustomer.CustomerEmail = dataNode.InnerText;
						}

					}
				
					//Website details
					nodeList = objNode.SelectNodes("GeneralPartyInfo/Communications/WebsiteInfo");
			
					foreach(XmlNode emailNode in nodeList)
					{
						dataNode = emailNode.SelectSingleNode("WebsiteURL");

						if ( dataNode != null )
						{
							objCustomer.CustomerWebsite = dataNode.InnerText;
						}
					}

				}
				//End of communications
				
				//Get Customer ID if it exists//////////////////////////
				currentNode = objNode.SelectSingleNode("InsuredOrPrincipalInfo");

				if ( currentNode != null )
				{
					if ( currentNode.Attributes["id"] != null )
					{
						objCustomer.CustomerId = Convert.ToInt32(currentNode.Attributes["id"].Value);
					}
				}
			
				return objCustomer;
			}

		
			public ClsApplicantDetailsInfo ParsePersApplicationInfo(XmlNode node)
			{
				ClsApplicantDetailsInfo objInfo = new ClsApplicantDetailsInfo();

				//this.currentNode = node.SelectSingleNode("PersPolicy/PersApplicationInfo");
				this.currentNode = node.SelectSingleNode(CommonPaths.ApplicantInfo);
				
				if ( currentNode == null ) return null;

				dataNode = currentNode.SelectSingleNode("ResidenceOwnedRentedCd");

				if ( dataNode!= null )
				{
					if ( dataNode.InnerText != "" )
					{
						objInfo.CURR_RES_TYPE = dataNode.InnerText;
					}
				}

				XmlNode addrNode = currentNode.SelectSingleNode("Addr");
			
				StringBuilder sbAddress = new StringBuilder();

				if ( addrNode != null )
				{
					dataNode = addrNode.SelectSingleNode("Addr1");

					if ( dataNode != null )
					{
						sbAddress.Append(dataNode.InnerText);
					}
					
					dataNode = addrNode.SelectSingleNode("Addr2");
					
					if ( dataNode != null )
					{
						sbAddress.Append(dataNode.InnerText);
					}
					
					dataNode = addrNode.SelectSingleNode("City");
					
					if ( dataNode != null )
					{
						sbAddress.Append(dataNode.InnerText);
					}

					dataNode = addrNode.SelectSingleNode("State");
					
					if ( dataNode != null )
					{
						sbAddress.Append(dataNode.InnerText);
					}

					dataNode = addrNode.SelectSingleNode("PostalCode");
					
					if ( dataNode != null )
					{
						sbAddress.Append(dataNode.InnerText);
					}

					dataNode = addrNode.SelectSingleNode("CountryCd");
					
					if ( dataNode != null )
					{
						sbAddress.Append(dataNode.InnerText);
					}
				
					objInfo.PREV_ADD = sbAddress.ToString();
				}
				
				if ( ( objInfo.CURR_RES_TYPE == null || objInfo.CURR_RES_TYPE == "" ) &&
					( objInfo.PREV_ADD == null || objInfo.PREV_ADD == "" )	)
				{
					return null;
				}

				return objInfo;
			}


			public ArrayList ParseLocations(XmlNode nodeApp)
			{
				XmlNodeList locList = nodeApp.SelectNodes("Location");
			
				if ( locList == null ) return null;

				ArrayList alLocations = new ArrayList();

				foreach(XmlNode locNode in locList)
				{
					ClsLocationInfo objLocInfo = new ClsLocationInfo();

					if ( locNode.Attributes["id"] != null )
					{
						objLocInfo.ID = locNode.Attributes["id"].Value;
					}

					Addr[] arAddr = this.ParseAddress(locNode,"Addr");

					if (  arAddr != null && arAddr.Length > 0)
					{
						foreach(Addr obj in arAddr)
						{
							
								objLocInfo.LOC_ADD1 = obj.Addr1;
								objLocInfo.LOC_ADD2 = obj.Addr2;
								objLocInfo.LOC_CITY = obj.City;
								objLocInfo.LOC_COUNTRY = "1";
								objLocInfo.LOC_ZIP = obj.PostalCode;
								objLocInfo.LOC_COUNTY = obj.County;
								objLocInfo.LOC_STATE = obj.StateProv;
								objLocInfo.ADDR_TYPE = obj.AddrTypeCd;
								objLocInfo.TERRITORY = obj.TerritoryCd;
								

						}
					}

					dataNode = locNode.SelectSingleNode("LocationDesc");

					if ( dataNode != null )
					{
						objLocInfo.DESCRIPTION = dataNode.InnerText;
					}
				
					//this.currentNode = locNode.SelectNodes("SubLocation");
				
					ArrayList alSubloc = null;
					if ( this.currentNode != null )
					{
						alSubloc = this.ParseSublocations(locNode);
					}
				
					if ( alSubloc != null )
					{
						objLocInfo.SetSublocations(alSubloc);
					}

					alLocations.Add(objLocInfo);
				}
			
				return alLocations;

			}
		

			/// <summary>
			/// Returns an Arraylist of sub locations for a location
			/// </summary>
			/// <param name="objNode"></param>
			/// <returns></returns>
			public ArrayList ParseSublocations(XmlNode objNode)
			{
				ArrayList alSubloc = null;//new ArrayList();
			
				XmlNodeList subLocList = objNode.SelectNodes("SubLocation");
			
				if ( subLocList == null ) return null;

				foreach(XmlNode subLocNode in subLocList)
				{
					this.dataNode = subLocNode.SelectSingleNode("SubLocationDesc");
				
					ClsSubLocationInfo objInfo = new ClsSubLocationInfo();
				
					if ( dataNode != null )
					{
						objInfo.SUB_LOC_DESC = dataNode.InnerText.Trim();
					}
				
					if ( objNode.Attributes["id"] != null )
					{
						objInfo.ID = objNode.Attributes["id"].Value;
					}

					if ( alSubloc == null )
					{
						alSubloc = new ArrayList();
					}

					alSubloc.Add(objInfo);

				}
			
				return alSubloc;

			}
		
			
			/// <summary>
			/// Parses Gen Info
			/// </summary>
			/// <param name="nodeApp"></param>
			/// <returns></returns>
			public ClsPPGeneralInformationInfo ParseGenInfo(XmlNode nodeApp)
			{
				XmlNode policyNode = nodeApp.SelectSingleNode("PersPolicy");
				
				ClsPPGeneralInformationInfo objInfo = null;

				if ( policyNode != null )
				{
					objInfo = new ClsPPGeneralInformationInfo();

					dataNode = policyNode.SelectSingleNode("MultiPolicy");

					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim().ToLower() == "y" )
						{
							objInfo.MULTI_POLICY_DISC_APPLIED = "1";
						}
						else if (dataNode.InnerText.Trim().ToLower() == "n")
						{
							objInfo.MULTI_POLICY_DISC_APPLIED = "0";
						}
					}

					//Persoanl Umberalla Policy:APPLY_PERS_UMB_POL
					dataNode = policyNode.SelectSingleNode("PersonalUmbrellaPolicy");

					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim().ToLower() == "y" )
						{
							objInfo.APPLY_PERS_UMB_POL = 1;
						}
						else if (dataNode.InnerText.Trim().ToLower() == "n")
						{
							objInfo.APPLY_PERS_UMB_POL = 0;
						}
					}



					// years insured 
					dataNode = policyNode.SelectSingleNode("LengthTimeInsured");
					if ( dataNode != null )
					{
						objInfo.YEARS_INSU = int.Parse (dataNode.InnerText.Trim()==""?"0":dataNode.InnerText.Trim());
					}

					//years insured with wolverine
					dataNode = policyNode.SelectSingleNode("LengthTimeInsuredWithWolverine");
					if ( dataNode != null )
					{
						objInfo.YEARS_INSU_WOL = int.Parse (dataNode.InnerText.Trim()==""?"0":dataNode.InnerText.Trim());
					}

					//Seat Belt credit
					dataNode = policyNode.SelectSingleNode("SeatBeltCredit");
					if ( dataNode != null )
					{
						objInfo.SEAT_BELT_CREDIT ="0";
						string seatBeltCredit=dataNode.InnerText.Trim().ToUpper();
						if (seatBeltCredit == "TRUE")
						{
							objInfo.SEAT_BELT_CREDIT ="1";
						}
					}

					dataNode = policyNode.SelectSingleNode("AnyCycleover40000");				
					if( dataNode != null)
					{
						if(dataNode.InnerText.ToString()!="" )
						{
							if(dataNode.InnerText.ToString().ToUpper().Equals("YES"))
								objInfo.IS_COST_OVER_DEFINED_LIMIT = "1";
							else
								objInfo.IS_COST_OVER_DEFINED_LIMIT = "0";

						}
							
					}

				}			

				return objInfo;
			}


			/// <summary>
			/// Parse UnderTier
			/// </summary>
			/// <param name="nodeApp"></param>
			/// <returns></returns>

			public ClsUnderwritingTierInfo ParseUnderTier(XmlNode nodeApp)
			{
				XmlNode policyNode = nodeApp.SelectSingleNode("PersPolicy");
				
				ClsUnderwritingTierInfo objInfo = null;

				if ( policyNode != null )
				{
					objInfo = new ClsUnderwritingTierInfo();

					dataNode = policyNode.SelectSingleNode("UnderWritingTier");

					if ( dataNode != null )
					{
                        objInfo.UNDERWRITING_TIER = dataNode.InnerText;
					}

					
					dataNode = policyNode.SelectSingleNode("UnderWritingTierDate");

					if ( dataNode != null )
					{
						if(dataNode.InnerText!="")
						{
							objInfo.UNTIER_ASSIGNED_DATE = DateTime.Parse(dataNode.InnerText.ToString());
						}
					}
				}			

				return objInfo;
			}



		}




		public class Addr
		{
			//Addr1 | DetailAddr)
			//"(AddrTypeCd*, (%ADDR1_CHOICE;)?, Addr2?, Addr3?, Addr4?, 
			//City?, City?, StateProv?, PostalCode?, CountryCd?, Country?, Latitude?, Longitude?, County?, (%REGION_CHOICE;)?, Township?, LegalAddr*)">
			//Region | RegionCd
			public string AddrTypeCd;
			public string Addr1;
			public string Addr2;
			public string Addr3;
			public string Addr4;
			public string City;
			public string StateProv;
			public string PostalCode;
			public string CountryCd;
			public string County;
			public string TerritoryCd;
		}

		public class PhoneInfo
		{
			public string PhoneTypeCd;
			public string CommunicationUseCd;
			public string PhoneNumber;
		}

		
		public class DriverViolationInfo
		{
			public string AccidentViolationCd = "";
			public DateTime AccidentViolationDt = DateTime.MinValue;
			public string DriverRef = "";
		}


	}
