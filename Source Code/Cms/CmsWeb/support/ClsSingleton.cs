/******************************************************************************************
	<Author					: - > Shrikant Bhatt
	<Start Date				: -	> 11/03/2004
	<End Date				: - > 
	<Description			: - > This is a Singleton Class that means we can create only single 
								  instance of the class. This class is used for populating data
								  into comboboxes / listboxes 
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: March 18, 2005- > 
	<Modified By			: - Gaurav Tyagi> 
	<Purpose				: - Add new function for Mobile error message format> 
 
 *******************************************************************************************
 
    <Modified Date			: - > April 12, 2010
    <Modified By			: - > Charles Gomes 
	<Purpose				: - > Added Multilingual Support


    <Modified Date			: - > Sep 20, 2011
    <Modified By			: - > Pravesh K Chandel
	<Purpose				: - > Support Multi carrier Singlton Object

*******************************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using System.Collections;
namespace Cms.CmsWeb
{
	/// <summary>
	/// Summary description for Singleton.
	/// </summary>
	public sealed class ClsSingleton : System.Data.DataSet 
	{
		static  ClsSingleton lObjSingletonDataSet = new ClsSingleton();
		private static readonly object padlock = new object();
        //Added by Charles on 12-Apr-10 for Multilingual Support
        public static string strLangSelectClause = "LANG_ID = 1";
        public static string strCarrierCode = "ALBA";
        public static Hashtable CarrierSingltonObject; 
        private static DataRow[] foundRows = null;
        //Added till here

		// Constructor
		static ClsSingleton()
		{
            CarrierSingltonObject = new Hashtable();
            loadData();			
		}

        private static ClsSingleton getSinglotonObject()
        {
                loadData();
                lObjSingletonDataSet = (ClsSingleton)CarrierSingltonObject[strCarrierCode];
                return lObjSingletonDataSet;
        }
		//Setting the Country data 
		public static DataTable Country 
		{
			get
			{
                lObjSingletonDataSet = getSinglotonObject();
                return lObjSingletonDataSet.Tables[0];
			}
		}
		//Setting the State data 
		public static DataTable State 
		{
			get
			{
                lObjSingletonDataSet = getSinglotonObject();
                return lObjSingletonDataSet.Tables[1];
			}
		}
		//Setting the Active State data 
		public static DataTable ActiveState 
		{
			get
			{
				//	DataTable	activeStates = lObjSingletonDataSet.Tables[1].Clone();
				//	DataRow[]	selectedStates;
				string strProcGetState =   "ProcGetStateId";
				DataSet actives;
				actives=Cms.DataLayer.DataWrapper.ExecuteDataset(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,strProcGetState);
				return actives.Tables[0];
			}
                  
					//	selectedStates			=	lObjSingletonDataSet.Tables[1].Select("IS_ACTIVE = 'Y'");
					//	try
					//	{
					//		foreach(DataRow drState in selectedStates)
					//		{
						
					//		activeStates.ImportRow(drState);
					//		}
					//	}	
		}
		//Setting the process names data 
		public static DataTable PolicyProcess 
		{
			get
			{
				string strProcGetState =   "Proc_FillPolicyProcess";
				DataSet process;
				process=Cms.DataLayer.DataWrapper.ExecuteDataset(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,strProcGetState);
                //Changed by Charles on 12-Apr-10 for Multilingual Support
                foundRows = process.Tables[0].Select(strLangSelectClause);
                return foundRows.CopyToDataTable<DataRow>();
			}                 
		
		}

        //Setting the ASLOB Code // Modified By Agniswar on 23 Sep 2011
        public static DataTable ASLOB
        {
            get
            {
                string strProcGetState = "Proc_GetASLOB";
                int iLangID = 1;
                Object[] objParam = new object[1];

                if (strLangSelectClause == "LANG_ID = 1")
                    iLangID = 1;
                else if (strLangSelectClause == "LANG_ID = 2")
                    iLangID = 2;
                else if (strLangSelectClause == "LANG_ID = 3")
                    iLangID = 3;

                objParam[0] = iLangID;
                DataSet aslob;
                aslob = Cms.DataLayer.DataWrapper.ExecuteDataset(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, strProcGetState, objParam);
                //Changed by Charles on 12-Apr-10 for Multilingual Support
                //foundRows = aslob.Tables[0].Select(strLangSelectClause);
                return aslob.Tables[0];
            }


        }

		//Setting the Todolist type data
		public static DataTable TodolistType
		{
			get
            {
                lObjSingletonDataSet = getSinglotonObject();
                //Changed by Charles on 12-Apr-10 for Multilingual Support
                foundRows = lObjSingletonDataSet.Tables[2].Select(strLangSelectClause);                
                return foundRows.CopyToDataTable<DataRow>();
			}
		}
		//Setting the Todolist type data
		public static DataTable TransactionListType
		{
			get
			{
                lObjSingletonDataSet = getSinglotonObject();
                //Changed by Charles on 12-Apr-10 for Multilingual Support
                foundRows = lObjSingletonDataSet.Tables[3].Select(strLangSelectClause);
                return foundRows.CopyToDataTable<DataRow>();
			}
		}

		//Setting the Policy Term Months data
		public static DataTable PolicyTermMonths
		{
			get
			{
                lObjSingletonDataSet = getSinglotonObject();
                //Changed by Charles on 12-Apr-10 for Multilingual Support
                foundRows = lObjSingletonDataSet.Tables[4].Select(strLangSelectClause);
                return foundRows.CopyToDataTable<DataRow>(); 
			}
		}
		//Setting the LOB list data
		public static DataTable LOBs
		{
			get
			{
                //Changed by Charles on 12-Apr-10 for Multilingual Support
                lObjSingletonDataSet = getSinglotonObject();
                foundRows = lObjSingletonDataSet.Tables[5].Select(strLangSelectClause);
                return foundRows.CopyToDataTable<DataRow>(); 
			}
		}

        


		//Setting the ReinsuranceContractType list data
		public static DataTable ReinsuranceContractType
		{
			get
			{
                lObjSingletonDataSet = getSinglotonObject();
                //Changed by Charles on 12-Apr-10 for Multilingual Support
                foundRows = lObjSingletonDataSet.Tables[6].Select(strLangSelectClause);
                return foundRows.CopyToDataTable<DataRow>(); 
			}
		}

		
		//Setting the SublineCode list data
		public static DataTable SublineCode 
		{
			get
			{
                lObjSingletonDataSet = getSinglotonObject();
                //Changed by Charles on 12-Apr-10 for Multilingual Support
                foundRows = lObjSingletonDataSet.Tables[7].Select(strLangSelectClause);
                return foundRows.CopyToDataTable<DataRow>(); 
			}
		}
		//Setting the All Country data 
		public static DataTable AllCountry 
		{
			get
			{

                lObjSingletonDataSet = getSinglotonObject();
                return lObjSingletonDataSet.Tables[8];
			}
		}

        //Setting the SUSEP_LOB
        //Added by Praveen Kumar on 29/04/2010
        public static DataTable SUSEP_LOB
        {
            get
            {
                lObjSingletonDataSet = getSinglotonObject();
                return lObjSingletonDataSet.Tables[9];
            }
        }
        public static DataTable Currency
        {
            get
            {
                lObjSingletonDataSet = getSinglotonObject();
                return lObjSingletonDataSet.Tables[10];
            }
        }
       
        //Added by Ruchika on 14-Jan-2012 for Singapore Implementation
        public static DataTable Fund_Types
        {
            get
            {
                string strProcGetFundTypes = "Proc_Get_ALL_FUND_TYPES";               
                DataSet FndTyp;                
                FndTyp = Cms.DataLayer.DataWrapper.ExecuteDataset(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr,strProcGetFundTypes);                
                return FndTyp.Tables[0];
            }
        }

       
		//Function for Filling the data into the dataset
		private static void loadData()
		{
			lock(padlock)
			{
                if (!CarrierSingltonObject.ContainsKey((object)strCarrierCode))
                {
                    ClsCommon lObjCommon = new ClsCommon();
                    lObjSingletonDataSet = null;
                    lObjSingletonDataSet = new ClsSingleton();
                    lObjCommon.GetCommonData().Fill(lObjSingletonDataSet);
                    CarrierSingltonObject.Add(strCarrierCode, lObjSingletonDataSet);
                }
			}
		}
		//Function for Reset the object
		public static void ResetObject()
		{
            CarrierSingltonObject.Clear();
            loadData();
		}
	}
}