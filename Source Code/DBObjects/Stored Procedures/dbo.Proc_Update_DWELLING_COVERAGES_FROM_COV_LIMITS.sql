IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--exec Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS    933,3,1,1

/*----------------------------------------------------------                                                        
Proc Name   : dbo.Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS                                                       
Created by  : Pradeep                                                        
Date        : 12/30/2005                                                      
Purpose     :  Adds/deletes relevant coverages                                
Revison History  :                        
Modified BY : Praveen kasana                                        
Modified On : 03 Jan 2006                                       
Purpose : Add Coverage provisions for Rental Dwelling                                                               
------------------------------------------------------------                                                                    
Date     Review By          Comments                                                                  
-----------------------------------------------------------*/                                                  
--drop proc Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS                                                
CREATE   PROCEDURE Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS                                                
(                                                 
 @CUSTOMER_ID int,                                                
 @APP_ID int,                                                
 @APP_VERSION_ID smallint,                                                 
 @DWELLING_ID smallint                                                     
)                                                
                                                
As                                              
                                    
                                    
BEGIN                                    
	--added by vijay
-- return
                                  
 DECLARE @COV_ID Int                                    
 DECLARE @STATE_ID Int                                  
 DECLARE @LOB_ID Int                                  
 DECLARE @POLICY_TYPE Int                                 
                       
 DECLARE @DWELLING_LIMIT    decimal (18,2)                                   
 DECLARE @OTHER_STRU_LIMIT     decimal (18,2)                                     
 DECLARE @PERSONAL_PROP_LIMIT     decimal (18,2)                                      
 DECLARE @LOSS_OF_USE     decimal (18,2)                                     
 DECLARE @PERSONAL_LIAB_LIMIT     decimal (18,2)                                    
 DECLARE @MED_PAY_EACH_PERSON     decimal (18,2)                                 
 DECLARE @DWELLING_REPLACE_COST NChar(1)             
 DECLARE @REPLACEMENT_COST_CONTS NChar(1)            
DECLARE @DEDUCTIBLE Decimal(18,0)            
            
-----------------------------------------------------                              
                              
 SELECT @DWELLING_LIMIT = DWELLING_LIMIT,                              
  @OTHER_STRU_LIMIT = OTHER_STRU_LIMIT,                              
   @PERSONAL_PROP_LIMIT = PERSONAL_PROP_LIMIT,                              
  @LOSS_OF_USE = CONVERT(DECIMAL(18,2), LOSS_OF_USE ),                              
  @PERSONAL_LIAB_LIMIT = PERSONAL_LIAB_LIMIT,                              
  @MED_PAY_EACH_PERSON = MED_PAY_EACH_PERSON,                            
  @DWELLING_REPLACE_COST = DWELLING_REPLACE_COST,            
  @REPLACEMENT_COST_CONTS = REPLACEMENT_COST_CONTS,            
  @DEDUCTIBLE = ALL_PERILL_DEDUCTIBLE_AMT                           
 FROM APP_DWELLING_COVERAGE                              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
  APP_ID = @APP_ID AND                              
  APP_VERSION_ID = @APP_VERSION_ID AND                              
  DWELLING_ID = @DWELLING_ID                
                              
 print @DWELLING_REPLACE_COST   
                            
 SELECT   @LOB_ID = APP_LOB,            
 @POLICY_TYPE = POLICY_TYPE ,            
 @STATE_ID = STATE_ID                              
 FROM APP_LIST WHERE          
 CUSTOMER_ID = @CUSTOMER_ID AND                              
 APP_ID = @APP_ID AND                              
 APP_VERSION_ID = @APP_VERSION_ID                              
                              
        
--HOMEOWNERS------------------------------------------          
   IF ( @LOB_ID = 1 )                              
   BEGIN                              
                               
   --Save relevant coverages in APP_DWELLING_SECTION_COVERAGES***********                                  
   --Insert Dwelling limit                                      
   EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
      @CUSTOMER_ID, --@CUSTOMER_ID                                      
      @APP_ID, --@APP_ID              
      @APP_VERSION_ID,--@APP_VERSION_ID                                      
      @DWELLING_ID, --@DWELLING_ID                                      
      0,  --@COVERAGE_ID                                      
      0,  --@COVERAGE_CODE_ID                                     
      @DWELLING_LIMIT, --@LIMIT_1                                      
      null,  --@LIMIT_2                                      
      null,  --@DEDUCTIBLE_1                                      
      null,  --@DEDUCTIBLE_2                           
      'S1',  --@COVERAGE_TYPE                                      
      'DWELL'  --@COVERAGE_CODE                                      
                                      
  --Coverage B - Other Structures  (OS)                                    
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
      @CUSTOMER_ID, --@CUSTOMER_ID                                 
      @APP_ID, --@APP_ID                                      
      @APP_VERSION_ID,--@APP_VERSION_ID                                      
      @DWELLING_ID, --@DWELLING_ID                                      
      0,  --@COVERAGE_ID                              
      0,  --@COVERAGE_CODE_ID                                      
      @OTHER_STRU_LIMIT, --@LIMIT_1                                      
      null,  --@LIMIT_2                                      
      null,  --@DEDUCTIBLE_1                         
      null,  --@DEDUCTIBLE_2                                      
      'S1',  --@COVERAGE_TYPE                                      
      'OS'  --@COVERAGE_CODE                                      
                                      
  --Coverage C - Unscheduled Personal Property (EBUSPP)                                    
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
      @CUSTOMER_ID, --@CUSTOMER_ID                                      
      @APP_ID, --@APP_ID                                      
      @APP_VERSION_ID,--@APP_VERSION_ID                                      
      @DWELLING_ID, --@DWELLING_ID                                      
      0,  --@COVERAGE_ID                                      
      0,  --@COVERAGE_CODE_ID                                      
      @PERSONAL_PROP_LIMIT, --@LIMIT_1                                      
      null,  --@LIMIT_2                                      
      null,  --@DEDUCTIBLE_1                                      
      null,  --@DEDUCTIBLE_2                                      
      'S1',  --@COVERAGE_TYPE                                      
      'EBUSPP'  --@COVERAGE_CODE                                      
                                  
                                  
  DECLARE @LOSS Decimal (18,0)                                    
  SET @LOSS = CONVERT(decimal(18,0),@LOSS_OF_USE)                                    
                                      
  --Coverage D - Loss of Use (LOSUR)                                    
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
      @CUSTOMER_ID, --@CUSTOMER_ID                                      
      @APP_ID, --@APP_ID                                      
      @APP_VERSION_ID,--@APP_VERSION_ID                                      
      @DWELLING_ID, --@DWELLING_ID                              
      0,  --@COVERAGE_ID                                      
      0,  --@COVERAGE_CODE_ID                                      
      @LOSS, --@LIMIT_1                                      
      null,  --@LIMIT_2                                      
      null,  --@DEDUCTIBLE_1                                      
      null,  --@DEDUCTIBLE_2                                      
      'S1',  --@COVERAGE_TYPE                                      
      'LOSUR'  --@COVERAGE_CODE                                      
        
                                     
  --Coverage F - Medical Payments to Others                                  
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
      @CUSTOMER_ID, --@CUSTOMER_ID                                      
      @APP_ID, --@APP_ID                                      
      @APP_VERSION_ID,--@APP_VERSION_ID                                      
      @DWELLING_ID, --@DWELLING_ID                                      
      0,  --@COVERAGE_ID                                      
      0,  --@COVERAGE_CODE_ID                                      
      @MED_PAY_EACH_PERSON, --@LIMIT_1                                      
      null,  --@LIMIT_2                                      
      null,  --@DEDUCTIBLE_1                                      
      null,  --@DEDUCTIBLE_2                                      
      'S1',  --@COVERAGE_TYPE                                      
      'MEDPM'  --@COVERAGE_CODE                                      
          
      print ( @MED_PAY_EACH_PERSON)                         
               
          
          
  --Coverage E - Personal Liability (PL)                                  
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                        
      @CUSTOMER_ID, --@CUSTOMER_ID                                      
      @APP_ID, --@APP_ID                                      
      @APP_VERSION_ID,--@APP_VERSION_ID                                      
      @DWELLING_ID, --@DWELLING_ID                                      
      0,  --@COVERAGE_ID                                      
      0,  --@COVERAGE_CODE_ID                                      
      @PERSONAL_LIAB_LIMIT, --@LIMIT_1                                      
      null,  --@LIMIT_2                                      
      null,  --@DEDUCTIBLE_1                                      
      null,  --@DEDUCTIBLE_2                                      
      'S1',  --@COVERAGE_TYPE          
      'PL'  --@COVERAGE_CODE          
                                    
  --***************************                         
                       
  IF ( @DWELLING_REPLACE_COST = '1' )                      
  BEGIN                      
    EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
        @CUSTOMER_ID, --@CUSTOMER_ID                                      
        @APP_ID, --@APP_ID                                      
        @APP_VERSION_ID,--@APP_VERSION_ID                                      
        @DWELLING_ID, --@DWELLING_ID                                      
        0,  --@COVERAGE_ID                            
        0,  --@COVERAGE_CODE_ID                                      
  NULL, --@LIMIT_1                                      
        null,  --@LIMIT_2                      
        null,  --@DEDUCTIBLE_1                                      
        null,  --@DEDUCTIBLE_2                                      
        'S1',  --@COVERAGE_TYPE                                      
        'EBRCPP'  --@COVERAGE_CODE                
                
     IF ( @@ERROR <> 0 )                                       
     BEGIN                                                
           RAISERROR ('Unable to save Dwelling replacement Cost.',16, 1)                                          
           RETURN                                         
     END                  
               
     --delete Reduction - C              
                 
   IF EXISTS               
   (              
     SELECT * FROM APP_DWELLING_SECTION_COVERAGES              
       WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
     APP_ID = @APP_ID AND              
     APP_VERSION_ID = @APP_VERSION_ID AND              
     DWELLING_ID = @DWELLING_ID AND              
     COVERAGE_CODE_ID IN (831,832)              
   )              
   BEGIN              
     DELETE FROM APP_DWELLING_SECTION_COVERAGES              
        WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
      APP_ID = @APP_ID AND              
      APP_VERSION_ID = @APP_VERSION_ID AND              
      DWELLING_ID = @DWELLING_ID AND              
      COVERAGE_CODE_ID IN (831,832)              
   END              
                      
   END                            
  ELSE                      
  BEGIN                      
     EXEC @COV_ID = Proc_GetCOVERAGE_ID @CUSTOMER_ID,                                              
                         @APP_ID,                
                      @APP_VERSION_ID,                                              
                     'EBRCPP'                         
                           
     EXEC Proc_DELETE_DWELLING_COVERAGES_BY_ID           
      @CUSTOMER_ID,--@CUSTOMER_ID     int,                                    
      @APP_ID,--@APP_ID     int,                                  
      @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                    
      @DWELLING_ID,--@DWELLING_ID smallint,                                    
      @COV_ID--@COVERAGE_CODE VarChar(10)     
               
     IF ( @@ERROR <> 0 )                                       
     BEGIN                                                      
           RAISERROR ('Unable to Delete Dwelling replacement Cost.',16, 1)                                          
           RETURN                                         
     END                          
                 
   END             
            
--For Ho-3 and HO-3 Premier Products, link Expanded Replacement Coverage A+B (HO-11)            
IF ( @POLICY_TYPE = 11400 OR @POLICY_TYPE = 11409 OR @POLICY_TYPE = 11148 )            
BEGIN            
 DECLARE @HO11 Int            
            
 IF ( @STATE_ID = 14 )            
 BEGIN            
  SET @HO11 = 144            
 END            
             
 IF ( @STATE_ID = 22 )            
 BEGIN            
  SET @HO11 = 56            
 END            
             
 IF ( @REPLACEMENT_COST_CONTS = '1' )            
 BEGIN            
              
   EXEC Proc_SAVE_DWELLING_COVERAGES                                             
            @CUSTOMER_ID, --@CUSTOMER_ID     int,                                                                  
            @APP_ID, --@APP_ID     int,                                                                  
            @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                                                  
            @DWELLING_ID, --@DWELLING_ID smallint,                                                                  
            -1,  --@COVERAGE_ID int,                                                                  
           @HO11, --@COVERAGE_CODE_ID int,                                                                  
            NULL,  --@LIMIT_1 Decimal(18,2),                                                                  
            NULL,  --@LIMIT_2 Decimal(18,2),                                                                       
            NULL,  --@DEDUCTIBLE_1 DECIMAL(18,2),                                          
            NULL  --@DEDUCTIBLE_2 DECIMAL(18,2)                            
                           
    IF ( @@ERROR <> 0 )                                                    
    BEGIN                                               
       --print(1)                                          
     RAISERROR ('Unable to save Expanded Replacement Coverage A + B (HO-11).',16, 1)                                   
      RETURN                                             
    END             
 END            
 ELSE            
 BEGIN            
  --Delete this coverage            
  EXEC Proc_DELETE_DWELLING_COVERAGES_BY_ID                                                 
   @CUSTOMER_ID, --@CUSTOMER_ID     int,                                          
   @APP_ID, --@APP_ID     int,                                          
   @APP_VERSION_ID, --@APP_VERSION_ID     smallint,                                          
   @DWELLING_ID, --@DWELLING_ID smallint,                                          
   @HO11 --@COV_CODE_ID Int                  
            
 END             
               
END            
            
--End of HO-11-----------------------------------------------------------------            
            
--For HO-3 and HO-5 Indiana, if Coveage A < 75,000,  then delete HO-24 f it exists        
IF ( @STATE_ID = 14 )        
BEGIN        
 IF ( @POLICY_TYPE IN (11148,11149) )        
 BEGIN        
  IF ( @DWELLING_LIMIT < 75000 )        
  BEGIN        
   EXEC Proc_DELETE_DWELLING_COVERAGES_BY_ID        
    @CUSTOMER_ID, --@CUSTOMER_ID     int,                                          
       @APP_ID, --@APP_ID     int,                                          
       @APP_VERSION_ID, --@APP_VERSION_ID     smallint,                                          
       @DWELLING_ID, --@DWELLING_ID smallint,         
       143        
           
   IF (@@ERROR <> 0 )        
       BEGIN        
    RAISERROR('Unable to delete HO-24.',16,1)        
    RETURN        
   END        
  END        
 END        
END        
------------------------------------------------------------------------------------        
                                 
END --End of Homeowners-----------------------------------------------                              
                              
---Start of rental---------                              
IF ( @LOB_ID = 6 )                              
BEGIN                              
  --Insert Dwelling limit                                      
  EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
     @CUSTOMER_ID, --@CUSTOMER_ID                                      
     @APP_ID, --@APP_ID                                      
     @APP_VERSION_ID,--@APP_VERSION_ID                                      
     @DWELLING_ID, --@DWELLING_ID                                      
     0,  --@COVERAGE_ID                                      
     0,  --@COVERAGE_CODE_ID                                      
     @DWELLING_LIMIT, --@LIMIT_1                                      
     null,  --@LIMIT_2                                      
     null,  --@DEDUCTIBLE_1                                      
     null,  --@DEDUCTIBLE_2                                      
     'S1',  --@COVERAGE_TYPE                                      
     'DWELL'  --@COVERAGE_CODE                    
                                    
 --Coverage B - Other Structures  (OSTR)                                
 EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD           
     @CUSTOMER_ID, --@CUSTOMER_ID                                 
     @APP_ID, --@APP_ID                             
     @APP_VERSION_ID,--@APP_VERSION_ID                                      
     @DWELLING_ID, --@DWELLING_ID                                      
     0,  --@COVERAGE_ID                                      
     0,  --@COVERAGE_CODE_ID                                      
     @OTHER_STRU_LIMIT, --@LIMIT_1                                      
     null,  --@LIMIT_2                                      
     null,  --@DEDUCTIBLE_1                                      
     null,  --@DEDUCTIBLE_2                                      
   'S1',  --@COVERAGE_TYPE             
     'OSTR'  --@COVERAGE_CODE                                      
                               
                               
 --Coverage C – Landlords Personal Property(LPP)                                    
 EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
     @CUSTOMER_ID, --@CUSTOMER_ID                                 
     @APP_ID, --@APP_ID                                      
     @APP_VERSION_ID,--@APP_VERSION_ID                                      
     @DWELLING_ID, --@DWELLING_ID                                      
     0,  --@COVERAGE_ID                                      
     0,  --@COVERAGE_CODE_ID                                      
     @PERSONAL_PROP_LIMIT, --@LIMIT_1                                      
     null,  --@LIMIT_2                                      
     null,  --@DEDUCTIBLE_1                                      
     null,  --@DEDUCTIBLE_2                                      
     'S1',  --@COVERAGE_TYPE                                      
     'LPP'  --@COVERAGE_CODE                               
                          
                          
 --Coverage D – Rental Value                          
 EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                       
     @CUSTOMER_ID, --@CUSTOMER_ID                                 
     @APP_ID, --@APP_ID                                      
     @APP_VERSION_ID,--@APP_VERSION_ID                                      
     @DWELLING_ID, --@DWELLING_ID                                      
     0,  --@COVERAGE_ID                                      
     0,  --@COVERAGE_CODE_ID                                      
     @LOSS_OF_USE, --@LIMIT_1                          
     null,  --@LIMIT_2                          
     null,  --@DEDUCTIBLE_1                                      
     null,  --@DEDUCTIBLE_2                                      
     'S1',  --@COVERAGE_TYPE                                      
     'RV'  --@COVERAGE_CODE                              
                           
 --Coverage E – Liability (CSL)----------------------------------------------------------                          
 IF (  @PERSONAL_LIAB_LIMIT IS NULL )                    
   --777                    
   --798                    
 BEGIN                    
               
      --Delete if exists                    
       IF EXISTS                    
      (                    
       SELECT * FROM APP_DWELLING_SECTION_COVERAGES                     
       WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
        APP_ID = @APP_ID AND                              
        APP_VERSION_ID = @APP_VERSION_ID AND                              
        DWELLING_ID = @DWELLING_ID  AND                      
       COVERAGE_CODE_ID IN (777,797)                    
      )                    
       BEGIN                    
        DELETE FROM APP_DWELLING_SECTION_COVERAGES                     
        WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
         APP_ID = @APP_ID AND                              
         APP_VERSION_ID = @APP_VERSION_ID AND                              
     DWELLING_ID = @DWELLING_ID  AND                      
        COVERAGE_CODE_ID IN (777,797)                    
      END                 
    END                    
                        
    ELSE                    
    BEGIN                
                
    print (@PERSONAL_LIAB_LIMIT)                     
        --Coverage E – Liability (CSL)                          
        EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
            @CUSTOMER_ID, --@CUSTOMER_ID                      
            @APP_ID, --@APP_ID                                      
            @APP_VERSION_ID,--@APP_VERSION_ID       
            @DWELLING_ID, --@DWELLING_ID                                      
            0,  --@COVERAGE_ID                                      
            0,  --@COVERAGE_CODE_ID                                      
            @PERSONAL_LIAB_LIMIT, --@LIMIT_1                                      
            null,  --@LIMIT_2                                      
            null,  --@DEDUCTIBLE_1                             
            null,  --@DEDUCTIBLE_2                                      
            'S1',  --@COVERAGE_TYPE                                      
            'CSL'  --@COVERAGE_CODE                            
      END                    
                 
                     
 --End of Liab limit                
                    
-------------END OF  Coverage E – Liability (CSL)----------------------------------------------------                    
                    
                         
 --Coverage F – Medical Payments to Others-----------------------------------------------------------------------                          
 /*                   
 EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
     @CUSTOMER_ID, --@CUSTOMER_ID                                 
     @APP_ID, --@APP_ID                                      
     @APP_VERSION_ID,--@APP_VERSION_ID                                      
     @DWELLING_ID, --@DWELLING_ID                                      
     0,  --@COVERAGE_ID                                      
     0,  --@COVERAGE_CODE_ID                                      
     @MED_PAY_EACH_PERSON, --@LIMIT_1                                      
     null,  --@LIMIT_2                                      
     null,  --@DEDUCTIBLE_1                   
     null,  --@DEDUCTIBLE_2                                      
     'S1',  --@COVERAGE_TYPE                                      
     'MEDPM'  --@COVERAGE_CODE                         
 */                  
                     
                   
 IF ( @MED_PAY_EACH_PERSON IS NULL )                    
  BEGIN                    
      --Delete if exists                    
      IF EXISTS                    
     (                    
       SELECT * FROM APP_DWELLING_SECTION_COVERAGES                     
       WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
        APP_ID = @APP_ID AND                              
        APP_VERSION_ID = @APP_VERSION_ID AND                              
        DWELLING_ID = @DWELLING_ID  AND                      
       COVERAGE_CODE_ID IN (798,778)                    
     )                    
      BEGIN                    
       DELETE FROM APP_DWELLING_SECTION_COVERAGES         
       WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
        APP_ID = @APP_ID AND                              
        APP_VERSION_ID = @APP_VERSION_ID AND                              
        DWELLING_ID = @DWELLING_ID  AND                      
       COVERAGE_CODE_ID IN (798,778)                    
      END                    
    END                    
 ELSE                    
     BEGIN                    
   print(@MED_PAY_EACH_PERSON)                     
       --Coverage F – Medical Payments to Others                          
       EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                                      
           @CUSTOMER_ID,  --@CUSTOMER_ID                                 
           @APP_ID,  --@APP_ID                
   @APP_VERSION_ID,--@APP_VERSION_ID                                      
           @DWELLING_ID,  --@DWELLING_ID                                      
           0,    --@COVERAGE_ID                                      
           0,    --@COVERAGE_CODE_ID                                      
           @MED_PAY_EACH_PERSON, --@LIMIT_1                                      
           null,  --@LIMIT_2                                      
           null,  --@DEDUCTIBLE_1                                      
           null,  --@DEDUCTIBLE_2                            
           'S1',  --@COVERAGE_TYPE                                      
           'MEDPM'  --@COVERAGE_CODE                              
     END                          
                       
                                
                         
--END OF  Coverage F – Medical Payments to Others----------------------------------                    
                
 /*            
 793 NULL DWELL Coverage A – Dwelling            
 773 NULL DWELL Coverage A – Dwelling            
 774 NULL OSTR Coverage B – Other Structures            
 794 NULL OSTR Coverage B – Other Structures            
 775 NULL LPP Coverage C – Landlords Personal Property            
 795 NULL LPP Coverage C – Landlords Personal Property            
 776 NULL RV Coverage D – Rental Value            
 796 NULL RV Coverage D – Rental Value            
             
             
 --Check for additional            
 785 NULL RTE Radio & Television Equipment            
 805 NULL RTE Radio & Television Equipment            
 806 NULL SD Satellite Dishes            
 786 NULL SD Satellite Dishes            
 784 NULL TSPL Trees, Shrubs, Plants & Lawns            
 804 NULL TSPL Trees, Shrubs, Plants & Lawns            
 787 NULL ACS Awnings, Canopies or Signs            
 807 NULL ACS Awnings, Canopies or Signs            
 799 NULL BOSTR Coverage B – Other Structures Rented to Others            
 779 NULL BOSTR Coverage B – Other Structures Rented to Others            
 800 NULL BIAA Building Improvements, Alterations & Additions            
 780 NULL BIAA Building Improvements, Alterations & Additions            
 782 NULL CS Contents in Storage            
 802 NULL CS Contents in Storage            
 803 NULL ARV Coverage D – Additional Rental Value            
 783 NULL ARV Coverage D – Additional Rental Value            
             
 */                 
   --Update relevant coverages with deductible            
  UPDATE APP_DWELLING_SECTION_COVERAGES                           
  SET DEDUCTIBLE = @DEDUCTIBLE            
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                              
   APP_ID = @APP_ID AND                              
   APP_VERSION_ID = @APP_VERSION_ID AND                              
   DWELLING_ID = @DWELLING_ID AND            
  (             
           COVERAGE_CODE_ID IN            
    (            
   793,            
   773,            
   774,            
   794,            
   775,            
   795,            
   776,            
   796            
     ) OR            
    (             
      COVERAGE_CODE_ID IN            
     (            
    785,            
    805,            
    806,            
    786,            
    784,            
    804,            
    787,            
    807,            
    799,            
    779,            
    780,            
    800,            
    782,       
    802,  
    803,            
    783              
                
      ) AND DEDUCTIBLE_1 IS NOT NULL            
               
    )  
 )            
            
            
                      
END                   
                              
------------------Rnd of Rental---------------------------------                              
                           
                                            
END                             










GO

