IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyScheduleOfUnderlying]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyScheduleOfUnderlying]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  ----------------------------------------------------------      
Proc Name       : dbo.Proc_DeletePolicyScheduleOfUnderlying  
Created by      : Ravindra
Date            : 03-22-2006
Purpose         : To delete record from  POL_UMBRELLA_UNDERLYING_POLICIES
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------     
*/ 
--drop proc Proc_DeletePolicyScheduleOfUnderlying
CREATE PROCEDURE dbo.Proc_DeletePolicyScheduleOfUnderlying  
(      
 @CUSTOMER_ID int  ,  
 @POLICY_ID  int,  
 @POLICY_VERSION_ID int,  
 @POLICY_NO varchar(75)  
   
)          
AS               
  
BEGIN                    
  
DELETE  FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES    
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  
  AND POLICY_ID=@POLICY_ID   
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
  AND POLICY_NUMBER=@POLICY_NO  
  
DELETE  FROM POL_UMBRELLA_UNDERLYING_POLICIES   
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  
  AND POLICY_ID=@POLICY_ID   
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
  AND POLICY_NUMBER=@POLICY_NO  
  
  
End    
    
  



GO

