IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExpertServiceId4ClaimParty]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExpertServiceId4ClaimParty]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                  
Proc Name       : dbo.Proc_GetExpertServiceId4ClaimParty                                 
Created by      : Mohit Agarwal                                                           
Date            : 29/08/2008                                                                  
Purpose         : Checks if vendor code is present in Expert Service table and returns its id              
Revewed by      :                                                               
Revison History :                                                                  
Used In        : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/                                                                  
--DROP PROC dbo.Proc_GetExpertServiceId4ClaimParty 781,5,'asf1'                           
CREATE PROC [dbo].[Proc_GetExpertServiceId4ClaimParty]                                  
@CLAIM_ID int,                                 
@PARTY_ID int,                                 
@VENDOR_CODE varchar(50)
AS                                                                  
BEGIN                 
IF EXISTS(SELECT * FROM CLM_PARTIES WHERE CLAIM_ID = @CLAIM_ID AND VENDOR_CODE = @VENDOR_CODE)
    SELECT EXPERT_SERVICE_ID FROM CLM_EXPERT_SERVICE_PROVIDERS WHERE EXPERT_SERVICE_VENDOR_CODE
		= LTRIM(RTRIM(@VENDOR_CODE))
END                
      
    
  
  
  



GO

