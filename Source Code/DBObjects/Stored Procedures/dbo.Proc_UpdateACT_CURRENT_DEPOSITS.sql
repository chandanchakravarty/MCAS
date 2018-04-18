IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_CURRENT_DEPOSITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_CURRENT_DEPOSITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name       : dbo.Proc_InsertACT_CURRENT_DEPOSITS      
Created by      : Vijay Joshi      
Date            : 6/20/2005      
Purpose     : To update the value in ACT_CURRENT_DEPOSITS      
Revison History :      
Used In  : Wolverine      
    
Reviewed By : Anurag Verma    
Reviewed On : 16-07-2007    
    
------------------------------------------------------------      
Date     Review By          Comments      
------------------------------------------------------------*/      
-- drop proc dbo.Proc_UpdateACT_CURRENT_DEPOSITS      
CREATE PROC [dbo].[Proc_UpdateACT_CURRENT_DEPOSITS]      
(      
 @DEPOSIT_ID      int,      
 @GL_ID       int,      
 @FISCAL_ID       smallint,      
 @ACCOUNT_ID      int,      
 @DEPOSIT_NUMBER  int,      
 @DEPOSIT_TRAN_DATE datetime,      
 @TOTAL_DEPOSIT_AMOUNT   decimal(18,2),      
 @DEPOSIT_NOTE   nvarchar(510),      
 @IS_BNK_RECONCILED     nchar(2),      
 @IN_BNK_RECON   int,  
 @DEPOSIT_TYPE   varchar(6),    
 @MODIFIED_BY  int,      
 @LAST_UPDATED_DATETIME datetime,    
 @RECEIPT_MODE smallint = null,    
 @RTL_FILE NVARCHAR(250) = null,    
 @ACCOUNT_BALANCE DECIMAL(18,2)      
)      
AS      
BEGIN      
      
 If (SELECT Upper(IS_COMMITED) FROM ACT_CURRENT_DEPOSITS       
   WHERE DEPOSIT_ID = @DEPOSIT_ID) = 'Y'      
 BEGIN      
  /*Record is commited hence can not be update hence existing with status -2*/      
  return -2      
 END       
      
 If Exists(SELECT DEPOSIT_NUMBER FROM ACT_CURRENT_DEPOSITS       
   WHERE DEPOSIT_NUMBER = @DEPOSIT_NUMBER AND      
   ACCOUNT_ID = @ACCOUNT_ID AND      
   DEPOSIT_ID <> @DEPOSIT_ID)      
 BEGIN      
  /*Duplicate deposit number exist hence existing with status -2*/      
  return -1      
 END      
      
 SELECT @GL_ID = GL_ID FROM ACT_GENERAL_LEDGER WHERE FISCAL_ID = @FISCAL_ID       
      
 UPDATE ACT_CURRENT_DEPOSITS      
 SET DEPOSIT_ID = @DEPOSIT_ID,       
  GL_ID = @GL_ID,       
  FISCAL_ID = @FISCAL_ID,       
  ACCOUNT_ID = @ACCOUNT_ID,      
  DEPOSIT_NUMBER = @DEPOSIT_NUMBER,      
  DEPOSIT_TRAN_DATE = @DEPOSIT_TRAN_DATE,       
--  TOTAL_DEPOSIT_AMOUNT = @TOTAL_DEPOSIT_AMOUNT,      
  DEPOSIT_NOTE = @DEPOSIT_NOTE,       
  IS_BNK_RECONCILED = @IS_BNK_RECONCILED,       
  IN_BNK_RECON = @IN_BNK_RECON, 
  DEPOSIT_TYPE = @DEPOSIT_TYPE,     
  MODIFIED_BY = @MODIFIED_BY,       
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,    
  RECEIPT_MODE = @RECEIPT_MODE,    
  RTL_FILE = @RTL_FILE ,   
  ACCOUNT_BALANCE = @ACCOUNT_BALANCE    
 WHERE DEPOSIT_ID = @DEPOSIT_ID      
       
 return 1      
END      
      
    
    
    
    
    
    
    
    
    
    
    
    
GO

