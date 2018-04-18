/******************************************************************************************
	<Author					: Deepak Gupta ->
	<Start Date				: Aug, 25 2005->
	<End Date				: - >
	<Description			: - This class will be used to fetch data related to Home quick quote>
	<Review Date			: - >
	<Reviewed By			: - >
	Modification History
	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/
using System;
using System.Xml;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.Data;
using Cms.Model.Quote;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsHome.
	/// </summary>
	public class ClsHome: Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		public ClsHome()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public const string FLOATER_DED = "500";

		public string GetUserDefaultXml(string UserID,string LobCd,string StateName)
		{
			string	strStoredProc =	"Proc_GetQuickQuoteUserDefaultXml";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@USER_ID",UserID);
			objWrapper.AddParameter("@LOB",LobCd);
			objWrapper.AddParameter("@STATE",StateName);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if (ds.Tables[0].Rows.Count > 0)
				return(ds.Tables[0].Rows[0][0].ToString());
			else
				return("");
			ds.Dispose();
		}

		public void SaveUserDefaultXml(ClsQuickQuoteInfo objQuickDefXml)
		{
			string		strStoredProc	=	"Proc_InsertUpdateQuickQuoteUserDefaultXml";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@USER_ID",objQuickDefXml.USER_ID);
				objDataWrapper.AddParameter("@DEFAULT_XML",objQuickDefXml.DEFAULT_XML);
				objDataWrapper.AddParameter("@LOB",objQuickDefXml.LOB);
				objDataWrapper.AddParameter("@STATE",objQuickDefXml.STATE);
				
				int returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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

		public string UpdateZipInsuranceScoreIntoXml(string strQQHomeXml, string CustomerId)
		{
			XmlDocument domQQHOME = new XmlDocument();
			domQQHOME.LoadXml(strQQHomeXml);
							
			DataSet ldsQQHome = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1)) WHEN -1 THEN '100' WHEN 0 THEN '100'  WHEN -2 THEN 'NOHITNOSCORE' ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) end  CUSTOMER_INSURANCE_SCORE,CUSTOMER_ZIP FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=" + CustomerId);
			//XmlNode Node = domQQHOME.SelectSingleNode("DWELLINGDETAILS");
			//domQQHOME.SelectSingleNode("DWELLINGDETAILS").Attributes["ADDRESS"].Value = ldsQQHome.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim();
			
			if (ldsQQHome.Tables[0].Rows[0]["CUSTOMER_INSURANCE_SCORE"].ToString().Trim()!="")
			{
				domQQHOME.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/INSURANCESCORE").InnerText = ldsQQHome.Tables[0].Rows[0]["CUSTOMER_INSURANCE_SCORE"].ToString().Trim();
			}
			
			return(domQQHOME.OuterXml.ToString().Trim());
		}

		public string UpdateTerritoryCodeIntoXml(string strQQHomeXml)
		{
			XmlDocument domQQHOME = new XmlDocument();
			domQQHOME.LoadXml(strQQHomeXml);
			string strOutPutXml="";
				
			string strSQL = "SELECT ISNULL(TERR,'') TERR,ISNULL(ZONE,'1') ZONE,CITY,COUNTY FROM MNT_TERRITORY_CODES WHERE LOBID=" + domQQHOME.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/LOB_ID").InnerText.ToString().Trim() + " AND ZIP='" + domQQHOME.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS").Attributes["ADDRESS"].Value.ToString().Trim().Replace("'","''") + "'";
			DataSet ldsQQHome = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,strSQL);
			if (ldsQQHome.Tables[0].Rows.Count > 0)
			{
				if (ldsQQHome.Tables[0].Rows[0]["TERR"].ToString().Trim()!="")
				{
					domQQHOME.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TERRITORYCODES").InnerText = ldsQQHome.Tables[0].Rows[0]["TERR"].ToString().Trim();
					if (domQQHOME.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TERRITORYZONE")!=null)
					{
						domQQHOME.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TERRITORYZONE").InnerText = ldsQQHome.Tables[0].Rows[0]["ZONE"].ToString().Trim();
						domQQHOME.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TERRITORYNAME").InnerText = ldsQQHome.Tables[0].Rows[0]["CITY"].ToString().Trim();
						domQQHOME.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TERRITORYCOUNTY").InnerText = ldsQQHome.Tables[0].Rows[0]["COUNTY"].ToString().Trim();
					}
					strOutPutXml=domQQHOME.OuterXml.ToString().Trim();
				}
			}
			return(strOutPutXml);
		}
		//GET TERRCOUNTY
		public string GetTerritoryCounty(string lobID,string zipCode)
		{
			string strSQL = "SELECT COUNTY FROM MNT_TERRITORY_CODES WHERE LOBID=" + lobID + " AND ZIP='" + zipCode + "'";
			DataSet ldsQQTerr = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,strSQL);
			if (ldsQQTerr.Tables[0].Rows.Count > 0)
			{
				return ldsQQTerr.Tables[0].Rows[0]["COUNTY"].ToString().Trim();
			}
			return("");

		}
				


		/*Use to fetch the Protection Class for Rental dwelling and Home : 07 June 2006*/
		public DataSet FetchProtectionClass(string protectionClass,int milesToDwell,string feetToHydrant,string lobCode)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@FIREPROTECTIONCLASS",protectionClass,SqlDbType.VarChar );
				objDataWrapper.AddParameter("@MILESTODWELLING",milesToDwell,SqlDbType.Int );
				objDataWrapper.AddParameter("@FEETTOHYDRANT",feetToHydrant,SqlDbType.VarChar );
				objDataWrapper.AddParameter("@LOB",lobCode,SqlDbType.VarChar );
								
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetProtectionClass");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}

		public string PrepareHomeAcordXml(string CustomerId,string QuoteId,string AcordXmlPath,string StateCd,string QuoteNumber)
		{
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			DataSet ObjDs;

			string QuickQuoteXml = new ClsQuickQuote().GetQuickQuoteXml(CustomerId,QuoteId);
			string AcordXml="";
			string strProductType;
			
			XmlDocument AcordDom=new XmlDocument();
			XmlDocument QuoteDomTemp=new XmlDocument();
			XmlDocument QuoteDom=new XmlDocument();

			if (QuickQuoteXml.Trim()!="")
			{
				string tempString="";
				QuoteDomTemp.LoadXml(QuickQuoteXml);
				if(QuoteDomTemp != null)
				{
					tempString = QuoteDomTemp.DocumentElement.FirstChild != null ? QuoteDomTemp.DocumentElement.FirstChild.OuterXml.ToString():"";
				}

				QuoteDom.LoadXml(tempString);
				AcordDom.Load(AcordXmlPath);
				
				XmlNode CustomerNode = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/InsuredOrPrincipal");
				
				//Customer Info
				objWrapper.AddParameter("@CustomerID",CustomerId);
				ObjDs = objWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
				string strCustomerStateCode = ObjDs.Tables[0].Rows[0]["CUSTOMER_STATE_CODE"].ToString().Trim();

				if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11110")
				{
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
				}
				else if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11109")
				{
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
				}
				
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/AddrTypeCd").InnerText = "StreetAddress";
				XmlNode Node1 = CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/AddrTypeCd");
				Node1.InnerText = "StreetAddress";
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr1").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr2").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/City").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd").InnerText = strCustomerStateCode;
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim();
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS").Attributes["ADDRESS"].Value.ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/CountryCd").InnerText = "us";
				CustomerNode.SelectSingleNode("InsuredOrPrincipalInfo").Attributes["id"].Value = CustomerId;
				
				//Customer Info End
				//Agency Info
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString().Trim();
				//add by kranti on 17th april 2007 for agency id
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialId").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_ID"].ToString().Trim();

				//Agency Info End

				//Location Info 
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Location [@id='L01']/Addr/StateProvCd").InnerText = StateCd;
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Location [@id='L01']/Addr/PostalCode").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS").Attributes["ADDRESS"].Value.ToString().Trim();

				//Pers Policy Info Start
				XmlNode PersPolicyNode = AcordDom.SelectSingleNode("//PersPolicy");
				PersPolicyNode.SelectSingleNode("ControllingStateProvCd").InnerText = StateCd;

				PersPolicyNode.SelectSingleNode("ContractTerm/EffectiveDt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				
				//AppTerms add by kranti
				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/TERMFACTOR") != null)
					PersPolicyNode.SelectSingleNode("ContractTerm/AppTerms").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/TERMFACTOR").InnerText;

				string effDate = QuoteDom.SelectSingleNode("DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				//DateTime dtExpDate = DateTime.Parse(effDate).AddMonths(12);
				int noMonths = Convert.ToInt32(QuoteDom.SelectSingleNode("DWELLINGDETAILS/TERMFACTOR").InnerText);
				DateTime dtExpDate = DateTime.Parse(effDate).AddMonths(noMonths);

				//PersPolicyNode.SelectSingleNode("ContractTerm/ExpirationDt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/QUOTEEXPDATE").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("ContractTerm/ExpirationDt").InnerText = dtExpDate.ToString();

				PersPolicyNode.SelectSingleNode("OriginalInceptionDt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				
				PersPolicyNode.SelectSingleNode("QuoteInfo/CompanysQuoteNumber").InnerText = QuoteNumber;
				PersPolicyNode.SelectSingleNode("QuoteInfo/InitialQuoteRequestDt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("MultiPolicy").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/MULTIPLEPOLICYFACTOR").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("NonsmokerCredit").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/NONSMOKER").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("WoodstoveSurcharge").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/WOODSTOVE_SURCHARGE").InnerText.ToString().Trim();
				
				


				//Breed of Dogs
				PersPolicyNode.SelectSingleNode("NoOfDogs").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/DOGSURCHARGE").InnerText.ToString().Trim();
				//PersPolicyNode.SelectSingleNode("DogBreed").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/BREEDOFDOG").InnerText.ToString().Trim();

				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/BREEDOFDOG_LIST")!=null)
                    PersPolicyNode.SelectSingleNode("DogBreed").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/BREEDOFDOG_LIST").InnerText.ToString().Trim();


				
				//For Exp Credit and Loss Free Status --Added on 25 jan 2006
				PersPolicyNode.SelectSingleNode("ExperienceCredit").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/EXPERIENCE").InnerText.ToString().Trim();
				
				PersPolicyNode.SelectSingleNode("LossFree").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/LOSSFREE").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("NotLossFree").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/NOTLOSSFREE").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("NotLossFreeSurcharge").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PRIOR_LOSS_SURCHARGE").InnerText.ToString().Trim();
				//End Loss free nad Exp Credit
			
				//Added By Ravindra(08-28-2006)  

				PersPolicyNode.SelectSingleNode("AnyFarming").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/ANY_FARMING").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("FarmingTypeCd").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/FARMING_DETAIL_CODE").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("AnyHorses").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/ANY_HORSES").InnerText.ToString().Trim();
				
								
				//add by kranti on 7th march 2007
				//NoOfLocation

				if(int.Parse(QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTH_LOC_OPR_EMPL_HO73").InnerText.Trim())>0)
				{
					PersPolicyNode.SelectSingleNode("NoOfLocation").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTH_LOC_OPR_EMPL_HO73").InnerText.ToString().Trim();
				}
				else if(int.Parse(QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTH_LOC_OPR_OTHERS_HO73").InnerText.Trim())>0)
				{
					PersPolicyNode.SelectSingleNode("NoOfLocation").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTH_LOC_OPR_OTHERS_HO73").InnerText.ToString().Trim();

				}

				//For OtherStructure (08-30-2006)
				/*XmlNode OtherStructureNode = AcordDom.SelectSingleNode("//OtherStructure");
				OtherStructureNode.SelectSingleNode("OnPremises").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/LOCATION_ON_PREMISES").InnerText.ToString().Trim();
				OtherStructureNode.SelectSingleNode("CoverageBasis").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/COVERAGE_BASIS").InnerText.ToString().Trim();
				OtherStructureNode.SelectSingleNode("InsValueOnPremises").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/INSURING_VALUE_ON_PREMISES").InnerText.ToString().Trim();

				OtherStructureNode.SelectSingleNode("OffPremises").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/LOCATION_OF_PREMISES").InnerText.ToString().Trim();
				OtherStructureNode.SelectSingleNode("InsValueOffPremises").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/INSURING_VALUE_OF_PREMISES").InnerText.ToString().Trim();*/

				//End Of Other Structure
				//Added By Ravindra End Here

				//Modified Other Structures March 29 2007
				XmlNode OtherStructureNode = AcordDom.SelectSingleNode("//OtherStructure");
				//Coverege HO-489
				if(int.Parse(QuoteDom.SelectSingleNode("DWELLINGDETAILS/REPAIRCOSTADDITIONAL").InnerText.ToString())>0)
				{
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO489']/OnPremises").InnerText =  "ONP";  //Code On Premises
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO489']/CoverageBasis").InnerText =  "RP";  //Code COverage Basis
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO489']/InsValueOnPremises").InnerText =  QuoteDom.SelectSingleNode("DWELLINGDETAILS/REPAIRCOSTADDITIONAL").InnerText.ToString();  
				}
				//Coverege HO-48
				if(int.Parse(QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO48ADDITIONAL").InnerText.ToString())>0)
				{
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO48']/OnPremises").InnerText =  "ONP";  //Code On Premises
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO48']/CoverageBasis").InnerText =  "RL";  //Code COverage Basis
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO48']/InsValueOnPremises").InnerText =  QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO48ADDITIONAL").InnerText.ToString(); 
				}
				
				//Coverege HO-40
				if(int.Parse(QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO40ADDITIONAL").InnerText.ToString())>0)
				{
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO40']/OnPremises").InnerText =  "OPRO";  //Code On Premises/Rented to others
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO40']/CoverageAmt").InnerText =  QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO40ADDITIONAL").InnerText.ToString(); 
				}

				//Coverege HO-490
				if(int.Parse(QuoteDom.SelectSingleNode("DWELLINGDETAILS/SPECIFICSTRUCTURESADDITIONAL").InnerText.ToString())>0)
				{
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO490']/OffPremises").InnerText =  "OPR";  //Code Off Premises
					OtherStructureNode.SelectSingleNode("Coverage[@Code='HO490']/InsValueOffPremises").InnerText =  QuoteDom.SelectSingleNode("DWELLINGDETAILS/SPECIFICSTRUCTURESADDITIONAL").InnerText.ToString(); 
				}

				//END : Modified Other Structures



				//Location
				XmlNode Location = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Location[@id='L01']");
				Location.SelectSingleNode("IsPrimary").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/LOCATIONCODE").InnerText.ToString().Trim();
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/WOLVERINEINSURESPRIMARY")!=null)
					Location.SelectSingleNode("InsuresPrimary").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/WOLVERINEINSURESPRIMARY").InnerText.ToString().Trim();

				//Deductible
				if ( QuoteDom.SelectSingleNode("DWELLINGDETAILS/DEDUCTIBLE") != null )
				{
					Location.SelectSingleNode("Deductible").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/DEDUCTIBLE").InnerText.Trim();
				}

				//HOME LINE OF BUSINESS
				XmlNode HOMELOB = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/HomeLineBusiness");
				HOMELOB.SelectSingleNode("RateEffectiveDt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/PolicyTypeCd").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS").Attributes["POLICYCODE"].Value.Trim();
				
				//Added By Ravindra (08-03-2006)
				strProductType=HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/PolicyTypeCd").InnerText;
				///Added By Ravindra End Here
				
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/YearBuilt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/DOC").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/TotalInsurableReplCostAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/REPLACEMENTCOSTFACTOR").InnerText.ToString().Trim();


				#region Loss Free Renewal Discount & Prior Loss Surcharge Itrack 6640

				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/YEARSCONTINSUREDWITHWOLVERINE")!=null)
                    HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/NoOfYearsInsured").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/YEARSCONTINSUREDWITHWOLVERINE").InnerText.ToString().Trim();

				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/NONWEATHERLOSSES")!=null)
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/NonWeatherLosses").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/NONWEATHERLOSSES").InnerText.ToString().Trim();

				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/WEATHERLOSSES")!=null)
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/WeatherLosses").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/WEATHERLOSSES").InnerText.ToString().Trim();

				#endregion

				
				//add by kranti
				if(strProductType == HomeProductType.HO4_TENANT ||strProductType ==  HomeProductType.HO6_UNIT )
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/DwellInspectionValuation/NumOfUnits").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/NUMBEROFUNITS").InnerText.ToString().Trim();
				else
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/DwellInspectionValuation/NumFamilies").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/NUMBEROFFAMILIES").InnerText.ToString().Trim();
			

				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/ConstructionCd").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/EXTERIOR_CONSTRUCTION").InnerText.ToString().Trim();

				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/FEET2HYDRANT").InnerText.ToString().Trim().Replace(" ","").ToUpper() == "1000ORLESS")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/DistanceToHydrant/NumUnits").InnerText = "11555"; // Lookup Unique id for "1000 or less" 
				else
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/DistanceToHydrant/NumUnits").InnerText = "11556";// Lookup Unique id for "greater than 1000" 
				
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/DistanceToFireStation/NumUnits").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/DISTANCET_FIRESTATION").InnerText.ToString().Trim();

				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/NumGasOrSmokeAlarms").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/N0_LOCAL_ALARM").InnerText.ToString().Trim();

				
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/CONSTRUCTIONCREDIT").InnerText.ToString().Trim()=="Y")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/FinishedConstructionPct").InnerText = "50";
				else	
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/FinishedConstructionPct").InnerText = "100";
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/FireProtectionClassCd").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/FIREPROTECTIONCLASS").InnerText.ToString().Trim();
				
				
				//Schedule Property Rest
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/NumEmployeesFullTimeResidence").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/RESIDENCE_EMP_NUMBER").InnerText.ToString().Trim();
				
				//Modify by kranti
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/RESIDENCE_PREMISES").InnerText.ToString().Trim() != "0.00" || QuoteDom.SelectSingleNode("DWELLINGDETAILS/OCCUPIED_INSURED").InnerText.ToString().Trim() != "0.00" ||
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTHER_LOC_1FAMILY").InnerText.ToString().Trim() != "0.00" || QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTHER_LOC_2FAMILY").InnerText.ToString().Trim() != "0.00" )
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/AdditionalResidenceInd").InnerText = "Y";
				else
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/AdditionalResidenceInd").InnerText = "N";

				//Modify By kranti for "Any Business conducted on Premises(unserwriting Question)"



				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/ONPREMISES_HO42").InnerText.Trim().ToUpper() == "Y"  || QuoteDom.SelectSingleNode("DWELLINGDETAILS/LOCATED_OTH_STRUCTURE").InnerText.Trim().ToUpper() == "Y" ||
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/INSTRUCTIONONLY_HO42").InnerText.Trim().ToUpper() == "Y" || QuoteDom.SelectSingleNode("DWELLINGDETAILS/OFF_PREMISES_HO43").InnerText.Trim().ToUpper() == "Y" ||
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/CLERICAL_OFFICE_HO71").InnerText.Trim().ToUpper() == "Y" || QuoteDom.SelectSingleNode("DWELLINGDETAILS/SALESMEN_INC_INSTALLATION").InnerText.Trim().ToUpper() == "Y" ||
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/TEACHER_ATHELETIC").InnerText.Trim().ToUpper() == "Y" || QuoteDom.SelectSingleNode("DWELLINGDETAILS/TEACHER_NOC").InnerText.Trim().ToUpper() == "Y" )
					
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/AnyBusinessConductedOnPremises").InnerText = "Y";
				else
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/AnyBusinessConductedOnPremises").InnerText = "N";


				
				
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/BURGLER_ALERT_POLICE").InnerText.ToString().Trim() == "Y")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/BurgAlarmAlertPolice").InnerText = "Y";
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/BURGLAR_ACORD").InnerText.ToString().Trim() == "52")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/CentStBurgAlarm").InnerText = "Y";
				
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/FIRE_ALARM_FIREDEPT").InnerText.ToString().Trim() == "Y")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/FireAlertFire").InnerText = "Y";
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/CENTRAL_FIRE").InnerText.ToString().Trim() == "Y")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/CentStFireAlarm").InnerText = "Y";

				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/CertAttached").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/CERTIFICATE_ATTACHED").InnerText.ToString().Trim();

				//Suburban Discount
				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/SUBURBAN_CLASS")!=null)
                    HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/SuburbanClassDiscount/SubUrbanDiscount").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SUBURBAN_CLASS").InnerText.ToString().Trim();

				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/LOCATED_IN_SUBDIVISION")!=null)
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/SuburbanClassDiscount/LocatedInSubDivision").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/LOCATED_IN_SUBDIVISION").InnerText.ToString().Trim();

				//Prior Loss : Waterpump and Sump Loss
				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/WATERBACKUP_LOSS")!=null)
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/PriorLossInfo/WaterPumpLoss").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/WATERBACKUP_LOSS").InnerText.ToString().Trim();
			
				

				//Market Value///////////
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/DwellInspectionValuation/MarketValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/DWELLING_LIMITS").InnerText;

				//Coverages
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS/EXPERIENCE").InnerText.ToString().Trim() == "0" && QuoteDom.SelectSingleNode("DWELLINGDETAILS/EXPERIENCE").InnerText.ToString().Trim() == "")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']").RemoveChild(HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'MATUR')]").ParentNode);
				
				//Added By Ravindra

				XmlNode quoteNode = null;
				XmlNode acordNode = null;

				if(strProductType == HomeProductType.HO4_TENANT ||strProductType ==  HomeProductType.HO6_UNIT )
				{
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/DWELLING_LIMITS");
					//acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBUSPP')]").ParentNode;
					acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage[CoverageCd='EBUSPP']");

					if ( quoteNode != null )
					{
						acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
				
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYINCREASEDLIMITADDITIONAL");

					if ( quoteNode != null )
					{
						acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}

					//HO-51 End cov C//


				}
				else
				{

					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'DWELL')]").ParentNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/DWELLING_LIMITS").InnerText.ToString().Trim();
					//HO-51///  --Shown in Coverage C
					//Building Additions & Alterations - Increased Limits (HO-51)
				
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYINCREASEDLIMITINCLUDE");
					//acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBUSPP')]").ParentNode;
					acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage[CoverageCd='EBUSPP']");

					if ( quoteNode != null )
					{
						acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
				
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYINCREASEDLIMITADDITIONAL");

					if ( quoteNode != null )
					{
						acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}

					//HO-51 End cov C//
				}
				
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'PL')]").ParentNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALLIABILITY_LIMIT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'MEDPM')]").ParentNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/MEDICALPAYMENTSTOOTHERS_LIMIT").InnerText.ToString().Trim();
								
				XmlNode dwellNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']");
				
				XmlNode sampleNode = dwellNode.SelectSingleNode("Coverage/CoverageCd[contains(.,'Sample')]").ParentNode;
				
				//Perill Deductible
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/DEDUCTIBLE");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToUpper() != "" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "APD";
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						dwellNode.AppendChild(clonedNode);
					}
				}
				//End of perill Deductible
				//Replacement Cost Dwelling : 9 feb 2006
								
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO34");
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToUpper() != "" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "RCOST";
						if (quoteNode.InnerText.ToString().Trim().ToUpper() == "Y")
						{
							clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "1";
						}
						else
						{
							clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "0";
						}
						dwellNode.AppendChild(clonedNode);
					}
				}
				//End of Replacement Cost Dwelling

				//
				//Coverage E - Personal Liability
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALLIABILITY_LIMIT");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToUpper() != "" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "PL";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						dwellNode.AppendChild(clonedNode);
					}
				}
					
				//HO-216
				//AddBy Kranti					

				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/N0_LOCAL_ALARM").InnerText.ToString().Trim()			!= "0" ||			
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/BURGLER_ALERT_POLICE").InnerText.ToString().Trim()	== "Y" ||
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/BURGLAR").InnerText.ToString().Trim()				== "Y" ||				
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/FIRE_ALARM_FIREDEPT").InnerText.ToString().Trim()	== "Y" ||
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/CENTRAL_FIRE").InnerText.ToString().Trim()			== "Y" &&				
					QuoteDom.SelectSingleNode("DWELLINGDETAILS/CERTIFICATE_ATTACHED").InnerText.ToString().Trim()	== "Y"   )
				{
					XmlNode clonedNode = sampleNode.Clone();
					clonedNode.SelectSingleNode("CoverageCd").InnerText = "HO216";
					//clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";
					dwellNode.AppendChild(clonedNode);

				}
				//End HO-216


				//

				//Added By Ravindra 
				//Remove Coverage A as not Applicable in HO-4 
				if(strProductType == HomeProductType.HO4_TENANT)
				{
					acordNode =HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'DWELL')]").ParentNode;
					dwellNode.RemoveChild(acordNode); 
				}

				//Added By Ravindra End Here 
				//H0-9
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO9");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'HO9')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				//End of HO9


				//Added By Ravindra (08-07-2006)
				//HO200

				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO200");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'HO200')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}

				//Added By Ravindra End Here

				//H0-20
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO20");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBP20')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}

				//HO-21
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO21");				
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBP21')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						if (HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBP21')]") != null)
						{
							dwellNode.RemoveChild(acordNode);
						}
					}
				}
			

				//HO-22 -- In mishigan HO22 data is picked from HO 21 only
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO22");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBP22')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						if (HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBP22')]") != null)
						{
							dwellNode.RemoveChild(acordNode);
						}
					}
				}
				
				//HO-23
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO23");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBP23')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				//HO-24
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO24");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBP24')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				
				//VIP premium
				//HO-25
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO25");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBP25')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				//End VIP Premium

				//HO-34
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO34");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBRCPP')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				
				//HO-11
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO11");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBEP11')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				
				//HO-32
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO32");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBCASP')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				
				//HO-277
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO277");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'SEWER')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				
				//HO-455
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO455");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'FRAUD')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
					else //If Yes Set Limit of HO-455
					{
						if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO455_LIMIT")!=null)
						{
							if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO455_LIMIT").InnerText!="")
                                acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO455_LIMIT").InnerText;
						}

					}
				}
				
				//HO-439
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO493");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'HO493')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				}
				//HO-315
				//Added By Ravindra
				//For product type HO-5 (Replacement & Premier) Select "Earthquake Coverage on Buidling"

				if(strProductType == HomeProductType.HO5_PREMIER  || strProductType == HomeProductType.HO5_REPLACEMENT)
				{
					acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'ECOB')]").ParentNode;
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO315");
					if ( quoteNode != null )
					{
						if ( quoteNode.InnerText.Trim().ToLower() == "n" )
						{
							dwellNode.RemoveChild(acordNode);
						}
					}
					//Remove EROK
					acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EROK')]").ParentNode;
					dwellNode.RemoveChild(acordNode);
				}
					//For other product type  Select "Earthquake (HO-315)"
				else
				{
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO315");
					acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EROK')]").ParentNode;

					if ( quoteNode != null )
					{
						if ( quoteNode.InnerText.Trim().ToLower() == "n" )
						{
							dwellNode.RemoveChild(acordNode);
						}
					}
					//Remove ECOB
					acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'ECOB')]").ParentNode;
					dwellNode.RemoveChild(acordNode);
				}
				
				//Added By Ravindra (08-08-2006) To import Reduction in Limit - Coverage C
				//REDUCTION_IN_COVERAGE_C
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/REDUCTION_IN_COVERAGE_C");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage[CoverageCd='REDUC']");

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
					else
					{
						acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
				}
				//Added By Ravindra Ends here

				//HO-287
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO287");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'MIN##')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
				
				}
				//MINESUBSIDENCE_ADDITIONAL
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/MINESUBSIDENCE_ADDITIONAL");
				
				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				//HO-96///////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO96INCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBIF96')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}

				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO96ADDITIONAL");
				
				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}

				/////////////////////
								
				//HO-48///////////////

				//VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COLL']"));
				//EBOS48
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO48INCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage[CoverageCd='OS']");

				if ( quoteNode != null )
				{				
					if(quoteNode.InnerText.Trim()!="N/A")
						acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					else
						acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "0";

				}

				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO48ADDITIONAL");
				
				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}

				/////////////////////
				///

				//HO-40///////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO40INCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBOS40')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}

				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO40ADDITIONAL");
				
				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}

				/////////////////////
		
				//HO-50///
				//Personal Property Coverage C Increased Limits Away from Premises (HO-50)
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYAWAYINCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBPPOP')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText =  quoteNode.InnerText.Trim()=="0"?"":quoteNode.InnerText.Trim().ToString();

				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYAWAYADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					
				}

				//HO-50 End//
				//HO-51/// for HO-4
				//Building Additions & Alterations - Increased Limits (HO-51)
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYINCREASEDLIMITINCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBBAA')]").ParentNode;

				if ( quoteNode != null )
				{
					
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYINCREASEDLIMITADDITIONAL");
				
				if ( quoteNode != null )
				{
					//This Check Itrack #2635 :Modified 29 Nov 2007
					if(Double.Parse(QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYAWAYADDITIONAL").InnerText.ToString()) <= 0.00)
					{
						if(Double.Parse(quoteNode.InnerText.ToString()) >= 0.00)  
						{
							HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']").RemoveChild(HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBPPOP')]").ParentNode);
						}
					}
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}

				//HO-51 End//

				//9 feb 2006 : HO 6 DWELL COv A
				//Added By Ravindra 
				if(strProductType != HomeProductType.HO4_TENANT)
				{
					acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'DWELL')]").ParentNode;
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PERSONALPROPERTYINCREASEDLIMITADDITIONAL");
					if ( quoteNode != null )
					{
						acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
				}

				//

				//HO-33 
				//Condo - Unit Owners Rental to Others (HO-33)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO33");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBUNIT')]").ParentNode;
				
				if ( quoteNode != null )
				{
					
					if ( quoteNode.InnerText.Trim().ToLower() == "n" )
					{
						dwellNode.RemoveChild(acordNode);
					}
					acordNode.SelectSingleNode("Included/Text").InnerText = quoteNode.InnerText.Trim();
				}

				//Condo end
				
				//Jewelry//////////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/UNSCHEDULEDJEWELRYINCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBCCSL')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/UNSCHEDULEDJEWELRYADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				////////////////////
				
				//Money////////////////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/MONEYINCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBCCSM')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/MONEYADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				////////////////
				
				//Securities///////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SECURITIESINCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'ESCCSS')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SECURITIESADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				////////////////
				
				//Silverware///////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SILVERWAREINCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBCCSI')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SILVERWAREADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				////////////////
				
				//Firearms///////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/FIREARMSINCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBCCSF')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/FIREARMSADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				////////////////
				//

				//	HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']").RemoveChild(HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'MATUR')]").ParentNode);
				//dwellNode.RemoveChild(acordNode);


				//HO-312///////////////Business Property On Premises Coverage (HO-312) 
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO312INCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'IBUSP')]").ParentNode;

				if ( quoteNode != null )
				{
					
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					
				}
				
				// If Additional Amount is Selected than Grant Coverage "IBUSPA" with Deductible = Additional

				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'IBUSPA')]").ParentNode;
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO312ADDITIONAL");

				if ( quoteNode != null)
				{
					if ( quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0")
					{
						HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']").RemoveChild(HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'IBUSPA')]").ParentNode);
					}
					else
					{
						acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
				}
				////////////////
				///

				/*Business Property Off Premises Coverage (HO-312) 01 MARCH 2007*/
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO312OFFINCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'IBUSPO')]").ParentNode;

				if ( quoteNode != null )
				{
					
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					
				}			
				
				// If Additional Amount is Selected than Grant Coverage "IBUSPOA" with Deductible = Additional

				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'IBUSPOA')]").ParentNode;
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO312OFFADDITIONAL");

				if ( quoteNode != null)
				{
					if ( quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0")
					{
						HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']").RemoveChild(HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'IBUSPOA')]").ParentNode);

					}
					else
					{
						acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
				}

				/*END : Business Property Off Premises Coverage (HO-312) */


				
				//Repair Cost Homeowners (HO-489)
				//Other Structure(s) Repair Cost Coverage (HO-489)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/REPAIRCOSTINCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();
					clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBOS489";
					clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

					//Additional
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/REPAIRCOSTADDITIONAL");
					
					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}

					dwellNode.AppendChild(clonedNode);
				}
				////////////////////
				///
				
				//HO-4 Deluxe, Indiana
				//"Renters Deluxe Coverage (HO-64)" coverage : 3 march 2006
              
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS").Attributes["POLICYTYPE"].Value.ToString() == "HO-4 Tenants")
				{
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO64RENTERDELUXE");
					
					if(quoteNode.InnerText.Trim().ToLower() == "y")
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBRDC";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

			
						dwellNode.AppendChild(clonedNode);
					}
				}
				//**End HO 64


				//HO-6 Deluxe, Indiana
				//"Condominium Deluxe Coverage (HO-66)" coverage  : 3 march 2006
				//Previous Code :EBRDCC
				//New Node : EBCDC
				if (QuoteDom.SelectSingleNode("DWELLINGDETAILS").Attributes["POLICYTYPE"].Value.ToString() == "HO-6 Unit Owners")
				{
					quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO66CONDOMINIUMDELUXE");
					
					if(quoteNode.InnerText.Trim().ToLower() == "y")
					{
					
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "EBCDC";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";
		
						dwellNode.AppendChild(clonedNode);
					}
					
				}

				
				
				//Addl Living expenses///////////////Coverage D -Code 
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/ADDITIONALLIVINGEXPENSEINCLUDE");
				//acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBALEXP')]").ParentNode;
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage[CoverageCd='LOSUR']");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/ADDITIONALLIVINGEXPENSEADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				////////////////
				
				//HO-53///////////////Credit Card and Depositors Forgery (HO-53)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO53INCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBICC53')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO53ADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				////////////////
				

				//HO-35///////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO35INCLUDE");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'LAC')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim().Replace("N/A","0");
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO35ADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim().Replace("N/A","0");
				}
				////////////////
				
				//Specific structures///////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SPECIFICSTRUCTURESINCLUDE ");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'EBSS490')]").ParentNode;

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SPECIFICSTRUCTURESADDITIONAL");

				if ( quoteNode != null )
				{
					acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
				}
				////////////////
				
				//HO-327 Water Backup and Sump Pump Overflow///////////////
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/HO327");
				acordNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'WBSPO')]").ParentNode;

				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0")
					{
						HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']").RemoveChild(HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'WBSPO')]").ParentNode);											
					}
					else
					{
						acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

					}
				}
				

				/*
				 if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0")
					{
						dwellNode.RemoveChild(acordNode);
					}
					else
					{
						acordNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
				}*/
								
				//******************Liability options (Section 2)////////////////////
				
				
				//Additional Premises (Number of Premises) -Occupied by Insured
				//Modified 1 march 2006 : The coverages should be displayed as selected if more than 0 value is entered in QQ
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/OCCUPIED_INSURED");
				
				if ( quoteNode != null )
				{
					if ( !(quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0"))
					{
					
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "APOBI";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();


						dwellNode.AppendChild(clonedNode);
					}
				}

				///////////////////////////////////////////
				
				//Additional Premises (Number of Premises) -Residence Premises - Rented to Others
				//Modified 1 march 2006 : The coverages should be displayed as selected if more than 0 value is entered in QQ
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/RESIDENCE_PREMISES");
				
				if ( quoteNode != null )
				{
					if ( !(quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0"))
					{
					
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "APRPR";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();


						dwellNode.AppendChild(clonedNode);
					}
				}
				/////////////////////
				
				//Additional Premises (Number of Premises) -Other Location -Rented to Others (1 Famiy)
				//Modified 1 march 2006 : The coverages should be displayed as selected if more than 0 value is entered in QQ
				//Modifed 20 March 2007 : Removed code APOLR ,Applied New Code AROF1:
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTHER_LOC_1FAMILY");
				
				if ( quoteNode != null )
				{
					if ( !(quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0"))
					{
					
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "AROF1";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();


						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Additional Premises (Number of Premises) -Other Location -Rented to Others (2 Famiy)
				//Modified 1 march 2006 : The coverages should be displayed as selected if more than 0 value is entered in QQ
				//Modifed 20 March 2007 : Removed code APOLO ,Applied New Code AROF2:
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTHER_LOC_2FAMILY");
				
				if ( quoteNode != null )
				{
					if ( !(quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0"))
					{
					
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "AROF2";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();


						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Incidental Office , Private School or Studio - On Premises (HO-42)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/ONPREMISES_HO42");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "IOPSS";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Incidental Office , Private School or Studio - Located in Other Structure
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/LOCATED_OTH_STRUCTURE");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "IOPSL";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}
				}
				

				
				//Incidental Office , Private School or Studio - Instruction Only (HO-42)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/INSTRUCTIONONLY_HO42");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "IOPSI";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Incidental Office , Private School or Studio - Off Premises (HO-43)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/OFF_PREMISES_HO43");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "IOPSO";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Personal Injury (HO-82)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PIP_HO82");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "PERIJ";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}
				}

				//Residence Employees (number)
				//Modified 1 march 2006 : The coverages should be displayed as selected if more than 0 value is entered in QQ
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/RESIDENCE_EMP_NUMBER");
				
				if ( quoteNode != null )
				{
					if ( !(quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0"))
					{
					
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "REEMN";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Business Pursuits(HO-71)Clerical Office Employee,Salesmen,Collectors(No Instalation,Demo or Service)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/CLERICAL_OFFICE_HO71");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "BPCES";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}

				}
				
				//Business Pursuits(HO-71)Clerical Office Employee,Salesmen,Collectors(No Instalation,Demo or Service)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SALESMEN_INC_INSTALLATION");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "BPSCM";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}

				}
				
				//Business Pursuits(HO-71)Teachers-athletic,Lab,Manual Training and Phys.Ed(Excluding Corporal Punish)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/TEACHER_ATHELETIC");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "BPTAL";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}

				}
				
				//Business Pursuits(HO-71)Teachers-NOC (Excl. Corporal Punishment)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/TEACHER_NOC");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "BPTNO";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";

						dwellNode.AppendChild(clonedNode);
					}

				}


				
				//Farm Liability (number of locations) Incidental Farming on Premises(HO-72)
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/INCIDENTAL_FARMING_HO72");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToLower() == "y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "FLIFP";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = "-1";
						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Farm Liability (number of locations) Owned Farms Operated by Insured's Employees(HO-73)
				//Modified 1 march 2006 : The coverages should be displayed as selected if more than 0 value is entered in QQ
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTH_LOC_OPR_EMPL_HO73");
				
				if ( quoteNode != null)
				{
					if ( !(quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0"))
					{
					
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "FLOFO";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();;

						dwellNode.AppendChild(clonedNode);
					}
				}				
				//Farm Liability (number of locations) Owned Farms Rented to Others(HO-73)
				//Modified 1 march 2006 : The coverages should be displayed as selected if more than 0 value is entered in QQ
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/OTH_LOC_OPR_OTHERS_HO73");
				
				if ( quoteNode != null )
				{
					if ( !(quoteNode.InnerText.ToString().Trim() == "0.00" || quoteNode.InnerText.ToString().Trim() == "0"))
					{					
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "FLOFR";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();;

						dwellNode.AppendChild(clonedNode);
					}
				}


				//Seasonal Secondary
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SEASONALSECONDARY");
				
				if ( quoteNode != null )
				{
					if(quoteNode.InnerText.ToUpper().Trim() == "Y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "SEASEC";
						dwellNode.AppendChild(clonedNode);
					}

				}

				//Wolverine insured primary
				quoteNode = QuoteDom.SelectSingleNode("DWELLINGDETAILS/WOLVERINEINSURESPRIMARY");
				
				if ( quoteNode != null )
				{
					if(quoteNode.InnerText.ToUpper().Trim() == "Y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "WIP";
						dwellNode.AppendChild(clonedNode);
					}

				}
				
				//RP -- Changed the coverages codes. Now new codes are used in INLine Marine
				//Scheduled personal Property.
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'BICYC')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_BICYCLE_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'CAMER')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_CAMERA_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'CELLU')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_CELL_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'FURS')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_FURS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'GOLF')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_GOLF_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'GUNS')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_GUNS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'JEWEL')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_JWELERY_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'MUSIC')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_MUSICAL_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'PERSOD')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_PERSCOMP_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'SILVE')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_SILVER_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'STAMP')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_STAMPS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'RARE')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_RARECOINS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'FINEBR')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_FINEARTS_BREAK_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'FINEWBR')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_FINEARTS_WO_BREAK_AMOUNT").InnerText.ToString().Trim();

				//New Coverage Added By Shafi
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'HANDI')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_HANDICAP_ELECTRONICS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'HEARI')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_HEARING_AIDS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'INSUL')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_INSULIN_PUMPS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'MART')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_MART_KAY_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'PERSOL')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'SALES')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_SALESMAN_SUPPLIES_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'SCUBA')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_SCUBA_DRIVING_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'SNOW')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_SNOW_SKIES_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TACK')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TACK_SADDLE_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TOOLSP')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TOOLS_PREMISES_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TOOLSB')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TOOLS_BUSINESS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TRACT')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TRACTORS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TRAIN')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TRAIN_COLLECTIONS_AMOUNT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'WHEEL')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_WHEELCHAIRS_AMOUNT").InnerText.ToString().Trim();

				//New Inline Marines Itrack 6488
				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_CAMERA_PROF_AMOUNT")!=null)
					HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'CAMERPU')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_CAMERA_PROF_AMOUNT").InnerText.ToString().Trim();
				if( QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_MUSICAL_REMUN_AMOUNT")!=null)
					HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'MUSICR')]").ParentNode.SelectSingleNode("ItemValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_MUSICAL_REMUN_AMOUNT").InnerText.ToString().Trim();				



				//Dedcutibles
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'BICYC')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_BICYCLE_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'CAMER')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_CAMERA_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'CELLU')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_CELL_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'FURS')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_FURS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'GOLF')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_GOLF_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'GUNS')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_GUNS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'JEWEL')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_JWELERY_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'MUSIC')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_MUSICAL_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'PERSOD')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_PERSCOMP_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'SILVE')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_SILVER_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'STAMP')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_STAMPS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'RARE')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_RARECOINS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'FINEBR')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_FINEARTS_BREAK_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'FINEWBR')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_FINEARTS_WO_BREAK_DED").InnerText.ToString().Trim();

				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'HANDI')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_HANDICAP_ELECTRONICS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'HEARI')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_HEARING_AIDS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'INSUL')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_INSULIN_PUMPS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'MART')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_MART_KAY_DED").InnerText.ToString().Trim();
				//HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'PERSOD')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_PERSONAL_COMPUTERS_LAPTOP_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'PERSOL')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_PERSONAL_COMPUTERS_LAPTOP_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'SALES')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_SALESMAN_SUPPLIES_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'SCUBA')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_SCUBA_DRIVING_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'SNOW')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_SNOW_SKIES_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TACK')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TACK_SADDLE_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TOOLSP')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TOOLS_PREMISES_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TOOLSB')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TOOLS_BUSINESS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TRACT')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TRACTORS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'TRAIN')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_TRAIN_COLLECTIONS_DED").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'WHEEL')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_WHEELCHAIRS_DED").InnerText.ToString().Trim();

				//New Inline Marines Itrack 6488
				if(QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_CAMERA_PROF_DED")!=null)
                    HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'CAMERPU')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_CAMERA_PROF_DED").InnerText.ToString().Trim();
				if( QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_MUSICAL_REMUN_DED")!=null)
                    HOMELOB.SelectSingleNode("PropertySchedule/PropertyClassCd[contains(.,'MUSICR')]").ParentNode.SelectSingleNode("Deductible/Amt").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/SCH_MUSICAL_REMUN_DED").InnerText.ToString().Trim();				



				AcordXml=AcordDom.OuterXml;
			}
			return(AcordXml);
		}


		public string PrepareRentalHomeAcordXml(string CustomerId,string QuoteId,string AcordXmlPath,string StateCd,string QuoteNumber)
		{
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			DataSet ObjDs;

			string QuickQuoteXml = new ClsQuickQuote().GetQuickQuoteXml(CustomerId,QuoteId);
			string AcordXml="";
			
			XmlDocument AcordDom=new XmlDocument();
			XmlDocument QuoteDom=new XmlDocument();

			if (QuickQuoteXml.Trim()!="")
			{
				QuoteDom.LoadXml(QuickQuoteXml);
				AcordDom.Load(AcordXmlPath);
				
				XmlNode CustomerNode = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/InsuredOrPrincipal");
				
				//Customer Info
				objWrapper.AddParameter("@CustomerID",CustomerId);
				ObjDs = objWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
				string strCustomerStateCode = ObjDs.Tables[0].Rows[0]["CUSTOMER_STATE_CODE"].ToString().Trim();

				if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11110")
				{
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
				}
				else if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11109")
				{
					CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
				}
				
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/AddrTypeCd").InnerText = "StreetAddress";
				XmlNode Node1 = CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/AddrTypeCd");
				Node1.InnerText = "StreetAddress";
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr1").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr2").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/City").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd").InnerText = strCustomerStateCode;
				
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim();
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS").Attributes["ADDRESS"].Value.ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/CountryCd").InnerText = "us";
				CustomerNode.SelectSingleNode("InsuredOrPrincipalInfo").Attributes["id"].Value = CustomerId;
				
				//Customer Info End
				//Agency Info
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString().Trim();
				//add by kranti on 17th april 2007 for agency id
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialId").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_ID"].ToString().Trim();

				//Agency Info End
			

				//Location Info 
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Location [@id='L01']/Addr/StateProvCd").InnerText = StateCd;
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Location [@id='L01']/Addr/PostalCode").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS").Attributes["ADDRESS"].Value.ToString().Trim();

				//Pers Policy Info Start
				XmlNode PersPolicyNode = AcordDom.SelectSingleNode("//PersPolicy");
				PersPolicyNode.SelectSingleNode("ControllingStateProvCd").InnerText = StateCd;

				//AppTerms add by kranti
				if(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TERMFACTOR")!=null)
					PersPolicyNode.SelectSingleNode("ContractTerm/AppTerms").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TERMFACTOR").InnerText;


				PersPolicyNode.SelectSingleNode("ContractTerm/EffectiveDt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				
				string effDate = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				
				int noMonths = Convert.ToInt32(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TERMFACTOR").InnerText);
				DateTime dtExpDate = DateTime.Parse(effDate).AddMonths(noMonths);

				//PersPolicyNode.SelectSingleNode("ContractTerm/ExpirationDt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QUOTEEXPDATE").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("ContractTerm/ExpirationDt").InnerText = dtExpDate.ToString();

				PersPolicyNode.SelectSingleNode("OriginalInceptionDt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				
				PersPolicyNode.SelectSingleNode("QuoteInfo/CompanysQuoteNumber").InnerText = QuoteNumber;
				PersPolicyNode.SelectSingleNode("QuoteInfo/InitialQuoteRequestDt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("MultiPolicy").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/MULTIPLEPOLICYFACTOR").InnerText.ToString().Trim();

				//No of yrs Insured with wolverine: 9 feb 2006
				PersPolicyNode.SelectSingleNode("LossFree").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/LOSSFREE").InnerText.ToString().Trim();
				PersPolicyNode.SelectSingleNode("NoOfYearsInsured").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/NO_YEARS_WITH_WOLVERINE").InnerText.ToString().Trim();
				//
				
//
				//For OtherStructure (09-05-2006)
//				XmlNode OtherStructureNode = AcordDom.SelectSingleNode("//OtherStructure");
//				OtherStructureNode.SelectSingleNode("LocationPrimises").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/OTHERSTRUCTURES/PREMISES_LOCATION").InnerText.ToString().Trim();
//				OtherStructureNode.SelectSingleNode("CoverageBasis").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/OTHERSTRUCTURES/COVERAGE_BASIS").InnerText.ToString().Trim();
//				OtherStructureNode.SelectSingleNode("AdditionalAmountOfIns").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/OTHERSTRUCTURES/ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").InnerText.ToString().Trim();
//				OtherStructureNode.SelectSingleNode("IsSatelliteEquip").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/OTHERSTRUCTURES/SATELLITE_EQUIPMENT").InnerText.ToString().Trim();
				
				
				

				//add by kranti
				//For OtherStructure (23-05-2007)
				XmlNode OtherStructureNode = AcordDom.SelectSingleNode("//OtherStructure");
				if(int.Parse(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APPURTENANTSTRUCTURES_ADDITIONAL").InnerText.ToString())>0)
				{
					OtherStructureNode.SelectSingleNode("Coverage[@Code='OSTR']/OnPremises").InnerText =  "ONP";  //Code On Premises
					OtherStructureNode.SelectSingleNode("Coverage[@Code='OSTR']/CoverageBasis").InnerText =  "RL";  //Code COverage Basis
					OtherStructureNode.SelectSingleNode("Coverage[@Code='OSTR']/InsValueOnPremises").InnerText =  QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APPURTENANTSTRUCTURES_ADDITIONAL").InnerText.ToString();  
					OtherStructureNode.SelectSingleNode("Coverage[@Code='OSTR']/IsSatelliteEquip").InnerText = "2";


				}
				if(int.Parse(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/SATELLITEDISHES_ADDITIONAL").InnerText.ToString())>0)
				{
					OtherStructureNode.SelectSingleNode("Coverage[@Code='SD']/OnPremises").InnerText =  "ONP";  //Code On Premises
					OtherStructureNode.SelectSingleNode("Coverage[@Code='SD']/CoverageBasis").InnerText =  "RL";  //Code COverage Basis
					OtherStructureNode.SelectSingleNode("Coverage[@Code='SD']/InsValueOnPremises").InnerText =  QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/SATELLITEDISHES_ADDITIONAL").InnerText.ToString();  
					OtherStructureNode.SelectSingleNode("IsSatelliteEquip").InnerText = "1";

				}
				
				

				//End Of Other Structure
				//Added By Ravindra End Here

				//Location COmmented On 27 June 2006
				XmlNode Location = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/Location[@id='L01']");
				/*if (QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/SEASONALSECONDARY").InnerText.ToString().Trim() == "Y")
					Location.SelectSingleNode("IsPrimary").InnerText = "N";
				else
					Location.SelectSingleNode("IsPrimary").InnerText = "Y";*/
			
				if (QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/LOCATIONCODE") != null)
					Location.SelectSingleNode("IsPrimary").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/LOCATIONCODE").InnerText.ToString().Trim();
				else
					Location.SelectSingleNode("IsPrimary").InnerText = "";
				
				//Location Zip Code
				//				XmlNode postNode = Location.SelectSingleNode("Addr/PostalCode");
				//				if ( postNode != null )
				//				{
				//					if ( QuoteDom.SelectSingleNode("DWELLINGDETAILS").Attributes["ADDRESS"] != null )
				//					{
				//						Location.SelectSingleNode("Addr/PostalCode").InnerText= QuoteDom.SelectSingleNode("DWELLINGDETAILS").Attributes["ADDRESS"].Value.ToString().Trim();
				//					}
				//				}

															   
															   
				//Deductible
				if ( QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DEDUCTIBLE") != null )
				{
					Location.SelectSingleNode("Deductible").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DEDUCTIBLE").InnerText.Trim();
				}

				//HOME LINE OF BUSINESS
				XmlNode HOMELOB = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/HomePolicyQuoteInqRq/HomeLineBusiness");
				HOMELOB.SelectSingleNode("RateEffectiveDt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/QUOTEEFFDATE").InnerText.ToString().Trim();
				//HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/PolicyTypeCd").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/PRODUCTNAME").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/PolicyTypeCd").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS").Attributes["POLICYCODE"].Value.ToString();
				
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/YearBuilt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DOC").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/TotalInsurableReplCostAmt/Amt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/REPLACEMENTCOSTFACTOR").InnerText.ToString().Trim();
				//For Rental NumFamilies
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/DwellInspectionValuation/NumFamilies").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/NUMBEROFFAMILIES").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/ConstructionCd").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/EXTERIOR_CONSTRUCTION").InnerText.ToString().Trim();

				if (QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/FEET2HYDRANT").InnerText.ToString().Trim().Replace(" ","").ToUpper() == "1000ORLESS")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/DistanceToHydrant/NumUnits").InnerText = "11555"; // Lookup Unique id for "1000 or less" 
				else
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/DistanceToHydrant/NumUnits").InnerText = "11556";// Lookup Unique id for "greater than 1000" 
				
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/DistanceToFireStation/NumUnits").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DISTANCET_FIRESTATION").InnerText.ToString().Trim();

				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/NumGasOrSmokeAlarms").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/N0_LOCAL_ALARM").InnerText.ToString().Trim();

			

				/*
				if (QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/CONSTRUCTIONCREDIT").InnerText.ToString().Trim()=="Y")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/FinishedConstructionPct").InnerText = "50";
				else	
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/FinishedConstructionPct").InnerText = "100";
				*/
				
				//HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/FireProtectionClassCd").InnerText = QuoteDom.SelectSingleNode("DWELLINGDETAILS/PROTECTIONCLASS").InnerText.ToString().Trim();

				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/BldgProtection/FireProtectionClassCd").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/FIREPROTECTIONCLASS").InnerText.ToString().Trim();
				//Schedule Property Rest
				//HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/NumEmployeesFullTimeResidence").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/RESIDENCE_EMP_NUMBER").InnerText.ToString().Trim();
				
				/*
				if (QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/RESIDENCE_PREMISES").InnerText.ToString().Trim() != "0" || QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/OCCUPIED_INSURED").InnerText.ToString().Trim() != "0")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/AdditionalResidenceInd").InnerText = "Y";
				else
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/AdditionalResidenceInd").InnerText = "N";
				*/

				//Fire alarms
				

				//Dwelling under Construction

				if (QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DWELL_UND_CONSTRUCTION_DP1143").InnerText.ToString().Trim()=="Y")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/FinishedConstructionPct").InnerText = "0";
				else	
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Construction/FinishedConstructionPct").InnerText = "100";

				//Market Value///////////
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/DwellInspectionValuation/MarketValueAmt/Amt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DWELLING_LIMITS").InnerText;


				 
				//Coverages
				/*if (QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/EXPERIENCE").InnerText.ToString().Trim() == "0" && QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/EXPERIENCE").InnerText.ToString().Trim() == "")
					HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']").RemoveChild(HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'MATUR')]").ParentNode);
				
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'DWELL')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DWELLING_LIMITS").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'PL')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/PERSONALLIABILITY_LIMIT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'MEDPM')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/MEDICALPAYMENTSTOOTHERS_LIMIT").InnerText.ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'OS')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = (Double.Parse(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/HO48INCLUDE").InnerText.ToString().Trim()) + Double.Parse(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/HO48ADDITIONAL").InnerText.ToString().Trim())).ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'PL')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = (Double.Parse(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/PERSONALPROPERTYINCREASEDLIMITINCLUDE").InnerText.ToString().Trim()) + Double.Parse(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/PERSONALPROPERTYINCREASEDLIMITADDITIONAL").InnerText.ToString().Trim())).ToString().Trim();
				HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']/Coverage/CoverageCd[contains(.,'LOU')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/HO35ADDITIONAL").InnerText.ToString().Trim();
				*/
				
				
				
				XmlNode dwellNode = HOMELOB.SelectSingleNode("Dwell[@LocationRef='L01']");
				XmlNode quoteNode = null;
				//XmlNode acordNode = null;
				XmlNode sampleNode = dwellNode.SelectSingleNode("Coverage/CoverageCd[contains(.,'Sample')]").ParentNode;
				
						
				//Perill Deductible
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DEDUCTIBLE");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToUpper() != "" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "APDI"; 
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						//Code for all peril deductible changed from PERILL To APDI
						//clonedNode.SelectSingleNode("CoverageCd").InnerText = "APDI"; 
						//clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						dwellNode.AppendChild(clonedNode);
					}
				}
				//End of perill Deductible
				//Coverages*************************************************************************
				
				//Coverage A
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/DWELLING_LIMITS");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToUpper() != "" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "DWELL";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						dwellNode.AppendChild(clonedNode);
					}
				}
			
				//Coverage E - Personal Liability
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/PERSONALLIABILITY_LIMIT");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "CSL";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);
					}
					else if ( quoteNode.InnerText.Trim().ToUpper() == "NO COVERAGE" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "CSL";
						clonedNode.SelectSingleNode("Included/Text").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Coverage F – Medical Payments to Others
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/MEDICALPAYMENTSTOOTHERS_LIMIT");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "MEDPM";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);
					}
					else if ( quoteNode.InnerText.Trim().ToUpper() == "NO COVERAGE" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "MEDPM";
						clonedNode.SelectSingleNode("Included/Text").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Earthquake (DP-469)
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/EARTHQUAKEDP469");
				
				if ( quoteNode != null )
				{
					if ( quoteNode.InnerText.Trim().ToUpper() == "Y" )
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "EDP469";
						//clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = "10";
						clonedNode.SelectSingleNode("Deductible/Text").InnerText = "%- 250";

						dwellNode.AppendChild(clonedNode);
					}
				}
				
				//Incidental Office (By Insured)
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/INCIDENTALOFFICE");
				
				if ( quoteNode != null )
				{
					

					if ( quoteNode.InnerText.Trim().ToUpper() == "Y" )
					{
						XmlNode clonedNode = sampleNode.Clone();	
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "IOO";
						//clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						dwellNode.AppendChild(clonedNode);
					}
				}
				//Modified 2 jan 2008
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/INCIDENTALOFFICE_SMALL_OFFICE");
				
				if ( quoteNode != null )
				{
					

					if ( quoteNode.InnerText.Trim().ToUpper() == "Y" )
					{
						XmlNode clonedNode = sampleNode.Clone();	
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "PIOSS";
						//clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						dwellNode.AppendChild(clonedNode);
					}
				}
				//End Incidental Office

				
				//Mine Subsidence Coverage (DP-480)
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/MINESUBSIDENCEDP480");
				
				if ( quoteNode != null )
				{
					

					if ( quoteNode.InnerText.Trim().ToUpper() == "Y" )
					{
						XmlNode clonedNode = sampleNode.Clone();	
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "MSC480";
						//clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

						dwellNode.AppendChild(clonedNode);
					}
				}
				//Additonal amount Mine Subsidence (DP-480)
				//MINESUBSIDENCE_ADDITIONAL
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/MINESUBSIDENCE_ADDITIONAL");
				
				if ( quoteNode != null )
				{
					if(QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/MINESUBSIDENCEDP480").InnerText.ToUpper() == "Y")
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "MSC480";
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);
					}
				}


				//Coverage B – Other Structures
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APPURTENANTSTRUCTURES_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();

					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE"  )
					{
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "OSTR";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/APPURTENANTSTRUCTURES_ADDITIONAL");

					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}

					dwellNode.AppendChild(clonedNode);

				}
				//-----------------------------
				
				//Building Improvements, Alterations & Additions
				
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/BUILDINGIMPROVEMENTS_ADDITIONAL");
				if ( quoteNode != null )
				{
					if (quoteNode.InnerText.Trim().ToUpper() != "0")
					{
						XmlNode clonedNode = sampleNode.Clone();

				
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "BIAA";
						//
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					
					
						/*
						//quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/BUILDINGIMPROVEMENTS_ADDITIONAL");
						quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/BUILDINGIMPROVEMENTS_INCLUDE");
						if ( quoteNode != null )
						{
							if (quoteNode.InnerText.Trim().ToUpper() != "")
							{
								clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
							}
						}
						*/
						dwellNode.AppendChild(clonedNode);
					}
				}

				//Coverage D – Rental Value
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/RENTALVALUE_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();

					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "RV";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}

					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/RENTALVALUE_ADDITIONAL");

					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
					dwellNode.AppendChild(clonedNode);
				}

				//Coverage C – Landlords Personal Property
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/PERSONALPROPERTY_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();

					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "LPP";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/PERSONALPROPERTY_ADDITIONAL");

					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
					dwellNode.AppendChild(clonedNode);
				}

				//Contents in Storage
				/*quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/CONTENTSINSTORAGE_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
					
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "CS";
						clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					
					}*/

				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/CONTENTSINSTORAGE_ADDITIONAL");

				if ( quoteNode != null )
				{

					if (quoteNode.InnerText.Trim().ToUpper() != "0")
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "CS";
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);

					}

				}

				//Trees, Shrubs, Plants & Lawns
				/*//add by kranti
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TREESLAWNSSHRUBS_INCLUDE");				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();

					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "TSPL";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}					
					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TREESLAWNSSHRUBS_ADDITIONAL");
					if ( quoteNode != null )
					{
						double dblTemp = double.Parse(quoteNode.InnerText.Trim()==""?"0.0":quoteNode.InnerText.Trim());
						if (dblTemp>0.0)
						{
									
							//clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
							clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						}
						
					}
					dwellNode.AppendChild(clonedNode);
				}*/
				
				  
				 
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TREESLAWNSSHRUBS_ADDITIONAL");

				if ( quoteNode != null )
				{

					double dblTemp = double.Parse(quoteNode.InnerText.Trim()==""?"0.0":quoteNode.InnerText.Trim());
					if (dblTemp>0.0)
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "TSPL";
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);

					}

				}
				/*
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TREESLAWNSSHRUBS_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "TSPL";
						double dblTemp = double.Parse(quoteNode.InnerText.Trim()==""?"0.0":quoteNode.InnerText.Trim());
						if (dblTemp>0.0)
						{

							clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						}
					}

					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/TREESLAWNSSHRUBS_ADDITIONAL");

					if ( quoteNode != null )
					{
						double dblTemp = double.Parse(quoteNode.InnerText.Trim()==""?"0.0":quoteNode.InnerText.Trim());
						if (dblTemp>0.0)
						{
							clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						}
					}
					dwellNode.AppendChild(clonedNode);
				}

				*/

				//Radio & Television Equipment

				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/RADIOTV_ADDITIONAL");

				if ( quoteNode != null )
				{
					double dblTemp = double.Parse(quoteNode.InnerText.Trim()==""?"0.0":quoteNode.InnerText.Trim());
					if (dblTemp>0.0)
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "RTE";
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);

					}

				}

				/*quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/RADIOTV_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "RTE";
						clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						
					}

					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/RADIOTV_ADDITIONAL");

					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();

					}
					dwellNode.AppendChild(clonedNode);

				}*/

				//Satellite Dishes
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/SATELLITEDISHES_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "SD";
						clonedNode.SelectSingleNode("Included/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						
					}

					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/SATELLITEDISHES_ADDITIONAL");

					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
					dwellNode.AppendChild(clonedNode);

				}

				//Awnings, Canopies or Signs

				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/AWNINGSCANOPIES_ADDITIONAL");

				if ( quoteNode != null )
				{
					double dblTemp = double.Parse(quoteNode.InnerText.Trim()==""?"0.0":quoteNode.InnerText.Trim());
					if (dblTemp>0.0)				 
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "ACS";
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						dwellNode.AppendChild(clonedNode);

					}

				}

				/*quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/AWNINGSCANOPIES_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "ACS";
						clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
			
					}

					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/AWNINGSCANOPIES_ADDITIONAL");

					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
					dwellNode.AppendChild(clonedNode);

				}*/

				//Installation Floater – Building Materials (IF-184)
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/FLOATERBUILDINGMATERIALS_ADDITIONAL");

				if ( quoteNode != null )
				{
					double dblTemp = double.Parse(quoteNode.InnerText.Trim()==""?"0.0":quoteNode.InnerText.Trim());
					if (dblTemp>0.0)
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "IF184";
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = FLOATER_DED;
						dwellNode.AppendChild(clonedNode);

					}

				}

				/*quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/FLOATERBUILDINGMATERIALS_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						
					clonedNode.SelectSingleNode("CoverageCd").InnerText = "IF184";
					clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					
					}

					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/FLOATERBUILDINGMATERIALS_ADDITIONAL");

					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
					dwellNode.AppendChild(clonedNode);

				}*/

				//Installation Floater – Non-Structural Equipment (IF-184)
				quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/FLOATERNONSTRUCTURAL_ADDITIONAL");

				if ( quoteNode != null )
				{

					double dblTemp = double.Parse(quoteNode.InnerText.Trim()==""?"0.0":quoteNode.InnerText.Trim());
					if (dblTemp>0.0)
					{
						XmlNode clonedNode = sampleNode.Clone();
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "IFNSE";
						clonedNode.SelectSingleNode("Additional/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = FLOATER_DED;
						dwellNode.AppendChild(clonedNode);

					}

				}

				/*quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/FLOATERNONSTRUCTURAL_INCLUDE");
				
				if ( quoteNode != null )
				{
					XmlNode clonedNode = sampleNode.Clone();
					if ( quoteNode.InnerText.Trim().ToUpper() != "NO COVERAGE" )
					{
						
						clonedNode.SelectSingleNode("CoverageCd").InnerText = "IFNSE";
						clonedNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					
					}

					quoteNode = QuoteDom.SelectSingleNode("QUICKQUOTE/DWELLINGDETAILS/FLOATERNONSTRUCTURAL_ADDITIONAL");

					if ( quoteNode != null )
					{
						clonedNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = quoteNode.InnerText.Trim();
					}
					dwellNode.AppendChild(clonedNode);

				}*/

				//***********************************************************************************
				AcordXml=AcordDom.OuterXml;
			}
			return(AcordXml);
		}
	}
}
