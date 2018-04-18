/******************************************************************************************
<Author				: -   
<Start Date			: -	
<End Date			: -	
<Description		: - 	
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 10-11-2005
<Modified By		: - Vijay Arora
<Purpose			: - Added the Policy Function Region

<Modified Date		: - 23-11-2005
<Modified By		: - Vijay Arora
<Purpose			: - Added the WaterCraft Additional Interest Functions in Policy Region
<Modified Date			: - 16/12/2005
<Modified By			: - Sumit Chhabra
<Purpose				: - Check has been added at update of data to prevent an entry from going into transaction log when no modication has taken place.
*******************************************************************************************/ 


using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsAdditionalInterest.
	/// </summary>
	public class ClsAdditionalInterest : Cms.BusinessLayer.BlApplication.clsapplication
	{
		private bool boolTransactionRequired			= true;		
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

		
		#region VIRTUAL FUNCTIONS

		public virtual string RiskIdColumnName()
		{
			return "";
		}

		public virtual string InsertSpName()	
		{
			return "";
		}

		public virtual string UpdateSpName()	
		{
			return "";
		}

		public virtual string ActivateDeactivateSpName()
		{
			return "";
		}

		public virtual string DeleteSpName()	
		{
			return "";
		}

		public virtual string GetSpName()		
		{
			return "";
		}

		public virtual string CalledFromString()
		{
			return "";
		}

		public virtual string BillMortagagee()			
		{
			return "";
		}
		public virtual int GetAppBillMortagagee(int CustomerId, int AppId, int AppVersionId,int DwellingId,int AddIntId)
		{
			return -1;
		}
		public virtual int GetPolBillMortagagee(int CustomerId, int PolId, int PolVersionId,int DwellingId,int AddIntId)
		{
			return -1;
		}

		public ClsAdditionalInterest()			
		{
			
		}


		#endregion

		#region ADD UPDATE ADDITIONAL INTEREST (APP/POL)
	
		public int Add(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo, string hidCustomInfo)
		{
			int retValue;
			try
			{
				//Making the Database connection for common transaction
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				
				//Inserting Additional Interest Details
				DateTime	RecordDate		=	DateTime.Now;
			
							
				objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objAdditionalInterestInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@APP_ID",objAdditionalInterestInfo.APP_ID);
				objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
				objDataWrapper.AddParameter("@MEMO",objAdditionalInterestInfo.MEMO);
				objDataWrapper.AddParameter("@NATURE_OF_INTEREST",objAdditionalInterestInfo.NATURE_OF_INTEREST);
				objDataWrapper.AddParameter("@RANK",objAdditionalInterestInfo.RANK );
				objDataWrapper.AddParameter("@LOAN_REF_NUMBER",objAdditionalInterestInfo.LOAN_REF_NUMBER);
				objDataWrapper.AddParameter("@CREATED_BY",objAdditionalInterestInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objAdditionalInterestInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@HOLDER_NAME",objAdditionalInterestInfo.HOLDER_NAME);
				objDataWrapper.AddParameter("@HOLDER_ADD1",objAdditionalInterestInfo.HOLDER_ADD1);
				objDataWrapper.AddParameter("@HOLDER_ADD2",objAdditionalInterestInfo.HOLDER_ADD2);
				objDataWrapper.AddParameter("@HOLDER_CITY",objAdditionalInterestInfo.HOLDER_CITY);
				objDataWrapper.AddParameter("@HOLDER_COUNTRY",objAdditionalInterestInfo.HOLDER_COUNTRY);
				objDataWrapper.AddParameter("@HOLDER_STATE",objAdditionalInterestInfo.HOLDER_STATE);
				objDataWrapper.AddParameter("@HOLDER_ZIP",objAdditionalInterestInfo.HOLDER_ZIP);
				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
		
				SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(RiskIdColumnName()!="")
					objDataWrapper.AddParameter(RiskIdColumnName(),objAdditionalInterestInfo.RISK_ID );
				if(BillMortagagee()!="")
					objDataWrapper.AddParameter(BillMortagagee(),objAdditionalInterestInfo.BILL_MORTAGAGEE);
				
				string strStoredProc = InsertSpName();
			          
				
				int returnResult = 0;
				
				try
				{
					//if transaction required
					if(this.boolTransactionRequired) 
					{
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						
						objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"application\aspx\AdditionalInterest.aspx.resx");

						string strTranXML = objBuilder.GetTransactionLogXML(objAdditionalInterestInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

						
						objTransactionInfo.APP_ID = objAdditionalInterestInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objAdditionalInterestInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objAdditionalInterestInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objAdditionalInterestInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1629", "");// "Additional Interest is added";	
						//strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='HOLDER_ID']");
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						//objTransactionInfo.CUSTOM_INFO		=	";Selected Holder/ Interest = " + objAdditionalInterestInfo.HOLDER_NAME + hidCustomInfo;
						objTransactionInfo.CUSTOM_INFO		=	hidCustomInfo;

						objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						returnResult	=	1;
					}
					else//if no transaction required
					{
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					}
								
					retValue = Convert.ToInt32(retParam.Value);
					ClsGeneralInformation obj=new ClsGeneralInformation();
					string strlob="";
					string strCalledFrom="";
					strCalledFrom=RiskIdColumnName();
					strlob=obj.Fun_GetLObID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.APP_ID,objAdditionalInterestInfo.APP_VERSION_ID);
					if(strlob == ((int)enumLOB.HOME).ToString() && strCalledFrom=="@DWELLING_ID")
					{
						
						UpdateAppHomeCoverages(objDataWrapper,objAdditionalInterestInfo);
					}

				}
				catch(Exception ex)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
					throw(ex);
				}

				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				return retValue;
			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
			
		}
		public void UpdatePolicyHomeCoverages(DataWrapper objDataWrapper,Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo )
		{
			ClsHomeCoverages objCoverages= new ClsHomeCoverages();
			objCoverages.UpdateCoveragesByRulePolicy(objDataWrapper,objAdditionalInterestInfo.CUSTOMER_ID,
				objAdditionalInterestInfo.POLICY_ID, 
				objAdditionalInterestInfo.POLICY_VERSION_ID, 
				RuleType.RiskDependent,
				objAdditionalInterestInfo.RISK_ID);
		}
/// <summary>
/// 
/// </summary>
/// <param name="objDataWrapper"></param>
/// <param name="objAdditionalInterestInfo"></param>
		public void UpdateAppHomeCoverages(DataWrapper objDataWrapper,Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo )
	    {
				ClsHomeCoverages objCoverages= new ClsHomeCoverages();
				objCoverages.UpdateCoveragesByRuleApp(objDataWrapper,objAdditionalInterestInfo.CUSTOMER_ID,
				objAdditionalInterestInfo.APP_ID,
				objAdditionalInterestInfo.APP_VERSION_ID,
				RuleType.RiskDependent,
				objAdditionalInterestInfo.RISK_ID);
		}
		public int AddAcord(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertAdditionalInterest_ACORD";
			
			DateTime	RecordDate		=	DateTime.Now;
			
			//Set transaction label in the new object, if required
			if ( this.boolTransactionRequired)
			{
				objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"application\aspx\PolicyAdditionalInterest.aspx.resx");
			}

			
			objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objAdditionalInterestInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@APP_ID",objAdditionalInterestInfo.APP_ID);
			objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
			objDataWrapper.AddParameter("@MEMO",objAdditionalInterestInfo.MEMO);
			objDataWrapper.AddParameter("@NATURE_OF_INTEREST",objAdditionalInterestInfo.NATURE_OF_INTEREST);
			objDataWrapper.AddParameter("@RANK",objAdditionalInterestInfo.RANK );
			objDataWrapper.AddParameter("@LOAN_REF_NUMBER",objAdditionalInterestInfo.LOAN_REF_NUMBER);
			objDataWrapper.AddParameter("@CREATED_BY",objAdditionalInterestInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now);
			
			objDataWrapper.AddParameter("@HOLDER_NAME",objAdditionalInterestInfo.HOLDER_NAME);
			objDataWrapper.AddParameter("@HOLDER_ADD1",objAdditionalInterestInfo.HOLDER_ADD1);
			objDataWrapper.AddParameter("@HOLDER_ADD2",objAdditionalInterestInfo.HOLDER_ADD2);
			objDataWrapper.AddParameter("@HOLDER_CITY",objAdditionalInterestInfo.HOLDER_CITY);
			objDataWrapper.AddParameter("@HOLDER_COUNTRY",objAdditionalInterestInfo.HOLDER_COUNTRY);
			objDataWrapper.AddParameter("@HOLDER_STATE",objAdditionalInterestInfo.HOLDER_STATE);
			objDataWrapper.AddParameter("@HOLDER_ZIP",objAdditionalInterestInfo.HOLDER_ZIP);
		
			SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);

			if (objAdditionalInterestInfo.VEHICLE_ID != 0)
			{
				//Vehicle id is passed hence adding the vehicle id parameter
				objDataWrapper.AddParameter("@VEHICLE_ID",objAdditionalInterestInfo.VEHICLE_ID );

				//Passing the sp name for saving the additional interest for vehicle
				strStoredProc = "Proc_InsertAdditionalInterest_ACORD";
			}
			else if(objAdditionalInterestInfo.BOAT_ID != 0 )
			{
				//Vehicle id is passed hence adding the BOAT id parameter
				objDataWrapper.AddParameter("@BOAT_ID",objAdditionalInterestInfo.BOAT_ID);

				//Passing the sp name for saving the additional interest for BOAT
				strStoredProc = "Proc_InsertWatercraftAdditionalInterest";
			}
			else if(objAdditionalInterestInfo.TRAILER_ID != 0 )
			{
				//Vehicle id is passed hence adding the BOAT id parameter
				objDataWrapper.AddParameter("@TRAILER_ID",objAdditionalInterestInfo.TRAILER_ID);

				//Passing the sp name for saving the additional interest for BOAT
				strStoredProc = "Proc_InsertTrailerAdditionalInterest";
			}
			else
			{
				//Vehicle id is not passed hence adding the dwelling id parameter
				objDataWrapper.AddParameter("@DWELLING_ID",objAdditionalInterestInfo.DWELLING_ID);
				objDataWrapper.AddParameter("@CERTIFICATE_REQUIRED",objAdditionalInterestInfo.CERTIFICATE_REQUIRED);

				//Passing the sp name for saving the home owner additional interest 
				strStoredProc = "Proc_InsertHomeOwnerAdditionalInterest";
			}
				
			int returnResult = 0;
				
			int holderID = 0;

			//if transaction required
			if(this.boolTransactionRequired) 
			{
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objAdditionalInterestInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				
				objTransactionInfo.APP_ID = objAdditionalInterestInfo.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objAdditionalInterestInfo.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objAdditionalInterestInfo.CUSTOMER_ID;
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objAdditionalInterestInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1630", "");// "Additional Interest Has Been Added";	
				objTransactionInfo.CHANGE_XML		=	strTranXML;


				objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				holderID = Convert.ToInt32(retParam.Value);

				objDataWrapper.ExecuteNonQuery(objTransactionInfo);

				returnResult	=	1;
			}
			else//if no transaction required
			{
				returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			}
							
			
			return holderID;
			
		}

		public int AddPolicyAddtionalInterest(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo)
		{
			
			DateTime	RecordDate		=	DateTime.Now;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			if ( this.boolTransactionRequired)
			{
				objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"policies\aspx\Automobile\PolicyAdditionalInterest.aspx.resx");
			}

			objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",objAdditionalInterestInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",objAdditionalInterestInfo.POLICY_VERSION_ID);
			objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
			objDataWrapper.AddParameter("@MEMO",objAdditionalInterestInfo.MEMO);
			objDataWrapper.AddParameter("@NATURE_OF_INTEREST",objAdditionalInterestInfo.NATURE_OF_INTEREST);
			objDataWrapper.AddParameter("@RANK",objAdditionalInterestInfo.RANK );
			objDataWrapper.AddParameter("@LOAN_REF_NUMBER",objAdditionalInterestInfo.LOAN_REF_NUMBER);
			objDataWrapper.AddParameter("@CREATED_BY",objAdditionalInterestInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objAdditionalInterestInfo.CREATED_DATETIME);
			
			objDataWrapper.AddParameter("@HOLDER_NAME",objAdditionalInterestInfo.HOLDER_NAME);
			objDataWrapper.AddParameter("@HOLDER_ADD1",objAdditionalInterestInfo.HOLDER_ADD1);
			objDataWrapper.AddParameter("@HOLDER_ADD2",objAdditionalInterestInfo.HOLDER_ADD2);
			objDataWrapper.AddParameter("@HOLDER_CITY",objAdditionalInterestInfo.HOLDER_CITY);
			objDataWrapper.AddParameter("@HOLDER_COUNTRY",objAdditionalInterestInfo.HOLDER_COUNTRY);
			objDataWrapper.AddParameter("@HOLDER_STATE",objAdditionalInterestInfo.HOLDER_STATE);
			objDataWrapper.AddParameter("@HOLDER_ZIP",objAdditionalInterestInfo.HOLDER_ZIP);
			objDataWrapper.AddParameter("@IS_ACTIVE","Y");
			if(BillMortagagee()!="")
				objDataWrapper.AddParameter(BillMortagagee(),objAdditionalInterestInfo.BILL_MORTAGAGEE);
		
			SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);
			if(RiskIdColumnName()!="")
				objDataWrapper.AddParameter(RiskIdColumnName(),objAdditionalInterestInfo.RISK_ID );
			//Passing the sp name for saving the additional interest for vehicle
			string strStoredProc = InsertSpName();
			
			
			int returnResult = 0;
				
			try
			{
				//if transaction required
				if(this.boolTransactionRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objAdditionalInterestInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.POLICY_ID		= objAdditionalInterestInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objAdditionalInterestInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		= objAdditionalInterestInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAdditionalInterestInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1518","");//"Policy Additional Interest Has Been Added";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;


					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult	=	1;
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				}
				
				int retVal = Convert.ToInt32(retParam.Value);
			
				objDataWrapper.ClearParameteres();
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string strlob="";
				string strCalledFrom="";
				strCalledFrom=RiskIdColumnName(); 
                //DataSet dsTemp = new DataSet();
                //dsTemp = obj.GetPolicyLOBID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.POLICY_ID,objAdditionalInterestInfo.POLICY_VERSION_ID);
                //strlob = dsTemp.Tables[0].Rows[0][0].ToString();
                //if(strlob == ((int)enumLOB.HOME).ToString() && strCalledFrom=="@DWELLING_ID")
                //{
                //    UpdatePolicyHomeCoverages(objDataWrapper,objAdditionalInterestInfo);
                //}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retVal;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
		}

		
		public int UpdateAcord(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_UpdateAdditionalInterest";
			
			DateTime	RecordDate		=	DateTime.Now;
			
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objAdditionalInterestInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@APP_ID",objAdditionalInterestInfo.APP_ID);
			objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
			objDataWrapper.AddParameter("@MEMO",objAdditionalInterestInfo.MEMO);
			objDataWrapper.AddParameter("@NATURE_OF_INTEREST",objAdditionalInterestInfo.NATURE_OF_INTEREST);
			objDataWrapper.AddParameter("@RANK",objAdditionalInterestInfo.RANK );
			objDataWrapper.AddParameter("@LOAN_REF_NUMBER",objAdditionalInterestInfo.LOAN_REF_NUMBER);
			objDataWrapper.AddParameter("@MODIFIED_BY",objAdditionalInterestInfo.CREATED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",RecordDate);
			
			objDataWrapper.AddParameter("@HOLDER_NAME",objAdditionalInterestInfo.HOLDER_NAME);
			objDataWrapper.AddParameter("@HOLDER_ADD1",objAdditionalInterestInfo.HOLDER_ADD1);
			objDataWrapper.AddParameter("@HOLDER_ADD2",objAdditionalInterestInfo.HOLDER_ADD2);
			objDataWrapper.AddParameter("@HOLDER_CITY",objAdditionalInterestInfo.HOLDER_CITY);
			objDataWrapper.AddParameter("@HOLDER_COUNTRY",objAdditionalInterestInfo.HOLDER_COUNTRY);
			objDataWrapper.AddParameter("@HOLDER_STATE",objAdditionalInterestInfo.HOLDER_STATE);
			objDataWrapper.AddParameter("@HOLDER_ZIP",objAdditionalInterestInfo.HOLDER_ZIP);
			objDataWrapper.AddParameter("@ADD_INT_ID",objAdditionalInterestInfo.ADD_INT_ID);

			if (objAdditionalInterestInfo.VEHICLE_ID != 0)
			{
				//Vehicle id is passed hence adding the vehicle id parameter
				objDataWrapper.AddParameter("@VEHICLE_ID",objAdditionalInterestInfo.VEHICLE_ID );

				//Passing the sp name for saving the additional interest for vehicle
				strStoredProc = "Proc_UpdateAdditionalInterest_ACORD";
			}
			else if(objAdditionalInterestInfo.BOAT_ID!=0)
			{
				//Vehicle id is passed hence adding the BOAT id parameter
				objDataWrapper.AddParameter("@BOAT_ID",objAdditionalInterestInfo.BOAT_ID);

				//Passing the sp name for saving the additional interest for BOAT
				strStoredProc = "Proc_UpdateWatercraftAdditionalInterest";

			}
			else if(objAdditionalInterestInfo.TRAILER_ID != 0 )
			{
				//Vehicle id is passed hence adding the BOAT id parameter
				objDataWrapper.AddParameter("@TRAILER_ID",objAdditionalInterestInfo.TRAILER_ID);

				//Passing the sp name for saving the additional interest for BOAT
				strStoredProc = "Proc_UpdateTrailerAdditionalInterest";
			}
		
			else
			{
				//Vehicle id is not passed hence adding the dwelling id parameter
				objDataWrapper.AddParameter("@DWELLING_ID",objAdditionalInterestInfo.DWELLING_ID);
				//	objDataWrapper.AddParameter("@CERTIFICATE_REQUIRED",objAdditionalInterestInfo.CERTIFICATE_REQUIRED);

				//Passing the sp name for saving the home owner additional interest 
				strStoredProc = "Proc_UpdateHomeOwnerAdditionalInterest";
			}

			objDataWrapper.ExecuteNonQuery(strStoredProc);
			return 1;
			
		}

		public int Update(Cms.Model.Application.ClsAdditionalInterestInfo objOldAdditionalInterestInfo, Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo, string hidCustomInfo)
		{
			int retValue;
			try
			{
				//Creating DataWrapper Object
				DataWrapper objDataWrapper		 = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				
				//Updating the Additional Interest Details
				DateTime	RecordDate		=	DateTime.Now;
			

				//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objAdditionalInterestInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@APP_ID",objAdditionalInterestInfo.APP_ID);
				objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
				objDataWrapper.AddParameter("@MEMO",objAdditionalInterestInfo.MEMO);
				objDataWrapper.AddParameter("@NATURE_OF_INTEREST",objAdditionalInterestInfo.NATURE_OF_INTEREST);
				objDataWrapper.AddParameter("@RANK",objAdditionalInterestInfo.RANK );
				objDataWrapper.AddParameter("@LOAN_REF_NUMBER",objAdditionalInterestInfo.LOAN_REF_NUMBER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objAdditionalInterestInfo.CREATED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objAdditionalInterestInfo.CREATED_DATETIME);
			
				objDataWrapper.AddParameter("@HOLDER_NAME",objAdditionalInterestInfo.HOLDER_NAME);
				objDataWrapper.AddParameter("@HOLDER_ADD1",objAdditionalInterestInfo.HOLDER_ADD1);
				objDataWrapper.AddParameter("@HOLDER_ADD2",objAdditionalInterestInfo.HOLDER_ADD2);
				objDataWrapper.AddParameter("@HOLDER_CITY",objAdditionalInterestInfo.HOLDER_CITY);
				objDataWrapper.AddParameter("@HOLDER_COUNTRY",objAdditionalInterestInfo.HOLDER_COUNTRY);
				objDataWrapper.AddParameter("@HOLDER_STATE",objAdditionalInterestInfo.HOLDER_STATE);
				objDataWrapper.AddParameter("@HOLDER_ZIP",objAdditionalInterestInfo.HOLDER_ZIP);
				objDataWrapper.AddParameter("@ADD_INT_ID",objAdditionalInterestInfo.ADD_INT_ID);
				if(RiskIdColumnName()!="")
					objDataWrapper.AddParameter(RiskIdColumnName(),objAdditionalInterestInfo.RISK_ID);
				if(BillMortagagee()!="")
					objDataWrapper.AddParameter(BillMortagagee(),objAdditionalInterestInfo.BILL_MORTAGAGEE);

				string strStoredProc = UpdateSpName();

	
				int returnResult = 0;
				
				try
				{
					//if transaction required
					if(this.boolTransactionRequired) 
					{
						objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"application\aspx\AdditionalInterest.aspx.resx");

						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						string strTranXML = objBuilder.GetTransactionLogXML(objOldAdditionalInterestInfo,objAdditionalInterestInfo);
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.APP_ID = objAdditionalInterestInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objAdditionalInterestInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objAdditionalInterestInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objAdditionalInterestInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1631", "");// "Additional Interest is modified";	
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						objTransactionInfo.CUSTOM_INFO		=	";Selected Holder/ Interest = " + objAdditionalInterestInfo.HOLDER_NAME +  hidCustomInfo;

						if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML=="")
							returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
						else
							returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				
					}
					else//if no transaction required
					{
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					
					}
					retValue = returnResult;
					ClsGeneralInformation obj=new ClsGeneralInformation();
					string strlob="";
					string strCalledFrom="";
					strCalledFrom=RiskIdColumnName();
					strlob=obj.Fun_GetLObID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.APP_ID,objAdditionalInterestInfo.APP_VERSION_ID);
					if(strlob == ((int)enumLOB.HOME).ToString() && strCalledFrom=="@DWELLING_ID")
					{
					
							ClsHomeCoverages objCoverages= new ClsHomeCoverages();
							objCoverages.UpdateCoveragesByRuleApp(objDataWrapper,objAdditionalInterestInfo.CUSTOMER_ID,
							objAdditionalInterestInfo.APP_ID,
							objAdditionalInterestInfo.APP_VERSION_ID,
							RuleType.RiskDependent,
							objAdditionalInterestInfo.DWELLING_ID );
					}
				}
				catch(Exception ex)
				{
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
					throw(ex);
				}			


				objDataWrapper.ClearParameteres();

				//Commit the Transaction
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				//Return the Value
				return retValue;
			}

			catch(Exception ex)
			{
				throw(ex);
			}
			
		}

		public int UpdatePolicyAdditionalInterest(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo,Cms.Model.Application.ClsAdditionalInterestInfo objOldAdditionalInterestInfo)
		{
			int intRetVal;
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//Set transaction label in the new object, if required
			if ( this.boolTransactionRequired)
			{
				objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"policies/aspx/automobile/PolicyAdditionalInterest.aspx.resx");
			}
			objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",objAdditionalInterestInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",objAdditionalInterestInfo.POLICY_VERSION_ID);
			objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
			objDataWrapper.AddParameter("@MEMO",objAdditionalInterestInfo.MEMO);
			objDataWrapper.AddParameter("@NATURE_OF_INTEREST",objAdditionalInterestInfo.NATURE_OF_INTEREST);
			objDataWrapper.AddParameter("@RANK",objAdditionalInterestInfo.RANK );
			objDataWrapper.AddParameter("@LOAN_REF_NUMBER",objAdditionalInterestInfo.LOAN_REF_NUMBER);
			objDataWrapper.AddParameter("@MODIFIED_BY",objAdditionalInterestInfo.CREATED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objAdditionalInterestInfo.CREATED_DATETIME);
			
			objDataWrapper.AddParameter("@HOLDER_NAME",objAdditionalInterestInfo.HOLDER_NAME);
			objDataWrapper.AddParameter("@HOLDER_ADD1",objAdditionalInterestInfo.HOLDER_ADD1);
			objDataWrapper.AddParameter("@HOLDER_ADD2",objAdditionalInterestInfo.HOLDER_ADD2);
			objDataWrapper.AddParameter("@HOLDER_CITY",objAdditionalInterestInfo.HOLDER_CITY);
			objDataWrapper.AddParameter("@HOLDER_COUNTRY",objAdditionalInterestInfo.HOLDER_COUNTRY);
			objDataWrapper.AddParameter("@HOLDER_STATE",objAdditionalInterestInfo.HOLDER_STATE);
			objDataWrapper.AddParameter("@HOLDER_ZIP",objAdditionalInterestInfo.HOLDER_ZIP);
			objDataWrapper.AddParameter("@ADD_INT_ID",objAdditionalInterestInfo.ADD_INT_ID);
			if(RiskIdColumnName()!="")
				objDataWrapper.AddParameter(RiskIdColumnName(),objAdditionalInterestInfo.RISK_ID );
			if(BillMortagagee()!="")
				objDataWrapper.AddParameter(BillMortagagee(),objAdditionalInterestInfo.BILL_MORTAGAGEE);
		
			string strStoredProc = UpdateSpName();
				
			int returnResult = 0;
				
			try
			{
				//if transaction required
				if(this.boolTransactionRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldAdditionalInterestInfo,objAdditionalInterestInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
						objTransactionInfo.POLICY_ID = objAdditionalInterestInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objAdditionalInterestInfo.POLICY_VERSION_ID; 
						objTransactionInfo.CLIENT_ID = objAdditionalInterestInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_TYPE_ID	=	1;
						objTransactionInfo.RECORDED_BY		=	objAdditionalInterestInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1631", "");//"Additional Interest is modified";	
						objTransactionInfo.CHANGE_XML		=	strTranXML;


						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}
					
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				}
				intRetVal = returnResult;
				objDataWrapper.ClearParameteres();

				
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string strlob="";
				string strCalledFrom="";
				strCalledFrom=RiskIdColumnName();
				DataSet dsTemp = new DataSet();
				dsTemp = obj.GetPolicyLOBID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.POLICY_ID,objAdditionalInterestInfo.POLICY_VERSION_ID);
                if(dsTemp.Tables[0].Rows.Count>0)
				strlob = dsTemp.Tables[0].Rows[0][0].ToString();
				if(strlob == ((int)enumLOB.HOME).ToString() && strCalledFrom=="@DWELLING_ID")
				{
					UpdatePolicyHomeCoverages(objDataWrapper,objAdditionalInterestInfo);
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
				
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
				throw(ex);
			}
			return intRetVal;
		}		

		
		#endregion		

		#region FILL/DELETE/ACTIVATE-DEACTIVATE ADDITIONAL INTEREST (APP/POL)

		public DataSet FillAdditionalInterestDetails(int CustomerID,int AppID,int AddVersionID,int RiskID,int addIntID)
		{
			string		strStoredProc	=	GetSpName();//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AddVersionID,SqlDbType.Int);
			if(RiskIdColumnName()!="")
				objDataWrapper.AddParameter(RiskIdColumnName(),RiskID,SqlDbType.Int);
			objDataWrapper.AddParameter("@ADD_INT_ID",addIntID,SqlDbType.Int);

			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
		
			return dsAccounts;

		}

		public DataTable GetAdditionalInterestList(int CustomerID,int PolID,int PolVersionID,int LobId , DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_GetAddlIntList_PolProc";
					
			DataSet dsAddIntList = new DataSet();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POL_ID",PolID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POL_VERSION_ID",PolVersionID,SqlDbType.Int);
			objDataWrapper.AddParameter("@LOB_ID",LobId,SqlDbType.Int);
			dsAddIntList = objDataWrapper.ExecuteDataSet(strStoredProc);
			if(dsAddIntList!=null && dsAddIntList.Tables.Count>0 && dsAddIntList.Tables[0]!=null)
				return dsAddIntList.Tables[0];
			else
				return null;

		}
		public DataTable GetAdditionalInterestList(int CustomerID,int PolID,int PolVersionID,int LobId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			return GetAdditionalInterestList(CustomerID,PolID,PolVersionID,LobId,objDataWrapper);
		}

		public int CheckInterestExistence(Cms.Model.Application.ClsAdditionalInterestInfo objInt,DataWrapper objDataWrapper)
		{
			string strStoredProc =	"Proc_Check_APP_ADD_OTHER_INT_EXISTS";

			objDataWrapper.AddParameter("@CUSTOMER_ID",objInt.CUSTOMER_ID);
			objDataWrapper.AddParameter("@APP_ID",objInt.APP_ID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objInt.APP_VERSION_ID);
			objDataWrapper.AddParameter("@VEHICLE_ID",objInt.VEHICLE_ID);
			objDataWrapper.AddParameter("@HOLDER_NAME",objInt.HOLDER_NAME);

			SqlParameter sHolderID = (SqlParameter)objDataWrapper.AddParameter("@ADD_INT_ID",SqlDbType.Int,ParameterDirection.Output);
			
			objDataWrapper.ExecuteNonQuery(strStoredProc);
			
			return Convert.ToInt32(sHolderID.Value);
			
		}
	
		public int Delete(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo,string hidCustomInfo)
		{
			int retValue;
			
			//Making the Database connection for common transaction
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//Inserting Additional Interest Details
			string		strStoredProc;
			DateTime	RecordDate		=	DateTime.Now;
				
			objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objAdditionalInterestInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@APP_ID",objAdditionalInterestInfo.APP_ID);
			objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
			objDataWrapper.AddParameter("@ADD_INT_ID",objAdditionalInterestInfo.ADD_INT_ID);
			if(RiskIdColumnName()!="")
				objDataWrapper.AddParameter(RiskIdColumnName(),objAdditionalInterestInfo.RISK_ID);
			strStoredProc = DeleteSpName();	

			int returnResult = 0;
				
			try
			{
				//if transaction required
				if(this.boolTransactionRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"application\aspx\AdditionalInterest.aspx.resx");

					string strTranXML = objBuilder.GetTransactionLogXML(objAdditionalInterestInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.APP_ID			=  objAdditionalInterestInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID   =  objAdditionalInterestInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=  objAdditionalInterestInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAdditionalInterestInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1632", "");// "Additional Interest Has Been Deleted";	
					
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					int rows = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult	=	1;
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				}
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string strlob="";
				string strCalledFrom="";
				strCalledFrom=RiskIdColumnName();
				strlob=obj.Fun_GetLObID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.APP_ID,objAdditionalInterestInfo.APP_VERSION_ID);
			if(strlob == ((int)enumLOB.HOME).ToString() && strCalledFrom=="@DWELLING_ID")
				{
						
					UpdateAppHomeCoverages(objDataWrapper,objAdditionalInterestInfo);
				}

				
				
				retValue = returnResult;
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					
			}
			
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
				throw(ex);
			}
			return retValue;
			
		}
	
		public int ActivateDeactivate(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo,string hidCustomInfo)
		{
			int retValue;
			
			//Making the Database connection for common transaction
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			//Inserting Additional Interest Details
			DateTime	RecordDate		=	DateTime.Now;
						
			objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",objAdditionalInterestInfo.APP_VERSION_ID);
			objDataWrapper.AddParameter("@APP_ID",objAdditionalInterestInfo.APP_ID);
			objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
			objDataWrapper.AddParameter("@ADD_INT_ID",objAdditionalInterestInfo.ADD_INT_ID);
			objDataWrapper.AddParameter("@IS_ACTIVE",objAdditionalInterestInfo.IS_ACTIVE);	
			if(RiskIdColumnName()!="")
				objDataWrapper.AddParameter(RiskIdColumnName(),objAdditionalInterestInfo.RISK_ID);			
			string strStoredProc = ActivateDeactivateSpName();
	
			int returnResult = 0;
				
			try
			{
				//if transaction requiredw
				if(this.boolTransactionRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"application\aspx\AdditionalInterest.aspx.resx");

					string strTranXML = objBuilder.GetTransactionLogXML(objAdditionalInterestInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.APP_ID			 = objAdditionalInterestInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	 = objAdditionalInterestInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		 = objAdditionalInterestInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	 =	1;
					objTransactionInfo.RECORDED_BY		 =	objAdditionalInterestInfo.CREATED_BY;

					if(objAdditionalInterestInfo.IS_ACTIVE=="Y")
						objTransactionInfo.TRANS_DESC	 =	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1633","");//"Additional Interest is activated";	
					else
                        objTransactionInfo.TRANS_DESC    = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1634", "");// "Additional Interest is deactivated";	

					objTransactionInfo.CHANGE_XML		=	strTranXML;	
					int rows = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult	=	1;
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				}
								
				ClsGeneralInformation obj=new ClsGeneralInformation();
				string strlob="";
				string strCalledFrom="";
				strCalledFrom=RiskIdColumnName();
				strlob=obj.Fun_GetLObID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.APP_ID,objAdditionalInterestInfo.APP_VERSION_ID);
				if(strlob == ((int)enumLOB.HOME).ToString() && strCalledFrom=="@DWELLING_ID")
				{
						
					UpdateAppHomeCoverages(objDataWrapper,objAdditionalInterestInfo);
				}
				retValue =  returnResult;
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
					
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
				throw(ex);
			}
					
			return retValue;		
					
		}
		
		public int ActivateDeactivatePolicyAdditionalInterest(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",objAdditionalInterestInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",objAdditionalInterestInfo.POLICY_VERSION_ID);
			objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
			//objDataWrapper.AddParameter("@VEHICLE_ID",objAdditionalInterestInfo.VEHICLE_ID);//Reference to Kasana sir mail - 9 July 09
			objDataWrapper.AddParameter("@ADD_INT_ID",objAdditionalInterestInfo.ADD_INT_ID);
			objDataWrapper.AddParameter("@IS_ACTIVE",objAdditionalInterestInfo.IS_ACTIVE);
		
			if(RiskIdColumnName()!="")
				objDataWrapper.AddParameter(RiskIdColumnName(),objAdditionalInterestInfo.RISK_ID);
			//Passing the sp name for saving the additional interest for vehicle
			string strStoredProc = ActivateDeactivateSpName();
			
			
			int returnResult = 0;
				
			try
			{
				//if transaction required
				if(this.boolTransactionRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"Policies/Aspx/Automobile/PolicyAdditionalInterest.aspx.resx");

					string strTranXML = objBuilder.GetTransactionLogXML(objAdditionalInterestInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.POLICY_ID = objAdditionalInterestInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID  = objAdditionalInterestInfo.POLICY_VERSION_ID; 
					objTransactionInfo.CLIENT_ID = objAdditionalInterestInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAdditionalInterestInfo.CREATED_BY;
					if(objAdditionalInterestInfo.IS_ACTIVE.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1635","");//"Policy Additional Interest is activated";	
					else
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1636", "");// "Policy Additional Interest is deactivated";	


					objTransactionInfo.CHANGE_XML		=	strTranXML;	

					int rows = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult	=	1;
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				}

				objDataWrapper.ClearParameteres();
				ClsGeneralInformation obj=new ClsGeneralInformation();
//				string strlob="";
				string strCalledFrom="";
				strCalledFrom=RiskIdColumnName();
				int intLob =0;
				intLob = Cms.BusinessLayer.BlCommon.ClsCommon.GetPolicyLOBID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.POLICY_ID,objAdditionalInterestInfo.POLICY_VERSION_ID);
//				strlob=obj.Fun_GetLObID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.POLICY_ID,objAdditionalInterestInfo.POLICY_VERSION_ID);
				if(intLob == ((int)enumLOB.HOME) && strCalledFrom=="@DWELLING_ID")
				{
					UpdatePolicyHomeCoverages(objDataWrapper,objAdditionalInterestInfo);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
				throw(ex);
			}
			
		}

		public int DeletePolicyAdditionalInterest(Cms.Model.Application.ClsAdditionalInterestInfo objAdditionalInterestInfo)
		{	
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objAdditionalInterestInfo.CUSTOMER_ID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",objAdditionalInterestInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",objAdditionalInterestInfo.POLICY_VERSION_ID);
			objDataWrapper.AddParameter("@HOLDER_ID",objAdditionalInterestInfo.HOLDER_ID);
			objDataWrapper.AddParameter("@ADD_INT_ID",objAdditionalInterestInfo.ADD_INT_ID);
			if(RiskIdColumnName()!="")
				objDataWrapper.AddParameter(RiskIdColumnName(),objAdditionalInterestInfo.RISK_ID );
		
			string strStoredProc = DeleteSpName();
			int returnResult = 0;
				
			try
			{
				//if transaction required
				if(this.boolTransactionRequired) 
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    objAdditionalInterestInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"Policies/Aspx/Automobile/PolicyAdditionalInterest.aspx.resx");

					string strTranXML = objBuilder.GetTransactionLogXML(objAdditionalInterestInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.POLICY_ID = objAdditionalInterestInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = objAdditionalInterestInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objAdditionalInterestInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAdditionalInterestInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1637", "");// "Additional Interest is deleted";	


					objTransactionInfo.CHANGE_XML		=	strTranXML;
					int rows = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult	=	1;
				}
				else//if no transaction required
				{
					returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				}

				objDataWrapper.ClearParameteres();
				ClsGeneralInformation obj=new ClsGeneralInformation();
//				string strlob="";
				string strCalledFrom="";
				strCalledFrom=RiskIdColumnName();
				int intLob =0;
				intLob = Cms.BusinessLayer.BlCommon.ClsCommon.GetPolicyLOBID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.POLICY_ID,objAdditionalInterestInfo.POLICY_VERSION_ID);
//				strlob=obj.Fun_GetLObID(objAdditionalInterestInfo.CUSTOMER_ID ,objAdditionalInterestInfo.POLICY_ID,objAdditionalInterestInfo.POLICY_VERSION_ID);
				if(intLob == ((int)enumLOB.HOME) && strCalledFrom=="@DWELLING_ID")
				{
					UpdatePolicyHomeCoverages(objDataWrapper,objAdditionalInterestInfo);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
				throw(ex);
			}
			
		}

        public DataSet GetAddIntDetails(int CustomerID, int policyID, int policyVersionID, int addIntID)
        {
            string strStoredProc = "Proc_GetPolicyAdditionalInterest";

            DataSet dsAccounts = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", policyID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", policyVersionID, SqlDbType.Int);
            //objDataWrapper.AddParameter("@ADD_INT_ID", addIntID, SqlDbType.Int);

            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);

            return dsAccounts;

        }

		public DataSet FillPolicyAdditionalInterestDetails(int CustomerID,int policyID,int policyVersionID,int RiskID,int addIntID)
		{
			string		strStoredProc	=	GetSpName();
					
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);
			if(RiskIdColumnName()!="")
				objDataWrapper.AddParameter(RiskIdColumnName(),RiskID,SqlDbType.Int);
			objDataWrapper.AddParameter("@ADD_INT_ID",addIntID,SqlDbType.Int);

			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
		
			return dsAccounts;

		}
		
		
		#endregion
	
		#region AUTO GENERATE ADDITIONAL INTEREST RANK (APP/POL)

		public string GetAppNewRankNumber(int CustomerID,int AppID,int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				objDataWrapper.AddParameter("@CALLEDFROM",CalledFromString());
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppNewRankNo");
				//
				if (dsTemp.Tables[0].Rows.Count>0)
				{
					return dsTemp.Tables[0].Rows[0]["RANK"].ToString();
				}
				else
				{	
					return "";
				}
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		public string GetPPAAppNewRankNumber(int CustomerID,int AppID,int AppVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@APP_ID",AppID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				objDataWrapper.AddParameter("@CALLEDFROM",CalledFromString());
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppNewRankNoAuto");
				//
				if (dsTemp.Tables[0].Rows.Count>0)
				{
					return dsTemp.Tables[0].Rows[0]["RANK"].ToString();
				}
				else
				{	
					return "";
				}
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		
		public string GetPolNewRankNumber(int CustomerID,int PolID,int PolVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
				objDataWrapper.AddParameter("@CALLEDFROM",CalledFromString());
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolNewRankNo");
				return dsTemp.Tables[0].Rows[0]["RANK"].ToString();		
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		public string GetPPAPolNewRankNumber(int CustomerID,int PolID,int PolVersionID, int VehicleID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objDataWrapper.AddParameter("@POLICY_ID",PolID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
				objDataWrapper.AddParameter("@CALLEDFROM",CalledFromString());
				objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolNewRankNoAuto");
				return dsTemp.Tables[0].Rows[0]["RANK"].ToString();		
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		#endregion
	}
		#region APPLICATION ADDITIONAL INTEREST CLASSES (All LOBs)

		// HomeOwner/Rental
		public class ClsadditionalInterestHomeOwner: ClsAdditionalInterest  
		{
			public override string RiskIdColumnName()
			{
				return "@DWELLING_ID";
			}

			public override string InsertSpName()
			{
				return "Proc_InsertHomeOwnerAdditionalInterest";
			}

			public override string GetSpName()
			{
				return "Proc_GetHomeOwnerAdditionalInterestDetails";
			}

			public override string UpdateSpName()
			{
				return "Proc_UpdateHomeOwnerAdditionalInterest";
			}

			public override string ActivateDeactivateSpName()
			{
				return "Proc_ActivateDeactivateHomeOwnerAdditionalInterest";
			}

			public override string DeleteSpName()
			{
				return "Proc_DeleteHomeOwnerAdditionalInterest";
			}

			public override string CalledFromString()
			{
				return "HOME";
			}
			public override string BillMortagagee()			
			{
				return "@BILL_MORTAGAGEE";
			}
			public override int GetAppBillMortagagee(int CustomerId, int AppId, int AppVersionId,int DwellingId,int AddIntId)
			{
				try
				{
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
					objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
					objDataWrapper.AddParameter("@APP_ID",AppId);
					objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);					
					objDataWrapper.AddParameter("@DWELLING_ID",DwellingId);					
					objDataWrapper.AddParameter("@ADD_INT_ID",AddIntId);					
					SqlParameter objRetParameter = (SqlParameter)objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);					
					objDataWrapper.ExecuteNonQuery("Proc_GetAppBillMortagagee");
					if(objRetParameter!=null && objRetParameter.Value.ToString()!="")
					{
						return int.Parse(objRetParameter.Value.ToString());
					}
					else
						return -1;
				}
				catch(Exception exc)
				{throw(exc);}
				finally
				{}

			}		
 
		}

		// Watercraft
		public class ClsadditionalInterestWatercraft: ClsAdditionalInterest 
		{
			public override string RiskIdColumnName()
			{
				return "@BOAT_ID";
			}

			public override string InsertSpName()
			{
				return "Proc_InsertWatercraftAdditionalInterest";
			}

			public override string GetSpName()
			{
				return "Proc_GetWatercraftAdditionalInterestDetails";
			}

			public override string UpdateSpName()
			{
				return "Proc_UpdateWatercraftAdditionalInterest";
			}


			public override string ActivateDeactivateSpName()
			{
				return "Proc_ActivateDeactivateWatercraftAdditionalInterest";
			}

			public override string DeleteSpName()
			{
				return "Proc_DeleteWatercraftAdditionalInterest";
			}

			public override string CalledFromString()
			{
				return "WAT";
			}
		}

		// Automobile/Motorcycle
		public class ClsadditionalInterestAutomobile: ClsAdditionalInterest 
		{
			public override string RiskIdColumnName()
			{
				return "@VEHICLE_ID";
			}

			public override string GetSpName()
			{
				return "Proc_GetAdditionalInterestDetails";
			}

			public override string InsertSpName()
			{
				return "Proc_InsertAdditionalInterest";
			}

			public override string UpdateSpName()
			{
				return "Proc_UpdateAdditionalInterest";
			}

			public override string ActivateDeactivateSpName()
			{
				return "Proc_ActivateDeactivateAdditionalInterest";
			}

			public override string DeleteSpName()
			{
				return "Proc_DeleteAdditionalInterest";
			}

			public override string CalledFromString()
			{
				return "PPA";
			}
		}

		// Trailer
		public class ClsadditionalInterestTrailer: ClsAdditionalInterest    
		{
			public override string RiskIdColumnName()
			{
				return "@TRAILER_ID";
			}

			public override string GetSpName()
			{
				return "Proc_GetTrailerAdditionalInterestDetails";
			}

			public override string InsertSpName()
			{
				return "Proc_InsertTrailerAdditionalInterest";
			}

			public override string UpdateSpName()
			{
				return "Proc_UpdateTrailerAdditionalInterest";
			}

			public override string ActivateDeactivateSpName()
			{
				return "Proc_ActivateDeactivateTrailerAdditionalInterest";
			}

			public override string DeleteSpName()
			{
				return "Proc_DeleteTrailerAdditionalInterest";
			}

			public override string CalledFromString()
			{
				return "WTR";
			}
		}

		// General Liability
		public class ClsadditionalInterestLiability: ClsAdditionalInterest  
		{

			public override string GetSpName()
			{
				return "Proc_GetGeneralLiabilityAdditionalInterestDetails";
			}

			public override string InsertSpName()
			{
				return "Proc_InsertGeneralAdditionalInterest";
			}

			public override string UpdateSpName()
			{
				return "Proc_UpdateGeneralAdditionalInterest";
			}

			public override string ActivateDeactivateSpName()
			{
				return "Proc_ActivateDeactivateAPP_GENERAL_HOLDER_INTEREST";
			}

			public override string DeleteSpName()
			{
				return "Proc_DeleteGeneralAdditionalInterest";
			}

			public override string CalledFromString()
			{
				return "GEN";
			}
		}
	// Recreational Vehicle
	public class ClsadditionalInterestRecVeh: ClsAdditionalInterest    
	{
		public override string RiskIdColumnName()
		{
			return "@REC_VEH_ID";
		}

		public override string GetSpName()
		{
			return "Proc_GetRecVehAdditionalInterestDetails";
		}

		public override string InsertSpName()
		{
			return "Proc_InsertRecVehAdditionalInterest";
		}

		public override string UpdateSpName()
		{
			return "Proc_UpdateRecVehAdditionalInterest";
		}

		public override string ActivateDeactivateSpName()
		{
			return "ActivateDeactivateRecVehAdditionalInterest";
		}

		public override string DeleteSpName()
		{
			return "DeleteRecVehAdditionalInterest";
		}

		public override string CalledFromString()
		{
			return "HREC";
		}
	}
	#endregion

		#region POLICY ADDITIONAL INTEREST CLASSES (All LOBs)

	// HomeOwner/Rental
	public class ClsPolAdditionalInterestHomeOwner: ClsAdditionalInterest  
	{
		public override string RiskIdColumnName()
		{
			return "@DWELLING_ID";
		}

		public override string InsertSpName()
		{
			return "Proc_InsertPolicyHomeAdditionalInterest";
		}

		public override string GetSpName()
		{
			return "Proc_GetPolicyHomeAdditionalInterestDetails";
		}

		public override string UpdateSpName()
		{
			return "Proc_UpdatePolicyHomeAdditionalInterest";
		}

		public override string ActivateDeactivateSpName()
		{
			return "ActivateDeactivatePolicyHomeAddtionalInterest";
		}

		public override string DeleteSpName()
		{
			return "Proc_DeletePolicyHomeAdditionalInterest";
		}

		public override string CalledFromString()
		{
			return "HOME";
		}
		
		public override string BillMortagagee()			
		{
			return "@BILL_MORTAGAGEE";
		}
		public override int GetPolBillMortagagee(int CustomerId, int PolId, int PolVersionId,int DwellingId,int AddIntId)
		{
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",PolId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionId);					
				objDataWrapper.AddParameter("@DWELLING_ID",DwellingId);					
				objDataWrapper.AddParameter("@ADD_INT_ID",AddIntId);					
				SqlParameter objRetParameter = (SqlParameter)objDataWrapper.AddParameter("@RETVAL",SqlDbType.Int,ParameterDirection.ReturnValue);					
				objDataWrapper.ExecuteNonQuery("Proc_GetPolBillMortagagee");
				if(objRetParameter!=null && objRetParameter.Value.ToString()!="")
				{
					return int.Parse(objRetParameter.Value.ToString());
				}
				else
					return -1;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}

		}
 
	}

	// Watercraft
	public class ClsPolAdditionalInterestWatercraft: ClsAdditionalInterest 
	{
		public override string RiskIdColumnName()
		{
			return "@BOAT_ID";
		}

		public override string InsertSpName()
		{
			return "Proc_InsertPolicyWaterCraftAdditionalInterest";
		}

		public override string GetSpName()
		{
			return "Proc_GetPolicyWatercraftAdditionalInterestDetails";
		}

		public override string UpdateSpName()
		{
			return "Proc_UpdatePolicyWaterCraftAdditionalInterest";
		}


		public override string ActivateDeactivateSpName()
		{
			return "ActivateDeactivatePolicyWaterCraftAddtionalInterest";
		}

		public override string DeleteSpName()
		{
			return "Proc_DeletePolicyWaterCraftAdditionalInterest";
		}

		public override string CalledFromString()
		{
			return "WAT";
		}
	}

	// Automobile/Motorcycle
	public class ClsPolAdditionalInterestAutomobile: ClsAdditionalInterest 
	{
		public override string RiskIdColumnName()
		{
			return "@VEHICLE_ID";
		}

		public override string GetSpName()
		{
			return "Proc_GetPolicyAutoAdditionalInterestDetails";
		}

		public override string InsertSpName()
		{
			return "Proc_InsertPolicyAdditionalInterest";
		}

		public override string UpdateSpName()
		{
			return "Proc_UpdatePolicyAdditionalInterest";
		}

		public override string ActivateDeactivateSpName()
		{
			return "Proc_ActivateDeactivatePolicyAdditionalInterest";
		}

		public override string DeleteSpName()
		{
			return "Proc_DeletePolicyAdditionalInterest";
		}

		public override string CalledFromString()
		{
			return "PPA";
		}
	}

	// Trailer
	public class ClsPolAdditionalInterestTrailer: ClsAdditionalInterest    
	{
		public override string RiskIdColumnName()
		{
			return "@TRAILER_ID";
		}

		public override string GetSpName()
		{
			return "Proc_GetPolicyWCTrailerAdditionalInterestDetails";
		}

		public override string InsertSpName()
		{
			return "Proc_InsertPolicyWCTrailerAdditionalInterest";
		}

		public override string UpdateSpName()
		{
			return "Proc_UpdatePolicyWCTrailerAdditionalInterest";
		}

		public override string ActivateDeactivateSpName()
		{
			return "ActivateDeactivatePolicyWCTrailerAddtionalInterest";
		}

		public override string DeleteSpName()
		{
			return "Proc_DeletePolicyWCTrailerAdditionalInterest";
		}

		public override string CalledFromString()
		{
			return "WTR";
		}
	}

	// General Liability
	public class ClsPolAdditionalInterestLiability: ClsAdditionalInterest  
	{

		public override string GetSpName()
		{
			return "Proc_GetPolicyGenLiabilityAdditionalInterest";
		}

		public override string InsertSpName()
		{
			return "Proc_InsertPolicyGenLiabilityAdditionalInterest";
		}

		public override string UpdateSpName()
		{
			return "Proc_UpdatePolicyGenLiabilityAdditionalInterest";
		}

		public override string ActivateDeactivateSpName()
		{
			return "Proc_ActivateDeactivateGenLiabilityAdditionalInterest";
		}

		public override string DeleteSpName()
		{
			return "Proc_DeletePolicyGenLiabilityAdditionalInterest";
		}

		public override string CalledFromString()
		{
			return "GEN";
		}
	}

	// Recreational Vehicle
	public class ClsPolAdditionalInterestRecVeh: ClsAdditionalInterest    
	{
		public override string RiskIdColumnName()
		{
			return "@REC_VEH_ID";
		}

		public override string GetSpName()
		{
			return "Proc_GetPolicyRecVehAdditionalInterestDetails";
		}

		public override string InsertSpName()
		{
			return "Proc_InsertPolicyRecVehAdditionalInterest";
		}

		public override string UpdateSpName()
		{
			return "Proc_UpdatePolicyRecVehAdditionalInterest";
		}

		public override string ActivateDeactivateSpName()
		{
			return "Proc_ActivateDeactivatePolicyRecVehAdditionalInterest";
		}

		public override string DeleteSpName()
		{
			return "Proc_DeletePolicyRecVehAdditionalInterest";
		}

		public override string CalledFromString()
		{
			return "HREC";
		}
	}
	#endregion
}
