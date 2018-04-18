/********************************************************
<Author					: -   Sneha
<Start Date				: -	1/09/2011 
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: -
**********************************************************/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlApplication;
using AdvCommon;
using AdvRuleParser; //sneha
using System.IO;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;
using System.Collections.Generic;
using System.Configuration;
 
namespace Cms.Account.Aspx
{
    public partial class UWRuleTestBed : Cms.Account.AccountBase
    {
        public string InputXML;
        public string InputXMLPol;
        public string OutputXML;
        public string systemId;
        public string customerId;
        public string PolicyId;
        public string PolicyverId;
        System.Resources.ResourceManager objResourceMgr;           
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "554_0";
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.UWRuleTestBed", System.Reflection.Assembly.GetExecutingAssembly());
            SetErrorMessages();
            SetCaption();
            getinputdata(); 
            Getvalues();
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
        private void SetErrorMessages()
        {
            revCustomerid.ValidationExpression = aRegExpInteger;
            revCustomerid.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("467");
            revPolicyId.ValidationExpression = aRegExpInteger;
            revPolicyId.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("467");
            revPolicyversionId.ValidationExpression = aRegExpInteger;
            revPolicyversionId.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("467");
        }
        private void SetCaption()
        {
            capHeader.Text          =   objResourceMgr.GetString("capHeader");
            capMandatoryNotes.Text  =   objResourceMgr.GetString("capMandatoryNotes");
            capRuleMethod.Text      =   objResourceMgr.GetString("capRuleMethod");
            capCustomerid.Text      =   objResourceMgr.GetString("capCustomerid");
            capPolicyId.Text        =   objResourceMgr.GetString("capPolicyId");
            capPolicyversionId.Text =   objResourceMgr.GetString("capPolicyversionId");
            capInputXML.Text        =   objResourceMgr.GetString("capInputXML");
            capInputXMLPol.Text     =   objResourceMgr.GetString("capInputXMLPol");
            btnPerformRule.Text     =   objResourceMgr.GetString("btnPerformRule");
            capOutputXML.Text       =   objResourceMgr.GetString("capOutputXML");
            btnShowOutPut.Text      =   objResourceMgr.GetString("btnShowOutPut");
             btnBulkTest.Text = objResourceMgr.GetString("btnBulkTest");
          
        }
        private void BindData()
        {
            cmbRuleMethod.Items.Add("");
            cmbRuleMethod.Items.Add("Policy Input");
            cmbRuleMethod.Items.Add("Manual Input XML");
        }
        private void getinputdata()
        {
            hidCustomerid.Value = txtCustomerid.Text.Trim();
            hidPolicyId.Value = txtPolicyId.Text.Trim();
            hidPolicyVerId.Value = txtPolicyversionId.Text.Trim();
            PolicyId=hidPolicyId.Value;
            customerId=hidCustomerid.Value;
            PolicyverId=hidPolicyVerId.Value;
            systemId = getCarrierSystemID();
        }
 
        protected void btnPerformRule_Click(object sender, EventArgs e)
        {
            InputXML = txtInputXML.Text.Trim();
            InputXMLPol = txtInputXMLPol.Text.Trim();
            if (cmbRuleMethod.SelectedIndex == 1)
            {
                string str = PerformRule(customerId, PolicyId, PolicyverId);
                txtOutputXML.Text = str;
            }
            else if (cmbRuleMethod.SelectedIndex == 2)
            {
                string str = PerformRule(InputXML, InputXMLPol);
                txtOutputXML.Text = str;
            }
        }


        // Below line of code is added to generate xml files against the records the fetched from table 'ruletest', itrack 813, mmodified by naveen 
        protected void btnBulkTest_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch stopWatch = new  System.Diagnostics.Stopwatch();   
                 //some operation on input            


            string strPath = "";

            System.Text.StringBuilder strBuild = new System.Text.StringBuilder();
            string strTemp = "";
            string strUserName = ConfigurationManager.AppSettings.Get("IUserName");
            string strPassWd = ConfigurationManager.AppSettings.Get("IPassWd");
            string strDomain = ConfigurationManager.AppSettings.Get("IDomain");
            Cms.BusinessLayer.BlCommon.ClsAttachment objAttachment = new ClsAttachment();
            string dd = DateTime.Now.Day.ToString();
            string mon = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();

            dd = dd.Length < 2 ? dd.PadLeft(2, '0') : dd.ToString();
            mon = mon.Length < 2 ? mon.PadLeft(2, '0') : mon.ToString();

            string date_cur = dd + mon + yy;
            DataSet DsruleXXMlData = new DataSet();
            ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
            DsruleXXMlData = objGenInfo.Get_BulkData_RuleTest();

            if (DsruleXXMlData != null)
            {
                foreach (DataRow dr in DsruleXXMlData.Tables[0].Rows)
                {
                    try
                    {

                        stopWatch.Start();
                        // long StartTtics = timeTaken;
                        string StartRoot = "<xml>";
                        string EndRoot = "</xml>";
                        string XML = "";
                        XML += StartRoot;
                        string Customer_id = dr["Customer_ID"].ToString();
                        string Policy_id = dr["Policy_ID"].ToString();
                        string Policy_Version_id = dr["Policy_VersionID"].ToString();
                        string Policy_Number = dr["Policy_Number"].ToString();
                        if (objAttachment.ImpersonateUser(strUserName, strPassWd, strDomain))
                        {
                            strPath = ConfigurationManager.AppSettings["PdfPath"] + "/" + "RuleXml";
                            if (!Directory.Exists(strPath))
                            {

                                System.IO.Directory.CreateDirectory(strPath);
                            }
                            strPath = strPath + "/" + Policy_Number + "_" + Customer_id + "_" + Policy_id + "_" + Policy_Version_id + "_" + date_cur + ".xml";
                            string XMLFile = PerformRule(Customer_id, Policy_id, Policy_Version_id);

                            //XMLFile = "<XML>" + XMLFile + "</xml>";
                            XML += XMLFile;
                            XML += EndRoot;
                            strTemp = "";
                            // long Endticks = timeTaken;
                            // long Seconds = Endticks-StartTtics ;
                            stopWatch.Stop();
                            long timeTaken = stopWatch.ElapsedMilliseconds;
                            TimeSpan elapsedSpan = new TimeSpan(timeTaken);
                            XML = XML.Replace("<xml>", @"<xml TimeTaken='" + elapsedSpan + "'>");
                            XMLFile = XML;
                            string strFullLengthString = "";
                            strFullLengthString = strTemp + XMLFile;


                            if (!File.Exists(strPath))
                            {

                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(strPath, true, System.Text.Encoding.ASCII))
                                {
                                    if (strFullLengthString.Length > 0)
                                    {
                                        file.Write(strFullLengthString);
                                    }
                                }
                            }

                            else
                            {
                                File.Delete(strPath);
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(strPath, true, System.Text.Encoding.ASCII))
                                {
                                    if (strFullLengthString.Length > 0)
                                    {
                                        file.Write(strFullLengthString);
                                    }
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                        throw (new Exception(ex.Message.ToString(), ex.InnerException));
                       
                    }
                }
            }
        }

        protected void btnShowOutPut_Click(object sender, EventArgs e)
        {
            OutputXML = txtOutputXML.Text.Trim();
            //string output = ShowHtml(OutputXML);
            //hidCalledFrom.Value = "UWRuleTestBed";
            //hidHtml.Value = output;
            Session.Add("stroutput", OutputXML);
            ClientScript.RegisterStartupScript(this.GetType(), "UWRuleTestBed", "<script>ShowDialogBox();</script>");
 
        }
        protected void Getvalues()
        {
            Session["customerID"] = 28070;
            Session["PolicyID"] = 419;
            Session["PolicyVersionID"] = 2;
        }
        // Added by sneha for page UWRuleTestBed
        public string PerformRule(string customerId, string PolicyId, string PolicyversionId)
        {
            string txtResult;
            try
            {
                int Customer_ID = Convert.ToInt32(customerId);
                int Policy_ID = Convert.ToInt32(PolicyId);
                int Policy_Version_ID = Convert.ToInt32(PolicyversionId);
                ClsRatingAndUnderwritingRules objRating = new ClsRatingAndUnderwritingRules(systemId);
                if (hidCustomerid.Value == "" && hidPolicyId.Value == "" && hidPolicyVerId.Value == "")
                {
                    txtResult = objRating.VerifyPolicy(Customer_ID, Policy_ID, Policy_Version_ID);
                }
                else
                {
                    txtResult = objRating.VerifyPolicy(Int32.Parse(hidCustomerid.Value), Int32.Parse(hidPolicyId.Value), Int32.Parse(hidPolicyVerId.Value));
                }
            }

            catch (Exception ex)
            {
                //txtResult = ex.Message + ex.StackTrace;
                throw (new Exception(ex.Message.ToString(), ex.InnerException));
            }
            return txtResult;
        }
        public string PerformRule(string InputXML, string InputXMLPol)
        {
            string txtResult;
           
            try
            {
                string SectionMasterPath = Cms.BusinessLayer.BlCommon.ClsCommon.CmsWebUrl + "support/UnderwritingRules/" + CarrierSystemID + "/" + "SectionMaster.xml";
                XmlDocument objSection = new XmlDocument();
                objSection.Load(SectionMasterPath);
                IUWRuleParser objRuleParser = new UWRuleParser(objSection);
                RuleResultCollection ResultSet = new RuleResultCollection();

                objRuleParser.VerifyRules(InputXML, InputXMLPol, ResultSet);

                IRuleResultFormatter objResultFormatter = new RuleResultXMLFormatter();

                txtResult = objResultFormatter.ToXML(ResultSet);

             
             }
          
            catch (Exception ex)
            {
                //txtResult = ex.Message + ex.StackTrace;
                throw (new Exception(ex.Message.ToString(), ex.InnerException));
            }
            return txtResult;
        }
        public string ShowHtml(string strRuleInputXML)
        {
            string xslFileName_Path = "", xslString = "", OutPutHtml = "";

            //strRuleInputXML = (strRuleInputXML);
            XmlDocument myXmlDocuments = new XmlDocument();
            myXmlDocuments.LoadXml(strRuleInputXML);

            #region CONVERT THE ACORD INPUT XML INTO QQ XML
            // load the xsl file
            XmlDocument docXSLFile = new XmlDocument();
            string strMessageMappingPath = "";

            xslFileName_Path = Cms.BusinessLayer.BlCommon.ClsCommon.GetKeyValueWithIP("UWRuleXSL_Path");
            if (Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID == 1)
            {
                strMessageMappingPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetKeyValueWithIP("UWRuleMessageXmlMapping_path");
            }
            else if (Cms.BusinessLayer.BlCommon.ClsCommon.BL_LANG_ID == 2)
            {
                strMessageMappingPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetKeyValueWithIP("UWRuleMessageXmlMapping_path_BR");
            }
            else
            {
                strMessageMappingPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetKeyValueWithIP("UWRuleMessageXmlMapping_path_SG");
            }
            docXSLFile.Load(xslFileName_Path);
            xslString = docXSLFile.InnerXml;

            XsltSettings xsltSetting = new XsltSettings();
            xsltSetting.EnableDocumentFunction = true;

            xsltSetting.EnableScript = true;

            xslString = xslString.Replace("MessagePath", strMessageMappingPath.Trim().ToString());
            System.Xml.Xsl.XslCompiledTransform xslt = new System.Xml.Xsl.XslCompiledTransform();
            xslt.Load(new XmlTextReader(new StringReader(xslString)), xsltSetting, null);
            //load the UWRule xml file
            StringWriter writer = new StringWriter();
            XmlDocument xmlDocTemp = new XmlDocument();
            xmlDocTemp.LoadXml(strRuleInputXML);
            //strRuleInputXML = xmlDocTemp.OuterXml;	
            // Transform the file and output an XML String 	
            XPathNavigator nav = ((IXPathNavigable)xmlDocTemp).CreateNavigator();
            xslt.Transform(nav, null, writer);
            OutPutHtml = writer.ToString();			 // QQ Input XML
            writer.Close();
            #endregion

            //load the qqInputXML 
            OutPutHtml = OutPutHtml.Replace("&gt;", ">");
            OutPutHtml = OutPutHtml.Replace("&lt;", "<");
            OutPutHtml = OutPutHtml.Replace("\"", "'");
            OutPutHtml = OutPutHtml.Replace("\r", "");
            OutPutHtml = OutPutHtml.Replace("\n", "");
            OutPutHtml = OutPutHtml.Replace("\t", "");

            OutPutHtml = OutPutHtml.Replace("<?xml version='1.0' encoding='utf-16'?>", "");
            return OutPutHtml;
        }
        // Sneha
    }

}