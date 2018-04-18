IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_HOME_COVERAGES_POLICY_FROM_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_HOME_COVERAGES_POLICY_FROM_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Proc Name       : dbo.Proc_UPDATE_HOME_COVERAGES_POLICY_FROM_GEN_INFO                                        
Created by      : Pradeep                                        
Date            : 02/03/2006                                   
Purpose       :  Updates coverages based on App general information      
Revison History :                                        
Used In  : Wolverine                                        
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
CREATE           PROC Dbo.Proc_UPDATE_HOME_COVERAGES_POLICY_FROM_GEN_INFO                                  
(                                        
 @CUSTOMER_ID     int,                                        
 @POLICY_ID     int,                                        
 @POLICY_VERSION_ID      smallint                                        
                 
)                                        
AS                                        
                                        
BEGIN        
       
 DECLARE   @NON_SMOKER_CREDIT NChar(1)      
 DECLARE @HO220 Int      
 DECLARE @STATE_ID Int      
 DECLARE @LOB_ID Int    
 DECLARE @HO72 INT  
 DECLARE @FLAG INT  
 DECLARE @FLAG73R INT  
 DECLARE @HO73R INT  
 DECLARE @NO_HORSES INT  
 DECLARE @FLAG73I INT  
 DECLARE @HO73I INT  
 DECLARE @HOFINAL INT  
  
 DECLARE @PREMISES_DETAIL INT  
  
  /*  
SP_FIND 'Proc_SAVE_HOME_COVERAGES',P  
  
*/   
      
 SET  @HO220 = 240      
       
 IF NOT EXISTS      
 (      
 SELECT DWELLING_ID      
  FROM POL_DWELLINGS_INFO      
  WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID       
 )      
 BEGIN      
 RETURN      
 END      
          
SELECT @STATE_ID = STATE_ID,      
 @LOB_ID = POLICY_LOB      
FROM POL_CUSTOMER_POLICY_LIST      
WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID      
--START  
  
  
    EXEC  @HO72 =  Proc_GetCOVERAGE_IDForPolicy @CUSTOMER_ID,                            
                    @POLICY_ID,                                                              
                    @POLICY_VERSION_ID,                                                 
                    'FLIFP'   
    EXEC @HO73I =   Proc_GetCOVERAGE_IDForPolicy @CUSTOMER_ID,                            
                     @POLICY_ID,                                                              
                    @POLICY_VERSION_ID,                                            
                    'FLOFO'  
    EXEC @HO73R =   Proc_GetCOVERAGE_IDForPolicy @CUSTOMER_ID,                            
                    @POLICY_ID,                                                              
                    @POLICY_VERSION_ID,                                            
                    'FLOFR'                                                   
  
  
--END  
SELECT @NO_HORSES = ISNULL(NO_HORSES,0) ,  
       @PREMISES_DETAIL=ISNULL(PREMISES,0)  
       FROM POL_HOME_OWNER_GEN_INFO      
    WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
       POLICY_ID = @POLICY_ID AND      
    POLICY_VERSION_ID = @POLICY_VERSION_ID      
IF (@NO_HORSES < 4 AND  @PREMISES_DETAIL=11557)  
BEGIN  
 SET @FLAG=1  
 SET @HOFINAL=@HO72  
END  
ELSE  
 SET @FLAG=0  
  
IF (@NO_HORSES < 4 AND  @PREMISES_DETAIL=11558)  
 BEGIN  
  SET @FLAG73I=1  
  SET @HOFINAL=@HO73I  
 END  
ELSE  
 SET @FLAG73I=0  
  
IF (@NO_HORSES < 4 AND  @PREMISES_DETAIL=11559)  
 BEGIN  
   SET @FLAG73R=1  
   SET @HOFINAL=@HO73R  
 END  
ELSE  
 SET @FLAG73R=0  
  
  
  
  
  
--For Indiana state    
  
    
IF ( @LOB_ID = 1)      
BEGIN      
      
if( @STATE_ID = 14)  
  BEGIN  
 SELECT @NON_SMOKER_CREDIT = ISNULL(NON_SMOKER_CREDIT,'0')      
 FROM POL_HOME_OWNER_GEN_INFO      
 WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
    POLICY_ID = @POLICY_ID AND      
    POLICY_VERSION_ID = @POLICY_VERSION_ID      
  
 IF ( @NON_SMOKER_CREDIT = '0' OR @NON_SMOKER_CREDIT = '' )      
 BEGIN      
  DELETE FROM POL_DWELLING_ENDORSEMENTS      
  WHERE   CUSTOMER_ID =  @CUSTOMER_ID AND      
    POLICY_ID = @POLICY_ID AND      
          POLICY_VERSION_ID = @POLICY_VERSION_ID    AND  
    ENDORSEMENT_ID = @HO220      
 END   
  END     
  
--Delete the coverage when condition not satisfied  
--( Farm Liability (number of locations) Incidental Farming on Premises(HO-72))  
  
IF ( @FLAG = 0 )      
 BEGIN      
  DELETE FROM POL_DWELLING_SECTION_COVERAGES      
  WHERE   CUSTOMER_ID =  @CUSTOMER_ID AND      
    POLICY_ID = @POLICY_ID AND      
          POLICY_VERSION_ID = @POLICY_VERSION_ID   AND   
    COVERAGE_CODE_ID = @HO72      
 END   
--Delete the coverage when condition not satisfied  
--Farm Liability (number of locations) Owned Farms Operated by Insured's Employees(HO-73)  
  
  
  
IF ( @FLAG73I = 0 )      
 BEGIN      
  DELETE FROM POL_DWELLING_SECTION_COVERAGES      
  WHERE   CUSTOMER_ID =  @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
        POLICY_VERSION_ID = @POLICY_VERSION_ID     AND      
  COVERAGE_CODE_ID = @HO73I      
 END   
--Delete the coverage when condition not satisfied  
--Farm Liability (number of locations) Owned Farms Rented to Others(HO-73)  
IF ( @FLAG73R = 0 )      
 BEGIN      
  DELETE FROM POL_DWELLING_SECTION_COVERAGES      
  WHERE   CUSTOMER_ID =  @CUSTOMER_ID AND      
        POLICY_ID = @POLICY_ID AND      
        POLICY_VERSION_ID = @POLICY_VERSION_ID     AND      
  COVERAGE_CODE_ID = @HO73R      
 END   
  
  
      
 CREATE TABLE #TEMP      
 (      
  IDENT_COL Int Identity(1,1),      
  DWELLING_ID Int       
 )       
      
 INSERT INTO #TEMP      
 (      
  DWELLING_ID      
 )      
 SELECT DWELLING_ID      
 FROM POL_DWELLINGS_INFO      
 WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID           
      
 DECLARE @IDENT Int      
 DECLARE @DWELL_ID Int     
       
 SET @IDENT = 1      
      
 WHILE 1 = 1      
      
BEGIN      
 IF NOT EXISTS      
 (      
   SELECT * FROM #TEMP      
   WHERE IDENT_COL = @IDENT      
         )      
 BEGIN      
   BREAK      
 END      
      
 SELECT @DWELL_ID = DWELLING_ID      
 FROM #TEMP      
 WHERE IDENT_COL = @IDENT      
       
        --Perform update   
  
IF ( @FLAG = 1 OR  @FLAG73I= 1 OR @FLAG73R=1)      
 BEGIN      
       exec Proc_SAVE_HOME_COVERAGES_FOR_POLICY  
                    @CUSTOMER_ID,                            
                    @POLICY_ID,                                                              
                    @POLICY_VERSION_ID,   
                    @DWELL_ID,  
                    -1,  
                    @HOFINAL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    "S2",  
                    NULL,  
                    NULL,  
                    NULL,   
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL,  
                    NULL                      
                       
                      
 END   
  
if( @STATE_ID = 14)  
BEGIN     
 IF ( @NON_SMOKER_CREDIT = '1' )      
 BEGIN      
  IF NOT EXISTS      
  (      
   SELECT * FROM       
   POL_DWELLING_ENDORSEMENTS      
   WHERE CUSTOMER_ID =  @CUSTOMER_ID AND      
   POLICY_ID = @POLICY_ID AND      
   POLICY_VERSION_ID = @POLICY_VERSION_ID     AND      
   DWELLING_ID = @DWELL_ID AND    
   ENDORSEMENT_ID = @HO220      
  )      
  BEGIN      
   INSERT INTO APP_DWELLING_ENDORSEMENTS 
	(
	CUSTOMER_ID,
	APP_ID,
	APP_VERSION_ID,
	DWELLING_ID,
	ENDORSEMENT_ID,
	REMARKS,
	DWELLING_ENDORSEMENT_ID
	)        
      SELECT @CUSTOMER_ID,        
       @POLICY_ID,        
       @POLICY_VERSION_ID,        
       @DWELL_ID,         
@HO220,        
       NULL,         
      (        
       SELECT ISNULL(MAX(DWELLING_ENDORSEMENT_ID),0) + 1        
       FROM POL_DWELLING_ENDORSEMENTS        
       WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
      POLICY_ID = @POLICY_ID AND      
      POLICY_VERSION_ID = @POLICY_VERSION_ID    
        AND DWELLING_ID = @DWELL_ID       
      )        
  END       
 END    
END  
  
    
        
 SET @IDENT = @IDENT + 1      
       
       
 END --END OF WHILE LOOP       
    
DROP TABLE #TEMP       
    
END --  OF LOB AND STATE      
                     
END                     



GO

