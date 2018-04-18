using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using Cms.BusinessLayer.BlCommon;
using System.Web.Services;
using System.Xml;
using System.Web.Services.Protocols;
using Cms.Model;
namespace Cms.CmsWeb.webservices
{
	/// <summary>
	/// Summary description for CalculateSymbol.
	/// </summary>
	public class CalculateSymbol : System.Web.Services.WebService
	{
		public AuthenticationToken AuthenticationTokenHeader;
		private string authenticationKey = "";

		public CalculateSymbol()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			authenticationKey = ClsCommon.ServiceAuthenticationToken;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

//		[WebMethod]
//		public string HelloWorld()
//		{
//			return "Hello World";
//		}
		[WebMethod(MessageName="GetSymbolForVehicle1", Description="GetSymbolForVehicle with 3 arguments")]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetSymbolForVehicle(string VehicleType,int Amount,int Year)
		{

			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{

				int Symbol=0;
				try
				{
					switch(VehicleType)
					{
						case "11334": //Get Symbol for Private Pesanger
						case "11337": //Get Symbol for Trailer
						case "11336": ////Get Symbol for Motor
						case "11870": //Get Symbol for Campers
						case "11338": //Get Symbol for Local Haul - Intermittent
						case "11339": //Get Symbol for Local Haul
						case "11340": //Get Symbol for Trailer  - Intermittent
						case "11341": //Get Symbol for Trailer
						case "11871": //Get Symbol for Long Haul
						case "11868":
						case "11869":
							Symbol = GetSymbol(VehicleType,Amount);		
							break;
						case "11335": ////Get Symbol for CustomizedVan
							Symbol = GetSymbolForCustomized(VehicleType,Amount,Year);
							break;
						default:
							Symbol = 0;
							break;
					}			
					return Symbol.ToString();
				}
				catch//(Exception ex)
				{
					return "";
				}
				finally{}
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		//23 feb 2006
		[WebMethod(MessageName="GetSymbolForVehicle2", Description="GetSymbolForVehicle with 4 arguments")]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetSymbolForVehicle(string VehicleUseType,string VehicleUseCode,int Amount,int Year)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string Symbol="0";
				try
				{
					//get the lookup unique id thry call to proc
					ClsAuto ObjQQ = new ClsAuto();
					string GetSymbol ;
					GetSymbol = ObjQQ.GetVehicleUniqueID(VehicleUseCode,VehicleUseType);

					Symbol = GetSymbolForVehicle(GetSymbol,Amount,Year);
					return Symbol;
				}
				catch//(Exception ex)
				{
					return "";
				}
				finally{}
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		//

		public int GetSymbol(string VehicleType, int Amount)
		{
            int Symbol=0;			
			
			XmlDocument xDoc=new XmlDocument();
			xDoc.Load(Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml")); 			
			

			//XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='11337']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
			XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='" + VehicleType + "']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
			
			if ( xNodeList.Count > 0 )
			{
				XmlNode node = xNodeList[0];
				
				XmlNode symbolNode = node.SelectSingleNode("Symbol");

				string strSymbol = symbolNode.InnerText.Trim();
				Symbol = Convert.ToInt32(strSymbol);

			}			

			return Symbol;
		}
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetSymbolForAppPolicy(string VehicleType,int Amount,int Year)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return GetSymbolForVehicle(VehicleType,Amount,Year);
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
		//Get VIN : 3 April 2006
		[WebMethod]
		[SoapHeader("AuthenticationTokenHeader")]
		public  DataSet GetAutoVinNos(string strVIN)
		{	
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				return(new ClsAuto().FetchVINMasterDetailsFromVIN(strVIN));
			}
			else
				return(null);
		}
		//END 


		public int GetSymbolForCustomized(string VehicleType, int Amount, int Year)
		{
			int Symbol=0;			
			
			XmlDocument xDoc=new XmlDocument();
			xDoc.Load(Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml")); 			

			if(Year>=1990)
				Year=1990;
			else
				Year=1989;
			

			//XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='11337']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
			XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='" + VehicleType + "']/Year[@ID='" + Year.ToString() + "']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
			
			if ( xNodeList.Count > 0 )
			{
				XmlNode node = xNodeList[0];
				
				XmlNode symbolNode = node.SelectSingleNode("Symbol");

				string strSymbol = symbolNode.InnerText.Trim();
				Symbol = Convert.ToInt32(strSymbol);

			}			

			return Symbol;
		}

		public int GetSymbolForMotor(int Amount)
		{
			int Symbol=0;
			
			XmlDocument xDoc=new XmlDocument();
			xDoc.Load(Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml")); 			
			

			XmlNodeList xNodeList=xDoc.SelectNodes("GeneralRules/VehicleType[@ID='11336']/Amount[@Amount1<=" + Amount.ToString() + " and @Amount2>=" + Amount.ToString() + "]"); 
			
			if ( xNodeList.Count > 0 )
			{
				XmlNode node = xNodeList[0];
				
				XmlNode symbolNode = node.SelectSingleNode("Symbol");

				string strSymbol = symbolNode.InnerText.Trim();
				Symbol = Convert.ToInt32(strSymbol);

			}	
			return Symbol;
		}
		[WebMethod(MessageName="GetCapitalCustomerCreditScore", Description="Get Customer insurance score with customer model as parameter.")]
		[SoapHeader("AuthenticationTokenHeader")]
		public string GetCapitalCustomerCreditScore(string strUserName,string strPassword,string strAccountNumber ,string strUrl,string strCustDetail)
		{

			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				Cms.Model.Client.ClsCustomerInfo objClsCustomerInfo=new Cms.Model.Client.ClsCustomerInfo();
				string  [] strCustDetails = new string[0];
				strCustDetails = strCustDetail.Split('^');
					objClsCustomerInfo.CustomerLastName=strCustDetails[0].ToString();
					objClsCustomerInfo.CustomerFirstName=strCustDetails[1].ToString();
					objClsCustomerInfo.CustomerMiddleName=strCustDetails[2].ToString();
					objClsCustomerInfo.CustomerAddress1=strCustDetails[3].ToString();
					objClsCustomerInfo.CustomerCity=strCustDetails[4].ToString();
					objClsCustomerInfo.CustomerZip=strCustDetails[5].ToString();
					objClsCustomerInfo.CustomerStateCode=strCustDetails[6].ToString();
					objClsCustomerInfo.CustomerHomePhone=strCustDetails[7].ToString();
					objClsCustomerInfo.CustomerSuffix=strCustDetails[8].ToString();
					objClsCustomerInfo.SSN_NO=strCustDetails[9].ToString();
								
				Utils.CreditScoreDetails objScore;
				objScore = (new Utils.Utility(strUserName, strPassword, strAccountNumber , strUrl).GetCustomerCreditScore(objClsCustomerInfo));
				return objScore.Score.ToString()+"^"+objScore.FirstFactor+"^"+objScore.SecondFactor+"^"+objScore.ThirdFactor+"^"+objScore.FourthFactor;
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}
	}
}
