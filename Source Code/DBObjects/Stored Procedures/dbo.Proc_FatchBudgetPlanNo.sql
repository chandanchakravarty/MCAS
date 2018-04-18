IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FatchBudgetPlanNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FatchBudgetPlanNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          	: Dbo.Proc_FatchBudgetPlanNo  
Created by           	: Manoj Rathore
Date                    : 06/22/2007  
Purpose               	:   
Revison History :  
Used In                	:   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
---- DROP PROC Dbo.Proc_FatchBudgetPlanNo    

create procedure dbo.Proc_FatchBudgetPlanNo
(
 @IDEN_ROW_ID int
)
AS


SELECT  IDEN_ROW_ID FROM ACT_BUDGET_PLAN  MVL 
--LEFT OUTER JOIN  MNT_VENDOR_LIST AVI ON MVL.VENDOR_ID = AVI.VENDOR_ID
WHERE MVL.IDEN_ROW_ID=@IDEN_ROW_ID







GO

