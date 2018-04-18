IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchAgencyChecksPayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchAgencyChecksPayments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran       
--drop proc dbo.Proc_FetchAgencyChecksPayments          
--go       
      
create  PROC [dbo].[Proc_FetchAgencyChecksPayments]         
(              
 @MONTH  INT =null,              
 @YEAR  INT  =null,            
 @AGENCY_ID INT = NULL,        
 @COMM_TYPE VARCHAR(20),        
 @CARRIER_SYSTEM_ID varchar(15) = null        
)              
AS              
BEGIN            
 DECLARE @YES Int,        
   @No Int,        
   @EFT_MODE Int,        
   @CHECK_MODE Int        
      
 SET @YES = 10963        
 SET @NO  = 10964        
 SET @EFT_MODE = 11976        
 SET @CHECK_MODE = 11787--11975        
          
 IF(@COMM_TYPE =  'CAC')        
 BEGIN         
  -- fetches items acc to the search criteria whether saved or not        
   SELECT        
   USR.USER_ID AS PAYEE_ENTITY_ID ,       
   ISNULL(USR.USER_FNAME,'') + ' ' + ISNULL(USR.USER_LNAME,'') AS ENTITY_NAME ,       
   CASE ISNULL(ACI.CHECK_ID,0)         
   WHEN 0 THEN 'False'        
   else  'True' END as SELECTED_ITEMS ,        
   ACI.CHECK_ID,         
   AAS.AGENCY_ID,      
   AGENCY.AGENCY_CODE AS AGENCY_CODE,        
   AAS.MONTH_NUMBER AS [MONTH],AAS.MONTH_YEAR AS [YEAR],        
   CONVERT(VARCHAR(30),AAS.MONTH_NUMBER) + ' ' +  CONVERT(VARCHAR(20),AAS.MONTH_YEAR) AS MONTHYEAR,         
   convert(varchar(30),convert(money,SUM(ISNULL(AAS.DUE_AMOUNT,0))  * -1) ,1) STMT_AMOUNT,           
   convert(varchar(30),convert(money,(SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0))) * -1) ,1) BALANCE_AMOUNT,         
   convert(varchar(30),convert(money,ACI.CHECK_AMOUNT),1) PAYMENT_AMOUNT,
   --raghav 
   ISNULL(AGENCY.REQ_SPECIAL_HANDLING,'10964') AS  REQ_SPECIAL_HANDLING ,             
    @CHECK_MODE AS PAYMENT_MODE ,        
   'NO' AS ALLOW_EFT,        
   'Distribute' AS DISTRIBUTE,        
   ISNULL(ACI.IS_RECONCILED,'N') as IS_RECONCILED        
   FROM ACT_AGENCY_STATEMENT AAS         
   INNER JOIN MNT_AGENCY_LIST AGENCY       
    ON AGENCY.AGENCY_ID = AAS.AGENCY_ID        
   INNER JOIN POL_CUSTOMER_POLICY_LIST CPL      
    ON CPL.AGENCY_ID  = AAS.AGENCY_ID       
    AND CPL.CUSTOMER_ID = AAS.CUSTOMER_ID       
    AND CPL.POLICY_ID = AAS.POLICY_ID       
    AND CPL.POLICY_VERSION_ID = AAS.POLICY_VERSION_ID       
   INNER JOIN MNT_USER_LIST USR      
   ON AAS.CSR_ID = USR.USER_ID       
   LEFT JOIN TEMP_ACT_CHECK_INFORMATION ACI         
    ON USR.USER_ID= ACI.PAYEE_ENTITY_ID        
    AND AAS.MONTH_NUMBER = ACI.[MONTH]        
    AND AAS.MONTH_YEAR = ACI.[YEAR]       
    AND AAS.AGENCY_ID = ACI.AGENCY_ID      
   WHERE         
   AAS.MONTH_NUMBER = @MONTH          
   AND AAS.MONTH_YEAR = @YEAR         
   AND AAS.COMM_TYPE=@COMM_TYPE         
   AND AAS.AGENCY_ID = CASE @AGENCY_ID WHEN 0 THEN AAS.AGENCY_ID ELSE  @AGENCY_ID END         
   AND ISNULL(AAS.IS_CHECK_CREATED,'N') <> 'Y'        
   GROUP BY AAS.AGENCY_ID,AAS.MONTH_NUMBER,AAS.MONTH_YEAR, AGENCY.REQ_SPECIAL_HANDLING,       
   AGENCY.AGENCY_CODE,ACI.CHECK_ID,ACI.IS_RECONCILED,        
   ACI.CHECK_AMOUNT,USR.USER_FNAME , USR.USER_LNAME ,USR.USER_ID      
   HAVING (SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0)))< 0        
         
   UNION          
      
   -- fetches selected(checked) items        
   SELECT       
   USR.USER_ID AS PAYEE_ENTITY_ID ,       
   ISNULL(USR.USER_FNAME,'') + ' ' + ISNULL(USR.USER_LNAME,'') AS ENTITY_NAME ,       
   CASE ISNULL(CHK.CHECK_ID,0)         
   WHEN 0 THEN 'False'        
   else  'True' END as SELECTED_ITEMS ,        
   CHK.CHECK_ID,         
   CHK.AGENCY_ID,      
   AGN.AGENCY_CODE AS AGENCY_CODE,        
   CHK.MONTH AS [MONTH],CHK.YEAR AS [YEAR],        
   CONVERT(VARCHAR(30),CHK.MONTH) + ' ' +  CONVERT(VARCHAR(20),CHK.YEAR) AS MONTHYEAR,        
   convert(varchar(30),convert(money,SUM(ISNULL(TMP.DUE_AMOUNT,0))  * -1) ,1) STMT_AMOUNT,           
   convert(varchar(30),convert(money,(SUM(ISNULL(TMP.DUE_AMOUNT,0)) - SUM(ISNULL(TMP.TOTAL_PAID,0))) * -1) ,1) BALANCE_AMOUNT,         
   convert(varchar(30),convert(money,CHK.CHECK_AMOUNT),1) PAYMENT_AMOUNT, 
   --RAGHAV
   ISNULL(AGN.REQ_SPECIAL_HANDLING,'10964') AS  REQ_SPECIAL_HANDLING ,                     
   @CHECK_MODE AS PAYMENT_MODE ,        
   'NO' AS ALLOW_EFT,        
   'Distribute' AS DISTRIBUTE,        
   ISNULL(CHK.IS_RECONCILED,'N') as IS_RECONCILED          
   FROM TEMP_ACT_CHECK_INFORMATION CHK      
   INNER JOIN MNT_USER_LIST USR      
    ON CHK.PAYEE_ENTITY_ID = USR.USER_ID      
    AND ISNULL(CHK.PAYEE_ENTITY_TYPE , '') = 'CSR'      
   INNER JOIN MNT_AGENCY_LIST AGN       
    ON CHK.AGENCY_ID = AGN.AGENCY_ID      
   INNER JOIN       
   (       
    SELECT AAS.ROW_ID  , CPL.CSR AS CSR_ID , AAS.DUE_AMOUNT , AAS.TOTAL_PAID , AAS.AGENCY_ID       
    FROM ACT_AGENCY_STATEMENT AAS      
    INNER JOIN POL_CUSTOMER_POLICY_LIST CPL      
     ON AAS.AGENCY_ID  = CPL.AGENCY_ID       
     AND AAS.CUSTOMER_ID = CPL.CUSTOMER_ID       
     AND AAS.POLICY_ID = CPL.POLICY_ID       
     AND AAS.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID       
    WHERE AAS.MONTH_NUMBER = @MONTH          
    AND AAS.MONTH_YEAR = @YEAR         
    AND AAS.COMM_TYPE=@COMM_TYPE         
    AND AAS.AGENCY_ID = CASE @AGENCY_ID WHEN 0 THEN AAS.AGENCY_ID ELSE  @AGENCY_ID END         
    AND ISNULL(AAS.IS_CHECK_CREATED,'N') <> 'Y'        
   )AS TMP      
   ON TMP.AGENCY_ID = CHK.AGENCY_ID       
   AND TMP.CSR_ID  = CHK.PAYEE_ENTITY_ID       
   GROUP BY AGN.AGENCY_CODE,CHK.CHECK_ID,CHK.CHECK_AMOUNT,USR.USER_FNAME , USR.USER_LNAME ,AGN.REQ_SPECIAL_HANDLING,      
     CHK.CHECK_ID,   CHK.AGENCY_ID , CHK.MONTH ,CHK.YEAR ,CHK.IS_RECONCILED  , USR.USER_ID      
   order by SELECTED_ITEMS , AGENCY_CODE , ENTITY_NAME      
        
 END      
 ELSE IF ( @COMM_TYPE =  'OP')       
 BEGIN       
   SELECT         
   AAS.AGENCY_ID AS PAYEE_ENTITY_ID ,       
   CASE ISNULL(ACI.CHECK_ID,0)         
   WHEN 0 THEN 'False'        
   else  'True' END as SELECTED_ITEMS ,        
   ACI.CHECK_ID,         
   AAS.AGENCY_ID,AGENCY.AGENCY_DISPLAY_NAME as ENTITY_NAME , AGENCY.AGENCY_CODE,        
   AAS.MONTH_NUMBER AS [MONTH],AAS.MONTH_YEAR AS [YEAR],        
   CONVERT(VARCHAR(30),AAS.MONTH_NUMBER) + ' ' +  CONVERT(VARCHAR(20),AAS.MONTH_YEAR) AS MONTHYEAR,         
   convert(varchar(30),convert(money,SUM(ISNULL(AAS.DUE_AMOUNT,0))  * -1) ,1) STMT_AMOUNT,           
   convert(varchar(30),convert(money,(SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0))) * -1) ,1) BALANCE_AMOUNT,         
   --RAGHAV
   ISNULL(AGENCY.REQ_SPECIAL_HANDLING,'10964') AS  REQ_SPECIAL_HANDLING ,             
   convert(varchar(30),convert(money,ACI.CHECK_AMOUNT),1) PAYMENT_AMOUNT,     
   CASE ISNULL(ACI.PAYMENT_MODE,0)         
   When 0 THEN        
   CASE ISNULL(AGENCY.ALLOWS_EFT,@NO)         
   WHEN @YES THEN @EFT_MODE        
   ELSE @CHECK_MODE END         
   ELSE ACI.PAYMENT_MODE END        
   AS PAYMENT_MODE ,        
   /*CASE ISNULL(AGENCY.ALLOWS_EFT,@NO)         
   WHEN @YES THEN 'YES'        
   ELSE 'NO' END  AS ALLOW_EFT,  */      
   CASE       
   WHEN ISNULL(AGENCY.ACCOUNT_ISVERIFIED1,@NO) = @YES       
   AND  ISNULL(AGENCY.ALLOWS_EFT,@NO)  = @YES      
   THEN 'YES'       
   ELSE 'NO' END      
   AS ALLOW_EFT  ,      
   'Distribute' AS DISTRIBUTE,        
   ISNULL(ACI.IS_RECONCILED,'N') as IS_RECONCILED        
   FROM ACT_AGENCY_STATEMENT AAS         
   LEFT JOIN TEMP_ACT_CHECK_INFORMATION ACI         
   ON AAS.AGENCY_ID = ACI.PAYEE_ENTITY_ID        
   AND AAS.MONTH_NUMBER = ACI.[MONTH]        
   AND AAS.MONTH_YEAR = ACI.[YEAR]        
   LEFT JOIN MNT_AGENCY_LIST AGENCY ON AGENCY.AGENCY_ID = AAS.AGENCY_ID        
   WHERE         
   AAS.MONTH_NUMBER = @MONTH          
   AND AAS.MONTH_YEAR = @YEAR         
   AND ISNULL(AAS.ITEM_STATUS,'') = @COMM_TYPE        
   AND ISNULL(AAS.IS_CHECK_CREATED,'N') <> 'Y'        
   AND AAS.AGENCY_ID = CASE @AGENCY_ID WHEN 0 THEN AAS.AGENCY_ID ELSE  @AGENCY_ID END         
   GROUP BY AAS.AGENCY_ID,AAS.MONTH_NUMBER,AAS.MONTH_YEAR,        
   AGENCY.AGENCY_DISPLAY_NAME,AGENCY.AGENCY_CODE,ACI.CHECK_ID,ACI.IS_RECONCILED,AGENCY.REQ_SPECIAL_HANDLING,  
   ACI.CHECK_AMOUNT,ACI.PAYMENT_MODE,AGENCY.ALLOWS_EFT, AGENCY.ACCOUNT_ISVERIFIED1        
   --HAVING (SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0)))< 0        
   order by SELECTED_ITEMS , ENTITY_NAME      
 END      
 ELSE      
 BEGIN         
   -- fetches items acc to the search criteria whether saved or not        
   SELECT         
   AAS.AGENCY_ID AS PAYEE_ENTITY_ID ,       
   CASE ISNULL(ACI.CHECK_ID,0)      
   WHEN 0 THEN 'False'        
   else  'True' END as SELECTED_ITEMS ,        
   ACI.CHECK_ID,         
   AAS.AGENCY_ID,AGENCY.AGENCY_DISPLAY_NAME as ENTITY_NAME, AGENCY.AGENCY_CODE,        
   AAS.MONTH_NUMBER AS [MONTH],AAS.MONTH_YEAR AS [YEAR],        
   CONVERT(VARCHAR(30),AAS.MONTH_NUMBER) + ' ' +  CONVERT(VARCHAR(20),AAS.MONTH_YEAR) AS MONTHYEAR,         
    convert(varchar(30),convert(money,SUM(ISNULL(AAS.DUE_AMOUNT,0))  * -1) ,1) STMT_AMOUNT,           
   convert(varchar(30),convert(money,(SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0))) * -1) ,1) BALANCE_AMOUNT,         
   --RAGHAV
   ISNULL(AGENCY.REQ_SPECIAL_HANDLING,'10964') AS  REQ_SPECIAL_HANDLING ,             
   convert(varchar(30),convert(money,ACI.CHECK_AMOUNT),1) PAYMENT_AMOUNT ,            
   CASE ISNULL(ACI.PAYMENT_MODE,0)         
   When 0 THEN        
   CASE ISNULL(AGENCY.ALLOWS_EFT,@NO)         
   WHEN @YES THEN @EFT_MODE        
   ELSE @CHECK_MODE END         
   ELSE ACI.PAYMENT_MODE END        
   AS PAYMENT_MODE ,       
   /*CASE ISNULL(AGENCY.ALLOWS_EFT,@NO)         
   WHEN @YES THEN 'YES'        
   ELSE 'NO' END  AS ALLOW_EFT,  */      
   CASE       
   WHEN ISNULL(AGENCY.ACCOUNT_ISVERIFIED1,@NO) = @YES       
   AND  ISNULL(AGENCY.ALLOWS_EFT,@NO)  = @YES      
   THEN 'YES'       
   ELSE 'NO' END      
   AS ALLOW_EFT  ,      
   'Distribute' AS DISTRIBUTE,        
   ISNULL(ACI.IS_RECONCILED,'N') as IS_RECONCILED        
   FROM ACT_AGENCY_STATEMENT AAS         
   LEFT JOIN TEMP_ACT_CHECK_INFORMATION ACI         
   ON AAS.AGENCY_ID = ACI.PAYEE_ENTITY_ID        
   AND AAS.MONTH_NUMBER = ACI.[MONTH]        
   AND AAS.MONTH_YEAR = ACI.[YEAR]        
   LEFT JOIN MNT_AGENCY_LIST AGENCY ON AGENCY.AGENCY_ID = AAS.AGENCY_ID        
   WHERE         
   AAS.MONTH_NUMBER = @MONTH          
   AND AAS.MONTH_YEAR = @YEAR         
   AND AAS.COMM_TYPE=@COMM_TYPE         
   AND AAS.AGENCY_ID = CASE @AGENCY_ID WHEN 0 THEN AAS.AGENCY_ID ELSE  @AGENCY_ID END         
   AND ISNULL(AAS.IS_CHECK_CREATED,'N') <> 'Y'        
   GROUP BY AAS.AGENCY_ID,AAS.MONTH_NUMBER,AAS.MONTH_YEAR, AGENCY.REQ_SPECIAL_HANDLING,       
   AGENCY.AGENCY_DISPLAY_NAME,AGENCY.AGENCY_CODE,ACI.CHECK_ID,ACI.IS_RECONCILED,        
   ACI.CHECK_AMOUNT,ACI.PAYMENT_MODE,AGENCY.ALLOWS_EFT, AGENCY.ACCOUNT_ISVERIFIED1  
   HAVING (SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0)))< 0        
         
   UNION          
      
   -- fetches selected(checked) items        
   SELECT         
   AAS.AGENCY_ID AS PAYEE_ENTITY_ID ,       
   CASE ISNULL(ACI.CHECK_ID,0)         
   WHEN 0 THEN 'False'        
   else  'True' END as SELECTED_ITEMS ,        
   ACI.CHECK_ID,ACI.PAYEE_ENTITY_ID,        
   AGENCY.AGENCY_DISPLAY_NAME as ENTITY_NAME,AGENCY.AGENCY_CODE,        
   AAS.MONTH_NUMBER AS [MONTH],AAS.MONTH_YEAR AS [YEAR],        
   CONVERT(VARCHAR(30),AAS.MONTH_NUMBER) + ' ' +  CONVERT(VARCHAR(20),AAS.MONTH_YEAR) AS MONTHYEAR,         
   convert(varchar(30),convert(money,SUM(ISNULL(AAS.DUE_AMOUNT,0))  * -1) ,1) STMT_AMOUNT,           
   convert(varchar(30),convert(money,(SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0))) * -1) ,1) BALANCE_AMOUNT,         
   --RAGHAV 
   ISNULL(AGENCY.REQ_SPECIAL_HANDLING,'10964') AS  REQ_SPECIAL_HANDLING ,             
   convert(varchar(30),convert(money,ACI.CHECK_AMOUNT),1) PAYMENT_AMOUNT ,    
   CASE ISNULL(ACI.PAYMENT_MODE,0) When 0 THEN        
   CASE ISNULL(AGENCY.ALLOWS_EFT,@NO)         
   WHEN @YES THEN @EFT_MODE        
   ELSE @CHECK_MODE END         
   ELSE ACI.PAYMENT_MODE END        
   AS PAYMENT_MODE ,        
   /*CASE ISNULL(AGENCY.ALLOWS_EFT,@NO)         
   WHEN @YES THEN 'YES'        
   ELSE 'NO' END  AS ALLOW_EFT,  */      
   CASE       
   WHEN ISNULL(AGENCY.ACCOUNT_ISVERIFIED1,@NO) = @YES       
   AND  ISNULL(AGENCY.ALLOWS_EFT,@NO)  = @YES      
   THEN 'YES'       
   ELSE 'NO' END      
   AS ALLOW_EFT  ,      
   'Distribute' AS DISTRIBUTE,        
   ISNULL(ACI.IS_RECONCILED,'N') as IS_RECONCILED        
   FROM TEMP_ACT_CHECK_INFORMATION ACI        
   LEFT JOIN MNT_AGENCY_LIST AGENCY ON AGENCY.AGENCY_ID = ACI.PAYEE_ENTITY_ID        
   LEFT JOIN ACT_AGENCY_STATEMENT AAS         
   ON ACI.PAYEE_ENTITY_ID = AAS.AGENCY_ID         
   WHERE ACI.CHECK_TYPE= '2472'         
   AND AAS.COMM_TYPE=@COMM_TYPE         
   AND AAS.MONTH_NUMBER = ACI.[MONTH]         
   AND AAS.MONTH_YEAR = ACI.[YEAR]        
   AND AAS.AGENCY_ID = CASE @AGENCY_ID WHEN 0 THEN AAS.AGENCY_ID ELSE  @AGENCY_ID END          
   AND ISNULL(AAS.IS_CHECK_CREATED,'N') <> 'Y'        
   GROUP BY AAS.AGENCY_ID ,ACI.PAYEE_ENTITY_ID,AAS.MONTH_NUMBER,AAS.MONTH_YEAR,AGENCY.REQ_SPECIAL_HANDLING,        
   AGENCY.AGENCY_DISPLAY_NAME,AGENCY.AGENCY_CODE,ACI.CHECK_AMOUNT ,ACI.CHECK_id,ACI.IS_RECONCILED,        
   ACI.PAYMENT_MODE,AGENCY.ALLOWS_EFT , AGENCY.ACCOUNT_ISVERIFIED1  
   HAVING (SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0))) < 0        
   order by SELECTED_ITEMS, ENTITY_NAME      
 END        
        
END             
        
----      
--go       
--exec Proc_FetchAgencyChecksPayments   10 , 2007 , 0 , 'REG'      
--rollback tran       
----      
--      
      
      
      
      
      
      
      
      
      
      
GO

