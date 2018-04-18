IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCreditCardSpool]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCreditCardSpool]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------  
Proc Name        : dbo.Proc_UpdateCreditCardSpool
Created by       : Ravinda Gupta 
Date             : 03-23-227
Purpose      	 : Update status of record in  CC Spool Table
Revison History :  
Used In   :Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.Proc_UpdateCreditCardSpool
CREATE PROC dbo.Proc_UpdateCreditCardSpool
(
@SPOOL_ID Int,
@REF_DEPOSIT_ID Int = null,
@REF_DEP_DETAIL_ID Int = Null,
@PROCESSED	Char(1) = null,
@ERROR_DESCRIPTION Varchar(255) = null ,
@PAYPALREFID	Varchar(50)  = Null,
@PAYPALRESULT	Varchar(200)  = Null
)
AS
BEGIN 

DECLARE @SQL Varchar(8000)

SET @SQL = 'UPDATE EOD_CREDIT_CARD_SPOOL SET PROCESSED_DATETIME = GETDATE(), '

IF(@REF_DEPOSIT_ID IS NOT  null)
BEGIN 
	SET @SQL = @SQL + 'REF_DEPOSIT_ID = ' + CONVERT(VARCHAR, @REF_DEPOSIT_ID )  + ',' 
END
IF(@REF_DEP_DETAIL_ID IS NOT null)
BEGIN 
	SET @SQL = @SQL + 'REF_DEP_DETAIL_ID =' + CONVERT(VARCHAR,@REF_DEP_DETAIL_ID) + ','
END
IF (@ERROR_DESCRIPTION IS NOT null)
BEGIN 
	SET @SQL = @SQL + 'ERROR_DESCRIPTION =''' +  @ERROR_DESCRIPTION + ''','
END


IF (@PAYPALRESULT IS NOT null)
BEGIN 
	SET @SQL = @SQL + 'PAYPAL_RESULT =''' +  @PAYPALRESULT + ''','
END
IF (@PAYPALREFID IS NOT null)
BEGIN 
	SET @SQL = @SQL + 'PAY_PAL_REF_ID =''' +  @PAYPALREFID + ''','
END
SET @SQL = @SQL + ' PROCESSED = ''' +  @PROCESSED  + ''' WHERE IDEN_ROW_ID = ' +  CONVERT(VARCHAR,@SPOOL_ID )

EXECUTE(@SQL) 


END





GO

