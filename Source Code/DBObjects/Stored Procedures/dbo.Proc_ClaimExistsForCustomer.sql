IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ClaimExistsForCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ClaimExistsForCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name       : dbo.Proc_ClaimExistsForCustomer      
Created by      : Sumit Chhabra  
Date            : 06/16/2006      
Purpose       : This proc is used to check existence of records for customer and policy  
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC dbo.Proc_ClaimExistsForCustomer      
(      
@CUSTOMER_ID int,  
@POLICY_ID int,  
@POLICY_VERSION_ID int  
    
)      
AS      
BEGIN      
if exists(SELECT CLAIM_ID FROM CLM_CLAIM_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID) --AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
 return 1  
else  
 return -1  
END

GO

