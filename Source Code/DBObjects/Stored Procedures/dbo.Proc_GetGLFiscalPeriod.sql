IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGLFiscalPeriod]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGLFiscalPeriod]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



  /*  
CREATED BY: AJIT ON 25/07/2005  
pURPOSE: TO GET FISCAL PERIOD OF A FISCAL ID  
drop proc dbo.Proc_GetGLFiscalPeriod 
*/  
CREATE PROCEDURE [dbo].[Proc_GetGLFiscalPeriod]  
@FISCAL_ID INT  
AS  
BEGIN  
-- //SELECTING FISCAL PERIOD  
 SELECT FISCAL_BEGIN_DATE,  
 FISCAL_END_DATE,  
 POSTING_LOCK_DATE  
 FROM ACT_GENERAL_LEDGER  
 WHERE FISCAL_ID  = @FISCAL_ID  
END  
  
  
  
  
  
  
  
  


GO

