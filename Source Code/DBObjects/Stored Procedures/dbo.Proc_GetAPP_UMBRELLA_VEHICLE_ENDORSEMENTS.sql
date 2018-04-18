IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_UMBRELLA_VEHICLE_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_UMBRELLA_VEHICLE_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*                  
----------------------------------------------------------                      
Proc Name       : dbo.Proc_GetAPP_UMBRELLA_VEHICLE_ENDORSEMENTS                  
Created by      : Pradeep                    
Date            : Oct 17, 2005                   
Purpose         : Selects records from Umbrella Endorsements                      
Revison History :                      
Used In         : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------                     
drop proc dbo.Proc_GetAPP_UMBRELLA_VEHICLE_ENDORSEMENTS                  
*/                  
                  
CREATE            PROCEDURE dbo.Proc_GetAPP_UMBRELLA_VEHICLE_ENDORSEMENTS                  
(                  
 @CUSTOMER_ID int,                  
 @APP_ID int,                  
 @APP_VERSION_ID smallint,                  
 @VEHICLE_ID smallint,                
 @APP_TYPE Char(1)                
)                  
                  
As                  
                  
                
DECLARE @STATE_ID SmallInt                  
DECLARE @LOB_ID NVarCHar(5)  
DECLARE @APP_EFFECTIVE_DATE DATETIME
                
--N for New Business, R for renewal                
--DECLARE @APP_TYPE Char(1)                
                
--SET @APP_TYPE = 'N'                
                  
SELECT @STATE_ID = STATE_ID,                  
 @LOB_ID = APP_LOB ,
@APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE                
FROM APP_LIST                  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
 APP_ID = @APP_ID AND                  
 APP_VERSION_ID = @APP_VERSION_ID                  
    
SET @LOB_ID = 2    
        
---For renewal                
DECLARE @APP_COVERAGE_COUNT int                
DECLARE @PREV_APP_VERSION_ID smallint                
                
                
IF ( @APP_TYPE = 'R')                
BEGIN             
             
--Get row count in Vehicle endorsements table            
 SELECT @APP_COVERAGE_COUNT = COUNT(*)            
 FROM APP_UMBRELLA_VEHICLE_COV_IFNO            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
   APP_ID = @APP_ID AND                  
   APP_VERSION_ID = @APP_VERSION_ID                
                 
 SELECT @PREV_APP_VERSION_ID = MAX(APP_VERSION_ID)                
 FROM APP_LIST                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
   APP_ID = @APP_ID AND                  
   APP_VERSION_ID < @APP_VERSION_ID                
                 
 IF (@APP_COVERAGE_COUNT = 0)            
 BEGIN            
     
  --Get Endorsements from previous APP_VERSION_ID            
  SELECT  ED.ENDORSMENT_ID,            
   ED.DESCRIPTION as ENDORSEMENT,            
   ED.TYPE,            
   VE.REMARKS,            
   VE.VEHICLE_ENDORSEMENT_ID,          
   NULL  as Selected            
 FROM MNT_ENDORSMENT_DETAILS  ED             
 LEFT OUTER JOIN APP_UMBRELLA_VEHICLE_ENDORSEMENTS VE ON            
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID AND            
  VE.VEHICLE_ID = @VEHICLE_ID  AND       
  VE.CUSTOMER_ID = @CUSTOMER_ID  AND       
  VE.APP_ID = @APP_ID  AND       
VE.APP_VERSION_ID = @APP_VERSION_ID              
 WHERE ED.STATE_ID = @STATE_ID AND            
  ED.LOB_ID = @LOB_ID AND            
  ED.ENDORS_ASSOC_COVERAGE = 'N'            
 UNION            
  SELECT  ED.ENDORSMENT_ID,            
   ED.DESCRIPTION as ENDORSEMENT,            
   ED.TYPE,            
   VE.REMARKS,            
   VE.VEHICLE_ENDORSEMENT_ID,          
   (          
 SELECT COUNT(*)            
   FROM MNT_ENDORSMENT_DETAILS MED            
   INNER JOIN APP_UMBRELLA_VEHICLE_COV_IFNO AVC ON            
    MED.SELECT_COVERAGE =  AVC.COVERAGE_CODE_ID            
   WHERE MED.ENDORSMENT_ID = ED.ENDORSMENT_ID AND          
   AVC.CUSTOMER_ID = @CUSTOMER_ID AND               
       APP_ID = @APP_ID AND                  
       APP_VERSION_ID = @PREV_APP_VERSION_ID AND            
   MED.STATE_ID = @STATE_ID AND            
   MED.LOB_ID = @LOB_ID AND            
   AVC.VEHICLE_ID = @VEHICLE_ID            
   ) as Selected    
 FROM MNT_ENDORSMENT_DETAILS  ED             
 LEFT OUTER JOIN APP_UMBRELLA_VEHICLE_ENDORSEMENTS VE ON            
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID AND            
  VE.VEHICLE_ID = @VEHICLE_ID   AND       
  VE.CUSTOMER_ID = @CUSTOMER_ID  AND       
  VE.APP_ID = @APP_ID   AND      
     VE.APP_VERSION_ID = @APP_VERSION_ID        
 WHERE ED.STATE_ID = @STATE_ID AND            
  ED.LOB_ID = @LOB_ID AND            
  ED.ENDORS_ASSOC_COVERAGE = 'Y'           
          
 END            
 ELSE            
 BEGIN            
    
  SELECT  ED.ENDORSMENT_ID,            
   ED.DESCRIPTION as ENDORSEMENT,            
   ED.TYPE,            
   VE.REMARKS,            
   VE.VEHICLE_ENDORSEMENT_ID  ,          
   NULL as Selected           
 FROM MNT_ENDORSMENT_DETAILS  ED             
 LEFT OUTER JOIN APP_UMBRELLA_VEHICLE_ENDORSEMENTS VE ON            
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID AND            
  VE.VEHICLE_ID = @VEHICLE_ID    AND       
  VE.CUSTOMER_ID = @CUSTOMER_ID  AND       
  VE.APP_ID = @APP_ID    AND      
     VE.APP_VERSION_ID = @APP_VERSION_ID         
 WHERE ED.STATE_ID = @STATE_ID AND            
  ED.LOB_ID = @LOB_ID AND            
  ED.ENDORS_ASSOC_COVERAGE = 'N'            
 UNION            
  SELECT ED.ENDORSMENT_ID,            
   ED.DESCRIPTION as ENDORSEMENT,            
   ED.TYPE,            
   VE.REMARKS,            
   VE.VEHICLE_ENDORSEMENT_ID ,          
   (          
 SELECT COUNT(*)           
   FROM MNT_ENDORSMENT_DETAILS MED            
   INNER JOIN APP_UMBRELLA_VEHICLE_COV_IFNO AVC ON            
    MED.SELECT_COVERAGE =  AVC.COVERAGE_CODE_ID            
   WHERE MED.ENDORSMENT_ID = ED.ENDORSMENT_ID AND          
   AVC.CUSTOMER_ID = @CUSTOMER_ID AND                  
       APP_ID = @APP_ID AND                  
       APP_VERSION_ID = @APP_VERSION_ID AND            
   MED.STATE_ID = @STATE_ID AND            
   MED.LOB_ID = @LOB_ID AND            
   AVC.VEHICLE_ID = @VEHICLE_ID            
   )as Selected             
 FROM MNT_ENDORSMENT_DETAILS  ED             
 LEFT OUTER JOIN APP_UMBRELLA_VEHICLE_ENDORSEMENTS VE ON            
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID AND            
  VE.VEHICLE_ID = @VEHICLE_ID AND       
  VE.CUSTOMER_ID = @CUSTOMER_ID  AND       
  VE.APP_ID = @APP_ID    AND      
     VE.APP_VERSION_ID = @APP_VERSION_ID           
 WHERE ED.STATE_ID = @STATE_ID AND            
  ED.LOB_ID = @LOB_ID AND            
  ED.ENDORS_ASSOC_COVERAGE = 'Y'           
          
            
 END            
 /*                
 IF ( @PREV_APP_VERSION_ID IS NULL )                
 BEGIN                
  --RAISERROR('Previous application not found',16,1)                
 END                
 */                
             
                 
END                
                
--For New Business                
IF ( @APP_TYPE = 'N')                
BEGIN                
                  
 SELECT  ED.ENDORSMENT_ID,            
   ED.DESCRIPTION as ENDORSEMENT,            
   ED.TYPE,            
   VE.REMARKS,            
   VE.VEHICLE_ENDORSEMENT_ID   ,          
    NULL as Selected           
 FROM MNT_ENDORSMENT_DETAILS  ED             
 LEFT OUTER JOIN APP_UMBRELLA_VEHICLE_ENDORSEMENTS VE ON            
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID    AND       
  VE.CUSTOMER_ID = @CUSTOMER_ID  AND       
  VE.APP_ID = @APP_ID  AND       
  VE.APP_VERSIOn_ID =  @APP_VERSION_ID        
 WHERE ED.STATE_ID = @STATE_ID AND            
  ED.LOB_ID = @LOB_ID AND            
  ED.ENDORS_ASSOC_COVERAGE = 'N' AND            
  VE.VEHICLE_ID = @VEHICLE_ID            
          
 UNION            
  SELECT ED.ENDORSMENT_ID,            
   ED.DESCRIPTION as ENDORSEMENT,            
  ED.TYPE,            
   VE.REMARKS,            
   VE.VEHICLE_ENDORSEMENT_ID ,          
   (          
 SELECT COUNT(*)           
   FROM MNT_ENDORSMENT_DETAILS MED            
   INNER JOIN APP_UMBRELLA_VEHICLE_COV_IFNO AVC ON            
    MED.SELECT_COVERAGE =  AVC.COVERAGE_CODE_ID            
   WHERE MED.ENDORSMENT_ID = ED.ENDORSMENT_ID AND          
   AVC.CUSTOMER_ID = @CUSTOMER_ID AND                 
       APP_ID = @APP_ID AND                  
       APP_VERSION_ID = @APP_VERSION_ID AND            
   MED.STATE_ID = @STATE_ID AND            
   MED.LOB_ID = @LOB_ID AND            
   AVC.VEHICLE_ID = @VEHICLE_ID            
   )as Selected              
 FROM MNT_ENDORSMENT_DETAILS  ED             
 LEFT OUTER JOIN APP_UMBRELLA_VEHICLE_ENDORSEMENTS VE ON            
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID AND            
  VE.VEHICLE_ID = @VEHICLE_ID  AND       
  VE.CUSTOMER_ID = @CUSTOMER_ID  AND       
  VE.APP_ID = @APP_ID  AND      
      VE.APP_VERSIOn_ID = @APP_VERSION_ID         
 WHERE ED.STATE_ID = @STATE_ID AND            
  ED.LOB_ID = @LOB_ID AND            
  ED.ENDORS_ASSOC_COVERAGE = 'Y'           
          
            
END                
                
--Table 1                
            
                  
--Table 3              
--Get the State for the application              
EXEC Proc_GetApplicationState @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID              

 -- for addtion date                    
select ENDORSEMENT_ATTACH_ID,ENDORSEMENT_ID,ATTACH_FILE,VALID_DATE EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE,DISABLED_DATE,FORM_NUMBER,
 EDITION_DATE from MNT_ENDORSEMENT_ATTACHMENT  
  where    VALID_DATE <= @APP_EFFECTIVE_DATE  AND   @APP_EFFECTIVE_DATE<ISNULL(EFFECTIVE_TO_DATE,'3000-01-01')                  
                        
                      
                                  
            
                  
                  
                  
                
                   
                  
                  
                  
                
                
                
              
              
            
            
          
        
      
    
  







GO

