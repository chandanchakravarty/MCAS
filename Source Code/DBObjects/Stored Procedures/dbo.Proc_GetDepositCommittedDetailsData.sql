IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDepositCommittedDetailsData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDepositCommittedDetailsData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================                
-- Author   : Pradeep Kushwaha    
-- Create date: 20-Jun-2011            
-- Description: Get Deposit Committed Details Data
-- DROP PROC Proc_GetDepositCommittedDetailsData          
-- Proc_GetDepositCommittedDetailsData  457 
-- =============================================                          
                             
CREATE PROC [dbo].[Proc_GetDepositCommittedDetailsData]     
 (                          
 @DEPOSIT_ID   INT,
 @CD_LINE_ITEM_ID INT=NULL
)       
AS                          
BEGIN               
IF(@CD_LINE_ITEM_ID<1)    
      SET @CD_LINE_ITEM_ID = NULL  
           
SELECT ITEMS.CUSTOMER_ID,ITEMS.POLICY_ID,ITEMS.POLICY_VERSION_ID,    
ITEMS.CD_LINE_ITEM_ID,ITEMS.DEPOSIT_ID, ITEMS.RECEIPT_NUM,
 ITEMS.IS_APPROVE
FROM    
ACT_CURRENT_DEPOSIT_LINE_ITEMS ITEMS WITH(NOLOCK)
INNER JOIN ACT_CURRENT_DEPOSITS ACD WITH(NOLOCK) ON 
ITEMS.DEPOSIT_ID=ACD.DEPOSIT_ID 
AND ISNULL(ACD.IS_COMMITED, 'N')<>'N'
AND ITEMS.INSTALLMENT_NO=1
AND ISNULL(ITEMS.IS_APPROVE,'') <> 'R'
WHERE ITEMS.DEPOSIT_ID=@DEPOSIT_ID AND  (@CD_LINE_ITEM_ID IS NULL OR CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID)
 
END        
  
             
   
   
GO

