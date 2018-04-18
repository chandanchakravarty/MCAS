/******************************************************************************************
	<Author					: Deepak Gupta ->
	<Start Date				: July 29, 2005->
	<End Date				: - >
	<Description			: - This class will be used to fetch data related to autop quick quote>
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
using System.Collections;
namespace Cms.BusinessLayer.BlCommon
{
	public class ClsAuto: Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
									
		const int MEDPM2LimitAmt = 1000;
		const int MEDPM2DeductibleAmt = 50;
		const string MEDPM1LimitText = "Reject";
		const string REGULAR_400 = "400 REGULAR";
		const string REGULAR_500 = "500 REGULAR";
		const string UMPD_LIMIT_TEXT = "Reject";
		const string PUMSP_LIMIT_TEXT = "Reject";
		const string VT_CAMPER_TRAVEL_TRAILER = "CTT";
		const string VT_UTILITY_TRAILER = "TR";
		const string VT_CUSTOMIZED_VAN = "CV";
		const string VT_PRIVATE_PASSENGER = "PP";
		const string MINITORTLimit = "500";
		const string REJECT = "Reject";
		
		public ClsAuto()
		{
			//ClsCommon.ConnStr;
			//DataLayer.DataWrapper.ExecuteDataset(
		}
		//Class for CLASS Relativity
		public class  ClsDriver : IComparable
		{

			public int mDriverid;
			public int mDriverage;
			public int mRelativityDriverid;
			public double mRelativity;

			
			public ClsDriver()
			{
				mDriverid=0;
				mDriverage=0;
				mRelativityDriverid=0;
				mRelativity=0;
			
			}
			#region IComparable Members

			public int CompareTo(object obj)
			{
				// TODO:  Add ClsDriver.CompareTo implementation
				return 0;
			}

			#endregion
		}
		//private ArrayList arrValues; 
		
		public static string GetUniqueIdCommClass(string strClassCode)
		{	
			DataSet DS = DataLayer.DataWrapper.ExecuteDataset(
				ClsCommon.ConnStr,
				System.Data.CommandType.Text,
				"GETUNIQUEIDCOMMERICALCLASS");

			if (DS != null)
				return DS.Tables[0].Rows[0].ToString();
			else
				return "0";
		}

		public string GetVehicleMakeXml(string strVehicleYear)
		{
			DataSet ldsVehicleMake = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteVinMaster 'MAKE'," + strVehicleYear);
			string VehicleMakeXml="";
			foreach(DataRow Row in ldsVehicleMake.Tables[0].Rows)
			{
				VehicleMakeXml = VehicleMakeXml + Row[0].ToString();
			}
			VehicleMakeXml = VehicleMakeXml.Replace("<MNT_VIN_MASTER/>","");
			VehicleMakeXml = VehicleMakeXml.Replace("<MNT_VIN_MASTER ","<MAKE ");
			VehicleMakeXml = "<MAKES><MAKE MAKE_NAME=\"\"/>" + VehicleMakeXml + "</MAKES>";
			return(VehicleMakeXml);
		}
		
		public string GetVehicleModelXml(string strVehicleYear,string strVehicleMake)
		{
			DataSet ldsVehicleModel = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteVinMaster 'MODEL'," + strVehicleYear + ",'" + strVehicleMake + "'");
			string VehicleModelXml="";
			foreach(DataRow Row in ldsVehicleModel.Tables[0].Rows)
			{
				VehicleModelXml = VehicleModelXml + Row[0].ToString();
			}
			VehicleModelXml = VehicleModelXml.Replace("<MNT_VIN_MASTER/>","");
			VehicleModelXml = VehicleModelXml.Replace("<MNT_VIN_MASTER ","<MODEL ");
			VehicleModelXml = "<MODELS><MODEL MODEL_NAME=\"\"/>" + VehicleModelXml + "</MODELS>";
			return(VehicleModelXml);
		}

		public string GetVehicleVinSymbol(string strVehicleYear,string strVehicleMake,string strVehicleModel)
		{
			DataSet ldsVehicleVin = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetQuickQuoteVinMaster 'VIN'," + strVehicleYear + ",'" + strVehicleMake + "'" + ",'" + strVehicleModel + "'");
			string VehicleVinXml="";
			foreach(DataRow Row in ldsVehicleVin.Tables[0].Rows)
			{
				VehicleVinXml = VehicleVinXml + Row[0].ToString();
			}
			VehicleVinXml = VehicleVinXml.Replace("<MNT_VIN_MASTER/>","");
			VehicleVinXml = VehicleVinXml.Replace("<MNT_VIN_MASTER ","<VIN ");
			VehicleVinXml = "<VINNUMBERS><VIN VIN_NO=\"\" SYMBOL=\"\" BRAKES=\"\" AIRBAG=\"\"/>" + VehicleVinXml + "</VINNUMBERS>";
			return(VehicleVinXml);
		}

		//Get Symbol on the Basis of VIN (ref .Capital Rater)
		public string GetVehicleSymbol(string strVin)
		{
			DataSet dsVin= new DataSet();
			int intlnt=0;//,intfistvinlnt=0;
			string strfistvin="", strlastvin="";
			intlnt=strVin.Length;
			if(intlnt>2)
			{
				// Capital Rater Send '0' at position 9 in vin number have to replace it with & in our code
				
//				if(strVin.IndexOf("&")<=0)
//				{
//					if(strVin.LastIndexOf("0")>0)
//					{
//						strfistvin = strVin.Substring(0,8);//strVin.LastIndexOf("0"));
//						intfistvinlnt = strfistvin.Length;
//						strlastvin = strVin.Substring((strVin.LastIndexOf("0")+1),(intlnt-(intfistvinlnt+1)));
//						strVin=strfistvin+"&"+strlastvin;
//					}
//				}
				if(strVin.Length >=10)
				{
					strfistvin = strVin.Substring(0,8);
					strlastvin = strVin.Substring(9,1);
					strVin=strfistvin+"&"+strlastvin;
				}
				dsVin = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT TOP 1 CONVERT(INT,ISNULL(SYMBOL,1) ) AS SYMBOL FROM MNT_VIN_MASTER WHERE VIN=" + "'" + strVin.ToString().Trim() + "'");
			}
				if(dsVin.Tables[0].Rows.Count > 0)
				return dsVin.Tables[0].Rows[0]["SYMBOL"].ToString();
			else
				return "";
		}
		//Get Year,make,Model,symbol,Airbag,Break on the basis of VIN : 3 April 2006
		public DataSet FetchVINMasterDetailsFromVIN(string VIN)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@VIN",VIN,SqlDbType.VarChar );
				objDataWrapper.AddParameter("@CALLEDFROM","QQ",SqlDbType.VarChar );
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetVINDetailsByVIN");
			
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}

		//End : 3 April 2006
		
		public string GetMotorCycleVinXml(string effectiveDate)
		{
			//Removed Inline Query
			DataSet ldsMcycleMake = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetManufacturerQuickQuote"+ "'" + effectiveDate + "'" );
			//DataSet ldsMcycleMake = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT DISTINCT MANUFACTURER FROM MNT_VIN_MOTORCYCLE_MASTER ORDER BY 1");
			string MotorCycleVinXml="<MOTORCYCLE>";
			foreach(DataRow Row in ldsMcycleMake.Tables[0].Rows)
			{
				MotorCycleVinXml =  MotorCycleVinXml + "<MANUFACTURER MAKE=\"" + Row[0].ToString().Trim().Replace("&","&amp;") + "\"><MODEL MODEL_NAME=\"\" CLASS=\"A\"/>";

				string manufacturer = Row[0].ToString().Trim().Replace("'","''");
				//Removed Inline Query and Put Effective Date Check on MotorCycle MODELS : ITRACK 4276: 
				//DataSet ldsMcycleModel = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT MODEL MODEL_NAME,REPLACE(CLASS,'O','A') CLASS FROM MNT_VIN_MOTORCYCLE_MASTER WHERE MANUFACTURER='" + Row[0].ToString().Trim().Replace("'","''") + "' FOR XML AUTO");
				DataSet ldsMcycleModel = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_GetModelQuickQuote" + "'" + manufacturer + "'" + ",'" + effectiveDate + "'");

				foreach(DataRow ModelRow in ldsMcycleModel.Tables[0].Rows)
				{
					MotorCycleVinXml =  MotorCycleVinXml + ModelRow[0].ToString().Replace("<MNT_VIN_MOTORCYCLE_MASTER","<MODEL").Replace("<MNT_VIN_MOTORCYCLE_MASTER/>","");
				}
				MotorCycleVinXml =  MotorCycleVinXml + "</MANUFACTURER>";
			}
			MotorCycleVinXml =  MotorCycleVinXml + "</MOTORCYCLE>";
			return(MotorCycleVinXml);
		}

		public string GetCountyList(string strXmlString)
		{
			XmlDocument domQQ = new XmlDocument();
			domQQ.LoadXml(strXmlString);
			//DataSet ldsQQ = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT DISTINCT('<County>' + UPPER(MTC.COUNTY) + '</County>') FROM MNT_TERRITORY_CODES MTC INNER JOIN MNT_LOB_MASTER MLM ON MTC.LOBID=MLM.LOB_ID  AND MLM.LOB_CODE='BOAT' INNER JOIN MNT_COUNTRY_STATE_LIST MCS ON MTC.STATE=MCS.STATE_ID WHERE COUNTY IS NOT NULL AND STATE_NAME  = 'INDIANA'");
			DataSet ldsQQ = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT DISTINCT('<County>' + REPLACE(REPLACE(REPLACE(LOOKUP_VALUE_DESC,'IN Terr II-',''),'IN Terr I-',''),'&','&amp;') + '</County>') FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID IN (SELECT LOOKUP_ID FROM MNT_LOOKUP_TABLES WHERE LOOKUP_NAME ='TERR') AND LOOKUP_VALUE_DESC NOT LIKE '%ALL OTHER%' AND LEFT(LOOKUP_VALUE_DESC,2) = 'IN'");
			foreach (DataRow Row in ldsQQ.Tables[0].Rows)
			{
				domQQ.SelectSingleNode("quickQuote/boat/ContyOfOperation[@state='INDIANA']").InnerXml = domQQ.SelectSingleNode("quickQuote/boat/ContyOfOperation[@state='INDIANA']").InnerXml.ToString().Trim() +  Row[0].ToString().Trim();
			}
			//ldsQQ = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT DISTINCT('<County>' + UPPER(MTC.COUNTY) + '</County>') FROM MNT_TERRITORY_CODES MTC INNER JOIN MNT_LOB_MASTER MLM ON MTC.LOBID=MLM.LOB_ID  AND MLM.LOB_CODE='BOAT' INNER JOIN MNT_COUNTRY_STATE_LIST MCS ON MTC.STATE=MCS.STATE_ID WHERE COUNTY IS NOT NULL AND STATE_NAME  = 'MICHIGAN'");
			ldsQQ = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT DISTINCT('<County>' + REPLACE(REPLACE(REPLACE(LOOKUP_VALUE_DESC,'MI Terr II-',''),'MI Terr I-',''),'&','&amp;') + '</County>') FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID IN (SELECT LOOKUP_ID FROM MNT_LOOKUP_TABLES WHERE LOOKUP_NAME ='TERR') AND LOOKUP_VALUE_DESC NOT LIKE '%ALL OTHER%' AND LEFT(LOOKUP_VALUE_DESC,2) = 'MI'");
			foreach (DataRow Row in ldsQQ.Tables[0].Rows)
			{
				domQQ.SelectSingleNode("quickQuote/boat/ContyOfOperation[@state='MICHIGAN']").InnerXml = domQQ.SelectSingleNode("quickQuote/boat/ContyOfOperation[@state='MICHIGAN']").InnerXml.ToString().Trim() +  Row[0].ToString().Trim();
			}
			//ldsQQ = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT DISTINCT('<County>' + UPPER(MTC.COUNTY) + '</County>') FROM MNT_TERRITORY_CODES MTC INNER JOIN MNT_LOB_MASTER MLM ON MTC.LOBID=MLM.LOB_ID  AND MLM.LOB_CODE='BOAT' INNER JOIN MNT_COUNTRY_STATE_LIST MCS ON MTC.STATE=MCS.STATE_ID WHERE COUNTY IS NOT NULL AND STATE_NAME  = 'WISCONSIN'");
			ldsQQ = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT DISTINCT('<County>' + REPLACE(REPLACE(REPLACE(LOOKUP_VALUE_DESC,'WI Terr II-',''),'WI Terr I-',''),'&','&amp;') + '</County>') FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID IN (SELECT LOOKUP_ID FROM MNT_LOOKUP_TABLES WHERE LOOKUP_NAME ='TERR') AND LOOKUP_VALUE_DESC NOT LIKE '%ALL OTHER%' AND LEFT(LOOKUP_VALUE_DESC,2) = 'WI'");
			foreach (DataRow Row in ldsQQ.Tables[0].Rows)
			{
				domQQ.SelectSingleNode("quickQuote/boat/ContyOfOperation[@state='WISCONSIN']").InnerXml = domQQ.SelectSingleNode("quickQuote/boat/ContyOfOperation[@state='WISCONSIN']").InnerXml.ToString().Trim() +  Row[0].ToString().Trim();
			}
			return(domQQ.OuterXml.ToString().Trim());
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

		public string GetUserDefaultXml(string UserID,string LOBCode,string StateName)
		{
			string	strStoredProc =	"Proc_GetQuickQuoteUserDefaultXml";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@USER_ID",UserID);
			objWrapper.AddParameter("@LOB",LOBCode);
			objWrapper.AddParameter("@STATE",StateName);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			if (ds.Tables[0].Rows.Count > 0)
				return(ds.Tables[0].Rows[0][0].ToString());
			else
				return("");
			ds.Dispose();
		}

		public string UpdateZipInsuranceScoreIntoXml(string strQQHomeXml, string CustomerId, string Xpath)
		{
			XmlDocument domQQAuto = new XmlDocument();
			domQQAuto.LoadXml(strQQHomeXml);
				
			DataSet ldsQQHome = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1)) WHEN -1 THEN '100' WHEN 0 THEN '100' WHEN -2 THEN 'NOHITNOSCORE' ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) end  CUSTOMER_INSURANCE_SCORE,CUSTOMER_ZIP FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=" + CustomerId);
			//XmlNode Node = domQQAuto.SelectSingleNode("DWELLINGDETAILS");
			//if (domQQAuto.SelectSingleNode(Xpath + "/zipCode")!=null)
			//	domQQAuto.SelectSingleNode(Xpath + "/zipCode").InnerText = ldsQQHome.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim();
			if (domQQAuto.SelectSingleNode(Xpath + "/InsuranceScore")!=null)
				domQQAuto.SelectSingleNode(Xpath + "/InsuranceScore").InnerText = ldsQQHome.Tables[0].Rows[0]["CUSTOMER_INSURANCE_SCORE"].ToString().Trim();
			if (domQQAuto.SelectSingleNode(Xpath + "/QUOTEEFFDATE")!=null)
				domQQAuto.SelectSingleNode(Xpath + "/QUOTEEFFDATE").InnerText = System.DateTime.Now.Month.ToString() + "/" + System.DateTime.Now.Day.ToString() + "/" + System.DateTime.Now.Year.ToString();
			if (domQQAuto.SelectSingleNode(Xpath + "/INSURANCESCORE")!=null)
				domQQAuto.SelectSingleNode(Xpath + "/INSURANCESCORE").InnerText = ldsQQHome.Tables[0].Rows[0]["CUSTOMER_INSURANCE_SCORE"].ToString().Trim();
			if (domQQAuto.SelectSingleNode(Xpath + "/QUOTEEFFDATE")!=null)
				domQQAuto.SelectSingleNode(Xpath + "/QUOTEEFFDATE").InnerText = System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Year.ToString();
			
			//foreach (XmlNode Node in domQQAuto.SelectNodes("quickQuote/vehicles/*"))
			//{
			//	Node.SelectSingleNode("ZipCodeGaragedLocation").InnerText = ldsQQHome.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim();
			//}

			//			if (domQQAuto.SelectSingleNode(Xpath + "/TERRITORY")!=null)
			//			{
			//				DataSet DS = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT TERR FROM MNT_TERRITORY_CODES WHERE LOBID=2 AND ZIP='" + ldsQQHome.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim() + "'");
			//				if (DS.Tables[0].Rows.Count > 0)
			//					domQQAuto.SelectSingleNode(Xpath + "/TERRITORY").InnerText = DS.Tables[0].Rows[0][0].ToString().Trim();
			//				else
			//					domQQAuto.SelectSingleNode(Xpath + "/TERRITORY").InnerText = "";
			//			}

			return(domQQAuto.OuterXml.ToString().Trim());
		}

		//Function to fetch the customers INDURANCE score: 8 feb 2006
		public string UpdateInsuranceScoreIntoXml(string strQQHomeXml, string CustomerId, string Xpath)
		{
			XmlDocument domInsQQAuto = new XmlDocument();
			domInsQQAuto.LoadXml(strQQHomeXml);
				
			DataSet ldsQQHome = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1)) WHEN -1 THEN '100' WHEN -2 THEN 'NOHITNOSCORE' ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) end   CUSTOMER_INSURANCE_SCORE,CUSTOMER_ZIP FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=" + CustomerId);
		
			if (domInsQQAuto.SelectSingleNode(Xpath + "/InsuranceScore")!=null)
				domInsQQAuto.SelectSingleNode(Xpath + "/InsuranceScore").InnerText = ldsQQHome.Tables[0].Rows[0]["CUSTOMER_INSURANCE_SCORE"].ToString().Trim();
			
			if (domInsQQAuto.SelectSingleNode(Xpath + "/INSURANCESCORE")!=null)
				domInsQQAuto.SelectSingleNode(Xpath + "/INSURANCESCORE").InnerText = ldsQQHome.Tables[0].Rows[0]["CUSTOMER_INSURANCE_SCORE"].ToString().Trim();
				
		
			return(domInsQQAuto.OuterXml.ToString().Trim());
		}
		//End Insurance Score
		//Calculate symbol 23 feb 2006: Praveen K
		public string GetVehicleUniqueID(string LookupCode,string LookupVehicleUse)
		{
			DataSet ldsQuickQuoteInfoSymbol = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"Proc_getVehicle_Unique_ID '" + LookupVehicleUse + "','" + LookupCode + "'");
			if (ldsQuickQuoteInfoSymbol.Tables[0].Rows.Count > 0)
				return(ldsQuickQuoteInfoSymbol.Tables[0].Rows[0][0].ToString().Trim());
			else
				return("");
		}

		//end symbol
		#region VEHICLE CLASS CALCULATION :  1 MAY 2006
		public string GetVehicleClass(string vehicleInputXml,string LobCode)
		{
			string strClass = "";
			XmlDocument ClassDoc = new XmlDocument();
			vehicleInputXml=vehicleInputXml.Replace("&AMP;","&amp;");
			vehicleInputXml=vehicleInputXml.Replace("&GT;","&gt;");
			vehicleInputXml=vehicleInputXml.Replace("&LT;","&lt;");
			ClassDoc.LoadXml(vehicleInputXml);
			XmlNode ClassNode = ClassDoc.SelectSingleNode("CLASS");
			XmlNode VehicleTypeNode = ClassDoc.SelectSingleNode("CLASS/DRIVERINFO/DRIVER/VEHICLETYPE");
			if(VehicleTypeNode !=null && VehicleTypeNode.InnerText!="" && (VehicleTypeNode.InnerText==VT_CAMPER_TRAVEL_TRAILER || VehicleTypeNode.InnerText==VT_UTILITY_TRAILER))
			{
				strClass = "<CLASS>" + "<VEHICLECLASS>" +"</VEHICLECLASS>" + "</CLASS>";
				return strClass;
			}
			else
			{
				//Effective Date
				DateTime dtEffdate;
				dtEffdate = DateTime.Parse(ClassNode.SelectSingleNode("QUOTEEFFDATE").InnerText.ToString());
			
				//Load Masterdata XML
				XmlDocument DataDoc = new XmlDocument();
				//DataDoc.Load(System.Web.HttpContext.Current.Server.MapPath("../QuickQuote/VEHICLE_CLASS.XML"));
				string  strXmlFilePath;
				if(IsEODProcess)
				{
					strXmlFilePath =WebAppUNCPath + @"\cmsweb\" + VehicleClassXml ;
				}
				else
				{
					strXmlFilePath = ClsCommon.GetKeyValueWithIP("VEHICLE_CLASS");
				}
				DataDoc.Load(strXmlFilePath);
				//XmlNode DataNode = DataDoc.SelectSingleNode("VEHICLECLASS");
				/*Date Check in Vehicle class
				 * get the XML data according to the date*/
				XmlDocument classDateDoc = new XmlDocument();
				string strClassXml = "";
				foreach(XmlNode Node in DataDoc.SelectNodes("VEHICLECLASS/*"))
				{
					if(Node.Attributes["CODE"].Value.ToString() == "AUTOP")
					{
						if (dtEffdate >= DateTime.Parse(Node.Attributes["START"].Value.ToString()) && dtEffdate <= DateTime.Parse(Node.Attributes["END"].Value.ToString()))
							strClassXml = "<VEHICLECLASS>" +  Node.OuterXml + "</VEHICLECLASS>";
					}
				}
			
				classDateDoc.LoadXml(strClassXml);
				XmlNode DataNode = classDateDoc.SelectSingleNode("VEHICLECLASS");
				/*Date Check in Vehicle class*/
			
				string strState;string strRenewal;string strNewBussiness;
				string yearsContInsuredWithWolverine = "0";
				strState = ClassNode.SelectSingleNode("STATENAME").InnerText.ToString();
				strRenewal = ClassNode.SelectSingleNode("RENEWAL").InnerText.ToString();
				strNewBussiness = ClassNode.SelectSingleNode("NEWBUSINESS").InnerText.ToString();
			
				yearsContInsuredWithWolverine = ClassNode.SelectSingleNode("YEARSCONTINSUREDWITHWOLVERINE").InnerText.ToString();
				string vehicleClass = "";
				//string driverMvrPoints = "";
				int driverMvrPoints = 0;
				
				// ---No driver nodes when no operator is assigned --so assign PA
			
				foreach(XmlNode DriverNode in ClassNode.SelectNodes("DRIVERINFO/*"))
				{
					int driverAge=0;
					if (DriverNode.SelectSingleNode("BIRTHDATE").InnerText.ToString().Trim() != "")
					{
						/*Sumit Chhabra(04/30/2007):
						 * Age of driver calculated here is not coming correct. Date comparison is done only for year
						 * part. This results in certain incorrect values (overflow) of age.
						 * Age is being calculated at database level in sql and is coming in the node itself.
						 * If node does not come, then we can calculate it here*/
						if(DriverNode.SelectSingleNode("AGEOFDRIVER")==null || DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText=="")
						{
							TimeSpan TS =  System.DateTime.Now-DateTime.Parse(DriverNode.SelectSingleNode("BIRTHDATE").InnerText.ToString());
							DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText = (TS.Days/365).ToString().Trim();
							driverAge=(TS.Days/365);
						}
						else
							driverAge=int.Parse(DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText);

										
						if(DriverNode.SelectSingleNode("SUMOFVIOLATIONPOINTS")!=null &&
							DriverNode.SelectSingleNode("SUMOFACCIDENTPOINTS")!=null )
						{
							driverMvrPoints = int.Parse(DriverNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString())
								+
								int.Parse(DriverNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());

						}

						if(driverAge >= 70 && driverMvrPoints < 5 && int.Parse(yearsContInsuredWithWolverine) >=3)
						{
							strRenewal = "TRUE";
						}

						string strHaveCar="";
						if(DriverNode.SelectSingleNode("HAVE_CAR") != null)
							strHaveCar = DriverNode.SelectSingleNode("HAVE_CAR").InnerText.ToString();

					
				
						//Changes have been been made the drive field of the driver...New lookup values have been
						//added and existing values being updated. Changes have been made at app and pol level
						//but necessary changes at quick quote and web-service will be made later. Thus, for the time
						//being, value of operator has been fixed to prevent any errors at pol or app level.																										   
					
						string strOperator = DriverNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText.ToUpper().Trim().ToString();
						string strCollegeStudent = DriverNode.SelectSingleNode("COLLEGESTUDENT").InnerText.ToString();
						//string strHaveCar = DriverNode.SelectSingleNode("HAVE_CAR").InnerText.ToString();

					
						//Check if there is no Assigned Driver : 
						if (strOperator!="0" && strOperator!= "NR")  //NR NOT RATED
						{
							//Fetch the MAX LIMIT Age because the xquery will use renewal also for Age >= MaxAgeLimit:
							string strMaxLimit = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/@MAXAGELIMIT").InnerText.ToString();
							strMaxLimit = strMaxLimit ==""?"0":strMaxLimit;
							// in case age >= maxage limit
							if ( driverAge >= int.Parse (strMaxLimit))
							{
								if(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='ANY' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge  + " <=@MAXAGE and @MINMVRPOINTS <= " + driverMvrPoints + " and @MAXMVRPOINTS >= " + driverMvrPoints + " and @RENEWAL ='"+strRenewal.ToUpper().Trim()+"']/@CLASS")!=null)
									vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='ANY' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge  + " <=@MAXAGE and @MINMVRPOINTS <= " + driverMvrPoints + " and @MAXMVRPOINTS >= " + driverMvrPoints + " and @RENEWAL ='"+strRenewal.ToUpper().Trim()+"']/@CLASS").InnerText.ToString();
							}
							else if (driverAge >=25) // AGE greater than 25
							{
								if(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='ANY' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge +" >=@MINAGE and  " + driverAge  + " <=@MAXAGE and @MINMVRPOINTS <= " + driverMvrPoints + " and @MAXMVRPOINTS >= " + driverMvrPoints + "]/@CLASS")!=null)
									vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='ANY' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge +" >=@MINAGE and  " + driverAge  + " <=@MAXAGE and @MINMVRPOINTS <= " + driverMvrPoints + " and @MAXMVRPOINTS >= " + driverMvrPoints + "]/@CLASS").InnerText.ToString();
							}
							else //AGE less than 25 will have two sections.. PRINCIPAL DRIVER and OCCASIONAL DRIVER
							{
								//Check for College Student Class :
								if (strCollegeStudent.ToUpper().ToString().Trim() == "TRUE")
									//vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@COLLEGECLASS").InnerText.ToString();
								{
									if(strHaveCar == "10963")
									{
										if(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@CLASS")!=null)
											vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@CLASS").InnerText.ToString();

									}
									else
									{
										if(DriverNode.SelectSingleNode("VEHICLECOUNT")!=null && DriverNode.SelectSingleNode("DRIVERCOUNT")!=null)
										{
											if(DriverNode.SelectSingleNode("VEHICLECOUNT").InnerText.Trim().ToString()=="1" && DriverNode.SelectSingleNode("DRIVERCOUNT").InnerText.Trim().ToString()=="1")
											{
												if(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@CLASS")!=null)
													vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@CLASS").InnerText.ToString();
											}
											else
											{
												if(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@COLLEGECLASS")!=null)
													vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@COLLEGECLASS").InnerText.ToString();
											}
										}
										else
											if(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@COLLEGECLASS")!=null)
											vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@COLLEGECLASS").InnerText.ToString();
									}
								}
								else
								{
									if(strOperator == "OCCASIONAL")//Sate Check for 6 range classes for AUTOP:
									{
										if(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@CLASS")!=null)
											vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@CLASS").InnerText.ToString();
									}
									else
									{
										if(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@CLASS")!=null)
											vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator.ToString().ToUpper() + "' and @STATE='" + strState.ToUpper().Trim() +"']/ATTRIBUTES[" + driverAge  +" >=@MINAGE and  " + driverAge + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("GENDER").InnerText.ToString().ToUpper() +"']/@CLASS").InnerText.ToString();
									}
								}
							}
						}
						else
						{
							vehicleClass  = DataNode.FirstChild.Attributes["DEFAULT_CLASS"].Value.ToString().Trim();
						}
					}
				}
				strClass = "<CLASS>" + "<VEHICLECLASS>" + vehicleClass + "</VEHICLECLASS>" + "</CLASS>";
			}
				return strClass;


		}

		/// <summary>
		/// New Implementation of Driver Class :  20 August 2008
		/// driverInputXml -->From QQ Includes all Drivers
		///				   -->From APP and POL from Proc XML	
		///				   -->XML will Include POLICY INFO and DRIVERS INFO XML
		/// </summary>
		/// <param name="driverInputXml"></param>
		/// <returns></returns>
		public string GetEligibleDrivers(string driverInputXml)
		{
			
			string finalDriverXML = "";
			const string YOUTHFUL_OCCASSIONAL_POINT_APPLIED = "YOPA";
			const string YOUTHFUL_OCCASSIONAL_POINT_NOT_APPLIED = "YONPA";

			const string YOUTHFUL_PRINCIPAL_POINT_APPLIED = "YPPA";
			const string YOUTHFUL_PRINCIPAL_POINT_NOT_APPLIED = "YPNPA";

			const string PRINCIPAL_POINTS_APPLIED = "PPA";
			const string PRINCIPAL_POINTS_NOT_APPLIED = "PNPA";

			try
			{
				//Load Driver XML
				XmlDocument driverDoc = new XmlDocument();
				driverInputXml=driverInputXml.Replace("&AMP;","&amp;");
				driverDoc.LoadXml(driverInputXml);

				//List of Points Not Applied 
				XmlNodeList notAppliedDriverList = null;
				// ITRACK 5559 PREFERNCE WILL BE GIVEN TO YOUTHFULL PRINCIPAL RATHER THAN YOUTHFULL OCCASIONAL
				//notAppliedDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + YOUTHFUL_OCCASSIONAL_POINT_NOT_APPLIED + "' ]");
				notAppliedDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + YOUTHFUL_PRINCIPAL_POINT_NOT_APPLIED + "' ]");

				XmlNodeList youngDriverList = null;
                //First Prority YOUTHFUL_OCCASSIONAL_POINT_APPLIED (OVERWRIITEN BY ITRACK 5559)
				//youngDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + YOUTHFUL_OCCASSIONAL_POINT_APPLIED + "' ]");
				youngDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + YOUTHFUL_PRINCIPAL_POINT_APPLIED + "' ]");
				//First Prority YOUTHFUL_PRINCIPAL_POINT_APPLIED
				if(youngDriverList.Count >= 1)
				{
					finalDriverXML = youngDriverList.Item(0).OuterXml;
					return finalDriverXML;
				}
				else if(notAppliedDriverList.Count >=1) //YOUTHFUL_PRINCIPAL_POINT_NOT_APPLIED
				{
					//IGNORE THE VIOLATION AND ACCIDENT POINTS AS DRIVER IS POINT_NOT_APPLIED
					
					notAppliedDriverList.Item(0).SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText = "0";
					notAppliedDriverList.Item(0).SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText = "0";
					finalDriverXML = notAppliedDriverList.Item(0).OuterXml;
					return finalDriverXML;
				}
				else // Second Prority YOUTHFUL_OCCASSIONAL_POINT_APPLIED
				{
					notAppliedDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + YOUTHFUL_OCCASSIONAL_POINT_NOT_APPLIED + "' ]");

					youngDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + YOUTHFUL_OCCASSIONAL_POINT_APPLIED + "' ]");
					if(youngDriverList!=null)
					{
						if(youngDriverList.Count >= 1)
						{
							finalDriverXML = youngDriverList.Item(0).OuterXml;
							return finalDriverXML;
						}
						else if(notAppliedDriverList.Count >=1) //YOUTHFUL_OCCASSIONAL_POINT_NOT_APPLIED
						{
							//IGNORE THE VIOLATION AND ACCIDENT POINTS AS DRIVER IS POINT_NOT_APPLIED
							
							notAppliedDriverList.Item(0).SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText = "0";
							notAppliedDriverList.Item(0).SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText = "0";
							finalDriverXML = notAppliedDriverList.Item(0).OuterXml;
							return finalDriverXML;
						}
						else 
						{   //Third Priority PRINCIPAL
							//PRINCIPAL_POINTS_NOT_APPLIED;
							notAppliedDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_NOT_APPLIED + "' ]");

							XmlNodeList pDriverList = null;
							pDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_APPLIED + "' ]");
							if(pDriverList.Count >= 1)
							{
								int minAge = 150;
								int mindriverId = 0;
								foreach(XmlNode nodeP in pDriverList)
								{				
									
									int age = int.Parse(nodeP.Attributes["AGEOFDRIVER"].Value.ToString().Trim());
									int driverId = int.Parse(nodeP.Attributes["ID"].Value.ToString().Trim());

									if(age < minAge)
									{
										minAge = age;
										mindriverId = driverId;
									}
								
								}
								pDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_APPLIED + "' and @AGEOFDRIVER = '" + minAge + "' and @ID ='" + mindriverId + "']");
								if(pDriverList!=null)
								{
									if(pDriverList.Count>=1)
									{
										finalDriverXML = pDriverList.Item(0).OuterXml;
										return finalDriverXML;
									}

								}
							}
							else if(notAppliedDriverList.Count >=1) //YOUTHFUL_PRINCIPAL_POINT_NOT_APPLIED
							{
								//IGNORE THE VIOLATION AND ACCIDENT POINTS AS DRIVER IS POINT_NOT_APPLIED
								
								notAppliedDriverList.Item(0).SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText = "0";
								notAppliedDriverList.Item(0).SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText = "0";
								finalDriverXML = notAppliedDriverList.Item(0).OuterXml;
								return finalDriverXML;
							}
							
						}
					}

				}
							
			

			return finalDriverXML;
			}
			catch(XmlException ex)
			{
				throw(ex);
			}
			finally
			{}



		}

		/// <summary>
		/// For Motor Cycle to fetch Youngest Driver
		/// </summary>
		/// <param name="driverInputXml"></param>
		/// <returns></returns>
		public string GetEligibleDriversMotor(string driverInputXml)
		{

			string finalDriverXML = "";
			const string PRINCIPAL_POINTS_APPLIED = "PPA";
			const string PRINCIPAL_POINTS_NOT_APPLIED = "PNPA";

			try
			{
				//Load Driver XML
				XmlDocument driverDoc = new XmlDocument();
				driverInputXml=driverInputXml.Replace("&AMP;","&amp;");
				driverDoc.LoadXml(driverInputXml);

				XmlNodeList pDriverList = null;
				pDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_APPLIED + "' ]");
				if(pDriverList.Count >= 1)
				{
					int minAge = 150;
					int mindriverId = 0;
					foreach(XmlNode nodeP in pDriverList)
					{				
									
						int age = int.Parse(nodeP.Attributes["AGEOFDRIVER"].Value.ToString().Trim());
						int driverId = int.Parse(nodeP.Attributes["ID"].Value.ToString().Trim());

						if(age < minAge)
						{
							minAge = age;
							mindriverId = driverId;
						}
								
					}
					pDriverList = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_APPLIED + "' and @AGEOFDRIVER = '" + minAge + "' and @ID ='" + mindriverId + "']");
					if(pDriverList!=null)
					{
						if(pDriverList.Count>=1)
						{
							finalDriverXML = pDriverList.Item(0).OuterXml;
							return finalDriverXML;
						}

					}
				}
				//If Both NOT APPLIED
				XmlNodeList pDriverListNotApplied = null;
				pDriverListNotApplied = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_NOT_APPLIED + "' ]");
				if(pDriverListNotApplied.Count >= 1)
				{
					int minAge = 150;
					int mindriverId = 0;
					foreach(XmlNode nodeP in pDriverListNotApplied)
					{				
									
						int age = int.Parse(nodeP.Attributes["AGEOFDRIVER"].Value.ToString().Trim());
						int driverId = int.Parse(nodeP.Attributes["ID"].Value.ToString().Trim());

						if(age < minAge)
						{
							minAge = age;
							mindriverId = driverId;
						}
								
					}
					pDriverListNotApplied = driverDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_NOT_APPLIED + "' and @AGEOFDRIVER = '" + minAge + "' and @ID ='" + mindriverId + "']");
					if(pDriverListNotApplied!=null)
					{
						if(pDriverListNotApplied.Count>=1)
						{
							finalDriverXML = pDriverListNotApplied.Item(0).OuterXml;
							return finalDriverXML;
						}

					}
				}
				
				return finalDriverXML;
			}
			catch(XmlException ex)
			{
				throw(ex);
			}
			finally
			{}


		}
		#region Commented on 20 August 2008
		
	/*public string GetEligibleDrivers(string driverInputXml)
		{
			arrValues = new ArrayList();
			
			string driverNodes = "";
			const int limitAge = 25;
			XmlDocument driversDoc = new XmlDocument();
			driverInputXml=driverInputXml.Replace("&AMP;","&amp;");
			driverInputXml=driverInputXml.Replace("&GT;","&gt;");
			driverInputXml=driverInputXml.Replace("&LT;","&lt;");
			driversDoc.LoadXml(driverInputXml);
			XmlNode tmpNode;

			#region AT least one Driver below 25 yrs
			XmlNodeList DriverList = driversDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@AGEOFDRIVER < '" + limitAge + "' ]");
			XmlNodeList PrinDriverList = driversDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS = '" + "PRINCIPAL" + "' ]");
			
			if (DriverList != null)
			{
				//Case 1: when there is just 1 driver < 25 yrs
				if (DriverList.Count == 1)
				{
					tmpNode = driversDoc.SelectSingleNode("ELIGIBLEDRIVERS/DRIVER[@VEHICLEDRIVEDAS]");
					tmpNode.Attributes["VEHICLEDRIVEDAS"].Value = "PRINCIPAL";
					driverNodes = DriverList.Item(0).OuterXml;					
					return driverNodes;
				}
				else
				{
					//Case 2: Check the No of 'Principal' drivers  < 25 yrs
					XmlNodeList pDriverList = driversDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@AGEOFDRIVER < '" + limitAge + "' and @VEHICLEDRIVEDAS = '" + "PRINCIPAL" + "']");
					if(pDriverList.Count == 1)  //Pick the Driver (PRINCIPAL and < 25)
					{
						driverNodes = pDriverList.Item(0).OuterXml.ToString();  
						return driverNodes;
					}
					else if(pDriverList.Count > 1) //Pick the Driver (Principal and < 25 having least age)
					{
						foreach(XmlNode nodeP in pDriverList)
						{				
							ClsDriver obj = new ClsDriver();
							obj.mDriverage = int.Parse(nodeP.Attributes["AGEOFDRIVER"].Value.ToString().Trim());
							obj.mDriverid = int.Parse(nodeP.Attributes["ID"].Value.ToString().Trim());
							arrValues.Add(obj);
						}
						#region Sorting Drivers
						int result=0,age=0;
						int driverId = 0;
						if(arrValues.Count > 0)
						{
							ClsDriver obj = new ClsDriver();
							obj = (ClsDriver) arrValues[0];
							result = obj.mDriverage;
							driverId = obj.mDriverid;
							for (int i=0; i<arrValues.Count ;i++)
							{					
								obj = (ClsDriver) arrValues[i];
								age = obj.mDriverage;
								if(result >= age)
								{
									result=age;
									driverId = obj.mDriverid;
								}
							}
						}
						#endregion END Sorting Drivers
						/*Pick the OuterXML of youngest driver according to the Driver ID
						driverNodes = driversDoc.SelectSingleNode("//ELIGIBLEDRIVERS/DRIVER[@ID='" +  driverId + "']").OuterXml.ToString();
						return driverNodes;

					}
						// if no Principle driver found having age below 25 
					else if(pDriverList.Count == 0)
					{
						XmlNodeList pODriverList = driversDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@AGEOFDRIVER < '" + limitAge + "']");
						if(pODriverList.Count > 0)
						{
							foreach(XmlNode nodeP in pODriverList)
							{				
								ClsDriver obj = new ClsDriver();
								obj.mDriverage = int.Parse(nodeP.Attributes["AGEOFDRIVER"].Value.ToString().Trim());
								obj.mDriverid = int.Parse(nodeP.Attributes["ID"].Value.ToString().Trim());
								arrValues.Add(obj);
							}
							#region Sorting Drivers
							int result=0,age=0;
							int driverId = 0;
							if(arrValues.Count > 0)
							{
								ClsDriver obj = new ClsDriver();
								obj = (ClsDriver) arrValues[0];
								result = obj.mDriverage;
								driverId = obj.mDriverid;
								for (int i=0; i<arrValues.Count ;i++)
								{					
									obj = (ClsDriver) arrValues[i];
									age = obj.mDriverage;
									if(result >= age)
									{
										result=age;
										driverId = obj.mDriverid;
									}
								}
							}
							#endregion END Sorting Drivers
							/*Pick the OuterXML of youngest driver according to the Driver ID
							tmpNode = driversDoc.SelectSingleNode("//ELIGIBLEDRIVERS/DRIVER[@ID='" +  driverId + "']");
							if(PrinDriverList.Count<1)
							{
								tmpNode.Attributes["VEHICLEDRIVEDAS"].Value = "PRINCIPAL";
							}
							driverNodes = driversDoc.SelectSingleNode("//ELIGIBLEDRIVERS/DRIVER[@ID='" +  driverId + "']").OuterXml.ToString();
							return driverNodes;
						}
					
					}
					
				}
			}
			#endregion

			#region All Principal drivers above age 25 
			//Modifed XPATH,Applicabel to PRI  and OCC :
					
			string driverXml = "";string strPolicy = "";string finalXML = "";string classXml="";
			XmlNodeList pDriverAboveAgeLimitList = driversDoc.SelectNodes("ELIGIBLEDRIVERS/DRIVER[@AGEOFDRIVER >= '" + limitAge + "']");// and @VEHICLEDRIVEDAS = '" + "PRINCIPAL" + "']");
			if(pDriverAboveAgeLimitList.Count > 0)
			{
				if(pDriverAboveAgeLimitList.Count == 1)
				{
					driverNodes = pDriverAboveAgeLimitList.Item(0).OuterXml.ToString();
					return driverNodes; 
				}
				else if(pDriverAboveAgeLimitList.Count > 1)
				{
					foreach(XmlNode node in pDriverAboveAgeLimitList)
					{
						driverXml = driverXml.ToString() + node.OuterXml.ToString();
					}
					/*Get the Class of the Principal Drivers above 25
					XmlNode policyNode = driversDoc.SelectSingleNode("//POLICY");
					strPolicy = policyNode.InnerXml.ToString();
					finalXML = "<CLASS>" + strPolicy + "<ELIGIBLEDRIVER>" + driverXml + "</ELIGIBLEDRIVER>" + "</CLASS>" ;
					classXml = GetEligibleDriverClass(finalXML);
					/*Find the Relativity of each Class corresponding to the Driver

					#region Find Highest Relativity Class of Drivers
					if(classXml!="")
					{
						ArrayList arrValuesR = new ArrayList();
								
						//Load Masterdata XML
						string  relativityClass = "";
						XmlDocument DataDoc = new XmlDocument();
						string  strXmlFilePath = ClsCommon.GetKeyValueWithIP("VEHICLE_CLASS");
						DataDoc.Load(strXmlFilePath);
						XmlNode DataNode = DataDoc.SelectSingleNode("VEHICLECLASS");
						//Load the XML and Check the relativity of the class
						XmlDocument relativityDoc = new XmlDocument();
						relativityDoc.LoadXml(classXml);
								
						foreach(XmlNode relativeNode in relativityDoc.SelectNodes("CLASS/TOTALCLASS/*"))
						{
							ClsDriver objRel = new ClsDriver();
							objRel.mRelativityDriverid = int.Parse(relativeNode.Attributes["ID"].Value.ToString());
							relativityClass = relativeNode.SelectSingleNode("DRIVERCLASS").InnerText.ToString().Trim();
							if(relativityClass!=null && relativityClass!="")
								objRel.mRelativity   = double.Parse(DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/FACTOR/NODE/ATTRIBUTES[@CLASS= '" + relativityClass +"']/@RELATIVITY").InnerText.ToString());
							arrValuesR.Add(objRel);
						}

					
						#region Sorting Drivers
						double result=0,number=0;
						int driverId=0;								
						if(arrValuesR.Count > 0)
						{
							ClsDriver objRel = new ClsDriver();
							objRel = (ClsDriver) arrValuesR[0];
							result= objRel.mRelativity;
							driverId = objRel.mRelativityDriverid;
							for (int i=0; i<arrValuesR.Count ;i++)
							{
								objRel = (ClsDriver) arrValuesR[i];
								number= objRel.mRelativity; 
								if(result < number)
								{
									result=number;
									driverId = objRel.mRelativityDriverid;
								}
							}
								
						}
						#endregion
						
						driverNodes = driversDoc.SelectSingleNode("//ELIGIBLEDRIVERS/DRIVER[@ID='" +  driverId + "']").OuterXml.ToString();
						return driverNodes;
					}
					#endregion
				}
			}
			
			return ("");
			#endregion

			
		}*/
	
		#endregion

		#region GET Driver Class
		public string GetEligibleDriverClass(string InputClassXml)
		{
			XmlDocument ClassDoc = new XmlDocument();
			ClassDoc.LoadXml(InputClassXml);
			XmlNode ClassNode = ClassDoc.SelectSingleNode("CLASS");
			
			//Load Masterdata XML
			XmlDocument DataDoc = new XmlDocument();
			string  strXmlFilePath = ClsCommon.GetKeyValueWithIP("VEHICLE_CLASS");
			DataDoc.Load(strXmlFilePath);
			XmlNode DataNode = DataDoc.SelectSingleNode("VEHICLECLASS");
			
			string strState;string strRenewal;string strNewBussiness;
			string yearsContInsuredWithWolverine = "0";
			DateTime dtEffdate;
			dtEffdate = DateTime.Parse(ClassNode.SelectSingleNode("QUOTEEFFDATE").InnerText.ToString());
			strState = ClassNode.SelectSingleNode("STATENAME").InnerText.ToString();
			strRenewal = ClassNode.SelectSingleNode("RENEWAL").InnerText.ToString();
			strNewBussiness = ClassNode.SelectSingleNode("NEWBUSINESS").InnerText.ToString();
			
			yearsContInsuredWithWolverine = ClassNode.SelectSingleNode("YEARSCONTINSUREDWITHWOLVERINE").InnerText.ToString();
			//string vehicleClass = "";
			string totalVehicleClass = "";
			string eligibleClass = ""; 
			string eligibleDriverXml = ""; //Above 25 and Principal
			string DriverXml = ""; //Appended Nodes Drivers
			int driverMvrPoints = 0;
			string strClass = "";
			int driverAge=0;
			// ---No driver nodes when no operator is assigned --so assign PA
			
			foreach(XmlNode DriverNode in ClassNode.SelectNodes("ELIGIBLEDRIVER/*"))
			{
				if (DriverNode.SelectSingleNode("BIRTHDATE").InnerText.ToString().Trim() != "")
				{
					if(DriverNode.SelectSingleNode("AGEOFDRIVER")==null || DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText=="")
					{
						TimeSpan TS =  System.DateTime.Now-DateTime.Parse(DriverNode.SelectSingleNode("BIRTHDATE").InnerText.ToString());
						DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText = (TS.Days/365).ToString().Trim();
						driverAge=(TS.Days/365);
					}
					else
						driverAge=int.Parse(DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText);

					
					driverMvrPoints = int.Parse(DriverNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
					string strOperator = DriverNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText.ToUpper().Trim().ToString();					
					//Check if there is no Assigned Driver : 
					if (strOperator!="0" && strOperator!= "NR")  //NR NOT RATED
					{
						eligibleClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='ANY']/ATTRIBUTES[" + driverAge +" >=@MINAGE and  " + driverAge  + " <=@MAXAGE and @MINMVRPOINTS <= " + driverMvrPoints + " and @MAXMVRPOINTS >= " + driverMvrPoints + "]/@CLASS").InnerText.ToString();
						eligibleDriverXml =  DriverNode.OuterXml;
					}
					totalVehicleClass = totalVehicleClass  + "<DRIVER ID = '" + DriverNode.Attributes["ID"].Value.ToString()  +"' >" + "<DRIVERCLASS>" + eligibleClass +   "</DRIVERCLASS>" + "</DRIVER>";
					//Appending the Eligible Drivers Nodes
					DriverXml = DriverXml + eligibleDriverXml;
				
				}
			}
			
			strClass = "<CLASS>" + "<TOTALCLASS>" + totalVehicleClass +  "</TOTALCLASS>" + "<DRIVERS>" + DriverXml + "</DRIVERS>" + "</CLASS>";
			return strClass;


		}
		#endregion 

		#region COMMENTED CODE 
		//Get Info Drivers
		//			foreach(XmlNode DriverNode in ClassNode.SelectNodes("DRIVERINFO/*"))
		//			{
				
		//			
		//				//
		//				if (DriverNode.SelectSingleNode("BirthDate").InnerText.ToString().Trim() != "")
		//				{
		//					TimeSpan TS =  System.DateTime.Now-DateTime.Parse(DriverNode.SelectSingleNode("BirthDate").InnerText.ToString());
		//					DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText = (TS.Days/365).ToString().Trim();
		//					/*
		//					 * CLASS P
		//						Owner or Principal Operator With 4 Points or Less
		//						PA: Age 25-29
		//						PB: Age 30-34
		//						PC: Age 35-44
		//						PD: Age 45-49
		//						PE: Age 50-69
		//						PF: Age 70 +
		//					 * 
		//					 * */
		//					string strOperator = DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToUpper().Trim().ToString();
		//					if (DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" && Convert.ToInt32(DriverNode.SelectSingleNode("MVR").InnerText.ToString())<=4)
		//					{
		//						//XPATH FOR CLASS P : 
		//						if ((TS.Days/365) >=25)
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "']/ATTRIBUTES[" + (TS.Days/365) +" >=@MINAGE and  " + (TS.Days/365) + " <=@MAXAGE and @MAXMVRPOINTS <=4]/@CLASS").InnerText.ToString();
		//							//Gender Check
		//						else //if (DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" && DriverNode.SelectSingleNode("Gender").InnerText.ToString().ToUpper() == "MALE")
		//						{
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "']/ATTRIBUTES[" + (TS.Days/365) +" >=@MINAGE and  " + (TS.Days/365) + " <=@MAXAGE and @GENDER='"+ DriverNode.SelectSingleNode("Gender").InnerText.ToString().ToUpper() +"']/@CLASS").InnerText.ToString();
		//						}
		////						else if (DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" && DriverNode.SelectSingleNode("Gender").InnerText.ToString().ToUpper() == "FEMALE")
		////						{
		////							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "']/ATTRIBUTES[" + (TS.Days/365) +" >=@MINAGE and  " + (TS.Days/365) + " <=@MAXAGE and @GENDER='FEMALE']/@CLASS").InnerText.ToString();
		////						}
		//							/*	driverClassComp1 = "P";
		//
		//								if ((TS.Days/365) >=25 && (TS.Days/365) <=29)
		//								{
		//									driverClass = "PA";
		//									driverClassComp2 = "A";
		//								}
		//								else if ((TS.Days/365) >=30 && (TS.Days/365) <=34)
		//								{
		//									driverClass = "PB";
		//									driverClassComp2 = "B";
		//								}
		//								else if ((TS.Days/365) >=35 && (TS.Days/365) <=44)
		//								{
		//									driverClass = "PC";
		//									driverClassComp2 = "C";
		//								}
		//								else if ((TS.Days/365) >=45 && (TS.Days/365) <=49)
		//								{
		//									driverClass = "PD";
		//									driverClassComp2 = "D";
		//								}
		//								else if ((TS.Days/365) >=50 && (TS.Days/365) <=69)
		//								{
		//									driverClass = "PE";
		//									driverClassComp2 = "E";
		//								}
		//								//3.3 Age 70+, points <=4, Renewal, Class will be PF.
		//								else if ((TS.Days/365) >=70 && strRenewal.ToString() == "True")
		//								{
		//									driverClass = "PF";
		//									driverClassComp2 = "F";
		//								}*/
		//						}
		//							/*
		//							 * CLASS 1
		//								Owner or Principal Operator With 5 or More Points or Age 70+ and Not With Wolverine Mutual for 3 Prior Years
		//								1A: Age 25-29
		//								1B: Age 30-34
		//								1C: Age 35-44
		//								1D: Age 45-49
		//								1E: Age 50-69
		//								1F: Age 70 & Over & Not Insured With Wolverine Mutual the Prior 3 Yrs
		//							 * 
		//							 * */
		//						else if ((DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" && Convert.ToInt32(DriverNode.SelectSingleNode("MVR").InnerText.ToString()) >=5))// || ((TS.Days/365) >=70 && Double.Parse(yearsContInsuredWithWolverine.ToString()) <3))
		//						{
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "']/ATTRIBUTES[" + (TS.Days/365) +" >=@MINAGE and  " + (TS.Days/365) + " <=@MAXAGE and (@MAXMVRPOINTS = 5 or @MAXMVRPOINTS >=5)]/@CLASS").InnerText.ToString();
		//							/*driverClassComp1 = "1";
		//
		//							if ((TS.Days/365) >=25 && (TS.Days/365) <=29)
		//							{
		//								driverClass = "1A";
		//								driverClassComp2 = "A";
		//							}
		//							else if ((TS.Days/365) >=30 && (TS.Days/365) <=34)
		//							{
		//								driverClass = "1B";
		//								driverClassComp2 = "B";
		//							}
		//							else if ((TS.Days/365) >=35 && (TS.Days/365) <=44)
		//							{
		//								driverClass = "1C";
		//								driverClassComp2 = "C";
		//							}
		//							else if ((TS.Days/365) >=45 && (TS.Days/365) <=49)
		//							{
		//								driverClass = "1D";
		//								driverClassComp2 = "D";
		//							}
		//							else if ((TS.Days/365) >=50 && (TS.Days/365) <=69)
		//							{
		//								driverClass = "1E";
		//								driverClassComp2 = "E";
		//							}*/
		//							//						else if (((TS.Days/365) >=70 && Double.Parse(yearsContInsuredWithWolverine.ToString()) <3))
		//							//						{
		//							//							driverClass = "1F";
		//							//							driverClassComp2 = "F";
		//							//						}
		//							//
		//							/*3. In case of Age above 70, consider the following.
		//								3.1 Age 70+, Points >= 5, Class will be 1F.	//*/
		//							/*else if ((TS.Days/365) >=70  && Convert.ToInt32(DriverNode.SelectSingleNode("MVR").InnerText.ToString()) >=5)
		//							{
		//								driverClass = "1F";
		//								driverClassComp2 = "F";
		//							}*/
		//							//3.2 Age 70+, New Business, Class will be 1F.
		//							/*else if ((TS.Days/365) >=70  && strNewBussiness.ToString() == "True")
		//							{
		//								driverClass = "1F";
		//								driverClassComp2 = "F";
		//							}*/
		//						}
		//							//NEW ADDED POINTS : 
		//						else if (((DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" || DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "OCCASIONAL") && ((TS.Days/365) >=70 && strRenewal == "TRUE")))
		//						{
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "' and @MAXAGELIMIT >= 70]/ATTRIBUTES[@MAXAGE >= 70 and @RENEWAL='TRUE' and @MAXMVRPOINTS <=4]/@CLASS").InnerText.ToString();
		//						}
		//						else if (((DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" || DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "OCCASIONAL") && ((TS.Days/365) >=70 && strRenewal == "FALSE")))
		//						{
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "' and @MAXAGELIMIT >= 70]/ATTRIBUTES[@MAXAGE >= 70 and @RENEWAL='TRUE']/@CLASS").InnerText.ToString();
		//						}
		//						else if ((DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" || DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "OCCASIONAL") && ((TS.Days/365) >=70 || Convert.ToInt32(DriverNode.SelectSingleNode("MVR").InnerText.ToString()) >=5))
		//						{
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "' and @MAXAGELIMIT >= 70]/ATTRIBUTES[@MAXAGE >= 70 and @MAXMVRPOINTS >=5 ]/@CLASS").InnerText.ToString();
		//						}
		//							/*
		//							 * CLASS 2
		//								Occasional Male or Female Operator
		//								2A: Age 21-24
		//								2B: Age 18-20
		//								2C: Age 16-17
		//							 * */
		//						else if (DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "OCCASIONAL")
		//						{
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "']/ATTRIBUTES[" + (TS.Days/365) +" >=@MINAGE and  " + (TS.Days/365) + " <=@MAXAGE ]/@CLASS").InnerText.ToString();
		//							/*driverClassComp1 = "2";
		//
		//							if ((TS.Days/365) >=21 && (TS.Days/365) <=24)
		//							{
		//								driverClass = "2A";
		//								driverClassComp2 = "A";
		//							}
		//							else if ((TS.Days/365) >=18 && (TS.Days/365) <=20)
		//							{
		//								driverClass = "2B";
		//								driverClassComp2 = "B";
		//							}
		//							else if ((TS.Days/365) >=16 && (TS.Days/365) <=17)
		//							{
		//								driverClass = "2C";
		//								driverClassComp2 = "C";
		//							}*/
		//						}
		//							/*
		//							 * CLASS 3
		//								Owner or Principal Male Operator
		//								3A: Age 21-24
		//								3B: Age 18-20
		//								3C: Age 16-17
		//							 * 
		//							 * */
		//						else if (DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" && DriverNode.SelectSingleNode("Gender").InnerText.ToString().ToUpper() == "MALE")
		//						{
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "']/ATTRIBUTES[" + (TS.Days/365) +" >=@MINAGE and  " + (TS.Days/365) + " <=@MAXAGE and @GENDER='MALE']/@CLASS").InnerText.ToString();
		//							/*driverClassComp1 = "3";
		//
		//							if ((TS.Days/365) >=21 && (TS.Days/365) <=24)
		//							{
		//								driverClass = "3A";
		//								driverClassComp2 = "A";
		//							}
		//							else if ((TS.Days/365) >=18 && (TS.Days/365) <=20)
		//							{
		//								driverClass = "3B";
		//								driverClassComp2 = "B";
		//							}
		//							else if ((TS.Days/365) >=16 && (TS.Days/365) <=17)
		//							{
		//								driverClass = "3C";
		//								driverClassComp2 = "C";
		//							}*/
		//						}
		//							/*
		//							 * CLASS 4
		//								Owner or Principal Female
		//								4A: Age 21-24
		//								4B: Age 18-20
		//								4C: Age 16-17
		//							 * */
		//						else if (DriverNode.SelectSingleNode("VehicleDrivedAs").InnerText.ToString().ToUpper() == "PRINCIPAL" && DriverNode.SelectSingleNode("Gender").InnerText.ToString().ToUpper() == "FEMALE")
		//						{
		//							driverClass  = DataNode.SelectSingleNode("LOB[@CODE='AUTOP']/TYPE[@OPERATOR='" + strOperator + "']/ATTRIBUTES[" + (TS.Days/365) +" >=@MINAGE and  " + (TS.Days/365) + " <=@MAXAGE and @GENDER='FEMALE']/@CLASS").InnerText.ToString();
		//							/*driverClassComp1 = "4";
		//
		//							if ((TS.Days/365) >=21 && (TS.Days/365) <=24)
		//							{
		//								driverClass = "4A";
		//								driverClassComp2 = "A";
		//							}
		//							else if ((TS.Days/365) >=18 && (TS.Days/365) <=20)
		//							{
		//								driverClass = "4B";
		//								driverClassComp2 = "B";
		//							}
		//							else if ((TS.Days/365) >=16 && (TS.Days/365) <=17)
		//							{
		//								driverClass = "4C";
		//								driverClassComp2 = "C";
		//							}*/
		//						}
		//					
		//				}
		//			}
		
		//			strClass = "<CLASS>" + "<DRIVERCLASS>" + driverClass + "</DRIVERCLASS>" + "<DRIVERCLASSCOMPONENT1>" + driverClassComp1 + "</DRIVERCLASSCOMPONENT1>"  + "<DRIVERCLASSCOMPONENT2>" + driverClassComp2 + "</DRIVERCLASSCOMPONENT2>" + "</CLASS>";
		//			return strClass;
		//
		//			//
		//                
		//		}
		#endregion		

		#endregion

		#region MOTOR CLASS CALCULATION
		public string GetMotorVehicleClass(string InputClassXml)
		{
			XmlDocument ClassDoc = new XmlDocument();
			InputClassXml=InputClassXml.Replace("&AMP;","&amp;");
			ClassDoc.LoadXml(InputClassXml);
			XmlNode ClassNode = ClassDoc.SelectSingleNode("CLASS");

			//Load Masterdata XML
			XmlDocument DataDoc = new XmlDocument();
			string  strXmlFilePath;
			if(IsEODProcess)
			{
				strXmlFilePath =WebAppUNCPath + @"\cmsweb\" + VehicleClassXml ;
			}
			else
			{
				strXmlFilePath = ClsCommon.GetKeyValueWithIP("VEHICLE_CLASS");
			}
			
			DataDoc.Load(strXmlFilePath);
			XmlNode DataNode = DataDoc.SelectSingleNode("VEHICLECLASS");

			//Effective Date
			DateTime dtEffdate;
			dtEffdate = DateTime.Parse(ClassNode.SelectSingleNode("POLICY/QUOTEEFFDATE").InnerText.ToString());
			int driverAge = -1;
			//Getting Default Class from Maping XML;
			string vehicleClass = DataNode.SelectSingleNode("LOB[@CODE='CYCL']/@DEFAULT_CLASS").InnerText.ToString().Trim();
			if(ClassNode.SelectNodes("DRIVERINFO/*")!=null)
			{
				foreach(XmlNode DriverNode in ClassNode.SelectNodes("DRIVERINFO/*"))
				{
					//Done for Itrack Issue 5087 on 1 May 2005
					if(DriverNode.SelectSingleNode("VEHICLEDRIVEDAS").InnerText.ToString().ToUpper().Trim() == "PRINCIPAL")
					{
						if (DriverNode.SelectSingleNode("BIRTHDATE").InnerText.ToString().Trim() != "" && DriverNode.SelectSingleNode("BIRTHDATE")!=null)
						{
							//TimeSpan TS =  dtEffdate - DateTime.Parse(DriverNode.SelectSingleNode("BIRTHDATE").InnerText.ToString());
							//driverAge = int.Parse((TS.Days/365).ToString().Trim());
							if(DriverNode.SelectSingleNode("AGEOFDRIVER")!=null)
							{
								if(DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText!="")
									driverAge = int.Parse(DriverNode.SelectSingleNode("AGEOFDRIVER").InnerText);

							}


							//Fetch the MIN LIMIT Age 
							string strMinLimit = DataNode.SelectSingleNode("LOB[@CODE='CYCL']/@MINAGELIMIT").InnerText.ToString();
							strMinLimit = strMinLimit ==""?"0":strMinLimit;
							//Fetch the MAX LIMIT Age 
							string strMaxLimit = DataNode.SelectSingleNode("LOB[@CODE='CYCL']/@MAXAGELIMIT").InnerText.ToString();
							strMaxLimit = strMaxLimit ==""?"0":strMaxLimit;
							if(driverAge!=-1)
							{
								//XQUERY for getting Class
								if (driverAge < int.Parse ( strMinLimit ))
									vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='CYCL']/ATTRIBUTES[" + driverAge  +" >= @MINAGE and  " + driverAge  + " < @MAXAGE ]/@CLASS").InnerText.ToString();
								else if (driverAge >= int.Parse(strMinLimit) && driverAge < int.Parse(strMaxLimit))
									vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='CYCL']/ATTRIBUTES[" + driverAge  +" >= @MINAGE and  " + driverAge  + " < @MAXAGE ]/@CLASS").InnerText.ToString();
								else if(driverAge >= int.Parse(strMaxLimit))
									vehicleClass  = DataNode.SelectSingleNode("LOB[@CODE='CYCL']/ATTRIBUTES[" + driverAge  +" >= @MINAGE and  " + driverAge  + " < @MAXAGE ]/@CLASS").InnerText.ToString();
							}
						}

					}
				}
			}
			return vehicleClass;

            
		}


		#endregion


		public string PrepareAutoAcordXml(string CustomerId,string QuoteId,string AcordXmlPath,string StateCd,string QuoteNumber)
		{

			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				DataSet ObjDs;
				string strSEATBELT="";
				string QuickQuoteXml = new ClsQuickQuote().GetQuickQuoteXml(CustomerId,QuoteId);
				QuickQuoteXml = QuickQuoteXml.Replace("H673GSUYD7G3J73UDH","");
				QuickQuoteXml = QuickQuoteXml.Replace("D673GSUYD7G3J73UDD","\"");

				string AcordXml="";
			
				XmlDocument AcordDom=new XmlDocument();
				XmlDocument QuoteDom=new XmlDocument();

				if (QuickQuoteXml.Trim()!="")
				{
					QuoteDom.LoadXml(QuickQuoteXml);
					AcordDom.Load(AcordXmlPath);
					XmlNode CustomerNode = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/InsuredOrPrincipal");
				
					//********* Start of Customer Info **********************************************************************
					objWrapper.AddParameter("@CustomerID",CustomerId);
					ObjDs = objWrapper.ExecuteDataSet("Proc_GetCustomerDetails");
				
					string strCustomerStateCode = ObjDs.Tables[0].Rows[0]["CUSTOMER_STATE_CODE"].ToString().Trim();
					string strCustomerStateID = ObjDs.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString().Trim();
					if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11110")
					{
						CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString().Trim();
						CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
					}
					else if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11109")
					{
						CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
						
					}
				
					XmlNode nodTemp = CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/AddrTypeCd");
					nodTemp.InnerText = "StreetAddress";
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr1").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr2").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/City").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd").InnerText = strCustomerStateCode;
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateID").InnerText = strCustomerStateID;
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = QuoteDom.SelectSingleNode("//zipCode").InnerText.ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/CountryCd").InnerText = "us";
					CustomerNode.SelectSingleNode("InsuredOrPrincipalInfo").Attributes["id"].Value = CustomerId;
					//********* End of Customer Info **********************************************************************


					//********* Start of Agency Info **********************************************************************
					AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString().Trim();
					
					//add by kranti on 17th april 2007 for agency id
					AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialId").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_ID"].ToString().Trim();
					//********* End of Agency Info **********************************************************************

					//********* Start of Pers Policy Info **********************************************************************
					XmlNode PersPolicyNode = AcordDom.SelectSingleNode("//PersPolicy");
					PersPolicyNode.SelectSingleNode("ControllingStateProvCd").InnerText = StateCd;
					
					//AppTerms add by kranti
					if(QuoteDom.SelectSingleNode("//POLICYTERMS")!= null)
					PersPolicyNode.SelectSingleNode("ContractTerm/AppTerms").InnerText = QuoteDom.SelectSingleNode("//POLICYTERMS").InnerText ;
					
					PersPolicyNode.SelectSingleNode("ContractTerm/EffectiveDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("ContractTerm/ExpirationDt").InnerText = DateTime.Parse(QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim()).AddMonths(int.Parse(QuoteDom.SelectSingleNode("//POLICYTERMS").InnerText.ToString().Trim())).ToString().Trim();
					PersPolicyNode.SelectSingleNode("ContractTerm/ContinuousInd").InnerText = QuoteDom.SelectSingleNode("//yearsContInsured").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("OriginalInceptionDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("QuoteInfo/CompanysQuoteNumber").InnerText = QuoteNumber;
					PersPolicyNode.SelectSingleNode("QuoteInfo/InitialQuoteRequestDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("CreditScoreInfo/CreditScore").InnerText = QuoteDom.SelectSingleNode("//InsuranceScore").InnerText.ToString().Trim();
					if ( QuoteDom.SelectSingleNode("//multiPolicyAutoHomeDiscount") != null )
					{
						PersPolicyNode.SelectSingleNode("MultiPolicy").InnerText = QuoteDom.SelectSingleNode("//multiPolicyAutoHomeDiscount").InnerText.Trim();
					}
					PersPolicyNode.SelectSingleNode("LengthTimeInsured").InnerText = QuoteDom.SelectSingleNode("//yearsContInsured").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("LengthTimeInsuredWithWolverine").InnerText = QuoteDom.SelectSingleNode("//YEARSCONTINSUREDWITHWOLVERINE").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("SubLOBCd").InnerText = QuoteDom.SelectSingleNode("//qualifiesTraiblazerProgram").InnerText.ToString().Trim();
					strSEATBELT= QuoteDom.SelectSingleNode("//wearingSeatBelt").InnerText.ToString().Trim();

					//Underwriting Tier
					if(QuoteDom.SelectSingleNode("//UNDRWRTINGTIER")!=null)
                        PersPolicyNode.SelectSingleNode("UnderWritingTier").InnerText = QuoteDom.SelectSingleNode("//UNDRWRTINGTIER").InnerText.ToString().Trim();

					PersPolicyNode.SelectSingleNode("UnderWritingTierDate").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();

					//********* End of Pers Policy Info **********************************************************************
				

				
					//********* Start of Vehicle Implementation **********************************************************************
					XmlNode VehicleNode = AcordDom.SelectSingleNode("//PersVeh");
					string VehicleBlankXml = VehicleNode.OuterXml;
					AcordDom.SelectSingleNode("//PersAutoLineBusiness").RemoveChild(VehicleNode);
				
					XmlNode LocationNode = AcordDom.SelectSingleNode("//PersAutoPolicyQuoteInqRq/Location");
					string LocationBlankXml = LocationNode.OuterXml;
					AcordDom.SelectSingleNode("//PersAutoPolicyQuoteInqRq").RemoveChild(LocationNode);
				
					XmlNode node;

					foreach (XmlNode Node in QuoteDom.SelectNodes("//vehicles/*"))
					{
						XmlDocument VehicleDom = new XmlDocument();
						VehicleDom.LoadXml(VehicleBlankXml);

						XmlDocument LocationDom = new XmlDocument();
						LocationDom.LoadXml(LocationBlankXml);

						VehicleDom.FirstChild.Attributes["id"].Value="V" + Node.Attributes["id"].Value.ToString().Trim();
						VehicleDom.FirstChild.Attributes["LocationRef"].Value="L" + Node.Attributes["id"].Value.ToString().Trim();

						VehicleDom.FirstChild.SelectSingleNode("Manufacturer").InnerText=Node.SelectSingleNode("Make").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("Model").InnerText=Node.SelectSingleNode("Model").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("ModelYear").InnerText=Node.SelectSingleNode("Year").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("VehIdentificationNumber").InnerText=Node.SelectSingleNode("Vin").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("CostNewAmt/Amt").InnerText=Node.SelectSingleNode("Cost").InnerText.ToString().Trim();
						//For Itrack 5348 : Added VehAge in INTR XML
						if(VehicleDom.FirstChild.SelectSingleNode("VehAge")!=null)
						{
							VehicleDom.FirstChild.SelectSingleNode("VehAge").InnerText = Node.SelectSingleNode("AGE").InnerText.ToString();
						}
					
						if ( Node.SelectSingleNode("VEHICLECLASS") != null )
						{
							if ( Node.SelectSingleNode("VEHICLETYPEUSE").InnerText.Trim().ToUpper() == "PERSONAL")
							{
								VehicleDom.FirstChild.SelectSingleNode("RateClassCd").InnerText = Node.SelectSingleNode("VEHICLECLASS").InnerText.Trim();
								if(VehicleDom.FirstChild.SelectSingleNode("RateClassId")!=null)
								VehicleDom.FirstChild.SelectSingleNode("RateClassId").InnerText = Node.SelectSingleNode("CLASS_DRIVERID").InnerText.Trim();
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("RateClassCd").InnerText = Node.SelectSingleNode("VEHICLECLASS_COMM").InnerText.Trim();
								//take the node:RateClassDescCd
								if(VehicleDom.FirstChild.SelectSingleNode("RateClassDescCd")!=null)								
									VehicleDom.FirstChild.SelectSingleNode("RateClassDescCd").InnerText = Node.SelectSingleNode("VEHICLECLASS_CODE_COMM").InnerText.Trim();
								else
									VehicleDom.FirstChild.SelectSingleNode("RateClassDescCd").InnerText = "0";

							}
						}
					
						if ( Node.SelectSingleNode("VEHICLETYPEUSE") != null )
						{
							VehicleDom.FirstChild.SelectSingleNode("VehicleUse").InnerText = Node.SelectSingleNode("VEHICLETYPEUSE").InnerText.Trim();
						}
					
						if ( Node.SelectSingleNode("MultiCarDiscountCode") != null )
						{
							VehicleDom.FirstChild.SelectSingleNode("MulticarDisount").InnerText = Node.SelectSingleNode("MultiCarDiscountCode").InnerText.Trim();
						}
					             
						VehicleDom.FirstChild.SelectSingleNode("VehTypeCd").InnerText=Node.SelectSingleNode("vehicleType").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("EstimatedAnnualDistance/NumUnits").InnerText=Node.SelectSingleNode("AnnualMiles").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("VehUseCd").InnerText=Node.SelectSingleNode("Use").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("AntiLockBrakeCd").InnerText=Node.SelectSingleNode("isAntiLockBrakesDiscounts").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("AntilockBrakeDiscount").InnerText=Node.SelectSingleNode("isAntiLockBrakesDiscounts").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("VehSymbolCd").InnerText=Node.SelectSingleNode("Symbol").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("DistanceOneWay/NumUnits").InnerText=Node.SelectSingleNode("MilesEachWay").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("AirBagTypeCd").InnerText=Node.SelectSingleNode("AirBagDiscount").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("TerritoryCd").InnerText=Node.SelectSingleNode("TerrCodeGaragedLocation").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("SeatBeltTypeCd").InnerText=strSEATBELT;
						
						
						//Addded by RP - 5 sep 2006
						VehicleDom.FirstChild.SelectSingleNode("CarPool").InnerText=Node.SelectSingleNode("CARPOOL").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("RadiusOfUse").InnerText=Node.SelectSingleNode("RadiusOfUse").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("SnowPlowCode").InnerText=Node.SelectSingleNode("SNOWPLOWCODE").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("InsuranceAmountMiscEqupt").InnerText=Node.SelectSingleNode("InsuranceAmount").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("InsuranceDescMiscEqupt").InnerText="Miscellaneous Equipment";

						//Added by Kasana - 12 Nov 2008
						if(VehicleDom.FirstChild.SelectSingleNode("IsSuspendedComp")!=null)
						{
							if(Node.SelectSingleNode("SUSPENDEDCOMP")!=null)
								VehicleDom.FirstChild.SelectSingleNode("IsSuspendedComp").InnerText=Node.SelectSingleNode("SUSPENDEDCOMP").InnerText.ToString().Trim();
						}
						
						
						//VehicleDom.FirstChild.SelectSingleNode("SnowPlowCode").InnerText=Node.SelectSingleNode("SNOWPLOW").InnerText.ToString().Trim();
						//End of addition by RP - 5 Sep 2006

						//Garaging location Zip code and territory code
						LocationDom.FirstChild.Attributes["id"].Value = "L" + Node.Attributes["id"].Value.ToString().Trim();
						LocationDom.FirstChild.SelectSingleNode("Addr/PostalCode").InnerText = Node.SelectSingleNode("ZipCodeGaragedLocation ").InnerText.ToString().Trim();
						LocationDom.FirstChild.SelectSingleNode("Addr/County").InnerText = Node.SelectSingleNode("TerrCodeGaragedLocation ").InnerText.ToString().Trim();

						AcordDom.SelectSingleNode("//PersAutoPolicyQuoteInqRq").InnerXml = AcordDom.SelectSingleNode("//PersAutoPolicyQuoteInqRq").InnerXml + LocationDom.OuterXml.ToString().Trim();
						
						// ############## START OF VEHICLE COVERAGES ###########################################

						//Comprehensive
						if ( Node.SelectSingleNode("ComprehensiveDeductible") != null )
						{
							double dbComprehensiveDeductible = Convert.ToDouble(Node.SelectSingleNode("ComprehensiveDeductible").InnerText.Trim()==""?"0.0":Node.SelectSingleNode("ComprehensiveDeductible").InnerText.Trim());
							if ( Node.SelectSingleNode("ComprehensiveDeductible").InnerText.ToString().Trim() == ""  || dbComprehensiveDeductible == 0 )
							{															
									VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COMP']"));
									VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'OTC')]").ParentNode);
								
							}
							else
							{							
										
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'OTC')]").ParentNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("ComprehensiveDeductible").InnerText.ToString().Trim();
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COMP']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("ComprehensiveDeductible").InnerText.ToString().Trim();
								
							}
						}
					
						//Collision
						if ( Node.SelectSingleNode("CollisionDeductible") != null )
						{
							if ( Node.SelectSingleNode("CollisionDeductible").InnerText.ToString().Trim() == "" )
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COLL']"));
							}
							else
							{

								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COLL']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("COVGCOLLISIONDEDUCTIBLE").InnerText.ToString().Trim();
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COLL']").SelectSingleNode("Deductible/Text").InnerText=Node.SelectSingleNode("COVGCOLLISIONTYPE").InnerText.ToString().Trim();
								//Added on 30 Oct 2008
								//VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COLL']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = "0";

							}
						}

						
						//If CollisionDeductible is 400 and 500 REGULAR then set Coverage RCC68 : Itrack 4557
						if(Node.SelectSingleNode("CollisionDeductible") != null )
						{
							if ( Node.SelectSingleNode("CollisionDeductible").InnerText.ToString().Trim()!="")
							{
								if ( Node.SelectSingleNode("CollisionDeductible").InnerText.ToString().Trim().ToUpper() == REGULAR_400 ||
									Node.SelectSingleNode("CollisionDeductible").InnerText.ToString().Trim().ToUpper() == REGULAR_500)
								{
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RCC68')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = "-1";

								}
								else
								{
									VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RCC68')]").ParentNode);
                                    
								}
							}
							else
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RCC68')]").ParentNode);

						}

						//Remove Policy level coverage {Underinsured Motorists (BI Split Limit)} from Commercial Trailer 
						if ( Node.SelectSingleNode("VEHICLETYPEUSE").InnerText.Trim().ToUpper() == "COMMERCIAL")
						{
							if(Node.SelectSingleNode("vehicleType").InnerText == "TR")
							{
								//UNDSP
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']");
								if ( node != null )
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
						}

						//Remove Policy level coverage (Medical Payments) from Utility Trailer
						if ( Node.SelectSingleNode("VEHICLETYPEUSE").InnerText.Trim().ToUpper() == "PERSONAL")
						{
							if(Node.SelectSingleNode("vehicleType").InnerText == "TR")
							{
								//UNDSP
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MP']");
								if ( node != null )
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
						}



						//Road Service
						if ( Node.SelectSingleNode("RoadService") != null )
						{
							if ( Node.SelectSingleNode("RoadService").InnerText.ToString().Trim() == "" )
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'ROAD')]").ParentNode);
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'ROAD')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("RoadService").InnerText.ToString().Trim();
							}
						}

						//Rental Reimbursement
						if (Node.SelectSingleNode("RentalReimbursement").InnerText.ToString().Trim()!="")
						{
							string[] strCoverage = Node.SelectSingleNode("RentalReimbursement").InnerText.ToString().Trim().Split('/');
							VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RREIM')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strCoverage[0];
							VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RREIM')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strCoverage[1];
						}
						else
						{
							VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RREIM')]").ParentNode);
						}
					
						//Mini tort PD Liability
						if  ( Node.SelectSingleNode("MiniTortPdLiab") != null )
						{
							if ( Node.SelectSingleNode("MiniTortPdLiab").InnerText.ToString().Trim().ToLower() == "true" )
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'LPD')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = MINITORTLimit;
							}
							else
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'LPD')]").ParentNode);
							}
						}
						//For MI PIP94 (8 July 2008) : Itrack 4457
						if(StateCd.Trim().ToString().ToUpper() == "MICHIGAN")
						{
							if  ( Node.SelectSingleNode("WAIVEWORKLOSS") != null )
							{
								if ( Node.SelectSingleNode("WAIVEWORKLOSS").InnerText.ToString().Trim().ToLower() == "true" )
								{
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PIP94')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = ""; 
								}
							}
						}					
						//Loan/Lease gap
						if  ( Node.SelectSingleNode("LoanLeaseGap") != null )
						{
							if ( Node.SelectSingleNode("LoanLeaseGap").InnerText.ToString().Trim().ToLower() != "" )
							{
								string loanText = Node.SelectSingleNode("LoanLeaseGap").InnerText.ToString().Trim().ToUpper();
 
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'LLGC')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = "-1";

								if ( loanText == "LOAN")
								{
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'LLGC')]").ParentNode.SelectSingleNode("Limit/Text").InnerText = "Loan";
								}	
								else if ( loanText == "LEASE")
								{
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'LLGC')]").ParentNode.SelectSingleNode("Limit/Text").InnerText = "Lease";
								}

							}
							else
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'LLGC')]").ParentNode);
							}
						}

						//Sound Reproducing Equipment (Tapes)
						if  ( Node.SelectSingleNode("is200SoundReproducing") != null )
						{
							if ( Node.SelectSingleNode("is200SoundReproducing").InnerText.ToString().Trim().ToLower() == "true" )
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'SORPE')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = "200"; // HARD CODED SINCE IT WILL BE 200 ALWAYS ... Node.SelectSingleNode("SoundReceivingTransmittingSystem").InnerText.ToString().Trim();
							}
							else
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'SORPE')]").ParentNode);
							}
						}
					
						//Sound receiving and Transmitting equipment Prevoius Code :SORCV NEW CODE : SRTE
						if ( Node.SelectSingleNode("SoundReceivingTransmittingSystem") != null )
						{	

							double dblSoundReceiving = Convert.ToDouble(Node.SelectSingleNode("SoundReceivingTransmittingSystem").InnerText.Trim()==""?"0.0":Node.SelectSingleNode("SoundReceivingTransmittingSystem").InnerText.Trim());
							if (dblSoundReceiving <=0)
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'SRTE')]").ParentNode);
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'SRTE')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = Node.SelectSingleNode("SoundReceivingTransmittingSystem").InnerText.ToString().Trim();	
							}

						}

						/* UM PD Limit
						if ( Node.SelectSingleNode("PDLimit") != null )
						{	
							if ( Node.SelectSingleNode("PDLimit").InnerText.Trim() == "" )
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UMPD')]").ParentNode);
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UMPD')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("//PDLimit").InnerText.ToString().Trim();	
							}

						} */

						//Residual Bodily Injury Split limit
						if (QuoteDom.SelectSingleNode("//BI").InnerText.ToString().Trim().ToUpper() != "" && QuoteDom.SelectSingleNode("//BI").InnerText.ToString().Trim().ToUpper() != "NO COVERAGE")
						{
							string[] strCoverage = QuoteDom.SelectSingleNode("//BI").InnerText.ToString().Trim().Split('/'); 

							if ( strCoverage != null && strCoverage.Length == 2)
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strCoverage[0];
							
								double dblBISPL = 0;

								if ( strCoverage[1] != null )
								{
									dblBISPL = double.Parse( strCoverage[1] );
									//dblBISPL = dblBISPL / 1000;

								}

								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = dblBISPL.ToString();
							}

						}
						else
						{
							VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode);
						}
						// End of Bodily injury Split limit
										
						//Property Damage
						if ( Node.SelectSingleNode("//PD") != null )
						{	
							if ( Node.SelectSingleNode("//PD").InnerText.Trim() == "" || Node.SelectSingleNode("//PD").InnerText.Trim().ToUpper() == "NO COVERAGE" )
							{
							
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PD']");

								if ( node != null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PD']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//PD").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");
							}

						}
						//End of Property Damage

						//Residual liability CSL
						if (QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper() != "" && QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper() != "NO COVERAGE")
						{
							//VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RLCSL')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");
							//add by kranti
							VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'SLL')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");

						}						
					
						if (QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper() == "" || QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper() == "NO COVERAGE")
						{
							VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RLCSL')]").ParentNode);
							VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'SLL')]").ParentNode);
						}
						//
					

					
						//Medical Payment
						if ( Node.SelectSingleNode("MEDPM") != null )
						{	
							if ( Node.SelectSingleNode("MEDPM").InnerText.Trim() == "" )
							{
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MP']");
								if ( node != null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}

								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PIP']");
								if ( node != null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
								 
							}
							else
							{
								if(StateCd.Trim().ToUpper() =="INDIANA")
								{
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MP']");
									if ( node != null )
									{	
										if ( QuoteDom.SelectSingleNode("//MEDPM").InnerText.ToString().Trim().ToUpper()  != "NO COVERAGE" )
											node.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("//MEDPM").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");
										else
											VehicleDom.FirstChild.RemoveChild(node);
									}
									
									//remove PIP
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PIP']");
									if ( node != null)
									{
										VehicleDom.FirstChild.RemoveChild(node);
									}
								}
								else //for michigan
								{
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PIP']");
									if ( node != null )
									{	
										if ( Node.SelectSingleNode("VEHICLETYPEUSE").InnerText.Trim().ToUpper() == "PERSONAL")
										{
											string strPIP = QuoteDom.SelectSingleNode("//MEDPM").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");
											switch (strPIP)
											{
												case "FULL":
													node.SelectSingleNode("Limit/Text").InnerText = "Full Medical & Full Wage Loss";
													break;
												case "EXCESSWAGE":
													node.SelectSingleNode("Limit/Text").InnerText = "Excess Wage";
													break;
												case "EXCESSMEDICAL":
													node.SelectSingleNode("Limit/Text").InnerText = "Excess Medical";
													break;
												case "EXCESSBOTH":
													node.SelectSingleNode("Limit/Text").InnerText = "Excess Wage/Medical";
													break;
												default:
													node.SelectSingleNode("Limit/Text").InnerText="Excess Medical";	
													break;
											}
										}
										else
										{
											string strPIP = QuoteDom.SelectSingleNode("//MEDPM").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");
											switch (strPIP)
											{
												//case "FULL":
												case "FULLMEDICALFULLWAGELOSS":
													node.SelectSingleNode("Limit/Text").InnerText = "Full Medical & Full Wage Loss";
													break;
												//case "EXCESSWAGE":
												case "FULLMEDICALEXCESSWAGEWORKCOMP":
													node.SelectSingleNode("Limit/Text").InnerText = "Full Medical, Excess Wage &/or Work Comp";
													break;
												//case "EXCESSMEDICAL":
												case "EXCESSMEDICALFULLWAGELOSS":
													node.SelectSingleNode("Limit/Text").InnerText = "Excess Medical & Full Wage Loss";
													break;
												//case "EXCESSBOTH":
												case "EXCESSMEDICALEXCESSWAGELOSSWORKCOMP":
													node.SelectSingleNode("Limit/Text").InnerText = "Excess Medical & Excess Wage Loss &/Or Work Comp";
													break;
												default:
													node.SelectSingleNode("Limit/Text").InnerText="Excess Medical & Full Wage Loss";	
													break;
											}
										}
										//Put the value in Deductible node for PIP 
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PIP']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//MEDPMDEDUCTIBLE").InnerText.ToString().Trim();
																	
									}

									//remove MP
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MP']");
									if ( node != null)
									{
										VehicleDom.FirstChild.RemoveChild(node);
									}
								}
							}

						}

						//Medical Type. BI or BI/PD -- uninsured and under insured
						if ( Node.SelectSingleNode("Type") != null )
						{	
							if ( Node.SelectSingleNode("Type").InnerText.Trim() == "No Coverage" )
							{
								//Remove BI and BIPD nodes
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='BI']"));
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='BIPD']"));
							}
							else if ( Node.SelectSingleNode("Type").InnerText.Trim() == "BI" )
							{
								//Remove BI/PD	
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='BIPD']"));
							
							}
							else if ( Node.SelectSingleNode("Type").InnerText.Trim() == "BI/PD $0 Deductible" ||Node.SelectSingleNode("Type").InnerText.Trim() == "BI/PD $300 Deductible" )
							{
								//Remove BI	
								XmlNode remNode = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='BI']");

								VehicleDom.FirstChild.RemoveChild(remNode);

								//Put the value in Deductible node for PD - 'PDDEDUCTIBLE'
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='BIPD']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//PDDEDUCTIBLE").InnerText.ToString().Trim();
							
							}

						}
					
				
						//****** Uninsured and Insured *********
						if ( Node.SelectSingleNode("Type") != null )
						{
							/* Check if Type = No coverage.
							 * Remove all the coverage nodes related to uninsured and underinsured motorists.
							 * ELSE
							 * 1. Check the BI split limit for UM and UIM. If BI Split Limit doesnt exist, 
							 *	  then check for CSL.
							 * 2. Check if UnderInsured motorists is checked. If yes then make same entries for UIM as those for UM.
							 */
							if ( Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() == "NO COVERAGE" || Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() == "")
							{
								//Remove :PUMSP, PUNCS, UMPD,UNDSP,UNCSL,UNDSP

								//PUMSP
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']");
								if ( node != null )
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							
								//PUNCS
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']");
								if ( node != null )
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							
								//UMPD
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']");
								if ( node != null )
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}

								//UNDSP
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']");
								if ( node != null )
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							
								//UNCSL
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNCSL']");
								if ( node != null )
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
							else
							{
								// Check UnInsured motorists limits - BI split limit .If it exists, then proceed.Remove CSL node.
								if (QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim() != "" && QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim() != "0")
								{
									
									string[] strUMCoverage = QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim().Split('/'); 
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUMCoverage[0];
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUMCoverage[1];
									
									//Remove Uninsured CSL - PUNCS
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']");
									if ( node != null )
									{
										VehicleDom.FirstChild.RemoveChild(node);
									}

									//same entries for Under Insured Motorists if selected
									if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="TRUE")
									{
										
										string[] strUIMCoverage = QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim().Split('/'); 
										if(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']")!=null)
											VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']").SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage[0];
										if(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']")!=null)
											VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']").SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage[1];

										//Itrack 5341
										if(StateCd.Trim().ToUpper() =="INDIANA")
										{
											string[] strUIMCoverageIN = QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim().Split('/'); 
											if(strUIMCoverageIN[0] == "25" && strUIMCoverageIN[1] == "50,000")
											{
												if(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']")!=null)
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']").SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText= "50";
												if(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']")!=null)
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']").SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = "50000";

											}

										}
										
										//Remove Underinsured CSL
										VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNCSL']"));
										
									}
									else
									{
										//Remove entries related to UIM: UNCSL,UNDSP

										//UNCSL
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNCSL']");

										if ( node != null)
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}

										//UNDSP
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']");

										if ( node != null )
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}


									}
									//07 jan 2009
									/*o	If the limit for Uninsured Motorist (BI split Limit) is lower than the Bodily Injury Liability (Split Limit) 
										o	open up the box for Signature Obtained ..Done
										strUMCoverage = Limit_1
										*/
									string limitBI = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.Replace(",","");
									if(limitBI!="")
									{
										if(int.Parse(strUMCoverage[0].Replace(",","")) < int.Parse(limitBI))
										{
											//Open Signature Box
											VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectSingleNode("IsSigObt").InnerText = "N";
										}
									}
									//07 Jan 2009
									/*o	If the limit for Uninsured Motorist PD is reject –then 
										open up the box for Signature Obtained
									*/
									if(QuoteDom.SelectSingleNode("//PDLimit").InnerText.ToString().Trim()=="") //Reject UMPD 
									{
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectNodes("Limit/Text").Item(0).InnerText = REJECT;
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectSingleNode("IsSigObt").InnerText = "N";
									}

								} // Uninsured motorist - CSL 
								else if(QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim() != "")
								{
									// Add UM CSL and remove the other BI split limits for UM and UIM 							
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']").SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim();
								
									//Part C - Uninsured Motorists (BI Split Limit)
									if(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']")!=null)
                                        VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']"));								
			  
									//same entries for Under Insured Motorists if selected
									if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="TRUE")
									{
										// Add Underinsured CSL - UNCSL
										string  strUIMCoverage = QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim(); 
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNCSL']").SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage;

										//Remove Underinsured Split Limit
										if(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']")!=null)
                                            VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNDSP']"));
										//Itrack 4961 : Underinsured Motorists (CSL) – not applicable for Commercial vehicle. So, it should not be there on DEC page. 30 Oct 2008
										if(StateCd.Trim().ToUpper() !="INDIANA" ) //Itrack 5441:Underinsured Motorists (CSL) – applicable for Commercial vehicle in case of state is INDIANA:Praveen Kumar(06-03-2009)
										{
											if ( Node.SelectSingleNode("VEHICLETYPEUSE").InnerText.Trim().ToUpper() == "COMMERCIAL")
											{
												//Remove Underinsured CSL
												VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNCSL']"));
											}
										}

									}
									else
									{
										//Remove entries relatd to UIM: UNCSL,UNDSP
										//UNCSL
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");

										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}

										//UNDSP
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}
									}
									//6 jan 2008
									//If the limit for Uninsured Motorist CSL lower than the Combined Single Limit (CSL) –then 
									//	o open up the box for Signature Obtained 
									int intUMCSL = 0;
									int intCSL = 0;

									if(QuoteDom.SelectSingleNode("//UMCSL").InnerText!="")
										intUMCSL = int.Parse(QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim().Replace(",",""));
									if(QuoteDom.SelectSingleNode("//CSL").InnerText!="")
										intCSL = int.Parse(QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().Replace(",",""));
									if(intUMCSL < intCSL)
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']").SelectSingleNode("IsSigObt").InnerText = "N";

									/*o	once the coverage Uninsured Motorist CSL is checked then make the following coverages available and checked 
									o	Underinsured Motorist ..Done
									o	Uninsured Motorist PD … REJECT
									*/
									if(QuoteDom.SelectSingleNode("//PDLimit").InnerText.ToString().Trim()=="") //Reject UMPD 
									{
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectNodes("Limit/Text").Item(0).InnerText = REJECT;
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectSingleNode("IsSigObt").InnerText = "N";
									}


								}
								//#################5 jan 2008  : If the limit is No coverage on QQ then put in Reject on the App/Policy side 
								else//REJECT CASE UMCSL
								{
                                    VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']").SelectNodes("Limit/Text").Item(0).InnerText = REJECT;
									//If Uninsured Motorist CSL is set to reject then do not open 
									//o	Underinsured Motorist 
									//o	Uninsured Motorist PD 
									//UMPD,UNCSL,UNDSP

									//UMPD
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UMPD')]");

									if ( node != null && node.ParentNode != null)
									{
										VehicleDom.FirstChild.RemoveChild(node.ParentNode);
									}
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");
									//UNCSL
									if ( node != null && node.ParentNode != null)
									{
										VehicleDom.FirstChild.RemoveChild(node.ParentNode);
									}
                                    //UNDSP
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

									if ( node != null && node.ParentNode != null)
									{
										VehicleDom.FirstChild.RemoveChild(node.ParentNode);
									}
									//PUMSP --Moved to BI --Commented 07 jan 2009
//									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]");
//									if ( node != null && node.ParentNode != null)
//									{
//										VehicleDom.FirstChild.RemoveChild(node.ParentNode);
//									}
									//Case 2 :
									/*
									 *  o If the limit for Uninsured Motorist CSL is reject --or lower than the Combined Single Limit (CSL) –then 
										o open up the box for Signature Obtained 
										o check off and grey out the coverage Rejection / Reduction of Uninsured & Underinsured Motorist Coverage (A-9)	---Cant IMport from QQ									
									 * */
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']").SelectSingleNode("IsSigObt").InnerText = "N";

									//BI Section 07 jan 2009..............
									//o	If the limit is No coverage on QQ then put in Reject on the App/Policy side
									string limitBI = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText;
									if(limitBI!="") 
									{
										if(QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim() == "0")//Reject Case UMSPLIT
										{
											VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectNodes("Limit/Text").Item(0).InnerText=PUMSP_LIMIT_TEXT;
											VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectNodes("Limit/Text").Item(1).InnerText=PUMSP_LIMIT_TEXT;

											/*o	If the limit for Uninsured Motorist (BI split Limit) is reject or lower than the Bodily Injury Liability (Split Limit) 
											  o	open up the box for Signature Obtained 
											*/
											VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectSingleNode("IsSigObt").InnerText = "N";


											/*: If Uninsured Motorist (BI Split Limit) is set to reject then do not open 
											o	Underinsured Motorist --PUNCS
											o	Uninsured Motorist PD --UMPD
											*/
											//UMPD
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UMPD')]");

											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}

											//PUNCS
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']");
											if ( node != null )
											{
												VehicleDom.FirstChild.RemoveChild(node);
											}

										}
									}
									else
									{
										//REmove PUMSP --Remove when UMCSL is blank (When CSL is taken) and BI is Blank
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]");
										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}
									}

								}
							
								//in either case,if BI/PD is selected and if state = indiana then PD Limit
								if(StateCd.Trim().ToUpper() =="INDIANA" && Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() != "NO COVERAGE" && Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() != "" && Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() != "BI ONLY" )
								{
									//UMPD
									if(QuoteDom.SelectSingleNode("//PDLimit").InnerText.ToString().Trim()!="")
									{
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//PDLimit").InnerText.ToString().Trim();
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectNodes("Deductible").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//PDDEDUCTIBLE").InnerText.ToString().Trim();
									}
									else
									{
										//6 jan 2009 : If UMCSL not blank and PD is blank than 
										if(QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim() != "")
										{
											//Put Reject
											VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectNodes("Limit/Text").Item(0).InnerText=UMPD_LIMIT_TEXT;
											VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectSingleNode("IsSigObt").InnerText = "N";
										}
										//Commented on 6 jan 2008				
										/*if(QuoteDom.SelectSingleNode("//UMSplit").InnerText=="0") //Reject case
										{
											//Remove UMPD
											//VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UMPD')]").ParentNode);
											//remove UMPD
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']");
											if ( node != null )
											{
												VehicleDom.FirstChild.RemoveChild(node);
											}
										}
										else
                                            VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectNodes("Limit/Text").Item(0).InnerText=UMPD_LIMIT_TEXT;*/
									}
									
								}
								else
								{
									//remove UMPD
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']");
									if ( node != null )
									{
										VehicleDom.FirstChild.RemoveChild(node);
									}
								}
							}
						
						}
						//**********************
					
						//InsuranceAmount (Misc. Extra Equipment)
						/* Misc Extra Equipment Amount will be entered for EECOMP and EECOLL
						 * No amount to be entered for "MEE".
						 */
						
						if ( Node.SelectSingleNode("InsuranceAmount") != null )
						{	
							
							
							double dblInsuranceAmount = Convert.ToDouble(Node.SelectSingleNode("InsuranceAmount").InnerText.Trim()==""?"0.0":Node.SelectSingleNode("InsuranceAmount").InnerText.Trim());
							if ( dblInsuranceAmount <=0 )
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOMP')]").ParentNode);
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOLL')]").ParentNode);

							}
							else
							{
									
								//Extra Equipment-Comprehensive Deductible
								if ( Node.SelectSingleNode("EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE") != null )
								{	
									if ( Node.SelectSingleNode("EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE").InnerText.Trim() == "" ||  Node.SelectSingleNode("EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE").InnerText.Trim() == "0" )
									{
										VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOMP')]").ParentNode);
									}
									else
									{
										VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOMP')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("InsuranceAmount").InnerText.Trim();
										VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOMP')]").ParentNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE").InnerText.ToString().Trim();
									}

								}

								//Extra Equipment-Collision type Deductible
								if ( Node.SelectSingleNode("EXTRAEQUIPCOLLISIONDEDUCTIBLE") != null )
								{	
									//If no coverage then do not save it.
									//string amount = Node.SelectSingleNode("EXTRAEQUIPCOLLISIONDEDUCTIBLE").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");
									//if ( amount != "0" && amount !="") 
									string amount = Node.SelectSingleNode("EXTRAEQUIPCOLLISIONDEDUCTIBLE").InnerText.ToString().Trim().ToUpper().Replace("LIMITED","0");
									if (amount !="" && amount !="0") 
									{
										VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOLL')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = Node.SelectSingleNode("InsuranceAmount").InnerText.Trim();
										VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOLL')]").ParentNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = amount;
								
										if(StateCd.Trim().ToUpper() =="MICHIGAN")
										{
											if(Node.SelectSingleNode("EXTRAEQUIPCOLLISIONTYPE").InnerText.ToString().Trim().ToUpper() == "LIMITED")
                                                VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOLL')]").ParentNode.SelectSingleNode("Deductible/Text").InnerText = Node.SelectSingleNode("COVGCOLLISIONTYPE").InnerText.ToString().Trim();//"0";
											else
												VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOLL')]").ParentNode.SelectSingleNode("Deductible/Text").InnerText = Node.SelectSingleNode("EXTRAEQUIPCOLLISIONTYPE").InnerText.ToString().Trim();
										}

									}
									else
									{
										VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'EECOLL')]").ParentNode);
									}    

								}
							}

						}

						
					
						// ############## END OF VEHICLE COVERAGES ###########################################
					

						AcordDom.SelectSingleNode("//PersAutoLineBusiness").InnerXml = AcordDom.SelectSingleNode("//PersAutoLineBusiness").InnerXml + VehicleDom.OuterXml.ToString().Trim();
					}
					//Vehicle End

					//Driver IMPLEMENTATION	
					XmlNode DriverNode = AcordDom.SelectSingleNode("//PersDriver");
					string DriverBlankXml = DriverNode.OuterXml;
					AcordDom.SelectSingleNode("//PersAutoLineBusiness").RemoveChild(DriverNode);
				
					foreach (XmlNode Node in QuoteDom.SelectNodes("//drivers/*"))
					{
						XmlDocument DriverDom = new XmlDocument();
						DriverDom.LoadXml(DriverBlankXml);
					
						DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = Node.SelectSingleNode("DriverLName").InnerText.ToString().Trim();
						DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = Node.SelectSingleNode("DriverFName").InnerText.ToString().Trim();
						DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/OtherGivenName").InnerText = Node.SelectSingleNode("DriverMName").InnerText.ToString().Trim();

						DriverDom.FirstChild.Attributes["id"].Value ="D" + Node.Attributes["id"].Value.ToString().Trim();

						if (Node.SelectSingleNode("Gender").InnerText.ToString().Trim().ToUpper()=="MALE")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/GenderCd").InnerText = "M";
						else
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/GenderCd").InnerText = "F";

						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/BirthDt").InnerText = Node.SelectSingleNode("BirthDate").InnerText.ToString().Trim();

						if (Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="DIVORCED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "D";
						else if(Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="MARRIED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "M";
						else if(Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="SEPARATED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "P";
						else if(Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="SINGLE")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "S";
						else if(Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="WIDOWED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "W";
						else 
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "S";
					
						if ( Node.SelectSingleNode("DriverIncome") != null )
						{
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/Income").InnerText =  Node.SelectSingleNode("DriverIncome").InnerText.Trim();
						}

						//No of dependants		: Modified on 				
						if (Node.SelectSingleNode("Dependents") != null )
						{
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/NoOfDependants").InnerText =  Node.SelectSingleNode("Dependents").InnerText.Trim();
						}

						DriverDom.SelectSingleNode("PersDriver/DriverInfo/DriversLicense/DriversLicenseNumber").InnerText = Node.SelectSingleNode("DriverLic").InnerText.ToString().Trim();
						DriverDom.SelectSingleNode("PersDriver/PersDriverInfo/GoodDriverInd").InnerText = Node.SelectSingleNode("GoodStudent").InnerText.ToString().Trim();
						DriverDom.SelectSingleNode("PersDriver/PersDriverInfo/DistantStudentInd").InnerText = Node.SelectSingleNode("DistantStudent").InnerText.ToString().Trim();
						DriverDom.SelectSingleNode("PersDriver/PersDriverInfo").Attributes["VehPrincipallyDrivenRef"].Value = "V" + Node.SelectSingleNode("VehicleAssignedAsOperator").InnerText.ToString().Trim();

						/*Importing Operator Type*/
						string[] opCode = Node.SelectSingleNode("VehicleDrivedAsCode").InnerText.ToString().Split('^');
						DriverDom.SelectSingleNode("PersDriver/PersDriverInfo/DriverTypeCd").InnerText = opCode[0];
						
					
						//Premier Driver discount
						if(Node.SelectSingleNode("NoPremDriverDisc").InnerText.ToString().Trim().Equals("True") )
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/PremierDriverDiscount").InnerText = "true";
						else
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/PremierDriverDiscount").InnerText = "false";

						//Safe Driver Discount
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/SafeDriverRenewalDiscount").InnerText = Node.SelectSingleNode("SAFEDRIVER").InnerText.ToString();
											
						//Good Student
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/GoodStudent").InnerText = Node.SelectSingleNode("GoodStudent").InnerText.ToString().Trim();
					
						//College Student
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/CollegeStudent").InnerText = Node.SelectSingleNode("CollegeStudent").InnerText.ToString().Trim();

						//Waive of work loss benefit
						if (Node.SelectSingleNode("WaiveWorkLoss") != null )
						{
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/WaiveWorkLoss").InnerText =  Node.SelectSingleNode("WaiveWorkLoss").InnerText.Trim();
						}

						AcordDom.SelectSingleNode("//PersAutoLineBusiness").InnerXml = AcordDom.SelectSingleNode("//PersAutoLineBusiness").InnerXml + DriverDom.OuterXml.ToString().Trim();
					}
					//Driver End

					//violations
					XmlNode ViolationNode = AcordDom.SelectSingleNode("//PersPolicy/AccidentViolation");
					string ViolationBlankXml = ViolationNode.OuterXml;
					AcordDom.SelectSingleNode("//PersPolicy").RemoveChild(ViolationNode);

					foreach (XmlNode Node in QuoteDom.SelectNodes("//violations/*"))
					{
						XmlDocument ViolationDom = new XmlDocument();
						ViolationDom.LoadXml(ViolationBlankXml);

										
						ViolationDom.FirstChild.Attributes["VehRef"].Value="V" + Node.ParentNode.ParentNode.SelectSingleNode("VehicleAssignedAsOperator").InnerText.ToString().Trim();
						ViolationDom.FirstChild.Attributes["DriverRef"].Value="D" + Node.ParentNode.ParentNode.Attributes["id"].Value.ToString().Trim();
					
						ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDt").InnerText= Node.SelectSingleNode("VIODATE").InnerText.ToString().Trim();
						ViolationDom.FirstChild.SelectSingleNode("DamageTotalAmt/Amt").InnerText= Node.SelectSingleNode("AMOUNTPAID").InnerText.ToString().Trim();
					
						if (Node.SelectSingleNode("Death").InnerText.ToString().Trim().ToLower() == "true")
							ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= "Death";
						else
							ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= "";

						//Passing the Violation CODE

						//string[] ViolationType = Node.SelectSingleNode("VIOLATIONTYPE").InnerText.ToString().Trim().Split(':');
						//ViolationDom.FirstChild.SelectSingleNode("AccidentViolationCd").InnerText= ViolationType[0].ToString().Trim();
						string ViolationType = Node.SelectSingleNode("VIOLATION_CODE").InnerText.ToString();
						ViolationDom.FirstChild.SelectSingleNode("AccidentViolationCd").InnerText = ViolationType.ToString().Trim();

						//Modified on 3 Dec 2007
						//Purpose : Use Violation ID when Violation Code is not present for Some violation which doesnt have ant Violation Types:
						if(ViolationType == "")
							ViolationDom.FirstChild.SelectSingleNode("AccidentViolationId").InnerText = Node.SelectSingleNode("VIOLATIONID").InnerText.ToString();
						//END 


						//Points Assigned
						//Get Assigned Points Modified on 30 Nov 2007
						string pointsAssigned = "";
						if(Node.SelectSingleNode("MVRPOINTS")!=null)
							pointsAssigned  = Node.SelectSingleNode("MVRPOINTS").InnerText.ToString();
						if(ViolationDom.FirstChild.SelectSingleNode("PointsAssigned")!=null)
							ViolationDom.FirstChild.SelectSingleNode("PointsAssigned").InnerText = pointsAssigned.Trim();

						AcordDom.SelectSingleNode("//PersPolicy").InnerXml = AcordDom.SelectSingleNode("//PersPolicy").InnerXml + ViolationDom.OuterXml.ToString().Trim();
					}
					AcordXml=AcordDom.OuterXml;
				}
				return(AcordXml);
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally 
			{}
		}


		public string PrepareCyclAcordXml(string CustomerId,string QuoteId,string AcordXmlPath,string StateCd,string QuoteNumber)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
				DataSet ObjDs;

				string QuickQuoteXml = new ClsQuickQuote().GetQuickQuoteXml(CustomerId,QuoteId);
				QuickQuoteXml = QuickQuoteXml.Replace("H673GSUYD7G3J73UDH","");
				QuickQuoteXml = QuickQuoteXml.Replace("D673GSUYD7G3J73UDD","\"");

				string AcordXml="";
				XmlNode node;
				XmlDocument AcordDom=new XmlDocument();
				XmlDocument QuoteDom=new XmlDocument();

				if (QuickQuoteXml.Trim()!="")
				{
					QuoteDom.LoadXml(QuickQuoteXml);
					AcordDom.Load(AcordXmlPath);
					XmlNode CustomerNode = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/InsuredOrPrincipal");
				
					//Customer Info
					objWrapper.AddParameter("@CustomerID",CustomerId);
					ObjDs = objWrapper.ExecuteDataSet("Proc_GetCustomerDetails");

					string strCustomerStateCode = ObjDs.Tables[0].Rows[0]["CUSTOMER_STATE_CODE"].ToString().Trim();
					string strCustomerStateID = ObjDs.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString().Trim();
					if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11110")
					{
						CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString().Trim();
						CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
					}
					else if (ObjDs.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim()=="11109")
					{
						CustomerNode.SelectSingleNode("GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString().Trim();
						
					}
					XmlNode nodTemp = CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/AddrTypeCd");
					nodTemp.InnerText = "StreetAddress";
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr1").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/Addr2").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/City").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd").InnerText = StateCd;
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateID").InnerText = strCustomerStateID;
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = QuoteDom.SelectSingleNode("//zipCode").InnerText.ToString().Trim();
					CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/CountryCd").InnerText = "us";
					CustomerNode.SelectSingleNode("InsuredOrPrincipalInfo").Attributes["id"].Value = CustomerId;
					//Customer Info End

					AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString().Trim();
					AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialId").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_ID"].ToString().Trim();
					
					
					//Pers Policy Info Start
					XmlNode PersPolicyNode = AcordDom.SelectSingleNode("//PersPolicy");
					PersPolicyNode.SelectSingleNode("ControllingStateProvCd").InnerText = StateCd;
					
					//AppTerms add by kranti
					if(QuoteDom.SelectSingleNode("//policyTerms")!= null)
					PersPolicyNode.SelectSingleNode("ContractTerm/AppTerms").InnerText = QuoteDom.SelectSingleNode("//policyTerms").InnerText ;
					
					PersPolicyNode.SelectSingleNode("ContractTerm/EffectiveDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
					string effDate = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
					DateTime dtExpDate = DateTime.Parse(effDate).AddMonths(int.Parse(QuoteDom.SelectSingleNode("//policyTerms").InnerText.ToString()));
					PersPolicyNode.SelectSingleNode("ContractTerm/ExpirationDt").InnerText = dtExpDate.ToString();
					PersPolicyNode.SelectSingleNode("ContractTerm/ContinuousInd").InnerText = QuoteDom.SelectSingleNode("//yearsContInsured").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("OriginalInceptionDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("QuoteInfo/CompanysQuoteNumber").InnerText = QuoteNumber;
					PersPolicyNode.SelectSingleNode("QuoteInfo/InitialQuoteRequestDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("CreditScoreInfo/CreditScore").InnerText = QuoteDom.SelectSingleNode("//InsuranceScore").InnerText.ToString().Trim();
					if ( QuoteDom.SelectSingleNode("//multiPolicyAutoHomeDiscount") != null )
					{
						PersPolicyNode.SelectSingleNode("MultiPolicy").InnerText = QuoteDom.SelectSingleNode("//multiPolicyAutoHomeDiscount").InnerText.Trim();
					}
					if(QuoteDom.SelectSingleNode("//UmbrellaPolicy")!=null)
					{
						PersPolicyNode.SelectSingleNode("PersonalUmbrellaPolicy").InnerText = QuoteDom.SelectSingleNode("//UmbrellaPolicy").InnerText.Trim();                        
					}
					
					PersPolicyNode.SelectSingleNode("LengthTimeInsured").InnerText = QuoteDom.SelectSingleNode("//yearsContInsured").InnerText.ToString().Trim();
					PersPolicyNode.SelectSingleNode("LengthTimeInsuredWithWolverine").InnerText = QuoteDom.SelectSingleNode("//YEARSCONTINSUREDWITHWOLVERINE").InnerText.ToString().Trim();
	
					//Vehicle IMPLEMENTATION	
					XmlNode VehicleNode = AcordDom.SelectSingleNode("//PersVeh");
					string VehicleBlankXml = VehicleNode.OuterXml;
					AcordDom.SelectSingleNode("//PersAutoLineBusiness").RemoveChild(VehicleNode);
					XmlNode LocationNode = AcordDom.SelectSingleNode("//PersAutoPolicyQuoteInqRq/Location");
					string LocationBlankXml = LocationNode.OuterXml;
					AcordDom.SelectSingleNode("//PersAutoPolicyQuoteInqRq").RemoveChild(LocationNode);
					foreach (XmlNode Node in QuoteDom.SelectNodes("//vehicles/*"))
					{
						XmlDocument VehicleDom = new XmlDocument();
						VehicleDom.LoadXml(VehicleBlankXml);
						XmlDocument LocationDom = new XmlDocument();
						LocationDom.LoadXml(LocationBlankXml);
						VehicleDom.FirstChild.Attributes["id"].Value="V" + Node.Attributes["id"].Value.ToString().Trim();
						VehicleDom.FirstChild.Attributes["LocationRef"].Value="L" + Node.Attributes["id"].Value.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("Manufacturer").InnerText=Node.SelectSingleNode("Make").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("Model").InnerText=Node.SelectSingleNode("Model").InnerText.ToString().Trim();
						//For Itrack 5348 : Added VehAge in INTR XML
						if(VehicleDom.FirstChild.SelectSingleNode("VehAge")!=null)
						{
							VehicleDom.FirstChild.SelectSingleNode("VehAge").InnerText = Node.SelectSingleNode("AGE").InnerText.ToString();
						}
						

						VehicleDom.FirstChild.SelectSingleNode("ModelYear").InnerText=Node.SelectSingleNode("Year").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("VehIdentificationNumber").InnerText=Node.SelectSingleNode("Vin").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("VehTypeCd").InnerText=Node.SelectSingleNode("vehicleType").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("VehSymbolCd").InnerText=Node.SelectSingleNode("Symbol").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("TerritoryCd").InnerText=Node.SelectSingleNode("TerrCodeGaragedLocation").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("GaragingPostalCode").InnerText=Node.SelectSingleNode("ZipCodeGaragedLocation").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("Displacement/NumUnits").InnerText=Node.SelectSingleNode("CC").InnerText.ToString().Trim();
						VehicleDom.FirstChild.SelectSingleNode("VehicleValue").InnerText=Node.SelectSingleNode("AMOUNT").InnerText.ToString().Trim();												

						
						if(VehicleDom.FirstChild.SelectSingleNode("IsCompOnly")!=null)
						{
							if(Node.SelectSingleNode("COMPONLY")!=null)
								VehicleDom.FirstChild.SelectSingleNode("IsCompOnly").InnerText=Node.SelectSingleNode("COMPONLY").InnerText.ToString().Trim();
						}
						
						if(Node.SelectSingleNode("AMOUNT")!=null)
							if(Node.SelectSingleNode("AMOUNT").InnerText!="" && Node.SelectSingleNode("AMOUNT").InnerText.ToString() !="$0" )
								if(double.Parse(Node.SelectSingleNode("AMOUNT").InnerText.ToString())>=40000) 
									PersPolicyNode.SelectSingleNode("AnyCycleover40000").InnerText = "Yes";
								else
									PersPolicyNode.SelectSingleNode("AnyCycleover40000").InnerText = "No";
							else
								PersPolicyNode.SelectSingleNode("AnyCycleover40000").InnerText = "No";
						else
							PersPolicyNode.SelectSingleNode("AnyCycleover40000").InnerText = "No";

					
						//Garaging location Zip code and territory code
						LocationDom.FirstChild.Attributes["id"].Value = "L" + Node.Attributes["id"].Value.ToString().Trim();
						LocationDom.FirstChild.SelectSingleNode("Addr/PostalCode").InnerText = Node.SelectSingleNode("ZipCodeGaragedLocation ").InnerText.ToString().Trim();
						LocationDom.FirstChild.SelectSingleNode("Addr/County").InnerText = Node.SelectSingleNode("TerrCodeGaragedLocation ").InnerText.ToString().Trim();
						LocationDom.FirstChild.SelectSingleNode("Addr/TerritoryCd").InnerText = Node.SelectSingleNode("TerrCodeGaragedLocation ").InnerText.ToString().Trim();
						AcordDom.SelectSingleNode("//PersAutoPolicyQuoteInqRq").InnerXml = AcordDom.SelectSingleNode("//PersAutoPolicyQuoteInqRq").InnerXml + LocationDom.OuterXml.ToString().Trim();
					
						if ( Node.SelectSingleNode("CLASS") != null )
						{
							VehicleDom.FirstChild.SelectSingleNode("RateClassCd").InnerText = Node.SelectSingleNode("CLASS").InnerText.Trim();
						}

						// ############## START OF VEHICLE COVERAGES ###########################################
						
						 
						//Comprehensive
						if ( Node.SelectSingleNode("ComprehensiveDeductible") != null )
						{
							if ( Node.SelectSingleNode("ComprehensiveDeductible").InnerText.ToString().Trim() == "" )
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'OTC')]").ParentNode);
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'OTC')]").ParentNode.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("ComprehensiveDeductible").InnerText.ToString().Trim();
							}
						}

						//Collision
						if ( Node.SelectSingleNode("CollisionDeductible") != null )
						{
							if ( Node.SelectSingleNode("CollisionDeductible").InnerText.ToString().Trim() == "" )
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COLL']"));
							}
							else
							{

								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='COLL']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("CollisionDeductible").InnerText.ToString().Trim();

							}
						}
					
						//Road Service
						if ( Node.SelectSingleNode("RoadService") != null )
						{
							if ( Node.SelectSingleNode("RoadService").InnerText.ToString().Trim() == "" )
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'ROAD')]").ParentNode);
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'ROAD')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("RoadService").InnerText.ToString().Trim();
							}
						}

						

						/////////Residual Bodily Injury Split limit
						if (QuoteDom.SelectSingleNode("//BI").InnerText.ToString().Trim().ToUpper() != "" && QuoteDom.SelectSingleNode("//BI").InnerText.ToString().Trim().ToUpper() != "NO COVERAGE")
						{
							string[] strCoverage = QuoteDom.SelectSingleNode("//BI").InnerText.ToString().Trim().Split('/'); 

							if ( strCoverage != null && strCoverage.Length == 2)
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strCoverage[0];
							
								double dblBISPL = 0;

								if ( strCoverage[1] != null )
								{
									dblBISPL = double.Parse( strCoverage[1] );
								}

								VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText = dblBISPL.ToString();
							}

						}
					
						if (QuoteDom.SelectSingleNode("//BI").InnerText.ToString().Trim().ToUpper() == "" || QuoteDom.SelectSingleNode("//BI").InnerText.ToString().Trim().ToUpper() == "NO COVERAGE")
						{
							VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode);
						}
						
					
						//Property Damage
						if ( Node.SelectSingleNode("//PD") != null )
						{	
							if ( Node.SelectSingleNode("//PD").InnerText.Trim() == "" || Node.SelectSingleNode("//PD").InnerText.Trim().ToUpper() == "NO COVERAGE" )
							{
							
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PD']");

								if ( node != null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PD']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//PD").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");
							}

						}
											
						
						if (QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper() != "" && QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper() != "NO COVERAGE")
						{
							VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RLCSL')]").ParentNode.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper().Replace("NO COVERAGE","0");
						} 
					
						if (QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper() == "" || QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().ToUpper() == "NO COVERAGE")
						{
							VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'RLCSL')]").ParentNode);
						}
						
					
						
						#region Medical Payment-In case of INDIANA
						if(StateCd.Trim().ToUpper() =="INDIANA")
						{
							if ( Node.SelectSingleNode("MedPmType").InnerText.Trim().ToUpper().Equals("NO COVERAGE"))
							{
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM']");
								if ( node != null) 
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
								
							}
							else
							{
								
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM']");
								if ( node != null )
								{	
									node.SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("//MedPm").InnerText.ToString();
									node.SelectSingleNode("Limit/Text").InnerText = QuoteDom.SelectSingleNode("//MedPmType").InnerText.ToString();
									//ADD BY KRANTI FOR PIP DED ON 18APRIL 2007
									//node.SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("//MEDPMDEDUCTIBLE").InnerText.ToString();

								}
							}
						}

						 
						# endregion
						#region Medical Payment in case of MICHIGAN
						if(StateCd.Trim().ToUpper()=="MICHIGAN")
						{
							if (Node.SelectSingleNode("Type") != null)
							{	
								if ( Node.SelectSingleNode("Type").InnerText.Trim() != "No Coverage" && QuoteDom.SelectSingleNode("//MedPmLimit").InnerText.ToString().Trim().ToUpper() != "")
								{
									//$1000 Medical
									string strMEPDMTYPE =  Node.SelectSingleNode("Type").InnerText.Trim();
									if(strMEPDMTYPE == "$1000 Medical")
									{
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM2']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = MEDPM2LimitAmt.ToString();
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM2']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = MEDPM2DeductibleAmt.ToString();
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM1']").SelectSingleNode("Limit/Text").InnerText = MEDPM1LimitText;
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM1']").SelectSingleNode("IsSigObt").InnerText = "Y";
									}
									else
									{
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM1']").SelectSingleNode("Limit/Text").InnerText= Node.SelectSingleNode("Type").InnerText.Trim();
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM1']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//MedPmLimit").InnerText.ToString();										
										VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM1']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("//MEDICALDEDUCTIBLE").InnerText.ToString();
										
										//Removing  MEDPM2 
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM2']");
										if ( node != null)
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}

									}
								}
								else
								{
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM1']");

									if ( node != null)
									{
										VehicleDom.FirstChild.RemoveChild(node);
									}	
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='MEDPM2']");

									if ( node != null)
									{
										VehicleDom.FirstChild.RemoveChild(node);
									}
									
								}
							}
						}
						#endregion//END MICHIGAN 

						if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]")!=null)
						{
							string limitBISPL = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText;
							if(limitBISPL!="") 
							{
								//Remove Follwing
								//PUNCS	Uninsured Motorist (CSL)
								//UNCSL	Underinsured Motorists (CSL) (M-16)
								if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]")!=null)
                                    VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode);
								if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]")!=null)
                                    VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode);


							}
						}

						if ( Node.SelectSingleNode("Type") != null )
						{	
							if ( Node.SelectSingleNode("Type").InnerText.Trim() == "No Coverage" )
							{
								//Remove BI and BIPD nodes
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='BI']"));
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='BIPD']"));
							}
							else if ( StateCd.Trim().ToUpper()=="INDIANA" &&  Node.SelectSingleNode("Type").InnerText.Trim() == "BI" )
							{
								//Remove BI/PD	0 deductible and 300 deductible option
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BIDED')]").ParentNode);
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BIPDDED')]").ParentNode);

							}
							else if ( StateCd.Trim().ToUpper()=="INDIANA" &&  Node.SelectSingleNode("Type").InnerText.Trim() == "BI/PD $0 Deductible" ||Node.SelectSingleNode("Type").InnerText.Trim() == "BI/PD $300 Deductible" )
							{
								//Remove BI	
								XmlNode remNode = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='BI']");
								VehicleDom.FirstChild.RemoveChild(remNode);
							}
						}


						//****** Uninsured and Insured *********
						if ( Node.SelectSingleNode("Type") != null )
						{
							
							/* Check if Type = No coverage.
								 * Remove all the coverage nodes related to uninsured and underinsured motorists.
								 * ELSE
								 * 1. Check the BI split limit for UM and UIM. If BI Split Limit doesnt exist, 
								 *	  then check for CSL.
								 * 2. Check if UnderInsured motorists is checked. If yes then make same entries for UIM as those for UM.
								 */
							if (Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() == "NO COVERAGE" || Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() == "")
							{
								//Remove :PUNCS,PUMSP,UNCSL,UNDSP,UMPD

								//PUNCS		
								if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]")!=null)
								{
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode;
									if(StateCd.Trim().ToUpper()=="INDIANA")
									{
										if ( node != null)
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}
									}
								}
								// Check UnInsured motorists limits - BI split limit .If it exists, then proceed.Remove CSL node.
								if (QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim() != "")
								{
									string[] strUMCoverage = QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim().Split('/'); 
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUMCoverage[0];
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUMCoverage[1];

									//Remove Uninsured CSL - PUNCS
									if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]")!=null)
									{
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode;
										if ( node != null )
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}
									}

									//If State=Indiana then check for Underinsured Motorists 
									if(StateCd.Trim().ToUpper()=="INDIANA")
									{
										//Same entries for Underinsured Motorists if selected :  Modified (Set Y isUnderInsuredMotorists): 27 feb 2006
										if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="Y" || QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="TRUE")
										{
											string[] strUIMCoverage = QuoteDom.SelectSingleNode("//UIMSplit").InnerText.ToString().Trim().Split('/'); 
											VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage[0];
											VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage[1];

											//Remove Underinsured CSL
											VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode);
										}
										else
										{
											//Remove entries relatd to UIM: UNCSL,UNDSP
											//UNCSL
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");

											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}

											//UNDSP
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}
										}

									}
									else
									{
										//Remove entries relatd to UIM: UNCSL,UNDSP
										//UNCSL
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");

										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}

										//UNDSP
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}
									}
								} // Uninsured motorist - CSL 
								else if(QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim() != "")
								{
									// Add UM CSL and remove the other BI split limits for UM and UIM 							
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim();
								
									//Part C - Uninsured Motorists (BI Split Limit)
									VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode);
								
									//Check if State='INDIANA'
									if(StateCd.Trim().ToUpper()=="INDIANA")
									{
										//same entries for Under Insured Motorists if selected //Modified on 27 feb 2006
										if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="Y")
										{
											// Add Underinsured CSL - UNCSL
											string  strUIMCoverage = QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim(); 
											VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage;

											//Remove Underinsured Split Limit
											VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode);
										}
										else
										{
											//Remove entries relatd to UIM: UNCSL,UNDSP
											//UNCSL
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");

											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}

											//UNDSP
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}
										}
									}
									else
									{
										//Remove entries relatd to UIM: UNCSL,UNDSP
										//UNCSL
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");

										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}

										//UNDSP
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}
									}
							
								}

								else
								{
									if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]")!=null)
									{
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode;
										if ( node != null )
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}
									}
									//If UMCSL is blank than Remove PUNCS:Itrack 6766
									if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]")!=null)
									{
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode;
										if ( node != null )
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}
									}

									
								} 
								//Start ---on 26 May 2009
								//UNCSL					
								if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]")!=null)
								{
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode;
									if(StateCd.Trim().ToUpper()=="INDIANA")
									{
										if ( node != null )
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}
									}
								}

								//UNDSP				
								if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]")!=null)
								{
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode;
									if(StateCd.Trim().ToUpper()=="INDIANA")
									{
										if ( node != null )
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}
									}
								}
							
								//UMPD				
								if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UMPD')]")!=null)
								{
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UMPD')]").ParentNode;
									if(StateCd.Trim().ToUpper()=="INDIANA")
									{
										if ( node != null )
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}
									}
								}
								//End Added -------------- on 26 May 2009
							
							}
							else //UM Split Limit --If Type is Other than NO Coverage--Itrack 5712 
							{
								// Check UnInsured motorists limits - BI split limit .If it exists, then proceed.Remove CSL node.
								if (QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim() != "")
								{
									string[] strUMCoverage = QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim().Split('/'); 
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUMCoverage[0];
									VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUMCoverage[1];

									//Remove Uninsured CSL - PUNCS
									if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]")!=null)
									{
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode;
										if ( node != null )
										{
											VehicleDom.FirstChild.RemoveChild(node);
										}
									}

									//If State=Indiana then check for Underinsured Motorists 
									if(StateCd.Trim().ToUpper()=="INDIANA")
									{
										//Same entries for Underinsured Motorists if selected :  Modified (Set Y isUnderInsuredMotorists): 27 feb 2006
										if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="Y" || QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="TRUE")
										{
											string[] strUIMCoverage = QuoteDom.SelectSingleNode("//UIMSplit").InnerText.ToString().Trim().Split('/'); 
											VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage[0];
											VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage[1];

											//Remove Underinsured CSL
											if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]")!=null)
                                                VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode);

											/*1.If the limit for Uninsured Motorist (BI split Limit) is lower than the Bodily Injury Liability (Split Limit) 
											open up the box for Signature Obtained. (SET NO).*/
											if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]")!=null)
											{
												string limitBI = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText.Replace(",","");

												if(limitBI!="")
												{
													if(int.Parse(strUMCoverage[0].Replace(",","")) < int.Parse(limitBI))
													{
														//Open Signature Box
														VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectSingleNode("IsSigObt").InnerText = "N";
													}
												}
											}
										}
										else
										{
											//Remove entries relatd to UIM: UNCSL,UNDSP
											//UNCSL
											if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]")!=null)
											{
												node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");

												if ( node != null && node.ParentNode != null)
												{
													VehicleDom.FirstChild.RemoveChild(node.ParentNode);
												}
											}

											//UNDSP
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}
										}
									}
									else
									{
										//Remove entries relatd to UIM: UNCSL,UNDSP
										//UNCSL
										if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]")!=null)
										{
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");

											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}
										}

										//UNDSP
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}
									}
								} // Uninsured motorist - CSL 
								else  //REJECT CASE UMCSL
								{

									if(QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim() == "")
									{					
										
										if(StateCd.Trim().ToUpper()=="INDIANA")
										{
											//INDIANA  --Delete if UMBREALL POLICY TAKEN : Itrack 6766
											if(QuoteDom.SelectSingleNode("//UmbrellaPolicy").InnerText == "Y")
											{
												//INDIANA  --REMOVE 
												node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]");
												if ( node != null && node.ParentNode != null)
												{
													VehicleDom.FirstChild.RemoveChild(node.ParentNode);
												}

											}
											else//INDIANA  --Import REJECT if UMCSL is BLANK: Itrack 6766
											{
												if( VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']")!=null)
												{
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']").SelectNodes("Limit/Text").Item(0).InnerText = REJECT;
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']").SelectSingleNode("IsSigObt").InnerText = "N";
												}
											}
										}
										else if(StateCd.Trim().ToUpper()=="MICHIGAN")
										{
											//MICHIGAN  --REMOVE 
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]");
											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}

										}
										
										//If UMCSL is REJECT --Under Insured Motorists UNCSL - Reject
										if(StateCd.Trim().ToUpper()=="INDIANA")
										{
											if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="Y")
											{
												//Put Reject UNCSL
												if(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNCSL']")!=null)
												{
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNCSL']").SelectNodes("Limit/Text").Item(0).InnerText=REJECT;
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UNCSL']").SelectSingleNode("IsSigObt").InnerText = "N";	
												}

											}
											else
											{ //Remove -UNCSL
												node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");
												if ( node != null && node.ParentNode != null)
												{
													VehicleDom.FirstChild.RemoveChild(node.ParentNode);
												}
											}
										}
									}
									else
									{
										// Add UM CSL and remove the other BI split limits for UM and UIM 							
										if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]")!=null)
                                            VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim();

										if(StateCd.Trim().ToUpper()=="INDIANA")
										{
											//same entries for Under Insured Motorists if selected
											if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="Y")
											{
												// Add Underinsured CSL - UNCSL
												string  strUIMCoverage = QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim(); 
												VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage;

												//Remove Underinsured Split Limit
												VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode);
											}
											else
											{ //Remove -UNCSL
												node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");
												if ( node != null && node.ParentNode != null)
												{
													VehicleDom.FirstChild.RemoveChild(node.ParentNode);
												}
											}

											//If the limit for Uninsured Motorist CSL lower than the Combined Single Limit (CSL) –then 
											//o open up the box for Signature Obtained 
											int intUMCSL = 0;
											int intCSL = 0;

											if(QuoteDom.SelectSingleNode("//UMCSL")!=null)
											{
												if(QuoteDom.SelectSingleNode("//UMCSL").InnerText!="")
													intUMCSL = int.Parse(QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim().Replace(",",""));
												if(QuoteDom.SelectSingleNode("//CSL").InnerText!="")
													intCSL = int.Parse(QuoteDom.SelectSingleNode("//CSL").InnerText.ToString().Trim().Replace(",",""));
												if(intUMCSL < intCSL)
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUNCS']").SelectSingleNode("IsSigObt").InnerText = "N";
											}
										}
									}

									
								

									//UNDSP
									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");

									if ( node != null && node.ParentNode != null)
									{
										VehicleDom.FirstChild.RemoveChild(node.ParentNode);
									}

									if(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]")!=null)
									{
										string limitBI = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'BISPL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText;
										if(limitBI!="") 
										{
											if(QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim() == "0"
												|| QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim() == "")//Reject Case UMSPLIT
											{
												if(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']")!=null)
												{
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectNodes("Limit/Text").Item(0).InnerText=PUMSP_LIMIT_TEXT;
													VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PUMSP']").SelectNodes("Limit/Text").Item(1).InnerText=PUMSP_LIMIT_TEXT;
												}

											}
										}
										else
										{
											//REmove PUMSP --Remove when UMCSL is blank (When CSL is taken) and BI is Blank
											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]");
											if ( node != null && node.ParentNode != null)
											{
												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
											}
										}
									}
									else
									{
										//REmove PUMSP --Remove when UMCSL is blank (When CSL is taken) and BI is Blank
										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]");
										if ( node != null && node.ParentNode != null)
										{
											VehicleDom.FirstChild.RemoveChild(node.ParentNode);
										}
									}

								}
							}
							
							
							if((StateCd.Trim().ToUpper() == "INDIANA")
								&& 
								(Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() != "NO COVERAGE" &&
								Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() != "" &&
								Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() != "BI ONLY")
								)
							{		
						
								if(Node.SelectSingleNode("PDLimit").InnerText.ToString().Trim()!="")
								{
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("//PDLimit").InnerText.ToString().Trim();	
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText = QuoteDom.SelectSingleNode("//PDDEDUCTIBLE").InnerText.ToString().Trim();	
								}
								else
								{
									//Put Reject UMPD
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectNodes("Limit/Text").Item(0).InnerText=UMPD_LIMIT_TEXT;
									VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']").SelectSingleNode("IsSigObt").InnerText = "N";								
								}
								
							}
							else
							{
								node =	VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='UMPD']");
								if(node !=null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
							# region commented code 
							//							else if(StateCd.Trim().ToUpper() == "MICHIGAN")
							//							{
							//								# region MICHIGAN
							//								/* Check if Type = No coverage.
							//								* Remove all the coverage nodes related to uninsured and underinsured motorists.
							//								* ELSE
							//								* 1. Check the BI split limit for UM and UIM. If BI Split Limit doesnt exist, 
							//								*	  then check for CSL.
							//								* 2. Check if UnderInsured motorists is checked. If yes then make same entries for UIM as those for UM.
							//								*/
							//								if ( Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() == "NO COVERAGE" || Node.SelectSingleNode("Type").InnerText.Trim().ToUpper() == "")
							//								{
							//									//Remove :PUNCS,PUMSP,UNCSL,UNDSP,UMPD
							//
							//									//PUNCS
							//									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode;
							//									if ( node != null )
							//									{
							//										VehicleDom.FirstChild.RemoveChild(node);
							//									}
							//							
							//									//PUMSP
							//									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode;
							//									if ( node != null )
							//									{
							//										VehicleDom.FirstChild.RemoveChild(node);
							//									}
							//							
							//									//UNCSL
							//									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode;
							//									if ( node != null )
							//									{
							//										VehicleDom.FirstChild.RemoveChild(node);
							//									}
							//
							//									//UNDSP
							//									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode;
							//									if ( node != null )
							//									{
							//										VehicleDom.FirstChild.RemoveChild(node);
							//									}
							//							
							//									//UMPD
							//									node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UMPD')]").ParentNode;
							//									if ( node != null )
							//									{
							//										VehicleDom.FirstChild.RemoveChild(node);
							//									}
							//								}
							//								else
							//								{
							//									// Check UnInsured motorists limits - BI split limit .If it exists, then proceed.Remove CSL node.
							//									if (QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim() != "")
							//									{
							//										string[] strUMCoverage = QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim().Split('/'); 
							//										VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUMCoverage[0];
							//										VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUMCoverage[1];
							//
							//										//Remove Uninsured CSL - PUNCS
							//										node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode;
							//										if ( node != null )
							//										{
							//											VehicleDom.FirstChild.RemoveChild(node);
							//										}
							//
							//										//If State=Indiana then check for Underinsured Motorists 
							//										if(StateCd.Trim().ToUpper()=="INDIANA")
							//										{
							//											//Same entries for Underinsured Motorists if selected
							//											if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="Y")
							//											{
							//												string[] strUIMCoverage = QuoteDom.SelectSingleNode("//UMSplit").InnerText.ToString().Trim().Split('/'); 
							//												VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage[0];
							//												VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode.SelectNodes("Limit").Item(1).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage[1];
							//
							//												//Remove Underinsured CSL
							//												VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode);
							//											}
							//											else
							//											{
							//												//Remove entries relatd to UIM: UNCSL,UNDSP
							//												//UNCSL
							//												node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");
							//
							//												if ( node != null && node.ParentNode != null)
							//												{
							//													VehicleDom.FirstChild.RemoveChild(node.ParentNode);
							//												}
							//
							//												//UNDSP
							//												node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");
							//
							//												if ( node != null && node.ParentNode != null)
							//												{
							//													VehicleDom.FirstChild.RemoveChild(node.ParentNode);
							//												}
							//											}
							//										}
							//										else
							//										{
							//											//Remove entries relatd to UIM: UNCSL,UNDSP
							//											//UNCSL
							//											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");
							//
							//											if ( node != null && node.ParentNode != null)
							//											{
							//												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
							//											}
							//
							//											//UNDSP
							//											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");
							//
							//											if ( node != null && node.ParentNode != null)
							//											{
							//												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
							//											}
							//										}
							//									} // Uninsured motorist - CSL 
							//									else if(QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim() != "")
							//									{
							//										// Add UM CSL and remove the other BI split limits for UM and UIM 							
							//										VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUNCS')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim();
							//								
							//										//Part C - Uninsured Motorists (BI Split Limit)
							//										VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'PUMSP')]").ParentNode);
							//								
							//										//Check if State='INDIANA'
							//										if(StateCd.Trim().ToUpper()=="INDIANA")
							//										{
							//											//same entries for Under Insured Motorists if selected
							//											if (QuoteDom.SelectSingleNode("//isUnderInsuredMotorists").InnerText.ToString().Trim().ToUpper()=="TRUE")
							//											{
							//												// Add Underinsured CSL - UNCSL
							//												string  strUIMCoverage = QuoteDom.SelectSingleNode("//UMCSL").InnerText.ToString().Trim(); 
							//												VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]").ParentNode.SelectNodes("Limit").Item(0).SelectSingleNode("FormatCurrencyAmt/Amt").InnerText=strUIMCoverage;
							//
							//												//Remove Underinsured Split Limit
							//												VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]").ParentNode);
							//											}
							//											else
							//											{
							//												//Remove entries relatd to UIM: UNCSL,UNDSP
							//												//UNCSL
							//												node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");
							//
							//												if ( node != null && node.ParentNode != null)
							//												{
							//													VehicleDom.FirstChild.RemoveChild(node.ParentNode);
							//												}
							//
							//												//UNDSP
							//												node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");
							//
							//												if ( node != null && node.ParentNode != null)
							//												{
							//													VehicleDom.FirstChild.RemoveChild(node.ParentNode);
							//												}
							//											}
							//										}
							//										else
							//										{
							//											//Remove entries relatd to UIM: UNCSL,UNDSP
							//											//UNCSL
							//											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNCSL')]");
							//
							//											if ( node != null && node.ParentNode != null)
							//											{
							//												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
							//											}
							//
							//											//UNDSP
							//											node = VehicleDom.FirstChild.SelectSingleNode("Coverage/CoverageCd[contains(.,'UNDSP')]");
							//
							//											if ( node != null && node.ParentNode != null)
							//											{
							//												VehicleDom.FirstChild.RemoveChild(node.ParentNode);
							//											}
							//										}
							//							
							//									}
							//							
							//								}
							//								# endregion
							//							}
							# endregion
						}
						//**********************


						//Motorcycle Trailer (M-49) - EBM49
						//Applied check if 0 is entered ,the coverage will not be selected: 10 feb 2006
						//Appled check for Multiple vehicles : 10 feb 2006
						if ( Node.SelectSingleNode("McycleTrailer") != null )
						{	
							if ( Node.SelectSingleNode("McycleTrailer").InnerText.ToString().Trim() == "" || Node.SelectSingleNode("McycleTrailer").InnerText.ToString().Trim().ToUpper() == "NO COVERAGE" || Node.SelectSingleNode("McycleTrailer").InnerText.ToString().Trim() == "0")
							{
							
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EBM49']");

								if ( node != null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
							else
							{
								//VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EBM49']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("McycleTrailer").InnerText.ToString().Trim();
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EBM49']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText = Node.SelectSingleNode("McycleTrailer").InnerText.ToString().Trim();
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EBM49']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("ComprehensiveDeductible").InnerText.ToString().Trim();
												
							}

						}
						//End of  Motorcycle Trailer (M-49) - EBM49

						//Motorcycle Trailer - M-49 Collision  - CEBM49
						//Added on 23 Feb 2007
						if ( Node.SelectSingleNode("McycleTrailerCollision") != null )
						{
							if ( Node.SelectSingleNode("McycleTrailerCollision").InnerText.ToString().Trim() == "" || Node.SelectSingleNode("McycleTrailerCollision").InnerText.ToString().Trim() == "N")
							{
								VehicleDom.FirstChild.RemoveChild(VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='CEBM49']"));
							}
							else
							{

								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='CEBM49']").SelectSingleNode("Deductible/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("CollisionDeductible").InnerText.ToString().Trim();

							}
						}
						//


						//Additional Physical Damage Coverage (M-14):PDC14
						//Applied check if 0 is entered: 10 feb 2006
						//Applied check for Multiple vehicles 10 feb 2006
						//Chnage Code PDC14 previous code : EBM14
						if ( Node.SelectSingleNode("AddlPD") != null )
						{	
							if ( Node.SelectSingleNode("AddlPD").InnerText.ToString().Trim() == "" || Node.SelectSingleNode("AddlPD").InnerText.ToString().Trim().ToUpper() == "NO COVERAGE" || Node.SelectSingleNode("AddlPD").InnerText.ToString().Trim() == "0" )
							{
							
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PDC14']");

								if ( node != null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
							else
							{
								//VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EBM14']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//AddlPD").InnerText.ToString().Trim();
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='PDC14']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("AddlPD").InnerText.ToString().Trim();
							}

						}
						//End of  Additional Physical Damage Coverage (M-14):EBM14


						//Helmet & Riding Apparel Coverage (M-15):EBM15
						if ( Node.SelectSingleNode("//Helmet") != null )
						{	
							if ( Node.SelectSingleNode("//Helmet").InnerText.Trim() == "" || Node.SelectSingleNode("//Helmet").InnerText.Trim().ToUpper() == "NO COVERAGE" )
							{
							
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EBM15']");

								if ( node != null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
							else
							{
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EBM15']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//Helmet").InnerText.ToString().Trim();
							}

						}
						//End of Helmet & Riding Apparel Coverage (M-15)

						// Extra Equipment
						//Applied chek for 0 : 10 feb 2006
						//Applied check for multiple vehicles : 10 feb 2006
						if ( Node.SelectSingleNode("INCREASEDVALUE") != null )
						{	
							if ( Node.SelectSingleNode("INCREASEDVALUE").InnerText.ToString().Trim() == "" || Node.SelectSingleNode("INCREASEDVALUE").InnerText.ToString().Trim() == "0")
							{
							
								node = VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EE']");

								if ( node != null)
								{
									VehicleDom.FirstChild.RemoveChild(node);
								}
							}
							else
							{
								//VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EE']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=QuoteDom.SelectSingleNode("//INCREASEDVALUE").InnerText.ToString().Trim();
								VehicleDom.FirstChild.SelectSingleNode("Coverage[CoverageCd='EE']").SelectSingleNode("Limit/FormatCurrencyAmt/Amt").InnerText=Node.SelectSingleNode("INCREASEDVALUE").InnerText.ToString().Trim();
							}

						}
						//End of Extra Equipment

						//end of coverages************************************************************

						AcordDom.SelectSingleNode("//PersAutoLineBusiness").InnerXml = AcordDom.SelectSingleNode("//PersAutoLineBusiness").InnerXml + VehicleDom.OuterXml.ToString().Trim();
					}
					//Vehicle End

					//Driver IMPLEMENTATION	
					XmlNode DriverNode = AcordDom.SelectSingleNode("//PersDriver");
					string DriverBlankXml = DriverNode.OuterXml;
					AcordDom.SelectSingleNode("//PersAutoLineBusiness").RemoveChild(DriverNode);
				
					foreach (XmlNode Node in QuoteDom.SelectNodes("//drivers/*"))
					{
						XmlDocument DriverDom = new XmlDocument();
						DriverDom.LoadXml(DriverBlankXml);
						DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = Node.SelectSingleNode("DriverLName").InnerText.ToString().Trim();
						DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = Node.SelectSingleNode("DriverFName").InnerText.ToString().Trim();
						DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/OtherGivenName").InnerText = Node.SelectSingleNode("DriverMName").InnerText.ToString().Trim();

						DriverDom.FirstChild.Attributes["id"].Value ="D" + Node.Attributes["id"].Value.ToString().Trim();

						if (Node.SelectSingleNode("Gender").InnerText.ToString().Trim().ToUpper()=="MALE")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/GenderCd").InnerText = "M";
						else
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/GenderCd").InnerText = "F";

						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/BirthDt").InnerText = Node.SelectSingleNode("BirthDate").InnerText.ToString().Trim();

						/*if (Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="MARRIED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "M";
						else
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "S";*/
						/*MaritalStatus*/
						if (Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="DIVORCED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "D";
						else if(Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="MARRIED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "M";
						else if(Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="SEPARATED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "P";
						else if(Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="SINGLE")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "S";
						else if(Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="WIDOWED")
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "W";
						else 
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "S";
						/*MaritalStatus*/
					
						if ( Node.SelectSingleNode("DriverIncome") != null )
						{
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/Income").InnerText =  Node.SelectSingleNode("DriverIncome").InnerText.Trim();
						}

						DriverDom.SelectSingleNode("PersDriver/PersDriverInfo").Attributes["VehPrincipallyDrivenRef"].Value = "V" + Node.SelectSingleNode("VehicleAssignedAsOperator").InnerText.ToString().Trim();

						//Driver Type
						if(Node.SelectSingleNode("DRIVERTYPECODE")!=null)
							DriverDom.SelectSingleNode("PersDriver/PersDriverInfo/OperatesCd").InnerText = Node.SelectSingleNode("DRIVERTYPECODE").InnerText.ToString().Trim();

						/*Operator Type*/
						string[] opCode = Node.SelectSingleNode("VehicleDrivedAsCode").InnerText.ToString().Split('^');
						DriverDom.SelectSingleNode("PersDriver/PersDriverInfo/DriverTypeCd").InnerText = opCode[0];
						//DriverDom.SelectSingleNode("PersDriver/PersDriverInfo/DriverTypeCd").InnerText = Node.SelectSingleNode("DRIVERTYPECODE").InnerText.ToString().Trim();
						/*Operator Type*/

						//NoCyclEndmt And LicUnder3Yrs are rest 
					
						//Premier Driver discount
						//DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/PremierDriverDiscount").InnerText = Node.SelectSingleNode("NoPremDriverDisc").InnerText.ToString().Trim();
					
						//Good Student
						//DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/GoodStudent").InnerText = Node.SelectSingleNode("GoodStudent").InnerText.ToString().Trim();
					
						//College Student
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/CollegeStudent").InnerText = Node.SelectSingleNode("COLLEGESTUDENT").InnerText.ToString().Trim();

						//No Cycle Endorsement :  27 feb 2006
						if(Node.SelectSingleNode("NoCyclEndmt")!=null)
						{
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/NoCycleEndorsement").InnerText = Node.SelectSingleNode("NoCyclEndmt").InnerText.ToString().Trim();
						}

						//No Cycle Endorsement :  27 feb 2006
						if(Node.SelectSingleNode("CYCLEWITHYOU")!=null)
						{
							DriverDom.SelectSingleNode("PersDriver/DriverInfo/QuestionAnswer/CycleWithYou").InnerText = Node.SelectSingleNode("CYCLEWITHYOU").InnerText.ToString().Trim();
						}



						AcordDom.SelectSingleNode("//PersAutoLineBusiness").InnerXml = AcordDom.SelectSingleNode("//PersAutoLineBusiness").InnerXml + DriverDom.OuterXml.ToString().Trim();
					}
					//Driver End

					//violations
					XmlNode ViolationNode = AcordDom.SelectSingleNode("//PersPolicy/AccidentViolation");
					string ViolationBlankXml = ViolationNode.OuterXml;
					AcordDom.SelectSingleNode("//PersPolicy").RemoveChild(ViolationNode);

					foreach (XmlNode Node in QuoteDom.SelectNodes("//violations/*"))
					{
						XmlDocument ViolationDom = new XmlDocument();
						ViolationDom.LoadXml(ViolationBlankXml);
					
						ViolationDom.FirstChild.Attributes["VehRef"].Value="V" + Node.ParentNode.ParentNode.SelectSingleNode("VehicleAssignedAsOperator").InnerText.ToString().Trim();
						ViolationDom.FirstChild.Attributes["DriverRef"].Value="D" + Node.ParentNode.ParentNode.Attributes["id"].Value.ToString().Trim();
					
						ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDt").InnerText= Node.SelectSingleNode("VIODATE").InnerText.ToString().Trim();
						ViolationDom.FirstChild.SelectSingleNode("DamageTotalAmt/Amt").InnerText= Node.SelectSingleNode("AMOUNTPAID").InnerText.ToString().Trim();
						//Passing the Violation CODE of Violation 
						//string[] ViolationType = Node.SelectSingleNode("VIOLATIONTYPE").InnerText.ToString().Trim().Split(':');
						//ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= ViolationType[1].ToString().Trim();
						//ViolationDom.FirstChild.SelectSingleNode("AccidentViolationCd").InnerText= ViolationType[0].ToString().Trim();
						string ViolationType = Node.SelectSingleNode("VIOLATION_CODE").InnerText.ToString();
						ViolationDom.FirstChild.SelectSingleNode("AccidentViolationCd").InnerText = ViolationType.ToString().Trim();

						//Modified on 3 Dec 2007
						//Purpose : Use Violation ID when Violation Code is not present for Some violation which doesnt have ant Violation Types:
						if(ViolationType == "")
							ViolationDom.FirstChild.SelectSingleNode("AccidentViolationId").InnerText = Node.SelectSingleNode("VIOLATIONID").InnerText.ToString();

						//Points Assigned
						//Get Assigned Points Modified on 30 Nov 2007
						string pointsAssigned = "";
						if(Node.SelectSingleNode("MVRPOINTS")!=null)
							pointsAssigned  = Node.SelectSingleNode("MVRPOINTS").InnerText.ToString();
						if(ViolationDom.FirstChild.SelectSingleNode("PointsAssigned")!=null)
							ViolationDom.FirstChild.SelectSingleNode("PointsAssigned").InnerText = pointsAssigned.Trim();


						if (Node.SelectSingleNode("Death").InnerText.ToString().Trim().ToUpper() == "Y")
							ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= "Death";
						else
							ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= "";

						AcordDom.SelectSingleNode("//PersPolicy").InnerXml = AcordDom.SelectSingleNode("//PersPolicy").InnerXml + ViolationDom.OuterXml.ToString().Trim();
					}

				
					AcordXml=AcordDom.OuterXml;

				}
				return(AcordXml);
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		}


		public string PrepareBoatAcordXml(string CustomerId,string QuoteId,string AcordXmlPath,string StateCd,string QuoteNumber)
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
				XmlNode CustomerNode = AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/PersAutoPolicyQuoteInqRq/InsuredOrPrincipal");
				
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
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd").InnerText = StateCd;
				
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/StateProvCd").InnerText = StateCd;
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = ObjDs.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString().Trim();
				//CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/PostalCode").InnerText = QuoteDom.SelectSingleNode("//zipCode").InnerText.ToString().Trim();
				CustomerNode.SelectSingleNode("GeneralPartyInfo/Addr/CountryCd").InnerText = "us";
								
				CustomerNode.SelectSingleNode("InsuredOrPrincipalInfo").Attributes["id"].Value = CustomerId;

				//Customer Info End
				//Agency Info
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/WatercraftPolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialName").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString().Trim();
				//add by kranti on 17th april 2007 for agency id
				AcordDom.SelectSingleNode("ACORD/InsuranceSvcRq/WatercraftPolicyQuoteInqRq/Producer/GeneralPartyInfo/NameInfo/CommlName/CommercialId").InnerText = ObjDs.Tables[0].Rows[0]["AGENCY_ID"].ToString().Trim();
				
				//Agency Info End

				//Pers Policy Info Start
				XmlNode PersPolicyNode = AcordDom.SelectSingleNode("//PersPolicy");
				PersPolicyNode.SelectSingleNode("ControllingStateProvCd").InnerText = StateCd;

				PersPolicyNode.SelectSingleNode("ContractTerm/EffectiveDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
				string effDate = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
				DateTime dtExpDate = DateTime.Parse(effDate).AddMonths(int.Parse(QuoteDom.SelectSingleNode("//POLICYTERMS").InnerText.ToString()));
				PersPolicyNode.SelectSingleNode("ContractTerm/ExpirationDt").InnerText = dtExpDate.ToString();
				
				PersPolicyNode.SelectSingleNode("OriginalInceptionDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();
				
				PersPolicyNode.SelectSingleNode("QuoteInfo/CompanysQuoteNumber").InnerText = QuoteNumber;
				PersPolicyNode.SelectSingleNode("QuoteInfo/InitialQuoteRequestDt").InnerText = QuoteDom.SelectSingleNode("//QUOTEEFFDATE").InnerText.ToString().Trim();

				PersPolicyNode.SelectSingleNode("CreditScoreInfo/CreditScore").InnerText = QuoteDom.SelectSingleNode("//INSURANCESCORE").InnerText.ToString().Trim();
				
				//BOAT IMPLEMENTATION	
				XmlNode BoatNode = AcordDom.SelectSingleNode("//WatercraftLineBusiness/Watercraft");
				string BoatBlankXml = BoatNode.OuterXml;
				AcordDom.SelectSingleNode("//WatercraftLineBusiness").RemoveChild(BoatNode);
				
				foreach (XmlNode Node in QuoteDom.SelectNodes("//BOATS/*"))
				{
					XmlDocument BoatDom = new XmlDocument();
					BoatDom.LoadXml(BoatBlankXml);

					BoatDom.FirstChild.Attributes["id"].Value="W" + Node.Attributes["id"].Value.ToString().Trim();

					BoatDom.FirstChild.SelectSingleNode("Make").InnerText=Node.SelectSingleNode("MANUFACTURER").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Model").InnerText=Node.SelectSingleNode("MODEL").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Year").InnerText=Node.SelectSingleNode("YEAR").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("SerialNumber").InnerText=Node.SelectSingleNode("SERIALNUMBER").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("WaterUnitTypeCd").InnerText=Node.SelectSingleNode("BOATTYPECODE").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Length/NumUnits").InnerText=Node.SelectSingleNode("LENGTH").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Horsepower/NumUnits").InnerText=Node.SelectSingleNode("HORSEPOWER").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("WatersNavigatedCd").InnerText=Node.SelectSingleNode("WATERSCODE").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("Speed/Speed").InnerText=Node.SelectSingleNode("CAPABLESPEED").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("PresentValueAmt/Amt").InnerText=Node.SelectSingleNode("MARKETVALUE").InnerText.ToString().Trim();
					//BoatDom.FirstChild.SelectSingleNode("WeightCapacity/NumUnits").InnerText=Node.SelectSingleNode("WEIGHT").InnerText.ToString().Trim();
					BoatDom.FirstChild.SelectSingleNode("HullMaterialTypeCd").InnerText=Node.SelectSingleNode("CONSTRUCTIONCODE").InnerText.ToString().Trim();

					AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml = AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml + BoatDom.OuterXml.ToString().Trim();
				}
				//Boat End

				//Driver IMPLEMENTATION	
				XmlNode DriverNode = AcordDom.SelectSingleNode("//WatercraftLineBusiness/PersDriver");
				string DriverBlankXml = DriverNode.OuterXml;
				AcordDom.SelectSingleNode("//WatercraftLineBusiness").RemoveChild(DriverNode);
				
				foreach (XmlNode Node in QuoteDom.SelectNodes("//OPERATORS/*"))
				{
					XmlDocument DriverDom = new XmlDocument();
					DriverDom.LoadXml(DriverBlankXml);
					DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/Surname").InnerText = Node.SelectSingleNode("OPERATORLNAME").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/GivenName").InnerText = Node.SelectSingleNode("OPERATORFNAME").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/GeneralPartyInfo/NameInfo/PersonName/OtherGivenName").InnerText = Node.SelectSingleNode("OPERATORMNAME").InnerText.ToString().Trim();

					DriverDom.FirstChild.Attributes["id"].Value ="D" + Node.Attributes["id"].Value.ToString().Trim();

					if (Node.SelectSingleNode("Gender").InnerText.ToString().Trim().ToUpper()=="MALE")
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/GenderCd").InnerText = "M";
					else
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/GenderCd").InnerText = "F";

					DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/BirthDt").InnerText = Node.SelectSingleNode("BirthDate").InnerText.ToString().Trim();

					if (Node.SelectSingleNode("MaritalStatus").InnerText.ToString().Trim().ToUpper()=="MARRIED")
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "M";
					else
						DriverDom.SelectSingleNode("PersDriver/DriverInfo/PersonInfo/MaritalStatusCd").InnerText = "S";
					
					DriverDom.SelectSingleNode("PersDriver/PersDriverInfo").Attributes["VehPrincipallyDrivenRef"].Value = "W" + Node.SelectSingleNode("BOATASSIGNEDASOPERATOR").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/PersDriverInfo/DriverTypeCd").InnerText = Node.SelectSingleNode("BOATDRIVEDAS").InnerText.ToString().Trim();
					
					
					DriverDom.SelectSingleNode("PersDriver/QuestionAnswer/PowerSquadronCourse").InnerText = Node.SelectSingleNode("POWERSQUADRONCOURSE").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/QuestionAnswer/CoastGuardAuxService").InnerText = Node.SelectSingleNode("COASTGUARDAUXILARYCOURSE").InnerText.ToString().Trim();
					DriverDom.SelectSingleNode("PersDriver/QuestionAnswer/FiveYearsOperatorExp").InnerText = Node.SelectSingleNode("HAS_5_YEARSOPERATOREXPERIENCE").InnerText.ToString().Trim();
					
					
					AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml = AcordDom.SelectSingleNode("//WatercraftLineBusiness").InnerXml + DriverDom.OuterXml.ToString().Trim();
				}
				//Driver End

				//violations
				XmlNode ViolationNode = AcordDom.SelectSingleNode("//PersPolicy/AccidentViolation");
				string ViolationBlankXml = ViolationNode.OuterXml;
				AcordDom.SelectSingleNode("//PersPolicy").RemoveChild(ViolationNode);

				foreach (XmlNode Node in QuoteDom.SelectNodes("//VIOLATIONS/*"))
				{
					XmlDocument ViolationDom = new XmlDocument();
					ViolationDom.LoadXml(ViolationBlankXml);
					
					ViolationDom.FirstChild.Attributes["VehRef"].Value="W" + Node.ParentNode.ParentNode.SelectSingleNode("BOATASSIGNEDASOPERATOR").InnerText.ToString().Trim();
					ViolationDom.FirstChild.Attributes["DriverRef"].Value="D" + Node.ParentNode.ParentNode.Attributes["id"].Value.ToString().Trim();
					
					ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDt").InnerText= Node.SelectSingleNode("VIODATE").InnerText.ToString().Trim();
					ViolationDom.FirstChild.SelectSingleNode("DamageTotalAmt/Amt").InnerText= Node.SelectSingleNode("AMOUNTPAID").InnerText.ToString().Trim();
					
					string[] ViolationType = Node.SelectSingleNode("VIOLATIONTYPE").InnerText.ToString().Trim().Split(':');
					
					//ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= ViolationType[1].ToString().Trim();
					ViolationDom.FirstChild.SelectSingleNode("AccidentViolationCd").InnerText= ViolationType[0].ToString().Trim();

					if (Node.SelectSingleNode("Death").InnerText.ToString().Trim().ToUpper() == "Y")
						ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= "Death";
					else
						ViolationDom.FirstChild.SelectSingleNode("AccidentViolationDesc").InnerText= "";

					AcordDom.SelectSingleNode("//PersPolicy").InnerXml = AcordDom.SelectSingleNode("//PersPolicy").InnerXml + ViolationDom.OuterXml.ToString().Trim();
				}
				AcordXml=AcordDom.OuterXml;
			}
			return(AcordXml);
		}

		public string FetchValuesXML(int intState, int intLOB,string strZIP,string effdate)
		{
			DataSet dsQQXMLValues = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"select terr from MNT_TERRITORY_CODES where ZIP = "+strZIP+" and LOBID = "+intLOB+" and STATE= "+intState+" and "+ "'"+System.Convert.ToDateTime(effdate)+"'"+" BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') AND AUTO_VEHICLE_TYPE IS NULL  ");
			if (dsQQXMLValues.Tables[0].Rows.Count > 0)
				return(dsQQXMLValues.Tables[0].Rows[0][0].ToString().Trim());
			else
				return("");
		
		}
		/// <summary>
		/// Return MVR points 
		/// </summary>
		/// <param name="arrViolationCode"></param>
		/// <param name="intState"></param>
		/// <param name="intLOB"></param>
		/// <returns></returns>
		public int GetMVRPointsForACORD( string arrViolationCode,int intState,int intLOB)
		{
			DataSet dsMVRPointsForACORD = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,System.Data.CommandType.Text,"SELECT MVR_POINTS  FROM MNT_VIOLATIONS WHERE VIOLATION_CODE = '"+arrViolationCode+"' and LOB = "+intLOB+" and STATE= "+intState+"");
			if (dsMVRPointsForACORD.Tables[0].Rows.Count > 0)
				return(Convert.ToInt32(dsMVRPointsForACORD.Tables[0].Rows[0][0].ToString().Trim()));
			else  
				return(0);		
		}  
	}
}

