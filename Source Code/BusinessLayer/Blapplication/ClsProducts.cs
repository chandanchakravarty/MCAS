/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	12-04-2010
<End Date			: -	
<Description		: - Use to deal with all Products's Operation details Business Layer
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -  
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.Transportation;
using Cms.Model.Policy.Accident;

namespace Cms.BusinessLayer.BlApplication
{
    public class ClsProducts : clsapplication 
    {
        public ClsProducts()
        { }
        #region For the MariTime Product Operation details
        /// <summary>
        /// Get the mariTime Data using MARITIME_ID
        /// </summary>
        /// <param name="MARITIME_ID"></param>
        /// <returns></returns>
        public Boolean FetchData(ref ClsMeritimeInfo objMeritimeInfo)
        {
            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = objMeritimeInfo.FetchData();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objMeritimeInfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;


            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;

        }//public Boolean FetchData(ref ClsMeritimeInfo objMeritimeInfo)

        
        /// <summary>
        /// Activate and Deactivate the Maritime base on the MARITIME_ID and is_Activae 
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivateMariTime(ClsMeritimeInfo ObjMaritimeInfo)
        {
            int returnValue = 0;
            if (ObjMaritimeInfo.RequiredTransactionLog)
            {
                ObjMaritimeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddMeriTimeInfo.aspx.resx");

                returnValue = ObjMaritimeInfo.ActivateDeactivateMariTimeData();
            }//if (ObjMaritimeInfo.RequiredTransactionLog)
            return returnValue;
        }//public int ActivateDeactivateMariTime(ClsMeritimeInfo ObjMaritimeInfo)
        
        /// <summary>
        /// Delete the MariTime Data Based on MariTime id 
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int DeleteMariTime(ClsMeritimeInfo ObjMaritimeInfo)
        {
            int returnValue = 0;
            if (ObjMaritimeInfo.RequiredTransactionLog)
            {
                ObjMaritimeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddMeriTimeInfo.aspx.resx");
                returnValue = ObjMaritimeInfo.DeleteMariTimeData();
            }//if (ObjMaritimeInfo.RequiredTransactionLog)
            return returnValue;
        }// public int DeleteMariTime(ClsMeritimeInfo ObjMaritimeInfo)

        /// <summary>
        /// Insert the MariTime Data
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int AddMariTime(ClsMeritimeInfo ObjMaritimeInfo)
        {
            int returnValue = 0;

            if (ObjMaritimeInfo.RequiredTransactionLog)
            {
                ObjMaritimeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddMeriTimeInfo.aspx.resx");

                returnValue = ObjMaritimeInfo.AddMariTimeData();

            }//if (ObjMaritimeInfo.RequiredTransactionLog)
            return returnValue;
        }//public int AddMariTime(ClsMeritimeInfo ObjMaritimeInfo)

        /// <summary>
        /// Update The MariTime Date Based On the MariTime Id
        /// </summary>
        /// <param name="ObjMaritimeInfo"></param>
        /// <returns></returns>
        public int UpdateMariTime(ClsMeritimeInfo ObjMaritimeInfo)
        {
            int returnValue = 0;

            if (ObjMaritimeInfo.RequiredTransactionLog)
            {
                ObjMaritimeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddMeriTimeInfo.aspx.resx");


                returnValue = ObjMaritimeInfo.UpdateMariTimeData();

            }//if (ObjMaritimeInfo.RequiredTransactionLog)

            return returnValue;
        }//public int UpdateMariTime(ClsMeritimeInfo ObjMaritimeInfo)

        #endregion

        #region For the National and International Transportation Commodity Info Product Operation details
         /// <summary>
        ///  Get the Commodity info Data  
         /// </summary>
         /// <param name="objCommodityInfo"></param>
         /// <returns></returns>
        public Boolean FetchCommodityInfoData(ref ClsCommodityInfo objCommodityInfo)
        {

            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = objCommodityInfo.FetchData();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objCommodityInfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;


            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;

        }//public ClsCommodityInfo FetchCommodityInfoData(Int32 Commodity_id)


        /// <summary>
        /// Activate and Deactivate the Commodity info base on the Commodity_id and is_Activae 
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivateCommodityInfo(ClsCommodityInfo objCommodityInfo)
        {
            int returnValue = 0;
            if (objCommodityInfo.RequiredTransactionLog)
            {
                objCommodityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Transportation/AddCommodityInfo.aspx.resx");

                returnValue = objCommodityInfo.ActivateDeactivateCommodityData();
            }//if (objCommodityInfo.RequiredTransactionLog)
            return returnValue;
        }// public int ActivateDeactivateCommodityInfo(ClsCommodityInfo objCommodityInfo)

        /// <summary>
        /// Delete the Commodity info Data Based on Commodity id 
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int DeleteCommodityInfo(ClsCommodityInfo objCommodityInfo)
        {
            int returnValue = 0;
            if (objCommodityInfo.RequiredTransactionLog)
            {
                objCommodityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Transportation/AddCommodityInfo.aspx.resx");
                returnValue = objCommodityInfo.DeleteCommodityData();
            }//if (objCommodityInfo.RequiredTransactionLog)
            return returnValue;
        }//public int DeleteCommodityInfo(ClsCommodityInfo objCommodityInfo)

        /// <summary>
        /// Insert the Commodity info Data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int AddCommodityInfo(ClsCommodityInfo objCommodityInfo)
        {
            int returnValue = 0;

            if (objCommodityInfo.RequiredTransactionLog)
            {
                objCommodityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Transportation/AddCommodityInfo.aspx.resx");

                returnValue = objCommodityInfo.AddCommodityData();

            }//if (objCommodityInfo.RequiredTransactionLog)
            return returnValue;
        }//public int AddCommodityInfo(ClsCommodityInfo objCommodityInfo)

        /// <summary>
        /// Update The Commodity info Data Based On the Commodity Id
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int UpdateCommodityInfo(ClsCommodityInfo objCommodityInfo)
        {
            int returnValue = 0;

            if (objCommodityInfo.RequiredTransactionLog)
            {
                objCommodityInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Transportation/AddCommodityInfo.aspx.resx");


                returnValue = objCommodityInfo.UpdateCommodityData();

            }//if (objCommodityInfo.RequiredTransactionLog)

            return returnValue;
        }//public int UpdateCommodityInfo(ClsCommodityInfo objCommodityInfo)

        #endregion

        #region For Individual Personal Accident Product Operation details
        /// <summary>
        /// Get the Individual Data using PERSONAL_INFO_ID
        /// </summary>
        /// <param name="PERSONAL_INFO_ID"></param>
        /// <returns></returns>
        public Boolean FetchPersonalAccidentInfoData(ref ClsIndividualInfo objIndividualInfo)
        {

            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = objIndividualInfo.FetchData();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objIndividualInfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;


            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;

        }// public Boolean FetchPersonalAccidentInfoData(ref ClsIndividualInfo objIndividualInfo)

        public int AddPersonalAccidentInfoData(ClsIndividualInfo objIndividualInfo, String CalledFrom)
        {
            int returnValue = 0;

            if (objIndividualInfo.RequiredTransactionLog)
            {
                objIndividualInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Accident/AddInvidualInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "INDPA":
                        objIndividualInfo.TRANS_TYPE_ID = 113;  //TRANS_TYPE_ID For Add Location Information Add
                        break;
                    case "CPCACC":
                        objIndividualInfo.TRANS_TYPE_ID = 212;
                        break;
                    case "MRTG":
                        objIndividualInfo.TRANS_TYPE_ID = 372;  //TRANS_TYPE_ID For Add Location Information Add
                        break;
                    case "GRPLF":
                        objIndividualInfo.TRANS_TYPE_ID = 367;
                        break;
                    default:
                        break;
                }//switch (CalledFrom)

                returnValue = objIndividualInfo.ADDPersonalAccidentData();

            }
            return returnValue;
        }

        /// <summary>
        /// Update Individual Data Based On the PERSONAL_INFO_ID 
        /// </summary>
        /// <param name="objIndividualInfo"></param>
        /// <returns></returns>
        public int UpdatePersonalAccidentInfoData(ClsIndividualInfo objIndividualInfo, String CalledFrom)
        {
            int returnValue = 0;

            if (objIndividualInfo.RequiredTransactionLog)
            {
                objIndividualInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Accident/AddInvidualInfo.aspx.resx");

                switch (CalledFrom)
                {
                    case "INDPA":
                        objIndividualInfo.TRANS_TYPE_ID = 114;  //TRANS_TYPE_ID For Add Location Information Add
                        break;
                    case "CPCACC":
                        objIndividualInfo.TRANS_TYPE_ID = 213;
                        break;
                    case "MRTG":
                        objIndividualInfo.TRANS_TYPE_ID = 374;  //TRANS_TYPE_ID For Add Location Information Add
                        break;
                    case "GRPLF":
                        objIndividualInfo.TRANS_TYPE_ID = 369;
                        break;
                    default:
                        break;
                }//switch (CalledFrom)

                returnValue = objIndividualInfo.UpdatePersonalAccidentData();

            }//if (ObjMaritimeInfo.RequiredTransactionLog)

            return returnValue;
        }

        /// <summary>
        /// Delete the Individual Data Based On the PERSONAL_INFO_ID
        /// </summary>
        /// <param name="objIndividualInfo"></param>
        /// <returns></returns>
        public int DeletePersonalAccidentInfoData(ClsIndividualInfo objIndividualInfo, String CalledFrom)
        {
            int returnValue = 0;
            if (objIndividualInfo.RequiredTransactionLog)
            {
                objIndividualInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Accident/AddInvidualInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "INDPA":
                        objIndividualInfo.TRANS_TYPE_ID = 115;  //TRANS_TYPE_ID For Add Location Information Add
                        break;
                    case "CPCACC":
                        objIndividualInfo.TRANS_TYPE_ID = 214;
                        break;
                    case "MRTG":
                        objIndividualInfo.TRANS_TYPE_ID = 373;  //TRANS_TYPE_ID For Add Location Information Add
                        break;
                    case "GRPLF":
                        objIndividualInfo.TRANS_TYPE_ID = 368;
                        break;
                    default:
                        break;
                }//switch (CalledFrom)

                returnValue = objIndividualInfo.DeletePersonalAccidentData();
            }
            return returnValue;
        }

        /// <summary>
        /// Activate and Deactivate the Individual Data Based On the PERSONAL_INFO_ID and is_Activae 
        /// </summary>
        /// <param name="objIndividualInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivatePersonalAccidentInfoData(ClsIndividualInfo objIndividualInfo, String CalledFrom)
        {
            int returnValue = 0;
            if (objIndividualInfo.RequiredTransactionLog)
            {
                objIndividualInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Accident/AddInvidualInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "INDPA":
                        if (objIndividualInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objIndividualInfo.TRANS_TYPE_ID = 116;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objIndividualInfo.TRANS_TYPE_ID = 117;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "CPCACC":
                        if (objIndividualInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objIndividualInfo.TRANS_TYPE_ID = 215;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objIndividualInfo.TRANS_TYPE_ID = 216;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "MRTG":
                        if (objIndividualInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objIndividualInfo.TRANS_TYPE_ID = 375;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objIndividualInfo.TRANS_TYPE_ID = 376;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "GRPLF":
                        if (objIndividualInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objIndividualInfo.TRANS_TYPE_ID = 370;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objIndividualInfo.TRANS_TYPE_ID = 371;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;

                    default:
                        break;
                }//switch (CalledFrom)

                returnValue = objIndividualInfo.ActivateDeactivatePersonalAccidentData();
            }
            return returnValue;
        }

        /// <summary>
        /// Get the Applicant Data using APPLICANT_ID
        /// </summary>
        /// <param name="PERSONAL_INFO_ID"></param>
        /// <returns></returns>
        public ClsIndividualInfo FetchApplicantsInfoDetails(Int32 APPLICANT_ID, Int32 CUSTOMER_ID, Int32 POLICY_VERSION_ID, Int32 @POLICY_ID)
        {
            DataSet dsCount = null;
            ClsIndividualInfo ObjIndividualInfo = new ClsIndividualInfo();
            try
            {
                dsCount = ObjIndividualInfo.FetchApplicantsDetails(APPLICANT_ID, CUSTOMER_ID, POLICY_VERSION_ID, @POLICY_ID);

                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, ObjIndividualInfo);
                    if(dsCount.Tables[0].Columns.Contains("PERSONAL_INFO_ID"))
                    {ObjIndividualInfo.PERSONAL_INFO_ID.CurrentValue = Convert.ToInt32(dsCount.Tables[0].Rows[0]["PERSONAL_INFO_ID"]);}
                    
                }
            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return ObjIndividualInfo;
        }
        #endregion

        #region For the Protective Devices info Operation details
        /// <summary>
        /// Get the Protective Devices info Data based on Customer id , policu id , policy version id , risk id 
        /// </summary>
        /// <returns></returns>
        public Boolean FetchProtectiveDevicesInfoData(ref ClsProtectiveDevicesInfo ObjProtectiveDevicesInfo)
        {
            Boolean returnValue=false;
            DataSet dsCount = null;
             
            try
            {
                dsCount = ObjProtectiveDevicesInfo.FetchData();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, ObjProtectiveDevicesInfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;
                

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;
        }//public ClsProtectiveDevicesInfo FetchProtectiveDevicesInfoData(Int32 Protective_Device_ID)


        /// <summary>
        /// Activate and Deactivate the Protective Device info base on the Protective Device Id and is_Activae 
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivateProtectiveDevicesInfo(ClsProtectiveDevicesInfo objProtectiveDevicesInfo)
        {
            int returnValue = 0;
            if (objProtectiveDevicesInfo.RequiredTransactionLog)
            {
                objProtectiveDevicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/ProtectiveDevicesInfo.aspx.resx");

                returnValue = objProtectiveDevicesInfo.ActivateDeactivateProtectiveDeviceData();
            }//if (objProtectiveDevicesInfo.RequiredTransactionLog)
            return returnValue;
        }// public int ActivateDeactivateProtectiveDevicesInfo(ClsProtectiveDevicesInfo objClsProtectiveDevicesInfo)

        /// <summary>
        /// Delete the Protective Devices info Data Based on Protective Devices ID
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int DeleteProtectiveDevicesInfo(ClsProtectiveDevicesInfo objProtectiveDevicesInfo)
        {
            int returnValue = 0;
            if (objProtectiveDevicesInfo.RequiredTransactionLog)
            {
                objProtectiveDevicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/ProtectiveDevicesInfo.aspx.resx");
                returnValue = objProtectiveDevicesInfo.DeleteProtectiveDeviceData();
            }//if (objProtectiveDevicesInfo.RequiredTransactionLog)
            return returnValue;
        }//public int DeleteProtectiveDevicesInfo(ClsProtectiveDevicesInfo objProtectiveDevicesInfo)

        /// <summary>
        /// Insert the Protective Devices info Data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int AddProtectiveDevicesInfo(ClsProtectiveDevicesInfo objProtectiveDevicesInfo)
        {
            int returnValue = 0;

            if (objProtectiveDevicesInfo.RequiredTransactionLog)
            {
                objProtectiveDevicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/ProtectiveDevicesInfo.aspx.resx");

                returnValue = objProtectiveDevicesInfo.AddProtectiveDeviceData();

            }//if (objProtectiveDevicesInfo.RequiredTransactionLog)
            return returnValue;
        }//public int AddProtectiveDevicesInfo(ClsProtectiveDevicesInfo objProtectiveDevicesInfo)

        /// <summary>
        /// Update The Protective Devices info Data  
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int UpdateProtectiveDevicesInfo(ClsProtectiveDevicesInfo objProtectiveDevicesInfo)
        {
            int returnValue = 0;

            if (objProtectiveDevicesInfo.RequiredTransactionLog)
            {
                objProtectiveDevicesInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/ProtectiveDevicesInfo.aspx.resx");


                returnValue = objProtectiveDevicesInfo.UpdateProtectiveDeviceData();

            }//if (objProtectiveDevicesInfo.RequiredTransactionLog)

            return returnValue;
        }//public int UpdateProtectiveDevicesInfo(ClsProtectiveDevicesInfo objProtectiveDevicesInfo)

        #endregion
        #region For the DPVAT (Cat. 3 e 4)Products
        /// <summary>
        /// Insert DPVAT (Cat. 3 e 4) product Info Data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int AddDPVATCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo, String CalledFrom)
        {
            int returnValue = 0;

            if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            {
                ObjCivilTransportVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddDpvatInfo.aspx.resx");

                switch (CalledFrom)
                {
                    case "DPVA":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 306;//TRANS_TYPE_ID  
                        break;
                    case "DPVAT2":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 317;//TRANS_TYPE_ID  
                        break;
                    default:
                        break;
                }//switch (CalledFrom)
                     
                returnValue = ObjCivilTransportVehicleInfo.AddCivilDPVATTransportVehicleData();

            }//if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            return returnValue;
        }// public int AddDPVATCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)

        /// <summary>
        /// Update DPVAT (Cat. 3 e 4) product Info Data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int UpdateDPVATCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo, String CalledFrom)
        {
            int returnValue = 0;

            if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            {
                ObjCivilTransportVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddDpvatInfo.aspx.resx");

                switch (CalledFrom)
                {
                    case "DPVA":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 308;//TRANS_TYPE_ID  
                        break;
                    case "DPVAT2":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 319;//TRANS_TYPE_ID  
                        break;
                    default:
                        break;
                }//switch (CalledFrom)


                returnValue = ObjCivilTransportVehicleInfo.UpdateDPVATCivilTransportVehicleData();

            }//if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)

            return returnValue;
        }// public int UpdateDPVATCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)

        #endregion
        #region For the Civil Liability Transportation Products
        /// <summary>
        /// Get the Civil Transportation Vehicle Info Data based on Customer id , policy id , policy version id , Vehicle id
        /// </summary>
        /// <returns></returns>
        public Boolean FetchCivilTransportVehicleInfoData(ref ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)
        {
            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = ObjCivilTransportVehicleInfo.FetchData();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, ObjCivilTransportVehicleInfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;


            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;
        }// public Boolean FetchCivilTransportVehicleInfoData(ref ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)


        /// <summary>
        /// Activate and Deactivate the Civil Transportation Vehicle Info base on the Vehicle Id and Is_Activae 
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivateCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo,String CalledFrom)
        {
            int returnValue = 0;
            if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            {
                ObjCivilTransportVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Transportation/AddCivilTransportationVehicleInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "FLVEHICLEINFO":

                        if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 183;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 184;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "CLTVEHICLEINFO":

                        if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                             ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 131;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 132;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "DPVA"://DPVAT (Cat. 3 e 4)

                        if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 309;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 310;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "DPVAT2"://DPVAT(Cat. 1,2,9 e 10)
                        if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 320;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 321;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "AERO"://Aeronautic Product
                        if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 332;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 333;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "MTOR"://Motor Product
                        if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 340;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 341;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "CTCL"://Cargo Civil Liability transportation
                        if (ObjCivilTransportVehicleInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 355;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 356;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    default:
                        break;
                }//switch (CalledFrom)
                returnValue = ObjCivilTransportVehicleInfo.ActivateDeactivateCivilTransportVehicleData(CalledFrom);
            }//if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            return returnValue;
        }//public int ActivateDeactivateCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)

        /// <summary>
        /// Delete the Civil Transportation Vehicle Info Data Based on Vehicle ID,Customer ID ,Policy id and Policy version id
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int DeleteCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo,String CalledFrom)
        {
            int returnValue = 0;
            if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            {
                ObjCivilTransportVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Transportation/AddCivilTransportationVehicleInfo.aspx.resx");
                 switch (CalledFrom)
                {
                    case "FLVEHICLEINFO":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 182;//TRANS_TYPE_ID For Add Location Information Update
                        break;
                    case "CLTVEHICLEINFO":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 129;
                        break;
                    case "DPVA"://DPVAT (Cat. 3 e 4)
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 307;
                        break;
                    case "DPVAT2"://DPVAT(Cat. 1,2,9 e 10)
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 318;
                        break;
                    case "AERO"://Aeronautic Product
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 330;
                        break;
                    case "MTOR":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 339;
                        break;
                    case "CTCL"://Cargo Civil Liability transportation
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 353;
                        break;
                    default:
                        break;
                }//switch (CalledFrom)
                returnValue = ObjCivilTransportVehicleInfo.DeleteCivilTransportVehicleData();
            }//if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            return returnValue;
        }//public int DeleteCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)

        /// <summary>
        /// Insert the Civil Transportation Vehicle Info Data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int AddCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo, String CalledFrom)
        {
            int returnValue = 0;

            if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            {
                ObjCivilTransportVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Transportation/AddCivilTransportationVehicleInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "FLVEHICLEINFO":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 180;//TRANS_TYPE_ID For Add Location Information Update
                        break;
                    case "CLTVEHICLEINFO":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 128;
                        break;
                    case "AERO"://Aeronautic Product
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 329;
                        break;
                    case "MTOR":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID=337;
                        break;
                    case "CTCL"://Cargo Civil Liability transportation
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 352;
                        break;
                   default:
                        break;
                }//switch (CalledFrom)
                returnValue = ObjCivilTransportVehicleInfo.AddCivilTransportVehicleData();

            }//if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            return returnValue;
        }//public int AddCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)

        /// <summary>
        /// Update The Civil Transportation Vehicle Info Data  
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int UpdateCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo,String CalledFrom)
        {
            int returnValue = 0;

            if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)
            {
                ObjCivilTransportVehicleInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Transportation/AddCivilTransportationVehicleInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "FLVEHICLEINFO":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 181;//TRANS_TYPE_ID For Add Location Information Update
                        break;
                    case "CLTVEHICLEINFO":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 130;
                        break;
                    case "AERO"://Aeronautic Product
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 331;
                        break;
                    case "MTOR":
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 338;
                        break;
                    case "CTCL"://Cargo Civil Liability transportation
                        ObjCivilTransportVehicleInfo.TRANS_TYPE_ID = 354;
                        break;
                   default:
                        break;
                }//switch (CalledFrom)

                returnValue = ObjCivilTransportVehicleInfo.UpdateCivilTransportVehicleData();

            }//if (ObjCivilTransportVehicleInfo.RequiredTransactionLog)

            return returnValue;
        }//public int UpdateCivilTransportVehicleInfo(ClsCivilTransportVehicleInfo ObjCivilTransportVehicleInfo)

       

        #endregion

        #region For the Comprehensive Condominium Product

        public int AddProcductLocationInfo(ClsProductLocationInfo objroductLocationInfo,String CalledFrom) 
        {
            int returnval=0;

            if (objroductLocationInfo.RequiredTransactionLog) 
            {
                objroductLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddProductLocationInfo.aspx.resx");
                switch (CalledFrom)
                {
                   case "CompCondo":
                        objroductLocationInfo.TRANS_TYPE_ID = 136; //TRANS_TYPE_ID For Add Location Information Add
                        break;
                   case "RISK":
                        objroductLocationInfo.TRANS_TYPE_ID = 141;
                        break;
                   case "CompComp":
                        objroductLocationInfo.TRANS_TYPE_ID = 157;
                        break;
                   case "DWELLING":
                        objroductLocationInfo.TRANS_TYPE_ID = 175;
                        break;
                   case "ROBBERY":
                        objroductLocationInfo.TRANS_TYPE_ID = 201;
                        break;
                   case "GenCvlLib":
                        objroductLocationInfo.TRANS_TYPE_ID = 224;
                        break;
                   case "GLBANK"://Global of Bank
                        objroductLocationInfo.TRANS_TYPE_ID = 362;
                        break;
                   case "JDLGR"://Judicial Guarantee (Garantia Judicial)
                        objroductLocationInfo.TRANS_TYPE_ID = 379;
                        break;
                   default:
                        break;
                }//switch (CalledFrom)
                
                returnval=objroductLocationInfo.AddLocationInformation();
            }//if (objroductLocationInfo.RequiredTransactionLog)
            return returnval;
        }// public int AddProcductLocationInfo(ClsProductLocationInfo objroductLocationInfo,String CalledFrom) 

        public int UpdateProcductLocationInfo(ClsProductLocationInfo objroductLocationInfo,String CalledFrom)
        {
            int returnval = 0;

            if (objroductLocationInfo.RequiredTransactionLog)
            {
                objroductLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddProductLocationInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "CompCondo":
                        objroductLocationInfo.TRANS_TYPE_ID = 137;//TRANS_TYPE_ID For Add Location Information Update
                        break;
                    case "RISK":
                        objroductLocationInfo.TRANS_TYPE_ID = 142;
                        break;
                    case "CompComp":
                        objroductLocationInfo.TRANS_TYPE_ID = 158;
                        break;
                    case "DWELLING":
                        objroductLocationInfo.TRANS_TYPE_ID = 176;
                        break;
                    case "ROBBERY":
                        objroductLocationInfo.TRANS_TYPE_ID = 202;
                        break;
                    case "GenCvlLib":
                        objroductLocationInfo.TRANS_TYPE_ID = 225;
                        break;
                    case "GLBANK"://Global of Bank
                        objroductLocationInfo.TRANS_TYPE_ID = 364;
                        break;
                    case "JDLGR"://Judicial Guarantee (Garantia Judicial)
                        objroductLocationInfo.TRANS_TYPE_ID = 381;
                        break;
                    default:
                        break;
                }//switch (CalledFrom)
                returnval = objroductLocationInfo.UpdateLocationInformation();
            }//if (objroductLocationInfo.RequiredTransactionLog)
            return returnval;
        }//public int UpdateProcductLocationInfo(ClsProductLocationInfo objroductLocationInfo,String CalledFrom)

        public int DeleteProcductLocationInfo(ClsProductLocationInfo objroductLocationInfo,String CalledFrom,Int32 ConfirmValue) 
        {
            int returnvalue = 0;
            if (objroductLocationInfo.RequiredTransactionLog) 
            {
                objroductLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddProductLocationInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "CompCondo":
                        objroductLocationInfo.TRANS_TYPE_ID = 138;  //TRANS_TYPE_ID For Add Location Information Delete
                        break;
                    case "RISK":
                        objroductLocationInfo.TRANS_TYPE_ID = 143; 
                        break;
                    case "CompComp":
                        objroductLocationInfo.TRANS_TYPE_ID = 159;
                        break;
                    case "DWELLING":
                        objroductLocationInfo.TRANS_TYPE_ID = 177;
                        break;
                    case "ROBBERY":
                        objroductLocationInfo.TRANS_TYPE_ID = 203;
                        break;
                    case "GenCvlLib":
                        objroductLocationInfo.TRANS_TYPE_ID = 226;
                        break;
                    case "GLBANK"://Global of Bank
                        objroductLocationInfo.TRANS_TYPE_ID = 363;
                        break;
                    case "JDLGR"://Judicial Guarantee (Garantia Judicial)
                        objroductLocationInfo.TRANS_TYPE_ID = 380;
                        break;
                    default:
                        break;
                }//switch (CalledFrom)
                returnvalue = objroductLocationInfo.DeleteLocationInformation(ConfirmValue);
            }//if (objroductLocationInfo.RequiredTransactionLog) 
            return returnvalue;
        }// public int DeleteProcductLocationInfo(ClsProductLocationInfo objroductLocationInfo,String CalledFrom) 

        public int ActivateDeactivateProcductLocationInfo(ClsProductLocationInfo objroductLocationInfo,String CalledFrom)
        {
            int returnvalue = 0;
            if (objroductLocationInfo.RequiredTransactionLog)
            {
                objroductLocationInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/AddProductLocationInfo.aspx.resx");
                switch (CalledFrom)
                {
                    case "CompCondo":
                        if (objroductLocationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objroductLocationInfo.TRANS_TYPE_ID  = 139;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objroductLocationInfo.TRANS_TYPE_ID = 140;   //TRANS_TYPE_ID For Add Location Information Deactivate

                        break;
                    case "RISK":
                        if (objroductLocationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objroductLocationInfo.TRANS_TYPE_ID = 144;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objroductLocationInfo.TRANS_TYPE_ID = 145;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "CompComp":
                        if (objroductLocationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objroductLocationInfo.TRANS_TYPE_ID = 160;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objroductLocationInfo.TRANS_TYPE_ID = 161;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "DWELLING":
                        if (objroductLocationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objroductLocationInfo.TRANS_TYPE_ID = 178;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objroductLocationInfo.TRANS_TYPE_ID = 179;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "ROBBERY":
                        if (objroductLocationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objroductLocationInfo.TRANS_TYPE_ID = 204;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objroductLocationInfo.TRANS_TYPE_ID = 205;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "GenCvlLib":
                        if (objroductLocationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objroductLocationInfo.TRANS_TYPE_ID = 227;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objroductLocationInfo.TRANS_TYPE_ID = 228;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "GLBANK"://Global of Bank
                        if (objroductLocationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objroductLocationInfo.TRANS_TYPE_ID = 365;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objroductLocationInfo.TRANS_TYPE_ID = 366;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    case "JDLGR"://Judicial Guarantee (Garantia Judicial)
                        if (objroductLocationInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                            objroductLocationInfo.TRANS_TYPE_ID = 382;   //TRANS_TYPE_ID For Add Location Information Activate
                        else
                            objroductLocationInfo.TRANS_TYPE_ID = 383;   //TRANS_TYPE_ID For Add Location Information Deactivate
                        break;
                    default:
                        break;
                }//switch (CalledFrom)

                returnvalue = objroductLocationInfo.ActivateDeactivateLocationInformation();

            }//if (objroductLocationInfo.RequiredTransactionLog)
            return returnvalue;
        }//public int ActivateDeactivateProcductLocationInfo(ClsProductLocationInfo objroductLocationInfo,String CalledFrom)

        public Boolean FetchProductLocationInfo(ref ClsProductLocationInfo objProductLocationInfo)
        {


            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = objProductLocationInfo.FetchLocationInformation();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objProductLocationInfo);
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


        public void GetLocationDetailsForPolProductsinfo(System.Web.UI.WebControls.DropDownList cmbLOCATION, Int32 CustomerID,Int32 PolicyID)
        {
            ClsProductLocationInfo objProductLocationInfo=new ClsProductLocationInfo();
            DataSet ds = new DataSet();

            ds = objProductLocationInfo.GetLocationDetailsForProductsData(CustomerID, PolicyID);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = "LOCATION_ADDRESS";
                cmbLOCATION.DataSource = dv;
                cmbLOCATION.DataTextField = "LOCATION_ADDRESS";
                cmbLOCATION.DataValueField = "LOCATION_ID";
                cmbLOCATION.DataBind();
                cmbLOCATION.Items.Insert(0, "");

            }
            else
            {
                cmbLOCATION.Items.Insert(0, "");
            }


        }
        
        /// <summary>
        /// Get the Location details using Location Id 
        /// </summary>
        /// <param name="LocationID"></param>
        /// <returns></returns>
        public DataSet FetchLocationDataUsingLocationID(Int32 CustomerID, Int32 LocationID)
        {
           
            DataSet dsCount = null;
            ClsProductLocationInfo objProductLocationInfo = new ClsProductLocationInfo();
            try
            {
                dsCount = objProductLocationInfo.FetchLocationDataLocationID(CustomerID,LocationID);

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return dsCount;
        }
        #endregion 

        #region For Personal Accident for Passengers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPassengerAccidentInfo"></param>
        /// <returns></returns>
        public Boolean FetchPassengerAccidentInfo(ref ClsPassengerAccidentInfo objPassengerAccidentInfo)
        {
            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = objPassengerAccidentInfo.FetchInsuredInformation();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objPassengerAccidentInfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;


            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;

        }//public Boolean FetchPassengerAccidentInfo(ref ClsPassengerAccidentInfo objPassengerAccidentInfo)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPassengerAccidentInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivatePassengerAccidentInfo(ClsPassengerAccidentInfo objPassengerAccidentInfo)
        {
            int returnValue = 0;
            if (objPassengerAccidentInfo.RequiredTransactionLog)
            {
                objPassengerAccidentInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Accident/AddPassengerAccidentInfo.aspx.resx");

                returnValue = objPassengerAccidentInfo.ActivateDeactivateInsuredInformation();
            }//if (objPassengerAccidentInfo.RequiredTransactionLog)
            return returnValue;
        }// public int ActivateDeactivatePassengerAccidentInfo(ClsPassengerAccidentInfo objPassengerAccidentInfo)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPassengerAccidentInfo"></param>
        /// <returns></returns>
        public int DeletePassengerAccidentInfo(ClsPassengerAccidentInfo objPassengerAccidentInfo)
        {
            int returnValue = 0;
            if (objPassengerAccidentInfo.RequiredTransactionLog)
            {
                objPassengerAccidentInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Accident/AddPassengerAccidentInfo.aspx.resx");
                returnValue = objPassengerAccidentInfo.DeleteInsuredInformation();
            }//if (objPassengerAccidentInfo.RequiredTransactionLog)
            return returnValue;
        }// public int DeletePassengerAccidentInfo(ClsPassengerAccidentInfo objPassengerAccidentInfo)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPassengerAccidentInfo"></param>
        /// <returns></returns>
        public int AddPassengerAccidentInfo(ClsPassengerAccidentInfo objPassengerAccidentInfo)
        {
            int returnValue = 0;

            if (objPassengerAccidentInfo.RequiredTransactionLog)
            {
                objPassengerAccidentInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Accident/AddPassengerAccidentInfo.aspx.resx");

                returnValue = objPassengerAccidentInfo.AddInsuredInformation();

            }//if (objPassengerAccidentInfo.RequiredTransactionLog)
            return returnValue;
        }// public int AddPassengerAccidentInfo(ClsPassengerAccidentInfo objPassengerAccidentInfo)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPassengerAccidentInfo"></param>
        /// <returns></returns>
        public int UpdatePassengerAccidentInfo(ClsPassengerAccidentInfo objPassengerAccidentInfo)
        {
            int returnValue = 0;

            if (objPassengerAccidentInfo.RequiredTransactionLog)
            {
                objPassengerAccidentInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Accident/AddPassengerAccidentInfo.aspx.resx");


                returnValue = objPassengerAccidentInfo.UpdateInsuredInformation();

            }//if (objPassengerAccidentInfo.RequiredTransactionLog)

            return returnValue;
        }//public int UpdatePassengerAccidentInfo(ClsPassengerAccidentInfo objPassengerAccidentInfo)
        #endregion

        #region For Product Master Details information

        public Boolean FetchProductMasterInfo(ref Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo ,ref DataTable dt)
        {

            Boolean returnValue = false;
            DataSet dsCount = null;

            try
            {
                dsCount = objProductMasterInfo.FetchProductMasterInfoUsingLobId();


                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount.Tables[0], objProductMasterInfo);
                    dt = dsCount.Tables[1];
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;


            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;
        }
        /// <summary>
        /// Update Product info data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public Boolean UpdateProductInfo(Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo)
        {
            Boolean returnValue = false;
            int retval = 0;
            if (objProductMasterInfo.RequiredTransactionLog)
            {
                objProductMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/ProductMasterDetails.aspx.resx");

                retval = objProductMasterInfo.UpdateProductInfo();
                if (retval > 0)
                    returnValue = true;
                else
                    returnValue = false;

            }//if (objCommodityInfo.RequiredTransactionLog)
            return returnValue;
        }//public int AddCommodityInfo(ClsCommodityInfo objCommodityInfo)
        #endregion

        #region Product Penhor Rural information

        /// <summary>
        /// Get Penhor Rural Info Data based on Customer id , policy id , policy version id , Penhor Rural id
        /// </summary>
        /// <returns></returns>
        public Boolean FetchPenhorRuralInfoData(ref ClsPenhorRuralInfo objPenhorRuralInfo)
        {
            Boolean returnValue = false;
            DataSet dsCount = null;
            try
            {
                dsCount = objPenhorRuralInfo.FetchData();
                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, objPenhorRuralInfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;
            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;
        }// public Boolean FetchPenhorRuralInfoData(ref ClsPenhorRuralInfo objPenhorRuralInfo)


        /// <summary>
        /// Activate and Deactivate the Penhor Rural Info base on the Penhor Rural Id and Is_Activae 
        /// </summary>
        /// <param name="objPenhorRuralInfo"></param>
        /// <returns></returns>
        public int ActivateDeactivatePenhorRuralInfo(ClsPenhorRuralInfo objPenhorRuralInfo)
        {
            int returnValue = 0;
            if (objPenhorRuralInfo.RequiredTransactionLog)
            { 
                objPenhorRuralInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PenhorRural/PenhorRuralInfo.aspx.resx");

                if (objPenhorRuralInfo.IS_ACTIVE.CurrentValue.ToString() == "Y")
                    objPenhorRuralInfo.TRANS_TYPE_ID = 314;   
                else
                    objPenhorRuralInfo.TRANS_TYPE_ID = 315;   

                returnValue = objPenhorRuralInfo.ActivateDeactivatePenhorRuralData();
            }// if (objPenhorRuralInfo.RequiredTransactionLog)
            return returnValue;
        }// public int ActivateDeactivatePenhorRuralInfo(ClsPenhorRuralInfo objPenhorRuralInfo)

        /// <summary>
        /// Delete the Penhor Rural Info Data Based on Penhor Rural ID,Customer ID ,Policy id and Policy version id
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public int DeletePenhorRuralInfo(ClsPenhorRuralInfo objPenhorRuralInfo)
        {
            int returnValue = 0;
            if (objPenhorRuralInfo.RequiredTransactionLog)
            {
                objPenhorRuralInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PenhorRural/PenhorRuralInfo.aspx.resx");

                objPenhorRuralInfo.TRANS_TYPE_ID = 312;

                returnValue = objPenhorRuralInfo.DeletePenhorRuralData();
            }//if (objPenhorRuralInfo.RequiredTransactionLog)
            return returnValue;
        }// public int DeletePenhorRuralInfo(ClsPenhorRuralInfo objPenhorRuralInfo)

        /// <summary>
        /// Insert the Penhor Rural Info Data
        /// </summary>
        /// <param name="objPenhorRuralInfo"></param>
        /// <returns></returns>
        public int AddPenhorRuralInfo(ClsPenhorRuralInfo objPenhorRuralInfo)
        {
            int returnValue = 0;

            if (objPenhorRuralInfo.RequiredTransactionLog)
            {
                objPenhorRuralInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PenhorRural/PenhorRuralInfo.aspx.resx");

                objPenhorRuralInfo.TRANS_TYPE_ID = 311;
                returnValue = objPenhorRuralInfo.AddPenhorRuralData();

            }//if (objPenhorRuralInfo.RequiredTransactionLog)
            return returnValue;
        }//public int AddPenhorRuralInfo(ClsPenhorRuralInfo objPenhorRuralInfo)

        /// <summary>
        /// Update The Penhor Rural Info Data  
        /// </summary>
        /// <param name="objPenhorRuralInfo"></param>
        /// <returns></returns>
        public int UpdatePenhorRuralInfo(ClsPenhorRuralInfo objPenhorRuralInfo)
        {
            int returnValue = 0;

            if (objPenhorRuralInfo.RequiredTransactionLog)
            {
                objPenhorRuralInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/PenhorRural/PenhorRuralInfo.aspx.resx");

                objPenhorRuralInfo.TRANS_TYPE_ID = 313; 

                returnValue = objPenhorRuralInfo.UpdatePenhorRuralData();

            }// if (objPenhorRuralInfo.RequiredTransactionLog)

            return returnValue;
        }// public int UpdatePenhorRuralInfo(ClsPenhorRuralInfo objPenhorRuralInfo)
        #endregion

        public DataSet GetInsuredObjectdata(Int32 PERSONAL_INFO_ID, Int32 CUSTOMER_ID, Int32 POLICY_ID, Int32 POLICY_VERSION_ID, String CALLED_FOR)
        {
            DataSet dsCount = null;
            ClsIndividualInfo ObjIndividualInfo = new ClsIndividualInfo();
            try
            {
                dsCount = ObjIndividualInfo.FetchInsuredObjectData(PERSONAL_INFO_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, CALLED_FOR);

            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return dsCount;
        }


        #region For Product SUSEP CODE Master Details information By Pradeep Kushwaha

        public Boolean FetchProductSUSEPCODEMasterInfo(Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo, ref DataSet dsCount)
        {

            Boolean returnValue = false;
           
            try
            {
                dsCount = objProductMasterInfo.FetchProductSUSEPCODEMasterInfoUsingLobId();
                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;
            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;
        }//public Boolean FetchProductSUSEPCODEMasterInfo(ref Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo, ref DataTable dt)

        /// <summary>
        /// Update Product SUSEP CODE info data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public Boolean UpdateProductSUSEPCODEInfo(Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo)
        {
            Boolean returnValue = false;
            int retval = 0;
            if (objProductMasterInfo.RequiredTransactionLog)
            {
                objProductMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/ProductSusepCodeMasterDetails.aspx.resx");

                retval = objProductMasterInfo.UpdateProductSUSEPCODEInfo();
                if (retval > 0)
                    returnValue = true;
                else
                    returnValue = false;

            }//if (objProductMasterInfo.RequiredTransactionLog)
            return returnValue;
        }//public Boolean UpdateProductSUSEPCODEInfo(Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo)
        /// <summary>
        /// Insert Product SUSEP CODE info data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public Boolean InsertProductSUSEPCODEInfo(Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo)
        {
            Boolean returnValue = false;
            int retval = 0;
            if (objProductMasterInfo.RequiredTransactionLog)
            {
                objProductMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/ProductSusepCodeMasterDetails.aspx.resx");

                retval = objProductMasterInfo.InsertProductSUSEPCODEInfo();
                if (retval > 0)
                    returnValue = true;
                else
                    returnValue = false;

            }//if (objProductMasterInfo.RequiredTransactionLog)
            return returnValue;
        }//public Boolean InsertProductSUSEPCODEInfo(Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo)
        /// <summary>
        /// Delete Product SUSEP CODE info data
        /// </summary>
        /// <param name="objCommodityInfo"></param>
        /// <returns></returns>
        public Boolean DeleteProductSUSEPCODEInfo(Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo)
        {
            Boolean returnValue = false;
            int retval = 0;
            if (objProductMasterInfo.RequiredTransactionLog)
            {
                objProductMasterInfo.TransactLabel = ClsCommon.MapTransactionLabel("Cmsweb/Maintenance/ProductSusepCodeMasterDetails.aspx.resx");

                retval = objProductMasterInfo.DeleteProductSUSEPCODEInfo();
                if (retval > 0)
                    returnValue = true;
                else
                    returnValue = false;

            }//if (objProductMasterInfo.RequiredTransactionLog)
            return returnValue;
        }//public Boolean DeleteProductSUSEPCODEInfo(Cms.Model.Maintenance.ClsProductMasterInfo objProductMasterInfo)
        #endregion
    }
}
