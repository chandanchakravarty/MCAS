/******************************************************************************************
<Author				: -		Ravindra Gupta
<Start Date			: -		1-16-2007
<End Date			: -		
<Description		: - 	NACHA file creation logic
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 

using System;
using System.Text;
using System.Collections;
using System.IO;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlAccount
{

	
	#region Codes Used In NACHA
	public class NACHACodes
	{
		public const int RecordSize = 94;

		public const char OriginatorStatusCode   = '1';
		public const char HaveAddendaRecord = '1';
		public const char NoAddendaRecord = '0';
		public const char   DefaultFormatCode     = '1';
		public const string DefaultPriorityCode	= "01";
		public const string	NachaRecordSize     = "094";
		public const string	DefaultBlockingFactor	= "10";
		

	}
	
	public class RecordTypeCodes
	{
		public  const char FileHeader   = '1';
		public	const char FileControl  = '9';
		public  const char BatchHeader  = '5';
		public  const char BatchControl = '8';
		public  const char DetailInfo   = '6';
		
	}

	public class EntryClassCode
	{
		public const string AccountsReceivableEntry = "ARC";
		public const string CoorporateCrossBorderPayment = "CBR";
		public const string CashConcentrationOrDisbursement= "CCD";
		public const string CorporateTradeExchange = "CTX";
		public const string ConsumerCrossBorderPayment = "PBR";
		public const string PointOfPurchaseEntry = "POP";

		public const string PrearrangedPaymentAndDeposit = "PPD";
		public const string RePresentedCheckEntry = "RCK";
		public const string TelephoneInitiatedEntry = "TEL";
		public const string InternetInitiatedEntry = "WEB";




	}
	public class TransactionCodes
	{

		public const string CreditToCheckingAccount = "22";
		public const string CreditPrenoteToCheckingAccount = "23";
		public const string DebitToCheckingAccount = "27";
		public const string DebitPrenoteToCheckingAccount = "28";

		public const string CreditToSavingAccount = "32";
		public const string CreditPrenoteToSavingAccount = "33";
		public const string DebitToSavingAccount = "37";
		public const string DebitPrenoteToSavingAccount = "38";

		public static bool IsCreditEntry(string TranCode)
		{

			if(TranCode == CreditPrenoteToCheckingAccount ||
				TranCode == CreditPrenoteToSavingAccount ||
				TranCode == CreditToCheckingAccount ||
				TranCode == CreditToSavingAccount )
					return true;

			return false;
		}
		
		public static bool IsDebitEntry(string TranCode)
		{

			if(TranCode == DebitPrenoteToCheckingAccount ||
				TranCode == DebitPrenoteToSavingAccount ||
				TranCode == DebitToCheckingAccount ||
				TranCode == DebitToSavingAccount  )
					return true;

			return false;
		}

	}
	public class ServiceClassCodes
	{
		public const string ACHMixedCreditDebit		= "200";
		public const string ACHMixedCreditOnly		= "220";
		public const string ACHMixedDebitOnly		= "225";
	}
	#endregion 
	
	
	#region Base Class For NACHA Model
	public class ClsNACHABase
	{

		public ClsNACHABase()
		{
		}

		public static void CopyCharArray(char []Source,char[] Destination)
		{

			for(int i=0; i < Source.Length ; i++)
			{
				Destination[i] = Source[i];
			}
		}

	}
	#endregion


	#region ClsNACHAFileHeader Definition
	/// <summary>
	/// File Header Record 
	/// </summary>
	public class ClsNACHAFileHeader : ClsNACHABase
	{
		private char mRecordTypeCode;				// 1 Byte
		private char[] mPriorityCode;				// 2 Bytes
		private char[] mImmediateDestination;		// 10 Bytes
		private char[] mImmediateOrigin;			// 10 Bytes
		private char[] mFileCreationDate;			// 6 Bytes
		private char[] mFileCreationTime;			// 4 bytes
		private char   mFileIDModifier;				// 1 Byte
		private char[] mRecordSize;					// 3 Bytes
		private char[] mBlockingFactor;				// 2 Bytes
		private char   mFormatCode;					// 1 Byte
		private char[] mImmediateDestinationName;	// 23 Bytes
		private char[] mImmediateOriginName;		// 23 Bytes
		private char[] mRefrenceCode;				// 8 Bytes

		#region Public Properties
		public char RecordTypeCode
		{
			get
			{
				return mRecordTypeCode;
			}
		}
		

		public char[] PriorityCode
		{
			get
			{
				return mPriorityCode;
			}
			set 
			{
				CopyCharArray(value,mPriorityCode);
			}
		}


		public char[] ImmediateDestination 
		{
			get 
			{
				return mImmediateDestination;
			}
			set 
			{
				//Validate Transit/Route Number if not valid Throw Exception

				mImmediateDestination[0] = ' '; // First character will be Space always
				for(int i=0; i < value.Length; i++)
					mImmediateDestination[i+1] = value[i];
			}
		}


		public char[] ImmediateOrigin
		{
			get
			{
				return mImmediateOrigin;
			}
			set 
			{
				if(value.Length != 9)
				{
					Exception ex = new Exception("Length of ImmediateOrigin should be exactly 9 chars");
					throw(ex);
				}
				mImmediateOrigin[0] = '1';
				for(int i =0; i < value.Length; i++)
					mImmediateOrigin[i+1] = value[i];
			}
		}


		public char[] FileCreationDate
		{
			get
			{
				return mFileCreationDate;
			}
			set 
			{
				CopyCharArray(value,mFileCreationDate);
			}
		}


		public char[] FileCreationTime
		{
			get
			{
				return mFileCreationTime;
			}
			set 
			{
				CopyCharArray(value,mFileCreationTime);
			}
		}


		public char FileIDModifier
		{
			get
			{
				return mFileIDModifier;
			}
			set 
			{
				mFileIDModifier = value;
			}
		}


		public char[] RecordSize
		{
			get
			{
				return mRecordSize;
			}
		}


		public char[] BlockingFactor
		{
			get
			{
				return mBlockingFactor;
			}
		}


		public char FormatCode
		{
			get 
			{
				return mFormatCode;
			}
		}


		public char[] ImmediateDestinationName
		{
			get 
			{
				return mImmediateDestinationName;
			}
			set 
			{
				CopyCharArray(value,mImmediateDestinationName);
				//If less than 23 characters are provided append spaces
				if(mImmediateDestinationName.Length < 23)
				{
					for(int i = mImmediateDestinationName.Length ; i< 23; i++)
					{
						mImmediateDestinationName[i] = ' ' ;
					}
					
				}
			}
		}


		public char[] ImmediateOriginName
		{
			get 
			{
				return mImmediateOriginName;
			}
			set 
			{
				CopyCharArray(value,mImmediateOriginName);
				//If less than 23 characters are provided append spaces
				if(mImmediateOriginName.Length < 23)
				{
					for(int i = mImmediateOriginName.Length  ; i< 23; i++)
					{
						mImmediateOriginName[i] = ' ' ;
					}
					
				}
			}
		}


		public char[] RefrenceCode
		{
			get
			{
				return mRefrenceCode;
			}
			set 
			{
				CopyCharArray(value,mRefrenceCode);
				//If less than 8 characters are provided append spaces
				if(mRefrenceCode.Length < 8)
				{
					for(int i = mRefrenceCode.Length ; i< 8; i++)
					{
						mRefrenceCode[i] = ' ' ;
					}
					
				}
			}
		}
		#endregion

		public ClsNACHAFileHeader()
		{
			
			mPriorityCode = new char[2];				// 2 Bytes
			mImmediateDestination = new char[10];		// 10 Bytes
			mImmediateOrigin = new char[10];			// 10 Bytes
			mFileCreationDate = new char[6];			// 6 Bytes
			mFileCreationTime = new char[4];			// 4 bytes
			mFileIDModifier =' ';						// 1 Byte
			mRecordSize =new char[3];					// 3 Bytes
			mBlockingFactor = new char[2];				// 2 Bytes
			mFormatCode = '1';							// 1 Byte
			mImmediateDestinationName = new char[23];	// 23 Bytes
			mImmediateOriginName = new char[23];		// 23 Bytes
			mRefrenceCode = new char[8];				// 8 Bytes
			SetDefaultValues();
		}


		private void SetDefaultValues()
		{
			mRecordTypeCode	= RecordTypeCodes.FileHeader ; // 1 Byte
			CopyCharArray(NACHACodes.DefaultPriorityCode.ToCharArray(),mPriorityCode);
			CopyCharArray(NACHACodes.NachaRecordSize.ToCharArray(),mRecordSize); 
			CopyCharArray(NACHACodes.DefaultBlockingFactor.ToCharArray(),mBlockingFactor);
			mFormatCode     = NACHACodes.DefaultFormatCode; 
		}
		
		/// <summary>
		/// Returns String representation of NACHA Record
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sbTemp = new StringBuilder(NACHACodes.RecordSize );
			sbTemp.Append(mRecordTypeCode);
			sbTemp.Append(mPriorityCode);
			sbTemp.Append(mImmediateDestination);
			sbTemp.Append(mImmediateOrigin);
			sbTemp.Append(mFileCreationDate);
			sbTemp.Append(mFileCreationTime);
			sbTemp.Append(mFileIDModifier);
			sbTemp.Append(mRecordSize);
			sbTemp.Append(mBlockingFactor);
			sbTemp.Append(mFormatCode);
			sbTemp.Append(mImmediateDestinationName);
			sbTemp.Append(mImmediateOriginName);
			sbTemp.Append(mRefrenceCode);
			if(sbTemp.Length > 94)
			{
				Exception ex = new Exception("Length of NACHA File Header exceeds 94 chars");
				throw(ex);
			}
			return sbTemp.ToString();
		}

	}
	
	#endregion
	
	
	
	#region ClsNACHABatchHeader Definition
	/// <summary>
	/// BatchHeader Record
	/// </summary>
	public class ClsNACHABatchHeader : ClsNACHABase
	{
		private char mRecordTypeCode;				// 1 Byte
		private char[] mServiceClassCode;			// 3 Bytes
		private char[] mCompanyName ;				// 16 Bytes
		private char[] mCompanyDiscretionaryData;	// 20 Bytes
		private char[] mCompanyID;					// 10 Bytes
		private char[] mStandardEntryClassCode;		// 3 bytes
		private char[] mEntryDescription;			// 10 Byte
		private char[] mDescriptiveDate;			// 6 Bytes
		private char[] mEffectiveEntryDate;			// 6 Bytes
		private char[] mSettelmentDate;				// 3 Byte
		private char   mOriginatorStatusCode;		// 1 Bytes
		private char[] mOriginatingDFIID;			// 8 Bytes
		private char[] mBatchNumber;				// 7 Bytes

		/// <summary>
		/// Returns String representation of NACHA Record
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sbTemp = new StringBuilder(NACHACodes.RecordSize );
			sbTemp.Append(mRecordTypeCode);
			sbTemp.Append(mServiceClassCode);
			sbTemp.Append(mCompanyName);
			sbTemp.Append(mCompanyDiscretionaryData);
			sbTemp.Append(mCompanyID);
			sbTemp.Append(mStandardEntryClassCode);
			sbTemp.Append(mEntryDescription);
			sbTemp.Append(mDescriptiveDate);
			sbTemp.Append(mEffectiveEntryDate);
			sbTemp.Append(mSettelmentDate);
			sbTemp.Append(mOriginatorStatusCode);
			sbTemp.Append(mOriginatingDFIID);
			sbTemp.Append(mBatchNumber);
			if(sbTemp.Length > 94)
			{
				Exception ex = new Exception("Length of NACHA Batch Header exceeds 94 chars");
				throw(ex);
			}
			return sbTemp.ToString();
		}


		#region Public Properties
		public char RecordTypeCode
		{
			get
			{
				return mRecordTypeCode;
			}
		}

		public char[] ServiceClassCode
		{
			get
			{
				return mServiceClassCode;
			}
			set
			{
			 	 CopyCharArray(value,mServiceClassCode );
			}
		}

		public char[] CompanyName
		{
			get
			{
				return mCompanyName;
			}
			set 
			{
				CopyCharArray(value,mCompanyName );
			}
		}

		public char[] CompanyDiscretionaryData
		{
			get
			{
				return mCompanyDiscretionaryData;
			}
			set
			{
				CopyCharArray(value,mCompanyDiscretionaryData);
			}
		}


		//First Character will be "1" always
		public char[] CompanyID
		{
			get
			{
				return mCompanyID;
			}
			set
			{
				if(value.Length != 8)
				{
					Exception ex = new Exception("CompanyID should contain 8 digits");
					ex.Source = "ClsNACHABatchHeader";
				}
				mCompanyID[0] = '1';
				for(int i=0;i< value.Length ;i++)
				{
					mCompanyID[i+1] = value[i];
				}
				
			}
		}

		public char[] StandardEntryClassCode
		{
			get 
			{
				return 	mStandardEntryClassCode;
			}
			set
			{
				CopyCharArray(value,mStandardEntryClassCode);
			}
		}

		public char[] EntryDescription
		{
			get
			{
				return mEntryDescription;
			}
			set 
			{
				CopyCharArray(value,mEntryDescription);
			}
		}

		public char[] DescriptiveDate
		{
			get
			{
				return mDescriptiveDate;
			}
			set
			{
				CopyCharArray(value,mDescriptiveDate);
			}
		}
				
		public char[] EffectiveEntryDate
		{
			get 
			{
				return mEffectiveEntryDate;
			}
			set 
			{
				CopyCharArray(value,mEffectiveEntryDate);
			}
		}

		public char[] SettelmentDate
		{
			get
			{
				return mSettelmentDate;
			}
		}

		public char OriginatorStatusCode
		{
			get
			{
				return mOriginatorStatusCode;
			}
		}

		public char[] OriginatingDFIID
		{
			get
			{
				return mOriginatingDFIID;
			}
			set 
			{
				CopyCharArray(value,mOriginatingDFIID);
			}
		}
		public char[] BatchNumber
		{
			get
			{
				return mBatchNumber;
			}
			set 
			{
				char[] tmp = new char[value.Length];
				CopyCharArray(value,tmp);
				int i;
				for( i= 0 ; i< 7 - tmp.Length ;i++)
				{
					mBatchNumber[i] = '0';
				}
				for(int j=0;i<7 ; i++,j++)
				{
					mBatchNumber[i] = tmp[j];
				}
			}
		}
		
		#endregion
		
		public ClsNACHABatchHeader()
		{
			
			mServiceClassCode = new char[3];							// 3 Bytes
			mCompanyName = new char[16];								// 16 Bytes
			mCompanyDiscretionaryData = new char[20];					// 20 Bytes
			mCompanyID = new char[10];									// 10 Bytes
			mStandardEntryClassCode = new char[3];						// 3 bytes
			mEntryDescription = new char[10];							// 10 Byte
			mDescriptiveDate = new char[6];							// 6 Bytes
			mEffectiveEntryDate = new char[6];							// 6 Bytes
			mSettelmentDate	= new char[3];								// 3 Byte
			mOriginatingDFIID = new char[8];							// 8 Bytes
			mBatchNumber = new char[7];									// 7 Bytes
			SetDefaultValues();
		}

		private void SetDefaultValues()
		{
			mRecordTypeCode	= RecordTypeCodes.BatchHeader ;				// 1 Byte
			mOriginatorStatusCode = NACHACodes.OriginatorStatusCode ;	// 1 Bytes
			// Always three Spaces
			for(int i=0;i<mSettelmentDate.Length;i++)
			{
				mSettelmentDate[i] = ' ' ;
			}
			StandardEntryClassCode = EntryClassCode.PrearrangedPaymentAndDeposit.ToCharArray();

		}

	}
	#endregion

	
	
	#region ClsNACHADetailInfo Definition
	/// <summary>
	/// DetailInfo Record
	/// </summary>
	public class ClsNACHADetailInfo : ClsNACHABase 
	{
		private char   mRecordTypeCode;				// 1 Byte
		private char[] mTransactionCode;			// 2 Bytes
		private char[] mRecievingDFIID;				// 8 Bytes
		private char   mDFICheckDigit;				// 1 Bytes
		private char[] mDFIAccountNumber;			// 17 Bytes 
		private char[] mAmount;						// 10 bytes
		private char[] mRecieversID;				// 15 Byte  Optional
		private char[] mRecieversName;				// 22 Bytes 
		private char[] mDiscretionaryData;			// 2 Bytes  Optional
		private char   mAddendaRecordIndicator;		// 1 Byte
		private char[] mTraceNumber;				// 15 Bytes
		private string mTraceNumberToSet;

		private decimal mTransactionAmount;
		
		#region Public Properties
		public char   RecordTypeCode
		{
			get 
			{
				return mRecordTypeCode;
			}
		}
		public char[] TransactionCode
		{
			get 
			{
				return mTransactionCode;
			}
			set 
			{
				CopyCharArray(value,mTransactionCode);
			}
		}

		public char[] RecievingDFIID
		{
			get
			{
				return mRecievingDFIID;
			}
			set 
			{
				int i;
				for(i=0;i < value.Length-1;i++)
				{

					mRecievingDFIID[i] = value[i];
				}
				mDFICheckDigit = value[i];
			}
		}
		public char   DFICheckDigit
		{
			get
			{
				return mDFICheckDigit;
			}
//			set 
//			{
//				mDFICheckDigit = value;
//			}
		}
		
		public char[] DFIAccountNumber
		{
			get
			{
				return mDFIAccountNumber;
			}
			set 
			{
				CopyCharArray(value,mDFIAccountNumber);
				//If less than 17 chars add blank spaces
				if(value.Length < 17)
				{
					//for (int i = value.Length-1 ;i<17; i++)
					for (int i = value.Length ;i<17; i++)
					{
						mDFIAccountNumber[i] = ' ';
					}
				}
			}
		}

		public decimal TransactionAmount
		{
			get
			{
				return mTransactionAmount;
			}
			set
			{
				mTransactionAmount = value;
				string tempStr = mTransactionAmount.ToString("F");
				if(tempStr.IndexOf('.') > 0)
				{
					Amount = tempStr.Remove(tempStr.IndexOf('.'),1).ToCharArray();
				}
				else
				{
					Amount = tempStr.ToCharArray();
				}

			}
		}

		public char[] Amount
		{
			get 
			{
				return mAmount;
			}
			set
			{
				//Prepend Zeros if less than 10 chars
				int i=0;
				while(i< (10 - value.Length))
				{
					mAmount[i] = '0';
					i++;
				}
				for (int j = 0; j< value.Length; j++,i++)
				{
					mAmount[i] = value[j];
				}
			}
		}

		public char[] RecieversID
		{
			get
			{
				return mRecieversID;
			}
			set
			{
				int i;
				//If less than 15 characters append spaces
				for(i=0;i<value.Length ; i++)
				{
					mRecieversID[i]= value[i];
				}
				while(i<15)
				{
					mRecieversID[i] = ' ';
					i++;
				}
			}
		}
		public char[] RecieversName
		{
			get
			{
				return mRecieversName;
			}
			set
			{
				int i;
				//If less than 22 characters append spaces
				for(i=0;i<value.Length ; i++)
				{
					mRecieversName[i]= value[i];
				}
				while(i<22)
				{
					mRecieversName[i] = ' ';
					i++;
				}
			}
		}

		public char[] DiscretionaryData
		{
			get
			{
				return mDiscretionaryData;
			}
			set
			{
				CopyCharArray(value,mDiscretionaryData);
			}
		}

		public char   AddendaRecordIndicator
		{
			get
			{
				return mAddendaRecordIndicator;
			}
			set
			{
				mAddendaRecordIndicator = value;
			}
		}
		
		public char[] TraceNumber
		{
			get
			{
				return mTraceNumber;
			}
			set
			{
				CopyCharArray(value,mTraceNumber);
			}
		}
		public string TraceNumberToSet
		{
			get
			{
				return mTraceNumberToSet;
			}
			set
			{
				mTraceNumberToSet = value;
			}
		}
		#endregion


		/// <summary>
		/// Returns String representation of NACHA Record
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sbTemp = new StringBuilder();
			sbTemp.Append(mRecordTypeCode);
			sbTemp.Append(mTransactionCode);
			sbTemp.Append(mRecievingDFIID);
			sbTemp.Append(mDFICheckDigit);
			sbTemp.Append(mDFIAccountNumber);
			sbTemp.Append(mAmount);
			sbTemp.Append(mRecieversID);
			sbTemp.Append(mRecieversName);
			sbTemp.Append(mDiscretionaryData);
			sbTemp.Append(mAddendaRecordIndicator);
			sbTemp.Append(mTraceNumber);
			if(sbTemp.Length > 94)
			{
				Exception ex = new Exception("Length of NACHA Detail Record exceeds 94 chars");
				throw(ex);
			}
			return sbTemp.ToString();
		}

		public ClsNACHADetailInfo()
		{
			mTransactionCode = new char[2];			// 2 Bytes
			mRecievingDFIID = new char[8];				// 8 Bytes
			mDFIAccountNumber = new char[17];			// 17 Bytes
			mAmount = new char[10];						// 10 bytes
			mRecieversID = new char[15];				// 15 Byte
			mRecieversName = new char[22];				// 22 Bytes
			mDiscretionaryData  = new char[2];			// 2 Bytes
			mTraceNumber = new char[15];				// 15 Bytes
	
			SetDefaultValues();
		}

		private void SetDefaultValues()
		{
			mRecordTypeCode	= RecordTypeCodes.DetailInfo ;				// 1 Byte
			mAddendaRecordIndicator  = NACHACodes.NoAddendaRecord  ;
			CopyCharArray("  ".ToCharArray(),mDiscretionaryData);
		}

	}
	#endregion


	
	#region ClsNACHABatchControl Definition

	/// <summary>
	/// Batch Control Record
	/// </summary>
	public class ClsNACHABatchControl : ClsNACHABase
	{
		private char mRecordTypeCode;				// 1 Byte
		private char[] mServiceClassCode;			// 3 Bytes
		private char[] mEntryCount;					// 6 Bytes
		private char[] mEntryHash;					// 10 Bytes
		private char[] mTotalDebitEntry;			// 12 Bytes
		private char[] mTotalCreditEntry;			// 12 bytes
		private char[] mCompanyID;					// 10 Byte
		private char[] mAuthenticationCode;			// 19 Bytes
		private char[] mReserved;					// 6 Bytes
		private char[] mOriginatingDFIID;			// 8 Byte
		private char[] mBatchNumber;				// 7 Bytes
		
		#region Public Properties
		public char  RecordTypeCode
		{
			get
			{
				return mRecordTypeCode;
			}
		}
		public char[] ServiceClassCode
		{
			get
			{
				return mServiceClassCode;
			}
			set
			{
				CopyCharArray(value,mServiceClassCode);
			}
		}

		public char[] EntryCount
		{
			get
			{
				return mEntryCount;
			}
			set
			{
				//If less than 6 chars prepend zeros
				int i=0;
				while(i < 6 - value.Length )
				{
					mEntryCount[i]= '0';
					i++;
				}
				for(int j=0; j<value.Length ;j++,i++)
				{
					mEntryCount[i] = value[j];
				}
			}
		}

		public char[] EntryHash
		{
			get
			{
				return mEntryHash;
			}
			set 
			{
				CopyCharArray(value,mEntryHash);
			}
		}

		public char[] TotalDebitEntry
		{

			get 
			{
				return mTotalDebitEntry;
			}
			set
			{
				//Prepend Zeros if less than 12 chars
				int i=0;
				while(i< (12 - value.Length))
				{
					mTotalDebitEntry[i] = '0';
					i++;
				}
				for (int j = 0; j< value.Length; j++,i++)
				{
					mTotalDebitEntry[i] = value[j];
				}
			}
		}
		public char[] TotalCreditEntry
		{

			get 
			{
				return mTotalCreditEntry ;
			}
			set
			{
				//Prepend Zeros if less than 12 chars
				int i=0;
				while(i< (12 - value.Length))
				{
					mTotalCreditEntry[i] = '0';
					i++;
				}
				for (int j = 0; j< value.Length; j++,i++)
				{
					mTotalCreditEntry[i] = value[j];
				}
			}
		}
		public char[] CompanyID
		{
			get
			{
				return mCompanyID;
			}
			set
			{
				CopyCharArray(value,mCompanyID);
			}
		}
		public char[] AuthenticationCode
		{
			get
			{
				return mAuthenticationCode;
			}
			set
			{
				CopyCharArray(value,mAuthenticationCode);
			}
		}

		public char[] Reserved
		{
			get 
			{
				return mReserved;
			}
			set
			{
				CopyCharArray(value,mReserved);
			}
		}
		public char[] OriginatingDFIID
		{
			get 
			{
				return mOriginatingDFIID;
			}
			set
			{
				CopyCharArray(value,mOriginatingDFIID);
			}
		}

		public char[] BatchNumber
		{
			get
			{
				return mBatchNumber;
			}
			set
			{
				CopyCharArray(value,mBatchNumber); 
			}
		}
		#endregion

		/// <summary>
		/// Returns String representation of NACHA Record
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sbTemp = new StringBuilder(NACHACodes.RecordSize);
			sbTemp.Append(mRecordTypeCode);
			sbTemp.Append(mServiceClassCode);
			sbTemp.Append(mEntryCount);
			sbTemp.Append(mEntryHash);
			sbTemp.Append(mTotalDebitEntry);
			sbTemp.Append(mTotalCreditEntry);
			sbTemp.Append(mCompanyID);
			sbTemp.Append(mAuthenticationCode);
			sbTemp.Append(mReserved);
			sbTemp.Append(mOriginatingDFIID);
			sbTemp.Append(mBatchNumber);
			if(sbTemp.Length > 94)
			{
				Exception ex = new Exception("Length of NACHA Batch Control exceeds 94 chars");
				throw(ex);
			}
			return sbTemp.ToString();
		}

		
		
		public ClsNACHABatchControl()
		{
			mServiceClassCode = new char[3];			// 3 Bytes
			mEntryCount = new char[6];					// 6 Bytes
			mEntryHash = new char[10];					// 10 Bytes
			mTotalDebitEntry = new char[12];			// 12 Bytes
			mTotalCreditEntry  = new char[12];			// 12 bytes
			mCompanyID = new char[10];					// 10 Byte
			mAuthenticationCode = new char[19];			// 19 Bytes
			mReserved = new char[6];					// 6 Bytes
			mOriginatingDFIID = new char[8];			// 8 Byte
			mBatchNumber = new char[7];				// 7 Bytes
	
			SetDefaultValues();
		}

		private void SetDefaultValues()
		{
			mRecordTypeCode	= RecordTypeCodes.BatchControl ;				// 1 Byte
			CopyCharArray( new string(' ',19).ToCharArray(),mAuthenticationCode); 
			CopyCharArray( new string(' ',6).ToCharArray(),mReserved);
		}

	}

	#endregion

	
	#region ClsNACHAFileControl Definition 

	/// <summary>
	/// Batch Control Record
	/// </summary>
	public class ClsNACHAFileControl : ClsNACHABase 
	{
		private char mRecordTypeCode;				// 1 Byte
		private char[] mBatchCount;					// 6 Bytes
		private char[] mBlockCount;					// 6 Bytes
		private char[] mEntryCount;					// 8 Bytes
		private char[] mEntryHash;					// 10 Bytes
		private char[] mTotalDebitEntry;			// 12 Bytes
		private char[] mTotalCreditEntry;			// 12 bytes
		private char[] mReserved;					// 39 Bytes
		
		/// <summary>
		/// Returns String representation of NACHA Record
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sbTemp = new StringBuilder(NACHACodes.RecordSize);
			sbTemp.Append(mRecordTypeCode);
			sbTemp.Append(mBatchCount);
			sbTemp.Append(mBlockCount);
			sbTemp.Append(mEntryCount);
			sbTemp.Append(mEntryHash);
			sbTemp.Append(mTotalDebitEntry);
			sbTemp.Append(mTotalCreditEntry);
			sbTemp.Append(mReserved);

			if(sbTemp.Length > 94)
			{
				Exception ex = new Exception("Length of NACHA File Control exceeds 94 chars");
				throw(ex);
			}

			return sbTemp.ToString();
		}


		#region Public Properties
		public char  RecordTypeCode
		{
			get
			{
				return mRecordTypeCode;
			}
		}
		public char[] BatchCount
		{
			get
			{
				return mBatchCount;
			}
			set
			{
				//If less than 6 chars prepend zeros
				int i=0;
				while(i < 6 - value.Length )
				{
					mBatchCount[i]= '0';
					i++;
				}
				for(int j=0; j<value.Length ;j++,i++)
				{
					mBatchCount[i] = value[j];
				}
			}
		}

		public char[] BlockCount
		{
			get
			{
				return mBlockCount;
			}
			set
			{
				//If less than 6 chars prepend zeros
				int i=0;
				while(i < 6 - value.Length )
				{
					mBlockCount[i]= '0';
					i++;
				}
				for(int j=0; j<value.Length ;j++,i++)
				{
					mBlockCount[i] = value[j];
				}
			}
		}

		public char[] EntryCount
		{
			get
			{
				return mEntryCount;
			}
			set
			{
				//If less than 8 chars prepend zeros
				int i=0;
				while(i < 8 - value.Length )
				{
					mEntryCount[i]= '0';
					i++;
				}
				for(int j=0; j<value.Length ;j++,i++)
				{
					mEntryCount[i] = value[j];
				}
			}
		}

		public char[] EntryHash
		{
			get
			{
				return mEntryHash;
			}
			set 
			{
				CopyCharArray( value,mEntryHash);
			}
		}

		public char[] TotalDebitEntry
		{

			get 
			{
				return mTotalDebitEntry;
			}
			set
			{
				//Prepend Zeros if less than 12 chars
				int i=0;
				while(i< (12 - value.Length))
				{
					mTotalDebitEntry[i] = '0';
					i++;
				}
				for (int j = 0; j< value.Length; j++,i++)
				{
					mTotalDebitEntry[i] = value[j];
				}
			}
		}
		public char[] TotalCreditEntry
		{

			get 
			{
				return mTotalCreditEntry ;
			}
			set
			{
				//Prepend Zeros if less than 12 chars
				int i=0;
				while(i< (12 - value.Length))
				{
					mTotalCreditEntry[i] = '0';
					i++;
				}
				for (int j = 0; j< value.Length; j++,i++)
				{
					mTotalCreditEntry[i] = value[j];
				}
			}
		}

		public char[] Reserved
		{
			get
			{
				return mReserved ;
			}
			set
			{
				CopyCharArray( value,mReserved);
			}
		}
		#endregion

		public ClsNACHAFileControl()
		{
			mBatchCount = new char[6];					// 6 Bytes
			mBlockCount = new char[6];					// 6 Bytes
			mEntryCount = new char[8];					// 8 Bytes
			mEntryHash = new char[10];					// 10 Bytes
			mTotalDebitEntry = new char[12];			// 12 Bytes
			mTotalCreditEntry  = new char[12];			// 12 bytes
			mReserved = new char[39];					// 39 Bytes
			
			SetDefaultValues();
		}

		private void SetDefaultValues()
		{
			mRecordTypeCode	= RecordTypeCodes.FileControl ;				// 1 Byte
			for(int i=0; i<39; i++)
			{
				mReserved[i]= ' ';
			}
		}

	}

	#endregion

	
	//Represent a individual NACHA batch 
	public class ClsNACHABatch
	{

		private ClsNACHABatchHeader mBatchHeader ;
		private ClsNACHABatchControl mBatchControl;
		private ArrayList arrDetailRecord;

		private decimal dblTotalCreditAmt ;
		private decimal dblTotalDebitAmt;
		private System.UInt64 iTotalOfDFI;
		private int mRecordCount;


		#region Public Properties
		public decimal TotalCreditAmount
		{
			get
			{
				return dblTotalCreditAmt;
			}
		}

		public decimal TotalDebitAmount
		{
			get
			{
				return dblTotalDebitAmt;
			}
		}

		public System.UInt64 TotalOfDFI
		{
			get
			{
				return iTotalOfDFI;
			}
		}

		public ClsNACHABatchHeader BatchHeaderInfo
		{
			get
			{
				return mBatchHeader;
			}
			set
			{
				mBatchHeader = value;
			}
		}

		public ClsNACHABatchControl BatchControlInfo
		{
			get
			{
				return mBatchControl;
			}
			set
			{
				mBatchControl = value;
			}
		}
		public int RecordCount
		{
			get
			{ 
				return mRecordCount;
			}
		}
		#endregion

		
		public ClsNACHABatch(ClsNACHABatchHeader objBatchHeader)
		{
			mBatchHeader = objBatchHeader;
			arrDetailRecord = new ArrayList();
			dblTotalCreditAmt = 0.00M;
			dblTotalDebitAmt = 0.00M;
			iTotalOfDFI = 0;
			mRecordCount = 1;
		}


		public int AddDetail(ClsNACHADetailInfo objDetailInfo)
		{
			char []tmp = objDetailInfo.TraceNumberToSet.ToCharArray();
			char []TraceNum = new char[15];

			int i , j , k;
			for(i = 0 ; i< mBatchHeader.OriginatingDFIID.Length;i++)
			{
				TraceNum[i] = mBatchHeader.OriginatingDFIID[i];
			}
			for(j=0 ;j< 7-tmp.Length ;j++,i++)
			{
				TraceNum[i] = '0';
			}
			for(k=0;k<tmp.Length;k++,i++)
			{
				TraceNum[i] = tmp[k];
			}
			
			objDetailInfo.TraceNumber = TraceNum;
			arrDetailRecord.Add(objDetailInfo);
			if(TransactionCodes.IsDebitEntry(new string(objDetailInfo.TransactionCode)))
				dblTotalDebitAmt = dblTotalDebitAmt + objDetailInfo.TransactionAmount; 
			else if(TransactionCodes.IsCreditEntry(new string(objDetailInfo.TransactionCode)))
				dblTotalCreditAmt = dblTotalCreditAmt + objDetailInfo.TransactionAmount;

			string strDFI_ID =new string(objDetailInfo.RecievingDFIID);
			
			iTotalOfDFI = iTotalOfDFI + Convert.ToUInt64 (strDFI_ID);

			mRecordCount ++ ;
			//Set other info of batch header
			return 1;
		}

		public int SetBatchControl()
		{
			string strTotalCredit,strTotalDebit;
			strTotalCredit = dblTotalCreditAmt.ToString();
			if(strTotalCredit.IndexOf('.') > 0)
			{
				strTotalCredit = strTotalCredit.Remove(strTotalCredit.IndexOf('.'),1);
			}

			strTotalDebit = dblTotalDebitAmt.ToString();
			if(strTotalDebit.IndexOf('.') > 0)
			{
				strTotalDebit= strTotalDebit.Remove(strTotalDebit.IndexOf('.'),1);
			}

			mBatchControl = new ClsNACHABatchControl();
            mBatchControl.ServiceClassCode = mBatchHeader.ServiceClassCode ;
			mBatchControl.EntryCount = arrDetailRecord.Count.ToString().ToCharArray();
			
			//Entry hash to be calculated
			// If more than 10 chars ingore the left most positions
			char []tmpEntryHash = iTotalOfDFI.ToString().ToCharArray();
			int i=0;
			if(tmpEntryHash.Length > 10)
			{
				i = (tmpEntryHash.Length - 10)-1;
			}

			int Counter = 0;
			if(tmpEntryHash.Length < 10)
			{
				for(Counter = 0 ; Counter < 10 - tmpEntryHash.Length ; Counter++)
				{
					mBatchControl.EntryHash[Counter] = '0';
				}
			}

			for(int j= Counter ; i< tmpEntryHash.Length ; i++,j++)
				mBatchControl.EntryHash[j] = tmpEntryHash[i];

			mBatchControl.TotalCreditEntry = strTotalCredit.ToCharArray();
			mBatchControl.TotalDebitEntry  = strTotalDebit.ToCharArray();
			mBatchControl.CompanyID  = mBatchHeader.CompanyID;
			mBatchControl.OriginatingDFIID = mBatchHeader.OriginatingDFIID;
			mBatchControl.BatchNumber = mBatchHeader.BatchNumber;

			mRecordCount++;
			return 1;

		}
		public override string ToString()
		{
			StringBuilder sbTemp = new StringBuilder();
			sbTemp.Append(mBatchHeader.ToString());
			sbTemp.Append(System.Environment.NewLine);
			for(int i =0 ; i< arrDetailRecord.Count;i++)
			{
				ClsNACHADetailInfo objDetail = (ClsNACHADetailInfo)(arrDetailRecord[i]);
				sbTemp.Append(objDetail.ToString());
				sbTemp.Append(System.Environment.NewLine);
			}
			sbTemp.Append(mBatchControl.ToString());
			sbTemp.Append(System.Environment.NewLine);
			return sbTemp.ToString();
		}
	}

	//Collection of NACHA Batches
	public class ClsNACHABatches
	{
	
		private ClsNACHABatch mCurrentBatch;
		private int mNoOfBatches;
		private ArrayList arrNACHABatches ;
		private decimal dblTotalCreditAmt ;
		private decimal dblTotalDebitAmt;
		private System.UInt64 iTotalOfDFI;
		private int mRecordCount;
		
		public ClsNACHABatches()
		{
			arrNACHABatches = new ArrayList();
			mCurrentBatch = null;
			mNoOfBatches = 0;
			dblTotalCreditAmt = 0;
			dblTotalDebitAmt = 0;
			iTotalOfDFI = 0;
			mRecordCount = 0;
		}
	

		public int AddBatch(ClsNACHABatchHeader objBatchHeader)
		{
			if(mCurrentBatch != null)
			{
				Exception ex = new Exception("Open Batch found ,close it before adding a new batch");
				throw(ex);
			}
			mCurrentBatch = new ClsNACHABatch(objBatchHeader);
			mNoOfBatches ++ ;
			mCurrentBatch.BatchHeaderInfo.BatchNumber = mNoOfBatches.ToString().ToCharArray();
			arrNACHABatches.Add(mCurrentBatch);
			
			return 1;
		}

		public int CloseBatch()
		{
			//Populate BatchControl for current batch from batch header
			mCurrentBatch.SetBatchControl();				
			dblTotalDebitAmt = dblTotalDebitAmt + mCurrentBatch.TotalDebitAmount;
			dblTotalCreditAmt = dblTotalCreditAmt + mCurrentBatch.TotalCreditAmount;
			iTotalOfDFI = iTotalOfDFI + mCurrentBatch.TotalOfDFI;
			mRecordCount = mRecordCount + mCurrentBatch.RecordCount;
			mCurrentBatch = null;
			return 1;
		}

		public override string ToString()
		{
			StringBuilder sbTemp = new StringBuilder();
			for(int i =0 ; i< arrNACHABatches.Count;i++)
			{
				ClsNACHABatch  objBatch = (ClsNACHABatch)(arrNACHABatches[i]);
				sbTemp.Append(objBatch.ToString());
			}
			return sbTemp.ToString();
		}

		#region Public Properties
		public decimal TotalCreditAmount
		{
			get
			{
				return dblTotalCreditAmt;
			}
		}

		public decimal TotalDebitAmount
		{
			get
			{
				return dblTotalDebitAmt;
			}
		}

		public int NumberOfBatches
		{
			get
			{
				return mNoOfBatches;

			}
		}
	
		public System.UInt64 TotalOfDFI
		{
			get
			{
				return iTotalOfDFI;
			}
		}
		public bool HasOpenBatch
		{
			get
			{

				if(mCurrentBatch == null)
				{
					return false;
				}
				return true;
			}

		}
		public ClsNACHABatch CurrentBatch
		{
			get
			{
				if(mCurrentBatch == null)
				{
					Exception ex = new Exception("No Open Batch found");
					throw(ex);
				}
				return mCurrentBatch;
			}
		}

		public int RecordCount
		{
			get
			{ 
				return mRecordCount;
			}
		}
		#endregion
		
	}

	/// <summary>
	/// Summary description for ClsNACHARecord.
	/// </summary>
	public class ClsNACHARecord
	{
		private string mHostName,mUserName,mPassWord,mFTPDirectory,mLocalDirectory;
		
		private ClsNACHAFileHeader mFileHeader ;
		private ClsNACHAFileControl mFileControl = new ClsNACHAFileControl();
		private ClsNACHABatches mNachaBatches; 
		private int mBlockCount;
		private int mRecordCount;
		private int mEntryAddendaCount;
		private string []mFillerRecord;

		#region Public Properties
		public int EntryAddendaCount
		{
			get
			{
				return mEntryAddendaCount;
			}
			set
			{
				mEntryAddendaCount = value;
			}
		}
		public ClsNACHAFileHeader FileHeader
		{
			get
			{
				return mFileHeader;
			}
		}

		public ClsNACHAFileControl FileControl
		{
			get
			{
				return mFileControl;
			}
		}
		
		public ClsNACHABatches Batches
		{
			get
			{
				return mNachaBatches;
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

		public ClsNACHARecord(ClsNACHAFileHeader objHeaderInfo)
		{
			mFileHeader = objHeaderInfo;
			mNachaBatches = new ClsNACHABatches();
			mRecordCount = 1;
			mFillerRecord = null;
			mEntryAddendaCount = 0;
		}

		public int CloseFile()
		{
			
			
			//Populate FileControl
			string strTotalCredit,strTotalDebit;
			strTotalCredit = mNachaBatches.TotalCreditAmount.ToString();
			if(strTotalCredit.IndexOf('.') > 0)
			{
				strTotalCredit = strTotalCredit.Remove(strTotalCredit.IndexOf('.'),1);
			}

			strTotalDebit = mNachaBatches.TotalDebitAmount.ToString();
			if(strTotalDebit.IndexOf('.') > 0)
			{
				strTotalDebit= strTotalDebit.Remove(strTotalDebit.IndexOf('.'),1);
			}
			
			mFileControl = new ClsNACHAFileControl();
			mFileControl.BatchCount = mNachaBatches.NumberOfBatches.ToString().ToCharArray();

			//Block Count to be calculated and Filter record to be added before writing file
			int iFillerRecord = 0;
			mRecordCount++;
			mRecordCount += mNachaBatches.RecordCount;

			if(mRecordCount < 10)
			{
				iFillerRecord = 10 - mRecordCount;
			}
			else
			{
				iFillerRecord = 10 - (mRecordCount % 10);
				mBlockCount = mRecordCount  / 10;
			}

			if(iFillerRecord !=0)
				mBlockCount++;

			//If any block has less than 10 records add that many filter records
			if(iFillerRecord != 0)
			{
				//Add Filter Records
				mFillerRecord = new string[iFillerRecord];
				for(int i=0; i< iFillerRecord ; i++)
				{
					mFillerRecord[i] = new string('9',94);
				}

			}

			mFileControl.BlockCount = mBlockCount.ToString().ToCharArray();
            
			//End calculating Blockcount
			mFileControl.EntryCount = mEntryAddendaCount.ToString().ToCharArray() ;

			//Entry hash to be calculated
			// If more than 10 chars ingore the left most positions
			char []tmpEntryHash = mNachaBatches.TotalOfDFI.ToString().ToCharArray();
			int index = 0 ;
			if(tmpEntryHash.Length > 10)
			{
				index = (tmpEntryHash.Length - 10)-1;
			}
			int Counter = 0;
			if(tmpEntryHash.Length < 10)
			{
				for(Counter = 0 ; Counter < 10 - tmpEntryHash.Length ; Counter++)
				{
					mFileControl.EntryHash[Counter] = '0';
				}
			}
			for(int j=Counter ; index< tmpEntryHash.Length ; index++,j++)
				mFileControl.EntryHash[j] = tmpEntryHash[index];
			
			mFileControl.TotalDebitEntry = strTotalDebit.ToCharArray();
			mFileControl.TotalCreditEntry = strTotalCredit.ToCharArray();

			return 1;
		}

		public int FTPNachaFile(string FileName, bool SupressFTP)
		{
			//string FilePath;
            /*
			FilePath = mLocalDirectory + FileName;

			WriteToFile(FilePath);

			if(!SupressFTP)
			{
				FtpLib.FTPFactory objFTP =  new  FtpLib.FTPFactory();
				objFTP.RemoteHost = mHostName;
				objFTP.RemoteUser = mUserName;
				objFTP.RemotePassword = mPassWord;
				objFTP.Login();
				objFTP.ChangeDirectory(mFTPDirectory);
				objFTP.Upload(FilePath);
				objFTP.Close();
			}*/
			return 1;

		}
		public int WriteToFile(string FilePath)
		{
			//Ravindra(04-20-2009): Impersonate
			ClsAttachment objAttachment = new ClsAttachment();

			objAttachment.ImpersonateUser(ClsCommon.ImpersonationUserId,ClsCommon.ImpersonationPassword,ClsCommon.ImpersonationDomain);

			StringBuilder sbTemp = new StringBuilder();
			sbTemp.Append(mFileHeader.ToString());
			sbTemp.Append(System.Environment.NewLine);
            sbTemp.Append(mNachaBatches.ToString());
			sbTemp.Append(mFileControl.ToString());
			if(mFillerRecord != null)
			{
				for(int i=0; i<mFillerRecord.Length; i++)
				{
					sbTemp.Append(System.Environment.NewLine);
					sbTemp.Append(mFillerRecord[i]);
				}
			}
			//File creation logic
			FileStream objFStream  = new FileStream(FilePath,FileMode.Create,FileAccess.Write);
			StreamWriter objSwriter = new StreamWriter(objFStream);
			objSwriter.Write(sbTemp.ToString());
			objSwriter.Close();
			objFStream.Close();
			return 1;
		}

	}

}

