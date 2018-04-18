IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyACT_JOURNAL_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyACT_JOURNAL_MASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : Dbo.Proc_CopyACT_JOURNAL_MASTER        
Created by      : Vijay Joshi        
Date            : 10/June/2005        
Purpose     : Insert values in Journal Entry Details table        
Revison History :        
Used In  : Wolverine        
      
Modified By : Vijay Arora      
Modified Date : 05-04-2006      
Purpose  : Copy records only when line items has > 0 records. 

Modified By : Uday Shanker      
Modified Date : 14/Dec/2007      
Purpose  : Copy records only when line items has > 0 records.     
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- drop proc dbo.Proc_CopyACT_JOURNAL_MASTER        
create PROC dbo.Proc_CopyACT_JOURNAL_MASTER        
(        
 @JOURNAL_ID      int,        
 @CREATED_BY  Int,  
 @NEW_JOURNAL_ID  int output,  
 @NEW_JOURNAL_ENTRY_NO int output    
)        
AS        
BEGIN        
      
DECLARE @COUNT_LINE_ITEMS INT       
      
SELECT @COUNT_LINE_ITEMS = COUNT(JOURNAL_ID) FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = @JOURNAL_ID      
      
IF @COUNT_LINE_ITEMS > 0       
BEGIN      
      
 Declare @JOURNAL_ENTRY_NO numeric        
 /*Retreiving the maximim journal id*/        
 SELECT @NEW_JOURNAL_ID = IsNull(Max(JOURNAL_ID), 0) + 1, @JOURNAL_ENTRY_NO = IsNull(Max(Convert(numeric,JOURNAL_ENTRY_NO)), 0) + 1        
 FROM ACT_JOURNAL_MASTER        
         
 INSERT INTO ACT_JOURNAL_MASTER        
 (        
  JOURNAL_ID, JOURNAL_GROUP_TYPE,        
  TRANS_DATE, JOURNAL_GROUP_CODE,        
  JOURNAL_ENTRY_NO, [DESCRIPTION], DIV_ID, DEPT_ID, PC_ID, GL_ID,        
  FISCAL_ID, FREQUENCY, START_DATE, END_DATE, DAY_OF_THE_WK,        
  LAST_PROCESSED_DATE, IS_COMMITED, DATE_COMMITED, IMPORTSTATUS,        
  LAST_VALID_POSTING_DATE, NO_OF_RUN,         
  IS_ACTIVE, CREATED_BY, CREATED_DATETIME        
 )        
 SELECT        
         
  @NEW_JOURNAL_ID, CASE JOURNAL_GROUP_TYPE WHEN 'TP' THEN 'ML' ELSE JOURNAL_GROUP_TYPE END,        
  Getdate(), JOURNAL_GROUP_CODE,        
  @JOURNAL_ENTRY_NO, [DESCRIPTION], DIV_ID, DEPT_ID, PC_ID, GL_ID,        
  FISCAL_ID, FREQUENCY, START_DATE, END_DATE, DAY_OF_THE_WK,        
  LAST_PROCESSED_DATE, 'N', NULL, IMPORTSTATUS,        
  LAST_VALID_POSTING_DATE, NO_OF_RUN,        
  'Y', @CREATED_BY, Getdate()        
 FROM        
  ACT_JOURNAL_MASTER COPY_FROM        
 WHERE        
  COPY_FROM.JOURNAL_ID = @JOURNAL_ID        
        
 /*Inserting the child records*/        
        
 Declare @JE_LINE_ITEM_ID numeric        
 Declare @DIV_ID int, @DEPT_ID int, @PC_ID int, @CUSTOMER_ID int, @POLICY_ID int, @POLICY_VERSION_ID int,        
  @AMOUNT decimal(18,2), @TYPE nchar(5), @REGARDING VARCHAR(30), @REF_CUSTOMER int, @ACCOUNT_ID int,        
   @BILL_TYPE int, @NOTE nvarchar(100), @CREATED_DATETIME datetime,@TRAN_CODE varchar(20)    
        
 Declare Line_Items Cursor FOR         
  SELECT         
   DIV_ID, DEPT_ID,        
   PC_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,        
   AMOUNT, TYPE, REGARDING, REF_CUSTOMER, ACCOUNT_ID,        
   BILL_TYPE, NOTE,        
   Getdate(),TRAN_CODE      
  FROM         
   ACT_JOURNAL_LINE_ITEMS        
  WHERE        
   JOURNAL_ID = @JOURNAL_ID        
         
 Open Line_Items        
         
 While 1=1        
 BEGIN        
  Fetch Next From Line_ITems Into        
   @DIV_ID, @DEPT_ID,        
   @PC_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,        
   @AMOUNT, @TYPE, @REGARDING, @REF_CUSTOMER, @ACCOUNT_ID,        
   @BILL_TYPE, @NOTE,        
   @CREATED_DATETIME,@TRAN_CODE       
          
  IF @@FETCH_STATUS <> 0        
   break        
        
  /*Calling the insert sp for inserting the value*/         
  Exec dbo.Proc_InsertACT_JOURNAL_LINE_ITEMS        
   @JE_LINE_ITEM_ID, @NEW_JOURNAL_ID, @DIV_ID, @DEPT_ID,        
        @PC_ID, null, null, null,        
   @AMOUNT, @TYPE, null, null, @ACCOUNT_ID,        
   @BILL_TYPE, @NOTE, @CREATED_BY, @CREATED_DATETIME,null,@TRAN_CODE    
 END        
         
 Close Line_Items        
 Deallocate  Line_Items    
SET @NEW_JOURNAL_ENTRY_NO=@JOURNAL_ENTRY_NO      
END      
ELSE      
BEGIN      
SET @NEW_JOURNAL_ID = -1     
SET @NEW_JOURNAL_ENTRY_NO=-1  
END      
  
  
    
END  



GO

