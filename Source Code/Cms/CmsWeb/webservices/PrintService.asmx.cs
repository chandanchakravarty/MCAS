/******************************************************************************************
<Author					: -   Mohit Agarwal
<Start Date				: -	  11-Dec-2006
<End Date				: -	
<Description			: - 	Web service to return the pdf files from server url in byte form
<Review Date			: - 
<Reviewed By			: - 	
Modification History


<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Text;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using System.Configuration;
using System.Web.Services.Protocols;

namespace Cms.CmsWeb.webservices
{
	/// <summary>
	/// Summary description for PrintService.
	/// </summary>
	public class PrintService : System.Web.Services.WebService
	{
		public AuthenticationToken AuthenticationTokenHeader;
		private string authenticationKey ="";

		private int fileno = 0;

		public PrintService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}


		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			authenticationKey = ClsCommon.ServiceAuthenticationToken;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		

		//Web method for getting all the files in url folder and one sub directory level
		#region ReadTemplateFromServer
		[WebMethod]
		public byte[][] ReadTemplateFromServer(string Url,string fileNametoFetch,string fileName,  ref string[] fileNames)
		{
			int NumFiles = 1;

			byte [][] Filesbytes;
			Filesbytes = new byte [1][];
			Filesbytes[0] = null;

            string lstrUserName = ConfigurationManager.AppSettings.Get("IUserName");
            string lstrPassword = ConfigurationManager.AppSettings.Get("IPassWd");
            string lstrDomain = ConfigurationManager.AppSettings.Get("IDomain");

			fileName = DecryptData(fileName); 		
			fileNametoFetch = DecryptData(fileNametoFetch);
			Url=DecryptData(Url); 

			ClsAttachment lImpertionation =  new ClsAttachment();
			if (lImpertionation.ImpersonateUser(lstrUserName,lstrPassword,lstrDomain))
			{
				string FilePath = Server.MapPath(Url+"\\" + fileNametoFetch);// + "\\" + fileName;
				
				
				fileno = 0;
		
				Filesbytes = new byte [NumFiles][];
				fileNames = new string[NumFiles];

				for(int indexbyte = 0;indexbyte < Filesbytes.Length; indexbyte++)
				{
					Filesbytes[indexbyte] = null;
					fileNames[indexbyte] = "";
				}
						
				if(((fileName == "CHK")&& fileNametoFetch.EndsWith(fileName))||((fileName != "CHK")&& !fileNametoFetch.EndsWith("CHK")))
				{

					FileStream FSO=null;
					try
					{
						FSO = new FileStream(FilePath,FileMode.Open,FileAccess.Read);
						if(FSO != null)
						{
							FileInfo finfo = new FileInfo(FilePath);

							DirectoryInfo dinfo = finfo.Directory;

							FileSystemInfo[] fsinfo = dinfo.GetFiles();

							fileNames[fileno] = fileNametoFetch ;

							byte[] TemplateBytes = new byte[FSO.Length];
							FSO.Read(TemplateBytes,0,Convert.ToInt32(FSO.Length));
							FSO.Close();
					
							Filesbytes[0] = TemplateBytes;
						}
						else
							Filesbytes[0] = null;
					}
					catch(Exception ex)
					{
						ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(new Exception("Error while reading template from server " , ex));
						Filesbytes[0] = null;
					}
				}
				else
				Filesbytes[0] = null;
				lImpertionation.endImpersonation();
			}


			return Filesbytes;

		}
		#endregion

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetXMLFromDataSet()
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{

				DataSet dsPrinter =null;

                string ConnStr = ConfigurationManager.AppSettings.Get("DB_CON_STRING");
				SqlConnection sqlconn = new SqlConnection(ConnStr);
				if (sqlconn.State == 0)
					sqlconn.Open();

				SqlCommand sqlcomm = new SqlCommand("Select * from MNT_PRINT_DOCUMENT_TYPE WHERE IS_ACTIVE='Y' ORDER BY DOCUMENT_TYPE ",sqlconn);
				SqlDataAdapter adapterPrint = new SqlDataAdapter(sqlcomm);
				dsPrinter = new DataSet();

				adapterPrint.Fill(dsPrinter);

				sqlconn.Close();

				return EncryptData(dsPrinter.GetXml());
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}


	

		[WebMethod(MessageName="OnDemand")][SoapHeader("AuthenticationTokenHeader")]
		public string GetPrintJobsXML(string fromDate,string toDate,string strDocument,string query,string onDemandFlag,string orderBy)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				DataSet dsPrinter = new DataSet();
                string ConnStr = ConfigurationManager.AppSettings.Get("DB_CON_STRING");
		
				SqlConnection sqlconn = new SqlConnection(ConnStr);
				if (sqlconn.State == 0)
					sqlconn.Open();


				SqlCommand sqlcomm = new SqlCommand("PROC_GETPRINTJOBSXML",sqlconn);
		
				SqlDataAdapter adapterPrint = new SqlDataAdapter(sqlcomm);
				sqlcomm.CommandType = CommandType.StoredProcedure;
					
				sqlcomm.Parameters.Add("@QUERY",SqlDbType.VarChar ,4000 );
		
				query=DecryptData(query);  

				if(query.ToUpper().IndexOf("WHERE")<0)
					query+=" WHERE DOCUMENT_CODE IN (" + DecryptData(strDocument) + ") AND CAST(CONVERT(VARCHAR,PRINT_DATETIME,101) AS DATETIME) >='" + DateTime.Parse(DecryptData(fromDate)).ToShortDateString() + "' AND CAST(CONVERT(VARCHAR,PRINT_DATETIME,101) AS DATETIME)<='" + DateTime.Parse(DecryptData(toDate)).ToShortDateString() + "' AND ONDEMAND_FLAG IN (" +  DecryptData(onDemandFlag) + ") " + DecryptData(orderBy);
				else if(query.ToUpper().IndexOf("WHERE")>0)
					query+=" AND DOCUMENT_CODE IN (" + DecryptData(strDocument) + ") AND CAST(CONVERT(VARCHAR,PRINT_DATETIME,101) AS DATETIME) >='" + DateTime.Parse(DecryptData(fromDate)).ToShortDateString() + "' AND CAST(CONVERT(VARCHAR,PRINT_DATETIME,101) AS DATETIME)<='" + DateTime.Parse(DecryptData(toDate)).ToShortDateString() + "' AND ONDEMAND_FLAG IN (" +  DecryptData(onDemandFlag) + ") " + DecryptData(orderBy);

			
				sqlcomm.Parameters["@QUERY"].Value=query;
	
				adapterPrint.Fill(dsPrinter);

				sqlconn.Close();	

				return EncryptData(dsPrinter.GetXml());
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}

		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public string GetRePrintJobsXML(string fromDate,string toDate,string strDocument,string rePrintQuery,string rePrintOrder)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				DataSet dsPopupPrinter = new DataSet();

                string ConnStr = ConfigurationManager.AppSettings.Get("DB_CON_STRING");
			
				SqlConnection sqlconn = new SqlConnection(ConnStr);
				if (sqlconn.State == 0)
					sqlconn.Open();


				SqlCommand sqlcomm = new SqlCommand("PROC_GETPRINTJOBSXML",sqlconn);
			
				SqlDataAdapter adapterPrint = new SqlDataAdapter(sqlcomm);
				sqlcomm.CommandType = CommandType.StoredProcedure;
						
	
				sqlcomm.Parameters.Add("@QUERY",SqlDbType.VarChar ,4000 );

				rePrintQuery=DecryptData(rePrintQuery);

				if(rePrintQuery.ToUpper().IndexOf("WHERE")<0)
					rePrintQuery +=" WHERE DOCUMENT_CODE IN (" + DecryptData(strDocument) + ") AND CAST(CONVERT(VARCHAR,PRINT_DATETIME,101) AS DATETIME) >='" + DateTime.Parse(DecryptData(fromDate)).ToShortDateString() + "' AND CAST(CONVERT(VARCHAR,PRINT_DATETIME,101) AS DATETIME)<='" + DateTime.Parse(DecryptData(toDate)).ToShortDateString() + "'" +   DecryptData(rePrintOrder);
				else if(rePrintQuery.ToUpper().IndexOf("WHERE")>0)
					rePrintQuery +=" AND DOCUMENT_CODE IN (" + DecryptData(strDocument) + ") AND CAST(CONVERT(VARCHAR,PRINT_DATETIME,101) AS DATETIME) >='" + DateTime.Parse(DecryptData(fromDate)).ToShortDateString() + "' AND CAST(CONVERT(VARCHAR,PRINT_DATETIME,101) AS DATETIME)<='" + DateTime.Parse(DecryptData(toDate)).ToShortDateString() + "'" +   DecryptData(rePrintOrder);
				
				sqlcomm.Parameters["@QUERY"].Value=rePrintQuery;
				
				adapterPrint.Fill(dsPopupPrinter);

				sqlconn.Close();	

				return EncryptData(dsPopupPrinter.GetXml());
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);

		}


		[WebMethod(MessageName="ServiceCall")][SoapHeader("AuthenticationTokenHeader")]
		public string GetPrintJobsXML(string query,string orderBy,string onDemandFlag)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				DataSet dsPrinter = new DataSet();

                string ConnStr = ConfigurationManager.AppSettings.Get("DB_CON_STRING");
				
				SqlConnection sqlconn = new SqlConnection(ConnStr);
				if (sqlconn.State == 0)
					sqlconn.Open();

				SqlCommand sqlcomm = new SqlCommand("PROC_GETPRINTJOBSXML",sqlconn);
				
				SqlDataAdapter adapterPrint = new SqlDataAdapter(sqlcomm);
				sqlcomm.CommandType = CommandType.StoredProcedure;
							
				sqlcomm.Parameters.Add("@QUERY",SqlDbType.VarChar ,4000 );

				query =DecryptData(query);

				if(query.ToUpper().IndexOf("WHERE")<0)
					query += " WHERE ONDEMAND_FLAG IN (" +  DecryptData(onDemandFlag) + ") " + DecryptData(orderBy);
				else if(query.ToUpper().IndexOf("WHERE")>0)
					query += " AND ONDEMAND_FLAG IN (" +  DecryptData(onDemandFlag) + ") " + DecryptData(orderBy);
				
				sqlcomm.Parameters["@QUERY"].Value=query;

				adapterPrint.Fill(dsPrinter);

				sqlconn.Close();	

				return EncryptData(dsPrinter.GetXml());	
			}
			else
				return(ClsCommon.ServiceAuthenticationMsg);
		}



		[WebMethod][SoapHeader("AuthenticationTokenHeader")]
		public void Update_PrintJobs(string customer_id, string doc_code, string success, string Duplex,string printJobsId)
		{
			if(AuthenticationTokenHeader!=null && AuthenticationTokenHeader.TokenValue!="" && AuthenticationTokenHeader.TokenValue == authenticationKey)
			{
				string updateComm;
				SqlConnection sqlconn =null;
                string ConnStr = ConfigurationManager.AppSettings.Get("DB_CON_STRING");
				sqlconn = new SqlConnection(ConnStr);
				if (sqlconn.State == 0)
					sqlconn.Open();

				if(DecryptData(Duplex) == "True")
					updateComm = "UPDATE PRINT_JOBS SET PRINT_SUCCESSFUL = '" + DecryptData(success) + "', PRINTED_DATETIME = '" + DateTime.Now.ToString() + "', DUPLEX = 'Y' WHERE CUSTOMER_ID = " + DecryptData(customer_id) + " AND DOCUMENT_CODE = '" + DecryptData(doc_code) + "' and PRINT_JOBS_ID="+ int.Parse(DecryptData(printJobsId));
				else
					updateComm = "UPDATE PRINT_JOBS SET PRINT_SUCCESSFUL = '" + DecryptData(success) + "', PRINTED_DATETIME = '" + DateTime.Now.ToString() + "', DUPLEX = 'N' WHERE CUSTOMER_ID = " + DecryptData(customer_id) + " AND DOCUMENT_CODE = '" + DecryptData(doc_code) + "' and PRINT_JOBS_ID="+ int.Parse(DecryptData(printJobsId));

				SqlCommand sqlcomm = new SqlCommand(updateComm,sqlconn);

				sqlcomm.ExecuteNonQuery();

				sqlconn.Close();
			}
			


		}

		/// <summary>
		///	To decryption of data to avoid any manipulation in data if this Control called from outside the application
		/// </summary>
		/// <param name="pStrData"></param>
		/// <returns> decrypted data string </returns>
		private string DecryptData(string pStrData)
		{
			string data = null;
			//data = System.Web.HttpUtility.UrlDecode(pStrData.Trim());
			pStrData = pStrData.Replace("%3D","=");
			data = cmsbase.DecryptMessage(pStrData.Trim());
			return data;
		}

		/// <summary>
		///	To encryption of data to avoid any manipulation in data if this Control called from outside the application
		/// </summary>
		/// <param name="pStrData"></param>
		/// <returns></returns>
		private string EncryptData(string pStrData)
		{
			string data = null;
			data = cmsbase.EncryptMessage(pStrData.Trim());
			return data;
		}

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

		//		[WebMethod]
		//		public string HelloWorld()
		//		{
		//			return "Hello World";
		//		}
	}
}

