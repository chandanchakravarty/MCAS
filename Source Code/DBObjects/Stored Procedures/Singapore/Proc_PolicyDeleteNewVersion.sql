--begin tran          
 --drop proc Proc_PolicyDeleteNewVersion                                                                 
 --go           
/*----------------------------------------------------------                                              
Proc Name       : dbo.Proc_PolicyDeleteNewVersion                                              
Created by      : Vijay Arora                              
Date            : 29-12-2005                              
Purpose         : Delete the newly created version of the Policy (Only PPA, M/C, Rental, W/C, Home has been done)                              
Revison History :                                              
Used In         : Wolverine                                              
                      
Modified by  : Ravindra Gupta                       
Modified On  : March -17-2006                      
Purpose   : Delete data for LOB - Umbrella                      
                      
Modified by  : Ravindra Gupta                       
Modified On  : March -24-2006                      
Purpose   : Delete data for POL_UMBRELLA_MVR_INFORMATION in LOB - Umbrella               
                     
Modified by  : Pravesh K Chandel          
Modified On  : April -20-2007                      
Purpose   : Delete data for ACT_POL_EFT_CUST_INFO          
        
Modified by  :Praveen Kasana         
Modified On  : Sep -18-2007                      
Purpose   : Delete data for POL_OPERATOR_ASSIGNED_BOAT           
        
Modified by  :Praveen Kasana : Delete from EFT (April -20-2007) & Credit Card tables            
Modified On  : April -21-2008                      
Purpose   : Delete data for ACT_POL_CREDIT_CARD_DETAILS           
        
Reviewed By : Anurag Verma        
Reviewed On : 12-07-2007        
---------------------------------------------------------- */                                    
                      
-- drop proc dbo.Proc_PolicyDeleteNewVersion           
                      
alter PROC [dbo].[Proc_PolicyDeleteNewVersion]                                                                 
(                                                          
 @CUSTOMER_ID int,                                                          
 @POLICY_ID  int,                                                          
 @POLICY_VERSION_ID smallint                                                          
)                                                          
AS                                                          
BEGIN                                                          
                                                
-- BEGIN TRAN                                                
   DECLARE @TEMP_ERROR_CODE INT                                               
                                        
   /* Get LOB of Current Policy */                                                                    
   Declare @RISKID int                                                          
   Set @RISKID= (SELECT POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID)                                                          
                                                   
   /*RISKIDs                                                          
   1.  Homeowners                                                          
   2.  Private Passenger                                                          
   3.  Motorcycle                                                          
   4.  Watercraft                                                          
   5.  Umbrella                                                          
   6.  Rental Dwelling                                                          
   7.  General Liability          
  8. AVIATION        
   */                
      
IF (@RISKID in (9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,25,26,27,28,29,30,31,32,33,34,35,36,37))  --change by Lalit April 20,2011.LOB_ID = 23 not added for delete value        
BEGIN            
    DELETE FROM POL_PRODUCT_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM           
    
     
 DELETE FROM POL_PROTECTIVE_DEVICES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
 DELETE FROM POL_DISCOUNT_SURCHARGE WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
 --Added By Lalit ,April 05,2011 .i-track #482    
 --delete BENEFICIARY tab data     
  DELETE FROM POL_BENEFICIARY WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
     
                
END            
IF (@RISKID in( 9,26))          
BEGIN           
              
 DELETE FROM POL_PERILS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
END        
IF (@RISKID in (13)  )        
BEGIN           
              
 DELETE FROM POL_MARITIME WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
END        
IF (@RISKID in (15,21,33,34))          
BEGIN           
              
 DELETE FROM POL_PERSONAL_ACCIDENT_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
END        
IF (@RISKID in(20,23))          
BEGIN           
              
 DELETE FROM POL_COMMODITY_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
END        
IF (@RISKID in(17,18,28,29,30,31,36))          
BEGIN           
              
 DELETE FROM POL_CIVIL_TRANSPORT_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
END        
IF (@RISKID in(10,11,12,14,16,19,25,27,32))          
BEGIN           
              
 DELETE FROM POL_PRODUCT_LOCATION_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
END        
IF (@RISKID =22)          
BEGIN           
        
 DELETE FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
END        
        
IF (@RISKID  in (35,37))          
BEGIN           
        
 DELETE FROM POL_PENHOR_RURAL_INFO        
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                     
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
END        
 IF (@RISKID = 8)                                                 
 BEGIN                            
                            
  DELETE FROM POL_AVIATION_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM           
        
  DELETE FROM POL_AVIATION_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
END                              
                                                    
   -- Private Passenger or Motorcycle                                                          
                             
   IF (@RISKID = 2 OR @RISKID = 3 OR @RISKID = 38)                                                 
 BEGIN                            
  DELETE FROM POL_AUTO_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                     
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                          
  DELETE FROM POL_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR               
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                     
                            
  DELETE FROM POL_ADD_OTHER_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                            
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_VEHICLE_ENDORSEMENTS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                         
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM               
        
 DELETE FROM POL_DRIVER_ASSIGNED_VEHICLE WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
            
  if(@RISKID=2) --delete data at POL_MISCELLANEOUS_EQUIPMENT_VALUES for Automobile            
  begin            
  DELETE FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
        
  DELETE FROM POL_UNDERWRITING_TIER WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                     
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM              
  end            
                                              
                            
  DELETE FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
 END             
          
                                          
   --ONLY HomeOwners              
IF (@RISKID = 1)                                              
BEGIN                            
         
--Added for Itrack Issue 6731 on 11 Nov 09 -- Chimney Stove has dependency on Solid Fuel.        
 DELETE FROM POL_HOME_OWNER_CHIMNEY_STOVE WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM        
          
 DELETE FROM POL_HOME_OWNER_FIRE_PROT_CLEAN WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                  
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
           
 DELETE FROM POL_HOME_OWNER_SOLID_FUEL WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
           
 DELETE FROM POL_HOME_OWNER_PER_ART_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                         
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
        
 DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POL_ID=@POLICY_ID and POL_VERSION_ID=@POLICY_VERSION_ID                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM             
           
 DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
           
 DELETE FROM POL_OTHER_LOCATIONS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                      
END                                           
                
   -- Homeowners or Rental Dwelling                                                 
   IF (@RISKID = 1 OR @RISKID =6)                                               
 BEGIN                            
  DELETE FROM POL_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
--Added for Itrack Issue 6731 on 11 Nov 09 -- Additional Interest has dependency on Recreational Vehicles.        
DELETE FROM POL_HOMEOWNER_REC_VEH_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                 
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
        
  DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_DWELLING_ENDORSEMENTS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_DWELLING_COVERAGE WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                        
  DELETE FROM POL_HOME_OWNER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_HOME_RATING_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
                          
 DELETE FROM POL_DWELLING_SECTION_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                             
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                              
        
 DELETE FROM POL_OTHER_STRUCTURE_DWELLING WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
                        
 DELETE FROM POL_DWELLINGS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_LOCATIONS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                    
                        
 DELETE FROM POL_HOME_OWNER_CHIMNEY_STOVE WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                           
                        
                         
 END                            
                  
                                              
   --For HOmeOWner and Watercraft                                           
   IF (@RISKID = 1 or @RISKID = 4)                               
 BEGIN                            
  DELETE FROM POL_WATERCRAFT_EQUIP_DETAILLS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_WATERCRAFT_TRAILER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_WATERCRAFT_TRAILER_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
               
  DELETE FROM POL_WATERCRAFT_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                              
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_WATERCRAFT_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                  
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                          
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_WATERCRAFT_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                             
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                   
                            
  DELETE FROM POL_WATERCRAFT_COV_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID   SELECT @TEMP_ERROR_CODE = @@ERROR                                  
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                            
  DELETE FROM POL_WATERCRAFT_ENDORSEMENTS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_WATERCRAFT_COVERAGE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                            
  DELETE FROM POL_WATERCRAFT_ENGINE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
                      
  DELETE FROM POL_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM          
--Added by praveen kasana        
 DELETE FROM POL_OPERATOR_ASSIGNED_BOAT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                               
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                   
                            
 END                        
                      
                      
 --- Added By Ravindra (03-17-2006)                      
 --  To delete policy record  for Umbrella LOB                      
                      
 --For Umbrella                         
 IF (@RISKID = 5)                      
   BEGIN                            
                           
------- 1. POL_UMBRELLA_LIMITS                      
                      
     DELETE FROM POL_UMBRELLA_LIMITS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                                 
                  
-------- 2. POL_UMBRELLA_RATING_INFO                      
                      
     DELETE FROM POL_UMBRELLA_RATING_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                     
     SELECT @TEMP_ERROR_CODE = @@ERROR                                  
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                                 
                      
-------- 3. POL_UMBRELLA_DWELLINGS_INFO                      
                      
     DELETE FROM POL_UMBRELLA_DWELLINGS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                                 
                            
                                
------- 4.  POL_UMBRELLA_REAL_ESTATE_LOCATION                      
                      
     DELETE FROM POL_UMBRELLA_REAL_ESTATE_LOCATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM             
                            
                    
                      
-------- 5. POL_UMBRELLA_VEHICLE_INFO                      
                      
     DELETE FROM POL_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                   
                            
-------- 6.  POL_UMBRELLA_RECREATIONAL_VEHICLES                      
                      
     DELETE FROM POL_UMBRELLA_RECREATIONAL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                         
                      
--------- 7.  POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                      
                      
     DELETE FROM POL_UMBRELLA_WATERCRAFT_ENGINE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                            
       
-------- 8. POL_UMBRELLA_WATERCRAFT_INFO                      
                      
     DELETE FROM POL_UMBRELLA_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                 
                             
                      
------------- 9. POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                      
                      
     DELETE FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                     
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                          
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                 
                      
                
---------- 10. POL_UMBRELLA_UNDERLYING_POLICIES                      
                      
                      
     DELETE FROM POL_UMBRELLA_UNDERLYING_POLICIES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                           
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                 
                      
                
------------- 11.   POL_UMBRELLA_MVR_INFORMATION                      
                      
     DELETE FROM POL_UMBRELLA_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                       
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                 
                      
                      
                      
------------- 12.   POL_UMBRELLA_DRIVER_DETAILS                      
                      
     DELETE FROM POL_UMBRELLA_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                 
                      
-------------- 13.  POL_UMBRELLA_FARM_INFO                      
          
     DELETE FROM POL_UMBRELLA_FARM_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID      
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR             
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                 
          
------- 14. POL_UMBRELLA_GEN_INFO                      
     DELETE FROM POL_UMBRELLA_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID              
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                                 
--------15 POL_UMBRELLA_COVERAGES   aded by Praveah        
                    
   DELETE FROM POL_UMBRELLA_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POL_ID=@POLICY_ID              
     and POL_VERSION_ID=@POLICY_VERSION_ID                                 
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
     IF (@TEMP_ERROR_CODE <> 0)                       
  GOTO PROBLEM                                  
                            
                      
                      
                         
   END                      
--- Added By Ravindra Ends Here                      
-----deleting other Policy level tabels        
DELETE FROM POL_LOCATIONS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
        
DELETE FROM POL_CO_INSURANCE WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
        
DELETE FROM POL_REMUNERATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
        
DELETE FROM POL_CLAUSES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
        
DELETE FROM POL_DISCOUNT_SURCHARGE WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
  
DELETE FROM POL_REINSURANCE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                
---------         
--added by Pravesh          
 DELETE FROM ACT_POL_EFT_CUST_INFO  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
--end here          
--Added by Kasana  :         
 DELETE FROM ACT_POL_CREDIT_CARD_DETAILS  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                      
--end here          
        
 DELETE FROM QOT_CUSTOMER_QUOTE_LIST_POL  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
          
 DELETE FROM POL_APPLICANT_LIST  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
            
            
--For Billing info details         
        
 DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
          
 DELETE FROM ACT_POLICY_INSTALL_PLAN_DATA  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM             
        
        
--Added By Lalit Chauhan May 05,2011  
--itrack # 1083  
DELETE FROM POL_POLICY_ENDORSEMENTS_DETAILS WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID  
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
  
DELETE FROM POL_POLICY_ENDORSEMENTS WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID  
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
    
                            
DELETE FROM POL_CUSTOMER_POLICY_LIST  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                 
        
        
        
  
  
  
                      
                            
--  COMMIT TRAN                                                    
 RETURN 1                                                                     
  PROBLEM:                                                               
   --ROLLBACK TRAN                                                                      
                              
END                                                     
                                 
--go   
--exec Proc_PolicyDeleteNewVersion 3680,4,2    
--rollback tran  