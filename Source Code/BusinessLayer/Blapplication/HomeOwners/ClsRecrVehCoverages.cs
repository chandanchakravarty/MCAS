using System;

namespace blapplication.HomeOwners
{
	/// <summary>
	/// Summary description for ClsRecrVehCoverages.
	/// </summary>
	public class ClsRecrVehCoverages
	{
		public ClsRecrVehCoverages()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/*
		public DataSet GetCoverages(int customerID,int appID, int appVersionID,int currentPageIndex)
		{
			string strSQL = "SELECT * FROM " + 
							"(SELECT TOP 5 * FROM " + 
							"(SELECT TOP 5*" + currentPageIndex.ToString() + " * FROM " + 
							"(SELECT * FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES " + 
							"WHERE CUSTOMER_ID = " + customerID.ToString()  + " AND " + 
								" APP_ID = " + appID.ToString()  + " AND " + 
								" APP_VERSION_ID = " + appVersionID.ToString()  +
								") " + 
							"AS t0 " + 
							"ORDER BY COVERAGE_UNIQUE_ID ASC) AS t1" + 
							"ORDER BY COVERAGE_UNIQUE_ID DESC) AS t2 " + 
							"ORDER BY COVERAGE_UNIQUE_ID";


		}
		*/

	}
}
