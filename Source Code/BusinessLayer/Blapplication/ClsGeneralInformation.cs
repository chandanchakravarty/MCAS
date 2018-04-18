/******************************************************************************************
<Author					: -   Nidhi
<Start Date				: -	4/26/2005 3:13:20 PM
<End Date				: -	
<Description			: - 	This screen will display/save the general information related to application
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: Nidhi
<Modified By			: 7/19/2005 11:54:37 AM 
<Purpose				: Added method -GeneratePolicyNumber() to generate policy number 

<Modified Date			: ashwani 
<Modified By			: 10/05/2005 11:54:37 AM 
<Purpose				: Added method -GetInputXML() to get the inputXML


********************************************************************************************/


using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Application;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.Model;
using Cms.Model.Policy;
using System.Collections;
using Cms.Model.Diary;
using Cms.Model.Support;
using System.Collections.Generic;
using Model.Policy;
using System.Threading;


namespace Cms.BusinessLayer.BlApplication
{
    /// <summary>
    /// Summary description for ClsGeneralInformation.
    /// </summary>
    public class ClsGeneralInformation : Cms.BusinessLayer.BlCommon.ClsCommon
    {
        public ClsGeneralInformation()
        {
            base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
        }

        private const string APP_LIST = "APP_LIST";
        private const string ACTIVATE_DEACTIVATE_PROC = "Proc_ActivateDeactivateApplication";
        private const string LOB_HOME = "1";
        private const string LOB_PRIVATE_PASSENGER = "2";
        private const string LOB_MOTORCYCLE = "3";
        private const string LOB_WATERCRAFT = "4";
        private const string LOB_UMBRELLA = "5";
        private const string LOB_RENTAL_DWELLING = "6";
        private const string LOB_GENERAL_LIABILITY = "7";
        private const string LOB_AVIATION = "8";
        private const string LOB_MOTOR = "38";
        private const int UMBRELLA = 3;
        private const string AGENCY_NAME = "Wolverine";
        public static string CalledFromSubmitAnywayButton = "SubmitAnywayButton";
        public static string CalledFromSubmitButton = "SubmitButton";
        public static string CalledFromSubmitAnywayImage = "SubmitAnywayImage";
        public static string CalledFromSubmitImage = "SubmitImage";
        public static string CalledFromSubmitAnyway = "ANYWAY";
        #region Properties

        private int customerID = 0;
        private int appID = 0;
        private int appVersionID = 0;

        public int pCustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }


        public int pAppID
        {
            get
            {
                return appID;
            }
            set
            {
                appID = value;
            }
        }


        public int pAppVersionID
        {
            get
            {
                return appVersionID;
            }
            set
            {
                appVersionID = value;
            }
        }


        #endregion

        private bool boolTransactionRequired = true;
        public bool TransactionRequired
        {
            get
            {
                return boolTransactionRequired;
            }
            set
            {
                boolTransactionRequired = value;
            }
        }


        #region UTILITY FUNCTIONS

        //Function added by Charles on 18-Dec-09 for Itrack 6681
        public static string FetchEffectiveReplacementCostForHome(string strPolicyLookupType, string strAppEffDate, string strStateId)
        {
            string strPolicyType = "", strProductName = "", strUrl = "", strReplacementCost = "";
            DateTime dtAppEffDate = DateTime.Parse(strAppEffDate);
            strReplacementCost = "100000";

            if (strStateId == "22")
                strStateId = "MICHIGAN";
            else if (strStateId == "14")
                strStateId = "INDIANA";
            //Policy Type
            if (strPolicyLookupType == "11149" || strPolicyLookupType == "11192" || strPolicyLookupType == "11400" || strPolicyLookupType == "11401" || strPolicyLookupType == "11402")
            {
                strPolicyType = "REPLACE";
            }
            else if (strPolicyLookupType == "11193" || strPolicyLookupType == "11194" || strPolicyLookupType == "11403" || strPolicyLookupType == "11404")
            {
                strPolicyType = "REPAIR";
            }
            else if (strPolicyLookupType == "11195" || strPolicyLookupType == "11405")
            {
                strPolicyType = "TENANT";
            }
            else if (strPolicyLookupType == "11196" || strPolicyLookupType == "11406")
            {
                strPolicyType = "UNIT";
            }
            else if (strPolicyLookupType == "11409" || strPolicyLookupType == "11410")
            {
                strPolicyType = "PREMIER";
            }
            //Product Name
            if (strPolicyLookupType == "11148" || strPolicyLookupType == "11194" || strPolicyLookupType == "11400" || strPolicyLookupType == "11404" || strPolicyLookupType == "11409")
            {
                strProductName = "HO-3";
            }
            else if (strPolicyLookupType == "11149" || strPolicyLookupType == "11401" || strPolicyLookupType == "11410")
            {
                strProductName = "HO-5";
            }
            else if (strPolicyLookupType == "11192" || strPolicyLookupType == "11193" || strPolicyLookupType == "11402" || strPolicyLookupType == "11403")
            {
                strProductName = "HO-2";
            }
            else if (strPolicyLookupType == "11195" || strPolicyLookupType == "11405")
            {
                strProductName = "HO-4";
            }
            else if (strPolicyLookupType == "11196" || strPolicyLookupType == "11406")
            {
                strProductName = "HO-6";
            }

            try
            {
                strUrl = System.Configuration.ConfigurationManager.AppSettings.Get("CmsWebUrl").ToString() + "QuickQuote/HOME_DEFAULT_VALUES.XML";
                XmlDocument doc = new XmlDocument();
                doc.Load(strUrl);

                XmlNodeList nodeList = null;
                nodeList = doc.SelectNodes("quickQuote/basicpolicy/COV_A_LIMIT_RULE/*");
                foreach (XmlNode node in nodeList)
                {
                    if (node.Attributes["STATE"].Value.ToString() == strStateId.ToUpper())
                    {
                        DateTime tmpDATE_START = DateTime.Parse(node.Attributes["DATE_START"].Value.ToString());
                        DateTime tmpEND = DateTime.Parse(node.Attributes["END"].Value.ToString());

                        if (dtAppEffDate >= tmpDATE_START && dtAppEffDate <= tmpEND)
                        {
                            for (int i = 0; i < node.ChildNodes.Count; i++)
                            {
                                if (node.ChildNodes[i].Attributes["PRODUCTNAME"].Value.ToString() == strProductName && node.ChildNodes[i].Attributes["POLICYTYPE"].Value.ToString() == strPolicyType)
                                {
                                    strReplacementCost = node.ChildNodes[i].Attributes["MINIMUM_LIMIT"].Value.ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return strReplacementCost;

        }

        public string FetchPolicyWatercraftInputXML(int customerID, int POLICYID, int POLICYVersionID)
        {
            return FetchPolicyWatercraftInputXML(customerID, POLICYID, POLICYVersionID, null);
        }

        public string FetchPolicyWatercraftInputXML(int customerID, int policyID, int policyVersionID, DataWrapper objDataWrapper)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForWatercraft_AppComponent = "Proc_GetPolicyRatingInformationForWatercraft_PolicyComponent";
            string strStoredProcForWatercraft_SportEquipmentsComponent = "Proc_GetPolicyRatingInformationForWatercraft_SportEquipmentsComponent";
            string strStoredProcForWatercraft_BoatComponent = "Proc_GetPolicyRatingInformationForWatercraft_BoatComponent";
            string strStoredProcForWatercraft_BoatCovgComponent = "Proc_GetPolicyRatingInformationForWatercraft_CoverageComponent";
            string strStoredProcForWatercraft_OperatorComponent = "Proc_GetPolicyRatingInformationForWatercraft_OperatorComponent";
            string strStoredProcForWatercraft_ViolationsComponent = "Proc_GetPolicyRatingInformationForWatercraft_Violations";
            string strStoredProcForWatercraft_TrailerComponent = "Proc_GetPolicyRatingInformationForWatercraft_TrailerComponent";
            string strStoredProcForWatercraft_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo_Pol";

            string strReturnXML = "";

            string strpolicy;
            string strMVRPoints = "", strVioId = "", strviolationXML = "";
            int temptotalpoints = 0, strLenght = 0;
            DataSet dsTempXML;
            XmlNode nodetempviolation;//nodetempviolationId,
            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@policyID", policyID);
                objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_AppComponent);
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml(); --Commented on 18 April 2008
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                returnString.Append(strReturnXML);

                /*Assigning the policy node  : PK*/
                strpolicy = strReturnXML;

                /*----------Primary Applicant Info--------*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", policyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", policyVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml();
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*----------Primary Applicant Info--------*/


                // get the equipment info against the customerID, appID, appVersionID
                // get the equipment details for each equipment	
                returnString.Append("<SCHEDULEDMISCSPORTS>");
                string strEquipIDs = clsWatercraftInformation.GetEquipmentID_Pol(customerID, policyID, policyVersionID);
                if (strEquipIDs != "-1")
                {
                    string[] arrEquipID = new string[0];
                    arrEquipID = strEquipIDs.Split('^');
                    // Run a loop to get the inputXML for each equipment ID
                    for (int iCounterForEquip = 0; iCounterForEquip < arrEquipID.Length; iCounterForEquip++)
                    {
                        returnString.Append("<SCH_MISC ID='" + arrEquipID[iCounterForEquip] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@policyID", policyID);
                        objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                        objDataWrapper.AddParameter("@EQUIP_ID", arrEquipID[iCounterForEquip]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_SportEquipmentsComponent);
                        objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        returnString.Append(strReturnXML);
                        returnString.Append("</SCH_MISC>");
                    }
                }
                returnString.Append("</SCHEDULEDMISCSPORTS>");
                returnString.Append("</POLICY>");
                // Get the Boat Ids against the customerID, appID, appVersionID
                //get the boat details for each boat
                returnString.Append("<BOATS>");
                string strBoatIDs = ClsRatingAndUnderwritingRules.GetBoatIDs_Pol(customerID, policyID, policyVersionID);
                //string strBoatIDs = clsWatercraftInformation.GetBoatIDs(customerID,policyID,policyVersionID);
                if (strBoatIDs != "-1")
                {
                    string[] arrBoatID = new string[0];
                    arrBoatID = strBoatIDs.Split('^');

                    // Run a loop to get the inputXML for each BoatID
                    for (int iCounter = 0; iCounter < arrBoatID.Length; iCounter++)
                    {
                        returnString.Append("<BOAT ID='" + arrBoatID[iCounter] + "' BOATTYPE='" + "B" + "'>");	//Appended the Attribute BOATTYPE for RISK ID//											
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@policyID", policyID);
                        objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                        objDataWrapper.AddParameter("@BOATID", arrBoatID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_BoatComponent);
                        objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        //Add node for each Boat for count
                        int rowID = iCounter + 1;
                        string strBoatRowIDNode = "<BOATROWID>" + rowID.ToString() + "</BOATROWID>";
                        returnString.Append(strBoatRowIDNode);
                        returnString.Append(strReturnXML);
                        //Get coverages
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@policy_ID", policyID);
                        objDataWrapper.AddParameter("@policy_VERSION_ID", policyVersionID);
                        objDataWrapper.AddParameter("@BOAT_ID", arrBoatID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_BoatCovgComponent);
                        objDataWrapper.ClearParameteres();

                        if (dsTempXML.Tables[0] != null && dsTempXML.Tables[0].Rows.Count > 0)
                        {
                            strReturnXML = dsTempXML.Tables[0].Rows[0][0].ToString();
                            returnString.Append(strReturnXML);
                        }
                        returnString.Append("</BOAT>");
                    }
                }

                //Get The Trailer Information
                strBoatIDs = ClsRatingAndUnderwritingRules.GetPolTrailerDs_Pol(customerID, policyID, policyVersionID);
                //string strBoatIDs = clsWatercraftInformation.GetBoatIDs(customerID,policyID,policyVersionID);
                if (strBoatIDs != "-1")
                {
                    string[] arrBoatID = new string[0];
                    arrBoatID = strBoatIDs.Split('^');

                    // Run a loop to get the inputXML for each BoatID
                    for (int iCounter = 0; iCounter < arrBoatID.Length; iCounter++)
                    {
                        returnString.Append("<BOAT ID='" + arrBoatID[iCounter] + "' BOATTYPE='" + "T" + "'>");	//Appended the Attribute BOATTYPE for RISK ID//					
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@policyID", policyID);
                        objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                        objDataWrapper.AddParameter("@TRAILER_ID", arrBoatID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_TrailerComponent);
                        objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        //Add node for each Boat for count
                        int rowID = iCounter + 1;
                        string strBoatRowIDNode = "<BOATROWID>" + rowID.ToString() + "</BOATROWID>";
                        returnString.Append(strBoatRowIDNode);
                        returnString.Append(strReturnXML);
                        returnString.Append("</BOAT>");
                    }
                }
                returnString.Append("</BOATS>");
                //get the operator details for each operator
                returnString.Append("<OPERATORS>");
                string driverIDs = ClsRatingAndUnderwritingRules.GetRuleWCDriverIDs_Pol(customerID, policyID, policyVersionID);
                if (driverIDs != "-1")
                {
                    string[] driverID = new string[0];
                    driverID = driverIDs.Split('^');
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        returnString.Append("<OPERATOR ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@policyID", policyID);
                        objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_OperatorComponent);
                        objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        returnString.Append(strReturnXML);
                        //for each driver also get the violation detials (if any)
                        //returnString.Append("<VIOLATIONS>");
                        string violationIDs = ClsDriverDetail.GetWCViolationIDs_Pol(customerID, policyID, policyVersionID, int.Parse(driverID[iCounter].ToString()));

                        //int sumofViolationPoints = 0, sumofAccidentPoints = 0, sumofMvrPoints = 0; //Set Violation Variables
                        int violationPoints = 0, accidentPoints = 0, MvrPoints = 0;
                        strviolationXML = "";
                        StringBuilder strViolationNodes = new StringBuilder(); //For Driver Violations .

                        if (violationIDs != "-1")
                        {
                            string[] violationID = new string[0];
                            violationID = violationIDs.Split('^');
                            strViolationNodes.Append("<VIOLATIONS>");
                            //strViolationNodes.Remove(0,strViolationNodes.Length);

                            // Run a loop to get the inputXML for each vehicleID
                            for (int iCounterForViolations = 0; iCounterForViolations < violationID.Length; iCounterForViolations++)
                            {
                                //Violation Append
                                strViolationNodes.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>"); //for just violation nodes
                                //
                                //returnString.Append("<VIOLATION ID='"+ violationID[iCounterForViolations]+"'>");
                                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                                objDataWrapper.AddParameter("@policyID", policyID);
                                objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                                objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                                //objDataWrapper.AddParameter("@VIOLATIONID",violationID[iCounterForViolations]);	
                                //Modified it to @POL_WATER_MVR_ID from @VIOLATIONID
                                objDataWrapper.AddParameter("@POL_WATER_MVR_ID", violationID[iCounterForViolations]);
                                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_ViolationsComponent);
                                objDataWrapper.ClearParameteres();
                                //strReturnXML = dsTempXML.GetXml();
                                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("<Table>", "");
                                strReturnXML = strReturnXML.Replace("</Table>", "");
                                //returnString.Append(strReturnXML);
                                //Appending the Violations 
                                strViolationNodes.Append(strReturnXML);
                                //End 
                                //returnString.Append("</VIOLATION>");

                                strViolationNodes.Append("</VIOLATION>");
                            }
                            strViolationNodes.Append("</VIOLATIONS>");
                            XmlDocument xmlDoc = new XmlDocument();
                            strviolationXML = strViolationNodes.ToString();
                            xmlDoc.LoadXml(strviolationXML);
                            XmlNode nodtempViolation = xmlDoc.SelectSingleNode("VIOLATIONS");
                            foreach (XmlNode nodchild in nodtempViolation.ChildNodes)
                            {
                                //nodetempviolationId = xmlDoc.SelectSingleNode("VIOLATIONS/VIOLATION/@ID");
                                strVioId = nodchild.Attributes["ID"].Value.ToString().Trim();
                                nodetempviolation = xmlDoc.SelectSingleNode("VIOLATIONS/VIOLATION[@ID='" + strVioId + "']/MVRPOINTS");
                                temptotalpoints += System.Convert.ToInt32(nodetempviolation.InnerText);
                            }
                        }
                        //////////////////////////
                        ///
                        ClsQuickQuote objQQ = new ClsQuickQuote();
                        DataTable dtMVR = new DataTable();
                        dtMVR = objQQ.GetMVRPointsForSurcharge(customerID, policyID, policyVersionID, int.Parse(driverID[iCounter]), "POL");
                        if (dtMVR != null && dtMVR.Rows.Count > 0)
                        {
                            MvrPoints = int.Parse(dtMVR.Rows[0]["MVR_POINTS"].ToString());
                            violationPoints = int.Parse(dtMVR.Rows[0]["SUM_MVR_POINTS"].ToString());
                            accidentPoints = int.Parse(dtMVR.Rows[0]["ACCIDENT_POINTS"].ToString());
                        }
                        strMVRPoints = "<MVR>" + MvrPoints.ToString() + "</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() + "</SUMOFACCIDENTPOINTS>";

                        strLenght = strviolationXML.Length;
                        strviolationXML = strviolationXML.Insert(strLenght, strMVRPoints);
                        returnString.Append(strviolationXML);
                        /////////////////////////
                        //Set the values of the variables Calling method to Calculate points:
                        /*string strLOBCode = "BOAT";
                            string strMVRPointsForSurchargeXML = "<ViolationsMVRPoints>" + strpolicy + "<violations>" + strViolationNodes.ToString() +  "</violations>" + "</ViolationsMVRPoints>";
                            ClsQuickQuote ClsQQobj = new ClsQuickQuote();
                            strMVRPoints = ClsQQobj.GetMVRPointsForSurcharge(strMVRPointsForSurchargeXML,strLOBCode);
                            //End 
                        }
                        else
                        {
                            //if No Violations are Selected //
                            strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>-1</SUMOFACCIDENTPOINTS></POINTS>";
                        }
                        returnString.Append("</VIOLATIONS>");

                        //Set the nodes of sum of violation n accident points for each driver
                        XmlDocument PointsDoc = new XmlDocument();
						
                        PointsDoc.LoadXml(strMVRPoints);
                        XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
                        if(PointNode!=null)
                        {
							
                            sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
                            sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                            sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                
                            returnString.Append("<MVR>");
                            returnString.Append(sumofMvrPoints);
                            returnString.Append("</MVR>");

                            returnString.Append("<SUMOFVIOLATIONPOINTS>");
                            returnString.Append(sumofViolationPoints);
                            returnString.Append("</SUMOFVIOLATIONPOINTS>");

                            returnString.Append("<SUMOFACCIDENTPOINTS>");
                            returnString.Append(sumofAccidentPoints);
                            returnString.Append("</SUMOFACCIDENTPOINTS>");
							
                        }*/
                        //END


                        returnString.Append("</OPERATOR>");
                        temptotalpoints = 0;
                    }
                }
                returnString.Append("</OPERATORS>");
                returnString.Append("</QUICKQUOTE>");
                returnString = returnString.Replace(",", "");
                returnString = returnString.Replace("\r\n", "");
                //*****Loading Documnet and Setting the VIOLATION POINTS at BOAT LEVEL : 21 April 2006
                //**Loading the Appended XML form Procedures and Appending and Setting the Violation NODES**//
                string strOutXml = "";
                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                int mvrPoints = 0, sumOFviolations = 0, sumOfAccidents = 0;
                foreach (XmlNode VehicleVioNode in returnXMLDoc.SelectNodes("//BOATS/*"))
                {
                    int VehID = int.Parse(VehicleVioNode.Attributes["ID"].Value.ToString().Trim());
                    mvrPoints = 0; sumOFviolations = 0; sumOfAccidents = 0;
                    foreach (XmlNode DriverVioNode in returnXMLDoc.SelectNodes("//OPERATORS/*"))
                    {
                        if (DriverVioNode.SelectSingleNode("BOATASSIGNEDASOPERATOR") != null)
                        {
                            if (Convert.ToInt32(DriverVioNode.SelectSingleNode("BOATASSIGNEDASOPERATOR").InnerText.ToString()) == VehID)
                            {
                                if (DriverVioNode.SelectSingleNode("VIOLATIONS") != null && DriverVioNode.SelectSingleNode("VIOLATIONS").InnerText != "")
                                {
                                    mvrPoints += int.Parse(DriverVioNode.SelectSingleNode("MVR").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("MVR").InnerText.ToString());
                                    sumOFviolations += int.Parse(DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                                    sumOfAccidents += int.Parse(DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                }
                            }
                        }
                    }

                    //**Creating New Nodes for VIOLATIONS (Watercraft)**//
                    XmlElement mvrNode = returnXMLDoc.CreateElement("MVR");
                    XmlElement vioNode = returnXMLDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                    XmlElement accidentNode = returnXMLDoc.CreateElement("SUMOFACCIDENTPOINTS");
                    //Set the text to node
                    XmlText mvrtext = returnXMLDoc.CreateTextNode(mvrPoints.ToString());
                    XmlText viotext = returnXMLDoc.CreateTextNode(sumOFviolations.ToString());
                    XmlText accidenttext = returnXMLDoc.CreateTextNode(sumOfAccidents.ToString());
                    //Append the nodes 
                    VehicleVioNode.AppendChild(mvrNode);
                    VehicleVioNode.AppendChild(vioNode);
                    VehicleVioNode.AppendChild(accidentNode);
                    //Save the value of the fields into the nodes
                    mvrNode.AppendChild(mvrtext);
                    vioNode.AppendChild(viotext);
                    accidentNode.AppendChild(accidenttext);

                    strOutXml = returnXMLDoc.OuterXml;

                }

                //END 
                return strOutXml.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        //end
        public static string GetLOBNameByID(int LOBID)
        {

            DataSet dsLOB = ClsPolicyNoSetup.GetLOBMasterXml(LOBID);

            string RetVal = "";
            if (dsLOB != null && dsLOB.Tables[0].Rows.Count > 0)
            {
                RetVal = (dsLOB.Tables[0].Rows[0]["LOB_DESC"] == null ? "" : dsLOB.Tables[0].Rows[0]["LOB_DESC"].ToString());
            }
            return RetVal;

        }



        #region Get The Class
        public string GetAllClassOnStateId()
        {
            int stateID = 0;
            return GetAllClassOnStateId(stateID);
        }
        public string GetAllClassOnStateId(int stateID)
        {
            try
            {

                DataSet dsClass = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                if (objDataWrapper.CommandParameters.Length > 0)
                    objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@STATE_ID", stateID, SqlDbType.Int);
                string strClass = "";
                dsClass = objDataWrapper.ExecuteDataSet("Proc_GetClassOnStateId");
                if (dsClass.Tables[0].Rows.Count > 0)
                {
                    //					int rowsCount = 0;
                    //					//rowsCount = int.Parse(dsClass.Tables[0].Rows.Count);
                    //					for (rowsCount = 0;rowsCount< int.Parse(dsClass.Tables[0].Rows.Count);rowsCount++)
                    //					{
                    //						string strClass=dsClass.Tables[0].Rows[0][0].ToString();
                    //					}

                    foreach (DataRow Row in dsClass.Tables[0].Rows)
                    {
                        strClass = strClass + Row[0].ToString();
                    }
                    return strClass;
                }
                else
                    return "";
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }
        #endregion

        public DataSet GetLOBBYSTATEID()
        {
            return GetLOBBYSTATEID(0);
        }
        public DataSet GetLOBBYSTATEID(int stateID)
        {
            try
            {

                DataSet dsLOB = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@STATE_ID", stateID, SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
                dsLOB = objDataWrapper.ExecuteDataSet("Proc_SELECTLOB");

                return dsLOB;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        public DataSet GetSubLOBBYSTATELOBID(int stateID, int lobID)
        {
            try
            {

                DataSet dsLOB = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@STATE_ID", stateID, SqlDbType.Int);
                objDataWrapper.AddParameter("@LOB_ID", lobID, SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
                dsLOB = objDataWrapper.ExecuteDataSet("Proc_GetLobAndSubLOBByState");

                return dsLOB;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        public DataSet GetLOBBYSTATEID_PI(int stateID)
        {
            try
            {

                DataSet dsLOB = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@STATE_ID", stateID, SqlDbType.Int);

                dsLOB = objDataWrapper.ExecuteDataSet("Proc_SELECTLOB_PI");

                return dsLOB;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        /// <summary>
        /// Get state id and name
        /// </summary>
        /// <param name="LOBID"></param>
        /// <returns></returns>
        /// 
        public static DataSet GetStateNameId(string lob_id)
        {
            string strStoredProc = "Proc_GetLobStateName";

            DataSet dsState = new DataSet();

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOB_ID", lob_id);

            dsState = objDataWrapper.ExecuteDataSet(strStoredProc);

            return dsState;
        }

        public static DataSet GetLOBTerms(int LOBID)
        {
            try
            {

                DataSet dsLOB = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@LOB_ID", LOBID, SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", BlCommon.ClsCommon.BL_LANG_ID, SqlDbType.Int);

                dsLOB = objDataWrapper.ExecuteDataSet("Proc_GetTermsValue");

                if (dsLOB != null && dsLOB.Tables[0].Rows.Count > 0)
                {
                    return dsLOB;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }

        }


        /// <summary>
        /// This is used to Generate XML for SUBLOBs and Department mapping and will called form Add Accounting Entity Page,
        ///  this function will stop post back of page to reterive value of other drop down on selected index change
        /// </summary>
        /// <returns></returns>
        public static DataSet FetchApplicationLobID(int CustomerID, int AppID, int AppVersionID)
        {
            DataSet dsTemp = new DataSet();

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMERID", CustomerID, SqlDbType.Int);
            objDataWrapper.AddParameter("@APPID", AppID, SqlDbType.Int);
            objDataWrapper.AddParameter("@APPVERSIONID", AppVersionID, SqlDbType.Int);

            dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetApplicationLOBID");

            return dsTemp;

        }

        public static DataSet FetchApplication(int CustomerID, int AppID, int AppVersionID)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMERID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APPID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APPVERSIONID", AppVersionID, SqlDbType.Int);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppInformation");

                return dsTemp;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }

        public static string FetchApplicationXML(int CustomerID, int AppID, int AppVersionID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = FetchApplication(CustomerID, AppID, AppVersionID);
                return ClsCommon.GetXMLEncoded(dsTemp.Tables[0]);
            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }

        public static string GenerateApplicationNumber(int LOBID, int agencyID)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objDataWrapper.AddParameter("@APP_LOB", LOBID, SqlDbType.Int);
                objDataWrapper.AddParameter("@AGENCY_ID", agencyID, SqlDbType.Int);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GenerateAppNumber");
                string retVal = "";

                if (dsTemp != null)
                    retVal = dsTemp.Tables[0].Rows[0][0].ToString();
                return retVal;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }

        /// <summary>
        /// Generates Application/Policy Number
        /// </summary>
        /// <param name="iPOLICY_LOB"></param>
        /// <param name="iDIV_ID"></param>
        /// <param name="dISSUE_DATE"></param>
        /// <returns></returns>
        /// Added by Charles on 24-May-2010
        ///Added By lalit for itrack # 1493.Susep code for genrate policy Number should pick from pol_customer_policy_list

        public static string GenerateAppPolNumber(int iPOLICY_LOB, int iDIV_ID, DateTime dISSUE_DATE, string sCALLED_FROM, int CO_INSURANCE)
        {
            return GenerateAppPolNumber(iPOLICY_LOB, iDIV_ID, dISSUE_DATE, sCALLED_FROM, CO_INSURANCE,0,0,0);
        }
        public static string GenerateAppPolNumber(int iPOLICY_LOB, int iDIV_ID, DateTime dISSUE_DATE, string sCALLED_FROM)
        {
            return GenerateAppPolNumber(iPOLICY_LOB, iDIV_ID, dISSUE_DATE, sCALLED_FROM, 0,0,0,0);
        }


        public static string GenerateAppPolNumber(int iPOLICY_LOB, int iDIV_ID, DateTime dISSUE_DATE, string sCALLED_FROM, int CO_INSURANCE, int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            string SUSEP_CODE = string.Empty;
            return GenerateAppPolNumber(iPOLICY_LOB, iDIV_ID, dISSUE_DATE, sCALLED_FROM, 0, 0, 0, 0, out SUSEP_CODE);
        }
        public static string GenerateAppPolNumber(int iPOLICY_LOB, int iDIV_ID, DateTime dISSUE_DATE, string sCALLED_FROM, int CO_INSURANCE, int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID,out string SUSEP_LOB_CODE)
        {
            DataSet dsTemp = new DataSet();
            SUSEP_LOB_CODE = "0";
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@POLICY_LOB", iPOLICY_LOB);
                objDataWrapper.AddParameter("@DIV_ID", iDIV_ID);
                objDataWrapper.AddParameter("@ISSUE_DATE", dISSUE_DATE);
                objDataWrapper.AddParameter("@CALLED_FROM", sCALLED_FROM);
                objDataWrapper.AddParameter("@CO_INSURANCE", CO_INSURANCE);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);//Added By lalit for itrack # 1493.Susep code for genrate policy Number should pick from pol_customer_policy_list
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GenerateAppPolNumber");
                objDataWrapper.ClearParameteres();
                string retVal = "";

                if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                {
                    retVal = dsTemp.Tables[0].Rows[0]["APPLICATIONNUMBER"].ToString();
                    SUSEP_LOB_CODE = dsTemp.Tables[0].Rows[0]["SUSEP_LOB_CODE"].ToString();
                }

                return retVal;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                dsTemp.Dispose();
            }
        }

        // Added by Sonal to check duplicate app and policy number 19-07-2010
        public int CheckDuplicateAppNumber(string POLICYAPP_NUMBER, string CALLED_FROM, int iPOLICY_LOB, DateTime dISSUE_DATE)
        {
            int returnResult = 0;
            string strStoredProc = "Proc_CheckDuplicatePolAppNumber";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@POLICYAPP_NUMBER", POLICYAPP_NUMBER);
                objDataWrapper.AddParameter("@CALLED_FROM", CALLED_FROM);
                objDataWrapper.AddParameter("@POLICY_LOB", iPOLICY_LOB);
                objDataWrapper.AddParameter("@ISSUE_DATE", dISSUE_DATE);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN", SqlDbType.Int, ParameterDirection.Output);


                objDataWrapper.ExecuteNonQuery(strStoredProc);
                returnResult = int.Parse(objSqlParameter.Value.ToString());

            }
            catch
            { }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }
        // added by sonal to check wether plani id has been generated intallment or not 20-07-2010
        public DataSet GetPlanStatus(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);


                dsTemp = objDataWrapper.ExecuteDataSet("Proc_CheckInstallPlanData");

                return dsTemp;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }

        public int GetDefaultPlanId(int PolTerm, int Lob_id)
        {
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objWrapper.AddParameter("@APPLABLE_POLTERM", PolTerm);
                objWrapper.AddParameter("@LOB_ID", Lob_id);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_Get_Default_ACT_INSTALL_PLAN_DETAIL");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                else
                    return 0;
            }
            catch (Exception objExp)
            {
                throw (new Exception("Following error occured while fetching default plan." + objExp.Message));
            }
        }




        /// <summary>
        /// Gets LOB_CODE based on LOB_ID from MNT_LOB_MASTER
        /// </summary>
        /// <param name="LOBID">LOB_ID</param>        
        /// <returns>LOB_CODE</returns>
        /// Added by Charles on 7-Apr-10 for Policy Page
        public static string GetLOBCodeFromID(int LOBID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objDataWrapper.AddParameter("@LOB_ID", LOBID, SqlDbType.Int);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetLOBCodeFromID");
                string retVal = "";

                if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
                    retVal = dsTemp.Tables[0].Rows[0][0].ToString();
                return retVal;
            }
            catch (Exception exc)
            { throw (exc); }
        }

        //=================================	
        public DataSet FetchClsAttachmentInfo(int CustomerID, int AppID, int AppVersionID)
        {
            string strStoredProc = "Proc_FetchAppAttachmentInfo";

            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@APP_ID", AppID);
                objWrapper.AddParameter("@APP_VERSION_ID", AppVersionID);

                DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

                return ds;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Update the reject reason data
        /// </summary>
        /// <param name="objPolicyRejectReasonInfo"></param>
        /// <returns></returns>
        public int UpdateRejectReasonInfo(ClsPolicyRejectReasonInfo objPolicyRejectReasonInfo)
        {
            int returnval = 0;

            if (objPolicyRejectReasonInfo.RequiredTransactionLog)
            {
                objPolicyRejectReasonInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyRejectReson.aspx.resx");

                returnval = objPolicyRejectReasonInfo.UpdateRejectReasonInformation();
            }//if (objPolicyRejectReasonInfo.RequiredTransactionLog)
            return returnval;
        }// public int UpdateRejectReasonInfo(ClsPolicyRejectReasonInfo objPolicyRejectReasonInfo)
        //Added by Pradeep Kushwaha on 08-July-2010
        /// <summary>
        /// Get the reject reason data
        /// </summary>
        /// <param name="objProductLocationInfo"></param>
        /// <returns></returns>
        public Boolean FetchRejectReasonInfo(ref ClsPolicyRejectReasonInfo objPolicyRejectReasonInfo)
        {
            Boolean returnValue = false;
            DataSet dsCount = null;
            try
            {
                dsCount = objPolicyRejectReasonInfo.FetchRejectReasonDetails();
                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objPolicyRejectReasonInfo);
                    returnValue = true;
                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;
            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;
        } //public ClsProductLocationInfo FetchProductLocationInfo(int PRODUCT_RISK_ID)


        //Added by Pradeep Kushwaha on 07-July-2010
        /// <summary>
        /// Insert the Policy reject reason description
        /// </summary>
        /// <param name="objPolicyRejectReasonInfo"> ClsPolicyRejectReasonInfo Model object</param>
        /// <returns>return REJECT_REASON_ID</returns>
        public int AddPolicyRejectReason(ClsPolicyRejectReasonInfo objPolicyRejectReasonInfo)
        {
            int returnval = 0;

            if (objPolicyRejectReasonInfo.RequiredTransactionLog)
            {
                objPolicyRejectReasonInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyRejectReson.aspx.resx");
                objPolicyRejectReasonInfo.RequiredTransactionLog = false;
                returnval = objPolicyRejectReasonInfo.AddPolicyRejectReasonData();
            }//if (objPolicyRejectReasonInfo.RequiredTransactionLog)
            return returnval;
        }
        //============================================

        // Added by Pradeep Kushwaha on 21-June-2010 for App and Policy Page
        /// <summary>
        ///  Creates New Version of Application
        /// </summary>
        /// <param name="objGeneralInfo"></param>
        /// <param name="CustomerID"></param>
        /// <param name="AppID"></param>
        /// <param name="AppVersionID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="strLOB"></param>
        /// <param name="new_Version"></param>
        /// <param name="CalledFrom"></param>
        /// <returns></returns>
        public int CopyAppPolFromReject(Cms.Model.Policy.ClsPolicyInfo objGeneralInfo, string New_App_Pol_Number, String CalledFrom)
        {
            string strProcedure = "Proc_CopyAppPol";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                int RetVal = 0;
                if (this.boolTransactionRequired)
                {

                    objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID, SqlDbType.Int);
                    objDataWrapper.AddParameter("@OLD_POL_ID", objGeneralInfo.POLICY_ID, SqlDbType.Int);
                    objDataWrapper.AddParameter("@OLD_POL_VERSION_ID", objGeneralInfo.POLICY_VERSION_ID, SqlDbType.Int);
                    objDataWrapper.AddParameter("@NEW_APP_POL_NUMBER", New_App_Pol_Number, SqlDbType.NVarChar);
                    objDataWrapper.AddParameter("@CREATED_BY", objGeneralInfo.CREATED_BY, SqlDbType.Int);

                    SqlParameter objSqlParameterPOL = (SqlParameter)objDataWrapper.AddParameter("@POL_NEW_ID", SqlDbType.Int, ParameterDirection.Output);
                    SqlParameter objSqlParameterAPP = (SqlParameter)objDataWrapper.AddParameter("@APP_NEW_ID", SqlDbType.Int, ParameterDirection.Output);



                    RetVal = objDataWrapper.ExecuteNonQuery(strProcedure);

                    if (RetVal > 0)
                    {
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                        objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");



                        string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);

                        if (CalledFrom == "APP")
                            objTransactionInfo.TRANS_TYPE_ID = 251;
                        else
                            objTransactionInfo.TRANS_TYPE_ID = 252;

                        objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                        objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                        objTransactionInfo.POLICY_ID = objGeneralInfo.POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objGeneralInfo.POLICY_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objGeneralInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC = "";

                        objTransactionInfo.CUSTOM_INFO = ";" + FetchGeneralMessage("1160", "") + " = " + New_App_Pol_Number;

                        objTransactionInfo.CHANGE_XML = strTranXML;

                        objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                        objGeneralInfo.POLICY_ID = int.Parse(objSqlParameterPOL.Value.ToString());
                        objGeneralInfo.POLICY_VERSION_ID = 1;
                        objGeneralInfo.APP_ID = int.Parse(objSqlParameterAPP.Value.ToString());
                        objGeneralInfo.APP_VERSION_ID = 1;

                    }
                }
                else
                {
                    RetVal = objDataWrapper.ExecuteNonQuery(strProcedure);
                }

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return RetVal;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }

        }


        /// <summary>
        /// Creates New Version of Application
        /// </summary>
        /// <param name="objGeneralInfo"></param>
        /// <param name="CustomerID"></param>
        /// <param name="AppID"></param>
        /// <param name="AppVersionID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="strLOB"></param>
        /// <param name="new_Version"></param>
        /// <returns></returns>
        /// Added by Charles on 6-Apr-2010 for Policy Page
        public int CopyApplication(Cms.Model.Policy.ClsPolicyInfo objGeneralInfo, int CustomerID, int AppID, int AppVersionID, int CreatedBy, string strLOB, out int new_Version)
        {
            //string strTranXML;
            int returnResult = 0;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@CREATED_BY", CreatedBy, SqlDbType.Int);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@NEW_VERSION", SqlDbType.Int, ParameterDirection.Output);

                try
                {
                    //if transaction required
                    if (this.boolTransactionRequired)
                    {
                        objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");
                    }
                    int new_APP_Version_ID;
                    if (this.boolTransactionRequired)
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery("Proc_CopyPolicy");

                        objDataWrapper.ClearParameteres();

                        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

                        new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                        new_Version = new_APP_Version_ID;

                        if (returnResult > 0)
                        {

                            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                            objGeneralInfo.APP_VERSION = new_Version + ".0";

                            string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
                            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                            objTransactionInfo.TRANS_TYPE_ID = 174;
                            objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;

                            objTransactionInfo.APP_VERSION_ID = new_Version;
                            objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                            objTransactionInfo.RECORDED_BY = CreatedBy;
                            objTransactionInfo.TRANS_DESC = "";//"New Application Version copied successfully.";

                            objTransactionInfo.CUSTOM_INFO = "Old Application Version = " + objGeneralInfo.APP_VERSION_ID + ".0";
                            objTransactionInfo.CHANGE_XML = strTranXML;

                            objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                            returnResult = 1;
                        }
                    }
                    else//if no transaction required
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery("Proc_CopyPolicy");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                        new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                        new_Version = new_APP_Version_ID;
                    }

                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                }
                catch (Exception ex)
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                    throw (ex);
                }

                //Commented by Charles on 14-May-10 for Policy Page Implementation
                //objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                //objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                //objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                //objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);
                //objDataWrapper.AddParameter("@NEW_APP_VERSION_ID", new_Version);

                //try
                //{
                //    //if transaction required
                //    int ValRet = 0;
                //    if (this.boolTransactionRequired)
                //    {
                //        ValRet = objDataWrapper.ExecuteNonQuery("Proc_CopyApplicationAttachment");
                //        objDataWrapper.ClearParameteres();
                //        if (ValRet > 0)
                //        {                            
                //            DataSet dsClsAttachmentInfo = FetchClsAttachmentInfo(objGeneralInfo.CUSTOMER_ID, objGeneralInfo.APP_ID, new_Version);

                //            string ATTACH_FILE_NAME = "";
                //            string ATTACH_FILE_DESC = "";
                //            string ATTACH_FILE_TYPE = "";
                //            StringBuilder sbTranXML = new StringBuilder();
                //            sbTranXML.Append("<root>");
                //            int ATTACH_TYPE = 0;
                //            if (dsClsAttachmentInfo.Tables[0].Rows.Count > 0)
                //            {
                //                for (int i = 0; i < dsClsAttachmentInfo.Tables[0].Rows.Count; i++)
                //                {
                //                    ATTACH_FILE_NAME = dsClsAttachmentInfo.Tables[0].Rows[i]["ATTACH_FILE_NAME"].ToString();
                //                    ATTACH_FILE_DESC = dsClsAttachmentInfo.Tables[0].Rows[i]["ATTACH_FILE_DESC"].ToString();
                //                    ATTACH_FILE_TYPE = dsClsAttachmentInfo.Tables[0].Rows[i]["ATTACH_FILE_TYPE"].ToString();
                //                    ATTACH_TYPE = int.Parse(dsClsAttachmentInfo.Tables[0].Rows[i]["ATTACH_TYPE"].ToString());

                //                    Cms.Model.Maintenance.ClsAttachmentInfo objClsAttachmentInfo = new Cms.Model.Maintenance.ClsAttachmentInfo();
                //                    objClsAttachmentInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddAttachment.aspx.resx");                                    

                //                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                //                    objClsAttachmentInfo.ATTACH_FILE_NAME = ATTACH_FILE_NAME;
                //                    objClsAttachmentInfo.ATTACH_FILE_DESC = ATTACH_FILE_DESC;
                //                    objClsAttachmentInfo.ATTACH_FILE_TYPE = ATTACH_FILE_TYPE;
                //                    objClsAttachmentInfo.ATTACH_TYPE = ATTACH_TYPE;

                //                    string strTranXML = objBuilder.GetTransactionLogXML(objClsAttachmentInfo);
                //                    sbTranXML.Append(strTranXML);
                //                }
                //                sbTranXML.Append("</root>");
                //                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                //                objTransactionInfo.TRANS_TYPE_ID = 1;
                //                objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                //                objTransactionInfo.APP_VERSION_ID = new_Version;
                //                objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                //                objTransactionInfo.RECORDED_BY = CreatedBy;
                //                objTransactionInfo.TRANS_DESC = "Attachment copied successfully.";
                //                objTransactionInfo.CUSTOM_INFO = "Old Application Version = " + objGeneralInfo.APP_VERSION_ID + ".0";
                //                objTransactionInfo.CHANGE_XML = sbTranXML.ToString();

                //                objDataWrapper.ExecuteNonQuery(objTransactionInfo);
                //            }
                //            ValRet = 1;
                //        }
                //    }
                //    else//if no transaction required
                //    {
                //        ValRet = objDataWrapper.ExecuteNonQuery("Proc_CopyApplicationAttachment");
                //        objDataWrapper.ClearParameteres();
                //    }

                //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);                    
                //}
                //catch (Exception ex)
                //{
                //    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                //    throw (ex);
                //}              
            }

            finally
            {
            }
            return returnResult;
        }



        public int CopyApplication(Cms.Model.Application.ClsGeneralInfo objGeneralInfo, int CustomerID, int AppID, int AppVersionID, int CreatedBy, string strLOB, out int new_Version)
        {
            //string strTranXML;
            int returnResult = 0;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@CREATED_BY", CreatedBy, SqlDbType.Int);
                 
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@NEW_VERSION", SqlDbType.Int, ParameterDirection.Output);


                try
                {
                    //if transaction required
                    if (this.boolTransactionRequired)
                    {
                        objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                    }
                    int new_APP_Version_ID;
                    if (this.boolTransactionRequired)
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery("Proc_CopyApplication");

                        objDataWrapper.ClearParameteres();

                        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

                        new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                        new_Version = new_APP_Version_ID;

                        if (returnResult > 0)
                        {

                            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                            //Oct27,2005:Sumit Chhabra:Modified from and Modified To should not come in transaction log. Hence the following line is being commented

                            //Added by Asfa (23-Apr-2008) - iTrack issue #4043
                            objGeneralInfo.APP_VERSION = new_Version + ".0";

                            string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
                            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                            objTransactionInfo.TRANS_TYPE_ID = 1;
                            objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                            //objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                            //To display the version as decimal, the following change has been made
                            objTransactionInfo.APP_VERSION_ID = new_Version;
                            objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                            objTransactionInfo.RECORDED_BY = CreatedBy;
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1572", "");// "New Application Version copied successfully.";
                            //Oct27,2005:Sumit Chahbra: No modified from & to info to be generated
                            //objTransactionInfo.CHANGE_XML		=	strTranXML;
                            //Display information about old and new application version
                            //Nov 08,2005:Sumit Chhabra: Old Application Version string added to transaction has been removed
                            //objTransactionInfo.CUSTOM_INFO		=	";Old Application Version = " + objGeneralInfo.APP_VERSION_ID + ".0" + ";New Application Version = " + new_Version + ".0";
                            objTransactionInfo.CUSTOM_INFO = "Old Application Version = " + objGeneralInfo.APP_VERSION_ID + ".0";//LOB = " + strLOB;
                            objTransactionInfo.CHANGE_XML = strTranXML;


                            objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                            returnResult = 1;
                        }
                    }
                    else//if no transaction required
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery("Proc_CopyApplication");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                        new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                        new_Version = new_APP_Version_ID;

                    }


                    //					objDataWrapper.ClearParameteres();
                    //
                    //					
                    //					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                    //returnResult	=	objDataWrapper.ExecuteNonQuery("Proc_CopyApplication");
                }
                catch (Exception ex)
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                    throw (ex);
                }

                //				int new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                //				new_Version=new_APP_Version_ID ;

                //added by vj to copy the attachments of the application version 
                // -- start --
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@NEW_APP_VERSION_ID", new_Version);

                try
                {
                    //if transaction required
                    int ValRet = 0;
                    if (this.boolTransactionRequired)
                    {
                        ValRet = objDataWrapper.ExecuteNonQuery("Proc_CopyApplicationAttachment");
                        objDataWrapper.ClearParameteres();
                        if (ValRet > 0)
                        {
                            ////////////////////////
                            DataSet dsClsAttachmentInfo = FetchClsAttachmentInfo(objGeneralInfo.CUSTOMER_ID, objGeneralInfo.APP_ID, new_Version);

                            string ATTACH_FILE_NAME = "";
                            string ATTACH_FILE_DESC = "";
                            string ATTACH_FILE_TYPE = "";
                            StringBuilder sbTranXML = new StringBuilder();
                            sbTranXML.Append("<root>");
                            int ATTACH_TYPE = 0;
                            if (dsClsAttachmentInfo.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsClsAttachmentInfo.Tables[0].Rows.Count; i++)
                                {
                                    ATTACH_FILE_NAME = dsClsAttachmentInfo.Tables[0].Rows[i]["ATTACH_FILE_NAME"].ToString();
                                    ATTACH_FILE_DESC = dsClsAttachmentInfo.Tables[0].Rows[i]["ATTACH_FILE_DESC"].ToString();
                                    ATTACH_FILE_TYPE = dsClsAttachmentInfo.Tables[0].Rows[i]["ATTACH_FILE_TYPE"].ToString();
                                    ATTACH_TYPE = int.Parse(dsClsAttachmentInfo.Tables[0].Rows[i]["ATTACH_TYPE"].ToString());

                                    //////////////////////////							

                                    Cms.Model.Maintenance.ClsAttachmentInfo objClsAttachmentInfo = new Cms.Model.Maintenance.ClsAttachmentInfo();
                                    objClsAttachmentInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddAttachment.aspx.resx");
                                    //objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddAttachment.aspx.resx");

                                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                                    objClsAttachmentInfo.ATTACH_FILE_NAME = ATTACH_FILE_NAME;
                                    objClsAttachmentInfo.ATTACH_FILE_DESC = ATTACH_FILE_DESC;
                                    objClsAttachmentInfo.ATTACH_FILE_TYPE = ATTACH_FILE_TYPE;
                                    objClsAttachmentInfo.ATTACH_TYPE = ATTACH_TYPE;

                                    string strTranXML = objBuilder.GetTransactionLogXML(objClsAttachmentInfo);
                                    sbTranXML.Append(strTranXML);
                                }
                                sbTranXML.Append("</root>");
                                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                                objTransactionInfo.TRANS_TYPE_ID = 1;
                                objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                                objTransactionInfo.APP_VERSION_ID = new_Version;//objGeneralInfo.APP_VERSION_ID;
                                objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                                objTransactionInfo.RECORDED_BY = CreatedBy;
                                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1573", "");// "Attachment copied successfully.";
                                objTransactionInfo.CUSTOM_INFO = "Old Application Version = " + objGeneralInfo.APP_VERSION_ID + ".0";//LOB = " + strLOB;
                                objTransactionInfo.CHANGE_XML = sbTranXML.ToString();

                                objDataWrapper.ExecuteNonQuery(objTransactionInfo);
                            }
                            ValRet = 1;
                        }
                    }
                    else//if no transaction required
                    {
                        ValRet = objDataWrapper.ExecuteNonQuery("Proc_CopyApplicationAttachment");
                        objDataWrapper.ClearParameteres();
                    }

                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                    //returnResult	=	objDataWrapper.ExecuteNonQuery("Proc_CopyApplicationAttachment");
                }
                catch (Exception ex)
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                    throw (ex);
                }

                //int ValRet = objDataWrapper.ExecuteNonQuery("Proc_CopyApplicationAttachment");

                // -- end --

                //return RetVal;
            }
            //			catch(Exception exc)
            //			{throw (exc);}
            finally
            {
            }
            return returnResult;
        }

        /// <summary>
        /// Delete Application
        /// </summary>
        /// <param name="objGeneralInfo"></param>
        /// <param name="CustomerID"></param>
        /// <param name="AppID"></param>
        /// <param name="AppVersionID"></param>
        /// <param name="DeletedBy"></param>
        /// <returns></returns>
        /// Added by Charles on 6-Apr-2010 for Policy Page
        public int DeleteApplication(Cms.Model.Policy.ClsPolicyInfo objGeneralInfo, int CustomerID, int AppID, int AppVersionID, int DeletedBy)
        {
            int returnResult = 0;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);

                try
                {
                    //if transaction required
                    if (this.boolTransactionRequired)
                    {
                        objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");
                    }

                    if (this.boolTransactionRequired)
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery("PROC_DELETE_APP_POLICY");
                        if (returnResult > 0)
                        {
                            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                            objTransactionInfo.TRANS_TYPE_ID = 126;
                            objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                            objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                            objTransactionInfo.CUSTOM_INFO = objGeneralInfo.CUSTOM_INFO;//";Application Number: " + objGeneralInfo.APP_NUMBER + ";Version: " + objGeneralInfo.APP_VERSION;
                            objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                            objTransactionInfo.RECORDED_BY = DeletedBy;
                            objTransactionInfo.TRANS_DESC = "";//"Application version deleted";                            
                            objTransactionInfo.CHANGE_XML = "";

                            objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                            returnResult = 1;
                        }
                    }
                    else//if no transaction required
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery("PROC_DELETE_APP_POLICY");
                    }

                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);

                }
                catch (Exception ex)
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                    throw (ex);
                }
            }
            finally
            { }
            return returnResult;
        }

        public int DeleteApplication(Cms.Model.Application.ClsGeneralInfo objGeneralInfo, int CustomerID, int AppID, int AppVersionID, int DeletedBy)
        {
            int returnResult = 0;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", AppID, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID, SqlDbType.Int);

                try
                {
                    //if transaction required
                    if (this.boolTransactionRequired)
                    {
                        objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                    }

                    if (this.boolTransactionRequired)
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery("Proc_DeleteApplication");
                        if (returnResult > 0)
                        {

                            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                            //October 26, 2005:Sumit Chhabra:Commented as the value so returned is not being used anywhere
                            //string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
                            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                            objTransactionInfo.TRANS_TYPE_ID = 1;
                            objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                            objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                            objTransactionInfo.CUSTOM_INFO = ";Application Number:" + objGeneralInfo.APP_NUMBER + ";Version: " + objGeneralInfo.APP_VERSION;
                            objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                            objTransactionInfo.RECORDED_BY = DeletedBy;
                            objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1563", "");// "Application version deleted";
                            //objTransactionInfo.TRANS_DESC		="APPLICATION VERSION DELETED: Application No.: " + objGeneralInfo.APP_NUMBER;
                            //October 26,2005:Sumit Chhabra:Commented to prevent modified from and to from appearing in the transaction log
                            //objTransactionInfo.CHANGE_XML		=	strTranXML;
                            objTransactionInfo.CHANGE_XML = "";


                            objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                            returnResult = 1;
                        }
                    }
                    else//if no transaction required
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery("Proc_DeleteApplication");

                    }


                    objDataWrapper.ClearParameteres();


                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                    //returnResult	=	objDataWrapper.ExecuteNonQuery("Proc_CopyApplication");
                }
                catch (Exception ex)
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                    throw (ex);
                }

                //int RetVal = objDataWrapper.ExecuteNonQuery("Proc_DeleteApplication");


            }

            finally
            { }
            return returnResult;
        }

        //		public static string GeneratePolicyNumber(int LOBID)
        //		{
        //			try
        //			{
        //				DataSet dsTemp = new DataSet();
        //			
        //				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
        //			 
        //				objDataWrapper.AddParameter("@POLICY_LOB",LOBID,SqlDbType.Int);
        //
        //				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GeneratePolicyNumber");
        //				string  retVal="";
        //
        //				if (dsTemp!=null)
        //					retVal = dsTemp.Tables[0].Rows[0][0].ToString();
        //				return retVal;
        //				 
        //			}
        //			catch(Exception exc)
        //			{throw (exc);}
        //			finally
        //			{}
        //		}

        #endregion

        #region Generate Claim ID links for a policy
        public System.Text.StringBuilder GetClaimNumberAsLinkForPolicy(DataSet dsPolicy, string SystemId)
        {
            System.Text.StringBuilder sbClaimLink = new StringBuilder();
            string strCurrentWorkingClaim = "";
            string strCarrier = CarrierSystemID;// System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString().ToUpper();

            if (System.Web.HttpContext.Current.Session["claimID"] != null)
                strCurrentWorkingClaim = System.Web.HttpContext.Current.Session["claimID"].ToString();
            int ClaimFound = -1;
            //sbClaimLink = new System.Text.StringBuilder();
            for (int i = 0; i < dsPolicy.Tables[0].Rows.Count; i++)
            {
                if (dsPolicy.Tables[0].Rows[i]["CLAIM_ID"].ToString() != "0")
                {
                    if (ClaimFound != 1 && strCurrentWorkingClaim.Equals(dsPolicy.Tables[0].Rows[i]["CLAIM_ID"].ToString()))
                    {
                        //Modified by Asfa(09-July-2008) - iTrack #4459
                        if (SystemId.Equals(strCarrier))
                            sbClaimLink.Append("<a class='CalLink' style='TEXT-DECORATION: underline;FONT-WEIGHT: bold' href='javascript:ShowClaim(" + dsPolicy.Tables[0].Rows[i]["CLAIM_ID"] + "," + dsPolicy.Tables[0].Rows[i]["CLAIM_POLICY_VERSION_ID"] + ")'>" + dsPolicy.Tables[0].Rows[i]["CLAIM_NUMBER"] + "</a>, ");//Added parameter CLAIM_POLICY_VERSION_ID by Charles on 14-Sep-09 for Itrack 6317
                        else
                            sbClaimLink.Append(dsPolicy.Tables[0].Rows[i]["CLAIM_NUMBER"] + ", ");
                        ClaimFound = 1;
                    }
                    else
                    {
                        if (SystemId.Equals(strCarrier))
                            sbClaimLink.Append("<a class='CalLink' style='TEXT-DECORATION: underline' href='javascript:ShowClaim(" + dsPolicy.Tables[0].Rows[i]["CLAIM_ID"] + "," + dsPolicy.Tables[0].Rows[i]["CLAIM_POLICY_VERSION_ID"] + ")'>" + dsPolicy.Tables[0].Rows[i]["CLAIM_NUMBER"] + "</a>, ");//Added parameter CLAIM_POLICY_VERSION_ID by Charles on 3-Sep-09 for Itrack 6317
                        else
                            sbClaimLink.Append(dsPolicy.Tables[0].Rows[i]["CLAIM_NUMBER"] + ", ");
                    }
                }

            }
            return sbClaimLink;
        }
        #endregion

        #region SAVE AND UPDATE
        /// <summary>
        /// Inserting Application General details
        /// </summary>
        /// <param name="objGeneralInfo">Modal Object for Application</param>
        /// <returns>int: this is the number of records added.</returns>
        public int Add(Cms.Model.Application.ClsGeneralInfo objGeneralInfo)
        {
            string strStoredProc = "Proc_InsertApplicationGenInfo";
            DateTime RecordDate = DateTime.Now;

            //Set transaction label in the new object, if required
            if (this.boolTransactionRequired)
            {
                objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
            }

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
            //objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID,SqlDbType.Int ,ParameterDirection.Output ,5);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@PARENT_APP_VERSION_ID", objGeneralInfo.PARENT_APP_VERSION_ID);
            objDataWrapper.AddParameter("@APP_STATUS", objGeneralInfo.APP_STATUS);
            objDataWrapper.AddParameter("@APP_NUMBER", objGeneralInfo.APP_NUMBER);
            objDataWrapper.AddParameter("@APP_VERSION", objGeneralInfo.APP_VERSION);
            objDataWrapper.AddParameter("@APP_TERMS", objGeneralInfo.APP_TERMS);
            if (objGeneralInfo.APP_INCEPTION_DATE != DateTime.MinValue)
            {
                objDataWrapper.AddParameter("@APP_INCEPTION_DATE", objGeneralInfo.APP_INCEPTION_DATE);
            }

            objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", objGeneralInfo.APP_EFFECTIVE_DATE);
            objDataWrapper.AddParameter("@APP_EXPIRATION_DATE", objGeneralInfo.APP_EXPIRATION_DATE);
            objDataWrapper.AddParameter("@APP_LOB", objGeneralInfo.APP_LOB);
            objDataWrapper.AddParameter("@APP_SUBLOB", objGeneralInfo.APP_SUBLOB);
            objDataWrapper.AddParameter("@CSR", objGeneralInfo.CSR);
            objDataWrapper.AddParameter("@PRODUCER", objGeneralInfo.PRODUCER);
            objDataWrapper.AddParameter("@UNDERWRITER", objGeneralInfo.UNDERWRITER);
            objDataWrapper.AddParameter("@IS_UNDER_REVIEW", null);
            objDataWrapper.AddParameter("@APP_AGENCY_ID", objGeneralInfo.APP_AGENCY_ID);
            objDataWrapper.AddParameter("@IS_ACTIVE", objGeneralInfo.IS_ACTIVE);

            objDataWrapper.AddParameter("@CREATED_BY", objGeneralInfo.CREATED_BY);
            objDataWrapper.AddParameter("@CREATED_DATETIME", objGeneralInfo.CREATED_DATETIME);
            objDataWrapper.AddParameter("@MODIFIED_BY", null);
            objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);

            objDataWrapper.AddParameter("@COUNTRY_ID", objGeneralInfo.COUNTRY_ID);
            objDataWrapper.AddParameter("@STATE_ID", objGeneralInfo.STATE_ID);
            //			if(objGeneralInfo.YEAR_AT_CURR_RESI==Double.MinValue)
            //				objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI",System.DBNull.Value);
            //			else
            //				objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI",objGeneralInfo.YEAR_AT_CURR_RESI);
            //Nov,07,2005:Sumit Chhabra:Datatype for YEAR_AT_CURR_RESI being changed from double to int
            if (objGeneralInfo.YEAR_AT_CURR_RESI == Int32.MinValue)
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", objGeneralInfo.YEAR_AT_CURR_RESI);

            objDataWrapper.AddParameter("@YEARS_AT_PREV_ADD", objGeneralInfo.YEARS_AT_PREV_ADD);
            //billing information
            objDataWrapper.AddParameter("@DIV_ID", objGeneralInfo.DIV_ID);
            objDataWrapper.AddParameter("@DEPT_ID", objGeneralInfo.DEPT_ID);
            objDataWrapper.AddParameter("@PC_ID", objGeneralInfo.PC_ID);
            objDataWrapper.AddParameter("@BILL_TYPE", objGeneralInfo.BILL_TYPE_ID);
            objDataWrapper.AddParameter("@COMPLETE_APP", objGeneralInfo.COMPLETE_APP);
            objDataWrapper.AddParameter("@PROPRTY_INSP_CREDIT", objGeneralInfo.PROPRTY_INSP_CREDIT);
            objDataWrapper.AddParameter("@INSTALL_PLAN_ID", objGeneralInfo.INSTALL_PLAN_ID);
            objDataWrapper.AddParameter("@CHARGE_OFF_PRMIUM", objGeneralInfo.CHARGE_OFF_PRMIUM);
            if (objGeneralInfo.RECEIVED_PRMIUM == Double.MinValue)
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", objGeneralInfo.RECEIVED_PRMIUM);
            if (objGeneralInfo.PROXY_SIGN_OBTAINED != int.MinValue) // proxy sign is set
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", objGeneralInfo.PROXY_SIGN_OBTAINED);
            }
            else
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", null);
            }
            if (objGeneralInfo.POLICY_TYPE == -1)
                objDataWrapper.AddParameter("@POLICY_TYPE", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@POLICY_TYPE", objGeneralInfo.POLICY_TYPE);



            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID, SqlDbType.Int, ParameterDirection.Output);


            int returnResult = 0;
            int APP_ID = 0;
            try
            {
                //if transaction required

                if (this.boolTransactionRequired)
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    if (returnResult > 0)
                    {
                        APP_ID = int.Parse(objSqlParameter.Value.ToString());
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                        objTransactionInfo.TRANS_TYPE_ID = 1;
                        objTransactionInfo.APP_ID = APP_ID;
                        objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objGeneralInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1564", "");// "New application added";
                        objTransactionInfo.CHANGE_XML = strTranXML;


                        objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                        returnResult = 1;
                    }
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    APP_ID = int.Parse(objSqlParameter.Value.ToString());//int.Parse(objDataWrapper.CommandParameters[1].Value.ToString());
                }

                objGeneralInfo.APP_ID = APP_ID;

                objDataWrapper.ClearParameteres();

                //Insert into Diary
                /*				Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();

                                ClsDiary objDiary =  = new ClsDiary();
				
                                objDiary.TransactionLogRequired = true;

                                PopulateToDoListObject(objToDoListInfo,objGeneralInfo);

                                objDiary.Add(objToDoListInfo,objDataWrapper);
                */

                //Commit the transaction if all opeations are successfull.
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                return APP_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return 0;
        }
        public static int CheckForApplicationStatus(int customer_id, int app_id, int app_version)
        {
            string strStoredProc = "Proc_CheckAPP_Status";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
            objDataWrapper.AddParameter("@APP_ID", app_id);
            objDataWrapper.AddParameter("@APP_VERSION_ID", app_version);

            SqlParameter convert = (SqlParameter)objDataWrapper.AddParameter("@convertr", SqlDbType.Int, ParameterDirection.Output);

            objDataWrapper.ExecuteNonQuery(strStoredProc);

            return Convert.ToInt32(convert.Value);

        }

        public static int CheckForConvertedVersion(int customer_id, int app_id, int app_version)
        {
            string strStoredProc = "Proc_CheckAPP_Version_Converted_to_Policy";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
            objDataWrapper.AddParameter("@APP_ID", app_id);
            objDataWrapper.AddParameter("@APP_VERSION_ID", app_version);

            SqlParameter convert = (SqlParameter)objDataWrapper.AddParameter("@convertr", SqlDbType.Int, ParameterDirection.Output);

            objDataWrapper.ExecuteNonQuery(strStoredProc);

            return Convert.ToInt32(convert.Value);

        }
        public static int CheckForRuleVerificationStatus(int customer_id, int app_id, int app_version)
        {
            return CheckForRuleVerificationStatus(customer_id, app_id, app_version, "");
        }
        public static int CheckForRuleVerificationStatus(int customer_id, int app_id, int app_version, string CalledFrom)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
                objDataWrapper.AddParameter("@APP_ID", app_id);
                objDataWrapper.AddParameter("@APP_VERSION_ID", app_version);
                objDataWrapper.AddParameter("@CALLED_FROM", CalledFrom);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue);
                objDataWrapper.ExecuteNonQuery("Proc_CheckApp_Rule_Verification_Status");
                if (objSqlParameter != null && objSqlParameter.Value.ToString() != "")
                {
                    return int.Parse(objSqlParameter.Value.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper = null;
            }


        }


        public static int CheckForConverted(int customer_id, int app_id)
        {
            string strStoredProc = "Proc_CheckAPP_Converted_to_Policy";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
            objDataWrapper.AddParameter("@APP_ID", app_id);

            SqlParameter convert = (SqlParameter)objDataWrapper.AddParameter("@convertr", SqlDbType.Int, ParameterDirection.Output);

            objDataWrapper.ExecuteNonQuery(strStoredProc);

            return Convert.ToInt32(convert.Value);

        }
        /// <summary>
        /// Inserting Application UnderWriting Question details
        /// </summary>
        /// <param name="objGeneralInfo">Modal Object for Application</param>
        /// /// <param name="objGeneralInfo">Modal Object for GeneralInformation</param>
        /// <returns>int: this is the number of records added.</returns>
        public int AddUWGEN(Cms.Model.Application.ClsGeneralInfo objGeneralInfo, Cms.Model.Application.ClsPPGeneralInformationInfo objPPGeneralInformation, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_InsertPPGeneralInformationAccord";
            DateTime RecordDate = DateTime.Now;

            //Set transaction label in the new object, if required
            if (this.boolTransactionRequired)
            {
                objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\PPGeneralInformationIframe.aspx.resx");
            }

            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH", objPPGeneralInformation.ANY_NON_OWNED_VEH);
            objDataWrapper.AddParameter("@ANY_NON_OWNED_VEH_PP_DESC", objPPGeneralInformation.ANY_NON_OWNED_VEH_PP_DESC);
            objDataWrapper.AddParameter("@CAR_MODIFIED", objPPGeneralInformation.CAR_MODIFIED);
            objDataWrapper.AddParameter("@CAR_MODIFIED_DESC", objPPGeneralInformation.CAR_MODIFIED_DESC);
            objDataWrapper.AddParameter("@EXISTING_DMG", objPPGeneralInformation.EXISTING_DMG);
            objDataWrapper.AddParameter("@EXISTING_DMG_PP_DESC", objPPGeneralInformation.EXISTING_DMG_PP_DESC);
            objDataWrapper.AddParameter("@ANY_CAR_AT_SCH", objPPGeneralInformation.ANY_CAR_AT_SCH);
            objDataWrapper.AddParameter("@ANY_CAR_AT_SCH_DESC", objPPGeneralInformation.ANY_CAR_AT_SCH_DESC);
            //objDataWrapper.AddParameter("@SCHOOL_CARS_LIST",objPPGeneralInformation.SCHOOL_CARS_LIST);
            objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU", objPPGeneralInformation.ANY_OTH_AUTO_INSU);
            objDataWrapper.AddParameter("@ANY_OTH_AUTO_INSU_DESC", objPPGeneralInformation.ANY_OTH_AUTO_INSU_DESC);
            objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP_PP_DESC", objPPGeneralInformation.ANY_OTH_INSU_COMP_PP_DESC);
            objDataWrapper.AddParameter("@ANY_OTH_INSU_COMP", objPPGeneralInformation.ANY_OTH_INSU_COMP);
            //objDataWrapper.AddParameter("@OTHER_POLICY_NUMBER_LIST",objPPGeneralInformation.OTHER_POLICY_NUMBER_LIST);
            objDataWrapper.AddParameter("@H_MEM_IN_MILITARY", objPPGeneralInformation.H_MEM_IN_MILITARY);
            objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_DESC", objPPGeneralInformation.H_MEM_IN_MILITARY_DESC);
            //objDataWrapper.AddParameter("@H_MEM_IN_MILITARY_LIST",objPPGeneralInformation.H_MEM_IN_MILITARY_LIST);
            objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED", objPPGeneralInformation.DRIVER_SUS_REVOKED);
            objDataWrapper.AddParameter("@DRIVER_SUS_REVOKED_PP_DESC", objPPGeneralInformation.DRIVER_SUS_REVOKED_PP_DESC);
            objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED", objPPGeneralInformation.PHY_MENTL_CHALLENGED);
            objDataWrapper.AddParameter("@PHY_MENTL_CHALLENGED_PP_DESC", objPPGeneralInformation.PHY_MENTL_CHALLENGED_PP_DESC);
            objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY", objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY);
            objDataWrapper.AddParameter("@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC", objPPGeneralInformation.ANY_FINANCIAL_RESPONSIBILITY_PP_DESC);
            objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER", objPPGeneralInformation.INS_AGENCY_TRANSFER);
            objDataWrapper.AddParameter("@INS_AGENCY_TRANSFER_PP_DESC", objPPGeneralInformation.INS_AGENCY_TRANSFER_PP_DESC);
            objDataWrapper.AddParameter("@COVERAGE_DECLINED", objPPGeneralInformation.COVERAGE_DECLINED);
            objDataWrapper.AddParameter("@COVERAGE_DECLINED_PP_DESC", objPPGeneralInformation.COVERAGE_DECLINED_PP_DESC);
            objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED", objPPGeneralInformation.AGENCY_VEH_INSPECTED);
            objDataWrapper.AddParameter("@AGENCY_VEH_INSPECTED_PP_DESC", objPPGeneralInformation.AGENCY_VEH_INSPECTED_PP_DESC);
            objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE", objPPGeneralInformation.USE_AS_TRANSPORT_FEE);
            objDataWrapper.AddParameter("@USE_AS_TRANSPORT_FEE_DESC", objPPGeneralInformation.USE_AS_TRANSPORT_FEE_DESC);
            objDataWrapper.AddParameter("@SALVAGE_TITLE", objPPGeneralInformation.SALVAGE_TITLE);
            objDataWrapper.AddParameter("@SALVAGE_TITLE_PP_DESC", objPPGeneralInformation.SALVAGE_TITLE_PP_DESC);
            objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO", objPPGeneralInformation.ANY_ANTIQUE_AUTO);
            objDataWrapper.AddParameter("@ANY_ANTIQUE_AUTO_DESC", objPPGeneralInformation.ANY_ANTIQUE_AUTO_DESC);
            objDataWrapper.AddParameter("@POLICY_DESCRIPTION", objPPGeneralInformation.POLICY_DESCRIPTION);
            objDataWrapper.AddParameter("@IS_ACTIVE", objPPGeneralInformation.IS_ACTIVE);
            objDataWrapper.AddParameter("@CREATED_BY", objPPGeneralInformation.CREATED_BY);
            objDataWrapper.AddParameter("@CREATED_DATETIME", objPPGeneralInformation.CREATED_DATETIME);
            objDataWrapper.AddParameter("@MODIFIED_BY", null);
            objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);
            objDataWrapper.AddParameter("@INSERTUPDATE", "I");
            objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED", objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED);
            objDataWrapper.AddParameter("@MULTI_POLICY_DISC_APPLIED_PP_DESC", objPPGeneralInformation.MULTI_POLICY_DISC_APPLIED_PP_DESC);
            objDataWrapper.AddParameter("@YEARS_INSU_WOL", DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU_WOL));
            objDataWrapper.AddParameter("@YEARS_INSU", DefaultValues.GetIntNull(objPPGeneralInformation.YEARS_INSU));
            objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES", objPPGeneralInformation.ANY_PRIOR_LOSSES);
            objDataWrapper.AddParameter("@ANY_PRIOR_LOSSES_DESC", objPPGeneralInformation.ANY_PRIOR_LOSSES_DESC);
            if (objPPGeneralInformation.COST_EQUIPMENT_DESC != 0)
            {
                objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC", objPPGeneralInformation.COST_EQUIPMENT_DESC);
            }
            else
            {
                objDataWrapper.AddParameter("@COST_EQUIPMENT_DESC", null);
            }
            objDataWrapper.AddParameter("@CURR_RES_TYPE", objPPGeneralInformation.CURR_RES_TYPE);
            objDataWrapper.AddParameter("@IS_OTHER_THAN_INSURED", objPPGeneralInformation.IS_OTHER_THAN_INSURED);
            if (objPPGeneralInformation.IS_OTHER_THAN_INSURED == "1")
            {
                objDataWrapper.AddParameter("@FullName", objPPGeneralInformation.FullName);
                if (objPPGeneralInformation.DATE_OF_BIRTH != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", objPPGeneralInformation.DATE_OF_BIRTH);
                else
                    objDataWrapper.AddParameter("@DATE_OF_BIRTH", null);
                objDataWrapper.AddParameter("@DrivingLisence", objPPGeneralInformation.DrivingLisence);
                objDataWrapper.AddParameter("@PolicyNumber", objPPGeneralInformation.PolicyNumber);
                objDataWrapper.AddParameter("@CompanyName", objPPGeneralInformation.CompanyName);
                objDataWrapper.AddParameter("@InsuredElseWhere", objPPGeneralInformation.InsuredElseWhere);
                objDataWrapper.AddParameter("@WhichCycle", objPPGeneralInformation.WhichCycle);
            }
            else
            {
                objDataWrapper.AddParameter("@FullName", null);
                objDataWrapper.AddParameter("@DATE_OF_BIRTH", null);
                objDataWrapper.AddParameter("@DrivingLisence", null);
                objDataWrapper.AddParameter("@WhichCycle", null);
                objDataWrapper.AddParameter("@PolicyNumber", null);
                objDataWrapper.AddParameter("@CompanyName", null);
                objDataWrapper.AddParameter("@InsuredElseWhere", null);
            }
            int returnResult = 0;
            try
            {
                //if transaction required

                if (this.boolTransactionRequired)
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    if (returnResult > 0)
                    {
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                        objTransactionInfo.TRANS_TYPE_ID = 1;
                        objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                        objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objGeneralInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1565", "");// "Private passenger underwriting questions is added";
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                        returnResult = 1;
                    }
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                objDataWrapper.ClearParameteres();

                //Commit the transaction if all opeations are successfull.
                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                return returnResult;
            }
            catch (Exception ex)
            {
                //objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                throw (ex);
            }
            finally
            {
                //if(objDataWrapper != null) objDataWrapper.Dispose();
            }
            return 0;
        }
        public int Add1(Cms.Model.Application.ClsGeneralInfo objGeneralInfo, string strLOB)
        {
            string strStoredProc = "Proc_InsertApplicationGenInfo";
            DateTime RecordDate = DateTime.Now;

            //Set transaction label in the new object, if required
            if (this.boolTransactionRequired)
            {
                objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
            }

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
            //objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID,SqlDbType.Int ,ParameterDirection.Output ,5);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@PARENT_APP_VERSION_ID", objGeneralInfo.PARENT_APP_VERSION_ID);
            objDataWrapper.AddParameter("@APP_STATUS", objGeneralInfo.APP_STATUS);
            objDataWrapper.AddParameter("@APP_NUMBER", objGeneralInfo.APP_NUMBER);
            objDataWrapper.AddParameter("@APP_VERSION", objGeneralInfo.APP_VERSION);
            objDataWrapper.AddParameter("@APP_TERMS", objGeneralInfo.APP_TERMS);
            if (objGeneralInfo.APP_INCEPTION_DATE != DateTime.MinValue)
            {
                objDataWrapper.AddParameter("@APP_INCEPTION_DATE", objGeneralInfo.APP_INCEPTION_DATE);
            }

            objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", objGeneralInfo.APP_EFFECTIVE_DATE);
            objDataWrapper.AddParameter("@APP_EXPIRATION_DATE", objGeneralInfo.APP_EXPIRATION_DATE);
            objDataWrapper.AddParameter("@APP_LOB", objGeneralInfo.APP_LOB);
            objDataWrapper.AddParameter("@APP_SUBLOB", objGeneralInfo.APP_SUBLOB);
            if (objGeneralInfo.CSR == -1)
                objDataWrapper.AddParameter("@CSR", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@CSR", objGeneralInfo.CSR);

            objDataWrapper.AddParameter("@UNDERWRITER", objGeneralInfo.UNDERWRITER);
            objDataWrapper.AddParameter("@IS_UNDER_REVIEW", null);
            objDataWrapper.AddParameter("@APP_AGENCY_ID", objGeneralInfo.APP_AGENCY_ID);
            objDataWrapper.AddParameter("@IS_ACTIVE", objGeneralInfo.IS_ACTIVE);

            objDataWrapper.AddParameter("@CREATED_BY", objGeneralInfo.CREATED_BY);
            objDataWrapper.AddParameter("@CREATED_DATETIME", objGeneralInfo.CREATED_DATETIME);
            objDataWrapper.AddParameter("@MODIFIED_BY", null);
            objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);

            objDataWrapper.AddParameter("@COUNTRY_ID", objGeneralInfo.COUNTRY_ID);
            objDataWrapper.AddParameter("@STATE_ID", objGeneralInfo.STATE_ID);
            objDataWrapper.AddParameter("@PRODUCER", objGeneralInfo.PRODUCER);
            //			if(objGeneralInfo.YEAR_AT_CURR_RESI==Double.MinValue)
            //				objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI",System.DBNull.Value);
            //			else
            //				objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI",objGeneralInfo.YEAR_AT_CURR_RESI);
            //Nov,07,2005:Sumit Chhabra:Datatype for YEAR_AT_CURR_RESI being changed from double to int
            if (objGeneralInfo.YEAR_AT_CURR_RESI == Int32.MinValue)
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", objGeneralInfo.YEAR_AT_CURR_RESI);

            objDataWrapper.AddParameter("@YEARS_AT_PREV_ADD", objGeneralInfo.YEARS_AT_PREV_ADD);
            //billing information
            objDataWrapper.AddParameter("@DIV_ID", objGeneralInfo.DIV_ID);
            objDataWrapper.AddParameter("@DEPT_ID", objGeneralInfo.DEPT_ID);
            objDataWrapper.AddParameter("@PC_ID", objGeneralInfo.PC_ID);
            objDataWrapper.AddParameter("@BILL_TYPE_ID", objGeneralInfo.BILL_TYPE_ID);
            objDataWrapper.AddParameter("@COMPLETE_APP", objGeneralInfo.COMPLETE_APP);
            objDataWrapper.AddParameter("@PROPRTY_INSP_CREDIT", objGeneralInfo.PROPRTY_INSP_CREDIT);
            objDataWrapper.AddParameter("@INSTALL_PLAN_ID", objGeneralInfo.INSTALL_PLAN_ID);
            objDataWrapper.AddParameter("@CHARGE_OFF_PRMIUM", objGeneralInfo.CHARGE_OFF_PRMIUM);
            objDataWrapper.AddParameter("@IS_HOME_EMP", objGeneralInfo.IS_HOME_EMP);

            if (objGeneralInfo.RECEIVED_PRMIUM == Double.MinValue)
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", objGeneralInfo.RECEIVED_PRMIUM);
            if (objGeneralInfo.PROXY_SIGN_OBTAINED != int.MinValue) // proxy sign is set
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", objGeneralInfo.PROXY_SIGN_OBTAINED);
            }
            else
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", null);
            }
            if (objGeneralInfo.POLICY_TYPE == -1)
                objDataWrapper.AddParameter("@POLICY_TYPE", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@POLICY_TYPE", objGeneralInfo.POLICY_TYPE);
            objDataWrapper.AddParameter("@PIC_OF_LOC", objGeneralInfo.PIC_OF_LOC);
            objDataWrapper.AddParameter("@DOWN_PAY_MODE", objGeneralInfo.DOWN_PAY_MODE);

            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID, SqlDbType.Int, ParameterDirection.Output);


            int returnResult = 0;
            int APP_ID = 0;
            try
            {
                //if transaction required

                if (this.boolTransactionRequired)
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    if (returnResult > 0)
                    {
                        APP_ID = int.Parse(objSqlParameter.Value.ToString());
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        switch (BL_LANG_CULTURE)
                        {
                            case "pt-BR":
                                objTransactionInfo.TRANS_DESC = "Nova aplicação acrescentado";
                                objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\application.pt.resx");
                                break;
                            case "en-US":
                            default:
                                objTransactionInfo.TRANS_DESC = "New application added";
                                objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\application.en.resx");
                                break;
                        }

                        string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
                        //Remove the field "Property Inspection Credit Applies" for LOBs other than homeowner
                        if (objGeneralInfo.APP_LOB == "2" || objGeneralInfo.APP_LOB == "3" || objGeneralInfo.APP_LOB == "4" || objGeneralInfo.APP_LOB == "5")
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT']");

                        objTransactionInfo.TRANS_TYPE_ID = 1;
                        objTransactionInfo.APP_ID = APP_ID;
                        objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objGeneralInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1564", "");// "New application added";
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='Producer' and @NewValue='0']", "NewValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='APP_SUBLOB' and @NewValue='0']", "NewValue", "null");
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        //objTransactionInfo.CUSTOM_INFO		=	";LOB = " + strLOB;


                        objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                        returnResult = 1;
                    }
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    APP_ID = int.Parse(objSqlParameter.Value.ToString());//int.Parse(objDataWrapper.CommandParameters[1].Value.ToString());
                }

                objGeneralInfo.APP_ID = APP_ID;

                objDataWrapper.ClearParameteres();

                //Insert into Diary
                /*				Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();

                                ClsDiary objDiary =  = new ClsDiary();
				
                                objDiary.TransactionLogRequired = true;

                                PopulateToDoListObject(objToDoListInfo,objGeneralInfo);

                                objDiary.Add(objToDoListInfo,objDataWrapper);
                */

                //Commit the transaction if all opeations are successfull.
                if (strLOB.ToUpper().Trim() == "UMBRELLA")
                {
                    ClsUmbrellaCoverages objCoverage = new ClsUmbrellaCoverages();
                    objCoverage.SaveDefaultCoveragesApp(objDataWrapper, objGeneralInfo.CUSTOMER_ID, objGeneralInfo.APP_ID, objGeneralInfo.APP_VERSION_ID, 0);
                }

                #region Underwriting Tier
                if (strLOB.ToUpper().Trim() == "AUTOMOBILE" && objGeneralInfo.STATE_ID == 14)//Added STATE_ID,Charles, Itrack 6830, 6-Jan-09
                {
                    ClsUnderwritingTier objTier = new ClsUnderwritingTier();
                    objTier.UpdateUnderwritingTier(objGeneralInfo.CUSTOMER_ID, objGeneralInfo.APP_ID, objGeneralInfo.APP_VERSION_ID, "APP", objDataWrapper);
                }
                #endregion
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                return APP_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return 0;
        }

        /// <summary>
        /// Update Default Co-Insurer
        /// </summary>
        /// <param name="flag">Y - Add, N - Remove</param>
        /// <param name="objCoInsuranceInfo">Model Object</param>
        /// <returns></returns>
        /// Added by Charles on 7-Apr-10 for Co-Insurance Page
        public static int UpdateDefaultCoInsurer(ClsCoInsuranceInfo objCoInsuranceInfo, string strCarrier_Code)
        {
            int returnResult = 0;
            string strStoredProc = "Proc_UpdateDefaultCoInsurer";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@CARRIER_CODE", strCarrier_Code);
                objDataWrapper.AddParameter("@CUSTOMER_ID", objCoInsuranceInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objCoInsuranceInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objCoInsuranceInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@LEADER_FOLLOWER", objCoInsuranceInfo.LEADER_FOLLOWER);
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
            }
            catch
            { }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }

        /// <summary>
        /// Updates Default Broker in Renumeration Tab
        /// </summary>
        /// <param name="objnewPolicyRemunerationInfo">ClsPolicyRemunerationInfo Object</param>
        /// <param name="iAGENCY_ID">AGENCY_ID</param>
        /// <param name="iOLD_AGENCY_ID">OLD AGENCY ID</param>
        /// <returns></returns>
        /// Added by Charles on 8-Apr-10 for Renumeration Tab
        public static int UpdateDefaultBroker(ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo, int iAGENCY_ID, int iOLD_AGENCY_ID)
        {
            int returnResult = 0;
            string strStoredProc = "Proc_UpdateDefaultBroker";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@AGENCY_ID", iAGENCY_ID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", objnewPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue);
                objDataWrapper.AddParameter("@POLICY_ID", objnewPolicyRemunerationInfo.POLICY_ID.CurrentValue);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objnewPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue);
                objDataWrapper.AddParameter("@OLD_AGENCY_ID", iOLD_AGENCY_ID);

                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
            }
            catch
            { }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
            return returnResult;
        }


        /// <summary>
        /// Inserting new policy information
        /// </summary>
        /// <param name="objGeneralInfo">Model Object</param>
        /// <param name="strLOB">Line of Business</param>
        /// <returns></returns>
        /// Added by Charles on 25-Feb-2010 for Policy Page Implementation
        public int AddPolicy(ClsPolicyInfo objGeneralInfo, string strLOB)
        {
            string strStoredProc = "Proc_InsertPolicyGenInfo";
            DateTime RecordDate = DateTime.Now;

            //if (this.boolTransactionRequired)
            //{
            //    objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");
            //}

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@PARENT_APP_VERSION_ID", objGeneralInfo.PARENT_APP_VERSION_ID);
            objDataWrapper.AddParameter("@APP_STATUS", objGeneralInfo.APP_STATUS);
            objDataWrapper.AddParameter("@APP_NUMBER", objGeneralInfo.APP_NUMBER);
            objDataWrapper.AddParameter("@APP_VERSION", objGeneralInfo.APP_VERSION);
            objDataWrapper.AddParameter("@APP_TERMS", objGeneralInfo.APP_TERMS);
            if (objGeneralInfo.APP_INCEPTION_DATE != DateTime.MinValue)
            {
                objDataWrapper.AddParameter("@APP_INCEPTION_DATE", objGeneralInfo.APP_INCEPTION_DATE);
            }

            objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", objGeneralInfo.APP_EFFECTIVE_DATE);
            objDataWrapper.AddParameter("@APP_EXPIRATION_DATE", objGeneralInfo.APP_EXPIRATION_DATE);
            objDataWrapper.AddParameter("@APP_LOB", objGeneralInfo.POLICY_LOB);
            objDataWrapper.AddParameter("@APP_SUBLOB", objGeneralInfo.POLICY_SUBLOB);
            if (objGeneralInfo.CSR == -1)
                objDataWrapper.AddParameter("@CSR", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@CSR", objGeneralInfo.CSR);

            objDataWrapper.AddParameter("@UNDERWRITER", objGeneralInfo.UNDERWRITER);
            objDataWrapper.AddParameter("@IS_UNDER_REVIEW", null);
            objDataWrapper.AddParameter("@APP_AGENCY_ID", objGeneralInfo.AGENCY_ID);
            objDataWrapper.AddParameter("@IS_ACTIVE", objGeneralInfo.IS_ACTIVE);

            objDataWrapper.AddParameter("@CREATED_BY", objGeneralInfo.CREATED_BY);
            objDataWrapper.AddParameter("@CREATED_DATETIME", objGeneralInfo.CREATED_DATETIME);
            objDataWrapper.AddParameter("@MODIFIED_BY", null);
            objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);

            objDataWrapper.AddParameter("@COUNTRY_ID", objGeneralInfo.COUNTRY_ID);
            objDataWrapper.AddParameter("@STATE_ID", objGeneralInfo.STATE_ID);
            objDataWrapper.AddParameter("@PRODUCER", objGeneralInfo.PRODUCER);

            if (objGeneralInfo.YEAR_AT_CURR_RESI == Int32.MinValue)
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", objGeneralInfo.YEAR_AT_CURR_RESI);

            objDataWrapper.AddParameter("@YEARS_AT_PREV_ADD", objGeneralInfo.YEARS_AT_PREV_ADD);
            //billing information
            if (objGeneralInfo.DIV_ID == Int32.MinValue)
                objDataWrapper.AddParameter("@DIV_ID", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@DIV_ID", objGeneralInfo.DIV_ID);
            objDataWrapper.AddParameter("@DEPT_ID", objGeneralInfo.DEPT_ID);
            objDataWrapper.AddParameter("@PC_ID", objGeneralInfo.PC_ID);
            objDataWrapper.AddParameter("@BILL_TYPE_ID", objGeneralInfo.BILL_TYPE);
            objDataWrapper.AddParameter("@COMPLETE_APP", objGeneralInfo.COMPLETE_APP);
            objDataWrapper.AddParameter("@PROPRTY_INSP_CREDIT", objGeneralInfo.PROPRTY_INSP_CREDIT);
            objDataWrapper.AddParameter("@INSTALL_PLAN_ID", objGeneralInfo.INSTALL_PLAN_ID);
            objDataWrapper.AddParameter("@CHARGE_OFF_PRMIUM", objGeneralInfo.CHARGE_OFF_PRMIUM);
            objDataWrapper.AddParameter("@IS_HOME_EMP", objGeneralInfo.IS_HOME_EMP);

            if (objGeneralInfo.RECEIVED_PRMIUM == Double.MinValue)
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", objGeneralInfo.RECEIVED_PRMIUM);
            if (objGeneralInfo.PROXY_SIGN_OBTAINED != int.MinValue) // proxy sign is set
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", objGeneralInfo.PROXY_SIGN_OBTAINED);
            }
            else
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", null);
            }
            if (objGeneralInfo.POLICY_TYPE == "-1")
                objDataWrapper.AddParameter("@POLICY_TYPE", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@POLICY_TYPE", objGeneralInfo.POLICY_TYPE);
            objDataWrapper.AddParameter("@PIC_OF_LOC", objGeneralInfo.PIC_OF_LOC);
            objDataWrapper.AddParameter("@DOWN_PAY_MODE", objGeneralInfo.DOWN_PAY_MODE);

            objDataWrapper.AddParameter("@POLICY_CURRENCY", objGeneralInfo.POLICY_CURRENCY);
            objDataWrapper.AddParameter("@PAYOR", objGeneralInfo.PAYOR);
            objDataWrapper.AddParameter("@CO_INSURANCE", objGeneralInfo.CO_INSURANCE);

            objDataWrapper.AddParameter("@POLICY_LEVEL_COMISSION", objGeneralInfo.POLICY_LEVEL_COMISSION);
            objDataWrapper.AddParameter("@BILLTO", objGeneralInfo.BILLTO);
            objDataWrapper.AddParameter("@CONTACT_PERSON", objGeneralInfo.CONTACT_PERSON);

            //Added by Charles on 14-May-2010 for Policy Page Implementation
            objDataWrapper.AddParameter("@POLICY_LEVEL_COMM_APPLIES", objGeneralInfo.POLICY_LEVEL_COMM_APPLIES);
            objDataWrapper.AddParameter("@TRANSACTION_TYPE", objGeneralInfo.TRANSACTION_TYPE);
            objDataWrapper.AddParameter("@PREFERENCE_DAY", objGeneralInfo.PREFERENCE_DAY);
            objDataWrapper.AddParameter("@BROKER_REQUEST_NO", objGeneralInfo.BROKER_REQUEST_NO);
            objDataWrapper.AddParameter("@BROKER_COMM_FIRST_INSTM", objGeneralInfo.BROKER_COMM_FIRST_INSTM);
            objDataWrapper.AddParameter("@POLICY_DESCRIPTION", objGeneralInfo.POLICY_DESCRIPTION);
            //Added till here
	
            //Added by Agniswar for Singapore Implementation
            objDataWrapper.AddParameter("@BILLING_CURRENCY", objGeneralInfo.BILLING_CURRENCY);
            objDataWrapper.AddParameter("@FUND_TYPE", objGeneralInfo.FUND_TYPE);

            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID, SqlDbType.Int, ParameterDirection.Output);
            SqlParameter sPolicyID = (SqlParameter)objDataWrapper.AddParameter("@POLICY_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;
            int APP_ID = 0;
            try
            {
                if (this.boolTransactionRequired)
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    if (returnResult > 0)
                    {
                        APP_ID = int.Parse(objSqlParameter.Value.ToString());
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                        objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");

                        string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
                        //Remove the field "Property Inspection Credit Applies" for LOBs other than homeowner
                        if (objGeneralInfo.POLICY_LOB == "2" || objGeneralInfo.POLICY_LOB == "3" || objGeneralInfo.POLICY_LOB == "4" || objGeneralInfo.POLICY_LOB == "5")
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT']");

                        objTransactionInfo.TRANS_TYPE_ID = 124;
                        objTransactionInfo.POLICY_ID = int.Parse(sPolicyID.Value.ToString());
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objGeneralInfo.APP_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objGeneralInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC = "";//"New Application added";
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='Producer' and @NewValue='0']", "NewValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='APP_SUBLOB' and @NewValue='0']", "NewValue", "null");
                        objTransactionInfo.CHANGE_XML = strTranXML;

                        objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                        returnResult = 1;
                    }
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    APP_ID = int.Parse(objSqlParameter.Value.ToString());
                }

                objGeneralInfo.APP_ID = APP_ID;
                objGeneralInfo.POLICY_ID = int.Parse(sPolicyID.Value.ToString());

                #region Add Diary Entry
                String SubLine = String.Empty;
                String Notes = String.Empty;
                Int32 Module_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;
                Int32 ListTypeID = (int)ClsDiary.enumDiaryType.APPLICATION_CREATION;


                SubLine = FetchGeneralMessage("1316", "");
                Notes = FetchGeneralMessage("1317", "") + "(" + objGeneralInfo.APP_NUMBER.ToString() + " ) " + FetchGeneralMessage("1318", "");



                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                objModel.LISTTYPEID = ListTypeID;
                objModel.POLICY_ID = objGeneralInfo.POLICY_ID;
                objModel.POLICY_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                objModel.CUSTOMER_ID = objGeneralInfo.CUSTOMER_ID;
                objModel.APP_ID = objGeneralInfo.APP_ID;
                objModel.LOB_ID = int.Parse(objGeneralInfo.POLICY_LOB);
                objModel.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                objModel.FROMUSERID = objModel.CREATED_BY = objGeneralInfo.CREATED_BY;
                objModel.CREATED_DATETIME = DateTime.Now;
                objModel.RECDATE = DateTime.Now;
                objModel.LISTOPEN = "Y";
                objModel.SUBJECTLINE = SubLine;
                objModel.NOTE = Notes;
                objModel.MODULE_ID = Module_ID;
                objModel.APPLICATION_NUMBER = objGeneralInfo.APP_NUMBER;
                objModel.POLICY_NUMBER = objGeneralInfo.APP_NUMBER;

                objClsDiary.DiaryEntryfromSetup(objModel, objDataWrapper);
                #endregion

                /*
                int returnUWResult = CheckToAssignedUnderWriter(objGeneralInfo.CUSTOMER_ID, objGeneralInfo.APP_ID, objGeneralInfo.APP_VERSION_ID, objGeneralInfo.POLICY_LOB, objGeneralInfo.CREATED_BY, "SUBMIT", objDataWrapper);
                if (returnUWResult == -2)
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                    return returnUWResult;
                } 
                */

                objDataWrapper.ClearParameteres();

                //Commit the transaction if all opeations are successfull.
                //if (strLOB.ToUpper().Trim() == "UMBRELLA")
                //{
                //    ClsUmbrellaCoverages objCoverage = new ClsUmbrellaCoverages();
                //    objCoverage.SaveDefaultCoveragesPolicy(objDataWrapper, objGeneralInfo.CUSTOMER_ID, objGeneralInfo.POLICY_ID, objGeneralInfo.APP_VERSION_ID, 0);
                //}

                #region Underwriting Tier
                //if (strLOB.ToUpper().Trim() == "AUTOMOBILE" && objGeneralInfo.STATE_ID == 14)
                //{
                //    ClsUnderwritingTier objTier = new ClsUnderwritingTier();
                //    objTier.UpdateUnderwritingTier(objGeneralInfo.CUSTOMER_ID, objGeneralInfo.POLICY_ID, objGeneralInfo.APP_VERSION_ID, "POLICY", objDataWrapper);
                //}
                #endregion
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                return APP_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }


        /// <summary>
        /// Updates the form's modified value
        /// </summary>
        /// <param name="objOldCustomerInfo">model object having old information</param>
        /// <param name="objCustomerInfo">model object having new information(form control's value)</param>
        /// <returns>no. of rows updated (1 or 0)</returns>
        public int Update(ClsGeneralInfo objOldGeneralInfo, ClsGeneralInfo objGeneralInfo)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_UpdateApplicationGenInfo";
            DateTime RecordDate = DateTime.Now;
            string strTranXML;



            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@PARENT_APP_VERSION_ID", objGeneralInfo.PARENT_APP_VERSION_ID);
                objDataWrapper.AddParameter("@APP_STATUS", objGeneralInfo.APP_STATUS);
                objDataWrapper.AddParameter("@APP_NUMBER", objGeneralInfo.APP_NUMBER);
                objDataWrapper.AddParameter("@APP_VERSION", objGeneralInfo.APP_VERSION);
                objDataWrapper.AddParameter("@APP_TERMS", objGeneralInfo.APP_TERMS);
                if (objGeneralInfo.APP_INCEPTION_DATE != DateTime.MinValue)
                {
                    objDataWrapper.AddParameter("@APP_INCEPTION_DATE", objGeneralInfo.APP_INCEPTION_DATE);
                }
                objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", objGeneralInfo.APP_EFFECTIVE_DATE);
                objDataWrapper.AddParameter("@APP_EXPIRATION_DATE", objGeneralInfo.APP_EXPIRATION_DATE);
                objDataWrapper.AddParameter("@APP_LOB", objGeneralInfo.APP_LOB);
                objDataWrapper.AddParameter("@APP_SUBLOB", objGeneralInfo.APP_SUBLOB);
                objDataWrapper.AddParameter("@CSR", objGeneralInfo.CSR);
                objDataWrapper.AddParameter("@PRODUCER", objGeneralInfo.PRODUCER);
                objDataWrapper.AddParameter("@UNDERWRITER", objGeneralInfo.UNDERWRITER);
                objDataWrapper.AddParameter("@IS_UNDER_REVIEW", null);
                objDataWrapper.AddParameter("@APP_AGENCY_ID", objGeneralInfo.APP_AGENCY_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", objGeneralInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@MODIFIED_BY", objGeneralInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objGeneralInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@COUNTRY_ID", objGeneralInfo.COUNTRY_ID);
                objDataWrapper.AddParameter("@STATE_ID", objGeneralInfo.STATE_ID);
                //				if(objGeneralInfo.YEAR_AT_CURR_RESI==Double.MinValue)
                //					objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI",System.DBNull.Value);
                //				else
                //					objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI",objGeneralInfo.YEAR_AT_CURR_RESI);
                //Nov,07,2005:Sumit Chhabra:Datatype for YEAR_AT_CURR_RESI being changed from double to int
                if (objGeneralInfo.YEAR_AT_CURR_RESI == Int32.MinValue)
                    objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", objGeneralInfo.YEAR_AT_CURR_RESI);

                objDataWrapper.AddParameter("@YEARS_AT_PREV_ADD", objGeneralInfo.YEARS_AT_PREV_ADD);
                //billing information
                objDataWrapper.AddParameter("@DIV_ID", objGeneralInfo.DIV_ID);
                objDataWrapper.AddParameter("@DEPT_ID", objGeneralInfo.DEPT_ID);
                objDataWrapper.AddParameter("@PC_ID", objGeneralInfo.PC_ID);
                objDataWrapper.AddParameter("@BILL_TYPE", objGeneralInfo.BILL_TYPE_ID);
                objDataWrapper.AddParameter("@COMPLETE_APP", objGeneralInfo.COMPLETE_APP);
                objDataWrapper.AddParameter("@PROPRTY_INSP_CREDIT", objGeneralInfo.PROPRTY_INSP_CREDIT);
                objDataWrapper.AddParameter("@INSTALL_PLAN_ID", objGeneralInfo.INSTALL_PLAN_ID);
                objDataWrapper.AddParameter("@CHARGE_OFF_PRMIUM", objGeneralInfo.CHARGE_OFF_PRMIUM);
                objDataWrapper.AddParameter("@IS_HOME_EMP", objGeneralInfo.IS_HOME_EMP);

                if (objGeneralInfo.RECEIVED_PRMIUM == Double.MinValue)
                    objDataWrapper.AddParameter("@RECEIVED_PRMIUM", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@RECEIVED_PRMIUM", objGeneralInfo.RECEIVED_PRMIUM);

                if (objGeneralInfo.PROXY_SIGN_OBTAINED != int.MinValue) // proxy sign is set
                {
                    objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", objGeneralInfo.PROXY_SIGN_OBTAINED);
                }
                else
                {
                    objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", null);
                }
                if (objGeneralInfo.POLICY_TYPE == -1)
                    objDataWrapper.AddParameter("@POLICY_TYPE", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@POLICY_TYPE", objGeneralInfo.POLICY_TYPE);
                objDataWrapper.AddParameter("@DOWN_PAY_MODE", objGeneralInfo.DOWN_PAY_MODE);

                int returnResult = 0;

                if (TransactionRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                    strTranXML = objBuilder.GetTransactionLogXML(objOldGeneralInfo, objGeneralInfo);
                    //Nov 28,2005:Sumit Chhabra:Check added to prevent information being added to transaction log
                    //when no changes have actually taken place
                    if (strTranXML == "<LabelFieldMapping></LabelFieldMapping>")
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    else
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 2;
                        objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                        objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objGeneralInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1566", "");// "Application information is modified";
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT' and @NewValue='0']", "NewValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT' and @OldValue='0']", "OldValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PIC_OF_LOC' and @NewValue='0']", "NewValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PIC_OF_LOC' and @OldValue='0']", "OldValue", "null");


                        objTransactionInfo.CHANGE_XML = strTranXML;
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                //Commented By Ravindra(05-1-006) 
                //Update coverages if Effective date changes//////////////////
                /*if ( objOldGeneralInfo.APP_EFFECTIVE_DATE != objGeneralInfo.APP_EFFECTIVE_DATE )
                {
                    objDataWrapper.ClearParameteres();

                    objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
                    objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID);
                    objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralInfo.APP_VERSION_ID);

                    objDataWrapper.ExecuteNonQuery("Proc_ADJUST_APP_COVERAGES");
                }*/
                /////////////////////////////////////////////
                //Commented By Ravindra Ends Here 

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }

        public int Update(ClsGeneralInfo objOldGeneralInfo, ClsGeneralInfo objGeneralInfo, string strLOB)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_UpdateApplicationGenInfo";
            DateTime RecordDate = DateTime.Now;
            string strTranXML;



            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@PARENT_APP_VERSION_ID", objGeneralInfo.PARENT_APP_VERSION_ID);
                objDataWrapper.AddParameter("@APP_STATUS", objGeneralInfo.APP_STATUS);
                objDataWrapper.AddParameter("@APP_NUMBER", objGeneralInfo.APP_NUMBER);
                objDataWrapper.AddParameter("@APP_VERSION", objGeneralInfo.APP_VERSION);
                objDataWrapper.AddParameter("@APP_TERMS", objGeneralInfo.APP_TERMS);
                if (objGeneralInfo.APP_INCEPTION_DATE != DateTime.MinValue)
                {
                    objDataWrapper.AddParameter("@APP_INCEPTION_DATE", objGeneralInfo.APP_INCEPTION_DATE);
                }
                objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", objGeneralInfo.APP_EFFECTIVE_DATE);
                objDataWrapper.AddParameter("@APP_EXPIRATION_DATE", objGeneralInfo.APP_EXPIRATION_DATE);
                objDataWrapper.AddParameter("@APP_LOB", objGeneralInfo.APP_LOB);
                objDataWrapper.AddParameter("@APP_SUBLOB", objGeneralInfo.APP_SUBLOB);
                if (objGeneralInfo.CSR == -1)
                    objDataWrapper.AddParameter("@CSR", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@CSR", objGeneralInfo.CSR);
                objDataWrapper.AddParameter("@PRODUCER", objGeneralInfo.PRODUCER);
                objDataWrapper.AddParameter("@UNDERWRITER", objGeneralInfo.UNDERWRITER);
                objDataWrapper.AddParameter("@IS_UNDER_REVIEW", null);
                objDataWrapper.AddParameter("@APP_AGENCY_ID", objGeneralInfo.APP_AGENCY_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", objGeneralInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@MODIFIED_BY", objGeneralInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objGeneralInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@COUNTRY_ID", objGeneralInfo.COUNTRY_ID);
                objDataWrapper.AddParameter("@STATE_ID", objGeneralInfo.STATE_ID);
                //				if(objGeneralInfo.YEAR_AT_CURR_RESI==Double.MinValue)
                //					objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI",System.DBNull.Value);
                //				else
                //					objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI",objGeneralInfo.YEAR_AT_CURR_RESI);
                //Nov,07,2005:Sumit Chhabra:Datatype for YEAR_AT_CURR_RESI being changed from double to int
                if (objGeneralInfo.YEAR_AT_CURR_RESI == Int32.MinValue)
                    objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", objGeneralInfo.YEAR_AT_CURR_RESI);

                objDataWrapper.AddParameter("@YEARS_AT_PREV_ADD", objGeneralInfo.YEARS_AT_PREV_ADD);
                //billing information
                objDataWrapper.AddParameter("@DIV_ID", objGeneralInfo.DIV_ID);
                objDataWrapper.AddParameter("@DEPT_ID", objGeneralInfo.DEPT_ID);
                objDataWrapper.AddParameter("@PC_ID", objGeneralInfo.PC_ID);
                objDataWrapper.AddParameter("@BILL_TYPE_ID", objGeneralInfo.BILL_TYPE_ID);
                objDataWrapper.AddParameter("@COMPLETE_APP", objGeneralInfo.COMPLETE_APP);
                objDataWrapper.AddParameter("@PROPRTY_INSP_CREDIT", objGeneralInfo.PROPRTY_INSP_CREDIT);
                objDataWrapper.AddParameter("@INSTALL_PLAN_ID", objGeneralInfo.INSTALL_PLAN_ID);
                objDataWrapper.AddParameter("@CHARGE_OFF_PRMIUM", objGeneralInfo.CHARGE_OFF_PRMIUM);
                objDataWrapper.AddParameter("@IS_HOME_EMP", objGeneralInfo.IS_HOME_EMP);

                if (objGeneralInfo.RECEIVED_PRMIUM == Double.MinValue)
                    objDataWrapper.AddParameter("@RECEIVED_PRMIUM", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@RECEIVED_PRMIUM", objGeneralInfo.RECEIVED_PRMIUM);

                if (objGeneralInfo.PROXY_SIGN_OBTAINED != int.MinValue) // proxy sign is set
                {
                    objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", objGeneralInfo.PROXY_SIGN_OBTAINED);
                }
                else
                {
                    objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", null);
                }
                if (objGeneralInfo.POLICY_TYPE == -1)
                    objDataWrapper.AddParameter("@POLICY_TYPE", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@POLICY_TYPE", objGeneralInfo.POLICY_TYPE);
                objDataWrapper.AddParameter("@PIC_OF_LOC", objGeneralInfo.PIC_OF_LOC);
                objDataWrapper.AddParameter("@DOWN_PAY_MODE", objGeneralInfo.DOWN_PAY_MODE);

                int returnResult = 0;

                if (TransactionRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                    switch (BL_LANG_CULTURE)
                    {
                        case "pt-BR":
                            objTransactionInfo.TRANS_DESC = "Informações do aplicativo é modificado";
                            objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\application.pt.resx");
                            break;
                        case "en-US":
                        default:
                            objTransactionInfo.TRANS_DESC = "Application information is modified";
                            objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\application.en.resx");
                            break;
                    }
                    strTranXML = objBuilder.GetTransactionLogXML(objOldGeneralInfo, objGeneralInfo);
                    //Nov 28,2005:Sumit Chhabra:Check added to prevent information being added to transaction log
                    //when no changes have actually taken place
                    if (strTranXML == "<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    else
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 2;
                        objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                        objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objGeneralInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1566", "");//"Application information is modified";
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='CSR' and @OldValue='-1']", "OldValue", " ");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='Producer' and @OldValue='0']", "OldValue", " ");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='Producer' and @NewValue='0']", "NewValue", " ");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='CSR' and @NewValue='-1']", "NewValue", " ");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT' and @NewValue='0']", "NewValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT' and @OldValue='0']", "OldValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PIC_OF_LOC' and @NewValue='0']", "NewValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PIC_OF_LOC' and @OldValue='0']", "OldValue", "null");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT' and @NewValue='10964']", "NewValue", "No");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT' and @OldValue='10964']", "OldValue", "No");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PIC_OF_LOC' and @NewValue='10964']", "NewValue", "No");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PIC_OF_LOC' and @OldValue='10964']", "OldValue", "No");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT' and @NewValue='10963']", "NewValue", "Yes");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PROPRTY_INSP_CREDIT' and @OldValue='10963']", "OldValue", "Yes");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PIC_OF_LOC' and @NewValue='10963']", "NewValue", "Yes");
                        strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='PIC_OF_LOC' and @OldValue='10963']", "OldValue", "Yes");

                        objTransactionInfo.CHANGE_XML = strTranXML;
                        //objTransactionInfo.CUSTOM_INFO		=	";LOB = " + strLOB;
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                //Commented By Ravindra(05-1-006) 
                //Update coverages if Effective date changes//////////////////
                /*if ( objOldGeneralInfo.APP_EFFECTIVE_DATE != objGeneralInfo.APP_EFFECTIVE_DATE )
                {
                    objDataWrapper.ClearParameteres();

                    objDataWrapper.AddParameter("@CUSTOMER_ID",objGeneralInfo.CUSTOMER_ID);
                    objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID);
                    objDataWrapper.AddParameter("@APP_VERSION_ID",objGeneralInfo.APP_VERSION_ID);

                    objDataWrapper.ExecuteNonQuery("Proc_ADJUST_APP_COVERAGES");
                }*/
                /////////////////////////////////////////////

                if (objOldGeneralInfo.APP_LOB == LOB_WATERCRAFT || objOldGeneralInfo.APP_LOB == LOB_HOME)
                {
                    if (objOldGeneralInfo.APP_EFFECTIVE_DATE != objGeneralInfo.APP_EFFECTIVE_DATE)
                    {
                        objDataWrapper.ClearParameteres();
                        ClsWatercraftCoverages objWatCov = new ClsWatercraftCoverages();
                        objWatCov.UpdateCoveragesByRuleApp(objDataWrapper, objGeneralInfo.CUSTOMER_ID, objGeneralInfo.APP_ID, objGeneralInfo.APP_VERSION_ID, RuleType.AppDependent);

                    }
                }

                //bY PRAVESH FOR dEFAULT COVERAGE
                if (strLOB.ToUpper().Trim() == "UMBRELLA")
                {
                    ClsUmbrellaCoverages objCoverage = new ClsUmbrellaCoverages();
                    objCoverage.SaveDefaultCoveragesApp(objDataWrapper, objGeneralInfo.CUSTOMER_ID, objGeneralInfo.APP_ID, objGeneralInfo.APP_VERSION_ID, 0);
                }
                //END 
                this.UpdateEndorsementAttachments(objDataWrapper, objGeneralInfo.CUSTOMER_ID, objGeneralInfo.APP_ID, objGeneralInfo.APP_VERSION_ID);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }
        # endregion

        //THIS FUNCTION WILL update Endorsment Attachment
        public void UpdateEndorsementAttachments(DataWrapper objDataWrapper, int Customer_id, int App_id, int App_Version_id)
        {
            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@CUSTOMER_ID", Customer_id);
            objDataWrapper.AddParameter("@APP_ID", App_id);
            objDataWrapper.AddParameter("@APP_VERSION_ID", App_Version_id);

            int retVal = objDataWrapper.ExecuteNonQuery("proc_UpdateEndorsmentAttachmentAPP");
        }


        //Added by Ruchika Chauhan on 13-March-2012 for TFS Bug # 3635        
        public DataSet FillAccumulationDetails(int Policy_id, int Policy_version_id, int Cust_id)
        {
            string strStoredProc = "PROC_UPDATE_POL_ACCUMULATION_DETAILS";

            DataSet dsAccounts = new DataSet();

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            //objDataWrapper.AddParameter("@Acc_id", Acc_id, SqlDbType.Int);            
            objDataWrapper.AddParameter("@CUSTOMER_ID", Cust_id, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_version_id, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", Policy_id, SqlDbType.Int);

            //SqlParameter sqlFacultativeRI = (SqlParameter)objDataWrapper.AddParameter("@FACULTATIVE_RI", null, SqlDbType.Int, ParameterDirection.Output);
            //SqlParameter sqlOwnRetention = (SqlParameter)objDataWrapper.AddParameter("@OWN_RETENTION", null, SqlDbType.Int, ParameterDirection.Output);
            //SqlParameter sqlQuotaShare = (SqlParameter)objDataWrapper.AddParameter("@QUOTE_SHARE", null, SqlDbType.Int, ParameterDirection.Output);
            //SqlParameter sqlFirstSurplus = (SqlParameter)objDataWrapper.AddParameter("@FIRST_SURPLUS", null, SqlDbType.Int, ParameterDirection.Output);


            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);

            //FacultativeRI = sqlFacultativeRI.Value.ToString();
            //OwnRetention = sqlOwnRetention.Value.ToString();
            //QuotaShare = sqlQuotaShare.Value.ToString();
            //FirstSurplus = sqlFirstSurplus.Value.ToString();
          
            return dsAccounts;
        }



        //THIS FUNCTION WILL SET DEFAULT BILL TYPE IN DROP DOWN
        public static void GetBillType(DropDownList objDropDownList, int LOB_ID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOB_ID", LOB_ID);
            DataTable objDataTable = objDataWrapper.ExecuteDataSet("Proc_GetBillType").Tables[0];
            objDropDownList.DataSource = objDataTable;
            objDropDownList.DataTextField = "LOOKUP_VALUE_DESC";
            objDropDownList.DataValueField = "LOOKUP_UNIQUE_ID";
            objDropDownList.DataBind();
            if (LOB_ID == 1 || LOB_ID == 6)
                objDropDownList.SelectedIndex = 4;
            else
                objDropDownList.SelectedIndex = 2;
        }


        //THIS FUNCTION WILL NOT SELECT DEFAULT BILL TYPE IN DROP DOWN
        public static void GetBillType(DropDownList objDropDownList, int LOB_ID, int custId, string id, int versionID, string strMode)
        {
            GetBillType(objDropDownList, LOB_ID, custId, id, versionID, strMode, BL_LANG_ID.ToString());
        }
        public static void GetBillType(DropDownList objDropDownList, int LOB_ID, int custId, string id, int versionID, string strMode, string Lang_ID)
        {
            if (id == "NEW") id = "0";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOB_ID", LOB_ID);
            objDataWrapper.AddParameter("@CUSTOMER_ID", custId);
            objDataWrapper.AddParameter("@APP_POL_ID", id);
            objDataWrapper.AddParameter("@APP_POL_VERSION_ID", versionID);
            objDataWrapper.AddParameter("@LANG_ID", Lang_ID);
            objDataWrapper.AddParameter("@CALLED_FROM", strMode);
            DataTable objDataTable = objDataWrapper.ExecuteDataSet("Proc_GetBillType").Tables[0];
            objDropDownList.DataSource = objDataTable;
            objDropDownList.DataTextField = "LOOKUP_VALUE_DESC";
            objDropDownList.DataValueField = "LOOKUP_UNIQUE_ID";
            objDropDownList.DataBind();

            //Set the index to Insured Bill for watercraft LOB
            //if(LOB_ID==int.Parse(((int)enumLOB.BOAT).ToString()))
            //	objDropDownList.SelectedIndex=2;
            //Set Value of Combo By Value : 7 march 2008
            if (LOB_ID == int.Parse(((int)enumLOB.BOAT).ToString()))
            {
                ListItem li = objDropDownList.Items.FindByValue("8460"); //MNT ID  Insured Bill : 8460 
                if (li != null && li.Value != "")
                    objDropDownList.SelectedValue = li.Value;
            }
        }


        /// <summary>
        /// Get Bill Type depending on Product
        /// </summary>
        /// <param name="LOB_ID">Product ID</param>
        /// <param name="strMode">APP/POL</param>
        /// <returns>Bill Types DataTable</returns>
        /// Added by Charles on 14-June-10 for Quick App
        public static DataTable GetBillType(int LOB_ID, string strMode)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@LOB_ID", LOB_ID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", -1);
                objDataWrapper.AddParameter("@APP_POL_ID", -1);
                objDataWrapper.AddParameter("@APP_POL_VERSION_ID", -1);
                objDataWrapper.AddParameter("@CALLED_FROM", strMode);
                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
                DataTable objDataTable = objDataWrapper.ExecuteDataSet("Proc_GetBillType").Tables[0];

                return objDataTable;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region Calling and Creating Input XML For Application [From SPs]
        /// <summary>
        /// Overloaded function for creating the Input XML
        /// </summary>
        /// <returns></returns>
        public string GetInputXML(int customerID, int appID, int appVersionID)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            //string		strStoredProcForHO	=		"Proc_GetRatingInformationForHO";			 
            string strStoredProcForLOB = "Proc_GetApplicationLOB";
            string strStoredProcForHO = "Proc_GetHomeowner3Rule";
            //string strStoredProcForPP = "Proc_GetRatingInformationForPP";
            //string strStoredProcForMot = "Proc_GetRatingInformationForMOT";
            // string strStoredProcForWAT = "Proc_GetRatingInformationForWAT";
            // string strStoredProcForRD = "Proc_GetRatingInformationForRD";
            // string strStoredProcForUMB = "Proc_GetRatingInformationForUMB";

            DateTime RecordDate = DateTime.Now;
            DataSet dsLOB = new DataSet();
            string LOBID = "0";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                // get the application LOB
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                dsLOB = objDataWrapper.ExecuteDataSet(strStoredProcForLOB);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                LOBID = dsLOB.Tables[0].Rows[0][0].ToString();
                objDataWrapper.ClearParameteres();
                string inputXML = "";
                if (LOBID != "0")
                {
                    // switch case on the basis of the lob
                    switch (LOBID)
                    {
                        case LOB_HOME:
                            // Get the Dwelling ids against the customerID, appID, appVersionID
                            string dwellingIDs = ClsDwellingDetails.GetDwellingID(customerID, appID, appVersionID);
                            //If no dewlling found
                            if (dwellingIDs != "-1")
                            {
                                string[] dwellingIDAndAddress = new string[0];
                                dwellingIDAndAddress = dwellingIDs.Split('~');


                                inputXML = "";
                                // Run a loop to get the inputXML for all the dwellings
                                for (int iCounter = 0; iCounter < dwellingIDAndAddress.Length; iCounter++)
                                {
                                    string[] dwellingDetail = dwellingIDAndAddress[iCounter].Split('^');
                                    objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                                    objDataWrapper.AddParameter("@APPID", appID);
                                    objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                                    objDataWrapper.AddParameter("@DWELLINGID", dwellingDetail[0]);
                                    DataSet dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO);
                                    objDataWrapper.ClearParameteres();

                                    string strReturnXML = dsTempXML.GetXml();
                                    strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                                    strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                                    strReturnXML = strReturnXML.Replace("<Table>", "<DWELLINGDETAILS ID= '" + dwellingDetail[0] + "' ADDRESS='" + EncodeXMLCharacters(dwellingDetail[1]) + "'>");
                                    strReturnXML = strReturnXML.Replace("</Table>", "</DWELLINGDETAILS>");

                                    inputXML = inputXML + strReturnXML;

                                }
                                //inputXML = "<INPUTXML>"+inputXML+"</INPUTXML>";
                                inputXML = inputXML;
                            }
                            else
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'No Dwelling Found'/>";
                            }
                            break;
                        case LOB_PRIVATE_PASSENGER:
                            inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Private Passenger quotes'/>";
                            break;
                        case LOB_MOTORCYCLE:
                            inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Motorcycle quotes'/>";
                            break;
                        case LOB_WATERCRAFT:
                            inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Watercraft quotes'/>";
                            break;
                        case LOB_RENTAL_DWELLING:
                            inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Rental Dwelling quotes'/>";
                            break;
                        case LOB_UMBRELLA:
                            inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Umbrella quotes'/>";
                            break;
                        default:
                            break;
                    }

                }
                return inputXML;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }

            }
        }

        public string GetInputXML(int customerID, int appID, int appVersionID, string LOBID)
        {
            return GetInputXML(customerID, appID, appVersionID, LOBID, null);
        }

        /// <summary>
        /// Overloaded function for creating the Input XML
        /// </summary>
        /// <returns></returns>
        public string GetInputXML(int customerID, int appID, int appVersionID, string LOBID, DataWrapper objDataWrapper)
        {

            DateTime RecordDate = DateTime.Now;
            try
            {
                string inputXML = "";
                if (LOBID != "0")
                {
                    // switch case on the basis of the lob
                    switch (LOBID)
                    {
                        case LOB_HOME:
                            inputXML = FetchHomeownersInputXML(customerID, appID, appVersionID);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'No Dwelling Found'/>";
                            }
                            break;
                        case LOB_PRIVATE_PASSENGER:

                            inputXML = FetchPrivatePassengerInputXML(customerID, appID, appVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Private Passenger quotes'/>";
                            }
                            break;
                        case LOB_MOTORCYCLE:
                            inputXML = FetchMotorcyclerInputXML(customerID, appID, appVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Motorcycle quotes'/>";
                            }
                            break;
                        case LOB_WATERCRAFT:
                            inputXML = FetchWatercraftInputXML(customerID, appID, appVersionID);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Watercraft quotes'/>";
                            }
                            break;
                        case LOB_RENTAL_DWELLING:
                            inputXML = FetchRentalDwellingInputXML(customerID, appID, appVersionID);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Rental Dwelling quotes'/>";
                            }
                            break;
                        case LOB_UMBRELLA:
                            inputXML = FetchUmbrellaInputXML(customerID, appID, appVersionID);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Umbrella quotes'/>";
                            }
                            break;
                        case LOB_AVIATION:
                            inputXML = FetchAviationInputXML(customerID, appID, appVersionID);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Aviation quotes'/>";
                            }
                            break;
                        case LOB_MOTOR:
                            inputXML = "";
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Motor quotes'/>";
                            }
                            break;
                        default:
                            break;
                    }

                }
                return inputXML;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }

        private string FetchHomeownersInputXML(int customerID, int appID, int appVersionID)
        {
            DataWrapper objDataWrapper;
            string strStoredProcForHO = "Proc_GetRatingInformationFor_HO";
            string strStoredProcForHO_RecreationalVehicles = "Proc_GetRatingInformationForRV";
            string strStoredProcForHO_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo";


            try
            {
                // Get the Dwelling ids against the customerID, appID, appVersionID
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                string dwellingIDs = ClsDwellingDetails.GetDwellingID(customerID, appID, appVersionID);
                StringBuilder returnString = new StringBuilder();
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE>");

                //If no dewlling found
                if (dwellingIDs != "-1")
                {
                    string[] dwellingIDAndAddress = new string[0];
                    dwellingIDAndAddress = dwellingIDs.Split('~');

                    // Run a loop to get the inputXML for all the dwellings
                    for (int iCounter = 0; iCounter < dwellingIDAndAddress.Length; iCounter++)
                    {
                        string[] dwellingDetail = dwellingIDAndAddress[iCounter].Split('^');
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingDetail[0]);
                        DataSet dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO);
                        objDataWrapper.ClearParameteres();


                        //string strCoverages		=	dsTempXML.Tables[1].Rows[0][0].ToString();;
                        string strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        string strReturnXML = strOtherColumns;


                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_VALUE>", "");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_VALUE>", "");

                        //get the policy type.
                        string tempInput = strReturnXML.ToString();
                        XmlDocument docTemp = new XmlDocument();
                        docTemp.LoadXml(tempInput);
                        string productType = docTemp.DocumentElement.SelectSingleNode("PRODUCTNAME").InnerXml + ' ' + docTemp.DocumentElement.SelectSingleNode("PRODUCT_PREMIER").InnerXml;


                        //
                        strReturnXML = strReturnXML.Replace("<Table>", "<DWELLINGDETAILS ID= '" + dwellingDetail[0] + "' ADDRESS= '" + EncodeXMLCharacters(dwellingDetail[1]) + "' POLICYTYPE='" + productType + "'>");
                        strReturnXML = strReturnXML.Replace("</Table>", "</DWELLINGDETAILS>");





                        returnString.Append(strReturnXML);
                    }
                }

                //  For recreational Vehicles
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                string recreationVehiclesIDs = ClsHomeRecrVehicles.GetAppRecreationVehicleIDs(customerID, appID, appVersionID);
                StringBuilder returnRVString = new StringBuilder();
                returnRVString.Remove(0, returnRVString.Length);
                returnRVString.Append("<RECREATIONVEHICLES>");

                //If no RVs found
                if (recreationVehiclesIDs != "-1")
                {
                    string[] recreationVehiclesID = new string[0];
                    recreationVehiclesID = recreationVehiclesIDs.Split('^');

                    // Run a loop to get the inputXML for all the dwellings
                    for (int iRVCounter = 0; iRVCounter < recreationVehiclesID.Length; iRVCounter++)
                    {

                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@REC_VEHICLE_ID", recreationVehiclesID[iRVCounter]);
                        DataSet dsTempRVXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_RecreationalVehicles);
                        objDataWrapper.ClearParameteres();
                        string strRVColumns = ClsCommon.GetXML(dsTempRVXML.Tables[0]);

                        strRVColumns = strRVColumns.Replace("<NewDataSet>", "");
                        strRVColumns = strRVColumns.Replace("</NewDataSet>", "");
                        returnRVString.Append(strRVColumns);
                        returnRVString = returnRVString.Replace("<Table>", "<RECREATIONVEHICLE ID= '" + recreationVehiclesID[iRVCounter] + "'>");
                        returnRVString = returnRVString.Replace("</Table>", "</RECREATIONVEHICLE>");
                    }
                }
                returnRVString.Append("</RECREATIONVEHICLES>");
                returnString.Append(returnRVString.ToString());

                /*Get Primary Applicant Info*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "APP");
                DataSet dsAppTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                string strAppReturnXML = dsAppTempXML.GetXml();
                strAppReturnXML = strAppReturnXML.Replace("<NewDataSet>", "");
                strAppReturnXML = strAppReturnXML.Replace("</NewDataSet>", "");
                strAppReturnXML = strAppReturnXML.Replace("<Table>", "");
                strAppReturnXML = strAppReturnXML.Replace("</Table>", "");
                strAppReturnXML = strAppReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strAppReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*Get Primary Applicant Info*/



                returnString.Append("</QUICKQUOTE>");
                return returnString.ToString();
            }
            catch
            { }
            finally
            { }
            return "";

        }

        public static string FetchDriverApplicationInputXML(int customerID, int appID, int appVersionID)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForAuto_DriverDetail = "Proc_Get_Application_Vehicle_Information";

            string strReturnXML = "";

            string strOutXml = returnString.ToString();


            DataSet dsTempXML;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE>");

                // Get the app details
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_DriverDetail);
                objDataWrapper.ClearParameteres();

                strReturnXML = dsTempXML.GetXml();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("</LOBNAME>", "</LOBNAME><POLICY>");
                strReturnXML = strReturnXML.Replace("</QUOTEEFFDATE>", "</QUOTEEFFDATE></POLICY><VEHICLES><VEHICLE>");
                strReturnXML = strReturnXML.Replace("</VEHICLETYPEUSE>", "</VEHICLETYPEUSE></VEHICLE></VEHICLES></QUICKQUOTE>");
                returnString.Append(strReturnXML);

                return returnString.ToString();

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }


        }


        //Function Added by Sibin on 2 Feb 2009 for Itrack Issue 5381
        public static string FetchDriverPolicyInputXML(int customerID, int PolicyID, int PolicyVersionID)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForAuto_PolicyDriverDetail = "Proc_Get_Policy_Vehicle_Information";

            string strReturnXML = "";

            string strOutXml = returnString.ToString();


            DataSet dsTempXML;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE>");

                // Get the app details
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", PolicyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_PolicyDriverDetail);
                objDataWrapper.ClearParameteres();

                strReturnXML = dsTempXML.GetXml();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("</LOBNAME>", "</LOBNAME><POLICY>");
                strReturnXML = strReturnXML.Replace("</QUOTEEFFDATE>", "</QUOTEEFFDATE></POLICY><VEHICLES><VEHICLE>");
                strReturnXML = strReturnXML.Replace("</VEHICLETYPEUSE>", "</VEHICLETYPEUSE></VEHICLE></VEHICLES></QUICKQUOTE>");
                returnString.Append(strReturnXML);

                return returnString.ToString();

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }


        }

        public static string FetchPrivatePassengerInputXML(int customerID, int appID, int appVersionID)
        {
            return FetchPrivatePassengerInputXML(customerID, appID, appVersionID);
        }
        public static string FetchPrivatePassengerInputXML(int customerID, int appID, int appVersionID, DataWrapper objDataWrapper)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForAuto_DriverComponent = "Proc_GetRatingInformationForAuto_DriverComponent";
            string strStoredProcForAuto_VehicleComponent = "Proc_GetRatingInformationForAuto_VehicleComponent";
            string strStoredProcForAuto_VehicleCovgComponent = "Proc_GetRatingInformationForAuto_VehicleCovgComponent";
            string strStoredProcForAuto_ViolationsComponent = "Proc_GetRatingInformationForAuto_DriverViolationComponent";
            string strStoredProcForAuto_AppComponent = "Proc_GetRatingInformationForAuto_AppComponent";
            string strStoredProcForAuto_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo";
            string strReturnXML = "", strAssidrivers = "";
            int intassidriveVio = 0, intassidriverAcci = 0;
            ClsQuickQuote objQuickQuote = new ClsQuickQuote();
            // 
            string[] assinOlddriverID = new string[0];
            string strpolicy;
            string strMVRPoints = "";
            int violationPoints = 0;
            int accidentPoints = 0;

            DataSet dsTempXML;
            XmlDocument xmlDocViolAcc = new XmlDocument();
            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_AppComponent);
                objDataWrapper.ClearParameteres();

                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");

                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");

                returnString.Append(strReturnXML);
                //Assigning the policy node  : PK
                strpolicy = strReturnXML;
                //End 
                /*Get Primary Applicant Info*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "APP");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_PrimaryApplicants);
                objDataWrapper.ClearParameteres();

                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");

                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*Get Primary Applicant Info*/

                returnString.Append("</POLICY>");
                strReturnXML = "";


                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the vehicle details for each vehicle
                returnString.Append("<VEHICLES>");
                string vehicleIDs = ClsGeneralInformation.GetVehicleIDs(customerID, appID, appVersionID, objDataWrapper);
                if (vehicleIDs != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = vehicleIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<VEHICLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_VehicleComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");

                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");

                        //Add node for each vehicle  for count
                        int rowID = iCounter + 1;
                        //string strVioDrvID = "";
                        string vehicleRowIDNode = "<VEHICLEROWID>" + rowID.ToString() + "</VEHICLEROWID>";
                        returnString.Append(vehicleRowIDNode);
                        returnString.Append(strReturnXML);

                        //Get coverages
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_VehicleCovgComponent);
                        objDataWrapper.ClearParameteres();

                        if (dsTempXML.Tables[0] != null && dsTempXML.Tables[0].Rows.Count > 0)
                        {
                            strReturnXML = dsTempXML.Tables[0].Rows[0][0].ToString();
                            strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                            returnString.Append(strReturnXML);
                        }
                        //string setvalue = "";
                        string AssinegDriverId = ClsDriverDetail.GetAssinedDriverId(customerID, appID, appVersionID, int.Parse(vehicleID[iCounter]), objDataWrapper, "APP");
                        strAssidrivers = strAssidrivers + "^" + AssinegDriverId;
                        if (AssinegDriverId != "-1")
                        {
                            string[] assindriverID = new string[0];
                            assindriverID = AssinegDriverId.Split('^');
                            for (int iAssCounter = 0; iAssCounter < assindriverID.Length; iAssCounter++)
                            {
                                DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID, appID, appVersionID, int.Parse(assindriverID[iAssCounter].ToString()), "APP", objDataWrapper);
                                if (dtMvr != null && dtMvr.Rows.Count > 0)
                                {
                                    intassidriveVio = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                    intassidriverAcci = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                                }
                                returnString.Append("<ASSIGNDRIVIOACCPNT" + " " + "ID ='" + assindriverID[iAssCounter].ToString() + "'><SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS></ASSIGNDRIVIOACCPNT>");
                            }
                            // if  driver id found in old assign driver array
                            /*if(assinOlddriverID.Length > 0)
                                {
										
                                    foreach(string assin in assinOlddriverID)
                                    {
                                        // if driver's point have been charged for any vehicle
                                        if(assin == assindriverID[iAssCounter].ToString())
                                        {
                                            setvalue ="false";
                                        }
										
                                    }
                                    if(setvalue=="false")
                                    {
                                        strVioDrvID="";
                                    }
                                    else
                                    {
                                        strVioDrvID=assindriverID[iAssCounter].ToString();
                                    }
                                    // if driver's point did not charged for any vehicle
                                    if(strVioDrvID!="")
                                    {
                                        DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID,appID,appVersionID,int.Parse(strVioDrvID),"APP",objDataWrapper);
																		
                                    if(dtMvr!=null && dtMvr.Rows.Count>0)	
                                        {
                                            intassidriveVio = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                            intassidriverAcci = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                                        }														
                                        returnString.Append("<ASSIGNDRIVIOACCPNT" +" " + "ID ='" + assindriverID[iAssCounter].ToString() + "'><SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS></ASSIGNDRIVIOACCPNT>");
											
                                    }
                                }
                                // if no driver id found in old assign driver array
                                else
                                {
                                    DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID,appID,appVersionID,int.Parse(assindriverID[iAssCounter].ToString()),"APP",objDataWrapper);
                                    if(dtMvr!=null && dtMvr.Rows.Count>0)	
                                    {
                                        intassidriveVio = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                        intassidriverAcci = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                                    }														
                                    returnString.Append("<ASSIGNDRIVIOACCPNT" +" " + "ID ='" + assindriverID[iAssCounter].ToString() + "'><SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS></ASSIGNDRIVIOACCPNT>");
										
                                }
                                setvalue="";
                            }
                            assinOlddriverID = strAssidrivers.Split('^');
                        */
                        }
                        returnString.Append("<SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS>");
                        returnString.Append("</VEHICLE>");

                    }
                    intassidriveVio = 0;
                    intassidriverAcci = 0;
                }
                returnString.Append("</VEHICLES>");

                //get the driver details for each driver
                returnString.Append("<DRIVERS>");
                string driverIDs = ClsDriverDetail.GetDriverIDs(customerID, appID, appVersionID, objDataWrapper);
                if (driverIDs != "-1")
                {
                    string[] driverID = new string[0];
                    driverID = driverIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        returnString.Append("<DRIVER ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_DriverComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                        // LOAD XML TO FETCH SUM OF VIOLATION AND ACCIDENT POINT
                        xmlDocViolAcc.LoadXml(strReturnXML);

                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                        returnString.Append(strReturnXML);
                        //for each driver also get the violation detials (if any)
                        returnString.Append("<VIOLATIONS>");
                        string violationIDs = ClsDriverDetail.GetViolationIDs(customerID, appID, appVersionID, int.Parse(driverID[iCounter].ToString()), objDataWrapper);

                        int sumofViolationPoints = 0, sumofAccidentPoints = 0, sumofMvrPoints = 0, MvrPoints = 0; violationPoints = 0; accidentPoints = 0;//Set Violation Variables
                        StringBuilder strViolationNodes = new StringBuilder(); //For Driver Violations .

                        if (violationIDs != "-1")
                        {
                            string[] violationID = new string[0];
                            violationID = violationIDs.Split('^');

                            strViolationNodes.Remove(0, strViolationNodes.Length);

                            // Run a loop to get the inputXML for each vehicleID
                            for (int iCounterForViolations = 0; iCounterForViolations < violationID.Length; iCounterForViolations++)
                            {
                                //For just Violation Nodes
                                strViolationNodes.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>");
                                //End		
                                returnString.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>");
                                objDataWrapper.ClearParameteres();
                                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                                objDataWrapper.AddParameter("@APPID", appID);
                                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                                objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                                //objDataWrapper.AddParameter("@VIOLATIONID",violationID[iCounterForViolations]);	
                                //Modified it to @APP_MVR_ID from @VIOLATIONID
                                objDataWrapper.AddParameter("@APP_MVR_ID", violationID[iCounterForViolations]);

                                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_ViolationsComponent);
                                objDataWrapper.ClearParameteres();

                                strReturnXML = dsTempXML.GetXml();
                                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("<Table>", "");
                                strReturnXML = strReturnXML.Replace("</Table>", "");
                                //Appending the Violations 
                                strViolationNodes.Append(strReturnXML);
                                returnString.Append(strReturnXML);
                                //End 
                                returnString.Append("</VIOLATION>");
                                //Appending the Violations 
                                strViolationNodes.Append("</VIOLATION>");
                                //End 
                            }
                            //Set the values of the variables Calling method to Calculate points:
                            /*string strMVRPointsForSurchargeCyclXML = "<ViolationsMVRPoints>" + strpolicy + "<violations>" + strViolationNodes.ToString() +  "</violations>" + "</ViolationsMVRPoints>";
                            ClsQuickQuote ClsQQobj = new ClsQuickQuote();
                            strMVRPoints = ClsQQobj.GetMVRPointsForSurcharge(strMVRPointsForSurchargeCyclXML,"AUTOP");*/
                            //							ClsQuickQuote objQuickQuote = new ClsQuickQuote();
                            //							DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID,appID,appVersionID,int.Parse(driverID[iCounter].ToString()),"APP",objDataWrapper);
                            ////							if(dtMvr!=null && dtMvr.Rows.Count>0)	
                            //							{
                            //								violationPoints = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                            //								accidentPoints = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                            //							}					

                            strMVRPoints = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() + "</SUMOFACCIDENTPOINTS></POINTS>";
                            //End 
                        }
                        else
                        {
                            //if strViolation is blank (No Violations Selected //
                            strMVRPoints = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS></POINTS>";
                        }
                        if ((xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "PPA^PRINCIPAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "OPA^OCCASIONAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "YOPA^OCCASIONAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "YPPA^PRINCIPAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "NR^NR"))
                        {
                            DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID, appID, appVersionID, int.Parse(driverID[iCounter].ToString()), "APP", objDataWrapper);
                            if (dtMvr != null && dtMvr.Rows.Count > 0)
                            {
                                MvrPoints = int.Parse(dtMvr.Rows[0]["MVR_POINTS"].ToString());
                                violationPoints = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                accidentPoints = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                            }
                        }
                        strMVRPoints = "<POINTS><MVR>" + MvrPoints.ToString() + "</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() + "</SUMOFACCIDENTPOINTS></POINTS>";

                        returnString.Append("</VIOLATIONS>");
                        //**
                        //Set the nodes of sum of violation n accident points for each driver
                        XmlDocument PointsDoc = new XmlDocument();
                        PointsDoc.LoadXml(strMVRPoints);
                        XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
                        if (PointNode != null)
                        {

                            sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
                            sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                            sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());

                            returnString.Append("<MVR>");
                            returnString.Append(sumofMvrPoints);
                            returnString.Append("</MVR>");

                            returnString.Append("<SUMOFVIOLATIONPOINTS>");
                            returnString.Append(sumofViolationPoints);
                            returnString.Append("</SUMOFVIOLATIONPOINTS>");

                            returnString.Append("<SUMOFACCIDENTPOINTS>");
                            returnString.Append(sumofAccidentPoints);
                            returnString.Append("</SUMOFACCIDENTPOINTS>");

                        }
                        //END
                        //**

                        returnString.Append("</DRIVER>");

                    }
                }
                returnString.Append("</DRIVERS>");

                returnString.Append("</QUICKQUOTE>");
                //**Start 
                //*****Loading Documnet and Setting the VIOLATION POINTS at VEHICLE LEVEL : 21 April 2006
                //**Loading the Appended XML form Procedures and Appending and Setting the Violation NODES**//
                string strOutXml = returnString.ToString();
                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                int mvrPoints = 0, sumOFviolations = 0, sumOfAccidents = 0;
                foreach (XmlNode VehicleVioNode in returnXMLDoc.SelectNodes("//VEHICLES/*"))
                {
                    int VehID = int.Parse(VehicleVioNode.Attributes["ID"].Value.ToString().Trim());
                    mvrPoints = 0; sumOFviolations = 0; sumOfAccidents = 0;
                    foreach (XmlNode DriverVioNode in returnXMLDoc.SelectNodes("//DRIVERS/*"))
                    {
                        if (DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString() != "" && DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR") != null && Convert.ToInt32(DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString()) == VehID)
                        {
                            mvrPoints += int.Parse(DriverVioNode.SelectSingleNode("MVR").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("MVR").InnerText.ToString());
                            sumOFviolations += int.Parse(DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                            sumOfAccidents += int.Parse(DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                        }
                    }

                    //**Creating New Nodes for VIOLATIONS (CYCL)**//
                    XmlElement mvrNode = returnXMLDoc.CreateElement("MVR");
                    XmlElement vioNode = returnXMLDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                    XmlElement accidentNode = returnXMLDoc.CreateElement("SUMOFACCIDENTPOINTS");
                    //Set the text to node
                    XmlText mvrtext = returnXMLDoc.CreateTextNode(mvrPoints.ToString());
                    XmlText viotext = returnXMLDoc.CreateTextNode(sumOFviolations.ToString());
                    XmlText accidenttext = returnXMLDoc.CreateTextNode(sumOfAccidents.ToString());
                    //Append the nodes 
                    //VehicleVioNode.AppendChild(mvrNode);
                    //VehicleVioNode.AppendChild(vioNode);
                    //VehicleVioNode.AppendChild(accidentNode);
                    //Save the value of the fields into the nodes
                    //mvrNode.AppendChild(mvrtext);
                    //vioNode.AppendChild(viotext);
                    //accidentNode.AppendChild(accidenttext);

                    strOutXml = returnXMLDoc.OuterXml;

                }

                //**END
                //return returnString.ToString();
                return strOutXml.ToString();

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }


        }
        ///<summary>
        ///used to genrate aviation lob rate
        ///</summary>
        ////////////////////////////////////////////////////
        public static string FetchPolicyAviationInputXML(int customerID, int polID, int polVersionID)
        {

            return FetchPolicyAviationInputXML(customerID, polID, polVersionID, null);
        }
        public static string FetchPolicyAviationInputXML(int customerID, int polID, int polVersionID, DataWrapper objDataWrapper)
        {
            // code to be modified cusrrently fetchibng App inputxml	
            DataSet ds = (new ClsGeneralInformation()).GetPolicyDetails(customerID, 0, 0, polID, polVersionID);
            int appID = int.Parse(ds.Tables[0].Rows[0]["APP_ID"].ToString());
            int appVersionID = int.Parse(ds.Tables[0].Rows[0]["APP_VERSION_ID"].ToString());
            return FetchAviationInputXML(customerID, appID, appVersionID);
        }
        public static string FetchAviationInputXML(int customerID, int appID, int appVersionID)
        {
            return FetchAviationInputXML(customerID, appID, appVersionID, null);
        }
        public static string FetchAviationInputXML(int customerID, int appID, int appVersionID, DataWrapper objDataWrapper)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForAviation_VehicleComponent = "Proc_Get_RateinformationforAviation";
            string strStoredProcForAviation_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo";
            string strStoredProcForAuto_AppComponent = "Proc_GetRatingInformationForAuto_AppComponent";
            string strReturnXML = "", strOutXml = "";//strAssidrivers = "",
            int intassidriveVio = 0, intassidriverAcci = 0;
            ClsQuickQuote objQuickQuote = new ClsQuickQuote();
            // 
            string[] assinOlddriverID = new string[0];
            string strpolicy;
            //string strMVRPoints = "";
            //int violationPoints = 0;
            //int accidentPoints = 0;

            DataSet dsTempXML;
            XmlDocument xmlDocViolAcc = new XmlDocument();
            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_AppComponent);
                objDataWrapper.ClearParameteres();

                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");

                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");

                returnString.Append(strReturnXML);
                //Assigning the policy node  : PK
                strpolicy = strReturnXML;
                //End 
                /*Get Primary Applicant Info*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "APP");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAviation_PrimaryApplicants);
                objDataWrapper.ClearParameteres();

                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");

                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*Get Primary Applicant Info*/

                returnString.Append("</POLICY>");
                strReturnXML = "";


                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the vehicle details for each vehicle
                returnString.Append("<AIRCRAFTS>");
                string vehicleIDs = ClsGeneralInformation.GetAviationVehicleIDs(customerID, appID, appVersionID, objDataWrapper);
                if (vehicleIDs != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = vehicleIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<AIRCRAFT ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@AIRCRAFTID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAviation_VehicleComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        returnString.Append(strReturnXML);
                        returnString.Append("</AIRCRAFT>");

                    }
                    intassidriveVio = 0;
                    intassidriverAcci = 0;
                }
                returnString.Append("</AIRCRAFTS>");
                returnString.Append("</QUICKQUOTE>");
                strOutXml = returnString.ToString();
                return strOutXml.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        /// <summary>
        /// Used to Transform Accident and Vioaltion Nodes for Surcharge
        /// InputXml from QQ 
        /// </summary>
        /// <param name="InputXml"></param>
        /// <returns></returns>
        public string SetAssignDriverAcciVioPointsNode(string InputXml)
        {
            string strAssopertor = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(InputXml);
            XmlNode tmpNode;
            XmlElement nodeElment, nodeVioElment, NodeAccElement;


            const string YOUTHFUL_OCCASSIONAL_POINT_APPLIED = "YOPA";
            const string YOUTHFUL_PRINCIPAL_POINT_APPLIED = "YPPA";
            const string PRINCIPAL_POINTS_APPLIED = "PPA";
            const string OCCASSIONAL_POINTS_APPLIED = "OPA";




            XmlNodeList VnodeList = xmldoc.SelectNodes("quickQuote/vehicles/vehicle");
            XmlNodeList DnodeList = xmldoc.SelectNodes("quickQuote/drivers/driver[@VEHICLEDRIVEDAS ='" + YOUTHFUL_OCCASSIONAL_POINT_APPLIED + "' or @VEHICLEDRIVEDAS ='" + YOUTHFUL_PRINCIPAL_POINT_APPLIED + "' or @VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_APPLIED + "' or  @VEHICLEDRIVEDAS ='" + OCCASSIONAL_POINTS_APPLIED + "']");

            foreach (XmlNode Vnode in VnodeList)
            {
                //Delete Existing Record 
                XmlNodeList VIOACCPNTList = Vnode.SelectNodes("ASSIGNDRIVIOACCPNT");
                foreach (XmlNode VIOACCPNTnode in VIOACCPNTList)
                {
                    if (VIOACCPNTnode != null)
                    {
                        Vnode.RemoveChild(VIOACCPNTnode);
                    }

                }

                tmpNode = xmldoc.SelectSingleNode("quickQuote/vehicles/vehicle[@id = '" + Vnode.Attributes["id"].Value.ToString().Trim() + "']");

                // TRAVEL DRIVERS NODE FOR ASSIGNED DRIVER
                foreach (XmlNode Dnode in DnodeList)
                {
                    strAssopertor = Dnode.SelectSingleNode("VehicleAssignedAsOperator").InnerText;

                    if (strAssopertor != "")
                    {
                        if (strAssopertor == Vnode.Attributes["id"].Value.ToString().Trim())
                        {
                            if (Dnode.SelectSingleNode("violations").HasChildNodes)
                            {
                                nodeElment = xmldoc.CreateElement("ASSIGNDRIVIOACCPNT");
                                nodeElment.SetAttribute("ID", Dnode.Attributes["id"].Value.ToString().Trim());
                                nodeVioElment = xmldoc.CreateElement("SUMOFVIOLATIONPOINTS");
                                nodeVioElment.InnerText = xmldoc.SelectSingleNode("quickQuote/drivers/driver[@id = '" + Dnode.Attributes["id"].Value.ToString().Trim() + "']/SUMOFVIOLATIONPOINTS").InnerText;
                                NodeAccElement = xmldoc.CreateElement("SUMOFACCIDENTPOINTS");
                                NodeAccElement.InnerText = xmldoc.SelectSingleNode("quickQuote/drivers/driver[@id = '" + Dnode.Attributes["id"].Value.ToString().Trim() + "']/SUMOFACCIDENTPOINTS").InnerText;
                                nodeElment.AppendChild(nodeVioElment);
                                nodeElment.AppendChild(NodeAccElement);
                                tmpNode.InsertAfter(nodeElment, tmpNode.LastChild);
                            }

                        }

                    }

                }

            }

            return UndeclaredEntitiesInXml(xmldoc.OuterXml.ToString());

        }
        /// <summary>
        /// Used to Transform Accident and Vioaltion Nodes for Surcharge
        /// InputXml from Capital Rater 
        /// </summary>
        /// <param name="InputXml"></param>
        /// <returns></returns>
        public string SetAssignDriverAcciVioPointsCapitalRaterNode(string InputXml, string CollgDrvID)
        {
            string strAssopertor = "", strPotVhApd = "";
            int intPtAld = 0;
            XmlDocument xmldoc = new XmlDocument();
            InputXml = InputXml.Replace("&AMP;", "&amp;");
            xmldoc.LoadXml(InputXml);
            XmlNode tmpNode;
            XmlElement nodeElment, nodeVioElment, NodeAccElement;
            string[] CollDrv = new string[0];
            CollDrv = CollgDrvID.Split('~');
            const string YOUTHFUL_OCCASSIONAL_POINT_APPLIED = "YOPA";
            const string YOUTHFUL_PRINCIPAL_POINT_APPLIED = "YPPA";
            const string PRINCIPAL_POINTS_APPLIED = "PPA";
            const string OCCASSIONAL_POINTS_APPLIED = "OPA";



            XmlNodeList VnodeList = xmldoc.SelectNodes("QUICKQUOTE/VEHICLES/VEHICLE");
            XmlNodeList DnodeList = xmldoc.SelectNodes("QUICKQUOTE/DRIVERS/DRIVER[@VEHICLEDRIVEDAS ='" + YOUTHFUL_OCCASSIONAL_POINT_APPLIED + "' or @VEHICLEDRIVEDAS ='" + YOUTHFUL_PRINCIPAL_POINT_APPLIED + "' or @VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_APPLIED + "' or  @VEHICLEDRIVEDAS ='" + OCCASSIONAL_POINTS_APPLIED + "']");


            foreach (XmlNode Vnode in VnodeList)
            {
                tmpNode = xmldoc.SelectSingleNode("QUICKQUOTE/VEHICLES/VEHICLE[@ID = '" + Vnode.Attributes["ID"].Value.ToString().Trim() + "']");

                // TRAVEL DRIVERS NODE FOR ASSIGNED DRIVER
                foreach (XmlNode Dnode in DnodeList)
                {

                    //strAssopertor = Dnode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText;
                    strAssopertor = Dnode.Attributes["VEHICLESASSIGNEDASOPERATOR"].Value.ToString().Trim();
                    if (strAssopertor.StartsWith("^"))
                        strAssopertor = strAssopertor.Substring(strAssopertor.IndexOf("^") + 1, (strAssopertor.Length - 1));
                    string[] assindriverID = new string[0];
                    assindriverID = strAssopertor.Split('^');
                    for (int iCounter = 0; iCounter < assindriverID.Length; iCounter++)
                    {
                        if (Vnode.Attributes["ID"].Value.ToString().Trim() == assindriverID[iCounter])
                        {
                            strAssopertor = assindriverID[iCounter];
                        }
                    }
                    if (strAssopertor != "")
                    {
                        if (strAssopertor == Vnode.Attributes["ID"].Value.ToString().Trim())
                        {
                            intPtAld = 0;
                            //Fetch Points appllied node for each Driver 
                            string[] DrvVioPnt = new string[0];
                            if (Dnode.SelectSingleNode("POINTSAPPLIED") != null)
                                strPotVhApd = Dnode.SelectSingleNode("POINTSAPPLIED").InnerText;
                            if (strPotVhApd.IndexOf(Vnode.Attributes["ID"].Value.ToString().Trim()) > 0)
                            {
                                if (strPotVhApd.StartsWith("^"))
                                    strPotVhApd = strPotVhApd.Substring(strPotVhApd.IndexOf("^") + 1, (strPotVhApd.Length - 1));
                                DrvVioPnt = strPotVhApd.Split('^');
                                for (int ctr = 0; ctr < DrvVioPnt.Length; ctr++)
                                {
                                    if (DrvVioPnt[ctr].IndexOf(Vnode.Attributes["ID"].Value.ToString().Trim()) >= 0)
                                    {
                                        if (DrvVioPnt[ctr].IndexOf("Y") > 0 || DrvVioPnt[ctr].IndexOf("y") > 0)
                                            intPtAld = 1;
                                        else
                                            intPtAld = 0;
                                    }
                                }
                            }
                            if (Dnode.SelectSingleNode("MVR").InnerText != "" && intPtAld == 1)
                            {
                                nodeElment = xmldoc.CreateElement("ASSIGNDRIVIOACCPNT");
                                nodeElment.SetAttribute("ID", Dnode.Attributes["ID"].Value.ToString().Trim());
                                nodeVioElment = xmldoc.CreateElement("SUMOFVIOLATIONPOINTS");
                                nodeVioElment.InnerText = xmldoc.SelectSingleNode("QUICKQUOTE/DRIVERS/DRIVER[@ID = '" + Dnode.Attributes["ID"].Value.ToString().Trim() + "']/SUMOFVIOLATIONPOINTS").InnerText;
                                NodeAccElement = xmldoc.CreateElement("SUMOFACCIDENTPOINTS");
                                NodeAccElement.InnerText = xmldoc.SelectSingleNode("QUICKQUOTE/DRIVERS/DRIVER[@ID = '" + Dnode.Attributes["ID"].Value.ToString().Trim() + "']/SUMOFACCIDENTPOINTS").InnerText;
                                nodeElment.AppendChild(nodeVioElment);
                                nodeElment.AppendChild(NodeAccElement);
                                tmpNode.InsertAfter(nodeElment, tmpNode.LastChild);
                            }
                            else
                            {
                                if (Dnode.SelectSingleNode("ASSIGNDRIVIOACCPNT") != null)
                                {
                                    XmlNode node = Dnode.SelectSingleNode("//ASSIGNDRIVIOACCPNT");
                                    Dnode.RemoveChild(node);
                                }
                            }

                        }

                    }

                }
                //if vehicle is drived by college student 
                // accident and violation of college student will be considered. rest will be removed
                if (CollgDrvID != "")
                {
                    for (int ctr = 0; ctr < CollDrv.Length; ctr++)
                    {
                        string[] VehDrv = new string[0];
                        VehDrv = CollDrv[ctr].Split('^');
                        if (VehDrv.Length >= 2)
                        {
                            if (VehDrv[1] == Vnode.Attributes["ID"].Value.ToString().Trim())
                            {
                                XmlNodeList lstnod = Vnode.SelectNodes("ASSIGNDRIVIOACCPNT");
                                foreach (XmlNode pNode in lstnod)
                                {
                                    if (pNode.Attributes["ID"].Value.ToString().Trim() != VehDrv[0])
                                        Vnode.RemoveChild(pNode);
                                }
                                //Update Premier and Safe driver discount
                                XmlNodeList lstNode = Vnode.SelectNodes("ASSIGNDRIVIOACCPNT");
                                int intVio = 0, intAcc = 0;
                                foreach (XmlNode vNode in lstNode)
                                {
                                    if (vNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString().Trim() != "")
                                    {
                                        intVio += int.Parse(vNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString().Trim());
                                    }
                                    if (vNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString().Trim() != "")
                                    {
                                        intAcc += int.Parse(vNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString().Trim());
                                    }
                                }
                                if (Vnode.SelectSingleNode("TMPPREMIER").InnerText == "TRUE" && intVio <= 0)
                                {
                                    Vnode.SelectSingleNode("PREMIERDRIVER").InnerText = "TRUE";
                                }
                                if (Vnode.SelectSingleNode("TMPPREMIER").InnerText == "TRUE" && intAcc <= 0)
                                {
                                    Vnode.SelectSingleNode("PREMIERDRIVER").InnerText = "TRUE";
                                }
                                if (Vnode.SelectSingleNode("TMPSAFE").InnerText == "TRUE" && intVio <= 0)
                                {
                                    Vnode.SelectSingleNode("SAFEDRIVER").InnerText = "TRUE";
                                }
                                if (Vnode.SelectSingleNode("TMPSAFE").InnerText == "TRUE" && intAcc <= 0)
                                {
                                    Vnode.SelectSingleNode("SAFEDRIVER").InnerText = "TRUE";
                                }
                            }
                        }
                    }

                }
                Vnode.RemoveChild(Vnode.SelectSingleNode("TMPSAFE"));
                Vnode.RemoveChild(Vnode.SelectSingleNode("TMPPREMIER"));
            }
            return xmldoc.OuterXml;

        }

        /// <summary>
        /// Used to Transform Accident and Vioaltion Nodes for Surcharge
        /// InputXml from QQ -- MOTORCYCLE
        /// </summary>
        /// <param name="InputXml"></param>
        /// <returns></returns>
        public string SetAssignDriverAcciVioPointsNodeMotor(string InputXml)
        {
            string strAssopertor = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(InputXml);
            XmlNode tmpNode;
            XmlElement nodeElment, nodeVioElment, NodeAccElement;


            //const string YOUTHFUL_OCCASSIONAL_POINT_APPLIED = "YOPA";
            //const string YOUTHFUL_PRINCIPAL_POINT_APPLIED = "YPPA";
            const string PRINCIPAL_POINTS_APPLIED = "PPA";
            const string OCCASSIONAL_POINTS_APPLIED = "OPA";




            XmlNodeList VnodeList = xmldoc.SelectNodes("quickQuote/vehicles/vehicle");
            XmlNodeList DnodeList = xmldoc.SelectNodes("quickQuote/drivers/driver[@VEHICLEDRIVEDAS ='" + PRINCIPAL_POINTS_APPLIED + "' or  @VEHICLEDRIVEDAS ='" + OCCASSIONAL_POINTS_APPLIED + "']");

            foreach (XmlNode Vnode in VnodeList)
            {
                //Delete Existing Record 
                XmlNodeList VIOACCPNTList = Vnode.SelectNodes("ASSIGNDRIVIOACCPNT");
                foreach (XmlNode VIOACCPNTnode in VIOACCPNTList)
                {
                    if (VIOACCPNTnode != null)
                    {
                        Vnode.RemoveChild(VIOACCPNTnode);
                    }

                }

                tmpNode = xmldoc.SelectSingleNode("quickQuote/vehicles/vehicle[@id = '" + Vnode.Attributes["id"].Value.ToString().Trim() + "']");

                // TRAVEL DRIVERS NODE FOR ASSIGNED DRIVER
                foreach (XmlNode Dnode in DnodeList)
                {
                    strAssopertor = Dnode.SelectSingleNode("VehicleAssignedAsOperator").InnerText;

                    if (strAssopertor != "")
                    {
                        if (strAssopertor == Vnode.Attributes["id"].Value.ToString().Trim())
                        {
                            if (Dnode.SelectSingleNode("violations").HasChildNodes)
                            {
                                nodeElment = xmldoc.CreateElement("ASSIGNDRIVIOACCPNT");
                                nodeElment.SetAttribute("ID", Dnode.Attributes["id"].Value.ToString().Trim());
                                nodeVioElment = xmldoc.CreateElement("SUMOFVIOLATIONPOINTS");
                                nodeVioElment.InnerText = xmldoc.SelectSingleNode("quickQuote/drivers/driver[@id = '" + Dnode.Attributes["id"].Value.ToString().Trim() + "']/SUMOFVIOLATIONPOINTS").InnerText;
                                NodeAccElement = xmldoc.CreateElement("SUMOFACCIDENTPOINTS");
                                NodeAccElement.InnerText = xmldoc.SelectSingleNode("quickQuote/drivers/driver[@id = '" + Dnode.Attributes["id"].Value.ToString().Trim() + "']/SUMOFACCIDENTPOINTS").InnerText;
                                nodeElment.AppendChild(nodeVioElment);
                                nodeElment.AppendChild(NodeAccElement);
                                tmpNode.InsertAfter(nodeElment, tmpNode.LastChild);
                            }

                        }

                    }

                }

            }

            return UndeclaredEntitiesInXml(xmldoc.OuterXml.ToString());

        }
        public string PaymentDistrbutionPlan(string TotalPremium, string Term, DateTime PolicyEfffDate)
        {
            try
            {
                StringBuilder returnString = new StringBuilder();
                returnString.Append("<brics_PaymentOption>");
                string strReturnXML = "";
                string strPaymentPlan = "Proc_GetPaymentPlanFor_TotalPremium";
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                DataSet dsTempXML;

                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@TOTALPREMIUM", TotalPremium);
                objDataWrapper.AddParameter("@POLICYTERM", Term);
                objDataWrapper.AddParameter("@POLICYEFFFDATE", PolicyEfffDate);
                dsTempXML = objDataWrapper.ExecuteDataSet(strPaymentPlan);
                objDataWrapper.ClearParameteres();

                //strReturnXML = dsTempXML.GetXml();
                if (dsTempXML.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow AutoDetail in dsTempXML.Tables[0].Rows)
                    {
                        returnString.Append(AutoDetail["PAYMENT_PLAN"].ToString());
                    }
                }
                returnString.Append("</brics_PaymentOption>");
                strReturnXML = returnString.ToString();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                return strReturnXML;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        private string UndeclaredEntitiesInXml(string strPremiumXml)
        {
            strPremiumXml = strPremiumXml.Replace("'", "H673GSUYD7G3J73UDH");
            return strPremiumXml;

        }
        public static string FetchMotorcyclerInputXML(int customerID, int appID, int appVersionID)
        {
            return FetchMotorcyclerInputXML(customerID, appID, appVersionID, null);
        }
        public static string FetchMotorcyclerInputXML(int customerID, int appID, int appVersionID, DataWrapper objDataWrapper)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForMotorcycle_DriverComponent = "Proc_GetRatingInformationFormMotorcycle_DriverComponent";
            string strStoredProcForMotorcycle_VehicleComponent = "Proc_GetRatingInformationForMotorcycle_VehicleComponent";
            string strStoredProcForMotorcycle_VehicleCovgComponent = "Proc_GetRatingInformationForMotorcycle_VehicleCovgComponent";
            string strStoredProcForMotorcycle_ViolationsComponent = "Proc_GetRatingInformationForMotorcycle_DriverViolationComponent";
            string strStoredProcForMotorcycle_AppComponent = "Proc_GetRatingInformationFormMotorcycle_PolicyComponent";
            string strStoredProcForMotorcycle_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo";
            string strReturnXML = "", strAssidrivers = "";//strviolationXML = "",
            int intassidriveVio = 0, intassidriverAcci = 0, MvrPoints = 0, intassidriverMicPoint = 0;
            ClsQuickQuote objQuickQuote = new ClsQuickQuote();
            XmlDocument xmlDocViolAcc = new XmlDocument();
            string strpolicy;
            string strMVRPoints = "";//, strVioId = "", strvio = "";
            int violationPoints = 0;
            int accidentPoints = 0;
            //int temptotalpoints = 0;
            //int strLenght = 0;
            DataSet dsTempXML;
            XmlNode nodtempvehicleassig;//nodetempviolation, nodetempviolationId,

            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_AppComponent);
                objDataWrapper.ClearParameteres();

                strReturnXML = dsTempXML.GetXml();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");

                returnString.Append(strReturnXML);
                //Assigning the policy node  : PK
                strpolicy = strReturnXML;
                //End 

                /*Get Primary Applicant Info*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "APP");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*Get Primary Applicant Info*/

                returnString.Append("</POLICY>");
                strReturnXML = "";


                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the vehicle details for each vehicle
                returnString.Append("<VEHICLES>");
                string vehicleIDs = ClsGeneralInformation.GetVehicleIDs(customerID, appID, appVersionID, objDataWrapper);
                if (vehicleIDs != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = vehicleIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<VEHICLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_VehicleComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");

                        //Add node for each vehicle  for count
                        int rowID = iCounter + 1;
                        string vehicleRowIDNode = "<VEHICLEROWID>" + rowID.ToString() + "</VEHICLEROWID>";
                        returnString.Append(vehicleRowIDNode);
                        returnString.Append(strReturnXML);

                        //Get coverages
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_VehicleCovgComponent);
                        objDataWrapper.ClearParameteres();

                        if (dsTempXML.Tables[0] != null && dsTempXML.Tables[0].Rows.Count > 0)
                        {
                            strReturnXML = dsTempXML.Tables[0].Rows[0][0].ToString();
                            returnString.Append(strReturnXML);
                        }

                        /************** START ITRACK # 5081 Manoj Rathore ***************************/

                        //string setvalue = "";
                        int intVio = 0, intAcci = 0, intMis = 0;//intMvr = 0,
                        string AssinegDriverId = ClsDriverDetail.GetMotorAssinedDriverId(customerID, appID, appVersionID, int.Parse(vehicleID[iCounter]), objDataWrapper, "APP");
                        strAssidrivers = strAssidrivers + "^" + AssinegDriverId;
                        if (AssinegDriverId != "-1")
                        {
                            string[] assindriverID = new string[0];
                            assindriverID = AssinegDriverId.Split('^');
                            for (int iAssCounter = 0; iAssCounter < assindriverID.Length; iAssCounter++)
                            {
                                DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID, appID, appVersionID, int.Parse(assindriverID[iAssCounter].ToString()), "APP", objDataWrapper);
                                if (dtMvr != null && dtMvr.Rows.Count > 0)
                                {
                                    intassidriveVio = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                    intassidriverAcci = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                                    intassidriverMicPoint = int.Parse(dtMvr.Rows[0]["SUMOFMISCPOINTS"].ToString());
                                    intVio += intassidriveVio;
                                    intAcci += intassidriverAcci;
                                    intMis += intassidriverMicPoint;
                                }
                                //returnString.Append("<ASSIGNDRIVIOACCPNT" +" " + "ID ='" + assindriverID[iAssCounter].ToString() + "'><SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS><SUMOFMISCPOINTS>" + intassidriverMicPoint.ToString() + "</SUMOFMISCPOINTS></ASSIGNDRIVIOACCPNT>");
                            }
                            returnString.Append("<SUMOFVIOLATIONPOINTS>" + intVio.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intAcci.ToString() + "</SUMOFACCIDENTPOINTS><SUMOFMISCPOINTS>" + intMis.ToString() + "</SUMOFMISCPOINTS>");
                        }

                        /************** END ITRACK # 5081 *******************************************/

                        returnString.Append("</VEHICLE>");

                    }
                    intassidriveVio = 0;
                    intassidriverAcci = 0;
                    intassidriverMicPoint = 0;
                }
                returnString.Append("</VEHICLES>");

                //get the driver details for each driver
                returnString.Append("<DRIVERS>");
                string driverIDs = ClsDriverDetail.GetDriverIDs(customerID, appID, appVersionID, objDataWrapper);
                if (driverIDs != "-1")
                {
                    string[] driverID = new string[0];
                    driverID = driverIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        returnString.Append("<DRIVER ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_DriverComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = dsTempXML.GetXml();
                        // LOAD XML TO FETCH SUM OF VIOLATION AND ACCIDENT POINT
                        xmlDocViolAcc.LoadXml(strReturnXML);

                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        returnString.Append(strReturnXML);
                        //for each driver also get the violation detials (if any)
                        //returnString.Append("<VIOLATIONS>");
                        returnString.Append("<VIOLATIONS>");
                        string violationIDs = ClsDriverDetail.GetViolationIDs(customerID, appID, appVersionID, int.Parse(driverID[iCounter].ToString()), objDataWrapper);
                        int sumofViolationPoints = 0, sumofAccidentPoints = 0, sumofMvrPoints = 0; //Set Violation Variables

                        StringBuilder strViolationNodes = new StringBuilder(); //For Driver Violations .

                        if (violationIDs != "-1")
                        {
                            string[] violationID = new string[0];
                            violationID = violationIDs.Split('^');

                            //strViolationNodes.Remove(0,strViolationNodes.Length);

                            // Run a loop to get the inputXML for each vehicleID
                            for (int iCounterForViolations = 0; iCounterForViolations < violationID.Length; iCounterForViolations++)
                            {
                                //For just Violation Nodes
                                strViolationNodes.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>");
                                //
                                returnString.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>");
                                objDataWrapper.ClearParameteres();
                                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                                objDataWrapper.AddParameter("@APPID", appID);
                                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                                objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                                //Modified it to @APP_MVR_ID from @VIOLATIONID
                                objDataWrapper.AddParameter("@APP_MVR_ID", violationID[iCounterForViolations]);
                                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_ViolationsComponent);
                                objDataWrapper.ClearParameteres();

                                strReturnXML = dsTempXML.GetXml();
                                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("<Table>", "");
                                strReturnXML = strReturnXML.Replace("</Table>", "");
                                //Appending the Violations 
                                strViolationNodes.Append(strReturnXML);
                                returnString.Append(strReturnXML);
                                //End 
                                returnString.Append("</VIOLATION>");
                                //Appending the Violations 
                                strViolationNodes.Append("</VIOLATION>");

                            }
                            strMVRPoints = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() + "</SUMOFACCIDENTPOINTS></POINTS>";

                        }
                        else
                        {
                            //if strViolation is blank (No Violations Selected //
                            strMVRPoints = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS><SUMOFMISCPOINTS>0</SUMOFMISCPOINTS></POINTS>";
                        }
                        if ((xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "PPA^PRINCIPAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "OPA^OCCASIONAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "NR^NR"))
                        {
                            DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID, appID, appVersionID, int.Parse(driverID[iCounter].ToString()), "APP", objDataWrapper);
                            if (dtMvr != null && dtMvr.Rows.Count > 0)
                            {
                                MvrPoints = int.Parse(dtMvr.Rows[0]["MVR_POINTS"].ToString());
                                violationPoints = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                accidentPoints = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                            }
                        }
                        strMVRPoints = "<POINTS><MVR>" + MvrPoints.ToString() + "</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() + "</SUMOFACCIDENTPOINTS></POINTS>";

                        returnString.Append("</VIOLATIONS>");
                        //Set the nodes of sum of violation n accident points for each driver
                        XmlDocument PointsDoc = new XmlDocument();
                        PointsDoc.LoadXml(strMVRPoints);
                        XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
                        if (PointNode != null)
                        {

                            sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
                            sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                            sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());

                            returnString.Append("<MVR>");
                            returnString.Append(sumofMvrPoints);
                            returnString.Append("</MVR>");

                            returnString.Append("<SUMOFVIOLATIONPOINTS>");
                            returnString.Append(sumofViolationPoints);
                            returnString.Append("</SUMOFVIOLATIONPOINTS>");

                            returnString.Append("<SUMOFACCIDENTPOINTS>");
                            returnString.Append(sumofAccidentPoints);
                            returnString.Append("</SUMOFACCIDENTPOINTS>");

                        }
                        returnString.Append("</DRIVER>");

                    }
                }
                returnString.Append("</DRIVERS>");

                returnString.Append("</QUICKQUOTE>");
                #region
                /*
							string strMVRPointsForSurchargeCyclXML = "<ViolationsMVRPoints>" + strpolicy + "<violations>" + strViolationNodes.ToString() +  "</violations>" + "</ViolationsMVRPoints>";
							ClsQuickQuote ClsQQobj = new ClsQuickQuote();
							strMVRPoints = ClsQQobj.GetMVRPointsForSurcharge(strMVRPointsForSurchargeCyclXML,"CYCL");
							*/
                /*Set the values of the variables Calling method to Calculate points:*/
                /*ClsQuickQuote objQuickQuote = new ClsQuickQuote();
                            DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID,appID,appVersionID,int.Parse(driverID[iCounter].ToString()),"APP",objDataWrapper);
                            if(dtMvr!=null && dtMvr.Rows.Count>0)	
                            {
                                violationPoints = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                accidentPoints = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                            }					
				
                            strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() +  "</SUMOFACCIDENTPOINTS></POINTS>";
                            //End 
                        }
                        else
                        {
                            strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>-1</SUMOFACCIDENTPOINTS></POINTS>";
                        }*/


                /*Set the nodes of sum of violation n accident points for each driver*/
                /*XmlDocument PointsDoc = new XmlDocument();
                            PointsDoc.LoadXml(strMVRPoints);
                            XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
                            if(PointNode!=null)
                            {
							
                                sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
                                sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                                sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                
                                returnString.Append("<MVR>");
                                returnString.Append(sumofMvrPoints);
                                returnString.Append("</MVR>");

                                returnString.Append("<SUMOFVIOLATIONPOINTS>");
                                returnString.Append(sumofViolationPoints);
                                returnString.Append("</SUMOFVIOLATIONPOINTS>");

                                returnString.Append("<SUMOFACCIDENTPOINTS>");
                                returnString.Append(sumofAccidentPoints);
                                returnString.Append("</SUMOFACCIDENTPOINTS>");
							
                            }*/

                //							strViolationNodes.Append("</VIOLATIONS>");
                //							strviolationXML = strViolationNodes.ToString();
                //							XmlDocument xmlDoc = new XmlDocument();
                //							xmlDoc.LoadXml(strviolationXML);
                //							XmlNode nodtempViolation = xmlDoc.SelectSingleNode("VIOLATIONS");
                //							foreach(XmlNode nodchild in nodtempViolation.ChildNodes)
                //							{
                //								nodetempviolationId = xmlDoc.SelectSingleNode("VIOLATIONS/VIOLATION/@ID");
                //								strVioId=nodetempviolationId.InnerText;
                //								nodetempviolation = xmlDoc.SelectSingleNode("VIOLATIONS/VIOLATION[@ID='"+ strVioId +"']/MVR");
                //								temptotalpoints += System.Convert.ToInt32(nodetempviolation.InnerText);
                //
                //							}
                //							strLenght =strviolationXML.Length; 
                //							//strvio = "<MVR>0</MVR><SUMOFVIOLATIONPOINTS>+"+temptotalpoints+"+</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS>";
                //							strviolationXML = strviolationXML.Insert(strLenght,"<MVR>0</MVR><SUMOFVIOLATIONPOINTS>"+temptotalpoints+"</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS>");
                //							/*XmlElement elmnttempMVR = xmlDoc.CreateElement("MVR");
                //							elmnttempMVR.InnerText ="0"; 
                //							XmlElement elmnttempVio = xmlDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                //							elmnttempVio.InnerText =temptotalpoints.ToString(); 
                //							XmlElement elmnttempAcci = xmlDoc.CreateElement("SUMOFACCIDENTPOINTS");
                //							elmnttempAcci.InnerText =temptotalpoints.ToString();*/
                //							
                //							//nodtempViolation.InsertAfter(elmnttempAcci,nodtempViolation);
                //							returnString.Append(strviolationXML);
                //						}
                //						
                //						returnString.Append("</DRIVER>");
                //					}
                //				}
                //				returnString.Append("</DRIVERS>");
                //				returnString.Append("</QUICKQUOTE>");
                //returnString	=	returnString.Replace(",","");
                #endregion
                //**Start 
                //*****Loading Documnet and Setting the VIOLATION POINTS at VUHICLE LEVEL : 21 April 2006
                //**Loading the Appended XML form Procedures and Appending and Setting the Violation NODES**//
                string strOutXml = returnString.ToString();
                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                int mvrPoints = 0, sumOFviolations = 0, sumOfAccidents = 0;
                foreach (XmlNode VehicleVioNode in returnXMLDoc.SelectNodes("//VEHICLES/*"))
                {
                    int VehID = int.Parse(VehicleVioNode.Attributes["ID"].Value.ToString().Trim());
                    mvrPoints = 0; sumOFviolations = 0; sumOfAccidents = 0;
                    foreach (XmlNode DriverVioNode in returnXMLDoc.SelectNodes("//DRIVERS/*"))
                    {
                        nodtempvehicleassig = DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR");
                        if (nodtempvehicleassig != null)
                        {
                            if (Convert.ToInt32(DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString()) == VehID)
                            {
                                if (DriverVioNode.SelectSingleNode("MVR") != null)
                                {
                                    mvrPoints += Convert.ToInt32(DriverVioNode.SelectSingleNode("MVR").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("MVR").InnerText.ToString());
                                    sumOFviolations += Convert.ToInt32(DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                                    sumOfAccidents += Convert.ToInt32(DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                }
                            }
                        }
                    }

                    //**Creating New Nodes for VIOLATIONS (CYCL)**//
                    XmlElement mvrNode = returnXMLDoc.CreateElement("MVR");
                    XmlElement vioNode = returnXMLDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                    XmlElement accidentNode = returnXMLDoc.CreateElement("SUMOFACCIDENTPOINTS");
                    //Set the text to node
                    XmlText mvrtext = returnXMLDoc.CreateTextNode(mvrPoints.ToString());
                    XmlText viotext = returnXMLDoc.CreateTextNode(sumOFviolations.ToString());
                    XmlText accidenttext = returnXMLDoc.CreateTextNode(sumOfAccidents.ToString());
                    //Append the nodes 
                    //					VehicleVioNode.AppendChild(mvrNode);
                    //					VehicleVioNode.AppendChild(vioNode);
                    //					VehicleVioNode.AppendChild(accidentNode);
                    //					//Save the value of the fields into the nodes
                    //					mvrNode.AppendChild(mvrtext);
                    //					vioNode.AppendChild(viotext);
                    //					accidentNode.AppendChild(accidenttext);
                    //					strOutXml = returnXMLDoc.OuterXml;

                }

                //**END
                //Returning the Modified string after appending the Violation Node:
                return strOutXml.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }


        }

        public static string FetchWatercraftInputXML(int customerID, int appID, int appVersionID)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForWatercraft_AppComponent = "Proc_GetRatingInformationForWatercraft_PolicyComponent";
            string strStoredProcForWatercraft_SportEquipmentsComponent = "Proc_GetRatingInformationForWatercraft_SportEquipmentsComponent";
            string strStoredProcForWatercraft_BoatComponent = "Proc_GetRatingInformationForWatercraft_BoatComponent";
            string strStoredProcForWatercraft_BoatCovgComponent = "Proc_GetRatingInformationForWatercraft_CoverageComponent";
            string strStoredProcForWatercraft_OperatorComponent = "Proc_GetRatingInformationForWatercraft_OperatorComponent";
            string strStoredProcForWatercraft_ViolationsComponent = "Proc_GetRatingInformationForWatercraft_Violations";
            string strStoredProcForWatercraft_TrailerComponent = "Proc_GetRatingInformationForWatercraft_TrailerComponent";
            string strStoredProcForWatercraft_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo";


            string strpolicy;
            string strMVRPoints = "", strviolationXML = "", strVioId = "";

            string strReturnXML = "";
            int violationPoints = 0, temptotalpoints = 0, strLenght = 0;
            int accidentPoints = 0, MvrPoints = 0;
            DataSet dsTempXML;
            XmlNode nodetempviolation;//nodetempviolationId,
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_AppComponent);
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml(); Removed on 17 April 2008
                //This Function Iterate through each Column ,Remove Junk Characters from NODES.
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                returnString.Append(strReturnXML);

                //Assigning the policy node  : PK
                strpolicy = strReturnXML;

                /*Get Primary Applicant Info*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "APP");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml();
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*Get Primary Applicant Info*/

                // get the equipment info against the customerID, appID, appVersionID
                // get the equipment details for each equipment	
                returnString.Append("<SCHEDULEDMISCSPORTS>");
                string strEquipIDs = clsWatercraftInformation.GetEquipmentID(customerID, appID, appVersionID);
                if (strEquipIDs != "-1")
                {
                    string[] arrEquipID = new string[0];
                    arrEquipID = strEquipIDs.Split('^');
                    // Run a loop to get the inputXML for each equipment ID
                    for (int iCounterForEquip = 0; iCounterForEquip < arrEquipID.Length; iCounterForEquip++)
                    {
                        returnString.Append("<SCH_MISC ID='" + arrEquipID[iCounterForEquip] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@EQUIP_ID", arrEquipID[iCounterForEquip]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_SportEquipmentsComponent);
                        objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        returnString.Append(strReturnXML);
                        returnString.Append("</SCH_MISC>");
                    }
                }
                returnString.Append("</SCHEDULEDMISCSPORTS>");
                returnString.Append("</POLICY>");
                // Get the Boat Ids against the customerID, appID, appVersionID
                //get the boat details for each boat
                returnString.Append("<BOATS>");
                string strBoatIDs = clsWatercraftInformation.GetBoatIDs(customerID, appID, appVersionID);
                if (strBoatIDs != "-1")
                {
                    string[] arrBoatID = new string[0];
                    arrBoatID = strBoatIDs.Split('^');
                    // Run a loop to get the inputXML for each BoatID
                    for (int iCounter = 0; iCounter < arrBoatID.Length; iCounter++)
                    {
                        returnString.Append("<BOAT ID='" + arrBoatID[iCounter] + "' BOATTYPE='" + "B" + "'>");	//Appended the Attribute BOATTYPE for RISK ID//					
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@BOATID", arrBoatID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_BoatComponent);
                        objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        //Add node for each Boat for count
                        int rowID = iCounter + 1;
                        string strBoatRowIDNode = "<BOATROWID>" + rowID.ToString() + "</BOATROWID>";
                        returnString.Append(strBoatRowIDNode);
                        returnString.Append(strReturnXML);
                        //Get coverages
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@APP_ID", appID);
                        objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                        objDataWrapper.AddParameter("@BOAT_ID", arrBoatID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_BoatCovgComponent);
                        objDataWrapper.ClearParameteres();

                        if (dsTempXML.Tables[0] != null && dsTempXML.Tables[0].Rows.Count > 0)
                        {
                            strReturnXML = dsTempXML.Tables[0].Rows[0][0].ToString();
                            returnString.Append(strReturnXML);
                        }
                        returnString.Append("</BOAT>");
                    }

                }

                strBoatIDs = clsWatercraftInformation.GetTrailerBoatIDs(customerID, appID, appVersionID);
                if (strBoatIDs != "-1")
                {
                    string[] arrBoatID = new string[0];
                    arrBoatID = strBoatIDs.Split('^');
                    // Run a loop to get the inputXML for each BoatID
                    for (int iCounter = 0; iCounter < arrBoatID.Length; iCounter++)
                    {
                        returnString.Append("<BOAT ID='" + arrBoatID[iCounter] + "' BOATTYPE='" + "T" + "'>");	//Appended the Attribute BOATTYPE for RISK ID//		
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@TRAILER_ID", arrBoatID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_TrailerComponent);
                        objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        //Add node for each Boat for count
                        int rowID = iCounter + 1;
                        string strBoatRowIDNode = "<BOATROWID>" + rowID.ToString() + "</BOATROWID>";
                        returnString.Append(strBoatRowIDNode);
                        returnString.Append(strReturnXML);
                        returnString.Append("</BOAT>");
                    }
                }
                returnString.Append("</BOATS>");
                //get the operator details for each operator
                returnString.Append("<OPERATORS>");
                string driverIDs = ClsDriverDetail.GetRuleWCDriverIDs(customerID, appID, appVersionID);
                if (driverIDs != "-1")
                {
                    string[] driverID = new string[0];
                    driverID = driverIDs.Split('^');
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        returnString.Append("<OPERATOR ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_OperatorComponent);
                        objDataWrapper.ClearParameteres();
                        //strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        returnString.Append(strReturnXML);
                        //for each driver also get the violation detials (if any)
                        //returnString.Append("<VIOLATIONS>");
                        string violationIDs = ClsDriverDetail.GetWCViolationIDs(customerID, appID, appVersionID, int.Parse(driverID[iCounter].ToString()));

                        //int sumofViolationPoints = 0, sumofAccidentPoints = 0, sumofMvrPoints = 0;
                        violationPoints = 0; accidentPoints = 0; MvrPoints = 0; //Set Violation Variables
                        strviolationXML = "";
                        StringBuilder strViolationNodes = new StringBuilder(); //For Driver Violations .
                        strViolationNodes.Append("<VIOLATIONS>");
                        //returns -1 when violations do not exist
                        if (violationIDs != "-1")
                        {
                            //violations existing
                            string[] violationID = new string[0];
                            violationID = violationIDs.Split('^');
                            //strViolationNodes.Remove(0,strViolationNodes.Length);

                            // Run a loop to get the inputXML for each vehicleID
                            for (int iCounterForViolations = 0; iCounterForViolations < violationID.Length; iCounterForViolations++)
                            {
                                //Violation Append
                                strViolationNodes.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>"); //for just violation nodes
                                //
                                //returnString.Append("<VIOLATION ID='"+ violationID[iCounterForViolations]+"'>");
                                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                                objDataWrapper.AddParameter("@APPID", appID);
                                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                                objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                                //APP_WATER_MVR_ID to get the Violation Details WATERCRAFT : 
                                //Modified it to @APP_WATER_MVR_ID from @VIOLATIONID
                                objDataWrapper.AddParameter("@APP_WATER_MVR_ID", violationID[iCounterForViolations]);
                                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_ViolationsComponent);
                                objDataWrapper.ClearParameteres();
                                //strReturnXML = dsTempXML.GetXml();
                                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("<Table>", "");
                                strReturnXML = strReturnXML.Replace("</Table>", "");
                                strViolationNodes.Append(strReturnXML);
                                //Appending the Violations 
                                //strViolationNodes.Append(strReturnXML);
                                //End 
                                strViolationNodes.Append("</VIOLATION>");
                                //
                                //strViolationNodes.Append("</VIOLATION>");
                                //
                            }

                            //Set the values of the variables Calling method to Calculate points:
                            //							string strLOBCode = "BOAT";
                            //							string strMVRPointsForSurchargeXML = "<ViolationsMVRPoints>" + strpolicy + "<violations>" + strViolationNodes.ToString() +  "</violations>" + "</ViolationsMVRPoints>";
                            //							ClsQuickQuote ClsQQobj = new ClsQuickQuote();
                            //							strMVRPoints = ClsQQobj.GetMVRPointsForSurcharge(strMVRPointsForSurchargeXML,strLOBCode);
                            //End 
                            /*Calculating Violation Points : */
                            /*ClsQuickQuote objQQ = new ClsQuickQuote();
                            DataTable dtMVR = new DataTable();
						
                            dtMVR = objQQ.GetMVRPointsForSurcharge(customerID,appID,appVersionID,int.Parse(driverID[iCounter]),"APP");
                            if(dtMVR!=null && dtMVR.Rows.Count>0)
                            {
                                violationPoints = int.Parse(dtMVR.Rows[0]["SUM_MVR_POINTS"].ToString());
                                accidentPoints = int.Parse(dtMVR.Rows[0]["ACCIDENT_POINTS"].ToString());
                            }
                            strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() +  "</SUMOFACCIDENTPOINTS></POINTS>";
                            //Calculating Violation Points : 

                        }
                        else
                        {
                            //if No Violations are Selected 
                            strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>-1</SUMOFACCIDENTPOINTS></POINTS>";
							
                        }*/

                            strViolationNodes.Append("</VIOLATIONS>");
                            XmlDocument xmlDoc = new XmlDocument();
                            strviolationXML = strViolationNodes.ToString();
                            xmlDoc.LoadXml(strviolationXML);
                            XmlNode nodtempViolation = xmlDoc.SelectSingleNode("VIOLATIONS");
                            foreach (XmlNode nodchild in nodtempViolation.ChildNodes)
                            {
                                //nodetempviolationId = nodchild.SelectSingleNode("VIOLATION");
                                strVioId = nodchild.Attributes["ID"].Value.ToString().Trim();
                                nodetempviolation = xmlDoc.SelectSingleNode("VIOLATIONS/VIOLATION[@ID='" + strVioId + "']/MVRPOINTS");
                                temptotalpoints += System.Convert.ToInt32(nodetempviolation.InnerText);

                            }
                            strLenght = strviolationXML.Length;
                            /////////////////////
                            ///
                        }
                        ClsQuickQuote objQQ = new ClsQuickQuote();
                        DataTable dtMVR = new DataTable();

                        dtMVR = objQQ.GetMVRPointsForSurcharge(customerID, appID, appVersionID, int.Parse(driverID[iCounter]), "APP");
                        if (dtMVR != null && dtMVR.Rows.Count > 0)
                        {
                            MvrPoints = int.Parse(dtMVR.Rows[0]["MVR_POINTS"].ToString());
                            violationPoints = int.Parse(dtMVR.Rows[0]["SUM_MVR_POINTS"].ToString());
                            accidentPoints = int.Parse(dtMVR.Rows[0]["ACCIDENT_POINTS"].ToString());
                        }
                        strMVRPoints = "<MVR>" + MvrPoints.ToString() + "</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() + "</SUMOFACCIDENTPOINTS>";
                        ///////////////////////////////////
                        if (strviolationXML != "")
                        {
                            strviolationXML = strviolationXML.Insert(strLenght, strMVRPoints);
                        }
                        else
                        {
                            strviolationXML = strMVRPoints;
                        }
                        returnString.Append(strviolationXML);

                        /*Set the nodes of sum of violation n accident points for each driver*/
                        /*XmlDocument PointsDoc = new XmlDocument();
                            PointsDoc.LoadXml(strMVRPoints);
                            XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
                            if(PointNode!=null)
                            {
							
                                sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
                                sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                                sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
							                               
                                returnString.Append("<MVR>");
                                returnString.Append(sumofMvrPoints);
                                returnString.Append("</MVR>");

                                returnString.Append("<SUMOFVIOLATIONPOINTS>");
                                returnString.Append(sumofViolationPoints);
                                returnString.Append("</SUMOFVIOLATIONPOINTS>");

                                returnString.Append("<SUMOFACCIDENTPOINTS>");
                                returnString.Append(sumofAccidentPoints);
                                returnString.Append("</SUMOFACCIDENTPOINTS>");
							
                            }*/


                        returnString.Append("</OPERATOR>");
                        temptotalpoints = 0;
                    }
                }
                returnString.Append("</OPERATORS>");
                returnString.Append("</QUICKQUOTE>");
                returnString = returnString.Replace(",", "");

                //*****Loading Documnet and Setting the VIOLATION POINTS at BOAT LEVEL : 21 April 2006
                //**Loading the Appended XML form Procedures and Appending and Setting the Violation NODES**//
                string strOutXml = "";
                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                int mvrPoints = 0, sumOFviolations = 0, sumOfAccidents = 0;
                foreach (XmlNode VehicleVioNode in returnXMLDoc.SelectNodes("//BOATS/*"))
                {
                    int VehID = int.Parse(VehicleVioNode.Attributes["ID"].Value.ToString().Trim());
                    mvrPoints = 0; sumOFviolations = 0; sumOfAccidents = 0;
                    foreach (XmlNode DriverVioNode in returnXMLDoc.SelectNodes("//OPERATORS/*"))
                    {
                        if (DriverVioNode.SelectSingleNode("BOATASSIGNEDASOPERATOR") != null)
                        {
                            if (Convert.ToInt32(DriverVioNode.SelectSingleNode("BOATASSIGNEDASOPERATOR").InnerText.ToString()) == VehID)
                            {
                                if (DriverVioNode.SelectSingleNode("VIOLATIONS") != null && DriverVioNode.SelectSingleNode("VIOLATIONS").InnerText != "")
                                {
                                    mvrPoints += int.Parse(DriverVioNode.SelectSingleNode("MVR").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("MVR").InnerText.ToString());
                                    sumOFviolations += int.Parse(DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                                    sumOfAccidents += int.Parse(DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                }
                            }
                        }
                    }

                    //**Creating New Nodes for VIOLATIONS (Watercraft)**//
                    XmlElement mvrNode = returnXMLDoc.CreateElement("MVR");
                    XmlElement vioNode = returnXMLDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                    XmlElement accidentNode = returnXMLDoc.CreateElement("SUMOFACCIDENTPOINTS");
                    //Set the text to node
                    XmlText mvrtext = returnXMLDoc.CreateTextNode(mvrPoints.ToString());
                    XmlText viotext = returnXMLDoc.CreateTextNode(sumOFviolations.ToString());
                    XmlText accidenttext = returnXMLDoc.CreateTextNode(sumOfAccidents.ToString());
                    //Append the nodes 
                    VehicleVioNode.AppendChild(mvrNode);
                    VehicleVioNode.AppendChild(vioNode);
                    VehicleVioNode.AppendChild(accidentNode);
                    //Save the value of the fields into the nodes
                    mvrNode.AppendChild(mvrtext);
                    vioNode.AppendChild(viotext);
                    accidentNode.AppendChild(accidenttext);

                    strOutXml = returnXMLDoc.OuterXml;

                }

                //END 
                //return returnStringFinal.ToString();
                return strOutXml.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }



        public static string FetchRentalDwellingInputXML(int customerID, int appID, int appVersionID)
        {
            DataWrapper objDataWrapper;
            string strStoredProc_For_Rental = "Proc_GetRatingInformationForRentalDwelling";
            string strStoredProcFor_Rental_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo";
            StringBuilder returnString = new StringBuilder();
            try
            {
                // Get the Dwelling ids against the customerID, appID, appVersionID
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                string dwellingIDs = ClsDwellingDetails.GetDwellingID(customerID, appID, appVersionID);
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE>");
                //If no dewlling found
                if (dwellingIDs != "-1")
                {
                    string[] dwellingIDAndAddress = new string[0];
                    dwellingIDAndAddress = dwellingIDs.Split('~');

                    // Run a loop to get the inputXML for all the dwellings
                    for (int iCounter = 0; iCounter < dwellingIDAndAddress.Length; iCounter++)
                    {
                        string[] dwellingDetail = dwellingIDAndAddress[iCounter].Split('^');
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingDetail[0]);
                        DataSet dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_For_Rental);
                        objDataWrapper.ClearParameteres();


                        //string strCoverages		=	dsTempXML.Tables[1].Rows[0][0].ToString();;
                        string strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        string strReturnXML = strOtherColumns;
                        string strAddress = dwellingDetail[1];

                        strAddress = strAddress.Replace("'", "H673GSUYD7G3J73UDH");
                        strAddress = strAddress.Replace("&", "&amp;");
                        strAddress = strAddress.Replace("<", "&amp;LT;");
                        strAddress = strAddress.Replace(">", "&amp;GT;");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_VALUE>", "");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_VALUE>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "<DWELLINGDETAILS ID= '" + dwellingDetail[0] + "' ADDRESS='" + strAddress + "'>");
                        strReturnXML = strReturnXML.Replace("</Table>", "</DWELLINGDETAILS>");
                        returnString.Append(strReturnXML);
                    }
                }
                /*Get Primary Applicant Info*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "APP");
                DataSet dsAppTempXML = objDataWrapper.ExecuteDataSet(strStoredProcFor_Rental_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                //string strAppReturnXML = dsAppTempXML.GetXml();
                string strAppReturnXML = ClsCommon.GetXML(dsAppTempXML.Tables[0]);
                strAppReturnXML = strAppReturnXML.Replace("<NewDataSet>", "");
                strAppReturnXML = strAppReturnXML.Replace("</NewDataSet>", "");
                strAppReturnXML = strAppReturnXML.Replace("<Table>", "");
                strAppReturnXML = strAppReturnXML.Replace("</Table>", "");
                strAppReturnXML = strAppReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strAppReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*Get Primary Applicant Info*/

                returnString.Append("</QUICKQUOTE>");
                return returnString.ToString();
            }
            catch
            { }
            finally
            { }
            return "";
        }
        public static DataSet FtechUmbrellaSelectedPolicies(int customerID, int appId, int appversionID)
        {
            string strStoredProc_For_Umb_policy = "Proc_GetSelectedPolicyInformationForUMB";
            DataSet dsCount = null;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", customerID, SqlDbType.Int);
            objDataWrapper.AddParameter("@ID", appId, SqlDbType.Int);
            objDataWrapper.AddParameter("@VERSION_ID", appversionID, SqlDbType.Int);
            dsCount = objDataWrapper.ExecuteDataSet(strStoredProc_For_Umb_policy);

            return dsCount;
        }
        public static int maximumCoverage(ArrayList Coveragelist)
        {
            int maxcov = 0;
            if (Coveragelist.Count >= 1)
            {
                string tempfirstvalue = Coveragelist[0].ToString();
                if (tempfirstvalue == "")
                    tempfirstvalue = "0";
                if (Coveragelist.Count == 1)
                {
                    if (Coveragelist[0].ToString() != "")
                        maxcov = System.Convert.ToInt32(Coveragelist[0].ToString());
                }
                else
                {
                    string tempsecondvalue = Coveragelist[1].ToString();
                    if (tempsecondvalue == "")
                        tempsecondvalue = "0";
                    int pintMaxValue = Math.Max(System.Convert.ToInt32(tempfirstvalue), System.Convert.ToInt32(tempsecondvalue));
                    int cavragevalue = 0;

                    for (int i = 2; i < Coveragelist.Count; i++)
                    {
                        // Check to see if the value is greater than the max value. If it is,
                        // bump everything down.
                        if (Coveragelist[i].ToString() == "")
                        {
                            cavragevalue = 0;
                        }
                        else
                        {
                            cavragevalue = System.Convert.ToInt32(Coveragelist[i].ToString());
                        }
                        if (cavragevalue > pintMaxValue)
                        {
                            // Set the max value to the array element.
                            pintMaxValue = System.Convert.ToInt32(Coveragelist[i].ToString());
                        }

                    }
                    maxcov = pintMaxValue;
                }
                return maxcov;
            }
            else
            {
                return 0;
            }
        }
        public static string FetchUmbrellaWatercraft(int customerID, int appId, int appVersionId, int dataaccessvalue)
        {
            DataWrapper objDataWrapper;
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            string strWatercraftID = "", strReturnXML = "", ReturnXML = "";
            string strStoredProcedureFor_Umb_Watercraft_details = "Proc_GetRatingInformationForUMBWaterCraft";
            StringBuilder returnString = new StringBuilder();

            strWatercraftID = clsWatercraftInformation.GetUmbrellaWatercraftID(customerID, appId, appVersionId, dataaccessvalue);
            DataSet dsTempXML;
            // fetch policy details for selected policy


            if (strWatercraftID != "-1")
            {
                string[] arrstrWatercraftID = new string[0];
                arrstrWatercraftID = strWatercraftID.Split('~');
                // Run a loop to get the inputXML for each equipment ID
                for (int iCounterForWater = 0; iCounterForWater < arrstrWatercraftID.Length; iCounterForWater++)
                {
                    returnString.Append("<WATERCRAFT ID='" + arrstrWatercraftID[iCounterForWater] + "'>");
                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                    objDataWrapper.AddParameter("@ID", appId);
                    objDataWrapper.AddParameter("@VERSION_ID", appVersionId);
                    objDataWrapper.AddParameter("@WATERCRAFT_ID", arrstrWatercraftID[iCounterForWater]);
                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrWatercraftID[iCounterForWater]);
                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_Watercraft_details);
                    objDataWrapper.ClearParameteres();

                    strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                    //strReturnXML		=	strOtherColumns;						
                    strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                    strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                    strReturnXML = strReturnXML.Replace("<Table>", "");
                    strReturnXML = strReturnXML.Replace("</Table>", "");
                    strReturnXML = strReturnXML.Replace("/r/n", "");
                    returnString.Append(strReturnXML);
                    returnString.Append("</WATERCRAFT>");

                }
                ReturnXML = returnString.ToString();
            }
            return ReturnXML;

        }
        public static string FetchUmbrellaInputXML(int customerID, int appID, int appVersionId)
        {
            DataWrapper objDataWrapper;
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            //genralinformation procs

            string strStoredProc_For_Umb_policy_details = "Proc_GetSelectedPolicyDetailsInformationForUMB";
            string strStoredProc_For_Umb_Genral_information = "Proc_GetGenralRatingInformationForUMB";
            string strStoredProcFor_Umb_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo";
            //coverages procs
            string strStoredProcedureFor_Umb_dwelling = "Proc_GetDwellingCoverageRatingInformationForUMB";
            string strStoredProcedureFor_Umb_Auto = "Proc_GetAutoCoverageRatingInformationForUMB";
            string strStoredProcedureFor_Umb_homeowners = "Proc_GetHomeOwnersCoverageRatingInformationForUMB";
            string strStoredProcedureFor_Umb_Motorcycle = "Proc_GetMotorcycleCoverageRatingInformationForUMB";
            string strStoredProcedureFor_Umb_Watercraft = "Proc_GetwatercraftCoverageRatingInformationForUMB";
            // watercraft details proc


            //string strpolicy;
            //string strMVRPoints = "";
            string strOtherColumns = "";
            string strReturnXML = "";
            //fetch policy information
            int idValue, versionValue, dataaccessvalue, counthomelob = 0, countautolob = 0, countmotorlob = 0, countwatercraftlob = 0, countdwellinglob = 0,
                totalAuto = 0, totalmotorhome = 0, totalinexpauto = 0, totalinexpmotro = 0, totalautomotordriver = 0, tempautocsl = 0,
                totalmotor = 0, totalinexpmotor = 0, totalmotorcycledriver = 0, tempautobilo = 0, tempautoup = 0, tempautopd = 0;
            string lobvalue = "", maturedriverautomoto = "N", uninsuredbi = "", uninsuredcsl = "", underinsuredbi = "",
                underinsuredcsl = "", excludeuninsured = "Y", maturedrivermotorcycle = "N", tempununderinsuredmotorist = "",
                tempWatercraftDetails = "";

            // Declaration of arraylist to select maximum coverage

            ArrayList AutoSplitCoveragelo = new ArrayList();
            ArrayList AutoSplitCoverageup = new ArrayList();
            ArrayList AutoPdCoverage = new ArrayList();
            ArrayList AutoCslCoverage = new ArrayList();
            ArrayList AutoCombinedCoverage = new ArrayList();
            ArrayList MotorSplitCoveragelo = new ArrayList();
            ArrayList MotorSplitCoverageup = new ArrayList();
            ArrayList MotorSplitCoveragePd = new ArrayList();
            ArrayList MotorCombinedCoverage = new ArrayList();
            ArrayList HomeOwnersCoverage = new ArrayList();
            ArrayList DwellingCoverage = new ArrayList();
            ArrayList WatercraftCoverage = new ArrayList();
            //Xml Document Declaration to load xml fetched from dataset
            XmlDocument xmlDatassetDoc = new XmlDocument();
            XmlNode nodetemp, nodetempautoBilo, nodetempautoBiup, nodtempautopd, nodetempautocsl, nodetempautouninsuredbi,
                nodtempautouninsuredcsl, nodetempautounderinsuredbi, nodetempautounderinsuredcsl, nodetempautomobiles,
                nodetempautomotorhome, nodetempautoinexpe, nodtempmotorinexpe, nodtempautomotorhomedreiver, nodtempautomatdriver, nodetempexcludeuninsured;

            StringBuilder returnString = new StringBuilder();
            StringBuilder returnStringwatercraft = new StringBuilder();
            DataSet dsTempXML;
            try
            {   //start building the inputxml
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE>");
                returnString.Append("<UMBRELLA>");
                returnString.Append("<APPLICANT_INFIORMATION>");
                //umbrella application general information
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@ID", appID);
                objDataWrapper.AddParameter("@VERSION_ID", appVersionId);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_For_Umb_Genral_information);
                objDataWrapper.ClearParameteres();
                strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                strReturnXML = strOtherColumns;
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                returnString.Append(strReturnXML);
                returnString.Append("</APPLICANT_INFIORMATION>");

                //check policies to be considered for picking coverages
                dsTempXML = FtechUmbrellaSelectedPolicies(customerID, appID, appVersionId);
                string strPolicyNumber = "-1";
                string strlob = "-1";
                string strpolicycompany = "-1";
                string strIsPolicy = "-1";
                string[] arrstrWatercraftPolicies = new string[0];
                string strwaterpolicies = "";
                if (dsTempXML.Tables[0] != null && dsTempXML.Tables[0].Rows != null)
                {
                    int intCount = dsTempXML.Tables[0].Rows.Count;
                    for (int i = 0; i < intCount; i++)
                    {
                        if (i == 0)
                        {
                            strPolicyNumber = dsTempXML.Tables[0].Rows[i][0].ToString();
                            strlob = dsTempXML.Tables[0].Rows[i][1].ToString();
                            strpolicycompany = dsTempXML.Tables[0].Rows[i][2].ToString();
                            strIsPolicy = dsTempXML.Tables[0].Rows[i][3].ToString();
                        }
                        else
                        {
                            strPolicyNumber = strPolicyNumber + '~' + dsTempXML.Tables[0].Rows[i][0].ToString();
                            strlob = strlob + '~' + dsTempXML.Tables[0].Rows[i][1].ToString();
                            strpolicycompany = strpolicycompany + '~' + dsTempXML.Tables[0].Rows[i][2].ToString();
                            strIsPolicy = strIsPolicy + '~' + dsTempXML.Tables[0].Rows[i][3].ToString();
                        }
                    }
                    //}

                    int flagHOME = 0, flagAUTO = 0, flagMOTORCYCLE = 0, flagWATERCRAFT = 0, flagRENTAL = 0;
                    if (strPolicyNumber != "-1")
                    {
                        string[] arrstrPolicyNumber = new string[0];
                        arrstrPolicyNumber = strPolicyNumber.Split('~');
                        string[] arrstrlob = new string[0];
                        arrstrlob = strlob.Split('~');
                        string[] arrstrcompany = new string[0];
                        arrstrcompany = strpolicycompany.Split('~');
                        string[] arrstrISPOLICY = new string[0];
                        arrstrISPOLICY = strIsPolicy.Split('~');
                        // Count the number of policy with a lob
                        for (int arraylenght = 0; arraylenght < arrstrlob.Length; arraylenght++)
                        {
                            if (LOB_HOME == arrstrlob[arraylenght])
                            {
                                counthomelob++;
                            }
                            if (LOB_PRIVATE_PASSENGER == arrstrlob[arraylenght])
                            {
                                countautolob++;
                            }
                            if (LOB_MOTORCYCLE == arrstrlob[arraylenght])
                            {
                                countmotorlob++;
                            }
                            if (LOB_WATERCRAFT == arrstrlob[arraylenght])
                            {
                                countwatercraftlob++;
                                strwaterpolicies = strwaterpolicies + '~' + arrstrPolicyNumber[arraylenght];
                                //arrstrWatercraftPolicies = arrstrPolicyNumber[arraylenght] + '~';
                            }
                            if (LOB_RENTAL_DWELLING == arrstrlob[arraylenght])
                            {
                                countdwellinglob++;
                            }

                        }


                        // Run a loop to get the coverages/information for each policy under each lob in the umb app
                        for (int iCounterForPolicy = 0; iCounterForPolicy < arrstrPolicyNumber.Length; iCounterForPolicy++)
                        {

                            //check the policy company. if it is WOLVERINE then get the PK parameter details 
                            if (arrstrcompany[iCounterForPolicy] == AGENCY_NAME) //CHANGE IT 
                            {
                                objDataWrapper.AddParameter("@POLICY_NUMBER", arrstrPolicyNumber[iCounterForPolicy]);
                                objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                objDataWrapper.AddParameter("@IS_POLICY", arrstrISPOLICY[iCounterForPolicy]);

                                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_For_Umb_policy_details);
                                objDataWrapper.ClearParameteres();
                                strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                strReturnXML = strOtherColumns;
                                XmlDocument docPolicyInput = new XmlDocument();
                                docPolicyInput.LoadXml(strReturnXML);
                                idValue = System.Convert.ToInt32(docPolicyInput.SelectSingleNode("NewDataSet/Table/IDS").InnerText);
                                versionValue = System.Convert.ToInt32(docPolicyInput.SelectSingleNode("NewDataSet/Table/VERSION_ID").InnerText);
                                lobvalue = docPolicyInput.SelectSingleNode("NewDataSet/Table/LOB").InnerText;
                                dataaccessvalue = System.Convert.ToInt32(docPolicyInput.SelectSingleNode("NewDataSet/Table/DATAACCESSPOINT").InnerText);
                            }
                            else
                            {
                                //else set teh same PK parameter as tht of the umbrella app.
                                idValue = appID;
                                versionValue = appVersionId;
                                dataaccessvalue = UMBRELLA;
                                lobvalue = arrstrlob[iCounterForPolicy];
                            }
                            // fetch data according to lob wise
                            if (lobvalue == LOB_HOME)
                            {
                                if (flagHOME <= counthomelob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_homeowners);
                                    objDataWrapper.ClearParameteres();
                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    nodetemp = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/HOMEOWNERPOLICY");
                                    HomeOwnersCoverage.Add(nodetemp.InnerText);
                                    flagHOME++;
                                    if (counthomelob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }
                                }
                                if (flagHOME == counthomelob)
                                {
                                    int tempvalue = maximumCoverage(HomeOwnersCoverage);
                                    strReturnXML = "";
                                    strReturnXML = strReturnXML.Insert(0, "<HOMEOWNERPOLICY>" + tempvalue.ToString() + "</HOMEOWNERPOLICY>");
                                    returnString.Append(strReturnXML);
                                }
                            }
                            if (lobvalue == LOB_PRIVATE_PASSENGER)
                            {
                                if (flagAUTO <= countautolob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_Auto);
                                    objDataWrapper.ClearParameteres();

                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    // Picking coverage for each policy 													
                                    nodetempautoBilo = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/PERSONALAUTOPOLICYLOWERLIMIT");
                                    if (nodetempautoBilo.InnerText != "0" && nodetempautoBilo.InnerText != "")
                                    {
                                        AutoSplitCoveragelo.Add(nodetempautoBilo.InnerText);
                                    }
                                    nodetempautoBiup = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/PERSONALAUTOPOLICYUPPERLIMIT");
                                    if (nodetempautoBiup.InnerText != "0" && nodetempautoBiup.InnerText != "")
                                    {
                                        AutoSplitCoverageup.Add(nodetempautoBiup.InnerText);
                                    }
                                    nodtempautopd = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/AUTOPD");
                                    if (nodtempautopd.InnerText != "0" && nodtempautopd.InnerText != "")
                                    {
                                        AutoPdCoverage.Add(nodtempautopd.InnerText);
                                    }
                                    nodetempautocsl = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/AUTOCSL");
                                    if (nodetempautocsl.InnerText != "0" && nodetempautocsl.InnerText != "")
                                    {
                                        AutoCslCoverage.Add(nodetempautocsl.InnerText);
                                    }
                                    nodetempautouninsuredbi = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNINSINSMOTORISTLIMITBISPLIT");
                                    // Checking uninsured motorist coverage if found any of the policy then set value 
                                    if ((nodetempautouninsuredbi.InnerText) != "" && (nodetempautouninsuredbi.InnerText) != "0")
                                    {
                                        uninsuredbi = nodetempautouninsuredbi.InnerText;
                                    }

                                    nodtempautouninsuredcsl = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNINSUNDERINSMOTORISTLIMITCSL");
                                    if ((nodtempautouninsuredcsl.InnerText) != "" && (nodtempautouninsuredcsl.InnerText) != "0")
                                    {
                                        uninsuredcsl = nodtempautouninsuredcsl.InnerText;
                                    }
                                    nodetempautounderinsuredcsl = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNDERINSMOTORISTLIMITCSL");
                                    if ((nodetempautounderinsuredcsl.InnerText) != "" && (nodetempautounderinsuredcsl.InnerText) != "0")
                                    {
                                        underinsuredcsl = nodetempautounderinsuredcsl.InnerText;
                                    }
                                    nodetempautounderinsuredbi = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNDERINSMOTORISTLIMITBISPLIT");
                                    if ((nodetempautounderinsuredbi.InnerText) != "" && (nodetempautounderinsuredbi.InnerText) != "0")
                                    {
                                        underinsuredbi = nodetempautounderinsuredbi.InnerText;
                                    }
                                    // total number of auto , motorhome , drivers of all selected policy
                                    nodetempautomobiles = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/AUTOMOBILES");
                                    totalAuto += System.Convert.ToInt32(nodetempautomobiles.InnerText);
                                    nodetempautoinexpe = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/INEXPDRIVERSAUTO");
                                    totalinexpauto += System.Convert.ToInt32(nodetempautoinexpe.InnerText);
                                    nodetempautomotorhome = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTOTHOMES");
                                    totalmotorhome += System.Convert.ToInt32(nodetempautomotorhome.InnerText);
                                    nodtempmotorinexpe = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/INEXPDRIVERSMOTORHOME");
                                    totalinexpmotro += System.Convert.ToInt32(nodtempmotorinexpe.InnerText);
                                    nodtempautomotorhomedreiver = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS");
                                    totalautomotordriver += System.Convert.ToInt32(nodtempautomotorhomedreiver.InnerText);
                                    nodtempautomatdriver = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MATUREAGEDISCOUNTAUTOMOTORHOM");
                                    // if mature driver is found in any policy then send  y in mature driver node 
                                    if ((nodtempautomatdriver.InnerText) == "Y")
                                    {
                                        maturedriverautomoto = "Y";
                                    }
                                    nodetempexcludeuninsured = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/EXCLUDEUNINSMOTORIST");
                                    // if any policy contains uninsured covarege then set exclude uninsured motorist to N
                                    if ((nodetempautouninsuredbi.InnerText) != "" || (nodtempautouninsuredcsl.InnerText) != "")
                                    {
                                        nodetempexcludeuninsured.InnerText = "N";
                                    }
                                    excludeuninsured = nodetempexcludeuninsured.InnerText;
                                    flagAUTO++;
                                    if (countautolob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }
                                }
                                // when all selected policy data is fetched  
                                if (flagAUTO == countautolob)
                                {
                                    // if split coverage found in any policy then send below written xml
                                    if (AutoSplitCoveragelo.Count != 0)
                                    {
                                        tempautobilo = maximumCoverage(AutoSplitCoveragelo);
                                        tempautoup = maximumCoverage(AutoSplitCoverageup);
                                        tempautopd = maximumCoverage(AutoPdCoverage);
                                        strReturnXML = "";
                                        strReturnXML = strReturnXML.Insert(0, "<PERSONALAUTOPOLICYLOWERLIMIT>" + tempautobilo.ToString() + "</PERSONALAUTOPOLICYLOWERLIMIT>" +
                                            "<PERSONALAUTOPOLICYUPPERLIMIT>" + tempautoup.ToString() + "</PERSONALAUTOPOLICYUPPERLIMIT>" +
                                            "<AUTOPD>" + tempautopd.ToString() + "</AUTOPD>" + "<AUTOCSL>" + "</AUTOCSL>" + "<UNINSUNDERINSMOTORISTLIMITCSL>" + uninsuredcsl + "</UNINSUNDERINSMOTORISTLIMITCSL>" +
                                            "<UNINSINSMOTORISTLIMITBISPLIT>" + uninsuredbi + "</UNINSINSMOTORISTLIMITBISPLIT>" + "<UNDERINSMOTORISTLIMITCSL>" + underinsuredcsl + "</UNDERINSMOTORISTLIMITCSL>" +
                                            "<UNDERINSMOTORISTLIMITBISPLIT>" + underinsuredbi + "</UNDERINSMOTORISTLIMITBISPLIT>" + "<AUTOMOBILES>" + totalAuto + "</AUTOMOBILES>" +
                                            "<INEXPDRIVERSAUTO>" + totalinexpauto + "</INEXPDRIVERSAUTO>" + "<MOTOTHOMES>" + totalmotorhome + "</MOTOTHOMES>" +
                                            "<INEXPDRIVERSMOTORHOME>" + totalinexpmotro + "</INEXPDRIVERSMOTORHOME>" + "<TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS>" + totalautomotordriver + "</TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS>" +
                                            "<MATUREAGEDISCOUNTAUTOMOTORHOM>" + maturedriverautomoto + "</MATUREAGEDISCOUNTAUTOMOTORHOM>" + "<EXCLUDEUNINSMOTORIST>" + excludeuninsured + "</EXCLUDEUNINSMOTORIST>");
                                        returnString.Append(strReturnXML);
                                    }
                                    // if any policy contains csl with 300000 value then set csl value to that
                                    if (AutoCslCoverage.Count != 0)
                                    {
                                        tempautocsl = maximumCoverage(AutoCslCoverage);
                                        for (int i = 0; i < AutoCslCoverage.Count; i++)
                                        {
                                            if (System.Convert.ToInt32(AutoCslCoverage[i].ToString()) <= 300000)
                                            {
                                                tempautocsl = System.Convert.ToInt32(AutoCslCoverage[i].ToString());
                                            }
                                        }

                                    }
                                    // if split limit not found then send below mentioned xml
                                    if (AutoSplitCoveragelo.Count == 0 && AutoCslCoverage.Count != 0)
                                    {
                                        strReturnXML = "";
                                        strReturnXML = strReturnXML.Insert(0, "<PERSONALAUTOPOLICYLOWERLIMIT>" + "</PERSONALAUTOPOLICYLOWERLIMIT>" +
                                            "<PERSONALAUTOPOLICYUPPERLIMIT>" + "</PERSONALAUTOPOLICYUPPERLIMIT>" +
                                            "<AUTOPD>" + "</AUTOPD>" + "<AUTOCSL>" + tempautocsl + "</AUTOCSL>" + "<UNINSUNDERINSMOTORISTLIMITCSL>" + uninsuredcsl + "</UNINSUNDERINSMOTORISTLIMITCSL>" +
                                            "<UNINSINSMOTORISTLIMITBISPLIT>" + uninsuredbi + "</UNINSINSMOTORISTLIMITBISPLIT>" + "<UNDERINSMOTORISTLIMITCSL>" + underinsuredcsl + "</UNDERINSMOTORISTLIMITCSL>" +
                                            "<UNDERINSMOTORISTLIMITBISPLIT>" + underinsuredbi + "</UNDERINSMOTORISTLIMITBISPLIT>" + "<AUTOMOBILES>" + totalAuto + "</AUTOMOBILES>" +
                                            "<INEXPDRIVERSAUTO>" + totalinexpauto + "</INEXPDRIVERSAUTO>" + "<MOTOTHOMES>" + totalmotorhome + "</MOTOTHOMES>" +
                                            "<INEXPDRIVERSMOTORHOME>" + totalinexpmotro + "</INEXPDRIVERSMOTORHOME>" + "<TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS>" + totalautomotordriver + "</TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS>" +
                                            "<MATUREAGEDISCOUNTAUTOMOTORHOM>" + maturedriverautomoto + "</MATUREAGEDISCOUNTAUTOMOTORHOM>" + "<EXCLUDEUNINSMOTORIST>" + excludeuninsured + "</EXCLUDEUNINSMOTORIST>");
                                        returnString.Append(strReturnXML);
                                    }

                                }

                            }

                            if (lobvalue == LOB_MOTORCYCLE)
                            {
                                if (flagMOTORCYCLE <= countmotorlob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_Motorcycle);
                                    objDataWrapper.ClearParameteres();

                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    // fetch coverage from selected policy
                                    nodetempautoBilo = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORCYCLEPOLICYLOWERLIMIT");
                                    if (nodetempautoBilo.InnerText != "0" && nodetempautoBilo.InnerText != "")
                                    {
                                        MotorSplitCoveragelo.Add(nodetempautoBilo.InnerText);
                                    }
                                    nodetempautoBiup = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORCYCLEPOLICYUPPERLIMIT");
                                    if (nodetempautoBiup.InnerText != "0")
                                    {
                                        MotorSplitCoverageup.Add(nodetempautoBiup.InnerText);
                                    }
                                    nodtempautopd = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORPD");
                                    if (nodtempautopd.InnerText != "0")
                                    {
                                        MotorSplitCoveragePd.Add(nodtempautopd.InnerText);
                                    }
                                    nodetempautocsl = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORCSL");
                                    if (nodetempautocsl.InnerText != "0" && nodetempautocsl.InnerText != "")
                                    {
                                        MotorCombinedCoverage.Add(nodetempautocsl.InnerText);
                                    }
                                    nodetempautouninsuredbi = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNINSUNDERINSMOTORISTLIMIT");
                                    tempununderinsuredmotorist = nodetempautouninsuredbi.InnerText;
                                    // fetch total motorcycle ,driver, inexperienced driver from all selected policy
                                    nodetempautomobiles = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORCYCLES");
                                    totalmotor += System.Convert.ToInt32(nodetempautomobiles.InnerText);
                                    nodetempautoinexpe = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/INEXPDRIVERSMOTORCYCL");
                                    totalinexpmotor += System.Convert.ToInt32(nodetempautoinexpe.InnerText);
                                    nodtempautomotorhomedreiver = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/TOTALNUMEROFMOTORCYCLEDRIVERS");
                                    totalmotorcycledriver += System.Convert.ToInt32(nodtempautomotorhomedreiver.InnerText);
                                    nodtempautomatdriver = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MATUREAGEDISCOUNTMOTORCYCLE");

                                    if ((nodtempautomatdriver.InnerText) == "Y")
                                    {
                                        maturedrivermotorcycle = "Y";
                                    }
                                    flagMOTORCYCLE++;
                                    if (countmotorlob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }

                                }
                                // when all seletced policy data is fetched then make xml
                                if (flagMOTORCYCLE == countmotorlob)
                                {
                                    // if split limti found in selected policies then send below written xml
                                    if (MotorSplitCoveragelo.Count != 0)
                                    {
                                        tempautobilo = maximumCoverage(MotorSplitCoveragelo);
                                        tempautoup = maximumCoverage(MotorSplitCoverageup);
                                        tempautopd = maximumCoverage(MotorSplitCoveragePd);
                                        strReturnXML = "";
                                        strReturnXML = strReturnXML.Insert(0, "<MOTORCYCLEPOLICYLOWERLIMIT>" + tempautobilo.ToString() + "</MOTORCYCLEPOLICYLOWERLIMIT>" +
                                            "<MOTORCYCLEPOLICYUPPERLIMIT>" + tempautoup.ToString() + "</MOTORCYCLEPOLICYUPPERLIMIT>" + "<MOTORPD>" + tempautopd.ToString() + "</MOTORPD>" +
                                            "<MOTORCSL>" + "</MOTORCSL>" + "<UNINSUNDERINSMOTORISTLIMIT>" + tempununderinsuredmotorist + "</UNINSUNDERINSMOTORISTLIMIT>" +
                                            "<MOTORCYCLES>" + totalmotor.ToString() + "</MOTORCYCLES>" + "<INEXPDRIVERSMOTORCYCL>" + totalinexpmotor.ToString() + "</INEXPDRIVERSMOTORCYCL>" +
                                            "<TOTALNUMEROFMOTORCYCLEDRIVERS>" + totalmotorcycledriver.ToString() + "</TOTALNUMEROFMOTORCYCLEDRIVERS>" + "<MATUREAGEDISCOUNTMOTORCYCLE>" + maturedrivermotorcycle + "</MATUREAGEDISCOUNTMOTORCYCLE>");
                                        returnString.Append(strReturnXML);
                                    }

                                    // if any selected policy contains csl limit with 300000 value then set csl limit to that 
                                    if (MotorCombinedCoverage.Count != 0)
                                    {
                                        tempautocsl = maximumCoverage(MotorCombinedCoverage);
                                        for (int i = 0; i < MotorCombinedCoverage.Count; i++)
                                        {
                                            if (System.Convert.ToInt32(MotorCombinedCoverage[i].ToString()) <= 300000)
                                            {
                                                tempautocsl = System.Convert.ToInt32(MotorCombinedCoverage[i].ToString());
                                            }
                                        }

                                    }
                                    // if split limit not found then send below written xml
                                    if (MotorSplitCoveragelo.Count == 0 && MotorCombinedCoverage.Count != 0)
                                    {
                                        strReturnXML = "";
                                        strReturnXML = strReturnXML.Insert(0, "<MOTORCYCLEPOLICYLOWERLIMIT>" + "</MOTORCYCLEPOLICYLOWERLIMIT>" +
                                            "<MOTORCYCLEPOLICYUPPERLIMIT>" + "</MOTORCYCLEPOLICYUPPERLIMIT>" + "<MOTORPD>" + "</MOTORPD>" +
                                            "<MOTORCSL>" + tempautocsl.ToString() + "</MOTORCSL>" + "<UNINSUNDERINSMOTORISTLIMIT>" + tempununderinsuredmotorist + "</UNINSUNDERINSMOTORISTLIMIT>" +
                                            "<MOTORCYCLES>" + totalmotor.ToString() + "</MOTORCYCLES>" + "<INEXPDRIVERSMOTORCYCL>" + totalinexpmotor.ToString() + "</INEXPDRIVERSMOTORCYCL>" +
                                            "<TOTALNUMEROFMOTORCYCLEDRIVERS>" + totalmotorcycledriver.ToString() + "</TOTALNUMEROFMOTORCYCLEDRIVERS>" + "<MATUREAGEDISCOUNTMOTORCYCLE>" + maturedrivermotorcycle + "</MATUREAGEDISCOUNTMOTORCYCLE>");
                                        returnString.Append(strReturnXML);
                                    }

                                }
                            }
                            if (lobvalue == LOB_WATERCRAFT)
                            {
                                if (flagWATERCRAFT <= countwatercraftlob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_Watercraft);
                                    objDataWrapper.ClearParameteres();
                                    // fetch watercraft details of this policy
                                    tempWatercraftDetails = FetchUmbrellaWatercraft(customerID, idValue, versionValue, dataaccessvalue);
                                    returnStringwatercraft.Append(tempWatercraftDetails);
                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    nodetemp = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/WATERCRAFTPOLICY");
                                    WatercraftCoverage.Add(nodetemp.InnerText);
                                    flagWATERCRAFT++;
                                    if (countwatercraftlob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }
                                }
                                if (flagWATERCRAFT == countwatercraftlob)
                                {
                                    int tempvalue = maximumCoverage(WatercraftCoverage);
                                    strReturnXML = "";
                                    strReturnXML = strReturnXML.Insert(0, "<WATERCRAFTPOLICY>" + tempvalue.ToString() + "</WATERCRAFTPOLICY>");
                                    returnString.Append(strReturnXML);
                                }

                            }



                            if (lobvalue == LOB_RENTAL_DWELLING)
                            {
                                if (flagRENTAL <= countdwellinglob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_dwelling);
                                    objDataWrapper.ClearParameteres();

                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    nodetemp = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/DWELLINGFIREPOLICY");
                                    DwellingCoverage.Add(nodetemp.InnerText);
                                    flagRENTAL++;
                                    if (countdwellinglob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }
                                }
                                if (flagRENTAL == countdwellinglob)
                                {
                                    int tempvalue = maximumCoverage(DwellingCoverage);
                                    strReturnXML = "";
                                    strReturnXML = strReturnXML.Insert(0, "<DWELLINGFIREPOLICY>" + tempvalue.ToString() + "</DWELLINGFIREPOLICY>");
                                    returnString.Append(strReturnXML);
                                }


                            }
                        }

                    }
                }
                /*Get Watercraft Info(start)*/
                returnString.Append("<WATERCRAFT_EXPOSURES>");
                returnString.Append(returnStringwatercraft.ToString());
                returnString.Append("</WATERCRAFT_EXPOSURES>");
                /*Get Watercraft Info(End)*/
                /*Get Primary Applicant Info(Start)*/
                strReturnXML = "";
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionId);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "APP");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcFor_Umb_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                int indexFirstname;
                indexFirstname = strReturnXML.LastIndexOf("<FIRST_NAME>");
                strReturnXML = strReturnXML.Insert(indexFirstname, "<PRIMARYAPPLICANT>");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                strReturnXML = returnString.ToString();

                /*Get Primary Applicant Info(End)*/

                returnString.Append("</UMBRELLA>");
                returnString = returnString.Replace(",", "");
                returnString.Append("</QUICKQUOTE>");
                returnString = returnString.Replace("\r\n\r\n", "");
                returnString = returnString.Replace("\r\n", "");

                string inputxml = returnString.ToString();
                XmlDocument docinputXML = new XmlDocument();
                docinputXML.LoadXml(inputxml);

                //creating node for insertion in inputxml

                XmlNode nodeautomobilepersonalvehicle, nodeumbrella, nodeapplicantinformation;
                XmlElement nodpersonalautoexposer, nodTemppersonalexposer, nodTempofficepremises, nodtemptentaldwellingunit, nodunderlyingpolicies,
                    nodautobilowerlimit, nodautobiupperlimit, nodautocsllimit, nodautopdlimit, nodhomeownerpolicylimit,
                    nodmotorcyclepolicybilowerlimit, nodmotorcyclepolicybiupperlimit, nodmotorcyclecsllimit, nodmotorcyclepdlimit, nodmotorcycleuninsuredbilimit,
                    nodmotorcycleuninsuredcsllimit, nodunderinsuerdbisplit, nodunderinsuredcsllimt,
                    nodmotorcycle, nodmotorhomes, nodautomobile, nodinexperincedauto, nodinexperincemotorcycle, nodinexperincemotorhomes,
                    nodmaturage, noddrivers, nodexecludeuninsuredmotorist, nodWatercraftpolicy, noddwellingfirepolicy;

                nodeapplicantinformation = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION");
                nodeumbrella = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA");

                // inserting parrent node
                XmlNode nodTemp = nodeumbrella.LastChild;
                nodpersonalautoexposer = docinputXML.CreateElement("PERSONAL_AUTO_EXPOSURES");
                nodeumbrella.InsertAfter(nodpersonalautoexposer, nodTemp);

                //inserting personal exposer node
                nodTemppersonalexposer = docinputXML.CreateElement("PERSONAL_EXPOSURE");
                nodpersonalautoexposer.AppendChild(nodTemppersonalexposer);
                //inserting underlying policy node
                nodunderlyingpolicies = docinputXML.CreateElement("UNDERLYINGPOLICY");
                nodpersonalautoexposer.AppendChild(nodunderlyingpolicies);


                //inserting automobilemotorvehicle exposer node
                nodeautomobilepersonalvehicle = docinputXML.CreateElement("AUTOMOBILE_MOTORVEHICLE_EXPOSURES");
                nodpersonalautoexposer.AppendChild(nodeautomobilepersonalvehicle);

                //inserting child node on personal exposer

                //office promises
                nodTempofficepremises = docinputXML.CreateElement("OFFICEPREMISES");
                XmlNode nodetempoffice = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/OFFICE_PROMISES");
                if (nodetempoffice != null)
                {
                    if (nodetempoffice.InnerText != null)
                    {
                        nodTempofficepremises.InnerText = nodetempoffice.InnerText;
                        nodeapplicantinformation.RemoveChild(nodetempoffice);
                    }
                }
                nodTemppersonalexposer.AppendChild(nodTempofficepremises);


                // rental dwelling unit
                nodtemptentaldwellingunit = docinputXML.CreateElement("RENTALDWELLINGUNIT");
                XmlNode nodtemprentallunit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/RENTAL_DWELLING_UNITS");
                if (nodtemprentallunit != null)
                {
                    nodtemptentaldwellingunit.InnerText = nodtemprentallunit.InnerText;
                    nodeapplicantinformation.RemoveChild(nodtemprentallunit);
                }
                nodTemppersonalexposer.AppendChild(nodtemptentaldwellingunit);



                // inserting node on underlying policies

                // inserting bi lower limit
                nodautobilowerlimit = docinputXML.CreateElement("PERSONALAUTOPOLICYLOWERLIMIT");
                XmlNode nodetempautobilowerlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/PERSONALAUTOPOLICYLOWERLIMIT");
                if (nodetempautobilowerlimit != null)
                {
                    nodautobilowerlimit.InnerText = nodetempautobilowerlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempautobilowerlimit);
                }

                nodunderlyingpolicies.AppendChild(nodautobilowerlimit);


                // inserting bi upper limit
                nodautobiupperlimit = docinputXML.CreateElement("PERSONALAUTOPOLICYUPPERLIMIT");
                XmlNode nodetempautobiupperlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/PERSONALAUTOPOLICYUPPERLIMIT");
                if (nodetempautobiupperlimit != null)
                {
                    nodautobiupperlimit.InnerText = nodetempautobiupperlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempautobiupperlimit);
                }

                nodunderlyingpolicies.AppendChild(nodautobiupperlimit);


                // inserting auto csl limit 
                nodautocsllimit = docinputXML.CreateElement("AUTOCSL");
                XmlNode nodetempautocsllimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/AUTOCSL");
                if (nodetempautocsllimit != null)
                {
                    nodautocsllimit.InnerText = nodetempautocsllimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempautocsllimit);
                }

                nodunderlyingpolicies.AppendChild(nodautocsllimit);


                // inserting auto pd limit
                nodautopdlimit = docinputXML.CreateElement("AUTOPD");
                XmlNode nodetempautopdlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/AUTOPD");
                if (nodetempautopdlimit != null)
                {
                    nodautopdlimit.InnerText = nodetempautopdlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempautopdlimit);
                }

                nodunderlyingpolicies.AppendChild(nodautopdlimit);


                // motorcycle bilimit
                nodmotorcyclepolicybilowerlimit = docinputXML.CreateElement("MOTORCYCLEPOLICYLOWERLIMIT");
                XmlNode nodetempmotorcyclepolicybilowerlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORCYCLEPOLICYLOWERLIMIT");
                if (nodetempmotorcyclepolicybilowerlimit != null)
                {
                    nodmotorcyclepolicybilowerlimit.InnerText = nodetempmotorcyclepolicybilowerlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcyclepolicybilowerlimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcyclepolicybilowerlimit);

                // motorcycle bi upper limit

                nodmotorcyclepolicybiupperlimit = docinputXML.CreateElement("MOTORCYCLEPOLICYUPPERLIMIT");
                XmlNode nodetempmotorcyclepolicybiupperlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORCYCLEPOLICYUPPERLIMIT");
                if (nodetempmotorcyclepolicybiupperlimit != null)
                {
                    nodmotorcyclepolicybiupperlimit.InnerText = nodetempmotorcyclepolicybiupperlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcyclepolicybiupperlimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcyclepolicybiupperlimit);


                // motor cycle pd limit
                nodmotorcyclepdlimit = docinputXML.CreateElement("MOTORPD");
                XmlNode nodetempmotorcyclepdlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORPD");
                if (nodetempmotorcyclepdlimit != null)
                {
                    nodmotorcyclepdlimit.InnerText = nodetempmotorcyclepdlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcyclepdlimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcyclepdlimit);


                // motor cycle csl limit
                nodmotorcyclecsllimit = docinputXML.CreateElement("MOTORCSL");
                XmlNode nodetempmotorcyclecsllimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORCSL");
                if (nodetempmotorcyclecsllimit != null)
                {
                    nodmotorcyclecsllimit.InnerText = nodetempmotorcyclecsllimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcyclecsllimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcyclecsllimit);


                // uninsured bilimit
                nodmotorcycleuninsuredbilimit = docinputXML.CreateElement("UNINSINSMOTORISTLIMITBISPLIT");
                XmlNode nodetempmotorcycleuninsuredbilimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/UNINSINSMOTORISTLIMITBISPLIT");
                if (nodetempmotorcycleuninsuredbilimit != null)
                {
                    nodmotorcycleuninsuredbilimit.InnerText = nodetempmotorcycleuninsuredbilimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcycleuninsuredbilimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcycleuninsuredbilimit);


                // underinsured csl limit
                nodunderinsuredcsllimt = docinputXML.CreateElement("UNDERINSMOTORISTLIMITCSL");
                XmlNode nodetempunderinsuredcsllimt = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/UNDERINSMOTORISTLIMITCSL");
                if (nodetempunderinsuredcsllimt != null)
                {
                    nodunderinsuredcsllimt.InnerText = nodetempunderinsuredcsllimt.InnerText;
                    nodeumbrella.RemoveChild(nodetempunderinsuredcsllimt);
                }

                nodunderlyingpolicies.AppendChild(nodunderinsuredcsllimt);


                //underinsured bilimit
                nodunderinsuerdbisplit = docinputXML.CreateElement("UNDERINSMOTORISTLIMITBISPLIT");
                XmlNode nodetempunderinsuerdbisplit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/UNDERINSMOTORISTLIMITBISPLIT");
                if (nodetempunderinsuerdbisplit != null)
                {
                    nodunderinsuerdbisplit.InnerText = nodetempunderinsuerdbisplit.InnerText;
                    nodeumbrella.RemoveChild(nodetempunderinsuerdbisplit);
                }
                nodunderlyingpolicies.AppendChild(nodunderinsuerdbisplit);


                //uninsurecsllimit
                nodmotorcycleuninsuredcsllimit = docinputXML.CreateElement("UNINSUNDERINSMOTORISTLIMITCSL");
                XmlNode nodetempmotorcycleuninsuredcsllimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/UNINSUNDERINSMOTORISTLIMITCSL");
                if (nodetempmotorcycleuninsuredcsllimit != null)
                {
                    nodmotorcycleuninsuredcsllimit.InnerText = nodetempmotorcycleuninsuredcsllimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcycleuninsuredcsllimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcycleuninsuredcsllimit);


                // homeowners policy limit
                nodhomeownerpolicylimit = docinputXML.CreateElement("HOMEOWNERPOLICY");
                XmlNode nodetemphomeownerpolicylimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/HOMEOWNERPOLICY");
                if (nodetemphomeownerpolicylimit != null)
                {
                    nodhomeownerpolicylimit.InnerText = nodetemphomeownerpolicylimit.InnerText;
                    nodeumbrella.RemoveChild(nodetemphomeownerpolicylimit);
                }

                nodunderlyingpolicies.AppendChild(nodhomeownerpolicylimit);


                // renatll dwelling policy limit
                noddwellingfirepolicy = docinputXML.CreateElement("DWELLINGFIREPOLICY");
                XmlNode nodetempdwellingfirepolicy = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/DWELLINGFIREPOLICY");
                if (nodetempdwellingfirepolicy != null)
                {
                    noddwellingfirepolicy.InnerText = nodetempdwellingfirepolicy.InnerText;
                    nodeumbrella.RemoveChild(nodetempdwellingfirepolicy);
                }
                nodunderlyingpolicies.AppendChild(noddwellingfirepolicy);


                // watercraft policy limit
                nodWatercraftpolicy = docinputXML.CreateElement("WATERCRAFTPOLICY");
                XmlNode nodtempWatercraftpolicy = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/WATERCRAFTPOLICY");
                if (nodtempWatercraftpolicy != null)
                {
                    nodWatercraftpolicy.InnerText = nodtempWatercraftpolicy.InnerText;
                    nodeumbrella.RemoveChild(nodtempWatercraftpolicy);
                }
                nodunderlyingpolicies.AppendChild(nodWatercraftpolicy);


                // inserting  child node on auto motor vehicle exposer 

                // total numbrr of drivers
                string strautodrivers = "0", strotherdrivers = "0";//strmotorcycledrivers = "0", 
                noddrivers = docinputXML.CreateElement("TOTALNUMBERDRIVERS");
                XmlNode nodtempdriversothers = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/OTHER_DRIVERS");
                XmlNode nodtempautodrivers = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS");
                //	XmlNode nodetempmotorcycledrivers = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/TOTALNUMEROFMOTORCYCLEDRIVERS");
                if (nodtempdriversothers != null)
                {
                    strotherdrivers = nodtempdriversothers.InnerText;
                    nodeapplicantinformation.RemoveChild(nodtempdriversothers);
                }
                if (nodtempautodrivers != null)
                {
                    strautodrivers = nodtempautodrivers.InnerText;
                    nodeumbrella.RemoveChild(nodtempautodrivers);
                }

                /*if (nodetempmotorcycledrivers != null)
                        {
                            strmotorcycledrivers = nodetempmotorcycledrivers.InnerText;
                            nodeumbrella.RemoveChild(nodetempmotorcycledrivers);
                        }*/
                int totalNumber_drivers;
                totalNumber_drivers = System.Convert.ToInt32(strautodrivers) + System.Convert.ToInt32(strotherdrivers);
                noddrivers.InnerText = totalNumber_drivers.ToString();
                nodeautomobilepersonalvehicle.AppendChild(noddrivers);



                //personal automobile 
                nodautomobile = docinputXML.CreateElement("AUTOMOBILES");
                XmlNode nodetempautomobile = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/AUTOMOBILES");
                if (nodetempautomobile != null)
                {
                    nodautomobile.InnerText = nodetempautomobile.InnerText;
                    nodeumbrella.RemoveChild(nodetempautomobile);
                }
                else
                {
                    nodautomobile.InnerText = "0";
                }
                nodeautomobilepersonalvehicle.AppendChild(nodautomobile);




                // inexperince drivers auto
                nodinexperincedauto = docinputXML.CreateElement("INEXPDRIVERSAUTO");
                XmlNode nodetempinexperincedauto = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/INEXPDRIVERSAUTO");
                if (nodetempinexperincedauto != null)
                {
                    nodinexperincedauto.InnerText = nodetempinexperincedauto.InnerText;
                    nodeumbrella.RemoveChild(nodetempinexperincedauto);
                }
                nodeautomobilepersonalvehicle.AppendChild(nodinexperincedauto);
                //nodeautomobilepersonalvehicle.AppendChild(nodinexperincedauto);


                //motorcycle
                nodmotorcycle = docinputXML.CreateElement("MOTORCYCLES");
                XmlNode nodetempmotorcycle = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORCYCLES");
                if (nodetempmotorcycle != null)
                {
                    nodmotorcycle.InnerText = nodetempmotorcycle.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcycle);
                }
                else
                {
                    nodmotorcycle.InnerText = "0";
                }
                nodeautomobilepersonalvehicle.AppendChild(nodmotorcycle);


                // inexperince drivers motorcycle
                nodinexperincemotorcycle = docinputXML.CreateElement("INEXPDRIVERSMOTORCYCL");
                XmlNode nodetempinexperincemotorcycle = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/INEXPDRIVERSMOTORCYCL");
                if (nodetempinexperincemotorcycle != null)
                {
                    nodinexperincemotorcycle.InnerText = nodetempinexperincemotorcycle.InnerText;
                    nodeumbrella.RemoveChild(nodetempinexperincemotorcycle);
                }
                nodeautomobilepersonalvehicle.AppendChild(nodinexperincemotorcycle);


                // motor homes
                nodmotorhomes = docinputXML.CreateElement("MOTOTHOMES");
                XmlNode nodetempmotorhomes = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTOTHOMES");
                if (nodetempmotorhomes != null)
                {
                    nodmotorhomes.InnerText = nodetempmotorhomes.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorhomes);
                }
                else
                {
                    nodmotorhomes.InnerText = "0";
                }
                nodeautomobilepersonalvehicle.AppendChild(nodmotorhomes);


                // inexperince drivers motor hopmes
                nodinexperincemotorhomes = docinputXML.CreateElement("INEXPDRIVERSMOTORHOME");
                XmlNode nodetempinexperincemotorhome = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/INEXPDRIVERSMOTORHOME");
                if (nodetempinexperincemotorhome != null)
                {
                    nodinexperincemotorhomes.InnerText = nodetempinexperincemotorhome.InnerText;
                    nodeumbrella.RemoveChild(nodetempinexperincemotorhome);
                }

                nodeautomobilepersonalvehicle.AppendChild(nodinexperincemotorhomes);


                // exclude uninsured motorist
                nodexecludeuninsuredmotorist = docinputXML.CreateElement("EXCLUDEUNINSMOTORIST");
                XmlNode nodtempexecludeuninsuredmotorist = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/EXCLUDEUNINSMOTORIST");
                if (nodtempexecludeuninsuredmotorist != null)
                {
                    nodexecludeuninsuredmotorist.InnerText = nodtempexecludeuninsuredmotorist.InnerText;
                    nodeumbrella.RemoveChild(nodtempexecludeuninsuredmotorist);
                }
                nodeautomobilepersonalvehicle.AppendChild(nodexecludeuninsuredmotorist);


                //mature age discount
                nodmaturage = docinputXML.CreateElement("MATUREDISCOUNT");
                XmlNode nodetempmatureageautomotorhome = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MATUREAGEDISCOUNTAUTOMOTORHOM");
                XmlNode nodetempmaturagemotorcycvle = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MATUREAGEDISCOUNTMOTORCYCLE");
                string matureagediscount = "";
                string matureagedscountmotorcycle = "0", matureagediscountmotorhome = "0";
                if (nodetempmaturagemotorcycvle != null)
                {
                    matureagedscountmotorcycle = nodetempmaturagemotorcycvle.InnerText;
                    nodeumbrella.RemoveChild(nodetempmaturagemotorcycvle);

                }
                if (nodetempmatureageautomotorhome != null)
                {
                    matureagediscountmotorhome = nodetempmatureageautomotorhome.InnerText;
                    nodeumbrella.RemoveChild(nodetempmatureageautomotorhome);

                }
                if (nodetempmaturagemotorcycvle != null || nodetempmatureageautomotorhome != null)
                {
                    if (matureagedscountmotorcycle == "Y" || matureagediscountmotorhome == "Y")
                    {
                        matureagediscount = "Y";
                    }
                    else
                    {
                        matureagediscount = "N";
                    }
                }
                else
                {
                    matureagediscount = "N";
                }
                nodmaturage.InnerText = matureagediscount;
                nodeautomobilepersonalvehicle.AppendChild(nodmaturage);




                return docinputXML.InnerXml;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }


        }
        # endregion

        #region Calling and Creating Input XML For Policy [From SPs]

        public string GetPolicyInputXML(int customerID, int policyID, int policyVersionID, string LOBID)
        {
            return GetPolicyInputXML(customerID, policyID, policyVersionID, LOBID, null);
        }

        public string GetPolicyInputXML(int customerID, int policyID, int policyVersionID, string LOBID, DataWrapper objDataWrapper)
        {

            DateTime RecordDate = DateTime.Now;
            try
            {
                string inputXML = "";
                if (LOBID != "0")
                {
                    // switch case on the basis of the lob
                    switch (LOBID)
                    {
                        case LOB_HOME:
                            inputXML = FetchPolicyHomeownersInputXML(customerID, policyID, policyVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'No Dwelling Found'/>";
                            }
                            break;
                        case LOB_PRIVATE_PASSENGER:
                            inputXML = FetchPolicyPrivatePassengerInputXML(customerID, policyID, policyVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Private Passenger quotes'/>";
                            }
                            break;
                        case LOB_MOTORCYCLE:
                            inputXML = FetchPolicyMotorcyclerInputXML(customerID, policyID, policyVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Motorcycle quotes'/>";
                            }
                            break;
                        case LOB_WATERCRAFT:
                            inputXML = FetchPolicyWatercraftInputXML(customerID, policyID, policyVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Watercraft quotes'/>";
                            }
                            break;
                        case LOB_RENTAL_DWELLING:
                            inputXML = FetchPolicyRentalDwellingInputXML(customerID, policyID, policyVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Rental Dwelling quotes'/>";
                            }
                            break;
                        case LOB_UMBRELLA:
                            inputXML = FetchPolicyUmbrellaInputXML(customerID, policyID, policyVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Umbrella quotes'/>";
                            }
                            break;
                        case LOB_AVIATION:
                            inputXML = FetchPolicyAviationInputXML(customerID, policyID, policyVersionID, objDataWrapper);
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Aviation quotes'/>";
                            }
                            break;
                        case LOB_MOTOR:
                            inputXML ="";
                            if (inputXML.Trim() == "")
                            {
                                inputXML = "<ERROR ERRFOUND = 'T' ErrMsg = 'We are currently working on generation of Motor quotes'/>";
                            }
                            break;
                        default:
                            break;
                    }

                }
                return inputXML.ToUpper();
            }

            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }


        public string FetchPolicyHomeownersInputXML(int customerID, int POLICYID, int POLICYVersionID)
        {
            return FetchPolicyHomeownersInputXML(customerID, POLICYID, POLICYVersionID, null);
        }

        public string FetchPolicyHomeownersInputXML(int customerID, int POLICYID, int POLICYVersionID, DataWrapper objDataWrapper)
        {
            //DataWrapper	objDataWrapper;
            string strStoredProcForHO = "Proc_GetPolicyRatingInformationFor_HO";
            string strStoredProcForHO_RecreationalVehicles = "Proc_GetRatingInformationForRV_Pol";
            string strStoredProcForHO_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo_Pol";
            string strStoredProcForHO_BillingInfo = "Proc_GetFireRule_BillingInfo_Pol";

            try
            {
                // Get the Dwelling ids against the customerID, POLICYID, appVersionID
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                string dwellingIDs = ClsDwellingDetails.GetPolicyDwellingID(customerID, POLICYID, POLICYVersionID);
                StringBuilder returnString = new StringBuilder();
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE>");
                //If no dewlling found
                if (dwellingIDs != "-1")
                {
                    string[] dwellingIDAndAddress = new string[0];
                    dwellingIDAndAddress = dwellingIDs.Split('~');

                    // Run a loop to get the inputXML for all the dwellings
                    for (int iCounter = 0; iCounter < dwellingIDAndAddress.Length; iCounter++)
                    {
                        string[] dwellingDetail = dwellingIDAndAddress[iCounter].Split('^');
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICYID", POLICYID);
                        objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingDetail[0]);
                        DataSet dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO);
                        objDataWrapper.ClearParameteres();

                        //string strCoverages		=	dsTempXML.Tables[1].Rows[0][0].ToString();;
                        string strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        string strReturnXML = strOtherColumns;
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_VALUE>", "");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_VALUE>", "");
                        //get the policy type.
                        string tempInput = strReturnXML.ToString();
                        XmlDocument docTemp = new XmlDocument();
                        docTemp.LoadXml(tempInput);
                        string productType = docTemp.DocumentElement.SelectSingleNode("PRODUCTNAME").InnerXml + ' ' + docTemp.DocumentElement.SelectSingleNode("PRODUCT_PREMIER").InnerXml;


                        strReturnXML = strReturnXML.Replace("<Table>", "<DWELLINGDETAILS ID= '" + dwellingDetail[0] + "' ADDRESS='" + EncodeXMLCharacters(dwellingDetail[1]) + "' POLICYTYPE='" + productType + "'>");
                        strReturnXML = strReturnXML.Replace("</Table>", "</DWELLINGDETAILS>");
                        returnString.Append(strReturnXML);
                    }
                }

                //  For recreational Vehicles
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                string recreationVehiclesIDs = ClsHomeRecrVehicles.GetPolRecreationVehicleIDs(customerID, POLICYID, POLICYVersionID);
                StringBuilder returnRVString = new StringBuilder();
                returnRVString.Remove(0, returnRVString.Length);
                returnRVString.Append("<RECREATIONVEHICLES>");

                //If no RVs found
                if (recreationVehiclesIDs != "-1")
                {
                    string[] recreationVehiclesID = new string[0];
                    recreationVehiclesID = recreationVehiclesIDs.Split('^');

                    // Run a loop to get the inputXML for all the dwellings
                    for (int iRVCounter = 0; iRVCounter < recreationVehiclesID.Length; iRVCounter++)
                    {

                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POL_ID", POLICYID);
                        objDataWrapper.AddParameter("@POL_VERSION_ID", POLICYVersionID);
                        //Asfa Praveen (24-Jan-2008) - iTrack issue #3471
                        objDataWrapper.AddParameter("@REC_VEHICLE_ID", recreationVehiclesID[iRVCounter]);
                        DataSet dsTempRVXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_RecreationalVehicles);
                        objDataWrapper.ClearParameteres();
                        string strRVColumns = ClsCommon.GetXML(dsTempRVXML.Tables[0]);

                        strRVColumns = strRVColumns.Replace("<NewDataSet>", "");
                        strRVColumns = strRVColumns.Replace("</NewDataSet>", "");
                        returnRVString.Append(strRVColumns);
                        returnRVString = returnRVString.Replace("<Table>", "<RECREATIONVEHICLE ID= '" + recreationVehiclesID[iRVCounter] + "'>");
                        returnRVString = returnRVString.Replace("</Table>", "</RECREATIONVEHICLE>");
                    }
                }
                returnRVString.Append("</RECREATIONVEHICLES>");
                returnString.Append(returnRVString.ToString());

                /*Get Primary Applicant Info*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", POLICYID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                DataSet dsAppTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                string strAppReturnXML = dsAppTempXML.GetXml();
                strAppReturnXML = strAppReturnXML.Replace("<NewDataSet>", "");
                strAppReturnXML = strAppReturnXML.Replace("</NewDataSet>", "");
                strAppReturnXML = strAppReturnXML.Replace("<Table>", "");
                strAppReturnXML = strAppReturnXML.Replace("</Table>", "");
                strAppReturnXML = strAppReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strAppReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*Get Primary Applicant Info*/
                ClsRatingAndUnderwritingRules OBJ = new ClsRatingAndUnderwritingRules("S001");
                string PlanIDs = OBJ.GetPlanIDs_Pol(customerID, POLICYID, POLICYVersionID);
                int intPlanNo = 0;
                string[] PlanID = new string[0];
                if (PlanIDs != "-1")
                {
                    PlanID = PlanIDs.Split('^');
                    intPlanNo = PlanID.Length;
                }
                returnString.Append("<BillingInfoS COUNT='" + intPlanNo + "'>");
                if (PlanIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < PlanID.Length; iCounter++)
                    {
                        string strLocationID = PlanID[iCounter];
                        returnString.Append("<Plan ID='" + PlanID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                        objDataWrapper.AddParameter("@POLICY_ID", POLICYID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICYVersionID);
                        objDataWrapper.AddParameter("@PLAN_ID", PlanID[iCounter]);
                        // get Location details
                        DataSet dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_BillingInfo);
                        string strbILLINGReturnXML = dsAppTempXML.GetXml();
                        objDataWrapper.ClearParameteres();
                        strbILLINGReturnXML = dsTempXML.GetXml();
                        strbILLINGReturnXML = ReplaceString(strbILLINGReturnXML);
                        returnString.Append(strbILLINGReturnXML);
                        returnString.Append("</Plan>");
                    }
                }

                returnString.Append("</BillingInfoS>");

                returnString.Append("</QUICKQUOTE>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        public string FetchPolicyPrivatePassengerInputXML(int customerID, int POLICYID, int POLICYVersionID)
        {
            return FetchPolicyPrivatePassengerInputXML(customerID, POLICYID, POLICYVersionID, null);
        }

        public string FetchPolicyPrivatePassengerInputXML(int customerID, int POLICYID, int POLICYVersionID, DataWrapper objDataWrapper)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForAuto_DriverComponent = "Proc_GetPolicyRatingInformationForAuto_DriverComponent";
            string strStoredProcForAuto_VehicleComponent = "Proc_GetPolicyRatingInformationForAuto_VehicleComponent";
            string strStoredProcForAuto_VehicleCovgComponent = "Proc_GetPolicyRatingInformationForAuto_VehicleCovgComponent";
            string strStoredProcForAuto_ViolationsComponent = "Proc_GetPolicyRatingInformationForAuto_DriverViolationComponent";
            string strStoredProcForAuto_AppComponent = "Proc_GetPolicyRatingInformationForAuto_AppComponent";
            string strStoredProcForAuto_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo_Pol";
            string strReturnXML = "", strAssidrivers = "";
            int intassidriveVio = 0, intassidriverAcci = 0;
            ClsQuickQuote objQuickQuote = new ClsQuickQuote();
            string[] assinOlddriverID = new string[0];
            string strpolicy;  //Policy
            string strMVRPoints = ""; //MVR Points
            int violationPoints = 0;
            int accidentPoints = 0;
            DataSet dsTempXML;
            XmlDocument xmlDocViolAcc = new XmlDocument();

            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", POLICYID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_AppComponent);
                objDataWrapper.ClearParameteres();

                //strReturnXML = dsTempXML.GetXml();
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");

                returnString.Append(strReturnXML);
                //Assigning the policy node  : PK
                strpolicy = strReturnXML;
                //END
                /*----------Primary Applicant Info--------*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", POLICYID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                //				if(IsEODProcess)
                //				{
                //					objDataWrapper.AddParameter("@USERID",EODUserID.ToString());
                //				}
                //				else
                //				{
                //					objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                //				}
                objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*----------Primary Applicant Info--------*/

                returnString.Append("</POLICY>");
                strReturnXML = "";


                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the vehicle details for each vehicle
                returnString.Append("<VEHICLES>");
                string vehicleIDs = ClsGeneralInformation.GetVehicleIDsPolicy(customerID, POLICYID, POLICYVersionID, objDataWrapper);
                if (vehicleIDs != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = vehicleIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<VEHICLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICYID", POLICYID);
                        objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_VehicleComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");

                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");

                        //Add node for each vehicle  for count
                        int rowID = iCounter + 1;
                        string vehicleRowIDNode = "<VEHICLEROWID>" + rowID.ToString() + "</VEHICLEROWID>";
                        returnString.Append(vehicleRowIDNode);
                        returnString.Append(strReturnXML);
                        objDataWrapper.ClearParameteres();
                        //Get coverages
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICYID", POLICYID);
                        objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_VehicleCovgComponent);
                        objDataWrapper.ClearParameteres();

                        if (dsTempXML.Tables[0] != null && dsTempXML.Tables[0].Rows.Count > 0)
                        {
                            strReturnXML = dsTempXML.Tables[0].Rows[0][0].ToString();
                            returnString.Append(strReturnXML);
                        }
                        //string setvalue = "", strVioDrvID = "";
                        string AssinegDriverId = ClsDriverDetail.GetAssinedDriverId(customerID, POLICYID, POLICYVersionID, int.Parse(vehicleID[iCounter]), objDataWrapper, "POL");
                        strAssidrivers = strAssidrivers + "^" + AssinegDriverId;
                        if (AssinegDriverId != "-1")
                        {
                            string[] assindriverID = new string[0];
                            assindriverID = AssinegDriverId.Split('^');
                            for (int iAssCounter = 0; iAssCounter < assindriverID.Length; iAssCounter++)
                            {
                                DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID, POLICYID, POLICYVersionID, int.Parse(assindriverID[iAssCounter].ToString()), "POL", objDataWrapper);
                                if (dtMvr != null && dtMvr.Rows.Count > 0)
                                {
                                    intassidriveVio = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                    intassidriverAcci = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                                }
                                returnString.Append("<ASSIGNDRIVIOACCPNT" + " " + "ID ='" + assindriverID[iAssCounter].ToString() + "'><SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS></ASSIGNDRIVIOACCPNT>");
                            }
                            // if  driver id found in old assign driver array
                            /*if(assinOlddriverID.Length > 0)
                                {
                                    foreach(string assin in assinOlddriverID)
                                        {
                                            // if driver's point have been charged for any vehicle
                                            if(assin == assindriverID[iAssCounter].ToString())
                                            {
                                                setvalue ="false";
                                            }
										
                                        }
                                        if(setvalue=="false")
                                        {
                                            strVioDrvID="";
                                        }
                                        else
                                        {
                                            strVioDrvID=assindriverID[iAssCounter].ToString();
                                        }
                                        if(strVioDrvID!="")
                                        {
                                            // if already driver' point did not charged for any vehicle 
                                            DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID,POLICYID,POLICYVersionID,int.Parse(assindriverID[iAssCounter].ToString()),"POL",objDataWrapper);
                                            if(dtMvr!=null && dtMvr.Rows.Count>0)	
                                            {
                                                intassidriveVio = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                                intassidriverAcci = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                                            }														
                                            returnString.Append("<ASSIGNDRIVIOACCPNT" +" " + "ID ='" + assindriverID[iAssCounter].ToString() + "'><SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS></ASSIGNDRIVIOACCPNT>");
											
                                        }
									
                                }
                                    // if no driver id found in old assign driver array
                                else
                                {
                                    DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID,POLICYID,POLICYVersionID,int.Parse(assindriverID[iAssCounter].ToString()),"POL",objDataWrapper);
                                    if(dtMvr!=null && dtMvr.Rows.Count>0)	
                                    {
                                        intassidriveVio = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                        intassidriverAcci = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                                    }														
                                    returnString.Append("<ASSIGNDRIVIOACCPNT" +" " + "ID ='" + assindriverID[iAssCounter].ToString() + "'><SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS></ASSIGNDRIVIOACCPNT>");
										
                                }
                                setvalue="";
                            }
                            assinOlddriverID = strAssidrivers.Split('^');*/
                        }
                        returnString.Append("</VEHICLE>");
                    }
                }
                returnString.Append("</VEHICLES>");

                //get the driver details for each driver
                returnString.Append("<DRIVERS>");
                string driverIDs = ClsDriverDetail.GetDriverIDs_forPolicy(customerID, POLICYID, POLICYVersionID, objDataWrapper);
                if (driverIDs != "-1")
                {
                    string[] driverID = new string[0];
                    driverID = driverIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        returnString.Append("<DRIVER ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICYID", POLICYID);
                        objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_DriverComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                        // LOAD XML TO FETCH SUM OF VIOLATION AND ACCIDENT POINT
                        xmlDocViolAcc.LoadXml(strReturnXML);
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                        returnString.Append(strReturnXML);

                        //for each driver also get the violation detials (if any)
                        returnString.Append("<VIOLATIONS>");
                        string violationIDs = ClsDriverDetail.GetViolationIDsForPolicy(customerID, POLICYID, POLICYVersionID, int.Parse(driverID[iCounter].ToString()), objDataWrapper);

                        int sumofViolationPoints = 0, sumofAccidentPoints = 0, sumofMvrPoints = 0, MvrPoints = 0; violationPoints = 0; accidentPoints = 0;//Set Violation Variables
                        StringBuilder strViolationNodes = new StringBuilder(); //For Driver Violations .

                        if (violationIDs != "-1")
                        {
                            string[] violationID = new string[0];
                            violationID = violationIDs.Split('^');

                            strViolationNodes.Remove(0, strViolationNodes.Length);

                            // Run a loop to get the inputXML for each vehicleID
                            for (int iCounterForViolations = 0; iCounterForViolations < violationID.Length; iCounterForViolations++)
                            {
                                //For just Violation Nodes
                                strViolationNodes.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>");
                                //End				
                                returnString.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>");
                                objDataWrapper.ClearParameteres();
                                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                                objDataWrapper.AddParameter("@POLICYID", POLICYID);
                                objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                                objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                                //objDataWrapper.AddParameter("@VIOLATIONID",violationID[iCounterForViolations]);	
                                //Modified it to @POL_MVR_ID from @VIOLATIONID
                                objDataWrapper.AddParameter("@POL_MVR_ID", violationID[iCounterForViolations]);

                                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_ViolationsComponent);
                                objDataWrapper.ClearParameteres();

                                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 16 June 2008 : praveen
                                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");

                                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("<Table>", "");
                                strReturnXML = strReturnXML.Replace("</Table>", "");
                                //Appending the Violations 
                                strViolationNodes.Append(strReturnXML);
                                returnString.Append(strReturnXML);
                                //End 
                                returnString.Append("</VIOLATION>");
                                //Appending the Violations 
                                strViolationNodes.Append("</VIOLATION>");
                                //End 
                            }
                        }
                        //Set the values of the variables Calling method to Calculate points:
                        if ((xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "PPA^PRINCIPAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "OPA^OCCASIONAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "YOPA^OCCASIONAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "YPPA^PRINCIPAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "NR^NR"))
                        {
                            DataTable dtTemp = objQuickQuote.GetMVRPointsForSurcharge(customerID, POLICYID, POLICYVersionID, int.Parse(driverID[iCounter].ToString()), "POL", objDataWrapper);
                            //int MVR_Points = 0;
                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                MvrPoints = int.Parse(dtTemp.Rows[0]["MVR_POINTS"].ToString());
                                violationPoints = int.Parse(dtTemp.Rows[0]["SUM_MVR_POINTS"].ToString());
                                accidentPoints = int.Parse(dtTemp.Rows[0]["ACCIDENT_POINTS"].ToString());
                            }
                        }
                        strMVRPoints = "<POINTS><MVR>" + MvrPoints.ToString() + "</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() + "</SUMOFACCIDENTPOINTS></POINTS>";
                        returnString.Append("</VIOLATIONS>");
                        //**
                        //Set the nodes of sum of violation n accident points for each driver
                        XmlDocument PointsDoc = new XmlDocument();
                        PointsDoc.LoadXml(strMVRPoints);
                        XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
                        if (PointNode != null)
                        {

                            sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
                            sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                            sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());

                            returnString.Append("<MVR>");
                            returnString.Append(sumofMvrPoints);
                            returnString.Append("</MVR>");

                            returnString.Append("<SUMOFVIOLATIONPOINTS>");
                            returnString.Append(sumofViolationPoints);
                            returnString.Append("</SUMOFVIOLATIONPOINTS>");

                            returnString.Append("<SUMOFACCIDENTPOINTS>");
                            returnString.Append(sumofAccidentPoints);
                            returnString.Append("</SUMOFACCIDENTPOINTS>");

                        }
                        //END
                        //**

                        returnString.Append("</DRIVER>");

                    }
                }
                returnString.Append("</DRIVERS>");

                returnString.Append("</QUICKQUOTE>");
                returnString = returnString.Replace(",", "");
                returnString = returnString.Replace("\r\n", "");
                //**Start 
                //*****Loading Documnet and Setting the VIOLATION POINTS at VEHICLE LEVEL : 21 April 2006
                //**Loading the Appended XML form Procedures and Appending and Setting the Violation NODES**//
                string strOutXml = "";
                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                int mvrPoints = 0, sumOFviolations = 0, sumOfAccidents = 0;
                foreach (XmlNode VehicleVioNode in returnXMLDoc.SelectNodes("//VEHICLES/*"))
                {
                    int VehID = int.Parse(VehicleVioNode.Attributes["ID"].Value.ToString().Trim());
                    mvrPoints = 0; sumOFviolations = 0; sumOfAccidents = 0;
                    foreach (XmlNode DriverVioNode in returnXMLDoc.SelectNodes("//DRIVERS/*"))
                    {
                        if (DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString() != "" && DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR") != null && Convert.ToInt32(DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString()) == VehID)
                        {
                            mvrPoints += int.Parse(DriverVioNode.SelectSingleNode("MVR").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("MVR").InnerText.ToString());
                            sumOFviolations += int.Parse(DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                            sumOfAccidents += int.Parse(DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                        }
                    }

                    //**Creating New Nodes for VIOLATIONS (CYCL)**//
                    XmlElement mvrNode = returnXMLDoc.CreateElement("MVR");
                    XmlElement vioNode = returnXMLDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                    XmlElement accidentNode = returnXMLDoc.CreateElement("SUMOFACCIDENTPOINTS");
                    //Set the text to node
                    mvrNode.InnerText = mvrPoints.ToString();
                    vioNode.InnerText = sumOFviolations.ToString();
                    accidentNode.InnerText = sumOfAccidents.ToString();
                    /*
                    XmlText mvrtext = returnXMLDoc.CreateTextNode(mvrPoints.ToString());
                    XmlText viotext = returnXMLDoc.CreateTextNode(sumOFviolations.ToString());
                    XmlText accidenttext = returnXMLDoc.CreateTextNode(sumOfAccidents.ToString());
                    */
                    //Append the nodes 
                    VehicleVioNode.AppendChild(mvrNode);
                    VehicleVioNode.AppendChild(vioNode);
                    VehicleVioNode.AppendChild(accidentNode);
                    //Save the value of the fields into the nodes
                    /*mvrNode.AppendChild(mvrtext);
                    vioNode.AppendChild(viotext);
                    accidentNode.AppendChild(accidenttext);
                    */
                    strOutXml = returnXMLDoc.OuterXml;

                }
                //**END
                return strOutXml.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        public string FetchPolicyMotorcyclerInputXML(int customerID, int POLICYID, int POLICYVersionID)
        {
            return FetchPolicyMotorcyclerInputXML(customerID, POLICYID, POLICYVersionID, null);
        }


        public string FetchPolicyMotorcyclerInputXML(int customerID, int policyID, int policyVersionID, DataWrapper objDataWrapper)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForMotorcycle_Policy_DriverComponent = "Proc_GetRatingInformationForMotorcycle_Policy_DriverComponent";
            string strStoredProcForMotorcycle_Policy_VehicleComponent = "Proc_GetRatingInformationForMotorcycle_Policy_VehicleComponent";
            string strStoredProcForMotorcycle_Policy_VehicleCovgComponent = "Proc_GetRatingInformationForMotorcycle_Policy_VehicleCovgComponent";
            string strStoredProcForMotorcycle_Policy_ViolationsComponent = "Proc_GetRatingInformationForMotorcycle_Policy_DriverViolationComponent";
            string strStoredProcForMotorcycle_Policy_policyComponent = "Proc_GetRatingInformationFormMotorcycle_Policy_PolicyComponent";
            string strStoredProcForMotorcycle_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo_Pol";
            string strReturnXML = "", strAssidrivers = "";
            int intassidriveVio = 0, intassidriverAcci = 0;
            ClsQuickQuote objQuickQuote = new ClsQuickQuote();
            string strpolicy;  //Policy
            string strMVRPoints = "";//, strVioId = "", strvio = "", strviolationXML = "";//MVR Points
            int violationPoints = 0;
            int accidentPoints = 0;
            //int temptotalpoints = 0;
            int intassidriverMicPoint = 0;
            //int strLenght = 0;
            DataSet dsTempXML;
            XmlDocument xmlDocViolAcc = new XmlDocument();
            //XmlNode nodetempviolation, nodetempviolationId;

            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLID", policyID);
                objDataWrapper.AddParameter("@POLVERSIONID", policyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_Policy_policyComponent);
                objDataWrapper.ClearParameteres();

                strReturnXML = dsTempXML.GetXml();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");

                returnString.Append(strReturnXML);
                //Assigning the policy node  : PK
                strpolicy = strReturnXML;
                //End 
                /*----------Primary Applicant Info--------*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", policyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", policyVersionID);
                //				if(IsEODProcess)
                //				{
                //					objDataWrapper.AddParameter("@USERID",EODUserID.ToString());
                //				}
                //				else
                //				{
                //					objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                //				}
                objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*----------Primary Applicant Info--------*/
                /*Get Primary Applicant Info*/

                returnString.Append("</POLICY>");
                strReturnXML = "";


                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the vehicle details for each vehicle
                //policyID,int policyVersionID
                returnString.Append("<VEHICLES>");
                string vehicleIDs = ClsGeneralInformation.GetVehicleIDsPolicy(customerID, policyID, policyVersionID);
                if (vehicleIDs != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = vehicleIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<VEHICLE ID='" + vehicleID[iCounter] + "'>");

                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLID", policyID);
                        objDataWrapper.AddParameter("@POLVERSIONID", policyVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_Policy_VehicleComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");

                        //Add node for each vehicle  for count
                        int rowID = iCounter + 1;
                        string vehicleRowIDNode = "<VEHICLEROWID>" + rowID.ToString() + "</VEHICLEROWID>";
                        returnString.Append(vehicleRowIDNode);
                        returnString.Append(strReturnXML);

                        //Get coverages
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLID", policyID);
                        objDataWrapper.AddParameter("@POLVERSIONID", policyVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_Policy_VehicleCovgComponent);
                        objDataWrapper.ClearParameteres();

                        if (dsTempXML.Tables[0] != null && dsTempXML.Tables[0].Rows.Count > 0)
                        {
                            strReturnXML = dsTempXML.Tables[0].Rows[0][0].ToString();
                            returnString.Append(strReturnXML);
                        }
                        /************** START ITRACK # 5081 Manoj Rathore ***************************/

                        //string setvalue = "";
                        int intVio = 0, intAcci = 0, intMis = 0;//intMvr = 0,
                        string AssinegDriverId = ClsDriverDetail.GetMotorAssinedDriverId(customerID, policyID, policyVersionID, int.Parse(vehicleID[iCounter]), objDataWrapper, "POL");
                        strAssidrivers = strAssidrivers + "^" + AssinegDriverId;
                        if (AssinegDriverId != "-1")
                        {
                            string[] assindriverID = new string[0];
                            assindriverID = AssinegDriverId.Split('^');
                            for (int iAssCounter = 0; iAssCounter < assindriverID.Length; iAssCounter++)
                            {
                                DataTable dtMvr = objQuickQuote.GetMVRPointsForSurcharge(customerID, policyID, policyVersionID, int.Parse(assindriverID[iAssCounter].ToString()), "POL", objDataWrapper);
                                if (dtMvr != null && dtMvr.Rows.Count > 0)
                                {
                                    intassidriveVio = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
                                    intassidriverAcci = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
                                    intassidriverMicPoint = int.Parse(dtMvr.Rows[0]["SUMOFMISCPOINTS"].ToString());
                                    intVio += intassidriveVio;
                                    intAcci += intassidriverAcci;
                                    intMis += intassidriverMicPoint;
                                }
                                //returnString.Append("<ASSIGNDRIVIOACCPNT" +" " + "ID ='" + assindriverID[iAssCounter].ToString() + "'><SUMOFVIOLATIONPOINTS>" + intassidriveVio + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intassidriverAcci.ToString() + "</SUMOFACCIDENTPOINTS><SUMOFMISCPOINTS>" + intassidriverMicPoint.ToString() + "</SUMOFMISCPOINTS></ASSIGNDRIVIOACCPNT>");
                            }
                            returnString.Append("<SUMOFVIOLATIONPOINTS>" + intVio.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + intAcci.ToString() + "</SUMOFACCIDENTPOINTS><SUMOFMISCPOINTS>" + intMis.ToString() + "</SUMOFMISCPOINTS>");
                        }

                        /************** END ITRACK # 5081 *******************************************/

                        returnString.Append("</VEHICLE>");

                    }
                    intassidriveVio = 0;
                    intassidriverAcci = 0;
                    intassidriverMicPoint = 0;
                }
                returnString.Append("</VEHICLES>");

                //get the driver details for each driver
                returnString.Append("<DRIVERS>");
                string driverIDs = ClsDriverDetail.GetDriverIDs_forPolicy(customerID, policyID, policyVersionID);
                if (driverIDs != "-1")
                {
                    string[] driverID = new string[0];
                    driverID = driverIDs.Split('^');


                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        returnString.Append("<DRIVER ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLID", policyID);
                        objDataWrapper.AddParameter("@POLVERSIONID", policyVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_Policy_DriverComponent);
                        objDataWrapper.ClearParameteres();

                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                        // LOAD XML TO FETCH SUM OF VIOLATION AND ACCIDENT POINT
                        xmlDocViolAcc.LoadXml(strReturnXML);
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<Table>", "");
                        strReturnXML = strReturnXML.Replace("</Table>", "");
                        strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                        strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                        returnString.Append(strReturnXML);

                        //for each driver also get the violation detials (if any)
                        returnString.Append("<VIOLATIONS>");
                        string violationIDs = ClsDriverDetail.GetViolationIDsForPolicy(customerID, policyID, policyVersionID, int.Parse(driverID[iCounter].ToString()));

                        int sumofViolationPoints = 0, sumofAccidentPoints = 0, sumofMvrPoints = 0, MvrPoints = 0; //Set Violation Variables
                        StringBuilder strViolationNodes = new StringBuilder(); //For Driver Violations .
                        //strViolationNodes.Append("<VIOLATIONS>");
                        if (violationIDs != "-1")
                        {
                            string[] violationID = new string[0];
                            violationID = violationIDs.Split('^');

                            strViolationNodes.Remove(0, strViolationNodes.Length);


                            // Run a loop to get the inputXML for each vehicleID
                            for (int iCounterForViolations = 0; iCounterForViolations < violationID.Length; iCounterForViolations++)
                            {
                                //For just Violation Nodes
                                strViolationNodes.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>");
                                //
                                returnString.Append("<VIOLATION ID='" + violationID[iCounterForViolations] + "'>");
                                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                                objDataWrapper.AddParameter("@POLID", policyID);
                                objDataWrapper.AddParameter("@POLVERSIONID", policyVersionID);
                                objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                                //Modified it to @POL_MVR_ID from @VIOLATIONID
                                objDataWrapper.AddParameter("@POL_MVR_ID", violationID[iCounterForViolations]);
                                //objDataWrapper.AddParameter("@VIOLATIONID",violationID[iCounterForViolations]);	
                                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMotorcycle_Policy_ViolationsComponent);
                                objDataWrapper.ClearParameteres();

                                strReturnXML = dsTempXML.GetXml();
                                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");

                                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                                strReturnXML = strReturnXML.Replace("<Table>", "");
                                strReturnXML = strReturnXML.Replace("</Table>", "");
                                // addeded by praveen 
                                //returnString.Append(strReturnXML);
                                //returnString.Append("</VIOLATION>");

                                //Appending the Violations 
                                strViolationNodes.Append(strReturnXML);
                                returnString.Append(strReturnXML);
                                //End 
                                returnString.Append("</VIOLATION>");
                                //Appending the Violations 
                                strViolationNodes.Append("</VIOLATION>");
                                //End 
                            }
                        }
                        //-------------- ITRACK # 5081 
                        //Set the values of the variables Calling method to Calculate points:
                        if ((xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "PPA^PRINCIPAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "OPA^OCCASIONAL") || (xmlDocViolAcc.SelectSingleNode("NewDataSet/Table/VEHICLEDRIVEDASCODE").InnerText == "NR^NR"))
                        {
                            DataTable dtTemp = objQuickQuote.GetMVRPointsForSurcharge(customerID, policyID, policyVersionID, int.Parse(driverID[iCounter].ToString()), "POL", objDataWrapper);
                            //int MVR_Points = 0;
                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                MvrPoints = int.Parse(dtTemp.Rows[0]["MVR_POINTS"].ToString());
                                violationPoints = int.Parse(dtTemp.Rows[0]["SUM_MVR_POINTS"].ToString());
                                accidentPoints = int.Parse(dtTemp.Rows[0]["ACCIDENT_POINTS"].ToString());
                            }
                        }
                        strMVRPoints = "<POINTS><MVR>" + MvrPoints.ToString() + "</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() + "</SUMOFACCIDENTPOINTS></POINTS>";
                        returnString.Append("</VIOLATIONS>");
                        //Set the nodes of sum of violation n accident points for each driver
                        XmlDocument PointsDoc = new XmlDocument();
                        PointsDoc.LoadXml(strMVRPoints);
                        XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
                        if (PointNode != null)
                        {

                            sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
                            sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                            sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());

                            returnString.Append("<MVR>");
                            returnString.Append(sumofMvrPoints);
                            returnString.Append("</MVR>");

                            returnString.Append("<SUMOFVIOLATIONPOINTS>");
                            returnString.Append(sumofViolationPoints);
                            returnString.Append("</SUMOFVIOLATIONPOINTS>");

                            returnString.Append("<SUMOFACCIDENTPOINTS>");
                            returnString.Append(sumofAccidentPoints);
                            returnString.Append("</SUMOFACCIDENTPOINTS>");

                        }
                        returnString.Append("</DRIVER>");

                    }
                }
                returnString.Append("</DRIVERS>");
                returnString.Append("</QUICKQUOTE>");
                returnString = returnString.Replace(",", "");
                returnString = returnString.Replace("\r\n", "");
                //**Start 
                //*****Loading Documnet and Setting the VIOLATION POINTS at VEHICLE LEVEL : 21 April 2006
                //**Loading the Appended XML form Procedures and Appending and Setting the Violation NODES**//
                string strOutXml = "";
                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                int mvrPoints = 0, sumOFviolations = 0, sumOfAccidents = 0;
                foreach (XmlNode VehicleVioNode in returnXMLDoc.SelectNodes("//VEHICLES/*"))
                {
                    int VehID = int.Parse(VehicleVioNode.Attributes["ID"].Value.ToString().Trim());
                    mvrPoints = 0; sumOFviolations = 0; sumOfAccidents = 0;
                    foreach (XmlNode DriverVioNode in returnXMLDoc.SelectNodes("//DRIVERS/*"))
                    {
                        if (DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR") != null && DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString() != "" && Convert.ToInt32(DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString()) == VehID)
                        {
                            mvrPoints += int.Parse(DriverVioNode.SelectSingleNode("MVR").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("MVR").InnerText.ToString());
                            sumOFviolations += int.Parse(DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                            sumOfAccidents += int.Parse(DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString() == "" ? "0" : DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                        }
                    }

                    //**Creating New Nodes for VIOLATIONS (CYCL)**//
                    XmlElement mvrNode = returnXMLDoc.CreateElement("MVR");
                    XmlElement vioNode = returnXMLDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                    XmlElement accidentNode = returnXMLDoc.CreateElement("SUMOFACCIDENTPOINTS");
                    //Set the text to node
                    mvrNode.InnerText = mvrPoints.ToString();
                    vioNode.InnerText = sumOFviolations.ToString();
                    accidentNode.InnerText = sumOfAccidents.ToString();
                    /*
                    XmlText mvrtext = returnXMLDoc.CreateTextNode(mvrPoints.ToString());
                    XmlText viotext = returnXMLDoc.CreateTextNode(sumOFviolations.ToString());
                    XmlText accidenttext = returnXMLDoc.CreateTextNode(sumOfAccidents.ToString());
                    */
                    //Append the nodes 
                    //VehicleVioNode.AppendChild(mvrNode);
                    //VehicleVioNode.AppendChild(vioNode);
                    //VehicleVioNode.AppendChild(accidentNode);
                    //Save the value of the fields into the nodes
                    /*mvrNode.AppendChild(mvrtext);
                    vioNode.AppendChild(viotext);
                    accidentNode.AppendChild(accidenttext);
                    */

                    #region Comment on 27th Apr 2009
                    /*
							strViolationNodes.Append("</VIOLATIONS>");
							strviolationXML = strViolationNodes.ToString();
							XmlDocument xmlDoc = new XmlDocument();
							xmlDoc.LoadXml(strviolationXML);
							XmlNode nodtempViolation = xmlDoc.SelectSingleNode("VIOLATIONS");
					
							//Set the values of the variables Calling method to Calculate points:
							/*string strMVRPointsForSurchargeCyclXML = "<ViolationsMVRPoints>" + strpolicy + "<violations>" + strViolationNodes.ToString() +  "</violations>" + "</ViolationsMVRPoints>";
							ClsQuickQuote ClsQQobj = new ClsQuickQuote();
							DataTable dtMvr = ClsQQobj.GetMVRPointsForSurcharge(customerID,policyID,policyVersionID,int.Parse(driverID[iCounter].ToString()),"POL");
							//strMVRPoints = ClsQQobj.GetMVRPointsForSurcharge(strMVRPointsForSurchargeCyclXML,"CYCL");
							//End 
							if(dtMvr!=null && dtMvr.Rows.Count>0)	
							{
								violationPoints = int.Parse(dtMvr.Rows[0]["SUM_MVR_POINTS"].ToString());
								accidentPoints = int.Parse(dtMvr.Rows[0]["ACCIDENT_POINTS"].ToString());
							}					
				
							strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>" + violationPoints.ToString() + "</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>" + accidentPoints.ToString() +  "</SUMOFACCIDENTPOINTS></POINTS>";
						
							
						}
						else
						{
							//if No Violations are Selected //
							strMVRPoints  = "<POINTS><MVR>0</MVR><SUMOFVIOLATIONPOINTS>0</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>-1</SUMOFACCIDENTPOINTS></POINTS>";
						}
						returnString.Append("</VIOLATIONS>");

						//Set the nodes of sum of violation n accident points for each driver
						XmlDocument PointsDoc = new XmlDocument();
						PointsDoc.LoadXml(strMVRPoints);
						XmlNode PointNode = PointsDoc.SelectSingleNode("//POINTS");
						if(PointNode!=null)
						{
							
							sumofMvrPoints = Convert.ToInt32(PointNode.SelectSingleNode("MVR").InnerText.ToString());
							sumofViolationPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
							sumofAccidentPoints = Convert.ToInt32(PointNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                
							returnString.Append("<MVR>");
							returnString.Append(sumofMvrPoints);
							returnString.Append("</MVR>");

							returnString.Append("<SUMOFVIOLATIONPOINTS>");
							returnString.Append(sumofViolationPoints);
							returnString.Append("</SUMOFVIOLATIONPOINTS>");

							returnString.Append("<SUMOFACCIDENTPOINTS>");
							returnString.Append(sumofAccidentPoints);
							returnString.Append("</SUMOFACCIDENTPOINTS>");
							
						}*/
                    //END
                    /*strViolationNodes.Append("</VIOLATIONS>");
                            strviolationXML = strViolationNodes.ToString();
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(strviolationXML);
                            XmlNode nodtempViolation = xmlDoc.SelectSingleNode("VIOLATIONS");
					
                            foreach(XmlNode nodchild in nodtempViolation.ChildNodes)
                            {
                                nodetempviolationId = xmlDoc.SelectSingleNode("VIOLATIONS/VIOLATION/@ID");
                                strVioId=nodetempviolationId.InnerText;
                                nodetempviolation = xmlDoc.SelectSingleNode("VIOLATIONS/VIOLATION[@ID='"+ strVioId +"']/MVR");
                                temptotalpoints += System.Convert.ToInt32(nodetempviolation.InnerText);

                            }
                            strLenght =strviolationXML.Length; 
                            //strvio = "<MVR>0</MVR><SUMOFVIOLATIONPOINTS>+"+temptotalpoints+"+</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS>";
                            strviolationXML = strviolationXML.Insert(strLenght,"<MVR>0</MVR><SUMOFVIOLATIONPOINTS>"+temptotalpoints+"</SUMOFVIOLATIONPOINTS><SUMOFACCIDENTPOINTS>0</SUMOFACCIDENTPOINTS>");
                            /*XmlElement elmnttempMVR = xmlDoc.CreateElement("MVR");
                            elmnttempMVR.InnerText ="0"; 
                            XmlElement elmnttempVio = xmlDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                            elmnttempVio.InnerText =temptotalpoints.ToString(); 
                            XmlElement elmnttempAcci = xmlDoc.CreateElement("SUMOFACCIDENTPOINTS");
                            elmnttempAcci.InnerText =temptotalpoints.ToString();*/
                    //nodtempViolation.InsertAfter(elmnttempAcci,nodtempViolation);*/

                    /*
                                returnString.Append(strviolationXML);
                            }
                            returnString.Append("</DRIVER>");
                        }
                    }
                    //Node placed outside to match starting tag of <DRIVERS>.Earlier when tag was inside it causing problem when there was 1 driver and tried to delete it.It would not go inside the loop because driverIDs = -1 - Done by Sibin for Itrack issue 5226
                    returnString.Append("</DRIVERS>");
                    returnString.Append("</QUICKQUOTE>");
                    //returnString	=	returnString.Replace(",","");
                    returnString	=	returnString.Replace("\r\n","");
                    //**Start 
                    //*****Loading Documnet and Setting the VIOLATION POINTS at VUHICLE LEVEL : 21 April 2006
                    //**Loading the Appended XML form Procedures and Appending and Setting the Violation NODES
                    string strOutXml = "";
                    XmlDocument returnXMLDoc = new XmlDocument();
                    returnXMLDoc.LoadXml(returnString.ToString());
			
                    int mvrPoints=0, sumOFviolations=0, sumOfAccidents=0;
                    foreach(XmlNode VehicleVioNode in returnXMLDoc.SelectNodes("//VEHICLES/*"))
                    {
                        int VehID = int.Parse(VehicleVioNode.Attributes["ID"].Value.ToString().Trim());
                        mvrPoints=0; sumOFviolations=0; sumOfAccidents=0;
                        foreach(XmlNode DriverVioNode in returnXMLDoc.SelectNodes("//DRIVERS/*"))
                        {
                            if(DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR")!=null)
                            {
                                if(Convert.ToInt32(DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString()) == VehID)
                                {
                                    if(DriverVioNode.SelectSingleNode("MVR")!=null)
                                    {
                                        mvrPoints += int.Parse(DriverVioNode.SelectSingleNode("MVR").InnerText.ToString()==""?"0":DriverVioNode.SelectSingleNode("MVR").InnerText.ToString());
                                        sumOFviolations += int.Parse (DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString()==""?"0":DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                                        sumOfAccidents += int.Parse (DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString()==""?"0":DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                    }
                                }
                            }
                        }
					
                        int mvrPoints=0, sumOFviolations=0, sumOfAccidents=0;
                        foreach(XmlNode VehicleVioNode in returnXMLDoc.SelectNodes("//VEHICLES/*"))
                        {
                            int VehID = int.Parse(VehicleVioNode.Attributes["ID"].Value.ToString().Trim());
                            mvrPoints=0; sumOFviolations=0; sumOfAccidents=0;
                            foreach(XmlNode DriverVioNode in returnXMLDoc.SelectNodes("//DRIVERS/*"))
                            {
                                if(DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString() != "" && DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR")!=null && Convert.ToInt32(DriverVioNode.SelectSingleNode("VEHICLEASSIGNEDASOPERATOR").InnerText.ToString()) == VehID)
                                {
                                    mvrPoints += int.Parse(DriverVioNode.SelectSingleNode("MVR").InnerText.ToString()==""?"0":DriverVioNode.SelectSingleNode("MVR").InnerText.ToString());
                                    sumOFviolations += int.Parse (DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString()==""?"0":DriverVioNode.SelectSingleNode("SUMOFVIOLATIONPOINTS").InnerText.ToString());
                                    sumOfAccidents += int.Parse (DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString()==""?"0":DriverVioNode.SelectSingleNode("SUMOFACCIDENTPOINTS").InnerText.ToString());
                                }
                            }
                            //**Creating New Nodes for VIOLATIONS (CYCL)*
                            XmlElement mvrNode= returnXMLDoc.CreateElement("MVR");
                            XmlElement vioNode= returnXMLDoc.CreateElement("SUMOFVIOLATIONPOINTS");
                            XmlElement accidentNode= returnXMLDoc.CreateElement("SUMOFACCIDENTPOINTS");
                            //Set the text to node
                            mvrNode.InnerText=mvrPoints.ToString();
                            vioNode.InnerText=sumOFviolations.ToString();
                            accidentNode.InnerText=sumOfAccidents.ToString();
                            //Set the text to node
                            /*XmlText mvrtext = returnXMLDoc.CreateTextNode(mvrPoints.ToString());
                            XmlText viotext = returnXMLDoc.CreateTextNode(sumOFviolations.ToString());
                            XmlText accidenttext = returnXMLDoc.CreateTextNode(sumOfAccidents.ToString());
                            //Append the nodes 
                            VehicleVioNode.AppendChild(mvrNode);
                            VehicleVioNode.AppendChild(vioNode);
                            VehicleVioNode.AppendChild(accidentNode);
                            //					//Save the value of the fields into the nodes
                            //					mvrNode.AppendChild(mvrtext);
                            //					vioNode.AppendChild(viotext);
                            //					accidentNode.AppendChild(accidenttext);*/
                    #endregion

                }
                strOutXml = returnXMLDoc.OuterXml;

                //**END
                return strOutXml.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }


        }

        public string FetchPolicyRentalDwellingInputXML(int customerID, int POLICYID, int POLICYVersionID)
        {
            return FetchPolicyRentalDwellingInputXML(customerID, POLICYID, POLICYVersionID, null);
        }

        public string FetchPolicyRentalDwellingInputXML(int customerID, int POLICYID, int POLICYVersionID, DataWrapper objDataWrapper)
        {
            string strStoredProc_For_Rental = "Proc_GetPolicyRatingInformationForRentalDwelling";
            string strStoredProc_For_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo_Pol";
            try
            {
                // Get the Dwelling ids against the customerID, appID, appVersionID
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                string dwellingIDs = ClsDwellingDetails.GetPolicyDwellingID(customerID, POLICYID, POLICYVersionID);
                StringBuilder returnString = new StringBuilder();
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE>");
                //If no dewlling found
                if (dwellingIDs != "-1")
                {
                    string[] dwellingIDAndAddress = new string[0];
                    dwellingIDAndAddress = dwellingIDs.Split('~');

                    // Run a loop to get the inputXML for all the dwellings
                    for (int iCounter = 0; iCounter < dwellingIDAndAddress.Length; iCounter++)
                    {
                        string[] dwellingDetail = dwellingIDAndAddress[iCounter].Split('^');
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@POLICYID", POLICYID);
                        objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingDetail[0]);
                        DataSet dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_For_Rental);
                        objDataWrapper.ClearParameteres();


                        //string strCoverages		=	dsTempXML.Tables[1].Rows[0][0].ToString();;
                        string strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                        string strReturnXML = strOtherColumns;
                        string strAddress = dwellingDetail[1];
                        strAddress = strAddress.Replace("'", "H673GSUYD7G3J73UDH");
                        strAddress = strAddress.Replace("<", "&amp;LT;");
                        strAddress = strAddress.Replace(">", "&amp;GT;");
                        strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_VALUE>", "");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_VALUE>", "");

                        //strReturnXML= strReturnXML.Replace("<Table>","<DWELLINGDETAILS ID= '"+ dwellingDetail[0]+"' ADDRESS='"+EncodeXMLCharacters(dwellingDetail[1])+"'>");
                        strReturnXML = strReturnXML.Replace("<Table>", "<DWELLINGDETAILS ID= '" + dwellingDetail[0] + "' ADDRESS='" + strAddress + "'>");
                        strReturnXML = strReturnXML.Replace("</Table>", "</DWELLINGDETAILS>");
                        returnString.Append(strReturnXML);
                    }
                }
                /*Get Primary Applicant Info*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", POLICYID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", POLICYVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                DataSet dsAppTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_For_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                string strAppReturnXML = dsAppTempXML.GetXml();
                strAppReturnXML = strAppReturnXML.Replace("<NewDataSet>", "");
                strAppReturnXML = strAppReturnXML.Replace("</NewDataSet>", "");
                strAppReturnXML = strAppReturnXML.Replace("<Table>", "");
                strAppReturnXML = strAppReturnXML.Replace("</Table>", "");
                strAppReturnXML = strAppReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strAppReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*Get Primary Applicant Info*/
                returnString.Append("</QUICKQUOTE>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
            return "";
        }
        public static DataSet FtechPolicyUmbrellaSelectedPolicies(int customerID, int polID, int polversionID)
        {
            string strStoredProc_For_Umb_policy = "Proc_GetSelectedPolicyInformationForUMB_POL";
            DataSet dsCount = null;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", customerID, SqlDbType.Int);
            objDataWrapper.AddParameter("@ID", polID, SqlDbType.Int);
            objDataWrapper.AddParameter("@VERSION_ID", polversionID, SqlDbType.Int);
            dsCount = objDataWrapper.ExecuteDataSet(strStoredProc_For_Umb_policy);

            return dsCount;
        }
        public static string FetchUmbrellaWatercraft_pol(int customerID, int polId, int polVersionId, int dataaccessvalue, string policyCompany)
        {
            DataWrapper objDataWrapper;
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            string strWatercraftID = "", strReturnXML = "", ReturnXML = "";
            string strStoredProcedureFor_Umb_Watercraft_details = "Proc_GetRatingInformationForUMBWaterCraft_POL";
            StringBuilder returnString = new StringBuilder();

            strWatercraftID = clsWatercraftInformation.GetUmbrellaWatercraftIDPolicy(customerID, polId, polVersionId, dataaccessvalue);
            DataSet dsTempXML;
            // fetch policy details for selected policy


            if (strWatercraftID != "-1")
            {
                string[] arrstrWatercraftID = new string[0];
                arrstrWatercraftID = strWatercraftID.Split('~');
                // Run a loop to get the inputXML for each equipment ID
                for (int iCounterForWater = 0; iCounterForWater < arrstrWatercraftID.Length; iCounterForWater++)
                {
                    returnString.Append("<WATERCRAFT ID='" + arrstrWatercraftID[iCounterForWater] + "'>");
                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                    objDataWrapper.AddParameter("@ID", polId);
                    objDataWrapper.AddParameter("@VERSION_ID", polVersionId);
                    objDataWrapper.AddParameter("@WATERCRAFT_ID", arrstrWatercraftID[iCounterForWater]);
                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                    objDataWrapper.AddParameter("@POLICY_COMPANY", policyCompany);
                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_Watercraft_details);
                    objDataWrapper.ClearParameteres();

                    strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);
                    //strReturnXML		=	strOtherColumns;						
                    strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                    strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                    strReturnXML = strReturnXML.Replace("<Table>", "");
                    strReturnXML = strReturnXML.Replace("</Table>", "");
                    strReturnXML = strReturnXML.Replace("/r/n", "");
                    returnString.Append(strReturnXML);
                    returnString.Append("</WATERCRAFT>");

                }
                ReturnXML = returnString.ToString();
            }
            return ReturnXML;

        }


        public string FetchPolicyUmbrellaInputXML(int customerID, int POLICYID, int POLICYVersionID)
        {
            return FetchPolicyUmbrellaInputXML(customerID, POLICYID, POLICYVersionID, null);
        }

        public static string FetchPolicyUmbrellaInputXML(int customerID, int polID, int polVersionId, DataWrapper objDataWrapper)
        {

            if (objDataWrapper == null)
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            //genralinformation procs

            string strStoredProc_For_Umb_policy_details = "Proc_GetSelectedPolicyDetailsInformationForUMB_POL";
            string strStoredProc_For_Umb_Genral_information = "Proc_GetGenralRatingInformationForUMB_POL";
            string strStoredProcFor_Umb_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo_POL";
            //coverages procs
            string strStoredProcedureFor_Umb_dwelling = "Proc_GetDwellingCoverageRatingInformationForUMB_POL";
            string strStoredProcedureFor_Umb_Auto = "Proc_GetAutoCoverageRatingInformationForUMB_POL";
            string strStoredProcedureFor_Umb_homeowners = "Proc_GetHomeOwnersCoverageRatingInformationForUMB_POL";
            string strStoredProcedureFor_Umb_Motorcycle = "Proc_GetMotorcycleCoverageRatingInformationForUMB_POL";
            string strStoredProcedureFor_Umb_Watercraft = "Proc_GetwatercraftCoverageRatingInformationForUMB_POL";
            // watercraft details proc
            //string strStoredProcedureFor_Umb_Watercraft_details = "Proc_GetRatingInformationForUMBWaterCraft_POL";

            // string strpolicy;
            //string strMVRPoints = "";
            string strOtherColumns = "";
            string strReturnXML = "";
            //fetch policy information
            int idValue, versionValue, dataaccessvalue, counthomelob = 0, countautolob = 0, countmotorlob = 0, countwatercraftlob = 0, countdwellinglob = 0,
                totalAuto = 0, totalmotorhome = 0, totalinexpauto = 0, totalinexpmotro = 0, totalautomotordriver = 0, tempautocsl = 0,
                totalmotor = 0, totalinexpmotor = 0, totalmotorcycledriver = 0, tempautobilo = 0, tempautoup = 0, tempautopd = 0;
            string lobvalue = "", maturedriverautomoto = "N", uninsuredbi = "", uninsuredcsl = "", underinsuredbi = "",
                underinsuredcsl = "", excludeuninsured = "Y", maturedrivermotorcycle = "N", tempununderinsuredmotorist = "",
                tempWatercraftDetails = "";

            // Declaration of arraylist to select maximum coverage

            ArrayList AutoSplitCoveragelo = new ArrayList();
            ArrayList AutoSplitCoverageup = new ArrayList();
            ArrayList AutoPdCoverage = new ArrayList();
            ArrayList AutoCslCoverage = new ArrayList();
            ArrayList AutoCombinedCoverage = new ArrayList();
            ArrayList MotorSplitCoveragelo = new ArrayList();
            ArrayList MotorSplitCoverageup = new ArrayList();
            ArrayList MotorSplitCoveragePd = new ArrayList();
            ArrayList MotorCombinedCoverage = new ArrayList();
            ArrayList HomeOwnersCoverage = new ArrayList();
            ArrayList DwellingCoverage = new ArrayList();
            ArrayList WatercraftCoverage = new ArrayList();
            //Xml Document Declaration to load xml fetched from dataset
            XmlDocument xmlDatassetDoc = new XmlDocument();
            XmlNode nodetemp, nodetempautoBilo, nodetempautoBiup, nodtempautopd, nodetempautocsl, nodetempautouninsuredbi,
                nodtempautouninsuredcsl, nodetempautounderinsuredbi, nodetempautounderinsuredcsl, nodetempautomobiles,
                nodetempautomotorhome, nodetempautoinexpe, nodtempmotorinexpe, nodtempautomotorhomedreiver, nodtempautomatdriver, nodetempexcludeuninsured;

            StringBuilder returnString = new StringBuilder();
            StringBuilder returnStringwatercraft = new StringBuilder();
            DataSet dsTempXML;
            try
            {   //start building the inputxml
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE>");
                returnString.Append("<UMBRELLA>");
                returnString.Append("<APPLICANT_INFIORMATION>");
                //umbrella application general information
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@ID", polID);
                objDataWrapper.AddParameter("@VERSION_ID", polVersionId);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_For_Umb_Genral_information);
                objDataWrapper.ClearParameteres();
                strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                strReturnXML = strOtherColumns;
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                returnString.Append(strReturnXML);
                returnString.Append("</APPLICANT_INFIORMATION>");

                //check policies to be considered for picking coverages
                dsTempXML = FtechPolicyUmbrellaSelectedPolicies(customerID, polID, polVersionId);
                string strPolicyNumber = "-1";
                string strlob = "-1";
                string strpolicycompany = "-1";
                string strIsPolicy = "-1";
                string[] arrstrWatercraftPolicies = new string[0];
                string strwaterpolicies = "";
                if (dsTempXML.Tables[0] != null && dsTempXML.Tables[0].Rows != null)
                {
                    int intCount = dsTempXML.Tables[0].Rows.Count;
                    for (int i = 0; i < intCount; i++)
                    {
                        if (i == 0)
                        {
                            strPolicyNumber = dsTempXML.Tables[0].Rows[i][0].ToString();
                            strlob = dsTempXML.Tables[0].Rows[i][1].ToString();
                            strpolicycompany = dsTempXML.Tables[0].Rows[i][2].ToString();
                            strIsPolicy = dsTempXML.Tables[0].Rows[i][3].ToString();
                        }
                        else
                        {
                            strPolicyNumber = strPolicyNumber + '~' + dsTempXML.Tables[0].Rows[i][0].ToString();
                            strlob = strlob + '~' + dsTempXML.Tables[0].Rows[i][1].ToString();
                            strpolicycompany = strpolicycompany + '~' + dsTempXML.Tables[0].Rows[i][2].ToString();
                            strIsPolicy = strIsPolicy + '~' + dsTempXML.Tables[0].Rows[i][3].ToString();
                        }
                    }
                    //}

                    int flagHOME = 0, flagAUTO = 0, flagMOTORCYCLE = 0, flagWATERCRAFT = 0, flagRENTAL = 0;
                    if (strPolicyNumber != "-1")
                    {
                        string[] arrstrPolicyNumber = new string[0];
                        arrstrPolicyNumber = strPolicyNumber.Split('~');
                        string[] arrstrlob = new string[0];
                        arrstrlob = strlob.Split('~');
                        string[] arrstrcompany = new string[0];
                        arrstrcompany = strpolicycompany.Split('~');
                        string[] arrstrISPOLICY = new string[0];
                        arrstrISPOLICY = strIsPolicy.Split('~');
                        // Count the number of policy with a lob
                        for (int arraylenght = 0; arraylenght < arrstrlob.Length; arraylenght++)
                        {
                            if (LOB_HOME == arrstrlob[arraylenght])
                            {
                                counthomelob++;
                            }
                            if (LOB_PRIVATE_PASSENGER == arrstrlob[arraylenght])
                            {
                                countautolob++;
                            }
                            if (LOB_MOTORCYCLE == arrstrlob[arraylenght])
                            {
                                countmotorlob++;
                            }
                            if (LOB_WATERCRAFT == arrstrlob[arraylenght])
                            {
                                countwatercraftlob++;
                                strwaterpolicies = strwaterpolicies + '~' + arrstrPolicyNumber[arraylenght];
                                //arrstrWatercraftPolicies = arrstrPolicyNumber[arraylenght] + '~';
                            }
                            if (LOB_RENTAL_DWELLING == arrstrlob[arraylenght])
                            {
                                countdwellinglob++;
                            }

                        }


                        // Run a loop to get the coverages/information for each policy under each lob in the umb app
                        for (int iCounterForPolicy = 0; iCounterForPolicy < arrstrPolicyNumber.Length; iCounterForPolicy++)
                        {

                            //check the policy company. if it is WOLVERINE then get the PK parameter details 
                            if (arrstrcompany[iCounterForPolicy] == AGENCY_NAME) //CHANGE IT 
                            {
                                objDataWrapper.AddParameter("@POLICY_NUMBER", arrstrPolicyNumber[iCounterForPolicy]);
                                objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                objDataWrapper.AddParameter("@IS_POLICY", arrstrISPOLICY[iCounterForPolicy]);

                                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_For_Umb_policy_details);
                                objDataWrapper.ClearParameteres();
                                strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                strReturnXML = strOtherColumns;
                                XmlDocument docPolicyInput = new XmlDocument();
                                docPolicyInput.LoadXml(strReturnXML);
                                idValue = System.Convert.ToInt32(docPolicyInput.SelectSingleNode("NewDataSet/Table/IDS").InnerText);
                                versionValue = System.Convert.ToInt32(docPolicyInput.SelectSingleNode("NewDataSet/Table/VERSION_ID").InnerText);
                                lobvalue = docPolicyInput.SelectSingleNode("NewDataSet/Table/LOB").InnerText;
                                dataaccessvalue = System.Convert.ToInt32(docPolicyInput.SelectSingleNode("NewDataSet/Table/DATAACCESSPOINT").InnerText);
                            }
                            else
                            {
                                //else set teh same PK parameter as tht of the umbrella app.
                                idValue = polID;
                                versionValue = polVersionId;
                                dataaccessvalue = UMBRELLA;
                                lobvalue = arrstrlob[iCounterForPolicy];
                            }
                            // fetch data according to lob wise
                            if (lobvalue == LOB_HOME)
                            {
                                if (flagHOME <= counthomelob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_homeowners);
                                    objDataWrapper.ClearParameteres();
                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    nodetemp = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/HOMEOWNERPOLICY");
                                    HomeOwnersCoverage.Add(nodetemp.InnerText);
                                    flagHOME++;
                                    if (counthomelob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }
                                }
                                if (flagHOME == counthomelob)
                                {
                                    int tempvalue = maximumCoverage(HomeOwnersCoverage);
                                    strReturnXML = "";
                                    strReturnXML = strReturnXML.Insert(0, "<HOMEOWNERPOLICY>" + tempvalue.ToString() + "</HOMEOWNERPOLICY>");
                                    returnString.Append(strReturnXML);
                                }
                            }
                            if (lobvalue == LOB_PRIVATE_PASSENGER)
                            {
                                if (flagAUTO <= countautolob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_Auto);
                                    objDataWrapper.ClearParameteres();

                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    // Picking coverage for each policy 													
                                    nodetempautoBilo = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/PERSONALAUTOPOLICYLOWERLIMIT");
                                    if (nodetempautoBilo.InnerText != "0" && nodetempautoBilo.InnerText != "")
                                    {
                                        AutoSplitCoveragelo.Add(nodetempautoBilo.InnerText);
                                    }
                                    nodetempautoBiup = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/PERSONALAUTOPOLICYUPPERLIMIT");
                                    if (nodetempautoBiup.InnerText != "0" && nodetempautoBiup.InnerText != "")
                                    {
                                        AutoSplitCoverageup.Add(nodetempautoBiup.InnerText);
                                    }
                                    nodtempautopd = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/AUTOPD");
                                    if (nodtempautopd.InnerText != "0" && nodtempautopd.InnerText != "")
                                    {
                                        AutoPdCoverage.Add(nodtempautopd.InnerText);
                                    }
                                    nodetempautocsl = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/AUTOCSL");
                                    if (nodetempautocsl.InnerText != "0" && nodetempautocsl.InnerText != "")
                                    {
                                        AutoCslCoverage.Add(nodetempautocsl.InnerText);
                                    }
                                    nodetempautouninsuredbi = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNINSINSMOTORISTLIMITBISPLIT");
                                    // Checking uninsured motorist coverage if found any of the policy then set value 
                                    if ((nodetempautouninsuredbi.InnerText) != "" && (nodetempautouninsuredbi.InnerText) != "0")
                                    {
                                        uninsuredbi = nodetempautouninsuredbi.InnerText;
                                    }

                                    nodtempautouninsuredcsl = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNINSUNDERINSMOTORISTLIMITCSL");
                                    if ((nodtempautouninsuredcsl.InnerText) != "" && (nodtempautouninsuredcsl.InnerText) != "0")
                                    {
                                        uninsuredcsl = nodtempautouninsuredcsl.InnerText;
                                    }
                                    nodetempautounderinsuredcsl = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNDERINSMOTORISTLIMITCSL");
                                    if ((nodetempautounderinsuredcsl.InnerText) != "" && (nodetempautounderinsuredcsl.InnerText) != "0")
                                    {
                                        underinsuredcsl = nodetempautounderinsuredcsl.InnerText;
                                    }
                                    nodetempautounderinsuredbi = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNDERINSMOTORISTLIMITBISPLIT");
                                    if ((nodetempautounderinsuredbi.InnerText) != "" && (nodetempautounderinsuredbi.InnerText) != "0")
                                    {
                                        underinsuredbi = nodetempautounderinsuredbi.InnerText;
                                    }
                                    // total number of auto , motorhome , drivers of all selected policy
                                    nodetempautomobiles = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/AUTOMOBILES");
                                    totalAuto += System.Convert.ToInt32(nodetempautomobiles.InnerText);
                                    nodetempautoinexpe = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/INEXPDRIVERSAUTO");
                                    totalinexpauto += System.Convert.ToInt32(nodetempautoinexpe.InnerText);
                                    nodetempautomotorhome = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTOTHOMES");
                                    totalmotorhome += System.Convert.ToInt32(nodetempautomotorhome.InnerText);
                                    nodtempmotorinexpe = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/INEXPDRIVERSMOTORHOME");
                                    totalinexpmotro += System.Convert.ToInt32(nodtempmotorinexpe.InnerText);
                                    nodtempautomotorhomedreiver = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS");
                                    totalautomotordriver += System.Convert.ToInt32(nodtempautomotorhomedreiver.InnerText);
                                    nodtempautomatdriver = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MATUREAGEDISCOUNTAUTOMOTORHOM");
                                    // if mature driver is found in any policy then send  y in mature driver node 
                                    if ((nodtempautomatdriver.InnerText) == "Y")
                                    {
                                        maturedriverautomoto = "Y";
                                    }
                                    nodetempexcludeuninsured = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/EXCLUDEUNINSMOTORIST");
                                    // if any policy contains uninsured covarege then set exclude uninsured motorist to N
                                    if ((nodetempautouninsuredbi.InnerText) != "" || (nodtempautouninsuredcsl.InnerText) != "")
                                    {
                                        nodetempexcludeuninsured.InnerText = "N";
                                    }
                                    excludeuninsured = nodetempexcludeuninsured.InnerText;
                                    flagAUTO++;
                                    if (countautolob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }
                                }
                                // when all selected policy data is fetched  
                                if (flagAUTO == countautolob)
                                {
                                    // if split coverage found in any policy then send below written xml
                                    if (AutoSplitCoveragelo.Count != 0)
                                    {
                                        tempautobilo = maximumCoverage(AutoSplitCoveragelo);
                                        tempautoup = maximumCoverage(AutoSplitCoverageup);
                                        tempautopd = maximumCoverage(AutoPdCoverage);
                                        strReturnXML = "";
                                        strReturnXML = strReturnXML.Insert(0, "<PERSONALAUTOPOLICYLOWERLIMIT>" + tempautobilo.ToString() + "</PERSONALAUTOPOLICYLOWERLIMIT>" +
                                            "<PERSONALAUTOPOLICYUPPERLIMIT>" + tempautoup.ToString() + "</PERSONALAUTOPOLICYUPPERLIMIT>" +
                                            "<AUTOPD>" + tempautopd.ToString() + "</AUTOPD>" + "<AUTOCSL>" + "</AUTOCSL>" + "<UNINSUNDERINSMOTORISTLIMITCSL>" + uninsuredcsl + "</UNINSUNDERINSMOTORISTLIMITCSL>" +
                                            "<UNINSINSMOTORISTLIMITBISPLIT>" + uninsuredbi + "</UNINSINSMOTORISTLIMITBISPLIT>" + "<UNDERINSMOTORISTLIMITCSL>" + underinsuredcsl + "</UNDERINSMOTORISTLIMITCSL>" +
                                            "<UNDERINSMOTORISTLIMITBISPLIT>" + underinsuredbi + "</UNDERINSMOTORISTLIMITBISPLIT>" + "<AUTOMOBILES>" + totalAuto + "</AUTOMOBILES>" +
                                            "<INEXPDRIVERSAUTO>" + totalinexpauto + "</INEXPDRIVERSAUTO>" + "<MOTOTHOMES>" + totalmotorhome + "</MOTOTHOMES>" +
                                            "<INEXPDRIVERSMOTORHOME>" + totalinexpmotro + "</INEXPDRIVERSMOTORHOME>" + "<TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS>" + totalautomotordriver + "</TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS>" +
                                            "<MATUREAGEDISCOUNTAUTOMOTORHOM>" + maturedriverautomoto + "</MATUREAGEDISCOUNTAUTOMOTORHOM>" + "<EXCLUDEUNINSMOTORIST>" + excludeuninsured + "</EXCLUDEUNINSMOTORIST>");
                                        returnString.Append(strReturnXML);
                                    }
                                    // if any policy contains csl with 300000 value then set csl value to that
                                    if (AutoCslCoverage.Count != 0)
                                    {
                                        tempautocsl = maximumCoverage(AutoCslCoverage);
                                        for (int i = 0; i < AutoCslCoverage.Count; i++)
                                        {
                                            if (System.Convert.ToInt32(AutoCslCoverage[i].ToString()) <= 300000)
                                            {
                                                tempautocsl = System.Convert.ToInt32(AutoCslCoverage[i].ToString());
                                            }
                                        }

                                    }
                                    // if split limit not found then send below mentioned xml
                                    if (AutoSplitCoveragelo.Count == 0 && AutoCslCoverage.Count != 0)
                                    {
                                        strReturnXML = "";
                                        strReturnXML = strReturnXML.Insert(0, "<PERSONALAUTOPOLICYLOWERLIMIT>" + "</PERSONALAUTOPOLICYLOWERLIMIT>" +
                                            "<PERSONALAUTOPOLICYUPPERLIMIT>" + "</PERSONALAUTOPOLICYUPPERLIMIT>" +
                                            "<AUTOPD>" + "</AUTOPD>" + "<AUTOCSL>" + tempautocsl + "</AUTOCSL>" + "<UNINSUNDERINSMOTORISTLIMITCSL>" + uninsuredcsl + "</UNINSUNDERINSMOTORISTLIMITCSL>" +
                                            "<UNINSINSMOTORISTLIMITBISPLIT>" + uninsuredbi + "</UNINSINSMOTORISTLIMITBISPLIT>" + "<UNDERINSMOTORISTLIMITCSL>" + underinsuredcsl + "</UNDERINSMOTORISTLIMITCSL>" +
                                            "<UNDERINSMOTORISTLIMITBISPLIT>" + underinsuredbi + "</UNDERINSMOTORISTLIMITBISPLIT>" + "<AUTOMOBILES>" + totalAuto + "</AUTOMOBILES>" +
                                            "<INEXPDRIVERSAUTO>" + totalinexpauto + "</INEXPDRIVERSAUTO>" + "<MOTOTHOMES>" + totalmotorhome + "</MOTOTHOMES>" +
                                            "<INEXPDRIVERSMOTORHOME>" + totalinexpmotro + "</INEXPDRIVERSMOTORHOME>" + "<TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS>" + totalautomotordriver + "</TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS>" +
                                            "<MATUREAGEDISCOUNTAUTOMOTORHOM>" + maturedriverautomoto + "</MATUREAGEDISCOUNTAUTOMOTORHOM>" + "<EXCLUDEUNINSMOTORIST>" + excludeuninsured + "</EXCLUDEUNINSMOTORIST>");
                                        returnString.Append(strReturnXML);
                                    }

                                }

                            }

                            if (lobvalue == LOB_MOTORCYCLE)
                            {
                                if (flagMOTORCYCLE <= countmotorlob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_Motorcycle);
                                    objDataWrapper.ClearParameteres();

                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    // fetch coverage from selected policy
                                    nodetempautoBilo = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORCYCLEPOLICYLOWERLIMIT");
                                    if (nodetempautoBilo.InnerText != "0" && nodetempautoBilo.InnerText != "")
                                    {
                                        MotorSplitCoveragelo.Add(nodetempautoBilo.InnerText);
                                    }
                                    nodetempautoBiup = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORCYCLEPOLICYUPPERLIMIT");
                                    if (nodetempautoBiup.InnerText != "0" && nodetempautoBiup.InnerText != "")
                                    {
                                        MotorSplitCoverageup.Add(nodetempautoBiup.InnerText);
                                    }
                                    nodtempautopd = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORPD");
                                    if (nodtempautopd.InnerText != "0" && nodtempautopd.InnerText != "")
                                    {
                                        MotorSplitCoveragePd.Add(nodtempautopd.InnerText);
                                    }
                                    nodetempautocsl = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORCSL");
                                    if (nodetempautocsl.InnerText != "0" && nodetempautocsl.InnerText != "")
                                    {
                                        MotorCombinedCoverage.Add(nodetempautocsl.InnerText);
                                    }
                                    nodetempautouninsuredbi = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/UNINSUNDERINSMOTORISTLIMIT");
                                    tempununderinsuredmotorist = nodetempautouninsuredbi.InnerText;
                                    // fetch total motorcycle ,driver, inexperienced driver from all selected policy
                                    nodetempautomobiles = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MOTORCYCLES");
                                    totalmotor += System.Convert.ToInt32(nodetempautomobiles.InnerText);
                                    nodetempautoinexpe = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/INEXPDRIVERSMOTORCYCL");
                                    totalinexpmotor += System.Convert.ToInt32(nodetempautoinexpe.InnerText);
                                    nodtempautomotorhomedreiver = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/TOTALNUMEROFMOTORCYCLEDRIVERS");
                                    totalmotorcycledriver += System.Convert.ToInt32(nodtempautomotorhomedreiver.InnerText);
                                    nodtempautomatdriver = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/MATUREAGEDISCOUNTMOTORCYCLE");

                                    if ((nodtempautomatdriver.InnerText) == "Y")
                                    {
                                        maturedrivermotorcycle = "Y";
                                    }
                                    flagMOTORCYCLE++;
                                    if (countmotorlob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }

                                }
                                // when all seletced policy data is fetched then make xml
                                if (flagMOTORCYCLE == countmotorlob)
                                {
                                    // if split limti found in selected policies then send below written xml
                                    if (MotorSplitCoveragelo.Count != 0)
                                    {
                                        tempautobilo = maximumCoverage(MotorSplitCoveragelo);
                                        tempautoup = maximumCoverage(MotorSplitCoverageup);
                                        tempautopd = maximumCoverage(MotorSplitCoveragePd);
                                        strReturnXML = "";
                                        strReturnXML = strReturnXML.Insert(0, "<MOTORCYCLEPOLICYLOWERLIMIT>" + tempautobilo.ToString() + "</MOTORCYCLEPOLICYLOWERLIMIT>" +
                                            "<MOTORCYCLEPOLICYUPPERLIMIT>" + tempautoup.ToString() + "</MOTORCYCLEPOLICYUPPERLIMIT>" + "<MOTORPD>" + tempautopd.ToString() + "</MOTORPD>" +
                                            "<MOTORCSL>" + "</MOTORCSL>" + "<UNINSUNDERINSMOTORISTLIMIT>" + tempununderinsuredmotorist + "</UNINSUNDERINSMOTORISTLIMIT>" +
                                            "<MOTORCYCLES>" + totalmotor.ToString() + "</MOTORCYCLES>" + "<INEXPDRIVERSMOTORCYCL>" + totalinexpmotor.ToString() + "</INEXPDRIVERSMOTORCYCL>" +
                                            "<TOTALNUMEROFMOTORCYCLEDRIVERS>" + totalmotorcycledriver.ToString() + "</TOTALNUMEROFMOTORCYCLEDRIVERS>" + "<MATUREAGEDISCOUNTMOTORCYCLE>" + maturedrivermotorcycle + "</MATUREAGEDISCOUNTMOTORCYCLE>");
                                        returnString.Append(strReturnXML);
                                    }

                                    // if any selected policy contains csl limit with 300000 value then set csl limit to that 
                                    if (MotorCombinedCoverage.Count != 0)
                                    {
                                        tempautocsl = maximumCoverage(MotorCombinedCoverage);
                                        for (int i = 0; i < MotorCombinedCoverage.Count; i++)
                                        {
                                            if (System.Convert.ToInt32(MotorCombinedCoverage[i].ToString()) <= 300000)
                                            {
                                                tempautocsl = System.Convert.ToInt32(MotorCombinedCoverage[i].ToString());
                                            }
                                        }

                                    }
                                    // if split limit not found then send below written xml
                                    if (MotorSplitCoveragelo.Count == 0 && MotorCombinedCoverage.Count != 0)
                                    {
                                        strReturnXML = "";
                                        strReturnXML = strReturnXML.Insert(0, "<MOTORCYCLEPOLICYLOWERLIMIT>" + "</MOTORCYCLEPOLICYLOWERLIMIT>" +
                                            "<MOTORCYCLEPOLICYUPPERLIMIT>" + "</MOTORCYCLEPOLICYUPPERLIMIT>" + "<MOTORPD>" + "</MOTORPD>" +
                                            "<MOTORCSL>" + tempautocsl.ToString() + "</MOTORCSL>" + "<UNINSUNDERINSMOTORISTLIMIT>" + tempununderinsuredmotorist + "</UNINSUNDERINSMOTORISTLIMIT>" +
                                            "<MOTORCYCLES>" + totalmotor.ToString() + "</MOTORCYCLES>" + "<INEXPDRIVERSMOTORCYCL>" + totalinexpmotor.ToString() + "</INEXPDRIVERSMOTORCYCL>" +
                                            "<TOTALNUMEROFMOTORCYCLEDRIVERS>" + totalmotorcycledriver.ToString() + "</TOTALNUMEROFMOTORCYCLEDRIVERS>" + "<MATUREAGEDISCOUNTMOTORCYCLE>" + maturedrivermotorcycle + "</MATUREAGEDISCOUNTMOTORCYCLE>");
                                        returnString.Append(strReturnXML);
                                    }

                                }
                            }
                            if (lobvalue == LOB_WATERCRAFT)
                            {
                                if (flagWATERCRAFT <= countwatercraftlob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_Watercraft);
                                    objDataWrapper.ClearParameteres();
                                    // fetch watercraft details of this policy
                                    tempWatercraftDetails = FetchUmbrellaWatercraft_pol(customerID, idValue, versionValue, dataaccessvalue, arrstrcompany[iCounterForPolicy]);
                                    returnStringwatercraft.Append(tempWatercraftDetails);
                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    nodetemp = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/WATERCRAFTPOLICY");
                                    WatercraftCoverage.Add(nodetemp.InnerText);
                                    flagWATERCRAFT++;
                                    if (countwatercraftlob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }
                                }
                                if (flagWATERCRAFT == countwatercraftlob)
                                {
                                    int tempvalue = maximumCoverage(WatercraftCoverage);
                                    strReturnXML = "";
                                    strReturnXML = strReturnXML.Insert(0, "<WATERCRAFTPOLICY>" + tempvalue.ToString() + "</WATERCRAFTPOLICY>");
                                    returnString.Append(strReturnXML);
                                }

                            }



                            if (lobvalue == LOB_RENTAL_DWELLING)
                            {
                                if (flagRENTAL <= countdwellinglob)
                                {
                                    objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                                    objDataWrapper.AddParameter("@ID", idValue);
                                    objDataWrapper.AddParameter("@VERSION_ID", versionValue);
                                    objDataWrapper.AddParameter("@UMBRELLA_POLICY_ID", arrstrPolicyNumber[iCounterForPolicy]);
                                    objDataWrapper.AddParameter("@DATA_ACCESS_POINT", dataaccessvalue);
                                    objDataWrapper.AddParameter("@POLICY_COMPANY", arrstrcompany[iCounterForPolicy]);
                                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcedureFor_Umb_dwelling);
                                    objDataWrapper.ClearParameteres();

                                    strOtherColumns = ClsCommon.GetXML(dsTempXML.Tables[0]);
                                    strReturnXML = strOtherColumns;
                                    strReturnXML = strReturnXML.Replace("\r\n", "");
                                    strReturnXML = strReturnXML.Replace(",", "");
                                    xmlDatassetDoc.LoadXml(strReturnXML);
                                    nodetemp = xmlDatassetDoc.SelectSingleNode("NewDataSet/Table/DWELLINGFIREPOLICY");
                                    DwellingCoverage.Add(nodetemp.InnerText);
                                    flagRENTAL++;
                                    if (countdwellinglob > 1)
                                    {
                                        strReturnXML = "";
                                        returnString.Append(strReturnXML);
                                    }
                                }
                                if (flagRENTAL == countdwellinglob)
                                {
                                    int tempvalue = maximumCoverage(DwellingCoverage);
                                    strReturnXML = "";
                                    strReturnXML = strReturnXML.Insert(0, "<DWELLINGFIREPOLICY>" + tempvalue.ToString() + "</DWELLINGFIREPOLICY>");
                                    returnString.Append(strReturnXML);
                                }


                            }
                        }

                    }
                }
                /*Get Watercraft Info(start)*/
                returnString.Append("<WATERCRAFT_EXPOSURES>");
                returnString.Append(returnStringwatercraft.ToString());
                returnString.Append("</WATERCRAFT_EXPOSURES>");
                /*Get Watercraft Info(End)*/
                /*Get Primary Applicant Info(Start)*/
                strReturnXML = "";
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", polID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", polVersionId);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                objDataWrapper.AddParameter("@CALLEDFROM", "APP");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcFor_Umb_PrimaryApplicants);
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                int indexFirstname;
                indexFirstname = strReturnXML.LastIndexOf("<FIRST_NAME>");
                strReturnXML = strReturnXML.Insert(indexFirstname, "<PRIMARYAPPLICANT>");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                strReturnXML = returnString.ToString();

                /*Get Primary Applicant Info(End)*/


                returnString.Append("</UMBRELLA>");
                returnString = returnString.Replace(",", "");
                returnString.Append("</QUICKQUOTE>");
                returnString = returnString.Replace("\r\n\r\n", "");
                returnString = returnString.Replace("\r\n", "");

                string inputxml = returnString.ToString();
                XmlDocument docinputXML = new XmlDocument();
                docinputXML.LoadXml(inputxml);

                //creating node for insertion in inputxml

                XmlNode nodeautomobilepersonalvehicle, nodeumbrella, nodeapplicantinformation;
                XmlElement nodpersonalautoexposer, nodTemppersonalexposer, nodTempofficepremises, nodtemptentaldwellingunit, nodunderlyingpolicies,
                    nodautobilowerlimit, nodautobiupperlimit, nodautocsllimit, nodautopdlimit, nodhomeownerpolicylimit,
                    nodmotorcyclepolicybilowerlimit, nodmotorcyclepolicybiupperlimit, nodmotorcyclecsllimit, nodmotorcyclepdlimit, nodmotorcycleuninsuredbilimit,
                    nodmotorcycleuninsuredcsllimit, nodunderinsuerdbisplit, nodunderinsuredcsllimt,
                    nodmotorcycle, nodmotorhomes, nodautomobile, nodinexperincedauto, nodinexperincemotorcycle, nodinexperincemotorhomes,
                    nodmaturage, noddrivers, nodexecludeuninsuredmotorist, nodWatercraftpolicy, noddwellingfirepolicy;

                nodeapplicantinformation = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION");
                nodeumbrella = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA");

                // inserting parrent node
                XmlNode nodTemp = nodeumbrella.LastChild;
                nodpersonalautoexposer = docinputXML.CreateElement("PERSONAL_AUTO_EXPOSURES");
                nodeumbrella.InsertAfter(nodpersonalautoexposer, nodTemp);

                //inserting personal exposer node
                nodTemppersonalexposer = docinputXML.CreateElement("PERSONAL_EXPOSURE");
                nodpersonalautoexposer.AppendChild(nodTemppersonalexposer);
                //inserting underlying policy node
                nodunderlyingpolicies = docinputXML.CreateElement("UNDERLYINGPOLICY");
                nodpersonalautoexposer.AppendChild(nodunderlyingpolicies);


                //inserting automobilemotorvehicle exposer node
                nodeautomobilepersonalvehicle = docinputXML.CreateElement("AUTOMOBILE_MOTORVEHICLE_EXPOSURES");
                nodpersonalautoexposer.AppendChild(nodeautomobilepersonalvehicle);

                //inserting child node on personal exposer

                //office promises
                nodTempofficepremises = docinputXML.CreateElement("OFFICEPREMISES");
                XmlNode nodetempoffice = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/OFFICE_PROMISES");
                if (nodetempoffice != null)
                {
                    if (nodetempoffice.InnerText != null)
                    {
                        nodTempofficepremises.InnerText = nodetempoffice.InnerText;
                        nodeapplicantinformation.RemoveChild(nodetempoffice);
                    }
                }
                nodTemppersonalexposer.AppendChild(nodTempofficepremises);


                // rental dwelling unit
                nodtemptentaldwellingunit = docinputXML.CreateElement("RENTALDWELLINGUNIT");
                XmlNode nodtemprentallunit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/RENTAL_DWELLING_UNITS");
                if (nodtemprentallunit != null)
                {
                    nodtemptentaldwellingunit.InnerText = nodtemprentallunit.InnerText;
                    nodeapplicantinformation.RemoveChild(nodtemprentallunit);
                }
                nodTemppersonalexposer.AppendChild(nodtemptentaldwellingunit);



                // inserting node on underlying policies

                // inserting bi lower limit
                nodautobilowerlimit = docinputXML.CreateElement("PERSONALAUTOPOLICYLOWERLIMIT");
                XmlNode nodetempautobilowerlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/PERSONALAUTOPOLICYLOWERLIMIT");
                if (nodetempautobilowerlimit != null)
                {
                    nodautobilowerlimit.InnerText = nodetempautobilowerlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempautobilowerlimit);
                }

                nodunderlyingpolicies.AppendChild(nodautobilowerlimit);


                // inserting bi upper limit
                nodautobiupperlimit = docinputXML.CreateElement("PERSONALAUTOPOLICYUPPERLIMIT");
                XmlNode nodetempautobiupperlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/PERSONALAUTOPOLICYUPPERLIMIT");
                if (nodetempautobiupperlimit != null)
                {
                    nodautobiupperlimit.InnerText = nodetempautobiupperlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempautobiupperlimit);
                }

                nodunderlyingpolicies.AppendChild(nodautobiupperlimit);


                // inserting auto csl limit 
                nodautocsllimit = docinputXML.CreateElement("AUTOCSL");
                XmlNode nodetempautocsllimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/AUTOCSL");
                if (nodetempautocsllimit != null)
                {
                    nodautocsllimit.InnerText = nodetempautocsllimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempautocsllimit);
                }

                nodunderlyingpolicies.AppendChild(nodautocsllimit);


                // inserting auto pd limit
                nodautopdlimit = docinputXML.CreateElement("AUTOPD");
                XmlNode nodetempautopdlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/AUTOPD");
                if (nodetempautopdlimit != null)
                {
                    nodautopdlimit.InnerText = nodetempautopdlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempautopdlimit);
                }

                nodunderlyingpolicies.AppendChild(nodautopdlimit);


                // motorcycle bilimit
                nodmotorcyclepolicybilowerlimit = docinputXML.CreateElement("MOTORCYCLEPOLICYLOWERLIMIT");
                XmlNode nodetempmotorcyclepolicybilowerlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORCYCLEPOLICYLOWERLIMIT");
                if (nodetempmotorcyclepolicybilowerlimit != null)
                {
                    nodmotorcyclepolicybilowerlimit.InnerText = nodetempmotorcyclepolicybilowerlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcyclepolicybilowerlimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcyclepolicybilowerlimit);

                // motorcycle bi upper limit

                nodmotorcyclepolicybiupperlimit = docinputXML.CreateElement("MOTORCYCLEPOLICYUPPERLIMIT");
                XmlNode nodetempmotorcyclepolicybiupperlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORCYCLEPOLICYUPPERLIMIT");
                if (nodetempmotorcyclepolicybiupperlimit != null)
                {
                    nodmotorcyclepolicybiupperlimit.InnerText = nodetempmotorcyclepolicybiupperlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcyclepolicybiupperlimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcyclepolicybiupperlimit);


                // motor cycle pd limit
                nodmotorcyclepdlimit = docinputXML.CreateElement("MOTORPD");
                XmlNode nodetempmotorcyclepdlimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORPD");
                if (nodetempmotorcyclepdlimit != null)
                {
                    nodmotorcyclepdlimit.InnerText = nodetempmotorcyclepdlimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcyclepdlimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcyclepdlimit);


                // motor cycle csl limit
                nodmotorcyclecsllimit = docinputXML.CreateElement("MOTORCSL");
                XmlNode nodetempmotorcyclecsllimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORCSL");
                if (nodetempmotorcyclecsllimit != null)
                {
                    nodmotorcyclecsllimit.InnerText = nodetempmotorcyclecsllimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcyclecsllimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcyclecsllimit);


                // uninsured bilimit
                nodmotorcycleuninsuredbilimit = docinputXML.CreateElement("UNINSINSMOTORISTLIMITBISPLIT");
                XmlNode nodetempmotorcycleuninsuredbilimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/UNINSINSMOTORISTLIMITBISPLIT");
                if (nodetempmotorcycleuninsuredbilimit != null)
                {
                    nodmotorcycleuninsuredbilimit.InnerText = nodetempmotorcycleuninsuredbilimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcycleuninsuredbilimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcycleuninsuredbilimit);


                // underinsured csl limit
                nodunderinsuredcsllimt = docinputXML.CreateElement("UNDERINSMOTORISTLIMITCSL");
                XmlNode nodetempunderinsuredcsllimt = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/UNDERINSMOTORISTLIMITCSL");
                if (nodetempunderinsuredcsllimt != null)
                {
                    nodunderinsuredcsllimt.InnerText = nodetempunderinsuredcsllimt.InnerText;
                    nodeumbrella.RemoveChild(nodetempunderinsuredcsllimt);
                }

                nodunderlyingpolicies.AppendChild(nodunderinsuredcsllimt);


                //underinsured bilimit
                nodunderinsuerdbisplit = docinputXML.CreateElement("UNDERINSMOTORISTLIMITBISPLIT");
                XmlNode nodetempunderinsuerdbisplit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/UNDERINSMOTORISTLIMITBISPLIT");
                if (nodetempunderinsuerdbisplit != null)
                {
                    nodunderinsuerdbisplit.InnerText = nodetempunderinsuerdbisplit.InnerText;
                    nodeumbrella.RemoveChild(nodetempunderinsuerdbisplit);
                }
                nodunderlyingpolicies.AppendChild(nodunderinsuerdbisplit);


                //uninsurecsllimit
                nodmotorcycleuninsuredcsllimit = docinputXML.CreateElement("UNINSUNDERINSMOTORISTLIMITCSL");
                XmlNode nodetempmotorcycleuninsuredcsllimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/UNINSUNDERINSMOTORISTLIMITCSL");
                if (nodetempmotorcycleuninsuredcsllimit != null)
                {
                    nodmotorcycleuninsuredcsllimit.InnerText = nodetempmotorcycleuninsuredcsllimit.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcycleuninsuredcsllimit);
                }
                nodunderlyingpolicies.AppendChild(nodmotorcycleuninsuredcsllimit);


                // homeowners policy limit
                nodhomeownerpolicylimit = docinputXML.CreateElement("HOMEOWNERPOLICY");
                XmlNode nodetemphomeownerpolicylimit = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/HOMEOWNERPOLICY");
                if (nodetemphomeownerpolicylimit != null)
                {
                    nodhomeownerpolicylimit.InnerText = nodetemphomeownerpolicylimit.InnerText;
                    nodeumbrella.RemoveChild(nodetemphomeownerpolicylimit);
                }

                nodunderlyingpolicies.AppendChild(nodhomeownerpolicylimit);


                // renatll dwelling policy limit
                noddwellingfirepolicy = docinputXML.CreateElement("DWELLINGFIREPOLICY");
                XmlNode nodetempdwellingfirepolicy = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/DWELLINGFIREPOLICY");
                if (nodetempdwellingfirepolicy != null)
                {
                    noddwellingfirepolicy.InnerText = nodetempdwellingfirepolicy.InnerText;
                    nodeumbrella.RemoveChild(nodetempdwellingfirepolicy);
                }
                nodunderlyingpolicies.AppendChild(noddwellingfirepolicy);


                // watercraft policy limit
                nodWatercraftpolicy = docinputXML.CreateElement("WATERCRAFTPOLICY");
                XmlNode nodtempWatercraftpolicy = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/WATERCRAFTPOLICY");
                if (nodtempWatercraftpolicy != null)
                {
                    nodWatercraftpolicy.InnerText = nodtempWatercraftpolicy.InnerText;
                    nodeumbrella.RemoveChild(nodtempWatercraftpolicy);
                }
                nodunderlyingpolicies.AppendChild(nodWatercraftpolicy);


                // inserting  child node on auto motor vehicle exposer 

                // total numbrr of drivers
                string strautodrivers = "0", strotherdrivers = "0";//strmotorcycledrivers = "0",
                noddrivers = docinputXML.CreateElement("TOTALNUMBERDRIVERS");
                XmlNode nodtempdriversothers = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/APPLICANT_INFIORMATION/OTHER_DRIVERS");
                XmlNode nodtempautodrivers = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/TOTALNUMER_OF_AUTO_MOTORHOME_DRIVERS");
                //XmlNode nodetempmotorcycledrivers = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/TOTALNUMEROFMOTORCYCLEDRIVERS");
                if (nodtempdriversothers != null)
                {
                    strotherdrivers = nodtempdriversothers.InnerText;
                    nodeapplicantinformation.RemoveChild(nodtempdriversothers);
                }
                if (nodtempautodrivers != null)
                {
                    strautodrivers = nodtempautodrivers.InnerText;
                    nodeumbrella.RemoveChild(nodtempautodrivers);
                }

                /*if (nodetempmotorcycledrivers != null)
                {
                    strmotorcycledrivers = nodetempmotorcycledrivers.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcycledrivers);
                }*/
                int totalNumber_drivers;
                totalNumber_drivers = System.Convert.ToInt32(strautodrivers) + System.Convert.ToInt32(strotherdrivers);
                noddrivers.InnerText = totalNumber_drivers.ToString();
                nodeautomobilepersonalvehicle.AppendChild(noddrivers);



                //personal automobile 
                nodautomobile = docinputXML.CreateElement("AUTOMOBILES");
                XmlNode nodetempautomobile = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/AUTOMOBILES");
                if (nodetempautomobile != null)
                {
                    nodautomobile.InnerText = nodetempautomobile.InnerText;
                    nodeumbrella.RemoveChild(nodetempautomobile);
                }
                else
                {
                    nodautomobile.InnerText = "0";
                }
                nodeautomobilepersonalvehicle.AppendChild(nodautomobile);




                // inexperince drivers auto
                nodinexperincedauto = docinputXML.CreateElement("INEXPDRIVERSAUTO");
                XmlNode nodetempinexperincedauto = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/INEXPDRIVERSAUTO");
                if (nodetempinexperincedauto != null)
                {
                    nodinexperincedauto.InnerText = nodetempinexperincedauto.InnerText;
                    nodeumbrella.RemoveChild(nodetempinexperincedauto);
                }
                nodeautomobilepersonalvehicle.AppendChild(nodinexperincedauto);
                //nodeautomobilepersonalvehicle.AppendChild(nodinexperincedauto);


                //motorcycle
                nodmotorcycle = docinputXML.CreateElement("MOTORCYCLES");
                XmlNode nodetempmotorcycle = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTORCYCLES");
                if (nodetempmotorcycle != null)
                {
                    nodmotorcycle.InnerText = nodetempmotorcycle.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorcycle);
                }
                else
                {
                    nodmotorcycle.InnerText = "0";
                }
                nodeautomobilepersonalvehicle.AppendChild(nodmotorcycle);


                // inexperince drivers motorcycle
                nodinexperincemotorcycle = docinputXML.CreateElement("INEXPDRIVERSMOTORCYCL");
                XmlNode nodetempinexperincemotorcycle = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/INEXPDRIVERSMOTORCYCL");
                if (nodetempinexperincemotorcycle != null)
                {
                    nodinexperincemotorcycle.InnerText = nodetempinexperincemotorcycle.InnerText;
                    nodeumbrella.RemoveChild(nodetempinexperincemotorcycle);
                }
                nodeautomobilepersonalvehicle.AppendChild(nodinexperincemotorcycle);


                // motor homes
                nodmotorhomes = docinputXML.CreateElement("MOTOTHOMES");
                XmlNode nodetempmotorhomes = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MOTOTHOMES");
                if (nodetempmotorhomes != null)
                {
                    nodmotorhomes.InnerText = nodetempmotorhomes.InnerText;
                    nodeumbrella.RemoveChild(nodetempmotorhomes);
                }
                else
                {
                    nodmotorhomes.InnerText = "0";
                }
                nodeautomobilepersonalvehicle.AppendChild(nodmotorhomes);


                // inexperince drivers motor hopmes
                nodinexperincemotorhomes = docinputXML.CreateElement("INEXPDRIVERSMOTORHOME");
                XmlNode nodetempinexperincemotorhome = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/INEXPDRIVERSMOTORHOME");
                if (nodetempinexperincemotorhome != null)
                {
                    nodinexperincemotorhomes.InnerText = nodetempinexperincemotorhome.InnerText;
                    nodeumbrella.RemoveChild(nodetempinexperincemotorhome);
                }

                nodeautomobilepersonalvehicle.AppendChild(nodinexperincemotorhomes);


                // exclude uninsured motorist
                nodexecludeuninsuredmotorist = docinputXML.CreateElement("EXCLUDEUNINSMOTORIST");
                XmlNode nodtempexecludeuninsuredmotorist = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/EXCLUDEUNINSMOTORIST");
                if (nodtempexecludeuninsuredmotorist != null)
                {
                    nodexecludeuninsuredmotorist.InnerText = nodtempexecludeuninsuredmotorist.InnerText;
                    nodeumbrella.RemoveChild(nodtempexecludeuninsuredmotorist);
                }
                nodeautomobilepersonalvehicle.AppendChild(nodexecludeuninsuredmotorist);


                //mature age discount
                nodmaturage = docinputXML.CreateElement("MATUREDISCOUNT");
                XmlNode nodetempmatureageautomotorhome = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MATUREAGEDISCOUNTAUTOMOTORHOM");
                XmlNode nodetempmaturagemotorcycvle = docinputXML.SelectSingleNode("QUICKQUOTE/UMBRELLA/MATUREAGEDISCOUNTMOTORCYCLE");
                string matureagediscount = "";
                string matureagedscountmotorcycle = "0", matureagediscountmotorhome = "0";
                if (nodetempmaturagemotorcycvle != null)
                {
                    matureagedscountmotorcycle = nodetempmaturagemotorcycvle.InnerText;
                    nodeumbrella.RemoveChild(nodetempmaturagemotorcycvle);

                }
                if (nodetempmatureageautomotorhome != null)
                {
                    matureagediscountmotorhome = nodetempmatureageautomotorhome.InnerText;
                    nodeumbrella.RemoveChild(nodetempmatureageautomotorhome);

                }
                if (nodetempmaturagemotorcycvle != null || nodetempmatureageautomotorhome != null)
                {
                    if (matureagedscountmotorcycle == "Y" || matureagediscountmotorhome == "Y")
                    {
                        matureagediscount = "Y";
                    }
                    else
                    {
                        matureagediscount = "N";
                    }
                }
                else
                {
                    matureagediscount = "N";
                }
                nodmaturage.InnerText = matureagediscount;
                nodeautomobilepersonalvehicle.AppendChild(nodmaturage);




                return docinputXML.InnerXml;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }


        }



        # endregion

        private void PopulateToDoListObject(Cms.Model.Diary.TodolistInfo objToDoListInfo, ClsGeneralInfo objGeneralInfo)
        {
            objToDoListInfo.STARTTIME = System.DateTime.Now;
            objToDoListInfo.ENDTIME = System.DateTime.Now;
            objToDoListInfo.RECDATE = System.DateTime.Now;
            objToDoListInfo.CREATED_DATETIME = DateTime.Now;
            objToDoListInfo.FOLLOWUPDATE = DateTime.Parse(System.DateTime.Now.AddDays(5).ToString());
            objToDoListInfo.LAST_UPDATED_DATETIME = DateTime.Now;
            objToDoListInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
            objToDoListInfo.CUSTOMER_ID = objGeneralInfo.CUSTOMER_ID;
            objToDoListInfo.SUBJECTLINE = "Application added.";
            objToDoListInfo.PRIORITY = "M";
            objToDoListInfo.TOUSERID = objGeneralInfo.UNDERWRITER;
            objToDoListInfo.FROMUSERID = objGeneralInfo.CREATED_BY;
            objToDoListInfo.LISTTYPEID = 6;
        }


        public int CheckApplicationExistence(ClsGeneralInfo objGeneralInfo, DataWrapper objDataWrapper, out int appID, out int appVersionID)
        {
            string strStoredProc = "Proc_CheckApplicationExistence";

            objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@APP_NUMBER", objGeneralInfo.APP_NUMBER);
            objDataWrapper.AddParameter("@APP_VERSION", objGeneralInfo.APP_VERSION);

            SqlParameter sAppID = (SqlParameter)objDataWrapper.AddParameter("@APP_ID", SqlDbType.Int, ParameterDirection.Output);
            SqlParameter sAppVersionID = (SqlParameter)objDataWrapper.AddParameter("@APP_VERSION_ID", SqlDbType.SmallInt, ParameterDirection.Output);

            objDataWrapper.ExecuteNonQuery(strStoredProc);

            if (Convert.ToInt32(sAppID.Value) == -1)
            {
                appID = -1;
                appVersionID = -1;
                return -1;
            }

            appID = Convert.ToInt32(sAppID.Value);
            appVersionID = Convert.ToInt32(sAppVersionID.Value);
            //int returnResult = Convert.ToInt32(retVal.Value);

            return 1;
        }


        /// <summary>
        /// Inserting Application General details
        /// </summary>
        /// <param name="objGeneralInfo">Modal Object for Application</param>
        /// <returns>int: this is the number of records added.</returns>
        public int Add(Cms.Model.Application.ClsGeneralInfo objGeneralInfo, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_InsertApplicationGenInfo_ACORD";
            DateTime RecordDate = DateTime.Now;

            //Set transaction label in the new object, if required
            if (this.boolTransactionRequired)
            {
                objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
            }

            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
            //objDataWrapper.AddParameter("@APP_ID",objGeneralInfo.APP_ID,SqlDbType.Int ,ParameterDirection.Output ,5);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@PARENT_APP_VERSION_ID", objGeneralInfo.PARENT_APP_VERSION_ID);
            objDataWrapper.AddParameter("@APP_STATUS", objGeneralInfo.APP_STATUS);
            objDataWrapper.AddParameter("@APP_NUMBER", objGeneralInfo.APP_NUMBER);
            objDataWrapper.AddParameter("@APP_VERSION", objGeneralInfo.APP_VERSION);
            objDataWrapper.AddParameter("@APP_TERMS", objGeneralInfo.APP_TERMS);
            if (objGeneralInfo.APP_INCEPTION_DATE != DateTime.MinValue)
            {
                objDataWrapper.AddParameter("@APP_INCEPTION_DATE", objGeneralInfo.APP_INCEPTION_DATE);
            }
            //Nov,07,2005:Sumit Chhabra:Datatype for YEAR_AT_CURR_RESI being changed from double to int
            if (objGeneralInfo.YEAR_AT_CURR_RESI == Int32.MinValue)
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", objGeneralInfo.YEAR_AT_CURR_RESI);
            objDataWrapper.AddParameter("@YEARS_AT_PREV_ADD", objGeneralInfo.YEARS_AT_PREV_ADD);
            objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", objGeneralInfo.APP_EFFECTIVE_DATE);
            objDataWrapper.AddParameter("@APP_EXPIRATION_DATE", objGeneralInfo.APP_EXPIRATION_DATE);
            objDataWrapper.AddParameter("@APP_LOB", objGeneralInfo.APP_LOB);
            objDataWrapper.AddParameter("@APP_SUBLOB", objGeneralInfo.APP_SUBLOB);
            objDataWrapper.AddParameter("@CSR", objGeneralInfo.CSR);

            objDataWrapper.AddParameter("@UNDERWRITER", objGeneralInfo.UNDERWRITER);
            objDataWrapper.AddParameter("@IS_UNDER_REVIEW", null);
            objDataWrapper.AddParameter("@APP_AGENCY_ID", objGeneralInfo.APP_AGENCY_ID);
            objDataWrapper.AddParameter("@IS_ACTIVE", objGeneralInfo.IS_ACTIVE);

            objDataWrapper.AddParameter("@CREATED_BY", objGeneralInfo.CREATED_BY);
            objDataWrapper.AddParameter("@CREATED_DATETIME", objGeneralInfo.CREATED_DATETIME);
            objDataWrapper.AddParameter("@MODIFIED_BY", null);
            objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);

            objDataWrapper.AddParameter("@COUNTRY_ID", objGeneralInfo.COUNTRY_ID);
            objDataWrapper.AddParameter("@STATE_CODE", objGeneralInfo.STATE_CODE);

            //billing information
            objDataWrapper.AddParameter("@DIV_ID", objGeneralInfo.DIV_ID);
            objDataWrapper.AddParameter("@DEPT_ID", objGeneralInfo.DEPT_ID);
            objDataWrapper.AddParameter("@PC_ID", objGeneralInfo.PC_ID);
            objDataWrapper.AddParameter("@BILL_TYPE", objGeneralInfo.BILL_TYPE_ID);
            objDataWrapper.AddParameter("@COMPLETE_APP", objGeneralInfo.COMPLETE_APP);
            objDataWrapper.AddParameter("@PROPRTY_INSP_CREDIT", objGeneralInfo.PROPRTY_INSP_CREDIT);
            objDataWrapper.AddParameter("@INSTALL_PLAN_ID", objGeneralInfo.INSTALL_PLAN_ID);
            objDataWrapper.AddParameter("@CHARGE_OFF_PRMIUM", objGeneralInfo.CHARGE_OFF_PRMIUM);
            if (objGeneralInfo.RECEIVED_PRMIUM == Double.MinValue)
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", objGeneralInfo.RECEIVED_PRMIUM);
            if (objGeneralInfo.PROXY_SIGN_OBTAINED != int.MinValue) // proxy sign is set
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", objGeneralInfo.PROXY_SIGN_OBTAINED);
            }
            else
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", null);
            }
            if (objGeneralInfo.POLICY_TYPE == -1)
                objDataWrapper.AddParameter("@POLICY_TYPE", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@POLICY_TYPE", objGeneralInfo.POLICY_TYPE);

            objDataWrapper.AddParameter("@POLICY_TYPE_CODE", objGeneralInfo.POLICY_TYPE_CODE);

            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID, SqlDbType.Int, ParameterDirection.Output);

            SqlParameter objSqlAppNum = (SqlParameter)objDataWrapper.AddParameter("@NEW_APP_NUM", null, SqlDbType.VarChar, ParameterDirection.Output, 20);

            int returnResult = 0;
            int APP_ID = 0;
            //if transaction required

            if (this.boolTransactionRequired)
            {
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                if (returnResult > 0)
                {
                    string QQNum = "";
                    if (objGeneralInfo.QQ_ID != 0)
                    {
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
                        objDataWrapper.AddParameter("@QQ_ID", objGeneralInfo.QQ_ID);
                        DataSet dsQQ = objDataWrapper.ExecuteDataSet("Proc_GetQuickQuoteNumber");
                        if (dsQQ != null && dsQQ.Tables[0].Rows.Count > 0)
                            QQNum = dsQQ.Tables[0].Rows[0]["QQ_NUMBER"].ToString();
                    }
                    APP_ID = int.Parse(objSqlParameter.Value.ToString());
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strOldLOB = objGeneralInfo.APP_LOB;
                    objGeneralInfo.APP_LOB = GetLobDescription(objGeneralInfo.APP_LOB);
                    string strTranXML = objBuilder.GetTransactionLogXML(objGeneralInfo);
                    objGeneralInfo.APP_LOB = strOldLOB;
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.APP_ID = APP_ID;
                    objTransactionInfo.RECORDED_BY = objGeneralInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = objGeneralInfo.CREATED_DATETIME;
                    objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objGeneralInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1567", "");// "New application added from Quick Quote";
                    objTransactionInfo.CUSTOM_INFO = ";State = " + objGeneralInfo.STATE_CODE + ";Created Date = " + objGeneralInfo.CREATED_DATETIME.ToString() + ";User = " + objGeneralInfo.CREATED_BY;
                    if (objGeneralInfo.QQ_ID != 0 && QQNum != "")
                    {
                        objTransactionInfo.CUSTOM_INFO += ";Quote Number = " + QQNum;
                    }
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    objDataWrapper.ExecuteNonQuery(objTransactionInfo);

                    returnResult = 1;
                }
            }
            else//if no transaction required
            {
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                APP_ID = int.Parse(objSqlParameter.Value.ToString());//int.Parse(objDataWrapper.CommandParameters[1].Value.ToString());
            }

            objGeneralInfo.APP_ID = APP_ID;
            objGeneralInfo.APP_NUMBER = objSqlAppNum.Value.ToString();


            //Commit the transaction if all opeations are successfull.
            //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
            return APP_ID;


        }


        /// <summary>
        /// for lob description
        /// </summary>
        /// <param name="LOB_CODE"></param>
        /// <returns>LOB Description</returns>
        public string GetLobDescription(string LOB_CODE)
        {

            string strStoredProc = "Proc_GetLOBDescription";
            DataSet dsLob;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@APP_LOB", LOB_CODE);
            dsLob = objDataWrapper.ExecuteDataSet(strStoredProc);

            if (dsLob != null && dsLob.Tables[0].Rows.Count > 0)
                return dsLob.Tables[0].Rows[0]["lob_desc"].ToString();

            return "";

        }

        /// <summary>
        /// Updates the form's modified value
        /// </summary>
        /// <param name="objOldCustomerInfo">model object having old information</param>
        /// <param name="objCustomerInfo">model object having new information(form control's value)</param>
        /// <returns>no. of rows updated (1 or 0)</returns>
        public int UpdateApp(ClsGeneralInfo objGeneralInfo, DataWrapper objDataWrapper)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_UpdateApplicationGenInfo_ACORD";
            DateTime RecordDate = DateTime.Now;
            //string strTranXML;



            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            objDataWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID);
            objDataWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
            objDataWrapper.AddParameter("@PARENT_APP_VERSION_ID", objGeneralInfo.PARENT_APP_VERSION_ID);
            objDataWrapper.AddParameter("@APP_STATUS", objGeneralInfo.APP_STATUS);
            objDataWrapper.AddParameter("@APP_NUMBER", objGeneralInfo.APP_NUMBER);
            objDataWrapper.AddParameter("@APP_VERSION", objGeneralInfo.APP_VERSION);
            objDataWrapper.AddParameter("@APP_TERMS", objGeneralInfo.APP_TERMS);
            if (objGeneralInfo.APP_INCEPTION_DATE != DateTime.MinValue)
            {
                objDataWrapper.AddParameter("@APP_INCEPTION_DATE", objGeneralInfo.APP_INCEPTION_DATE);
            }
            //Nov,07,2005:Sumit Chhabra:Datatype for YEAR_AT_CURR_RESI being changed from double to int
            if (objGeneralInfo.YEAR_AT_CURR_RESI == Int32.MinValue)
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", objGeneralInfo.YEAR_AT_CURR_RESI);
            objDataWrapper.AddParameter("@YEARS_AT_PREV_ADD", objGeneralInfo.YEARS_AT_PREV_ADD);
            objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", objGeneralInfo.APP_EFFECTIVE_DATE);
            objDataWrapper.AddParameter("@APP_EXPIRATION_DATE", objGeneralInfo.APP_EXPIRATION_DATE);
            objDataWrapper.AddParameter("@APP_LOB", objGeneralInfo.APP_LOB);
            objDataWrapper.AddParameter("@APP_SUBLOB", objGeneralInfo.APP_SUBLOB);
            objDataWrapper.AddParameter("@CSR", objGeneralInfo.CSR);
            objDataWrapper.AddParameter("@UNDERWRITER", objGeneralInfo.UNDERWRITER);
            objDataWrapper.AddParameter("@IS_UNDER_REVIEW", null);
            objDataWrapper.AddParameter("@APP_AGENCY_ID", objGeneralInfo.APP_AGENCY_ID);
            objDataWrapper.AddParameter("@IS_ACTIVE", objGeneralInfo.IS_ACTIVE);
            objDataWrapper.AddParameter("@MODIFIED_BY", objGeneralInfo.MODIFIED_BY);
            objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objGeneralInfo.LAST_UPDATED_DATETIME);
            objDataWrapper.AddParameter("@COUNTRY_ID", objGeneralInfo.COUNTRY_ID);
            objDataWrapper.AddParameter("@STATE_CODE", objGeneralInfo.STATE_CODE);

            //billing information
            objDataWrapper.AddParameter("@DIV_ID", objGeneralInfo.DIV_ID);
            objDataWrapper.AddParameter("@DEPT_ID", objGeneralInfo.DEPT_ID);
            objDataWrapper.AddParameter("@PC_ID", objGeneralInfo.PC_ID);
            objDataWrapper.AddParameter("@BILL_TYPE", objGeneralInfo.BILL_TYPE_ID);
            objDataWrapper.AddParameter("@COMPLETE_APP", objGeneralInfo.COMPLETE_APP);
            objDataWrapper.AddParameter("@PROPRTY_INSP_CREDIT", objGeneralInfo.PROPRTY_INSP_CREDIT);
            objDataWrapper.AddParameter("@INSTALL_PLAN_ID", objGeneralInfo.INSTALL_PLAN_ID);
            objDataWrapper.AddParameter("@CHARGE_OFF_PRMIUM", objGeneralInfo.CHARGE_OFF_PRMIUM);
            if (objGeneralInfo.RECEIVED_PRMIUM == Double.MinValue)
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@RECEIVED_PRMIUM", objGeneralInfo.RECEIVED_PRMIUM);
            if (objGeneralInfo.PROXY_SIGN_OBTAINED != int.MinValue) // proxy sign is set
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", objGeneralInfo.PROXY_SIGN_OBTAINED);
            }
            else
            {
                objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", null);
            }
            if (objGeneralInfo.POLICY_TYPE == -1)
                objDataWrapper.AddParameter("@POLICY_TYPE", System.DBNull.Value);
            else
                objDataWrapper.AddParameter("@POLICY_TYPE_CODE", objGeneralInfo.POLICY_TYPE);

            objDataWrapper.AddParameter("@POLICY_TYPE", objGeneralInfo.POLICY_TYPE_CODE);

            int returnResult = 0;

            if (TransactionRequired)
            {
                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");

                objTransactionInfo.TRANS_TYPE_ID = 2;
                objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                objTransactionInfo.RECORDED_BY = objGeneralInfo.MODIFIED_BY;
                objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1566", "");//"Application information is modified";


                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
            }
            else
            {
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
            }
            //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return returnResult;

        }

        /// <summary>
        /// Following method returns Lob Code corresponding to a LobId
        /// </summary>
        /// <param name="gstrLobID"></param>
        /// <returns>LobCode</returns>
        new public string GetLobCodeForLobId(string gstrLobID)
        {
            string strLOBCODE = "";

            if (gstrLobID == ((int)enumLOB.AUTOP).ToString())
                strLOBCODE = "PPA";
            else if (gstrLobID == ((int)enumLOB.HOME).ToString())
                strLOBCODE = "HOME";
            else if (gstrLobID == ((int)enumLOB.BOAT).ToString())
                strLOBCODE = "WAT";
            else if (gstrLobID == ((int)enumLOB.CYCL).ToString())
                strLOBCODE = "MOT";
            else if (gstrLobID == ((int)enumLOB.REDW).ToString())
                strLOBCODE = "RENT";
            else if (gstrLobID == ((int)enumLOB.UMB).ToString())
                strLOBCODE = "UMB";
            return strLOBCODE;

        }


        #region INPUT XMLS UNDERWRITING RULES
        /// <summary>
        /// This function will be used to return the input xml for all LOBs.
        /// This input xml will be checked against a set of  underwriting rules for respective LOBs.
        /// </summary>
        /// <param name="customerID">Customer ID</param>
        /// <param name="appID">Application ID</param>
        /// <param name="appVersionID">Application Version ID</param>
        /// <param name="appLobID">Application LOB ID</param>
        /// <returns>string: this return the inputs in form of a string</returns>
        /// 
        public string GetRuleInputXML(string appLobID, string strCalled)
        {
            try
            {
                string inputXML = "";
                if (appLobID != "0")
                {
                    // switch case on the basis of the lob
                    switch (appLobID)
                    {
                        case LOB_HOME:
                            inputXML = FetchHORuleInputXML(strCalled);
                            break;
                        case LOB_PRIVATE_PASSENGER:
                            inputXML = FetchAutoRuleInputXML(strCalled);
                            break;
                        case LOB_MOTORCYCLE:
                            inputXML = FetchMotorcycleRuleInputXML(strCalled);
                            break;
                        case LOB_WATERCRAFT:
                            inputXML = FetchWatercraftRuleInputXML(strCalled);
                            break;
                        case LOB_RENTAL_DWELLING:
                            inputXML = FetchRentalDwellingRuleInputXML(strCalled);
                            break;
                        case LOB_UMBRELLA:
                            inputXML = FetchUmbrellaRuleInputXML(strCalled);
                            break;
                        case LOB_AVIATION:
                            inputXML = FetchAviationRuleInputXML(strCalled);
                            break;
                        default:
                            inputXML = "<INPUTXML></INPUTXML>";
                            break;
                    }
                }
                return inputXML;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }

        // To filter the string
        private string ReplaceString(string strReturnXML)
        {
            strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
            strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
            strReturnXML = strReturnXML.Replace("<Table>", "");
            strReturnXML = strReturnXML.Replace("</Table>", "");
            strReturnXML = strReturnXML.Replace("<Table1>", "");
            strReturnXML = strReturnXML.Replace("</Table1>", "");

            return strReturnXML;
        }

        public string SetForbiddenRenewalRefferalRule(string Customerid, string Policyid, string Policyversionid, string RuleInputXml)
        {
            string strChangedNodName = "";
            string strStoredProcForOldRuleXmlAllVersion = "PROC_FetchOldRuleInputXml";
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", Customerid);
                objDataWrapper.AddParameter("@POLICY_ID", Policyid);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policyversionid);
                objDataWrapper.AddParameter("@CALLED_FROM", "Policy");
                // get Rule Xml for all previous versions
                DataSet dsXML = objDataWrapper.ExecuteDataSet(strStoredProcForOldRuleXmlAllVersion);
                objDataWrapper.ClearParameteres();
                if (dsXML.Tables.Count > 0 && dsXML.Tables[0] != null)
                {
                    if (dsXML.Tables[0].Rows[0]["PRIORLOSS"].ToString() == "N")
                    {
                        XmlDocument ruleDoc = new XmlDocument();
                        ruleDoc.LoadXml(RuleInputXml);

                        // Get rule default Xml
                        string strPath = ClsCommon.GetKeyValueWithIP("RULES_DEFAULT");

                        XmlDocument DefaultRuleXML = new XmlDocument();
                        DefaultRuleXML.Load(strPath);

                        foreach (DataRow row in dsXML.Tables[0].Rows)
                        {
                            // Load Old rule Xml
                            XmlDocument OldRuleXML = new XmlDocument();
                            OldRuleXML.LoadXml(row["RULE_INPUT_XML"].ToString());

                            XmlNodeList tmpList = null;
                            XmlNode tmpNod = null, tmpLobSubnod = null, tmpLobCommonValue = null;//tmpModNod = null,

                            //travel Lob wise xml
                            tmpLobSubnod = DefaultRuleXML.SelectSingleNode("RULES/RULES_FORBIDDEN_RENEWAL/LOB[@ID='" + row["POLICY_LOB"].ToString().ToUpper().Trim() + "']");
                            tmpLobCommonValue = DefaultRuleXML.SelectSingleNode("RULES/COMMON_RULE_NODE/LOB[@ID='" + row["POLICY_LOB"].ToString().ToUpper().Trim() + "']");
                            foreach (XmlNode nod in tmpLobSubnod.ChildNodes)
                            {
                                if (nod.NodeType != XmlNodeType.Comment)
                                {
                                    string NodName = nod.Name;
                                    string NodValue = nod.InnerText;

                                    tmpNod = OldRuleXML.SelectSingleNode("INPUTXML");
                                    if (tmpNod != null)
                                        tmpList = tmpNod.SelectNodes("//" + NodName);
                                    if (tmpList != null)//&& tmpList.Count!=1)
                                    {
                                        foreach (XmlNode tmpnd in tmpList)
                                        {
                                            string oldID = "", newID = "";
                                            if (tmpnd.ParentNode.Attributes["ID"] != null)
                                                oldID = tmpnd.ParentNode.Attributes["ID"].Value.ToString();
                                            foreach (XmlNode tmpNwNod in ruleDoc.SelectNodes("//" + NodName))
                                            {
                                                if (tmpNwNod.ParentNode.Attributes["ID"] != null)
                                                {
                                                    if (oldID == tmpNwNod.ParentNode.Attributes["ID"].Value.ToString())
                                                    {
                                                        newID = tmpNwNod.ParentNode.Attributes["ID"].Value.ToString();

                                                        if (tmpNwNod != null)
                                                        {
                                                            if (NodValue.IndexOf(tmpNwNod.InnerText) != -1 && strChangedNodName.IndexOf(NodName + "~" + newID) == -1)
                                                            {
                                                                if (strChangedNodName == "")
                                                                    strChangedNodName = NodName + "~" + newID;
                                                                else
                                                                    strChangedNodName = strChangedNodName + "^" + NodName + "~" + newID;
                                                                bool isCommonChanged = false;
                                                                foreach (XmlNode CommonNod in tmpLobCommonValue.ChildNodes)
                                                                {
                                                                    if (NodName == CommonNod.Name)
                                                                    {
                                                                        tmpNwNod.InnerText = CommonNod.InnerText;
                                                                        isCommonChanged = true;
                                                                    }
                                                                    //else
                                                                    //tmpNwNod.InnerText="N";
                                                                }
                                                                if (isCommonChanged == false)
                                                                    tmpNwNod.InnerText = "N";
                                                            }
                                                        }
                                                    }
                                                }
                                                // if parrent node does not have id
                                                else
                                                {
                                                    if (tmpnd.ParentNode.ParentNode.ParentNode.Attributes["ID"] != null)
                                                        oldID = tmpnd.ParentNode.ParentNode.ParentNode.Attributes["ID"].Value.ToString();
                                                    //foreach(XmlNode tmpNwNod in ruleDoc.SelectNodes("//"+NodName))
                                                    //{
                                                    if (tmpNwNod.ParentNode.ParentNode.ParentNode.Attributes["ID"] != null)
                                                    {
                                                        if (oldID == tmpNwNod.ParentNode.ParentNode.ParentNode.Attributes["ID"].Value.ToString())
                                                        {
                                                            newID = tmpNwNod.ParentNode.ParentNode.ParentNode.Attributes["ID"].Value.ToString();
                                                            if (tmpNwNod != null)
                                                            {
                                                                if (NodValue.IndexOf(tmpNwNod.InnerText) != -1 && strChangedNodName.IndexOf(NodName + "~" + newID) == -1)
                                                                {
                                                                    if (strChangedNodName == "")
                                                                        strChangedNodName = NodName + "~" + newID;
                                                                    else
                                                                        strChangedNodName = strChangedNodName + "^" + NodName + "~" + newID;
                                                                    bool isCommonChanged = false;
                                                                    foreach (XmlNode CommonNod in tmpLobCommonValue.ChildNodes)
                                                                    {
                                                                        if (NodName == CommonNod.Name)
                                                                        {
                                                                            tmpNwNod.InnerText = CommonNod.InnerText;
                                                                            isCommonChanged = true;
                                                                        }
                                                                        //else
                                                                        //tmpNwNod.InnerText="N";
                                                                    }
                                                                    if (isCommonChanged == false)
                                                                        tmpNwNod.InnerText = "N";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //}
                                                }
                                            }
                                        }
                                    }
                                    /*else if(tmpList!=null)
                                    {
                                        tmpModNod=tmpNod.SelectSingleNode("//"+NodName);
                                        if(tmpModNod!=null)
                                        {
                                            if(NodValue==tmpModNod.InnerText && strChangedNodName.IndexOf(NodName)==-1)
                                            {
                                                if(strChangedNodName=="")
                                                    strChangedNodName=NodName;
                                                else 
                                                    strChangedNodName=strChangedNodName+"^"+NodName;
                                                tmpNod=ruleDoc.SelectSingleNode("//"+NodName);
                                                bool IsCommonChange=false;
                                                foreach(XmlNode CommonNod in tmpLobCommonValue.ChildNodes)
                                                {
                                                    if(NodName == CommonNod.Name)
                                                    {
                                                        IsCommonChange=true;
                                                        tmpNod.InnerText=CommonNod.InnerText;
                                                    }
                                                    //else
                                                    //	tmpNod.InnerText="N";
                                                }
                                                if(IsCommonChange==false)
                                                    tmpNod.InnerText="N";
                                            }
                                        }
                                    }*/
                                }
                            }
                        }
                        return ruleDoc.OuterXml;
                    }
                    else
                        return RuleInputXml;
                }
                else
                    return RuleInputXml;
            }
            catch
            {
                return RuleInputXml;
            }
        }
        // Get Application Info 
        private string GetAppLevelInfoForRules(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();

            string strStoredProcForHO_ApplicationComponet = "Proc_GetPPARule_APP";
            string strReturnXML = "";
            string strRulesDefValue = GetRulesDefaultXml();
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                if (strCalled != "FromHomeowner")
                {
                    returnString.Append("<INPUTXML>");
                    returnString.Append("<APPLICATIONS>");
                    // get the application info 
                    objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                    objDataWrapper.AddParameter("@APPID", appID);
                    objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                    objDataWrapper.AddParameter("@DESC", strRulesDefValue);
                    //objDataWrapper.AddParameter("@DEFAULT_DOC",strRulesDefValue);
                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_ApplicationComponet);
                    objDataWrapper.ClearParameteres();
                    returnString.Append("<APPLICATION>");
                    strReturnXML = dsTempXML.GetXml();
                    strReturnXML = ReplaceString(strReturnXML);
                    returnString.Append(strReturnXML);
                    returnString.Append("</APPLICATION>");
                    returnString.Append("</APPLICATIONS>");
                }
                return returnString.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <param name="strCalled"></param>
        /// <returns>inputXML as a string </returns>
        private string FetchHORuleInputXML(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForHO_LocationComponet = "Proc_GetHORule_LocationInfo";
            string strStoredProcForHO_DwellingComponent = "Proc_GetHORule_DwellingInfo";
            string strStoredProcForHO_RatingComponent = "Proc_GetHORule_RatingInfo";
            string strStoredProcForHO_CoverageLimitComponent = "Proc_GetHORule_DwellingCoverageInfo";
            string strStoredProcForHO_EndorsementComponent = "Proc_GetHORule_Endorsement"; //Added on 20 June 2006
            string strStoredProcForHO_GenInfoComponent = "Proc_GetHORule_GenInfo";
            string strStoredProcForHOSpl_GenInfoComponent = "Proc_GetHORule_Sch_Items_Cvgs";
            string strStoredProcForHO_SolidFuelComponent = "Proc_GetHORule_SolidFuelInfo";
            string strStoredProcForHO_RVInfoComponent = "Proc_GetHORule_RVInfo";
            string strStoredProcForHORule_AddInterest = "Proc_GetHORule_AddInterest";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetAppLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                // Get the Location Ids against the customerID, appID, appVersionID
                //********* Check whether login user is agency.
                string strSystemID;

                if (IsEODProcess)
                {
                    strSystemID = EODSystemID;
                }
                else
                {
                    strSystemID = System.Web.HttpContext.Current.Session["systemId"].ToString();
                }
                //*********
                //get the Location details for each location				
                string LocationIDs = ClsGeneralInformation.GetLocationIDs(customerID, appID, appVersionID);
                int intLocNo = 0;
                string[] LocationID = new string[0];
                if (LocationIDs != "-1")
                {
                    LocationID = LocationIDs.Split('^');
                    intLocNo = LocationID.Length;
                }
                returnString.Append("<LOCATIONS COUNT='" + intLocNo + "'>");
                if (LocationIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < LocationID.Length; iCounter++)
                    {
                        string strLocationID = LocationID[iCounter];
                        returnString.Append("<LOCATION ID='" + LocationID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@LOCATIONID", LocationID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_LocationComponet);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</LOCATION>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</LOCATIONS>");

                //get the Dwelling details for each dwelling
                string dwellingIDs = ClsGeneralInformation.
                    GetDwellingIDs(customerID, appID, appVersionID);
                int intDwellingNo = 0;
                string[] dwellingID = new string[0];
                if (dwellingIDs != "-1")
                {
                    dwellingID = dwellingIDs.Split('^');
                    intDwellingNo = dwellingID.Length;
                }
                returnString.Append("<DWELLINGS COUNT='" + intDwellingNo + "'>");
                if (dwellingIDs != "-1")
                {
                    // Run a loop to get the inputXML for each DwellingID
                    for (int iCounter = 0; iCounter < dwellingID.Length; iCounter++)
                    {

                        returnString.Append("<DWELLINGINFO>");
                        // 1. Dwelling
                        string strDwellingId = dwellingID[iCounter];
                        returnString.Append("<DWELLING ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_DwellingComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</DWELLING>");
                        //2. Rating Info string 
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<RATINGINFO ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_RatingComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</RATINGINFO>");
                        //3. Additional Interest string 
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<ADDINTEREST ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHORule_AddInterest);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</ADDINTEREST>");
                        //4.Coverage
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<COVERAGE ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_CoverageLimitComponent);

                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);

                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");
                        //Replace Table4 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table4>", "");
                        strReturnXML = strReturnXML.Replace("</Table4>", "");
                        strReturnXML = strReturnXML.Replace("<ADDDEDUCTIBLE_DESCRIPTION>", "<ADDDEDUCT><ADDDEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</ADDDEDUCTIBLE_DESCRIPTION>", "</ADDDEDUCT_DES></ADDDEDUCT>");

                        returnString.Append(strReturnXML);
                        returnString.Append("</COVERAGE>");
                        //4. Endorsement : Modified 20 June 2006
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<ENDORSEMENT>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_EndorsementComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</ENDORSEMENT>");

                        //End 
                        returnString.Append("</DWELLINGINFO>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</DWELLINGS>");

                // RV Info  start

                string RV_VehicleIDs = ClsGeneralInformation.GetRV_VehicleIDs(customerID, appID, appVersionID);
                int intRV_VehNo = 0;
                string[] RV_VehicleID = new string[0];
                if (RV_VehicleIDs != "-1")
                {
                    RV_VehicleID = RV_VehicleIDs.Split('^');
                    intRV_VehNo = RV_VehicleID.Length;
                }
                returnString.Append("<RV_VEHICLES COUNT='" + intRV_VehNo + "'>");
                if (RV_VehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < RV_VehicleID.Length; iCounter++)
                    {
                        //string strLocationID=RV_VehicleID[iCounter];
                        returnString.Append("<RV_VEHICLE RV_VEHICLEID='" + RV_VehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@RV_VEHICLEID", RV_VehicleID[iCounter]);

                        // get  RV Vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_RVInfoComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</RV_VEHICLE>");
                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<ERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</RV_VEHICLES>");

                //	RV Info end

                //get the underwriting questions
                returnString.Append("<HOGENINFOS>");
                returnString.Append("<HOGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                objDataWrapper.AddParameter("@DESC", strCalled);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</HOGENINFO>");
                returnString.Append("</HOGENINFOS>");
                // Get the fuel Ids against the customerID, appID, appVersionID
                //get the fuel details for each fuel ID
                string strFuelIDs = ClsGeneralInformation.GetFuelIDs(customerID, appID, appVersionID);
                int intFuelNo = 0;
                string[] arrFuelID = new string[0];
                if (strFuelIDs != "-1")
                {
                    arrFuelID = strFuelIDs.Split('^');
                    intFuelNo = arrFuelID.Length;
                }
                returnString.Append("<FUELINFOS COUNT='" + intFuelNo + "'>");
                if (strFuelIDs != "-1")
                {
                    // Run a loop to get the inputXML for each fuel ID
                    for (int iCounter = 0; iCounter < arrFuelID.Length; iCounter++)
                    {
                        string strFuelID = arrFuelID[iCounter];
                        returnString.Append("<FUELINFO ID='" + arrFuelID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@FUELID", arrFuelID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHO_SolidFuelComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</FUELINFO>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<FUELERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</FUELINFOS>");
                //get the scheduled items coverages (Inland Marine)
                returnString.Append("<SCHEDULEDITEMS>");
                returnString.Append("<SCHEDULEDITEM>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                objDataWrapper.AddParameter("@DESC", strCalled);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForHOSpl_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</SCHEDULEDITEM>");
                returnString.Append("</SCHEDULEDITEMS>");

                //Watercraft rule applied if Boat attached with Homeowners is 'Y' Itrack No. 2789
                string boatwithhomeowner = ClsGeneralInformation.GetBoatWithHomeowner(customerID, appID, appVersionID, "APP");
                if (boatwithhomeowner == "1")
                {
                    strCalled = "FromHomeowner";
                    string strWatercraftRuleInputXML = FetchWatercraftRuleInputXML(strCalled);
                    returnString.Append(strWatercraftRuleInputXML);
                }
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        #region App level Rule verification for Aviation
        public static string GetAviationVehicleIDs(int customerID, int appID, int appVersionID, DataWrapper objWrapper)
        {
            string strStoredProc = "Proc_GetAviationVehicleIDs";

            if (objWrapper == null)
                objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strVehicleID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strVehicleID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strVehicleID = strVehicleID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strVehicleID;
        }
        private string FetchAviationRuleInputXML(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strProcAviation_VehicleComponent = "Proc_GetAviationRule_Vehicle";
            // string strProcAviation_CoverageLimitComponent = "Proc_GetAviationRule_CoverageInfo";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetAppLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the vehicle details for each vehicle				
                string vehicleIDs = GetAviationVehicleIDs(customerID, appID, appVersionID, objDataWrapper);
                int intVehicleNo = 0;
                string[] vehicleID = new string[0];
                if (vehicleIDs != "-1")
                {
                    vehicleID = vehicleIDs.Split('^');
                    intVehicleNo = vehicleID.Length;
                }
                returnString.Append("<VEHICLES COUNT='" + intVehicleNo + "'>");

                // Check whether login user is agency.
                string strSystemID;

                if (IsEODProcess)
                {
                    strSystemID = EODSystemID;
                }
                else
                {
                    strSystemID = System.Web.HttpContext.Current.Session["systemId"].ToString();
                }

                if (vehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        string strVehicleID = vehicleID[iCounter];
                        returnString.Append("<VEHICLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        // get vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strProcAviation_VehicleComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        /*//3.Coverage
                        returnString.Append("<COVERAGE ID='"+ vehicleID[iCounter]+"'>");
                        objDataWrapper.AddParameter("@CUSTOMERID",customerID);
                        objDataWrapper.AddParameter("@APPID",appID);
                        objDataWrapper.AddParameter("@APPVERSIONID",appVersionID);	
                        objDataWrapper.AddParameter("@VEHICLEID",vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@DESC",strCalled);	
                        objDataWrapper.AddParameter("@USER",strSystemID);							
                        dsTempXML =	objDataWrapper.ExecuteDataSet(strProcAviation_CoverageLimitComponent);	
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>","");
                        strReturnXML = strReturnXML.Replace("</Table1>","");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>","<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>","</COV_DES></COV>");	
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>","");
                        strReturnXML = strReturnXML.Replace("</Table2>","");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>","<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>","</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>","");
                        strReturnXML = strReturnXML.Replace("</Table3>","");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>","<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>","</DEDUCT_DES></DEDUCT>");
						
                        returnString.Append(strReturnXML);						
                        returnString.Append("</COVERAGE>");	
                        */
                        returnString.Append("</VEHICLE>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</VEHICLES>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        #endregion
        private string FetchAutoRuleInputXML(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForAuto_VehicleComponent = "Proc_GetPPARule_Vehcile";
            string strStoredProcForAuto_DriverComponent = "Proc_GetPPARule_Drivers";
            string strStoredProcForAuto_GenInfoComponent = "Proc_GetPPARule_GenInfo";
            string strStoredProcForPPA_CoverageLimitComponent = "Proc_GetPPARule_CoverageInfo";
            string strStoredProcForPPA_MVRInfoComponent = "Proc_GetMotorcycleRule_DriverMVR";
            string strReturnXML = "";
            DataSet dsTempXML;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetAppLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the vehicle details for each vehicle				
                string vehicleIDs = ClsGeneralInformation.GetVehicleIDs(customerID, appID, appVersionID);
                int intVehicleNo = 0;
                string[] vehicleID = new string[0];
                if (vehicleIDs != "-1")
                {
                    vehicleID = vehicleIDs.Split('^');
                    intVehicleNo = vehicleID.Length;
                }
                returnString.Append("<VEHICLES COUNT='" + intVehicleNo + "'>");

                // Check whether login user is agency.
                string strSystemID;

                if (IsEODProcess)
                {
                    strSystemID = EODSystemID;
                }
                else
                {
                    strSystemID = System.Web.HttpContext.Current.Session["systemId"].ToString();
                }

                if (vehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        string strVehicleID = vehicleID[iCounter];
                        returnString.Append("<VEHICLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        // get vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_VehicleComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        //Grandfather case for territory
                        //						strReturnXML = strReturnXML.Replace("<Table>","");
                        //						strReturnXML = strReturnXML.Replace("</Table>","");
                        //						strReturnXML = strReturnXML.Replace("<TERRITORY_DESCRIPTION>","<TERR><TERR_DES>");
                        //						strReturnXML = strReturnXML.Replace("</TERRITORY_DESCRIPTION>","</TERR_DES></TERR>");

                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);

                        //3.Coverage

                        returnString.Append("<COVERAGE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForPPA_CoverageLimitComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");

                        returnString.Append(strReturnXML);
                        returnString.Append("</COVERAGE>");
                        returnString.Append("</VEHICLE>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</VEHICLES>");

                //get the driver details for each driver

                string driverIDs = ClsDriverDetail.GetRuleDriverIDs(customerID, appID, appVersionID);
                int intDriverNo = 0;
                string[] driverID = new string[0];
                if (driverIDs != "-1")
                {
                    driverID = driverIDs.Split('^');
                    intDriverNo = driverID.Length;
                }
                returnString.Append("<DRIVERS COUNT='" + intDriverNo + "'>");
                if (driverIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        string strDriverId = driverID[iCounter];
                        returnString.Append("<DRIVER ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_DriverComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</DRIVER>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</DRIVERS>");

                //Start Get MVR Information
                string mvrIDs = GetRuleMVRIDs(customerID, appID, appVersionID);
                int intmvrNo = 0;
                string[] mvrID = new string[0];
                if (mvrIDs != "-1")
                {
                    mvrID = mvrIDs.Split('^');
                    intmvrNo = mvrID.Length;
                }
                if (mvrIDs != "-1")
                {
                    returnString.Append("<MVRS COUNT='" + intmvrNo + "'>");
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < mvrID.Length; iCounter++)
                    {
                        string strMVRId = mvrID[iCounter];
                        returnString.Append("<MVR ID='" + mvrID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        //objDataWrapper.AddParameter("@DRIVERID",driverID[iCounter]);
                        objDataWrapper.AddParameter("@APPMVRID", mvrID[iCounter]);
                        //objDataWrapper.AddParameter("@APPMVRID",mvrID[iCounter]);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForPPA_MVRInfoComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</MVR>");
                    }
                    returnString.Append("</MVRS>");
                }

                //******************** End MVR Information **********************	
                //get the underwriting questions
                returnString.Append("<AUTOGENINFOS>");
                returnString.Append("<AUTOGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForAuto_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</AUTOGENINFO>");
                returnString.Append("</AUTOGENINFOS>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        private string FetchMotorcycleRuleInputXML(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForMC_VehicleComponent = "Proc_GetMCRule_Motorcycle";
            string strStoredProcForMC_DriverComponent = "Proc_GetMotorcycleRule_Drivers";
            string strStoredProcForMC_GenInfoComponent = "Proc_GetMotorcycleRule_GenInfo";
            string strStoredProcForMC_MVRDriverInfoComponent = "Proc_GetMotorcycleRule_DriverMVR";
            string strStoredProcForMC_CoverageLimitComponent = "Proc_GetPPARule_CoverageInfo";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                // Check whether login user is agency.
                string strSystemID;

                if (IsEODProcess)
                {
                    strSystemID = EODSystemID;
                }
                else
                {
                    strSystemID = System.Web.HttpContext.Current.Session["systemId"].ToString();
                }

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetAppLevelInfoForRules(strCalled);
                returnString.Append(strResult);
                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the vehicle details for each vehicle				
                string vehicleIDs = ClsGeneralInformation.GetVehicleIDs(customerID, appID, appVersionID);
                int intVehicleNo = 0;
                string[] vehicleID = new string[0];
                if (vehicleIDs != "-1")
                {
                    vehicleID = vehicleIDs.Split('^');
                    intVehicleNo = vehicleID.Length;
                }
                returnString.Append("<MOTORCYCLES COUNT='" + intVehicleNo + "'>");
                if (vehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        string strVehicleID = vehicleID[iCounter];
                        returnString.Append("<MOTORCYCLE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        // get vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_VehicleComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        //						//Grandfather case for territory
                        //						strReturnXML = strReturnXML.Replace("<Table>","");
                        //						strReturnXML = strReturnXML.Replace("</Table>","");
                        //						strReturnXML = strReturnXML.Replace("<TERRITORY_DESCRIPTION>","<TERR><TERR_DES>");
                        //						strReturnXML = strReturnXML.Replace("</TERRITORY_DESCRIPTION>","</TERR_DES></TERR>");
                        //						//*****************************
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);

                        //Added by Manoj Rathore on 27 May 2009 Itrack # 5889

                        //Coverage						
                        returnString.Append("<COVERAGE ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_CoverageLimitComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");

                        returnString.Append(strReturnXML);
                        returnString.Append("</COVERAGE>");
                        //**************************************************
                        returnString.Append("</MOTORCYCLE>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</MOTORCYCLES>");

                //get the driver details for each driver

                string driverIDs = ClsDriverDetail.GetRuleDriverIDs(customerID, appID, appVersionID);
                int intDriverNo = 0;
                string[] driverID = new string[0];
                if (driverIDs != "-1")
                {
                    driverID = driverIDs.Split('^');
                    intDriverNo = driverID.Length;
                }
                returnString.Append("<DRIVERS COUNT='" + intDriverNo + "'>");
                if (driverIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        string strDriverId = driverID[iCounter];
                        returnString.Append("<DRIVER ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_DriverComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</DRIVER>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</DRIVERS>");

                //*************** Start Get MVR Information *************

                string mvrIDs = GetRuleMVRIDs(customerID, appID, appVersionID);
                int intmvrNo = 0;
                string[] mvrID = new string[0];
                if (mvrIDs != "-1")
                {
                    mvrID = mvrIDs.Split('^');
                    intmvrNo = mvrID.Length;
                }
                if (mvrIDs != "-1")
                {
                    returnString.Append("<MVRS COUNT='" + intmvrNo + "'>");
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < mvrID.Length; iCounter++)
                    {
                        string strMVRId = mvrID[iCounter];
                        returnString.Append("<MVR ID='" + mvrID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        //objDataWrapper.AddParameter("@DRIVERID",driverID[iCounter]);
                        objDataWrapper.AddParameter("@APPMVRID", mvrID[iCounter]);
                        //objDataWrapper.AddParameter("@APPMVRID",mvrID[iCounter]);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_MVRDriverInfoComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</MVR>");
                    }
                    returnString.Append("</MVRS>");
                }
                //*********** End MVR Information	 ***********

                //get the underwriting questions
                returnString.Append("<MOTOTRCYCLEGENINFOS>");
                returnString.Append("<MOTOTRCYCLEGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                objDataWrapper.AddParameter("@DESC", strCalled);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForMC_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</MOTOTRCYCLEGENINFO>");
                returnString.Append("</MOTOTRCYCLEGENINFOS>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        /// <summary>
        /// Gets MVR Ids against one application for policy rule implementation
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="Application Id"></param>
        /// <param name="ApplicationVersionID"></param>
        /// <returns>MVR Ids as carat separated string </returns>
        private string GetRuleWatMVRIDs(int customerID, int AppID, int AppVersionID)
        {
            string strStoredProc = "Proc_GetWatMVRIDs_App";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APPID", AppID);
            objWrapper.AddParameter("@APPVERSIONID", AppVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            int intCount = ds.Tables[0].Rows.Count;
            string strMVRIds = "-1";
            for (int i = 0; i < intCount; i++)
            {
                if (i == 0)
                {
                    strMVRIds = ds.Tables[0].Rows[i]["DRIVER_ID"].ToString() + "~" + ds.Tables[0].Rows[i]["APP_WATER_MVR_ID"].ToString();
                }
                else
                {
                    strMVRIds = strMVRIds + '^' + ds.Tables[0].Rows[i]["DRIVER_ID"].ToString() + "~" + ds.Tables[0].Rows[i]["APP_WATER_MVR_ID"].ToString();
                }
            }
            return strMVRIds;
        }
        private string FetchWatercraftRuleInputXML(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForWatercraft_BoatComponent = "Proc_GetWatercraftRule_Boat";
            string strStoredProcForWatercraft_OperatorComponent = "Proc_GetWatercraftRule_Operators";
            string strStoredProcForWatercraft_GenInfoComponent = "Proc_GetWatercraftRule_GenInfo";
            string strStoredProcForWatercraft_EquipmentComponent = "PROC_GETWATERCRAFTRULE_EQUIP";
            string strStoredProcForWatercraft_TrailerInfoComponent = "Proc_GetWatercraftRule_TrailerInfo";
            string strStoredProcForWatercraft_MVRInfoComponent = "Proc_GetWatercraftRule_MVRInfo";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetAppLevelInfoForRules(strCalled);
                returnString.Append(strResult);

                // Check whether login user is agency.
                string strSystemID;

                if (IsEODProcess)
                {
                    strSystemID = EODSystemID;
                }
                else
                {
                    strSystemID = System.Web.HttpContext.Current.Session["systemId"].ToString();
                }
                //
                string strRulesDefValue = GetRulesDefaultXml();
                // Get the Vehicle Ids against the customerID, appID, appVersionID
                //get the Boat details for each vehicle
                string strBoatID = clsWatercraftInformation.GetBoatIDs(customerID, appID, appVersionID);
                int intBoatNo = 0;
                string[] BoatID = new string[0];
                if (strBoatID != "-1")
                {
                    BoatID = strBoatID.Split('^');
                    intBoatNo = BoatID.Length;
                }
                returnString.Append("<BOATS COUNT='" + intBoatNo + "'>");
                if (strBoatID != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = strBoatID.Split('^');
                    // Run a loop to get the inputXML for each BOATID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<BOAT ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@BOATID", vehicleID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        objDataWrapper.AddParameter("@DEFAULT_DOC", strRulesDefValue);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_BoatComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");
                        //********************
                        //Add node for each vehicle  for count
                        int rowID = iCounter + 1;
                        string vehicleRowIDNode = "<BOATID>" + rowID.ToString() + "</BOATID>";
                        returnString.Append(vehicleRowIDNode);
                        returnString.Append(strReturnXML);
                        returnString.Append("</BOAT>");

                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<BOATERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</BOATS>");
                //get the Operator details for each operator

                string driverIDs = ClsDriverDetail.GetRuleWCDriverIDs(customerID, appID, appVersionID);
                int intOperatorNo = 0;
                string[] OperatorID = new string[0];
                if (driverIDs != "-1")
                {
                    OperatorID = driverIDs.Split('^');
                    intOperatorNo = OperatorID.Length;
                }
                returnString.Append("<OPERATORS COUNT='" + intOperatorNo + "'>");
                if (driverIDs != "-1")
                {
                    string[] driverID = new string[0];
                    driverID = driverIDs.Split('^');
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < driverID.Length; iCounter++)
                    {
                        returnString.Append("<OPERATOR ID='" + driverID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", driverID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_OperatorComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</OPERATOR>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<OPERATORERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</OPERATORS>");
                //**************** Start Get MVR Information ******************
                string mvrIDs = GetRuleWatMVRIDs(customerID, appID, appVersionID);
                int intmvrNo = 0;
                string[] mvrID = new string[0];
                if (mvrIDs != "-1")
                {
                    mvrID = mvrIDs.Split('^');
                    intmvrNo = mvrID.Length;
                }
                if (mvrIDs != "-1")
                {
                    returnString.Append("<MVRS COUNT='" + intmvrNo + "'>");
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < mvrID.Length; iCounter++)
                    {
                        string strMVRId = mvrID[iCounter];
                        string[] mvrdRIDs = strMVRId.Split('~');
                        string strDriverId = mvrdRIDs[0];
                        string MVRId = mvrdRIDs[1];

                        returnString.Append("<MVR ID='" + MVRId + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DRIVERID", strDriverId);
                        objDataWrapper.AddParameter("@APPMVRID", MVRId);
                        //objDataWrapper.AddParameter("@APPMVRID",mvrID[iCounter]);
                        // get driver detial
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_MVRInfoComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</MVR>");
                    }
                    returnString.Append("</MVRS>");
                }


                //******************** End MVR Information	**************************

                //	TRAILER INFO
                returnString.Append("<TRAILERS>");
                string strTrailerIDs = clsWatercraftInformation.GetTrailerBoatIDs(customerID, appID, appVersionID);
                //int intTrailerNo=0;
                //string[] TrailerID=new string[0];
                //				if(strTrailerIDs !="-1")
                //				{
                //					TrailerID=strTrailerIDs.Split('^');
                //					intTrailerNo=TrailerID.Length;
                //				}
                //				returnString.Append("<TRAILERS COUNT='" + intTrailerNo + "'>");	

                if (strTrailerIDs != "-1")
                {
                    string[] arrTrailerID = new string[0];
                    arrTrailerID = strTrailerIDs.Split('^');
                    // Run a loop to get the inputXML for each equipment ID
                    for (int iCounterForEquip = 0; iCounterForEquip < arrTrailerID.Length; iCounterForEquip++)
                    {
                        returnString.Append("<TRAILER ID='" + arrTrailerID[iCounterForEquip] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@TRAILER_ID", arrTrailerID[iCounterForEquip]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_TrailerInfoComponent); // Equipment Info		
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</TRAILER>");

                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<TRAILERERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</TRAILERS>");
                // END  TRAILER INFO 

                //GET EQUIPMENT DETAIL FOR WC  PK
                returnString.Append("<WATERCRAFTEQUIPMENTS>");
                string strEquipIDs = clsWatercraftInformation.GetEquipmentID(customerID, appID, appVersionID);
                if (strEquipIDs != "-1")
                {
                    string[] arrEquipID = new string[0];
                    arrEquipID = strEquipIDs.Split('^');
                    // Run a loop to get the inputXML for each equipment ID
                    for (int iCounterForEquip = 0; iCounterForEquip < arrEquipID.Length; iCounterForEquip++)
                    {
                        returnString.Append("<WATERCRAFTEQUIPMENT ID='" + arrEquipID[iCounterForEquip] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@EQUIP_ID", arrEquipID[iCounterForEquip]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_EquipmentComponent); // Equipment Info		
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</WATERCRAFTEQUIPMENT>");

                    }
                }
                returnString.Append("</WATERCRAFTEQUIPMENTS>");

                //END Equipment Detail  WC PK


                //get the underwriting questions for watercraft
                ClsWatercraftGenInformation obj = new ClsWatercraftGenInformation();
                int intWCGenCount = obj.FetchData(appID, customerID, appVersionID).Tables[0].Rows.Count;
                returnString.Append("<WATERCRAFTGENINFOS COUNT='" + intWCGenCount + "'>");
                if (intWCGenCount > 0)
                {
                    returnString.Append("<WATERCRAFTGENINFO>");
                    objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                    objDataWrapper.AddParameter("@APPID", appID);
                    objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                    dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForWatercraft_GenInfoComponent);	// Gen Info		
                    objDataWrapper.ClearParameteres();
                    strReturnXML = dsTempXML.GetXml();
                    strReturnXML = ReplaceString(strReturnXML);
                    returnString.Append(strReturnXML);
                    returnString.Append("</WATERCRAFTGENINFO>");
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<WATERCRAFTGENINFOERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</WATERCRAFTGENINFOS>");
                if (strCalled != "FromHomeowner")
                {
                    returnString.Append("</INPUTXML>");
                }


                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <param name="strCalled"></param>
        /// <returns>inputXML as a string for RD </returns>
        //New
        private string FetchRentalDwellingRuleInputXML(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForRD_LocationComponet = "Proc_GetRDRule_LocationInfo";
            string strStoredProcForRD_DwellingComponent = "Proc_GetRDRule_DwellingInfo";
            string strStoredProcForRD_RatingComponent = "Proc_GetRDRule_RatingInfo";
            string strStoredProcForRD_CoverageLimitComponent = "Proc_GetRDRule_DwellingCoverageInfo";
            string strStoredProcForRD_GenInfoComponent = "Proc_GetRDRule_GenInfo";
            string strStoredProcForRD_AddInterest = "Proc_GetRDRule_AddInterest";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetAppLevelInfoForRules(strCalled);
                returnString.Append(strResult);

                //********* Check whether login user is agency.
                string strSystemID;

                if (IsEODProcess)
                {
                    strSystemID = EODSystemID;
                }
                else
                {
                    strSystemID = System.Web.HttpContext.Current.Session["systemId"].ToString();
                }
                //*********
                // Get the Location Ids against the customerID, appID, appVersionID
                //get the Location details for each location				
                string LocationIDs = ClsGeneralInformation.GetLocationIDs(customerID, appID, appVersionID);
                int intLocNo = 0;
                string[] LocationID = new string[0];
                if (LocationIDs != "-1")
                {
                    LocationID = LocationIDs.Split('^');
                    intLocNo = LocationID.Length;
                }
                returnString.Append("<LOCATIONS COUNT='" + intLocNo + "'>");
                if (LocationIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < LocationID.Length; iCounter++)
                    {
                        string strLocationID = LocationID[iCounter];
                        returnString.Append("<LOCATION ID='" + LocationID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@LOCATIONID", LocationID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_LocationComponet);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</LOCATION>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</LOCATIONS>");

                //get the Dwelling details for each dwelling
                string dwellingIDs = ClsGeneralInformation.GetDwellingIDs(customerID, appID, appVersionID);
                int intDwellingNo = 0;
                string[] dwellingID = new string[0];
                if (dwellingIDs != "-1")
                {
                    dwellingID = dwellingIDs.Split('^');
                    intDwellingNo = dwellingID.Length;
                }
                returnString.Append("<DWELLINGS COUNT='" + intDwellingNo + "'>");
                if (dwellingIDs != "-1")
                {
                    // Run a loop to get the inputXML for each DwellingID
                    for (int iCounter = 0; iCounter < dwellingID.Length; iCounter++)
                    {

                        returnString.Append("<DWELLINGINFO>");
                        // 1. Dwelling
                        string strDwellingId = dwellingID[iCounter];
                        returnString.Append("<DWELLING ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_DwellingComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</DWELLING>");
                        //2. Rating Info string 
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<RATINGINFO ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_RatingComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</RATINGINFO>");
                        //3. Additional Interest string 
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<ADDINTEREST ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_AddInterest);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</ADDINTEREST>");
                        //4.Coverage
                        strDwellingId = dwellingID[iCounter];
                        returnString.Append("<COVERAGE ID='" + dwellingID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@DWELLINGID", dwellingID[iCounter]);
                        objDataWrapper.AddParameter("@DESC", strCalled);
                        objDataWrapper.AddParameter("@USER", strSystemID);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_CoverageLimitComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);

                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //
                        //Replace Table2 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table2>", "");
                        strReturnXML = strReturnXML.Replace("</Table2>", "");
                        strReturnXML = strReturnXML.Replace("<LIMIT_DESCRIPTION>", "<LIMIT><LIMIT_DES>");
                        strReturnXML = strReturnXML.Replace("</LIMIT_DESCRIPTION>", "</LIMIT_DES></LIMIT>");
                        //********************
                        //Replace Table3 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table3>", "");
                        strReturnXML = strReturnXML.Replace("</Table3>", "");
                        strReturnXML = strReturnXML.Replace("<DEDUCTIBLE_DESCRIPTION>", "<DEDUCT><DEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</DEDUCTIBLE_DESCRIPTION>", "</DEDUCT_DES></DEDUCT>");
                        //********************
                        //Replace Table4 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table4>", "");
                        strReturnXML = strReturnXML.Replace("</Table4>", "");
                        strReturnXML = strReturnXML.Replace("<ADDDEDUCTIBLE_DESCRIPTION>", "<ADDDEDUCT><ADDDEDUCT_DES>");
                        strReturnXML = strReturnXML.Replace("</ADDDEDUCTIBLE_DESCRIPTION>", "</ADDDEDUCT_DES></ADDDEDUCT>");
                        //***********************
                        returnString.Append(strReturnXML);
                        returnString.Append("</COVERAGE>");
                        returnString.Append("</DWELLINGINFO>");
                    }
                }
                else
                {
                    returnString.Append("<ERRORS>");
                    strReturnXML = "<ERROR ERRFOUND = 'T'/>";
                    returnString.Append(strReturnXML);
                    returnString.Append("</ERRORS>");
                }
                returnString.Append("</DWELLINGS>");
                //get the underwriting questions
                returnString.Append("<RDGENINFOS>");
                returnString.Append("<RDGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                objDataWrapper.AddParameter("@DESC", strCalled);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</RDGENINFO>");
                returnString.Append("</RDGENINFOS>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        /// <summary>
        ///  App rule input xml for Umbrella
        /// </summary>q
        /// <returns></returns>
        private string FetchUmbrellaRuleInputXML(string strCalled)
        {
            StringBuilder returnString = new StringBuilder();
            string strStoredProcForUM_LocationComponet = "Proc_GetUMRule_LocationInfo";
            string strStoredProcForUM_BoatComponent = "Proc_GetUMRule_Boat";
            string strStoredProcForUM_RecrVehicleComponent = "Proc_GetUMRule_Rec_Vehicle";
            string strStoredProcForUM_UnderLayingofScheduleComponent = "Proc_GetUMRule_SchdUnderlayingInfo";
            string strStoredProcForUM_GenInfoComponent = "Proc_GetUMRule_GenInfo";
            string strStoredProcForRD_ExcesslimitEndorsements = "Proc_GetUMRule_Excesslimit_Endorsements";
            //string strStoredProcForUM_DriverComponent = "";
            string strReturnXML = "";
            DataSet dsTempXML;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                string strResult = GetAppLevelInfoForRules(strCalled);
                returnString.Append(strResult);

                //********* Check whether login user is agency.
                string strSystemID;

                if (IsEODProcess)
                {
                    strSystemID = EODSystemID;
                }
                else
                {
                    strSystemID = System.Web.HttpContext.Current.Session["systemId"].ToString();
                }
                //*********
                // Get the Location Ids against the customerID, appID, appVersionID
                //get the Location details for each location				
                string LocationIDs = ClsGeneralInformation.GetUMLocationIDs(customerID, appID, appVersionID);
                int intLocNo = 0;
                string[] LocationID = new string[0];
                if (LocationIDs != "-1")
                {
                    LocationID = LocationIDs.Split('^');
                    intLocNo = LocationID.Length;
                }
                returnString.Append("<LOCATIONS COUNT='" + intLocNo + "'>");
                if (LocationIDs != "-1")
                {
                    // Run a loop to get the inputXML for each vehicleID
                    for (int iCounter = 0; iCounter < LocationID.Length; iCounter++)
                    {
                        string strLocationID = LocationID[iCounter];
                        returnString.Append("<LOCATION ID='" + LocationID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@LOCATIONID", LocationID[iCounter]);
                        // get Location details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_LocationComponet);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</LOCATION>");
                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<ERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</LOCATIONS>");

                //get the Boat details for each vehicle
                string strBoatID = clsWatercraftInformation.GetUMBoatIDs(customerID, appID, appVersionID);
                int intBoatNo = 0;
                string[] BoatID = new string[0];
                if (strBoatID != "-1")
                {
                    BoatID = strBoatID.Split('^');
                    intBoatNo = BoatID.Length;
                }
                returnString.Append("<BOATS COUNT='" + intBoatNo + "'>");
                if (strBoatID != "-1")
                {
                    string[] vehicleID = new string[0];
                    vehicleID = strBoatID.Split('^');
                    // Run a loop to get the inputXML for each BOATID
                    for (int iCounter = 0; iCounter < vehicleID.Length; iCounter++)
                    {
                        returnString.Append("<BOAT ID='" + vehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@BOATID", vehicleID[iCounter]);
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_BoatComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        //Repalce Table1 Nodes.
                        strReturnXML = strReturnXML.Replace("<Table1>", "");
                        strReturnXML = strReturnXML.Replace("</Table1>", "");
                        strReturnXML = strReturnXML.Replace("<COVERAGE_DES>", "<COV><COV_DES>");
                        strReturnXML = strReturnXML.Replace("</COVERAGE_DES>", "</COV_DES></COV>");
                        //Add node for each vehicle  for count
                        int rowID = iCounter + 1;
                        string vehicleRowIDNode = "<BOATID>" + rowID.ToString() + "</BOATID>";
                        returnString.Append(vehicleRowIDNode);
                        returnString.Append(strReturnXML);
                        returnString.Append("</BOAT>");

                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<BOATERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</BOATS>");
                // Recreational Vehicle
                //get the  Recreational vehicle Ids			
                string RVehicleIDs = ClsHomeRecrVehicles.GetAppRecreationVehicleIDs(customerID, appID, appVersionID);
                int intVehicleNo = 0;
                string[] RVehicleID = new string[0];
                if (RVehicleIDs != "-1")
                {
                    RVehicleID = RVehicleIDs.Split('^');
                    intVehicleNo = RVehicleID.Length;
                }
                returnString.Append("<RVEHICLES COUNT='" + intVehicleNo + "'>");

                // Check whether login user is agency.
                if (RVehicleIDs != "-1")
                {
                    // Run a loop to get the inputXML for each RVehicleID
                    for (int iCounter = 0; iCounter < RVehicleID.Length; iCounter++)
                    {
                        string strVehicleID = RVehicleID[iCounter];
                        returnString.Append("<RVEHICLE ID='" + RVehicleID[iCounter] + "'>");
                        objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                        objDataWrapper.AddParameter("@APPID", appID);
                        objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                        objDataWrapper.AddParameter("@VEHICLEID", RVehicleID[iCounter]);

                        // get vehicle details
                        dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_RecrVehicleComponent);
                        objDataWrapper.ClearParameteres();
                        strReturnXML = dsTempXML.GetXml();
                        strReturnXML = ReplaceString(strReturnXML);
                        returnString.Append(strReturnXML);
                        returnString.Append("</RVEHICLE>");
                    }
                }
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<ERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                returnString.Append("</RVEHICLES>");
                // Umbrella Drivers 
                //get the driver details for each driver				
                //				string driverIDs = ClsDriverDetail.GetUMRuleDriverIDs(customerID,appID,appVersionID);
                //				int intDriverNo=0;
                //				string[] driverID = new string[0];
                //				if(driverIDs !="-1")
                //				{
                //					driverID = driverIDs.Split('^');					 
                //					intDriverNo=driverID.Length;	
                //				}
                //				returnString.Append("<DRIVERS COUNT='"+ intDriverNo +"'>");				
                //				if(driverIDs != "-1")
                //				{
                //					// Run a loop to get the inputXML for each vehicleID
                //					for (int iCounter=0; iCounter<driverID.Length ; iCounter++)
                //					{			
                //						string strDriverId=driverID[iCounter];
                //						returnString.Append("<DRIVER ID='"+ driverID[iCounter]+"'>");
                //						objDataWrapper.AddParameter("@CUSTOMERID",customerID);
                //						objDataWrapper.AddParameter("@APPID",appID);
                //						objDataWrapper.AddParameter("@APPVERSIONID",appVersionID);	
                //						objDataWrapper.AddParameter("@DRIVERID",driverID[iCounter]);						
                //						// get driver detial
                //						dsTempXML =	objDataWrapper.ExecuteDataSet(strStoredProcForUM_DriverComponent);			
                //						objDataWrapper.ClearParameteres();
                //						strReturnXML = dsTempXML.GetXml();
                //						strReturnXML= ReplaceString(strReturnXML);
                //						returnString.Append(strReturnXML);						
                //						returnString.Append("</DRIVER>");
                //					}
                //				}
                //				else
                //				{
                //					returnString.Append("<ERRORS>");
                //					strReturnXML = "<ERROR ERRFOUND = 'T'/>";					
                //					returnString.Append(strReturnXML);
                //					returnString.Append("</ERRORS>");
                //				}
                //				returnString.Append("</DRIVERS>");



                // Schedule of Underlaying 				
                returnString.Append("<SCHEDULES>");
                returnString.Append("<SCHEDULE>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_UnderLayingofScheduleComponent);	// Underlayings of Schedule	
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</SCHEDULE>");
                returnString.Append("</SCHEDULES>");

                //Excess Limit and Endorsements
                returnString.Append("<EXCESSLIMITS>");
                returnString.Append("<EXCESSLIMIT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForRD_ExcesslimitEndorsements);
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</EXCESSLIMIT>");
                returnString.Append("</EXCESSLIMITS>");
                //****************************

                //get the underwriting questions
                returnString.Append("<UMGENINFOS>");
                returnString.Append("<UMGENINFO>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@APPID", appID);
                objDataWrapper.AddParameter("@APPVERSIONID", appVersionID);
                objDataWrapper.AddParameter("@DESC", strCalled);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProcForUM_GenInfoComponent);	// Gen Info		
                objDataWrapper.ClearParameteres();
                strReturnXML = dsTempXML.GetXml();
                strReturnXML = ReplaceString(strReturnXML);
                returnString.Append(strReturnXML);
                returnString.Append("</UMGENINFO>");
                returnString.Append("</UMGENINFOS>");
                returnString.Append("</INPUTXML>");
                return returnString.ToString();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        /// <summary>
        /// Gets a  Additional Iinterest against an vehicle in application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Add Int Ids  as a carat separated string . if no Additional Interest exist then it returns -1</returns>
        public static string GetAdditionalInterestIDs(int customerID, int appID, int appVersionID, string strVehicleID)
        {
            string strStoredProc = "Proc_GetAddInterestIDs";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            objWrapper.AddParameter("@VEHICLE_ID", int.Parse(strVehicleID));
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strAddInID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strAddInID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strAddInID = strAddInID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strAddInID;
        }

        /// <summary>
        /// Gets a  APP_MVR_IDs against a Driver  in application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>APP_MVR_IDs as a carat separated string . if no APP_MVR_IDs exist then it returns -1</returns>
        public static string GetAppMVRIDs(int customerID, int appID, int appVersionID, string strDriverID)
        {
            string strStoredProc = "Proc_GetAppMVRIDs";
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            objWrapper.AddParameter("@DRIVER_ID", strDriverID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strAPPMVRIDs = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strAPPMVRIDs = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strAPPMVRIDs = strAPPMVRIDs + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strAPPMVRIDs;
        }


        /// <summary>
        /// Gets a  Location Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Location Ids  as a carat separated string . if no location exist then it returns -1</returns>
        public static string GetLocationIDs(int customerID, int appID, int appVersionID)
        {
            string strStoredProc = "Proc_GetLocationIDs";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strLocationID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strLocationID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strLocationID = strLocationID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strLocationID;
        }


        /// <summary>
        /// Gets a  Vehicle Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Vehicle Ids  as a carat separated string . if no vehicle exist then it returns -1</returns>
        public static string GetRV_VehicleIDs(int customerID, int appID, int appVersionID)
        {
            string strStoredProc = "Proc_GetRV_VehicleIDs";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strRV_VehicleID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strRV_VehicleID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strRV_VehicleID = strRV_VehicleID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strRV_VehicleID;
        }

        /// <summary>
        /// Gets a  Location Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Location Ids  as a carat separated string . if no location exist then it returns -1</returns>
        public static string GetDwellingIDs(int customerID, int appID, int appVersionID)
        {
            string strStoredProc = "Proc_GetDwellingIDs";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strDwellingID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strDwellingID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strDwellingID = strDwellingID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strDwellingID;
        }
        /// <summary>
        /// Gets a  Boat with Homeowners Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Boat Ids  </returns>
        public static string GetBoatWithHomeowner(int customerID, int appID, int appVersionID, string CalledFrom)
        {
            string strStoredProc = "Proc_GetBoatwithHomeowners";
            string strBoatHome = "";
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objWrapper.AddParameter("@APP_ID", appID);
                objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                objWrapper.AddParameter("@CALLEDFROM", CalledFrom);
                DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    strBoatHome = ds.Tables[0].Rows[0]["BOAT_WITH_HOMEOWNER"].ToString();
                }
                return strBoatHome;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a  Fuel Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Fuel Ids  as a carat separated string . if no fuel id exist then it returns -1</returns>
        public static string GetFuelIDs(int customerID, int appID, int appVersionID)
        {
            string strStoredProc = "Proc_FuelIDs";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strFuelID = "-1";
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    int intCount = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < intCount; i++)
                    {
                        if (i == 0)
                        {
                            strFuelID = ds.Tables[0].Rows[i][0].ToString();
                        }
                        else
                        {
                            strFuelID = strFuelID + '^' + ds.Tables[0].Rows[i][0].ToString();
                        }
                    }
                }
            }
            return strFuelID;
        }

        #endregion

        public void ActivateDeactivate(ClsGeneralInfo objGeneralInfo, string strStatus)
        {
            string strStoredProc = "Proc_ActivateDeactivateApplication";
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@APP_ID", objGeneralInfo.APP_ID);
                objWrapper.AddParameter("@APP_VERSION_ID", objGeneralInfo.APP_VERSION_ID);
                objWrapper.AddParameter("@IS_ACTIVE", strStatus);
                SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel("application/aspx/HomeOwners/AddOtherLocations.aspx.resx");
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.APP_ID = objGeneralInfo.APP_ID;
                    objTransactionInfo.APP_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objGeneralInfo.MODIFIED_BY;
                    if (strStatus.ToUpper() == "Y")
                    {
                        switch (BL_LANG_CULTURE)
                        {
                            case "pt-BR":
                                objTransactionInfo.TRANS_DESC = "Aplicativo é ativado";
                                break;
                            case "en-US":
                            default:
                                objTransactionInfo.TRANS_DESC = "Application is Activated";
                                break;
                        }
                    }
                    else
                    {
                        switch (BL_LANG_CULTURE)
                        {
                            case "pt-BR":
                                objTransactionInfo.TRANS_DESC = "Aplicativo está Desactivado";
                                break;
                            case "en-US":
                            default:
                                objTransactionInfo.TRANS_DESC = "Application is Deactivated";
                                break;
                        }
                    }
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                }
                objWrapper.ClearParameteres();
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objWrapper != null) objWrapper.Dispose();
            }
        }

        /// <summary>
        /// Activate Deactivate Policy Page
        /// </summary>
        /// <param name="objGeneralInfo"></param>
        /// <param name="strStatus">Y or N</param>
        /// Added by Charles on 7-April-10 for Policy Page
        public void ActivateDeactivatePolicy(ClsPolicyInfo objGeneralInfo, string strStatus)
        {
            string strStoredProc = "Proc_ActivateDeactivatePolicy";
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", objGeneralInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objGeneralInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objGeneralInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@IS_ACTIVE", strStatus);
                SqlParameter paramRetVal = (SqlParameter)objWrapper.AddParameter("@RET_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objGeneralInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objGeneralInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objGeneralInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objGeneralInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objGeneralInfo.MODIFIED_BY;
                    if (strStatus.ToUpper() == "Y")
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 133;
                        //objTransactionInfo.TRANS_DESC = "Application is Activated";                              
                    }
                    else
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 134;
                        //objTransactionInfo.TRANS_DESC = "Application is Deactivated";                           
                    }
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                }
                objWrapper.ClearParameteres();
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objWrapper != null) objWrapper.Dispose();
            }
        }


        public static string GetVehicleIDs(int customerID, int appID, int appVersionID)
        {
            return GetVehicleIDs(customerID, appID, appVersionID, null);
        }

        /// <summary>
        /// Gets a  Vehicle Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Vehicle Ids  as a carat separated string . if no vehicles exist then it returns -1</returns>
        public static string GetVehicleIDs(int customerID, int appID, int appVersionID, DataWrapper objWrapper)
        {
            string strStoredProc = "Proc_GetVehicleIDs";

            if (objWrapper == null)
                objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strVehicleID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strVehicleID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strVehicleID = strVehicleID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strVehicleID;
        }
        ///Added by praveen for motorcycle policy 
        /// <summary> 
        /// Gets a  Vehicle Ids against an policy
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Vehicle Ids  as a carat separated string . if no vehicles exist then it returns -1</returns>
        public static string GetVehicleIDsPolicy(int customerID, int policyID, int policyVersionID)
        {
            return GetVehicleIDsPolicy(customerID, policyID, policyVersionID, null);
        }
        public static string GetVehicleIDsPolicy(int customerID, int policyID, int policyVersionID, DataWrapper objWrapper)
        {
            string strStoredProc = "Proc_GetVehicleIDs_Policy";

            if (objWrapper == null)
                objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POL_ID", policyID);
            objWrapper.AddParameter("@POL_VERSION_ID", policyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strVehicleID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strVehicleID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strVehicleID = strVehicleID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strVehicleID;
        }

        /// <summary>
        /// Gets MVR Ids against one application for policy rule implementation
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>MVR Ids as carat separated string </returns>
        private string GetRuleMVRIDs(int customerID, int AppID, int AppVersionID)
        {
            string strStoredProc = "Proc_GetMVRIDs_App";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APPID", AppID);
            objWrapper.AddParameter("@APPVERSIONID", AppVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            int intCount = ds.Tables[0].Rows.Count;
            string strMVRIds = "-1";
            for (int i = 0; i < intCount; i++)
            {
                if (i == 0)
                {
                    strMVRIds = ds.Tables[0].Rows[i][0].ToString();
                }
                else
                {
                    strMVRIds = strMVRIds + '^' + ds.Tables[0].Rows[i][0].ToString();
                }
            }
            return strMVRIds;
        }
        // Motorcycle 
        /// <summary>
        /// Returns the Dropdown result sets for the Gen Info screen
        /// </summary>
        /// <param name="intLobID"></param>
        /// <param name="strAgencyCode"></param>
        /// <returns></returns>
        public static DataSet GetDropDowns(string strAgencyCode)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            objDataWrapper.AddParameter("@AGENCY_CODE", strAgencyCode);

            DataSet dsLookup = objDataWrapper.ExecuteDataSet("Proc_GetApplicationDDL");

            return dsLookup;
        }

        /// <summary>
        /// Returns the LOB specific result sets for an LOB
        /// </summary>
        /// <param name="intLobID"></param>
        /// <returns></returns>
        public static DataSet GetLOBInfo(int intLobID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            objDataWrapper.AddParameter("@LOB_ID", intLobID);

            DataSet dsLookup = objDataWrapper.ExecuteDataSet("Proc_GetLOBInfo");

            return dsLookup;
        }


        #region Copy Application To Policy
        // Added by ashwani 
        public int CopyAppToPolicy(int intCustomerID, int intAppID, int intAppVersionID, int intUserID, string strAppLobID, string strCalled)
        {
            int returnResult = 0;
            string strSPName = "";
            string strLOBDesc = "";
            try
            {
                if (strAppLobID != "0")
                {
                    // switch case on the basis of the lob
                    switch (strAppLobID)
                    {
                        case LOB_HOME:
                            strSPName = "Proc_ConvertApplicationToPolicy_HO";
                            strLOBDesc = "Homeowner";
                            break;
                        case LOB_PRIVATE_PASSENGER:
                            strSPName = "Proc_ConvertApplicationToPolicy_PPA";
                            strLOBDesc = "Automobile";
                            break;
                        case LOB_MOTORCYCLE:
                            strSPName = "Proc_ConvertApplicationToPolicy_PPA";
                            strLOBDesc = "Motorcycle";
                            break;
                        case LOB_WATERCRAFT:
                            strSPName = "Proc_ConvertApplicationToPolicy_Watercraft";
                            strLOBDesc = "Watercraft";
                            break;
                        case LOB_RENTAL_DWELLING:
                            strSPName = "Proc_ConvertApplicationToPolicy_HO";
                            strLOBDesc = "Rental ";
                            break;
                        case LOB_UMBRELLA:
                            strSPName = "Proc_ConvertApplicationToPolicy_UMB";
                            strLOBDesc = "Umbrella";
                            break;
                        case LOB_GENERAL_LIABILITY:
                            strSPName = "Proc_ConvertApplicationToPolicy_GEN";
                            strLOBDesc = "General Liability";
                            break;
                        case LOB_AVIATION:
                            strSPName = "Proc_ConvertApplicationToPolicy_AVIATION";
                            strLOBDesc = "Aviation";
                            break;

                        default:
                            break;
                    }
                }

                //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,);
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
                objDataWrapper.AddParameter("@APP_ID", intAppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);
                objDataWrapper.AddParameter("@CREATED_BY", intUserID);
                if (strAppLobID == LOB_RENTAL_DWELLING)
                {
                    objDataWrapper.AddParameter("@PARAM1", 6);  //For Rental Dwelling
                }
                else if (strAppLobID == LOB_HOME)
                {
                    objDataWrapper.AddParameter("@PARAM1", 1); //For HomeOwners
                }
                objDataWrapper.AddParameter("@CALLED_FROM", strCalled);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RESULT", SqlDbType.Int, ParameterDirection.Output);
                try
                {
                    if (this.boolTransactionRequired)
                    {
                        objDataWrapper.ExecuteNonQuery(strSPName);
                        returnResult = int.Parse(objSqlParameter.Value.ToString());
                        if (returnResult > 0)
                        {
                            objDataWrapper.ClearParameteres();
                            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                            objTransactionInfo.TRANS_TYPE_ID = 1;
                            objTransactionInfo.APP_ID = intAppID;
                            objTransactionInfo.APP_VERSION_ID = intAppVersionID;
                            objTransactionInfo.POLICY_ID = returnResult;
                            objTransactionInfo.POLICY_VER_TRACKING_ID = 1;
                            objTransactionInfo.CLIENT_ID = intCustomerID;
                            objTransactionInfo.RECORDED_BY = intUserID;
                            //objTransactionInfo.TRANS_DESC		=	"Application has been converted to Policy ";
                            objTransactionInfo.TRANS_DESC = strLOBDesc + " " + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1568", "");//" Application has been converted to Policy ";
                            //objTransactionInfo.CHANGE_XML		=	"";
                            objDataWrapper.ExecuteNonQuery(objTransactionInfo);
                        }
                    }
                    else //if no transaction required
                    {
                        returnResult = objDataWrapper.ExecuteNonQuery(strSPName);
                    }
                    objDataWrapper.ClearParameteres();
                    //Calling Assign Underwriter function
                    int retVal = 0;
                    objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
                    objDataWrapper.AddParameter("@APP_ID", intAppID);
                    objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);
                    objDataWrapper.AddParameter("@LOB_ID", strAppLobID);
                    //objDataWrapper.AddParameter("@SYSTEM_ID",SystemID);
                    objDataWrapper.AddParameter("@CREATED_BY", intUserID);
                    objDataWrapper.AddParameter("@CALLED_FROM", strCalled);
                    SqlParameter objSelectedUnder = (SqlParameter)objDataWrapper.AddParameter("@SELECTED_UNDERWRITER", SqlDbType.Int, ParameterDirection.Output);
                    SqlParameter objSubjectLine = (SqlParameter)objDataWrapper.AddParameter("@SUBJECT_LINE", "", SqlDbType.VarChar, ParameterDirection.Output, 100);
                    //get Return Value : By Praveen Kasana on 03 Feb 2009
                    SqlParameter objSqlParameterRetVal = (SqlParameter)objDataWrapper.AddParameter("@RESULT", SqlDbType.Int, ParameterDirection.Output);
                    objDataWrapper.ExecuteNonQuery("Proc_AssignUnderwriterToCustomer");
                    //Added By Praveen Kasana on 03 Feb 2009
                    string diarySubjectLine = "";
                    if (objSqlParameterRetVal.Value != DBNull.Value)
                        retVal = int.Parse(objSqlParameterRetVal.Value.ToString());
                    if (retVal == -10) //If Has underwriter 
                    {
                        diarySubjectLine = "New Application Submitted ";
                        if (strCalled == "ANYWAY")
                            diarySubjectLine = diarySubjectLine + " Type - Submit Anyway";
                        else
                        {
                            string strRefStatus = "";//Load and Parse Rule XML;
                            strRefStatus = FetchRuleReferedStatus(intCustomerID, intAppID, intAppVersionID, objDataWrapper);
                            if (strRefStatus == "0")
                                diarySubjectLine = diarySubjectLine + " Type - Refer to Underwriter";
                            else
                                diarySubjectLine = diarySubjectLine + " Type - Meets Requirements";
                        }
                    }




                    ///commented by Anurag Verma on 14/03/2007 for new diary changes
                    //if(objSelectedUnder!=null && objSelectedUnder.Value!=System.DBNull.Value && int.Parse(objSelectedUnder.Value.ToString())>0 && objSubjectLine!=null && objSubjectLine.Value.ToString()!="")
                    //{
                    //AddDiaryEntry(intCustomerID,intAppID, intAppVersionID, returnResult, 1, int.Parse(objSelectedUnder.Value.ToString()),intUserID,objSubjectLine.Value.ToString());
                    //Modify 	objSubjectLine.Value.ToString() to diarySubjectLine : Added By Praveen Kasana on 03 Feb 2009
                    AddDiaryEntry(intCustomerID, intAppID, intAppVersionID, returnResult, 1, intUserID, diarySubjectLine, strAppLobID);
                    //}
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.NO);
                }
                catch (Exception ex)
                {
                    objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                    throw (ex);
                }
            }
            finally
            { }
            return returnResult;
        }
        # endregion
        #region APP ISACTIVE STATUS
        public static void CheckIsactiveApplication(int intCustomerID, int intAppID, int intAppVersionID, out string isActive, out string strMessage)
        {
            string strProcedure = "Proc_CheckApplicationIsActive";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
                objDataWrapper.AddParameter("@APP_ID", intAppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);
                SqlParameter objSaqlParam = (SqlParameter)objDataWrapper.AddParameter("@IS_ACTIVE", SqlDbType.VarChar, ParameterDirection.Output);
                objSaqlParam.Size = 2;
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                if (objSaqlParam.Value.ToString().ToUpper().Equals("N"))
                {
                    strMessage = "Application is deactive, Please activate the application.";
                    isActive = objSaqlParam.Value.ToString();
                }
                else
                {
                    strMessage = "";
                    isActive = objSaqlParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
                objDataSet.Dispose();
            }
        }
        #endregion


        /// <summary>
        /// Reject the Application and Policy  
        /// </summary>
        /// <param name="intCustomerID">Customer ID</param>
        /// <param name="intPolicyID">Policy ID</param>
        /// <param name="intPolicyVersionID">Policy Version ID</param>
        /// <returns></returns>
        // Added by Pradeep Kushwaha on 16-June-2010 for Reject App/Pol Implementation
        public int RejectAppPol(ClsPolicyInfo objPolicyInfo, String CalledFrom)
        {
            string strProcedure = "Proc_RejectAppPol";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                int RetVal = 0;
                if (this.boolTransactionRequired)
                {
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objPolicyInfo.CUSTOMER_ID);
                    objDataWrapper.AddParameter("@POLICY_ID", objPolicyInfo.POLICY_ID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicyInfo.POLICY_VERSION_ID);
                    objDataWrapper.AddParameter("@CALLEDFROM", CalledFrom);

                    RetVal = objDataWrapper.ExecuteNonQuery(strProcedure);

                    if (RetVal > 0)
                    {
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                        objPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");

                        string strTranXML = objBuilder.GetTransactionLogXML(objPolicyInfo);
                        if (CalledFrom == "APP")
                            objTransactionInfo.TRANS_TYPE_ID = 242;
                        else if (CalledFrom == "POL")
                            objTransactionInfo.TRANS_TYPE_ID = 243;

                        objTransactionInfo.POLICY_ID = objPolicyInfo.POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objPolicyInfo.POLICY_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objPolicyInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objPolicyInfo.CREATED_BY;
                        objTransactionInfo.TRANS_DESC = "";

                        objTransactionInfo.CHANGE_XML = strTranXML;

                        objDataWrapper.ExecuteNonQuery(objTransactionInfo);


                        Int32 ListTypeID = (int)ClsDiary.enumDiaryType.POLICY_REJECTED;
                        Int32 Module_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;

                        String SubLine = String.Empty;
                        String Notes = String.Empty;

                        SubLine = FetchGeneralMessage("1161", "");
                        Notes = FetchGeneralMessage("1162", "") + "(" + objPolicyInfo.POLICY_NUMBER.ToString() + " ) " + FetchGeneralMessage("1163", "");


                        AddDiaryEntry(objPolicyInfo.CUSTOMER_ID, objPolicyInfo.APP_ID, objPolicyInfo.APP_VERSION_ID, objPolicyInfo.POLICY_ID, objPolicyInfo.POLICY_VERSION_ID, objPolicyInfo.CREATED_BY, Module_ID, ListTypeID, SubLine, Notes, objPolicyInfo.POLICY_LOB);
                    }
                }
                else
                {
                    RetVal = objDataWrapper.ExecuteNonQuery(strProcedure);
                }

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return RetVal;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        //Added By Pradeep Kushwaha on 25-06-2010 for reject policy 
        /// <summary>
        /// Add diary entry 
        /// </summary>
        /// <param name="CustomerId">Customer Id</param>
        /// <param name="AppId">Application id</param>
        /// <param name="AppVersionId">Application Version Id</param>
        /// <param name="PolicyId">Policy id</param>
        /// <param name="PolicyVersionId">Policy Version Id</param>
        /// <param name="CreatedBy">Created User Id </param>
        /// <param name="ModuleID">Module Id</param>
        /// <param name="ListTypeID">List Type Id</param>
        /// <param name="SubjectLine">Subject Line Message</param>
        /// <param name="Notes">Detail Notes</param>
        /// <param name="Pol_LOB"> Policy Line of business Id</param>
        public void AddDiaryEntry(Int32 CustomerId, Int32 AppId, Int32 AppVersionId, Int32 PolicyId, Int32 PolicyVersionId, Int32 CreatedBy, Int32 ModuleID, Int32 ListTypeID, String SubjectLine, String Notes, String Pol_LOB)
        {
            try
            {
                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                objModel.LISTTYPEID = ListTypeID;
                objModel.POLICY_ID = PolicyId;
                objModel.POLICY_VERSION_ID = PolicyVersionId;
                objModel.CUSTOMER_ID = CustomerId;
                objModel.APP_ID = AppId;
                objModel.LOB_ID = int.Parse(Pol_LOB);
                objModel.APP_VERSION_ID = AppVersionId;
                objModel.FROMUSERID = objModel.CREATED_BY = CreatedBy;
                objModel.CREATED_DATETIME = DateTime.Now;
                objModel.RECDATE = DateTime.Now;
                objModel.LISTOPEN = "Y";
                objModel.SUBJECTLINE = SubjectLine;
                objModel.NOTE = Notes;
                objModel.MODULE_ID = ModuleID;

                objClsDiary.DiaryEntryfromSetup(objModel);

            }
            catch
            {
            }
        }

        /// <summary>
        /// Check to see if Policy is Active or not
        /// </summary>
        /// <param name="intCustomerID">Customer ID</param>
        /// <param name="intPolicyID">Policy ID</param>
        /// <param name="intPolicyVersionID">Policy Version ID</param>
        /// <returns>IS ACTIVE</returns>
        /// Added by Charles on 17-Mar-10 for Policy Page Implementation
        public static string CheckIsActivePolicy(int intCustomerID, int intPolicyID, int intPolicyVersionID)
        {
            string strProcedure = "Proc_CheckPolicyIsActive";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", intPolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", intPolicyVersionID);
                SqlParameter objSaqlParam = (SqlParameter)objDataWrapper.AddParameter("@IS_ACTIVE", SqlDbType.VarChar, ParameterDirection.Output);
                objSaqlParam.Size = 2;

                int RetVal = objDataWrapper.ExecuteNonQuery(strProcedure);

                return objSaqlParam.Value.ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        /// <summary>
        /// Assign Underwriter when Policy is added
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intAppID"></param>
        /// <param name="intAppVersionID"></param>
        /// <param name="strAppLobID"></param>
        /// <param name="intUserID"></param>
        /// <param name="strCalled"></param>
        /// <param name="objDataWrapper"></param>
        /// <returns></returns>
        /// Added by Charles on 5-Mar-10 for Policy Page Implementation
        public int CheckToAssignedUnderWriter(int intCustomerID, int intAppID, int intAppVersionID, string strAppLobID, int intUserID, string strCalled, DataWrapper objDataWrapper)
        {
            try
            {
                //Calling Assign Underwriter function
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
                objDataWrapper.AddParameter("@APP_ID", intAppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);
                objDataWrapper.AddParameter("@LOB_ID", strAppLobID);
                objDataWrapper.AddParameter("@CREATED_BY", intUserID);
                objDataWrapper.AddParameter("@CALLED_FROM", strCalled);
                SqlParameter objSelectedUnder = (SqlParameter)objDataWrapper.AddParameter("@SELECTED_UNDERWRITER", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSubjectLine = (SqlParameter)objDataWrapper.AddParameter("@SUBJECT_LINE", "", SqlDbType.VarChar, ParameterDirection.Output, 100);
                int RetVal = objDataWrapper.ExecuteNonQuery("Proc_AssignUnderwriterToCustomer");
                int retUnderWriter = 0;
                if (objSelectedUnder.Value != DBNull.Value)
                    retUnderWriter = int.Parse(objSelectedUnder.Value.ToString());

                objDataWrapper.ClearParameteres();

                if (retUnderWriter == 0)
                {
                    return -2;
                }
                else
                    return 1;
            }
            catch
            {
                return -2;
            }
        }

        public int CheckToAssignedUnderWriter(int intCustomerID, int intAppID, int intAppVersionID, string strAppLobID, int intUserID, string strCalled)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                //Calling Assign Underwriter function
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", intCustomerID);
                objDataWrapper.AddParameter("@APP_ID", intAppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", intAppVersionID);
                objDataWrapper.AddParameter("@LOB_ID", strAppLobID);
                objDataWrapper.AddParameter("@CREATED_BY", intUserID);
                objDataWrapper.AddParameter("@CALLED_FROM", strCalled);
                SqlParameter objSelectedUnder = (SqlParameter)objDataWrapper.AddParameter("@SELECTED_UNDERWRITER", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSubjectLine = (SqlParameter)objDataWrapper.AddParameter("@SUBJECT_LINE", "", SqlDbType.VarChar, ParameterDirection.Output, 100);
                //SqlParameter objSubjectLine  = (SqlParameter) objDataWrapper.AddParameter("@SUBJECT_LINE",SqlDbType.VarChar,ParameterDirection.Output,100);
                int RetVal = objDataWrapper.ExecuteNonQuery("Proc_AssignUnderwriterToCustomer");
                int retUnderWriter = 0;
                if (objSelectedUnder.Value != DBNull.Value)
                    retUnderWriter = int.Parse(objSelectedUnder.Value.ToString());

                objDataWrapper.ClearParameteres();
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                if (retUnderWriter == 0)
                {
                    return -2;
                }
                else
                    return 1;
            }
            catch
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.NO);
                return -2;
            }
        }
        private void AddDiaryEntry(int CustomerId, int AppId, int AppVersionId, int PolicyId, int PolicyVersionId, int CreatedBy, string SubjectLine, string LOBID)
        {
            try
            {
                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                //Commented by Anurag Verma on 20/03/2007 as these properties are removed
                //objModel.POLICYCLIENTID = CustomerId;
                //objModel.LISTTYPEID =  (int)ClsDiary.enumDiaryType.NEW_BUSINESS_REQUESTS;
                objModel.LISTTYPEID = (int)ClsDiary.enumDiaryType.APPLICATION_SUBMITTED_TO_POLICY;
                objModel.POLICY_ID = PolicyId;
                objModel.POLICY_VERSION_ID = PolicyVersionId;
                objModel.CUSTOMER_ID = CustomerId;
                objModel.APP_ID = AppId;
                objModel.APP_VERSION_ID = AppVersionId;
                objModel.FROMUSERID = objModel.CREATED_BY = CreatedBy;
                objModel.CREATED_DATETIME = DateTime.Now;
                objModel.RECDATE = DateTime.Now;
                objModel.LISTOPEN = "Y";
                objModel.NOTE = objModel.SUBJECTLINE = SubjectLine;
                objModel.MODULE_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;
                objModel.LOB_ID = LOBID == "" ? 0 : int.Parse(LOBID);



                objClsDiary.DiaryEntryfromSetup(objModel);


                ///Commented by Anurag Verma on 12-03-2006 for checking new diary object
                #region OLD CODE BEFORE DIARY OBJECT---DO NOT DELETE
                //objModel.TOUSERID = ToUserId;				
                //				objModel.FOLLOWUPDATE = DateTime.Now;    
                //				objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                //				objModel.RECDATE = DateTime.Now;					
                //				objModel.STARTTIME = (System.DateTime)DateTime.Now;
                //				objModel.ENDTIME = (System.DateTime)DateTime.Now;							
                //objClsDiary.AddPolicyEntry(objModel); 
                #endregion


            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="AppId"></param>
        /// <param name="AppVersionId"></param>
        /// <param name="PolicyId"></param>
        /// <param name="PolicyVersionId"></param>
        /// <param name="ToUserId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="SubjectLine"></param>
        private void AddDiaryEntry(int CustomerId, int AppId, int AppVersionId, int PolicyId, int PolicyVersionId, int ToUserId, int CreatedBy, string SubjectLine)
        {
            try
            {
                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                //Commented by Anurag Verma on 20/03/2007 as these properties are removed
                //objModel.POLICYCLIENTID = CustomerId;
                objModel.LISTTYPEID = (int)ClsDiary.enumDiaryType.NEW_BUSINESS_REQUESTS;
                objModel.POLICY_ID = PolicyId;
                objModel.POLICY_VERSION_ID = PolicyVersionId;
                objModel.CUSTOMER_ID = CustomerId;
                objModel.APP_ID = AppId;
                objModel.APP_VERSION_ID = AppVersionId;
                objModel.FROMUSERID = objModel.CREATED_BY = CreatedBy;
                objModel.CREATED_DATETIME = DateTime.Now;
                objModel.RECDATE = DateTime.Now;
                objModel.LISTOPEN = "Y";
                objModel.NOTE = objModel.SUBJECTLINE = SubjectLine;
                objModel.MODULE_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;


                objClsDiary.DiaryEntryfromSetup(objModel);


                ///Commented by Anurag Verma on 12-03-2006 for checking new diary object
                #region OLD CODE BEFORE DIARY OBJECT---DO NOT DELETE
                //objModel.TOUSERID = ToUserId;				
                //				objModel.FOLLOWUPDATE = DateTime.Now;    
                //				objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                //				objModel.RECDATE = DateTime.Now;					
                //				objModel.STARTTIME = (System.DateTime)DateTime.Now;
                //				objModel.ENDTIME = (System.DateTime)DateTime.Now;							
                //objClsDiary.AddPolicyEntry(objModel); 
                #endregion


            }
            catch
            {
            }
        }

        #region Method to check for start of NBS process for agencies that have defined their LOB for the state in the maintenance section
        public string CheckForStartNBSProcess(int iCustomerID, int iAppID, int iAppVersionID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", iCustomerID);
                objDataWrapper.AddParameter("@APP_ID", iAppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", iAppVersionID);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RETURN_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);
                objDataWrapper.ExecuteNonQuery("Proc_CheckNBSProcessStartForAgency");
                if (objSqlParameter != null && objSqlParameter.Value.ToString() != "")
                {
                    int returnValue = int.Parse(objSqlParameter.Value.ToString());
                    if (returnValue == 1) //Current agency has set up LOB & State for NBS, lets call the process
                        return "Y";
                }
                return "N";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper = null;
            }
        }
        #endregion

        #region Policy Functions

        /// <summary>
        /// Update Policy Status
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="PolicyStatus"></param>
        /// <returns></returns>
        /// Added by Charles on 19-Mar-10 for Policy Page Implementation

        public static int SetPolicyStatus(int CustomerID, int PolicyID, int PolicyVersionID, string PolicyStatus, int userId, string strAppLobID, string sNewAppPolNumber, DataWrapper objWrapper)
        {
            DateTime dApp_SubmittedDate = DateTime.Now;
            //DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.ON);

            string strStoredProc = "Proc_UpdateApplicationStatus";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@POLICY_STATUS", PolicyStatus);
                objWrapper.AddParameter("@POLICY_NUMBER", sNewAppPolNumber);
                objWrapper.AddParameter("@APP_SUBMITTED_DATE", dApp_SubmittedDate);

                int returnResult = 0;

                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                string strTranXML = "<LabelFieldMapping></LabelFieldMapping>";//<Map label=\"Status\" field=\"POLICY_STATUS\" OldValue=\"" + "APPLICATION" + "\" NewValue=\"" + "SUSPENDED" + "\" /></LabelFieldMapping>";
                objTransactionInfo.TRANS_TYPE_ID = 135;
                objTransactionInfo.POLICY_ID = PolicyID;
                objTransactionInfo.POLICY_VER_TRACKING_ID = PolicyVersionID;
                objTransactionInfo.CLIENT_ID = CustomerID;
                objTransactionInfo.RECORDED_BY = userId;
                objTransactionInfo.TRANS_DESC = "";//"Application converted to policy";
                objTransactionInfo.CHANGE_XML = strTranXML;
                returnResult = objWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                objWrapper.ClearParameteres();

                string diarySubjectLine = "New Application Submitted ";

                string strRefStatus = "";//Load and Parse Rule XML;

                ClsGeneralInformation objClsGeneralInformation = new ClsGeneralInformation();

                strRefStatus = objClsGeneralInformation.FetchRuleReferedStatus(CustomerID, PolicyID, PolicyVersionID, objWrapper);

                #region M A K E     D I A R Y   E N T R Y   C O M P L E T E
                //Adde by lalit,dec 22 2010 for make diary  entry complete of application create
                Int32 intListTypeID = (int)ClsDiary.enumDiaryType.APPLICATION_CREATION;
                Int32 intModule_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;
                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                //string CompleteSublectLine = FetchGeneralMessage("1317", "") + " " + sNewAppPolNumber + FetchGeneralMessage("1319", "");

                objClsDiary.MarkCompleteDiaryEntry(CustomerID, PolicyID, PolicyVersionID, intListTypeID, intModule_ID, objWrapper);


                //CompleteDiaryEntry
                #endregion

                if (strRefStatus == "0")
                    diarySubjectLine = diarySubjectLine + " Type - Refer to Underwriter";
                else
                    diarySubjectLine = diarySubjectLine + " Type - Meets Requirements";






                objClsGeneralInformation.AddDiaryEntry(CustomerID, PolicyID, PolicyVersionID, PolicyID, 1, userId, diarySubjectLine, strAppLobID);

                return 1;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        /// <summary>
        /// Return the xml of Policy
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public DataSet GetPolicyDataSet(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", BL_LANG_ID, SqlDbType.Int); //Added by Charles on 24-May-2010 for Multilingual Support 
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyInformation");
                //string retVal = "";

                //if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    retVal = dsTemp.Tables[0].Rows[0]["APPLICATIONNUMBER"].ToString();
                //}

                //return retVal;


                

                return dsTemp;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            {
              
            }
        }
        public DataSet GetClaimPolicyDataSet(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            return GetClaimPolicyDataSet(CustomerID, PolicyID, PolicyVersionID, 1);
        }
        public DataSet GetClaimPolicyDataSet(int CustomerID, int PolicyID, int PolicyVersionID, int LangId)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", LangId, SqlDbType.Int);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetClaimPolicyInformation");

                return dsTemp;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }
        public static int GetPolicyStatus(int customer_id, int pol_id, int pol_version)
        {
            string strStoredProc = "Proc_CheckForPolicyStatus";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
            objDataWrapper.AddParameter("@POL_ID", pol_id);
            objDataWrapper.AddParameter("@POL_VERSION_ID", pol_version);

            SqlParameter convert = (SqlParameter)objDataWrapper.AddParameter("@convertr", SqlDbType.Int, ParameterDirection.Output);

            objDataWrapper.ExecuteNonQuery(strStoredProc);

            return Convert.ToInt32(convert.Value);

        }
        public int UpdatePolicyBillTypeId(int Customer_Id, int Policy_Id, int Policy_version_id, string strNewBillTypeid, string strOldBillTypeId, int userId)
        {

            string strStoredProc = "Proc_UpdatePolicyBillType";
            DateTime RecordDate = DateTime.Now;
            string strTranXML;


            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", Customer_Id);
                objDataWrapper.AddParameter("@POLICY_ID", Policy_Id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_version_id);
                objDataWrapper.AddParameter("@BILL_TYPE_ID", strNewBillTypeid);

                int returnResult = 0;

                if (TransactionRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    strTranXML = "<LabelFieldMapping><Map label=\"Bill Type\" field=\"BILL_TYPE_ID\" OldValue=\"" + strOldBillTypeId + "\" NewValue=\"" + strNewBillTypeid + "\" /></LabelFieldMapping>";
                    objTransactionInfo.TRANS_TYPE_ID = 2;
                    objTransactionInfo.POLICY_ID = Policy_Id;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = Policy_version_id;
                    objTransactionInfo.CLIENT_ID = Customer_Id;
                    objTransactionInfo.RECORDED_BY = userId;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1569", "");// "Policy Bill Type is modified";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }
        public int UpdatePolicy(ClsPolicyInfo objOldPolicyInfo, ClsPolicyInfo objPolicyInfo)
        {
            return UpdatePolicy(objOldPolicyInfo, objPolicyInfo, "");
        }
        public int UpdatePolicy(ClsPolicyInfo objOldPolicyInfo, ClsPolicyInfo objPolicyInfo, string policy_status)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_UpdatePolicyInfo";
            DateTime RecordDate = DateTime.Now;
            string strTranXML;


            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", objPolicyInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objPolicyInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicyInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@APP_TERMS", objPolicyInfo.APP_TERMS);

                if (objPolicyInfo.APP_INCEPTION_DATE != DateTime.MinValue)
                {
                    objDataWrapper.AddParameter("@APP_INCEPTION_DATE", objPolicyInfo.APP_INCEPTION_DATE);
                }

                objDataWrapper.AddParameter("@APP_EFFECTIVE_DATE", objPolicyInfo.APP_EFFECTIVE_DATE);
                objDataWrapper.AddParameter("@APP_EXPIRATION_DATE", objPolicyInfo.APP_EXPIRATION_DATE);

                objDataWrapper.AddParameter("@UNDERWRITER", objPolicyInfo.UNDERWRITER);

                objDataWrapper.AddParameter("@MODIFIED_BY", objPolicyInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objPolicyInfo.LAST_UPDATED_DATETIME);
                if (policy_status == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_REWRITE || policy_status == Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_SUSPENSE_REWRITE)
                    objDataWrapper.AddParameter("@IS_HOME_EMP", objPolicyInfo.IS_HOME_EMP);

                if (objPolicyInfo.YEAR_AT_CURR_RESI == Double.MinValue)
                    objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@YEAR_AT_CURR_RESI", objPolicyInfo.YEAR_AT_CURR_RESI);

                objDataWrapper.AddParameter("@YEARS_AT_PREV_ADD", objPolicyInfo.YEARS_AT_PREV_ADD);
                //Added by kuldeep to store div/dept/pc when coming from quick quote or update
                objDataWrapper.AddParameter("@DIV_ID", objPolicyInfo.DIV_ID);
                objDataWrapper.AddParameter("@DEPT_ID", objPolicyInfo.DEPT_ID);
                objDataWrapper.AddParameter("@PC_ID", objPolicyInfo.PC_ID);

                objDataWrapper.AddParameter("@BILL_TYPE_ID", objPolicyInfo.BILL_TYPE);
                objDataWrapper.AddParameter("@COMPLETE_APP", objPolicyInfo.COMPLETE_APP);
                objDataWrapper.AddParameter("@INSTALL_PLAN_ID", objPolicyInfo.INSTALL_PLAN_ID);
                objDataWrapper.AddParameter("@CHARGE_OFF_PRMIUM", objPolicyInfo.CHARGE_OFF_PRMIUM);
                objDataWrapper.AddParameter("@PROPRTY_INSP_CREDIT", DefaultValues.GetStringNull(objPolicyInfo.PROPRTY_INSP_CREDIT));
                if (objPolicyInfo.RECEIVED_PRMIUM == -1)
                    objDataWrapper.AddParameter("@RECEIVED_PRMIUM", System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@RECEIVED_PRMIUM", objPolicyInfo.RECEIVED_PRMIUM);


                if (objPolicyInfo.PROXY_SIGN_OBTAINED != int.MinValue) // proxy sign is set
                    objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", objPolicyInfo.PROXY_SIGN_OBTAINED);
                else
                    objDataWrapper.AddParameter("@PROXY_SIGN_OBTAINED", null);

                if (objPolicyInfo.POLICY_TYPE == "-1")
                    objPolicyInfo.POLICY_TYPE = "0";

                objDataWrapper.AddParameter("@POLICY_TYPE", DefaultValues.GetStringNull(objPolicyInfo.POLICY_TYPE.ToString()));
                objDataWrapper.AddParameter("@POLICY_SUBLOB", DefaultValues.GetStringNull(objPolicyInfo.POLICY_SUBLOB.ToString()));

                objDataWrapper.AddParameter("@PIC_OF_LOC", objPolicyInfo.PIC_OF_LOC);
                objDataWrapper.AddParameter("@DOWN_PAY_MODE", objPolicyInfo.DOWN_PAY_MODE);
                objDataWrapper.AddParameter("@NOT_RENEW", objPolicyInfo.NOT_RENEW);
                objDataWrapper.AddParameter("@NOT_RENEW_REASON", objPolicyInfo.NOT_RENEW_REASON);
                objDataWrapper.AddParameter("@REFER_UNDERWRITER", objPolicyInfo.REFER_UNDERWRITER);
                objDataWrapper.AddParameter("@REFERAL_INSTRUCTIONS", objPolicyInfo.REFERAL_INSTRUCTIONS);
                objDataWrapper.AddParameter("@REINS_SPECIAL_ACPT", objPolicyInfo.REINS_SPECIAL_ACPT);
                if (objPolicyInfo.STATE_ID != 0)
                    objDataWrapper.AddParameter("@STATE_ID", objPolicyInfo.STATE_ID);
                if (objPolicyInfo.AGENCY_ID != 0)
                    objDataWrapper.AddParameter("@AGENCY_ID", objPolicyInfo.AGENCY_ID);
                if (objPolicyInfo.CSR != 0)
                    objDataWrapper.AddParameter("@CSR", objPolicyInfo.CSR);
                if (objPolicyInfo.PRODUCER != 0)
                    objDataWrapper.AddParameter("@PRODUCER", objPolicyInfo.PRODUCER);

                //Added by Charles on 18-Mar-2010 for Policy Page Implementation
                objDataWrapper.AddParameter("@POLICY_CURRENCY", objPolicyInfo.POLICY_CURRENCY);
                objDataWrapper.AddParameter("@PAYOR", objPolicyInfo.PAYOR);
                objDataWrapper.AddParameter("@CO_INSURANCE", objPolicyInfo.CO_INSURANCE);
                objDataWrapper.AddParameter("@POLICY_LEVEL_COMISSION", objPolicyInfo.POLICY_LEVEL_COMISSION);
                objDataWrapper.AddParameter("@BILLTO", objPolicyInfo.BILLTO);
                objDataWrapper.AddParameter("@CONTACT_PERSON", objPolicyInfo.CONTACT_PERSON);
                //Added till here	

                //Added by Charles on 14-May-2010 for Policy Page Implementation
                objDataWrapper.AddParameter("@POLICY_LEVEL_COMM_APPLIES", objPolicyInfo.POLICY_LEVEL_COMM_APPLIES);
                objDataWrapper.AddParameter("@TRANSACTION_TYPE", objPolicyInfo.TRANSACTION_TYPE);
                objDataWrapper.AddParameter("@PREFERENCE_DAY", objPolicyInfo.PREFERENCE_DAY);
                objDataWrapper.AddParameter("@BROKER_REQUEST_NO", objPolicyInfo.BROKER_REQUEST_NO);
                objDataWrapper.AddParameter("@BROKER_COMM_FIRST_INSTM", objPolicyInfo.BROKER_COMM_FIRST_INSTM);
                objDataWrapper.AddParameter("@POLICY_DESCRIPTION", objPolicyInfo.POLICY_DESCRIPTION);
                //Added till here	

                //Added by Agniswar for Singapore Implementation
                objDataWrapper.AddParameter("@BILLING_CURRENCY", objPolicyInfo.BILLING_CURRENCY);
                objDataWrapper.AddParameter("@FUND_TYPE", objPolicyInfo.FUND_TYPE);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RESULT", SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;

                if (TransactionRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    if (objPolicyInfo.POLICY_STATUS.ToUpper() == "APPLICATION")
                    {
                        objPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");
                    }
                    else
                    {
                        objPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyInformation.aspx.resx");
                    }

                    strTranXML = objBuilder.GetTransactionLogXML(objOldPolicyInfo, objPolicyInfo);
                    if (objPolicyInfo.RECEIVED_PRMIUM == -1)
                    {
                        strTranXML = ClsCommon.RemoveNode(strTranXML, "LabelFieldMapping/Map[@field='RECEIVED_PRMIUM']");
                    }
                    if (strTranXML == "<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    else
                    {
                        if (objPolicyInfo.POLICY_STATUS.ToUpper() == "APPLICATION")
                        {
                            objTransactionInfo.TRANS_TYPE_ID = 127;
                        }
                        else
                        {
                            objTransactionInfo.TRANS_TYPE_ID = 125;
                        }

                        objTransactionInfo.APP_ID = objPolicyInfo.APP_ID;
                        objTransactionInfo.APP_VERSION_ID = objPolicyInfo.APP_VERSION_ID;
                        objTransactionInfo.POLICY_ID = objPolicyInfo.POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objPolicyInfo.POLICY_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objPolicyInfo.CUSTOMER_ID;
                        objTransactionInfo.RECORDED_BY = objPolicyInfo.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = "";//"Policy information is modified";
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                //For Home and rental, adjust coverages
                if (objPolicyInfo.POLICY_LOB == "1" || objPolicyInfo.POLICY_LOB == "6")
                {
                    //If policy type has changed, adjust coverages for each dwelling
                    if (objOldPolicyInfo.POLICY_TYPE != objPolicyInfo.POLICY_TYPE)
                    {
                        objDataWrapper.ClearParameteres();

                        objDataWrapper.AddParameter("@CUSTOMER_ID", objPolicyInfo.CUSTOMER_ID);
                        objDataWrapper.AddParameter("@POLICY_ID", objPolicyInfo.POLICY_ID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicyInfo.POLICY_VERSION_ID);
                        objDataWrapper.ExecuteNonQuery("Proc_ADJUST_DWELLING_COVERAGES_POLICY_NEW");

                        objDataWrapper.ClearParameteres();
                        ClsHomeCoverages objHomeCov;
                        if (objPolicyInfo.POLICY_LOB == "6")
                        {
                            objHomeCov = new ClsHomeCoverages("1");
                        }
                        else
                        {
                            objHomeCov = new ClsHomeCoverages();
                        }
                        if (objPolicyInfo.POLICY_LOB == "6")
                            objHomeCov.UpdateCoveragesByRulePolicy(objDataWrapper,
                                objPolicyInfo.CUSTOMER_ID,
                                objPolicyInfo.POLICY_ID,
                                objPolicyInfo.POLICY_VERSION_ID,
                                RuleType.ProductDependend);
                        else
                            objHomeCov.UpdateCoveragesByRulePolicy(objDataWrapper,
                                objPolicyInfo.CUSTOMER_ID,
                                objPolicyInfo.POLICY_ID,
                                objPolicyInfo.POLICY_VERSION_ID,
                                RuleType.AppDependent);
                        objHomeCov.UpdateCoveragesByRulePolicy(objDataWrapper,
                            objPolicyInfo.CUSTOMER_ID,
                            objPolicyInfo.POLICY_ID,
                            objPolicyInfo.POLICY_VERSION_ID, RuleType.RiskDependent);

                    }
                }

                // In case of Rewrite if there is a change in State Delete all coverage and
                // Rewrite coverage / endorsement info

                if (objPolicyInfo.STATE_ID != 0 && objPolicyInfo.STATE_ID != -1)
                {
                    if (objPolicyInfo.STATE_ID != objOldPolicyInfo.STATE_ID)
                    {
                        objDataWrapper.ClearParameteres();
                        objDataWrapper.AddParameter("@CUSTOMER_ID", objPolicyInfo.CUSTOMER_ID);
                        objDataWrapper.AddParameter("@POLICY_ID", objPolicyInfo.POLICY_ID);
                        objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicyInfo.POLICY_VERSION_ID);
                        objDataWrapper.AddParameter("@LOB_ID", objPolicyInfo.POLICY_LOB);
                        objDataWrapper.ExecuteNonQuery("Proc_DeleteAllCoverages");

                        objDataWrapper.ClearParameteres();

                        ClsCoverages objCoverage = null;
                        if (objPolicyInfo.POLICY_LOB == "1")
                        {
                            objCoverage = new ClsHomeCoverages();

                        }
                        else if (objPolicyInfo.POLICY_LOB == "6")
                        {
                            objCoverage = new ClsHomeCoverages("RENTAL");
                        }
                        else if (objPolicyInfo.POLICY_LOB == "2")
                        {
                            objCoverage = new ClsVehicleCoverages();
                        }
                        else if (objPolicyInfo.POLICY_LOB == "3")
                        {
                            objCoverage = new ClsVehicleCoverages("MOTOR");
                        }
                        else if (objPolicyInfo.POLICY_LOB == "4")
                        {
                            objCoverage = new ClsWatercraftCoverages();
                        }
                        if (objCoverage != null)
                        {
                            DataTable dt = null;
                            dt = objCoverage.GetRisksForLobPolicy(objPolicyInfo.CUSTOMER_ID, objPolicyInfo.POLICY_ID,
                                objPolicyInfo.POLICY_VERSION_ID, -1);

                            if (dt != null)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    objDataWrapper.ClearParameteres();
                                    //Invalidate initialisation
                                    objCoverage.InvalidateInitialisation();

                                    objDataWrapper.ClearParameteres();
                                    objCoverage.SaveDefaultCoveragesPolicy(objDataWrapper, objPolicyInfo.CUSTOMER_ID,
                                        objPolicyInfo.POLICY_ID, objPolicyInfo.POLICY_VERSION_ID, Convert.ToInt32(dr["RISK_ID"]));
                                }
                            }
                        }
                    }
                }
                // commeted by Pravesh on 17 april 2008
                //				//Update coverages according to Effective date
                //				if ( objPolicyInfo.APP_EFFECTIVE_DATE != objOldPolicyInfo.APP_EFFECTIVE_DATE)
                //				{
                //					objDataWrapper.ClearParameteres();
                //					
                //					objDataWrapper.AddParameter("@CUSTOMER_ID",objPolicyInfo.CUSTOMER_ID);
                //					objDataWrapper.AddParameter("@POLICY_ID",objPolicyInfo.POLICY_ID);
                //					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objPolicyInfo.POLICY_VERSION_ID);
                //
                //					objDataWrapper.ExecuteNonQuery("Proc_ADJUST_POL_COVERAGES");
                //				}


                //Adjust coverages based on rules
                if (objPolicyInfo.POLICY_LOB == LOB_WATERCRAFT || objPolicyInfo.POLICY_LOB == LOB_HOME)
                {
                    if (objPolicyInfo.APP_EFFECTIVE_DATE != objOldPolicyInfo.APP_EFFECTIVE_DATE)
                    {
                        objDataWrapper.ClearParameteres();
                        ClsWatercraftCoverages objWatCov = new ClsWatercraftCoverages();
                        objWatCov.UpdateCoveragesByRulePolicy(objDataWrapper, objPolicyInfo.CUSTOMER_ID, objPolicyInfo.POLICY_ID, objPolicyInfo.POLICY_VERSION_ID, RuleType.AppDependent);
                    }
                }

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }

        public int IssuePolicy(int CustomerID, int PolicyID, int PolicyVersionID, int UserID, int AppID, int AppVersionId)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_UpdatePolicyStatus";
            DateTime RecordDate = DateTime.Now;
            //string strTranXML;


            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RESULT", SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;

                if (TransactionRequired)
                {
                    /*Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
										
                    objTransactionInfo.TRANS_TYPE_ID	=	2;
                    objTransactionInfo.APP_ID = ;
                    objTransactionInfo.APP_VERSION_ID = objPolicyInfo.APP_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objPolicyInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY		=	UserID;
                    objTransactionInfo.TRANS_DESC		=	"Policy Status Updated to Normal";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    */
                    //returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                objDataWrapper.ClearParameteres();
                Cms.BusinessLayer.BlAccount.ClsPolicyPremium objPremium = new Cms.BusinessLayer.BlAccount.ClsPolicyPremium();
                objPremium.PostPolicyPremium(PolicyID, PolicyVersionID, CustomerID);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }

        public DataSet GetPolicyDetailsForAttachment(int CustomerID, int AppID)
        {
            string strStoredProc = "Proc_GetPolicyDetailsForAttachment";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@APP_ID", AppID);
                //objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }
        public DataSet GetPolicyDetails(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            return GetPolicyDetails(CustomerID, 0, 0, PolicyID, PolicyVersionID);
        }
        public DataSet GetPolicyDetails(int CustomerID, int AppID, int AppVersionID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetPolicyDetails";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);

                //objDataWrapper.AddParameter("@APP_ID",AppID);
                //objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID);
                if (AppID != 0 && AppVersionID != 0)
                {
                    objDataWrapper.AddParameter("@APP_ID", AppID);
                    objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID);
                }
                if (PolicyID != 0 && PolicyVersionID != 0)
                {
                    objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                }
                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }

        public DataSet GetPolicyLOBID(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetPolicyLOBID";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }

        public string GetStatusOfPolicy(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            DataSet dsTemp;
            try
            {

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POL_ID", PolicyID);
                objWrapper.AddParameter("@POL_VERSION_ID", PolicyVersionID);
                dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyStatus");
                objWrapper.ClearParameteres();
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (dsTemp.Tables[0].Rows.Count > 0)
                    return dsTemp.Tables[0].Rows[0]["POLICY_STATUS"].ToString();
                else
                    return null;

            }
            catch (Exception objExp)
            {
                throw (objExp);
            }

        }

        public string GetStatusOfApplication(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            DataSet dsTemp;
            try
            {

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POL_ID", PolicyID);
                objWrapper.AddParameter("@POL_VERSION_ID", PolicyVersionID);
                dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyStatus");
                objWrapper.ClearParameteres();
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (dsTemp.Tables[0].Rows.Count > 0)
                    return dsTemp.Tables[0].Rows[0]["APP_STATUS"].ToString();
                else
                    return null;

            }
            catch (Exception objExp)
            {
                throw (objExp);
            }

        }

        /*
        /// <summary>
        /// Get Customer Agency Information from Customer_ID
        /// </summary>
        /// <param name="CustomerID">Customer_ID</param>
        /// <returns>Datatable</returns>
        /// Added by Charles on 22-Mar-2010 for Policy Page Implementation
        public static DataTable GET_AGENCY_INFO_FROM_CUSTOMER_ID(int CustomerID)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            DataSet dsTemp = null;
            try
            {

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                dsTemp = objWrapper.ExecuteDataSet("PROC_GET_AGENCY_INFO_FROM_CUSTOMER_ID");
                objWrapper.ClearParameteres();

                if (dsTemp.Tables[0].Rows.Count > 0)
                    return dsTemp.Tables[0];
                else
                    return null;

            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
            finally
            {
                dsTemp.Dispose();
            }
        }
         * */

        public DataSet GetPolicyLOBString(int LOBID)
        {
            string strStoredProc = "Proc_GetLOBString";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@LOB_ID", LOBID);


                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }

        public void GetAppDetailsFromPolicy(int CustomerId, int PolicyId, int PolicyVersionId, out int AppId, out int AppVersionId)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                DataSet dsTemp;
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppLobDetailsFromPolicy");
                AppId = int.Parse(dsTemp.Tables[0].Rows[0]["APP_ID"].ToString());
                AppVersionId = int.Parse(dsTemp.Tables[0].Rows[0]["APP_VERSION_ID"].ToString());
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }

        #region Named Insured Functions
        public static int GetPolicyPrimary_Applicant(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            int intApplicant_ID = 0;
            try
            {
                DataTable dt = new DataTable();
                dt = CheckApplicantForPolicy(CustomerID, PolicyID, PolicyVersionID);
                if (dt != null && dt.Rows.Count > 0)
                    intApplicant_ID = int.Parse(dt.Rows[0]["APPLICANT_ID"].ToString());

            }
            catch
            {
                //throw (exc);
            }
            finally
            { }
            return intApplicant_ID;
        }

        public static DataTable CheckApplicantForPolicy(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMERID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICYID", PolicyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICYVERSIONID", PolicyVersionID, SqlDbType.Int);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_CheckPrimaryApplicantForPolicy");
                return dsTemp.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public static DataTable FetchCustApplicantInsured(int CustomerID)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMERID", CustomerID, SqlDbType.Int);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCustApplicantInsured");
                return dsTemp.Tables[0];
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        public static void SavePrimaryNamedInsured(ArrayList arr)
        {
            string strStoredProc = "Proc_InsertPrimaryNamedInsured";
            try
            {
                for (int i = 0; i <= arr.Count - 1; i++)
                {
                    ClsPolicyInsuredInfo objPolicy = (ClsPolicyInsuredInfo)arr[i];
                    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                    objDataWrapper.AddParameter("@POLICY_ID", objPolicy.POLICY_ID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicy.POLICY_VERSION_ID);
                    objDataWrapper.AddParameter("@APPLICANT_ID", objPolicy.APPLICANT_ID);
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objPolicy.CUSTOMER_ID);
                    //objDataWrapper.AddParameter("@APP_ID",objPolicy.APP_ID);
                    //objDataWrapper.AddParameter("@APP_VERSION_ID",objPolicy.APP_VERSION_ID);
                    objDataWrapper.AddParameter("@CREATED_BY", objPolicy.CREATED_BY);
                    objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT", objPolicy.IS_PRIMARY_APPLICANT);
                    objDataWrapper.AddParameter("@COMMISSION_PERCENT", objPolicy.COMMISSION_PERCENT);
                    objDataWrapper.AddParameter("@FEES_PERCENT", objPolicy.FEES_PERCENT);
                    objDataWrapper.AddParameter("@PRO_LABORE_PERCENT", objPolicy.PRO_LABORE_PERCENT);
                    objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        /// <summary>
        /// Updating applicants for customer ,application & applicantion version.
        /// Delete is performed for customer ,application & applicantion version befor updating the
        /// applicants. 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By"></param>
        public static void UpdatePrimaryNamedInsured(ArrayList arr)
        {


            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {

                DeleteSelectedCustApplicant((ClsPolicyInsuredInfo)arr[0], objDataWrapper);

                string strStoredProc = "Proc_UpdatePrimaryNamedInsured";
                objDataWrapper.ClearParameteres();
                for (int i = 0; i <= arr.Count - 1; i++)
                {
                    ClsPolicyInsuredInfo objPolicy = (ClsPolicyInsuredInfo)arr[i];
                    objDataWrapper.AddParameter("@POLICY_ID", objPolicy.POLICY_ID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicy.POLICY_VERSION_ID);
                    objDataWrapper.AddParameter("@APPLICANT_ID", objPolicy.APPLICANT_ID);
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objPolicy.CUSTOMER_ID);
                    //objDataWrapper.AddParameter("@APP_ID",objPolicy.APP_ID);
                    //objDataWrapper.AddParameter("@APP_VERSION_ID",objPolicy.APP_VERSION_ID);
                    objDataWrapper.AddParameter("@MODIFIED_BY", objPolicy.MODIFIED_BY);
                    objDataWrapper.AddParameter("@IS_PRIMARY_APPLICANT", objPolicy.IS_PRIMARY_APPLICANT);
                    objDataWrapper.AddParameter("@COMMISSION_PERCENT", objPolicy.COMMISSION_PERCENT);
                    objDataWrapper.AddParameter("@FEES_PERCENT", objPolicy.FEES_PERCENT);
                    objDataWrapper.AddParameter("@PRO_LABORE_PERCENT", objPolicy.PRO_LABORE_PERCENT);
                    objDataWrapper.ExecuteNonQuery(strStoredProc);
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                }
            }
            catch (Exception exc)
            {
                objDataWrapper.RollbackTransaction(Cms.DataLayer.DataWrapper.CloseConnection.YES);
                throw (exc);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        /// <summary>
        /// Deleting all the apllicants for particular customer,applicant & application version.
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="objDataWrapper"></param>
        private static void DeleteSelectedCustApplicant(ClsPolicyInsuredInfo objPolicy, DataWrapper objDataWrapper)
        {
            string strStoredProc = "Proc_DeletePrimaryNamedInsured";
            objDataWrapper.AddParameter("@CUSTOMER_ID", objPolicy.CUSTOMER_ID);
            objDataWrapper.AddParameter("@POLICY_ID", objPolicy.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicy.POLICY_VERSION_ID);
            objDataWrapper.ExecuteNonQuery(strStoredProc);
            objDataWrapper.ClearParameteres();

        }
        public static DataSet CheckApplicantInRemuneration(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            DataSet ds;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {

                string strStoredProc = "Proc_GetRemunerationCoApp";
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                objDataWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.Dispose();
            }


            return ds;

        }

        #endregion

        # endregion

        public string GetValidationRange(int customerID, int appID, int appVersionID)
        {
            string strStoredProcName = "Proc_GetRangeValidation", strIsValid = "";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@ISVALID", SqlDbType.Int, ParameterDirection.Output);
                objDataWrapper.ExecuteNonQuery(strStoredProcName);
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                strIsValid = objSqlParameter.Value.ToString();
                objDataWrapper.ClearParameteres();
                return strIsValid;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }




        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// getting all driver list for a particular application 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">ashis kumar</param>

        public DataSet GetDriverList(int customerID, int appID, int appVersionID, string strLobID)
        {

            try
            {
                DataSet dsDriver = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                if (strLobID == "2" || strLobID == "3")//2,3 for ppa and motorcyle 
                {
                    dsDriver = objDataWrapper.ExecuteDataSet("Proc_GetDriversDetails");
                }
                else// watercraft
                {
                    dsDriver = objDataWrapper.ExecuteDataSet("Proc_GetWaterCraftDriversDetails");
                }
                return dsDriver;



            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// getting previous UDVI report from database 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">Mohit Agarwal</param>

        public DataSet GetUDVIReport(int customerID, int appID, int appVersionID, string calledFor)
        {

            try
            {
                DataSet dsUDVI = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APPPOL_ID", appID);
                objDataWrapper.AddParameter("@APPPOL_VERSION_ID", appVersionID);
                objDataWrapper.AddParameter("@CALLED_FOR", calledFor);

                dsUDVI = objDataWrapper.ExecuteDataSet("Proc_Get_UDVI_Report");

                return dsUDVI;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }


        public void AddUDVIReport(int customerID, int appID, int appVersionID, int userID, string reportHtml, string CalledFor)
        {

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APPPOL_ID", appID);
                objDataWrapper.AddParameter("@APPPOL_VERSION_ID", appVersionID);
                objDataWrapper.AddParameter("@REPORT_HTML", reportHtml);
                objDataWrapper.AddParameter("@CREATED_BY", userID);
                objDataWrapper.AddParameter("@CALLED_FOR", CalledFor);

                objDataWrapper.ExecuteNonQuery("Proc_Insert_UDVI_Report");
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }
        //overload by Pravesh on 19 March 09		
        public void WriteTransactionLog(int CustomerID, int App_PolicyID, int App_PolicyVersionID, string TransactionDescription, int RecordedBy, string CustomDesc, string strCalledFrom)
        {
            WriteTransactionLog(CustomerID, App_PolicyID, App_PolicyVersionID, TransactionDescription, RecordedBy, CustomDesc, strCalledFrom, null);
        }
        //Added by Mohit Agarwal ITrack 2030 26-Jun-2007
        public void WriteTransactionLog(int CustomerID, int App_PolicyID, int App_PolicyVersionID, string TransactionDescription, int RecordedBy, string CustomDesc, string strCalledFrom, DataWrapper objDataWrapper)
        {
            //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
            if (objDataWrapper == null)
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
            objTransactionInfo.CUSTOM_INFO = CustomDesc;
            objTransactionInfo.TRANS_TYPE_ID = 0;
            objTransactionInfo.CLIENT_ID = CustomerID;
            if (strCalledFrom.ToUpper().Equals("POLICY"))
            {
                objTransactionInfo.POLICY_ID = App_PolicyID;
                objTransactionInfo.POLICY_VER_TRACKING_ID = App_PolicyVersionID;
            }
            else if (strCalledFrom.ToUpper().Equals("APPLICATION"))
            {
                objTransactionInfo.APP_ID = App_PolicyID;
                objTransactionInfo.APP_VERSION_ID = App_PolicyVersionID;
            }
            else
                return;
            objTransactionInfo.RECORDED_BY = RecordedBy;
            objTransactionInfo.TRANS_DESC = TransactionDescription;
            objDataWrapper.ExecuteNonQuery(objTransactionInfo);
            objDataWrapper.ClearParameteres();
        }
        //overload by Pravesh on 19 March 09
        public void SetMvrOrdered(int customerID, int appID, int appVersionID, int driverID, string strLobID, string mvr_remarks, string mvr_status, string CalledFrom, string mvr_lic_class, string DRIVER_LIC_APP)
        {
            SetMvrOrdered(customerID, appID, appVersionID, driverID, strLobID, mvr_remarks, mvr_status, CalledFrom, mvr_lic_class, DRIVER_LIC_APP, null);
        }
        //Added by Mohit Agarwal 3-Jul-07
        public void SetMvrOrdered(int customerID, int appID, int appVersionID, int driverID, string strLobID, string mvr_remarks, string mvr_status, string CalledFrom, string mvr_lic_class, string DRIVER_LIC_APP, DataWrapper objDataWrapper)
        {

            try
            {
                //DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                objDataWrapper.AddParameter("@DRIVER_ID", driverID);
                objDataWrapper.AddParameter("@MVR_REMARKS", mvr_remarks);
                objDataWrapper.AddParameter("@MVR_STATUS", mvr_status);
                objDataWrapper.AddParameter("@CALLED_FROM", CalledFrom);
                objDataWrapper.AddParameter("@MVR_LIC_CLASS", mvr_lic_class);
                objDataWrapper.AddParameter("@DRIVER_LICENSE_APPLICATION", DRIVER_LIC_APP);
                if (strLobID == "2" || strLobID == "3")//2,3 for ppa and motorcyle 
                {
                    objDataWrapper.ExecuteNonQuery("Proc_SetDriverMvrOrder");
                }
                else// watercraft
                {
                    objDataWrapper.ExecuteNonQuery("Proc_SetWatDriverMvrOrder");
                }
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }

        /// <summary>
        /// getting all driver list for a particular Policy 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">Swarup pal</param>

        public DataSet GetNamedInsuredList(int customerID, int polID, int polVersionID, string strCalledFrom)
        {

            try
            {
                DataSet dsAppl = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POLICY_ID", polID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", polVersionID);
                objDataWrapper.AddParameter("@CALLED_FROM", strCalledFrom);
                dsAppl = objDataWrapper.ExecuteDataSet("Proc_GetCancellationNoticeData");
                return dsAppl;



            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }

        /// <summary>
        /// Fetch Co-Insurers for Pol
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="polID"></param>
        /// <param name="polVersionID"></param>
        /// <param name="carrierID"></param>
        /// <returns></returns>
        /// Added by Charles on 22-Mar-10 for Co-Insurance Page
        public static DataTable GetPolicyCoInsurers(int customerID, int polID, int polVersionID, int carrierID, string strCOINSURANCE_ID)
        {
            string strStoredProc = "Proc_GetPOL_POL_CO_INSURANCE";
            DataSet dstemp = null;
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                objWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objWrapper.AddParameter("@POLICY_ID", polID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", polVersionID);
                objWrapper.AddParameter("@COMPANY_ID", carrierID);
                objWrapper.AddParameter("@COINSURANCE_ID", strCOINSURANCE_ID);
                dstemp = objWrapper.ExecuteDataSet(strStoredProc);
                if (dstemp.Tables.Count > 0)
                {
                    return dstemp.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get Multilingual Information
        /// </summary>
        /// <returns></returns>
        /// Added by Charles on 23-Mar-10 for Multilingual Description Support
        public static DataTable GetLangCultInfo()
        {
            string strStoredProc = "PROC_LANG_CULTURE_INFO";
            DataSet dstemp = null;
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

                dstemp = objWrapper.ExecuteDataSet(strStoredProc);
                if (dstemp.Tables.Count > 0)
                {
                    return dstemp.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get Master & Child Table Descriptions
        /// </summary>
        /// <param name="strPRIMARY_COLUMN">PRIMARY COLUMN NAME</param>
        /// <param name="iPRIMARY_ID">PRIMARY ID VALUE</param>
        /// <param name="strMASTER_TABLE_NAME">MASTER TABLE NAME</param>
        /// <param name="strCHILD_TABLE_NAME">CHILD TABLE NAME</param>
        /// <param name="strDESCRIPTION_COLUMN">DESCRIPTION COLUMN NAME</param>
        /// <returns>DESCRIPTIONS WITH LANG_ID</returns>
        public static DataTable GetMasterChildInfo(string strPRIMARY_COLUMN, int iPRIMARY_ID, string strMASTER_TABLE_NAME, string strCHILD_TABLE_NAME, string strDESCRIPTION_COLUMN)
        {
            string strStoredProc = "PROC_FETCH_MASTER_CHILD_INFO";
            DataSet dstemp = null;
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                objWrapper.AddParameter("@PRIMARY_COLUMN", strPRIMARY_COLUMN);
                objWrapper.AddParameter("@PRIMARY_ID", iPRIMARY_ID);
                objWrapper.AddParameter("@MASTER_TABLE_NAME", strMASTER_TABLE_NAME);
                objWrapper.AddParameter("@CHILD_TABLE_NAME", strCHILD_TABLE_NAME);
                objWrapper.AddParameter("@DESCRIPTION_COLUMN", strDESCRIPTION_COLUMN);
                objWrapper.AddParameter("@Lang_id", ClsCommon.BL_LANG_ID);
                dstemp = objWrapper.ExecuteDataSet(strStoredProc);
                if (dstemp.Tables.Count > 0)
                {
                    return dstemp.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static int SaveMasterChildInfo(string strPRIMARY_COLUMN, int iPRIMARY_ID, string strTABLE_NAME, int iLANG_ID, string strDESCRIPTION_COLUMN, string strDESCRIPTION)
        {
            string strStoredProc = "PROC_UPDATE_MASTER_CHILD_INFO";
            int result = 0;
            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                objWrapper.AddParameter("@PRIMARY_COLUMN", strPRIMARY_COLUMN);
                objWrapper.AddParameter("@PRIMARY_ID", iPRIMARY_ID);
                objWrapper.AddParameter("@TABLE_NAME", strTABLE_NAME);
                objWrapper.AddParameter("@LANG_ID", iLANG_ID);
                objWrapper.AddParameter("@DESCRIPTION_COLUMN", strDESCRIPTION_COLUMN);
                objWrapper.AddParameter("@DESCRIPTION", strDESCRIPTION);

                result = objWrapper.ExecuteNonQuery(strStoredProc);
            }
            catch
            {
                result = -3; //Exception
            }

            return result;
        }

        /// <summary>
        /// Get Co-Insurance Carriers
        /// </summary>
        /// <returns></returns>
        /// Added by Charles on 22-Mar-10 for Co-Insurance Page
        public static DataSet GetCoInsurers(int customerID, int polID, int polVersionID)
        {
            string strStoredProc = "Proc_Get_CO_INSURERS";
            DataSet dstemp = null;

            try
            {
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                objWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objWrapper.AddParameter("@POLICY_ID", polID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", polVersionID);
                dstemp = objWrapper.ExecuteDataSet(strStoredProc);
                if (dstemp.Tables.Count > 0)
                {
                    return dstemp;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Delete Policy Co-Insurers
        /// </summary>
        /// <param name="iCOINSURANCE_ID"></param>
        /// <returns></returns>
        /// Added by Charles on 22-Mar-10 for Co-Insurance Page
        public static int DelPolCoInsurers(ArrayList aldelcoinsurance)
        {

            XmlDocument xmlDoc = new XmlDocument();
            string strTranXML;
            StringBuilder sbTranXml = new StringBuilder();
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            sbTranXml.Append("<root>");
            try
            {
                ClsCoInsuranceInfo objdelCoInsuranceInfo = (ClsCoInsuranceInfo)aldelcoinsurance[0];

                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
                for (int i = 0; i < aldelcoinsurance.Count; i++)
                {
                    ClsCoInsuranceInfo objdelModel = (ClsCoInsuranceInfo)aldelcoinsurance[i];

                    string strStoredProc = "Proc_DelPOL_POL_CO_INSURANCE";
                    int result;

                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@COINSURANCE_ID", objdelModel.COINSURANCE_ID);
                    objWrapper.AddParameter("@CUSTOMER_ID", objdelModel.CUSTOMER_ID);
                    objWrapper.AddParameter("@POLICY_ID", objdelModel.POLICY_ID);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", objdelModel.POLICY_VERSION_ID);
                    objWrapper.AddParameter("@COMPANY_ID", objdelModel.COMPANY_ID);
                    result = objWrapper.ExecuteNonQuery(strStoredProc);

                    objdelModel.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\AddCoinsurance.aspx.resx");
                    strTranXML = objBuilder.GetTransactionLogXML(objdelModel);
                    sbTranXml.Append(strTranXML);

                }
                sbTranXml.Append("</root>");
                if (sbTranXml.ToString() != "<root></root>")// || strCustomInfo!="")
                {

                    objTransactionInfo.RECORDED_BY = objdelCoInsuranceInfo.CREATED_BY;
                    objTransactionInfo.POLICY_ID = objdelCoInsuranceInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objdelCoInsuranceInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objdelCoInsuranceInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 190;
                    objTransactionInfo.CHANGE_XML = sbTranXml.ToString();
                    objTransactionInfo.CUSTOM_INFO = "";
                    objWrapper.ExecuteNonQuery(objTransactionInfo);
                }
                objWrapper.ClearParameteres();
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// getting all driver list for a particular Policy 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">Swarup pal</param>

        public DataSet GetPolDriverList(int customerID, int polID, int polVersionID, string strLobID)
        {

            try
            {
                DataSet dsDriver = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@POL_ID", polID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", polVersionID);
                if (strLobID == "2" || strLobID == "3")//2,3 for ppa and motorcyle 
                {
                    dsDriver = objDataWrapper.ExecuteDataSet("Proc_GetPolDriversDetails");
                }
                else// watercraft
                {
                    dsDriver = objDataWrapper.ExecuteDataSet("Proc_GetPolWaterCraftDriversDetails");
                }
                return dsDriver;



            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }


        /// <summary>
        /// getting all violation detials for list of ssc volition code (retrived from mvr web service) 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">ashis kumar</param>
        public DataSet GetViolationRecords(string strViolationCode, string strLobID, string strStateID)
        {

            try
            {
                string strTempcode = strViolationCode;
                //strTempcode = strTempcode.Replace("'","''"); 


                DataSet dsViolationDetails = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@MVR_Violation_Codes", strTempcode);
                objDataWrapper.AddParameter("@LobID", strLobID);
                objDataWrapper.AddParameter("@StateID", strStateID);
                dsViolationDetails = objDataWrapper.ExecuteDataSet("Proc_GetMVRViolationDetails");
                return dsViolationDetails;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }

        /// <summary>
        /// Save Policy Co-Insurers
        /// </summary>
        /// <param name="objCoInsuranceInfo"></param>
        /// <param name="flag"></param>
        /// Added by Charles on 22-Mar-2010 for Co-Insurance Page
        public int SavePolCoInsurers(ArrayList arObjCoinsurance, string strOldXML)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc;
            int iTransactionID = 0;
            XmlElement root = null;
            XmlDocument xmlDoc = new XmlDocument();
            string strTranXML;
            strStoredProc = "Proc_SavePOL_POL_CO_INSURANCE";
            StringBuilder sbTranXml = new StringBuilder();
            sbTranXml.Append("<root>");
            if (strOldXML != "")
            {
                //strOldXML = ReplaceXMLCharacters(strOldXML);
                xmlDoc.LoadXml(strOldXML);
                root = xmlDoc.DocumentElement; //holds the root of the transaction XML
            }

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                ClsCoInsuranceInfo objCoInsuranceInfo = (ClsCoInsuranceInfo)arObjCoinsurance[0];

                Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                int returnResult = 0;
                for (int i = 0; i < arObjCoinsurance.Count; i++)
                {
                    ClsCoInsuranceInfo objModel = (ClsCoInsuranceInfo)arObjCoinsurance[i];
                    objDataWrapper.ClearParameteres();

                    objDataWrapper.AddParameter("@CUSTOMER_ID", objModel.CUSTOMER_ID);
                    objDataWrapper.AddParameter("@POLICY_ID", objModel.POLICY_ID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objModel.POLICY_VERSION_ID);
                    objDataWrapper.AddParameter("@COMPANY_ID", objModel.COMPANY_ID);
                    objDataWrapper.AddParameter("@CO_INSURER_NAME", objModel.CO_INSURER_NAME);
                    objDataWrapper.AddParameter("@LEADER_FOLLOWER", objModel.LEADER_FOLLOWER);
                    objDataWrapper.AddParameter("@COINSURANCE_PERCENT", objModel.COINSURANCE_PERCENT);
                    objDataWrapper.AddParameter("@COINSURANCE_FEE", objModel.COINSURANCE_FEE);
                    objDataWrapper.AddParameter("@BROKER_COMMISSION", objModel.BROKER_COMMISSION);
                    objDataWrapper.AddParameter("@TRANSACTION_ID", objModel.TRANSACTION_ID);
                    objDataWrapper.AddParameter("@LEADER_POLICY_NUMBER", objModel.LEADER_POLICY_NUMBER);
                    objDataWrapper.AddParameter("@FLAG", objModel.Action);
                    objDataWrapper.AddParameter("@COINSURANCE_ID", objModel.COINSURANCE_ID);
                    objDataWrapper.AddParameter("@CREATED_BY", objModel.CREATED_BY);
                    objDataWrapper.AddParameter("@CREATED_DATETIME", objModel.CREATED_DATETIME);
                    objDataWrapper.AddParameter("@ENDORSEMENT_POLICY_NUMBER", objModel.ENDORSEMENT_POLICY_NUMBER);
                    SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@Trans_Id", null, SqlDbType.Int, ParameterDirection.Output);


                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    iTransactionID = int.Parse(objSqlParameter.Value.ToString());


                    if (iTransactionID != 1)
                    {
                        if (objModel.Action == 0)
                        {
                            objModel.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\AddCoinsurance.aspx.resx");
                            strTranXML = objBuilder.GetTransactionLogXML(objModel);
                            sbTranXml.Append(strTranXML);

                        }
                        else
                        {

                            objModel.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\AddCoinsurance.aspx.resx");
                            strTranXML = this.GetCoinsuranceTranXML(objModel, strOldXML, objModel.COINSURANCE_ID, root);
                            if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                                sbTranXml.Append(strTranXML);


                        }

                    }


                }

                sbTranXml.Append("</root>");
                if (sbTranXml.ToString() != "<root></root>")// || strCustomInfo!="")
                {

                    objTransactionInfo.RECORDED_BY = objCoInsuranceInfo.CREATED_BY;
                    objTransactionInfo.POLICY_ID = objCoInsuranceInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objCoInsuranceInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objCoInsuranceInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 189;
                    objTransactionInfo.CHANGE_XML = sbTranXml.ToString();
                    objTransactionInfo.CUSTOM_INFO = "";
                    objDataWrapper.ExecuteNonQuery(objTransactionInfo);
                }
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return iTransactionID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }


        protected string GetCoinsuranceTranXML(Cms.Model.Policy.ClsCoInsuranceInfo objNew, string xml, int coverageID, XmlElement root)
        {
            if (root == null) return "";
            XmlNode node = root.SelectSingleNode("Table[COINSURANCE_ID=" + coverageID.ToString() + "]");

            //For making Old Object of model class
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(xml);
            XmlNodeList oldNodeList = XmlDoc.SelectNodes("NewDataSet/Table");
            XmlNode oldNode = oldNodeList.Item(0);
            oldNode = oldNodeList.Item(0);
            string strTranXML = "";

            Cms.Model.Policy.ClsCoInsuranceInfo objOld = new Cms.Model.Policy.ClsCoInsuranceInfo();

            objOld.POLICY_ID = objNew.POLICY_ID;
            objOld.POLICY_VERSION_ID = objNew.POLICY_VERSION_ID;
            objOld.CUSTOMER_ID = objNew.CUSTOMER_ID;
            objOld.COINSURANCE_ID = objNew.COINSURANCE_ID;

            XmlNode element = null;



            element = oldNode.SelectSingleNode("COINSURANCE_ID");

            if (element.InnerXml != "")
            {
                objOld.COINSURANCE_ID = Convert.ToInt32(element.InnerXml);
            }
            if (objOld.COINSURANCE_ID != objNew.COINSURANCE_ID)
            {

                element = oldNode.SelectSingleNode("LEADER_FOLLOWER");

                if (element != null)
                {
                    objOld.LEADER_FOLLOWER = element.InnerXml == "" ? 0 : Convert.ToInt32(element.InnerXml);
                }

                element = oldNode.SelectSingleNode("LEADER_POLICY_NUMBER");
                if (element != null)
                {
                    objOld.LEADER_POLICY_NUMBER = element.InnerXml;
                }

                element = oldNode.SelectSingleNode("TRANSACTION_ID");
                if (element != null)
                {
                    objOld.TRANSACTION_ID = element.InnerXml;
                }

                element = oldNode.SelectSingleNode("CO_INSURER_NAME");
                if (element != null)
                {
                    objOld.CO_INSURER_NAME = element.InnerXml;
                }

                element = oldNode.SelectSingleNode("COINSURANCE_PERCENT");
                if (element != null)
                {
                    objOld.COINSURANCE_PERCENT = element.InnerXml == "" ? 0 : Convert.ToDouble(element.InnerXml);
                }

                element = oldNode.SelectSingleNode("COINSURANCE_FEE");
                if (element != null)
                {
                    objOld.COINSURANCE_FEE = element.InnerXml == "" ? 0 : Convert.ToDouble(element.InnerXml);
                }

                element = oldNode.SelectSingleNode("BROKER_COMMISSION");
                if (element != null)
                {
                    objOld.BROKER_COMMISSION = element.InnerXml == "" ? 0 : Convert.ToDouble(element.InnerXml);
                }
                SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                strTranXML = objBuilder.GetTransactionLogXML(objOld, objNew);
            }
            return strTranXML;
        }

        /// <summary>
        /// insert violation entry in for 2,3,4 lob 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">ashis kumar</param>
        public void InsertMVRViolationDetail(bool IsWaterCraft, ClsMvrInfo objMVRInfo, string strPolicyID, string strPolicyVerID)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc;
            string strTranXML;
            if (IsWaterCraft == false)
            {
                strStoredProc = "Proc_InsertPOL_MVR_INFORMATION";
            }
            else
            {
                strStoredProc = "Proc_InsertPOL_WATERCRAFT_MVR_INFORMATION";
            }
            DateTime RecordDate = DateTime.Now;


            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {


                objDataWrapper.AddParameter("@CUSTOMER_ID", objMVRInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", strPolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", strPolicyVerID);
                objDataWrapper.AddParameter("@DRIVER_ID", objMVRInfo.DRIVER_ID);
                objDataWrapper.AddParameter("@MVR_AMOUNT", objMVRInfo.MVR_AMOUNT);
                objDataWrapper.AddParameter("@VERIFIED", 1);

                if (objMVRInfo.MVR_DEATH != "")
                {
                    objDataWrapper.AddParameter("@MVR_DEATH", objMVRInfo.MVR_DEATH);
                }
                else
                {
                    objDataWrapper.AddParameter("@MVR_DEATH", DBNull.Value);
                }
                if (objMVRInfo.MVR_DATE.Ticks != 0)
                {
                    objDataWrapper.AddParameter("@MVR_DATE", objMVRInfo.MVR_DATE);
                }
                else
                {
                    objDataWrapper.AddParameter("@MVR_DATE", DBNull.Value);
                }
                objDataWrapper.AddParameter("@VIOLATION_ID", objMVRInfo.VIOLATION_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", objMVRInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@VIOLATION_SOURCE", "IIX");

                if (IsWaterCraft == false)
                {

                    objDataWrapper.AddParameter("@APP_MVR_ID", "", SqlDbType.Int, ParameterDirection.Output);

                }
                else
                {

                    objDataWrapper.AddParameter("@APP_WATER_MVR_ID", "", SqlDbType.Int, ParameterDirection.Output);


                }

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objMVRInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");

                    //SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    strTranXML = objBuilder.GetTransactionLogXML(objMVRInfo);
                    //Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objMVRInfo.CREATED_BY;
                    objTransactionInfo.APP_ID = objMVRInfo.APP_ID;
                    objTransactionInfo.APP_VERSION_ID = objMVRInfo.APP_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objMVRInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1570", "");// "Mvr info is added for a driver violation";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    objTransactionInfo.CUSTOM_INFO = "";
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }


                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        /// <summary>
        /// getting all driver list for a particular application 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">ashis kumar</param>


        public DataSet GetPoilcyInfo(int customerID, int appID, int appVersionID)
        {

            try
            {
                DataSet dsDriver = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                dsDriver = objDataWrapper.ExecuteDataSet("Proc_GetAppPolDetail");
                return dsDriver;
            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }


        /// <summary>
        /// insert unmatched violation 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">ashis kumar</param>
        public int InsertUnmatchedMVRViolationDetail(Cms.Model.Policy.ClsPolicyAutoMVR objMVRInfo, string strMVRCode, string strDescription, string strAppID, string strAppVersionID, bool TranLog)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_Insert_MVR_EXCEPTION";
            string strTranXML;
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {


                objDataWrapper.AddParameter("@CUSTOMER_ID", objMVRInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", strAppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", strAppVersionID);
                objDataWrapper.AddParameter("@POL_ID", objMVRInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", objMVRInfo.POLICY_VERSION_ID);
                if (objMVRInfo.MVR_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@MVR_DATE", objMVRInfo.MVR_DATE);
                else
                    objDataWrapper.AddParameter("@MVR_DATE", System.DBNull.Value);
                objDataWrapper.AddParameter("@MVR_CODE", strMVRCode);
                objDataWrapper.AddParameter("@MVR_DESCRIPTION", strDescription);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@MVR_ID", 0, SqlDbType.Int, ParameterDirection.Output);
                int returnResult = 0;
                if (TranLog)
                {
                    if (TransactionLogRequired)
                    {
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        objMVRInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");

                        //SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        strTranXML = objBuilder.GetTransactionLogXML(objMVRInfo);
                        //Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        objTransactionInfo.TRANS_TYPE_ID = 1;
                        objTransactionInfo.RECORDED_BY = objMVRInfo.CREATED_BY;
                        objTransactionInfo.POLICY_ID = objMVRInfo.POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objMVRInfo.POLICY_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objMVRInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1571", "");// "Unmatched Mvr info is added for a driver violation";
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        objTransactionInfo.CUSTOM_INFO = "";
                        //Executing the query
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                //
                int MVR_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (MVR_ID == -1)
                {
                    return -1;
                }
                else
                {
                    //objMvrInfo.APP_MVR_ID = APP_MVR_ID;
                    //return returnResult;
                    return MVR_ID;
                }
                //

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        /// <summary>
        /// insert unmatched driver violation 
        /// </summary>
        /// <param name="dtTemp"></param> ClsMvrInfo()
        /// <param name="modified_By">Pravesh Chandel</param>
        public int InsertUnmatchedMVRViolationDetail(Cms.Model.Application.ClsMvrInfo objMVRInfo, string strMVRCode, string strDescription, string strAppID, string strAppVersionID, string strPolID, string strPolVerID, bool TranLog)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            //string strStoredProc="Proc_Insert_MVR_AUTO";
            string strStoredProc = "Proc_Insert_MVR_EXCEPTION";
            string strTranXML;
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {


                objDataWrapper.AddParameter("@CUSTOMER_ID", objMVRInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID", strAppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", strAppVersionID);

                objDataWrapper.AddParameter("@POL_ID", strPolID);
                objDataWrapper.AddParameter("@POL_VERSION_ID", strPolVerID);
                //	objDataWrapper.AddParameter("@DRIVER_ID",objMVRInfo.DRIVER_ID);

                objDataWrapper.AddParameter("@MVR_CODE", strMVRCode);
                if (objMVRInfo.MVR_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@MVR_DATE", objMVRInfo.MVR_DATE);
                else
                    objDataWrapper.AddParameter("@MVR_DATE", System.DBNull.Value);
                objDataWrapper.AddParameter("@MVR_DESCRIPTION", strDescription);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@MVR_ID", 0, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TranLog)
                {
                    if (TransactionLogRequired)
                    {
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        objMVRInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");

                        //SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        strTranXML = objBuilder.GetTransactionLogXML(objMVRInfo);
                        //Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        objTransactionInfo.TRANS_TYPE_ID = 1;
                        objTransactionInfo.RECORDED_BY = objMVRInfo.CREATED_BY;
                        objTransactionInfo.POLICY_ID = int.Parse(strPolID);
                        objTransactionInfo.POLICY_VER_TRACKING_ID = int.Parse(strPolVerID);
                        objTransactionInfo.CLIENT_ID = objMVRInfo.CUSTOMER_ID;
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1571", "");//  "Unmatched Mvr info is added for a driver violation";
                        objTransactionInfo.CHANGE_XML = strTranXML;
                        objTransactionInfo.CUSTOM_INFO = "";
                        //Executing the query
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    }
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                //
                int MVR_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (MVR_ID == -1)
                {
                    return -1;
                }
                else
                {
                    //objMvrInfo.APP_MVR_ID = APP_MVR_ID;
                    //return returnResult;
                    return MVR_ID;
                }
                //


                //objDataWrapper.ClearParameteres();
                //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        ///fetch Undisclosed drivers
        ///
        ///by pravesh on  19 dec 2006
        ///

        public static System.Data.DataSet GetUndisclosedDrivers(int Customer_id, int App_id, int App_version_id)
        {
            string strStoredProc = "Proc_Get_UNDISCLOSED_DRIVER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                System.Data.DataSet dsUnDisclosedDriver = new System.Data.DataSet();
                objDataWrapper.AddParameter("@CUSTOMER_ID", Customer_id);
                objDataWrapper.AddParameter("@APP_ID", App_id);
                objDataWrapper.AddParameter("@APP_VERSION_ID", App_version_id);
                //objDataWrapper.AddParameter("@DRIVER_ID",strDriverID);
                //int returnResult = 0;
                dsUnDisclosedDriver = objDataWrapper.ExecuteDataSet(strStoredProc);
                //		}
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return dsUnDisclosedDriver;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }
        /// 
        /// Delete Undisclosed Drivers  
        /// by Pravesh on 20 dec 2006
        /// 
        public static int DeleteUndisclosedDrivers(string Undisclosed_DriverIDs)
        {
            string strStoredProc = "Proc_delete_UNDISCLOSED_DRIVER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@UNDISCLOSED_DRIVER_IDS", Undisclosed_DriverIDs);
                int returnResult = 0;
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        ///save undisclosed driver details
        ///by pravesh chandel on 13 dec 2006
        ///
        public void InsertUndisclosedDriverDetail(string strCustomerID, string strAppID, string strAppVersionID, string strDriverID, string strDriverName)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "Proc_SAVE_UNDISCLOSED_DRIVER";
            //string strTranXML;
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {


                objDataWrapper.AddParameter("@CUSTOMER_ID", strCustomerID);
                objDataWrapper.AddParameter("@APP_ID", strAppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", strAppVersionID);
                objDataWrapper.AddParameter("@DRIVER_ID", strDriverID);
                objDataWrapper.AddParameter("@DRIVER_NAME", strDriverName);
                int returnResult = 0;
                //if (TranLog)
                //	{
                //		if(TransactionLogRequired)
                //		{
                //			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                //				objMVRInfo.TransactLabel  = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                //SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                //strTranXML = objBuilder.GetTransactionLogXML(objMVRInfo);
                //Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                //objTransactionInfo.TRANS_TYPE_ID	=	1;
                //objTransactionInfo.RECORDED_BY		=	objMVRInfo.CREATED_BY;
                //objTransactionInfo.POLICY_ID 			=	objMVRInfo.POLICY_ID;
                //objTransactionInfo.POLICY_VER_TRACKING_ID =	objMVRInfo.POLICY_VERSION_ID; 
                //objTransactionInfo.CLIENT_ID		=	objMVRInfo.CUSTOMER_ID;
                //objTransactionInfo.TRANS_DESC		=	"Unmatched Mvr info is added for a driver violation";
                //objTransactionInfo.CHANGE_XML		=	strTranXML;
                //objTransactionInfo.CUSTOM_INFO		=	"";
                //Executing the query
                //returnResult	= objDataWrapper.ExecuteNonQuery (strStoredProc ,objTransactionInfo);
                //				returnResult	= objDataWrapper.ExecuteNonQuery (strStoredProc);
                //			}
                //		}
                //		else
                //		{
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                //		}

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        ///save undisclosed driver details
        ///by SWARUP PAL on 19 Mar 2007
        ///
        public void InsertUndisclosedPolDriverDetail(string strCustomerID, string strPolID, string strPolVersionID, string strDriverID, string strDriverName)
        {
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "PROC_SAVE_POL_UNDISCLOSED_DRIVER";
            //string strTranXML;
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {


                objDataWrapper.AddParameter("@CUSTOMER_ID", strCustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", strPolID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", strPolVersionID);
                objDataWrapper.AddParameter("@DRIVER_ID", strDriverID);
                objDataWrapper.AddParameter("@DRIVER_NAME", strDriverName);
                int returnResult = 0;
                //if (TranLog)
                //	{
                //		if(TransactionLogRequired)
                //		{
                //			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                //				objMVRInfo.TransactLabel  = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                //SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                //strTranXML = objBuilder.GetTransactionLogXML(objMVRInfo);
                //Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                //objTransactionInfo.TRANS_TYPE_ID	=	1;
                //objTransactionInfo.RECORDED_BY		=	objMVRInfo.CREATED_BY;
                //objTransactionInfo.POLICY_ID 			=	objMVRInfo.POLICY_ID;
                //objTransactionInfo.POLICY_VER_TRACKING_ID =	objMVRInfo.POLICY_VERSION_ID; 
                //objTransactionInfo.CLIENT_ID		=	objMVRInfo.CUSTOMER_ID;
                //objTransactionInfo.TRANS_DESC		=	"Unmatched Mvr info is added for a driver violation";
                //objTransactionInfo.CHANGE_XML		=	strTranXML;
                //objTransactionInfo.CUSTOM_INFO		=	"";
                //Executing the query
                //returnResult	= objDataWrapper.ExecuteNonQuery (strStoredProc ,objTransactionInfo);
                //				returnResult	= objDataWrapper.ExecuteNonQuery (strStoredProc);
                //			}
                //		}
                //		else
                //		{
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                //		}

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }



        /// <summary>
        /// getting all violation detials for list of ssc volition code (retrived from mvr web service) 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">ashis kumar</param>


        public DataSet GetMVRFetchDetail(int nCustomerID)
        {

            try
            {

                DataSet dsMVRDetails = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", nCustomerID);
                dsMVRDetails = objDataWrapper.ExecuteDataSet("Proc_GetMVRFetchDetail");
                return dsMVRDetails;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }

        /// <summary>
        /// getting all violation detials for list of ssc volition code (retrived from mvr web service) 
        /// </summary>
        /// <param name="dtTemp"></param>
        /// <param name="modified_By">ashis kumar</param>
        public void UpdateMVRFetchDetail(int nCustomerID, DateTime FetchDate)
        {

            try
            {

                DataSet dsMVRDetails = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", nCustomerID);
                objDataWrapper.AddParameter("@FETCH_DATE", FetchDate);
                objDataWrapper.ExecuteNonQuery("Proc_UpdateMVRFetchDetail");

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }
        }


        /// <summary>
        /// created by praveen singh 
        /// getting the lobID on the basis of customer detail 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>


        public string Fun_GetLObID(int customerID, int appID, int appVersionID)
        {

            DataSet dsLOB = new DataSet();
            string LOBID = "0";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            // get the application LOB
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                dsLOB = objDataWrapper.ExecuteDataSet("Proc_GetApplicationLOB");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                LOBID = dsLOB.Tables[0].Rows[0][0].ToString();
                objDataWrapper.ClearParameteres();
                return LOBID;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Added by Mohit Agarwal 23-Jan-08 ITrack 1209
        public string Fun_GetLObID(int customerID, int polID, int polVersionID, string CalledFrom)
        {

            DataSet dsLOB = new DataSet();
            string LOBID = "0";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            // get the application LOB
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", polID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", polVersionID);
                objDataWrapper.AddParameter("@CALLED_FROM", CalledFrom);
                dsLOB = objDataWrapper.ExecuteDataSet("Proc_GetApplicationLOB");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                LOBID = dsLOB.Tables[0].Rows[0][0].ToString();
                objDataWrapper.ClearParameteres();
                return LOBID;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        //temporary method. TO BE REMOVED LATER -PRAVEEN SINGH FEB 8TH 2006
        /// <summary>
        /// getting temporary input xml from qq.
        /// </summary>
        /// <param name="customerID"></param>	 
        /// <param name="appID"></param>	 
        /// <param name="appVersionID"></param>	 
        public string GetTempInputXMLFromQQ(int customerID, int appID, int appVersionID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                string retVal = "";
                DataSet dsInputXMLFromQQ = new DataSet();
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objDataWrapper.AddParameter("@APP_ID", appID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
                dsInputXMLFromQQ = objDataWrapper.ExecuteDataSet("Proc_Get_QQXML");
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                retVal = dsInputXMLFromQQ.Tables[0].Rows[0][0].ToString();
                objDataWrapper.ClearParameteres();

                return retVal;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            { }

        }

        /// <summary>
        /// Return the status of the Application.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public string GetApplicationStatus(int CustomerID, int AppID, int AppVersionID)
        {
            string strStoredProc = "Proc_GetApplicationStatus";
            DataSet dsTemp;
            string result = "";

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@APP_ID", AppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    result = dsTemp.Tables[0].Rows[0][0].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }


        /// <summary>
        /// Gets whether this policy  is under new business or renewal
        /// </summary>
        /// <returns>"R" if this policy is under renewal, "N" if this is under New business</returns>
        public string GetNewBusinessOrRenewal(int customerID, int policyID, int policyVersionID)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            objDataWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objDataWrapper.AddParameter("@POLICY_ID", policyID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", policyVersionID);

            SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);

            objDataWrapper.ExecuteNonQuery("Proc_Get_POLICY_STATUS");

            int retVal = Convert.ToInt32(objSqlParameter.Value);

            if (retVal == 1)
            {
                return "N";
            }

            return "R";

        }

        #region Quote Details

        public DataSet GetQuoteDetails(int CustomerID, int AppID, int AppVersionID)
        {
            string strStoredProc = "Proc_GetQuoteDetails";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@APP_ID", AppID);
                objDataWrapper.AddParameter("@APP_VERSION_ID", AppVersionID);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }


        public DataSet GetPolicyQuoteDetails(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetPolicyQuoteDetails";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }


        #endregion


        #region Policy Details Section
        public string GetAllPolicyNumber(int CustomerID, int AgencyID, string PolicyNumber)
        {
            string strStoredProc = "Proc_GetAllPolicies";
            string returnResult = "";
            string LobName = "";
            string firstTemp = "";
            string temp = "";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@AGENCY_ID", AgencyID);
                objDataWrapper.AddParameter("@POLICY_NUMBER", PolicyNumber);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
                    {

                        LobName = dsTemp.Tables[0].Rows[count][0].ToString();
                        firstTemp = LobName.Substring(0, 1);

                        if (firstTemp != temp)
                        {
                            temp = firstTemp;

                            //Added by Asfa (29-May-2008) - iTrack #4121
                            if (returnResult != "")
                                returnResult = returnResult.Substring(0, returnResult.LastIndexOf(","));

                            if (firstTemp == "A")
                            {
                                if (count == 0)
                                    returnResult += "<b>Automobile </b>: <br>";
                                else
                                {
                                    //Commented by Asfa (29-May-2008) - iTrack #4121
                                    //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                    returnResult += "<br><b>Automobile </b>: <br>";
                                }
                            }

                            if (firstTemp == "C")
                            {
                                if (count == 0)
                                    returnResult += "<b>Motorcycle </b>: <br>";
                                else
                                {
                                    //Commented by Asfa (29-May-2008) - iTrack #4121
                                    //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                    returnResult += "<br><b>Motorcycle </b>: <br>";
                                }
                            }

                            if (firstTemp == "H")
                            {
                                if (count == 0)
                                    returnResult += "<b>Homeowner </b>: <br>";
                                else
                                {
                                    //Commented by Asfa (29-May-2008) - iTrack #4121
                                    //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                    returnResult += "<br><b>Homeowner </b>: <br>";
                                }
                            }

                            if (firstTemp == "R")
                            {
                                if (count == 0)
                                    returnResult += "<b>Rental </b>: <br>";
                                else
                                {
                                    //Commented by Asfa (29-May-2008) - iTrack #4121
                                    //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                    returnResult += "<br><b>Rental </b>: <br>";
                                }
                            }

                            if (firstTemp == "W")
                            {
                                if (count == 0)
                                    returnResult += "<b>Watercraft </b>: <br>";
                                else
                                {
                                    //Commented by Asfa (29-May-2008) - iTrack #4121
                                    //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                    returnResult += "<br><b>Watercraft </b>: <br>";
                                }
                            }
                        }


                        returnResult += dsTemp.Tables[0].Rows[count][0].ToString();
                        if (count != (dsTemp.Tables[0].Rows.Count - 1))
                            returnResult += ", ";

                    }
                }
                else
                    returnResult = "";

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }


        public string GetEligiblePolicyNumber(int CustomerID, int AgencyID, int LobID, string PolicyNumber)
        {
            string strStoredProc = "Proc_GetEligiblePolicies";
            string returnResult = "";
            string LobName = "";
            string firstTemp = "";
            string temp = "";
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@AGENCY_ID", AgencyID);
                objDataWrapper.AddParameter("@POLICY_LOB", LobID);
                objDataWrapper.AddParameter("@POLICY_NUMBER", PolicyNumber);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);


                if (dsTemp != null)
                {
                    if (dsTemp.Tables[0].Rows.Count > 0)
                    {
                        for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
                        {
                            LobName = dsTemp.Tables[0].Rows[count][0].ToString();
                            firstTemp = LobName.Substring(0, 1);

                            if (firstTemp != temp)
                            {
                                temp = firstTemp;

                                //Added by Asfa (29-May-2008) - iTrack #4121
                                if (returnResult != "")
                                    returnResult = returnResult.Substring(0, returnResult.LastIndexOf(","));

                                if (firstTemp == "A")
                                {
                                    if (count == 0)
                                        returnResult += "<b>Automobile </b>: <br>";
                                    else
                                    {
                                        //Commented by Asfa (29-May-2008) - iTrack #4121
                                        //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                        returnResult += "<br><b>Automobile </b>: <br>";
                                    }
                                }

                                if (firstTemp == "C")
                                {
                                    if (count == 0)
                                        returnResult += "<b>Motorcycle </b>: <br>";
                                    else
                                    {
                                        //Commented by Asfa (29-May-2008) - iTrack #4121
                                        //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                        returnResult += "<br><b>Motorcycle </b>: <br>";
                                    }
                                }

                                if (firstTemp == "H")
                                {
                                    if (count == 0)
                                        returnResult += "<b>Homeowner </b>: <br>";
                                    else
                                    {
                                        //Commented by Asfa (29-May-2008) - iTrack #4121
                                        //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                        returnResult += "<br><b>Homeowner </b>: <br>";
                                    }
                                }

                                if (firstTemp == "R")
                                {
                                    if (count == 0)
                                        returnResult += "<b>Rental </b>: <br>";
                                    else
                                    {
                                        //Commented by Asfa (29-May-2008) - iTrack #4121
                                        //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                        returnResult += "<br><b>Rental </b>: <br>";
                                    }
                                }

                                if (firstTemp == "W")
                                {
                                    if (count == 0)
                                        returnResult += "<b>Watercraft </b>: <br>";
                                    else
                                    {
                                        //Commented by Asfa (29-May-2008) - iTrack #4121
                                        //returnResult = returnResult.Substring(0,(returnResult.Length - 1));
                                        returnResult += "<br><b>Watercraft </b>: <br>";
                                    }
                                }
                            }

                            returnResult += dsTemp.Tables[0].Rows[count][0].ToString();

                            if (count != (dsTemp.Tables[0].Rows.Count - 1))
                                returnResult += ", ";
                        }
                    }

                    else
                        returnResult = "";
                }
                else
                    returnResult = "";

                return returnResult;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }


        public int GetAgencyId(string AgencyCode)
        {
            //CODE COMMETED BY PAWAN
            //string		strStoredProc	=	"Proc_GetAgencyIDFromCode";
            string strStoredProc = "Proc_GetAgencyID";
            int returnResult = 0;
            DataSet dsTemp;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@AGENCY_CODE", AgencyCode);

                dsTemp = new DataSet();
                dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    returnResult = int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                }

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
            }
        }




        #endregion


        #region MVR Policies Windows Service

        /// <summary>
        /// Returns the policies which are used by MVR Windows Service.
        /// </summary>
        /// <returns></returns>
        public DataSet GetMVRWinServicePolicies()
        {
            try
            {

                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetMVRWinServicePolicies");

                return dsTemp;
            }
            catch (Exception exc)
            {
                throw (exc);
            }

        }


        /// <summary>
        /// Get the Driver list on the basis of customer, policy and policy version.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <returns></returns>
        private DataSet GetPolicyDriverList(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                DataSet dsDriver = new DataSet();
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                dsDriver = objDataWrapper.ExecuteDataSet("Proc_GetPolicyDriverList");
                objDataWrapper.ClearParameteres();

                return dsDriver;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }


        /// <summary>
        /// Get the Policy Driver List based on the Specific LOB
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public DataSet GetPolicyDriverList(int CustomerID, int PolicyID, int PolicyVersionID, int LOBID)
        {
            try
            {

                DataSet dsTemp = new DataSet();

                if (LOBID == 2 || LOBID == 3)   //for Automobile and Motorcycle
                {
                    dsTemp = GetPolicyDriverList(CustomerID, PolicyID, PolicyVersionID);
                }
                else //if (LOBID == 4)
                {

                    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                    objDataWrapper.ClearParameteres();
                    objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                    objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                    dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyWaterCraftOperatorList");
                    objDataWrapper.ClearParameteres();
                }
                //				else
                //				{
                //					dsTemp = null;
                //				}

                return dsTemp;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }


        /// <summary>
        /// Verfify the Rules, Update the Discount if needed and also insert the Diary Entry.
        /// Modified BY	: Anurag verma
        /// Modified On	:	14/03/2007
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="DriverID"></param>
        /// <param name="LOBID"></param>
        /// <returns></returns>
        private int UpdateRulesDiscountSendDiary(int CustomerID, int PolicyID, int PolicyVersionID, int DriverID, int LOBID)
        {
            ClsDiary objDiary = new ClsDiary();
            TodolistInfo todolist = new TodolistInfo();
            string strStoredProc = "Proc_SendDiaryForMVRService";
            int returnResult = 0;
            string sdPts = "N";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objDataWrapper.AddParameter("@DRIVER_ID", DriverID);
                objDataWrapper.AddParameter("@LOB_ID", LOBID);

                //Adding an output paramater for capturing SD points
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@SD_PTS", null, SqlDbType.Char, ParameterDirection.Output, 1);

                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                //retrieving output paramater value
                if (objSqlParameter != null)
                    if (objSqlParameter.Value.ToString() != "")
                        sdPts = objSqlParameter.Value.ToString();

                //if the value is Y then insert a diary entry
                if (sdPts.Equals("Y"))
                {
                    todolist.MODULE_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;
                    todolist.LISTTYPEID = (int)ClsDiary.enumDiaryType.FOLLOW_UPS;
                    todolist.RECDATE = DateTime.Now;
                    todolist.SUBJECTLINE = "MVR VIOLATIONS RETRIVED";
                    todolist.LISTOPEN = "Y";
                    todolist.POLICY_ID = PolicyID;
                    todolist.POLICY_VERSION_ID = PolicyVersionID;
                    todolist.CUSTOMER_ID = customerID;
                    todolist.LOB_ID = LOBID;

                    objDiary.DiaryEntryfromSetup(todolist);
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        /// <summary>
        /// Verify the Rules, Updates the discount if necessary and also send the Diary Entry.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="LOBID"></param>
        public void RulesDiscountSendDiary(int CustomerID, int PolicyID, int PolicyVersionID, int LOBID)
        {
            try
            {
                int DriverID = 0;
                int Result = 0;
                DataSet dsTemp = GetPolicyDriverList(CustomerID, PolicyID, PolicyVersionID);

                if (dsTemp != null)
                {
                    if (dsTemp.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTemp.Tables[0].Rows)
                        {
                            DriverID = int.Parse(dr["DRIVER_ID"].ToString());
                            Result = UpdateRulesDiscountSendDiary(CustomerID, PolicyID, PolicyVersionID, DriverID, LOBID);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// Update the Policy MVR Windows Service Flag.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public int UpdatePolicyMVRFlag(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_UpdatePolicyMVRFlag";
            int returnResult = 0;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }


        /// <summary>
        /// Insert the Web Service Request Log.
        /// </summary>
        /// <param name="objLog"></param>
        /// <returns></returns>
        public int InsertRequestLog(Cms.Model.Maintenance.ClsRequestResponseLogInfo objLog)
        {
            string strStoredProc = "Proc_InsertRequestResponseLog";
            int returnResult = 0;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapper.ClearParameteres();

                if (objLog.CUSTOMER_ID != Convert.ToInt32(null))
                    objDataWrapper.AddParameter("@CUSTOMER_ID", objLog.CUSTOMER_ID);
                else
                    objDataWrapper.AddParameter("@CUSTOMER_ID", null);

                if (objLog.POLICY_ID != Convert.ToInt32(null))
                    objDataWrapper.AddParameter("@POLICY_ID", objLog.POLICY_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_ID", null);


                if (objLog.POLICY_VERSION_ID != Convert.ToInt32(null))
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objLog.POLICY_VERSION_ID);
                else
                    objDataWrapper.AddParameter("@POLICY_VERSION_ID", null);


                if (objLog.CATEGORY_ID != Convert.ToInt32(null))
                    objDataWrapper.AddParameter("@CATEGORY_ID", objLog.CATEGORY_ID);
                else
                    objDataWrapper.AddParameter("@CATEGORY_ID", null);


                if (objLog.SERVICE_VENDOR != null)
                    objDataWrapper.AddParameter("@SERVICE_VENDOR", objLog.SERVICE_VENDOR);
                else
                    objDataWrapper.AddParameter("@SERVICE_VENDOR", null);


                if (objLog.CREATED_BY != Convert.ToInt32(null))
                    objDataWrapper.AddParameter("@CREATED_BY", objLog.CREATED_BY);
                else
                    objDataWrapper.AddParameter("@CREATED_BY", null);


                if (objLog.REQUEST_DATETIME != Convert.ToDateTime(null))
                    objDataWrapper.AddParameter("@REQUEST_DATETIME", objLog.REQUEST_DATETIME);
                else
                    objDataWrapper.AddParameter("@REQUEST_DATETIME", null);


                if (objLog.RESPONSE_DATETIME != Convert.ToDateTime(null))
                {
                    objDataWrapper.AddParameter("@RESPONSE_DATETIME", objLog.RESPONSE_DATETIME);
                }
                else
                    objDataWrapper.AddParameter("@RESPONSE_DATETIME", null);

                if (objLog.IIX_REQUEST != null)
                    objDataWrapper.AddParameter("@IIX_REQUEST", objLog.IIX_REQUEST);
                else
                    objDataWrapper.AddParameter("@IIX_REQUEST", null);

                if (objLog.IIX_RESPONSE != null)
                    objDataWrapper.AddParameter("@IIX_RESPONSE", objLog.IIX_RESPONSE);
                else
                    objDataWrapper.AddParameter("@IIX_RESPONSE", null);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@ROW_ID", SqlDbType.Int, ParameterDirection.Output);

                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                if (objSqlParameter != null)
                    objLog.ROW_ID = int.Parse(objSqlParameter.Value.ToString());

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }

        public static string GetIIXBillCode(string ProductCode)
        {
            string strBillCode = "000";
            try
            {

                DataSet dsTemp;
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@PRODUCT_CODE", ProductCode);
                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetIIXBillCode");
                objDataWrapper.ClearParameteres();
                if (dsTemp.Tables[0].Rows.Count > 0)
                    strBillCode = dsTemp.Tables[0].Rows[0][0].ToString();
                return strBillCode;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        /// <summary>
        /// Update the Web Service Response Log.
        /// </summary>
        /// <param name="objLog"></param>
        /// <returns></returns>
        public int UpdateRequestLog(Cms.Model.Maintenance.ClsRequestResponseLogInfo objLog)
        {
            string strStoredProc = "Proc_UpdateRequestResponseLog";
            int returnResult = 0;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapper.ClearParameteres();

                objDataWrapper.AddParameter("@ROW_ID", objLog.ROW_ID);

                if (objLog.RESPONSE_DATETIME != Convert.ToDateTime(null))
                {
                    objDataWrapper.AddParameter("@RESPONSE_DATETIME", objLog.RESPONSE_DATETIME);
                }
                else
                {
                    objDataWrapper.AddParameter("@RESPONSE_DATETIME", null);
                }

                if (objLog.IIX_REQUEST != null && objLog.IIX_REQUEST != "")
                    objDataWrapper.AddParameter("@IIX_REQUEST", objLog.IIX_REQUEST);
                else
                    objDataWrapper.AddParameter("@IIX_REQUEST", null);

                if (objLog.IIX_RESPONSE != null && objLog.IIX_RESPONSE != "")
                    objDataWrapper.AddParameter("@IIX_RESPONSE", objLog.IIX_RESPONSE);
                else
                    objDataWrapper.AddParameter("@IIX_RESPONSE", null);


                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }
        #endregion

        #region LOAD DEFAULT RULES XML
        public string GetRulesDefaultXml()
        {
            XmlDocument DataDoc = new XmlDocument();
            string strXmlFilePath = ClsCommon.GetKeyValueWithIP("RULES_DEFAULT");
            //DataDoc.Load("D://RULES_DEFAULT.XML");
            DataDoc.Load(strXmlFilePath);
            string strRulesXml = DataDoc.InnerXml;
            strRulesXml = strRulesXml.Replace("\"", "'");
            return strRulesXml;
        }
        #endregion

        /// <summary>
        /// Gets a  Location Ids against an application for UM 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Location Ids  as a carat separated string . if no location exist then it returns -1</returns>
        public static string GetUMLocationIDs(int customerID, int appID, int appVersionID)
        {
            string strStoredProc = "Proc_GetUMLocationIDs";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@APP_ID", appID);
            objWrapper.AddParameter("@APP_VERSION_ID", appVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strLocationID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strLocationID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strLocationID = strLocationID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strLocationID;
        }

        /// <summary>
        /// Gets a  Location Ids against a policy for UM 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVersionID"></param>
        /// <returns>Location Ids  as a carat separated string . if no location exist then it returns -1</returns>
        public static string Get_PolUMLocationIDs(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetUMLocationIDs_Pol";

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@CUSTOMER_ID", customerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
            string strLocationID = "-1";
            if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                int intCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < intCount; i++)
                {
                    if (i == 0)
                    {
                        strLocationID = ds.Tables[0].Rows[i][0].ToString();
                    }
                    else
                    {
                        strLocationID = strLocationID + '^' + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            return strLocationID;
        }

        /// <summary>
        /// Added by Praveen Kasana to parse Rule XML
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="appID"></param>
        /// <param name="appVerId"></param>
        /// <param name="objWrapper"></param>
        /// <returns></returns>
        public string FetchRuleReferedStatus(int customerID, int appID, int appVerId, DataWrapper objWrapper)
        {
            string status = "";
            string strStoredProc = "Proc_GetRuleXml";
            string ruleXML = "";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objWrapper.AddParameter("@APP_ID", appID);
                objWrapper.AddParameter("@APP_VERSION_ID", appVerId);
                DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
                if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                        ruleXML = ds.Tables[0].Rows[0][0].ToString();
                }
                if (ruleXML != null && ruleXML != "")
                {
                    //Find ReferdStatus From Rulke XML
                    ruleXML = "<ROOT>" + ruleXML + "</ROOT>"; //Make Valid XML
                    ruleXML = ruleXML.Replace("\"", "'");
                    ruleXML = ruleXML.Replace("&", "");
                    ruleXML = ruleXML.Replace("&gt;", "");
                    ruleXML = ruleXML.Replace("&GT;", "");
                    ruleXML = ruleXML.Replace("&LT;", "");
                    ruleXML = ruleXML.Replace("&lt;", "");
                    ruleXML = ruleXML.Replace("&AMP;", "");
                    ruleXML = ruleXML.Replace("&amp;", "");

                    ruleXML = ruleXML.Replace("stylesheet'>", "stylesheet'></LINK>");
                    ruleXML = ruleXML.Replace("charset=utf-16'>", "charset=utf-16'></META>");
                    ruleXML = ruleXML.ToUpper();
                    ruleXML = ruleXML.Replace("XMLNS", "xmlns");

                    XmlDocument xmlRuleDoc = new XmlDocument();
                    xmlRuleDoc.LoadXml(ruleXML);
                    if (xmlRuleDoc.SelectSingleNode("ROOT/SPAN") != null)
                    {
                        if (xmlRuleDoc.SelectSingleNode("ROOT/SPAN/REFEREDSTATUS") != null)
                        {
                            status = xmlRuleDoc.SelectSingleNode("ROOT/SPAN/REFEREDSTATUS").InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
            return status;
        }

        //Added by Sibin
        public void UpdateLastVisitedPageEntry(string strCalledFrom, string page_Info, int userID, string system_ID)
        {
            string strStoredProc = "Proc_UpdateUserLastVisitedPages";

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@PAGE_INFO", page_Info);
                objDataWrapper.AddParameter("@CALLED_FROM", strCalledFrom);
                objDataWrapper.AddParameter("@USER_ID", userID);
                objDataWrapper.AddParameter("@USER_SYSTEM_ID", system_ID);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }


        public DataSet GetLastVisitedPageEntry(int userID, string system_Id)
        {
            string strStoredProc = "Proc_GetUserLastVisitedPages";

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@USER_ID", userID);
                objDataWrapper.AddParameter("@USER_SYSTEM_ID", system_Id);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }

        #region Add Broker Added by lalit For Remuneration

        public static DataTable GetCommissionType(string Tran_Type, int LOB_ID)
        {

            DataTable dt = null;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@Tran_Type", Tran_Type);
                objDataWrapper.AddParameter("@LOB_ID", LOB_ID);
                objDataWrapper.AddParameter("@lang_id", BlCommon.ClsCommon.BL_LANG_ID);
                dt = objDataWrapper.ExecuteDataSet("proc_getCommissionType").Tables[0];
            }
            catch
            {

            }
            return dt;
        }


        public DataSet GetBrokerList(int customer_id, int policy_id, int Poversion_id)
        {
            string strStoredProc = "Proc_GetBrokerList";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
                objDataWrapper.AddParameter("@POLICY_ID", policy_id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Poversion_id);
                objDataWrapper.AddParameter("@RENTYPE", "RL");
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }
        public int AddUpdateBroker(ArrayList alpremunerationobj)
        {
            int returnValue = 0;
            for (int i = 0; i < alpremunerationobj.Count; i++)
            {
                ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo = (ClsPolicyRemunerationInfo)alpremunerationobj[i];
                if (objnewPolicyRemunerationInfo.ACTION == "I")
                {
                    //Insert New Commssion %

                    if (objnewPolicyRemunerationInfo.RequiredTransactionLog)
                    {
                        objnewPolicyRemunerationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddPolicyRemuneration.aspx.resx");
                        returnValue = objnewPolicyRemunerationInfo.AddPlicyRemuneration();

                    }

                }
                else if (objnewPolicyRemunerationInfo.ACTION == "U")
                {
                    //Update Old Commssion %
                    if (objnewPolicyRemunerationInfo.RequiredTransactionLog)
                    {
                        objnewPolicyRemunerationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddPolicyRemuneration.aspx.resx");
                        returnValue = objnewPolicyRemunerationInfo.UpdatePlicyRemuneration();

                    }

                }


            }


            return returnValue;

        }//Add update Broker 


        public int AddRemuneration(ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo)
        {
            int returnValue = 0;
            if (objnewPolicyRemunerationInfo.RequiredTransactionLog)
            {
                objnewPolicyRemunerationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddRemunerationDetails.aspx.resx");
                returnValue = objnewPolicyRemunerationInfo.AddPlicyRemuneration();
            }
            return returnValue;

        }//Add  Broker 

        public int UpdateRemuneration(ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo)
        {
            int returnValue = 0;
            if (objnewPolicyRemunerationInfo.RequiredTransactionLog)
            {
                objnewPolicyRemunerationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddRemunerationDetails.aspx.resx");
                returnValue = objnewPolicyRemunerationInfo.UpdatePlicyRemuneration();
            }
            return returnValue;

        }//Add  Broker 

        //public int AddBroker(ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo)
        //{


        //    //string strTranXML="";
        //    //SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //    //int returnresult = 0;
        //    //string strStoredProc = "Proc_InsertRemuneration";

        //    //DateTime RecordDate = DateTime.Now;

        //    ////Set transaction label in the new object, if required
        //    //if (this.boolTransactionRequired)
        //    //{
        //    //    objnewPolicyRemunerationInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\AddPolicyRemuneration.aspx.resx");
        //    //}

        //    //DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
        //    //try
        //    //{

        //    //    objDataWrapper.AddParameter("@CUSTOMER_ID", objnewPolicyRemunerationInfo.CUSTOMER_ID);
        //    //    objDataWrapper.AddParameter("@POLICY_ID", objnewPolicyRemunerationInfo.POLICY_ID);
        //    //    objDataWrapper.AddParameter("@POLICY_VERSION_ID", objnewPolicyRemunerationInfo.POLICY_VERSION_ID);
        //    //    objDataWrapper.AddParameter("@BROKER_ID", objnewPolicyRemunerationInfo.BROKER_ID);
        //    //    objDataWrapper.AddParameter("@COMMISSION_PERCENT", objnewPolicyRemunerationInfo.COMMISSION_PERCENT);
        //    //    objDataWrapper.AddParameter("@COMMISSION_TYPE", objnewPolicyRemunerationInfo.COMMISSION_TYPE);
        //    //    objDataWrapper.AddParameter("@IS_ACTIVE", objnewPolicyRemunerationInfo.IS_ACTIVE);
        //    //    objDataWrapper.AddParameter("@CREATED_BY", objnewPolicyRemunerationInfo.CREATED_BY);
        //    //    //If Transaction  Required
        //    //    if (TransactionLogRequired)
        //    //    {
        //    //        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();                    
        //    //        objTransactionInfo.TRANS_TYPE_ID = 1;
        //    //        objTransactionInfo.RECORDED_BY = objnewPolicyRemunerationInfo.CREATED_BY;
        //    //        objTransactionInfo.POLICY_ID = objnewPolicyRemunerationInfo.POLICY_ID;
        //    //        objTransactionInfo.POLICY_VER_TRACKING_ID = objnewPolicyRemunerationInfo.POLICY_VERSION_ID;
        //    //        objTransactionInfo.CLIENT_ID = objnewPolicyRemunerationInfo.CUSTOMER_ID;


        //    //            strTranXML = objBuilder.GetTransactionLogXML(objnewPolicyRemunerationInfo);
        //    //            objTransactionInfo.TRANS_DESC = "Broker Added";

        //    //            objTransactionInfo.CHANGE_XML = strTranXML;
        //    //            objTransactionInfo.CUSTOM_INFO = "";
        //    //            returnresult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);


        //    //    }
        //    //        //If Transaction Not Required
        //    //    else
        //    //    {
        //    //        returnresult = objDataWrapper.ExecuteNonQuery(strStoredProc);
        //    //    }
        //    //    objDataWrapper.ClearParameteres();
        //    //    objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

        //    //    return returnresult;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw (ex);
        //    //}
        //    //finally
        //    //{
        //    //    if (objDataWrapper != null) objDataWrapper.Dispose();
        //    //}


        //}

        //public int UpdateBroker(ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo)
        //{



        //    //int returnresult = 0;
        //    //string strStoredProc = "Proc_UpdateRemuneration";

        //    //DateTime RecordDate = DateTime.Now;
        //    ////Set transaction label in the new object, if required
        //    ////if (this.boolTransactionRequired)
        //    ////{
        //    ////    //objnewPolicyRemunerationInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\AddPolicyRemuneration.aspx.resx");
        //    ////}

        //    //DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);//, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
        //    //try
        //    //{
        //    //    objDataWrapper.AddParameter("@REMUNERATION_ID", objnewPolicyRemunerationInfo.REMUNERATION_ID.CurrentValue);
        //    //    //objDataWrapper.AddParameter("@CUSTOMER_ID", objnewPolicyRemunerationInfo.CUSTOMER_ID);
        //    //    //objDataWrapper.AddParameter("@POLICY_ID", objnewPolicyRemunerationInfo.POLICY_ID);
        //    //    //objDataWrapper.AddParameter("@POLICY_VERSION_ID", objnewPolicyRemunerationInfo.POLICY_VERSION_ID);
        //    //    //objDataWrapper.AddParameter("@BROKER_ID", objnewPolicyRemunerationInfo.BROKER_ID);
        //    //    objDataWrapper.AddParameter("@COMMISSION_PERCENT", objnewPolicyRemunerationInfo.COMMISSION_PERCENT.CurrentValue);
        //    //    objDataWrapper.AddParameter("@COMMISSION_TYPE", objnewPolicyRemunerationInfo.COMMISSION_TYPE.CurrentValue);
        //    //    objDataWrapper.AddParameter("@IS_ACTIVE", objnewPolicyRemunerationInfo.IS_ACTIVE.CurrentValue);
        //    //    objDataWrapper.AddParameter("@MODIFIED_BY", objnewPolicyRemunerationInfo.MODIFIED_BY.CurrentValue);
        //    //    //objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objnewPolicyRemunerationInfo.LAST_UPDATED_DATETIME);

        //    //    returnresult = objDataWrapper.ExecuteNonQuery(strStoredProc);

        //    //    if (returnresult > 0)
        //    //    {


        //    //    }
        //    //    return returnresult;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw (ex);
        //    //}
        //    //finally
        //    //{
        //    //    if (objDataWrapper != null) objDataWrapper.Dispose();
        //    //}


        //}
        public int DeleteBroker(ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo)//ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo)
        {

            int returnValue = 0;

            if (objnewPolicyRemunerationInfo.RequiredTransactionLog)
            {
                objnewPolicyRemunerationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddPolicyRemuneration.aspx.resx");
                returnValue = objnewPolicyRemunerationInfo.DeletePlicyRemuneration();

            }
            return returnValue;
            //int returnvalue = 0;
            //string strStoredProc = "Proc_DeleteRemuneration";

            //DateTime RecordDate = DateTime.Now;

            //DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);//, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            //try
            //{
            //    objDataWrapper.AddParameter("@REMUNERATION_ID", RemunerationID);
            //    returnvalue = objDataWrapper.ExecuteNonQuery(strStoredProc);
            //    if (returnvalue > 0)
            //    {

            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}
            //finally
            //{
            //    if (objDataWrapper != null) objDataWrapper.Dispose();
            //}


            //return returnvalue;
        }

        public string GetPolicyLavelCommission(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string Pol_Level_DefaulValues = string.Empty;
            try
            {
                DataSet dsTemp = new DataSet();

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);

                dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyInformation");
                if (dsTemp.Tables.Count > 0)
                    if (dsTemp.Tables[0].Rows.Count > 0)
                    {
                        if (dsTemp.Tables[0].Rows[0]["POLICY_LEVEL_COMISSION"] != null)
                        {
                            Pol_Level_DefaulValues = dsTemp.Tables[0].Rows[0]["POLICY_LEVEL_COMISSION"].ToString();
                        }
                        if (dsTemp.Tables[0].Rows[0]["AGENCY_ID"] != null)
                        {
                            Pol_Level_DefaulValues = Pol_Level_DefaulValues + "^" + dsTemp.Tables[0].Rows[0]["AGENCY_ID"].ToString();

                        }
                    }
                return Pol_Level_DefaulValues;

            }
            catch (Exception exc)
            { throw (exc); }
            finally
            { }
        }//Get The Policy Level Commission

        #endregion

        #region Discounts and Surcharge


        public static DataTable getDiscountSurchargeList(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, string Level)
        {
            DataTable dt = new DataTable();

            try
            {
                string strStoredProc = "Proc_GetDiscountSurchargeList";
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objWrapper.AddParameter("@Level", Level);

                dt = objWrapper.ExecuteDataSet(strStoredProc).Tables[0];
                if (dt != null)
                    return dt;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }


        }   //Get DisCount Surcharge List From Master

        public DataSet GetPolicyDiscountSurcharge(int Customer_ID, int Policy_Id, int Policy_version_ID, String CalledFrom, int Risk_id)
        {
            DataSet ds = null;
            try
            {
                ClsDiscountSurchargeInfo objClsDiscountSurchargeInfo = new ClsDiscountSurchargeInfo();
                ds = objClsDiscountSurchargeInfo.GetPolicyDiscountSurcharge(Customer_ID, Policy_Id, Policy_version_ID, CalledFrom, Risk_id);
                if (ds != null)
                    return ds;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }   //Fetch Discount Surcharge Percentage from POL_DISCOUNT_SURCHARGE TABLE

        public int AddUpdateDiscountSurcharge(ArrayList ArlObjDiscountSurchargeInfo, string oldxml, int CustomerId, int PolicyId, int PolicyVersionid, int RecordedBy)
        {
            int returnval = 0;
            XmlElement root = null;
            XmlDocument xmldoc = new XmlDocument();
            StringBuilder sbTranXml = new StringBuilder();

            sbTranXml.Append("<root>");
            if (oldxml != "")
            {
                xmldoc.LoadXml(oldxml);
                root = xmldoc.DocumentElement;  //hold the root element
            }

            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

            for (int i = 0; i < ArlObjDiscountSurchargeInfo.Count; i++)
            {
                string strTranXML = "";
                ClsDiscountSurchargeInfo objnewDiscountSurchargeInfo = (ClsDiscountSurchargeInfo)ArlObjDiscountSurchargeInfo[i];

                if (objnewDiscountSurchargeInfo.ACTION == "I")
                {
                    //Insert New Discount Percentage %

                    if (objnewDiscountSurchargeInfo.RequiredTransactionLog)
                    {
                        objnewDiscountSurchargeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddDiscountSurcharge.aspx.resx");
                        returnval = objnewDiscountSurchargeInfo.AddDiscountPercentage();
                        strTranXML = objnewDiscountSurchargeInfo.GenerateTransactionLogXML_New(true);
                        sbTranXml.Append(strTranXML);
                    }

                }
                else if (objnewDiscountSurchargeInfo.ACTION == "U")
                {
                    //Update New Discount Percentage %
                    if (objnewDiscountSurchargeInfo.RequiredTransactionLog)
                    {
                        objnewDiscountSurchargeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddDiscountSurcharge.aspx.resx");

                        returnval = objnewDiscountSurchargeInfo.UpdateDiscountPercentage();
                        strTranXML = objnewDiscountSurchargeInfo.GetDiscountSurchargeTranXML(objnewDiscountSurchargeInfo, oldxml, objnewDiscountSurchargeInfo.DISCOUNT_ROW_ID.CurrentValue, root);
                        if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            sbTranXml.Append(strTranXML);
                    }

                }


            }

            sbTranXml.Append("</root>");

            if (sbTranXml.ToString() != "<root></root>")// || strCustomInfo!="")
            {
                //ClsDiscountSurchargeInfo objnewDiscountSurchargeInfo = new ClsDiscountSurchargeInfo();
                if (ArlObjDiscountSurchargeInfo.Count > 0)
                {
                    //String TransacDesc = "";
                    ClsDiscountSurchargeInfo objnew = new ClsDiscountSurchargeInfo();

                    int Tranreturnval = objnew.SaveTransaction(sbTranXml.ToString(), CustomerId, PolicyId, PolicyVersionid, RecordedBy, "");

                    //sbTranXml, objnewDiscountSurchargeInfo.CUSTOMER_ID.CurrentValue, objnewDiscountSurchargeInfo.POLICY_ID.CurrentValue, objnewDiscountSurchargeInfo.POLICY_VERSION_ID.CurrentValue, objnewDiscountSurchargeInfo.CREATED_BY.CurrentValue,TransacDesc
                }
            }

            //if (ObjMaritimeInfo.RequiredTransactionLog)
            //{
            //    ObjMaritimeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddMeriTimeInfo.aspx.resx");

            //    returnValue = ObjMaritimeInfo.AddMariTimeData();

            //}//if (ObjMaritimeInfo.RequiredTransactionLog)


            return returnval;
        }   //Add OR Update Discount Surcharge Percentage In POL_DISCOUNT_SURCHARGE TABLE

        public int DeleteDiscountSurchargePercent(ClsDiscountSurchargeInfo objDiscountSurchargeInfo, int DiscountRowId)
        {
            int returnValue = 0;
            if (objDiscountSurchargeInfo.RequiredTransactionLog)
            {
                objDiscountSurchargeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddDiscountSurcharge.aspx.resx");
                returnValue = objDiscountSurchargeInfo.DeleteDiscountSurchargePercent(DiscountRowId);
            }
            return returnValue;
        }






        #endregion
        # region clauses
        public int UpdatePolClauses(ArrayList ArlObjClausesInfo, string oldxml, int CustomerId, int PolicyId, int PolicyVersionid, int RecordedBy)
        {
            int returnval = 0;
            XmlElement root = null;
            XmlDocument xmldoc = new XmlDocument();
            StringBuilder sbTranXml = new StringBuilder();

            sbTranXml.Append("<root>");
            if (oldxml != "")
            {
                xmldoc.LoadXml(oldxml);
                root = xmldoc.DocumentElement;  //hold the root element
            }


            for (int i = 0; i < ArlObjClausesInfo.Count; i++)
            {
                ClsPolicyClauseInfo objPolicyClauseInfo = (ClsPolicyClauseInfo)ArlObjClausesInfo[i];
                string strTranXML = "";
                if (objPolicyClauseInfo.RequiredTransactionLog)
                {
                    objPolicyClauseInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyClauses.aspx.resx");
                    returnval = objPolicyClauseInfo.UpdatePolicyClauses();
                    strTranXML = objPolicyClauseInfo.GetClausesTranXML(objPolicyClauseInfo, oldxml, objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue, root);
                    if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                        sbTranXml.Append(strTranXML);
                }
            }

            sbTranXml.Append("</root>");

            if (sbTranXml.ToString() != "<root></root>")// || strCustomInfo!="")
            {
                //ClsDiscountSurchargeInfo objnewDiscountSurchargeInfo = new ClsDiscountSurchargeInfo();
                if (ArlObjClausesInfo.Count > 0)
                {
                    //String TransacDesc = "";
                    ClsPolicyClauseInfo objnew = new ClsPolicyClauseInfo();

                    int Tranreturnval = objnew.SaveTransaction(sbTranXml.ToString(), CustomerId, PolicyId, PolicyVersionid, RecordedBy, "");

                    //sbTranXml, objnewDiscountSurchargeInfo.CUSTOMER_ID.CurrentValue, objnewDiscountSurchargeInfo.POLICY_ID.CurrentValue, objnewDiscountSurchargeInfo.POLICY_VERSION_ID.CurrentValue, objnewDiscountSurchargeInfo.CREATED_BY.CurrentValue,TransacDesc
                }
            }

            //if (ObjMaritimeInfo.RequiredTransactionLog)
            //{
            //    ObjMaritimeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddMeriTimeInfo.aspx.resx");

            //    returnValue = ObjMaritimeInfo.AddMariTimeData();

            //}//if (ObjMaritimeInfo.RequiredTransactionLog)


            return returnval;
        }
        public int DeactivatePolClauses(ArrayList ArlObjClausesInfo, string oldxml, int CustomerId, int PolicyId, int PolicyVersionid, int RecordedBy)
        {
            int returnval = 0;
            XmlElement root = null;
            XmlDocument xmldoc = new XmlDocument();
            StringBuilder sbTranXml = new StringBuilder();

            sbTranXml.Append("<root>");
            if (oldxml != "")
            {
                xmldoc.LoadXml(oldxml);
                root = xmldoc.DocumentElement;  //hold the root element
            }


            for (int i = 0; i < ArlObjClausesInfo.Count; i++)
            {
                ClsPolicyClauseInfo objPolicyClauseInfo = (ClsPolicyClauseInfo)ArlObjClausesInfo[i];
                string strTranXML = "";
                if (objPolicyClauseInfo.RequiredTransactionLog)
                {
                    objPolicyClauseInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyClauses.aspx.resx");
                    returnval = objPolicyClauseInfo.DeactivatePolClauses();
                    strTranXML = objPolicyClauseInfo.GetClausesTranXML(objPolicyClauseInfo, oldxml, objPolicyClauseInfo.POL_CLAUSE_ID.CurrentValue, root);
                    if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                        sbTranXml.Append(strTranXML);
                }
            }

            sbTranXml.Append("</root>");

            if (sbTranXml.ToString() != "<root></root>")// || strCustomInfo!="")
            {
                //ClsDiscountSurchargeInfo objnewDiscountSurchargeInfo = new ClsDiscountSurchargeInfo();
                if (ArlObjClausesInfo.Count > 0)
                {
                    //String TransacDesc = "";
                    ClsPolicyClauseInfo objnew = new ClsPolicyClauseInfo();

                    int Tranreturnval = objnew.SaveTransaction(sbTranXml.ToString(), CustomerId, PolicyId, PolicyVersionid, RecordedBy, "");

                    //sbTranXml, objnewDiscountSurchargeInfo.CUSTOMER_ID.CurrentValue, objnewDiscountSurchargeInfo.POLICY_ID.CurrentValue, objnewDiscountSurchargeInfo.POLICY_VERSION_ID.CurrentValue, objnewDiscountSurchargeInfo.CREATED_BY.CurrentValue,TransacDesc
                }
            }

            //if (ObjMaritimeInfo.RequiredTransactionLog)
            //{
            //    ObjMaritimeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddMeriTimeInfo.aspx.resx");

            //    returnValue = ObjMaritimeInfo.AddMariTimeData();

            //}//if (ObjMaritimeInfo.RequiredTransactionLog)


            return returnval;
        }


        public int AddPolClauses(ClsPolicyClauseInfo objPolicyClauseInfo)
        {
            int returnval = 0;

            if (objPolicyClauseInfo.RequiredTransactionLog)
            {
                objPolicyClauseInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyClauses.aspx.resx");

                returnval = objPolicyClauseInfo.AddPolClauses();

            }

            return returnval;
        }

        public int DeactivatePolClauses(ClsPolicyClauseInfo objPolicyClauseInfo)
        {
            int returnval = 0;

            if (objPolicyClauseInfo.RequiredTransactionLog)
            {
                objPolicyClauseInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PolicyClauses.aspx.resx");

                returnval = objPolicyClauseInfo.DeactivatePolClauses();

            }

            return returnval;
        }
        #endregion


        #region Reinsurance
        // Pravesh to change Grid binding aproach in new EbixModel
        public static List<Model.Policy.ClsPolicyReinsurerInfo> GetReinsurer(int customer_id, int policy_id, int Poversion_id)
        {
            string strStoredProc = "Proc_Get_POL_REINSURANCE_INFO";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
                objDataWrapper.AddParameter("@POLICY_ID", policy_id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Poversion_id);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                List<Model.Policy.ClsPolicyReinsurerInfo> reinsurers = new List<Model.Policy.ClsPolicyReinsurerInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.Policy.ClsPolicyReinsurerInfo ObjReinsurer = new Model.Policy.ClsPolicyReinsurerInfo();
                    ClsCommon.PopulateEbixPageModel(dr, ObjReinsurer);
                    reinsurers.Add(ObjReinsurer);
                }
                return reinsurers;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }
        public DataSet GetReinsurerInfo(int customer_id, int policy_id, int Poversion_id)
        {
            string strStoredProc = "Proc_Get_POL_REINSURANCE_INFO";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
                objDataWrapper.AddParameter("@POLICY_ID", policy_id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Poversion_id);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }

        public DataSet GetValues(int CONTRACT_ID)
        {
            string strStoredProc = "Proc_FetchReinsuranceCommissionValues";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CONTRACT_ID", CONTRACT_ID);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }

        public int DeleteReinsurer(ClsPolicyReinsurerInfo objPolicyReinsurerInfo)
        {

            int returnValue = 0;

            if (objPolicyReinsurerInfo.RequiredTransactionLog)
            {
                objPolicyReinsurerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddPolicyReinsurer.aspx.resx");
                returnValue = objPolicyReinsurerInfo.DeleteReinsurer();

            }
            return returnValue;
        }

        public int SaveReinsurer(ArrayList arrReinsurace)
        {
            ClsPolicyReinsurerInfo objPolicyReinsurerInfo = (ClsPolicyReinsurerInfo)arrReinsurace[0];
            int returnValue = 0;
            //if (objPolicyReinsurerInfo.RequiredTransactionLog)
            // {
            objPolicyReinsurerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddPolicyReinsurer.aspx.resx");

            // }
            returnValue = objPolicyReinsurerInfo.SaveReinsurer(arrReinsurace);
            return returnValue;
        }

        public int UpdateReinsurer(ClsPolicyReinsurerInfo objPolicyReinsurerInfo)
        {

            int returnValue = 0;

            if (objPolicyReinsurerInfo.RequiredTransactionLog)
            {
                objPolicyReinsurerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddPolicyReinsurer.aspx.resx");
                returnValue = objPolicyReinsurerInfo.UpdateReinsurer();

            }
            return returnValue;
        }

        #endregion


        //Added By Lalit on DATE:MAY 17,2010
        #region Genrate Coverages RATE
        public string GenrateRiskCoveragesXml(int Cusomer_id, int Policy_id, int Policy_version_id, int Lob_id, int colorscheme, int userid)
        {
            try
            {
                if (Lob_id != 0)
                {
                    return GenrateCoveragesRateXml(Cusomer_id, Policy_id, Policy_version_id, Lob_id, colorscheme, userid);
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }
        public string GenrateCoveragesRateXml(int Cusomer_id, int Policy_id, int Policy_version_id, int Lob_id, int colorscheme, int Userid)
        {
            //System.Globalization.CultureInfo oldculture = Thread.CurrentThread.CurrentCulture;
            // Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            StringBuilder returnString = new StringBuilder();
            string Returnxml = string.Empty;
            string strStoredProc_PrimaryApplicants = "Proc_GetPrimaryApplicantInfo_Pol";
            string strStoredProc_PolicyGenralInfo = "Proc_GetPolicyInformation";
            string strStoredProc_CoveragePremimum = "Proc_GetPolicyCoveragePremium";
            DataSet dsQuote = null;
            string strReturnXML = "";
            string strLANG_ID = "";
            int intLANG_ID = 0;
            DataSet dsTempXML;
            XmlDocument xmlDocViolAcc = new XmlDocument();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUOTE>");
                returnString.Append("<POLICY>");

                //Genrate  Primary Aplicant Information XML

                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", Cusomer_id);
                objDataWrapper.AddParameter("@POLICYID", Policy_id);
                objDataWrapper.AddParameter("@POLICYVERSIONID", Policy_version_id);
                objDataWrapper.AddParameter("@USERID", Userid);
                objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_PrimaryApplicants);
                objDataWrapper.ClearParameteres();

                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);

                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");



                strLANG_ID = ClsCommon.FetchValueFromXML("LANG_ID", ClsCommon.GetXML(dsTempXML.Tables[0]));
                if (strLANG_ID != "")
                {
                    intLANG_ID = int.Parse(strLANG_ID);
                }

                //END

                objDataWrapper.ClearParameteres();
                strReturnXML = "";
                dsTempXML = null;

                //Genrate Policy Information XML
                objDataWrapper.AddParameter("@CUSTOMER_ID", Cusomer_id);
                objDataWrapper.AddParameter("@POLICY_ID", Policy_id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_version_id);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                dsTempXML = objDataWrapper.ExecuteDataSet(strStoredProc_PolicyGenralInfo);
                objDataWrapper.ClearParameteres();                           //Cleae datawrapper parameter                     

                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]);       // Convert DataSet Table value int to XML from created GetXML(DataTable dt) function               
                //Remove unused tags and string                        
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                //End
                strReturnXML = strReturnXML.Replace("<Table>", "");              //replace <table >into <COVERAGE> tag for saprate each coverage regarding the particula risk
                strReturnXML = strReturnXML.Replace("</Table>", "");
                returnString.Append(strReturnXML);                      //append PolicyGenralInfo genrated xml into final XML

                returnString.Append("</POLICY>");        //Close Policy Tag of genrated XML after complete ploicy information

                //End Policy information  XML

                objDataWrapper.ClearParameteres();      //Cleae datawrapper parameter       
                strReturnXML = "";                      //Clear  string for assign next genrated xml


                //Genrate Risk Coverages XML
                returnString.Append("<COVERAGES>");                                                      //start coverages tag for policy coverage details
                objDataWrapper.AddParameter("@CUSTOMER_ID", Cusomer_id, SqlDbType.Int);                  //set proc parameter
                objDataWrapper.AddParameter("@POLICY_ID", Policy_id, SqlDbType.Int);                     //set proc parameter
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Policy_version_id, SqlDbType.Int);     //set proc parameter
                objDataWrapper.AddParameter("@LOBID", Lob_id, SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", intLANG_ID, SqlDbType.Int); //set proc parameter  

                dsQuote = objDataWrapper.ExecuteDataSet(strStoredProc_CoveragePremimum);                                  //get the the coverage value in to dataset


                if (dsQuote != null && dsQuote.Tables.Count > 0 && dsQuote.Tables[0].Rows.Count > 0)     //check if coverages data set have any row for coverages details
                {
                    strReturnXML = ClsCommon.GetXML(dsQuote.Tables[0], 1);                   // Convert DataSet Table value int to XML from created GetXML(DataTable dt) function
                    //Remove unused tags and string from genraetd xml  
                    strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                    strReturnXML = strReturnXML.Replace("\"", "D673GSUYD7G3J73UDD");
                    strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                    strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                    //End
                    strReturnXML = strReturnXML.Replace("<Table>", "<COVERAGE>");              //replace <table >into <COVERAGE> tag for saprate each coverage regarding the particula risk
                    strReturnXML = strReturnXML.Replace("</Table>", "</COVERAGE>");            //replace <table >into <COVERAGE> tag for saprate each coverage regarding the particula risk

                    returnString.Append(strReturnXML);  //Combine dsQuote coverages details in XML For Trnasform
                    strReturnXML = "";
                }

                returnString.Append("</COVERAGES>");   //Close Coverages tag 
                //End coverages XML
                //Thread.CurrentThread.CurrentCulture = oldculture;
                returnString.Append("</QUOTE>");       // Close start Quote tag after compliting the input xml
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
            return returnString.ToString();
        }
        #endregion 	 //END Added Lalit

        #region GetPolicy Coverage Limit
        public DataSet GetPolicyCov_Details(int CustomerId, int PolicyId, int PilicyVersionId, int LobId, int LangId)
        {
            DataSet ds = null;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            string strStoredProc_CoveragePremimum = "Proc_GetPolicyCoveragePremium";
            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);                  //set proc parameter
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);                     //set proc parameter
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PilicyVersionId, SqlDbType.Int);     //set proc parameter
                objDataWrapper.AddParameter("@LOBID", LobId, SqlDbType.Int);
                objDataWrapper.AddParameter("@LANG_ID", LangId, SqlDbType.Int); //set proc parameter
                ds = objDataWrapper.ExecuteDataSet(strStoredProc_CoveragePremimum);
            }
            catch
            {

            }

            return ds;
            // Proc_GetPolicyCoveragePremium
        }
        #endregion

        #region Update policy Limit
        /// <summary>
        /// Update Policy Coverage Limit 
        /// </summary>
        /// <param name="CustomerId">Int</param>
        /// <param name="PolicyId">Int</param>
        /// <param name="PilicyVersionId">Int</param>
        /// <param name="LobId">Int</param>
        /// <param name="RiskId">Int</param>
        /// <param name="Coverage_Code_Id">Int</param>
        /// <param name="Limit">Double</param>
        /// <returns></returns>
        public int UpdatePolicyCovLimit(int CustomerId, int PolicyId, int PilicyVersionId, int LobId, int RiskId, int Coverage_Code_Id, double Limit)
        {

            int retval = 0;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            string strStoredProc_CoveragePremimum = "Proc_UpdatePolicyCovLimit";
            try
            {
                if (objDataWrapper == null)
                    objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);                  //set proc parameter
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);                     //set proc parameter
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PilicyVersionId, SqlDbType.Int);     //set proc parameter
                objDataWrapper.AddParameter("@LOBID", LobId, SqlDbType.Int);
                objDataWrapper.AddParameter("@RISK_ID", RiskId, SqlDbType.Int); //set proc parameter
                objDataWrapper.AddParameter("@COVERAGE_CODE_ID", Coverage_Code_Id, SqlDbType.Int); //set proc parameter
                objDataWrapper.AddParameter("@LIMIT", Limit, SqlDbType.Decimal); //set proc parameter    
                retval = objDataWrapper.ExecuteNonQuery(strStoredProc_CoveragePremimum);
            }
            catch
            {

            }

            return retval;

        }
        #endregion

        //added By Lalit Nov 16,2010
        #region Update New Policy No on NBS commit
        public int UpdatePolicyNo(int CustomerId, int PolicyId, int PilicyVersionId, string policy_No, int PROCESS_ID, DataWrapper objDataWrapper)
        {
            string SUSEP_LOB_CODE = string.Empty;
            return UpdatePolicyNo(CustomerId, PolicyId, PilicyVersionId, policy_No, PROCESS_ID, objDataWrapper, SUSEP_LOB_CODE);
        }
        public int UpdatePolicyNo(int CustomerId, int PolicyId, int PilicyVersionId, string policy_No, int PROCESS_ID, DataWrapper objDataWrapper, string SUSEP_LOB_CODE)
        {

            int retval = 0;

            string strStoredProc_CoveragePremimum = "Proc_UpdatePolicyNumber";
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);                  //set proc parameter
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);                     //set proc parameter
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PilicyVersionId, SqlDbType.Int);     //set proc parameter
                objDataWrapper.AddParameter("@POLICY_NUMBER", policy_No, SqlDbType.NVarChar, ParameterDirection.Input, 75); //set proc parameter
                objDataWrapper.AddParameter("@PROCESS_ID", PROCESS_ID, SqlDbType.Int); //set proc parameter
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", SUSEP_LOB_CODE); //set proc parameter
                retval = objDataWrapper.ExecuteNonQuery(strStoredProc_CoveragePremimum);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retval;

        }
        #endregion


        public DataSet GetPolicyDocumentList(int customer_id, int policy_id, int Poversion_id, string Doc_code, int Claim_id, int Activity_id)
        {
            string strStoredProc = "Proc_GetPolicyDocumentList";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", customer_id);
                objDataWrapper.AddParameter("@POLICY_ID", policy_id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", Poversion_id);
                objDataWrapper.AddParameter("@LANG_ID", BlCommon.ClsCommon.BL_LANG_ID);
                objDataWrapper.AddParameter("@DOCUMENT_CODE", Doc_code);
                objDataWrapper.AddParameter("@CLAIM_ID", Claim_id);
                objDataWrapper.AddParameter("@ACTIVITY_ID", Activity_id);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }
        #region To get installment Details
        //Added by Pradeep Kushwaha on 30-June-2011
        /// <summary>
        /// To get installment Details based on Customer_id , policy_id, Policy_version_id
        /// </summary>
        /// <param name="_Customer_Id"></param>
        /// <param name="_Policy_Id"></param>
        /// <param name="_Policy_Version_Id"></param>
        /// <returns></returns>
        public DataSet GetInstallmentDetails(int _Customer_Id, int _Policy_Id, int _Policy_Version_Id)
        {
            string strStoredProc = "Proc_Fetch_INSTALLMENT_BOLETO";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", _Customer_Id);
                objDataWrapper.AddParameter("@POLICY_ID", _Policy_Id);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", _Policy_Version_Id);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }

        }
        #endregion

        public ClsPolicyRemunerationInfo FetchData(Int32 REMUNERATION_ID, int customerId, int PolicyId, int PolicyVersionId)
        {

            DataSet dsCount = null;
            ClsPolicyRemunerationInfo objPolicyRemunerationInfo = new ClsPolicyRemunerationInfo();
            try
            {
                dsCount = objPolicyRemunerationInfo.FetchData(REMUNERATION_ID, customerId, PolicyId, PolicyVersionId);

                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objPolicyRemunerationInfo);
                }

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return objPolicyRemunerationInfo;
        }

        public static int GetPolicyEndorsementCoApplicant(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            DataSet ds = null;
            int CO_APPLICANT_ID = 0;
            string StoredProcedure = "getPolicyEndorsement_CoApplicant";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                ds = objDataWrapper.ExecuteDataSet(StoredProcedure);
                objDataWrapper.Dispose();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["CO_APPLICANT_ID"].ToString() != "")
                        CO_APPLICANT_ID = int.Parse(ds.Tables[0].Rows[0]["CO_APPLICANT_ID"].ToString());


                }
            }
            catch
            {

            }
            return CO_APPLICANT_ID;
        }

        public DataSet GetPolicyEndorsementVersions(int CustomerId, int PolicyId, int PolicyVersionId, DateTime Effective_date, string CalledFrom)
        {
            DataSet DsEnd = null;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
            objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
            objDataWrapper.AddParameter("@END_EFFE_DATE", Effective_date);
            objDataWrapper.AddParameter("@CALLED_FROM", CalledFrom);
            objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
            DsEnd = objDataWrapper.ExecuteDataSet("Proc_GetPolicyEndorsementVersions");
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return DsEnd;
        }
        public DataSet GetPolicyEndorsementDetails(int CustomerId, int PolicyId, int PolicyVersionId)
        {
            DataSet DsEnd = null;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
            objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
            DsEnd = objDataWrapper.ExecuteDataSet("Proc_GetPolicyEndorsementNo");
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return DsEnd;
        }
        // changes by naveen pujari for  implementation of new management screen
        public DataSet GetManagementReportList(int REPORT_TYPE)
        {
            DataSet dsReport = null;
            string Proc_Name = "Proc_SUSEP_Report";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@REPORT_TYPE", REPORT_TYPE);
            dsReport = objDataWrapper.ExecuteDataSet(Proc_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }

        // changes by naveen pujari for  implementation of new management screen,tfs ID:529,modified by naveen
        public DataSet GetSp_Policy(DateTime InitialDate, DateTime EndDate, string Division, string SusepLOB, string Policy_NO, int Broker_Code, int Order_By, string Procedure_Name)
        {
            DataSet dsReport = null;
          
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@APP_EFFECTIVE_START_DATE", InitialDate);
             objDataWrapper.AddParameter("@APP_EFFECTIVE_END_DATE", EndDate);
            if (Division != " ")
            {
                objDataWrapper.AddParameter("@DIV_CODE", Division);
            }
            else
                objDataWrapper.AddParameter("@DIV_CODE", null);
            if (SusepLOB != " ")
            {
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", SusepLOB);
            }
            else
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", null);
            if (Policy_NO != "")
            {
                objDataWrapper.AddParameter("@POLICY_NUMBER", Policy_NO);
            }
            else
                objDataWrapper.AddParameter("@POLICY_NUMBER", null);
            if (Broker_Code != 0)
            {
                objDataWrapper.AddParameter("@AGENCY_CODE", Broker_Code);
            }
            else
                objDataWrapper.AddParameter("@AGENCY_CODE", null);

            if (Order_By != 0)
            {
                objDataWrapper.AddParameter("@ORDER_BY", Order_By);
            }
            else
                objDataWrapper.AddParameter("@ORDER_BY", null);
            dsReport = objDataWrapper.ExecuteDataSet(Procedure_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }
        //tfs ID:529,modified by naveen
        public DataSet GetSp_Installment(DateTime InitialDate, DateTime EndDate, string Division, string SusepLOB, string Policy_NO, int Order_By, string Procedure_Name)
        {
            DataSet dsReport = null;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
           
                objDataWrapper.AddParameter("@APP_EFFECTIVE_START_DATE", InitialDate);
           
                objDataWrapper.AddParameter("@APP_EFFECTIVE_END_DATE", EndDate);
           
            if (Division != " ")
            {
                objDataWrapper.AddParameter("@DIV_CODE", Division);
            }
            else
                objDataWrapper.AddParameter("@DIV_CODE", null);
            if (SusepLOB != " ")
            {
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", SusepLOB);
            }
            else
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", null);
            if (Policy_NO != "")
            {
                objDataWrapper.AddParameter("@POLICY_NUMBER", Policy_NO);
            }
            else
                objDataWrapper.AddParameter("@POLICY_NUMBER", null);

            if (Order_By != 0)
            {
                objDataWrapper.AddParameter("@ORDER_BY", Order_By);
            }
            else
                objDataWrapper.AddParameter("@ORDER_BY", null);
            dsReport = objDataWrapper.ExecuteDataSet(Procedure_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }
       // tfs ID:529,modified by naveen
        public DataSet GetSp_CLAIM_DETAILS(DateTime InitialDate, DateTime EndDate, string ClaimNumber,  string Procedure_Name)
        {
            DataSet dsReport = null;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
           
                objDataWrapper.AddParameter("@ACTIVITY_START_DATE", InitialDate);
            
                objDataWrapper.AddParameter("@ACTIVITY_END_DATE", EndDate);
           
            if (ClaimNumber != "")
            {
                objDataWrapper.AddParameter("@CLAIM_NUMBER", ClaimNumber);
            }
            else
                objDataWrapper.AddParameter("@CLAIM_NUMBER", null);
           
            dsReport = objDataWrapper.ExecuteDataSet(Procedure_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }


        public DataSet GetSp_CLAIM(DateTime InitialDate, DateTime EndDate, string Division, string SusepLOB, string Policy_NO, string Claim_No, int Order_By, string Procedure_Name)
        {
            DataSet dsReport = null;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
           
                objDataWrapper.AddParameter("@ACTIVITY_START_DATE", InitialDate);
            
            
                objDataWrapper.AddParameter("@ACTIVITY_END_DATE", EndDate);
            
            if (Division != " ")
            {
                objDataWrapper.AddParameter("@DIV_CODE", Division);
            }
            else
                objDataWrapper.AddParameter("@DIV_CODE", null);
            if (SusepLOB != " ")
            {
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", SusepLOB);
            }
            else
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", null);
            if (Policy_NO != "")
            {
                objDataWrapper.AddParameter("@POLICY_NUMBER", Policy_NO);
            }
            else
                objDataWrapper.AddParameter("@POLICY_NUMBER", null);
            if (Claim_No != "")
            {
                objDataWrapper.AddParameter("@CLAIM_NUMBER", Claim_No);
            }
            else
                objDataWrapper.AddParameter("@CLAIM_NUMBER", null);
            if (Order_By != 0)
            {
                objDataWrapper.AddParameter("@ORDER_BY", Order_By);
            }
            else
                objDataWrapper.AddParameter("@ORDER_BY", null);
            dsReport = objDataWrapper.ExecuteDataSet(Procedure_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }

        //tfs ID:529,modified by naveen
        public DataSet GetSp_BROKER_INSTALLMENTS(DateTime InitialDate, DateTime EndDate, string Division, string SusepLOB, string Policy_NO, int Order_By, string Procedure_Name)
        {
            DataSet dsReport = null;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
          
                objDataWrapper.AddParameter("@APP_EFFECTIVE_START_DATE", InitialDate);
           
            
                objDataWrapper.AddParameter("@APP_EFFECTIVE_END_DATE", EndDate);
           
            if (Division != " ")
            {
                objDataWrapper.AddParameter("@DIV_CODE", Division);
            }
            else
                objDataWrapper.AddParameter("@DIV_CODE", null);
            if (SusepLOB != " ")
            {
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", SusepLOB);
            }
            else
                objDataWrapper.AddParameter("@SUSEP_LOB_CODE", null);
            if (Policy_NO != "")
            {
                objDataWrapper.AddParameter("@POLICY_NUMBER", Policy_NO);
            }
            else
                objDataWrapper.AddParameter("@POLICY_NUMBER", null); 
            if (Order_By != 0)
            {
                objDataWrapper.AddParameter("@ORDER_BY", Order_By);
            }
            else
                objDataWrapper.AddParameter("@ORDER_BY", null);
            dsReport = objDataWrapper.ExecuteDataSet(Procedure_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }


        public DataSet GetSp_Susep_Report(DateTime InitialDate, string Procedure_Name)
        {
            DataSet dsReport = null;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
         
             objDataWrapper.AddParameter("@DATETIME", InitialDate);
           
            dsReport = objDataWrapper.ExecuteDataSet(Procedure_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }


        //1061,1064,
        public DataSet GetSp_Susep_FromDate_ToDate(DateTime InitialDate,DateTime EndDate, string Procedure_Name)
        {
            DataSet dsReport = null;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
           
            objDataWrapper.AddParameter("@FROM_DATETIME", InitialDate);
            objDataWrapper.AddParameter("@TO_DATETIME", EndDate);
            
            dsReport = objDataWrapper.ExecuteDataSet(Procedure_Name);
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }



        public static string GetManagement_ReportName(int ReportId, int GroupByID)
        {
            DataSet dsReport = null;
            string _ReportName = "";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();

            objDataWrapper.AddParameter("@REPORT_TYPE_ID", ReportId);
            objDataWrapper.AddParameter("@GROUPBY_ID", GroupByID);

            dsReport = objDataWrapper.ExecuteDataSet("Proc_GetManagementReport_Name");
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            if (dsReport != null && dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
                _ReportName = dsReport.Tables[0].Rows[0]["RDL_SHORT_NAME"].ToString();

            return _ReportName;

        }
        public int GenrateManagementReport(DateTime EffectiveStartDate, DateTime EffectiveEndDate, string Division, string SusepLOB, string Policy_NO, int Broker_Code, string ClaimNo, int ORDER_BY)
        {
            int _Value = 0;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {             

                
                objDataWrapper.AddParameter("@EFFECTIVE_START_DATE", EffectiveStartDate);
                objDataWrapper.AddParameter("@EFFECTIVE_END_DATE", EffectiveEndDate);
                if (Division != "")
                {
                    objDataWrapper.AddParameter("@DIV_CODE", Division);
                }
                else
                    objDataWrapper.AddParameter("@DIV_CODE", null);
                if (SusepLOB != "")
                {
                    objDataWrapper.AddParameter("@SUSEP_LOB_CODE", SusepLOB);
                }
                else
                    objDataWrapper.AddParameter("@SUSEP_LOB_CODE", null);
                if (Policy_NO != "")
                {
                    objDataWrapper.AddParameter("@POLICY_NUMBER", Policy_NO);
                }
                else
                    objDataWrapper.AddParameter("@POLICY_NUMBER", null);
                if (Broker_Code != 0)
                {
                    objDataWrapper.AddParameter("@AGENCY_CODE", Broker_Code);
                }
                else
                    objDataWrapper.AddParameter("@AGENCY_CODE", null);
                if (ClaimNo != "")
                {
                    objDataWrapper.AddParameter("@CLAIM_NUMBER", ClaimNo);
                }
                else
                    objDataWrapper.AddParameter("@CLAIM_NUMBER", null);               
         
                objDataWrapper.AddParameter("@ORDER_BY", ORDER_BY);             

                _Value = objDataWrapper.ExecuteNonQuery("SP_INSERT_MANAGEMENT_REPORT");
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return _Value;
               // objDataWrapper.Dispose();
            }
            catch (Exception ex) {

                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            
            }

           

        }
        #region Get Policy input xml 
        //added By Lalit ,Sep 09 2011.
        /// <summary>
        /// Get policy Input xml From DataBase
        /// </summary>
        /// <param name="_CustomerID"></param>
        /// <param name="_PolicyID"></param>
        /// <param name="_PolicyVersionID"></param>
        /// <returns></returns>

        public string GetPolicyInputXML(int _CustomerID,int _PolicyID,int _PolicyVersionID)
        {
            DataSet DsInputXML = null;
            string strReturnXML="";
            StringBuilder strbldrReturnXML = new StringBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", _CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", _PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", _PolicyVersionID);
                DsInputXML = objDataWrapper.ExecuteDataSet("PROC_GETPOLICY_INPUTXML");

                if (DsInputXML != null && DsInputXML.Tables.Count > 0)
                {
                    foreach (DataRow dr in DsInputXML.Tables[0].Rows)
                    {

                        strReturnXML = dr[0].ToString();

                        strbldrReturnXML.Append(strReturnXML);

                    }
                }
                //return DsInputXML.Tables[0].Rows[0][0].ToString();
                return strbldrReturnXML.ToString();
            }
            catch(Exception ex)
            {
                throw (ex);
            }

        }
        #endregion

        //Get bult data, function added by naveen ,itrack 813
        public DataSet Get_BulkData_RuleTest()
        {
            DataSet dsReport = null;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            objDataWrapper.ClearParameteres();
            dsReport = objDataWrapper.ExecuteDataSet("Proc_FetchRuleXmlData");
            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();
            return dsReport;
        }

        //itrack 974, implemented by naveen , sept 27,2011

        public void InsertFormat(string Phone_number, string Acc_NO,String Zip_Code,string Date)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@Phone_No", Phone_number);
                objDataWrapper.AddParameter("@Account_No", Acc_NO);
                objDataWrapper.AddParameter("@ZIP_Code", Zip_Code);
                objDataWrapper.AddParameter("@Date", Date);
                objDataWrapper.ExecuteNonQuery("Proc_Set_CustomFormat");
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            }
            catch (Exception ex)
            {

                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);

            }
                 
               
        }


        //itrack 974, implemented by naveen 
        public DataSet GetFormatDetails()
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            DataSet dsFomatDetail = null;
            try
            {

                return dsFomatDetail = objDataWrapper.ExecuteDataSet("Proc_GetCustomFormat_Detail");
              
               
            }
            catch (Exception ex)
            {

               
                throw (ex);

            }


        }

        public int UpdateACT_POLICY_REIN_INSTALLMENT_DETAILS(ClsPolicyReinsurerInfo objReinsurerInfo)  //Added by Aditya for TFS BUG # 2705
        {
            int returnValue = 0;

            if (objReinsurerInfo.RequiredTransactionLog)
            {
                objReinsurerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddPolicyReinsurer.aspx.resx");

                returnValue = objReinsurerInfo.UpdateACT_POLICY_REIN_INSTALLMENT_DETAILS();

            }
            return returnValue;
        }   //Added till here

        public int UpdatePolicyRIContactInfo(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int DISREGARD_RI_CONTRACT) 
        {
            int RET_VAL = 0;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@DISREGARD_RI_CONTRACT", DISREGARD_RI_CONTRACT);
                RET_VAL = objDataWrapper.ExecuteNonQuery("PROC_UpdatePolicyRIContactInfo");                
                
            }
            catch (Exception ex)
            {

                //objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);

            }

            return RET_VAL;

        }

    }
}


