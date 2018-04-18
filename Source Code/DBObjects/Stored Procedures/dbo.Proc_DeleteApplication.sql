IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteApplication]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_DeleteApplication                                      
Created by      : nidhi                                      
Date            : 6/14/2005                                      
Purpose     : Delete  Application based on certain parameters passed to it                                      
Revison History :                                      
Used In  : Wolverine                                       
                                      
Modified by : Anurag Verma                                      
Modified On : 4/8/2005                                       
Purpose  : deleteing app_dwelling_coverage record before app_dwelling_info record                                      
                                      
Modified by : Anurag Verma                                      
Modified On : 14/09/2005                                       
Purpose  : removing delete query for APP_DRIVER_DISCOUNTS table and APP_ASSIGN_VEHICLE because driver discount and assign vehicle is merged into driver detail screen                                      
                                      
Modified by : Pradeep Iyer                                      
Modified On : 22/09/2005                                       
Purpose  : Removed delete statements for APP_PROTECT_DEVICES, APP_SQR_FOOT_IMPROVEMENTS and APP_HOME_CONSTRUCTION_INFO.                                      
                                      
Modified by : Anurag Verna                                      
Modified On : 22/09/2005                                       
Purpose  : Removed delete statements for deleting APP_WATERCRAFT_DRIVER_DISCOUNTS table and APP_WATERCRAFT_ASSIGN_VEHICLE table                                      
                                      
Modified By : Anurag Verma                                      
Modified On : 06/10/2005                                      
Purpose  : Removing delete query for APP_WATERCRAFT_HULL_INFO                                      
                                      
Modified By : Vijay Arora                                      
Modified On : 07/10/2005                                      
Purpose  : Added the Application Attachment records flag to 'N' for MNT_ATTACHMENT_LIST                                      
                                      
Modified By : Anurag Verma                                      
Modified On : 10/10/2005                                      
Purpose  : Removing APP_WATERCRAFT_TRAILERS_COVE_INFO delete query                                       
                                      
Modified By : Anurag Verma                                      
Modified On : 11/10/2005                                      
Purpose  : Removing APP_WATERCRAFT_ENGINE_COVE_INFO delete query                                       
                                      
Modified By : Anurag Verma                                      
Modified On : 11/10/2005                                      
Purpose  : Removing APP_WATERCRAFT_ENG_ADD_INT delete query                                       
                                    
Modified By : Pradeep                                      
Modified On : 10/21/2005                                      
Purpose  : Added code to remove Vehicle and Coverage endorsements                                    
                                  
Modified By : Shafi                                      
Modified On : 01/16/2006                                  
Purpose  : Added code to remove  endorsements and In proper Heirarchy                                   
                                
Modified By : Ashwani                                      
Modified On : 02/02/2006                                  
Purpose     : Reviwed & Added the missing tables                            
                          
Modified By : Ravindra Gupta                           
Modified On : March-16-2006                          
Purpose   : To remove Delete query for                          
 APP_UMBRELLA_VEHICLE_COV_IFNO                          
   APP_UMBRELLA_VEHICLE_ENDORSEMENTS                     
        APP_UMBRELLA_RECREATIONAL_VEHICLES_COVERAGES                          
    APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS   & COVERAGES                     
    APP_UMBRELLA_OPERATOR_INFO                          
                        
  And Add Query for                           
   APP_UMBRELLA_DWELLINGS_INFO                          
   APP_UMBRELLA_RATING_INFO                          
   APP_UMBRELLA_FARM_INFO                          
   APP_UMBRELLA_UMDERLYING_POLICIES                          
   APP_UMBRELLA_UMDERLYING_POLICIES_COVERAGES                           
                          
Modified By : Ravindra Gupta                           
Modified On : March-24-2006                          
Purpose   : To add Delete query  for                            
   APP_UMBRELLA_MVR_INFORMATION                       
                      
                      
Modified By : Ravindra Gupta                           
Modified On : March-31-2006                          
Purpose   : To add Delete query  for                            
   APP_GENERAL_COVERAGE_LIMIT_INFO                                      
  APP_GENERAL_DEDUCTIBLES_COMMISSION                                      
 And Added                       
   APP_GENERAL_LIABILITY_DETAILS                      
   APP_GENERAL_COVERAGE_LIMITS                      
 In General Liability Module                           
              
              
Modified By : RPSINGH              
Modified On : 11/05/2006              
Purpose  : Removing 'APP_UMBRELLA_EMPLOYMENT_INFO' delete query . Table is dropped              
              
Modified By : PRAVESH CHANDEL              
Modified On : 18/10/2006              
Purpose  : Removing 'APP_UMBRELLA_COVERAGES' delete query .     

Modified By : Praveen kasana
Modified On : 18/09/2007              
Purpose  : Removing 'APP_OPERATOR_ASSIGNED_BOAT' delete query .    
   
        
Reviewed By	:	Anurag Verma
Reviewed On	:	04/07/2007                   
------------------------------------------------------------                                     
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                    
                          
-- drop proc Proc_DeleteApplication                                              
CREATE  PROC dbo.Proc_DeleteApplication                                      
(                                      
@CUSTOMER_ID int,                                      
@APP_ID  int,                                      
@APP_VERSION_ID smallint                              
                                      
                                      
)                                      
AS                                      
BEGIN                                      
                                      
Declare @RISKID int                                      
Set @RISKID= (SELECT APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID)                                      
                                      
/* DELETE  THE LOB SPECIFIC DATA                                        
   RISKIDs                                      
                                
 1. Homeowners                                      
 2. Private Passenger                                      
 3. Motorcycle                                      
 4. Watercraft                                      
 5. Umbrella                                       6. Rental Dwelling                               
 7. General Liability                                     
*/          
                                      
                              
 -- Private Passenger or Motorcycle                                      
 IF  (@RISKID = 2 OR @RISKID =3)                                      
 BEGIN                                      
      
                                        
                                        
  --APP_AUTO_APPLICANT_DETAILS                                      
  --DELETE FROM APP_AUTO_APPLICANT_DETAILS                                       
  --WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                        
                                        
  --APP_AUTO_GEN_INFO                                      
  DELETE  FROM APP_AUTO_GEN_INFO                                       
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                      
                                      
                                        
  --APP_VEHICLE_COVERAGES                                      
  DELETE  FROM APP_VEHICLE_COVERAGES                                        
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID    
  
--start   
--APP_DRIVER_ASSIGNED_VEHICLE                                      
  DELETE  FROM APP_DRIVER_ASSIGNED_VEHICLE                                        
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID    
--end                           
                                        
  --APP_VEHICLE_ENDORSEMENTS                                    
  DELETE  FROM APP_VEHICLE_ENDORSEMENTS                                 
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                       
                         
  --APP_ADD_OTHER_INT                                   
  DELETE FROM  APP_ADD_OTHER_INT                                       
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                  
  --APP_AUTO_ID_CARD_INFO                                       
  DELETE  FROM APP_AUTO_ID_CARD_INFO                                       
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
      
 -- Added by Swarup <Start> 04/02/2007     
 --APP_MISCELLANEOUS_EQUIPMENT_VALUES                              
  DELETE  FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES                                     
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
  --<End>    
    
  -- Added by Ashwani <Start> 02/02/2006                                 
  --APP_VEHICLES                                      
  DELETE  FROM APP_VEHICLES                                      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                        
 --<End>                    
                                 
  -- Added by Ashwani <Start> 02/02/2006                                 
  -- APP_MVR_INFORMATION                                 
   DELETE  FROM APP_MVR_INFORMATION                                      
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                        
  --<End>                                     
                              
  --APP_DRIVER_DETAILS                                      
  DELETE FROM APP_DRIVER_DETAILS                                      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                      
  --APP_VEHICLES                                       
  DELETE FROM APP_VEHICLES WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
 END                                       
                                      
 -- HOMEOWNERS or Rental Dwelling--------------------                                      
 IF(@RISKID = 1 or @RISKID = 6)                                      
 BEGIN                                      
  --APP_HOME_OWNER_ADD_INT                                      
DELETE FROM APP_HOME_OWNER_ADD_INT                                       
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
              
--COMMENTED AS TABLE DOES NOT EXISTS NOW                                     
  --APP_HOME_APPLICANT_DETAILS                                      
  --DELETE  FROM APP_HOME_APPLICANT_DETAILS                                      
  --WHERE  CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                      
  --APP_HOME_OWNER_GEN_INFO                                      
  DELETE FROM APP_HOME_OWNER_GEN_INFO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                       
                                        
                                        
  --APP_HOME_CONSTRUCTION_INFO                                      
  --DELETE  FROM APP_HOME_CONSTRUCTION_INFO                                       
  --WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                               
                                         
  --APP_HOME_OWNER_CHIMNEY_STOVE                                        
  DELETE  FROM APP_HOME_OWNER_CHIMNEY_STOVE                                      
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                                        
  --APP_HOME_OWNER_FIRE_PROT_CLEAN                                      
  DELETE FROM APP_HOME_OWNER_FIRE_PROT_CLEAN                                      
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                         
                                        
                               
                                        
  --APP_HOME_OWNER_PER_ART_GEN_INFO                                      
  DELETE FROM APP_HOME_OWNER_PER_ART_GEN_INFO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                       
                                      
  -- APP_HOMEOWNER_REC_VEH_ADD_INT          
 DELETE  FROM  APP_HOMEOWNER_REC_VEH_ADD_INT                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID            
          
            
  --APP_HOME_OWNER_RECREATIONAL_VEHICLES             
  DELETE  FROM  APP_HOME_OWNER_RECREATIONAL_VEHICLES                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                                        
 --APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES                                      
  DELETE FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID           
        
  --APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS               
 DELETE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS         
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                            
                                        
  --APP_HOME_OWNER_SCH_ITEMS_CVGS                                      
  DELETE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                        
                             
  --APP_HOME_OWNER_SOLID_FUEL                                      
  DELETE  FROM APP_HOME_OWNER_SOLID_FUEL                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND  APP_VERSION_ID = @APP_VERSION_ID                            
                                        
  --APP_HOME_OWNER_SUB_INSU                                      
  DELETE FROM APP_HOME_OWNER_SUB_INSU                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                        
  --APP_HOME_OWNER_UNIT_CLEARANCE                                      
  DELETE  FROM APP_HOME_OWNER_UNIT_CLEARANCE                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                        
  --APP_HOME_RATING_INFO                                      
  DELETE FROM APP_HOME_RATING_INFO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                       
  --APP_PROTECT_DEVICES                                      
  --DELETE FROM APP_PROTECT_DEVICES                                      
  --WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                        
              
  --APP_SQR_FOOT_IMPROVEMENTS                                        
  --DELETE  FROM APP_SQR_FOOT_IMPROVEMENTS                                      
  --WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                
                                        
  --APP_SUB_LOCATIONS                                      
  DELETE FROM APP_SUB_LOCATIONS                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                  
--Added By Shafi         
        
--        
 DELETE FROM APP_OTHER_STRUCTURE_DWELLING        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                         
                                  
  DELETE FROM APP_DWELLING_ENDORSEMENTS                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                  
 --APP_DWELLING_SECTION_COVERAGES                                      
  DELETE FROM APP_DWELLING_SECTION_COVERAGES                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                      
                                  
  --APP_DWELLING_COVERAGE                                      
  DELETE FROM APP_DWELLING_COVERAGE                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID              
          
 --APP_OTHER_LOCATIONS          
          
  DELETE FROM APP_OTHER_LOCATIONS                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID              
          
                                  
                                   
                                  
                                      
--                                       
  --APP_DWELLINGS_INFO                                      
  DELETE FROM APP_DWELLINGS_INFO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                        
                                   
--APP_LOCATIONS                
  DELETE FROM APP_LOCATIONS                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                      
 END                  
 ---End of Homeowners, Dwelling                                      
                                       
 --UMBRELLA-----------------                                
 IF(@RISKID = 5)                                      
 BEGIN                                  
 -- Commented by Ashwani <start> 02/02/2006                                     
/*  --APP_UMBRELLA_EMPLOYMENT_INFO                                      
  DELETE FROM APP_UMBRELLA_EMPLOYMENT_INFO                                  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                 
                    
  --APP_UMBRELLA_GEN_INFO                                      
  DELETE FROM APP_UMBRELLA_GEN_INFO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                       
                                      
                                        
   --APP_UMBRELLA_LIMITS                                      
  DELETE FROM APP_UMBRELLA_LIMITS                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                       
                                       
   --APP_UMBRELLA_VEHICLE_COV_INFO                                      
  DELETE FROM APP_UMBRELLA_VEHICLE_COV_IFNO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                      
   --APP_UMBRELLA_VEHICLE_INFO                                      
  DELETE FROM APP_UMBRELLA_VEHICLE_INFO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                       
                                        
  --APP_UMBRELLA_POL_INFO_OTHER                                      
  DELETE FROM APP_UMBRELLA_POL_INFO_OTHER                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                          
                                        
                                         
  --APP_UMBRELLA_REAL_ESTATE_SUB_LOC                                
  DELETE FROM APP_UMBRELLA_REAL_ESTATE_SUB_LOC                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                      
  --APP_UMBRELLA_REAL_ESTATE_LOCATION                                      
  DELETE  FROM APP_UMBRELLA_REAL_ESTATE_LOCATION                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                      
                                      
  --APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO                                      
  DELETE FROM APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                                      
                                      
  --APP_UMBRELLA_WATERCRAFT_INFO                                      
  DELETE FROM APP_UMBRELLA_WATERCRAFT_INFO                                      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID   */                                     
 --<End by Ashwani>                                        
                                
 -- <Added by Ashwani on 02/02/2006>               
                                 
 --APP_UMBRELLA_EMPLOYMENT_INFO                                      
   --commented by RP. Table is dropped               
   --DELETE FROM APP_UMBRELLA_EMPLOYMENT_INFO                                      
   --WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                 
                                
 --1. APP_UMBRELLA_LIMITS                                
  DELETE FROM APP_UMBRELLA_LIMITS                                 
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                                 
 --2. APP_UMBRELLA_GEN_INFO                                
 DELETE FROM APP_UMBRELLA_GEN_INFO                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                                
--Commented By Ravindra(03-16-2006) Because Table Deleted                           
/*--3. APP_UMBRELLA_VEHICLE_COV_IFNO                                
 DELETE FROM APP_UMBRELLA_VEHICLE_COV_IFNO                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                          
                                
 --4. APP_UMBRELLA_VEHICLE_ENDORSEMENTS                                
 DELETE FROM APP_UMBRELLA_VEHICLE_ENDORSEMENTS                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
 */                          
                               
 --5. APP_UMBRELLA_VEHICLE_INFO                                
 DELETE FROM APP_UMBRELLA_VEHICLE_INFO                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                  
--Delete data from child tables rating and dwelling before deleting data from parent table real estate location                  
-- 6. APP_UMBRELLA_RATING_INFO                  
DELETE FROM APP_UMBRELLA_RATING_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID                           
 and APP_VERSION_ID=@APP_VERSION_ID                    
--7. APP_UMBRELLA_DWELLINGS_INFO                 
DELETE FROM APP_UMBRELLA_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID                           
 and APP_VERSION_ID=@APP_VERSION_ID                                       
                                
 --8. APP_UMBRELLA_REAL_ESTATE_LOCATION                                
 DELETE FROM   APP_UMBRELLA_REAL_ESTATE_LOCATION                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                    
                  
                                
--Commented By Ravindra(03-16-2006) Because Table Deleted            
/*                          
 --9. APP_UMBRELLA_RECREATIONAL_VEHICLES_COVERAGES                                
 DELETE FROM APP_UMBRELLA_RECREATIONAL_VEHICLES_COVERAGES                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                  
  */                          
                              
 --10. APP_UMBRELLA_RECREATIONAL_VEHICLES                                
 DELETE FROM APP_UMBRELLA_RECREATIONAL_VEHICLES                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                                
--Commented By Ravindra(03-16-2006) Because Table Deleted                          
/*                          
 --. APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS                                
 DELETE FROM APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS               
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                                
 --. APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO                                
 DELETE FROM APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
  */                          
                          
 --11. APP_WATERCRAFT_ENGINE_INFO                                 
 DELETE FROM APP_WATERCRAFT_ENGINE_INFO                                 
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                            
 --12. APP_UMBRELLA_WATERCRAFT_INFO                                
 DELETE FROM APP_UMBRELLA_WATERCRAFT_INFO                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                        
                                
-- Changed By Ravindra (03-24-2006) -- new table APP_UMBRELLA_MVR_INFORMATION added for mvr info in umbrella module                          
 --13. APP_MVR_INFORMATION                                
/* DELETE FROM APP_MVR_INFORMATION                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID      */                          
                 
  DELETE FROM APP_UMBRELLA_MVR_INFORMATION                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                           
                          
                            
                          
                          
 --14. APP_UMBRELLA_DRIVER_DETAILS                                
 DELETE FROM APP_UMBRELLA_DRIVER_DETAILS                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                            
                                
 --15. APP_WATER_MVR_INFORMATION                                
 DELETE FROM APP_WATER_MVR_INFORMATION                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                                 
--Commented By Ravindra(03-16-2006) Because Table Deleted                          
/*                           
--16. APP_UMBRELLA_OPERATOR_INFO                                
 DELETE FROM APP_UMBRELLA_OPERATOR_INFO                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                        
                           
                              
 --17. APP_UMBRELLA_POL_INFO_OTHER                                
 DELETE FROM APP_UMBRELLA_POL_INFO_OTHER                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID     */                                   
                
 --18. MNT_ATTACHMENT_LIST                                 
 --DELETE FROM MNT_ATTACHMENT_LIST                                 
 --WHERE ATTACH_CUSTOMER_ID=@CUSTOMER_ID AND ATTACH_APP_ID=@APP_ID AND ATTACH_APP_VER_ID=@APP_VERSION_ID         
                                
                                 
 -- <End Added by Ashwani >                                
                          
-- Added By Ravindra (03-16-2006)                          
                          
              
                          
                           
DELETE FROM APP_UMBRELLA_UNDERLYING_POLICIES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID                           
and APP_VERSION_ID=@APP_VERSION_ID                          
                          
                          
DELETE FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID                           
and APP_VERSION_ID=@APP_VERSION_ID                          
                          
                          
DELETE FROM APP_UMBRELLA_FARM_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID                           
and APP_VERSION_ID=@APP_VERSION_ID                          
                          
-- Added By Ravindra Ends Here        
---ADDED BY PRAVESH        
DELETE FROM APP_UMBRELLA_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID                           
and APP_VERSION_ID=@APP_VERSION_ID                          
--END HERE                          
                                      
 END                                      
 --End of UMBRELLA-----------                                      
                                      
 -- WATERCRAFT or RENTAL DWELLING or HOMEOWNERS                                      
                                
 IF (@RISKID = 4 or @RISKID = 6 or @RISKID = 1)                                 
 BEGIN                                       
 -- APP_WATERCRAFT_GEN_INFO                                       
  DELETE FROM APP_WATERCRAFT_GEN_INFO                                      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                
                                        
  --APP_WATERCRAFT_COV_ADD_INT                                      
  DELETE FROM APP_WATERCRAFT_COV_ADD_INT                                      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                 
                                      
  --APP_WATERCRAFT_TRAILER_ADD_INT                                      
  DELETE FROM APP_WATERCRAFT_TRAILER_ADD_INT                                      
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                      
  --APP_WATERCRAFT_ENDORSEMENTS                                    
  DELETE  FROM APP_WATERCRAFT_ENDORSEMENTS                                    
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                       
                                    
  --APP_WATERCRAFT_COVERAGE_INFO                                      
  DELETE  FROM APP_WATERCRAFT_COVERAGE_INFO              
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                      
  --APP_WATERCRAFT_ENGINE_INFO                                      
  DELETE  FROM APP_WATERCRAFT_ENGINE_INFO                                      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                      
  --APP_WATERCRAFT_TRAILER_INFO                                      
  DELETE FROM APP_WATERCRAFT_TRAILER_INFO                               
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                      
  --APP_WATERCRAFT_EQUIP_DETAILLS                                      
  DELETE FROM APP_WATERCRAFT_EQUIP_DETAILLS                  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                      
 -- Added by Ashwani <Start> 02/02/2006                                
  DELETE FROM APP_WATER_MVR_INFORMATION                                      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
 -- <End>                                
                                
  --APP_WATERCRAFT_DRIVER_DETAILS                                      
  DELETE FROM APP_WATERCRAFT_DRIVER_DETAILS                                     
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
                                      
  --APP_WATERCRAFT_INFO                                      
  DELETE FROM APP_WATERCRAFT_INFO                                      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID    


   
--APP_OPERATOR_ASSIGNED_BOAT                                      
  DELETE  FROM APP_OPERATOR_ASSIGNED_BOAT                                        
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID    
                                  
                                        
 END                                     
                              
 --  Added by Ashwani 02/02/2006 <start>                              
 -- For General Liability                               
 IF(@RISKID=7 )                              
 BEGIN                               
                              
 -- 1.    APP_LOCATIONS                                      
   DELETE FROM APP_LOCATIONS                                      
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                               
                      
-------Added By Ravindra(03-31-2006)                      
                      
 ---2.    APP_GENERAL_LIABILITY_DETAILS                      
   DELETE FROM APP_GENERAL_LIABILITY_DETAILS                                      
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                       
                      
 ---3.    APP_GENERAL_COVERAGE_LIMITS                      
   DELETE FROM APP_GENERAL_COVERAGE_LIMITS                                      
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                      
                       
----Added By Ravindra  Ends Here                      
                             
 ---- 4. APP_GENERAL_HOLDER_INTEREST                                      
                    
   DELETE FROM APP_GENERAL_HOLDER_INTEREST                                      
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                      
 ---- 5.  APP_GENERAL_UNDERWRITING_INFO                                      
                    
   DELETE FROM APP_GENERAL_UNDERWRITING_INFO                                      
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                
 END                                
                              
--<End by Ashwani>                              
                               
                              
                               
                                      
                                     
                                       
 --APP_PKG_LOB_DETAILS                                      
 DELETE  FROM APP_PKG_LOB_DETAILS                                       
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                           
                              
 --APP_LIST                 
        DELETE  FROM APP_LIST                                       
 WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID             
                                      
 -- Added By Mohit.                                      
 DELETE FROM APP_APPLICANT_LIST WHERE                                      
 CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                       
                                       
       
 -- Delete from EFT & Credit Card tables    
    
 DELETE FROM ACT_APP_EFT_CUST_INFO    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                       
    
 DELETE FROM ACT_APP_CREDIT_CARD_DETAILS    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                       
                              
                                       
 IF EXISTS (SELECT ATTACH_ID FROM MNT_ATTACHMENT_LIST                                       
     WHERE ATTACH_CUSTOMER_ID=@CUSTOMER_ID AND ATTACH_APP_ID=@APP_ID AND                                     
           ATTACH_APP_VER_ID=@APP_VERSION_ID)                                      
  BEGIN              
   UPDATE MNT_ATTACHMENT_LIST SET IS_ACTIVE = 'N'                                        
   WHERE ATTACH_CUSTOMER_ID=@CUSTOMER_ID AND ATTACH_APP_ID=@APP_ID AND ATTACH_APP_VER_ID=@APP_VERSION_ID                                         
  END                                      
END                                      
                
                                      
                                       
                                      
                                      
                                      
           
                                       
                                    
                               
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                
              
              
            
            
            
            
            
            
          
        
        
        
        
        
      
    
    
    
  







GO

