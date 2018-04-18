IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyScheduleOfUnderlyingCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyScheduleOfUnderlyingCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  ----------------------------------------------------------        
Proc Name       : dbo.Proc_GetPolicyScheduleOfUnderlyingCoverages    
Created by      : Ravindra  
Date            : 03-22-2006  
Purpose         : To fetch Data from  POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES    
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------       
*/    
 -- drop PROCEDURE dbo.Proc_GetPolicyScheduleOfUnderlyingCoverages   
CREATE PROCEDURE dbo.Proc_GetPolicyScheduleOfUnderlyingCoverages    
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
COVERAGE_DESC AS COV_DES ,    
COVERAGE_AMOUNT AS COV_AMOUNT,    
POLICY_TEXT,
COVERAGE_TYPE
    
    
    
 FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES     
WHERE  CUSTOMER_ID=@CUSTOMER_ID    
  AND POLICY_ID=@POLICY_ID     
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
  AND POLICY_NUMBER=@POLICY_NO    
    
    
End      
      
    
  



GO

