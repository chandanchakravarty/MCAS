IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTransactionCodeDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTransactionCodeDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : Proc_GetTransactionCodeDetailsXML
Created by      : Ashwani Sharma
Date            : 25th May 2006
Purpose    	: To get record for xml.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_GetTransactionCodeDetails
(
	@TRAN_GROUP_ID int,
	@PAGE_SIZE int,  
	@CURRENT_PAGE_INDEX int  
)
as
begin
	DECLARE @STARTINDEX int   
	DECLARE @ENDPAGEINDEX int   
  
	SET @STARTINDEX =  ((@CURRENT_PAGE_INDEX - 1 ) * @PAGE_SIZE) + 1  
	SET @ENDPAGEINDEX = @STARTINDEX + @PAGE_SIZE  
  
	SELECT COUNT(1)  
	FROM ACT_TRAN_CODE_GROUP_DETAILS  
	WHERE (TRAN_GROUP_ID  = @TRAN_GROUP_ID)

	CREATE TABLE #ACT_TRAN_CODE_GROUP_DETAILS
	(
		[IDENT_COL] 	Int Identity(1,1),  
		DETAIL_ID	smallint,
		TRAN_ID		smallint,
		DEF_SEQ		smallint,
		IS_ACTIVE	char(1), 
           	TRAN_CODE	nvarchar(5), 
		DISPLAY_DESCRIPTION nvarchar(150)
	)

	INSERT INTO #ACT_TRAN_CODE_GROUP_DETAILS
	(
		DETAIL_ID,
		TRAN_ID,
		DEF_SEQ,
		IS_ACTIVE,
           	TRAN_CODE,
		DISPLAY_DESCRIPTION
	)
	SELECT  ACT_TRAN_CODE_GROUP_DETAILS.DETAIL_ID, ACT_TRAN_CODE_GROUP_DETAILS.TRAN_ID, 
           	ACT_TRAN_CODE_GROUP_DETAILS.DEF_SEQ, ACT_TRAN_CODE_GROUP_DETAILS.IS_ACTIVE, 
           	ACT_TRANSACTION_CODES.TRAN_CODE, ACT_TRANSACTION_CODES.DISPLAY_DESCRIPTION
	FROM    ACT_TRAN_CODE_GROUP_DETAILS 
	INNER JOIN ACT_TRANSACTION_CODES ON ACT_TRAN_CODE_GROUP_DETAILS.TRAN_ID = ACT_TRANSACTION_CODES.TRAN_ID
	where (ACT_TRAN_CODE_GROUP_DETAILS.TRAN_GROUP_ID  = @TRAN_GROUP_ID)

	  
	SELECT * FROM #ACT_TRAN_CODE_GROUP_DETAILS
	WHERE IDENT_COL >= @STARTINDEX AND  
      	IDENT_COL <  @ENDPAGEINDEX   

	DROP TABLE #ACT_TRAN_CODE_GROUP_DETAILS

end











GO

