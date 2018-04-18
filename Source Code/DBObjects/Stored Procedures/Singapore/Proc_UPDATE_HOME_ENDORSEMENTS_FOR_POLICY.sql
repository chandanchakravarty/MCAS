  
 /*----------------------------------------------------------                                                    
Proc Name       : dbo.Proc_UPDATE_HOME_ENDORSEMENTS_FOR_POLICY                                                    
Created by      : SHAFI                                                    
Date            : 16/02/06                                             
Purpose      :Inserts linked endorsemnts for this coverage                                                  
Revison History :              
Modified By :Pravesh k Chandel    
modified Date :30 May 2007    
Purpose  : some Endorsement Wwas not being Updated hence Call 'Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES_POLICY'                                  
                                          
Used In  : Wolverine                                                    
------------------------------------------------------------                                                    
Date     Review By          Comments                                                    
------   ------------       -------------------------*/         
--drop proc Proc_UPDATE_HOME_ENDORSEMENTS_FOR_POLICY                                                 
ALTER           PROC [dbo].[Proc_UPDATE_HOME_ENDORSEMENTS_FOR_POLICY]  --28718,114,1,1                              
(                                                    
  @CUSTOMER_ID     int,                                                    
  @POL_ID     int,                                                    
  @POL_VERSION_ID     smallint,                                                    
  @DWELLING_ID smallint                              
)                                 
                                
AS                                 
                                
BEGIN                                
                                 
   DECLARE @STATEID SmallInt                                                        
   DECLARE @LOBID NVarCHar(5)                                     
   DECLARE @PRODUCT Int                              
   DECLARE @END_ID Int                               
   DECLARE @HO65 Int                    
   DECLARE @HO211 Int      
   DECLARE @HO865 INT       
DECLARE @HO315 INT    
DECLARE @HO315_COUNT INT    
    
    
SELECT @HO315_COUNT = COUNT(1)      
 FROM  POL_DWELLING_SECTION_COVERAGES              
 WHERE COVERAGE_CODE_ID IN (80,157,904,905,906,907)              
 AND CUSTOMER_ID = @CUSTOMER_ID AND              
 POLICY_ID = @POL_ID AND              
 POLICY_VERSION_ID = @POL_VERSION_ID AND              
 DWELLING_ID = @DWELLING_ID      
           
                               
 SELECT @STATEID = STATE_ID,                                  
  @LOBID = POLICY_LOB ,                 
@PRODUCT= POLICY_TYPE                  
 FROM POL_CUSTOMER_POLICY_LIST                                  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
  POLICY_ID = @POL_ID AND                                  
  POLICY_VERSION_ID = @POL_VERSION_ID    
  
                            
                                 
  IF ( @STATEID = 22 or @STATEID = 0)                                  
   BEGIN                    
  SET @HO65 = 178                    
  SET @HO211 = 435--187 --Changed by Charles on 28-Apr-10 for Itrack 7065          
  SET @HO865 = 294     
     SET @HO315 =193                  
   END                             
                    
 IF ( @STATEID = 14 )                              
                                
   BEGIN                    
  SET @HO65 = 228                    
  SET @HO211 = 434--237 --Changed by Charles on 28-Apr-10 for Itrack 7065    
  SET @HO865 = 295     
     SET @HO315 =245                  
   END                             
             
 --Homeowners----------------------------------------                             
 IF ( @LOBID = 1 )                            
 BEGIN        
    
IF (   @HO315_COUNT = 0 )      
BEGIN      
   EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY      
      @CUSTOMER_ID,--@CUSTOMER_ID int,                
      @POL_ID,--@POLICY_ID int,                
      @POL_VERSION_ID,--@POLICY_VERSION_ID smallint,                      
      @HO315,--@ENDORSEMENT_ID smallint,              
      @DWELLING_ID--@DWELLING_ID smallint        
         
  IF @@ERROR <> 0       
   BEGIN      
    RAISERROR('HO-48 Other Structures-Increased Limits.',16,1)      
          
   END      
END      
ELSE      
BEGIN     
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY      
       @CUSTOMER_ID,--@CUSTOMER_ID int,                
      @POL_ID,--@POLICY_ID int,                
      @POL_VERSION_ID,--@POLICY_VERSION_ID smallint,                  
      @HO315,--@ENDORSEMENT_ID smallint,              
      @DWELLING_ID--@DWELLING_ID smallint                  
       
   IF @@ERROR <> 0       
   BEGIN      
    RAISERROR('HO-48 Other Structures-Increased Limits .',16,1)      
          
   END      
 END      
         
    
      --HO-865 Watercraft Endorsement     
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_WATERCRAFT_INFO WHERE    
                CUSTOMER_ID = @CUSTOMER_ID AND                                  
    POLICY_ID = @POL_ID AND                                  
    POLICY_VERSION_ID = @POL_VERSION_ID        
            )    
     BEGIN    
        EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY             
        @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @HO865, @DWELLING_ID                      
                          
      IF @@ERROR <> 0                       
      BEGIN                      
              RAISERROR ('Unable to save HO-865 Watercraft Endorsement',16, 1)                                        
                      
      END       
     END    
  ELSE    
    BEGIN      
      EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @HO865, @DWELLING_ID                              
    END               
   --For HO-5 Insert HO-211 else insert HO-65 endorsments----------------                    
  IF ( @PRODUCT = 11401 OR @PRODUCT = 11149 )                    
  BEGIN                    
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY                     
    @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @HO211, @DWELLING_ID                              
                                  
     IF @@ERROR <> 0                               
     BEGIN                              
             RAISERROR ('Unable to save HO-211 Endorsement',16, 1)                                                
                             
     END         
         
    EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY                     
    @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @HO65, @DWELLING_ID                              
                                  
     IF @@ERROR <> 0                               
     BEGIN                              
             RAISERROR ('Unable to save HO-65 Endorsement',16, 1)                                                
                             
     END       
  END                    
  ELSE                    
  BEGIN   
          
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY                     
    @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @HO65, @DWELLING_ID                              
                                  
     IF @@ERROR <> 0                               
     BEGIN                              
             RAISERROR ('Unable to save HO-211 Endorsement',16, 1)                                                
                             
     END                      
  END                      
   ------------------------------------------------------                    
                    
  --Insert these mandatory endorsements                              
  IF ( @STATEID = 22 )                              
                                
   BEGIN                              
                                 
   SET @END_ID = 149                              
   --149       HO-300 Special Provisions - Michigan                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID                              
                                 
   IF @@ERROR <> 0                               
   BEGIN                              
    print('1')              
END                             
                               
   --150       HO-310 Sec II - Intentional Acts Exclusion        
   SET @END_ID = 150                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID                              
                                 
   IF @@ERROR <> 0                               
   BEGIN                              
    print('11')                              
END                              
                               
                                
   --151       HO-320 Coverage B - Other Structures (Special Limit for Satellite Dish Antennas)                              
   SET @END_ID = 151                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID                              
                                 
                                 
   IF @@ERROR <> 0                               
   BEGIN                              
    print('111')                              
   END                              
                               
   --152       HO-330 Limited Fungi Covg                              
   SET @END_ID = 152                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID                              
                                 
   IF @@ERROR <> 0                               
   BEGIN                              
    print('1111')                              
   END                              
                               
  --153       HO-350 Supplemental Provisions                              
   SET @END_ID = 153                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID                              
                                 
   IF @@ERROR <> 0                               
   BEGIN                              
    print('11111')                              
   END                              
                               
                               
   --154       HO-360 Section II Liability Coverages-Asbestos and Silica Exclusion                              
   SET  @END_ID = 154                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID                              
                                 
   IF @@ERROR <> 0                               
   BEGIN                              
    print('111111')                              
   END                              
                               
   --155       HO-382 Lead Liability Excl.-Rental Exposure                              
     SET @END_ID = 155                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID                              
                                 
   IF @@ERROR <> 0                               
   BEGIN                              
    print('1111111')                              
   END                              
                               
   --156       HO-417 Section I & II Exclusion for Computer-Related Damage or Injury                              
   SET  @END_ID = 156                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID                              
                                  
   IF @@ERROR <> 0                               
   BEGIN                              
    print('11111111')                              
   END                              
              
                               
   --157       SFP-1MI Standard Fire Policy End.                              
   SET  @END_ID = 157                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @END_ID, @DWELLING_ID            
        
   IF @@ERROR <> 0                               
   BEGIN                              
    print('111111111')                              
   END          
          
    --For repair cost polices, HO-289 Repair Cost Homeowners End. is mandatory          
   IF (  @PRODUCT IN (11403,11404) )          
   BEGIN          
 EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, 191, @DWELLING_ID                        
   END                 
                              
  END                               
  --End of Michigan                              
                          
                             
  IF ( @STATEID = 14 )                              
  BEGIN                              
   --201       HO-300 Special Provisions- IN                           
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, 201 ,@DWELLING_ID                              
                                
   --202       HO-320 Coverage B-Other Structures (Special Limit for Satellite Dish Antennas)                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, 202 ,@DWELLING_ID                              
                                 
   --203       HO-330 Limited Fungi Covg                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  203,@DWELLING_ID                              
                                 
   --204       HO-350 Supplemental Provisions                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  204,@DWELLING_ID                              
                                
   --205       HO-373 Contingent Workers' Compensation                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  205,@DWELLING_ID                              
                                 
   --206       HO-382 Lead Liability Exclusion-Rental Exposure                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  206,@DWELLING_ID                              
                                
   --207       HO-417 Sections I & II Exclusions for Computer-Related Damage or Injury                              
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  207, @DWELLING_ID                              
               
  --For HO-3 and HO-5 Asbetos is mandatory--------------------          
    IF ( @PRODUCT IN (11148,11149) )            
   BEGIN              
     --287 14 1 B M HO-360 Section II Liability Coverages-Asbestos and Silica Exclusion NULL N              
    EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  287, @DWELLING_ID                               
    END           
  -------------------------------------------------------          
          
 --Non smoker credit--If underwritng question is answered as yes, then         
 DECLARE @NON_SMOKER_CREDIT NChar(1)        
         
 SELECT @NON_SMOKER_CREDIT = NON_SMOKER_CREDIT        
 FROM POL_HOME_OWNER_GEN_INFO        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POL_ID AND        
  POLICY_VERSION_ID = @POL_VERSION_ID        
         
         
 --default Ho-220 on  the basis of underwrting questions----------------------------------------------        
 IF ( @NON_SMOKER_CREDIT = '1' )        
 BEGIN       
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, 240, @DWELLING_ID                        
 END        
 ELSE        
 BEGIN        
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, 240, @DWELLING_ID                        
 END        
 -------------------------------------------------------------------        
        
  ---------------------------------------------          
  --For repair cost polices, HO-289 Repair Cost Homeowners End. is mandatory          
   IF (  @PRODUCT IN (11193,11194) )          
   BEGIN          
 EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, 243, @DWELLING_ID                        
   END        
   --------------------------------------------------------            
         
   END                              
                                 
   END                             
   --End of Homeowners ------------------------------------------------                           
                            
 --Rental dwelling------------------------------------------                          
  IF (  @LOBID = 6 )                            
  BEGIN                            
                           
 --Michigan                          
 IF ( @STATEID = 22 )                            
 BEGIN                            
   --DP-3 Premier-----------------------------                             
   IF (@PRODUCT = 11458)                            
   BEGIN                            
   --276 22 6 B M Premier Tree Debris Removal                            
    EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID,  276, @DWELLING_ID                              
   END                            
   ---------------End of DP-3 Premier------------------------                
                
-- For repair cost , default repair cost endorsement---------------------------                
  --267 22 6 B M DP-289 Repair Cost End                
  IF @PRODUCT IN ( 11290, 11292)                          
  BEGIN                
                
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY                 
   @CUSTOMER_ID,                 
   @POL_ID,                 
   @POL_VERSION_ID,                 
    267,                
    @DWELLING_ID                            
                
  END                 
  --------------------------------------------------------------------------------                
                        
 END                            
                            
 --Indiana                          
 IF ( @STATEID = 14 )                            
 BEGIN                 
                       
   --Repair Cost----------------------------------------------------------                        
    --254 14 6 B M DP-289 Repair Cost End                
   IF @PRODUCT IN ( 11480, 11482)                          
   BEGIN                
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY                 
     @CUSTOMER_ID,                 
     @POL_ID,                 
     @POL_VERSION_ID,                 
      254,                
      @DWELLING_ID                            
                 
   END                 
    --End of repair Cost----------------------------------------------                
                    
 END                           
                            
  END                             
--End of rental           
    
EXEC Proc_UPDATE_HOME_ENDORSEMENTS_RATING_FOR_POLICY  @CUSTOMER_ID,@POL_ID,@POL_VERSION_ID,@DWELLING_ID,null    
--added by pravesh as Other Endorsement Was not being updated    
exec Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES_POLICY  @CUSTOMER_ID,@POL_ID,@POL_VERSION_ID,@DWELLING_ID    
                                    
END                                                   
                              
                            
  
    
  