IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetStatus_ACT_BANK_RECONCILIATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetStatus_ACT_BANK_RECONCILIATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : dbo.ACT_BANK_RECONCILIATION   
Created by      : Ajit Singh Chahal  
Rule  : a bank recon can be committed only if fully distributed.  
Date            : 6/28/2005  
Purpose     : Returns status of bank reconciliation record  
Revison History :  
Used In  : Wolverine  
Status values : N-Not distributed, D-Distributed, C-Committed  
------------------------------------------------------------  
Date     Review By          Comments  
drop PROC dbo.Proc_GetStatus_ACT_BANK_RECONCILIATION  
------   ------------       -------------------------*/  
create   PROC Dbo.Proc_GetStatus_ACT_BANK_RECONCILIATION  
(  
@AC_RECONCILIATION_ID    int  
)  
AS  
BEGIN  
declare @retValue varchar(5) ,  
@IsCommited char(1),  
@RemainingAmount decimal(18,2)  
  
select @RemainingAmount=(BR.BANK_CHARGES_CREDITS-  
isnull((SELECT SUM(DISTRIBUTION_AMOUNT) FROM ACT_DISTRIBUTION_DETAILS WHERE      GROUP_TYPE = 'BRN' AND GROUP_ID = BR.AC_RECONCILIATION_ID),0))   
from ACT_BANK_RECONCILIATION BR where AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID  
  
 if @RemainingAmount=0   
 begin  
  select @IsCommited=IS_COMMITED from  ACT_BANK_RECONCILIATION where AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID  
  if @IsCommited='Y'  
          begin  
   set @retValue = 'C'  
   end  
  else  
    set @retValue = 'D'  
 end  
 else  
  set @retValue = 'N'  
  
select @retValue as status   
end  
  
  



GO

