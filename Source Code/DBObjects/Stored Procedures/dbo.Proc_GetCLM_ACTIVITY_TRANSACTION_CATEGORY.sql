IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_TRANSACTION_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_TRANSACTION_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_GetCLM_ACTIVITY_TRANSACTION_CATEGORY
Created by      :  Asfa Praveen    
Date            :  13/NOV/2007                
Purpose         :  To get Transaction_Category corresponding to ACTION_ON_PAYMENT
Revison History :                
Used In         :   Wolverine                
-------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_GetCLM_ACTIVITY_TRANSACTION_CATEGORY             
CREATE proc dbo.Proc_GetCLM_ACTIVITY_TRANSACTION_CATEGORY                
@ACTION_ON_PAYMENT INT
AS                
BEGIN 

  SELECT TRANSACTION_CATEGORY FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID = @ACTION_ON_PAYMENT     

END         


GO

