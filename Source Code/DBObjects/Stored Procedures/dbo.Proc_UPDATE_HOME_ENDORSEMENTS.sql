IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_HOME_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_HOME_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                            
Proc Name       : dbo.Proc_UPDATE_HOME_ENDORSEMENTS                                            
Created by      : Pradeep                                            
Date            : 12/23/2005                                            
Purpose      :Inserts linked endorsemnts for this coverage                                          
Revison History :              
Modified By :Pravesh k Chandel    
modified Date :30 May 2007    
Purpose  : some Endorsement Wwas not being Updated hence Call 'Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES'                                  
Used In  : Wolverine                                            
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------*/            
--drop proc dbo.Proc_UPDATE_HOME_ENDORSEMENTS                                  
CREATE           PROC dbo.Proc_UPDATE_HOME_ENDORSEMENTS                       
(                                            
  @CUSTOMER_ID     int,                                            
  @APP_ID     int,                                            
  @APP_VERSION_ID     smallint,                                            
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
DECLARE @HO73_COUNT Int     
DECLARE @HO73 Int      
    
DECLARE @HO315 INT    
DECLARE @HO315_COUNT INT   
     
SELECT @HO315_COUNT = COUNT(1)      
 FROM  APP_DWELLING_SECTION_COVERAGES              
 WHERE COVERAGE_CODE_ID IN (80,157,904,905,906,907)              
 AND CUSTOMER_ID = @CUSTOMER_ID AND                
 APP_ID = @APP_ID AND                
 APP_VERSION_ID = @APP_VERSION_ID AND                
 DWELLING_ID = @DWELLING_ID            
    
SELECT @STATEID = STATE_ID,                                                
@LOBID = APP_LOB ,                      
@PRODUCT = POLICY_TYPE                                                
 FROM APP_LIST                                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                
 APP_ID = @APP_ID AND                                                
 APP_VERSION_ID = @APP_VERSION_ID                           
                         
IF ( @STATEID = 22 )                          
BEGIN            
 SET @HO65  = 178            
 SET @HO211 = 187            
 SET @HO865 = 294    
 SET @HO73  = 183    
END                     
            
IF ( @STATEID = 14 )                      
BEGIN            
 SET @HO65 = 228            
 SET @HO211 = 237       
 SET @HO865 = 295         
 SET @HO315 =245     
 SET @HO73 = 233    
END                     
            
            
 --Homeowners----------------------------------------                     
IF ( @LOBID = 1 )                    
BEGIN          
 
 SELECT @HO73_COUNT = COUNT(1)      
 FROM  APP_DWELLING_SECTION_COVERAGES WITH(NOLOCK)             
 WHERE COVERAGE_CODE_ID IN (288,290,289,291)              
  AND CUSTOMER_ID = @CUSTOMER_ID AND                
  APP_ID = @APP_ID AND                
  APP_VERSION_ID = @APP_VERSION_ID AND                
  DWELLING_ID = @DWELLING_ID          
    
 IF ( @PRODUCT = 11401 OR @PRODUCT = 11149 )            
 BEGIN            
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID             
  @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @HO211, @DWELLING_ID                      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   RAISERROR ('Unable to save HO-211 Endorsement',16, 1)                                        
      
  END       
      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID             
  @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @HO65, @DWELLING_ID       
      
  IF @@ERROR <> 0                       
  BEGIN                      
   RAISERROR ('Unable to save HO-65 Endorsement',16, 1)                                        
      
  END       
    
 END            
 ELSE            
 BEGIN            
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID             
  @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @HO65, @DWELLING_ID                      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   RAISERROR ('Unable to save HO-65 Endorsement',16, 1)                                        
      
  END              
 END              
          
        --IF HO-73 Coverage Is Present    
 IF (   @HO73_COUNT = 0 )      
 BEGIN      
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID      
  @CUSTOMER_ID,--@CUSTOMER_ID int,                  
  @APP_ID,--@APP_ID int,                  
  @APP_VERSION_ID,--@APP_VERSION_ID smallint,                   
  @HO73,--@ENDORSEMENT_ID smallint,              
  @DWELLING_ID--@DWELLING_ID smallint        
     
  IF @@ERROR <> 0       
  BEGIN      
   RAISERROR('Unable to delete HO-73 endorsement.',16,1)      
      
  END      
 END      
 ELSE      
 BEGIN      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID      
  @CUSTOMER_ID,--@CUSTOMER_ID int,                  
  @APP_ID,--@APP_ID int,                  
  @APP_VERSION_ID,--@APP_VERSION_ID smallint,                   
  @HO73,--@ENDORSEMENT_ID smallint,              
  @DWELLING_ID--@DWELLING_ID smallint                  
      
  IF @@ERROR <> 0       
  BEGIN      
   RAISERROR('Unable to insert HO-73 endorsement.',16,1)      
      
  END      
 END     
  ------------------------------------------------------            
            
 --Insert these mandatory endorsements                 
 IF ( @STATEID = 22 )                      
 BEGIN                      
      
  SET @END_ID = 149              
  SET @HO315 =193            
  --149       HO-300 Special Provisions - Michigan                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID                      
      
  IF @@ERROR <> 0        
  BEGIN                      
   print('1')                      
  END                      
      
  --150       HO-310 Sec II - Intentional Acts Exclusion                      
  SET @END_ID = 150                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID                      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   print('11')                      
  END                      
      
      
  --151       HO-320 Coverage B - Other Structures (Special Limit for Satellite Dish Antennas)                      
  SET @END_ID = 151                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID                      
      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   print('111')                      
  END                      
      
  --152       HO-330 Limited Fungi Covg                      
  SET @END_ID = 152                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID                      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   print('1111')                      
  END                      
      
  --153       HO-350 Supplemental Provisions                      
  SET @END_ID = 153                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID                      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   print('11111')                      
  END                      
      
      
  --154       HO-360 Section II Liability Coverages-Asbestos and Silica Exclusion                      
  SET  @END_ID = 154                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   print('111111')                      
  END     
      
  --155       HO-382 Lead Liability Excl.-Rental Exposure                      
  SET @END_ID = 155                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID                  
      
  IF @@ERROR <> 0                       
  BEGIN                      
   print('1111111')                      
  END                      
      
  --156       HO-417 Section I & II Exclusion for Computer-Related Damage or Injury                      
  SET  @END_ID = 156                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID                      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   print('11111111')                      
  END                      
      
      
  --157       SFP-1MI Standard Fire Policy End.                      
  SET  @END_ID = 157                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @END_ID, @DWELLING_ID                      
      
  IF @@ERROR <> 0                       
  BEGIN                      
   print('111111111')                      
  END             
      
  --For repair cost polices, HO-289 Repair Cost Homeowners End. is mandatory        
  IF (  @PRODUCT IN (11403,11404) )        
  BEGIN        
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, 191, @DWELLING_ID                      
  END               
 END                       
  --End of Michigan************************************************************                      
                  
  --INDIANA**********************************************************************                   
 IF ( @STATEID = 14 )                      
 BEGIN                      
  --201 HO-300 Special Provisions- IN                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, 201 ,@DWELLING_ID                      
                   
  --202       HO-320 Coverage B-Other Structures (Special Limit for Satellite Dish Antennas)                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, 202 ,@DWELLING_ID                      
                    
  --203       HO-330 Limited Fungi Covg                 
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID,  203,@DWELLING_ID                      
                    
  --204       HO-350 Supplemental Provisions                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID,  204,@DWELLING_ID                      
                   
  --205       HO-373 Contingent Workers' Compensation                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID,  205,@DWELLING_ID                      
                    
  --206       HO-382 Lead Liability Exclusion-Rental Exposure                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID,  206,@DWELLING_ID                      
                   
  --207       HO-417 Sections I & II Exclusions for Computer-Related Damage or Injury                      
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID,  207, @DWELLING_ID                      
        
  --For HO-3 and HO-5 Asbetos is mandatory--------------------        
  --287 14 1 B M HO-360 Section II Liability Coverages-Asbestos and Silica Exclusion NULL N          
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID,  287, @DWELLING_ID                           
  ---------------------------------------        
      
  --For repair cost polices, HO-289 Repair Cost Homeowners End. is mandatory-------------        
  IF (  @PRODUCT IN (11193,11194) )        
  BEGIN        
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, 243, @DWELLING_ID                      
  END       
      
  --Non smoker credit--If underwritng question is answered as yes, then       
  DECLARE @NON_SMOKER_CREDIT NChar(1)      
      
  SELECT @NON_SMOKER_CREDIT = NON_SMOKER_CREDIT      
  FROM APP_HOME_OWNER_GEN_INFO      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID      
 END                
                         
END                     
--End of Homeowners ------------------------------------------------                   
                    
 --Rental dwelling------------------------------------------                  
IF (  @LOBID = 6 )                    
BEGIN                    
DECLARE @LP124 INT                  
 --Michigan                  
 IF ( @STATEID = 22 )                    
 BEGIN          
         SET @LP124=273        
  --DP-3 Premier                     
  IF (@PRODUCT = 11458)                    
  BEGIN                    
   --276 22 6 B M Premier Tree Debris Removal                    
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID,  276, @DWELLING_ID                      
  END                    
      
  -- For repair cost , default repair cost endorsement---------------------------          
  --267 22 6 B M DP-289 Repair Cost End          
  IF @PRODUCT IN ( 11290, 11292)                    
  BEGIN          
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID           
   @CUSTOMER_ID,           
   @APP_ID,           
   @APP_VERSION_ID,           
   267,          
   @DWELLING_ID                      
  END           
 END                    
                    
 --Indiana                  
 IF ( @STATEID = 14 )                    
 BEGIN                    
      
  SET @LP124=260       
  --254 14 6 B M DP-289 Repair Cost End          
  IF @PRODUCT IN ( 11480, 11482)                    
  BEGIN          
   EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID           
   @CUSTOMER_ID,           
   @APP_ID,           
   @APP_VERSION_ID,           
   254,          
   @DWELLING_ID                      
  END           
 END     
   /* Commented by pravesh on 28 march as LP 124 is now coverage itself            
 IF EXISTS (SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
 APP_ID = @APP_ID AND                
 APP_VERSION_ID = @APP_VERSION_ID AND            
 DWELLING_ID = @DWELLING_ID AND                
 COVERAGE_CODE_ID IN (964,965))    
 BEGIN    
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID        
  @CUSTOMER_ID,--@CUSTOMER_ID int,                    
  @APP_ID,--@APP_ID int,                    
  @APP_VERSION_ID,--@APP_VERSION_ID smallint,                     
  @LP124,--@ENDORSEMENT_ID smallint,                
  @DWELLING_ID--@DWELLING_ID smallint                    
  IF @@ERROR <> 0         
  BEGIN        
   RAISERROR('Unable to insert LP-124 endorsement.',16,1)        
      
  END       
 END     
 ELSE    
 BEGIN    
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID        
  @CUSTOMER_ID,--@CUSTOMER_ID int,                    
  @APP_ID,--@APP_ID int,                    
  @APP_VERSION_ID,--@APP_VERSION_ID smallint,                     
  @LP124,--@ENDORSEMENT_ID smallint,                
  @DWELLING_ID--@DWELLING_ID smallint          
  IF @@ERROR <> 0         
  BEGIN        
   RAISERROR('Unable to delete LP-124 endorsement.',16,1)        
      
  END    
 END    
 */     
END                     
--End of rental         
    
IF (@LOBID=1)    
BEGIN     
 IF (   @HO315_COUNT = 0 )      
BEGIN      
  EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID      
     @CUSTOMER_ID,--@CUSTOMER_ID int,                  
     @APP_ID,--@APP_ID int,                  
     @APP_VERSION_ID,--@APP_VERSION_ID smallint,                   
     @HO315,--@ENDORSEMENT_ID smallint,              
     @DWELLING_ID--@DWELLING_ID smallint        
        
  IF @@ERROR <> 0       
  BEGIN      
   RAISERROR('HO-315 Earthquake.',16,1)      
         
  END      
 END      
 ELSE      
 BEGIN     
    
  EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID      
     @CUSTOMER_ID,--@CUSTOMER_ID int,                  
     @APP_ID,--@APP_ID int,                  
     @APP_VERSION_ID,--@APP_VERSION_ID smallint,                   
     @HO315,--@ENDORSEMENT_ID smallint,              
     @DWELLING_ID--@DWELLING_ID smallint                  
      
  IF @@ERROR <> 0       
  BEGIN      
   RAISERROR('HO-315 Earthquake .',16,1)      
         
  END      
      
 END      
    
END     
    
    
EXEC Proc_UPDATE_HOME_ENDORSEMENTS_RATING  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DWELLING_ID,'NA'    
--added by pravesh as Other Endorsement Was not being updated    
exec Proc_UPDATE_DWELLING_ENDORSEMENTS_FROM_COVERAGES @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DWELLING_ID    
          
END 
GO

