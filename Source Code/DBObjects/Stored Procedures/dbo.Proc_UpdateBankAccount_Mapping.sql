IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateBankAccount_Mapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateBankAccount_Mapping]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_UpdateBankAccount_Mapping
create proc dbo.Proc_UpdateBankAccount_Mapping          
(          
 @GL_ID       int,   
 @FISCAL_ID int = null,	         
 @BNK_OVER_PAYMENT      int,          
 @BNK_RETURN_PRM_PAYMENT int,          
 @BNK_SUSPENSE_AMOUNT    int,        
 @BNK_CLAIMS_DEFAULT_AC    int,        
 @BNK_REINSURANCE_DEFAULT_AC int,           
 @BNK_DEPOSITS_DEFAULT_AC int,    
 @BNK_MISC_CHK_DEFAULT_AC int,      
 @MODIFIED_BY      int,          
 @LAST_UPDATED_DATETIME  datetime     ,    
 --@BNK_MISC_CHK_DEFAULT_AC int,       
 @CLM_CHECK_DEFAULT_AC int,  
 @BNK_CUST_DEP_EFT_CARD  int,
 @BNK_AGEN_CHK_DEFAULT_AC int  
)          
as          
begin          
 update ACT_GENERAL_LEDGER          
 set          
  BNK_OVER_PAYMENT  = @BNK_OVER_PAYMENT,          
  BNK_RETURN_PRM_PAYMENT  = @BNK_RETURN_PRM_PAYMENT,          
  BNK_SUSPENSE_AMOUNT     = @BNK_SUSPENSE_AMOUNT,        
  BNK_CLAIMS_DEFAULT_AC = @BNK_CLAIMS_DEFAULT_AC,        
  BNK_REINSURANCE_DEFAULT_AC = @BNK_REINSURANCE_DEFAULT_AC,          
  BNK_DEPOSITS_DEFAULT_AC  = @BNK_DEPOSITS_DEFAULT_AC,    
  BNK_MISC_CHK_DEFAULT_AC = @BNK_MISC_CHK_DEFAULT_AC,       
  MODIFIED_BY   = @MODIFIED_BY,          
  LAST_UPDATED_DATETIME   = @LAST_UPDATED_DATETIME ,    
  CLM_CHECK_DEFAULT_AC     = @CLM_CHECK_DEFAULT_AC,  
  BNK_CUST_DEP_EFT_CARD = @BNK_CUST_DEP_EFT_CARD,
  BNK_AGEN_CHK_DEFAULT_AC= @BNK_AGEN_CHK_DEFAULT_AC   
where GL_ID = @GL_ID      
and
FISCAL_ID = @FISCAL_ID    
end           




GO

