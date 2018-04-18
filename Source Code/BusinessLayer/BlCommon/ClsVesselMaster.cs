/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	8/3/2005 2:15:40 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/
using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Security;

namespace Cms.BusinessLayer.BlCommon
{
    public class ClsVesselMaster : Cms.BusinessLayer.BlCommon.ClsCommon
    {

        private const string MNT_VESSEL_MASTER = "MNT_VESSEL_MASTER";

        #region Private Instance Variables
        private bool boolTransactionLog;
        private const string ACTIVATE_DEACTIVATE_PROC = "Proc_ActivateDeactivateVesselMasterInfo";
        #endregion

        #region Public Properties
        public bool TransactionLog
        {
            set
            {
                boolTransactionLog = value;
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
        public ClsVesselMaster()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			//base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="ObjVesselMasterInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsVesselMasterInfo ObjVesselMasterInfo, int CreatedBy)
        {
            string strStoredProc = "Proc_InsertVesselMasterValue";
            DateTime RecordDate = DateTime.Now;
            TransactionLogRequired = true;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@VESSEL_NAME", ObjVesselMasterInfo.VESSEL_NAME);
                objDataWrapper.AddParameter("@GRT", ObjVesselMasterInfo.GRT);
                objDataWrapper.AddParameter("@IMO", ObjVesselMasterInfo.IMO);
                objDataWrapper.AddParameter("@NRT", ObjVesselMasterInfo.NRT);
                objDataWrapper.AddParameter("@FLAG", ObjVesselMasterInfo.FLAG);
                objDataWrapper.AddParameter("@CLASSIFICATION", ObjVesselMasterInfo.CLASSIFICATION);
                objDataWrapper.AddParameter("@YEAR_BUILT", ObjVesselMasterInfo.YEAR_BUILT);
                objDataWrapper.AddParameter("@LINER", ObjVesselMasterInfo.LINER);
                objDataWrapper.AddParameter("@TYPE_OF_VESSEL", ObjVesselMasterInfo.TYPE_OF_VESSEL);
                objDataWrapper.AddParameter("@CLASS_TYPE", ObjVesselMasterInfo.CLASS_TYPE);
                objDataWrapper.AddParameter("@CLASS", ObjVesselMasterInfo.CLASS);
                objDataWrapper.AddParameter("@IS_ACTIVE", ObjVesselMasterInfo.IS_ACTIVE);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@VESSEL_ID", ObjVesselMasterInfo.VESSEL_ID, SqlDbType.Int, ParameterDirection.Output);
                int returnResult = 0;

                if (TransactionLogRequired)
                {
                    string strTranXML = "";
                    ObjVesselMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddVesselMaster.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    if (ObjVesselMasterInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(ObjVesselMasterInfo);
                    }
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = CreatedBy;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int Vessel_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (Vessel_ID == -1)
                {
                    return -1;
                }
                else
                {
                    ObjVesselMasterInfo.VESSEL_ID = Vessel_ID;
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
        #endregion

        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="ObjOldVesselMasterInfo">Model object having old information</param>
        /// <param name="ObjVesselMasterInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsVesselMasterInfo ObjOldVesselMasterInfo, ClsVesselMasterInfo ObjVesselMasterInfo)
        {
            string strTranXML;
            int returnResult = 0;
            TransactionLogRequired = true;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_UpdateVesselMasterValue";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@VESSEL_ID", ObjVesselMasterInfo.VESSEL_ID);
                objDataWrapper.AddParameter("@VESSEL_NAME", ObjVesselMasterInfo.VESSEL_NAME);
                objDataWrapper.AddParameter("@GRT", ObjVesselMasterInfo.GRT);
                objDataWrapper.AddParameter("@IMO", ObjVesselMasterInfo.IMO);
                objDataWrapper.AddParameter("@NRT", ObjVesselMasterInfo.NRT);
                objDataWrapper.AddParameter("@FLAG", ObjVesselMasterInfo.FLAG);
                objDataWrapper.AddParameter("@CLASSIFICATION", ObjVesselMasterInfo.CLASSIFICATION);
                objDataWrapper.AddParameter("@YEAR_BUILT", ObjVesselMasterInfo.YEAR_BUILT);
                objDataWrapper.AddParameter("@LINER", ObjVesselMasterInfo.LINER);
                objDataWrapper.AddParameter("@TYPE_OF_VESSEL", ObjVesselMasterInfo.TYPE_OF_VESSEL);
                objDataWrapper.AddParameter("@CLASS_TYPE", ObjVesselMasterInfo.CLASS_TYPE);
                objDataWrapper.AddParameter("@CLASS", ObjVesselMasterInfo.CLASS);
                objDataWrapper.AddParameter("@IS_ACTIVE", ObjVesselMasterInfo.IS_ACTIVE);

                if (TransactionLogRequired)
                {
                    ObjVesselMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddVesselMaster.aspx.resx");
                    string strUpdate = objBuilder.GetUpdateSQL(ObjOldVesselMasterInfo, ObjVesselMasterInfo, out strTranXML);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = ObjVesselMasterInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = "Information Has Been Updated";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }
        #endregion

        public static DataTable GetLookupTableDataForVessel(string CalledFrom)
        {
            string strProcedure = "Proc_GetLookUpTableDataForVessel";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@CalledFrom", CalledFrom);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static string GetLookUpDetailXml(int VesselID)
        {
            string strProcedure = "Proc_GetVesselMasterData";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@VESSEL_ID", VesselID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);

                if (objDataSet.Tables[0].Rows.Count != 0)
                {
                    return objDataSet.GetXml();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public static DataTable GetVesselMasterDataForVessel(int VesselID)
        {
            string strProcedure = "Proc_GetVesselMasterData";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@VESSEL_ID", VesselID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public virtual string ActivateDeactivate(string strCode, string isActive)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@VESSEL_ID", strCode);
                objDataWrapper.AddParameter("@IS_ACTIVE", isActive);
                SqlParameter objPaam = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", System.Data.DbType.Int16, System.Data.ParameterDirection.ReturnValue);

                //objDataWrapper.ExecuteNonQuery(strActivateDeactivateProcedure,objTranasction);
                objDataWrapper.ExecuteNonQuery(ACTIVATE_DEACTIVATE_PROC);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (objPaam != null)
                {
                    return (objPaam.Value.ToString());
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
    }
}
