IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_JOURNAL_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_JOURNAL_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.ACT_JOURNAL_LINE_ITEMS      
Created by      : Vijay Joshi      
Date            : 6/9/2005      
Purpose     :Insert values in Journal Entry Details table      
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
-- DROP PROC dbo.Proc_InsertACT_JOURNAL_LINE_ITEMS      
  
CREATE PROC dbo.Proc_InsertACT_JOURNAL_LINE_ITEMS      
(      
 @JE_LINE_ITEM_ID  int OUTPUT,      
 @JOURNAL_ID      int,      
 @DIV_ID       smallint,      
 @DEPT_ID       smallint,      
 @PC_ID        smallint,      
 @CUSTOMER_ID     int,      
 @POLICY_ID       smallint,      
 @POLICY_VERSION_ID  smallint,      
 @AMOUNT       decimal(18,2),      
 @TYPE        nvarchar(10),      
 @REGARDING       varchar(30),      
 @REF_CUSTOMER    int,      
 @ACCOUNT_ID      int,      
 @BILL_TYPE       nchar(4),      
 @NOTE        nvarchar(200),      
 @CREATED_BY      int,      
 @CREATED_DATETIME  datetime,    
 @POLICY_NUMBER NVARCHAR (10) =null,  
 @TRAN_CODE varchar(20) = NULL     
)      
AS


BEGIN      


IF @TYPE = 'OTH'
BEGIN
 SET @CUSTOMER_ID = 0
 SET @POLICY_ID = 0
 SET @POLICY_VERSION_ID = 0
END
       
 /*Retreiving the maximum journal entry id*/      
 SELECT @JE_LINE_ITEM_ID = IsNull(Max(JE_LINE_ITEM_ID), 0) + 1 FROM ACT_JOURNAL_LINE_ITEMS   

--Check AB type policy
IF(@TYPE = 'CUS')
BEGIN
	IF (SELECT BILL_TYPE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
	WHERE CUSTOMER_ID=@REGARDING AND 
	 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID) = 'AB'
	BEGIN
		RETURN -5
	END
END

      
 INSERT INTO ACT_JOURNAL_LINE_ITEMS      
 (      
  JE_LINE_ITEM_ID, JOURNAL_ID, DIV_ID, DEPT_ID,      
  PC_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,      
  AMOUNT, TYPE, REGARDING, REF_CUSTOMER, ACCOUNT_ID,      
  BILL_TYPE, NOTE,POLICY_NUMBER,       
  IS_ACTIVE, CREATED_BY, CREATED_DATETIME,TRAN_CODE      
 )      
 VALUES      
 (      
  @JE_LINE_ITEM_ID, @JOURNAL_ID, @DIV_ID, @DEPT_ID,      
  @PC_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,      
  @AMOUNT, @TYPE, @REGARDING, @REF_CUSTOMER, @ACCOUNT_ID,      
  @BILL_TYPE, @NOTE,@POLICY_NUMBER,      
  'Y', @CREATED_BY, @CREATED_DATETIME,@TRAN_CODE      
 )      
END


--SELECT * FROM ACT_JOURNAL_LINE_ITEMS ORDER BY JOURNAL_ID DESC






GO

