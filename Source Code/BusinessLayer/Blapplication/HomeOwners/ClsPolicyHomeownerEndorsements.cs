using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlApplication ;
using Cms.BusinessLayer.BlApplication.HomeOwners ;
using Cms.BusinessLayer.BlCommon ;
using Cms.Model.Policy;
using Cms.Model.Policy.HomeOwners;
using Cms.Model.Policy.Homeowners;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlApplication.HomeOwners
{
	/// <summary>
	/// Summary description for ClsPolicyHomeownerEndorsements.
	/// </summary>
	public class ClsPolicyHomeownerEndorsements : clsapplication 
	{
		public ClsPolicyHomeownerEndorsements()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DataSet GetDwellingEndorsements(int customerID, int polID, 
			int polVersionID, int dwellingId, string polType)
		{
			string	strStoredProc =	"Proc_GetPOL_DWELLING_ENDORSEMENTS";
			
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			
			objWrapper.AddParameter("@CUSTOMER_ID",customerID);
			objWrapper.AddParameter("@POL_ID",polID);
			objWrapper.AddParameter("@POL_VERSION_ID",polVersionID);
			objWrapper.AddParameter("@DWELLING_ID",dwellingId);
			objWrapper.AddParameter("@POL_TYPE",polType);
			
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		
		}

		public int SaveDwellingEndorsements(ArrayList alNewEndorsements,string strOldXML)
		{
			string	strStoredProc =	"Proc_SAVE_POLICYDWELLING_ENDORSEMENTS";

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
					Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo objNew = (ClsPolicyHomeOwnerEndorsementInfo)alNewEndorsements[i];
					
					objWrapper.AddParameter("@CUSTOMER_ID",objNew.CUSTOMER_ID);
					objWrapper.AddParameter("@POL_ID",objNew.POLICY_ID);
					objWrapper.AddParameter("@POL_VERSION_ID",objNew.POLICY_VERSION_ID);
					objWrapper.AddParameter("@DWELLING_ID",objNew.DWELLING_ID);
					objWrapper.AddParameter("@ENDORSEMENT_ID",objNew.ENDORSEMENT_ID);
					objWrapper.AddParameter("@DWELLING_ENDORSEMENT_ID",objNew.DWELLING_ENDORSEMENT_ID);
					objWrapper.AddParameter("@REMARKS",objNew.REMARKS);

					string strTranXML = "";
					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

					if ( objNew.DWELLING_ENDORSEMENT_ID == -1 )
					{
						//Insert
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyHomeOwnerEndorsements.aspx.resx");
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						strTranXML = objBuilder.GetTransactionLogXML(objNew);

						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Home endorsement added.";
						objTransactionInfo.CHANGE_XML		=	strTranXML;

					}
					else
					{
						//Update	
						objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/homeowner/PolicyHomeOwnerEndorsements.aspx.resx");
				
						strTranXML = this.GetTranXML(objNew,strOldXML,objNew.DWELLING_ENDORSEMENT_ID,root);
					}
				
					if ( strTranXML.Trim() == "" )
					{
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
						//SqlHelper.ExecuteNonQuery(tran,"Proc_SAVE_VEHICLE_COVERAGES",param);
				
					}
					else
					{
						objTransactionInfo.TRANS_TYPE_ID	=	2;
						objTransactionInfo.POLICY_ID  = objNew.POLICY_ID ;
						objTransactionInfo.POLICY_VER_TRACKING_ID  = objNew.POLICY_VERSION_ID ;
						objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
						//objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
						objTransactionInfo.TRANS_DESC		=	"Home endorsement updated.";
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

		public int DeleteDwellingEndorsements(ArrayList alNewCoverages)
		{
			
			string	strStoredProc =	"Proc_DELETE_POLICY_HOME_ENDORSEMENTS";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);	
			
			SqlParameter sCustomerID = (SqlParameter)objWrapper.AddParameter("@CUSTOMER_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppID = (SqlParameter)objWrapper.AddParameter("@POL_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sAppVersionID = (SqlParameter)objWrapper.AddParameter("@POL_VERSION_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sDwellingID = (SqlParameter)objWrapper.AddParameter("@DWELLING_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sID = (SqlParameter)objWrapper.AddParameter("@DWELLING_ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);
			SqlParameter sEndID = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_ID",SqlDbType.Int,ParameterDirection.Input);

			try
			{
				for(int i = 0; i < alNewCoverages.Count; i++ )
				{
					sAppID.Value = ((ClsPolicyHomeOwnerEndorsementInfo)alNewCoverages[i]).POLICY_ID; 
					sAppVersionID.Value = ((ClsPolicyHomeOwnerEndorsementInfo)alNewCoverages[i]).POLICY_VERSION_ID ;
					sCustomerID.Value = ((ClsPolicyHomeOwnerEndorsementInfo)alNewCoverages[i]).CUSTOMER_ID;
					sDwellingID.Value = ((ClsPolicyHomeOwnerEndorsementInfo)alNewCoverages[i]).DWELLING_ID;
					sID.Value = ((ClsPolicyHomeOwnerEndorsementInfo)alNewCoverages[i]).DWELLING_ENDORSEMENT_ID;
					sEndID.Value = ((ClsPolicyHomeOwnerEndorsementInfo)alNewCoverages[i]).ENDORSEMENT_ID;

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

		private string GetTranXML(ClsPolicyHomeOwnerEndorsementInfo objNew,string xml,int endorsementID, XmlElement root)
		{
			XmlNode node = root.SelectSingleNode("Table[DWELLING_ENDORSEMENT_ID=" + endorsementID.ToString() + "]");
						
			//Cms.Model.Application.ClsCoveragesInfo  objOld = new ClsCoveragesInfo();
			Cms.Model.Policy.Homeowners.ClsPolicyHomeOwnerEndorsementInfo objOld = new ClsPolicyHomeOwnerEndorsementInfo();
			
			objOld.POLICY_ID = objNew.POLICY_ID ;
			objOld.POLICY_VERSION_ID = objNew.POLICY_VERSION_ID ;
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
