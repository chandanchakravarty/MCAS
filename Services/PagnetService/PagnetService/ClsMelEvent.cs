using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Cms.DataLayer;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace PagnetService
{
   public class ClsMelEvent
    {
       #region FUNCTION TO GENERATE TEXT FILE
       public string strConnectionString;
       clsUtility obj = new clsUtility();

       public string ConnStr
       {

           get
           {
               return strConnectionString;
           }
           set
           {
               strConnectionString = value;
           }
       }
       public void InsertLog(string PROCESS_TYPE, string ACTIVITY_DESCRIPTION, DateTime START_DATETIME, DateTime END_DATETIME, string STATUS, string ADDITIONAL_INFO)
       {
           int ResultSet = 0;

           DataWrapper objDataWrapperP = null;
           string strStoredProcP = "PROC_PAGNET_PROCESS_LOG";

           objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

           try
           {

               objDataWrapperP.AddParameter("@PROCESS_TYPE", PROCESS_TYPE);
               objDataWrapperP.AddParameter("@ACTIVITY_DESCRIPTION", ACTIVITY_DESCRIPTION);
               objDataWrapperP.AddParameter("@START_DATETIME", START_DATETIME);
               objDataWrapperP.AddParameter("@END_DATETIME", END_DATETIME);
               objDataWrapperP.AddParameter("@STATUS", STATUS);
               objDataWrapperP.AddParameter("@ADDITIONAL_INFO", ADDITIONAL_INFO);
               ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
               objDataWrapperP.ClearParameteres();
               //objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
               ResultSet = 1;
           }
           catch (Exception ex)
           {
               //objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
               throw (new Exception("Error while Insert IN PAGNET_PROCESS_LOG TABLE.", ex.InnerException));
           }
           finally
           {
               objDataWrapperP.Dispose();
           }


       }

       

       //with parameter
       public DataSet getData(string procName)
       {
            
           DataWrapper objDataWrapper = null;
           IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);


           string strStoredProc = procName;
           objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
           //InsertLog("MelEventos", "Going to Fetch records related to meleventos", DateTime.Now, DateTime.Now, "Success", ""); 

           DataSet DS_MEL = new DataSet();
           try
           {
               objDataWrapper.ClearParameteres();
               //objDataWrapper.AddParameter("@INCEPTION_DATE", String.Format("{0:MM/dd/yyyy}",ConvertToDate(DateTime.Now.ToShortDateString())));
               objDataWrapper.AddParameter("@INCEPTION_DATE",String.Format("{0:MM/dd/yyyy}",Convert.ToDateTime(DateTime.Now.ToShortDateString(),culture).ToShortDateString()));     
               //objDataWrapper.AddParameter("@INCEPTION_DATE", dumyDate);    
               DS_MEL = objDataWrapper.ExecuteDataSet(strStoredProc);
               objDataWrapper.ClearParameteres();
               //obj.InsertLog("MelEventos", "Records Fetched successfully of meleventos", DateTime.Now, DateTime.Now, "Success", ""); 
           }
           catch (Exception ex)
           {
              // obj.InsertLog("MelEventos", "Going to Fetch records related to meleventos", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.ToString()); 
               throw (new Exception("Error  in Fetching record for Mel Events:  " + ex.Message.ToString(), ex));


           }

           return DS_MEL;           
           
           
       }   


       //public Boolean GenerateTxt(string procName,string txtFileName, int flagDelFile)
       //{
       public string GenerateTxt(string procName, string txtFileName)
       {
           DataSet ds = new DataSet();
           string strFullLengthString = "";
           InsertLog("MelEventos", "Going to Fetch records related to meleventos", DateTime.Now, DateTime.Now, "Success", ""); 
           ds = getData(procName);
           string strLog = "";
           

           StringBuilder strBuild = new StringBuilder();
           string strTemp = "";
           System.IO.StreamWriter file = null;
           //string strFileLen = "";
           int i = 0;
           int j = 0;

           try
           {
               if (ds != null && ds.Tables.Count > 0)
               {                  

                   for (int tableCount = 0; tableCount < ds.Tables.Count; tableCount++)
                   {

                       if (ds.Tables[tableCount].Rows.Count > 0)
                       {
                           strLog = strLog + "Meleventos: Records are available to create Meleventos File. ";
                           InsertLog("MelEventos", "Records are available to create Meleventos File.", DateTime.Now, DateTime.Now, "Success", ""); 
                           foreach (DataRow dr in ds.Tables[tableCount].Rows)
                           {
                               strTemp = "";
                               strFullLengthString = "";
                               if (i == 0)
                               {
                                   strFullLengthString = "|" + dr["LAYOUT_ID"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["COMPANY_CODE"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["YEAR_MONTH"].ToString();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["BATCH_CODE"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["SEQUENTIAL"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["EVENT_CODE"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["POSTING_DATE"].ToString().Trim() + "|";
                                   strFullLengthString = strFullLengthString + strTemp;
                               }
                               else
                               {
                                   strFullLengthString = "|" + dr["LAYOUT_ID"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["COMPANY_CODE"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["YEAR_MONTH"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["BATCH_CODE"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["SEQUENTIAL"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["EVENT_CODE"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr["POSTING_DATE"].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[7].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[8].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[9].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[10].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[11].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[12].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[13].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[14].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[15].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[16].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[17].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[18].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[19].ToString().Trim();
                                   strFullLengthString = strFullLengthString + strTemp;

                                   strTemp = "|" + dr[20].ToString().Trim() + "|";
                                   strFullLengthString = strFullLengthString + strTemp;

                               }

                               i = i + 1;
                               if (i == ds.Tables[tableCount].Rows.Count)
                               {
                                   //insert into process_log
                                   //below log is commented out as per discussion Pawan and Anurag
                                   //InsertLog("MelEventos", "Records " + dr[5].ToString() + " Inserted successfully ", DateTime.Now, DateTime.Now, "Success", ""); 
                                   strBuild.Append(strFullLengthString);

                               }
                               else
                               {
                                   //insert into process_log
                                   //below log is commented out as per discussion Pawan and Anurag
                                   //InsertLog("MelEventos", "Records " + dr[5].ToString() + " Inserted successfully ", DateTime.Now, DateTime.Now, "Success", ""); 
                                   strBuild.Append(strFullLengthString);
                                   strBuild.Append(Environment.NewLine);
                               }
                           }//foreach close
                       }//if close rows count

                       i = 0;
                       j = j + 1;
                       if (j < ds.Tables.Count)
                       {
                           strBuild.Append(Environment.NewLine);
                           strBuild.Append("\r\n");

                       }

                   } //for loop on tables

                   string dd = DateTime.Now.Day.ToString();
                   string mon = DateTime.Now.Month.ToString();
                   string yy = DateTime.Now.Year.ToString();

                   string FileDate = "";
                   string SearchDate = "";
                   dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
                   mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();

                   string date = "";
                   string strPath = ConfigurationSettings.AppSettings["FilePathForMelEvents"];
                   strLog = strLog + "\r\n" + "Meleventos: Meleventos File is being created at " + strPath.ToString() + ".";
                   InsertLog("MelEventos", "Meleventos File is being created at " + strPath.ToString() + ".", DateTime.Now, DateTime.Now, "Success", strPath); 
                   string[] filePaths = null;
                   int flagFileExists = 0;

                   if (Directory.Exists(strPath))
                   {
                       filePaths = Directory.GetFiles(strPath, "*.txt");

                       //int j = 0;
                       for (int k = 0; k < filePaths.Length; k++)
                       {
                           // FileDate = File.GetCreationTime(filePaths[k]).ToShortDateString();                           
                           //SearchDate = DateTime.Now.ToShortDateString();


                           FileDate = Path.GetFileNameWithoutExtension(filePaths[k]);
                           FileDate = FileDate.Substring(FileDate.IndexOf('_') + 1, 8);



                           SearchDate = DateTime.Now.Day.ToString().PadLeft(2, '0');
                           SearchDate = SearchDate + DateTime.Now.Month.ToString().PadLeft(2, '0');
                           SearchDate = SearchDate + DateTime.Now.Year.ToString();

                           if (FileDate == SearchDate)
                           {
                               flagFileExists = 1;
                               break;
                           }
                       }

                       if (flagFileExists == 0)
                       {
                           date = dd + mon + yy + "0";

                           //using (System.IO.StreamWriter file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathForMelEvents"] + txtFileName + "_" + date + ".txt", true, Encoding.ASCII))
                           //{
                           file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathForMelEvents"] + txtFileName + "_" + date + ".txt", true, Encoding.ASCII);
                           if (strBuild.Length > 0)
                           {
                               file.Write(strBuild);
                               InsertLog("MelEventos", "File " + txtFileName + "_" + date + ".txt" + " created successfully ", DateTime.Now, DateTime.Now, "Success", ConfigurationSettings.AppSettings["FilePathForMelEvents"] + txtFileName + "_" + date + ".txt");
                               strLog = strLog + "\r\n" + "Meleventos: Meleventos File "+ConfigurationSettings.AppSettings["FilePathForMelEvents"] +  txtFileName + "_" + date + ".txt " + "created successfully. ";
                               file.Dispose();

                               //return true;
                           }
                           else
                           {

                               InsertLog("MelEventos", "File " + ConfigurationSettings.AppSettings["FilePathForMelEvents"] + txtFileName + "_" + date + ".txt" + " created successfully ", DateTime.Now, DateTime.Now, "Fail", "");
                               //strReturn = "There is no record to insert";
                               //return strReturn;
                               //return false;
                           }

                           //}



                       }
                       else
                       {
                           strLog = strLog + "\r\n" + "Meleventos: Meleventos File is already is exists for: " + String.Format("{0:dd/MM/yyyy}",DateTime.Now.ToShortDateString());
                           InsertLog("MelEventos", "Meleventos File is already is exists for: " + String.Format("{0:dd/MM/yyyy}",DateTime.Now.ToShortDateString()), DateTime.Now, DateTime.Now, "Fail", "");
                       }
                   }
                   else
                   {
                       strLog = strLog + "Meleventos: Meleventos File path do not exists: " + strPath.ToString();
                       InsertLog("MelEventos", "Meleventos File path do not exists: " + strPath.ToString(), DateTime.Now, DateTime.Now, "Fail", strPath.ToString());
                   }


                   //return true;
               }// 
               else //else of top if
               {
                   //return false;
                   strLog = strLog + "\r\n" + "Meleventos: There is no record to create Meleventos File. ";
                   InsertLog("MelEventos", "There is no record to create Meleventos File." , DateTime.Now, DateTime.Now, "Fail", "");
               }
               return strLog;
           }

           catch (Exception ex)
           {
               //strLog = strLog + "Some error is occurred. Error is " + ex.Message.ToString();
               InsertLog("MelEventos", "Error in file creation", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.ToString()); 
               throw ex;
           }
           finally
           {
               ds.Dispose();
               
           }

       }

    

       #endregion
    }
}
