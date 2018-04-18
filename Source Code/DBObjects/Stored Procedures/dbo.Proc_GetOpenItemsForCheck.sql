IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOpenItemsForCheck]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOpenItemsForCheck]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran           
--drop proc dbo.Proc_GetOpenItemsForCheck                
--go           
/*----------------------------------------------------------                    
Proc Name       : Proc_GetOpenItemsForCheck                    
Created by      : Ajit Singh Chahal                    
Date            : 18 Aug,2005                    
Purpose   : To get open items for current check type: Automation of checks process                    
Revison History :                    
Used In   : Wolverine                    
                  
Modified By  : Vijay Arora                  
Modified Date : 03-03-2006                  
Purpose   : To include the name of Customer and Policy Version                  
          
Modified By  : Ravindra           
Modidied On  : Feb 23-2007          
Reason    : Review (Problem with Query multiple rows for same record getting selcted )          
          
Modified By  : Praveen           
Modidied On  : Mar 19-2008          
Reason    : Format Amount OP_AMOUNT          
          
Modified By : Raghav        
Modidied On  : Jan 13-2009         
Reason        : Itrack 4969        
exec Proc_GetOpenItemsForCheck   '','1/30/2009','RP',''          
------------------------------------------------------------                  
Date     Review By          Comments                    
------------------------------------------------------------*/                    
-- drop proc dbo.Proc_GetOpenItemsForCheck                
Create PROC dbo.Proc_GetOpenItemsForCheck                    
(                    
 @fromDate DateTime,                    
 @toDate DateTime,                    
 @ITEM_STATUS varchar(3),          
 @POLICY_NUMBER varchar(24)= null                
)                    
AS                    
BEGIN                  
                  
IF @FROMDATE  IS NULL AND @TODATE IS NULL                     
BEGIN                
        
        
                 
--  SELECT DISTINCT * FROM (                  
--  SELECT IDEN_ROW_ID,ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' +  ISNULL(CUSTOMER_LAST_NAME,'')                   
--  AS CUSTOMER_NAME ,POLICY_NUMBER, POLICY_DISP_VERSION,            
--  ((ISNULL(TOTAL_PAID,0) ) - (ISNULL(TOTAL_DUE,0) )) AS OP_AMOUNT,                  
--  PL.POLICY_ID, PL.POLICY_VERSION_ID, PL.CUSTOMER_ID, convert(varchar(10),COI.SOURCE_TRAN_DATE,101) as SOURCE_TRAN_DATE,            
--  CASE WHEN PRO.CANCELLATION_TYPE =0 THEN ' ' ELSE MNT.LOOKUP_VALUE_DESC END AS CANCELLATION_TYPE            
--  FROM  POL_CUSTOMER_POLICY_LIST PL,                 
--  CLT_CUSTOMER_LIST CL,                 
--  ACT_CUSTOMER_OPEN_ITEMS COI,            
--  POL_POLICY_PROCESS PRO,            
--  MNT_LOOKUP_VALUES MNT                   
--  WHERE PL.POLICY_ID=COI.POLICY_ID                 
--  AND PL.POLICY_VERSION_ID=COI.POLICY_VERSION_ID AND                  
--  PL.CUSTOMER_ID = COI.CUSTOMER_ID AND                 
--  CL.CUSTOMER_ID = COI.CUSTOMER_ID AND            
--  -- Get Cancellation Type from Pol_Policy_Process               
--  PRO.CUSTOMER_ID = PL.CUSTOMER_ID AND            
--  PRO.POLICY_ID = PL.POLICY_ID AND             
--  PRO.POLICY_VERSION_ID = PL.POLICY_VERSION_ID AND            
--  (PRO.CANCELLATION_TYPE = MNT.LOOKUP_UNIQUE_ID or PRO.CANCELLATION_TYPE = 0) AND             
--  ITEM_STATUS in (@ITEM_STATUS , case when @ITEM_STATUS = 'RSP' then 'ROP' end) and                 
--  ISNULL(IS_CHECK_CREATED,'N') = 'N') TEST                 
--  WHERE OP_AMOUNT > 0                  
           
 SELECT  ISNULL(LKP.LOOKUP_VALUE_DESC,' ') AS CANCELLATION_TYPE ,          
 OPEN_ITEM.IDEN_ROW_ID As IDEN_ROW_ID,          
 ISNULL(CL.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CL.CUSTOMER_MIDDLE_NAME,'') + ' '           
 +  ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME ,          
 CPL.POLICY_NUMBER AS POLICY_NUMBER ,          
 CPL.POLICY_DISP_VERSION AS POLICY_DISP_VERSION,          
 CPL.POLICY_ID AS POLICY_ID,          
 CPL.POLICY_VERSION_ID AS POLICY_VERSION_ID,          
 CPL.CUSTOMER_ID AS CUSTOMER_ID,          
  --OPEN_ITEM.SOURCE_TRAN_DATE AS SOURCE_TRAN_DATE,          
 --OPEN_ITEM.POSTING_DATE AS SOURCE_TRAN_DATE,       
--Comment For Itrack #4969 OPEN_ITEM.POSTING_DATE AS SOURCE_TRAN_DATE,           
  CASE  @ITEM_STATUS  WHEN 'RP'       
 THEN convert(varchar(30),OPEN_ITEM.DUE_DATE,101)       
  ELSE convert(varchar(30),OPEN_ITEM.POSTING_DATE,101) end AS SOURCE_TRAN_DATE,       
 --ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0) AS OP_AMOUNT          
    CONVERT(VARCHAR(20),CONVERT(MONEY,(ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0))),1) AS OP_AMOUNT   ,    
 case  @ITEM_STATUS when 'RP' THEN     
 convert(varchar(30),
(
	select max(posting_date) from ACT_CUSTOMER_OPEN_ITEMS OIIN where     
	 OIIN.customer_id = OPEN_ITEM.CUSTOMER_ID and    
	 OIIN.policy_id = OPEN_ITEM.POLICY_ID      
     and updated_from='D'),101
)       
 ELSE NULL  end  AS PAYMENT_DATE    
 FROM POL_CUSTOMER_POLICY_LIST CPL          
 INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OPEN_ITEM          
 ON CPL.CUSTOMER_ID = OPEN_ITEM.CUSTOMER_ID          
 AND CPL.POLICY_ID  = OPEN_ITEM.POLICY_ID          
 AND CPL.POLICY_VERSION_ID = OPEN_ITEM.POLICY_VERSION_ID          
 INNER JOIN CLT_CUSTOMER_LIST CL          
 ON CL.CUSTOMER_ID = CPL.CUSTOMER_ID           
 LEFT JOIN POL_POLICY_PROCESS PRO           
 ON PRO.CUSTOMER_ID = CPL.CUSTOMER_ID          
 AND PRO.POLICY_ID = CPL.POLICY_ID          
 AND PRO.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID          
 AND PRO.PROCESS_ID IN (29,12)          
        LEFT JOIN MNT_LOOKUP_VALUES LKP           
 ON LKP.LOOKUP_UNIQUE_ID = PRO.CANCELLATION_TYPE          
 WHERE (ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0)) > 0          
 AND          
 ( SELECT ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)),0)           
   FROM ACT_CUSTOMER_OPEN_ITEMS OI           
   WHERE OI.CUSTOMER_ID = CPL.CUSTOMER_ID AND OI.POLICY_ID = CPL.POLICY_ID           
 ) < 0           
 AND ITEM_STATUS in (@ITEM_STATUS , case when @ITEM_STATUS = 'RSP' then 'ROP' end)          
 AND ISNULL(IS_CHECK_CREATED,'N') = 'N'            
 AND CPL.POLICY_NUMBER =  CASE @POLICY_NUMBER WHEN '' THEN CPL.POLICY_NUMBER ELSE  @POLICY_NUMBER END           
 ORDER BY CUSTOMER_NAME , POLICY_NUMBER --OPEN_ITEM.IDEN_ROW_ID          
END                
ELSE                    
BEGIN                    
 IF @FROMDATE IS NULL                    
 BEGIN           
        
--   SELECT DISTINCT * FROM           
--   (                  
--   SELECT IDEN_ROW_ID,ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' +  ISNULL(CUSTOMER_LAST_NAME,'')                   
--   AS CUSTOMER_NAME ,POLICY_NUMBER, POLICY_DISP_VERSION,            
--   (TOTAL_PAID - TOTAL_DUE) AS OP_AMOUNT,                  
--   PL.POLICY_ID, PL.POLICY_VERSION_ID, PL.CUSTOMER_ID,          
--   convert(varchar(10),COI.SOURCE_TRAN_DATE,101) as SOURCE_TRAN_DATE,            
--   CASE WHEN PRO.CANCELLATION_TYPE =0 THEN ' ' ELSE MNT.LOOKUP_VALUE_DESC END AS CANCELLATION_TYPE                
--   FROM   POL_CUSTOMER_POLICY_LIST PL,           
--          CLT_CUSTOMER_LIST CL,           
--    ACT_CUSTOMER_OPEN_ITEMS COI,          
--    POL_POLICY_PROCESS PRO,          
--    MNT_LOOKUP_VALUES MNT                    
--   WHERE PL.POLICY_ID=COI.POLICY_ID           
--          AND  PL.POLICY_VERSION_ID=COI.POLICY_VERSION_ID           
--    AND  PL.CUSTOMER_ID = COI.CUSTOMER_ID           
--    AND  CL.CUSTOMER_ID = COI.CUSTOMER_ID           
--   -- Get Cancellation Type from Pol_Policy_Process               
--    AND  PRO.CUSTOMER_ID = COI.CUSTOMER_ID           
--    AND  PRO.POLICY_ID = COI.POLICY_ID           
--    AND   PRO.POLICY_VERSION_ID = COI.POLICY_VERSION_ID           
--    AND  (PRO.CANCELLATION_TYPE = MNT.LOOKUP_UNIQUE_ID or PRO.CANCELLATION_TYPE = 0)           
--    AND   ITEM_STATUS in (@ITEM_STATUS , case when @ITEM_STATUS = 'RSP' then 'ROP' end)           
--    and    ISNULL(IS_CHECK_CREATED,'N') = 'N' AND SOURCE_TRAN_DATE <= @toDate) TEST           
--    WHERE OP_AMOUNT > 0                  
--           
  SELECT  ISNULL(LKP.LOOKUP_VALUE_DESC,' ') AS CANCELLATION_TYPE ,          
  OPEN_ITEM.IDEN_ROW_ID As IDEN_ROW_ID,          
  ISNULL(CL.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CL.CUSTOMER_MIDDLE_NAME,'') + ' '           
  +  ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME ,          
  CPL.POLICY_NUMBER AS POLICY_NUMBER ,          
  CPL.POLICY_DISP_VERSION AS POLICY_DISP_VERSION,          
  CPL.POLICY_ID AS POLICY_ID,          
  CPL.POLICY_VERSION_ID AS POLICY_VERSION_ID,          
  CPL.CUSTOMER_ID AS CUSTOMER_ID,          
   --OPEN_ITEM.POSTING_DATE AS SOURCE_TRAN_DATE,      
--Comment For Itrack #4969 OPEN_ITEM.POSTING_DATE AS SOURCE_TRAN_DATE,           
  CASE  @ITEM_STATUS  WHEN 'RP'       
 THEN convert(varchar(30),OPEN_ITEM.DUE_DATE,101)       
  ELSE convert(varchar(30),OPEN_ITEM.POSTING_DATE,101) end AS SOURCE_TRAN_DATE,            
  --ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0) AS OP_AMOUNT          
  CONVERT(VARCHAR(20),CONVERT(MONEY,(ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0))),1) AS OP_AMOUNT  ,    
--convert(varchar(30),PAYMENT_DATE,101)    
   case  @ITEM_STATUS when 'RP' THEN     
   convert(varchar(30),(select max(posting_date) from ACT_CUSTOMER_OPEN_ITEMS OIIN where     
    OIIN.customer_id = OPEN_ITEM.CUSTOMER_ID and    
 OIIN.policy_id = OPEN_ITEM.POLICY_ID  
 and updated_from='D') ,101)        
 ELSE NULL  end  as PAYMENT_DATE    
  FROM POL_CUSTOMER_POLICY_LIST CPL          
  INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OPEN_ITEM          
  ON CPL.CUSTOMER_ID = OPEN_ITEM.CUSTOMER_ID          
  AND CPL.POLICY_ID  = OPEN_ITEM.POLICY_ID          
  AND CPL.POLICY_VERSION_ID = OPEN_ITEM.POLICY_VERSION_ID          
  INNER JOIN CLT_CUSTOMER_LIST CL          
  ON CL.CUSTOMER_ID = CPL.CUSTOMER_ID           
  LEFT JOIN POL_POLICY_PROCESS PRO           
  ON PRO.CUSTOMER_ID = CPL.CUSTOMER_ID          
  AND PRO.POLICY_ID = CPL.POLICY_ID          
  AND PRO.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID          
  AND PRO.PROCESS_ID IN (29,12)          
  LEFT JOIN MNT_LOOKUP_VALUES LKP           
  ON LKP.LOOKUP_UNIQUE_ID = PRO.CANCELLATION_TYPE          
  WHERE (ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0)) > 0          
  AND          
  ( SELECT ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)),0)           
    FROM ACT_CUSTOMER_OPEN_ITEMS OI           
    WHERE OI.CUSTOMER_ID = CPL.CUSTOMER_ID AND OI.POLICY_ID = CPL.POLICY_ID           
  ) < 0           
  AND  ITEM_STATUS in (@ITEM_STATUS , case when @ITEM_STATUS = 'RSP' then 'ROP' end)          
  AND ISNULL(IS_CHECK_CREATED,'N') = 'N'            
  --AND SOURCE_TRAN_DATE <= @TODATE          
  AND CAST(CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS DATETIME) <= @TODATE          
  AND CPL.POLICY_NUMBER =  CASE @POLICY_NUMBER WHEN '' THEN CPL.POLICY_NUMBER ELSE  @POLICY_NUMBER END           
  ORDER BY OPEN_ITEM.IDEN_ROW_ID          
          
 END          
 IF @TODATE  IS NULL           
 BEGIN           
        
        
         
--   SELECT DISTINCT * FROM (                  
--   SELECT IDEN_ROW_ID,ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' +  ISNULL(CUSTOMER_LAST_NAME,'')                   
--   AS CUSTOMER_NAME ,POLICY_NUMBER, POLICY_DISP_VERSION,            
--   (TOTAL_PAID - TOTAL_DUE) AS OP_AMOUNT,                  
--   PL.POLICY_ID, PL.POLICY_VERSION_ID, PL.CUSTOMER_ID,convert(varchar(10),COI.SOURCE_TRAN_DATE,101) as SOURCE_TRAN_DATE,            
--   CASE WHEN PRO.CANCELLATION_TYPE =0 THEN ' ' ELSE MNT.LOOKUP_VALUE_DESC END AS CANCELLATION_TYPE            
--   FROM  POL_CUSTOMER_POLICY_LIST PL, CLT_CUSTOMER_LIST CL, ACT_CUSTOMER_OPEN_ITEMS COI,POL_POLICY_PROCESS PRO,MNT_LOOKUP_VALUES MNT                    
--   WHERE PL.POLICY_ID=COI.POLICY_ID AND PL.POLICY_VERSION_ID=COI.POLICY_VERSION_ID AND                  
--   PL.CUSTOMER_ID = COI.CUSTOMER_ID AND CL.CUSTOMER_ID = COI.CUSTOMER_ID AND              
--   -- Get Cancellation Type from Pol_Policy_Process               
--   PRO.CUSTOMER_ID = PL.CUSTOMER_ID AND            
--   PRO.POLICY_ID = PL.POLICY_ID AND             
--   PRO.POLICY_VERSION_ID = PL.POLICY_VERSION_ID AND            
--   (PRO.CANCELLATION_TYPE = MNT.LOOKUP_UNIQUE_ID or PRO.CANCELLATION_TYPE = 0) AND              
--   ITEM_STATUS in (@ITEM_STATUS , case when @ITEM_STATUS = 'RSP' then 'ROP' end) and                 
--   ISNULL(IS_CHECK_CREATED,'N') = 'N' AND SOURCE_TRAN_DATE >= @fromDate) TEST WHERE OP_AMOUNT > 0                  
          
  SELECT  ISNULL(LKP.LOOKUP_VALUE_DESC,' ') AS CANCELLATION_TYPE ,          
  OPEN_ITEM.IDEN_ROW_ID As IDEN_ROW_ID,          
  ISNULL(CL.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CL.CUSTOMER_MIDDLE_NAME,'') + ' '           
  +  ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME ,          
  CPL.POLICY_NUMBER AS POLICY_NUMBER ,          
  CPL.POLICY_DISP_VERSION AS POLICY_DISP_VERSION,          
  CPL.POLICY_ID AS POLICY_ID,          
  CPL.POLICY_VERSION_ID AS POLICY_VERSION_ID,          
  CPL.CUSTOMER_ID AS CUSTOMER_ID,          
   --OPEN_ITEM.POSTING_DATE AS SOURCE_TRAN_DATE,      
--Comment For Itrack #4969 OPEN_ITEM.POSTING_DATE AS SOURCE_TRAN_DATE,           
  CASE  @ITEM_STATUS  WHEN 'RP'       
 THEN convert(varchar(30),OPEN_ITEM.DUE_DATE,101)       
  ELSE convert(varchar(30),OPEN_ITEM.POSTING_DATE,101) end AS SOURCE_TRAN_DATE,             
  --ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0) AS OP_AMOUNT          
  CONVERT(VARCHAR(20),CONVERT(MONEY,(ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0))),1) AS OP_AMOUNT ,    
  case  @ITEM_STATUS when 'RP' THEN     
  convert(varchar(30),(select max(posting_date) from ACT_CUSTOMER_OPEN_ITEMS OIIN where     
  OIIN.customer_id = OPEN_ITEM.CUSTOMER_ID and    
  OIIN.policy_id = OPEN_ITEM.POLICY_ID and    
  updated_from='D'),101)         
  ELSE NULL  end  AS PAYMENT_DATE               
  FROM POL_CUSTOMER_POLICY_LIST CPL          
  INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OPEN_ITEM          
  ON CPL.CUSTOMER_ID = OPEN_ITEM.CUSTOMER_ID          
  AND CPL.POLICY_ID  = OPEN_ITEM.POLICY_ID          
  AND CPL.POLICY_VERSION_ID = OPEN_ITEM.POLICY_VERSION_ID          
  INNER JOIN CLT_CUSTOMER_LIST CL          
  ON CL.CUSTOMER_ID = CPL.CUSTOMER_ID           
  LEFT JOIN POL_POLICY_PROCESS PRO           
  ON PRO.CUSTOMER_ID = CPL.CUSTOMER_ID          
  AND PRO.POLICY_ID = CPL.POLICY_ID          
  AND PRO.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID          
  AND PRO.PROCESS_ID IN (29,12)          
  LEFT JOIN MNT_LOOKUP_VALUES LKP           
  ON LKP.LOOKUP_UNIQUE_ID = PRO.CANCELLATION_TYPE          
  WHERE (ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0)) > 0          
  AND          
  ( SELECT ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)),0)           
    FROM ACT_CUSTOMER_OPEN_ITEMS OI           
    WHERE OI.CUSTOMER_ID = CPL.CUSTOMER_ID AND OI.POLICY_ID = CPL.POLICY_ID           
  ) < 0           
  AND  ITEM_STATUS in (@ITEM_STATUS , case when @ITEM_STATUS = 'RSP' then 'ROP' end)          
  AND ISNULL(IS_CHECK_CREATED,'N') = 'N'            
  --AND SOURCE_TRAN_DATE >= @TODATE          
  AND CAST(CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS DATETIME) >= @FROMDATE          
  AND CPL.POLICY_NUMBER =  CASE @POLICY_NUMBER WHEN '' THEN CPL.POLICY_NUMBER ELSE  @POLICY_NUMBER END           
  ORDER BY OPEN_ITEM.IDEN_ROW_ID          
 END          
END                    
IF @FROMDATE  IS NOT NULL AND @TODATE IS NOT NULL                     
BEGIN           
        
        
--  SELECT DISTINCT * FROM (                  
--  SELECT IDEN_ROW_ID,ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' +  ISNULL(CUSTOMER_LAST_NAME,'')                   
--  AS CUSTOMER_NAME ,POLICY_NUMBER, POLICY_DISP_VERSION,            
--  (TOTAL_PAID - TOTAL_DUE) AS OP_AMOUNT,                  
--  PL.POLICY_ID, PL.POLICY_VERSION_ID, PL.CUSTOMER_ID,convert(varchar(10),COI.SOURCE_TRAN_DATE,101) as SOURCE_TRAN_DATE,            
--  CASE WHEN PRO.CANCELLATION_TYPE =0 THEN ' ' ELSE MNT.LOOKUP_VALUE_DESC END AS CANCELLATION_TYPE             
--  FROM  POL_CUSTOMER_POLICY_LIST PL, CLT_CUSTOMER_LIST CL, ACT_CUSTOMER_OPEN_ITEMS COI,POL_POLICY_PROCESS PRO,MNT_LOOKUP_VALUES MNT                    
--  WHERE PL.POLICY_ID=COI.POLICY_ID AND PL.POLICY_VERSION_ID=COI.POLICY_VERSION_ID AND                  
--  PL.CUSTOMER_ID = COI.CUSTOMER_ID AND CL.CUSTOMER_ID = COI.CUSTOMER_ID AND                  
--  ITEM_STATUS in (@ITEM_STATUS , case when @ITEM_STATUS = 'RSP' then 'ROP' end) and              
--  -- Get Cancellation Type from Pol_Policy_Process               
--  PRO.CUSTOMER_ID = PL.CUSTOMER_ID AND            
--  PRO.POLICY_ID = PL.POLICY_ID AND             
--  PRO.POLICY_VERSION_ID = PL.POLICY_VERSION_ID AND            
--  (PRO.CANCELLATION_TYPE = MNT.LOOKUP_UNIQUE_ID or PRO.CANCELLATION_TYPE = 0) AND             
--  ISNULL(IS_CHECK_CREATED,'N') = 'N' AND SOURCE_TRAN_DATE >= @fromDate and SOURCE_TRAN_DATE <= @toDate)                   
--  TEST WHERE OP_AMOUNT > 0                  
          
 SELECT  ISNULL(LKP.LOOKUP_VALUE_DESC,' ') AS CANCELLATION_TYPE ,          
 OPEN_ITEM.IDEN_ROW_ID As IDEN_ROW_ID,          
 ISNULL(CL.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CL.CUSTOMER_MIDDLE_NAME,'') + ' '           
 +  ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME ,          
 CPL.POLICY_NUMBER AS POLICY_NUMBER ,          
 CPL.POLICY_DISP_VERSION AS POLICY_DISP_VERSION,          
 CPL.POLICY_ID AS POLICY_ID,          
 CPL.POLICY_VERSION_ID AS POLICY_VERSION_ID,          
 CPL.CUSTOMER_ID AS CUSTOMER_ID,          
--Comment For Itrack #4969 OPEN_ITEM.POSTING_DATE AS SOURCE_TRAN_DATE,           
  CASE  @ITEM_STATUS  WHEN 'RP'       
  THEN convert(varchar(30),OPEN_ITEM.DUE_DATE,101)       
  ELSE convert(varchar(30),OPEN_ITEM.POSTING_DATE,101) end AS SOURCE_TRAN_DATE,      
 -- ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0) AS OP_AMOUNT     
  CONVERT(VARCHAR(20),CONVERT(MONEY,(ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0))),1) AS OP_AMOUNT ,    
  case  @ITEM_STATUS when 'RP' THEN     
  convert(varchar(30),(select max(posting_date) from ACT_CUSTOMER_OPEN_ITEMS OIIN where     
 OIIN.customer_id = OPEN_ITEM.CUSTOMER_ID and    
 OIIN.policy_id = OPEN_ITEM.POLICY_ID and    
 updated_from='D'),101)    
 ELSE NULL  end  AS PAYMENT_DATE     
 FROM POL_CUSTOMER_POLICY_LIST CPL          
 INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OPEN_ITEM          
 ON CPL.CUSTOMER_ID = OPEN_ITEM.CUSTOMER_ID          
 AND CPL.POLICY_ID  = OPEN_ITEM.POLICY_ID          
 AND CPL.POLICY_VERSION_ID = OPEN_ITEM.POLICY_VERSION_ID          
 INNER JOIN CLT_CUSTOMER_LIST CL          
 ON CL.CUSTOMER_ID = CPL.CUSTOMER_ID           
 LEFT JOIN POL_POLICY_PROCESS PRO           
 ON PRO.CUSTOMER_ID = CPL.CUSTOMER_ID          
 AND PRO.POLICY_ID = CPL.POLICY_ID          
 AND PRO.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID          
 AND PRO.PROCESS_ID IN (29,12)          
 LEFT JOIN MNT_LOOKUP_VALUES LKP           
 ON LKP.LOOKUP_UNIQUE_ID = PRO.CANCELLATION_TYPE          
 WHERE (ISNULL(OPEN_ITEM.TOTAL_PAID,0) - ISNULL(OPEN_ITEM.TOTAL_DUE,0)) > 0          
 AND          
  ( SELECT ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)),0)           
    FROM ACT_CUSTOMER_OPEN_ITEMS OI           
    WHERE OI.CUSTOMER_ID = CPL.CUSTOMER_ID AND OI.POLICY_ID = CPL.POLICY_ID           
  ) < 0           
 AND  ITEM_STATUS in (@ITEM_STATUS , case when @ITEM_STATUS = 'RSP' then 'ROP' end)          
 AND ISNULL(IS_CHECK_CREATED,'N') = 'N'            
 --AND SOURCE_TRAN_DATE >= @FROMDATE AND SOURCE_TRAN_DATE <= @TODATE          
 AND CAST(CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS DATETIME) >= @FROMDATE AND CAST(CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS DATETIME) <= @TODATE          
 AND CPL.POLICY_NUMBER =  CASE @POLICY_NUMBER WHEN '' THEN CPL.POLICY_NUMBER ELSE  @POLICY_NUMBER END           
 ORDER BY OPEN_ITEM.IDEN_ROW_ID          
END          
          
    
SELECT SYS_PAYMENT_DAYS FROM MNT_SYSTEM_PARAMS     
END                  
            
          
          
          
--go           
--          
--exec dbo.Proc_GetOpenItemsForCheck '','01/13/2009','RP',''             
--          
--          
--rollback tran           
         
    
    
--select max(posting_date) from ACT_CUSTOMER_OPEN_ITEMS where     
--customer_id = and    
--policy_id = and    
--policy_version_id updated_from='D'
GO

