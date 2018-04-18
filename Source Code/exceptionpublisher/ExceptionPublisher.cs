using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.ExceptionInterface.ExceptionManagement;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Cms.CustomException.CustomPublisher
{
	public class ExceptionPublisher : IExceptionPublisher
	{
		private string m_LogName = @"C:\ErrorLog.txt";
		
		public ExceptionPublisher()
		{

		}

		// Provide implementation of the IExceptionPublisher interface

		// This contains the single Publish method.

		void IExceptionPublisher.Publish(Exception exception, 
			NameValueCollection additionalInfo, 
			NameValueCollection configSettings)
		{

			int CustomerId, AppId, AppVersionId, PolicyId, PolicyVersionId, ClaimId,QQId, UserId,LobId;
			string Source, Message, ClassName,MethodName, QueryStringParams,SystemId, ExceptionType;

			CustomerId=AppId=AppVersionId=PolicyId=PolicyVersionId=ClaimId=QQId=UserId=LobId=0;
			Source=Message=ClassName=MethodName=QueryStringParams=SystemId=ExceptionType="";
			//Load  the above defined variables
			if(HttpContext.Current.Session["userId"]!=null && HttpContext.Current.Session["userId"].ToString()!="")
				UserId = Convert.ToInt32(HttpContext.Current.Session["userId"].ToString());			

			if(HttpContext.Current.Session["systemId"]!=null && HttpContext.Current.Session["systemId"].ToString()!="")
				SystemId = HttpContext.Current.Session["systemId"].ToString();

			if(HttpContext.Current.Session["appID"]!=null && HttpContext.Current.Session["appID"].ToString()!="")
				AppId = Convert.ToInt32(HttpContext.Current.Session["appID"].ToString());

			if(HttpContext.Current.Session["customerID"]!=null && HttpContext.Current.Session["customerID"].ToString()!="")
				CustomerId = Convert.ToInt32(HttpContext.Current.Session["customerID"].ToString());

			if(HttpContext.Current.Session["appVersionID"]!=null && HttpContext.Current.Session["appVersionID"].ToString()!="")
				AppVersionId = Convert.ToInt32(HttpContext.Current.Session["appVersionID"].ToString());

			if(HttpContext.Current.Session["policyID"]!=null && HttpContext.Current.Session["policyID"].ToString()!="")
				PolicyId = Convert.ToInt32(HttpContext.Current.Session["policyID"].ToString());

			if(HttpContext.Current.Session["policyVersionID"]!=null && HttpContext.Current.Session["policyVersionID"].ToString()!="")
				PolicyVersionId = Convert.ToInt32(HttpContext.Current.Session["policyVersionID"].ToString());

			if(HttpContext.Current.Session["claimID"]!=null && HttpContext.Current.Session["claimID"].ToString()!="")
				ClaimId = Convert.ToInt32(HttpContext.Current.Session["claimID"].ToString());

			if(HttpContext.Current.Session["lobID"]!=null && HttpContext.Current.Session["lobID"].ToString()!="")
				LobId = Convert.ToInt32(HttpContext.Current.Session["lobID"].ToString());

			//if(HttpContext.Current.Session["QQId"]!=null && HttpContext.Current.Session["QQId"].ToString()!="")
			//	QQId = Convert.ToInt16(HttpContext.Current.Session["QQId"].ToString());

//			if(((System.Reflection.RuntimeMethodInfo)(((System.Reflection.MethodBase)(exception.TargetSite)))).ReflectedType.ToString()!="")
//				ClassName = ((System.Reflection.RuntimeMethodInfo)(((System.Reflection.MethodBase)(exception.TargetSite)))).ReflectedType;
//
//			if(((System.Reflection.RuntimeMethodInfo)(((System.Reflection.MethodBase)(exception.TargetSite)))).Name!="")
//				MethodName = ((System.Reflection.RuntimeMethodInfo)(((System.Reflection.MethodBase)(exception.TargetSite)))).Name;
			Source = exception.Source;
			Message = exception.Message;
			ExceptionType = exception.GetType().Name.ToString();
			
		// Load Config values if they are provided.
		if (configSettings != null)
			{
				if (configSettings["fileName"] != null &&  
					configSettings["fileName"].Length > 0)
				{  
					m_LogName = configSettings["fileName"];
				}
			}

			// Create StringBuilder to maintain publishing information.
			StringBuilder strInfo = new StringBuilder();

			// Record the contents of the additionalInfo collection.
			if (additionalInfo != null)
			{
				// Record General information.
				strInfo.AppendFormat("{0}General Information {0}", Environment.NewLine);
				strInfo.AppendFormat("{0}Additonal Info:", Environment.NewLine);
				foreach (string i in additionalInfo)
				{
					strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, i, 
						additionalInfo.Get(i));
				}
			}
		
			//Added By Ravindra (01-22-2007) 
			//Iterate Through Inner Exceptions and publish the details 
			Exception objEx = exception;
			strInfo.Append("Exception Information : ");
			while(true)
			{
				if(objEx == null)
					break;
				strInfo.Append(objEx.Message);				
			    objEx = objEx.InnerException;
				if(objEx == null)
					break;

				strInfo.Append(" :: Inner Exception Information : ");
			}

			if ( exception != null )
				strInfo.AppendFormat(Environment.NewLine +  " : " + exception.StackTrace);
			else
				strInfo.AppendFormat("{0}{0}No Exception.{0}", Environment.NewLine);


			//Commented By Ravindra
			// Append the exception text
//			if ( exception != null )
//				strInfo.AppendFormat("{0}Exception Information {1}",
//					Environment.NewLine, exception.Message.ToString()+ " : " + exception.StackTrace);
//			else
//				strInfo.AppendFormat("{0}{0}No Exception.{0}", Environment.NewLine);

			// Write the entry to the database
            //SqlConnection conn	=	new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings.Get("DB_CON_STRING"));
            
            string ExcepConnStr = ExceptionManager.ExceptionConString;
            if (!ExceptionManager.IsEODProcess)
            {
                if (HttpContext.Current.Session["ConnStr"] != null && HttpContext.Current.Session["ConnStr"].ToString() != "")
                    ExcepConnStr = DefaultPublisher.DecryptString(HttpContext.Current.Session["ConnStr"].ToString());
                else
                    System.Web.HttpContext.Current.Response.Redirect("/cms/cmsweb/aspx/login.aspx", true);
            }
            SqlConnection conn = new SqlConnection(ExcepConnStr); // by Pravesh on 19 march
			try
			{
				conn.Open();
				SqlCommand command	=	new SqlCommand("Proc_InsertExceptionLog",conn);
				command.CommandType	=	CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@exceptiondesc", strInfo.ToString());
                command.Parameters.AddWithValue("@CUSTOMER_ID", CustomerId);
                command.Parameters.AddWithValue("@APP_ID", AppId);
                command.Parameters.AddWithValue("@APP_VERSION_ID", AppVersionId);
                command.Parameters.AddWithValue("@POLICY_ID", PolicyId);
                command.Parameters.AddWithValue("@POLICY_VERSION_ID", PolicyVersionId);
                command.Parameters.AddWithValue("@CLAIM_ID", ClaimId);
                command.Parameters.AddWithValue("@QQ_ID", QQId);
                command.Parameters.AddWithValue("@SOURCE", Source);
                command.Parameters.AddWithValue("@MESSAGE", Message);
                command.Parameters.AddWithValue("@CLASS_NAME", ClassName);
                command.Parameters.AddWithValue("@METHOD_NAME", MethodName);
                command.Parameters.AddWithValue("@QUERY_STRING_PARAMS", QueryStringParams);
                command.Parameters.AddWithValue("@LOB_ID", LobId);
                command.Parameters.AddWithValue("@USER_ID", UserId);
                command.Parameters.AddWithValue("@SYSTEM_ID", SystemId);
                command.Parameters.AddWithValue("@EXCEPTION_TYPE", ExceptionType);
				command.ExecuteNonQuery();
			}
			finally
			{
				if(conn != null) 
				{
					if(conn.State	==	ConnectionState.Open)
					{
						conn.Close();
					}
					conn.Dispose();
				}
			}
        }
       
	}
}