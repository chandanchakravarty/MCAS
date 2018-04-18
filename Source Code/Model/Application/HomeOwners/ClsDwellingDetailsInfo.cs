/******************************************************************************************
<Author				: - Pradeep Iyer
<Start Date			: -	5/11/2005 5:40:31 PM
<End Date			: -	
<Description		: - Model class for APP_DWELLING_INFO table
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - Mohit
<Modified By		: - 23/09/2005
<Purpose			: - Code for fields(IS_VACENT_OCCUPY,IS_RENTED_IN_PART,IS_DWELLING_OWNED_BY_OTHER,COMMENT_DWELLING_OWNED) 
<					: -	are commented as droped from the table. 
*******************************************************************************************/ 

using System;
using System.Collections;
using System.Data;
using Cms.Model;

namespace Cms.Model.Application.HomeOwners
{
	/// <summary>
	/// Summary description for ClsDwellingDetailsInfo.
	/// </summary>
	public class ClsDwellingDetailsInfo : Cms.Model.ClsCommonModel
	{

		private const string APP_DWELLINGS_INFO = "APP_DWELLINGS_INFO";
		private ArrayList alAdditionalInterest;
		//private ClsHomeConstructionInfo objHomeConstruction;
		private ClsHomeRatingInfo objHomeRatingInfo;
		private ClsProtectDevicesInfo objProtectInfo;
		private ClsDwellingCoverageLimitInfo objCovg;
		private ArrayList alCoverages;

		public ClsDwellingDetailsInfo()
		{
			base.dtModel.TableName = "APP_DWELLINGS_INFO";		// setting table name for data table that holds property values.
			this.AddColumns();								// add columns of the database table APP_DWELLINGS_INFO
			base.dtModel.Rows.Add(base.dtModel.NewRow());	// add a blank row in the datatable
		}
		
		public ClsDwellingCoverageLimitInfo GetCoverageLimit()
		{
			return objCovg;
		}

		public void SetCoverageLimit(ClsDwellingCoverageLimitInfo objInfo)
		{
			objCovg = objInfo;
		}

		public void SetProtectDevices(ClsProtectDevicesInfo objInfo)
		{
			this.objProtectInfo = objInfo;
		}

		public ClsProtectDevicesInfo GetProtectDevices()
		{
			return objProtectInfo;
		}

		public ClsHomeRatingInfo GetHomeRating()
		{
			return objHomeRatingInfo;
		}

		public void SetHomeRating(ClsHomeRatingInfo objInfo)
		{
			objHomeRatingInfo = objInfo;
		}
//
//		public ClsHomeConstructionInfo GetHomeConstruction()
//		{
//			return objHomeConstruction;
//		}
		
//		public void SetHomeConstruction(ClsHomeConstructionInfo objInfo)
//		{
//			this.objHomeConstruction = objInfo;
//		}

		public ArrayList GetAdditionalInterest()
		{
			return alAdditionalInterest;
		}
		
		public void SetAdditionalInterest(ArrayList alAddInt)
		{
			alAdditionalInterest = alAddInt;
		}
		
		public ArrayList GetCoverages()
		{
			return this.alCoverages;
		}
		
		public void SetCoverages(ArrayList alCov)
		{
			this.alCoverages = alCov;
		}

		private void AddColumns()
		{
			base.dtModel.Columns.Add("CUSTOMER_ID",typeof(int));
			base.dtModel.Columns.Add("APP_ID",typeof(int));
			base.dtModel.Columns.Add("APP_VERSION_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_ID",typeof(int));
			base.dtModel.Columns.Add("DWELLING_NUMBER",typeof(int));
			base.dtModel.Columns.Add("LOCATION_ID",typeof(int));
			base.dtModel.Columns.Add("SUB_LOC_ID",typeof(int));
			base.dtModel.Columns.Add("YEAR_BUILT",typeof(int));
			base.dtModel.Columns.Add("PURCHASE_YEAR",typeof(int));
			base.dtModel.Columns.Add("PURCHASE_PRICE",typeof(double));
			base.dtModel.Columns.Add("MARKET_VALUE",typeof(double));
			base.dtModel.Columns.Add("REPLACEMENT_COST",typeof(double));
			base.dtModel.Columns.Add("REPLACEMENTCOST_COVA",typeof(string));
			base.dtModel.Columns.Add("BUILDING_TYPE",typeof(int));
			base.dtModel.Columns.Add("OCCUPANCY",typeof(int));
			base.dtModel.Columns.Add("OCCUPANCY_CODE",typeof(string));
			base.dtModel.Columns.Add("NEED_OF_UNITS",typeof(int));
			base.dtModel.Columns.Add("USAGE",typeof(string));
			base.dtModel.Columns.Add("NEIGHBOURS_VISIBLE",typeof(string));
			//base.dtModel.Columns.Add("IS_VACENT_OCCUPY",typeof(string));
			//base.dtModel.Columns.Add("IS_RENTED_IN_PART",typeof(string));
			base.dtModel.Columns.Add("OCCUPIED_DAILY",typeof(string));
			base.dtModel.Columns.Add("NO_WEEKS_RENTED",typeof(int));
			//base.dtModel.Columns.Add("IS_DWELLING_OWNED_BY_OTHER",typeof(string));
			base.dtModel.Columns.Add("LOCATION_REF",typeof(string));
			base.dtModel.Columns.Add("SUB_LOCATION_REF",typeof(string));
		
			//RPSINGH - 12 June 2006 
			base.dtModel.Columns.Add("DETACHED_OTHER_STRUCTURES",typeof(string));
			
			base.dtModel.Columns.Add("MONTHS_RENTED",typeof(double));
			//RPSINGH - END - 12 June 2006
			base.dtModel.Columns.Add("LOC_NUM",typeof(string));
		}


		#region Database schema details
		// model for database field CUSTOMER_ID(int)
//		public string COMMENT_DWELLING_OWNED
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["COMMENTDWELLINGOWNED"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["COMMENTDWELLINGOWNED"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["COMMENTDWELLINGOWNED"] = value;
//			}
//		}

		

		// model for database field CUSTOMER_ID(int)
		public int CUSTOMER_ID
		{
			get
			{
				return base.dtModel.Rows[0]["CUSTOMER_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["CUSTOMER_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["CUSTOMER_ID"] = value;
			}
		}
		// model for database field APP_ID(int)
		public int APP_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["APP_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_ID"] = value;
			}
		}
		// model for database field APP_VERSION_ID(int)
		public int APP_VERSION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["APP_VERSION_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["APP_VERSION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["APP_VERSION_ID"] = value;
			}
		}
		// model for database field DWELLING_ID(int)
		public int DWELLING_ID
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["DWELLING_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_ID"] = value;
			}
		}
		// model for database field DWELLING_NUMBER(int)
		public int DWELLING_NUMBER
		{
			get
			{
				return base.dtModel.Rows[0]["DWELLING_NUMBER"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["DWELLING_NUMBER"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["DWELLING_NUMBER"] = value;
			}
		}
		// model for database field LOCATION_ID(int)
		public int LOCATION_ID
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["LOCATION_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_ID"] = value;
			}
		}
		// model for database field SUB_LOC_ID(int)
		public int SUB_LOC_ID
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOC_ID"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["SUB_LOC_ID"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOC_ID"] = value;
			}
		}
		// model for database field YEAR_BUILT(int)
		public int YEAR_BUILT
		{
			get
			{
				return base.dtModel.Rows[0]["YEAR_BUILT"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["YEAR_BUILT"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["YEAR_BUILT"] = value;
			}
		}
		// model for database field PURCHASE_YEAR(int)
		public int PURCHASE_YEAR
		{
			get
			{
				return base.dtModel.Rows[0]["PURCHASE_YEAR"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["PURCHASE_YEAR"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PURCHASE_YEAR"] = value;
			}
		}
		// model for database field PURCHASE_PRICE(double)
		public double PURCHASE_PRICE
		{
			get
			{
				return base.dtModel.Rows[0]["PURCHASE_PRICE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["PURCHASE_PRICE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["PURCHASE_PRICE"] = value;
			}
		}
		// model for database field MARKET_VALUE(double)
		public double MARKET_VALUE
		{
			get
			{
				return base.dtModel.Rows[0]["MARKET_VALUE"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["MARKET_VALUE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["MARKET_VALUE"] = value;
			}
		}
		// model for database field REPLACEMENT_COST(double)
		public double REPLACEMENT_COST
		{
			get
			{
				return base.dtModel.Rows[0]["REPLACEMENT_COST"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["REPLACEMENT_COST"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["REPLACEMENT_COST"] = value;
			}
		}
		// model for database field REPLACEMENTCOST(String)
		public string REPLACEMENTCOST_COVA
		{
			get
			{
				return base.dtModel.Rows[0]["REPLACEMENTCOST_COVA"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["REPLACEMENTCOST_COVA"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["REPLACEMENTCOST_COVA"] = value;
			}
		}

		// model for database field BUILDING_TYPE(int)
		public int BUILDING_TYPE
		{
			get
			{
				return base.dtModel.Rows[0]["BUILDING_TYPE"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["BUILDING_TYPE"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["BUILDING_TYPE"] = value;
			}
		}
		// model for database field OCCUPANCY(int)
		public int OCCUPANCY
		{
			get
			{
				return base.dtModel.Rows[0]["OCCUPANCY"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["OCCUPANCY"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["OCCUPANCY"] = value;
			}
		}

		public string OCCUPANCY_CODE
		{
			get
			{
				return base.dtModel.Rows[0]["OCCUPANCY_CODE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OCCUPANCY_CODE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OCCUPANCY_CODE"] = value;
			}
		}

		// model for database field NEED_OF_UNITS(int)
		public int NEED_OF_UNITS
		{
			get
			{
				return base.dtModel.Rows[0]["NEED_OF_UNITS"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NEED_OF_UNITS"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NEED_OF_UNITS"] = value;
			}
		}
		// model for database field USAGE(string)
		public string USAGE
		{
			get
			{
				return base.dtModel.Rows[0]["USAGE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["USAGE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["USAGE"] = value;
			}
		}
		// model for database field NEIGHBOURS_VISIBLE(string)
		public string NEIGHBOURS_VISIBLE
		{
			get
			{
				return base.dtModel.Rows[0]["NEIGHBOURS_VISIBLE"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["NEIGHBOURS_VISIBLE"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["NEIGHBOURS_VISIBLE"] = value;
			}
		}
		// model for database field IS_VACENT_OCCUPY(string)
//		public string IS_VACENT_OCCUPY
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["IS_VACENT_OCCUPY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_VACENT_OCCUPY"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["IS_VACENT_OCCUPY"] = value;
//			}
//		}
		// model for database field IS_RENTED_IN_PART(string)
//		public string IS_RENTED_IN_PART
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["IS_RENTED_IN_PART"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_RENTED_IN_PART"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["IS_RENTED_IN_PART"] = value;
//			}
//		}
		// model for database field OCCUPIED_DAILY(string)
		public string OCCUPIED_DAILY
		{
			get
			{
				return base.dtModel.Rows[0]["OCCUPIED_DAILY"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["OCCUPIED_DAILY"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["OCCUPIED_DAILY"] = value;
			}
		}
		// model for database field NO_WEEKS_RENTED(int)
		public int NO_WEEKS_RENTED
		{
			get
			{
				return base.dtModel.Rows[0]["NO_WEEKS_RENTED"] == DBNull.Value ? -1 : int.Parse(base.dtModel.Rows[0]["NO_WEEKS_RENTED"].ToString());
			}
			set
			{
				base.dtModel.Rows[0]["NO_WEEKS_RENTED"] = value;
			}
		}
		// model for database field IS_DWELLING_OWNED_BY_OTHER(int)
//		public string IS_DWELLING_OWNED_BY_OTHER
//		{
//			get
//			{
//				return base.dtModel.Rows[0]["IS_DWELLING_OWNED_BY_OTHER"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["IS_DWELLING_OWNED_BY_OTHER"].ToString();
//			}
//			set
//			{
//				base.dtModel.Rows[0]["IS_DWELLING_OWNED_BY_OTHER"] = value;
//			}
//		}

		// model for database field IS_DWELLING_OWNED_BY_OTHER(int)
		public string LOCATION_REF
		{
			get
			{
				return base.dtModel.Rows[0]["LOCATION_REF"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOCATION_REF"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOCATION_REF"] = value;
			}
		}
		
		public string SUB_LOCATION_REF
		{
			get
			{
				return base.dtModel.Rows[0]["SUB_LOCATION_REF"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["SUB_LOCATION_REF"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["SUB_LOCATION_REF"] = value;
			}
		}

		

		// model for database field MONTHS_RENTED(double)
		public double MONTHS_RENTED
		{
			get{return base.dtModel.Rows[0]["MONTHS_RENTED"] == DBNull.Value ? -1 : double.Parse(base.dtModel.Rows[0]["MONTHS_RENTED"].ToString());}
			set{base.dtModel.Rows[0]["MONTHS_RENTED"] = value;}
		}

		

		public string DETACHED_OTHER_STRUCTURES
		{
			get{return base.dtModel.Rows[0]["DETACHED_OTHER_STRUCTURES"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["DETACHED_OTHER_STRUCTURES"].ToString();}
			set{base.dtModel.Rows[0]["DETACHED_OTHER_STRUCTURES"] = value;}
		}
		public string LOC_NUM
		{
			get
			{
				return base.dtModel.Rows[0]["LOC_NUM"] == DBNull.Value ? Convert.ToString(null) : base.dtModel.Rows[0]["LOC_NUM"].ToString();
			}
			set
			{
				base.dtModel.Rows[0]["LOC_NUM"] = value;
			}
		}

		

		#endregion
	}
	
	
}
