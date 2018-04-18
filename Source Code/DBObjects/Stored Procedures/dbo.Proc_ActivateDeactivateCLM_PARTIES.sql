IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCLM_PARTIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCLM_PARTIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_ActivateDeactivateCLM_PARTIES
Created by      : Sumit Chhabra
Date            : 6/20/2006  
Purpose         : To activate-deactivate the values of parties
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC dbo.Proc_ActivateDeactivateCLM_PARTIES  
(  
 @CLAIM_ID int,  
 @PARTY_ID int,  
 @IS_ACTIVE char(1)
)  
AS  
BEGIN  
 UPDATE CLM_PARTIES SET IS_ACTIVE=@IS_ACTIVE WHERE CLAIM_ID=@CLAIM_ID AND PARTY_ID=@PARTY_ID
END








GO

