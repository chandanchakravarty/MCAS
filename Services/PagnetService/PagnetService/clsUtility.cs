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
using System.Security.AccessControl;
using System.Net;
using Microsoft.VisualBasic;



using System.Threading;
using System.Globalization;

//namespace GenerateTxt
//{
    public class clsUtility
    {
        #region GLOBAL VARIABLES
        string strFullLengthString = "";
        SqlParameter[] aryParm;
        StringBuilder strBuild;
        string strReturn = "";
        public string strConnectionString;        
       
        //Thread.C
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture((lang_CULTURE == null || lang_CULTURE == "") ? DEFAULT_LANG_CULTURE : lang_CULTURE);
        //Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
        //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

        public clsUtility()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            //Currency Format                
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";
            Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyGroupSeparator = ",";

            //Number Format
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
            Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberGroupSeparator = ",";

        }
        public enum FileStatus
        {
            Generated,
            Imported_by_PagNet_with_success,
            Imported_by_PagNet_with_error,
            Return_Processed,
            ok
        };

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

        #endregion

        #region FUNCTION FOR FETCH DATA AND UPDATE

        //with parameter 

        public static string DecryptString(string strText)
        {
            string Password = "EBIXINDIA";
            // First  need to turn the input string into a byte array. 
            // We presume that Base64 encoding was used 
            if (strText.Trim() == "") return "";
            byte[] cipherBytes = Convert.FromBase64String(strText);

            // Then, need to turn the password into Key and IV 
            // here using salt to make it harder to guess our key using a dictionary attack - 
            // trying to guess a password by enumerating all possible words. 

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            // Now get the key/IV and do the decryption using the function that accepts byte arrays. 
            // Using PasswordDeriveBytes object we are first getting 32 bytes for the Key 
            // (the default Rijndael key length is 256bit = 32bytes) and then 16 bytes for the IV. 
            // IV should always be the block size, which is by default 16 bytes (128 bit) for Rijndael. 

            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            // Now we need to turn the resulting byte array into a string. 
            // We are going to be using Base64 encoding that is designed exactly for what we are using 

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }
        // Decrypt a byte array into a byte array using a key and an IV 

        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the decrypted bytes 

            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 
            // We are going to use Rijndael because it is strong and available on all platforms. 
            Rijndael alg = Rijndael.Create();
            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because the algorithm is operating in its default 
            // mode called CBC (Cipher Block Chaining). The IV is XORed with the first block (8 byte) 
            // of the data after it is decrypted, and then each decrypted block is XORed with the previous 
            // cipher block. This is done to make encryption more secure. 

            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be pumping our data. 
            // CryptoStreamMode.Write means that we are going to be writing data to the stream 
            // and the output will be written in the MemoryStream we have provided. 

            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            // Write the data and make it do the decryption 
            cs.Write(cipherData, 0, cipherData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            // This will tell it that we have done our decryption and there is no more data coming in, 
            // and it is now remove the padding and finalize the decryption process. 

            cs.Close();

            // Now get the decrypted data from the MemoryStream. 
            byte[] decryptedData = ms.ToArray();
            return decryptedData;

        }

        public DataSet getData(string strProcName)
        {
            DataWrapper objDataWrapper = null;

            string strStoredProc = strProcName;
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            DataSet DS = new DataSet();
            try
            {
                objDataWrapper.ClearParameteres();
                DS = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error in Fetch data:  " + ex.Message.ToString(), ex.InnerException));

            }

            return DS;

        }
       

        public void updateCommissionFlag(string strProcName, string strTableName, int intRowId, int claimId, int activityId, char is_commission_process)
        {
            int ResultSet = 0;

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = strProcName;

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapperP.AddParameter("@TABLE_NAME", strTableName);
                objDataWrapperP.AddParameter("@ROW_ID", intRowId);
                objDataWrapperP.AddParameter("@CLAIM_ID", claimId);
                objDataWrapperP.AddParameter("@ACTIVITY_ID", activityId);
                objDataWrapperP.AddParameter("@IS_COMMISSION_PROCESS", is_commission_process);

                
                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                ResultSet = 1;
            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while updating record.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }
                      

        }
               
        //UPDATE COMMISSION FLAG
        public void updateCommissionFlag(string strProcName, string strTableName, int intRowId, int claimId, int activityId, int covID, int compID, char is_commission_process)
        {

            int ResultSet = 0;

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = strProcName;

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {                
                objDataWrapperP.AddParameter("@TABLE_NAME", strTableName);
                objDataWrapperP.AddParameter("@ROW_ID", intRowId);
                objDataWrapperP.AddParameter("@CLAIM_ID", claimId);
                objDataWrapperP.AddParameter("@ACTIVITY_ID", activityId);
                objDataWrapperP.AddParameter("@COV_ID", covID);
                objDataWrapperP.AddParameter("@COMP_ID", compID);
                objDataWrapperP.AddParameter("@IS_COMMISSION_PROCESS", is_commission_process);



                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                ResultSet = 1;
            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while updating record.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }                    
            


        }

        public void updatePagnetExport(string strProcName, string PAYMENT_ID, string IS_EXPORT, string b_ac_no, string p_ac_no, string operation_Type, string FILE_NAME)
        {
            int ResultSet = 0;

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = strProcName;

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapperP.AddParameter("@PAYMENT_ID", PAYMENT_ID);
                objDataWrapperP.AddParameter("@IS_EXPORT", IS_EXPORT);                
                objDataWrapperP.AddParameter("@FILE_NAME", FILE_NAME);  
                objDataWrapperP.AddParameter("@BENEFICIARY_BANK_ACCOUNT_NUMBER", b_ac_no);
                objDataWrapperP.AddParameter("@PAYEE_BANK_ACCOUNT_NO", p_ac_no);
                objDataWrapperP.AddParameter("@OPERATION_TYPE", operation_Type); 
                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                ResultSet = 1;
            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while updating PAGNET_EXPORT_RECORD TABLE.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }


        }
        

        public void InsertPagnetExportFiles(string strProcName, string interface_code, string FILE_NAME, string status, string created_by)
        {
            int ResultSet = 0;

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = strProcName;

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            try
            {                              
                             
                objDataWrapperP.AddParameter("@FILE_NAMES", FILE_NAME);
                objDataWrapperP.AddParameter("@INTERFACE_CODE", interface_code);
                objDataWrapperP.AddParameter("@STATUS", status);
                objDataWrapperP.AddParameter("@USER_NAME", created_by);
                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                //objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                ResultSet = 1;
            }
            catch (Exception ex)
            {
                //objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while Insert IN Pagnet_Export_Files TABLE.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }


        }

        public void UpdateExportFiles(string strProcName, string status,string created_by, string FILE_NAME )
        {
            int ResultSet = 0;
            

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = strProcName;

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapperP.AddParameter("@FILE_NAME", FILE_NAME);
                objDataWrapperP.AddParameter("@STATUS", status);
                objDataWrapperP.AddParameter("@USER_NAME", created_by);                
                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                ResultSet = 1;
                
            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while updating STATUS IN PAGNET_EXPORT_FILE TABLE.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }
            
        }

        public string UpdateFlagsInParent(string strProcName, int file_id)
        {
            //int ResultSet = 0;
            string ResultSet = "";


            DataWrapper objDataWrapperP = null;
            string strStoredProcP = strProcName;

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                //SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@ROW_ID", objProcessInfo.ROW_ID,
                //   SqlDbType.Int, ParameterDirection.Output);
                //objDataWrapperP.ClearParameteres();
                objDataWrapperP.AddParameter("@F_ID", file_id);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapperP.AddParameter("@OUT_PARAM",null, SqlDbType.VarChar, ParameterDirection.Output,200);
                objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                ResultSet = objSqlParameter.Value.ToString();
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                

            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while updating STATUS IN Update Flags In Parents TABLE.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }
            return ResultSet;
        }

        public DataSet GetExportFiles(string strProcName,string status)
        {
            //int ResultSet = 0;
            DataSet ds = new DataSet();

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = strProcName;

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                //objDataWrapperP.AddParameter("@FILE_NAMES", FILE_NAME);
                objDataWrapperP.AddParameter("@STATUS", status);
                //objDataWrapperP.AddParameter("@FLAG", flag);
                ds = objDataWrapperP.ExecuteDataSet(strStoredProcP);
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);

            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while fatch from PAGNET EXPORT TABLE, Proc : PROC_GET_PAGNET_EXPORT_FILES.", ex.InnerException));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }
            return ds;
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


        #endregion        

        #region IMPORT RETURNING FILE

        //Import Returning files into system
        public int Import(string strData,int Isformat,string createdBy,string import_file_name,int delFlag)
        {
            
            int ResultSet = 0;

            DataWrapper objDataWrapperP = null;
            string strStoredProcP = "PROC_PAGNET_IMPORT"; 

            objDataWrapperP = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                
                objDataWrapperP.AddParameter("@records", strData);
                //isformat=1 then insert to in main table after format
                objDataWrapperP.AddParameter("@Isformat", Isformat);
                objDataWrapperP.AddParameter("@CREATED_BY", createdBy);
                objDataWrapperP.AddParameter("@IMPORT_FILE_NAME", import_file_name);
                objDataWrapperP.AddParameter("@DEL_FLAG", delFlag);
                objDataWrapperP.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.Output);


                ResultSet = objDataWrapperP.ExecuteNonQuery(strStoredProcP);
                ResultSet = (int)objDataWrapperP.GetParameterValue("@RETURN_VALUE");
                objDataWrapperP.ClearParameteres();
                objDataWrapperP.CommitTransaction(DataWrapper.CloseConnection.YES);
                //ResultSet = 1;

                return ResultSet;
            }
            catch (Exception ex)
            {
                objDataWrapperP.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while IMPORT. PROC_PAGNET_IMPORT", ex));
            }
            finally
            {
                objDataWrapperP.Dispose();
            }


        }

       



        #endregion 

        #region FUNCTION TO GENERATE TEXT FILE



        public string Customer_Refund()
        {

            InsertLog("Pagnet", "Going to Fetch records for customer refund", DateTime.Now, DateTime.Now, "Success", ""); 
            DataSet ds = getData("PROC_RefundData");
            InsertLog("Pagnet", "Fetch records " + ds.Tables[0].Rows.Count + " for customer refund."  , DateTime.Now, DateTime.Now, "Success", ""); 
            strBuild = new StringBuilder();
            string strTemp = "";
            string strTemp2 = "";
            int i = 0;
            string[] strArrTemp;
            string strMin = string.Empty ;
            string date = "";
            System.IO.StreamWriter file = null;

            try
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        
                        strTemp = "";
                        strFullLengthString = "";
                        strTemp2 = "";
                        strFullLengthString = dr["Interface code"].ToString().Length < 3 ? dr["Interface code"].ToString().PadLeft(3, '0') : dr["Interface code"].ToString().Substring(0, 3);

                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sequence of record"].ToString().PadLeft(10, '0').ToString();
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Type"].ToString().Length < 1 ? dr["Beneficiary Type"].ToString().PadRight(1, ' ') : dr["Beneficiary Type"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["FOREIGN"].ToString().PadLeft(1, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Class"].ToString().PadLeft(3, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary name"].ToString().Length < 60 ? dr["Beneficiary name"].ToString().PadRight(60, ' ') : dr["Beneficiary name"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Beneficiary ID
                        strTemp = dr["Beneficiary ID"].ToString();
                        //strTemp2 = "";
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }

                        }
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Beneficiary foreign ID"].ToString().Length < 14 ? dr["Beneficiary foreign ID"].ToString().PadRight(14, ' ') : dr["Beneficiary foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        //ADDRESS
                        strTemp = dr["Beneficiary Address (street name)"].ToString().Length < 30 ? dr["Beneficiary Address (street name)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (street name)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        //NUMBER
                        strTemp = dr["Beneficiary Address (number)"].ToString().Length < 5 ? dr["Beneficiary Address (number)"].ToString().PadRight(5, ' ') : dr["Beneficiary Address (number)"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (complement)"].ToString().Length < 10 ? dr["Beneficiary Address (complement)"].ToString().PadRight(10, ' ') : dr["Beneficiary Address (complement)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (district)"].ToString().Length < 30 ? dr["Beneficiary Address (district)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (district)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (state)"].ToString().Length < 2 ? dr["Beneficiary Address (state)"].ToString().PadRight(2, ' ') : dr["Beneficiary Address (state)"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (city)"].ToString().Length < 30 ? dr["Beneficiary Address (city)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (city)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (zip code)"].ToString().Length < 10 ? dr["Beneficiary Address (zip code)"].ToString().PadLeft(10, '0') : dr["Beneficiary Address (zip code)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary e-mail address"].ToString().Length < 60 ? dr["Beneficiary e-mail address"].ToString().PadRight(60, ' ') : dr["Beneficiary e-mail address"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Number"].ToString().Length < 5 ? dr["Beneficiary Bank Number"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch"].ToString().Length < 10 ? dr["Beneficiary Bank Branch"].ToString().PadLeft(10, '0') : dr["Beneficiary Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Beneficiary Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Beneficiary Bank Branch Verifier Digit"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        
                        strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                        strFullLengthString = strFullLengthString + strTemp;


                        //Beneficiary Bank Account Verifier Digit
                        strTemp = dr["Beneficiary Bank Account Verifier Digit"].ToString();
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');

                                strTemp = "";
                                strTemp = strArrTemp[1];
                            }
                            else
                            {
                                strTemp = "";
                            }
                        
                        strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);                       
                        strFullLengthString = strFullLengthString + strTemp;


                        strTemp = dr["Beneficiary Bank Account type"].ToString().Length < 2 ? dr["Beneficiary Bank Account type"].ToString().PadLeft(2, '0') : dr["Beneficiary Bank Account type"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Account Currency"].ToString().Length < 5 ? dr["Beneficiary Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Account Currency"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação IRRF"].ToString().Length < 2 ? dr["Cód Tributação IRRF"].ToString().PadLeft(2, '0') : dr["Cód Tributação IRRF"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Natureza do Rendimento"].ToString().Length < 5 ? dr["Natureza do Rendimento"].ToString().PadLeft(5, '0') : dr["Natureza do Rendimento"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula ISS ?"].ToString().Length < 1 ? dr["Calcula ISS ?"].ToString().PadLeft(1, '0') : dr["Calcula ISS ?"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula INSS ?"].ToString().Length < 1 ? dr["Calcula INSS ?"].ToString().PadLeft(1, '0') : dr["Calcula INSS ?"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação INSS"].ToString().Length < 2 ? dr["Cód Tributação INSS"].ToString().PadLeft(2, '0') : dr["Cód Tributação INSS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação CSLL"].ToString().Length < 2 ? dr["Cód Tributação CSLL"].ToString().PadLeft(2, '0') : dr["Cód Tributação CSLL"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação COFINS"].ToString().Length < 2 ? dr["Cód Tributação COFINS"].ToString().PadLeft(2, '0') : dr["Cód Tributação COFINS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação PIS"].ToString().Length < 2 ? dr["Cód Tributação PIS"].ToString().PadLeft(2, '0') : dr["Cód Tributação PIS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No de Dependentes"].ToString().Length < 2 ? dr["No de Dependentes"].ToString().PadLeft(2, '0') : dr["No de Dependentes"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No PIS"].ToString().Length < 11 ? dr["No PIS"].ToString().PadLeft(11, '0') : dr["No PIS"].ToString().Substring(0,11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Inscrição Municipal"].ToString().Length < 11 ? dr["Inscrição Municipal"].ToString().PadLeft(11, '0') : dr["Inscrição Municipal"].ToString().Substring(0,11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Número interno do corretor"].ToString().Length < 10 ? dr["Número interno do corretor"].ToString().PadLeft(10, '0') : dr["Número interno do corretor"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["CBO (Classific Brasileira Ocupação)"].ToString().Length < 10 ? dr["CBO (Classific Brasileira Ocupação)"].ToString().PadRight(10, ' ') : dr["CBO (Classific Brasileira Ocupação)"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Código SUSEP"].ToString().Length < 14 ? dr["Código SUSEP"].ToString().PadLeft(14, '0') : dr["Código SUSEP"].ToString().Substring(0,14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Funcionário"].ToString().Length < 10 ? dr["No do Funcionário"].ToString().PadLeft(10, '0') : dr["No do Funcionário"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód da Filial"].ToString().Length < 15 ? dr["Cód da Filial"].ToString().PadRight(15, ' ') : dr["Cód da Filial"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód do Centro de Custo"].ToString().Length < 15 ? dr["Cód do Centro de Custo"].ToString().PadRight(15, ' ') : dr["Cód do Centro de Custo"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["DATE OF BIRTH"].ToString().Length < 8 ? dr["DATE OF BIRTH"].ToString().PadLeft(8, '0') : dr["DATE OF BIRTH"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Party Type"].ToString().Length < 1 ? dr["Payee Party Type"].ToString().PadRight(1, ' ') : dr["Payee Party Type"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Foreign2"].ToString().Length < 1 ? dr["Foreign2"].ToString().PadLeft(1, '0') : dr["Foreign2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Payee Class"].ToString().Length < 3 ? dr["Payee Class"].ToString().PadLeft(3, '0') : dr["Payee Class"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Name"].ToString().Length < 60 ? dr["Payee Name"].ToString().PadRight(60, ' ') : dr["Payee Name"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // + dr["Payee ID (CPF or CNPJ)"].ToString().PadRight(14, ' ')
                        strTemp = dr["Payee ID (CPF or CNPJ)"].ToString();
                        //strTemp2 = "";
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                        }
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Payee foreign ID"].ToString().Length < 14 ? dr["Payee foreign ID"].ToString().PadRight(14, ' ') : dr["Payee foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Number"].ToString().Length < 5 ? dr["Payee Bank Number"].ToString().PadLeft(5, '0') : dr["Payee Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch"].ToString().Length < 10 ? dr["Payee Bank Branch"].ToString().PadLeft(10, '0') : dr["Payee Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Payee Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Payee Bank Branch Verifier Digit"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;                       


                        strTemp = dr["Payee Bank Account no."].ToString().Length < 12 ? dr["Payee Bank Account no."].ToString().PadLeft(12, '0') : dr["Payee Bank Account no."].ToString().Substring(0, 12);
                        strFullLengthString = strFullLengthString + strTemp;

                        //Payee Bank Account Verifier digit
                        strTemp = dr["Payee Bank Account no."].ToString();
                        if (strTemp.IndexOf('-') > 0)
                        {
                            strArrTemp = strTemp.Split('-');

                            strTemp = "";
                            strTemp = strArrTemp[1];

                        }
                        else
                        {
                            strTemp = "";
                        }
                        strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);                        
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account Type"].ToString().Length < 2 ? dr["Payee Bank Account Type"].ToString().PadLeft(2, '0') : dr["Payee Bank Account Type"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account Currency"].ToString().Length < 5 ? dr["Payee Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Payee Bank Account Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["PAYMENT ID"].ToString().Length < 25 ? dr["PAYMENT ID"].ToString().PadRight(25, ' ') : dr["PAYMENT ID"].ToString().Substring(0, 25);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Code"].ToString().Length < 5 ? dr["Carrier Code"].ToString().PadLeft(5, '0') : dr["Carrier Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Policy Branch Code"].ToString().Length < 5 ? dr["Carrier Policy Branch Code"].ToString().PadLeft(5, '0') : dr["Carrier Policy Branch Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["EVENT CODE"].ToString().PadLeft(5, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Operation Type"].ToString().PadLeft(3, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment Method"].ToString().PadLeft(3, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number"].ToString().Length < 20 ? dr["Document number"].ToString().PadLeft(20, '0') : dr["Document number"].ToString().Substring(0,20);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Série da Nota Fiscal"].ToString().PadRight(5, ' ');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice issuance date"].ToString().Length < 8 ? dr["Invoice issuance date"].ToString().PadRight(8, ' ') : dr["Invoice issuance date"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice due date"].ToString().Length < 8 ? dr["Invoice due date"].ToString().PadRight(8, ' ') : dr["Invoice due date"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Currency"].ToString().Length < 5 ? dr["Policy Currency"].ToString().PadLeft(5, '0') : dr["Policy Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Exchange rate
                        strTemp = dr["Exchange rate"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.Length < 15 ? strTemp.PadLeft(15, '0') : strTemp.Substring(0,15);
                        }
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Refund amount sign"].ToString().Length < 1 ? dr["Refund amount sign"].ToString().PadRight(1, ' ') : dr["Refund amount sign"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Refund amount                       
                        strTemp = dr["Refund amount"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento IR"].ToString().PadRight(1, ' ');
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento IR                       
                        strTemp = dr["Valor Isento IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável IR"].ToString().Length < 1 ? dr["Sinal do Valor Tributável IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável IR                       
                        strTemp = dr["Valor Tributável IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento ISS"].ToString().Length < 1 ? dr["Sinal do Valor Isento ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento ISS"].ToString().Substring(0,1); 
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento ISS                     
                        strTemp = dr["Valor Isento ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0'): strTemp.Substring(0,18);
                        //strTemp = dr["Valor Isento ISS"].ToString().PadLeft(2, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável ISS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável ISS                       
                        strTemp = dr["Valor Tributável ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento INSS"].ToString().Length < 1 ?  dr["Sinal do Valor Isento INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento INSS                       
                        strTemp = dr["Valor Isento INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável INSS"].ToString().Length < 1 ?   dr["Sinal do Valor Tributável INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Tributável INSS                       
                        strTemp = dr["Valor Tributável INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Length < 1 ?  dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Isento CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável CSLL/COFINS/PIS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável CSLL/COFINS/PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável CSLL/COFINS/PIS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Tributável CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR"].ToString().Length < 1 ? dr["Sinal do Valor IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor IR                       
                        strTemp = dr["Valor IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS"].ToString().Length < 1 ? dr["Sinal do Valor ISS"].ToString().PadRight(1, ' '): dr["Sinal do Valor ISS"].ToString().Substring(0,1);

                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Sinal do Valor Desconto"].ToString().Length < 1 ? dr["Sinal do Valor Desconto"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Desconto"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Desconto                       
                        strTemp = dr["Valor Desconto"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18); 
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Multa"].ToString().Length < 1 ? dr["Sinal do Valor Multa"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Multa"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Multa                       
                        strTemp = dr["Valor Multa"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["REFUND PAYMENT DESCRIPTION"].ToString().Length < 60 ? dr["REFUND PAYMENT DESCRIPTION"].ToString().PadRight(60, ' ') : dr["REFUND PAYMENT DESCRIPTION"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Branch Code"].ToString().Length < 15 ?  dr["Policy Branch Code"].ToString().PadRight(15, ' ') : dr["Policy Branch Code"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Profit Center code"].ToString().Length < 15 ? dr["Profit Center code"].ToString().PadLeft(15, '0') : dr["Profit Center code"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR1"].ToString().Length < 15 ? dr["A_DEFINIR1"].ToString().PadRight(15, ' ') : dr["A_DEFINIR1"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR2"].ToString().Length < 15 ? dr["A_DEFINIR2"].ToString().PadRight(15, ' ') : dr["A_DEFINIR2"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Accounting LOB"].ToString().Length < 15 ? dr["Policy Accounting LOB"].ToString().PadRight(15, ' ') : dr["Policy Accounting LOB"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR3"].ToString().Length < 15 ? dr["A_DEFINIR3"].ToString().PadRight(15, ' ') : dr["A_DEFINIR3"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Apólice"].ToString().Length < 15 ? dr["Apólice"].ToString().PadRight(15, ' ') : dr["Apólice"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Apólice (cont)"].ToString().Length < 10 ? dr["Apólice (cont)"].ToString().PadRight(10, ' ') : dr["Apólice (cont)"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Endorsement number"].ToString().Length < 5 ? dr["Endorsement number"].ToString().PadLeft(5, '0') : dr["Endorsement number"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Parcela"].ToString().Length < 5 ? dr["Parcela"].ToString().PadRight(5, ' ') : dr["Parcela"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR4"].ToString().Length < 15 ? dr["A_DEFINIR4"].ToString().PadRight(15, ' ') : dr["A_DEFINIR4"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR5"].ToString().Length < 10 ? dr["A_DEFINIR5"].ToString().PadRight(10, ' ') : dr["A_DEFINIR5"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data de quitação da parcela"].ToString().Length < 15 ? dr["Data de quitação da parcela"].ToString().PadRight(15, ' ') : dr["Data de quitação da parcela"].ToString().Substring(0,15); 
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Tomador/ Descrição"].ToString().Length < 60 ? dr["Tomador/ Descrição"].ToString().PadRight(60, ' ') : dr["Tomador/ Descrição"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Prêmio                       
                        strTemp = dr["Prêmio"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        // % Comissão                       
                        strTemp = dr["% Comissão"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A115                       
                        strTemp = dr["A_DEFINIR6"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A116                       
                        strTemp = dr["A_DEFINIR7"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A117                       
                        strTemp = dr["A_DEFINIR8"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Tipo de Movimento"].ToString().Length < 1 ? dr["Tipo de Movimento"].ToString().PadLeft(1, '0'): dr["Tipo de Movimento"].ToString().Substring(0,1) ;
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data do Pagamento"].ToString().Length < 8 ? dr["Data do Pagamento"].ToString().PadLeft(8, '0') : dr["Data do Pagamento"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Pago"].ToString().Length < 1 ? dr["Sinal do Valor Pago"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Pago"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A121                       
                        strTemp = dr["Valor Pago"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                            
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Moeda do pagamento"].ToString().Length < 5 ? dr["Moeda do pagamento"].ToString().PadLeft(5, '0') : dr["Moeda do pagamento"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Código do Banco"].ToString().Length < 5 ? dr["Código do Banco"].ToString().PadLeft(5, '0') : dr["Código do Banco"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Agência"].ToString().Length < 10 ? dr["No da Agência"].ToString().PadLeft(10, '0') : dr["No da Agência"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Conta Corrente"].ToString().Length < 12 ? dr["No da Conta Corrente"].ToString().PadLeft(12, '0') : dr["No da Conta Corrente"].ToString().Substring(0,12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Cheque"].ToString().Length < 15 ? dr["No do Cheque"].ToString().PadLeft(15, '0') : dr["No do Cheque"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR_2"].ToString().Length < 1 ? dr["Sinal do Valor IR_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR_2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor IR                       
                        strTemp = dr["Valor IR_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS_2"].ToString().Length < 1 ? dr["Sinal do Valor ISS_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS_2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor ISS                       
                        strTemp = dr["Valor ISS_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor INSS"].ToString().Length < 1 ? dr["Sinal do Valor INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A132                       
                        strTemp = dr["Valor INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor CSLL"].ToString().Length < 1 ? dr["Sinal do Valor CSLL"].ToString().PadRight(1, ' ') : dr["Sinal do Valor CSLL"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // A134                       
                        strTemp = dr["Valor CSLL"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor COFINS"].ToString().Length < 1 ? dr["Sinal do Valor COFINS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor COFINS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A136                       
                        strTemp = dr["Valor COFINS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor PIS"].ToString().Length < 1 ? dr["Sinal do Valor PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor PIS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A138                       
                        strTemp = dr["Valor PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cod Ocorrencia"].ToString().Length < 3 ? dr["Cod Ocorrencia"].ToString().PadLeft(3, '0') : dr["Cod Ocorrencia"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Motivo do Cancelamento do Cheque"].ToString().Length < 60 ? dr["Motivo do Cancelamento do Cheque"].ToString().PadRight(60, ' ') : dr["Motivo do Cancelamento do Cheque"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Forma de Pagamento"].ToString().Length < 3 ? dr["Forma de Pagamento"].ToString().PadLeft(3, '0') : dr["Forma de Pagamento"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Código do Banco Pagador"].ToString().Length < 5 ? dr["Código do Banco Pagador"].ToString().PadLeft(5, '0') : dr["Código do Banco Pagador"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Agência_2"].ToString().Length < 10 ? dr["No da Agência_2"].ToString().PadLeft(10, '0') : dr["No da Agência_2"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Conta Corrente_2"].ToString().Length < 12 ? dr["No da Conta Corrente_2"].ToString().PadLeft(12, '0') : dr["No da Conta Corrente_2"].ToString().Substring(0,12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Taxa conversão"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(15, '0');
                        }

                        strFullLengthString = strFullLengthString + strTemp;

                        //NEW COLUMN ADDED
                        strTemp = dr["INCONSISTENCY_1"].ToString().Length < 5 ? dr["INCONSISTENCY_1"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_1"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_2"].ToString().Length < 5 ? dr["INCONSISTENCY_2"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_2"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_3"].ToString().Length < 5 ? dr["INCONSISTENCY_3"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_3"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_4"].ToString().Length < 5 ? dr["INCONSISTENCY_4"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_4"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_5"].ToString().Length < 5 ? dr["INCONSISTENCY_5"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_5"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;


                        i = i + 1;

                        string dd = DateTime.Now.Day.ToString();
                        string mon = DateTime.Now.Month.ToString();
                        string yy = DateTime.Now.Year.ToString();
                        string hh = DateTime.Now.Hour.ToString();
                        string min = DateTime.Now.Minute.ToString();

                        dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
                        mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();
                        hh = hh.Length < 2 ? hh.PadLeft(2, '0') : hh.ToString();
                        min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                        if (i != 1)
                            min = strMin;
                        else
                            min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                        date = dd + mon + yy + hh + min;

                        if (i == 1)
                        {
                            strMin = min;
                        }

                        if (i == 1 )//First Record
                        {   
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString()+", Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for customer refund.", DateTime.Now, DateTime.Now, "Success", ""); 

                               
                            }
                            catch(Exception ex)
                            {
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + ", Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for customer refund.", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message); 
                                continue;
                            }

                            try
                            {

                                if (File.Exists(ConfigurationSettings.AppSettings["FilePathCustRefund"] + "Customer_Refund" + date + ".txt"))
                                {
                                    File.Delete(ConfigurationSettings.AppSettings["FilePathCustRefund"] + "Customer_Refund" + date + ".txt");
                                }
                               
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCustRefund"] + "Customer_Refund" + date + ".txt", true, Encoding.ASCII);

                                InsertLog("Pagnet", "File created name: Customer_Refund" + date + ".txt", DateTime.Now, DateTime.Now, "Success", ""); 

                                strBuild.AppendLine("File created for Customer_Refund Successfully");

                                if (ds.Tables[0].Rows.Count == 1)
                                  
                                    //Changes by naveen, tfs 2419
                                    // file.Write(strFullLengthString);
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                else
                                    // file.Write(strFullLengthString);
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));

                                InsertLog("Pagnet", "Record inserted in File: Customer_Refund" + date + ".txt, Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() , DateTime.Now, DateTime.Now, "Success", ""); 


                                file.Close();

                                
                                //Insert Export file name
                                InsertPagnetExportFiles("PROC_INSERT_PAGNET_EXPORT_FILES", ds.Tables[0].Rows[i - 1]["Interface code"].ToString(), "Customer_Refund" + date + ".txt", FileStatus.Generated.ToString(), Dns.GetHostName());
                                InsertLog("Pagnet", "File name: " + "Customer_Refund" + date + ".txt " + " Inserted in PAGNET_EXPORT_FILES " , DateTime.Now, DateTime.Now, "Success", ""); 

                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "Customer_Refund" + date + ".txt");
                                //InsertLog("Pagnet", "Table PAGNET_EXPORT_FILES updated for payment id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + ".", DateTime.Now, DateTime.Now, "Success", ""); 

                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record inserted in File: Customer_Refund" + date + ".txt, Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Faile", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                               
                                file.Dispose();
                            }
                           
                        }
                        else if (i == ds.Tables[0].Rows.Count)//Last record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                InsertLog("Pagnet", "Update flag in ACT_CUSTOMER_OPEN_ITEMS " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for customer refund.", DateTime.Now, DateTime.Now, "Success", ""); 
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + ", PAYMENT ID: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for customer refund.", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message); 
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {                               
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCustRefund"] + "Customer_Refund" + date + ".txt", true, Encoding.ASCII);

                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));

                                file.Close();
                                InsertLog("Pagnet", "Record inserted in File: Customer_Refund" + date + ".txt, Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", ""); 
                                

                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "Customer_Refund" + date + ".txt");
                            }
                            catch (Exception ex)
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }


                        }
                        else // Middle Record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + " PAYMENT ID: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for customer refund.", DateTime.Now, DateTime.Now, "Success", ""); 
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + " PAYMENT ID: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for customer refund.", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message); 
                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {

                                
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCustRefund"] + "Customer_Refund" + date + ".txt", true, Encoding.ASCII);
                                
                               
                                // changes by naveen , tfs 2419
                                // file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                file.Close();

                                InsertLog("Pagnet", "Record inserted in File: Customer_Refund" + date + ".txt, Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", ""); 
                                
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "Customer_Refund" + date + ".txt");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record inserted in File: Customer_Refund" + date + ".txt, Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message );
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }

                        }                                    

                    }
                    strBuild.AppendLine("There inserted successfully for refund. ");
                    return strBuild.ToString(); 
                    
                  }
                    
                    
                else
                {
                    strBuild.AppendLine("There is no record to insert for refund.");
                   return strBuild.ToString();
                  
                }
            }

            catch (Exception ex)
            {
                throw ex;                
            }
            finally
            {
                ds.Dispose();
            }

        }

        public string Broker_Commission()
        {
            InsertLog("Pagnet", "Going to Fetch records for Broker Commission", DateTime.Now, DateTime.Now, "Success", ""); 
            DataSet ds = getData("PROC_COMMISSION");
            InsertLog("Pagnet", "Fetch records " + ds.Tables[0].Rows.Count + " for Broker Commission.", DateTime.Now, DateTime.Now, "Success", ""); 
            strBuild = new StringBuilder();
            string strTemp = "";
            string strTemp2 = "";
            int i = 0;
            string[] strArrTemp;
            string dd = "";
            string mon = "";
            string yy = "";
            string date = "";
            System.IO.StreamWriter file = null;
            string strMin = string.Empty;
            int SequenceNo = 0;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        strTemp = "";
                        SequenceNo += 1;
                        strFullLengthString = "";
                        strTemp2 = "";
                        strFullLengthString = dr["Interface code"].ToString().Length < 3 ? dr["Interface code"].ToString().PadLeft(3, '0') : dr["Interface code"].ToString().Substring(0, 3);
                        strFullLengthString = strFullLengthString + strTemp;
                       strTemp = dr["Sequence of record"].ToString().Length < 10 ? dr["Sequence of record"].ToString().PadLeft(10, '0').ToString() : dr["Sequence of record"].ToString().Substring(0, 10);
                        //strTemp = SequenceNo.ToString().Length < 10 ? SequenceNo.ToString().PadLeft(10, '0') : SequenceNo.ToString().Substring(1, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Type"].ToString().Length < 1 ? dr["Beneficiary Type"].ToString().PadRight(1, ' ') : dr["Beneficiary Type"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["FOREIGN"].ToString().Length < 1 ? dr["FOREIGN"].ToString().PadLeft(1, '0') : dr["FOREIGN"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        //Beneficiary Class
                        strTemp = dr["EVENT CODE"].ToString();
                        if (strTemp == "01030")
                        {
                            strTemp = "004";
                        }
                        else
                        {
                            strTemp = "001";
                        }

                        //strTemp = dr["Beneficiary Class"].ToString().Length < 3 ? dr["Beneficiary Class"].ToString().PadLeft(3, '0') : dr["Beneficiary Class"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary name"].ToString().Length < 60 ? dr["Beneficiary name"].ToString().PadRight(60, ' ') : dr["Beneficiary name"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Beneficiary ID
                        strTemp = dr["Beneficiary ID"].ToString();
                        //strTemp2 = "";
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }

                        }
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Beneficiary foreign ID"].ToString().Length < 14 ? dr["Beneficiary foreign ID"].ToString().PadRight(14, ' ') : dr["Beneficiary foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        //ADDRESS
                        strTemp = dr["Beneficiary Address (street name)"].ToString().Length < 30 ? dr["Beneficiary Address (street name)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (street name)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;


                        // NUMBER                        
                        strTemp = strTemp.Length < 5 ? strTemp.PadRight(5, ' ') : strTemp.Substring(0, 5);
                        strTemp = dr["Beneficiary Address (number)"].ToString().Length < 5 ? dr["Beneficiary Address (number)"].ToString().PadRight(5, ' ') : dr["Beneficiary Address (number)"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (complement)"].ToString().Length < 10 ? dr["Beneficiary Address (complement)"].ToString().PadRight(10, ' ') : dr["Beneficiary Address (complement)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (district)"].ToString().Length < 30 ? dr["Beneficiary Address (district)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (district)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (state)"].ToString().Length < 2 ? dr["Beneficiary Address (state)"].ToString().PadRight(2, ' ') : dr["Beneficiary Address (state)"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (city)"].ToString().Length < 30 ? dr["Beneficiary Address (city)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (city)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (zip code)"].ToString().Length < 10 ? dr["Beneficiary Address (zip code)"].ToString().PadLeft(10, '0') : dr["Beneficiary Address (zip code)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary e-mail address"].ToString().Length < 60 ? dr["Beneficiary e-mail address"].ToString().PadRight(60, ' ') : dr["Beneficiary e-mail address"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;


                        strTemp = dr["Beneficiary Bank Number"].ToString().Length < 5 ? dr["Beneficiary Bank Number"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch"].ToString().Length < 10 ? dr["Beneficiary Bank Branch"].ToString().PadLeft(10, '0') : dr["Beneficiary Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Beneficiary Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Beneficiary Bank Branch Verifier Digit"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        //Beneficiary Bank Account number
                        try
                        {
                            if (!String.IsNullOrEmpty(dr["Beneficiary Bank Account number"].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else
                                {
                                    strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());
                                }

                            }

                            strTemp = strTemp.Length < 12 ? strTemp.PadLeft(12, '0') : strTemp.Substring(0, 12);
                            // strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        catch (Exception ex)
                        {
                            strBuild.AppendLine("Payment ID : "+ dr["Payment ID"].ToString() + ", Cause :"+ ex.ToString());
                            continue;
                        }
                        //Beneficiary Bank Account Verifier Digit
                        try
                        {
                            if (!String.IsNullOrEmpty(dr["Beneficiary Bank Account number"].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());
                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else
                                {
                                    strTemp = "";
                                }
                            }
                            else
                            {
                                strTemp = "";
                            }
                            strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);
                            //strTemp = dr["Beneficiary Bank Account Verifier Digit"].ToString().Length < 2 ? dr["Beneficiary Bank Account Verifier Digit"].ToString().PadRight(2, ' ') : dr["Beneficiary Bank Account Verifier Digit"].ToString().Substring(0,2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        catch (Exception ex)
                        {
                            strBuild.AppendLine("Payment ID : " + dr["Payment ID"].ToString() + ", Cause :" + ex.ToString());
                            continue;
                        }


                        strTemp = dr["Beneficiary Bank Account type"].ToString().Length < 2 ? dr["Beneficiary Bank Account type"].ToString().PadLeft(2, '0') : dr["Beneficiary Bank Account type"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Account Currency"].ToString().Length < 5 ? dr["Beneficiary Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Account Currency"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação IRRF"].ToString().Length < 2 ? dr["Cód Tributação IRRF"].ToString().PadLeft(2, '0') : dr["Cód Tributação IRRF"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Natureza do Rendimento"].ToString().Length < 5 ? dr["Natureza do Rendimento"].ToString().PadLeft(5, '0') : dr["Natureza do Rendimento"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula ISS"].ToString().Length < 1 ? dr["Calcula ISS"].ToString().PadLeft(1, '0') : dr["Calcula ISS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula INSS"].ToString().Length < 1 ? dr["Calcula INSS"].ToString().PadLeft(1, '0') : dr["Calcula INSS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação INSS"].ToString().Length < 2 ? dr["Cód Tributação INSS"].ToString().PadLeft(2, '0') : dr["Cód Tributação INSS"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação CSLL"].ToString().Length < 2 ? dr["Cód Tributação CSLL"].ToString().PadLeft(2, '0') : dr["Cód Tributação CSLL"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação COFINS"].ToString().Length < 2 ? dr["Cód Tributação COFINS"].ToString().PadLeft(2, '0') : dr["Cód Tributação COFINS"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação PIS"].ToString().Length < 2 ? dr["Cód Tributação PIS"].ToString().PadLeft(2, '0') : dr["Cód Tributação PIS"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No de Dependentes"].ToString().Length < 2 ? dr["No de Dependentes"].ToString().PadLeft(2, '0') : dr["No de Dependentes"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No PIS"].ToString().Length < 11 ? dr["No PIS"].ToString().PadLeft(11, '0') : dr["No PIS"].ToString().Substring(0, 11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Inscrição Municipal"].ToString().Length < 11 ? dr["Inscrição Municipal"].ToString().PadLeft(11, '0') : dr["Inscrição Municipal"].ToString().Substring(0, 11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Número interno do corretor"].ToString().Length < 10 ? dr["Número interno do corretor"].ToString().PadLeft(10, '0') : dr["Número interno do corretor"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["CBO (Classific Brasileira Ocupação)"].ToString().Length < 10 ? dr["CBO (Classific Brasileira Ocupação)"].ToString().PadRight(10, ' ') : dr["CBO (Classific Brasileira Ocupação)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Broker SUSEP Code"].ToString().Length < 14 ? dr["Broker SUSEP Code"].ToString().PadLeft(14, '0') : dr["Broker SUSEP Code"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Funcionário"].ToString().Length < 10 ? dr["No do Funcionário"].ToString().PadLeft(10, '0') : dr["No do Funcionário"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód da Filial"].ToString().Length < 15 ? dr["Cód da Filial"].ToString().PadRight(15, ' ') : dr["Cód da Filial"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód do Centro de Custo"].ToString().Length < 15 ? dr["Cód do Centro de Custo"].ToString().PadRight(15, ' ') : dr["Cód do Centro de Custo"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data de Nascimento"].ToString().Length < 8 ? dr["Data de Nascimento"].ToString().PadLeft(8, '0') : dr["Data de Nascimento"].ToString().Substring(0, 8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Party Type"].ToString().Length < 1 ? dr["Payee Party Type"].ToString().PadRight(1, ' ') : dr["Payee Party Type"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Foreign2"].ToString().Length < 1 ? dr["Foreign2"].ToString().PadLeft(1, '0') : dr["Foreign2"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        //Payee Class Class
                        strTemp = dr["EVENT CODE"].ToString();
                        if (strTemp == "01030")
                        {
                            strTemp = "004";
                        }
                        else
                        {
                            strTemp = "001";
                        }
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Name"].ToString().Length < 60 ? dr["Payee Name"].ToString().PadRight(60, ' ') : dr["Payee Name"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // + dr["Payee ID (CPF or CNPJ)"].ToString().PadRight(14, ' ')
                        strTemp = dr["Payee ID (CPF or CNPJ)"].ToString();
                        //strTemp2 = "";
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                        }
                        strTemp = strTemp.Length < 14 ? strTemp.PadRight(14, ' ') : strTemp.Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Payee foreign ID"].ToString().Length < 14 ? dr["Payee foreign ID"].ToString().PadRight(14, ' ') : dr["Payee foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Number"].ToString().Length < 5 ? dr["Payee Bank Number"].ToString().PadLeft(5, '0') : dr["Payee Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch"].ToString().Length < 10 ? dr["Payee Bank Branch"].ToString().PadLeft(10, '0') : dr["Payee Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Payee Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Payee Bank Branch Verifier Digit"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        try
                        {
                            if (!String.IsNullOrEmpty(dr["Payee Bank Account no."].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Payee Bank Account no."].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else
                                {
                                    strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());
                                }

                            }

                            strTemp = strTemp.Length < 12 ? strTemp.PadLeft(12, '0') : strTemp.Substring(0, 12);

                            //strTemp = dr["Payee Bank Account no."].ToString().Length < 12 ? dr["Payee Bank Account no."].ToString().PadLeft(12, '0') : dr["Payee Bank Account no."].ToString().Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        catch (Exception ex)
                        {
                            strBuild.AppendLine("Payment ID : " + dr["Payment ID"].ToString() + ", Cause :" + ex.ToString());
                            //return strBuild.ToString();
                            continue;
                        }

                        //Payee Bank Account Verifier digit

                        try
                        {
                            if (!String.IsNullOrEmpty(dr["Payee Bank Account no."].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Payee Bank Account no."].ToString());
                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else
                                {
                                    strTemp = "";
                                }

                            }
                            else
                            {
                                strTemp = "";
                            }

                            strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        catch (Exception ex)
                        {
                            strBuild.AppendLine("Payment ID : " + dr["Payment ID"].ToString() + ", Cause :" + ex.ToString());
                            continue;
                        }

                        strTemp = dr["Payee Bank Account Type"].ToString().Length < 2 ? dr["Payee Bank Account Type"].ToString().PadLeft(2, '0') : dr["Payee Bank Account Type"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account Currency"].ToString().Length < 5 ? dr["Payee Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Payee Bank Account Currency"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["PAYMENT ID"].ToString().Length < 25 ? dr["PAYMENT ID"].ToString().PadRight(25, ' ') : dr["PAYMENT ID"].ToString().Substring(0, 25);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Code"].ToString().Length < 5 ? dr["Carrier Code"].ToString().PadLeft(5, '0') : dr["Carrier Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Policy Branch Code"].ToString().Length < 5 ? dr["Carrier Policy Branch Code"].ToString().PadLeft(5, '0') : dr["Carrier Policy Branch Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["EVENT CODE"].ToString().Length < 5 ? dr["EVENT CODE"].ToString().PadLeft(5, '0') : dr["EVENT CODE"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Operation Type"].ToString().Length < 3 ? dr["Operation Type"].ToString().PadLeft(3, '0') : dr["Operation Type"].ToString().Substring(0, 3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment Method"].ToString().Length < 3 ? dr["Payment Method"].ToString().PadLeft(3, '0') : dr["Payment Method"].ToString().Substring(0, 3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number"].ToString().Length < 20 ? dr["Document number"].ToString().PadLeft(20, '0') : dr["Document number"].ToString().Substring(0, 20);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number - serial number"].ToString().Length < 5 ? dr["Document number - serial number"].ToString().PadRight(5, ' ') : dr["Document number - serial number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice issuance date"].ToString().Length < 8 ? dr["Invoice issuance date"].ToString().PadRight(8, ' ') : dr["Invoice issuance date"].ToString().Substring(0, 8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice due date"].ToString().Length < 8 ? dr["Invoice due date"].ToString().PadRight(8, ' ') : dr["Invoice due date"].ToString().Substring(0, 8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Currency"].ToString().Length < 5 ? dr["Policy Currency"].ToString().PadLeft(5, '0') : dr["Policy Currency"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Exchange rate
                        strTemp = dr["Exchange rate"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.Length < 15 ? strTemp.PadLeft(15, '0') : strTemp.Substring(0, 15);
                        }
                        strTemp = strTemp.Length < 15 ? strTemp.PadLeft(15, '0') : strTemp.Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Commission amount sign"].ToString().Length < 1 ? dr["Commission amount sign"].ToString().PadRight(1, ' ') : dr["Commission amount sign"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Refund amount                       
                        strTemp = dr["Commission amount"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento IR"].ToString().Length < 1 ? dr["Sinal do Valor Isento IR"].ToString().PadRight(1, ' ') : strTemp.Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento IR                       
                        strTemp = dr["Valor Isento IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável IR"].ToString().Length < 1 ? dr["Sinal do Valor Tributável IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável IR"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável IR                       
                        strTemp = dr["Valor Tributável IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento ISS"].ToString().Length < 1 ? dr["Sinal do Valor Isento ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento ISS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento ISS                     
                        strTemp = dr["Valor Isento ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável ISS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável ISS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável ISS                       
                        strTemp = dr["Valor Tributável ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento INSS"].ToString().Length < 1 ? dr["Sinal do Valor Isento INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento INSS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento INSS                       
                        strTemp = dr["Valor Isento INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável INSS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável INSS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Tributável INSS                       
                        strTemp = dr["Valor Tributável INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Length < 1 ? dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Isento CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável"].ToString().Length < 1 ? dr["Sinal do Valor Tributável"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Tributável CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR"].ToString().Length < 1 ? dr["Sinal do Valor IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor IR                       
                        strTemp = dr["Valor IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS"].ToString().Length < 1 ? dr["Sinal do Valor ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Sinal do Valor Desconto"].ToString().Length < 1 ? dr["Sinal do Valor Desconto"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Desconto"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Desconto                       
                        strTemp = dr["Valor Desconto"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Multa"].ToString().Length < 1 ? dr["Sinal do Valor Multa"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Multa"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Multa                       
                        strTemp = dr["Valor Multa"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment Description"].ToString().Length < 60 ? dr["Payment Description"].ToString().PadRight(60, ' ') : dr["Payment Description"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Branch Code"].ToString().Length < 15 ? dr["Policy Branch Code"].ToString().PadRight(15, ' ') : dr["Policy Branch Code"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Profit Center code"].ToString().Length < 15 ? dr["Profit Center code"].ToString().PadLeft(15, '0') : dr["Profit Center code"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR1"].ToString().Length < 15 ? dr["A_DEFINIR1"].ToString().PadRight(15, ' ') : dr["A_DEFINIR1"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR2"].ToString().Length < 15 ? dr["A_DEFINIR2"].ToString().PadRight(15, ' ') : dr["A_DEFINIR2"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Accounting LOB"].ToString().Length < 15 ? dr["Policy Accounting LOB"].ToString().PadRight(15, ' ') : dr["Policy Accounting LOB"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR3"].ToString().Length < 15 ? dr["A_DEFINIR3"].ToString().PadRight(15, ' ') : dr["A_DEFINIR3"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy number (first 15 digits)"].ToString().Length < 15 ? dr["Policy number (first 15 digits)"].ToString().PadRight(15, ' ') : dr["Policy number (first 15 digits)"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Number (remaining digits)"].ToString().Length < 10 ? dr["Policy Number (remaining digits)"].ToString().PadRight(10, ' ') : dr["Policy Number (remaining digits)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Endorsement number"].ToString().Length < 5 ? dr["Endorsement number"].ToString().PadLeft(5, '0') : dr["Endorsement number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Installment number"].ToString().Length < 5 ? dr["Installment number"].ToString().PadRight(5, ' ') : dr["Installment number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR4"].ToString().Length < 15 ? dr["A_DEFINIR4"].ToString().PadRight(15, ' ') : dr["A_DEFINIR4"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR5"].ToString().Length < 10 ? dr["A_DEFINIR5"].ToString().PadRight(10, ' ') : dr["A_DEFINIR5"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Installment payment date"].ToString().Length < 15 ? dr["Installment payment date"].ToString().PadRight(15, ' ') : dr["Installment payment date"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Applicant/ Co Applicant name"].ToString().Length < 60 ? dr["Applicant/ Co Applicant name"].ToString().PadRight(60, ' ') : dr["Applicant/ Co Applicant name"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Prêmio                       
                        strTemp = dr["Premium amount"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;


                        // % Comissão                       
                        strTemp = dr["Commission percentage"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A115                       
                        strTemp = dr["A_DEFINIR6"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A116                       
                        strTemp = dr["A_DEFINIR7"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A117                       
                        strTemp = dr["A_DEFINIR8"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Movement type"].ToString().Length < 1 ? dr["Movement type"].ToString().PadLeft(1, '0') : dr["Movement type"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Commission Payment date"].ToString().Length < 8 ? dr["Commission Payment date"].ToString().PadLeft(8, '0') : dr["Commission Payment date"].ToString().Substring(0, 8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Commission Paid Amount sign"].ToString().Length < 1 ? dr["Commission Paid Amount sign"].ToString().PadRight(1, ' ') : dr["Commission Paid Amount sign"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A121                       
                        strTemp = dr["Commission paid amount"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment currency"].ToString().Length < 5 ? dr["Payment currency"].ToString().PadLeft(5, '0') : dr["Payment currency"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Bank account number"].ToString().Length < 5 ? dr["Bank account number"].ToString().PadLeft(5, '0') : dr["Bank account number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Bank branch code"].ToString().Length < 10 ? dr["Bank branch code"].ToString().PadLeft(10, '0') : dr["Bank branch code"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Conta Corrente"].ToString().Length < 12 ? dr["No da Conta Corrente"].ToString().PadLeft(12, '0') : dr["No da Conta Corrente"].ToString().Substring(0, 12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cheque number"].ToString().Length < 15 ? dr["Cheque number"].ToString().PadLeft(15, '0') : dr["Cheque number"].ToString().Substring(0, 15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR_2"].ToString().Length < 1 ? dr["Sinal do Valor IR_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR_2"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor IR                       
                        strTemp = dr["Valor IR_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS_2"].ToString().Length < 1 ? dr["Sinal do Valor ISS_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS_2"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor INSS"].ToString().PadRight(1, ' ');
                        strFullLengthString = strFullLengthString + strTemp;
                        // A132                       
                        strTemp = dr["Valor INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor CSLL"].ToString().Length < 1 ? dr["Sinal do Valor CSLL"].ToString().PadRight(1, ' ') : dr["Sinal do Valor CSLL"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // A134                       
                        strTemp = dr["Valor CSLL"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor COFINS"].ToString().Length < 1 ? dr["Sinal do Valor COFINS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor COFINS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A136                       
                        strTemp = dr["Valor COFINS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor PIS"].ToString().Length < 1 ? dr["Sinal do Valor PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor PIS"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A138                       
                        strTemp = dr["Valor PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Occurrence code"].ToString().Length < 3 ? dr["Occurrence code"].ToString().PadLeft(3, '0') : dr["Occurrence code"].ToString().Substring(0, 3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cheque cancellation reason"].ToString().Length < 60 ? dr["Cheque cancellation reason"].ToString().PadRight(60, ' ') : dr["Cheque cancellation reason"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment method_2"].ToString().Length < 3 ? dr["Payment method_2"].ToString().PadLeft(3, '0') : dr["Payment method_2"].ToString().Substring(0, 3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Number"].ToString().Length < 5 ? dr["Carrier Bank Number"].ToString().PadLeft(5, '0') : dr["Carrier Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Branch number"].ToString().Length < 10 ? dr["Carrier Bank Branch number"].ToString().PadLeft(10, '0') : dr["Carrier Bank Branch number"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Account number"].ToString().Length < 12 ? dr["Carrier Bank Account number"].ToString().PadLeft(12, '0') : dr["Carrier Bank Account number"].ToString().Substring(0, 12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Exchange rate2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(15, '0');
                        }


                        strFullLengthString = strFullLengthString + strTemp;


                        //NEW COLUMN ADDED
                        strTemp = dr["INCONSISTENCY_1"].ToString().Length < 5 ? dr["INCONSISTENCY_1"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_1"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_2"].ToString().Length < 5 ? dr["INCONSISTENCY_2"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_2"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_3"].ToString().Length < 5 ? dr["INCONSISTENCY_3"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_3"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_4"].ToString().Length < 5 ? dr["INCONSISTENCY_4"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_4"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_5"].ToString().Length < 5 ? dr["INCONSISTENCY_5"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_5"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        i = i + 1;

                        dd = DateTime.Now.Day.ToString();
                        mon = DateTime.Now.Month.ToString();
                        yy = DateTime.Now.Year.ToString();
                        string hh = DateTime.Now.Hour.ToString();
                        string min = DateTime.Now.Minute.ToString();
                        
                        dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
                        mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();
                        hh = hh.Length < 2 ? hh.PadLeft(2, '0') : hh.ToString();
                        min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                        //applied the below condition to solve , point 16 of commision file.excel sheet v20
                        if (i != 1)
                            min = strMin;
                        else
                            min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                        date = dd + mon + yy + hh + min;

                        if (i == 1)
                        {
                            strMin = min;
                        }

                        if (i == 1)//First Record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "ACT_AGENCY_STATEMENT_DETAILED", Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                InsertLog("Pagnet", "Update flag in ACT_AGENCY_STATEMENT_DETAILED " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Broker Commission.", DateTime.Now, DateTime.Now, "Success", "");
                                
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Update flag in ACT_AGENCY_STATEMENT_DETAILED " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Broker Commission.", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {
                                strBuild.AppendLine("OUTER Check   existstance of File for Commission");
                                if (File.Exists(ConfigurationSettings.AppSettings["FilePathCommission"] + "Commission" + date + ".txt"))
                                {
                                    strBuild.AppendLine("INNER Check   existstance of File for Commission");
                                    File.Delete(ConfigurationSettings.AppSettings["FilePathCommission"] + "Commission" + date + ".txt");
                                }
                                strBuild.AppendLine("OUTER File create  for Commission Successfully");

                                //Insert in text file

                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCommission"] + "Commission" + date + ".txt", true, Encoding.ASCII);
                                strBuild.AppendLine("INNER File  created for Commission Successfully");
                                if (ds.Tables[0].Rows.Count == 1)
                                    
                                    // changes by naveen , tfs 2419
                                   //file.Write(strFullLengthString);
                                    //file.Write(StrEncodeXML(strFullLengthString));
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                else
                                
                                // changes by naveen , tfs 2419
                               // file.Write(strFullLengthString);
                                //file.Write(StrEncodeXML(strFullLengthString));
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                
                                file.Close();

                                InsertLog("Pagnet", "Record Inserted in " + "Commission" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");
                                                                
                                //Insert Export file name
                                InsertPagnetExportFiles("PROC_INSERT_PAGNET_EXPORT_FILES", ds.Tables[0].Rows[i - 1]["Interface code"].ToString(), "Commission" + date + ".txt", FileStatus.Generated.ToString(), Dns.GetHostName());
                                InsertLog("Pagnet", "Insert file name " + "Commission" + date + ".txt" + " in PAGNET_EXPORT_FILES", DateTime.Now, DateTime.Now, "Success", "");

                                //PROC_UPDATE_PAGNET_EXPORT                                
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "Commission" + date + ".txt");

                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in " + "Commission" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "ACT_AGENCY_STATEMENT_DETAILED", Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }

                        }
                        else if (i == ds.Tables[0].Rows.Count)//Last record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "ACT_AGENCY_STATEMENT_DETAILED", Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                InsertLog("Pagnet", "Update flag in ACT_AGENCY_STATEMENT_DETAILED" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Broker Commission.", DateTime.Now, DateTime.Now, "Success", "");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Update flag in ACT_AGENCY_STATEMENT_DETAILED" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Broker Commission.", DateTime.Now, DateTime.Now, "Fail", "");
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {

                                //inset in text file
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCommission"] + "Commission" + date + ".txt", true, Encoding.ASCII);
                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));

                                file.Close();
                                InsertLog("Pagnet", "Record Inserted in " + "Commission" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");
                                
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "Commission" + date + ".txt");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in" + "Commission" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "ACT_AGENCY_STATEMENT_DETAILED", Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }


                        }
                        else // Middle Record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "ACT_AGENCY_STATEMENT_DETAILED", Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                InsertLog("Pagnet", "Update flag in ACT_AGENCY_STATEMENT_DETAILED" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Broker Commission.", DateTime.Now, DateTime.Now, "Success", "");
                            }
                            catch (Exception ex)
                            {

                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {
                                
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCommission"] + "Commission" + date + ".txt", true, Encoding.ASCII);
                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));

                                file.Close();
                                
                                InsertLog("Pagnet", "Record Inserted in " + "Commission" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");                                
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "Commission" + date + ".txt");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in " + "Commission" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "ACT_AGENCY_STATEMENT_DETAILED", Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }


                        }


                    }


                    strBuild.AppendLine("Record inserted successfully for Broker Commission. ");
                     return strBuild.ToString();
                    


                }
                else
                {
                    strBuild.AppendLine("There is no record to insert");
                      return strBuild.ToString();
                 

                }
            }


            catch (Exception ex)
            {
                throw (new Exception("Broker Commission  " + ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString()));
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                ds.Dispose();
            }
        }


        public string RI_CLAIM()
        {
           // aryParm = new SqlParameter[] { (new SqlParameter("@EVENT_CODE", strEventCode)) };

            InsertLog("Pagnet", "Going to Fetch records for RI_CLAIM", DateTime.Now, DateTime.Now, "Success", ""); 
            DataSet ds = getData("PROC_RI_Claim");
            InsertLog("Pagnet", "Fetch records " + ds.Tables[0].Rows.Count + " for RI_CLAIM", DateTime.Now, DateTime.Now, "Success", ""); 
            strBuild = new StringBuilder();
            string strTemp = "";
            string strTemp2 = "";
            int i = 0;
            string[] strArrTemp;
            string date="";
            string strMin = string.Empty;
            System.IO.StreamWriter file = null;

            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strTemp = "";
                        strFullLengthString = "";
                        strTemp2 = "";
                        strFullLengthString = dr["Interface code"].ToString().Length < 3 ? dr["Interface code"].ToString().PadLeft(3, '0') : dr["Interface code"].ToString().Substring(0, 3);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Sequence of record"].ToString().Length < 10 ? dr["Sequence of record"].ToString().PadLeft(10, '0').ToString() : dr["Sequence of record"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Type"].ToString().Length < 1 ? dr["Beneficiary Type"].ToString().PadRight(1, ' ') : dr["Beneficiary Type"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["FOREIGN"].ToString().Length < 1 ? dr["FOREIGN"].ToString().PadLeft(1, '0') : dr["FOREIGN"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Class"].ToString().Length < 3 ? dr["Beneficiary Class"].ToString().PadLeft(3, '0') : dr["Beneficiary Class"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary name"].ToString().Length < 60 ? dr["Beneficiary name"].ToString().PadRight(60, ' ') : dr["Beneficiary name"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Beneficiary ID
                        strTemp = dr["Beneficiary ID"].ToString();
                        //strTemp2 = "";
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }

                        }
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Beneficiary foreign ID"].ToString().Length < 14 ? dr["Beneficiary foreign ID"].ToString().PadRight(14, ' ') : dr["Beneficiary foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (street name)"].ToString().Length < 30 ? dr["Beneficiary Address (street name)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (street name)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;


                        strTemp = dr["Beneficiary Address (number)"].ToString().Length < 5 ? dr["Beneficiary Address (number)"].ToString().PadRight(5, ' ') : dr["Beneficiary Address (number)"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (complement)"].ToString().Length < 10 ? dr["Beneficiary Address (complement)"].ToString().PadRight(10, ' ') : dr["Beneficiary Address (complement)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (district)"].ToString().Length < 30 ? dr["Beneficiary Address (district)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (district)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (state)"].ToString().Length < 2 ? dr["Beneficiary Address (state)"].ToString().PadRight(2, ' ') : dr["Beneficiary Address (state)"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (city)"].ToString().Length < 30 ? dr["Beneficiary Address (city)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (city)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (zip code)"].ToString().Length < 10 ? dr["Beneficiary Address (zip code)"].ToString().PadLeft(10, '0') : dr["Beneficiary Address (zip code)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary e-mail address"].ToString().Length < 60 ? dr["Beneficiary e-mail address"].ToString().PadRight(60, ' ') : dr["Beneficiary e-mail address"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Number"].ToString().Length < 5 ? dr["Beneficiary Bank Number"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch"].ToString().Length < 10 ? dr["Beneficiary Bank Branch"].ToString().PadLeft(10, '0') : dr["Beneficiary Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Beneficiary Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Beneficiary Bank Branch Verifier Digit"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Account Verifier Digit"].ToString().Length < 2 ? dr["Beneficiary Bank Account Verifier Digit"].ToString().PadRight(2, ' ') : dr["Beneficiary Bank Account Verifier Digit"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;


                        strTemp = dr["Beneficiary Bank Account type"].ToString().Length < 2 ? dr["Beneficiary Bank Account type"].ToString().PadLeft(2, '0') : dr["Beneficiary Bank Account type"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Account Currency"].ToString().Length < 5 ? dr["Beneficiary Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Account Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação IRRF"].ToString().Length < 2 ? dr["Cód Tributação IRRF"].ToString().PadLeft(2, '0') : dr["Cód Tributação IRRF"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Natureza do Rendimento"].ToString().Length < 5 ? dr["Natureza do Rendimento"].ToString().PadLeft(5, '0') : dr["Natureza do Rendimento"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula ISS"].ToString().Length < 1 ? dr["Calcula ISS"].ToString().PadLeft(1, '0') : dr["Calcula ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula INSS"].ToString().Length < 1 ? dr["Calcula INSS"].ToString().PadLeft(1, '0') : dr["Calcula INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação INSS"].ToString().Length < 2 ? dr["Cód Tributação INSS"].ToString().PadLeft(2, '0') : dr["Cód Tributação INSS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação CSLL"].ToString().Length < 2 ? dr["Cód Tributação CSLL"].ToString().PadLeft(2, '0') : dr["Cód Tributação CSLL"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação COFINS"].ToString().Length < 2 ? dr["Cód Tributação COFINS"].ToString().PadLeft(2, '0') : dr["Cód Tributação COFINS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação PIS"].ToString().Length < 2 ? dr["Cód Tributação PIS"].ToString().PadLeft(2, '0') : dr["Cód Tributação PIS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No de Dependentes"].ToString().Length < 2 ? dr["No de Dependentes"].ToString().PadLeft(2, '0') : dr["No de Dependentes"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No PIS"].ToString().Length < 11 ? dr["No PIS"].ToString().PadLeft(11, '0') : dr["No PIS"].ToString().Substring(0,11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Inscrição Municipal"].ToString().Length < 11 ? dr["Inscrição Municipal"].ToString().PadLeft(11, '0') : dr["Inscrição Municipal"].ToString().Substring(0,11);
                        strFullLengthString = strFullLengthString + strTemp;
                         
                        strTemp = dr["Número interno do corretor"].ToString().Length < 10 ? dr["Número interno do corretor"].ToString().PadLeft(10, '0') : dr["Número interno do corretor"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["CBO (Classific Brasileira Ocupação)"].ToString().Length < 10 ? dr["CBO (Classific Brasileira Ocupação)"].ToString().PadRight(10, ' ') : dr["CBO (Classific Brasileira Ocupação)"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;
                         
                        strTemp = dr["Código SUSEP"].ToString().Length < 14 ? dr["Código SUSEP"].ToString().PadLeft(14, '0') : dr["Código SUSEP"].ToString().Substring(0,14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Funcionário"].ToString().Length < 10 ? dr["No do Funcionário"].ToString().PadLeft(10, '0') : dr["No do Funcionário"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód da Filial"].ToString().Length < 15 ? dr["Cód da Filial"].ToString().PadRight(15, ' ') : dr["Cód da Filial"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód do Centro de Custo"].ToString().Length < 15 ? dr["Cód do Centro de Custo"].ToString().PadRight(15, ' ') : dr["Cód do Centro de Custo"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data de Nascimento"].ToString().Length < 8 ? dr["Data de Nascimento"].ToString().PadLeft(8, '0') : dr["Data de Nascimento"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Party Type"].ToString().Length < 1 ?  dr["Payee Party Type"].ToString().PadRight(1, ' ') : dr["Payee Party Type"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Foreign2"].ToString().Length < 1 ? dr["Foreign2"].ToString().PadLeft(1, '0') : dr["Foreign2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Payee Class"].ToString().Length < 3 ? dr["Payee Class"].ToString().PadLeft(3, '0') : dr["Payee Class"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Name"].ToString().Length < 60 ? dr["Payee Name"].ToString().PadRight(60, ' ') : dr["Payee Name"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        
                        strTemp = dr["Payee ID (CPF or CNPJ)"].ToString();
                        //strTemp2 = "";
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                        }
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Payee foreign ID"].ToString().Length < 14 ? dr["Payee foreign ID"].ToString().PadRight(14, ' ') : dr["Payee foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Number"].ToString().Length < 5 ? dr["Payee Bank Number"].ToString().PadLeft(5, '0') : dr["Payee Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch"].ToString().Length < 10 ? dr["Payee Bank Branch"].ToString().PadLeft(10, '0') : dr["Payee Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Payee Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Payee Bank Branch Verifier Digit"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account no."].ToString().Length < 12 ? dr["Payee Bank Account no."].ToString().PadLeft(12, '0') : dr["Payee Bank Account no."].ToString().Substring(0, 12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account Verifier digit"].ToString().Length < 2 ? dr["Payee Bank Account Verifier digit"].ToString().PadRight(2, ' ') : dr["Payee Bank Account Verifier digit"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account Type"].ToString().Length < 2 ? dr["Payee Bank Account Type"].ToString().PadLeft(2, '0') : dr["Payee Bank Account Type"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account Currency"].ToString().Length < 5 ? dr["Payee Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Payee Bank Account Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["PAYMENT ID"].ToString().Length < 25 ? dr["PAYMENT ID"].ToString().PadRight(25, ' ') : dr["PAYMENT ID"].ToString().Substring(0, 25);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Code"].ToString().Length < 5 ? dr["Carrier Code"].ToString().PadLeft(5, '0') : dr["Carrier Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Policy Branch Code"].ToString().Length < 5 ? dr["Carrier Policy Branch Code"].ToString().PadLeft(5, '0') : dr["Carrier Policy Branch Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["EVENT CODE"].ToString().Length < 5 ? dr["EVENT CODE"].ToString().PadLeft(5, '0') : dr["EVENT CODE"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Operation Type"].ToString().Length < 3 ? dr["Operation Type"].ToString().PadLeft(3, '0') : dr["Operation Type"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment Method"].ToString().Length < 3 ? dr["Payment Method"].ToString().PadLeft(3, '0') : dr["Payment Method"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number"].ToString().Length < 20 ? dr["Document number"].ToString().PadLeft(20, '0') : dr["Document number"].ToString().Substring(0,20);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number - serial number"].ToString().Length < 5 ? dr["Document number - serial number"].ToString().PadRight(5, ' ') : dr["Document number - serial number"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice issuance date"].ToString().Length < 8 ? dr["Invoice issuance date"].ToString().PadRight(8, ' ') : dr["Invoice issuance date"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice due date"].ToString().Length < 8 ? dr["Invoice due date"].ToString().PadRight(8, ' ') : dr["Invoice due date"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Currency"].ToString().Length < 5 ? dr["Policy Currency"].ToString().PadLeft(5, '0') : dr["Policy Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Exchange rate
                        strTemp = dr["Exchange rate"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(15, '0');
                        }
                        strTemp = strTemp.Length < 15 ? strTemp.PadLeft(15, '0') : strTemp.Substring(0,15) ;
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Co amount sign"].ToString().Length < 1 ? dr["Co amount sign"].ToString().PadRight(1, ' ') : dr["Co amount sign"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Refund amount                       
                        strTemp = dr["Co amount"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18) ;
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento IR"].ToString().Length < 1 ? dr["Sinal do Valor Isento IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento IR                       
                        strTemp = dr["Valor Isento IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18) ;
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável IR"].ToString().Length < 1 ? dr["Sinal do Valor Tributável IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável IR                       
                        strTemp = dr["Valor Tributável IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18) ;
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento ISS"].ToString().Length < 1 ?  dr["Sinal do Valor Isento ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento ISS                     
                        strTemp = dr["Valor Isento ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18) ;
                        //strTemp = dr["Valor Isento ISS"].ToString().PadLeft(2, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável ISS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável ISS                       
                        strTemp = dr["Valor Tributável ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento INSS"].ToString().Length < 1 ?  dr["Sinal do Valor Isento INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento INSS                       
                        strTemp = dr["Valor Isento INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ?  strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável INSS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Tributável INSS                       
                        strTemp = dr["Valor Tributável INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Length < 1 ? dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Isento CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                           
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável"].ToString().Length < 1 ?  dr["Sinal do Valor Tributável"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Tributável CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ?  strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR"].ToString().Length < 1 ? dr["Sinal do Valor IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor IR                       
                        strTemp = dr["Valor IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ?  strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS"].ToString().Length < 1 ? dr["Sinal do Valor ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ?  strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Desconto"].ToString().PadRight(1, ' ');
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Desconto                       
                        strTemp = dr["Valor Desconto"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp = strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp =dr["Sinal do Valor Multa"].ToString().Length < 1 ?  dr["Sinal do Valor Multa"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Multa"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Multa                       
                        strTemp = dr["Valor Multa"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp = strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                       
                        strTemp = dr["Payment Description"].ToString().Length < 60 ? dr["Payment Description"].ToString().PadRight(60, ' ') : dr["Payment Description"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Branch Code"].ToString().Length < 15 ? dr["Policy Branch Code"].ToString().PadRight(15, ' ') : dr["Policy Branch Code"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Profit Center code"].ToString().Length < 15 ? dr["Profit Center code"].ToString().PadLeft(15, '0') : dr["Profit Center code"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR1"].ToString().Length < 15 ? dr["A_DEFINIR1"].ToString().PadRight(15, ' ') : dr["A_DEFINIR1"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR2"].ToString().Length < 15 ? dr["A_DEFINIR2"].ToString().PadRight(15, ' ') : dr["A_DEFINIR2"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Accounting LOB"].ToString().Length < 15 ? dr["Policy Accounting LOB"].ToString().PadRight(15, ' ') : dr["Policy Accounting LOB"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR3"].ToString().Length < 15 ? dr["A_DEFINIR3"].ToString().PadRight(15, ' ') : dr["A_DEFINIR3"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Apólice"].ToString().Length < 15 ?  dr["Apólice"].ToString().PadRight(15, ' ') : dr["Apólice"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Apólice (cont)"].ToString().Length < 10 ? dr["Apólice (cont)"].ToString().PadRight(10, ' ') : dr["Apólice (cont)"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Endosso"].ToString().Length < 5 ?  dr["Endosso"].ToString().PadLeft(5, '0') : dr["Endosso"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Parcela"].ToString().Length < 5 ? dr["Parcela"].ToString().PadRight(5, ' ') : dr["Parcela"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR4"].ToString().Length < 15 ? dr["A_DEFINIR4"].ToString().PadRight(15, ' ') :dr["A_DEFINIR4"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR5"].ToString().Length < 10 ?  dr["A_DEFINIR5"].ToString().PadRight(10, ' ') : dr["A_DEFINIR5"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data de quitação da parcela"].ToString().Length < 15 ? dr["Data de quitação da parcela"].ToString().PadRight(15, ' ') : dr["Data de quitação da parcela"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Tomador/ Descrição"].ToString().Length < 60 ?  dr["Tomador/ Descrição"].ToString().PadRight(60, ' ') :dr["Tomador/ Descrição"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Prêmio                       
                        strTemp = dr["Prêmio"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                           
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp = strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;


                        // % Comissão                       
                        strTemp = dr["% Comissão"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp = strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A115                       
                        strTemp = dr["A_DEFINIR6"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp = strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A116                       
                        strTemp = dr["A_DEFINIR7"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp = strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A117                       
                        strTemp = dr["A_DEFINIR8"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp = strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Tipo de Movimento"].ToString().Length < 1 ?  dr["Tipo de Movimento"].ToString().PadRight(1, ' ') : dr["Tipo de Movimento"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data do Pagamento"].ToString().Length < 8 ? dr["Data do Pagamento"].ToString().PadLeft(8, '0') : dr["Data do Pagamento"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Pago"].ToString().Length < 1 ? dr["Sinal do Valor Pago"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Pago"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A121                       
                        strTemp = dr["Valor Pago"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp = strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Moeda do pagamento"].ToString().Length < 5 ?  dr["Moeda do pagamento"].ToString().PadLeft(5, '0') : dr["Moeda do pagamento"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Código do Banco"].ToString().Length < 5 ? dr["Código do Banco"].ToString().PadLeft(5, '0') : dr["Código do Banco"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Agência"].ToString().Length < 10 ? dr["No da Agência"].ToString().PadLeft(10,'0') : dr["No da Agência"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Conta Corrente"].ToString().Length < 12 ? dr["No da Conta Corrente"].ToString().PadLeft(12, '0') : dr["No da Conta Corrente"].ToString().Substring(0,12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Cheque"].ToString().Length < 15 ? dr["No do Cheque"].ToString().PadLeft(15, '0') : dr["No do Cheque"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR_2"].ToString().Length < 1 ? dr["Sinal do Valor IR_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR_2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor IR                       
                        strTemp = dr["Valor IR_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS_2"].ToString().Length < 1 ? dr["Sinal do Valor ISS_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS_2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor INSS"].ToString().Length < 1 ? dr["Sinal do Valor INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A132                       
                        strTemp = dr["Valor INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor CSLL"].ToString().Length < 1 ? dr["Sinal do Valor CSLL"].ToString().PadRight(1, ' ') : dr["Sinal do Valor CSLL"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // A134                       
                        strTemp = dr["Valor CSLL"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor COFINS"].ToString().Length < 1 ? dr["Sinal do Valor COFINS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor COFINS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A136                       
                        strTemp = dr["Valor COFINS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor PIS"].ToString().Length < 1 ? dr["Sinal do Valor PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor PIS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A138                       
                        strTemp = dr["Valor PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                           
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0, 18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Occurrence code"].ToString().Length < 3 ? dr["Occurrence code"].ToString().PadLeft(3, '0') : dr["Occurrence code"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cheque cancellation reason"].ToString().Length < 60 ? dr["Cheque cancellation reason"].ToString().PadRight(60, ' ') : dr["Cheque cancellation reason"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment method_2"].ToString().Length < 3 ? dr["Payment method_2"].ToString().PadLeft(3, '0') : dr["Payment method_2"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Number"].ToString().Length < 5 ? dr["Carrier Bank Number"].ToString().PadLeft(5, '0') : dr["Carrier Bank Number"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Branch number"].ToString().Length < 10 ? dr["Carrier Bank Branch number"].ToString().PadLeft(10, '0') : dr["Carrier Bank Branch number"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Account number"].ToString().Length < 12 ? dr["Carrier Bank Account number"].ToString().PadLeft(12, '0') : dr["Carrier Bank Account number"].ToString().Substring(0,12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Exchange rate2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(15, '0');
                        }

                        strFullLengthString = strFullLengthString + strTemp;

                        //NEW COLUMN ADDED
                        strTemp = dr["INCONSISTENCY_1"].ToString().Length < 5 ? dr["INCONSISTENCY_1"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_1"].ToString().Substring(0, 5); 
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_2"].ToString().Length < 5 ? dr["INCONSISTENCY_2"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_2"].ToString().Substring(0, 5); 
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_3"].ToString().Length < 5 ? dr["INCONSISTENCY_3"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_3"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_4"].ToString().Length < 5 ? dr["INCONSISTENCY_4"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_4"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_5"].ToString().Length < 5 ? dr["INCONSISTENCY_5"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_5"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                         i = i + 1;

                         string dd = DateTime.Now.Day.ToString();
                         string mon = DateTime.Now.Month.ToString();
                         string yy = DateTime.Now.Year.ToString();
                         string hh = DateTime.Now.Hour.ToString();
                         string min = DateTime.Now.Minute.ToString();

                         dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
                         mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();
                         hh = hh.Length < 2 ? hh.PadLeft(2, '0') : hh.ToString();
                         min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                         if (i != 1)
                             min = strMin;
                         else
                             min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                         date = dd + mon + yy + hh + min;

                         if (i == 1)
                         {
                             strMin = min;
                         }


                        if (i == 1 )//First Record
                        {   
                            try
                            {                               
                                
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString()))
                                {
                                    if (ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() == "CLM_ACTIVITY_CO_RI_BREAKDOWN" || ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() == "CLM_ACTIVITY_RESERVE")
                                    {
                                        //updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_CO_RI_BREAKDOWN", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'Y');
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'Y');
                                        InsertLog("Pagnet", "Update flag in "  + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() +"Payment Id: "+ ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for CO-RI Claims", DateTime.Now, DateTime.Now, "Success", "");
                                    }
                                    else
                                    {
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                        InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + "Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for CO-RI Claims", DateTime.Now, DateTime.Now, "Success", "");
                                    }

                                }

                            }
                            catch(Exception ex)
                            {
                                InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + "Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for CO-RI Claims", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());                                
                                continue;
                            }

                            try
                            {
                                strBuild.AppendLine("OUTER Check   existstance of File for RI_CLAIM");
                                if (File.Exists(ConfigurationSettings.AppSettings["FilePathCORI"] + "RI_CLAIM" + date + ".txt"))
                                {
                                    strBuild.AppendLine("INNER Check   existstance of File for RI_CLAIM");
                                    File.Delete(ConfigurationSettings.AppSettings["FilePathCORI"] + "RI_CLAIM" + date + ".txt");
                                }
                                strBuild.AppendLine("OUTER File creatE for RI_CLAIM");
                                
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCORI"] + "RI_CLAIM" + date + ".txt", true, Encoding.ASCII);
                                strBuild.AppendLine("INNER File created for RI_CLAIM Successfully");

                                if (ds.Tables[0].Rows.Count == 1)
                                    // changes by naveen , tfs 2419
                                    //file.Write(strFullLengthString);
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                else
                                    // changes by naveen , tfs 2419
                                    //file.Write(strFullLengthString);
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));

                                file.Close();

                                InsertLog("Pagnet", "Record Inserted in" + "RI_CLAIM" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");
                                                              

                                //Insert Export file name
                                InsertPagnetExportFiles("PROC_INSERT_PAGNET_EXPORT_FILES", ds.Tables[0].Rows[i - 1]["Interface code"].ToString(), "RI_CLAIM" + date + ".txt", FileStatus.Generated.ToString(), Dns.GetHostName());
                                InsertLog("Pagnet", "Insert file name " + "RI_CLAIM" + date + ".txt" + " in PAGNET_EXPORT_FILES", DateTime.Now, DateTime.Now, "Success", "");

                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "RI_CLAIM" + date + ".txt");

                            }
                            catch (Exception ex)
                            {
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString()))
                                {
                                    if (dr["TABLE_NAME"].ToString() == "CLM_ACTIVITY_CO_RI_BREAKDOWN" || dr["TABLE_NAME"].ToString() == "CLM_ACTIVITY_RESERVE")
                                    {
                                        //updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_CO_RI_BREAKDOWN", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'N');
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", dr["TABLE_NAME"].ToString(), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'N');
                                    }
                                    else
                                    {
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                    }

                                    InsertLog("Pagnet", "Record Inserted in" + "RI_CLAIM" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                }
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }
                           
                        }
                        else if (i == ds.Tables[0].Rows.Count)//Last record
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString()))
                                {
                                    if (ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() == "CLM_ACTIVITY_CO_RI_BREAKDOWN" || ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() == "CLM_ACTIVITY_RESERVE")
                                    {
                                        //updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_CO_RI_BREAKDOWN", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'Y');
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'Y');
                                    }
                                    else
                                    {
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                    }
                                    
                                    InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + "Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for CO-RI Claims", DateTime.Now, DateTime.Now, "Success", "");
                                    updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "RI_CLAIM" + date + ".txt");

                                }
                            }
                            catch (Exception ex)
                            {

                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {

                                //using (System.IO.StreamWriter file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCORI"] + "RI_CLAIM" + date + ".txt", true, Encoding.ASCII))
                                //{
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCORI"] + "RI_CLAIM" + date + ".txt", true, Encoding.ASCII);
                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                file.Close();

                                InsertLog("Pagnet", "Record Inserted in" + "RI_CLAIM" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");
                                //}
                            }
                            catch (Exception ex)
                            {
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString()))
                                {
                                    if (ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() == "CLM_ACTIVITY_CO_RI_BREAKDOWN" || ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() == "CLM_ACTIVITY_RESERVE")
                                    {
                                        //updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_CO_RI_BREAKDOWN", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'N');
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'N');
                                    }
                                    else
                                    {
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                    }

                                    InsertLog("Pagnet", "Record Inserted in" + "RI_CLAIM" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);

                                }
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }


                        }
                        else // Middle Record
                        {
                            try
                            {
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString()))
                                {
                                    if (dr["TABLE_NAME"].ToString() == "CLM_ACTIVITY_CO_RI_BREAKDOWN" || dr["TABLE_NAME"].ToString() == "CLM_ACTIVITY_RESERVE")
                                    {
                                        //updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_CO_RI_BREAKDOWN", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'Y');
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", dr["TABLE_NAME"].ToString(), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'Y');
                                    }
                                    else
                                    {
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'Y');
                                    }

                                    InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + "Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for CO-RI Claims", DateTime.Now, DateTime.Now, "Success", "");
                                    updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "RI_CLAIM" + date + ".txt");

                                }
                            }
                            catch (Exception ex)
                            {

                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }
                            try
                            {

                                //using (System.IO.StreamWriter file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCORI"] + "RI_CLAIM" + date + ".txt", true, Encoding.ASCII))
                                //{
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathCORI"] + "RI_CLAIM" + date + ".txt", true, Encoding.ASCII);
                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                file.Close();

                                InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + "Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for CO-RI Claims", DateTime.Now, DateTime.Now, "Success", "");
                                //}
                            }
                            catch (Exception ex)
                            {
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString()))
                                {
                                    if (dr["TABLE_NAME"].ToString() == "CLM_ACTIVITY_CO_RI_BREAKDOWN" || dr["TABLE_NAME"].ToString() == "CLM_ACTIVITY_RESERVE")
                                    {
                                        //updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_CO_RI_BREAKDOWN", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'N');
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", dr["TABLE_NAME"].ToString(), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COMP_ID"].ToString()), 'N');
                                    }
                                    else
                                    {
                                        updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString()), 0, 0, 'N');
                                    }

                                }
                                InsertLog("Pagnet", "Update flag in " + ds.Tables[0].Rows[i - 1]["TABLE_NAME"].ToString() + "Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for CO-RI Claims", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }

                        }                      

                    }
                    strBuild.AppendLine("Record inserter successfully for RI CLAIM ");
                    return strBuild.ToString(); 
                    }                    
                else
                {
                    strReturn = "There is no record to insert";
                    return strReturn;                    
                   
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("RI_CLAIM  " + ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString()));
               
            }
            finally
            {
                ds.Dispose();
            }
        }

        public string CLAIM_EXPENSE()
        {
           // aryParm = new SqlParameter[] { (new SqlParameter("@EVENT_CODE", strEvenCode)) };
            InsertLog("Pagnet", "Going to Fetch records for ClaimExpense", DateTime.Now, DateTime.Now, "Success", ""); 
            DataSet ds = getData("PROC_ClaimExpense");
            InsertLog("Pagnet", "Fetch records " + ds.Tables[0].Rows.Count + " for Claim Expense.", DateTime.Now, DateTime.Now, "Success", ""); 
            strBuild = new StringBuilder();
            string strTemp = "";
            int i = 0;
            string strTemp2 = "";
            string date = "";
            string[] strArrTemp;
            System.IO.StreamWriter file = null; ;
            string strMin = string.Empty;
            string FileStringToWrite = string.Empty;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strTemp = "";
                        strFullLengthString = "";
                        strTemp2 = "";
                        strFullLengthString = dr["Interface code"].ToString().Length < 3 ? dr["Interface code"].ToString().PadLeft(3, '0') : dr["Interface code"].ToString().Substring(0, 3);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Sequence of record"].ToString().Length < 10 ? dr["Sequence of record"].ToString().PadLeft(10, '0').ToString() : dr["Sequence of record"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Type"].ToString().Length < 1 ? dr["Beneficiary Type"].ToString().PadRight(1, ' ') : dr["Beneficiary Type"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["FOREIGN"].ToString().Length < 1 ? dr["FOREIGN"].ToString().PadLeft(1, '0') : dr["FOREIGN"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Class"].ToString().Length < 3 ? dr["Beneficiary Class"].ToString().PadLeft(3, '0') : dr["Beneficiary Class"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary name"].ToString().Length < 60 ? dr["Beneficiary name"].ToString().PadRight(60, ' ') : dr["Beneficiary name"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Beneficiary ID
                        strTemp = dr["Beneficiary ID"].ToString();
                        //strTemp2 = "";
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }

                        }
                        strTemp = strTemp.Length < 14 ? strTemp.PadRight(14, ' ') : strTemp.Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Beneficiary foreign ID"].ToString().Length < 14 ? dr["Beneficiary foreign ID"].ToString().PadRight(14, ' ') : dr["Beneficiary foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (street name)"].ToString().Length < 30 ? dr["Beneficiary Address (street name)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (street name)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;


                        strTemp = dr["Beneficiary Address (number)"].ToString().Length < 5 ? dr["Beneficiary Address (number)"].ToString().PadRight(5, ' ') : dr["Beneficiary Address (number)"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (complement)"].ToString().Length < 10 ? dr["Beneficiary Address (complement)"].ToString().PadRight(10, ' ') : dr["Beneficiary Address (complement)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (district)"].ToString().Length < 30 ? dr["Beneficiary Address (district)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (district)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (state)"].ToString().Length < 2 ? dr["Beneficiary Address (state)"].ToString().PadRight(2, ' ') : dr["Beneficiary Address (state)"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (city)"].ToString().Length < 30 ? dr["Beneficiary Address (city)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (city)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (zip code)"].ToString().Length < 10 ? dr["Beneficiary Address (zip code)"].ToString().PadLeft(10, '0') : dr["Beneficiary Address (zip code)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary e-mail address"].ToString().Length < 60 ? dr["Beneficiary e-mail address"].ToString().PadRight(60, ' ') : dr["Beneficiary e-mail address"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Number"].ToString().Length < 5 ? dr["Beneficiary Bank Number"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch"].ToString().Length < 10 ? dr["Beneficiary Bank Branch"].ToString().PadLeft(10, '0') : dr["Beneficiary Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Beneficiary Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Beneficiary Bank Branch Verifier Digit"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;
                        
                        //itrack #1750
                        strTemp = strTemp.Trim();
                        if (dr["PARTY_TYPE_ID"].ToString() == "208")
                        {
                            //strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                            //strFullLengthString = strFullLengthString + strTemp;

                            if (!String.IsNullOrEmpty(dr["Beneficiary Bank Account number"].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else
                                {
                                    strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());
                                }

                            }
                            strTemp = strTemp.Length < 12 ? strTemp.PadLeft(12, '0') : strTemp.Substring(0, 12);
                            // strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        else
                        {
                            strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;
                        }

                        //itrack #1750
                        strTemp = "";
                        if (dr["PARTY_TYPE_ID"].ToString() == "208")
                        {
                            //strTemp = dr["Beneficiary Bank Account Verifier Digit"].ToString().Length < 2 ? dr["Beneficiary Bank Account Verifier Digit"].ToString().PadRight(2, ' ') : dr["Beneficiary Bank Account Verifier Digit"].ToString().Substring(0, 2);
                            //strFullLengthString = strFullLengthString + strTemp;
                            if (!String.IsNullOrEmpty(dr["Beneficiary Bank Account number"].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                //Change on basis of Itrack #1750 
                                else  if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else
                                {
                                    strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());
                                }

                            }

                            strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);
                            //strTemp = dr["Beneficiary Bank Account Verifier Digit"].ToString().Length < 2 ? dr["Beneficiary Bank Account Verifier Digit"].ToString().PadRight(2, ' ') : dr["Beneficiary Bank Account Verifier Digit"].ToString().Substring(0,2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        else
                        {
                            strTemp = dr["Beneficiary Bank Account Verifier Digit"].ToString().Length < 2 ? dr["Beneficiary Bank Account Verifier Digit"].ToString().PadRight(2, ' ') : dr["Beneficiary Bank Account Verifier Digit"].ToString().Substring(0, 2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }

                        strTemp = dr["Beneficiary Bank Account type"].ToString().Length < 2 ? dr["Beneficiary Bank Account type"].ToString().PadLeft(2, '0') : dr["Beneficiary Bank Account type"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Account Currency"].ToString().Length < 5 ? dr["Beneficiary Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Account Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação IRRF"].ToString().Length < 2 ? dr["Cód Tributação IRRF"].ToString().PadLeft(2, '0') : dr["Cód Tributação IRRF"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Natureza do Rendimento"].ToString().Length < 5 ? dr["Natureza do Rendimento"].ToString().PadLeft(5, '0') : dr["Natureza do Rendimento"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula ISS"].ToString().Length < 1 ? dr["Calcula ISS"].ToString().PadLeft(1, '0') : dr["Calcula ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula INSS"].ToString().Length < 1 ? dr["Calcula INSS"].ToString().PadLeft(1, '0') : dr["Calcula INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação INSS"].ToString().Length < 2 ? dr["Cód Tributação INSS"].ToString().PadLeft(2, '0') : dr["Cód Tributação INSS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação CSLL"].ToString().Length < 2 ? dr["Cód Tributação CSLL"].ToString().PadLeft(2, '0') : dr["Cód Tributação CSLL"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação COFINS"].ToString().Length < 2 ? dr["Cód Tributação COFINS"].ToString().PadLeft(2, '0') : dr["Cód Tributação COFINS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação PIS"].ToString().Length < 2 ? dr["Cód Tributação PIS"].ToString().PadLeft(2, '0') : dr["Cód Tributação PIS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No de Dependentes"].ToString().Length < 2 ? dr["No de Dependentes"].ToString().PadLeft(2, '0') : dr["No de Dependentes"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No PIS"].ToString().Length < 11 ? dr["No PIS"].ToString().PadLeft(11, '0') : dr["No PIS"].ToString().Substring(0,11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Inscrição Municipal"].ToString().Length < 11 ? dr["Inscrição Municipal"].ToString().PadLeft(11, '0') : dr["Inscrição Municipal"].ToString().Substring(0,11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Número interno do corretor"].ToString().Length < 10 ? dr["Número interno do corretor"].ToString().PadLeft(10, '0') : dr["Número interno do corretor"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["CBO (Classific Brasileira Ocupação)"].ToString().Length < 10 ? dr["CBO (Classific Brasileira Ocupação)"].ToString().PadRight(10, ' ') : dr["CBO (Classific Brasileira Ocupação)"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Código SUSEP"].ToString().Length < 14 ? dr["Código SUSEP"].ToString().PadLeft(14, '0') : dr["Código SUSEP"].ToString().Substring(0,14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Funcionário"].ToString().Length < 10 ? dr["No do Funcionário"].ToString().PadLeft(10, '0') : dr["No do Funcionário"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód da Filial"].ToString().Length < 15 ? dr["Cód da Filial"].ToString().PadRight(15, ' ') : dr["Cód da Filial"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód do Centro de Custo"].ToString().Length < 15 ? dr["Cód do Centro de Custo"].ToString().PadRight(15, ' ') : dr["Cód do Centro de Custo"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data de Nascimento"].ToString().Length < 8 ? dr["Data de Nascimento"].ToString().PadLeft(8, '0') : dr["Data de Nascimento"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Party Type"].ToString().Length < 1 ? dr["Payee Party Type"].ToString().PadRight(1, ' ') : dr["Payee Party Type"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Foreign2"].ToString().Length < 1 ? dr["Foreign2"].ToString().PadLeft(1, '0') : dr["Foreign2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Payee Class"].ToString().PadLeft(3, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Name"].ToString().Length < 60 ? dr["Payee Name"].ToString().PadRight(60, ' ') : dr["Payee Name"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        
                        strTemp = dr["Payee ID (CPF or CNPJ)"].ToString();
                        
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                        }
                        strTemp = strTemp.Length < 14 ? strTemp.PadRight(14, ' ') : strTemp.Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Payee foreign ID"].ToString().Length < 14 ? dr["Payee foreign ID"].ToString().PadRight(14, ' ') : dr["Payee foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Number"].ToString().Length < 5 ? dr["Payee Bank Number"].ToString().PadLeft(5, '0') : dr["Payee Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch"].ToString().Length < 10 ? dr["Payee Bank Branch"].ToString().PadLeft(10, '0') : dr["Payee Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Payee Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Payee Bank Branch Verifier Digit"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        
                         //itrack #1750
                        if (dr["PARTY_TYPE_ID"].ToString() == "208")
                        {
                            //strTemp = dr["Payee Bank Account no."].ToString().Length < 12 ? dr["Payee Bank Account no."].ToString().PadLeft(12, '0') : dr["Payee Bank Account no."].ToString().Substring(0, 12);
                            //strFullLengthString = strFullLengthString + strTemp;
                            strTemp = strTemp.Trim();
                            if (!String.IsNullOrEmpty(dr["Payee Bank Account no."].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Payee Bank Account no."].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else
                                {
                                    strTemp = DecryptString(dr["Payee Bank Account no."].ToString());
                                }

                            }
                            strTemp = strTemp.Length < 12 ? strTemp.PadLeft(12, '0') : strTemp.Substring(0, 12);                           
                            strFullLengthString = strFullLengthString + strTemp;

                        }
                        else
                        {
                            strTemp = dr["Payee Bank Account no."].ToString().Length < 12 ? dr["Payee Bank Account no."].ToString().PadLeft(12, '0') : dr["Payee Bank Account no."].ToString().Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;
                        }

                          //itrack #1750
                        if (dr["PARTY_TYPE_ID"].ToString() == "208")
                        {
                            //strTemp = dr["Payee Bank Account Verifier digit"].ToString().Length < 2 ? dr["Payee Bank Account Verifier digit"].ToString().PadRight(2, ' ') : dr["Payee Bank Account Verifier digit"].ToString().Substring(0, 2);
                            //strFullLengthString = strFullLengthString + strTemp;
                            strTemp = "";
                            if (!String.IsNullOrEmpty(dr["Payee Bank Account no."].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Payee Bank Account no."].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else
                                {
                                    strTemp = "";
                                }

                            }
                            strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);                            
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        else
                        {
                            strTemp = dr["Payee Bank Account Verifier digit"].ToString().Length < 2 ? dr["Payee Bank Account Verifier digit"].ToString().PadRight(2, ' ') : dr["Payee Bank Account Verifier digit"].ToString().Substring(0, 2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        strTemp = dr["Payee Bank Account Type"].ToString().Length < 2 ? dr["Payee Bank Account Type"].ToString().PadLeft(2, '0') : dr["Payee Bank Account Type"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account Currency"].ToString().Length < 5 ? dr["Payee Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Payee Bank Account Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["PAYMENT ID"].ToString().Length < 25 ? dr["PAYMENT ID"].ToString().PadRight(25, ' ') : dr["PAYMENT ID"].ToString().Substring(0, 25);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Code"].ToString().Length < 5 ? dr["Carrier Code"].ToString().PadLeft(5, '0') : dr["Carrier Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Policy Branch Code"].ToString().Length < 5 ? dr["Carrier Policy Branch Code"].ToString().PadLeft(5, '0') : dr["Carrier Policy Branch Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["EVENT CODE"].ToString().Length < 5 ? dr["EVENT CODE"].ToString().PadLeft(5, '0') : dr["EVENT CODE"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Operation Type"].ToString().Length < 3 ? dr["Operation Type"].ToString().PadLeft(3, '0') : dr["Operation Type"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment Method"].ToString().Length < 3 ? dr["Payment Method"].ToString().PadLeft(3, '0') : dr["Payment Method"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number"].ToString().Length < 20 ? dr["Document number"].ToString().PadLeft(20, '0') : dr["Document number"].ToString().Substring(0,20) ;
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number - serial number"].ToString().PadRight(5, ' ');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice issuance date"].ToString().Length < 8 ? dr["Invoice issuance date"].ToString().PadRight(8, ' ') : dr["Invoice issuance date"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice due date"].ToString().Length < 8 ? dr["Invoice due date"].ToString().PadRight(8, ' ') : dr["Invoice due date"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Currency"].ToString().Length < 5 ? dr["Policy Currency"].ToString().PadLeft(5, '0') : dr["Policy Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Exchange rate
                        strTemp = dr["Exchange rate"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(15, '0');
                        }
                        strTemp = strTemp.Length < 15 ? strTemp.PadLeft(15, '0') : strTemp.Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Commission amount sign"].ToString().Length < 1 ? dr["Commission amount sign"].ToString().PadRight(1, ' ') : dr["Commission amount sign"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Refund amount                       
                        strTemp = dr["Commission amount"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento IR"].ToString().Length < 1 ? dr["Sinal do Valor Isento IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento IR                       
                        strTemp = dr["Valor Isento IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável IR"].ToString().Length < 1 ? dr["Sinal do Valor Tributável IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável IR                       
                        strTemp = dr["Valor Tributável IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                           
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento ISS"].ToString().Length < 1 ? dr["Sinal do Valor Isento ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento ISS                     
                        strTemp = dr["Valor Isento ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        //strTemp = dr["Valor Isento ISS"].ToString().PadLeft(2, '0');
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável ISS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável ISS                       
                        strTemp = dr["Valor Tributável ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento INSS"].ToString().Length < 1 ? dr["Sinal do Valor Isento INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento INSS                       
                        strTemp = dr["Valor Isento INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável INSS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Tributável INSS                       
                        strTemp = dr["Valor Tributável INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Length < 1 ? dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Isento CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável"].ToString().Length < 1 ? dr["Sinal do Valor Tributável"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Tributável CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR"].ToString().Length < 1 ? dr["Sinal do Valor IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor IR                       
                        strTemp = dr["Valor IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS"].ToString().Length < 1 ? dr["Sinal do Valor ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Sinal do Valor Desconto"].ToString().Length < 1 ? dr["Sinal do Valor Desconto"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Desconto"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Desconto                       
                        strTemp = dr["Valor Desconto"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Multa"].ToString().PadRight(1, ' ');
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Multa                       
                        strTemp = dr["Valor Multa"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Expense payment description"].ToString().Length < 60 ? dr["Expense payment description"].ToString().PadRight(60, ' ') : dr["Expense payment description"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Branch Code"].ToString().Length < 15 ? dr["Policy Branch Code"].ToString().PadRight(15, ' ') : dr["Policy Branch Code"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Profit Center code"].ToString().Length < 15 ? dr["Profit Center code"].ToString().PadLeft(15, '0') : dr["Profit Center code"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR1"].ToString().Length < 15 ? dr["A_DEFINIR1"].ToString().PadRight(15, ' ') : dr["A_DEFINIR1"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR2"].ToString().Length < 15 ? dr["A_DEFINIR2"].ToString().PadRight(15, ' ') : dr["A_DEFINIR2"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Accounting LOB"].ToString().Length < 15 ? dr["Policy Accounting LOB"].ToString().PadRight(15, ' ') : dr["Policy Accounting LOB"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR3"].ToString().Length < 15 ? dr["A_DEFINIR3"].ToString().PadRight(15, ' ') : dr["A_DEFINIR3"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Apólice"].ToString().Length < 15 ? dr["Apólice"].ToString().PadRight(15, ' ') : dr["Apólice"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Apólice (cont)"].ToString().Length < 10 ? dr["Apólice (cont)"].ToString().PadRight(10, ' ') : dr["Apólice (cont)"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Endosso"].ToString().Length < 5 ? dr["Endosso"].ToString().PadLeft(5, '0') : dr["Endosso"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Parcela"].ToString().Length < 5 ? dr["Parcela"].ToString().PadRight(5, ' ') : dr["Parcela"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR4"].ToString().Length < 15 ? dr["A_DEFINIR4"].ToString().PadRight(15, ' ') : dr["A_DEFINIR4"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR5"].ToString().Length < 10 ? dr["A_DEFINIR5"].ToString().PadRight(10, ' ') : dr["A_DEFINIR5"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data de quitação da parcela"].ToString().Length < 15 ? dr["Data de quitação da parcela"].ToString().PadRight(15, ' ') : dr["Data de quitação da parcela"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Tomador/ Descrição"].ToString().Length < 60 ? dr["Tomador/ Descrição"].ToString().PadRight(60, ' ') : dr["Tomador/ Descrição"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Prêmio                       
                        strTemp = dr["Prêmio"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;


                        // % Comissão                       
                        strTemp = dr["% Comissão"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                           
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A115                       
                        strTemp = dr["A_DEFINIR6"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A116                       
                        strTemp = dr["A_DEFINIR7"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp =  strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A117                       
                        strTemp = dr["A_DEFINIR8"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        //Movement Type
                        strTemp = dr["Tipo de Movimento"].ToString().Length < 1 ? dr["Tipo de Movimento"].ToString().PadRight(1, ' ') : dr["Tipo de Movimento"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data do Pagamento"].ToString().Length < 8 ? dr["Data do Pagamento"].ToString().PadLeft(8, '0') : dr["Data do Pagamento"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Pago"].ToString().Length < 1 ? dr["Sinal do Valor Pago"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Pago"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A121                       
                        strTemp = dr["Valor Pago"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Moeda do pagamento"].ToString().Length < 5 ? dr["Moeda do pagamento"].ToString().PadLeft(5, '0') : dr["Moeda do pagamento"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Código do Banco"].ToString().Length < 5 ? dr["Código do Banco"].ToString().PadLeft(5, '0') : dr["Código do Banco"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Agência"].ToString().Length < 10 ? dr["No da Agência"].ToString().PadLeft(10, '0') : dr["No da Agência"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Conta Corrente"].ToString().Length < 12 ? dr["No da Conta Corrente"].ToString().PadLeft(12, '0') : dr["No da Conta Corrente"].ToString().Substring(0,12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Cheque"].ToString().Length < 15 ? dr["No do Cheque"].ToString().PadLeft(15, '0') : dr["No do Cheque"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR_2"].ToString().Length < 1 ? dr["Sinal do Valor IR_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR_2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor IR                       
                        strTemp = dr["Valor IR_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS_2"].ToString().Length < 1 ? dr["Sinal do Valor ISS_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS_2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor INSS"].ToString().Length < 1 ? dr["Sinal do Valor INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A132                       
                        strTemp = dr["Valor INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor CSLL"].ToString().Length < 1 ? dr["Sinal do Valor CSLL"].ToString().PadRight(1, ' ') : dr["Sinal do Valor CSLL"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // A134                       
                        strTemp = dr["Valor CSLL"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor COFINS"].ToString().Length < 1 ? dr["Sinal do Valor COFINS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor COFINS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A136                       
                        strTemp = dr["Valor COFINS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor PIS"].ToString().Length < 1 ? dr["Sinal do Valor PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor PIS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A138                       
                        strTemp = dr["Valor PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Occurrence code"].ToString().Length < 3 ? dr["Occurrence code"].ToString().PadLeft(3, '0') : dr["Occurrence code"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cheque cancellation reason"].ToString().Length < 60 ? dr["Cheque cancellation reason"].ToString().PadRight(60, ' ') : dr["Cheque cancellation reason"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment method_2"].ToString().Length < 3 ? dr["Payment method_2"].ToString().PadLeft(3, '0') : dr["Payment method_2"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Number"].ToString().Length < 5 ? dr["Carrier Bank Number"].ToString().PadLeft(5, '0') : dr["Carrier Bank Number"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Branch number"].ToString().Length < 10 ? dr["Carrier Bank Branch number"].ToString().PadLeft(10, '0') : dr["Carrier Bank Branch number"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Account number"].ToString().Length < 12 ? dr["Carrier Bank Account number"].ToString().PadLeft(12, '0') : dr["Carrier Bank Account number"].ToString().Substring(0,12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Exchange rate2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(15, '0');
                        }

                        strFullLengthString = strFullLengthString + strTemp;

                        //NEW COLUMN ADDED
                        strTemp = dr["INCONSISTENCY_1"].ToString().Length < 5 ? dr["INCONSISTENCY_1"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_1"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_2"].ToString().Length < 5 ? dr["INCONSISTENCY_2"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_2"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_3"].ToString().Length < 5 ? dr["INCONSISTENCY_3"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_3"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_4"].ToString().Length < 5 ? dr["INCONSISTENCY_4"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_4"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_5"].ToString().Length < 5 ? dr["INCONSISTENCY_5"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_5"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        i = i + 1;

                        string dd = DateTime.Now.Day.ToString();
                        string mon = DateTime.Now.Month.ToString();
                        string yy = DateTime.Now.Year.ToString();
                        string hh = DateTime.Now.Hour.ToString();
                        string min = DateTime.Now.Minute.ToString();
                        
                         
                        dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
                        mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();
                        hh = hh.Length < 2 ? hh.PadLeft(2, '0') : hh.ToString();
                        min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                       
                        if (i != 1)
                            min = strMin;
                        else
                            min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                        date = dd + mon + yy + hh + min;

                        if (i == 1)
                        {
                            strMin = min;
                        }
                        

                        if (i == 1 )//First Record
                        {   
                            try
                            {                                
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()),
                                           Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COVERAGE_ID"].ToString()), 0, 'Y');
                                InsertLog("Pagnet", "Update flag in CLM_ACTIVITY_RESERVE " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Expense.", DateTime.Now, DateTime.Now, "Success", "");

                            }
                            catch(Exception ex)
                            {
                                InsertLog("Pagnet", "Update flag in CLM_ACTIVITY_RESERVE " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Expense.", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {

                                if (File.Exists(ConfigurationSettings.AppSettings["FilePathClaimExp"] + "ClaimExpense" + date + ".txt"))
                                {
                                    File.Delete(ConfigurationSettings.AppSettings["FilePathClaimExp"] + "ClaimExpense" + date + ".txt");
                                }
                                
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathClaimExp"] + "ClaimExpense" + date + ".txt", true, Encoding.ASCII);
                                strBuild.AppendLine("File created for ClaimExpense Successfully");
                                if (ds.Tables[0].Rows.Count == 1)
                                    // changes by naveen , tfs 2419
                                    //file.Write(strFullLengthString);
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                else
                                {
                                    // changes by naveen , tfs 2419
                                    //file.Write(strFullLengthString);
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                    
                                }


                                file.Close();

                                InsertLog("Pagnet", "Record Inserted in " + "ClaimExpense" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");

                                //Insert Export file name
                                InsertPagnetExportFiles("PROC_INSERT_PAGNET_EXPORT_FILES", ds.Tables[0].Rows[i - 1]["Interface code"].ToString(), "ClaimExpense" + date + ".txt", FileStatus.Generated.ToString(), Dns.GetHostName());
                                InsertLog("Pagnet", "Insert file name " + "ClaimExpense" + date + ".txt" + " in PAGNET_EXPORT_FILES", DateTime.Now, DateTime.Now, "Success", "");
                                
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "ClaimExpense" + date + ".txt");
                                

                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in " + "ClaimExpense" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()),
                                          Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COVERAGE_ID"].ToString()), 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }
                           
                        }
                        else if (i == ds.Tables[0].Rows.Count)//Last record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()),
                                          Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COVERAGE_ID"].ToString()), 0, 'Y');
                                InsertLog("Pagnet", "Update flag in CLM_ACTIVITY_RESERVE " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Expense.", DateTime.Now, DateTime.Now, "Success", "");
                            }
                            catch (Exception ex)
                            {

                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {

                                
                               // file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathClaimExp"] + "ClaimExpense" + date + ".txt", true, Encoding.ASCII);
                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                               
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                file.Close();

                                InsertLog("Pagnet", "Record Inserted in " + "ClaimExpense" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");
                                
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "ClaimExpense" + date + ".txt");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in " + "ClaimExpense" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()),
                                           Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COVERAGE_ID"].ToString()), 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }


                        }
                        else // Middle Record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()),
                                           Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COVERAGE_ID"].ToString()), 0, 'Y');
                                InsertLog("Pagnet", "Update flag in CLM_ACTIVITY_RESERVE " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Expense.", DateTime.Now, DateTime.Now, "Success", "");
                            }
                            catch (Exception ex)
                            {

                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {

                                
                               // file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathClaimExp"] + "ClaimExpense" + date + ".txt", true, Encoding.ASCII);
                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                file.Close();
                                InsertLog("Pagnet", "Record Inserted in " + "ClaimExpense" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "ClaimExpense" + date + ".txt");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in " + "ClaimExpense" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()),
                                           Convert.ToInt32(ds.Tables[0].Rows[i - 1]["COVERAGE_ID"].ToString()), 0, 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }

                        }
                        //FileStringToWrite +=  strFullLengthString;
                    }
                    strBuild.AppendLine("Record inserted successfully for Claim Expense. ");
                    return strBuild.ToString(); 
                    }                   
                   

                else
                {
                    strBuild.AppendLine("There is no record to insert for : Claim Expense");
                    return strBuild.ToString();
                   
                }
            }
           
            catch (Exception ex)
            {
                throw (new Exception("Claim Expense  " + ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString()));
                
            }
            finally
            {
                ds.Dispose();
            }
        }

        public string Claim_Indeminity()
        {
            
            //aryParm = new SqlParameter[] { (new SqlParameter("@EVENT_CODE", strEventCode)) };
            InsertLog("Pagnet", "Going to Fetch records for Claim Indeminity", DateTime.Now, DateTime.Now, "Success", ""); 
            DataSet ds = getData("PROC_ClaimInd");
            InsertLog("Pagnet", "Fetch records " + ds.Tables[0].Rows.Count + " for Claim Indeminity.", DateTime.Now, DateTime.Now, "Success", ""); 
            strBuild = new StringBuilder();
            string strTemp = "";
            int i = 0;
            string strTemp2 = "";
            string[] strArrTemp;
            string strMin = string.Empty;
            string date = "";
            System.IO.StreamWriter file = null;

            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strTemp = "";
                        strFullLengthString = "";
                        strTemp2 = "";
                        strFullLengthString = dr["Interface code"].ToString().Length < 3 ? dr["Interface code"].ToString().PadLeft(3, '0') : dr["Interface code"].ToString().Substring(0, 3);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Sequence of record"].ToString().Length < 10 ? dr["Sequence of record"].ToString().PadLeft(10, '0').ToString() : dr["Sequence of record"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Type"].ToString().Length < 1 ? dr["Beneficiary Type"].ToString().PadRight(1, ' ') : dr["Beneficiary Type"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["FOREIGN"].ToString().Length < 1 ? dr["FOREIGN"].ToString().PadLeft(1, '0') :  dr["FOREIGN"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Class"].ToString().Length < 3 ? dr["Beneficiary Class"].ToString().PadLeft(3, '0') : dr["Beneficiary Class"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary name"].ToString().Length < 60 ? dr["Beneficiary name"].ToString().PadRight(60, ' ') : dr["Beneficiary name"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Beneficiary ID
                        strTemp = dr["Beneficiary ID"].ToString();                        
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }

                        }
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Beneficiary foreign ID"].ToString().Length < 14 ? dr["Beneficiary foreign ID"].ToString().PadRight(14, ' ') : dr["Beneficiary foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (street name)"].ToString().Length < 30 ? dr["Beneficiary Address (street name)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (street name)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;


                        strTemp = dr["Beneficiary Address (number)"].ToString().Length < 5 ? dr["Beneficiary Address (number)"].ToString().PadRight(5, ' ') : dr["Beneficiary Address (number)"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (complement)"].ToString().Length < 10 ? dr["Beneficiary Address (complement)"].ToString().PadRight(10, ' ') : dr["Beneficiary Address (complement)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (district)"].ToString().Length < 30 ? dr["Beneficiary Address (district)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (district)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (state)"].ToString().Length < 2 ? dr["Beneficiary Address (state)"].ToString().PadRight(2, ' ') : dr["Beneficiary Address (state)"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (city)"].ToString().Length < 30 ? dr["Beneficiary Address (city)"].ToString().PadRight(30, ' ') : dr["Beneficiary Address (city)"].ToString().Substring(0, 30);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Address (zip code)"].ToString().Length < 10 ? dr["Beneficiary Address (zip code)"].ToString().PadLeft(10, '0') : dr["Beneficiary Address (zip code)"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary e-mail address"].ToString().Length < 60 ? dr["Beneficiary e-mail address"].ToString().PadRight(60, ' ') : dr["Beneficiary e-mail address"].ToString().Substring(0, 60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Number"].ToString().Length < 5 ? dr["Beneficiary Bank Number"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch"].ToString().Length < 10 ? dr["Beneficiary Bank Branch"].ToString().PadLeft(10, '0') : dr["Beneficiary Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Beneficiary Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Beneficiary Bank Branch Verifier Digit"].ToString().Substring(0, 1);
                        strFullLengthString = strFullLengthString + strTemp;

                         //itrack #1750
                        strTemp=strTemp.Trim();
                        if (dr["PARTY_TYPE_ID"].ToString() == "208")
                        {
                            //strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                            //strFullLengthString = strFullLengthString + strTemp;
                            if (!String.IsNullOrEmpty(dr["Beneficiary Bank Account number"].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else
                                {
                                    strTemp = DecryptString(dr["Beneficiary Bank Account number"].ToString());
                                }

                            }
                            strTemp = strTemp.Length < 12 ? strTemp.PadLeft(12, '0') : strTemp.Substring(0, 12);
                            // strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        else
                        {
                            strTemp = dr["Beneficiary Bank Account number"].ToString().Length < 12 ? dr["Beneficiary Bank Account number"].ToString().PadLeft(12, '0') : dr["Beneficiary Bank Account number"].ToString().Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;
                        }

                         //itrack #1750
                        if (dr["PARTY_TYPE_ID"].ToString() == "208")
                        {
                             //Removed this comment as per new requirement,itrack 1750
                             //issue
                            //  For payment thru check (when position 617 = 5) the system retrieves verifier 
                            //digit of account number filled with "00" o the fields 21 (position 312)
                           // and field 52 (position 565).
                          // Also the bank account number should be full filled with zeros, 
                          //but the system retrieved one record with one zero missing on fields 20 (position #300) and field 51 (position 553).
                            //if (dr["Payee Bank Account no."] == "")
                            //{
                            //    strTemp = dr["Beneficiary Bank Account Verifier Digit"].ToString().Length < 2 ? dr["Beneficiary Bank Account Verifier Digit"].ToString().PadRight(2, ' ') : dr["Beneficiary Bank Account Verifier Digit"].ToString().Substring(0, 2);
                            //    strFullLengthString = strFullLengthString + strTemp;
                               
                            //}
                            //else
                            //{
                                //strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);
                                //strFullLengthString = strFullLengthString + strTemp;
                           //} 
                            strTemp = "";
                            if (!String.IsNullOrEmpty(dr["Payee Bank Account no."].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Payee Bank Account no."].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else
                                {
                                    strTemp = "";
                                }

                            }
                            //commented by naveen, itrack 1750
                            strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        else
                        {
                            strTemp = dr["Beneficiary Bank Account Verifier Digit"].ToString().Length < 2 ? dr["Beneficiary Bank Account Verifier Digit"].ToString().PadRight(2, ' ') : dr["Beneficiary Bank Account Verifier Digit"].ToString().Substring(0, 2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }

                        strTemp = dr["Beneficiary Bank Account type"].ToString().Length < 2 ? dr["Beneficiary Bank Account type"].ToString().PadLeft(2, '0') : dr["Beneficiary Bank Account type"].ToString().Substring(0, 2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Beneficiary Bank Account Currency"].ToString().Length < 5 ? dr["Beneficiary Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Beneficiary Bank Account Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação IRRF"].ToString().Length < 2 ? dr["Cód Tributação IRRF"].ToString().PadLeft(2, '0') : dr["Cód Tributação IRRF"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Natureza do Rendimento"].ToString().Length < 5 ? dr["Natureza do Rendimento"].ToString().PadLeft(5, '0') : dr["Natureza do Rendimento"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula ISS"].ToString().Length < 1 ? dr["Calcula ISS"].ToString().PadLeft(1, '0') : dr["Calcula ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Calcula INSS"].ToString().Length < 1 ? dr["Calcula INSS"].ToString().PadLeft(1, '0') : dr["Calcula INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação INSS"].ToString().Length < 2 ? dr["Cód Tributação INSS"].ToString().PadLeft(2, '0') : dr["Cód Tributação INSS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação CSLL"].ToString().Length < 2 ? dr["Cód Tributação CSLL"].ToString().PadLeft(2, '0') : dr["Cód Tributação CSLL"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação COFINS"].ToString().Length < 2 ? dr["Cód Tributação COFINS"].ToString().PadLeft(2, '0') : dr["Cód Tributação COFINS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód Tributação PIS"].ToString().Length < 2 ? dr["Cód Tributação PIS"].ToString().PadLeft(2, '0') : dr["Cód Tributação PIS"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No de Dependentes"].ToString().Length < 2 ? dr["No de Dependentes"].ToString().PadLeft(2, '0') : dr["No de Dependentes"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No PIS"].ToString().Length < 11 ? dr["No PIS"].ToString().PadLeft(11, '0') : dr["No PIS"].ToString().Substring(0,11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Inscrição Municipal"].ToString().Length < 11 ? dr["Inscrição Municipal"].ToString().PadLeft(11, '0') : dr["Inscrição Municipal"].ToString().Substring(0,11);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Número interno do corretor"].ToString().Length < 10 ? dr["Número interno do corretor"].ToString().PadLeft(10, '0') : dr["Número interno do corretor"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["CBO (Classific Brasileira Ocupação)"].ToString().Length < 10 ? dr["CBO (Classific Brasileira Ocupação)"].ToString().PadRight(10, ' ') : dr["CBO (Classific Brasileira Ocupação)"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Código SUSEP"].ToString().Length < 14 ? dr["Código SUSEP"].ToString().PadLeft(14, '0') : dr["Código SUSEP"].ToString().Substring(0,14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Funcionário"].ToString().Length < 10 ? dr["No do Funcionário"].ToString().PadLeft(10, '0') : dr["No do Funcionário"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód da Filial"].ToString().Length < 15 ? dr["Cód da Filial"].ToString().PadRight(15, ' ') : dr["Cód da Filial"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cód do Centro de Custo"].ToString().Length < 15 ? dr["Cód do Centro de Custo"].ToString().PadRight(15, ' ') : dr["Cód do Centro de Custo"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        
                        strTemp = dr["Data de Nascimento"].ToString().Length < 8 ? dr["Data de Nascimento"].ToString().PadLeft(8, '0') : dr["Data de Nascimento"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Party Type"].ToString().Length < 1 ? dr["Payee Party Type"].ToString().PadRight(1, ' ') : dr["Payee Party Type"].ToString().Substring(0,1); 
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Foreign2"].ToString().Length < 1 ? dr["Foreign2"].ToString().PadLeft(1, '0') : dr["Foreign2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Payee Class"].ToString().Length < 3 ? dr["Payee Class"].ToString().PadLeft(3, '0') : dr["Payee Class"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Name"].ToString().Length < 60 ? dr["Payee Name"].ToString().PadRight(60, ' ') : dr["Payee Name"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;
                        
                        strTemp = dr["Payee ID (CPF or CNPJ)"].ToString();
                        
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');

                            strTemp = "";
                            for (int j = 0; j < strArrTemp.Length; j++)
                            {
                                strTemp = strTemp + strArrTemp[j];
                            }
                            if (strTemp.IndexOf('/') > 0)
                            {
                                strArrTemp = strTemp.Split('/');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                            if (strTemp.IndexOf('-') > 0)
                            {
                                strArrTemp = strTemp.Split('-');
                                strTemp = strArrTemp[0] + strArrTemp[1];
                            }
                        }
                        strTemp = strTemp.Length < 14 ? strTemp.PadRight(14,' ') : strTemp.Substring(0,14);
                        strFullLengthString = strFullLengthString + strTemp.PadRight(14, ' ');

                        strTemp = dr["Payee foreign ID"].ToString().Length < 14 ? dr["Payee foreign ID"].ToString().PadRight(14, ' ') : dr["Payee foreign ID"].ToString().Substring(0, 14);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Number"].ToString().Length < 5 ? dr["Payee Bank Number"].ToString().PadLeft(5, '0') : dr["Payee Bank Number"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch"].ToString().Length < 10 ? dr["Payee Bank Branch"].ToString().PadLeft(10, '0') : dr["Payee Bank Branch"].ToString().Substring(0, 10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Branch Verifier Digit"].ToString().Length < 1 ? dr["Payee Bank Branch Verifier Digit"].ToString().PadRight(1, ' ') : dr["Payee Bank Branch Verifier Digit"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        //itrack #1750
                        strTemp = strTemp.Trim();
                        if (dr["PARTY_TYPE_ID"].ToString() == "208")
                        {
                            //strTemp = dr["Payee Bank Account no."].ToString().Length < 12 ? dr["Payee Bank Account no."].ToString().PadLeft(12, '0') : dr["Payee Bank Account no."].ToString().Substring(0, 12);
                            //strFullLengthString = strFullLengthString + strTemp;
                            if (!String.IsNullOrEmpty(dr["Payee Bank Account no."].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Payee Bank Account no."].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[0];
                                }
                                 else
                                {
                                    strTemp = DecryptString(dr["Payee Bank Account no."].ToString());
                                }

                            }
                            strTemp = strTemp.Length < 12 ? strTemp.PadLeft(12, '0') : strTemp.Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;

                        }
                        else
                        {
                            strTemp = dr["Payee Bank Account no."].ToString().Length < 12 ? dr["Payee Bank Account no."].ToString().PadLeft(12, '0') : dr["Payee Bank Account no."].ToString().Substring(0, 12);
                            strFullLengthString = strFullLengthString + strTemp;
                        }

                         //itrack #1750
                        if (dr["PARTY_TYPE_ID"].ToString() == "208")
                        {
                            //added by navee for fetching bank verifier disgit as blank

                            //if (dr["Payee Bank Account no."] == "")
                            //{
                            //    strTemp = dr["Payee Bank Account Verifier digit"].ToString().Length < 2 ? dr["Payee Bank Account Verifier digit"].ToString().PadRight(2, ' ') : dr["Payee Bank Account Verifier digit"].ToString().Substring(0, 2);
                            //    strFullLengthString = strFullLengthString + strTemp;
                            //}
                            //else
                            //{
                            //    strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);
                            //    strFullLengthString = strFullLengthString + strTemp;
                            //}

                            strTemp = "";
                            if (!String.IsNullOrEmpty(dr["Payee Bank Account no."].ToString()))
                            {
                                strTemp = "";
                                strTemp = DecryptString(dr["Payee Bank Account no."].ToString());

                                if (strTemp.IndexOf('-') > 0)
                                {
                                    strArrTemp = strTemp.Split('-');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                //Change on basis of Itrack #1750 
                                else if (strTemp.IndexOf('/') > 0)
                                {
                                    strArrTemp = strTemp.Split('/');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf('.') > 0)
                                {
                                    strArrTemp = strTemp.Split('.');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else if (strTemp.IndexOf(',') > 0)
                                {
                                    strArrTemp = strTemp.Split(',');

                                    strTemp = "";
                                    strTemp = strArrTemp[1];
                                }
                                else
                                {
                                    strTemp = "";
                                }

                            }
                           // commented by naveen, itrack 1750
                            strTemp = strTemp.Length < 2 ? strTemp.PadRight(2, ' ') : strTemp.Substring(0, 2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }
                        else
                        {
                            strTemp = dr["Payee Bank Account Verifier digit"].ToString().Length < 2 ? dr["Payee Bank Account Verifier digit"].ToString().PadRight(2, ' ') : dr["Payee Bank Account Verifier digit"].ToString().Substring(0, 2);
                            strFullLengthString = strFullLengthString + strTemp;
                        }

                        strTemp = dr["Payee Bank Account Type"].ToString().Length < 2 ? dr["Payee Bank Account Type"].ToString().PadLeft(2, '0') : dr["Payee Bank Account Type"].ToString().Substring(0,2);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payee Bank Account Currency"].ToString().Length < 5 ? dr["Payee Bank Account Currency"].ToString().PadLeft(5, '0') : dr["Payee Bank Account Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["PAYMENT ID"].ToString().Length < 25 ? dr["PAYMENT ID"].ToString().PadRight(25, ' ') : dr["PAYMENT ID"].ToString().Substring(0, 25);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Code"].ToString().Length < 5 ? dr["Carrier Code"].ToString().PadLeft(5, '0') : dr["Carrier Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Policy Branch Code"].ToString().Length < 5 ? dr["Carrier Policy Branch Code"].ToString().PadLeft(5, '0') : dr["Carrier Policy Branch Code"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["EVENT CODE"].ToString().Length < 5 ? dr["EVENT CODE"].ToString().PadLeft(5, '0') : dr["EVENT CODE"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Operation Type"].ToString().Length < 3 ? dr["Operation Type"].ToString().PadLeft(3, '0') : dr["Operation Type"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment Method"].ToString().Length < 3 ? dr["Payment Method"].ToString().PadLeft(3, '0') : dr["Payment Method"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number"].ToString().Length < 20 ? dr["Document number"].ToString().PadLeft(20, '0') : dr["Document number"].ToString().Substring(0,20);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Document number - serial number"].ToString().Length < 5 ? dr["Document number - serial number"].ToString().PadRight(5, ' ') : dr["Document number - serial number"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice issuance date"].ToString().Length < 8 ? dr["Invoice issuance date"].ToString().PadRight(8, ' ') : dr["Invoice issuance date"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Invoice due date"].ToString().Length < 8 ? dr["Invoice due date"].ToString().PadRight(8, ' ') : dr["Invoice due date"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Currency"].ToString().Length < 5 ? dr["Policy Currency"].ToString().PadLeft(5, '0') : dr["Policy Currency"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Exchange rate
                        strTemp = dr["Exchange rate"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(15, '0');
                        }
                        strTemp = strTemp.Length < 15 ? strTemp.PadLeft(15, '0') : strTemp.Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Commission amount sign"].ToString().Length < 1 ? dr["Commission amount sign"].ToString().PadRight(1, ' ') : dr["Commission amount sign"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Refund amount                       
                        strTemp = dr["Commission amount"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento IR"].ToString().Length < 1 ? dr["Sinal do Valor Isento IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento IR                       
                        strTemp = dr["Valor Isento IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável IR"].ToString().Length < 1 ? dr["Sinal do Valor Tributável IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável IR                       
                        strTemp = dr["Valor Tributável IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento ISS"].ToString().Length < 1 ? dr["Sinal do Valor Isento ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento ISS                     
                        strTemp = dr["Valor Isento ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);                        
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável ISS"].ToString().Length  < 1 ?  dr["Sinal do Valor Tributável ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável ISS                       
                        strTemp = dr["Valor Tributável ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento INSS"].ToString().Length < 1 ? dr["Sinal do Valor Isento INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento INSS                       
                        strTemp = dr["Valor Isento INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável INSS"].ToString().Length < 1 ? dr["Sinal do Valor Tributável INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Tributável INSS                       
                        strTemp = dr["Valor Tributável INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Length < 1 ? dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Isento CSLL/COFINS/PIS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Isento CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Isento CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Tributável"].ToString().Length < 1 ? dr["Sinal do Valor Tributável"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Tributável"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor Tributável CSLL/COFINS/PIS                       
                        strTemp = dr["Valor Tributável CSLL/COFINS/PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR"].ToString().Length < 1 ? dr["Sinal do Valor IR"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Valor IR                       
                        strTemp = dr["Valor IR"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS"].ToString().Length < 1 ? dr["Sinal do Valor ISS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        strTemp = dr["Sinal do Valor Desconto"].ToString().Length < 1 ? dr["Sinal do Valor Desconto"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Desconto"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Desconto                       
                        strTemp = dr["Valor Desconto"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Multa"].ToString().Length < 1 ? dr["Sinal do Valor Multa"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Multa"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor Multa                       
                        strTemp = dr["Valor Multa"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Expense payment description"].ToString().Length < 60 ? dr["Expense payment description"].ToString().PadRight(60, ' ') : dr["Expense payment description"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Branch Code"].ToString().Length < 15 ? dr["Policy Branch Code"].ToString().PadRight(15, ' ') : dr["Policy Branch Code"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Profit Center code"].ToString().Length < 15 ? dr["Profit Center code"].ToString().PadLeft(15, '0') : dr["Profit Center code"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR1"].ToString().Length < 15 ? dr["A_DEFINIR1"].ToString().PadRight(15, ' ') : dr["A_DEFINIR1"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR2"].ToString().Length < 15 ? dr["A_DEFINIR2"].ToString().PadRight(15, ' ') : dr["A_DEFINIR2"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Policy Accounting LOB"].ToString().Length < 15 ? dr["Policy Accounting LOB"].ToString().PadRight(15, ' ') : dr["Policy Accounting LOB"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR3"].ToString().Length < 15 ? dr["A_DEFINIR3"].ToString().PadRight(15, ' ') : dr["A_DEFINIR3"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Apólice"].ToString().Length < 15 ? dr["Apólice"].ToString().PadRight(15, ' ') :  dr["Apólice"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Apólice (cont)"].ToString().Length < 10 ? dr["Apólice (cont)"].ToString().PadRight(10, ' ') : dr["Apólice (cont)"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp =  dr["Endosso"].ToString().Length < 5 ? dr["Endosso"].ToString().PadLeft(5, '0') : dr["Endosso"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Parcela"].ToString().Length < 5 ? dr["Parcela"].ToString().PadRight(5, ' ') : dr["Parcela"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR4"].ToString().Length < 15 ? dr["A_DEFINIR4"].ToString().PadRight(15, ' ') : dr["A_DEFINIR4"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["A_DEFINIR5"].ToString().Length < 10 ? dr["A_DEFINIR5"].ToString().PadRight(10, ' ') : dr["A_DEFINIR5"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data de quitação da parcela"].ToString().Length < 15 ? dr["Data de quitação da parcela"].ToString().PadRight(15, ' ') : dr["Data de quitação da parcela"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Tomador/ Descrição"].ToString().Length < 60 ? dr["Tomador/ Descrição"].ToString().PadRight(60, ' ') : dr["Tomador/ Descrição"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        // Prêmio                       
                        strTemp = dr["Prêmio"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;


                        // % Comissão                       
                        strTemp = dr["% Comissão"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A115                       
                        strTemp = dr["A_DEFINIR6"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A116                       
                        strTemp = dr["A_DEFINIR7"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A117                       
                        strTemp = dr["A_DEFINIR8"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Tipo de Movimento"].ToString().Length < 1 ? dr["Tipo de Movimento"].ToString().PadRight(1, ' ') : dr["Tipo de Movimento"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Data do Pagamento"].ToString().Length < 8 ? dr["Data do Pagamento"].ToString().PadLeft(8, '0') : dr["Data do Pagamento"].ToString().Substring(0,8);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor Pago"].ToString().Length < 1 ? dr["Sinal do Valor Pago"].ToString().PadRight(1, ' ') : dr["Sinal do Valor Pago"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A121                       
                        strTemp = dr["Valor Pago"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Moeda do pagamento"].ToString().Length < 5 ? dr["Moeda do pagamento"].ToString().PadLeft(5, '0') : dr["Moeda do pagamento"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Código do Banco"].ToString().Length < 5 ? dr["Código do Banco"].ToString().PadLeft(5, '0') : dr["Código do Banco"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Agência"].ToString().Length < 10 ? dr["No da Agência"].ToString().PadLeft(10, '0') : dr["No da Agência"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No da Conta Corrente"].ToString().Length < 12 ? dr["No da Conta Corrente"].ToString().PadLeft(12, '0') : dr["No da Conta Corrente"].ToString().Substring(0,12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["No do Cheque"].ToString().Length < 15 ? dr["No do Cheque"].ToString().PadLeft(15, '0') : dr["No do Cheque"].ToString().Substring(0,15);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor IR_2"].ToString().Length < 1 ? dr["Sinal do Valor IR_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor IR_2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor IR                       
                        strTemp = dr["Valor IR_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                           
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor ISS_2"].ToString().Length < 1 ? dr["Sinal do Valor ISS_2"].ToString().PadRight(1, ' ') : dr["Sinal do Valor ISS_2"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // Valor ISS                       
                        strTemp = dr["Valor ISS_2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor INSS"].ToString().Length < 1 ? dr["Sinal do Valor INSS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor INSS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A132                       
                        strTemp = dr["Valor INSS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor CSLL"].ToString().Length < 1 ? dr["Sinal do Valor CSLL"].ToString().PadRight(1, ' ') : dr["Sinal do Valor CSLL"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;

                        // A134                       
                        strTemp = dr["Valor CSLL"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor COFINS"].ToString().Length < 1 ? dr["Sinal do Valor COFINS"].ToString().PadRight(1, ' ') : dr["Sinal do Valor COFINS"].ToString().Substring(0,1);
                        strFullLengthString = strFullLengthString + strTemp;
                        // A136                       
                        strTemp = dr["Valor COFINS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Sinal do Valor PIS"].ToString().PadRight(1, ' ');
                        strFullLengthString = strFullLengthString + strTemp;
                        // A138                       
                        strTemp = dr["Valor PIS"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            
                            strTemp2 = strArrTemp[1].Length > 2 ? strArrTemp[1].Substring(0, 2) : strArrTemp[1].PadRight(2, '0');
                            strTemp = strArrTemp[0].PadLeft(16, '0') + strTemp2;
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(18, '0');
                        }
                        strTemp = strTemp.Length < 18 ? strTemp.PadLeft(18, '0') : strTemp.Substring(0,18);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Occurrence code"].ToString().Length < 3 ? dr["Occurrence code"].ToString().PadLeft(3, '0') : dr["Occurrence code"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Cheque cancellation reason"].ToString().Length < 60 ? dr["Cheque cancellation reason"].ToString().PadRight(60, ' ') : dr["Cheque cancellation reason"].ToString().Substring(0,60);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Payment method_2"].ToString().Length < 3 ? dr["Payment method_2"].ToString().PadLeft(3, '0') : dr["Payment method_2"].ToString().Substring(0,3);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Number"].ToString().Length < 5 ? dr["Carrier Bank Number"].ToString().PadLeft(5, '0') : dr["Carrier Bank Number"].ToString().Substring(0,5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Branch number"].ToString().Length < 10 ? dr["Carrier Bank Branch number"].ToString().PadLeft(10, '0') : dr["Carrier Bank Branch number"].ToString().Substring(0,10);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Carrier Bank Account number"].ToString().Length < 12 ? dr["Carrier Bank Account number"].ToString().PadLeft(12, '0') : dr["Carrier Bank Account number"].ToString().Substring(0,12);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["Exchange rate2"].ToString();
                        if (strTemp.IndexOf('.') > 0)
                        {
                            strArrTemp = strTemp.Split('.');
                            strTemp = strArrTemp[0].PadLeft(8, '0') + strArrTemp[1].PadRight(7, '0');
                        }
                        else
                        {
                            strTemp = strTemp.PadLeft(15, '0');
                        }

                        strFullLengthString = strFullLengthString + strTemp;

                        //NEW COLUMN ADDED
                        strTemp = dr["INCONSISTENCY_1"].ToString().Length < 5 ? dr["INCONSISTENCY_1"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_1"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_2"].ToString().Length < 5 ? dr["INCONSISTENCY_2"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_2"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_3"].ToString().Length < 5 ? dr["INCONSISTENCY_3"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_3"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_4"].ToString().Length < 5 ? dr["INCONSISTENCY_4"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_4"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;

                        strTemp = dr["INCONSISTENCY_5"].ToString().Length < 5 ? dr["INCONSISTENCY_5"].ToString().PadLeft(5, ' ') : dr["INCONSISTENCY_5"].ToString().Substring(0, 5);
                        strFullLengthString = strFullLengthString + strTemp;                        
                        
                        i = i + 1;

                        string dd = DateTime.Now.Day.ToString();
                        string mon = DateTime.Now.Month.ToString();
                        string yy = DateTime.Now.Year.ToString();
                        string hh = DateTime.Now.Hour.ToString();
                        string min = DateTime.Now.Minute.ToString();

                        dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
                        mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();
                        hh = hh.Length < 2 ? hh.PadLeft(2, '0') : hh.ToString();
                        min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                        if (i != 1)
                            min = strMin;
                        else
                            min = min.Length < 2 ? min.PadLeft(2, '0') : min.ToString();
                         date = dd + mon + yy + hh + min;

                        if (i == 1)
                        {
                            strMin = min;
                        }


                        if (i == 1 )//First Record
                        {   
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 'Y');
                                InsertLog("Pagnet", "Update flag in CLM_ACTIVITY_RESERVE" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Indeminity.", DateTime.Now, DateTime.Now, "Success", "");
                            }
                            catch(Exception ex)
                            {
                                InsertLog("Pagnet", "Update flag in CLM_ACTIVITY_RESERVE" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Indeminity.", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {

                                if (File.Exists(ConfigurationSettings.AppSettings["FilePathClaimInd"] + "ClaimInd" + date + ".txt"))
                                {
                                    File.Delete(ConfigurationSettings.AppSettings["FilePathClaimInd"] + "ClaimInd" + date + ".txt");
                                }
                                //using (System.IO.StreamWriter file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathClaimInd"] + "ClaimInd" + date + ".txt", true, Encoding.ASCII))
                                //{

                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathClaimInd"] + "ClaimInd" + date + ".txt", true, Encoding.ASCII);
                                strBuild.AppendLine("File created for ClaimInd Successfully");

                                if (ds.Tables[0].Rows.Count == 1)
                                    // changes by naveen , tfs 2419
                                    //file.Write(strFullLengthString);
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                else
                                    // changes by naveen , tfs 2419
                                    //file.Write(strFullLengthString);
                                    file.WriteLine(ConvertSpecialChar(strFullLengthString));

                                file.Close();

                                InsertLog("Pagnet", "Record Inserted in" + "ClaimInd" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");

                                //Insert Export Files
                                InsertPagnetExportFiles("PROC_INSERT_PAGNET_EXPORT_FILES", ds.Tables[0].Rows[i - 1]["Interface code"].ToString(), "ClaimInd" + date + ".txt", FileStatus.Generated.ToString(), Dns.GetHostName());
                                InsertLog("Pagnet", "Insert file name " + "ClaimInd" + date + ".txt" + " in PAGNET_EXPORT_FILES", DateTime.Now, DateTime.Now, "Success", "");

                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "ClaimInd" + date + ".txt");
                                //}

                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in " + "ClaimInd" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }
                           
                        }
                        else if (i == ds.Tables[0].Rows.Count)//Last record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 'Y');
                                InsertLog("Pagnet", "Update flag in CLM_ACTIVITY_RESERVE" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Indeminity.", DateTime.Now, DateTime.Now, "Success", "");
                            }
                            catch (Exception ex)
                            {

                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {                                
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathClaimInd"] + "ClaimInd" + date + ".txt", true, Encoding.ASCII);
                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                file.Close();

                                InsertLog("Pagnet", "Record Inserted in " + "ClaimInd" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");                                
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "ClaimInd" + date + ".txt");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in " + "ClaimInd" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);                                
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }


                        }
                        else // Middle Record
                        {
                            try
                            {
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 'Y');
                                InsertLog("Pagnet", "Update flag in CLM_ACTIVITY_RESERVE " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Indeminity.", DateTime.Now, DateTime.Now, "Success", "");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "update flag in CLM_ACTIVITY_RESERVE " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString() + " for Claim Indeminity.", DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                strBuild.AppendLine(ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString());
                                continue;
                            }

                            try
                            {

                                
                                file = new System.IO.StreamWriter(ConfigurationSettings.AppSettings["FilePathClaimInd"] + "ClaimInd" + date + ".txt", true, Encoding.ASCII);
                                // changes by naveen , tfs 2419
                                //file.Write(strFullLengthString);
                                file.WriteLine(ConvertSpecialChar(strFullLengthString));
                                file.Close();

                                InsertLog("Pagnet", "Record Inserted in " + "ClaimInd" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id: " + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Success", "");                                                            
                                updatePagnetExport("PROC_UPDATE_PAGNET_EXPORT", ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), "Y", ds.Tables[0].Rows[i - 1]["Beneficiary Bank Account number"].ToString(), ds.Tables[0].Rows[i - 1]["Payee Bank Account no."].ToString(), ds.Tables[0].Rows[i - 1]["Operation Type"].ToString(), "ClaimInd" + date + ".txt");
                            }
                            catch (Exception ex)
                            {
                                InsertLog("Pagnet", "Record Inserted in " + "ClaimInd" + date + ".txt" + " Interface Code: " + ds.Tables[0].Rows[i - 1]["Interface code"].ToString() + " Payment Id:" + ds.Tables[0].Rows[i - 1]["PAYMENT ID"].ToString(), DateTime.Now, DateTime.Now, "Fail", ex.InnerException.Message);
                                updateCommissionFlag("PROC_UPDATE_COMMISSION_FLAG", "CLM_ACTIVITY_RESERVE", 0, Convert.ToInt32(ds.Tables[0].Rows[i - 1]["CLAIM_ID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i - 1]["ACTIVITY_ID"].ToString()), 'N');
                                strBuild.AppendLine(ex.Message.ToString() + "\r\n" + ex.StackTrace.ToString());
                                continue;

                            }
                            finally
                            {
                                file.Dispose();
                            }

                        }                      

                    }
                    strBuild.AppendLine("Record inserted successfully for Claim Indemenity. ");
                    return strBuild.ToString(); 
                    }

                else
                {
                    strBuild.AppendLine("There is no record to insert for : Claim Idemenity ");
                    return strBuild.ToString();
                    
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Claim Ideminity  " + ex.Message.ToString()+"\r\n"+ ex.StackTrace.ToString()));
                
            }
            finally
            {
                ds.Dispose();
            }

        }

        //public string StrEncodeXML(string SInput)
        //{
        //    int i = 0;
        //    int N = 0;
        //    string S = null;
        //    string C = null;
        //    S = "";
        //    for (N = 1; N <= Strings.Len(SInput); N++)
        //    {
        //        C = "";
        //        i = Strings.Asc(Strings.Mid(SInput, N, 1));
        //        if ((i >= 32 & i <= 127) & (i != 60) & (i != 38))
        //        {
        //            C = Strings.Chr(i).ToString();
        //        }
        //        else if (i >= 192)
        //        {
        //            C = Strings.Chr(195).ToString() + Strings.Chr(i - 64).ToString();
        //        }
        //        else
        //        {
        //            C = "&#" + i + ";";
        //        }
        //        S = S + C;
        //    }
        //    return S;
        //}

        public string ConvertSpecialChar(string big5String)
        {
            Encoding big5 = Encoding.GetEncoding("big5");
            Encoding utf8 = Encoding.GetEncoding("utf-8");

            // convert string to bytes
            byte[] big5Bytes = big5.GetBytes(big5String);

            // convert encoding from big5 to utf8
            byte[] utf8Bytes = Encoding.Convert(big5, utf8, big5Bytes);

            char[] utf8Chars = new char[utf8.GetCharCount(utf8Bytes, 0, utf8Bytes.Length)];
            utf8.GetChars(utf8Bytes, 0, utf8Bytes.Length, utf8Chars, 0);

            return new string(utf8Chars);
        }



        //Some comment portion has been removed at 13 june
        #endregion

    }
//}
