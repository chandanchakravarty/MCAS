IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTransactionDescription]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTransactionDescription]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------  
Proc Name          : Dbo. Proc_GetTransactionDescription  
Created by           : Mohit Gupta  
Date                    : 31/05/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE    PROCEDURE dbo.Proc_GetTransactionDescription  
(  
 @TRANS_ID Int   
)  
AS  
BEGIN  

SELECT CLIENT_ID AS CUSTOMER_ID, ISNULL(POLICY_ID,0) AS POLICY_ID, ISNULL(POLICY_VER_TRACKING_ID,0) AS POLICY_VERSION_ID, TRANS_DESC,ADDITIONAL_INFO,APP_VERSION_ID,TRANS_TYPE_ID FROM MNT_TRANSACTION_LOG WHERE TRANS_ID = @TRANS_ID  
END  
  
  







GO

