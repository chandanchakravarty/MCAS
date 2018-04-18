/******************************************************************************************
<Author				: -   Anurag Vermaa
<Start Date				: -	4/19/2005 2:49:55 PM
<End Date				: -	
<Description				: - 	This file contains functionality for adding and updating default message
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: 24 May 2005- 
<Modified By				: Gaurav- 
<Purpose				: Added GetXmlForPageControls() function- 

<Modified Date			: - Anshuman
<Modified By			: - June 08, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Maintenance;  


namespace Cms.BusinessLayer.BlCommon
{
    /// <summary>
    /// This file contains functionality for adding and updating default message
    /// </summary>
    public class ClsDefaultMessage : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
    {
        private const	string		MNT_MESSAGE_LIST			=	"MNT_MESSAGE_LIST";
        #region Private Instance Variables
            private			bool		boolTransactionLog;
            private int _MSG_ID;
        #endregion
        
        #region Public Properties
            public int 	MSG_ID
        {
            get
            {
                return _MSG_ID;
            }
        }
            public bool TransactionLog
        {
            set
            {
                boolTransactionLog	=	value;
            }
            get
            {
                return boolTransactionLog;
            }
        }
        #endregion
        
        #region private Utility Functions
        #endregion
        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsDefaultMessage()
        {
            boolTransactionLog	= base.TransactionLogRequired;
        }
        #endregion

		#region "Get Xml Methods"
		/// <summary>
		/// This function is used to generate XML
		/// </summary>
		/// <param name="strMessageId"></param>
		/// <param name="strMessageType"></param>
		/// <returns>XML for page controls in update mode</returns>
		public static string GetXmlForPageControls(string strMessageId, string strMessageType)
		{
			string strXml="";
			string strSql = "Select msg_id MSG_ID,msg_type MSG_TYPE,msg_code MSG_CODE,msg_desc MSG_DESC,msg_text MSG_TEXT,case msg_position when 'C' then 'Center' when 'L' then 'Left' when 'R' then 'Right' end MSG_POSITION_GRID,case msg_type when 'S' then case msg_apply_to when 'Y,Y' then 'Client,Agency' when 'Y,N' then 'Client' when 'N,Y' then 'Agency' when 'N,N' then ' ' end when 'L' then case msg_apply_to when 'Y' then 'Default' when 'N' then ' ' end end msg_apply_to_grid,msg_position MSG_POSITION,msg_apply_to MSG_APPLY_TO From mnt_message_list" ;
			strSql += " where msg_id="+strMessageId + " and msg_type='" + strMessageType  + "'";
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			
			if (objDataSet.Tables[0].Rows.Count == 0)
			{
				strXml ="";
			}
			else
			{
				strXml = objDataSet.GetXml().ToString();
			}
			return strXml;
			
			//return objDataSet.GetXml();
		}
		#endregion
        
        #region Add(Insert) functions
        public int Add(ClsDefaultMessageInfo objDefModelInfo)
        {
            string		strStoredProc	=	"Proc_InsertDefaultMessage";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDefModelInfo.IS_ACTIVE                 =   "Y"; 
                objDefModelInfo.CREATED_DATETIME          =   RecordDate;
                objDefModelInfo.LAST_UPDATED_DATETIME     =   RecordDate;
              
                objDataWrapper.AddParameter("@MSG_TYPE",objDefModelInfo.MSG_TYPE);
                objDataWrapper.AddParameter("@MSG_CODE",objDefModelInfo.MSG_CODE);
                objDataWrapper.AddParameter("@MSG_DESC",objDefModelInfo.MSG_DESC);
                objDataWrapper.AddParameter("@MSG_TEXT",objDefModelInfo.MSG_TEXT);
                objDataWrapper.AddParameter("@MSG_POSITION",objDefModelInfo.MSG_POSITION);
                objDataWrapper.AddParameter("@MSG_APPLY_TO",objDefModelInfo.MSG_APPLY_TO);
                objDataWrapper.AddParameter("@IS_ACTIVE",objDefModelInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY",objDefModelInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME",objDefModelInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY",objDefModelInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDefModelInfo.LAST_UPDATED_DATETIME);

                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@MSG_ID",MSG_ID,SqlDbType.Int,ParameterDirection.Output);
                int returnResult              = 0;
				returnResult				  =	objDataWrapper.ExecuteNonQuery(strStoredProc);
				_MSG_ID                       = int.Parse(objSqlParameter.Value.ToString());


                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                
                if(returnResult>0)
                    return _MSG_ID ;
                else
                    return returnResult ;
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

        
        /// <summary>
        /// Update method that recieves parameters required to save.
        /// </summary>
        /// <param name="oldModelInfo">Model object containing old data</param>
        /// <param name="newModelInfo">Model object containing for post data</param>
        /// <returns>integer value</returns>
        public int Update(ClsDefaultMessageInfo oldModelInfo, ClsDefaultMessageInfo newModelInfo)
        {   
            int returnResult                            = -1; //using this variable to store return value of database operation 
            SqlUpdateBuilder objBuilder                 = new SqlUpdateBuilder();
            objBuilder.WhereClause                      = " where msg_id=" + newModelInfo.MSG_ID;

            string strTranXML                           = "";  //using this variable to pass as an out paramater to GETUPDATESQL method 
            string strUpdate                            = ""; // using this variable for creating query and receiving query from GETUPDTAESQL method 
	
            strUpdate                                   =	"IF Not Exists(SELECT MSG_CODE "
                                                             + " FROM MNT_MESSAGE_LIST "
                                                             + " WHERE MSG_CODE='" + ReplaceInvalidCharecter(newModelInfo.MSG_CODE) 
                                                             + "' AND MSG_ID<>" + newModelInfo.MSG_ID 
                                                             + ")";
             DataWrapper objDataWrapper			    =	new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF); 
            try
            {
                if(base.TransactionLogRequired)
                {
                    newModelInfo.TransactLabel              = ClsCommon.MapTransactionLabel("/Cmsweb/Maintenance/AddDefaultMessage.aspx.resx");
                    // calling GetUpdateSQL method to compare and build query out of new and old model object      
                    strUpdate                              += objBuilder.GetUpdateSQL(oldModelInfo,newModelInfo,out strTranXML);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	    =	3;
                    objTransactionInfo.RECORDED_BY		    =	newModelInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC		    =	"System statement is modified";
                    objTransactionInfo.CHANGE_XML		    =	strTranXML;

                    returnResult					        =	objDataWrapper.ExecuteNonQuery(strUpdate,objTransactionInfo);
                }
                else
                {   
                    returnResult					        =	objDataWrapper.ExecuteNonQuery(strUpdate);
                }
                objDataWrapper.ClearParameteres();     
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);               
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
                if(objDataWrapper != null) objDataWrapper.Dispose();
            }                
               
            return returnResult; 
        }
        #endregion        
    }
}