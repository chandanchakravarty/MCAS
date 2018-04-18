IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyLOBID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyLOBID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetPolicyLOBID  
Created by      : Vijay Arora    
Date            : 04-11-2005     
Purpose         : It will get the Policy LOB Id.  
Revison History :        
Used In         : Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC [dbo].[Proc_GetPolicyLOBID]    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT  
AS    
BEGIN    
SELECT POLICY_LOB,CONVERT(NVARCHAR(20),ISNULL(APP_EFFECTIVE_DATE,''),101) AS APPEFFECTIVEDATE,ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') AS STATENAME
FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)   
INNER JOIN MNT_COUNTRY_STATE_LIST WITH(NOLOCK) 
ON 
POL_CUSTOMER_POLICY_LIST.STATE_ID = MNT_COUNTRY_STATE_LIST.STATE_ID
   
WHERE CUSTOMER_ID = @CUSTOMER_ID  
AND POLICY_ID = @POLICY_ID  
AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
END    
  









GO

