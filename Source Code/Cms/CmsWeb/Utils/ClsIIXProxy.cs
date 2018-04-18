/******************************************************************************************
<Author					: -   
<Start Date				: -	  
<End Date				: -	
<Description			: - 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 

using System;
using System.Collections;
using System.Text;
using Cms.Model.Client;
using Cms.CmsWeb.com.iix.expressnet;
using System.Configuration;
using System.Xml; 
using Cms.Model.Maintenance;

namespace Cms.CmsWeb.Utils
{


	#region Enum
	public enum ServiceCategory
	{
		MVRDATA = 11666,				//Lookup Value ID
		INSURANCEDATA = 11667,			//Lookup Value ID
		DRIVERDATA = 11668,				//Lookup Value ID
		MVRTOKEN = 11669,				//Lookup Value ID
		INSURANCETOKEN = 11670,			//Lookup Value ID
		DRIVERTOKEN = 11671	,			//Lookup Value ID
		LOSSTOKEN = 14247	,			//Lookup Value ID
		APLLOSSDATA = 14248				//Lookup Value ID
	};
	#endregion

	/// <summary>
	/// Class inherited for Web Service used IIX.
	/// </summary>
	/// 
	public class ClsIIXProxy : auth
	{
			
		
		#region Private Variables
		
		private Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo =  null;
		private ClsRequestResponseLogInfo objLog = null;
		#endregion
	

		#region Constructor
		public ClsIIXProxy()
		{
			System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("IIXSettings");
			
			if ( dic == null )
			{
				throw new Exception("The custom section IIXSettings not found in web.config.");
			}

			if (  dic["URL"] == null )
			{
				throw new Exception("Web service URL not found");
			}
			
		
			string url = dic["URL"].ToString();
			this.Url = url;

			//Create the object.
			objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
		}

		public ClsIIXProxy(string url)
		{
					
			if (  url == null || url.Trim() == "")
			{
				throw new Exception("Web service URL not found");
			}

			this.Url = url;

			//Create the object.
			objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
		}

		#endregion

		#region Properties

		public ClsRequestResponseLogInfo SetLogModel
		{
			get
			{
				return objLog;
			}
			set
			{
				objLog = value;
			}
		
		}

		#endregion

		#region Methods
		/// <summary>
		/// Use to log the response and call base class method.
		/// </summary>
		/// <param name="msgString"></param>
		/// <returns></returns>
		public new string getResponse(string msgString) 
		{
			try
			{
				//Log before Data Request.
				objGenInfo.InsertRequestLog(objLog);
			
				//Call response method
				string response = base.getResponse(msgString);

				//Update after Data Response
				objLog.IIX_RESPONSE=objLog.IIX_RESPONSE + ' ' + response;
				objLog.RESPONSE_DATETIME = DateTime.Now;
				objGenInfo.UpdateRequestLog(objLog);	
			
				return response;
			}
			catch(Exception ex)
			{
				throw( new Exception("IIX web service not responding.Please Try Later.",ex));
			}
		}

		/// <summary>
		/// Use to log the request and call base class method.
		/// </summary>
		/// <param name="msgString"></param>
		/// <returns></returns>
		public new string sendRequest(string msgString) 
		{
			try
			{
				//Log the Token Request
				objGenInfo.InsertRequestLog(objLog);
			
				//Call the Request Method
				string request = base.sendRequest(msgString);
			
				//Update the Token Request
				objLog.RESPONSE_DATETIME = DateTime.Now;
				objGenInfo.UpdateRequestLog(objLog);
			
				return request;
			}
			catch(Exception ex)
			{
				throw( new Exception("IIX web service not responding.Please Try Later.",ex));
			}
		}

		#endregion
	}
}
