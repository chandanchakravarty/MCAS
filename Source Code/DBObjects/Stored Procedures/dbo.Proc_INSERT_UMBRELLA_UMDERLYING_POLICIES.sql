IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_UMBRELLA_UMDERLYING_POLICIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_UMBRELLA_UMDERLYING_POLICIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_INSERT_UMBRELLA_UMDERLYING_POLICIES
CREATE PROC dbo.Proc_INSERT_UMBRELLA_UMDERLYING_POLICIES                            
(                            
 @CUSTOMER_ID     int,                            
 @APP_ID     int,                            
 @APP_VERSION_ID     smallint,                            
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
 @STATE_ID smallint    
      
)                            
AS                            
BEGIN             
      
IF EXISTS(SELECT POLICY_NUMBER FROM   APP_UMBRELLA_UNDERLYING_POLICIES       
  WHERE  CUSTOMER_ID=@CUSTOMER_ID      
   AND APP_ID=@APP_ID       
    AND APP_VERSION_ID=@APP_VERSION_ID      
   AND POLICY_NUMBER=@POLICY_NUMBER)  
RETURN -2      
      
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
 STATE_ID    
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
 @STATE_ID 
    
)      
      
                            
END                        
                        
      
    
  



GO

