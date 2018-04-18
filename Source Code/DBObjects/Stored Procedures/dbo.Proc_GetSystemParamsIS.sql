IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSystemParamsIS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSystemParamsIS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetSystemParamsIS  
Created by      : Anurag Verma  
Date            :  7/8/2005  
Purpose         : To select  sys_insurance_score_validity field  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
  
CREATE PROC Dbo.Proc_GetSystemParamsIS  
  
AS  
  
BEGIN  
  
  SELECT SYS_INSURANCE_SCORE_VALIDITY FROM MNT_SYSTEM_PARAMS  
END  



GO

