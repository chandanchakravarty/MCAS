IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*              
----------------------------------------------------------                  
Proc Name        : dbo.Proc_GetPolicyStatus              
Created by       : Anurag                
Date             : 04 novs,2005                  
Purpose : Selects vehcile coverages    
Revison History :                  
Used In         : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------                 
drop proc dbo.Proc_GetPolicyStatus 2126,45,1   
*/              
              
CREATE PROCEDURE [dbo].[Proc_GetPolicyStatus]    
(              
 @CUSTOMER_ID int,              
 @POL_ID int,              
 @POL_VERSION_ID smallint    
)              
              
As              
    
SELECT isnull(POLICY_STATUS,'') as POLICY_STATUS,isnull(APP_STATUS,'') as APP_STATUS,isnull(IS_ACTIVE,'N') AS IS_ACTIVE FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID=@POL_ID    
AND POLICY_VERSION_ID=@POL_VERSION_ID    
    
    
GO

