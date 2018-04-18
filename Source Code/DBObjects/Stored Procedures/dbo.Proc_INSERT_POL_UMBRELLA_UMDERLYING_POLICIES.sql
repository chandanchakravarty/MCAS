IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_POL_UMBRELLA_UMDERLYING_POLICIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_POL_UMBRELLA_UMDERLYING_POLICIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  ----------------------------------------------------------                    
Proc Name       : dbo.Proc_INSERT_POL_UMBRELLA_UMDERLYING_POLICIES                
Created by      : Ravindra              
Date            : 03-22-2006              
Purpose         : To Insert Data in  POL_UMBRELLA_UNDERLYING_POLICIES              
Revison History :                    
Used In         : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------                   
*/               
--drop PROC dbo.Proc_INSERT_POL_UMBRELLA_UMDERLYING_POLICIES                  
CREATE PROC dbo.Proc_INSERT_POL_UMBRELLA_UMDERLYING_POLICIES                                      
(                                      
 @CUSTOMER_ID     int,                                      
 @POLICY_ID     int,                                      
 @POLICY_VERSION_ID     smallint,                                      
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
 @EXCLUDE_UNINSURED_MOTORIST int ,  
 @HAS_MOTORIST_PROTECTION int,  
 @HAS_SIGNED_A9 int,
 @LOWER_LIMITS int     
)                                      
AS                                      
BEGIN                       
                
--IF EXISTS(SELECT POLICY_NUMBER FROM   POL_UMBRELLA_UNDERLYING_POLICIES                 
--  WHERE  CUSTOMER_ID=@CUSTOMER_ID                
--   AND POLICY_ID=@POLICY_ID                 
 --   AND POLICY_VERSION_ID=@POLICY_VERSION_ID                
 --  AND POLICY_NUMBER=@POLICY_NUMBER)                
--RETURN -2                
                
INSERT INTO POL_UMBRELLA_UNDERLYING_POLICIES                                      
(                                      
 CUSTOMER_ID,                                      
 POLICY_ID,                                      
 POLICY_VERSION_ID,                                      
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
 EXCLUDE_UNINSURED_MOTORIST,  
 HAS_MOTORIST_PROTECTION,  
 HAS_SIGNED_A9,
 LOWER_LIMITS  
                
)                                      
VALUES                                      
(                                      
 @CUSTOMER_ID ,                                      
 @POLICY_ID ,                                      
 @POLICY_VERSION_ID ,                                      
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
 @HAS_MOTORIST_PROTECTION,  
 @HAS_SIGNED_A9,
 @LOWER_LIMITS  
        
              
)                                     
END             
          
        



GO

