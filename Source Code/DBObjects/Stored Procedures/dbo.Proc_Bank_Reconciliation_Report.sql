IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Bank_Reconciliation_Report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Bank_Reconciliation_Report]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--  BEGIN TRAN        
--        
--  DROP PROC [dbo].[Proc_Bank_Reconciliation_Report]       
--GO        
create proc [dbo].[Proc_Bank_Reconciliation_Report]            
(          
@REF_FILE_ID int  ,        
@MATCHED_RECORD_STATUS INT = NULL        
)          
AS          
BEGIN          
if(@MATCHED_RECORD_STATUS = -1)      
 set @MATCHED_RECORD_STATUS = null      
      
 DECLARE @QRY VARCHAR(8000)        
 --Added Check_amount and order by Check_amount for Itrack Issue #5517    
 SET @QRY = '        
SELECT        
 ISNULL(ABRCF.ACCOUNT_NUMBER,'''') AS [ACCOUNT_NUMBER],        
 ISNULL(ABRCF.SERIAL_NUMBER,'''') AS [SERIAL_NUMBER],        
 ISNULL(ABRCF.CHECK_DATE,'''') AS [CHECK_DATE],        
 ISNULL(ABRCF.ADDITIONAL_DATA,'''') AS [ADDITIONAL_DATA],        
 ISNULL(ABRCF.SEQUENCE_NUMBER,'''') AS [SEQUENCE_NUMBER],        
 ISNULL(ABRCF.ERROR_DESC,'''') AS [ERROR_DESC],      
  CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(AMOUNT,0),1),1)  AS CHECK_AMOUNT,          
 CASE         
 ISNULL(ABRCF.MATCHED_RECORD_STATUS,0) WHEN 0 THEN ''UNMATCHED''         
 WHEN 1 THEN ''MATCHED'' END AS MATCHED_RECORD_STATUS         
FROM ACT_BANK_RECON_CHECK_FILE ABRCF  LEFT JOIN        
ACT_BANK_RECON_UPLOAD_FILE ABRUF ON  ISNULL(ABRCF.RECON_GROUP_ID,0) = ISNULL(ABRUF.AC_RECONCILIATION_ID,0)'     
        
IF (@MATCHED_RECORD_STATUS IS NOT NULL)      
 SET @QRY = @QRY + 'WHERE ABRUF.FILE_ID =  '+ CAST(@REF_FILE_ID AS varchar) +  ' AND  ABRCF.MATCHED_RECORD_STATUS = ' + cast(@MATCHED_RECORD_STATUS as varchar) + 'ORDER BY SERIAL_NUMBER'           
ELSE      
 SET @QRY = @QRY + 'WHERE ABRUF.FILE_ID =  ' +  CAST(@REF_FILE_ID AS varchar) + 'ORDER BY SERIAL_NUMBER'       
     
        
        
print @QRY        
exec (@QRY)      
        
end        
        
----        
--  go        
--EXEC Proc_Bank_Reconciliation_Report 313,-1        
----        
--rollback tran        
------          
      
--select * from ACT_BANK_RECON_CHECK_FILE      
        
          
          
GO

