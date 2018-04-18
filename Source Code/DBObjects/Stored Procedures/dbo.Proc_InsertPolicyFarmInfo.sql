IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyFarmInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyFarmInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------      
Proc Name       : dbo.Proc_InsertUmbrellaFarmInfo      
Created by      : Ravindra     
Date            : 03-20-2006  
Purpose         : To add record in POL_UMBRELLA_FARM_INFO table  
Revison History :      
Used In         :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC dbo.Proc_InsertPolicyFarmInfo      
(      
  @CUSTOMER_ID              int,      
  @POLICY_ID                int,      
  @POLICY_VERSION_ID        smallint ,    
  @FARM_ID                  smallint OUTPUT,      
  @ADDRESS_1                nvarchar(75),      
  @ADDRESS_2                nvarchar(75),      
  @CITY                     nvarchar(75) ,      
  @COUNTY                   nvarchar(75),      
  @STATE                    int,      
  @ZIPCODE                  nvarchar(10),      
  @PHONE_NUMBER             nvarchar(15),      
  @FAX_NUMBER               nvarchar(15) ,    
  @NO_OF_ACRES       	    int,  
  @OCCUPIED_BY_APPLICANT    char (1),  
  @RENTED_TO_OTHER          char (1) ,  
  @EMP_FULL_PART            char (1),        
  @CREATED_BY               int,      
  @CREATED_DATETIME         datetime   ,  
  @LOCATION_NUMBER          int  
)      
AS      
     
BEGIN    
      
 If Exists(SELECT FARM_ID FROM POL_UMBRELLA_FARM_INFO   
  WHERE LOCATION_NUMBER = @LOCATION_NUMBER AND    
  CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID )     
 
	RETURN -2
    
 SELECT @FARM_ID = ISNULL(Max(FARM_ID),0)+1      
   FROM POL_UMBRELLA_FARM_INFO   
     
      
 INSERT INTO POL_UMBRELLA_FARM_INFO   
  (      
   CUSTOMER_ID,         
   POLICY_ID,                     
   POLICY_VERSION_ID,               
   FARM_ID,            
   ADDRESS_1,                 
   ADDRESS_2,    
   CITY,                    
   COUNTY,                     
   STATE,                    
   ZIPCODE,                    
   PHONE_NUMBER,               
   FAX_NUMBER,     
   NO_OF_ACRES,  
   OCCUPIED_BY_APPLICANT,  
   RENTED_TO_OTHER,  
   EMP_FULL_PART,  
   IS_ACTIVE,    
   CREATED_BY,               
   CREATED_DATETIME ,  
   LOCATION_NUMBER          
 )         
  values      
 (      
     
   @CUSTOMER_ID ,         
   @POLICY_ID ,                     
   @POLICY_VERSION_ID ,               
   @FARM_ID ,            
   @ADDRESS_1,                 
   @ADDRESS_2 ,    
   @CITY,                   
   @COUNTY  ,                     
   @STATE  ,                    
   @ZIPCODE  ,                    
   @PHONE_NUMBER ,               
   @FAX_NUMBER,     
   @NO_OF_ACRES,  
   @OCCUPIED_BY_APPLICANT,  
   @RENTED_TO_OTHER,  
   @EMP_FULL_PART,  
   'Y',    
   @CREATED_BY   ,               
   @CREATED_DATETIME  ,  
   @LOCATION_NUMBER  
    )      
                    
END    
    

    
    
    
    
  



GO

