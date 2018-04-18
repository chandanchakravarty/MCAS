IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCLM_MCCA_ATTACHMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCLM_MCCA_ATTACHMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivateCLM_MCCA_ATTACHMENT    
Created by      : Sumit Chhabra    
Date            : 07/11/2007    
Purpose     	: To activate/ deactivate in table named CLM_MCCA_ATTACHMENT    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC dbo.Proc_ActivateDeactivateCLM_MCCA_ATTACHMENT    
CREATE PROC [dbo].[Proc_ActivateDeactivateCLM_MCCA_ATTACHMENT]    
(    
@MCCA_ATTACHMENT_ID     int,    
@IS_ACTIVE char(1)
)    
AS    
BEGIN
-- Added by Asfa(19-June-2008) - iTrack #4355
/*before activating a record, check whether an active record has the same policy/loss period dates, 
then don't allow the record to be active. */
DECLARE @POLICY_PERIOD_DATE_FROM datetime
DECLARE @LOSS_PERIOD_DATE_FROM   datetime

IF(@IS_ACTIVE = 'Y')    
BEGIN
  SELECT @POLICY_PERIOD_DATE_FROM= POLICY_PERIOD_DATE_FROM, @LOSS_PERIOD_DATE_FROM = LOSS_PERIOD_DATE_FROM
  FROM CLM_MCCA_ATTACHMENT WHERE MCCA_ATTACHMENT_ID = @MCCA_ATTACHMENT_ID

  IF EXISTS(SELECT MCCA_ATTACHMENT_ID FROM CLM_MCCA_ATTACHMENT   
			WHERE ((@POLICY_PERIOD_DATE_FROM BETWEEN POLICY_PERIOD_DATE_FROM AND POLICY_PERIOD_DATE_TO)   
			OR   (@LOSS_PERIOD_DATE_FROM BETWEEN LOSS_PERIOD_DATE_FROM AND LOSS_PERIOD_DATE_TO))
			AND ISNULL(IS_ACTIVE,'N')='Y')
  BEGIN
	RETURN;
  END
END
  UPDATE  
	CLM_MCCA_ATTACHMENT    
  SET    
	  IS_ACTIVE  =  @IS_ACTIVE      
  WHERE 
	MCCA_ATTACHMENT_ID = @MCCA_ATTACHMENT_ID    
END  





GO

