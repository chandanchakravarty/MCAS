IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_VEHICLE_COVERAGES_COPY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_VEHICLE_COVERAGES_COPY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_GetAPP_VEHICLE_COVERAGES_COPY  
  
/*----------------------------------------------------------                  
Proc Name   : dbo.Proc_GetAPP_VEHICLE_COVERAGES_COPY        
Created by  : Pradeep        
Date        : 18 October,2005        
Purpose     : Get the coverages IDs for copying        
Revison History  :                        
MODIFIED by  : Pravesh        
Date        : 3 MArch 09
Purpose     : Get the coverages Desc

 ------------------------------------------------------------                              
Date     Review By          Comments                            
                   
------   ------------       -------------------------*/                  
CREATE PROCEDURE [dbo].[Proc_GetAPP_VEHICLE_COVERAGES_COPY]        
(        
 @VEHICLE_ID smallint,        
 @CUSTOMER_ID int,        
 @APP_ID int,        
 @APP_VERSION_ID int,      
 @CALLED_FROM VarChar(3)        
        
)            
AS                 
BEGIN                  
       
--IF (   @CALLED_FROM = 'PPA' OR @CALLED_FROM = 'VEH' OR @CALLED_FROM = 'MOT')      
--BEGIN      
      
 DECLARE @APP_EFFECTIVE_DATE DateTime          
 DECLARE @APP_INCEPTION_DATE DateTime     
                                                         
 DECLARE @STATEID SmallInt                                                          
 DECLARE @LOBID NVarCHar(5)                                                   
                                                           
 SELECT @STATEID = STATE_ID,                                                          
 @LOBID = APP_LOB,          
 @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,          
 @APP_INCEPTION_DATE = APP_INCEPTION_DATE                                                           
 FROM APP_LIST                                                          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                          
 APP_ID = @APP_ID AND                                       
 APP_VERSION_ID = @APP_VERSION_ID
 --Get the Coverages        
 SELECT C.COV_DES AS COV_DESC,AVC.*,        
        C.COV_ID,        
  C.COV_CODE,        
  C.IS_MANDATORY          
 FROM APP_VEHICLE_COVERAGES AVC        
 INNER JOIN MNT_COVERAGE C ON        
  AVC.COVERAGE_CODE_ID = C.COV_ID        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
 APP_ID = @APP_ID AND         
 APP_VERSION_ID = @APP_VERSION_ID  AND        
 VEHICLE_ID = @VEHICLE_ID         
             
 --Get the coverage ranges        
 SELECT MCR.*        
 FROM APP_VEHICLE_COVERAGES AVC        
 INNER JOIN MNT_COVERAGE C ON        
  AVC.COVERAGE_CODE_ID = C.COV_ID        
 INNER JOIN MNT_COVERAGE_RANGES MCR ON        
  MCR.COV_ID = C.COV_ID        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
 APP_ID = @APP_ID AND         
 APP_VERSION_ID = @APP_VERSION_ID  AND        
 VEHICLE_ID = @VEHICLE_ID        
    
----Table 2                                   
 --Get the State for the application                                    
 EXEC Proc_GetApplicationState @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID    
 
--Table 3
               
 --Get vehicle details                       
 SELECT *,'APP' AS CALLEDFROM       
 FROM APP_VEHICLES                     
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                              
 APP_ID = @APP_ID AND                                                          
    APP_VERSION_ID = @APP_VERSION_ID AND                                                          
    VEHICLE_ID = @VEHICLE_ID  

 --Table 4
SELECT convert(int,SUM(ITEM_VALUE)) as SUM_MIS FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES APP_MIS  
WHERE APP_MIS.CUSTOMER_ID = @CUSTOMER_ID   
 AND APP_MIS.APP_ID  = @APP_ID  
 AND APP_MIS.APP_VERSION_ID  = @APP_VERSION_ID
 AND APP_MIS.VEHICLE_ID=@VEHICLE_ID
 --Table 5

-- select COUNT(CUSTOMER_ID) AS NO_CLAIMS from APP_PRIOR_LOSS_INFO
--  where CUSTOMER_ID=@CUSTOMER_ID and LOSS_TYPE=9765 AND  OCCURENCE_DATE > DATEADD(YEAR,-3 ,@APP_EFFECTIVE_DATE)

DECLARE @NO_CLAIMS INT,@NO_PRIOR_LOSS INT
SELECT @NO_PRIOR_LOSS = COUNT(CUSTOMER_ID)  FROM APP_PRIOR_LOSS_INFO
	WHERE CUSTOMER_ID=@CUSTOMER_ID 
	--AND LOSS_TYPE=9765 
	AND ISNULL(CLAIMS_TYPE,0)=14234
--	AND OCCURENCE_DATE >= DATEADD(DD,-3*365 ,@APP_EFFECTIVE_DATE)   
    AND OCCURENCE_DATE >= DATEADD(DD,-DATEDIFF(DAY,DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE)   
SELECT @NO_CLAIMS=COUNT(C.CUSTOMER_ID)
	FROM CLM_CLAIM_INFO C WITH (NOLOCK)            
	WHERE C.CUSTOMER_ID=@CUSTOMER_ID
	AND ISNULL(C.IS_ACTIVE,'Y')='Y' 
	AND ISNULL(PINK_SLIP_TYPE_LIST,'') like '%13005%'  -- at fault
--	AND LOSS_DATE >=DATEADD(DD,-3*365 ,@APP_EFFECTIVE_DATE)   
    AND LOSS_DATE >=DATEADD(DD,-DATEDIFF(DAY,DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE) ,@APP_EFFECTIVE_DATE)   

--TABLE 5
SELECT @NO_CLAIMS+@NO_PRIOR_LOSS AS NO_CLAIMS

DECLARE @UNDER_25_AGE SMALLINT
SET @UNDER_25_AGE=0
IF EXISTS
( SELECT DRIVER_DOB FROM APP_DRIVER_DETAILS AD WITH(NOLOCK)
--	INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ASV WITH(NOLOCK) ON ASV.CUSTOMER_ID=AD.CUSTOMER_ID AND ASV.APP_ID=AD.APP_ID 
--	AND ASV.APP_VERSION_ID=AD.APP_VERSION_ID AND ASV.DRIVER_ID=AD.DRIVER_ID 
	--AND ASV.APP_VEHICLE_PRIN_OCC_ID<>'11931' 
--	AND ASV.VEHICLE_ID=@VEHICLE_ID
	WHERE AD.CUSTOMER_ID=@CUSTOMER_ID AND AD.APP_ID=@APP_ID AND AD.APP_VERSION_ID=@APP_VERSION_ID
	AND DBO.GETAGE(DRIVER_DOB ,@APP_EFFECTIVE_DATE) < 25
)
SET @UNDER_25_AGE=1

--Table 6
SELECT ISNULL(APPLY_PERS_UMB_POL,0) AS UMB_POL,@UNDER_25_AGE as UNDER_25_AGE FROM APP_AUTO_GEN_INFO WHERE 
 CUSTOMER_ID = @CUSTOMER_ID   
 AND APP_ID  = @APP_ID  
 AND APP_VERSION_ID  = @APP_VERSION_ID
                                             
  
--END      
 
/*     
IF (@CALLED_FROM = 'UMB')      
BEGIN      
 --Get the Coverages        
 SELECT AVC.*,        
        C.COV_ID,        
  C.COV_CODE,        
  C.IS_MANDATORY          
 FROM APP_UMBRELLA_VEHICLE_COV_IFNO AVC        
 INNER JOIN MNT_COVERAGE C ON        
  AVC.COVERAGE_CODE_ID = C.COV_ID        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
 APP_ID = @APP_ID AND         
 APP_VERSION_ID = @APP_VERSION_ID  AND        
 VEHICLE_ID = @VEHICLE_ID         
             
 --Get the coverage ranges        
 SELECT MCR.*        
 FROM APP_UMBRELLA_VEHICLE_COV_IFNO AVC        
 INNER JOIN MNT_COVERAGE C ON        
  AVC.COVERAGE_CODE_ID = C.COV_ID        
 INNER JOIN MNT_COVERAGE_RANGES MCR ON        
  MCR.COV_ID = C.COV_ID        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
 APP_ID = @APP_ID AND         
 APP_VERSION_ID = @APP_VERSION_ID  AND        
 VEHICLE_ID = @VEHICLE_ID        
      
----Table 2                                   
 --Get the State for the application                                    
 EXEC Proc_GetApplicationState @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID    
 
--Table 3
--Get Motorcycle details                 
 SELECT * 
 FROM APP_UMBRELLA_VEHICLES               
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                        
	APP_ID = @APP_ID AND                                                    
    APP_VERSION_ID = @APP_VERSION_ID AND                                                    
    VEHICLE_ID = @VEHICLE_ID       

END      
      
*/

END        
        
        
      
    
  


GO

