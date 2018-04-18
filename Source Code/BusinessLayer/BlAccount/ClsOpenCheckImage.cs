using System;
using Cms.DataLayer;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon;
using System.Collections; 
using System.Text; 

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Summary description for ClsAccount.
	/// </summary>
	public class ClsOpenCheckImage : Cms.BusinessLayer.BlCommon.ClsCommon
	{
			 
		
		public static DataSet OpenCheck (string RTL_BATCH_NUMBER,string RTL_GROUP_NUMBER)
		{
			

			string RTLConStr = System.Configuration.ConfigurationManager.AppSettings.Get("RTL_CON_STRING");
            string RTLBasePath = System.Configuration.ConfigurationManager.AppSettings.Get("RTL_BASE_PATH");

			StringBuilder sbQuery = new StringBuilder(); 
			sbQuery.Append("SELECT BatchMain.Batch_Number, BatchMain.BatchScanType, BatchRead.Group_Number, '");

			sbQuery.Append(RTLBasePath);
			sbQuery.Append("' + BatchMain.Image_Folder + '\\' + BatchRead.ImageFile AS CheckFilePath_Front,'");
			sbQuery.Append(RTLBasePath);
			sbQuery.Append("' + BatchMain.Image_Folder + '\\' + BatchRead.BackImageFile AS CheckFilePath_Back,'");
			sbQuery.Append(RTLBasePath);
			sbQuery.Append("' + StubMain.Image_Folder + '\\' + StubMain.ImageFile AS StubFilePath_Front,'");
			sbQuery.Append(RTLBasePath);
			sbQuery.Append("' + StubMain.Image_Folder + '\\' + StubMain.BackImageFile AS StubFilePath_Back ");
			sbQuery.Append(" FROM BatchMain INNER JOIN BatchRead ON BatchMain.Image_Folder = BatchRead.Image_Folder LEFT OUTER JOIN StubMain ");
			sbQuery.Append(" ON BatchRead.Image_Folder = StubMain.Image_Folder  AND BatchRead.Group_Number = StubMain.Group_Number ");
			sbQuery.Append("WHERE  BatchMain.Batch_Number = '");
			sbQuery.Append(RTL_BATCH_NUMBER);
			sbQuery.Append("' AND    BatchRead.Group_Number = '");
			sbQuery.Append(RTL_GROUP_NUMBER);
			sbQuery.Append("'");


		
			DataWrapper objDataWrapper = new DataWrapper(RTLConStr,CommandType.Text,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(sbQuery.ToString());
			objDataWrapper.Dispose();
			return objDataSet;		
        
			
		}
		
		
	}
	
}		
		
				
		

	

