IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCliamDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCliamDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                    
Proc Name       : dbo.Proc_GetCliamDetails
Created by      : Vijay Arora
Date            : 07/17/2006
Purpose         : To get the details from Claims Table.
Revison History :                                                    
Used In        : Wolverine                                                    
------------------------------------------------------------                                                    
Date     Review By          Comments                                                    
------   ------------       -------------------------*/                                                    
--DROP PROC dbo.Proc_GetCliamDetails                                                                       
CREATE PROC dbo.Proc_GetCliamDetails                                                                       
@CLAIM_NUMBER char(10)                              
AS                                                    
BEGIN                                                    
SELECT C.CUSTOMER_ID,C.POLICY_ID,C.POLICY_VERSION_ID,C.CLAIM_ID,C.CLAIM_NUMBER,P.POLICY_LOB
FROM CLM_CLAIM_INFO  C
LEFT JOIN POL_CUSTOMER_POLICY_LIST P ON P.CUSTOMER_ID = C.CUSTOMER_ID AND
P.POLICY_ID = C.POLICY_ID AND P.POLICY_VERSION_ID = C.POLICY_VERSION_ID
WHERE                                            
CLAIM_NUMBER LIKE '%'+ RTRIM(LTRIM(@CLAIM_NUMBER)) +'%'
END                                              
                                          



GO

