/* ***************************************************************************************
 * Author	: Nidhi
   Creation Date : 30/05/2005
   Last Updated  : 31/05/2005
   Reviewed By	 : 
   Purpose	: To view the tabs for a particular screen. 
   Comments	: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.BusinessLayer;


namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsQuestionTab.
	/// </summary>
	public class ClsQuestionTab: ClsCommon
	{
		 
		private const string QUOTE_PENDING_STATUS = "PQ";
		private const string QUOTE_REFER_STATUS = "RF";
		private const string QUOTE_DECLINE_STATUS = "DQ";
		private const string QUOTE_ACCEPT_STATUS = "QA";
		private const string QUOTE_UNACCEPT_STATUS = "UN";
		private const string QUOTE_REJECT_STATUS = "RJ";
		private const string QUOTE_EXPIRED_STATUS = "EX";
		public ClsQuestionTab()
		{	
			// TODO: Add constructor logic here
			//
		}
		// This function returns the dataset which contains the TabID and TabName based on the screenID passed.
		public DataSet GetTabs(string lstrScreenID)
		{	
		
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
				objDataWrapper.AddParameter("@CARRIERID",1,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				 		
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabs");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				 
			}	
		}
		//getting the screen name of specfied screen id
		//manab
		public string getScreenName(string liScreenId)
		{
			string lsScrenName = "";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				object loScreenValue = objDataWrapper.ExecuteDataSet("select screenname from onlinescreenmaster where screenid= " + liScreenId);
				if (loScreenValue != null && loScreenValue.ToString() != "")
				{
					lsScrenName =loScreenValue.ToString();
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				 
			}
			return lsScrenName;

		}
		public int fnReferalTabExists(int lintClientID, int lintPolicyID, int lintPolicyVersion, int lintProposalVersion, int lintCarrierID, int lintBrokerID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				int lintRetVal=0;
				StringBuilder lStrSqlString= new StringBuilder(500);
				lStrSqlString.Append ("SELECT COUNT(*) FROM QUESTIONGROUPMASTER ");
//				lStrSqlString.Append (" WHERE TABID=0 AND (SUBCLASSID IS NULL OR SUBCLASSID IN(");
//				lStrSqlString.Append (" SELECT CLASSID FROM POLICYCLASS WHERE ");
//				lStrSqlString.Append (" CLIENTID = " + lintClientID +" AND POLICYID = " + lintPolicyID + " AND POLICYVERSION = " + lintPolicyVersion);
//				lStrSqlString.Append (" AND PROPOSALVERSION = " + lintProposalVersion +" AND CARRIERID = " + lintCarrierID +" AND BROKERID=" + lintBrokerID + "))");
				lintRetVal = int.Parse(objDataWrapper.ExecuteNonQuery(lStrSqlString.ToString()).ToString());
				return lintRetVal;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				 
			}
		}
		public string ReferalFlag(int lintClientID, int lintPolicyID, int lintPolicyVersion, int lintProposalVersion, int lintCarrierID, int lintBrokerID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				string lintRetVal="";
				StringBuilder lStrSqlString = new StringBuilder(500);
				//objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				lStrSqlString.Append ("SELECT NVL(ISREFERAL,'N') AS ISREFERAL FROM POLICYDETAILS ");				
				lStrSqlString.Append (" WHERE CLIENTID = " + lintClientID + " AND POLICYID = " + lintPolicyID + " AND POLICYVERSION = " + lintPolicyVersion);
				lStrSqlString.Append (" AND PROPOSALVERSION = " + lintProposalVersion +" AND CARRIERID = " + lintCarrierID +" AND BROKERID=" + lintBrokerID);				
				lintRetVal = objDataWrapper.ExecuteNonQuery(lStrSqlString.ToString()).ToString();
				return lintRetVal;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				 
			}
		}
		public DataSet GetLObs(string lstrClientID, string lstrPolicyID, string lstrPolicyVersion, string lstrProposalVersion, string lstrCarrierID, string lstrBrokerID)
		{	
			StringBuilder SqlString = new StringBuilder(500);
			DataWrapper 				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				SqlString.Append (" Select CM.PARENTCLASSID, QD.CLASSID, CM.CLASSCODE ");
				SqlString.Append (" From QUOTEDETAILS QD ");
				SqlString.Append (" INNER JOIN CLASSMASTER CM  ON ");
				SqlString.Append (" CM.CLASSID = QD.CLASSID AND (CM.CARRIERID  = 0 OR CM.CARRIERID  =  " +  lstrCarrierID+ ") ");
				SqlString.Append (" WHERE QD.CLIENTID = " + lstrClientID + " AND QD.POLICYID = " + lstrPolicyID + " AND QD.POLICYVERSION = "+ lstrPolicyVersion + " AND PROPOSALVERSION = " + lstrProposalVersion );
				SqlString.Append (" AND QD.CARRIERID = " + lstrCarrierID + " AND BROKERID  = " + lstrBrokerID + " AND QD.QUOTESTATUSID = '" + QUOTE_ACCEPT_STATUS + "' ");
				DataSet lObjDS=new DataSet();
				lObjDS = objDataWrapper.ExecuteDataSet(SqlString.ToString());
				return lObjDS;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				objDataWrapper.Dispose();		
				SqlString=null;
			}	   
		}
		public DataSet GetPolicyLObs(string lstrClientID, string lstrPolicyID, string lstrPolicyVersion, string lstrProposalVersion, string lstrCarrierID, string lstrBrokerID)
		{	
			StringBuilder SqlString = new StringBuilder(500);
			DataWrapper 				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				SqlString.Append (" Select CM.PARENTCLASSID, PC.CLASSID, CM.CLASSCODE ");
				SqlString.Append (" From POLICYCLASS PC ");
				SqlString.Append (" INNER JOIN CLASSMASTER CM  ON ");
				SqlString.Append (" CM.CLASSID = PC.CLASSID AND (CM.CARRIERID  = 0 OR CM.CARRIERID  =  " +  lstrCarrierID+ ") ");
				SqlString.Append (" WHERE PC.CLIENTID = " + lstrClientID + " AND PC.POLICYID = " + lstrPolicyID + " AND PC.POLICYVERSION = "+ lstrPolicyVersion + " AND PROPOSALVERSION = " + lstrProposalVersion );
				SqlString.Append (" AND PC.CARRIERID = " + lstrCarrierID + " AND PC.BROKERID  = " + lstrBrokerID);
				DataSet lObjDS=new DataSet();
				lObjDS = objDataWrapper.ExecuteDataSet(SqlString.ToString());
				return lObjDS;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				objDataWrapper.Dispose();	
				SqlString=null;
			}	   
		}
		private int fnGetQTypeID(int lIntQID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				string lStrSqlString;			
				lStrSqlString = "SELECT QUESTIONTYPEID FROM USERQUESTIONS WHERE CARRIERID="; // + EbxSession.Get("CarrierID")+ " AND QID = " + lIntQID;								
				DataSet lObjDS=new DataSet();						
				int RetVal = objDataWrapper.ExecuteNonQuery(lStrSqlString);	
				return RetVal;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				
			}
		}
		public string fnGetQuestionDesc(int QID,int HOptionID,int VOptionID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string SqlString;
			try
			{
				if(VOptionID>0 || HOptionID>0)
				{
					int QtypeID = fnGetQTypeID(QID);
					if(QtypeID==8)
					{
						SqlString="SELECT OPTIONTEXT FROM QUESTIONGRID WHERE QID="+QID + " AND QGRIDOPTIONID = " + VOptionID + " AND GRIDOPTIONLAYOUT='V'";
					}
					else
					{
					
						SqlString="SELECT OPTIONTEXT FROM QUESTIONGRID WHERE QID="+QID + " AND QGRIDOPTIONID = " + HOptionID + " AND GRIDOPTIONLAYOUT='H'";
					}
				}
				else
				{
					SqlString="SELECT QDESC FROM USERQUESTIONS WHERE QID="+QID;
				}
				string QDesc = objDataWrapper.ExecuteNonQuery(SqlString).ToString();
				return QDesc;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				 				
			}	   
		}
		public DataSet GetProfession(string lstrClientID, string lstrPolicyID, string lstrPolicyVersion, string lstrProposalVersion, string lstrCarrierID, string lstrBrokerID, string lstrTableName)
		{	
			StringBuilder SqlString = new StringBuilder(500);
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				SqlString.Append (" SELECT PROFESSIONID FROM " + lstrTableName );
				SqlString.Append (" WHERE  CLIENTID = " + lstrClientID + " AND POLICYID = " + lstrPolicyID + " AND POLICYVERSION = " + lstrPolicyVersion + " AND PROPOSALVERSION = " + lstrProposalVersion);
				SqlString.Append (" AND CARRIERID = " + lstrCarrierID  + " AND BROKERID  = " + lstrBrokerID);
				DataSet lObjDS=new DataSet();
				lObjDS = objDataWrapper.ExecuteDataSet(SqlString.ToString());
				return lObjDS;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{				 	
				
			}	   
		}		
		public DataSet GetScreens(string lstrClassID, string lstrSUBClassID,string lstrProfessionID)
		{	
			StringBuilder SqlString = new StringBuilder(500);
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				SqlString.Append (" SELECT SCREENID, SCREENNAME, DISPLAYNAME ");
				SqlString.Append (" FROM ONLINESCREENMASTER ");
				SqlString.Append (" WHERE CLASSID  = " + lstrClassID + " AND SUBCLASSID = " + lstrSUBClassID + " AND PROFESSIONID = " + lstrProfessionID + " AND ISACTIVE ='Y'" );
				DataSet lObjDS=new DataSet();
				lObjDS = objDataWrapper.ExecuteDataSet(SqlString.ToString());
				return lObjDS;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{			 
				SqlString=null;
			}	   
		}	
		public DataSet GetClientScreens()
		{	
			StringBuilder SqlString = new StringBuilder(500);
			DataWrapper 				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				SqlString.Append (" SELECT SCREENID, SCREENNAME, DISPLAYNAME ");
				SqlString.Append (" FROM ONLINESCREENMASTER ");
				//SqlString.Append (" WHERE CARRIERID="+EbxSession.Get("CarrierID")+" AND SCREENID=-1 AND ISACTIVE ='Y'" );
				DataSet lObjDS=new DataSet();
				lObjDS = objDataWrapper.ExecuteDataSet(SqlString.ToString());
				return lObjDS;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
			 
				SqlString=null;
			}	   
		}
	}
}
