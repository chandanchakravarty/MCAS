using System;
using EODCommon;
using Cms.BusinessLayer;
using System.Data ;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.BusinessLayer.BlProcess;
using Cms.BusinessLayer.BlCommon ;
using System.Xml;
namespace EODPolicyProcesses
{
	/// <summary>
	/// Summary description for ClsEODPolicyProcess.
	/// </summary>
	public class ClsEODPolicyProcess : ClsEODCommon
	{
			
		public ClsEODPolicyProcess()
		{
		}

		public void AddFollowup()
		{
			ClsPolicyProcess objProcess = new ClsPolicyProcess();
			string XMLPath;
			XMLPath = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
			XmlDocument configXML = new XmlDocument();
			configXML.Load(XMLPath);
			XmlNode diaryNode = configXML.SelectSingleNode("Root/PendingFollowUP");
			objProcess.MakeEntryforEOD("<PendingFollowUP>" + diaryNode.InnerXml + "</PendingFollowUP>",EOD_USER_ID);

		}
		public void GenerateRTLHotFile()
		{
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.PolicyProcess ;
			objLog.SubActivity = (int) EODActivities.PolicyProcess ;

			ClsAttachment lImpertionation =  new ClsAttachment();
			try
			{

				if (lImpertionation.ImpersonateUser(ClsCommon.ImpersonationUserId,ClsCommon.ImpersonationPassword,
						ClsCommon.ImpersonationDomain ))
				{
					string XMLPath;
					XMLPath = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
					XmlDocument configXML = new XmlDocument();
					configXML.Load(XMLPath);
					XmlNode RTLParameters = configXML.SelectSingleNode("Root/RTLParameters");
					string LocalUploadDirectory = RTLParameters.SelectSingleNode("LocalUploadDirectory").InnerText.Trim();
					string RemoteHostName= RTLParameters.SelectSingleNode("RemoteHostName").InnerText.Trim();
					string RemoteUserName= RTLParameters.SelectSingleNode("RemoteUserName").InnerText.Trim();
					string RemotePassword= RTLParameters.SelectSingleNode("RemotePassword").InnerText.Trim();
					string RemoteDirectory= RTLParameters.SelectSingleNode("RemoteDirectory").InnerText.Trim();
					string FilePath = LocalUploadDirectory + RTLParameters.SelectSingleNode("RTLFileName").InnerText.Trim();
			
					string strDate = DateTime.Now.Year.ToString().Substring(2) 
						+ DateTime.Now.Month.ToString().PadLeft(2,'0') 
						+ DateTime.Now.Day.ToString().PadLeft(2,'0');

					string strTime = DateTime.Now.Hour.ToString().PadLeft(2,'0')
						+ DateTime.Now.Minute.ToString().PadLeft(2,'0');

					FilePath = FilePath + " - " + strDate + strTime + ".txt";
			
					string strStoredProc = "Proc_GetFiscalYearActiveCancPolicies";
					DataSet dsFiscalPolicies= null;
					string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
					DataWrapper objDataWrapper = new DataWrapper(strCnnString,CommandType.StoredProcedure);

					try
					{
						string PolNum = "";
				
						dsFiscalPolicies = new DataSet();

						objLog.ActivtyDescription = "Fetching Policies from DataBase: ";
						objLog.StartDateTime = System.DateTime.Now ;
 				
						dsFiscalPolicies= objDataWrapper.ExecuteDataSet(strStoredProc);
						System.IO.StreamWriter RTLFile = new System.IO.StreamWriter(FilePath, true);
						objLog.ActivtyDescription +=" Creating Hot File";
						foreach(DataRow drFiscal in dsFiscalPolicies.Tables[0].Rows)
						{
							PolNum = drFiscal["POLICY_NUMBER"].ToString();
							//Code will come from SP itself
							//					if(drFiscal["POLICY_STATUS"].ToString().ToUpper() == "CANCEL")
							//						PolNum += "C";
							RTLFile.WriteLine(PolNum);
						}
						RTLFile.Close();

						//objLog.ActivtyDescription += " Uploading to FTP" ;
						//FTPRTLFile(FilePath,RemoteHostName,RemoteUserName,RemotePassword,RemoteDirectory);
					
						objLog.Status= ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = DateTime.Now;
						WriteLog(objLog);
					}
					catch(Exception ex)
					{
						objLog.Status = ActivityStatus.FAILED ;
						objLog.EndDateTime = System.DateTime.Now;
						objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
						WriteLog(objLog);
					}
					finally
					{
						objDataWrapper.Dispose();
					}
				}
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED ;
				objLog.EndDateTime = System.DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
			}
		}
		
		public void FTPRTLFile(string FileName,string HostName, string UserName,string PassWord,
					string FTPDirectory)
		{
			FtpLib.FTPFactory objFTP =  new  FtpLib.FTPFactory();
			objFTP.RemoteHost = HostName;
			objFTP.RemoteUser = UserName;
			objFTP.RemotePassword = PassWord;
			objFTP.Login();
			objFTP.ChangeDirectory(FTPDirectory);
			objFTP.Upload(FileName);
			objFTP.Close();
		}
	
		public void UpdatePolicyStatus()
		{
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString,CommandType.StoredProcedure);

			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.PolicyProcess ;
			objLog.SubActivity = (int) EODActivities.PolicyProcess ;
			try
			{
				objLog.ActivtyDescription = "Updating Policy Status for future effective policies : ";
				objLog.StartDateTime = System.DateTime.Now ;
				objDataWrapper.ExecuteNonQuery("Proc_EOD_UpdatePoliciesStatus");
				objLog.Status= ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				WriteLog(objLog);
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED ;
				objLog.EndDateTime = System.DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
	}
}
