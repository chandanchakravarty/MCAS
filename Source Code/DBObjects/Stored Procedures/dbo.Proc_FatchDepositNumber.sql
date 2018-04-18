IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FatchDepositNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FatchDepositNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name           : Dbo.Proc_FatchDepositNumber    
Created by            : kranti singh    
Date                    : 07/06/2007    
Purpose                :     
Revison History :    
Used In                 :   Wolverine      

Reviewed By	:	Anurag Verma
Reviewed On	:	12-07-2007
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
---- DROP PROC Dbo.Proc_FatchDepositNumber      
  
create procedure dbo.Proc_FatchDepositNumber  
(  
 @CD_LINE_ITEM_ID int  
)  
AS  
  
SELECT ACD.DEPOSIT_NUMBER FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS ACDLI
LEFT OUTER JOIN ACT_CURRENT_DEPOSITS ACD ON ACD.DEPOSIT_ID = ACDLI.DEPOSIT_ID
WHERE ACDLI.CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID 
  
  
  





GO

