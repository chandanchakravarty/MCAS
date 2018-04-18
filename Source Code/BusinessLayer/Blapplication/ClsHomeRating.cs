/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/13/2005 3:59:07 PM
<End Date				: -	
<Description				: - 	Implementing business logic for Home Rating module
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  4/10/2005
<Modified By				: - Mohit Gupta 
<Purpose				: -   Added function GetDefaultTerritory

<Modified Date			: -  16-11-2005
<Modified By			: -  Vijay Arora
<Purpose				: -  Added the Policy Functions.
*******************************************************************************************/ 

using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application.HomeOwners;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;  

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsHomeRating.
	/// </summary>
	public class ClsHomeRating : Cms.BusinessLayer.BlApplication.clsapplication     
	{
		private const	string		APP_HOME_RATING_INFO			=	"APP_HOME_RATING_INFO";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		#endregion

		#region Public Properties
		public bool TransactionLog
		{
			set
			{
				boolTransactionLog	=	value;
			}
			get
			{
				return boolTransactionLog;
			}
		}
		#endregion

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsHomeRating()
		{
			boolTransactionLog	= base.TransactionLogRequired;
       
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objHomeRatingInfo">Model class object.</param>
		/// <param name="strCalledFrom">Name of page from where function called</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Application.HomeOwners.ClsHomeRatingInfo objHomeRatingInfo,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_INSERT_APP_HOME_RATING";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objHomeRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objHomeRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objHomeRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@IS_SUPERVISED",objHomeRatingInfo.IS_SUPERVISED);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objHomeRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objHomeRatingInfo.PROT_CLASS);

				//Home construction
				//objDataWrapper.AddParameter("@NO_OF_APARTMENTS",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_APARTMENTS));
				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objHomeRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objHomeRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objHomeRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objHomeRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objHomeRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objHomeRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objHomeRatingInfo.ROOF_OTHER_DESC);

				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objHomeRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objHomeRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objHomeRatingInfo.MONTH_OCC_EACH_YEAR);
				   
				if(objHomeRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objHomeRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objHomeRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objHomeRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objHomeRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objHomeRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objHomeRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objHomeRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objHomeRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objHomeRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objHomeRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objHomeRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objHomeRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objHomeRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objHomeRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objHomeRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objHomeRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objHomeRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);

				//RPSINGH -- 4 July 2006
				objDataWrapper.AddParameter("@NEED_OF_UNITS",objHomeRatingInfo.NEED_OF_UNITS);
				
                
				//Prot devices
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objHomeRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@TEMPERATURE",objHomeRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objHomeRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objHomeRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objHomeRatingInfo.FIRE_PLACES);
	
				
				if (objHomeRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);				
				}
				//objDataWrapper.AddParameter("@SWIMMING_POOL",objHomeRatingInfo.SWIMMING_POOL);
				//objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objHomeRatingInfo.SWIMMING_POOL_TYPE);

				//--------------------------Added by Mohit for change request issue 742---.
				if (objHomeRatingInfo.IS_UNDER_CONSTRUCTION =="1")
				{
					if(objHomeRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objHomeRatingInfo.DWELLING_CONST_DATE );
					else
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				else
				{
					objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				//----------------------------------End-----------------------------------.
				
				//////////////Pradeep /////////////////
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objHomeRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objHomeRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objHomeRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objHomeRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objHomeRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objHomeRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objHomeRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objHomeRatingInfo.TWO_MORE_FIRE);
				if(objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES==0)
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);				
				objDataWrapper.AddParameter("@SPRINKER",objHomeRatingInfo.SPRINKER);
				////////////////////////
				if(objHomeRatingInfo.ALARM_CERT_ATTACHED != "")				
					objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",objHomeRatingInfo.ALARM_CERT_ATTACHED);
				else
					objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",null);
				//Suburban details
				objDataWrapper.AddParameter("@SUBURBAN_CLASS",objHomeRatingInfo.SUBURBAN_CLASS);
				objDataWrapper.AddParameter("@LOCATED_IN_SUBDIVISION",objHomeRatingInfo.LOCATED_IN_SUBDIVISION);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objHomeRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/HomeOwners/AddHomeRating.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objHomeRatingInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objHomeRatingInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objHomeRatingInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objHomeRatingInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objHomeRatingInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1736", "");// "New home rating information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
				objDataWrapper.ClearParameteres();

				
				ClsHomeCoverages objCoverages;
				if(strCalledFrom.ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();

				}
				objCoverages.UpdateCoveragesByRuleApp(objDataWrapper,objHomeRatingInfo.CUSTOMER_ID,objHomeRatingInfo.APP_ID,
					objHomeRatingInfo.APP_VERSION_ID,RuleType.RiskDependent,objHomeRatingInfo.DWELLING_ID );

				//				//Update relevant Coverages//////////////////////////////////
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				//				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				//				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
				//				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_RATING");
				//				objDataWrapper.ClearParameteres();
				//				/////////////////////////////////////////////////////////////////
				//				
				//				//Update Mandatory endorsements/////////////////
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				//				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				//				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				//				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				//				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_RATING");
				//				//////////////////////////////////////////////
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (returnResult == -1)
				{
					return -1;
				}
				else
				{
                
					return returnResult;
				}
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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldHomeRatingInfo">Model object having old information</param>
		/// <param name="objHomeRatingInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsHomeRatingInfo objOldHomeRatingInfo,ClsHomeRatingInfo objHomeRatingInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UPDATE_APP_HOME_RATING_INFO";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objHomeRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objHomeRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objHomeRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@IS_SUPERVISED",objHomeRatingInfo.IS_SUPERVISED);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objHomeRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				
				objDataWrapper.AddParameter("@PROT_CLASS",objHomeRatingInfo.PROT_CLASS);				

				//RPSINGH - 4 jul 2006
				objDataWrapper.AddParameter("@NEED_OF_UNITS",objHomeRatingInfo.NEED_OF_UNITS);
                
           
				if(objHomeRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objHomeRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objHomeRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objHomeRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objHomeRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objHomeRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objHomeRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objHomeRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objHomeRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objHomeRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objHomeRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objHomeRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objHomeRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objHomeRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objHomeRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objHomeRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objHomeRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objHomeRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);
				
				

				//Home construction
				//objDataWrapper.AddParameter("@NO_OF_APARTMENTS",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_APARTMENTS));
				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				if(objHomeRatingInfo.CONSTRUCTION_CODE==-1)
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objHomeRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objHomeRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objHomeRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objHomeRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objHomeRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objHomeRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objHomeRatingInfo.ROOF_OTHER_DESC);
				//				objDataWrapper.AddParameter("@WIRING",objHomeRatingInfo.WIRING);
				//				if(objHomeRatingInfo.WIRING_LAST_INSPECTED != DateTime.MinValue)
				//					objDataWrapper.AddParameter("@WIRING_LAST_INSPECTED",objHomeRatingInfo.WIRING_LAST_INSPECTED);
				//				else
				//					objDataWrapper.AddParameter("@WIRING_LAST_INSPECTED",null);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objHomeRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objHomeRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objHomeRatingInfo.MONTH_OCC_EACH_YEAR);

					

				//Prot devices
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objHomeRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@TEMPERATURE",objHomeRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objHomeRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objHomeRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objHomeRatingInfo.FIRE_PLACES);
				if (objHomeRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);				
				}
				//objDataWrapper.AddParameter("@SWIMMING_POOL",objHomeRatingInfo.SWIMMING_POOL);
				//objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objHomeRatingInfo.SWIMMING_POOL_TYPE);

				//--------------------------Added by Mohit for change request issue 742---.
				if (objHomeRatingInfo.IS_UNDER_CONSTRUCTION == "1")
				{
					if(objHomeRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
					{
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objHomeRatingInfo.DWELLING_CONST_DATE );
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
				//----------------------------------End-----------------------------------.
				
				//////////////Pradeep /////////////////
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objHomeRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objHomeRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objHomeRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objHomeRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objHomeRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objHomeRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objHomeRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objHomeRatingInfo.TWO_MORE_FIRE);

				
				////////////////////////
				
				if(TransactionLogRequired) 
				{
					objHomeRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/HomeOwners/AddHomeRating.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldHomeRatingInfo,objHomeRatingInfo);                   
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID			=	objHomeRatingInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objHomeRatingInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objHomeRatingInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objHomeRatingInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1735", "");//"Home rating information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				objDataWrapper.ClearParameteres();
				
				//Update relevant Coverages//////////////////////////////////
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_RATING");
				objDataWrapper.ClearParameteres();
				/////////////////////////////////////////////////////////////////
				
				//Update Mandatory endorsements/////////////////
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_RENTAL_ENDORSEMENTS_RATING");
				//////////////////////////////////////////////
				
				
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

		public int Update(ClsHomeRatingInfo objOldHomeRatingInfo,ClsHomeRatingInfo objHomeRatingInfo,string strCalledFrom)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UPDATE_APP_HOME_RATING_INFO";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objHomeRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objHomeRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objHomeRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@IS_SUPERVISED",objHomeRatingInfo.IS_SUPERVISED);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objHomeRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objHomeRatingInfo.PROT_CLASS);				
                
				//RPSINGH - 4 JUL 2006
				objDataWrapper.AddParameter("@NEED_OF_UNITS",objHomeRatingInfo.NEED_OF_UNITS);				

				
                
				if(objHomeRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objHomeRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objHomeRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objHomeRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objHomeRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objHomeRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objHomeRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objHomeRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objHomeRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objHomeRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objHomeRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objHomeRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objHomeRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objHomeRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objHomeRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objHomeRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objHomeRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objHomeRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);
				
				

				//Home construction
				//objDataWrapper.AddParameter("@NO_OF_APARTMENTS",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_APARTMENTS));
				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				if(objHomeRatingInfo.CONSTRUCTION_CODE==-1)
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objHomeRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objHomeRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objHomeRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objHomeRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objHomeRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objHomeRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objHomeRatingInfo.ROOF_OTHER_DESC);
				//				objDataWrapper.AddParameter("@WIRING",objHomeRatingInfo.WIRING);
				//				if(objHomeRatingInfo.WIRING_LAST_INSPECTED != DateTime.MinValue)
				//					objDataWrapper.AddParameter("@WIRING_LAST_INSPECTED",objHomeRatingInfo.WIRING_LAST_INSPECTED);
				//				else
				//					objDataWrapper.AddParameter("@WIRING_LAST_INSPECTED",null);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objHomeRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objHomeRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objHomeRatingInfo.MONTH_OCC_EACH_YEAR);
				//Prot devices
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objHomeRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@TEMPERATURE",objHomeRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objHomeRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objHomeRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objHomeRatingInfo.FIRE_PLACES);
				if (objHomeRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);				
				}
				//objDataWrapper.AddParameter("@SWIMMING_POOL",objHomeRatingInfo.SWIMMING_POOL);
				//objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objHomeRatingInfo.SWIMMING_POOL_TYPE);

				//--------------------------Added by Mohit for change request issue 742---.
				if (objHomeRatingInfo.IS_UNDER_CONSTRUCTION == "1")
				{
					if(objHomeRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
					{
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objHomeRatingInfo.DWELLING_CONST_DATE );
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
				//----------------------------------End-----------------------------------.
				
				//////////////Pradeep /////////////////
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objHomeRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objHomeRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objHomeRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objHomeRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objHomeRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objHomeRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objHomeRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objHomeRatingInfo.TWO_MORE_FIRE);
				if(objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES==0)
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);
				objDataWrapper.AddParameter("@SPRINKER",objHomeRatingInfo.SPRINKER);
				////////////////////////
				if(objHomeRatingInfo.ALARM_CERT_ATTACHED != "")				
					objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",objHomeRatingInfo.ALARM_CERT_ATTACHED);
				else
					objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",null);
				
				// Suburban Class details
				objDataWrapper.AddParameter("@SUBURBAN_CLASS",objHomeRatingInfo.SUBURBAN_CLASS);
				objDataWrapper.AddParameter("@LOCATED_IN_SUBDIVISION",objHomeRatingInfo.LOCATED_IN_SUBDIVISION);

				if(TransactionLogRequired) 
				{
					objHomeRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/HomeOwners/AddHomeRating.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldHomeRatingInfo,objHomeRatingInfo);                   
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.APP_ID			=	objHomeRatingInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objHomeRatingInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objHomeRatingInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objHomeRatingInfo.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1735","");//"Home rating information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				objDataWrapper.ClearParameteres();

				
				ClsHomeCoverages objCoverages;
				if(strCalledFrom.ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();

				}
				
				objCoverages.UpdateCoveragesByRuleApp(objDataWrapper,objHomeRatingInfo.CUSTOMER_ID,objHomeRatingInfo.APP_ID,
					objHomeRatingInfo.APP_VERSION_ID,RuleType.RiskDependent,objHomeRatingInfo.DWELLING_ID );
				
				//				//Update relevant Coverages//////////////////////////////////
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				//				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				//				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
				//				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_RATING");
				//				objDataWrapper.ClearParameteres();
				//				/////////////////////////////////////////////////////////////////
				//				
				//				//Update Mandatory endorsements/////////////////
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
				//				objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
				//				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
				//				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				//				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_RATING");
				//				//////////////////////////////////////////////
				
				
				
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


		#region FETCHING DATA
       
		/// <summary>
		/// Returns the data from APP_HOME_RATING_INFO table
		/// </summary>
		/// <param name="appId"></param>
		/// <param name="customerId"></param>
		/// <param name="appVersionId"></param>
		/// <param name="dwellingId"></param>
		/// <returns></returns>
		public static DataSet GetHomeRatingInfo(int appId,int customerId,int appVersionId,int dwellingId)
		{
			string		strStoredProc	=	"Proc_GetHomeRatingInformation";
			
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

		#region FETCHING PPC
       
		/// <summary>
		/// Returns the data from APP_PPC_STATE_ADD table
		/// Author: Mohit Agarwal
		/// Date: 17-Jan-2007
		/// </summary>
		/// <param name="appId"></param>
		/// <param name="customerId"></param>
		/// <param name="appVersionId"></param>
		/// <param name="dwellingId"></param>
		/// <returns></returns>
		public static string GetHomeRatingPPC(int appId,int customerId,int appVersionId,int dwellingId)
		{
			string		strStoredProc	=	"Proc_GetPPCForApplication";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingId,SqlDbType.Int);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);

				int tabindex = 0;
				string ppc = "";
				if(dsCount != null && dsCount.Tables.Count > 0)
				{
					tabindex = dsCount.Tables.Count-1;
					if(dsCount.Tables[tabindex].Rows.Count > 0)
						ppc = dsCount.Tables[tabindex].Rows[0]["PPC"].ToString();
				}
				return ppc;
			}
			catch(Exception ex)
			{
				throw(ex);
				return "";
			}
		}

		#endregion

		#region FETCHING PPC FOR POLICY
       
		/// <summary>
		/// Returns the data from APP_PPC_STATE_ADD table
		/// Author: Mohit Agarwal
		/// Date: 17-Jan-2007
		/// </summary>
		/// <param name="appId"></param>
		/// <param name="customerId"></param>
		/// <param name="appVersionId"></param>
		/// <param name="dwellingId"></param>
		/// <returns></returns>
		public static string GetHomeRatingPPCForPolicy(int polId,int customerId,int polVersionId,int dwellingId)
		{
			string		strStoredProc	=	"Proc_GetPPCForPolicy";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POL_ID",polId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POL_VERSION_ID",polVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingId,SqlDbType.Int);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);

				int tabindex = 0;
				string ppc = "";
				if(dsCount != null && dsCount.Tables.Count > 0)
				{
					tabindex = dsCount.Tables.Count-1;
					if(dsCount.Tables[tabindex].Rows.Count > 0)
						ppc = dsCount.Tables[tabindex].Rows[0]["PPC"].ToString();
				}
				return ppc;
			}
			catch(Exception ex)
			{
				throw(ex);
				return "";
			}
		}

		#endregion

		public static DataSet  GetYearBuiltOfDewelling(int appId,int customerId,int appVersionId,int dwellingId)
		{
			string		strStoredProc	=	"PROC_GET_YearBuiltOfDewelling";
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingId,SqlDbType.Int);
                

				return objDataWrapper.ExecuteDataSet(strStoredProc);
			
			
			}
			
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		public static int GetYearBuiltOfDewellingForPolicy(int customerId,int polId,int polVersionId,int dwellingId)
		{
			string		strStoredProc	=	"PROC_GET_YearBuiltOfDewellingForPolicy";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",polId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",polVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingId,SqlDbType.Int);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			
			}
			
			catch(Exception ex)
			{
				throw(ex);
			}
			if(dsCount.Tables[0].Rows.Count>0)
			{
				return int.Parse(dsCount.Tables[0].Rows[0]["YEAR_BUILT"].ToString());
			}
			else
			{
				return 0;
			}
	
			

		}

		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objHomeRatingInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Save(Cms.Model.Application.HomeOwners.ClsHomeRatingInfo objHomeRatingInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_Save_APP_HOME_RATING_INFO_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
			objDataWrapper.AddParameter("@HYDRANT_DIST",DefaultValues.GetDoubleNullFromNegative(objHomeRatingInfo.HYDRANT_DIST));
			objDataWrapper.AddParameter("@FIRE_STATION_DIST",DefaultValues.GetDoubleNullFromNegative(objHomeRatingInfo.FIRE_STATION_DIST));
			objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objHomeRatingInfo.IS_UNDER_CONSTRUCTION);
			objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objHomeRatingInfo.EXPERIENCE_CREDIT);
			objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER);
			objDataWrapper.AddParameter("@PERSONAL_LIAB_TER_CODE",objHomeRatingInfo.PERSONAL_LIAB_TER_CODE);
			objDataWrapper.AddParameter("@PROT_CLASS",objHomeRatingInfo.PROT_CLASS);
			if(objHomeRatingInfo.RATING_METHOD!=0)
			{
				objDataWrapper.AddParameter("@RATING_METHOD",objHomeRatingInfo.RATING_METHOD);
			}
                
			//objDataWrapper.AddParameter("@NO_OF_APARTMENTS",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_APARTMENTS));
			objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
			objDataWrapper.AddParameter("@NEED_OF_UNITS",objHomeRatingInfo.NEED_OF_UNITS);

			objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objHomeRatingInfo.EXTERIOR_CONSTRUCTION);
			objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION_CODE",objHomeRatingInfo.EXTERIOR_CONSTRUCTION_CODE);
			objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objHomeRatingInfo.EXTERIOR_OTHER_DESC);
			objDataWrapper.AddParameter("@FOUNDATION",objHomeRatingInfo.FOUNDATION);
			objDataWrapper.AddParameter("@FOUNDATION_CODE",objHomeRatingInfo.FOUNDATION_CODE);
			objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objHomeRatingInfo.FOUNDATION_OTHER_DESC);
			objDataWrapper.AddParameter("@ROOF_TYPE",objHomeRatingInfo.ROOF_TYPE);
			objDataWrapper.AddParameter("@ROOF_TYPE_CODE",objHomeRatingInfo.ROOF_TYPE_CODE);
			objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objHomeRatingInfo.ROOF_OTHER_DESC);
			//objDataWrapper.AddParameter("@WIRING",objHomeRatingInfo.WIRING);
			objDataWrapper.AddParameter("@WIRING_CODE",objHomeRatingInfo.WIRING_CODE);
			//			if(objHomeRatingInfo.WIRING_LAST_INSPECTED != DateTime.MinValue)
			//				objDataWrapper.AddParameter("@WIRING_LAST_INSPECTED",objHomeRatingInfo.WIRING_LAST_INSPECTED);
			//			else
			//				objDataWrapper.AddParameter("@WIRING_LAST_INSPECTED",null);
			objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objHomeRatingInfo.PRIMARY_HEAT_TYPE);
			objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE_CODE",objHomeRatingInfo.PRIMARY_HEAT_TYPE_CODE);
			objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objHomeRatingInfo.SECONDARY_HEAT_TYPE);
			objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE_CODE",objHomeRatingInfo.SECONDARY_HEAT_TYPE_CODE);
			objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objHomeRatingInfo.MONTH_OCC_EACH_YEAR);

			// Commented by mohit as field removed from the page.
			//objDataWrapper.AddParameter("@ADD_COVERAGE_INFO",objHomeRatingInfo.ADD_COVERAGE_INFO);
			// objDataWrapper.AddParameter("@IS_OUTSIDE_STAIR",objHomeRatingInfo.IS_OUTSIDE_STAIR);
			
			// end 
			

			objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objHomeRatingInfo.PROTECTIVE_DEVICES);
			objDataWrapper.AddParameter("@TEMPERATURE",objHomeRatingInfo.TEMPERATURE);
			objDataWrapper.AddParameter("@SMOKE",objHomeRatingInfo.SMOKE);
			objDataWrapper.AddParameter("@BURGLAR",objHomeRatingInfo.BURGLAR);
			objDataWrapper.AddParameter("@BURGLAR_CODE",objHomeRatingInfo.BURGLAR_CODE);
			objDataWrapper.AddParameter("@FIRE_PLACES",objHomeRatingInfo.FIRE_PLACES);
			if (objHomeRatingInfo.NO_OF_WOOD_STOVES==0)
			{
				objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
			}
			else
			{
				objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);				
			}
			//No of alarms 23 jan 2006
			if(objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES==0)
				objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",System.DBNull.Value);
			else
				objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);

			//end alarms
			objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objHomeRatingInfo.CIRCUIT_BREAKERS);
			objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",objHomeRatingInfo.ALARM_CERT_ATTACHED);			

			//objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);
			//objDataWrapper.AddParameter("@SWIMMING_POOL",objHomeRatingInfo.SWIMMING_POOL);
			//objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objHomeRatingInfo.SWIMMING_POOL_TYPE);
			
			//////////////Pradeep /////////////////
			objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objHomeRatingInfo.CENT_ST_BURG_FIRE);
			objDataWrapper.AddParameter("@CENT_ST_FIRE",objHomeRatingInfo.CENT_ST_FIRE);
			objDataWrapper.AddParameter("@CENT_ST_BURG",objHomeRatingInfo.CENT_ST_BURG);

			objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objHomeRatingInfo.DIR_FIRE_AND_POLICE);
			objDataWrapper.AddParameter("@DIR_FIRE",objHomeRatingInfo.DIR_FIRE);
			objDataWrapper.AddParameter("@DIR_POLICE",objHomeRatingInfo.DIR_POLICE);

			objDataWrapper.AddParameter("@LOC_FIRE_GAS",objHomeRatingInfo.LOC_FIRE_GAS);
			objDataWrapper.AddParameter("@TWO_MORE_FIRE",objHomeRatingInfo.TWO_MORE_FIRE);

			objDataWrapper.AddParameter("@SUBURBAN_CLASS",objHomeRatingInfo.SUBURBAN_CLASS);
			objDataWrapper.AddParameter("@LOCATED_IN_SUBDIVISION",objHomeRatingInfo.LOCATED_IN_SUBDIVISION);

			int returnResult = 0;
				
			returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			objDataWrapper.ClearParameteres();

			//Update relevant Coverages//////////////////////////////////
			//Commented by Praveen K
			/*objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
			objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_RATING");
			objDataWrapper.ClearParameteres();*/
			/////////////////////////////////////////////////////////////////
				
			//Update Mandatory endorsements/////////////////
			/*objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objHomeRatingInfo.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objHomeRatingInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
			objDataWrapper.AddParameter("@CALLED_FROM","HOME");
			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_RATING");*/
			//////////////////////////////////////////////
			
			return 1;
			
		}
		
		/// <summary>
		/// Gets the lookups for the Home rating page
		/// </summary>
		/// <returns></returns>
		public static DataSet GetHomeRatingLookup()
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			DataSet dsLookup = objDataWrapper.ExecuteDataSet("Proc_GetHomeRatingLookups");

			return dsLookup;
		}


		public static string GetFRAME_OR_MASONRY()
		{
			string		strStoredProc	=	"Proc_getLOOKUP_FRAME_OR_MASONRY";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			}

			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount.GetXml();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="appId"></param>
		/// <param name="customerId"></param>
		/// <param name="appVersionId"></param>
		/// <param name="dwellingId"></param>
		/// <returns></returns>
		public static int GetDefaultTerritory(int customerId,int appId,int appVersionId,int dwellingId)
		{
			string		strStoredProc	=	"Proc_DefaultTerritory";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingId,SqlDbType.Int);
                
			
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
				if ( dsCount.Tables[0].Rows.Count > 0)
				{
					return int.Parse(dsCount.Tables[0].Rows[0][0].ToString());			
				}
				else
				{
					return 0;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			
		}


		#region Policy Functions 

		
		/// <summary>
		/// Returns the Policy Home Rating Details.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public static DataSet GetPolicyHomeRatingInfo(int customerID,int policyID,int policyVersionID,int dwellingID)
		{
			string		strStoredProc	=	"Proc_GetPolicyHomeRatingInformation";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingID ,SqlDbType.Int);
                

				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			return dsCount;
		}

		
		/// <summary>
		/// Returns the Policy Default Territory.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public static int GetPolicyDefaultTerritory(int customerID,int policyID,int policyVersionID,int dwellingID)
		{
			string		strStoredProc	=	"Proc_GetPolicyDefaultTerritory";
			
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@DWELLING_ID",dwellingID,SqlDbType.Int);
                
			
				dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
				if ( dsCount.Tables[0].Rows.Count > 0)
				{
					return int.Parse(dsCount.Tables[0].Rows[0][0].ToString());			
				}
				else
				{
					return 0;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
	
			
		}

		/// <summary>
		/// Saves the Policy Home Rating Info Details.
		/// </summary>
		/// <param name="objHomeRatingInfo"></param>
		/// <returns></returns>
		//START
		public int AddPolicyHomeRatingInfo(Cms.Model.Policy.Homeowners.ClsHomeRatingInfo   objHomeRatingInfo,string strCalledFrom)
		{
			string		strStoredProc	=	"Proc_INSERT_POL_HOME_RATING";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objHomeRatingInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objHomeRatingInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objHomeRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objHomeRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objHomeRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@IS_SUPERVISED",objHomeRatingInfo.IS_SUPERVISED);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objHomeRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objHomeRatingInfo.PROT_CLASS);

				//RP
				objDataWrapper.AddParameter("@NEED_OF_UNITS",objHomeRatingInfo.NEED_OF_UNITS);


				//Home construction
				//objDataWrapper.AddParameter("@NO_OF_APARTMENTS",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_APARTMENTS));
				//objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				if(objHomeRatingInfo.NO_OF_FAMILIES==0)
					objDataWrapper.AddParameter("@NO_OF_FAMILIES",null);
				else
					objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objHomeRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objHomeRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objHomeRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objHomeRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objHomeRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objHomeRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objHomeRatingInfo.ROOF_OTHER_DESC);

				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objHomeRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objHomeRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objHomeRatingInfo.MONTH_OCC_EACH_YEAR);
				
				if(objHomeRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objHomeRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objHomeRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objHomeRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objHomeRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objHomeRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objHomeRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objHomeRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objHomeRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objHomeRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objHomeRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objHomeRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objHomeRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objHomeRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objHomeRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objHomeRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objHomeRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objHomeRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);


				
                
				//Prot devices
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objHomeRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@TEMPERATURE",objHomeRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objHomeRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objHomeRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objHomeRatingInfo.FIRE_PLACES);

				
				if (objHomeRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);				
				}
				//objDataWrapper.AddParameter("@SWIMMING_POOL",objHomeRatingInfo.SWIMMING_POOL);
				//objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objHomeRatingInfo.SWIMMING_POOL_TYPE);

				//--------------------------Added by Mohit for change request issue 742---.
				if (objHomeRatingInfo.IS_UNDER_CONSTRUCTION =="1")
				{
					if(objHomeRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objHomeRatingInfo.DWELLING_CONST_DATE );
					else
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				else
				{
					objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				//----------------------------------End-----------------------------------.
				
				//////////////Pradeep /////////////////
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objHomeRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objHomeRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objHomeRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objHomeRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objHomeRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objHomeRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objHomeRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objHomeRatingInfo.TWO_MORE_FIRE);
				if(objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES==0)
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);				
				objDataWrapper.AddParameter("@SPRINKER",objHomeRatingInfo.SPRINKER);

				if(objHomeRatingInfo.ALARM_CERT_ATTACHED != "")				
					objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",objHomeRatingInfo.ALARM_CERT_ATTACHED);
				else
					objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",null);

				//Suburban details
				objDataWrapper.AddParameter("@SUBURBAN_CLASS",objHomeRatingInfo.SUBURBAN_CLASS);
				objDataWrapper.AddParameter("@LOCATED_IN_SUBDIVISION",objHomeRatingInfo.LOCATED_IN_SUBDIVISION);
				////////////////////////
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objHomeRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/HomeOwner/PolicyAddHomeRating.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objHomeRatingInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objHomeRatingInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objHomeRatingInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objHomeRatingInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objHomeRatingInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1736", "");// "New home rating information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
				objDataWrapper.ClearParameteres();
				//Added By Ravindra By 07-04-2006
				
				ClsHomeCoverages objCoverages;
				if(strCalledFrom.ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}


				objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objHomeRatingInfo.CUSTOMER_ID,
					objHomeRatingInfo.POLICY_ID,
					objHomeRatingInfo.POLICY_VERSION_ID ,
					RuleType.RiskDependent,
					objHomeRatingInfo.DWELLING_ID);

				





				objDataWrapper.ClearParameteres();

				
				//				//Update relevant Coverages//////////////////////////////////
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@POL_ID",objHomeRatingInfo.POLICY_ID);
				//				objDataWrapper.AddParameter("@POL_VERSION_ID",objHomeRatingInfo.POLICY_VERSION_ID);
				//				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
				//				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_RATING_FOR_POLICY");
				//				objDataWrapper.ClearParameteres();
				//				/////////////////////////////////////////////////////////////////
				//				
				//				//Update Mandatory endorsements/////////////////
				//				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				//				objDataWrapper.AddParameter("@POL_ID",objHomeRatingInfo.POLICY_ID);
				//				objDataWrapper.AddParameter("@POL_VERSION_ID",objHomeRatingInfo.POLICY_VERSION_ID);
				//				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				//				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
				//				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_RATING_FOR_POLICY");
				//				//////////////////////////////////////////////
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (returnResult == -1)
				{
					return -1;
				}
				else
				{
                
					return returnResult;
				}
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



		//END
		
		public int AddPolicyHomeRatingInfo(Cms.Model.Policy.Homeowners.ClsHomeRatingInfo objHomeRatingInfo)
		{
			string		strStoredProc	=	"Proc_INSERT_POL_HOME_RATING_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objHomeRatingInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objHomeRatingInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objHomeRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objHomeRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objHomeRatingInfo.IS_UNDER_CONSTRUCTION);
				
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objHomeRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objHomeRatingInfo.PROT_CLASS);

				//RP
				objDataWrapper.AddParameter("@NEED_OF_UNITS",objHomeRatingInfo.NEED_OF_UNITS);


				if(objHomeRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objHomeRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objHomeRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objHomeRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objHomeRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objHomeRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objHomeRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objHomeRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objHomeRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objHomeRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objHomeRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objHomeRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objHomeRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objHomeRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objHomeRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objHomeRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objHomeRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objHomeRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);

				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				objDataWrapper.AddParameter("@CONSTRUCTION_CODE",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.CONSTRUCTION_CODE));
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objHomeRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objHomeRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objHomeRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objHomeRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objHomeRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objHomeRatingInfo.ROOF_OTHER_DESC);

				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objHomeRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objHomeRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objHomeRatingInfo.MONTH_OCC_EACH_YEAR);
				
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objHomeRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@TEMPERATURE",objHomeRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objHomeRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objHomeRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objHomeRatingInfo.FIRE_PLACES);
				
				if (objHomeRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);				
				}
				
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				
				if (objHomeRatingInfo.IS_UNDER_CONSTRUCTION =="1")
				{
					if(objHomeRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objHomeRatingInfo.DWELLING_CONST_DATE );
					else
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				else
				{
					objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}

				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objHomeRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objHomeRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objHomeRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objHomeRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objHomeRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objHomeRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objHomeRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objHomeRatingInfo.TWO_MORE_FIRE);
				//objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);				
				if(objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES==0)
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objHomeRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/HomeOwner/PolicyAddHomeRating.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objHomeRatingInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID = objHomeRatingInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objHomeRatingInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objHomeRatingInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objHomeRatingInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1737", "");// "New policy home rating information is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (returnResult == -1)
				{
					return -1;
				}
				else
				{
                
					return returnResult;
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

		//START
		public  int UpdatePolicyHomeRatingInfo(Cms.Model.Policy.Homeowners.ClsHomeRatingInfo objOldHomeRatingInfo,Cms.Model.Policy.Homeowners.ClsHomeRatingInfo objHomeRatingInfo,string strCalledFrom)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UPDATE_POL_HOME_RATING_INFO";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objHomeRatingInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objHomeRatingInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objHomeRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objHomeRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objHomeRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@IS_SUPERVISED",objHomeRatingInfo.IS_SUPERVISED);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objHomeRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objHomeRatingInfo.PROT_CLASS);				

				//RP
				objDataWrapper.AddParameter("@NEED_OF_UNITS",objHomeRatingInfo.NEED_OF_UNITS);				
                
				if(objHomeRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objHomeRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objHomeRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objHomeRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objHomeRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objHomeRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objHomeRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objHomeRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objHomeRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objHomeRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objHomeRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objHomeRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objHomeRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objHomeRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objHomeRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objHomeRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objHomeRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objHomeRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);
				
				

				//Home construction
				//objDataWrapper.AddParameter("@NO_OF_APARTMENTS",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_APARTMENTS));
				//objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				if(objHomeRatingInfo.CONSTRUCTION_CODE==-1)
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@CONSTRUCTION_CODE",objHomeRatingInfo.CONSTRUCTION_CODE);
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objHomeRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objHomeRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objHomeRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objHomeRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objHomeRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objHomeRatingInfo.ROOF_OTHER_DESC);
				//				objDataWrapper.AddParameter("@WIRING",objHomeRatingInfo.WIRING);
				//				if(objHomeRatingInfo.WIRING_LAST_INSPECTED != DateTime.MinValue)
				//					objDataWrapper.AddParameter("@WIRING_LAST_INSPECTED",objHomeRatingInfo.WIRING_LAST_INSPECTED);
				//				else
				//					objDataWrapper.AddParameter("@WIRING_LAST_INSPECTED",null);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objHomeRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objHomeRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objHomeRatingInfo.MONTH_OCC_EACH_YEAR);

				//Prot devices
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objHomeRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@TEMPERATURE",objHomeRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objHomeRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objHomeRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objHomeRatingInfo.FIRE_PLACES);
				if (objHomeRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);				
				}
				//objDataWrapper.AddParameter("@SWIMMING_POOL",objHomeRatingInfo.SWIMMING_POOL);
				//objDataWrapper.AddParameter("@SWIMMING_POOL_TYPE",objHomeRatingInfo.SWIMMING_POOL_TYPE);

				//--------------------------Added by Mohit for change request issue 742---.
				if (objHomeRatingInfo.IS_UNDER_CONSTRUCTION == "1")
				{
					if(objHomeRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
					{
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objHomeRatingInfo.DWELLING_CONST_DATE );
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
				//----------------------------------End-----------------------------------.
				
				//////////////Pradeep /////////////////
				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objHomeRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objHomeRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objHomeRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objHomeRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objHomeRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objHomeRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objHomeRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objHomeRatingInfo.TWO_MORE_FIRE);
				if(objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES==0)
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);
				objDataWrapper.AddParameter("@SPRINKER",objHomeRatingInfo.SPRINKER);
				////////////////////////
				if(objHomeRatingInfo.ALARM_CERT_ATTACHED != "")				
					objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",objHomeRatingInfo.ALARM_CERT_ATTACHED);
				else
					objDataWrapper.AddParameter("@ALARM_CERT_ATTACHED",null);

				// Suburban Class details
				objDataWrapper.AddParameter("@SUBURBAN_CLASS",objHomeRatingInfo.SUBURBAN_CLASS);
				objDataWrapper.AddParameter("@LOCATED_IN_SUBDIVISION",objHomeRatingInfo.LOCATED_IN_SUBDIVISION);

				if(TransactionLogRequired) 
				{
					objHomeRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/HomeOwner/PolicyAddHomeRating.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldHomeRatingInfo,objHomeRatingInfo);                   
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	                    
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.POLICY_ID 		=	objHomeRatingInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID 	=	objHomeRatingInfo.POLICY_VERSION_ID ;
						objTransactionInfo.CLIENT_ID		=	objHomeRatingInfo.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objHomeRatingInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1735", "");//"Home rating information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				objDataWrapper.ClearParameteres();
				
//				//Update relevant Coverages//////////////////////////////////
//				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
//				objDataWrapper.AddParameter("@POL_ID",objHomeRatingInfo.POLICY_ID);
//				objDataWrapper.AddParameter("@POL_VERSION_ID",objHomeRatingInfo.POLICY_VERSION_ID);
//				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
//				objDataWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FROM_RATING_FOR_POLICY");
//				objDataWrapper.ClearParameteres();
//				/////////////////////////////////////////////////////////////////
//				
//				//Update Mandatory endorsements/////////////////
//				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
//				objDataWrapper.AddParameter("@POL_ID",objHomeRatingInfo.POLICY_ID);
//				objDataWrapper.AddParameter("@POL_VERSION_ID",objHomeRatingInfo.POLICY_VERSION_ID);
//				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);				
//				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);
//				objDataWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_RATING_FOR_POLICY");
//				//////////////////////////////////////////////
				

				ClsHomeCoverages objCoverages;
				if(strCalledFrom.ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
				}


				objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objHomeRatingInfo.CUSTOMER_ID,
					objHomeRatingInfo.POLICY_ID,
					objHomeRatingInfo.POLICY_VERSION_ID ,
					RuleType.RiskDependent,
					objHomeRatingInfo.DWELLING_ID);
				
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




		//END


		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldHomeRatingInfo">Model object having old information</param>
		/// <param name="objHomeRatingInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdatePolicyHomeRatingInfo(Cms.Model.Policy.Homeowners.ClsHomeRatingInfo objOldHomeRatingInfo,Cms.Model.Policy.Homeowners.ClsHomeRatingInfo objHomeRatingInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UPDATEPOL_HOME_RATING_INFO";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objHomeRatingInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objHomeRatingInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objHomeRatingInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objHomeRatingInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@HYDRANT_DIST",objHomeRatingInfo.HYDRANT_DIST);
				objDataWrapper.AddParameter("@FIRE_STATION_DIST",objHomeRatingInfo.FIRE_STATION_DIST);
				objDataWrapper.AddParameter("@IS_UNDER_CONSTRUCTION",objHomeRatingInfo.IS_UNDER_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXPERIENCE_CREDIT",objHomeRatingInfo.EXPERIENCE_CREDIT);
				objDataWrapper.AddParameter("@IS_AUTO_POL_WITH_CARRIER",objHomeRatingInfo.IS_AUTO_POL_WITH_CARRIER);
				objDataWrapper.AddParameter("@PROT_CLASS",objHomeRatingInfo.PROT_CLASS);
				//RP
				objDataWrapper.AddParameter("@NEED_OF_UNITS",objHomeRatingInfo.NEED_OF_UNITS);				

				if(objHomeRatingInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objHomeRatingInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objHomeRatingInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objHomeRatingInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objHomeRatingInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objHomeRatingInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objHomeRatingInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objHomeRatingInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objHomeRatingInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objHomeRatingInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objHomeRatingInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objHomeRatingInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objHomeRatingInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objHomeRatingInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objHomeRatingInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objHomeRatingInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objHomeRatingInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objHomeRatingInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objHomeRatingInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);

				objDataWrapper.AddParameter("@NO_OF_FAMILIES",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.NO_OF_FAMILIES));
				objDataWrapper.AddParameter("@CONSTRUCTION_CODE",DefaultValues.GetIntNullFromNegative(objHomeRatingInfo.CONSTRUCTION_CODE));
				objDataWrapper.AddParameter("@EXTERIOR_CONSTRUCTION",objHomeRatingInfo.EXTERIOR_CONSTRUCTION);
				objDataWrapper.AddParameter("@EXTERIOR_OTHER_DESC",objHomeRatingInfo.EXTERIOR_OTHER_DESC);
				objDataWrapper.AddParameter("@FOUNDATION",objHomeRatingInfo.FOUNDATION);
				objDataWrapper.AddParameter("@FOUNDATION_OTHER_DESC",objHomeRatingInfo.FOUNDATION_OTHER_DESC);
				objDataWrapper.AddParameter("@ROOF_TYPE",objHomeRatingInfo.ROOF_TYPE);
				objDataWrapper.AddParameter("@ROOF_OTHER_DESC",objHomeRatingInfo.ROOF_OTHER_DESC);

				objDataWrapper.AddParameter("@PRIMARY_HEAT_TYPE",objHomeRatingInfo.PRIMARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_TYPE",objHomeRatingInfo.SECONDARY_HEAT_TYPE);
				objDataWrapper.AddParameter("@MONTH_OCC_EACH_YEAR",objHomeRatingInfo.MONTH_OCC_EACH_YEAR);
				
				objDataWrapper.AddParameter("@PROTECTIVE_DEVICES",objHomeRatingInfo.PROTECTIVE_DEVICES);
				objDataWrapper.AddParameter("@TEMPERATURE",objHomeRatingInfo.TEMPERATURE);
				objDataWrapper.AddParameter("@SMOKE",objHomeRatingInfo.SMOKE);
				objDataWrapper.AddParameter("@BURGLAR",objHomeRatingInfo.BURGLAR);
				objDataWrapper.AddParameter("@FIRE_PLACES",objHomeRatingInfo.FIRE_PLACES);
				
				if (objHomeRatingInfo.NO_OF_WOOD_STOVES==0)
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",null);
				}
				else
				{
					objDataWrapper.AddParameter("@NO_OF_WOOD_STOVES",objHomeRatingInfo.NO_OF_WOOD_STOVES);				
				}
				
				objDataWrapper.AddParameter("@PRIMARY_HEAT_OTHER_DESC",objHomeRatingInfo.PRIMARY_HEAT_OTHER_DESC);
				objDataWrapper.AddParameter("@SECONDARY_HEAT_OTHER_DESC",objHomeRatingInfo.SECONDARY_HEAT_OTHER_DESC);
				
				if (objHomeRatingInfo.IS_UNDER_CONSTRUCTION =="1")
				{
					if(objHomeRatingInfo.DWELLING_CONST_DATE != DateTime.MinValue)
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",objHomeRatingInfo.DWELLING_CONST_DATE );
					else
						objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}
				else
				{
					objDataWrapper.AddParameter("@DWELLING_CONST_DATE",null );
				}

				objDataWrapper.AddParameter("@CENT_ST_BURG_FIRE",objHomeRatingInfo.CENT_ST_BURG_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_FIRE",objHomeRatingInfo.CENT_ST_FIRE);
				objDataWrapper.AddParameter("@CENT_ST_BURG",objHomeRatingInfo.CENT_ST_BURG);

				objDataWrapper.AddParameter("@DIR_FIRE_AND_POLICE",objHomeRatingInfo.DIR_FIRE_AND_POLICE);
				objDataWrapper.AddParameter("@DIR_FIRE",objHomeRatingInfo.DIR_FIRE);
				objDataWrapper.AddParameter("@DIR_POLICE",objHomeRatingInfo.DIR_POLICE);

				objDataWrapper.AddParameter("@LOC_FIRE_GAS",objHomeRatingInfo.LOC_FIRE_GAS);
				objDataWrapper.AddParameter("@TWO_MORE_FIRE",objHomeRatingInfo.TWO_MORE_FIRE);
				//objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);
				if(objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES==0)
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@NUM_LOC_ALARMS_APPLIES",objHomeRatingInfo.NUM_LOC_ALARMS_APPLIES);				
				if(TransactionLogRequired) 
				{
					strTranXML = objBuilder.GetTransactionLogXML(objOldHomeRatingInfo,objHomeRatingInfo);

					objHomeRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/Homeowner/PolicyAddHomeRating.aspx.resx");
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.POLICY_ID			=	objHomeRatingInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objHomeRatingInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objHomeRatingInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objHomeRatingInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1738", "");// "Policy home rating information is modified";
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

		#endregion


	}
}

#endregion
