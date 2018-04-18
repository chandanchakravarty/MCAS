IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFAuto_Excluded_Driver_and_Message]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFAuto_Excluded_Driver_and_Message]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Proc_GetPDFAuto_Excluded_Driver_and_Message
(                                
 @CUSTOMERID   int,                                
 @POLID        int,                                
 @VERSIONID   int,                                
 @VEHICLEID   int,  
 @CALLEDFROM  VARCHAR(20)                                
) 
AS                                
BEGIN     
-- Creat Temp Table and put excluded driver and "Extended Non-Owned Coverage for Named Individual Required" drivers on it  
CREATE TABLE #PEXCEXTEND_DRIVER                    
(     
 [DRIVER_NAME] NVARCHAR(100),  
 [EXCEXTEND_DRIVER] NVARCHAR(40),  
 [DRIVER_DOB] NVARCHAR(100),  
 [SIGNATURE_OBT] NVARCHAR(20)           
  
)   
  
-- Creat Temp Table and put excluded driver message on it  
CREATE TABLE #PEXCEXTEND_DRIVER_message                    
(     
 [DESCRIPTION_MESSAGE] TEXT,  
 [LIMIT_MESSAGE] TEXT,  
 [DEDUCTIBLE_MESSAGE] TEXT              
)                               
 IF (@CALLEDFROM='APPLICATION')                                
	 BEGIN                             
		---------------------------------------------------------------------------------  
		 --         A-95, a-96  DRIVER EXCLUSION DRIVER DETAILS (START)  
		---------------------------------------------------------------------------------  
		  
		  
		-- FETCH DB "EXTENDED NON-OWNED COVERAGE FOR NAMED INDIVIDUAL REQUIRED"  
		INSERT INTO #PEXCEXTEND_DRIVER  
		SELECT ISNULL(ADDS.DRIVER_FNAME,'') + ' ' + ISNULL(ADDS.DRIVER_MNAME,'') + ' ' + ISNULL(ADDS.DRIVER_LNAME,'') , ADDS.EXT_NON_OWN_COVG_INDIVI , CONVERT(NVARCHAR(100),ADDS.DRIVER_DOB,101),CONVERT(NVARCHAR(20),ADDS.FORM_F95)   FROM 
		--APP_DRIVER_ASSIGNED_VEHICLE ADAV INNER JOIN   
		APP_DRIVER_DETAILS ADDS  with(nolock)  --ON ADAV.DRIVER_ID = ADDS.DRIVER_ID AND ADAV.CUSTOMER_ID = ADDS.CUSTOMER_ID AND ADAV.APP_ID = ADDS.APP_ID AND ADAV.APP_VERSION_ID= ADDS.APP_VERSION_ID  
		WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@POLID AND ADDS.APP_VERSION_ID=@VERSIONID AND ADDS.EXT_NON_OWN_COVG_INDIVI = 10963  
		  
		-- FETCH DB EXCLUDED DRIVER  
		INSERT INTO #PEXCEXTEND_DRIVER  
		SELECT ISNULL(ADDS.DRIVER_FNAME,'') + ' ' + ISNULL(ADDS.DRIVER_MNAME,'') + ' ' + ISNULL(ADDS.DRIVER_LNAME,''), ADDS.DRIVER_DRIV_TYPE , CONVERT(NVARCHAR(100),ADDS.DRIVER_DOB,101),CONVERT(NVARCHAR(20),ADDS.FORM_F95) FROM APP_DRIVER_DETAILS ADDS    
		with(nolock)  
		WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@POLID AND ADDS.APP_VERSION_ID=@VERSIONID AND ADDS.DRIVER_DRIV_TYPE = 3477   
		-- SELECT FROM TEMP TABLE  
		SELECT * FROM #PEXCEXTEND_DRIVER  
		---------------------------------------------------------------------------------  
		 --         A-95, a-96  DRIVER EXCLUSION DRIVER DETAILS (END)  
		---------------------------------------------------------------------------------  
	END     
 ELSE IF(@CALLEDFROM='POLICY')
	BEGIN
		---------------------------------------------------------------------------------  
		 --         A-95, a-96  DRIVER EXCLUSION DRIVER DETAILS (START)  
		---------------------------------------------------------------------------------  
		  
		-- FETCH DB "EXTENDED NON-OWNED COVERAGE FOR NAMED INDIVIDUAL REQUIRED"  
		INSERT INTO #PEXCEXTEND_DRIVER  
		SELECT ISNULL(PDDS.DRIVER_FNAME,'') + ' ' + ISNULL(PDDS.DRIVER_MNAME,'') + ' ' + ISNULL(PDDS.DRIVER_LNAME,'') , PDDS.EXT_NON_OWN_COVG_INDIVI , CONVERT(NVARCHAR(100),PDDS.DRIVER_DOB,101),CONVERT(NVARCHAR(20),PDDS.FORM_F95)    FROM 
		--POL_DRIVER_ASSIGNED_VEHICLE PDAV   
		--INNER JOIN   
		POL_DRIVER_DETAILS PDDS   
		--ON PDAV.DRIVER_ID = PDDS.DRIVER_ID and PDAV.CUSTOMER_ID = PDDS.CUSTOMER_ID AND PDAV.POLICY_ID=PDDS.POLICY_ID AND PDAV.POLICY_VERSION_ID=PDDS.POLICY_VERSION_ID  
		 WHERE PDDS.CUSTOMER_ID=@CUSTOMERID AND PDDS.POLICY_ID=@POLID AND PDDS.POLICY_VERSION_ID=@VERSIONID  and  PDDS.EXT_NON_OWN_COVG_INDIVI=10963  
		  
		-- FETCH DB EXCLUDED DRIVER  
		INSERT INTO #PEXCEXTEND_DRIVER  
		SELECT ISNULL(PDDS.DRIVER_FNAME,'') + ' ' + ISNULL(PDDS.DRIVER_MNAME,'') + ' ' + ISNULL(PDDS.DRIVER_LNAME,'') , PDDS.DRIVER_DRIV_TYPE , CONVERT(NVARCHAR(100),PDDS.DRIVER_DOB,101),CONVERT(NVARCHAR(20),PDDS.FORM_F95)   FROM POL_DRIVER_DETAILS PDDS    
		with(nolock) 
		 WHERE PDDS.CUSTOMER_ID=@CUSTOMERID AND PDDS.POLICY_ID=@POLID AND PDDS.POLICY_VERSION_ID=@VERSIONID AND PDDS.DRIVER_DRIV_TYPE=3477  
		  
		-- SELECT FROM TEMP TABLE  
		SELECT * FROM #PEXCEXTEND_DRIVER  
		  
		---------------------------------------------------------------------------------  
		 --         A-95, a-96  DRIVER EXCLUSION DRIVER DETAILS (END)  
		---------------------------------------------------------------------------------  
	END   
	 --  TEMP TABLE  
  
DECLARE @GARRAGEZIPSTATE NVARCHAR(100)  
IF (@CALLEDFROM='APPLICATION')  
	BEGIN   
		 SELECT @GARRAGEZIPSTATE =CONVERT(NVARCHAR(100),STATE)  FROM APP_VEHICLES WITH (NOLOCK)  INNER JOIN MNT_TERRITORY_CODES WITH (NOLOCK)  ON   
		 SUBSTRING(LTRIM(RTRIM(GRG_ZIP)),1,5) = ZIP WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND VEHICLE_ID=@VEHICLEID  
	END  
ELSE IF(@CALLEDFROM='POLICY')  
	BEGIN   
		 SELECT   
		 @GARRAGEZIPSTATE =CONVERT(NVARCHAR(100),STATE)  FROM POL_VEHICLES WITH (NOLOCK)  INNER JOIN MNT_TERRITORY_CODES WITH (NOLOCK)  ON   
		 SUBSTRING(LTRIM(RTRIM(GRG_ZIP)),1,5) = ZIP WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND VEHICLE_ID=@VEHICLEID  
	END  
IF(@GARRAGEZIPSTATE = '22')  
	BEGIN   
		INSERT INTO #PEXCEXTEND_DRIVER_MESSAGE  
		SELECT '   WARNING - When a named excluded person operates a vehicle all liability coverag','e is void - no one is insured. Owners',' of the vehicle and others'  
		  
		INSERT INTO #PEXCEXTEND_DRIVER_MESSAGE  
		SELECT '   legally responsible for the acts of the named excluded person remain fully personall','y liable.',''  
	END  
  
ELSE IF(@GARRAGEZIPSTATE = '14')  
	BEGIN  
		INSERT INTO #PEXCEXTEND_DRIVER_MESSAGE  
		SELECT '   WARNING - The excluded person(s), owner and/or registrant will not be covered fo',' r the insurance afforded by the poli','cy when your covered '  
		  
		INSERT INTO #PEXCEXTEND_DRIVER_MESSAGE  
		SELECT '   auto is being used or operated by or at the direction of the excluded driver.','',''  
	END  
	-- SELECT FROM TEMP TABLE  
	SELECT * FROM #PEXCEXTEND_DRIVER_MESSAGE  
	  
	DROP TABLE #PEXCEXTEND_DRIVER  
	DROP TABLE #PEXCEXTEND_DRIVER_MESSAGE                           
END   


GO

