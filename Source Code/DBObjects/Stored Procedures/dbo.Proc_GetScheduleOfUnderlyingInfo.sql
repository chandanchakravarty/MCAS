IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetScheduleOfUnderlyingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetScheduleOfUnderlyingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop PROCEDURE dbo.Proc_GetScheduleOfUnderlyingInfo         
CREATE PROCEDURE dbo.Proc_GetScheduleOfUnderlyingInfo              
(                  
 @CUSTOMER_ID int  ,              
 @APP_ID  int,              
 @APP_VERSION_ID int,              
 @POLICY_NO varchar(75),  
 @POLICY_COMPANY varchar(150) = null                  
)                      
AS                
BEGIN               
 SELECT               
 POLICY_NUMBER,              
 POLICY_LOB,              
 POLICY_COMPANY,              
 CONVERT(VARCHAR(10),POLICY_START_DATE,101) AS POLICY_START_DATE ,              
 CONVERT(VARCHAR(10),POLICY_EXPIRATION_DATE,101) AS POLICY_EXPIRATION_DATE,              
 POLICY_PREMIUM,              
 QUESTION,              
 QUES_DESC,        
 EXCLUDE_UNINSURED_MOTORIST,    
 HAS_MOTORIST_PROTECTION,
 LOWER_LIMITS,    
 HAS_SIGNED_A9    
        
 FROM APP_UMBRELLA_UNDERLYING_POLICIES               
 WHERE  CUSTOMER_ID=@CUSTOMER_ID              
   AND APP_ID=@APP_ID               
   AND APP_VERSION_ID=@APP_VERSION_ID              
   AND POLICY_NUMBER=@POLICY_NO  
and POLICY_COMPANY = @POLICY_COMPANY               
               
End                
            



GO

