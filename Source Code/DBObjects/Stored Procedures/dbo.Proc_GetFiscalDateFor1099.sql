IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetFiscalDateFor1099]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetFiscalDateFor1099]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*--------------------------------------------------------
Proc Name       : dbo.Proc_GetFiscalDateFor1099                           
Created by      : Praveen Kasana                            
Date            : 02 feb 2009                        
Purpose         :                            
Revison History :                            
Used In			: Wolverine    
Description : 
EOY can only be processed for first previous year. 
So if current fiscal is 
‘1-1-2009’ to ’12-31-2009’ 1099 can only be run for previous fiscal 

i.e ‘1-1-2008’ to -12-31-2008 fiscal. 
                        

--drop proc dbo.Proc_GetFiscalDateFor1099            
------------------------------------------------------------  */
 CREATE proc dbo.Proc_GetFiscalDateFor1099
(  
 @FOR_DATE DATETIME,
 @YEAR INT OUT,
 @FISCAL_ID INT OUT ,
 @TEN99_PROCESSED int OUT,
 @TEN99_PROCESSING_DATE DATETIME OUT 
 
 
)  
AS  
BEGIN   


DECLARE @PREVIOUS_FISCAL_ID INT

 SELECT @FISCAL_ID = FISCAL_ID
 FROM ACT_GENERAL_LEDGER  
 WHERE CAST(CONVERT(VARCHAR,@FOR_DATE,101) AS Datetime)   
     >= CAST(CONVERT(VARCHAR,FISCAL_BEGIN_DATE,101) AS Datetime)  
 AND  CAST(CONVERT(VARCHAR,@FOR_DATE,101) AS Datetime)   
     <= CAST(CONVERT(VARCHAR,FISCAL_END_DATE,101) AS Datetime)  


SET @PREVIOUS_FISCAL_ID = @FISCAL_ID - 1
SET @FISCAL_ID = @PREVIOUS_FISCAL_ID --OUT

	
SELECT 
	@YEAR = YEAR(FISCAL_BEGIN_DATE), --OUT
	@TEN99_PROCESSED = ISNULL(TEN99_PROCESSED,0), --OUT
	@TEN99_PROCESSING_DATE = ISNULL(TEN99_PROCESSING_DATE,'') --OUT
FROM ACT_GENERAL_LEDGER with(nolock)
WHERE FISCAL_ID = @PREVIOUS_FISCAL_ID


END  
  
 ----test 
 --2009-01-22 12:30:38.440
--rollback tran
--declare @d datetime
--set @d ='2009-01-22' 
-- declare @TEN99_PROCESSED int  
-- declare @TEN99_PROCESSING_DATE DATETIME  
-- declare @YEAR INT 
-- declare @FISCAL_ID INT 
--
--exec Proc_GetFiscalDateFor1099  @d,@YEAR out,@FISCAL_ID out ,@TEN99_PROCESSED out,@TEN99_PROCESSING_DATE out
--select @YEAR ,@FISCAL_ID,@TEN99_PROCESSED,@TEN99_PROCESSING_DATE




GO

