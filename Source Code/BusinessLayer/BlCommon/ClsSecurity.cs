/***********************************************************************
	<Author					: Anshuman- > 
	<Start Date				: 27/05/2005-	> 
	<End Date				: - > 
	<Description			: Security related function for user type, user and screen- > 
	<Review Date			: - >
	<Reviewed By			: - >
	
	<Modified Date			: 02/06/2005    
	<Modified By			: Anshuman
	<Purpose				: Added function GetSecurityXML from clscommon to ClsSecurity
************************************************************************/
using System;
using System.Data;
using System.Xml;
using System.Text;
using Cms.DataLayer;
using Cms.Model.Maintenance.Security;
using System.Web;
using System.Collections;
//using System.Web.Services;
//using System.Web.Services.Protocols;


namespace Cms.BusinessLayer.BlCommon
{
	/// <summary>
	/// Summary description for ClsSecurity.
	/// </summary>
	public class ClsSecurity : ClsCommon
	{
		private DataTable ldScreenList;
		private XmlDocument doc;
		private XmlDocument compareDoc;
		public ClsSecurity()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		#region Fetch screen list
		public string GetScreenList(int subSectionId, int userTypeId, int userId)
		{
			return GetScreenList(subSectionId, userTypeId, userId, 0);
		}

		public string GetScreenList(int subSectionId, int userTypeId, int userId, int agency_level)
		{
			string strMenuScreenProc		=	"proc_GetMNT_SCREENS_SECURITY_LIST";
			DataWrapper objDataWrapper		=	new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@SUB_SECTION_ID",subSectionId);
				if(userTypeId != 0)
					objDataWrapper.AddParameter("@USER_TYPE_ID",userTypeId);
				if(userId != 0)
					objDataWrapper.AddParameter("@USER_ID",userId);
				if(agency_level == 1)
					objDataWrapper.AddParameter("@CALLED_FOR","Agency");

				ldScreenList				=	objDataWrapper.ExecuteDataSet(strMenuScreenProc).Tables[0];
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			try
			{
				DataView dv = ldScreenList.DefaultView;
				dv.Sort = "level1_id,level2_id,level3_id,level4_id";
				
				doc				= new XmlDocument();
				XmlElement root = doc.CreateElement("root");
				for(int i=0; i<dv.Table.Rows.Count; i++)
				{
					XmlElement screen = doc.CreateElement("screen");
					screen.SetAttribute("level1",dv[i]["level1"].ToString());
					screen.SetAttribute("level1_id",dv[i]["level1_id"].ToString());
					screen.SetAttribute("level2",dv[i]["level2"].ToString());
					screen.SetAttribute("level2_id",dv[i]["level2_id"].ToString());
					screen.SetAttribute("level3",dv[i]["level3"].ToString());
					screen.SetAttribute("level3_id",dv[i]["level3_id"].ToString());
					screen.SetAttribute("level4",dv[i]["level4"].ToString());
					screen.SetAttribute("level4_id",dv[i]["level4_id"].ToString());
					screen.SetAttribute("screen_id",dv[i]["screen_id"].ToString());
					screen.SetAttribute("screen_desc",dv[i]["screen_desc"].ToString());
					screen.SetAttribute("screen_read",dv[i]["screen_read"].ToString());
					screen.SetAttribute("screen_write",dv[i]["screen_write"].ToString());
					screen.SetAttribute("screen_delete",dv[i]["screen_delete"].ToString());
					screen.SetAttribute("screen_execute",dv[i]["screen_execute"].ToString());					
					if(dv[i]["module_name"]!=null)
					{
						if(dv[i]["module_name"].ToString().ToUpper()=="POL")
							screen.SetAttribute("module_name","Policy");
						else
							screen.SetAttribute("module_name","");
					}
					if(dv[i]["permission_xml"].ToString() != "")
					{
						CreateSecurityElement(dv[i]["permission_xml"].ToString(),screen);
					}
					root.AppendChild(screen);
				}
				return root.OuterXml;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
        public string GetScreen(int MODULE, int SUB_MODULE, int CARRIER_ID)//changes by praveer for TFS# 738
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);//changes by praveer for TFS# 738
            try
            {
            string strMenuScreenProc = "SP_GET_MENU_LIST";           
            objDataWrapper.AddParameter("@MODULE", MODULE);
            objDataWrapper.AddParameter("@SUB_MODULE", SUB_MODULE);
            objDataWrapper.AddParameter("@CARRIER_ID", CARRIER_ID);//changes by praveer for TFS# 738
            ldScreenList = objDataWrapper.ExecuteDataSet(strMenuScreenProc).Tables[0];
            objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        
            
				DataView dv = ldScreenList.DefaultView;
				dv.Sort = "level1_id,level2_id,level3_id";
				
				doc				= new XmlDocument();
				XmlElement root = doc.CreateElement("root");
				for(int i=0; i<dv.Table.Rows.Count; i++)
				{
					XmlElement screen = doc.CreateElement("screen");
					screen.SetAttribute("level1",dv[i]["level1"].ToString());
					screen.SetAttribute("level1_id",dv[i]["level1_id"].ToString());
					screen.SetAttribute("level2",dv[i]["level2"].ToString());
					screen.SetAttribute("level2_id",dv[i]["level2_id"].ToString());
					screen.SetAttribute("level3",dv[i]["level3"].ToString());
					screen.SetAttribute("level3_id",dv[i]["level3_id"].ToString());				
					screen.SetAttribute("screen_id",dv[i]["screen_id"].ToString());
					screen.SetAttribute("screen_desc",dv[i]["screen_desc"].ToString());
                    screen.SetAttribute("is_active", dv[i]["is_active"].ToString());		
					root.AppendChild(screen);
				}
				return root.OuterXml;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);//changes by praveer for TFS# 738
                throw ex;
            }
            
        }
        public DataSet GetModule()
        {
            string strMenuScreenProc = "SP_GET_MENU_LIST";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);   //changes by praveer for TFS# 738
           try
            {
                DataSet ds = objDataWrapper.ExecuteDataSet(strMenuScreenProc);               
                return ds;
            }
           catch (Exception ex)
           {
               throw ex;
           }

        }

        public DataTable GetCarrier()
        {
            string strCarrierProc = "PROC_GET_CARRIER_LIST";//changes by praveer for TFS# 738
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);//changes by praveer for TFS# 738
            try
            {
                ldScreenList = objDataWrapper.ExecuteDataSet(strCarrierProc).Tables[0];               
                return ldScreenList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateMenuList(ArrayList ArlObjClausesInfo, int CARRIER_ID)//changes by praveer for TFS# 738
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);//changes by praveer for TFS# 738
            try
            {               
                int intretval = 0;
                string procMenuList = "SP_DEACTIVATE_MENU_LIST";

                for (int i = 0; i < ArlObjClausesInfo.Count; i++)
                {
                    string MENU_ISACTIVE = ArlObjClausesInfo[i].ToString();
                    objDataWrapper.AddParameter("@CARRIER_ID", CARRIER_ID);//changes by praveer for TFS# 738
                    objDataWrapper.AddParameter("@MENU_ISACTIVE", MENU_ISACTIVE);
                    intretval = objDataWrapper.ExecuteNonQuery(procMenuList);
                    objDataWrapper.ClearParameteres();


                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return intretval;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);//changes by praveer for TFS# 738
                throw ex;
            }

          }

		public string GetScreenList(int subSectionId, int userTypeId, int userId,string moduleName)
		{
			return GetScreenList(subSectionId, userTypeId, userId,moduleName, 0);
		}
		
		public string GetScreenList(int subSectionId, int userTypeId, int userId,string moduleName, int agency_level)
		{
			string strMenuScreenProc		=	"proc_GetMNT_SCREENS_SECURITY_LIST";
			DataWrapper objDataWrapper		=	new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@SUB_SECTION_ID",subSectionId);
				objDataWrapper.AddParameter("@USER_TYPE_ID",userTypeId);
				if(userId != 0)
					objDataWrapper.AddParameter("@USER_ID",userId);
				if(agency_level == 1)
					objDataWrapper.AddParameter("@CALLED_FOR","Agency");
				objDataWrapper.AddParameter("@MODULE_NAME",moduleName);				
				
				ldScreenList				=	objDataWrapper.ExecuteDataSet(strMenuScreenProc).Tables[0];
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			try
			{
				DataView dv = ldScreenList.DefaultView;
				dv.Sort = "level1_id,level2_id,level3_id,level4_id";
				
				doc				= new XmlDocument();
				XmlElement root = doc.CreateElement("root");
				for(int i=0; i<dv.Table.Rows.Count; i++)
				{
					XmlElement screen = doc.CreateElement("screen");
					screen.SetAttribute("level1",dv[i]["level1"].ToString());
					screen.SetAttribute("level1_id",dv[i]["level1_id"].ToString());
					screen.SetAttribute("level2",dv[i]["level2"].ToString());
					screen.SetAttribute("level2_id",dv[i]["level2_id"].ToString());
					screen.SetAttribute("level3",dv[i]["level3"].ToString());
					screen.SetAttribute("level3_id",dv[i]["level3_id"].ToString());
					screen.SetAttribute("level4",dv[i]["level4"].ToString());
					screen.SetAttribute("level4_id",dv[i]["level4_id"].ToString());
					screen.SetAttribute("screen_id",dv[i]["screen_id"].ToString());
					screen.SetAttribute("screen_desc",dv[i]["screen_desc"].ToString());
					screen.SetAttribute("screen_read",dv[i]["screen_read"].ToString());
					screen.SetAttribute("screen_write",dv[i]["screen_write"].ToString());
					screen.SetAttribute("screen_delete",dv[i]["screen_delete"].ToString());
					screen.SetAttribute("screen_execute",dv[i]["screen_execute"].ToString());					
					if(dv[i]["module_name"]!=null)
					{
						if(dv[i]["module_name"].ToString().ToUpper()=="POL")
							screen.SetAttribute("module_name","Policy");
						else
							screen.SetAttribute("module_name","");
					}
					if(dv[i]["permission_xml"].ToString() != "")
					{
						CreateSecurityElement(dv[i]["permission_xml"].ToString(),screen);
					}
					root.AppendChild(screen);
				}
				return root.OuterXml;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void CreateSecurityElement(string securityXML, XmlElement screen)
		{
			XmlDocument docSecurity		=	new XmlDocument();
			docSecurity.LoadXml(securityXML);
			string read,write,delete,execute;
			try
			{
				read	=	docSecurity.FirstChild.SelectSingleNode("Read").InnerText;
				write	=	docSecurity.FirstChild.SelectSingleNode("Write").InnerText;
				delete	=	docSecurity.FirstChild.SelectSingleNode("Delete").InnerText;
				execute	=	docSecurity.FirstChild.SelectSingleNode("Execute").InnerText;
			}
			catch
			{
				read	=	"N";
				write	=	"N";
				delete	=	"N";
				execute	=	"N";
			}
			screen.SetAttribute("permission_read",read);
			screen.SetAttribute("permission_write",write);
			screen.SetAttribute("permission_delete",delete);
			screen.SetAttribute("permission_execute",execute);
		}
		#endregion

		#region Add records to USER_TYPE_PERMISSION or USER_PERMISSION
		public int AddSecurity(ClsSecurityInfo objSecurityInfo)
		{
			string		INSERT_UPADTE_PROC	=	"Proc_InsertUpdate_USERTYPE_SECURITY";
			DataWrapper objDataWrapper		=	new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				if(objSecurityInfo.USER_ID == 0)
				{
					objDataWrapper.AddParameter("@USER_TYPE_ID",objSecurityInfo.USER_TYPE_ID);
				}
				else
				{
					INSERT_UPADTE_PROC		=	"Proc_InsertUpdate_USER_SECURITY";
					objDataWrapper.AddParameter("@USER_ID",objSecurityInfo.USER_ID);
				}
				objDataWrapper.AddParameter("@SCREEN_ID",objSecurityInfo.SCREEN_ID);
				objDataWrapper.AddParameter("@PERMISSION_XML",objSecurityInfo.PERMISSION_XML);
				objDataWrapper.AddParameter("@IS_ACTIVE",objSecurityInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objSecurityInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objSecurityInfo.CREATED_DATETIME);
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	2;
					objTransactionInfo.RECORDED_BY		=	objSecurityInfo.CREATED_BY;
					if(objSecurityInfo.USER_ID == 0)
						objTransactionInfo.TRANS_DESC	=	"For user_type_id=" + objSecurityInfo.USER_TYPE_ID + " security rights has been set for screen_id=" + objSecurityInfo.SCREEN_ID;
					else
						objTransactionInfo.TRANS_DESC	=	"For user_id=" + objSecurityInfo.USER_ID + " security rights has been set for screen_id=" + objSecurityInfo.SCREEN_ID;
					returnResult						=	objDataWrapper.ExecuteNonQuery(INSERT_UPADTE_PROC,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(INSERT_UPADTE_PROC);
				}
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}
		#endregion

		#region Comparing existing permission with new permission
		public void LoadOldData(string xmlString)
		{
			compareDoc	=	new XmlDocument();
			compareDoc.LoadXml(xmlString);
		}
		public bool ComparePermission(string screenId, string permissionXML)
		{
			XmlDocument permissionDoc	=	new XmlDocument();
			bool		compareResult	=	false;
			permissionDoc.LoadXml(permissionXML);
			string	permissionRead		=	permissionDoc.FirstChild.SelectSingleNode("Read").InnerText;
			string	permissionWrite		=	permissionDoc.FirstChild.SelectSingleNode("Write").InnerText;
			string	permissionDelete	=	permissionDoc.FirstChild.SelectSingleNode("Delete").InnerText;
			string	permissionExecute	=	permissionDoc.FirstChild.SelectSingleNode("Execute").InnerText;
			foreach(XmlNode screenNode in compareDoc.FirstChild.SelectNodes("screen"))
			{
				if(screenNode.Attributes["screen_id"].Value == screenId)
				{
					if((permissionRead		==	(screenNode.Attributes["permission_read"] == null ? "" : screenNode.Attributes["permission_read"].Value))&&
						(permissionWrite	==	(screenNode.Attributes["permission_write"] == null ? "" : screenNode.Attributes["permission_write"].Value))&&
						(permissionDelete	==	(screenNode.Attributes["permission_delete"] == null ? "" : screenNode.Attributes["permission_delete"].Value))&&
						(permissionExecute	==	(screenNode.Attributes["permission_execute"] == null ? "" : screenNode.Attributes["permission_execute"].Value)))
					{
						compareResult	=	true;
					}
					break;
				}
			}
			return compareResult;
		}
		#endregion

		#region Get security xml aginst a user and a screen
		/// <summary>
		/// fetch security xml against a user and a screen
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="userTypeId"></param>
		/// <param name="screenId"></param>
		/// <returns></returns>
		public static string GetSecurityXML(int userId, int userTypeId, string screenId)
		{
			string strStoredProc		= "proc_GetMNT_USER_PERMISSION";
			string strSecuityXml		= "";
			DataWrapper objDataWrapper	= new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				objDataWrapper.AddParameter("@USER_ID",userId);
				objDataWrapper.AddParameter("@USER_TYPE_ID",userTypeId);
				objDataWrapper.AddParameter("@SCREEN_ID",screenId);

				DataSet ds		= objDataWrapper.ExecuteDataSet(strStoredProc);
				if(ds.Tables[0].Rows.Count != 0)
				{
					strSecuityXml	= ds.Tables[0].Rows[0]["PERMISSION_XML"].ToString();
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(objDataWrapper != null)
				{
					objDataWrapper.Dispose();
				}
			}
			return strSecuityXml;
		}
		#endregion
	}
	/// <summary>
	/// Web Service Authentication 
	/// </summary>
	public class AuthenticationToken : System.Web.Services.Protocols.SoapHeader
	{
		public string TokenValue;		
	}


}
