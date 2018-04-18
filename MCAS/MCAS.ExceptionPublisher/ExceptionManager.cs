/*********************************************
 * Author       :  Pravesh K Chandel
 * Created Date : 21 Jan 2015
 * Purpose      : define exception Manager to publish exceptions of the application
 * modified By  :
 * Modified Date:
 * Reason       :
 ********************************************/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Security;
using System.Security.Principal;
using System.Security.Permissions;
using System.Data;
using System.Data.SqlClient;
using System.Resources;
using System.Reflection;
using System.Configuration;
using MCAS.ExceptionPublisher.ExceptionInterface.ExceptionManagement;
using System.Threading;
using System.Web;
using System.Collections;

namespace MCAS.ExceptionPublisher.ExceptionManagement
{
    #region ExceptionManager Class
    /// <summary>
    /// The Exception Manager class manages the publishing of exception information based on settings in the configuration file.
    /// </summary>
    public sealed class ExceptionManager
    {
        /// <summary>
        /// Private constructor to restrict an instance of this class from being created.
        /// </summary>
        private ExceptionManager()
        {
        }
        private static string mCnnString;
        private static string mExcepionProcName; 

        public static string ExceptionConString 
        {
            get
            {
                return mCnnString;
            }
            set
            {
                mCnnString = value;
            }
        }
        public static string ExceptionProcName 
        {
            get
            {
                return mExcepionProcName;
            }
            set
            {
                mExcepionProcName = value;
            }
        }
        private static Hashtable _htParameters = new Hashtable();

        public static Hashtable Parameters
        {
            get
            {
                return _htParameters;
            }
            set
            {
                _htParameters = value;
            }
        }
        // Member variable declarations
        private const string EXCEPTIONMANAGEMENT_CONFIG_SECTION = "exceptionManagement";
        private readonly static string EXCEPTIONMANAGER_NAME = typeof(ExceptionManager).Name;

        // Resource Manager for localized text.
        private static ResourceManager resourceManager = new ResourceManager(typeof(ExceptionManager).Namespace + ".ExceptionManager", Assembly.GetAssembly(typeof(ExceptionManager)));

        /// <summary>
        /// Static method to publish the exception information.
        /// </summary>
        /// <param name="exception">The exception object whose information should be published.</param>
        public static void Publish(Exception exception)
        {
            ExceptionManager.Publish(exception, null);
        }

        /// <summary>
        /// Static method to publish the exception information and any additional information.
        /// </summary>
        /// <param name="exception">The exception object whose information should be published.</param>
        /// <param name="additionalInfo">A collection of additional data that should be published along with the exception information.</param>
        public static void Publish(Exception exception, NameValueCollection additionalInfo)
        {
            try
            {
                #region Load the AdditionalInformation Collection with environment data.
                // Create the Additional Information collection if it does not exist.
                if (null == additionalInfo) additionalInfo = new NameValueCollection();

                // Add environment information to the information collection.
                try
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".MachineName", Environment.MachineName);
                }
                catch (SecurityException)
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".MachineName", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_PERMISSION_DENIED"));
                }
                catch
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".MachineName", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION"));
                }

                try
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".TimeStamp", DateTime.Now.ToString());
                }
                catch (SecurityException)
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".TimeStamp", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_PERMISSION_DENIED"));
                }
                catch
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".TimeStamp", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION"));
                }

                try
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".FullName", Assembly.GetExecutingAssembly().FullName);
                }
                catch (SecurityException)
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".FullName", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_PERMISSION_DENIED"));
                }
                catch
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".FullName", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION"));
                }

                try
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".AppDomainName", AppDomain.CurrentDomain.FriendlyName);
                }
                catch (SecurityException)
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".AppDomainName", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_PERMISSION_DENIED"));
                }
                catch
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".AppDomainName", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION"));
                }

                try
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".ThreadIdentity", Thread.CurrentPrincipal.Identity.Name);
                }
                catch (SecurityException)
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".ThreadIdentity", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_PERMISSION_DENIED"));
                }
                catch
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".ThreadIdentity", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION"));
                }

                try
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".WindowsIdentity", WindowsIdentity.GetCurrent().Name);
                }
                catch (SecurityException)
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".WindowsIdentity", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_PERMISSION_DENIED"));
                }
                catch
                {
                    additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".WindowsIdentity", resourceManager.GetString("RES_EXCEPTIONMANAGEMENT_INFOACCESS_EXCEPTION"));
                }

                #endregion

                #region Publish the exception based on Configuration Settings
                // Check for any settings in config file.
                //if (ConfigurationManager.GetSection(EXCEPTIONMANAGEMENT_CONFIG_SECTION) == null)
                {

                    // Publish the exception and additional information to the default publisher if no settings are present.
                    PublishToDefaultPublisher(exception, additionalInfo);
                }
                //else
                //{
      
                //} // End else statement when config settings are provided.
                #endregion
            }
            catch (Exception e)
            {
                // Publish the exception thrown within the ExceptionManager.
                PublishInternalException(e, null);

                // Publish the original exception and additional information to the default publisher.
                PublishToDefaultPublisher(exception, additionalInfo);
            }
        } // End Publish(Exception exception, NameValueCollection AdditionalInfo)

     

        /// <summary>
        /// Private static helper method to publish the exception information to the default publisher.
        /// </summary>
        /// <param name="exception">The exception object whose information should be published.</param>
        /// <param name="additionalInfo">A collection of additional data that should be published along with the exception information.</param>
        internal static void PublishInternalException(Exception exception, NameValueCollection additionalInfo)
        {
            // Get the Default Publisher
            DefaultPublisher Publisher = new DefaultPublisher("Application", resourceManager.GetString("RES_EXCEPTIONMANAGER_INTERNAL_EXCEPTIONS"));

            // Publish the exception and any additional information
            Publisher.Publish(exception, additionalInfo, null);
        }

        /// <summary>
        /// Private helper function to assist in run-time activations. Returns
        /// an object from the specified assembly and type.
        /// </summary>
        /// <param name="assembly">Name of the assembly file (w/out extension)</param>
        /// <param name="typeName">Name of the type to create</param>
        /// <returns>Instance of the type specified in the input parameters.</returns>
        private static object Activate(string assembly, string typeName)
        {
            return AppDomain.CurrentDomain.CreateInstanceAndUnwrap(assembly, typeName);
        }
        private static void PublishToDefaultPublisher(Exception exception, NameValueCollection additionalInfo)
        {
            // Get the Default Publisher
            DefaultPublisher Publisher = new DefaultPublisher();

            // Publish the exception and any additional information
            Publisher.Publish(exception, additionalInfo, null);
        }
    }
    #endregion

    #region DefaultPublisher Class
    /// <summary>
    /// Component used as the default publishing component if one is not specified in the config file.
    /// </summary>
    public sealed class DefaultPublisher : IExceptionPublisher
    {
        //private static string mCnnString;

        //public static string CnnString
        //{
        //    get
        //    {
        //        return mCnnString;
        //    }
        //    set
        //    {
        //        mCnnString = value;
        //    }
        //}



        /// <summary>
        /// Default Constructor.
        /// </summary>
        public DefaultPublisher()
        {
        }

        /// <summary>
        /// Constructor allowing the log name and application names to be set.
        /// </summary>
        /// <param name="logName">The name of the log for the DefaultPublisher to use.</param>
        /// <param name="applicationName">The name of the application.  This is used as the Source name in the event log.</param>
        public DefaultPublisher(string logName, string applicationName)
        {
            this.logName = logName;
            this.applicationName = applicationName;
        }

        private static ResourceManager resourceManager = new ResourceManager(typeof(ExceptionManager).Namespace + ".ExceptionManager", Assembly.GetAssembly(typeof(ExceptionManager)));

        // Member variable declarations
        private string logName = "Application";
        private string applicationName = "ExceptionManagerPublishedException"; //resourceManager.GetString("RES_EXCEPTIONMANAGER_PUBLISHED_EXCEPTIONS");
        private const string TEXT_SEPARATOR = "*********************************************";

        public void Publish(Exception exception,
            NameValueCollection additionalInfo,
            NameValueCollection configSettings)
        {

            string Source, Message, ClassName, MethodName, QueryStringParams, SystemId, ExceptionType;
            Source = Message = ClassName = MethodName = QueryStringParams = SystemId = ExceptionType = "";
            // Create StringBuilder to maintain publishing information.
            StringBuilder strInfo = new StringBuilder();
            try
            {
                //if (((System.Reflection.RuntimeMethodInfo)(((System.Reflection.MethodBase)(exception.TargetSite)))).ReflectedType.ToString() != "")
                //    ClassName = ((System.Reflection.RuntimeMethodInfo)(((System.Reflection.MethodBase)(exception.TargetSite)))).ReflectedType;

                //if (((System.Reflection.RuntimeMethodInfo)(((System.Reflection.MethodBase)(exception.TargetSite)))).Name != "")
                //    MethodName = ((System.Reflection.RuntimeMethodInfo)(((System.Reflection.MethodBase)(exception.TargetSite)))).Name;

                Source = exception.Source;
                Message = exception.Message;
                ExceptionType = exception.GetType().Name.ToString();
                // Load Config values if they are provided.
                //if (configSettings != null)
                //{
                //    if (configSettings["fileName"] != null &&
                //        configSettings["fileName"].Length > 0)
                //    {
                //        m_LogName = configSettings["fileName"];
                //    }
                //}

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

                //Iterate Through Inner Exceptions and publish the details 
                Exception objEx = exception;
                strInfo.Append("Exception Information : ");
                while (true)
                {
                    if (objEx == null)
                        break;
                    strInfo.Append(objEx.Message);
                    objEx = objEx.InnerException;
                    if (objEx == null)
                        break;

                    strInfo.Append(" :: Inner Exception Information : ");
                }

                if (exception != null)
                    strInfo.AppendFormat(Environment.NewLine + " : " + exception.StackTrace);
                else
                    strInfo.AppendFormat("{0}{0}No Exception.{0}", Environment.NewLine);
            }
            catch
            {
            }

            if (ExceptionManager.Parameters.ContainsKey("@SOURCE"))
                ExceptionManager.Parameters["@SOURCE"] = ExceptionManager.Parameters["@SOURCE"].ToString() + " : " + Source;
            else
                ExceptionManager.Parameters.Add("@SOURCE", Source);

            if (ExceptionManager.Parameters.ContainsKey("@MESSAGE"))
                ExceptionManager.Parameters["@MESSAGE"] = ExceptionManager.Parameters["@MESSAGE"].ToString() + " : " + Message;
            else
                ExceptionManager.Parameters.Add("@MESSAGE", Message);

            if (ExceptionManager.Parameters.ContainsKey("@CLASS_NAME"))
                ExceptionManager.Parameters["@CLASS_NAME"] = ExceptionManager.Parameters["@CLASS_NAME"].ToString() + " : " + ClassName;
            else
                ExceptionManager.Parameters.Add("@CLASS_NAME", ClassName);

            if (ExceptionManager.Parameters.ContainsKey("@METHOD_NAME"))
                ExceptionManager.Parameters["@METHOD_NAME"] = ExceptionManager.Parameters["@METHOD_NAME"].ToString() + " : " + MethodName;
            else
                ExceptionManager.Parameters.Add("@METHOD_NAME", MethodName);

            if (ExceptionManager.Parameters.ContainsKey("@EXCEPTION_TYPE"))
                ExceptionManager.Parameters["@EXCEPTION_TYPE"] = ExceptionManager.Parameters["@EXCEPTION_TYPE"].ToString() + " : " + ExceptionType;
            else
                ExceptionManager.Parameters.Add("@EXCEPTION_TYPE", ExceptionType);

            if (ExceptionManager.Parameters.ContainsKey("@exceptiondesc"))
                ExceptionManager.Parameters["@exceptiondesc"] = ExceptionManager.Parameters["@exceptiondesc"].ToString() + " : " + strInfo.ToString();
            else
                ExceptionManager.Parameters.Add("@exceptiondesc", strInfo.ToString());      

            // Write the entry to the database
            SqlConnection conn = new SqlConnection(ExceptionManager.ExceptionConString);
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(ExceptionManager.ExceptionProcName , conn);
                command.CommandType = CommandType.StoredProcedure;

                foreach (var parm in ExceptionManager.Parameters.Keys)
                {
                    SqlParameter sqlPrm = new SqlParameter(parm.ToString(), ExceptionManager.Parameters[parm].ToString());
                    command.Parameters.Add(sqlPrm);
                }
                command.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }
        }

    }


    #endregion
}
