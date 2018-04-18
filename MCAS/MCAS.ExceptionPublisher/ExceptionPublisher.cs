/*********************************************
 * Author       :  Pravesh K Chandel
 * Created Date : 21 Jan 2015
 * Purpose      : define Exception Publisher to publish exceptions of the MCAS application
 * modified By  :
 * Modified Date:
 * Reason       :
 ********************************************/ 

using System;
using System.Collections.Generic;
using MCAS.ExceptionPublisher.ExceptionInterface.ExceptionManagement;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Web;


namespace MCAS.ExceptionPublisher.ExceptionManagement
{
    public class ExceptionPublisher : IExceptionPublisher
    {
        private string m_LogName = @"ErrorLog.txt";

        public ExceptionPublisher()
        {

        }
            
        // Provide implementation of the IExceptionPublisher interface
        // This contains the single Publish method.

        void IExceptionPublisher.Publish(Exception exception,
            NameValueCollection additionalInfo,
            NameValueCollection configSettings)
        {

            DefaultPublisher Publisher = new DefaultPublisher();
            // Publish the exception and any additional information
            Publisher.Publish(exception, additionalInfo,configSettings);

           

            
              
            //// Write the entry to the database
            ////SqlConnection conn	=	new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings.Get("DB_CON_STRING"));
            //SqlConnection conn = new SqlConnection(ExceptionManager.ExceptionConString); // by Pravesh on 19 march
            //try
            //{
            //    conn.Open();
            //    SqlCommand command = new SqlCommand("Proc_InsertExceptionLog", conn);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Parameters.AddWithValue("@exceptiondesc", strInfo.ToString());
            //    command.Parameters.AddWithValue("@CUSTOMER_ID", CustomerId);
            //    command.Parameters.AddWithValue("@APP_ID", AppId);
            //    command.Parameters.AddWithValue("@APP_VERSION_ID", AppVersionId);
            //    command.Parameters.AddWithValue("@POLICY_ID", PolicyId);
            //    command.Parameters.AddWithValue("@POLICY_VERSION_ID", PolicyVersionId);
            //    command.Parameters.AddWithValue("@CLAIM_ID", ClaimId);
            //    command.Parameters.AddWithValue("@QQ_ID", QQId);
            //    command.Parameters.AddWithValue("@SOURCE", Source);
            //    command.Parameters.AddWithValue("@MESSAGE", Message);
            //    command.Parameters.AddWithValue("@CLASS_NAME", ClassName);
            //    command.Parameters.AddWithValue("@METHOD_NAME", MethodName);
            //    command.Parameters.AddWithValue("@QUERY_STRING_PARAMS", QueryStringParams);
            //    command.Parameters.AddWithValue("@LOB_ID", LobId);
            //    command.Parameters.AddWithValue("@USER_ID", UserId);
            //    command.Parameters.AddWithValue("@SYSTEM_ID", SystemId);
            //    command.Parameters.AddWithValue("@EXCEPTION_TYPE", ExceptionType);
            //    command.ExecuteNonQuery();
            //}
            //finally
            //{
            //    if (conn != null)
            //    {
            //        if (conn.State == ConnectionState.Open)
            //        {
            //            conn.Close();
            //        }
            //        conn.Dispose();
            //    }
            //}
        }
    }
}
