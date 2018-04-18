/******************************************************************************************
<Author				: -	Pradeep
<Start Date			: -	Mar 14, 2006
<End Date			: -	
<Description		: -	Class file for Watercraft Endorsements
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 

*******************************************************************************************/ 


using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlApplication ;
using Cms.BusinessLayer.BlCommon ;
using Cms.Model.Application;
using Cms.Model.Policy.Watercraft;
using Cms.Model.Policy;
using Cms.Model.Application.PrivatePassenger;
using Cms.DataLayer;


namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsWatercraftEndorsements.
	/// </summary>
	public class ClsWatercraftEndorsements :  clsapplication 
	{
		public ClsWatercraftEndorsements()
		{
			//
			// TODO: Add constructor logic here
			//
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
		public string GetWatercraftEndorsementsToRemove(int customerID,int appID, int appVersionID, 
			int boatID, DataSet objDataSet,string calledFrom)
		{
						
			StringBuilder sbXML = new StringBuilder();
			
			//if( dsWatercraftInfo==null ) return "";

			//if ( dsWatercraftInfo.Tables.Count == 0 ) return "";

			DataTable dtEnd = objDataSet.Tables[0];
			DataTable dtBoat = objDataSet.Tables[2];
			

			int age = 0;
			double len= 0;
			double insuredValue = 0;
			int year= 0;
		
			sbXML.Append("<Endorsements>");
			
			string strWatercraftType  = "";
			string strWatercraftStyle = "";
			string endCode = "";
			int CoverageType=0;

			//Coverage Type Basis 
			if(dtBoat.Rows[0]["COV_TYPE_BASIS"]!=System.DBNull.Value)	
			{
				CoverageType= Convert.ToInt32(dtBoat.Rows[0]["COV_TYPE_BASIS"]);
			}	

			//Boat type
			if(dtBoat.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value)	
			{
				strWatercraftType  = dtBoat.Rows[0]["TYPE_OF_WATERCRAFT"].ToString().Trim();
			}	
			if(dtBoat.Rows[0]["TYPE"]!=System.DBNull.Value)	
			{
				strWatercraftStyle = dtBoat.Rows[0]["TYPE"].ToString().Trim();
			}
			//Length
			if(dtBoat.Rows[0]["LENGTH"]!=System.DBNull.Value)	
			{
				len = Convert.ToDouble(dtBoat.Rows[0]["LENGTH"]);
			}	
			
			//Insured Value
			if(dtBoat.Rows[0]["INSURING_VALUE"]!=System.DBNull.Value)	
			{
				insuredValue = Convert.ToDouble(dtBoat.Rows[0]["INSURING_VALUE"]);
			}
			
			//Year
			if(dtBoat.Rows[0]["YEAR"]!=System.DBNull.Value)	
			{
				year = Convert.ToInt32(dtBoat.Rows[0]["YEAR"]);
			}

			//Get The Age Of WaterCraft
			if ( objDataSet.Tables[1].Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
			{

				age = ConvertToDate(objDataSet.Tables[1].Rows[0]["APP_EFFECTIVE_DATE"].ToString()).Year - year;			
			}

		
			
			//Read Coverages to remove from XML file and remove the endorsment on the bases of Style od watercraft
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/WatercraftCoverages.xml");
			XmlDocument doc = new XmlDocument();

			doc.Load(filePath);

			XmlNode node = doc.SelectSingleNode("Root/Boat[@ID='" +  strWatercraftStyle + "']");	
			XmlNode removeNode = node.SelectSingleNode("Remove");
			XmlNodeList covList = removeNode.SelectNodes("Endorsement");

			foreach(XmlNode remNode in covList)
			{
				if ( remNode.Attributes["Code"] != null )
				{
					endCode = remNode.Attributes["Code"].Value;
					
					//Find ID from datatable
					DataRow[] dr = dtEnd.Select("ENDORSEMENT_CODE='" + endCode + "'");
					
					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
						sbXML.Append("</Endorsement>");
					}
				}
			}
			
		//Read Coverages to remove from XML file and remove the endorsment on the bases of Type of watercraft
			
			
			

			node = doc.SelectSingleNode("Root/Boat[@ID='" +  strWatercraftType + "']");	
			if (node != null)
			{
					removeNode = node.SelectSingleNode("Remove");
					covList = removeNode.SelectNodes("Endorsement");

					foreach(XmlNode remNode in covList)
					{
							if ( remNode.Attributes["Code"] != null )
							{
								endCode = remNode.Attributes["Code"].Value;

								//Find ID from datatable
								DataRow[] dr = dtEnd.Select("ENDORSEMENT_CODE='" + endCode + "'");

								foreach(DataRow dr1 in dr)
								{
								int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
								//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
								sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
								sbXML.Append("</Endorsement>");
								}
							}
					}
			}
			//Read Coverages to remove from XML file and remove the endorsment on the bases of Style od watercraft


			/////////////////////////////////////////

			/*
				//Trailer, Jetski Trailer, Waverunner Trailer
				//Remove:
				//1.Client Entertainment Liability (OP 720) EBSMECE
				//2.Watercraft Liability Pollution Coverage (OP 900) EBSMWL
				//3.Agreed Value (AV 100) EBSCEAV
				if ( strWatercraftType == "11490" || strWatercraftType == "11445" || strWatercraftType == "11446")
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (279,68,69,280,67,70,66,71,278)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sbXML.Append("</Endorsement>");
						}

					}
				}
				
			

				//Mini Jet Boat
				if ( strWatercraftType == "11373" )
				{
					//Remove Client Entertainment Liability (OP 720) ***************
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (279,68,69)");
					
					if ( dr != null  && dr.Length > 0 )
					{
				
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sbXML.Append("</Endorsement>");
						}

					}
				}
				
				//Jet ski with Lift bar
				if ( strWatercraftType == "11387" )
				{
					//Remove these:
					
					//3. Client Entertainment Liability (OP 720)
					//6. Agreed Value (AV 100) EBSCEAV
					//279,68,69,66,71,278
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (279,68,69,66,71,278)");
				
					if ( dr != null  && dr.Length > 0 )
					{				
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sbXML.Append("</Endorsement>");
						}
						
		
					}
				}
				
				
				//Waverunner
				if ( strWatercraftType == "11386" )
				{
					//Remove these:
					
					//3. Client Entertainment Liability (OP 720)
					//6. Agreed Value (AV 100) EBSCEAV
					//279,68,69,66,71,278
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (279,68,69,66,71,278)");
				
					if ( dr != null  && dr.Length > 0 )
					{				
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sbXML.Append("</Endorsement>");
						}
						
		
					}
				}
			*/
				
			
			//If Actual Cash Value[11758] remove Agreed Value (AV-100) [71]
			//66	71 278

			//Remove AV -100 based on conditions 66,71,278
			if(len>26 || insuredValue>75000 || age > 20 || CoverageType == 11758)
			{
				DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (66,71,278)");
				
				if ( dr != null  && dr.Length > 0 )
				{				
					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
						sbXML.Append("</Endorsement>");
					}
						
		
				}			
			}				

			sbXML.Append("</Endorsements>");	
			return sbXML.ToString();
							
		}//end of if


		/// <summary>
		/// Gets Endorsements from APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="appType"></param>
		/// <returns></returns>
		public  DataSet GetWatercraftEndorsements(int customerID, int appID, 
			int appVersionID, int vehicleID, string appType)
		{
			string	strStoredProc =	"Proc_GetAPP_WATERCRAFT_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@BOAT_ID",vehicleID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			//clsWatercraftInformation objWater = new clsWatercraftInformation();

			string removeXML = this.GetWatercraftEndorsementsToRemove(customerID,
				appID,
				appVersionID,
				vehicleID,
				ds,"");

			if ( removeXML != "" )
			{
				this.RemoveEndorsements(ds,removeXML);
			}
			
			string mandXML = this.GetMandatoryEndorsements(ds);
			
			if ( mandXML != "" )
			{
				this.UpdateMandatoryEndorsements(ds,mandXML);
			}

			return ds;
		
		}
		

		/// <summary>
		/// Gets mandatory endorsments from XML 
		/// </summary>
		/// <param name="boatType"></param>
		/// <param name="ds"></param>
		/// <returns></returns>
		public string GetWatercraftMandatoryEndorsementsFromXML(string boatStyle,string boatType, DataSet ds)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/WatercraftCoverages.xml");
			
			DataTable dtEnd = ds.Tables[0];

			StringBuilder sbXML = new StringBuilder();
		    sbXML.Append("");
			
			XmlDocument doc = new XmlDocument();
			doc.Load(filePath);
            //Make Mandatory Enodrsments on the bases of Style Of watercraft
			XmlNode node = doc.SelectSingleNode("Root/Boat[@ID='" +  boatStyle + "']");
	
		    
			if ( node != null ) 
			{
				//In Case Of Home Make Watercraft Policy (WP-100) Open
				/* this code is commented by Pravesh on 26 april 2007 as pere Issue no.1009 of Coverage as this Endorsement Is mandatory for Home level also
				if(ds.Tables[1].Rows[0]["LOB_ID"].ToString() == "1")
				{
					DataRow[] drWP100 = dtEnd.Select("ENDORSEMENT_CODE='WP100'");
					foreach(DataRow dr1 in drWP100)
					{
						int endorsementIDWP100 = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						//string endCode = dr1["ENDORSEMENT_CODE"].ToString();

						sbXML.Append("<Endorsement ID='" + endorsementIDWP100.ToString() + "' Code='WP100' Remove='N' Type='O'>");
						sbXML.Append("</Endorsement>");	}
              

				}*/

         
				XmlNode removeNode = node.SelectSingleNode("Mandatory/Endorsements");

				if ( removeNode != null ) 
				{

					XmlNodeList endList = removeNode.SelectNodes("Endorsement");
		


					foreach(XmlNode remNode in endList)
					{
						string endCode = remNode.Attributes["Code"].Value;
						string mand = remNode.Attributes["Mandatory"].Value;
					
						//Find ID from datatable
						DataRow[] dr = dtEnd.Select("ENDORSEMENT_CODE='" + endCode + "'");
					
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							//string endCode = dr1["ENDORSEMENT_CODE"].ToString();

							sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='" + mand + "'>");
							sbXML.Append("</Endorsement>");
						}

	
					}
				}
			}
            //Make Mandatory Enodrsments on the bases of Type Of watercraft
		   node = doc.SelectSingleNode("Root/Boat[@ID='" +  boatType + "']");
			if(node!=null) 
			{
			XmlNode	removeNode = node.SelectSingleNode("Mandatory/Endorsements");

				if ( removeNode == null ) return sbXML.ToString();

			   XmlNodeList	 endList = removeNode.SelectNodes("Endorsement");



				foreach(XmlNode remNode in endList)
				{
					string endCode = remNode.Attributes["Code"].Value;
					string mand = remNode.Attributes["Mandatory"].Value;

					//Find ID from datatable
					DataRow[] dr = dtEnd.Select("ENDORSEMENT_CODE='" + endCode + "'");

					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						//string endCode = dr1["ENDORSEMENT_CODE"].ToString();

						sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='" + mand + "'>");
						sbXML.Append("</Endorsement>");
					}


				}
			}

			return sbXML.ToString();
		}

		
		/// <summary>
		/// Prepares mandatory endorsements for Policy accordign to business rules
		/// </summary>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetPolicyMandatoryEndorsements(DataSet objDataSet)
		{
			StringBuilder sbMand = new StringBuilder();

			//string mand = this.GetWatercraftMandatoryEndorsementsFromXML(objDataSet);
			DataTable dtBoat = objDataSet.Tables[2];
			
			string strWatercraftStyle = "";
	        string strWatercraftType  = "";
			//Boat Type
			if(dtBoat.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value)	
			{
				strWatercraftType  = dtBoat.Rows[0]["TYPE_OF_WATERCRAFT"].ToString().Trim();
			}
			//Boat type
			if(dtBoat.Rows[0]["TYPE"]!=System.DBNull.Value)	
			{
				strWatercraftStyle = dtBoat.Rows[0]["TYPE"].ToString().Trim();
			}	
			
			string strMand = this.GetWatercraftMandatoryEndorsementsFromXML(strWatercraftStyle,strWatercraftType,objDataSet);
			
			sbMand.Append("<Endorsements>");
			sbMand.Append(strMand);
			sbMand.Append("</Endorsements>");

			return sbMand.ToString();

		}


		/// <summary>
		/// Prepares mandatory endorsements accordign to business rules
		/// </summary>
		/// <param name="objDataSet"></param>
		/// <returns></returns>
		public string GetMandatoryEndorsements(DataSet objDataSet)
		{
			StringBuilder sbMand = new StringBuilder();

			//string mand = this.GetWatercraftMandatoryEndorsementsFromXML(objDataSet);
			DataTable dtBoat = objDataSet.Tables[2];
			
			string strWatercraftStyle = "";
			string strWatercraftType  = "";
	
			//Boat Type
			if(dtBoat.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value)	
			{
				strWatercraftType  = dtBoat.Rows[0]["TYPE_OF_WATERCRAFT"].ToString().Trim();
			}
			//Boat type
			if(dtBoat.Rows[0]["TYPE"]!=System.DBNull.Value)	
			{
				strWatercraftStyle = dtBoat.Rows[0]["TYPE"].ToString().Trim();
			}
			
			string strMand = this.GetWatercraftMandatoryEndorsementsFromXML(strWatercraftStyle,strWatercraftType,objDataSet);
			
			sbMand.Append("<Endorsements>");
			sbMand.Append(strMand);
			sbMand.Append("</Endorsements>");

			return sbMand.ToString();

		}


		public int SaveWatercraftEndorsementsNew(ArrayList alNewEndorsements,string strOldXML,string hiddenCustomInfo)
		{
			string	strStoredProc =	"Proc_SAVE_WATERCRAFT_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;
			//string strCustomInfo="Following endorsements have been deleted:",str="";

			try
			{
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objNew = (ClsVehicleEndorsementInfo)alNewEndorsements[i];

					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					objTransactionInfo.RECORDED_BY = objNew.CREATED_BY;
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);
                    objWrapper.AddParameter("@EDITION_DATE",objNew.EDITION_DATE);
					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
					//Insert Case
					//if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					if(objNew.ACTION=="I")
					{
						strStoredProc =	"Proc_SAVE_WATERCRAFT_ENDORSEMENTS";
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						//						objTransactionInfo.TRANS_TYPE_ID	=	2;
						//						objTransactionInfo.APP_ID = objNew.APP_ID;
						//						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						//						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						//						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						//						objTransactionInfo.CHANGE_XML		=	strTranXML;
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);					
						objWrapper.ClearParameteres();
						//sbTranXML.Append(strTranXML);

					}
						/*else
						{
							//Update	
							objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
				
							strTranXML = this.GetTranXML(objNew,strOldXML,objNew.VEHICLE_ENDORSEMENT_ID,root);
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
							objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
							objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement updated.";
							objTransactionInfo.CHANGE_XML		=	strTranXML;
							if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
								sbTranXML.Append(strTranXML);
							//sbTranXML.Append(strTranXML);
							//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
							//int retVal = cmdCoverage.ExecuteNonQuery();
							//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
						}*/
					else if(objNew.ACTION=="U")
					{
						strStoredProc =	"Proc_SAVE_WATERCRAFT_ENDORSEMENTS";
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.VEHICLE_ENDORSEMENT_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);					
						objWrapper.ClearParameteres();
					}
					else if(objNew.ACTION=="D")
					{
						strStoredProc =	"Proc_DELETE_WATERCRAFT_ENDORSEMENTS";				
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
						objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
						objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
						objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
						//str+=";" + objNew.ENDORSEMENT;
						objWrapper.ExecuteNonQuery(strStoredProc);				
						objWrapper.ClearParameteres();
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);
						sbTranXML.Append(strTranXML);
					}

				}

				//Insert/Update is complete, start with delete operation

				/*strStoredProc =	"Proc_DELETE_WATERCRAFT_ENDORSEMENTS";				

				for(int i = 0; i < alDeleteEndorsements.Count; i++ )
				{
					Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objDelete = (ClsVehicleEndorsementInfo)alDeleteEndorsements[i];

					objWrapper.AddParameter("@CUSTOMER_ID",objDelete.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objDelete.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objDelete.APP_VERSION_ID);
					objWrapper.AddParameter("@BOAT_ID",objDelete.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objDelete.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objDelete.VEHICLE_ENDORSEMENT_ID);
					objWrapper.ExecuteNonQuery(strStoredProc);				
					objWrapper.ClearParameteres();
				}*/

				sbTranXML.Append("</root>");

				/*if(str!="")
					strCustomInfo+=str;
				else
					strCustomInfo="";

				if(sbTranXML.ToString()!="<root></root>")
					strCustomInfo+=";Following endorsements have been added/updated";
				*/
				objWrapper.ClearParameteres();

				
				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Watercraft endorsement updated.";
					objTransactionInfo.CUSTOM_INFO = hiddenCustomInfo;// + ";" + strCustomInfo;
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
					else
						objTransactionInfo.CHANGE_XML ="";
				

					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				objWrapper.ClearParameteres();
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			return 1;
		
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objNew"></param>
		/// <param name="xml"></param>
		/// <param name="endorsementID"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		private string GetPolicyTranXML(Cms.Model.Policy.ClsPolicyVehicleEndorsementInfo  objNew,string xml,int endorsementID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[VEHICLE_ENDORSEMENT_ID=" + endorsementID.ToString() + "]");
						
			//Cms.Model.Application.ClsCoveragesInfo  objOld = new ClsCoveragesInfo();
			Cms.Model.Policy.ClsPolicyVehicleEndorsementInfo  objOld = new Cms.Model.Policy.ClsPolicyVehicleEndorsementInfo();
			
			objOld.POLICY_ID  = objNew.POLICY_ID;
			objOld.POLICY_VERSION_ID = objNew.POLICY_VERSION_ID;
			objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
			objOld.ENDORSEMENT_ID = objNew.ENDORSEMENT_ID;
						
			XmlNode element = null;

			element = node.SelectSingleNode("ENDORSEMENT_ID");

			if ( element != null)
			{
				objOld.ENDORSEMENT_ID = Convert.ToInt32(element.InnerXml);
			}
						
			element = node.SelectSingleNode("VEHICLE_ENDORSEMENT_ID");
						
			if ( element != null )
			{
				objOld.VEHICLE_ENDORSEMENT_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(element.InnerXml));
			}
			
			element = node.SelectSingleNode("ENDORSEMENT");
						
			if ( element != null )
			{
				objOld.ENDORSEMENT = ClsCommon.DecodeXMLCharacters(element.InnerXml);
			}
			//Added by Sumit for Remarks' section
			element = node.SelectSingleNode("REMARKS");
						
			if ( element != null )
			{
				objOld.REMARKS = ClsCommon.DecodeXMLCharacters(element.InnerXml);
			}

			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

			string strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);

			return strTranXML;
		}


		private string GetTranXML(Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objNew,string xml,int endorsementID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[VEHICLE_ENDORSEMENT_ID=" + endorsementID.ToString() + "]");
						
			//Cms.Model.Application.ClsCoveragesInfo  objOld = new ClsCoveragesInfo();
			Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objOld = new ClsVehicleEndorsementInfo();
			
			objOld.APP_ID = objNew.APP_ID;
			objOld.APP_VERSION_ID = objNew.APP_VERSION_ID;
			objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
			objOld.ENDORSEMENT_ID = objNew.ENDORSEMENT_ID;
						
			XmlNode element = null;

			element = node.SelectSingleNode("ENDORSEMENT_ID");

			if ( element != null)
			{
				objOld.ENDORSEMENT_ID = Convert.ToInt32(element.InnerXml);
			}
						
			element = node.SelectSingleNode("VEHICLE_ENDORSEMENT_ID");
						
			if ( element != null )
			{
				objOld.VEHICLE_ENDORSEMENT_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(element.InnerXml));
			}
			
			element = node.SelectSingleNode("ENDORSEMENT");
						
			if ( element != null )
			{
				objOld.ENDORSEMENT = ClsCommon.DecodeXMLCharacters(element.InnerXml);
			}
			//Added by Sumit for Remarks' section
			element = node.SelectSingleNode("REMARKS");
						
			if ( element != null )
			{
				objOld.REMARKS = ClsCommon.DecodeXMLCharacters(element.InnerXml);
			}

			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

			string strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);

			return strTranXML;
		}

		
		#region "Policy"
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="alNewEndorsements"></param>
		/// <param name="strOldXML"></param>
		/// <param name="hiddenCustomInfo"></param>
		/// <returns></returns>
		public int SaveWatercraftEndorsementsForPolicy(ArrayList alNewEndorsements,string strOldXML,string hiddenCustomInfo)
		{
			string	strStoredProc =	"Proc_SAVE_POLICY_WATERCRAFT_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			
			
			int customerID = 0;
			int polID = 0;
			int polVersionID = 0;
			string strCustomInfo="Following endorsements have been deleted:",str="";

			try
			{
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyVehicleEndorsementInfo objNew = (Cms.Model.Policy.ClsPolicyVehicleEndorsementInfo)alNewEndorsements[i];


					customerID = objNew.CUSTOMER_ID;
					polID = objNew.POLICY_ID;
					polVersionID = objNew.POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY = objNew.CREATED_BY;
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POL_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POL_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);
					objWrapper.AddParameter("@EDITION_DATE",objNew.EDITION_DATE);

					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
					//Insert Case
					//if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					if(objNew.ACTION=="I")
					{
						strStoredProc =	"Proc_SAVE_POLICY_WATERCRAFT_ENDORSEMENTS";
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyEndorsement.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						//						objTransactionInfo.TRANS_TYPE_ID	=	2;
						//						objTransactionInfo.APP_ID = objNew.APP_ID;
						//						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						//						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						//						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						//						objTransactionInfo.CHANGE_XML		=	strTranXML;
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);					
						objWrapper.ClearParameteres();
						//sbTranXML.Append(strTranXML);

					}
						/*else
						{
							//Update	
							objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
				
							strTranXML = this.GetTranXML(objNew,strOldXML,objNew.VEHICLE_ENDORSEMENT_ID,root);
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
							objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
							objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement updated.";
							objTransactionInfo.CHANGE_XML		=	strTranXML;
							if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
								sbTranXML.Append(strTranXML);
							//sbTranXML.Append(strTranXML);
							//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
							//int retVal = cmdCoverage.ExecuteNonQuery();
							//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
						}*/
					else if(objNew.ACTION=="U")
					{
						strStoredProc =	"Proc_SAVE_POLICY_WATERCRAFT_ENDORSEMENTS";
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyEndorsement.aspx.resx");
				
						strTranXML = this.GetPolicyTranXML(objNew,strOldXML,objNew.VEHICLE_ENDORSEMENT_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);					
						objWrapper.ClearParameteres();
					}
					else if(objNew.ACTION=="D")
					{
						strStoredProc =	"Proc_DELETE_POLICY_WATERCRAFT_ENDORSEMENTS";				
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@POL_ID",objNew.POLICY_ID);
						objWrapper.AddParameter("@POL_VERSION_ID",objNew.POLICY_VERSION_ID);
						objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
						objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
						objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
						str+=";" + objNew.ENDORSEMENT;
						objWrapper.ExecuteNonQuery(strStoredProc);				
						objWrapper.ClearParameteres();
					}

				}

				

				sbTranXML.Append("</root>");

				if(str!="")
					strCustomInfo+=str;
				else
					strCustomInfo="";

				if(sbTranXML.ToString()!="<root></root>")
					strCustomInfo+=";Following endorsements have been added/updated";
				
				objWrapper.ClearParameteres();

				
				if(sbTranXML.ToString()!="<root></root>" || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.POLICY_ID  = polID;
					objTransactionInfo.POLICY_VER_TRACKING_ID = polVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Watercraft endorsement updated.";
					objTransactionInfo.CUSTOM_INFO = hiddenCustomInfo + ";" + strCustomInfo;
					if(sbTranXML.ToString()!="<root></root>")
						objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
					else
						objTransactionInfo.CHANGE_XML ="";
				

					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				objWrapper.ClearParameteres();
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			return 1;
		
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polid"></param>
		/// <param name="polVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="polType"></param>
		/// <returns></returns>
		public  DataSet GetWatercraftEndorsementsForPolicy(int customerID, int polid, 
			int polVersionID, int vehicleID, string polType)
		{
			string	strStoredProc =	"Proc_GetPOLICY_WATERCRAFT_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polid);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@BOAT_ID",vehicleID);
			objWrapper.AddParameter("@POL_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			clsWatercraftInformation objWater = new clsWatercraftInformation();

			string removeXML = this.GetWatercraftEndorsementsToRemoveForPolicy(customerID,
				polid,
				polVersionID,
				vehicleID,
				ds,"");

			if ( removeXML != "" )
			{
				this.RemoveEndorsements(ds,removeXML);
			}
			
			string mandXML = this.GetPolicyMandatoryEndorsements(ds);

			if ( mandXML != "" )
			{
				this.UpdateMandatoryEndorsements(ds,mandXML);
			}

			return ds;
		
		}
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="boatID"></param>
		/// <param name="objDataSet"></param>
		/// <param name="calledFrom"></param>
		/// <returns></returns>
		
		public string GetWatercraftEndorsementsToRemoveForPolicy(int customerID,int polID, int polVersionID, 
			int boatID, DataSet objDataSet,string calledFrom)
		{
						
			StringBuilder sbXML = new StringBuilder();
			
			//if( dsWatercraftInfo==null ) return "";

			//if ( dsWatercraftInfo.Tables.Count == 0 ) return "";

			DataTable dtEnd = objDataSet.Tables[0];
			DataTable dtBoat = objDataSet.Tables[2];
			

			int age = 0;
			double len= 0;
			double insuredValue = 0;
			int year= 0;
		
			sbXML.Append("<Endorsements>");
			
			string strWatercraftType = "";
			string strWatercraftStyle = "";
			string endCode = "";
			int CoverageType=0;
		
			//COV_TYPE_BASIS

			if(dtBoat.Rows[0]["COV_TYPE_BASIS"]!=System.DBNull.Value)	
			{
				CoverageType= Convert.ToInt32(dtBoat.Rows[0]["COV_TYPE_BASIS"]);
			}	

			//Boat type
			if(dtBoat.Rows[0]["TYPE_OF_WATERCRAFT"]!=System.DBNull.Value)	
			{
				strWatercraftType = dtBoat.Rows[0]["TYPE_OF_WATERCRAFT"].ToString().Trim();
			}	
			
			//Boat type
			if(dtBoat.Rows[0]["TYPE"]!=System.DBNull.Value)	
			{
				strWatercraftStyle = dtBoat.Rows[0]["TYPE"].ToString().Trim();
			}	
			
			//Length
			if(dtBoat.Rows[0]["LENGTH"]!=System.DBNull.Value)	
			{
				len = Convert.ToDouble(dtBoat.Rows[0]["LENGTH"]);
			}	
			
			//Insured Value
			if(dtBoat.Rows[0]["INSURING_VALUE"]!=System.DBNull.Value)	
			{
				insuredValue = Convert.ToDouble(dtBoat.Rows[0]["INSURING_VALUE"]);
			}
			
			//Year
			if(dtBoat.Rows[0]["YEAR"]!=System.DBNull.Value)	
			{
				year = Convert.ToInt32(dtBoat.Rows[0]["YEAR"]);
			}
			//Get The Age Of WaterCraft
			if ( objDataSet.Tables[1].Rows[0]["APP_EFFECTIVE_DATE"] != System.DBNull.Value)
			{

				age = ConvertToDate(objDataSet.Tables[1].Rows[0]["APP_EFF_DATE"].ToString()).Year - year;			
			}

			
			
			//Read Coverages to remove from XML file
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/WatercraftCoverages.xml");
			XmlDocument doc = new XmlDocument();

			doc.Load(filePath);

			XmlNode node = doc.SelectSingleNode("Root/Boat[@ID='" +  strWatercraftStyle + "']");	

			if ( node == null ) return "";

			XmlNode removeNode = node.SelectSingleNode("Remove");

			if ( removeNode == null ) return "";

			XmlNodeList covList = removeNode.SelectNodes("Endorsement");
			
			if ( covList == null ) return "";

			foreach(XmlNode remNode in covList)
			{
				if ( remNode.Attributes["Code"] != null )
				{
					endCode = remNode.Attributes["Code"].Value;
					
					//Find ID from datatable
					DataRow[] dr = dtEnd.Select("ENDORSEMENT_CODE='" + endCode + "'");
					
					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
						sbXML.Append("</Endorsement>");
					}
				}
			}
			//Read Coverages to remove from XML file and remove the endorsment on the bases of Type of watercraft
			
			
			

			node = doc.SelectSingleNode("Root/Boat[@ID='" +  strWatercraftType + "']");	
			if (node != null)
			{
				removeNode = node.SelectSingleNode("Remove");
				covList = removeNode.SelectNodes("Endorsement");

				foreach(XmlNode remNode in covList)
				{
					if ( remNode.Attributes["Code"] != null )
					{
						endCode = remNode.Attributes["Code"].Value;

						//Find ID from datatable
						DataRow[] dr = dtEnd.Select("ENDORSEMENT_CODE='" + endCode + "'");

						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sbXML.Append("</Endorsement>");
						}
					}
				}
			}

				
			//Remove AV -100 based on conditions 66,71,278
			if(len>26 || insuredValue>75000 || age > 20 || CoverageType == 11758)
			{
				DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (66,71,278)");
				
				if ( dr != null  && dr.Length > 0 )
				{				
					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						//string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sbXML.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
						sbXML.Append("</Endorsement>");
					}
						
		
				}			
			}				

			sbXML.Append("</Endorsements>");	
			return sbXML.ToString();
							
		}//end of if


		#endregion

		#region Endorsements
		/// <summary>
		/// Modifies the dataset according to the settings in the XML string
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="xml"></param>
		public   void RemoveEndorsements(DataSet ds,string xml)
		{
			if ( xml.Trim() == "" ) return;


			XmlDocument xmldoc = new XmlDocument();
	
			xmldoc.LoadXml(xml);

			
			//Endorsements to be removed based on conditions
			XmlNodeList removeList = xmldoc.SelectNodes("Endorsements/Endorsement[@Remove='Y']");
			
			if ( removeList != null)
			{
				foreach(XmlNode node in removeList)
				{
					string endorsementID = node.Attributes["ID"].Value;
					string code = node.Attributes["Code"].Value;
					DataRow[] dr = ds.Tables[0].Select("ENDORSMENT_ID=" + endorsementID + "");
					
					if ( dr != null && dr.Length > 0)
					{
						ds.Tables[0].Rows.Remove(dr[0]);
					}

				}
			}

	
			

		}


		/// <summary>
		/// Modifies the dataset according to the settings in the XML string
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="xml"></param>
		public   void UpdateMandatoryEndorsements(DataSet ds,string xml)
		{
			if ( xml.Trim() == "" ) return;

			XmlDocument xmldoc = new XmlDocument();
	
			xmldoc.LoadXml(xml);
			
			//Endorsements to be removed based on conditions
			XmlNodeList removeList = xmldoc.SelectNodes("Endorsements/Endorsement[@Remove='N']");
			
			if ( removeList != null)
			{
				foreach(XmlNode node in removeList)
				{
					string endorsementID = node.Attributes["ID"].Value;
					string mand = node.Attributes["Type"].Value;

					DataRow[] dr = ds.Tables[0].Select("ENDORSMENT_ID=" + endorsementID + "");
					
					if ( dr != null && dr.Length > 0)
					{
						dr[0]["TYPE"] = mand;
					}

				}
			}
		}


		#endregion
	}
}
