/******************************************************************************************
<Author				: -  Pradeep Iyer
<Start Date			: -	5/12/2005
<End Date			: -	
<Description		: - Business Logic for Application dwelling details
<Review Date		: - 
<Reviewed By			: - 	
Modification History
<Modified Date		: - Anshuman
<Modified By		: - June 07, 2005
<Purpose			: - transaction description modified

<Modified Date		: - Mohit
<Modified By		: - 23/09/2005
<Purpose			: - Code for fields(IS_VACENT_OCCUPY,IS_RENTED_IN_PART,IS_DWELLING_OWNED_BY_OTHER,COMMENT_DWELLING_OWNED) 
<					: -	are commented from the functions as droped from the table. 

*******************************************************************************************/ 

using System;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Policy.Homeowners;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication.HomeOwners;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsDwellingDetails.
	/// </summary>
	public class ClsDwellingDetails : Cms.BusinessLayer.BlApplication.clsapplication
	{
		public ClsDwellingDetails()
		{
			//
			// TODO: Add constructor logic here
			//
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}

		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_DWELLINGS_INFO";
		

         
		#region GetRemaining Location
		public DataTable GetPolicyRemainingLocations(int customerID, int polID, 
			int polVersionID)
		{
			string	strStoredProc =	"Proc_GetRemainingLocations_FOR_POLICY";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds.Tables[0];
		}
       #endregion
		
		/// <summary>
		/// Gets the remaining locations for an application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		public DataTable GetRemainingLocations(int customerID, int appID, 
											int appVersionID)
		{
			string	strStoredProc =	"Proc_GetRemainingLocationsForApplication";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds.Tables[0];
		}

		/// <summary>
		/// Gets the remaining locations for an application
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <returns></returns>
		public DataTable GetRemainingLocationsPolicy(int customerID, int polID, 
			int polVersionID)
		{
			string	strStoredProc =	"Proc_GetRemainingLocationsForPolicy";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds.Tables[0];
		}

		/// <summary>
		/// Inserts a record into the database
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Getpolicyid(int customerID, int APP_ID,int APP_VERSION_ID)
		{
			string strStoredProc= "Proc_GetPolicyType";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);
			SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@POLICYID",0,SqlDbType.Int,ParameterDirection.Output);
			objWrapper.ExecuteNonQuery(strStoredProc);
				
			int POLICYID = int.Parse(objSqlParameter.Value.ToString());
			return POLICYID;
	
				
					

		}


//GET THE policy Type and State

		public DataSet GetPolicyStateForPolicy(string CUSTOMER_ID, string POLICY_ID,string POLICY_VERSION_ID)
		{
			DataSet dsPolicyState=new DataSet();
			string strStoredProc= "Proc_PolGetPolicyState";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objWrapper.AddParameter("@CUSTOMER_ID",CUSTOMER_ID);
			objWrapper.AddParameter("@POLICY_ID",POLICY_ID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",POLICY_VERSION_ID);				
			dsPolicyState=objWrapper.ExecuteDataSet(strStoredProc);
				
			if(dsPolicyState!=null)
				return dsPolicyState;
			else
				return null;
					

		}

		
		public DataSet GetPolicyState(string customerID, string APP_ID,string APP_VERSION_ID)
		{
			DataSet dsPolicyState=new DataSet();
			string strStoredProc= "Proc_AppGetPolicyState";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);				
			dsPolicyState=objWrapper.ExecuteDataSet(strStoredProc);
			
			if(dsPolicyState!=null)
				return dsPolicyState;
			else
				return null;
				

			}
		public DataSet GetEffectiveDate(string customerID, string APP_ID,string APP_VERSION_ID,string CALLED_FROM)
		{
			DataSet dsEffDate=new DataSet();
			string strStoredProc= "Proc_AppGetEffDate";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",APP_VERSION_ID);
			objWrapper.AddParameter("@CALLED_FROM",CALLED_FROM);
				
			dsEffDate=objWrapper.ExecuteDataSet(strStoredProc);
			
			if(dsEffDate!=null)
				return dsEffDate;
			else
				return null;
				

		}

		/// <summary>
		/// Adds a dwelling in the database
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Add(ClsDwellingDetailsInfo objInfo)
		{
			return Add(objInfo,"","");
		}
		

		public int Add(ClsDwellingDetailsInfo objInfo,string strCustomInfo,string strLOB)
		{
			string	strStoredProc =	"Proc_InsertAppDwellingsInfo";

			string strTranXML = "";
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddDwellingDetails.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@REPLACEMENTCOST_COVA",objInfo.REPLACEMENTCOST_COVA);
			objWrapper.AddParameter("@BUILDING_TYPE",objInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objInfo.NEIGHBOURS_VISIBLE);
			//objWrapper.AddParameter("@IS_VACENT_OCCUPY",objInfo.IS_VACENT_OCCUPY);
			//objWrapper.AddParameter("@IS_RENTED_IN_PART",objInfo.IS_RENTED_IN_PART);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objInfo.NO_WEEKS_RENTED));
			//objWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objInfo.IS_DWELLING_OWNED_BY_OTHER);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			//objWrapper.AddParameter("@COMMENT_DWELLING_OWNED",objInfo.COMMENT_DWELLING_OWNED);
			objWrapper.AddParameter("@IS_ACTIVE",objInfo.IS_ACTIVE);

			//RPSINGH - start
			objWrapper.AddParameter("@DETACHED_OTHER_STRUCTURES",objInfo.DETACHED_OTHER_STRUCTURES);
			
			objWrapper.AddParameter("@MONTHS_RENTED",DefaultValues.GetDoubleNullFromNegative(objInfo.MONTHS_RENTED));
			//RPSINGH - end
			
			SqlParameter sqlParamDwellingID = (SqlParameter) objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Output);
			
			int dwellingID = 0;

			try
			{
				if ( strTranXML.Trim() == ""  || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
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
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1696", "");// "New Dwelling details/coverage is added.";
					//objTransactionInfo.TRANS_DESC		=	"New Dwelling Info is added.";
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='DWELLING_NUMBER']");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='LOCATION_ID']");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strCustomInfo != "")
					{
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					}
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				
				dwellingID = Convert.ToInt32(sqlParamDwellingID.Value);
				
				//If negative value returned for some reason
				if ( dwellingID <= 0 )
				{
					objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return dwellingID;
				}
				
				//Added by ravindra(06-14-2006)
				objWrapper.ClearParameteres();

				ClsHomeCoverages objCoverage;

				if(strLOB.ToString().ToUpper().Trim().Equals("RENTAL"))
				{
					objCoverage = new ClsHomeCoverages("1");
				}
				else
				{
                   objCoverage = new ClsHomeCoverages();
					objCoverage.UpdateCoveragesByRuleApp(objWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID  ,
						objInfo.APP_VERSION_ID,RuleType.OtherAppDependent);
					
					objWrapper.ClearParameteres();
					objCoverage.UpdateCoveragesByRuleApp(objWrapper,objInfo.CUSTOMER_ID,objInfo.APP_ID  ,
					objInfo.APP_VERSION_ID,RuleType.LobDependent);
					objWrapper.ClearParameteres();
					if(ClsCommon.CheckWatercraft(objInfo.CUSTOMER_ID,objInfo.APP_ID,objInfo.APP_VERSION_ID))
					{
						objCoverage.UpdateCoveragesByRuleApp (objWrapper,objInfo.CUSTOMER_ID,
							objInfo.APP_ID,
							objInfo.APP_VERSION_ID,RuleType.LobDependent);
						objWrapper.ClearParameteres();
					}
				}
				objCoverage.SaveDefaultCoveragesApp (objWrapper,objInfo.CUSTOMER_ID,
													objInfo.APP_ID,
													objInfo.APP_VERSION_ID,
													dwellingID);

				


				
//				//Update coverages/////////////////////////////////////////////////////
//				objWrapper.ClearParameteres();
//
//				objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
//				objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",Convert.ToInt32(sqlParamDwellingID.Value));
//				
//				objWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES");
//				////////////////////////////////////////////////////////////////////////
//				
//				//Update Mandatory endorsements///////////////////////////////////
//				objWrapper.ClearParameteres();
//
//				objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
//				objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",Convert.ToInt32(sqlParamDwellingID.Value));
//				
//				objWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS");
//				///////////////////////////////////////////////////////////////////
//				
//				//Adjust Coverages//////////////////////////////
//				objWrapper.ClearParameteres();
//
//				objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
//				objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",Convert.ToInt32(sqlParamDwellingID.Value));
//				
//				objWrapper.ExecuteNonQuery("Proc_ADJUST_DWELLING_COVERAGES");
//				//////////////////////
//				
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			

			return dwellingID;
			

		}
		
		
		/// <summary>
		/// Inserts a record into the database
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		/// 
		public int Add(ClsPolicyDwellingDetailsInfo objInfo,string strLOB)
		{
			return Add(objInfo,"",strLOB);
		}
		public int Add(ClsPolicyDwellingDetailsInfo objInfo,string strCustomInfo,string strLOB)
		{
		
			string	strStoredProc =	"Proc_InsertPolDwellingsInfo";

			string strTranXML = "";
			int dwellingID=0;
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/Homeowner/PolicyAddDwellingDetails.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objInfo);
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POL_ID",objInfo.POLICY_ID);
			objWrapper.AddParameter("@POL_VERSION_ID",objInfo.POLICY_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@REPLACEMENTCOST_COVA",objInfo.REPLACEMENTCOST_COVA);
			objWrapper.AddParameter("@BUILDING_TYPE",objInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objInfo.NEIGHBOURS_VISIBLE);
			//objWrapper.AddParameter("@IS_VACENT_OCCUPY",objInfo.IS_VACENT_OCCUPY);
			//objWrapper.AddParameter("@IS_RENTED_IN_PART",objInfo.IS_RENTED_IN_PART);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objInfo.NO_WEEKS_RENTED));
			//objWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objInfo.IS_DWELLING_OWNED_BY_OTHER);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			//objWrapper.AddParameter("@COMMENT_DWELLING_OWNED",objInfo.COMMENT_DWELLING_OWNED);
			objWrapper.AddParameter("@MONTHS_RENTED",DefaultValues.GetDoubleNullFromNegative(objInfo.MONTHS_RENTED));
			

			SqlParameter sqlParamDwellingID = (SqlParameter) objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Output);
			



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
					objTransactionInfo.POLICY_ID  = objInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1696", "");// "New Dwelling details/coverage is added.";
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='DWELLING_NUMBER']");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='LOCATION_ID']");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strCustomInfo != "")
					{
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					}
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
			
                
				dwellingID = Convert.ToInt32(sqlParamDwellingID.Value);
				
				//If negative value returned for some reason
				if ( dwellingID <= 0 )
				{
					objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					return dwellingID;
				}
				
				//Added by ravindra(06-14-2006)
				objWrapper.ClearParameteres();
				ClsHomeCoverages objCoverage ;
				if(strLOB.ToUpper().Equals("RENTAL"))
				{
					objCoverage=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverage=new ClsHomeCoverages();
					objCoverage.UpdateCoveragesByRulePolicy(objWrapper,objInfo.CUSTOMER_ID,objInfo.POLICY_ID  ,
						objInfo.POLICY_VERSION_ID,RuleType.OtherAppDependent );
					objWrapper.ClearParameteres();
					
					if(ClsCommon.CheckWatercraft_Pol(objInfo.CUSTOMER_ID,objInfo.POLICY_ID ,objInfo.POLICY_VERSION_ID))
					{
						objCoverage.UpdateCoveragesByRulePolicy(objWrapper,objInfo.CUSTOMER_ID,
							objInfo.POLICY_ID ,objInfo.POLICY_VERSION_ID,RuleType.LobDependent);
						objWrapper.ClearParameteres();
					}
				}
			
                
				objCoverage.SaveDefaultCoveragesPolicy(objWrapper,objInfo.CUSTOMER_ID,
					objInfo.POLICY_ID,
					objInfo.POLICY_VERSION_ID,
					dwellingID);

//				//Update coverages/////////////////////////////////////////////////////
//				objWrapper.ClearParameteres();
//
//				objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@POL_ID",objInfo.POLICY_ID);
//				objWrapper.AddParameter("@POL_VERSION_ID",objInfo.POLICY_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",Convert.ToInt32(sqlParamDwellingID.Value));
//				
//				objWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FOR_Policy");
//				////////////////////////////////////////////////////////////////////////
//				
//				//Update Mandatory endorsements///////////////////////////////////
//				objWrapper.ClearParameteres();
//
//				objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@POL_ID",objInfo.POLICY_ID);
//				objWrapper.AddParameter("@POL_VERSION_ID",objInfo.POLICY_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",Convert.ToInt32(sqlParamDwellingID.Value));
//				
//				objWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_FOR_POLICY");
//				///////////////////////////////////////////////////////////////////
//				
//				//Adjust Coverages//////////////////////////////
//				objWrapper.ClearParameteres();
//
//				objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@POL_ID",objInfo.POLICY_ID);
//				objWrapper.AddParameter("@POL_VERSION_ID",objInfo.POLICY_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",Convert.ToInt32(sqlParamDwellingID.Value));
//				
//				objWrapper.ExecuteNonQuery("Proc_ADJUST_DWELLING_COVERAGES_FOR_POLICY");
//				//////////////////////
//				
//				//Update coverages from Gen Info////////////////
//				Cms.BusinessLayer.BlApplication.ClsHomeGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsHomeGeneralInformation();
//				objGen.UpdatePolicyCoverages(objInfo.CUSTOMER_ID,objInfo.POLICY_ID,objInfo.POLICY_VERSION_ID,dwellingID,objWrapper);
//				/////////////////////////////////////
//				
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
		
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
		  return dwellingID;
			

		}
		
		/// <summary>
		/// Updates adwelling details record 
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objNewInfo"></param>
		public int Update(ClsDwellingDetailsInfo objOldInfo,ClsDwellingDetailsInfo objNewInfo)
		{
			return Update(objOldInfo,objNewInfo,"","");
		}


		public int Update(ClsDwellingDetailsInfo objOldInfo,ClsDwellingDetailsInfo objNewInfo, string strCustomInfo,string strLOB)
		{
			string	strStoredProc =	"Proc_UpdateAppDwellingsInfo";
			string strTranXML = "";
			
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddDwellingDetails.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
		
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objNewInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objNewInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objNewInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objNewInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@REPLACEMENTCOST_COVA",objNewInfo.REPLACEMENTCOST_COVA);
			objWrapper.AddParameter("@BUILDING_TYPE",objNewInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objNewInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objNewInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objNewInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objNewInfo.NEIGHBOURS_VISIBLE);
			//objWrapper.AddParameter("@IS_VACENT_OCCUPY",objNewInfo.IS_VACENT_OCCUPY);
			//objWrapper.AddParameter("@IS_RENTED_IN_PART",objNewInfo.IS_RENTED_IN_PART);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objNewInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objNewInfo.NO_WEEKS_RENTED));
			//objWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objNewInfo.IS_DWELLING_OWNED_BY_OTHER);
			objWrapper.AddParameter("@MODIFIED_BY",objNewInfo.CREATED_BY);
			//objWrapper.AddParameter("@COMMENT_DWELLING_OWNED",objNewInfo.COMMENT_DWELLING_OWNED);
			objWrapper.AddParameter("@IS_ACTIVE",objNewInfo.IS_ACTIVE);


			//RPSINGH - start
			objWrapper.AddParameter("@DETACHED_OTHER_STRUCTURES",objNewInfo.DETACHED_OTHER_STRUCTURES);
			
			objWrapper.AddParameter("@MONTHS_RENTED",DefaultValues.GetDoubleNullFromNegative(objNewInfo.MONTHS_RENTED));
			//RPSINGH - end
		
			

			
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

			try
			{	
				if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
				{
					objWrapper.ExecuteNonQuery(strStoredProc);
				
				}
				else
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objNewInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objNewInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNewInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1697", "");// "Dwelling Info is modified";
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='DWELLING_NUMBER']");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='LOCATION_ID']");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strCustomInfo != "")
					{
						objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;
					}

					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);


				}
				
				//Update coverages/////////////////////////////////////////////////////
				objWrapper.ClearParameteres();
				ClsHomeCoverages objCoverage;

				if(strLOB.ToString().ToUpper().Trim().Equals("RENTAL"))
				{
					objCoverage = new ClsHomeCoverages("1");
				}
				else
				{
					objCoverage = new ClsHomeCoverages();
					objCoverage.UpdateCoveragesByRuleApp(objWrapper,objNewInfo.CUSTOMER_ID,objNewInfo.APP_ID  ,
						objNewInfo.APP_VERSION_ID,RuleType.OtherAppDependent);
					objWrapper.ClearParameteres();
				
					if(ClsCommon.CheckWatercraft(objNewInfo.CUSTOMER_ID,objNewInfo.APP_ID,objNewInfo.APP_VERSION_ID))
					{
						objCoverage.UpdateCoveragesByRuleApp (objWrapper,objNewInfo.CUSTOMER_ID,
							objNewInfo.APP_ID,
							objNewInfo.APP_VERSION_ID,RuleType.LobDependent);
						objWrapper.ClearParameteres();
					}

				}
				objCoverage.UpdateCoveragesByRuleApp(objWrapper,objNewInfo.CUSTOMER_ID,objNewInfo.APP_ID,
													 objNewInfo.APP_VERSION_ID,RuleType.RiskDependent,objNewInfo.DWELLING_ID);

//				objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
//				objWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
//				
//				objWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES");
//				////////////////////////////////////////////////////////////////////////
//				
//				//Update Mandatory endorsements///////////////////////////////////
//				objWrapper.ClearParameteres();
//
//				objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
//				objWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
//				
//				objWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS");
//				///////////////////////////////////////////////////////////////////
//				
//				//Adjust Coverages//////////////////////////////
//				objWrapper.ClearParameteres();
//				
//				if ( objNewInfo.MARKET_VALUE != objOldInfo.MARKET_VALUE )
//				{
//					objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
//					objWrapper.AddParameter("@APP_ID",objNewInfo.APP_ID);
//					objWrapper.AddParameter("@APP_VERSION_ID",objNewInfo.APP_VERSION_ID);
//					objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
//				
//					objWrapper.ExecuteNonQuery("Proc_ADJUST_DWELLING_COVERAGES");
//				}
//				//////////////////////
//				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			
			int retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal;
		
		}

		/// <summary>
		/// Updates adwelling details record 
		/// </summary>
		/// <param name="objOldInfo"></param>
		/// <param name="objNewInfo"></param>
		/// 

		public int Update(ClsPolicyDwellingDetailsInfo objOldInfo,ClsPolicyDwellingDetailsInfo objNewInfo,string strLOB,string strCustominfo)
		{
			return UpdatePol(objOldInfo,objNewInfo,strLOB,"");
		}
		public int UpdatePol(ClsPolicyDwellingDetailsInfo objOldInfo,ClsPolicyDwellingDetailsInfo objNewInfo,string strLOB,string strCustominfo)
		{
			string	strStoredProc =	"Proc_UpdatePolDwellingsInfo";
			string strTranXML = "";
			
			if ( this.TransactionLogRequired)
			{
				objNewInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/Homeowner/PolicyAddDwellingDetails.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				
				strTranXML = objBuilder.GetTransactionLogXML(objOldInfo,objNewInfo);
		
			}

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@POL_ID",objNewInfo.POLICY_ID );
			objWrapper.AddParameter("@POL_VERSION_ID",objNewInfo.POLICY_VERSION_ID );
			objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objNewInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objNewInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objNewInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objNewInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objNewInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objNewInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@REPLACEMENTCOST_COVA",objNewInfo.REPLACEMENTCOST_COVA);
			objWrapper.AddParameter("@BUILDING_TYPE",objNewInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objNewInfo.OCCUPANCY);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objNewInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objNewInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objNewInfo.NEIGHBOURS_VISIBLE);
			//objWrapper.AddParameter("@IS_VACENT_OCCUPY",objNewInfo.IS_VACENT_OCCUPY);
			//objWrapper.AddParameter("@IS_RENTED_IN_PART",objNewInfo.IS_RENTED_IN_PART);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objNewInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objNewInfo.NO_WEEKS_RENTED));
			//objWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objNewInfo.IS_DWELLING_OWNED_BY_OTHER);
			objWrapper.AddParameter("@MODIFIED_BY",objNewInfo.CREATED_BY);
			//objWrapper.AddParameter("@COMMENT_DWELLING_OWNED",objNewInfo.COMMENT_DWELLING_OWNED);
			objWrapper.AddParameter("@MONTHS_RENTED",DefaultValues.GetDoubleNullFromNegative(objNewInfo.MONTHS_RENTED));
			
	        		
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);

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
					objTransactionInfo.POLICY_ID  = objNewInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objNewInfo.POLICY_VERSION_ID ;
					objTransactionInfo.CLIENT_ID = objNewInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objNewInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1698", "");// "Dwelling details/coverage is modified";
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='DWELLING_NUMBER']");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='LOCATION_ID']");
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strCustominfo != "")
						objTransactionInfo.CUSTOM_INFO  =   strCustominfo;
                    objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);


				}

				objWrapper.ClearParameteres();
				//Added By Ravindra By 07-04-2006
				ClsHomeCoverages objCoverages;

				if(strLOB.ToUpper().Equals("RENTAL"))
				{
					objCoverages=new ClsHomeCoverages("1");
				}
				else
				{
					objCoverages=new ClsHomeCoverages();
					objCoverages.UpdateCoveragesByRulePolicy(objWrapper,objNewInfo.CUSTOMER_ID,objNewInfo.POLICY_ID ,objNewInfo.POLICY_VERSION_ID
						,RuleType.OtherAppDependent);
					objWrapper.ClearParameteres();
					if(ClsCommon.CheckWatercraft_Pol(objNewInfo.CUSTOMER_ID,objNewInfo.POLICY_ID ,objNewInfo.POLICY_VERSION_ID))
					{
						objCoverages.UpdateCoveragesByRulePolicy(objWrapper,objNewInfo.CUSTOMER_ID,
							objNewInfo.POLICY_ID ,objNewInfo.POLICY_VERSION_ID,RuleType.LobDependent);
						objWrapper.ClearParameteres();
					}

				}
			
			
                

				objCoverages.UpdateCoveragesByRulePolicy(objWrapper,objNewInfo.CUSTOMER_ID,
															objNewInfo.POLICY_ID,
															objNewInfo.POLICY_VERSION_ID ,
															RuleType.RiskDependent,
															objNewInfo.DWELLING_ID);

				
				objWrapper.ClearParameteres();

//				objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@POL_ID",objNewInfo.POLICY_ID);
//				objWrapper.AddParameter("@POL_VERSION_ID",objNewInfo.POLICY_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
//				
//				objWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES_FOR_Policy");
//				////////////////////////////////////////////////////////////////////////
//				
//				//Update Mandatory endorsements///////////////////////////////////
//				objWrapper.ClearParameteres();
//
//				objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
//				objWrapper.AddParameter("@POL_ID",objNewInfo.POLICY_ID);
//				objWrapper.AddParameter("@POL_VERSION_ID",objNewInfo.POLICY_VERSION_ID);
//				objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
//				
//				objWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS_FOR_POLICY");
//				///////////////////////////////////////////////////////////////////
//				
//				//Adjust Coverages//////////////////////////////
//				objWrapper.ClearParameteres();
//				
//				if ( objNewInfo.MARKET_VALUE != objOldInfo.MARKET_VALUE )
//				{
//					objWrapper.AddParameter("@CUSTOMER_ID",objNewInfo.CUSTOMER_ID);
//					objWrapper.AddParameter("@POL_ID",objNewInfo.POLICY_ID);
//					objWrapper.AddParameter("@POL_VERSION_ID",objNewInfo.POLICY_VERSION_ID);
//					objWrapper.AddParameter("@DWELLING_ID",objNewInfo.DWELLING_ID);
//				
//					objWrapper.ExecuteNonQuery("Proc_ADJUST_DWELLING_COVERAGES_FOR_POLICY");
//				}
//				//////////////////////
//				
//				//Update coverages from Gen Info////////////////
//				Cms.BusinessLayer.BlApplication.ClsHomeGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsHomeGeneralInformation();
//				objGen.UpdatePolicyCoverages(objNewInfo.CUSTOMER_ID,objNewInfo.POLICY_ID,objNewInfo.POLICY_VERSION_ID,objNewInfo.DWELLING_ID,objWrapper);
				/////////////////////////////////////
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
				//return -1;
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			
			int retVal = Convert.ToInt32(sqlParamRetVal.Value);
			
			return retVal;
		
		}
		

		/// <summary>
		/// Gets a single Dwelling info record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>	
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public DataSet GetDwellingInfoByID(int customerID, int appID, int appVersionID, int dwellingID)
		{
			string	strStoredProc =	"Proc_GetDwellingInfoByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}
		
		/// <summary>
		/// Gets a single Dwelling info record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public DataSet GetPolicyDwellingInfoByID(int customerID, int polID, int polVersionID, int dwellingID)
		{
			string	strStoredProc =	"Proc_GetPolicyDwellingInfoByID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}

		#region Copy Dwelling Details For Policy
		public int  CopyPolicyDwellingDetails(int customerID, int polID, int polVersionID, int dwellingID,
			int locationID, int subLocationID,int intFromUserID
			)
		{
			string	strStoredProc =	"Proc_CopyPolDwellingsInfo";
			int newDwellingID=0;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			try
			{
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			objWrapper.AddParameter("@LOCATION_ID",locationID);
			objWrapper.AddParameter("@SUB_LOC_ID",subLocationID);
			//objWrapper.AddParameter("@NEWDWELLINGID",SqlDbType.Int,ParameterDirection.Output);
			SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@NEWDWELLINGID",SqlDbType.Int,ParameterDirection.Output);
			objWrapper.ExecuteNonQuery(strStoredProc);
			if(TransactionLogRequired)
			{
				ClsDwellingDetailsInfo objDwellingDetailsInfo=new ClsDwellingDetailsInfo();
				objDwellingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Homeowner/PolicyLocationPopup.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID			=	1;
				objTransactionInfo.POLICY_ID				=	polID;
				objTransactionInfo.POLICY_VER_TRACKING_ID	=	polVersionID;
				objTransactionInfo.CLIENT_ID				=	customerID;
				objTransactionInfo.RECORDED_BY				=	intFromUserID;
                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1699", "");// "Dwelling Info Has Been Copied";
				objTransactionInfo.CUSTOM_INFO				=   " ;Dwelling # = " + objSqlParameter.Value.ToString() + ";Location # = " + subLocationID.ToString();
				
				objWrapper.ExecuteNonQuery(objTransactionInfo);
			}				
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			newDwellingID = Convert.ToInt32(objSqlParameter.Value);

			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			/*objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);			
			int newDwellingID = Convert.ToInt32(objWrapper.CommandParameters[6].Value);*/
			
			return newDwellingID;

		}
		



		#endregion
		/// <summary>
		/// Copies a dwelling info record
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public int  CopyDwellingDetails(int customerID, int appID, int appVersionID, int dwellingID,
										int locationID, int subLocationID,int intFromUserID
										)
		{
			string	strStoredProc =	"Proc_CopyAppDwellingsInfo";			
			int newDwellingID=0;
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			try
			{
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@APP_ID",appID);
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				objWrapper.AddParameter("@DWELLING_ID",dwellingID);
				objWrapper.AddParameter("@LOCATION_ID",locationID);
				objWrapper.AddParameter("@SUB_LOC_ID",subLocationID);
				//objWrapper.AddParameter("@NEWDWELLINGID",SqlDbType.Int,ParameterDirection.Output);

				SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@NEWDWELLINGID",SqlDbType.Int,ParameterDirection.Output);
				objWrapper.ExecuteNonQuery(strStoredProc);				

				if(TransactionLogRequired)
				{
					ClsDwellingDetailsInfo objDwellingDetailsInfo=new ClsDwellingDetailsInfo();
					objDwellingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/Aspx/HomeOwners/LocationPopup.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	1;
					objTransactionInfo.APP_ID				    =	appID;
					objTransactionInfo.APP_VERSION_ID	        =	appVersionID;
					objTransactionInfo.CLIENT_ID				=	customerID;
					objTransactionInfo.RECORDED_BY				=	intFromUserID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1699", "");//"Dwelling Info Has Been Copied";
					objTransactionInfo.CUSTOM_INFO				=   " ;Dwelling # = " + objSqlParameter.Value.ToString() + ";Location # = " + subLocationID.ToString();
					
					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}				
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				newDwellingID = Convert.ToInt32(objSqlParameter.Value);

			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			/*Commented Itrack # 6254 -27th Aug 2009 -MANOJ 
			 * objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);			
			int newDwellingID = Convert.ToInt32(objWrapper.CommandParameters[6].Value);*/
			
			
			return newDwellingID;

		}
		
		/// <summary>
		/// Deletes a dwelling details record from the  database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public int Delete(int customerID, int appID, int appVersionID, int dwellingID)
		{
			string	strStoredProc =	"Proc_DeleteDwellingsInfo";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			
			try
			{ 
				objWrapper.ExecuteNonQuery(strStoredProc);
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
		/// Deletes a dwelling details record from the  database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public int DeletePolicy(int customerID, int polID, int polVersionID, int dwellingID)
		{
			string	strStoredProc =	"Proc_DeletePolicyDwellingsInfo";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
			SqlParameter sqlParamRetVal = (SqlParameter) objWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			
			
			try
			{ 
				objWrapper.ExecuteNonQuery(strStoredProc);
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
		/// 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <returns></returns>
		public static int GetNextDwellingNumberForPolicy(int customerID, int polID, int polVersionID)
		{
			string	strStoredProc =	"Proc_GetNextDwelling_NumberForPolicy";
			int nextDwellingID = 0;

			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@POL_ID",polID);
			sqlParams[2] = new SqlParameter("@POL_VERSION_ID",polVersionID);
			
			try
			{
				nextDwellingID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return nextDwellingID;
			
		}
		

		/// <summary>
		/// Returns the next Dwelling number 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public static int GetNextDwellingNumber(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GetNextDwelling_Number";
			int nextDwellingID = 0;

			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@APP_ID",appID);
			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			
			try
			{
				nextDwellingID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return nextDwellingID;
			
		}
		

			public int GetReplacementCost(string strCUSTOMERID,string strAppId, string strAppVersionId)			
			{
				string		strStoredProc	=	"Proc_GetReplacementCost";
				DateTime	RecordDate		=	DateTime.Now;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
				try
				{
					objDataWrapper.AddParameter("@CUSTOMER_ID",strCUSTOMERID);
					objDataWrapper.AddParameter("@APP_ID",strAppId);
					objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVersionId);				
					SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@retVal",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				
					objDataWrapper.ExecuteNonQuery(strStoredProc);
				
					int retVal = int.Parse(objSqlParameter.Value.ToString());
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return retVal;
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



		public int GetPercentagePoints(string strCUSTOMERID,string strAppId, string strAppVersionId)			
		{
			string		strStoredProc	=	"Proc_GetPercentagePoints";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",strCUSTOMERID);
				objDataWrapper.AddParameter("@APP_ID",strAppId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",strAppVersionId);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@retVal",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				
				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				int retVal = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retVal;
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
		/// Returns the next Dwelling number 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="dwellingID"></param>
		/// <returns></returns>
		public static int GetNextPolicyDwellingNumber(int customerID, int polID, int polVersionID)
		{
			string	strStoredProc =	"Proc_GetNextPolicyDwelling_Number";
			int nextDwellingID = 0;

			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@POL_ID",polID);
			sqlParams[2] = new SqlParameter("@POL_VERSION_ID",polVersionID);
			
			try
			{
				nextDwellingID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams));
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return nextDwellingID;
			
		}


		public int GetPrevDwellingInfoByID(int customerID, int appID, int appVersionID, int dwellingID)
		{
			string	strStoredProc =	"Proc_GetPrevDwellingInfoByID";
			int prevDwellingID = 0;

			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@APP_ID",appID);
			sqlParams[2] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			sqlParams[3] = new SqlParameter("@DWELLING_ID",dwellingID);
			
			try
			{
				prevDwellingID = SqlHelper.ExecuteNonQuery(ConnStr,CommandType.StoredProcedure,strStoredProc,sqlParams);
			}
			catch(Exception ex)
			{
				
				throw(ex);
			}
			
			//objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			
			//int newDwellingID = Convert.ToInt32(objWrapper.CommandParameters[4].Value);
			
			return 1;

		}

        /// <summary>
        /// Gets a dwelling Id For Policy
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="policyID"></param>
        /// <param name="policyVersionID"></param>
        /// <returns></returns>
 
		public static string  GetPolicyDwellingID(int customerID, int policyID, int policyVersionID)
		{
			string	strStoredProc =	"Proc_GET_POLICYDWELLINGSID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDwellingId="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDwellingId=ds.Tables[0].Rows[i][0].ToString()+ '^' + ds.Tables[0].Rows[i][1].ToString();
				}
				else
				{
					strDwellingId = strDwellingId + '~'  + ds.Tables[0].Rows[i][0].ToString()+ '^' + ds.Tables[0].Rows[i][1].ToString();
				}

				
			}
			return strDwellingId;
		}

		/// <summary>
		/// Gets a  Dwelling Ids
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns>Dwelling Ids and the address as a carat separated string </returns>
		public static string  GetDwellingID(int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_GET_DWELLINGSID";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			int intCount=ds.Tables[0].Rows.Count;
			string strDwellingId="-1";
			for(int i=0;i<intCount;i++)
			{
				if(i==0)
				{
					strDwellingId=ds.Tables[0].Rows[i][0].ToString()+ '^' + ds.Tables[0].Rows[i][1].ToString() + '^' + ds.Tables[0].Rows[i][2].ToString();;
				}
				else
				{
					strDwellingId = strDwellingId + '~'  + ds.Tables[0].Rows[i][0].ToString()+ '^' + ds.Tables[0].Rows[i][1].ToString()+ '^' + ds.Tables[0].Rows[i][2].ToString();
				}

				
			}
			return strDwellingId;
		}

		#region Policy ActivateDeactivateDwellings 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objDwellingDetailsInfo"></param>
		/// <param name="IS_ACTIVE"></param>
		/// <returns></returns>
		public int ActivateDeactivateDwellings(ClsPolicyDwellingDetailsInfo  objDwellingDetailsInfo, string IS_ACTIVE)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivatePOL_DWELLINGS_INFO";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objDwellingDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objDwellingDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingDetailsInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",objDwellingDetailsInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",IS_ACTIVE);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					
					objDwellingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/Homeowner/PolicyAddDwellingDetails.aspx.resx");					
					
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID  = objDwellingDetailsInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objDwellingDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objDwellingDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDwellingDetailsInfo.MODIFIED_BY;
					if(IS_ACTIVE.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1700","");//"Dwelling Info Has Been Activated";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1701", "");//"Dwelling Info Has Been Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	";Dwelling # = " + objDwellingDetailsInfo.DWELLING_NUMBER + ";Location # = " + objDwellingDetailsInfo.LOC_NUM;
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


		#endregion


		//end
		
		public int ActivateDeactivateDwellings(ClsDwellingDetailsInfo objDwellingDetailsInfo, string IS_ACTIVE)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAPP_DWELLINGS_INFO";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingDetailsInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@LOCATION_ID",objDwellingDetailsInfo.LOCATION_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",IS_ACTIVE);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					
					objDwellingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/HomeOwners/AddDwellingDetails.aspx.resx");
					
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objDwellingDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objDwellingDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objDwellingDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDwellingDetailsInfo.MODIFIED_BY;
					if(IS_ACTIVE.ToUpper()=="Y")
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1700", "");//"Dwelling Info Has Been Activated";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1701", "");//"Dwelling Info Has Been Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	";Dwelling # = " + objDwellingDetailsInfo.DWELLING_NUMBER + ";Location # = " + objDwellingDetailsInfo.LOC_NUM;
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


		public void ActivateDeactivateDwelling(ClsDwellingDetailsInfo objDwellingDetailsInfo, string IS_ACTIVE)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateAPP_DWELLINGS_INFO";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingDetailsInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",IS_ACTIVE);				

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					
					objDwellingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/HomeOwners/AddDwellingDetails.aspx.resx");
					
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objDwellingDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objDwellingDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objDwellingDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDwellingDetailsInfo.MODIFIED_BY;
					if(IS_ACTIVE.ToUpper()=="Y")
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1700", "");//"Dwelling Info Has Been Activated";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1701", "");// "Dwelling Info Has Been Deactivated";
					objTransactionInfo.CUSTOM_INFO		=	";Dwelling ID = " + objDwellingDetailsInfo.DWELLING_ID + ";Location ID = " + objDwellingDetailsInfo.LOCATION_ID;
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
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			
		}
         
		//start

		public int DeleteDwellingForPolicy(ClsPolicyDwellingDetailsInfo  objDwellingDetailsInfo)
		{
			string		strStoredProc	=	"Proc_DeleteDwellingsInfoForPolicy";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POL_ID",objDwellingDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POL_VERSION_ID",objDwellingDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingDetailsInfo.DWELLING_ID);				
				SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					
					//ClsCommon.MapTransactionLabel("Policies/aspx/Homeowner/PolicyAddDwellingDetails.aspx.resx");
					
					objDwellingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/aspx/Homeowner/PolicyAddDwellingDetails.aspx.resx");					
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.POLICY_ID  = objDwellingDetailsInfo.POLICY_ID ;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objDwellingDetailsInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objDwellingDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDwellingDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1702", "");// "Dwelling Info Has Been Deleted";
					objTransactionInfo.CUSTOM_INFO		=	";Dwelling # = " + objDwellingDetailsInfo.DWELLING_NUMBER + ";Location # = " + objDwellingDetailsInfo.LOC_NUM;					
					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return int.Parse(sqlParamRetVal.Value.ToString());
			}
			catch(Exception ex)
			{
				throw(ex);
				return -1;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			
		}



		//end

		public int DeleteDwelling(ClsDwellingDetailsInfo objDwellingDetailsInfo)
		{
			string		strStoredProc	=	"Proc_DeleteDwellingsInfo";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDwellingDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objDwellingDetailsInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objDwellingDetailsInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objDwellingDetailsInfo.DWELLING_ID);				
				SqlParameter sqlParamRetVal = (SqlParameter) objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					
					objDwellingDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/HomeOwners/AddDwellingDetails.aspx.resx");					
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objDwellingDetailsInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objDwellingDetailsInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objDwellingDetailsInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objDwellingDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1702", "");// "Dwelling Info Has Been Deleted";
					objTransactionInfo.CUSTOM_INFO		=	";Dwelling # = " + objDwellingDetailsInfo.DWELLING_NUMBER + ";Location # = " + objDwellingDetailsInfo.LOC_NUM;					
					
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();

				//Update watercraft coverages/////////
				ClsDwellingCoverageLimit objLimit = new ClsDwellingCoverageLimit();
				objLimit.UpdateWatercraftCoverages(objDwellingDetailsInfo.CUSTOMER_ID,objDwellingDetailsInfo.APP_ID,objDwellingDetailsInfo.APP_VERSION_ID,objDataWrapper);
				/////////////
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return int.Parse(sqlParamRetVal.Value.ToString());
			}
			catch(Exception ex)
			{
				throw(ex);
				return -1;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			
		}



		/// <summary>
		/// Inserts a record into the database from ACORD document
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int Save(ClsDwellingDetailsInfo objInfo,DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_Save_APP_DWELLINGS_INFO_ACORD";

			//string strTranXML = "";
			
			//DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_NUMBER",objInfo.DWELLING_NUMBER);
			objWrapper.AddParameter("@LOCATION_ID",DefaultValues.GetIntNullFromNegative(objInfo.LOCATION_ID));
			objWrapper.AddParameter("@SUB_LOC_ID",DefaultValues.GetIntNullFromNegative(objInfo.SUB_LOC_ID));
			objWrapper.AddParameter("@YEAR_BUILT",DefaultValues.GetIntNullFromNegative(objInfo.YEAR_BUILT));
			objWrapper.AddParameter("@PURCHASE_YEAR",DefaultValues.GetIntNullFromNegative(objInfo.PURCHASE_YEAR));
			objWrapper.AddParameter("@PURCHASE_PRICE",DefaultValues.GetDoubleNullFromNegative(objInfo.PURCHASE_PRICE));
			objWrapper.AddParameter("@MARKET_VALUE",DefaultValues.GetDoubleNullFromNegative(objInfo.MARKET_VALUE));
			objWrapper.AddParameter("@REPLACEMENT_COST",DefaultValues.GetDoubleNullFromNegative(objInfo.REPLACEMENT_COST));
			objWrapper.AddParameter("@BUILDING_TYPE",objInfo.BUILDING_TYPE);
			objWrapper.AddParameter("@OCCUPANCY",objInfo.OCCUPANCY);
			objWrapper.AddParameter("@OCCUPANCY_CODE",objInfo.OCCUPANCY_CODE);
			objWrapper.AddParameter("@NEED_OF_UNITS",DefaultValues.GetIntNullFromNegative(objInfo.NEED_OF_UNITS));
			objWrapper.AddParameter("@USAGE",objInfo.USAGE);
			objWrapper.AddParameter("@NEIGHBOURS_VISIBLE",objInfo.NEIGHBOURS_VISIBLE);
			//objWrapper.AddParameter("@IS_VACENT_OCCUPY",objInfo.IS_VACENT_OCCUPY);
			//objWrapper.AddParameter("@IS_RENTED_IN_PART",objInfo.IS_RENTED_IN_PART);
			objWrapper.AddParameter("@OCCUPIED_DAILY",objInfo.OCCUPIED_DAILY);
			objWrapper.AddParameter("@NO_WEEKS_RENTED",DefaultValues.GetIntNullFromNegative(objInfo.NO_WEEKS_RENTED));
			//objWrapper.AddParameter("@IS_DWELLING_OWNED_BY_OTHER",objInfo.IS_DWELLING_OWNED_BY_OTHER);
			objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
			objWrapper.AddParameter("@MODIFIED_BY",objInfo.MODIFIED_BY);
			
			SqlParameter sqlParamDwellingID = (SqlParameter) objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Output);
			
			objWrapper.ExecuteNonQuery(strStoredProc);
			
			//Get the tran log XML , if present
			if ( this.TransactionLogRequired)
			{
				objWrapper.ClearParameteres();

				objInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Homeowners/AddDwellingDetails.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objInfo);

				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.APP_ID = objInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objInfo.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objInfo.MODIFIED_BY;
                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1703", "");// "New Dwelling Info is added from Quick Quote.";
				objTransactionInfo.CHANGE_XML		=	strTranXML;

				objWrapper.ExecuteNonQuery(objTransactionInfo);

				objWrapper.ClearParameteres();
			}

			int dwellingID = Convert.ToInt32(sqlParamDwellingID.Value);
			
			objInfo.DWELLING_ID = dwellingID;
			//Change on 12 jan 2005
			//Update coverages and Endorsements//////////
			//UpdateCoveragesAndEndorsements(dwellingID,objWrapper,objInfo);
			//////////////////////////////////////////////

			return dwellingID;
			

		}

		/// <summary>
		/// Upadtes Coverages and Endorsements when dwelling is saved
		/// </summary>
		/// <param name="dwellingID"></param>
		/// <param name="objWrapper"></param>
		private void UpdateCoveragesAndEndorsements(int dwellingID, DataWrapper objWrapper,ClsDwellingDetailsInfo objInfo)
		{
			//Update coverages/////////////////////////////////////////////////////
			objWrapper.ClearParameteres();

			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
				
			objWrapper.ExecuteNonQuery("Proc_Update_DWELLING_COVERAGES");
			////////////////////////////////////////////////////////////////////////
				
			//Update Mandatory endorsements///////////////////////////////////
			objWrapper.ClearParameteres();

			objWrapper.AddParameter("@CUSTOMER_ID",objInfo.CUSTOMER_ID);
			objWrapper.AddParameter("@APP_ID",objInfo.APP_ID);
			objWrapper.AddParameter("@APP_VERSION_ID",objInfo.APP_VERSION_ID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingID);
				
			objWrapper.ExecuteNonQuery("Proc_UPDATE_HOME_ENDORSEMENTS");
			///////////////////////////////////////////////////////////////////
		}


	}
}
