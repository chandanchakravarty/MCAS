IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_CURRENT_DEPOSITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_CURRENT_DEPOSITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertACT_CURRENT_DEPOSITS    
Created by      : Vijay Joshi    
Date            : 6/20/2005    
Purpose     : Evaluation    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_InsertACT_CURRENT_DEPOSITS    
CREATE PROC [dbo].[Proc_InsertACT_CURRENT_DEPOSITS]    
(    
 @DEPOSIT_ID       int OUTPUT,    
 @GL_ID         int,    
 @FISCAL_ID        smallint,    
 @ACCOUNT_ID       int,    
 @DEPOSIT_NUMBER   int,    
 @DEPOSIT_TRAN_DATE  datetime,    
 @TOTAL_DEPOSIT_AMOUNT   decimal(18,2),    
 @DEPOSIT_NOTE    nvarchar(510),    
 @IS_BNK_RECONCILED     nchar(2),    
 @IN_BNK_RECON    int,    
 @CREATED_BY       int,    
 @CREATED_DATETIME  datetime,    
 @DEPOSIT_TYPE   varchar(6),  
 @RECEIPT_MODE smallint=null,  
 @RTL_FILE NVARCHAR(250) = null,  
 @ACCOUNT_BALANCE DECIMAL (18,2) = null  
)    
AS    
BEGIN    
    
 If Exists(SELECT DEPOSIT_NUMBER FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_NUMBER = @DEPOSIT_NUMBER AND ACCOUNT_ID = @ACCOUNT_ID)    
 BEGIN    
  /*Duplicate deposit number exist hence existing with status -2*/    
  return -2    
 END    
    
 /*Retreiving the maximim journal id*/    
 SELECT @DEPOSIT_ID = IsNull(Max(DEPOSIT_ID), 0) + 1    
 FROM ACT_CURRENT_DEPOSITS    
    
 SELECT @GL_ID = GL_ID FROM ACT_GENERAL_LEDGER WHERE FISCAL_ID = @FISCAL_ID    
    
 INSERT INTO ACT_CURRENT_DEPOSITS    
 (    
  DEPOSIT_ID, GL_ID, FISCAL_ID, ACCOUNT_ID,    
  DEPOSIT_NUMBER, DEPOSIT_TRAN_DATE, TOTAL_DEPOSIT_AMOUNT,    
  DEPOSIT_NOTE, IS_BNK_RECONCILED, IN_BNK_RECON, DEPOSIT_TYPE,    
  IS_COMMITED, DATE_COMMITED, IS_ACTIVE, CREATED_BY, CREATED_DATETIME,  
  IS_CONFIRM,RECEIPT_MODE,RTL_FILE,ACCOUNT_BALANCE    
 )    
 VALUES    
 (    
  @DEPOSIT_ID, @GL_ID, @FISCAL_ID, @ACCOUNT_ID,    
  @DEPOSIT_NUMBER, @DEPOSIT_TRAN_DATE, @TOTAL_DEPOSIT_AMOUNT,    
  @DEPOSIT_NOTE, @IS_BNK_RECONCILED, @IN_BNK_RECON,  @DEPOSIT_TYPE,    
  'N', NULL, 'Y', @CREATED_BY, @CREATED_DATETIME,  
  'N',@RECEIPT_MODE,@RTL_FILE,@ACCOUNT_BALANCE    
 )    
  return 1    
END    
  
  
  
  
  
  
  
  
  
  
GO

