using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cms.DataLayer;
using WebSupergoo.ABCpdf7;
using System.Reflection;
using System.Xml.Linq;
using System.IO;

namespace BlGeneratePdf
{
    public class ControlParse
    {

        public string PdfTemplatePath { get; set; }
        public string MapXmlFilePath { get; set; }
        public string InputXml { get; set; }
        public string PdfOutPutPath { get; set; }
        public int CUSTOMER_Id { get; set; }
        public int Policy_Id { get; set; }
        public int Policy_Version { get; set; }
        public string NOTICE_DUE_DATE { get; set; }
        public int IS_EOD { get; set; }      
        public string IDomain { get; set; }
        public string IUserName { get; set; }
        public string IPassWd { get; set; }

        XDocument xdoc;
        XDocument xMapDoc;
        
        public string GeneratePdf()
        {
           LoadXml();
           LoadMappingXml(MapXmlFilePath);
           string Filename = LoadandMapPdfTemplate(PdfTemplatePath);
           return Filename;
        }
        private void LoadXml()
        {
            //System.IO.StringReader sr = new System.IO.StringReader(InputXml);
            System.IO.Stream s = new System.IO.MemoryStream(ASCIIEncoding.Default.GetBytes(InputXml));
            xdoc = XDocument.Load(s);            
        }        
        private void LoadMappingXml(string XmlFilePath)
        {
            xMapDoc = XDocument.Load(XmlFilePath);
        }
        //private DataSet ExecuteProc()
        //{
        //    DataSet dsPNotice = null;
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
        //    objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_Id, SqlDbType.Int);
        //    objDataWrapper.AddParameter("@POLICY_ID", Policy_Id, SqlDbType.Int);
        //    objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_Version, SqlDbType.Int);
        //    objDataWrapper.AddParameter("@NOTICE_DUE_DATE", NOTICE_DUE_DATE, SqlDbType.Date);
        //    objDataWrapper.AddParameter("@IS_EOD", IS_EOD, SqlDbType.SmallInt);
        //    objDataWrapper.AddParameter("@CARRIER_CODE", CARRIER_CODE, SqlDbType.NVarChar);
        //    objDataWrapper.AddParameter("@LANG_ID", LANG_ID, SqlDbType.Int);

        //    dsPNotice = objDataWrapper.ExecuteDataSet("Proc_GetPremiumNoticeDetails");
        //    return dsPNotice;
        //}
        private string LoadandMapPdfTemplate(string File_Path)
        {
            //impersonate User
            ClsGeneratePdf objGenerateOdf = new ClsGeneratePdf();
            objGenerateOdf.ImpersonateUser(IUserName, IPassWd, IDomain);
          

            WebSupergoo.ABCpdf7.Doc PdfDoc = new WebSupergoo.ABCpdf7.Doc();
            PdfDoc.Read(File_Path);
            var Names = from name in xdoc.Descendants("Policy")
                        select name;

            var MapPDFCONTROLS = from MapPDFCONTROL in xMapDoc.Descendants("PDFCONTROLS")
                                 select MapPDFCONTROL;
            foreach (XElement Nod in Names.Elements())
            {
                bool Mapped = false;
                // Check in mapping xml
                foreach (XElement MapNod in MapPDFCONTROLS.Elements())
                {
                    if (Nod.Name.ToString().ToUpper().Trim() == MapNod.Attribute("DBNAME").Value)
                    {
                        //foreach (WebSupergoo.ABCpdf7.Objects.Field Fild in PdfDoc.Form.Fields)
                        //{
                        //    if (Fild.Name == MapNod.Attribute("ELEMENTNAME").Value.ToString().ToUpper().Trim())
                        //    {
                        WebSupergoo.ABCpdf7.Objects.Field Fild = PdfDoc.Form[MapNod.Attribute("ELEMENTNAME").Value.ToString().Trim()];
                        if (Fild != null)
                        {
                            Fild.Value = Nod.Value;
                            Mapped = true;
                        }
                        //break;
                        //    }
                        //}
                    }
                    if (Mapped == true)
                        break;
                }
                // If not mapped in mapping Xml
                if (Mapped == false)
                {
                    //foreach (WebSupergoo.ABCpdf7.Objects.Field Fild in PdfDoc.Form.Fields)
                    //{
                    //    if (Fild.Name == Nod.Name.ToString().ToUpper().Trim())
                    //    {
                    WebSupergoo.ABCpdf7.Objects.Field Fild = PdfDoc.Form[Nod.Name.ToString().Trim()];
                    if (Fild != null)
                    {
                        Fild.Value = Nod.Value;
                    }
                          //  break;
                    //    }
                    //}
                }
            }

            MapPDFCONTROLS = from MapPDFCONTROL in xMapDoc.Descendants("PDFCONTROLSREPETABLE")
                             select MapPDFCONTROL;

            var Name_ACCROW_Child = from name in xdoc.Descendants("ACCROW")
                                    select name;
            int Num_child = 0;
            foreach (var name in Name_ACCROW_Child)
            {
                Num_child = name.Descendants().Count();
            }

            var Names_ACCROW = from name in xdoc.Descendants("ACCROW")
                               select name;

            int MatchCtr = 0, ValueMappCtr = 0;
            foreach (XElement Nod in Names_ACCROW.Elements())
            {
                bool Mapped = false;
                // Check in mapping xml
                foreach (XElement MapNod in MapPDFCONTROLS.Elements())
                {
                    if (Nod.Name.ToString().ToUpper().Trim() == MapNod.Attribute("DBNAME").Value)
                    {
                        //foreach (WebSupergoo.ABCpdf7.Objects.Field Fild in PdfDoc.Form.Fields)
                        //{
                        //    if (Fild.Name == MapNod.Attribute("ELEMENTNAME").Value.ToString().ToUpper().Trim().Replace("$", MatchCtr.ToString()))
                        //    {
                        WebSupergoo.ABCpdf7.Objects.Field Fild = PdfDoc.Form[MapNod.Attribute("ELEMENTNAME").Value.ToString().Trim().Replace("$", MatchCtr.ToString())];
                        if (Fild != null)
                        {
                            Fild.Value = Nod.Value;
                            Mapped = true;
                        }
                        //        break;
                        //    }
                        //}
                    }
                    if (Mapped == true)
                        break;
                }

                // If not mapped in mapping Xml
                if (Mapped == false)
                {
                    //foreach (WebSupergoo.ABCpdf7.Objects.Field Fild in PdfDoc.Form.Fields)
                    //{
                    WebSupergoo.ABCpdf7.Objects.Field Fild = PdfDoc.Form[Nod.Name.ToString().Trim() + "_" + MatchCtr.ToString()];
                    //if (Fild.Name == Nod.Name.ToString().ToUpper().Trim() + "_" + MatchCtr.ToString())
                    //{
                    if (Fild != null)
                    {
                        Fild.Value = Nod.Value;
                    }
                    //    break;
                    //}
                    //}
                }
                ValueMappCtr++;
                if (ValueMappCtr % Num_child == 0 && ValueMappCtr != 1)
                    MatchCtr++;
            }         


            var Name_INSROW_Child = from name in xdoc.Descendants("INSROW")
                                    select name;
            Num_child = 0;
            foreach (var name in Name_INSROW_Child)
            {
                Num_child = name.Descendants().Count();
            }

            var Name_INSROW = from name in xdoc.Descendants("INSROW").Nodes()
                              select name;

            MatchCtr = 0; ValueMappCtr = 0;
            foreach (XElement Nod in Name_INSROW)
            {
                bool Mapped = false;
                // Check in mapping xml
                foreach (XElement MapNod in MapPDFCONTROLS.Elements())
                {
                    if (Nod.Name.ToString().ToUpper().Trim() == MapNod.Attribute("DBNAME").Value)
                    {
                        //foreach (WebSupergoo.ABCpdf7.Objects.Field Fild in PdfDoc.Form.Fields)
                        //{
                        //    if (Fild.Name == MapNod.Attribute("ELEMENTNAME").Value.ToString().ToUpper().Trim().Replace("$", MatchCtr.ToString()))
                        //    {
                        WebSupergoo.ABCpdf7.Objects.Field Fild = PdfDoc.Form[MapNod.Attribute("ELEMENTNAME").Value.ToString().Trim().Replace("$", MatchCtr.ToString())];
                        if (Fild != null)
                        {
                            Fild.Value = Nod.Value;
                            Mapped = true;
                        }
                        //        break;
                        //    }
                        //}
                    }
                    if (Mapped == true)
                        break;
                }

                // If not mapped in mapping Xml
                if (Mapped == false)
                {
                    //foreach (WebSupergoo.ABCpdf7.Objects.Field Fild in PdfDoc.Form.Fields)
                    //{
                    WebSupergoo.ABCpdf7.Objects.Field Fild = PdfDoc.Form[Nod.Name.ToString().ToUpper().Trim() + "_" + MatchCtr.ToString()];
                    // if (Fild.Name == Nod.Name.ToString().ToUpper().Trim() + "_" + MatchCtr.ToString())
                    // {
                    if (Fild != null)
                    {
                        Fild.Value = Nod.Value;
                    }
                    break;
                    //}
                    //}
                }
                ValueMappCtr++;
                if (ValueMappCtr % Num_child == 0 && ValueMappCtr != 1)
                    MatchCtr++;
            }
            SetTmpPDFPath(PdfOutPutPath);
            PdfDoc.Save(PdfOutPutPath+"Prem_"+CUSTOMER_Id+"_"+Policy_Id+"_"+Policy_Version+".pdf");
            PdfDoc.Dispose();
            objGenerateOdf.endImpersonation();
            return "Prem_" + CUSTOMER_Id + "_" + Policy_Id + "_" + Policy_Version + ".pdf";
           
        }       
        private void SetTmpPDFPath(string strPDfDirectory)
        {
            try
            {
                DirectoryInfo DirInfo = new DirectoryInfo(strPDfDirectory);

                if (DirInfo.Exists == false)
                {
                    DirInfo.Create();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
    }  
}
