/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		03-11-2005
<End Date			: -	
<Description		: - 	Policy Vehicle Index page 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
*******************************************************************************************/ 
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;
using System.Resources;
using System.Reflection;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class PolicyVehicleIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Literal litTextGrid;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		private const string CUSTOMER_SECTION ="CLT";
		private const string APP_SECTION ="APP";
        private const string MOTORCYCLE_SECTION ="MOT";
		private const string UMBRELLA_SECTION ="UMB";
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPPID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		
		protected string strCalledFrom = "";
        ResourceManager objResourceMgr = null;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id
			if(Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
			{
				strCalledFrom	=	Request.QueryString["CalledFrom"].ToString().ToUpper();
			}

            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.PolicyVehicleIndex", Assembly.GetExecutingAssembly());

			switch(strCalledFrom.ToUpper())
			{
				case "ppa" :
				case "PPA" :
					base.ScreenId	=	"227";
					break;
				case "mot" :
				case "MOT" :
					base.ScreenId	=	"231";
					break;
				case "UMB":
					base.ScreenId	=	"275";
					break;

			}
			#endregion

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
					break;
			}

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion         

			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				string sFECTHCOLUMNS = "";
				string strCUSTOMER_ID =GetCustomerID();
				hidCustomerID.Value =strCUSTOMER_ID;
				string strAPPID = GetAppID();
				hidAPPID.Value =strAPPID;
				string strAPPVersionID = GetAppVersionID();
				hidAppVersionID.Value =strAPPVersionID;
				string strPolicyID = GetPolicyID();
				string strPolicyVersionID = GetPolicyVersionID();

				hidPolicyID.Value = strPolicyID;
				hidPolicyVersionID.Value = strPolicyVersionID;
 
				string strCALLEDFROM="";
				string sSELECTCLAUSE="", sWHERECLAUSE="", sFROMCLAUSE="",sQUERYSTRING="";

				
				if(Request.QueryString["CALLEDFROM"]!= null)
				{
					strCALLEDFROM = Request["CalledFrom"].ToString();
					hidCalledFrom.Value =Request["CalledFrom"].ToString().ToUpper();
					objWebGrid.ExtraButtons = "4^Add New~Copy~Request Loss Report~Prior Loss Tab^0~1^addRecord~copy~FetchLossReport~PriorLossTab";
					objWebGrid.OrderByClause	="VIN asc";
					if(hidCalledFrom.Value=="PPA")
					{
						//sSELECTCLAUSE="t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,t1.BODY_TYPE,t1.GRG_ADD1,t1.GRG_ADD2,t1.GRG_CITY,t1.GRG_COUNTRY,t1.GRG_STATE,t1.GRG_ZIP,t1.REGISTERED_STATE,t1.TERRITORY,t1.CLASS,t1.REGN_PLATE_NUMBER,t1.ST_AMT_TYPE,t1.AMOUNT,t1.SYMBOL,t1.VEHICLE_AGE,t1.CUSTOMER_ID,t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB,case when t1.APP_USE_VEHICLE_ID='11332' then l1.lookup_value_desc else l4.lookup_value_desc end vehicle_type, case when t1.APP_USE_VEHICLE_ID='11332' then l3.lookup_value_desc else l2.lookup_value_desc end vehicle_class, l5.lookup_value_desc as vehicle_use,t1.IS_ACTIVE";
						sSELECTCLAUSE="t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,t1.BODY_TYPE,t1.GRG_ADD1,t1.GRG_ADD2,t1.GRG_CITY,t1.GRG_COUNTRY,t1.GRG_STATE,t1.GRG_ZIP,t1.REGISTERED_STATE,t1.TERRITORY,t1.CLASS,t1.REGN_PLATE_NUMBER,t1.ST_AMT_TYPE,t1.AMOUNT,t1.SYMBOL,t1.VEHICLE_AGE,t1.CUSTOMER_ID,t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB,case when t1.APP_USE_VEHICLE_ID='11332' then dbo.PADSPACES(l1.lookup_value_desc,25) else dbo.PADSPACES(l4.lookup_value_desc,25) end vehicle_type, case when t1.APP_USE_VEHICLE_ID='11332' then dbo.PADSPACES(l3.lookup_value_desc,25) else dbo.PADSPACES(l2.lookup_value_desc,25) end vehicle_class, l5.lookup_value_desc as vehicle_use,t1.IS_ACTIVE";
						sSELECTCLAUSE += " , CASE WHEN T1.APP_USE_VEHICLE_ID='11332' THEN L1.LOOKUP_VALUE_DESC ELSE L4.LOOKUP_VALUE_DESC END VEHICLE_TYPE_TOOL_TIP, CASE WHEN T1.APP_USE_VEHICLE_ID='11332' THEN L3.LOOKUP_VALUE_DESC ELSE L2.LOOKUP_VALUE_DESC END VEHICLE_CLASS_TOOL_TIP ";
						sFROMCLAUSE  = "POL_VEHICLES t1 LEFT JOIN POL_CUSTOMER_POLICY_LIST PL ON PL.POLICY_ID = t1.POLICY_ID AND PL.POLICY_VERSION_ID = t1.POLICY_VERSION_ID AND PL.CUSTOMER_ID = t1.CUSTOMER_ID left join mnt_lookup_values l1 on t1.APP_VEHICLE_PERTYPE_ID  = l1.lookup_unique_id left join mnt_lookup_values l2 on t1.APP_VEHICLE_COMCLASS_ID  = l2.lookup_unique_id left join mnt_lookup_values l3 on t1.APP_VEHICLE_PERCLASS_ID  = l3.lookup_unique_id left join mnt_lookup_values l4 on t1.APP_VEHICLE_COMTYPE_ID = l4.lookup_unique_id left join mnt_lookup_values l5 on t1.APP_USE_VEHICLE_ID = l5.lookup_unique_id";
						sWHERECLAUSE = " t1.CUSTOMER_ID="+strCUSTOMER_ID+" AND t1.POLICY_ID="+strPolicyID+" AND t1.POLICY_VERSION_ID ="+strPolicyVersionID;	
						sQUERYSTRING = "&CUSTOMER_ID= "+strCUSTOMER_ID +"&POLICY_ID="+strPolicyID+"&POLICY_VERSION_ID="+strPolicyVersionID;
						sFECTHCOLUMNS = "1^3^2^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24^25^26^27";
						objWebGrid.SearchColumnHeadings = "Vehicle #^VIN^Vehicle Use^Class^Type^Year^Make^Model";	
						objWebGrid.SearchColumnNames = "t1.INSURED_VEH_NUMBER^t1.VIN^l5.lookup_value_desc^case when t1.APP_USE_VEHICLE_ID='11332' then l1.lookup_value_desc else l4.lookup_value_desc end^case when t1.APP_USE_VEHICLE_ID='11332' then l3.lookup_value_desc else l2.lookup_value_desc end^t1.VEHICLE_YEAR^t1.MAKE^t1.MODEL";
						objWebGrid.SearchColumnType = "T^T^T^T^T^T^T^T";
						objWebGrid.DisplayColumnHeadings = "Vehicle #^VIN^Vehicle Use^Class^Type^Year^Make^Model";
						objWebGrid.DisplayColumnNumbers = "2^3^4^5^6^7^8^9";
						objWebGrid.DisplayColumnNames = "INSURED_VEH_NUMBER^VIN^Vehicle_Use^Vehicle_Class^Vehicle_Type^VEHICLE_YEAR^MAKE^MODEL";
						objWebGrid.DisplayTextLength = "10^10^10^20^20^10^10^10";
						objWebGrid.DisplayColumnPercent = "10^10^10^20^20^10^10^10";
						objWebGrid.PrimaryColumns = "1";
						objWebGrid.PrimaryColumnsName = "t1.VEHICLE_ID";
						objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";
						objWebGrid.HeaderString ="Vehicle Information" ;
						TabCtl.TabURLs = "PolicyVehicleInformation.aspx?CALLEDFROM=" + strCALLEDFROM +  sQUERYSTRING +"&"; 
						TabCtl.TabTitles ="Vehicle Info";
						TabCtl.TabLength =175;				
						objWebGrid.OrderByClause	="t1.INSURED_VEH_NUMBER asc";
					}
					else if(hidCalledFrom.Value=="MOT")
					{
						//sSELECTCLAUSE="t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,t1.BODY_TYPE,t1.GRG_ADD1,t1.GRG_ADD2,t1.GRG_CITY,t1.GRG_COUNTRY,t1.GRG_STATE,t1.GRG_ZIP,t1.REGISTERED_STATE,t1.TERRITORY,t1.CLASS,t1.REGN_PLATE_NUMBER,t1.ST_AMT_TYPE,t1.AMOUNT,t1.SYMBOL,t1.VEHICLE_AGE,t1.CUSTOMER_ID,t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB";
						//sSELECTCLAUSE="t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,t1.BODY_TYPE,t1.GRG_ADD1,t1.GRG_ADD2,t1.GRG_CITY,t1.GRG_COUNTRY,t1.GRG_STATE,t1.GRG_ZIP,t1.REGISTERED_STATE,t1.TERRITORY,t1.CLASS,t1.REGN_PLATE_NUMBER,t1.ST_AMT_TYPE,t1.AMOUNT,t1.SYMBOL,t1.VEHICLE_AGE,t1.CUSTOMER_ID,t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB,L6.LOOKUP_VALUE_DESC AS MOTORCYCLE_TYPE,T1.IS_ACTIVE";						
                        sSELECTCLAUSE = "t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,";
                        sSELECTCLAUSE += "t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,LV.LOOKUP_VALUE_DESC AS MAKE,";
                        sSELECTCLAUSE += "L6.MODEL AS MODEL,t1.BODY_TYPE,t1.GRG_ADD1,t1.GRG_ADD2,";
                        sSELECTCLAUSE += "t1.GRG_CITY,t1.GRG_COUNTRY,t1.GRG_STATE,t1.GRG_ZIP,";
                        sSELECTCLAUSE += "t1.REGISTERED_STATE,t1.TERRITORY,t1.CLASS,t1.REGN_PLATE_NUMBER,";
                        sSELECTCLAUSE += "t1.ST_AMT_TYPE,t1.AMOUNT,t1.SYMBOL,t1.VEHICLE_AGE,t1.CUSTOMER_ID,";
                        sSELECTCLAUSE += "t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB,";
                        sSELECTCLAUSE += "MT.MODEL_TYPE AS MOTORCYCLE_TYPE,T1.IS_ACTIVE";
						//sFROMCLAUSE  = " POL_VEHICLES t1 LEFT JOIN POL_CUSTOMER_POLICY_LIST PL ON PL.POLICY_ID = t1.POLICY_ID AND PL.POLICY_VERSION_ID = t1.POLICY_VERSION_ID AND PL.CUSTOMER_ID = t1.CUSTOMER_ID";
						//sFROMCLAUSE  = " POL_VEHICLES t1 LEFT JOIN MNT_LOOKUP_VALUES L6 ON T1.MOTORCYCLE_TYPE = L6.LOOKUP_UNIQUE_ID LEFT JOIN POL_CUSTOMER_POLICY_LIST PL ON PL.POLICY_ID = t1.POLICY_ID AND PL.POLICY_VERSION_ID = t1.POLICY_VERSION_ID AND PL.CUSTOMER_ID = t1.CUSTOMER_ID";
                        sFROMCLAUSE = "POL_VEHICLES t1 ";
                        sFROMCLAUSE += "LEFT JOIN MNT_LOOKUP_VALUES LV ";
                        sFROMCLAUSE += "ON LV.LOOKUP_UNIQUE_ID = t1.MAKE ";
                        sFROMCLAUSE += "LEFT JOIN MNT_VEHICLE_MODEL_LIST L6 ";
                        sFROMCLAUSE += "ON T1.MODEL = L6.ID ";
                        sFROMCLAUSE += "LEFT JOIN MNT_VEHICLE_MODEL_TYPE_LIST MT ";
                        sFROMCLAUSE += "ON T1.MOTORCYCLE_TYPE = MT.ID ";
                        sFROMCLAUSE += "LEFT JOIN POL_CUSTOMER_POLICY_LIST PL ";
                        sFROMCLAUSE += "ON PL.POLICY_ID = t1.POLICY_ID ";
                        sFROMCLAUSE += "AND PL.POLICY_VERSION_ID = t1.POLICY_VERSION_ID ";
                        sFROMCLAUSE += "AND PL.CUSTOMER_ID = t1.CUSTOMER_ID ";

						sWHERECLAUSE = " t1.CUSTOMER_ID="+strCUSTOMER_ID+" AND t1.POLICY_ID="+strPolicyID+" AND t1.POLICY_VERSION_ID ="+strPolicyVersionID;	
						sQUERYSTRING = "&CUSTOMER_ID= "+ strCUSTOMER_ID +"&POLICY_ID="+strPolicyID+"&POLICY_VERSION_ID="+strPolicyVersionID;
						sFECTHCOLUMNS = "1^3^2^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24^25";
						//objWebGrid.SearchColumnHeadings = "VIN^Insured Motorcycle Number^Year^Make of Motorcycle^Model of Motorcycle^Motorcycle Type";
                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                        
                        //objWebGrid.DisplayColumnHeadings = "VIN^Insured Motorcycle Number^Year^Make of Motorcycle^Model of Motorcycle^Motorcycle Type";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");
                        
                        objWebGrid.DisplayColumnNumbers = "2^3^4^5^6^7";
						objWebGrid.DisplayColumnNames = "VIN^INSURED_VEH_NUMBER^VEHICLE_YEAR^MAKE^MODEL^MOTORCYCLE_TYPE";
                        objWebGrid.SearchColumnNames = "t1.VIN^t1.INSURED_VEH_NUMBER^t1.VEHICLE_YEAR^MAKE^MODEL^MOTORCYCLE_TYPE";
						objWebGrid.SearchColumnType = "T^T^T^T^T^T";
						objWebGrid.DisplayTextLength = "50^50^50^50^50^50";
						objWebGrid.DisplayColumnPercent = "10^20^10^20^20^20";
						objWebGrid.PrimaryColumns = "1";
						objWebGrid.PrimaryColumnsName = "t1.VEHICLE_ID";
						objWebGrid.ColumnTypes = "B^B^B^B^B^B";
						//objWebGrid.HeaderString ="Motorcycle Information" ;
                        objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                        
                        //objWebGrid.DisplayColumnHeadings = "VIN^Insured Motorcycle Number^Year^Make of Motorcycle^Model of Motorcycle^Model of Motorcycle";
						TabCtl.TabURLs = "/cms/Policies/Aspx/Motorcycle/PolicyMotorCycleInformation.aspx?CALLEDFROM=" + strCALLEDFROM + sQUERYSTRING +"&";
						//TabCtl.TabTitles ="Motorcycle Info";
                        TabCtl.TabTitles = objResourceMgr.GetString("TabTitles");

						TabCtl.TabLength =175;

                        objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
					}
					else if(hidCalledFrom.Value.ToUpper()=="UMB")
					{
						//sSELECTCLAUSE="t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,t1.BODY_TYPE,t1.GRG_ADD1,t1.GRG_ADD2,t1.GRG_CITY,t1.GRG_COUNTRY,t1.GRG_STATE,t1.GRG_ZIP,t1.REGISTERED_STATE,t1.TERRITORY,t1.CLASS,t1.REGN_PLATE_NUMBER,t1.ST_AMT_TYPE,t1.AMOUNT,t1.SYMBOL,t1.VEHICLE_AGE,t1.CUSTOMER_ID,t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB,case when t1.USE_VEHICLE='11332' then l1.lookup_value_desc else l2.lookup_value_desc end vehicle_type, case when t1.USE_VEHICLE='11332' then l3.lookup_value_desc else l4.lookup_value_desc end vehicle_class, l5.lookup_value_desc as vehicle_use,t1.IS_ACTIVE";
						//sSELECTCLAUSE="t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,t1.BODY_TYPE,t1.GRG_ADD1,t1.GRG_ADD2,t1.GRG_CITY,t1.GRG_COUNTRY,t1.GRG_STATE,t1.GRG_ZIP,t1.REGISTERED_STATE,t1.TERRITORY,t1.CLASS,t1.REGN_PLATE_NUMBER,t1.ST_AMT_TYPE,t1.AMOUNT,t1.SYMBOL,t1.VEHICLE_AGE,t1.CUSTOMER_ID,t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB,case when t1.USE_VEHICLE='11332' then l1.lookup_value_desc else l4.lookup_value_desc end vehicle_type, case when t1.USE_VEHICLE='11332' then l3.lookup_value_desc else l2.lookup_value_desc end vehicle_class, l5.lookup_value_desc as vehicle_use,t1.IS_ACTIVE";
						sSELECTCLAUSE="t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,t1.REGN_PLATE_NUMBER,t1.CUSTOMER_ID,t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB, t1.IS_ACTIVE,LV.LOOKUP_VALUE_DESC AS MOTORCYCLE_TYPE_DESC";
						//sSELECTCLAUSE="t1.VEHICLE_ID,t1.VEHICLE_ID as VH_ID,t1.VIN,t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,t1.BODY_TYPE,t1.GRG_ADD1,t1.GRG_ADD2,t1.GRG_CITY,t1.GRG_COUNTRY,t1.GRG_STATE,t1.GRG_ZIP,t1.REGISTERED_STATE,t1.TERRITORY,t1.CLASS,t1.REGN_PLATE_NUMBER,t1.ST_AMT_TYPE,t1.AMOUNT,t1.SYMBOL,t1.VEHICLE_AGE,t1.CUSTOMER_ID,t1.POLICY_ID,t1.POLICY_VERSION_ID,PL.POLICY_LOB";
						//sFROMCLAUSE  = " POL_UMBRELLA_VEHICLE_INFO t1 LEFT JOIN POL_CUSTOMER_POLICY_LIST PL ON PL.POLICY_ID = t1.POLICY_ID AND PL.POLICY_VERSION_ID = t1.POLICY_VERSION_ID AND PL.CUSTOMER_ID = t1.CUSTOMER_ID";						
						//sFROMCLAUSE  = " POL_UMBRELLA_VEHICLE_INFO t1 LEFT JOIN POL_CUSTOMER_POLICY_LIST PL ON PL.POLICY_ID = t1.POLICY_ID AND PL.POLICY_VERSION_ID = t1.POLICY_VERSION_ID AND PL.CUSTOMER_ID = t1.CUSTOMER_ID";												
						
						//sFROMCLAUSE  = "POL_UMBRELLA_VEHICLE_INFO t1 LEFT JOIN POL_CUSTOMER_POLICY_LIST PL ON PL.POLICY_ID = t1.POLICY_ID AND PL.POLICY_VERSION_ID = t1.POLICY_VERSION_ID AND PL.CUSTOMER_ID = t1.CUSTOMER_ID left join mnt_lookup_values l1 on t1.VEHICLE_TYPE_PER  = l1.lookup_unique_id left join mnt_lookup_values l2 on t1.CLASS_COM  = l2.lookup_unique_id left join mnt_lookup_values l3 on t1.CLASS_PER  = l3.lookup_unique_id left join mnt_lookup_values l4 on t1.VEHICLE_TYPE_COM = l4.lookup_unique_id left join mnt_lookup_values l5 on t1.USE_VEHICLE = l5.lookup_unique_id";
						//sFROMCLAUSE  = "POL_UMBRELLA_VEHICLE_INFO t1 LEFT JOIN POL_CUSTOMER_POLICY_LIST PL ON PL.POLICY_ID = t1.POLICY_ID AND PL.POLICY_VERSION_ID = t1.POLICY_VERSION_ID AND PL.CUSTOMER_ID = t1.CUSTOMER_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES LV ON T1.MOTORCYCLE_TYPE = LV.LOOKUP_UNIQUE_?ID";
						sFROMCLAUSE  = "POL_UMBRELLA_VEHICLE_INFO t1 left join mnt_lookup_values l1 on t1.vehicle_type_per  = l1.lookup_unique_id and T1.VEHICLE_TYPE_PER = l1.LOOKUP_UNIQUE_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES LV ON (CASE WHEN t1.MOTORCYCLE_TYPE IS NULL THEN t1.VEHICLE_TYPE_PER ELSE t1.MOTORCYCLE_TYPE END) = LV.LOOKUP_UNIQUE_ID LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PL ON  T1.CUSTOMER_ID = PL.CUSTOMER_ID AND T1.POLICY_ID = PL.POLICY_ID AND T1.POLICY_VERSION_ID = PL.Policy_VERSION_ID";
						sWHERECLAUSE = " t1.CUSTOMER_ID="+strCUSTOMER_ID+" AND t1.POLICY_ID="+strPolicyID+" AND t1.POLICY_VERSION_ID ="+strPolicyVersionID;	
						sQUERYSTRING = "&CUSTOMER_ID= "+ strCUSTOMER_ID +"&POLICY_ID="+strPolicyID+"&POLICY_VERSION_ID="+strPolicyVersionID;
						//sFECTHCOLUMNS = "1^3^2^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24";
						sFECTHCOLUMNS = "1^3^2^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24^25^26^27";

						// Modified by Swastika on 1st Mar'06 for Pol Iss # 40
						//objWebGrid.SearchColumnHeadings = "VIN^Vehicle #^Vehicle Use^Class^Type^Year^Make^Model";	
						objWebGrid.SearchColumnHeadings = "Vehicle #^Type of Vehicle^Year^Make^Model";	
						//objWebGrid.SearchColumnNames = "t1.VIN^t1.INSURED_VEH_NUMBER^l5.lookup_value_desc^case when t1.USE_VEHICLE='11332' then l3.lookup_value_desc else l4.lookup_value_desc end^case when t1.USE_VEHICLE='11332' then l1.lookup_value_desc else l2.lookup_value_desc end^t1.VEHICLE_YEAR^t1.MAKE^t1.MODEL";
						//objWebGrid.SearchColumnNames = "t1.VIN^t1.INSURED_VEH_NUMBER^l5.lookup_value_desc^case when t1.USE_VEHICLE='11332' then l3.lookup_value_desc else l2.lookup_value_desc end^case when t1.USE_VEHICLE='11332' then l1.lookup_value_desc else l4.lookup_value_desc end^t1.VEHICLE_YEAR^t1.MAKE^t1.MODEL";
						objWebGrid.SearchColumnNames = "t1.VEHICLE_ID^LV.LOOKUP_VALUE_DESC^t1.VEHICLE_YEAR^t1.MAKE^t1.MODEL";
						//objWebGrid.SearchColumnType = "T^T^T^T^T^T^T^T";
						objWebGrid.SearchColumnType = "T^T^T^T^T";
						//objWebGrid.DisplayColumnHeadings = "VIN^Vehicle #^Vehicle Use^Class^Type^Year^Make^Model";
						objWebGrid.DisplayColumnHeadings = "Vehicle #^Type of Vehicle^Year^Make^Model";
						//objWebGrid.DisplayColumnNumbers = "2^3^4^5^6^7^8^9";
						objWebGrid.DisplayColumnNumbers = "1^6^7^8^9";
						//objWebGrid.DisplayColumnNames = "VIN^INSURED_VEH_NUMBER^Vehicle_Use^Vehicle_Class^Vehicle_Type^VEHICLE_YEAR^MAKE^MODEL";
						objWebGrid.DisplayColumnNames = "VEHICLE_ID^MOTORCYCLE_TYPE_DESC^VEHICLE_YEAR^MAKE^MODEL";
						
						//objWebGrid.DisplayTextLength = "50^50^50^50^50";
						//objWebGrid.DisplayTextLength = "10^10^20^25^45^10^15^40";
						objWebGrid.DisplayTextLength = "10^10^15^30^10";
						//objWebGrid.DisplayColumnPercent = "20^30^10^20^20";
						//objWebGrid.DisplayColumnPercent = "8^8^10^6^25^8^10^25";
						objWebGrid.DisplayColumnPercent = "10^20^20^25^25";
						objWebGrid.PrimaryColumns = "1";
						objWebGrid.PrimaryColumnsName = "t1.VEHICLE_ID";
						//objWebGrid.ColumnTypes = "B^B^B^B^B";
						//objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";
						objWebGrid.ColumnTypes = "B^B^B^B^B";
						objWebGrid.HeaderString ="Umbrella Vehicles - Other Carriers" ;
						TabCtl.TabURLs = "AddPolUmbVehicleInfo.aspx?CALLEDFROM=" + strCALLEDFROM +  sQUERYSTRING +"&"; 
						TabCtl.TabTitles ="Umbrella Vehicle Info";
						TabCtl.TabLength =175;
						//objWebGrid.ExtraButtons = "3^Add New~Copy~Copy Records^0~1^addRecord~copy~CopySchRecords";
						
						objWebGrid.ExtraButtons = "2^Add New~View Records^0~1^addRecord~CopySchRecords";

					}
				}
				
				
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ; 
				objWebGrid.WhereClause = sWHERECLAUSE;	
				/*if (Request["CalledFrom"] == "PPA")
				{
					objWebGrid.SearchColumnHeadings = "VIN^Insured Vehicle Number^Year^Make of Vehicle^Model of Vehicle";	
				}
				else
				{
					objWebGrid.SearchColumnHeadings = "VIN^Insured Motorcycle Number^Year^Make of Motorcycle^Model of Motorcycle";
				}*/
				
				//objWebGrid.SearchColumnNames = "t1.VIN^t1.INSURED_VEH_NUMBER^t1.VEHICLE_YEAR^t1.MAKE^t1.MODEL";
				//objWebGrid.SearchColumnType = "T^T^T^T^T";				
				//objWebGrid.DisplayColumnNumbers = "2^3^4^5^6";
				//objWebGrid.DisplayColumnNames = "VIN^INSURED_VEH_NUMBER^VEHICLE_YEAR^MAKE^MODEL";

				/*if (Request["CalledFrom"] == "PPA")
				{
					objWebGrid.DisplayColumnHeadings = "VIN^Insured Vehicle Number^Year^Make of Vehicle^Model of Vehicle";
				}
				else
				{
					objWebGrid.DisplayColumnHeadings = "VIN^Insured Motorcycle Number^Year^Make of Motorcycle^Model of Motorcycle";
				}*/
				

				/*objWebGrid.DisplayTextLength = "50^50^50^50^50";
				objWebGrid.DisplayColumnPercent = "20^30^10^20^20";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "t1.VEHICLE_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B";*/
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = sFECTHCOLUMNS;
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//commented by vj on 07-12-2005.
				//objWebGrid.ExtraButtons = "2^Add New~Copy^0~1^addRecord~copy";//"1^Add New^0^addRecord";//"1^Add New^0";
//				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";//"1^Add New^0";
//				objWebGrid.ExtraButtons = "2^Add New~Copy^0~1^addRecord~copy";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				//objWebGrid.HeaderString ="Vehicle Information" ;
				objWebGrid.SelectClass = colors;				
				objWebGrid.FilterLabel = "Show Complete";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns ="VH_ID";//sQUERYSTRING;

				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterColumnName = "t1.Is_Active";
				objWebGrid.FilterValue = "Y";
				objWebGrid.RequireQuery = "Y";
				
				
					
						
					// Pass additional parameters to the next page
				/*if (strCALLEDFROM != null)
				{
					switch (strCALLEDFROM)
					{
						case MOTORCYCLE_SECTION:
							/*objWebGrid.HeaderString ="Motorcycle Information" ;
							objWebGrid.DisplayColumnHeadings = "VIN^Insured Motorcycle Number^Year^Make of Motorcycle^Model of Motorcycle^Model of Motorcycle";
							TabCtl.TabURLs = "/cms/Policies/Aspx/Motorcycle/PolicyMotorCycleInformation.aspx?CALLEDFROM=" + strCALLEDFROM + sQUERYSTRING +"&";
							TabCtl.TabTitles ="Motorcycle Info";
							TabCtl.TabLength =175;
							break;							
						default:
							/*objWebGrid.HeaderString ="Vehicle Information" ;
							TabCtl.TabURLs = "PolicyVehicleInformation.aspx?CALLEDFROM=" + strCALLEDFROM +  sQUERYSTRING +"&"; 
							TabCtl.TabTitles ="Vehicle Info";
							TabCtl.TabLength =150;
							break;				 
					}
				}*/								
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				cltClientTop.CustomerID = int.Parse(GetCustomerID());
				cltClientTop.PolicyID = int.Parse(GetPolicyID());
				cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID());
        
				cltClientTop.ShowHeaderBand ="Policy";
				cltClientTop.Visible = true;        
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion


			if(base.ScreenId == "227" || base.ScreenId == "231" || base.ScreenId == "275")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		
		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}