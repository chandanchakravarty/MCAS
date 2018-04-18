IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_CURRENT_DEPOSITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_CURRENT_DEPOSITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------            
Proc Name       : Proc_GetACT_CURRENT_DEPOSITS            
Created by      : Vijay Joshi            
Date            : 22 june,2005            
Purpose     : To Get the deposit information            
Revison History :            
Used In  : Wolverine            
    
Reviewed by : Anurag Verma    
Reviewed On : 16-07-2007    
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--  exec Proc_GetACT_CURRENT_DEPOSITS 50, 2       
-- drop proc dbo.Proc_GetACT_CURRENT_DEPOSITS            
CREATE PROC [dbo].[Proc_GetACT_CURRENT_DEPOSITS]            
(            
 @DEPOSIT_ID int   ,
 @LANG_ID INT=NULL         
)            
AS            
BEGIN            
DECLARE @Temp_TOTAL_AGENCY VARCHAR(10)     
   
 SELECT DEPOSIT_ID, GL_ID,FISCAL_ID,             
 ACCOUNT_ID, DEPOSIT_NUMBER, DEPOSIT_TRAN_DATE,             
 dbo.fun_FormatCurrency ( TOTAL_DEPOSIT_AMOUNT, @LANG_ID) TOTAL_DEPOSIT_AMOUNT,
 --convert(varchar(30),convert(money,TOTAL_DEPOSIT_AMOUNT),1) TOTAL_DEPOSIT_AMOUNT,  
 DEPOSIT_NOTE, IS_BNK_RECONCILED, DEPOSIT_TYPE, 
 dbo.fun_FormatCurrency ( TAPE_TOTAL_CUST, @LANG_ID) TAPE_TOTAL_CUST,
 --convert(varchar(30),convert(money,TAPE_TOTAL_CUST),1) TAPE_TOTAL_CUST,  
 --isnull(TAPE_TOTAL_CLM,0)TAPE_TOTAL_CLM,isnull(TAPE_TOTAL_MISC,0)TAPE_TOTAL_MISC,  
 dbo.fun_FormatCurrency ( TAPE_TOTAL_CLM, @LANG_ID) TAPE_TOTAL_CLM, 
-- convert(varchar(30),convert(money,TAPE_TOTAL_CLM),1) TAPE_TOTAL_CLM,  
 --isnull(isnull(TOTAL_DEPOSIT_AMOUNT,0) - (isnull(TAPE_TOTAL_CUST,0) + isnull(TAPE_TOTAL_CLM,0) +  isnull(TAPE_TOTAL_MISC,0)),0) TOTAL_AGENCY,        
 
 dbo.fun_FormatCurrency (isnull(isnull(TOTAL_DEPOSIT_AMOUNT,0) - (isnull(TAPE_TOTAL_CUST,0) + isnull(TAPE_TOTAL_CLM,0) +  isnull(TAPE_TOTAL_MISC,0)),0), @LANG_ID) TOTAL_AGENCY ,
 
 --convert(varchar(30),convert(money,isnull(isnull(TOTAL_DEPOSIT_AMOUNT,0) - (isnull(TAPE_TOTAL_CUST,0) + isnull(TAPE_TOTAL_CLM,0) +  isnull(TAPE_TOTAL_MISC,0)),0)),1) TOTAL_AGENCY,  
 
 IN_BNK_RECON, IS_COMMITED, DATE_COMMITED,           
 CREATED_DATETIME,              
 MODIFIED_BY, LAST_UPDATED_DATETIME, 
  dbo.fun_FormatCurrency ( TAPE_TOTAL_MISC, @LANG_ID) TAPE_TOTAL_MISC, 
  
 --convert(varchar(30),convert(money,TAPE_TOTAL_MISC),1) TAPE_TOTAL_MISC,  
    IS_CONFIRM,RECEIPT_MODE,        
 (            
 SELECT COUNT(DEPOSIT_ID) FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS  (NOLOCK)          
 WHERE DEPOSIT_ID = ACT_CURRENT_DEPOSITS.DEPOSIT_ID            
 ) NO_OF_LINE_ITEMS,RTL_FILE,  
 --RTL CHECK  
 (  
 select count(li.DEPOSIT_ID) from ACT_CURRENT_DEPOSIT_LINE_ITEMS LI  
 inner join ACT_CURRENT_DEPOSITS  CD on   
 cd.DEPOSIT_ID = li.DEPOSIT_ID  
 where li.DEPOSIT_TYPE = 'CUST'  
 and li.created_from='RTL'  
 and li.deposit_id = ACT_CURRENT_DEPOSITS.DEPOSIT_ID  
 )  RTL_LINE_ITEMS,           
 --ACCOUNT_BALANCE   
 dbo.fun_FormatCurrency ( ACCOUNT_BALANCE, @LANG_ID) ACCOUNT_BALANCE 
 --convert(varchar(30),convert(money,ACCOUNT_BALANCE),1) ACCOUNT_BALANCE  
 FROM ACT_CURRENT_DEPOSITS  (NOLOCK)      
 WHERE DEPOSIT_ID = @DEPOSIT_ID            
END            
            
  
  
  
  
  

GO

