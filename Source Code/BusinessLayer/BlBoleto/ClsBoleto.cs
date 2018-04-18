using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.BusinessLayer.BlBoleto;
using System.Web.UI;

namespace Cms.BusinessLayer.BlBoleto
{
    public class ClsBoleto : Cms.BusinessLayer.BlCommon.ClsCommon
    {
        #region "Varriables"

        private DateTime maturity;
        private short bankCode;
        Double TotalAmt = 0.0;
        String StrAgencyCode = "";
        String StrAssigner = "";
        String StrOurNumber = "";
        String strBANK_NUMBER = "";
        String strAC_NUMBER = "";
        String strBRANCH_NUMBER = "";
        String strMAX_OUR_NUMBER = "";
        String strAGREEMENT_NUMBER = "";
        String strSUSEP_LOB_CODE="";
        String strBRANCH_CODE = "";
        String StrBankName = "";
        String StrPolicy_LOB_SUB_LOB = "";
        String strAGENCY_DISPLAY_NAME = "";
        String strEFFECTIVE_DATE = "";
        String strEXPIRATION_DATE = "";
        String strAPPICATION_NUMBER = "";
        String StrPOLICY_NUMBER = "";
        String strENDORSEMENT_NO = "";
        String _Total_InstallmentNO = "";
        String strCO_INSURANCE = "";
        String strPOLICY_VERIFY_DIGIT = "";
        String strENDORSEMENT_VERIFY_DIGIT = "";
        String strENDO_TYPE_DIGIT = "";
        String _CNPJ = "";
        String StrCPF_CNPJ, strFName, StrMName, StrLName;
        String Straddress1, StrDistrict, StrCity, StrCEP;
        String strADDRESS2 = String.Empty;
        String strNUMBER = String.Empty;
        String strSTATE_CODE = String.Empty;
        String strCancellationDate = String.Empty;
        String strSTATE_NAME = String.Empty;
        DateTime InstallmentDate;
        String DocumentNO;
        DataWrapper objWrapper;
        String _CURRENCY_SYMBOL = "";
        String _Our_Number = String.Empty;//Added by Pradeep Kushwaha on 24-Nov-2010
        String _Boleto_BarCode_Number = String.Empty;//Added by Pradeep Kushwaha on 20-July-2011
        String _Called_From = String.Empty;
        //ArrayList StrArr = new ArrayList();

        #endregion "Varriables"

        public ClsBoleto()
        {
            objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
        }
        public ClsBoleto(DataWrapper objDataWrapper)
        {
            objWrapper = objDataWrapper;
        }
        public DataSet ReturnBoletoValues(int Policyid, int PolicyVersionID, int CustomerID, int InstallmentNo, object Flag, String Bank_number,int CoApplicant_id)
        {
            string strStoredProc = "Proc_FetchPolicyBoletodetails";

            //DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

            DataSet resultDS = new DataSet();
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@POLICY_ID", Policyid);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@INSTALLMENT_NO", InstallmentNo);
                objWrapper.AddParameter("@FLAG", Flag);
                objWrapper.AddParameter("@BANK_NUMBER", Bank_number);
                objWrapper.AddParameter("@CO_APPLICANT_ID", CoApplicant_id);
                objWrapper.AddParameter("@CALLED_FROM", Called_From);//Added by Pradeep on 21-july -2011 for reprint
                

                resultDS = objWrapper.ExecuteDataSet(strStoredProc);
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //if (objWrapper != null) objWrapper.Dispose();
            }
            return resultDS;

        }
        
        public DataSet ReturnGeneratedBoletoDataset(int CUSTOMER_ID, int PolicyId, int POLICY_VERSION_ID, int INSTALLEMT_ID, int INSTALLMENT_NO, String Called_from, int _CO_APPLICANT_ID)
        {
            string strStoredProc = "Proc_FetchPolicyBoletos";

            //DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

            DataSet resultDS = new DataSet();
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                if (INSTALLMENT_NO == 0 && Called_from == "GENERATED_BOLETO" )
                    objWrapper.AddParameter("@POLICY_VERSION_ID",null);
                else
                    objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);

                objWrapper.AddParameter("@CALLED_FROM", Called_from);

                if (INSTALLEMT_ID == 0)
                {
                    objWrapper.AddParameter("@INSTALLEMT_ID", null);
                }
                else
                {
                    objWrapper.AddParameter("@INSTALLEMT_ID", INSTALLEMT_ID);
                }
                if (INSTALLMENT_NO == 0)
                {
                    objWrapper.AddParameter("@INSTALLMENT_NO", null);
                }
                else
                {
                    objWrapper.AddParameter("@INSTALLMENT_NO", INSTALLMENT_NO);
                }
                if(_CO_APPLICANT_ID==0)
                    objWrapper.AddParameter("@CO_APPLICANT_ID", null);
                else
                    objWrapper.AddParameter("@CO_APPLICANT_ID", _CO_APPLICANT_ID);


                resultDS = objWrapper.ExecuteDataSet(strStoredProc);
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //if (objWrapper != null) objWrapper.Dispose();
            }

            return resultDS;

        }

        public int SaveGeneratedBoletoForReprint(Int32 CustomerID, Int32 PolicyID, Int32 PolicyVersionID, Int32 INSTALLEMT_ID,
            Int32 _InstallmentNO, Int32 BANK_ID, String html, Int32 userid, String Our_number)
        {
            int intResult = 0;
         
            String strStoredProc = "Proc_SAVE_POL_INSTALLMENT_BOLETO";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@INSTALLEMT_ID", INSTALLEMT_ID);
                objWrapper.AddParameter("@INSTALLMENT_NO", _InstallmentNO);
                objWrapper.AddParameter("@BANK_ID", BANK_ID);
                objWrapper.AddParameter("@BOLETO_HTML", html);
                objWrapper.AddParameter("@CREATED_BY", userid);
                objWrapper.AddParameter("@CREATED_DATETIME", DateTime.Now);
                objWrapper.AddParameter("@MODIFIED_BY", null);
                objWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);
                objWrapper.AddParameter("@CALLED_FOR", "BOLETO_REPRINT");
                objWrapper.AddParameter("@OUR_NUMBER", Our_number);
                objWrapper.AddParameter("@BOLETO_BARCODE_NUMBER", Boleto_BarCode_Number);
                intResult = objWrapper.ExecuteNonQuery(strStoredProc);

                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
               
                throw (new Exception("Error while saving record.", ex.InnerException));
            }
            return intResult;
        }

        #region "Save Generated Boleto"

        // This method save generated boleto in Boleto table POL_INSTALLMENT_BOLETO.
        public int SaveGeneratedBoleto(ref Double TotalAmt, ref String StrAgencyCode, ref String StrAssigner,
            ref String StrOurNumber, ref String StrBankName, ref String html, int _PolicyID,
            int _PolicyVersionID, int _CustomerID, int _InstallmentNO, ref int userid, int i, int INSTALLEMT_ID, int BANK_ID,string Our_number)
        {


            int intResult = 0;
            String strStoredProc = "Proc_SAVE_POL_INSTALLMENT_BOLETO";
            //DataWrapper objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objWrapper.ClearParameteres(); 
                objWrapper.AddParameter("@CUSTOMER_ID", _CustomerID);
                objWrapper.AddParameter("@POLICY_ID", _PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", _PolicyVersionID);
                objWrapper.AddParameter("@INSTALLEMT_ID", INSTALLEMT_ID);
                objWrapper.AddParameter("@INSTALLMENT_NO", _InstallmentNO);
                objWrapper.AddParameter("@BANK_ID", BANK_ID);
                objWrapper.AddParameter("@BOLETO_HTML", html);
                objWrapper.AddParameter("@CREATED_BY", userid);
                objWrapper.AddParameter("@CREATED_DATETIME", DateTime.Now);
                objWrapper.AddParameter("@MODIFIED_BY", null);
                objWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);
                objWrapper.AddParameter("@OUR_NUMBER", Our_number);
                objWrapper.AddParameter("@BOLETO_BARCODE_NUMBER", Boleto_BarCode_Number);

                intResult = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //if (objWrapper != null) objWrapper.Dispose();
            }

            return intResult;

        }



        #endregion "Save Generated Boleto"
        public String CurrencySymbol
        {
            get { return _CURRENCY_SYMBOL; }
            set { _CURRENCY_SYMBOL = value; }
        }

        #region "HSBC Bank"
        // if all boleto is required to generate then getting values from dataset
        //  from InitializeElementsOfBoleto from ith row
        public BilletBanking Bank_HSBC(int bankCode, int _PolicyID, int _PolicyVersionID, int _CustomerID, int _InstallmentNO, int CoApplicant_id)
        {
            BilletBanking bb = new BilletBanking();
            //List<BilletBanking> boletos = new List<BilletBanking>();
            //  String ref StrCPF_CNPJ, ref strFName,ref StrMName,ref StrLName,ref Straddress1,ref StrDistrict, ref StrCity,ref StrCEP;
            InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref _CNPJ, ref StrCPF_CNPJ, ref strFName, ref StrMName, ref StrLName, ref Straddress1, ref StrDistrict, ref StrCity, ref StrCEP, ref InstallmentDate, ref DocumentNO,
                         _PolicyID, _PolicyVersionID, _CustomerID, _InstallmentNO, ref strAC_NUMBER, ref strBRANCH_NUMBER, ref strMAX_OUR_NUMBER, ref strAGREEMENT_NUMBER, ref strSUSEP_LOB_CODE, ref strBRANCH_CODE, ref StrPolicy_LOB_SUB_LOB, ref strAGENCY_DISPLAY_NAME, ref strEFFECTIVE_DATE, ref strEXPIRATION_DATE, ref _Total_InstallmentNO, ref strAPPICATION_NUMBER,
                         ref StrPOLICY_NUMBER, ref strENDORSEMENT_NO, ref strCO_INSURANCE, ref strPOLICY_VERIFY_DIGIT, ref strENDORSEMENT_VERIFY_DIGIT, ref strENDO_TYPE_DIGIT, ref strADDRESS2, ref strNUMBER, ref strSTATE_CODE, CoApplicant_id, ref strCancellationDate, ref strSTATE_NAME);

            //InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref  _CNPJ, ref i);

            //For HSBC
            bb.BankCode = 399;

            //DateTime maturity = new DateTime(2008, 7, 4);
            maturity = InstallmentDate;
            //This code for adding instructions 
            //Instructions shows based on parametere 

            Instructions_Itau item1 = new Instructions_Itau(9, 5);
            Instructions_Itau item2 = new Instructions_Itau(81, 10);

            //new, StrAgencyCode should be number in string type[can't be character]
            Assginor asg = new Assginor(_CNPJ, StrAssigner, StrAgencyCode, "13000");

            //old
            // Assginor asg = new Assginor("00.000.000/0000-00", "Shyam Lal & company", "0542", "13000");
            //cpfcnf,Assignor,ageny,Agency-account

            // Code provided by the agency, which is NOT the account number

            asg.Code = 945665878; // 7 positions
            //What evere value want to display pass all values from here with the help of object

            //old
            ///Boleto b = new Boleto(maturity, 550, "198", "92082835", asg);

            //New
            Boleto b = new Boleto(maturity, TotalAmt, "198", StrOurNumber, asg);

            //Boleto b = new Boleto(maturity, 550, "198", "92082835", asg, new Documentkind(341, 1));
            //ExpiryDate,Value document,wallet,

            //b.OurNumber = "9999999999999"; //We can over write Our Number 92082835 with  9999999999999
            b.Especie = CurrencySymbol;
            //b.Documentnumber = "9999";
            b.Documentnumber = DocumentNO;
            // b.Documentdate = maturity;


            //All Details Display from CLT_CUSTOMER_LIST table
            //New

            String StrName = strFName + StrMName + StrLName;

            b.Drawee = new Drawee(StrCPF_CNPJ, StrName);
            b.Drawee.Address.address = Straddress1;
            b.Drawee.Address.District = StrDistrict;
            b.Drawee.Address.City = StrCity;
            b.Drawee.Address.CEP = StrCEP;     //CUSTOMER_ZIP zip code
            b.Drawee.Address.UF = "UF";

            //OLD
            //  b.Drawee = new Drawee("43.764.124/0001-56", " Praveen Kumar ");
            //b.Drawee.Address.address = "A-147 Sector 20 Noida UP";
            //b.Drawee.Address.District = "Gautam Buddh Nagar";
            //b.Drawee.Address.City = "Noida";
            //b.Drawee.Address.CEP = "00000000";
            //b.Drawee.Address.UF = "UF"

            item2.Description += " " + item2.QuantityDays.ToString() + " calendar days of winning.";
            b.Instructions.Add(item1);
            b.Instructions.Add(item2);

            bb.CurrencySymbol = CurrencySymbol;
            bb.Boleto = b;
            bb.Boleto.Validates();
            this.Our_Number = b.OurNumber;
            //boletos.Add(bb);


            //Re Initialize varriable 
            TotalAmt = 0;
            StrAgencyCode = "";
            StrAssigner = "";
            StrOurNumber = "";
            StrBankName = "";

            return bb;

        }

        
        #endregion "HSBC Bank"
        /// <summary>
        /// Use to get and set our number while generating boleto 
        /// </summary>
        //Added by Pradeep Kushwaha on 24-nov-2010
        public String Our_Number
        {
            get { return _Our_Number; }

            set { _Our_Number = value; }
        }
        /// <summary>
        /// Use to get and set Boleto Bar code Number while generating boleto 
        /// </summary>
        //Added by Pradeep Kushwaha on 21-July-2011
        public String Boleto_BarCode_Number
        {
            get { return _Boleto_BarCode_Number; }

            set { _Boleto_BarCode_Number = value; }
        }
        /// <summary>
        /// Use to get and set Called from while boleto process
        /// </summary>
        //Added by Pradeep Kushwaha on 21-July-2011
        private String Called_From
        {
            get { return _Called_From; }

            set { _Called_From = value; }
        }
        #region "Itau Bank"
        public BilletBanking Bank_Itau(int bankCode, int _PolicyID, int _PolicyVersionID, int _CustomerID, int _InstallmentNO, int CoApplicant_id)
        {
            BilletBanking bb = new BilletBanking();
            //InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref  _CNPJ, ref i);
            InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref _CNPJ, ref StrCPF_CNPJ, ref strFName, ref StrMName, ref StrLName, ref Straddress1, ref StrDistrict, ref StrCity, ref StrCEP, ref InstallmentDate, ref DocumentNO,
                        _PolicyID, _PolicyVersionID, _CustomerID, _InstallmentNO, ref strAC_NUMBER, ref strBRANCH_NUMBER, ref strMAX_OUR_NUMBER, ref strAGREEMENT_NUMBER, ref strSUSEP_LOB_CODE, ref strBRANCH_CODE, ref StrPolicy_LOB_SUB_LOB, ref strAGENCY_DISPLAY_NAME, ref strEFFECTIVE_DATE, ref strEXPIRATION_DATE, ref _Total_InstallmentNO, ref strAPPICATION_NUMBER,
                        ref StrPOLICY_NUMBER, ref strENDORSEMENT_NO, ref strCO_INSURANCE, ref strPOLICY_VERIFY_DIGIT, ref strENDORSEMENT_VERIFY_DIGIT, ref strENDO_TYPE_DIGIT, ref strADDRESS2, ref strNUMBER, ref strSTATE_CODE, CoApplicant_id, ref strCancellationDate, ref strSTATE_NAME);

            bb.BankCode = 341;

            DateTime maturity = new DateTime(2008, 7, 4);

            //This code for adding instructions 
            //Instructions shows based on parametere 

            Instructions_Itau item1 = new Instructions_Itau(9, 5);
            Instructions_Itau item2 = new Instructions_Itau(81, 10);
            //old
            //Assginor asg = new Assginor("00.000.000/0000-00", "Shyam Lal & company", "0542", "13000");
            //New
            Assginor asg = new Assginor("00.000.000/0000-00", StrAssigner, StrAgencyCode, "13000");
            //cpfcnf,Assignor,ageny,Agency-account

            // Code provided by the agency, which is NOT the account number

            asg.Code = 945665878; // 7 positions

            //What evere value want to display pass all values from here with the help of object
            //old
            //Boleto b = new Boleto(maturity, 1642, "198", "92082835", asg, new Documentkind(341, 1));

            //New
            Boleto b = new Boleto(maturity, TotalAmt, "198", StrOurNumber, asg, new Documentkind(341, 1));

            b.Documentnumber = "1008073";
            //b.Especie = "15475";

            b.Drawee = new Drawee("000.000.000-00", " Praveen Kumar ");
            b.Drawee.Address.address = "A-147 Sector 20 Noida UP";
            b.Drawee.Address.District = "Gautam Buddh Nagar";
            b.Drawee.Address.City = "Noida";
            b.Drawee.Address.CEP = "00000000";
            b.Drawee.Address.UF = "UF";

            item2.Description += " " + item2.QuantityDays.ToString() + " calendar days of winning.";
            b.Instructions.Add(item1);
            b.Instructions.Add(item2);


            if (b.Discountvalue == 0)
            {
                Instructions_Itau item3 = new Instructions_Itau(999, 1);
                item3.Description += ("1.00 per day in anticipation.");
                b.Instructions.Add(item3);
            }
            bb.Boleto = b;
            bb.Boleto.Validates();
            // bb.ProofofDilivery = true;
            //bb.FormatMeat = true;
            return bb;

        }

        #endregion "Itau Bank"

        #region "Santander Bank"

        public BilletBanking Bank_Santander(int bankCode, int _PolicyID, int _PolicyVersionID, int _CustomerID, int _InstallmentNO, int CoApplicant_id)
        {

            BilletBanking bb = new BilletBanking();

            //InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref  _CNPJ, ref i);
            InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref _CNPJ, ref StrCPF_CNPJ, ref strFName, ref StrMName, ref StrLName, ref Straddress1, ref StrDistrict, ref StrCity, ref StrCEP, ref InstallmentDate, ref DocumentNO,
                          _PolicyID, _PolicyVersionID, _CustomerID, _InstallmentNO, ref strAC_NUMBER, ref strBRANCH_NUMBER, ref strMAX_OUR_NUMBER, ref strAGREEMENT_NUMBER, ref strSUSEP_LOB_CODE, ref strBRANCH_CODE, ref StrPolicy_LOB_SUB_LOB, ref strAGENCY_DISPLAY_NAME, ref strEFFECTIVE_DATE, ref strEXPIRATION_DATE, ref _Total_InstallmentNO, ref strAPPICATION_NUMBER,
                          ref StrPOLICY_NUMBER, ref strENDORSEMENT_NO, ref strCO_INSURANCE, ref strPOLICY_VERIFY_DIGIT, ref strENDORSEMENT_VERIFY_DIGIT, ref strENDO_TYPE_DIGIT, ref strADDRESS2, ref strNUMBER, ref strSTATE_CODE, CoApplicant_id, ref strCancellationDate, ref strSTATE_NAME);

            bb.BankCode = 33;


            DateTime maturity = new DateTime(2008, 7, 4);
            //Old
            //Assginor asg = new Assginor("00.000.000/0000-00", "Shyam Lal & company", "2269", "130000946");
            //New
            Assginor asg = new Assginor("00.000.000/0000-00", StrAssigner, StrAgencyCode, "130000946");

            // Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "2269", "130000946");
            asg.Code = 1795082;

            Boleto b = new Boleto(maturity, 0.20, "101", "566612457800", asg);
            //For Satander
            //Boleto b = new Boleto(maturity, 0.20, "101", "566612457800", asg); 
            b.OurNumber = "999999999999"; //Our Number must 12 positions for this bank
            //OUR NUMBER
            //############################################################################################################################
            //Number adopted and controlled by you, to identify the collection document.
            //Information used by banks to reference identifying the document object recovery.
            //May contain duplicate number in case of collection of bills, policy number, in case of recovery of insurance, etc..
            //This field is returned in the return file.

            b.Documentnumber = "0282033";

            b.Drawee = new Drawee("000.000.000-00", " Pradeep Kushwaha ");
            b.Drawee.Address.address = "Kalidi Kunj Sector 12 Noida UP";
            b.Drawee.Address.District = "Gautam Buddh Nagar";
            b.Drawee.Address.City = "Noida";
            b.Drawee.Address.CEP = "70000000";
            b.Drawee.Address.UF = "DF";



            //Espécie Documento - [R] Recibo
            b.documentkind = new Documentkind_Santander(17);

            bb.Boleto = b;
            bb.DisplayWalletCode = true;

            bb.Boleto.Validates();
            //bb.ProofofDilivery = true;
            return bb;
        }

        #endregion "Santander Bank"
        public String GetPolicyCancellationDate(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int INSTALLMENT_NO)
        {

            //DataTable returnval = null;
            String returnval = null;
            DataSet ds;
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objWrapper.AddParameter("@CALLEDFROM", "BOLETO");
                objWrapper.AddParameter("@INSTALLMENT_NO", INSTALLMENT_NO);

                ds = objWrapper.ExecuteDataSet("Proc_GetNonPayCancellationDate");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    returnval = ds.Tables[0].Rows[0]["CANCELLATION_DATE"].ToString();
                objWrapper.ClearParameteres();
                return returnval;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        
        private Double _Taxes=0;
        private Double _FINE_AMOUNT = 0;
        private Double _PENALITY = 0;//Late fee 
        private String _End_No = String.Empty;
        private String _App_Number = String.Empty;
        private String _DAYS_FOR_BOLETO_EXPIRATION = String.Empty;
         
        private Double Taxes 
        { 
            get {return _Taxes;} 
            set{ _Taxes=value;}
        }
        private String END_NO
        {
            get { return _End_No; }
            set { _End_No = value; }
        }
        private String APP_NUMBER
        {
            get { return _App_Number; }
            set { _App_Number = value; }
        }
        private Double FINE_AMOUNT
        {
            get { return _FINE_AMOUNT; }
            set { _FINE_AMOUNT = value; }
        }
        private Double PENALITY
        {
            get { return _PENALITY; }
            set { _PENALITY = value; }
        }
        private String DAYS_FOR_BOLETO_EXPIRATION
        {
            get { return _DAYS_FOR_BOLETO_EXPIRATION; }
            set { _DAYS_FOR_BOLETO_EXPIRATION = value; }
        }

        #region "Brazil Bank"
        public BilletBanking Bank_Brazil(int bankCode, int _PolicyID, int _PolicyVersionID, int _CustomerID, int _InstallmentNO, int CoApplicant_id)
        {
            BilletBanking bb = new BilletBanking();
            // InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref _CNPJ, ref i);
            InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref _CNPJ, ref StrCPF_CNPJ, ref strFName, ref StrMName, ref StrLName, ref Straddress1, ref StrDistrict, ref StrCity, ref StrCEP, ref InstallmentDate, ref DocumentNO,
                        _PolicyID, _PolicyVersionID, _CustomerID, _InstallmentNO, ref strAC_NUMBER, ref strBRANCH_NUMBER, ref strMAX_OUR_NUMBER, ref strAGREEMENT_NUMBER, ref strSUSEP_LOB_CODE, ref strBRANCH_CODE, ref StrPolicy_LOB_SUB_LOB, ref strAGENCY_DISPLAY_NAME, ref strEFFECTIVE_DATE, ref strEXPIRATION_DATE, ref _Total_InstallmentNO, ref strAPPICATION_NUMBER,
                        ref StrPOLICY_NUMBER, ref strENDORSEMENT_NO, ref strCO_INSURANCE, ref strPOLICY_VERIFY_DIGIT, ref strENDORSEMENT_VERIFY_DIGIT, ref strENDO_TYPE_DIGIT, ref strADDRESS2, ref strNUMBER, ref strSTATE_CODE, CoApplicant_id, ref strCancellationDate, ref strSTATE_NAME);

            #region Commented on 20-Jan-2011 by Pradeep
            //String strCancelationDate= GetPolicyCancellationDate(_CustomerID, _PolicyID, _PolicyVersionID, _InstallmentNO);
            #endregion

            //For Brazil
            bb.BankCode = 001;
            //DateTime maturity = new DateTime(2008, 7, 4);
            maturity = InstallmentDate;

            #region Portfolio Example 16, with our number 11 position
            /*
             * In this example we use the portfolio 16 and our maximum number of 11 positions.
             * It is not necessary to inform the number of covenant and not the kind of sport.
             * Our number has to be at most 11 positions.
             */

            Assginor asg = new Assginor(_CNPJ, StrAssigner, strBRANCH_NUMBER, "0", strAC_NUMBER.Substring(0, 8), int.Parse(strAGREEMENT_NUMBER), strAC_NUMBER.Replace("-", "").Substring(1, 8));//Need to Discuss


            BusinessLayer.BlBoleto.Boleto b = new BusinessLayer.BlBoleto.Boleto(maturity, TotalAmt, "16", strMAX_OUR_NUMBER, asg); //Need to verify

            #endregion Portfolio Example 16, with our number 11 position

            #region Portfolio Example 16, an agreement of 6 positions and type mode 21
            /*
             * In this example we use the portfolio and the number 16 of the covenant of 6 positions.
             * It is mandatory to inform the type of mode 21.
             * Our number has to be at most 10 positions.
             */
            //Assginor asg = new Assginor("00.000.000/0000-00", "Shyam Lal & company", "1234","1", "123456", "1");
            // asg.Convenio = 123456;

            //Boleto b = new Boleto(maturity, 0.01, "16", "09876543210", c);
            //b.modaltypedae = "21";
            #endregion Portfolio Example 16, an agreement of 6 positions and type mode 21

            #region Portfolio Example 18, with our number 11 position
            /*
             * This example we use the portfolio 18 and our maximum number of 11 positions.
             * It is not necessary to inform the number of covenant and not the kind of sport.
             * Our number has to be at most 11 positions.
             */
            //Assginor asg = new Assginor("00.000.000/0000-00", "Shyam Lal & company", "1234","1", "123456", "1");

            //Boleto b = new Boleto(vencimento, 0.01, "18", "09876543210", c);

            #endregion Portfolio Example 18, with our number 11 position

            #region Portfolio Example 18, an agreement of 6 positions and type mode 21
            /*
             * In this example we use the wallet 18 of the covenant and the number 6 position.
             * It is mandatory to inform the type of mode 21.
             * Our number has to be at most 10 positions.
             */

            //Assginor asg = new Assginor("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            //c.Convenio = 123456;

            //Boleto b = new Boleto(maturity, 0.01, "18", "09876543210", c);
            //b.TipoModalidade = "21";

            #endregion Portfolio Example 18, an agreement of 6 positions and type mode 21

            String strPolicyStartVeifyDigit = String.Empty;
            String strPolicyEndVeifyDigit = String.Empty;
            String strEndoStartVeifyDigit = String.Empty;
            String strEndoEndVeifyDigit = String.Empty;
            
            if(!String.IsNullOrEmpty(strCO_INSURANCE))
            {   //14548 - Leader
                //0 means direct business and ceded coinsurance, 
                //and 9 means Accepted CO (means ALBA is follower on the policy).
                //Accepted Coinsurance =ALBA is the Follower
                //Ceded Coinsurance =ALBA is the Leader

                if (strCO_INSURANCE.Trim() == "14547" || strCO_INSURANCE.Trim() == "14548")//14547 - Direct //14548 - Leader
                    strPolicyStartVeifyDigit = "0.";
                if (strCO_INSURANCE.Trim() == "14549")//14549 - Follower
                    strPolicyStartVeifyDigit = "9.";
            }
            if (!String.IsNullOrEmpty(strPOLICY_VERIFY_DIGIT))
                strPolicyEndVeifyDigit ="."+strPOLICY_VERIFY_DIGIT;
            if (!String.IsNullOrEmpty(strENDO_TYPE_DIGIT))
                strEndoStartVeifyDigit = strENDO_TYPE_DIGIT + ".";
            if (!String.IsNullOrEmpty(strENDORSEMENT_VERIFY_DIGIT))
                strEndoEndVeifyDigit ="."+strENDORSEMENT_VERIFY_DIGIT ;
         

            //b.Documentnumber = strBRANCH_CODE + " " + strSUSEP_LOB_CODE + " " + StrPOLICY_NUMBER+" "+ strENDORSEMENT_NO;//"12415487";
            b.Documentnumber = strBRANCH_CODE + " " + strSUSEP_LOB_CODE + " " + strPolicyStartVeifyDigit + StrPOLICY_NUMBER + strPolicyEndVeifyDigit + " "
                                + strEndoStartVeifyDigit + strENDORSEMENT_NO + strEndoEndVeifyDigit;//"12415487";
            b.Bankuse = strAC_NUMBER.Replace("-", "").Substring(2, 7);
            b.EspecieDoc = "SEGURO";// "SEGURO";//INSURANCE
            //b.BankAccount.AccountNumber = strAC_NUMBER.Replace("-", "").Substring(1, 8);
            b.Drawee = new Drawee(StrCPF_CNPJ, strFName);
            b.Drawee.Address.address = Straddress1 ;
            b.Drawee.Address.Completion =  strADDRESS2;
            b.Drawee.Address.Number = strNUMBER;
            b.Drawee.Address.District = StrDistrict;
            b.Drawee.Address.City = StrCity;
            b.Drawee.Address.CEP = StrCEP;     //CUSTOMER_ZIP zip code
            b.Drawee.Address.UF = strSTATE_CODE;//UF means state abbreviation
            #region Commented by Pradeep iTrack 685 Notes By Paula Dated 12-July-2011
            //b.Drawee.Address.State = strSTATE_NAME;
            #endregion


            //Adds instructions to the fetlock
            #region Instructions
            //strAGENCY_DISPLAY_NAME -- Agency Display Name
            //StrPolicy_LOB_SUB_LOB --Application Number 
            //StrPolicy_LOB_SUB_LOB --Policy Lob, sub lob and lob desc
            //_InstallmentNO --installment number/total
            //strFName  -- Applicant name
            //Policy/Endorsement Effective and expire date 
            //Cancellation Date of boleto payment (Do not receive after <expire date>).
           
            DateTime CancellDate = new DateTime();
            DateTime EFFECTIVE_DATE=new DateTime();
            DateTime EXPIRATION_DATE = new DateTime();

            CancellDate = Convert.ToDateTime(strCancellationDate);
            if (DAYS_FOR_BOLETO_EXPIRATION.ToString()!=String.Empty && _InstallmentNO == 1)
            {   //"Add # Days for Boleto Expiration if the installment #1 Itrack-1125
                CancellDate = CancellDate.AddDays(int.Parse(DAYS_FOR_BOLETO_EXPIRATION));
            }
            EFFECTIVE_DATE = Convert.ToDateTime(strEFFECTIVE_DATE);
            EXPIRATION_DATE = Convert.ToDateTime(strEXPIRATION_DATE);

            System.Globalization.CultureInfo oldculture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo(enumCulture.BR, true).NumberFormat;
            nfi.NumberDecimalDigits = 2;

            String format = "dd/MM/yyyy";
            if (strAPPICATION_NUMBER.ToString() != "")
                strAPPICATION_NUMBER = ", " + strAPPICATION_NUMBER ;

            String StrInstruction = strAGENCY_DISPLAY_NAME + strAPPICATION_NUMBER + "</br>" + StrPolicy_LOB_SUB_LOB
                + "</br>" + _InstallmentNO + "/" + _Total_InstallmentNO + ", " + strFName + "</br>"
                + "Vigência Original: " + EFFECTIVE_DATE.ToString(format) + " - " + EXPIRATION_DATE.ToString(format);
               //This message for fine and penalty printed on boletos must be printed only if boleto expire date is higher than boleto due date
               if(CancellDate>InstallmentDate)//Added by Pradeep itrack-1721 and tfs 1889 on 20-oct-2011
                    StrInstruction += "</br>Após vencimento: Multa de R$ " + Convert.ToDouble(PENALITY).ToString("N", nfi) + " + Juros de Mora por cada dia de atraso de R$ " + Convert.ToDouble(FINE_AMOUNT).ToString("N", nfi) + "";

           
            //Do not receive after <expire date>.
            if (strCancellationDate != "")
            {
                StrInstruction += " </br>Não receber após " + CancellDate.ToString(format) + ". Após esta data, procurar a seguradora.";
            }
            if(!String.IsNullOrEmpty(END_NO))
                END_NO="/"+END_NO;

            StrInstruction += "</br> </br> </br>Valor do IOF em R$ " + Convert.ToDouble(Taxes).ToString("N", nfi) + " - Proposta/PT " + APP_NUMBER + END_NO + " </br> Este pagamento não quita débitos anteriores, nem reativa apólice já cancelada.";

            System.Threading.Thread.CurrentThread.CurrentCulture = oldculture;
            //Protestar
            Instruction_BankBrazil item = new Instruction_BankBrazil(9, 5);
            item.Description = StrInstruction;
            b.Instructions.Add(item);

            //ImportanciaporDiaDesconto
            //item = new Instruction_BankBrazil(30, 0);
            
            //b.Instructions.Add(item);

            //ProtestarAposNDiasCorridos
            //item = new Instruction_BankBrazil(81, 15);
            //item.Description += " " + item.QuantityDays.ToString() + " calendar days of winning.";
            //b.Instructions.Add(item);

            #endregion Instructions
           
            
            bb.CurrencySymbol = CurrencySymbol;
            
            bb.Boleto = b;

            bb.Boleto.Validates();
            this.Our_Number = b.OurNumber;
            this.Boleto_BarCode_Number = b.Barcode.Code.ToString();//added by Pradeep on 20-July-2011
            // bb.ProofofDilivery = true;
            
            //Re Initialize varriable 
            TotalAmt = 0;
            StrAgencyCode = "";
            StrAssigner = "";
            StrOurNumber = "";
            StrBankName = "";
          
            return bb;


            #region Comment Old Code For B R A Z I L B A N K
            //BilletBanking bb = new BilletBanking();
            //// InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref _CNPJ, ref i);

            //InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref _CNPJ, ref StrCPF_CNPJ, ref strFName, ref StrMName, ref StrLName, ref Straddress1, ref StrDistrict, ref StrCity, ref StrCEP, ref InstallmentDate, ref DocumentNO,
            //   _PolicyID, _PolicyVersionID, _CustomerID, _InstallmentNO);


            ////For Brazil
            //bb.BankCode = 001;
            //      //DateTime maturity = new DateTime(2008, 7, 4);
            //DateTime maturity = new DateTime(2008, 11, 11);

            //#region Portfolio Example 16, with our number 11 position
            ///*
            // * In this example we use the portfolio 16 and our maximum number of 11 positions.
            // * It is not necessary to inform the number of covenant and not the kind of sport.
            // * Our number has to be at most 11 positions.
            // */

            //Assginor asg = new Assginor("00.000.000/0000-00", "Shyam Lal & company", "1234", "1", "123456", "1");


            //BusinessLayer.BlBoleto.Boleto b = new BusinessLayer.BlBoleto.Boleto(maturity, 0.01, "16", "09876543210", asg);

            //#endregion Portfolio Example 16, with our number 11 position

            //#region Portfolio Example 16, an agreement of 6 positions and type mode 21
            ///*
            // * In this example we use the portfolio and the number 16 of the covenant of 6 positions.
            // * It is mandatory to inform the type of mode 21.
            // * Our number has to be at most 10 positions.
            // */
            ////Assginor asg = new Assginor("00.000.000/0000-00", "Shyam Lal & company", "1234","1", "123456", "1");
            //// asg.Convenio = 123456;

            ////Boleto b = new Boleto(maturity, 0.01, "16", "09876543210", c);
            ////b.modaltypedae = "21";
            //#endregion Portfolio Example 16, an agreement of 6 positions and type mode 21

            //#region Portfolio Example 18, with our number 11 position
            ///*
            // * This example we use the portfolio 18 and our maximum number of 11 positions.
            // * It is not necessary to inform the number of covenant and not the kind of sport.
            // * Our number has to be at most 11 positions.
            // */
            ////Assginor asg = new Assginor("00.000.000/0000-00", "Shyam Lal & company", "1234","1", "123456", "1");

            ////Boleto b = new Boleto(vencimento, 0.01, "18", "09876543210", c);

            //#endregion Portfolio Example 18, with our number 11 position

            //#region Portfolio Example 18, an agreement of 6 positions and type mode 21
            ///*
            // * In this example we use the wallet 18 of the covenant and the number 6 position.
            // * It is mandatory to inform the type of mode 21.
            // * Our number has to be at most 10 positions.
            // */

            ////Assginor asg = new Assginor("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            ////c.Convenio = 123456;

            ////Boleto b = new Boleto(maturity, 0.01, "18", "09876543210", c);
            ////b.TipoModalidade = "21";

            //#endregion Portfolio Example 18, an agreement of 6 positions and type mode 21


            //b.Documentnumber = "12415487";

            //b.Drawee = new Drawee("000.000.000-00", "Pravesh K. Chandel");
            //b.Drawee.Address.address = "Sector-34 H.N. 250";
            //b.Drawee.Address.District = "Gautam Buddh Nagar";
            //b.Drawee.Address.City = "Noida";
            //b.Drawee.Address.CEP = "00000000";
            //b.Drawee.Address.UF = "UF";

            ////Adds instructions to the fetlock
            //#region Instructions
            ////Protestar
            //Instruction_BankBrazil item = new Instruction_BankBrazil(9, 5);
            //b.Instructions.Add(item);

            ////ImportanciaporDiaDesconto
            //item = new Instruction_BankBrazil(30, 0);
            //b.Instructions.Add(item);

            ////ProtestarAposNDiasCorridos
            //item = new Instruction_BankBrazil(81, 15);
            //b.Instructions.Add(item);

            //#endregion Instructions


            //bb.Boleto = b;

            //bb.Boleto.Validates();

            //// bb.ProofofDilivery = true;

            //return bb;
            #endregion Till Here
        }

        #endregion "Brazil Bank"

        #region "Sudameris Bank"

        public BilletBanking Bank_Sudameris(int bankCode, int _PolicyID, int _PolicyVersionID, int _CustomerID, int _InstallmentNO, int CoApplicant_id)
        {


            BilletBanking bb = new BilletBanking();

            //InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref  _CNPJ, ref i);

            InitializeElementsOfBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref _CNPJ, ref StrCPF_CNPJ, ref strFName, ref StrMName, ref StrLName, ref Straddress1, ref StrDistrict, ref StrCity, ref StrCEP, ref InstallmentDate, ref DocumentNO,
                        _PolicyID, _PolicyVersionID, _CustomerID, _InstallmentNO, ref strAC_NUMBER, ref strBRANCH_NUMBER, ref strMAX_OUR_NUMBER, ref strAGREEMENT_NUMBER, ref strSUSEP_LOB_CODE, ref strBRANCH_CODE, ref StrPolicy_LOB_SUB_LOB, ref strAGENCY_DISPLAY_NAME, ref strEFFECTIVE_DATE, ref strEXPIRATION_DATE, ref _Total_InstallmentNO, ref strAPPICATION_NUMBER,
                        ref StrPOLICY_NUMBER, ref strENDORSEMENT_NO, ref strCO_INSURANCE, ref strPOLICY_VERIFY_DIGIT, ref strENDORSEMENT_VERIFY_DIGIT, ref strENDO_TYPE_DIGIT, ref strADDRESS2, ref strNUMBER, ref strSTATE_CODE, CoApplicant_id, ref strCancellationDate, ref strSTATE_NAME);

            //For Sudameris
            bb.BankCode = 347;

            DateTime maturity = new DateTime(2008, 7, 4);
            //Old
            //Assginor asg = new Assginor("00.000.000/0000-00", "Titanic Ship Corporation", "0501", "6703255");
            //New
            Assginor asg = new Assginor("00.000.000/0000-00", StrAssigner, "0501", "6703255");
            asg.Code = 13000;

            //Our 7-digit number
            string nn = "0003020";
            //Our 13-digit number
            //nn = "0000000003025";

            Boleto b = new Boleto(maturity, 1642, "198", nn, asg);// EnumEspecieDocumento_Sudameris.DuplicataMercantil);

            b.Documentnumber = "1008073";

            b.Drawee = new Drawee("000.000.000-00", "Lalit singh Chauhan");
            b.Drawee.Address.address = "D-147 Sector 20 Noida UP";
            b.Drawee.Address.District = "Gautam Buddh Nagar";
            b.Drawee.Address.City = "Noida";
            b.Drawee.Address.CEP = "70000000";
            b.Drawee.Address.UF = "DF";


            bb.Boleto = b;
            bb.DisplayWalletCode = true;

            bb.Boleto.Validates();
            //bb.ProofofDilivery = true;
            return bb;


        }

        #endregion "Sudameris Bank"


        private void InitializeElementsOfBoleto(ref Double TotalAmt, ref String StrAgencyCode, ref String StrAssigner, ref String StrOurNumber, ref String StrBankName, ref string _CNPJ, ref String StrCPF_CNPJ, ref String strFName, ref String StrMName, ref String StrLName, ref String Straddress1, ref String StrDistrict, ref String StrCity, ref String StrCEP, ref DateTime InstallmentDate, ref String DocumentNO,
           int _PolicyID, int _PolicyVersionID, int _CustomerID, int _InstallmentNO, ref String strAC_NUMBER, ref String strBRANCH_NUMBER, ref String strMAX_OUR_NUMBER, ref String strAGREEMENT_NUMBER,
          ref String strSUSEP_LOB_CODE, ref String strBRANCH_CODE, ref String StrPolicy_LOB_SUB_LOB, ref String strAGENCY_DISPLAY_NAME, ref String strEFFECTIVE_DATE, ref String strEXPIRATION_DATE, ref String _Total_InstallmentNO, ref String strAPPICATION_NUMBER, ref String StrPOLICY_NUMBER, ref String strENDORSEMENT_NO,
          ref String strCO_INSURANCE, ref String strPOLICY_VERIFY_DIGIT, ref String strENDORSEMENT_VERIFY_DIGIT, ref String strENDO_TYPE_DIGIT, ref String strADDRESS2, ref String strNUMBER, ref String strSTATE_CODE, int CoApplicant_id, ref String strCancellationDate, ref String strSTATE_NAME)
        {
            //ref string _CNPJ,
            DataSet oDS = new DataSet();
            oDS = ReturnBoletoValues(_PolicyID, _PolicyVersionID, _CustomerID, _InstallmentNO, 1, "001", CoApplicant_id);

            if (oDS.Tables[0].Rows.Count > 0)
            {
                //Table 0 'ACT_POLICY_INSTALLMENT_DETAILS'
                TotalAmt = Double.Parse(oDS.Tables[0].Rows[0]["TOTAL"].ToString());
                InstallmentDate = DateTime.Parse(oDS.Tables[0].Rows[0]["INSTALLMENT_EFFECTIVE_DATE"].ToString());
                _InstallmentNO = int.Parse(oDS.Tables[0].Rows[0]["INSTALLMENT_NO"].ToString());
                _Total_InstallmentNO = oDS.Tables[0].Rows[0]["TOTAL_INSTALL_NO"].ToString();
                strCancellationDate = oDS.Tables[0].Rows[0]["INSTALLMENT_EXPIRE_DATE"].ToString();
                Taxes = Double.Parse(oDS.Tables[0].Rows[0]["TAXES"].ToString());
            }

            //TABLE 1 'COMPANY INFORMATION'
            if (oDS.Tables[1].Rows.Count > 0)
            {
                _CNPJ = oDS.Tables[1].Rows[0]["CARRIER_CNPJ"].ToString();
                StrAssigner = oDS.Tables[1].Rows[0]["REIN_COMAPANY_NAME"].ToString();//Company Name
                DAYS_FOR_BOLETO_EXPIRATION = oDS.Tables[1].Rows[0]["DAYS_FOR_BOLETO_EXPIRATION"].ToString();//"# Days for Boleto Expiration
                //DocumentNO = oDS.Tables[1].Rows[0]["DOCUMENT_NO"].ToString();
            }

            //TABLE 2 'BANK INFORMATION'
            if (oDS.Tables[2].Rows.Count > 0)
            {
                StrOurNumber = oDS.Tables[2].Rows[0]["STARTING_OUR_NUMBER"].ToString();
                StrBankName = oDS.Tables[2].Rows[0]["BANK_NAME"].ToString();

                //strBANK_NUMBER = oDS.Tables[2].Rows[0]["BANK_NUMBER"].ToString();
                strAC_NUMBER = oDS.Tables[2].Rows[0]["AC_NUMBER"].ToString();
                strBRANCH_NUMBER = oDS.Tables[2].Rows[0]["BRANCH_NUMBER"].ToString();
                strMAX_OUR_NUMBER = oDS.Tables[2].Rows[0]["MAX_OUR_NUMBER"].ToString();
                strAGREEMENT_NUMBER = oDS.Tables[2].Rows[0]["AGREEMENT_NUMBER"].ToString();
            }


            //TABLE 3 'AGENCY INFORMATION'
            if (oDS.Tables[3].Rows.Count > 0)
            {

                StrAgencyCode = oDS.Tables[3].Rows[0]["NUM_AGENCY_CODE"].ToString();
                strAGENCY_DISPLAY_NAME = oDS.Tables[3].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
            }

            //TABLE 4 'CUSTOMER INFORMATION'
            if (oDS.Tables[4].Rows.Count > 0)
            {

                StrCPF_CNPJ = oDS.Tables[4].Rows[0]["CPF_CNPJ"].ToString();
          
                strFName = oDS.Tables[4].Rows[0]["NAME"].ToString();
                //strFName = oDS.Tables[4].Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                //StrMName = oDS.Tables[4].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                //StrLName = oDS.Tables[4].Rows[0]["CUSTOMER_LAST_NAME"].ToString();

                Straddress1 = oDS.Tables[4].Rows[0]["ADDRESS1"].ToString();
                strADDRESS2 = oDS.Tables[4].Rows[0]["ADDRESS2"].ToString();
                strNUMBER = oDS.Tables[4].Rows[0]["NUMBER"].ToString();
                StrDistrict = oDS.Tables[4].Rows[0]["DISTRICT"].ToString();

                StrCity = oDS.Tables[4].Rows[0]["CITY"].ToString();
                StrCEP = oDS.Tables[4].Rows[0]["ZIP_CODE"].ToString();
                strSTATE_CODE = oDS.Tables[4].Rows[0]["STATE_CODE"].ToString();
                strSTATE_NAME = oDS.Tables[4].Rows[0]["STATE_NAME"].ToString();
            }
            //TABLE 5
            if (oDS.Tables[5].Rows.Count > 0)
            {
                strSUSEP_LOB_CODE = oDS.Tables[5].Rows[0]["SUSEP_LOB_CODE"].ToString();
                StrPolicy_LOB_SUB_LOB = oDS.Tables[5].Rows[0]["SUSEP_LOB_CODE"].ToString() + "." + oDS.Tables[5].Rows[0]["SUSEP_CODE"].ToString() + " - " + oDS.Tables[5].Rows[0]["LOB_DESC"].ToString();
                StrPOLICY_NUMBER = oDS.Tables[5].Rows[0]["POLICY_NUMBER"].ToString();
                strCO_INSURANCE  =oDS.Tables[5].Rows[0]["CO_INSURANCE"].ToString();
                strPOLICY_VERIFY_DIGIT =oDS.Tables[5].Rows[0]["POLICY_VERIFY_DIGIT"].ToString();
            }
            
            if (oDS.Tables[6].Rows.Count > 0)
                strBRANCH_CODE = oDS.Tables[6].Rows[0]["BRANCH_CODE"].ToString();
             
            if (oDS.Tables[7].Rows.Count > 0)
             {
                strEFFECTIVE_DATE = oDS.Tables[7].Rows[0]["EFFECTIVE_DATE"].ToString();
                strEXPIRATION_DATE = oDS.Tables[7].Rows[0]["EXPIRATION_DATE"].ToString();
                strAPPICATION_NUMBER = oDS.Tables[7].Rows[0]["APPICATION_NUMBER"].ToString();
                strENDORSEMENT_NO = oDS.Tables[7].Rows[0]["ENDORSEMENT_NO"].ToString();
                strENDORSEMENT_VERIFY_DIGIT = oDS.Tables[7].Rows[0]["ENDORSEMENT_VERIFY_DIGIT"].ToString();
                strENDO_TYPE_DIGIT = oDS.Tables[7].Rows[0]["ENDO_TYPE_DIGIT"].ToString();
                APP_NUMBER = oDS.Tables[7].Rows[0]["APP_NUMBER"].ToString();
                END_NO = oDS.Tables[7].Rows[0]["END_NO"].ToString();
             }
            if (oDS.Tables[8].Rows.Count > 0)
            {
                FINE_AMOUNT = double.Parse(oDS.Tables[8].Rows[0]["FINE_AMOUNT"].ToString());
                PENALITY =double.Parse( oDS.Tables[8].Rows[0]["PENALITY"].ToString());
                
            }  
            

            oDS.Dispose();
        }


        private BilletBanking displayBolletos(int policyid, int policyVersionID, int CustomerID, int InstallmentNo, short BankCode, int CoApplicant_id)
        {


            switch (BankCode)
            {
                case 399: // HSBC bank

                    return Bank_HSBC(399, policyid, policyVersionID, CustomerID, InstallmentNo, CoApplicant_id);

                //break;

                case 341: // Bank_Itau
                    return Bank_Itau(341, policyid, policyVersionID, CustomerID, InstallmentNo, CoApplicant_id);
                // break;

                case 33: // Bank_Santander
                    return Bank_Santander(33, policyid, policyVersionID, CustomerID, InstallmentNo, CoApplicant_id);

                //break;

                case 1: // Bank_Brazil
                    return Bank_Brazil(001, policyid, policyVersionID, CustomerID, InstallmentNo, CoApplicant_id);

                // break;

                case 347: // Bank_Brazil
                    return Bank_Sudameris(347, policyid, policyVersionID, CustomerID, InstallmentNo, CoApplicant_id);

                // break;

                default:
                    return Bank_HSBC(399, policyid, policyVersionID, CustomerID, InstallmentNo, CoApplicant_id);


            }
        }



        public String RetunHTMLforBoleto(String _GENERATE_ALL_INSTALLMENT, int _CustomerID, int _PolicyID, int _PolicyVersionID, int _InstallmentID, int _InstallmentNO, int userid, int _CO_APPLICANT_ID)
        {
            try
            {
                //String BoletoHTMl="";
                int installmentNo;
               // GenerateBoletos(_GENERATE_ALL_INSTALLMENT, _CustomerID, _PolicyID, _PolicyVersionID, _InstallmentID, _InstallmentNO, userid);
                if (_GENERATE_ALL_INSTALLMENT == "ALL")//for a all installments
                    installmentNo = 0;
                else
                    installmentNo = _InstallmentNO;
                StringBuilder strlbldr = new StringBuilder();
                DataSet DS = ReturnGeneratedBoletoDataset(_CustomerID, _PolicyID, _PolicyVersionID, 0, installmentNo, "GENERATED_BOLETO", _CO_APPLICANT_ID);
                //Itrack 685 Page break implementation 
                Int32 Count = 0;
                foreach (DataRow dr in DS.Tables[0].Rows)
                {
                    if (Count < DS.Tables[0].Rows.Count - 1)//Itrack 685
                    {
                        strlbldr.Append("<div style='page-break-after: always'>");

                        if (dr["RELEASED_STATUS"].ToString() == "Y")// itrack 1352
                            strlbldr.Append(dr["BOLETO_HTML"].ToString().Replace("<div>", "<div style='background-image:url(../../cmsweb/Images/PAID.gif);'>"));//Watermark on paid boleto
                        else
                            strlbldr.Append(dr["BOLETO_HTML"].ToString());

                        strlbldr.Append("</div>");

                    }
                    else
                    {
                        if (dr["RELEASED_STATUS"].ToString() == "Y")//Itrack 685
                            strlbldr.Append(dr["BOLETO_HTML"].ToString().Replace("<div>", "<div style='background-image:url(../../cmsweb/Images/PAID.gif);'>")); //Watermark on paid boleto
                        else
                            strlbldr.Append(dr["BOLETO_HTML"].ToString());
                    }

                    Count++;
                }
                //till here 
                return strlbldr.ToString();
                


            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


        }

        private short GetBankCode()
        {
            short code;

            //code = 399;  // HSBC

            //code = 341; //Itau

            //code = 33;  //Bank_Santander

           code = 001; //Bank_Brazil

            //code = 347; //Bank_Sudameris

            return code;
        }
        //Generate Boleto and Save it
        private void GenerateHTMLAndSave(int POLICY_ID, int POLICY_VERSION_ID, int CUSTOMER_ID, int INSTALLMENT_NO, int userid, int rowid, short bankCode, int CoApplicant_ID)
        {

            BilletBanking bb = displayBolletos(POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, INSTALLMENT_NO, GetBankCode(), CoApplicant_ID);

            System.Web.UI.WebControls.Panel Panel1 = new System.Web.UI.WebControls.Panel();

            Panel1.Controls.Clear();
            Panel1.Controls.Add(bb);

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htmlTW = new HtmlTextWriter(sw);
            
            Panel1.RenderControl(htmlTW);
            String html = sw.ToString();
           
            int j = SaveGeneratedBoleto(ref TotalAmt, ref StrAgencyCode, ref StrAssigner, ref StrOurNumber, ref StrBankName, ref html,
               POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, INSTALLMENT_NO,
               ref userid, 0, rowid, bankCode,this.Our_Number);

            html = "";
            sw.Dispose();
            htmlTW.Dispose();
            //bb.Dispose();
        }
        /// <summary>
        /// Generate Boleto with updated record for boleto reprint 
        /// </summary>
        /// <param name="CUSTOMER_ID"></param>
        /// <param name="POLICY_ID"></param>
        /// <param name="POLICY_VERSION_ID"></param>
        /// <param name="INSTALLEMT_ID"></param>
        /// <param name="INSTALLMENT_NO"></param>
        //Added By Pradeep Kushwaha on 09-Nov-2010
        public void GenereteBoletoForRePrint(Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID, Int32 INSTALLEMT_ID, Int32 INSTALLMENT_NO, Int32 USER_ID, int CO_APPLICANT_ID)
        {

            try
            {
                short bankCode = GetBankCode();
                Called_From = "BOLETO_REPRINT";
                BilletBanking bb = displayBolletos(POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, INSTALLMENT_NO, bankCode, CO_APPLICANT_ID);

                System.Web.UI.WebControls.Panel Panel1 = new System.Web.UI.WebControls.Panel();

                Panel1.Controls.Clear();
                Panel1.Controls.Add(bb);

                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htmlTW = new HtmlTextWriter(sw);

                Panel1.RenderControl(htmlTW);
                String html = sw.ToString();

                int j = SaveGeneratedBoletoForReprint(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, INSTALLEMT_ID, INSTALLMENT_NO, bankCode, html, USER_ID, this.Our_Number);
                
                html = "";
                sw.Dispose();
                htmlTW.Dispose();
 
            }
            catch (Exception ex)
            {
                throw new Exception("Error while Update" + ex.Message);
            }

        }
        /// <summary>
        /// Generate Boleto from Commit process
        /// </summary>
        public ArrayList GenerateBoletoFromCommitProcess(int _CustomerID, int _PolicyID, int _PolicyVersionID, int User_id)
        {
            short bankCode = GetBankCode();
            DataSet InstallDetailsDs = ReturnGeneratedBoletoDataset(_CustomerID, _PolicyID, _PolicyVersionID, 0, 0, "INSTALLMENT",0);
            ArrayList arFiles = new ArrayList();
            string CoApplicant = "";
            foreach (DataRow dr in InstallDetailsDs.Tables[0].Rows)
            {
               if (!string.IsNullOrEmpty(dr["INSTALLMENT_AMOUNT"].ToString()) && double.Parse(dr["INSTALLMENT_AMOUNT"].ToString()) > 0)
               {
                   GenerateHTMLAndSave(int.Parse(dr["POLICY_ID"].ToString()), int.Parse(dr["POLICY_VERSION_ID"].ToString()), int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(dr["INSTALLMENT_NO"].ToString()), User_id, int.Parse(dr["ROW_ID"].ToString()), bankCode, int.Parse(dr["CO_APPLICANT_ID"].ToString()));
                   if (CoApplicant != dr["CO_APPLICANT_ID"].ToString())
                   {
                       CoApplicant = dr["CO_APPLICANT_ID"].ToString();
                       arFiles.Add("BOLETO_" + _CustomerID.ToString() + "_" + _PolicyID.ToString() + "_" + _PolicyVersionID.ToString() + "_" + dr["CO_APPLICANT_ID"].ToString());
                   }
               }
            }
            return arFiles;
        }

        #region Generate BOLETO of Initial Load Added by Pradeep Kushwaha
        /*
        /// <summary>
        /// Generate  Boletos of Inital Load Data based on the criteria  
        /// </summary>
        public ArrayList GenerateBoletoOfInitialLoadData(int _CustomerID, int _PolicyID, int _PolicyVersionID, int User_id)
        {
            try
            {
                //Get the bank code 
                short bankCode = GetBankCode();
                ArrayList arFiles = new ArrayList();
                string CoApplicant = "";
                //get the geneated boleto details dataset 
                DataSet InstallDetailsDs = ReturnGeneratedBoletoDataset(_CustomerID, _PolicyID, _PolicyVersionID,0, 0, "INSTALLMENT", 0);
                //loop through the table rows 
                foreach (DataRow dr in InstallDetailsDs.Tables[0].Rows)
                {
                    //installment amount should not be less then zero
                    if (double.Parse(dr["INSTALLMENT_AMOUNT"].ToString()) > 0)
                    {
                        //To regenerate Boleto html and update
                        Called_From="INITIAL_LOAD";
                        this.ReGenerateHTMLAndUpdate(int.Parse(dr["POLICY_ID"].ToString()), int.Parse(dr["POLICY_VERSION_ID"].ToString()), int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(dr["INSTALLMENT_NO"].ToString()), User_id, int.Parse(dr["ROW_ID"].ToString()), bankCode, int.Parse(dr["CO_APPLICANT_ID"].ToString()));
                        this.UpdateInitialLoalPolicyBoletoData(int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(dr["POLICY_ID"].ToString()), int.Parse(dr["POLICY_VERSION_ID"].ToString()), int.Parse(dr["ROW_ID"].ToString()));
                        if (CoApplicant != dr["CO_APPLICANT_ID"].ToString())
                        {
                            CoApplicant = dr["CO_APPLICANT_ID"].ToString();
                            arFiles.Add("BOLETO_" + _CustomerID.ToString() + "_" + _PolicyID.ToString() + "_" + _PolicyVersionID.ToString() + "_" + dr["CO_APPLICANT_ID"].ToString());
                        }//if (CoApplicant != dr["CO_APPLICANT_ID"].ToString())

                    }//if (double.Parse(dr["INSTALLMENT_AMOUNT"].ToString()) > 0)
                }//foreach (DataRow dr in InstallDetailsDs.Tables[0].Rows)

                return arFiles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error during execution of the initial load Boleto transaction.", ex);
            }//catch (Exception ex)
           
        }//public void RegenerateBoleto(int _CustomerID, int _PolicyID, int _PolicyVersionID, int User_id)
        */
        /*
        /// <summary>
        /// Update Initial Loal Policy Boleto Data
        /// </summary>
        /// <param name="_customer_id"></param>
        /// <param name="_policy_id"></param>
        /// <param name="_policy_version_id"></param>
        /// <param name="_installment_id"></param>
        /// <returns></returns>
        private int UpdateInitialLoalPolicyBoletoData(int _customer_id,int _policy_id,int _policy_version_id,int _installment_id)
        {
            int intResult = 0;

            String strStoredProc = "PROC_POL_IL_UPDATE_POLICY_BOLETO_DATA";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", _customer_id);
                objWrapper.AddParameter("@POLICY_ID", _policy_id);
                objWrapper.AddParameter("@POLICY_VERSION_ID", _policy_version_id);
                objWrapper.AddParameter("@INSTALLEMT_ID", _installment_id);
                objWrapper.AddParameter("@CALLED_FROM", "BOLETO");
                intResult = objWrapper.ExecuteNonQuery(strStoredProc);

                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {

                throw (new Exception("Error while Updating Regenerated Boleto record.", ex.InnerException));
            }
            return intResult;
        }//private void UpdateInitialLoalPolicyBoletoData(int _customer_id,int _policy_id,int _policy_version_id,int _installment_id)
        */
        #endregion

        #region Regenerate Boleto Html and Update - Added by Pradeep on 19-July-2011   iTrack 1383,1361,1380
        /// <summary>
        /// Regenerate  Boletos based on the criteria  
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <param name="_PolicyID"></param>
        /// <param name="_PolicyVersionID"></param>
        /// <param name="User_id"></param>
        
        public void RegenerateBoleto(int _CustomerID, int _PolicyID, int _PolicyVersionID, int User_id)
        {
            try
            {
                //Get the bank code 
                short bankCode = GetBankCode();
                //get the geneated boleto details dataset 
                DataSet InstallDetailsDs = ReturnGeneratedBoletoDataset(_CustomerID, _PolicyID, _PolicyVersionID, 0, 0, "INSTALLMENT", 0);
                //loop through the table rows 
                foreach (DataRow dr in InstallDetailsDs.Tables[0].Rows)
                {
                    //installment amount should not be less then zero
                    if (double.Parse(dr["INSTALLMENT_AMOUNT"].ToString()) > 0)
                    {
                        //To regenerate Boleto html and update
                        ReGenerateHTMLAndUpdate(int.Parse(dr["POLICY_ID"].ToString()), int.Parse(dr["POLICY_VERSION_ID"].ToString()), int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(dr["INSTALLMENT_NO"].ToString()), User_id, int.Parse(dr["ROW_ID"].ToString()), bankCode, int.Parse(dr["CO_APPLICANT_ID"].ToString()));

                    }//if (double.Parse(dr["INSTALLMENT_AMOUNT"].ToString()) > 0)
                }//foreach (DataRow dr in InstallDetailsDs.Tables[0].Rows)
            }
            catch (Exception ex)
            {
                throw new Exception("Error during execution of the Regenerate Boleto transaction.", ex);
            }//catch (Exception ex)
        }//public void RegenerateBoleto(int _CustomerID, int _PolicyID, int _PolicyVersionID, int User_id)

        /// <summary>
        /// ReGenerate Boleto and Update it
        /// </summary>
        /// <param name="POLICY_ID"></param>
        /// <param name="POLICY_VERSION_ID"></param>
        /// <param name="CUSTOMER_ID"></param>
        /// <param name="INSTALLMENT_NO"></param>
        /// <param name="userid"></param>
        /// <param name="rowid"></param>
        /// <param name="bankCode"></param>
        /// <param name="CoApplicant_ID"></param>
        private void ReGenerateHTMLAndUpdate(int POLICY_ID, int POLICY_VERSION_ID, int CUSTOMER_ID, int INSTALLMENT_NO, int userid, int rowid, short bankCode, int CoApplicant_ID)
        {
            try
            {
                //Generate boleto html
                BilletBanking bb = displayBolletos(POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, INSTALLMENT_NO, bankCode, CoApplicant_ID);

                System.Web.UI.WebControls.Panel Panel1 = new System.Web.UI.WebControls.Panel();

                Panel1.Controls.Clear();
                Panel1.Controls.Add(bb);

                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htmlTW = new HtmlTextWriter(sw);

                Panel1.RenderControl(htmlTW);
                //get generated html
                String html = sw.ToString();
                //Save generated boleto
                int j = SaveReGeneratedBoleto(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, rowid, INSTALLMENT_NO, bankCode, html, userid, this.Our_Number);

                html = "";
                sw.Dispose();
                htmlTW.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Error while Update" + ex.Message);
            }//catch (Exception ex)
        }//private void ReGenerateHTMLAndUpdate(int POLICY_ID, int POLICY_VERSION_ID, int CUSTOMER_ID, int INSTALLMENT_NO, int userid, int rowid, short bankCode, int CoApplicant_ID)
        
        
        /// <summary>
        /// Update regenerated boleto html based on criteria (Customer id ,policy id, policy version id, instalment id and installmet no )
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="INSTALLEMT_ID"></param>
        /// <param name="_InstallmentNO"></param>
        /// <param name="BANK_ID"></param>
        /// <param name="html"></param>
        /// <param name="userid"></param>
        /// <param name="Our_number"></param>
        /// <returns></returns>
        public int SaveReGeneratedBoleto(Int32 CustomerID, Int32 PolicyID, Int32 PolicyVersionID, Int32 INSTALLEMT_ID,
            Int32 _InstallmentNO, Int32 BANK_ID, String html, Int32 userid, String Our_number)
        {
            int intResult = 0;

            String strStoredProc = "Proc_SAVE_POL_INSTALLMENT_BOLETO";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@INSTALLEMT_ID", INSTALLEMT_ID);
                objWrapper.AddParameter("@INSTALLMENT_NO", _InstallmentNO);
                objWrapper.AddParameter("@BANK_ID", BANK_ID);
                objWrapper.AddParameter("@BOLETO_HTML", html);
                objWrapper.AddParameter("@CREATED_BY", userid);
                objWrapper.AddParameter("@CREATED_DATETIME", DateTime.Now);
                objWrapper.AddParameter("@MODIFIED_BY", null);
                objWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);
                objWrapper.AddParameter("@CALLED_FOR", "REGENERATE_BOLETO");
                objWrapper.AddParameter("@OUR_NUMBER", Our_number);
                objWrapper.AddParameter("@BOLETO_BARCODE_NUMBER", Boleto_BarCode_Number);
                intResult = objWrapper.ExecuteNonQuery(strStoredProc);

                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {

                throw (new Exception("Error while Updating Regenerated Boleto record.", ex.InnerException));
            }
            return intResult;
        }
        #endregion

        public void GenerateBoletos(String _GENERATE_ALL_INSTALLMENT, int _CustomerID, int _PolicyID, int _PolicyVersionID, int _InstallmentID, int _InstallmentNO, int userid)
        {
            short bankCode = GetBankCode();
            int tmpInstallmentNo;
            //Generating Boleto for all Installments
            if (_GENERATE_ALL_INSTALLMENT == "ALL")
                tmpInstallmentNo = 0;
            else
                tmpInstallmentNo = _InstallmentNO;

            //In case if use want to generate all boleto first check 
            // for all  installment boleto is generated or not
            DataSet oDS;
            oDS = ReturnGeneratedBoletoDataset(_CustomerID, _PolicyID, _PolicyVersionID, 0, tmpInstallmentNo, "GENERATED_BOLETO",0);
            foreach (DataRow dr in oDS.Tables[0].Rows)
            {
                //Get BOLETO_ID POL_INSTALLMENT_BOLETO check this, if generated then will not be 0 
                //If RELEASED_STATUS IS YES AND boleto is generated then show it from boleto table
                //if (!(dr["RELEASED_STATUS"].ToString().Trim() == "Y" && dr["BOLETO_HTML"].ToString() != ""))
                //{
                //    GenerateHTMLAndSave(int.Parse(dr["POLICY_ID"].ToString()), int.Parse(dr["POLICY_VERSION_ID"].ToString()), int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(dr["INSTALLMENT_NO"].ToString()), userid, int.Parse(dr["ROW_ID"].ToString()), bankCode);
                //}
                if (!(dr["BOLETO_HTML"].ToString() != ""))
                {
                    GenerateHTMLAndSave(int.Parse(dr["POLICY_ID"].ToString()), int.Parse(dr["POLICY_VERSION_ID"].ToString()), int.Parse(dr["CUSTOMER_ID"].ToString()), int.Parse(dr["INSTALLMENT_NO"].ToString()), userid, int.Parse(dr["ROW_ID"].ToString()), bankCode, int.Parse(dr["CO_APPLICANT_ID"].ToString()));
                }
            }

           
        }

    }
}