/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> March 25,2005
	<End Date				: - >
	<Description			: - > This file contains functionality for adding and updating default values
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - Anshuman
	<Modified By			: - June 08, 2005
	<Purpose				: - transaction description modified
    
    <Modified Date			: - > 
	<Modified By			: - > 
	<Purpose				: - > 
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
	/// Summary description for ClsDefaultValue.
	/// </summary>
	public class ClsDefaultValue : Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private const	string		MNT_MESSAGE_LIST			=	"MNT_MESSAGE_LIST";
        
        #region Private Instance Variables
            private	bool boolTransactionLog;
            private int _VALUE_ID;
        #endregion
        
        #region Public Properties
        public int 	DEFV_ID
        {
            get
            {
                return _VALUE_ID;
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
        
        
        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsDefaultValue()
        {
            boolTransactionLog	= base.TransactionLogRequired;
        }
        #endregion

		#region "Get Xml Methods"
		
		/// <summary>
		/// This function is used to generate XML
		/// </summary>
		/// <param name="strDefaultValueId"></param>
		/// <returns></returns>
		public static string GetXmlForPageControls(string strDefaultValueId)
		{
			string strXml="";
			string strSql = "Select DEFV_ID,DEFV_ENTITY_NAME,DEFV_VALUE,IS_ACTIVE From MNT_DEFAULT_VALUE_LIST" ;
			strSql += " where DEFV_ID="+strDefaultValueId + " ";
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
        public int Add(ClsDefaultValueInfo objDefModelInfo)
        {
            string		strStoredProc	=	"Proc_InsertDefaultValue";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);


            try
            {
                objDefModelInfo.IS_ACTIVE                 =   "Y"; 
                objDefModelInfo.CREATED_DATETIME          =   RecordDate;
                objDefModelInfo.LAST_UPDATED_DATETIME     =   RecordDate;
              
                objDataWrapper.AddParameter("@DEFV_ENTITY_NAME",objDefModelInfo.DEFV_ENTITY_NAME);
                objDataWrapper.AddParameter("@DEFV_VALUE",objDefModelInfo.DEFV_VALUE);
                objDataWrapper.AddParameter("@IS_ACTIVE",objDefModelInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY",objDefModelInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME",objDefModelInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY",objDefModelInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDefModelInfo.LAST_UPDATED_DATETIME);

                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DEFV_ID",DEFV_ID,SqlDbType.Int,ParameterDirection.Output);
                int returnResult              = 0;
                returnResult				  =	objDataWrapper.ExecuteNonQuery(strStoredProc);
                _VALUE_ID                       = int.Parse(objSqlParameter.Value.ToString());

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                
                if(returnResult>0)
                    return _VALUE_ID ;
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
        public int Update(ClsDefaultValueInfo oldModelInfo, ClsDefaultValueInfo newModelInfo)
        {   
            int returnResult                            = -1; //using this variable to store return value of database operation 
            SqlUpdateBuilder objBuilder                 = new SqlUpdateBuilder();
            objBuilder.WhereClause                      = " where defv_id=" + newModelInfo.DEFV_ID;

            string strTranXML                           = "";  //using this variable to pass as an out paramater to GETUPDATESQL method 
            string strUpdate                            = ""; // using this variable for creating query and receiving query from GETUPDTAESQL method 

            if(this.TransactionLog)
            {
                newModelInfo.TransactLabel              = ClsCommon.MapTransactionLabel("cmsweb/maintenance/AddDefaultValue.aspx.resx"); 
            }
	
            // calling GetUpdateSQL method to compare and build query out of new and old model object      
            strUpdate                                   += objBuilder.GetUpdateSQL(oldModelInfo,newModelInfo,out strTranXML);

            if(base.TransactionLogRequired)
            {
                DataWrapper objDataWrapper			    =	new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
                try
                {
                    returnResult					    =	objDataWrapper.ExecuteNonQuery(strUpdate);
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
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
            return returnResult; 
        }
        #endregion        
	}
}
