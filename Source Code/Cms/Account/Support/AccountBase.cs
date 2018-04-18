using System;
using System.Xml;
using Ajax;
using System.Collections;
using System.Data;

namespace Cms.Account
{
	/// <summary>
	/// Summary description for AccountBase.
	/// </summary>
	public class AccountBase : Cms.CmsWeb.cmsbase
	{
		//Stores the bank aacount no which will be defaulted in diff screen
		private static string strDefBankAccountId;

		

		public AccountBase()
		{
			//
			// TODO: Add constructor logic here
			//
			
		
		}
		// Transanction Type
		public enum enumTransType
		{
			NSF = 37,
			LF  = 38,
			IF	= 39,
			CIF  = 7
//CIF Added For Charge  Installment Fee Screen
			
		}

		//This property is used to set the size of page of deposit details screen
		protected int DepositDetailsPageSize
		{
			get
			{
				return 18;	//by default only 20 records will be visible
			}
		}

		/// <summary>
		/// Returns the bank account no, which will be defaulted in deposit and other screen
		/// </summary>
		public static string DefBankAccountId
		{
			get
			{
				if (strDefBankAccountId == "" || strDefBankAccountId == null)
				{
					//Retreiving the bank acc no from web.config
					strDefBankAccountId = System.Configuration.ConfigurationManager.AppSettings.Get("DefBankAccountId");
				}
				return strDefBankAccountId;
			}
		}

		[AjaxMethod()]
		public string GetSearch(string query, string lookupNodeName, string dispColName, string args)
		{
			
			try
			{
				string xmlGridConfigFile = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("RootURL") + "/support/LookupForms1.xml");

				//string xmlGridConfigFile = @"C:\Inetpub\wwwroot\lookup\LookupForms1.xml";

				XmlDocument xmlDoc = new XmlDocument();

				xmlDoc.Load(xmlGridConfigFile);
				
				//Get root
				XmlNode xmlSql = xmlDoc.SelectSingleNode("LOOKUPFORM/" + lookupNodeName + "/SQL");
			
			
				//Save to ViewState
				string finalSQL = xmlSql.InnerText.Trim();

				if ( args.Trim() != "" )
				{
					string[] strArray = args.Split(";".ToCharArray());
			
					Hashtable htWhere = new Hashtable();

					for(int j = 0; j < strArray.Length; j++)
					{
						string[] strSplit = strArray[j].Split("=".ToCharArray());
						
						htWhere.Add(strSplit[0],strSplit[1]);
					}
			
					IDictionaryEnumerator myEnumerator = htWhere.GetEnumerator();

					//Build the where columns if any
					while ( myEnumerator.MoveNext() )
					{
						//If no where condtion is set
						if ( myEnumerator.Value == null ) continue;
						
						string strValue = myEnumerator.Value.ToString().Trim();
						
						if ( strValue == "" )
						{
							strValue = "0";
							//Remove the where clause
							int whereIndex = finalSQL.IndexOf("WHERE ");
							finalSQL = finalSQL.Substring(0,whereIndex);
							break;
						}
						else
						{
							finalSQL = finalSQL.Replace(myEnumerator.Key.ToString().Trim(),strValue);
						}
					}
				}
				
				query = Server.UrlDecode(query);

				finalSQL = "SELECT * FROM ( " + finalSQL + ")t WHERE " + dispColName + " LIKE '" + query + "%'";
				
				DataSet ds = Cms.BusinessLayer.BlCommon.ClsCommon.ExecuteDataSet(finalSQL);

				string xml = ds.GetXml();
			
				return xml;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
		}

	}
}
