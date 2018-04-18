IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateProtectiveDevice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateProtectiveDevice]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------            
Proc Name       : dbo.POL_PROTECTIVE_DEVICES            
Created by      : Pradeep kushwaha   
Date            : 19/04/2010            
Purpose			:To Activate and deactivate records in POL_PROTECTIVE_DEVICES table.            
Revison History :            
Used In			: Ebix Advantage            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_ActivateDeactivateProtectiveDevice     
  
  
CREATE  PROC [dbo].[Proc_ActivateDeactivateProtectiveDevice]  
(          
 @CUSTOMER_ID Int,      
 @POLICY_ID Int,      
 @POLICY_VERSION_ID smallint,    
 @RISK_ID int,       
 @PROTECTIVE_DEVICE_ID  int,          
 @IS_ACTIVE   NChar(1)          
)          
AS          
BEGIN  
UPDATE POL_PROTECTIVE_DEVICES      
 SET           
    Is_Active  = @IS_ACTIVE         
 WHERE          
	 PROTECTIVE_DEVICE_ID    = @PROTECTIVE_DEVICE_ID AND      
	 CUSTOMER_ID =  @CUSTOMER_ID AND      
	 POLICY_ID = @POLICY_ID AND      
	 POLICY_VERSION_ID=@POLICY_VERSION_ID AND
	 RISK_ID=@RISK_ID       
      
   
END      
      
GO

