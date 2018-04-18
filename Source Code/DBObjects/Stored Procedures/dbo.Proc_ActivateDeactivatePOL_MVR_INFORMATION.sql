IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePOL_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePOL_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : Dbo.Proc_ActivateDeactivatePOL_MVR_INFORMATION  
Created by      : Anurag Verma     
Date            : 11/08/2005        
Purpose         : Activate/ Deactivate of data in table POL_MVR_INFORMATION  
Revison History :        
Used In                   : Wolverine        
------------------------------------------------------------        
Date      Review By           Comments        
28th Mar'06 Swastika   Set class on Activation/Deactivation  
------    ------------        -------------------------*/        
CREATE PROC dbo.Proc_ActivateDeactivatePOL_MVR_INFORMATION  
(        
 @POL_MVR_ID INT,  
 @IS_ACTIVE      nchar(1)   ,  
 @CUSTOMER_ID  int,  
 @POL_ID INT,  
 @POL_VERSION_ID INT,  
 @DRIVER_ID INT,  
 @CALLED_FROM varchar(10)=null            
)        
AS        
BEGIN        
        
 UPDATE  POL_MVR_INFORMATION  
 SET          
  IS_ACTIVE   =  @IS_ACTIVE       
 WHERE            
 POL_MVR_ID = @POL_MVR_ID AND  
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND DRIVER_ID=@DRIVER_ID  
  
--Start : Added by Swastika  
/* IF(UPPER(@CALLED_FROM)='PPA')        
 BEGIN  
  EXEC PROC_SETVEHICLECLASSRULEPOL @CUSTOMER_ID,@POL_ID,@POL_VERSION_ID,@DRIVER_ID      
 END  
-- End  
*/
END  
  
  
  
  



GO

