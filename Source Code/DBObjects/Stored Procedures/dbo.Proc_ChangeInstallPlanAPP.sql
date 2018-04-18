IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ChangeInstallPlanAPP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ChangeInstallPlanAPP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ChangeInstallPlanAPP
Created by      : Ravindra 
Date            : 12-26-2006
Purpose         : To Re Adjust installment plan for the selected Application
Revison History :        
Used In         :            

-----------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop PROC dbo.Proc_ChangeInstallPlanAPP
CREATE PROC [dbo].[Proc_ChangeInstallPlanAPP]
(
@CUSTOMER_ID Int,
@APP_ID Int,
@APP_VERSION_ID Int,
@NEW_PLAN_ID Int
)
AS        
BEGIN        


DECLARE @MODE_OF_DOWNPAY INT
DECLARE @MODE_OF_DOWNPAY1 INT
DECLARE @MODE_OF_DOWNPAY2 INT

SELECT 
@MODE_OF_DOWNPAY = ISNULL(MODE_OF_DOWNPAY,0),
@MODE_OF_DOWNPAY1 = ISNULL(MODE_OF_DOWNPAY1,0),
@MODE_OF_DOWNPAY2=  ISNULL(MODE_OF_DOWNPAY2,0)
FROM 
ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK)
WHERE IDEN_PLAN_ID = @NEW_PLAN_ID


UPDATE APP_LIST SET INSTALL_PLAN_ID = @NEW_PLAN_ID,
DOWN_PAY_MODE = CASE WHEN @MODE_OF_DOWNPAY <> 0 THEN @MODE_OF_DOWNPAY ELSE 
			CASE WHEN @MODE_OF_DOWNPAY1 <> 0 THEN @MODE_OF_DOWNPAY1 ELSE 
			    CASE WHEN @MODE_OF_DOWNPAY2 <> 0 THEN @MODE_OF_DOWNPAY2 ELSE DOWN_PAY_MODE 
				END
			 END	
		      END 
WHERE APP_ID = @APP_ID
AND APP_VERSION_ID = @APP_VERSION_ID
AND CUSTOMER_ID  = @CUSTOMER_ID

return 1

END    
  





GO

