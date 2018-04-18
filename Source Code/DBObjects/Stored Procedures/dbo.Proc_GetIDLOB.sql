IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetIDLOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetIDLOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetIDLOB  
Created by         : Vijay Arora
Date               : 21-12-2005
Purpose            : To get LOB ID.
 
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetIDLOB  
(  
 @CUSTOMER_ID int,
 @POLICY_ID int,
 @POLICY_VERSION_ID int
)  
AS  
BEGIN  
SELECT POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST  
WHERE CUSTOMER_ID = @CUSTOMER_ID
AND POLICY_ID = @POLICY_ID
AND POLICY_VERSION_ID = @POLICY_VERSION_ID
END  



GO

