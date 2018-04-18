IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_UMBRELLA_UNDERLYING_POLICIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_UMBRELLA_UNDERLYING_POLICIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Drop PROC dbo.Proc_INSERT_UMBRELLA_UNDERLYING_POLICIES           
CREATE PROC dbo.Proc_INSERT_UMBRELLA_UNDERLYING_POLICIES                                                
(                                                
 @CUSTOMER_ID     int,                                                
 @APP_ID     int,                                                
 @APP_VERSION_ID     smallint,        
 @UNDERLYING_POL_ID  VARCHAR(50) output,                                                  
 @POLICY_NUMBER     varchar(75),                                                
 @POLICY_LOB varchar(25),                          
 @POLICY_COMPANY varchar(150) ,                          
 @POLICY_TERMS varchar(5),                          
 @POLICY_START_DATE datetime ,                          
 @POLICY_EXPIRATION_DATE datetime,                          
 @POLICY_PREMIUM decimal(18, 0) ,                          
 @QUESTION char(1),                          
 @QUES_DESC varchar(125),                        
 @IS_POLICY bit,                    
 @STATE_ID smallint,            
 @EXCLUDE_UNINSURED_MOTORIST int,    
 @HAS_MOTORIST_PROTECTION int = null,    
 @HAS_SIGNED_A9 int = null,
 @LOWER_LIMITS int = null       
              
)                                                
AS                                                
BEGIN                                 
          
DECLARE @TEMP_UNDERLYING_POL_ID             VARCHAR(50)                
                  
IF EXISTS(SELECT POLICY_NUMBER FROM   APP_UMBRELLA_UNDERLYING_POLICIES                           
  WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND APP_ID=@APP_ID  AND APP_VERSION_ID=@APP_VERSION_ID AND POLICY_NUMBER=@POLICY_NUMBER AND POLICY_COMPANY =@POLICY_COMPANY)                      
 -- will not insert the same policy number twice          
 RETURN -2                          
ELSE          
 begin          
  -- fetch the next underlying_pol_id that is to be inserted          
  SELECT @TEMP_UNDERLYING_POL_ID=ISNULL(MAX(ISNULL(UNDERLYING_POL_ID,0)+1),0)          
  FROM APP_UMBRELLA_UNDERLYING_POLICIES             
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND  APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID            
          
  -- insert the record          
 INSERT INTO APP_UMBRELLA_UNDERLYING_POLICIES                                                
(                                                
 CUSTOMER_ID,                                                
 APP_ID,                                                
 APP_VERSION_ID,                                                
 POLICY_NUMBER,                          
 POLICY_LOB ,                          
 POLICY_COMPANY,                          
 POLICY_TERMS ,                          
 POLICY_START_DATE,                          
 POLICY_EXPIRATION_DATE,                          
 POLICY_PREMIUM ,                          
 QUESTION ,                          
 QUES_DESC,                        
 IS_POLICY,                    
 STATE_ID,            
 EXCLUDE_UNINSURED_MOTORIST ,            
 UNDERLYING_POL_ID,    
 HAS_MOTORIST_PROTECTION,    
 HAS_SIGNED_A9,
 LOWER_LIMITS               
)                                                
VALUES                                                
(                                                
 @CUSTOMER_ID ,                                                
 @APP_ID ,                                                
 @APP_VERSION_ID ,                                                
 @POLICY_NUMBER  ,                                                
 @POLICY_LOB ,                          
 @POLICY_COMPANY ,                          
 @POLICY_TERMS ,                          
 @POLICY_START_DATE ,                          
 @POLICY_EXPIRATION_DATE ,                          
 @POLICY_PREMIUM ,                          
 @QUESTION ,                          
 @QUES_DESC,                        
 @IS_POLICY,                    
 @STATE_ID,            
 @EXCLUDE_UNINSURED_MOTORIST,            
 @TEMP_UNDERLYING_POL_ID,    
 @HAS_MOTORIST_PROTECTION,    
 @HAS_SIGNED_A9,
 @LOWER_LIMITS              
)                  
RETURN @TEMP_UNDERLYING_POL_ID                  
      END                    
                                                
END                                            
                  
  
  



GO

