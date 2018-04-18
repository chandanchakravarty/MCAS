IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUmbrellaFarmInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUmbrellaFarmInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC Proc_UpdateUmbrellaFarmInfo   
(    
 @CUSTOMER_ID              int,    
 @APP_ID                   int,    
 @APP_VERSION_ID           smallint,    
 @FARM_ID               smallint,     
 @LOCATION_NUMBER          int,   
 @ADDRESS_1                nvarchar(75),    
 @ADDRESS_2                nvarchar(75),    
 @CITY                     nvarchar(75) ,    
 @COUNTY                   nvarchar(75),    
 @STATE                    int,    
 @ZIPCODE                  nvarchar(10),    
 @PHONE_NUMBER             nvarchar(15),    
 @FAX_NUMBER               nvarchar(15) ,    
 @NO_OF_ACRES      int,  
 @OCCUPIED_BY_APPLICANT    char (1),  
 @RENTED_TO_OTHER      char (1) ,  
 @EMP_FULL_PART      char (1),        
 @MODIFIED_BY              int,    
 @LAST_UPDATED_DATETIME    datetime   
      
)    
AS    
    
BEGIN    
  
 UPDATE APP_UMBRELLA_FARM_INFO  
  
 SET  
 CUSTOMER_ID=@CUSTOMER_ID ,       
 APP_ID=@APP_ID ,                   
 APP_VERSION_ID=@APP_VERSION_ID ,             
 LOCATION_NUMBER=@LOCATION_NUMBER,         
 ADDRESS_1=@ADDRESS_1,               
 ADDRESS_2=@ADDRESS_2 ,            
 CITY = @CITY,      
 COUNTY=@COUNTY  ,                   
 STATE = @STATE,                  
 ZIPCODE =@ZIPCODE,                  
 PHONE_NUMBER =@PHONE_NUMBER,             
 FAX_NUMBER=@FAX_NUMBER,    
 NO_OF_ACRES=@NO_OF_ACRES,  
 OCCUPIED_BY_APPLICANT=@OCCUPIED_BY_APPLICANT,  
 RENTED_TO_OTHER=@RENTED_TO_OTHER,  
 EMP_FULL_PART=@EMP_FULL_PART ,  
 MODIFIED_BY=@MODIFIED_BY   ,             
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME   
        
 WHERE   
  
 CUSTOMER_ID=@CUSTOMER_ID AND       
 APP_ID=@APP_ID AND                   
 APP_VERSION_ID=@APP_VERSION_ID AND  
 FARM_ID =@FARM_ID       
    
END  
  



GO

