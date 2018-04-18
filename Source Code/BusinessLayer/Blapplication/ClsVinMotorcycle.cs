/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	 24-10-2005
<End Date			: -	 24-10-2005
<Description		: -  Class Contains the services for vin number of motorcycle.
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Application.PriorLoss;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;  

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsVinMaster.
	/// </summary>
	public class ClsVinMotorcycle : Cms.BusinessLayer.BlApplication.clsapplication   
	{
		//private const	string		APP_EXPIRY_DATES			=	"APP_EXPIRY_DATES";
		public ClsVinMotorcycle()
		{
		}

		public int AddVINMotorCycleMaster(int vinID,string manufacturer,string model,string modelYear,int modelCC)
		{
			string		strStoredProc	=	"PROC_INSERT_MOTORCYCLE_VIN_MASTER";
   
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);

			try
			{
				objDataWrapper.AddParameter("@ID",vinID);
				objDataWrapper.AddParameter("@MANUFACTURER",manufacturer);
				objDataWrapper.AddParameter("@MODEL",model);
				objDataWrapper.AddParameter("@MODEL_YEAR",modelYear);
				if (modelCC != 0)
				{
					objDataWrapper.AddParameter("@MODEL_CC",modelCC);
				}
				
				TransactionLogRequired=false;

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					/*objExpiryDatesModel.TransactLabel = ClsCommon.MapTransactionLabel("/Application/PriorLoss/AddExpiryDates.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objExpiryDatesModel);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objExpiryDatesModel.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objExpiryDatesModel.APP_VERSION_ID;
					
					objTransactionInfo.CLIENT_ID		=	objExpiryDatesModel.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objExpiryDatesModel.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New Expiration date is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);*/
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
                
				objDataWrapper.ClearParameteres();
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

	}
}
