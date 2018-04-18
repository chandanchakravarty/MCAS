IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyAppNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyAppNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name	: Dbo.Proc_GetPolicyAppNumber    
Created by	: Asfa Praveen   
Date		: 19/Dec/2007
Purpose		: To get Policy and Application number based on Policy_id    
Revison History	:    
Used In		: Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       --------------------------------
Modify by	:
Date		:
Purpose		:
------------------------------------------------------------*/  

-- DROP PROC dbo.Proc_GetPolicyAppNumber         
CREATE PROC dbo.Proc_GetPolicyAppNumber          
(            
 @POLICY_ID     int,
 @CUSTOMER_ID int         
)            
AS            
BEGIN

	SELECT 
	TOP 1 
	PCPL.POLICY_NUMBER, PCPL.APP_NUMBER, CQL.QQ_NUMBER  ,ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME
	FROM 
	POL_CUSTOMER_POLICY_LIST PCPL 
	LEFT OUTER JOIN CLT_QUICKQUOTE_LIST CQL
		ON PCPL.CUSTOMER_ID=CQL.CUSTOMER_ID AND PCPL.APP_ID=CQL.APP_ID
	LEFT JOIN CLT_CUSTOMER_LIST CLT 
		ON CLT.CUSTOMER_ID = PCPL.CUSTOMER_ID
	WHERE 
	PCPL.CUSTOMER_ID=@CUSTOMER_ID AND PCPL.POLICY_ID=@POLICY_ID 
	ORDER BY PCPL.POLICY_VERSION_ID
END      
      

    
  










GO

