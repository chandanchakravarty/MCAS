IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_TYPE_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_TYPE_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertCLM_TYPE_DETAIL  
Created by      : Vijay Arora  
Date            : 4/20/2006  
Purpose     : To Insert the Record in Table named CLM_TYPE_DETAIL  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc Proc_InsertCLM_TYPE_DETAIL
CREATE PROC Dbo.Proc_InsertCLM_TYPE_DETAIL      
(      
 @DETAIL_TYPE_DESCRIPTION     varchar(100),      
 @TYPE_ID int,      
 @CREATED_BY int,      
 @DETAIL_TYPE_ID int OUTPUT,    
 @TRANSACTION_CODE  int,    
 @TRANSACTION_CATEGORY  VARCHAR(20),    
 @IS_SYSTEM_GENERATED CHAR(1)= NULL,    
 @SelectedDebitLedgers varchar(500)= null,    
 @SelectedCreditLedgers varchar(500)= null ,  
 @LOSS_TYPE_CODE nvarchar(80)=NULL,  
 @LOSS_DEPARTMENT nvarchar(40)=NULL,  
 @LOSS_EXTRA_COVER nvarchar(40)=NULL
)      
AS      
BEGIN      
      
SELECT @DETAIL_TYPE_ID=isnull(Max(DETAIL_TYPE_ID),0)+1 from CLM_TYPE_DETAIL      
      
INSERT INTO CLM_TYPE_DETAIL      
(      
 DETAIL_TYPE_ID,      
 TYPE_ID,      
 DETAIL_TYPE_DESCRIPTION,      
 TRANSACTION_CODE,    
 TRANSACTION_CATEGORY,    
 IS_SYSTEM_GENERATED,    
 IS_ACTIVE,      
 CREATED_BY,      
 CREATED_DATETIME ,    
 SelectedDebitLedgers ,    
 SelectedCreditLedgers,  
 LOSS_TYPE_CODE,  
 LOSS_DEPARTMENT,  
 LOSS_EXTRA_COVER    
)      
VALUES      
(      
 @DETAIL_TYPE_ID,      
 @TYPE_ID,      
 @DETAIL_TYPE_DESCRIPTION,    
 @TRANSACTION_CODE,    
 @TRANSACTION_CATEGORY,     
 @IS_SYSTEM_GENERATED,     
 'Y',      
 @CREATED_BY,      
 GetDate()  ,    
 @SelectedDebitLedgers,    
 @SelectedCreditLedgers,  
 @LOSS_TYPE_CODE,  
 @LOSS_DEPARTMENT,  
 @LOSS_EXTRA_COVER     
)      
END 
  








GO

