IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyOldInputXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyOldInputXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name      : dbo.Proc_GetPolicyOldInputXML    
Created by     : ASFA   PRAVEEN  
Date           : 22-June-2006 7  
Purpose        : Get the Old InputXML for the Policy Which Status is Active(Policy has been Comitted)  
Revison History :      
Modified by   :       
Description  :       
Used In        : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
-- DROP PROC Dbo.Proc_GetPolicyOldInputXML 2156,70,1      
CREATE  PROC [dbo].[Proc_GetPolicyOldInputXML]    
@CUSTOMER_ID INT,    
@POLICY_ID INT,      
@POLICY_VERSION_ID SMALLINT    
AS      
BEGIN      
SELECT QCQ.QUOTE_INPUT_XML ,
POLICY_STATUS = CASE 
WHEN PCP.POLICY_STATUS IS NULL
	THEN UPPER(APP_STATUS)
ELSE
	UPPER(PCP.POLICY_STATUS) END, QCQ.QUOTE_ID  
FROM POL_CUSTOMER_POLICY_LIST PCP WITH (NOLOCK) INNER JOIN QOT_CUSTOMER_QUOTE_LIST_POL QCQ WITH (NOLOCK)  
ON PCP.CUSTOMER_ID=QCQ.CUSTOMER_ID AND PCP.POLICY_ID=QCQ.POLICY_ID AND PCP.POLICY_VERSION_ID=QCQ.POLICY_VERSION_ID  
WHERE PCP.CUSTOMER_ID=@CUSTOMER_ID  AND PCP.POLICY_ID=@POLICY_ID  AND PCP.POLICY_VERSION_ID=@POLICY_VERSION_ID  
END      
  
GO

