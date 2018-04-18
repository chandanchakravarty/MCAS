/******************************************************************************************
<Author					: -		Pradeep Iyer
<Start Date				: -		11/18/2005
<End Date				: -	
<Description			: - 	Parses the contents of the Quick quote for Watercraft.
<Review Date			: - 
<Reviewed By			: - 	

*******************************************************************************************/ 

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Cms.Model;
using Cms.Model.Client;
using Cms.Model.Application;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application.PrivatePassenger;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.DataLayer;
using Cms.Model.Application.Watercrafts;

namespace Cms.CmsWeb
{

	/// <summary>
	/// This class is used to parse the ACORD XML for the Home LOB
	/// </summary>

	public class ClsWatercraftParser : AcordParserBase
	{

		//11766	EL	Electronic
		//11767	OT	Others
		//Changed By Ravindra(08-24-2006)
		//private const int UNIQUEID_FOR_ELECTRONIC_EQUIPMENT =11561;
		private const int UNIQUEID_FOR_ELECTRONIC_EQUIPMENT =11766;
		private const int UNIQUEID_FOR_OTHER_EQUIPMENT =11767;
		private const int UNIQUEID_FOR_MALE =6615;
		private const int UNIQUEID_FOR_FEMALE =6614;
		private const int UNIQUEID_FOR_YES =10963;
		private const int UNIQUEID_FOR_NO =10964;
		private const int UNIQUEID_FOR_PRINCIPAL =11936;
		private const int UNIQUEID_FOR_OCCASIONAL =11937 ;
		private const int UNIQUEID_FOR_SERIOUS_VIOLATION=11410;
		private const int INCHES = 12;

		private const string 	ACCIDENT_INDIANA = "15052";
		private const string 	ACCIDENT_MICHIGAN = "15053";
		private const string 	ACCIDENT_WISCONSIN = "15054";


		public const string ViolationInformation = "PersPolicy/AccidentViolation";

		//Added By Ravindra (08-24-2006) For Equipment Type
		public enum EquipmentType
		{
			DPSD = 8746,  //Depth Sounder	
			STSR = 8747,  //Ship to Shore	
			LNS  = 11452, //Loran Navigation System	
			GPS  = 11453, //GPS System	
			SSC  = 11454,  //Shore Station	
			Others = -1

		}
		

		public enum CoverageTypeBasis
		{
			ACV=11758,
			AGV=11759,
			ANA=11978
		}
		
		public ClsWatercraftParser()
		{
		}
		
		public override AcordBase Parse()
		{
			//No of request in the XML
			XmlNodeList InsuranceSvcRq = GetApplicationNodeList();
			
			ClsAcordWatercraft objApplication = null;

			foreach(XmlNode node in InsuranceSvcRq)
			{
				//XmlNodeList appNodeList = node.SelectNodes("HomePolicyQuoteInqRq");
				XmlNodeList appNodeList = node.SelectNodes("WatercraftPolicyQuoteInqRq");

				objApplication = new ClsAcordWatercraft();

				foreach(XmlNode nodeApp in appNodeList)
				{
					//Parse Agency info
					ClsAgencyInfo objInfo = this.ParseProducer(nodeApp);
					objApplication.objAgency = objInfo;

					//Parse customer details
					Cms.Model.Client.ClsCustomerInfo objCustomer = ParseInsuredOrPrincipal(nodeApp);
					objApplication.objCustomer = objCustomer;
					
					//Parse application info
					ClsGeneralInfo objApp = ParsePersPolicy(nodeApp);
					objApplication.objApplication = objApp;

					//Prior carrier info
					ClsPriorPolicyInfo objPriorPolicy = ParseOtherOrPriorPolicy(nodeApp);
					objApplication.objPriorPolicy = objPriorPolicy;
					
					//Parse Watercraft
					ArrayList alWater = ParseWatercraft(nodeApp);
					objApplication.alWatercrafts = alWater;

				
					//Parse Operators/////////////////////
					ArrayList alOperators = this.ParsePersDriver(nodeApp);
					objApplication.alOperators = alOperators;
					///////////////////////////////////

					//Driver Violations
					ArrayList alViolations = this.ParseDriverViolations(nodeApp);
					objApplication.alDriverViolations = alViolations;

					//Parse Equipments
					ArrayList alEquipments = this.ParseEquipments(nodeApp);
					objApplication.alEquipments = alEquipments;

					//gen info : 15 feb 2006
					//General Information
					ClsWatercraftGenInfo objWC = this.ParseGenInfo(nodeApp);
					objApplication.objWGenInfo = objWC;


					
				}
			}
			
			return objApplication;
		}
		
		/// <summary>
		/// Parses the watercraft node and populates the model object.
		/// </summary>
		/// <param name="nodeApp"></param>
		/// <returns></returns>
	
		public ArrayList ParseWatercraft(XmlNode nodeApp)
		{
			XmlNodeList waterNodes = nodeApp.SelectNodes("WatercraftLineBusiness/Watercraft");
			
			ArrayList alWatercraft = new ArrayList();
			
			foreach(XmlNode waterNode in waterNodes)
			{
				ClsWatercraftInfo objWater = new ClsWatercraftInfo();
				
				if ( waterNode.Attributes["id"] != null )
				{
					objWater.ID = waterNode.Attributes["id"].Value;
				}
				
				
				//Changed By Ravindra Coverage Type Basis Will be imported from the selected value 
				// at QQ 
				//COV_TYPE_BASIS should be default to Actual Cash Value
				//objWater.COV_TYPE_BASIS = 11758;
				dataNode = waterNode.SelectSingleNode("CoverageTypeBasisCd");
				if(dataNode != null)
				{
					string strCoverageTypeBasisCd=dataNode.InnerText.Trim();
					if(strCoverageTypeBasisCd == CoverageTypeBasis.ACV.ToString())
					{
						objWater.COV_TYPE_BASIS=Convert.ToInt32(CoverageTypeBasis.ACV);
					}
					if(strCoverageTypeBasisCd == CoverageTypeBasis.AGV.ToString())
					{
						objWater.COV_TYPE_BASIS = Convert.ToInt32(CoverageTypeBasis.AGV);
					}
					if(strCoverageTypeBasisCd == CoverageTypeBasis.ANA.ToString())
					{
						objWater.COV_TYPE_BASIS = Convert.ToInt32(CoverageTypeBasis.ANA);
					}
				}


				
				dataNode = waterNode.SelectSingleNode("WaterUnitTypeCd");

				if ( dataNode != null)
				{
					objWater.TYPE_OF_WATERCRAFT = dataNode.InnerText.Trim();
				}
				
				// Trailer Deductible Information - Asfa Praveen - 02-July-2007

				if(objWater.TYPE_OF_WATERCRAFT == "TRAI" || objWater.TYPE_OF_WATERCRAFT == "JT" || objWater.TYPE_OF_WATERCRAFT == "WRT")
				{
					dataNode = waterNode.SelectSingleNode("TrailerDedId");

					if ( dataNode != null)
					{
						objWater.TRAILER_DED_ID = Convert.ToInt32(dataNode.InnerText.Trim());
					}

					dataNode = waterNode.SelectSingleNode("TrailerDed");

					if ( dataNode != null)
					{
						objWater.TRAILER_DED = Convert.ToDouble(dataNode.InnerText.Trim());
					}
					
					dataNode = waterNode.SelectSingleNode("TralierDedTxt");

					if ( dataNode != null)
					{
						objWater.TRAILER_DED_AMOUNT_TEXT = dataNode.InnerText.Trim();
					}
				}

				dataNode = waterNode.SelectSingleNode("Length/NumUnits");

				if ( dataNode != null)
				{
					//Convert Inches to Ft (Map with database)
					//QQ nodes conatin value in Inches..So while make app Convert to Ft.
					int intFt = Convert.ToInt32(dataNode.InnerText.Trim()) / INCHES;
					objWater.LENGTH= intFt.ToString().Trim();
				}
				
				dataNode = waterNode.SelectSingleNode("Horsepower/NumUnits");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						try
						{
							objWater.WATERCRAFT_HORSE_POWER = Convert.ToInt32(dataNode.InnerText.Trim());
						}
						catch(Exception ex)
						{
							throw new Exception("Parse error: Unable to parse Horse Power." + ex.Message);
						}

					}

				}

				dataNode = waterNode.SelectSingleNode("WatersNavigatedCd");

				if ( dataNode != null)
				{
					objWater.WATERS_NAVIGATED = dataNode.InnerText.Trim();
				}
				
				dataNode = waterNode.SelectSingleNode("Speed/NumUnits");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						try
						{
							objWater.MAX_SPEED = Convert.ToDouble(dataNode.InnerText.Trim());
						}
						catch(Exception ex)
						{
							throw new Exception("Parse error: Unable to parse Speed." + ex.Message);
						}

					}

				}
				
				dataNode = waterNode.SelectSingleNode("PresentValueAmt/Amt");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						try
						{
							objWater.INSURING_VALUE  = Convert.ToDouble(dataNode.InnerText.Trim());
						}
						catch(Exception ex)
						{
							throw new Exception("Parse error: Unable to parse Present Value." + ex.Message);
						}

					}

				}
// Commented by Swastika on 5th May'06 for Gen Iss 2674 &  2675

//				dataNode = waterNode.SelectSingleNode("WeightCapacity/NumUnits");
//
//				if ( dataNode != null)
//				{
//					if ( dataNode.InnerText.Trim() != "" )
//					{
//						objWater.WEIGHT =dataNode.InnerText.Trim();
//						
//					}
//
//				}

				dataNode = waterNode.SelectSingleNode("Make");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						objWater.MAKE =dataNode.InnerText.Trim();
						
					}

				}
				
				dataNode = waterNode.SelectSingleNode("Model");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						objWater.MODEL =dataNode.InnerText.Trim();
						
					}

				}
				
				dataNode = waterNode.SelectSingleNode("SerialNumber");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						objWater.HULL_ID_NO =dataNode.InnerText.Trim();
						
					}

				}
				
				dataNode = waterNode.SelectSingleNode("Year");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						try
						{
							objWater.YEAR = Convert.ToInt32(dataNode.InnerText.Trim());
						}
						catch(Exception ex)
						{
							throw new Exception("Parse error: Unable to parse Year." + ex.Message);
						}
					}

				}
				
		
				dataNode = waterNode.SelectSingleNode("HullMaterialTypeCd");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						
						objWater.HULL_MATERIAL_CODE = dataNode.InnerText.Trim();
						
					}

				}
				//Added by Asfa Praveen - 29/June/2007
				dataNode = waterNode.SelectSingleNode("HullMaterialTypeCd");

				if ( dataNode != null)
				{
					if ( dataNode.InnerText.Trim() != "" )
					{
						
						objWater.HULL_MATERIAL_CODE = dataNode.InnerText.Trim();
						
					}

				}

				

				//County
				dataNode = waterNode.SelectSingleNode("County");
				if(dataNode !=null)
				{
					objWater.TERRITORY = dataNode.InnerText.Trim();
				}

				// credits and discounts
				//int intForYes = 10963; //Lookup Unique id for Yes
				//int intForNo = 10964;  //Lookup Unique id for No
					
				dataNode = waterNode.SelectSingleNode("CreditSurcharge[@id='DIESELENGINE']");
				if(dataNode !=null)
				{
					objWater.FUEL_TYPE = 3725;
					objWater.DIESEL_ENGINE = UNIQUEID_FOR_YES ;
				}
				else
				{
					objWater.DIESEL_ENGINE = UNIQUEID_FOR_NO ;
				}
				dataNode = waterNode.SelectSingleNode("CreditSurcharge[@id='SHORESTATION']");
				if(dataNode !=null)
				{							
					objWater.SHORE_STATION = UNIQUEID_FOR_YES ;
				}
				else
				{
					objWater.SHORE_STATION = UNIQUEID_FOR_NO ;
				}
					
				dataNode = waterNode.SelectSingleNode("CreditSurcharge[@id='HALONFIRE']");
				if(dataNode !=null)
				{
					objWater.HALON_FIRE_EXT_SYSTEM = UNIQUEID_FOR_YES ;
				}
				else
				{
					objWater.HALON_FIRE_EXT_SYSTEM = UNIQUEID_FOR_NO ;
				}
				dataNode = waterNode.SelectSingleNode("CreditSurcharge[@id='LORANNAVIGATIONSYSTEM']");
				if(dataNode !=null)
				{							 
					objWater.LORAN_NAV_SYSTEM = UNIQUEID_FOR_YES ;
				}
				else
				{
					objWater.LORAN_NAV_SYSTEM = UNIQUEID_FOR_NO ;
				}
				dataNode = waterNode.SelectSingleNode("CreditSurcharge[@id='DUALOWNERSHIP']");
				if(dataNode !=null)
				{							 
					objWater.DUAL_OWNERSHIP = UNIQUEID_FOR_YES ;
				}
				else
				{
					objWater.DUAL_OWNERSHIP = UNIQUEID_FOR_NO ;
				}
				dataNode = waterNode.SelectSingleNode("CreditSurcharge[@id='REMOVESAILBOAT']");
				if(dataNode !=null)
				{							 
					objWater.REMOVE_SAILBOAT  = UNIQUEID_FOR_YES ;
				}
				else
				{
					objWater.REMOVE_SAILBOAT  = UNIQUEID_FOR_NO ;
				}
					
					 
			 

				//Parse coverages for this watercraft**********************************
				ArrayList alCoverages = this.ParseCoverages(waterNode);
				objWater.SetCoverages(alCoverages);

				//*********************************************************************

				alWatercraft.Add(objWater);

			}

			return alWatercraft;

		}


		/// <summary>
		/// Parses the operators of the watercraft
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ArrayList ParsePersDriver(XmlNode node)
		{
			ArrayList alDrivers = new ArrayList();
			XmlNode nodAllForViolation = node;

			//XmlNodeList objDriverList = node.SelectNodes("PersAutoLineBusiness/PersDriver");
			XmlNodeList objDriverList = node.SelectNodes("WatercraftLineBusiness/PersDriver");	
			
			if ( objDriverList == null ) return null;

			foreach(XmlNode objDriverNode in objDriverList)
			{
				ClsWatercraftOperatorInfo objDriver = new ClsWatercraftOperatorInfo();	
				//ClsDriverDetailsInfo objDriver = new ClsDriverDetailsInfo();
					
				if ( objDriverNode.Attributes["id"] != null )
				{
					objDriver.ID = objDriverNode.Attributes["id"].Value;
				}

				//Getting the name of customer/////////////
				currentNode = objDriverNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName");

				//XmlNode dataNode;
					
				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("Surname");

					if ( dataNode != null )
					{	
						objDriver.DRIVER_LNAME = dataNode.InnerText.Trim();
					}
			
					dataNode = currentNode.SelectSingleNode("GivenName");

					if ( dataNode != null )
					{	
						objDriver.DRIVER_FNAME = dataNode.InnerText.Trim();
					}

					dataNode = currentNode.SelectSingleNode("OtherGivenName");

					if ( dataNode != null )
					{	
						objDriver.DRIVER_MNAME = dataNode.InnerText.Trim();
					}

					//End of name
				}
					
				if ( objDriver.DRIVER_LNAME == null || objDriver.DRIVER_LNAME == "" )
				{
					throw new Exception("Driver Last Name is empty in XML.");
				}
					
				if ( objDriver.DRIVER_FNAME == null || objDriver.DRIVER_FNAME == "" )
				{
					throw new Exception("Driver First Name is empty in XML.");
				}

				//Get the address details////////
				XmlNodeList nodeList = objDriverNode.SelectNodes("GeneralPartyInfo/Addr");
			
				foreach(XmlNode addrNode in nodeList)
				{
					string addrType  = "";
				
					dataNode = addrNode.SelectSingleNode("AddrTypeCd");
				
					if ( dataNode != null )
					{
						addrType = dataNode.InnerText;
					}

					if ( addrType == "StreetAddress")
					{
						XmlNode addrDataNode = addrNode.SelectSingleNode("Addr1");

						if ( addrDataNode != null )
						{
							objDriver.DRIVER_ADD1 = addrDataNode.InnerText;
						}
					
						addrDataNode = addrNode.SelectSingleNode("Addr2");
					
						if ( addrDataNode != null )
						{
							objDriver.DRIVER_ADD2 = addrDataNode.InnerText;
						}
					
						addrDataNode = addrNode.SelectSingleNode("City");
					
						if ( addrDataNode != null )
						{
							objDriver.DRIVER_CITY = addrDataNode.InnerText;
						}

						addrDataNode = addrNode.SelectSingleNode("State");
					
						if ( addrDataNode != null )
						{
							objDriver.DRIVER_STATE = addrDataNode.InnerText;
						}

						addrDataNode = addrNode.SelectSingleNode("PostalCode");
					
						if ( addrDataNode != null )
						{
							objDriver.DRIVER_ZIP = addrDataNode.InnerText;
						}

					}
			
				}
				
				//Person Info
				currentNode = objDriverNode.SelectSingleNode("DriverInfo/PersonInfo");
					
				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("GenderCd");
				
					if ( dataNode != null )
					{
						objDriver.DRIVER_SEX_CODE = dataNode.InnerText;

						objDriver.DRIVER_SEX = objDriver.DRIVER_SEX_CODE;
						/*if ( objDriver.DRIVER_SEX_CODE == "M" )
						{
							objDriver.DRIVER_SEX = UNIQUEID_FOR_MALE;
						}
						else if (objDriver.DRIVER_SEX_CODE == "F" )
						{
							objDriver.DRIVER_SEX = UNIQUEID_FOR_FEMALE;
						}*/

					}
					//
					dataNode = currentNode.SelectSingleNode("MaritalStatusCd");
			
					if ( dataNode != null )
					{
						objDriver.MARITAL_STATUS = dataNode.InnerText;


					}
		
					dataNode = currentNode.SelectSingleNode("BirthDt");
				
					if ( dataNode != null )
					{
						objDriver.DRIVER_DOB = DefaultValues.GetDateFromString(dataNode.InnerText);
					}

					
				}

				//Licence Info
				currentNode = objDriverNode.SelectSingleNode("DriverInfo/DriversLicense");
					
				if ( currentNode != null )
				{
					
				
					dataNode = currentNode.SelectSingleNode("DriversLicenseNumber");
				
					if ( dataNode != null )
					{
						objDriver.DRIVER_DRIV_LIC = dataNode.InnerText;
					}

					dataNode = currentNode.SelectSingleNode("StateProv");
				
					if ( dataNode != null )
					{
						objDriver.DRIVER_LIC_STATE = dataNode.InnerText;
					}

					dataNode = currentNode.SelectSingleNode("YearsLicensed");
					if ( dataNode != null )
					{
						objDriver.YEARS_LICENSED  = Convert.ToInt16(dataNode.InnerText);
					}
				}
				
				//Question Answer
				currentNode = objDriverNode.SelectSingleNode("DriverInfo/QuestionAnswer");
				
				if ( currentNode != null )
				{
					string strPowerSquadronCourse="";
					string strCoastGuardAuxService="";
					string strPowFiveYearsOperatorExp="";

					dataNode = currentNode.SelectSingleNode("PowerSquadronCourse");
					if ( dataNode != null )
					{
						strPowerSquadronCourse = dataNode.InnerText.ToString().ToUpper().Trim();
					}
					
					dataNode = currentNode.SelectSingleNode("CoastGuardAuxService");
					if ( dataNode != null )
					{
						strCoastGuardAuxService = dataNode.InnerText.ToString().ToUpper().Trim();
					}

					dataNode = currentNode.SelectSingleNode("FiveYearsOperatorExp");
					if ( dataNode != null )
					{
						strPowFiveYearsOperatorExp = dataNode.InnerText.ToString().ToUpper().Trim();
					}
					
					if (strPowFiveYearsOperatorExp.Trim() != "")
					{
						objDriver.DRIVER_COST_GAURAD_AUX = int.Parse(strPowFiveYearsOperatorExp.Trim());
					}

					
					//11 may 2006 (Operator level)
					// Did you take a Water Safety Course --1
					if (strPowerSquadronCourse == "Y")
						objDriver.WAT_SAFETY_COURSE = 10963; //Yes
					else
						objDriver.WAT_SAFETY_COURSE = 10964;  //NO
					//Do you have a Certificate for Coast guard or Power Squadron Course --2
					if (strCoastGuardAuxService == "Y")
						objDriver.CERT_COAST_GUARD = 10963;  //Yes
					else
						objDriver.CERT_COAST_GUARD = 10964;  //No


				}

				//PersDriver Info
				currentNode = objDriverNode.SelectSingleNode("PersDriverInfo");
				
				if ( currentNode != null )
				{
					if ( currentNode.Attributes["VehPrincipallyDrivenRef"] != null )
					{
						objDriver.VEHICLEID = currentNode.Attributes["VehPrincipallyDrivenRef"].Value; 
					}
								
					dataNode = currentNode.SelectSingleNode("DriverTypeCd");
				
					if ( dataNode != null )
					{
						if ( dataNode.InnerText.ToLower().Trim() == "principal")
						{
							objDriver.APP_VEHICLE_PRIN_OCC_ID = UNIQUEID_FOR_PRINCIPAL;
						}
						else if (dataNode.InnerText.ToLower().Trim() == "occasional")
						{
							objDriver.APP_VEHICLE_PRIN_OCC_ID = UNIQUEID_FOR_OCCASIONAL;
						}
								
					}

				}
				
				//Get the phone details
				nodeList = objDriverNode.SelectNodes("GeneralPartyInfo/Communications/PhoneInfo");
			
				foreach(XmlNode phoneNode in nodeList)
				{
					string phoneType  = "";
				
					dataNode = phoneNode.SelectSingleNode("PhoneTypeCd");
				
					if ( dataNode != null )
					{
						phoneType = dataNode.InnerText;
					}
				
					dataNode = phoneNode.SelectSingleNode("CommunicationUseCd");
				
					string commType = "";

					if ( dataNode  != null  )
					{
						commType = dataNode.InnerText;
					}

					switch(phoneType)
					{
						case "Phone":
							if ( commType == "Home" )
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if ( dataNode != null )
								{
									//objDriver.DRIVER_HOME_PHONE = dataNode.InnerText;
								}
							}
						
							if ( commType == "Business" )
							{
								dataNode = phoneNode.SelectSingleNode("PhoneNumber");

								if ( dataNode != null )
								{
									//objDriver.DRIVER_BUSINESS_PHONE = dataNode.InnerText;
								}
							}
							break;
						case "Cell":
							
							dataNode = phoneNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								//objDriver.DRIVER_MOBILE = dataNode.InnerText;
							}
							

							break;
						
					}	

				}
			
				//Added By Kranti Checking Violations			 
				
				XmlAttribute tempAttr = objDriverNode.Attributes["id"];
				string tempattribute="";
				ClsCommon ObjClsCommon = new ClsCommon();
				if (tempAttr !=null)
					tempattribute = tempAttr.Value.ToString();
				string strTemp =ViolationInformation+"[@DriverRef='"+tempattribute +"']";

				XmlNodeList tempNodes = nodAllForViolation.SelectNodes(strTemp);
				if (tempNodes != null && tempNodes.Count >0)
				{
					//objDriver.VIOLATIONS = ObjClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES ;
					objDriver.VIOLATIONS = 10963;
				}
				else
				{
					//objDriver.VIOLATIONS = ObjClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO ;
					objDriver.VIOLATIONS = 10964;
				}

				alDrivers.Add(objDriver);	
			}

			return alDrivers;
			
		}

		

		/// <summary>
		/// Parses the equipments of the watercraft
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ArrayList ParseEquipments(XmlNode node)
		{
			ArrayList alEquipments = new ArrayList();
			XmlNode tempNode;
			 
			XmlNodeList objEquipmentList = node.SelectNodes("WatercraftLineBusiness/WatercraftAccessory");	
			
			if ( objEquipmentList == null ) return null;

			foreach(XmlNode objEquipmentNode in objEquipmentList)
			{
				ClsWatercraftEquipmentsInfo objEquipment = new ClsWatercraftEquipmentsInfo();	
					
				//Getting the Equipment Type. If "Y" then unique id of Electronic Equipment in the Lookup Values table else 0
				currentNode = objEquipmentNode.SelectSingleNode("EquipmentTypeCd");	
			 

				if ( currentNode != null )
				{
					//if Sample then proceed
					if (currentNode.InnerText.Trim().ToUpper() == "SAMPLE")
					{
						continue;
					}


					if (currentNode.InnerText.Trim().ToUpper() == "Y")
					{
						objEquipment.EQUIPMENT_TYPE   = UNIQUEID_FOR_ELECTRONIC_EQUIPMENT;
					}
					else
					{
					
						objEquipment.EQUIPMENT_TYPE   = UNIQUEID_FOR_OTHER_EQUIPMENT;
					}
				}

				//Changed By Ravindra (08-24-2006)
				//Getting the description
				
				currentNode = objEquipmentNode.SelectSingleNode("Make");
				if ( currentNode != null )
				{
					
					//objEquipment.MAKE  = currentNode.InnerText.Trim();
					string strEQUIPMENT_TYPE=currentNode.InnerText.Trim();
					if(strEQUIPMENT_TYPE == EquipmentType.DPSD.ToString())
						objEquipment.EQUIP_TYPE=Convert.ToInt32(EquipmentType.DPSD) ;
					else if(strEQUIPMENT_TYPE == EquipmentType.GPS .ToString())
						objEquipment.EQUIP_TYPE=Convert.ToInt32(EquipmentType.GPS);
					else if(strEQUIPMENT_TYPE == EquipmentType.LNS.ToString())
						objEquipment.EQUIP_TYPE=Convert.ToInt32(EquipmentType.LNS);
					else if(strEQUIPMENT_TYPE == EquipmentType.SSC .ToString())
						objEquipment.EQUIP_TYPE=Convert.ToInt32(EquipmentType.SSC);
					else if(strEQUIPMENT_TYPE == EquipmentType.STSR.ToString())
						objEquipment.EQUIP_TYPE=Convert.ToInt32(EquipmentType.STSR);
					//In case of Equipo Type Other Import ID as -1
					else if(strEQUIPMENT_TYPE == EquipmentType.Others.ToString())
						objEquipment.EQUIP_TYPE=Convert.ToInt32(EquipmentType.Others);

				}
				currentNode = objEquipmentNode.SelectSingleNode("OtherDescription");
				if ( currentNode != null )
				{
					objEquipment.OTHER_DESCRIPTION  = currentNode.InnerText.Trim();
				}
				 
				//Changed By Ravindra Ends Here

				//Getting the Serial Number
				currentNode = objEquipmentNode.SelectSingleNode("SerialNumber");
				if ( currentNode != null )
				{
					objEquipment.SERIAL_NO  = currentNode.InnerText.Trim();
				}

				//Getting the Deductible and Insuring Value
				currentNode = objEquipmentNode.SelectSingleNode("Coverage");
				if ( currentNode != null )
				{
					//deductible
					tempNode = objEquipmentNode.SelectSingleNode("Coverage/Deductible/FormatCurrencyAmt/Amt");
					if(tempNode!=null && tempNode.InnerText.Trim().ToUpper()!= "NONE" && tempNode.InnerText.ToString()!="")
					{
						objEquipment.EQUIP_AMOUNT = Convert.ToInt32(tempNode.InnerText.Trim());
					}
					
					//Insuring Value
					tempNode = objEquipmentNode.SelectSingleNode("Coverage/Limit/FormatCurrencyAmt/Amt");
					if(tempNode!=null)
					{
						objEquipment.INSURED_VALUE = Convert.ToInt32(tempNode.InnerText.Trim());
					}
				}

				

				
			
				 
					alEquipments.Add(objEquipment);	
				 
			}

			return alEquipments;
			
		}

		//Parse gen info : 15 feb 2006
		/// <summary>
		/// Parses Gen Info
		/// </summary>
		/// <param name="nodeApp"></param>
		/// <returns></returns>
		public ClsWatercraftGenInfo ParseGenInfo(XmlNode nodeApp)
		{
			XmlNode policyNode = nodeApp.SelectSingleNode("PersPolicy");
				
			ClsWatercraftGenInfo objInfo = null;

			if ( policyNode != null )
			{
				objInfo = new ClsWatercraftGenInfo();

				dataNode = policyNode.SelectSingleNode("CreditSurcharge[@id='BOATHOMEDISCOUNT']");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText.Trim().ToLower() == "y" )
					{
						objInfo.BOAT_HOME_DISCOUNT = "1";
						objInfo.MULTI_POLICY_DISC_APPLIED = "1";
					}
					else if (dataNode.InnerText.Trim().ToLower() == "n")
					{
						objInfo.BOAT_HOME_DISCOUNT = "0";
						objInfo.MULTI_POLICY_DISC_APPLIED = "0";
					}
				}
			
			}
			
			//Modified by Mohit Agarwal 22-Nov-2006
			XmlNodeList waterNodes = nodeApp.SelectNodes("WatercraftLineBusiness/Watercraft");
			
			ArrayList alWatercraft = new ArrayList();
			
			foreach(XmlNode waterNode in waterNodes)
			{

				dataNode = waterNode.SelectSingleNode("CreditSurcharge[@id='DUALOWNERSHIP']");
				if(dataNode !=null)
				{							 
					objInfo.IS_BOAT_COOWNED = "1";
					break;
				}
				else
				{
					objInfo.IS_BOAT_COOWNED = "0";
				}
			}
			
			// Modified by Mohit Agarwal 27-Nov-2006
			string effDate = DateTime.Now.ToString(); //QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
			DateTime tempDate = DateTime.Now;
			string violationCode="";
			DateTime violationDate=DateTime.Now;

			//This code is to get effective date only
			XmlNode effDtNode = nodeApp.SelectSingleNode("PersPolicy/ContractTerm");
			if(effDtNode != null)
			{
				dataNode = effDtNode.SelectSingleNode("EffectiveDt");

				if ( dataNode != null )
				{
					tempDate = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
				}				

			}		



			XmlNodeList violationList = nodeApp.SelectNodes("PersPolicy/AccidentViolation");

			objInfo.MINOR_VIOLATION="0";
			objInfo.DRINK_DRUG_VOILATION="0";
			if ( violationList != null ) 
			{
				

				foreach(XmlNode violNode in violationList )
				{
					int vio_found=0;
					dataNode = violNode.SelectSingleNode("AccidentViolationCd");

					if ( dataNode != null )
					{
						violationCode= dataNode.InnerText.Trim();
						vio_found = 1;
					}
					dataNode = violNode.SelectSingleNode("AccidentViolationDt");

					if ( dataNode != null )
					{
						violationDate = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
						if(vio_found == 1)
							vio_found = 2;
					}
					effDate = tempDate.Month.ToString() + "//" + tempDate.Day.ToString() + "//" + (tempDate.Year -3).ToString();
					if(vio_found == 2 && violationCode!=UNIQUEID_FOR_SERIOUS_VIOLATION.ToString() && violationDate >= Convert.ToDateTime(effDate))
						objInfo.MINOR_VIOLATION="1";

					effDate = tempDate.Month.ToString() + "//" + tempDate.Day.ToString() + "//" + (tempDate.Year -5).ToString();
					if(vio_found == 2 && violationCode==UNIQUEID_FOR_SERIOUS_VIOLATION.ToString() && violationDate >= Convert.ToDateTime(effDate))
						objInfo.DRINK_DRUG_VOILATION="1";

				}
			}

			return objInfo;
		}


	}
		//
	
	



	/// <summary>
	/// Summary description for ClsAcordWatercraft.
	/// </summary>
	public class ClsAcordWatercraft : AcordBase
	{
		public ArrayList alWatercrafts;
		public ArrayList alOperators;



		private const string 	ACCIDENT_INDIANA = "15052";
		private const string 	ACCIDENT_MICHIGAN = "15053";
		private const string 	ACCIDENT_WISCONSIN = "15054";

		public ClsAcordWatercraft()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/// <summary>
		/// Imports the data for Watercrat application
		/// </summary>
		/// <returns></returns>
		public override ClsGeneralInfo Import()
		{
			objDataWrapper = new DataWrapper(ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
			
			try
			{
//				int agencyID = ClsAgency.GetAgencyID(objAgency,objDataWrapper);
//				
//				if ( agencyID == -1 )
//				{
//					//System.Web.HttpContext.Current.Response.Write("<br>Agency not found in the database.");
//					throw new Exception("Agency not found in the database.");
//				}
//			
//				objDataWrapper.ClearParameteres();
//
//				objAgency.AGENCY_ID = agencyID;
				
				if ( this.objCustomer.CustomerId == -1 || this.objCustomer.CustomerId == 0 )
				{
					SaveCustomer();
			
					objDataWrapper.ClearParameteres();
				}

				/* When Making an application against a watercraft QQ, check if it is attached to a home application.
				 * If it is attached to a home app,then set retVal as -1 and values for appId and appVersionID same as that of home 
				 * else check application existance and proceed.
				 */

				
				//check if attached to home policy
				//if yes then do not save application  set the foll values 
                     //this.objApplication.APP_VERSION_ID = home app version id
					//this.objApplication.APP_ID = home app id
				//else
					//SaveApplication()
				SaveApplication();

				objDataWrapper.ClearParameteres();
				
				//Save Watercrafts
				if ( alWatercrafts != null & alWatercrafts.Count > 0 )
				{
					//Save Vehicles
					for(int i = 0; i < alWatercrafts.Count; i++ )
					{
						ClsWatercraftInfo objInfo = (ClsWatercraftInfo)alWatercrafts[i];

						//If trailer type then save to trailer screen : 13 feb 2006 : Praveen K
						//Trailer (TRAI)
						//Jetski Trailer (JT)
						//Waverunner trailer (WRT)
						if(objInfo.TYPE_OF_WATERCRAFT == "TRAI" || objInfo.TYPE_OF_WATERCRAFT == "JT" || objInfo.TYPE_OF_WATERCRAFT == "WRT")
						{
							//ClsWatercraftTrailerInfo objInfoTrailer = (ClsWatercraftTrailerInfo)alWatercrafts[i];
							ClsWatercraftTrailerInfo objInfoTrailer = new ClsWatercraftTrailerInfo();
						   	objInfoTrailer.MANUFACTURER = objInfo.MAKE;
							objInfoTrailer.YEAR = objInfo.YEAR;
							objInfoTrailer.INSURED_VALUE = objInfo.INSURING_VALUE;
							objInfoTrailer.TRAILER_TYPE_CODE = objInfo.TYPE_OF_WATERCRAFT;
							objInfoTrailer.SERIAL_NO = objInfo.HULL_ID_NO;
							objInfoTrailer.MODEL = objInfo.MODEL;
							
							//Trailer Deductible Information - Asfa Praveen - 02-July-2007
							objInfoTrailer.TRAILER_DED_ID= objInfo.TRAILER_DED_ID;
							objInfoTrailer.TRAILER_DED= objInfo.TRAILER_DED;
							objInfoTrailer.TRAILER_DED_AMOUNT_TEXT= objInfo.TRAILER_DED_AMOUNT_TEXT;
							//end
							
							if (objInfo.TYPE_OF_WATERCRAFT == "JT")
							{
								//Ski jet trailor
								objInfoTrailer.TRAILER_TYPE_CODE = "11761";
							}
							else
							{
								//All other trailor
								objInfoTrailer.TRAILER_TYPE_CODE = "11760";
							}
								
											
							//Save Trailer Type
							SaveTrailer(objInfoTrailer);
							objDataWrapper.ClearParameteres();
						}//End Trailer type
						else 
						{
							//Save Watercraft Info
							SaveWatercraft(objInfo);
							objDataWrapper.ClearParameteres();
					
						}

					}
				
					objDataWrapper.ClearParameteres();
					SaveCoverages();
					objDataWrapper.ClearParameteres();

				}
				
				
				if ( this.alOperators != null && alOperators.Count > 0 )
				{
					this.SaveOperators();
				
					objDataWrapper.ClearParameteres();

					//Save Driver Violations
					SaveDriverViolations();

					objDataWrapper.ClearParameteres();



				}

				// Import Equipments
				if ( alEquipments!= null & alEquipments.Count > 0 )
				{
					//Loop for each Equipment 
					for(int i = 0; i < alEquipments.Count; i++ )
					{
						ClsWatercraftEquipmentsInfo objInfo = (ClsWatercraftEquipmentsInfo)alEquipments[i];
					
						//Save Equipments
						SaveEquipments(objInfo);

						objDataWrapper.ClearParameteres();
					}
				}
				//General Inforamtion : 15 feb 2006
					SaveGenInfo(objWGenInfo);
				//
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				

				if ( ex.InnerException != null )
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex.InnerException);
					throw(ex.InnerException);
				}
				else
				{
					throw(ex);
				}

				
			}
			
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return this.objApplication;
		}
		

		/// <summary>
		/// Saves the coverages for the watercraft in the database
		/// </summary>
		/// <returns></returns>
		private int SaveCoverages()
		{

			//Save Coverages info
			for(int i = 0; i < this.alWatercrafts.Count; i++ )
			{
				ClsWatercraftCoverages objBLL = new ClsWatercraftCoverages();
				ClsWatercraftInfo objInfo = (ClsWatercraftInfo)alWatercrafts[i];
					
				ArrayList alCoverages = objInfo.GetCoverages();
				
				if ( alCoverages == null ) continue;

				for(int j = 0; j < alCoverages.Count; j++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objCoverage = (Cms.Model.Application.ClsCoveragesInfo)alCoverages[j];
					
					if ( objCoverage == null ) continue;

					objCoverage.APP_ID = objInfo.APP_ID;
					objCoverage.APP_VERSION_ID  = objInfo.APP_VERSION_ID;
					objCoverage.CUSTOMER_ID = objInfo.CUSTOMER_ID;	
					objCoverage.RISK_ID = objInfo.BOAT_ID;
					objCoverage.CREATED_BY  =Convert.ToInt32(this.UserID);		
				}

				int retVal = 0;
				
				if(objInfo.TYPE_OF_WATERCRAFT != "TRAI" && objInfo.TYPE_OF_WATERCRAFT != "JT" && objInfo.TYPE_OF_WATERCRAFT != "WRT")
				{
					objBLL.createdby = Convert.ToInt32(this.UserID);	
					retVal =objBLL.SaveWatercraftCoveragesAcord(alCoverages,"",objDataWrapper);
					objDataWrapper.ClearParameteres();
					objBLL.UpdateCoveragesByRuleApp(objDataWrapper,objInfo.CUSTOMER_ID,
													objInfo.APP_ID,objInfo.APP_VERSION_ID,
													RuleType.RiskDependent ,objInfo.BOAT_ID );
					objDataWrapper.ClearParameteres();
				}
	
				if ( retVal == -1 )
				{
					
					//System.Web.HttpContext.Current.Response.Write("Coverage code " + objCoverage
				}
			}

			return 1;
		}
		
		
		/// <summary>
		/// Saves a Watercraft record from Quick quote
		/// </summary>
		/// <param name="objInfo"></param>
		public void SaveWatercraft(ClsWatercraftInfo objInfo)
		{
			objInfo.CREATED_BY= Convert.ToInt32(this.UserID);
			objInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
			objInfo.APP_ID = this.objApplication.APP_ID;
			objInfo.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
			objInfo.STATE_REG = this.objCustomer.CustomerState;	
			
			// taking customer Addreess
			objInfo.LOCATION_ADDRESS = this.objCustomer.CustomerAddress1;
			objInfo.LOCATION_CITY = this.objCustomer.CustomerCity;
			objInfo.LOCATION_STATE = this.objCustomer.CustomerState;	
			objInfo.LOCATION_ZIP = this.objCustomer.CustomerZip;
			
			
			// Commented by Swastika on 5th May'06 for Gen Iss 2674 &  2675
			//objInfo.OTHER_HULL_TYPE = "";
			objInfo.BERTH_LOC = "";

			Cms.BusinessLayer.BlApplication.clsWatercraftInformation objBLL = new clsWatercraftInformation();
		
				objBLL.AddWatercraftAcord(objInfo, this.objDataWrapper);
			
		}
		/// <summary>
		/// Saves a Trailer record from Quick quote : 13 feb 2006 Praveen K
		/// </summary>
		/// <param name="objInfo"></param>
		public void SaveTrailer(ClsWatercraftTrailerInfo objInfoTrailer)
		{
			objInfoTrailer.CREATED_BY= Convert.ToInt32(this.UserID);
			objInfoTrailer.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
			objInfoTrailer.APP_ID = this.objApplication.APP_ID;
			objInfoTrailer.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
			

			Cms.BusinessLayer.BlApplication.clsWatercraftInformation objBLL = new clsWatercraftInformation();
		
			objBLL.AddWatercraftTrailerAcord(objInfoTrailer, this.objDataWrapper);
			
		}
		/// <summary>
		/// Saves a General Info : 15 feb 2006 Praveen K
		/// </summary>
		/// <param name="objInfo"></param>
		public void SaveGenInfo(ClsWatercraftGenInfo objWGenInfo)
		{
			ClsWatercraftGenInformation objWat = new ClsWatercraftGenInformation();
//			ClsWatercraftGenInfo objWGenInfo = new ClsWatercraftGenInfo();
            
			if (objWGenInfo!=null)
			{
				objWGenInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
				objWGenInfo.APP_ID = this.objApplication.APP_ID;
				objWGenInfo.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
				objWGenInfo.CREATED_BY = Convert.ToInt32(this.UserID);
			}

			
			//CHANGES DONE BY RPSINGH -- DUE TO CHANGE IN IMPLEMENTATION AT APP LEVEL

			//////If Customer have Homeowners policy in prior then Underwriting Question 
			//////"Does Wolverine Insure the Homeowners Policy? "at application level should default to Yes.
			////int retValue=ClsWatercraftGenInformation.CheckExistancePolicyHome(this.objApplication.CUSTOMER_ID );
			////if ( retValue >= 1 )
			////{
			////	objWGenInfo.MULTI_POLICY_DISC_APPLIED = "1";
			////}
			////else
			////{
			////	objWGenInfo.MULTI_POLICY_DISC_APPLIED = "0";
			////
			////}
			
			
			string strHomeAppNumner = new clsapplication().getWaterCraftHomeAttachedForQQ(this.objApplication.CUSTOMER_ID,
																this.objApplication.APP_ID,
																this.objApplication.APP_VERSION_ID);

			if (strHomeAppNumner.StartsWith("H"))
			{
				objWGenInfo.MULTI_POLICY_DISC_APPLIED			= "1";
				objWGenInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC	= strHomeAppNumner;
			}
//			else
//			{
//				objWGenInfo.MULTI_POLICY_DISC_APPLIED			= "0";
//				objWGenInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC	= "";
//			}
			//End of addition by RPSINGH
            		
			objWat.Save(objWGenInfo,objDataWrapper);
            
		}
		

		/// <summary>
		/// Saves the operator information in the database.
		/// </summary>
		/// <returns></returns>
		public int SaveOperators()
		{
			ClsDriverDetail objBLL = new ClsDriverDetail();
			
			//Save Driver details
			for(int i = 0; i < this.alOperators.Count; i++ )
			{
				ClsWatercraftOperatorInfo objInfo = (ClsWatercraftOperatorInfo)alOperators[i];
					
				if ( objInfo.DRIVER_FNAME == null || objInfo.DRIVER_FNAME == "" )
				{
					System.Web.HttpContext.Current.Response.Write("<br>Driver first Name cannot be empty.");
					throw new Exception("Driver First Name cannot be empty.");
				}
				
				if ( objInfo.DRIVER_LNAME == null || objInfo.DRIVER_LNAME == "" )
				{
					System.Web.HttpContext.Current.Response.Write("<br>Driver Last Name cannot be empty.");
					throw new Exception("Driver Last Name cannot be empty.");
				}

				objInfo.APP_ID = this.objApplication.APP_ID;
				objInfo.APP_VERSION_ID  = objApplication.APP_VERSION_ID;
				objInfo.CUSTOMER_ID = objApplication.CUSTOMER_ID;	
				objInfo.DRIVER_ADD1 = this.objCustomer.CustomerAddress1;
				objInfo.DRIVER_ADD2 = this.objCustomer.CustomerAddress2;
				objInfo.DRIVER_CITY = this.objCustomer.CustomerCity;
				objInfo.DRIVER_STATE = this.objCustomer.CustomerState;
				objInfo.DRIVER_LIC_STATE = this.objCustomer.CustomerState;
				objInfo.DRIVER_ZIP = this.objCustomer.CustomerZip;
				objInfo.DRIVER_COUNTRY = "1";
				objInfo.CREATED_DATETIME = DateTime.Now;
				

				//objInfo.DRIVER_US_CITIZEN = "1";
				//objInfo.DRIVER_DRINK_VIOLATION = "0";
				
				//Added By Kranti On 31may 2007 for SSN_NO				
				#region Taking customer detail from customer table based on customer id
				//Commented on 28 August 2009.
				//DataSet custds = ClsCustomer.GetCustomerDetails(objApplication.CUSTOMER_ID);

				//Added Praveen K on 28 August 2009
				DataSet custds = ClsCustomer.GetCustomerSSN(objApplication.CUSTOMER_ID);
				if(custds.Tables[0].Rows.Count > 0)
				{
					objInfo.DRIVER_SSN=custds.Tables[0].Rows[0]["SSN_NO"].ToString();

				}
				#endregion

				//Assign vehicles
				if ( this.alWatercrafts != null && alWatercrafts.Count > 0 )
				{
					for(int j =0; j < alWatercrafts.Count; j++ )
					{	
						ClsWatercraftInfo objVehInfo = (ClsWatercraftInfo)alWatercrafts[j];

						if ( objInfo.VEHICLEID == objVehInfo.ID )
						{
							objInfo.VEHICLEID = objVehInfo.BOAT_ID.ToString();
							objInfo.VEHICLE_ID = objVehInfo.BOAT_ID.ToString();
						}

					}
				}

				//int driverID = objBLL.CheckDriverExistence(objInfo,objDataWrapper);
				
				objDataWrapper.ClearParameteres();

				//objInfo.DRIVER_ID = driverID;

				string firstName = objInfo.DRIVER_FNAME;
				string lastName = objInfo.DRIVER_LNAME;
				objInfo.DRIVER_CODE = ClsCommon.GenerateRandomCode(firstName, lastName);
												
				/*if ( firstName.Length > 2 && lastName.Length > 2 )
				{
					objInfo.DRIVER_CODE = objInfo.DRIVER_FNAME.Substring(0,2) + objInfo.DRIVER_LNAME.Substring(0,2) + "000001";
				}*/
				objInfo.CREATED_BY = Convert.ToInt32(this.UserID);
				
				
				objBLL.AddOperator(objInfo,objDataWrapper);
				
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
			if ( this.alDriverViolations != null && alDriverViolations.Count > 0 )
			{
				for(int i =0; i < alDriverViolations.Count; i++ )
				{
					ClsMvrInfo objInfo = (ClsMvrInfo)alDriverViolations[i];
					
					objInfo.APP_ID = this.objApplication.APP_ID;
					objInfo.APP_VERSION_ID  = objApplication.APP_VERSION_ID;
					objInfo.CUSTOMER_ID = objApplication.CUSTOMER_ID;	
					objInfo.CREATED_BY = Convert.ToInt32(this.UserID);

					for(int j = 0; j < this.alOperators.Count ; j++ )
					{
						ClsWatercraftOperatorInfo objDriver = (ClsWatercraftOperatorInfo)alOperators[j];
							
						if ( objInfo.DRIVER_REF == objDriver.ID )
						{
							objInfo.DRIVER_ID = objDriver.DRIVER_ID;

							ClsMvrInformation objBLL = new ClsMvrInformation();
													
							//If Accident then Import to Prior Loss Tab Itrack # 
							if(objInfo.VIOLATION_ID == int.Parse(ACCIDENT_INDIANA.ToString())
								|| objInfo.VIOLATION_ID == int.Parse(ACCIDENT_MICHIGAN.ToString())
								|| objInfo.VIOLATION_ID == int.Parse(ACCIDENT_WISCONSIN.ToString())
								)
							{
                                objBLL.SavePriorLossAccident(objInfo,this.objDataWrapper);
								this.objDataWrapper.ClearParameteres();

							}
	
							objBLL.SaveWatercraftViolations(objInfo,this.objDataWrapper);
							
							this.objDataWrapper.ClearParameteres();
						}
					}


				}
			}

			return 1;
		}


		public int SaveEquipments(ClsWatercraftEquipmentsInfo  objInfo)
		{
				//setting information that is not captured while importing
				objInfo.CREATED_BY= Convert.ToInt32(this.UserID);
				objInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
				objInfo.APP_ID = this.objApplication.APP_ID;
				objInfo.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
				objInfo.IS_ACTIVE ="Y";
				objInfo.MODEL ="";
				objInfo.SHIP_TO_SHORE =0;
				objInfo.YEAR = 0;
				objInfo.ASSOCIATED_BOAT =0;
				objInfo.EQUIP_NO =0;
				
				
				//Calling the Add function to add in the database
				Cms.BusinessLayer.BlApplication.ClsWatercraftEquipment  objWatercraftEquipment= new ClsWatercraftEquipment();
				int retVal = objWatercraftEquipment.Add (objInfo, this.objDataWrapper);
				return retVal;
		}
	}


}


