IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateStatusCLM_CLAIM_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateStatusCLM_CLAIM_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*Proc Name     : dbo.Proc_UpdateStatusCLM_CLAIM_INFO                                  
Created by      : Vijay Arora
Date            : 16-06-2006
Purpose         :Update the Claim Status
Revison History :                                  
Used In        : Wolverine                                  
              
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
--drop PROC dbo.Proc_UpdateStatusCLM_CLAIM_INFO                           
CREATE PROC [dbo].[Proc_UpdateStatusCLM_CLAIM_INFO]                           
(                                  
	@CLAIM_ID int,    
	@CLAIM_STATUS int
)                            
AS                                  
BEGIN                                  

IF EXISTS (SELECT CLAIM_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID = @CLAIM_ID)
-- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008
	UPDATE CLM_CLAIM_INFO SET CLAIM_STATUS = @CLAIM_STATUS, REOPENED_DATE=GETDATE() WHERE CLAIM_ID = @CLAIM_ID
END                          
                                  
                                  
                                
                                
                                
                                





GO

