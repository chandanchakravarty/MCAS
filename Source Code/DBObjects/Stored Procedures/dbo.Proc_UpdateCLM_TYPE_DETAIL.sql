IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_TYPE_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_TYPE_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- DROP PROC Dbo.Proc_UpdateCLM_TYPE_DETAIL  
CREATE PROC Dbo.Proc_UpdateCLM_TYPE_DETAIL      
(      
 @DETAIL_TYPE_ID     int,      
 @DETAIL_TYPE_DESCRIPTION     varchar(100),      
 @MODIFIED_BY     int,    
 @TRANSACTION_CODE  int,    
 @TRANSACTION_CATEGORY  VARCHAR(20),    
 @IS_SYSTEM_GENERATED CHAR(1)= NULL,    
 @SelectedDebitLedgers varchar(500)=null,    
 @SelectedCreditLedgers varchar(500)=null,  
 @LOSS_TYPE_CODE nvarchar(80)=NULL,    
 @LOSS_DEPARTMENT nvarchar(40)=NULL,    
 @LOSS_EXTRA_COVER nvarchar(40)=NULL     
)      
AS      
BEGIN      
UPDATE CLM_TYPE_DETAIL       
 SET  DETAIL_TYPE_DESCRIPTION = @DETAIL_TYPE_DESCRIPTION,      
  MODIFIED_BY = @MODIFIED_BY,LAST_UPDATED_DATETIME = GETDATE(),    
  TRANSACTION_CODE = @TRANSACTION_CODE ,TRANSACTION_CATEGORY = @TRANSACTION_CATEGORY,    
  IS_SYSTEM_GENERATED = @IS_SYSTEM_GENERATED,SelectedDebitLedgers = @SelectedDebitLedgers,     
  SelectedCreditLedgers = @SelectedCreditLedgers,     
  LOSS_TYPE_CODE = @LOSS_TYPE_CODE,    
  LOSS_DEPARTMENT = @LOSS_DEPARTMENT ,    
  LOSS_EXTRA_COVER = @LOSS_EXTRA_COVER   
WHERE DETAIL_TYPE_ID = @DETAIL_TYPE_ID      
END  
  









GO

