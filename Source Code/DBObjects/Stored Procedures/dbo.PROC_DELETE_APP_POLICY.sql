IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETE_APP_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETE_APP_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------                                          
PROC NAME       : DBO.PROC_DELETE_APP_POLICY  2043,211,1                                        
CREATED BY      : CHARLES GOMES                                          
DATE            : 6/APRIL/2010                                          
PURPOSE     : DELETES APPLICATION , POLICY PAGE IMPLEMENTATION                       
------------------------------------------------------------                                         
DATE     REVIEW BY          COMMENTS                                          
------   ------------       -------------------------*/                                        
--BEGIN TRAN                              
--DROP PROC PROC_DELETE_APP_POLICY -- 28325,19,1                                            
--GO

CREATE  PROC [dbo].[PROC_DELETE_APP_POLICY]                                          
(                                          
@CUSTOMER_ID INT,                                          
@APP_ID  INT,                                          
@APP_VERSION_ID SMALLINT                                                                                                               
)                                          
AS                                          
BEGIN                                          
   
DECLARE @POLICY_ID INT, 
@POLICY_VERSION_ID INT,
@RISKID INT  


--Added by Charles on 1-Jun-2010
DECLARE @TABLE_NAME NVARCHAR(128),
@TEMP_ERROR_CODE INT,
@QUERY NVARCHAR(MAX)
   
SELECT @POLICY_ID = POLICY_ID, @POLICY_VERSION_ID = POLICY_VERSION_ID, @RISKID = CAST(POLICY_LOB AS INT) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID 
AND  ISNULL(POLICY_STATUS,APP_STATUS) = 'APPLICATION' 
                                                                          
--SET @RISKID = (SELECT APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID)                                          
 
 --Added by Shikha
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
      
 --AVIATION              
 IF (@RISKID = 8)  
 BEGIN  
 DELETE FROM POL_AVIATION_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID       
 END  
                              
 -- PRIVATE PASSENGER OR MOTORCYCLE                                          
 IF  (@RISKID = 2 OR @RISKID =3)                                          
 BEGIN                                                                               
                                       
  --POL_AUTO_GEN_INFO                                          
  DELETE  FROM POL_AUTO_GEN_INFO                                           
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                          
      
--POL_DRIVER_ASSIGNED_VEHICLE                                          
  DELETE  FROM POL_DRIVER_ASSIGNED_VEHICLE                                            
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                                           
                             
  --POL_ADD_OTHER_INT                                       
  DELETE FROM  POL_ADD_OTHER_INT                                           
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
                      
  --APP_AUTO_ID_CARD_INFO                                           
  --DELETE  FROM APP_AUTO_ID_CARD_INFO                                           
  --WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                          
              
 --POL_MISCELLANEOUS_EQUIPMENT_VALUES                                  
  DELETE  FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES                                         
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                               
                                                                     
  -- POL_MVR_INFORMATION                                     
   DELETE  FROM POL_MVR_INFORMATION                                          
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                                                                                      
                                                                               
 END                                           
                                          
-- HOMEOWNERS OR RENTAL DWELLING                               
 IF(@RISKID = 1 OR @RISKID = 6)                                          
 BEGIN                                          
  --POL_HOME_OWNER_ADD_INT                                          
DELETE FROM POL_HOME_OWNER_ADD_INT                                           
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                     
  
  --POL_HOME_OWNER_GEN_INFO                                          
  DELETE FROM POL_HOME_OWNER_GEN_INFO                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                         
                                            
  --POL_HOME_OWNER_CHIMNEY_STOVE                                            
  DELETE  FROM POL_HOME_OWNER_CHIMNEY_STOVE                                          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                           
                                            
  --POL_HOME_OWNER_FIRE_PROT_CLEAN                                          
  DELETE FROM POL_HOME_OWNER_FIRE_PROT_CLEAN                                          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                    
                                                                                                                     
  --POL_HOME_OWNER_PER_ART_GEN_INFO                                          
  DELETE FROM POL_HOME_OWNER_PER_ART_GEN_INFO                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
                                          
  -- POL_HOMEOWNER_REC_VEH_ADD_INT              
 DELETE  FROM  POL_HOMEOWNER_REC_VEH_ADD_INT                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID              
                           
  --POL_HOME_OWNER_RECREATIONAL_VEHICLES                 
  DELETE  FROM  POL_HOME_OWNER_RECREATIONAL_VEHICLES                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND   POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                         
                                            
 --POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES                                          
  DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                    
            
  --POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                   
 DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS             
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POL_ID=@POLICY_ID AND POL_VERSION_ID=@POLICY_VERSION_ID                                          
                                                                        
  --POL_HOME_OWNER_SCH_ITEMS_CVGS                    
  DELETE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                            
                                                                          
  --POL_HOME_OWNER_SOLID_FUEL                                          
  DELETE  FROM POL_HOME_OWNER_SOLID_FUEL                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                             
                                            
  --APP_HOME_OWNER_SUB_INSU                                          
  --DELETE FROM APP_HOME_OWNER_SUB_INSU                                          
  --WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                          
                                            
  --APP_HOME_OWNER_UNIT_CLEARANCE                                          
  --DELETE  FROM APP_HOME_OWNER_UNIT_CLEARANCE                          
  --WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                          
                                            
  --POL_HOME_RATING_INFO                                          
  DELETE FROM POL_HOME_RATING_INFO                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                         
                                       
  --APP_SUB_LOCATIONS                                          
  --DELETE FROM APP_SUB_LOCATIONS                              
  --WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                 
            
--  POL_OTHER_STRUCTURE_DWELLING          
 DELETE FROM POL_OTHER_STRUCTURE_DWELLING            
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                
                                      
  DELETE FROM POL_DWELLING_ENDORSEMENTS                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
                                      
 --POL_DWELLING_SECTION_COVERAGES                                          
  DELETE FROM POL_DWELLING_SECTION_COVERAGES                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                       
                          
                                      
  --POL_DWELLING_COVERAGE                                          
  DELETE FROM POL_DWELLING_COVERAGE                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                
              
 --POL_OTHER_LOCATIONS           
  DELETE FROM POL_OTHER_LOCATIONS                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                    
                                       
  --POL_DWELLINGS_INFO                                          
  DELETE FROM POL_DWELLINGS_INFO                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                         
  
                                          
 END                      
 ---END OF HOMEOWNERS, DWELLING                                          
                                        
 -- WATERCRAFT OR RENTAL DWELLING OR HOMEOWNERS                                          
                                    
 IF (@RISKID = 4 OR @RISKID = 6 OR @RISKID = 1)                
 BEGIN                                           
 -- POL_WATERCRAFT_GEN_INFO                                           
  DELETE FROM POL_WATERCRAFT_GEN_INFO                                          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
                                            
  --POL_WATERCRAFT_COV_ADD_INT                                          
  DELETE FROM POL_WATERCRAFT_COV_ADD_INT                                          
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                     
                                          
  --POL_WATERCRAFT_TRAILER_ADD_INT                                          
  DELETE FROM POL_WATERCRAFT_TRAILER_ADD_INT                                          
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
                                          
  --POL_WATERCRAFT_ENDORSEMENTS                                        
  DELETE  FROM POL_WATERCRAFT_ENDORSEMENTS                                        
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                           
                                        
  --POL_WATERCRAFT_COVERAGE_INFO                                          
  DELETE  FROM POL_WATERCRAFT_COVERAGE_INFO                  
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
                                          
  --POL_WATERCRAFT_ENGINE_INFO                                          
  DELETE  FROM POL_WATERCRAFT_ENGINE_INFO                                          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
                                          
  --POL_WATERCRAFT_TRAILER_INFO                                          
  DELETE FROM POL_WATERCRAFT_TRAILER_INFO                                   
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                            
                                          
  --POL_WATERCRAFT_EQUIP_DETAILLS                                          
  DELETE FROM POL_WATERCRAFT_EQUIP_DETAILLS                      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                           
                                
  --DELETE FROM APP_WATER_MVR_INFORMATION                                          
  --WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                           
                                    
  --POL_WATERCRAFT_DRIVER_DETAILS                                          
  DELETE FROM POL_WATERCRAFT_DRIVER_DETAILS                                         
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                         
                                          
  --POL_WATERCRAFT_INFO                                          
  DELETE FROM POL_WATERCRAFT_INFO                                          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
       
--POL_OPERATOR_ASSIGNED_BOAT                                          
  DELETE  FROM POL_OPERATOR_ASSIGNED_BOAT                                            
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
                                                                              
 END    
 
 --ADDED BY SHIKHA
 
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_CLAUSES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
 BEGIN
 DELETE FROM POL_CLAUSES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                               
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM    
 END 
 
 IF EXISTS(SELECT CUSTOMER_ID FROM QOT_CUSTOMER_QUOTE_LIST_POL WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
 BEGIN
 DELETE FROM QOT_CUSTOMER_QUOTE_LIST_POL  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                               
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM
END 


   
 IF EXISTS(SELECT CUSTOMER_ID FROM ACT_PREMIUM_PROCESS_SUB_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
 BEGIN  
 DELETE FROM ACT_PREMIUM_PROCESS_SUB_DETAILS WHERE                                          
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
 END  
   
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_ADD_OTHER_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
 BEGIN  
 DELETE FROM POL_ADD_OTHER_INT WHERE                                          
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
 END  
   
   --POL_DRIVER_DETAILS   
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
  BEGIN                                         
  DELETE FROM POL_DRIVER_DETAILS                                          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
  END  
                                       
--POL_LOCATIONS     
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_LOCATIONS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
  BEGIN                  
  DELETE FROM POL_LOCATIONS                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
  END  
    
   IF EXISTS(SELECT CUSTOMER_ID FROM POL_PERILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
  BEGIN                  
  DELETE FROM POL_PERILS                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
  END  
    
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_PRODUCT_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
  BEGIN                  
  DELETE FROM POL_PRODUCT_COVERAGES                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
  END  
    
    --POL_VEHICLE_ENDORSEMENTS                                        
  DELETE  FROM POL_VEHICLE_ENDORSEMENTS                                     
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
    
    
    --POL_VEHICLE_COVERAGES                                          
  DELETE  FROM POL_VEHICLE_COVERAGES                                            
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
    
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_REINSURANCE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
  BEGIN                  
  DELETE FROM POL_REINSURANCE_INFO                                          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
  END  
    
    --POL_VEHICLES                                          
  DELETE  FROM POL_VEHICLES                                          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
     
   
  DELETE FROM POL_APPLICANT_LIST WHERE                                          
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
   
  DELETE FROM POL_REMUNERATION WHERE                                          
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
 
   DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE                                          
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
 
 IF EXISTS(SELECT CUSTOMER_ID FROM ACT_POLICY_INSTALL_PLAN_DATA WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
 BEGIN
 DELETE FROM ACT_POLICY_INSTALL_PLAN_DATA  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                               
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM
  END
 
 IF EXISTS(SELECT * FROM POL_POLICY_ENDORSEMENTS WITH(NOLOCK) WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID
						AND POLICY_VERSION_ID = @POLICY_VERSION_ID)
		BEGIN
			DELETE FROM POL_POLICY_ENDORSEMENTS WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID
						AND POLICY_VERSION_ID = @POLICY_VERSION_ID
		
		END	
		
 
   DELETE FROM POL_CO_INSURANCE WHERE                                          
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID 
   
  DELETE FROM POL_PRODUCT_LOCATION_INFO 
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
                                                                          
 --DELETE FROM APP_APPLICANT_LIST WHERE                                          
 --CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                    
           
 -- DELETE FROM EFT & CREDIT CARD TABLES        
        
 IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ACT_APP_EFT_CUST_INFO')
 BEGIN         
 DELETE FROM ACT_APP_EFT_CUST_INFO        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID 
 END       
 
 DELETE FROM ACT_POL_EFT_CUST_INFO        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
        
 IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ACT_APP_CREDIT_CARD_DETAILS')
 BEGIN        
 DELETE FROM ACT_APP_CREDIT_CARD_DETAILS        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
 END 
 
 DELETE FROM ACT_POL_CREDIT_CARD_DETAILS        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                              
                                           
 IF EXISTS (SELECT ATTACH_ID FROM MNT_ATTACHMENT_LIST WITH(NOLOCK)                                          
 WHERE ATTACH_CUSTOMER_ID=@CUSTOMER_ID AND ATTACH_APP_ID=@APP_ID AND ATTACH_APP_VER_ID=@APP_VERSION_ID)                                          
  BEGIN                  
   UPDATE MNT_ATTACHMENT_LIST SET IS_ACTIVE = 'N'                                            
   WHERE ATTACH_CUSTOMER_ID=@CUSTOMER_ID AND ATTACH_APP_ID=@APP_ID AND ATTACH_APP_VER_ID=@APP_VERSION_ID                                             
  END      
  

--Added by Charles on 1-Jun-2010
SET @QUERY = ''

DECLARE DEL_TABLE_CURSOR CURSOR FOR    
SELECT DISTINCT
OBJECT_NAME(F.PARENT_OBJECT_ID) AS TABLENAME
FROM SYS.FOREIGN_KEYS AS F
INNER JOIN SYS.FOREIGN_KEY_COLUMNS AS FC
ON F.OBJECT_ID = FC.CONSTRAINT_OBJECT_ID
WHERE OBJECT_NAME (F.REFERENCED_OBJECT_ID) = 'POL_CUSTOMER_POLICY_LIST'
ORDER BY OBJECT_NAME(F.PARENT_OBJECT_ID)

OPEN DEL_TABLE_CURSOR

 FETCH NEXT FROM DEL_TABLE_CURSOR INTO @TABLE_NAME     
 WHILE @@FETCH_STATUS = 0    
 BEGIN 
 
 SET @QUERY = ' IF EXISTS(SELECT CUSTOMER_ID FROM ' + @TABLE_NAME + ' WHERE CUSTOMER_ID= ' + CAST(@CUSTOMER_ID AS VARCHAR)+ ' AND POLICY_ID= ' + CAST(@POLICY_ID AS VARCHAR)+ '
  AND POLICY_VERSION_ID= ' + CAST(@POLICY_VERSION_ID AS VARCHAR) + ')
 BEGIN	DELETE FROM ' + @TABLE_NAME + ' WHERE CUSTOMER_ID= ' + CAST(@CUSTOMER_ID AS VARCHAR) + ' AND POLICY_ID= ' + CAST(@POLICY_ID AS VARCHAR)
	 + ' AND POLICY_VERSION_ID= ' + CAST(@POLICY_VERSION_ID AS VARCHAR) + ' END '
	
 BEGIN TRY	
	EXEC(@QUERY)	
 END TRY
 BEGIN CATCH
	
 END CATCH
	
 FETCH NEXT FROM DEL_TABLE_CURSOR INTO @TABLE_NAME     
 END    
     
 CLOSE DEL_TABLE_CURSOR    
 DEALLOCATE DEL_TABLE_CURSOR    
 --Added till here 
  
  --POL_CUSTOMER_POLICY_LIST  
DELETE FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  

 --APP_LIST         
 --IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'APP_LIST')
 --BEGIN           
	-- DELETE  FROM APP_LIST                                           
	-- WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID         
 --END
 
 RETURN 1                                                                   
  PROBLEM:
  RAISERROR ('ERROR IN EXECUTION',10,1)
                                   
END                                          
 
 --GO
 --EXEC PROC_DELETE_APP_POLICY 28325,19,1
 --ROLLBACK TRAN                   
GO

