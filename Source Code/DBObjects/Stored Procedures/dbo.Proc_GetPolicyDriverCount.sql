IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDriverCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDriverCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetPolicyDriverCount  
Created by           : Vijay Arora
Date                     : 07-11-2005
Purpose               : To get the driver count for the Policy
Revison History :  
Used In                :   Wolverine  
  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetPolicyDriverCount  
 
@CUSTOMERID  int,  
@POLICYID  int,  
@POLICYVERSIONID int
  
AS  
BEGIN  
 SELECT COUNT(DRIVER_ID) DRIVER_ID FROM POL_DRIVER_DETAILS  
 WHERE    CUSTOMER_ID = @CUSTOMERID   and POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID   
END  
  
  
  
  
  
  



GO

