IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                              
Proc Name       : dbo.Proc_DeletePolicy                          
Created by      : Swarup                            
Date            : 04-01-2007                             
Purpose         :                             
Revison History :                              
Used In         : Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                       
-- Drop Procedure dbo.Proc_DeletePolicy      
-- exec Proc_DeletePolicy @CustomerId = 949    
                 
CREATE  PROC dbo.Proc_DeletePolicy                                           
(                                                      
@CustomerId int                                                      
--@Polid  int = null,        
--@Polversionid int =null                                                     
                                                      
)                                                      
AS                              
BEGIN                   
-- To Change Application status          
/*DECLARE @APP_ID INT          
DECLARE @APP_VERSION_ID INT          
Select @APP_ID=APP_ID, @APP_VERSION_ID=APP_VERSION_ID FROM pol_customer_policy_list        
WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid and POLICY_VERSION_ID=@Polversionid */       
      
--      
      
                       
/*IF (@Polid is not NULL)                  
BEGIN                   
                
 Declare @RISKID int                                              
 Set @RISKID= (select distinct policy_lob from pol_customer_policy_list WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid and POLICY_VERSION_ID=@Polversionid)           
          
  /* DELETE  THE LOB SPECIFIC DATA                                                
     RISKIDs                                              
                                          
   1. Homeowners                                              
   2. Private Passenger                                              
   3. Motorcycle                                              
   4. Watercraft                                              
   5. Umbrella                                              
   6. Rental Dwelling                                       
   7. General Liability                                             
  */  */        
          
          
  -- Private Passenger or Motorcycle                                              
 /* IF  (@RISKID = 2 OR @RISKID =3)                                              
  BEGIN           
           
    DELETE FROM POL_MVR_INFORMATION                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_AUTO_GEN_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_DRIVER_ASSIGNED_VEHICLE                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_DRIVER_DETAILS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_ADD_OTHER_INT                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_VEHICLE_ENDORSEMENTS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_VEHICLE_COVERAGES                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
  
    DELETE FROM POL_VEHICLES                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
            
  END     
           
           
  -- HOMEOWNERS or Rental Dwelling --------------------                                              
  IF(@RISKID = 1 or @RISKID = 6 )                          
  BEGIN            
    DELETE FROM POL_HOME_OWNER_GEN_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOME_OWNER_CHIMNEY_STOVE                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOME_OWNER_FIRE_PROT_CLEAN                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOME_OWNER_SOLID_FUEL                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOME_OWNER_PER_ART_GEN_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                  
    WHERE CUSTOMER_ID=@CustomerId AND POL_ID=@Polid                  
                     
    DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOMEOWNER_REC_VEH_ADD_INT                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_DWELLING_ENDORSEMENTS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_OTHER_LOCATIONS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_DWELLING_COVERAGE                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_DWELLING_SECTION_COVERAGES                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOME_OWNER_ADD_INT                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_HOME_RATING_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_OTHER_STRUCTURE_DWELLING                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_DWELLINGS_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_LOCATIONS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid             
            
  END          
       
  --UMBRELLA-----------------                                        
  IF(@RISKID = 5)                                              
  BEGIN            
    DELETE FROM POL_UMBRELLA_FARM_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_GEN_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_EMPLOYMENT_INFO                   
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_EMPLOYMENT_INFO     
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_POL_INFO_OTHER                   
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid             
         
    DELETE FROM POL_UMBRELLA_MVR_INFORMATION                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_RECREATIONAL_VEHICLES                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                    
    DELETE FROM POL_UMBRELLA_DRIVER_DETAILS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_LIMITS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_COVERAGES                  
    WHERE CUSTOMER_ID=@CustomerId AND POL_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_WATERCRAFT_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_RATING_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                      
                     
    DELETE FROM POL_UMBRELLA_DWELLINGS_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_REAL_ESTATE_LOCATION                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                      
    DELETE FROM POL_UMBRELLA_VEHICLE_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_UMBRELLA_UNDERLYING_POLICIES                   
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid           
           
  END          
        
  -- For General Liability                                       
  IF(@RISKID=7 )           
  BEGIN          
    DELETE FROM POL_GENERAL_LIABILITY_DETAILS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_GENERAL_COVERAGE_LIMITS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_GENERAL_UNDERWRITING_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_GENERAL_HOLDER_INTEREST                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid            
            
    DELETE FROM POL_LOCATIONS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid             
           
  END          
  -- WATERCRAFT  or HOMEOWNERS                                          
                                    
 IF (@RISKID = 4  or @RISKID = 1)          
                                          
  BEGIN            
    DELETE FROM POL_WATERCRAFT_ENDORSEMENTS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_WATERCRAFT_TRAILER_ADD_INT                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
    DELETE FROM POL_WATERCRAFT_COV_ADD_INT                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_WATERCRAFT_GEN_INFO 
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_WATERCRAFT_MVR_INFORMATION                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_WATERCRAFT_DRIVER_DETAILS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid              
                     
    DELETE FROM POL_WATERCRAFT_TRAILER_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_WATERCRAFT_EQUIP_DETAILLS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_WATERCRAFT_ENGINE_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_WATERCRAFT_COVERAGE_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_WATERCRAFT_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
                      
    DELETE FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid           
  END          
           
           
           
    DELETE FROM CLM_CLAIM_INFO                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
                     
           
                     
    DELETE FROM POL_POLICY_PROCESS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_POLICY_ENDORSEMENTS_DETAILS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_POLICY_ENDORSEMENTS                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
                     
    DELETE FROM POL_POLICY_NOTICE_MASTER                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_EOD_TRANSACTIONS_DETAIL                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    --DELETE FROM POL_EOD_TRANSACTIONS_MASTER                  
   --WHERE CUSTOMER_ID=@CustomerId AND POL_ID=@Polid                  
   --                  
           
                      
                      
    DELETE FROM POL_APPLICANT_LIST                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid                  
                     
    DELETE FROM POL_CUSTOMER_POLICY_LIST                  
    WHERE CUSTOMER_ID=@CustomerId AND POLICY_ID=@Polid             
          
  --Delete Accounting Data For this policy           
 exec Proc_CleanAccountingDateCompletly @CustomerId,@PolID          
                
END                  
ELSE */                 
                     
                            
                            
   BEGIN TRAN                                                                      
   DECLARE @TEMP_ERROR_CODE INT                   
                            
                             
    DELETE FROM POL_WATERCRAFT_ENDORSEMENTS                            
    WHERE CUSTOMER_ID=@CustomerId                             
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM             
                           
    DELETE FROM POL_WATERCRAFT_TRAILER_ADD_INT       
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM           
                             
    DELETE FROM POL_WATERCRAFT_COV_ADD_INT                            
    WHERE CUSTOMER_ID=@CustomerId                            
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM           
                               
    DELETE FROM POL_WATERCRAFT_GEN_INFO        
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                          
                               
    DELETE FROM POL_WATERCRAFT_MVR_INFORMATION                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                               
    DELETE FROM POL_WATERCRAFT_DRIVER_DETAILS                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
    
    --Added 18 sep 2007
    DELETE FROM POL_OPERATOR_ASSIGNED_BOAT                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                         
                               
    DELETE FROM POL_WATERCRAFT_TRAILER_INFO                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                               
    DELETE FROM POL_WATERCRAFT_EQUIP_DETAILLS                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_WATERCRAFT_ENGINE_INFO                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_WATERCRAFT_COVERAGE_INFO                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_WATERCRAFT_INFO                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
                                
    DELETE FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
                               
    DELETE FROM POL_HOME_OWNER_GEN_INFO                            
    WHERE CUSTOMER_ID=@CustomerId         
    SELECT @TEMP_ERROR_CODE = @@ERROR           
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_HOME_OWNER_CHIMNEY_STOVE                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_HOME_OWNER_FIRE_PROT_CLEAN                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                               
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_HOME_OWNER_SOLID_FUEL                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_HOME_OWNER_PER_ART_GEN_INFO                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_HOMEOWNER_REC_VEH_ADD_INT                      
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_DWELLING_ENDORSEMENTS                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_OTHER_LOCATIONS                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_DWELLING_COVERAGE                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                   
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_DWELLING_SECTION_COVERAGES                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                       
                               
    DELETE FROM POL_HOME_OWNER_ADD_INT                            
      WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                     
    DELETE FROM POL_HOME_RATING_INFO                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_OTHER_STRUCTURE_DWELLING                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                               
    DELETE FROM POL_DWELLINGS_INFO                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                       
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_LOCATIONS                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
                               
    DELETE FROM POL_MVR_INFORMATION                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                               
    DELETE FROM POL_AUTO_GEN_INFO                            
    WHERE CUSTOMER_ID=@CustomerId               
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
                               
    DELETE FROM POL_DRIVER_ASSIGNED_VEHICLE                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_DRIVER_DETAILS                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_ADD_OTHER_INT                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_VEHICLE_ENDORSEMENTS                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
                               
 DELETE FROM POL_VEHICLE_COVERAGES                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                               
    DELETE FROM POL_VEHICLES                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_FARM_INFO                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_UMBRELLA_GEN_INFO              
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                        
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_UMBRELLA_EMPLOYMENT_INFO                             
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_EMPLOYMENT_INFO                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_POL_INFO_OTHER                     
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                              
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_UMBRELLA_MVR_INFORMATION                     
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_RECREATIONAL_VEHICLES                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_UMBRELLA_DRIVER_DETAILS                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR          
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_LIMITS                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_UMBRELLA_COVERAGES                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                          
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_WATERCRAFT_INFO                            
    WHERE CUSTOMER_ID=@CustomerId              
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
                               
    DELETE FROM POL_UMBRELLA_RATING_INFO                            
    WHERE CUSTOMER_ID=@CustomerId             
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
                                
                               
    DELETE FROM POL_UMBRELLA_DWELLINGS_INFO                            
    WHERE CUSTOMER_ID=@CustomerId            
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                           
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                               
    DELETE FROM POL_UMBRELLA_REAL_ESTATE_LOCATION                            
    WHERE CUSTOMER_ID=@CustomerId          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
                               
    DELETE FROM POL_UMBRELLA_VEHICLE_INFO                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                               
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
    DELETE FROM POL_UMBRELLA_UNDERLYING_POLICIES                             
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
                               
    DELETE FROM POL_POLICY_PROCESS                            
    WHERE CUSTOMER_ID=@CustomerId            
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                               
    DELETE FROM POL_POLICY_ENDORSEMENTS_DETAILS                            
    WHERE CUSTOMER_ID=@CustomerId             
    SELECT @TEMP_ERROR_CODE = @@ERROR 
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
                               
    DELETE FROM POL_POLICY_ENDORSEMENTS                            
    WHERE CUSTOMER_ID=@CustomerId            
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                               
                               
    DELETE FROM POL_POLICY_NOTICE_MASTER                            
    WHERE CUSTOMER_ID=@CustomerId             
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                     
                               
    DELETE FROM POL_EOD_TRANSACTIONS_DETAIL                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                               
                        
                               
         
    DELETE FROM POL_GENERAL_LIABILITY_DETAILS                            
    WHERE CUSTOMER_ID=@CustomerId           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                               
    DELETE FROM POL_GENERAL_COVERAGE_LIMITS                            
    WHERE CUSTOMER_ID=@CustomerId            
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                               
    DELETE FROM POL_GENERAL_UNDERWRITING_INFO                            
    WHERE CUSTOMER_ID=@CustomerId            
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                               
    DELETE FROM POL_GENERAL_HOLDER_INTEREST                            
    WHERE CUSTOMER_ID=@CustomerId            
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
                               
                               
    DELETE FROM POL_APPLICANT_LIST                            
    WHERE CUSTOMER_ID=@CustomerId             
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               


	DELETE FROM ACT_POL_EFT_CUST_INFO
	WHERE CUSTOMER_ID=@CustomerId              
	SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
		IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 

            
                               
    --Delete Accounting Data For this Customer                    
    exec Proc_CleanAccountingDateCompletly @CustomerId,null           
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM   
                   
    DELETE FROM POL_CUSTOMER_POLICY_LIST                            
    WHERE CUSTOMER_ID=@CustomerId              
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 

	
	DELETE FROM CLT_PREMIUM_SPLIT_EXCEPTIONS WHERE SPLIT_UNIQUE_ID IN (SELECT UNIQUE_ID FROM  CLT_PREMIUM_SPLIT WHERE CUSTOMER_ID=@CustomerId)
	SELECT @TEMP_ERROR_CODE = @@ERROR                                                              
	IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 

	DELETE FROM CLT_PREMIUM_SPLIT_DETAILS WHERE SPLIT_UNIQUE_ID IN (SELECT UNIQUE_ID FROM  CLT_PREMIUM_SPLIT WHERE CUSTOMER_ID=@CustomerId)
	SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
	IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 

	DELETE FROM CLT_PREMIUM_SPLIT WHERE CUSTOMER_ID=@CustomerId
	SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
	IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 


                   
    DELETE FROM QOT_CUSTOMER_QUOTE_LIST_POL  
 WHERE CUSTOMER_ID=@CustomerId              
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
         IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
   
 /*-- for Clear Data From ToDolist    
 update todolist    
 set policyid=null,    
 policyclientid=null,    
 policyversion=null,    
 POLICYCARRIERID=null,    
 POLICYBROKERID=null,    
 POLICY_ID=null,    
 POLICY_VERSION_ID=null      
 where customer_id= @CustomerId      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
       IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      */  
  
 DELETE FROM TODOLIST  
 WHERE CUSTOMER_ID=@CustomerId              
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
         IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
  
      
    DELETE FROM MNT_TRANSACTION_XML  
 WHERE TRANS_ID IN (SELECT TRANS_ID FROM  MNT_TRANSACTION_LOG WHERE  CLIENT_ID = @CustomerId)            
   DELETE FROM MNT_TRANSACTION_LOG  
 WHERE CLIENT_ID=@CustomerId              
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
         IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM    
    
                   
    
    
    UPDATE APP_LIST                    
    set app_status = 'Incomplete'                     
    where CUSTOMER_ID=@CustomerId             
    SELECT @TEMP_ERROR_CODE = @@ERROR                       
      IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
                   
                    
  COMMIT TRAN                                           
   RETURN 1      
                                                             
       
                                                                                                     
  PROBLEM:                                                        
   IF (@TEMP_ERROR_CODE <> 0)                                            
    BEGIN                                                                                                    
      ROLLBACK TRAN                                                                                                     
      RETURN -1      
    END                                                                                                    
                                                                                                       
        
--return @@ERROR                            
END      
      
  






GO

