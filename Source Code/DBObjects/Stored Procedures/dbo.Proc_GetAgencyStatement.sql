IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyStatement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyStatement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran        
--drop proc dbo.Proc_GetAgencyStatement         
--go        
 /*----------------------------------------------------------------------            
CREATE BY   : Vijay Joshi  Proc_GetAgencyStatement        
CREATE DATETIME : 24 Oct'2005 03.08.00 PM            
PURPOSE    : To Fetch the agency statement details of specified agency of specified month            
REVIOSN HISTORY            
Revised By  Date  Reason            
       
Revised By  : Ravindra        
Date   : 09-7-2007        
Reason   : For Complete App Bonus and statement all ready processed Csr Details are not fetched        
        
        
Revised By  : Ravindra        
Date   : 09-27-2007        
Reason   : To display report as per discussion with Rajan         
     Transactions will be shown Groupped by Policy Number , Commission Percentage ,        
     Transaction Type        
     In stat fees , gross and net premium actual processed amount will be displayed         
-----------------------------------------------------------------------*/         
--Proc_GetAgencyStatement 195,12,2007,'CAC'          
--Proc_GetAgencyStatement 179,12,2006,'REG'          
--drop proc dbo.Proc_GetAgencyStatement        
CREATE PROC [dbo].[Proc_GetAgencyStatement]           
(            
 @AGENCY_ID  VARCHAR (1000),            
 @MONTH  INT,            
 @YEAR   INT,          
 @COMM_TYPE VARCHAR(20) ,    
 @USER_TYPE_CODE VARCHAR(20) = null         
 -- @IS_INTERIM CHAR(2) OUTPUT --IF INTERIM RETURN 'Y' ELSE 'N'          
)            
AS            
          
BEGIN             
        
 CREATE TABLE #STATEMENT        
 (        
   CUSTOMER_ID   Int,        
   POLICY_ID   INt,        
   POLICY_VERSION_ID Int,        
   MCCA_FEES   Decimal(18,2),        
   NET_AMOUNT   Decimal(18,2),        
   GROSS_AMOUNT  As ISNULL(MCCA_FEES , 0) + ISNULL(NET_AMOUNT , 0),        
   COMMISSION_RATE  Decimal(18,2),        
   COMMISSION_AMOUNT Decimal(18,2),        
   BILL_TYPE   CHAR(4),        
   TRAN_TYPE   Char(10),        
   AGENCY_ID   Int ,        
   MONTH_NUMBER  Int,        
   MONTH_YEAR   Int        
 )      
      
 CREATE TABLE #TEMP_STATEMENT      
 (      
  CUSTOMER_NAME VARCHAR(1000),            
  POLICY_NUMBER VARCHAR (100),        
  SOURCE_EFF_DATE DATETIME,         
  GROSS_PREMIUM Decimal (18,2),         
  MCCA_FEES Decimal(18,2) ,         
  MCCA_FEES1 Decimal(18,2),         
  PREMIUM Decimal(18,2),        
  PREMIUM1 Decimal(18,2),         
  COMMISSION_RATE Decimal(18,2),       
  COMMISSION_AMOUNT Decimal(18,2),      
  AMOUNT_FOR_CALCULATION  Decimal(18,2),       
  LOB_CODE varchar(10),      
  DUE_AMOUNT Decimal(18,2),      
  BILL_TYPE CHAR(4) ,         
  TRAN_TYPE  Char(10),        
  AGENCY_DISPLAY_NAME VARCHAR(1000),          
  AGENCY_CODE varchar(10),           
  AGENCY_ADD1 VARCHAR(1000),          
  AGENCY_ADD2 VARCHAR(1000),          
  AGENCY_CITY VARCHAR(1000),          
  AGENCY_STATE VARCHAR(1000),          
  AGENCY_ZIP VARCHAR(1000),          
  PRODUCER_ID int,          
  PRODUCER_CODE VARCHAR(1000),          
  PRODUCER_NAME VARCHAR(1000),      
  CSR_ID int,          
  CSR_CODE VARCHAR(1000),          
  CSR_NAME VARCHAR(1000),      
  IS_INTERIM VARCHAR (10)          
 )        
 --IF EXISTS(SELECT AGENCY_ID FROM ACT_AGENCY_STATEMENT where AGENCY_ID = @AGENCY_ID             
 --  AND MONTH_NUMBER = @MONTH  AND MONTH_YEAR = @YEAR  AND COMM_TYPE = @COMM_TYPE)          
      
 -- IF EXISTS(SELECT AGENCY_ID FROM ACT_AGENCY_STATEMENT where AGENCY_ID = @AGENCY_ID             
 --   AND ((MONTH_NUMBER >= @MONTH  AND MONTH_YEAR = @YEAR) OR (MONTH_YEAR > @YEAR)) AND COMM_TYPE = @COMM_TYPE)       
      
      
       
IF (@AGENCY_ID <>'')        
BEGIN        
 DECLARE @CURRENT_AGENCY_ID VARCHAR (20)      
 DECLARE @SCOUNT INT      
 SET @SCOUNT=1      
 SET @CURRENT_AGENCY_ID = DBO.PIECE(@AGENCY_ID,',',@SCOUNT)                  
 WHILE @CURRENT_AGENCY_ID IS NOT NULL                  
 BEGIN          
  TRUNCATE TABLE #STATEMENT    
          
  IF EXISTS(SELECT ROW_ID FROM ACT_AGENCY_STATEMENT --WHERE AGENCY_ID = @CURRENT_AGENCY_ID--dbo.instring(replace(@CURRENT_AGENCY_ID,',',' '),AGENCY_ID)>0                 
  WHERE ((MONTH_NUMBER >= @MONTH  AND MONTH_YEAR = @YEAR) OR (MONTH_YEAR > @YEAR)) AND COMM_TYPE = @COMM_TYPE)         
  BEGIN          
   IF(@COMM_TYPE = 'CAC')          
   BEGIN    
       
    insert into #TEMP_STATEMENT      
    (      
     CUSTOMER_NAME,          
     POLICY_NUMBER ,        
     SOURCE_EFF_DATE,         
     --TRAN_TYPE,      
     GROSS_PREMIUM,         
     MCCA_FEES,      
     MCCA_FEES1,         
     PREMIUM,      
     PREMIUM1,        
     COMMISSION_RATE,       
     AMOUNT_FOR_CALCULATION,      
     BILL_TYPE,        
     COMMISSION_AMOUNT,      
     DUE_AMOUNT,         
     LOB_CODE,      
     TRAN_TYPE,        
     AGENCY_DISPLAY_NAME,          
     AGENCY_CODE,           
     AGENCY_ADD1,          
     AGENCY_ADD2,          
     AGENCY_CITY,          
     AGENCY_STATE,          
     AGENCY_ZIP,          
     CSR_ID,          
     CSR_CODE,          
     CSR_NAME,          
     IS_INTERIM           
    )                
    SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,            
    PCPL.POLICY_NUMBER, CONVERT(VARCHAR(20), AAS.SOURCE_EFF_DATE, 101) SOURCE_EFF_DATE,             
    AAS.GROSS_PREMIUM AS GROSS_PREMIUM, AAS.FEES AS MCCA_FEES,        
    CASE WHEN TRAN_TYPE != 'PIC' THEN AAS.FEES ELSE 0 END AS MCCA_FEES1,             
    AAS.PREMIUM_AMOUNT PREMIUM,         
    CASE WHEN TRAN_TYPE != 'PIC' THEN AAS.PREMIUM_AMOUNT ELSE 0 END AS PREMIUM1,        
    AAS.COMMISSION_RATE,            
    AMOUNT_FOR_CALCULATION,       
    AAS.BILL_TYPE,            
    AAS.COMMISSION_AMOUNT,       
    AAS.DUE_AMOUNT,       
    LOB.LOB_CODE AS LOB_CODE, ISNULL(TRAN_CODE,'') + '-' + ISNULL(TRAN_TYPE,'') AS TRAN_TYPE,            
    ISNULL(AGNL.AGENCY_DISPLAY_NAME,' ')  + ' - ' + ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_DISPLAY_NAME,          
    ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_CODE,           
    ISNULL(AGNL.AGENCY_ADD1,' ') AS AGENCY_ADD1,          
    ISNULL(AGNL.AGENCY_ADD2,' ') AS AGENCY_ADD2,          
    ISNULL(AGNL.AGENCY_CITY,' ') AS AGENCY_CITY,          
    (          
     SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST WHERE           
     COUNTRY_ID = AGNL.AGENCY_COUNTRY AND STATE_ID = AGNL.AGENCY_STATE          
    ) AS AGENCY_STATE,          
    ISNULL(AGNL.AGENCY_ZIP,' ')  AS AGENCY_ZIP,          
    PCPL.CSR AS CSR_ID,          
    MUL.SUB_CODE AS CSR_CODE,          
    ISNULL(MUL.USER_FNAME,'') + ' ' + ISNULL(MUL.USER_LNAME,'') AS CSR_NAME ,          
    'N' as IS_INTERIM          
    FROM ACT_AGENCY_STATEMENT AAS            
    LEFT JOIN CLT_CUSTOMER_LIST CCL ON CCL.CUSTOMER_ID = AAS.CUSTOMER_ID            
    LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON PCPL.POLICY_ID = AAS.POLICY_ID             
    AND PCPL.POLICY_VERSION_ID = AAS.POLICY_VERSION_ID             
    AND PCPL.CUSTOMER_ID = AAS.CUSTOMER_ID            
    LEFT JOIN MNT_LOB_MASTER LOB ON LOB.LOB_ID = PCPL.POLICY_LOB            
    LEFT JOIN MNT_AGENCY_LIST AGNL ON AGNL.AGENCY_ID = AAS.AGENCY_ID          
    --LEFT JOIN MNT_USER_LIST UL ON UL.USER_SYSTEM_ID = AGNL.AGENCY_CODE ----ADDED FOR CSR          
    --LEFT JOIN MNT_USER_TYPES UT ON UT.USER_TYPE_ID = UL.USER_TYPE_ID   --ADDED FOR CSR          
    LEFT JOIN MNT_USER_LIST MUL ON AAS.CSR_ID = MUL.[USER_ID]          
    LEFT JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI ON ACOI.IDEN_ROW_ID = AAS.CUSTOMER_OPEN_ITEM_ID            
    LEFT JOIN ACT_AGENCY_OPEN_ITEMS AGOI ON AGOI.IDEN_ROW_ID = AAS.AGENCY_OPEN_ITEM_ID            
    WHERE (AGOI.UPDATED_FROM = 'P' OR AGOI.UPDATED_FROM IS NULL) AND (ACOI.UPDATED_FROM = 'P' OR ACOI.UPDATED_FROM IS NULL)            
    AND AAS.AGENCY_ID = @CURRENT_AGENCY_ID        
    AND AAS.MONTH_NUMBER = @MONTH             
    AND AAS.MONTH_YEAR   = @YEAR          
    AND AAS.COMM_TYPE = @COMM_TYPE        
    ORDER BY AGNL.AGENCY_DISPLAY_NAME,PCPL.BILL_TYPE , AAS.ROW_ID            
   END          
   ELSE       
   BEGIN  --REG CASE    
    INSERT INTO #STATEMENT        
    (        
     CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
     MCCA_FEES , NET_AMOUNT , COMMISSION_RATE ,         
     COMMISSION_AMOUNT ,BILL_TYPE ,  TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    )        
    SELECT AAS.CUSTOMER_ID ,AAS.POLICY_ID ,         
    AAS.POLICY_VERSION_ID,     
    AAS.STAT_FEES_FOR_CALCULATION ,AAS.AMOUNT_FOR_CALCULATION , AAS.COMMISSION_RATE,        
    AAS.COMMISSION_AMOUNT ,AAS.BILL_TYPE ,         
    CASE AAS.TRAN_CODE   WHEN 'NBSP'  Then 'REG' --'NBS'        
    WHEN 'NBSC'  Then 'REG' --'NBS'        
    WHEN 'RENP'  THEN 'REG' --'REN'        
    WHEN 'RENC'  Then 'REG' --'REN'        
    WHEN 'ENDP'  Then 'REG' --'END'        
    WHEN 'ENDC'  THEN 'REG' --'END'          
    WHEN 'CANCP'  Then 'REG' --'CAN'        
    WHEN 'CANC'  THEN 'REG' --'CAN'           
    WHEN 'RINSP'  Then 'REG' --'RIN'        
    WHEN 'RINC' Then  'REG' --'RIN'        
    ELSE TRAN_CODE        
    END ,        
    AAS.AGENCY_ID , AAS.MONTH_NUMBER ,  AAS.MONTH_YEAR         
    from ACT_AGENCY_STATEMENT AAS         
    LEFT JOIN ACT_AGENCY_OPEN_ITEMS AGOI         
    ON AGOI.IDEN_ROW_ID = AAS.AGENCY_OPEN_ITEM_ID         
    LEFT JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI         
    ON ACOI.IDEN_ROW_ID = AAS.CUSTOMER_OPEN_ITEM_ID            
    WHERE (AGOI.UPDATED_FROM = 'P' OR AGOI.UPDATED_FROM IS NULL) AND (ACOI.UPDATED_FROM = 'P' OR ACOI.UPDATED_FROM IS NULL)            
    AND AAS.AGENCY_ID = @CURRENT_AGENCY_ID          
    AND AAS.MONTH_NUMBER = @MONTH             
    AND AAS.MONTH_YEAR = @YEAR            
    AND AAS.COMM_TYPE = @COMM_TYPE          
    
    insert into #TEMP_STATEMENT      
    (      
     CUSTOMER_NAME,          
     POLICY_NUMBER ,        
     SOURCE_EFF_DATE,         
     --TRAN_TYPE,      
     GROSS_PREMIUM,         
     MCCA_FEES,      
     MCCA_FEES1,         
     PREMIUM,      
     PREMIUM1,          
     COMMISSION_RATE,      
     COMMISSION_AMOUNT,       
     LOB_CODE,      
     BILL_TYPE,        
     TRAN_TYPE,        
     AGENCY_DISPLAY_NAME,          
     AGENCY_CODE,           
     AGENCY_ADD1,          
     AGENCY_ADD2,          
     AGENCY_CITY,          
     AGENCY_STATE,          
     AGENCY_ZIP,          
     PRODUCER_ID,          
     PRODUCER_CODE,          
     PRODUCER_NAME,          
     IS_INTERIM           
    )                
    
    SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,            
    PCPL.POLICY_NUMBER AS POLICY_NUMBER,        
    CONVERT(VARCHAR,PCPL.POL_VER_EFFECTIVE_DATE ,101) AS SOURCE_EFF_DATE,         
    TMP.GROSS_AMOUNT AS GROSS_PREMIUM,       
    TMP.MCCA_FEES MCCA_FEES ,        
    CASE WHEN TMP.TRAN_TYPE != 'PIC' THEN TMP.MCCA_FEES ELSE 0 END AS MCCA_FEES1,          
    TMP.NET_AMOUNT AS PREMIUM,         
    CASE WHEN TMP.TRAN_TYPE != 'PIC' THEN TMP.NET_AMOUNT ELSE 0 END AS PREMIUM1,         
    TMP.COMMISSION_RATE  AS COMMISSION_RATE,       
    TMP.COMMISSION_AMOUNT AS COMMISSION_AMOUNT,         
    LOB.LOB_CODE AS LOB_CODE,TMP.BILL_TYPE AS BILL_TYPE , TMP.TRAN_TYPE,        
    ISNULL(AGNL.AGENCY_DISPLAY_NAME,' ') AS AGENCY_DISPLAY_NAME,          
    ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_CODE,           
    ISNULL(AGNL.AGENCY_ADD1,' ') AS AGENCY_ADD1,          
    ISNULL(AGNL.AGENCY_ADD2,' ') AS AGENCY_ADD2,          
    ISNULL(AGNL.AGENCY_CITY,' ') AS AGENCY_CITY,          
    (          
     SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST WHERE           
     COUNTRY_ID = AGNL.AGENCY_COUNTRY AND STATE_ID = AGNL.AGENCY_STATE          
    ) AS AGENCY_STATE,          
    ISNULL(AGNL.AGENCY_ZIP,' ')  AS AGENCY_ZIP,          
    PCPL.PRODUCER AS PRODUCER_ID,          
    MUL.SUB_CODE AS PRODUCER_CODE,          
    ISNULL(MUL.USER_FNAME,'')  + ' ' + ISNULL(MUL.USER_LNAME,'') AS PRODUCER_NAME ,          
    'N' as IS_INTERIM          
    FROM         
    (         
     select  CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
     GROSS_AMOUNT, MCCA_FEES , NET_AMOUNT , COMMISSION_RATE ,         
     SUM(COMMISSION_AMOUNT) AS COMMISSION_AMOUNT         
     ,BILL_TYPE ,  TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
     from #STATEMENT         
     where bill_type = 'AB'        
     group by CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID  , COMMISSION_RATE ,        
     GROSS_AMOUNT  , MCCA_FEES ,NET_AMOUNT, BILL_TYPE , TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    UNION         
    
     select      
     CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
     SUM(GROSS_AMOUNT) AS GROSS_AMOUNT,         
     SUM(MCCA_FEES) AS MCCA_FEES,         
     SUM(NET_AMOUNT)  AS NET_AMOUNT,         
     COMMISSION_RATE ,         
     SUM(COMMISSION_AMOUNT) AS COMMISSION_AMOUNT         
     ,BILL_TYPE ,  TRAN_TYPE,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
     from #STATEMENT         
     where bill_type = 'DB'        
     group by CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID  ,        
     COMMISSION_RATE ,BILL_TYPE ,TRAN_TYPE,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    ) TMP         
    INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL         
    ON PCPL.POLICY_ID = TMP.POLICY_ID             
    AND PCPL.POLICY_VERSION_ID = TMP.POLICY_VERSION_ID             
    AND PCPL.CUSTOMER_ID = TMP.CUSTOMER_ID            
    INNER JOIN CLT_CUSTOMER_LIST CCL ON CCL.CUSTOMER_ID = TMP.CUSTOMER_ID            
    INNER JOIN MNT_LOB_MASTER LOB ON LOB.LOB_ID = PCPL.POLICY_LOB            
    LEFT  JOIN MNT_AGENCY_LIST AGNL ON AGNL.AGENCY_ID = TMP.AGENCY_ID            
    LEFT  JOIN MNT_USER_LIST MUL ON PCPL.PRODUCER = MUL.[USER_ID]          
    ORDER BY AGNL.AGENCY_DISPLAY_NAME,PCPL.BILL_TYPE ,PCPL.POLICY_NUMBER        
    
   END        
  END          
 ELSE          
  BEGIN     
    
 --SET @IS_INTERIM = 'Y'           
   EXEC Proc_ProcessAgencyStatement @MONTH,@YEAR,@COMM_TYPE          
    
   --IF COMMISION TYPE IS COMPLETE APP COMMISSION(CAC):          
   --THEN PICK UP THE CSR AND PRODUCER DETAILS(CODE AND NAME)          
   IF(@COMM_TYPE = 'CAC')          
   BEGIN      
    insert into #TEMP_STATEMENT      
    (      
     CUSTOMER_NAME,          
     POLICY_NUMBER ,        
     SOURCE_EFF_DATE,         
     --TRAN_TYPE,      
     GROSS_PREMIUM,         
     MCCA_FEES,      
     MCCA_FEES1,         
     PREMIUM,      
     PREMIUM1,          
     COMMISSION_RATE,       
     AMOUNT_FOR_CALCULATION,      
     BILL_TYPE,        
     COMMISSION_AMOUNT,      
     DUE_AMOUNT,         
     LOB_CODE,      
     TRAN_TYPE,        
     AGENCY_DISPLAY_NAME,          
     AGENCY_CODE,           
     AGENCY_ADD1,          
     AGENCY_ADD2,          
     AGENCY_CITY,          
     AGENCY_STATE,          
     AGENCY_ZIP,          
     CSR_ID,          
     CSR_CODE,          
     CSR_NAME,            
     IS_INTERIM         
    )            
    SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,            
    PCPL.POLICY_NUMBER, CONVERT(VARCHAR(20), AAS.SOURCE_EFF_DATE, 101) SOURCE_EFF_DATE,             
    AAS.GROSS_PREMIUM AS GROSS_PREMIUM,       
    AAS.FEES AS MCCA_FEES,        
    CASE WHEN TRAN_TYPE != 'PIC' THEN AAS.FEES ELSE 0 END AS MCCA_FEES1,           
    AAS.PREMIUM_AMOUNT PREMIUM,         
    CASE WHEN TRAN_TYPE != 'PIC' THEN AAS.PREMIUM_AMOUNT ELSE 0 END AS PREMIUM1,         
    AAS.COMMISSION_RATE,            
    AMOUNT_FOR_CALCULATION,       
    AAS.BILL_TYPE,            
    AAS.COMMISSION_AMOUNT,       
    AAS.DUE_AMOUNT,       
    LOB.LOB_CODE AS LOB_CODE,       
    ISNULL(TRAN_CODE,'') + '-' + ISNULL(TRAN_TYPE,'') AS TRAN_TYPE,            
    ISNULL(AGNL.AGENCY_DISPLAY_NAME,' ')  + ' - ' + ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_DISPLAY_NAME,          
    ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_CODE,           
    ISNULL(AGNL.AGENCY_ADD1,' ') AS AGENCY_ADD1,          
    ISNULL(AGNL.AGENCY_ADD2,' ') AS AGENCY_ADD2,          
    ISNULL(AGNL.AGENCY_CITY,' ') AS AGENCY_CITY,          
    (          
     SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST WHERE           
     COUNTRY_ID = AGNL.AGENCY_COUNTRY AND STATE_ID = AGNL.AGENCY_STATE          
    ) AS AGENCY_STATE,          
    ISNULL(AGNL.AGENCY_ZIP,' ')  AS AGENCY_ZIP,          
    PCPL.CSR AS CSR_ID,          
    MUL.SUB_CODE AS CSR_CODE,          
    ISNULL(MUL.USER_FNAME,'') + ' ' + ISNULL(MUL.USER_LNAME,'') AS CSR_NAME ,          
    'Y' as IS_INTERIM          
    FROM ##TEMP_ACT_AGENCY_STATEMENT AAS            
    LEFT JOIN CLT_CUSTOMER_LIST CCL ON CCL.CUSTOMER_ID = AAS.CUSTOMER_ID            
    LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON PCPL.POLICY_ID = AAS.POLICY_ID             
    AND PCPL.POLICY_VERSION_ID = AAS.POLICY_VERSION_ID           
    AND PCPL.CUSTOMER_ID = AAS.CUSTOMER_ID            
    LEFT JOIN MNT_LOB_MASTER LOB ON LOB.LOB_ID = PCPL.POLICY_LOB            
    LEFT JOIN MNT_AGENCY_LIST AGNL ON AGNL.AGENCY_ID = AAS.AGENCY_ID          
    --LEFT JOIN MNT_USER_LIST UL ON UL.USER_SYSTEM_ID = AGNL.AGENCY_CODE ----ADDED FOR CSR          
    --LEFT JOIN MNT_USER_TYPES UT ON UT.USER_TYPE_ID = UL.USER_TYPE_ID   --ADDED FOR CSR          
    LEFT JOIN MNT_USER_LIST MUL ON AAS.CSR_ID = MUL.[USER_ID]          
    LEFT JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI ON ACOI.IDEN_ROW_ID = AAS.CUSTOMER_OPEN_ITEM_ID            
    LEFT JOIN ACT_AGENCY_OPEN_ITEMS AGOI ON AGOI.IDEN_ROW_ID = AAS.AGENCY_OPEN_ITEM_ID            
    WHERE (AGOI.UPDATED_FROM = 'P' OR AGOI.UPDATED_FROM IS NULL) AND (ACOI.UPDATED_FROM = 'P' OR ACOI.UPDATED_FROM IS NULL)            
    AND AAS.AGENCY_ID = @CURRENT_AGENCY_ID           
    AND AAS.MONTH_NUMBER = @MONTH             
    AND AAS.MONTH_YEAR   = @YEAR          
    --AND UT.USER_TYPE_CODE = @USER_TYPE_CSR          
    ORDER BY AGENCY_DISPLAY_NAME,PCPL.BILL_TYPE , AAS.ROW_ID            
   END          
   ELSE          
   BEGIN          
    
    INSERT INTO #STATEMENT        
    (        
     CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
     MCCA_FEES , NET_AMOUNT , COMMISSION_RATE ,         
     COMMISSION_AMOUNT ,BILL_TYPE ,  TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    )        
    SELECT CUSTOMER_ID ,POLICY_ID ,         
    AAS.POLICY_VERSION_ID,         
    STAT_FEES_FOR_CALCULATION ,AMOUNT_FOR_CALCULATION , AAS.COMMISSION_RATE,        
    COMMISSION_AMOUNT ,BILL_TYPE ,         
    CASE TRAN_CODE   WHEN 'NBSP' Then 'REG' --'NBS'        
    WHEN 'NBSC' Then 'REG' --'NBS'        
    WHEN 'RENP' THEN 'REG' --'REN'        
    WHEN 'RENC' Then 'REG' --'REN'        
    WHEN 'ENDP' Then 'REG' --'END'        
    WHEN 'ENDC' THEN 'REG' --'END'          
    WHEN 'CANCP' Then 'REG' --'CAN'        
    WHEN 'CANC' THEN 'REG' --'CAN'           
    WHEN 'RINSP' Then 'REG' --'RIN'        
    WHEN 'RINC' Then  'REG' --'RIN'        
    ELSE TRAN_CODE        
    END ,        
    AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    from ##TEMP_ACT_AGENCY_STATEMENT AAS     where agency_id = @current_agency_id    
    
    insert into #TEMP_STATEMENT      
    (      
     CUSTOMER_NAME, POLICY_NUMBER ,    SOURCE_EFF_DATE,         
     --TRAN_TYPE,      
     GROSS_PREMIUM,     MCCA_FEES,MCCA_FEES1,     PREMIUM,  PREMIUM1,  COMMISSION_RATE,       
     COMMISSION_AMOUNT,     LOB_CODE,  BILL_TYPE,     TRAN_TYPE,    AGENCY_DISPLAY_NAME,          
     AGENCY_CODE,       AGENCY_ADD1,      AGENCY_ADD2,AGENCY_CITY,      AGENCY_STATE,          
     AGENCY_ZIP,      PRODUCER_ID,      PRODUCER_CODE,      PRODUCER_NAME,      IS_INTERIM           
    )        
    SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,            
    PCPL.POLICY_NUMBER AS POLICY_NUMBER,        
    CONVERT(VARCHAR,PCPL.POL_VER_EFFECTIVE_DATE ,101) AS SOURCE_EFF_DATE,         
    CASE TMP.TRAN_TYPE WHEN 'PIC' THEN NULL ELSE TMP.GROSS_AMOUNT  END AS GROSS_PREMIUM,         
    CASE TMP.TRAN_TYPE WHEN 'PIC' THEN NULL ELSE TMP.MCCA_FEES END AS MCCA_FEES ,     
    CASE WHEN TMP.TRAN_TYPE != 'PIC' THEN TMP.MCCA_FEES ELSE 0 END AS MCCA_FEES1 ,        
    CASE TMP.TRAN_TYPE WHEN 'PIC' THEN NULL ELSE TMP.NET_AMOUNT END AS PREMIUM,     
    CASE WHEN TMP.TRAN_TYPE != 'PIC' THEN TMP.NET_AMOUNT ELSE 0 END AS PREMIUM1,         
    
    TMP.COMMISSION_RATE  AS COMMISSION_RATE, TMP.COMMISSION_AMOUNT AS COMMISSION_AMOUNT,         
    LOB.LOB_CODE AS LOB_CODE,TMP.BILL_TYPE AS BILL_TYPE ,         
    TMP.TRAN_TYPE,        
    ISNULL(AGNL.AGENCY_DISPLAY_NAME,' ') AS AGENCY_DISPLAY_NAME,          
    ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_CODE,           
    ISNULL(AGNL.AGENCY_ADD1,' ') AS AGENCY_ADD1,          
    ISNULL(AGNL.AGENCY_ADD2,' ') AS AGENCY_ADD2,          
    ISNULL(AGNL.AGENCY_CITY,' ') AS AGENCY_CITY,          
    (          
     SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST WHERE           
     COUNTRY_ID = AGNL.AGENCY_COUNTRY AND STATE_ID = AGNL.AGENCY_STATE          
    ) AS AGENCY_STATE,          
    ISNULL(AGNL.AGENCY_ZIP,' ')  AS AGENCY_ZIP,          
    PCPL.PRODUCER AS PRODUCER_ID,          
    MUL.SUB_CODE AS PRODUCER_CODE,          
    ISNULL(MUL.USER_FNAME,'')  + ' ' + ISNULL(MUL.USER_LNAME,'') AS PRODUCER_NAME ,          
    'Y' as IS_INTERIM          
    FROM         
    (         
     select  CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
     GROSS_AMOUNT,         
     MCCA_FEES,         
     NET_AMOUNT ,         
     COMMISSION_RATE ,         
     SUM(COMMISSION_AMOUNT) AS COMMISSION_AMOUNT         
     ,BILL_TYPE ,          
     TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
     from #STATEMENT         
     where bill_type = 'AB'        
     group by CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID          
     , COMMISSION_RATE ,        
     GROSS_AMOUNT  , MCCA_FEES ,NET_AMOUNT, BILL_TYPE ,         
     TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    UNION         
    
     select         
     CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
     SUM(GROSS_AMOUNT) AS GROSS_AMOUNT,         
     SUM(MCCA_FEES) AS MCCA_FEES,         
     SUM(NET_AMOUNT)  AS NET_AMOUNT,         
     COMMISSION_RATE ,         
     SUM(COMMISSION_AMOUNT) AS COMMISSION_AMOUNT         
     ,BILL_TYPE ,          
     TRAN_TYPE,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
     from #STATEMENT         
     where bill_type = 'DB'        
     group by CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID         
     ,COMMISSION_RATE ,BILL_TYPE ,        
     TRAN_TYPE,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    ) TMP         
    INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL         
    ON PCPL.POLICY_ID = TMP.POLICY_ID             
    AND PCPL.POLICY_VERSION_ID = TMP.POLICY_VERSION_ID             
    AND PCPL.CUSTOMER_ID = TMP.CUSTOMER_ID            
    INNER JOIN CLT_CUSTOMER_LIST CCL ON CCL.CUSTOMER_ID = TMP.CUSTOMER_ID            
    INNER JOIN MNT_LOB_MASTER LOB ON LOB.LOB_ID = PCPL.POLICY_LOB            
    LEFT  JOIN MNT_AGENCY_LIST AGNL ON AGNL.AGENCY_ID = TMP.AGENCY_ID            
    LEFT  JOIN MNT_USER_LIST MUL ON PCPL.PRODUCER = MUL.[USER_ID]          
    WHERE  TMP.AGENCY_ID = @CURRENT_AGENCY_ID              
    AND TMP.MONTH_NUMBER = @MONTH             
    AND TMP.MONTH_YEAR = @YEAR          
    ORDER BY AGNL.AGENCY_DISPLAY_NAME,PCPL.BILL_TYPE ,PCPL.POLICY_NUMBER        
    
    
   END         
   --INSERT INTO #TEMP_NEW AS SELECT * FROM ##TEMP_ACT_AGENCY_STATEMENT         
   DROP TABLE ##TEMP_ACT_AGENCY_STATEMENT          
  END       
  IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'#STATEMENT') AND OBJECTPROPERTY(ID, N'ISUSERTABLE') = 1)      
  begin        
  DROP TABLE [#STATEMENT]      
 END       
    
 SET @SCOUNT=@SCOUNT+1                       
 SET @CURRENT_AGENCY_ID=DBO.PIECE(@AGENCY_ID,',',@SCOUNT)                  
     
    
END      
    
END        
    
ELSE -- SELECT ALL CASE      
BEGIN      
    
 IF EXISTS(SELECT AGENCY_ID FROM ACT_AGENCY_STATEMENT where  ((MONTH_NUMBER >= @MONTH  AND MONTH_YEAR = @YEAR) OR (MONTH_YEAR > @YEAR)) AND COMM_TYPE = @COMM_TYPE)         
 BEGIN          
  IF(@COMM_TYPE = 'CAC')          
  BEGIN       
    
   insert into #TEMP_STATEMENT      
   (      
   CUSTOMER_NAME,   POLICY_NUMBER ,    SOURCE_EFF_DATE,         
   --TRAN_TYPE,      
   GROSS_PREMIUM,     MCCA_FEES,  MCCA_FEES1,     PREMIUM,  PREMIUM1,      COMMISSION_RATE,   AMOUNT_FOR_CALCULATION,      
   BILL_TYPE,    COMMISSION_AMOUNT,  DUE_AMOUNT,     LOB_CODE,  TRAN_TYPE,    AGENCY_DISPLAY_NAME,      AGENCY_CODE,           
   AGENCY_ADD1,      AGENCY_ADD2,     AGENCY_CITY,      AGENCY_STATE,      AGENCY_ZIP,      CSR_ID,      CSR_CODE,          
   CSR_NAME,        IS_INTERIM     )               
   SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,            
   PCPL.POLICY_NUMBER, CONVERT(VARCHAR(20), AAS.SOURCE_EFF_DATE, 101) SOURCE_EFF_DATE,             
   AAS.GROSS_PREMIUM AS GROSS_PREMIUM, AAS.FEES AS MCCA_FEES,        
   CASE WHEN TRAN_TYPE != 'PIC' THEN AAS.FEES ELSE 0 END AS MCCA_FEES1,         
   AAS.PREMIUM_AMOUNT PREMIUM,         
   CASE WHEN TRAN_TYPE != 'PIC' THEN AAS.PREMIUM_AMOUNT ELSE 0 END AS PREMIUM1,        
   AAS.COMMISSION_RATE,            
   AMOUNT_FOR_CALCULATION, AAS.BILL_TYPE,            
   AAS.COMMISSION_AMOUNT, AAS.DUE_AMOUNT, LOB.LOB_CODE AS LOB_CODE, ISNULL(TRAN_CODE,'') + '-' + ISNULL(TRAN_TYPE,'') AS TRAN_TYPE,            
   ISNULL(AGNL.AGENCY_DISPLAY_NAME,' ') + ' - ' + ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_DISPLAY_NAME,          
   ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_CODE,           
   ISNULL(AGNL.AGENCY_ADD1,' ') AS AGENCY_ADD1,          
   ISNULL(AGNL.AGENCY_ADD2,' ') AS AGENCY_ADD2,          
   ISNULL(AGNL.AGENCY_CITY,' ') AS AGENCY_CITY,          
   (          
   SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE           
   COUNTRY_ID = AGNL.AGENCY_COUNTRY AND STATE_ID = AGNL.AGENCY_STATE          
   ) AS AGENCY_STATE,          
   ISNULL(AGNL.AGENCY_ZIP,' ')  AS AGENCY_ZIP,          
   PCPL.CSR AS CSR_ID,          
   MUL.SUB_CODE AS CSR_CODE,          
   ISNULL(MUL.USER_FNAME,'') + ' ' + ISNULL(MUL.USER_LNAME,'') AS CSR_NAME ,          
   'N' as IS_INTERIM          
   FROM ACT_AGENCY_STATEMENT AAS (NOLOCK)           
   LEFT JOIN CLT_CUSTOMER_LIST CCL (NOLOCK) ON CCL.CUSTOMER_ID = AAS.CUSTOMER_ID            
   LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL (NOLOCK) ON PCPL.POLICY_ID = AAS.POLICY_ID             
   AND PCPL.POLICY_VERSION_ID = AAS.POLICY_VERSION_ID             
   AND PCPL.CUSTOMER_ID = AAS.CUSTOMER_ID            
   LEFT JOIN MNT_LOB_MASTER LOB (NOLOCK) ON LOB.LOB_ID = PCPL.POLICY_LOB            
   LEFT JOIN MNT_AGENCY_LIST AGNL (NOLOCK) ON AGNL.AGENCY_ID = AAS.AGENCY_ID          
   --LEFT JOIN MNT_USER_LIST UL ON UL.USER_SYSTEM_ID = AGNL.AGENCY_CODE ----ADDED FOR CSR          
   --LEFT JOIN MNT_USER_TYPES UT ON UT.USER_TYPE_ID = UL.USER_TYPE_ID   --ADDED FOR CSR          
   LEFT JOIN MNT_USER_LIST MUL (NOLOCK) ON AAS.CSR_ID = MUL.[USER_ID]          
   LEFT JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI (NOLOCK) ON ACOI.IDEN_ROW_ID = AAS.CUSTOMER_OPEN_ITEM_ID            
   LEFT JOIN ACT_AGENCY_OPEN_ITEMS AGOI (NOLOCK) ON AGOI.IDEN_ROW_ID = AAS.AGENCY_OPEN_ITEM_ID            
   WHERE (AGOI.UPDATED_FROM = 'P' OR AGOI.UPDATED_FROM IS NULL)     
   AND (ACOI.UPDATED_FROM = 'P' OR ACOI.UPDATED_FROM IS NULL)            
   --AND AAS.AGENCY_ID = @AGENCY_ID          
   AND AAS.MONTH_NUMBER = @MONTH             
   AND AAS.MONTH_YEAR   = @YEAR          
   AND AAS.COMM_TYPE = @COMM_TYPE        
   ORDER BY AGNL.AGENCY_DISPLAY_NAME,PCPL.BILL_TYPE , AAS.ROW_ID            
  END          
  ELSE          
  BEGIN          
   INSERT INTO #STATEMENT        
   (        
   CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
   MCCA_FEES , NET_AMOUNT , COMMISSION_RATE ,         
   COMMISSION_AMOUNT ,BILL_TYPE ,  TRAN_TYPE ,        
   AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
   )        
   SELECT AAS.CUSTOMER_ID ,AAS.POLICY_ID ,       
   AAS.POLICY_VERSION_ID,         
   AAS.STAT_FEES_FOR_CALCULATION ,AAS.AMOUNT_FOR_CALCULATION , AAS.COMMISSION_RATE,        
   AAS.COMMISSION_AMOUNT ,AAS.BILL_TYPE ,         
   CASE AAS.TRAN_CODE   WHEN 'NBSP'  Then 'REG' --'NBS'        
   WHEN 'NBSC'  Then 'REG' --'NBS'        
   WHEN 'RENP'  THEN 'REG' --'REN'        
   WHEN 'RENC'  Then 'REG' --'REN'        
   WHEN 'ENDP'  Then 'REG' --'END'        
   WHEN 'ENDC'  THEN 'REG' --'END'          
   WHEN 'CANCP'  Then 'REG' --'CAN'        
   WHEN 'CANC'  THEN 'REG' --'CAN'           
   WHEN 'RINSP'  Then 'REG' --'RIN'        
   WHEN 'RINC' Then  'REG' --'RIN'        
   ELSE TRAN_CODE        
   END ,        
   AAS.AGENCY_ID , AAS.MONTH_NUMBER ,  AAS.MONTH_YEAR         
   from ACT_AGENCY_STATEMENT AAS (NOLOCK)        
   LEFT JOIN ACT_AGENCY_OPEN_ITEMS AGOI (NOLOCK)        
   ON AGOI.IDEN_ROW_ID = AAS.AGENCY_OPEN_ITEM_ID         
   LEFT JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI  (NOLOCK)       
   ON ACOI.IDEN_ROW_ID = AAS.CUSTOMER_OPEN_ITEM_ID            
   WHERE (AGOI.UPDATED_FROM = 'P' OR AGOI.UPDATED_FROM IS NULL)     
   AND (ACOI.UPDATED_FROM = 'P' OR ACOI.UPDATED_FROM IS NULL)            
   --AND AAS.AGENCY_ID = @AGENCY_ID             
   AND AAS.MONTH_NUMBER = @MONTH             
   AND AAS.MONTH_YEAR = @YEAR            
   AND AAS.COMM_TYPE = @COMM_TYPE          
    
   insert into #TEMP_STATEMENT      
   (      
   CUSTOMER_NAME,POLICY_NUMBER ,    SOURCE_EFF_DATE,     GROSS_PREMIUM,     MCCA_FEES,  MCCA_FEES1,     PREMIUM,      
   PREMIUM1,      COMMISSION_RATE,  COMMISSION_AMOUNT,   LOB_CODE,  BILL_TYPE,TRAN_TYPE,  AGENCY_DISPLAY_NAME,          
   AGENCY_CODE,       AGENCY_ADD1,      AGENCY_ADD2,      AGENCY_CITY,      AGENCY_STATE,      AGENCY_ZIP,          
   PRODUCER_ID,      PRODUCER_CODE,      PRODUCER_NAME,      IS_INTERIM         
   )            
   SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,            
   PCPL.POLICY_NUMBER AS POLICY_NUMBER,        
   CONVERT(VARCHAR,PCPL.POL_VER_EFFECTIVE_DATE ,101) AS SOURCE_EFF_DATE,         
   TMP.GROSS_AMOUNT AS GROSS_PREMIUM, TMP.MCCA_FEES MCCA_FEES ,        
   CASE WHEN TMP.TRAN_TYPE != 'PIC' THEN TMP.MCCA_FEES ELSE 0 END AS MCCA_FEES1,          
   TMP.NET_AMOUNT AS PREMIUM,         
   CASE WHEN TMP.TRAN_TYPE != 'PIC' THEN TMP.NET_AMOUNT ELSE 0 END AS PREMIUM1,         
   TMP.COMMISSION_RATE  AS COMMISSION_RATE, TMP.COMMISSION_AMOUNT AS COMMISSION_AMOUNT,         
   LOB.LOB_CODE AS LOB_CODE,TMP.BILL_TYPE AS BILL_TYPE , TMP.TRAN_TYPE,        
   ISNULL(AGNL.AGENCY_DISPLAY_NAME,' ') AS AGENCY_DISPLAY_NAME,          
   ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_CODE,           
   ISNULL(AGNL.AGENCY_ADD1,' ') AS AGENCY_ADD1,          
   ISNULL(AGNL.AGENCY_ADD2,' ') AS AGENCY_ADD2,          
   ISNULL(AGNL.AGENCY_CITY,' ') AS AGENCY_CITY,          
   (          
   SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE           
   COUNTRY_ID = AGNL.AGENCY_COUNTRY AND STATE_ID = AGNL.AGENCY_STATE          
   ) AS AGENCY_STATE,          
   ISNULL(AGNL.AGENCY_ZIP,' ')  AS AGENCY_ZIP,          
   PCPL.PRODUCER AS PRODUCER_ID,          
   MUL.SUB_CODE AS PRODUCER_CODE,          
   ISNULL(MUL.USER_FNAME,'')  + ' ' + ISNULL(MUL.USER_LNAME,'') AS PRODUCER_NAME ,          
   'N' as IS_INTERIM          
   FROM         
   (         
   select  CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
   GROSS_AMOUNT, MCCA_FEES , NET_AMOUNT , COMMISSION_RATE ,         
   SUM(COMMISSION_AMOUNT) AS COMMISSION_AMOUNT         
   ,BILL_TYPE ,  TRAN_TYPE ,        
   AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
   from #STATEMENT (NOLOCK)        
   where bill_type = 'AB'        
   group by CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID  , COMMISSION_RATE ,        
   GROSS_AMOUNT  , MCCA_FEES ,NET_AMOUNT, BILL_TYPE , TRAN_TYPE ,        
   AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
   UNION         
    
   select         
   CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,           SUM(GROSS_AMOUNT) AS GROSS_AMOUNT,         
   SUM(MCCA_FEES) AS MCCA_FEES,         
   SUM(NET_AMOUNT)  AS NET_AMOUNT,         
   COMMISSION_RATE ,         
   SUM(COMMISSION_AMOUNT) AS COMMISSION_AMOUNT         
   ,BILL_TYPE ,  TRAN_TYPE,        
   AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
   from #STATEMENT  (NOLOCK)       
   where bill_type = 'DB'        
   group by CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID  ,        
   COMMISSION_RATE ,BILL_TYPE ,TRAN_TYPE,        
   AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
   ) TMP         
   INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL (NOLOCK)        
   ON PCPL.POLICY_ID = TMP.POLICY_ID             
   AND PCPL.POLICY_VERSION_ID = TMP.POLICY_VERSION_ID             
   AND PCPL.CUSTOMER_ID = TMP.CUSTOMER_ID            
   INNER JOIN CLT_CUSTOMER_LIST CCL (NOLOCK) ON CCL.CUSTOMER_ID = TMP.CUSTOMER_ID            
   INNER JOIN MNT_LOB_MASTER LOB (NOLOCK) ON LOB.LOB_ID = PCPL.POLICY_LOB            
   LEFT  JOIN MNT_AGENCY_LIST AGNL (NOLOCK) ON AGNL.AGENCY_ID = TMP.AGENCY_ID            
   LEFT  JOIN MNT_USER_LIST MUL (NOLOCK) ON PCPL.PRODUCER = MUL.[USER_ID]          
   ORDER BY AGNL.AGENCY_DISPLAY_NAME,PCPL.BILL_TYPE ,PCPL.POLICY_NUMBER        
    
  END        
 END        
 -- IF EXISTS (SELECT AGENCY_ID  FROM ACT_AGENCY_STATEMENT WHERE AGENCY_ID NOT IN (SELECT AGENCY_ID FROM ACT_AGENCY_STATEMENT WHERE  ((MONTH_NUMBER >= @MONTH  AND MONTH_YEAR = @YEAR) OR (MONTH_YEAR > @YEAR)) AND COMM_TYPE = @COMM_TYPE)    )      
 ELSE    
 BEGIN          
  --SET @IS_INTERIM = 'Y'           
  EXEC Proc_ProcessAgencyStatement @MONTH,@YEAR,@COMM_TYPE          
    
  --IF COMMISION TYPE IS COMPLETE APP COMMISSION(CAC):          
  --THEN PICK UP THE CSR AND PRODUCER DETAILS(CODE AND NAME)          
  IF(@COMM_TYPE = 'CAC')          
  BEGIN       
   insert into #TEMP_STATEMENT      
   (      
   CUSTOMER_NAME,          
   POLICY_NUMBER ,        
   SOURCE_EFF_DATE,         
   --TRAN_TYPE,      
   GROSS_PREMIUM,         
   MCCA_FEES,      
   MCCA_FEES1,         
   PREMIUM,      
   PREMIUM1,          
   COMMISSION_RATE,       
   AMOUNT_FOR_CALCULATION,      
   BILL_TYPE,        
   COMMISSION_AMOUNT,      
   DUE_AMOUNT,         
   LOB_CODE,      
   TRAN_TYPE,        
   AGENCY_DISPLAY_NAME,          
   AGENCY_CODE,           
   AGENCY_ADD1,          
   AGENCY_ADD2,          
   AGENCY_CITY,          
   AGENCY_STATE,          
   AGENCY_ZIP,          
   CSR_ID,          
   CSR_CODE,          
   CSR_NAME,            
   IS_INTERIM         
   )       
    
    SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,            
    PCPL.POLICY_NUMBER, CONVERT(VARCHAR(20), AAS.SOURCE_EFF_DATE, 101) SOURCE_EFF_DATE,             
    AAS.GROSS_PREMIUM AS GROSS_PREMIUM, AAS.FEES AS MCCA_FEES,        
    CASE WHEN TRAN_TYPE != 'PIC' THEN AAS.FEES ELSE 0 END AS MCCA_FEES1,           
    AAS.PREMIUM_AMOUNT PREMIUM,         
    CASE WHEN TRAN_TYPE != 'PIC' THEN AAS.PREMIUM_AMOUNT ELSE 0 END AS PREMIUM1,         
    AAS.COMMISSION_RATE,            
    AMOUNT_FOR_CALCULATION, AAS.BILL_TYPE,            
    AAS.COMMISSION_AMOUNT, AAS.DUE_AMOUNT, LOB.LOB_CODE AS LOB_CODE, ISNULL(TRAN_CODE,'') + '-' + ISNULL(TRAN_TYPE,'') AS TRAN_TYPE,            
    ISNULL(AGNL.AGENCY_DISPLAY_NAME,' ') + ' - ' + ISNULL(AGNL.AGENCY_CODE,' ')  AS AGENCY_DISPLAY_NAME,          
    ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_CODE,           
    ISNULL(AGNL.AGENCY_ADD1,' ') AS AGENCY_ADD1,          
    ISNULL(AGNL.AGENCY_ADD2,' ') AS AGENCY_ADD2,          
    ISNULL(AGNL.AGENCY_CITY,' ') AS AGENCY_CITY,          
    (          
     SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE           
     COUNTRY_ID = AGNL.AGENCY_COUNTRY AND STATE_ID = AGNL.AGENCY_STATE          
    ) AS AGENCY_STATE,          
    ISNULL(AGNL.AGENCY_ZIP,' ')  AS AGENCY_ZIP,          
    PCPL.CSR AS CSR_ID,          
    MUL.SUB_CODE AS CSR_CODE,          
    ISNULL(MUL.USER_FNAME,'') + ' ' + ISNULL(MUL.USER_LNAME,'') AS CSR_NAME ,          
    'Y' as IS_INTERIM      
    FROM ##TEMP_ACT_AGENCY_STATEMENT AAS (NOLOCK)           
    LEFT JOIN CLT_CUSTOMER_LIST CCL (NOLOCK) ON CCL.CUSTOMER_ID = AAS.CUSTOMER_ID      
 LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL (NOLOCK) ON PCPL.POLICY_ID = AAS.POLICY_ID             
    AND PCPL.POLICY_VERSION_ID = AAS.POLICY_VERSION_ID             
    AND PCPL.CUSTOMER_ID = AAS.CUSTOMER_ID            
    LEFT JOIN MNT_LOB_MASTER LOB (NOLOCK) ON LOB.LOB_ID = PCPL.POLICY_LOB            
    LEFT JOIN MNT_AGENCY_LIST AGNL (NOLOCK) ON AGNL.AGENCY_ID = AAS.AGENCY_ID          
    --LEFT JOIN MNT_USER_LIST UL ON UL.USER_SYSTEM_ID = AGNL.AGENCY_CODE ----ADDED FOR CSR          
    --LEFT JOIN MNT_USER_TYPES UT ON UT.USER_TYPE_ID = UL.USER_TYPE_ID   --ADDED FOR CSR          
    LEFT JOIN MNT_USER_LIST MUL (NOLOCK) ON AAS.CSR_ID = MUL.[USER_ID]          
    LEFT JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI (NOLOCK) ON ACOI.IDEN_ROW_ID = AAS.CUSTOMER_OPEN_ITEM_ID            
    LEFT JOIN ACT_AGENCY_OPEN_ITEMS AGOI (NOLOCK) ON AGOI.IDEN_ROW_ID = AAS.AGENCY_OPEN_ITEM_ID            
    WHERE (AGOI.UPDATED_FROM = 'P' OR AGOI.UPDATED_FROM IS NULL)     
 AND (ACOI.UPDATED_FROM = 'P' OR ACOI.UPDATED_FROM IS NULL)            
    --AND AAS.AGENCY_ID = @AGENCY_ID          
    AND AAS.MONTH_NUMBER = @MONTH             
    AND AAS.MONTH_YEAR   = @YEAR          
    --AND UT.USER_TYPE_CODE = @USER_TYPE_CSR          
    ORDER BY AGNL.AGENCY_DISPLAY_NAME,PCPL.BILL_TYPE , AAS.ROW_ID            
   END          
   ELSE          
   BEGIN          
      
      
    INSERT INTO #STATEMENT        
    (        
     CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
  MCCA_FEES , NET_AMOUNT , COMMISSION_RATE ,         
     COMMISSION_AMOUNT ,BILL_TYPE ,  TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    )        
    SELECT CUSTOMER_ID ,POLICY_ID ,         
    AAS.POLICY_VERSION_ID,         
    STAT_FEES_FOR_CALCULATION ,AMOUNT_FOR_CALCULATION , AAS.COMMISSION_RATE,        
    COMMISSION_AMOUNT ,BILL_TYPE ,         
    CASE TRAN_CODE   WHEN 'NBSP' Then 'REG' --'NBS'        
    WHEN 'NBSC' Then 'REG' --'NBS'        
    WHEN 'RENP' THEN 'REG' --'REN'        
    WHEN 'RENC' Then 'REG' --'REN'        
    WHEN 'ENDP' Then 'REG' --'END'   
    WHEN 'ENDC' THEN 'REG' --'END'          
    WHEN 'CANCP' Then 'REG' --'CAN'        
    WHEN 'CANC' THEN 'REG' --'CAN'           
    WHEN 'RINSP' Then 'REG' --'RIN'        
    WHEN 'RINC' Then  'REG' --'RIN'        
    ELSE TRAN_CODE        
    END ,        
    AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    from ##TEMP_ACT_AGENCY_STATEMENT AAS  (NOLOCK)       
      
     insert into #TEMP_STATEMENT      
      (      
      CUSTOMER_NAME,          
      POLICY_NUMBER ,        
      SOURCE_EFF_DATE,         
      --TRAN_TYPE,      
      GROSS_PREMIUM,         
      MCCA_FEES,    
      MCCA_FEES1,         
      PREMIUM,          
      PREMIUM1,           
      COMMISSION_RATE,       
      COMMISSION_AMOUNT,         
      LOB_CODE,      
      BILL_TYPE,         
      TRAN_TYPE,        
      AGENCY_DISPLAY_NAME,          
      AGENCY_CODE,           
      AGENCY_ADD1,          
      AGENCY_ADD2,          
      AGENCY_CITY,          
      AGENCY_STATE,          
      AGENCY_ZIP,          
      PRODUCER_ID,          
      PRODUCER_CODE,          
      PRODUCER_NAME,          
      IS_INTERIM           
      )        
    SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') CUSTOMER_NAME,            
    PCPL.POLICY_NUMBER AS POLICY_NUMBER,        
    CONVERT(VARCHAR,PCPL.POL_VER_EFFECTIVE_DATE ,101) AS SOURCE_EFF_DATE,         
    CASE TMP.TRAN_TYPE WHEN 'PIC' THEN NULL ELSE TMP.GROSS_AMOUNT  END AS GROSS_PREMIUM,         
    CASE TMP.TRAN_TYPE WHEN 'PIC' THEN NULL ELSE TMP.MCCA_FEES END AS MCCA_FEES ,         
--3April     
    CASE WHEN TMP.TRAN_TYPE != 'PIC' THEN TMP.MCCA_FEES ELSE 0 END AS MCCA_FEES1 ,         
    CASE TMP.TRAN_TYPE WHEN 'PIC' THEN NULL ELSE TMP.NET_AMOUNT END AS PREMIUM,        
--3April    
    CASE WHEN TMP.TRAN_TYPE != 'PIC' THEN TMP.NET_AMOUNT ELSE 0 END AS PREMIUM1,         
--    TMP.GROSS_AMOUNT  AS GROSS_PREMIUM,         
--    TMP.MCCA_FEES AS MCCA_FEES ,         
--    TMP.NET_AMOUNT AS PREMIUM,         
    TMP.COMMISSION_RATE  AS COMMISSION_RATE, TMP.COMMISSION_AMOUNT AS COMMISSION_AMOUNT,         
    LOB.LOB_CODE AS LOB_CODE,TMP.BILL_TYPE AS BILL_TYPE ,         
    TMP.TRAN_TYPE,        
    ISNULL(AGNL.AGENCY_DISPLAY_NAME,' ') AS AGENCY_DISPLAY_NAME,          
    ISNULL(AGNL.AGENCY_CODE,' ') AS AGENCY_CODE,           
    ISNULL(AGNL.AGENCY_ADD1,' ') AS AGENCY_ADD1,          
    ISNULL(AGNL.AGENCY_ADD2,' ') AS AGENCY_ADD2,          
    ISNULL(AGNL.AGENCY_CITY,' ') AS AGENCY_CITY,          
    (          
     SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE           
     COUNTRY_ID = AGNL.AGENCY_COUNTRY AND STATE_ID = AGNL.AGENCY_STATE          
    ) AS AGENCY_STATE,          
    ISNULL(AGNL.AGENCY_ZIP,' ')  AS AGENCY_ZIP,          
    PCPL.PRODUCER AS PRODUCER_ID,          
    MUL.SUB_CODE AS PRODUCER_CODE,          
    ISNULL(MUL.USER_FNAME,'')  + ' ' + ISNULL(MUL.USER_LNAME,'') AS PRODUCER_NAME ,          
    'Y' as IS_INTERIM          
    FROM         
    (         
     select  CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
     GROSS_AMOUNT,         
     MCCA_FEES,         
     NET_AMOUNT ,         
     COMMISSION_RATE ,         
     SUM(COMMISSION_AMOUNT) AS COMMISSION_AMOUNT         
     ,BILL_TYPE ,          
     TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
     from #STATEMENT  (NOLOCK)       
     where bill_type = 'AB'        
     group by CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID          
     , COMMISSION_RATE ,        
     GROSS_AMOUNT  , MCCA_FEES ,NET_AMOUNT, BILL_TYPE , TRAN_TYPE ,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
     UNION         
      
     select         
     CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID ,        
     SUM(GROSS_AMOUNT) AS GROSS_AMOUNT,         
     SUM(MCCA_FEES) AS MCCA_FEES,         
     SUM(NET_AMOUNT)  AS NET_AMOUNT,         
     COMMISSION_RATE ,         
     SUM(COMMISSION_AMOUNT) AS COMMISSION_AMOUNT         
     ,BILL_TYPE ,          
     TRAN_TYPE,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
     from #STATEMENT  (NOLOCK)       
     where bill_type = 'DB'        
     group by CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID         
     ,COMMISSION_RATE ,BILL_TYPE ,        
     TRAN_TYPE,        
     AGENCY_ID , MONTH_NUMBER ,  MONTH_YEAR         
    ) TMP         
    INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL (NOLOCK)        
    ON PCPL.POLICY_ID = TMP.POLICY_ID             
    AND PCPL.POLICY_VERSION_ID = TMP.POLICY_VERSION_ID             
    AND PCPL.CUSTOMER_ID = TMP.CUSTOMER_ID            
    INNER JOIN CLT_CUSTOMER_LIST CCL (NOLOCK) ON CCL.CUSTOMER_ID = TMP.CUSTOMER_ID            
    INNER JOIN MNT_LOB_MASTER LOB (NOLOCK) ON LOB.LOB_ID = PCPL.POLICY_LOB            
    LEFT  JOIN MNT_AGENCY_LIST AGNL (NOLOCK) ON AGNL.AGENCY_ID = TMP.AGENCY_ID            
    LEFT  JOIN MNT_USER_LIST MUL ON PCPL.PRODUCER = MUL.[USER_ID]          
    WHERE  --TMP.AGENCY_ID = @AGENCY_ID             
    TMP.MONTH_NUMBER = @MONTH             
    AND TMP.MONTH_YEAR = @YEAR          
    ORDER BY AGNL.AGENCY_DISPLAY_NAME,PCPL.BILL_TYPE ,PCPL.POLICY_NUMBER        
      
      
   END          
   DROP TABLE ##TEMP_ACT_AGENCY_STATEMENT          
  END          
    
  IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'#STATEMENT') AND OBJECTPROPERTY(ID, N'ISUSERTABLE') = 1)      
   begin        
    DROP TABLE [#STATEMENT]      
   END       
      
 END       
   
IF ( @USER_TYPE_CODE = 'PRO' )    
BEGIN     
 SELECT * FROM       
 (      
  SELECT CUSTOMER_NAME , POLICY_NUMBER ,SOURCE_EFF_DATE , GROSS_PREMIUM ,         
  MCCA_FEES , MCCA_FEES1 , PREMIUM , PREMIUM1 , COMMISSION_RATE , COMMISSION_AMOUNT ,      
  AMOUNT_FOR_CALCULATION , LOB_CODE , DUE_AMOUNT , BILL_TYPE , TRAN_TYPE ,        
  AGENCY_DISPLAY_NAME , AGENCY_CODE , AGENCY_ADD1 , AGENCY_ADD2 ,          
  AGENCY_CITY , AGENCY_STATE , AGENCY_ZIP ,   
  CASE ISNULL(PRODUCER_ID,0) WHEN  -1 THEN 0 ELSE ISNULL(PRODUCER_ID,0) END AS PRODUCER_ID  ,          
  ISNULL(PRODUCER_CODE ,'') AS PRODUCER_CODE  , ISNULL(PRODUCER_NAME,'') AS PRODUCER_NAME, CSR_ID ,          
  CSR_CODE , CSR_NAME , IS_INTERIM      
   , CASE BILL_TYPE WHEN 'DB' THEN 0 ELSE 1 END AS SUB_ORDER        
  FROM #TEMP_STATEMENT  WHERE NOT (ISNULL(COMMISSION_AMOUNT ,0) = 0 AND ISNULL(PREMIUM ,0) = 0 )      
 )TEMP       
 ORDER BY AGENCY_DISPLAY_NAME,AGENCY_CODE,PRODUCER_CODE,SUB_ORDER,POLICY_NUMBER ,TRAN_TYPE      
END    
ELSE    
BEGIN     
 IF(@COMM_TYPE = 'CAC')          
 BEGIN     
  SELECT * FROM     
  (    
   SELECT  * ,CASE BILL_TYPE WHEN 'DB' THEN 0 ELSE 1 END AS SUB_ORDER      
  FROM #TEMP_STATEMENT  WHERE NOT (ISNULL(COMMISSION_AMOUNT ,0) = 0 AND ISNULL(PREMIUM ,0) = 0 )    
  )TEMP     
  ORDER BY AGENCY_DISPLAY_NAME,AGENCY_CODE,CSR_NAME , SUB_ORDER,POLICY_NUMBER ,TRAN_TYPE    
 END    
 ELSE     
 BEGIN     
  SELECT * FROM     
  (    
   SELECT * , CASE BILL_TYPE WHEN 'DB' THEN 0 ELSE 1 END AS SUB_ORDER      
   FROM #TEMP_STATEMENT  WHERE NOT (ISNULL(COMMISSION_AMOUNT ,0) = 0 AND ISNULL(PREMIUM ,0) = 0 )    
  )TEMP     
  ORDER BY AGENCY_DISPLAY_NAME , AGENCY_CODE,SUB_ORDER,POLICY_NUMBER ,TRAN_TYPE    
 END    
END    
    
--    
-- IF ( @COMM_TYPE = 'CAC')    
-- BEGIN    
--  SELECT * FROM #TEMP_STATEMENT  WHERE COMMISSION_AMOUNT <> 0     
--  ORDER BY AGENCY_DISPLAY_NAME    
-- end    
-- ELSE    
-- BEGIN     
--  SELECT * FROM #TEMP_STATEMENT  WHERE GROSS_PREMIUM <> 0     
--  ORDER BY AGENCY_DISPLAY_NAME    
-- END    
 drop table [#TEMP_STATEMENT]         
END       
    
    
--go         
--    
--exec Proc_GetAgencyStatement '' , 11 ,2008 , 'REG' ,'PRO'    
--    
--rollback tran    
--    
   
    


GO

