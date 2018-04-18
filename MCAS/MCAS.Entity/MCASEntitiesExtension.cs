/******************************************************************************************
<Author				: -   Pravesh K Chandel
<Start Date			: -	 7 April 2014
<End Date			: -	
<Description		: - to track the Audit Trail/ Changes in Data By User
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;

using System.Data.EntityClient;
using System.Collections;
using System.Data.SqlClient;
using System.Xml.Serialization;


namespace MCAS.Entity
{
    public partial class CLM_MandateSummary : EntityObject
    {
        public CLM_MandateSummary ShallowCopy()
        {
            return (CLM_MandateSummary)this.MemberwiseClone();
        }
    }
    public partial class CLM_MandateDetails : EntityObject
    {
        public CLM_MandateDetails ShallowCopy()
        {
            return (CLM_MandateDetails)this.MemberwiseClone();
        }
    }
    public partial class CLM_PaymentSummary : EntityObject
    {
        public CLM_PaymentSummary ShallowCopy()
        {
            return (CLM_PaymentSummary)this.MemberwiseClone();
        }
    }
    public partial class CLM_PaymentDetails : EntityObject
    {
        public CLM_PaymentDetails ShallowCopy()
        {
            return (CLM_PaymentDetails)this.MemberwiseClone();
        }
    }
    public partial class MCASEntities
    {
        public string UserName { get; set; }
        public List<MNT_TransactionAuditLog> tranAuditTrailList = new List<MNT_TransactionAuditLog>();

        public enum AuditActions
        {
            I,
            U,
            D
        }

        partial void OnContextCreated()
        {
            this.SavingChanges += new EventHandler(MCASEntities_SavingChanges);
        }

        void MCASEntities_SavingChanges(object sender, EventArgs e)
        {
            IEnumerable<ObjectStateEntry> changes = this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified);
            foreach (ObjectStateEntry stateEntryEntity in changes)
            {
                if (!stateEntryEntity.IsRelationship &&
                        stateEntryEntity.Entity != null &&
                            !(stateEntryEntity.Entity is MNT_TransactionAuditLog))
                {//is a normal entry, not a relationship
                    var uid = "";
                    var TypeId = "";
                    var modified = "";
                    try
                    {
                        TypeId = (stateEntryEntity.EntitySet).ElementType.KeyMembers[0].ToString();
                        if (stateEntryEntity.EntityKey.EntityKeyValues == null)
                        {
                            modified = "1";
                        }
                        else
                        {
                            modified = "2";
                            uid = Convert.ToString(stateEntryEntity.EntityKey.EntityKeyValues[0].Value);
                        }
                    }
                    catch (Exception) { }
                    MNT_TransactionAuditLog audit = this.AuditTrailFactory(stateEntryEntity, UserName, uid, TypeId, modified);
                    var str1 = audit.NewData == null ? "" : audit.NewData.IndexOf("</ModifiedDate>") == -1 ? audit.NewData.Replace("<ModifiedDate xsi:nil=\"true\" />", "") : audit.NewData.Replace("<ModifiedDate>" + Between(audit.NewData, "<ModifiedDate>", "</ModifiedDate>") + "</ModifiedDate>", "");
                    var str2 = audit.OldData == null ? "" : audit.OldData.IndexOf("</ModifiedDate>") == -1 ? audit.OldData.Replace("<ModifiedDate xsi:nil=\"true\" />", "") : audit.OldData.Replace("<ModifiedDate>" + Between(audit.OldData, "<ModifiedDate>", "</ModifiedDate>") + "</ModifiedDate>", "");
                    if (!tranAuditTrailList.Contains(audit) && (str1 != str2))
                        tranAuditTrailList.Add(audit);
                }
            }
            if (tranAuditTrailList.Count > 0)
            {
                foreach (var audit in tranAuditTrailList)
                {//add all audits 
                    try
                    {
                        this.AddToMNT_TransactionAuditLog(audit);

                    }
                    catch { }
                }
            }
        }
        public static string Between(string value, string FirstString, string LastString)
        {
            string STR = value;
            string STRFirst = FirstString;
            string STRLast = LastString;
            string FinalString;
            if (STR == null)
            {
                FinalString = "";
            }
            else if (STR.IndexOf(LastString) == -1)
            {
                FinalString = "<ModifiedDate xsi:nil='true' />";
            }
            else
            {
                int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
                int Pos2 = STR.IndexOf(LastString);
                FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            }
            return FinalString;
        }
        private MNT_TransactionAuditLog AuditTrailFactory(ObjectStateEntry entry, string UserName, string uid, string TypeId, string modified)
        {
            int ClaimID = 0;
            int AccidentClaimId;
            int AccidentId = 0;
            string cb = string.Empty;
            string mb = string.Empty;
            string TableDescription = string.Empty;
            string TableType = string.Empty;
            MCASEntities obj = new MCASEntities();
            MNT_TransactionAuditLog tranAudit = new MNT_TransactionAuditLog();
            tranAudit.TimeStamp = DateTime.Now;
            tranAudit.TableName = entry.EntitySet.Name;
            CurrentValueRecord rec1 = entry.CurrentValues;
            try
            {
                cb = GetPropertyValue(entry, "CreatedBy");
                mb = GetPropertyValue(entry, "ModifiedBy");
                UserName = modified == "1" ? cb : mb;
                tranAudit.UserName = UserName;
                tranAudit.EntityCode = uid;
                tranAudit.EntityTypeId = TypeId;
            }
            catch { }
            try
            {
                string strClaimID = GetPropertyValue(entry, "ClaimId");
                if (strClaimID == "")
                    strClaimID = GetPropertyValue(entry, "CLAIM_ID");
                if (!String.IsNullOrEmpty(strClaimID))
                    ClaimID = Convert.ToInt32(strClaimID);
                tranAudit.ClaimID = ClaimID;
            }
            catch { }

            try
            {
                string strAccidentClaimID = GetPropertyValue(entry, "AccidentClaimId");
                if (strAccidentClaimID == "")
                    strAccidentClaimID = GetPropertyValue(entry, "AccidentId");
                if (!String.IsNullOrEmpty(strAccidentClaimID))
                    AccidentId = Convert.ToInt32(strAccidentClaimID);
                tranAudit.AccidentId = AccidentId;
            }
            catch { }
            try
            {
                TableDescription = (from l in obj.MNT_TableDesc where l.TableName == tranAudit.TableName select l.TableDesc).FirstOrDefault();
            }
            catch (Exception)
            {
                TableDescription = tranAudit.TableName;
            }
            try
            {
                TableType = (from l in obj.MNT_TableDesc where l.TableName == tranAudit.TableName select l.Type).FirstOrDefault();
            }
            catch (Exception)
            {
                TableType = "";
            }
            if (entry.State == EntityState.Added)
            {//entry is Added 
                tranAudit.NewData = GetEntryValueInString(entry, false);
                tranAudit.Actions = AuditActions.I.ToString();
                tranAudit.TansDescription = "Recored Inserted In " + TableDescription;
                tranAudit.EntityType = TableType;
                tranAudit.ChangedColumns = GetChangedColumns(entry);
            }
            else if (entry.State == EntityState.Deleted)
            {//entry in deleted
                tranAudit.OldData = GetEntryValueInString(entry, true);
                tranAudit.Actions = AuditActions.D.ToString();
                tranAudit.TansDescription = "Recored Deleted In " + TableDescription;
                tranAudit.EntityType = TableType;
            }
            else
            {//entry is modified
                tranAudit.OldData = GetEntryValueInString(entry, true);
                tranAudit.NewData = GetEntryValueInString(entry, false);
                tranAudit.Actions = AuditActions.U.ToString();
                tranAudit.TansDescription = "Recored Updated In " + TableDescription;
                tranAudit.EntityType = TableType;

                //IEnumerable<string> modifiedProperties = entry.GetModifiedProperties();
                //assing collection of mismatched Columns name as serialized string 
                tranAudit.ChangedColumns = GetChangedColumns(entry);//XMLSerializationHelper.XmlSerialize(modifiedProperties.ToArray());

                if(tranAudit.TableName == "MNT_Deductible")
                {
                tranAudit.CustomInfo = "Change in " + GetChangedColumn(entry);
                }   
            }

            return tranAudit;
        }
        private string GetPropertyValue(ObjectStateEntry entry, string PrpName)
        {
            string retValue = "";
            try
            {
                foreach (var propName in entry.CurrentValues.DataRecordInfo.FieldMetadata)
                {
                    string entPName = propName.FieldType.Name.ToString();//.ToLower();
                    if (entPName.ToLower() == PrpName.ToLower())
                    {
                        retValue = entry.CurrentValues.GetValue(entry.CurrentValues.GetOrdinal(entPName)).ToString();
                        break;
                    }
                }
            }
            catch { }
            return retValue;
        }

        public string GetChangedColumn(ObjectStateEntry entry)
        {
            string strChange = "";
            string strChangeval = "";
            try
            {
                if (entry.Entity is EntityObject)
                {
                    foreach (string propName in entry.GetModifiedProperties())
                    {
                        object ChangeValue = null;
                        object OrgValue = null;
                        //Get orginal value 
                        OrgValue = entry.OriginalValues[propName];
                        //Get Current value 
                        ChangeValue = entry.CurrentValues[propName];
                        //Compare property value with orgibal value 
                        if (ChangeValue != DBNull.Value)
                        {
                            if (propName != "ModifiedDate" && propName != "ModifiedBy")
                            {
                                if (ChangeValue.ToString() != OrgValue.ToString())
                                {
                                    if (strChange == "")
                                    {
                                        strChange += propName;
                                    }
                                    else
                                    {
                                        strChange +=  ", " + propName;
                                    }
                                }
                            }
                        }
                    }//end foreach

                    if (strChange == "DeductibleAmt")
                    {
                        strChangeval = "Deductible Amount";
                    }
                    if (strChange == "EffectiveTo")
                    {
                        strChangeval = "Effective To Date";
                    }
                    if (strChange == "DeductibleAmt, EffectiveTo" || strChange == "EffectiveTo, DeductibleAmt")
                    {
                        strChangeval = "Effective To Date and Deductible Amount";
                    }

                    return strChangeval;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return null;
        }

        //by Pravesh K Chandel on 11 March 15- capture only change properties and their values
        private string GetChangedColumns(ObjectStateEntry entry)
        {
            string strChangeXml = "<ChangeXml>";
            try
            {
                if (entry.Entity is EntityObject)
                {
                    if (entry.State == EntityState.Added)
                    {
                        foreach (var propName in entry.CurrentValues.DataRecordInfo.FieldMetadata)
                        {
                            string entPName = propName.FieldType.Name.ToString();//.ToLower();
                            string ChangeValue = entry.CurrentValues.GetValue(entry.CurrentValues.GetOrdinal(entPName)).ToString();
                            strChangeXml += "<Column label=\"\" field=\"" + entPName + "\" OldValue=\"" + "" + "\" NewValue=\"" + ChangeValue + "\" />";
                        }
                    }
                    else
                    {
                        foreach (string propName in entry.GetModifiedProperties())
                        {
                            object ChangeValue = null;
                            object OrgValue = null;
                            //Get orginal value 
                            OrgValue = entry.OriginalValues[propName];
                            //Get Current value 
                            ChangeValue = entry.CurrentValues[propName];
                            //Compare property value with orgibal value 
                            if (ChangeValue != DBNull.Value)
                            {
                                int Num;
                                bool isNum = int.TryParse(OrgValue.ToString(), out Num);
                                if (isNum)
                                {
                                    isNum = int.TryParse(ChangeValue.ToString(), out Num);
                                    if (isNum)
                                    {
                                        if (Convert.ToDecimal(ChangeValue.ToString()) != Convert.ToDecimal(OrgValue.ToString()))
                                        {
                                            strChangeXml += "<Column label=\"\" field=\"" + propName + "\" OldValue=\"" + OrgValue + "\" NewValue=\"" + ChangeValue + "\" />";
                                        }
                                    }
                                    else
                                    {
                                        if (ChangeValue.ToString() != OrgValue.ToString())
                                        {
                                            strChangeXml += "<Column label=\"\" field=\"" + propName + "\" OldValue=\"" + OrgValue + "\" NewValue=\"" + ChangeValue + "\" />";
                                        }
                                    }
                                }
                                else
                                {
                                    if (ChangeValue.ToString() != OrgValue.ToString())
                                    {
                                        strChangeXml += "<Column label=\"\" field=\"" + propName + "\" OldValue=\"" + OrgValue + "\" NewValue=\"" + ChangeValue + "\" />";
                                    }
                                }
                            }
                        }//end foreach
                    }
                    strChangeXml += "</ChangeXml>";
                    return strChangeXml;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return null;
        }

        public void InsertTransactionAuditLog(string TableName, string TablePrimaryKey, string UserName, int? ClaimId, int? AccidentId, string Actions, string ChangedColumns)
        {            
            try
            {
                string TableDescription = string.Empty;
                string TableType = string.Empty;
                MNT_TransactionAuditLog transAudit = new MNT_TransactionAuditLog();
                transAudit.TimeStamp = DateTime.Now;
                transAudit.TableName = TableName;
                transAudit.UserName = UserName;
                //transAudit.EntityCode = uid;
                transAudit.EntityTypeId = TablePrimaryKey;
                transAudit.ClaimID = ClaimId;
                transAudit.AccidentId = AccidentId;


                var objTableDesc = MNT_TableDesc.Where(t => t.TableName == transAudit.TableName).FirstOrDefault();
                if (objTableDesc != null)
                {
                    TableDescription = objTableDesc.TableDesc;
                    TableType = objTableDesc.Type;
                }
                else
                {
                    TableDescription = transAudit.TableName;
                    TableType = "";
                }

                transAudit.Actions = Actions;
                transAudit.TansDescription = "Recored Inserted In " + TableDescription;
                transAudit.EntityType = TableType;
                transAudit.ChangedColumns = ChangedColumns;
                transAudit.IsValidXml = false;

                MNT_TransactionAuditLog.AddObject(transAudit);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }         

        }
        private string GetEntryValueInString(ObjectStateEntry entry, bool isOrginal)
        {
            if (entry.Entity is EntityObject)
            {
                object target = CloneEntity((EntityObject)entry.Entity);
                foreach (string propName in entry.GetModifiedProperties())
                {
                    object ChangeValue = null;
                    if (isOrginal)
                    {
                        //Get orginal value 
                        ChangeValue = entry.OriginalValues[propName];
                    }
                    else
                    {
                        //Get Current value 
                        ChangeValue = entry.CurrentValues[propName];
                    }
                    //Find property to update 
                    PropertyInfo propInfo = target.GetType().GetProperty(propName);
                    //update property with orgibal value 
                    if (ChangeValue == DBNull.Value)
                    {//
                        ChangeValue = null;
                    }
                    propInfo.SetValue(target, ChangeValue, null);
                }//end foreach

                XmlSerializer formatter = new XmlSerializer(target.GetType());
                XDocument document = new XDocument();

                using (XmlWriter xmlWriter = document.CreateWriter())
                {
                    formatter.Serialize(xmlWriter, target);
                }
                return document.Root.ToString();
            }
            return null;
        }

        public EntityObject CloneEntity(EntityObject obj)
        {
            DataContractSerializer dcSer = new DataContractSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();

            dcSer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;

            EntityObject newObject = (EntityObject)dcSer.ReadObject(memoryStream);
            return newObject;
        }

        #region execute Datatable/Dataset by Pk Chandel on 8 Jul 2014
        private Hashtable _htParameters = new Hashtable();
        public void AddParameter(string ParamName, string paramValue)
        {
            if (!_htParameters.Contains(ParamName))
                _htParameters.Add(ParamName, paramValue);
        }
        public void AddParameter(EntityParameter param)
        {
            if (!_htParameters.Contains(param.ParameterName))
                _htParameters.Add(param.ParameterName, param);
        }
        public void AddParameter(SqlParameter param)
        {
            if (!_htParameters.Contains(param.ParameterName))
                _htParameters.Add(param.ParameterName, param);
        }
        public void ClearParameteres()
        {
            _htParameters.Clear();
        }
        public DataTable ExecuteQuery(string commandText, params Object[] parameters)
        {
            DataTable retDt = new DataTable();
            retDt = this.ExecuteStoreQuery<DataTable>(commandText, parameters).FirstOrDefault();
            return retDt;

        }
        public int ExecuteEntityNonQuery(string SqlText, CommandType cmdType)
        {
            int ret = 0;
            EntityConnection entityConn = (EntityConnection)this.Connection;
            EntityCommand ecommd = entityConn.CreateCommand();
            using (ecommd)
            {
                ecommd.CommandText = SqlText;
                ecommd.CommandType = cmdType;
                foreach (var parm in _htParameters.Keys)
                {
                    EntityParameter entPrm = null;
                    if (_htParameters[parm].GetType().ToString() == "System.Data.EntityClient.EntityParameter")
                        entPrm = (EntityParameter)_htParameters[parm];
                    else
                        entPrm = new EntityParameter() { ParameterName = parm.ToString(), Value = _htParameters[parm].ToString() };
                    ecommd.Parameters.Add(entPrm);
                }

                ret = ecommd.ExecuteNonQuery();
            }
            return ret;

        }

        public List<T> ExecuteStoredProcedure<T>(string storedProcedureName)
        {
            var spSignature = new StringBuilder();
            var parameters = new List<SqlParameter>();
            foreach (var parm in _htParameters.Keys)
            {
                SqlParameter sqlPrm = null;
                if (_htParameters[parm].GetType().ToString() == "System.Data.SqlClient.SqlParameter")
                    sqlPrm = (SqlParameter)_htParameters[parm];
                else
                    sqlPrm = new SqlParameter() { ParameterName = parm.ToString(), Value = _htParameters[parm].ToString() };
                parameters.Add(sqlPrm);
            }

            object[] spParameters;
            bool hasTableVariables = parameters.Any(p => p.SqlDbType == SqlDbType.Structured);

            spSignature.AppendFormat("EXECUTE {0}", storedProcedureName);
            var length = parameters.Count() - 1;
            if (hasTableVariables)
            {
                var tableValueParameters = new List<SqlParameter>();

                for (int i = 0; i < parameters.Count(); i++)
                {
                    switch (parameters[i].SqlDbType)
                    {
                        case SqlDbType.Structured:
                            spSignature.AppendFormat(" @{0}", parameters[i].ParameterName);
                            tableValueParameters.Add(parameters[i]);
                            break;
                        case SqlDbType.VarChar:
                        case SqlDbType.Char:
                        case SqlDbType.Text:
                        case SqlDbType.NVarChar:
                        case SqlDbType.NChar:
                        case SqlDbType.NText:
                        case SqlDbType.Xml:
                        case SqlDbType.UniqueIdentifier:
                        case SqlDbType.Time:
                        case SqlDbType.Date:
                        case SqlDbType.DateTime:
                        case SqlDbType.DateTime2:
                        case SqlDbType.DateTimeOffset:
                        case SqlDbType.SmallDateTime:
                            spSignature.AppendFormat(" '{0}'", parameters[i].Value.ToString());
                            break;
                        default:
                            spSignature.AppendFormat(" {0}", parameters[i].Value.ToString());
                            break;
                    }

                    if (i != length) spSignature.Append(",");
                }
                spParameters = tableValueParameters.Cast<object>().ToArray();
            }
            else
            {
                for (int i = 0; i < parameters.Count(); i++)
                {
                    spSignature.AppendFormat(" @{0}", parameters[i].ParameterName);
                    if (i != length) spSignature.Append(",");
                }
                spParameters = parameters.Cast<object>().ToArray();
            }

            var query = this.ExecuteStoreQuery<T>(spSignature.ToString(), spParameters);
            //var query = this.ExecuteStoreCommand(spSignature.ToString(), spParameters);
            var list = query.ToList();
            return list;
        }
        public int ExecuteNonQuery(string SqlText, CommandType cmdType)
        {
            object ret;
            return ExecuteNonQuery(SqlText, cmdType, out ret);
            //int ret = 0;
            //EntityConnection entityConn = (EntityConnection)this.Connection;
            //SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
            //SqlCommand cmd = new SqlCommand(SqlText, sqlConn);
            //using (cmd)
            //{
            //    cmd.CommandType = cmdType;
            //    foreach (var parm in _htParameters.Keys)
            //    {
            //        SqlParameter sqlPrm=null;
            //        if (_htParameters[parm].GetType().ToString() == "System.Data.SqlClient.SqlParameter")
            //            sqlPrm = (SqlParameter)_htParameters[parm];
            //        else
            //            sqlPrm = new SqlParameter(parm.ToString(), _htParameters[parm].ToString());
            //        cmd.Parameters.Add(sqlPrm);
            //    }

            //    ret = cmd.ExecuteNonQuery();
            //}
            //return ret;

        }

        public int ExecuteNonQuery(string SqlText, CommandType cmdType, out object ReturnValue)
        {
            int ret = 0;
            EntityConnection entityConn = (EntityConnection)this.Connection;
            SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
            SqlCommand cmd = new SqlCommand(SqlText, sqlConn);
            using (cmd)
            {
                cmd.CommandType = cmdType;
                foreach (var parm in _htParameters.Keys)
                {
                    SqlParameter sqlPrm = null;
                    if (_htParameters[parm].GetType().ToString() == "System.Data.SqlClient.SqlParameter")
                        sqlPrm = (SqlParameter)_htParameters[parm];
                    else
                        sqlPrm = new SqlParameter(parm.ToString(), _htParameters[parm].ToString());
                    cmd.Parameters.Add(sqlPrm);
                }

                SqlParameter retValue = cmd.Parameters.Add("return", SqlDbType.NVarChar);
                retValue.Direction = ParameterDirection.ReturnValue;

                ret = cmd.ExecuteNonQuery();
                ReturnValue = retValue.Value;

                //cmd.Dispose();
                //sqlConn.Close();
                //sqlConn.Dispose();
            }
            return ret;

        }
        public DataSet ExecuteDataSet(string Sql, CommandType cmdType)
        {
            DataSet ds = new DataSet();
            EntityConnection entityConn = (EntityConnection)this.Connection;
            SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
            SqlCommand cmd = new SqlCommand(Sql, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            using (cmd)
            {
                cmd.CommandType = cmdType; //CommandType.StoredProcedure;
                foreach (var parm in _htParameters.Keys)
                {
                    SqlParameter sqlPrm = new SqlParameter(parm.ToString(), _htParameters[parm].ToString());
                    cmd.Parameters.Add(sqlPrm);
                }

                da.Fill(ds);
            }

            return ds;
        }
        #endregion

    }


    static class XMLSerializationHelper
    {
        public static string XmlSerialize(object obj)
        {
            if (null != obj)
            {
                // Assuming obj is an instance of an object
                XmlSerializer ser = new XmlSerializer(obj.GetType());
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StringWriter writer = new System.IO.StringWriter(sb);
                ser.Serialize(writer, obj);
                return sb.ToString();
            }
            return string.Empty;
        }

        public static object XmlDeserialize(Type objType, string xmlDoc)
        {
            if (xmlDoc != null && objType != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlDoc);
                //Assuming doc is an XML document containing a serialized object and objType is a System.Type set to the type of the object.
                XmlNodeReader reader = new XmlNodeReader(doc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(objType);
                return ser.Deserialize(reader);
            }
            return null;
        }
    }


}
