IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_Deposit_Line_Items]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_Deposit_Line_Items]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*               
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_Delete_Deposit_Line_Items                          
Created by      : Pradeep Kushwaha                     
Date            : 27/Oct/2010                                    
Purpose         : Delete Depost Line items                              
Revison History :                                    
Used In        : Ebix Advantage                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                    
drop proc Proc_Delete_Deposit_Line_Items   
*/                  
                       
CREATE PROC  [dbo].[Proc_Delete_Deposit_Line_Items]
 (                    
 @DEPOSIT_ID   INT,                                
 @CD_LINE_ITEM_ID INT
) 
AS                    
BEGIN         

UPDATE ACT_CURRENT_DEPOSITS  
SET TOTAL_DEPOSIT_AMOUNT = (SELECT isnull(SUM(RECEIPT_AMOUNT),0)  
FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS  
WHERE DEPOSIT_ID = @DEPOSIT_ID AND CD_LINE_ITEM_ID <> @CD_LINE_ITEM_ID)  
WHERE DEPOSIT_ID = @DEPOSIT_ID  


DELETE  ACT_CURRENT_DEPOSIT_LINE_ITEMS
WHERE 
CD_LINE_ITEM_ID=@CD_LINE_ITEM_ID AND 
DEPOSIT_ID= @DEPOSIT_ID

END  
      
                 

GO

