using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Cms.Model;
using Cms.Model.Client;
using Cms.Model.Application.PriorLoss;

using Cms.Model.Application;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application.PrivatePassenger;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlApplication.HomeOwners;
using Cms.DataLayer;

namespace Cms.CmsWeb
{

	public class HomePath
	{
		public const string ApplicationNodes = "HomePolicyQuoteInqRq";
		public const string Dwelling = "HomeLineBusiness/Dwell";
		public const string PropertySchedule = "HomeLineBusiness/PropertySchedule";

	}

	public enum FarmingPremisesType
	{
		HUOPII=	11557,
		HUOPOI=	11558,
		HUOPRO=	11559	
	}

	

	/// <summary>
	/// This class is used to parse the ACORD XML for the Home LOB
	/// </summary>
	public class HomeLOBParser : AcordParserBase
	{
		public ArrayList alDwelling;

		public HomeLOBParser()
		{
		}
		
		
	
		/// <summary>
		/// Parses scheduled coverage items and returns an array list of model objects
		/// </summary>
		/// <param name="nodeApp"></param>
		/// <returns></returns>
		public ArrayList ParseScheduledItems(XmlNode nodeApp)
		{
			//XmlNodeList schNodes = nodeApp.SelectNodes("HomeLineBusiness/PropertySchedule");
			XmlNodeList schNodes = nodeApp.SelectNodes(HomePath.PropertySchedule);

			if ( schNodes == null ) return null;
			
			ArrayList alSchItems = new ArrayList();
				
				foreach(XmlNode schNode in schNodes)
				{
					if(/*schNode.SelectSingleNode("Deductible/Amt").InnerText!="0" || */schNode.SelectSingleNode("ItemValueAmt/Amt").InnerText!="0")//Deductible having 0 will not get save in DB
					{
						ClsSchItemsCovgInfo objInfo = new ClsSchItemsCovgInfo();
						dataNode = schNode.SelectSingleNode("PropertyClassCd");

						if ( dataNode != null)
						{
							objInfo.CATEGORY_CODE = dataNode.InnerText.Trim();
						}

						dataNode = schNode.SelectSingleNode("ItemValueAmt/Amt");

						if ( dataNode != null )
						{
							if ( !(dataNode.InnerText.ToString().Trim() == "0.00" || dataNode.InnerText.ToString().Trim() == "0"))
							{
								objInfo.AMOUNT_OF_INSURANCE = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
							}
						}
			
						dataNode = schNode.SelectSingleNode("Deductible/Amt");

						if ( dataNode != null )
						{
							if ( !(dataNode.InnerText.ToString().Trim() == "0.00" || dataNode.InnerText.ToString().Trim() == "0"))
							{
								objInfo.DEDUCTIBLE = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
							}
						}				

						alSchItems.Add(objInfo);	
					}
				
				}			
			
			return alSchItems;
		}

		public ArrayList ParseOtherStructureInfo(XmlNode nodeApp)
		{
			XmlNode othNode = nodeApp.SelectSingleNode("OtherStructure");
			int flag =0 ;

			if ( othNode == null ) return null;
			
			ArrayList alOtherStructures = new ArrayList();

			XmlNode dataNode=null;
			ClsOtherStructuresInfo  objInfo1 = new ClsOtherStructuresInfo();
			//HO489
			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO489']/OnPremises");
			if(dataNode != null)
			{
				if(dataNode.InnerText.ToString()!="")
				{
					objInfo1.PREMISES_LOCATION = dataNode.InnerText.Trim();
					flag = 1;
				}
			}

			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO489']/CoverageBasis");
			if(dataNode != null)
			{
				objInfo1.COVERAGE_BASIS  = dataNode.InnerText.Trim();
			}


			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO489']/InsValueOnPremises");
			if(dataNode != null)
			{
				string strInsVal = dataNode.InnerText.Trim();
				if(strInsVal != "")
				{
					objInfo1.INSURING_VALUE = Convert.ToDouble(strInsVal);
				//	objInfo.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED = Convert.ToDouble(strInsVal);
				//	objInfo.SATELLITE_EQUIPMENT = "2"; //NO in case Replacement
				}
			}
			//End HO489

			if(flag ==1)
			{
				alOtherStructures.Add(objInfo1);
				flag =0;
			}

			//HO48
			ClsOtherStructuresInfo  objInfo2 = new ClsOtherStructuresInfo();
			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO48']/OnPremises");
			if(dataNode != null)
			{
				if(dataNode.InnerText.ToString()!="")
				{
					objInfo2.PREMISES_LOCATION = dataNode.InnerText.Trim();
					flag = 1;
				}
			}

			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO48']/CoverageBasis");
			if(dataNode != null)
			{
				objInfo2.COVERAGE_BASIS  = dataNode.InnerText.Trim();
			}


			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO48']/InsValueOnPremises");
			if(dataNode != null)
			{
				string strInsVal = dataNode.InnerText.Trim();
				if(strInsVal != "")
				{
					objInfo2.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED = Convert.ToDouble(strInsVal);
					objInfo2.SATELLITE_EQUIPMENT = "10964"; //NO in case Replacement
				}
			}
 
			//END HO48

 
			if(flag ==1)
			{
				alOtherStructures.Add(objInfo2);
				flag =0;
			}

			//HO40

			ClsOtherStructuresInfo  objInfo3 = new ClsOtherStructuresInfo();
			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO40']/OnPremises");
			if(dataNode != null)
			{
				if(dataNode.InnerText.ToString()!="")
				{
					objInfo3.PREMISES_LOCATION = dataNode.InnerText.Trim();
					flag = 1;
				}
			}

			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO40']/CoverageAmt");
			if(dataNode != null)
			{
				string strInsVal = dataNode.InnerText.Trim();
				if(strInsVal != "")
				{
					objInfo3.COVERAGE_AMOUNT = Convert.ToDouble(strInsVal);
				}
			}

			if(flag ==1)
			{
				alOtherStructures.Add(objInfo3);
				flag =0;
			}

 			//End HO40
			//Start HO490

			ClsOtherStructuresInfo  objInfo4 = new ClsOtherStructuresInfo();
			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO490']/OffPremises");
			if(dataNode != null)
			{
				if(dataNode.InnerText.ToString()!="")
				{
					objInfo4.PREMISES_LOCATION = dataNode.InnerText.Trim();
					flag = 1;
				}
			}

			dataNode = othNode.SelectSingleNode("Coverage[@Code='HO490']/InsValueOffPremises");
			if(dataNode != null)
			{
				string strInsVal = dataNode.InnerText.Trim();
				if(strInsVal != "")
				{
					objInfo4.INSURING_VALUE_OFF_PREMISES = Convert.ToDouble(strInsVal);
				}
			}

			if(flag ==1)
			{
				alOtherStructures.Add(objInfo4);
				flag =0;
			}


			//End HO490

			
			ClsOtherStructuresInfo objNew = new ClsOtherStructuresInfo();
			dataNode = othNode.SelectSingleNode("OffPremises");
			if(dataNode != null)
			{
				if(dataNode.InnerText.Trim() != "")
				{
					objNew.PREMISES_LOCATION = dataNode.InnerText.Trim();
					flag =2;
				}
			}

			dataNode = othNode.SelectSingleNode("InsValueOffPremises");
			if(dataNode != null)
			{
				string strInsVal = dataNode.InnerText.Trim();
				if(strInsVal != "")
				{
					objNew.INSURING_VALUE_OFF_PREMISES = Convert.ToDouble(strInsVal);
				}
				
			}
			if(flag== 2)
			{
				alOtherStructures.Add(objNew);
			}
			
			
			//For Rental
			//for Satellite Dishes
			ClsOtherStructuresInfo objOthRental = new ClsOtherStructuresInfo();
			dataNode = othNode.SelectSingleNode("Coverage[@Code='SD']/OnPremises");			
			if(dataNode != null)
			{
				if(dataNode.InnerText.Trim().Trim() != "")
				{
					objOthRental.PREMISES_LOCATION = dataNode.InnerText.Trim();
					flag = 3;
				}
			}
			dataNode = othNode.SelectSingleNode("Coverage[@Code='SD']/CoverageBasis");
			if(dataNode != null)
			{
				objOthRental.COVERAGE_BASIS  =  dataNode.InnerText.Trim();
			}
			dataNode = othNode.SelectSingleNode("Coverage[@Code='SD']/InsValueOnPremises");
			if(dataNode != null)
			{
				string strInsVal = dataNode.InnerText.Trim();
				if(strInsVal != "")
				{
					objOthRental.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED  = Convert.ToDouble(strInsVal);
				}
			}
			dataNode = othNode.SelectSingleNode("Coverage[@Code='SD']/IsSatelliteEquip");
			if(dataNode != null)
			{
				objOthRental.SATELLITE_EQUIPMENT = "10963"; //Coverage set to yes
			}


			if(flag== 3)
			{
				alOtherStructures.Add(objOthRental);
			}

			//add By kranti for Other Struc. Coverage B			
			

			ClsOtherStructuresInfo objOthRental2 = new ClsOtherStructuresInfo();
			dataNode = othNode.SelectSingleNode("Coverage[@Code='OSTR']/OnPremises");			
			if(dataNode != null)
			{
				if(dataNode.InnerText.Trim().Trim() != "")
				{
					objOthRental2.PREMISES_LOCATION = dataNode.InnerText.Trim();
					flag = 4;
				}
			}
			dataNode = othNode.SelectSingleNode("Coverage[@Code='OSTR']/CoverageBasis");
			if(dataNode != null)
			{
				objOthRental2.COVERAGE_BASIS  =  dataNode.InnerText.Trim();
			}
			dataNode = othNode.SelectSingleNode("Coverage[@Code='OSTR']/InsValueOnPremises");
			if(dataNode != null)
			{
				string strInsVal = dataNode.InnerText.Trim();
				if(strInsVal != "")
				{
					objOthRental2.ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED  = Convert.ToDouble(strInsVal);
				}
			}
			dataNode = othNode.SelectSingleNode("Coverage[@Code='OSTR']/IsSatelliteEquip");
			if(dataNode != null)
			{
				objOthRental2.SATELLITE_EQUIPMENT = "10964"; //NO
			}

			if(flag== 4)
			{
				alOtherStructures.Add(objOthRental2);
			}

			return alOtherStructures;
		
		}
		/// <summary>
		/// Parses the gen info and returns the appropriate model object
		/// </summary>
		/// <param name="dwellNode"></param>
		/// <returns></returns>
		public clsGeneralInfo ParseGenInfo(XmlNode nodeApp,string strLOB)
		{
			XmlNodeList dwellNodes = nodeApp.SelectNodes("HomeLineBusiness/Dwell");
			
			if ( dwellNodes == null ) return null;
			
			clsGeneralInfo objGenInfo = new clsGeneralInfo();

			for(int i = 0; i < dwellNodes.Count; i++ )
			{
				if ( i == 0 )
				{
					XmlNode dwellNode = dwellNodes[i];
					
					
					dataNode = dwellNode.SelectSingleNode("AdditionalResidenceInd");

					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim() == "Y" )
						{
							objGenInfo.ANY_OTHER_RESI_OWNED = "1";			
						}
						else if ( dataNode.InnerText.Trim() == "N" )
						{
							objGenInfo.ANY_OTHER_RESI_OWNED = "0";			
						}
						
					}
					// Added for Any Business Conducted On Premises (under writing Question ) 
					dataNode = dwellNode.SelectSingleNode("AnyBusinessConductedOnPremises");

					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim() == "Y" )
						{
							objGenInfo.ANY_FARMING_BUSINESS_COND = "1";			
						}
						else if ( dataNode.InnerText.Trim() == "N" )
						{
							objGenInfo.ANY_FARMING_BUSINESS_COND = "0";			
						}
						
					}



					dataNode = dwellNode.SelectSingleNode("NumEmployeesFullTimeResidence");

					if ( dataNode != null )
					{
						if ( dataNode.InnerText != "" )
						{
							int num = Convert.ToInt32(dataNode.InnerText);

							if ( num > 0 )
							{
								objGenInfo.ANY_RESIDENCE_EMPLOYEE = "1";	
								objGenInfo.DESC_RESIDENCE_EMPLOYEE = num.ToString();
							}
							else
							{
								objGenInfo.ANY_RESIDENCE_EMPLOYEE = "0";	
							}
						}
					}
					#region LOSSES NON WEATHER AND WEATHER - (Itrack 6640) HOME
					//Added (Itrack 6640)
					if(strLOB == "HOME")
					{
						dataNode = dwellNode.SelectSingleNode("NoOfYearsInsured");
						if (dataNode != null)
						{
							if(dataNode.InnerText!="")
								objGenInfo.YEARS_INSU_WOL = Convert.ToInt32(dataNode.InnerText);
							else
								objGenInfo.YEARS_INSU_WOL = -1;
						}

						//Losses Non Weather and Weather
						
						dataNode = dwellNode.SelectSingleNode("NonWeatherLosses");
						if ( dataNode != null )
						{
							objGenInfo.NON_WEATHER_CLAIMS = Convert.ToInt32(dataNode.InnerText); ;
						}

						dataNode = dwellNode.SelectSingleNode("WeatherLosses");
						if ( dataNode != null )
						{
							objGenInfo.WEATHER_CLAIMS = Convert.ToInt32(dataNode.InnerText);
						}

						/*-	When converting from QQ to Application/Policy and the number is 
						 * 0 in both Total # of Non Weather Claims in the last 36 months.
						 *  and Total # of Weathered Claims in the last 36 months then 
						 * on the Home Underwriting Questions - Any Prior Losses *  Put a no  */

						if(objGenInfo.NON_WEATHER_CLAIMS == 0 && objGenInfo.WEATHER_CLAIMS == 0)
						{
                            objGenInfo.ANY_PRIOR_LOSSES = "0";
						}

						/*
						 * -When converting from QQ to Application/Policy and the number 
						 * is anything other than 0 then on the 
						 * Home Underwriting Questions - 
						 * Any Prior Losses*  Put a Yes */

						if(objGenInfo.NON_WEATHER_CLAIMS !=0)
						{
							objGenInfo.ANY_PRIOR_LOSSES = "1";
						}
						if(objGenInfo.WEATHER_CLAIMS !=0)
						{
							objGenInfo.ANY_PRIOR_LOSSES = "1";
						}

					}
					#endregion

				}
			}
			
			XmlNode policyNode = nodeApp.SelectSingleNode("PersPolicy");

			if ( policyNode != null )
			{
				dataNode = policyNode.SelectSingleNode("MultiPolicy");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText == "Y" )
					{
						objGenInfo.MULTI_POLICY_DISC_APPLIED = "1";
					}
					else
					{
						objGenInfo.MULTI_POLICY_DISC_APPLIED = "0";
					}
				}
				
				dataNode = policyNode.SelectSingleNode("NonsmokerCredit");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText == "Y" )
					{
						objGenInfo.NON_SMOKER_CREDIT = "1";
					}
					else
					{
						objGenInfo.NON_SMOKER_CREDIT = "0";
					}
				}
				
				dataNode = policyNode.SelectSingleNode("WoodstoveSurcharge");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText == "Y" )
					{
						objGenInfo.ANY_HEATING_SOURCE= "1";
					}
					else
					{
						objGenInfo.ANY_HEATING_SOURCE = "0";
					}
				}

				dataNode = policyNode.SelectSingleNode("NoOfDogs");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						//Modifeid 07 August 2007 : Set NO for ANIMALS_EXO_PETS_HISTORY irrespective of No of Dogs
						objGenInfo.ANIMALS_EXO_PETS_HISTORY = 0;	
						int num = Convert.ToInt32(dataNode.InnerText);

						if ( num > 0 )
						{
							//objGenInfo.ANIMALS_EXO_PETS_HISTORY = 1;	//Comented 07 August 2007
							objGenInfo.ANIMALS_EXO_PETS_HISTORY = 1;	//Added on  09 Dec 2009 :Itrack 6640
							objGenInfo.NO_OF_PETS = num;						
						}
						
					}

					
				}

				//Dog Breed :
				dataNode = policyNode.SelectSingleNode("DogBreed");
				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						objGenInfo.OTHER_DESCRIPTION = dataNode.InnerText;
					}
				}
			

				//Added for Loss free 
				string isLossFree="",isNotLossFree="";
				dataNode = policyNode.SelectSingleNode("LossFree");
				if ( dataNode != null )
				{
					if ( dataNode.InnerText.Trim().ToUpper() == "Y" )
					{
						objGenInfo.IS_LOSS_FREE_12_MONTHS= "1";
						isLossFree="1";
					}	
						
					else
					{
						objGenInfo.IS_LOSS_FREE_12_MONTHS= "0";
						isLossFree="1";
					}
							
				}
				//add by kranti

				//Added a LOB  REDW Check : Itrack 6640
				//Loss Free used in RENTAL
				//According to Itrack 6640 : ANY_PRIOR_LOSSES will be based on Wheather Losses : To Be handeled
				if(strLOB == "REDW")
				{
					dataNode = policyNode.SelectSingleNode("LossFree");
					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim().ToUpper() == "Y" )
						{
							objGenInfo.ANY_PRIOR_LOSSES= "0";						

						}						
					}
					dataNode = policyNode.SelectSingleNode("NotLossFree");
					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim().ToUpper() == "Y" )
						{
							objGenInfo.ANY_PRIOR_LOSSES= "1";						

						}	
					
					}

					dataNode = policyNode.SelectSingleNode("NotLossFreeSurcharge");
					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim().ToUpper() == "Y" )
						{
							objGenInfo.ANY_PRIOR_LOSSES= "1";						
						}
					}

					//Added for Not Loss free 
					dataNode = policyNode.SelectSingleNode("NotLossFree");

					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim().ToUpper() == "Y" )
						{
							objGenInfo.IS_LOSS_FREE_12_MONTHS= "0";
							isNotLossFree="1";

						}
					
					}
					//Passing -1 if both(Loss free and Bot Loss free) are not selected

					if (isNotLossFree=="" && isLossFree=="")
					{
						objGenInfo.IS_LOSS_FREE_12_MONTHS= "-1";
					}
				}

				//No of years insured with wolv :On the basis of LOSS FREE and NO LOSS FREE 
				
//				const int YEARS_INSU_WOL = 3;
//				if(policyNode.SelectSingleNode("LossFree")!=null && policyNode.SelectSingleNode("NotLossFree")!=null)
//				{
//					if(policyNode.SelectSingleNode("LossFree").InnerText.ToString() == "Y" 
//						||  policyNode.SelectSingleNode("NotLossFree").InnerText.ToString() == "Y")
//						objGenInfo.YEARS_INSU_WOL = YEARS_INSU_WOL;
//					else
//						objGenInfo.YEARS_INSU_WOL = 0;
//				}
				
				//add by kranti
				//No of years insured with wolv :On the basis of LOSS FREE and NO LOSS FREE only for home not for rental
				//so we must check this is home or rental

				//For Rental 
				if(strLOB == "REDW")
				{
					dataNode = policyNode.SelectSingleNode("LossFree");
					if ( dataNode != null )
					{
						if ( dataNode.InnerText.Trim().ToUpper() == "Y" )
						{
							objGenInfo.ANY_PRIOR_LOSSES= "0";						
						}					
						else
							objGenInfo.ANY_PRIOR_LOSSES= "1";						
					}
				
				}

				if (strLOB == "HOME")
				{
					//Commented for Itrack 6640
					/*
					const int YEARS_INSU_WOL = 3;
					if(policyNode.SelectSingleNode("LossFree")!=null && policyNode.SelectSingleNode("NotLossFree")!=null)
					{
						if(policyNode.SelectSingleNode("LossFree").InnerText.ToString() == "Y" 
							||  policyNode.SelectSingleNode("NotLossFree").InnerText.ToString() == "Y")
							objGenInfo.YEARS_INSU_WOL = YEARS_INSU_WOL;
						else
							objGenInfo.YEARS_INSU_WOL = 0;
					}*/
				}
				else
				{
					dataNode = policyNode.SelectSingleNode("NoOfYearsInsured");
					if ( dataNode != null )
					{
						objGenInfo.YEARS_INSU_WOL = Convert.ToInt32(dataNode.InnerText); ;
					}
				}

			



				//Added for Exp credit
			   dataNode = policyNode.SelectSingleNode("ExperienceCredit");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						try
						{
							objGenInfo.EXP_AGE_CREDIT = Convert.ToInt32(dataNode.InnerText);
						}
						catch(Exception ex)
						{
							throw new Exception("Unable to parse Experience Credit" + dataNode.InnerText + " " + ex.Message);
						}
					}

					
				}

				//Added For Farming Details
				dataNode = policyNode.SelectSingleNode("AnyFarming");
				if ( dataNode != null )
				{
					if ( dataNode.InnerText.ToUpper() == "Y" )
					{
						objGenInfo.Any_Forming = "1";
					}
					else 
					{
						objGenInfo.Any_Forming = "0";
					}

				}

				//NoOfLocation
				dataNode = policyNode.SelectSingleNode("NoOfLocation");
				if(dataNode != null)
				{
					if ( dataNode.InnerText.ToString().Trim() != "0.00" || dataNode.InnerText.ToString().Trim()!= "0" )
					{					
						objGenInfo.Location = dataNode.InnerText.ToString().Trim();
					}
				}



				dataNode = policyNode.SelectSingleNode("FarmingTypeCd");
				if ( dataNode != null )
				{
					string strFarmingTypeCd = dataNode.InnerText.Trim();
					if(strFarmingTypeCd == FarmingPremisesType.HUOPII.ToString()) 
					{
						objGenInfo.Premises = Convert.ToInt32(FarmingPremisesType.HUOPII);
					}
					else if(strFarmingTypeCd == FarmingPremisesType.HUOPOI.ToString())
					{
						objGenInfo.Premises= Convert.ToInt32(FarmingPremisesType.HUOPOI);
					}
					else if(strFarmingTypeCd == FarmingPremisesType.HUOPRO.ToString())
					{
						objGenInfo.Premises = Convert.ToInt32(FarmingPremisesType.HUOPRO);
					}
				}
				dataNode = policyNode.SelectSingleNode("AnyHorses");
				if ( dataNode != null )
				{
					if ( dataNode.InnerText.Trim().ToUpper() == "Y" )
					{
						objGenInfo.IsAny_Horse  = "1";
					}
					else
					{
						objGenInfo.IsAny_Horse  = "0";
					}
				}


				

				//Added for No of years insured with wolvorien(Renatl) : 9 feb 2006 
				dataNode = policyNode.SelectSingleNode("NoOfYearsInsured");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						try
						{
							objGenInfo.NO_OF_YEARS_INSURED = Convert.ToInt32(dataNode.InnerText);
						}
						catch(Exception ex)
						{
							throw new Exception("Unable to parse No Of YearsInsured" + dataNode.InnerText + " " + ex.Message);
						}
					}

					
				}



				//

			}

			return objGenInfo;

		}

		/// <summary>
		/// /// Parses the Prior Loss details and returns the relevant model object
		/// </summary>
		/// <param name="dwellNode"></param>
		/// <param name="nodeApp"></param>
		/// <returns></returns>
		public ClsPriorLossInfo_Home ParsePriorLossHome(XmlNode nodeApp)
		{
			XmlNodeList dwellNodes = nodeApp.SelectNodes(HomePath.Dwelling);
			ClsPriorLossInfo_Home objPriorLoss =  new ClsPriorLossInfo_Home();

			foreach(XmlNode dwellNode in dwellNodes)
			{
				dataNode = dwellNode.SelectSingleNode("PriorLossInfo");

				if (dataNode == null)
				{ 				
				}
						
				

				XmlNode nodeWaterPump = null;            
				nodeWaterPump = dataNode.SelectSingleNode("WaterPumpLoss");

				if ( nodeWaterPump != null )
				{
					try
					{
						if(nodeWaterPump.InnerText.Trim().ToUpper() == "YES")
							objPriorLoss.WATERBACKUP_SUMPPUMP_LOSS = 10963;
						else if (nodeWaterPump.InnerText.Trim().ToUpper() == "NO")
							objPriorLoss.WATERBACKUP_SUMPPUMP_LOSS = 10964;
						else if (nodeWaterPump.InnerText.Trim().ToUpper() == "-1") //Case when Limit of HO-327 is 0 and Loss is Blank
							objPriorLoss.WATERBACKUP_SUMPPUMP_LOSS = -1;

					}
					catch(Exception)
					{
						throw new Exception("Exception while parsing Prior Loss");
					}

				}
			}
			return objPriorLoss;
                       
		}

		
		
		/// <summary>
		/// Parses the Building Protection details and returns the relevant model object
		/// </summary>
		/// <param name="dwellNode"></param>
		/// <returns></returns>
		public ClsHomeRatingInfo ParseHomeRating(XmlNode dwellNode, XmlNode nodeApp)
		{
			//Parse Building protecttion info///////////////////////////
			currentNode = dwellNode.SelectSingleNode("BldgProtection");

			if ( currentNode == null )
			{
				//objProtectInfo = null;
				//return null;
			}
			
			ClsHomeRatingInfo objRating = new ClsHomeRatingInfo();
			
			//Protect devices info
			//objProtectInfo = new ClsProtectDevicesInfo();
			
		

			dataNode = currentNode.SelectSingleNode("DistanceToFireStation/NumUnits");

			if ( dataNode != null )
			{
				try
				{
					objRating.FIRE_STATION_DIST = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
				}
				catch(Exception)
				{
					throw new Exception("Exception while parsing Distance to Fire Station");
				}

			}

			dataNode = currentNode.SelectSingleNode("DistanceToHydrant/NumUnits");

			if ( dataNode != null )
			{
				try
				{
					objRating.HYDRANT_DIST = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
				}
				catch(InvalidCastException ex)
				{
					throw new Exception("Parse error:Hydrant distance has invalid values."); 
				}
				
			}
			
			dataNode = currentNode.SelectSingleNode("FireProtectionClassCd");

			if ( dataNode != null )
			{
				objRating.PROT_CLASS = dataNode.InnerText.Trim();

			}

			//Alarms 23 jan 2006
			dataNode = currentNode.SelectSingleNode("NumGasOrSmokeAlarms");

			if ( dataNode != null )
			{
				try
				{
					objRating.NUM_LOC_ALARMS_APPLIES = Convert.ToInt32(dataNode.InnerText.Trim()==""?"0":dataNode.InnerText.Trim());
				}
				catch(InvalidCastException ex)
				{
					throw new Exception("Parse error:No of Gas Alarms has invalid values."); 
				}

			}

			//end alarm
			
			//Burglary
			dataNode = currentNode.SelectSingleNode("BurgAlarmAlertPolice");

			if ( dataNode != null )
			{
				if ( dataNode.InnerText.Trim() == "Y" )
				{
					objRating.DIR_POLICE = "Y";
				}
			}
			
			dataNode = currentNode.SelectSingleNode("FireAlertFire");

			if ( dataNode != null )
			{
				if ( dataNode.InnerText.Trim() == "Y" )
				{
					objRating.DIR_FIRE = "Y";
				}
			}

			//Fire
			dataNode = currentNode.SelectSingleNode("CentStBurgAlarm");

			if ( dataNode != null )
			{
				if ( dataNode.InnerText.Trim() == "Y" )
				{
					objRating.CENT_ST_BURG = "Y";
				}

			}
			
			dataNode = currentNode.SelectSingleNode("CentStFireAlarm");

			if ( dataNode != null )
			{
				if ( dataNode.InnerText.Trim() == "Y" )
				{
					objRating.CENT_ST_FIRE = "Y";
				}

			}

			dataNode = currentNode.SelectSingleNode("CertAttached");

			if ( dataNode != null )
			{
				if ( dataNode.InnerText.Trim() == "Y" )
				{
					objRating.ALARM_CERT_ATTACHED = "10963";
				}
				else if ( dataNode.InnerText.Trim() == "N" )
				{
					objRating.ALARM_CERT_ATTACHED = "10964";
				}

			}

			dataNode = currentNode.SelectSingleNode("NumGasOrSmokeAlarms");

			if ( dataNode != null )
			{
				
					try
					{
						if ( dataNode.InnerText.Trim() != "" )
						{
							int num = Convert.ToInt32(dataNode.InnerText.Trim());

							if ( num == 1 )
							{
								objRating.LOC_FIRE_GAS = "Y";
							}
					
							if ( num >=2 )
							{
								//objRating.LOC_FIRE_GAS = "Y";
								objRating.TWO_MORE_FIRE = "Y";
							}
						}
					}

				
				catch(FormatException ex)
				{
					throw new Exception("Invalid value in NumGasOrSmokeAlarms");
				}


			}

			//Data from Construction
			XmlNode conNode = dwellNode.SelectSingleNode("Construction");
			
			dataNode = conNode.SelectSingleNode("FinishedConstructionPct");

			if ( dataNode != null )
			{
				int intPerc = DefaultValues.GetIntFromString(dataNode.InnerText.Trim());
				
				if ( intPerc >= 100 )
				{
					objRating.IS_UNDER_CONSTRUCTION = "0";
				}
				else
				{
					objRating.IS_UNDER_CONSTRUCTION = "1";
				}
			}
			//End of Bldg protection
			
			//Parse Home construction info//
			currentNode = dwellNode.SelectSingleNode("Construction");
			
			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("ConstructionCd");

				if ( dataNode != null )
				{
					if ( dataNode.InnerText != "" )
					{
						try
						{
							objRating.EXTERIOR_CONSTRUCTION = Convert.ToInt32(dataNode.InnerText);
						}
						catch(Exception ex)
						{
							throw new Exception("Unable to parse Exterior Construction" + dataNode.InnerText + " " + ex.Message);
						}
					}

					//objRating.EXTERIOR_CONSTRUCTION = dataNode.InnerText;
				}

				dataNode = currentNode.SelectSingleNode("FoundationCd");
				
				if ( dataNode != null )
				{
					objRating.FOUNDATION_CODE = dataNode.InnerText;
				}
	
				dataNode = currentNode.SelectSingleNode("RoofingMaterial/RoofMaterialCd");
				
				if ( dataNode != null )
				{
					objRating.ROOF_TYPE_CODE = dataNode.InnerText;
				}
				
				dataNode = currentNode.SelectSingleNode("FinishedConstructionPct");
				
				if ( dataNode != null )
				{
					objRating.ROOF_TYPE_CODE = dataNode.InnerText;
				}


			}

			dataNode = dwellNode.SelectSingleNode("WiringTypeCd");
				
			if ( dataNode != null )
			{
				objRating.WIRING_CODE = dataNode.InnerText;
			}


			dataNode = dwellNode.SelectSingleNode("WiringInspectedDt");
				
			if ( dataNode != null )
			{
				//objRating.WIRING_LAST_INSPECTED = DefaultValues.GetDateFromString(dataNode.InnerText);
			}
				
			currentNode = dwellNode.SelectSingleNode("DwellInspectionValuation");

			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("HeatSourcePrimaryCd");
				
				if ( dataNode != null )
				{
					objRating.PRIMARY_HEAT_TYPE_CODE = dataNode.InnerText;
				}
				
				dataNode = currentNode.SelectSingleNode("HeatSourceSupplementalCd");
				
				if ( dataNode != null )
				{
					objRating.SECONDARY_HEAT_TYPE_CODE = dataNode.InnerText;
				}

				dataNode = currentNode.SelectSingleNode("NumOfUnits");
				
				if ( dataNode != null )
				{
					
					objRating.NEED_OF_UNITS = dataNode.InnerText.ToString().Trim();
				}
				dataNode = currentNode.SelectSingleNode("NumFamilies");
				
				if ( dataNode != null )
				{
					if(DefaultValues.GetIntFromString(dataNode.InnerText)>0)
					{					
						objRating.NO_OF_FAMILIES = DefaultValues.GetIntFromString(dataNode.InnerText);
					}
				}

			}
			//////////////////////////////
			
			//Pers policy node
			dataNode = nodeApp.SelectSingleNode("PersPolicy/MultiPolicy");
			
			if ( dataNode != null )
			{
				if ( dataNode.InnerText == "Y")
				{
					objRating.IS_AUTO_POL_WITH_CARRIER = "1";
				}
				else
				{
					objRating.IS_AUTO_POL_WITH_CARRIER = "0";
				}

			}

			//Suburban discount
			XmlNode node = dwellNode.SelectSingleNode("SuburbanClassDiscount");
			if(node!=null)
			{
				dataNode = node.SelectSingleNode("SubUrbanDiscount");
				if(dataNode!=null)
				{
                    objRating.SUBURBAN_CLASS = dataNode.InnerText;
				}
				dataNode = node.SelectSingleNode("LocatedInSubDivision");
				if(dataNode!=null)
				{
					if(dataNode.InnerText.ToUpper() == "YES")
                        objRating.LOCATED_IN_SUBDIVISION = "10963";
					else if(dataNode.InnerText.ToUpper() == "NO")
						 objRating.LOCATED_IN_SUBDIVISION = "10964";

				}

			}

			return objRating;

		}


		/// <summary>
		/// Parses the Home construction detaails and returns teh relevant Model object
		/// </summary>
		/// <param name="dwellNode"></param>
		/// <returns></returns>
		public void ParseHomeConstruction(XmlNode dwellNode,ClsHomeRatingInfo objInfo)
		{
			//ClsHomeRatingInfo objInfo = new ClsHomeConstructionInfo();

			currentNode = dwellNode.SelectSingleNode("Construction");
			
			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("ConstructionCd");

				if ( dataNode != null )
				{
					objInfo.EXTERIOR_CONSTRUCTION_CODE = dataNode.InnerText;
				}

				dataNode = currentNode.SelectSingleNode("FoundationCd");
				
				if ( dataNode != null )
				{
					objInfo.FOUNDATION_CODE = dataNode.InnerText;
				}
	
				dataNode = currentNode.SelectSingleNode("RoofingMaterial/RoofMaterialCd");
				
				if ( dataNode != null )
				{
					objInfo.ROOF_TYPE_CODE = dataNode.InnerText;
				}
				
				dataNode = currentNode.SelectSingleNode("FinishedConstructionPct");
				
				if ( dataNode != null )
				{
					objInfo.ROOF_TYPE_CODE = dataNode.InnerText;
				}


			}

			dataNode = dwellNode.SelectSingleNode("WiringTypeCd");
				
			if ( dataNode != null )
			{
				objInfo.WIRING_CODE = dataNode.InnerText;
			}


			dataNode = dwellNode.SelectSingleNode("WiringInspectedDt");
				
			if ( dataNode != null )
			{
				//objInfo.WIRING_LAST_INSPECTED = DefaultValues.GetDateFromString(dataNode.InnerText);
			}
				
			currentNode = dwellNode.SelectSingleNode("DwellInspectionValuation");

			if ( currentNode != null )
			{
				dataNode = currentNode.SelectSingleNode("HeatSourcePrimaryCd");
				
				if ( dataNode != null )
				{
					objInfo.PRIMARY_HEAT_TYPE_CODE = dataNode.InnerText;
				}
				
				dataNode = currentNode.SelectSingleNode("HeatSourceSupplementalCd");
				
				if ( dataNode != null )
				{
					objInfo.SECONDARY_HEAT_TYPE_CODE = dataNode.InnerText;
				}

				dataNode = currentNode.SelectSingleNode("NumFamilies");
				
				if ( dataNode != null )
				{
					objInfo.NO_OF_FAMILIES = DefaultValues.GetIntFromString(dataNode.InnerText);
				}

			}
			
			//return objInfo;
			

		}

		
		/// <summary>
		/// Returns an Arraylist of sub locations for a location
		/// </summary>
		/// <param name="objNode"></param>
		/// <returns></returns>
		public new ArrayList ParseSublocations(XmlNode objNode)
		{
			ArrayList alSubloc = null;//new ArrayList();
			
			XmlNodeList subLocList = objNode.SelectNodes("SubLocation");
			
			if ( subLocList == null ) return null;

			foreach(XmlNode subLocNode in subLocList)
			{
				this.dataNode = subLocNode.SelectSingleNode("SubLocationDesc");
				
				ClsSubLocationInfo objInfo = new ClsSubLocationInfo();
				
				if ( dataNode != null )
				{
					objInfo.SUB_LOC_DESC = dataNode.InnerText.Trim();
				}
				
				if ( objNode.Attributes["id"] != null )
				{
					objInfo.ID = objNode.Attributes["id"].Value;
				}

				if ( alSubloc == null )
				{
					alSubloc = new ArrayList();
				}

				alSubloc.Add(objInfo);

			}
			
			return alSubloc;

		}
		

		/// <summary>
		/// Returns an ArrayList containing LocationInfo objects.
		/// </summary>
		/// <param name="nodeApp"></param>
		/// <returns></returns>
		public new ArrayList ParseLocations(XmlNode nodeApp,string strLOB)
		{
			//App Object for picking the APP_LOB
			ClsGeneralInfo objGinfo = new ClsGeneralInfo();
			
			XmlNodeList locList = nodeApp.SelectNodes("Location");
			
			if ( locList == null ) return null;

			ArrayList alLocations = new ArrayList();

			foreach(XmlNode locNode in locList)
			{
				ClsLocationInfo objLocInfo = new ClsLocationInfo();

				if ( locNode.Attributes["id"] != null )
				{
					objLocInfo.ID = locNode.Attributes["id"].Value;
				}

				Addr[] arAddr = this.ParseAddress(locNode,"Addr");

				if (  arAddr != null && arAddr.Length > 0)
				{
					foreach(Addr obj in arAddr)
					{
						if ( obj.AddrTypeCd == "StreetAddress")
						{
							objLocInfo.LOC_ADD1 = obj.Addr1;
							objLocInfo.LOC_ADD2 = obj.Addr2;
							objLocInfo.LOC_CITY = obj.City;
							objLocInfo.LOC_COUNTRY = "1";
							objLocInfo.LOC_ZIP = obj.PostalCode;
							objLocInfo.LOC_COUNTY = obj.County;
							objLocInfo.LOC_STATE = obj.StateProv;
						}

					}
				}

				dataNode = locNode.SelectSingleNode("LocationDesc");

				if ( dataNode != null )
				{
					objLocInfo.DESCRIPTION = dataNode.InnerText;
				}
				
				dataNode = locNode.SelectSingleNode("IsPrimary");

				if ( dataNode != null )
				{
					if(strLOB == "HOME")
						objLocInfo.LOCATION_TYPE = ClsCommon.GetLookupUniqueId("LOCTYP",dataNode.InnerText.Trim());
					else
						objLocInfo.LOCATION_TYPE = ClsCommon.GetLookupUniqueId("REN_LO",dataNode.InnerText.Trim());
				}

				//Added for Wolverine Insures Primary
				dataNode = locNode.SelectSingleNode("InsuresPrimary");

				if ( dataNode != null )
				{
					objLocInfo.IS_PRIMARY = dataNode.InnerText.Trim();	
				}
				//END InsuresPrimary
				
				dataNode = locNode.SelectSingleNode("Deductible");

				if ( dataNode != null )
				{				
					//objLocInfo.DEDUCTIBLE = dataNode.InnerText.Trim();
				}

				
				ArrayList alSubloc = null;
				if ( this.currentNode != null )
				{
					alSubloc = this.ParseSublocations(locNode);
				}
				
				if ( alSubloc != null )
				{
					objLocInfo.SetSublocations(alSubloc);
				}

				alLocations.Add(objLocInfo);
			}
			
			return alLocations;

		}
		
		
		/// <summary>
		/// Returns an Arraylist containing ClsDwellingInfo objects
		/// </summary>
		/// <param name="nodeApp"></param>
		/// <returns></returns>
		public ArrayList ParseDwelling(XmlNode nodeApp,ClsGeneralInfo objApplication)
		{
			//XmlNodeList dwellNodes = nodeApp.SelectNodes("HomeLineBusiness/Dwell");
			XmlNodeList dwellNodes = nodeApp.SelectNodes(HomePath.Dwelling);
			
			ArrayList alDwelling = new ArrayList();
			
			int i = 0;

			foreach(XmlNode dwellNode in dwellNodes)
			{
				ClsDwellingDetailsInfo objDwelling = new ClsDwellingDetailsInfo();
				
				if ( dwellNode.Attributes["LocationRef"] != null )
				{
					objDwelling.LOCATION_REF = dwellNode.Attributes["LocationRef"].Value;
				}

				if ( dwellNode.Attributes["SubLocationRef"] != null )
				{
					objDwelling.SUB_LOCATION_REF = dwellNode.Attributes["SubLocationRef"].Value;
				}

				dataNode = dwellNode.SelectSingleNode("PurchaseDt");

				if ( dataNode!= null )
				{
					DateTime dtPurchaseDate = DefaultValues.GetDateFromString(dataNode.InnerText.Trim());
					
					if ( dtPurchaseDate > DateTime.MinValue )
					{
						objDwelling.PURCHASE_YEAR = dtPurchaseDate.Year;
					}

				}

				dataNode = dwellNode.SelectSingleNode("Construction/YearBuilt");

				if ( dataNode!= null )
				{
					objDwelling.YEAR_BUILT = Convert.ToInt32(dataNode.InnerText.Trim());
				}

				dataNode = dwellNode.SelectSingleNode("PurchasePriceAmt/Amt");
				
				if ( dataNode != null )
				{
					if(dataNode.InnerText.Trim()!="")					
						objDwelling.PURCHASE_PRICE = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
					
				}
				
				dataNode = dwellNode.SelectSingleNode("TotalInsurableReplCostAmt/Amt");
				
				if ( dataNode != null )
				{
					objDwelling.REPLACEMENT_COST = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
				}

				dataNode = dwellNode.SelectSingleNode("DwellInspectionValuation/MarketValueAmt/Amt");
				
				if ( dataNode != null )
				{
					objDwelling.MARKET_VALUE = DefaultValues.GetDoubleFromString(dataNode.InnerText.Trim());
				}

				dataNode = dwellNode.SelectSingleNode("DwellOccupancy/OccupancyTypeCd");
				
				if ( dataNode != null )
				{
					objDwelling.OCCUPANCY_CODE = dataNode.InnerText.Trim();
				}
				
				//Policy type
				if ( i ==0 )
				{
					dataNode = dwellNode.SelectSingleNode("PolicyTypeCd");

					if ( dataNode != null )
					{
						objApplication.POLICY_TYPE_CODE = dataNode.InnerText.Trim();
					}
					
					

				}
				///

				//Parse Home rating details
				//ClsHomeConstructionInfo objInfo = this.ParseHomeConstruction(dwellNode);
				//objDwelling.SetHomeConstruction(objInfo);
				
				//ClsProtectDevicesInfo objProtectInfo = null;

				//Parse Building protection details
				ClsHomeRatingInfo objRatingInfo = this.ParseHomeRating(dwellNode,nodeApp);
				objDwelling.SetHomeRating(objRatingInfo);
			
				//Parse Home protection details
				//objDwelling.SetProtectDevices(objProtectInfo);
				
				ClsDwellingCoverageLimitInfo objCovg = new ClsDwellingCoverageLimitInfo();
			
				//Parse Covergae info
				ArrayList alCoverages = this.ParseSectionCoverages(dwellNode);
				objDwelling.SetCoverages(alCoverages);

				/*
				if ( alCoverages != null && alCoverages.Count > 0 )
				{
					for(int j =0; j < alCoverages.Count; j++ )
					{
						Cms.Model.Application.HomeOwners.ClsDwellingSectionCoveragesInfo objCovInfo = (ClsDwellingSectionCoveragesInfo)alCoverages[j];

						switch(objCovInfo.COVERAGE_CODE )
						{
							case "MATUR":
								//Experinced Homeowner
								objRatingInfo.EXPERIENCE_CREDIT = "1";
								break;

                            case "CSL":   //Added for Rental Personal Liablity (CSL) Coverage B
							case "PL":

								//Coverage E - Personal Liability (PL)
								objCovg.PERSONAL_LIAB_LIMIT = objCovInfo.LIMIT_1;
								//alCoverages.Remove(objCovInfo);
								
								break;
							case "DWELL":
								//Coverage A
								objCovg.DWELLING_LIMIT = objCovInfo.LIMIT_1;
								
								//Appurtenant Structures Limit B = 10%
								objCovg.OTHER_STRU_LIMIT = objCovg.DWELLING_LIMIT * 0.10;
								
								if ( objApplication.APP_LOB == "HOME" )
								{
									//HO-4 & HO-4 Deluxe 
									if (objApplication.POLICY_TYPE_CODE == "HO-4" || objApplication.POLICY_TYPE_CODE == "HO-4 Deluxe")
									{
//										//Coverage A
//										objCovg.DWELLING_LIMIT = objCovInfo.LIMIT_1;
//
//										//Personal Property Limit C 
//										objCovg.PERSONAL_PROP_LIMIT = objCovg.DWELLING_LIMIT;
//
//										//Loss Of Use D = 40% of Coverage C
//										double lossOfUse = objCovg.PERSONAL_PROP_LIMIT * 0.40;
//										objCovg.LOSS_OF_USE = lossOfUse;



										//Coverage A
										//objCovg.DWELLING_LIMIT = objCovInfo.LIMIT_1;

										//Personal Property Limit C 
										objCovg.PERSONAL_PROP_LIMIT = objCovInfo.LIMIT_1;

										//Loss Of Use D = 40% of Coverage C
										double lossOfUse = objCovg.PERSONAL_PROP_LIMIT * 0.40;
										objCovg.LOSS_OF_USE = lossOfUse;


									}
									//HO-6 and HO-6 deluxe
									else if (objApplication.POLICY_TYPE_CODE == "HO-6" || objApplication.POLICY_TYPE_CODE == "HO-6 Deluxe")
									{
										
										//Personal Property Limit C 
										objCovg.PERSONAL_PROP_LIMIT = objCovg.DWELLING_LIMIT;

										//Loss Of Use D = 40% of Coverage C
										double lossOfUse = objCovg.PERSONAL_PROP_LIMIT * 0.40;
										objCovg.LOSS_OF_USE = lossOfUse;

										//Coverage A 10% of Cov A..
										objCovg.DWELLING_LIMIT = objCovg.PERSONAL_PROP_LIMIT * 0.10;
										// Covg. A should default to 2,000 if covg. A is calculated and comes to be less than 2,000.
										if (objCovg.DWELLING_LIMIT < 2000)
										{
											objCovg.DWELLING_LIMIT=2000;
										}

									}
									else
									{
										
										//Personal Property Limit C = 70%
										objCovg.PERSONAL_PROP_LIMIT = objCovg.DWELLING_LIMIT * 0.70;

										//Loss Of Use D = 30%
										double lossOfUse = objCovg.DWELLING_LIMIT * 0.30;
										objCovg.LOSS_OF_USE = lossOfUse;
									}

									
								}
								
								if ( objApplication.APP_LOB == "REDW" )
								{
									//DP3 Michigan Covg.C should be 10% of Covg. A : 28 feb 2006
									if(objApplication.POLICY_TYPE_CODE == "DP-3 Premier")
									{
										objCovg.PERSONAL_PROP_LIMIT = objCovg.DWELLING_LIMIT * 0.10;
									}
									//End DP3 premium
									else
									{
										//Personal Property Limit C = 5%
										objCovg.PERSONAL_PROP_LIMIT = objCovg.DWELLING_LIMIT * 0.05;
									}
										//Loss Of Use D = 10%
										double lossOfUse = objCovg.DWELLING_LIMIT * 0.10;
										objCovg.LOSS_OF_USE = lossOfUse;
									
								}

								//alCoverages.Remove(objCovInfo);
								break;
							case "MEDPM":
								//Medical payment limit
								objCovg.MED_PAY_EACH_PERSON = objCovInfo.LIMIT_1;
								//alCoverages.Remove(objCovInfo);
								
								break;
							case "PERILL":
								//Perill Deductible limit
								objCovg.ALL_PERILL_DEDUCTIBLE_AMT = objCovInfo.LIMIT_1;
								//alCoverages.Remove(objCovInfo);

								break;
								//Added Covergae for Replacement Cost Contents : 9 feb 2006
							case "RCOST":
								objCovg.DWELLING_REPLACE_COST = objCovInfo.LIMIT_1.ToString();
								break;
								//
							
						}

					}

					
					//Remove the relevant coverages
					for(int j =0; j < alCoverages.Count; j++ )
					{
						Cms.Model.Application.HomeOwners.ClsDwellingSectionCoveragesInfo objCovInfo = (ClsDwellingSectionCoveragesInfo)alCoverages[j];

						if (objCovInfo.COVERAGE_CODE == "DWELL" || objCovInfo.COVERAGE_CODE == "MEDPM" || objCovInfo.COVERAGE_CODE == "CSL" || objCovInfo.COVERAGE_CODE == "PL" || objCovInfo.COVERAGE_CODE == "PERILL" || objCovInfo.COVERAGE_CODE == "RCOST")
						{
							alCoverages.Remove(objCovInfo);
						}
					}

				}*/
				
				objDwelling.SetCoverageLimit(objCovg);

				alDwelling.Add(objDwelling);

				i++;

			}

			return alDwelling;

		}


		public override AcordBase Parse()
		{
			//No of request in the XML
			XmlNodeList InsuranceSvcRq = GetApplicationNodeList();
			
			Home objApplication = null;

			foreach(XmlNode node in InsuranceSvcRq)
			{
				//XmlNodeList appNodeList = node.SelectNodes("HomePolicyQuoteInqRq");
				XmlNodeList appNodeList = node.SelectNodes(HomePath.ApplicationNodes);

				objApplication = new Home();
				

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

					//Get locations
					string appLOB = objApp.APP_LOB;
					ArrayList alLocations = this.ParseLocations(nodeApp,appLOB);
					objApplication.alLocations = alLocations;
					
					//No locations found
					if ( alLocations == null || alLocations.Count == 0 )
					{
						throw new Exception("No locations found in ACORD XML.");
					}

					//Get Dwelling Info
					ArrayList alDwelling = this.ParseDwelling(nodeApp,objApplication.objApplication);
					objApplication.alDwelling = alDwelling;
					
					//No locations found
					if ( alDwelling == null || alDwelling.Count == 0 )
					{
						throw new Exception("No Dwellings found in ACORD XML.");
					}

					//Parse Scheduled Items
					ArrayList alSchItems = this.ParseScheduledItems(nodeApp);
					objApplication.alSchItems = alSchItems;

					//Parse Gen Info
					clsGeneralInfo objGenInfo = this.ParseGenInfo(nodeApp,appLOB);
					objApplication.objGenInfo = objGenInfo;

					//Prior Loss Home
					if (objApp.APP_LOB == "HOME")
					{
						ClsPriorLossInfo_Home objPriorInfo = this.ParsePriorLossHome(nodeApp);
						objApplication.objPriorInfo = objPriorInfo;
					}
										
					//Parse Other Structure
					ArrayList alOtherStructures= this.ParseOtherStructureInfo(nodeApp);
					objApplication.alOtherStructures = alOtherStructures;

				}
			}

			return objApplication;
		}
	}


	/// <summary>
	/// This class is used to import all the data for the Home Line of business
	/// </summary>
	public class Home : AcordBase
	{
		public ClsApplicantDetailsInfo objApplicant;
		public ArrayList alLocations;		
		public ArrayList alDwelling;
		public ArrayList alSchItems;
		public clsGeneralInfo objGenInfo;
		public Cms.Model.Application.PriorLoss.ClsPriorLossInfo_Home objPriorInfo;
		public ArrayList alOtherStructures;
		
		public override ClsGeneralInfo Import()
		{
			objDataWrapper = new DataWrapper(ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				
			try
			{
//				int agencyID = ClsAgency.GetAgencyID(objAgency,objDataWrapper);
//				
//				if ( agencyID == -1 )
//				{
//					//System.Web.HttpContext.Current.Response.Write("Agency not found in the database.");
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

				SaveApplication();

				objDataWrapper.ClearParameteres();
				
				if ( this.objPriorPolicy != null)
				{
					SavePriorPolicy();
				
					objDataWrapper.ClearParameteres();
				}
				
				SaveLocations();
				
				objDataWrapper.ClearParameteres();

				SaveSublocations();

				objDataWrapper.ClearParameteres();
				
				SaveDwelling();

				objDataWrapper.ClearParameteres();
				
				SaveHomeRating();

				objDataWrapper.ClearParameteres();

				SavePriorLossHome();

				
				//objDataWrapper.ClearParameteres();

				//SaveHomeConstruction();

				//objDataWrapper.ClearParameteres();

				//this.SaveProtectDevices();
				
				//objDataWrapper.ClearParameteres();
				
				if ( this.objGenInfo != null )
				{
					this.SaveGenInfo();
				
					objDataWrapper.ClearParameteres();
				}

				if ( this.alSchItems != null && this.alSchItems.Count > 0 )
				{
					SaveSchItems();

					objDataWrapper.ClearParameteres();

				}
				if(this.alOtherStructures != null && this.alOtherStructures.Count > 0)
				{
					SaveOtherStructures();
					objDataWrapper.ClearParameteres();
				}
				
				//SaveCoverageLimits();

				objDataWrapper.ClearParameteres();

				this.SaveSectionCoverages();

			
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				
				System.Web.HttpContext.Current.Response.Write(ex.Message);

				if ( ex.InnerException != null )
				{
					System.Web.HttpContext.Current.Response.Write(ex.InnerException.Message);
				}
				
				string strMessage = ex.Message;

				if ( ex.InnerException != null )
				{
					strMessage = ex.InnerException.Message;
				}
				throw new Exception("Error in import: " + strMessage);
			}

			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			//System.Web.HttpContext.Current.Response.Write("Data imported successfully.");
			return(objApplication);

		}
		

		/// <summary>
		/// Saves the Dwelling section coveages
		/// </summary>
		/// <returns></returns>
		private int SaveSectionCoverages()
		{
			ClsHomeCoverages objBLL =null ;
			
			ClsGeneralInformation  objAppInfo = new ClsGeneralInformation();
			string strLOB_ID ="0";
			if(alDwelling.Count >0) 
			{
				ClsDwellingDetailsInfo objDwell= (ClsDwellingDetailsInfo)alDwelling[0];
				strLOB_ID = objAppInfo.Fun_GetLObID(objDwell.CUSTOMER_ID,objDwell.APP_ID,objDwell.APP_VERSION_ID);   

			}
			if(strLOB_ID=="1")
				objBLL= new ClsHomeCoverages();
			else if(strLOB_ID=="6")
				objBLL= new ClsHomeCoverages("RENTAL");
				
			

			for(int i = 0 ; i < this.alDwelling.Count; i++ )
			{
				ClsDwellingDetailsInfo objDwelling = (ClsDwellingDetailsInfo)alDwelling[i];
				
				ArrayList alCov = objDwelling.GetCoverages();

				for(int j = 0; j < alCov.Count; j++ )
				{
					//Cms.Model.Application.HomeOwners.ClsDwellingSectionCoveragesInfo objInfo = (ClsDwellingSectionCoveragesInfo)alCov[j];
					Cms.Model.Application.ClsCoveragesInfo  objInfo = (Cms.Model.Application.ClsCoveragesInfo)alCov[j];

				
					if ( objInfo == null ) continue;

					objInfo.APP_ID = objDwelling.APP_ID;
					objInfo.APP_VERSION_ID = objDwelling.APP_VERSION_ID;
					objInfo.CUSTOMER_ID = objDwelling.CUSTOMER_ID;
					objInfo.RISK_ID  = objDwelling.DWELLING_ID;
	
				}
				if (this.UserID != null || this.UserID != "")
				{
					objBLL.createdby = Convert.ToInt32(this.UserID.ToString());
				}
				else
				{
					objBLL.createdby = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());  
				}
				objBLL.SaveDefaultCoveragesApp(this.objDataWrapper,objDwelling.CUSTOMER_ID,
												objDwelling.APP_ID,objDwelling.APP_VERSION_ID,objDwelling.DWELLING_ID); 
				objBLL.SaveAcordHomeCoverages(alCov,"",this.objDataWrapper);
				objBLL.InvalidateInitialisation();
				objBLL.UpdateCoveragesByRuleApp(this.objDataWrapper, objDwelling.CUSTOMER_ID, objDwelling.APP_ID, 
					objDwelling.APP_VERSION_ID, RuleType.RiskDependent,objDwelling.DWELLING_ID );
				if(strLOB_ID=="1")
				{
					objBLL.UpdateCoveragesByRuleApp(this.objDataWrapper ,objDwelling.CUSTOMER_ID,objDwelling.APP_ID,objDwelling.APP_VERSION_ID,RuleType.OtherAppDependent);
				}
				objBLL.UpdateCoveragesByRuleApp(this.objDataWrapper ,objDwelling.CUSTOMER_ID,objDwelling.APP_ID,objDwelling.APP_VERSION_ID,RuleType.ProductDependend);
			}


			return 1;
		}

		//Save coverage limits
		private int SaveCoverageLimits()
		{
			ClsDwellingCoverageLimit objBLL = new ClsDwellingCoverageLimit();
			
			for(int i = 0 ; i < this.alDwelling.Count; i++ )
			{
				ClsDwellingDetailsInfo objDwelling = (ClsDwellingDetailsInfo)alDwelling[i];
				
				ClsDwellingCoverageLimitInfo objInfo = objDwelling.GetCoverageLimit();
				
				if ( objInfo == null ) continue;

				objInfo.APP_ID = objDwelling.APP_ID;
				objInfo.APP_VERSION_ID = objDwelling.APP_VERSION_ID;
				objInfo.CUSTOMER_ID = objDwelling.CUSTOMER_ID;
				objInfo.DWELLING_ID = objDwelling.DWELLING_ID;

				objBLL.Save(objInfo,this.objDataWrapper);
			}
			
			return 1;
		}
		
		
		/// <summary>
		/// Saves the locattions for this policy
		/// </summary>
		/// <returns></returns>
		public int SaveLocations()
		{
			ClsLocation objBLL = new ClsLocation();
			
			for(int i = 0; i < this.alLocations.Count; i++ )
			{
				ClsLocationInfo objInfo = (ClsLocationInfo)alLocations[i];

				objInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
				objInfo.APP_ID = this.objApplication.APP_ID;
				objInfo.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
				objInfo.LOC_STATE = this.objApplication.STATE_CODE;
				objInfo.LOC_COUNTRY = "1";
				if (this.UserID != null || this.UserID != "")
				{
					objInfo.CREATED_BY = int.Parse(this.UserID);
					objInfo.MODIFIED_BY = int.Parse(this.UserID);
				}
				else
				{
					objInfo.CREATED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());    
					objInfo.MODIFIED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());    
				}
				objBLL.SaveLocation(objInfo,this.objDataWrapper);

				objDataWrapper.ClearParameteres();

				
			}

			return 1;
		}
		
		
		/// <summary>
		/// Saves the sub locations for each location
		/// </summary>
		/// <returns></returns>
		public int SaveSublocations()
		{
			for(int i = 0; i < this.alLocations.Count; i++ )
			{
				ClsLocationInfo objInfo = (ClsLocationInfo)alLocations[i];

				ArrayList alSublocs = objInfo.GetSublocations();
				
				ClsSubLocation objBLL = new ClsSubLocation();

				if ( alSublocs != null && alSublocs.Count > 0 )
				{
					for(int j=0; j < alSublocs.Count; j++ )
					{
						ClsSubLocationInfo objSubLocInfo = (ClsSubLocationInfo)alSublocs[j];
						
						objSubLocInfo.APP_ID = objInfo.APP_ID;
						objSubLocInfo.CUSTOMER_ID = objInfo.CUSTOMER_ID;
						objSubLocInfo.APP_VERSION_ID = objInfo.APP_VERSION_ID;
						objSubLocInfo.LOCATION_ID = objInfo.LOCATION_ID;

						//Save Sublocation
						objBLL.Save(objSubLocInfo,this.objDataWrapper);
						
						objDataWrapper.ClearParameteres();
					}
				}
			}
			return 1;
		}

		
		/// <summary>
		/// Saves each Dwelling object in the instance ArrayList
		/// </summary>
		/// <returns></returns>
		public int SaveDwelling()
		{
			ClsDwellingDetails objBLL = new ClsDwellingDetails();
			
			for(int i = 0 ; i < this.alDwelling.Count; i++ )
			{
				ClsDwellingDetailsInfo objDwelling = (ClsDwellingDetailsInfo)alDwelling[i];
				
				objDwelling.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
				objDwelling.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
				objDwelling.APP_ID = this.objApplication.APP_ID;
				objDwelling.CREATED_BY = Convert.ToInt32(this.UserID);
				objDwelling.MODIFIED_BY = Convert.ToInt32(this.UserID);

				for(int j=0; j < this.alLocations.Count; j++ )
				{
					ClsLocationInfo objLoc = (ClsLocationInfo)alLocations[j];

					if ( objDwelling.LOCATION_REF == objLoc.ID )
					{
						objDwelling.LOCATION_ID = objLoc.LOCATION_ID;

						if ( objDwelling.SUB_LOCATION_REF != null && objDwelling.SUB_LOCATION_REF != "")
						{
							ArrayList alSubLoc = objLoc.GetSublocations();

							if ( alSubLoc != null && alSubLoc.Count > 0 )
							{
								for(int k = 0; k < alSubLoc.Count; k++ )
								{
									ClsSubLocationInfo objSubLoc = (ClsSubLocationInfo)alSubLoc[k];

									if ( objDwelling.SUB_LOCATION_REF == objSubLoc.ID )
									{
										objDwelling.SUB_LOC_ID = objSubLoc.SUB_LOC_ID;
									}

								}
							}
						}
						
						//Save dwelling details
						objBLL.Save(objDwelling,this.objDataWrapper);
						
						objDataWrapper.ClearParameteres();
					}

				}

			}

			return 1;

		}


		/// <summary>
		/// Saves Home Prior Loss Info
		/// </summary>
		/// <returns></returns>
		public int SavePriorLossHome()
		{
			if ( objApplication.APP_LOB == "HOME" )
			{
				ClsPriorLoss objPrior = new ClsPriorLoss();
				if(objPriorInfo.WATERBACKUP_SUMPPUMP_LOSS!=-1)
				{
					objPriorInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
					objPrior.SavePriorLossHome(objPriorInfo,this.objDataWrapper);
					objDataWrapper.ClearParameteres();
				}
			}
			return 1;

			
		}

		/// <summary>
		/// Saves the Home rating information
		/// </summary>
		/// <returns></returns>
		public int SaveHomeRating()
		{
			ClsHomeRating objBLL = new ClsHomeRating();


			for( int i = 0; i < this.alDwelling.Count; i++ )
			{
				ClsDwellingDetailsInfo objDwell = (ClsDwellingDetailsInfo)alDwelling[i];

				ClsHomeRatingInfo objRating = objDwell.GetHomeRating();

				if ( objRating == null )
				{
					continue;
				}
				
				objRating.CUSTOMER_ID = objDwell.CUSTOMER_ID;
				objRating.APP_ID = objDwell.APP_ID;
				objRating.APP_VERSION_ID = objDwell.APP_VERSION_ID;
				objRating.DWELLING_ID = objDwell.DWELLING_ID;

				//Circuit breakers dropdown at Rating Info screen should NOT HAVE default value 
				//if we make App thru QQ. Issue no 309 - MAke APP
				//EARLIAR IT WAS MADE 'YES' AS PER ISSUE NO 60 AND 93 OF MAKE APP
				//God knows what will happen next
			    
				//UN COMMENT FOR DEFAULT 'YES'
				//objRating.CIRCUIT_BREAKERS = "10963"; 

				//UN COMMENT FOR DEFAULT 'NO'
				//objRating.CIRCUIT_BREAKERS = "10964"; 

				//UN COMMENT FOR DEFAULT 'BLANK'
				objRating.CIRCUIT_BREAKERS = "10963"; 

				objBLL.Save(objRating,this.objDataWrapper);
				
				objDataWrapper.ClearParameteres();
			}

			return 1;

		}
		
//		/// <summary>
//		/// Saves the Protect devices information
//		/// </summary>
//		/// <returns></returns>
//		public int SaveProtectDevices()
//		{
//			ClsProtectDevices objBLL = new ClsProtectDevices();
//			
//			for( int i = 0; i < this.alDwelling.Count; i++ )
//			{
//				ClsDwellingDetailsInfo objDwell = (ClsDwellingDetailsInfo)alDwelling[i];
//
//				ClsProtectDevicesInfo objProtect = objDwell.GetProtectDevices();
//
//				if ( objProtect == null ) 
//				{
//					continue;
//				}
//				
//				objProtect.CUSTOMER_ID = objDwell.CUSTOMER_ID;
//				objProtect.APP_ID = objDwell.APP_ID;
//				objProtect.APP_VERSION_ID = objDwell.APP_VERSION_ID;
//				objProtect.DWELLING_ID = objDwell.DWELLING_ID;
//
//				objBLL.Save(objProtect,this.objDataWrapper);
//				
//				objDataWrapper.ClearParameteres();
//
//
//			}
//			
//			return 1;
//
//		}


		public int SaveOtherStructures()
		{
			if( alOtherStructures== null) return 0;
			if(alOtherStructures.Count <=0) return 0;
			ClsOtherStructures objOtherStructure = new ClsOtherStructures();
			int retVal=0;
			for(int i=0;i < alOtherStructures.Count;i++)
			{
				ClsOtherStructuresInfo objInfo = new ClsOtherStructuresInfo();
				objInfo =(ClsOtherStructuresInfo) alOtherStructures[i];
				objInfo.CUSTOMER_ID=objApplication.CUSTOMER_ID;
				objInfo.APP_ID=objApplication.APP_ID;
				objInfo.APP_VERSION_ID =1;
 				objInfo.CREATED_BY = Convert.ToInt32(this.UserID  );
				retVal= objOtherStructure.AddAcord(objInfo,this.objDataWrapper );
			}
			return retVal;
		}

		public int SaveGenInfo()
		{
			if ( this.objGenInfo == null ) return 1;

			ClsHomeGeneralInformation objBLL = new ClsHomeGeneralInformation();
			
			objGenInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
			objGenInfo.APP_ID = this.objApplication.APP_ID;
			objGenInfo.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;
			objGenInfo.IS_VACENT_OCCUPY = "0";
			objGenInfo.IS_RENTED_IN_PART = "0";
			objGenInfo.IS_DWELLING_OWNED_BY_OTHER = "0";
			//objGenInfo.ANY_FARMING_BUSINESS_COND = "0";
			objGenInfo.IS_PROP_NEXT_COMMERICAL = "0";
			objGenInfo.ARE_STAIRWAYS_PRESENT = "0";
			//objGenInfo.ANIMALS_EXO_PETS_HISTORY = 1;
			//objGenInfo.NO_OF_PETS = 0;
			objGenInfo.IS_SWIMPOLL_HOTTUB = "0";
			objGenInfo.HAS_INSU_TRANSFERED_AGENCY = "0";
			objGenInfo.IS_OWNERS_DWELLING_CHANGED = "0";
			objGenInfo.ANY_COV_DECLINED_CANCELED = "0";
			objGenInfo.CONVICTION_DEGREE_IN_PAST = "0";
			objGenInfo.LEAD_PAINT_HAZARD = "0";
			//objGenInfo.MULTI_POLICY_DISC_APPLIED = "0";
			
			
			//by kranti on 3rd may 07 
			//objGenInfo.ANY_OTHER_RESI_OWNED = "0";
			//objGenInfo.ANY_RESIDENCE_EMPLOYEE ="0";
			//end kranti

			objGenInfo.ANY_OTH_INSU_COMP = "0";
			objGenInfo.ANY_RENOVATION = "0";
			objGenInfo.TRAMPOLINE = "0";
			objGenInfo.RENTERS = "0";
			//objGenInfo.ANY_HEATING_SOURCE = "0";
			objGenInfo.BUILD_UNDER_CON_GEN_CONT = "0";
			objGenInfo.SWIMMING_POOL = "0";
			//objGenInfo.Any_Forming = "0";
			
			//RPSINGH -- 17 July 2006
			objGenInfo.PROPERTY_ON_MORE_THAN		= "0";
			objGenInfo.PROPERTY_USED_WHOLE_PART		= "0";
			objGenInfo.DWELLING_MOBILE_HOME			= "0";

			objGenInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE = "0";
			objGenInfo.MODULAR_MANUFACTURED_HOME		= "0";

			//objGenInfo.YEARS_INSU_WOL	=	 objGenInfo.NO_OF_YEARS_INSURED;
			
			objBLL.Save(objGenInfo,this.objDataWrapper);

			return 1;
			

		}


		public int SaveSchItems()
		{
			if ( this.alSchItems == null ) return 1;
			
			ClsSchItemsCovg objBLL = new ClsSchItemsCovg();
			//Leave first 14 Coverages
			for(int i = 14; i < alSchItems.Count; i++ )
			{
				ClsSchItemsCovgInfo objInfo = (ClsSchItemsCovgInfo)alSchItems[i];
				
				objInfo.CUSTOMER_ID = this.objApplication.CUSTOMER_ID;
				objInfo.APP_ID = this.objApplication.APP_ID;
				objInfo.APP_VERSION_ID = this.objApplication.APP_VERSION_ID;

				objBLL.Save(objInfo,this.objDataWrapper);

				objDataWrapper.ClearParameteres();
				
			}

			return 1;
		}

	}

}
