using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Cms.Model.Client;
using Cms.Model;
using Cms.Model.Application;
using Cms.Model.Application.HomeOwners;
//using Cms.Model.Application.Watercrafts;
using Cms.Model.Application.PrivatePassenger;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.DataLayer;
using Cms.ExceptionPublisher.ExceptionManagement;

namespace Cms.BusinessLayer.BlQuote.AcordXMLParser
{
	/// <summary>
	/// Summary description for ClsAutoAcordXMLParser.
	/// </summary>	
	public class ClsAutoAcordXMLParser:Cms.BusinessLayer.BlQuote.AcordXMLParser.ClsBaseAcordXmlParser
	{
		
		#region 
		private ArrayList alPersVehicle;
		private ArrayList alDrivers;
		private ArrayList alVehLoc;
		private ArrayList alDriverViolations;
		private ArrayList alLocations;
		private ArrayList alCoverages;
		string PolicyEffcDate="",strAdd_Info="";
		int intCtr=0;
		
		private const string PersVeh = "PersAutoLineBusiness/PersVeh";
		public const string PersDriver = "PersAutoLineBusiness/PersDriver";
		public const string ViolationInformation = "PersPolicy/AccidentViolation";
		private ClsLocationInfo objClsLocationInfo;
		public enum ACCIDENT_ID
		{
			AAF=42100, AAFPI=42110, AAFPD=42120, AAFD=42130, AVAA=40000, ACC=41000, DPIA=41210, FANE=41211, 
			FAE=41212, FA=41213, DPDA=41220, DFWARW=41230, FSRAWA=41260, VRA=41400, 
			ANFEPD=42220, FMSFA=52400, ANFEPI=42210, ANFED=42230, ANFE=42200
		};
		public enum SNOW_PLOW_CODE
		{
			INWOI=11914,INWI=11913,FT=11912
		};
		public enum VEHICLE_ASSSIGN_CODE
		{
			NR=11931,YONPA=11928,YOPA=11927,YPNPA=11930,YPPA=11929,OMPA=11926,OPA=11925,PNPA=11399,PPA=11398
		};
		
		public class Addr
		{
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

		#endregion

		public ClsAutoAcordXMLParser(string SystemID )
		{
			mSystemID = SystemID;
		}			
		/// <summary>
		/// Save the Rest info of PPA 
		/// </summary>		
		override public void ParseRiskInfo()
		{	
			try
			{
				//No of request in the XML
				XmlNodeList InsuranceSvcRq = GetApplicationNodeList();
				foreach(XmlNode node in InsuranceSvcRq)
				{
					XmlNodeList appNodeList = node.SelectNodes("PersAutoPolicyQuoteInqRq");		
					foreach(XmlNode nodeApp in appNodeList)
					{
						//Parse personal vehicle info
						alPersVehicle = ParseVehInfo(nodeApp);					
						//Parse Driver Details					
						alDrivers = ParsePersDriver(nodeApp);
						//Parse locations      // this is common to all lob's 
						alVehLoc = ParseLocations(nodeApp);							
						//Driver Violations
						alDriverViolations = this.ParseDriverViolations(nodeApp);					
					}
				}	
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				throw ex;
			}
		}		
		/// <summary>
		/// Save the Info 
		/// </summary>
		override public void SaveRiskInfo()
		{
			try
			{
				// Vehicle info
				if(alPersVehicle != null & alPersVehicle.Count > 0)
				{
					//Multi car discount
					if(alPersVehicle.Count > 1)
					{
						for(int i = 0; i < alPersVehicle.Count; i++)
						{
							ClsVehicleInfo objInfo = (ClsVehicleInfo)alPersVehicle[i];							
							objInfo.MULTI_CAR = "11919";
						}
					}
					for(int i = 0; i < alPersVehicle.Count; i++)
					{
						ClsVehicleInfo objInfo = (ClsVehicleInfo)alPersVehicle[i];
						objInfo.AIR_BAG=ReplaceEsqapeXmlCharacters(objInfo.AIR_BAG);
						objInfo.ANTI_LOCK_BRAKES=ReplaceEsqapeXmlCharacters(objInfo.ANTI_LOCK_BRAKES);
						objInfo.AUTO_POL_NO=ReplaceEsqapeXmlCharacters(objInfo.AUTO_POL_NO);
						objInfo.BODY_TYPE=ReplaceEsqapeXmlCharacters(objInfo.BODY_TYPE);
						objInfo.CLASS=ReplaceEsqapeXmlCharacters(objInfo.CLASS);
						objInfo.CLASS_DESCRIPTION=ReplaceEsqapeXmlCharacters(objInfo.CLASS_DESCRIPTION);
						objInfo.GRG_ADD1=ReplaceEsqapeXmlCharacters(objInfo.GRG_ADD1);
						objInfo.GRG_ADD2=ReplaceEsqapeXmlCharacters(objInfo.GRG_ADD2);
						objInfo.GRG_CITY=ReplaceEsqapeXmlCharacters(objInfo.GRG_CITY);
						objInfo.GRG_COUNTRY=ReplaceEsqapeXmlCharacters(objInfo.GRG_COUNTRY);
						objInfo.MAKE=ReplaceEsqapeXmlCharacters(objInfo.MAKE);
						objInfo.MODEL=ReplaceEsqapeXmlCharacters(objInfo.MODEL);
						objInfo.REGISTERED_STATE=ReplaceEsqapeXmlCharacters(objInfo.REGISTERED_STATE);
						objInfo.VIN=ReplaceEsqapeXmlCharacters(objInfo.VIN);
					}
					
					//Save Vehicle
					for(int i = 0; i < alPersVehicle.Count; i++)
					{
						ClsVehicleInfo objInfo = (ClsVehicleInfo)alPersVehicle[i];					
						SavePersVehicle(objInfo);
						objDataWrapper.ClearParameteres();
					}				
					objDataWrapper.ClearParameteres();						
					//Save Coverages
					SaveCoverages();				
					objDataWrapper.ClearParameteres();			
				}

				
				if(this.alDrivers != null && alDrivers.Count > 0)
				{
					// Driver Info
					for(int i = 0; i < alDrivers.Count; i++)
					{
						ClsDriverDetailsInfo objClsDriverDetailsInfo = (ClsDriverDetailsInfo)alDrivers[i];	
						objClsDriverDetailsInfo.DRIVER_ADD1=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_ADD1);
						objClsDriverDetailsInfo.DRIVER_ADD2=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_ADD2);
						objClsDriverDetailsInfo.DRIVER_BUSINESS_PHONE=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_BUSINESS_PHONE);
						objClsDriverDetailsInfo.DRIVER_CITY=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_CITY);
						objClsDriverDetailsInfo.DRIVER_COUNTRY=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_COUNTRY);
						objClsDriverDetailsInfo.DRIVER_DRIV_LIC=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_DRIV_LIC);
						objClsDriverDetailsInfo.DRIVER_FNAME=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_FNAME);
						objClsDriverDetailsInfo.DRIVER_HOME_PHONE=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_HOME_PHONE);
						objClsDriverDetailsInfo.DRIVER_LIC_STATE=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_LIC_STATE);
						objClsDriverDetailsInfo.DRIVER_LNAME=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_LNAME);
						objClsDriverDetailsInfo.DRIVER_MNAME=ReplaceEsqapeXmlCharacters(objClsDriverDetailsInfo.DRIVER_MNAME);
					}
					SaveDriverDetails();				
					objDataWrapper.ClearParameteres();
					
					//Save Driver Violations					
					SaveDriverViolations();
					objDataWrapper.ClearParameteres();

					//Save Driver specific endorsements A-94 and A-95
					ClsDriverDetail objDriver = new ClsDriverDetail();					
					objDriver.UpdateDriverEndorsements(objClsCustomerInfo.CustomerId,objClsGeneralInfo.APP_ID,objClsGeneralInfo.APP_VERSION_ID,objDataWrapper);
					objDataWrapper.ClearParameteres();
				}
				
			//	objDataWrapper.CommitTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				throw ex;
			}
		}	
		#region LOAD AUTO MAPPING XML
		public XmlDocument LoadMappingXml(string strPath)
		{
			XmlDocument mapDoc = new XmlDocument();
			mapDoc.Load(strPath);
			return mapDoc;
		}
		#endregion

		#region Auto Parsing 
		/// <summary>
		/// Parse the Vehicle Info
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ArrayList ParseVehInfo(XmlNode node)
		{	
			//Loading Mapping XML for Vehicle Info
			XmlNode nodeVeh = LoadMappingXml(ClsCommon.GetKeyValueWithIP("QQAcordMapping_AUTO_path")).SelectSingleNode("PRODUCTMASTER/PRODUCT[@ID='AUTO-MAPPING']");
			XmlNode nodePolicy = node.SelectSingleNode("PersPolicy/PersApplicationInfo");
			XmlNode nodePol = node.SelectSingleNode("PersPolicy");
			XmlNodeList objVehNodes = node.SelectNodes(PersVeh);	
		    			
			if( objVehNodes == null) 
				return null ;			
			
			ArrayList alVehicles= new ArrayList();
			foreach(XmlNode objNode in objVehNodes)
			{
				string strVinInfo="",strMakeInfo="",strBodyInfo="",strManuInfo="";
				dataNode = objNode.SelectSingleNode("VehIdentificationNumber");
				if( dataNode != null)
				{
					strVinInfo=ClsVinMaster.GetDetailsFromVIN(ClsCommon.DecodeXMLCharacters (dataNode.InnerText.Trim())).GetXml();
				}
				strVinInfo=strVinInfo.Replace("\r\n","");
				strVinInfo=strVinInfo.ToUpper();
				ClsVehicleInfo objClsVehicleInfo= new ClsVehicleInfo();					 
				
				if(objNode.Attributes["id"] != null)
				{
					objClsVehicleInfo.ID = objNode.Attributes["id"].Value;
				}	
				if(objNode.Attributes["LocationRef"] != null)
				{
					objClsVehicleInfo.LOCATION_REF = objNode.Attributes["LocationRef"].Value;
				}
				
				dataNode = objNode.SelectSingleNode("Manufacturer");				
				if( dataNode != null)
				{
					//objClsVehicleInfo.MAKE =  ClsCommon.DecodeXMLCharacters (dataNode.InnerText.Trim());
					// As descussed make , model , body type will be filled by our vin database
					if(strVinInfo.IndexOf("<MAKE_CODE>")>=0 && strVinInfo.IndexOf("</MAKE_CODE>")>=0)
					{
						strManuInfo=strVinInfo.Substring(strVinInfo.IndexOf("<MAKE_CODE>"),(strVinInfo.IndexOf("</MAKE_CODE>")-strVinInfo.IndexOf("<MAKE_CODE>"))).Replace("<MAKE_CODE>","");
						objClsVehicleInfo.MAKE =  strManuInfo;
					}
					else
					{
						objClsVehicleInfo.MAKE =  ClsCommon.DecodeXMLCharacters (dataNode.InnerText.Trim());
					}					
				}	
				
				dataNode = objNode.SelectSingleNode("Model");				
				if( dataNode != null)
				{
					if(strVinInfo.IndexOf("<SERIES_NAME>")>=0 && strVinInfo.IndexOf("</SERIES_NAME>")>=0)
					{
						strMakeInfo=strVinInfo.Substring(strVinInfo.IndexOf("<SERIES_NAME>"),(strVinInfo.IndexOf("</SERIES_NAME>")-strVinInfo.IndexOf("<SERIES_NAME>"))).Replace("<SERIES_NAME>","");
						objClsVehicleInfo.MODEL =  strMakeInfo;
					}
					else
					{
						objClsVehicleInfo.MODEL =  ClsCommon.DecodeXMLCharacters (dataNode.InnerText.Trim());
					}
				}
				if(strVinInfo.IndexOf("<BODY_TYPE>")>=0 && strVinInfo.IndexOf("</BODY_TYPE>")>=0)
					{
						strBodyInfo=strVinInfo.Substring(strVinInfo.IndexOf("<BODY_TYPE>"),(strVinInfo.IndexOf("</BODY_TYPE>")-strVinInfo.IndexOf("<BODY_TYPE>"))).Replace("<BODY_TYPE>","");
						objClsVehicleInfo.BODY_TYPE =  strBodyInfo;
					}
				dataNode = objNode.SelectSingleNode("ModelYear");				
				if( dataNode != null)
				{
					objClsVehicleInfo.VEHICLE_YEAR =  dataNode.InnerText.Trim();
				}				
				if( objClsVehicleInfo.VEHICLE_YEAR == null || objClsVehicleInfo.VEHICLE_YEAR == "")
				{
					System.Web.HttpContext.Current.Response.Write("<br>Vehicle year cannot be empty.");
					throw new Exception("Vehicle year cannot be empty.");
				}					
				if( objClsVehicleInfo.MAKE == null || objClsVehicleInfo.MAKE == "")
				{
					System.Web.HttpContext.Current.Response.Write("<br>Vehicle make cannot be empty.");
					throw new Exception("Vehicle make cannot be empty.");
				}
				if( objClsVehicleInfo.MODEL == null || objClsVehicleInfo.MODEL == "")
				{
					System.Web.HttpContext.Current.Response.Write("<br>Vehicle Model cannot be empty.");
					throw new Exception("Vehicle Model cannot be empty.");
				}
				
				dataNode = objNode.SelectSingleNode("Coverage[CoverageCd='COMP']/Option/OptionCd");	
				if(dataNode!=null)
				{
					string strSusp="";
					strSusp = dataNode.InnerText;
					if(strSusp=="SUS")
						objClsVehicleInfo.IS_SUSPENDED=10963;
				}
				dataNode = objNode.SelectSingleNode("VehBodyTypeCd");				
				if( dataNode != null)
				{
					// MAPING VEHICLE TYPES
					if(nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@ACCORDBODYTPE='"+  dataNode.InnerText.ToString() +"']")!=null)
					{
						//objClsVehicleInfo.USE_VEHICLE = ClsCommon.DecodeXMLCharacters (nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@ACCORDBODYTPE='"+  dataNode.InnerText.ToString() +"']/@QQBODYTYPE").InnerText.ToString());
						// objClsVehicleInfo.BODY_TYPE = ClsCommon.DecodeXMLCharacters (nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@ACCORDBODYTPE='"+  dataNode.InnerText.ToString() +"']/@QQBODYTYPE").InnerText.ToString());
					}
					else
					{
						//objClsVehicleInfo.USE_VEHICLE = ClsCommon.DecodeXMLCharacters ( dataNode.InnerText.Trim());
						//objClsVehicleInfo.BODY_TYPE = ClsCommon.DecodeXMLCharacters ( dataNode.InnerText.Trim());
					}
				}				

				dataNode = objNode.SelectSingleNode("VehIdentificationNumber");
				if( dataNode != null)
				{
					if(dataNode.InnerText.Trim().Length>2)
						objClsVehicleInfo.VIN =  ClsCommon.DecodeXMLCharacters (dataNode.InnerText.Trim());
					else
						objClsVehicleInfo.VIN = "";
				}

				//Calculating Symbol (ref.Capital Rater)
				//Get Symbol 
				dataNode = objNode.SelectSingleNode("VehSymbolCd");
				if(dataNode!=null) //Check if VehIdentificationNumber Node exists or Not
				{
					string strSymbol="";
					strSymbol = dataNode.InnerText.Trim();

					//if(objClsVehicleInfo.VIN!=null && objClsVehicleInfo.VIN.ToString().Trim() !="" && objClsVehicleInfo.VIN.ToString().Trim().Length>2)
					//{
						if(strSymbol!="")
							objClsVehicleInfo.SYMBOL = Convert.ToInt32(strSymbol.ToString());
						else
							objClsVehicleInfo.SYMBOL = 1;
					//}
					//else if(objClsVehicleInfo.VIN!=null && dataNode.InnerText.Trim().Length<=2)
					//{
						//objClsVehicleInfo.SYMBOL = int.Parse(objClsVehicleInfo.VIN.ToString().Trim());
					//}
					/*
					ClsAuto gObjQQAuto= new ClsAuto();
					string strVin = "",strSymbol="";
					if(objClsVehicleInfo.VIN.ToString().Trim() !="" && objClsVehicleInfo.VIN.ToString().Trim().Length>2)
					{
						strSymbol = gObjQQAuto.GetVehicleSymbol(objClsVehicleInfo.VIN.ToString());
						if(strSymbol!="")
							objClsVehicleInfo.SYMBOL = Convert.ToInt32(strSymbol.ToString());
						else
							objClsVehicleInfo.SYMBOL = 1;
					}
					else if(dataNode.InnerText.Trim().Length<=2)
					{
						objClsVehicleInfo.SYMBOL = int.Parse(dataNode.InnerText.Trim());
					}
					*/
				}
				//End Calculating Symbol (ref.Capital Rater)

				dataNode = objNode.SelectSingleNode("VehPerformanceCd");
				if( dataNode != null)
				{
					objClsVehicleInfo.VEH_PERFORMANCE =  dataNode.InnerText;
				}
				
				/*start Symbol */				
				/*ClsAuto objClsAuto = new ClsAuto();
				string strVehicleSymbol="";
				string strVehcileYear="";
				string strVehicleMake="";
				string strVehilceModel="";
				
				strVehcileYear = objNode.SelectSingleNode("ModelYear").InnerText.ToString().Trim();
				strVehicleMake = objNode.SelectSingleNode("Manufacturer").InnerText.ToString().Trim();
				strVehilceModel = objNode.SelectSingleNode("Model").InnerText.ToString().Trim();
				strVehicleSymbol = objClsAuto.GetVehicleVinSymbol(strVehcileYear,strVehicleMake,strVehilceModel);

				XmlDocument SymbolXmlDocument = new XmlDocument();
				SymbolXmlDocument.LoadXml(strVehicleSymbol);
				if(SymbolXmlDocument.SelectSingleNode("//VIN[@SYMBOL]").InnerText != "")
					strVehicleSymbol = SymbolXmlDocument.SelectSingleNode("//VIN[@SYMBOL]").InnerText;
				else
					strVehicleSymbol = "0";		
				objClsVehicleInfo.SYMBOL =Convert.ToInt32(strVehicleSymbol);*/
				/*end Symbol*/	
				
				//RateClassCd
				dataNode = objNode.SelectSingleNode("RateClassCd");				
				if( dataNode != null)
				{
					objClsVehicleInfo.CLASS= dataNode.InnerText.Trim();						 
				}
				
				dataNode = objNode.SelectSingleNode("VehicleValue");
				if( dataNode != null)
				{
					if(dataNode.InnerText!="" && dataNode.InnerText.ToString()!="$0")
						objClsVehicleInfo.AMOUNT = Convert.ToDouble(dataNode.InnerText.Trim());
						 
				}
				
                dataNode = objNode.SelectSingleNode("CostNewAmt/Amt"); 				
				if( dataNode != null)
				{
					if(dataNode.InnerText!="" && dataNode.InnerText.ToString()!="$0")
						objClsVehicleInfo.AMOUNT = Convert.ToDouble(dataNode.InnerText.Trim());						 
				}
				dataNode = objNode.SelectSingleNode("EstimatedAnnualDistance/NumUnits");					
				if( dataNode != null)
				{
					try
					{
						objClsVehicleInfo.ANNUAL_MILEAGE = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
					}
					catch//(Exception ex)
					{
						throw new Exception("Estimated Annual Distance has invalid value.");
					}
				}
				//RadiusOfUse
				dataNode = objNode.SelectSingleNode("RadiusOfUse");				
				if( dataNode != null)
					objClsVehicleInfo.RADIUS_OF_USE= Convert.ToInt32(dataNode.InnerText.Trim());	
				
	 			
				dataNode = objNode.SelectSingleNode("EstimatedAnnualDistance");		
				if( dataNode != null)
				{
					if( dataNode.InnerText.Trim().ToLower() == "yes")
					{
						objClsVehicleInfo.CAR_POOL = 10963;
					}
					else if(dataNode.InnerText.Trim().ToLower() == "no")
					{
						objClsVehicleInfo.CAR_POOL = 10964;
					}
				}	
					
				dataNode = objNode.SelectSingleNode("SnowPlowCode");				
				if( dataNode != null)
				{
					if(dataNode.InnerText.Trim() != "")
					{
						if(dataNode.InnerText.Trim().Equals("INWOI"))
							objClsVehicleInfo.SNOWPLOW_CONDS=(int) SNOW_PLOW_CODE.INWOI ;
						else if(dataNode.InnerText.Trim().Equals("INWI"))
							objClsVehicleInfo.SNOWPLOW_CONDS=(int) SNOW_PLOW_CODE.INWI ;
						else if(dataNode.InnerText.Trim().Equals("FT"))
							objClsVehicleInfo.SNOWPLOW_CONDS=(int) SNOW_PLOW_CODE.FT  ;
					}
				}	
			
				dataNode = objNode.SelectSingleNode("VEHICLECLASS_COMM");				
				if( dataNode != null)
					objClsVehicleInfo.CLASS_DESCRIPTION = ClsAuto.GetUniqueIdCommClass(dataNode.InnerText.Trim());
				else
					objClsVehicleInfo.CLASS_DESCRIPTION = "0";					
					
					objClsVehicleInfo.COVERED_BY_WC_INSU = "0";
					objClsVehicleInfo.TRANSPORT_CHEMICAL = "0";	
					objClsVehicleInfo.IS_ACTIVE = "Y";
				
				dataNode = objNode.SelectSingleNode("Displacement/NumUnits");						
				if( dataNode != null)
				{
					try
					{
						if( dataNode.InnerText.Trim() != "")
						{
							string strCC = dataNode.InnerText.Trim().Replace(",","");
							objClsVehicleInfo.VEHICLE_CC= DefaultValues.GetIntFromString(strCC);
						}
					}
					catch//(Exception ex)
					{
						throw new Exception("CC has invalid value.");
					}
				}	
			
				dataNode = objNode.SelectSingleNode("DistanceOneWay/NumUnits");
				if( dataNode != null)
				{
					if( dataNode.InnerText.Trim() != "")
					{
						objClsVehicleInfo.MILES_TO_WORK = dataNode.InnerText.Trim();
					}
				}

				dataNode = objNode.SelectSingleNode("CostNewAmt/Amt");
				if( dataNode != null)
				{
					if( dataNode.InnerText.Trim() != "")
					{
						objClsVehicleInfo.AMOUNT_COST_NEW = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
					}
				}	
				//Anti Lock Brake Code
				dataNode = objNode.SelectSingleNode("AntiLockBrakeCd");				
				if( dataNode != null)
				{						
					if (dataNode.InnerText.Trim()=="4" )
					{
						objClsVehicleInfo.ANTI_LOCK_BRAKES = "10963";
					}
					else
						objClsVehicleInfo.ANTI_LOCK_BRAKES = "10964";					
				}
				else
					objClsVehicleInfo.ANTI_LOCK_BRAKES = "10964";
	
							
				dataNode = objNode.SelectSingleNode("LicensePlateNumber");			
				if( dataNode != null)
				{
					objClsVehicleInfo.REGN_PLATE_NUMBER  = dataNode.InnerText.Trim();
				}	

				dataNode = objNode.SelectSingleNode("GaragingPostalCode");				
				if( dataNode != null)
				{
					objClsVehicleInfo.GRG_ZIP = dataNode.InnerText.Trim();
				}				

				dataNode = objNode.SelectSingleNode("TerritoryCd");				
				if( dataNode != null)
                {
					objClsVehicleInfo.TERRITORY = dataNode.InnerText.Trim();
				}	
				
				dataNode = objNode.SelectSingleNode("VehBodyTypeCd");				
				if( dataNode != null)
				{
					if(nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@ACCORDBODYTPE='"+  dataNode.InnerText.ToString() +"']")!=null)
					{
						objClsVehicleInfo.VEH_TYPE_CODE = ClsCommon.DecodeXMLCharacters (nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@ACCORDBODYTPE='"+  dataNode.InnerText.ToString() +"']/@QQBODYTYPE").InnerText.ToString());
						objClsVehicleInfo.MOTORCYCLE_TYPE_CODE = ClsCommon.DecodeXMLCharacters (nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@ACCORDBODYTPE='"+  dataNode.InnerText.ToString() +"']/@QQBODYTYPE").InnerText.ToString());
					}
				}

				//dataNode = objNode.SelectSingleNode("SeatBeltTypeCd");		
				dataNode = nodePolicy.SelectSingleNode("Seatbelt");		
				if( dataNode != null)
				{
					if(dataNode.InnerText.Trim() == "1")
					{
						objClsVehicleInfo.PASSIVE_SEAT_BELT = "10963";
					}
					else
						objClsVehicleInfo.PASSIVE_SEAT_BELT = "10964";

				}
				else
					objClsVehicleInfo.PASSIVE_SEAT_BELT = "10964";


						
				dataNode = objNode.SelectSingleNode("AirBagTypeCd");				
				if( dataNode != null)
				{
					// MAPPING AIRBAG VEHICLE DISCOUNT
					if(nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLE_DISCOUNTS']/NODE[@ID='AIRBAG_DISCOUNT']/ATTRIBUTES[@ACCORD_AIRBAG='"+  dataNode.InnerText.ToString() +"']")!=null)
					{
						objClsVehicleInfo.AIR_BAG = ClsCommon.DecodeXMLCharacters (nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLE_DISCOUNTS']/NODE[@ID='AIRBAG_DISCOUNT']/ATTRIBUTES[@ACCORD_AIRBAG='"+  dataNode.InnerText.ToString() +"']/@QQ_AIRBAG").InnerText.ToString());
					}
					else
						objClsVehicleInfo.AIR_BAG = ClsCommon.DecodeXMLCharacters ( dataNode.InnerText.Trim());
				}
				
				//Vehicle Use Code 
				dataNode = objNode.SelectSingleNode("VehUseCd");				
				if( dataNode != null)
				{
					// MAPPING VEHICLE USE
					if(nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLEUSE']/NODE[@ID='VEHICLE_USE']/ATTRIBUTES[@ACORDUSECODE='"+  dataNode.InnerText.ToString() +"']")!=null)
					{
						objClsVehicleInfo.VEHICLE_USE = ClsCommon.DecodeXMLCharacters (nodeVeh.SelectSingleNode("FACTOR[@ID='VEHICLEUSE']/NODE[@ID='VEHICLE_USE']/ATTRIBUTES[@ACORDUSECODE='"+  dataNode.InnerText.ToString() +"']/@MAKEAPPCODE").InnerText.ToString());
					}
					else
						objClsVehicleInfo.VEHICLE_USE = ClsCommon.DecodeXMLCharacters ( dataNode.InnerText.Trim());

					// Miles to work
					if(dataNode.InnerText.ToString()=="DO" || dataNode.InnerText.ToString()=="DU")
					{
						dataNode=objNode.SelectSingleNode("DistanceOneWay/NumUnits");		
						if(dataNode != null)
						objClsVehicleInfo.MILES_TO_WORK=dataNode.InnerText.ToString();
					}

				}

				//Use Vehicle personal or Commercial 					
				string vehType = objNode.Attributes["id"].Value.ToString();
				if( vehType.Substring(0,7) == "PersVeh" )
					objClsVehicleInfo.USE_VEHICLE_CODE  = "Personal";
				 else
					objClsVehicleInfo.USE_VEHICLE_CODE  = "Commercial";
				//Vehicle Age
				dataNode = objNode.SelectSingleNode("ModelYear");	
			
				if( dataNode != null)
				{
					if(nodePol.SelectSingleNode("ContractTerm/EffectiveDt")!=null)
					{
						PolicyEffcDate=nodePol.SelectSingleNode("ContractTerm/EffectiveDt").InnerText.Trim();
						if(nodePol.SelectSingleNode("ContractTerm/EffectiveDt").InnerText.Trim()!="")
							objClsVehicleInfo.VEHICLE_AGE =  VehicleAge(System.Convert.ToDateTime(nodePol.SelectSingleNode("ContractTerm/EffectiveDt").InnerText.Trim()),dataNode.InnerText.ToString().Trim(),vehType);
					}
				}	
				dataNode = objNode.SelectSingleNode("MultiCarDiscountInd");				
				if( dataNode != null)
				{
					if( dataNode.InnerText.Trim().ToLower() == "1")
					{
						objClsVehicleInfo.MULTI_CAR = "11919";
					}
					/*else if(  dataNode.InnerText.Trim().ToLower() == "false")
					{
						objClsVehicleInfo.MULTI_CAR = "10964";
					}*/
				}

				dataNode = objNode.SelectSingleNode("PurchaseDt");				
				if( dataNode != null)
				{
					objClsVehicleInfo.PURCHASE_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				}				

				dataNode = objNode.SelectSingleNode("InsuranceAmountMiscEqupt");				
				if( dataNode != null)
				{
					if(!dataNode.InnerText.Equals(""))
						objClsVehicleInfo.MISC_AMT = double.Parse(dataNode.InnerText.Trim());
				}

				dataNode = objNode.SelectSingleNode("InsuranceDescMiscEqupt");				
				if( dataNode != null)
				{
					if(!dataNode.InnerText.Equals(""))
						objClsVehicleInfo.MISC_EQUIP_DESC  = dataNode.InnerText.Trim();
				}	
				
				//Parse Addl interest
				ArrayList alAddInt = ParseAdditionalInterest(objNode);
				objClsVehicleInfo.SetAdditionalInterest(alAddInt);
				//end of Addl Info
				
				//Parse coverages
				alCoverages = ParseCoverages(objNode);
				objClsVehicleInfo.SetCoverages(alCoverages);
				//end of coverages			
			
				alVehicles.Add(objClsVehicleInfo);
			}
			return alVehicles;			
		}		
		
		/// <summary>
		///  Parse Driver Info
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ArrayList ParsePersDriver(XmlNode node)
		{
			string strDOB="";
			ArrayList alDrivers = new ArrayList();	
			XmlNode nodAllForViolation = node;
			XmlNodeList objDriverList = node.SelectNodes(PersDriver);
			XmlNodeList objGenNodeList = node.SelectNodes(InsuredOrPrincipal);	
			XmlNode objPersPolicy = node.SelectSingleNode(PersPolicy);
			if(objDriverList == null) 
				return null;
			//picking violation nodes to set a field in the driver table
			//XmlNodeList nodViolations = node.SelectNodes(ViolationInformation);
			
			
			foreach(XmlNode objDriverNode in objDriverList)
			{
				string deiverID2="",DriverID="";
				ClsDriverDetailsInfo objClsDriverDetailsInfo = new ClsDriverDetailsInfo();					
				if(objDriverNode.Attributes["id"] != null)
				{
					DriverID = objDriverNode.Attributes["id"].Value;
					objClsDriverDetailsInfo.ID = objDriverNode.Attributes["id"].Value;
					
				}				
				
				currentNode = objDriverNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName");
				if(currentNode != null)
				{
					dataNode = currentNode.SelectSingleNode("Surname");
					if(dataNode != null)
					{	
						objClsDriverDetailsInfo.DRIVER_LNAME = dataNode.InnerText.Trim();
					}			
					dataNode = currentNode.SelectSingleNode("GivenName");
					if(dataNode != null)
					{	
						objClsDriverDetailsInfo.DRIVER_FNAME = dataNode.InnerText.Trim();
					}
					
					dataNode = currentNode.SelectSingleNode("NameSuffix");
					if(dataNode != null)
					{
						objClsDriverDetailsInfo.DRIVER_SUFFIX = dataNode.InnerText.Trim();
					}
					
//					//have to check this field in database 
//					dataNode = currentNode.SelectSingleNode("TitlePrefix");
//					if(dataNode != null)
//					{
//						objClsDriverDetailsInfo.DRIVER_TITLE = dataNode.InnerText.Trim();
//					}

					dataNode = currentNode.SelectSingleNode("OtherGivenName");
					if(dataNode != null)
					{	
						objClsDriverDetailsInfo.DRIVER_MNAME = dataNode.InnerText.Trim();
					}					
				}	

				//<PersDriverInfo VehPrincipallyDrivenRef="1">
				//Assigned vehicle not fund in BL,ML,SP and table
				currentNode = objDriverNode.SelectSingleNode("PersDriverInfo");
				if(currentNode!= null)
				{				
					string driverID = objClsDriverDetailsInfo.ID;
					driverID = driverID.Substring(10,1);

                    
					if(currentNode.Attributes["VehPrincipallyDrivenRef"]!=null)
					{
						deiverID2 = currentNode.Attributes["VehPrincipallyDrivenRef"].Value;

						if(deiverID2 == driverID)
						{
							objClsDriverDetailsInfo.ASSIGNED_VEHICLE = "11398";
						}
						else
							objClsDriverDetailsInfo.ASSIGNED_VEHICLE = "11931";
					}
					else
						objClsDriverDetailsInfo.ASSIGNED_VEHICLE = "11931";
				}
				else 
					objClsDriverDetailsInfo.ASSIGNED_VEHICLE = "11931";


				//Checking SSN 
				
				currentNode = objDriverNode.SelectSingleNode("GeneralPartyInfo/NameInfo/TaxIdentity");
				if(currentNode != null)
				{
					string strSSN = "";
					dataNode = currentNode.SelectSingleNode("TaxIdTypeCd");
					if(dataNode != null && dataNode.InnerText.Trim()== "SSN" )
					{	
						dataNode = currentNode.SelectSingleNode("TaxId");
						if(dataNode != null )
						{
							if(dataNode.InnerText.ToString()!="")
							{
								if(dataNode.InnerText.ToString().Length > 11)
								{
									strSSN = dataNode.InnerText.ToString().Substring(0,11);//11 DIgit format
									strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(strSSN);
									objClsDriverDetailsInfo.DRIVER_SSN = strSSN.Trim();
								}
								else
								{
									strSSN=Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(dataNode.InnerText.ToString().Trim());
									objClsDriverDetailsInfo.DRIVER_SSN = strSSN;//dataNode.InnerText.ToString().Trim();
								}

							}
							else
								objClsDriverDetailsInfo.DRIVER_SSN =  null;			
						}
						else
							objClsDriverDetailsInfo.DRIVER_SSN =  null;						
					}
					else
                        objClsDriverDetailsInfo.DRIVER_SSN =  null;				
				}

				if(objClsDriverDetailsInfo.DRIVER_LNAME == null || objClsDriverDetailsInfo.DRIVER_LNAME == "")
				{
					throw new Exception("Driver Last Name is empty in XML.");
				}					
				if(objClsDriverDetailsInfo.DRIVER_FNAME == null || objClsDriverDetailsInfo.DRIVER_FNAME == "")
				{
					throw new Exception("Driver First Name is empty in XML.");
				}

				//Get the address details////////
				XmlNodeList nodeList = objDriverNode.SelectNodes("GeneralPartyInfo/Addr");
							
				foreach(XmlNode addrNode in objGenNodeList)
				{
//					string addrType  = "";				
//					dataNode = addrNode.SelectSingleNode("AddrTypeCd");				
//					if(dataNode != null)
//					{
//						addrType = dataNode.InnerText;
//					}
//					if(addrType == "StreetAddress")
//					{
						XmlNode addrDataNode = addrNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr1");
						if(addrDataNode != null)
						{
							objClsDriverDetailsInfo.DRIVER_ADD1 = addrDataNode.InnerText;
						}					
						addrDataNode = addrNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr2");					
						if(addrDataNode != null)
						{
							objClsDriverDetailsInfo.DRIVER_ADD2 = addrDataNode.InnerText;
						}					
						addrDataNode = addrNode.SelectSingleNode("GeneralPartyInfo/Addr/City");					
						if(addrDataNode != null)
						{
							objClsDriverDetailsInfo.DRIVER_CITY = addrDataNode.InnerText;
						}						
						
						addrDataNode = addrNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd");					
						if(addrDataNode != null)
						{								
							objClsDriverDetailsInfo.DRIVER_STATE = addrDataNode.InnerText.Trim();
						}

						addrDataNode = addrNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode");					
						if(addrDataNode != null)
						{
							objClsDriverDetailsInfo.DRIVER_ZIP = addrDataNode.InnerText;
						}
//					}			
				}

				//Person Info
				currentNode = objDriverNode.SelectSingleNode("DriverInfo/PersonInfo");					
				if(currentNode != null)
				{
					dataNode = currentNode.SelectSingleNode("GenderCd");				
					if(dataNode != null)
					{
						objClsDriverDetailsInfo.DRIVER_SEX = dataNode.InnerText;
					}
					dataNode = currentNode.SelectSingleNode("BirthDt");				
					if(dataNode != null)
					{
						strDOB = dataNode.InnerText;
						objClsDriverDetailsInfo.DRIVER_DOB = DefaultValues.GetDateFromString(dataNode.InnerText);
					}
					dataNode = currentNode.SelectSingleNode("MaritalStatusCd");				
					if(dataNode != null)
					{
						objClsDriverDetailsInfo.DRIVER_MART_STAT = dataNode.InnerText;
					}				
					dataNode = currentNode.SelectSingleNode("OccupationClassCd");				
					if(dataNode != null)
					{
						objClsDriverDetailsInfo.DRIVER_OCC_CODE = dataNode.InnerText;
					}
					//Driver Income
					//dataNode = currentNode.SelectSingleNode("EmployeePay/NumUnits");				
					dataNode = currentNode.SelectSingleNode("DriverIncome");				
					if(dataNode != null)
					{																	
						//we'll take Num of Units from some  where else 
						int empNumOfUnits = int.Parse(dataNode.InnerText.Trim());
						if(empNumOfUnits >= 9)
						{
							objClsDriverDetailsInfo.DRIVER_INCOME = 11415;//High income 
						}						
						else
						{
							objClsDriverDetailsInfo.DRIVER_INCOME = 11414;//Low income
						}
					}
					else
						objClsDriverDetailsInfo.DRIVER_INCOME = 11414;//Low income

					// No of dependants
					dataNode = currentNode.SelectSingleNode("NumDependents");				
					if(dataNode != null)
					{
						if(int.Parse(dataNode.InnerText.Trim()) >0)
						{
							objClsDriverDetailsInfo.NO_DEPENDENTS = 11589;
						}
						if(dataNode.InnerText.Trim().ToLower() == "0")
						{
							objClsDriverDetailsInfo.NO_DEPENDENTS = 11588;
						}				 
					}
				}

				//Licence Info
				currentNode = objDriverNode.SelectSingleNode("DriverInfo/License");
					
				if(currentNode != null)
				{
					dataNode = currentNode.SelectSingleNode("LicensedDt");
				
					if(dataNode != null)
					{
						//objClsDriverDetailsInfo.DATE_LICENSED = DefaultValues.GetDateFromString(dataNode.InnerText);
					}				
					dataNode = currentNode.SelectSingleNode("LicensePermitNumber");
				
					if(dataNode != null)
					{
						objClsDriverDetailsInfo.DRIVER_DRIV_LIC = dataNode.InnerText;
					}
					
					dataNode = currentNode.SelectSingleNode("LicenseClassCd");				
					if(dataNode != null)
					{
						objClsDriverDetailsInfo.DRIVER_LIC_CLASS = dataNode.InnerText;
					}
					//convert state code to state id 
					dataNode = currentNode.SelectSingleNode("StateProvCd");				
					if(dataNode != null)
					{
						objClsDriverDetailsInfo.DRIVER_LIC_STATE = dataNode.InnerText;
					}
					
					dataNode = currentNode.SelectSingleNode("LicenseStatusCd");				
					if(dataNode != null)
					{
						//Temp To be removed after maping
						if(dataNode.InnerText.ToUpper().Trim() == "ACTIVE")
							objClsDriverDetailsInfo.DRIVER_DRIV_TYPE = "11603";
						else
							objClsDriverDetailsInfo.DRIVER_DRIV_TYPE = "3478";

					}
					//objClsDriverDetailsInfo.DRIVER_DRIV_TYPE= "3477"; //Excluded						
					//objClsDriverDetailsInfo.DRIVER_DRIV_TYPE= "11603"; //Licensed						
					//objClsDriverDetailsInfo.DRIVER_DRIV_TYPE= "3478"; //Not licensed*/
					
				}
				//DriverRelationshipToApplicantCd
				/*currentNode = objDriverNode.SelectSingleNode("PersDriverInfo");
				if(currentNode != null)
					dataNode = currentNode.SelectSingleNode("DriverRelationshipToApplicantCd");
					if(dataNode != null)
                    {
						objClsDriverDetailsInfo.DRIVER_REL = currentNode.InnerText.Trim();
					}*/



				//Get Assigned Driver ID from Vehicles : as per new Implementation 27/4/2007 Testing code
				//Select all vehicles from Acord Xml : 27 april 2007
				//XmlNodeList objVehicleList = node.SelectNodes("PersAutoLineBusiness/PersVeh/PersDriverInfo/GoverningDriver");				
				
				//int assignediD = 0;
				//string strDrvType = "";
				//string strGovernDrv = "";
				XmlNodeList vnodelist = node.SelectNodes("PersAutoLineBusiness/PersVeh");
				// commented because Accord xml do not contain PersDriverInfo/GoverningDriver
				// pick principle driven vehicle from VehPrincipallyDrivenRef attribute
				/*foreach(XmlNode vnode in vnodelist)
				{
					//Get Type of Driver
					if(vnode.SelectSingleNode("PersDriverInfo/GoverningDriver").InnerText ==
						objDriverNode.Attributes["id"].Value.ToString())
					{
						strDrvType = vnode.SelectSingleNode("PersDriverInfo/DriverType").InnerText.ToString();
						strGovernDrv = vnode.SelectSingleNode("PersDriverInfo/GoverningDriver").InnerText.ToString();
						assignediD = int.Parse(strGovernDrv.Substring(10).ToString().Trim()); //i was here

					}
                    
				}*/

				//if(strDrvType.ToString() == "Principal")
				if(deiverID2!="")
					objClsDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID = 11398;
				else
					objClsDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID = 11399;

				//Vehicle ID is not being used in APP_DRIVER_DETAILS,default it to 1: 

				objClsDriverDetailsInfo.VEHICLEID  = "1";

				//End test Code


				#region PersDriverInfo To Be commented
                //PersDriver Info
				//Code to be removed as Acord does not send PersDriverInfo node in Driver elements:
				currentNode = objDriverNode.SelectSingleNode("PersDriverInfo");
                				
				if(currentNode != null)
				{
					if(currentNode.Attributes["VehPrincipallyDrivenRef"] != null)
					{
						objClsDriverDetailsInfo.VEHICLEID = currentNode.Attributes["VehPrincipallyDrivenRef"].Value; 
					}
					else //By Default 1 To be asked 
						objClsDriverDetailsInfo.VEHICLEID  = "1";

					
					dataNode = currentNode.SelectSingleNode("DriverRelationshipToApplicantCd");				
					if(dataNode != null)
					{

						//objClsDriverDetailsInfo.RELATIONSHIP = int.Parse(dataNode.InnerText.Trim());

						string relationCd = dataNode.InnerText.Trim().ToString();
						//Spouse
						if(relationCd== "SP")
						{
							objClsDriverDetailsInfo.RELATIONSHIP_CODE   = "S";
							objClsDriverDetailsInfo.RELATIONSHIP = 3472;
						}
							//Child
						else if(relationCd== "CH")
						{
							objClsDriverDetailsInfo.RELATIONSHIP_CODE = "C";
							objClsDriverDetailsInfo.RELATIONSHIP = 3467;
						}	
						else if(relationCd== "PA")
						{
							objClsDriverDetailsInfo.RELATIONSHIP_CODE = "O";
							objClsDriverDetailsInfo.RELATIONSHIP = 3470;
						}
						else if(relationCd== "IN")
						{
							objClsDriverDetailsInfo.RELATIONSHIP_CODE = "E";
							objClsDriverDetailsInfo.RELATIONSHIP = 3468;
						}
						else	//Other						
							objClsDriverDetailsInfo.RELATIONSHIP_CODE = "O";
						
					}
					else //Other
						objClsDriverDetailsInfo.RELATIONSHIP_CODE = "O";


					dataNode = currentNode.SelectSingleNode("YearsLicensed");	
					if(dataNode != null)
					{
						objClsDriverDetailsInfo.DATE_LICENSED = DefaultValues.GetDateFromString(PolicyEffcDate).AddYears(-int.Parse(dataNode.InnerText));
					}
					

						
					dataNode = currentNode.SelectSingleNode("GoodStudentCd");				
					if(dataNode != null)
					{
						if(dataNode.InnerText.Trim() != "")
							objClsDriverDetailsInfo.DRIVER_GOOD_STUDENT = "1";
					}

					//mature driver 					
					dataNode = currentNode.SelectSingleNode("MatureDriverInd");				
					if(dataNode != null)
					{						
							objClsDriverDetailsInfo.Mature_Driver = dataNode.InnerText.Trim() ;
					}
					// if driver has assigned point then 11398 if not aasigned point then 11399
					XmlNode nodtmp = node.SelectSingleNode("PersPolicy");
					XmlNodeList lstacvio = nodtmp.SelectNodes("AccidentViolation");
					string assitype="";
					
//					foreach(XmlNode nod in lstacvio)
//					{
//						assitype = nodtmp.SelectSingleNode("AccidentViolation").Attributes["DriverRef"].Value.ToString().Trim();
//						if(nod.Attributes["DriverRef"].Value.ToString()!=null && currentNode.Attributes["id"].Value.ToString() !=null)
//						{
//							if(nod.Attributes["DriverRef"].Value.ToString()==currentNode.Attributes["id"].Value.ToString())
//							{
//								assitype="Y";
//							}
//						}
//					}
					dataNode = currentNode.SelectSingleNode("@VehPrincipallyDrivenRef");				
					if(dataNode != null)
					{
						if(dataNode.InnerText.ToString() != "")
						{
							if(assitype=="Y")
								objClsDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID = 11398; //Principal point 
							else
								objClsDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID = 11399; //Principal no point 

						}
						else //case assign as occasional to the one tht is unassigned
							objClsDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID = 11926;

						/*if(dataNode.InnerText.ToLower().Trim() == "principal")
						{
							objClsDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID = 11398;
						}
						else if (dataNode.InnerText.ToLower().Trim() == "occasional")
						{
							objClsDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID = 11399;
						}	*/							
					}
//					else //case assign as occasional to the one tht is unassigned
//						objClsDriverDetailsInfo.APP_VEHICLE_PRIN_OCC_ID = 11399;


				}	
	
				//College student
				dataNode = currentNode.SelectSingleNode("DistantStudentInd");

				if(dataNode != null)
				{
					if(dataNode.InnerText.Trim().ToLower() == "1")
					{
						objClsDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED = "1";
					}
					else if (dataNode.InnerText.Trim().ToLower() == "") 
					{
						objClsDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED = "";
					}
				}
				else 
					objClsDriverDetailsInfo.DRIVER_STUD_DIST_OVER_HUNDRED = "";
				#endregion
		

				
				//Get the phone details
				nodeList = objDriverNode.SelectNodes("GeneralPartyInfo/Communications/PhoneInfo");			
				foreach(XmlNode phoneNode in nodeList)
				{
					string phoneType  = "";
				
					dataNode = phoneNode.SelectSingleNode("PhoneTypeCd");
				
					if(dataNode != null)
					{
						phoneType = dataNode.InnerText;
					}
				
					dataNode = phoneNode.SelectSingleNode("CommunicationsUseCd");
				
					string commType = "";

					if(dataNode  != null)
					{
						commType = dataNode.InnerText;
					}

					switch(phoneType)
					{
						case "Phone":
							if(commType == "Home")
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if(dataNode != null)
								{
									objClsDriverDetailsInfo.DRIVER_HOME_PHONE = dataNode.InnerText;
								}
							}
						
							if(commType == "Business")
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if(dataNode != null)
								{
									objClsDriverDetailsInfo.DRIVER_BUSINESS_PHONE = dataNode.InnerText;
								}
							}
							break;
						case "Cell":
							
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if(dataNode != null)
							{
								objClsDriverDetailsInfo.DRIVER_MOBILE = dataNode.InnerText;
							}
							break;						
					}

					// Question answers //
					currentNode = objDriverNode.SelectSingleNode("DriverInfo/QuestionAnswer");

					if(currentNode != null)
					{
						//Premier driver discount
						dataNode = currentNode.SelectSingleNode("PremierDriverDiscount");

						if(dataNode != null)
						{
							if(dataNode.InnerText.Trim().ToLower() == "true")
							{
								objClsDriverDetailsInfo.DRIVER_PREF_RISK = "1";
							}
							else if (dataNode.InnerText.Trim().ToLower() == "false") 
							{
								objClsDriverDetailsInfo.DRIVER_PREF_RISK = "0";
							}
						}
							
						//Good student
						dataNode = currentNode.SelectSingleNode("GoodStudent");

						if(dataNode != null)
						{
							if(dataNode.InnerText.Trim().ToLower() == "true")
							{
								objClsDriverDetailsInfo.DRIVER_GOOD_STUDENT = "1";
							}
							else if (dataNode.InnerText.Trim().ToLower() == "false") 
							{
								objClsDriverDetailsInfo.DRIVER_GOOD_STUDENT = "0";
							}
						}
							
						

						//Cycle With You
						dataNode = currentNode.SelectSingleNode("CycleWithYou");

						if(dataNode != null)
						{
							if(dataNode.InnerText.Trim().ToLower() == "yes")
							{
								objClsDriverDetailsInfo.CYCL_WITH_YOU  = 1;
							}
							else if (dataNode.InnerText.Trim().ToLower() == "no") 
							{
								objClsDriverDetailsInfo.CYCL_WITH_YOU = 0;
							}
						}
						
						//work loss waiver
						dataNode = currentNode.SelectSingleNode("WaiveWorkLoss");

						if(dataNode != null)
						{
							if(dataNode.InnerText.Trim().ToLower() == "true")
							{
								objClsDriverDetailsInfo.WAIVER_WORK_LOSS_BENEFITS  = "1";
							}
							else if (dataNode.InnerText.Trim().ToLower() == "false") 
							{
								objClsDriverDetailsInfo.WAIVER_WORK_LOSS_BENEFITS = "0";
							}
						}
						
						dataNode = currentNode.SelectSingleNode("NoCycleEndorsement");
						if(dataNode != null)
						{
							if(dataNode.InnerText.Trim().ToUpper() == "Y")
							{
								objClsDriverDetailsInfo.NO_CYCLE_ENDMT  = "1";
							}
							else if (dataNode.InnerText.Trim().ToUpper() == "N") 
							{
								objClsDriverDetailsInfo.NO_CYCLE_ENDMT = "0";
							}
						}						
					}	
				}		
				
				//Checking Violations			 
				
				XmlAttribute tempAttr = objDriverNode.Attributes["id"];
				string tempattribute="";
				if (tempAttr !=null)
						tempattribute = tempAttr.Value.ToString();
				string strTemp =ViolationInformation+"[@DriverRef='"+tempattribute +"']";

				XmlNodeList tempNodes = nodAllForViolation.SelectNodes(strTemp);
				if (tempNodes != null && tempNodes.Count >0)
				{
					objClsDriverDetailsInfo.VIOLATIONS = 10963;
				}
				else
				{
					objClsDriverDetailsInfo.VIOLATIONS = 10964;
				}
				// Driver Assignment for vehicle
				
				XmlNodeList tmpAssnod=objPersPolicy.SelectNodes("DriverVeh");
				string VehicleAssign="",flgprmDriver="",flgSafeDrv="";
				foreach(XmlNode nod in tmpAssnod)
				{
					string tempVehId="",tempDrvId="",tempDrvUse="",FuldrvId="",tmpPointsApllid="";
					if(nod.Attributes["VehRef"]!=null)
						tempVehId=nod.Attributes["VehRef"].Value.ToString();
					//Premier Driver
					foreach(XmlNode VHnod in vnodelist)
					{
						if(VHnod.Attributes["id"].Value.ToString()==tempVehId)
						{
							foreach(XmlNode CSnode in VHnod.SelectNodes("CreditOrSurcharge"))
							{
								if(CSnode.SelectSingleNode("SecondaryCd")!=null)
								{
									if(CSnode.SelectSingleNode("SecondaryCd").InnerText=="D_PRM_DVR")
									{
										if(CSnode.SelectSingleNode("com.brics_CreditDesc").InnerText.IndexOf("Premier")>=0 && flgprmDriver=="")
											flgprmDriver="TRUE";
										else if(CSnode.SelectSingleNode("com.brics_CreditDesc").InnerText.IndexOf("Safe")>=0 && flgSafeDrv=="")
											flgSafeDrv="TRUE";
									}
								}
							}
						}
					}
					if(tempVehId.Length>=8)
						tempVehId=tempVehId.Substring(tempVehId.Length-1,1);
					if(nod.Attributes["DriverRef"]!=null)
						tempDrvId=nod.Attributes["DriverRef"].Value.ToString();
					FuldrvId=tempDrvId;
					if(tempDrvId.Length>=11)
						tempDrvId=tempDrvId.Substring(tempDrvId.Length-1,1);
					if(nod.SelectSingleNode("DriverUseCd")!=null)
						tempDrvUse=nod.SelectSingleNode("DriverUseCd").InnerText;
					if(nod.SelectSingleNode("PointsApplied")!=null)
						tmpPointsApllid=nod.SelectSingleNode("PointsApplied").InnerText;
					
					if(FuldrvId==DriverID)
					{
						//VEHICLE_ASSSIGN_CODE,driverID;
						string strDrvAge=ClsAcord.CalculateDriverAge(PolicyEffcDate,strDOB);
						if(tempDrvUse.ToUpper().Trim() == "OCCASIONAL")
						{
							if(int.Parse(strDrvAge)>=25 && tmpPointsApllid.ToUpper()=="Y")
							{
								if(VehicleAssign=="")
									VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.OPA).ToString();
								else
									VehicleAssign= VehicleAssign+"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.OPA).ToString();
							}
							if(int.Parse(strDrvAge)>=25 && tmpPointsApllid.ToUpper()=="N")
							{
								if(VehicleAssign=="")
									VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.OMPA).ToString();
								else
									VehicleAssign = VehicleAssign+"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.OMPA).ToString();
							}
							if(int.Parse(strDrvAge)<25 && tmpPointsApllid.ToUpper()=="N")
							{
								if(VehicleAssign=="")
									VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.YONPA).ToString();
								else
									VehicleAssign =VehicleAssign+"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.YONPA).ToString();
							}
							if(int.Parse(strDrvAge)<25 && tmpPointsApllid.ToUpper()=="Y")
							{
								if(VehicleAssign=="")
									VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.YOPA).ToString();
								else
									VehicleAssign=VehicleAssign +"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.YOPA).ToString();
							}
						}
						else if(tempDrvUse.ToUpper().Trim() == "PRINCIPAL")
						{
							if(int.Parse(strDrvAge)>=25 && tmpPointsApllid.ToUpper()=="Y")
							{
								if(VehicleAssign=="")
									VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.PPA).ToString();
								else
									VehicleAssign=VehicleAssign +"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.PPA).ToString();
							}
							if(int.Parse(strDrvAge)>=25 && tmpPointsApllid.ToUpper()=="N")
							{
								if(VehicleAssign=="")
									VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.PNPA).ToString();
								else
									VehicleAssign=VehicleAssign +"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.PNPA).ToString();
							}
							if(int.Parse(strDrvAge)<25 && tmpPointsApllid.ToUpper()=="N")
							{
								if(VehicleAssign=="")
									VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.YPNPA).ToString();
								else
									VehicleAssign=VehicleAssign +"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.YPNPA).ToString();
							}
							if(int.Parse(strDrvAge)<25 && tmpPointsApllid.ToUpper()=="Y")
							{
								if(VehicleAssign=="")
									VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.YPPA).ToString();
								else
									VehicleAssign=VehicleAssign +"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.YPPA).ToString();
							}
						}
						else
						{
							if(VehicleAssign=="")
								VehicleAssign=tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.NR).ToString();
							else
								VehicleAssign=VehicleAssign +"|"+tempVehId+"~"+((int)VEHICLE_ASSSIGN_CODE.NR).ToString();
						}

					}
					
				}
				if(flgprmDriver!="")
					objClsDriverDetailsInfo.DRIVER_PREF_RISK="1";
				else
					objClsDriverDetailsInfo.DRIVER_PREF_RISK="0";
				if(flgSafeDrv!="")
					objClsDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT="1";
				else
					objClsDriverDetailsInfo.SAFE_DRIVER_RENEWAL_DISCOUNT="0";

				objClsDriverDetailsInfo.ASSIGNED_VEHICLE=VehicleAssign;
				alDrivers.Add(objClsDriverDetailsInfo);	
			}	
			return alDrivers;
		}

//		public ArrayList returnAccViolAttb(XmlNode node)
//		{
//		
//			ArrayList alAccViolationAttb = new ArrayList();
//			currentNode = node.SelectSingleNode("PersPolicy/AccidentViolation");			
//			foreach(XmlNode objDriverNode in currentNode)
//			{			
//				
//				string AccidentViolationAttb = objDriverNode.Attributes["DriverRef"].Value.ToString();				
//				alAccViolationAttb.Add(AccidentViolationAttb);
//			}			
//			
//			return alAccViolationAttb;
//		
//		
//		}

		
		/// <summary>
		/// parse location info
		/// </summary>
		/// <param name="nodeApp"></param>
		/// <returns></returns>
		public ArrayList ParseLocations(XmlNode nodeApp)
		{
			XmlNodeList locList = nodeApp.SelectNodes("Location");			
			if(locList == null) 
				return null;
			
			alLocations = new ArrayList();
			foreach(XmlNode locNode in locList)
			{
				objClsLocationInfo = new ClsLocationInfo();
				if(locNode.Attributes["id"] != null)
				{
					objClsLocationInfo.ID = locNode.Attributes["id"].Value;
				}
				
				Addr[] arAddr = ParseAddress(locNode,"Addr");				
				if( arAddr != null && arAddr.Length > 0)
				{
					foreach(Addr obj in arAddr)
					{							
						objClsLocationInfo.LOC_ADD1 = obj.Addr1;
						objClsLocationInfo.LOC_ADD2 = obj.Addr2;
						objClsLocationInfo.LOC_CITY = obj.City;
						objClsLocationInfo.LOC_COUNTRY = "1";
						objClsLocationInfo.LOC_ZIP = obj.PostalCode;
						objClsLocationInfo.LOC_COUNTY = obj.County;
						objClsLocationInfo.LOC_STATE = obj.StateProv;
						objClsLocationInfo.ADDR_TYPE = obj.AddrTypeCd;
						objClsLocationInfo.TERRITORY = obj.TerritoryCd;
					}
				}
				dataNode = locNode.SelectSingleNode("LocationDesc");
				
				if(dataNode != null)
				{
					objClsLocationInfo.DESCRIPTION = dataNode.InnerText;
				}				
			
				ArrayList alSubloc = null;
				if(this.currentNode != null)
				{
					alSubloc = ParseSublocations(locNode);
				}				
				if(alSubloc != null)
				{
					objClsLocationInfo.SetSublocations(alSubloc);
				}
				alLocations.Add(objClsLocationInfo);
			}			
			return alLocations;
		}
		

		/// <summary>
		/// Returns an array list containing driver violation objects
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ArrayList ParseDriverViolations(XmlNode node)
		{
			XmlNodeList violationList = node.SelectNodes("PersPolicy/AccidentViolation");							
			
			ArrayList alViol = new ArrayList();
			if(violationList == null)
				return null;

			foreach(XmlNode violNode in violationList)
			{
				ClsMvrInfo objClsMvrInfo = new ClsMvrInfo();

				if(violNode.Attributes["DriverRef"] != null)
				{
					objClsMvrInfo.DRIVER_REF = violNode.Attributes["DriverRef"].Value;
				}

				// Get AccID of accident and violation
				string strAVId="";
				if(violNode.Attributes["AccId"] != null)
				{
					strAVId=violNode.Attributes["AccId"].InnerText.ToString();
				}
//				string strpointapplied="";
//				if(violNode.Attributes["PointsApplied"] != null)
//				{
//					strpointapplied=violNode.Attributes["PointsApplied"].InnerText.ToString();
//				}
				// Check each violation AccRef id if AccID matches with any AccRef then 
				//this violation have same number of negative point as the point it have.
				string flgAVAdjPnt="0";
				foreach(XmlNode Nod in violationList)
				{
					if(Nod.Attributes["AccRef"].InnerText.ToString() == strAVId)
						flgAVAdjPnt="1";
				}
				//Checking Violation Code is numeric or not
				dataNode = violNode.SelectSingleNode("AccidentViolationCd");
				if(dataNode != null)
				{
					//if(IsInteger(dataNode.InnerText.Trim())== true )
					if(dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.AAF)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.AAFD)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.AAFPD)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.AAFPI)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.ACC)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.ANFEPD)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.AVAA)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.DFWARW)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.DPDA)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.DPIA)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.FA)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.FAE)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.FANE)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.FMSFA)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.FSRAWA)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.VRA)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.ANFEPI)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.ANFED)).ToString()
						|| dataNode.InnerText.Trim() == ((int)(ACCIDENT_ID.ANFE)).ToString()
						|| dataNode.InnerText.Trim() == "COMP"
						)
					{
						objClsMvrInfo.VIOLATION_CODE= dataNode.InnerText.Trim();//+'^'+'^'+strpointapplied;
					}
					else
						objClsMvrInfo.VIOLATION_CODE= dataNode.InnerText.Trim()+'^'+flgAVAdjPnt;//+'^'+strpointapplied;
					//else
					//	objClsMvrInfo.VIOLATION_CODE = "0" ;
				}
					
				dataNode = violNode.SelectSingleNode("AccidentViolationDt");

				if(dataNode != null)
				{
					objClsMvrInfo.MVR_DATE = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				}
					
				dataNode = violNode.SelectSingleNode("DamageTotalAmt");					
				if(dataNode != null)
				{
					objClsMvrInfo.MVR_AMOUNT = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
				}
					
				dataNode = violNode.SelectSingleNode("AccidentViolationDesc");
					
				if(dataNode != null)
				{
					if(dataNode.InnerText.Trim().ToLower() == "death")
					{
						objClsMvrInfo.MVR_DEATH = "Y";
					}
					else
					{
						objClsMvrInfo.MVR_DEATH = "N";
					}
				}
				alViol.Add(objClsMvrInfo);
			}
			return alViol;
		}


		/// <summary>
		/// Returns an array list containing ClsCoveragesInfo objects
		/// </summary>
		/// <param name="objNode"></param>
		/// <returns></returns>
		public ArrayList ParseCoverages(XmlNode objNode)
		{
			//string strRegCol="";
			string collType = "";
			//Loading Mapping XML for Coverage Codes 
			XmlNode nodeCode = LoadMappingXml(ClsCommon.GetKeyValueWithIP("QQAcordMapping_AUTO_path")).SelectSingleNode("PRODUCTMASTER/PRODUCT/FACTOR[@ID='BRICS_COVERAGE_CODE']");
			XmlNodeList objCoverageNodes = objNode.SelectNodes("Coverage");
			
			if(objCoverageNodes == null) 
				return null;
			 
			alCoverages = new ArrayList();
			
			foreach(XmlNode objCovNode in objCoverageNodes)
			{
				Cms.Model.Application.ClsCoveragesInfo objClsCoveragesInfo = new Cms.Model.Application.ClsCoveragesInfo();
				dataNode = objCovNode.SelectSingleNode("CoverageCd");
				if(dataNode != null)
				{
					#region MAPING COV CODE
					// MAPING XML COVERAGE CODES
					if(nodeCode.SelectSingleNode("NODE[@ID='BRICS_CODE']/ATTRIBUTES[@ACORD_CODE='"+  dataNode.InnerText.ToString() +"']")!=null)
					{
						if(nodeCode.SelectSingleNode("NODE[@ID='BRICS_CODE']/ATTRIBUTES[@ACORD_CODE='"+  dataNode.InnerText.ToString() +"']/@QQ_CODE")!=null)
							objClsCoveragesInfo.COVERAGE_CODE = nodeCode.SelectSingleNode("NODE[@ID='BRICS_CODE']/ATTRIBUTES[@ACORD_CODE='"+  dataNode.InnerText.ToString() +"']/@QQ_CODE").InnerText.ToString();
						else
							objClsCoveragesInfo.COVERAGE_CODE = dataNode.InnerText.Trim();
					}
					else
						objClsCoveragesInfo.COVERAGE_CODE = dataNode.InnerText.Trim();
					//END : MAPING XML COVERAGE CODES
					#endregion 
				}					
				dataNode = objCovNode.SelectSingleNode("CoverageDesc");
				if(dataNode != null)
				{
					objClsCoveragesInfo.COV_DESC = dataNode.InnerText.Trim();
				}
				if(objClsCoveragesInfo.COVERAGE_CODE == null || objClsCoveragesInfo.COVERAGE_CODE == "")
				{
					throw new Exception("Coverage code cannot be empty in ACORD XML");
				}
				//Signature Obtained
				XmlNode sigNode = objCovNode.SelectSingleNode("IsSigObt"); 
				if(sigNode != null)
				{
					objClsCoveragesInfo.SIGNATURE_OBTAINED = sigNode.InnerText.Trim();
				}
				//Limits
				XmlNodeList limitNodes = objCovNode.SelectNodes("Limit");
				//Cov Option
				XmlNodeList OptNodes = objCovNode.SelectNodes("Option");
				//Deuduc Option
				XmlNodeList DucNodes = objCovNode.SelectNodes("Deductible");

				#region MAPING COVERAGES 

				//****************************Mapping Cov Limits ********************//
				//BISPL
				XmlNode nodeMapCode = LoadMappingXml(ClsCommon.GetKeyValueWithIP("QQAcordMapping_AUTO_path")).SelectSingleNode("PRODUCTMASTER/PRODUCT/FACTOR[@ID='VEHICLECOVERAGES']");
				if(objClsCoveragesInfo.COVERAGE_CODE == "BISPL")
				{
					string minLimit = "",maxLimit = "";
					int lMaxLimit = 0;
					int lMax = int.Parse(limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString()) / 1000;
					int lMin = int.Parse(limitNodes.Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString()) / 1000;

					if(nodeMapCode.SelectSingleNode("NODE[@ID='BI_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_BI_MIN =" + lMin + " and @ACORD_BI_MAX= " + lMax + "]")!=null)
					{
						minLimit = nodeMapCode.SelectSingleNode("NODE[@ID='BI_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_BI_MIN =" + lMin + " and @ACORD_BI_MAX= " + lMax + "]/@QQ_BI_MIN").InnerText.ToString();
					}
					if(nodeMapCode.SelectSingleNode("NODE[@ID='BI_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_BI_MIN =" + lMin + " and @ACORD_BI_MAX= " + lMax + "]")!=null)
					{
						maxLimit = nodeMapCode.SelectSingleNode("NODE[@ID='BI_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_BI_MIN =" + lMin + " and @ACORD_BI_MAX= " + lMax + "]/@QQ_BI_MAX").InnerText.ToString();
					}
					lMaxLimit = int.Parse(maxLimit.ToString()) * 1000;
										
					//Appending the Nodelist
					limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = minLimit;
					limitNodes.Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = lMaxLimit.ToString();

				}
				//UM/UIM
				if(objClsCoveragesInfo.COVERAGE_CODE == "PUMSP" || objClsCoveragesInfo.COVERAGE_CODE == "UNDSP")
				{
					string minLimit = "",maxLimit = "";
					int lMaxLimit = 0;
					int lMax = int.Parse(limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString()) / 1000;
					int lMin = int.Parse(limitNodes.Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString()) / 1000;

					if(nodeMapCode.SelectSingleNode("NODE[@ID='UM_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UM_MIN =" + lMin + " and @ACORD_UM_MAX= " + lMax + "]")!=null)
					{
						minLimit = nodeMapCode.SelectSingleNode("NODE[@ID='UM_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UM_MIN =" + lMin + " and @ACORD_UM_MAX= " + lMax + "]/@QQ_UM_MIN").InnerText.ToString();
					}
					if(nodeMapCode.SelectSingleNode("NODE[@ID='UM_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UM_MIN =" + lMin + " and @ACORD_UM_MAX= " + lMax + "]")!=null)
					{
						maxLimit = nodeMapCode.SelectSingleNode("NODE[@ID='UM_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UM_MIN =" + lMin + " and @ACORD_UM_MAX= " + lMax + "]/@QQ_UM_MAX").InnerText.ToString();
					}
					lMaxLimit = int.Parse(maxLimit.ToString()) * 1000;
					
					//Appending the Nodelist
					limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = minLimit;
					limitNodes.Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = lMaxLimit.ToString();

				}
				//PD
				if(objClsCoveragesInfo.COVERAGE_CODE == "PD")
				{
					string minLimit = "";
					int lMin = int.Parse(limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString());
					
					if(nodeMapCode.SelectSingleNode("NODE[@ID='PD_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_PD_VAL =" + lMin + "]")!=null)
					{
						minLimit = nodeMapCode.SelectSingleNode("NODE[@ID='PD_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_PD_VAL =" + lMin + "]/@QQ_PD_VAL").InnerText.ToString();
					}
					
					//Appending the Nodelist
					limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = minLimit;
				}
				//CSL
				if(objClsCoveragesInfo.COVERAGE_CODE == "CSL")
				{
					string minLimit = "";
					int lMin = int.Parse(limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString());
					
					if(nodeMapCode.SelectSingleNode("NODE[@ID='CSL_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_CSL_VAL =" + lMin + "]")!=null)
					{
						minLimit = nodeMapCode.SelectSingleNode("NODE[@ID='CSL_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_CSL_VAL =" + lMin + "]/@QQ_CSL_VAL").InnerText.ToString();
					}
					
					//Appending the Nodelist
					limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = minLimit;
				}
				//UMCSL
				if(objClsCoveragesInfo.COVERAGE_CODE == "UMCSL")
				{
					string minLimit = "";
					int lMin = int.Parse(limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString());
					
					if(nodeMapCode.SelectSingleNode("NODE[@ID='UMCSL_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UMCSL_VAL =" + lMin + "]")!=null)
					{
						minLimit = nodeMapCode.SelectSingleNode("NODE[@ID='UMCSL_COVERAGE_TYPE']/ATTRIBUTES[@ACORD_UMCSL_VAL =" + lMin + "]/@QQ_UMCSL_VAL").InnerText.ToString();
					}
					
					//Appending the Nodelist
					limitNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = minLimit;
				}
				//COLL
				if(objClsCoveragesInfo.COVERAGE_CODE == "COLL")
				{
					string mindeduc = "";
					
					collType = OptNodes.Item(0).SelectSingleNode("OptionCd").InnerText.ToString();
					if(OptNodes.Item(0).SelectSingleNode("OptionCd").InnerText.ToString()!=null)
					{
						if(collType == "R")
						{
							int DucVal = int.Parse(DucNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString());
							if(DucNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.ToString()!=null)
							{
								if(nodeMapCode.SelectSingleNode("NODE[@ID='COLL_COVERAGE_TYPE']/ATTRIBUTES[@ACCORD_COLL_VAL =" + DucVal + "]")!=null)
								{
									mindeduc = nodeMapCode.SelectSingleNode("NODE[@ID='COLL_COVERAGE_TYPE']/ATTRIBUTES[@ACCORD_COLL_VAL =" + DucVal + "]/@QQ_COLL_VAL").InnerText.ToString().Trim();
									DucNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = mindeduc;
									objClsCoveragesInfo.DEDUCTIBLE1_AMOUNT_TEXT="Regular";
								}
							}
						}
						else if(collType == "B")
						{
							objClsCoveragesInfo.DEDUCTIBLE1_AMOUNT_TEXT="Broad";
						}
						else if(collType == "L")
						{
							objClsCoveragesInfo.DEDUCTIBLE1_AMOUNT_TEXT="Limited";
							DucNodes.Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = "0";
						}
					}
				}
				// Loan/Lease
				if(objClsCoveragesInfo.COVERAGE_CODE == "LLGC")
				{
					if(objCovNode.SelectSingleNode("CoverageCd")!=null)
						objClsCoveragesInfo.LIMIT1_AMOUNT_TEXT=objCovNode.SelectSingleNode("CoverageCd").InnerText.Trim();
				}
				//******************************End Mapping Limits**********************
				#endregion MAPPING COVERGAES	

				int i = 0;
				if(limitNodes != null)
				{
					foreach(XmlNode limitNode in limitNodes)
					{
						if(i < 2)
						{
							dataNode = limitNode.SelectSingleNode("FormatCurrencyAmt/Amt");
							
							if(i == 0)
							{
								if(dataNode != null)
								{
									try
									{
										objClsCoveragesInfo.LIMIT_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}
									catch(FormatException )
									{
										//objClsCoveragesInfo.LIMIT_1 = 0;
										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
									}				
									catch(InvalidCastException )
									{
										//objClsCoveragesInfo.LIMIT_1 = 0;
										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
									}
										
								}
									
								dataNode = limitNode.SelectSingleNode("Text");
									
								if(dataNode != null)
								{
									objClsCoveragesInfo.LIMIT1_AMOUNT_TEXT = dataNode.InnerText.Trim();
								}

							}

							if(i == 1)
							{
								if(dataNode != null)
								{
									try
									{
										objClsCoveragesInfo.LIMIT_2 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}
									catch(FormatException )
									{
										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
									}				
									catch(InvalidCastException )
									{
										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
									}
								}

								dataNode = limitNode.SelectSingleNode("Text");
									
								if(dataNode != null)
								{
									objClsCoveragesInfo.LIMIT2_AMOUNT_TEXT = dataNode.InnerText.Trim();
								}
							}
						
							//alCoverages.Add(objClsCoveragesInfo);
							i++;
						}
					}
				}
				i = 0;

				//Deductibles
				XmlNodeList dedNodes = objCovNode.SelectNodes("Deductible");

									
				if(dedNodes != null)
				{
					foreach(XmlNode dedNode in dedNodes)
					{
						if(i < 2)
						{
							dataNode = dedNode.SelectSingleNode("FormatCurrencyAmt/Amt");
							
							if(i == 0)
							{
								if(dataNode != null)
								{
									try
									{
										objClsCoveragesInfo.DEDUCTIBLE_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}
									catch(FormatException )
									{
										throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
									}				
									catch(InvalidCastException )
									{
										throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
									}
								}

								dataNode = dedNode.SelectSingleNode("Text");
									
								if(dataNode != null)
								{
									objClsCoveragesInfo.DEDUCTIBLE1_AMOUNT_TEXT = dataNode.InnerText.Trim();
								}
								////PIP MAPING
								if(objClsCoveragesInfo.COVERAGE_CODE == "PIP")
								{
									string strPIP = "",strAmtText="";
									if(dedNode.NextSibling.FirstChild.NextSibling!=null)
									{
										if(dedNode.NextSibling.FirstChild.NextSibling.LocalName == "OptionCd")
										{
											dataNode = dedNode.NextSibling.FirstChild.NextSibling;
											strPIP = dataNode.InnerText.Trim();
											if(nodeMapCode.SelectSingleNode("NODE[@ID='PIP_COVERAGE']/ATTRIBUTES[@ACORDPIPCODE ='" + strPIP + "']")!=null)
												strAmtText = nodeMapCode.SelectSingleNode("NODE[@ID='PIP_COVERAGE']/ATTRIBUTES[@ACORDPIPCODE ='" + strPIP + "']/@MAKEAPPCODE").InnerText.ToString();
											
												if(strPIP.ToUpper().Trim()=="M"||strPIP.ToUpper().Trim()=="MW")
												{
													if(intCtr==0)
													{
													if(nodHealthCare!=null)
														strAdd_Info=nodHealthCare.InnerText.Trim();
													}
													intCtr++;
												}																							
										}
										objClsCoveragesInfo.LIMIT1_AMOUNT_TEXT = strAmtText.ToString().Trim();
										objClsCoveragesInfo.ADD_INFORMATION = strAdd_Info;
									}
										//If No LocalName "OptionCd" does not exists : Set as FULL
									else
									{
										strAmtText = nodeMapCode.SelectSingleNode("NODE[@ID='PIP_COVERAGE']/ATTRIBUTES/@MAKEAPPCODEDEFAULT").InnerText.ToString();
										objClsCoveragesInfo.LIMIT1_AMOUNT_TEXT = strAmtText.ToString().Trim();
									}
								}


							}

							if(i == 1)
							{
								if(dataNode != null)
								{
									try
									{
										objClsCoveragesInfo.DEDUCTIBLE_2 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}
									catch(FormatException )
									{
										throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
									}			
									catch(InvalidCastException )
									{
										throw new Exception("Parse error: Invalid value in Deductible. Value: " + dataNode.InnerXml.Trim());
									}
								}

									
								dataNode = dedNode.SelectSingleNode("Text");
									
								if(dataNode != null)
								{
									objClsCoveragesInfo.DEDUCTIBLE2_AMOUNT_TEXT = dataNode.InnerText.Trim();
								}
							}
						
						
							i++;
						}
					}
				}
				#region LIMIT LOAN LEASED
				i = 0;
				//Loan Limits
				XmlNodeList loanLeaseNodes = objCovNode.SelectNodes("CurrentTermAmt");
				if(loanLeaseNodes != null)
				{
					foreach(XmlNode limitNode in loanLeaseNodes)
					{
						if(i < 2)
						{
							dataNode = limitNode.SelectSingleNode("CurrentTermAmt/Amt");
							
							if(i == 0)
							{
								if(dataNode != null)
								{
									try
									{
										objClsCoveragesInfo.LIMIT_1 = DefaultValues.GetDoubleFromString(dataNode.InnerXml);
									}
									catch(FormatException )
									{
										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
									}				
									catch(InvalidCastException )
									{
										throw new Exception("Parse error: Invalid value in Limit. Value: " + dataNode.InnerXml.Trim());
									}
										
								}
									
								dataNode = objCovNode.SelectSingleNode("CoverageCd");
								if(dataNode != null)
								{
									if(nodeMapCode.SelectSingleNode("NODE[@ID='LOAN_LEASE_TYPE']/ATTRIBUTES[@ACORD_LLGC_CODE ='" + dataNode.InnerText.ToString().Trim() + "']")!=null)
									{
										objClsCoveragesInfo.LIMIT1_AMOUNT_TEXT =  nodeMapCode.SelectSingleNode("NODE[@ID='LOAN_LEASE_TYPE']/ATTRIBUTES[@ACORD_LLGC_CODE ='" + dataNode.InnerText.ToString().Trim() + "']/@MAKEAPPCODE").InnerText.ToString();
									}
								}

							}
						
							i++;
						}
					}
				}

				#endregion
				alCoverages.Add(objClsCoveragesInfo);
			}
				if(collType=="R")
				{
					Cms.Model.Application.ClsCoveragesInfo objClsCoveragesInfo = new Cms.Model.Application.ClsCoveragesInfo();
			
					objClsCoveragesInfo.COVERAGE_CODE="RCC68";
					objClsCoveragesInfo.COV_DESC="Regular Collision Coverage A-68";
					objClsCoveragesInfo.LIMIT1_AMOUNT_TEXT="";
					objClsCoveragesInfo.LIMIT2_AMOUNT_TEXT="";
					alCoverages.Add(objClsCoveragesInfo);
				}				
	
			return alCoverages;
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
			
			if(objAddIntNodes == null)
				return null;

			ArrayList alAddInt = new ArrayList();

			foreach(XmlNode objIntNode in objAddIntNodes)
			{
				ClsAdditionalInterestInfo onjIntInfo = new ClsAdditionalInterestInfo();

				//currentNode = objIntNode.SelectSingleNode("AdditionalInterest");
					
				dataNode = objIntNode.SelectSingleNode("AdditionalInterestInfo/NatureInterestCd");
					
				if(dataNode != null)
				{
					onjIntInfo.NATURE_OF_INTEREST = dataNode.InnerText.Trim(); 
				}

				currentNode = objIntNode.SelectSingleNode("GeneralPartyInfo/NameInfo");
					
				if(currentNode != null)
				{
					dataNode = currentNode.SelectSingleNode("CommlName/CommercialName");

					if(dataNode != null)
					{
						onjIntInfo.HOLDER_NAME = ClsCommon.DecodeXMLCharacters (dataNode.InnerXml.Trim());	
					}
				}					
				if(onjIntInfo.HOLDER_NAME == null || onjIntInfo.HOLDER_NAME == "")
				{
					throw new Exception("Holder Name cannot be empty in ACORD XML");

				}
				currentNode = objIntNode.SelectSingleNode("GeneralPartyInfo/Addr");				
				if(currentNode != null)
				{
					dataNode = currentNode.SelectSingleNode("Addr1");
					if(dataNode != null)
					{
						onjIntInfo.HOLDER_ADD1 = dataNode.InnerXml.Trim();	
					}
					dataNode = currentNode.SelectSingleNode("Addr2");
					if(dataNode != null)
					{
						onjIntInfo.HOLDER_ADD2 = dataNode.InnerXml.Trim();	
					}
					dataNode = currentNode.SelectSingleNode("City");
					if(dataNode != null)
					{
						onjIntInfo.HOLDER_CITY = dataNode.InnerXml.Trim();	
					}
					dataNode = currentNode.SelectSingleNode("StateProv");
					if(dataNode != null)
					{
						onjIntInfo.HOLDER_STATE = dataNode.InnerXml.Trim();	
					}					
					dataNode = currentNode.SelectSingleNode("PostalCode");
					if(dataNode != null)
					{
						onjIntInfo.HOLDER_ZIP = dataNode.InnerXml.Trim();	
					}
				}	
				alAddInt.Add(onjIntInfo);
			}
			return alAddInt;
		}
		

		/// <summary>
		///  
		/// </summary>
		/// <param name="node"></param>
		/// <param name="xPath"></param>
		/// <returns></returns>
		public Addr[] ParseAddress(XmlNode node, string xPath)
		{
			//Get the address details////////
			XmlNodeList nodeList = node.SelectNodes(xPath);
			
			ArrayList alAddress = new ArrayList();

			foreach(XmlNode addrNode in nodeList)
			{
				Addr objAddr = new Addr();

				dataNode = addrNode.SelectSingleNode("AddrTypeCd");
				
				if(dataNode != null)
				{
					objAddr.AddrTypeCd = dataNode.InnerText;
				}

				dataNode = addrNode.SelectSingleNode("Addr1");

				if(dataNode != null)
				{
					objAddr.Addr1 = dataNode.InnerText;
				}
				
				dataNode = addrNode.SelectSingleNode("Addr2");
				
				if(dataNode != null)
				{
					objAddr.Addr2 = dataNode.InnerText;
				}
				
				dataNode = addrNode.SelectSingleNode("City");
				
				if(dataNode != null)
				{
					objAddr.City = dataNode.InnerText;
				}

				dataNode = addrNode.SelectSingleNode("StateProvCd");
				
				if(dataNode != null)
				{
					objAddr.StateProv = dataNode.InnerText;
				}

				dataNode = addrNode.SelectSingleNode("PostalCode");
				
				if(dataNode != null)
				{
					objAddr.PostalCode = dataNode.InnerText;
				}
				
				dataNode = addrNode.SelectSingleNode("County");
				
				if(dataNode != null)
				{
					objAddr.County = dataNode.InnerText;
				}

				alAddress.Add(objAddr);
			}

			return (Addr[])alAddress.ToArray(typeof(Addr));
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
			
			if(subLocList == null) 
				return null;
			
			foreach(XmlNode subLocNode in subLocList)
			{
				this.dataNode = subLocNode.SelectSingleNode("SubLocationDesc");				
				ClsSubLocationInfo objInfo = new ClsSubLocationInfo();				
				if(dataNode != null)
				{
					objInfo.SUB_LOC_DESC = dataNode.InnerText.Trim();
				}				
				if(objNode.Attributes["id"] != null)
				{
					objInfo.ID = objNode.Attributes["id"].Value;
				}
				if(alSubloc == null)
				{
					alSubloc = new ArrayList();
				}
				alSubloc.Add(objInfo);
			}			
			return alSubloc;
		}		
		
		#endregion

		#region Save 	
		
		/// <summary>
		/// Saves Personal Vehicle details
		/// </summary>
		/// <param name="objInfo"></param>
		/// <returns></returns>
		public int SavePersVehicle(ClsVehicleInfo objClsVehicleInfo)
		{
			ClsVehicleInformation objBLL = new ClsVehicleInformation();
			//To be chk later
			//objClsVehicleInfo.CREATED_BY= Convert.ToInt32(this.UserID);
			objClsVehicleInfo.CUSTOMER_ID = objClsCustomerInfo.CustomerId;
			objClsVehicleInfo.APP_ID = objClsGeneralInfo.APP_ID;
			objClsVehicleInfo.APP_VERSION_ID = objClsGeneralInfo.APP_VERSION_ID;
			objClsVehicleInfo.GRG_ADD1 = objClsCustomerInfo.CustomerAddress1;
			objClsVehicleInfo.GRG_ADD2 = objClsCustomerInfo.CustomerAddress2;
			objClsVehicleInfo.GRG_CITY = objClsCustomerInfo.CustomerCity;
			objClsVehicleInfo.GRG_COUNTRY = "1";

			//string getStatecode = 
			objClsVehicleInfo.GRG_STATE = GetStateCode(this.objClsCustomerInfo.CustomerState).ToString();
				
//				this.objClsCustomerInfo.CustomerState;

			objClsVehicleInfo.REGISTERED_STATE = GetStateCode(this.objClsCustomerInfo.CustomerState).ToString();
				//this.objClsCustomerInfo.CustomerState;
			
//			//Check vehicle Type Com or Per
//			if(objClsGeneralInfo.APP_LOB == "AutoP")
//					objClsVehicleInfo.VEHICLE_TYPE_PER =  1209;
//			if(objClsGeneralInfo.APP_LOB == "AutoC")
//				objClsVehicleInfo.VEHICLE_TYPE_PER =  1210;


			
			//Add Garaging information
			for ( int i = 0; i < this.alLocations.Count; i++)
			{
				ClsLocationInfo objLoc = (ClsLocationInfo)alLocations[i];

				if(objLoc.ID == objClsVehicleInfo.LOCATION_REF && objLoc.ADDR_TYPE == "GaragingAddress")
				{	
					objClsVehicleInfo.GRG_ZIP = objClsLocationInfo.LOC_ZIP;
					objClsVehicleInfo.TERRITORY = objClsLocationInfo.TERRITORY;
				}
			}
			
			if(objClsVehicleInfo.GRG_ZIP == null || objClsVehicleInfo.GRG_ZIP == "")
			{
				objClsVehicleInfo.GRG_ZIP = objClsCustomerInfo.CustomerZip;
			}

			try
			{
				objDataWrapper.ClearParameteres();				
				int val = objBLL.AddVehicle(objClsVehicleInfo,objDataWrapper);
				objClsVehicleInfo.VEHICLE_ID = val;			
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);			
			}	
			return 1;
		}

		/// <summary>
		/// Saves Coverages
		/// </summary>
		/// <returns></returns>
		public int SaveCoverages()
		{
			try
			{
				ClsVehicleCoverages objBLL = new ClsVehicleCoverages();
				//Save Coverages info
				for(int i = 0; i < alPersVehicle.Count; i++)
				{
					ClsVehicleInfo objClsVehicleInfo = (ClsVehicleInfo)alPersVehicle[i];					
					ArrayList alCoverages = objClsVehicleInfo.GetCoverages();				
					if(alCoverages == null) continue;
					for(int j = 0; j < alCoverages.Count; j++)
					{
						Cms.Model.Application.ClsCoveragesInfo objCoverage = (Cms.Model.Application.ClsCoveragesInfo)alCoverages[j];
						if(objCoverage == null) continue;
						objCoverage.APP_ID = objClsVehicleInfo.APP_ID;
						objCoverage.APP_VERSION_ID  = objClsVehicleInfo.APP_VERSION_ID;
						objCoverage.CUSTOMER_ID = objClsVehicleInfo.CUSTOMER_ID;	
						objCoverage.RISK_ID = objClsVehicleInfo.VEHICLE_ID;
						// To be checked later
						///objCoverage.CREATED_BY  =Convert.ToInt32(this.UserID);
					}

					objBLL.SaveDefaultCoveragesApp(objDataWrapper,objClsVehicleInfo.CUSTOMER_ID,objClsVehicleInfo.APP_ID,
						objClsVehicleInfo.APP_VERSION_ID,objClsVehicleInfo.VEHICLE_ID);  
					
					objBLL.InvalidateInitialisation();
					objBLL.UpdateCoveragesByRuleApp(objDataWrapper,objClsVehicleInfo.CUSTOMER_ID,objClsVehicleInfo.APP_ID,objClsVehicleInfo.APP_VERSION_ID,
						RuleType.MakeAppDependent,objClsVehicleInfo.VEHICLE_ID);
					
					int retVal = objBLL.SaveAcordVehicleCoverages(alCoverages,objDataWrapper);
					//Coverage Rule Implementation :
					objBLL.InvalidateInitialisation();

					objBLL.UpdateCoveragesByRuleApp(objDataWrapper,objClsVehicleInfo.CUSTOMER_ID,objClsVehicleInfo.APP_ID,objClsVehicleInfo.APP_VERSION_ID,
						RuleType.RiskDependent,objClsVehicleInfo.VEHICLE_ID);
				
					objBLL.UpdateCoveragesByRuleApp(objDataWrapper,objClsVehicleInfo.CUSTOMER_ID,objClsVehicleInfo.APP_ID,objClsVehicleInfo.APP_VERSION_ID,
						RuleType.AutoDriverDep,objClsVehicleInfo.VEHICLE_ID);
					objDataWrapper.ClearParameteres();				
				}
			}
			catch(Exception exc)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();				
				addInfo.Add("Err Descriptor ","Error while Saving Coverages.");				
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc,addInfo);

			}
			return 1;
		}	


		/// <summary>
		/// Saves Driver details
		/// </summary>
		/// <returns></returns>
		public int SaveDriverDetails()
		{
			ClsDriverDetail objBLL = new ClsDriverDetail();			
			//Save Driver details
			for(int i = 0; i < this.alDrivers.Count; i++)
			{
				ClsDriverDetailsInfo objClsDriverDetailsInfo = (ClsDriverDetailsInfo)alDrivers[i];					
				if(objClsDriverDetailsInfo.DRIVER_FNAME == null || objClsDriverDetailsInfo.DRIVER_FNAME == "")
				{
					System.Web.HttpContext.Current.Response.Write("<br>Driver first Name cannot be empty.");
					throw new Exception("Driver First Name cannot be empty.");
				}				
				if(objClsDriverDetailsInfo.DRIVER_LNAME == null || objClsDriverDetailsInfo.DRIVER_LNAME == "")
				{
					System.Web.HttpContext.Current.Response.Write("<br>Driver Last Name cannot be empty.");
					throw new Exception("Driver Last Name cannot be empty.");
				}
				objClsDriverDetailsInfo.APP_ID = objClsGeneralInfo.APP_ID;
				objClsDriverDetailsInfo.APP_VERSION_ID  = objClsGeneralInfo.APP_VERSION_ID;
				objClsDriverDetailsInfo.CUSTOMER_ID = objClsCustomerInfo.CustomerId;				

//				objClsDriverDetailsInfo.DRIVER_ADD1 = objClsCustomerInfo.CustomerAddress1;
//				objClsDriverDetailsInfo.DRIVER_ADD2 = objClsCustomerInfo.CustomerAddress2;
//				objClsDriverDetailsInfo.DRIVER_CITY = objClsCustomerInfo.CustomerCity;
//				objClsDriverDetailsInfo.DRIVER_STATE = objClsCustomerInfo.CustomerState;
//				objClsDriverDetailsInfo.DRIVER_LIC_STATE = this.objClsCustomerInfo.CustomerState;
//				objClsDriverDetailsInfo.DRIVER_ZIP = this.objClsCustomerInfo.CustomerZip;
				objClsDriverDetailsInfo.DRIVER_COUNTRY = "1";
				objClsDriverDetailsInfo.DRIVER_US_CITIZEN = "1";
				objClsDriverDetailsInfo.DRIVER_DRINK_VIOLATION = "0";
				objClsDriverDetailsInfo.DRIVER_VOLUNTEER_POLICE_FIRE = "0";
				//Assign vehicles
				if(this.alPersVehicle != null && alPersVehicle.Count > 0)
				{
					for(int j =0; j < alPersVehicle.Count; j++)
					{	
						ClsVehicleInfo objClsVehicleInfo = (ClsVehicleInfo)alPersVehicle[j];

						//get vehicle ID from Acord XML:
						string strID = objClsVehicleInfo.ID.Substring(7);
						string vehAcordID = strID.ToString();
						//End

						if(objClsDriverDetailsInfo.VEHICLEID == vehAcordID)
						{
							objClsDriverDetailsInfo.VEHICLEID = objClsVehicleInfo.VEHICLE_ID.ToString();
						}
					}
				}
				
				int driverID = objBLL.CheckDriverExistence(objClsDriverDetailsInfo,objDataWrapper);				
				objDataWrapper.ClearParameteres();
				objClsDriverDetailsInfo.DRIVER_ID = driverID;
				string firstName = objClsDriverDetailsInfo.DRIVER_FNAME;
				string lastName = objClsDriverDetailsInfo.DRIVER_LNAME;
				
				if(firstName.Length > 2 && lastName.Length > 2)
				{
					objClsDriverDetailsInfo.DRIVER_CODE = objClsDriverDetailsInfo.DRIVER_FNAME.Substring(0,2) + objClsDriverDetailsInfo.DRIVER_LNAME.Substring(0,2) + "000001";
				}
				//To be chk later
				//objClsDriverDetailsInfo.CREATED_BY = Convert.ToInt32(this.UserID);
				objBLL.SaveDriverDetailsCapitalAcord(objClsDriverDetailsInfo,objDataWrapper);								
				objDataWrapper.ClearParameteres();
			}
			return 1;
		}


		/// <summary>
		/// Saves the Driver violations from Quick quote in the database
		/// </summary>
		/// <returns></returns>
		public int SaveDriverViolations()
		{
			try
			{
				if(this.alDriverViolations != null && alDriverViolations.Count > 0)
				{
					for(int i =0; i < alDriverViolations.Count; i++)
					{
						ClsMvrInfo objClsMvrInfo = (ClsMvrInfo)alDriverViolations[i];
						ClsMvrInformation objBLL = new ClsMvrInformation();	
						objClsMvrInfo.APP_ID = this.objClsGeneralInfo.APP_ID;
						objClsMvrInfo.APP_VERSION_ID  = objClsGeneralInfo.APP_VERSION_ID;
						objClsMvrInfo.CUSTOMER_ID = objClsCustomerInfo.CustomerId;	
						// to be chk later
						//objClsMvrInfo.CREATED_BY = Convert.ToInt32(this.UserID);

						for(int j = 0; j < this.alDrivers.Count ; j++)
						{
							ClsDriverDetailsInfo objDriver = (ClsDriverDetailsInfo)alDrivers[j];							
							if(objClsMvrInfo.DRIVER_REF == objDriver.ID)
							{
								objClsMvrInfo.DRIVER_ID = objDriver.DRIVER_ID;
								if(objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.AAF)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.AAFD)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.AAFPD)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.AAFPI)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.ACC)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.ANFEPD)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.AVAA)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.DFWARW)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.DPDA)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.DPIA)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.FA)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.FAE)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.FANE)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.FMSFA)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.FSRAWA)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.VRA)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.ANFEPI)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.ANFED)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == ((int)(ACCIDENT_ID.ANFE)).ToString()
									|| objClsMvrInfo.VIOLATION_CODE == "COMP"
								)
								{
									objBLL.SavePriorLossAccidentVehicle(objClsGeneralInfo.APP_LOB,objClsMvrInfo,this.objDataWrapper);
									this.objDataWrapper.ClearParameteres();
									//have to handle this error later																
								}
								else
								{
									
									objBLL.SaveViolations(objClsMvrInfo,this.objDataWrapper);							
									this.objDataWrapper.ClearParameteres();
								}
							}
						}
					}
				}
				return 1;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Vehicle Age

		public static int VehicleAge(DateTime dtpQuoteEffDate, string Year, string VehicleUse)
		{
			int age =0;
				if (Year!= "")
                    {
                       //Check Effective Date : If Oct and Above then +1 Vehicle Age Else Vehicle Age
                           if(Year!="")
                                  age = dtpQuoteEffDate.Year - int.Parse(Year);
                           if (age < 0)
                                  age = 0;
                           int AgeToCompare = 0;
                           if(VehicleUse.ToUpper() == "PersVeh")
                               AgeToCompare = 5; //Personal
                           else
                               AgeToCompare = 3; //Commercial
								int modelMonth = 10; //Model Month
								int CurrentMonth = dtpQuoteEffDate.Month;
                            if(age>AgeToCompare)
                              {                             
                                 if(CurrentMonth < modelMonth)
                                   {
                                      age = age+1;      
                                   }
                                 else
                                   {
                                       age = age+2;      
                                   }
                              }
                              else
                              {
                                 if(Year == "")
                                    {
                                        age=0;
                                    }
                                 else
                                     if(CurrentMonth < modelMonth)
	                                    {
	                                         age = age+1;
	                                    }
                                     else
                                        {
                                              age = age+2;
                                        }
                              }
                        }
                        else
                           age=0;
			return age;
		}
# endregion

	}
}
