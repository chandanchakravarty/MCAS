IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolicyDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolicyDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------            
Proc Name       : dbo.Proc_ActivateDeactivatePolicyDriver      
Created by      : Vijay Arora            
Date            : 07-11-2005      
Purpose         : To Activate/Deactivate the record of Policy driver        
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments           
drop PROC dbo.Proc_ActivateDeactivatePolicyDriver     
------   ------------       -------------------------*/            
CREATE PROC dbo.Proc_ActivateDeactivatePolicyDriver        
(            
 @CUSTOMER_ID int,        
 @POLICY_ID  int,        
 @POLICY_VERSION_ID int,        
 @DRIVER_ID int,        
 @IS_ACTIVE   Char(1)            
)            
AS         
DECLARE @CUSTOMER_DETAILS VARCHAR(20)--Done for Itrack Issue 5457 on 14 April 2009    
BEGIN            
 UPDATE POL_DRIVER_DETAILS        
 SET             
    IS_ACTIVE  = @IS_ACTIVE           
 WHERE            
  CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
  DRIVER_ID = @DRIVER_ID        
    
--Done for Itrack Issue 5457 on 14 April 2009    
SET @CUSTOMER_DETAILS = CONVERT( VARCHAR(10),@CUSTOMER_ID) + '^' + CONVERT(VARCHAR(10),@POLICY_ID)+ '^' +       
CONVERT(VARCHAR(10),@POLICY_VERSION_ID) + '^' + CONVERT(VARCHAR(10),@DRIVER_ID) + '^POL'    
    
IF EXISTS(SELECT * FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND DRIVER_ID = @DRIVER_ID)      
    BEGIN    
   UPDATE APP_PRIOR_LOSS_INFO SET DRIVER_NAME='',DRIVER_ID=''     
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND DRIVER_ID = @DRIVER_ID  
 END    
--Added till here    
END        
        
        
        
        
        
        
GO

