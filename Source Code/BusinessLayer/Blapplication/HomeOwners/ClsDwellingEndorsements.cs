using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlApplication ;
using Cms.BusinessLayer.BlCommon ;
using Cms.Model.Application;
using Cms.Model.Application.HomeOwners;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlApplication.HomeOwners
{
	/// <summary>
	/// Summary description for ClsDwellingEndorsements.
	/// </summary>
	public class ClsDwellingEndorsements :clsapplication 
	{
		public ClsDwellingEndorsements()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public int DeleteDwellingEndorsements(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DELETE_HOME_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sDwellingID = (SqlParameter)objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sID = (SqlParameter)objWrapper.AddParameter("@DWELLING_ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sEndID = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsHomeOwnerEndorsementInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsHomeOwnerEndorsementInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsHomeOwnerEndorsementInfo)alNewCoverages[i]).CUSTOMER_ID;
					sDwellingID.Value = ((ClsHomeOwnerEndorsementInfo)alNewCoverages[i]).DWELLING_ID;
					sID.Value = ((ClsHomeOwnerEndorsementInfo)alNewCoverages[i]).DWELLING_ENDORSEMENT_ID;
					sEndID.Value = ((ClsHomeOwnerEndorsementInfo)alNewCoverages[i]).ENDORSEMENT_ID;

					objWrapper.ExecuteNonQuery(strStoredProc);				
				}
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

			return 1;

		}


		//start
		public int SaveDwellingEndorsementsForPolicy(ArrayList alNewEndorsements,string strOldXML,int userID, int customerID, int polID, int polVersionID)
		{
			string	strStoredProc =	"Proc_SAVE_DWELLING_ENDORSEMENTS_FOR_POLICY";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			StringBuilder sbTranXML = new StringBuilder();


			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			objTransactionInfo.POLICY_ID =polID;
			objTransactionInfo.POLICY_VER_TRACKING_ID  = polVersionID;
			objTransactionInfo.CLIENT_ID = customerID;
			objTransactionInfo.TRANS_TYPE_ID	=	2;
            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1704", "");// "Home endorsements added/updated.";

			try
			{
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo  objNew = (Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo)alNewEndorsements[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@DWELLING_ID",objNew.DWELLING_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@DWELLING_ENDORSEMENT_ID",objNew.DWELLING_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);
					objWrapper.AddParameter("@EDITION_DATE",objNew.EDITION_DATE);

					string strTranXML = "";
					
					if ( objNew.ACTION == "I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyHomeOwnerEndorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);
						
						sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);
						

					}
					else if (objNew.ACTION == "U" )
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Homeowner/PolicyHomeOwnerEndorsements.aspx.resx");
				        this.GetPolicyTranXML( objNew,strOldXML,objNew.DWELLING_ENDORSEMENT_ID,root);
						//strTranXML = this.(objNew,strOldXML,objNew.DWELLING_ENDORSEMENT_ID,root);
						sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);
					}
					
					
					objWrapper.ClearParameteres();

				}
				
				objWrapper.ClearParameteres();

				SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sPolID = (SqlParameter)objWrapper.AddParameter("@POLICY_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sPolVersionID = (SqlParameter)objWrapper.AddParameter("@POLICY_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sDwellingID = (SqlParameter)objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sID = (SqlParameter)objWrapper.AddParameter("@DWELLING_ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sEndID = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);


				//Delete Endorsements here ////////////////////////////////
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo  objInfo = (Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo)alNewEndorsements[i];

					if ( objInfo.ACTION == "D" )
					{
						sPolID.Value = objInfo.POLICY_ID;
						sPolVersionID.Value = objInfo.POLICY_VERSION_ID;
						sCustomerID.Value = objInfo.CUSTOMER_ID;
						sDwellingID.Value = objInfo.DWELLING_ID;
						sID.Value = objInfo.DWELLING_ENDORSEMENT_ID;
						sEndID.Value = objInfo.ENDORSEMENT_ID;

						objWrapper.ExecuteNonQuery("Proc_DELETE_HOME_ENDORSEMENTS_FOR_POLICY");				
					}
				}

				objWrapper.ClearParameteres();
				///////////////////////////////////////////////////////////
				///

				//Transaction log
				

				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			return 1;
		
		}
		

		//end

		/// <summary>
		/// Saves the Dwelling Info in the database
		/// </summary>
		/// <param name="alNewEndorsements"></param>
		/// <param name="strOldXML"></param>
		/// <param name="userID"></param>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <returns></returns>
		public int SaveDwellingEndorsements(ArrayList alNewEndorsements,string strOldXML,int userID, int customerID, int appID, int appVersionID)
		{
			string	strStoredProc =	"Proc_SAVE_DWELLING_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			StringBuilder sbTranXML = new StringBuilder();


			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			objTransactionInfo.APP_ID = appID;
			objTransactionInfo.APP_VERSION_ID = appVersionID;
			objTransactionInfo.CLIENT_ID = customerID;
			objTransactionInfo.TRANS_TYPE_ID	=	2;
            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1704", "");//"Home endorsements added/updated.";

			try
			{
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Application.HomeOwners.ClsHomeOwnerEndorsementInfo objNew = (ClsHomeOwnerEndorsementInfo)alNewEndorsements[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@DWELLING_ID",objNew.DWELLING_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@DWELLING_ENDORSEMENT_ID",objNew.DWELLING_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);
					objWrapper.AddParameter("@EDITION_DATE",objNew.EDITION_DATE);
					
					string strTranXML = "";
					if ( objNew.ACTION == "I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/HomeOwnersEndorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);
						
						sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);
						

					}
					else if (objNew.ACTION == "U" )
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/HomeOwnersEndorsements.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.DWELLING_ENDORSEMENT_ID,root);
						sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);
					}
					
					
					objWrapper.ClearParameteres();

				}
				
				objWrapper.ClearParameteres();

				SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sDwellingID = (SqlParameter)objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sID = (SqlParameter)objWrapper.AddParameter("@DWELLING_ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);
				SqlParameter sEndID = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);


				//Delete Endorsements here ////////////////////////////////
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					ClsHomeOwnerEndorsementInfo objInfo = (ClsHomeOwnerEndorsementInfo)alNewEndorsements[i];

					if ( objInfo.ACTION == "D" )
					{
						sAppID.Value = objInfo.APP_ID;
						sAppVersionID.Value = objInfo.APP_VERSION_ID;
						sCustomerID.Value = objInfo.CUSTOMER_ID;
						sDwellingID.Value = objInfo.DWELLING_ID;
						sID.Value = objInfo.DWELLING_ENDORSEMENT_ID;
						sEndID.Value = objInfo.ENDORSEMENT_ID;

						objWrapper.ExecuteNonQuery("Proc_DELETE_HOME_ENDORSEMENTS");				
					}
				}

				objWrapper.ClearParameteres();
				///////////////////////////////////////////////////////////
				///

				//Transaction log
				

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
		/// Filters the policy rental endorsements acoording to rules
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingId"></param>
		/// <param name="appType"></param>
		/// <returns></returns>
		public  DataSet GetRentalDwellingEndorsementsForPolicy(int customerID, int polID, 
			int polVersionID, int dwellingId, string appType)
		{
			string	strStoredProc =	"Proc_GetPOL_DWELLING_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingId);
			objWrapper.AddParameter("@POLICY_TYPE",appType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

			//Get Modifications XML
			string RemoveXml = GetPolicyRentalEndorsementsToRemove(ds);
			
			string MandatoryXml = GetPolicyMandatoryRentalEndorsements(ds);
			
			RemoveEndorsements(ds,RemoveXml);
			
			UpdateMandatoryEndorsements(ds,MandatoryXml);
				
			
			return ds;
		
		}

		#region Home Dewelling Enforsements

		/// <summary>
		/// Gets dwelling endorsements for Policy
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="polID"></param>
		/// <param name="polVersionID"></param>
		/// <param name="dwellingId"></param>
		/// <param name="polType"></param>
		/// <returns></returns>
		public  DataSet GetDwellingEndorsementsForPolicy(int customerID, int polID, 
			int polVersionID, int dwellingId, string polType)
		{
			string	strStoredProc =	"Proc_GetPOL_DWELLING_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingId);
			objWrapper.AddParameter("@POLICY_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

			//Get Modifications XML
			string RemoveXml = GetPolicyHomeEndorsementsToRemove(ds);
			
			string MandatoryXml = GetMandatoryHomeEndorsements(ds);
			
			RemoveEndorsements(ds,RemoveXml);
			
			UpdateMandatoryEndorsements(ds,MandatoryXml);

			return ds;
		
		}
		
		/// <summary>


		#endregion


			public  DataSet GetRentalDwellingEndorsements(int customerID, int appID, 
				int appVersionID, int dwellingId, string appType)
			{
				string	strStoredProc =	"Proc_GetAPP_DWELLING_ENDORSEMENTS";
			
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
				objWrapper.AddParameter("@CUSTOMER_ID",customerID);
				objWrapper.AddParameter("@APP_ID",appID);
				objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
				objWrapper.AddParameter("@DWELLING_ID",dwellingId);
				objWrapper.AddParameter("@APP_TYPE",appType);
			
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

				//Get Modifications XML
				string RemoveXml = GetRentalEndorsementsToRemove(ds);
			
				string MandatoryXml = GetMandatoryRentalEndorsements(ds);
			
				RemoveEndorsements(ds,RemoveXml);
			
				UpdateMandatoryEndorsements(ds,MandatoryXml);
				
			
				return ds;
		
			}

		public  DataSet GetDwellingEndorsements(int customerID, int appID, 
			int appVersionID, int dwellingId, string appType)
		{
			string	strStoredProc =	"Proc_GetAPP_DWELLING_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingId);
			objWrapper.AddParameter("@APP_TYPE",appType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

			//Get Modifications XML
			string RemoveXml = GetHomeEndorsementsToRemove(ds);
			
			string MandatoryXml = GetMandatoryHomeEndorsements(ds);
			
			RemoveEndorsements(ds,RemoveXml);
			
			UpdateMandatoryEndorsements(ds,MandatoryXml);

			return ds;
		
		}
		
		/// <summary>
		/// Gets the relevant endorsements to remove from XML
		/// </summary>
		/// <param name="productID"></param>
		/// <param name="stateID"></param>
		/// <returns></returns>
		public string GetHomeEndorsementsToRemoveFromXML(int productID, int stateID )
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/HomeEndorsements.xml");
			//string xml = "";
			StringBuilder sb = new StringBuilder();
			
			XmlDocument doc = new XmlDocument();
			doc.Load(filePath);

			XmlNode node = doc.SelectSingleNode("Home/State[@ID=" +  stateID.ToString() + "]");
	
			if ( node == null ) return "";

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + productID.ToString() + "]");

			if ( productNode == null ) return "";

			XmlNode remNode = productNode.SelectSingleNode("Remove/Endorsements");

			if ( remNode == null ) return "";

			XmlNodeList endList = remNode.SelectNodes("Endorsement");

			foreach(XmlNode endNode in endList)
			{
				int endorsementID = int.Parse(endNode.Attributes["ID"].Value);
				
				sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='M'>");
				sb.Append("</Endorsement>");

				
			}
			
			return sb.ToString();
		}
		
		/// <summary>
		/// Returns an XML containing Endorsements to remove
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetPolicyHomeEndorsementsToRemove(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="O"></Endorsement>
			//</Endorsements>
			
			int product = 0;
			int stateID = 0;
			
			DataTable dtEnd = ds.Tables[0];
			DataTable dtHome = ds.Tables[1];

			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0)
				{
					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						product = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

				}
			}
			
			StringBuilder sb=new StringBuilder();	
		 
			sb.Append("<Endorsements>");
			
			string xml = GetHomeEndorsementsToRemoveFromXML(product,stateID);

			sb.Append(xml);
			
			string xmlCommon = GetCommmonHomeEndorsementsToRemove(ds);
			
			sb.Append(xmlCommon);

			sb.Append("</Endorsements>");

			return sb.ToString();
		}
		
		/// <summary>
		/// Gets comon endorsements to remove from application and policy
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public string GetCommmonHomeEndorsementsToRemove(DataSet ds)
		{
			int numWatercrafts = 0;
			
			DataTable dtWater = ds.Tables[4];
			DataTable dtEnd = ds.Tables[0];

			if ( dtWater != null )
			{
				if ( dtWater.Rows.Count > 0)
				{
					if ( dtWater.Rows[0]["COUNT_WATERCRAFTS"] != DBNull.Value )
					{
						numWatercrafts = Convert.ToInt32(dtWater.Rows[0]["COUNT_WATERCRAFTS"]);
					}
				}
			}

			StringBuilder sb = new StringBuilder();	
			
			// If boat added to a home, HO-865 Watercraft Endorsement 
			//should show up on the home policy (HO-2, HO-3, HO-4, HO-5, HO-6, HO-2 Repair, HO-3 Repair)		 
			if ( numWatercrafts == 0 )
			{
				DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (294, 295)"); 

				if ( dr != null  && dr.Length > 0 )
				{
					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}

				}		
			}

			return sb.ToString();

		}

		/// <summary>
		/// Returns an XML containing Endorsements to remove
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetHomeEndorsementsToRemove(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="O"></Endorsement>
			//</Endorsements>
			
			int product = 0;
			int stateID = 0;
			int numWatercrafts = 0;

			DataTable dtEnd = ds.Tables[0];
			DataTable dtHome = ds.Tables[1];
			DataTable dtWater = ds.Tables[4];

			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0)
				{
					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						product = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

				}
			}
			
			if ( dtWater != null )
			{
				if ( dtWater.Rows.Count > 0)
				{
					if ( dtWater.Rows[0]["COUNT_WATERCRAFTS"] != DBNull.Value )
					{
						numWatercrafts = Convert.ToInt32(dtWater.Rows[0]["COUNT_WATERCRAFTS"]);
					}
				}
			}

			StringBuilder sb=new StringBuilder();	
		 
			sb.Append("<Endorsements>");
			
			string xml = GetHomeEndorsementsToRemoveFromXML(product,stateID);

			sb.Append(xml);
			
			string xmlCommon = GetCommmonHomeEndorsementsToRemove(ds);
			
			sb.Append(xmlCommon);
			/*
			//MICHIGAN*************************************************************************************
			if ( stateID == 22 )
			{
				
				#region "Premier Programs"
				
				//HO-3 Premier or HO-5 Premier
				//Remove these endorsements:
				//162           HO-21 Homeowners Preferred Plus (V.I.P.) Endorsement
				//163           HO-23 Homeowners HO-5 Preferred Plus (V.I.P.) Endorsement
				//164           HO-32 Covg A-Special Coverage
				//165           HO-33 Unit Owners-Rental to Others
				//167           HO-35 Loss Assessment Coverage
				//174           HO-51 Building Additions & Alterations-Increased Limits
				//177           HO-64 Renters Deluxe Coverage End.
				//179           HO-66 Condominium Deluxe Covg. End.
				//186           HO-200 Waterbed Liability
				//187           HO-211 Coverage C - Increased Special Limits of Liability
				//191           HO-289 Repair Cost Homeowners End.
				if ( product == 11409 || product == 11410 )
				{
					if ( product == 11409)//3 Premier
					{
						DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (162,163,164,165,167,174,177,179,186,187,191)"); 

						if ( dr != null  && dr.Length > 0 )
						{
							if ( sb.ToString() == "" )
							{
								sb.Append("<Endorsement>");
							}
	
							foreach(DataRow dr1 in dr)
							{
								int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
								string endCode = dr1["ENDORSEMENT_CODE"].ToString();
								sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
								sb.Append("</Endorsement>");
							}

						}		
					}
					else//5 Premier
					{
						DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (161,162,163,164,165,167,174,177,179,186,187,191)"); 
						if ( dr != null  && dr.Length > 0 )
						{
							if ( sb.ToString() == "" )
							{
								sb.Append("<Endorsement>");
							}
	
							foreach(DataRow dr1 in dr)
							{
								int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
								string endCode = dr1["ENDORSEMENT_CODE"].ToString();
								sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
								sb.Append("</Endorsement>");
							}

						}		
					}
					
				}
				else
				{
						////////////////////////////////////////////////////////////////////////
						//Remove these endorsements:
						//200           HO-25 Premier (V.I.P.) Endorsement 
						DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (200)"); 

						
						if ( dr != null  && dr.Length > 0 )
						{
							if ( sb.ToString() == "" )
							{
								sb.Append("<Endorsement>");
							}
		
							foreach(DataRow dr1 in dr)
							{
								int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
								string endCode = dr1["ENDORSEMENT_CODE"].ToString();
								sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
								sb.Append("</Endorsement>");
							}

						}		

				}


				#endregion

				//HO-2//////////////////////////////////////////////////////////////////////////////
				//Remove 187 HO-211 Coverage C - Increased Special Limits of Liability
				//Remove 191 HO-289 Repair Cost Homeowners End.	
				//Remove 186 HO-200 Waterbed Liability	
				if ( product == 11402 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (163,158,164,165,167,169,174,177,179,191,200,187,186)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				//End of HO-2//////////////////////////////////////////////////////////////////////////////

				//HO-3//////////////////////////////////////////////////////////////////////////////
				//Remove 187  HO-211 Coverage C - Increased Special Limits of Liability
				//Remove 191 HO-289 Repair Cost Homeowners End.	
				//Remove 186 HO-200 Waterbed Liability	

				if ( product == 11400 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (163,164,165,167,169,174,177,179,191,200,187,186)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				//End of HO-3//////////////////////////////////////////////////////////////////////////////


				//HO-4//////////////////////////////////////////////////////////////////////////////
				//Remove 187 HO-211 Coverage C - Increased Special Limits of Liability	
				//REmove 172 HO-48 Other Structures-Increased Limits
				//Remove 177 HO-64 Renters Deluxe Coverage End.
				//Remove 159 HO-14 Dwelling Under Construction 
				//Remove 191 HO-289 Repair Cost Homeowners End.
				//Remove 167 HO-35 Loss Assessment Coverage
				//Remove 179 HO-66 Condominium Deluxe Covg. End.
				

				if ( product == 11405 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (158,200,190,163,164,165,159,187,172,191,167,179,177)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				//End of HO-4//////////////////////////////////////////////////////////////////////////////

				//HO-5//////////////////////////////////////////////////////////////////////////////
				//Remove 178 HO-65 Coverage C - Increased Special Limits of Liability 
				//Remove 177 HO-64 Renters Deluxe Coverage End.
				//Remove 191 HO-289 Repair Cost Homeowners End.	
				//Remove 167 HO-35 Loss Assessment Coverage
				//Remove 179 HO-66 Condominium Deluxe Covg. End.
				//Remove 186 HO-200 Waterbed Liability	


				if ( product == 11401 )
				{
					//DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN ('161,162,164,165,174,178'");
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (161,162,164,165,174,178,177,191,167,179,186,200)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				//End of HO-5//////////////////////////////////////////////////////////////////////////////

				//HO-6//////////////////////////////////////////////////////////////////////////////
				//Remove 187  HO-211 Coverage C - Increased Special Limits of Liability 
				
				//Remove 177 HO-64 Renters Deluxe Coverage End.
				//Remove 159 HO-14 Dwelling Under Construction 
				//Remove 191 HO-289 Repair Cost Homeowners End.	
				//HO-66 Condominium Deluxe Covg. End. 179

				if ( product == 11406 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (200,158,190,163,174,159,187,177,191,179)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				//End of HO-6//////////////////////////////////////////////////////////////////////////////

				//HO-4 Deluxe//////////////////////////////////////////////////////////////////////////////
				//Remove 187 HO-211 Coverage C - Increased Special Limits of Liability 
				//Remove 172 HO-48 Other Structures-Increased Limits
				//Remove 159 HO-14 Dwelling Under Construction 
				//Remove 191 HO-289 Repair Cost Homeowners End.	
				//Remove 167 HO-35 Loss Assessment Coverage
				//Remove 179 HO-66 Condominium Deluxe Covg. End.


				if ( product == 11407 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (158,200,190,163,164,165,159,187,172,191,167,179)");


					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				//End of HO-4 Deluxe////////////////////////////////////////////////////////////////////

				//HO-6 Deluxe/////////////////////////////////////////////////////////////
				//Remove 187 HO-211 Coverage C - Increased Special Limits of Liability 
				
				//Remove 177 HO-64 Renters Deluxe Coverage End.
				//Remove 159 HO-14 Dwelling Under Construction 
				//Remove 191 HO-289 Repair Cost Homeowners End.	

				if ( product == 11408 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (200,158,190,174,163,159,187,177,191)");


					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				//End of HO-6 Deluxe///////////////////////////////////////////////////////////

				//HO-2 Repair Cost and HO-3 Repair Cost//////////////////////////////////////////////////
				//Remove 187 HO-211 Coverage C - Increased Special Limits of Liability 
				//Remove 177 HO-64 Renters Deluxe Coverage End.
				//Remove 167 HO-35 Loss Assessment Coverage
				//Remove 179 HO-66 Condominium Deluxe Covg. End.
				//Remove 186 HO-200 Waterbed Liability	


				if ( product == 11403 || product == 11404 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (163,158,164,165,174,200,187,177,167,179,186)");

					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				//End of HO-2 Repair Cost and HO-3 Repair Cost/////////////////////////////////////////////////
		
			}//END OF MICHIGAN*************************************************************************************
			else	//INDIANA
			{
				//HO-2 Repair Cost and HO-3 Repair Cost  (11193 and 11194)
				//////////////////////////////////////////////////
				//Remove 237  HO-211 Coverage C - Increased Special Limits of Liability 
				//Remove 214 HO-24 Premier (V.I.P.) End.
				//Remove 227 HO-64 Renters Deluxe Coverage End.
				//Remove 236 HO-200 Waterbed Liability	
				//Remove 277 HO-35 Loss Assessment Coverage	
				

				if ( product == 11193 || product == 11194 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (237,214,227,236,277,209,215,216,229,224)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}

				//End of HO-2 Repair cost and HO-3 Repair cost

				//HO-2///////////
				//Remove these:
				//Remove 214 HO-24 Premier (V.I.P.) End.
				//Remove 243 HO-289 Repair Cost Homeowners End.	
				//Remove 236 HO-200 Waterbed Liability	
				//Remove 277 HO-35 Loss Assessment Coverage	
			
				if ( product == 11192)
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (209,215,216,224,227,229,237,243,214,236,277)");

					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
				}
				///////End of HO-2
				///
				
				///////////////HO-3///////////
				//Remove these:
				//243 HO-289 Repair Cost Homeowners End.
				//Remove 236 HO-200 Waterbed Liability	
				//Remove 277 HO-35 Loss Assessment Coverage	
			
				if ( product == 11148)
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (215,216,224,227,229,237,243,236,277)");

					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
				}
				///////End of HO-3
				

				//HO-4///////////
				//Remove 237  HO-211 Coverage C - Increased Special Limits of Liability 
				//HO-14 Dwelling Under Construction 210
				//Remove 222  HO-48 Other Structures-Increased Limits
				//Remove 227 HO-64 Renters Deluxe Coverage End.
				//Remove 241 HO-277 Ordinance or Law Coverage
				//Remove 208 HO-9 Collapse From Sub-Surface Water	
				//Remove 210 HO-14 Dwelling Under Construction 
				//Remove 243 HO-289 Repair Cost Homeowners End.	
				//Remove 277 HO-35 Loss Assessment Coverage	
				//Remove 214 HO-24 Premier (V.I.P.) End.
				//Remove 209 HO-11 Building Replacement Cost Provision Expanded Coverage	
				//216 	HO-33 Unit Owners-Rental to Others	
				//215	HO-32 Coverage A-Special Coverage



				if ( product == 11195 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (210,237,222,241,208,243,277,214,209,216,229,215,227)");

					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
				}
				///////
				///
				//HO-4 Deluxe
				//Remove these:
				//HO-14 Dwelling Under Construction 210
				//Remove 237  HO-211 Coverage C - Increased Special Limits of Liability 
				//Remove 222  HO-48 Other Structures-Increased Limits
				//Remove 241 HO-277 Ordinance or Law Coverage
				//Remove 208 HO-9 Collapse From Sub-Surface Water
				//Remove 210 HO-14 Dwelling Under Construction 
				//Remove 243 HO-289 Repair Cost Homeowners End.	
				//Remove 277 HO-35 Loss Assessment Coverage	
				//Remove 214 HO-24 Premier (V.I.P.) End.

				if ( product == 11245 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (210,237,222,241,208,243,277,214,209,216,229,215)");

					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
				}
				///
				//Added on 09 jan 06
				//HO-5///////////
				//Remove these:
				
				//212 HO-20 Homeowners "Preferred Plus End
				//213 HO-22 Homowners "Preferred Plus (V.I.P.) End.
				//224 HO-51 Building Additions & Alterations-Increased Limits
				//227 HO-64 Renters Deluxe Coverage End.
				//243 HO-289 Repair Cost Homeowners End.
				//236 HO-200 Waterbed Liability	
				//277 HO-35 Loss Assessment Coverage	

				if ( product == 11149 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (212,213,224,227,243,236,277,215,229)");

					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
				}
				///Edn of HO-5


				///HO-6////////
				/////Remove these:
				//HO-14 Dwelling Under Construction 210
				//Remove 237  HO-211 Coverage C - Increased Special Limits of Liability 
				
				//Remove 224 HO-51 Building Additions & Alterations-Increased Limits
				//Remove 227 HO-64 Renters Deluxe Coverage End.
				//Remove 241 HO-277 Ordinance or Law Coverage
				//Remove 208 HO-9 Collapse From Sub-Surface Water	
				//Remove 210 HO-14 Dwelling Under Construction 
				//Remove 243 HO-289 Repair Cost Homeowners End.	
				//Remove 214 HO-24 Premier (V.I.P.) End.
				//229 HO-66 Condominium Deluxe

				if ( product == 11196 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (210,237,224,227,241,208,243,214,209,229)");

					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
				}
				///
				//HO-6 Deluxe///////
				//Remove these:
				//HO-14 Dwelling Under Construction 210
				//Remove 237  HO-211 Coverage C - Increased Special Limits of Liability 
				
				//Remove 224 HO-51 Building Additions & Alterations-Increased Limits
				//Remove 227 HO-64 Renters Deluxe Coverage End.
				//Remove 241 HO-277 Ordinance or Law Coverage
				//Remove 208 HO-9 Collapse From Sub-Surface Water	
				//Remove 210 HO-14 Dwelling Under Construction 
				//Remove 243 HO-289 Repair Cost Homeowners End.	
				//Remove 214 HO-24 Premier (V.I.P.) End.

				if ( product == 11246 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (210,237,224,227,241,208,243,214,209)");

					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
				}
			}
			*/
			
			sb.Append("</Endorsements>");

			return sb.ToString();
		}
		
		/// <summary>
		/// Prepares an XML containing endorsements to remove by reading from XML file
		/// </summary>
		/// <param name="stateID"></param>
		/// <param name="productID"></param>
		/// <returns></returns>
		private string GetRentalEndorsementsToRemoveFromXML(int stateID, int productID)
		{
			StringBuilder sb = new StringBuilder();


			//Read Coverages to remove from XML file
			XmlDocument doc = new XmlDocument();
			
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/RentalCoverages.xml");
			doc.Load(filePath);

			XmlNode node = doc.SelectSingleNode("Root/State[@ID=" +  stateID.ToString() + "]");	

			if ( node == null ) return "";

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + productID.ToString() + "]");

			if ( productNode == null ) return "";

			XmlNode removeNode = productNode.SelectSingleNode("Remove/Endorsements");

			if ( removeNode == null ) return "";

			XmlNodeList endList = removeNode.SelectNodes("Endorsement");
			
			if ( endList == null ) return "";

			foreach(XmlNode remNode in endList)
			{
				string endorsementID = remNode.Attributes["ID"].Value;
				
				sb.Append("<Endorsement ID='" + endorsementID + "' Remove='Y' Type='O'>");
				sb.Append("</Endorsement>");
			}

			return sb.ToString();

		}
		
		/// <summary>
		/// Returns an XML with endorsments to remove
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetPolicyRentalEndorsementsToRemove(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="O"></Endorsement>
			//</Endorsements>
			
			int product = 0;
			int stateID = 0;
			int numLocAlarmsApplies = 0;

			DataTable dtEnd = ds.Tables[0];
			DataTable dtHome = ds.Tables[1];
			DataTable dtRating = ds.Tables[3];
			
			
			//string xml = "";

			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0)
				{
					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						product = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

				}
			}
			
			if ( dtRating != null )
			{
				if ( dtRating.Rows.Count > 0)
				{
					if ( dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"] != DBNull.Value )
					{
						numLocAlarmsApplies = Convert.ToInt32(dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"]);
					}
				}
			}
			
			StringBuilder sb=new StringBuilder();	

			sb.Append("<Endorsements>");

			string removeXML = GetRentalEndorsementsToRemoveFromXML(stateID,product);
				
			sb.Append(removeXML);
					
			sb.Append("</Endorsements>");

			return sb.ToString();
		}
		

		public  string GetRentalEndorsementsToRemove(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="O"></Endorsement>
			//</Endorsements>
			
			int product = 0;
			int stateID = 0;
			int numLocAlarmsApplies = 0;

			DataTable dtEnd = ds.Tables[0];
			DataTable dtHome = ds.Tables[1];
			DataTable dtRating = ds.Tables[3];
			
			
			//string xml = "";

			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0)
				{
					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						product = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

				}
			}
			
			if ( dtRating != null )
			{
				if ( dtRating.Rows.Count > 0)
				{
					if ( dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"] != DBNull.Value )
					{
						numLocAlarmsApplies = Convert.ToInt32(dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"]);
					}
				}
			}
			
			StringBuilder sb=new StringBuilder();	

			sb.Append("<Endorsements>");

		
			string removeXML = GetRentalEndorsementsToRemoveFromXML(stateID,product);
				
			sb.Append(removeXML);
			/*	
			//MICHIGAN*************************************************************************************
			if ( stateID == 22 )
			{


				//Premier//////////////////////////////////////////////////////////////////////////////
				if ( product == 11458 )
				{
					//Repair Cost End
					//Dwelling Under Construction (Builders Risk)
					//Installation Floater
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (267,271,272)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
				else
				{
					//Non premier program
					//Premier Tree Debris Removal
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (276)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
				}

			
			}//END OF MICHIGAN*************************************************************************************			
			
			//MICHIGAN*************************************************************************************
			if ( stateID == 22 )
			{
				
				//Premier//////////////////////////////////////////////////////////////////////////////
				if ( product != 11290 && product != 11292 )
				{
					//Repair Cost End
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (267)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
			}

			//Indiana*************************************************************************************
			if ( stateID == 14 )
			{
			

				if ( product != 11480 && product != 11482 )
				{
					
                 //Repair Cost End
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (254)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='Y' Type='O'>");
							sb.Append("</Endorsement>");
						}

					}
					
				}
			}
			*/
			
			sb.Append("</Endorsements>");

			return sb.ToString();
		}
		
		/// <summary>
		/// Modifies the dataset according to the settings in the XML string
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="xml"></param>
		public static  new void RemoveEndorsements(DataSet ds,string xml)
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
					string code = "";

					if ( node.Attributes["Code"] != null)
					{
						code = node.Attributes["Code"].Value;
					}

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
		public static  void UpdateMandatoryEndorsements(DataSet ds,string xml)
		{

			if ( xml.Trim() == "" ) return;

			XmlDocument xmldoc = new XmlDocument();
	
			xmldoc.LoadXml(xml);

			//Endorsements to be made mandatory or optional based on condition
			XmlNodeList mandatoryList = xmldoc.SelectNodes("Endorsements/Endorsement[@Remove='N']");

			if ( mandatoryList != null)
			{
				foreach(XmlNode node in mandatoryList)
				{
					
					DataRow[] dr = ds.Tables[0].Select("Endorsment_ID=" + node.Attributes["ID"].Value);
					
					if ( dr != null )
					{
						if ( dr.Length > 0 )
						{
							dr[0]["TYPE"] = node.Attributes["Type"].Value;
						}
					}

				}
			}

		}

		/// <summary>
		/// Gets the mandatory home endorsments
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetMandatoryHomeEndorsements(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="M"></Endorsement>			
			//</Endorsements>

			StringBuilder sb=new StringBuilder();		
	 
			int product = 0;
			int stateID = 0;
			
			DataTable dtEnd = ds.Tables[0];
			DataTable dtHome = ds.Tables[1];
			

			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0)
				{
					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						product = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

				}
			}
			
			
		 
			sb.Append("<Endorsements>");
			/*HO-4 Deluxe,HO-4,HO-6,HO-6 Deluxe
			 * HO-48 Other Structures-Increased Limits Is Non Mandatory For these Products*/
			

			//Michigan
			if(stateID==22)
			{

				DataRow[] drh = dtEnd.Select("ENDORSMENT_ID IN (172)");
                if( product != 11407 && product != 11405 && product != 11406 && product != 11408)
				{
					foreach(DataRow dr1 in drh)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}

				}
				DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (149,150,151,152,153,154,155,156,157,178,187)");
					
				if ( dr != null  && dr.Length > 0 )
				{
					if ( sb.ToString() == "" )
					{
						sb.Append("<Endorsement>");
					}
	
					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}
				}


				
						
			}	
			else if(stateID==14)
			{
				//Indiana
				DataRow[] drh = dtEnd.Select("ENDORSMENT_ID IN (222)");
    		    if( product != 11195 && product != 11245 && product != 11196 && product != 11246)
				{
					foreach(DataRow dr1 in drh)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}

				}
				DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (201,202,203,204,205,206,207,228,237)");
					
				if ( dr != null  && dr.Length > 0 )
				{
					if ( sb.ToString() == "" )
					{
						sb.Append("<Endorsement>");
					}
	
					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}
				}

				
				
			}
			//HO-48 Other Structures-Increased Limits 

			sb.Append("</Endorsements>");

			
			return sb.ToString();
		}
		
		/// <summary>
		/// Returns an XML containing mandatory coverages
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetPolicyMandatoryRentalEndorsements(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="M"></Endorsement>			
			//</Endorsements>

			StringBuilder sb=new StringBuilder();		
	 
			int product = 0;
			int stateID = 0;
			int numLocAlarmsApplies = 0;

			DataTable dtEnd = ds.Tables[0];
			DataTable dtHome = ds.Tables[1];
			DataTable dtRating = ds.Tables[3];

			//Table containing Coverage/Limits information
			DataTable dtCovLimits = ds.Tables[2];
			
			decimal liabLimit = 0;
			decimal medLimit = 0;
			
			if ( dtRating != null )
			{
				if ( dtRating.Rows.Count > 0)
				{
					if ( dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"] != DBNull.Value )
					{
						numLocAlarmsApplies = Convert.ToInt32(dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"]);
					}
				}
			}

			if ( dtCovLimits.Rows.Count > 0 )
			{
				if ( dtCovLimits.Rows[0]["PERSONAL_LIAB_LIMIT"] != DBNull.Value )
				{
					liabLimit = Convert.ToDecimal(dtCovLimits.Rows[0]["PERSONAL_LIAB_LIMIT"]);
				}
			
				if ( dtCovLimits.Rows[0]["MED_PAY_EACH_PERSON"] != DBNull.Value )
				{
					medLimit = Convert.ToDecimal(dtCovLimits.Rows[0]["MED_PAY_EACH_PERSON"]);
				}
			}


			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0)
				{
					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						product = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

				}
			}
			
		
			sb.Append("<Endorsements>");
			
			string xml = GetRentalMandatoryEndorsementsFromXML(stateID,product);

		
			if(stateID==22)
			{
				
				//DP-382 Lead Liability Exclusion depending on Med payments and Personal Liability
				DataRow[] drMand = dtEnd.Select("ENDORSMENT_ID IN (268)");
				string mand = "N";

				foreach(DataRow dr1 in drMand)
				{
					if (liabLimit == 0 && medLimit == 0) // E, F not taken
					{
						mand = "O";
					}
					else // E & F Coverages taken
					{
						mand = "M";
					}

					int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
					string endCode = dr1["ENDORSEMENT_CODE"].ToString();
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='" + mand + "'>");
					sb.Append("</Endorsement>");
				}
		
			}	
			else if(stateID==14)
			{
				//DP-382 Lead Liability Exclusion depending on Med payments and Personal Liability
				DataRow[] drMand = dtEnd.Select("ENDORSMENT_ID IN (255)");
				string mand = "N";

				foreach(DataRow dr1 in drMand)
				{
					if (liabLimit == 0 && medLimit == 0) // E, F not taken
					{
						mand = "O";
					}
					else // E & F Coverages taken
					{
						mand = "M";
					}

					int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
					string endCode = dr1["ENDORSEMENT_CODE"].ToString();
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='" + mand + "'>");
					sb.Append("</Endorsement>");
				}
				//End for DP 392

			}
			sb.Append("</Endorsements>");

			
			return sb.ToString();
		}
		
		/// <summary>
		/// Returns an XML with mandatory coverages read from XML file
		/// </summary>
		/// <param name="stateID"></param>
		/// <param name="product"></param>
		/// <returns></returns>
		public string GetRentalMandatoryEndorsementsFromXML(int stateID, int product)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/RentalCoverages.xml");
			//string xml = "";
			StringBuilder sb = new StringBuilder();
			
			XmlDocument doc = new XmlDocument();
			doc.Load(filePath);

			XmlNode node = doc.SelectSingleNode("Rental/State[@ID=" +  stateID.ToString() + "]");
	
			if ( node == null ) return "";

			XmlNode productNode = node.SelectSingleNode("Product[@ID=" + product.ToString() + "]");

			if ( productNode == null ) return "";

			XmlNode mandNode = productNode.SelectSingleNode("Mandatory/Endorsements");

			if ( mandNode == null ) return "";

			XmlNodeList endList = mandNode.SelectNodes("Endorsement");

			foreach(XmlNode endNode in endList)
			{
				int endorsementID = int.Parse(endNode["ENDORSMENT_ID"].ToString());
				string endCode = endNode["ENDORSEMENT_CODE"].ToString();
				sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
				sb.Append("</Endorsement>");

				
			}
			
			return sb.ToString();
		}


		/// <summary>
		/// Returns an XML containing mandatory coverages
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetMandatoryRentalEndorsements(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="M"></Endorsement>			
			//</Endorsements>

			StringBuilder sb=new StringBuilder();		
	 
			int product = 0;
			int stateID = 0;
			int numLocAlarmsApplies = 0;

			DataTable dtEnd = ds.Tables[0];
			DataTable dtHome = ds.Tables[1];
			DataTable dtRating = ds.Tables[3];

			//Table containing Coverage/Limits information
			DataTable dtCovLimits = ds.Tables[2];
			
			decimal liabLimit = 0;
			decimal medLimit = 0;
			
			if ( dtRating != null )
			{
				if ( dtRating.Rows.Count > 0)
				{
					if ( dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"] != DBNull.Value )
					{
						numLocAlarmsApplies = Convert.ToInt32(dtRating.Rows[0]["NUM_LOC_ALARMS_APPLIES"]);
					}
				}
			}

			if ( dtCovLimits.Rows.Count > 0 )
			{
				if ( dtCovLimits.Rows[0]["PERSONAL_LIAB_LIMIT"] != DBNull.Value )
				{
					liabLimit = Convert.ToDecimal(dtCovLimits.Rows[0]["PERSONAL_LIAB_LIMIT"]);
				}
			
				if ( dtCovLimits.Rows[0]["MED_PAY_EACH_PERSON"] != DBNull.Value )
				{
					medLimit = Convert.ToDecimal(dtCovLimits.Rows[0]["MED_PAY_EACH_PERSON"]);
				}
			}


			if ( dtHome != null )
			{
				if ( dtHome.Rows.Count > 0)
				{
					if ( dtHome.Rows[0]["STATE_ID"] != DBNull.Value )
					{
						stateID = Convert.ToInt32(dtHome.Rows[0]["STATE_ID"]);
					}

					if ( dtHome.Rows[0]["POLICY_TYPE"] != DBNull.Value )
					{
						product = Convert.ToInt32(dtHome.Rows[0]["POLICY_TYPE"]);
					}

				}
			}
			
			
		 
			sb.Append("<Endorsements>");
			
			string xml = GetRentalMandatoryEndorsementsFromXML(stateID,product);

			sb.Append(xml);

			/*
			if(stateID==22) //MICHIGAN
			{
				DataRow[] drArray = dtEnd.Select("ENDORSMENT_ID=271 or  ENDORSMENT_ID=272 OR ENDORSMENT_ID=273 OR ENDORSMENT_ID=274 OR ENDORSMENT_ID=269");

				//Mandatory endorsements for all products
				//258,271
				// For Michigan, Product - All, setting Mandatory Endorsements
				if ( drArray != null  && drArray.Length > 0 )
				{
					foreach(DataRow dr1 in drArray)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}
				}

				//DP-216 Premises Alarm or Fire Protection System 266
			
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (266)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
							sb.Append("</Endorsement>");
						}

					}
				

			}
			else if(stateID==14)//INDIANA
			{

				DataRow[] drArray = dtEnd.Select("ENDORSMENT_ID=258 or  ENDORSMENT_ID=259 OR ENDORSMENT_ID=260 or  ENDORSMENT_ID=261 OR ENDORSMENT_ID=255");

				//Mandatory endorsements for all products
				//258,271
				// For Michigan, Product - All, setting Mandatory Endorsements
				if ( drArray != null  && drArray.Length > 0 )
				{
					foreach(DataRow dr1 in drArray)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}
				}

				//DP-216 Premises Alarm or Fire Protection System 253
				
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (253)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
							sb.Append("</Endorsement>");
						}

					}
				


			}
			*/
			
			if(stateID==22)
			{
				
				//DP-382 Lead Liability Exclusion depending on Med payments and Personal Liability
				DataRow[] drMand = dtEnd.Select("ENDORSMENT_ID IN (268)");
				string mand = "N";

				foreach(DataRow dr1 in drMand)
				{
					if (liabLimit == 0 && medLimit == 0) // E, F not taken
					{
						mand = "O";
					}
					else // E & F Coverages taken
					{
						mand = "M";
					}

					int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
					string endCode = dr1["ENDORSEMENT_CODE"].ToString();
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='" + mand + "'>");
					sb.Append("</Endorsement>");
				}

				
				/*
				//Premier//////////////////////////////////////////////////////////////////////////////
				if ( product == 11458 )
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (262,263,276,264,265)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
							sb.Append("</Endorsement>");
						}
					}
				}
				else
				{
					DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (262,263,264,265)");
					
					if ( dr != null  && dr.Length > 0 )
					{
						if ( sb.ToString() == "" )
						{
							sb.Append("<Endorsement>");
						}
	
						foreach(DataRow dr1 in dr)
						{
							int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
							string endCode = dr1["ENDORSEMENT_CODE"].ToString();
							sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
							sb.Append("</Endorsement>");
						}
					}
				}*/
						
			}	
			else if(stateID==14)
			{
				//DP-382 Lead Liability Exclusion depending on Med payments and Personal Liability
				DataRow[] drMand = dtEnd.Select("ENDORSMENT_ID IN (255)");
				string mand = "N";

				foreach(DataRow dr1 in drMand)
				{
					if (liabLimit == 0 && medLimit == 0) // E, F not taken
					{
						mand = "O";
					}
					else // E & F Coverages taken
					{
						mand = "M";
					}

					int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
					string endCode = dr1["ENDORSEMENT_CODE"].ToString();
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='" + mand + "'>");
					sb.Append("</Endorsement>");
				}
				//End for DP 392


				/*
				DataRow[] dr = dtEnd.Select("ENDORSMENT_ID IN (249,250,251,252)");
					
				if ( dr != null  && dr.Length > 0 )
				{
					if ( sb.ToString() == "" )
					{
						sb.Append("<Endorsement>");
					}
	
					foreach(DataRow dr1 in dr)
					{
						int endorsementID = int.Parse(dr1["ENDORSMENT_ID"].ToString());
						string endCode = dr1["ENDORSEMENT_CODE"].ToString();
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Code='" + endCode + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}
				}*/

			}
			sb.Append("</Endorsements>");

			
			return sb.ToString();
		}
		

		/// <summary>
		/// Gets the transaction log XML for Policy coverages
		/// </summary>
		/// <param name="objNew"></param>
		/// <param name="xml"></param>
		/// <param name="endorsementID"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		private string GetPolicyTranXML(Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo   objNew,string xml,int endorsementID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[DWELLING_ENDORSEMENT_ID=" + endorsementID.ToString() + "]");
						
			//Cms.Model.Application.ClsCoveragesInfo  objOld = new ClsCoveragesInfo();
			Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo   objOld = new Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo();
			
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
						
			element = node.SelectSingleNode("DWELLING_ENDORSEMENT_ID");
						
			if ( element != null )
			{
				objOld.DWELLING_ENDORSEMENT_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(element.InnerXml));
			}
			
			element = node.SelectSingleNode("ENDORSEMENT");
						
			if ( element != null )
			{
				objOld.ENDORSEMENT = ClsCommon.DecodeXMLCharacters(element.InnerXml);
			}

			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

			string strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);

			return strTranXML;
		}



//end

		private string GetTranXML(ClsHomeOwnerEndorsementInfo objNew,string xml,int endorsementID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[DWELLING_ENDORSEMENT_ID=" + endorsementID.ToString() + "]");
						
			//Cms.Model.Application.ClsCoveragesInfo  objOld = new ClsCoveragesInfo();
			Cms.Model.Application.HomeOwners.ClsHomeOwnerEndorsementInfo objOld = new ClsHomeOwnerEndorsementInfo();
			
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
						
			element = node.SelectSingleNode("DWELLING_ENDORSEMENT_ID");
						
			if ( element != null )
			{
				objOld.DWELLING_ENDORSEMENT_ID = Convert.ToInt32(ClsCommon.DecodeXMLCharacters(element.InnerXml));
			}
			
			element = node.SelectSingleNode("ENDORSEMENT");
						
			if ( element != null )
			{
				objOld.ENDORSEMENT = ClsCommon.DecodeXMLCharacters(element.InnerXml);
			}

			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

			string strTranXML = objBuilder.GetTransactionLogXML(objOld,objNew);

			return strTranXML;
		}
	}
}
