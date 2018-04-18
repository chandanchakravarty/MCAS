/******************************************************************************************
<Author				: -	Pradeep
<Start Date			: -	Oct 13, 2005
<End Date			: -	
<Description		: -	Class file for Vehicle Endorsements
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
	/// Summary description for ClsVehicleEndorsements.
	/// </summary>
	public class ClsVehicleEndorsements :  clsapplication 
	{
		int appID;
		int appVersionID;
		int customerID;
		int vehicleID;
		int policyID;
		int policyVersionID;

		public ClsVehicleEndorsements()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/// <summary>
		/// Deletes endorsements from APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <returns></returns>
		public int DeleteUmbrellaVehicleEndorsements(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DELETE_UMBRELLA_VEHICLE_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sVehicleID = (SqlParameter)objWrapper.AddParameter("@VEHICLE_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sID = (SqlParameter)objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sEndID = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).CUSTOMER_ID;
					sVehicleID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).VEHICLE_ID;
					sID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).VEHICLE_ENDORSEMENT_ID;
					sEndID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).ENDORSEMENT_ID;

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


		#region "Watercraft endorsements"
		
		/// <summary>
		/// Deletes endorsements from APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <returns></returns>
		public int DeleteUmbrellaWatercraftEndorsements(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DELETE_UMBRELLA_WATERCRAFT_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sBoatID = (SqlParameter)objWrapper.AddParameter("@BOAT_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sID = (SqlParameter)objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sEndID = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).CUSTOMER_ID;
					sBoatID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).VEHICLE_ID;
					sID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).VEHICLE_ENDORSEMENT_ID;
					sEndID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).ENDORSEMENT_ID;

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

		/// <summary>
		/// Deletes endorsements from APP_WATERCRAFT_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <returns></returns>
		public int DeleteWatercraftEndorsements(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DELETE_WATERCRAFT_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sBoatID = (SqlParameter)objWrapper.AddParameter("@BOAT_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sID = (SqlParameter)objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sEndID = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).CUSTOMER_ID;
					sBoatID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).VEHICLE_ID;
					sID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).VEHICLE_ENDORSEMENT_ID;
					sEndID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).ENDORSEMENT_ID;

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
		
		/// <summary>
		/// Gets Endorsements from APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="appType"></param>
		/// <returns></returns>
		public static DataSet GetUmbrellaWatercraftEndorsements(int customerID, int appID, 
			int appVersionID, int vehicleID, string appType)
		{
			string	strStoredProc =	"Proc_GetAPP_UMBRELLA_WATERCRAFT_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@BOAT_ID",vehicleID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		
		
		
		/// <summary>
		/// Saves endorsements in APP_UMBRELLA_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewEndorsements"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int SaveUmbrellaWatercraftEndorsements(ArrayList alNewEndorsements,string strOldXML)
		{
			string	strStoredProc =	"Proc_SAVE_UMBRELLA_WATERCRAFT_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
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
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objNew = (ClsVehicleEndorsementInfo)alNewEndorsements[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);

					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
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
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Umbrella Watercraft endorsement updated.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
				
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			return 1;
		
		}
		


		public int SaveUmbrellaWatercraftEndorsementsNew(ArrayList alNewEndorsements,string strOldXML,string hiddenCustomInfo)
		{
			string	strStoredProc =	"Proc_SAVE_UMBRELLA_WATERCRAFT_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();
			
			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;
			//string strCustomInfo="Following endorsements have been deleted:",str="";

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

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

					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					//if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					if(objNew.ACTION=="I")
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);
						strStoredProc =	"Proc_SAVE_UMBRELLA_WATERCRAFT_ENDORSEMENTS";

						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);					
						objWrapper.ClearParameteres();

						/*objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;*/

					}
					else if(objNew.ACTION=="U")
					{
						//Update	
						strStoredProc =	"Proc_SAVE_UMBRELLA_WATERCRAFT_ENDORSEMENTS";
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.VEHICLE_ENDORSEMENT_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);					
						objWrapper.ClearParameteres();
					}
					else if(objNew.ACTION=="D")
					{
						strStoredProc =	"Proc_DELETE_UMBRELLA_WATERCRAFT_ENDORSEMENTS";				
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

				
					/*if ( strTranXML.Trim() == "" )
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
						objTransactionInfo.TRANS_DESC		=	"Umbrella Watercraft endorsement updated.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();*/



				}

				sbTranXML.Append("</root>");

				/*if(str!="")
					strCustomInfo+=str;
				else
					strCustomInfo="";*/

				//if(sbTranXML.ToString()!="<root></root>")
				//	strCustomInfo+=";Following endorsements have been added/updated";
				
				objWrapper.ClearParameteres();

				
				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Umbrella Watercraft endorsement updated.";
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
		/// Saves endorsements in APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewEndorsements"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int SaveWatercraftEndorsements(ArrayList alNewEndorsements,string strOldXML)
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
			
			objTransactionInfo.RECORDED_BY = 70;
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;

			try
			{
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objNew = (ClsVehicleEndorsementInfo)alNewEndorsements[i];

					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);

					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
					//Insert Case
					if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						sbTranXML.Append(strTranXML);

					}
					else
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
						sbTranXML.Append(strTranXML);
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ExecuteNonQuery(strStoredProc);					
					objWrapper.ClearParameteres();

				}
				sbTranXML.Append("</root>");

				objTransactionInfo.TRANS_TYPE_ID	=	2;
				objTransactionInfo.APP_ID = appID;
				objTransactionInfo.APP_VERSION_ID = appVersionID;
				objTransactionInfo.CLIENT_ID = customerID;
				
				objTransactionInfo.TRANS_DESC		=	"Watercraft endorsement updated.";
				
				objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();
				
				objWrapper.ClearParameteres();

				objWrapper.ExecuteNonQuery(objTransactionInfo);
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			return 1;
		
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


		public int SaveWatercraftEndorsements(ArrayList alNewEndorsements,ArrayList alDeleteEndorsements,string strOldXML)
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
			
			objTransactionInfo.RECORDED_BY = 70;
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;

			try
			{
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objNew = (ClsVehicleEndorsementInfo)alNewEndorsements[i];

					customerID = objNew.CUSTOMER_ID;
					appID = objNew.APP_ID;
					appVersionID = objNew.APP_VERSION_ID;
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@BOAT_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);

					string strTranXML = "";
					
					//Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
					//Insert Case
					if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.CREATED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						//sbTranXML.Append(strTranXML);

					}
					else
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
						
						
					}
					
					//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ExecuteNonQuery(strStoredProc);					
					objWrapper.ClearParameteres();

				}

				//Insert/Update is complete, start with delete operation

				strStoredProc =	"Proc_DELETE_WATERCRAFT_ENDORSEMENTS";				

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
				}
				objWrapper.ClearParameteres();

				sbTranXML.Append("</root>");
				if(sbTranXML.ToString()!="<root></root>")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Watercraft endorsement updated.";
				
					objTransactionInfo.CHANGE_XML		=	sbTranXML.ToString();			
				

					objWrapper.ExecuteNonQuery(objTransactionInfo);
				}
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			return 1;
		
		}

		
		#endregion

		#region "Vehicle Endorsements"

		/// <summary>
		/// Gets Endorsements from APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="appType"></param>
		/// <returns></returns>
		public  DataSet GetUmbrellaVehicleEndorsements(int customerID, int appID, 
			int appVersionID, int vehicleID, string appType)
		{
			string	strStoredProc =	"Proc_GetAPP_UMBRELLA_VEHICLE_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			this.customerID = customerID;
			this.appID = appID;
			this.appVersionID = appVersionID;
			this.vehicleID = vehicleID;


			//Get Modifications XML
			string xml = this.GetRelevantUmbrellaVehicleEndorsements(ds);

			//Apply modifications to dataset
			this.ModifyVehicleEndorsements(ds,xml);

			return ds;
		
		}

		/// <summary>
		/// Gets Endorsements from APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="appType"></param>
		/// <returns></returns>
		public  DataSet GetVehicleEndorsements(int customerID, int appID, 
			int appVersionID, int vehicleID, string appType)
		{
			this.customerID = customerID;
			this.appID = appID;
			this.appVersionID = appVersionID;
			this.vehicleID = vehicleID;

			string	strStoredProc =	"Proc_GetAPP_VEHICLE_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@APP_ID",appID);
			objWrapper.AddParameter("@APP_VERSION_ID",appVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objWrapper.AddParameter("@APP_TYPE",appType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			//Get Modifications XML
			string xml = this.GetRelevantVehicleEndorsements(ds);

			//Apply modifications to dataset
			this.ModifyVehicleEndorsements(ds,xml);

			return ds;
		
		}
		
		/// <summary>
		/// Based on business rules, this will return an XML string with 
		/// Endorsements to remove, mandatory endorsements etc etc
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetRelevantUmbrellaVehicleEndorsements(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="O"></Endorsement>
			//</Endorsements>
			ClsVehicleInformation objVehBLL = new ClsVehicleInformation();
			DataSet dsVehicle = objVehBLL.GetUmbrellaVehicleInformation(this.customerID,
				this.appID,
				this.appVersionID,
				this.vehicleID);

			DataTable dtVehicle = dsVehicle.Tables[0];
			
			string vehicleUse = "";
			string vehTypePer = "";

			if ( dtVehicle.Rows[0]["VEHICLE_USE"] != System.DBNull.Value )
			{
				vehicleUse = dtVehicle.Rows[0]["VEHICLE_USE"].ToString();
			}
			
			if ( dtVehicle.Rows[0]["VEHICLE_TYPE_PER"] != System.DBNull.Value )
			{
				vehTypePer = dtVehicle.Rows[0]["VEHICLE_TYPE_PER"].ToString();
			}


			DataTable dtEndorsements = ds.Tables[0];
			
			StringBuilder sb = new StringBuilder();
			
			sb.Append("<Endorsements>");

			foreach(DataRow dr in dtEndorsements.Rows)
			{
				int endorsementID = Convert.ToInt32(dr["ENDORSMENT_ID"]);
				
				//Transportation Expense – Amendment (A-90)
				if ( endorsementID == 14 || endorsementID == 34 )
				{
					sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='N' Type='M'>");
					sb.Append("</Endorsement>");
				}
				else
				{
					sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='N' Type='O'>");
					sb.Append("</Endorsement>");
				}
			
				
				//Snow plow
				//if ( vehicleUse != "11166" )
				//{
					if ( endorsementID == 2 || endorsementID == 33 )
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}
				//}

				//Customizing equipment
				if ( vehTypePer != "11335" )
				{
					if ( endorsementID == 92 || endorsementID == 93 )
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}

				//Remove these endorsements from Application section******************
				//1.Stated Amount (A-45) 
				//2.Classic Car (A-46) 
				//3.Antique Car (A-49) 
				//4.Diminishing Deductible (A-25) 
				if (  endorsementID == 18 || endorsementID == 37 ||
					endorsementID == 19 || endorsementID == 38 || endorsementID == 40
					)
				{
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
					sb.Append("</Endorsement>");
				}
				//********************************


			}

			sb.Append("</Endorsements>");
			
			return sb.ToString();
		}

		/// <summary>
		/// Based on business rules, this will return an XML string with 
		/// Endorsements to remove, mandatory endorsements etc etc
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetRelevantPolicyVehicleEndorsements(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="O"></Endorsement>
			//</Endorsements>
			ClsVehicleInformation objVehBLL = new ClsVehicleInformation();

			DataSet dsVehicle = objVehBLL.GetPolicyVehicleInfo(this.customerID,
				this.policyID,
				this.policyVersionID,
				this.vehicleID);

			DataTable dtVehicle = dsVehicle.Tables[0];
		
			string vehTypePer = "";
			string newUsed = "";
			string useVehicle = "";

	
			if ( dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"] != System.DBNull.Value )
			{
				vehTypePer = dtVehicle.Rows[0]["APP_VEHICLE_PERTYPE_ID"].ToString();
			}
			
			if ( dtVehicle.Rows[0]["IS_NEW_USED"] != DBNull.Value )
			{
				newUsed = dtVehicle.Rows[0]["IS_NEW_USED"].ToString();
			}
			
			if ( dtVehicle.Rows[0]["APP_USE_VEHICLE_ID"] != DBNull.Value )
			{
				useVehicle = dtVehicle.Rows[0]["APP_USE_VEHICLE_ID"].ToString();
			}

			DataTable dtEndorsements = ds.Tables[0];
			
			StringBuilder sb = new StringBuilder();
			
			sb.Append("<Endorsements>");

			foreach(DataRow dr in dtEndorsements.Rows)
			{
				int endorsementID = Convert.ToInt32(dr["ENDORSMENT_ID"]);
				
				//Transportation Expense – Amendment (A-90)
				//21	Driver Exclusion (A-96)	14
				//--39	22	2	B	M	Driver Exclusion (A-95)
				//--43	22	2	B	M	PIP Waiver / Waive Work Loss Benefits (A-94)	NULL	N	46	Y	70	2005-10-11 13:11:51.000	NULL	NULL	N	aiimsquarters.txt	PIPWR	NULL
				//15	14	2	N	M	Rejection / Reduction of Uninsured & Underinsured Motorist Coverage (A-9)
				//47	14	3	B	O	Rejection / Reduction of Uninsured & Underinsured Motorist Coverage (A-9)
				//2,33 Snow plow
				if ( endorsementID == 14 ||  endorsementID == 34 || endorsementID == 39 || endorsementID == 43 || endorsementID == 21 || endorsementID == 15 || endorsementID == 47 || endorsementID == 2 || endorsementID == 33)
				{
					sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='N' Type='M'>");
					sb.Append("</Endorsement>");
				}
				else
				{
					sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='N' Type='O'>");
					sb.Append("</Endorsement>");
				}
				
				//If vehicle type is Personal, remove A-80 and A-85 endorsements///////////
				//Employers Non-Ownership Liability (A-80) 8,143
				//Hired Automobiles – Cost of Hire Basis (A-85) 9,142
				if ( useVehicle == "" || useVehicle == "11332" )  // for personal vehicle
				{
					if ( endorsementID == 8 || endorsementID == 9 || endorsementID == 143 || endorsementID == 142 )
					{
						sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}

				if( useVehicle=="11333")
				{
					if ( endorsementID == 32 || endorsementID == 5 || endorsementID == 28 || endorsementID == 6 || endorsementID == 31 )   //endorsementID == 5 || endorsementID == 28  are adde in this condition as per issue no 922 on 8 dec 2006 by Pravesh and for issue no 920 
					{
						sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}

				}
				/////////////

				//If vehicle type is Suspended(comp only), remove all linked Endorsements
				/*
				$200 Sound Reproducing - Tapes  (A-29)	
				Employers Non-Ownership Liability (A-80)
				Employers Non-Ownership Liability (A-80)
				Extended Non-Owned Coverage for Named Individual (A-34)	
				Hired Automobiles – Cost of Hire Basis (A-85)
				Loan / Lease Gap (A-11)
				Miscellaneous Extra Equipment (A-15)
				Sound Receiving & Transmitting Equipment (A-31) 
				Snow Plowing (A-8)
				*/

				if ( vehTypePer == "11618" )
				{
					if ( endorsementID == 5 || endorsementID == 28 || endorsementID == 143 || endorsementID == 8 ||
						endorsementID == 145 || endorsementID == 32 || endorsementID == 142 || endorsementID == 9 ||
						endorsementID == 25 || endorsementID == 1 || endorsementID == 26 ||
						endorsementID == 3 || endorsementID == 6 || endorsementID == 31 || 
						endorsementID == 2 || endorsementID == 33 || endorsementID == 284 || endorsementID == 285
						)
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}

				// If vehicle is not new, remove Loan/Lease gap Coverage
				/*if ( newUsed == "" || newUsed == "1" ) //Commented by Charles on 16-Sep-09 for Patch 12.1 Additional Errors
				{
					if ( endorsementID == 1 || endorsementID == 25 )
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}*/

		
				/*
				//Customizing equipment
				if ( vehTypePer != "11335" )
				{
					if ( endorsementID == 92 || endorsementID == 93 )
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}*/
				
				//Trailer
				//Remove these 
				//1. Sound Reproducing Equipment (Tapes) (A-29) 
				//2. Sound Receiving & Transmitting Equipment (A-31) 
				//3.Loan / Lease Gap (A-11)
				//4. Extended Non-Owned Coverage for Named Individual (A-34)
				//5. Hired Automobiles – Cost of Hire Basis (A-85)
				//6. Employers Non-Ownership Liability (A-80)
				//7. Snow plow 2, 33
				//8. Rental reimbursement 284, 285
				if ( vehTypePer == "11337" )
				{  
					if ( endorsementID == 5 || endorsementID == 28 ||  
						endorsementID == 1 || endorsementID == 25 || endorsementID == 145 || endorsementID == 32 || endorsementID == 9 || endorsementID == 142
						|| endorsementID == 8 || endorsementID == 143 || endorsementID == 2 || endorsementID == 33 ||
						endorsementID == 284 || endorsementID == 285
						)  //endorsementID == 31 || endorsementID == 6 || was remove from this condition as it should not be remove from endorsement as per issue no 923 of covg.
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}
				
				//If vehicle type is Motorhome, remove
				//Rental reimbursement
				if ( vehTypePer == "11336" && vehTypePer != "11337")
				{
					if ( endorsementID == 284 || endorsementID == 285 )
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}
				
				/*If vehicle type is not Motor home or trailers, remove Motor Homes, Campers & Travel Trailers (A-22) 
				 * 16	Motor Homes, Campers & Travel Trailers (A-22)
					35	Motor Homes, Campers & Travel Trailers (A-22)
				*/
				if ( endorsementID == 16 || endorsementID == 35 )
				{
					string mand = "M";
					string remove = "N";

					if ( vehTypePer == "11336" || vehTypePer == "11337")
					{	
						remove = "N";

					}
					else
					{
						remove = "Y";
					}
	
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='" + remove + "' Type='" + mand + "'>");
					sb.Append("</Endorsement>");
				}

			}

			sb.Append("</Endorsements>");
			
			return sb.ToString();
		}
		


		/// <summary>
		/// Based on business rules, this will return an XML string with 
		/// Endorsements to remove, mandatory endorsements etc etc
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public  string GetRelevantVehicleEndorsements(DataSet ds)
		{
			//Format of Endorsement XML
			//<Endorsements>
			//<Endorsement ID="" Remove="N" Type="O"></Endorsement>
			//</Endorsements>
			
			DataTable dtVehicle = ds.Tables[2];
		
			
			string vehicleUse = "";
			string vehTypePer = "";
			string newUsed = "";
			string useVehicle = "";

			if ( dtVehicle.Rows[0]["VEHICLE_USE"] != System.DBNull.Value )
			{
				vehicleUse = dtVehicle.Rows[0]["VEHICLE_USE"].ToString();
			}
			
			if ( dtVehicle.Rows[0]["VEHICLE_TYPE_PER"] != System.DBNull.Value )
			{
				vehTypePer = dtVehicle.Rows[0]["VEHICLE_TYPE_PER"].ToString();
			}
			
			if ( dtVehicle.Rows[0]["IS_NEW_USED"] != DBNull.Value )
			{
				newUsed = dtVehicle.Rows[0]["IS_NEW_USED"].ToString();
			}
			
			if ( dtVehicle.Rows[0]["USE_VEHICLE"] != DBNull.Value )
			{
				useVehicle = dtVehicle.Rows[0]["USE_VEHICLE"].ToString();
			}

			DataTable dtEndorsements = ds.Tables[0];
			
			StringBuilder sb = new StringBuilder();
			
			sb.Append("<Endorsements>");

			foreach(DataRow dr in dtEndorsements.Rows)
			{
				int endorsementID = Convert.ToInt32(dr["ENDORSMENT_ID"]);
				
				//Transportation Expense – Amendment (A-90)
				//21	Driver Exclusion (A-96)	14
				//--39	22	2	B	M	Driver Exclusion (A-95)
				//--43	22	2	B	M	PIP Waiver / Waive Work Loss Benefits (A-94)	NULL	N	46	Y	70	2005-10-11 13:11:51.000	NULL	NULL	N	aiimsquarters.txt	PIPWR	NULL
				//15	14	2	N	M	Rejection / Reduction of Uninsured & Underinsured Motorist Coverage (A-9)
				//47	14	3	B	O	Rejection / Reduction of Uninsured & Underinsured Motorist Coverage (A-9)
				//2,33 Snow plow
				if (endorsementID == 39 || endorsementID == 43 || endorsementID == 21 || endorsementID == 15 || endorsementID == 47 )
				{
					sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='N' Type='M'>");
					sb.Append("</Endorsement>");
				}
				else
				{
					sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='N' Type='O'>");
					sb.Append("</Endorsement>");
				}
				
				//If vehicle type is Personal, remove A-80 and A-85 endorsements///////////
				//Employers Non-Ownership Liability (A-80) 8,143
				//Hired Automobiles – Cost of Hire Basis (A-85) 9,142
				if ( useVehicle == "" || useVehicle == "11332" )  ///for personal vehicle
				{
					if ( endorsementID == 8 || endorsementID == 9 || endorsementID == 143 || endorsementID == 142  || endorsementID==353	|| endorsementID ==358 || endorsementID == 352 )
					{
						sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
					if(  endorsementID == 2 || endorsementID == 33 || endorsementID == 351 || endorsementID == 34)
					{
						sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");
					}
					
				}

				/////////////

				//If vehicle type is Suspended(comp only), remove all linked Endorsements
				/*
				$200 Sound Reproducing - Tapes  (A-29)	
				Employers Non-Ownership Liability (A-80)
				Employers Non-Ownership Liability (A-80)
				Extended Non-Owned Coverage for Named Individual (A-34)	
				Hired Automobiles – Cost of Hire Basis (A-85)
				Loan / Lease Gap (A-11)
				Miscellaneous Extra Equipment (A-15)
				Sound Receiving & Transmitting Equipment (A-31) 
				Snow Plowing (A-8)
				*/

				if ( vehTypePer == "11618" )
				{
					if ( endorsementID == 5 || endorsementID == 28 || endorsementID == 143 || endorsementID == 8 ||
						endorsementID == 145 || endorsementID == 32 || endorsementID == 142 || endorsementID == 9 ||
						endorsementID == 25 || endorsementID == 1 || endorsementID == 26 ||
						endorsementID == 3 || endorsementID == 6 || endorsementID == 31 ||  endorsementID == 2 || endorsementID == 33 ||
						 endorsementID == 284 || endorsementID == 285
						)
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}

				// If vehicle is not new, remove Loan/Lease gap Coverage
				/*if ( newUsed == "" || newUsed == "1" ) //Commented by Charles on 16-Sep-09 for Patch 12.1 Additional Errors
				{
					if ( endorsementID == 1 || endorsementID == 25 )
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}*/

				if( useVehicle=="11333")
				{
					if ( endorsementID == 32 || endorsementID ==1 || endorsementID==25 || endorsementID==50 || endorsementID==252 || endorsementID == 43 || endorsementID == 2 || endorsementID == 33 || endorsementID == 14 ||  endorsementID ==92 || endorsementID==93 || endorsementID == 349 || endorsementID == 354 || endorsementID ==145 || endorsementID == 18 || endorsementID == 37 ||
					endorsementID == 19 || endorsementID == 38 || endorsementID == 5 || endorsementID == 28 || endorsementID == 6 || endorsementID == 31 )   //endorsementID == 5 || endorsementID == 28  are adde in this condition as per issue no 922 on 8 dec 2006 // endorsementID == 347 || endorsementID==34 || endorsementID == 351 || REMOVED ON 26 AUG 08 ITRACK 4579
					{
						sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
					if( endorsementID ==358 || endorsementID == 352 || endorsementID ==350 || endorsementID == 352)
					{
						sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='N' Type='M'>");
						sb.Append("</Endorsement>");

					}
				}

				if(vehTypePer != "11869" && vehTypePer != "11868")
				{
					if ( endorsementID == 19 || endorsementID == 38 || endorsementID == 18 || endorsementID == 37)
					{
						sb.Append("<Endorsement ID='" + dr["ENDORSMENT_ID"].ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}
               
				/*
				//Customizing equipment
				if ( vehTypePer != "11335" )
				{
					if ( endorsementID == 92 || endorsementID == 93 )
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}*/
				
				//Trailer
				//Remove these 
				//1. Sound Reproducing Equipment (Tapes) (A-29) 
				//2. Sound Receiving & Transmitting Equipment (A-31) 
				//3.Loan / Lease Gap (A-11)
				//4. Extended Non-Owned Coverage for Named Individual (A-34)
				//5. Hired Automobiles – Cost of Hire Basis (A-85)
				//6. Employers Non-Ownership Liability (A-80)
				//7. Snow plow 2, 33
				//8. Rental reimbursement 284, 285
				if ( vehTypePer == "11337" )
				{
					if ( endorsementID == 5 || endorsementID == 28 || 
						endorsementID == 1 || endorsementID == 25 || endorsementID == 145 || endorsementID == 32 || endorsementID == 9 || endorsementID == 142
						|| endorsementID == 8 || endorsementID == 143 || 
						endorsementID == 284 || endorsementID == 285 
						)  ///endorsementID == 31 || endorsementID == 6 ||  these are removed from this condition as per issue no 923 of covg on 7 dec 2006
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}
				
				//If vehicle type is Motorhome, remove
				//Rental reimbursement
				if ( vehTypePer == "11336" )
				{
					if ( endorsementID == 284 || endorsementID == 285 )
					{
						sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
						sb.Append("</Endorsement>");
					}
				}
	
				//Remove these endorsements from Application section******************
				//1.Stated Amount (A-45) 
				//2.Classic Car (A-46) 
				//3.Antique Car (A-49) 
				//4.Diminishing Deductible (A-25) 
				//5. 286	14	2	R	O	Commercial Truck (A-2)	Commercial Truck (A-2)
				
				//Ravindra Diminishing Deductible (A-25) is grandfathered & will be available accordingly
				/*if ( endorsementID == 40 || endorsementID == 286)
				{
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
					sb.Append("</Endorsement>");
				}*/
				if ( endorsementID == 286)
				{
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='Y' Type='O'>");
					sb.Append("</Endorsement>");
				}
				//********************************

				/*If vehicle type is not Motor home or trailers, remove Motor Homes, Campers & Travel Trailers (A-22) 
				 * 16	Motor Homes, Campers & Travel Trailers (A-22)
					35	Motor Homes, Campers & Travel Trailers (A-22)
				*/
				if ( endorsementID == 16 || endorsementID == 35 )
				{
					string mand = "M";
					string remove = "N";

					if ( vehTypePer == "11336" || vehTypePer == "11870")
					{	
						remove = "N";

					}
					else
					{
						remove = "Y";
					}
	
					sb.Append("<Endorsement ID='" + endorsementID.ToString() + "' Remove='" + remove + "' Type='" + mand + "'>");
					sb.Append("</Endorsement>");
				}
				
			}

			sb.Append("</Endorsements>");
			
			return sb.ToString();
		}
		

		/// <summary>
		/// Modifies the dataset according to the settings in the XML string
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="xml"></param>
		public  void ModifyVehicleEndorsements(DataSet ds,string xml)
		{
			XmlDocument xmldoc = new XmlDocument();
	
			xmldoc.LoadXml(xml);

			
			//Endorsements to be removed based on conditions
			XmlNodeList removeList = xmldoc.SelectNodes("Endorsements/Endorsement[@Remove='Y']");
			
			if ( removeList != null)
			{
				foreach(XmlNode node in removeList)
				{
					string endorsementID = node.Attributes["ID"].Value;
					DataRow[] dr = ds.Tables[0].Select("Endorsment_ID=" + endorsementID);
					
					if ( dr != null && dr.Length > 0)
					{
						ds.Tables[0].Rows.Remove(dr[0]);
					}

				}
			}

	
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

		#region "Policy Vehicle Endorsements"
		/// <summary>
		/// Gets Endorsements from APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="appID"></param>
		/// <param name="appVersionID"></param>
		/// <param name="vehicleID"></param>
		/// <param name="appType"></param>
		/// <returns></returns>
		public  DataSet GetPolicyVehicleEndorsements(int customerID, int polID, 
			int polVersionID, int vehicleID, string polType)
		{
			this.customerID = customerID;
			this.policyID = polID;
			this.policyVersionID = polVersionID;
			this.vehicleID = vehicleID;

			string	strStoredProc =	"Proc_GetPOL_VEHICLE_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POLICY_ID",polID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@VEHICLE_ID",vehicleID);
			objWrapper.AddParameter("@POLICY_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			//Get Modifications XML
			string xml = this.GetRelevantVehicleEndorsements(ds);

			//Apply modifications to dataset
			this.ModifyVehicleEndorsements(ds,xml);

			return ds;
		
		}
		#endregion


		/// <summary>
		/// Deletes endorsements from APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewCoverages"></param>
		/// <returns></returns>
		public int DeleteVehicleEndorsements(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DELETE_VEHICLE_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@APP_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@APP_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sVehicleID = (SqlParameter)objWrapper.AddParameter("@VEHICLE_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sID = (SqlParameter)objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sEndID = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).APP_ID;
					sAppVersionID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).APP_VERSION_ID;
					sCustomerID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).CUSTOMER_ID;
					sVehicleID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).VEHICLE_ID;
					sID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).VEHICLE_ENDORSEMENT_ID;
					sEndID.Value = ((ClsVehicleEndorsementInfo)alNewCoverages[i]).ENDORSEMENT_ID;

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


		/// <summary>
		/// Saves endorsements in APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewEndorsements"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int SaveVehicleEndorsements(ArrayList alNewEndorsements,string strOldXML)
		{
			string	strStoredProc =	"Proc_SAVE_VEHICLE_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
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
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objNew = (ClsVehicleEndorsementInfo)alNewEndorsements[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);

					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
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
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
				
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}

			return 1;
		
		}


		public int SaveVehicleEndorsementsNew(ArrayList alNewEndorsements,string strOldXML)
		{
			string	strStoredProc =	"Proc_SAVE_VEHICLE_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();

			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");			

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			//string strCustomInfo="Following endorsements have been deleted:",str="";
			
			int customerID = 0;
			int appID = 0;
			int appVersionID = 0;
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

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
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);
					objWrapper.AddParameter("@EDITION_DATE",objNew.EDITION_DATE);

					string strTranXML = "";
					
					

					//if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					if(objNew.ACTION=="I")
					{
						//Insert
						strStoredProc =	"Proc_SAVE_VEHICLE_ENDORSEMENTS";
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						/*objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;*/
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if(objNew.ACTION=="U")
					{
						//Update	
						strStoredProc =	"Proc_SAVE_VEHICLE_ENDORSEMENTS";
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.VEHICLE_ENDORSEMENT_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
					else if(objNew.ACTION=="D")
					{
						strStoredProc =	"Proc_DELETE_VEHICLE_ENDORSEMENTS";				
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
						objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
						objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
						objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
						objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
						//str+=";" + objNew.ENDORSEMENT;
						objWrapper.ExecuteNonQuery(strStoredProc);				
						objWrapper.ClearParameteres();
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);
						sbTranXML.Append(strTranXML);
					}
				
						/*if ( strTranXML.Trim() == "" )
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
						
							//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
							//int retVal = cmdCoverage.ExecuteNonQuery();
							//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
						}*/
					
						//objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
						//objWrapper.ClearParameteres();

					}
				sbTranXML.Append("</root>");
				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement updated.";
					//objTransactionInfo.CUSTOM_INFO = strCustomInfo;
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
		/// Saves endorsements in APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewEndorsements"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int SavePolicyVehicleEndorsements(ArrayList alNewEndorsements,string strOldXML)
		{
			string	strStoredProc =	"Proc_SAVE_POLICY_VEHICLE_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			XmlElement root = null;
			XmlDocument xmlDoc = new XmlDocument();

			StringBuilder sbTranXML = new StringBuilder();
			
			sbTranXML.Append("<root>");			

			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			
			//string strCustomInfo="Following endorsements have been deleted:",str="";
			
			int customerID = 0;
			int policyID = 0;
			int policyVersionID = 0;
			

			if ( strOldXML != "" )
			{
				//strOldXML = ReplaceXMLCharacters(strOldXML);
				xmlDoc.LoadXml(strOldXML);
				root = xmlDoc.DocumentElement; //holds the root of the transaction XML
			}

			try
			{
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Policy.ClsPolicyVehicleEndorsementInfo objNew = (Cms.Model.Policy.ClsPolicyVehicleEndorsementInfo)alNewEndorsements[i];
					
					customerID = objNew.CUSTOMER_ID;
					policyID = objNew.POLICY_ID;
					policyVersionID = objNew.POLICY_VERSION_ID;
					
					objTransactionInfo.RECORDED_BY = objNew.CREATED_BY;

					objWrapper.ClearParameteres();	
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POL_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POL_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);
					objWrapper.AddParameter("@EDITION_DATE",objNew.EDITION_DATE);

					string strTranXML = "";
					
				
					//if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					if(objNew.ACTION=="I")
					{
						//Insert
						//strStoredProc =	"Proc_SAVE_VEHICLE_ENDORSEMENTS";
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyEndorsement.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						/*objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;*/
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();

					}
					else if(objNew.ACTION=="U")
					{
						//Update	
						//strStoredProc =	"Proc_SAVE_VEHICLE_ENDORSEMENTS";
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/PolicyEndorsement.aspx.resx");
				
						strTranXML = this.GetPolicyTranXML(objNew,strOldXML,objNew.VEHICLE_ENDORSEMENT_ID,root);
						if(strTranXML!="<LabelFieldMapping></LabelFieldMapping>")
							sbTranXML.Append(strTranXML);
						objWrapper.ExecuteNonQuery(strStoredProc);
						objWrapper.ClearParameteres();
					}
					else if(objNew.ACTION=="D")
					{
						objWrapper.ClearParameteres();

						objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
						objWrapper.AddParameter("@POLICY_ID",objNew.POLICY_ID);
						objWrapper.AddParameter("@POLICY_VERSION_ID",objNew.POLICY_VERSION_ID);
						objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
						objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
						objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
						//str+=";" + objNew.ENDORSEMENT;
						
						objWrapper.ExecuteNonQuery("Proc_DELETE_POLICY_VEHICLE_ENDORSEMENTS");	
			
						objWrapper.ClearParameteres();
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetDeleteTransactionLogXML(objNew);
						sbTranXML.Append(strTranXML);
					}
				
				

				}
				sbTranXML.Append("</root>");
				if(sbTranXML.ToString()!="<root></root>")// || strCustomInfo!="")
				{
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.APP_ID = appID;
					objTransactionInfo.APP_VERSION_ID = appVersionID;
					objTransactionInfo.CLIENT_ID = customerID;
				
					objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement updated.";
					//objTransactionInfo.CUSTOM_INFO = strCustomInfo;
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

		
		#endregion



		

		
		

		/// <summary>
		/// Saves endorsements in APP_VEHICLE_ENDORSEMENTS
		/// </summary>
		/// <param name="alNewEndorsements"></param>
		/// <param name="strOldXML"></param>
		/// <returns></returns>
		public int SaveUmbrellaVehicleEndorsements(ArrayList alNewEndorsements,string strOldXML)
		{
			string	strStoredProc =	"Proc_SAVE_UMBRELLA_VEHICLE_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
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
				for(int i = 0; i < alNewEndorsements.Count; i++ )
				{
					Cms.Model.Application.PrivatePassenger.ClsVehicleEndorsementInfo objNew = (ClsVehicleEndorsementInfo)alNewEndorsements[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@APP_ID",objNew.APP_ID);
					objWrapper.AddParameter("@APP_VERSION_ID",objNew.APP_VERSION_ID);
					objWrapper.AddParameter("@VEHICLE_ID",objNew.VEHICLE_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@VEHICLE_ENDORSEMENT_ID",objNew.VEHICLE_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);
					objWrapper.AddParameter("@EDITION_DATE",objNew.EDITION_DATE);
					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.VEHICLE_ENDORSEMENT_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/Endorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.APP_ID = objNew.APP_ID;
						objTransactionInfo.APP_VERSION_ID = objNew.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
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
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Vehicle endorsement updated.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						
						//SqlHelper.ExecuteNonQuery(tran,CommandType.StoredProcedure,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//int retVal = cmdCoverage.ExecuteNonQuery();
						//ClsCommon.AddTransactionLog(objTransactionInfo,tran);
						
						
					}
					
					objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					objWrapper.ClearParameteres();

				}
				
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

	}
}
