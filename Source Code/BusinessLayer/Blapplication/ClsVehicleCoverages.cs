/******************************************************************************************
<Author				: -	Pradeep
<Start Date			: -	21/02/2005 11:59:31 AM
<End Date			: -	
<Description		: -	Class file for Policy Coverages
<Review Date		: -  
<Reviewed By		: - 	
*******************************************************/

using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application;
using Cms.Model.Policy.Homeowners ;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;
using System.Web.UI.WebControls;
using System.Collections;

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsVehicleCoverages.
	/// </summary>
	public class ClsVehicleCoverages :  ClsCoverages 
	{

		#region Private Instance Variables
		private			bool		boolTransactionRequired;	
		private int thiscreatedby;
		private int thisModifiedby;
		
		DataSet dsPolicyVehicleInfo;
				
		DataTable dtVehicle = null;
		DataTable dtMotorcycleInfo = null;
		DataTable dtPolicyMotorcycleInfo = null;
		bool AutoRemoveXMLLoaded;
		
		int StateID = 0;
		string VehicleUse = "0";
		string VehicleType = "0";
		internal string strLOB = "";

		#endregion

		private const string AutoRuleXML = "/cmsweb/support/coverages/AutoDefaultCoverageRule.xml";
		private const string MotorRuleXML ="/cmsweb/support/coverages/MotorDefaultCoverageRule.xml";
		private const string AviationRuleXML ="/cmsweb/support/coverages/AviationDefaultCoverageRule.xml";

		#region Auto Constructor
		public ClsVehicleCoverages()
		{
			boolTransactionRequired = base.TransactionLogRequired;
			if(IsEODProcess )
			{
				string strTemp = AutoRuleXML.Replace("/",@"\");
				filePath = WebAppUNCPath + strTemp;
				filePath=  System.IO.Path.GetFullPath(filePath);
			}
			else
			{
				filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + AutoRuleXML );
			}
			RuleDoc = new XmlDocument();
			RuleDoc.Load(filePath); 
			AutoRemoveXMLLoaded=false;
			
		}
		#endregion

		#region Motor Constructor
		public ClsVehicleCoverages(string tempStr)
		{
			strLOB=tempStr;
			boolTransactionRequired = base.TransactionLogRequired;
			if(IsEODProcess )
			{
				string strTemp = MotorRuleXML.Replace("/",@"\");
				filePath = WebAppUNCPath + strTemp;
				filePath=  System.IO.Path.GetFullPath(filePath);
			}
			else
			{
				if(tempStr=="AVIATION")
				filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + AviationRuleXML  );
				else
				filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + MotorRuleXML  );
			}
			RuleDoc = new XmlDocument();
			RuleDoc.Load(filePath); 
			AutoRemoveXMLLoaded=false;
			
		}
		#endregion



		#region Public Properties
		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}
			set
			{
				boolTransactionRequired=value;
			}
		}
		public int createdby
		{
			
			set
			{
				thiscreatedby=value;
			}
			get
			{
				return thiscreatedby;
			}
		}

		public int modifiedby
		{
			
			set
			{
				thisModifiedby=value;
			}
			get
			{
					return thisModifiedby;
			}
		}
		#endregion
		
	
		#region GetAutoCoveragesToRemoveFromXML Function
		/// <summary>
		/// Returns an XML with coverages to remove read from XML file
		/// </summary>
		/// <param name="stateID"></param>
		/// <param name="product"></param>
		/// <returns></returns>
		public string GetAutoCoveragesToRemoveFromXML(int stateID, string VehicleUse, string vehicleType,string strIsSuspended, DataSet objDataSet)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/AutoCoverages.xml");
			StringBuilder sbXML = new StringBuilder();
			StringBuilder sbRemove = new StringBuilder();
			StringBuilder sbRemoveLimit=new StringBuilder();


			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			
			XmlDocument doc = new XmlDocument();
			doc.Load(filePath);
			
			if ( VehicleUse.Trim() == "" ) 
			{
				VehicleUse = "0";
			}

			XmlNode node = doc.SelectSingleNode("Auto/State[@ID=" +  stateID.ToString() + "]");
	
			if ( node == null ) return "";

			XmlNode vehUseNode = node.SelectSingleNode("VehicleUse[@ID=" + VehicleUse + "]");

			if ( vehUseNode == null ) return "";
			
			//Get coverages to remove based on Commercial/Personal
			XmlNode removeNode = vehUseNode.SelectSingleNode("Remove/Coverages");

			if ( removeNode != null )
			{
				XmlNodeList removeList = removeNode.SelectNodes("Coverage");
				
				//Loop thru each coveages to remove
				foreach(XmlNode remNode in removeList)
				{
					string coverageID = remNode.Attributes["ID"].Value;
					string covCode = remNode.Attributes["Code"].Value;
				
					if ( sbRemove.ToString() == "" )
					{
						sbRemove.Append(coverageID);
					}
					else
					{
						sbRemove.Append("," + coverageID );
					}
				}
			}
			//End of Commercial/Personal

			//Get coverages to remove based on Vehicle type: Trailer, Suspended etc etc
			XmlNode vehTypeNode = vehUseNode.SelectSingleNode("VehicleType[@ID=" + vehicleType + "]");
			
			if ( vehTypeNode != null)
			{
				//Get coverages to remove based on Commercial/Personal
				removeNode = vehTypeNode.SelectSingleNode("Remove/Coverages");

				if ( removeNode != null )
				{
					XmlNodeList removeList = removeNode.SelectNodes("Coverage");
					
					//Loop thru each coverages to remove
					foreach(XmlNode remNode in removeNode)
					{
						string coverageID = remNode.Attributes["ID"].Value;
						string covCode = remNode.Attributes["Code"].Value;
				
						if ( sbRemove.ToString() == "" )
						{
							sbRemove.Append(coverageID);
						}
						else
						{
							sbRemove.Append("," + coverageID );
						}
					}
				}
				//////
				//Get coverages to remove based on Is Suspended
				string IsSuspended=strIsSuspended==""?"N":strIsSuspended;
				XmlNode IsSuspendedNode = vehTypeNode.SelectSingleNode("IsSuspended[@Type=" + IsSuspended + "]");

				if ( IsSuspendedNode != null )
				{
					//Get coverages to remove based on Commercial/Personal
					removeNode = IsSuspendedNode.SelectSingleNode("Remove/Coverages");
					if (removeNode!=null)
					{
						//Loop thru each coverages to remove
						foreach(XmlNode remNode in removeNode)
						{
							string coverageID = remNode.Attributes["ID"].Value;
							string covCode = remNode.Attributes["Code"].Value;
				
							if ( sbRemove.ToString() == "" )
							{
								sbRemove.Append(coverageID);
							}
							else
							{
								sbRemove.Append("," + coverageID );
							}
						}
					}
				}
				/////
			}
			//End of specific vehicle type

			//Get the coverages to remove from the master dataset and create XML
			DataRow[] drRemove = dtCoverage.Select("COV_ID IN (" + sbRemove.ToString() + ")");
			
			foreach(DataRow dr1 in drRemove)
			{
				string covID = dr1["COV_ID"].ToString();
				string mand = dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
				string covCode = dr1["COV_CODE"].ToString();
				
				sbXML.Append("<Coverage COV_ID='" + covID + "' COV_CODE='" + covCode + "' Remove='Y' Mandatory='N'>");
				sbXML.Append("</Coverage>");
			}

			//start
		 
			
			//Get All The Coverage whose Limit,Deductible Is to Be removed  based on Commercial/Personal
			XmlNodeList removeNodeLimit = vehUseNode.SelectNodes("Remove/CoverageLimit");
			foreach(XmlNode removeNodeList in removeNodeLimit )
			{
				string covID = removeNodeList.Attributes["COV_ID"].Value;
				XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
				//Loop thru each coveages to remove
				
				foreach(XmlNode remNode in removeLimitList)
				{
					string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
					string Amount = remNode.Attributes["Amount"].Value;
					if ( sbRemoveLimit.ToString() == "" )
					{
						sbRemoveLimit.Append(strLIMIT_DEDUC_ID);
					}
					else
					{
						sbRemoveLimit.Append("," + strLIMIT_DEDUC_ID );
					}
					
					
				
            	}
				DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit.ToString() + ")");
				foreach(DataRow drLimit in drLimits)
				{	
					if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
					{
						
							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
						
					}
				}
				sbXML.Append("</Coverage>");
			}

			//start
			if(sbRemoveLimit.ToString() != "")
			{
				sbRemoveLimit.Remove(0,sbRemoveLimit.Length-1);
			}

			vehTypeNode = vehUseNode.SelectSingleNode("VehicleType[@ID=" + vehicleType + "]");
			if ( vehTypeNode != null ) 
			{
			
				//Get All The Coverage whose Limit,Deductible Is to Be removed  based on Vehicle Type
				removeNodeLimit = vehTypeNode.SelectNodes("Remove/CoverageLimit");
				foreach(XmlNode removeNodeList in removeNodeLimit )
				{
					string covID = removeNodeList.Attributes["COV_ID"].Value;
					XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
					//Loop thru each coveages to remove
				
					foreach(XmlNode remNode in removeLimitList)
					{
						string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
						string Amount = remNode.Attributes["Amount"].Value;
						if ( sbRemoveLimit.ToString() == "" )
						{
							sbRemoveLimit.Append(strLIMIT_DEDUC_ID);
						}
						else
						{
							sbRemoveLimit.Append("," + strLIMIT_DEDUC_ID );
						}
					
					
				
					}
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit.ToString() + ")");
					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
						{
						
							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
						
						}
					}
					sbXML.Append("</Coverage>");
				}
			}
//end
			
			return sbXML.ToString();
		}
		#endregion
	
		#region GetCoveragesToRemove Function
		
		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetCoveragesToRemove(int customerID,int appID, int appVersionID, int vehicleID, DataSet objDataSet)
		{
			string newUsed = "";
			string strPersVehicleType = "";
			string strVehType = "";
			string strIs_Suspended = "";

			if ( dtVehicle != null )
			{
				if ( dtVehicle.Rows.Count > 0 )
				{
					if ( dtVehicle.Rows[0]["IS_NEW_USED"] != DBNull.Value )
					{
						newUsed = dtVehicle.Rows[0]["IS_NEW_USED"].ToString();
					}
					if ( dtVehicle.Rows[0]["VEHICLE_TYPE_PER"]  != DBNull.Value )
					{
						strPersVehicleType = dtVehicle.Rows[0]["VEHICLE_TYPE_PER"].ToString();
					}
					if ( dtVehicle.Rows[0]["USE_VEHICLE"]  != DBNull.Value )
					{
						strVehType = dtVehicle.Rows[0]["USE_VEHICLE"].ToString();
					}
					if ( dtVehicle.Rows[0]["IS_SUSPENDED"]  != DBNull.Value )
					{
						strIs_Suspended = dtVehicle.Rows[0]["IS_SUSPENDED"].ToString();
					}
				}
			}

			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
			
//			//Remove $50 Limit option from (Part E - Oher than collision option) for Application section///
//			DataRow[] drOTC = dtCoverage.Select("COV_CODE='COMP'");
//					
//			if ( drOTC != null  && drOTC.Length > 0 )
//			{
//						
//				string covID = drOTC[0]["COV_ID"].ToString();
//
//				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory='" + drOTC[0]["IS_MANDATORY"].ToString() + "'>");
//					
//				DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID);
//
//				foreach(DataRow drLimit in drLimits)
//				{	
//					if ( drLimit["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
//					{
//						if ( drLimit["LIMIT_DEDUC_AMOUNT"].ToString() == "50" )
//						{
//							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
//							sbXML.Append("</Limit>");	
//						}
//
//					}
//				}
//				
//				sbXML.Append("</Coverage>");
//				
//			}
//			//***End of removal of limits
			string xml=GetAutoCoveragesToRemoveFromXML(StateID,VehicleUse,VehicleType,strIs_Suspended,objDataSet);

			sbXML.Append(xml);
			//*************************************************************************************
			
			/*
			strPersVehicleType = dtVehicle.Rows[0]["VEHICLE_TYPE_PER"].ToString();
				
			/////If Veh type is Suspended Comp only, remove everything except Comprehensive
			if ( strPersVehicleType == "11618" )
			{
				foreach(DataRow dr in dtCoverage.Rows )
				{
					string covID = dr["COV_ID"].ToString();
					string covCode = dr["COV_CODE"].ToString();
						
					if ( covCode != "COMP" && covCode != "OTC")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE='COLL' Remove='Y' Mandatory='" + dr["IS_MANDATORY"].ToString() + "'>");
						sbXML.Append("</Coverage>");
					}
				}
			}

			//If Vehicle type is Trailer, Remove all coverages:******************
			//except: 
			//Part E - Other Than Collision (Comprehensive) 
			//Part E - Collision 
			//Miscellaneous Extra Equipment (A-15) 
			//Extra Equipment-Comprehensive Deductible 
			//Extra Equipment-Collision type Deductible 
			if ( strPersVehicleType == "11337" )
			{
				//DataRow[] dr = dtCoverage.Select("COV_CODE NOT IN ( 'COMP' ,'OTC','COLL','MEE','EECOMP','EECOLL')");
				DataRow[] dr = dtCoverage.Select("COV_CODE NOT IN ('OTC', 'COMP', 'COLL', 'EECOMP','EECOLL')"); // Set Other Than Collision (Comprehensive) in traile : 10 feb 2006
					
				foreach(DataRow dr1 in dr )
				{
					string covID = dr1["COV_ID"].ToString();
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE='COLL' Remove='Y' Mandatory='N'>");
					sbXML.Append("</Coverage>");
				}
					
			}
				
			//If vehicle type is Motorhome, remove
			//Rental reimbursement
			if ( strPersVehicleType == "11336" )
			{
				//Rental reimbursement
				DataRow[] dr = dtCoverage.Select("COV_CODE='RREIM'");
					
				if ( dr != null  && dr.Length > 0 )
				{
					string covID = dr[0]["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE='RREIM' Remove='Y' Mandatory='" + dr[0]["IS_MANDATORY"].ToString() + "'>");
					sbXML.Append("</Coverage>");
				}
			}

			
			//*************************************************************************************
				
			//Remove coverages based on Commercial or personal
			
			//Commercial
			if ( strVehType == "11333" )
			{
				//Road
				DataRow[] dr = dtCoverage.Select("COV_CODE='ROAD'");
					
				if ( dr != null  && dr.Length > 0 )
				{
					string covID = dr[0]["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE='ROAD' Remove='Y' Mandatory='" + dr[0]["IS_MANDATORY"].ToString() + "'>");
					sbXML.Append("</Coverage>");
				}

				//Rental reimbursement
				dr = dtCoverage.Select("COV_CODE='RREIM'");
					
				if ( dr != null  && dr.Length > 0 )
				{
					string covID = dr[0]["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE='RREIM' Remove='Y' Mandatory='" + dr[0]["IS_MANDATORY"].ToString() + "'>");
					sbXML.Append("</Coverage>");
				}

				//Part D - Underinsured Motorists (BI Split Limits) ,UNDSP
				dr = dtCoverage.Select("COV_CODE='UNDSP'");
					
				if ( dr != null  && dr.Length > 0 )
				{
					string covID = dr[0]["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE='UNDSP' Remove='Y' Mandatory='" + dr[0]["IS_MANDATORY"].ToString() + "'>");
					sbXML.Append("</Coverage>");
				}

				//Part C - Underinsured Motorists (CSL)   UNCSL 
				dr = dtCoverage.Select("COV_CODE='UNCSL'");
					
				if ( dr != null  && dr.Length > 0 )
				{
					string covID = dr[0]["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE='UNCSL' Remove='Y' Mandatory='" + dr[0]["IS_MANDATORY"].ToString() + "'>");
					sbXML.Append("</Coverage>");
				}


			}
			
			/*Other than Commercial
			Remove A-80 and A-85
			
			 * 54		HA	Hired Automobiles - Cost of hire basis (A-85)
				248		EBENO	Employers Non-Ownership Liability (A-80)
				297		EBENO	Employers Non-Ownership Liability (A-80)
				298		EBHA	Hired Automobiles – Cost of Hire Basis (A-85)

			 
			if ( strVehType == "11332" || strVehType == "")
			{
				DataRow[] dr = dtCoverage.Select("COV_ID IN (54, 248, 297,298)");
				
				foreach(DataRow drRem in dr)
				{
					string covID = drRem["COV_ID"].ToString();

					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE='UNCSL' Remove='Y' Mandatory='" + drRem["IS_MANDATORY"].ToString() + "'>");
					sbXML.Append("</Coverage>");
				}

			}
			*/


			if ( sbXML.ToString() != "" )
			{		
				return "<Coverages>" + sbXML.Append("</Coverages>").ToString();
			}

			return "";


		}
		#endregion

		#region GetMandatoryCoverages Function 
		/// <summary>
		/// Sets each coverage to mandatory or optional based on business rules
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="objDataSet"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		public string GetMandatoryCoverages(int customerID,int appID, int appVersionID, 
			int vehicleID, DataSet objDataSet)
		{
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtAppInfo = objDataSet.Tables[2];

			string newUsed = "";
			string strPersVehicleType = "";
			string strVehType = "";
			string strVehUse  = "";
			int stateID = 0;
			string strcalledfrom="";
			
			if ( dtAppInfo != null && dtAppInfo.Rows.Count > 0 )
			{
				if ( dtAppInfo.Rows[0]["STATE_ID"] != DBNull.Value )
				{
					stateID = Convert.ToInt32(dtAppInfo.Rows[0]["STATE_ID"]);
				}
			}

			if ( dtVehicle != null )
			{
				if ( dtVehicle.Rows.Count > 0 )
				{
					if ( dtVehicle.Rows[0]["IS_NEW_USED"] != DBNull.Value )
					{
						newUsed = dtVehicle.Rows[0]["IS_NEW_USED"].ToString();
					}
					if(dtVehicle.Rows[0]["VEHICLE_TYPE_PER"] != null)
					{
						if ( dtVehicle.Rows[0]["VEHICLE_TYPE_PER"]  != DBNull.Value )
						{
							strPersVehicleType = dtVehicle.Rows[0]["VEHICLE_TYPE_PER"].ToString();
						}
						else if(dtVehicle.Rows[0]["VEHICLE_TYPE_PER"]  != DBNull.Value)
						{
							strPersVehicleType = dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString();

						}
					}
					if ( dtVehicle.Rows[0]["USE_VEHICLE"]  != DBNull.Value )
					{
						strVehType = dtVehicle.Rows[0]["USE_VEHICLE"].ToString();
					}
					if ( dtVehicle.Rows[0]["VEHICLE_USE"]  != DBNull.Value )
					{
						strVehUse = dtVehicle.Rows[0]["VEHICLE_USE"].ToString();
					}
					if ( dtVehicle.Rows[0]["CALLEDFROM"]  != DBNull.Value )
					{
						strcalledfrom = dtVehicle.Rows[0]["CALLEDFROM"].ToString();
					}

				}
			}

			StringBuilder sbXML = new StringBuilder();

			sbXML.Append("<Coverages>");
			
			

			foreach(DataRow dr in dtCoverage.Rows )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				if (dr["IS_MANDATORY"].ToString()=="1" && int.Parse(covID)>=10000) //don't override is_Mandatory if coverage is added through Maintenance Setup Screen  ///by Pravesh
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
				else	
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='N'>");
				
				sbXML.Append("</Coverage>");
				
				//If Vehicle type is Customized van truck, Motorhome or PPA, or Commercial
				//For Michigan state, Part B - Property Protection Insurance is mandatory

				if ( strPersVehicleType == "11335" || strPersVehicleType == "11336" || strPersVehicleType == "11334" || strVehType == "11333")
				{
					if ( covID == "117" && stateID == 22)
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}

					
				}
				
				if ( strPersVehicleType == "11868" ||  strPersVehicleType == "11869")
				{
					if ( covID=="1000" || covID=="1001" || covID=="1008" || covID=="1009") 
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}
				}
				//Customized Van or truck 49,251 mandatory
				if ( strPersVehicleType == "11335" )
				{
					if ( covID == "49" || covID == "251")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}
				}
				if(strVehUse == "")
				{
					if(covID== "1014")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");

					}
				}

				if(strVehType == "11333")
				{
					if(covID =="1019" || covID =="1015")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");

					}
				}
				
				if(covID =="1011" || covID =="1018")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");

					}

			


				

				
				//For Michigan, PIP is mandatory
				//116		PIP	Personal Injury Protection	22 is mandatory
					
					/* Commented by Asfa(13-June-2008) - iTrack #4283
					
					if ( covID == "116" || covID =="997"  || covID=="998" || covID =="1003" || covID == "1004" || covID =="1006"  || covID == "1007" ||
						covID =="1012" || covID == "1013" || covID == "1014" || covID=="1002" || covID == "1010" ||  covID=="1016"  || covID=="1014" || covID=="1017" || covID=="1005" || covID =="1013" || covID=="1015" ||
						covID== "1020" )
					*/
					if ( covID == "116" || covID =="997"  || covID=="998" || covID =="1003" || covID == "1004" || covID =="1006"  || covID == "1007" ||
					covID =="1012" || covID == "1013" || covID == "1014" || covID=="1002" || covID == "1010" || covID=="1014" || covID=="1017" || covID=="1005" || covID =="1013" || covID=="1015" ||
					covID== "1020" )
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}
							
			}	
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
			
		}
		
		

		#endregion
		
		#region GetVehiclesToCopy Function ----- Fetch Vehicles For A Application Except Current
		/// <summary>
		/// Gets a list of vehicles to copy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		public static DataTable GetVehiclesToCopy(int customerID, 
			int appID, int appVersionID, int vehicleID,string calledFrom)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@APP_ID",appID);
			sqlParams[1] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			sqlParams[4] = new SqlParameter("@CALLED_FROM",calledFrom);

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetVehicles",sqlParams);

			return ds.Tables[0];
		}
		#endregion

		#region GetCoveragesToCopy Function ---- Fetch Coverages For A Specified Vehicle
		/// <summary>
		/// Gets all the coverages from the selected vehicle 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public static DataSet GetCoveragesToCopy(int customerID, 
			int appID, int appVersionID, int vehicleID,string calledFrom)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@APP_ID",appID);
			sqlParams[1] = new SqlParameter("@APP_VERSION_ID",appVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			sqlParams[4] = new SqlParameter("@CALLED_FROM",calledFrom);

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetAPP_VEHICLE_COVERAGES_COPY",sqlParams);

			return ds;
		}
		#endregion

		#region GetVehicleCoverages Function For Copy
		/// <summary>
		/// Filters coverages from the passed in DataSet
		/// </summary>
		/// <param name="dsCoverages"></param>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public DataSet GetVehicleCoverages(DataSet dsCoverages,int customerID, 
			int appID, int appVersionID, int vehicleID)
		{
			//fetching XML string with all coverages to remove
			
			//Populate instance data/////
			DataTable dtState = dsCoverages.Tables[2];
			dtVehicle= dsCoverages.Tables[3];
			
			if ( dtState != null && dtState.Rows.Count > 0 )
			{
				if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
				{
					this.StateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
				}
			}
			
			if ( dtVehicle != null && dtVehicle.Rows.Count > 0 )
			{
				if ( dtVehicle.Rows[0]["VEHICLE_TYPE_PER"] != DBNull.Value )
				{
					this.VehicleType = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE_PER"]);
				}

				if ( dtVehicle.Rows[0]["USE_VEHICLE"] != DBNull.Value )
				{
					this.VehicleUse = Convert.ToString(dtVehicle.Rows[0]["USE_VEHICLE"]);
				}
				if(VehicleUse == "11333")//Commercial
				{
					if ( dtVehicle.Rows[0]["VEHICLE_TYPE_COM"] != DBNull.Value )
					{
						this.VehicleType = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE_COM"]);
					}
				}

			}
			//End of populate data

			
			string covXML=this.GetCoveragesToRemove(customerID,
				appID,
				appVersionID,
				vehicleID,dsCoverages
				);	
			
			string covMandatoryXML=this.GetMandatoryCoverages(customerID,
				appID,
				appVersionID,
				vehicleID,dsCoverages
				);	
			
			//if XML string is not blank		
			if(covXML!="" )
			{
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}
			
			//function call to update mandatory field
			if ( covMandatoryXML != "" )
			{
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,covMandatoryXML);			
			}

			return dsCoverages;             
		}
		#endregion 

		#region CopyVehicleCoverages Function
		/// <summary>
		/// Copies coverages from one vehicle to another
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int CopyVehicleCoverages(ArrayList alNewCoverages,int customerID, int appID,
			int appVersionID,int vehicleID,string strOldXML,int userID)
		{
			string	strDelProc =	"Proc_DeleteAPP_VEHICLE_COVERAGES_ALL";
			string	strStoredProc =	"Proc_SAVE_VEHICLE_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			//Delete linked Endorsements and coverages///////////////////////////
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			
			try
			{
				objWrapper.ExecuteNonQuery(strDelProc);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			objWrapper.ClearParameteres();
			///////////////////////////
			//Update/Copy policy Level coverages/////////////////////////// By Pravesh on 13 April 09 itrak 5460
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			
			try
			{
				objWrapper.ExecuteNonQuery("Proc_COPY_POLICY_LEVEL_COVERAGES_APP");
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			objWrapper.ClearParameteres();
			///////////////////////////

			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
		
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];

					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID );
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					//Modified by Praveen Kumar(19-01-2009):Itrack 5281
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					//END PRAVEEN KUMAR
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@SIGNATURE_OBTAINED",objNew.SIGNATURE_OBTAINED);
					//ADDED BY PRAVEEN KUMAR(19-01-2009):Itrack 5281
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUC_ID));

					//END PRAVEEN KUMAR
					objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);
				
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.COVERAGE_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						//ADDED PRAVEEN KUMAR(4-02-2009):ITRACK 5281
						objTransactionInfo.RECORDED_BY  =  userID;		
						//END
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
					}
				
					if ( strTranXML.Trim() == "" )
					{
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
				
					}
					else
					{
						
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.RECORDED_BY  =  userID;		//PRAVEEN KUMAR
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages copied.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}

				
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
			}
			catch(Exception ex)
			{
				//tran.Rollback();
				//conn.Close();
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			//tran.Commit();
			//conn.Close();
			UpdateCoveragesByRuleApp(objWrapper,customerID ,appID ,appVersionID,RuleType.RiskDependent,vehicleID);
			objWrapper.ClearParameteres();
			UpdateCoveragesByRuleApp(objWrapper,customerID ,appID ,appVersionID,RuleType.MiscEquipment,vehicleID);
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}
		#endregion
		
		#region GetVehCoverages Function -- Fetch All Coverages for vehicle regardless of business rules
		/// <summary>
		/// Gets vehicle coverages from database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="appType"></param>
		/// <returns>Dataset Containing all coverages without filter</returns>
		private  DataSet GetVehCoverages(int customerID, int appID, 
			int appVersionID, int vehicleID, string appType)
		{
			string	strStoredProc =	"Proc_GetAPP_VEHICLE_COVERAGES_NEW";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objWrapper.AddParameter("@APP_TYPE","N");
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			return ds;
		}
		#endregion
		
		#region GetVehicleCoverages Function --(Page) Fetch filtered Coverages based on business rules
		/// <returns>Dataset having Coverages applicable to Application as per Business rules </returns>
		public DataSet GetVehicleCoverages(int customerID, int appID, int appVersionID, int vehicleID, string appType)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;
			
			//Gets the master list of coverages from database
			dsCoverages = this.GetVehCoverages(customerID,
				appID,
				appVersionID,
				vehicleID,appType
				);	
			
			//Populate instance data /////
			DataTable dtState = dsCoverages.Tables[2];
			dtVehicle = dsCoverages.Tables[3];
			
			if ( dtState != null && dtState.Rows.Count > 0 )
			{
				if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
				{
					this.StateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
				}
			}
			
			if ( dtVehicle != null && dtVehicle.Rows.Count > 0 )
			{
				if ( dtVehicle.Rows[0]["VEHICLE_TYPE_PER"] != DBNull.Value )
				{
					this.VehicleType = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE_PER"]);
				}

				if ( dtVehicle.Rows[0]["USE_VEHICLE"] != DBNull.Value )
				{
					this.VehicleUse = Convert.ToString(dtVehicle.Rows[0]["USE_VEHICLE"]);
				}
				if(VehicleUse == "11333")//Commercial
				{
					if ( dtVehicle.Rows[0]["VEHICLE_TYPE_COM"] != DBNull.Value )
					{
						this.VehicleType = Convert.ToString(dtVehicle.Rows[0]["VEHICLE_TYPE_COM"]);
					}
				}
			}
			//End of populate data

			//fetching XML string with all coverages to remove
						
			string covXML=this.GetCoveragesToRemove(customerID,
				appID,
				appVersionID,
				vehicleID,dsCoverages
				);	
			
			string covMandatoryXML=this.GetMandatoryCoverages(customerID,
				appID,
				appVersionID,
				vehicleID,dsCoverages
				);	

		
			//if XML string is not blank		
			if(covXML!="" )
			{
				Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			
	
			

				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}

			//function call to update mandatory field
			if ( covMandatoryXML != "" )
			{
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,covMandatoryXML);			
			}

			return dsCoverages;             
		}


		#endregion
		
		#region SaveVehicleCoverages Function
		/// <summary>
		/// Saves the coverages for Auto and Motor in the database
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <param name="strCustomInfo"></param>
		/// <returns></returns>
		public int SaveVehicleCoverages(ArrayList alNewCoverages,string strOldXML, string strCustomInfo)
		{
			
			string	strStoredProc =	"Proc_SAVE_VEHICLE_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[16];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;
			int vehicleID = 0;

			try
			{
				//Loop thru aray list
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					
					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					vehicleID = objNew.RISK_ID;

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@SIGNATURE_OBTAINED",objNew.SIGNATURE_OBTAINED);
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);

					string strTranXML = "";
				
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;

					if ( objNew.ACTION == "I" )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverage added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverage updated.";
						if(root != null)
						{
							strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
							if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
								sbTranXML.Append(strTranXML);
						}
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
//						objWrapper.ExecuteNonQuery(strStoredProc);
//						objWrapper.ClearParameteres();
					}
					objWrapper.ExecuteNonQuery(strStoredProc);
					objWrapper.ClearParameteres();
				
				}

				objWrapper.ClearParameteres();

				//Delete Coverages/////////////////////////////////////
				//string strCustomInfo="Following coverages have been deleted:",str="";
				//strCustomInfo = "";
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					
					Cms.Model.Application.ClsCoveragesInfo objDelete = (ClsCoveragesInfo)alNewCoverages[i];
					
					if ( objDelete.ACTION == "D" )
					{
						objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objDelete.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objDelete.APP_VERSION_ID);
						objWrapper.AddParameter("@VEHICLE_ID",objDelete.RISK_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objDelete.COVERAGE_ID);
						//str+=";" + objDelete.COV_DESC;
						//Delete the coverage
						objWrapper.ExecuteNonQuery("Proc_DeleteAPP_VEHICLE_COVERAGES");

						//Get Tran log
						objDelete.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDelete);
						string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

						sbTranXML.Append(strTranXML);

						objWrapper.ClearParameteres();
					}
				}
				//////////////////////////////////////////////////////////

				sbTranXML.Append("</root>");

				//if(sbTranXML.ToString()!="<root></root>")
				//	strCustomInfo+=";Following coverages have been added/updated";

				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;

                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1497", "");//"Vehicle coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//////////////////
				
				//Update non-linked endorsements////////
				objWrapper.ClearParameteres();

//				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
//				objWrapper.AddParameter("@APP_ID",appID);
//				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
//				objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
//		
//				objWrapper.ExecuteNonQuery("Proc_UPDATE_VEHICLE_ENDORSEMENTS");
//				objWrapper.ClearParameteres();
				//End of non-linked/////////////////////

				//Update Policy Coverages///////////////
				if(alNewCoverages.Count > 0)
				{
					UpdateApplicationCoverages(alNewCoverages,objWrapper,vehicleID,customerID,appID,appVersionID);
					/////////////////////////////////////////
				
					//Update relevant endorsements
					UpdateEndorsmentApp(objWrapper,customerID,appID,appVersionID,vehicleID);
				}
				//UpdateApplicationEndorsements(customerID,appID,appVersionID,vehicleID,objWrapper);
				////////////////////
				
			}
			catch(Exception ex)
			{
				//tran.Rollback();
				//conn.Close();
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			
			//tran.Commit();
			//conn.Close();
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}
		#endregion

		
		#region SaveAcordVehicleCoverages Function
		/// <summary>
		/// Saves coveraes and endorsements from Quick quote
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="objWrapper"></param>
		/// <returns></returns>
		public int SaveAcordVehicleCoverages(ArrayList alNewCoverages,DataWrapper objWrapper)
		{
			
			string	strStoredProc =	"Proc_SAVE_VEHICLE_COVERAGES_ACORD";

			//DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
		
			for(int i = 0; i < alNewCoverages.Count; i++ )
			{
				Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					
				objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
				objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
				objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
				objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
				objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
				objWrapper.AddParameter("@COVERAGE_CODE",objNew.COVERAGE_CODE);
				objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
				objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
				objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
				objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
				objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
				objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
				objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
				objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
				
				objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
				objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
				objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
				objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
				objWrapper.AddParameter("@SIGNATURE_OBTAINED",objNew.SIGNATURE_OBTAINED);
				objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);

				SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID ,SqlDbType.Int ,ParameterDirection.ReturnValue );

				objWrapper.ExecuteNonQuery(strStoredProc);
				int COVERAGE_CODE_ID =int.Parse(objSqlParameter.Value.ToString());
				objWrapper.ClearParameteres();
				if (TransactionLogRequired)
				{
					string strTranXML = "";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					strTranXML = objBuilder.GetTransactionLogXML(objNew);
					
					//Replace values with -1
					strTranXML = strTranXML.Replace("-1","");	

					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = objNew.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY = objNew.CREATED_BY;
					objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
					//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Vehicle coverage added from Quick Quote.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					
					objWrapper.ExecuteNonQuery(objTransactionInfo);
					objWrapper.ClearParameteres();
				}

				objWrapper.ClearParameteres();

				

			}
			
			

//			if ( alNewCoverages.Count > 0 )
//			{
//				Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[0];
//
//				//Update relevant endorsements
//				UpdateApplicationEndorsements(objNew.CUSTOMER_ID,objNew.APP_ID,objNew.APP_VERSION_ID,objNew.RISK_ID,objWrapper);
//				////////////////////
//				
//				//Update relevant endorsements
//				UpdateApplicationEndorsements(objNew.CUSTOMER_ID,objNew.APP_ID,objNew.APP_VERSION_ID,objNew.RISK_ID,objWrapper);
//				////////////////////
//				
//				objWrapper.ClearParameteres();
//
//				//Update non-linked endorsements////////
//			
//				objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
//				objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
//				objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
//				objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
//		
//				objWrapper.ExecuteNonQuery("Proc_UPDATE_VEHICLE_ENDORSEMENTS");
//				objWrapper.ClearParameteres();
//				//End of non-linked/////////////////////

//			}

			
			return 1;
		}
		

		#endregion

		
		#region UpdateApplicationCoverages
		/// <summary>
		/// Updates the application level coverages for all vehicle in this application except Suspended
		/// and Trailer
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="objWrapper"></param>
		/// <param name="vehicleID"></param>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		private int UpdateApplicationCoverages(ArrayList alNewCoverages, DataWrapper objWrapper, int vehicleID, int customerID , int appID, int appVersionID)
		{
			objWrapper.ClearParameteres();
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);

			//Get all other vehicles except "Suspended comp only" and Trailer
			DataSet dsVehicles = objWrapper.ExecuteDataSet("Proc_Get_VEHICLES_FOR_APP_COVERAGES");
			
			objWrapper.ClearParameteres();

			DataTable dtVehicle = dsVehicles.Tables[0];

			if ( dtVehicle.Rows.Count == 0 ) return 1;

			//For each vehicle other than current in the application
			//Loop thru the App level coveages and update each Vehicle
			/*
				 127		BISPL	Part A - Bodily Injury Liability (Split Limit)	14
					207		BISPL	Part A - Bodily Injury Liability (Split Limit)	22
					114		BISPL	Part A - Bodily Injury Liability (Split Limit)	22
					115		PD	Part A - Property Damage Liability	22
					4		PD	Part A - Property Damage Liability	14
					208		PD	Part A - Property Damage Liability 	22
					128		PD	Part A - Property Damage Liability 	14
					113		RLCSL	Part A - Residual Liability CSL (BI and PD)	22
					126		RLCSL	Part A - Single Limits Liability (CSL)	14
					206		RLCSL	Part A - Single Limits Liability (CSL)	22
					1		SLL	Part A - Single Limits Liability CSL (BI and PD)	14
					116		PIP	Part A – Personal Injury Protection	22
					6		MP	Part B - Medical Payments	14
					117		PPI	Part B - Property Protection Insurance	22
					34		UNDSP	Part C - Underinsured Motorists (BI Split Limit)	14
					214		UNDSP	Part C - Underinsured Motorists (BI Split Limit) (M-16)	14
					14		UNCSL	Part C - Underinsured Motorists (CSL)	14
					263		UNCSL	Part C - Underinsured Motorists (CSL)	14
					133		UNCSL	Part C - Underinsured Motorists (CSL) (M-16)	14
					132		PUMSP	Part C - Uninsured Motorists (BI Split Limit)	14
					212		PUMSP	Part C - Uninsured Motorists (BI Split Limit)	22
					12		PUMSP	Part C - Uninsured Motorists (BI Split Limit)	14
					120		PUMSP	Part C - Uninsured Motorists (BI Split Limits)	22
					119		PUNCS	Part C - Uninsured Motorists (CSL)	22
					9		PUNCS	Part C - Uninsured Motorists (CSL)	14
					211		PUNCS	Part C - Uninsured Motorists (CSL)	22
					131		PUNCS	Part C - Uninsured Motorists (CSL)	14
					36		UMPD	Part C - Uninsured Motorists (PD) (A - 21)	14
					199		UMPD	Part C - Uninsured Motorists (PD) (M-16)	14

				*/
			foreach(DataRow drVeh in dtVehicle.Rows)
			{
				int newVehicleID = Convert.ToInt32(drVeh["VEHICLE_ID"]);

				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					string strCovCode = objNew.COVERAGE_CODE;

					if ( objNew.COVERAGE_TYPE == "PL")
					{
						if ( objNew.ACTION == "I" || objNew.ACTION == "U" )
						{
							//Add Linked endorsements////////////////////////////////
							objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
							objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
							objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
							objWrapper.AddParameter("@VEHICLE_ID",newVehicleID);
							objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
							objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
							objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
							objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
							objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
							objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
							objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
							objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
							objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
							objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
							objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
							objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
							objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
							objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
							objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
							objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
							objWrapper.AddParameter("@SIGNATURE_OBTAINED",objNew.SIGNATURE_OBTAINED);
							objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
							objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
							objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);
							
						
							objWrapper.ExecuteNonQuery("Proc_SAVE_VEHICLE_COVERAGES");
							objWrapper.ClearParameteres();
						}

						if ( objNew.ACTION == "D")
						{
							objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
							objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
							objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
							objWrapper.AddParameter("@VEHICLE_ID",newVehicleID);
							objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
							
							//Delete the coverage
							objWrapper.ExecuteNonQuery("Proc_DeleteAPP_VEHICLE_COVERAGES_BY_ID");

							objWrapper.ClearParameteres();
						}

					}

				}

			}
			
			return 1;
		}

		#endregion
		
		#region Get/add/update Aviation Coverages Functions 
		/// <summary>
		/// Used while adding/updating/getting Coverages for Aviation
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetAviationCoverages(int customerID, int appID, int appVersionID, int vehicleID)
		{
			string	strStoredProc =	"Proc_GetAPP_AVIATION_VEHICLES_COVERAGES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objWrapper.AddParameter("@APP_TYPE","N");
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			return ds;
		}
		public DataSet GetPolicyAviationCoverages(int customerID, int polID, int polVersionID, int vehicleID, string appType)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;

			string	strStoredProc =	"Proc_GetPOL_AVIATION_VEHICLES_COVERAGES";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objWrapper.AddParameter("@APP_TYPE","N");
            dsCoverages = objWrapper.ExecuteDataSet(strStoredProc);
			return dsCoverages;
			
		}

		/// <summary>
		/// Saves the coverages for Aviation in the database
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <param name="strCustomInfo"></param>
		/// <returns></returns>
		public int SaveAviationVehicleCoverages(ArrayList alNewCoverages,string strOldXML, string strCustomInfo)
		{
			
			string	strStoredProc =	"Proc_SAVE_AVIATION_VEHICLE_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[16];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			if ( strOldXML != "" )
			{
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;
			int vehicleID = 0;

			try
			{
				//Loop thru aray list
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objNew = (ClsCoveragesInfo)alNewCoverages[i];
					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					vehicleID = objNew.RISK_ID;

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);
					
					objWrapper.AddParameter("@RATE",DefaultValues.GetDoubleNullFromNegative(objNew.RATE) );
					objWrapper.AddParameter("@COVERAGE_TYPE_ID",objNew.COVERAGE_TYPE_ID);

					string strTranXML = "";
				
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;

					if ( objNew.ACTION == "I" )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/aviation/VehicleCoverageDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Aviation Vehicle coverage added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/aviation/VehicleCoverageDetails.aspx.resx");
						objTransactionInfo.TRANS_DESC		=	"Aviation Vehicle coverage updated.";
						if(root != null)
						{
							strTranXML = this.GetTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
							if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
								sbTranXML.Append(strTranXML);
						}
					}
					objWrapper.ExecuteNonQuery(strStoredProc);
					objWrapper.ClearParameteres();
				}

				objWrapper.ClearParameteres();

				//Delete Coverages/////////////////////////////////////
				//strCustomInfo = "";
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Application.ClsCoveragesInfo objDelete = (ClsCoveragesInfo)alNewCoverages[i];
					if ( objDelete.ACTION == "D" )
					{
						objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objDelete.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objDelete.APP_VERSION_ID);
						objWrapper.AddParameter("@VEHICLE_ID",objDelete.RISK_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objDelete.COVERAGE_ID);
						//Delete the coverage
						objWrapper.ExecuteNonQuery("Proc_DeleteAPP_AVIATION_VEHICLE_COVERAGES");

						//Get Tran log
						objDelete.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/aviation/VehicleCoverageDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDelete);
						string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

						sbTranXML.Append(strTranXML);

						objWrapper.ClearParameteres();
					}
				}
				sbTranXML.Append("</root>");

				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;

                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1498", ""); //"Aviation Vehicle coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();
					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//Update non-linked endorsements////////
				objWrapper.ClearParameteres();
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return 1;
		}
		public int SaveAviationPolicyVehicleCoverages(ArrayList alNewCoverages,string strOldXML, string strCustomInfo)
		{
			
			string	strStoredProc =	"Proc_SAVE_AVIATION_VEHICLE_COVERAGES_POLICY";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[16];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			if ( strOldXML != "" )
			{
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			int customerID = 0;
			int polID = 0;
			int polVersionID = 0;
			int vehicleID = 0;

			try
			{
				//Loop thru aray list
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					customerID = objNew.CUSTOMER_ID;
					polID = objNew.POLICY_ID;
					polVersionID = objNew.POLICY_VERSION_ID;
					vehicleID = objNew.RISK_ID;

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);
					
					objWrapper.AddParameter("@RATE",DefaultValues.GetDoubleNullFromNegative(objNew.RATE) );
					objWrapper.AddParameter("@COVERAGE_TYPE_ID",objNew.COVERAGE_TYPE_ID);

					string strTranXML = "";
				
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;

					if ( objNew.ACTION == "I" )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/aviation/PolicyVehicleCoverageDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Aviation Policy Vehicle coverage added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/aviation/PolicyVehicleCoverageDetails.aspx.resx");
						objTransactionInfo.TRANS_DESC		=	"Aviation Policy Vehicle coverage updated.";
						if(root != null)
						{
							strTranXML = this.GetPolicyTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
							if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
								sbTranXML.Append(strTranXML);
						}
					}
					objWrapper.ExecuteNonQuery(strStoredProc);
					objWrapper.ClearParameteres();
				}

				objWrapper.ClearParameteres();

				//Delete Coverages/////////////////////////////////////
				//strCustomInfo = "";
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo objDelete = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					if ( objDelete.ACTION == "D" )
					{
						objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
						objWrapper.AddParameter("@POLICY_ID",objDelete.POLICY_ID);
						objWrapper.AddParameter("@POLICY_VERSION_ID",objDelete.POLICY_VERSION_ID);
						objWrapper.AddParameter("@VEHICLE_ID",objDelete.RISK_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objDelete.COVERAGE_ID);
						//Delete the coverage
						objWrapper.ExecuteNonQuery("Proc_DeletePOL_AVIATION_VEHICLE_COVERAGES");

						//Get Tran log
						objDelete.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/aviation/PolicyVehicleCoverageDetails.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDelete);
						string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

						sbTranXML.Append(strTranXML);

						objWrapper.ClearParameteres();
					}
				}
				sbTranXML.Append("</root>");

				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID = polID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = polVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Aviation Policy Vehicle coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();
					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//Update non-linked endorsements////////
				objWrapper.ClearParameteres();
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			return 1;
		}
		#endregion
		#region GetMotorcycleCoverages Function For Copy Coverages
		/// <summary>
		/// Used while copying Coverages for Motorcycle
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetMotorcycleCoverages(DataSet dsCoverages,int customerID, int appID, int appVersionID, int vehicleID)
		{
			
			//fetching XML string with all coverages to remove
			this.dtMotorcycleInfo = dsCoverages.Tables[3];
			
			string covXML=this.GetMotorCoveragesToRemove(customerID,
				appID,
				appVersionID,
				vehicleID,dsCoverages
				);	

			//if XML string is not blank		
			if(covXML!="" )
			{
				
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			
				
				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);		
			}
		
			return dsCoverages;             
		}

		#endregion 

		#region GetMotorcycleCoverages Function to fetch coverages after Filteration as per Business rules
		/// <summary>
		/// 
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetMotorcycleCoverages(int customerID, int appID, int appVersionID, int vehicleID, string appType)
		{
			//fetching dataset with all coverages
			
			DataSet dsCoverages=null;
			
			//Get master list of Coverages from Database
			dsCoverages = this.GetVehCoverages(customerID,
				appID,
				appVersionID,
				vehicleID,appType
				);	
			
			
			this.dtMotorcycleInfo = dsCoverages.Tables[3];

			//Gets coverage to remove XML
			string covXML=this.GetMotorCoveragesToRemove(customerID,
				appID,
				appVersionID,
				vehicleID,dsCoverages
				);	
			
			//Gets an XML containing mandatory coverages
			string strMandXML = this.GetMandatoryCoveragesMotorCycle(customerID,
				appID,
				appVersionID,
				vehicleID,dsCoverages
				);
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			/*
			TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/cmsweb/support/temp_coverage.xml"));
			covXML=tr.ReadToEnd(); 
			tr.Close();
			*/
			 
			  
			Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();

			//if XML string is not blank		
			if(covXML!="" )
			{
				
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			
				
				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);		
			}

			if ( strMandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,strMandXML);			
			}

			return dsCoverages;             
		}

		#endregion 
		


		#region UpdateMotorcycleCoverages Function
		/// <summary>
		/// Updates coverages for motorcycle when it is added/updated
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		public void UpdateMotorcycleCoverages(int customerID,int appID, int appVersionID, int vehicleID, DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 )
			{
				objDataWrapper.ClearParameteres();
			}

			//Insert/delete relevant coverages*********************	
			objDataWrapper.AddParameter("@APP_ID",appID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objDataWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);

			objDataWrapper.ExecuteNonQuery("Proc_Update_MOTORCYCLE_COVERAGES");
		}


		#endregion 
		
		#region "Policy Vehicle Coverages"
		
		/// <summary>
		/// Updates the application level coverages for all vehicle in this application except Suspended
		/// and Trailer
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="objWrapper"></param>
		/// <param name="vehicleID"></param>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		public int UpdatePolicyCoverages(ArrayList alNewCoverages, DataWrapper objWrapper, int vehicleID, int customerID , int polID, int polVersionID)
		{
			objWrapper.ClearParameteres();
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);

			//Get all other vehicles except "Suspended comp only" and Trailer
			DataSet dsVehicles = objWrapper.ExecuteDataSet("Proc_Get_POL_VEHICLES_FOR_POL_COVERAGES");
			
			objWrapper.ClearParameteres();

			DataTable dtVehicle = dsVehicles.Tables[0];

			if ( dtVehicle.Rows.Count == 0 ) return 1;

			//For each vehicle other than current in the application
			//Loop thru the App level coveages and update each Vehicle
			/*
				 127		BISPL	Part A - Bodily Injury Liability (Split Limit)	14
					207		BISPL	Part A - Bodily Injury Liability (Split Limit)	22
					114		BISPL	Part A - Bodily Injury Liability (Split Limit)	22
					115		PD	Part A - Property Damage Liability	22
					4		PD	Part A - Property Damage Liability	14
					208		PD	Part A - Property Damage Liability 	22
					128		PD	Part A - Property Damage Liability 	14
					113		RLCSL	Part A - Residual Liability CSL (BI and PD)	22
					126		RLCSL	Part A - Single Limits Liability (CSL)	14
					206		RLCSL	Part A - Single Limits Liability (CSL)	22
					1		SLL	Part A - Single Limits Liability CSL (BI and PD)	14
					116		PIP	Part A – Personal Injury Protection	22
					6		MP	Part B - Medical Payments	14
					117		PPI	Part B - Property Protection Insurance	22
					34		UNDSP	Part C - Underinsured Motorists (BI Split Limit)	14
					214		UNDSP	Part C - Underinsured Motorists (BI Split Limit) (M-16)	14
					14		UNCSL	Part C - Underinsured Motorists (CSL)	14
					263		UNCSL	Part C - Underinsured Motorists (CSL)	14
					133		UNCSL	Part C - Underinsured Motorists (CSL) (M-16)	14
					132		PUMSP	Part C - Uninsured Motorists (BI Split Limit)	14
					212		PUMSP	Part C - Uninsured Motorists (BI Split Limit)	22
					12		PUMSP	Part C - Uninsured Motorists (BI Split Limit)	14
					120		PUMSP	Part C - Uninsured Motorists (BI Split Limits)	22
					119		PUNCS	Part C - Uninsured Motorists (CSL)	22
					9		PUNCS	Part C - Uninsured Motorists (CSL)	14
					211		PUNCS	Part C - Uninsured Motorists (CSL)	22
					131		PUNCS	Part C - Uninsured Motorists (CSL)	14
					36		UMPD	Part C - Uninsured Motorists (PD) (A - 21)	14
					199		UMPD	Part C - Uninsured Motorists (PD) (M-16)	14

				*/
			foreach(DataRow drVeh in dtVehicle.Rows)
			{
				int newVehicleID = Convert.ToInt32(drVeh["VEHICLE_ID"]);

				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					string strCovCode = objNew.COVERAGE_CODE;

					if ( objNew.COVERAGE_TYPE == "PL")
					{
						if ( objNew.ACTION == "I" || objNew.ACTION == "U" )
						{
							//Add Linked endorsements////////////////////////////////
							objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
							objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
							objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
							objWrapper.AddParameter("@VEHICLE_ID",newVehicleID);
							objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
							objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
							objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
							objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
							objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
							objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
							objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
							objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
							objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
							objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
							objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
							objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
							objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
							objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
							objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
							objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
							objWrapper.AddParameter("@SIGNATURE_OBTAINED",objNew.SIGNATURE_OBTAINED);
							objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
							objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
							objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);

							objWrapper.ExecuteNonQuery("Proc_SAVE_POLICY_VEHICLE_COVERAGES");
							objWrapper.ClearParameteres();
						}

						if ( objNew.ACTION == "D")
						{
							objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
							objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
							objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
							objWrapper.AddParameter("@VEHICLE_ID",newVehicleID);
							objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
							
							//Delete the coverage
							objWrapper.ExecuteNonQuery("Proc_Delete_POL_VEHICLE_COVERAGES_BY_ID");

							objWrapper.ClearParameteres();
						}

					}

				}

			}
			
			return 1;
		}


		/// <summary>
		/// Copies coverages from one vehicle to another
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int CopyPolicyVehicleCoverages(ArrayList alNewCoverages,int customerID, int policyID,
			int policyVersionID,int vehicleID,string strOldXML,int userID)
		{
			string	strDelProc =	"Proc_DeletePOL_VEHICLE_COVERAGES_ALL";
			string	strStoredProc =	"Proc_SAVE_POLICY_VEHICLE_COVERAGES";

			string tranLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			//Delete linked Endorsements and coverages///////////////////////////
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			
			try
			{
				objWrapper.ExecuteNonQuery(strDelProc);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			objWrapper.ClearParameteres();
			///////////////////////////
			//Update/Copy policy Level coverages/////////////////////////// By Pravesh on 13 April 09 itrak 5460
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",policyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			
			try
			{
				objWrapper.ExecuteNonQuery("Proc_COPY_POLICY_LEVEL_COVERAGES");
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			objWrapper.ClearParameteres();
			///////////////////////////

			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
		
			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					
					

					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					//Modified by Praveen Kumar(19-01-2009):Itrack 5281
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					//END PRAVEEN KUMAR
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@SIGNATURE_OBTAINED",objNew.SIGNATURE_OBTAINED);
					//ADDED BY PRAVEEN KUMAR(19-01-2009):Itrack 5281
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUC_ID));

					//END PRAVEEN KUMAR
					objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.COVERAGE_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = tranLabel;
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						//ADDED PRAVEEN KUMAR(4-02-2009):ITRACK 5281
						objTransactionInfo.RECORDED_BY			=	userID;
						//END
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = tranLabel;
				
						strTranXML = this.GetPolicyTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
					}
				
					if ( strTranXML.Trim() == "" )
					{
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
				
					}
					else
					{
						
					
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						//ADDED PRAVEEN KUMAR(4-02-2009):ITRACK 5281
						objTransactionInfo.RECORDED_BY			=	userID;
						//END
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverages copied.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
			}
			catch(Exception ex)
			{
				//tran.Rollback();
				//conn.Close();
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			//tran.Commit();
			//conn.Close();
			UpdateCoveragesByRulePolicy(objWrapper,customerID,policyID,policyVersionID,RuleType.RiskDependent,vehicleID);
			objWrapper.ClearParameteres();
			UpdateCoveragesByRulePolicy(objWrapper,customerID,policyID,policyVersionID,RuleType.MiscEquipment,vehicleID);
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}


		/// <summary>
		/// Gets all the coverages from the selected vehicle
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public DataSet GetPolicyCoveragesToCopy(int customerID, 
			int polID, int polVersionID, int vehicleID,string calledFrom)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@POLICY_ID",polID);
			sqlParams[1] = new SqlParameter("@POLICY_VERSION_ID",polVersionID);
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			sqlParams[4] = new SqlParameter("@CALLED_FROM",calledFrom);

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GetPOL_VEHICLE_COVERAGES_COPY",sqlParams);

			return ds;
		}
		

		/// <summary>
		/// Gets all the vehicles eligible for copying
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		public DataTable GetPolicyVehiclesToCopy(int customerID, 
			int polID, int polVersionID, int vehicleID,string calledFrom)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@CUSTOMER_ID",customerID);
			sqlParams[1] = new SqlParameter("@POLICY_ID",polID);
			sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID",polVersionID);
			sqlParams[3] = new SqlParameter("@VEHICLE_ID",vehicleID);
			sqlParams[4] = new SqlParameter("@CALLED_FROM",calledFrom);

			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_GET_POL_VEHICLES_COPY",sqlParams);

			return ds.Tables[0];
		}
		
		/// <summary>
		/// For Copy Application 
		/// Filters the master coverage list  accordignto business rules
		/// </summary>
		/// <returns>DataSet after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetPolicyVehicleCoverages(DataSet dsCoverages,int customerID, int polID, int polVersionID, int vehicleID, string polType)
		{
			
			
			//fetching XML string with all coverages to remove

			dtVehicle = dsCoverages.Tables[3];
			string covXML=this.GetCoveragesToRemove(customerID,
				polID,
				polVersionID,
				vehicleID,dsCoverages
				);	

			string covMandatoryXML=this.GetMandatoryCoverages(customerID,
				polID,
				polVersionID,
				vehicleID,dsCoverages
				);	
		
			//if XML string is not blank		
			if(covXML!="" )
			{
				//Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages = this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages = this.DeleteCoverageOptions(dsCoverages,covXML);			

				//function call to update default field
				dsCoverages = this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}

			return dsCoverages;             
		}



		#region GetPolicyVehicleCoverages ----- Not Used 
		/// <summary>
		/// Filters the master coverage list accordignto business rules
		/// </summary>
		/// <returns>DataSet after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetPolicyVehicleCoverages(int customerID, int polID, int polVersionID, int vehicleID, string polType)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;

			dsCoverages = GetPolicyVehicleCoverage(customerID,
				polID,
				polVersionID,
				vehicleID,polType
				);	

			//Populate instance data/////
			DataTable dtState = dsCoverages.Tables[2];
			dtVehicle = dsCoverages.Tables[3];  

			if ( dtState != null && dtState.Rows.Count > 0 )
			{
				if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
				{
					this.StateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
				}
			}
			
			if ( dtVehicle != null && dtVehicle.Rows.Count > 0 )
			{
				if ( dtVehicle.Rows[0]["APP_USE_VEHICLE_ID"] != DBNull.Value )
				{
					this.VehicleUse = Convert.ToString(dtVehicle.Rows[0]["APP_USE_VEHICLE_ID"]);
				}
				if(VehicleUse == "11333")//Commercial
				{
					if ( dtVehicle.Rows[0]["APP_VEHICLE_COMTYPE_ID"] != DBNull.Value )
					{
						this.VehicleType = Convert.ToString(dtVehicle.Rows[0]["APP_VEHICLE_COMTYPE_ID"]);
					}
				}
				if(VehicleUse == "11332") //Personal
				{
					if ( dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"] != DBNull.Value )
					{
						this.VehicleType = Convert.ToString(dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"]);
					}
				}
			}
			//End of populate data

			string covXML=this.GetCoveragesToRemove(customerID,
				polID,
				polVersionID,
				vehicleID,dsCoverages
				);	
			
			string covMandatoryXML=this.GetMandatoryCoverages(customerID,
				polID,
				polVersionID,
				vehicleID,dsCoverages
				);	

		
			//if XML string is not blank		
			if(covXML!="" )
			{
				//Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();
				//function call to delete coverage
				dsCoverages = this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages = this.DeleteCoverageOptions(dsCoverages,covXML);			

				//function call to update default field
				dsCoverages = this.OverwriteCoverageDefaultValue(dsCoverages,covXML);			
			}
			
			if ( covMandatoryXML != "" )
			{
				//function call to update mandatory field
				dsCoverages = this.UpdateCoverageMandatory(dsCoverages,covMandatoryXML);
			}

			return dsCoverages;             
		}
		#endregion

		
		/// <summary>
		/// Gets polilcy vehicle resultset from the database
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="polType"></param>
		/// <returns></returns>
		private  DataSet GetPolicyVehicleCoverage(int customerID, int polID, 
			int polVersionID, int vehicleID, string polType)
		{
			//string	strStoredProc =	"Proc_Get_POL_VEHICLE_COVERAGES";
			string	strStoredProc =	"Proc_Get_POL_VEHICLE_COVERAGES_NEW";
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objWrapper.AddParameter("@POL_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}


		
		/// <summary>
		/// Saves Vehicle coverages for PPA and Motorcycle
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int SavePolicyVehicleCoverages(ArrayList alNewCoverages,string strOldXML, string strCustomInfo)
		{
			
			string	strStoredProc =	"Proc_SAVE_POLICY_VEHICLE_COVERAGES";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			SqlCommand cmdCoverage = new SqlCommand();
			cmdCoverage.CommandText = strStoredProc;
			cmdCoverage.CommandType = CommandType.StoredProcedure;
			
			SqlParameter[] param = new SqlParameter[16];
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}
			
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			int customerID = 0;
			int policyID = 0;
			int policyVersionID = 0;
			int vehicleID = 0;
			/*string strLimitAmount1="";
			string strDeductibleAmount1="";
			string strLimitAmount2="";
			string strDeductibleAmount2="";*/


			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					
					customerID = objNew.CUSTOMER_ID;
					policyID = objNew.POLICY_ID;
					policyVersionID = objNew.POLICY_VERSION_ID;
					vehicleID = objNew.RISK_ID ;

					/*if(objNew.LIMIT_1.ToString()!="-1")
						strLimitAmount1=objNew.LIMIT_1.ToString();
					if(objNew.LIMIT_2.ToString()!="-1")
						strLimitAmount2=objNew.LIMIT_2.ToString();
					if(objNew.DEDUCTIBLE_1.ToString()!="-1")
						strDeductibleAmount1=objNew.DEDUCTIBLE_1.ToString();
					if(objNew.DEDUCTIBLE_2.ToString()!="-1")
						strDeductibleAmount2=objNew.DEDUCTIBLE_2.ToString();
					objNew.LIMIT_AMOUNT_TEXT = strLimitAmount1 + objNew.LIMIT1_AMOUNT_TEXT + " " + strLimitAmount2 +  " " + objNew.LIMIT2_AMOUNT_TEXT;
						//objNew.LIMIT_1.ToString() + " " + objNew.LIMIT1_AMOUNT_TEXT + " " + objNew.LIMIT_2.ToString() + " " + objNew.LIMIT2_AMOUNT_TEXT;
					objNew.DEDUCTIBLE_AMOUNT_TEXT = strDeductibleAmount1 + " " + objNew.DEDUCTIBLE1_AMOUNT_TEXT + " " + strDeductibleAmount2 + " " + objNew.DEDUCTIBLE2_AMOUNT_TEXT;
					//	objNew.DEDUCTIBLE_1.ToString() + " " + objNew.DEDUCTIBLE1_AMOUNT_TEXT + " " + objNew.DEDUCTIBLE_2.ToString() + " " + objNew.DEDUCTIBLE2_AMOUNT_TEXT;*/

					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.RISK_ID);
					objWrapper.AddParameter("@COVERAGE_ID",objNew.COVERAGE_ID);
					objWrapper.AddParameter("@COVERAGE_CODE_ID",objNew.COVERAGE_CODE_ID);
					objWrapper.AddParameter("@LIMIT_1_TYPE",objNew.LIMIT_1_TYPE);
					objWrapper.AddParameter("@LIMIT_2_TYPE",objNew.LIMIT_2_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_1",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
					objWrapper.AddParameter("@DEDUCTIBLE_2",DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
					objWrapper.AddParameter("@LIMIT_1",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
					objWrapper.AddParameter("@LIMIT_2",DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
					objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",objNew.LIMIT1_AMOUNT_TEXT);
					objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",objNew.LIMIT2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",objNew.DEDUCTIBLE1_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",objNew.DEDUCTIBLE2_AMOUNT_TEXT);
					objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE",objNew.DEDUCTIBLE_1_TYPE);
					objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE",objNew.DEDUCTIBLE_2_TYPE);
					objWrapper.AddParameter("@WRITTEN_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
					objWrapper.AddParameter("@FULL_TERM_PREMIUM",DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
					objWrapper.AddParameter("@SIGNATURE_OBTAINED",objNew.SIGNATURE_OBTAINED);
					objWrapper.AddParameter("@LIMIT_ID",DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
					objWrapper.AddParameter("@DEDUC_ID",DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
					objWrapper.AddParameter("@ADD_INFORMATION",objNew.ADD_INFORMATION);

					string strTranXML = "";
				
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;

					if ( objNew.ACTION == "I" )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverage added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

						sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if ( objNew.ACTION == "U")
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
						objTransactionInfo.TRANS_DESC		=	"Vehicle coverage updated.";
						strTranXML = this.GetPolicyTranXML(objNew,strOldXML,objNew.COVERAGE_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
				
				}
				objWrapper.ClearParameteres();
				//Delete Coverages/////////////////////////////////////
				//string strCustomInfo="Following coverages have been deleted:",str="";
				//strCustomInfo = "";
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					
					Cms.Model.Policy.ClsPolicyCoveragesInfo objDelete = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];
					
					if ( objDelete.ACTION == "D" )
					{
						objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
						objWrapper.AddParameter("@POLICY_ID",objDelete.POLICY_ID);
						objWrapper.AddParameter("@POLICY_VERSION_ID",objDelete.POLICY_VERSION_ID);
						objWrapper.AddParameter("@VEHICLE_ID",objDelete.RISK_ID);
						objWrapper.AddParameter("@COVERAGE_ID",objDelete.COVERAGE_ID);
						//str+=";" + objDelete.COV_DESC;
						//Delete the coverage
						objWrapper.ExecuteNonQuery("Proc_Delete_POL_VEHICLE_COVERAGES");

						//Get Tran log
                        objDelete.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						//string strTranXML = objBuilder.GetTransactionLogXML(objDelete);
						string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

						sbTranXML.Append(strTranXML);

						objWrapper.ClearParameteres();
					}
				}
				//////////////////////////////////////////////////////////

				//Insert tran log entry//////////////////////////
				/*if(str!="")
					strCustomInfo+=str;
				else
					strCustomInfo="";*/
				
				sbTranXML.Append("</root>");

				//if(sbTranXML.ToString()!="<root></root>")
				//	strCustomInfo+=";Following coverages have been added/updated";

				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID= policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = policyVersionID;
					objTransactionInfo.CLIENT_ID = customerID;

                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1497", ""); //"Vehicle coverages updated.";
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();

					objTransactionInfo.CUSTOM_INFO=strCustomInfo;
				
					objWrapper.ClearParameteres();

					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				//////////////////
				///Ravindra(04-25-2006) 
				//Update non-linked endorsements////////
//				objWrapper.ClearParameteres();
//				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
//				objWrapper.AddParameter("@POLICY_ID",policyID);
//				objWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID);
//				objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
//		
//				objWrapper.ExecuteNonQuery("Proc_UPDATE_POLICY_VEHICLE_ENDORSEMENTS");
//				objWrapper.ClearParameteres();
				//End of non-linked/////////////////////
				
				//Update Policy Coverages///////////////
				if(alNewCoverages.Count>0)
				{
					UpdatePolicyCoverages(alNewCoverages,objWrapper,vehicleID,customerID,policyID,policyVersionID);
					/////////////////////////////////////////
				
					//Update relevant endorsements from coverages/////////
					UpdateEndorsmentPolicy(objWrapper,customerID,policyID,policyVersionID,vehicleID);
					//UpdatePolicyEndorsements(customerID,policyID,policyVersionID,vehicleID,objWrapper);
				}
				/////////////////////

				
			}
			catch(Exception ex)
			{
				//tran.Rollback();
				//conn.Close();
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				if ( ex.InnerException != null)
				{
					string message = ex.InnerException.Message.ToLower();
				

					if ( message.StartsWith("violation of primary key"))
					{
						return -2;
					}

				}

				throw(ex);
			} 
			
			//tran.Commit();
			//conn.Close();
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;
		}
		

		#endregion


		#region Motorcycle Coverages --Policy

		/// <summary>
		/// Retirns a dataset after applying business rules for motorcycle
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetPolicyMotorcycleCoverages(int customerID, int polID, int polVersionID, int vehicleID, string appType)
		{
			//fetching dataset with all coverages
			DataSet dsCoverages=null;

			/*
			dsCoverages = this.GetPolicyMotCoverages(customerID,
				polID,
				polVersionID,
				vehicleID,appType
				);	
			*/
			dsCoverages = this.GetPolicyVehicleCoverage(customerID,
				polID,
				polVersionID,
				vehicleID,appType
				);	

            //Set Public Valraible
			this.dtMotorcycleInfo = dsCoverages.Tables[3];
			//Gets coverage to remove XML
			string covXML=this.GetMotorCoveragesToRemove(customerID,
				polID ,
				polVersionID,
				vehicleID,dsCoverages
				);	
			
			//Gets an XML containing mandatory coverages
			string strMandXML = this.GetMandatoryCoveragesMotorCycle(customerID,
				polID,
				polVersionID,
				vehicleID,dsCoverages
				);
			this.dtPolicyMotorcycleInfo = dsCoverages.Tables[3];

			/*
			dsPolicyVehicleInfo = ClsVehicleInformation.FetchCycleFromPOLVehicleTable(customerID,
				polID,
				polVersionID,
				vehicleID
				);
			*/

			
			  
			//if XML string is not blank		
			if(covXML!="" )
			{
				
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			
				
				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);		
			}

			if ( strMandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,strMandXML);			
			}

			return dsCoverages;             
		}


		/// <summary>
		/// Applies busineess rules to the passed in dataset
		/// </summary>
		/// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
		public DataSet GetPolicyMotorcycleCoverages(DataSet dsCoverages,int customerID, int polID, int polVersionID, int vehicleID, string appType)
		{
			
			//fetching XML string with all coverages to remove
			ClsVehicleInformation objVeh=new ClsVehicleInformation();  

			//Get Motorcycle Information
			dsPolicyVehicleInfo = ClsVehicleInformation.FetchCycleFromPOLVehicleTable(customerID,
				polID,
				polVersionID,
				vehicleID
				);

			string covXML=this.GetMotorCoveragesToRemove(customerID,
				polID,
				polVersionID,
				vehicleID,dsCoverages
				);	
			
			//Gets an XML containing mandatory coverages
			string strMandXML = this.GetMandatoryCoveragesMotorCycle(customerID,
				polID,
				polVersionID,
				vehicleID,dsCoverages
				);
			/*Reading from the XML file and saving the text in string until the function start giving the XML string */
			/*
			TextReader tr=new StreamReader(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath  + "/cmsweb/support/temp_coverage.xml"));
			covXML=tr.ReadToEnd(); 
			tr.Close();
			*/
			 
			  
			Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation = new Cms.BusinessLayer.BlApplication.clsapplication();

			//if XML string is not blank		
			if(covXML!="" )
			{
				
				//function call to delete coverage
				dsCoverages=this.DeleteCoverage(dsCoverages,covXML);			

				//function call to delete coverage limits
				dsCoverages=this.DeleteCoverageOptions(dsCoverages,covXML);			
				
				//function call to update default field
				dsCoverages=this.OverwriteCoverageDefaultValue(dsCoverages,covXML);		
			}

			if ( strMandXML != "" )
			{
				//function call to update mandatory field
				dsCoverages=this.UpdateCoverageMandatory(dsCoverages,strMandXML);			
			}

			return dsCoverages;             
		}

		#endregion


		#region "Update endorsements"
		
		/// <summary>
		/// Updates the relevant endorsements from coverages
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public static int UpdateApplicationEndorsements(int customerID, int appID, int appVersionID, int vehicleID, DataWrapper objWrapper)
		{
//			if ( objWrapper.CommandParameters.Length > 0 )
//			{
//				objWrapper.ClearParameteres();
//			}

//			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
//			objWrapper.AddParameter("@APP_ID",appID);
//			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
//			//objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
//			
//			objWrapper.ExecuteNonQuery("Proc_UPDATE_DRIVER_ENDORSEMENTS");

			if ( objWrapper.CommandParameters.Length > 0 )
			{
				objWrapper.ClearParameteres();
			}
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objWrapper.ExecuteNonQuery("Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES");

			

			return 1;

		}
		
		/// <summary>
		/// Updates the relevant endorsements from coverages
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <returns></returns>
		public static int UpdatePolicyEndorsements(int customerID, int polID, int polVersionID, int vehicleID, DataWrapper objWrapper)
		{
			if ( objWrapper.CommandParameters.Length > 0 )
			{
				objWrapper.ClearParameteres();
			}

			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			
			objWrapper.ExecuteNonQuery("Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_POLICY");

			return 1;

		}
		
		#endregion
	
			
		
		#region "App Motor rules"
		
		/// <summary>
		/// Sets each coverage to mandatory or optional based on business rules
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="objDataSet"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		public string GetMandatoryCoveragesMotorCycle(int customerID,int appID, int appVersionID, 
			int vehicleID, DataSet objDataSet)
		{
			//if ( this.dtMotorcycleInfo == null ) return "";

			//if ( dtMotorcycleInfo.Rows.Count == 0 ) return "";
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtState = objDataSet.Tables[2];
			int appYear=0;
			int motYear=0;
			int motCC=0;
			dtVehicle= objDataSet.Tables[3];
            string strComp="";

			if(dtVehicle != null)
			{
				if(dtVehicle.Rows.Count >0)
				{
					strComp=dtVehicle.Rows[0]["COMPRH_ONLY"].ToString();
					motYear=Convert.ToInt32(dtVehicle.Rows[0]["VEHICLE_YEAR"]);
					motCC  =Convert.ToInt32(dtVehicle.Rows[0]["VEHICLE_CC"]);
		     	}
			}
				
			StringBuilder sbXML = new StringBuilder();
			
			int stateID = 0;

			if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
			{
				stateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
				appYear = Convert.ToInt32(dtState.Rows[0]["APP_YEAR"]);
			}
		
			sbXML.Append("<Coverages>");
			
			foreach(DataRow dr in dtCoverage.Rows )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				
				if ( covCode == "MEDPM" && stateID == 22)
				{
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
					sbXML.Append("</Coverage>");
				}				
				if(strComp =="10963")// Indiana State check removed as applicable for Michigan also,20-Aug-09 Itrack 6151 -Charles
				{
					if(covCode =="EBM14" || covCode =="EBM49" || covCode =="EBM15" || covCode =="PDC14" ||  covCode =="OTC" )
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='N'>");
						sbXML.Append("</Coverage>");
					}
					else
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
                    }
				}
				/* Commeted by Pravesh on 23 july 2009 Itrack 5912 
				if(appYear-motYear > 25)
				{
					if(covCode =="OTC")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}
				}*/
				if(motCC < 51)
				{
					if(covCode =="MEDPM1")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}
				}
//				else
//				{
//					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='N'>");
//					sbXML.Append("</Coverage>");
//				}
			}	
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
			
		}
		
		/// <summary>
		/// Returns an XML containing limits lesser than the min deductible for the passed in Motorcycle CC 
		/// </summary>
		/// <param name="dsCoverages"></param>
		/// <param name="cc"></param>
		/// <param name="stateID"></param>
		/// <param name="lobID"></param>
		/// <returns></returns>
		public string GetCoveragesToRemoveBasedOnCC(DataSet dsCoverages, int cc, int stateID)
		{
			//DataSet dsCC = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			objDataWrapper.AddParameter("@STATE_ID",stateID,SqlDbType.Int);
			
			DataSet dsCC = objDataWrapper.ExecuteDataSet("Proc_Get_MOTORCYCLE_CC_RANGES");
			
			StringBuilder sbXML = new StringBuilder();

			if ( dsCC == null ) return "";

			if ( dsCC.Tables.Count == 0 ) return "";

			DataTable dtCC = dsCC.Tables[0];
			DataTable dtLimits = dsCoverages.Tables[1];
			
			
			int otcID = 0;
			int collID = 0;
			
			//Indiana
			if ( stateID == 14 )
			{
				otcID = 201;
				collID = 200;
			}
			
			//Michigan
			if ( stateID == 22 )
			{
				otcID = 217;
				collID = 216;
			}
			
			
			//Other than Collision
			//Get the minimum limit for this particular CC
			DataRow[] drOTC = dtCC.Select("COV_ID = " + otcID.ToString() + " AND " + cc.ToString() + " >= CC_RANGE1 AND " + cc.ToString() + " <= CC_RANGE2");
			
			if ( drOTC != null && drOTC.Length > 0)
			{
				//Minimum amount
				int amount = 0;

				if ( drOTC[0]["LIMIT_DEDUC_AMOUNT"] != DBNull.Value )
				{
					amount = Convert.ToInt32(drOTC[0]["LIMIT_DEDUC_AMOUNT"]);
				}
				
				//Part D - Other Than Collision (Comprehensive)
				DataRow[] drOTCLimits = dtLimits.Select("COV_ID=" + otcID.ToString() + " AND LIMIT_DEDUC_AMOUNT < " + amount.ToString() );
					
				sbXML.Append("<Coverage COV_ID=\"" + otcID.ToString() + "\" COV_CODE=\"COLL\" Remove=\"N\" Mandatory='N'" + ">");

				foreach(DataRow dr in drOTCLimits)
				{
					if ( dr["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
					{
						string amt = dr["LIMIT_DEDUC_AMOUNT"].ToString();
		
						sbXML.Append("<Limit id='"+dr["LIMIT_DEDUC_ID"]+"' amount='"+dr["LIMIT_DEDUC_AMOUNT"]+"' type='"+dr["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
						sbXML.Append("</Limit>");	
						
					}
				}
				
				sbXML.Append("</Coverage>");
			}
			
			//Collision
			DataRow[] drCOLL = dtCC.Select("COV_ID = " + collID.ToString() + " AND " + cc.ToString() + " >= CC_RANGE1 AND " +  cc.ToString() + "  <= CC_RANGE2");
			
			if ( drCOLL != null && drCOLL.Length > 0)
			{
				//Minimum amount
				int amount = 0;

				if ( drCOLL[0]["LIMIT_DEDUC_AMOUNT"] != DBNull.Value )
				{
					amount = Convert.ToInt32(drCOLL[0]["LIMIT_DEDUC_AMOUNT"]);
				}
				
				//Collission
				DataRow[] drCOLLLimits = dtLimits.Select("COV_ID=" + collID.ToString() + " AND LIMIT_DEDUC_AMOUNT < " + amount.ToString() );
					
				sbXML.Append("<Coverage COV_ID=\"" + collID.ToString() + "\" COV_CODE=\"COLL\" Remove=\"N\" Mandatory='N'" + ">");

				foreach(DataRow dr in drCOLLLimits)
				{
					if ( dr["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
					{
						string amt = dr["LIMIT_DEDUC_AMOUNT"].ToString();
		
						sbXML.Append("<Limit id='"+dr["LIMIT_DEDUC_ID"]+"' amount='"+dr["LIMIT_DEDUC_AMOUNT"]+"' type='"+dr["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
						sbXML.Append("</Limit>");	
						
					}
				}
				
				sbXML.Append("</Coverage>");
			}
			
			return sbXML.ToString();
			
		}
		
		/// <summary>
		/// Provides common functionality for Motor coverages removal for both app and policy
		/// </summary>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetCommonMotorCoveragesToRemove(DataSet objDataSet)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/MotorCoverages.xml");
			StringBuilder sbXML = new StringBuilder();
			StringBuilder sbRemove = new StringBuilder();
			StringBuilder sbRemoveLimit=new StringBuilder();


			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];
			DataTable dtState  =objDataSet.Tables[2];
			int stateID = 0;
			int lobID = 0;
			int umbPol=0,under25=0;

			if(objDataSet.Tables[6].Rows.Count > 0)
			{
				umbPol=Convert.ToInt32(objDataSet.Tables[6].Rows[0]["UMB_POL"]);
				under25=Convert.ToInt32(objDataSet.Tables[6].Rows[0]["UNDER_25_AGE"]);
			}
			
			//Get LOB and State//////////////////////////////////////////
			if ( dtState != null )
			{
				if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
				{
					stateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
				}

				if ( dtState.Rows[0]["LOB_ID"] != DBNull.Value )
				{
					lobID = Convert.ToInt32(dtState.Rows[0]["LOB_ID"]);
				}

			}
			
			XmlDocument doc = new XmlDocument();
			doc.Load(filePath);
			
			if ( VehicleUse.Trim() == "" ) 
			{
				VehicleUse = "0";
			}

			XmlNode node = doc.SelectSingleNode("Motor/State[@STATE_ID=" +  stateID.ToString() + "]");
	
			if ( node == null ) return "";

			XmlNode vehUseNode = node.SelectSingleNode("UmbrellaPolicy[@Value=" + umbPol.ToString() + "]");

			if ( vehUseNode == null ) return "";
			//added by Pravesh to Remove Coverages
			XmlNode removeNode = vehUseNode.SelectSingleNode("Remove/Coverages");
			
			if ( removeNode != null )
			{
				XmlNodeList removeList = removeNode.SelectNodes("Coverage");
				
				//Loop thru each coveages to remove
				foreach(XmlNode remNode in removeList)
				{
					string coverageID = remNode.Attributes["ID"].Value;
					string covCode = remNode.Attributes["Code"].Value;
				
					if ( sbRemove.ToString() == "" )
					{
						sbRemove.Append(coverageID);
					}
					else
					{
						sbRemove.Append("," + coverageID );
					}
				}
			}
			removeNode = vehUseNode.SelectSingleNode("IsUnder25[@Value=" + under25.ToString() + "]/Remove/Coverages");
			if ( removeNode != null )
			{
				XmlNodeList removeList = removeNode.SelectNodes("Coverage");
				
				//Loop thru each coveages to remove
				foreach(XmlNode remNode in removeList)
				{
					string coverageID = remNode.Attributes["ID"].Value;
					string covCode = remNode.Attributes["Code"].Value;
				
					if ( sbRemove.ToString() == "" )
					{
						sbRemove.Append(coverageID);
					}
					else
					{
						sbRemove.Append("," + coverageID );
					}
				}
			}
			//Get the coverages to remove from the master dataset and create XML
			if (sbRemove.ToString()!="")
			{
				DataRow[] drRemove = dtCoverage.Select("COV_ID IN (" + sbRemove.ToString() + ")");
			
				foreach(DataRow dr1 in drRemove)
				{
					string covID = dr1["COV_ID"].ToString();
					string mand = dr1["IS_MANDATORY"].ToString()=="0"?"N":"Y";
					string covCode = dr1["COV_CODE"].ToString();
				
					sbXML.Append("<Coverage COV_ID='" + covID + "' COV_CODE='" + covCode + "' Remove='Y' Mandatory='N'>");
					sbXML.Append("</Coverage>");
				}
			}
			XmlNodeList removeNodeLimit = vehUseNode.SelectNodes("IsUnder25[@Value=" + under25.ToString() + "]/Remove/CoverageLimit");
			foreach(XmlNode removeNodeList in removeNodeLimit)
			{
				if (removeNodeList.ChildNodes.Count==0) break;
				string covID = removeNodeList.Attributes["COV_ID"].Value;
				XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"\" Remove=\"N\" Mandatory=\"N\">");
				//Loop thru each coveages to remove
			
				foreach(XmlNode remNode in removeLimitList)
				{
					string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
					string Amount = remNode.Attributes["Amount"].Value;
					if ( sbRemoveLimit.ToString() == "" )
					{
						sbRemoveLimit.Append(strLIMIT_DEDUC_ID);
					}
					else
					{
						sbRemoveLimit.Append("," + strLIMIT_DEDUC_ID );
					}
				
				
			
				}
				if (sbRemoveLimit.ToString()!="")
				{
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit.ToString() + ")");
					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
						{
					
							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
					
						}
					}
				}
				sbXML.Append("</Coverage>");
			}
			// added by Pravesh end here
			//XmlNodeList removeNodeLimit = vehUseNode.SelectNodes("Remove/CoverageLimit");
			 removeNodeLimit = vehUseNode.SelectNodes("Remove/CoverageLimit");
			foreach(XmlNode removeNodeList in removeNodeLimit )
			{
				string covID = removeNodeList.Attributes["COV_ID"].Value;
				XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
				//Loop thru each coveages to remove
			
				foreach(XmlNode remNode in removeLimitList)
				{
					string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
					string Amount = remNode.Attributes["Amount"].Value;
					if ( sbRemoveLimit.ToString() == "" )
					{
						sbRemoveLimit.Append(strLIMIT_DEDUC_ID);
					}
					else
					{
						sbRemoveLimit.Append("," + strLIMIT_DEDUC_ID );
					}
				
				
			
				}
				DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit.ToString() + ")");
				foreach(DataRow drLimit in drLimits)
				{	
					if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
					{
					
						sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
						sbXML.Append("</Limit>");	
					
					}
				}
				sbXML.Append("</Coverage>");
			}

		
//			/////////////////////////////////////////////////
//
//			StringBuilder sbXML = new StringBuilder();
//			//Part D Collision
//			
//			DataRow[] drCOLL = dtCoverage.Select("COV_CODE='COLL'");
//			
//			//Part D - Other Than Collision (Comprehensive)
//			DataRow[] drOTC = dtCoverage.Select("COV_CODE='OTC'");
//			
//			//Part A - Single Limits Liability (CSL) 
//			DataRow[] drCSL =  dtCoverage.Select("COV_CODE='RLCSL' OR COV_CODE='CSL' ");
//			
//			//Part A - Bodily Injury Liability (Split Limit) 
//			DataRow[] drBISPL =  dtCoverage.Select("COV_CODE='BISPL'");
//			
//			string collisionID = "0";
//			string otcID = "0";
//			string cslID = "0";
//			string bisplID = "0";


//			//Collision ID
//			if ( drCOLL != null && drCOLL.Length > 0)
//			{
//				collisionID = drCOLL[0]["COV_ID"].ToString();
//			}
//			
//			if ( drOTC != null && drOTC.Length > 0)
//			{
//				//Other than Collision ID
//				otcID = drOTC[0]["COV_ID"].ToString();
//			}
//			
//			if ( drCSL != null && drCSL.Length > 0)
//			{
//				cslID = drCSL[0]["COV_ID"].ToString();
//			}
//			
//			if ( drBISPL != null && drBISPL.Length > 0)
//			{
//				bisplID = drBISPL[0]["COV_ID"].ToString();
//			}
//
//			DataRow[] drCollLimits = dtLimits.Select("COV_ID=" + collisionID);
//			DataRow[] drOTCLimits = dtLimits.Select("COV_ID=" + otcID);
//			DataRow[] drCSLLimits = dtLimits.Select("COV_ID=" + cslID);
//			DataRow[] drBISPLLimits = dtLimits.Select("COV_ID=" + bisplID);
			
		
			//Commented By Ravindra -- will be handeled at SP level as Grandfathered
			//For Michigan, remove 250,000 and 300,000 from 
			//211		PUNCS	Uninsured Motorists (CSL)
			/*if ( stateID == 22 )
			{
				sbXML.Append("<Coverage COV_ID=\"211\"" + " COV_CODE=\"PUNCS\" Remove=\"N\" Mandatory='N'>");

				DataRow[] drPUNCS = dtLimits.Select("LIMIT_DEDUC_ID IN (400,401)");

				for ( int i = 0; i < drPUNCS.Length; i ++ )
				{
					if ( drPUNCS[i]["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
					{
						sbXML.Append("<Limit id='"+ drPUNCS[i]["LIMIT_DEDUC_ID"]+"' amount='"+ drPUNCS[i]["LIMIT_DEDUC_AMOUNT"]+"' type='"+ drPUNCS[i]["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
						sbXML.Append("</Limit>");	
					}
				}

				sbXML.Append("</Coverage>");
			}*/
			//////////////////
			
//			//For Indiana State remove limits from BISPL and CSL
//			if ( stateID == 14 )
//			{
//				//BISPL
//				if ( drBISPL != null  && drBISPL.Length > 0 )
//				{	
//					sbXML.Append("<Coverage COV_ID=\"" + bisplID + "\" COV_CODE=\"COLL\" Remove=\"N\" Mandatory='" + drBISPL[0]["IS_MANDATORY"].ToString() + "'>");
//
//					for ( int i = 0; i < drBISPLLimits.Length ; i ++ )
//					{
//						if ( drBISPLLimits[i]["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
//						{
//							string amt =  drBISPLLimits[i]["LIMIT_DEDUC_AMOUNT"].ToString();
//
//							if ( amt == "250" || amt == "300")
//							{
//								sbXML.Append("<Limit id='"+ drBISPLLimits[i]["LIMIT_DEDUC_ID"]+"' amount='"+ drBISPLLimits[i]["LIMIT_DEDUC_AMOUNT"]+"' type='"+ drBISPLLimits[i]["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
//								sbXML.Append("</Limit>");	
//							}
//
//						}
//					}
//
//					sbXML.Append("</Coverage>");
//
//				}
//
//				//CSL
//				if ( drCSL != null  && drCSL.Length > 0 )
//				{	
//					sbXML.Append("<Coverage COV_ID=\"" + cslID + "\" COV_CODE=\"COLL\" Remove=\"N\" Mandatory='" + drCSL[0]["IS_MANDATORY"].ToString() + "'>");
//
//					for ( int i = 0; i < drCSLLimits.Length ; i ++ )
//					{
//						if ( drCSLLimits[i]["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
//						{
//							string amt =  drCSLLimits[i]["LIMIT_DEDUC_AMOUNT"].ToString();
//
//							if ( amt == "250" || amt == "300")
//							{
//								sbXML.Append("<Limit id='"+ drCSLLimits[i]["LIMIT_DEDUC_ID"]+"' amount='"+ drCSLLimits[i]["LIMIT_DEDUC_AMOUNT"]+"' type='"+ drCSLLimits[i]["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
//								sbXML.Append("</Limit>");	
//							}
//
//						}
//					}
//
//					sbXML.Append("</Coverage>");
//
//				}
//
//			}
			/////End Of Indiana

			return sbXML.ToString();
		}

		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetMotorCoveragesToRemove(int customerID,int appID, int appVersionID, int vehicleID, DataSet objDataSet)
		{

			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
			DataTable dtState = objDataSet.Tables[2];		
					
			if ( dtMotorcycleInfo == null ) return "";

			//if ( dtMotorcycleInfo.Tables.Count == 0 ) return "";
			//if ( dtMotorcycleInfo.Tables[0].Rows.Count == 0 ) return "";

			string strMotType = "";
			DateTime appEffectiveDate = DateTime.MinValue;
			int stateID = 0;
			int lobID = 0;
			
			//Get LOB and State//////////////////////////////////////////
			if ( dtState != null )
			{
				if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
				{
					stateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
				}

				if ( dtState.Rows[0]["LOB_ID"] != DBNull.Value )
				{
					lobID = Convert.ToInt32(dtState.Rows[0]["LOB_ID"]);
				}

			}
			/////////////////////////////////////////////////

			StringBuilder sbXML = new StringBuilder();
			
			string removeXML = this.GetCommonMotorCoveragesToRemove(objDataSet);

			sbXML.Append(removeXML);

			/*
			//Part D Collision
			DataRow[] drCOLL = dtCoverage.Select("COV_CODE='COLL'");
			
			//Part D - Other Than Collision (Comprehensive)
			DataRow[] drOTC = dtCoverage.Select("COV_CODE='OTC'");
			
			//Part A - Single Limits Liability (CSL) 
			DataRow[] drCSL =  dtCoverage.Select("COV_CODE='RLCSL' OR COV_CODE='CSL' ");
			
			//Part A - Bodily Injury Liability (Split Limit) 
			DataRow[] drBISPL =  dtCoverage.Select("COV_CODE='BISPL'");
			
			string collisionID = "0";
			string otcID = "0";
			string cslID = "0";
			string bisplID = "0";


			//Collision ID
			if ( drCOLL != null && drCOLL.Length > 0)
			{
				collisionID = drCOLL[0]["COV_ID"].ToString();
			}
			
			if ( drOTC != null && drOTC.Length > 0)
			{
				//Other than Collision ID
				otcID = drOTC[0]["COV_ID"].ToString();
			}
			
			if ( drCSL != null && drCSL.Length > 0)
			{
				cslID = drCSL[0]["COV_ID"].ToString();
			}
			
			if ( drBISPL != null && drBISPL.Length > 0)
			{
				bisplID = drBISPL[0]["COV_ID"].ToString();
			}

			DataRow[] drCollLimits = dtLimits.Select("COV_ID=" + collisionID);
			DataRow[] drOTCLimits = dtLimits.Select("COV_ID=" + otcID);
			DataRow[] drCSLLimits = dtLimits.Select("COV_ID=" + cslID);
			DataRow[] drBISPLLimits = dtLimits.Select("COV_ID=" + bisplID);
			
		
			
			//For Michigan, remove 250,000 and 300,000 from 
			//211		PUNCS	Uninsured Motorists (CSL)
			if ( stateID == 22 )
			{
				sbXML.Append("<Coverage COV_ID=\"211\"" + " COV_CODE=\"PUNCS\" Remove=\"N\" Mandatory='N'>");

				DataRow[] drPUNCS = dtLimits.Select("LIMIT_DEDUC_ID IN (400,401)");

				for ( int i = 0; i < drPUNCS.Length; i ++ )
				{
					if ( drPUNCS[i]["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
					{
						sbXML.Append("<Limit id='"+ drPUNCS[i]["LIMIT_DEDUC_ID"]+"' amount='"+ drPUNCS[i]["LIMIT_DEDUC_AMOUNT"]+"' type='"+ drPUNCS[i]["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
						sbXML.Append("</Limit>");	
					}
				}

				sbXML.Append("</Coverage>");
			}
			//////////////////
			
			//For Indiana State remove limits from BISPL and CSL
			if ( stateID == 14 )
			{
				//BISPL
				if ( drBISPL != null  && drBISPL.Length > 0 )
				{	
					sbXML.Append("<Coverage COV_ID=\"" + bisplID + "\" COV_CODE=\"COLL\" Remove=\"N\" Mandatory='" + drBISPL[0]["IS_MANDATORY"].ToString() + "'>");

					for ( int i = 0; i < drBISPLLimits.Length ; i ++ )
					{
						if ( drBISPLLimits[i]["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
						{
							string amt =  drBISPLLimits[i]["LIMIT_DEDUC_AMOUNT"].ToString();

							if ( amt == "250" || amt == "300")
							{
								sbXML.Append("<Limit id='"+ drBISPLLimits[i]["LIMIT_DEDUC_ID"]+"' amount='"+ drBISPLLimits[i]["LIMIT_DEDUC_AMOUNT"]+"' type='"+ drBISPLLimits[i]["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
							}

						}
					}

					sbXML.Append("</Coverage>");

				}

				//CSL
				if ( drCSL != null  && drCSL.Length > 0 )
				{	
					sbXML.Append("<Coverage COV_ID=\"" + cslID + "\" COV_CODE=\"COLL\" Remove=\"N\" Mandatory='" + drCSL[0]["IS_MANDATORY"].ToString() + "'>");

					for ( int i = 0; i < drCSLLimits.Length ; i ++ )
					{
						if ( drCSLLimits[i]["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
						{
							string amt =  drCSLLimits[i]["LIMIT_DEDUC_AMOUNT"].ToString();

							if ( amt == "250" || amt == "300")
							{
								sbXML.Append("<Limit id='"+ drCSLLimits[i]["LIMIT_DEDUC_ID"]+"' amount='"+ drCSLLimits[i]["LIMIT_DEDUC_AMOUNT"]+"' type='"+ drCSLLimits[i]["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
							}

						}
					}

					sbXML.Append("</Coverage>");

				}

			}
			/////End Of Indiana
			*/

			//Remove limits based on Motorcycle type*************************
			if ( dtMotorcycleInfo.Rows[0]["MOTORCYCLE_TYPE"] != System.DBNull.Value )
			{
				strMotType = dtMotorcycleInfo.Rows[0]["MOTORCYCLE_TYPE"].ToString();
				
				string xml = this.GetCoveragesToRemoveBasedOnMotType(objDataSet, strMotType );
				
				sbXML.Append(xml);

			}
			//*************************************************
			
			//Remove limits based on Motorcycle CC*************************
			/*
			if ( dtMotorcycleInfo.Rows[0]["VEHICLE_CC"] != System.DBNull.Value )
			{
				int intCC = Convert.ToInt32(dtMotorcycleInfo.Rows[0]["VEHICLE_CC"]);
			
				string strCCXml = this.GetCoveragesToRemoveBasedOnCC(objDataSet,intCC,stateID);

				sbXML.Append(strCCXml);
			}
			*/
			//************************************************************


			if ( sbXML.ToString() != "" )
			{		
				return "<Coverages>" + sbXML.Append("</Coverages>").ToString();
			}

			return "";
		}
		

		#endregion
		
		
		
		/// <summary>
		/// Returns an XML with coverages/limits to remove based on Motorcycle type
		/// </summary>
		/// <param name="objDataSet"></param>
		/// <param name="motType"></param>
		/// <returns></returns>
		public string GetCoveragesToRemoveBasedOnMotType(DataSet objDataSet, string strMotType )
		{
			DataTable dtCoverage =  objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
			
			StringBuilder sbXML = new StringBuilder();

			//Part D Collision
			DataRow[] drCOLL = dtCoverage.Select("COV_CODE='COLL'");
			
			//Part D - Other Than Collision (Comprehensive)
			DataRow[] drOTC = dtCoverage.Select("COV_CODE='OTC'");
			
			string collisionID = "0";
			string otcID = "0";
			//Collision ID
			if ( drCOLL != null && drCOLL.Length > 0)
			{
				collisionID = drCOLL[0]["COV_ID"].ToString();
			}
			
			if ( drOTC != null && drOTC.Length > 0)
			{
				//Other than Collision ID
				otcID = drOTC[0]["COV_ID"].ToString();
			}
			
			DataRow[] drCollLimits = dtLimits.Select("COV_ID=" + collisionID);
			DataRow[] drOTCLimits = dtLimits.Select("COV_ID=" + otcID);

			//Remove limit options in Collision and Othe than collision 
			//if Motorcycle type is Sports high performance
			if ( strMotType == "11424" )
			{
				if ( drCOLL != null  && drCOLL.Length > 0 )
				{	
					sbXML.Append("<Coverage COV_ID=\"" + collisionID + "\" COV_CODE=\"COLL\" Remove=\"N\" Mandatory='" + drCOLL[0]["IS_MANDATORY"].ToString() + "'>");

					foreach(DataRow dr in drCollLimits)
					{
						if ( dr["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
						{
							string amt = dr["LIMIT_DEDUC_AMOUNT"].ToString();

							if ( amt == "50" || amt == "100" || amt == "150" || amt == "200" || amt == "250")
							{
								sbXML.Append("<Limit id='"+dr["LIMIT_DEDUC_ID"]+"' amount='"+dr["LIMIT_DEDUC_AMOUNT"]+"' type='"+dr["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
							}

						}
					}

					sbXML.Append("</Coverage>");

				}

				if ( drOTC != null  && drOTC.Length > 0 )
				{	
					
					sbXML.Append("<Coverage COV_ID=\"" + otcID + "\" COV_CODE=\"OTC\" Remove=\"N\" Mandatory='" + drOTC[0]["IS_MANDATORY"].ToString() + "'>");

					foreach(DataRow dr in drOTCLimits)
					{
						if ( dr["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
						{
							string amt = dr["LIMIT_DEDUC_AMOUNT"].ToString();

							if ( amt == "50" || amt == "100" || amt == "150" || amt == "200" || amt == "250")
							{
								sbXML.Append("<Limit id='"+dr["LIMIT_DEDUC_ID"]+"' amount='"+dr["LIMIT_DEDUC_AMOUNT"]+"' type='"+dr["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
								sbXML.Append("</Limit>");	
							}

						}
					}

					sbXML.Append("</Coverage>");

				}
			}

			return sbXML.ToString();
		}


		#region "Policy Motor rules"

		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetPolicyMotorCoveragesToRemove(int customerID,int appID, int appVersionID, int vehicleID, DataSet objDataSet)
		{
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
			DataTable dtState = objDataSet.Tables[2];		
	
			if ( dtPolicyMotorcycleInfo == null ) return "";

			if ( dtPolicyMotorcycleInfo.Rows.Count == 0 ) return "";
				
	
			string strMotType = "";
			DateTime appEffectiveDate = DateTime.MinValue;
			int stateID = 0;

			
			StringBuilder sbXML = new StringBuilder();
			
			string removeXML = this.GetCommonMotorCoveragesToRemove(objDataSet);
			
			sbXML.Append(removeXML);

		
			if ( dtState != null )
			{
				if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
				{
					stateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
				}
			}
			
						
			//Remove limits based on Motorcycle Type*************************
			if ( dtPolicyMotorcycleInfo.Rows[0]["MOTORCYCLE_TYPE"] != System.DBNull.Value )
			{
				strMotType = dtPolicyMotorcycleInfo.Rows[0]["MOTORCYCLE_TYPE"].ToString();
				
				string xml = GetCoveragesToRemoveBasedOnMotType(objDataSet,strMotType);

				sbXML.Append(xml);
				
			}
			//*********************
			
			//Remove limits based on Motorcycle CC*************************
			int intCC = 0;

			if ( dtPolicyMotorcycleInfo.Rows[0]["VEHICLE_CC"] != System.DBNull.Value )
			{
				intCC = 0;
				intCC = Convert.ToInt32(dtPolicyMotorcycleInfo.Rows[0]["VEHICLE_CC"]);
				
				//Remove limits based on CC
				string xml = this.GetCoveragesToRemoveBasedOnCC(objDataSet,intCC,stateID);

				sbXML.Append(xml);
			}
			//************************************************************


			if ( sbXML.ToString() != "" )
			{		
				return "<Coverages>" + sbXML.Append("</Coverages>").ToString();
			}

			return "";
		}
		
		/// <summary>
		/// Sets each coverage to mandatory or optional based on business rules
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="objDataSet"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		public string GetPolicyMandatoryCoveragesMotorCycle(int customerID,int polID, int polVersionID, 
			int vehicleID, DataSet objDataSet)
		{
			if ( this.dtPolicyMotorcycleInfo == null ) return "";

			if ( dtPolicyMotorcycleInfo.Rows.Count == 0 ) return "";
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtState = objDataSet.Tables[2];

			StringBuilder sbXML = new StringBuilder();
			
			int stateID = 0;

			if ( dtState.Rows[0]["STATE_ID"] != DBNull.Value )
			{
				stateID = Convert.ToInt32(dtState.Rows[0]["STATE_ID"]);
			}
		
			sbXML.Append("<Coverages>");
			
			foreach(DataRow dr in dtCoverage.Rows )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				
				if ( covCode == "MEDPM" && stateID == 22)
				{
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
					sbXML.Append("</Coverage>");
				}
				else
				{
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='N'>");
					sbXML.Append("</Coverage>");
				}
			}	
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
			
		}


		#endregion
		
		#region "Policy Vehicle"

		/// <summary>
		/// Returns an XML containing coverages to be removed
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetPolicyCoveragesToRemove(int customerID,int polID, int polVersionID, int vehicleID, DataSet objDataSet)
		{
			string newUsed = "";
			string strPersVehicleType = "";
			string strVehType = "",strIsSuspended="";

			if ( dtVehicle != null )
			{
				if ( dtVehicle.Rows.Count > 0 )
				{
					if ( dtVehicle.Rows[0]["IS_NEW_USED"] != DBNull.Value )
					{
						newUsed = dtVehicle.Rows[0]["IS_NEW_USED"].ToString();
					}
					
					if ( dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"]  != DBNull.Value )
					{
						strPersVehicleType = dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString();
					}

					if ( dtVehicle.Rows[0]["APP_USE_VEHICLE_ID"]  != DBNull.Value )
					{
						strVehType = dtVehicle.Rows[0]["APP_USE_VEHICLE_ID"].ToString();
					}
					if ( dtVehicle.Rows[0]["IS_SUSPENDED"]  != DBNull.Value )
					{
						strIsSuspended = dtVehicle.Rows[0]["IS_SUSPENDED"].ToString();
					}
				}
			}

			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtLimits = objDataSet.Tables[1];	
			
			//Remove $50 Limit option from (Part E - Oher than collision option) for Application section///
			DataRow[] drOTC = dtCoverage.Select("COV_CODE='COMP'");
					
			if ( drOTC != null  && drOTC.Length > 0 )
			{
						
				string covID = drOTC[0]["COV_ID"].ToString();

				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory='" + drOTC[0]["IS_MANDATORY"].ToString() + "'>");
					
				DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID);

				foreach(DataRow drLimit in drLimits)
				{	
					if ( drLimit["LIMIT_DEDUC_AMOUNT"] != System.DBNull.Value )
					{
						if ( drLimit["LIMIT_DEDUC_AMOUNT"].ToString() == "50" )
						{
							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
						}

					}
				}
				
				sbXML.Append("</Coverage>");
				
			}
			//***End of removal of limits

			string xml = GetAutoCoveragesToRemoveFromXML(StateID,VehicleUse,VehicleType,strIsSuspended,objDataSet);

			sbXML.Append(xml);
			
		
			if ( sbXML.ToString() != "" )
			{		
				return "<Coverages>" + sbXML.Append("</Coverages>").ToString();
			}

			return "";

		}

		/// <summary>
		/// Sets each coverage to mandatory or optional based on business rules
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="objDataSet"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		public string GetPolicyMandatoryCoverages(int customerID,int polID, int polVersionID, 
			int vehicleID, DataSet objDataSet)
		{
			DataTable dtCoverage = objDataSet.Tables[0];
			DataTable dtAppInfo = objDataSet.Tables[2];

			string newUsed = "";
			string strPersVehicleType = "";
			string strVehType = "";
			int stateID = 0;
			
			if ( dtAppInfo != null && dtAppInfo.Rows.Count > 0 )
			{
				if ( dtAppInfo.Rows[0]["STATE_ID"] != DBNull.Value )
				{
					stateID = Convert.ToInt32(dtAppInfo.Rows[0]["STATE_ID"]);
				}
			}

			if ( dtVehicle != null )
			{
				if ( dtVehicle.Rows.Count > 0 )
				{
					if ( dtVehicle.Rows[0]["IS_NEW_USED"] != DBNull.Value )
					{
						newUsed = dtVehicle.Rows[0]["IS_NEW_USED"].ToString();
					}
					
					if ( dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"]  != DBNull.Value )
					{
						strPersVehicleType = dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString();
					}

					if ( dtVehicle.Rows[0]["APP_USE_VEHICLE_ID"]  != DBNull.Value )
					{
						strVehType = dtVehicle.Rows[0]["APP_USE_VEHICLE_ID"].ToString();
					}

				}
			}

			StringBuilder sbXML = new StringBuilder();

			sbXML.Append("<Coverages>");
			
			

			foreach(DataRow dr in dtCoverage.Rows )
			{
				string covID = dr["COV_ID"].ToString();
				string covCode = dr["COV_CODE"].ToString();
				
				sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='N'>");
				sbXML.Append("</Coverage>");
				
				//If Vehicle type is Customized van truck, Motorhome or PPA, or Commercial
				//For Michigan state, Part B - Property Protection Insurance is mandatory

				if ( strPersVehicleType == "11335" || strPersVehicleType == "11336" || strPersVehicleType == "11334" || strVehType == "11333")
				{
					if ( covID == "117" && stateID == 22)
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}

					
				}
				
				//Customized Van or truck 49,251 mandatory
				if ( strPersVehicleType == "11335" )
				{
					if ( covID == "49" || covID == "251")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}
				}

				//For Michigan, PIP is mandatory
				//116		PIP	Personal Injury Protection	22 is mandatory
				if ( stateID == 22 )
				{
					if ( covID == "116")
					{
						sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\""+ dr["COV_code"].ToString() +  "\" Remove=\"N\" Mandatory='Y'>");
						sbXML.Append("</Coverage>");
					}
				}
				
				
			}	
			
			sbXML.Append("</Coverages>");

			return sbXML.ToString();
			
		}


		#endregion

		
		/// <summary>
		/// Updates coverages for motorcycle when it is added/updated
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		public void UpdatePolicyMotorcycleCoverages(int customerID,int polID, int polVersionID, int vehicleID, DataWrapper objDataWrapper)
		{
			if ( objDataWrapper.CommandParameters.Length > 0 )
			{
				objDataWrapper.ClearParameteres();
			}

			//Insert/delete relevant coverages*********************	
			objDataWrapper.AddParameter("@POLICY_ID",polID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objDataWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objDataWrapper.AddParameter("@CUSTOMER_ID",customerID);

			objDataWrapper.ExecuteNonQuery("Proc_Update_MOTORCYCLE_COVERAGES_POLICY");
		}
		

		#region Save Default/Rule Coverages APPLICATION
		

		//Added By shafi
		public override DataTable GetRisksForLobApp(int CustomerId, int AppId, int AppVersionId, int RiskId)
		{
			SqlParameter[] sqlParams = new SqlParameter[3];
			
			sqlParams[0] = new SqlParameter("@APP_ID",AppId);
			sqlParams[1] = new SqlParameter("@APP_VERSION_ID",AppVersionId );
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",CustomerId);
			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_FetchAllVehicleAPP",sqlParams);

			return ds.Tables[0];
				
		}
		//added by Pravesh on 25 march 07
		public override DataTable GetRisksForLobPolicy(int CustomerId, int PolicyId, int PolicyVersionId, int RiskId)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@POL_ID",PolicyId);
			sqlParams[1] = new SqlParameter("@POL_VERSION_ID",PolicyVersionId );
			sqlParams[2] = new SqlParameter("@CUSTOMER_ID",CustomerId);
		
			DataSet ds = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,"Proc_FetchAllVehiclePOL",sqlParams);

			return ds.Tables[0];
		}

		protected override void UpdateCoverageApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId, Coverage cov)
		{
			string strStoredProc="Proc_UPDATE_VEHICLE_RULE_COVERAGES";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_ID",-1);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			

			if(cov.DEDUCTIBLE1_AMOUNT != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",cov.DEDUCTIBLE1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",DBNull.Value);
			}
			if(cov.DEDUCTIBLE2_AMOUNT  != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",cov.DEDUCTIBLE2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",DBNull.Value  );
			}
			if(cov.LIMIT1_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_1",cov.LIMIT1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_1",DBNull.Value );
			}
			if(cov.LIMIT2_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_2",cov.LIMIT2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_2",DBNull.Value);
			}
			

			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			int Cov_Id=	objDataWrapper.ExecuteNonQuery(strStoredProc);
			if (Cov_Id>=0)
			{
				//added by pravesh for Transaction log while default coverages saved
				Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
				base.PupulateCoverageModel(objNew,cov); 
				objNew.COVERAGE_CODE_ID =Cov_Id; 
				objNew.APP_ID =AppId;
				objNew.APP_VERSION_ID =AppVersionId;
				objNew.CUSTOMER_ID	= CustomerId; 
				objNew.CREATED_BY = this.createdby; 
				if (objNew.CREATED_BY==0)
					objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());         
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				string strTranXML="";
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objNew);

//				objTransactionInfo.TRANS_TYPE_ID	=	2;
//				objTransactionInfo.APP_ID = objNew.APP_ID;
//				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
//				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//				objTransactionInfo.TRANS_DESC		=	"Vehicle coverages updated.";
//				objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//				objTransactionInfo.CHANGE_XML		=	strTranXML;
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='LIMIT_1' and @NewValue='0']","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='LIMIT_2' and @NewValue='0']","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='DEDUCTIBLE_1' and @NewValue='0']","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='DEDUCTIBLE_2' and @NewValue='0']","NewValue"," ");
				if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
					sbDefaultTranXML.Append(strTranXML);
				//objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				//end here
				//objDataWrapper.ExecuteNonQuery("Proc_UPDATE_VEHICLE_RULE_COVERAGES"); 
				//objDataWrapper.ClearParameteres();
			}
			objDataWrapper.ClearParameteres();
		}
		//added by pravesh on 14 jan 2008 to save default coverages from database which are entered from system 
		protected override  void SaveDefaultCoveragesFromDB(DataWrapper objDataWrapper,int CustomerId,int AppPolId,int VersionId,int RiskId,string CalledFor)
		{
			
			string strStoredProc="";
			objDataWrapper.ClearParameteres();
			if (CalledFor=="APP")
			{
				strStoredProc="Proc_SAVE_VEHICLE_DEFAULT_COVERAGES_FROMDB";
				objDataWrapper.AddParameter("@APP_ID",AppPolId);
				objDataWrapper.AddParameter("@APP_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@CREATED_BY",this.createdby); 
				objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			else if(CalledFor=="POLICY")
			{
				strStoredProc="Proc_SAVE_VEHICLE_DEFAULT_COVERAGES_FROMDB_POL";
				objDataWrapper.AddParameter("@POLICY_ID",AppPolId);
				objDataWrapper.AddParameter("@POL_VERSION_ID",VersionId);
				objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objDataWrapper.AddParameter("@CREATED_BY",this.createdby); 
				objDataWrapper.ExecuteNonQuery(strStoredProc);
			}

			objDataWrapper.ClearParameteres();
		}
		protected override void SaveCoverageApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId, Coverage cov)
		{
			
			string strStoredProc ="Proc_SAVE_VEHICLE_DEFAULT_COVERAGES";
			string strLobId=this.coverageKeys["LOB_ID"].ToString();
			if(strLobId=="8")
				 strStoredProc ="Proc_SAVE_AVIATION_VEHICLE_DEFAULT_COVERAGES";

			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_ID",-1);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			

			if(cov.DEDUCTIBLE1_AMOUNT != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",cov.DEDUCTIBLE1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",DBNull.Value);
			}
			if(cov.DEDUCTIBLE2_AMOUNT  != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",cov.DEDUCTIBLE2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",DBNull.Value  );
			}
			if(cov.LIMIT1_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_1",cov.LIMIT1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_1",DBNull.Value );
			}
			if(cov.LIMIT2_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_2",cov.LIMIT2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_2",DBNull.Value);
			}
			

			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			//added by pravesh for Transaction log while default coverages saved
			int Cov_ID=objDataWrapper.ExecuteNonQuery(strStoredProc); 
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			if(Cov_ID >=0)
			{
				base.PupulateCoverageModel(objNew,cov); 
				objNew.COVERAGE_CODE_ID =Cov_ID ;
				objNew.APP_ID =AppId;
				objNew.APP_VERSION_ID =AppVersionId;
				objNew.CUSTOMER_ID	= CustomerId; 
				objNew.CREATED_BY	= this.createdby; 
				if (objNew.CREATED_BY==0)
					objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());         
		
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				string strTranXML="";
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				strTranXML = objBuilder.GetTransactionLogXML(objNew);

//				objTransactionInfo.TRANS_TYPE_ID	=	2;
//				objTransactionInfo.APP_ID = objNew.APP_ID;
//				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
//				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//				objTransactionInfo.TRANS_DESC		=	"Vehicle coverages modified.";
//				objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//				objTransactionInfo.CHANGE_XML		=	strTranXML;
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
					sbDefaultTranXML.Append(strTranXML);	
				//objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				//end here
				//objDataWrapper.ExecuteNonQuery("Proc_SAVE_VEHICLE_DEFAULT_COVERAGES"); 
			}
			objDataWrapper.ClearParameteres();
			
		}
		protected override  void WriteTranLogApp(DataWrapper objDataWrapper,int CustomerId, int AppId, int AppVersionId,int RiskId,Coverage cov)
		{
			objDataWrapper.ClearParameteres(); 
			sbDefaultTranXML.Append("</root>"); 
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			base.PupulateCoverageModel(objNew,cov); 
			objNew.APP_ID =AppId;
			objNew.APP_VERSION_ID =AppVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML=sbDefaultTranXML.ToString();
			if (strTranXML !="<root></root>")
			{							   
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
				
				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.APP_ID = objNew.APP_ID;
				objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1496", "");//"Vehicle coverages modified.";
				objTransactionInfo.CUSTOM_INFO		=	"Risk Id= " + RiskId.ToString()+ "<br> Since these are policy level coverages so modified for all vehicles"; //+ objNew.COVERAGE_CODE ;
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}

		}
		protected override  void WriteTranLogPol(DataWrapper objDataWrapper,int  CustomerId, int PolId, int PolVersionId,int RiskId,Coverage cov)
		{
			objDataWrapper.ClearParameteres(); 
			sbDefaultTranXML.Append("</root>"); 
			Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = new Cms.Model.Policy.ClsPolicyCoveragesInfo(); 
			base.PupulateCoverageModel(objNew,cov); 
			objNew.POLICY_ID =PolId;
			objNew.POLICY_VERSION_ID=PolVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY = this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());      
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML=sbDefaultTranXML.ToString();
			int lobID = ClsCommon.GetPolicyLOBID(CustomerId,PolId,PolVersionId);//Done for Itrack issue 6187 on 3 Aug 09
			if (strTranXML !="<root></root>")
			{							   
				objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");

				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
				objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
				objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
				objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
				//Done for Itrack issue 6187 on 3 Aug 09
				if(lobID==3)
					objTransactionInfo.TRANS_DESC		=	"Motorcycle coverages modified.";
				else
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1496", ""); //"Vehicle coverages modified.";
				objTransactionInfo.CUSTOM_INFO		=	"Risk Id= " + RiskId.ToString()+ "<br> Since these are policy level coverages so modified for all vehicles"; //+ objNew.COVERAGE_CODE ;
				//objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString();  //+ " and Coverage Code=" + objNew.COVERAGE_CODE ;
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_1' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='0']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='LIMIT_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeListAttributeValue(strTranXML,"Map[@field='DEDUCTIBLE_2' and @NewValue='-1']","root/LabelFieldMapping","NewValue"," ");
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
			}
		}
		protected override void DeleteCoverageApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId, string strCov_Code)
		{
			string strStoredProc="Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",strCov_Code);
			//added by pravesh for Transaction log while default coverages saved
			Cms.Model.Application.ClsCoveragesInfo objNew = new ClsCoveragesInfo();
			objNew.APP_ID =AppId;
			objNew.APP_VERSION_ID =AppVersionId;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.COVERAGE_CODE= strCov_Code;
			objNew.CREATED_BY=this.createdby;
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());         
		
//			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//			string strTranXML="";
//			objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Coverages.aspx.resx");
//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//			strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);

//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.APP_ID = objNew.APP_ID;
//			objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
//			objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			objTransactionInfo.TRANS_DESC		=	"Vehicle coverages deleted.";
//			objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;

//			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
//				sbDefaultTranXML.Append(strTranXML);	
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE"); 
			objDataWrapper.ClearParameteres();
		}
		

		
		protected override void UpdateEndorsmentApp(DataWrapper objDataWrapper, int CustomerId, int AppId, int AppVersionId, int RiskId)
		{
//			objDataWrapper.ClearParameteres();
//			objDataWrapper.AddParameter("@APP_ID",AppId);
//			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
//			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
//			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_DRIVER_ENDORSEMENTS");
			if ( objDataWrapper.CommandParameters.Length > 0 )
			{
				objDataWrapper.ClearParameteres();
			}
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@APP_ID",AppId);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES");
			objDataWrapper.ClearParameteres();
		}

		#endregion



		#region Save Default/Rule Coverages POLICY
		

		//Added By shafi
		protected override void UpdateCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, Coverage cov)
		{
			string strStoredProc="Proc_UPDATE_POL_VEHICLE_RULE_COVERAGES";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyId );
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_ID",-1);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			

			if(cov.DEDUCTIBLE1_AMOUNT != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",cov.DEDUCTIBLE1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",DBNull.Value);
			}
			if(cov.DEDUCTIBLE2_AMOUNT  != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",cov.DEDUCTIBLE2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",DBNull.Value  );
			}
			if(cov.LIMIT1_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_1",cov.LIMIT1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_1",DBNull.Value );
			}
			if(cov.LIMIT2_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_2",cov.LIMIT2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_2",DBNull.Value);
			}
			

			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			//added by pravesh for Transaction log while default coverages saved
			Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = new Cms.Model.Policy.ClsPolicyCoveragesInfo(); 
			base.PupulateCoverageModel(objNew,cov); 
			objNew.POLICY_ID  = PolicyId; 
			objNew.POLICY_VERSION_ID =PolicyVersionId ;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY	= this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());         
		
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML="";
			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			strTranXML = objBuilder.GetTransactionLogXML(objNew);

			strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='LIMIT_1' and @NewValue='0']","NewValue"," ");
			strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='LIMIT_2' and @NewValue='0']","NewValue"," ");
			strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='DEDUCTIBLE_1' and @NewValue='0']","NewValue"," ");
			strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='DEDUCTIBLE_2' and @NewValue='0']","NewValue"," ");

//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.POLICY_ID  = objNew.POLICY_ID; 
//			objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID ;
//			objTransactionInfo.CLIENT_ID		= objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			objTransactionInfo.TRANS_DESC		=	"Vehicle coverages updated.";
//			objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
				sbDefaultTranXML.Append(strTranXML);	
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_UPDATE_POL_VEHICLE_RULE_COVERAGES"); 
			objDataWrapper.ClearParameteres();

		}

		protected override void SaveCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, Coverage cov)
		{
			
			string strStoredProc="Proc_SAVE_POL_VEHICLE_DEFAULT_COVERAGES";
			string strLobId=this.coverageKeys["LOB_ID"].ToString();
			if(strLobId=="8")
				strStoredProc ="Proc_SAVE_POL_AVIATION_VEHICLE_DEFAULT_COVERAGES";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@Policy_ID",PolicyId);
			objDataWrapper.AddParameter("@Policy_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_ID",-1);
			objDataWrapper.AddParameter("@COVERAGE_CODE",cov.COV_CODE);
			

			if(cov.DEDUCTIBLE1_AMOUNT != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",cov.DEDUCTIBLE1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_1",DBNull.Value);
			}
			if(cov.DEDUCTIBLE2_AMOUNT  != 0)
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",cov.DEDUCTIBLE2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@DEDUCTIBLE_2",DBNull.Value  );
			}
			if(cov.LIMIT1_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_1",cov.LIMIT1_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_1",DBNull.Value );
			}
			if(cov.LIMIT2_AMOUNT !=0)
			{
				objDataWrapper.AddParameter("@LIMIT_2",cov.LIMIT2_AMOUNT );
			}
			else
			{
				objDataWrapper.AddParameter("@LIMIT_2",DBNull.Value);
			}
			

			objDataWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT",cov.LIMIT1_TEXT );
			objDataWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT",cov.LIMIT2_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT",cov.DEDUCTIBLE1_TEXT );
			objDataWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT",cov.DEDUCTIBLE2_TEXT );
			//added by pravesh for Transaction log while default coverages saved
			Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = new Cms.Model.Policy.ClsPolicyCoveragesInfo(); 
			base.PupulateCoverageModel(objNew,cov); 
			objNew.POLICY_ID  = PolicyId; 
			objNew.POLICY_VERSION_ID =PolicyVersionId ;
			objNew.CUSTOMER_ID	= CustomerId; 
			objNew.CREATED_BY	= this.createdby; 
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());         
		
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			string strTranXML="";
			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			strTranXML = objBuilder.GetTransactionLogXML(objNew);
			
			strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='LIMIT_1' and @NewValue='0']","NewValue"," ");
			strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='LIMIT_2' and @NewValue='0']","NewValue"," ");
			strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='DEDUCTIBLE_1' and @NewValue='0']","NewValue"," ");
			strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='DEDUCTIBLE_2' and @NewValue='0']","NewValue"," ");
//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.POLICY_ID  = objNew.POLICY_ID; 
//			objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID ;
//			objTransactionInfo.CLIENT_ID		= objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			objTransactionInfo.TRANS_DESC		=	"Vehicle coverages modified.";
//			objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
				sbDefaultTranXML.Append(strTranXML);	
			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_SAVE_POL_VEHICLE_DEFAULT_COVERAGES"); 
			objDataWrapper.ClearParameteres();
			
		}

		protected override void DeleteCoveragePolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId, string strCov_Code)
		{
			string strStoredProc="Proc_Delete_POL_VEHICLE_COVERAGES_BY_CODE";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@Policy_ID",PolicyId);
			objDataWrapper.AddParameter("@Policy_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.AddParameter("@COVERAGE_CODE",strCov_Code);
			//added by pravesh for Transaction log while default coverages saved
			Cms.Model.Policy.ClsPolicyCoveragesInfo  objNew = new Cms.Model.Policy.ClsPolicyCoveragesInfo(); 
			objNew.POLICY_ID  = PolicyId; 
			objNew.POLICY_VERSION_ID =PolicyVersionId ;
			objNew.CUSTOMER_ID	= CustomerId; 
			 objNew.COVERAGE_CODE =strCov_Code;
			objNew.CREATED_BY=this.createdby;
			if (objNew.CREATED_BY==0)
				objNew.CREATED_BY=  int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());         
		
//			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
//			string strTranXML="";
//			objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyCoverages.aspx.resx");
//			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
//			strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);

//			objTransactionInfo.TRANS_TYPE_ID	=	2;
//			objTransactionInfo.POLICY_ID  = objNew.POLICY_ID; 
//			objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID ;
//			objTransactionInfo.CLIENT_ID		= objNew.CUSTOMER_ID;
//			objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
//			objTransactionInfo.TRANS_DESC		=	"Vehicle coverages modified.";
//			objTransactionInfo.CUSTOM_INFO		=	"Vehicle Id= " + RiskId.ToString()  + " and Coverage Code=" + objNew.COVERAGE_CODE ;
//			objTransactionInfo.CHANGE_XML		=	strTranXML;
			
//			if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
//				sbDefaultTranXML.Append(strTranXML);	

			objDataWrapper.ExecuteNonQuery(strStoredProc);//,objTransactionInfo);
			//end here
			//objDataWrapper.ExecuteNonQuery("Proc_Delete_POL_VEHICLE_COVERAGES_BY_CODE"); 
			objDataWrapper.ClearParameteres();
		}
		

		
		protected override void UpdateEndorsmentPolicy(DataWrapper objDataWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int RiskId)
		{
//			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
//			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
//			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
//			
//			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_DRIVER_ENDORSEMENTS_POLICY");
			if ( objDataWrapper.CommandParameters.Length > 0 )
			{
				objDataWrapper.ClearParameteres();
			}
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionId);
			objDataWrapper.AddParameter("@VEHICLE_ID",RiskId);
			objDataWrapper.ExecuteNonQuery("Proc_UPDATE_VEHICLE_ENDORSEMENTS_FROM_COVERAGES_POLICY");
			objDataWrapper.ClearParameteres();
		}

		#endregion


		
	

	}
}
