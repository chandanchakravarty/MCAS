IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNamedInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNamedInsured]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN                
--drop PROC dbo.Proc_GetNamedInsured                
--GO                
/*----------------------------------------------------------                                                                        
Proc Name       : dbo.Proc_GetNamedInsured                                                                  
Created by      : Sumit Chhabra                                                                      
Date            : 02/05/2006                                                                        
Purpose         : Get Named Insured/Insured                                
Created by      : Sumit Chhabra                                                                       
Revison History :                                                                        
Used In        : Wolverine                                                  
NAMED_INSURED,NAMED_INSURED_ID,ADDRESS1,ADDRESS2,CITY,STATE,COUNTRY,ZIP-CODE,PHONE                                   
TYPE_OF_DRIVER=1&CLAIM_ID=1134&CUSTOMER_ID=1009&POLICY_ID=1&POLICY_VERSION_ID=1&LOB_ID=4&TYPE_OF_OWNER=1&&transferdata=                      
------------------------------------------------------------                                                                        
Date     Review By          Comments                                                                        
--Shikha(20/5/2010): If some other driver (other than assigned driver of the vehicle) is selected while saving driver information then at the time of viewing the page,
--the selected name did not appear due to the chenges made against I Track 6053.Hence the following change madeto the query.
------   ------------       -------------------------*/                                                                        
--drop PROC dbo.Proc_GetNamedInsured 1009,24,1,11752,1215                        
CREATE PROC [dbo].[Proc_GetNamedInsured]                                                                                                
	@CUSTOMER_ID int,                                
	@POLICY_ID int,                                
	@POLICY_VERSION_ID int,                                
	@VEHICLE_OWNER int,                                
	@CLAIM_ID int,                
	@NAME_ID VARCHAR(20) = null,                      
	@NAMED_INSURED_TYPE VARCHAR(20) = NULL,                      
	@DRIVER_TYPE VARCHAR(30) = null,                
	@VEHICLE_ID int = 0                     
AS                                                                        
BEGIN                                  
                      
DECLARE @NAMED_INSURED_TYPE_CO_APP VARCHAR(30)                      
DECLARE @NAMED_INSURED_TYPE_DRIVER VARCHAR(30)                      
DECLARE @NAMED_INSURED_TYPE_OPERATOR VARCHAR(30)     
DECLARE @POLICY_VEHICLE_ID INT  --Done for Itrack Issue 6053 on 21 Aug 2009                 
SET @NAMED_INSURED_TYPE_CO_APP = 'NAMED_INSURED'                      
SET @NAMED_INSURED_TYPE_DRIVER = 'DRIVER'                      
SET @NAMED_INSURED_TYPE_OPERATOR = 'OPERATOR'                      
                   
                     
if(@DRIVER_TYPE='PP')                      
 SET @DRIVER_TYPE = 'PRINCIPAL'                      
ELSE if(@DRIVER_TYPE='OTHER')                      
 SET @DRIVER_TYPE = ''                      
ELSE                      
 SET @DRIVER_TYPE = 'OCCASIONAL'   
                   
                      
IF(@NAME_ID = '')                      
BEGIN                      
	SET @NAME_ID=NULL;                      
END                      
/*Lookup Unique ids of Vehicle Owner being enumerated                                 
   NAMED_INSURED = 11752,                                
   INSURED = 11753,                                
   NOT_ON_POLICY = 11754,                      
   RATED DRIVER          = 14151                      
*/                                
--We have to fetch insureds only for named_insured and insured, if not on policy comes, return                                
if(@VEHICLE_OWNER is null or @VEHICLE_OWNER=11754) return                                
                      
                      
--Select Named insureds for the current policy            
--Value of 0 for Vehicle_owner will be passed from driver details screen to fetch data for current policy                              
if(@VEHICLE_OWNER=11752 or @VEHICLE_OWNER=0 or @VEHICLE_OWNER=14151)                                
BEGIN                       

	IF (@NAME_ID IS NULL)                       
		if (@DRIVER_TYPE='')                      
		BEGIN                      
			-- select '1'                      
			SELECT                        
			CAST(ISNULL(T1.APPLICANT_ID,0) AS VARCHAR) + '^' + @NAMED_INSURED_TYPE_CO_APP AS NAMED_INSURED_ID,           
			T1.FIRST_NAME+ ' ' + isnull(T1.LAST_NAME,' ') AS NAMED_INSURED,                            
			T1.ADDRESS1,T1.ADDRESS2,T1.CITY,T1.STATE,T1.ZIP_CODE,T1.PHONE, --HomePhone                          
			T1.CO_APPLI_EMPL_PHONE AS WORK_PHONE,T1.EXT AS EXTENSION,                      
			--'' AS WORK_PHONE,'' AS EXTENSION,                      
			T1.MOBILE AS MOBILE_PHONE,T1.COUNTRY,                      
			CONVERT(VARCHAR,T1.CO_APPL_DOB,101) AS DATE_OF_BIRTH,                      
			MLV.LOOKUP_VALUE_DESC AS RELATION_INSURED                      
			FROM CLT_APPLICANT_LIST T1 left outer join POL_APPLICANT_LIST T2  ON T1.APPLICANT_ID=T2.APPLICANT_ID              LEFT OUTER JOIN CLM_CLAIM_INFO CLAIM ON T2.CUSTOMER_ID=CLAIM.CUSTOMER_ID AND                            
			T2.POLICY_ID=CLAIM.POLICY_ID AND T2.POLICY_VERSION_ID=CLAIM.POLICY_VERSION_ID                        
			LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON           
			CAST(MLV.LOOKUP_UNIQUE_ID AS VARCHAR) = CAST(T1.CO_APPL_RELATIONSHIP AS VARCHAR)                      
			WHERE CLAIM.CLAIM_ID=@CLAIM_ID AND T1.IS_ACTIVE='Y'                      
		END                      
		ELSE                      
		BEGIN                      
			IF((SELECT LOB_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID) = 4 
			--Done for Itrack Issue 6523 on 7 Oct 2009 -- BOAT CALLED FROM INSIDE OF HOME LOB	
			OR 
			(SELECT LOB_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID) = 1)
			-- Added By Swarup                       
			-- Modified by Asfa Praveen (11-Feb-2008) - iTrack issue 3598                      
          
			begin                      
			-- select '2'                      
				if(@VEHICLE_OWNER=11752)                                
				BEGIN   
					SELECT CAST(ISNULL(INSURED.APPLICANT_ID,0) AS VARCHAR) + '^' + 'NAMED_INSURED' AS NAMED_INSURED_ID,			 ISNULL(INSURED.FIRST_NAME,'') + ' '  + ISNULL(INSURED.LAST_NAME,'') AS NAMED_INSURED,	
					ISNULL(INSURED.ADDRESS1,'') AS ADDRESS1, ISNULL(INSURED.ADDRESS2,'') AS ADDRESS2,          
					ISNULL(INSURED.CITY,'') AS CITY,                      
					ISNULL(INSURED.STATE,0) AS STATE,ISNULL(INSURED.ZIP_CODE,'') AS ZIP_CODE,                      
					ISNULL(INSURED.COUNTRY,'') AS COUNTRY,                      
					CONVERT(VARCHAR,ISNULL(INSURED.CO_APPL_DOB,''),101) AS DATE_OF_BIRTH,          
					MLV.LOOKUP_VALUE_DESC AS RELATION_INSURED                      
					FROM CLT_APPLICANT_LIST INSURED                       
					LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON           
					CAST(MLV.LOOKUP_UNIQUE_ID AS VARCHAR) = CAST(INSURED.CO_APPL_RELATIONSHIP AS VARCHAR)   
					WHERE INSURED.CUSTOMER_ID=@CUSTOMER_ID AND INSURED.IS_ACTIVE!='N'                           
				end                      
				else if (@VEHICLE_OWNER=14151)                      
				BEGIN    
					--Done for Itrack Issue 6053 on 21 Aug 2009  
					IF((SELECT LOB_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID) = 4)
					BEGIN
						SELECT @POLICY_VEHICLE_ID = POLICY_BOAT_ID FROM CLM_INSURED_BOAT  
						WHERE CLAIM_ID=@CLAIM_ID AND BOAT_ID=@VEHICLE_ID 
					END
					--Done for Itrack Issue 6490 on 30 Sept 2009 -- BOAT CALLED FROM INSIDE OF HOME LOB
					ELSE IF((SELECT LOB_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID) = 1)
					BEGIN
						SELECT @POLICY_VEHICLE_ID = BOAT_ID FROM CLM_INSURED_BOAT  
						WHERE CLAIM_ID=@CLAIM_ID AND BOAT_ID=@VEHICLE_ID 
					END
     
					SELECT               
					CAST(ISNULL(DRIVER.DRIVER_ID,0) AS VARCHAR) + '^' + 'NAMED_INSURED' AS NAMED_INSURED_ID,              
					ISNULL(DRIVER.DRIVER_FNAME,'') + ' '  + ISNULL(DRIVER.DRIVER_LNAME,'') AS NAMED_INSURED,	
					ISNULL(DRIVER.DRIVER_ADD1,'') AS ADDRESS1, ISNULL(DRIVER.DRIVER_ADD2,'') AS ADDRESS2,        
					ISNULL(DRIVER.DRIVER_CITY,'') AS CITY,                      
					ISNULL(DRIVER.DRIVER_STATE,0) AS STATE,ISNULL(DRIVER.DRIVER_ZIP,'') AS ZIP_CODE,                      
					ISNULL(DRIVER.DRIVER_COUNTRY,'') AS COUNTRY,                      
					CONVERT(VARCHAR,ISNULL(DRIVER.DRIVER_DOB,''),101) AS DATE_OF_BIRTH,'' AS RELATION_INSURED,DRIVER.IS_ACTIVE,       LOOKUP.LOOKUP_ID,CLAIM_ID,LOOKUP.LOOKUP_VALUE_CODE                       
					FROM CLM_CLAIM_INFO CLAIM                       
					LEFT OUTER JOIN POL_WATERCRAFT_DRIVER_DETAILS DRIVER                       
					ON CLAIM.CUSTOMER_ID = DRIVER.CUSTOMER_ID AND CLAIM.POLICY_ID = DRIVER.POLICY_ID AND                      
					CLAIM.POLICY_VERSION_ID = DRIVER.POLICY_VERSION_ID JOIN POL_OPERATOR_ASSIGNED_BOAT ASSIGN                      
					ON DRIVER.CUSTOMER_ID = ASSIGN.CUSTOMER_ID AND DRIVER.POLICY_ID = ASSIGN.POLICY_ID AND                      
					DRIVER.POLICY_VERSION_ID = ASSIGN.POLICY_VERSION_ID AND                      
					DRIVER.DRIVER_ID = ASSIGN.DRIVER_ID               
					--Added for Itrack Issue 6053 30 July 2009 to remove duplicate driver name              
					LEFT OUTER JOIN POL_WATERCRAFT_INFO PWI WITH (NOLOCK)                  
					ON PWI.CUSTOMER_ID = ASSIGN.CUSTOMER_ID AND                      
					PWI.POLICY_ID = ASSIGN.POLICY_ID AND                      
					PWI.POLICY_VERSION_ID = ASSIGN.POLICY_VERSION_ID AND                  
					PWI.BOAT_ID=ASSIGN.BOAT_ID             
					--Added till here             
					JOIN MNT_LOOKUP_VALUES LOOKUP ON                      
					ASSIGN.APP_VEHICLE_PRIN_OCC_ID=LOOKUP.LOOKUP_UNIQUE_ID                      
					WHERE CLAIM_ID=@CLAIM_ID AND            
					--Added for Itrack Issue 6053 30 July 2009 to select Driver Name corresponding to the selected vehicle       
					PWI.BOAT_ID=@POLICY_VEHICLE_ID             
					AND DRIVER.IS_ACTIVE!='N' AND LOOKUP.LOOKUP_ID=1292                    
					AND LOOKUP.LOOKUP_VALUE_CODE LIKE ('%PP%') --Added for Itrack Issue 6523 26 Oct 2009   

				end                      
			END                      
			ELSE      
				-- select '3'      
				BEGIN

				--Done for Itrack Issue 6053 on 21 Aug 2009                  
				SELECT @POLICY_VEHICLE_ID = POLICY_VEHICLE_ID FROM CLM_INSURED_VEHICLE   
				WHERE CLAIM_ID=@CLAIM_ID AND INSURED_VEHICLE_ID=@VEHICLE_ID              

				SELECT           
				CAST(ISNULL(DRIVER.DRIVER_ID,0) AS VARCHAR) + '^' + @NAMED_INSURED_TYPE_DRIVER AS NAMED_INSURED_ID,          
				ISNULL(DRIVER.DRIVER_FNAME,'') + ' '  + ISNULL(DRIVER.DRIVER_LNAME,'') AS NAMED_INSURED,                      
				ISNULL(DRIVER.DRIVER_ADD1,'') AS ADDRESS1, ISNULL(DRIVER.DRIVER_ADD2,'') AS ADDRESS2,          
				ISNULL(DRIVER.DRIVER_CITY,'') AS CITY,                      
				ISNULL(DRIVER.DRIVER_STATE,0) AS STATE,ISNULL(DRIVER.DRIVER_ZIP,'') AS ZIP_CODE,          
				ISNULL(DRIVER.DRIVER_HOME_PHONE,'') AS PHONE,                      
				ISNULL(DRIVER.DRIVER_BUSINESS_PHONE,'') AS WORK_PHONE,      
				ISNULL(DRIVER.DRIVER_EXT,'') AS EXTENSION,                      
				ISNULL(DRIVER.DRIVER_MOBILE,'') AS MOBILE_PHONE,ISNULL(DRIVER.DRIVER_COUNTRY,'') AS COUNTRY,   
				CONVERT(VARCHAR,ISNULL(DRIVER.DRIVER_DOB,''),101) AS DATE_OF_BIRTH,'' AS RELATION_INSURED	
				FROM POL_DRIVER_DETAILS DRIVER WITH (NOLOCK)                      
				LEFT OUTER JOIN POL_DRIVER_ASSIGNED_VEHICLE ASSIGN WITH (NOLOCK)                      
				ON DRIVER.CUSTOMER_ID = ASSIGN.CUSTOMER_ID AND                      
				DRIVER.POLICY_ID = ASSIGN.POLICY_ID AND      
				DRIVER.POLICY_VERSION_ID = ASSIGN.POLICY_VERSION_ID AND                      
				DRIVER.DRIVER_ID = ASSIGN.DRIVER_ID                    
				--AND DRIVER.VEHICLE_ID=ASSIGN.VEHICLE_ID  --Done for Itrack Issue 5927 on 3 June 2009 -- To remove duplicate       records from Claims-> Driver -> cmbName                  
				--Done for Itrack Issue 5987 on 24 June 2009                   
				LEFT OUTER JOIN POL_VEHICLES PV WITH (NOLOCK)                  
				ON PV.CUSTOMER_ID = ASSIGN.CUSTOMER_ID AND                      
				PV.POLICY_ID = ASSIGN.POLICY_ID AND                      
				PV.POLICY_VERSION_ID = ASSIGN.POLICY_VERSION_ID AND                  
				PV.VEHICLE_ID=ASSIGN.VEHICLE_ID                     
				LEFT OUTER JOIN MNT_LOOKUP_VALUES LOOKUP WITH (NOLOCK)                      
				ON ASSIGN.APP_VEHICLE_PRIN_OCC_ID=LOOKUP.LOOKUP_UNIQUE_ID                      
				WHERE DRIVER.IS_ACTIVE='Y' AND DRIVER.POLICY_ID=@POLICY_ID AND DRIVER.POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER.CUSTOMER_ID=@CUSTOMER_ID 
				AND LOOKUP.LOOKUP_VALUE_CODE LIKE ('%' + @DRIVER_TYPE)            
				--Added for Itrack Issue 6053 30 July 2009 to select Driver Name corresponding to the selected vehicle        
				AND PV.VEHICLE_ID=@POLICY_VEHICLE_ID                

				--Shikha(20/5/2010)
				UNION

				SELECT CAST(ISNULL(P.DRIVER_ID,0) AS VARCHAR)+ '^' + @NAMED_INSURED_TYPE_DRIVER AS NAMED_INSURED_ID, [NAME],D.ADDRESS1, D.ADDRESS2,D.CITY, D.STATE, D.ZIP, D.HOME_PHONE, 
				D.WORK_PHONE, D.EXTENSION,D.MOBILE_PHONE,D.COUNTRY, 
				CONVERT(VARCHAR,ISNULL(D.DATE_OF_BIRTH,''),101) AS DATE_OF_BIRTH,'' AS RELATION_INSURED	
				--D.DATE_OF_BIRTH, D.RELATION_INSURED
				FROM CLM_DRIVER_INFORMATION D
				INNER JOIN CLM_CLAIM_INFO C 
				ON D.CLAIM_ID = C.CLAIM_ID
				INNER JOIN POL_DRIVER_DETAILS P ON 
				P.CUSTOMER_ID = C.CUSTOMER_ID 
				AND P.POLICY_ID = C.POLICY_ID 
				AND P.POLICY_VERSION_ID = C.POLICY_VERSION_ID
				AND ISNULL(P.DRIVER_FNAME,'') + ' '  + ISNULL(P.DRIVER_LNAME,'')  = D.[NAME]
				WHERE D.CLAIM_ID = @CLAIM_ID AND D.VEHICLE_ID = @VEHICLE_ID


	--     SELECT CAST(ISNULL(DRIVER.DRIVER_ID,0) AS VARCHAR) + '^' + @NAMED_INSURED_TYPE_DRIVER AS NAMED_INSURED_ID,ISNULL(DRIVER.DRIVER_FNAME,'') + ' '  + ISNULL(DRIVER.DRIVER_LNAME,'') AS NAMED_INSURED,                      
	--     ISNULL(DRIVER.DRIVER_ADD1,'') AS ADDRESS1, ISNULL(DRIVER.DRIVER_ADD2,'') AS ADDRESS2,ISNULL(DRIVER.DRIVER_CITY,'') AS CITY,                      
	--     ISNULL(DRIVER.DRIVER_STATE,0) AS STATE,ISNULL(DRIVER.DRIVER_ZIP,'') AS ZIP_CODE,ISNULL(DRIVER.DRIVER_HOME_PHONE,'') AS PHONE,                      
	--     ISNULL(DRIVER.DRIVER_BUSINESS_PHONE,'') AS WORK_PHONE, ISNULL(DRIVER.DRIVER_EXT,'') AS EXTENSION,                      
	--     ISNULL(DRIVER.DRIVER_MOBILE,'') AS MOBILE_PHONE,ISNULL(DRIVER.DRIVER_COUNTRY,'') AS COUNTRY,                      
	--     CONVERT(VARCHAR,ISNULL(DRIVER.DRIVER_DOB,''),101) AS DATE_OF_BIRTH,ISNULL(LOOKUP.LOOKUP_VALUE_DESC,'') AS RELATION_INSURED,                      
	--     DRIVER.IS_ACTIVE,LOOKUP.LOOKUP_ID,CLAIM_ID,LOOKUP.LOOKUP_VALUE_CODE                       
	--     FROM CLM_CLAIM_INFO CLAIM                       
	--     LEFT OUTER JOIN POL_DRIVER_DETAILS DRIVER                       
	--     ON                      
	--     CLAIM.CUSTOMER_ID = DRIVER.CUSTOMER_ID AND                      
	--     CLAIM.POLICY_ID = DRIVER.POLICY_ID AND           
	--     CLAIM.POLICY_VERSION_ID = DRIVER.POLICY_VERSION_ID                      
	--     JOIN POL_DRIVER_ASSIGNED_VEHICLE ASSIGN                      
	--     ON DRIVER.CUSTOMER_ID = ASSIGN.CUSTOMER_ID AND                      
	--     DRIVER.POLICY_ID = ASSIGN.POLICY_ID AND                      
	--     DRIVER.POLICY_VERSION_ID = ASSIGN.POLICY_VERSION_ID AND                      
	--     DRIVER.DRIVER_ID = ASSIGN.DRIVER_ID             
	--     JOIN MNT_LOOKUP_VALUES LOOKUP ON                      
	--     ASSIGN.APP_VEHICLE_PRIN_OCC_ID=LOOKUP.LOOKUP_UNIQUE_ID                      
	--     WHERE CLAIM_ID=@CLAIM_ID  AND DRIVER.IS_ACTIVE='Y'                       
	                      
	                      
	--    SELECT                        
	--      CAST(ISNULL(DRIVER.DRIVER_ID,0) AS VARCHAR) + '^' + @NAMED_INSURED_TYPE_DRIVER AS NAMED_INSURED_ID,ISNULL(DRIVER.DRIVER_FNAME,'') + ' '  + ISNULL(DRIVER.DRIVER_LNAME,'') AS NAMED_INSURED,                      
	--      ISNULL(DRIVER.DRIVER_ADD1,'') AS ADDRESS1, ISNULL(DRIVER.DRIVER_ADD2,'') AS ADDRESS2,ISNULL(DRIVER.DRIVER_CITY,'') AS CITY,                      
	--      ISNULL(DRIVER.DRIVER_STATE,0) AS STATE,ISNULL(DRIVER.DRIVER_ZIP,'') AS ZIP_CODE,ISNULL(DRIVER.DRIVER_HOME_PHONE,'') AS PHONE,                      
	--      ISNULL(DRIVER.DRIVER_BUSINESS_PHONE,'') AS WORK_PHONE, ISNULL(DRIVER.DRIVER_EXT,'') AS EXTENSION,               
	--      ISNULL(DRIVER.DRIVER_MOBILE,'') AS MOBILE_PHONE,ISNULL(DRIVER.DRIVER_COUNTRY,'') AS COUNTRY,                      
	--      CONVERT(VARCHAR,ISNULL(DRIVER.DRIVER_DOB,''),101) AS DATE_OF_BIRTH,ISNULL(LOOKUP1.LOOKUP_VALUE_DESC,'') AS RELATION_INSURED                       
	--      FROM CLM_CLAIM_INFO CLAIM                       
	--      LEFT OUTER JOIN POL_DRIVER_DETAILS DRIVER                       
	--      ON                      
	--      CLAIM.CUSTOMER_ID = DRIVER.CUSTOMER_ID AND                      
	--      CLAIM.POLICY_ID = DRIVER.POLICY_ID AND                      
	--      CLAIM.POLICY_VERSION_ID = DRIVER.POLICY_VERSION_ID    
	--      JOIN POL_DRIVER_ASSIGNED_VEHICLE ASSIGN                      
	--      ON DRIVER.CUSTOMER_ID = ASSIGN.CUSTOMER_ID AND                      
	--      DRIVER.POLICY_ID = ASSIGN.POLICY_ID AND                      
	--      DRIVER.POLICY_VERSION_ID = ASSIGN.POLICY_VERSION_ID AND                      
	--      DRIVER.DRIVER_ID = ASSIGN.DRIVER_ID                      
	--      JOIN MNT_LOOKUP_VALUES LOOKUP ON                      
	--      ASSIGN.APP_VEHICLE_PRIN_OCC_ID=LOOKUP.LOOKUP_UNIQUE_ID                      
	--      JOIN MNT_LOOKUP_VALUES LOOKUP1 ON                      
	--      DRIVER.RELATIONSHIP=LOOKUP1.LOOKUP_UNIQUE_ID                       
	--    WHERE CLAIM_ID=@CLAIM_ID  AND DRIVER.IS_ACTIVE='Y'                           
	--      AND LOOKUP.LOOKUP_ID=1215 AND LOOKUP.LOOKUP_VALUE_CODE LIKE ('%' + @DRIVER_TYPE)                      
	--                          
	--     UNION                       
	--          
	--    SELECT                       
	--      CAST(ISNULL(OPERATOR.DRIVER_ID,0) AS VARCHAR) + '^' + @NAMED_INSURED_TYPE_OPERATOR AS NAMED_INSURED_ID,ISNULL(DRIVER.DRIVER_FNAME,'') + ' '  + ISNULL(DRIVER.DRIVER_LNAME,'') AS NAMED_INSURED,                      
	--      ISNULL(DRIVER.DRIVER_ADD1,'') AS ADDRESS1, ISNULL(DRIVER.DRIVER_ADD2,'') AS ADDRESS2,ISNULL(DRIVER.DRIVER_CITY,'') AS CITY,                      
	--      ISNULL(DRIVER.DRIVER_STATE,0) AS STATE,ISNULL(DRIVER.DRIVER_ZIP,'') AS ZIP_CODE,                      
	--      '' AS PHONE,                      
	--      '' AS WORK_PHONE, '' AS EXTENSION,                      
	--      '' AS MOBILE_PHONE,ISNULL(DRIVER.DRIVER_COUNTRY,'') AS COUNTRY,                      
	--      CONVERT(VARCHAR,ISNULL(DRIVER.DRIVER_DOB,''),101) AS DATE_OF_BIRTH,'' AS RELATION_INSURED                       
	--      FROM CLM_CLAIM_INFO CLAIM                       
	--      LEFT OUTER JOIN POL_DRIVER_DETAILS DRIVER                    
	--      ON                      
	--      CLAIM.CUSTOMER_ID = DRIVER.CUSTOMER_ID AND                      
	--      CLAIM.POLICY_ID = DRIVER.POLICY_ID AND                      
	--      CLAIM.POLICY_VERSION_ID = DRIVER.POLICY_VERSION_ID                      
	-- --     full OUTER JOIN POL_WATERCRAFT_DRIVER_DETAILS OPERATOR                       
	--      left OUTER JOIN POL_OPERATOR_ASSIGNED_BOAT OPERATOR                       
	--      ON                      
	--      CLAIM.CUSTOMER_ID = OPERATOR.CUSTOMER_ID AND                      
	--      CLAIM.POLICY_ID = OPERATOR.POLICY_ID AND                      
	--      CLAIM.POLICY_VERSION_ID = OPERATOR.POLICY_VERSION_ID and                      
	--      DRIVER.DRIVER_ID = OPERATOR.DRIVER_ID                      
	--      JOIN MNT_LOOKUP_VALUES LOOKUP ON                      
	--      OPERATOR.APP_VEHICLE_PRIN_OCC_ID=LOOKUP.LOOKUP_UNIQUE_ID                      
	--      WHERE CLAIM_ID=@CLAIM_ID  AND DRIVER.IS_ACTIVE='Y'                      
	--      AND LOOKUP.LOOKUP_ID=1215 AND LOOKUP.LOOKUP_VALUE_DESC LIKE ('%' + @DRIVER_TYPE)         
	END             
END                      

ELSE      
BEGIN                       
                      
  IF(@NAMED_INSURED_TYPE = @NAMED_INSURED_TYPE_DRIVER) --When we have named insured as driver type                      
  begin                      
                  
-- select '4'                      
                      
   SELECT                       
   CAST(ISNULL(DRIVER.DRIVER_ID,0) AS VARCHAR) AS NAMED_INSURED_ID,ISNULL(DRIVER.DRIVER_FNAME,'') + ' '  +       
   ISNULL(DRIVER.DRIVER_LNAME,'') AS NAMED_INSURED,    
   ISNULL(DRIVER.DRIVER_ADD1,'') AS ADDRESS1, ISNULL(DRIVER.DRIVER_ADD2,'') AS ADDRESS2,      
   ISNULL(DRIVER.DRIVER_CITY,'') AS CITY,                      
   ISNULL(DRIVER.DRIVER_STATE,0) AS STATE,ISNULL(DRIVER.DRIVER_ZIP,'') AS ZIP_CODE,      
   ISNULL(DRIVER.DRIVER_HOME_PHONE,'') AS PHONE,                      
   ISNULL(DRIVER.DRIVER_BUSINESS_PHONE,'') AS WORK_PHONE, ISNULL(DRIVER.DRIVER_EXT,'') AS EXTENSION,                 ISNULL(DRIVER.DRIVER_MOBILE,'') AS MOBILE_PHONE,ISNULL(DRIVER.DRIVER_COUNTRY,'') AS COUNTRY,                      
   CONVERT(VARCHAR,ISNULL(DRIVER.DRIVER_DOB,''),101) AS DATE_OF_BIRTH,      
   ISNULL(MLV.LOOKUP_VALUE_DESC,'') AS RELATION_INSURED,                      
   ISNULL(DRIVER_LIC_STATE,'') AS LICENSE_STATE,ISNULL(DRIVER_DRIV_LIC,'') AS LICENSE_NUMBER,                      
   ISNULL(DRIVER.DRIVER_SSN,'') AS SSN_NO,ISNULL(DRIVER.DRIVER_SEX,'') AS SEX                       
   FROM POL_DRIVER_DETAILS DRIVER                       
   LEFT OUTER JOIN POL_DRIVER_ASSIGNED_VEHICLE ASSIGN                      
   ON DRIVER.CUSTOMER_ID = ASSIGN.CUSTOMER_ID AND                      
   DRIVER.POLICY_ID = ASSIGN.POLICY_ID AND                      
   DRIVER.POLICY_VERSION_ID = ASSIGN.POLICY_VERSION_ID AND                      
   DRIVER.DRIVER_ID = ASSIGN.DRIVER_ID                      
   AND DRIVER.VEHICLE_ID=ASSIGN.VEHICLE_ID                      
   LEFT OUTER JOIN MNT_LOOKUP_VALUES LOOKUP ON                      
   ASSIGN.APP_VEHICLE_PRIN_OCC_ID=LOOKUP.LOOKUP_UNIQUE_ID  
   LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID = DRIVER.RELATIONSHIP                      
   WHERE  DRIVER.IS_ACTIVE='Y' AND DRIVER.DRIVER_ID=CONVERT(SMALLINT,@NAME_ID)                      
   AND DRIVER.POLICY_ID=@POLICY_ID AND DRIVER.POLICY_VERSION_ID=@POLICY_VERSION_ID       
   AND DRIVER.CUSTOMER_ID=@CUSTOMER_ID                      
                      
--    SELECT                       
--     CAST(ISNULL(DRIVER.DRIVER_ID,0) AS VARCHAR) AS NAMED_INSURED_ID,ISNULL(DRIVER.DRIVER_FNAME,'') + ' '  + ISNULL(DRIVER.DRIVER_LNAME,'') AS NAMED_INSURED,                      
--     ISNULL(DRIVER.DRIVER_ADD1,'') AS ADDRESS1, ISNULL(DRIVER.DRIVER_ADD2,'') AS ADDRESS2,ISNULL(DRIVER.DRIVER_CITY,'') AS CITY,                      
--     ISNULL(DRIVER.DRIVER_STATE,0) AS STATE,ISNULL(DRIVER.DRIVER_ZIP,'') AS ZIP_CODE,ISNULL(DRIVER.DRIVER_HOME_PHONE,'') AS PHONE,                      
--     ISNULL(DRIVER.DRIVER_BUSINESS_PHONE,'') AS WORK_PHONE, ISNULL(DRIVER.DRIVER_EXT,'') AS EXTENSION,                      
--     ISNULL(DRIVER.DRIVER_MOBILE,'') AS MOBILE_PHONE,ISNULL(DRIVER.DRIVER_COUNTRY,'') AS COUNTRY,                      
--     CONVERT(VARCHAR,ISNULL(DRIVER.DRIVER_DOB,''),101) AS DATE_OF_BIRTH,ISNULL(LOOKUP1.LOOKUP_VALUE_DESC,'') AS RELATION_INSURED,                      
--   ISNULL(DRIVER_LIC_STATE,'') AS LICENSE_STATE,ISNULL(DRIVER_DRIV_LIC,'') AS LICENSE_NUMBER,                      
--     ISNULL(DRIVER.DRIVER_SSN,'') AS SSN_NO,ISNULL(DRIVER.DRIVER_SEX,'') AS SEX                       
--     FROM CLM_CLAIM_INFO CLAIM                       
--     LEFT OUTER JOIN POL_DRIVER_DETAILS DRIVER                       
--     JOIN POL_DRIVER_ASSIGNED_VEHICLE ASSIGN                      
--     ON DRIVER.CUSTOMER_ID = ASSIGN.CUSTOMER_ID AND                      
--     DRIVER.POLICY_ID = ASSIGN.POLICY_ID AND                      
--     DRIVER.POLICY_VERSION_ID = ASSIGN.POLICY_VERSION_ID AND                      
--     DRIVER.DRIVER_ID = ASSIGN.DRIVER_ID                      
--     JOIN MNT_LOOKUP_VALUES LOOKUP ON                      
--     ASSIGN.APP_VEHICLE_PRIN_OCC_ID=LOOKUP.LOOKUP_UNIQUE_ID                      
--     ON                      
--     CLAIM.CUSTOMER_ID = DRIVER.CUSTOMER_ID AND                      
--     CLAIM.POLICY_ID = DRIVER.POLICY_ID AND                      
--     CLAIM.POLICY_VERSION_ID = DRIVER.POLICY_VERSION_ID                      
--     JOIN MNT_LOOKUP_VALUES LOOKUP1 ON                  
--     DRIVER.RELATIONSHIP=LOOKUP1.LOOKUP_UNIQUE_ID                       
--    WHERE CLAIM_ID=@CLAIM_ID AND DRIVER.DRIVER_ID=@NAME_ID AND DRIVER.IS_ACTIVE='Y'                       
--     AND LOOKUP.LOOKUP_ID=1215 AND LOOKUP.LOOKUP_VALUE_CODE LIKE ('%' + @DRIVER_TYPE)                      
  end                      
  ELSE IF(@NAMED_INSURED_TYPE = @NAMED_INSURED_TYPE_OPERATOR) --When we have named insured as operator type                      
  begin          
-- select '5'                      
                      
   SELECT                       
     CAST(ISNULL(OPERATOR.DRIVER_ID,0) AS VARCHAR) + '^' + @NAMED_INSURED_TYPE_OPERATOR AS NAMED_INSURED_ID,        
  ISNULL(DRIVER.DRIVER_FNAME,'') + ' '  + ISNULL(DRIVER.DRIVER_LNAME,'') AS NAMED_INSURED,                      
     ISNULL(DRIVER.DRIVER_ADD1,'') AS ADDRESS1, ISNULL(DRIVER.DRIVER_ADD2,'') AS ADDRESS2,        
  ISNULL(DRIVER.DRIVER_CITY,'') AS CITY,                      
     ISNULL(DRIVER.DRIVER_STATE,0) AS STATE,ISNULL(DRIVER.DRIVER_ZIP,'') AS ZIP_CODE,'' AS PHONE,                      
     '' AS WORK_PHONE, '' AS EXTENSION,                      
     '' AS MOBILE_PHONE,ISNULL(DRIVER.DRIVER_COUNTRY,'') AS COUNTRY,                      
     ISNULL(DRIVER_LIC_STATE,'') AS LICENSE_STATE,ISNULL(DRIVER_DRIV_LIC,'') AS LICENSE_NUMBER,                       
     CONVERT(VARCHAR,ISNULL(DRIVER.DRIVER_DOB,''),101) AS DATE_OF_BIRTH,'' AS RELATION_INSURED,                      
   ISNULL(DRIVER.DRIVER_SSN,'') AS SSN_NO,ISNULL(DRIVER.DRIVER_SEX,'') AS SEX                        
     FROM CLM_CLAIM_INFO CLAIM                       
     LEFT OUTER JOIN POL_DRIVER_DETAILS DRIVER ON                      
     CLAIM.CUSTOMER_ID = DRIVER.CUSTOMER_ID AND                      
     CLAIM.POLICY_ID = DRIVER.POLICY_ID AND                      
     CLAIM.POLICY_VERSION_ID = DRIVER.POLICY_VERSION_ID                      
--     full OUTER JOIN POL_WATERCRAFT_DRIVER_DETAILS OPERATOR                       
     left OUTER JOIN POL_OPERATOR_ASSIGNED_BOAT OPERATOR ON                      
     CLAIM.CUSTOMER_ID = OPERATOR.CUSTOMER_ID AND                      
     CLAIM.POLICY_ID = OPERATOR.POLICY_ID AND                      
     CLAIM.POLICY_VERSION_ID = OPERATOR.POLICY_VERSION_ID and        
     DRIVER.DRIVER_ID = OPERATOR.DRIVER_ID                      
     JOIN MNT_LOOKUP_VALUES LOOKUP ON                      
     OPERATOR.APP_VEHICLE_PRIN_OCC_ID=LOOKUP.LOOKUP_UNIQUE_ID                      
     WHERE CLAIM_ID=@CLAIM_ID  AND DRIVER.IS_ACTIVE='Y'                      
     AND LOOKUP.LOOKUP_ID=1215 AND LOOKUP.LOOKUP_VALUE_DESC LIKE ('%' + @DRIVER_TYPE)                      
  end                      
  ELSE                      
  BEGIN                      
     IF((SELECT LOB_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID) = 4-- Added By Swarup                            --Done for Itrack Issue 6523 on 7 Oct 2009 -- BOAT CALLED FROM INSIDE OF HOME LOB	
	  OR 
	  (SELECT LOB_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID) = 1)
     -- Modified by Asfa Praveen (11-Feb-2008) - iTrack issue 3598                      
      begin                      
-- select '6'                      
    if(@VEHICLE_OWNER=11752)                                
    BEGIN                       
     SELECT INSURED.APPLICANT_ID AS NAMED_INSURED_ID,INSURED.FIRST_NAME + ' '  + 
	 ISNULL(INSURED.LAST_NAME,'') AS NAMED_INSURED,                      
     ISNULL(INSURED.ADDRESS1,'') AS ADDRESS1, ISNULL(INSURED.ADDRESS2,'') AS ADDRESS2,          
	 ISNULL(INSURED.CITY,'') AS CITY,                     
     ISNULL(INSURED.STATE,0) AS STATE,ISNULL(INSURED.ZIP_CODE,'') AS ZIP_CODE,                      
     ISNULL(PHONE,'') AS PHONE, --HomePhone                       
     INSURED.CO_APPLI_EMPL_PHONE AS WORK_PHONE,INSURED.EXT AS EXTENSION,                      
     -- ISNULL(BUSINESS_PHONE,'') AS WORK_PHONE,ISNULL(EXT,'') AS EXTENSION,                        
     ISNULL(MOBILE,'') AS MOBILE_PHONE,       
     ISNULL(INSURED.COUNTRY,'') AS COUNTRY,                      
     CONVERT(VARCHAR,ISNULL(INSURED.CO_APPL_DOB,''),101) AS DATE_OF_BIRTH,                      
     MLV.LOOKUP_VALUE_DESC AS RELATION_INSURED,                        
     '' AS LICENSE_STATE,'' AS LICENSE_NUMBER,ISNULL(INSURED.CO_APPL_SSN_NO,'') AS SSN_NO,                        
     ISNULL(INSURED.CO_APPL_GENDER,'') AS SEX                       
     FROM CLT_APPLICANT_LIST INSURED                       
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON           
	 CAST(MLV.LOOKUP_UNIQUE_ID AS VARCHAR) = CAST(INSURED.CO_APPL_RELATIONSHIP AS VARCHAR)   
     WHERE INSURED.CUSTOMER_ID=@CUSTOMER_ID                        
     AND INSURED.APPLICANT_ID = @NAME_ID AND                      
     INSURED.IS_ACTIVE!='N'                      
                           
    end                      
    else if (@VEHICLE_OWNER=14151)                      
    begin                      
     SELECT                       
     T1.DRIVER_ID AS NAMED_INSURED_ID, T1.DRIVER_FNAME+ ' ' + isnull(T1.DRIVER_LNAME,' ') AS NAMED_INSURED,                              
     T1.DRIVER_ADD1 AS ADDRESS1,T1.DRIVER_ADD2 AS ADDRESS2,T1.DRIVER_CITY AS CITY,T1.DRIVER_STATE AS STATE,  
     T1.DRIVER_ZIP AS ZIP_CODE, '' AS PHONE, --HomePhone                            
     '' AS WORK_PHONE,'' AS EXTENSION,                        
     '' AS MOBILE_PHONE,T1.DRIVER_COUNTRY AS COUNTRY,                        
     CONVERT(VARCHAR,T1.DRIVER_DOB,101) AS DATE_OF_BIRTH,                        
     '' AS RELATION_INSURED,                        
     T1.DRIVER_LIC_STATE AS LICENSE_STATE,T1.DRIVER_DRIV_LIC AS LICENSE_NUMBER,          
	 ISNULL(T1.DRIVER_SSN,'') AS SSN_NO,                        
     ISNULL(T1.DRIVER_SEX,'') AS SEX                         
     FROM POL_WATERCRAFT_DRIVER_DETAILS T1           
     LEFT OUTER JOIN POL_APPLICANT_LIST T2 ON T2.CUSTOMER_ID=T1.CUSTOMER_ID AND                              
     T2.POLICY_ID=T1.POLICY_ID AND T2.POLICY_VERSION_ID=T1.POLICY_VERSION_ID                       
     LEFT OUTER JOIN CLM_CLAIM_INFO CLAIM ON T1.CUSTOMER_ID=CLAIM.CUSTOMER_ID AND                              
     T1.POLICY_ID=CLAIM.POLICY_ID AND T1.POLICY_VERSION_ID=CLAIM.POLICY_VERSION_ID                           
     WHERE CLAIM.CLAIM_ID=@CLAIM_ID                       
     AND T1.DRIVER_ID = @NAME_ID                       
     AND T1.IS_ACTIVE='Y'                      
 end                      
   END                      
                      
   ELSE                      
   BEGIN                      
-- select '7'                      
    SELECT         
    T1.APPLICANT_ID AS NAMED_INSURED_ID, T1.FIRST_NAME+ ' ' + isnull(T1.LAST_NAME,' ') AS NAMED_INSURED, 
    T1.ADDRESS1,T1.ADDRESS2,T1.CITY,T1.STATE,T1.ZIP_CODE,T1.PHONE, --HomePhone                          
    T1.CO_APPLI_EMPL_PHONE AS WORK_PHONE,T1.EXT AS EXTENSION,                      
    -- '' AS WORK_PHONE,'' AS EXTENSION,                      
    T1.MOBILE AS MOBILE_PHONE,T1.COUNTRY,                      
    CONVERT(VARCHAR,T1.CO_APPL_DOB,101) AS DATE_OF_BIRTH,                      
    MLV.LOOKUP_VALUE_DESC AS RELATION_INSURED,                      
    '' AS LICENSE_STATE,'' AS LICENSE_NUMBER,ISNULL(T1.CO_APPL_SSN_NO,'') AS SSN_NO,                      
    ISNULL(T1.CO_APPL_GENDER,'') AS SEX                       
    FROM CLT_APPLICANT_LIST T1 left outer join POL_APPLICANT_LIST T2 ON T1.APPLICANT_ID=T2.APPLICANT_ID            LEFT OUTER JOIN CLM_CLAIM_INFO CLAIM ON T2.CUSTOMER_ID=CLAIM.CUSTOMER_ID AND                            
    T2.POLICY_ID=CLAIM.POLICY_ID AND T2.POLICY_VERSION_ID=CLAIM.POLICY_VERSION_ID                             
    LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON           
	CAST(MLV.LOOKUP_UNIQUE_ID AS VARCHAR) = CAST(T1.CO_APPL_RELATIONSHIP AS VARCHAR)                      
    WHERE CLAIM.CLAIM_ID=@CLAIM_ID AND T1.APPLICANT_ID = @NAME_ID AND T1.IS_ACTIVE='Y'                       
   END                      
  END                     
 END                      
                       
END                                       
else if(@VEHICLE_OWNER=11753) --select insured name for the current CLAIM DATA                                
  begin                      
/*SELECT                                 
 CAST(CLAIM_ID AS VARCHAR) + '^ '  as NAMED_INSURED_ID, ISNULL(CLAIMANT_NAME,'') as NAMED_INSURED,'' AS DATE_OF_BIRTH,                      
 '' AS RELATION_INSURED,ISNULL(ADDRESS1,'') AS ADDRESS1,ISNULL(ADDRESS2,'') AS ADDRESS2,ISNULL(CITY,'') AS CITY,                      
 ISNULL(STATE,'') AS STATE,ISNULL(ZIP,'') AS ZIP_CODE,ISNULL(HOME_PHONE,'') AS PHONE,                      
 ISNULL(WORK_PHONE,'') AS WORK_PHONE,ISNULL(EXTENSION,'') AS EXTENSION,ISNULL(MOBILE_PHONE,'') AS MOBILE_PHONE,                      
 ISNULL(COUNTRY,'') AS COUNTRY,'' AS DATE_OF_BIRTH,'' AS RELATION_INSURED,                      
 ISNULL(STATE,'') AS STATE,'' AS LICENSE_NUMBER,'' AS LICENSE_STATE,'' AS SSN_NO,'' AS SEX                      
 FROM CLM_CLAIM_INFO                       
WHERE                       
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND                       
 POLICY_VERSION_ID=@POLICY_VERSION_ID AND CLAIM_ID = @CLAIM_ID                                
*/                       
SELECT                                 
 CAST(CLAIM_ID AS VARCHAR) + '^ '  as NAMED_INSURED_ID, ISNULL(CLAIMANT_NAME,'') as NAMED_INSURED,CONVERT(VARCHAR,T1.CO_APPL_DOB,101) AS DATE_OF_BIRTH,                      
 MLV.LOOKUP_VALUE_DESC AS RELATION_INSURED,ISNULL(T1.ADDRESS1,'') AS ADDRESS1,ISNULL(T1.ADDRESS2,'') AS ADDRESS2,ISNULL(T1.CITY,'') AS CITY,                      
 ISNULL(T1.STATE,'') AS STATE,ISNULL(ZIP,'') AS ZIP_CODE,ISNULL(HOME_PHONE,'') AS PHONE,                      
 ISNULL(CO_APPLI_EMPL_PHONE,'') AS WORK_PHONE,ISNULL(EXT,'') AS EXTENSION,ISNULL(MOBILE_PHONE,'') AS MOBILE_PHONE,                      
 ISNULL(T1.COUNTRY,'') AS COUNTRY,                      
 ISNULL(T1.STATE,'') AS STATE,'' AS LICENSE_NUMBER,'' AS LICENSE_STATE,CO_APPL_SSN_NO AS SSN_NO,          
 CO_APPL_GENDER AS SEX                      
 FROM CLT_APPLICANT_LIST T1                       
 JOIN CLM_CLAIM_INFO CLAIM ON T1.CUSTOMER_ID=CLAIM.CUSTOMER_ID                       
 LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID=T1.CO_APPL_RELATIONSHIP  
 WHERE                       
 CLAIM.CUSTOMER_ID=@CUSTOMER_ID AND CLAIM.POLICY_ID=@POLICY_ID AND                       
 CLAIM.POLICY_VERSION_ID=@POLICY_VERSION_ID AND CLAIM.CLAIM_ID = @CLAIM_ID                       
 and IS_PRIMARY_APPLICANT=1                             
                      
--  SELECT                                 
--  CAST(CLAIM_ID AS VARCHAR) + '^ '  as NAMED_INSURED_ID, ISNULL(CLAIMANT_NAME,'') as NAMED_INSURED,CONVERT(VARCHAR,T1.CO_APPL_DOB,101) AS DATE_OF_BIRTH,                      
--  MLV.LOOKUP_VALUE_DESC AS RELATION_INSURED,ISNULL(T1.ADDRESS1,'') AS ADDRESS1,ISNULL(T1.ADDRESS2,'') AS ADDRESS2,ISNULL(T1.CITY,'') AS CITY,                      
--  ISNULL(T1.STATE,'') AS STATE,ISNULL(ZIP,'') AS ZIP_CODE,ISNULL(HOME_PHONE,'') AS PHONE,                      
--  ISNULL(WORK_PHONE,'') AS WORK_PHONE,ISNULL(EXTENSION,'') AS EXTENSION,ISNULL(MOBILE_PHONE,'') AS MOBILE_PHONE,                      
--  ISNULL(T1.COUNTRY,'') AS COUNTRY,                      
--  ISNULL(T1.STATE,'') AS STATE,'' AS LICENSE_NUMBER,'' AS LICENSE_STATE,CO_APPL_SSN_NO AS SSN_NO,CO_APPL_GENDER AS SEX                      
--  FROM CLT_APPLICANT_LIST T1                       
--  left outer join POL_APPLICANT_LIST T2  ON T1.APPLICANT_ID=T2.APPLICANT_ID                                 
--  LEFT OUTER JOIN CLM_CLAIM_INFO CLAIM ON T2.CUSTOMER_ID=CLAIM.CUSTOMER_ID AND                            
--  T2.POLICY_ID=CLAIM.POLICY_ID AND T2.POLICY_VERSION_ID=CLAIM.POLICY_VERSION_ID                             
--  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID=T1.CO_APPL_RELATIONSHIP                       
--  WHERE                       
--  CLAIM.CUSTOMER_ID=@CUSTOMER_ID AND CLAIM.POLICY_ID=@POLICY_ID AND                       
--  CLAIM.POLICY_VERSION_ID=@POLICY_VERSION_ID AND CLAIM.CLAIM_ID = @CLAIM_ID                              
                      
  end                      
                      
END                
                
--GO                 
--
--exec Proc_GetNamedInsured @CUSTOMER_ID=16050,@POLICY_ID=1,@POLICY_VERSION_ID=3,@VEHICLE_OWNER=14151,@CLAIM_ID=802,@DRIVER_TYPE=N'PP',@VEHICLE_ID=1
--
--
--ROLLBACK TRAN 
--

GO

