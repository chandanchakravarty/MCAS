/* ***************************************************************************************
 * Author	: Nidhi
   Creation Date : 30/05/2005
   Last Updated  : 01/06/2005
   Reviewed By	 : 
   Purpose	: This is used to show the proposal form to the users. 
   Comments	: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/
using System;
using System.Data;
 
 using Cms.DataLayer;
using System.Text;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsUserQuestion.
	/// </summary>
	public class ClsUserQuestion: ClsCommon
	{
		public DataWrapper objDataWrapper;
 		public ClsUserQuestion()
		{
		
		}

		
		public DataSet GetUserDefineList(string lstrListID, string lstrListType, string lstrListTableName)
		{	
 			StringBuilder SqlString = new StringBuilder(1000);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				if(lstrListType=="S")
				{
 					SqlString.Append (lstrListTableName);
				}
				else
				{
 					SqlString.Append (" Select LISTID,OPTIONID,OPTIONNAME,(CONVERT(varchar,LISTID) + '#' + CONVERT(varchar,OPTIONID)) COMBINEID ");
					SqlString.Append (" From QUESTIONLISTOPTIONMASTER WHERE LISTID = " + lstrListID + " order by OPTIONID");
				}
				DataSet lObjDS=new DataSet();	
				objDataWrapper.CommandType=CommandType.Text;
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

		

	
		public DataSet GetReferalTabQuestion(string lstrTabID, string lstrScreenID, string lstrCarrierID, string lstrClientID, string lstrPolicyID, string lstrPolicyVersion, string lstrProposalVersion, string lstrBrokerID)
		{	
			StringBuilder SqlString= new StringBuilder(1000);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append ("  SELECT QID, QDESC, QUESTIONTYPEID, QDESC,");
				SqlString.Append (" PREFIX, SUFFIX, userquestions.TABID, ISREQ, userquestions.GROUPID, QNOTES, QUESTIONLISTID,");
				SqlString.Append (" FLGQUESDESC, DESCTEXT, FLGCOMPVALUE, QG.GroupType, QLM.TYPE, QLM.TABLENAME,userquestions.REQTOTAL");
				SqlString.Append (" FROM userquestions");
				SqlString.Append (" Inner Join QUESTIONGROUPMASTER QG ON QG.GROUPID = userquestions.GROUPID AND QG.TABID = USERQUESTIONS.TABID");
				SqlString.Append (" Inner Join QUESTIONTABMASTER TB ON TB.TABID = userquestions.TABID AND TB.CARRIERID ="+lstrCarrierID);
				SqlString.Append (" Left Outer Join QUESTIONLISTMASTER QLM ON QLM.LISTID = userquestions.QUESTIONLISTID ");
				SqlString.Append (" WHERE userquestions.TABID= 0 AND TB.SCREENID = 0 AND userquestions.CARRIERID ="+lstrCarrierID + " AND NVL(userquestions.ISACTIVE,'N')='Y'");
				SqlString.Append (" AND (QG.SUBCLASSID IS NULL OR QG.SUBCLASSID IN(");
				SqlString.Append (" SELECT classid FROM POLICYCLASS PC WHERE PC.CLIENTID = " + lstrClientID +" AND PC.POLICYID =" + lstrPolicyID +" AND PC.POLICYVERSION = " + lstrPolicyVersion);
				SqlString.Append (" AND PC.PROPOSALVERSION = " + lstrProposalVersion + " AND PC.BROKERID = " + lstrBrokerID + " AND PC.CARRIERID =" + lstrCarrierID +")) ");
				SqlString.Append (" ORDER BY QG.SEQNO,userquestions.SEQNO");

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
		public DataSet GetTabGroup(string lstrGroupID, string lstrTabID, string lstrCarrierID)
		{	
//			string SqlString;
//			try
//			{
//				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
//				SqlString = "SELECT GROUPID, GROUPNAME from QUESTIONGROUPMASTER where GROUPID="+lstrGroupID + " AND TABID = " + lstrTabID + " AND CARRIERID = " +lstrCarrierID;
//				DataSet lObjDS=new DataSet();				
//				lObjDS = objDataWrapper.ExecuteDataSet(SqlString);				
//				return lObjDS;
//			}
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				//objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				 		
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabGroup");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if (objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	 
			 
		}
		
		
		
		public int InsertData(string lstrQID,string lstrHQgridID, string lstrVQgridID, string lstrAnswer, string lstrOptionValue,string lstrClientID, string lstrListID, string lstrOptionID,string lstrOthAnswerDescription,string lstrTabID, string lstrPolicyID, string lstrPolicyVersion,  string lstrCarrierID,string lstrScreenID)
		{
			int lintreturn;
			//string sqlString;						
			try
			{
			 
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@QID",lstrQID,SqlDbType.Int);
				objDataWrapper.AddParameter("@HQGRIDID",lstrHQgridID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VQGRIDID",lstrVQgridID,SqlDbType.Int);
				objDataWrapper.AddParameter("@ANSWER",lstrAnswer,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@OPTIONVALUE",lstrOptionValue,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@CLIENTID",lstrClientID,SqlDbType.Int);
				if (lstrListID == "") lstrListID= "0";
				objDataWrapper.AddParameter("@LISTID",lstrListID,SqlDbType.Int);
				objDataWrapper.AddParameter("@OPTIONID",lstrOptionID,SqlDbType.VarChar);
				
				if (lstrOthAnswerDescription == null) lstrOthAnswerDescription="";
				
				objDataWrapper.AddParameter("@DESCANSWER",lstrOthAnswerDescription,SqlDbType.VarChar);
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPLICATIONID",lstrPolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERSIONID",lstrPolicyVersion,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);

				lintreturn = objDataWrapper.ExecuteNonQuery("Proc_InsertUserDefinedData");			
				return lintreturn;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
			}	   
		}
		
		public int InsertClientData(string lstrQID,string lstrHQgridID, string lstrVQgridID, string lstrAnswer, string lstrOptionValue,string lstrClientID, string lstrListID, string lstrOptionID,string lstrOthAnswerDescription,string lstrTabID,string lstrCarrierID,string lstrBrokerID)
		{
			int lintreturn;
			string sqlString;						
			try
			{
				
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				sqlString = "Insert Into QUESTIONPREPARE(QID,HQUESGRIDOPTID,VQUESGRIDOPTID,ANSWER,OPTIONVALUE,CLIENTID,LISTID,OPTIONID,DESCANSWER,TABID,POLICYID,POLICYVERSION,PROPOSALVERSION,BROKERID,CARRIERID)";// Values("+lstrQID+","+ Utility.intDBValue(lstrHQgridID)+","+Utility.intDBValue(lstrVQgridID)+","+Utility.stringDBValue(lstrAnswer)+",'"+lstrOptionValue+"',"+lstrClientID+","+Utility.intDBValue(lstrListID)+","+Utility.stringDBValue(lstrOptionID)+","+Utility.stringDBValue(lstrOthAnswerDescription)+"," + lstrTabID +",null,null,null,"+Utility.intDBValue(lstrBrokerID)+ ","+Utility.intDBValue(lstrCarrierID)+ ")";								
				lintreturn = objDataWrapper.ExecuteNonQuery(sqlString);			
				return lintreturn;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
			}	   
		}
		public int UpdatePolicyReferal(string lstrClientID, string lstrPolicyID, string lstrPolicyVersion, string lstrProposalVersion, string lstrCarrierID, string lstrBrokerID)
		{
			int lintreturn;
			StringBuilder sqlString = new StringBuilder(500);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				sqlString.Append ("UPDATE POLICYDETAILS SET ISREFERAL = 'Y' WHERE CLIENTID = " + lstrClientID + " AND POLICYID="+lstrPolicyID);
				sqlString.Append (" AND POLICYVERSION = " + lstrPolicyVersion + " AND  PROPOSALVERSION = " + lstrProposalVersion  + " AND CARRIERID = " + lstrCarrierID + " AND BROKERID = " + lstrBrokerID);

				lintreturn = objDataWrapper.ExecuteNonQuery(sqlString.ToString());			
				return lintreturn;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
				sqlString=null;
			}	   
		}

		public DataSet GetUserQuestion(string lstrClientID, string lstrTabID, string lstrScreenID, string lstrAppID, string lstrAppVerId)
		{	
			StringBuilder SqlString = new StringBuilder(1000);
			try
			{
				DataSet lObjDS=new DataSet();	
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@CARRIERID",1,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CLIENTID",lstrClientID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",lstrAppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERID",lstrAppVerId,SqlDbType.Int);
		
				lObjDS = objDataWrapper.ExecuteDataSet("PROC_GETUSERQUESTION");				
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
		public DataSet GetUserQuestionReferal(string lintClientID, string lstrTabID, string lstrScreenID, string lstrPolicyID, string lstrPolicyVersion, string lstrProposalVersion, string lstrCarrierID, string lstrBrokerID)
		{	
			StringBuilder SqlString = new StringBuilder(1000);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append ("Select QP.QID, QP.HQUESGRIDOPTID, QP.VQUESGRIDOPTID, QP.ANSWER,");
				SqlString.Append (" QP.OPTIONVALUE, QU.QUESTIONLISTID , QU.ISREQ, QU.PREFIX,");
				SqlString.Append (" QU.SUFFIX, QU.QNOTES, QU.QUESTIONTYPEID, QU.SEQNO, QU.QDESC, QU.GROUPID, QP.OPTIONID, QU.FLGQUESDESC, QU.DESCTEXT, QU.FLGCOMPVALUE, QP.DESCANSWER, QG.GroupType, QLM.TYPE, QLM.TABLENAME, QU.REQTOTAL ");
				SqlString.Append (" from QUESTIONPREPARE QP ");
				SqlString.Append (" Inner Join UserQuestions QU on QU.QID = QP.QID ");
				SqlString.Append (" Inner Join QUESTIONGROUPMASTER QG on QG.GROUPID = QU.GROUPID AND QG.TABID = QU.TABID ");
				SqlString.Append (" Inner Join QUESTIONTABMASTER TB on ");
				SqlString.Append (" TB.TABID = QU.TABID ");
				SqlString.Append (" Left Outer Join QUESTIONLISTMASTER QLM on QLM.LISTID = QU.QUESTIONLISTID ");
				SqlString.Append (" Where CLIENTID ="+ lintClientID + " AND QU.TABID = 0 AND TB.SCREENID=0 ");
				SqlString.Append (" AND (QG.SUBCLASSID IS NULL OR QG.SUBCLASSID IN(");
				SqlString.Append (" SELECT classid FROM POLICYCLASS PC WHERE PC.CLIENTID = " + lintClientID + " AND PC.POLICYID =" + lstrPolicyID+" AND PC.POLICYVERSION = " + lstrPolicyVersion);
				SqlString.Append (" AND PC.PROPOSALVERSION = " + lstrProposalVersion + " AND PC.BROKERID = " + lstrBrokerID + " AND PC.CARRIERID = " + lstrCarrierID +"))");
				SqlString.Append (" AND QP.POLICYID ="+ lstrPolicyID + " AND QP.POLICYVERSION = "+ lstrPolicyVersion + " AND QP.PROPOSALVERSION="+lstrProposalVersion);
				SqlString.Append (" AND QP.BROKERID ="+ lstrBrokerID + " AND QP.CARRIERID = "+ lstrCarrierID + " AND NVL(QU.ISACTIVE,'N')='Y'");
				SqlString.Append (" Order by QG.SEQNO,QU.SEQNO");

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
		public DataSet GetClientQuestion(string lstrCarrierID,string lStrBrokerID,string lstrClientID,string lstrScreenID,string lstrTabID)
		{	
			StringBuilder SqlString = new StringBuilder(1000);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append ("Select QU.QID, QP.HQUESGRIDOPTID, QP.VQUESGRIDOPTID, QP.ANSWER,");
				SqlString.Append (" QP.OPTIONVALUE, QU.QUESTIONLISTID , QU.ISREQ, QU.PREFIX,");
				SqlString.Append (" QU.SUFFIX, QU.QNOTES, QU.QUESTIONTYPEID, QU.SEQNO, QU.QDESC, QU.GROUPID, QP.OPTIONID, QU.FLGQUESDESC, QU.DESCTEXT, QU.FLGCOMPVALUE, QP.DESCANSWER, QG.GroupType, QLM.TYPE, QLM.TABLENAME, QU.REQTOTAL ");
				SqlString.Append (" from QUESTIONTABMASTER TB inner join QUESTIONGROUPMASTER QG ON ");
				SqlString.Append ("  TB.TabID=QG.tabid AND TB.Carrierid=QG.CarrierID inner join UserQuestions QU ON ");
				SqlString.Append (" QG.Groupid = QU.GroupID AND QG.Carrierid=QU.CarrierID ");
				SqlString.Append (" Left Outer Join QUESTIONLISTMASTER QLM ON QLM.LISTID = QU.QUESTIONLISTID AND QLM.Carrierid = QU.Carrierid ");
				SqlString.Append (" left outer join QUESTIONPREPARE QP ON ");
				SqlString.Append (" QU.QID=QP.QID AND QU.Carrierid=QP.Carrierid AND QP.clientid="+ lstrClientID+" AND QP.BROKERID="+ lStrBrokerID +" ");
				SqlString.Append (" Where QU.TABID = "+lstrTabID+" AND TB.SCREENID="+lstrScreenID+" ");
				SqlString.Append (" AND QG.SUBCLASSID IS NULL ");
				SqlString.Append (" AND TB.CARRIERID = "+ lstrCarrierID + " AND NVL(QU.ISACTIVE,'N')='Y'");
				SqlString.Append (" Order by QG.SEQNO,QU.SEQNO");

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
		public bool GetUserQuestionReferalExists(string lintClientID, string lstrTabID, string lstrScreenID, string lstrPolicyID, string lstrPolicyVersion, string lstrProposalVersion, string lstrCarrierID, string lstrBrokerID)
		{	
			StringBuilder SqlString = new StringBuilder(1000);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			 
				 
    			SqlString.Append ("  SELECT QID, QDESC, QUESTIONTYPEID, QDESC,");
				SqlString.Append (" PREFIX, SUFFIX, userquestions.TABID, ISREQ, userquestions.GROUPID, QNOTES, QUESTIONLISTID,");
				SqlString.Append (" FLGQUESDESC, DESCTEXT, FLGCOMPVALUE, QG.GroupType, QLM.TYPE, QLM.TABLENAME,userquestions.REQTOTAL ");
				SqlString.Append (" FROM userquestions");
				SqlString.Append ( " Inner Join QUESTIONGROUPMASTER QG ON QG.GROUPID = userquestions.GROUPID AND QG.TABID = USERQUESTIONS.TABID ");
				SqlString.Append (" Inner Join QUESTIONTABMASTER TB ON TB.TABID = userquestions.TABID AND TB.CARRIERID ="+lstrCarrierID);
				SqlString.Append (" Left Outer Join QUESTIONLISTMASTER QLM ON QLM.LISTID = userquestions.QUESTIONLISTID");
				SqlString.Append (" WHERE userquestions.TABID= 0 AND TB.SCREENID = 0 AND userquestions.CARRIERID ="+lstrCarrierID + " AND NVL(userquestions.ISACTIVE,'N')='Y'");
				SqlString.Append (" AND (QG.SUBCLASSID IS NULL OR QG.SUBCLASSID IN(");
				SqlString.Append (" SELECT classid FROM POLICYCLASS PC WHERE PC.CLIENTID = " + lintClientID +" AND PC.POLICYID =" + lstrPolicyID +" AND PC.POLICYVERSION = " + lstrPolicyVersion);
				SqlString.Append (" AND PC.PROPOSALVERSION = " + lstrProposalVersion + " AND PC.BROKERID = " + lstrBrokerID + " AND PC.CARRIERID =" + lstrCarrierID +")) ");
				SqlString.Append (" ORDER BY QG.SEQNO,userquestions.SEQNO");
				DataSet lObjDS=new DataSet();				
				lObjDS = objDataWrapper.ExecuteDataSet(SqlString.ToString());
				
				 
				if(lObjDS.Tables[0].Rows.Count >0)
					return true;
				else
					return false;
				
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

		public DataSet GetUserRowsCount(int lstrQID, string lstrClientID, string lstrTabID, string lstrPolicyID, string lstrPolicyVer, string lstrProposalVer, string lstrCarrierID, string lstrBrokerID)
		{	
			StringBuilder SqlString = new StringBuilder(500);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append ("Select Count(*) As Count ");			
				SqlString.Append (" from QUESTIONPREPARE ");			
				SqlString.Append (" Where QID ="+ lstrQID + " AND TABID = " + lstrTabID + " AND CLIENTID = " +lstrClientID + " AND POLICYID = " + lstrPolicyID );
				SqlString.Append (" AND POLICYVERSION ="+ lstrPolicyVer + " AND PROPOSALVERSION = " + lstrProposalVer + " AND BROKERID = " +lstrBrokerID + " AND CARRIERID = " + lstrCarrierID );				
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
				SqlString =null;
			}	   
		}
		public DataSet GetClientUserRowsCount(int lstrQID, string lstrClientID, string lstrTabID, string lstrCarrierID, string lstrBrokerID)
		{	
			
			StringBuilder SqlString = new StringBuilder(500);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			
				SqlString.Append ("Select Count(*) As Count ");			
				SqlString.Append (" from QUESTIONPREPARE ");			
				SqlString.Append (" Where QID ="+ lstrQID + " AND TABID = " + lstrTabID + " AND CLIENTID = " +lstrClientID  );
				SqlString.Append (" AND BROKERID = " +lstrBrokerID + " AND CARRIERID = " + lstrCarrierID );				
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
				SqlString =null;
			}	   
		}
		public DataSet GetQuestionRowsCount(int lstrQID)
		{	
			string SqlString;
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString = "Select Count(*) As Count from QuestionGrid Where QID ="+ lstrQID;			
				DataSet lObjDS=new DataSet();				
				lObjDS = objDataWrapper.ExecuteDataSet(SqlString);				
				return lObjDS;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
			}	   
		}
		public DataSet GetUserHorizontalAnswerForCL(int QuestionID, int lintHGridOptID, int lintVGridOptID, string lstrTabID, string lstrClientID, string lstrCarrierID, string lstrBrokerID)
		{	
			StringBuilder SqlString= new StringBuilder(1000);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append ("Select OPTIONVALUE ");			
				SqlString.Append (" from QuestionPrepare ");			
				SqlString.Append (" Where QID ="+ QuestionID + " AND HQUESGRIDOPTID = " + lintHGridOptID + " AND VQUESGRIDOPTID = " + lintVGridOptID);
				SqlString.Append (" AND CLIENTID ="+ lstrClientID );
				SqlString.Append (" AND BROKERID ="+ lstrBrokerID + " AND CARRIERID = " + lstrCarrierID + " AND TABID = " + lstrTabID);
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
		
		public string GetUserHorizontalAnswerClient(int QuestionID, int lintHGridOptID, int lintVGridOptID, string lstrClientID, string lstrTabID, string lstrAppID, string lstrAppVer, string lstrCarrierID,string lsScreenId)
		{	
			DataSet dsTemp = new DataSet();
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CARRIERID",1,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lsScreenId,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CLIENTID",lstrClientID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",lstrAppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERID",lstrAppVer,SqlDbType.Int);
				objDataWrapper.AddParameter("@QID",QuestionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@HQOPTIONID",lintHGridOptID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VQOPTIONID",lintVGridOptID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("PROC_GETUSERANSWER");	
				if (dsTemp.Tables[0].Rows.Count>0)
				{
					return dsTemp.Tables[0].Rows[0][0].ToString();
				}
				else
				{
					return "";
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
			}	   
		}
		public DataSet GetUserHorizontalAnswerClientForClient(int QuestionID, int lintHGridOptID, int lintVGridOptID, string lintClientID, string lstrTabID,  string lstrCarrierID, string lstrBrokerID)
		{	
			StringBuilder SqlString= new StringBuilder(1000);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append ("Select OPTIONVALUE ");			
				SqlString.Append (" from QuestionPrepare ");			
				SqlString.Append (" Where QID ="+ QuestionID + " AND HQUESGRIDOPTID = " + lintHGridOptID + " AND VQUESGRIDOPTID = " + lintVGridOptID + " AND CLIENTID= " + lintClientID);
				SqlString.Append (" AND TABID ="+ lstrTabID );
				SqlString.Append (" AND BROKERID ="+ lstrBrokerID + " AND CARRIERID = " + lstrCarrierID);
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
		
		public int GetUserHorizontalAnswerSum(int QuestionID, int lintHGridOptID, string lintClientID, string lstrTabID, string lstrPolicyID, string lstrPolicyVer, string lstrProposalVer, string lstrCarrierID, string lstrBrokerID)
		{	
			StringBuilder SqlString= new StringBuilder(1000);
			int RetVal=0;
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append ("Select NVL(SUM(OPTIONVALUE),0) AS OPTIONVALUE ");			
				SqlString.Append (" from QuestionPrepare ");			
				SqlString.Append (" Where QID ="+ QuestionID + " AND HQUESGRIDOPTID = " + lintHGridOptID + " AND CLIENTID= " + lintClientID);
				SqlString.Append (" AND TABID ="+ lstrTabID + " AND POLICYID = " + lstrPolicyID + " AND POLICYVERSION = " + lstrPolicyVer + " AND PROPOSALVERSION = " + lstrProposalVer);
				SqlString.Append (" AND BROKERID ="+ lstrBrokerID + " AND CARRIERID = " + lstrCarrierID);
				DataSet lObjDS=new DataSet();
				RetVal = int.Parse(objDataWrapper.ExecuteNonQuery(SqlString.ToString()).ToString());			
				return RetVal;
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
		public int DeleteData(string lstrClientID,string lstrTabID, string lstrPolicyID,string lstrPolicyVersion,string lstrScreenId)
		{
			int lintreturn;
			//string sqlString;
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMERID",lstrClientID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",lstrPolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERID",lstrPolicyVersion,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenId,SqlDbType.Int);
				
				lintreturn = objDataWrapper.ExecuteNonQuery("Proc_DeleteUserData");			
				return lintreturn;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
			}	   
		}
		public int DeleteClientData(string lstrClientID,string lstrTabID, string lstrCarrierID)
		{
			int lintreturn;
			string sqlString;
			try
			{				
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				sqlString = "delete from QUESTIONPREPARE where CLIENTID = "+lstrClientID + " AND TABID =  " + lstrTabID + "  AND CARRIERID = " + lstrCarrierID;				
				lintreturn = objDataWrapper.ExecuteNonQuery(sqlString);								
				return lintreturn;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
			}	   
		}
		
		

		///Temporary place for getting the policyversion
		public DataSet GetPolicyVersion(string lstrClientID, string lstrPolicyID)
		{	
			StringBuilder SqlString = new StringBuilder(500);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append (" SELECT Max(POLICYVERSION) AS POLICYVERSION ");
				SqlString.Append (" FROM POLICYDETAILS ");
				SqlString.Append (" WHERE CLIENTID  = " + lstrClientID + " AND POLICYID = " + lstrPolicyID);		
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
	

		///Temporary place for gettin the class from the
		public DataSet GetProposalVersion(string lstrClientID, string lstrPolicyID, string lstrPolicyVersion)
		{	
			StringBuilder SqlString = new StringBuilder(500);
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString.Append ("  SELECT PROPOSALVERSION FROM POLICYDETAILS ");
				SqlString.Append (" WHERE CLIENTID  = " + lstrClientID + " AND POLICYID = " + lstrPolicyID + " AND POLICYVERSION = " + lstrPolicyVersion);
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
		public bool GetQuestionStatus(string lstrClientID, string lStrCarrier, string lStrTab)
		{	
			string SqlString;			
			int lIntCnt=0;
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				SqlString = " SELECT nvl(count(*),0) FROM questionprepare WHERE tabid ="+lStrTab+" and carrierid="+lStrCarrier+" and clientid="+lstrClientID ;
							
				lIntCnt = int.Parse(objDataWrapper.ExecuteNonQuery(SqlString).ToString());
				if(lIntCnt > 0)
					return true;
				else
					return false;
				
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




		#region CHANGED BY NIDHI

		public DataSet GetTabGroup(string lstrGroupID, string lstrTabID, string lstrCarrierID,string lstrScreenID)
		{	
			
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				objDataWrapper.AddParameter("@GROUPID",lstrGroupID,SqlDbType.Int);
				 		
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabGroup");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if (objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	 
			 
		}
		public DataSet GetScreenName(string lstrScreenID, string lstrCarrierID)
		{				 
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				 		
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetScreenName");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
			}	   
		}
		public static string GetScreenNameText (string lstrScreenID, string lstrCarrierID)
		{	
			 
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				 		
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetScreenName");			
				if (dsTemp!=null)
				{
					return dsTemp.Tables[0].Rows[0][0].ToString();
				}
				else
				{
					return "";
				}
			}
			catch(Exception ex)
			{
				throw (ex);
			}
			finally
			{	   
			
			}	   
		}
		public DataSet GetTabQuestion(string lstrTabID, string lstrScreenID, string lstrCarrierID)
		{				 
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				 		
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabQuestion");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if (objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	 
			
		}
		public DataSet GetTabQuestion(string lstrTabID, string lstrScreenID, string lstrCarrierID,string lsQID)
		{				 
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@QID",lsQID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				 		
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabQuestion");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if (objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	 
			
		}
		
		public DataSet GetList(string lstrListID)
		{	
 			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@LISTID",lstrListID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetList");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	   

		}

		public DataSet GetGridQuestion(int QuestionID, string lstrCarrierID,string TabId,string ScreenId)
		{	
		   
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@QID",QuestionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",TabId,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",ScreenId,SqlDbType.Int);

              	dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetGridQuestion");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	
		}
		public DataSet GetUserHorizontalAnswer(int QuestionID, int lintHGridOptID, int lintVGridOptID, string lstrTabID, string lstrClientID, string lstrPolicyID, string lstrPolicyVer, string lstrProposalVer, string lstrCarrierID, string lstrBrokerID)
		{	
//				SqlString.Append ("Select OPTIONVALUE ");			
//				SqlString.Append (" from QuestionPrepare ");			
//				SqlString.Append (" Where QID ="+ QuestionID + " AND HQUESGRIDOPTID = " + lintHGridOptID + " AND VQUESGRIDOPTID = " + lintVGridOptID);
//				SqlString.Append (" AND CLIENTID ="+ lstrClientID + " AND POLICYID = " + lstrPolicyID + " AND POLICYVERSION = " + lstrPolicyVer + " AND PROPOSALVERSION = " + lstrProposalVer);
//				SqlString.Append (" AND BROKERID ="+ lstrBrokerID + " AND CARRIERID = " + lstrCarrierID + " AND TABID = " + lstrTabID);
		
 	   
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@QID",QuestionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@HQUESGRIDOPTID",lintHGridOptID,SqlDbType.Int);
				objDataWrapper.AddParameter("@VQUESGRIDOPTID",lintVGridOptID,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUserHorizontalAnswer");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	
		}
		
		public DataSet GetGridVerticalControls(int QuestionID, string lstrCarrierID,int groupID)
		{				  
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@QID",QuestionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
                objDataWrapper.AddParameter("@GROUPID",groupID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetGridVerticalControls");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	   

		}
		
		public DataSet GetGridVerticalQuestion(int QuestionID, string lstrCarrierID,string lsTabId,string lsScreenId)
		{	
 			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@QID",QuestionID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",lsTabId,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lsScreenId,SqlDbType.Int);
                //objDataWrapper.AddParameter("@GROUPID",groupid,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetGridVerticalQuestion");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	   
   
		}
		
		public DataSet GetQuestionDetail(string lstrCarrierID,string lstrScreenID,string lstrTabID,string lstrQID,int groupID)
		{		
			try
			{
				DataSet dsTemp = new DataSet();			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@QID",lstrQID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
                objDataWrapper.AddParameter("@GROUPID",groupID,SqlDbType.Int);

				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetQuestionDetail");			
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				if(objDataWrapper!=null)
					objDataWrapper.Dispose();
			}	   
		}
		
		#endregion


		//modified by manab adding a function to get the group name of question
		public string GetGroupName(string lstrGroupID, string lstrTabID, string lstrScreenId)
		{	
			DataWrapper objDataWrapper = null;
			try
			{
				DataSet dsTemp = new DataSet();			
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@GROUPID",lstrGroupID,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenId,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchGroupData");
			
				string lStrGroupName = "";
				if (dsTemp.Tables[0].Rows.Count > 0 )
				{
					lStrGroupName = dsTemp.Tables[0].Rows[0]["GROUPNAME"].ToString();
				}
				return lStrGroupName;
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	   
				objDataWrapper.Dispose();
			}	   
		}

		public DataSet GetScreensList( string lstrAppId, string lstrAppVerId,string lstrClientId)
		{	
			DataWrapper objDataWrapper = null;
			try
			{
				DataSet dsTemp = new DataSet();			
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);					
				objDataWrapper.AddParameter("@CUSTOMERID",lstrClientId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPID",lstrAppId,SqlDbType.Int);
				objDataWrapper.AddParameter("@APPVERID",lstrAppVerId,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetScreensAssociated");
			
				return dsTemp;
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{	
				if (objDataWrapper != null)
				{
					objDataWrapper.Dispose();
				}
			}	   
		}


		 
	}
}
