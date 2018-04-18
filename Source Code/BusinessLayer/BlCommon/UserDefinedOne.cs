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
	/// Summary description for UserDefinedOne.
	/// </summary>
	public class UserDefinedOne : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable   
	{
		public UserDefinedOne()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        #region Private Instance Variables
        
        #endregion

        /* The following function accepts the CarrierID and gets the data from DB. It returns a Dataset which will
         * be used in the application layer to populate the controls. This function is being called in screendetails.aspx.cs
         */

        
        public int fnInsertNewList(string lStrType,string lStrListName)
        {
            string		strStoredProc	=	"Proc_InsertList";
            DateTime	RecordDate		=	DateTime.Now;
            int listID=-1;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

            try
            {
                objDataWrapper.AddParameter("@TYPE",lStrType);
                objDataWrapper.AddParameter("@NAME",lStrListName);
                objDataWrapper.AddParameter("@ISACTIVE","Y");
                objDataWrapper.AddParameter("@CARRIERID",1);
                
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LISTID",listID,SqlDbType.Int,ParameterDirection.Output);

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
                listID    = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (listID == -1)
                {
                    return -1;
                }
                else
                {
                    return listID;
                }
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
		


         /* The following function accepts the screenID and gets the data from DB. It returns a Datarow which will
         * be used in the application layer to populate the controls. This function is being called ScreenDetails.aspx
         */
        public DataRow fnGetScreenData(int lIntScreenID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@SCREENID",lIntScreenID,SqlDbType.NVarChar,ParameterDirection.Input , 16);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchScreenData");
			
                return dsTemp.Tables[0].Rows[0]  ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}
        }	
	
        /* The following function accepts the carrierID and classID and gets the data from DB. It returns a DataSet which will
         * be used in the application layer to populate the controls. This function is being called in screendetails.aspx.cs
         */
        public DataSet fnGetSubClass(int lIntLOBID)
        {	
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LOBID",lIntLOBID,SqlDbType.NVarChar,ParameterDirection.Input , 16);
                objDataWrapper.AddParameter("@Lang_Id",BL_LANG_ID);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchSubLOBData");
			
                return dsTemp;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}
        }


        /* The following function accepts 4 parameters which will be used to while adding a new screen details. It returns the ScreenID after
         * successful insertion.
         */
        public int fnInsertScreenData(string lStrScreenName,string lStrClassID, string lStrSubClassID,string lStrDispName,int userId)
        {
            string		strStoredProc	=	"Proc_InsertScreen";
            DateTime	RecordDate		=	DateTime.Now;
            int screenID=-1;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

            try
            {
                objDataWrapper.AddParameter("@CLASSID",int.Parse(lStrClassID));
                objDataWrapper.AddParameter("@SUBCLASSID",int.Parse(lStrSubClassID));
                objDataWrapper.AddParameter("@PROFESSIONID",System.DBNull.Value);
                objDataWrapper.AddParameter("@SCREENNAME",lStrScreenName);
                objDataWrapper.AddParameter("@ISACTIVE","Y");                
                objDataWrapper.AddParameter("@DISPLAYNAME",lStrDispName);                                
                objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
                objDataWrapper.AddParameter("@LASTMODIFIEDBY",userId);
                
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@SCREENID",screenID,SqlDbType.Int,ParameterDirection.Output);

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
                screenID    = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
            //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (screenID == -1)
                {
                    return -1;
                }
                else
                {
                   return screenID;
                }
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

		
		/* The following function accepts 5 parameters which will be used to while adding a new screen details. 
		 */
		public int fnUpdateScreenData(string lStrScreenName,string lStrScreenID,string lStrClassID, string lStrSubClassID,string lStrDispName,int userId)
		{
			string		strStoredProc	=	"Proc_UpdateScreen";
			DateTime	RecordDate		=	DateTime.Now;
		 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

			try
			{
				objDataWrapper.AddParameter("@CLASSID",int.Parse(lStrClassID));
				objDataWrapper.AddParameter("@SCREENID",int.Parse(lStrScreenID));
				objDataWrapper.AddParameter("@SUBCLASSID",int.Parse(lStrSubClassID));
				objDataWrapper.AddParameter("@PROFESSIONID",System.DBNull.Value);
				objDataWrapper.AddParameter("@SCREENNAME",lStrScreenName);
				objDataWrapper.AddParameter("@ISACTIVE","Y");                
				objDataWrapper.AddParameter("@DISPLAYNAME",lStrDispName);                                
				objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
				objDataWrapper.AddParameter("@LASTMODIFIEDBY",userId);
                
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
				//    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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
		
		
		/* The following function accepts 5 parameters which will be used to while adding a new screen details. 
		 */
		public int fnDeactivateScreen(string lStrScreenID,string lStrStatus,int userId)
		{
			string		strStoredProc	=	"Proc_DeactivateScreen";
			DateTime	RecordDate		=	DateTime.Now;
		 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

			try
			{
				objDataWrapper.AddParameter("@SCREENID",int.Parse(lStrScreenID));
				objDataWrapper.AddParameter("@ISACTIVE",lStrStatus);                
				objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
				objDataWrapper.AddParameter("@LASTMODIFIEDBY",userId);
                
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
				//    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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
		
		
		
		/***************************************************************************/
        // The following code are required for the  Tab maintenance.

        // This function retrieves the details of the Tab based on the TabID passed.
        public DataRow fnGetTabData(int lIntTabID,  int lIntScreenID)
        {						
           
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@TABID",lIntTabID,SqlDbType.NVarChar,ParameterDirection.Input , 16);
				objDataWrapper.AddParameter("@SCREENID",lIntScreenID,SqlDbType.NVarChar,ParameterDirection.Input , 16);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchTabData");
			
                return dsTemp.Tables[0].Rows[0]  ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {
                
            }
        }		

        // This function inserts a new Tab in the QuestionTabMaster table. This returns the inserted 
        // Tabid as an integer. 
		//changed by manab on 28 june 2005
		//adding a parameter to add the repeat controls
        public int InsertTabData(int CarrierID, string lStrTabName,string lStrRepeatControls,string lStrScreenID,int userID)
        {
            string		strStoredProc	=	"Proc_InsertTab";
            DateTime	RecordDate		=	DateTime.Now;
            int tabID=-1;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

            try
            {
                objDataWrapper.AddParameter("@CARRIERID",CarrierID);
                objDataWrapper.AddParameter("@SCREENID",lStrScreenID);
                objDataWrapper.AddParameter("@TABNAME",lStrTabName);
				objDataWrapper.AddParameter("@REPEATCONTROLS",lStrRepeatControls);
                objDataWrapper.AddParameter("@ISACTIVE","Y");                                                            
                objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
                objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID);
                
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@TABID",tabID,SqlDbType.Int,ParameterDirection.Output);

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
                tabID    = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (tabID == -1)
                {
                    return -1;
                }
                else
                {
                    return tabID;
                }
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

		// This function inserts a new Tab in the QuestionTabMaster table. This returns the inserted 
		// Tabid as an integer. 
		//changed by manab on 28 june 2005
		//adding a parameter to add the repeat controls
		public int UpdateTabData(int CarrierID,int tabID,string lStrTabName,string lStrRepeatControls,string lStrScreenID,int userID)
		{
			string		strStoredProc	=	"Proc_UpdateTab";
			DateTime	RecordDate		=	DateTime.Now;
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

			try
			{
				objDataWrapper.AddParameter("@TABID",tabID);
				objDataWrapper.AddParameter("@CARRIERID",CarrierID);
				objDataWrapper.AddParameter("@SCREENID",lStrScreenID);
				objDataWrapper.AddParameter("@TABNAME",lStrTabName);
				objDataWrapper.AddParameter("@REPEATCONTROLS",lStrRepeatControls);
				//changed by manab on 28 june 2005 updating repetable controls columns                                                    
				objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
				objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID);
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

 		public int DeactivateTab(int CarrierID, string lStrTabID,string lStrScreenID,string lStrStatus,int userId)
		{
			string		strStoredProc	=	"Proc_DeactivateTab";
			DateTime	RecordDate		=	DateTime.Now;
		 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

			try
			{
				objDataWrapper.AddParameter("@CARRIERID",CarrierID);
				objDataWrapper.AddParameter("@TABID",int.Parse(lStrTabID));
				objDataWrapper.AddParameter("@SCREENID",int.Parse(lStrScreenID));
				objDataWrapper.AddParameter("@ISACTIVE",lStrStatus);                
				objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
				objDataWrapper.AddParameter("@LASTMODIFIEDBY",userId);
                
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
		
		public int DeactivateGroup(int CarrierID, string lStrTabID,string lStrScreenID,string lStrGroupID, string lStrStatus,int userId)
		{
			string		strStoredProc	=	"Proc_DeactivateGroup";
			DateTime	RecordDate		=	DateTime.Now;
		 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

			try
			{
				objDataWrapper.AddParameter("@CARRIERID",CarrierID);
				objDataWrapper.AddParameter("@TABID",int.Parse(lStrTabID));
				objDataWrapper.AddParameter("@SCREENID",int.Parse(lStrScreenID));
				objDataWrapper.AddParameter("@GROUPID",int.Parse(lStrGroupID));
				objDataWrapper.AddParameter("@ISACTIVE",lStrStatus);                
				objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
				objDataWrapper.AddParameter("@LASTMODIFIEDBY",userId);
                
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
		
        /***************************************************************************/
        // The following code are required for the Group maintenance.


        public DataRow fnGetGroupData(int lIntGroupID,int lIntTabID ,int lIntScreenID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@TABID",lIntTabID,SqlDbType.NVarChar,ParameterDirection.Input , 16);
                objDataWrapper.AddParameter("@GROUPID",lIntGroupID,SqlDbType.NVarChar,ParameterDirection.Input , 16);
				objDataWrapper.AddParameter("@SCREENID",lIntScreenID,SqlDbType.NVarChar,ParameterDirection.Input , 16);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchGroupData");
			
                return dsTemp.Tables[0].Rows[0]  ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}
        }


        /***************************************************************************/
        // The following code are required for the Group maintenance.


        public DataRow fnGetMaxGroupNo(int lIntTabID ,int lIntScreenID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@TABID",lIntTabID,SqlDbType.NVarChar,ParameterDirection.Input , 16);                
                objDataWrapper.AddParameter("@SCREENID",lIntScreenID,SqlDbType.NVarChar,ParameterDirection.Input , 16);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchMaxGroupNo");
			
                return dsTemp.Tables[0].Rows[0]  ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}
        }



        /*This function accepts 2 parameters as Group Name and Tab ID and inserts these details
         * in the Database. The GroupID and the SeqNo is assigned with incremented Max value for
         * that Tab ID. The function returns an integer which is the inserted GroupID.
         */
        public int fnInsertGroupData(int CarrierID,int ScreenID, string lStrTabID,string lStrGroupName,int userID)
        {

            string		strStoredProc	=	"Proc_InsertGroup";
            DateTime	RecordDate		=	DateTime.Now;
            int GROUPID=-1;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

            try
            {
				objDataWrapper.AddParameter("@CARRIERID",CarrierID);        
				objDataWrapper.AddParameter("@SCREENID",ScreenID);        
                objDataWrapper.AddParameter("@TABID",lStrTabID);
                objDataWrapper.AddParameter("@GROUPNAME",lStrGroupName);                 
                if(lStrGroupName!="")
                    objDataWrapper.AddParameter("@GROUPTYPE","G");  
                else
                    objDataWrapper.AddParameter("@GROUPTYPE","");  

                objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
                objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID);
                
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@GROUPID",GROUPID,SqlDbType.Int,ParameterDirection.Output);

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
                GROUPID    = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (GROUPID == -1)
                {
                    return -1;
                }
                else
                {
                    return GROUPID;
                }
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

		public int fnUpdateGroupData(int CarrierID,int ScreenID,string TabID, string GroupID,string lStrGroupName,int userID)
		{

			string		strStoredProc	=	"Proc_UpdateGroup";
			DateTime	RecordDate		=	DateTime.Now;
			 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

			try
			{
				objDataWrapper.AddParameter("@SCREENID",ScreenID);
				objDataWrapper.AddParameter("@TABID",TabID);	
				objDataWrapper.AddParameter("@GROUPID",GroupID);	
				objDataWrapper.AddParameter("@GROUPNAME",lStrGroupName);
				objDataWrapper.AddParameter("@CARRIERID",1);        //CarrierID  
				objDataWrapper.AddParameter("@LASTMODIFIEDDATE",RecordDate);
				objDataWrapper.AddParameter("@LASTMODIFIEDBY",userID);
                

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


        /******************************************************************************/
        /* The following functions are related with list manipulation activities.
        /* This function returns the list name of the LIST ID which has been passed as the parameter.		 
         */
        public string fnGetListData(string lStrLookUpID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LISTID",lStrLookUpID,SqlDbType.Int,ParameterDirection.Input);
              
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchListData");
			
                return dsTemp.Tables[0].Rows[0][0].ToString ()  ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}
        }

        /* This function returns the DataSet which is used to populate the drop down control in the application.		 
         */
        public DataSet fnGetList(string lStrLookUpID)
        {	
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LISTID",lStrLookUpID,SqlDbType.Int,ParameterDirection.Input);
              
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchMainListData");
			
                return dsTemp ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}
        }

        // This function returns the Option name based on the passed parameters which are 
        // listID and ListOPtionID. This function is being called in EditList.aspx.cs
        public string fnGetOptionName(string lStrLookUpID, string lStrLookUpValueID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
			
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LISTID",lStrLookUpID,SqlDbType.Int,ParameterDirection.Input);
                objDataWrapper.AddParameter("@LISTOPTIONID",lStrLookUpValueID,SqlDbType.Int,ParameterDirection.Input);
              
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_FetchOptionName");
			
                return dsTemp.Tables[0].Rows[0][0].ToString() ;
				 
            }
            catch(Exception exc)
            {throw (exc);}
            finally
            {}            
        }


        /* This function inserts the options for the newly created list into the database. It accepts 2 parameters which 
         * are ListID and the OptionName. This function is being called in EditList.aspx.cs
         */
        public int fnInsertListData(int lIntLookID,string lStrListOptionName,int lIntCarrierID)
        {
         
            string		strStoredProc	=	"Proc_InsertQuestionListOptionMaster";
 
            int optionID=-1;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

            try
            {
                objDataWrapper.AddParameter("@CARRIERID",lIntCarrierID);        
                objDataWrapper.AddParameter("@LISTID",lIntLookID);        
                objDataWrapper.AddParameter("@OPTIONNAME",lStrListOptionName);
                objDataWrapper.AddParameter("@ISACTIVE","Y");                 
                
                
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@OPTIONID",optionID,SqlDbType.Int,ParameterDirection.Output);

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
                optionID    = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (optionID == -1)
                {
                    return -1;
                }
                else
                {
                    return optionID;
                }
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


        /* This function inserts the options for the newly created list into the database. It accepts 2 parameters which 
         * are ListID and the OptionName. This function is being called in EditList.aspx.cs
         */
        public int fnUpdateListData(string txtDescp,int lblListID,int selDescp)
        {
         
            string		strStoredProc	=	"Proc_UpdateQuestionListOptionMaster";
 
            
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

            try
            {
                objDataWrapper.AddParameter("@OPTIONID",selDescp);        
                objDataWrapper.AddParameter("@LISTID",lblListID);        
                objDataWrapper.AddParameter("@OPTIONNAME",txtDescp);
               
                int returnResult = -1;
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
                //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (returnResult == -1)
                {
                    return -1;
                }
                else
                {
                    return returnResult	;
                }
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

		
		/******************************************************************************/
		// The following function are related with the sequence of the questions.
		// This function gets the groupID of a particular Tab.
//		public DataSet fnGetCounterCount(string lStrTabID)
//		{	
//			//			string lStrSqlString;
//			StringBuilder lStrSqlString = new StringBuilder (500);
//			try
//			{
//				objDataAccess=new DataAccessComponent(EbxSession.Get("BrokerDSN"));
//				lStrSqlString.Append (" SELECT DISTINCT GROUPID");
//				lStrSqlString.Append (" FROM USERQUESTIONS WHERE CARRIERID=" + EbxSession.Get("CarrierID")+ " AND TABID = " + lStrTabID);
//				DataSet lObjDS=new DataSet();				
//				lObjDS = objDataAccess.ExecuteSelectQuery(lStrSqlString.ToString());	
//				return lObjDS;
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//			finally
//			{	
//				objDataAccess.CloseConnection();
//				objDataAccess.Dispose();
//			}	   
//		}

		/******************************************************************************/
		public int  fnGetTotalTab(string lStrScreenID)
		{	
 
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@SCREENID",lStrScreenID,SqlDbType.Int,ParameterDirection.Input);
              
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabCount");
				if (dsTemp!=null)
				{
					return int.Parse (dsTemp.Tables[0].Rows[0][0].ToString());
				}
				else
				{
					return 0;
				}
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 
		}

		// This function gets the TabID of a particular Screen.
		public DataSet GetTabDetails(string lStrScreenID)
		{	 	 
			 
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@SCREENID",lStrScreenID,SqlDbType.Int,ParameterDirection.Input);
              
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_fnGetTabDetails");
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 
		}

		// This function has been written by MPhate. This is being used in Updating Question Sequence
		public int fnUpdateTabSequence(string lStrScreenID,string lIntSeqNo,int lIntTabId)
		{
			try
				{
					 
			
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@SCREENID",lStrScreenID,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@SEQNO",lIntSeqNo,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@TABID",lIntTabId,SqlDbType.Int,ParameterDirection.Input);

					int retVal = objDataWrapper.ExecuteNonQuery("Proc_UpdateTabSequence");
					return retVal;
				 
				}
				catch(Exception exc)
				{throw (exc);}
				finally
				{} 

		}	

		// This function returns the Tab name for the passed Tab ID 
		public static string GetTabNameText(string lstrScreenID, string lstrCarrierID, string lstrTabID)
		{	
			 
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
				objDataWrapper.AddParameter("@CARRIERID",lstrCarrierID,SqlDbType.Int);
				objDataWrapper.AddParameter("@SCREENID",lstrScreenID,SqlDbType.Int);
				objDataWrapper.AddParameter("@TABID",lstrTabID,SqlDbType.Int);
				 		
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabName");			
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

		// This function returns the Group name for the passed Tab ID and Group ID 
		public static string GetGroupNameText(string lStrScreenID,string lStrTabID,string lStrGroupID)
		{		 
			
				try
				{
					DataSet dsTemp = new DataSet();
			
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);	
				
					objDataWrapper.AddParameter("@CARRIERID",1,SqlDbType.Int);
					objDataWrapper.AddParameter("@SCREENID",lStrScreenID,SqlDbType.Int);
					objDataWrapper.AddParameter("@TABID",lStrTabID,SqlDbType.Int);
					objDataWrapper.AddParameter("@GROUPID",lStrGroupID,SqlDbType.Int);

				 		
					dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetGroupName");			
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

		/******************************************************************************/
		// The following function are related with the sequence of the questions.
		// This function gets the groupID of a particular Tab.
		public static DataSet GetGroupCount(string lStrScreenID, string lStrTabID)
		{	 
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@SCREENID",lStrScreenID,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@TABID",lStrTabID,SqlDbType.Int,ParameterDirection.Input);      
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetGroupCount");
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 
			
		}

		public DataSet fnGetTabGroupQues(string lStrScreenID, string lStrTabID)
		{	
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@SCREENID",lStrScreenID,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@TABID",lStrTabID,SqlDbType.Int,ParameterDirection.Input);      
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabGroupQuest");
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 
		}

		// This function gets the groupname where TabID and GroupID which are passed as input parameters.
		public DataSet fnGetGroupSeqName(int lStrScreenID, string lStrTabID, int lIntGroupID)
		{	
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@SCREENID",lStrScreenID ,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@TABID",lStrTabID,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@GROUPID",lIntGroupID,SqlDbType.Int,ParameterDirection.Input);
      
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetGroupSeqName");
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 
		}

		// This function has been written by MPhate. This is being used in Updating Question Sequence
		public DataSet fnGetTabGroupQuestion(string lStrScreenID ,string lStrTabID, string lStrGroupID)
		{	
			
				
				try
				{
					DataSet dsTemp = new DataSet();
			
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@SCREENID",lStrScreenID ,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@TABID",lStrTabID,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@GROUPID",lStrGroupID,SqlDbType.Int,ParameterDirection.Input);
      
					dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetTabGroupQuestion");
					return dsTemp;
				 
				}
				catch(Exception exc)
				{throw (exc);}
				finally
				{} 
		}

		// This function has been written by MPhate. This is being used in Updating Question Sequence
		public int fnUpdateQuestionSequence(int lIntScreenID, int lIntTabId,int lIntGroupID,string lStrSeqNo)
		{
							
				
				try
				{
					DataSet dsTemp = new DataSet();
			
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@SCREENID",lIntScreenID ,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@TABID",lIntTabId,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@GROUPID",lIntGroupID,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@SEQNO",lStrSeqNo,SqlDbType.Int,ParameterDirection.Input);
 
					int retVal=0;
					retVal = objDataWrapper.ExecuteNonQuery("Proc_UpdateQuestionSequence");	
					return retVal;
				 
				}
				catch(Exception exc)
				{throw (exc);}
				finally
				{} 
				
			

		}	

		public int fnUpdateGroupSeqNo(int lIntScreenID, int lIntTabId,int lIntQID,string lStrSeqNo)
		{
			
				
				try
				{
					DataSet dsTemp = new DataSet();
			
					DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objDataWrapper.AddParameter("@SCREENID",lIntScreenID ,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@TABID",lIntTabId,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@QID",lIntQID,SqlDbType.Int,ParameterDirection.Input);
					objDataWrapper.AddParameter("@SEQNO",lStrSeqNo,SqlDbType.Int,ParameterDirection.Input);
     
					int retVal=0;
					retVal = objDataWrapper.ExecuteNonQuery("Proc_UpdateGroupSeqNo");	
					return retVal;
				 
				}
				catch(Exception exc)
				{throw (exc);}
				finally
				{} 
				
		}	


		public DataSet getMappingFields()
		{

			try
			{
				DataSet dsTemp = new DataSet();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetMappingFields");	
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 
		}

		public DataSet getQuestionList(string ScreenId,string CarrierId)
		{

			try
			{
				DataSet dsTemp = new DataSet();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CARRIERID",CarrierId ,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@SCREENID",ScreenId ,SqlDbType.Int,ParameterDirection.Input);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetUnMappedQuestionList");	
				
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 

		}

		public DataSet getMappedQuestion(string ScreenId,string CarrierId)
		{

			try
			{
				DataSet dsTemp = new DataSet();
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CARRIERID",CarrierId ,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@SCREENID",ScreenId ,SqlDbType.Int,ParameterDirection.Input);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetMappedQuestion");	
				return dsTemp;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 

		}
		public int UpdateQuestionMapping(string ScreenId,string CarrierId,string Qid,string FieldId)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@SCREENID",ScreenId ,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@CARRIERID",CarrierId,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@QUESMAPPINGID",FieldId,SqlDbType.Int,ParameterDirection.Input);
				objDataWrapper.AddParameter("@QID",Qid,SqlDbType.Int,ParameterDirection.Input);
     
				int retVal=0;
				retVal = objDataWrapper.ExecuteNonQuery("PROC_UPDATEMAPPEDQUESTION");	
				return retVal;
				 
			}
			catch(Exception exc)
			{throw (exc);}
			finally
			{} 
		}

	}
}
