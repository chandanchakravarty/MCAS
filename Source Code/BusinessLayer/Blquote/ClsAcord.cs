/******************************************************************************************
<Author					: -   Ashwini
<Start Date				: -	12/12/2006 3:13:20 PM
<End Date				: -	
<Description			: - 	This class is used for comperision of rates in accord xml
<Review Date			: - 
<Reviewed By			: 
******************************************************************************************/




using System;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;
using System.IO;
using Cms.Model.Quote;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.DataLayer;
using System.Data;
using System.Data.SqlClient;

namespace Cms.BusinessLayer.BlQuote
{
	/// <summary>
	/// Summary description for ClsAcord.
	/// </summary>
	public class ClsAcord :Cms.BusinessLayer.BlCommon.ClsCommon
	{
		#region Class variables
		private const string LOB_HOME="1";
		private const string LOB_PRIVATE_PASSENGER="2";
		private const string LOB_MOTORCYCLE="3";
		private const string LOB_WATERCRAFT="4";
		private const string LOB_UMBRELLA="5";		
		private const string LOB_RENTAL_DWELLING="6";
		string strRqUIDXmlNodes ="";
		string strCollegeDrvId = "";
		private const int WaverWorkLossAge=60;
		string strAcordInsDetail = "";
		string striNSU_rEQiD = "";
		string strAcordReqId="";
		#endregion

		private string mSystemID; 
		public ClsAcord(string SystemID )
		{
			mSystemID= SystemID; 
		}
		

		public string GetAcordXMLPremium(string strAcordXML,string AgencyCode)
		{
			string qqInputXML="";
			try
			{				
				string xslFileName_Auto_Path="",xslString="",acordInputXML="", premiumXML="",acordOutputXML="";//,xmlFilePath="";
				//AgencyCode="w001";

				if(AgencyExists(AgencyCode))
				{
					#region Save Acord Xml into Database
					strAcordXML=RemoveJunkXmlCharacters(strAcordXML);
					XmlDocument myXmlDocuments = new XmlDocument();
					myXmlDocuments.LoadXml(strAcordXML);
					XmlNode RqUIDXmlNodes = myXmlDocuments.SelectSingleNode("ACORD/InsuranceSvcRq/RqUID");                	
					strRqUIDXmlNodes =ReplaceEsqapeXmlCharacters(RqUIDXmlNodes.InnerText);
					// Check For App Exists
					if(AppExists(strRqUIDXmlNodes))
					{
						return "Application is already converted for this request, please resend request as new quote from Wolverine Carier Specific Questions.";
					}

					int intAgencyIDs = int.Parse(GetAgencyID(AgencyCode));
					SaveACORD_XML(ReplaceEsqapeXmlCharacters(strAcordXML),null,null,strRqUIDXmlNodes,intAgencyIDs);
					#endregion
				
				
					#region CONVERT THE ACORD INPUT XML INTO QQ XML
					// load the xsl file
					XmlDocument docXSLFile =new XmlDocument();
					xslFileName_Auto_Path = ClsCommon.GetKeyValueWithIP("AutoXSL_Capital2QQ_Path");
					docXSLFile.Load(xslFileName_Auto_Path);
					xslString = docXSLFile.InnerXml;
					string strPath  =  ClsCommon.GetKeyValueWithIP("QQAcordMapping_AUTO_path");
					//string strPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
					//strPath = strPath + xmlFilePath;
					xslString = xslString.Replace("FactorPath",strPath.Trim().ToString());
                    //System.Xml.Xsl.XslTransform xslt = new XslTransform();
                    System.Xml.Xsl.XslCompiledTransform xslt = new XslCompiledTransform();
					xslt.Load( new XmlTextReader( new StringReader(xslString)));
			
					//load the acord xml file
					StringWriter writer = new StringWriter();
					XmlDocument xmlDocTemp = new XmlDocument();				
					xmlDocTemp.LoadXml(strAcordXML);
					acordInputXML  = xmlDocTemp.OuterXml;	// Acord Input XML
				
					#region Adding URL Node : Added 30 JULY 2007 (Adding URL Node in Acord XML)
					//Get GUID form AcordXml:
					XmlNode guidNode = null;
					guidNode = xmlDocTemp.SelectSingleNode("ACORD/InsuranceSvcRq/RqUID");
					string strGuid  = guidNode.InnerText.ToString().Trim();
					//Get Path
					string PARAM  = System.Configuration.ConfigurationManager.AppSettings.Get("CapitalURL_Param");
                    string NODENAME = System.Configuration.ConfigurationManager.AppSettings.Get("CapitalURL_Acord_Node");
					string urlPath  =  ClsCommon.GetKeyValueWithIP("CapitalURL");
					urlPath = urlPath + "?" + PARAM + "=" + strGuid;
					//Append Child Nodes in Acord XML 
					XmlNode urlNode = null;
					urlNode = xmlDocTemp.SelectSingleNode("ACORD");
					XmlElement urlElement = xmlDocTemp.CreateElement(NODENAME);
					XmlText urlText = xmlDocTemp.CreateTextNode(urlPath);
					urlNode.AppendChild(urlElement);
					urlElement.AppendChild(urlText);
					acordInputXML  = xmlDocTemp.OuterXml;	// Acord Input XML
					#endregion


					// Transform the file and output an XML String 			
					XPathNavigator nav = ((IXPathNavigable) xmlDocTemp).CreateNavigator();
					xslt.Transform(nav,null,writer);
					qqInputXML = writer.ToString();			 // QQ Input XML
					writer.Close();

					#endregion

					#region GENERATE QUOTE AND GET THE PREMIUM XML				 
					ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote(mSystemID);
					Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralinformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
					//load the qqInputXML 
					//qqInputXML=qqInputXML.Replace("'","H673GSUYD7G3J73UDH");
					qqInputXML=qqInputXML.Replace("&gt;",">");
					qqInputXML=qqInputXML.Replace("&lt;","<");
					qqInputXML=qqInputXML.Replace("\"","'");
					qqInputXML=qqInputXML.Replace("\r","");
					qqInputXML=qqInputXML.Replace("\n","");
					qqInputXML=qqInputXML.Replace("\t","");	

					qqInputXML =qqInputXML.Replace("<?xml version='1.0' encoding='utf-16'?>","");
				
					// Fetch the  values from Master Tables DB
					qqInputXML=FetchXMLNodeValues(qqInputXML,strAcordXML);
					if(qqInputXML=="NoScore")
						return "Unable to order insurance score for this client. Please review insured address and try again. If your getting this error again plese contact system administrator.";
					qqInputXML = qqInputXML.ToUpper();
					qqInputXML=objGeneralinformation.SetAssignDriverAcciVioPointsCapitalRaterNode(qqInputXML,strCollegeDrvId);
					qqInputXML=qqInputXML.Replace("\"","'");		
					// 	GENERATE QUOTE AND GET THE PREMIUM XML
					string verificationHTML="",isValidInput="0",returnvalue="";					
					string strVerify =  ClsCommon.GetKeyValueForSetup("InputVerificationForQuote");//System.Configuration.ConfigurationSettings.AppSettings.Get("InputVerificationForQuote").ToString();
					if (strVerify.Trim().ToUpper()== "Y")
					{
						// verifying the input xml
						string retVal = objGenerateQuote.InputXmlVerification(qqInputXML,LOB_PRIVATE_PASSENGER); //return is in the format string#0 or string#1 . 0 --> invalid  1-->valid
						string[] retValue =retVal.Split('#');
						verificationHTML = retValue[0].ToString();
						isValidInput = retValue[1].ToString();						
				
						if(isValidInput == "1")
						{
							premiumXML = objGenerateQuote.GetQuoteXML(qqInputXML,LOB_PRIVATE_PASSENGER);
						}
						//4. If the input is valid then calculate the rate.
						if (isValidInput.Trim() == "0")
						{	
							returnvalue = "0^"+verificationHTML;
							return returnvalue;
				
						}
					}
					else 
					{
						premiumXML = objGenerateQuote.GetQuoteXML(qqInputXML,LOB_PRIVATE_PASSENGER);
					}

					#endregion
				
					#region SET VALUES IN THE ACORD XML AND RETURN THE XML
				
					acordOutputXML = objGenerateQuote.AttachPremiumToAcordInput(acordInputXML ,premiumXML,LOB_PRIVATE_PASSENGER,striNSU_rEQiD);
					acordOutputXML=acordOutputXML.Replace("\"","'");		
					xmlDocTemp.LoadXml(acordOutputXML);
					XmlNode nodtmp=xmlDocTemp.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CurrentTermAmt");
					XmlNode nodTerm=xmlDocTemp.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/ContractTerm/DurationPeriod/NumUnits");
					XmlNode nodPolicyEffDate=xmlDocTemp.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/ContractTerm/EffectiveDt");
					if(nodtmp!=null && nodTerm!=null && nodPolicyEffDate!=null)
					{
						if(nodtmp.InnerText!="")
						{
							if(nodTerm.InnerText!="")
							{
								string strPymentXML=objGeneralinformation.PaymentDistrbutionPlan(nodtmp.InnerText,nodTerm.InnerText,System.Convert.ToDateTime(nodPolicyEffDate.InnerText));
								acordOutputXML=objGenerateQuote.AttachPaymentPlanToAcordOutputXML(acordOutputXML,strPymentXML,LOB_PRIVATE_PASSENGER);
							}
						}

					}
					// Save here both QQ_XML and ACORD_XML in database
					
					XmlDocument myXmlDocument = new XmlDocument();
					myXmlDocument.LoadXml(qqInputXML);								
					XmlNode RqUIDXmlNode = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/RQUID");                	
					string strRqUIDXmlNode =RqUIDXmlNode.InnerText;
					//int intAgencyID = 0;// to be assign later
					SaveACORD_XMLQQ_XML(ReplaceEsqapeXmlCharacters(acordOutputXML),qqInputXML,strRqUIDXmlNode,intAgencyIDs,premiumXML);
					acordOutputXML=ReplaceEsqapeXmlCharacters(acordOutputXML);
					return acordOutputXML;
					#endregion	
				}
				else
				{
					//string RequestDetails="";
					//RequestDetails = "GUID= " + strRqUIDXmlNodes + "  :: AgencyCode = " + AgencyCode ; 
					//AddRequestLogForPremium(strRqUIDXmlNodes,RequestDetails,"noAgency","");
					return "noAgency";
				}
			}
			catch(Exception exc)
			{
				AddRequestLogForPremium(strRqUIDXmlNodes,"Rate Genration Error",exc.Message,qqInputXML);
				throw exc;
			}
			finally
			{}				
		}

		/// <summary>
		///  Function get the values from database on the bases of given code in ACORD xml for each LOB
		/// </summary>
		/// <param name="qqInputXML"></param>
		/// <returns></returns>
		private string FetchXMLNodeValues(string qqInputXML, string strAcordXML)
		{
			// common to each lob 
			qqInputXML = ReturnXML_Territory(qqInputXML);		

			XmlDocument myXmlDocument = new XmlDocument();
			myXmlDocument.LoadXml(qqInputXML);
			XmlNode node;			
			//get lob here 
			node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/LOBID");
			string strLOB = node.InnerText.Trim();	

			if(strLOB!="0")
			{		
				// switch case on the basis of the lob
				switch(strLOB)
				{
					case LOB_PRIVATE_PASSENGER:
						// code here 
						qqInputXML=FetchXMLNodeValues_PPA(qqInputXML,strAcordXML);
						break;
					case LOB_HOME:
						// code  here 
						break;
					case LOB_MOTORCYCLE:
						// code  here 
						break;
					case LOB_WATERCRAFT:
						// code  here 
						break;
					case LOB_RENTAL_DWELLING:
						// code  here 				
						break;
					case LOB_UMBRELLA:
						// code  here 
						break;
					default:
						break;
				}				
			} 
			return qqInputXML;				
		}
		/// <summary>
		/// returns xml with values from database
		/// </summary>
		/// <param name="qqInputXML"></param>
		/// <returns></returns>
		private string FetchXMLNodeValues_PPA(string qqInputXML, string strAcordXML)
		{
			try
			{
				qqInputXML=FetchAndSetInsuranceScore(qqInputXML,strAcordXML);
				if(qqInputXML=="NoScore")
					return qqInputXML;
				XmlDocument myXmlDocument = new XmlDocument();
				myXmlDocument.LoadXml(qqInputXML);
				XmlNode node,nodeDamageTotalAmt;
				int intState=0,intLOB=0;			
				string policyZip="", policyTerritory="",strChargableLoss="",strcontinousinsured="",strPolicyEffectivedate="";
				XmlDocument mapDoc = new XmlDocument();
				mapDoc.Load(ClsCommon.GetKeyValueWithIP("QQAcordMapping_AUTO_path"));

				XmlNodeList vehNodesForClass = myXmlDocument.SelectNodes("QUICKQUOTE/VEHICLES/VEHICLE");
				XmlNodeList drvNodesForDrvCount = myXmlDocument.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER");
			
				int cntVehicle=vehNodesForClass.Count;
				int cntDriver=drvNodesForDrvCount.Count;

				node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/STATEID");
				if(node.InnerText != null)
				{
					intState = Convert.ToInt32(node.InnerText.Trim()); // state ID
				}
				node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/LOBID");
				if(node.InnerText != null)
				{
					intLOB = Convert.ToInt32(node.InnerText.Trim());// LOB_ID			
				}
				node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/ZIPCODE");
				if(node.InnerText != null)
				{
					policyZip = node.InnerText.Trim().ToString(); // zip code
				}
				node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/TERRITORY");
				if(node.InnerText != null)
				{
					policyTerritory = node.InnerText.Trim().ToString(); // zip code
				}
				node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/YEARSCONTINSUREDWITHWOLVERINE");
				if(node.InnerText != null)
				{
					strcontinousinsured=  node.InnerText.Trim().ToString(); // Years Continously Insured with Wolverine
				}
				node =  myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/LOSSES_CHARGEABLE_NONCHARGEABLE");
				if(node.InnerText != null)
				{
					strChargableLoss =  node.InnerText.Trim().ToString(); // Losses Chargeble or Non chargeble
				}
				node =  myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/QUOTEEFFDATE");
				if(node.InnerText != null)
				{
					strPolicyEffectivedate =  node.InnerText.Trim().ToString(); // Policy Effective Date
				}
				//Fetch the Insurance Score form Database :23 April 2007
				// Here we will fetch territory from Database
				ClsAuto objClsAuto = new ClsAuto();						
				//Get the mvr points at driver level
				int intDriverID=0,intAV_DriverID=0;
			
				// for each driver
				foreach(XmlNode DriverNode in myXmlDocument.SelectSingleNode("//DRIVERS"))
				{
					int intMVRPoints=0,intVioPoint=0,intAccPoint=0;//,intDamagetoAmount=0;
					intDriverID = int.Parse(DriverNode.Attributes["ID"].Value.ToString().Trim());
					// for each Accident violation
					string strViolationCode="" ,stDamageTotalAmt="", strNonChgVioCod="";//, strPointsApplied="";
					int TotalAccident=0;
					//string flgPntApild="";
					//get points applied node
					//if(DriverNode.SelectSingleNode("POINTSAPPLIED").InnerText.ToString().Trim()!="")
						//flgPntApild = DriverNode.SelectSingleNode("POINTSAPPLIED").InnerText.ToString().Trim();
					foreach(XmlNode AccidentViolationNode in myXmlDocument.SelectSingleNode("//ACCIDENTVIOLATIONS"))
					{
                        int intAccId = 0, flgNulVio = 0;//intAccRefid=0,
						
						intAV_DriverID = int.Parse(AccidentViolationNode.Attributes["DRIVERREF"].Value.ToString().Trim());
						if(intDriverID == intAV_DriverID)
						{
							//get accident violation id 
							if(AccidentViolationNode.Attributes["ACCID"].Value.ToString().Trim()!="")
								intAccId = int.Parse(AccidentViolationNode.Attributes["ACCID"].Value.ToString().Trim());

							//Travel each accident and violation node and get accident violation ref id 
							//if accident id matches with any ref id then this violation will not be charged and have zero point.
							foreach(XmlNode nod in myXmlDocument.SelectSingleNode("//ACCIDENTVIOLATIONS"))
							{	
								if(nod.Attributes["ACCREF"].Value.ToString().Trim()!="" && nod.Attributes["ACCREF"].Value.ToString().Trim()!="0")
								{
									if(int.Parse(nod.Attributes["ACCREF"].Value.ToString().Trim())==intAccId)
									{
										flgNulVio=1;
									}
									
								}
							}
							// get accident violation code for each driver 
							node = AccidentViolationNode.SelectSingleNode("./ACCIDENTVIOLATIONCD");
							strViolationCode = strViolationCode + '^' + node.InnerText.Trim();
							// set a string to have each acci/vio pointsapplied value
							//strPointsApplied=strPointsApplied + '^'+flgPntApild;
							if(flgNulVio==1)
								strNonChgVioCod=strNonChgVioCod+'^'+node.InnerText.Trim();
							else
								strNonChgVioCod=strNonChgVioCod+'^'+"0";
							
							// Get total Amount Paid against Accident or Violation
							nodeDamageTotalAmt = AccidentViolationNode.SelectSingleNode("./DAMAGETOTALAMT");
							if(nodeDamageTotalAmt !=null)
							{
								if(nodeDamageTotalAmt.InnerText.Trim()!="" && flgNulVio!=1)
								{
									stDamageTotalAmt = stDamageTotalAmt + '^' + nodeDamageTotalAmt.InnerText.Trim();
									TotalAccident++;
								}
								else
									stDamageTotalAmt = stDamageTotalAmt + '^' + "0";
							}
							
						}									
					}
					// get the mvr points against the code
					// get non chargable violation code
					//string[] arrPointsApplied = new string[0];
					//if(strPointsApplied!="")
					//	arrPointsApplied = strPointsApplied.Split('^');
					string[] arrNonChgVioCod = new string[0];
					if(strNonChgVioCod!="")
						arrNonChgVioCod = strNonChgVioCod.Split('^');
					string[] arrViolationCode = new string[0];					 
					if(strViolationCode !="")
						arrViolationCode = strViolationCode.Split('^');	
					string[] arrDamageTotalAmt = new string[0];
					if(stDamageTotalAmt !="")
						arrDamageTotalAmt = stDamageTotalAmt.Split('^');	
					for(int iCounter=1; iCounter<arrViolationCode.Length ; iCounter++)
					{
						bool IsNumeric = false;
						try
						{
							int iTest = Int32.Parse(arrViolationCode[iCounter]);
							IsNumeric = true;
						}
						catch(Exception ex)
						{
							IsNumeric = false;
                            Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
						}
						if(IsNumeric)
						{
							bool IsChgVln = true;//, IsPntApld = true;
							//if(arrPointsApplied[iCounter].ToString().ToUpper().Trim()=="N")
							//	IsPntApld = false;
							//for(int nonChgcntr=0; nonChgcntr<arrNonChgVioCod.Length;nonChgcntr++)
							//{
							//	if(arrNonChgVioCod[nonChgcntr]==arrViolationCode[iCounter])
							if(arrNonChgVioCod[iCounter].ToString().ToUpper().Trim()!="0")
								IsChgVln = false;
							//}
							//if( IsPntApld==true)
							//{
								if(IsChgVln==true)
									intMVRPoints= intMVRPoints + objClsAuto.GetMVRPointsForACORD(arrViolationCode[iCounter],intState,intLOB);					
							//}
							if(System.Convert.ToInt32(arrViolationCode[iCounter])!=41400 && System.Convert.ToInt32(arrViolationCode[iCounter])!= 41213 && System.Convert.ToInt32(arrViolationCode[iCounter])!= 41000 && System.Convert.ToInt32(arrViolationCode[iCounter])!=42110 && System.Convert.ToInt32(arrViolationCode[iCounter])!=41211 &&System.Convert.ToInt32(arrViolationCode[iCounter])!=42120 && System.Convert.ToInt32(arrViolationCode[iCounter])!=42220 &&
								 System.Convert.ToInt32(arrViolationCode[iCounter])!=42100 && System.Convert.ToInt32(arrViolationCode[iCounter])!=41210 && System.Convert.ToInt32(arrViolationCode[iCounter])!=40000 && System.Convert.ToInt32(arrViolationCode[iCounter])!=41260 &&
								System.Convert.ToInt32(arrViolationCode[iCounter])!=42130 && System.Convert.ToInt32(arrViolationCode[iCounter])!=41220 && System.Convert.ToInt32(arrViolationCode[iCounter])!=41212 && System.Convert.ToInt32(arrViolationCode[iCounter])!=52400 && System.Convert.ToInt32(arrViolationCode[iCounter])!=41230)
							{
								//if(IsPntApld==true)
								//{
									if(IsChgVln==true)
										intVioPoint = intVioPoint + objClsAuto.GetMVRPointsForACORD(arrViolationCode[iCounter],intState,intLOB);					
								//}
							}
							else
							{
								// if continously insured with Wolverine for more than or equal to three years and total accident are one
								//then we will not consider first accident having less than 2000 amount paid 
								if(System.Convert.ToInt32(strcontinousinsured) >= 3 && System.Convert.ToInt32(arrDamageTotalAmt[iCounter]) <2000 && TotalAccident ==1)
									intAccPoint=0;
									//if Damage to Auto amount is greater or equal to 1000 then give 3 points to first accident and 4 points to consequtive one. 
								else if(System.Convert.ToInt32(arrDamageTotalAmt[iCounter]) >=1000)//  && IsPntApld==true)
								{
									intAccPoint++;
								}
								//intAccPoint = intAccPoint + objClsAuto.GetMVRPointsForACORD(arrViolationCode[iCounter],intState,intLOB);					
							}
						}
					}	
					if(intAccPoint!=0)
						intAccPoint = intAccPoint*4 -1;
					intMVRPoints =intMVRPoints + intAccPoint;
					// Assign these points in the node
					XmlNode MVRXmlNode= DriverNode.SelectSingleNode("./MVR");
					XmlNode VIOXmlNode= DriverNode.SelectSingleNode("./SUMOFVIOLATIONPOINTS");
					XmlNode ACCIXmlNode= DriverNode.SelectSingleNode("./SUMOFACCIDENTPOINTS");
					MVRXmlNode.InnerText = intMVRPoints.ToString();	
					VIOXmlNode.InnerText = intVioPoint.ToString();
					ACCIXmlNode.InnerText = intAccPoint.ToString();
				}

				#region GET AND APPENDS NODES IN DRIVER QQ XML FOR CLASS INPUT
			
			
				/*Modified 26 April 2007 : 
				 * Before sending the driver nodes for fetching class 
				 * For each vehicle, Pick the driver having the id as governing driverid in the vehicle node 
				 * and change the innertext of the nodes*/
				string strmvrs = "0";
				string strDriverAge="",strDriverDateOfBirth="",strGoodStudent="";
				XmlNodeList vehList= myXmlDocument.SelectNodes("QUICKQUOTE/VEHICLES/VEHICLE");
				XmlNodeList DrvList= myXmlDocument.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER");
				foreach(XmlNode vNode in vehList)
				{
					string strPremDrivVeh="";//,strSafeDrivVeh="";
					//1: Split driver assignment and assined driver id
					//2: Update driver outer xml with assignment vehicle type and assinment vehicle ids
					//3: Update assined vehicle inner xml with all assigned  driver respective node value for discount and surcharge
					//4: Make all assigned driver xml for class calculation
					string VehId=vNode.Attributes["ID"].Value.ToString();
					foreach(XmlNode dNode in DrvList)
					{						
						string VehAsingdDriver = dNode.Attributes["VEHICLESASSIGNEDASOPERATOR"].Value.ToString().Trim();
						string DrvAssinType =  dNode.Attributes["VEHICLESDRIVEDAS"].Value.ToString().Trim();
						if(VehAsingdDriver.StartsWith("^"))
							VehAsingdDriver=VehAsingdDriver.Substring(VehAsingdDriver.IndexOf("^")+1,(VehAsingdDriver.Length-1));
						if(DrvAssinType.StartsWith("^"))
							DrvAssinType=DrvAssinType.Substring(DrvAssinType.IndexOf("^")+1,(DrvAssinType.Length-1));
						string[] VehAsingdDriverIds = new string[0];
						string[] DrvAssinTypes = new string[0];
						VehAsingdDriverIds=VehAsingdDriver.Split('^');
						DrvAssinTypes=DrvAssinType.Split('^');
						for(int iCounter=0; iCounter<VehAsingdDriverIds.Length;iCounter++)
						{
							if(VehId==VehAsingdDriverIds[iCounter])
							{
								//update vehicle assignment
								dNode.Attributes.Item(3).InnerText=VehAsingdDriverIds[iCounter];
								// update drive type
								strmvrs = dNode.SelectSingleNode("MVR").InnerText;  
								strDriverDateOfBirth = dNode.SelectSingleNode("BIRTHDATE").InnerText;
								strDriverAge  = CalculateDriverAge(strPolicyEffectivedate,strDriverDateOfBirth);
								dNode.Attributes.Item(1).InnerText = strDriverAge;
								dNode.SelectSingleNode("AGEOFDRIVER").InnerText=strDriverAge;
								dNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText=VehAsingdDriverIds[iCounter];
								if(DrvAssinTypes[iCounter]!=null)
								{
									if(System.Convert.ToInt32(strDriverAge)<25 && System.Convert.ToInt32(strmvrs)>0 && DrvAssinTypes[iCounter].ToString().ToUpper()=="PRINCIPAL") 
									{
										dNode.Attributes.Item(2).InnerText="YPPA";
										dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText="PRINCIPAL";
										dNode.SelectSingleNode("VEHICLEDRIVEDASCODE").InnerText="YPPA^PRINCIPAL";
									}
									else if(System.Convert.ToInt32(strDriverAge)<25 && System.Convert.ToInt32(strmvrs)>0 && DrvAssinTypes[iCounter].ToString().ToUpper()=="OCCASIONAL") 
									{
										dNode.Attributes.Item(2).InnerText="YOPA";
										dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText="OCCASIONAL";
										dNode.SelectSingleNode("VEHICLEDRIVEDASCODE").InnerText="YOPA^OCCASIONAL";
									}
									else if(System.Convert.ToInt32(strDriverAge)<25 && System.Convert.ToInt32(strmvrs)<=0 && DrvAssinTypes[iCounter].ToString().ToUpper()=="OCCASIONAL") 
									{
										dNode.Attributes.Item(2).InnerText="YONPA";
										dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText="OCCASIONAL";
										dNode.SelectSingleNode("VEHICLEDRIVEDASCODE").InnerText="YONPA^OCCASIONAL";
									}
									else if(System.Convert.ToInt32(strDriverAge)<25 && System.Convert.ToInt32(strmvrs)<=0 && DrvAssinTypes[iCounter].ToString().ToUpper()=="PRINCIPAL") 
									{
										dNode.Attributes.Item(2).InnerText="YPNPA";
										dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText="PRINCIPAL";
										dNode.SelectSingleNode("VEHICLEDRIVEDASCODE").InnerText="YPNPA^PRINCIPAL";
									}
									else if(System.Convert.ToInt32(strDriverAge)>=25 && System.Convert.ToInt32(strmvrs)<=0 && DrvAssinTypes[iCounter].ToString().ToUpper()=="PRINCIPAL") 
									{
										dNode.Attributes.Item(2).InnerText="PNPA";
										dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText="PRINCIPAL";
										dNode.SelectSingleNode("VEHICLEDRIVEDASCODE").InnerText="PNPA^PRINCIPAL";
									}
									else if(System.Convert.ToInt32(strDriverAge)>=25 && System.Convert.ToInt32(strmvrs)<=0 && DrvAssinTypes[iCounter].ToString().ToUpper()=="OCCASIONAL") 
									{
										dNode.Attributes.Item(2).InnerText="OMPA";
										dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText="OCCASIONAL";
										dNode.SelectSingleNode("VEHICLEDRIVEDASCODE").InnerText="OMPA^OCCASIONAL";
									}
									else if(System.Convert.ToInt32(strDriverAge)>=25 && System.Convert.ToInt32(strmvrs)>0 && DrvAssinTypes[iCounter].ToString().ToUpper()=="PRINCIPAL") 
									{
										dNode.Attributes.Item(2).InnerText="PPA";
										dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText="PRINCIPAL";
										dNode.SelectSingleNode("VEHICLEDRIVEDASCODE").InnerText="PPA^PRINCIPAL";
									}
									else if(System.Convert.ToInt32(strDriverAge)>=25 && System.Convert.ToInt32(strmvrs)>0 && DrvAssinTypes[iCounter].ToString().ToUpper()=="OCCASIONAL") 
									{
										dNode.Attributes.Item(2).InnerText="OPA";
										dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText="OCCASIONAL";
										dNode.SelectSingleNode("VEHICLEDRIVEDASCODE").InnerText="OPA^OCCASIONAL";
									}
								}
							}
						}
						//end driver node updation
					}
					XmlNodeList drvList = myXmlDocument.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER[@VEHICLEASSIGNEDASOPERATOR='"+ VehId + "']");
					//	string vehDriveAs = vNode.SelectSingleNode("DRIVESVEHICLE").InnerText.ToString();
					//	string gvernDrv = vNode.SelectSingleNode("GOVERNINGDRIVER").InnerText.ToString();
					//	int drvIDinVeh=0;
					//	if(gvernDrv.ToString() !="")
					//	{
					//		drvIDinVeh = int.Parse(gvernDrv.Substring(10,1).ToString().Trim());
					//	}
					//Pick the Drivers and set driver assignment
					//		XmlNodeList drvasigList = myXmlDocument.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER");
					
					/*foreach(XmlNode dNode in drvasigList)
					{
						strmvrs = dNode.SelectSingleNode("MVR").InnerText;  
						//dNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText = drvIDinVeh.ToString();
						dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText = vehDriveAs.ToString().ToUpper().Trim();
						if(strmvrs =="0" || strmvrs =="")
						{
							if(dNode.Attributes.Item(3).InnerText !="")
							{
								dNode.Attributes.Item(2).InnerText = "PNPA";
							}
							else
							{
								dNode.Attributes.Item(2).InnerText = "ONPA";
								dNode.Attributes.Item(3).InnerText = vNode.Attributes.Item(0).InnerText;
								dNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText = vNode.Attributes.Item(0).InnerText;
							}
						}
						else
						{
							if(dNode.Attributes.Item(3).InnerText !="")
							{
								dNode.Attributes.Item(2).InnerText = "PPA";
							}
							else
							{
								dNode.Attributes.Item(2).InnerText = "OPA";
								dNode.Attributes.Item(3).InnerText = vNode.Attributes.Item(0).InnerText;
								dNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText = vNode.Attributes.Item(0).InnerText;
							}
						}
					}*/
					//Pick the Drivers having Governing ID in vehical Node :
					//XmlNodeList drvList = myXmlDocument.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER[@ID='"+ drvIDinVeh + "']");
					int intMvr = 0, intPtAld=0;
					string strPotVhApd="";
					foreach(XmlNode dNode in drvList)
					{
						
						//dNode.Attributes.Item(3).InnerText = drvIDinVeh.ToString(); //Assigned as attribute 
						if(strPremDrivVeh=="" || strPremDrivVeh.Trim()=="TRUE")
							strPremDrivVeh = dNode.SelectSingleNode("NOPREMDRIVERDISC").InnerText;
					////////////////////////////////////////////////////////////////////
						intPtAld=0;
						//Fetch Points appllied node for each Driver 
						string[] DrvVioPnt = new string[0];
						if(dNode.SelectSingleNode("POINTSAPPLIED")!=null)
							strPotVhApd = dNode.SelectSingleNode("POINTSAPPLIED").InnerText;
						if(strPotVhApd.IndexOf(VehId)>0)
						{
							if(strPotVhApd.StartsWith("^"))
								strPotVhApd=strPotVhApd.Substring(strPotVhApd.IndexOf("^")+1,(strPotVhApd.Length-1));
							DrvVioPnt=strPotVhApd.Split('^');
							for(int ctr=0; ctr<DrvVioPnt.Length; ctr++)
							{
								if(DrvVioPnt[ctr].IndexOf(VehId)>=0)
								{
									if(DrvVioPnt[ctr].IndexOf("Y")>0 || DrvVioPnt[ctr].IndexOf("y")>0)
										intPtAld=1;
									else
										intPtAld=0;
								}
							}
						}
					///////////////////////////////////////////////////////////////////
						if(dNode.SelectSingleNode("MVR").InnerText!="" && intPtAld==1)
							intMvr += System.Convert.ToInt32(dNode.SelectSingleNode("MVR").InnerText);
						if(strGoodStudent=="" || strGoodStudent=="TRUE")
							strGoodStudent=vNode.SelectSingleNode("GOODSTUDENT").InnerText;
					}
					/*Premier Driver and safe Driver Discount*/
					// Premier driver
					// if driver age is blank then set it to 0
					if(strDriverAge=="")
						strDriverAge="0";
					if(strPremDrivVeh.Trim()=="TRUE" && System.Convert.ToInt32(strDriverAge.Trim()) >= 19 && System.Convert.ToInt32(strChargableLoss.Trim())<=0 && System.Convert.ToInt32(strcontinousinsured.Trim()) <=2 && intMvr <=0)
					{
						vNode.SelectSingleNode("PREMIERDRIVER").InnerText = "TRUE";
					}
					else
					{
						vNode.SelectSingleNode("PREMIERDRIVER").InnerText = "FALSE";
					}
					// safe driver
					if(System.Convert.ToInt32(strChargableLoss.Trim())<=2 && System.Convert.ToInt32(strcontinousinsured.Trim()) >=3 && intMvr <=0 && vNode.SelectSingleNode("PREMIERDRIVER").InnerText == "FALSE")
					{
						vNode.SelectSingleNode("SAFEDRIVER").InnerText = "TRUE";
					}
					else
					{
						vNode.SelectSingleNode("SAFEDRIVER").InnerText = "FALSE";
					}
					// Good Student
					if(intMvr>2 && strGoodStudent=="TRUE")
					{
						vNode.SelectSingleNode("GOODSTUDENT").InnerText = "FALSE";
					}
					strmvrs = "0";
					//Set Flag for college student premier driver and safe driver discount
					XmlElement tmpElment = myXmlDocument.CreateElement("TMPPREMIER");
					if(strPremDrivVeh.Trim()=="TRUE" && System.Convert.ToInt32(strDriverAge.Trim()) >= 19 && System.Convert.ToInt32(strChargableLoss.Trim())<=0 && System.Convert.ToInt32(strcontinousinsured.Trim()) <=2)
					{
						tmpElment.InnerText="TRUE";
						vNode.InsertAfter(tmpElment,vNode.LastChild);					
					}
					else
					{
						tmpElment.InnerText="FALSE";
						vNode.InsertAfter(tmpElment,vNode.LastChild);					
					}
					tmpElment = myXmlDocument.CreateElement("TMPSAFE");
					if(System.Convert.ToInt32(strChargableLoss.Trim())<=2 && System.Convert.ToInt32(strcontinousinsured.Trim()) >=3 && vNode.SelectSingleNode("PREMIERDRIVER").InnerText == "FALSE")
					{
						tmpElment.InnerText="TRUE";
						vNode.InsertAfter(tmpElment,vNode.LastChild);	
					}
					else
					{
						tmpElment.InnerText="FALSE";
						vNode.InsertAfter(tmpElment,vNode.LastChild);	
					}
				//}
				#endregion
				#region CLASS CALCULATION CAPITAL RATER
				ClsAuto gObjQQAuto = new ClsAuto();
		
				string strDriverInputXml ="" ,strEligibleDriver ="",strVehiclestrClassXml = "",strDriverClass = "PA";			
				string strDriverXml = "",strClassXml="";
			
				//Fetch all vehicles 
				XmlNodeList VehiclesXmlNodeList = myXmlDocument.SelectNodes("QUICKQUOTE/VEHICLES/VEHICLE");
				//Fetch Policy level info
				XmlNode PolicyXmlNode = myXmlDocument.SelectSingleNode("//POLICY");            
				// Get the policy level node details
				string strPolicyXmlNode = PolicyXmlNode.InnerXml;
			    
				// For each vehcile
				//foreach(XmlNode VehicleXmlNode in VehiclesXmlNodeList)
				//{
				//int VehicleID = int.Parse(VehicleXmlNode.Attributes["ID"].Value.ToString().Trim());
				//Get number of Assigned Drivers with same vehicle
				XmlNodeList DriversXmlNodeList = myXmlDocument.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER[@VEHICLEASSIGNEDASOPERATOR = '"+ VehId + "' ]");
				strDriverXml = "";
				foreach(XmlNode DriverXmlNode in DriversXmlNodeList)
				{
					XmlElement tmpElmtVeh = myXmlDocument.CreateElement("VEHICLECOUNT");
					tmpElmtVeh.InnerText = cntVehicle.ToString();
					DriverXmlNode.InsertAfter(tmpElmtVeh,DriverXmlNode.LastChild);
					XmlElement tmpElmtDrv = myXmlDocument.CreateElement("DRIVERCOUNT");
					tmpElmtDrv.InnerText = cntDriver.ToString();
					DriverXmlNode.InsertAfter(tmpElmtDrv,DriverXmlNode.LastChild);
					strDriverXml = strDriverXml + DriverXmlNode.OuterXml;
				}
				
				//Get the Eligible Driver for vehicle
				strEligibleDriver  =  "<ELIGIBLEDRIVERS>" + "<POLICY>" + strPolicyXmlNode.ToUpper() + "</POLICY>" + strDriverXml.ToUpper() + "</ELIGIBLEDRIVERS>"; 
				strDriverInputXml  =	gObjQQAuto.GetEligibleDrivers(strEligibleDriver);
				string strGoodStd="",strMvr="",strDrvAge="",strDrvInc="",strDrvDep="",strDrvId="";
				XmlDocument EligDrivDoc = new XmlDocument();
					if(strDriverInputXml!="")
					{
						EligDrivDoc.LoadXml(strDriverInputXml);
						strDrvId=EligDrivDoc.SelectSingleNode("DRIVER").Attributes["ID"].Value.ToString().Trim();
						strDrvId=strDrvId+"^"+VehId;
						strGoodStd=	EligDrivDoc.SelectSingleNode("DRIVER/GOODSTUDENT").InnerText;
						strMvr=	EligDrivDoc.SelectSingleNode("DRIVER/MVR").InnerText;
						strDrvAge= EligDrivDoc.SelectSingleNode("DRIVER/AGEOFDRIVER").InnerText;
						strDrvInc= EligDrivDoc.SelectSingleNode("DRIVER/DRIVERINCOME").InnerText;
						strDrvDep= EligDrivDoc.SelectSingleNode("DRIVER/DEPENDENTS").InnerText;
					}
					//Good Student
				if(strGoodStd.ToUpper()=="TRUE")
				{
					if(strMvr!="")
					{
						if(int.Parse(strMvr)<3)
							vNode.SelectSingleNode("GOODSTUDENT").InnerText=strGoodStd;
					}						
				}
					//WaverWorkLoss
					if(strDrvAge!="")
					{
						if(int.Parse(strDrvAge)>= WaverWorkLossAge && vNode.SelectSingleNode("WAIVEWORKLOSS").InnerText=="TRUE")
							vNode.SelectSingleNode("WAIVEWORKLOSS").InnerText="TRUE";
						else
							vNode.SelectSingleNode("WAIVEWORKLOSS").InnerText="FALSE";
					
					}
					//Driver Income
					if(strDrvInc!="")
					{
						vNode.SelectSingleNode("DRIVERINCOME").InnerText=strDrvInc;
					}
					//Driver Dependent
					if(strDrvDep!="")
					{
						vNode.SelectSingleNode("DEPENDENTS").InnerText=strDrvDep;
					}
				//Set the eligible driver
				strClassXml = "<CLASS>" + strPolicyXmlNode.ToUpper() + "<DRIVERINFO>" + strDriverInputXml.ToUpper() + "</DRIVERINFO>" + "</CLASS>";
				strVehiclestrClassXml = gObjQQAuto.GetVehicleClass(strClassXml,"AUTOP");

				XmlDocument ClassXmlDocument = new XmlDocument();
				ClassXmlDocument.LoadXml(strVehiclestrClassXml);						
				XmlNode ClassXmlNode = ClassXmlDocument.SelectSingleNode("//CLASS");				
				
				if(ClassXmlNode!=null)
					strDriverClass = ClassXmlNode.SelectSingleNode("VEHICLECLASS").InnerText.ToString();
				
				if(strDriverClass=="")		
					strDriverClass="PA";
				//If driver is not a college student
				if(strDriverClass=="5C")
					strCollegeDrvId=strCollegeDrvId+"~"+strDrvId;
				if(vNode.SelectSingleNode("./VEHICLETYPEUSE").InnerText.ToString().ToUpper().Trim() == "PERSONAL")
					vNode.SelectSingleNode("VEHICLECLASS").InnerText = strDriverClass;										
				
				if (strDriverClass.ToString().Length <=1)
				{
					vNode.SelectSingleNode("VEHICLECLASSCOMPONENT1").InnerText = strDriverClass;
					vNode.SelectSingleNode("VEHICLECLASSCOMPONENT2").InnerText = "";	
				}
				else
				{
					vNode.SelectSingleNode("VEHICLECLASSCOMPONENT1").InnerText = strDriverClass.Substring(0,1);
					vNode.SelectSingleNode("VEHICLECLASSCOMPONENT2").InnerText = strDriverClass.Substring(1,1);						
				}
			//}
					/*END Set Class*/
					#endregion
					#region SYMBOL CALCULATION
					/*start Symbol */
					/*string strVehicleSymbol="",strVehcileYear="",strVehicleMake="",strVehilceModel="";
				
					strVehcileYear = VehicleXmlNode.SelectSingleNode("YEAR").InnerText.ToString().Trim();
					strVehicleMake = VehicleXmlNode.SelectSingleNode("MAKE").InnerText.ToString().Trim();
					strVehilceModel = VehicleXmlNode.SelectSingleNode("MODEL").InnerText.ToString().Trim();
					strVehicleSymbol = gObjQQAuto.GetVehicleVinSymbol(strVehcileYear,strVehicleMake,strVehilceModel);

					XmlDocument SymbolXmlDocument = new XmlDocument();
					SymbolXmlDocument.LoadXml(strVehicleSymbol);
					if(SymbolXmlDocument.SelectSingleNode("//VIN[@SYMBOL]").InnerText != "")
						VehicleXmlNode.SelectSingleNode("SYMBOL").InnerText = SymbolXmlDocument.SelectSingleNode("//VIN[@SYMBOL]").InnerText;
					else
						VehicleXmlNode.SelectSingleNode("SYMBOL").InnerText="1";	//By Default 1*/
					/*end Symbol*/

					//Get Symbol 
					string strVin = "",strSymbol="";
					if(vNode.SelectSingleNode("VIN")!=null)
					strVin = vNode.SelectSingleNode("VIN").InnerText.ToString().Trim();
					strVin = ReplaceEsqapeXmlCharacters(strVin);
					if(strVin != "")
					{
						if(strVin.Length>2)
						{
							strSymbol = ClsVinMaster.GetDetailsFromVIN(strVin).GetXml();
							strSymbol=strSymbol.Replace("\r\n","");
							strSymbol=strSymbol.ToUpper();
							if(strSymbol.IndexOf("<SYMBOL>")>=0 && strSymbol.IndexOf("</SYMBOL>")>=0)
							{
								strSymbol = strSymbol.Substring(strSymbol.IndexOf("<SYMBOL>"),(strSymbol.IndexOf("</SYMBOL>")-strSymbol.IndexOf("<SYMBOL>"))).Replace("<SYMBOL>","");
								if(int.Parse(strSymbol)<10)
								{
									if(strSymbol.IndexOf("0")<0)
										strSymbol="0"+strSymbol;
								}
							}
							else if(strSymbol.IndexOf("<RESULT>")>=0 && strSymbol.IndexOf("</RESULT>")>=0)
							{
								strSymbol = strSymbol.Substring(strSymbol.IndexOf("<RESULT>"),(strSymbol.IndexOf("</RESULT>")-strSymbol.IndexOf("<RESULT>"))).Replace("<RESULT>","");
								if(strSymbol=="-1")
									strSymbol="01";
							}
							else
								strSymbol="01";
							//strSymbol = gObjQQAuto.GetVehicleSymbol(strVin);
						}
					}
						if(vNode.SelectSingleNode("COST")!=null && (strSymbol=="-1" || strSymbol=="01" || strSymbol ==""))
						{
							int amount=0,year=0;
							string vehicleType="";
							vehicleType  = vNode.SelectSingleNode("VEHICLETYPE").InnerText;
							if( vehicleType != null)
							{
								if(mapDoc.SelectSingleNode("PRODUCTMASTER/PRODUCT[@ID='AUTO-MAPPING']/FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@QQBODYTYPE='"+  vehicleType +"']")!=null)
									vehicleType=mapDoc.SelectSingleNode("PRODUCTMASTER/PRODUCT[@ID='AUTO-MAPPING']/FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@QQBODYTYPE='"+  vehicleType +"']/@QQBODYCODE").InnerText;
								else
									vehicleType=mapDoc.SelectSingleNode("PRODUCTMASTER/PRODUCT[@ID='AUTO-MAPPING']/FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@DEFAULT]/@QQBODYCODE").InnerText;
							}	
							if(vNode.SelectSingleNode("YEAR")!=null)
								year=int.Parse(vNode.SelectSingleNode("YEAR").InnerText);
							if(vNode.SelectSingleNode("COST").InnerText!="")
								amount=int.Parse(vNode.SelectSingleNode("COST").InnerText);
							strSymbol = GetSymbolForVehicle(vehicleType,amount, year);
						}
						/*else
						{
							bool IsNumeric = false;
							try
							{
								int iTest = Int32.Parse(strVin);
								IsNumeric = true;
							}
							catch(Exception ex)
							{
								IsNumeric = false;
							}
							if(IsNumeric)
							{
								if(int.Parse(strVin)<10)
								{
									if(strVin.IndexOf("0")<0)
										strSymbol="0"+strVin;
								}
								else
									strSymbol = strVin;
							}
							else
								strSymbol = strVin;
						}*/
						if(strSymbol!="")
							vNode.SelectSingleNode("SYMBOL").InnerText = strSymbol;
						else
							vNode.SelectSingleNode("SYMBOL").InnerText = "01";
					
					/*else if(vNode.SelectSingleNode("COST")!=null)
					{
						int amount=0,year=0;
						string vehicleType="";
						vehicleType  = vNode.SelectSingleNode("VEHICLETYPE").InnerText;
						if( vehicleType != null)
						{
							if(mapDoc.SelectSingleNode("PRODUCTMASTER/PRODUCT[@ID='AUTO-MAPPING']/FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@QQBODYTYPE='"+  vehicleType +"']")!=null)
								vehicleType=mapDoc.SelectSingleNode("PRODUCTMASTER/PRODUCT[@ID='AUTO-MAPPING']/FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@QQBODYTYPE='"+  vehicleType +"']/@QQBODYCODE").InnerText;
							else
								vehicleType=mapDoc.SelectSingleNode("PRODUCTMASTER/PRODUCT[@ID='AUTO-MAPPING']/FACTOR[@ID='VEHICLEBODYTYPE']/NODE[@ID='QQACORDVEHICLEBODYTYPE']/ATTRIBUTES[@DEFAULT]/@QQBODYCODE").InnerText;
						}	
						if(vNode.SelectSingleNode("YEAR")!=null)
							year=int.Parse(vNode.SelectSingleNode("YEAR").InnerText);
						if(vNode.SelectSingleNode("COST").InnerText!="")
							amount=int.Parse(vNode.SelectSingleNode("COST").InnerText);
						strSymbol = GetSymbolForVehicle(vehicleType,amount, year);
					}*/
					#endregion
					#region GARAGED LOCATION ON THE BASIS OF ZIP

                 	//FETCH THE  GARAGED LOCATION ON THE BASIS OF ZIP
					//Fetch territory from database on the basis of ZIP,STATE and LOB		
					string strZIP = vNode.SelectSingleNode("ZIPCODEGARAGEDLOCATION").InnerText.ToString().Trim(); // ZIP
					if (strZIP.Trim() =="" || strZIP.Trim()=="0")
					{
						strZIP=policyZip;
						vNode.SelectSingleNode("ZIPCODEGARAGEDLOCATION").InnerText = strZIP;
					}
					// Here we will fetch territory from Database
					//ClsAuto objClsAuto = new ClsAuto();
					string strTerr = objClsAuto.FetchValuesXML(intState,intLOB,strZIP,strPolicyEffectivedate);
					vNode.SelectSingleNode("TERRCODEGARAGEDLOCATION").InnerText = strTerr;
					if(strTerr !="" || strTerr !="0")
					{
						vNode.SelectSingleNode("GARAGEDLOCATION").InnerText = strZIP + " ,Territory : "+strTerr;
					}
					#endregion			
				}
				/**/
				
				qqInputXML = myXmlDocument.OuterXml;			
				return qqInputXML;					
			}
		
				catch(Exception ex)
			{
				throw(ex);
			}
		}

		private string FetchAndSetInsuranceScore(string qqInputXml, string strAcordXml)
		{
			//Model Object Of Customer
			Cms.Model.Client.ClsCustomerInfo objClsCustomerInfo = new Cms.Model.Client.ClsCustomerInfo();		
			XmlDocument Doc = new XmlDocument();
			Doc.LoadXml(strAcordXml);
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.LoadXml(qqInputXml);
			XmlNode nod =XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/INSURANCESCORE");
			if(nod.InnerText=="-1")
			{
				XmlNode currentNode,dataNode;

				XmlNode InsuranceSvcRq = Doc.DocumentElement.SelectSingleNode("InsuranceSvcRq/PersAutoPolicyQuoteInqRq/InsuredOrPrincipal");
				//Getting the Personal name of customer/////////////
				currentNode = InsuranceSvcRq.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName");

				if ( currentNode != null )
				{
					dataNode = currentNode.SelectSingleNode("GivenName");
					if ( dataNode != null )
					{	
						objClsCustomerInfo.CustomerFirstName = dataNode.InnerText;
					}

					dataNode = currentNode.SelectSingleNode("OtherGivenName");
					if ( dataNode != null )
					{	
						objClsCustomerInfo.CustomerMiddleName = dataNode.InnerText;
					}
			
					dataNode = currentNode.SelectSingleNode("Surname");
					if ( dataNode != null )
					{	
						objClsCustomerInfo.CustomerLastName = dataNode.InnerText;
					}
								
				}
				currentNode = InsuranceSvcRq.SelectSingleNode("GeneralPartyInfo/NameInfo/TaxIdentity");
				if(currentNode != null)
				{
					dataNode = currentNode.SelectSingleNode("TaxIdTypeCd");
					string strSSN = "";
					if(dataNode != null && dataNode.InnerText.Trim()== "SSN" )
					{	
						dataNode = currentNode.SelectSingleNode("TaxId");
						if(dataNode != null )
						{
							if(dataNode.InnerText.ToString() !="")
							{
								if(dataNode.InnerText.ToString().Length > 11)
								{
									strSSN = dataNode.InnerText.ToString().Substring(0,11);//11 DIgit format
									strSSN = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(strSSN);
									objClsCustomerInfo.SSN_NO = strSSN.Trim();
								}
								else
								{
									objClsCustomerInfo.SSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(dataNode.InnerText.ToString().Trim());
								}

							}
							else
								objClsCustomerInfo.SSN_NO = null;		
						}
						else
							objClsCustomerInfo.SSN_NO =  null;						
					}
					else
						objClsCustomerInfo.SSN_NO =  null;				
				}

				currentNode = InsuranceSvcRq.SelectSingleNode("GeneralPartyInfo/Addr");
				XmlNode addrDataNode=null; 
				if(currentNode!=null)
					addrDataNode = currentNode.SelectSingleNode("Addr1");

				if (addrDataNode != null )
				{
					objClsCustomerInfo.CustomerAddress1 = addrDataNode.InnerText;
				}
					
				addrDataNode = currentNode.SelectSingleNode("Addr2");
					
				if ( addrDataNode != null )
				{
					objClsCustomerInfo.CustomerAddress2 = addrDataNode.InnerText;
				}
					
				addrDataNode = currentNode.SelectSingleNode("City");
					
				if ( addrDataNode != null )
				{
					objClsCustomerInfo.CustomerCity = addrDataNode.InnerText;
				}

				//converting state code to state id
				addrDataNode = currentNode.SelectSingleNode("StateProvCd");					
				if ( addrDataNode != null )
				{
					objClsCustomerInfo.CustomerState = addrDataNode.InnerText;
				}

				addrDataNode = currentNode.SelectSingleNode("PostalCode");					
				if ( addrDataNode != null )
				{
					objClsCustomerInfo.CustomerZip = addrDataNode.InnerText;
				}

				addrDataNode = currentNode.SelectSingleNode("CountryCd");
					
				if ( addrDataNode != null )
				{
					objClsCustomerInfo.CustomerCountry = addrDataNode.InnerText;
				}

				currentNode = InsuranceSvcRq.SelectSingleNode("GeneralPartyInfo/Communications/PhoneInfo");

				string phoneType  = "";
				
				dataNode = currentNode.SelectSingleNode("PhoneTypeCd");
				
				if ( dataNode != null )
				{
					phoneType = dataNode.InnerText;
				}
				
				dataNode = currentNode.SelectSingleNode("CommunicationsUseCd");
				
				string commType = "";
				if ( dataNode  != null  )
				{
					commType = dataNode.InnerText;
				}
				string strPhoneNo="";
				switch(phoneType)
				{
					case "Phone":
						if ( commType == "Home" )
						{
							dataNode = currentNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								strPhoneNo=dataNode.InnerText;
								if(strPhoneNo.Length>=10)
								{
									strPhoneNo="("+strPhoneNo.Substring(0,3)+")"+strPhoneNo.Substring(3,3)+"-"+strPhoneNo.Substring(6,4);
								}
								objClsCustomerInfo.CustomerHomePhone = strPhoneNo;//dataNode.InnerText;
							}
						}
						
						if ( commType == "Business" )
						{
							dataNode = currentNode.SelectSingleNode("PhoneNumber");

							if ( dataNode != null )
							{
								strPhoneNo=dataNode.InnerText;
								if(strPhoneNo.Length>=10)
								{
									strPhoneNo="("+strPhoneNo.Substring(0,3)+")"+strPhoneNo.Substring(3,3)+"-"+strPhoneNo.Substring(6,4);
								}
								objClsCustomerInfo.CustomerBusinessPhone = strPhoneNo;//dataNode.InnerText;
							}
						}
						break;
				}
				// Fetch Insurance Score Request Id
				string InsuranceReqId="";
				bool IsInsExist=false;
				string score="";
				if(Doc.DocumentElement.SelectSingleNode("InsuranceSvcRq/RqUID")!=null)
					strAcordReqId =  Doc.DocumentElement.SelectSingleNode("InsuranceSvcRq/RqUID").InnerText.Trim();
				XmlNode nodInsu_Sco_Acept_Id = Doc.DocumentElement.SelectSingleNode("InsuranceSvcRq/PersAutoPolicyQuoteInqRq/PersPolicy/CreditScoreInfo/CnfNO");
				if(nodInsu_Sco_Acept_Id!=null)
					InsuranceReqId = nodInsu_Sco_Acept_Id.InnerText.Trim(); 
				if(InsuranceReqId!="")
					IsInsExist=true;
				if(InsuranceReqId=="0")
					InsuranceReqId="";
				// Check Insurance Score form Acord DataBase if exists then do not retrive from IIX
				if(IsInsExist)
					GetAcordInsuranceScore(InsuranceReqId);
				if(strAcordInsDetail=="")
					IsInsExist=false;
				XmlNode nodRecDate=null,nodRes1=null,nodRes2=null,nodRes3=null,nodRes4=null;
				if(IsInsExist==false)
				{
					score=FetchandUpdateCustomerInsurancScoreRate(objClsCustomerInfo).Trim();
				}
				else
				{
					score=strAcordInsDetail;
				}
				if(score=="-2")
				{
					nod.InnerText="NOHITNOSCORE";
					if(XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/CUSTOMERINSURANCERECEIVEDDATE")!=null)
						nodRecDate = XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/CUSTOMERINSURANCERECEIVEDDATE");
					nodRecDate.InnerText=System.DateTime.Now.ToString();
					if(IsInsExist==false)
						SaveFetchedInsuranceScore(XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/RQUID").InnerText.Trim(),score,nodRecDate.InnerText,"","","","");
				}
				else if(score.IndexOf('^')!=-1 )
				{
					string[] strCustInsuDetails=new string[0];
				
					strCustInsuDetails=score.Split('^');
					if(strCustInsuDetails[0].ToString()=="-2")
						nod.InnerText="NOHITNOSCORE";
					else
						nod.InnerText=strCustInsuDetails[0].ToString();
					if(XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/CUSTOMERINSURANCERECEIVEDDATE")!=null)
						nodRecDate = XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/CUSTOMERINSURANCERECEIVEDDATE");
					if(XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/REASONCODE1")!=null)
						nodRes1 = XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/REASONCODE1");
					if(XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/REASONCODE2")!=null)
						nodRes2 = XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/REASONCODE2");
					if(XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/REASONCODE3")!=null)
						nodRes3 = XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/REASONCODE3");
					if(XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/REASONCODE4")!=null)
						nodRes4 = XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/REASONCODE4");
					try
					{
						if(strCustInsuDetails[5]!=null)
							nodRecDate.InnerText=strCustInsuDetails[5].ToString();
					}
					catch(Exception ex)
					{
						nodRecDate.InnerText=System.DateTime.Now.ToString();
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					}
					nodRes1.InnerText=strCustInsuDetails[1].ToString();
					nodRes2.InnerText=strCustInsuDetails[2].ToString();
					nodRes3.InnerText=strCustInsuDetails[3].ToString();
					nodRes4.InnerText=strCustInsuDetails[4].ToString();
					int result = 0;
					if(IsInsExist==false)
						result = SaveFetchedInsuranceScore(XmlDoc.SelectSingleNode("QUICKQUOTE/POLICY/RQUID").InnerText.Trim(),nod.InnerText,nodRecDate.InnerText,nodRes1.InnerText,nodRes2.InnerText,nodRes3.InnerText,nodRes4.InnerText);
				}			
				else
				{
					if(score=="NoScore")
					{
						return score;
					}
						
					else
						nod.InnerText=score;
				}

			}
			return XmlDoc.OuterXml;
		}
		private string FetchandUpdateCustomerInsurancScoreRate(Cms.Model.Client.ClsCustomerInfo objClsCustomerInfo)
		{

            System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
			string strUserName = dic["UserName"].ToString();
			string strPassword = dic["Password"].ToString();
			string strAccountNumber = dic["AccountNumber"].ToString();
			string strUrl = dic["URL"].ToString();
			string strCustDetail="",strCustInsudetails="";
			string[] strCustInsuDetails=new string[0];
			string CmsWebURL = "";
			CmsWebURL =  System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString(); 
			strCustDetail=objClsCustomerInfo.CustomerLastName+"^"+objClsCustomerInfo.CustomerFirstName+"^"+objClsCustomerInfo.CustomerMiddleName+"^"+objClsCustomerInfo.CustomerAddress1+"^"+objClsCustomerInfo.CustomerCity+"^"+objClsCustomerInfo.CustomerZip+"^"+objClsCustomerInfo.CustomerState+"^"+objClsCustomerInfo.CustomerHomePhone+"^"+objClsCustomerInfo.CustomerSuffix+"^"+objClsCustomerInfo.SSN_NO;
			try
			{
				//WscCmsWeb.wscmsweb ObjServices = new WscCmsWeb.wscmsweb(CmsWebURL);
				//strCustInsudetails = ObjServices.GetCapitalCustomerCreditScore(strUserName,strPassword,strAccountNumber,strUrl,strCustDetail);
				//if(strCustInsudetails.IndexOf('^')!=-1)
				//{
					//strCustInsuDetails = strCustInsudetails.Split('^');
					//AddRequestLog("5291","INSURANCE SCORE",strCustInsuDetails[0].ToString(),"");
					return strCustInsudetails;
				//}
				//else
				//{
					//AddRequestLog("5291","INSURANCE SCORE",strCustInsudetails,"");
					//return strCustInsudetails;
				//}				
					
			}
			catch(Exception ex)
			{
				AddRequestLog(strAcordReqId,"INSURANCE SCORE",ex.Message,strCustDetail);
				return "NoScore";
			}			
		}
		private int SaveFetchedInsuranceScore(string ReqId,string InsScore, string InsRecDate, string InsReas1, string InsReas2, string insReas3, string InsReas4)
		{
			Cms.DataLayer.DataWrapper objWrapper = new Cms.DataLayer.DataWrapper(ClsCommon.ConnStr ,CommandType.StoredProcedure);
			objWrapper.AddParameter("@ReqId",ReqId);
			objWrapper.AddParameter("@InsScore",InsScore);
			objWrapper.AddParameter("@InsRecDate",InsRecDate);
			objWrapper.AddParameter("@InsReas1",InsReas1);
			objWrapper.AddParameter("@InsReas2",InsReas2);
			objWrapper.AddParameter("@insReas3",insReas3);
			objWrapper.AddParameter("@InsReas4",InsReas4);
			SqlParameter objParam = (SqlParameter) objWrapper.AddParameter 
				("@INSU_SCO_ID", SqlDbType.VarChar ,ParameterDirection.Output);		
			objParam.Size = 100 ;
			int result = objWrapper.ExecuteNonQuery("Proc_InsertAcordInsuranceScoreForReqId");
			if(objParam != null && objParam.Value != DBNull.Value )
			{
				striNSU_rEQiD = objParam.Value.ToString();
			}
			striNSU_rEQiD = striNSU_rEQiD + "^" + InsScore + "^" + InsRecDate;
			objWrapper.Dispose();
			return result;
		}
		private void GetAcordInsuranceScore(string ReqId)
		{
			bool IsInsExists=false;			
			Cms.DataLayer.DataWrapper objWrapper = new Cms.DataLayer.DataWrapper(ClsCommon.ConnStr ,CommandType.StoredProcedure);
			objWrapper.AddParameter("@ReqId",ReqId);
			DataSet DsTmp = objWrapper.ExecuteDataSet("Proc_GetAcordInsuranceScoreForReqId");
			if(DsTmp!=null)
			{
				if(DsTmp.Tables[0].Rows.Count>0 && DsTmp.Tables[0].Rows[0]["ReqInsuranceScore"].ToString()!="-1")
					IsInsExists=true;
				if(DsTmp.Tables[0].Rows.Count>0)
				{
					strAcordInsDetail = DsTmp.Tables[0].Rows[0]["ReqInsuranceScore"].ToString()+"^"+DsTmp.Tables[0].Rows[0]["CUSTOMER_REASON_CODE"].ToString()+"^"+DsTmp.Tables[0].Rows[0]["CUSTOMER_REASON_CODE2"].ToString()+"^"+DsTmp.Tables[0].Rows[0]["CUSTOMER_REASON_CODE3"].ToString()+"^"+DsTmp.Tables[0].Rows[0]["CUSTOMER_REASON_CODE4"].ToString()+"^"+DsTmp.Tables[0].Rows[0]["CUSTOMER_INSURANCE_RECEIVED_DATE"].ToString();
					striNSU_rEQiD = ReqId + "^" +DsTmp.Tables[0].Rows[0]["ReqInsuranceScore"].ToString()+ "^" +DsTmp.Tables[0].Rows[0]["CUSTOMER_INSURANCE_RECEIVED_DATE"].ToString();
				}
					
			}
			//return IsInsExists;
		}
		private void AddRequestLog(string RequestID, string RequestDetails, string ResponseMessage, string AdditionalMessage)
		{
			Cms.DataLayer.DataWrapper objWrapper = new Cms.DataLayer.DataWrapper(ClsCommon.ConnStr ,CommandType.StoredProcedure);
			objWrapper.AddParameter("@INSURANCE_SVC_RQ",RequestID);
			objWrapper.AddParameter("@REQUEST_DATETIME",DateTime.Now );
			objWrapper.AddParameter("@REQUEST_DETAILS",RequestDetails);
			objWrapper.AddParameter("@RETURN_MESSAGE",ResponseMessage);
			objWrapper.AddParameter("@ADDITIONAL_INFO",AdditionalMessage);
			objWrapper.ExecuteNonQuery("Proc_AddRealTimeLog");
			objWrapper.Dispose();
		}
		/// <summary>
		///  This function is common to all LOBs return xml with territory
		/// </summary>
		/// <param name="qqInputXML"></param>
		/// <returns></returns>
		private string ReturnXML_Territory(string qqInputXML)
		{
			XmlDocument myXmlDocument = new XmlDocument();
			myXmlDocument.LoadXml(qqInputXML);
			XmlNode node;
			string strZIP="", strEffdate=""; 
			int intState=0,intLOB=0;					
			
			//Fetch territory from database on the basis of ZIP,STATE and LOB									
			node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/STATEID");
			intState = Convert.ToInt32(node.InnerText.Trim()); // state ID
			
			node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/LOBID");
			intLOB = Convert.ToInt32(node.InnerText.Trim());// LOB_ID
			
			node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/ZIPCODE");
			strZIP = node.InnerText; // ZIP

			node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/QUOTEEFFDATE");
			strEffdate = node.InnerText; // ZIP

			// Here we will fetch territory from Database
			ClsAuto objClsAuto = new ClsAuto();
			string strTerr = objClsAuto.FetchValuesXML(intState,intLOB,strZIP,strEffdate);
			
			node = myXmlDocument.SelectSingleNode("QUICKQUOTE/POLICY/TERRITORY");
			node.InnerText = strTerr; 			
			qqInputXML = myXmlDocument.OuterXml;	

			return qqInputXML;	
		}

		/// <summary>
		///  To save Acord XML & QQ_XML in database
		/// </summary>
		/// <param name="ACORD_XML"></param>
		/// <param name="QQ_XML"></param>
		/// <param name="strRqUIDXmlNode"></param>
		/// <param name="?"></param>
		private void SaveACORD_XMLQQ_XML(string ACORD_XML,string QQ_XML,string strRqUIDXmlNode,int intintAgencyID,string PREMIUM_XML)
		{
			string		strStoredProc	=	"Proc_InsertACORD_QUOTE_DETAILS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,System.Data.CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@INSURANCE_SVC_RQ",strRqUIDXmlNode);
				objDataWrapper.AddParameter("@AGENCY_ID",intintAgencyID);
				objDataWrapper.AddParameter("@ACORD_XML",ACORD_XML);
				objDataWrapper.AddParameter("@QQ_XML",QQ_XML);		
				objDataWrapper.AddParameter("@PREMIUM_XML",PREMIUM_XML);		
				objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);				

				int returnResult = 0;			
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();				
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
		
		public void SaveACORD_XML(string ACORD_XML,string QQ_XML,string PREMIUM_XML,string strRqUIDXmlNode,int intintAgencyID)
		{
			string		strStoredProc	=	"Proc_InsertACORD_QUOTE_DETAILS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,System.Data.CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@INSURANCE_SVC_RQ",strRqUIDXmlNode);
				objDataWrapper.AddParameter("@AGENCY_ID",intintAgencyID);
				objDataWrapper.AddParameter("@ACORD_XML",ACORD_XML);
				objDataWrapper.AddParameter("@QQ_XML",QQ_XML);
				objDataWrapper.AddParameter("@PREMIUM_XML",QQ_XML);	
				objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);				

				int returnResult = 0;			
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();				
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
		///  To update Acord XML table with the state_id, appid, appversion and cust id 
		/// </summary>
		/// <param name="ACORD_XML"></param>
		/// <param name="QQ_XML"></param>
		/// <param name="strRqUIDXmlNode"></param>
		/// <param name="?"></param>
		public void UpdateAppDetailsInAcordDetails(int customerID, int appID,int appVersionID, int stateID, string GUID, DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_UpdateAcord_Quote_Details";			 
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,System.Data.CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@GUID",GUID);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objDataWrapper.AddParameter("@APP_ID",appID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);				
				objDataWrapper.AddParameter("@STATE_ID",stateID);				

				int returnResult = 0;			
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);				
				objDataWrapper.ClearParameteres();		
				objDataWrapper.CommitTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}		
		}
		//Make App Check 18 july 2007
		public string CheckApplicationExists(string GUID,string calledFrom)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@INSURANCE_SVC_RQ",GUID);
				objDataWrapper.AddParameter("@CALLED_FROM",calledFrom);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_CheckApplicationExistsCapitalrater");
			
				if(dsTemp.Tables[0].Rows.Count>0)
				{			
						return(dsTemp.Tables[0].Rows[0][0].ToString());
					
				}
				else
					return("");
				
				
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}
		//Make App Check 18 july 2007
		public string CheckAgencyExists(string AgencyCode)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@AGENCYCODE",AgencyCode);
				
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_CheckAgencyExistsCapitalrater");			
				if(dsTemp.Tables[0].Rows.Count>0)
				{			
					return(dsTemp.Tables[0].Rows[0][0].ToString());
					
				}
				else
					return("");
				
				
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}
		
		public string GetAgencyID(string AgencyCode)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@AGENCYCODE",AgencyCode);
				
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_CheckAgencyExistsCapitalrater");			
				if(dsTemp.Tables[0].Rows.Count>0)
				{			
					return(dsTemp.Tables[0].Rows[0][1].ToString());
					
				}
				else
					return("");
				
				
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}
		#region CHECK AGENCY
		public bool AgencyExists(string agencyID)
		{
			try
			{
				if(agencyID!="")
				{
					string appStatus  = CheckAgencyExists(agencyID);
					if(appStatus == "2")
						return(true);
					else
						return(false);

				}
			}
			catch(Exception ex)
            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
			return(true);

		}
		#endregion
		#region Application Exists
		private bool AppExists(string RqId)
		{
			try
			{
				if(RqId!="")
				{
					string AppNum= CheckApplicationExists(RqId,"APP");
					if(AppNum!="1")
						return (true);
					else
						return (false);
				}
			}
			catch(Exception ex)
            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
			return (true);
		}
		#endregion
		#region
		private void AddRequestLogForPremium(string RequestID, string RequestDetails, string ResponseMessage, string AdditionalMessage)
		{
			Cms.DataLayer.DataWrapper objWrapper = new Cms.DataLayer.DataWrapper(ClsCommon.ConnStr ,CommandType.StoredProcedure);
			objWrapper.AddParameter("@INSURANCE_SVC_RQ",RequestID);
			objWrapper.AddParameter("@REQUEST_DATETIME",DateTime.Now );
			objWrapper.AddParameter("@REQUEST_DETAILS",RequestDetails);
			objWrapper.AddParameter("@RETURN_MESSAGE",ResponseMessage);
			objWrapper.AddParameter("@ADDITIONAL_INFO",AdditionalMessage);
			objWrapper.ExecuteNonQuery("Proc_AddRealTimeLog");
			objWrapper.Dispose();
		}
		#endregion
		#region
		public static string RemoveJunkXmlCharacters(string strNodeContent)
		{
			strNodeContent = strNodeContent.Replace("&","&amp;");
			//strNodeContent = strNodeContent.Replace("\"","&quot;");
			strNodeContent = strNodeContent.Replace("'","H673GSUYD7G3J73UDH");
			return strNodeContent;
		}
		public static string ReplaceEsqapeXmlCharacters(string strNodeContent)
		{
			strNodeContent = strNodeContent.Replace("&amp;","&");
			//strNodeContent = strNodeContent.Replace("\"","&quot;");
			strNodeContent = strNodeContent.Replace("H673GSUYD7G3J73UDH","'");
			return strNodeContent;
		}
		#endregion
		#region Age Calculation
		private static string DateDiffAsString(DateTime inDate1, DateTime inDate2)
		{ 
			int y = 0;   int m = 0;   int d = 0;   //make sure date1 is before (or equal to) date2.. 
			DateTime date1 = inDate1 <= inDate2 ? inDate1 : inDate2;
			DateTime date2 = inDate1 <= inDate2 ? inDate2 : inDate1;
			DateTime temp1;
			if(DateTime.IsLeapYear(date1.Year) && !DateTime.IsLeapYear(date2.Year) && date1.Month == 2 && date1.Day == 29)
			{
				temp1 = new DateTime(date2.Year, date1.Month, date1.Day-1);
			}
			else 
			{  temp1 = new DateTime(date2.Year, date1.Month, date1.Day); }  
			y = date2.Year - date1.Year - (temp1 > date2 ? 1 : 0);  
			m = date2.Month - date1.Month + (12 * (temp1 > date2 ? 1 : 0));  
			d = date2.Day - date1.Day; 
			if (d < 0)   
			{   
				if (date2.Day == DateTime.DaysInMonth(date2.Year, date2.Month) && (date1.Day >= DateTime.DaysInMonth(date2.Year, date2.Month) || date1.Day >= DateTime.DaysInMonth(date2.Year, date1.Month)))  
				{
					d = 0; 
				}  
				else  
				{   
					m--; 
					if (DateTime.DaysInMonth(date2.Year, date2.Month) == DateTime.DaysInMonth(date1.Year, date1.Month) && date2.Month != date1.Month)  
					{   
						int dayBase = date2.Month - 1 > 0 ? DateTime.DaysInMonth(date2.Year, date2.Month - 1) : 31; 
						d = dayBase + d; 
					}  
					else  
					{   
						// d = DateTime.DaysInMonth(date2.Year, date1.Month) + d;    
						d = DateTime.DaysInMonth(date2.Year, date2.Month == 1 ? 12 : date2.Month - 1) + d; 
					}  
				}  
			}          
			string ts = "";   
			if (y >= 0)           
				ts += y + ":";    
			if (m >= 0)      
				ts += m + ":";     
			if (d >= 0)     
				ts += d;
 			return ts;     

		}

		public static string CalculateDriverAge(string Date_1 ,string Date_2)
		{
			string ageDiff = DateDiffAsString(DateTime.Parse(Date_1.Trim()),DateTime.Parse(Date_2.Trim()));
			string[] arrayDate = ageDiff.Split(':');
			return arrayDate[0].ToString();
		}
		#endregion
		
		#region Calculate Symbol from Cost
		public string GetSymbolForVehicle(string VehicleType,int Amount,int Year)
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
				catch(Exception ex)
				{
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					return "";
				}
				finally{}
		}
		public int GetSymbol(string VehicleType, int Amount)
		{
			int Symbol=0;			
			
			XmlDocument xDoc=new XmlDocument();
			xDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml")); 			
			

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

		public int GetSymbolForCustomized(string VehicleType, int Amount, int Year)
		{
			int Symbol=0;			
			
			XmlDocument xDoc=new XmlDocument();
			xDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/cmsweb/xsl/symbol/VehicleSymbols.xml")); 			

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
		#endregion
	}
}
