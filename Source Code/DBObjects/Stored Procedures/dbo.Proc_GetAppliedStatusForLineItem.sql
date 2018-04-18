IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppliedStatusForLineItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppliedStatusForLineItem]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc  
Created by      : Ebix  
Date            : 6/24/2005  
Purpose     : To get Applied status of line item  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop PROC Proc_GetAppliedStatusForLineItem  
create PROC Proc_GetAppliedStatusForLineItem  
(  
@CD_LINE_ITEM_ID int,  
@GROUP_TYPE varchar(5))  
as  
begin  
declare @count int,  
 @sum decimal,  
 @recAmount decimal  
 
 --select @count=count(*),@sum=sum(DISTRIBUTION_AMOUNT) 
 --COMMENTED BY PAWAN for REVIEW SHEET 
 select @count=count(DISTRIBUTION_AMOUNT),@sum=sum(DISTRIBUTION_AMOUNT)  
 from ACT_DISTRIBUTION_DETAILS  
 where GROUP_ID = @CD_LINE_ITEM_ID and GROUP_TYPE = @GROUP_TYPE  
 group by GROUP_ID,GROUP_TYPE  
if @count=0  
 return 'No'  
else  
begin  
 select @recAmount=RECEIPT_AMOUNT from ACT_CURRENT_DEPOSIT_LINE_ITEMS where CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID  
 if(@sum=@recAmount)  
  return 'Yes'  
 else if(@sum<@recAmount)  
  return 'Yes-Partial'  
 else if(@sum>@recAmount)  
  return 'Yes-Discrepancy'  
end  
end  
  
  
  



GO

