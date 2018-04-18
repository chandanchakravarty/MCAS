/******************************************************************************************
	<Author					: - > Pravesh K Chandel
	<Start Date				: -	> 
	<End Date				: - >
	<Description			: - > Class to fetch Menu data
	<Review Date			: - >
	<Reviewed By			: - >
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.support
{
	/// <summary>
	/// Summary description for ClsMenuData.
	/// </summary>
	public class ClsMenuData
	{
		public ClsMenuData()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public string fetchLobMenu(string LobString, string PolicyLevel, string AgencyCode)
		{
			//Calling Business Class
			ClsCommon objBLCommon = new ClsCommon();
			try
			{
				System.Web.SessionState.HttpSessionState sess;
				sess = HttpContext.Current.Session;
				if (sess["systemId"]!=null && sess["systemId"].ToString()!="")
				{
					AgencyCode = sess["systemId"].ToString().ToUpper();
				}
				return objBLCommon.GetLobMenuXml(LobString, PolicyLevel, AgencyCode);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public string fetchPolicyMenu(string PolicyId, string PolicyVersionID, string CustomerID, string LobString,string AgencyID)
		{
			
			System.Web.SessionState.HttpSessionState sess;
			sess = HttpContext.Current.Session;
			if (sess["systemId"]!=null && sess["systemId"].ToString()!="")
			{
				AgencyID = sess["systemId"].ToString().ToUpper();
			}

			//string MenuXml = fetchLobMenu(LobString, "1", "W001");
			//AgencyID will be passed from the login page itself
			string MenuXml = fetchLobMenu(LobString, "1", AgencyID);
			
			//Retrieve the list only the users belong to w001 group
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID.ToUpper();
			if(AgencyID!=strCarrierSystemID) return MenuXml;
			
			//Retreiving the list of eligible process for the policy
			ClsCommon objCommon = new ClsCommon();
			System.Data.DataSet ds = objCommon.GetEligibleProcess(int.Parse(CustomerID), int.Parse(PolicyId), int.Parse(PolicyVersionID));
			
			
			
			if(ds == null)
				return MenuXml;

			if(ds.Tables.Count == 0)
				return MenuXml;

			if(ds.Tables[0].Rows.Count == 0)
				return MenuXml;

			//Adding the process menu
			System.Data.DataTable dt = ds.Tables[0];
			
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml(MenuXml);
            ClsMessages.SetCustomizedXml(Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_CULTURE);
			//Appending the process in menus
			System.Xml.XmlNode node = doc.SelectSingleNode("/root/button[@menu_id='2']");
			if (node != null)
			{
				//Process menu under customer manager
				System.Xml.XmlElement topmenu = doc.CreateElement("topMenu");
                string MenuName = ClsMessages.FetchGeneralMessage("1325");//Added by Lalit for process menu multilingual
				//topmenu.SetAttribute("name", "Process");
                topmenu.SetAttribute("name", MenuName);
				topmenu.SetAttribute("menu_id", "1000");
				topmenu.SetAttribute("linkUrl", "");
				topmenu.SetAttribute("enabled", "true");
				
				System.Xml.XmlElement menu;
				int i = 0;
				foreach(System.Data.DataRow dr in dt.Rows)
				{
					//Processes menu under process menu
					menu = doc.CreateElement("menu");
					menu.SetAttribute("name", dr["PROCESS_DESC"].ToString());
					menu.SetAttribute("menu_id", (1000 + i).ToString());
                    string QueryStr = QueryStringModule.EncriptQueryString("Customer_ID="
                        + CustomerID + "&Policy_ID=" + PolicyId + "&Policy_Version_ID="
                        + PolicyVersionID + "&PROCESS=" + dr["PROCESS_SHORTNAME"].ToString());
					menu.SetAttribute("linkUrl", "/cms/policies/processes/PolicyProcess.aspx" + QueryStr);
					menu.SetAttribute("enabled", "true");
					
					topmenu.AppendChild(menu);
					i++;
				}

				node.AppendChild(topmenu);
			}

			return doc.OuterXml;
		}

		/// <summary>
				/// </summary>
		/// <returns></returns>
		public string fetchDefaultMenu(string AgencyCode)
		{
			//Calling Business Class
			ClsCommon objBLCommon = new ClsCommon();
			try
			{
				System.Web.SessionState.HttpSessionState sess;
				sess = HttpContext.Current.Session;
				if (sess["systemId"]!=null && sess["systemId"].ToString()!="")
				{
					AgencyCode = sess["systemId"].ToString().ToUpper();
				}
				return objBLCommon.GetDefaultMenuXml(AgencyCode);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// </summary>
		/// <returns>returns menu data in XML string</returns>
		public string fetchData()
		{
			//Calling Business Class
			ClsCommon objBLCommon = new ClsCommon();
			try
			{
				return objBLCommon.GetMenuXml();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

	}
}
