IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteScheduleOfUnderlying]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteScheduleOfUnderlying]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Drop PROCEDURE dbo.Proc_DeleteScheduleOfUnderlying  
CREATE PROCEDURE dbo.Proc_DeleteScheduleOfUnderlying  
(      
 @CUSTOMER_ID int  ,  
 @APP_ID  int,  
 @APP_VERSION_ID int,  
 @POLICY_NO varchar(75)  
   
)          
AS               
  
BEGIN                    
  
DELETE  FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES    
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  
  AND APP_ID=@APP_ID   
  AND APP_VERSION_ID=@APP_VERSION_ID  
  AND POLICY_NUMBER=@POLICY_NO  
  
DELETE  FROM APP_UMBRELLA_UNDERLYING_POLICIES   
 WHERE  CUSTOMER_ID=@CUSTOMER_ID  
  AND APP_ID=@APP_ID   
  AND APP_VERSION_ID=@APP_VERSION_ID  
  AND POLICY_NUMBER=@POLICY_NO  
  
  
End    
    
  
  
      
    
    
  
  


GO

