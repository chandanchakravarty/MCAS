/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  12/1/2006
<End Date				: -	
<Description			: -   BL for EFT Processing 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.DataLayer;
using Cms.Model.Account;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Collections;

namespace Cms.BusinessLayer.BlAccount
{
	
	public class EFTSpoolInfo
	{
		private int mSpoolID;
		private int mRefCheckID;
		private int mRefDepositID;
		private int mRefDepositDetailID;
		private string  mProcessed;
		private string mErrorDescription;
        private DateTime mProcessedDatetime;
		private int mBRICSTranType;
		public EFTSpoolInfo()
		{
			mSpoolID =-1;
			mRefCheckID = -1;
			mRefDepositID= -1;
			mRefDepositDetailID = -1;
			mProcessed = "";
			mErrorDescription = "";
		}
			

		#region Public Properties

		public int BRICSTransactionType
		{
			get
			{
				return mBRICSTranType ; 
			}
			set
			{
				mBRICSTranType = value;
			}
		}
		public int SPOOL_ID
		{
			get 
			{
				return mSpoolID;
			}
			set
			{
				mSpoolID = value;
			}
		}
		public int REF_CHECK_ID
		{
			get
			{
				return mRefCheckID;
			}
			set
			{
				mRefCheckID = value;
			}
		}
		public int REF_DEPOSIT_ID
		{
			get
			{
				return mRefDepositID;
			}
			set 
			{
				mRefDepositID=value;
			}
		}
		public int REF_DEPOSIT_DETAIL_ID 
		{
			get
			{
				return mRefDepositDetailID;
			}
			set 
			{
				mRefDepositDetailID=value;
			}
		}
		public string PROCESSED
		{
			get
			{
				return mProcessed;
			}
			set 
			{
				mProcessed = value;
			}
		}
		public DateTime PROCESSED_DATETIME
		{
			get
			{
				return mProcessedDatetime;
			}
			set
			{
				mProcessedDatetime = value;
			}
		}
		public string ERROR_DESCRIPTION
		{
			get
			{
				return mErrorDescription ;
			}
			set 
			{
				mErrorDescription = value;
			}
		}
		#endregion


	}

	public enum PaymentModes 
	{
		Check = 11975,
		EFT   = 11976 ,
		AlreadyProcesed =11977,
		CreditCard = 11974
	}

	public enum EFTCodes
	{

		CheckingAccount =100,
		SavingAccount	=101,
		DebitEntry		=11,
		CreditEntry		=12, 
		PrenoteEntry	=22
	}

	public enum EFTErrors
	{
		ValidTransitNumber = 100,
		StartsWithFive_Error = 101,
		InvalidCheckDigit_Error = 102,
		NumberOfDigits_Error  =103 
	}
	
	public enum BRICSTransactionType
	{
		RegularCommission = 101,
		AdditionalCommission =102,
		CustomerPaymentFromAgency = 103, 
		InsuredPremium		= 104,
		VendorEFT			= 105
	}

	/// <summary>
	/// Summary description for ClsEFT.
	/// </summary>
	public class ClsEFT : Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private			bool		boolTransactionLog;
		private DataWrapper objWrapper ;
		private string strCOMPANY_CODE;
		private int FiscalID ;  // Will be fetched Based on Date
		private static char FileIDModifier;
		private static int FileNumber;
		private string strDate,strTime;
		private string strCOMPANY_NAME;
		private string strBANK_TRANSIT_NO;
		private string strCOMPANY_ID;
		private string strBANK_NAME;
		private int		BankAccountID;
		private ArrayList arrSpoolRecords;
		private int LineItemCount;
		private int NextBatchNumber ; 
		private int iUserID;
		private int iDepositID;
		private string strDEPOSIT_NO;
		private const string DepositNote = "Deposit created from EFT process";
		private const string CreatedFrom = "EFT";
		private bool mSupressFTP ; 

		private string mHostName,mUserName,mPassWord,mFTPDirectory,mLocalDirectory;


		#region Public Properties


		public bool SupressFTP
		{
			set
			{
				mSupressFTP	=	value;
			}
			get
			{
				return mSupressFTP;
			}
		}

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
		public string HostName
		{
			get
			{
				return mHostName;
			}
			set
			{
				mHostName = value;
			}
		}
		public string UserName 
		{
			get
			{
				return mUserName ;
			}
			set 
			{
				mUserName = value;
			}
		}
		public string Password
		{
			get 
			{
				return mPassWord;
			}
			set
			{
				mPassWord = value;
			}
		}
		public string FTPDirectoty
		{
			get 
			{
				return mFTPDirectory;
			}
			set 
			{
				mFTPDirectory = value;
			}
		}
		public string LocalDirectoty
		{
			get 
			{
				return mLocalDirectory;
			}
			set 
			{
				mLocalDirectory = value;
			}
		}
		#endregion

		
		public ClsEFT()
		{
			boolTransactionLog	= base.TransactionLogRequired;			
			arrSpoolRecords = new ArrayList();
			LineItemCount = 0;
			mSupressFTP = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="StartedBy">User ID under whose credentials EFT will be processed</param>
		/// <param name="COMPANY_CODE">Carrrier System ID</param>
		public ClsEFT(int StartedBy,string COMPANY_CODE)
		{
			objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES,"EFT Sweep", DataWrapper.SetAutoCommit.OFF);	

			boolTransactionLog	= base.TransactionLogRequired;
			arrSpoolRecords = new ArrayList();
			mSupressFTP = true;

			LineItemCount = 0;
			
			iUserID = StartedBy;

			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@FOR_DATE",DateTime.Now, SqlDbType.DateTime);
			SqlParameter objParam = (SqlParameter) objWrapper.AddParameter 
				("@FISCAL_ID", null,SqlDbType.Int,ParameterDirection.Output);	

			objWrapper.ExecuteNonQuery("Proc_GetFiscalIDForCurrentDate");

			FiscalID = Convert.ToInt32(objParam.Value);
			objWrapper.ClearParameteres();

			FileIDModifier = 'A';
			FileNumber =1;
			strCOMPANY_CODE = COMPANY_CODE ; //"W001";

			strDate = DateTime.Now.Year.ToString().Substring(2) 
				+ DateTime.Now.Month.ToString().PadLeft(2,'0') 
				+ DateTime.Now.Day.ToString().PadLeft(2,'0');

			DataSet dsCompany = null;
			try
			{
				dsCompany = GetCompanyDetails();
			}
			catch(Exception ex)
			{
				throw new Exception("Error while fetching Company Details : " + ex.Message ,ex);
			}
			if(dsCompany != null && dsCompany.Tables.Count > 0 && dsCompany.Tables[0].Rows.Count > 0 )
			{
				try
				{
					strCOMPANY_NAME = Convert.ToString(dsCompany.Tables[0].Rows[0]["COMPANY_NAME"]);
				}
				catch(Exception ex)
				{
					throw new Exception("Company Name not found or incorrect data : " + ex.Message ,ex);
				}
				
				try
				{
					strBANK_NAME = Convert.ToString(dsCompany.Tables[0].Rows[0]["BANK_NAME"]);
					if(strBANK_NAME.Trim() == "")
					{
						throw new Exception("Bank Name not found or incorrect data, please review Bank Information tab for EFT Account ");
					}
				}
				catch(Exception ex)
				{
					throw new Exception("Bank Name not found or incorrect data, please review Bank Information tab for EFT Account : " + ex.Message ,ex);
				}
				try
				{
					strBANK_TRANSIT_NO = Convert.ToString(dsCompany.Tables[0].Rows[0]["TRANSIT_ROUTING_NUMBER"]);
					if(strBANK_TRANSIT_NO.Trim() == "")
					{
						throw new Exception("Transit Routing Number not found or incorrect data, please review Bank Information tab for EFT Account ");
					}
				}
				catch(Exception ex)
				{
					throw new Exception("Transit Routing Number not found or incorrect data, please review Bank Information tab for EFT Account : " + ex.Message ,ex);
				}
				try
				{
					strCOMPANY_ID = Convert.ToString(dsCompany.Tables[0].Rows[0]["COMPANY_ID"]);
					if(strCOMPANY_ID.Trim() == "")
					{
						throw new Exception("Company ID not found or incorrect data, please review Bank Information tab for EFT Account ");
					}
				}
				catch(Exception ex)
				{
					throw new Exception("Company ID not found or incorrect data, please review Bank Information tab for EFT Account : " + ex.Message ,ex);
				}
				try
				{
					BankAccountID = Convert.ToInt32 (dsCompany.Tables[0].Rows[0]["BANK_ACCOUNT"]);
				}
				catch(Exception ex)
				{
					throw new Exception("Default Bank Account not found or incorrect data, make sure default account for EFT is specified at Posting Interface : " + ex.Message ,ex);
				}
			}
			NextBatchNumber = Convert.ToInt32(dsCompany.Tables[0].Rows[0]["NEXT_EFT_BATCH"]);
			dsCompany = null;
		}

		public void NextFileID()
		{
			FileIDModifier++;
			FileNumber++;
		}

		private DataSet  GetCompanyDetails()
		{
			string	strStoredProc =	"Proc_GetCompanyDetailsForEFT";
			objWrapper.AddParameter("@COMPANY_CODE", strCOMPANY_CODE);
			DataSet dsRec = objWrapper.ExecuteDataSet(strStoredProc);
            objWrapper.ClearParameteres();
			return dsRec;
		}

		//EFT Related Methods
        
		private DataSet FetchRecordsToEFT()
		{

			//Fetch from EOD_EFT_SPOOL all records on which EFT is Pending
			// Fetch EFT related details for each record also,
			//There will be three data sets 
			// for customer
			// for agency
			// for vendors

			string	strStoredProc =	"Proc_EOD_GetRecordsForEFT";
			DataSet dsResult = objWrapper.ExecuteDataSet(strStoredProc);
			objWrapper.ClearParameteres();
			return dsResult;
		}
		private void UpdateSpoolStatus()
		{
			for(int counter = 0; counter< arrSpoolRecords.Count; counter++)
			{
				EFTSpoolInfo objSpoolInfo = (EFTSpoolInfo)(arrSpoolRecords[counter]);
				if(objSpoolInfo.SPOOL_ID == -1)
					continue;

				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@SPOOL_ID",objSpoolInfo.SPOOL_ID);
				objWrapper.AddParameter("@BATCH_NUMBER",NextBatchNumber);
				if(objSpoolInfo.REF_CHECK_ID != -1)
				{
					objWrapper.AddParameter("@REF_CHECK_ID",objSpoolInfo.REF_CHECK_ID);
				}
				if(objSpoolInfo.REF_DEPOSIT_ID != -1)
				{
					objWrapper.AddParameter("@REF_DEPOSIT_ID",objSpoolInfo.REF_DEPOSIT_ID);
				}
				if(objSpoolInfo.REF_DEPOSIT_DETAIL_ID != -1)
				{
					objWrapper.AddParameter("@REF_DEP_DETAIL_ID", objSpoolInfo.REF_DEPOSIT_DETAIL_ID);
				}
				if(objSpoolInfo.PROCESSED != "")
				{
					objWrapper.AddParameter("@PROCESSED",objSpoolInfo.PROCESSED);
				}
				if(objSpoolInfo.ERROR_DESCRIPTION != "")
				{
					objWrapper.AddParameter("@ERROR_DESCRIPTION",objSpoolInfo.ERROR_DESCRIPTION);
				}
				objWrapper.ExecuteNonQuery("Proc_UpdateEFTSpool");
			}

		}

		public void Start()
		{

			try
			{
				DataSet dsRecordsToEFT = FetchRecordsToEFT();
				bool CreateDeposit = false;
				int Receipt_Mode = 0;
				ClsDeposit objDeposit = null;
				ClsDepositInfo objDepositInfo = null;
				if(dsRecordsToEFT != null && dsRecordsToEFT.Tables.Count>0)
				{
					if(dsRecordsToEFT.Tables[3] != null && dsRecordsToEFT.Tables[3].Rows.Count >0
						&& Convert.ToInt32(dsRecordsToEFT.Tables[3].Rows[0]["CREATE_DEPOSIT"]) != 0 )
					{
						CreateDeposit = true;
						//Create a New Deposit with mode EFT
						objDeposit = new ClsDeposit();
						objDepositInfo = new ClsDepositInfo();
						//Populate Deposit Model object
						objDepositInfo.ACCOUNT_ID = BankAccountID;  
						objDepositInfo.DEPOSIT_NOTE =DepositNote;
						objDepositInfo.DEPOSIT_TRAN_DATE = DateTime.Now;
						objDepositInfo.RECEIPT_MODE = (int)PaymentModes.EFT;
						Receipt_Mode = objDepositInfo.RECEIPT_MODE;
						objDepositInfo.TOTAL_DEPOSIT_AMOUNT = 0;
						if (IsEODProcess)
							objDepositInfo.CREATED_BY = EODUserID;
						else
 							objDepositInfo.CREATED_BY = iUserID;
						objDepositInfo.CREATED_DATETIME = DateTime.Now;
						strDEPOSIT_NO = Cms.BusinessLayer.BlCommon.Accounting.ClsGlAccounts.GetDepositNumberByAccountID(FiscalID,BankAccountID);
						objDepositInfo.DEPOSIT_NUMBER = Convert.ToInt32(strDEPOSIT_NO);
			
						objDeposit.Add(objDepositInfo,objWrapper);
						iDepositID = objDepositInfo.DEPOSIT_ID;

						objWrapper.ClearParameteres();
					}
					if(dsRecordsToEFT.Tables[0] != null && dsRecordsToEFT.Tables[0].Rows.Count >0)
					{
						DoEFTForCustomers(dsRecordsToEFT.Tables[0]);
						NextFileID();
					}
					if(dsRecordsToEFT.Tables[1] != null && dsRecordsToEFT.Tables[1].Rows.Count >0)
					{
						DoEFTForAgency(dsRecordsToEFT.Tables[1]);
						NextFileID();
					}
					if(dsRecordsToEFT.Tables[2] != null && dsRecordsToEFT.Tables[2].Rows.Count >0)
					{
						DoEFTForVendors(dsRecordsToEFT.Tables[2]);
						NextFileID();
					}

					if(CreateDeposit)
					{
						if(LineItemCount > 0)
						{
							objWrapper.ClearParameteres();
							objDeposit.Commit(iDepositID,iUserID,objWrapper,Receipt_Mode);
							objWrapper.ClearParameteres();
						}
							//if no line item created delete the deposit group
						else
						{
							objWrapper.ClearParameteres();
							objDeposit.Delete(iDepositID,objDepositInfo,iUserID,objWrapper);
							objWrapper.ClearParameteres();
						}
					}
					
					UpdateSpoolStatus();
				}
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{

				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		private void PopulateFileHeader(ClsNACHAFileHeader objFileHeader)
		{

			objFileHeader.ImmediateDestination = strBANK_TRANSIT_NO.ToCharArray();
			objFileHeader.ImmediateOrigin = strCOMPANY_ID.ToCharArray() ;
					
			
			
			strTime = DateTime.Now.Hour.ToString().PadLeft(2,'0')
				+ DateTime.Now.Minute.ToString().PadLeft(2,'0');

			objFileHeader.FileCreationDate = strDate.ToCharArray();
			objFileHeader.FileCreationTime = strTime.ToCharArray() ;

			//File ID Modifier 
			objFileHeader.FileIDModifier= FileIDModifier;
			
			objFileHeader.ImmediateDestinationName= strBANK_NAME.ToCharArray();
			objFileHeader.ImmediateOriginName =strCOMPANY_NAME.ToCharArray();

			// Refrence Code Will be File Creation Date + Running Number
			objFileHeader.RefrenceCode = (strDate + FileNumber.ToString()).ToCharArray();

		}
		

		private void DoEFTForAgency(DataTable  dtRecordsForEFT)
		{

			decimal dblTotalDebit = 0 , dblTotalCredit = 0 ;
			int iEntityID ,iPreviousEntityID,iSoolRowID;
			string strENTITY_NAME,strEFT_RECORD_ID,strREF_NO,strENTRY_DESC, strRECORD_TYPE;
			
			ClsDepositDetails objDepositDetail = new ClsDepositDetails();

			//Create a NACHA File 
			ClsNACHAFileHeader objFileHeader= new ClsNACHAFileHeader();
			PopulateFileHeader(objFileHeader);
			ClsNACHARecord  objNACHARecord = new ClsNACHARecord(objFileHeader);

			int iNoOfRecords = dtRecordsForEFT.Rows.Count ;
			decimal dblAmount;
			string strTransactionType,strAccountType,strTransactionCode,
				strAccTranRequired,strDFI_NO,strTRANSIT_NO;

			bool AllFailed = true;
			bool ChangeBatch = true;
			
			strTransactionCode = "";
			strRECORD_TYPE = "";
			strREF_NO = "";
			iPreviousEntityID = 0;
			// For each record Add a NACHA batch / detail
			for(int i=0; i<iNoOfRecords ; i++)
			{
				//Create An instance of EFTSpoolINfo (Will be used for DB updation
				EFTSpoolInfo objSpoolInfo = new EFTSpoolInfo();
				objSpoolInfo.SPOOL_ID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["SPOOL_RECORD_ID"]);
				iSoolRowID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["SPOOL_RECORD_ID"]);
				iEntityID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["ENTITY_ID"]);

				objSpoolInfo.BRICSTransactionType = Convert.ToInt32(dtRecordsForEFT.Rows[i]["BRICS_TRAN_TYPE"]);

				dblAmount = Convert.ToDecimal(dtRecordsForEFT.Rows[i]["TRANSACTION_AMOUNT"]);
				strTransactionType = dtRecordsForEFT.Rows[i]["TRANSACTION_CODE"].ToString();
				//Added by Ravindra(08-09-2007)
				if(dtRecordsForEFT.Rows[i]["ACCOUNT_TYPE"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Type of Account is either not specified or incorrect for agency. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				if(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "DFI Account is either not specified or incorrect for agency. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				if(dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Transit Routing Number is either not specified or incorrect agency. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				strAccountType	= dtRecordsForEFT.Rows[i]["ACCOUNT_TYPE"].ToString();
				
				if(strAccountType == Convert.ToString((int)EFTCodes.CheckingAccount))
				{
					if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
					{
						strTransactionCode = TransactionCodes.CreditToCheckingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.DebitEntry ))
					{
						strTransactionCode = TransactionCodes.DebitToCheckingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.PrenoteEntry))
					{
						strTransactionCode = TransactionCodes.DebitPrenoteToCheckingAccount; 
					}
				}

				if(strAccountType == Convert.ToString((int)EFTCodes.SavingAccount))
				{
					if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
					{
						strTransactionCode = TransactionCodes.CreditToSavingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.DebitEntry ))
					{
						strTransactionCode = TransactionCodes.DebitToSavingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.PrenoteEntry ))
					{
						strTransactionCode = TransactionCodes.DebitPrenoteToSavingAccount; 
					}
				}

				strAccTranRequired	= dtRecordsForEFT.Rows[i]["ACC_TRAN_REQUIRED"].ToString();
				
				strENTITY_NAME= dtRecordsForEFT.Rows[i]["ENTITY_NAME"].ToString();
				strEFT_RECORD_ID= dtRecordsForEFT.Rows[i]["SPOOL_RECORD_ID"].ToString();
				
				if(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"] != DBNull.Value 
					&& dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"] != DBNull.Value)
				{
					strDFI_NO	=BusinessLayer.BlCommon.ClsCommon.DecryptString(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"].ToString());
					strTRANSIT_NO	= BusinessLayer.BlCommon.ClsCommon.DecryptString(dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"].ToString()) ;
				}
				else
				{
					//EFT Details for selected Agency are not there in DataBase
					//Mark status as error with proper error descriptor
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "EFT Information not found";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				
				if(strTRANSIT_NO.Length != 9)
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Invalid Transit/Routing Number length is : " + strTRANSIT_NO .Length.ToString();
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				

				
				if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
				{
					dblTotalCredit  = dblTotalCredit + dblAmount;
				}
				else 
				{
					dblTotalDebit      = dblTotalDebit + dblAmount;
				}

				if(strAccTranRequired == "Y")
				{
					objSpoolInfo.REF_DEPOSIT_ID = iDepositID;
					objSpoolInfo.REF_DEPOSIT_DETAIL_ID = ProcessDBDeposits(iSoolRowID);
				}
				
				strENTRY_DESC = "";
				if(strTransactionType ==Convert.ToString((int)(EFTCodes.DebitEntry)))
				{
					
					strRECORD_TYPE = "DEP";
					if(objSpoolInfo.BRICSTransactionType == (int) BRICSTransactionType.CustomerPaymentFromAgency )
					{
						strENTRY_DESC = "CUST PYMTS";
					}
					else if(objSpoolInfo.BRICSTransactionType == (int) BRICSTransactionType.AdditionalCommission  )
					{
						strENTRY_DESC = "ADDTL COMM";
					}
					else if(objSpoolInfo.BRICSTransactionType == (int) BRICSTransactionType.RegularCommission   )
					{
						strENTRY_DESC = "REGLR COMM";
					}
					
				}
				else
				{
					strRECORD_TYPE = "CHK";
					if(objSpoolInfo.BRICSTransactionType == (int) BRICSTransactionType.CustomerPaymentFromAgency )
					{
						strENTRY_DESC = "CUST PYMTS";
					}
					else if(objSpoolInfo.BRICSTransactionType == (int) BRICSTransactionType.AdditionalCommission  )
					{
						strENTRY_DESC = "ADDTL COMM";
					}
					else if(objSpoolInfo.BRICSTransactionType == (int) BRICSTransactionType.RegularCommission   )
					{
						strENTRY_DESC = "REGLR COMM";
					}
				}

				if(strTransactionType ==Convert.ToString((int)(EFTCodes.PrenoteEntry)))
				{
					strENTRY_DESC = "PRENOTE   ";
				}
			

				//Ravindra(08-05-2008): WMIC wants one Batch Per Transaction
				// Create Batch Header If New Entity
				//if(iPreviousEntityID!= iEntityID)
				//{
				 if(ChangeBatch)
				 {
					//Close Previous Batch if this is not First Batch
					if(objNACHARecord.Batches.HasOpenBatch)
					{
						objNACHARecord.Batches.CloseBatch();
					}
					ClsNACHABatchHeader objBatchHeader = new ClsNACHABatchHeader();
					//Populate Batch Header
					objBatchHeader.ServiceClassCode = ServiceClassCodes.ACHMixedCreditDebit.ToCharArray();
					objBatchHeader.CompanyName = strCOMPANY_NAME.ToCharArray();  
					
					//objBatchHeader.CompanyDiscretionaryData = "TO BE IMPLEMENTED".ToCharArray();
					
					StringBuilder sbDiscData  = new StringBuilder();
					sbDiscData.Append(strRECORD_TYPE);
					sbDiscData.Append(strREF_NO);
					sbDiscData.Append("A");
					sbDiscData.Append(iEntityID);
					
					objBatchHeader.CompanyDiscretionaryData = sbDiscData.ToString().ToCharArray();

					objBatchHeader.StandardEntryClassCode = EntryClassCode.CashConcentrationOrDisbursement.ToCharArray();

					objBatchHeader.CompanyID = strCOMPANY_ID.ToCharArray();
					objBatchHeader.EntryDescription = strENTRY_DESC.ToCharArray();
					objBatchHeader.DescriptiveDate = strDate.ToCharArray();
					objBatchHeader.EffectiveEntryDate = strDate.ToCharArray();
					objBatchHeader.OriginatingDFIID = strBANK_TRANSIT_NO.Substring(1,8).ToCharArray();
					objBatchHeader.BatchNumber =  iEntityID.ToString().ToCharArray();
					objNACHARecord.Batches.AddBatch(objBatchHeader);
					 if(objSpoolInfo.BRICSTransactionType == (int) BRICSTransactionType.CustomerPaymentFromAgency)
					 {
						 ChangeBatch = false;
					 }
				}
				iPreviousEntityID= iEntityID;

				//Add Detail Record
				ClsNACHADetailInfo objNACHADetail = new ClsNACHADetailInfo();

				objNACHADetail.TransactionCode = strTransactionCode.ToCharArray();
				objNACHADetail.RecievingDFIID = strTRANSIT_NO.ToCharArray();
				objNACHADetail.DFIAccountNumber = strDFI_NO.ToCharArray();
				objNACHADetail.TransactionAmount = dblAmount;
				objNACHADetail.RecieversID = iEntityID.ToString().ToCharArray();
				objNACHADetail.RecieversName = strENTITY_NAME.ToCharArray();
				//objNACHADetail.TraceNumber = strEFT_RECORD_ID.ToCharArray();
				objNACHADetail.TraceNumberToSet = strEFT_RECORD_ID;
				objNACHARecord.Batches.CurrentBatch.AddDetail(objNACHADetail);
				objNACHARecord.EntryAddendaCount++;

				objSpoolInfo.PROCESSED="Y";
				arrSpoolRecords.Add(objSpoolInfo);
				
				//At least one detail record added to Nacha Batch 
				AllFailed = false;
				
			}//End Iterating through EFT records 

			if(!AllFailed) 
			{
				//Close last open Batch 
				if(objNACHARecord.Batches.HasOpenBatch)
				{
					objNACHARecord.Batches.CloseBatch();
				}
				objNACHARecord.CloseFile();
			
				//Create & FTP file
				objNACHARecord.HostName = mHostName;
				objNACHARecord.UserName = mUserName;
				objNACHARecord.Password = mPassWord;
				objNACHARecord.FTPDirectoty = mFTPDirectory;
				objNACHARecord.LocalDirectoty = mLocalDirectory;

				StringBuilder sbFileName = new StringBuilder();
				sbFileName.Append( strDate);
				sbFileName.Append(" - ");
				sbFileName.Append(strTime);
				sbFileName.Append(" - ");
				sbFileName.Append(FileNumber.ToString());
				sbFileName.Append(".ACH");
			
				string strFileName =sbFileName.ToString() ;
				objNACHARecord.FTPNachaFile(strFileName,mSupressFTP);

					
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@ENTITY_TYPE","AGN");
				objWrapper.AddParameter("@NACHA_FILE_NAME",strFileName);
				objWrapper.AddParameter("@TOTAL_DEBIT",dblTotalDebit);
				objWrapper.AddParameter("@TOTAL_CREDIT",dblTotalCredit);
				objWrapper.AddParameter("@BATCH_NUMBER",NextBatchNumber);
				objWrapper.ExecuteNonQuery("Proc_AddNACHAHistory");
				objWrapper.ClearParameteres();
			}
		}

		private int ProcessDBDeposits(int iEFTSpoolID)
		{
			objWrapper.ClearParameteres ();
			string	strStoredProc =	"Proc_EOD_GetAgencyDBEFTDetails";
			objWrapper.AddParameter("@EFT_SPOOL_ID", iEFTSpoolID);
			DataSet dsRec = objWrapper.ExecuteDataSet(strStoredProc);
			objWrapper.ClearParameteres();
			int iDepositLineItemID =  0 ;

			if(dsRec.Tables.Count > 0)
			{
				if(dsRec.Tables[0].Rows.Count >0)
				{
					ClsDepositDetails objDepositDetail = new ClsDepositDetails();
					
					ClsDepositDetailsInfo objDetailInfo = new ClsDepositDetailsInfo();

					objDetailInfo.DEPOSIT_ID  = iDepositID;
					objDetailInfo.DEPOSIT_TYPE = "CUST";
					objDetailInfo.PAYOR_TYPE ="CUST";
					objDetailInfo.CUSTOMER_ID = Convert.ToInt32(dsRec.Tables[0].Rows[0]["CUSTOMER_ID"]);
					objDetailInfo.RECEIPT_FROM_ID = Convert.ToInt32(dsRec.Tables[0].Rows[0]["CUSTOMER_ID"]);
					objDetailInfo.POLICY_ID = Convert.ToInt32(dsRec.Tables[0].Rows[0]["POLICY_ID"]);
					objDetailInfo.POLICY_VERSION_ID = Convert.ToInt32(dsRec.Tables[0].Rows[0]["POLICY_VERSION_ID"]);
					objDetailInfo.POLICY_NO = dsRec.Tables[0].Rows[0]["POLICY_NUMBER"].ToString ();;
					objDetailInfo.RECEIPT_AMOUNT =Convert.ToDouble( dsRec.Tables[0].Rows[0]["AMOUNT"]); 
					objDetailInfo.CREATED_DATETIME = DateTime.Now;
					objDepositDetail.AddDepositDetail(objWrapper,objDetailInfo,CreatedFrom);
					//Need to Fetch line item id of created detail
					iDepositLineItemID = objDetailInfo.CD_LINE_ITEM_ID;
					LineItemCount++;
					objWrapper.ClearParameteres();
				}
			}
				
			return iDepositLineItemID ; 
		}
		
		private void DoEFTForCustomers(DataTable  dtRecordsForEFT)
		{

			decimal dblTotalDebit = 0 , dblTotalCredit = 0 ;

			int iCustomerID , iPolicyID , iPolicyVersionID,
				iDepositLineItemID , iPreviousCustomerID;
			string strENTITY_NAME,strEFT_RECORD_ID, strRECORD_TYPE;
			bool AllFailed = true;
			bool ChangeBatch = true;
			ClsDepositDetails objDepositDetail = new ClsDepositDetails();

			iPreviousCustomerID = 0;

			//Create a NACHA File 
			ClsNACHAFileHeader objFileHeader= new ClsNACHAFileHeader();
			PopulateFileHeader(objFileHeader);
			ClsNACHARecord  objNACHARecord = new ClsNACHARecord(objFileHeader);

			int iNoOfRecords = dtRecordsForEFT.Rows.Count ;
			decimal dblAmount;
			string strPolicyNumber,strTransactionType,strAccountType,strTransactionCode,
					strAccTranRequired,strDFI_NO,strTRANSIT_NO;
			
			strTransactionCode = "";
			strRECORD_TYPE = "";
			// For each record Add a deposit line item and a NACHA batch / detail
			for(int i=0; i<iNoOfRecords ; i++)
			{
				
				//Create An instance of EFTSpoolINfo (Will be used for DB updation
				EFTSpoolInfo objSpoolInfo = new EFTSpoolInfo();
				objSpoolInfo.SPOOL_ID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["SPOOL_RECORD_ID"]);
				iCustomerID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["ENTITY_ID"]);
				iPolicyID	= Convert.ToInt32(dtRecordsForEFT.Rows[i]["POLICY_ID"]);
				iPolicyVersionID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["POLICY_VERSION_ID"]);
				strPolicyNumber	 = dtRecordsForEFT.Rows[i]["POLICY_NUMBER"].ToString();
				dblAmount = Convert.ToDecimal(dtRecordsForEFT.Rows[i]["TRANSACTION_AMOUNT"]);
				strTransactionType = dtRecordsForEFT.Rows[i]["TRANSACTION_CODE"].ToString();
				//Added by Ravindra(08-09-2007)
				if(dtRecordsForEFT.Rows[i]["ACCOUNT_TYPE"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Type of Account is either not specified or incorrect for policy. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				if(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "DFI Account is either not specified or incorrect for policy. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				if(dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Transit Routing Number is either not specified or incorrect policy. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}

				strAccountType	= dtRecordsForEFT.Rows[i]["ACCOUNT_TYPE"].ToString();
			
				
				if(strAccountType == Convert.ToString((int)EFTCodes.CheckingAccount))
				{
					if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
					{
						strTransactionCode = TransactionCodes.CreditToCheckingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.DebitEntry ))
					{
						strTransactionCode = TransactionCodes.DebitToCheckingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.PrenoteEntry ))
					{
						strTransactionCode = TransactionCodes.DebitPrenoteToCheckingAccount;
					}

				}

				if(strAccountType == Convert.ToString((int)EFTCodes.SavingAccount))
				{
					if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
					{
						strTransactionCode = TransactionCodes.CreditToSavingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.DebitEntry ))
					{
						strTransactionCode = TransactionCodes.DebitToSavingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.PrenoteEntry ))
					{
						strTransactionCode = TransactionCodes.DebitPrenoteToSavingAccount ; 
					}
				}

				strAccTranRequired	= dtRecordsForEFT.Rows[i]["ACC_TRAN_REQUIRED"].ToString();

				strENTITY_NAME= dtRecordsForEFT.Rows[i]["ENTITY_NAME"].ToString();
				strEFT_RECORD_ID= dtRecordsForEFT.Rows[i]["SPOOL_RECORD_ID"].ToString();
				
				if(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"] != DBNull.Value 
					&& dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"] != DBNull.Value)
				{
					strDFI_NO	= BusinessLayer.BlCommon.ClsCommon.DecryptString(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"].ToString());
					strTRANSIT_NO	= BusinessLayer.BlCommon.ClsCommon.DecryptString(dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"].ToString());
				}
				else
				{
					//EFT Details for selected customer are not there in DataBase
					//Mark status as error with proper error descriptor
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "EFT Information not found";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				
				if(strTRANSIT_NO.Length != 9)
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Invalid Transit/Routing Number length is : " + strTRANSIT_NO .Length.ToString();
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}

				
				if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
				{
					dblTotalCredit  = dblTotalCredit + dblAmount;
				}
				else 
				{
					dblTotalDebit      = dblTotalDebit + dblAmount;
				}
				
				//If no transaction posted in books of accounts add a deposit line item or check
				if(strAccTranRequired == "Y")
				{
					if(strTransactionType ==Convert.ToString((int)(EFTCodes.DebitEntry)))
					{
						ClsDepositDetailsInfo objDetailInfo = new ClsDepositDetailsInfo();
						objDetailInfo.DEPOSIT_ID  = iDepositID;
						objDetailInfo.DEPOSIT_TYPE = "CUST";
						objDetailInfo.PAYOR_TYPE ="CUST";
						objDetailInfo.CUSTOMER_ID = iCustomerID;
						objDetailInfo.RECEIPT_FROM_ID = iCustomerID;
						objDetailInfo.POLICY_ID = iPolicyID;
						objDetailInfo.POLICY_VERSION_ID = iPolicyVersionID;
						objDetailInfo.POLICY_NO = strPolicyNumber;
						objDetailInfo.RECEIPT_AMOUNT =Convert.ToDouble( dblAmount); 
						objDetailInfo.CREATED_DATETIME = DateTime.Now;
						objDepositDetail.AddDepositDetail(objWrapper,objDetailInfo,CreatedFrom);
						//Need to Fetch line item id of created detail
						iDepositLineItemID = objDetailInfo.CD_LINE_ITEM_ID;
						objWrapper.ClearParameteres();
						strRECORD_TYPE = "DEP";
						//For Updating Database for Deposit Refrence
						objSpoolInfo.REF_DEPOSIT_ID = iDepositID;
						objSpoolInfo.REF_DEPOSIT_DETAIL_ID = iDepositLineItemID;
						LineItemCount++;
					}
					else
					{
						//as per current implementation Customer can't recieve payments through
						//EFT (in that case a cheque will be created if implemented)
						strRECORD_TYPE = "CHK";
					}
					//Update Database for Processed status & Processed datetime
					objSpoolInfo.PROCESSED="Y";
				}

				//Ravindra(08-05-2008): Only one batch will be there for Customer EFT
				
//				// Create Batch Header If New Entity
//				if(iPreviousCustomerID != iCustomerID)
//				{
//					ChangeBatch = true;
//				}
				
				if(ChangeBatch)
				{
					//Close Previous Batch if this is not First Batch
					if(objNACHARecord.Batches.HasOpenBatch)
					{
						objNACHARecord.Batches.CloseBatch();
					}
					ClsNACHABatchHeader objBatchHeader = new ClsNACHABatchHeader();
					//Populate Batch Header
					objBatchHeader.ServiceClassCode = ServiceClassCodes.ACHMixedCreditDebit.ToCharArray();
					objBatchHeader.CompanyName = strCOMPANY_NAME.ToCharArray();  
					
					StringBuilder sbDiscData  = new StringBuilder();
					sbDiscData.Append(strRECORD_TYPE);
					sbDiscData.Append(strDEPOSIT_NO);
					sbDiscData.Append("C");
					sbDiscData.Append(iCustomerID);
					
					objBatchHeader.CompanyDiscretionaryData = sbDiscData.ToString().ToCharArray();

					objBatchHeader.CompanyID = strCOMPANY_ID.ToCharArray();
					objBatchHeader.EntryDescription = "INSUR PREM".ToCharArray();
					objBatchHeader.DescriptiveDate = strDate.ToCharArray();
					objBatchHeader.EffectiveEntryDate = strDate.ToCharArray();
					objBatchHeader.OriginatingDFIID = strBANK_TRANSIT_NO.Substring(1,8).ToCharArray();
					//objBatchHeader.BatchNumber =  iCustomerID.ToString().ToCharArray();
					objNACHARecord.Batches.AddBatch(objBatchHeader);
					ChangeBatch = false;
				}
				iPreviousCustomerID = iCustomerID ;


				//Add Detail Record
				ClsNACHADetailInfo objNACHADetail = new ClsNACHADetailInfo();

				objNACHADetail.TransactionCode = strTransactionCode.ToCharArray();
				objNACHADetail.RecievingDFIID = strTRANSIT_NO.ToCharArray();
				objNACHADetail.DFIAccountNumber = strDFI_NO.ToCharArray();
				objNACHADetail.TransactionAmount = dblAmount;
				objNACHADetail.RecieversID = iCustomerID.ToString().ToCharArray();
				objNACHADetail.RecieversName = strENTITY_NAME.ToCharArray();
				//objNACHADetail.TraceNumber = strEFT_RECORD_ID.ToCharArray();
				objNACHADetail.TraceNumberToSet = strEFT_RECORD_ID;
				objNACHARecord.Batches.CurrentBatch.AddDetail(objNACHADetail);
				objNACHARecord.EntryAddendaCount++;

				AllFailed = false;
				objSpoolInfo.PROCESSED="Y";
				arrSpoolRecords.Add(objSpoolInfo);
				
				
			}//End Iterating through EFT records 

			if(!AllFailed)
			{
				//Close last open Batch 
				if(objNACHARecord.Batches.HasOpenBatch)
				{
					objNACHARecord.Batches.CloseBatch();
				}
				objNACHARecord.CloseFile();
		

				//Create & FTP file
				objNACHARecord.HostName = mHostName;
				objNACHARecord.UserName = mUserName;
				objNACHARecord.Password = mPassWord;
				objNACHARecord.FTPDirectoty = mFTPDirectory;
				objNACHARecord.LocalDirectoty = mLocalDirectory;

				StringBuilder sbFileName = new StringBuilder();
				sbFileName.Append( strDate);
				sbFileName.Append(" - ");
				sbFileName.Append(strTime);
				sbFileName.Append(" - ");
				sbFileName.Append(FileNumber.ToString());
				sbFileName.Append(".ACH");
			
				string strFileName =sbFileName.ToString() ;
				objNACHARecord.FTPNachaFile(strFileName,mSupressFTP);
			
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@ENTITY_TYPE","CUST");
				objWrapper.AddParameter("@NACHA_FILE_NAME",strFileName);
				objWrapper.AddParameter("@TOTAL_DEBIT",dblTotalDebit);
				objWrapper.AddParameter("@TOTAL_CREDIT",dblTotalCredit);
				objWrapper.AddParameter("@BATCH_NUMBER",NextBatchNumber);
				objWrapper.ExecuteNonQuery("Proc_AddNACHAHistory");
				objWrapper.ClearParameteres();
			}
			
		}



		private void DoEFTForVendors(DataTable  dtRecordsForEFT)
		{

			decimal dblTotalCredit = 0 , dblTotalDebit = 0 ;
			
			int iEntityID ,iPreviousEntityID,iSoolRowID;
			string strENTITY_NAME,strEFT_RECORD_ID,strREF_NO,strENTRY_DESC, strRECORD_TYPE;
			
			ClsDepositDetails objDepositDetail = new ClsDepositDetails();

			//Create a NACHA File 
			ClsNACHAFileHeader objFileHeader= new ClsNACHAFileHeader();
			PopulateFileHeader(objFileHeader);
			ClsNACHARecord  objNACHARecord = new ClsNACHARecord(objFileHeader);

			int iNoOfRecords = dtRecordsForEFT.Rows.Count ;
			decimal dblAmount;
			string strTransactionType,strAccountType,strTransactionCode,
				strAccTranRequired,strDFI_NO,strTRANSIT_NO;
			bool AllFailed = true;
			
			strTransactionCode = "";
			strRECORD_TYPE = "";
			strREF_NO = "";
			iPreviousEntityID = 0;
			// For each record Add a NACHA batch / detail
			for(int i=0; i<iNoOfRecords ; i++)
			{

				//Create An instance of EFTSpoolINfo (Will be used for DB updation
				EFTSpoolInfo objSpoolInfo = new EFTSpoolInfo();
				objSpoolInfo.SPOOL_ID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["SPOOL_RECORD_ID"]);				
				iSoolRowID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["SPOOL_RECORD_ID"]);
				iEntityID = Convert.ToInt32(dtRecordsForEFT.Rows[i]["ENTITY_ID"]);
				dblAmount = Convert.ToDecimal(dtRecordsForEFT.Rows[i]["TRANSACTION_AMOUNT"]);
				strTransactionType = dtRecordsForEFT.Rows[i]["TRANSACTION_CODE"].ToString();
				//Added by Ravindra(08-09-2007)
				if(dtRecordsForEFT.Rows[i]["ACCOUNT_TYPE"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Type of Account is either not specified or incorrect for vendor. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				if(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "DFI Account is either not specified or incorrect for vendor. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				if(dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"] == DBNull.Value )
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Transit Routing Number is either not specified or incorrect vendor. ";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				strAccountType	= dtRecordsForEFT.Rows[i]["ACCOUNT_TYPE"].ToString();
				
				if(strAccountType == Convert.ToString((int)EFTCodes.CheckingAccount))
				{
					if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
					{
						strTransactionCode = TransactionCodes.CreditToCheckingAccount; 
					}
					else  if(strTransactionType == Convert.ToString((int)EFTCodes.DebitEntry ))
					{
						strTransactionCode = TransactionCodes.DebitToCheckingAccount; 
					}
					else  if(strTransactionType == Convert.ToString((int)EFTCodes.PrenoteEntry))
					{
						strTransactionCode = TransactionCodes.DebitPrenoteToCheckingAccount; 
					}
				}

				if(strAccountType == Convert.ToString((int)EFTCodes.SavingAccount))
				{
					if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
					{
						strTransactionCode = TransactionCodes.CreditToSavingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.DebitEntry ))
					{
						strTransactionCode = TransactionCodes.DebitToSavingAccount; 
					}
					else if(strTransactionType == Convert.ToString((int)EFTCodes.PrenoteEntry ))
					{
						strTransactionCode = TransactionCodes.DebitPrenoteToSavingAccount; 
					}
				}

				strAccTranRequired	= dtRecordsForEFT.Rows[i]["ACC_TRAN_REQUIRED"].ToString();
				
				strENTITY_NAME= dtRecordsForEFT.Rows[i]["ENTITY_NAME"].ToString();
				strEFT_RECORD_ID= dtRecordsForEFT.Rows[i]["SPOOL_RECORD_ID"].ToString();
				
				if(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"] != DBNull.Value 
					&& dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"] != DBNull.Value)
				{
					strDFI_NO	= BusinessLayer.BlCommon.ClsCommon.DecryptString(dtRecordsForEFT.Rows[i]["DFI_ACC_NO"].ToString());
					strTRANSIT_NO	= BusinessLayer.BlCommon.ClsCommon.DecryptString(dtRecordsForEFT.Rows[i]["TRANSIT_ROUTING_NO"].ToString());
				}
				else
				{
					//EFT Details for selected Vendor are not there in DataBase
					//Mark status as error with proper error descriptor
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "EFT Information not found";
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				
				if(strTRANSIT_NO.Length != 9)
				{
					objSpoolInfo.PROCESSED="N";
					objSpoolInfo.ERROR_DESCRIPTION = "Invalid Transit/Routing Number length is : " + strTRANSIT_NO .Length.ToString();
					arrSpoolRecords.Add(objSpoolInfo);
					continue;
				}
				
						
				//For Vendor on Credit Transactions will be there 
				// i.e Payment from Carier to Vendor
				if(strTransactionType ==Convert.ToString((int)(EFTCodes.CreditEntry )))
				{
					strRECORD_TYPE = "CHK";
					//strENTRY_DESC = "VENDOR EFT";
				}
				else
				{
					strRECORD_TYPE = "";
					//strENTRY_DESC = "VENDOR EFT";
				}
			
				//Entry Description in Batch Header will always be same
				//Previously in Prenote it was setting this to blank
				strENTRY_DESC = "VENDOR EFT";

				if(strTransactionType == Convert.ToString((int)EFTCodes.CreditEntry))
				{
					dblTotalCredit  = dblTotalCredit + dblAmount;
				}
				else 
				{
					dblTotalDebit      = dblTotalDebit + dblAmount;
				}

				//Ravindra(08-05-2008): As per WMIC one batch per transaction
				// Create Batch Header If New Entity
				//if(iPreviousEntityID!= iEntityID)
				//{
					//Close Previous Batch if this is not First Batch
					if(objNACHARecord.Batches.HasOpenBatch)
					{
						objNACHARecord.Batches.CloseBatch();
					}
					ClsNACHABatchHeader objBatchHeader = new ClsNACHABatchHeader();
					//Populate Batch Header
					objBatchHeader.ServiceClassCode = ServiceClassCodes.ACHMixedCreditDebit.ToCharArray();
					objBatchHeader.CompanyName = strCOMPANY_NAME.ToCharArray();  
					
					//objBatchHeader.CompanyDiscretionaryData = "TO BE IMPLEMENTED".ToCharArray();
					
					StringBuilder sbDiscData  = new StringBuilder();
					sbDiscData.Append(strRECORD_TYPE);
					sbDiscData.Append(strREF_NO);
					sbDiscData.Append("V");
					sbDiscData.Append(iEntityID);
					
					objBatchHeader.CompanyDiscretionaryData = sbDiscData.ToString().ToCharArray();
					
					//Ravindra(08-05-2008):Transaction Type will be CCD
					objBatchHeader.StandardEntryClassCode = EntryClassCode.CashConcentrationOrDisbursement.ToCharArray() ;

					objBatchHeader.CompanyID = strCOMPANY_ID.ToCharArray();
					objBatchHeader.EntryDescription = strENTRY_DESC.ToCharArray();
					objBatchHeader.DescriptiveDate = strDate.ToCharArray();
					objBatchHeader.EffectiveEntryDate = strDate.ToCharArray();
					objBatchHeader.OriginatingDFIID = strBANK_TRANSIT_NO.Substring(1,8).ToCharArray();
					objBatchHeader.BatchNumber =  iEntityID.ToString().ToCharArray();
					objNACHARecord.Batches.AddBatch(objBatchHeader);
				//}
				iPreviousEntityID= iEntityID;

				//Add Detail Record
				ClsNACHADetailInfo objNACHADetail = new ClsNACHADetailInfo();

				objNACHADetail.TransactionCode = strTransactionCode.ToCharArray();
				objNACHADetail.RecievingDFIID = strTRANSIT_NO.ToCharArray();
				objNACHADetail.DFIAccountNumber = strDFI_NO.ToCharArray();
				objNACHADetail.TransactionAmount = dblAmount;
				objNACHADetail.RecieversID = iEntityID.ToString().ToCharArray();
				objNACHADetail.RecieversName = strENTITY_NAME.ToCharArray();
				//objNACHADetail.TraceNumber = strEFT_RECORD_ID.ToCharArray();
				objNACHADetail.TraceNumberToSet = strEFT_RECORD_ID;
				objNACHARecord.Batches.CurrentBatch.AddDetail(objNACHADetail);
				objNACHARecord.EntryAddendaCount++;

				AllFailed = false;
				objSpoolInfo.PROCESSED="Y";
				arrSpoolRecords.Add(objSpoolInfo);
				
			}//End Iterating through EFT records 

			if(!AllFailed)
			{
				//Close last open Batch 
				if(objNACHARecord.Batches.HasOpenBatch)
				{
					objNACHARecord.Batches.CloseBatch();
				}
				objNACHARecord.CloseFile();
			
				//Create & FTP file
				objNACHARecord.HostName = mHostName;
				objNACHARecord.UserName = mUserName;
				objNACHARecord.Password = mPassWord;
				objNACHARecord.FTPDirectoty = mFTPDirectory;
				objNACHARecord.LocalDirectoty = mLocalDirectory;

				StringBuilder sbFileName = new StringBuilder();
				sbFileName.Append( strDate);
				sbFileName.Append(" - ");
				sbFileName.Append(strTime);
				sbFileName.Append(" - ");
				sbFileName.Append(FileNumber.ToString());
				sbFileName.Append(".ACH");
			
				string strFileName =sbFileName.ToString() ;
				objNACHARecord.FTPNachaFile(strFileName,mSupressFTP);

					
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@ENTITY_TYPE","VEN");
				objWrapper.AddParameter("@NACHA_FILE_NAME",strFileName);
				objWrapper.AddParameter("@TOTAL_DEBIT",dblTotalDebit);
				objWrapper.AddParameter("@TOTAL_CREDIT",dblTotalCredit);
				objWrapper.AddParameter("@BATCH_NUMBER",NextBatchNumber);
				objWrapper.ExecuteNonQuery("Proc_AddNACHAHistory");
				objWrapper.ClearParameteres();
			}

		}


		
		#region Calculate EFT Number

		public static int CalculateTransitCheckDigit(string transitRoutNumber)
		{
			/*Code to calculate 9th digit based on rule*/
			int intSum = 0;
			int intPos = 0;
			int intchkDigit = 0;
			int val1=0,val2=0,val3=0,val4=0,val5=0,val6=0,val7=0,val8=0;
			if(transitRoutNumber != "")
			{
				if(transitRoutNumber.Length == 8)
				{
					for(int i=0;i<transitRoutNumber.Length;i++)
					{
						intPos = int.Parse(transitRoutNumber[i].ToString());
						if(i == 0)
							val1 = intPos * 3;
						if(i == 1)
							val2 = intPos * 7;
						if(i == 2)
							val3 = intPos * 1;
						if(i == 3)
							val4 = intPos * 3;
						if(i == 4)
							val5 = intPos * 7;
						if(i == 5)
							val6 = intPos * 1;
						if(i == 6)
							val7 = intPos * 3;
						if(i == 7)
							val8 = intPos * 7;
					}
				      
				}
				/*Add the results of each multiplication.*/
				intSum = val1 + val2 + val3 + val4 + val5 + val6 + val7 + val8;
				/*Subtract the sum from the next highest multiple of 10.  The result is the check digit*/
				intchkDigit = (((intSum/10)*10) + 10) - intSum;
				if(intchkDigit == 10)
					intchkDigit = 0;
			}
			return intchkDigit;
			
		}
		#endregion
		#region Validate EFT Number
		public static  EFTErrors ValidateTransitNumber(string transitRoutNumber)
		{
			int chkDigit = 0;
			int tmpChkDigit = 0;
			// check for Start With Five 
			if(int.Parse(transitRoutNumber[0].ToString()) == 5)
				return EFTErrors.StartsWithFive_Error;

			if(transitRoutNumber.Length == 9)
			{
				//Get Entered 9th digit by user
				chkDigit = int.Parse(transitRoutNumber[8].ToString());
				//Calculate the CheckDigit
				tmpChkDigit = CalculateTransitCheckDigit(transitRoutNumber.Substring(0,8));
				//Compare if both are equal or not
			
				if(tmpChkDigit != chkDigit)
					return EFTErrors.InvalidCheckDigit_Error;				
				else
					return EFTErrors.ValidTransitNumber;
			}
			else
			{
				return EFTErrors.NumberOfDigits_Error;
			}
			
			
		
		}
		#endregion

		#region Customer EFT Info SAVE UPDATE
		public int Save(ClsEFTInfo objEFTinfo, ClsEFTInfo objOldEFTinfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_APP_EFT_CUST_INFO";
			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				int retval = 0;
				int result = 0;
				objDataWrapper.AddParameter("@CUSTOMER_ID",objEFTinfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objEFTinfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objEFTinfo.APP_VERSION_ID);
				if(objEFTinfo.FEDERAL_ID!= "")
					{
						objDataWrapper.AddParameter("@FEDERAL_ID",objEFTinfo.FEDERAL_ID);
					}
				else 
					{
						objDataWrapper.AddParameter("@FEDERAL_ID",null);
					}

				objDataWrapper.AddParameter("@DFI_ACC_NO",objEFTinfo.DFI_ACC_NO);
				objDataWrapper.AddParameter("@TRANSIT_ROUTING_NO",objEFTinfo.TRANSIT_ROUTING_NO);
				objDataWrapper.AddParameter("@ACCOUNT_TYPE",objEFTinfo.ACCOUNT_TYPE);
				objDataWrapper.AddParameter("@CREATED_BY",objEFTinfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objEFTinfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objEFTinfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objEFTinfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@EFT_TENTATIVE_DATE",objEFTinfo.EFT_TENTATIVE_DATE);
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);

				if(TransactionLog)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					string strTranXML="";
					objEFTinfo.TransactLabel = BlCommon.ClsCommon.MapTransactionLabel("/application/aspx/InstallmentInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					if(objOldEFTinfo==null)
					{
						
						strTranXML = objBuilder.GetTransactionLogXML(objEFTinfo);
						strTranXML = ProcessTransXML(strTranXML);
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1705", "");// "Customer EFT Information Has Been Saved";
					}
					else
					{
						strTranXML = objBuilder.GetTransactionLogXML(objOldEFTinfo,objEFTinfo);
						//strTranXML = objBuilder.GetUpdateSQL(objOldEFTinfo,objEFTinfo,out strTranXML);
						strTranXML = ProcessTransXML(strTranXML);
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1706", "");// "Customer EFT Information Has Been Updated";
					}					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objEFTinfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=	objEFTinfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID			=	objEFTinfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objEFTinfo.APP_VERSION_ID;					
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query					
					result	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					result	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				retval = int.Parse(objSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();

				return retval;
				
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
		#region TRANS XML
		/// <summary>
		/// 
		/// </summary>
		/// <param name="transXML"></param>
		/// <returns></returns>
		public string ProcessTransXML(string transXML)
		{
			System.Xml.XmlDocument transDoc = new System.Xml.XmlDocument();
            
			try
			{
				transDoc.LoadXml(transXML);
				System.Xml.XmlNode mapNodeCARD_NO = transDoc.SelectSingleNode("//Map[@field='CARD_NO']");
				if(mapNodeCARD_NO!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCARD_NO);	
				
				System.Xml.XmlNode mapNodeCARD_TYPE = transDoc.SelectSingleNode("//Map[@field='CARD_TYPE']");
				if(mapNodeCARD_TYPE!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCARD_TYPE);		

				System.Xml.XmlNode mapNodeCUSTOMER_FIRST_NAME = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_FIRST_NAME']");
				if(mapNodeCUSTOMER_FIRST_NAME!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_FIRST_NAME);	

				System.Xml.XmlNode mapNodeCUSTOMER_LAST_NAME = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_LAST_NAME']");
				if(mapNodeCUSTOMER_LAST_NAME!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_LAST_NAME);

				System.Xml.XmlNode mapNodeCUSTOMER_MIDDLE_NAME = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_MIDDLE_NAME']");
				if(mapNodeCUSTOMER_MIDDLE_NAME!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_MIDDLE_NAME);	

				System.Xml.XmlNode mapNodeCUSTOMER_ADDRESS1 = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_ADDRESS1']");
				if(mapNodeCUSTOMER_ADDRESS1!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_ADDRESS1);	
			
				System.Xml.XmlNode mapNodeCUSTOMER_ADDRESS2 = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_ADDRESS2']");
				if(mapNodeCUSTOMER_ADDRESS2!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_ADDRESS2);	

				System.Xml.XmlNode mapNodeCUSTOMER_CITY = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_CITY']");
				if(mapNodeCUSTOMER_CITY!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_CITY);	

				System.Xml.XmlNode mapNodeCUSTOMER_STATE = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_STATE']");
				if(mapNodeCUSTOMER_STATE!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_STATE);	

				System.Xml.XmlNode mapNodeCUSTOMER_ZIP = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_ZIP']");
				if(mapNodeCUSTOMER_ZIP!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_ZIP);	

				System.Xml.XmlNode mapNodeCUSTOMER_COUNTRY = transDoc.SelectSingleNode("//Map[@field='CUSTOMER_COUNTRY']");
				if(mapNodeCUSTOMER_COUNTRY!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCUSTOMER_COUNTRY);	

				System.Xml.XmlNode mapNodeCARD_DATE_VALID_FROM = transDoc.SelectSingleNode("//Map[@field='CARD_DATE_VALID_FROM']");
				if(mapNodeCARD_DATE_VALID_FROM!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCARD_DATE_VALID_FROM);	

				System.Xml.XmlNode mapNodeCARD_DATE_VALID_TO = transDoc.SelectSingleNode("//Map[@field='CARD_DATE_VALID_TO']");
				if(mapNodeCARD_DATE_VALID_TO!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCARD_DATE_VALID_TO);	

				System.Xml.XmlNode mapNodeCARD_CVV_NUMBER = transDoc.SelectSingleNode("//Map[@field='CARD_CVV_NUMBER']");
				if(mapNodeCARD_CVV_NUMBER!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCARD_CVV_NUMBER);

				System.Xml.XmlNode mapNodeCARD_HOLDER_NAME = transDoc.SelectSingleNode("//Map[@field='CARD_HOLDER_NAME']");
				if(mapNodeCARD_HOLDER_NAME!=null)
					transDoc.SelectSingleNode("//LabelFieldMapping").RemoveChild(mapNodeCARD_HOLDER_NAME);

			

							
				return transDoc.OuterXml;

                
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{

			}
			
            
		}
		#endregion
		#region Get Customer EFT Info
		public DataSet GetAppEFTCustInfo(int CustomerID, int AppID, int AppVersionID)
		{
			try
			{
				string	strStoredProc =	"proc_GetACT_APP_EFT_CUST_INFO";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@APP_ID",AppID);
				objWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		#endregion
		#region Customer EFT Info SAVE UPDATE POL LEVEL
		/// <summary>
		/// Modified on 9 Sep 2009
		/// Commit Rollback Transaction
		/// </summary>
		/// <param name="objEFTinfo"></param>
		/// <param name="objOldEFTinfo"></param>
		/// <returns></returns>
		public int SavePolicy(ClsEFTInfo objEFTinfo, ClsEFTInfo objOldEFTinfo)
		{
			int retval = 0;
			string		strStoredProc	=	"Proc_InsertACT_POL_EFT_CUST_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				
				int result = 0;
				objDataWrapper.AddParameter("@CUSTOMER_ID",objEFTinfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objEFTinfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objEFTinfo.POLICY_VERSION_ID);
				if(objEFTinfo.FEDERAL_ID!= "")
				{
					objDataWrapper.AddParameter("@FEDERAL_ID",objEFTinfo.FEDERAL_ID);
				}
				else 
				{
					objDataWrapper.AddParameter("@FEDERAL_ID",null);
				}
				objDataWrapper.AddParameter("@DFI_ACC_NO",objEFTinfo.DFI_ACC_NO);
				objDataWrapper.AddParameter("@TRANSIT_ROUTING_NO",objEFTinfo.TRANSIT_ROUTING_NO);
				objDataWrapper.AddParameter("@ACCOUNT_TYPE",objEFTinfo.ACCOUNT_TYPE);
				objDataWrapper.AddParameter("@CREATED_BY",objEFTinfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objEFTinfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objEFTinfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objEFTinfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@EFT_TENTATIVE_DATE",objEFTinfo.EFT_TENTATIVE_DATE);
				objDataWrapper.AddParameter("@REVERIFIED_AC",objEFTinfo.REVERIFIED_AC); 
				SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);
				if(TransactionLog)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					string strTranXML="";
					objEFTinfo.TransactLabel = BlCommon.ClsCommon.MapTransactionLabel("/policies/aspx/InstallmentInfo.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					if(objOldEFTinfo==null)
					{
						strTranXML = objBuilder.GetTransactionLogXML(objEFTinfo);
						strTranXML = ProcessTransXML(strTranXML);
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1705", "");// "Customer EFT Information Has Been Saved";
					}
					else
					{
						strTranXML = objBuilder.GetTransactionLogXML(objOldEFTinfo,objEFTinfo);
						//strTranXML = objBuilder.GetUpdateSQL(objOldEFTinfo,objEFTinfo,out strTranXML);
						strTranXML = ProcessTransXML(strTranXML);
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1706", "");//"Customer EFT Information Has Been Updated";
					}					
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objEFTinfo.CREATED_BY;
					objTransactionInfo.CLIENT_ID		=	objEFTinfo.CUSTOMER_ID;
					objTransactionInfo.APP_ID			=	objEFTinfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objEFTinfo.APP_VERSION_ID;					
					objTransactionInfo.POLICY_ID		=	objEFTinfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objEFTinfo.POLICY_VERSION_ID;					
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query					
					result	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					result	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				retval = int.Parse(objSqlParameter.Value.ToString());

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				objDataWrapper.ClearParameteres();

				
				
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				retval = -1;
				//throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			return retval;
		
			


		}
		#endregion
		#region Get Customer EFT Info POL LEVEL
		public DataSet GetPolEFTCustInfo(int CustomerID, int PolID, int PolVersionID)
		{
			try
			{
				string	strStoredProc =	"proc_GetACT_POL_EFT_CUST_INFO";			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@POLICY_ID",PolID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",PolVersionID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);			
				if(ds!=null)
					return ds;
				else
					return null;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}
		#endregion

	}
}
