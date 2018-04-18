IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_VEHICLE_COVERAGES_COPY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_VEHICLE_COVERAGES_COPY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_GetPOL_VEHICLE_COVERAGES_COPY    
    
/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_GetPOL_VEHICLE_COVERAGES_COPY          
Created by  : Pradeep          
Date        : 22 Feb,2006          
Purpose     : Get the coverages IDs for copying          
Revison History  :                 
Modified by  : Pravesh
Date        : 3 Macrh 2009
Purpose     : Get the coverages Desc as well to maintain Tran LOG          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
------   ------------       -------------------------*/                    
CREATE PROCEDURE [dbo].[Proc_GetPOL_VEHICLE_COVERAGES_COPY]  
(          
 @VEHICLE_ID smallint,          
 @CUSTOMER_ID int,          
 @POLICY_ID int,          
 @POLICY_VERSION_ID int,        
 @CALLED_FROM VarChar(3)          
          
)              
AS                   
BEGIN                    
         
IF (   @CALLED_FROM = 'PPA' OR @CALLED_FROM = 'VEH' OR @CALLED_FROM = 'MOT')        
BEGIN        
       
   DECLARE @STATEID SmallInt                                                                
 DECLARE @LOBID NVarCHar(5)                                                                
 DECLARE @APP_EFFECTIVE_DATE DateTime                    
 DECLARE @APP_INCEPTION_DATE DateTime                                                             
 DECLARE @POLICY_STATUS NVARCHAR(20)                                                 
                                                                 
 SELECT @STATEID = STATE_ID,  
  @POLICY_STATUS = POLICY_STATUS,                                                                
  @LOBID = POLICY_LOB,                
  @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE  ,                
  @APP_INCEPTION_DATE = APP_INCEPTION_DATE                                                              
 FROM POL_CUSTOMER_POLICY_LIST                                                            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                                
  POLICY_ID = @POLICY_ID AND                                                                
  POLICY_VERSION_ID = @POLICY_VERSION_ID         
 --Get the Coverages          
 SELECT C.COV_DES as COV_DESC,AVC.*,          
        C.COV_ID,          
  C.COV_CODE,          
  C.IS_MANDATORY            
 FROM POL_VEHICLE_COVERAGES AVC          
 INNER JOIN MNT_COVERAGE C ON          
  AVC.COVERAGE_CODE_ID = C.COV_ID          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
 POLICY_ID = @POLICY_ID AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID  AND          
 VEHICLE_ID = @VEHICLE_ID           
               
 --Get the coverage ranges          
 SELECT MCR.*          
 FROM POL_VEHICLE_COVERAGES AVC          
 INNER JOIN MNT_COVERAGE C ON          
  AVC.COVERAGE_CODE_ID = C.COV_ID          
 INNER JOIN MNT_COVERAGE_RANGES MCR ON          
  MCR.COV_ID = C.COV_ID          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
 POLICY_ID = @POLICY_ID AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID  AND          
 VEHICLE_ID = @VEHICLE_ID      
  
----Table 2                                                            
-- --Get the State for the Application                                      
-- SELECT CS.STATE_ID,CS.STATE_NAME,A.POLICY_LOB as LOB_ID,APP_EFFECTIVE_DATE,ALL_DATA_VALID , YEAR(A.APP_EFFECTIVE_DATE ) as APP_YEAR                            
-- FROM POL_CUSTOMER_POLICY_LIST A                            
-- INNER JOIN MNT_COUNTRY_STATE_LIST CS ON                            
--  A.STATE_ID = CS.STATE_ID                       
-- WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND                            
--        A.POLICY_ID=@POLICY_ID     AND                            
--        A.POLICY_VERSION_ID=@POLICY_VERSION_ID AND                            
--   CS.COUNTRY_ID = 1     
--  
----Table 3  
----Get Motorcycle details 
--  
--  SELECT *, APP_USE_VEHICLE_ID AS USE_VEHICLE,APP_VEHICLE_PERTYPE_ID as VEHICLE_TYPE_PER  ,'POL' as CALLEDFROM
-- FROM POL_VEHICLES                 
-- WHERE  CUSTOMER_ID = @CUSTOMER_ID AND           
-- POLICY_ID = @POLICY_ID AND           
-- POLICY_VERSION_ID = @POLICY_VERSION_ID  AND          
-- VEHICLE_ID = @VEHICLE_ID       
        
END        
        
IF (@CALLED_FROM = 'UMB')        
BEGIN       
/*   
 --Get the Coverages          
 SELECT AVC.*,          
        C.COV_ID,          
  C.COV_CODE,          
  C.IS_MANDATORY            
 FROM APP_UMBRELLA_VEHICLE_COV_IFNO AVC          
 INNER JOIN MNT_COVERAGE C ON          
  AVC.COVERAGE_CODE_ID = C.COV_ID          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
 POLICY_ID = @POLICY_ID AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID  AND          
 VEHICLE_ID = @VEHICLE_ID           
               
 --Get the coverage ranges          
 SELECT MCR.*          
 FROM APP_UMBRELLA_VEHICLE_COV_IFNO AVC          
 INNER JOIN MNT_COVERAGE C ON          
  AVC.COVERAGE_CODE_ID = C.COV_ID          
 INNER JOIN MNT_COVERAGE_RANGES MCR ON          
  MCR.COV_ID = C.COV_ID          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
 POLICY_ID = @POLICY_ID AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID  AND          
 VEHICLE_ID = @VEHICLE_ID          
 */  
RETURN 1       
END        

--Get the State for the Application   
----Table 2                                     
 SELECT CS.STATE_ID,CS.STATE_NAME,A.POLICY_LOB as LOB_ID,APP_EFFECTIVE_DATE,ALL_DATA_VALID , YEAR(A.APP_EFFECTIVE_DATE ) as APP_YEAR                            
 FROM POL_CUSTOMER_POLICY_LIST A                            
 INNER JOIN MNT_COUNTRY_STATE_LIST CS ON                            
  A.STATE_ID = CS.STATE_ID                       
 WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND                            
        A.POLICY_ID=@POLICY_ID     AND                            
        A.POLICY_VERSION_ID=@POLICY_VERSION_ID AND                            
   CS.COUNTRY_ID = 1     
  
--Table 3  
--Get Motorcycle details       
 SELECT *, APP_USE_VEHICLE_ID AS USE_VEHICLE,APP_VEHICLE_PERTYPE_ID as VEHICLE_TYPE_PER,'POL' AS CALLEDFROM  
   FROM                             
  POL_VEHICLES      
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                                    
    POLICY_ID = @POLICY_ID AND                           
    POLICY_VERSION_ID = @POLICY_VERSION_ID                                                            
    AND  VEHICLE_ID = @VEHICLE_ID    
  
SELECT convert(int,SUM(ITEM_VALUE)) as SUM_MIS FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES   
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                    
    POLICY_ID = @POLICY_ID AND                           
    POLICY_VERSION_ID = @POLICY_VERSION_ID                                                            
    AND  VEHICLE_ID = @VEHICLE_ID    
  
 --table 5
--select COUNT(CUSTOMER_ID) AS NO_CLAIMS from APP_PRIOR_LOSS_INFO  
 -- where CUSTOMER_ID=@CUSTOMER_ID and LOSS_TYPE=9765 AND  OCCURENCE_DATE > DATEADD(YEAR,-3 ,@APP_EFFECTIVE_DATE)      
DECLARE @NO_CLAIMS INT,@NO_PRIOR_LOSS INT
SELECT @NO_PRIOR_LOSS = COUNT(CUSTOMER_ID)  FROM APP_PRIOR_LOSS_INFO
	WHERE CUSTOMER_ID=@CUSTOMER_ID 
	--AND LOSS_TYPE=9765 
    AND ISNULL(CLAIMS_TYPE,0)=14234
	--AND OCCURENCE_DATE >= DATEADD(DD,-3*365 ,@APP_EFFECTIVE_DATE)   
    AND OCCURENCE_DATE >= DATEADD(DD,-DATEDIFF(DAY,DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE)   
SELECT @NO_CLAIMS=COUNT(C.CUSTOMER_ID)
	FROM CLM_CLAIM_INFO C WITH (NOLOCK)            
	WHERE C.CUSTOMER_ID=@CUSTOMER_ID
	AND ISNULL(C.IS_ACTIVE,'Y')='Y' 
	AND ISNULL(PINK_SLIP_TYPE_LIST,'') like '%13005%'  -- at fault
	--AND LOSS_DATE >=DATEADD(DD,-3*365 ,@APP_EFFECTIVE_DATE)   
   AND LOSS_DATE >=DATEADD(DD,-DATEDIFF(DAY,DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE) ,@APP_EFFECTIVE_DATE)   

--TABLE 5
SELECT @NO_CLAIMS+@NO_PRIOR_LOSS AS NO_CLAIMS

DECLARE @UNDER_25_AGE SMALLINT
SET @UNDER_25_AGE=0

IF EXISTS
( SELECT DRIVER_DOB FROM POL_DRIVER_DETAILS AD WITH(NOLOCK)
--	INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ASV WITH(NOLOCK) ON ASV.CUSTOMER_ID=AD.CUSTOMER_ID AND ASV.POLICY_ID=AD.POLICY_ID 
--	AND ASV.POLICY_VERSION_ID=AD.POLICY_VERSION_ID AND ASV.DRIVER_ID=AD.DRIVER_ID 
	--AND ASV.APP_VEHICLE_PRIN_OCC_ID<>'11931' 
--	AND ASV.VEHICLE_ID=@VEHICLE_ID
	WHERE AD.CUSTOMER_ID=@CUSTOMER_ID AND AD.POLICY_ID=@POLICY_ID AND AD.POLICY_VERSION_ID=@POLICY_VERSION_ID
	AND DBO.GETAGE(DRIVER_DOB ,@APP_EFFECTIVE_DATE) < 25
)
SET @UNDER_25_AGE=1

  
--Table 6  
SELECT ISNULL(APPLY_PERS_UMB_POL,0) AS UMB_POL,@UNDER_25_AGE as UNDER_25_AGE FROM POL_AUTO_GEN_INFO WHERE   
 CUSTOMER_ID = @CUSTOMER_ID AND                                                    
    POLICY_ID = @POLICY_ID AND                           
    POLICY_VERSION_ID = @POLICY_VERSION_ID   
     
        
END          
          
          
        

GO

