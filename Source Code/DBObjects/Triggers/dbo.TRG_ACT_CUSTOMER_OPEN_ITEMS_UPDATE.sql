IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TRG_ACT_CUSTOMER_OPEN_ITEMS_UPDATE]'))
DROP TRIGGER [dbo].[TRG_ACT_CUSTOMER_OPEN_ITEMS_UPDATE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*******************************************************************    
    
Create by  : Vijay Joshi    
Create Datetime : 24 March,2006    
Purpose   : To customer balance information from customer open item table    
    
********************************************************************/    
--drop TRIGGER TRG_ACT_CUSTOMER_OPEN_ITEMS_UPDATE
CREATE TRIGGER TRG_ACT_CUSTOMER_OPEN_ITEMS_UPDATE ON [dbo].[ACT_CUSTOMER_OPEN_ITEMS]     
FOR UPDATE    
AS    
BEGIN    
    
 --Inserting the entry in customer balance informartion    
 INSERT INTO ACT_CUSTOMER_BALANCE_INFORMATION    
 (    
  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,    
  OPEN_ITEM_ROW_ID, AMOUNT, AMOUNT_DUE,    
  TRAN_DESC,    
  UPDATED_FROM, CREATED_DATE,    
  IS_PRINTED, PRINT_DATE, SOURCE_ROW_ID ,DUE_DATE    
 )    
 SELECT     
  ACOI.CUSTOMER_ID, ACOI.POLICY_ID, ACOI.POLICY_VERSION_ID,     
  ACOI.IDEN_ROW_ID, ACOI.TOTAL_DUE, ACOI.TOTAL_DUE,    
  ACOI.TRANS_DESC,    
  'P', GETDATE(),    
  0,NULL, ACOI.SOURCE_ROW_ID,ACOI.DUE_DATE    
 FROM     
  INSERTED ACOI    
 INNER JOIN DELETED OI ON ACOI.IDEN_ROW_ID = OI.IDEN_ROW_ID    
 WHERE     
  ISNULL(ACOI.NOT_COUNTED_RECEIVABLE,1) = 0 AND     
  ISNULL(ACOI.NOT_COUNTED_RECEIVABLE, 1) <> ISNULL(OI.NOT_COUNTED_RECEIVABLE, 1)  
  AND ISNULL(ACOI.UPDATED_FROM,'') NOT IN ('N','L')  
  AND ISNULL(ACOI.ITEM_TRAN_CODE,'') <> 'RENSF'
    
END    




GO

