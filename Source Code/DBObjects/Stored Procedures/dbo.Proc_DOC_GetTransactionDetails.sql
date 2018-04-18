IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_GetTransactionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_GetTransactionDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DOC_GetTransactionDetails
Created by      : Deepak Gupta         
Date            : 09-11-2006                         
Purpose         : To Get Transaction Details
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC DBO.Proc_DOC_GetTransactionDetails
(                
	@TRANS_ID int
)                
AS
BEGIN                
	SELECT AL.AGENCY_CODE,TL.CLIENT_ID,TL.APP_ID,TL.APP_VERSION_ID,TL.POLICY_ID,TL.POLICY_VER_TRACKING_ID POLICY_VERSION_ID,TL.RECORDED_BY USER_ID
	FROM MNT_TRANSACTION_LOG TL 
	INNER JOIN CLT_CUSTOMER_LIST CL ON CLIENT_ID=CUSTOMER_ID 
	INNER JOIN MNT_AGENCY_LIST AL ON CUSTOMER_AGENCY_ID=AGENCY_ID WHERE TL.TRANS_ID=@TRANS_ID
END





GO

