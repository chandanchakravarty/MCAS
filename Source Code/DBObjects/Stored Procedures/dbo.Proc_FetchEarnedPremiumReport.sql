IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchEarnedPremiumReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchEarnedPremiumReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--begin tran
--drop proc [Dbo].[Proc_FetchEarnedPremiumReport]
--go 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------                  
 Proc Name       : Dbo.Proc_FetchEarnedPremiumReport  
 Created by      : Ravindra   
 Date            : 06-25-2007  
 Modified by     : Arun   
 Date            : 01-09-2008  
 Purpose         : Fetch Earned premium report for particular month year   
Modified by     : Pravesh  
 Date            : 15 may-2008  
 Purpose         : add coverage extra table  
  
 Revison History :                  
 Used In       : Wolverine   (Reporting)  
  
exec Proc_FetchEarnedPremiumReport 12,2007  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
-- drop proc Dbo.Proc_FetchEarnedPremiumReport  
CREATE PROC [dbo].[Proc_FetchEarnedPremiumReport]  
(                  
 @MONTH    SMALLINT,  
 @YEAR     INT  
)                  
AS                 
BEGIN   
  
-- Ravindra(12-05-2007): Earned premium will be processed at month end  
-- -- If earned premium report has not processed yet for concerned month & year process it  
-- IF NOT EXISTS ( SELECT IDEN_ROW_ID FROM ACT_EARNED_PREMIUM   
--   WHERE MONTH_NUMBER = @MONTH   
--   AND   YEAR_NUMBER  = @YEAR )  
-- BEGIN   
--  exec Proc_ProcessEarnedPremiumReport @MONTH , @YEAR   
-- END  
  
SELECT   
--ISNULL(MNTC.COV_CODE,'') as COV_CODE,  
CASE WHEN MNTC.COV_CODE IS NULL THEN ISNULL(MNTC2.COV_CODE,'') ELSE ISNULL(MNTC.COV_CODE,'') END as COV_CODE,  
CASE  WHEN MNTC.COV_DES IS NULL THEN ISNULL(MNTC2.COV_DES,'') ELSE ISNULL(MNTC.COV_DES,'') END as COV_DES,    
 TMP.POLICY_NUMBER ,     
 AGN.AGENCY_CODE,    
 STATE.STATE_CODE,    
 TMP.POLICY_EFFECTIVE_DATE ,    
    TMP.POLICY_EXPIRATION_DATE ,    
 TMP.TRAN_EFFECTIVE_DATE ,    
    ISNULL(PRC.PROCESS_SHORTNAME ,'') AS TRANSACTION_CODE,    
   --Null check added for Itrack issue #5578.
    SUM(ISNULL(TMP.INFORCE_PREMIUM,0)) AS  INFORCE_PREMIUM,    
    SUM(ISNULL(TMP.BEGINNING_UNEARNED,0)) AS  BEGINNING_UNEARNED ,    
    SUM(ISNULL(TMP.WRITTEN_PREMIUM,0)) AS WRITTEN_PREMIUM ,    
    SUM(ISNULL(TMP.ENDING_UNEARNED,0)) AS  ENDING_UNEARNED,    
    SUM(ISNULL(TMP.EARNED_PREMIUM,0)) AS  EARNED_PREMIUM ,
   --Policy_Term added For Itrack Issue #5824. 
   TMP.POLICY_TERM   
FROM    ACT_EARNED_PREMIUM TMP     
INNER JOIN MNT_AGENCY_LIST AGN     
 ON TMP.AGENCY_ID = AGN.AGENCY_ID    
LEFT OUTER JOIN MNT_COVERAGE MNTC     
  ON TMP.COVERAGEID = MNTC.COV_ID      
    
LEFT OUTER JOIN MNT_COVERAGE_EXTRA MNTC2     
  ON TMP.COVERAGEID = MNTC2.COV_ID      
    
INNER JOIN MNT_COUNTRY_STATE_LIST STATE    
 ON TMP.STATE_ID = STATE.STATE_ID    
LEFT JOIN POL_PROCESS_MASTER PRC    
 ON TMP.PROCESS_ID = PRC.PROCESS_ID     
    
WHERE TMP.MONTH_NUMBER = @MONTH     
AND   TMP.YEAR_NUMBER  = @YEAR    
-- and TMP.INFORCE_PREMIUM <> 0   
AND NOT ( TMP.INFORCE_PREMIUM =  0  AND TMP.WRITTEN_PREMIUM = 0 AND TMP.EARNED_PREMIUM = 0 )  
--ORDER BY TMP.IDEN_ROW_ID    
GROUP BY   
CASE WHEN MNTC.COV_CODE IS NULL THEN ISNULL(MNTC2.COV_CODE,'') ELSE ISNULL(MNTC.COV_CODE,'') END ,  
CASE  WHEN MNTC.COV_DES IS NULL THEN ISNULL(MNTC2.COV_DES,'') ELSE ISNULL(MNTC.COV_DES,'') END ,    
 TMP.POLICY_NUMBER ,     
 AGN.AGENCY_CODE,    
 STATE.STATE_CODE,    
 TMP.POLICY_EFFECTIVE_DATE ,    
    TMP.POLICY_EXPIRATION_DATE ,    
 TMP.TRAN_EFFECTIVE_DATE , 
--Policy_term added For Itrack issue #5824 
TMP.POLICY_TERM ,    
    ISNULL(PRC.PROCESS_SHORTNAME ,'')    
ORDER BY TMP.POLICY_NUMBER,COV_CODE    
  
END  
--go
--exec Proc_FetchEarnedPremiumReport 1,2009
--rollback tran



GO

