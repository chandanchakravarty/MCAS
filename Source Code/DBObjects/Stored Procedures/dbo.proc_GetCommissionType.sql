IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetCommissionType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_GetCommissionType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                
Proc Name       : dbo.proc_getCommissionType
Created by      : Lalit Chauhan    
Date            : 06/03/2010                
Purpose         :Fetch records From POL_REMUNERATION Table.                
Revison History :                
Used In        : Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC proc_getCommissionType 'COM',0,2
 
/****** Script for POL_REMUNERATION into DATABASE  ******/  
Create Proc [dbo].[proc_GetCommissionType] 
(
@Tran_Type Nvarchar(6) ,
@LOB_ID INT = NULL,
@LANG_ID INT = 1
)
AS
BEGIN
 IF(@LOB_ID IS NOT NULL and @LOB_ID!=0)
	  BEGIN
			DECLARE @QUERY NVARCHAR(500)
			SELECT @QUERY = 'SELECT ATC.TRAN_ID,ATC.TRAN_CODE,ISNULL(ATCM.DISPLAY_DESCRIPTION, ATC.DISPLAY_DESCRIPTION ) AS DISPLAY_DESCRIPTION FROM ACT_TRANSACTION_CODES ATC WITH(NOLOCK) 
							LEFT OUTER JOIN  ACT_TRANSACTION_CODES_MULTILINGUAL ATCM ON (ATCM.TRAN_ID=ATC.TRAN_ID AND ATCM.LANG_ID='''+convert(nvarchar(5),@LANG_ID)+''') WHERE ATC.TRAN_TYPE='''+@Tran_Type+''' AND ATC.TRAN_ID
			 IN(' + (SELECT  ISNULL(APPLICABLE_COMMISSION,'') FROM MNT_LOB_MASTER WITH(NOLOCK) WHERE  LOB_ID = @LOB_ID ) + ')'
			
			EXEC (@QUERY)
	  END
  ELSE
		BEGIN
		  SELECT ATC.TRAN_ID,ATC.TRAN_CODE,ISNULL(ATCM.DISPLAY_DESCRIPTION, ATC.DISPLAY_DESCRIPTION ) AS DISPLAY_DESCRIPTION
		  FROM ACT_TRANSACTION_CODES ATC WITH(NOLOCK) 
			   LEFT OUTER JOIN  ACT_TRANSACTION_CODES_MULTILINGUAL ATCM WITH(NOLOCK)  ON 
			   ATCM.TRAN_ID=ATC.TRAN_ID AND ATCM.LANG_ID=@LANG_ID
		  WHERE TRAN_TYPE=@Tran_Type
	    END

END


GO

