IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_MCCA_ATTACHMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_MCCA_ATTACHMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertCLM_MCCA_ATTACHMENT    
Created by      : Vijay Arora    
Date            : 8/8/2006    
Purpose     : To Insert the record in table named CLM_MCCA_ATTACHMENT    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC Dbo.Proc_InsertCLM_MCCA_ATTACHMENT    
CREATE PROC [dbo].[Proc_InsertCLM_MCCA_ATTACHMENT]    
(    
@MCCA_ATTACHMENT_ID     int OUTPUT,    
@POLICY_PERIOD_DATE_FROM     datetime,    
@POLICY_PERIOD_DATE_TO     datetime,    
@LOSS_PERIOD_DATE_FROM     datetime,    
@LOSS_PERIOD_DATE_TO     datetime,    
@MCCA_ATTACHMENT_POINT     int,    
@CREATED_BY     int    
)    
AS    
BEGIN    
  
 IF NOT EXISTS   
 (SELECT MCCA_ATTACHMENT_ID FROM CLM_MCCA_ATTACHMENT   
  WHERE ((@POLICY_PERIOD_DATE_FROM BETWEEN POLICY_PERIOD_DATE_FROM AND POLICY_PERIOD_DATE_TO)   
  OR  
  (@LOSS_PERIOD_DATE_FROM BETWEEN LOSS_PERIOD_DATE_FROM AND LOSS_PERIOD_DATE_TO))
-- Added by Asfa(19-June-2008) - iTrack #4355
  AND ISNULL(IS_ACTIVE,'N')='Y'
  )  
  
 BEGIN  
  SELECT @MCCA_ATTACHMENT_ID=isnull(MAX(MCCA_ATTACHMENT_ID),0)+1 FROM CLM_MCCA_ATTACHMENT    
  INSERT INTO CLM_MCCA_ATTACHMENT    
  (    
  MCCA_ATTACHMENT_ID,    
  POLICY_PERIOD_DATE_FROM,    
  POLICY_PERIOD_DATE_TO,    
  LOSS_PERIOD_DATE_FROM,    
  LOSS_PERIOD_DATE_TO,    
  MCCA_ATTACHMENT_POINT,    
  IS_ACTIVE,    
  CREATED_BY,    
  CREATED_DATETIME    
  )    
  VALUES    
  (    
  @MCCA_ATTACHMENT_ID,    
  @POLICY_PERIOD_DATE_FROM,    
  @POLICY_PERIOD_DATE_TO,    
  @LOSS_PERIOD_DATE_FROM,    
  @LOSS_PERIOD_DATE_TO,    
  @MCCA_ATTACHMENT_POINT,    
  'Y',    
  @CREATED_BY,    
  GETDATE()    
  )    
 END  
 ELSE  
 BEGIN  
  RETURN -1  
 END  
END    
    
    
  






GO

