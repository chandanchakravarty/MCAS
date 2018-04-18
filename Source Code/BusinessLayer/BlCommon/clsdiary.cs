using System;
using System.Data;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Diary;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;
using System.Collections;

namespace Cms.BusinessLayer.BlCommon
{
    /// <summary>
    /// Summary description for clsdiary.
    /// </summary>
    public class ClsDiary : ClsCommon, IDisposable
    {
        private bool boolTransactionRequired;
        public const string DIARY_TABLE = "TODOLIST";

        public ClsDiary()
        {
            boolTransactionRequired = TransactionLogRequired;
        }


        #region Add(Insert) functions
        public int AddDiarySetup(TodolistInfo objDefModelInfo)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            //if(objDefModelInfo.LISTTYPEID > 0)
            //	Add(objDefModelInfo,objDataWrapper);
            return AddDiarySetup(objDefModelInfo, objDataWrapper);
        }

        // Added by Mohit Agarwal 6-Jun-2007
        private void GetAppQQ(int cust_id, int polId, int polVersionId, ref int appId, ref int appVersionId, ref int qqID, DataWrapper objDataWrapper)
        {

            string strStoredProc = "Proc_GetAppQQFromPolicy";

            objDataWrapper.ClearParameteres();
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", cust_id);
                objDataWrapper.AddParameter("@POLICY_ID", polId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", polVersionId);

                DataSet dsPolicy = objDataWrapper.ExecuteDataSet(strStoredProc);
                if (dsPolicy.Tables[0].Rows.Count > 0)
                {
                    try { appId = int.Parse(dsPolicy.Tables[0].Rows[0]["APP_ID"].ToString()); }
                    catch  { }
                    try { appVersionId = int.Parse(dsPolicy.Tables[0].Rows[0]["APP_VERSION_ID"].ToString()); }
                    catch  { }
                    try { qqID = int.Parse(dsPolicy.Tables[0].Rows[0]["QQ_ID"].ToString()); }
                    catch  { }
                }
            }
            catch 
            {
            }
        }

        public int Add(TodolistInfo objDefModelInfo, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_InsertDiary";
            DateTime RecordDate = DateTime.Now;
            // fsint			newListId;			// holds the value of listid of the rows which will get inserted

            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            int appId = 0, appVersionId = 0, qqID = 0;
            try
            {
                if (objDefModelInfo.POLICY_ID > 0 && objDefModelInfo.POLICY_VERSION_ID > 0)
                {
                    GetAppQQ(objDefModelInfo.CUSTOMER_ID, objDefModelInfo.POLICY_ID, objDefModelInfo.POLICY_VERSION_ID, ref appId, ref appVersionId, ref qqID, objDataWrapper);
                    if (objDefModelInfo.APP_ID == 0)
                        objDefModelInfo.APP_ID = appId;
                    if (objDefModelInfo.APP_VERSION_ID == 0)
                        objDefModelInfo.APP_VERSION_ID = appVersionId;
                    if (objDefModelInfo.QUOTEID == 0)
                        objDefModelInfo.QUOTEID = qqID;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@RECBYSYSTEM", null);
                //if(objDefModelInfo.RECDATE != DateTime.MinValue)
                objDataWrapper.AddParameter("@RECDATE", objDefModelInfo.RECDATE);
                //else
                //	objDataWrapper.AddParameter("@RECDATE",DateTime.Now);

                //if(objDefModelInfo.FOLLOWUPDATE != DateTime.MinValue)
                objDataWrapper.AddParameter("@FOLLOWUPDATE", objDefModelInfo.FOLLOWUPDATE);
                //else
                //	objDataWrapper.AddParameter("@FOLLOWUPDATE",DateTime.Now);

                objDataWrapper.AddParameter("@LISTTYPEID", objDefModelInfo.LISTTYPEID);

                objDataWrapper.AddParameter("@POLICYBROKERID", null);
                objDataWrapper.AddParameter("@SUBJECTLINE", objDefModelInfo.SUBJECTLINE);
                objDataWrapper.AddParameter("@LISTOPEN", "Y");
                if (objDefModelInfo.SYSTEMFOLLOWUPID != -1)
                    objDataWrapper.AddParameter("@SYSTEMFOLLOWUPID", objDefModelInfo.SYSTEMFOLLOWUPID);
                else
                    objDataWrapper.AddParameter("@SYSTEMFOLLOWUPID", System.DBNull.Value);
                objDataWrapper.AddParameter("@PRIORITY", objDefModelInfo.PRIORITY);

                objDataWrapper.AddParameter("@TOUSERID", objDefModelInfo.TOUSERID);
                objDataWrapper.AddParameter("@FROMUSERID", objDefModelInfo.FROMUSERID);
                if (objDefModelInfo.SYSTEMFOLLOWUPID != -1)
                {
                    if (objDefModelInfo.STARTTIME != DateTime.MinValue)
                        objDataWrapper.AddParameter("@STARTTIME", objDefModelInfo.STARTTIME);
                    else
                        objDataWrapper.AddParameter("@STARTTIME", DateTime.Now);
                    if (objDefModelInfo.ENDTIME != DateTime.MinValue)
                        objDataWrapper.AddParameter("@ENDTIME", objDefModelInfo.ENDTIME);
                    else
                        objDataWrapper.AddParameter("@ENDTIME", DateTime.Now.AddMinutes(1));
                }

                objDataWrapper.AddParameter("@NOTE", objDefModelInfo.NOTE);
                objDataWrapper.AddParameter("@PROPOSALVERSION", null);
                if (objDefModelInfo.QUOTEID != 0)
                    objDataWrapper.AddParameter("@QUOTEID", objDefModelInfo.QUOTEID);
                else
                    objDataWrapper.AddParameter("@QUOTEID", null);
                if (objDefModelInfo.CLAIMID != 0)
                    objDataWrapper.AddParameter("@CLAIMID", objDefModelInfo.CLAIMID);
                else
                    objDataWrapper.AddParameter("@CLAIMID", null);
                objDataWrapper.AddParameter("@CLAIMMOVEMENTID", null);
                objDataWrapper.AddParameter("@TOENTITYID", null);
                objDataWrapper.AddParameter("@FROMENTITYID", null);
                //objDataWrapper.AddParameter("@LISTID",null,SqlDbType.Int,ParameterDirection.Output);
                if (objDefModelInfo.CUSTOMER_ID != 0)
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objDefModelInfo.CUSTOMER_ID);
                else
                    objDataWrapper.AddParameter("@CUSTOMER_ID", null);
                if (objDefModelInfo.APP_ID != 0)
                    objDataWrapper.AddParameter("@APP_ID", objDefModelInfo.APP_ID);
                else
                    objDataWrapper.AddParameter("@APP_ID", null);
                if (objDefModelInfo.APP_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@APP_VERSION_ID", objDefModelInfo.APP_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@APP_VERSION_ID", null);
                if (objDefModelInfo.POLICY_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_ID", objDefModelInfo.POLICY_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_ID", null);
                if (objDefModelInfo.POLICY_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objDefModelInfo.POLICY_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", null);

                //Uncommented By Asfa (22-Jan-2008) - iTrack issue #3438
                if (objDefModelInfo.MODULE_ID != -1)
                    objDataWrapper.AddParameter("@MODULE_ID", objDefModelInfo.MODULE_ID);
                else
                    objDataWrapper.AddParameter("@MODULE_ID", null);

                objDataWrapper.AddParameter("@RULES_VERIFIED", objDefModelInfo.RULES_VERIFIED);


                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@LISTID", objDefModelInfo.LISTID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                int returnRowId = 0;
                //base.TransactionLogRequired=false;
                if (base.TransactionLogRequired)
                {
                    objDefModelInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Diary/AddDiaryEntry.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objDefModelInfo);
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.CLIENT_ID = objDefModelInfo.CUSTOMER_ID;
                    objTransInfo.POLICY_ID = objDefModelInfo.POLICY_ID;
                    objTransInfo.POLICY_VER_TRACKING_ID = objDefModelInfo.POLICY_VERSION_ID;
                    objTransInfo.APP_ID = objDefModelInfo.APP_ID;
                    objTransInfo.APP_VERSION_ID = objDefModelInfo.APP_VERSION_ID;
                    objTransInfo.CHANGE_XML = strTranXML;
                    objTransInfo.TRANS_TYPE_ID = 10;
                    //objTransInfo.RECORDED_BY			=	objDefModelInfo.RECORDED_BY;
                    objTransInfo.RECORDED_BY = int.Parse(objDefModelInfo.FROMUSERID.ToString());//Done by Sibin for Itrack Issue 4846
                    //objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);


                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);


                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                }


                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                returnRowId = int.Parse(objSqlParameter.Value.ToString());
                if (returnResult > 0)
                {
                    return returnRowId;
                }
                else
                {
                    return returnResult;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }


        }



        public int AddDiarySetup(TodolistInfo objDefModelInfo, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_InsertDiarySetup";
            DateTime RecordDate = DateTime.Now;
            // fsint			newListId;			// holds the value of listid of the rows which will get inserted

            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@MDD_FOLLOWUP", objDefModelInfo.FOLLOW_UP);
                objDataWrapper.AddParameter("@MDD_DIARYTYPE_ID", objDefModelInfo.LISTTYPEID);
                objDataWrapper.AddParameter("@MDD_SUBJECTLINE", objDefModelInfo.SUBJECTLINE);
                //objDataWrapper.AddParameter("@MDD_NOTIFICATION_LIST",objDefModelInfo.SYSTEMFOLLOWUPID);
                objDataWrapper.AddParameter("@MDD_PRIORITY", objDefModelInfo.PRIORITY);
                objDataWrapper.AddParameter("@MDD_MODULE_ID", objDefModelInfo.MODULE_ID);
                objDataWrapper.AddParameter("@MDD_LOB_ID", objDefModelInfo.LOB_ID);
                objDataWrapper.AddParameter("@MDD_USERGROUP_ID", objDefModelInfo.USERGROUP_ID);
                objDataWrapper.AddParameter("@MDD_USERLIST_ID", objDefModelInfo.USERLIST_ID);
                objDataWrapper.AddParameter("@MDD_IS_ACTIVE", "Y");
                objDataWrapper.AddParameter("@MDD_CREATED_DATETIME", DateTime.Now);
                objDataWrapper.AddParameter("@MDD_CREATED_BY", objDefModelInfo.CREATED_BY);

                int returnResult = 0;
               // int returnRowId = 0;
                base.TransactionLogRequired = false;
                //				if(base.TransactionLogRequired)
                //				{
                //					objDefModelInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Diary/AddDiaryEntry.aspx.resx");
                //					SqlUpdateBuilder objBuilder         =   new SqlUpdateBuilder();   
                //					string strTranXML                   =   objBuilder.GetTransactionLogXML(objDefModelInfo);
                //					ClsTransactionInfo objTransInfo     =   new ClsTransactionInfo();
                //					objTransInfo.CLIENT_ID = objDefModelInfo.CUSTOMER_ID;
                //					objTransInfo.POLICY_ID = objDefModelInfo.POLICY_ID;
                //					objTransInfo.POLICY_VER_TRACKING_ID = objDefModelInfo.POLICY_VERSION_ID;
                //					objTransInfo.APP_ID = objDefModelInfo.APP_ID;
                //					objTransInfo.APP_VERSION_ID = objDefModelInfo.APP_VERSION_ID;
                //					objTransInfo.CHANGE_XML             =   strTranXML;
                //					objTransInfo.TRANS_TYPE_ID          =   1;
                //					objTransInfo.RECORDED_BY			=	objDefModelInfo.CREATED_BY;
                //					objTransInfo.TRANS_DESC             =   "Diary Entry has been added.";
                //
                //					
                //					returnResult				        =	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransInfo);
                //					
                //					
                //				}
                //				else
                //				{
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                //				}


                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                //					if(returnResult > 0)
                //					{
                //						return returnRowId;
                //					}
                //					else
                //					{
                //						return returnResult;
                //					}
                //				

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }

        public int Add(TodolistInfo objDefModelInfo)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            return Add(objDefModelInfo, objDataWrapper);
        }

        //Policy

        public int AddPolicyEntry(TodolistInfo objDefModelInfo)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.ON);
            return AddPolicyEntry(objDefModelInfo, objDataWrapper);
        }

        public int AddPolicyEntry(TodolistInfo objDefModelInfo, DataWrapper objDataWrapper)
        {
            string strStoredProc = "proc_insertdiary";
            DateTime RecordDate = DateTime.Now;
            // fsint			newListId;			// holds the value of listid of the rows which will get inserted

            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            int appId = 0, appVersionId = 0, qqID = 0;
            try
            {
                if (objDefModelInfo.POLICY_ID > 0 && objDefModelInfo.POLICY_VERSION_ID > 0)
                {
                    GetAppQQ(objDefModelInfo.CUSTOMER_ID, objDefModelInfo.POLICY_ID, objDefModelInfo.POLICY_VERSION_ID, ref appId, ref appVersionId, ref qqID, objDataWrapper);
                    if (objDefModelInfo.APP_ID == 0)
                        objDefModelInfo.APP_ID = appId;
                    if (objDefModelInfo.APP_VERSION_ID == 0)
                        objDefModelInfo.APP_VERSION_ID = appVersionId;
                    if (objDefModelInfo.QUOTEID == 0)
                        objDefModelInfo.QUOTEID = qqID;
                }
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@RECBYSYSTEM", null);
                if (objDefModelInfo.RECDATE == DateTime.MinValue)
                    objDataWrapper.AddParameter("@RECDATE", null);
                else
                    objDataWrapper.AddParameter("@RECDATE", objDefModelInfo.RECDATE);
                if (objDefModelInfo.FOLLOWUPDATE == DateTime.MinValue)
                    objDataWrapper.AddParameter("@FOLLOWUPDATE", null);
                else
                    objDataWrapper.AddParameter("@FOLLOWUPDATE", objDefModelInfo.FOLLOWUPDATE);

                objDataWrapper.AddParameter("@LISTTYPEID", objDefModelInfo.LISTTYPEID);
                objDataWrapper.AddParameter("@POLICYBROKERID", null);
                objDataWrapper.AddParameter("@SUBJECTLINE", objDefModelInfo.SUBJECTLINE);
                objDataWrapper.AddParameter("@LISTOPEN", "Y");
                objDataWrapper.AddParameter("@SYSTEMFOLLOWUPID", objDefModelInfo.SYSTEMFOLLOWUPID);
                objDataWrapper.AddParameter("@PRIORITY", objDefModelInfo.PRIORITY);

                objDataWrapper.AddParameter("@TOUSERID", objDefModelInfo.TOUSERID);
                objDataWrapper.AddParameter("@FROMUSERID", objDefModelInfo.FROMUSERID);
                //				if (objDefModelInfo.STARTTIME == DateTime.MinValue)
                //					objDataWrapper.AddParameter("@STARTTIME",null);
                //				else
                //					objDataWrapper.AddParameter("@STARTTIME",objDefModelInfo.STARTTIME);
                //
                //				if (objDefModelInfo.ENDTIME == DateTime.MinValue)
                //                    objDataWrapper.AddParameter("@ENDTIME",null);
                //				else
                //					objDataWrapper.AddParameter("@ENDTIME",objDefModelInfo.ENDTIME);

                if (objDefModelInfo.STARTTIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@STARTTIME", objDefModelInfo.STARTTIME);
                else
                    objDataWrapper.AddParameter("@STARTTIME", DateTime.Now);
                if (objDefModelInfo.ENDTIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@ENDTIME", objDefModelInfo.ENDTIME);
                else
                    objDataWrapper.AddParameter("@ENDTIME", DateTime.Now.AddMinutes(1));

                objDataWrapper.AddParameter("@NOTE", objDefModelInfo.NOTE);
                objDataWrapper.AddParameter("@PROPOSALVERSION", null);
                if (objDefModelInfo.QUOTEID != 0)
                    objDataWrapper.AddParameter("@QUOTEID", objDefModelInfo.QUOTEID);
                else
                    objDataWrapper.AddParameter("@QUOTEID", null);
                objDataWrapper.AddParameter("@CLAIMID", null);
                objDataWrapper.AddParameter("@CLAIMMOVEMENTID", null);
                objDataWrapper.AddParameter("@TOENTITYID", null);
                objDataWrapper.AddParameter("@FROMENTITYID", null);
                //objDataWrapper.AddParameter("@LISTID",null,SqlDbType.Int,ParameterDirection.Output);
                if (objDefModelInfo.CUSTOMER_ID != 0)
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objDefModelInfo.CUSTOMER_ID);
                else
                    objDataWrapper.AddParameter("@CUSTOMER_ID", null);
                if (objDefModelInfo.APP_ID != 0)
                    objDataWrapper.AddParameter("@APP_ID", objDefModelInfo.APP_ID);
                else
                    objDataWrapper.AddParameter("@APP_ID", null);
                if (objDefModelInfo.APP_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@APP_VERSION_ID", objDefModelInfo.APP_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@APP_VERSION_ID", null);

                if (objDefModelInfo.POLICY_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_ID", objDefModelInfo.POLICY_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_ID", null);

                if (objDefModelInfo.POLICY_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objDefModelInfo.POLICY_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", null);

                if (objDefModelInfo.PROCESS_ROW_ID != 0)
                    objDataWrapper.AddParameter("@PROCESS_ROW_ID", objDefModelInfo.PROCESS_ROW_ID);
                else
                    objDataWrapper.AddParameter("@PROCESS_ROW_ID", null);

                objDataWrapper.AddParameter("@RULES_VERIFIED", objDefModelInfo.RULES_VERIFIED);

                if (objDefModelInfo.MODULE_ID != 0)
                    objDataWrapper.AddParameter("@MODULE_ID", objDefModelInfo.MODULE_ID);
                else
                    objDataWrapper.AddParameter("@MODULE_ID", null);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@LISTID", objDefModelInfo.LISTID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                int returnRowId = 0;
                //base.TransactionLogRequired=false;
                if (base.TransactionLogRequired)
                {
                    objDefModelInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Diary/AddDiaryEntry.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objDefModelInfo);
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.POLICY_ID = objDefModelInfo.POLICY_ID;
                    objTransInfo.POLICY_VER_TRACKING_ID = objDefModelInfo.POLICY_VERSION_ID;
                    objTransInfo.CLIENT_ID = objDefModelInfo.CUSTOMER_ID;
                    objTransInfo.CHANGE_XML = strTranXML;
                    objTransInfo.TRANS_TYPE_ID = 10;
                    if (IsEODProcess)
                        objTransInfo.RECORDED_BY = EODUserID;
                    else
                        objTransInfo.RECORDED_BY = objDefModelInfo.CREATED_BY;
                    //objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);                    
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                }


                objDataWrapper.ClearParameteres();
                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                returnRowId = int.Parse(objSqlParameter.Value.ToString());
                if (returnResult > 0)
                {
                    return returnRowId;
                }
                else
                {
                    return returnResult;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            /*finally
            {
                if(objDataWrapper != null) objDataWrapper.Dispose();
            }*/

        }


        public int Add(TodolistInfo objDefModelInfo, DataWrapper objDataWrapper, string strCustomInfo)
        {
            string strStoredProc = "proc_insertdiary";
            DateTime RecordDate = DateTime.Now;
            // fsint			newListId;			// holds the value of listid of the rows which will get inserted

            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            int appId = 0, appVersionId = 0, qqID = 0;
            try
            {
                if (objDefModelInfo.POLICY_ID > 0 && objDefModelInfo.POLICY_VERSION_ID > 0)
                {
                    GetAppQQ(objDefModelInfo.CUSTOMER_ID, objDefModelInfo.POLICY_ID, objDefModelInfo.POLICY_VERSION_ID, ref appId, ref appVersionId, ref qqID, objDataWrapper);
                    if (objDefModelInfo.APP_ID == 0)
                        objDefModelInfo.APP_ID = appId;
                    if (objDefModelInfo.APP_VERSION_ID == 0)
                        objDefModelInfo.APP_VERSION_ID = appVersionId;
                    if (objDefModelInfo.QUOTEID == 0)
                        objDefModelInfo.QUOTEID = qqID;
                }
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@RECBYSYSTEM", null);
                objDataWrapper.AddParameter("@RECDATE", objDefModelInfo.RECDATE);
                objDataWrapper.AddParameter("@FOLLOWUPDATE", objDefModelInfo.FOLLOWUPDATE);
                objDataWrapper.AddParameter("@LISTTYPEID", objDefModelInfo.LISTTYPEID);
                objDataWrapper.AddParameter("@POLICYBROKERID", null);
                objDataWrapper.AddParameter("@SUBJECTLINE", objDefModelInfo.SUBJECTLINE);
                objDataWrapper.AddParameter("@LISTOPEN", "Y");
                objDataWrapper.AddParameter("@SYSTEMFOLLOWUPID", objDefModelInfo.SYSTEMFOLLOWUPID);
                objDataWrapper.AddParameter("@PRIORITY", objDefModelInfo.PRIORITY);

                objDataWrapper.AddParameter("@TOUSERID", objDefModelInfo.TOUSERID);
                objDataWrapper.AddParameter("@FROMUSERID", objDefModelInfo.FROMUSERID);
                //				objDataWrapper.AddParameter("@STARTTIME",objDefModelInfo.STARTTIME);
                //				objDataWrapper.AddParameter("@ENDTIME",objDefModelInfo.ENDTIME);

                if (objDefModelInfo.STARTTIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@STARTTIME", objDefModelInfo.STARTTIME);
                else
                    objDataWrapper.AddParameter("@STARTTIME", DateTime.Now);
                if (objDefModelInfo.ENDTIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@ENDTIME", objDefModelInfo.ENDTIME);
                else
                    objDataWrapper.AddParameter("@ENDTIME", DateTime.Now.AddMinutes(1));
                objDataWrapper.AddParameter("@NOTE", objDefModelInfo.NOTE);
                objDataWrapper.AddParameter("@PROPOSALVERSION", null);
                if (objDefModelInfo.QUOTEID != 0)
                    objDataWrapper.AddParameter("@QUOTEID", objDefModelInfo.QUOTEID);
                else
                    objDataWrapper.AddParameter("@QUOTEID", null);
                objDataWrapper.AddParameter("@CLAIMMOVEMENTID", null);
                objDataWrapper.AddParameter("@TOENTITYID", null);
                objDataWrapper.AddParameter("@FROMENTITYID", null);
                //objDataWrapper.AddParameter("@LISTID",null,SqlDbType.Int,ParameterDirection.Output);
                if (objDefModelInfo.CUSTOMER_ID != 0)
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objDefModelInfo.CUSTOMER_ID);
                else
                    objDataWrapper.AddParameter("@CUSTOMER_ID", null);
                if (objDefModelInfo.APP_ID != 0)
                    objDataWrapper.AddParameter("@APP_ID", objDefModelInfo.APP_ID);
                else
                    objDataWrapper.AddParameter("@APP_ID", null);
                if (objDefModelInfo.APP_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@APP_VERSION_ID", objDefModelInfo.APP_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@APP_VERSION_ID", null);
                objDataWrapper.AddParameter("@RULES_VERIFIED", objDefModelInfo.RULES_VERIFIED);

                if (objDefModelInfo.CLAIMID != 0)
                    objDataWrapper.AddParameter("@CLAIMID", objDefModelInfo.CLAIMID);
                else
                    objDataWrapper.AddParameter("@CLAIMID", null);

                if (objDefModelInfo.POLICY_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_ID", objDefModelInfo.POLICY_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_ID", null);
                if (objDefModelInfo.POLICY_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objDefModelInfo.POLICY_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", null);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@LISTID", objDefModelInfo.LISTID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                int returnRowId = 0;
                //base.TransactionLogRequired=false;
                if (base.TransactionLogRequired)
                {
                    objDefModelInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Diary/AddDiaryEntry.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objDefModelInfo);
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.CLIENT_ID = objDefModelInfo.CUSTOMER_ID;
                    objTransInfo.APP_ID = objDefModelInfo.APP_ID;
                    objTransInfo.APP_VERSION_ID = objDefModelInfo.APP_VERSION_ID;
                    objTransInfo.POLICY_ID = objDefModelInfo.POLICY_ID;
                    objTransInfo.POLICY_VER_TRACKING_ID = objDefModelInfo.POLICY_VERSION_ID;
                    objTransInfo.CHANGE_XML = strTranXML;
                    objTransInfo.TRANS_TYPE_ID = 10;
                    objTransInfo.RECORDED_BY = objDefModelInfo.CREATED_BY;
                    //objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    objTransInfo.CUSTOM_INFO = strCustomInfo;


                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);


                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                }


                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                returnRowId = int.Parse(objSqlParameter.Value.ToString());
                if (returnResult > 0)
                {
                    return returnRowId;
                }
                else
                {
                    return returnResult;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }

        public int Add(TodolistInfo objDefModelInfo, string strCustomInfo)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            return Add(objDefModelInfo, objDataWrapper, strCustomInfo);
        }


        public static string GetCustomerDetails(string Customer_ID)
        {
            string strCustomerName, strCustomerCode, strCustomInfo = "";
            DataSet dsTransDesc = new DataSet();
            dsTransDesc = DataWrapper.ExecuteDataset(ConnStr, "Proc_GetCustomerInfo", Customer_ID);
            if (dsTransDesc.Tables.Count > 0)
            {
                if (dsTransDesc.Tables[0].Rows.Count > 0)
                {
                    strCustomerName = dsTransDesc.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() + " " + dsTransDesc.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " + dsTransDesc.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString();
                    strCustomerCode = dsTransDesc.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString();
                    strCustomInfo = ";"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1790","")+" = " + strCustomerName + "; "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1792","") + strCustomerCode; //";Customer Name = " + strCustomerName + ";Customer Code = " + strCustomerCode;
                }
            }
            return strCustomInfo;
        }


        public int AddDiaryEntry(Model.Diary.TodolistInfo objDiaryInfo, bool TransactionLogReq, DataWrapper objDataWrapper)
        {
            string strStoredProc = "proc_insertdiary";
            DateTime RecordDate = DateTime.Now;
            //int			newListId;			// holds the value of listid of the rows which will get inserted

            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            int appId = 0, appVersionId = 0, qqID = 0;
            string claimNumber = "";//Added for Itrack Issue 6932(Attachment 5) on 5 July 2010
            try
            {
                //Added for Itrack Issue 6932(Attachment 5) on 5 July 2010
                if (objDiaryInfo.CLAIMID != 0)
                {
                    //Added for Itrack Issue 6932(Attachment 5) on 6 July 2010
                    objDataWrapper.ClearParameteres();
                    DataSet ds = new DataSet();
                    objDataWrapper.AddParameter("@CLAIM_ID", objDiaryInfo.CLAIMID);
                    ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
                    claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
                    objDataWrapper.ClearParameteres();
                }

                if (objDiaryInfo.POLICY_ID > 0 && objDiaryInfo.POLICY_VERSION_ID > 0)
                {
                    GetAppQQ(objDiaryInfo.CUSTOMER_ID, objDiaryInfo.POLICY_ID, objDiaryInfo.POLICY_VERSION_ID, ref appId, ref appVersionId, ref qqID, objDataWrapper);
                    if (objDiaryInfo.APP_ID == 0)
                        objDiaryInfo.APP_ID = appId;
                    if (objDiaryInfo.APP_VERSION_ID == 0)
                        objDiaryInfo.APP_VERSION_ID = appVersionId;
                    if (objDiaryInfo.QUOTEID == 0)
                        objDiaryInfo.QUOTEID = qqID;
                }
                objDataWrapper.ClearParameteres();

                objDataWrapper.AddParameter("@RECBYSYSTEM", null);
                objDataWrapper.AddParameter("@RECDATE", RecordDate);
                if (!DateTime.MinValue.Equals(objDiaryInfo.FOLLOWUPDATE))
                {
                    //If the request is coming from Claims Module, we should put follow-up made on Sat/Sat to Mon
                    //Itrack # 1624
                    if (objDiaryInfo.MODULE_ID == (int)BlCommon.ClsDiary.enumModuleMaster.CLAIM)
                    {
                        if (objDiaryInfo.FOLLOWUPDATE.DayOfWeek.ToString().ToUpper() == "SATURDAY")
                            objDiaryInfo.FOLLOWUPDATE = objDiaryInfo.FOLLOWUPDATE.AddDays(2);
                        else if (objDiaryInfo.FOLLOWUPDATE.DayOfWeek.ToString().ToUpper() == "SUNDAY")
                            objDiaryInfo.FOLLOWUPDATE = objDiaryInfo.FOLLOWUPDATE.AddDays(1);
                    }
                    objDataWrapper.AddParameter("@FOLLOWUPDATE", objDiaryInfo.FOLLOWUPDATE);
                }
                objDataWrapper.AddParameter("@LISTTYPEID", objDiaryInfo.LISTTYPEID);
                objDataWrapper.AddParameter("@POLICYBROKERID", null);
                objDataWrapper.AddParameter("@SUBJECTLINE", objDiaryInfo.SUBJECTLINE);
                if (objDiaryInfo.LISTOPEN != "")
                    objDataWrapper.AddParameter("@LISTOPEN", objDiaryInfo.LISTOPEN);
                else
                    objDataWrapper.AddParameter("@LISTOPEN", null);


                if (objDiaryInfo.SYSTEMFOLLOWUPID != -1)
                    objDataWrapper.AddParameter("@SYSTEMFOLLOWUPID", objDiaryInfo.SYSTEMFOLLOWUPID);
                else
                    objDataWrapper.AddParameter("@SYSTEMFOLLOWUPID", null);

                objDataWrapper.AddParameter("@PRIORITY", objDiaryInfo.PRIORITY);
                objDataWrapper.AddParameter("@TOUSERID", objDiaryInfo.TOUSERID);
                objDataWrapper.AddParameter("@FROMUSERID", objDiaryInfo.FROMUSERID);
                //				if(!DateTime.MinValue.Equals(objDiaryInfo.STARTTIME))  
                //					objDataWrapper.AddParameter("@STARTTIME",objDiaryInfo.STARTTIME);
                //				if(!DateTime.MinValue.Equals(objDiaryInfo.ENDTIME))  
                //					objDataWrapper.AddParameter("@ENDTIME",objDiaryInfo.ENDTIME);

                if (objDiaryInfo.STARTTIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@STARTTIME", objDiaryInfo.STARTTIME);
                else
                    objDataWrapper.AddParameter("@STARTTIME", DateTime.Now);
                if (objDiaryInfo.ENDTIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@ENDTIME", objDiaryInfo.ENDTIME);
                else
                    objDataWrapper.AddParameter("@ENDTIME", DateTime.Now.AddMinutes(1));

                objDataWrapper.AddParameter("@NOTE", objDiaryInfo.NOTE);
                objDataWrapper.AddParameter("@PROPOSALVERSION", null);
                if (objDiaryInfo.QUOTEID != 0)
                    objDataWrapper.AddParameter("@QUOTEID", objDiaryInfo.QUOTEID);
                else
                    objDataWrapper.AddParameter("@QUOTEID", null);
                if (objDiaryInfo.CLAIMID != 0)
                    objDataWrapper.AddParameter("@CLAIMID", objDiaryInfo.CLAIMID);
                else
                    objDataWrapper.AddParameter("@CLAIMID", null);
                objDataWrapper.AddParameter("@CLAIMMOVEMENTID", null);
                objDataWrapper.AddParameter("@TOENTITYID", null);
                objDataWrapper.AddParameter("@FROMENTITYID", null);
                //objDataWrapper.AddParameter("@LISTID",null,SqlDbType.Int,ParameterDirection.Output);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@LISTID", null, SqlDbType.Int, ParameterDirection.Output);


                if (objDiaryInfo.CUSTOMER_ID != 0)
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objDiaryInfo.CUSTOMER_ID);
                else
                    objDataWrapper.AddParameter("@CUSTOMER_ID", null);
                if (objDiaryInfo.APP_ID != 0)
                    objDataWrapper.AddParameter("@APP_ID", objDiaryInfo.APP_ID);
                else
                    objDataWrapper.AddParameter("@APP_ID", null);
                if (objDiaryInfo.APP_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@APP_VERSION_ID", objDiaryInfo.APP_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@APP_VERSION_ID", null);
                if (objDiaryInfo.POLICY_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_ID", objDiaryInfo.POLICY_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_ID", null);
                if (objDiaryInfo.POLICY_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objDiaryInfo.POLICY_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", null);

                if (objDiaryInfo.MODULE_ID != 0)
                    objDataWrapper.AddParameter("@MODULE_ID", objDiaryInfo.MODULE_ID);
                else
                    objDataWrapper.AddParameter("@MODULE_ID", null);



                int transactEntry = 0, returnResult = 0;
                if (base.TransactionLogRequired)
                {
                    objDiaryInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Diary/AddDiaryEntry.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objDiaryInfo);
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.CLIENT_ID = objDiaryInfo.CUSTOMER_ID;
                    objTransInfo.POLICY_ID = objDiaryInfo.POLICY_ID;
                    objTransInfo.POLICY_VER_TRACKING_ID = objDiaryInfo.POLICY_VERSION_ID;
                    objTransInfo.APP_ID = objDiaryInfo.APP_ID;
                    objTransInfo.APP_VERSION_ID = objDiaryInfo.APP_VERSION_ID;
                    objTransInfo.CHANGE_XML = strTranXML;
                    objTransInfo.TRANS_TYPE_ID = 10;
                    objTransInfo.RECORDED_BY = objDiaryInfo.CREATED_BY;
                    //objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    //Added for Itrack Issue 6932(Attachment 5) on 5 July 2010
                    if (objDiaryInfo.CLAIMID != 0)
                        objTransInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","") + claimNumber;//"Claim Number : " + claimNumber;

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);


                }
                else
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                int returnRowId = 0;
                if (objSqlParameter != null)
                    if (objSqlParameter.Value.ToString() != "")
                        returnRowId = Convert.ToInt32(objSqlParameter.Value.ToString());

                objDataWrapper.ClearParameteres();

                if (TransactionLogReq)
                {
                    transactEntry = 1;
                }

                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (returnResult > 0)
                {
                    return returnRowId;
                }
                else
                {
                    return returnResult;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        /// <summary>
        ///  Function is called from Diary Page to add entry in TODOLIST
        /// </summary>
        /// <param name="objDiaryInfo"></param>
        /// <returns>No. of rows inserted</returns>
        public int AddDiaryEntry(Model.Diary.TodolistInfo objDiaryInfo, bool TransactionLogReq)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            int i = AddDiaryEntry(objDiaryInfo, TransactionLogReq, objDataWrapper);
            objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return i;

        }

        public int MakeDiaryEntry(Model.Diary.TodolistInfo objDiaryInfo)
        {
            return AddDiaryEntry(objDiaryInfo, TransactionLog);
        }





        #endregion

        #region DIARY SETUP

        public ArrayList DiaryEntryfromSetup(Model.Diary.TodolistInfo objDiaryInfo)
        {
            return DiaryEntryfromSetup(objDiaryInfo, null);
        }


        /// <summary>
        /// This function is created for fetching diary details from diary settings 
        /// This function also make calls for insertion of diary entry
        /// </summary>
        /// <param name="objDiaryInfo">This is model object for diary entry in which properties as module id,list type id, lob id are set</param>
        /// <param name="objDataWrapper">wrapper for object to insert diary entries</param>
        /// <returns>returns an array list of list id for which diary entry has been made</returns>
        public ArrayList DiaryEntryfromSetup(Model.Diary.TodolistInfo objDiaryInfo, DataWrapper objDataWrapper)
        {
            int mID = 0, dID = 0, lobID = 0;
            int rowCnt = 0, iarr = 0;
            Hashtable hColName = new Hashtable();

            ArrayList alResult = new ArrayList();

            mID = objDiaryInfo.MODULE_ID;
            dID = int.Parse(objDiaryInfo.LISTTYPEID.ToString());
            lobID = objDiaryInfo.LOB_ID;

            //calling function to read file for fetching column names
            hColName = ReadingXMlForDiary(mID);

            string USERGROUP_ID = "", USERLIST_ID = "", SUBJECTLINE = "", PRIORITY = "", FOLLOWUP = "";
            ArrayList alUgrp = new ArrayList();

            string typeFlag = getDiary_Type(dID);
            DataSet dsDiarySetup = new DataSet();


            #region FETCHING DIARY DETAILS
            if (typeFlag.Equals("D"))
                dsDiarySetup = GetModuleDetails(mID, dID, lobID);
            else
                dsDiarySetup = GetFollowDetails(mID, dID, lobID);


            if (dsDiarySetup != null)
            {
                if (dsDiarySetup.Tables.Count > 0)
                    if (dsDiarySetup.Tables[0].Rows.Count > 0)
                    {
                        for (; rowCnt < dsDiarySetup.Tables[0].Rows.Count; rowCnt++)
                        {
                            USERGROUP_ID = dsDiarySetup.Tables[0].Rows[rowCnt]["MDD_USERGROUP_ID"].ToString();
                            USERLIST_ID = dsDiarySetup.Tables[0].Rows[rowCnt]["MDD_USERLIST_ID"].ToString();
                            SUBJECTLINE = dsDiarySetup.Tables[0].Rows[rowCnt]["MDD_SUBJECTLINE"].ToString();
                            PRIORITY = dsDiarySetup.Tables[0].Rows[rowCnt]["MDD_PRIORITY"].ToString();
                            FOLLOWUP = dsDiarySetup.Tables[0].Rows[rowCnt]["MDD_FOLLOWUP"].ToString();
                        }

                        ArrayList clName = new ArrayList();
                        alUgrp = CreateQueryfordiaryDetails(USERGROUP_ID, hColName, mID, objDiaryInfo, ref clName, objDataWrapper);

                        #region USER LIST FUNCTION
                        if (USERLIST_ID != "")
                        {
                            string[] arrlst = USERLIST_ID.Split(new char[] { ',' });
                            if (arrlst != null)
                            {
                                if (arrlst.Length > 0)
                                {
                                    for (iarr = 0; iarr < arrlst.Length; iarr++)
                                    {
                                        if (arrlst[iarr].ToString() != "")
                                            if (!alUgrp.Contains(arrlst[iarr]))
                                                alUgrp.Add(arrlst[iarr].ToString());
                                    }
                                }
                            }
                        }
                        #endregion

                        #region FOR LOOP FOR DIARY ENTRY
                        iarr = 0;

                        for (; iarr < alUgrp.Count; iarr++)
                        {
                            if (alUgrp[iarr].ToString() != "0" && alUgrp[iarr].ToString() != "")
                            {
                                if (!SUBJECTLINE.Equals(""))
                                    objDiaryInfo.SUBJECTLINE = SUBJECTLINE;

                                objDiaryInfo.FOLLOW_UP = FOLLOWUP == "" ? 0 : int.Parse(FOLLOWUP);

                                if (typeFlag.Equals("D"))
                                {
                                    if (!DateTime.MinValue.Equals(objDiaryInfo.RECDATE))
                                        objDiaryInfo.FOLLOWUPDATE = (objDiaryInfo.RECDATE.AddDays((double)objDiaryInfo.FOLLOW_UP));
                                    else
                                    {
                                        objDiaryInfo.FOLLOWUPDATE = DateTime.Now.AddDays((double)objDiaryInfo.FOLLOW_UP);
                                        objDiaryInfo.RECDATE = System.DateTime.Now;
                                    }
                                }

                                objDiaryInfo.TOUSERID = long.Parse(alUgrp[iarr].ToString());
                                objDiaryInfo.PRIORITY = PRIORITY;
                                //								objDiaryInfo.STARTTIME		= System.DateTime.Now;
                                //								objDiaryInfo.ENDTIME		= System.DateTime.Now;

                                if (!mID.Equals(1))
                                {
                                    if (objDataWrapper == null)
                                        alResult.Add(AddDiaryEntry(objDiaryInfo, false));
                                    else
                                        alResult.Add(AddDiaryEntry(objDiaryInfo, false, objDataWrapper));
                                }
                                else
                                {
                                    if (objDataWrapper == null)
                                        alResult.Add(AddPolicyEntry(objDiaryInfo));
                                    else
                                        alResult.Add(AddPolicyEntry(objDiaryInfo, objDataWrapper));

                                }

                            }

                        }
                        #endregion
                    }
            }
            #endregion

            return alResult;

        }

        /// <summary>
        /// This function is used to retrieve type flag based on diary type id
        /// </summary>
        /// <param name="diaryID">todolist type id</param>
        /// <returns>flag</returns>
        public string getDiary_Type(int diaryID)
        {
            string flag = "";
            SqlParameter[] sparam = new SqlParameter[1];
            try
            {
                sparam[0] = new SqlParameter("@DIARY_ID", SqlDbType.Int);
                sparam[0].Value = diaryID;

                flag = DataWrapper.ExecuteScalar(ConnStr, "PROC_GETDIARY_TYPE", sparam) == null ? "" : DataWrapper.ExecuteScalar(ConnStr, "PROC_GETDIARY_TYPE", sparam).ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

            return flag;
        }


        /// <summary>
        /// this function peforms reading of diary XML and storing column names in hashtable
        /// </summary>
        /// <param name="mID">module id for which node has to be read</param>
        /// <returns>returns hashtable of user type code as key and column name as value</returns>
        public Hashtable ReadingXMlForDiary(int mID)
        {
            Hashtable hColName = new Hashtable();
            XmlDocument xDoc = new XmlDocument();
            //xDoc.Load(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/diary_xml.xml")); 			
            //Added By Ravindra(03-16-2007) to enable this method to be called from EOD process
            //Based on the flaf set at BLCommon lable the file path will be mapped to Web application 
            // or the UNC path of the resources associated 
            if (IsEODProcess)
            {
                string strFilePath = WebAppUNCPath + @"\cmsweb\support\diary_xml.xml";
                xDoc.Load(strFilePath);
            }
            else
            {
                xDoc.Load(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/diary_xml.xml"));
            }
            if (xDoc != null)
            {
                XmlNodeList xColLst = xDoc.SelectNodes("ROOT/MODULE[@ID='" + mID + "']/COLUMN");

                if (xColLst != null)
                {
                    if (xColLst.Count > 0)
                    {
                        foreach (XmlNode xNode in xColLst)
                        {
                            hColName.Add(xNode.Attributes["CODE"].Value, xNode.InnerText);
                        }
                    }
                }

            }
            return hColName;
        }


        public ArrayList CreateQueryfordiaryDetails(string USERGROUP_ID, Hashtable hColName, int mID, Model.Diary.TodolistInfo objDiaryInfo, ref ArrayList colName,
            DataWrapper objWrapper)
        {
            int iarr = 0;
            string sqlColName = "", sqlQuery = "";
            ArrayList alUgrp = new ArrayList();

            if (USERGROUP_ID != "")
            {
               // int ulCnt = 0;
                string[] arrgrp = USERGROUP_ID.Split(new char[] { ',' });
                ArrayList alColName = new ArrayList();
                if (arrgrp != null)
                {
                    if (arrgrp.Length > 0)
                    {
                        for (; iarr < arrgrp.Length; iarr++)
                        {
                            if (arrgrp[iarr].ToString() != "")
                            {
                                DataSet dsUType = ClsUserType.GetUserType(arrgrp[iarr].ToString());
                                if (dsUType != null)
                                {
                                    if (dsUType.Tables.Count > 0)
                                    {
                                        if (dsUType.Tables[0].Rows.Count > 0)
                                        {
                                            if (dsUType.Tables[0].Rows[0]["USER_TYPE_CODE"].ToString().ToUpper() != "")
                                                if (hColName.Contains(dsUType.Tables[0].Rows[0]["USER_TYPE_CODE"].ToString().ToUpper()))
                                                {
                                                    sqlColName += hColName[dsUType.Tables[0].Rows[0]["USER_TYPE_CODE"].ToString().ToUpper()].ToString() + ",";
                                                    alColName.Add(dsUType.Tables[0].Rows[0]["USER_TYPE_DESC"].ToString().ToUpper());
                                                }


                                        }//END OF USER TYPE DATASET ROW CHECK	
                                    }//END OF USER TYPE DATASET TABLE CHECK
                                }//END OF USER TYPE DATASET NULL CHECK
                            }//END OF ARRAY BLANK CHECK			
                        }//END OF ARRAY FOR
                    }//END OF ARRAY LENGTH CHECK
                }//END OF ARRAY NULL CHECK


                iarr = 0;


                //if xml does not have column name for this module ID
                if (hColName.Count > 0 && sqlColName != "")
                {
                    if (sqlColName.IndexOf(",") != -1)
                    {
                        sqlColName = sqlColName.Substring(0, (sqlColName.Length - 1));
                    }

                    colName = alColName;

                    switch (mID)
                    {
                        case (int)enumModuleMaster.APPLICATION:
                            sqlQuery = "SELECT " + sqlColName + " FROM APP_LIST with (nolock) WHERE CUSTOMER_ID=" + objDiaryInfo.CUSTOMER_ID + " AND APP_ID=" + objDiaryInfo.APP_ID + " AND APP_VERSION_ID=" + objDiaryInfo.APP_VERSION_ID;
                            break;
                        case (int)enumModuleMaster.POLICY:
                            sqlQuery = "SELECT " + sqlColName + " FROM POL_CUSTOMER_POLICY_LIST  with (nolock) WHERE CUSTOMER_ID=" + objDiaryInfo.CUSTOMER_ID + " AND POLICY_ID=" + objDiaryInfo.POLICY_ID + " AND POLICY_VERSION_ID=" + objDiaryInfo.POLICY_VERSION_ID;
                            break;
                        case (int)enumModuleMaster.POLICY_PROCESS:
                            sqlQuery = "SELECT " + sqlColName + " FROM POL_CUSTOMER_POLICY_LIST  with (nolock) WHERE CUSTOMER_ID=" + objDiaryInfo.CUSTOMER_ID + " AND POLICY_ID=" + objDiaryInfo.POLICY_ID + " AND POLICY_VERSION_ID=" + objDiaryInfo.POLICY_VERSION_ID;
                            break;
                        case (int)enumModuleMaster.CUSTOMER:
                            sqlQuery = "SELECT " + sqlColName + " FROM CLT_CUSTOMER_LIST  with (nolock) WHERE CUSTOMER_ID=" + objDiaryInfo.CUSTOMER_ID;
                            break;
                        case (int)enumModuleMaster.CLAIM:
                            //sqlQuery = "SELECT " + sqlColName + " FROM CLM_CLAIM_INFO  with (nolock) JOIN CLM_ADJUSTER ON CLM_CLAIM_INFO.ADJUSTER_CODE = CLM_ADJUSTER.ADJUSTER_ID WHERE CLAIM_ID=" + objDiaryInfo.CLAIMID  ;
                            sqlQuery = "SELECT " + sqlColName + " FROM CLM_CLAIM_INFO  with (nolock) JOIN CLM_ADJUSTER ON CLM_CLAIM_INFO.ADJUSTER_ID = CLM_ADJUSTER.ADJUSTER_ID WHERE CLAIM_ID=" + objDiaryInfo.CLAIMID;
                            break;
                    }
                    DataSet dsUser;
                    if (objWrapper != null)
                        dsUser = ClsUser.FetchUserforDiary(sqlQuery, objWrapper);
                    else
                        dsUser = ClsUser.FetchUserforDiary(sqlQuery);
                    if (dsUser != null)
                    {
                        if (dsUser.Tables.Count > 0)
                        {
                            if (dsUser.Tables[0].Rows.Count > 0 && dsUser.Tables[0].Columns.Count > 0)
                            {
                                for (; iarr < dsUser.Tables[0].Columns.Count; iarr++)
                                {
                                    alUgrp.Add(dsUser.Tables[0].Rows[0][dsUser.Tables[0].Columns[iarr].ToString()].ToString());
                                }
                            }
                        }
                    }
                }//END OF HASH TABLE KEY COUNT CHECK AND CHECKING 
            }//END OF USERGROUP_ID STRING BLANK CHECK

            return alUgrp;

        }

        public DataSet getUsertypeDetails(string nameList)
        {
            try
            {
                string sql = "SELECT DISTINCT USER_TYPE_DESC,USER_TYPE_CODE FROM MNT_USER_TYPES WHERE IS_ACTIVE='Y' and user_type_id in (" + nameList + ") ORDER BY USER_TYPE_DESC ";
                DataSet dsuser = new DataSet();

                DataSet dsUser = ClsUser.FetchUserforDiary(sql);


                return dsUser;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }


        public DataSet getUsersName()
        {
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
               // DataSet dsuser;//=new DataSet();
                DataSet dsUser = objDataWrapper.ExecuteDataSet("Proc_getUserforDiary");
                return dsUser;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        public DataSet getUserName()
        {
            try
            {
                //string sql="SELECT DISTINCT USER_ID,isnull(USER_FNAME,'')  + ' ' + isnull(USER_LNAME,'') USER_NAME,USER_TYPE_DESC,MUT.USER_TYPE_CODE FROM MNT_USER_LIST MUL INNER JOIN MNT_USER_TYPES MUT ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID WHERE MUL.IS_ACTIVE='Y' ORDER BY MUT.USER_TYPE_DESC ";
                //string sql="SELECT DISTINCT USER_ID,isnull(USER_FNAME,'')  + ' ' + isnull(USER_LNAME,'') USER_NAME,USER_TYPE_DESC,MUT.USER_TYPE_CODE FROM MNT_USER_LIST MUL INNER JOIN MNT_USER_TYPES MUT ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID WHERE MUL.IS_ACTIVE='Y' AND MUL.USER_SYSTEM_ID='W001' ORDER BY USER_NAME ASC ";

                //Query changed by Charles on 20-May-10 for Itrack 51
                //string sql="SELECT DISTINCT USER_ID,isnull(USER_FNAME,'')  + ' ' + isnull(USER_LNAME,'') USER_NAME,USER_TYPE_DESC,MUT.USER_TYPE_CODE FROM MNT_USER_LIST MUL INNER JOIN MNT_USER_TYPES MUT ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID WHERE MUL.IS_ACTIVE='Y' AND MUL.USER_SYSTEM_ID='W001' ORDER BY USER_TYPE_DESC,ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') ASC ";
                string sql = @"SELECT DISTINCT USER_ID,isnull(USER_FNAME,'')  + ' ' + isnull(USER_LNAME,'') USER_NAME,
                            USER_TYPE_DESC,MUT.USER_TYPE_CODE 
                            FROM MNT_USER_LIST MUL WITH(NOLOCK)
                            INNER JOIN MNT_USER_TYPES MUT WITH(NOLOCK)
                            ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID 
                            WHERE MUL.IS_ACTIVE='Y' AND MUL.USER_SYSTEM_ID=(SELECT REIN_COMAPANY_CODE FROM MNT_REIN_COMAPANY_LIST LIST WITH(NOLOCK)
                            INNER JOIN MNT_SYSTEM_PARAMS PARM WITH(NOLOCK) 
                            ON PARM.SYS_CARRIER_ID = LIST.REIN_COMAPANY_ID ) 
                            ORDER BY USER_TYPE_DESC,
                            ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') ASC";
                DataSet dsuser = new DataSet();

                DataSet dsUser = ClsUser.FetchUserforDiary(sql);

                return dsUser;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }


        #endregion

        #region Update Functions
        public int Update(TodolistInfo objOldModel, TodolistInfo objNewModel)
        {
            int returnResult = -1; //using this variable to store return value of database operation 
            string strStoredProc = "Proc_UpdateDiary";

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            int appId = 0, appVersionId = 0, qqID = 0;
            try
            {
                if (objNewModel.POLICY_ID > 0 && objNewModel.POLICY_VERSION_ID > 0)
                {
                    GetAppQQ(objNewModel.CUSTOMER_ID, objNewModel.POLICY_ID, objNewModel.POLICY_VERSION_ID, ref appId, ref appVersionId, ref qqID, objDataWrapper);
                    if (objNewModel.APP_ID == 0)
                        objNewModel.APP_ID = appId;
                    if (objNewModel.APP_VERSION_ID == 0)
                        objNewModel.APP_VERSION_ID = appVersionId;
                    if (objNewModel.QUOTEID == 0)
                        objNewModel.QUOTEID = qqID;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@RECDATE", objNewModel.RECDATE);
                objDataWrapper.AddParameter("@FOLLOWUPDATE", objNewModel.FOLLOWUPDATE);
                objDataWrapper.AddParameter("@LISTTYPEID", objNewModel.LISTTYPEID);
                objDataWrapper.AddParameter("@SUBJECTLINE", objNewModel.SUBJECTLINE);
                objDataWrapper.AddParameter("@SYSTEMFOLLOWUPID", objNewModel.SYSTEMFOLLOWUPID);
                objDataWrapper.AddParameter("@PRIORITY", objNewModel.PRIORITY);
                objDataWrapper.AddParameter("@TOUSERID", objNewModel.TOUSERID);
                objDataWrapper.AddParameter("@FROMUSERID", objNewModel.FROMUSERID);
                objDataWrapper.AddParameter("@STARTTIME", objNewModel.STARTTIME);
                objDataWrapper.AddParameter("@ENDTIME", objNewModel.ENDTIME);
                objDataWrapper.AddParameter("@NOTE", objNewModel.NOTE);
                objDataWrapper.AddParameter("@LISTID", objNewModel.LISTID);
                if (objNewModel.CUSTOMER_ID != 0)
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objNewModel.CUSTOMER_ID);
                else
                    objDataWrapper.AddParameter("@CUSTOMER_ID", null);
                if (objNewModel.APP_ID > 0)
                    objDataWrapper.AddParameter("@APP_ID", objNewModel.APP_ID);
                else
                    objDataWrapper.AddParameter("@APP_ID", null);
                if (objNewModel.APP_VERSION_ID > 0)
                    objDataWrapper.AddParameter("@APP_VERSION_ID", objNewModel.APP_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@APP_VERSION_ID", null);
                if (objNewModel.POLICY_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_ID", objNewModel.POLICY_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_ID", null);
                if (objNewModel.POLICY_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objNewModel.POLICY_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", null);
                if (objNewModel.QUOTEID > 0)
                    objDataWrapper.AddParameter("@QUOTEID", objNewModel.QUOTEID);
                else
                    objDataWrapper.AddParameter("@QUOTEID", null);
                objDataWrapper.AddParameter("@RULES_VERIFIED", objNewModel.RULES_VERIFIED);

                if (TransactionLogRequired)
                {
                    objNewModel.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Diary/AddDiaryEntry.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objOldModel, objNewModel);
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.CHANGE_XML = strTranXML;
                    objTransInfo.CLIENT_ID = objNewModel.CUSTOMER_ID;
                    objTransInfo.TRANS_TYPE_ID = 392;
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    objTransInfo.RECORDED_BY = objNewModel.MODIFIED_BY;

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                objDataWrapper.ClearParameteres();

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }



        public int Update(TodolistInfo objOldModel, TodolistInfo objNewModel, string strCustomInfo)
        {
            int returnResult = -1; //using this variable to store return value of database operation 
            string strStoredProc = "Proc_UpdateDiary";

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            int appId = 0, appVersionId = 0, qqID = 0;
            try
            {
                if (objNewModel.POLICY_ID > 0 && objNewModel.POLICY_VERSION_ID > 0)
                {
                    GetAppQQ(objNewModel.CUSTOMER_ID, objNewModel.POLICY_ID, objNewModel.POLICY_VERSION_ID, ref appId, ref appVersionId, ref qqID, objDataWrapper);
                    if (objNewModel.APP_ID == 0)
                        objNewModel.APP_ID = appId;
                    if (objNewModel.APP_VERSION_ID == 0)
                        objNewModel.APP_VERSION_ID = appVersionId;
                    if (objNewModel.QUOTEID == 0)
                        objNewModel.QUOTEID = qqID;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@RECDATE", objNewModel.RECDATE);
                objDataWrapper.AddParameter("@FOLLOWUPDATE", objNewModel.FOLLOWUPDATE);
                objDataWrapper.AddParameter("@LISTTYPEID", objNewModel.LISTTYPEID);
                objDataWrapper.AddParameter("@SUBJECTLINE", objNewModel.SUBJECTLINE);
                objDataWrapper.AddParameter("@SYSTEMFOLLOWUPID", objNewModel.SYSTEMFOLLOWUPID);
                objDataWrapper.AddParameter("@PRIORITY", objNewModel.PRIORITY);
                objDataWrapper.AddParameter("@TOUSERID", objNewModel.TOUSERID);
                objDataWrapper.AddParameter("@FROMUSERID", objNewModel.FROMUSERID);
                objDataWrapper.AddParameter("@STARTTIME", objNewModel.STARTTIME);
                objDataWrapper.AddParameter("@ENDTIME", objNewModel.ENDTIME);
                objDataWrapper.AddParameter("@NOTE", objNewModel.NOTE);
                objDataWrapper.AddParameter("@LISTID", objNewModel.LISTID);
                if (objNewModel.CUSTOMER_ID != 0)
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objNewModel.CUSTOMER_ID);
                else
                    objDataWrapper.AddParameter("@CUSTOMER_ID", null);
                if (objNewModel.APP_ID > 0)
                    objDataWrapper.AddParameter("@APP_ID", objNewModel.APP_ID);
                else
                    objDataWrapper.AddParameter("@APP_ID", null);
                if (objNewModel.APP_VERSION_ID > 0)
                    objDataWrapper.AddParameter("@APP_VERSION_ID", objNewModel.APP_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@APP_VERSION_ID", null);
                if (objNewModel.POLICY_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_ID", objNewModel.POLICY_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_ID", null);
                if (objNewModel.POLICY_VERSION_ID != 0)
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objNewModel.POLICY_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", null);
                if (objNewModel.QUOTEID > 0)
                    objDataWrapper.AddParameter("@QUOTEID", objNewModel.QUOTEID);
                else
                    objDataWrapper.AddParameter("@QUOTEID", null);
                objDataWrapper.AddParameter("@RULES_VERIFIED", objNewModel.RULES_VERIFIED);

                if (TransactionLogRequired)
                {
                    objNewModel.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/Diary/AddDiaryEntry.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objOldModel, objNewModel);
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.CHANGE_XML = strTranXML;
                    objTransInfo.CLIENT_ID = objNewModel.CUSTOMER_ID;
                    objTransInfo.APP_ID = objNewModel.APP_ID;
                    objTransInfo.APP_VERSION_ID = objNewModel.APP_VERSION_ID;
                    objTransInfo.POLICY_ID = objNewModel.POLICY_ID;
                    objTransInfo.POLICY_VER_TRACKING_ID = objNewModel.POLICY_VERSION_ID;
                    objTransInfo.TRANS_TYPE_ID = 392;
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    objTransInfo.RECORDED_BY = objNewModel.MODIFIED_BY;
                    objTransInfo.CUSTOM_INFO = strCustomInfo;

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                objDataWrapper.ClearParameteres();

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        #endregion

        #region Delete Functions
        /// <summary>
        /// The function delete entry from diary
        /// </summary>
        /// <returns>No. of rows deleted</returns>
        public int DeleteDiaryEntry1(int PK_Field, bool TransactionLogReq)
        {
            string strProcName = "proc_deletediary";
            //int transactEntry = 0, 
            int returnResult = 0;
            DataWrapper objDatawrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDatawrapper.AddParameter("@LISTID", PK_Field);
                returnResult = objDatawrapper.ExecuteNonQuery(strProcName);

                return returnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDatawrapper != null) objDatawrapper.Dispose();
            }
        }

        /// <summary>
        /// This function is used to delete diary entry from mnt_diary_details 
        /// based on module and lob_id
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="LOB ID"></param>
        /// <returns>number of rows affected</returns>
        public int DeleteFollowSetup(int moduleID, int LOBID)
        {
            string strProcName = "proc_deleteFollowSetup";
            int returnResult = 0;
            DataWrapper objDatawrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDatawrapper.AddParameter("@MDD_MODULE_ID", moduleID);
                objDatawrapper.AddParameter("@MDD_LOB_ID", LOBID);

                returnResult = objDatawrapper.ExecuteNonQuery(strProcName);

                return returnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDatawrapper != null) objDatawrapper.Dispose();
            }
        }

        /// <summary>
        /// This function is used to delete diary entry from mnt_diary_details 
        /// based on module and diary type id
        /// </summary>
        /// <param name="moduleID">module id</param>
        /// <param name="diaryID">diary type id</param>
        /// <returns>number of rows affected</returns>
        public int DeleteDiarySetup(int moduleID, int diaryID)
        {
            string strProcName = "proc_deletediarySetup";
            int returnResult = 0;
            DataWrapper objDatawrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDatawrapper.AddParameter("@MDD_MODULE_ID", moduleID);
                objDatawrapper.AddParameter("@MDD_DIARYTYPE_ID", diaryID);

                returnResult = objDatawrapper.ExecuteNonQuery(strProcName);

                return returnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDatawrapper != null) objDatawrapper.Dispose();
            }
        }


        public int DeleteDiaryEntry2(int PK_Field)
        {
            return DeleteDiaryEntry1(PK_Field, TransactionLog);
        }


        #endregion

        #region set complete task - Update listopen
        public void SetCompleteTask(string CSVListId)
        {
            StringBuilder updateSQL = new StringBuilder("UPDATE " + DIARY_TABLE + " set LISTOPEN = 'N' where LISTID in (" + CSVListId + ")");
            DataWrapper.ExecuteNonQuery(ConnStr, CommandType.Text, updateSQL.ToString());

        }
        #endregion

        #region private utility functions of diary

        private string ReplaceCustomizedMessageTag(string transactionDescription, string fromUserId, string listId)
        {

            transactionDescription = transactionDescription.Replace("[Table_Name]", DIARY_TABLE);
            transactionDescription = transactionDescription.Replace("[From_Id]", fromUserId);
            transactionDescription = transactionDescription.Replace("[listid]", listId);
            return transactionDescription;
        }


        /// <summary>
        /// Validates the diarydata object 
        /// </summary>
        /// <returns>True if Validation check succeed, false otherwise</returns>
        private bool ValidateDiary()
        {
            return true;
        }

        /// <summary>
        /// This function is being called from calendar class to retrieve data from database grouped on ToDoListType
        /// </summary>
        /// <param name="userId">integer value of logged in user's userid</param>
        /// <param name="connString">connection string to be passed to datawrapper class</param>
        /// <returns>DataSet containing count and list type id </returns>
        public DataSet GetCountListType(int userId, string strCustomerId)
        {
            /*DataSet dsCount=null;
            string sqlString="select count(listtypeid) 'Counting',listtypeid from todolist tdl where touserid=" + userId + " group by listtypeid";
            dsCount=DataWrapper.ExecuteDataset(connString,CommandType.Text,sqlString);
            return dsCount;*/

            SqlParameter[] sparam = new SqlParameter[2];
            try
            {
                sparam[0] = new SqlParameter("@USER_ID", SqlDbType.NVarChar, 4);
                sparam[0].Value = userId;
                sparam[1] = new SqlParameter("@CUSTOMER_ID", SqlDbType.NVarChar, 4);
                sparam[1].Value = strCustomerId;

                return DataWrapper.ExecuteDataset(ConnStr, "Proc_GetCountListType", sparam);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }

        /// <summary>
        /// This function is being called for fetching module name and diary types
        /// </summary>
        /// <returns>DataSet containing module name, module id,diary type name and its id </returns>
        public DataSet GetModuleDiaryType()
        {
            try
            {
                return DataWrapper.ExecuteDataset(ConnStr, "Proc_Fetch_Module_diarytype");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }

        public static void GetModule(DropDownList objDropDownList, string type)
        {
            try
            {
                SqlParameter[] sparam = new SqlParameter[2];

                sparam[0] = new SqlParameter("@type", SqlDbType.NChar, 1);
                sparam[0].Value = type;

                //Added By pradeep kushwaha on 22-July-2010 for the multilingual 
                sparam[1] = new SqlParameter("@LANG_ID", SqlDbType.Int);
                sparam[1].Value = BL_LANG_ID;

                DataSet ds = DataWrapper.ExecuteDataset(ConnStr, "Proc_Fetch_Module", sparam);

                objDropDownList.DataSource = ds;
                objDropDownList.DataTextField = "MM_MODULE_NAME";
                objDropDownList.DataValueField = "MM_MODULE_ID";
                objDropDownList.DataBind();
                objDropDownList.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "Selecione a opção" : "Select Option")); //"Select Option");


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }


        public static DataSet GetDiaryByModule(string mID, string flag)
        {
            SqlParameter[] sparam = new SqlParameter[3];
            try
            {
                sparam[0] = new SqlParameter("@moduleID", SqlDbType.NVarChar, 4);
                sparam[0].Value = mID;
                sparam[1] = new SqlParameter("@type_flag", SqlDbType.NChar, 1);
                sparam[1].Value = flag;
                //Added By pradeep kushwaha on 22-July-2010 for the multilingual 
                sparam[2] = new SqlParameter("@LANG_ID", SqlDbType.Int);
                sparam[2].Value = BL_LANG_ID;

                return DataWrapper.ExecuteDataset(ConnStr, "Proc_Fetch_DiaryType_Details", sparam);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }


        public static DataSet GetFollowUpDiaryType(string flag)
        {
            try
            {
                try
                {
                    SqlParameter[] sparam = new SqlParameter[1];

                    sparam[0] = new SqlParameter("@type_flag", SqlDbType.NChar, 1);
                    sparam[0].Value = flag;
                    return DataWrapper.ExecuteDataset(ConnStr, "Proc_Fetch_FollowUp", sparam);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }



        /// <summary>
        /// This function is used to fetch follow up details from mnt_diary_details  
        /// </summary>
        /// <param name="mID">module id</param>
        /// <param name="dID">lob id</param>
        /// <returns>dataset containing follow up details</returns>
        public DataSet GetFollowUpDetails(int mID, int lobID)
        {
            try
            {
                SqlParameter[] sparam = new SqlParameter[2];
                try
                {
                    sparam[0] = new SqlParameter("@moduleID", SqlDbType.Int);
                    sparam[0].Value = mID;
                    sparam[1] = new SqlParameter("@LOB_ID", SqlDbType.Int);
                    sparam[1].Value = lobID;


                    return DataWrapper.ExecuteDataset(ConnStr, "Proc_Fetch_Followup_Details", sparam);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }


        /// <summary>
        /// This function is used to fetch module details from mnt_diary_details  
        /// </summary>
        /// <param name="mID">module id</param>
        /// <param name="dID">diary type id</param>
        /// <returns></returns>
        public DataSet GetModuleDetails(int mID, int dID)
        {
            try
            {
                SqlParameter[] sparam = new SqlParameter[3];
                try
                {
                    sparam[0] = new SqlParameter("@moduleID", SqlDbType.Int);
                    sparam[0].Value = mID;
                    sparam[1] = new SqlParameter("@diaryTypeID", SqlDbType.Int);
                    sparam[1].Value = dID;



                    return DataWrapper.ExecuteDataset(ConnStr, "Proc_Fetch_Module_Details", sparam);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }


        /// <summary>
        /// This function is called from transfer diary entry. This is used for fetching 
        /// user group and user list to be populated in To User list dropdown. 
        /// </summary>
        /// <param name="listID">based on list ID we fetch details</param>		
        /// <returns>Data set which contains user list</returns>
        public static DataSet GetUserListforDiaryEntry(int listID, out int mId)
        {
            mId = 0;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                try
                {
                    objDataWrapper.AddParameter("@listID", listID);

                    SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@modID", null, SqlDbType.Int, ParameterDirection.Output);

                    DataSet dsResult = objDataWrapper.ExecuteDataSet("Proc_GetUserForDiaryTransfer");

                    if (objSqlParameter != null)
                        if (objSqlParameter.Value.ToString() != "")
                            mId = Convert.ToInt32(objSqlParameter.Value.ToString());

                    return dsResult;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }


        /// <summary>
        /// This is an overload function for fetching diary details from mnt_diary_details
        /// based on module id, diary type id and lob_id
        /// </summary>
        /// <param name="mID">module id</param>
        /// <param name="dID">diary type id</param>
        /// <param name="LOBID">lob id</param>
        /// <returns>Dataset containing diary details</returns>
        public DataSet GetFollowDetails(int mID, int dID, int LOBID)
        {
            try
            {
                SqlParameter[] sparam = new SqlParameter[3];
                try
                {
                    sparam[0] = new SqlParameter("@moduleID", SqlDbType.Int);
                    sparam[0].Value = mID;
                    sparam[1] = new SqlParameter("@diaryTypeID", SqlDbType.Int);
                    sparam[1].Value = dID;
                    sparam[2] = new SqlParameter("@LOBID", SqlDbType.Int);
                    sparam[2].Value = LOBID;

                    return DataWrapper.ExecuteDataset(ConnStr, "Proc_RETRIEVE_Follow_Details", sparam);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }


        /// <summary>
        /// This is an overload function for fetching diary details from mnt_diary_details
        /// based on module id, diary type id and lob_id
        /// </summary>
        /// <param name="mID">module id</param>
        /// <param name="dID">diary type id</param>
        /// <param name="LOBID">lob id</param>
        /// <returns>Dataset containing diary details</returns>
        public DataSet GetModuleDetails(int mID, int dID, int LOBID)
        {
            try
            {
                SqlParameter[] sparam = new SqlParameter[3];
                try
                {
                    sparam[0] = new SqlParameter("@moduleID", SqlDbType.Int);
                    sparam[0].Value = mID;
                    sparam[1] = new SqlParameter("@diaryTypeID", SqlDbType.Int);
                    sparam[1].Value = dID;
                    sparam[2] = new SqlParameter("@LOBID", SqlDbType.Int);
                    sparam[2].Value = LOBID;

                    return DataWrapper.ExecuteDataset(ConnStr, "Proc_Fetch_Module_Details", sparam);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }


        /// <summary>
        /// retrieving appointements 
        /// </summary>
        /// <param name="userId">integer value of logged in user's userid</param>
        /// <param name="connString">connection string to be passed to datawrapper class</param>
        /// <returns>string containing dates seperated by ~</returns>
        public string SetDiaryDates(int userId, string strCustomerId)
        {
            DataSet dsCount = null;
            string lSearchColumnType = "~";

            SqlParameter[] sparam = new SqlParameter[2];
            try
            {
                sparam[0] = new SqlParameter("@USER_ID", SqlDbType.NVarChar, 4);
                sparam[0].Value = userId;
                sparam[1] = new SqlParameter("@CUSTOMER_ID", SqlDbType.NVarChar, 4);
                sparam[1].Value = strCustomerId;

                dsCount = DataWrapper.ExecuteDataset(ConnStr, "Proc_GetDiarydates", sparam);
                foreach (DataRow DataRow in dsCount.Tables[0].Rows)
                    lSearchColumnType += DataRow["followupdate"].ToString().Trim() + "~";

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
            return lSearchColumnType;
        }


        /// <summary>
        /// This function is used to update todolist for completing diary entry
        /// </summary>
        /// <param name="listId">list id which has to be made completed</param>
        /// <returns>numbers of rows affected</returns>
        public int CompleteDiaryEntry(int ListID)
        {
            return CompleteDiaryEntry(ListID, "N");
        }

        /// <summary>
        /// This function is used to update todolist for completing diary entry
        /// </summary>
        /// <param name="listId">list id which has to be made completed</param>
        /// <returns>numbers of rows affected</returns>
        public int CompleteDiaryEntry(int ListID, string listDesc)
        {
            return CompleteDiaryEntry(ListID, "N", listDesc);
        }


        public int CompleteDiaryEntry(int ListID, string ListOpen, string listDesc)
        {

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            int returnResult = CompleteDiaryEntry(objDataWrapper, ListID, ListOpen, listDesc);
            objDataWrapper.ClearParameteres();

            return returnResult;
        }

        public int CompleteDiaryEntry(DataWrapper objWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int ProcessRowId, string ListOpen)
        {
            return CompleteDiaryEntry(objWrapper, CustomerId, PolicyId, PolicyVersionId, ProcessRowId, ListOpen, "");
        }
        public int CompleteDiaryEntry(DataWrapper objWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int ProcessRowId, string ListOpen, string strCalledFrom)
        {
            string strStoredProc = "Proc_UpdateProcessDiaryEntryStatus";
            int returnResult = 0;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@PROCESS_ROW_ID", ProcessRowId);
                objWrapper.AddParameter("@LIST_OPEN", ListOpen);
                if (strCalledFrom != "")
                {
                    objWrapper.AddParameter("@CALLEDFROM", strCalledFrom);
                }


                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.TRANS_TYPE_ID = 11;
                    if (IsEODProcess)
                    {
                        objTransInfo.RECORDED_BY = EODUserID;
                    }
                    else
                    {
                        objTransInfo.RECORDED_BY = int.Parse(System.Web.HttpContext.Current.Session["userid"].ToString());
                    }
                    //Added For Itrack Issue #6552
                    objTransInfo.CLIENT_ID = CustomerId;
                    objTransInfo.POLICY_ID = PolicyId;
                    objTransInfo.POLICY_VER_TRACKING_ID = PolicyVersionId;
                    //objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);

                    //Added by Charles on 6-May-09 to add additional diary data to transaction log on completion
                    string strCustomInfo = "";
                    DataSet ds = new DataSet();
                    ds = GetPolicyDetails(CustomerId, 0, 0, PolicyId, PolicyVersionId);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        strCustomInfo += Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1790","")+": " + ds.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();//"Customer Name: " + ds.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
                        //Comented For Itrack Issue #6552
                        //strCustomInfo+=";Policy Number: "+ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
                        //strCustomInfo+=";Policy Version: "+ds.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();					
                        objTransInfo.CUSTOM_INFO = strCustomInfo;
                    }
                    //Added till here

                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc);


                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public int CompleteDiaryEntry(DataWrapper objWrapper, int ListID, string ListOpen, string listDesc)
        {
            string strStoredProc = "Proc_UpdateDiaryEntryStatus";
            int returnResult = 0;

            try
            {
                objWrapper.AddParameter("@LIST_ID", ListID);
                objWrapper.AddParameter("@LIST_OPEN", ListOpen);

                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.TRANS_TYPE_ID = 11;
                    if (IsEODProcess)
                    {
                        objTransInfo.RECORDED_BY = EODUserID;
                    }
                    else
                    {
                        objTransInfo.RECORDED_BY = int.Parse(System.Web.HttpContext.Current.Session["userid"].ToString());
                    }
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);

                    //Added by Charles on 4-May-09 to make Subject Line entry of diary to transaction log on completion
                    DataSet ds = new DataSet();
                    ds = FetchData(ListID);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                        listDesc += ";;Subject Line: " + ds.Tables[0].Rows[0]["SUBJECTLINE"].ToString();
                    //Added till here

                    objTransInfo.CUSTOM_INFO = listDesc;
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public int CompleteDiaryEntry(DataWrapper objWrapper, int ListID, string ListOpen)
        {
            string strStoredProc = "Proc_UpdateDiaryEntryStatus";
            int returnResult = 0;

            try
            {
                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.TRANS_TYPE_ID = 11;
                    if (IsEODProcess)
                    {
                        objTransInfo.RECORDED_BY = EODUserID;
                    }
                    else
                    {
                        objTransInfo.RECORDED_BY = int.Parse(System.Web.HttpContext.Current.Session["userid"].ToString());
                    }
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);

                    //Added by Charles on 4-May-09 to make Subject Line entry of diary to transaction log on completion
                    DataSet ds = new DataSet();
                    ds = FetchData(ListID, objWrapper);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["SUBJECTLINE"].ToString() != "")
                            objTransInfo.CUSTOM_INFO = ";;Subject Line: " + ds.Tables[0].Rows[0]["SUBJECTLINE"].ToString();
                    }
                    //Added till here
                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@LIST_ID", ListID);
                    objWrapper.AddParameter("@LIST_OPEN", ListOpen);
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                {
                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@LIST_ID", ListID);
                    objWrapper.AddParameter("@LIST_OPEN", ListOpen);
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                }
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public int CompleteDiaryEntry(TodolistInfo objDefModelInfo, string strCustomInfo)
        {
            string strStoredProc = "Proc_UpdateDiaryEntryStatus";
            int returnResult = -1; // to store result of database operation 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@LIST_ID", objDefModelInfo.LISTID);
                objDataWrapper.AddParameter("@LIST_OPEN", "N");

                if (base.TransactionLogRequired)
                {
                    //Added by Charles on 4-May-09 to add additional diary data to transaction log on completion
                    DataSet ds = new DataSet();
                    string strType = "", strQuoteNumber = "";
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    ds = FetchData(int.Parse(objDefModelInfo.LISTID.ToString()));
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objDefModelInfo.TOUSERID = int.Parse(ds.Tables[0].Rows[0]["TOUSERID"].ToString());
                        objDefModelInfo.FOLLOWUPDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["FOLLOWUPDATE"].ToString());
                        objDefModelInfo.SUBJECTLINE = ds.Tables[0].Rows[0]["SUBJECTLINE"].ToString();
                        objDefModelInfo.POLICY_NUMBER = ds.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
                        objDefModelInfo.APPLICATION_NUMBER = ds.Tables[0].Rows[0]["APP_NUMBER"].ToString();

                        //Added for Itrack Issue 6637 on 27 Oct 09
                        if (ds.Tables[0].Rows[0]["APP_ID"].ToString() != null && ds.Tables[0].Rows[0]["APP_ID"].ToString() != "")
                            objDefModelInfo.APP_ID = int.Parse(ds.Tables[0].Rows[0]["APP_ID"].ToString());
                        if (ds.Tables[0].Rows[0]["APP_VERSION_ID"].ToString() != null && ds.Tables[0].Rows[0]["APP_VERSION_ID"].ToString() != "")
                            objDefModelInfo.APP_VERSION_ID = int.Parse(ds.Tables[0].Rows[0]["APP_VERSION_ID"].ToString());
                        if (ds.Tables[0].Rows[0]["POLICY_ID"].ToString() != null && ds.Tables[0].Rows[0]["POLICY_ID"].ToString() != "")
                            objDefModelInfo.POLICY_ID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
                        if (ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString() != null && ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString() != "")
                            objDefModelInfo.POLICY_VERSION_ID = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

                        strType = ds.Tables[0].Rows[0]["TYPEDESC"].ToString();
                        strQuoteNumber = ds.Tables[0].Rows[0]["QUOTE_NUMBER"].ToString();

                        ds = ClsUser.GetUserName(objDefModelInfo.TOUSERID.ToString());
                        strCustomInfo += ";;To User: " + ds.Tables[0].Rows[0]["USERNAME"].ToString();

                        strCustomInfo += ";Type: " + strType;
                        strCustomInfo += ";Follow Up Date: " + objDefModelInfo.FOLLOWUPDATE.ToString();
                        strCustomInfo += ";Subject Line: " + objDefModelInfo.SUBJECTLINE.ToString() + ";";
                        //Commented For Itrack Issue #6552.
                        //						if(objDefModelInfo.POLICY_NUMBER.ToString()!="")
                        //							strCustomInfo+=";Policy Number: "+objDefModelInfo.POLICY_NUMBER.ToString();
                        if (objDefModelInfo.APPLICATION_NUMBER.ToString() != "")
                            strCustomInfo += ";Application Number= " + objDefModelInfo.APPLICATION_NUMBER.ToString();

                        //objTransInfo.APP_ID = objDefModelInfo.APP_ID; 						 


                        if (strQuoteNumber != "")
                            strCustomInfo += ";Quote Number: " + strQuoteNumber;
                    }
                    //Added till here


                    objTransInfo.CLIENT_ID = objDefModelInfo.CUSTOMER_ID;
                    objTransInfo.TRANS_TYPE_ID = 11;
                    objTransInfo.RECORDED_BY = objDefModelInfo.CREATED_BY;

                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);

                    objTransInfo.CUSTOM_INFO = strCustomInfo;
                    //Added Fir Itrack Issue #6552.
                    objTransInfo.POLICY_ID = objDefModelInfo.POLICY_ID;
                    objTransInfo.POLICY_VER_TRACKING_ID = objDefModelInfo.POLICY_VERSION_ID;

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                }
                //returnResult		   =	objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }

        public int UpdateDiaryEntryFollowUpDate(DataWrapper objWrapper, int CustomerId, int PolicyId, int PolicyVersionId, int ProcessRowId, DateTime FollowupDate)
        {
            string strStoredProc = "Proc_UpdateProcessDiaryFollowUpDate";
            int returnResult = 0;

            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@PROCESS_ROW_ID", ProcessRowId);
                objWrapper.AddParameter("@FOLLOWUPDATE", FollowupDate);
                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.TRANS_TYPE_ID = 392;
                    objTransInfo.CLIENT_ID = CustomerId;
                    objTransInfo.POLICY_ID = PolicyId;
                    objTransInfo.POLICY_VER_TRACKING_ID = PolicyVersionId;
                    objTransInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1795","")+ FollowupDate;//"Follow-up Date : " + FollowupDate;
                    if (IsEODProcess)
                    {
                        objTransInfo.RECORDED_BY = EODUserID;
                    }
                    else
                    {
                        objTransInfo.RECORDED_BY = int.Parse(System.Web.HttpContext.Current.Session["userid"].ToString());
                    }

                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);

                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc);


                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// This function is used to update todolist for completing diary entry
        /// </summary>
        /// <param name="listId">list id which has to be made completed</param>
        /// <returns>numbers of rows affected</returns>
        public int DeleteDiaryEntry(string listId, string tableName)
        {
            string strStoredProc = "proc_deleteRecords";
            int returnResult = -1; // to store result of database operation 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@LISTID", listId);
                objDataWrapper.AddParameter("@TABLENAME", tableName);
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }



        public int DeleteDiaryEntry(TodolistInfo objDefModelInfo, string strCustomInfo, string listDesc)
        {
            string strStoredProc = "proc_deletediary";
            int returnResult = -1; // to store result of database operation 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@LISTID", objDefModelInfo.LISTID);
                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.CLIENT_ID = objDefModelInfo.CUSTOMER_ID;
                    objTransInfo.TRANS_TYPE_ID = 393;
                    objTransInfo.RECORDED_BY = objDefModelInfo.CREATED_BY;
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    if (!strCustomInfo.Equals(""))
                        objTransInfo.CUSTOM_INFO = strCustomInfo;
                    else
                        objTransInfo.CUSTOM_INFO = listDesc;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                }
                //returnResult		   =	objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }

        /// <summary>
        /// This function is used to update todolist for completing diary entry
        /// </summary>
        /// <param name="listId">list id which has to be made completed</param>
        /// <returns>numbers of rows affected</returns>
        public int DeleteDiaryEntry(int listId, string listDesc)
        {
            string strStoredProc = "proc_deletediary";
            int returnResult = -1; // to store result of database operation 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@LISTID", listId);

                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    //objTransInfo.CLIENT_ID = int.Parse(Customer);
                    objTransInfo.TRANS_TYPE_ID = 393;
                    if (IsEODProcess)
                    {
                        objTransInfo.RECORDED_BY = EODUserID;
                    }
                    else
                    {
                        objTransInfo.RECORDED_BY = int.Parse(System.Web.HttpContext.Current.Session["userid"].ToString());
                    }
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    objTransInfo.CUSTOM_INFO = listDesc;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }

        //Added for Itrack Issue 6548 on 22 Oct 09
        public int DeleteDiaryEntry(TodolistInfo objDefModelInfo, string listId, string tableName, string listDesc)
        {
            string strStoredProc = "proc_deleteRecords";
            int returnResult = -1; // to store result of database operation 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@LISTID", listId);
                objDataWrapper.AddParameter("@TABLENAME", tableName);
                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.CLIENT_ID = objDefModelInfo.CUSTOMER_ID;
                    objTransInfo.APP_ID = objDefModelInfo.APP_ID;
                    objTransInfo.APP_VERSION_ID = objDefModelInfo.APP_VERSION_ID;
                    objTransInfo.POLICY_ID = objDefModelInfo.POLICY_ID;
                    objTransInfo.POLICY_VER_TRACKING_ID = objDefModelInfo.POLICY_VERSION_ID;
                    objTransInfo.TRANS_TYPE_ID = 393;
                    objTransInfo.RECORDED_BY = objDefModelInfo.CREATED_BY;
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    if (objDefModelInfo.CUSTOMER_ID != 0)
                        objTransInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1790", "") + " = " + objDefModelInfo.CUSTOMER_NAME + ";;" + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1791", "") + listDesc + ";;" + listId;//"Customer Name = " + objDefModelInfo.CUSTOMER_NAME + ";;" + "List Type = " + listDesc + ";;" + listId;
                    else
                        objTransInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1791", "") +listDesc + ";;" + listId.Replace("(", "").Replace(")", "").Trim();//"List Type = " + listDesc + ";;" + listId.Replace("(", "").Replace(")", "").Trim();

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }


        /// <summary>
        /// This function is used to update todolist for transfering diary entry to other users
        /// </summary>
        /// <param name="listId">list id which has to be transferred to other user</param>
        /// <returns>numbers of rows affected</returns>
        public int TransferDiaryEntry(int listId, int userId, string notes, string followdate)
        {
            string strStoredProc = "proc_TransferDiaryEntry";
            int returnResult = -1; // to store result of database operation 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@LISTID", listId);
                objDataWrapper.AddParameter("@USERID", userId);
                objDataWrapper.AddParameter("@NOTES", notes);
                objDataWrapper.AddParameter("@FOLLOWUPDATE", Convert.ToDateTime(followdate));


                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    //objTransInfo.CLIENT_ID = int.Parse(Customer);
                    objTransInfo.TRANS_TYPE_ID = 394;
                    objTransInfo.RECORDED_BY = userId;
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    DataSet dsUser = new DataSet();
                    dsUser = ClsUser.GetUserName(userId.ToString());
                    if (dsUser != null && dsUser.Tables.Count > 0)
                        if (dsUser.Tables[0].Rows.Count > 0)
                            objTransInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1793","")+" "+ dsUser.Tables[0].Rows[0][0].ToString();//"The diary item has been transfered to " + dsUser.Tables[0].Rows[0][0].ToString();
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }

        public int TransferDiaryEntry(int listId, int userId, string notes, string followdate, int UserID, string Customer, string AppId, string AppVersionId, string PolId, string PolVersionId)
        {
            string strStoredProc = "proc_TransferDiaryEntry";
            int returnResult = -1; // to store result of database operation 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@LISTID", listId);
                objDataWrapper.AddParameter("@USERID", userId);
                objDataWrapper.AddParameter("@NOTES", notes);
                objDataWrapper.AddParameter("@FOLLOWUPDATE", Convert.ToDateTime(followdate));

                if (base.TransactionLogRequired)
                {
                    ClsTransactionInfo objTransInfo = new ClsTransactionInfo();
                    objTransInfo.CLIENT_ID = int.Parse(Customer);
                    objTransInfo.TRANS_TYPE_ID = 394;
                    objTransInfo.RECORDED_BY = UserID;
                    if (AppId != "")
                    {
                        objTransInfo.APP_ID = int.Parse(AppId);
                    }
                    if (AppVersionId != "")
                    {
                        objTransInfo.APP_VERSION_ID = int.Parse(AppVersionId);
                    }
                    if (PolId != "")
                    {
                        objTransInfo.POLICY_ID = int.Parse(PolId);
                    }
                    if (PolVersionId != "")
                    {
                        objTransInfo.POLICY_VER_TRACKING_ID = int.Parse(PolVersionId);
                    }
                    objTransInfo.TRANS_DESC = ClsTransactionLog.GetTransactionDetails(objTransInfo.TRANS_TYPE_ID);
                    DataSet dsUser = new DataSet();
                    dsUser = ClsUser.GetUserName(userId.ToString());
                    if (dsUser != null && dsUser.Tables.Count > 0)
                        if (dsUser.Tables[0].Rows.Count > 0)
                            objTransInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1793","")+" "+ dsUser.Tables[0].Rows[0][0].ToString();//"The diary item has been transfered to " + dsUser.Tables[0].Rows[0][0].ToString();

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                }

                //returnResult		   =	objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }



        #region FETCHING DATA
        public DataSet FetchData(int ListID)
        {
            string strStoredProc = "Proc_FetchToDoList";
            DataSet dsCount = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
                objDataWrapper.AddParameter("@LIST_ID", ListID, SqlDbType.Int);

                
                dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
            return dsCount;
        }

        public DataSet FetchData(int ListID, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_FetchToDoList";
            DataSet dsCount = null;

            try
            {
                //DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@LIST_ID", ListID, SqlDbType.Int);

                dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
            return dsCount;
        }
        //Added for Itrack Issue 6548 on 22 Oct 09
        public DataSet FetchToDoListData(string ListID)
        {
            string strStoredProc = "Proc_FetchToDoListInfo";
            DataSet dsTemp = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LIST_ID", ListID, SqlDbType.VarChar);


                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
            return dsTemp;
        }
        #endregion
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add ClsDiary.Dispose implementation
        }

        #endregion

        #region Properties Code
        public bool TransactionLog
        {
            set
            {
                boolTransactionRequired = value;
            }
            get
            {
                return boolTransactionRequired;
            }
        }
        #endregion

        #region "Drop Down List Modal"
        /// <summary>
        /// Fills data into drop down list.
        /// data is fetched from database on first request and from cache for subsequent request.
        /// </summary>
        /// <param name="tObjCacheDropDown"></param>
        public static void GetToDoTypesInDropDown(CachedDropDownList tObjCacheDropDown)
        {
            if (!tObjCacheDropDown.setDataSourceFromCache("ToDoTypes"))//if data is not found in cache get from database
            {
                //				string strConString = ConfigurationSettings.AppSettings.Get("DB_CON_STRING");
                DataSet objDataSet = SqlHelper.ExecuteDataset(ConnStr, CommandType.Text, "select TYPEID,TYPEDESC from TODOLISTTYPES");
                tObjCacheDropDown.setCachedDataSource("ToDoTypes", objDataSet.Tables[0]);
            }
            tObjCacheDropDown.DataTextField = "TYPEDESC";
            tObjCacheDropDown.DataValueField = "TYPEID";
            tObjCacheDropDown.DataBind();
        }
        #endregion

        #region functions to be removed from here, pet shop approach -- delete this entire region after sending the build

        #endregion


        #region Get Diary Entry
        public DataSet GetDiary_Entry(TodolistInfo objTodolist, DataWrapper objDataWrapper)
        {
            string strStoredProc = "PROC_GetDiaryEntry";
            DataSet ds = null;


            objDataWrapper.ClearParameteres();
            //DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", objTodolist.CUSTOMER_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", objTodolist.POLICY_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objTodolist.POLICY_VERSION_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@LIST_TYPE_ID", objTodolist.LISTTYPEID, SqlDbType.Int);
            objDataWrapper.AddParameter("@MODULE_ID", objTodolist.MODULE_ID, SqlDbType.Int);
            ds = objDataWrapper.ExecuteDataSet(strStoredProc);
            objDataWrapper.ClearParameteres();
            return ds;

        }



        public int MarkCompleteDiaryEntry(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int TYPE_ID, int MODULE_ID, DataWrapper objDataWrapper)
        {
            try
            {
                DataSet ds = null;
                int RetVal = 0;
                int TypeID = 0;
                TodolistInfo objTodolist = new TodolistInfo();
                objTodolist.CUSTOMER_ID = CUSTOMER_ID;
                objTodolist.POLICY_ID = POLICY_ID;
                objTodolist.POLICY_VERSION_ID = POLICY_VERSION_ID;
                objTodolist.MODULE_ID = MODULE_ID;
                objTodolist.LISTTYPEID = TYPE_ID;
                ds = GetDiary_Entry(objTodolist, objDataWrapper);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    TypeID = int.Parse(ds.Tables[0].Rows[0]["LISTID"].ToString());

                if (TypeID != 0)
                    RetVal = CompleteDiaryEntry(objDataWrapper, TypeID, "N");
                return RetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        #endregion
        public enum enumModuleMaster
        {
            POLICY_PROCESS = 1,
            POLICY = 2,
            APPLICATION = 3,
            CUSTOMER = 4,
            CLAIM = 5,
            MAINTENANCE = 6

        }

        public enum enumDiaryType
        {
            ADJUSTMENTS_REQUESTS = 1,
            RENEWAL_REQUESTS = 2,
            CANCELLATION_REQUESTS = 3,
            REINSTATEMENTS_REQUESTS = 4,
            MEETING = 5,
            APPLICATION_ADDED = 6,
            NEW_BUSINESS_REQUESTS = 7,
            ENDORSEMENT_REQUESTS = 8,
            COMMUNICATION_FOLLOW_UPS = 9,
            ATTENTION_NOTE_FOLLOW_UPS = 10,
            CLAIM_REVIEW = 11,
            FOLLOW_UPS = 12,
            EMAIL = 13,
            PHONE_MESSAGES = 14,
            PROCESS_PENDING_REQUEST = 15,
            REVIEWING_POLICY_REQUEST = 16,
            RESERVES_LIMIT_EXCEEDED = 17,
            PAYMENT_LIMIT_EXCEEDED = 18,
            PINK_SLIP = 19,
            RENEWAL_IN_SUSPENSE = 20,
            RESCISSION_CHECK = 21,
            NON_RENEWAL_REQUESTS = 22,
            REWRITE_REQUESTS = 23,
            REVERT_BACK_REQUESTS = 24,
            REWRITE_IN_SUSPENSE = 25,
            ENDORSEMENT_IN_SUSPENSE_REQUESTS = 26,
            LETTER = 27,
            CANCELLATION_REQUESTS_NSF = 31,
            CANCELLATION_REQUESTS_NONPAYMENT = 32,
            ADJUSTER_LIMIT_NOTIFICATION = 34,
            APPLICATION_SUBMITTED_TO_POLICY = 35,
            PENDING_CORRECTIVE_USER = 37,
            AGENCY_TERMINATED = 38,
            DEPOSIT_ON_CANCELLED_POLICY = 39,
            CANCELLATION_REQUESTS_NONPAYMENT_DIARY = 40,
            PENDING_RENEWAL_FOLLOWUP = 41,
            MID_TERM_ENDORSEMENT = 42,
            LAUNCHED_ENDORSEMENT_ON_RENEWAL_TERM = 43,
            REVERSE_PAYMENT = 44,
            PAID_LOSS_FINAL_VOIDED = 45,
            Cancellation_Primary_Home_Policy = 46,
            POLICY_REJECTED = 47,
            APPLICATION_CREATION = 48,
        }

    }
}
