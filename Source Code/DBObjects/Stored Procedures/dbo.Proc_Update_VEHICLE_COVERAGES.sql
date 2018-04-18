IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_VEHICLE_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_VEHICLE_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop procedure Proc_Update_VEHICLE_COVERAGES                                     
                                    
 /*----------------------------------------------------------                                                              
Proc Name   : dbo.Proc_Update_VEHICLE_COVERAGES                                                             
Created by  : Pradeep                                                              
Date        : 11/25/2005                                                            
Purpose     :  Deletes relevant coverages when a vehicle is updated                                                  
Revison History  :                                                                    
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                        
-----------------------------------------------------------*/                                                        
CREATE   PROCEDURE dbo.Proc_Update_VEHICLE_COVERAGES                                                      
(                                                       
 @CUSTOMER_ID int,                                                      
 @APP_ID int,                                                      
 @APP_VERSION_ID smallint,                                                       
 @VEHICLE_ID smallint                                                    
                                                       
)                                                      
                                                      
As                                                      
                                                       
DECLARE @VEHICLE_USE  NVarChar(5)                                                    
DECLARE @VEHICLE_TYPE_PER Int                                                    
DECLARE @USE_VEHICLE Int                                                
DECLARE @STATE_ID Int                                                      
DECLARE @LOB_ID int                                                      
              
                                      
SELECT   @VEHICLE_USE = VEHICLE_USE,                                                    
  @VEHICLE_TYPE_PER = VEHICLE_TYPE_PER    ,                                                
 @USE_VEHICLE = USE_VEHICLE                                                
FROM APP_VEHICLES                                                    
WHERE VEHICLE_ID = @VEHICLE_ID AND                                                    
 CUSTOMER_ID = @CUSTOMER_ID AND                                                    
 APP_ID = @APP_ID AND                                                    
 APP_VERSION_ID = @APP_VERSION_ID                                                    
                                              
                                                         
 SELECT @STATE_ID = STATE_ID,                                                      
  @LOB_ID = APP_LOB                                                      
 FROM APP_LIST                                                      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                      
  APP_ID = @APP_ID AND                                                      
  APP_VERSION_ID =  @APP_VERSION_ID                                                    
                                              
--Endorsement variables-------------------------------              
DECLARE @ENDORSEMENT_ID Int                                              
DECLARE @SNOWPLOW Int                                              
DECLARE @TRANSPORT Int                                            
DECLARE @PIP Int                                              
DECLARE @CUSTOMIZING Int     
DECLARE @A22 int                   
                                        
--For Customized van or truck, add/remove                                              
--Customizing Equipment (A-14) endorsement                      
IF ( @STATE_ID = 14 )                                              
--INDIANA          
BEGIN                                              
 SET @ENDORSEMENT_ID = 92     
 SET @SNOWPLOW = 2                                              
 SET @CUSTOMIZING = 49                    
                             --Transportation Expense - Amendment (A-90)                                            
 SET @TRANSPORT = 14      
SET @A22 = 16    
                   
END                                              
                                              
IF ( @STATE_ID = 22 )                     
--MICHIGAN                                              
BEGIN                                              
 SET @ENDORSEMENT_ID = 93                                              
 SET @SNOWPLOW = 33                                          
 SET  @PIP =  43                                          
 SET @CUSTOMIZING = 251                                           
 --Transportation Expense - Amendment (A-90)                                            
 SET @TRANSPORT = 34                                             
 SET @A22 = 35    
END                                              
------------------------------------------------------                                          
              
--Insert mandatory Endorsement Transportation Expense - Amendment (A-90)                                            
EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                       
          @APP_ID,                                              
         @APP_VERSION_ID,                                              
         @TRANSPORT,                                              
         @VEHICLE_ID                                              
 IF @@ERROR <> 0                                              
 BEGIN                                              
 RETURN                                              
 END                                    
--------------------------                        
                                          
--If Vehicle use is Suspended                                                    
IF ( @VEHICLE_TYPE_PER = '11618' )                                                    
BEGIN                                                    
                                                 
 IF EXISTS                                            
 (                                            
 SELECT * FROM APP_VEHICLE_COVERAGES                                            
        INNER JOIN MNT_COVERAGE ON                                                    
  APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                                    
 WHERE MNT_COVERAGE.STATE_ID = @STATE_ID AND                                                    
  MNT_COVERAGE.LOB_ID = @LOB_ID AND                                                    
  APP_VEHICLE_COVERAGES.APP_ID = @APP_ID AND                                                    
  APP_VEHICLE_COVERAGES.APP_VERSIOn_ID = @APP_VERSION_ID AND                                                    
  APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMER_ID AND                                                    
  APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLE_ID AND                                                    
  MNT_COVERAGE.COV_CODE <> 'COMP'                                            
  )                                             
 BEGIN                                            
                                          
 DELETE APP_VEHICLE_COVERAGES                                                    
 FROM  APP_VEHICLE_COVERAGES                                      
 INNER JOIN MNT_COVERAGE ON                                                    
  APP_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                                    
 WHERE MNT_COVERAGE.STATE_ID = @STATE_ID AND                                                    
  MNT_COVERAGE.LOB_ID = @LOB_ID AND                                                    
  APP_VEHICLE_COVERAGES.APP_ID = @APP_ID AND                                                
  APP_VEHICLE_COVERAGES.APP_VERSIOn_ID = @APP_VERSION_ID AND                                                    
  APP_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMER_ID AND                                                    
  APP_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLE_ID AND                                                    
  MNT_COVERAGE.COV_CODE <> 'COMP'                                                    
                                       
 END                                
                                                
 IF @@ERROR <> 0                                              
 BEGIN                                              
 RETURN             
 END                                               
                                            
IF EXISTS                                            
(                                            
 SELECT *                                             
 FROM APP_VEHICLE_ENDORSEMENTS                                                      
 INNER JOIN MNT_ENDORSMENT_DETAILS ON                                                      
  APP_VEHICLE_ENDORSEMENTS.ENDORSEMENT_ID = MNT_ENDORSMENT_DETAILS.ENDORSMENT_ID                      
 WHERE MNT_ENDORSMENT_DETAILS.STATE_ID = @STATE_ID AND                                                      
  MNT_ENDORSMENT_DETAILS.LOB_ID = @LOB_ID AND                                                      
  MNT_ENDORSMENT_DETAILS.ENDORS_ASSOC_COVERAGE = 'Y' AND                                                      
  APP_VEHICLE_ENDORSEMENTS.CUSTOMER_ID = @CUSTOMER_ID AND                                                      
  APP_VEHICLE_ENDORSEMENTS.APP_ID = @APP_ID AND                                             
  APP_VEHICLE_ENDORSEMENTS.APP_VERSION_ID = @APP_VERSION_ID AND                                                      
  APP_VEHICLE_ENDORSEMENTS.VEHICLE_ID = @VEHICLE_ID                                                      
                                             
)                                            
BEGIN                                                
 --Delete all Endorsements  except transportation amendment                                                  
 DELETE APP_VEHICLE_ENDORSEMENTS                                                      
 FROM APP_VEHICLE_ENDORSEMENTS                                                      
 INNER JOIN MNT_ENDORSMENT_DETAILS ON                                                      
  APP_VEHICLE_ENDORSEMENTS.ENDORSEMENT_ID = MNT_ENDORSMENT_DETAILS.ENDORSMENT_ID                                                      
 WHERE MNT_ENDORSMENT_DETAILS.STATE_ID = @STATE_ID AND                                                      
  MNT_ENDORSMENT_DETAILS.LOB_ID = @LOB_ID AND              MNT_ENDORSMENT_DETAILS.ENDORS_ASSOC_COVERAGE = 'Y' AND                                                      
  APP_VEHICLE_ENDORSEMENTS.CUSTOMER_ID = @CUSTOMER_ID AND                                                      
  APP_VEHICLE_ENDORSEMENTS.APP_ID = @APP_ID AND                                                      
  APP_VEHICLE_ENDORSEMENTS.APP_VERSION_ID = @APP_VERSION_ID AND                                                      
  APP_VEHICLE_ENDORSEMENTS.VEHICLE_ID = @VEHICLE_ID AND              
  APP_VEHICLE_ENDORSEMENTS.ENDORSEMENT_ID NOT IN (14,34)                                                       
END                                            
                                                
IF @@ERROR <> 0                                              
 BEGIN                                              
 RETURN                                              
 END                                               
                                              
  RETURN                                   
END                                                    
                                                    
--If peronal vehicle is trailer*******************************************************                                                 
IF ( @VEHICLE_TYPE_PER = 11337 )                      
                                           
BEGIN                           
                                             
    EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
                @APP_ID,                       
          @APP_VERSION_ID,                                               
          'ROAD',                                                  
          @VEHICLE_ID                                                  
                                                      
    IF @@ERROR <> 0                      
    BEGIN                                            
    RETURN               
    END                                             
                                                     
    EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
                @APP_ID,                                    
          @APP_VERSION_ID,                                                  
        'RREIM',                                                  
          @VEHICLE_ID                                                  
                                                
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                                             
                                             
    --Part C - Limited Property Damage Liability (Mini-Tort)                                                   
    EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
                @APP_ID,                                                  
          @APP_VERSION_ID,                           
          'LPD',                                                  
          @VEHICLE_ID                                                  
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                                             
                                               
    --Loan / Lease Gap Coverage (A-11)                             
    EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
                @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'LLGC',                                                  
          @VEHICLE_ID                                                   
                                               
   IF @@ERROR <> 0                                
    BEGIN                                            
    RETURN                                            
    END                                             
                                               
   --Sound Reproducing Equipment (Tapes) (A-29)                                                   
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
                @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'SORPE',                                   
          @VEHICLE_ID                                                   
                                               
   IF @@ERROR <> 0                                            
    BEGIN                           
    RETURN                                            
    END                                             
                                                     
   --Sound Receiving & Transmitting Equipment (A-31)                                                   
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                     
          @APP_VERSION_ID,                                                  
          'SORCV',                                                  
          @VEHICLE_ID                                                   
                                                     
                                        
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                           
    RETURN                                            
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'RLCSL',                                    
@VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                   
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'BISPL',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                       
    RETURN                                            
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'PD',                                                  
          @VEHICLE_ID                                                   
                
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'UMPD',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                        
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'PIP',                                                  
          @VEHICLE_ID                                                   
                                                     
                                              
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'PPI',                                            
          @VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'PUNCS',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                            
   @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'PUMSP',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                                          
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                            
                              
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'UNDSP',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                                            
    END                          
                EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'UNCSL',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                  
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                            
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'ENO',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                                            
    RETURN                               
    END                            
                               
                 
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'EBENO',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                     
                                               
   IF @@ERROR <> 0                                            
    BEGIN                 
    RETURN                                            
    END                            
                               
   EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                  
              @APP_ID,                                                  
          @APP_VERSION_ID,                                                  
          'EBHA',                                                  
          @VEHICLE_ID                                                   
                                                     
                                                     
                                              
   IF @@ERROR <> 0                                            
    BEGIN                                            
     RETURN                                            
    END                        
END                          
                                                 
--*************End of trailer**************************************************************                        
                      
                          
--For Michigan state, for vehicles ;Customized van, Motorhme or PPA , and all Commercial Property Protection Insurance default 1000,000                                                  
IF ( @STATE_ID = 22 )                          
 BEGIN                          
  IF ( @VEHICLE_TYPE_PER = 11336 OR @VEHICLE_TYPE_PER = 11335 OR @VEHICLE_TYPE_PER = 11334 OR @USE_VEHICLE = 11333)                          
   BEGIN                          
                            
    EXEC Proc_SAVE_VEHICLE_COVERAGES                                          
         @CUSTOMER_ID,--@CUSTOMER_ID     int,                              
         @APP_ID,--@APP_ID     int,                      
         @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                              
         @VEHICLE_ID,--@VEHICLE_ID smallint,                                                              
         -1,--@COVERAGE_ID int,                                                              
         117,--@COVERAGE_CODE_ID int,                                                              
         1000000,--@LIMIT_1 Decimal(18,2),                                                              
         NULL,--@LIMIT_2 Decimal(18,2),                                                            
         NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                
         NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                          
         NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                      
         NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                                              
         NULL,--@LIMIT_1_TYPE NVarChar(5),                                               
         NULL,--@LIMIT_2_TYPE NVarChar(5),                                                              
         NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                              
         NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                              
         NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                                              
         NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),            
         NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                                              
         NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2) ,                                               
         'N'--@SIGNATURE_OBTAINED NChar(1)                                  
    END                          
    ELSE                          
    BEGIN                          
     EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                    
                 @APP_ID,                                                    
            @APP_VERSION_ID,                                                    
            'PPI',                                                    
            @VEHICLE_ID                                
    END                          
END                          
                          
--If personal vehicle is Motorhome, remove                                              
--Rental reimbursement                                                  
IF ( @VEHICLE_TYPE_PER = 11336 )                                    
BEGIN                                                  
 EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                    
             @APP_ID,                                                    
        @APP_VERSION_ID,                                                    
        'RREIM',                                                    
        @VEHICLE_ID                                                    
END                                                  
                                              
IF @@ERROR <> 0                                              
 BEGIN                                              
 RETURN                                              
 END                   
                                                
-- If vehicle use is Commercial                                                
IF (@USE_VEHICLE = 11333)                                                
BEGIN                                                
  EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                    
              @APP_ID,                                                    
        @APP_VERSION_ID,                                        
        'ROAD',                                                    
        @VEHICLE_ID                                                    
                                                      
                                               
 IF @@ERROR <> 0                                              
  BEGIN                                              
  RETURN                              
  END                                               
                                                      
  EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                    
              @APP_ID,                                                    
        @APP_VERSION_ID,                                                    
  'RREIM',                                                    
        @VEHICLE_ID                                                    
                                               
 IF @@ERROR <> 0                                         
  BEGIN                                              
  RETURN                                              
  END                                               
                                                
 EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                    
              @APP_ID,                                                    
        @APP_VERSION_ID,                                                    
        'UNDSP',                                                   
        @VEHICLE_ID                                                    
                                              
 IF @@ERROR <> 0                                              
  BEGIN                                              
  RETURN                                              
  END                                               
                   
 EXEC Proc_Delete_APP_VEHICLE_COVERAGES_BY_CODE @CUSTOMER_ID,                                                    
              @APP_ID,                                                    
        @APP_VERSION_ID,                                                    
        'UNCSL',                                                    
        @VEHICLE_ID                                                    
                                               
 IF @@ERROR <> 0                                              
  BEGIN                                              
  RETURN                          END                                                 
END                                                
                                              
--Insert/Delete relevant endorsements**********************************                                              
                                              
              
                                       
--For Customized van or truck, add/remove                                    
--Customizing Equipment (A-14) coverage                                             
IF ( @VEHICLE_TYPE_PER = 11335 )                                              
BEGIN                                              
                     
                                    
  EXEC Proc_SAVE_VEHICLE_COVERAGES                                          
     @CUSTOMER_ID,--@CUSTOMER_ID     int,                                                              
     @APP_ID,--@APP_ID     int,                                                              
     @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                              
     @VEHICLE_ID,--@VEHICLE_ID smallint,                                                              
     -1,--@COVERAGE_ID int,                                                              
     @CUSTOMIZING,--@COVERAGE_CODE_ID int,                                                              
     NULL,--@LIMIT_1 Decimal(18,2),              
     NULL,--@LIMIT_2 Decimal(18,2),                                                            
     NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                              
     NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                              
     NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                      
     NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                                              
     NULL,--@LIMIT_1_TYPE NVarChar(5),                                               
     NULL,--@LIMIT_2_TYPE NVarChar(5),                                                              
     NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                              
     NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                              
     NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                                              
     NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                           
     NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                         
     NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2) ,                                               
     'N'--@SIGNATURE_OBTAINED NChar(1)                                      
                    
/*                    
 EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                              
       @APP_ID,                                              
      @APP_VERSION_ID,                                              
      @ENDORSEMENT_ID,                                              
      @VEHICLE_ID                                              
 */                                             
 IF @@ERROR <> 0                                              
 BEGIN                                              
 RETURN                                        
 END                                              
                                              
END                                              
                                               
                          
--***********************************************************************                                              
                                              
--If vehicle use is Snow plow, remove/ add relevant endorsements                                              
IF ( @VEHICLE_USE = 11272 )                                              
BEGIN                                              
  EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                              
    @APP_ID,                                              
       @APP_VERSION_ID,                                              
     @SNOWPLOW,                                              
       @VEHICLE_ID                                              
  IF @@ERROR <> 0                                              
  BEGIN                                              
  RETURN                                              
  END                                              
                                              
END                                              
ELSE                           
BEGIN                                              
  EXEC Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                              
        @APP_ID,                                              
       @APP_VERSION_ID,                                              
       @SNOWPLOW,                                              
       @VEHICLE_ID                                              
                                               
  IF @@ERROR <> 0                                              
  BEGIN                                              
  RETURN                                              
  END                                              
                                              
END                                              
---------------                                            
    
--If Vehicle is not Motorhome, remove Motor Homes, Campers & Travel Trailers (A-22)  if it exists                             
IF ( @VEHICLE_TYPE_PER = 11336 OR @VEHICLE_TYPE_PER = 11337 )     
BEGIN    
                                      
  EXEC Proc_Insert_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                              
          @APP_ID,                                              
         @APP_VERSION_ID,                                              
         @A22,                                              
         @VEHICLE_ID         
                                             
  IF @@ERROR <> 0                                              
  BEGIN        
	RAISERROR('Unable to add A22 endorsment.',16,1 )                                      
   RETURN                                              
  END      
                    
END    
ELSE
BEGIN
	EXEC Proc_Delete_APP_VEHICLE_ENDORSEMENT_BY_ID @CUSTOMER_ID,                                              
          @APP_ID,                                              
         @APP_VERSION_ID,                                              
         @A22,                                              
         @VEHICLE_ID         
	
	IF @@ERROR <> 0                                              
  		BEGIN        
			RAISERROR('Unable to delete A22 endorsment.',16,1 )                                      
   		RETURN                                              
  	END      
END
--End of Motorhome, trailer------------------------------------------------------------                                  
                                          
--If Indiana State, insert Personal Injury Protection Coverages******************                                          
IF ( @STATE_ID = 22 and @LOB_ID=2)                                          
BEGIN                                          
  DECLARE @COV_ID Int                                          
              
   --Insert mandatory medical payments                                          
         EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                                  
                   @APP_ID,                                                  
               @APP_VERSION_ID,                                                  
              'PIP'                                                  
                                                
    IF ( @COV_ID = 0 )                                           
   BEGIN                                              
     RAISERROR ('Coverage ID not found for Medical Payments.',                                              
           16, 1)                                              
   RETURN                                          
  END                                          
              
  IF NOT EXISTS            
  (            
  SELECT * FROM APP_VEHICLE_COVERAGES            
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   VEHICLE_ID = @VEHICLE_ID AND            
   COVERAGE_CODE_ID = @COV_ID            
  )                                         
            
    EXEC Proc_SAVE_VEHICLE_COVERAGES_ACORD          
   @CUSTOMER_ID,--@CUSTOMER_ID     int,                  
   @APP_ID,--@APP_ID     int,                  
   @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                  
   @VEHICLE_ID,--@VEHICLE_ID smallint,                  
   -1,--@COVERAGE_ID int,                  
   'PIP',--@COVERAGE_CODE NVarChar(10),                  
   NULL,--@LIMIT_1 Decimal(18,2),                  
   NULL,--@LIMIT_2 Decimal(18,2),                  
   NULL,--@LIMIT_1_TYPE NVarChar(5),                  
   NULL,--@LIMIT_2_TYPE NVarChar(5),                  
   NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                  
   NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                  
   NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                  
   NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                  
   'Excess Medical',--@LIMIT1_AMOUNT_TEXT NVarChar(100),                
   NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                  
   NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                
   NULL--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100)                                                       
END                                          
                                          
--*******************************************************************************                                      
                                        
                                        
                                         
-----------------------------------------------------------------------------                                        
                                        
RETURN 1

GO

