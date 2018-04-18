/******************************************************************************************
<Author					: -   Pradeep Iyer
<Start Date				: -	  Sep 11, 2005
<End Date				: -	
<Description			: - 	Contains some utility functions
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 

using System;
using System.Collections;
using System.Text;
using Cms.Model.Client;
//using Cms.CmsWeb.com.iix.expressnet;
using System.Configuration;
using System.Xml; 
using Cms.Model.Maintenance;

namespace Cms.CmsWeb.Utils
{
	public class CreditScoreDetails
	{
		public CreditScoreDetails()
		{
		}

		public int Score;
		public string FirstFactor = "";
		public string SecondFactor = "";
		public string ThirdFactor = "";
		public string FourthFactor = "";
	}

	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	/// 
	public class Utility
	{
		
		#region Private Variables
		private StringBuilder sbError; 
		private System.Collections.Specialized.NameValueCollection objSetting;
		private StringBuilder sbRequest; 
		private string strUrl;
		private string strUserName ;
		private string strPassword ;
		private string strAccountNumber ;
		private string strBillCode="000";
		private string pad = "";
		private ClsRequestResponseLogInfo objLogInfo = null;
		#endregion
		
		#region Properties

		
		/// <summary>
		/// Get and Set the Web Service User Name
		/// </summary>
		public string UserName
		{
			get
			{
				return strUserName;
			}
			set
			{
				strUserName = value;
			}
		}

		/// <summary>
		/// Get and Set the Web Service Password
		/// </summary>
		public string Password
		{
			get
			{
				return strPassword;
			}
			set
			{
				strPassword = value;
			}
		}

		/// <summary>
		/// Get and Set the Account Number
		/// </summary>
		public string AccountNumber
		{
			get
			{
				return strAccountNumber;
			}
			set
			{
				strAccountNumber = value;
			}
		}


		/// <summary>
		/// Get and Set the URL.
		/// </summary>
		public string URL
		{
			get
			{
				return strUrl;
			}
			set
			{
				strUrl = value;
			}
		}

		#endregion
				
		#region Constructor
		public Utility()
		{}
		
		public Utility(string UserName, string Password, string AccountNumber,string ServiceURL)
		{
			this.UserName = UserName;
			this.Password = Password;
			this.AccountNumber = AccountNumber;
			this.strUrl = ServiceURL;
		}

		#endregion

		#region Methods
		/// <summary>
		/// Calls the third party web service (IIX) and returns
		/// the Insurance score for a Customer
		/// </summary>
		/// <param name="objCustomerInfo"></param>
		/// <returns></returns>
		public CreditScoreDetails GetCustomerCreditScore(ClsCustomerInfo objCustomerInfo)
		{
			//auth objProxy = new auth();
			//ClsIIXProxy objProxy = new ClsIIXProxy();
			ClsIIXProxy objProxy = new ClsIIXProxy(strUrl);;
			objLogInfo = new ClsRequestResponseLogInfo();
						
			//System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)ConfigurationSettings.GetConfig("IIXSettings");
			
			 sbError = new StringBuilder();
			checkIIXSettings();
			//Perform validations
			if (  strUserName == null )
			{
				sbError.Append("<br>Web service User Name not found");
				//throw new Exception("Web service User Name not found");
			}
			
			if (  strPassword == null )
			{
				sbError.Append("<br>Password not found");
				//throw new Exception("Password not found");
			}

			if (  strAccountNumber == null )
			{
				sbError.Append("<br>Account Number not found");
				//throw new Exception("Account Number not found");
			}
			
			if ( objCustomerInfo.CustomerLastName.Length > 25 )
			{
				objCustomerInfo.CustomerLastName = objCustomerInfo.CustomerLastName.Substring(0,25);
				//sbError.Append("<br>Last name cannot be greater than 25.");
				//throw new Exception("Last name cannot be greater than 25.");
			}
			
			if ( objCustomerInfo.CustomerFirstName.Length > 15 )
			{
				objCustomerInfo.CustomerFirstName = objCustomerInfo.CustomerFirstName.Substring(0,15);
				//sbError.Append("<br>First name cannot be greater than 15.");
				//throw new Exception("First name cannot be greater than 15.");
			}
			
			if ( objCustomerInfo.CustomerAddress1.Length > 27 )
			{
				objCustomerInfo.CustomerAddress1 = objCustomerInfo.CustomerAddress1.Substring(0,27);
				//sbError.Append("<br>Address 1 cannot be greater than 27.");
				//throw new Exception("Address 1 cannot be greater than 27.");
			}
			
			if ( objCustomerInfo.CustomerCity.Length > 27)
			{
				objCustomerInfo.CustomerCity = objCustomerInfo.CustomerCity.Substring(0,27);
				//sbError.Append("<br>Address 1 cannot be greater than 27.");
			}

			if ( objCustomerInfo.CustomerZip.Length > 10)
			{
				objCustomerInfo.CustomerZip = objCustomerInfo.CustomerZip.Substring(0,10);
				//sbError.Append("<br>Zip cannot be greater than 10.");
			}
			
			if ( objCustomerInfo.CustomerStateCode.Length > 2)
			{
				objCustomerInfo.CustomerStateCode = objCustomerInfo.CustomerStateCode.Substring(0,2);
				//sbError.Append("<br>State code cannot be greater than 2 in length.");
			}
			
			if ( sbError.Length > 0 )
			{
				throw new Exception(sbError.ToString());
			}
			//End of validations
			
			CreditScoreDetails objScore = new CreditScoreDetails();

			//Pad each segment with extra spaces if required//////////////////////
			if ( objCustomerInfo.CustomerHomePhone.Length < 7)
			{
				objCustomerInfo.CustomerHomePhone = objCustomerInfo.CustomerHomePhone.PadRight(7,' ');
			}

			if ( objCustomerInfo.CustomerAddress1.Length < 27)
			{
				objCustomerInfo.CustomerAddress1 = objCustomerInfo.CustomerAddress1.PadRight(27,' ');
			}

			//Pad customer Last name
			if ( objCustomerInfo.CustomerLastName.Length < 25 )
			{
				objCustomerInfo.CustomerLastName = objCustomerInfo.CustomerLastName.PadRight(25,' ');
			}
			
			if ( objCustomerInfo.CustomerFirstName.Length < 15 )
			{
				objCustomerInfo.CustomerFirstName = objCustomerInfo.CustomerFirstName.PadRight(15,' ');
			}
			
			if ( objCustomerInfo.CustomerMiddleName.Length < 15 )
			{
				objCustomerInfo.CustomerMiddleName = objCustomerInfo.CustomerMiddleName.PadRight(15,' ');
			}
			
			/*
			if ( objCustomerInfo.PREFIX.Length < 3 )
			{
				objCustomerInfo.PREFIX.PadRight(3,' ');
			}*/
			
			if ( objCustomerInfo.CustomerSuffix.Length < 3 )
			{
				objCustomerInfo.CustomerSuffix = objCustomerInfo.CustomerSuffix.PadRight(3,' ');
			}
			
			if ( objCustomerInfo.CustomerAddress1.Length < 27)
			{
				objCustomerInfo.CustomerAddress1 = objCustomerInfo.CustomerAddress1.PadRight(27,' ');
			}
			
			if ( objCustomerInfo.CustomerCity.Length < 27)
			{
				objCustomerInfo.CustomerCity = objCustomerInfo.CustomerCity.PadRight(27,' ');
			}

			if ( objCustomerInfo.CustomerZip.Length < 10)
			{
				objCustomerInfo.CustomerZip = objCustomerInfo.CustomerZip.PadRight(10,' ');
			}
			
			if ( objCustomerInfo.CustomerHomePhone.Length < 7)
			{
				objCustomerInfo.CustomerHomePhone = objCustomerInfo.CustomerHomePhone.PadRight(7,' ');
			}

			if ( objCustomerInfo.CustomerStateCode.Length < 2)
			{
				objCustomerInfo.CustomerStateCode = objCustomerInfo.CustomerStateCode.PadRight(2,' ');
			}
			string strDateOfBirth= "";//Convert.ToString((objCustomerInfo.DATE_OF_BIRTH.Year/100)+1) + Convert.ToString(objCustomerInfo.DATE_OF_BIRTH.Year%100) + objCustomerInfo.DATE_OF_BIRTH.Month.ToString("00") +objCustomerInfo.DATE_OF_BIRTH.Day.ToString("00");
			//dateofbirth
			if (strDateOfBirth.Trim() !="")
			{
				//strDateOfBirth="";
				if (strDateOfBirth.Trim().Length > 8 )
				{
					//sbError.Append("<br>Date Of Birth length is greater than 8");
					strDateOfBirth = strDateOfBirth.Substring(0,8);
				}
				if (strDateOfBirth.Trim().Length < 8 )
				{
					strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
				}
			}
			else
			{
				strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
			}
			string strSSN=objCustomerInfo.SSN_NO.ToString();  
			strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(strSSN);
			strSSN=objCustomerInfo.SSN_NO.ToString().Replace("-","");  
			if (strSSN.Trim() !="")
			{
				if (strSSN.Trim().Length >9 )
				{
					strSSN = strSSN.Substring(0,9);
				}
				if (strSSN.Trim().Length < 9 )
				{
					strSSN=strSSN.PadRight(9,' '); 
				}
			}
			else
			{
				strSSN=strSSN.PadRight( 9,' '); 
			}

			string strHouseNo="1",strAptNumber="";
			Cms.CmsWeb.webcontrols.AddressDetails AddressDetails= this.CreateAddressDetails(objCustomerInfo.CustomerAddress1,objCustomerInfo.CustomerCity,objCustomerInfo.CustomerStateCode,objCustomerInfo.CustomerZip);
			if (AddressDetails.Status=="999" || AddressDetails.RecCount=="0")
			{
				sbError.Append("ADDRESS_NOT_VALIDATED :Address Could not be validate.");
				throw new Exception(sbError.ToString());
			}
            if (!string.IsNullOrEmpty(AddressDetails.AddressProperty[0].Number))
            {
                strHouseNo = AddressDetails.AddressProperty[0].Number.ToString();
            }
            if (!string.IsNullOrEmpty(AddressDetails.AddressProperty[0].Unit))
            {
                strAptNumber = AddressDetails.AddressProperty[0].Unit.ToString();
            }
			if (strHouseNo.Trim() !="")
			{
				if (strHouseNo.Trim().Length >10 )
				{
						strHouseNo = strHouseNo.Substring(0,10);
				}
				if (strHouseNo.Trim().Length < 10)
				{
					strHouseNo=strHouseNo.PadRight(10,' '); 
				}
			}
			else
			{
				strHouseNo=strHouseNo.PadRight(10,' '); 
			}
			if (strAptNumber.Trim() !="")
			{
				if (strAptNumber.Trim().Length >5 )
				{
					strAptNumber = strAptNumber.Substring(0,5);
				}
				if (strAptNumber.Trim().Length < 5)
				{
					strAptNumber=strAptNumber.PadRight(5,' '); 
				}
			}
			else
			{
				strAptNumber=strAptNumber.PadRight( 5,' '); 
			}
			pad = "";
			sbRequest = new StringBuilder();
			strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode("TUS");
			sbRequest.Append(strUserName);//User name,3
			sbRequest.Append(strPassword);//User password,10
			//sbRequest.Append("  ");	//2
			sbRequest.Append(strAccountNumber);//Account,6
			//sbRequest.Append("AMS");//Bill code,3
			sbRequest.Append(strBillCode);//Bill code,3
			sbRequest.Append("TUS");//Product,3
			sbRequest.Append("1");//nametype,1
			sbRequest.Append(objCustomerInfo.CustomerLastName);//lastname,25
			sbRequest.Append(objCustomerInfo.CustomerFirstName);//firstname,15
			sbRequest.Append(objCustomerInfo.CustomerMiddleName);//middlename,15
			//sbRequest.Append(objCustomerInfo.PREFIX);//prefix,3
			//sbRequest.Append(objCustomerInfo.CustomerSuffix);//suffix,3

			sbRequest.Append(pad.PadRight(3,' '));//prefix,3
			sbRequest.Append(pad.PadRight(3,' '));//suffix,3
			sbRequest.Append(strSSN); //ssn,9 
			//sbRequest.Append(pad.PadRight(9,' ')); //ssn,9 
			sbRequest.Append(pad.PadRight(8,' '));//dob,8
			//sbRequest.Append(strDateOfBirth);//dob,8
			sbRequest.Append(pad.PadRight(3,' '));//age,3
			sbRequest.Append(pad.PadRight(1,' '));//gender,1
			sbRequest.Append(strHouseNo.PadRight(10,' '));//house number,10
			sbRequest.Append(pad.PadRight(2,' '));//street direction,2
			sbRequest.Append(objCustomerInfo.CustomerAddress1);//street name,27
			sbRequest.Append(pad.PadRight(2,' '));//street type,2
			//sbRequest.Append(pad.PadRight(5,' '));//apt number,5
			sbRequest.Append(strAptNumber);//apt number,5
			sbRequest.Append(objCustomerInfo.CustomerCity);//city,27
			sbRequest.Append(objCustomerInfo.CustomerStateCode);//state,2
			sbRequest.Append(objCustomerInfo.CustomerZip);//zip,10
			sbRequest.Append(pad.PadRight(4,' '));//length of residence,4
			sbRequest.Append(pad.PadRight(1,' '));//residential status,1
			//sbRequest.Append(pad.PadRight(2,' '));//phone type,2
			sbRequest.Append("01");//phone type,2
			sbRequest.Append(pad.PadRight(3,' '));//phone area code,3
			//sbRequest.Append(objCustomerInfo.CustomerHomePhone);//phone number,7
			sbRequest.Append(pad.PadRight(7,' '));

			sbRequest.Append(pad.PadRight(5,' '));//phone extension,5
			sbRequest.Append("ebix" + pad.PadRight(36,' '));//quoteback,40

			string strRequest = sbRequest.ToString();
			
			if ( strRequest.Length != 255 )
			{
				throw new Exception("The request string length for IIX web service should be 255. The current length is: " + strRequest.Length.ToString());
			}
			
			//Set the Log Model Object Values
			objLogInfo.CUSTOMER_ID = objCustomerInfo.CustomerId;
			objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.INSURANCETOKEN);
			objLogInfo.SERVICE_VENDOR = "IIX";
			objLogInfo.REQUEST_DATETIME = DateTime.Now;
			objLogInfo.IIX_REQUEST=strRequest;
			
			//Send the request			  
			objProxy.SetLogModel = objLogInfo;
			string strWebRequest = 	objProxy.sendRequest(strRequest);
			
			if ( strWebRequest.StartsWith("Error"))
			{
				throw new Exception(strWebRequest);
			}
			
			string strResponse = "";
			string strData = "";
			string strCreditScore = "";
			string strFirstFactor = "";
			string strSecondFactor = "";
			string strThirdFactor = "";
			string strFourthFactor = "";

			//Response received
			if ( strWebRequest.StartsWith("Accept"))
			{
				try
				{
					strResponse = strRequest.Substring(0,25) + strWebRequest.Substring(7);

					
					//Set the Log Model Object Values
					objLogInfo.CUSTOMER_ID = objCustomerInfo.CustomerId;
					objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.INSURANCEDATA);
					objLogInfo.SERVICE_VENDOR = "IIX";
					objLogInfo.REQUEST_DATETIME = DateTime.Now;
					objLogInfo.IIX_RESPONSE=strResponse;
					//Get the Response
					objProxy.SetLogModel = objLogInfo;
					for (int i = 0; i < 5; i++)		// wait 5 seconds and try 5 times
					{
						strData = objProxy.getResponse(strResponse);
	
						if ( strData.IndexOf("Error:Response not yet available")==-1)
						{
							break; 
						}
						else if(strData.IndexOf("Error:Response not yet available")!=-1 && i==4)
						{
							throw new Exception("Insurance Score could not be fetched." + strData);
						}
						System.Threading.Thread.Sleep(5000);
					}
					//Find the Credit score segment from the response
					//Sample:SC0103400980+599   A086061069021
					int index = strData.IndexOf("SC01");
					if ( index == -1 )
					{
						//Credit score not found
						objScore.Score = -2; // CHANGED FROM -1 TO -2(no hit no score) ON 15 MAY AS ASKED BY RAJAN 
						return objScore;
					}
					else
					{
						string strSign = strData.Substring(index + 12,1);
						string strScore = strData.Substring(index + 13,5);
						if (strScore.Trim()=="") // Hit but No score Case
						{
							objScore.Score = -1; // CHANGED FROM 0 TO -1 (hit no score) ON 15 MAY AS ASKED BY RAJAN 
							return objScore;
						}
						strFirstFactor = strData.Substring(index + 20,3);
						strSecondFactor = strData.Substring(index + 23,3);
						strThirdFactor = strData.Substring(index + 26,3);
						strFourthFactor = strData.Substring(index + 29,3);

						strCreditScore = strSign.Trim() + strScore.Trim();

					
					}
				}
				catch(Exception ex)
				{
					throw (ex);
					
				}
			
			}

			int intScore = int.Parse(strCreditScore);
			
			objScore.Score = intScore;
			objScore.FirstFactor = strFirstFactor;
			objScore.SecondFactor = strSecondFactor;
			objScore.ThirdFactor = strThirdFactor;
			objScore.FourthFactor = strFourthFactor;
			
			return objScore;

		}



		private void checkIIXSettings()
		{
			//objSetting =(System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig("IIXSettings");
			//Perform validations
			/*
			if ( objSetting == null )
			{
				sbError.Append("The custom section IIXSettings not found in web.config.");
				return; 
				//throw new Exception("The custom section IIXSettings not found in web.config.");
			}

			if ( objSetting["UserName"] == null)
			{

				sbError.Append("<br>Web service User Name not found");
				return; 
				//throw new Exception("Web service User Name not found");
			}
			else
			{
				if (objSetting["UserName"].ToString().Trim().Length > 3)
				{
					sbError.Append("<br>User Name is wrong,maximum length exceed!");
					return; 
				}
				else
				{
					strUserName = objSetting["UserName"].ToString();
					if (strUserName.Trim().Length < 3)
					{
						strUserName= strUserName.PadRight(3,' '); 
					}
				}
			}
							
			if (  objSetting["Password"] == null )
			{
				sbError.Append("<br>Password not found");
				return; 
				//throw new Exception("Password not found");
			}
			else
			{
				if (objSetting["Password"].ToString().Trim().Length > 10)
				{
					sbError.Append("<br>Password is wrong,maximum length exceed!");
					return; 
				}
				else
				{
					strPassword = objSetting["Password"].ToString();
					if (strPassword .Trim().Length < 10)
					{
						strPassword= strPassword.PadRight(10,' '); 
					}
				}
			}

			if (  objSetting["AccountNumber"] == null )
			{
				sbError.Append("<br>Account Number not found");
				return; 
				//throw new Exception("Account Number not found");
			}
			else
			{
				if ( objSetting["AccountNumber"].ToString().Trim().Length > 6)
				{
					sbError.Append("<br>Account Number is wrong,maximum length exceed!");
					return; 
				}
				else
				{
					strAccountNumber = objSetting["AccountNumber"].ToString();
					if (strAccountNumber .Trim().Length < 6)
					{
						strAccountNumber= strAccountNumber.PadRight(6,' '); 
					}
				}
			}
			*/
//			if (  objSetting["BillCode"] == null )
//			{
//				sbError.Append("<br>Bill Code not found");
//				return; 
//				//throw new Exception("Account Number not found");
//			}
//			else
//			{
//				if ( objSetting["BillCode"].ToString().Length > 3)
//				{
//					sbError.Append("<br>Bill Code is wrong,maximum length exceed!");
//					return; 
//				}
//				else
//				{
//					strBillCode= objSetting["BillCode"].ToString();
//					if (strBillCode .Trim().Length < 3)
//					{
//						strBillCode= strBillCode.PadRight(3,' '); 
//					}
//				}
//			}
			//strBillCode="AMS";
			if (strUserName.Trim().Length < 3)
			{
				strUserName= strUserName.PadRight(3,' '); 
			}
			if (strAccountNumber .Trim().Length < 6)
			{
				strAccountNumber= strAccountNumber.PadRight(6,' '); 
			}
			if (strPassword .Trim().Length < 10)
			{
				strPassword= strPassword.PadRight(10,' '); 
			}
			strBillCode="000";
				
		}

		public System.Collections.Specialized.StringCollection GetUndisclosedDrivers(Cms.Model.Application.ClsDriverDetailsInfo [] objDriverInfo)
		{
			System.Collections.Specialized.StringCollection obj = new System.Collections.Specialized.StringCollection();
			XmlDocument objResult= new XmlDocument();  
			System.Xml.XmlElement  objElement = null;
		
			//Cms.BusinessLayer.BlCommon.ClsCommon.gets
			//com.iix.expressnet.auth objProxy =  new com.iix.expressnet.auth();
			//ClsIIXProxy objProxy = new ClsIIXProxy();
			ClsIIXProxy objProxy = new ClsIIXProxy(strUrl);
			ClsRequestResponseLogInfo objLogInfo = new ClsRequestResponseLogInfo();

			checkIIXSettings();

			string strRequest = GetCommonHader();
			System.Text.StringBuilder Drivers = new StringBuilder();

			if (objDriverInfo.Length > 0)
			{
				Drivers.Append( MakeCommonDriverRequest(objDriverInfo[0].DRIVER_STATE, objDriverInfo[0].DRIVER_DRIV_LIC
					, objDriverInfo[0].DRIVER_LNAME, objDriverInfo[0].DRIVER_FNAME
					, "",objDriverInfo[0].DRIVER_MNAME
					, objDriverInfo[0].DRIVER_DOB.ToString("MMddyyyy")
					,objDriverInfo[0].DRIVER_SEX,"",objDriverInfo[0]));
			}

			for (int ctr = 0; ctr < objDriverInfo.Length; ctr++)
			{	
				Drivers.Append( MakeRequest(objDriverInfo[ctr].DRIVER_STATE, objDriverInfo[ctr].DRIVER_DRIV_LIC
					, objDriverInfo[ctr].DRIVER_LNAME, objDriverInfo[ctr].DRIVER_FNAME
					, "",objDriverInfo[ctr].DRIVER_MNAME
					, objDriverInfo[ctr].DRIVER_DOB.ToString("MMddyyyy")
					,objDriverInfo[ctr].DRIVER_SEX,"",objDriverInfo[ctr]));
			}


			//Set the Log Model Object Values
			objLogInfo.CUSTOMER_ID = objDriverInfo[0].CUSTOMER_ID;
			objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERTOKEN);
			objLogInfo.SERVICE_VENDOR = "IIX";
			objLogInfo.REQUEST_DATETIME = DateTime.Now;
			objLogInfo.IIX_REQUEST= strRequest;
			
			//Send the request			  
			objProxy.SetLogModel = objLogInfo;
			string strWebRequest = 	objProxy.sendRequest(strRequest + Drivers.ToString());
			
			if ( strWebRequest.StartsWith("Error"))
			{
				return obj;
			}
			
			string strResponse = "";
			string strData = "";

			//Response received
			if ( strWebRequest.StartsWith("Accept"))
			{
				
				int i;
				int count=0;
				string Record=""; 
				Hashtable  objArrRecords = new Hashtable();
				strResponse = strRequest.Substring(0,25) + strWebRequest.Substring(7);
				
				
				//Set the Log Model Object Values
				objLogInfo.CUSTOMER_ID = objDriverInfo[0].CUSTOMER_ID;
				objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERDATA); 
				objLogInfo.SERVICE_VENDOR = "IIX";
				objLogInfo.REQUEST_DATETIME = DateTime.Now;
				objLogInfo.IIX_RESPONSE=strResponse;
			
				//get the response
				objProxy.SetLogModel = objLogInfo;
				for (int j = 0; j < 5; j++)		// wait 5 seconds and try 5 times
				{
					strData = objProxy.getResponse(strResponse);
	
					if ( strData.IndexOf("Error:Response not yet available")==-1)
					{
						break; 
					}
					else if(strData.IndexOf("Error:Response not yet available")!=-1 && j==4)
					{
						throw new Exception(strData);
					}
					System.Threading.Thread.Sleep(5000);
				}
				//Removing the common header
				string SubResponse = strData.Substring(108);
				
				//Retreiving the number of drivers
				int NoOfDrivers = int.Parse(SubResponse.Substring(9,2));
				//int NoOfDrivers = int.Parse(SubResponse.Substring(10,2));

				int Index = 0;

				count=0;
				for ( i = 0; i < NoOfDrivers; i++) 
				{

					//string test ="";
					objElement =objResult.CreateElement("Driver" );
					//test =Record.Substring(13,6);
					objElement.SetAttribute("name", SubResponse.Substring(51 + Index, 30)); 
					//test =Record.Substring(21,8);
//					objElement.SetAttribute("gender", SubResponse.Substring(81 + Index, 1)); 
//					//test =Record.Substring(41,38);
//					objElement.SetAttribute("dateOfBirth", SubResponse.Substring(82 + Index, 8));
//					//test =Record.Substring(79,10);
//					objElement.SetAttribute("height", SubResponse.Substring(90 + Index, 4));  
//					//test =Record.Substring(90,3);
//					objElement.SetAttribute("weight", SubResponse.Substring(94 + Index, 3)); 
//					//test =Record.Substring(93,10);
//					objElement.SetAttribute("license", SubResponse.Substring(97 + Index, 25));  
//					objElement.SetAttribute("ssn", SubResponse.Substring(122 + Index, 9));  
//					//test =Record.Substring(103,3);
//					objElement.SetAttribute("phone", SubResponse.Substring(131 + Index, 10));  
//					objElement.SetAttribute("ext", SubResponse.Substring(141 + Index, 5));  

					objResult.DocumentElement.AppendChild(objElement);

					Index = Index + 95;
				}
					
			}
			obj.Add(objResult.OuterXml); 
			//return objResult.OuterXml;
			return obj;
		}

		public string GetCommonHader()
		{

			sbRequest = new StringBuilder();
			checkIIXSettings();
			strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode("UDI");
			sbRequest.Append(strUserName);//User name,3
			sbRequest.Append(strPassword);//User password,10
			sbRequest.Append(strAccountNumber);//Account,6
			sbRequest.Append(strBillCode);//BillCode,3
			sbRequest.Append("UDI");//Product,3

			return sbRequest.ToString();

		}

		public string MakeCommonDriverRequest(string strState,string strDLNo, string strLastName, string strFirstName, string strSuffix,string strMiddleName,string strDateOfBirth,string strGender,string strClientCode,Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo)
		{
			
			try
			{
		
				if (strState.Trim() !="")
				{
					if ( strState.Trim().Length > 2 )
					{
						sbError.Append("<br>State code length is greater than 2 .");
					}
					if ( strState.Trim().Length < 2 )
					{
						strState=strState.PadRight( 2,' '); 
					}
				}
				else
				{
					strState= strState.PadRight( 2,' '); 
				}
				
		
				//---------------------------------------------------
				

			
				sbRequest = new StringBuilder();
	

				sbRequest.Append((objDriverInfo.DRIVER_ADD1 + ' ' + objDriverInfo.DRIVER_ADD2).PadRight(45,' '));//julian_date,3,optional zero fill if not used
				sbRequest.Append(objDriverInfo.DRIVER_CITY.PadRight(20,' '));//julian_date,3,optional zero fill if not used
				
				//sbRequest.Append("I");//order_purpose,1
				sbRequest.Append(strState);//State,2,post office state code

				sbRequest.Append(objDriverInfo.DRIVER_ZIP.PadRight(9));//julian_date,3,optional zero fill if not used
				sbRequest.Append(pad.PadRight(40,' '));	//julian_date,3,optional zero fill if not used
				
				
				
				return sbRequest.ToString();

			}
			catch (Exception ex )
			{
				throw (ex);
			}
		}

		public string MakeRequest(string strState,string strDLNo, string strLastName, string strFirstName, string strSuffix,string strMiddleName,string strDateOfBirth,string strGender,string strClientCode,Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo)
		{
			
			try
			{
		
				
				//dl 
				if (strDLNo.Trim() !="")
				{
					if ( strDLNo.Trim().Length > 19 )
					{
						sbError.Append("<br>DL length is greater than 19");
					}
					if ( strDLNo.Trim().Length < 19 )
					{
						strDLNo= strDLNo.PadRight( 19,' '); 
					}
				}
				else
				{
					strDLNo=strDLNo.PadRight( 19,' '); 
				}
				//last name 
				if (strLastName.Trim() !="")
				{
					if ( strLastName.Trim().Length > 16 )
					{
						sbError.Append("<br>Last name length is greater than 20");
					}
					if ( strLastName.Trim().Length < 16 )
					{
						strLastName=strLastName.PadRight( 16,' '); 
					}
				}
				else
				{
					strLastName=strLastName.PadRight( 16,' '); 
				}
				
				//first name 
				if (strFirstName.Trim() !="")
				{
					if ( strFirstName.Trim().Length > 13 )
					{
						sbError.Append("<br>first name length is greater than 13");
					}
					if ( strFirstName.Trim().Length < 13 )
					{
						strFirstName=strFirstName.PadRight( 13,' '); 
					}
				}
				else
				{
					strFirstName=strFirstName.PadRight( 13,' '); 
				}
				//Suffix name 
				if (strSuffix.Trim() !="")
				{
					if ( strSuffix.Trim().Length > 3)
					{
						sbError.Append("<br>Suffix length is greater than 3");
					}
					if ( strSuffix.Trim().Length < 3 )
					{
						strSuffix=strSuffix.PadRight( 3,' '); 
					}
				}
				else
				{
					strSuffix=strSuffix.PadRight( 3,' '); 
				}
				//first name 
				if (strMiddleName.Trim() !="")
				{
					if ( strMiddleName.Trim().Length > 15 )
					{
						sbError.Append("<br>Middle name length is greater than 15");
					}
					if ( strMiddleName.Trim().Length < 15 )
					{
						strMiddleName=strMiddleName.PadRight( 15,' '); 
					}
				}
				else
				{
					strMiddleName=strMiddleName.PadRight( 15,' '); 
				}
				//dateofbirth
				if (strDateOfBirth.Trim() !="")
				{
					//done right now to test --- need to uncoment the line below !! 
					//strDateOfBirth="";
					if (strDateOfBirth.Trim().Length > 8 )
					{
						sbError.Append("<br>Date Of Birth length is greater than 8");
					}
					if (strDateOfBirth.Trim().Length < 8 )
					{
						strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
					}
				}
				else
				{
					strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
				}
				//Gender
				if (strGender.Trim() !="")
				{
					if (strGender.Trim().Length > 1 )
					{
						sbError.Append("<br>Gender length is greater than 1");
					}
					if (strGender.Trim().Length < 1 )
					{
						strGender=strGender.PadRight( 1,' '); 
					}
				}
				else
				{
					strGender=strGender.PadRight( 1,' '); 
				}
				//client code
				if (strClientCode.Trim() !="")
				{
					if (strClientCode.Trim().Length > 8 )
					{
						sbError.Append("<br>Gender length is greater than 8");
					}
					if (strClientCode.Trim().Length < 8 )
					{
						strClientCode=strClientCode.PadRight( 8,' '); 
					}
				}
				else
				{
					strClientCode=strClientCode.PadRight( 8,' '); 
				}
				
		
				//---------------------------------------------------
				

			
				sbRequest = new StringBuilder();
	

				sbRequest.Append(strFirstName);//firstname,15

				sbRequest.Append(" ");	//middlename,initial
				sbRequest.Append(strLastName);//last name,20
				sbRequest.Append(strDateOfBirth);//date_of_birth,8,MMDDCCYY format
				sbRequest.Append(strGender);//Gender,1
				sbRequest.Append(strDLNo.ToUpper());//dl_number,19
				string strSSN=objDriverInfo.DRIVER_SSN.ToString();
				strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(strSSN);
				strSSN=strSSN.Replace("-","");
				sbRequest.Append(strSSN.PadRight(9));//dl_number,19
				

				string strRequest = sbRequest.ToString();
				
			
				return strRequest;

			}
			catch (Exception ex )
			{
				throw (ex);
			}
		}
		
		public string GetUndisclosedPolDriver(string strState,string strDLNo, string strLastName, string strFirstName, string strSuffix,string strMiddleName,string strDateOfBirth,string strGender,string strClientCode,Cms.Model.Policy.ClsPolicyDriverInfo objPolDriverInfo)
		{
			Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo=new Cms.Model.Application.ClsDriverDetailsInfo();
			objDriverInfo.CUSTOMER_ID = objPolDriverInfo.CUSTOMER_ID;
			objDriverInfo.DRIVER_ADD1  = objPolDriverInfo.DRIVER_ADD1;
			objDriverInfo.DRIVER_ADD2  = objPolDriverInfo.DRIVER_ADD2;
			objDriverInfo.DRIVER_CITY = objPolDriverInfo.DRIVER_CITY ;
			objDriverInfo.DRIVER_DOB =  objPolDriverInfo.DRIVER_DOB;
			string strSSN=objPolDriverInfo.DRIVER_SSN.ToString();
			strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(strSSN);
			objDriverInfo.DRIVER_SSN  =  strSSN.Replace("-",""); //objPolDriverInfo.DRIVER_SSN;
			objDriverInfo.DRIVER_ZIP   =  objPolDriverInfo.DRIVER_ZIP;
 			return GetUndisclosedDriver( strState, strDLNo,  strLastName,  strFirstName,  strSuffix, strMiddleName, strDateOfBirth, strGender, strClientCode,objDriverInfo);
		}
		public string GetUndisclosedDriver(string strState,string strDLNo, string strLastName, string strFirstName, string strSuffix,string strMiddleName,string strDateOfBirth,string strGender,string strClientCode,Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo)
		{
			XmlDocument objResult= new XmlDocument();  
			System.Xml.XmlElement  objElement = null;
			sbError = new StringBuilder();

			objElement = objResult.CreateElement("ResultData");
			objResult.AppendChild (objElement); 

			try
			{
				//com.iix.expressnet.auth objProxy =  new com.iix.expressnet.auth();
				//ClsIIXProxy objProxy = new ClsIIXProxy();
				ClsIIXProxy objProxy = new ClsIIXProxy(strUrl);
				ClsRequestResponseLogInfo objLogInfo = new ClsRequestResponseLogInfo();
				checkIIXSettings();
				
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				if (strState.Trim() !="")
				{
					if ( strState.Trim().Length > 2 )
					{
						sbError.Append("<br>State code length is greater than 2 .");
					}
					if ( strState.Trim().Length < 2 )
					{
						strState=strState.PadRight( 2,' '); 
					}
				}
				else
				{
					strState= strState.PadRight( 2,' '); 
				}
				//dl 
				if (strDLNo.Trim() !="")
				{
					if ( strDLNo.Trim().Length > 19 )
					{
						sbError.Append("<br>DL length is greater than 19");
					}
					if ( strDLNo.Trim().Length < 19 )
					{
						strDLNo= strDLNo.PadRight( 19,' '); 
					}
				}
				else
				{
					strDLNo=strDLNo.PadRight( 19,' '); 
				}
				//last name 
				if (strLastName.Trim() !="")
				{
					if ( strLastName.Trim().Length > 16 )
					{
						sbError.Append("<br>Last name length is greater than 20");
					}
					if ( strLastName.Trim().Length < 16 )
					{
						strLastName=strLastName.PadRight( 16,' '); 
					}
				}
				else
				{
					strLastName=strLastName.PadRight( 16,' '); 
				}
				
				//first name 
				if (strFirstName.Trim() !="")
				{
					if ( strFirstName.Trim().Length > 13 )
					{
						sbError.Append("<br>first name length is greater than 13");
					}
					if ( strFirstName.Trim().Length < 13 )
					{
						strFirstName=strFirstName.PadRight( 13,' '); 
					}
				}
				else
				{
					strFirstName=strFirstName.PadRight( 13,' '); 
				}
				//Suffix name 
				if (strSuffix.Trim() !="")
				{
					if ( strSuffix.Trim().Length > 3)
					{
						sbError.Append("<br>Suffix length is greater than 3");
					}
					if ( strSuffix.Trim().Length < 3 )
					{
						strSuffix=strSuffix.PadRight( 3,' '); 
					}
				}
				else
				{
					strSuffix=strSuffix.PadRight( 3,' '); 
				}
				//first name 
				if (strMiddleName.Trim() !="")
				{
					if ( strMiddleName.Trim().Length > 15 )
					{
						sbError.Append("<br>Middle name length is greater than 15");
					}
					if ( strMiddleName.Trim().Length < 15 )
					{
						strMiddleName=strMiddleName.PadRight( 15,' '); 
					}
				}
				else
				{
					strMiddleName=strMiddleName.PadRight( 15,' '); 
				}
				//dateofbirth
				if (strDateOfBirth.Trim() !="")
				{
					//done right now to test --- need to uncoment the line below !! 
					//strDateOfBirth="";
					if (strDateOfBirth.Trim().Length > 8 )
					{
						sbError.Append("<br>Date Of Birth length is greater than 8");
					}
					if (strDateOfBirth.Trim().Length < 8 )
					{
						strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
					}
				}
				else
				{
					strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
				}
				//Gender
				if (strGender.Trim() !="")
				{
					if (strGender.Trim().Length > 1 )
					{
						sbError.Append("<br>Gender length is greater than 1");
					}
					if (strGender.Trim().Length < 1 )
					{
						strGender=strGender.PadRight( 1,' '); 
					}
				}
				else
				{
					strGender=strGender.PadRight( 1,' '); 
				}
				//client code
				if (strClientCode.Trim() !="")
				{
					if (strClientCode.Trim().Length > 8 )
					{
						sbError.Append("<br>Gender length is greater than 8");
					}
					if (strClientCode.Trim().Length < 8 )
					{
						strClientCode=strClientCode.PadRight( 8,' '); 
					}
				}
				else
				{
					strClientCode=strClientCode.PadRight( 8,' '); 
				}
				//Zip code
				string strZIP=objDriverInfo.DRIVER_ZIP.ToString().Replace("-","");
				if (strZIP.Trim() !="")
				{
					if (strZIP.Trim().Length >9 )
					{
						sbError.Append("<br>ZIP length is greater than 9");
					}
					if (strZIP.Trim().Length < 9 )
					{
						strZIP=strZIP.PadRight(9,' '); 
					}
				}
				else
				{
					strZIP=strZIP.PadRight( 9,' '); 
				}
				//
				string strSSN=objDriverInfo.DRIVER_SSN.ToString();
				strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(strSSN);
				strSSN=strSSN.Replace("-","");  
		
				if (strSSN.Trim() !="")
				{
					if (strSSN.Trim().Length >9 )
					{
						sbError.Append("<br>SSN length is greater than 9");
					}
					if (strSSN.Trim().Length < 9 )
					{
						strSSN=strSSN.PadRight(9,' '); 
					}
				}
				else
				{
					strSSN=strSSN.PadRight( 9,' '); 
				}
				string strCity=objDriverInfo.DRIVER_CITY.ToString();
				if (strCity.Trim() !="")
				{
					if (strCity.Trim().Length >20 )
					{
						sbError.Append("<br>City length is greater than 20");
					}
					if (strCity.Trim().Length < 20 )
					{
						strCity=strCity.PadRight(20,' '); 
					}
				}
				else
				{
					strCity=strCity.PadRight( 20,' '); 
				}
				
				string strADR=objDriverInfo.DRIVER_ADD1 + ' ' + objDriverInfo.DRIVER_ADD2;
				if (strADR.Trim() !="")
				{
					if (strADR.Trim().Length >45)
					{
						strADR=objDriverInfo.DRIVER_ADD1.ToString(); 
						if (strADR.Trim().Length >45)
						{	
							sbError.Append("<br>Address length is greater than 45");
						}
						if (strADR.Trim().Length < 45)
						{
							strADR=strADR.PadRight(45,' '); 
						}
					}
					if (strADR.Trim().Length < 45)
					{
						strADR=strADR.PadRight(45,' '); 
					}
				}
				else
				{
					strADR=strADR.PadRight( 45,' '); 
				}
				
				
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				//---------------------------------------------------
				sbRequest = new StringBuilder();
				strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode("UDI");

				sbRequest.Append(strUserName);//User name,3
				sbRequest.Append(strPassword);//User password,10
				sbRequest.Append(strAccountNumber);//Account,6
				sbRequest.Append(strBillCode);//BillCode,3
				sbRequest.Append("UDI");//Product,3
				//sbRequest.Append((objDriverInfo.DRIVER_ADD1 + ' ' + objDriverInfo.DRIVER_ADD2).PadRight(45,' '));//julian_date,3,optional zero fill if not used
				sbRequest.Append(strADR);//julian_date,3,optional zero fill if not used
				sbRequest.Append(strCity);//julian_date,3,optional zero fill if not used
				//sbRequest.Append("I");//order_purpose,1
				sbRequest.Append(strState);//State,2,post office state code
				//sbRequest.Append(objDriverInfo.DRIVER_ZIP.PadRight(9));//julian_date,3,optional zero fill if not used
				sbRequest.Append(strZIP);//julian_date,3,optional zero fill if not used
				sbRequest.Append(pad.PadRight(40,' '));	//julian_date,3,optional zero fill if not used
				sbRequest.Append(strFirstName);//firstname,13

				sbRequest.Append(" ");	//middlename,initial
				sbRequest.Append(strLastName);//last name,16
				sbRequest.Append(strDateOfBirth);//date_of_birth,8,MMDDCCYY format
				sbRequest.Append(strGender);//Gender,1
				sbRequest.Append(strDLNo.ToUpper());//dl_number,19
				//sbRequest.Append(objDriverInfo.DRIVER_SSN.PadRight(9));//ssn no,9
				sbRequest.Append(strSSN);//ssn no,9

				string strRequest = sbRequest.ToString();
				//-------------------------------------------
				if ( strRequest.Length != 208 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg","The request string length for IIX web service should be 208. The current length is: " + strRequest.Length.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
				//Set the Log Model Object Values
				objLogInfo.CUSTOMER_ID = objDriverInfo.CUSTOMER_ID;
				objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERTOKEN);
				objLogInfo.SERVICE_VENDOR = "IIX";
				objLogInfo.REQUEST_DATETIME = DateTime.Now;
				objLogInfo.IIX_REQUEST=strRequest;
				//Send the request			  
				objProxy.SetLogModel = objLogInfo;
				string strWebRequest = 	objProxy.sendRequest(strRequest);
			
				if ( strWebRequest.StartsWith("Error"))
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",strWebRequest);
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
			
				string strResponse = "";
				string strData = "";
				//Response received
				if ( strWebRequest.StartsWith("Accept"))
				{
					int i;
					int count=0;
					string Record=""; 
					Hashtable  objArrRecords = new Hashtable();
					strResponse = strRequest.Substring(0,25) + strWebRequest.Substring(7);
					//Set the Log Model Object Values
					objLogInfo.CUSTOMER_ID = objDriverInfo.CUSTOMER_ID;
					objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERDATA);
					objLogInfo.SERVICE_VENDOR = "IIX";
					objLogInfo.REQUEST_DATETIME = DateTime.Now;
					objLogInfo.IIX_RESPONSE=strResponse;
					//get the response	  
					objProxy.SetLogModel = objLogInfo;
					for (int j = 0; j < 5; j++)		// wait 5 seconds and try 5 times
					{
						strData = objProxy.getResponse(strResponse);
						if ( strData.IndexOf("Error:Response not yet available")==-1)
						{
							break; 
						}
						else if(strData.IndexOf("Error:Response not yet available")!=-1 && j==4)
						{
							objElement =objResult.CreateElement("Error" );
							objElement.SetAttribute("Msg",strData);
							objResult.DocumentElement.AppendChild(objElement);
							return objResult.OuterXml;
						}
						System.Threading.Thread.Sleep(5000);
					}
					//Removing the common header
					string SubResponse = strData.Substring(108);
					//Retreiving the number of drivers
					int NoOfDrivers = int.Parse(SubResponse.Substring(9,2));
					int Index = 0;
					count=0;
					for ( i = 0; i < NoOfDrivers; i++) 
					{
						objElement =objResult.CreateElement("Driver" );
						//test =Record.Substring(13,6);
						objElement.SetAttribute("name", SubResponse.Substring(51 + Index, 30)); 
						//test =Record.Substring(21,8);
						objElement.SetAttribute("gender", SubResponse.Substring(81 + Index, 1)); 
						//test =Record.Substring(41,38);
						objElement.SetAttribute("dateOfBirth", SubResponse.Substring(82 + Index, 8));
						//test =Record.Substring(79,10);
						objElement.SetAttribute("height", SubResponse.Substring(90 + Index, 4));  
						//test =Record.Substring(90,3);
						objElement.SetAttribute("weight", SubResponse.Substring(94 + Index, 3)); 
						//test =Record.Substring(93,10);
						objElement.SetAttribute("license", SubResponse.Substring(97 + Index, 25));  
						objElement.SetAttribute("ssn", SubResponse.Substring(122 + Index, 9));  
						//test =Record.Substring(103,3);
						objElement.SetAttribute("phone", SubResponse.Substring(131 + Index, 10));  
						objElement.SetAttribute("ext", SubResponse.Substring(141 + Index, 5));  
						
						objResult.DocumentElement.AppendChild(objElement);

						Index = Index + 95;
					}
					
				}
				return objResult.OuterXml ; 

			}
			catch (Exception ex )
			{
				objElement =objResult.CreateElement("Error" );
				objElement.SetAttribute("Msg",ex.ToString());
				objResult.DocumentElement.AppendChild(objElement);
				return objResult.OuterXml;
			}
		}

		#region UDV
		//Added by Mohit Agarwal 16-Jul-2007
		public string GetUndisclosedDriverVehicle(string strBillCode, string strProduct,Cms.Model.Client.ClsCustomerInfo objCustomerInfo, ref string strWebResponse)
		{
			XmlDocument objResult= new XmlDocument();  
			System.Xml.XmlElement  objElement = null;
			sbError = new StringBuilder();

			objElement = objResult.CreateElement("ResultData");
			objResult.AppendChild (objElement); 

			#region request variables
			string userName="", password="", strAccount="", BillCode="", Product="";
			string HouseNo="", StreetDir="", StreetName="", StreetType="", AptNo="";
			string City="", State="", Zip="", QuoteBack="Wolverine";
			string LastName="", FirstName="", MiddleName="", Suffix="", Gender="";
			string DOB="", SSN="", LicState="", LicNum = "";

			#endregion
			try
			{
				//com.iix.expressnet.auth objProxy =  new com.iix.expressnet.auth();
				//ClsIIXProxy objProxy = new ClsIIXProxy();
				ClsIIXProxy objProxy = new ClsIIXProxy(strUrl);
				ClsRequestResponseLogInfo objLogInfo = new ClsRequestResponseLogInfo();
				checkIIXSettings();
				
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				userName = UserName;
				if (userName.Trim() !="")
				{
					if ( userName.Trim().Length > 3 )
					{
						sbError.Append("<br>User Name length is greater than 3.");
					}
					if ( userName.Trim().Length < 3 )
					{
						userName=userName.PadRight( 3,' '); 
					}
				}
				else
				{
					userName= userName.PadRight( 3,' '); 
				}
				//password
				password = Password;

				if (password.Trim() !="")
				{
					if ( password.Trim().Length > 10 )
					{
						sbError.Append("<br>Password length is greater than 10");
					}
					if ( password.Trim().Length < 10 )
					{
						password= password.PadRight( 10,' '); 
					}
				}
				else
				{
					password=password.PadRight( 10,' '); 
				}

				//Account 
				strAccount = AccountNumber;
				if (strAccount.Trim() !="")
				{
					if ( strAccount.Trim().Length > 6 )
					{
						sbError.Append("<br>Account length is greater than 6");
					}
					if ( strAccount.Trim().Length < 6 )
					{
						strAccount=strAccount.PadRight( 6,' '); 
					}
				}
				else
				{
					strAccount=strAccount.PadRight( 6,' '); 
				}
				
				//BillCode
				BillCode= strBillCode;
				if (BillCode.Trim() !="")
				{
					if ( BillCode.Trim().Length > 3 )
					{
						sbError.Append("<br>Bill Code length is greater than 3");
					}
					if ( BillCode.Trim().Length < 3 )
					{
						BillCode=BillCode.PadRight( 3,' '); 
					}
				}
				else
				{
					BillCode=BillCode.PadRight( 3,' '); 
				}

				//Product
				Product = "UDV";
				if (Product.Trim() !="")
				{
					if ( Product.Trim().Length > 3)
					{
						sbError.Append("<br>Product length is greater than 3");
					}
					if ( Product.Trim().Length < 3 )
					{
						Product=Product.PadRight( 3,' '); 
					}
				}
				else
				{
					Product=Product.PadRight( 3,' '); 
				}
				HouseNo="1";
				Cms.CmsWeb.webcontrols.AddressDetails AddressDetails= this.CreateAddressDetails(objCustomerInfo.CustomerAddress1,objCustomerInfo.CustomerCity,objCustomerInfo.CustomerStateCode,objCustomerInfo.CustomerZip);
				if (AddressDetails.Status=="999" || AddressDetails.RecCount=="0")
				{
					sbError.Append("ADDRESS_NOT_VALIDATED :Address Could not be validate.");
					throw new Exception(sbError.ToString());
				}
				HouseNo=AddressDetails.AddressProperty[0].Number.ToString();
				AptNo=AddressDetails.AddressProperty[0].Unit.ToString();
				StreetName=AddressDetails.AddressProperty[0].Street.ToString();
				//HouseNo 
				if (HouseNo.Trim() !="")
				{
					if ( HouseNo.Trim().Length > 10 )
					{
						//sbError.Append("<br>HouseNo length is greater than 10");
						HouseNo = HouseNo.Substring(0, 10);
					}
					if ( HouseNo.Trim().Length < 10 )
					{
						HouseNo=HouseNo.PadRight( 10,' '); 
					}
				}
				else
				{
					HouseNo=HouseNo.PadRight( 10,' '); 
				}

				//StreetDir
				if (StreetDir.Trim() !="")
				{
					//done right now to test --- need to uncoment the line below !! 
					//strDateOfBirth="";
					if (StreetDir.Trim().Length > 2 )
					{
						//sbError.Append("<br>Street Direction length is greater than 2");
						StreetDir = StreetDir.Substring(0,2);
					}
					if (StreetDir.Trim().Length < 2 )
					{
						StreetDir=StreetDir.PadRight( 2,' '); 
					}
				}
				else
				{
					StreetDir=StreetDir.PadRight( 2,' '); 
				}

				//StreetName
				if (StreetName.Trim() !="")
				{
					if (StreetName.Trim().Length > 30 )
					{
						//sbError.Append("<br>Street Name length is greater than 30");
						StreetName = StreetName.Substring(0,10);
					}
					if (StreetName.Trim().Length < 30 )
					{
						StreetName=StreetName.PadRight( 30,' '); 
					}
				}
				else
				{
					StreetName=StreetName.PadRight( 30,' '); 
				}

				//StreetType
				if (StreetType.Trim() !="")
				{
					if (StreetType.Trim().Length > 4 )
					{
						//sbError.Append("<br>Street Type length is greater than 4");
						StreetType = StreetType.Substring(0,4);
					}
					if (StreetType.Trim().Length < 4 )
					{
						StreetType=StreetType.PadRight( 4,' '); 
					}
				}
				else
				{
					StreetType=StreetType.PadRight( 4,' '); 
				}

				//AptNo
				if (AptNo.Trim() !="")
				{
					if (AptNo.Trim().Length > 5 )
					{
						//sbError.Append("<br>Apt No. length is greater than 5");
						AptNo = AptNo.Substring(0,5);
					}
					if (AptNo.Trim().Length < 5 )
					{
						AptNo=AptNo.PadRight( 5,' '); 
					}
				}
				else
				{
					AptNo=AptNo.PadRight( 5,' '); 
				}

				//City
				City=objCustomerInfo.CustomerCity;
				if (City.Trim() !="")
				{
					if (City.Trim().Length >20 )
					{
						//sbError.Append("<br>City length is greater than 20");
						City = City.Substring(0,20);
					}
					if (City.Trim().Length < 20 )
					{
						City=City.PadRight(20,' '); 
					}
				}
				else
				{
					City=City.PadRight( 20,' '); 
				}

				//State code
				State=objCustomerInfo.CustomerStateCode;
				if (State.Trim() !="")
				{
					if (State.Trim().Length >2 )
					{
						//sbError.Append("<br>State length is greater than 2");
						State = State.Substring(0,2);
					}
					if (State.Trim().Length < 2 )
					{
						State=State.PadRight(2,' '); 
					}
				}
				else
				{
					State=State.PadRight( 2,' '); 
				}

				//Zip code
				Zip=objCustomerInfo.CustomerZip.ToString().Replace("-","");
				if (Zip.Trim() !="")
				{
					if (Zip.Trim().Length >9 )
					{
						//sbError.Append("<br>ZIP length is greater than 9");
						Zip = Zip.Substring(0,9);
					}
					if (Zip.Trim().Length < 9 )
					{
						Zip=Zip.PadRight(9,' '); 
					}
				}
				else
				{
					Zip=Zip.PadRight( 9,' '); 
				}

				//QuoteBack
				if (QuoteBack.Trim() !="")
				{
					if (QuoteBack.Trim().Length > 40 )
					{
						//sbError.Append("<br>QuoteBack length is greater than 40");
						QuoteBack = QuoteBack.Substring(0,40);
					}
					if (QuoteBack.Trim().Length < 40 )
					{
						QuoteBack=QuoteBack.PadRight( 40,' '); 
					}
				}
				else
				{
					QuoteBack=QuoteBack.PadRight( 40,' '); 
				}

				//Last Name code
				LastName=objCustomerInfo.CustomerLastName;
				if (LastName.Trim() !="")
				{
					if (LastName.Trim().Length >16 )
					{
						//sbError.Append("<br>Last Name length is greater than 16");
						LastName = LastName.Substring(0,16);
					}
					if (LastName.Trim().Length < 16 )
					{
						LastName=LastName.PadRight(16,' '); 
					}
				}
				else
				{
					LastName=LastName.PadRight( 16,' '); 
				}

				//First Name code
				FirstName=objCustomerInfo.CustomerFirstName;
				if (FirstName.Trim() !="")
				{
					if (FirstName.Trim().Length >13 )
					{
						//sbError.Append("<br>First Name length is greater than 13");
						FirstName = FirstName.Substring(0,13);
					}
					if (FirstName.Trim().Length < 13 )
					{
						FirstName=FirstName.PadRight(13,' '); 
					}
				}
				else
				{
					FirstName=FirstName.PadRight( 13,' '); 
				}

				//Middle Name code
				if(objCustomerInfo.CustomerMiddleName != "")
					MiddleName=objCustomerInfo.CustomerMiddleName.Substring(0,1);
				if (MiddleName.Trim() !="")
				{
					if (MiddleName.Trim().Length >1 )
					{
						//sbError.Append("<br>Middle Name length is greater than 1");
						MiddleName = MiddleName.Substring(0,1);
					}
					if (MiddleName.Trim().Length < 1 )
					{
						MiddleName=MiddleName.PadRight(1,' '); 
					}
				}
				else
				{
					MiddleName=MiddleName.PadRight( 1,' '); 
				}

				//Suffix 
				Suffix=objCustomerInfo.CustomerSuffix;
				if (Suffix.Trim() !="")
				{
					if (Suffix.Trim().Length >4 )
					{
						//sbError.Append("<br>Suffix length is greater than 4");
						Suffix = Suffix.Substring(0,4);
					}
					if (Suffix.Trim().Length < 4 )
					{
						Suffix=Suffix.PadRight(4,' '); 
					}
				}
				else
				{
					Suffix=Suffix.PadRight( 4,' '); 
				}

				//Gender 
				if(objCustomerInfo.GENDER != "")
					Gender=objCustomerInfo.GENDER.Substring(0,1);
				if (Gender.Trim() !="")
				{
					if (Gender.Trim().Length >1 )
					{
						//sbError.Append("<br>Gender length is greater than 1");
						Gender = Gender.Substring(0,1);
					}
					if (Gender.Trim().Length < 1 )
					{
						Gender=Gender.PadRight(1,' '); 
					}
				}
				else
				{
					Gender=Gender.PadRight( 1,' '); 
				}

				//DOB 
				DOB=objCustomerInfo.DATE_OF_BIRTH.ToString("yyyyMMdd");
				if (DOB.Trim() !="")
				{
					if (DOB.Trim().Length >8 )
					{
						//sbError.Append("<br>Date of Birth length is greater than 8");
						DOB = DOB.Substring(0,8);
					}
					if (DOB.Trim().Length < 8 )
					{
						DOB=DOB.PadRight(8,' '); 
					}
				}
				else
				{
					DOB=DOB.PadRight( 8,' '); 
				}

				//SSN code
				SSN=objCustomerInfo.SSN_NO.ToString();
				SSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(SSN);
				SSN=SSN.ToString().Replace("-","");

				if (SSN.Trim() !="")
				{
					if (SSN.Trim().Length >9 )
					{
						//sbError.Append("<br>SSN length is greater than 9");
						SSN = SSN.Substring(0,9);
					}
					if (SSN.Trim().Length < 9 )
					{
						SSN=SSN.PadRight(9,' '); 
					}
				}
				else
				{
					SSN=SSN.PadRight( 9,' '); 
				}

				//LicState
				if (LicState.Trim() !="")
				{
					if (LicState.Trim().Length >2 )
					{
						//sbError.Append("<br>License State length is greater than 2");
						LicState = LicState.Substring(0,2);
					}
					if (LicState.Trim().Length < 2 )
					{
						LicState=LicState.PadRight(2,' '); 
					}
				}
				else
				{
					LicState=LicState.PadRight( 2,' '); 
				}

				//LicNum
				if (LicNum.Trim() !="")
				{
					if (LicNum.Trim().Length >19 )
					{
						//sbError.Append("<br>License Number length is greater than 19");
						LicNum = LicNum.Substring(0,19);
					}
					if (LicNum.Trim().Length < 19 )
					{
						LicNum=LicNum.PadRight(19,' '); 
					}
				}
				else
				{
					LicNum=LicNum.PadRight( 19,' '); 
				}


				
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				//---------------------------------------------------

				sbRequest = new StringBuilder();
				strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode(Product);

				sbRequest.Append(userName);//User name,3
				sbRequest.Append(password);//User password,10
				sbRequest.Append(strAccount);//Account,6
				//sbRequest.Append(BillCode);//BillCode,3
				sbRequest.Append(strBillCode);//BillCode,3
				sbRequest.Append(Product);//Product,3
				
				sbRequest.Append(HouseNo);//
				sbRequest.Append(StreetDir);//
				sbRequest.Append(StreetName);//
				sbRequest.Append(StreetType);//
				sbRequest.Append(AptNo);//
				sbRequest.Append(City);//
				sbRequest.Append(State);//
				sbRequest.Append(Zip);//
				sbRequest.Append(QuoteBack);//
				sbRequest.Append(LastName);//
				sbRequest.Append(FirstName);//
				sbRequest.Append(MiddleName);//
				sbRequest.Append(Suffix);//
				sbRequest.Append(Gender);//
				sbRequest.Append(DOB);//
				sbRequest.Append(SSN);//
				sbRequest.Append(LicState);//
				sbRequest.Append(LicNum);//

				string strRequest = sbRequest.ToString();
				//-------------------------------------------
				if ( strRequest.Length != 220 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg","The request string length for IIX web service should be 220. The current length is: " + strRequest.Length.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
				//Set the Log Model Object Values
				objLogInfo.CUSTOMER_ID = objCustomerInfo.CustomerId;
				objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERTOKEN);
				objLogInfo.SERVICE_VENDOR = "IIX";
				objLogInfo.REQUEST_DATETIME = DateTime.Now;
				objLogInfo.IIX_REQUEST=strRequest;
				//Send the request			  
				objProxy.SetLogModel = objLogInfo;
				string strWebRequest = 	objProxy.sendRequest(strRequest);
			
				strWebResponse = strWebRequest;
				if ( strWebRequest.StartsWith("Error"))
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",strWebRequest);
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
			
				string strResponse = "";
				string strData = "";
				//Response received
				if ( strWebRequest.StartsWith("Accept"))
				{
					int i;
					int count=0;
					string Record=""; 
					Hashtable  objArrRecords = new Hashtable();
					strResponse = strRequest.Substring(0,25) + strWebRequest.Substring(7);
					//Set the Log Model Object Values
					objLogInfo.CUSTOMER_ID = objCustomerInfo.CustomerId;
					objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERDATA);
					objLogInfo.SERVICE_VENDOR = "IIX";
					objLogInfo.REQUEST_DATETIME = DateTime.Now;
					objLogInfo.IIX_RESPONSE=strResponse;
					//get the response	  
					objProxy.SetLogModel = objLogInfo;
					//get the response
					for (int j = 0; j < 5; j++)		// wait 5 seconds and try 5 times
					{
						strData = objProxy.getResponse(strResponse);
						if (strData.IndexOf("Error:Response not yet available")==-1)
						{
							break; 
						}
						else if(strData.IndexOf("Error:Response not yet available")!=-1 && j==4)
						{
							objElement =objResult.CreateElement("Error" );
							objElement.SetAttribute("Msg",strData);//strWebRequest);
							objResult.DocumentElement.AppendChild(objElement);
							return objResult.OuterXml;
						}
						System.Threading.Thread.Sleep(5000);
					}
					if(strData.Length < 234)
					{
						objElement =objResult.CreateElement("Error" );
						//objElement.SetAttribute("Msg","The response string length from IIX web service is less than 492. The current length is: " + strData.Length.ToString());
						objElement.SetAttribute("Msg","No response found from IIX.");
						objResult.DocumentElement.AppendChild(objElement);
						return objResult.OuterXml;
						
					}
					////
					//Removing the common header
					string SubResponse = strData.Substring(108,126);
					//Retreiving the number of drivers
					int NoOfDrivers=0, NoOfVehicles=0;
					try{ NoOfDrivers = int.Parse(SubResponse.Substring(122,2));	} catch{}
					try{ NoOfVehicles = int.Parse(SubResponse.Substring(124,2)); } catch{}
					int Index = 0;
					count=0;

					StringBuilder strBuilder = new StringBuilder();

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>IIX UNDISCLOSED DRIVER/VEHICLE REPORT</b></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='Left' colspan='5'></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='Left' colspan='3'>Report Date: " + DateTime.Now.ToShortDateString() + "</td>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='2'>" + strWebRequest.Substring(7) + "</td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='Left' colspan='5'></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>Report Prepared For</b></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='Left' colspan='5'>WOLVERINE MUTUAL INSURANCE CO</td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'>1 WOLVERINE WAY</td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'>DOWAGIAC, MI 49047</td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'></td>");
					strBuilder.Append("</tr>");


					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='Left' colspan='5'><b>Comment</b></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>This Report has been generated for insurance purposes only and it may not be used for any other</b></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>purpose. Although reasonable procedures have been adopted to maximize the accuracy of this report,</b></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>subscribers are asked to investigate independently and evaluate all relevant data.</b></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>Search Criteria</b></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='Left' colspan='5'>Name: " + objCustomerInfo.CustomerFirstName + " " + objCustomerInfo.CustomerLastName + " </td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'>Address: " + objCustomerInfo.CustomerAddress1 + ", " + objCustomerInfo.CustomerAddress2 + "</td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + objCustomerInfo.CustomerCity + ", " + objCustomerInfo.CustomerStateCode + " " + objCustomerInfo.CustomerZip + "</td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'></td>");
					strBuilder.Append("</tr>");

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>" + NoOfDrivers.ToString() + " DRIVERS FOUND" + "</b></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'></td>");
					strBuilder.Append("</tr>");

					if(NoOfDrivers > 0)
					{
							strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td width='40%' class='DataGridRow' align='left'><b>Name /DL State And Number</b></td>");
						strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>DOB</b></td>");
						strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Gender</b></td>");
						strBuilder.Append("<td width='10%' class='DataGridRow' align='left'><b>Age</b></td>");
						strBuilder.Append("<td width='20%' class='DataGridRow' align='left'><b>Phone Number</b></td>");
						strBuilder.Append("</tr>");
					}


					for ( i = 0; i < NoOfDrivers; i++) 
					{
						SubResponse = strData.Substring(108+126 + i*89, 89);

						string dob = SubResponse.Substring(35, 8);
						string dob_1 = dob.Substring(4,2)+ "/" + dob.Substring(6,2)+ "/" + dob.Substring(0,4);
						if(dob.Trim() == "")
							dob_1 = "";

						string phone = SubResponse.Substring(79, 10);
						string phone_1 = "(" + phone.Substring(0,3)+ ")" + phone.Substring(3,3)+ "-" + phone.Substring(6,3); 
						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td width='40%' class='DataGridRow' align='left'>" + SubResponse.Substring(16, 13) + " " + SubResponse.Substring(29, 1)+ " " + SubResponse.Substring(0, 16) +"</td>");
						strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + dob_1 + "</td>");
						strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>" + SubResponse.Substring(34, 1) + "</td>");
						strBuilder.Append("<td width='10%' class='DataGridRow' align='left'>" + SubResponse.Substring(43, 2) + "</td>");
						strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>"+phone_1+"</td>");
						strBuilder.Append("</tr>");
						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td width='40%' class='DataGridRow' align='left' colspan='5'>"+ SubResponse.Substring(45, 2) + SubResponse.Substring(47, 23) +"</td>");
						strBuilder.Append("</tr>");

						objElement =objResult.CreateElement("Driver" );
						//test =Record.Substring(13,6);
						objElement.SetAttribute("lname", SubResponse.Substring(0, 16)); 
						objElement.SetAttribute("name", SubResponse.Substring(16, 13)); 
						objElement.SetAttribute("mname", SubResponse.Substring(29, 1)); 
						objElement.SetAttribute("suffix", SubResponse.Substring(30, 4)); 
						//test =Record.Substring(21,8);
						objElement.SetAttribute("gender", SubResponse.Substring(34, 1)); 
						//test =Record.Substring(41,38);
						objElement.SetAttribute("dateOfBirth", SubResponse.Substring(35, 8));
						objElement.SetAttribute("age", SubResponse.Substring(43, 2));
						//test =Record.Substring(93,10);
						objElement.SetAttribute("dlstate", SubResponse.Substring(45, 2));  
						objElement.SetAttribute("license", SubResponse.Substring(47, 23));  
						objElement.SetAttribute("ssn", SubResponse.Substring(70, 9));  
						//test =Record.Substring(103,3);
						objElement.SetAttribute("phone", SubResponse.Substring(79, 10));  
						
						objResult.DocumentElement.AppendChild(objElement);

						//					Index = Index + 95;
					}

					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>" + NoOfVehicles.ToString() + " VEHICLES FOUND" + "</b></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='10%' class='DataGridRow' align='Left' colspan='5'></td>");
					strBuilder.Append("</tr>");

					if(NoOfVehicles > 0)
					{
							strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td width='40%' class='DataGridRow' align='left'><b>VIN</b></td>");
						strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>State</b></td>");
						strBuilder.Append("<td width='15%' class='DataGridRow' align='left'><b>Year</b></td>");
						strBuilder.Append("<td width='10%' class='DataGridRow' align='left'><b>Make</b></td>");
						strBuilder.Append("<td width='20%' class='DataGridRow' align='left'><b>Owner</b></td>");
						strBuilder.Append("</tr>");
					}

					string []owners = new string[NoOfVehicles];
					int NoOfOwners = 0;

					if(108+126 + i*89 + NoOfVehicles*27 < strData.Length)
					{
						try
						{
							NoOfOwners = int.Parse(strData.Substring(108+126 + i*89 + NoOfVehicles*27, 2));
							owners = new string[NoOfOwners];
							for(int ownindex = 0; ownindex < NoOfOwners; ownindex++)
							{
								SubResponse = strData.Substring(108+126 + i*89 + NoOfVehicles*27 + 101*ownindex+2, 101);
								string fName = SubResponse.Substring(31,14);
								string MInit = SubResponse.Substring(45,1);
								string lName = SubResponse.Substring(1,30);
								if(MInit.Trim() != "")
									fName += " " + MInit + ". " + lName;
								else
									fName += " " + lName;

								owners[ownindex] = fName;
							}
						}
						catch
						{
						}
					}
					for ( int j = 0; j < NoOfVehicles; j++) 
					{
						SubResponse = strData.Substring(108+126 + i*89 + j*27, 27);
						strBuilder.Append("<tr height='20'>");
						strBuilder.Append("<td width='40%' class='DataGridRow' align='left'>"+ SubResponse.Substring(0, 17) +"</td>");
						strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>"+ SubResponse.Substring(25, 2) + "</td>");
						strBuilder.Append("<td width='15%' class='DataGridRow' align='left'>"+ SubResponse.Substring(17, 4)+ "</td>");
						if(j < NoOfOwners)
						{
							strBuilder.Append("<td width='10%' class='DataGridRow' align='left'>"+ SubResponse.Substring(21, 4)+ "</td>");
							strBuilder.Append("<td width='20%' class='DataGridRow' align='left'>"+owners[j]+"</td>");
						}
						else
							strBuilder.Append("<td width='10%' class='DataGridRow' align='left' colspan='2'>"+ SubResponse.Substring(21, 4)+ "</td>");
						strBuilder.Append("</tr>");

						objElement =objResult.CreateElement("Vehicle" );
						//test =Record.Substring(13,6);
						objElement.SetAttribute("vin", SubResponse.Substring(0, 17)); 
						objElement.SetAttribute("year", SubResponse.Substring(17, 4)); 
						objElement.SetAttribute("make", SubResponse.Substring(21, 4)); 
						objElement.SetAttribute("state", SubResponse.Substring(25, 2)); 
						//test =Record.Substring(21,8);
					
						objResult.DocumentElement.AppendChild(objElement);

	//					Index = Index + 95;
					}
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>iiX is a Trademark of Insurance Information Exchange, College Station, TX.</b></td>");
					strBuilder.Append("</tr>");
					strBuilder.Append("<tr height='20'>");
					strBuilder.Append("<td width='9%' class='DataGridRow' align='center' colspan='5'><b>*** End Of Report ***</b></td>");
					strBuilder.Append("</tr>");
					return strBuilder.ToString();
				}
				return "IIX has rejected the UDV request.";
				//return objResult.OuterXml ; 

			}
			catch (Exception ex )
			{
				objElement =objResult.CreateElement("Error" );
				objElement.SetAttribute("Msg",ex.ToString());
				objResult.DocumentElement.AppendChild(objElement);
				return objResult.OuterXml;
			}
		}

		#endregion

		#region  
		/// <summary> /////by Pravesh Chandel on 6 dec 2006
		/// Parse the iix response and returns the motercycle amd auto loss
		/// </summary>
		/// <param name="IIxResponse">IIX Response return from iix service</param>
		/// <returns>Loss Report for Auto and MoterCycle </returns>

		public string GetAutoLoss(string strState,string strDLNo, string strLastName, string strFirstName, string strSuffix,string strMiddleName,string strDateOfBirth,string strGender,string strClientCode,Cms.Model.Application.ClsDriverDetailsInfo objDriverInfo, ref string strretWebResponse,string strAdditionalDrivers)
		{
			XmlDocument objResult= new XmlDocument();  
			System.Xml.XmlElement  objElement = null;
			sbError = new StringBuilder();

			objElement = objResult.CreateElement("ResultData");
			objResult.AppendChild (objElement); 

			try
			{
				//com.iix.expressnet.auth objProxy =  new com.iix.expressnet.auth();
				//ClsIIXProxy objProxy = new ClsIIXProxy();
				ClsIIXProxy objProxy = new ClsIIXProxy(strUrl);
				ClsRequestResponseLogInfo objLogInfo = new ClsRequestResponseLogInfo();
				checkIIXSettings();
				
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				if (strState.Trim() !="")
				{
					if ( strState.Trim().Length > 2 )
					{
						//sbError.Append("<br>State code length is greater than 2 .");
						strState = strState.Substring(0,2);
					}
					if ( strState.Trim().Length < 2 )
					{
						strState=strState.PadRight( 2,' '); 
					}
				}
				else
				{
					strState= strState.PadRight( 2,' '); 
				}
				//dl 
				if (strDLNo.Trim() !="")
				{
					if ( strDLNo.Trim().Length > 20 )
					{
						//sbError.Append("<br>DL length is greater than 19");
						strDLNo = strDLNo.Substring(0,20);
					}
					if ( strDLNo.Trim().Length < 20 )
					{
						strDLNo= strDLNo.PadRight( 20,' '); 
					}
				}
				else
				{
					strDLNo=strDLNo.PadRight( 20,' '); 
				}
				//last name 
				if (strLastName.Trim() !="")
				{
					if ( strLastName.Trim().Length > 20 )
					{
						//sbError.Append("<br>Last name length is greater than 20");
						strLastName = strLastName.Substring(0,20);
					}
					if ( strLastName.Trim().Length < 20 )
					{
						strLastName=strLastName.PadRight( 20,' '); 
					}
				}
				else
				{
					strLastName=strLastName.PadRight( 20,' '); 
				}
				
				//first name 
				if (strFirstName.Trim() !="")
				{
					if ( strFirstName.Trim().Length > 15 )
					{
						//sbError.Append("<br>first name length is greater than 15");
						strFirstName = strFirstName.Substring(0,15);
					}
					if ( strFirstName.Trim().Length < 15 )
					{
						strFirstName=strFirstName.PadRight( 15,' '); 
					}
				}
				else
				{
					strFirstName=strFirstName.PadRight( 15,' '); 
				}
				//Suffix name 
				if (strSuffix.Trim() !="")
				{
					if ( strSuffix.Trim().Length > 3)
					{
						//sbError.Append("<br>Suffix length is greater than 3");
						strSuffix = strSuffix.Substring(0,3);
					}
					if ( strSuffix.Trim().Length < 3 )
					{
						strSuffix=strSuffix.PadRight( 3,' '); 
					}
				}
				else
				{
					strSuffix=strSuffix.PadRight( 3,' '); 
				}
				//first name 
				if (strMiddleName.Trim() !="")
				{
					if ( strMiddleName.Trim().Length > 15 )
					{
						//sbError.Append("<br>Middle name length is greater than 15");
						strMiddleName = strMiddleName.Substring(0,15);
					}
					if ( strMiddleName.Trim().Length < 15 )
					{
						strMiddleName=strMiddleName.PadRight( 15,' '); 
					}
				}
				else
				{
					strMiddleName=strMiddleName.PadRight( 15,' '); 
				}
				//dateofbirth
				if (strDateOfBirth.Trim() !="")
				{
					//done right now to test --- need to uncoment the line below !! 
					//strDateOfBirth="";
					if (strDateOfBirth.Trim().Length > 8 )
					{
						//sbError.Append("<br>Date Of Birth length is greater than 8");
						strDateOfBirth = strDateOfBirth.Substring(0,8);
					}
					if (strDateOfBirth.Trim().Length < 8 )
					{
						strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
					}
				}
				else
				{
					strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
				}
				//Gender
				if (strGender.Trim() !="")
				{
					if (strGender.Trim().Length > 1 )
					{
						//sbError.Append("<br>Gender length is greater than 1");
						strGender = strGender.Substring(0,1);
					}
					if (strGender.Trim().Length < 1 )
					{
						strGender=strGender.PadRight( 1,' '); 
					}
				}
				else
				{
					strGender=strGender.PadRight( 1,' '); 
				}
				//client code

				if (strClientCode.Trim() !="")
				{
					if (strClientCode.Trim().Length > 8 )
					{
						//sbError.Append("<br>Gender length is greater than 8");
						strClientCode = strClientCode.Substring(0,8);
					}
					if (strClientCode.Trim().Length < 8 )
					{
						strClientCode=strClientCode.PadRight( 8,' '); 
					}
				}
				else
				{
					strClientCode=strClientCode.PadRight( 8,' '); 
				}
				//strpassword
				if (strPassword.Trim() !="")
				{
					if (strPassword.Trim().Length > 10 )
					{
						//sbError.Append("<br>Password length is greater than 10");
						strPassword = strPassword.Substring(0,10);
					}
					if (strPassword.Trim().Length < 10  )
					{
						strPassword=strPassword.PadRight( 10,' '); 
					}
				}
				else
				{
					strPassword=strPassword.PadRight( 10,' '); 
				}
		
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				
				string strSSN= "";
				if(objDriverInfo.DRIVER_SSN != null)
				{
					strSSN = objDriverInfo.DRIVER_SSN.ToString();
					strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(strSSN);
					strSSN=strSSN.ToString().Replace("-","");

				}
				if (strSSN.Trim() !="")
				{
					if (strSSN.Trim().Length >9 )
					{
						//sbError.Append("<br>SSN length is greater than 9");
						strSSN = strSSN.Substring(0,9);
					}
					if (strSSN.Trim().Length < 9 )
					{
						strSSN=strSSN.PadRight(9,' '); 
					}
				}
				else
				{
					strSSN=strSSN.PadRight( 9,' '); 
				}
				//driver Address
				string strAddress=objDriverInfo.DRIVER_ADD1.ToString();
				if (strAddress.Trim() !="")
				{
					if (strAddress.Trim().Length >48 )
					{
						strAddress = strAddress.Substring(0,48);
					}
					if (strAddress.Trim().Length < 48 )
					{
						strAddress=strAddress.PadRight(48,' '); 
					}
				}
				else
				{
					strAddress=strAddress.PadRight( 48,' '); 
				}
				//Driver City
				string strDriverCity=objDriverInfo.DRIVER_CITY.ToString();
				if (strDriverCity.Trim() !="")
				{
					if (strDriverCity.Trim().Length >22 )
					{
						strDriverCity = strDriverCity.Substring(0,22);
					}
					if (strDriverCity.Trim().Length < 22 )
					{
						strDriverCity=strDriverCity.PadRight(22,' '); 
					}
				}
				else
				{
					strDriverCity=strDriverCity.PadRight( 22,' '); 
				}
				//Driver Zip
				string strZip=objDriverInfo.DRIVER_ZIP.ToString();
				strZip=strZip.Replace("-","");
				if (strZip.Trim() !="")
				{
					if (strZip.Trim().Length >9 )
					{
						strZip = strZip.Substring(0,9);
					}
					if (strZip.Trim().Length < 9 )
					{
						strZip=strZip.PadRight(9,' '); 
					}
				}
				else
				{
					strZip=strZip.PadRight(9,' '); 
				}

				//---------------------------------------------------
				

			
				sbRequest = new StringBuilder();
				strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode("APA");

				sbRequest.Append(strUserName);//User name,3
				sbRequest.Append(strPassword);//User password,10
				sbRequest.Append(strAccountNumber);//Account,6
				sbRequest.Append(strBillCode);//BillCode,3
				//sbRequest.Append("A39");//BillCode,3
				sbRequest.Append("APA");//Product,3
				sbRequest.Append("U");//Risk Address
				sbRequest.Append("2");//Response Format
				sbRequest.Append(strLastName);//last name,20
				sbRequest.Append(strFirstName);//firstname,15
				sbRequest.Append(strMiddleName);	//middlename,initial
				sbRequest.Append(strSuffix);// Name Suffix,3
				sbRequest.Append(strSSN);//ssn,9
				//sbRequest.Append(pad.PadRight(9,' '));//ssn,9
				sbRequest.Append(strDateOfBirth);//date_of_birth,8,MMDDCCYY format
				sbRequest.Append(strDLNo.ToUpper());//dl_number,20
				sbRequest.Append(strState);//State,2,
				sbRequest.Append(pad.PadRight(20,' '));//Prior DL Number,   20
				sbRequest.Append(pad.PadRight(2,' '));//Prior DL State , 2

				sbRequest.Append(pad.PadRight(10,' '));//Home Telephone, 10
				sbRequest.Append(pad.PadRight(10,' '));//Work TelePhone , 10
				//sbRequest.Append(pad.PadRight(48,' '));//umparsed Street address,48
				sbRequest.Append(strAddress);//umparsed Street address,48
				/*for parsed street address
				sbRequest.Append(pad.PadRight(7,' '));//House Number,7
				sbRequest.Append(pad.PadRight(2,' '));//Street Direction,2
				sbRequest.Append(pad.PadRight(18,' '));//Street Name,18
				sbRequest.Append(pad.PadRight(4,' '));//Street Type,4	
				sbRequest.Append(pad.PadRight(5,' '));//Apartment No.,5	
				*/
                 sbRequest.Append(strDriverCity);//City 22
				//sbRequest.Append("NewYork" + pad.PadRight(15,' ') );//City 22
				sbRequest.Append(strState);//State,2,
				sbRequest.Append(strZip);//Zip 9
				///sbRequest.Append("46001"+pad.PadRight(4,' '));//julian_date,3,optional zero fill if not used
				//sbRequest.Append((objDriverInfo.DRIVER_ADD1 + ' ' + objDriverInfo.DRIVER_ADD2).PadRight(45,' '));//julian_date,3,optional zero fill if not used
				sbRequest.Append(pad.PadRight(40,' '));	//  Quoteback
				
				//sbRequest.Append(strGender);//Gender,1
				
				
				
				if(strAdditionalDrivers!="" && strAdditionalDrivers.Length>=116)
					sbRequest.Append(strAdditionalDrivers);

				string strRequest = sbRequest.ToString();
				//-------------------------------------------
				if ( strRequest.Length < 282 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg","The request string length for IIX web service should be 282. The current length is: " + strRequest.Length.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
			
				//Set the Log Model Object Values
				objLogInfo.CUSTOMER_ID = objDriverInfo.CUSTOMER_ID;
				objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERTOKEN);
				objLogInfo.SERVICE_VENDOR = "IIX";
				objLogInfo.REQUEST_DATETIME = DateTime.Now;
				objLogInfo.IIX_REQUEST=strRequest;
			
				//Send the request			  
				objProxy.SetLogModel = objLogInfo;
				string strWebRequest = 	objProxy.sendRequest(strRequest);
			
				strretWebResponse = strWebRequest;
				if ( strWebRequest.StartsWith("Error"))
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",strWebRequest);
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
			
				string strResponse = "";
				string strData = "";

				//Response received
				if ( strWebRequest.StartsWith("Accept"))
				{
					

					int i;
					int count=0;
					string Record=""; 
					Hashtable  objArrRecords = new Hashtable();
					strResponse = strRequest.Substring(0,25) + strWebRequest.Substring(7);

					//Set the Log Model Object Values
					objLogInfo.CUSTOMER_ID = objDriverInfo.CUSTOMER_ID;
					objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERDATA);
					objLogInfo.SERVICE_VENDOR = "IIX";
					objLogInfo.REQUEST_DATETIME = DateTime.Now;
					objLogInfo.IIX_RESPONSE		=strResponse;
			
					//get the response	  
					objProxy.SetLogModel = objLogInfo;
					for (int j = 0; j < 5; j++)		// wait 5 seconds and try 5 times
					{
						strData = objProxy.getResponse(strResponse);
						if (strData.IndexOf("Error:Response not yet available")==-1)
						{
							break; 
						}
						else if(strData.IndexOf("Error:Response not yet available")!=-1 && j==4)
						{
							objElement =objResult.CreateElement("Error" );
							objElement.SetAttribute("Msg",strData);
							objResult.DocumentElement.AppendChild(objElement);
							return objResult.OuterXml;
						}
						System.Threading.Thread.Sleep(5000);
					}
					//Removing the common header
					//string SubResponse = strData.Substring(108);
				
					//Retreiving the number of drivers
					//int NoOfDrivers = int.Parse(SubResponse.Substring(9,2));

					int Index = 0;
					/*
					count=0;
					for ( i = 0; i < NoOfDrivers; i++) 
					{

						//string test ="";
						objElement =objResult.CreateElement("Driver" );
						//test =Record.Substring(13,6);
						objElement.SetAttribute("name", SubResponse.Substring(51 + Index, 30)); 
						//test =Record.Substring(21,8);
						objElement.SetAttribute("gender", SubResponse.Substring(81 + Index, 1)); 
						//test =Record.Substring(41,38);
						objElement.SetAttribute("dateOfBirth", SubResponse.Substring(82 + Index, 8));
						//test =Record.Substring(79,10);
						objElement.SetAttribute("height", SubResponse.Substring(90 + Index, 4));  
						//test =Record.Substring(90,3);
						objElement.SetAttribute("weight", SubResponse.Substring(94 + Index, 3)); 
						//test =Record.Substring(93,10);
						objElement.SetAttribute("license", SubResponse.Substring(97 + Index, 25));  
						objElement.SetAttribute("ssn", SubResponse.Substring(122 + Index, 9));  
						//test =Record.Substring(103,3);
						objElement.SetAttribute("phone", SubResponse.Substring(131 + Index, 10));  
						objElement.SetAttribute("ext", SubResponse.Substring(141 + Index, 5));  

						objResult.DocumentElement.AppendChild(objElement);

						Index = Index + 95;
					} */
					
				}
				//return objResult.OuterXml ; 
				return strData ; 

			}
			catch (Exception ex )
			{
				objElement =objResult.CreateElement("Error" );
				objElement.SetAttribute("Msg",ex.ToString());
				objResult.DocumentElement.AppendChild(objElement);
				return objResult.OuterXml;
			}
		}

		#endregion
		#region CVA
		//Added for Itrack Issue 6708 on 18 Nov 09
		public string GetPriorPolicy(Cms.Model.Client.ClsCustomerInfo objCustomerInfo, ref string strWebResponse)
		{
			XmlDocument objResult= new XmlDocument();  
			System.Xml.XmlElement  objElement = null;
			sbError = new StringBuilder();

			objElement = objResult.CreateElement("ResultData");
			objResult.AppendChild (objElement); 

			#region request variables
			string userName="", password="", strAccount="", BillCode="", Product="";
			string RiskAddressFormatCode="",ResponseFormatCode="";
			string UnparsedStreetAddress="" ;
			string City="", State="", Zip="", QuoteBack="Wolverine";
			string LastName="", FirstName="", MiddleName="", Suffix="";
			string DOB="", SSN="", DLState="", LicNum = "",PriorDLState="",PriorLicNum = "";
			string HomePhone ="",WorkPhone = "" ;

			#endregion
			try
			{
				//com.iix.expressnet.auth objProxy =  new com.iix.expressnet.auth();
				//ClsIIXProxy objProxy = new ClsIIXProxy();
				ClsIIXProxy objProxy = new ClsIIXProxy(strUrl);
				ClsRequestResponseLogInfo objLogInfo = new ClsRequestResponseLogInfo();
				checkIIXSettings();
				
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				userName = UserName;
				if (userName.Trim() !="")
				{
					if ( userName.Trim().Length > 3 )
					{
						sbError.Append("<br>User Name length is greater than 3.");
					}
					if ( userName.Trim().Length < 3 )
					{
						userName=userName.PadRight( 3,' '); 
					}
				}
				else
				{
					userName= userName.PadRight( 3,' '); 
				}
				//password
				password = Password;

				if (password.Trim() !="")
				{
					if ( password.Trim().Length > 10 )
					{
						sbError.Append("<br>Password length is greater than 10");
					}
					if ( password.Trim().Length < 10 )
					{
						password= password.PadRight( 10,' '); 
					}
				}
				else
				{
					password=password.PadRight( 10,' '); 
				}

				//Account 
				strAccount = AccountNumber;
				if (strAccount.Trim() !="")
				{
					if ( strAccount.Trim().Length > 6 )
					{
						sbError.Append("<br>Account length is greater than 6");
					}
					if ( strAccount.Trim().Length < 6 )
					{
						strAccount=strAccount.PadRight( 6,' '); 
					}
				}
				else
				{
					strAccount=strAccount.PadRight( 6,' '); 
				}
				
				//Product
				Product = "CVA";
				if (Product.Trim() !="")
				{
					if ( Product.Trim().Length > 3)
					{
						sbError.Append("<br>Product length is greater than 3");
					}
					if ( Product.Trim().Length <= 3 )
					{
						Product=Product.PadRight( 3,' '); 
					}
				}
				else
				{
					Product=Product.PadRight( 3,' '); 
				}

				//BillCode
				string strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode(Product);
				BillCode= strBillCode;
				if (BillCode.Trim() !="")
				{
					if ( BillCode.Trim().Length > 3 )
					{
						sbError.Append("<br>Bill Code length is greater than 3");
					}
					if ( BillCode.Trim().Length < 3 )
					{
						BillCode=BillCode.PadRight( 3,' '); 
					}
				}
				else
				{
					BillCode=BillCode.PadRight( 3,' '); 
				}

				RiskAddressFormatCode = "U";
				if (RiskAddressFormatCode.Trim() !="")
				{
					if ( RiskAddressFormatCode.Trim().Length > 1)
					{
						sbError.Append("<br>Risk Address Format Code length is greater than 1");
					}
					if ( RiskAddressFormatCode.Trim().Length < 1 )
					{
						RiskAddressFormatCode=RiskAddressFormatCode.PadRight( 1,' '); 
					}
				}
				else
				{
					RiskAddressFormatCode=RiskAddressFormatCode.PadRight( 1,' '); 
				}

				ResponseFormatCode = "2";
				if (ResponseFormatCode.Trim() !="")
				{
					if ( ResponseFormatCode.Trim().Length > 1)
					{
						sbError.Append("<br>Response Format Code length is greater than 1");
					}
					if ( ResponseFormatCode.Trim().Length < 1 )
					{
						ResponseFormatCode=ResponseFormatCode.PadRight( 1,' '); 
					}
				}
				else
				{
					ResponseFormatCode=ResponseFormatCode.PadRight( 1,' '); 
				}

				//Last Name code
				LastName=objCustomerInfo.CustomerLastName;
				if (LastName.Trim() !="")
				{
					if (LastName.Trim().Length >20 )
					{
						//sbError.Append("<br>Last Name length is greater than 16");
						LastName = LastName.Substring(0,20);
					}
					if (LastName.Trim().Length < 20 )
					{
						LastName=LastName.PadRight(20,' '); 
					}
				}
				else
				{
					LastName=LastName.PadRight( 20,' '); 
				}

				//First Name code
				FirstName=objCustomerInfo.CustomerFirstName;
				if (FirstName.Trim() !="")
				{
					if (FirstName.Trim().Length >15 )
					{
						//sbError.Append("<br>First Name length is greater than 13");
						FirstName = FirstName.Substring(0,15);
					}
					if (FirstName.Trim().Length < 15 )
					{
						FirstName=FirstName.PadRight(15,' '); 
					}
				}
				else
				{
					FirstName=FirstName.PadRight( 15,' '); 
				}

				//Middle Name code
				if(objCustomerInfo.CustomerMiddleName != "")
					MiddleName=objCustomerInfo.CustomerMiddleName.Substring(0,15);
				if (MiddleName.Trim() !="")
				{
					if (MiddleName.Trim().Length >15 )
					{
						//sbError.Append("<br>Middle Name length is greater than 1");
						MiddleName = MiddleName.Substring(0,15);
					}
					if (MiddleName.Trim().Length < 15 )
					{
						MiddleName=MiddleName.PadRight(15,' '); 
					}
				}
				else
				{
					MiddleName=MiddleName.PadRight( 15,' '); 
				}

				//Suffix 
				Suffix=objCustomerInfo.CustomerSuffix;
				if (Suffix.Trim() !="")
				{
					if (Suffix.Trim().Length >3 )
					{
						//sbError.Append("<br>Suffix length is greater than 4");
						Suffix = Suffix.Substring(0,3);
					}
					if (Suffix.Trim().Length < 3 )
					{
						Suffix=Suffix.PadRight(3,' '); 
					}
				}
				else
				{
					Suffix=Suffix.PadRight( 3,' '); 
				}

				//SSN code
				SSN=objCustomerInfo.SSN_NO.ToString();
				SSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(SSN);
				SSN=SSN.ToString().Replace("-","");

				if (SSN.Trim() !="")
				{
					if (SSN.Trim().Length >9 )
					{
						//sbError.Append("<br>SSN length is greater than 9");
						SSN = SSN.Substring(0,9);
					}
					if (SSN.Trim().Length < 9 )
					{
						SSN=SSN.PadRight(9,' '); 
					}
				}
				else
				{
					SSN=SSN.PadRight( 9,' '); 
				}

				//DOB 
				DOB=objCustomerInfo.DATE_OF_BIRTH.ToString("MMddyyyy");
				if (DOB.Trim() !="")
				{
					if (DOB.Trim().Length >8 )
					{
						//sbError.Append("<br>Date of Birth length is greater than 8");
						DOB = DOB.Substring(0,8);
					}
					if (DOB.Trim().Length < 8 )
					{
						DOB=DOB.PadRight(8,' '); 
					}
				}
				else
				{
					DOB=DOB.PadRight( 8,' '); 
				}

				
				//Home Phone
				if(objCustomerInfo.CustomerHomePhone != "")
					HomePhone=objCustomerInfo.CustomerHomePhone.Trim().ToString();
				if (HomePhone.Trim() !="")
				{
					if (HomePhone.Trim().Length >10 )
					{
						HomePhone = HomePhone.Substring(0,10);
					}
					if (HomePhone.Trim().Length <	0 )
					{
						HomePhone=HomePhone.PadRight(10,' '); 
					}
				}
				else
				{
					HomePhone=HomePhone.PadRight( 10,' '); 
				}

				//Work Phone
				if(objCustomerInfo.CustomerBusinessPhone != "")
					WorkPhone=objCustomerInfo.CustomerBusinessPhone.Trim().ToString();
				if (WorkPhone.Trim() !="")
				{
					if (WorkPhone.Trim().Length >10 )
					{
						//sbError.Append("<br>License Number length is greater than 19");
						WorkPhone = WorkPhone.Substring(0,10);
					}
					if (WorkPhone.Trim().Length < 10 )
					{
						WorkPhone=WorkPhone.PadRight(10,' '); 
					}
				}
				else
				{
					WorkPhone=WorkPhone.PadRight( 10,' '); 
				}
				
				UnparsedStreetAddress = objCustomerInfo.CustomerAddress1.ToString();
				if (UnparsedStreetAddress.Trim() !="")
				{
					if ( UnparsedStreetAddress.Trim().Length > 48 )
					{
						UnparsedStreetAddress = UnparsedStreetAddress.Substring(0, 48);
					}
					if ( UnparsedStreetAddress.Trim().Length < 48 )
					{
						UnparsedStreetAddress=UnparsedStreetAddress.PadRight( 48,' '); 
					}
				}
				else
				{
					UnparsedStreetAddress=UnparsedStreetAddress.PadRight( 48,' '); 
				}


				//City
				City=objCustomerInfo.CustomerCity;
				if (City.Trim() !="")
				{
					if (City.Trim().Length >22 )
					{
						//sbError.Append("<br>City length is greater than 22");
						City = City.Substring(0,22);
					}
					if (City.Trim().Length < 22 )
					{
						City=City.PadRight(22,' '); 
					}
				}
				else
				{
					City=City.PadRight( 22,' '); 
				}

				//State code
				State=objCustomerInfo.CustomerStateCode;
				if (State.Trim() !="")
				{
					if (State.Trim().Length >2 )
					{
						//sbError.Append("<br>State length is greater than 2");
						State = State.Substring(0,2);
					}
					if (State.Trim().Length < 2 )
					{
						State=State.PadRight(2,' '); 
					}
				}
				else
				{
					State=State.PadRight( 2,' '); 
				}

				//Zip code
				Zip=objCustomerInfo.CustomerZip.ToString().Replace("-","");
				if (Zip.Trim() !="")
				{
					if (Zip.Trim().Length >9 )
					{
						//sbError.Append("<br>ZIP length is greater than 9");
						Zip = Zip.Substring(0,9);
					}
					if (Zip.Trim().Length < 9 )
					{
						Zip=Zip.PadRight(9,' '); 
					}
				}
				else
				{
					Zip=Zip.PadRight( 9,' '); 
				}

				//LicNum
				if (LicNum.Trim() !="")
				{
					if (LicNum.Trim().Length >20 )
					{
						//sbError.Append("<br>License Number length is greater than 19");
						LicNum = LicNum.Substring(0,20);
					}
					if (LicNum.Trim().Length < 20 )
					{
						LicNum=LicNum.PadRight(20,' '); 
					}
				}
				else
				{
					LicNum=LicNum.PadRight( 20,' '); 
				}

				//DLLicState
				if (DLState.Trim() !="")
				{
					if (DLState.Trim().Length >2 )
					{
						//sbError.Append("<br>License State length is greater than 2");
						DLState = DLState.Substring(0,2);
					}
					if (DLState.Trim().Length < 2 )
					{
						DLState=DLState.PadRight(2,' '); 
					}
				}
				else
				{
					DLState=State.PadRight( 2,' '); 
				}

				//PriorLicNum
				if (PriorLicNum.Trim() !="")
				{
					if (PriorLicNum.Trim().Length >20 )
					{
						//sbError.Append("<br>License Number length is greater than 19");
						PriorLicNum = PriorLicNum.Substring(0,20);
					}
					if (PriorLicNum.Trim().Length < 20 )
					{
						PriorLicNum=LicNum.PadRight(20,' '); 
					}
				}
				else
				{
					PriorLicNum=PriorLicNum.PadRight( 20,' '); 
				}


				//PriorDLLicState
				if (PriorDLState.Trim() !="")
				{
					if (PriorDLState.Trim().Length >2 )
					{
						//sbError.Append("<br>License State length is greater than 2");
						PriorDLState = PriorDLState.Substring(0,2);
					}
					if (PriorDLState.Trim().Length < 2 )
					{
						PriorDLState=PriorDLState.PadRight(2,' '); 
					}
				}
				else
				{
					PriorDLState=DLState.PadRight( 2,' '); 
				}

				
				//QuoteBack
				if (QuoteBack.Trim() !="")
				{
					if (QuoteBack.Trim().Length > 40 )
					{
						//sbError.Append("<br>QuoteBack length is greater than 40");
						QuoteBack = QuoteBack.Substring(0,40);
					}
					if (QuoteBack.Trim().Length < 40 )
					{
						QuoteBack=QuoteBack.PadRight( 40,' '); 
					}
				}
				else
				{
					QuoteBack=QuoteBack.PadRight( 40,' '); 
				}

				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				//---------------------------------------------------

				sbRequest = new StringBuilder();
				strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode(Product);

				sbRequest.Append(userName);//User name,3
				sbRequest.Append(password);//User password,10
				sbRequest.Append(strAccount);//Account,6
				//sbRequest.Append(BillCode);//BillCode,3
				sbRequest.Append(strBillCode);//BillCode,3
				sbRequest.Append(Product);//Product,3
				sbRequest.Append(RiskAddressFormatCode);
				sbRequest.Append(ResponseFormatCode);

				sbRequest.Append(LastName);
				sbRequest.Append(FirstName);
				sbRequest.Append(MiddleName);
				sbRequest.Append(Suffix);
				sbRequest.Append(SSN);
				sbRequest.Append(DOB);
				sbRequest.Append(LicNum);
				sbRequest.Append(DLState);
				sbRequest.Append(PriorLicNum);
				sbRequest.Append(PriorDLState);
				sbRequest.Append(HomePhone);
				sbRequest.Append(WorkPhone);
				sbRequest.Append(UnparsedStreetAddress);
				sbRequest.Append(City);
				sbRequest.Append(State);
				sbRequest.Append(Zip);
				sbRequest.Append(QuoteBack);
				

				string strRequest = sbRequest.ToString();
				//-------------------------------------------
				if ( strRequest.Length != 282 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg","The request string length for IIX web service should be 282. The current length is: " + strRequest.Length.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
				//Set the Log Model Object Values
				objLogInfo.CUSTOMER_ID = objCustomerInfo.CustomerId;
				objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERTOKEN);
				objLogInfo.SERVICE_VENDOR = "IIX";
				objLogInfo.REQUEST_DATETIME = DateTime.Now;
				objLogInfo.IIX_REQUEST=strRequest;
				//Send the request			  
				objProxy.SetLogModel = objLogInfo;
				string strWebRequest = 	objProxy.sendRequest(strRequest);
			
				strWebResponse = strWebRequest;
				if ( strWebRequest.StartsWith("Error"))
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",strWebRequest);
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
			
				string strResponse = "";
				string strData = "";
				//Response received
				if ( strWebRequest.StartsWith("Accept"))
				{
					int i;
					int count=0;
					string Record=""; 
					Hashtable  objArrRecords = new Hashtable();
					strResponse = strRequest.Substring(0,25) + strWebRequest.Substring(7);
					//Set the Log Model Object Values
					objLogInfo.CUSTOMER_ID = objCustomerInfo.CustomerId;
					objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.DRIVERDATA);
					objLogInfo.SERVICE_VENDOR = "IIX";
					objLogInfo.REQUEST_DATETIME = DateTime.Now;
					objLogInfo.IIX_RESPONSE=strResponse;
					//get the response	  
					objProxy.SetLogModel = objLogInfo;
					//get the response
					for (int j = 0; j < 5; j++)		// wait 5 seconds and try 5 times
					{
						strData = objProxy.getResponse(strResponse);
						if (strData.IndexOf("Error:Response not yet available")==-1)
						{
							break; 
						}
						else if(strData.IndexOf("Error:Response not yet available")!=-1 && j==4)
						{
							objElement =objResult.CreateElement("Error" );
							objElement.SetAttribute("Msg",strData);//strWebRequest);
							objResult.DocumentElement.AppendChild(objElement);
							return objResult.OuterXml;
						}
						System.Threading.Thread.Sleep(5000);
					}
					if(strData.Length < 234)
					{
						objElement =objResult.CreateElement("Error" );
						//objElement.SetAttribute("Msg","The response string length from IIX web service is less than 492. The current length is: " + strData.Length.ToString());
						objElement.SetAttribute("Msg","Prior policy information not found.");//Done for Itrack Issue 6708 on 11 Dec 09
						objResult.DocumentElement.AppendChild(objElement);
						return objResult.OuterXml;
						
					}
					////
					//Removing the common header
				}
				return strData ;

			}
			catch (Exception ex )
			{
				objElement =objResult.CreateElement("Error" );
				objElement.SetAttribute("Msg",ex.ToString());
				objResult.DocumentElement.AppendChild(objElement);
				return objResult.OuterXml;
			}
		}
		#endregion

		#region
		/// <summary>  by pravesh Chandel
		/// Parse the iix response and returns the Loss Report for Home and Rental
		/// </summary>
		/// <param name="IIxResponse">IIX Response return from iix service</param>
		/// <returns></returns>
		/// 
		//public string GetHomeRentLoss(string strState,string strCity,string strLastName, string strFirstName,string strMiddleName,string strDateOfBirth,Cms.Model.Application.HomeOwners.clsGeneralInfo  objHomeInfo)
		public string GetHomeRentLoss(string strState,string strCity,string strLastName, string strFirstName,string strMiddleName,string strDateOfBirth,string strSSNO,string strZIPCODE,string strADD1,string strADD2,string strCUSTOMER_ID, ref string strretWebResponse)
		{
			XmlDocument objResult= new XmlDocument();  
			System.Xml.XmlElement  objElement = null;
			sbError = new StringBuilder();

			objElement = objResult.CreateElement("ResultData");
			objResult.AppendChild (objElement); 

			try
			{
				//com.iix.expressnet.auth objProxy =  new com.iix.expressnet.auth();
				//ClsIIXProxy objProxy = new ClsIIXProxy();
				ClsIIXProxy objProxy = new ClsIIXProxy(strUrl);
				ClsRequestResponseLogInfo objLogInfo = new ClsRequestResponseLogInfo();
				checkIIXSettings();
				
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				if (strState.Trim() !="")
				{
					if ( strState.Trim().Length > 2 )
					{
						//sbError.Append("<br>State code length is greater than 2 .");
						strState = strState.Substring(0,2);
					}
					if ( strState.Trim().Length < 2 )
					{
						strState=strState.PadRight( 2,' '); 
					}
				}
				else
				{
					strState= strState.PadRight( 2,' '); 
				}
				if (strCity.Trim() !="")
				{
					if ( strCity.Trim().Length > 22 )
					{
						//sbError.Append("<br>City length is greater than 22 .");
						strCity = strCity.Substring(0,22);
					}
					if ( strCity.Trim().Length < 22 )
					{
						strCity=strCity.PadRight( 22,' '); 
					}
				}
				else
				{
					strCity= strCity.PadRight( 22,' '); 
				}

				//last name 
				if (strLastName.Trim() !="")
				{
					if ( strLastName.Trim().Length > 24 )
					{
						//sbError.Append("<br>Last name length is greater than 24");
						strLastName = strLastName.Substring(0,24);

					}
					if ( strLastName.Trim().Length < 24 )
					{
						strLastName=strLastName.PadRight( 24,' '); 
					}
				}
				else
				{
					strLastName=strLastName.PadRight( 24,' '); 
				}
				
				//first name 
				if (strFirstName.Trim() !="")
				{
					if ( strFirstName.Trim().Length > 12)
					{
						//sbError.Append("<br>first name length is greater than 12");
						strFirstName = strFirstName.Substring(0,12);
					}
					if ( strFirstName.Trim().Length < 12 )
					{
						strFirstName=strFirstName.PadRight( 12,' '); 
					}
				}
				else
				{
					strFirstName=strFirstName.PadRight( 12,' '); 
				}
			
				//first name 
				if (strMiddleName.Trim() !="")
				{
					if ( strMiddleName.Trim().Length > 15 )
					{
						//sbError.Append("<br>Middle name length is greater than 15");
						strMiddleName = strMiddleName.Substring(0,15);
					}
					if ( strMiddleName.Trim().Length < 15 )
					{
						strMiddleName=strMiddleName.PadRight( 15,' '); 
					}
				}
				else
				{
					strMiddleName=strMiddleName.PadRight( 15,' '); 
				}
				//dateofbirth
				if (strDateOfBirth.Trim() !="")
				{
					//done right now to test --- need to uncoment the line below !! 
					//strDateOfBirth="";
					if (strDateOfBirth.Trim().Length > 8 )
					{
						//sbError.Append("<br>Date Of Birth length is greater than 8");
						strDateOfBirth = strDateOfBirth.Substring(0,8);
					}
					if (strDateOfBirth.Trim().Length < 8 )
					{
						strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
					}
				}
				else
				{
					strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
				}
				//strpassword
				if (strPassword.Trim() !="")
				{
					if (strPassword.Trim().Length > 10 )
					{
						//sbError.Append("<br>Password length is greater than 10");
						strPassword = strPassword.Substring(0,10);
					}
					if (strPassword.Trim().Length < 10  )
					{
						strPassword=strPassword.PadRight( 10,' '); 
					}
				}
				else
				{
					strPassword=strPassword.PadRight( 10,' '); 
				}
				//Zip code
				string strZIP=strZIPCODE.ToString().Replace("-","");
				if (strZIP.Trim() !="")
				{
					if (strZIP.Trim().Length >9 )
					{
						//sbError.Append("<br>ZIP length is greater than 9");
						strZIP = strZIP.Substring(0,9);
					}
					if (strZIP.Trim().Length < 9 )
					{
						strZIP=strZIP.PadRight(9,' '); 
					}
				}
				else
				{
					strZIP=strZIP.PadRight( 9,' '); 
				}
				//

				string strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(strSSNO);
				strSSN=strSSN.ToString().Replace("-","");  
		
				if (strSSN.Trim() !="")
				{
					if (strSSN.Trim().Length >9 )
					{
						//sbError.Append("<br>SSN length is greater than 9");
						strSSN = strSSN.Substring(0,9);
					}
					if (strSSN.Trim().Length < 9 )
					{
						strSSN=strSSN.PadRight(9,' '); 
					}
				}
				else
				{
					strSSN=strSSN.PadRight( 9,' '); 
				}

				string straddr1=strADD1.ToString();
		
				if (straddr1.Trim() !="")
				{
					if (straddr1.Trim().Length >18 )
					{
						//sbError.Append("<br>Address length is greater than 18");
						straddr1 = straddr1.Substring(0,18);
					}
					if (straddr1.Trim().Length < 18 )
					{
						straddr1=straddr1.PadRight(18,' '); 
					}
				}
				else
				{
					straddr1=straddr1.PadRight( 18,' '); 
				}

		
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
				}
				
				//---------------------------------------------------
				

			
				sbRequest = new StringBuilder();
				strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode("APL");

				sbRequest.Append(strUserName);//User name,3
				sbRequest.Append(strPassword);//User password,10
				sbRequest.Append(strAccountNumber);//Account,6
				sbRequest.Append(strBillCode);//BillCode,3
				//sbRequest.Append("AMS");//BillCode,3
				sbRequest.Append("APL");//Product,3
				//sbRequest.Append("U");//Risk Address,1
				sbRequest.Append("00");//Response Format,2
				sbRequest.Append("I");//record type,1
				sbRequest.Append(pad.PadRight(40,' '));	//  Quoteback,40
				sbRequest.Append("1");	//block Id

				sbRequest.Append(strLastName);//last name,24
				sbRequest.Append(strFirstName);//firstname,12
				sbRequest.Append(pad.PadRight(1,' '));	//middleinitial,initial
				sbRequest.Append(strDateOfBirth);//date_of_birth,8,MMDDCCYY format
				//sbRequest.Append(pad.PadRight(9,' '));//ssn,9
				sbRequest.Append(strSSN);//ssn,9
				sbRequest.Append(pad.PadRight(24,' '));//  AKA Last name,24
				sbRequest.Append(pad.PadRight(12,' '));//AKA First name,12
				sbRequest.Append(pad.PadRight(1,' '));//AKA Middle Initial,1
				sbRequest.Append("R");	//block Id,1

				sbRequest.Append(pad.PadRight(7,' '));//House Number,7
				sbRequest.Append(pad.PadRight(2,' '));//Street Direction,2
				//sbRequest.Append(pad.PadRight(18,' '));//Street Name,18
				sbRequest.Append(straddr1);//Street Name,18
				sbRequest.Append(pad.PadRight(4,' '));//Street Type,4	
				sbRequest.Append(pad.PadRight(5,' '));//Apartment No.,5	

				sbRequest.Append(strCity);//City 22
				
				sbRequest.Append(strState);//State,2,
				//sbRequest.Append("46001"+pad.PadRight(4,' '));//julian_date,3,optional zero fill if not used
				sbRequest.Append(strZIP);//Zip ,9

				/*
				sbRequest.Append(pad.PadRight(10,' '));//Home Telephone, 10
				sbRequest.Append(pad.PadRight(10,' '));//Work TelePhone , 10
				sbRequest.Append(pad.PadRight(48,' '));//umparsed Street address,48
				*/				
			
				string strRequest = sbRequest.ToString();
				//-------------------------------------------
				if ( strRequest.Length != 230 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg","The request string length for IIX web service should be 230. The current length is: " + strRequest.Length.ToString());
					objElement.SetAttribute("Status","E");
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
			
				//Set the Log Model Object Values
				//objLogInfo.CUSTOMER_ID = objHomeInfo.CUSTOMER_ID;
				objLogInfo.CUSTOMER_ID = int.Parse(strCUSTOMER_ID);
				objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.LOSSTOKEN);
				objLogInfo.SERVICE_VENDOR = "IIX";
				objLogInfo.REQUEST_DATETIME = DateTime.Now;
				objLogInfo.IIX_REQUEST=strRequest;
			
				//Send the request			  
				objProxy.SetLogModel = objLogInfo;
				string strWebRequest = 	objProxy.sendRequest(strRequest);
			
				strretWebResponse = strWebRequest;
				if ( strWebRequest.StartsWith("Error"))
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg","IIX Response in Error.");//strWebRequest);
					objElement.SetAttribute("Status","E");
					objResult.DocumentElement.AppendChild(objElement);
					return objResult.OuterXml;
					
				}
			
				string strResponse = "";
				string strData = "";

				//Response received
				if ( strWebRequest.StartsWith("Accept"))
				{
					

					int i;
					int count=0;
					string Record=""; 
					Hashtable  objArrRecords = new Hashtable();
					strResponse = strRequest.Substring(0,25) + strWebRequest.Substring(7);

					//Set the Log Model Object Values
					//objLogInfo.CUSTOMER_ID = objHomeInfo.CUSTOMER_ID;
					objLogInfo.CUSTOMER_ID = int.Parse(strCUSTOMER_ID);
					objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.APLLOSSDATA);
					objLogInfo.SERVICE_VENDOR = "IIX";
					objLogInfo.REQUEST_DATETIME = DateTime.Now;
					objLogInfo.IIX_RESPONSE	=strResponse;
					//get the response	  
					objProxy.SetLogModel = objLogInfo;
					for (int j = 0; j < 5; j++)		// wait 5 seconds and try 5 times
					{
						strData = objProxy.getResponse(strResponse);
						if (strData.IndexOf("Error:Response not yet available")==-1)
						{
							break; 
						}
						else if(strData.IndexOf("Error:Response not yet available")!=-1 && j==4)
						{
							objElement =objResult.CreateElement("Error" );
							objElement.SetAttribute("Msg",strData);//strWebRequest);
							objElement.SetAttribute("Status","E");
							objResult.DocumentElement.AppendChild(objElement);
							return objResult.OuterXml;
						}
						System.Threading.Thread.Sleep(5000);
					}
					//Removing the common header
					//string SubResponse = strData.Substring(108);
					
					if (strData.IndexOf("OAPR")==-1)
					{
						objElement =objResult.CreateElement("Error" );
						objElement.SetAttribute("Msg","No Report found.");
						objElement.SetAttribute("Status","N");
						objResult.DocumentElement.AppendChild(objElement);
						return objResult.OuterXml;
					}
					if(strData.IndexOf("<APLUS-PROPERTY>") > 0)
						return strData.Substring(strData.IndexOf("<APLUS-PROPERTY>"));

					string SubResponse = strData.Substring(strData.IndexOf("OAPR"));
					//Retreiving the number of drivers
					int NoOfRecords = int.Parse(SubResponse.Substring(40,5));
					if (NoOfRecords==0)
					{
						objElement =objResult.CreateElement("Error" );
						objElement.SetAttribute("Msg","No Report found.");
						objElement.SetAttribute("Status","N");
						objResult.DocumentElement.AppendChild(objElement);
						return objResult.OuterXml;
					}
					int Index = 0;
					SubResponse = SubResponse.Substring(56);
					
					count=0;
					for ( i = 0; i < NoOfRecords; i++) 
					{
					string	Records=SubResponse; 
						string test ="";
						objElement =objResult.CreateElement("LOSS_REPORT" );
						//test =Records.Substring(100,24);
						objElement.SetAttribute("LAST-INS-NAME", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(100 + Index, 24))); 
						//test =Records.Substring(124 + Index, 12);
						objElement.SetAttribute("FIRST-INS-NAME", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(124 + Index, 12))); 
						//test =Records.Substring(136 + Index, 1);
						objElement.SetAttribute("MIDDLE-INS-NAME", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(136 + Index, 1))); 
						//test =Records.Substring(174,1);
						objElement.SetAttribute("GENDER", SubResponse.Substring(174 + Index, 1)); 
						//test =Records.Substring(175 + Index, 9);
						objElement.SetAttribute("SSN_NO", SubResponse.Substring(175 + Index, 9)); 
						//test =Records.Substring(184,8);
						objElement.SetAttribute("DOB", SubResponse.Substring(184 + Index, 8)); 
						//test =Records.Substring(284 + Index, 66);
						objElement.SetAttribute("LOSS-LOCATION-ADD1", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(284 + Index, 27))); 
						objElement.SetAttribute("LOSS-LOCATION-ADD2", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(311 + Index, 8))); 
						objElement.SetAttribute("LOSS-LOCATION-CITY", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(319 + Index, 20))); 
						objElement.SetAttribute("LOSS-LOCATION-STATE", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(339 + Index, 2))); 
						objElement.SetAttribute("LOSS-LOCATION-ZIP", SubResponse.Substring(341 + Index, 9)); 

						//test =Records.Substring(350 + Index, 66);
						objElement.SetAttribute("CURRENT-ADDR1", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(350 + Index, 27)));
						objElement.SetAttribute("CURRENT-ADDR2", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(377 + Index, 8)));
						objElement.SetAttribute("CURRENT-ADDR-CITY", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(385 + Index, 20)));
						objElement.SetAttribute("CURRENT-ADDR-STATE", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(405 + Index, 2)));
						objElement.SetAttribute("CURRENT-ADDR-ZIP", SubResponse.Substring(407 + Index, 9));
						//test =Records.Substring(416 + Index, 2);
						objElement.SetAttribute("MATCH-TYPE", SubResponse.Substring(416 + Index, 2));  
						//test =Records.Substring(418 + Index, 8);
						objElement.SetAttribute("LOSS-DATE", SubResponse.Substring(418 + Index, 8)); 
						//test =Records.Substring(426 + Index, 25);
						objElement.SetAttribute("LOSS-AMOUNT", SubResponse.Substring(427 + Index, 24));  
						//test =Records.Substring(451 + Index, 14);
						objElement.SetAttribute("CLAIM-TYPE", SubResponse.Substring(451 + Index, 14));  
						//test =Records.Substring(465 + Index, 45);
						objElement.SetAttribute("CAUSE-OF-LOSS", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(465 + Index, 45)));  
						//test =Records.Substring(568 + Index, 4);
						objElement.SetAttribute("POLICY-TYPE", SubResponse.Substring(568 + Index, 4));  
						//test =Records.Substring(572 + Index, 16);
						objElement.SetAttribute("POLICY-NUM", SubResponse.Substring(572 + Index, 16));  
						//test =Records.Substring(588 + Index, 39);
						objElement.SetAttribute("LOSS-CARRIER", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(588 + Index, 39)));  
						//test =Records.Substring(765 + Index, 60);
						objElement.SetAttribute("REMARKS", ReplaceXmlNodeSpecialCharacters(SubResponse.Substring(765 + Index, 60)));  
						//test =Records.Substring(827 + Index, 1);
						objElement.SetAttribute("CLAIM-STATUS", SubResponse.Substring(827 + Index, 1));  
						//objElement.SetAttribute("CLAIMS-STANDARD-CODE", SubResponse.Substring(141 + Index, 5));  

						objResult.DocumentElement.AppendChild(objElement);

						Index = Index + 829;
					} 
					
				}
				return objResult.OuterXml ; 
			}
			catch (Exception ex )
			{
				objElement =objResult.CreateElement("Error" );
				objElement.SetAttribute("Msg",ex.ToString());
				objElement.SetAttribute("Status","E");
				objResult.DocumentElement.AppendChild(objElement);
				return objResult.OuterXml;
			}
		}



		#endregion
		/// <summary>
		/// Parse the iix response and returns the undisclosed driver details in xml
		/// </summary>
		/// <param name="IIxResponse">IIX Response return from iix service</param>
		/// <returns>UDI driver details xml</returns>
		private string ParseUndisclosedDriverResponse(string IIxResponse)
		{
			try
			{
				return "";
			}
			catch (Exception objExp)
			{
				throw(new Exception("Error occured while parsing iix response." + objExp.Message));
			}
		}

		

		public  XmlDocument GetViolation(string strState,string strDLNo, string strLastName, string strFirstName, string strSuffix,string strMiddleName,string strDateOfBirth,string strGender,string strClientCode, string strCustomerID, string strPolicyID, string strPolicyVersionID)
		{
			XmlDocument objResult= new XmlDocument();  
			System.Xml.XmlElement  objElement = null;
			sbError = new StringBuilder();

			objElement = objResult.CreateElement("ResultData");
			objResult.AppendChild (objElement); 

			try
			{
				//com.iix.expressnet.auth objProxy =  new com.iix.expressnet.auth();
				//ClsIIXProxy objProxy = new ClsIIXProxy(strUrl); 
				//ClsIIXProxy objProxy = new ClsIIXProxy(); 
				ClsIIXProxy objProxy = new ClsIIXProxy(strUrl);
				ClsRequestResponseLogInfo objLogInfo = new ClsRequestResponseLogInfo();
				checkIIXSettings();
				
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult;
				}
				if (strState.Trim() !="")
				{
					if ( strState.Trim().Length > 2 )
					{
						sbError.Append("<br>State code length is greater than 2 .");
					}
					if ( strState.Trim().Length < 2 )
					{
						strState=strState.PadRight( 2,' '); 
					}
				}
				else
				{
					strState= strState.PadRight( 2,' '); 
				}
				//dl 
				if (strDLNo.Trim() !="")
				{
					if ( strDLNo.Trim().Length > 19 )
					{
						sbError.Append("<br>DL length is greater than 19");
					}
					if ( strDLNo.Trim().Length < 19 )
					{
						strDLNo= strDLNo.PadRight( 19,' '); 
					}
				}
				else
				{
					strDLNo=strDLNo.PadRight( 19,' '); 
				}
				//last name 
				if (strLastName.Trim() !="")
				{
					if ( strLastName.Trim().Length > 20 )
					{
						sbError.Append("<br>Last name length is greater than 20");
					}
					if ( strLastName.Trim().Length < 20 )
					{
						strLastName=strLastName.PadRight( 20,' '); 
					}
				}
				else
				{
					strLastName=strLastName.PadRight( 20,' '); 
				}
				
				//first name 
				if (strFirstName.Trim() !="")
				{
					if ( strFirstName.Trim().Length > 15 )
					{
						sbError.Append("<br>first name length is greater than 15");
					}
					if ( strFirstName.Trim().Length < 15 )
					{
						strFirstName=strFirstName.PadRight( 15,' '); 
					}
				}
				else
				{
					strFirstName=strFirstName.PadRight( 15,' '); 
				}
				//Suffix name 
				if (strSuffix.Trim() !="")
				{
					if ( strSuffix.Trim().Length > 3)
					{
						sbError.Append("<br>Suffix length is greater than 3");
					}
					if ( strSuffix.Trim().Length < 3 )
					{
						strSuffix=strSuffix.PadRight( 3,' '); 
					}
				}
				else
				{
					strSuffix=strSuffix.PadRight( 3,' '); 
				}
				//first name 
				if (strMiddleName.Trim() !="")
				{
					if ( strMiddleName.Trim().Length > 15 )
					{
						sbError.Append("<br>Middle name length is greater than 15");
					}
					if ( strMiddleName.Trim().Length < 15 )
					{
						strMiddleName=strMiddleName.PadRight( 15,' '); 
					}
				}
				else
				{
					strMiddleName=strMiddleName.PadRight( 15,' '); 
				}
				//dateofbirth
				if (strDateOfBirth.Trim() !="")
				{
					//done right now to test --- need to uncoment the line below !! 
					//strDateOfBirth="";
					if (strDateOfBirth.Trim().Length > 8 )
					{
						sbError.Append("<br>Date Of Birth length is greater than 8");
					}
					if (strDateOfBirth.Trim().Length < 8 )
					{
						strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
					}
				}
				else
				{
					strDateOfBirth=strDateOfBirth.PadRight( 8,' '); 
				}
				//Gender
				if (strGender.Trim() !="")
				{
					if (strGender.Trim().Length > 1 )
					{
						sbError.Append("<br>Gender length is greater than 1");
					}
					if (strGender.Trim().Length < 1 )
					{
						strGender=strGender.PadRight( 1,' '); 
					}
				}
				else
				{
					strGender=strGender.PadRight( 1,' '); 
				}
				//client code
				if (strClientCode.Trim() !="")
				{
					if (strClientCode.Trim().Length > 8 )
					{
						sbError.Append("<br>Gender length is greater than 8");
					}
					if (strClientCode.Trim().Length < 8 )
					{
						strClientCode=strClientCode.PadRight( 8,' '); 
					}
				}
				else
				{
					strClientCode=strClientCode.PadRight( 8,' '); 
				}
				
		
				if ( sbError.Length > 0 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",sbError.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult;
				}
				
				//---------------------------------------------------
				

			
				sbRequest = new StringBuilder();
				strBillCode= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetIIXBillCode("MVR");

				sbRequest.Append(strUserName);//User name,3
				sbRequest.Append(strPassword);//User password,10
				//sbRequest.Append(pad.PadRight(2,' '));//paaded to fill the space of password as it needs 10 characters.
				sbRequest.Append(strAccountNumber);//Account,6
				sbRequest.Append(strBillCode);//BillCode,3
				sbRequest.Append("MVR");//Product,3
				sbRequest.Append("I");//order_purpose,1
				sbRequest.Append(strState);//State,2,post office state code

				sbRequest.Append(pad.PadRight(3,' '));//julian_date,3,optional zero fill if not used
				sbRequest.Append(pad.PadRight(6,' '));//rec_seq_no,6,optional zero fill if not used
				sbRequest.Append(strDLNo.ToUpper());//dl_number,19
				sbRequest.Append(strLastName);//last name,20

				sbRequest.Append(strSuffix);//suffix,3
				sbRequest.Append(strFirstName);//firstname,15
				sbRequest.Append(strMiddleName);//middlename,15
				sbRequest.Append(strDateOfBirth);//date_of_birth,8,MMDDCCYY format
			
				sbRequest.Append(strGender);//Gender,1
				
				sbRequest.Append(strClientCode);//client_code,8
				sbRequest.Append("ebix" + pad.PadRight(36,' '));//quoteback,40
				sbRequest.Append(pad.PadRight(1,' '));//request_type,1
				sbRequest.Append("V20");//format_id,3
				sbRequest.Append(pad.PadRight(1,' '));//r_flag,1
				sbRequest.Append(pad.PadRight(9,' '));//Ssn,9
				sbRequest.Append(pad.PadRight(30,' '));//extension,30

				string strRequest = sbRequest.ToString();
				//-------------------------------------------
				if ( strRequest.Length != 210 )
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg","The request string length for IIX web service should be 210. The current length is: " + strRequest.Length.ToString());
					objResult.DocumentElement.AppendChild(objElement);
					return objResult;
					
				}
			
				
				//Set the Log Model Object Values
				objLogInfo.CUSTOMER_ID = int.Parse(strCustomerID);
				objLogInfo.POLICY_ID = int.Parse(strPolicyID);
				objLogInfo.POLICY_VERSION_ID = int.Parse(strPolicyVersionID); 
				objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.MVRTOKEN); 
				objLogInfo.SERVICE_VENDOR = "IIX";
				objLogInfo.IIX_REQUEST = strRequest;
				objLogInfo.REQUEST_DATETIME = DateTime.Now;
			
				//Send the request			  
				objProxy.SetLogModel = objLogInfo;
				string strWebRequest = 	objProxy.sendRequest(strRequest);
			
				if ( strWebRequest.StartsWith("Error"))
				{
					objElement =objResult.CreateElement("Error" );
					objElement.SetAttribute("Msg",strWebRequest);
					objResult.DocumentElement.AppendChild(objElement);
					return objResult;
					
				}
			
				string strResponse = "";
				string strData = "";

				//Response received
				if ( strWebRequest.StartsWith("Accept"))
				{
					
					int i;
					int count=0;
					string Record=""; 
					Hashtable  objArrRecords = new Hashtable();
					strResponse = strRequest.Substring(0,25) + strWebRequest.Substring(7);
					
					//Set the Log Model Object Values
					objLogInfo.CUSTOMER_ID = int.Parse(strCustomerID);
					objLogInfo.POLICY_ID = int.Parse(strPolicyID);
					objLogInfo.POLICY_VERSION_ID = int.Parse(strPolicyVersionID); 
					objLogInfo.CATEGORY_ID = Convert.ToInt32(ServiceCategory.MVRDATA); 
					objLogInfo.SERVICE_VENDOR = "IIX";
					objLogInfo.IIX_RESPONSE		=strResponse;
					objLogInfo.REQUEST_DATETIME = DateTime.Now;
			
					//get the response
					objProxy.SetLogModel = objLogInfo;
					for (int j = 0; j < 5; j++)		// wait 5 seconds and try 5 times
					{
						strData = objProxy.getResponse(strResponse);
						if (strData.IndexOf("Error:Response not yet available")==-1)
						{
							break; 
						}
						else if(strData.IndexOf("Error:Response not yet available")!=-1 && j==4)
						{
							objElement =objResult.CreateElement("Error" );
							objElement.SetAttribute("Msg",strData);//strWebRequest);
							objResult.DocumentElement.AppendChild(objElement);
							return objResult;
						}
						System.Threading.Thread.Sleep(5000);
					}
					if(strData.Length < 492)
					{
						objElement =objResult.CreateElement("Error" );
						//objElement.SetAttribute("Msg","The response string length from IIX web service is less than 492. The current length is: " + strData.Length.ToString());
						objElement.SetAttribute("Msg","No response found from IIX.");
						objResult.DocumentElement.AppendChild(objElement);
						return objResult;
						
					}
					//string SubResponse= strData.Substring(492); 
					string SubResponse= strData.Substring(108); 
				
					for (i =0 ; i<= SubResponse.Length -1; i=i+128)
					{
						objArrRecords.Add (count, SubResponse.Substring(i,128)); 
						count++;

					}
					string[] objArrViolationRecords = new string[objArrRecords.Count];
					string[] ResponseRecordNum = new string[objArrRecords.Count];
					for (  i = 0 ; i<= objArrRecords.Count -1 ; i++) 
					{
						Record=""; 
						Record = objArrRecords[i].ToString() ;
						//if ( Record.Substring(12,1)=="5")
						{
							objArrViolationRecords[i]=Record ; 
							ResponseRecordNum[i] = Record.Substring(12,1);
						}

					}

					//creating xml document for result 
					
				System.Xml.XmlElement  objMVRElement = null;

					count=0;
					for (  i = 0 ; i<= objArrViolationRecords.Length-1 ; i++) 
					{

						Record=""; 
						if ( objArrViolationRecords[i]!= null)
						{
							Record = objArrViolationRecords[i].ToString() ;
						
							if ( Record !="" && (ResponseRecordNum[i] == "4" || ResponseRecordNum[i] == "1"))
							{
									//objElement =objResult.CreateElement("MVRINFO" );
									 objMVRElement	=	objResult.DocumentElement["MVRINFO"];
								if (objMVRElement==null)
								{
									objMVRElement =objResult.CreateElement("MVRINFO" );
								}
										string mvr_lic_class="";
										string DRIVER_LICENSE_APPLICATION="";
										string mvr_Remarks="";
										string MVR_STATUS="";
								if (ResponseRecordNum[i] == "1")
								{
									MVR_STATUS=Record.Substring(101,1);
									objMVRElement.SetAttribute("MVR_STATUS",MVR_STATUS ); 
									objResult.DocumentElement.AppendChild(objMVRElement);
								}
								else if (ResponseRecordNum[i] == "4")
								{
									for (int m=i+1 ;m<=objArrViolationRecords.Length-1 ; m++)
									{
										string extraMVRRecord=""; 
										if ( objArrViolationRecords[i+1]!= null)
										{
											extraMVRRecord = objArrViolationRecords[i+1].ToString() ;
											Record=objArrViolationRecords[i+1].ToString();
											if ( extraMVRRecord !="" && ResponseRecordNum[i+1] == "4" )
											{
												int index = extraMVRRecord.IndexOf(":");	
												if (extraMVRRecord.Substring(13,9)=="LIC CLASS")
													mvr_lic_class=extraMVRRecord.Substring(23,100).ToString().Trim();
												else if (extraMVRRecord.Substring(13,26).ToUpper()=="DRIVER LICENSE APPLICATION")
													DRIVER_LICENSE_APPLICATION=extraMVRRecord.Substring(40,85).ToString().Trim();
												else
													mvr_Remarks=mvr_Remarks+ " " + extraMVRRecord.Substring(13,100).ToString().Trim();
												i++;
											}
											else
												break;
										}
									}
									objMVRElement.SetAttribute("MVR_CLASS","" ); 
									objMVRElement.SetAttribute("MVR_LICENCE_CLASS",mvr_lic_class ); 
									objMVRElement.SetAttribute("MVR_LICENCE_RESTRICTION","" ); 
									objMVRElement.SetAttribute("MVR_DRIVER_LIC_APP",DRIVER_LICENSE_APPLICATION ); 
									objMVRElement.SetAttribute("MVR_REMARKS",ReplaceXmlNodeSpecialCharacters(mvr_Remarks) );
								}
									
									//objResult.DocumentElement.AppendChild(objElement);
							}
							if ( Record !="" && ResponseRecordNum[i] == "5")
							{
								//string test ="";
								objElement =objResult.CreateElement("Violation" );
								//test =Record.Substring(13,6);
								objElement.SetAttribute("viol_type",Record.Substring(13,6).ToString().Trim() ); 
								//test =Record.Substring(21,8);
								objElement.SetAttribute("viol_date",Record.Substring(21,8).ToString().Trim()  ); 
								objElement.SetAttribute("conviction_date",Record.Substring(32,8).ToString().Trim()  ); 

								// by pravesh on 14 march 08 to append all viol-desc
								string Violation_desc=Record.Substring(41,38).ToString().Trim();
								for (int j=i+1 ;j<=objArrViolationRecords.Length-1 ; j++)
								{
									string extraRecord=""; 
									if ( objArrViolationRecords[i+1]!= null)
									{
										extraRecord = objArrViolationRecords[i+1].ToString() ;
										if ( extraRecord !="" && ResponseRecordNum[i+1] == "5" && extraRecord.Substring(13,6).ToString().Trim()=="" )
										{
											Violation_desc=Violation_desc + " " + extraRecord.Substring(41,38).ToString().Trim();
											i++;
										}
									}
								}
								//objElement.SetAttribute("viol_description",Record.Substring(41,38).ToString().Trim()  );
								objElement.SetAttribute("viol_description",ReplaceXmlNodeSpecialCharacters(Violation_desc) );
								//end here
								//test =Record.Substring(79,10);
								objElement.SetAttribute("viol_code",Record.Substring(79,10).ToString().Trim());  
								//test =Record.Substring(90,3);
								objElement.SetAttribute("points",Record.Substring(90,3).ToString().Trim()); 
								//test =Record.Substring(93,10);
								objElement.SetAttribute("Severity",Record.Substring(93,1).ToString().Trim());  
								objElement.SetAttribute("AViolation_code",Record.Substring(94,9).ToString().Trim());  
								//test =Record.Substring(103,3);
								objElement.SetAttribute("APoints",Record.Substring(103,3).ToString().Trim());  
								objElement.SetAttribute("viol_remarks",ReplaceXmlNodeSpecialCharacters(Violation_desc));  
								objElement.SetAttribute("viol_status","");  
								objElement.SetAttribute("viol_record",Record.Substring(12,1).ToString().Trim());  
								objResult.DocumentElement.AppendChild(objElement);
					
							}
							/* commented by pravesh on 1 aprill as handeled above
							else //Added by Mohit Agarwal 25-Jul-2007 ITrack 2813
							{
								objElement =objResult.CreateElement("Violation" );
								//test =Record.Substring(13,6);
								objElement.SetAttribute("viol_type","" ); 
								//test =Record.Substring(21,8);
								objElement.SetAttribute("viol_date",""  ); 
								objElement.SetAttribute("conviction_date",""  ); 
								//test =Record.Substring(41,38);
								objElement.SetAttribute("viol_description",""  );
								//test =Record.Substring(79,10);
								objElement.SetAttribute("viol_code","");  
								//test =Record.Substring(90,3);
								objElement.SetAttribute("points",""); 
								//test =Record.Substring(93,10);
								objElement.SetAttribute("Severity","");  
								objElement.SetAttribute("AViolation_code","");  
								//test =Record.Substring(103,3);
								objElement.SetAttribute("APoints","");
								if(ResponseRecordNum[i] == "2")
									objElement.SetAttribute("viol_remarks",Record.Substring(76,40).ToString().Trim());  
								else
									objElement.SetAttribute("viol_remarks","");  
								if(ResponseRecordNum[i] == "1")
									objElement.SetAttribute("viol_status",Record.Substring(101,1).ToString().Trim());  
								else
									objElement.SetAttribute("viol_status","");  
								objElement.SetAttribute("viol_record",Record.Substring(12,1).ToString().Trim());  
								objResult.DocumentElement.AppendChild(objElement);
							}
							*/
						}

					}
				}
				return objResult ; 
			}
			catch (Exception ex )
			{
				objElement =objResult.CreateElement("Error" );
				objElement.SetAttribute("Msg",ex.ToString());
				objResult.DocumentElement.AppendChild(objElement);
				return objResult;
			}
		}
	
		#endregion
		#region address verification and split address parts
		private Cms.CmsWeb.webcontrols.AddressDetails CreateAddressDetails(string strAddress,string strcity,string strState,string strZip)
		{
			string CmsWebUrl = System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();

			Cms.CmsWeb.com.ebix.wolverine.VerifyAddress obj = new Cms.CmsWeb.com.ebix.wolverine.VerifyAddress(CmsWebUrl);
            string strXmlReturn = obj.CheckAddress("", "", strAddress, strcity + " " + strState + " " + strZip);
			return Cms.CmsWeb.webcontrols.AddressDetails.CreateAddressDetails(strXmlReturn);
	
		}	
		
		#endregion
		private string ReplaceXmlNodeSpecialCharacters(string inputString)
		{
			inputString=inputString.Replace("\"","");
			inputString=inputString.Replace("'","");
			return inputString;
		}

	}
}
