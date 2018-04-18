IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckForPolicyStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckForPolicyStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* =======================================================================                         
 Proc Name       : dbo.Proc_CheckForPolicyStatus
 Created by      : Shafi                                      
 Date            : 06 Feb. 2006                                     
 Purpose         : Check for Policy Status
  ========================================================================*/    
--drop PROC Proc_CheckForPolicyStatus
create PROC Proc_CheckForPolicyStatus
(                                            
@CUSTOMER_ID INT,                                            
@POL_ID INT,   
@POL_VERSION_ID INT,   
@convertr  INT OUTPUT    
)    
AS    
BEGIN   
--select policy_status,* from pol_customer_policy_list
DECLARE @POLICY_STATUS VARCHAR(30)
SELECT @POLICY_STATUS=POLICY_STATUS from POL_CUSTOMER_POLICY_LIST                                                  
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POL_ID AND POLICY_VERSION_ID = @POL_VERSION_ID --AND IS_ACTIVE = 'Y'
print @POLICY_STATUS
print 'hello'
IF UPPER(@POLICY_STATUS)= 'NORMAL'
set @convertr= 1    
ELSE    
set @convertr=2  
END    
    
    
    
  



GO

