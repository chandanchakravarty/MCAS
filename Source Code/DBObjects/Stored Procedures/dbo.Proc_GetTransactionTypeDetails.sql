IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTransactionTypeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTransactionTypeDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --drop proc dbo.Proc_GetTransactionTypeDetails  
  
/*  
Created By : Anurag Verma  
Created On : 22/03/2007  
Purpose  : Fetch transaction details according to trans type id from MNT_TRANSACTION_TYPE_LIST table  
*/  
  
CREATE PROC [dbo].[Proc_GetTransactionTypeDetails]  
(  
 @trans_id int  
   
   
)  
AS  
BEGIN  
SELECT isnull(TRANS_TYPE_DESC,'') TRANS_DESC   
from MNT_TRANSACTION_TYPE_LIST with(nolock)
where TRANS_type_id=@trans_id  
END  
  
  
  
  
  
  
  
GO

