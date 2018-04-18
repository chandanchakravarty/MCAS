IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_DWELLING_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_DWELLING_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*                            
----------------------------------------------------------                                
Proc Name       : dbo.Proc_GetPOL_DWELLING_ENDORSEMENTS                            
Created by      : SHAFI                              
Date            : 20 FEB 2006                             
Purpose         : Selects records from Endorsements                                
Revison History :                                
Used In         : Wolverine                                
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------                               
*/                            
--DROP PROC dbo.Proc_GetPOL_DWELLING_ENDORSEMENTS                            
CREATE            PROCEDURE dbo.Proc_GetPOL_DWELLING_ENDORSEMENTS                            
          
(                            
  @CUSTOMER_ID int,                            
  @POLICY_ID int,                            
  @POLICY_VERSION_ID smallint,                            
  @DWELLING_ID smallint,                          
  @POLICY_TYPE Char(1)                          
)                            
                            
As                            
                            
                          
DECLARE @STATE_ID SmallInt                        
DECLARE @LOB_ID NVarCHar(5)                              
DECLARE @APP_EFFECTIVE_DATE DateTime                    
DECLARE @POLICY_STATUS NVARCHAR(20)                                               
                    
                        
SELECT @STATE_ID = STATE_ID,  
  @POLICY_STATUS = POLICY_STATUS,                                                                
  @LOB_ID = POLICY_LOB,                
  @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE     
  FROM POL_CUSTOMER_POLICY_LIST                              
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
 POLICY_ID = @POLICY_ID AND                              
 POLICY_VERSION_ID = @POLICY_VERSION_ID                            
                          
---For renewal                          
DECLARE @POLICY_COVERAGE_COUNT int                          
DECLARE @PREV_POLICY_VERSION_ID smallint                          
                          
                          
                  
                          
DECLARE @IDENT_COL INT                
 SET  @IDENT_COL = 1                
         
 --Holds effective dates of all versions of current policy                
 DECLARE @TEMP_POLICY_LIST TABLE                
 (                
 IDENT_COL INT IDENTITY (1,1),                
 APP_EFFECTIVE_DATE DATETIME,                
 POLICY_VERSION_ID INT                
 )                
               
 --Holds coverages applicable to each version of this policy                
 DECLARE @TEMP_ENDORSMENT TABLE                
 (                
 ENDORSMENT_ID INT                
 )                
          
  
 -- Insert APP_EFFECTIVE_DATE of all versions of this policy in temporary table  
 INSERT INTO @TEMP_POLICY_LIST                
 (                
 APP_EFFECTIVE_DATE,                
 POLICY_VERSION_ID                
 )              
 SELECT APP_EFFECTIVE_DATE,POLICY_VERSION_ID                
 FROM POL_CUSTOMER_POLICY_LIST                
   WHERE CUSTOMER_ID = @CUSTOMER_ID   
  AND POLICY_ID = @POLICY_ID                 
  
 DECLARE @APP_EFF_DATE DateTime                
 DECLARE @CURRENT_VERSION_ID Int                          
  
 WHILE 1 = 1                
 BEGIN                
 IF NOT EXISTS                
   (                
     SELECT IDENT_COL FROM @TEMP_POLICY_LIST               
     WHERE IDENT_COL = @IDENT_COL                
    )                
   BEGIN                
     BREAK                
   END                
                  
  SELECT @APP_EFF_DATE = APP_EFFECTIVE_DATE,                
    @CURRENT_VERSION_ID = POLICY_VERSION_ID                
   FROM @TEMP_POLICY_LIST                
 WHERE IDENT_COL = @IDENT_COL                
                  
                   
 /*                
 Insert into temp table the list of all endorsment which where available in all versions                
 and which has date range between each of the effective date.                
 Also get coverages which were applicable during any endorsement process                
 */                
 INSERT INTO @TEMP_ENDORSMENT                
 SELECT ENDORSMENT_ID FROM MNT_ENDORSMENT_DETAILS                
 WHERE @APP_EFF_DATE BETWEEN ISNULL(EFFECTIVE_FROM_DATE,'1950-01-01') AND ISNULL(EFFECTIVE_TO_DATE,'3000-01-01 16:50:49.333')   
  AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFF_DATE              
  AND LOB_ID = @LOB_ID   
  AND STATE_ID = @STATE_ID   
  AND ENDORSMENT_ID NOT IN (SELECT ENDORSMENT_ID FROM @TEMP_ENDORSMENT)                
   
 SET @IDENT_COL = @IDENT_COL + 1                
END  --- End While Loop              
          
  
  
--Insert Endorsment which were opted in any previous version though not applicable to that version  
 INSERT INTO @TEMP_ENDORSMENT   
 (  
  ENDORSMENT_ID  
 )   
 SELECT DISTINCT ENDORSEMENT_ID FROM POL_DWELLING_ENDORSEMENTS  
 WHERE  
  CUSTOMER_ID = @CUSTOMER_ID     
  AND POLICY_ID = @POLICY_ID    
  AND  ENDORSEMENT_ID  IS NOT NULL  
  
          
--If New business select all Endorsments including Grandfathered  
IF @POLICY_STATUS = 'UISSUE' or @POLICY_STATUS = 'SUSPENDED'    
BEGIN   
 SELECT  ED.ENDORSMENT_ID,                        
  ED.DESCRIPTION as ENDORSEMENT,                        
      ED.ENDORSEMENT_CODE,                  
  ED.TYPE,                   
  VE.REMARKS,                        
  VE.DWELLING_ENDORSEMENT_ID   ,                      
  NULL as Selected,        
  ED.RANK,  
  ED.EFFECTIVE_FROM_DATE,  
  ED.EFFECTIVE_TO_DATE,
  VE.EDITION_DATE     
 FROM MNT_ENDORSMENT_DETAILS  ED                         
 LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS VE ON                        
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID   
  AND VE.CUSTOMER_ID = @CUSTOMER_ID  
  AND VE.POLICY_ID = @POLICY_ID  
  AND VE.POLICY_VERSION_ID =  @POLICY_VERSION_ID   
  AND VE.DWELLING_ID = @DWELLING_ID                  
 WHERE ED.STATE_ID = @STATE_ID   
  AND ED.LOB_ID = @LOB_ID   
  AND ED.ENDORS_ASSOC_COVERAGE = 'N'                     
  AND ED.IS_ACTIVE='Y'   
  AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')  
  OR   
  (  
   VE.ENDORSEMENT_ID is not null and ED.ENDORS_ASSOC_COVERAGE = 'N'   
  )  
 UNION                        
 SELECT  ED.ENDORSMENT_ID,                        
  ED.DESCRIPTION as ENDORSEMENT,                        
      ED.ENDORSEMENT_CODE,                  
  ED.TYPE,                        
  VE.REMARKS,                        
  VE.DWELLING_ENDORSEMENT_ID ,                   
  (                      
   SELECT COUNT(*)                        
   FROM MNT_ENDORSMENT_DETAILS MED                        
   INNER JOIN APP_DWELLING_SECTION_COVERAGES AVC ON                        
   MED.SELECT_COVERAGE =  AVC.COVERAGE_CODE_ID                        
   WHERE MED.ENDORSMENT_ID = ED.ENDORSMENT_ID   
   AND AVC.CUSTOMER_ID = @CUSTOMER_ID  
   AND POLICY_ID = @POLICY_ID   
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
   AND MED.STATE_ID = @STATE_ID  
   AND MED.LOB_ID = @LOB_ID   
   AND AVC.DWELLING_ID = @DWELLING_ID   
   AND MED.IS_ACTIVE='Y'                       
  )as Selected,        
  ED.RANK,  
  ED.EFFECTIVE_FROM_DATE,  
  ED.EFFECTIVE_TO_DATE ,
  VE.EDITION_DATE        
 FROM MNT_ENDORSMENT_DETAILS  ED                         
 LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS VE ON       
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID  
  AND VE.DWELLING_ID = @DWELLING_ID   
  AND VE.CUSTOMER_ID = @CUSTOMER_ID   
  AND VE.POLICY_ID = @POLICY_ID    
  AND VE.POLICY_VERSION_ID = @POLICY_VERSION_ID                     
 WHERE ED.STATE_ID = @STATE_ID  
  AND ED.LOB_ID = @LOB_ID   
  AND ED.ENDORS_ASSOC_COVERAGE = 'Y'   
  AND ED.IS_ACTIVE='Y'   
  AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')  
  OR   
  (  
   VE.ENDORSEMENT_ID is not null and ED.ENDORS_ASSOC_COVERAGE = 'Y'   
  )  
 ORDER BY ED.RANK                    
  
END  
-- End OF New Business  
  
-- In case of renewal Select all active Endorsment and   
-- Endorsment which are visible in any previous version of policy and are not disabled  
ELSE IF @POLICY_STATUS = 'URENEW'   
BEGIN  
 SELECT  ED.ENDORSMENT_ID,                        
  ED.DESCRIPTION as ENDORSEMENT,                        
      ED.ENDORSEMENT_CODE,                  
  ED.TYPE,                   
  VE.REMARKS,                        
  VE.DWELLING_ENDORSEMENT_ID   ,                      
  NULL as Selected,        
  ED.RANK,  
  ED.EFFECTIVE_FROM_DATE,  
  ED.EFFECTIVE_TO_DATE  ,
  VE.EDITION_DATE      
 FROM MNT_ENDORSMENT_DETAILS  ED                         
 LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS VE ON                        
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID    
  AND VE.CUSTOMER_ID = @CUSTOMER_ID    
  AND VE.POLICY_ID = @POLICY_ID    
  AND VE.POLICY_VERSION_ID =  @POLICY_VERSION_ID  
  AND VE.DWELLING_ID = @DWELLING_ID                  
 WHERE ED.STATE_ID = @STATE_ID   
  AND ED.LOB_ID = @LOB_ID   
  AND ED.ENDORS_ASSOC_COVERAGE = 'N'   
  AND ED.IS_ACTIVE='Y'  
  AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')  
  AND ED.ENDORSMENT_ID IN  
  (  
   SELECT DISTINCT  ENDORSMENT_ID FROM @TEMP_ENDORSMENT  
  )  
  OR   
  (  
   VE.ENDORSEMENT_ID is not null and ED.ENDORS_ASSOC_COVERAGE = 'N'   
  )  
    
 UNION  
      
 SELECT  ED.ENDORSMENT_ID,                        
  ED.DESCRIPTION as ENDORSEMENT,                        
      ED.ENDORSEMENT_CODE,                  
  ED.TYPE,                        
  VE.REMARKS,                        
  VE.DWELLING_ENDORSEMENT_ID ,                   
  (                      
   SELECT COUNT(*)                        
   FROM MNT_ENDORSMENT_DETAILS MED                        
   INNER JOIN APP_DWELLING_SECTION_COVERAGES AVC ON                        
   MED.SELECT_COVERAGE =  AVC.COVERAGE_CODE_ID                        
   WHERE MED.ENDORSMENT_ID = ED.ENDORSMENT_ID   
   AND AVC.CUSTOMER_ID = @CUSTOMER_ID  
   AND POLICY_ID = @POLICY_ID   
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
   AND MED.STATE_ID = @STATE_ID   
   AND MED.LOB_ID = @LOB_ID   
   AND AVC.DWELLING_ID = @DWELLING_ID   
   AND MED.IS_ACTIVE='Y'                       
  )as Selected,        
  ED.RANK,  
  ED.EFFECTIVE_FROM_DATE,  
  ED.EFFECTIVE_TO_DATE   ,
  VE.EDITION_DATE     
 FROM MNT_ENDORSMENT_DETAILS  ED                         
 LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS VE ON                        
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID  
  AND VE.DWELLING_ID = @DWELLING_ID    
  AND VE.CUSTOMER_ID = @CUSTOMER_ID   
  AND VE.POLICY_ID = @POLICY_ID    
  AND VE.POLICY_VERSION_ID = @POLICY_VERSION_ID                     
 WHERE ED.STATE_ID = @STATE_ID  
  AND ED.LOB_ID = @LOB_ID   
  AND ED.ENDORS_ASSOC_COVERAGE = 'Y'  
  AND ED.IS_ACTIVE='Y'  
  AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')  
   AND ED.ENDORSMENT_ID IN  
   (  
    SELECT DISTINCT  ENDORSMENT_ID FROM @TEMP_ENDORSMENT   
   )   
  OR   
  (  
   VE.ENDORSEMENT_ID is not null AND ED.ENDORS_ASSOC_COVERAGE = 'Y'  
  )  
    
 ORDER BY ED.RANK                    
  
END          
-- End of renewal  
  
-- Endorsement Case    
-- Fetch All Active Endorsments + Those Grandfathered(not disabled) but available in any previous version  
--     + Those disabled but available in base version of this policy  
ELSE IF @POLICY_STATUS = 'UENDRS'  
BEGIN  
 SELECT  ED.ENDORSMENT_ID,                        
  ED.DESCRIPTION as ENDORSEMENT,                        
      ED.ENDORSEMENT_CODE,                  
  ED.TYPE,                   
  VE.REMARKS,                        
  VE.DWELLING_ENDORSEMENT_ID   ,             
  NULL as Selected,        
  ED.RANK,  
  ED.EFFECTIVE_FROM_DATE,  
  ED.EFFECTIVE_TO_DATE   ,
  VE.EDITION_DATE     
 FROM MNT_ENDORSMENT_DETAILS  ED                         
 LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS VE ON                        
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID   
  AND VE.CUSTOMER_ID = @CUSTOMER_ID  
  AND VE.POLICY_ID = @POLICY_ID    
  AND VE.POLICY_VERSION_ID =  @POLICY_VERSION_ID    
  AND VE.DWELLING_ID = @DWELLING_ID                  
 WHERE ED.STATE_ID = @STATE_ID  
  AND ED.LOB_ID = @LOB_ID   
  AND ED.ENDORS_ASSOC_COVERAGE = 'N'  
  AND ED.IS_ACTIVE='Y'   
  AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')  
  AND ED.ENDORSMENT_ID IN  
  (  
   SELECT DISTINCT  ENDORSMENT_ID FROM @TEMP_ENDORSMENT  
  )  
  OR   
    
    
  (  
   VE.ENDORSEMENT_ID is not null and ED.ENDORS_ASSOC_COVERAGE = 'N'   
  )  
   OR   
  (  
   VE.ENDORSEMENT_ID IN  
   (  
    SELECT  ENDORSEMENT_ID FROM POL_DWELLING_ENDORSEMENTS  
    WHERE   
    CUSTOMER_ID= @CUSTOMER_ID   
    AND POLICY_ID=@POLICY_ID   
    AND POLICY_VERSION_ID IN   
    (  
     SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS   
     WHERE   
     CUSTOMER_ID= @CUSTOMER_ID   
     AND POLICY_ID=@POLICY_ID   
     AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit  
     AND PROCESS_STATUS ='COMPLETE'  
     AND NEW_POLICY_VERSION_ID IN   
     (  
      SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS  
      WHERE  
      CUSTOMER_ID= @CUSTOMER_ID  
      AND POLICY_ID=@POLICY_ID  
      AND PROCESS_ID IN (25,18)  
      AND PROCESS_STATUS ='COMPLETE'  
      )  
   
    )  
   )  and ED.ENDORS_ASSOC_COVERAGE = 'N'  
  )  
 UNION  
      
 SELECT  ED.ENDORSMENT_ID,                        
  ED.DESCRIPTION as ENDORSEMENT,   
      ED.ENDORSEMENT_CODE,                                       
  ED.TYPE,                        
  VE.REMARKS,                        
  VE.DWELLING_ENDORSEMENT_ID ,                   
  (                      
   SELECT COUNT(*)                        
   FROM MNT_ENDORSMENT_DETAILS MED                        
   INNER JOIN APP_DWELLING_SECTION_COVERAGES AVC ON                        
   MED.SELECT_COVERAGE =  AVC.COVERAGE_CODE_ID                        
   WHERE MED.ENDORSMENT_ID = ED.ENDORSMENT_ID  
   AND AVC.CUSTOMER_ID = @CUSTOMER_ID   
   AND POLICY_ID = @POLICY_ID  
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
   AND MED.STATE_ID = @STATE_ID   
   AND MED.LOB_ID = @LOB_ID   
   AND AVC.DWELLING_ID = @DWELLING_ID   
   AND MED.IS_ACTIVE='Y'                       
  )as Selected,        
  ED.RANK,  
  ED.EFFECTIVE_FROM_DATE,  
  ED.EFFECTIVE_TO_DATE   ,
  VE.EDITION_DATE     
 FROM MNT_ENDORSMENT_DETAILS  ED                         
 LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS VE ON                        
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID   
  AND VE.DWELLING_ID = @DWELLING_ID    
  AND VE.CUSTOMER_ID = @CUSTOMER_ID  
  AND VE.POLICY_ID = @POLICY_ID    
  AND VE.POLICY_VERSION_ID = @POLICY_VERSION_ID                     
 WHERE ED.STATE_ID = @STATE_ID  
  AND ED.LOB_ID = @LOB_ID   
  AND ED.ENDORS_ASSOC_COVERAGE = 'Y'   
  AND ED.IS_ACTIVE='Y'   
  AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')  
   AND ED.ENDORSMENT_ID IN  
   (  
    SELECT DISTINCT  ENDORSMENT_ID FROM @TEMP_ENDORSMENT   
   )   
  OR   
  (  
   VE.ENDORSEMENT_ID is not null AND ED.ENDORS_ASSOC_COVERAGE = 'Y'  
  )  
  OR   
  (  
   VE.ENDORSEMENT_ID IN  
   (  
    SELECT  ENDORSEMENT_ID FROM POL_DWELLING_ENDORSEMENTS  
    WHERE   
    CUSTOMER_ID= @CUSTOMER_ID   
    AND POLICY_ID=@POLICY_ID   
    AND POLICY_VERSION_ID IN   
    (  
     SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS   
     WHERE   
     CUSTOMER_ID= @CUSTOMER_ID   
     AND POLICY_ID=@POLICY_ID   
     AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit  
     AND PROCESS_STATUS ='COMPLETE'  
     AND NEW_POLICY_VERSION_ID IN   
     (  
      SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS  
      WHERE  
      CUSTOMER_ID= @CUSTOMER_ID  
      AND POLICY_ID=@POLICY_ID  
      AND PROCESS_ID IN (25,18)  
      AND PROCESS_STATUS ='COMPLETE'  
      )  
   
    )  
   )AND  ED.ENDORS_ASSOC_COVERAGE = 'Y'  
  )  
  
 ORDER BY ED.RANK                    
  
END  
-- End Of Endorsment Case  
  
-- In Case of active or inactive policy display wharever is applicable  
ELSE       
BEGIN   
 SELECT  ED.ENDORSMENT_ID,                        
  ED.DESCRIPTION as ENDORSEMENT,                        
      ED.ENDORSEMENT_CODE,                  
  ED.TYPE,                   
  VE.REMARKS,                        
  VE.DWELLING_ENDORSEMENT_ID   ,                      
  NULL as Selected,        
  ED.RANK,  
  ED.EFFECTIVE_FROM_DATE,  
  ED.EFFECTIVE_TO_DATE     ,
  VE.EDITION_DATE   
 FROM MNT_ENDORSMENT_DETAILS  ED                         
 LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS VE ON                        
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID     
  AND VE.CUSTOMER_ID = @CUSTOMER_ID    
  AND VE.POLICY_ID = @POLICY_ID    
  AND VE.POLICY_VERSION_ID =  @POLICY_VERSION_ID   
  AND VE.DWELLING_ID = @DWELLING_ID                  
 WHERE ED.STATE_ID = @STATE_ID   
  AND ED.LOB_ID = @LOB_ID   
  AND ED.ENDORS_ASSOC_COVERAGE = 'N'  
  AND ED.IS_ACTIVE='Y'     
  AND @APP_EFFECTIVE_DATE BETWEEN ISNULl(ED.EFFECTIVE_FROM_DATE,'1950-1-1')   
     and     ISNULL(ED.EFFECTIVE_TO_DATE,'3000-12-12')  
  AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')  
  OR   
  (  
   VE.ENDORSEMENT_ID is not null and ED.ENDORS_ASSOC_COVERAGE = 'N'   
  )  
 UNION                        
 SELECT  ED.ENDORSMENT_ID,                        
  ED.DESCRIPTION as ENDORSEMENT,                        
      ED.ENDORSEMENT_CODE,                  
  ED.TYPE,                        
  VE.REMARKS,                        
  VE.DWELLING_ENDORSEMENT_ID ,                   
  (                      
   SELECT COUNT(*)                        
   FROM MNT_ENDORSMENT_DETAILS MED                        
   INNER JOIN APP_DWELLING_SECTION_COVERAGES AVC ON                        
   MED.SELECT_COVERAGE =  AVC.COVERAGE_CODE_ID                        
   WHERE MED.ENDORSMENT_ID = ED.ENDORSMENT_ID   
   AND AVC.CUSTOMER_ID = @CUSTOMER_ID   
   AND POLICY_ID = @POLICY_ID  
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
   AND MED.STATE_ID = @STATE_ID   
   AND MED.LOB_ID = @LOB_ID   
   AND AVC.DWELLING_ID = @DWELLING_ID   
   AND MED.IS_ACTIVE='Y'                       
  )as Selected,        
  ED.RANK,  
  ED.EFFECTIVE_FROM_DATE,  
  ED.EFFECTIVE_TO_DATE     ,
  VE.EDITION_DATE   
 FROM MNT_ENDORSMENT_DETAILS  ED                         
 LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS VE ON                        
  ED.ENDORSMENT_ID = VE.ENDORSEMENT_ID  
  AND VE.DWELLING_ID = @DWELLING_ID    
  AND VE.CUSTOMER_ID = @CUSTOMER_ID  
  AND VE.POLICY_ID = @POLICY_ID    
  AND VE.POLICY_VERSION_ID = @POLICY_VERSION_ID                     
 WHERE ED.STATE_ID = @STATE_ID  
  AND ED.LOB_ID = @LOB_ID   
  AND ED.ENDORS_ASSOC_COVERAGE = 'Y'  
  AND ED.IS_ACTIVE='Y'     
  AND @APP_EFFECTIVE_DATE BETWEEN ISNULl(ED.EFFECTIVE_FROM_DATE,'1950-1-1')   
     and     ISNULL(ED.EFFECTIVE_TO_DATE,'3000-12-12')  
  AND @APP_EFFECTIVE_DATE <=  ISNULL(ED.DISABLED_DATE,'3000-12-12')  
  OR   
  (  
   VE.ENDORSEMENT_ID is not null and ED.ENDORS_ASSOC_COVERAGE = 'Y'   
  )  
 ORDER BY ED.RANK                    
  
END                               
                          
--Table 2                          
SELECT POLICY_TYPE, STATE_ID,APP_EFFECTIVE_DATE FROM                
POL_CUSTOMER_POLICY_LIST                  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
  POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID                
                                             
--Table 3                
--Get the Coverage /Limits information from POL_DWELLING_COVERAGE                
SELECT PERSONAL_LIAB_LIMIT,                
 MED_PAY_EACH_PERSON                
FROM POL_DWELLING_COVERAGE                
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
  POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                
 DWELLING_ID = @DWELLING_ID                
    
--Table 4                                    
--Get Rating Info        
SELECT NUM_LOC_ALARMS_APPLIES        
FROM POL_HOME_RATING_INFO        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
  POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                
 DWELLING_ID = @DWELLING_ID                     
            
          
    
--Table 4        
SELECT COUNT(*) as COUNT_WATERCRAFTS  
FROM POL_WATERCRAFT_INFO  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
 POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID    
 AND IS_ACTIVE='Y'         
 
-- for addtion date                    
select ENDORSEMENT_ATTACH_ID,ENDORSEMENT_ID,ATTACH_FILE,VALID_DATE EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE,DISABLED_DATE,FORM_NUMBER,
 EDITION_DATE from MNT_ENDORSEMENT_ATTACHMENT                  

  
  
  
  
  







GO

