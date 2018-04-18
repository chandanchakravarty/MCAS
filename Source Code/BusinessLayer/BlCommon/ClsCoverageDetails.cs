/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	7/4/2005 12:35:24 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Ravindra
<Modified By			: - 05-17-2006
<Purpose				: - Optimisation
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Maintenance;
using Cms.DataLayer;



namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsCoverageDetails.
	/// </summary>
	public class ClsCoverageDetails :Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private bool boolTransactionRequired			= true;
		public ClsCoverageDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		private const string MNT_COVERAGE ="MNT_COVERAGE";
		private	bool boolTransactionLog;
	
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateCoverage";
	
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
		public int AddReinsuranceCoverage(ClsCoverageDetailsInfo objCoverageDetailsInfo)
		{
			string		strStoredProc	=	"Proc_InsertReinsuranceCoverage";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//objDataWrapper.AddParameter("@COV_ID",objCoverageDetailsInfo.COV_ID);
				objDataWrapper.AddParameter("@COV_REF_CODE",objCoverageDetailsInfo.COV_REF_CODE);
				objDataWrapper.AddParameter("@COV_CODE",objCoverageDetailsInfo.COV_CODE);
				objDataWrapper.AddParameter("@COV_DES",objCoverageDetailsInfo.COV_DES);
				objDataWrapper.AddParameter("@STATE_ID",objCoverageDetailsInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objCoverageDetailsInfo.LOB_ID);
				objDataWrapper.AddParameter("@IS_DEFAULT",objCoverageDetailsInfo.IS_DEFAULT,SqlDbType.Bit);
				objDataWrapper.AddParameter("@IS_ACTIVE",objCoverageDetailsInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@TYPE",objCoverageDetailsInfo.TYPE);
				objDataWrapper.AddParameter("@LIMIT_TYPE",objCoverageDetailsInfo.LIMIT_TYPE);
				objDataWrapper.AddParameter("@DEDUCTIBLE_TYPE",objCoverageDetailsInfo.DEDUCTIBLE_TYPE);
                objDataWrapper.AddParameter("@SUB_LOB_ID", objCoverageDetailsInfo.SUB_LOB_ID);
				
				objDataWrapper.AddParameter("@IsLimitApplicable",objCoverageDetailsInfo.IsLimitApplicable);
				objDataWrapper.AddParameter("@IsDeductApplicable",objCoverageDetailsInfo.IsDeductApplicable);

                //Shikha - 1226
                if (objCoverageDetailsInfo.COV_TYPE_ABBR == "")
                    objDataWrapper.AddParameter("@COV_TYPE_ABBR", null);
                else
                    objDataWrapper.AddParameter("@COV_TYPE_ABBR", objCoverageDetailsInfo.COV_TYPE_ABBR);
                if(objCoverageDetailsInfo.SUSEP_COV_CODE == 0)
                    objDataWrapper.AddParameter("@SUSEP_COV_CODE",DBNull.Value);
                else
                objDataWrapper.AddParameter("@SUSEP_COV_CODE", objCoverageDetailsInfo.SUSEP_COV_CODE);
				
				if(objCoverageDetailsInfo.FORM_NUMBER=="")
					objDataWrapper.AddParameter("@FORM_NUMBER",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@FORM_NUMBER",objCoverageDetailsInfo.FORM_NUMBER);

				objDataWrapper.AddParameter("@COVERAGE_TYPE",objCoverageDetailsInfo.COVERAGE_TYPE);
				objDataWrapper.AddParameter("@IS_MANDATORY",objCoverageDetailsInfo.IS_MANDATORY);
				objDataWrapper.AddParameter("@RANK",objCoverageDetailsInfo.RANK);
				if(objCoverageDetailsInfo.EFFECTIVE_FROM_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objCoverageDetailsInfo.EFFECTIVE_FROM_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",null);
				if(objCoverageDetailsInfo.EFFECTIVE_TO_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objCoverageDetailsInfo.EFFECTIVE_TO_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",null);
				if(objCoverageDetailsInfo.DISABLED_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@DISABLED_DATE",objCoverageDetailsInfo.DISABLED_DATE);
				else
					objDataWrapper.AddParameter("@DISABLED_DATE",null);

				objDataWrapper.AddParameter("@REINSURANCE_LOB",objCoverageDetailsInfo.REINSURANCE_LOB);
				objDataWrapper.AddParameter("@REINSURANCE_COV",objCoverageDetailsInfo.REINSURANCE_COV);
				objDataWrapper.AddParameter("@ASLOB",objCoverageDetailsInfo.ASLOB);
				objDataWrapper.AddParameter("@REINSURANCE_CALC",objCoverageDetailsInfo.REINSURANCE_CALC);
				objDataWrapper.AddParameter("@REIN_REPORT_BUCK",objCoverageDetailsInfo.REIN_REPORT_BUCK);
				objDataWrapper.AddParameter("@REIN_REPORT_BUCK_COMM",objCoverageDetailsInfo.REIN_REPORT_BUCK_COMM);
				

				objDataWrapper.AddParameter("@COMM_VEHICLE",objCoverageDetailsInfo.COMM_VEHICLE);
				objDataWrapper.AddParameter("@COMM_REIN_COV_CAT",objCoverageDetailsInfo.COMM_REIN_COV_CAT);
				objDataWrapper.AddParameter("@REIN_ASLOB",objCoverageDetailsInfo.REIN_ASLOB);
				objDataWrapper.AddParameter("@COMM_CALC",objCoverageDetailsInfo.COMM_CALC);

				if(objCoverageDetailsInfo.ISADDDEDUCTIBLE_APP != 0)
					objDataWrapper.AddParameter("@ISADDDEDUCTIBLE_APP",objCoverageDetailsInfo.ISADDDEDUCTIBLE_APP);
				else
					objDataWrapper.AddParameter("@ISADDDEDUCTIBLE_APP",0);

				objDataWrapper.AddParameter("@ADDDEDUCTIBLE_TYPE",objCoverageDetailsInfo.ADDDEDUCTIBLE_TYPE);
                if (objCoverageDetailsInfo.MANDATORY_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@MANDATORY_DATE", objCoverageDetailsInfo.MANDATORY_DATE);
                else
                    objDataWrapper.AddParameter("@MANDATORY_DATE", null);

                if (objCoverageDetailsInfo.NON_MANDATORY_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@NON_MANDATORY_DATE", objCoverageDetailsInfo.NON_MANDATORY_DATE);
                else
                    objDataWrapper.AddParameter("@NON_MANDATORY_DATE", null);

                if (objCoverageDetailsInfo.DEFAULT_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@DEFAULT_DATE", objCoverageDetailsInfo.DEFAULT_DATE);
                else
                    objDataWrapper.AddParameter("@DEFAULT_DATE", null);

                if (objCoverageDetailsInfo.NON_DEFAULT_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@NON_DEFAULT_DATE", objCoverageDetailsInfo.NON_DEFAULT_DATE);
                else
                    objDataWrapper.AddParameter("@NON_DEFAULT_DATE", null);
                // Added by Praveen Kumar on 19/08/2010 starts ---------
                objDataWrapper.AddParameter("@DISPLAY_ON_CLAIM", objCoverageDetailsInfo.DISPLAY_ON_CLAIM);
                objDataWrapper.AddParameter("@CLAIM_RESERVE_APPLY", objCoverageDetailsInfo.CLAIM_RESERVE_APPLY);
                // Added by Praveen Kumar on 19/08/2010 Ends ---------
               
                //aNKIT
                objDataWrapper.AddParameter("@IS_MAIN", objCoverageDetailsInfo.IS_MAIN);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@COV_ID",SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objCoverageDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddCoverageDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objCoverageDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					//	objTransactionInfo.RECORDED_BY		=	objCoverageDetailsInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objCoverageDetailsInfo.COV_ID= int.Parse(objSqlParameter.Value.ToString());
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
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objCoverageDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsCoverageDetailsInfo objCoverageDetailsInfo)
		{
			int intEndorsementID=0;
			return this.Add(objCoverageDetailsInfo,out intEndorsementID);
		}
		public int Add(ClsCoverageDetailsInfo objCoverageDetailsInfo,out int intEndorsementID)
		{
			string		strStoredProc	=	"Proc_InsertCoverage";
			DateTime	RecordDate		=	DateTime.Now;
			intEndorsementID=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//objDataWrapper.AddParameter("@COV_ID",objCoverageDetailsInfo.COV_ID);
				objDataWrapper.AddParameter("@COV_REF_CODE",objCoverageDetailsInfo.COV_REF_CODE);
				objDataWrapper.AddParameter("@COV_CODE",objCoverageDetailsInfo.COV_CODE);
				objDataWrapper.AddParameter("@COV_DES",objCoverageDetailsInfo.COV_DES);
				objDataWrapper.AddParameter("@STATE_ID",objCoverageDetailsInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objCoverageDetailsInfo.LOB_ID);
				objDataWrapper.AddParameter("@IS_DEFAULT",objCoverageDetailsInfo.IS_DEFAULT,SqlDbType.Bit);
				objDataWrapper.AddParameter("@IS_ACTIVE",objCoverageDetailsInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@TYPE",objCoverageDetailsInfo.TYPE);
				objDataWrapper.AddParameter("@PURPOSE",objCoverageDetailsInfo.PURPOSE);
				objDataWrapper.AddParameter("@LIMIT_TYPE",objCoverageDetailsInfo.LIMIT_TYPE);
				objDataWrapper.AddParameter("@DEDUCTIBLE_TYPE",objCoverageDetailsInfo.DEDUCTIBLE_TYPE);
                objDataWrapper.AddParameter("@SUB_LOB_ID", objCoverageDetailsInfo.SUB_LOB_ID);
				objDataWrapper.AddParameter("@IsLimitApplicable",objCoverageDetailsInfo.IsLimitApplicable);
				objDataWrapper.AddParameter("@IsDeductApplicable",objCoverageDetailsInfo.IsDeductApplicable);
                //Shikha - 1226
                if (objCoverageDetailsInfo.COV_TYPE_ABBR == "")
                    objDataWrapper.AddParameter("@COV_TYPE_ABBR", null);
                else
                    objDataWrapper.AddParameter("@COV_TYPE_ABBR", objCoverageDetailsInfo.COV_TYPE_ABBR);
                if (objCoverageDetailsInfo.SUSEP_COV_CODE == 0)
                    objDataWrapper.AddParameter("@SUSEP_COV_CODE", DBNull.Value);
                else
                    objDataWrapper.AddParameter("@SUSEP_COV_CODE", objCoverageDetailsInfo.SUSEP_COV_CODE);
				// Gaurav 03/10/2005 added for Section 1 or section 2 coverages in Homeowners section
				// New Field Added
				if(objCoverageDetailsInfo.INCLUDED==0)
					objDataWrapper.AddParameter("@INCLUDED",null);
				else
					objDataWrapper.AddParameter("@INCLUDED",objCoverageDetailsInfo.INCLUDED);
	
				if(objCoverageDetailsInfo.FORM_NUMBER=="")
					objDataWrapper.AddParameter("@FORM_NUMBER",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@FORM_NUMBER",objCoverageDetailsInfo.FORM_NUMBER);

				objDataWrapper.AddParameter("@COVERAGE_TYPE",objCoverageDetailsInfo.COVERAGE_TYPE);
				objDataWrapper.AddParameter("@IS_MANDATORY",objCoverageDetailsInfo.IS_MANDATORY);
				objDataWrapper.AddParameter("@RANK",objCoverageDetailsInfo.RANK);
				if(objCoverageDetailsInfo.EFFECTIVE_FROM_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objCoverageDetailsInfo.EFFECTIVE_FROM_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",null);
				if(objCoverageDetailsInfo.EFFECTIVE_TO_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objCoverageDetailsInfo.EFFECTIVE_TO_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",null);
				if(objCoverageDetailsInfo.DISABLED_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@DISABLED_DATE",objCoverageDetailsInfo.DISABLED_DATE);
				else
					objDataWrapper.AddParameter("@DISABLED_DATE",null);

				// Swarup 28/03/2007 added for Reinsurance Section 
				// New Field Added

				objDataWrapper.AddParameter("@REINSURANCE_LOB",objCoverageDetailsInfo.REINSURANCE_LOB);
				objDataWrapper.AddParameter("@REINSURANCE_COV",objCoverageDetailsInfo.REINSURANCE_COV);
				objDataWrapper.AddParameter("@ASLOB",objCoverageDetailsInfo.ASLOB);
				objDataWrapper.AddParameter("@REINSURANCE_CALC",objCoverageDetailsInfo.REINSURANCE_CALC);
				objDataWrapper.AddParameter("@REIN_REPORT_BUCK",objCoverageDetailsInfo.REIN_REPORT_BUCK);
				objDataWrapper.AddParameter("@REIN_REPORT_BUCK_COMM",objCoverageDetailsInfo.REIN_REPORT_BUCK_COMM);

				
				objDataWrapper.AddParameter("@COMM_VEHICLE",objCoverageDetailsInfo.COMM_VEHICLE);
				objDataWrapper.AddParameter("@COMM_REIN_COV_CAT",objCoverageDetailsInfo.COMM_REIN_COV_CAT);
				objDataWrapper.AddParameter("@REIN_ASLOB",objCoverageDetailsInfo.REIN_ASLOB);
				objDataWrapper.AddParameter("@COMM_CALC",objCoverageDetailsInfo.COMM_CALC);

				if(objCoverageDetailsInfo.ISADDDEDUCTIBLE_APP != 0)
					objDataWrapper.AddParameter("@ISADDDEDUCTIBLE_APP",objCoverageDetailsInfo.ISADDDEDUCTIBLE_APP);
				else
					objDataWrapper.AddParameter("@ISADDDEDUCTIBLE_APP",0);

				objDataWrapper.AddParameter("@ADDDEDUCTIBLE_TYPE",objCoverageDetailsInfo.ADDDEDUCTIBLE_TYPE);
				objDataWrapper.AddParameter("@IS_SYSTEM_GENERAED",objCoverageDetailsInfo.IS_SYSTEM_GENERAED);
                // added by sonal for these 4 new fields
                if (objCoverageDetailsInfo.MANDATORY_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@MANDATORY_DATE", objCoverageDetailsInfo.MANDATORY_DATE);
                else
                    objDataWrapper.AddParameter("@MANDATORY_DATE", null);

                if (objCoverageDetailsInfo.NON_MANDATORY_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@NON_MANDATORY_DATE", objCoverageDetailsInfo.NON_MANDATORY_DATE);
                else
                    objDataWrapper.AddParameter("@NON_MANDATORY_DATE", null);

                if (objCoverageDetailsInfo.DEFAULT_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@DEFAULT_DATE", objCoverageDetailsInfo.DEFAULT_DATE);
                else
                    objDataWrapper.AddParameter("@DEFAULT_DATE", null);

                if (objCoverageDetailsInfo.NON_DEFAULT_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@NON_DEFAULT_DATE", objCoverageDetailsInfo.NON_DEFAULT_DATE);
                else
                    objDataWrapper.AddParameter("@NON_DEFAULT_DATE", null);

                // Added by Praveen Kumar on 19/08/2010 starts ---------
                objDataWrapper.AddParameter("@DISPLAY_ON_CLAIM", objCoverageDetailsInfo.DISPLAY_ON_CLAIM);
                objDataWrapper.AddParameter("@CLAIM_RESERVE_APPLY", objCoverageDetailsInfo.CLAIM_RESERVE_APPLY);
                // Added by Praveen Kumar on 19/08/2010 Ends ---------
                //aNKIT
                objDataWrapper.AddParameter("@IS_MAIN", objCoverageDetailsInfo.IS_MAIN);

				// Gaurav 03/10/2005 added for Section 1 or section 2 coverages in Homeowners section
				// New Field Added
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@COV_ID",SqlDbType.Int,ParameterDirection.Output);
				//added by Pravesh for Default Endorsement saved if Coverage is of type Endosemetn Coverage
				SqlParameter objSqlParameter1  = (SqlParameter) objDataWrapper.AddParameter("@ENDORSMENT_ID",SqlDbType.Int,ParameterDirection.Output);


				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objCoverageDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddCoverageDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objCoverageDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
				//	objTransactionInfo.RECORDED_BY		=	objCoverageDetailsInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objCoverageDetailsInfo.COV_ID= int.Parse(objSqlParameter.Value.ToString());
				//added by Pravesh for Default Endorsement saved if Coverage is of type Endosemetn Coverage
				if (objSqlParameter1.Value !=DBNull.Value )
					intEndorsementID=int.Parse(objSqlParameter1.Value.ToString());

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
		

		
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldCoverageDetailsInfo">Model object having old information</param>
		/// <param name="objCoverageDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsCoverageDetailsInfo objOldCoverageDetailsInfo,ClsCoverageDetailsInfo objCoverageDetailsInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdateCoverage";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@COV_ID",objCoverageDetailsInfo.COV_ID);
				objDataWrapper.AddParameter("@COV_REF_CODE",objCoverageDetailsInfo.COV_REF_CODE);
				objDataWrapper.AddParameter("@COV_CODE",objCoverageDetailsInfo.COV_CODE);
				objDataWrapper.AddParameter("@COV_DES",objCoverageDetailsInfo.COV_DES);
				objDataWrapper.AddParameter("@STATE_ID",objCoverageDetailsInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objCoverageDetailsInfo.LOB_ID);
				objDataWrapper.AddParameter("@IS_DEFAULT",objCoverageDetailsInfo.IS_DEFAULT,SqlDbType.Bit);
				objDataWrapper.AddParameter("@TYPE",objCoverageDetailsInfo.TYPE);
				objDataWrapper.AddParameter("@PURPOSE",objCoverageDetailsInfo.PURPOSE);
				objDataWrapper.AddParameter("@LIMIT_TYPE",objCoverageDetailsInfo.LIMIT_TYPE);
				objDataWrapper.AddParameter("@DEDUCTIBLE_TYPE",objCoverageDetailsInfo.DEDUCTIBLE_TYPE);			
				objDataWrapper.AddParameter("@IsLimitApplicable",objCoverageDetailsInfo.IsLimitApplicable);
				objDataWrapper.AddParameter("@IsDeductApplicable",objCoverageDetailsInfo.IsDeductApplicable);
                objDataWrapper.AddParameter("@SUB_LOB_ID", objCoverageDetailsInfo.SUB_LOB_ID);
                if(objCoverageDetailsInfo.COV_TYPE_ABBR == "")
                objDataWrapper.AddParameter("@COV_TYPE_ABBR", null);
                else
                    objDataWrapper.AddParameter("@COV_TYPE_ABBR", objCoverageDetailsInfo.COV_TYPE_ABBR);
                
                if (objCoverageDetailsInfo.SUSEP_COV_CODE == 0)
                    objDataWrapper.AddParameter("@SUSEP_COV_CODE", DBNull.Value);
                else
                    objDataWrapper.AddParameter("@SUSEP_COV_CODE", objCoverageDetailsInfo.SUSEP_COV_CODE);
				// Gaurav 03/10/2005 added for Section 1 or section 2 coverages in Homeowners section
				// New Field Added
				if(objCoverageDetailsInfo.INCLUDED==0)
					objDataWrapper.AddParameter("@INCLUDED",null);
				else
					objDataWrapper.AddParameter("@INCLUDED",objCoverageDetailsInfo.INCLUDED);

				if(objCoverageDetailsInfo.FORM_NUMBER=="")
					objDataWrapper.AddParameter("@FORM_NUMBER",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@FORM_NUMBER",objCoverageDetailsInfo.FORM_NUMBER);
				
				objDataWrapper.AddParameter("@COVERAGE_TYPE",objCoverageDetailsInfo.COVERAGE_TYPE);
				objDataWrapper.AddParameter("@IS_MANDATORY",objCoverageDetailsInfo.IS_MANDATORY);
				objDataWrapper.AddParameter("@RANK",objCoverageDetailsInfo.RANK);

				if(objCoverageDetailsInfo.EFFECTIVE_FROM_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objCoverageDetailsInfo.EFFECTIVE_FROM_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",null);
				if(objCoverageDetailsInfo.EFFECTIVE_TO_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objCoverageDetailsInfo.EFFECTIVE_TO_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",null);
				if(objCoverageDetailsInfo.DISABLED_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@DISABLED_DATE",objCoverageDetailsInfo.DISABLED_DATE);
				else
					objDataWrapper.AddParameter("@DISABLED_DATE",null);
				// Gaurav 03/10/2005 added for Section 1 or section 2 coverages in Homeowners section
				// New Field Added
				
				// Swarup 28/03/2007 added for Reinsurance Section 
				// New Field Added

				objDataWrapper.AddParameter("@REINSURANCE_LOB",objCoverageDetailsInfo.REINSURANCE_LOB);
				objDataWrapper.AddParameter("@REINSURANCE_COV",objCoverageDetailsInfo.REINSURANCE_COV);
				objDataWrapper.AddParameter("@ASLOB",objCoverageDetailsInfo.ASLOB);
				objDataWrapper.AddParameter("@REINSURANCE_CALC",objCoverageDetailsInfo.REINSURANCE_CALC);
				objDataWrapper.AddParameter("@REIN_REPORT_BUCK",objCoverageDetailsInfo.REIN_REPORT_BUCK);
				objDataWrapper.AddParameter("@REIN_REPORT_BUCK_COMM",objCoverageDetailsInfo.REIN_REPORT_BUCK_COMM);


				objDataWrapper.AddParameter("@COMM_VEHICLE",objCoverageDetailsInfo.COMM_VEHICLE);
				objDataWrapper.AddParameter("@COMM_REIN_COV_CAT",objCoverageDetailsInfo.COMM_REIN_COV_CAT);
				objDataWrapper.AddParameter("@REIN_ASLOB",objCoverageDetailsInfo.REIN_ASLOB);
				objDataWrapper.AddParameter("@COMM_CALC",objCoverageDetailsInfo.COMM_CALC);
                if (objCoverageDetailsInfo.MANDATORY_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@MANDATORY_DATE", objCoverageDetailsInfo.MANDATORY_DATE);
                else
                    objDataWrapper.AddParameter("@MANDATORY_DATE", null);

                if (objCoverageDetailsInfo.NON_MANDATORY_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@NON_MANDATORY_DATE", objCoverageDetailsInfo.NON_MANDATORY_DATE);
                else
                    objDataWrapper.AddParameter("@NON_MANDATORY_DATE", null);

                if (objCoverageDetailsInfo.DEFAULT_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@DEFAULT_DATE", objCoverageDetailsInfo.DEFAULT_DATE);
                else
                    objDataWrapper.AddParameter("@DEFAULT_DATE", null);

                if (objCoverageDetailsInfo.NON_DEFAULT_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@NON_DEFAULT_DATE", objCoverageDetailsInfo.NON_DEFAULT_DATE);
                else
                    objDataWrapper.AddParameter("@NON_DEFAULT_DATE", null);
                 // Added by Praveen Kumar on 19/08/2010 starts ---------
                objDataWrapper.AddParameter("@DISPLAY_ON_CLAIM", objCoverageDetailsInfo.DISPLAY_ON_CLAIM);
                objDataWrapper.AddParameter("@CLAIM_RESERVE_APPLY", objCoverageDetailsInfo.CLAIM_RESERVE_APPLY);
                // Added by Praveen Kumar on 19/08/2010 Ends ---------

                //aNKIT
                objDataWrapper.AddParameter("@IS_MAIN", objCoverageDetailsInfo.IS_MAIN);
               // objDataWrapper.AddParameter("@SUB_LOB_ID", objCoverageDetailsInfo.SUB_LOB_ID);
				if(TransactionRequired) 
				{
					objCoverageDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddCoverageDetails.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldCoverageDetailsInfo,objCoverageDetailsInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objCoverageDetailsInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
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
		public int UpdateReinsuranceCoverage(ClsCoverageDetailsInfo objOldCoverageDetailsInfo,ClsCoverageDetailsInfo objCoverageDetailsInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdateReinsuranceCoverage";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@COV_ID",objCoverageDetailsInfo.COV_ID);
				if(objCoverageDetailsInfo.FORM_NUMBER=="")
					objDataWrapper.AddParameter("@FORM_NUMBER",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@FORM_NUMBER",objCoverageDetailsInfo.FORM_NUMBER);
				
				if(objCoverageDetailsInfo.EFFECTIVE_FROM_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objCoverageDetailsInfo.EFFECTIVE_FROM_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",null);
				if(objCoverageDetailsInfo.EFFECTIVE_TO_DATE.Ticks != 0)
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objCoverageDetailsInfo.EFFECTIVE_TO_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",null);
				if(objCoverageDetailsInfo.DISABLED_DATE.Ticks !=0)
					objDataWrapper.AddParameter("@DISABLED_DATE",objCoverageDetailsInfo.DISABLED_DATE);
				else
					objDataWrapper.AddParameter("@DISABLED_DATE",null);
			
				objDataWrapper.AddParameter("@REINSURANCE_LOB",objCoverageDetailsInfo.REINSURANCE_LOB);
				objDataWrapper.AddParameter("@REINSURANCE_COV",objCoverageDetailsInfo.REINSURANCE_COV);
				objDataWrapper.AddParameter("@ASLOB",objCoverageDetailsInfo.ASLOB);
				objDataWrapper.AddParameter("@REINSURANCE_CALC",objCoverageDetailsInfo.REINSURANCE_CALC);
				objDataWrapper.AddParameter("@REIN_REPORT_BUCK",objCoverageDetailsInfo.REIN_REPORT_BUCK);
				objDataWrapper.AddParameter("@REIN_REPORT_BUCK_COMM",objCoverageDetailsInfo.REIN_REPORT_BUCK_COMM);

				objDataWrapper.AddParameter("@COMM_VEHICLE",objCoverageDetailsInfo.COMM_VEHICLE);
				objDataWrapper.AddParameter("@COMM_REIN_COV_CAT",objCoverageDetailsInfo.COMM_REIN_COV_CAT);
				objDataWrapper.AddParameter("@REIN_ASLOB",objCoverageDetailsInfo.REIN_ASLOB);
				objDataWrapper.AddParameter("@COMM_CALC",objCoverageDetailsInfo.COMM_CALC);
                objDataWrapper.AddParameter("@IS_MANDATORY", objCoverageDetailsInfo.IS_MANDATORY);
                objDataWrapper.AddParameter("@IS_DEFAULT", objCoverageDetailsInfo.IS_DEFAULT, SqlDbType.Bit);
                if (objCoverageDetailsInfo.MANDATORY_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@MANDATORY_DATE", objCoverageDetailsInfo.MANDATORY_DATE);
                else
                    objDataWrapper.AddParameter("@MANDATORY_DATE", null);

                if (objCoverageDetailsInfo.NON_MANDATORY_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@NON_MANDATORY_DATE", objCoverageDetailsInfo.NON_MANDATORY_DATE);
                else
                    objDataWrapper.AddParameter("@NON_MANDATORY_DATE", null);

                if (objCoverageDetailsInfo.DEFAULT_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@DEFAULT_DATE", objCoverageDetailsInfo.DEFAULT_DATE);
                else
                    objDataWrapper.AddParameter("@DEFAULT_DATE", null);

                if (objCoverageDetailsInfo.NON_DEFAULT_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@NON_DEFAULT_DATE", objCoverageDetailsInfo.NON_DEFAULT_DATE);
                else
                    objDataWrapper.AddParameter("@NON_DEFAULT_DATE", null);
                // Added by Praveen Kumar on 19/08/2010 starts ---------
                objDataWrapper.AddParameter("@DISPLAY_ON_CLAIM", objCoverageDetailsInfo.DISPLAY_ON_CLAIM);
                objDataWrapper.AddParameter("@CLAIM_RESERVE_APPLY", objCoverageDetailsInfo.CLAIM_RESERVE_APPLY);
                // Added by Praveen Kumar on 19/08/2010 Ends ---------

                //aNKIT
                objDataWrapper.AddParameter("@IS_MAIN", objCoverageDetailsInfo.IS_MAIN);
                //Abhinav
                objDataWrapper.AddParameter("@SUB_LOB_ID", objCoverageDetailsInfo.SUB_LOB_ID);

                //Shikha - 1226
                if (objCoverageDetailsInfo.COV_TYPE_ABBR == "")
                    objDataWrapper.AddParameter("@COV_TYPE_ABBR", null);
                else
                    objDataWrapper.AddParameter("@COV_TYPE_ABBR", objCoverageDetailsInfo.COV_TYPE_ABBR);

                if (objCoverageDetailsInfo.SUSEP_COV_CODE == 0)
                    objDataWrapper.AddParameter("@SUSEP_COV_CODE", DBNull.Value);
                else
                    objDataWrapper.AddParameter("@SUSEP_COV_CODE", objCoverageDetailsInfo.SUSEP_COV_CODE);
               

				if(TransactionRequired) 
				{
					objCoverageDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddCoverageDetails.aspx.resx");
					string strUpdate = objBuilder.GetUpdateSQL(objOldCoverageDetailsInfo,objCoverageDetailsInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objCoverageDetailsInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
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
		
		public static string GetCoverageDetailXml(int CovId)
		{			
			string strProcedure = "Proc_GetCoverageDetailXml";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@Cov_Id",CovId);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);

				if(objDataSet.Tables[0].Rows.Count!=0)
				{
					return objDataSet.GetXml();
				}
				else
				{
					return "";
				}
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}			
		}
		
		public static DataSet GetCoverageDetail(int CovId)
		{
			return GetCoverageDetail(CovId,"");
		}
		public static DataSet GetCoverageDetail(int CovId,string strCovType)
		{			
			string strProcedure = "Proc_GetCoverageDetailXml";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@Cov_Id",CovId);
				objDataWrapper.AddParameter("@COV_TYPE",strCovType);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);//Added by Pradeep Kushwaha on 15-Feb-2011
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);

				return objDataSet;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}			
		}
		
		public static int FindDefaultLOBForCoverage(int lob_Id,out int defaultCount)
		{
			string strProcedure = "Proc_FindDefaultLOBForCoverage";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@Lob_Id",lob_Id);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@TotalRecords",SqlDbType.Int,ParameterDirection.Output);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				defaultCount=int.Parse(objSqlParameter.Value.ToString());
				return defaultCount;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}		
		}
		

		public static DataTable  GetLobForSelectedState(int state)
		{
			string strProcedure = "Proc_GetLobForSelectedState";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@State_Id",state);
                objDataWrapper.AddParameter("@LangID", BL_LANG_ID);//Added by Charles on 1-Jun-2010 for Multilingual Support
				//SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@TotalRecords",SqlDbType.Int,ParameterDirection.Output);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				//defaultCount=int.Parse(objSqlParameter.Value.ToString());
				return objDataSet.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}	
		}	
	

		public static DataTable GetLobForCoverage(int Cov_Id)
		{
			string strProcedure = "Proc_GetLobForCoverage";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@Cov_Id",Cov_Id);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				return objDataSet.Tables[0];
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}	
		}	

		
		#region AUTO GENERATE ORDER

			public string GetNewOrderNo()
			{
				try
				{
					DataSet dsTemp = new DataSet();
			
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
					dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetNewOrderNo");
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
		public string GetNewOrderNo(int stateid, string lobid)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				objDataWrapper.AddParameter("@STATE_Id",stateid);
				objDataWrapper.AddParameter("@LOB_Id",lobid);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetNewOrderNo");
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
		#endregion

	public static int GetEndorsementId(int Cov_Id)
	{ 
		int endors_id=0;

		string strProcedure = "Proc_GetEndorsementId";
		DataSet objDataSet = new DataSet();
		DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
		try
		{
			objDataWrapper.AddParameter("@Cov_Id",Cov_Id);
			objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
			if(objDataSet!=null && objDataSet.Tables.Count>0 && objDataSet.Tables[0].Rows.Count>0)
			{
				endors_id=int.Parse(objDataSet.Tables[0].Rows[0]["ENDORSMENT_ID"].ToString());
			}
			return endors_id;
		}
		catch(Exception exc)
		{
			throw (exc);
		}
		finally
		{
			objDataWrapper.Dispose();
		}
	}	
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Cov_Id"></param>
		/// <param name="Count"></param>
		/// <returns></returns>
		public static int CheckCoverageUsed(int Cov_Id,out int Count)
		{
			string strProcedure = "Proc_CheckCoverageUsed";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@Cov_Id",Cov_Id);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@COUNT",SqlDbType.Int,ParameterDirection.Output);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				Count=int.Parse(objSqlParameter.Value.ToString());
				return Count;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Cov_Id"></param>
		/// <param name="Count"></param>
		/// <returns></returns>
		public static int CheckUniqueCovInState(string covCode,int state_Id,int lob_Id,out int covCount)
		{
			return CheckUniqueCovInState(covCode, state_Id, lob_Id,out covCount, "");
		}
		public static int CheckUniqueCovInState(string covCode,int state_Id,int lob_Id,out int covCount,string strCalledFrom)
		{
			string strProcedure = "Proc_CheckUniqueCovInState";
			DataSet objDataSet = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{			
				objDataWrapper.AddParameter("@COV_CODE",covCode);
				objDataWrapper.AddParameter("@STATE_ID",state_Id);
				objDataWrapper.AddParameter("@lOB_ID",lob_Id);
				objDataWrapper.AddParameter("@CALLED_FROM",strCalledFrom);

				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@covCount",SqlDbType.Int,ParameterDirection.Output);
				objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
				covCount=int.Parse(objSqlParameter.Value.ToString());
				return covCount;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}	


	}
}

		

		
	


