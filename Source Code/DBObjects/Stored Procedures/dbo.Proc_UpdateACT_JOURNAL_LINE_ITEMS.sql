IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_JOURNAL_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_JOURNAL_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateACT_JOURNAL_LINE_ITEMS  
Created by      : Vijay Joshi  
Date            : 6/9/2005  
Purpose     :Insert values in Journal Entry Details table  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- DROP PROC dbo.Proc_UpdateACT_JOURNAL_LINE_ITEMS  
CREATE PROC dbo.Proc_UpdateACT_JOURNAL_LINE_ITEMS  
(  
 @JE_LINE_ITEM_ID  int,  
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
 @MODIFIED_BY      int,  
 @LAST_UPDATED_DATETIME  datetime,
 @POLICY_NUMBER NVARCHAR(10) =null,
 @TRAN_CODE varchar(20) = NULL 
)  
AS  
BEGIN  
 --Updating only if recors in Journal Entry Master is not commited  
 If (SELECT Upper(IS_COMMITED) FROM ACT_JOURNAL_MASTER WHERE JOURNAL_ID = @JOURNAL_ID) = 'Y'  
 BEGIN  
  return -2  
 END  

--Check AB type policy
IF(@TYPE = 'CUS')
BEGIN
	IF (SELECT BILL_TYPE FROM POL_CUSTOMER_POLICY_LIST 
	WHERE CUStOMER_ID=@REGARDING AND 
	 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID) = 'AB'
	BEGIN
		RETURN -5
	END
END

--Check AGency corresponding to Policy
IF(@TYPE = 'AGN')
BEGIN
	SET @POLICY_NUMBER = SUBSTRING(@POLICY_NUMBER,1,8)
	IF NOT EXISTS (
		SELECT M.AGENCY_ID FROM MNT_AGENCY_LIST M 
		INNER JOIN POL_CUSTOMER_POLICY_LIST P 
		ON P.AGENCY_ID = M.AGENCY_ID
		WHERE 
		M.AGENCY_ID=@REGARDING AND P.POLICY_ID=@POLICY_ID AND P.POLICY_NUMBER=@POLICY_NUMBER 
                AND P.POLICY_VERSION_ID = @POLICY_VERSION_ID
	)
	BEGIN
		RETURN -8
	END
END
IF(@TYPE = 'CUS')
BEGIN
	SET @POLICY_NUMBER = SUBSTRING(@POLICY_NUMBER,1,8)
	IF NOT EXISTS (
		SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST P 
		WHERE P.CUSTOMER_ID=@REGARDING AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID = @POLICY_VERSION_ID 
		AND POLICY_NUMBER=@POLICY_NUMBER
	)
	BEGIN
		RETURN -7
	END
END
   
   
 UPDATE ACT_JOURNAL_LINE_ITEMS  
 SET JOURNAL_ID = @JOURNAL_ID ,   
  DIV_ID = @DIV_ID ,   
  DEPT_ID = @DEPT_ID ,  
  PC_ID = @PC_ID ,   
  CUSTOMER_ID = @CUSTOMER_ID ,   
  POLICY_ID = @POLICY_ID ,   
  POLICY_VERSION_ID = @POLICY_VERSION_ID ,  
  AMOUNT = @AMOUNT ,   
  TYPE = @TYPE ,   
  REGARDING = @REGARDING ,   
  REF_CUSTOMER = @REF_CUSTOMER ,   
  ACCOUNT_ID = @ACCOUNT_ID ,  
  BILL_TYPE = @BILL_TYPE ,   
  NOTE = @NOTE ,
  POLICY_NUMBER = @POLICY_NUMBER,   
  MODIFIED_BY = @MODIFIED_BY,   
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,
  TRAN_CODE = @TRAN_CODE  
 WHERE JE_LINE_ITEM_ID = @JE_LINE_ITEM_ID  
 return 1  
  
END  





GO

