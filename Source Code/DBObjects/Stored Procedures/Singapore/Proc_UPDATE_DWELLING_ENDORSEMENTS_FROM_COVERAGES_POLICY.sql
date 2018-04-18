/*----------------------------------------------------------                                      
                
Proc Name       : dbo.Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES                                      
Created by      : Pradeep                                      
Date            : 03/30/2006                                  
Purpose      : Updates endorsements linked with coverages  
Revison History :                                      
Used In  : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/    
--DROP PROC dbo.Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES_POLICY    
--Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES_POLICY 28718,114,1,1                                      
ALTER           PROC dbo.Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES_POLICY                
(                                      
 @CUSTOMER_ID     int,                                      
 @POLICY_ID     int,                                      
 @POLICY_VERSION_ID     smallint,                                      
 @DWELLING_ID smallint               
)                   
                  
AS                   
                  
BEGIN                  
                   
DECLARE @STATEID SmallInt                                          
DECLARE @LOBID NVarCHar(5)                       
DECLARE @PRODUCT Int        
    
SELECT @STATEID = STATE_ID,                                          
@LOBID = POLICY_LOB,        
@PRODUCT = POLICY_TYPE                                        
FROM POL_CUSTOMER_POLICY_LIST                                          
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                          
 POLICY_ID = @POLICY_ID AND                                          
 POLICY_VERSION_ID = @POLICY_VERSION_ID                     
  
DECLARE @HO71 Int  
DECLARE @HO73 Int  
DECLARE @HO42 Int  
DECLARE @HO71_COUNT Int    
DECLARE @HO73_COUNT Int    
DECLARE @HO42_COUNT Int  
DECLARE @HO70 INT  
DECLARE @HO70_COUNT INT  
DECLARE @HO48_COUNT INT  
DECLARE @HO48 INT  
DECLARE @HO315 INT  
DECLARE @HO315_COUNT INT  
DECLARE @COUNT_IBUSPA Int    
DECLARE @HO312 Int   
  
/*  
IF (@STATEID=14)    
BEGIN    
 SET @HO312=244     
END    
    
IF (@STATEID=22)    
BEGIN    
 SET @HO312=192      
END */  
--aded by pravesh ---for increased Limit  
DECLARE @HO96 Int   
DECLARE @HO96_COUNT Int   
DECLARE @HO327 Int   
DECLARE @HO327_COUNT Int  
DECLARE @HO53 Int   
DECLARE @HO53_COUNT Int   
DECLARE @HO65_211FUR Int   
DECLARE @HO65_211FUR_COUNT Int   
DECLARE @HO65_211MONEY Int  
DECLARE @HO65_211MONEY_COUNT Int  
DECLARE @HO65_211SECURITY Int  
DECLARE @HO65_211SECURITY_COUNT Int  
DECLARE @HO65_211GOLD Int  
DECLARE @HO65_211GOLD_COUNT Int  
DECLARE @HO65_211FIRM Int  
DECLARE @HO65_211FIRM_COUNT Int   
SELECT @HO96_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES  WHERE COVERAGE_CODE_ID IN (57,145)              
   AND ISNULL(DEDUCTIBLE_1,0)>0 AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID = @DWELLING_ID          
  
SELECT @HO327_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES  WHERE COVERAGE_CODE_ID IN (197,198)              
   AND ISNULL(DEDUCTIBLE_1,0)>0 AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID = @DWELLING_ID          
  
SELECT @HO53_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES  WHERE COVERAGE_CODE_ID IN (73,153)              
   AND ISNULL(DEDUCTIBLE_1,0)>0 AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID = @DWELLING_ID          
  
SELECT @HO65_211FUR_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES  WHERE COVERAGE_CODE_ID IN (74,154)              
   AND ISNULL(DEDUCTIBLE_1,0)>0 AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID = @DWELLING_ID          
  
SELECT @HO65_211MONEY_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES  WHERE COVERAGE_CODE_ID IN (188,189)              
   AND ISNULL(DEDUCTIBLE_1,0)>0 AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID = @DWELLING_ID          
  
SELECT @HO65_211SECURITY_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES  WHERE COVERAGE_CODE_ID IN (190,191)              
   AND ISNULL(DEDUCTIBLE_1,0)>0 AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID = @DWELLING_ID          
  
SELECT @HO65_211GOLD_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES  WHERE COVERAGE_CODE_ID IN (192,193)              
   AND ISNULL(DEDUCTIBLE_1,0)>0 AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID = @DWELLING_ID          
  
SELECT @HO65_211FIRM_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES  WHERE COVERAGE_CODE_ID IN (194,195)              
   AND ISNULL(DEDUCTIBLE_1,0)>0 AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID = @DWELLING_ID          
  
IF (@STATEID=14)    
BEGIN    
 SET @HO312=244     
  
 SET @HO96=235  
 SET @HO327=246  
 SET @HO53=225     
 IF (@PRODUCT=11149 OR @PRODUCT=11401 OR @PRODUCT =11410)  
 BEGIN  
   SET @HO65_211FUR =237     
  SET  @HO65_211MONEY =375  
  SET @HO65_211SECURITY =379  
  SET @HO65_211GOLD = 383  
  SET @HO65_211FIRM =387  
 ---removing Other linked Endoserment  
         EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 228,@DWELLING_ID  
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 373,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 377,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 381,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 385,@DWELLING_ID  
 END  
 ELSE  
 BEGIN  
   SET @HO65_211FUR =228     
  SET  @HO65_211MONEY =373  
  SET @HO65_211SECURITY =377  
  SET @HO65_211GOLD =381  
  SET @HO65_211FIRM =385  
 ---removing Other linked Endoserment  
         EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 237,@DWELLING_ID  
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 375,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 379,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 383,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 387,@DWELLING_ID  
 END  
END    
  select @STATEID,@PRODUCT  
IF (@STATEID=22 or @STATEID = 0)    
BEGIN    
 SET @HO312=192   
  
 SET @HO96=185     
 SET @HO327=194  
 SET @HO53=175  
 IF (@PRODUCT=11149 OR @PRODUCT=11401 OR @PRODUCT =11410)  
     BEGIN  
   SET @HO65_211FUR =187     
  SET  @HO65_211MONEY =376  
  SET @HO65_211SECURITY =380  
  SET @HO65_211GOLD =384  
  SET @HO65_211FIRM =388  
  ---removing Other linked Endoserment  
         EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 178,@DWELLING_ID  
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 374,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 378,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 382,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 386,@DWELLING_ID  
   
            END  
 ELSE  
            BEGIN  
   SET @HO65_211FUR =178     
  SET  @HO65_211MONEY =374  
  SET @HO65_211SECURITY =378  
  SET @HO65_211GOLD =382  
  SET @HO65_211FIRM =386  
  ---removing Other linked Endoserment  
         EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 187,@DWELLING_ID  
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 376,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 380,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 384,@DWELLING_ID  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
    @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, 388,@DWELLING_ID  
   
     END  
  
END   
  
  
IF (@HO96_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
   @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, @HO96,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96.',16,1)    
   END    
END    
ELSE    
BEGIN   
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID,@HO96,@DWELLING_ID  
     
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96 .',16,1)    
   END    
 END    
  
IF (@HO327_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
   @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, @HO327,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96.',16,1)    
   END    
END    
ELSE    
BEGIN   
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID,@HO327,@DWELLING_ID  
     
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96 .',16,1)    
   END    
 END    
  
IF (@HO53_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
   @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, @HO53,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96.',16,1)    
   END    
END    
ELSE    
BEGIN   
   PRINT '1' 
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID,@HO53,@DWELLING_ID  
     
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96 .',16,1)    
   END    
 END    
  
IF (@HO65_211FUR_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
   @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, @HO65_211FUR,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96.',16,1)    
   END    
END    
ELSE    
BEGIN   
PRINT '2'
      EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID,@HO65_211FUR,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96 .',16,1)    
   END    
 END    
  
IF (@HO65_211MONEY_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
   @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, @HO65_211MONEY,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96.',16,1)    
   END    
END    
ELSE    
BEGIN   
	PRINT '3'
      EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID,@HO65_211MONEY,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96 .',16,1)    
   END    
 END    
  
IF (@HO65_211SECURITY_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
   @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, @HO65_211SECURITY,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96.',16,1)    
   END    
END    
ELSE    
BEGIN   
PRINT '4'
      EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID,@HO65_211SECURITY,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96 .',16,1)    
   END    
 END    
IF (@HO65_211GOLD_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
   @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, @HO65_211GOLD,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96.',16,1)    
   END    
END    
ELSE    
BEGIN   
PRINT '5'
      EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID,@HO65_211GOLD,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96 .',16,1)    
   END    
 END    
  
IF (@HO65_211FIRM_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
   @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID, @HO65_211FIRM,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96.',16,1)    
   END    
END    
ELSE    
BEGIN   
PRINT '6'
      EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID, @POLICY_ID,@POLICY_VERSION_ID,@HO65_211FIRM,@DWELLING_ID  
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-96 .',16,1)    
   END    
 END    
  
  
  
---end here  
  
  
  
  
-----Increased Limits on Business Property (HO-312)    
    
SELECT @COUNT_IBUSPA = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES              
 WHERE COVERAGE_CODE_ID IN (941,942,945,946)              
 AND CUSTOMER_ID = @CUSTOMER_ID AND                
 POLICY_ID = @POLICY_ID AND                
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                
 DWELLING_ID = @DWELLING_ID          
IF(@COUNT_IBUSPA = 0)    
BEGIN    
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY   
  @CUSTOMER_ID,--@CUSTOMER_ID int,                  
  @POLICY_ID,  
  @POLICY_VERSION_ID,  
  @HO312,--@ENDORSEMENT_ID smallint,              
  @DWELLING_ID--@DWELLING_ID smallint        
      
  IF @@ERROR <> 0       
  BEGIN      
   RAISERROR('Unable to delete HO-312 endorsement.',16,1)      
  END     
END    
IF(@COUNT_IBUSPA > 0)    
BEGIN    
PRINT '7'
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY                       
  @CUSTOMER_ID,--@CUSTOMER_ID int,                  
  @POLICY_ID,--@APP_ID int,                  
  @POLICY_VERSION_ID,--@APP_VERSION_ID smallint,                   
  @HO312,--@ENDORSEMENT_ID smallint,              
  @DWELLING_ID--@DWELLING_ID smallint                  
      
  IF @@ERROR <> 0       
  BEGIN      
   RAISERROR('Unable to insert HO-312 endorsement.',16,1)      
  END      
END    
  
-----END OF Increased Limits on Business Property (HO-312)    
           
  --For HO-42 coverages, section 2        
  --If these have been selected, delete Reduction in C from Section 1        
  IF EXISTS      
  (      
 SELECT * FROM POL_DWELLING_SECTION_COVERAGES      
 WHERE COVERAGE_CODE_ID IN (266,267,270,271)      
 AND CUSTOMER_ID = @CUSTOMER_ID AND        
    POLICY_ID = @POLICY_ID AND        
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
    DWELLING_ID = @DWELLING_ID       
  )      
      
  BEGIN        
  IF EXISTS         
  (        
    SELECT * FROM POL_DWELLING_SECTION_COVERAGES        
      WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
    POLICY_ID = @POLICY_ID AND        
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
    DWELLING_ID = @DWELLING_ID AND        
    COVERAGE_CODE_ID IN (831,832)        
  )        
  BEGIN        
   DELETE FROM POL_DWELLING_SECTION_COVERAGES        
      WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
    POLICY_ID = @POLICY_ID AND        
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
    DWELLING_ID = @DWELLING_ID AND        
    COVERAGE_CODE_ID IN (831,832)        
  END        
  END         
  --------------------------      
  
--For Renatal  
DECLARE @HO124 INT  
 IF (@LOBID=6)  
 BEGIN  
    IF (@STATEID=14)    
 BEGIN    
  SET @HO124=260     
 END    
     
 IF (@STATEID=22)    
 BEGIN    
  SET @HO124=273      
 END   
   
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_DWELLING_SECTION_COVERAGES WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
   POLICY_ID = @POLICY_ID AND        
         POLICY_VERSION_ID = @POLICY_VERSION_ID AND               
  DWELLING_ID = @DWELLING_ID AND              
  COVERAGE_CODE_ID IN (10009 ,10010 ))  
 BEGIN  
 PRINT '8'
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY      
  @CUSTOMER_ID,--@CUSTOMER_ID int,                  
  @POLICY_ID,--@APP_ID int,                  
  @POLICY_VERSION_ID,--@APP_VERSION_ID smallint,                   
  @HO124,--@ENDORSEMENT_ID smallint,              
  @DWELLING_ID--@DWELLING_ID smallint                  
  IF @@ERROR <> 0       
  BEGIN      
   RAISERROR('Unable to insert LP-124 endorsement.',16,1)      
    
  END     
 END   
 ELSE  
 BEGIN  
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY      
  @CUSTOMER_ID,--@CUSTOMER_ID int,                  
  @POLICY_ID,--@APP_ID int,                  
  @POLICY_VERSION_ID,--@APP_VERSION_ID smallint,                   
  @HO124,--@ENDORSEMENT_ID smallint,              
  @DWELLING_ID--@DWELLING_ID smallint        
    
  IF @@ERROR <> 0       
  BEGIN      
   RAISERROR('Unable to delete LP-124 endorsement.',16,1)      
    
  END  
 END  
   
 END   
  
  
SELECT @HO315_COUNT = COUNT(1)    
 FROM  POL_DWELLING_SECTION_COVERAGES            
 WHERE COVERAGE_CODE_ID IN (80,157,904,905,906,907)            
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID    
  
  
  
--  272,273 Incidental Office , Private School or Studio - Off Premises (HO-43)  
     
  
             
--HO-71 coveages section 2, if any one of these is taken then default the endorsement HO-71-  
IF ( @STATEID = 14 )  
BEGIN  
 SELECT @HO71_COUNT = COUNT(1)  
 FROM  POL_DWELLING_SECTION_COVERAGES          
 WHERE COVERAGE_CODE_ID IN (278,280,282,284)          
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID      
   
 SELECT @HO73_COUNT = COUNT(1)  
 FROM  POL_DWELLING_SECTION_COVERAGES          
 WHERE COVERAGE_CODE_ID IN (288,290)          
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID      
   
 SELECT @HO42_COUNT = COUNT(1)  
 FROM  POL_DWELLING_SECTION_COVERAGES          
 WHERE COVERAGE_CODE_ID IN (266,270)          
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID  
 SELECT @HO70_COUNT = COUNT(1)    
 FROM  POL_DWELLING_SECTION_COVERAGES            
 WHERE COVERAGE_CODE_ID IN (262,264)            
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
 DWELLING_ID = @DWELLING_ID   
   
 SELECT @HO48_COUNT = COUNT(1)    
 FROM  POL_DWELLING_SECTION_COVERAGES            
 WHERE COVERAGE_CODE_ID =135  
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID  AND              
 DWELLING_ID = @DWELLING_ID and  
 ISNULL(DEDUCTIBLE_1,0) <> 0  
        
   
   
   
   
 SET @HO71 = 231  
 SET @HO73 = 233  
 SET @HO42 = 220  
 SET @HO70 = 230  
 SET @HO48 = 222  
    SET @HO315 =245   
  
END  
  
IF ( @STATEID = 22 OR @STATEID = 0)  
BEGIN  
 SELECT @HO71_COUNT = COUNT(1)  
 FROM  POL_DWELLING_SECTION_COVERAGES          
 WHERE COVERAGE_CODE_ID IN (279,281,283,285)   
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID      
   
 SELECT @HO73_COUNT = COUNT(1)  
 FROM  POL_DWELLING_SECTION_COVERAGES          
 WHERE COVERAGE_CODE_ID IN (289, 291)          
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID      
   
 SELECT @HO42_COUNT = COUNT(1)  
 FROM  POL_DWELLING_SECTION_COVERAGES          
 WHERE COVERAGE_CODE_ID IN (267,271)          
 AND CUSTOMER_ID = @CUSTOMER_ID AND      
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID      
   
 SELECT @HO70_COUNT = COUNT(1)    
 FROM  POL_DWELLING_SECTION_COVERAGES            
 WHERE COVERAGE_CODE_ID IN (265,772)            
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID  
   
 SELECT @HO48_COUNT = COUNT(1)    
 FROM  POL_DWELLING_SECTION_COVERAGES            
 WHERE COVERAGE_CODE_ID =5  
 AND CUSTOMER_ID = @CUSTOMER_ID AND            
 POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID  AND              
 DWELLING_ID = @DWELLING_ID and  
 ISNULL(DEDUCTIBLE_1,0) <> 0  
   
   
 SET @HO71 = 181  
 SET @HO73 = 183  
 SET @HO42 = 170  
 SET @HO70 = 180   
 SET @HO48 = 172  
    SET @HO315 =193  
END  
  
  
IF (   @HO315_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
      @CUSTOMER_ID,--@CUSTOMER_ID int,              
      @POLICY_ID,--@POLICY_ID int,              
      @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                    
      @HO315,--@ENDORSEMENT_ID smallint,            
      @DWELLING_ID--@DWELLING_ID smallint      
       
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-48 Other Structures-Increased Limits.',16,1)    
        
   END    
END    
ELSE    
BEGIN   
PRINT '8'
      print @HO315  
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID,--@CUSTOMER_ID int,              
      @POLICY_ID,--@POLICY_ID int,              
      @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                
      @HO315,--@ENDORSEMENT_ID smallint,            
      @DWELLING_ID--@DWELLING_ID smallint                
     
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-48 Other Structures-Increased Limits .',16,1)    
        
   END    
 END    
  
IF (   @HO48_COUNT = 0 )    
BEGIN    
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
      @CUSTOMER_ID,--@CUSTOMER_ID int,              
      @POLICY_ID,--@POLICY_ID int,              
      @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                    
      @HO48,--@ENDORSEMENT_ID smallint,            
      @DWELLING_ID--@DWELLING_ID smallint      
       
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-48 Other Structures-Increased Limits.',16,1)    
        
   END    
END    
ELSE    
BEGIN   
PRINT @HO48
PRINT '9'
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
       @CUSTOMER_ID,--@CUSTOMER_ID int,              
      @POLICY_ID,--@POLICY_ID int,              
      @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                
      @HO48,--@ENDORSEMENT_ID smallint,            
      @DWELLING_ID--@DWELLING_ID smallint                
     
   IF @@ERROR <> 0     
   BEGIN    
    RAISERROR('HO-48 Other Structures-Increased Limits .',16,1)    
        
   END    
 END    
  
  
IF (   @HO70_COUNT = 0 )    
 BEGIN    
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
     @CUSTOMER_ID,--@CUSTOMER_ID int,              
     @POLICY_ID,--@POLICY_ID int,              
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,          
     @HO70,--@ENDORSEMENT_ID smallint,            
     @DWELLING_ID--@DWELLING_ID smallint      
      
  IF @@ERROR <> 0     
  BEGIN    
   RAISERROR('HO-70 Additional Residence Rented to Others.',16,1)    
       
  END    
 END    
 ELSE    
 BEGIN   
  PRINT '10'
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY    
     @CUSTOMER_ID,--@CUSTOMER_ID int,              
     @POLICY_ID,--@POLICY_ID int,              
     @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,                
     @HO70,--@ENDORSEMENT_ID smallint,            
     @DWELLING_ID--@DWELLING_ID smallint                
    
  IF @@ERROR <> 0     
  BEGIN    
   RAISERROR('HO-70 Additional Residence Rented to Others.',16,1)    
       
  END    
    
 END       
  
  
IF (   @HO71_COUNT = 0 )  
BEGIN  
EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
  @CUSTOMER_ID,--@CUSTOMER_ID int,              
  @POLICY_ID,--@POLICY_ID int,              
  @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
  @HO71,--@ENDORSEMENT_ID smallint,          
  @DWELLING_ID--@DWELLING_ID smallint    
  
IF @@ERROR <> 0   
BEGIN  
RAISERROR('Unable to delete HO-71 endorsement.',16,1)  
  
END  
END  
ELSE  
BEGIN  
PRINT '11'
EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
  @CUSTOMER_ID,--@CUSTOMER_ID int,              
  @POLICY_ID,--@POLICY_ID int,              
  @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
  @HO71,--@ENDORSEMENT_ID smallint,          
  @DWELLING_ID--@DWELLING_ID smallint              
  
IF @@ERROR <> 0   
BEGIN  
RAISERROR('Unable to insert HO-71 endorsement.',16,1)  
  
END  
  
END     
  
IF (   @HO73_COUNT = 0 )  
BEGIN  
EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
  @CUSTOMER_ID,--@CUSTOMER_ID int,              
  @POLICY_ID,--@POLICY_ID int,              
  @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
  @HO73,--@ENDORSEMENT_ID smallint,          
  @DWELLING_ID--@DWELLING_ID smallint    
  
IF @@ERROR <> 0   
BEGIN  
RAISERROR('Unable to delete HO-73 endorsement.',16,1)  
  
END  
END  
ELSE  
BEGIN  
PRINT '12'
EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
  @CUSTOMER_ID,--@CUSTOMER_ID int,              
  @POLICY_ID,--@POLICY_ID int,              
  @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
  @HO73,--@ENDORSEMENT_ID smallint,          
  @DWELLING_ID--@DWELLING_ID smallint              
  
IF @@ERROR <> 0   
BEGIN  
RAISERROR('Unable to insert HO-73 endorsement.',16,1)  
  
END  
  
END     
  
IF (   @HO42_COUNT = 0 )  
BEGIN  
EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
  @CUSTOMER_ID,--@CUSTOMER_ID int,              
  @POLICY_ID,--@POLICY_ID int,              
  @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
  @HO42,--@ENDORSEMENT_ID smallint,          
  @DWELLING_ID--@DWELLING_ID smallint    
  
IF @@ERROR <> 0   
BEGIN  
RAISERROR('Unable to delete HO-73 endorsement.',16,1)  
  
END  
END  
ELSE  
BEGIN  
PRINT '13'
EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
  @CUSTOMER_ID,--@CUSTOMER_ID int,              
  @POLICY_ID,--@POLICY_ID int,              
  @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
  @HO42,--@ENDORSEMENT_ID smallint,          
  @DWELLING_ID--@DWELLING_ID smallint              
  
IF @@ERROR <> 0   
BEGIN  
RAISERROR('Unable to insert HO-73 endorsement.',16,1)  
  
END  
  
END     
END  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  