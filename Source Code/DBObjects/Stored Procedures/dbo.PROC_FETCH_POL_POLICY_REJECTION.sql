IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_FETCH_POL_POLICY_REJECTION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_FETCH_POL_POLICY_REJECTION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
 /*----------------------------------------------------------              
Proc Name       : dbo.POL_POLICY_REJECTION       
Created by      : Pradeep Kushwaha
Date            : 08/07/2010              
Purpose         :Fetch records From POL_POLICY_REJECTION  Table.              
Revison History :              
Used In        : Ebix Advantage              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--DROP PROC dbo.PROC_FETCH_POL_POLICY_REJECTION     

CREATE PROC [dbo].[PROC_FETCH_POL_POLICY_REJECTION]  
(  
@REJECT_REASON_ID INT,  
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT    
)  
AS  
BEGIN  
SELECT 
	CUSTOMER_ID,
	POLICY_ID,
	POLICY_VERSION_ID,
	REJECT_REASON_ID,
	REASON_TYPE_ID,
	REASON_DESC,
	IS_ACTIVE
FROM  
POL_POLICY_REJECTION     WITH(NOLOCK)  
WHERE   
	CUSTOMER_ID=  @CUSTOMER_ID AND    
	POLICY_ID= @POLICY_ID AND    
	POLICY_VERSION_ID=@POLICY_VERSION_ID  AND  
	REJECT_REASON_ID=@REJECT_REASON_ID   
END  
  
  
  
GO

