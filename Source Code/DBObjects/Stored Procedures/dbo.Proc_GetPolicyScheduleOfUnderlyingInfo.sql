IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyScheduleOfUnderlyingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyScheduleOfUnderlyingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  ----------------------------------------------------------            
Proc Name       : dbo.Proc_GetPolicyScheduleOfUnderlyingInfo        
Created by      : Ravindra      
Date            : 03-22-2006      
Purpose         : To fetch Data from  POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES        
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------    
--drop  PROCEDURE dbo.Proc_GetPolicyScheduleOfUnderlyingInfo       
*/        
      
CREATE PROCEDURE dbo.Proc_GetPolicyScheduleOfUnderlyingInfo        
(            
 @CUSTOMER_ID int  ,        
 @POLICY_ID  int,        
 @POLICY_VERSION_ID int,        
 @POLICY_NO varchar(75)        
         
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
ISNULL(HAS_MOTORIST_PROTECTION,0) AS HAS_MOTORIST_PROTECTION,
ISNULL(HAS_SIGNED_A9,0) AS HAS_SIGNED_A9,
ISNULL(LOWER_LIMITS,0) AS LOWER_LIMITS      
 FROM POL_UMBRELLA_UNDERLYING_POLICIES         
 WHERE  CUSTOMER_ID=@CUSTOMER_ID        
  AND POLICY_ID=@POLICY_ID         
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
  AND POLICY_NUMBER=@POLICY_NO        
        
        
End          
          
        
      
    
  







GO

