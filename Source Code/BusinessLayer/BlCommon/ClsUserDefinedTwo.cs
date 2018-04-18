using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance;
using Cms.DataLayer;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsUserDefinedTwo.
	/// </summary>
    public class ClsUserDefinedTwo  : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable  
    {
        public ClsUserDefinedTwo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /* Function to populate the dropdownlist for Question type. The function returns a dataset which is bound to the dropdown control  */
        public DataSet fnGetQuestionType(int qID)
        {	
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@QTYPE",qID,SqlDbType.Int,ParameterDirection.Input);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchQuesType");
			
                return dsTemp;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}    
        }

        // This function takes QID as parameter and returns a DataSet which contains the 
        // QuestionType for that QID.
        public DataSet fnGetQuestionTypeID(string lStrQuestion,int grpID,int tabID,int screenID)
        {
					
            try
            {
                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@QID",lStrQuestion,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@GROUPID",grpID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@TABID",tabID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@SCREENID",screenID,SqlDbType.Int,ParameterDirection.Input);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchQuesTypeID");

                return dsTemp;
    	
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}    
        }	


        /* The function takes 8 parameters which are the values entered by the user
         * and stored in the DB. The function returns an integer where		 * 
         * 1 returns for successful insertion of the record otherwise and 0 for any error.		 
        */	
        public int fnSaveGridQuestion(int lIntQID,string lStrOptionText,int lIntOptionTypeID,string lintAnswerType, string lintListID,string lStrRequired,string lStrActive,string lStrGridlayout,int userID,int groupID,int TabId,int ScreenId)
        {
            ClsUserDefinedTwo udTwo=new ClsUserDefinedTwo(); 
            //int lIntmaxID=-1;    
        
            try
            {	
                        /* The following block will execute whether it is group question or a direct 
                 * question.
                 */
                string		strStoredProc	=	"Proc_InsertQUESTIONGRID";
                DateTime	RecordDate		=	DateTime.Now;
                int QGRIDOPTIONID=-1;
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

                objDataWrapper.AddParameter("@QID",lIntQID);
                objDataWrapper.AddParameter("@OPTIONTEXT",lStrOptionText);
                objDataWrapper.AddParameter("@OPTIONTYPEID",lIntOptionTypeID);
                objDataWrapper.AddParameter("@ISREQUIRED",lStrRequired);
                objDataWrapper.AddParameter("@LISTID",lintListID);
                objDataWrapper.AddParameter("@CARRIERID",1);   
                objDataWrapper.AddParameter("@ISACTIVE","Y");
                objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
                objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID);
                objDataWrapper.AddParameter("@ANSWERTYPEID",lintAnswerType);
                objDataWrapper.AddParameter("@GRIDOPTIONLAYOUT",lStrGridlayout);
                objDataWrapper.AddParameter("@GROUPID",groupID);
				objDataWrapper.AddParameter("@TABID",TabId);
				objDataWrapper.AddParameter("@SCREENID",ScreenId);

            
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@QGRIDOPTIONID",QGRIDOPTIONID,SqlDbType.Int,ParameterDirection.Output);

                int returnResult = 0;
                /*if(TransactionLogRequired)
                {
                    ObjAgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addAgency.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjAgencyInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY		=	ObjAgencyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {*/
                returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                //}
                QGRIDOPTIONID    = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (QGRIDOPTIONID == -1)
                {
                    return -1;
                }
                else
                {
                    return QGRIDOPTIONID    ;
                }            
            }	
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }        			
        }



        /* This function takes 2 parameters and populates the List drop down based on the second parameter. The
          * 1st parameter is carrierID while the second parameter refers to type of list item of the drop down		  
          */
        public DataSet fnList(string lStrType)
        {	
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LTYPE",lStrType,SqlDbType.Char,ParameterDirection.Input);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchQuesList");
			
                return dsTemp;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}    
          
        }



        /* Function to check that populate other controls reading the data from DB based on the 
         * QID passed.
        */
        public DataRow fnGetQuesOptionData(int lIntQID,int lIntQGRIDOPTIONID)
        {

            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@QID",lIntQID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@QGRIDOPTIONID",lIntQGRIDOPTIONID,SqlDbType.Int,ParameterDirection.Input);
                
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchGridQuesData");
			
                return dsTemp.Tables[0].Rows[0] ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}    
          
        }

        /* Function to check that populate other controls reading the data from DB based on the 
         * QID passed.
        */
        public DataRow fnGetQuesData(int lIntQID,int screenID,int tabID,int grpID)
        {

            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@QID",lIntQID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@screenID",screenID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@tabID",tabID,SqlDbType.Int,ParameterDirection.Input);
                //if(grpID!=-1)
                    objDataWrapper.AddParameter("@grpID",grpID,SqlDbType.Int,ParameterDirection.Input);
               // else
                //    objDataWrapper.AddParameter("@QID",-1,SqlDbType.Int,ParameterDirection.Input);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchQuesData");
			
                return dsTemp.Tables[0].Rows[0] ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}    
          
        }

        /* This function populates the list items for the user defined / system defined list
         * which is being used in the Question submission page. It returns a dataset which is 
         * used in popluating the options.		 
         */
        public DataSet fnOptionList(string lStrListID)
        {				

            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LISTID",int.Parse(lStrListID),SqlDbType.Int,ParameterDirection.Input);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchOptionList");
			
                return dsTemp;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}    

         
        }		
	

        /* The function takes 13 parameters which are the values entered by the user
         * and stored in the DB. The function returns an integer where
         * Returns QuestionID for successful insertion of the record otherwise returns 0.		 
        */	
        public int fnSaveQuestion(string lStrQuestionText,int lIntTabID,string lStrGroupID,int lIntQuestionType,string lStrAnswerType,string lStrQListID,string lStrRequired,string lStrActive,string lStrSuffix,string lStrPrefix,string lStrQuestionNotes,string lStrFlgDesc,string lStrFlgValue, string lStrTotalField,string lsStyle,string lsValidationType,string lsMaxLength,string lsColSpan,int userID,int screenID, out int grpID,string lsDepQuestionText,string lsDepQuesType,string lsDepAnsType,string lsDepQuesList)
        {
            UserDefinedOne udOne=new UserDefinedOne(); 
            int lIntmaxID=-1;    
            if(lStrGroupID.Length>0)
            {
                lIntmaxID=int.Parse(lStrGroupID);
            }
            try
            {	
                
                if(lStrGroupID.Trim()=="")	// If it is not a group question 
                {
                    lIntmaxID=udOne.fnInsertGroupData(1,screenID, lIntTabID.ToString() ,"",userID);
                    
                }	// End of if (if this is not a group questions
                grpID=lIntmaxID;
                /* The following block will execute whether it is group question or a direct 
                 * question.
                 */
                string		strStoredProc	=	"Proc_InsertUSERQUESTIONS";
                DateTime	RecordDate		=	DateTime.Now;
                int qidID=-1;
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

                objDataWrapper.AddParameter("@CARRIERID",1);
                objDataWrapper.AddParameter("@QUESTIONLISTID",lStrQListID);
                objDataWrapper.AddParameter("@TABID",lIntTabID);
                objDataWrapper.AddParameter("@QDESC",lStrQuestionText);                                                                            
                objDataWrapper.AddParameter("@ISREQ",lStrRequired);
                objDataWrapper.AddParameter("@SCREENID",screenID);
                objDataWrapper.AddParameter("@PREFIX",lStrPrefix);
                objDataWrapper.AddParameter("@SUFFIX",lStrSuffix);
                objDataWrapper.AddParameter("@QNOTES",lStrQuestionNotes);
                objDataWrapper.AddParameter("@QUESTIONTYPEID",lIntQuestionType);
                objDataWrapper.AddParameter("@GROUPID",lIntmaxID);
                objDataWrapper.AddParameter("@ANSWERTYPEID",lStrAnswerType);
                objDataWrapper.AddParameter("@FLGCOMPVALUE",lStrFlgValue);
                objDataWrapper.AddParameter("@ISACTIVE","Y");
                objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
                objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID);
                objDataWrapper.AddParameter("@REQTOTAL",lStrTotalField);
                objDataWrapper.AddParameter("@FLGQUESDESC",lStrFlgDesc);
            

				//manab is modifying the code on 29 june 2005
				//as per the requirements adding 4 extra columns in the insert script
				objDataWrapper.AddParameter("@STYLE",lsStyle);
				objDataWrapper.AddParameter("@MAXLENGTH",lsMaxLength);
				objDataWrapper.AddParameter("@VALIDATIONTYPE",lsValidationType);
				objDataWrapper.AddParameter("@COLSPAN",lsColSpan);
				//******************************************************

				//for depenedent questions
				objDataWrapper.AddParameter("@DEPQUESTIONTEXT",lsDepQuestionText);
				objDataWrapper.AddParameter("@DEPQUESTYPE",lsDepQuesType);
				objDataWrapper.AddParameter("@DEPANSTYPE",lsDepAnsType);
				objDataWrapper.AddParameter("@DEPQUESTIONLIST",lsDepQuesList);


                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@QID",qidID,SqlDbType.Int,ParameterDirection.Output);

                int returnResult = 0;
                /*if(TransactionLogRequired)
                {
                    ObjAgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addAgency.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjAgencyInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY		=	ObjAgencyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {*/
                returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                //}
                qidID    = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (qidID == -1)
                {
                    return -1;
                }
                else
                {
                    return qidID;
                }            
            }	
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }


        /*** This function will update the Question details. It accepts 12 parameters and returns an integer 
         * value based on the block executed. It returns 3 if the record is updated else it returns 0.
         * 		 
         **/
        public int fnUpdateQuestion(int lIntQID,string lStrQuestionText,int lIntTabID,int lIntQuestionType,string lStrAnswerType,string lStrQListID,string lStrRequired,string lStrSuffix,string lStrPrefix,string lStrQuestionNotes,string lStrFlagDesc,string lStrFlagValue,string lStrTotalField,string lsStyle,string lsValidationType,string lsMaxLength,string lsColSpan,int grpID,int screenID,int userID,string lsDepQuestionText,string lsDepQuesType,string lsDepAnsType,string lsDepQuesList)
        {	

            string		strStoredProc	=	"Proc_UpdateUSERQUESTIONS";
            DateTime	RecordDate		=	DateTime.Now;
			 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

            try
            {
                objDataWrapper.AddParameter("@QUESTIONLISTID",lStrQListID);
                objDataWrapper.AddParameter("@TABID",lIntTabID);
                objDataWrapper.AddParameter("@QDESC",lStrQuestionText);                                                                            
                objDataWrapper.AddParameter("@ISREQ",lStrRequired);
                objDataWrapper.AddParameter("@SCREENID",screenID);
                objDataWrapper.AddParameter("@PREFIX",lStrPrefix);
                objDataWrapper.AddParameter("@SUFFIX",lStrSuffix);
                objDataWrapper.AddParameter("@QNOTES",lStrQuestionNotes);
                objDataWrapper.AddParameter("@QUESTIONTYPEID",lIntQuestionType);
                objDataWrapper.AddParameter("@GROUPID",grpID);
                objDataWrapper.AddParameter("@ISACTIVE","Y");
                objDataWrapper.AddParameter("@ANSWERTYPEID",lStrAnswerType);
                objDataWrapper.AddParameter("@FLGCOMPVALUE",lStrFlagValue);                
                objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
                objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID);
                objDataWrapper.AddParameter("@REQTOTAL",lStrTotalField);
                objDataWrapper.AddParameter("@FLGQUESDESC",lStrFlagDesc);
                objDataWrapper.AddParameter("@QID",lIntQID);
                

				//manab is modifying the code on 29 june 2005
				//as per the requirements adding 4 extra columns in the insert script
				objDataWrapper.AddParameter("@STYLE",lsStyle);
				objDataWrapper.AddParameter("@MAXLENGTH",lsMaxLength);
				objDataWrapper.AddParameter("@VALIDATIONTYPE",lsValidationType);
				objDataWrapper.AddParameter("@COLSPAN",lsColSpan);
				//******************************************************

				//for depenedent questions
				objDataWrapper.AddParameter("@DEPQUESTIONTEXT",lsDepQuestionText);
				objDataWrapper.AddParameter("@DEPQUESTYPE",lsDepQuesType);
				objDataWrapper.AddParameter("@DEPANSTYPE",lsDepAnsType);
				objDataWrapper.AddParameter("@DEPQUESTIONLIST",lsDepQuesList);

                int returnResult = 0;
                /*if(TransactionLogRequired)
                {
                    ObjAgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addAgency.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjAgencyInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY		=	ObjAgencyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {*/
                
                returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                //}
                objDataWrapper.ClearParameteres();
                return returnResult;
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


        /*** This function will update the Grid Question details. It accepts 8 parameters and returns an integer 
         * value based on the block executed. It returns 2 if the record is updated else it returns 0.
         * 		 
         **/
        public int fnUpdateGridQuestion(int lIntQGridOptionID,int lIntQID, string lStrOptionText,int lIntQuestionType,string lStrAnswerType,string lStrQListID,string lStrRequired,string lStrActive,string lStrLayout,int userID,int groupID,int TabId,int ScreenId)
        {	
            string		strStoredProc	=	"Proc_UpdateQuestionOptionGrid";
            DateTime	RecordDate		=	DateTime.Now;
			 
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

            try
            {
                objDataWrapper.AddParameter("@QID",lIntQID);
                objDataWrapper.AddParameter("@QGRIDOPTIONID",lIntQGridOptionID);
                objDataWrapper.AddParameter("@OPTIONTEXT",lStrOptionText);
                objDataWrapper.AddParameter("@OPTIONTYPEID",lIntQuestionType);
                objDataWrapper.AddParameter("@ISREQUIRED",lStrRequired);
                objDataWrapper.AddParameter("@LISTID",lStrQListID);
                objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
                objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID);
                objDataWrapper.AddParameter("@ANSWERTYPEID",lStrAnswerType);
                objDataWrapper.AddParameter("@GRIDOPTIONLAYOUT",lStrLayout);
                objDataWrapper.AddParameter("@GROUPID",groupID);
				objDataWrapper.AddParameter("@TABID",TabId);
				objDataWrapper.AddParameter("@SCREENID",ScreenId);

                
                int returnResult = 0;
                /*if(TransactionLogRequired)
                {
                    ObjAgencyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addAgency.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjAgencyInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY		=	ObjAgencyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {*/
                
                returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                //}
                objDataWrapper.ClearParameteres();
                return returnResult;
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

        //This function updates the Question for activation and deactivation. This function returns the QuestionID of 
        // the updated Question after successful updation or it returns 0.
        public int fnDeactivateQuestion(int lIntQuestionID,string lStrIsActive,int screenID,int tabID,int grpID,int userID)
        {
            try
            {
                DataSet dsTemp = new DataSet();

               	DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
			            
                objDataWrapper.AddParameter("@QID",lIntQuestionID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@screenID",screenID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@tabID",tabID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@grpID",grpID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@ISACTIVE",lStrIsActive,SqlDbType.NChar,ParameterDirection.Input,2);
                objDataWrapper.AddParameter("@LASTMODIFIEDDATE",DateTime.Now,SqlDbType.DateTime,ParameterDirection.Input);
                objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID,SqlDbType.Int,ParameterDirection.Input);

                int returnResult=objDataWrapper.ExecuteNonQuery("Proc_DeactivateQuestion");
			
                return returnResult;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}    
        }
    }
}
