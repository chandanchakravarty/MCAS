using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlBoleto;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Text;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;

namespace Cms.Policies.Aspx
{
    public partial class PolicyBoleto : Cms.CmsWeb.cmsbase
    {
        #region "Varriables"

        //private DateTime maturity;

        int _CustomerID = 0;
        //String _PolicyNo = "";
        int _PolicyID = 0;
        int _PolicyVersionID = 0;
        int _InstallmentNO = 0;
        int _CO_APPLICANT_ID = 0;
        int _InstallmentID;
        String _GENERATE_ALL_INSTALLMENT = "";
        int userid = 0;
        int _FLAG;
        //private short bankCode;
        String _CURRENCY_SYMBOL = "";
        #endregion "Varriables"


        ClsBoleto ObjClsBoleto; //= new ClsBoleto(objWrapper);

        protected void Page_Load(object sender, EventArgs e)
        {
            GetQueryStringValues();
            DataWrapper objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

            ObjClsBoleto = new ClsBoleto(objWrapper);
            if (!IsPostBack)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());

                try
                {
                    PrintBoleto.Text = ClsMessages.FetchGeneralMessage("1927");
                    ObjClsBoleto.CurrencySymbol = Cms.BusinessLayer.BlCommon.ClsCommon.GetPolicyCurrencySymbol(""); //_CURRENCY_SYMBOL;
                    String HTMLText = ObjClsBoleto.RetunHTMLforBoleto(_GENERATE_ALL_INSTALLMENT, _CustomerID, _PolicyID, _PolicyVersionID, _InstallmentID, _InstallmentNO, userid,_CO_APPLICANT_ID);
                    Panel1.Controls.Clear();
                    LiteralControl includet = new LiteralControl(HTMLText);
                    Panel1.Controls.Add(includet);
                    //LinkButton1.Visible = true;
                    if (HTMLText.ToString() == "")
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        LblMsg.Visible = true;
                        LblMsg.Text = ClsMessages.FetchGeneralMessage("1278");
                    }
                    else
                    {
                        PrintBoleto.Visible = true;
                        LblMsg.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    LblMsg.Visible = true;
                    LblMsg.Text = ex.Message.ToString();
                }
            }

        }

        public String CurrencySymbol
        {
            get { return _CURRENCY_SYMBOL; }
            set { _CURRENCY_SYMBOL = value; }
        }

        #region "Generating boleto based on query string values"

        // When user gives command to generated Boleto from EBix-Advantage Project
        //These code will execute

        private void GetQueryStringValues()
        {
            userid = int.Parse(GetUserId());

            if (Request.QueryString["CUSTOMER_ID"] != null)
                _CustomerID = int.Parse(Request.QueryString["CUSTOMER_ID"].ToString());
            
            if (Request.QueryString["POLICY_ID"] != null)
                _PolicyID = int.Parse(Request.QueryString["POLICY_ID"].ToString());
            
            if (Request.QueryString["POLICY_VERSION_ID"] != null)
                _PolicyVersionID = int.Parse(Request.QueryString["POLICY_VERSION_ID"].ToString());

            if (Request.QueryString["INSTALLMENT_NO"] != null)
                _InstallmentNO = int.Parse(Request.QueryString["INSTALLMENT_NO"].ToString());
            
            if (Request.QueryString["GENERATE_ALL_INSTALLMENT"] != null)
                _GENERATE_ALL_INSTALLMENT = Request.QueryString["GENERATE_ALL_INSTALLMENT"].ToString();

            if (Request.QueryString["CO_APPLICANT_ID"] != null && Request.QueryString["CO_APPLICANT_ID"].ToString() != "" && Request.QueryString["CO_APPLICANT_ID"].ToString()!="_1")
                _CO_APPLICANT_ID =int.Parse(Request.QueryString["CO_APPLICANT_ID"].ToString());
            else
                _CO_APPLICANT_ID = 0;

            if (_GENERATE_ALL_INSTALLMENT == "ALL")
            {
                _FLAG = 2;
                //Generate all installment Boleto
            }
            else
            {
                _FLAG = 1;
                //Generate Boleto for a installment
            }

            // bankCode = short.Parse(Request.QueryString["BANK_CODE"].ToString());
        }

        #endregion "Generating boleto based on query string values"


    }
}

