IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePostingInterface_Expense]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePostingInterface_Expense]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.UpdatePostingInterface_Expense
Created by      : Ajit Singh Chahal
Date            : 5/26/2005
Purpose    	  :To add posting interface - Expense
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc Proc_UpdatePostingInterface_Expense
create PROC Dbo.Proc_UpdatePostingInterface_Expense
(
@GL_ID     int,
@FISCAL_ID	int = null,
@EXP_COMM_INCURRED     int,
@EXP_REINS_COMM_EXCESS_CON     int,
@EXP_REINS_COMM_UMBRELLA_CON     int,
@EXP_ASSIGNED_CLAIMS     int,
@EXP_REINS_PAID_LOSSES     int,
@EXP_REINS_PAID_LOSSES_CAT     int,
@EXP_SMALL_BALANCE_WRITE_OFF int,
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
update ACT_GENERAL_LEDGER
set
	EXP_COMM_INCURRED= @EXP_COMM_INCURRED,
	EXP_REINS_COMM_EXCESS_CON= @EXP_REINS_COMM_EXCESS_CON,
	EXP_REINS_COMM_UMBRELLA_CON=@EXP_REINS_COMM_UMBRELLA_CON ,
	EXP_ASSIGNED_CLAIMS= @EXP_ASSIGNED_CLAIMS,
	EXP_REINS_PAID_LOSSES=@EXP_REINS_PAID_LOSSES ,
	EXP_REINS_PAID_LOSSES_CAT= @EXP_REINS_PAID_LOSSES_CAT,
	EXP_SMALL_BALANCE_WRITE_OFF=@EXP_SMALL_BALANCE_WRITE_OFF,
	MODIFIED_BY = @MODIFIED_BY ,
	LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
where GL_ID = @GL_ID
and
FISCAL_ID = @FISCAL_ID
end 








GO

