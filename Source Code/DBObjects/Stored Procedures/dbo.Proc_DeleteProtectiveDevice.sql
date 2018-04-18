IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteProtectiveDevice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteProtectiveDevice]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*------------------------------------------  
Proc name	:	[dbo].[Proc_DeleteProtectiveDevice] 
                
Created By	:	Pradeep Kushwaha    
date		:	19 April 2010    
Purpose		:	To delete POL_PROTECTIVE_DEVICES Data  
--------------------------------------------*/    
--DROP PROC [dbo].[Proc_DeleteProtectiveDevice]
  
CREATE PROC [dbo].[Proc_DeleteProtectiveDevice]    
(     
 
 @CUSTOMER_ID Int,      
 @POLICY_ID Int,      
 @POLICY_VERSION_ID smallint  ,
 @RISK_ID int  ,
 @PROTECTIVE_DEVICE_ID  int     
)                
AS                
BEGIN                
    
  DELETE FROM POL_PROTECTIVE_DEVICES     
  WHERE 
  PROTECTIVE_DEVICE_ID=@PROTECTIVE_DEVICE_ID AND      
  CUSTOMER_ID =  @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID=@POLICY_VERSION_ID  and
  RISK_ID=@RISK_ID
  
   
END         
    
GO

