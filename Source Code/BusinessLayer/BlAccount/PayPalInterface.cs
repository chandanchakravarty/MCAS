/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  05/31/2007
<End Date				: -	
<Description			: -   BL for Credit Card Processing 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
//using PFProCOMLib;
using PayPal.Payments.Common.Utility;
using PayPal.Payments.Communication;
using PayPal.Payments.DataObjects;
namespace Cms.BusinessLayer.BlAccount
{
	
	public enum CardType
	{
		AmericanExpressOptima =0,
		DinersClub =0,
		DiscoverNovus =0,
		Enroute =0,
		JCB =0,
		MasterCard =0,
		Visa =0
	}

	public enum PayPalResult
	{
		Approved = 0,
		UserAuthenticationFailed = 1,
		InvalidTenderType = 2,
		InvalidTransactionType = 3,
		InvalidAmountFormat = 4,
		InvalidMerchantInformation = 5,
		UnsupportedCurrencyCode = 6,
		FieldFormatError = 7,
		NotATransactionServer = 8,
		TooManyParameters = 9,
		TooManyLineItems = 10,
		ClientTimeOut = 11,
		Declined = 12,
		Referral = 13 , // Transaction cannot be approved electronically but can be approved with a verbal authorization
		InvalidClientCertificationID = 14,
		OriginalTransactionIDNotFound = 19,
		CannotFindCustomerReferenceNumber = 20,
		InvalidABANumber = 22,
		InvalidAccountNumber = 23,
		InvalidExpirationDate = 24,
		InvalidHostMapping = 25,
		InvalidVendorAccount = 26,
		InsufficientPartnerPermissions = 27,
		InsufficientUserPermissions =28,
		Exception = -1
	}

	public sealed class MethodOfPayment
	{
		public static readonly string AutomatedClearingHouse ="A";
		public static readonly string CreditCard = "C";
		public static readonly string PinlessDebit = "D";
		public static readonly string ElectronicCheck = "E";
		public static readonly string TeleCheck   = "K";
		public static readonly string PayPal="P";
	}
	public sealed class TransactionType
	{
		public static readonly string SalesTransaction = "S";
		public static readonly string CreditTransaction = "C";
		public static readonly string Authorization= "A";
		public static readonly string DelayedCapture = "D";
		public static readonly string Void = "V";
		public static readonly string VoiceAuthorization= "F";
		public static readonly string Inquiry= "I";
	}

	public class PayPalResponse
	{
		private string mPNREF;
		private string mResult;
		private string mCVV2Match;
		private string mResponseMessage;

		public string ResponseMessage
		{
			get
			{
				return mResponseMessage;
			}
			set
			{
				mResponseMessage = value;
			}
		}

		public string CVV2Match
		{
			get
			{
				return mCVV2Match;
			}
			set
			{
				mCVV2Match = value;
			}
		}
		public string PNRefrence
		{
			get
			{
				return mPNREF;
			}
			set
			{
				mPNREF = value;
			}
		}
		public string Result 
		{
			get 
			{
				return mResult;
			}
			set
			{
				mResult = value;
			}
		}
	}

	public class TransactionInfo
	{
		private string mTRXTYPE;
		private string mTENDER;
		private string mCardNumber;
		private string mCVV2;
		private string mExpiryDate;
		private string mAmount;
		private string mPolicyNumber;
		private string mCustomerName;
		private string mRefrenceID;

		private bool mAddressVerificatioRequired;
		//private bool mAddressVerificatioRequiredl;
		private string mFirstName;
		private string mMiddleName;
		private string mLastName;
		private string mStreet;
		private string mCity;
		private string mState;
		private string mZip;

		private string mNote;

		#region TransactionInfo Properties
		
		public string RefrenceID
		{
			get
			{
				return mRefrenceID;
			}
			set
			{
				mRefrenceID = value;
			}
		}

		public string PolicyNumber
		{
			get
			{
				return mPolicyNumber;
			}
			set
			{
				mPolicyNumber = value;
			}
		}

		public string CustomerName 
		{
			get
			{
				return mCustomerName;
			}
			set
			{
				mCustomerName = value;
			}
		}

		public string FirstName
		{
			get
			{
				return mFirstName;
			}
			set
			{
				mFirstName = value;
			}
		}

		public string MiddleName
		{
			get
			{
				return mMiddleName;
			}
			set
			{
				mMiddleName = value;
			}
		}

		public string LastName
		{
			get
			{
				return mLastName;
			}
			set
			{
				mLastName = value;
			}
		}
		public string Street
		{
			get
			{
				return mStreet;
			}
			set
			{
				mStreet = value;
			}
		}
		public string City
		{
			get
			{
				return mCity;
			}
			set 
			{
				mCity = value;
			}
		}
		public string State
		{
			get
			{
				return mState;
			}
			set
			{
				mState = value;
			}
		}
		public string Zip 
		{
			get
			{
				return mZip ;
			}
			set
			{
				mZip = value;
			}
		}

		public bool AddressVerificationRequired
		{
			get
			{
				return mAddressVerificatioRequired;
			}
			set
			{
				mAddressVerificatioRequired = value;
			}
		}

		public string NOTE
		{
			get
			{
				return mNote;
			}
			set
			{
				mNote = value;
			}
		}

		/// <summary>
		/// Get or Set Transaction Amount
		/// </summary>
		public string Amount
		{
			get
			{
				return mAmount;
			}
			set 
			{
				mAmount = value;
			}
		}
		/// <summary>
		/// Get or Set Credit Card Expiry Date format should be MMYY
		/// </summary>
		public string ExpiryDate
		{
			get
			{
				return mExpiryDate;
			}
			set
			{
				if(value.Length != 4)
				{
					throw(new Exception("Expiry Date should be 4 characters. Expected value MMYY"));
				}
				mExpiryDate = value;
			}
		}
		/// <summary>
		/// Get or Set Credit Card secutity code
		/// </summary>
		public string CVV2
		{
			get
			{
				return mCVV2;
			}
			set
			{
				if(value.Length > 4 || value.Length < 3)
				{
					throw(new Exception("CVV number should be excatly 3 or 4 characters long"));
				}
				mCVV2 = value;
			}
		}

		/// <summary>
		/// Get or Set Credit Card Number
		/// </summary>
		public string CardNumber
		{
			get
			{
				return mCardNumber;
			}
			set
			{
				//if(value.Length != 16)
				if(value.Length < 13)
				{
					//throw(new Exception("Card Number should be exactly 16 digits long"));
					throw(new Exception("Card Number length is invalid"));
				}
				mCardNumber = value;
			}
		}
					
		#endregion

		public TransactionInfo()
		{
			mAddressVerificatioRequired = false;
			mTRXTYPE = "";
			mTENDER = "";
		}
	}
	/// <summary>
	/// Summary description for PayPalInterface.
	/// </summary>
	public class PayPalInterface
	{
		private string mHostName;
		private int mHostPort;
		private string mVendorName;
		private string mPartnerName;
		private string mUserName;
		private string mPassword;
		private int mTimeOut; 

		

		#region Public Properties
		/// <summary>
		///  Get or Set Password for Paypal authentication
		/// </summary>
		public string Password
		{
			get
			{
				return mPassword;
			}
			set 
			{
				mPassword = value;
			}
		}

		/// <summary>
		/// Get or Set UserName for Paypal authentication
		/// </summary>
		public string UserName
		{
			get
			{
				return mUserName;
			}
			set
			{
				mUserName = value;
			}
		}
		/// <summary>
		///  Get or Set The ID provided to you by the authorized PayPal Reseller who registered you 
		///  for the Payflow Pro service. If you purchased your account directly from PayPal, use PayPal.
		/// </summary>
		public string PartnerName
		{
			get
			{
				return mPartnerName;
			}
			set
			{
				mPartnerName = value;
			}
		}
		
		/// <summary>
		/// Get or Set Your merchant login ID that you created when you registered for the Payflow Pro account
		/// </summary>
		public string VendorName
		{
			get
			{
				return mVendorName;
			}
			set
			{
				mVendorName = value;
			}
		}
		/// <summary>
		/// Get or Set Host Port for PayPal communication 
		/// </summary>
		public int HostPort
		{
			get
			{
				return mHostPort;
			}
			set
			{
				mHostPort = value;
			}
		}
		/// <summary>
		/// Get or Set Address(Name) of PayPal host
		/// </summary>
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
		/// <summary>
		/// Get or Set TimeOut for Paypal transaction. Default is 30 
		/// </summary>
		public int TimeOut
		{
			get
			{
				return mTimeOut;
			}
			set
			{
				mTimeOut = value;
			}
		}
					


		#endregion		

		
		public PayPalInterface()
		{
			mTimeOut	= 30;
			mHostPort	= 443;
		}

		public PayPalResponse CaptureAuthroiseFunds(string OrigID, string Amount) 
		{
			string strParamList;
			//CREATE  PARAMLIST FOR PAYPAL
			strParamList	= "USER=" + mUserName;
			strParamList	= strParamList + "&VENDOR=" + mVendorName;
			strParamList	= strParamList + "&PARTNER=" + mPartnerName ;
			strParamList	= strParamList + "&PWD=" + mPassword;
			strParamList	= strParamList + "&TRXTYPE=" + TransactionType.DelayedCapture;
			strParamList	= strParamList + "&TENDER=" + MethodOfPayment.CreditCard; ;
			strParamList	= strParamList + "&ORIGID["+ OrigID.Length  +"]=" + OrigID;
			strParamList	= strParamList + "&AMT=" + Amount ;
			
			//PASS DATA TO PAYPAL
			string strProcessReturnData = InvokeWebservice(strParamList);			
			//END PASS DATA TO PAYPAL
			PayPalResponse objResponse = new PayPalResponse();
			if(strProcessReturnData.StartsWith("PAYPAL ERROR"))
			{
				objResponse.Result = "404";
				objResponse.ResponseMessage = strProcessReturnData;
			}
			else
			{
				string []arrResult =  strProcessReturnData.Split('&');
				for(int i=0;i<arrResult.Length;i++)
				{
					string []temp = arrResult[i].Split('=');
				
					if(temp[0]  == "PNREF")
					{
						objResponse.PNRefrence = temp[1];
					}
					if(temp[0]  == "RESULT")
					{
						objResponse.Result = temp[1];
					}
					if(temp[0] == "CVV2MATCH")
					{
						objResponse.CVV2Match  = temp[1];
					}
				
					if(temp[0] == "RESPMSG")
					{
						objResponse.ResponseMessage  = temp[1];
					}
				}
			}
			return objResponse;
		}

		public PayPalResponse DoSalesFromReference(string OrigID, string Amount, string Comment1, string Comment2) 
		{
			string strParamList;
			//CREATE  PARAMLIST FOR PAYPAL
			strParamList	= "USER=" + mUserName;
			strParamList	= strParamList + "&VENDOR=" + mVendorName;
			strParamList	= strParamList + "&PARTNER=" + mPartnerName ;
			strParamList	= strParamList + "&PWD=" + mPassword;
			strParamList	= strParamList + "&TRXTYPE=" + TransactionType.SalesTransaction;
			strParamList	= strParamList + "&TENDER=" + MethodOfPayment.CreditCard; ;
			strParamList	= strParamList + "&ORIGID["+ OrigID.Length  +"]=" + OrigID;
			strParamList	= strParamList + "&AMT=" + Amount ;

			if(Comment1 != null)
			{
				strParamList	= strParamList + "&COMMENT1["+ Comment1.Length +"]=" + Comment1 ;
			}
			if(Comment2 != null)
			{
				strParamList	= strParamList + "&COMMENT2["+ Comment2.Length  +"]=" + Comment2;
			}

			//PASS DATA TO PAYPAL
			string strProcessReturnData = InvokeWebservice(strParamList);			
			//END PASS DATA TO PAYPAL
			PayPalResponse objResponse = new PayPalResponse();
			if(strProcessReturnData.StartsWith("PAYPAL ERROR"))
			{
				objResponse.Result = "404";
				objResponse.ResponseMessage = strProcessReturnData;
			}
			else
			{
				string []arrResult =  strProcessReturnData.Split('&');
				for(int i=0;i<arrResult.Length;i++)
				{
					string []temp = arrResult[i].Split('=');
				
					if(temp[0]  == "PNREF")
					{
						objResponse.PNRefrence = temp[1];
					}
					if(temp[0]  == "RESULT")
					{
						objResponse.Result = temp[1];
					}
					if(temp[0] == "CVV2MATCH")
					{
						objResponse.CVV2Match  = temp[1];
					}
				
					if(temp[0] == "RESPMSG")
					{
						objResponse.ResponseMessage  = temp[1];
					}
				}
			}
			return objResponse;
		}

		public PayPalResponse VoidAuthorisation(string OrigID)
		{
			string strParamList;
			//CREATE  PARAMLIST FOR PAYPAL
			strParamList	= "USER=" + mUserName;
			strParamList	= strParamList + "&VENDOR=" + mVendorName;
			strParamList	= strParamList + "&PARTNER=" + mPartnerName ;
			strParamList	= strParamList + "&PWD=" + mPassword;
			strParamList	= strParamList + "&TRXTYPE=" + TransactionType.Void ;
			strParamList	= strParamList + "&TENDER=" + MethodOfPayment.CreditCard; 
			strParamList	= strParamList + "&ORIGID["+ OrigID.Length  +"]=" + OrigID;
			//END PARAMLIST

			//PASS DATA TO PAYPAL
			string strProcessReturnData = InvokeWebservice(strParamList);			
			//END PASS DATA TO PAYPAL
			PayPalResponse objResponse = new PayPalResponse();
			if(strProcessReturnData.StartsWith("PAYPAL ERROR"))
			{
				objResponse.Result = "404";
				objResponse.ResponseMessage = strProcessReturnData;
			}
			else
			{
				string []arrResult =  strProcessReturnData.Split('&');
				for(int i=0;i<arrResult.Length;i++)
				{
					string []temp = arrResult[i].Split('=');
				
					if(temp[0]  == "PNREF")
					{
						objResponse.PNRefrence = temp[1];
					}
					if(temp[0]  == "RESULT")
					{
						objResponse.Result = temp[1];
					}
					if(temp[0] == "CVV2MATCH")
					{
						objResponse.CVV2Match  = temp[1];
					}
				
					if(temp[0] == "RESPMSG")
					{
						objResponse.ResponseMessage  = temp[1];
					}
				}
			}
			return objResponse;

		}

		/// <summary>
		/// This method will perform an Authorisation which can later be used for
		/// Delayed Capture or Recurring Transaction 
		/// </summary>
		/// <param name="objTran">Credit Card Information</param>
		/// <returns>Paypal Response</returns>
		public PayPalResponse DoAuthorisation(TransactionInfo objTran)
		{
			string strParamList;
			//CREATE  PARAMLIST FOR PAYPAL
			strParamList	= "USER=" + mUserName;
			strParamList	= strParamList + "&VENDOR=" + mVendorName;
			strParamList	= strParamList + "&PARTNER=" + mPartnerName ;
			strParamList	= strParamList + "&PWD=" + mPassword;
			strParamList	= strParamList + "&TRXTYPE=" + TransactionType.Authorization ;
			strParamList	= strParamList + "&TENDER=" + MethodOfPayment.CreditCard; ;
			strParamList	= strParamList + "&ACCT=" + objTran.CardNumber;
			strParamList	= strParamList + "&CVV2=" + objTran.CVV2;;
			strParamList	= strParamList + "&EXPDATE=" + objTran.ExpiryDate ;
			strParamList	= strParamList + "&AMT=" + objTran.Amount ;
			if(objTran.PolicyNumber != null)
			{
				strParamList	= strParamList + "&COMMENT1["+ objTran.PolicyNumber.Length +"]=" + objTran.PolicyNumber ;
			}
			if(objTran.CustomerName !=null)
			{
				strParamList	= strParamList + "&COMMENT2["+ objTran.CustomerName.Length  +"]=" + objTran.CustomerName;
			}


			// If Address Verifaction Required
			if(objTran.AddressVerificationRequired)
			{	
				if(objTran.FirstName != null)
				{
					strParamList = strParamList + "&NAME["+ objTran.FirstName.Length  +"]=" + objTran.FirstName  ;
				}
				if(objTran.MiddleName != null)
				{
					strParamList = strParamList + "&MIDDLENAME["+ objTran.MiddleName.Length +"]=" +  objTran.MiddleName ;
				}
				if(objTran.LastName != null)
				{
					strParamList = strParamList + "&LASTNAME["+ objTran.LastName.Length  +"]=" +  objTran.LastName ;
				}
				if(objTran.Street != null)
				{
					strParamList = strParamList + "&STREET["+objTran.Street.Length +"]=" + objTran.Street ;
				}
				if(objTran.City != null)
				{
					strParamList = strParamList + "&CITY["+ objTran.City.Length  +"]=" + objTran.City ;
				}
				if(objTran.State != null)
				{
					strParamList = strParamList + "&STATE["+ objTran.State.Length  +"]=" + objTran.State  ;
				}
				if(objTran.Zip != null)
				{
					strParamList = strParamList + "&ZIP["+ objTran.Zip.Length  +"]=" + objTran.Zip ;
				}
			}



			//END PARAMLIST

			//PASS DATA TO PAYPAL
			string strProcessReturnData = InvokeWebservice(strParamList);			
			//END PASS DATA TO PAYPAL
			PayPalResponse objResponse = new PayPalResponse();
			if(strProcessReturnData.StartsWith("PAYPAL ERROR"))
			{
				objResponse.Result = "404";
				objResponse.ResponseMessage = strProcessReturnData;
			}
			else
			{
				string []arrResult =  strProcessReturnData.Split('&');
				for(int i=0;i<arrResult.Length;i++)
				{
					string []temp = arrResult[i].Split('=');
				
					if(temp[0]  == "PNREF")
					{
						objResponse.PNRefrence = temp[1];
					}
					if(temp[0]  == "RESULT")
					{
						objResponse.Result = temp[1];
					}
					if(temp[0] == "CVV2MATCH")
					{
						objResponse.CVV2Match  = temp[1];
					}
				
					if(temp[0] == "RESPMSG")
					{
						objResponse.ResponseMessage  = temp[1];
					}
				}
			}
			return objResponse;

		}
		public PayPalResponse DoCreditTransaction(TransactionInfo objTran)
		{
			string strParamList;
		
			//CREATE  PARAMLIST FOR PAYPAL
			strParamList	= "USER=" + mUserName;
			strParamList	= strParamList + "&VENDOR=" + mVendorName;
			strParamList	= strParamList + "&PARTNER=" + mPartnerName ;
			strParamList	= strParamList + "&PWD=" + mPassword;
			strParamList	= strParamList + "&TRXTYPE=" + TransactionType.CreditTransaction ;
			strParamList	= strParamList + "&TENDER=" + MethodOfPayment.CreditCard; ;
			
			//Commented by Ravindra(06-25-2008)
//			strParamList	= strParamList + "&ACCT=" + objTran.CardNumber;
//			strParamList	= strParamList + "&CVV2=" + objTran.CVV2;;
//			strParamList	= strParamList + "&EXPDATE=" + objTran.ExpiryDate ;
			
			strParamList	= strParamList + "&AMT=" + objTran.Amount ;
			if(objTran.PolicyNumber != null)
			{
				strParamList = strParamList + "&COMMENT1["+ objTran.PolicyNumber.Length  +"]=" + objTran.PolicyNumber  ;
				//strParamList	= strParamList + "&COMMENT1=" + objTran.PolicyNumber ;
			}
			if(objTran.CustomerName != null)
			{
				strParamList = strParamList + "&COMMENT2["+ objTran.CustomerName.Length  +"]=" + objTran.CustomerName  ;
				//strParamList	= strParamList + "&COMMENT2=" + objTran.CustomerName;
			}
			strParamList	= strParamList + "&ORIGID["+ objTran.RefrenceID.Length  +"]="	+ objTran.RefrenceID ;
			
			//Commented by Ravindra(06-25-2008)
//			// If Address Verifaction Required
//			if(objTran.AddressVerificationRequired)
//			{	
//				strParamList = strParamList + "&NAME=" + objTran.FirstName  ;
//				strParamList = strParamList + "&MIDDLENAME=" +  objTran.MiddleName ;
//				strParamList = strParamList + "&LASTNAME=" +  objTran.LastName ;
//				strParamList = strParamList + "&STREET=" + objTran.Street ;
//				strParamList = strParamList + "&CITY=" + objTran.City ;
//				strParamList = strParamList + "&STATE=" + objTran.State  ;
//				strParamList = strParamList + "&ZIP=" + objTran.Zip ;
//			}

			//END PARAMLIST

			//PASS DATA TO PAYPAL
			string strProcessReturnData = InvokeWebservice(strParamList);			
			//END PASS DATA TO PAYPAL
			PayPalResponse objResponse = new PayPalResponse();
			if(strProcessReturnData.StartsWith("PAYPAL ERROR"))
			{
				objResponse.Result = "404";
				objResponse.ResponseMessage = strProcessReturnData;
			}
			else
			{
				string []arrResult =  strProcessReturnData.Split('&');
				for(int i=0;i<arrResult.Length;i++)
				{
					string []temp = arrResult[i].Split('=');
				
					if(temp[0]  == "PNREF")
					{
						objResponse.PNRefrence = temp[1];
					}
					if(temp[0]  == "RESULT")
					{
						objResponse.Result = temp[1];
					}
					if(temp[0] == "CVV2MATCH")
					{
						objResponse.CVV2Match  = temp[1];
					}
				
					if(temp[0] == "RESPMSG")
					{
						objResponse.ResponseMessage  = temp[1];
					}
				}
			}
			return objResponse;
		}

		public PayPalResponse VoidSalesTransaction(TransactionInfo objTran)
		{
			string strParamList;
		
			//CREATE  PARAMLIST FOR PAYPAL
			strParamList	= "USER=" + mUserName;
			strParamList	= strParamList + "&VENDOR=" + mVendorName;
			strParamList	= strParamList + "&PARTNER=" + mPartnerName ;
			strParamList	= strParamList + "&PWD=" + mPassword;
			strParamList	= strParamList + "&TRXTYPE=" + TransactionType.Void;
			strParamList	= strParamList + "&TENDER=" + MethodOfPayment.CreditCard; ;
			
			strParamList	= strParamList + "&AMT=" + objTran.Amount ;
			if(objTran.PolicyNumber != null)
			{
				strParamList = strParamList + "&COMMENT1["+ objTran.PolicyNumber.Length  +"]=" + objTran.PolicyNumber  ;
				//strParamList	= strParamList + "&COMMENT1=" + objTran.PolicyNumber ;
			}
			if(objTran.CustomerName != null)
			{
				strParamList	= strParamList + "&COMMENT2["+ objTran.CustomerName.Length  +"]=" + objTran.CustomerName  ;
				//strParamList	= strParamList + "&COMMENT2=" + objTran.CustomerName;
			}
			strParamList	= strParamList + "&ORIGID["+ objTran.RefrenceID.Length  +"]="	+ objTran.RefrenceID ;
			
			//PASS DATA TO PAYPAL
			string strProcessReturnData = InvokeWebservice(strParamList);			
			//END PASS DATA TO PAYPAL
			PayPalResponse objResponse = new PayPalResponse();
			if(strProcessReturnData.StartsWith("PAYPAL ERROR"))
			{
				objResponse.Result = "404";
				objResponse.ResponseMessage = strProcessReturnData;
			}
			else
			{
				string []arrResult =  strProcessReturnData.Split('&');
				for(int i=0;i<arrResult.Length;i++)
				{
					string []temp = arrResult[i].Split('=');
				
					if(temp[0]  == "PNREF")
					{
						objResponse.PNRefrence = temp[1];
					}
					if(temp[0]  == "RESULT")
					{
						objResponse.Result = temp[1];
					}
					if(temp[0] == "CVV2MATCH")
					{
						objResponse.CVV2Match  = temp[1];
					}
				
					if(temp[0] == "RESPMSG")
					{
						objResponse.ResponseMessage  = temp[1];
					}
				}
			}
			return objResponse;
		}

		public PayPalResponse DoSalesTransaction(TransactionInfo objTran)
		{
			string strParamList;
		
			//CREATE  PARAMLIST FOR PAYPAL
			strParamList	= "USER=" + mUserName;
			strParamList	= strParamList + "&VENDOR=" + mVendorName;
			strParamList	= strParamList + "&PARTNER=" + mPartnerName ;
			strParamList	= strParamList + "&PWD=" + mPassword;
			strParamList	= strParamList + "&TRXTYPE=" + TransactionType.SalesTransaction;
			strParamList	= strParamList + "&TENDER=" + MethodOfPayment.CreditCard; ;
			strParamList	= strParamList + "&ACCT=" + objTran.CardNumber;
			strParamList	= strParamList + "&CVV2=" + objTran.CVV2;;
			strParamList	= strParamList + "&EXPDATE=" + objTran.ExpiryDate ;
			strParamList	= strParamList + "&AMT=" + objTran.Amount ;
			if(objTran.PolicyNumber != null)
			{
				strParamList	= strParamList + "&COMMENT1["+ objTran.PolicyNumber.Length +"]=" + objTran.PolicyNumber ;
			}
			if(objTran.CustomerName !=null)
			{
				strParamList	= strParamList + "&COMMENT2["+ objTran.CustomerName.Length  +"]=" + objTran.CustomerName;
			}
			
			
			// If Address Verifaction Required
			if(objTran.AddressVerificationRequired)
			{	
				if(objTran.FirstName != null)
				{
					strParamList = strParamList + "&NAME["+ objTran.FirstName.Length  +"]=" + objTran.FirstName  ;
				}
				if(objTran.MiddleName != null)
				{
					strParamList = strParamList + "&MIDDLENAME["+ objTran.MiddleName.Length +"]=" +  objTran.MiddleName ;
				}
				if(objTran.LastName != null)
				{
					strParamList = strParamList + "&LASTNAME["+ objTran.LastName.Length  +"]=" +  objTran.LastName ;
				}
				if(objTran.Street != null)
				{
					strParamList = strParamList + "&STREET["+objTran.Street.Length +"]=" + objTran.Street ;
				}
				if(objTran.City != null)
				{
					strParamList = strParamList + "&CITY["+ objTran.City.Length  +"]=" + objTran.City ;
				}
				if(objTran.State != null)
				{
					strParamList = strParamList + "&STATE["+ objTran.State.Length  +"]=" + objTran.State  ;
				}
				if(objTran.Zip != null)
				{
					strParamList = strParamList + "&ZIP["+ objTran.Zip.Length  +"]=" + objTran.Zip ;
				}
			}

			//END PARAMLIST

			//PASS DATA TO PAYPAL
			string strProcessReturnData = InvokeWebservice(strParamList);			
			//END PASS DATA TO PAYPAL
			PayPalResponse objResponse = new PayPalResponse();
			if(strProcessReturnData.StartsWith("PAYPAL ERROR"))
			{
				objResponse.Result = "404";
				objResponse.ResponseMessage = strProcessReturnData;
			}
			else
			{
				string []arrResult =  strProcessReturnData.Split('&');
				for(int i=0;i<arrResult.Length;i++)
				{
					string []temp = arrResult[i].Split('=');
				
					if(temp[0]  == "PNREF")
					{
						objResponse.PNRefrence = temp[1];
					}
					if(temp[0]  == "RESULT")
					{
						objResponse.Result = temp[1];
					}
					if(temp[0] == "CVV2MATCH")
					{
						objResponse.CVV2Match  = temp[1];
					}
				
					if(temp[0] == "RESPMSG")
					{
						objResponse.ResponseMessage  = temp[1];
					}
				}
			}
			return objResponse;
		}

		private string InvokeWebservice(string strParamList)
		{	
			string strPayflowResponse;
			
			try 
			{
				PayflowNETAPI objPayFlow = new PayflowNETAPI(mHostName);;
				strPayflowResponse = objPayFlow.SubmitTransaction(strParamList,PayflowUtility.RequestId);
			}
			catch (Exception ex)
			{
				strPayflowResponse = "PAYPAL ERROR : " + ex.Message ;
			}
			return strPayflowResponse;

		}

		//Ravindra(07-12-2008): This version of PayFlow component is no longer supported.
		/*
		private string InvokeWebservice(string strParamList)
		{
			
			int iPPContext;
			string	strNullString = null;
			string strPayflowResponse;
			
			try 
			{
				PNComClass PayFlowPro = new PNComClass();
			
				PayFlowPro.PNInit();
				iPPContext = PayFlowPro.CreateContext(mHostName , mHostPort, mTimeOut, strNullString , 0, strNullString , strNullString ); 
				PayFlowPro.SubmitTransaction(iPPContext, strParamList, strParamList.Length);
			
				strPayflowResponse = PayFlowPro.Response;
				PayFlowPro.DestroyContext(iPPContext);
				PayFlowPro.PNCleanup();
			}
			catch (Exception ex)
			{
				strPayflowResponse = "PAYPAL ERROR : " + ex.Message ;
			}
			finally
			{
	
			}
			return strPayflowResponse;

		}
		*/
	}
}
