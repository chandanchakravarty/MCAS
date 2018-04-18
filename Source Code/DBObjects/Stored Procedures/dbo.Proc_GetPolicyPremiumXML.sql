IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyPremiumXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyPremiumXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
 Proc Name       : dbo.Proc_GetPolicyPremiumXML    
 Created by      : Vijay Arora    
 Date            : 12-01-2006  
 Purpose         : Get the Premium XML from Table named Proc_GetPolicyPremiumXML  
 Revison History :              
 Used In     : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC Dbo.Proc_GetPolicyPremiumXML  
(              
 	@CUSTOMER_ID  INT,        
 	@POLICY_ID  INT,      
 	@POLICY_VERSION_ID INT 
)              
AS              
BEGIN              
 	SELECT PREMIUM_XML, POSTED_PREMIUM_XML
	FROM  ACT_PREMIUM_PROCESS_DETAILS WHERE CUSTOMER_ID = @CUSTOMER_ID  
 	AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
END            



GO

