IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolicyAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolicyAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name    : dbo.Proc_ActivateDeactivatePolicyAdditionalInterest          
Created by   : Vijay Arora     
Date         : 10-11-2005  
Purpose      : Activate and Deactivate the Policy Addtional Interest Record.  
Revison History :          
Used In  :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
drop     PROC DBO.Proc_ActivateDeactivatePolicyAdditionalInterest                         
------   ------------       -------------------------*/             
CREATE     PROC [dbo].[Proc_ActivateDeactivatePolicyAdditionalInterest]          
(          
 @CUSTOMER_ID       int,          
 @POLICY_ID   int,          
 @POLICY_VERSION_ID  smallint,          
 @HOLDER_ID   INT,          
 @VEHICLE_ID   smallint,        
 @ADD_INT_ID INT,    
 @IS_ACTIVE nchar(2)   
)          
AS          
        
          
BEGIN      
	UPDATE   
	POL_ADD_OTHER_INT   
	SET   
	IS_ACTIVE=@IS_ACTIVE   
	WHERE   
	CUSTOMER_ID=@CUSTOMER_ID   
	AND POLICY_ID=@POLICY_ID   
	AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
	AND ADD_INT_ID=@ADD_INT_ID   
	AND VEHICLE_ID=@VEHICLE_ID        
	END


GO

