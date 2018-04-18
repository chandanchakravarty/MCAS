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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using System.IO;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlProcess;

namespace Cms.Claims.Aspx
{
    public partial class CededCOILetterLink : Cms.Claims.ClaimBase
    {
        public string Claim_ID;
        public string Activity_ID;
        public string POLICY_ID;
        public string POLICY_VERSION_ID;
        public string CUSTOMER_ID;
        public int PolicyID;
        public int PolicyVersionID;
        public int CustomerID;
        public int UserID;
        public int ClaimID;
        public int ActivityID;
        public string PROCESS_TYPE;
        public string val;
        public string function;
        public string PRODUCT_NUMBER;
        public string process_type;
        public string CLAIM_STATUS;
        public string ACTION_ON_PAYMENT;
        public string OFFICIAL_CLAIM_NUMBER;
        public string CLAIM_STATUS_UNDER;
        public int ACTIVITY_ID_FNOL;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.CededCOILetterLink", System.Reflection.Assembly.GetExecutingAssembly());
            if (!Page.IsPostBack)
            {
                Fetch_Value();
                Link_Hide();
                Link_Detail();
                SetCaption();             
            }
        }
        protected void Fetch_Value()
        {
            Claim_ID = Request.QueryString["Claim_ID"].ToString();
            Activity_ID = Request.QueryString["Activity_ID"].ToString();
            POLICY_ID = Request.QueryString["POLICY_ID"].ToString();
            POLICY_VERSION_ID = Request.QueryString["POLICY_VERSION_ID"].ToString();
            CUSTOMER_ID = Request.QueryString["CUSTOMER_ID"].ToString();
            PolicyID = int.Parse(POLICY_ID);
            PolicyVersionID = int.Parse(POLICY_VERSION_ID);
            CustomerID = int.Parse(CUSTOMER_ID);
            UserID = int.Parse(GetUserId());
            ClaimID = int.Parse(Claim_ID);
            if (Activity_ID != "")
            {
                ActivityID = int.Parse(Activity_ID);
            }
            else
            {
                ActivityID = 1;
            }
            ClsProductPdfXml obj = new ClsProductPdfXml();
            DataSet dsTemp = obj.fetch_CededClaimLetter(ClaimID, ActivityID, process_type);

            if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                OFFICIAL_CLAIM_NUMBER = dsTemp.Tables[0].Rows[0]["OFFICIAL_CLAIM_NUMBER"].ToString();
            }
            if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                ACTION_ON_PAYMENT = dsTemp.Tables[0].Rows[0]["ACTION_ON_PAYMENT"].ToString();
            }
            if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                CLAIM_STATUS = dsTemp.Tables[0].Rows[0]["CLAIM_STATUS"].ToString();
            }
            if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                CLAIM_STATUS_UNDER = dsTemp.Tables[0].Rows[0]["CLAIM_STATUS_UNDER"].ToString();
            }
            if(dsTemp.Tables.Count > 0 && dsTemp.Tables[2].Rows.Count > 0)
            {
                ACTIVITY_ID_FNOL =int.Parse(dsTemp.Tables[2].Rows[0]["ACTIVITY_ID"].ToString());
            }
            if (dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dsTemp.Tables[0];
                PRODUCT_NUMBER = dsTemp.Tables[0].Rows[0]["SUSEP_LOB_CODE"].ToString();
            }
         }

        protected void Link_Detail()
        {
            Fetch_Value();
           
            
            if (PRODUCT_NUMBER == "0173" || PRODUCT_NUMBER == "1163") // For Product --Global of Bank(0173) and  Rural Lien(1163)
                     {
                         if (((ACTION_ON_PAYMENT == "165" || ACTION_ON_PAYMENT == "168") &&  ACTION_ON_PAYMENT!="167")||(OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") && (CLAIM_STATUS_UNDER != "14933"))// (165-Reserve Creation) (168-Re-Open Reserve) //(ACTION_ON_PAYMENT == "165" || ACTION_ON_PAYMENT == "168" || ACTIVITY_ID_FNOL != 0 || ActivityID == 1) && 
                         {
                             tr19.Visible = true;
                         }
                         if ((ACTION_ON_PAYMENT == "165" || ACTION_ON_PAYMENT == "166" || ACTION_ON_PAYMENT == "180" || ACTION_ON_PAYMENT == "181" || ActivityID == 1) && ((OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") || (CLAIM_STATUS_UNDER != "14933"))) //(166-Change Reserve) (180-Payment – Partial) (181-Payment – Full)
                         {
                             tr25.Visible = true;
                             //tr25_2.Visible = true;
                         }
                         if (((CLAIM_STATUS == "11740" || CLAIM_STATUS == "11745") && ACTION_ON_PAYMENT == "167") && (CLAIM_STATUS_UNDER == "14933") && (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "")) // for 11740 & 11745 -- Close
                         {
                             tr31.Visible = true;
                         }
                         
                     }
            if (PRODUCT_NUMBER == "0977" || PRODUCT_NUMBER == "0982" || PRODUCT_NUMBER == "0993") // For Product -- Mortgage(0977) and  Group Personal Accident for Passenger(0982) Group Life(0993)
                   {
                       if ((ACTION_ON_PAYMENT == "165" &&  ACTION_ON_PAYMENT!="167")  || (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") && (CLAIM_STATUS_UNDER != "14933")) //(ACTION_ON_PAYMENT == "165" || ACTIVITY_ID_FNOL != 0 || ActivityID == 1) && 
                       {
                           tr20.Visible = true;
                       }
                       if ((ACTION_ON_PAYMENT == "165" || ACTION_ON_PAYMENT == "166" || ACTION_ON_PAYMENT == "180" || ACTION_ON_PAYMENT == "181" || ActivityID == 1) && ((OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") || (CLAIM_STATUS_UNDER != "14933")))
                       {
                           tr30.Visible = true;
                       }
                       if (((CLAIM_STATUS == "11740" || CLAIM_STATUS == "11745") && ACTION_ON_PAYMENT == "167") && (CLAIM_STATUS_UNDER == "14933") && (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != ""))
                       {
                           tr32.Visible = true;
                       }
                   }
            if (PRODUCT_NUMBER == "0114" || PRODUCT_NUMBER == "0115" || PRODUCT_NUMBER == "0116" || PRODUCT_NUMBER == "0118" || PRODUCT_NUMBER == "0176" || PRODUCT_NUMBER == "0171" || PRODUCT_NUMBER == "0196") // For Product -- Homeowners(0114)  Robbery(0115)  Comprehensive Condominium(0116)  Comprehensive Company(0118)  Diversified Risks(0171)   All Risks and Named Perils(0196)
                   {
                       if ((ACTION_ON_PAYMENT == "165" &&  ACTION_ON_PAYMENT!="167") || (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") && (CLAIM_STATUS_UNDER != "14933"))  //(ACTION_ON_PAYMENT == "165" || ACTIVITY_ID_FNOL != 0 || ActivityID == 1) && 
                       {
                           tr21.Visible = true;
                       }
                       if ((ACTION_ON_PAYMENT == "165" || ACTION_ON_PAYMENT == "166" || ACTION_ON_PAYMENT == "180" || ACTION_ON_PAYMENT == "181" ||  ActivityID == 1) && ((OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") || (CLAIM_STATUS_UNDER != "14933")))
                       {
                           tr26.Visible = true;
                       }
                       if (((CLAIM_STATUS == "11740" || CLAIM_STATUS == "11745") && ACTION_ON_PAYMENT == "167") && (CLAIM_STATUS_UNDER == "14933") && (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != ""))
                       {
                           tr33.Visible = true;
                       }
                   }
            if (PRODUCT_NUMBER == "0523" || PRODUCT_NUMBER == "0531" || PRODUCT_NUMBER == "0553" || PRODUCT_NUMBER == "0654") // For Product -- Civil Liability Transportation(0523)   Motor(0531)   Facultative Liability(0553)   Cargo Transportation Civil Liability(0654)
                   {
                       if ((ACTION_ON_PAYMENT == "165" &&  ACTION_ON_PAYMENT!="167") || (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") && (CLAIM_STATUS_UNDER != "14933")) //(ACTION_ON_PAYMENT == "165" || ACTIVITY_ID_FNOL != 0 || ActivityID == 1) &&
                       {
                           tr22.Visible = true;
                       }
                       if ((ACTION_ON_PAYMENT == "165" || ACTION_ON_PAYMENT == "166" || ACTION_ON_PAYMENT == "180" || ACTION_ON_PAYMENT == "181" || ActivityID == 1) && ((OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") || (CLAIM_STATUS_UNDER != "14933")))
                       {
                           tr27.Visible = true;
                       }
                       if (((CLAIM_STATUS == "11740" || CLAIM_STATUS == "11745") && ACTION_ON_PAYMENT == "167") && (CLAIM_STATUS_UNDER == "14933") && (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != ""))
                       {
                           tr34.Visible = true;
                       }
                   }
            if (PRODUCT_NUMBER == "0621" || PRODUCT_NUMBER == "0622") // For Product -- National Cargo Transport(0621) International Cargo Transport(0622)
                   {
                       if ((ACTION_ON_PAYMENT == "165" &&  ACTION_ON_PAYMENT!="167") || (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") && (CLAIM_STATUS_UNDER != "14933")) //(ACTION_ON_PAYMENT == "165" || ACTIVITY_ID_FNOL != 0 || ActivityID == 1) &&
                       {
                           tr23.Visible = true;
                       }
                       if ((ACTION_ON_PAYMENT == "165" || ACTION_ON_PAYMENT == "166" || ACTION_ON_PAYMENT == "180" || ACTION_ON_PAYMENT == "181" || ActivityID == 1) && ((OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") || (CLAIM_STATUS_UNDER != "14933")))
                       {
                           tr28.Visible = true;
                       }
                       if (((CLAIM_STATUS == "11740" || CLAIM_STATUS == "11745") && ACTION_ON_PAYMENT == "167") && (CLAIM_STATUS_UNDER == "14933") && (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != ""))
                       {
                           tr35.Visible = true;
                       }
                   }
            if (PRODUCT_NUMBER == "0433") // For Product -- Maritime(0433)
                   {
                       if ((ACTION_ON_PAYMENT == "165" &&  ACTION_ON_PAYMENT!="167") || (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") && (CLAIM_STATUS_UNDER != "14933")) //(ACTION_ON_PAYMENT == "165" || ACTIVITY_ID_FNOL != 0 || ActivityID == 1) && 
                       {
                           tr24.Visible = true;
                       }
                       if ((ACTION_ON_PAYMENT == "165" || ACTION_ON_PAYMENT == "166" || ACTION_ON_PAYMENT == "180" || ACTION_ON_PAYMENT == "181" || ActivityID == 1) && ((OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != "") || (CLAIM_STATUS_UNDER != "14933")))
                       {
                           tr29.Visible = true;
                       }
                       if (((CLAIM_STATUS == "11740" || CLAIM_STATUS == "11745") && ACTION_ON_PAYMENT == "167") && (CLAIM_STATUS_UNDER == "14933") && (OFFICIAL_CLAIM_NUMBER != null && OFFICIAL_CLAIM_NUMBER != ""))
                       {
                           tr36.Visible = true;
                       }
                   }
                }

           
     

        protected void Link_Hide()
        {
            tr19.Visible = false;
            tr20.Visible = false;
            tr21.Visible = false;
            tr22.Visible = false;
            tr23.Visible = false;
            tr24.Visible = false;
            tr25.Visible = false;
            tr25_2.Visible = false;
            tr26.Visible = false;
            tr27.Visible = false;
            tr28.Visible = false;
            tr29.Visible = false;
            tr30.Visible = false;
            tr31.Visible = false;
            tr32.Visible = false;
            tr33.Visible = false;
            tr34.Visible = false;
            tr35.Visible = false;
            tr36.Visible = false;
        }

        protected void SetCaption()
        {
            ceeded19.Text = "Aviso de Sinistro - Cosseguro Cedido";//"D-019 Letter for Ceded COI FNOL_Prod 0173, 1163";
            ceeded20.Text = "Aviso de Sinistro - Cosseguro Cedido";// "D-020 Letter for Ceded COI FNOL_Prod 0977, 0982, 0993";
            ceeded21.Text = "Aviso de Sinistro - Cosseguro Cedido";// "D-021 Letter for Ceded COI FNOL_Prod 0114, 0115, 0116, 0118, 0176, 0171, 0196";
            ceeded22.Text = "Aviso de Sinistro - Cosseguro Cedido";// "D-022 Letter for Ceded COI FNOL_Prod 0523, 0531, 0553, 0654";
            ceeded23.Text = "Aviso de Sinistro - Cosseguro Cedido";// "D-023 Letter for Ceded COI FNOL_Prod 0621, 0622";
            ceeded24.Text = "Aviso de Sinistro - Cosseguro Cedido ";// "D-024 Letter for Ceded COI FNOL_Prod 0433";
            ceeded25.Text = "Cobrança de Sinistro - Cosseguro Cedido";// "D-025 Letter for Ceded COI FNOL_Prod 0173, 1163 (Payments)";
            ceeded25_2.Text ="Cobrança de Sinistro - Cosseguro Cedido";
            ceeded26.Text = "Cobrança de Sinistro - Cosseguro Cedido";// "D-026 Letter for Ceded COI FNOL_Prod 0114, 0115, 0116, 0118, 0176, 0171, 0196 (Payments)";
            ceeded27.Text = "Cobrança de Sinistro - Cosseguro Cedido";// "D-027 Letter for Ceded COI FNOL_Prod 0523, 0531, 0553, 0654 (Payments)";
            ceeded28.Text = "Cobrança de Sinistro - Cosseguro Cedido";// "D-028 Letter for Ceded COI FNOL_Prod 0621, 0622 (Payments)";
            ceeded29.Text = "Cobrança de Sinistro - Cosseguro Cedido";// "D-029 Letter for Ceded COI FNOL_Prod 0433 (Payments)";
            ceeded30.Text = "Cobrança de Sinistro - Cosseguro Cedido";// "D-030 Letter for Ceded COI FNOL_Prod 0977, 0982, 0993 (Payments)";
            ceeded31.Text = "Encerramento de Sinistro - Cosseguro Cedido ";// "D-031 Letter for Ceded COI FNOL_Prod 0173, 1163 (Closure Without Payment)";
            ceeded32.Text = "Encerramento de Sinistro - Cosseguro Cedido ";// "D-032 Letter for Ceded COI FNOL_Prod 0977, 0982, 0993 (Closure Without Payment)";
            ceeded33.Text = "Encerramento de Sinistro - Cosseguro Cedido ";// "D-033 Letter for Ceded COI FNOL_Prod 0114, 0115, 0116, 0118, 0176, 0171, 0196 (Closure Without Payments)";
            ceeded34.Text = "Encerramento de Sinistro - Cosseguro Cedido ";// "D-034 Letter for Ceded COI FNOL_Prod 0523, 0531, 0553, 0654 (Closure Without Payment)";
            ceeded35.Text = "Encerramento de Sinistro - Cosseguro Cedido ";// "D-035 Letter for Ceded COI FNOL_Prod 0621, 0622 (Closure Without Payment)";
            ceeded36.Text = "Encerramento de Sinistro - Cosseguro Cedido ";// "D-036 Letter for Ceded COI FNOL_Prod 0433 (Closure Without Payment)";
       } 

        protected void ceeded19_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER";
            ObjPdfXML.generateCededCOILetter(ClaimID, ACTIVITY_ID_FNOL, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded19.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + ACTIVITY_ID_FNOL + "&PROCESS_TYPE=" + PROCESS_TYPE;// +"&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CUSTOMER_ID=" + CUSTOMER_ID;
            this.ceeded19.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded20_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_20";
            ObjPdfXML.generateCededCOILetter(ClaimID, 1, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded20.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + 1 + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded20.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded21_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_21";
            ObjPdfXML.generateCededCOILetter(ClaimID, 1, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded21.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + 1 + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded21.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded22_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_22";
            ObjPdfXML.generateCededCOILetter(ClaimID, 1, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded22.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + 1 + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded22.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded23_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_23";
            ObjPdfXML.generateCededCOILetter(ClaimID, 1, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded23.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + 1 + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded23.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded24_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_24";
            ObjPdfXML.generateCededCOILetter(ClaimID, 1, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded24.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + 1 + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded24.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded25_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_25";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded25.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded25.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded25_2_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_25_2";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded25_2.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded25_2.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded26_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_26";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded26.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded26.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded27_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_27";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded27.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded27.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded28_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_28";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded28.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded28.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded29_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_29";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded29.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded29.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded30_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_30";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded30.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded30.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded31_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_31";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded31.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded31.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded32_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_32";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded32.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded32.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded33_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_33";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded33.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded33.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded34_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_34";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded34.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded34.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded35_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_35";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded35.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded35.ForeColor = System.Drawing.Color.Red;
        }

        protected void ceeded36_Click(object sender, EventArgs e)
        {
            Fetch_Value();
            ClsProductPdfXml ObjPdfXML = new ClsProductPdfXml();
            PROCESS_TYPE = "CLM_LETTER_36";
            ObjPdfXML.generateCededCOILetter(ClaimID, ActivityID, CustomerID, PolicyID, PolicyVersionID, UserID, PROCESS_TYPE);
            ceeded36.PostBackUrl = "/Cms/Claims/Aspx/CededCOILetter.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&PROCESS_TYPE=" + PROCESS_TYPE;
            this.ceeded36.ForeColor = System.Drawing.Color.Red;
        }
    }
}
