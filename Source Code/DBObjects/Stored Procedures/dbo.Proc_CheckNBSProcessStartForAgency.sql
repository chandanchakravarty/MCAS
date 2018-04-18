IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckNBSProcessStartForAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckNBSProcessStartForAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
         
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
         
/*----------------------------------------------------------                                    
                                    
Proc Name       : Proc_CheckNBSProcessStartForAgency                                    
Created by      : Sumit Chhabra                    
Date            : 14/05/2006                                   
Purpose         : Assignment of underwriter to a customer based on round-robin algorithm                    
Revison History :                                    
Used In                   : Wolverine                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/               
-- drop proc dbo.Proc_CheckNBSProcessStartForAgency                            
CREATE PROC [dbo].[Proc_CheckNBSProcessStartForAgency]                                    
(                                    
 @CUSTOMER_ID  int,                                    
 @APP_ID  int,                                    
 @APP_VERSION_ID smallint    
)                                    
AS                                    
BEGIN    
    
DECLARE @RETVAL INT,@POLICY_ID INT,@POLICY_VERSION_ID INT,@NEW_BUSINESS_PROCESS_LAUNCH_ID INT    
    
SET @NEW_BUSINESS_PROCESS_LAUNCH_ID = 24    
    
SELECT @POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WHERE    
 CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE='Y'    
--Need to check for whether the agency is active or the NBS termination date has not been reached    
EXEC Proc_CheckAgenyTermination @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@NEW_BUSINESS_PROCESS_LAUNCH_ID,@RETVAL output    
    
IF @RETVAL<>1    
 RETURN -1    
if exists    
 (    
  SELECT MNT_AGENCY_STATE_LOB_ASSOC_ID FROM MNT_AGENCY_STATE_LOB_ASSOC MAS JOIN APP_LIST AL    
   ON  MAS.STATE_ID = AL.STATE_ID AND MAS.LOB_ID=AL.APP_LOB AND MAS.AGENCY_ID=AL.APP_AGENCY_ID    
   WHERE AL.CUSTOMER_ID=@CUSTOMER_ID AND AL.APP_ID=@APP_ID AND AL.APP_VERSION_ID=@APP_VERSION_ID)       
  return 1     
else    
 return -1    
     
    
END                    
              
    
    
            
            
            
          
        
      
    
    
    
    
    
    
    
GO

