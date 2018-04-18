IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePostingInterface_Equity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePostingInterface_Equity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.UpdatePostingInterface_Equity  
Created by      : Ajit Singh Chahal  
Date            : 5/26/2005  
Purpose       :To add posting interface - Equity  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_UpdatePostingInterface_Equity  
create  PROC dbo.Proc_UpdatePostingInterface_Equity  
(  
@GL_ID     int,
@FISCAL_ID int =null,  
@EQU_TRANSFER     int,
@EQU_UNASSIGNED_SURPLUS int,
@MODIFIED_BY     int,  
@LAST_UPDATED_DATETIME     datetime  
)  
AS  
BEGIN  
update ACT_GENERAL_LEDGER  
set  
 EQU_TRANSFER = @EQU_TRANSFER,
 EQU_UNASSIGNED_SURPLUS = @EQU_UNASSIGNED_SURPLUS,
 MODIFIED_BY = @MODIFIED_BY,  
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME  
where GL_ID = @GL_ID  
and
FISCAL_ID = @FISCAL_ID
end   
  
  





GO

