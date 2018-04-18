IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCancellationNoticeDataforPdf]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCancellationNoticeDataforPdf]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

             
 /*----------------------------------------------------                
 Crated By  : Praveen Kumar               
 Created On : 20 Aug 2010             
 Purpose    : Fetch Policy Info to genrate Premium Documents                
             
  -- drop proc Proc_GetCancellationNoticeDataforPdf  2126,179,2,NULL,'$'   2126,233,1 --           
     
 Proc_GetCancellationNoticeDataforPdf 2156,128,2,NULL,R$   
  Proc_GetCancellationNoticeDataforPdf 2126,453,2,NULL,R$                     
 ------------------------------------------------------*/             
 CREATE  PROCEDURE [dbo].[Proc_GetCancellationNoticeDataforPdf]  ----2156,128,2              
(                                                              
 @CUSTOMER_ID   int,                                                              
 @POLICY_ID      int,                                                              
 @POLICY_VERSION_ID   int,                                                              
 @CALLEDFROM  VARCHAR(20)=null,           
 @POLICYCURRENCYSYMBOL VARCHAR(10)='$',        
 @Lang_id smallint =1                                                      
)                                                              
AS                  
BEGIN              
-- Table 0 Plan ID and Desc          
          
DECLARE @MAX_VERSION_ID INt       
SELECT @MAX_VERSION_ID =MAX(POLICY_VERSION_ID) FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  
  --- Record Set 1    
 SELECT  INST_PLAN.IDEN_PLAN_ID AS INSTALL_PLAN_ID,           
 ISNULL(INST_PLAN.PLAN_DESCRIPTION,' ') --+ ' (' + ISNULL(INST_PLAN.PLAN_CODE,' ') + ')'           
  --+ ' - ' + ISNULL(LOOKUP.LOOKUP_VALUE_DESC,'')           
  AS BILLING_PLAN,          
dbo.fun_GetMessage(265,@Lang_id) + CASE          
WHEN @POLICYCURRENCYSYMBOL IS NULL OR @POLICYCURRENCYSYMBOL='' THEN           
ISNULL(CAST(INST_PLAN.INSTALLMENT_FEES AS NVARCHAR(50)),'0.00')          
ELSE          
@POLICYCURRENCYSYMBOL +' ' + ISNULL(CAST(INST_PLAN.INSTALLMENT_FEES AS NVARCHAR(50)),'0.00')          
END +dbo.fun_GetMessage(266,@Lang_id) as BILLING_FEE  ,          
dbo.fun_GetMessage(267,@Lang_id) + CASE          
WHEN @POLICYCURRENCYSYMBOL IS NULL OR @POLICYCURRENCYSYMBOL='0.00' THEN           
ISNULL(CAST(INST_PLAN.NON_SUFFICIENT_FUND_FEES AS NVARCHAR(50)),'0.00')          
ELSE          
@POLICYCURRENCYSYMBOL +' ' + ISNULL(CAST(INST_PLAN.NON_SUFFICIENT_FUND_FEES AS NVARCHAR(50)),'0.00')           
END + dbo.fun_GetMessage(268,@Lang_id) AS NON_SUFFICIENT_FUND_FEES,          
dbo.fun_GetMessage(267,@Lang_id) + CASE          
WHEN @POLICYCURRENCYSYMBOL IS NULL OR @POLICYCURRENCYSYMBOL='0.00' THEN           
ISNULL(CAST(INST_PLAN.LATE_FEES AS NVARCHAR(50)),'0.00')          
ELSE          
@POLICYCURRENCYSYMBOL +' ' + ISNULL(CAST(INST_PLAN.LATE_FEES AS NVARCHAR(50)),'0.00')           
END + dbo.fun_GetMessage(269,@Lang_id) AS LATE_FEES,          
          
  TOTAL_DUE=CASE          
WHEN @POLICYCURRENCYSYMBOL IS NULL OR @POLICYCURRENCYSYMBOL='' THEN           
 CAST((SUM(ACT_POL_DE.INSTALLMENT_AMOUNT)+INST_PLAN.INSTALLMENT_FEES) AS NVARCHAR(50)) --AS TOTAL_DUE          
ELSE          
@POLICYCURRENCYSYMBOL +' ' + CAST((SUM(ACT_POL_DE.INSTALLMENT_AMOUNT)+INST_PLAN.INSTALLMENT_FEES) AS NVARCHAR(50)) --- AS TOTAL_DUE          
END,          
SUM(INSTALLMENT_AMOUNT)+INST_PLAN.INSTALLMENT_FEES AS TOTAL_DUE_1          
 --(SUM(ACT_POL_DE.INSTALLMENT_AMOUNT)+INST_PLAN.INSTALLMENT_FEES) AS TOTAL_DUE          
          
  --INST_PLAN.INSTALLMENT_FEES AS BILLING_FEE ,          
  --INST_PLAN.NON_SUFFICIENT_FUND_FEES,          
  --ISNULL(CAST(INST_PLAN.LATE_FEES AS NVARCHAR(20)),'') AS LATE_FEES          
            
FROM ACT_INSTALL_PLAN_DETAIL INST_PLAN WITH(NOLOCK)         
--LEFT JOIN MNT_LOOKUP_VALUES LOOKUP                
--ON INST_PLAN.PLAN_PAYMENT_MODE = LOOKUP.LOOKUP_UNIQUE_ID           
LEFT OUTER JOIN ACT_POLICY_INSTALL_PLAN_DATA PLAN_DATA WITH(NOLOCK)ON           
INST_PLAN.IDEN_PLAN_ID=PLAN_DATA.PLAN_ID          
LEFT OUTER JOIN ACT_POLICY_INSTALLMENT_DETAILS as ACT_POL_DE WITH(NOLOCK)ON           ACT_POL_DE.CUSTOMER_ID=PLAN_DATA.CUSTOMER_ID AND ACT_POL_DE.POLICY_ID=PLAN_DATA.POLICY_ID           
AND ACT_POL_DE.POLICY_VERSION_ID=PLAN_DATA.POLICY_VERSION_ID          
           
WHERE PLAN_DATA.CUSTOMER_ID=@CUSTOMER_ID AND PLAN_DATA.POLICY_ID=@POLICY_ID AND       
PLAN_DATA.POLICY_VERSION_ID=  @MAX_VERSION_ID    
--------@POLICY_VERSION_ID          
---(SELECT MAX(POLICY_VERSION_ID) FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID )      
       
GROUP BY INST_PLAN.IDEN_PLAN_ID,INST_PLAN.PLAN_DESCRIPTION,INST_PLAN.INSTALLMENT_FEES,          
INST_PLAN.NON_SUFFICIENT_FUND_FEES,INST_PLAN.LATE_FEES          
          
FOR XML AUTO,ELEMENTS --, Root('ACT_INSTALL_PLAN_DETAIL')                
          
-- Record Set 2 Agency and Policy           
          
SELECT ISNULL(POL_CUST.POLICY_NUMBER,'') AS POLICY_NUMBER,CONVERT(VARCHAR(10), POL_CUST.APP_EFFECTIVE_DATE, 101) AS APP_EFFECTIVE_DATE,          
ISNULL(AGENCY_LIST.AGENCY_ID,'') AS AGENCY_ID,ISNULL(AGENCY_LIST.AGENCY_CODE,'') AS AGENCY_CODE,          
ISNULL(AGENCY_LIST.AGENCYNAME,'') AS AGENCYNAME,          
ISNULL(AGENCY_LIST.AGENCY_PHONE,'') AS AGENCY_PHONE,          
ISNULL(MNT_LOB_MASTER.LOB_DESC,'') AS LOB_DESC,           
LOC_NUM ,Rtrim(ltrim((ISNULL(NAME,'')+'-'+ISNULL(POL_LOCATIONS.LOC_ADD1,'')+'-'+ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.LOC_NUM),'')+'-' +ISNULL(POL_LOCATIONS.LOC_ADD2,'')+'-'+ISNULL(POL_LOCATIONS.DISTRICT,'')+'-'+ ISNULL(POL_LOCATIONS.LOC_CITY,'') +'- 
 
'    
      
        
+ ISNULL(POL_LOCATIONS.LOC_ZIP,''))))  AS LOC_ADD1           
          
 ,ISNULL(CUSTOMER.CUSTOMER_FIRST_NAME,'')+ISNULL(' '+CUSTOMER.CUSTOMER_MIDDLE_NAME,'')+ISNULL(' '+CUSTOMER.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,                
   ISNULL(CUSTOMER.CUSTOMER_ADDRESS1,'')+ISNULL(' '+CUSTOMER.CUSTOMER_ADDRESS2,'') +','+  ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') AS ADDRESS,                
  -- ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') CUSTOMER_STATE_NAME,           
   ISNULL(MNT_COUNTRY_LIST.COUNTRY_NAME,'') CUSTOMER_COUNTRY_NAME,            
   ISNULL(CUSTOMER.CUSTOMER_ZIP,'') AS ZIP_CODE          
          
FROM POL_CUSTOMER_POLICY_LIST POL_CUST WITH(NOLOCK)          
LEFT OUTER JOIN MNT_AGENCY_LIST AGENCY_LIST WITH(NOLOCK) ON                
AGENCY_LIST.AGENCY_ID=POL_CUST.AGENCY_ID          
LEFT OUTER JOIN  MNT_LOB_MASTER WITH(NOLOCK) ON                
POL_CUST.POLICY_LOB = MNT_LOB_MASTER.LOB_ID            
LEFT OUTER JOIN  POL_LOCATIONS WITH(NOLOCK) ON                
POL_CUST.CUSTOMER_ID = POL_LOCATIONS.CUSTOMER_ID  AND          
POL_CUST.POLICY_ID = POL_LOCATIONS.POLICY_ID  AND          
POL_CUST.POLICY_VERSION_ID = POL_LOCATIONS.POLICY_VERSION_ID           
LEFT OUTER JOIN  CLT_CUSTOMER_LIST CUSTOMER WITH(NOLOCK) ON            
POL_CUST.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID            
LEFT OUTER JOIN MNT_COUNTRY_LIST WITH(NOLOCK) ON  CUSTOMER.CUSTOMER_COUNTRY = MNT_COUNTRY_LIST.COUNTRY_ID                                                          
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST WITH(NOLOCK) ON  CUSTOMER.CUSTOMER_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID                
          
          
WHERE POL_CUST.CUSTOMER_ID=@CUSTOMER_ID AND POL_CUST.POLICY_ID=@POLICY_ID AND POL_CUST.POLICY_VERSION_ID=@POLICY_VERSION_ID         
 FOR XML AUTO,ELEMENTS---, Root('MNT_AGENCY_LIST')           
           
-- Record Set 3 Installment Details           
          
SELECT  ISNULL(CAST((ACT_INSTALL_DETAILS.INSTALLMENT_NO)AS NVARCHAR(20)),'') AS INSTALLMENT_NO,
ISNULL(CONVERT(VARCHAR(10), ACT_INSTALL_DETAILS.INSTALLMENT_EFFECTIVE_DATE, 101),'') AS DUE_DATE,          
--TOTAL_DUE=CASE          
--WHEN @POLICYCURRENCYSYMBOL IS NULL OR @POLICYCURRENCYSYMBOL='' THEN           
-- CAST(ACT_INSTALL_DETAILS.TOTAL AS NVARCHAR(50)) --AS TOTAL_DUE          
--ELSE          
--@POLICYCURRENCYSYMBOL +' ' + CAST(ACT_INSTALL_DETAILS.TOTAL AS NVARCHAR(50)) --- AS TOTAL_DUE          
--END,          
          
ISNULL(CONVERT(VARCHAR(10), ACT_INSTALL_DETAILS.CREATED_DATETIME, 101),'')  AS BILLING_DATE,           
          
INSTALLMENT_AMOUNT=CASE          
WHEN @POLICYCURRENCYSYMBOL IS NULL OR @POLICYCURRENCYSYMBOL='' THEN           
 ISNULL(CAST((ACT_INSTALL_DETAILS.INSTALLMENT_AMOUNT) AS NVARCHAR(50)),'') --AS INSTALLMENT_AMOUNT          
ELSE          
@POLICYCURRENCYSYMBOL +' ' + ISNULL(CAST((ACT_INSTALL_DETAILS.INSTALLMENT_AMOUNT) AS NVARCHAR(50)),'') --- AS INSTALLMENT_AMOUNT          
          
END,          
ISNULL(CONVERT(VARCHAR(10), ACT_INSTALL_DETAILS.INSTALLMENT_EFFECTIVE_DATE, 101),'') AS INSTALLMENT_EFFECTIVE_DATE,          
ISNULL(CONVERT(VARCHAR(10), ACT_INSTALL_DETAILS.CREATED_DATETIME, 101),'')  AS BILLING_DATE 
  
FROM POL_CUSTOMER_POLICY_LIST POL_CUST WITH(NOLOCK)          
LEFT OUTER JOIN ACT_POLICY_INSTALLMENT_DETAILS ACT_INSTALL_DETAILS WITH(NOLOCK) ON           
POL_CUST.CUSTOMER_ID=ACT_INSTALL_DETAILS.CUSTOMER_ID AND            
POL_CUST.POLICY_ID=ACT_INSTALL_DETAILS.POLICY_ID AND          
POL_CUST.POLICY_VERSION_ID=ACT_INSTALL_DETAILS.POLICY_VERSION_ID          
          
WHERE POL_CUST.CUSTOMER_ID=@CUSTOMER_ID AND POL_CUST.POLICY_ID=@POLICY_ID AND POL_CUST.POLICY_VERSION_ID= @MAX_VERSION_ID     
--(SELECT MAX(POLICY_VERSION_ID) FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID )        
 FOR XML AUTO,ELEMENTS, Root('INSTALLMENT_DETAILS')             
          
SELECT             
           
 CONVERT(VARCHAR,PRC.EFFECTIVE_DATETIME,101) AS SOURCE_EFF_DATE ,            
 CONVERT(VARCHAR,PRC.EFFECTIVE_DATETIME,101) AS POSTING_DATE ,            
 case when isnumeric(BAL.TRAN_DESC)=1 then dbo.fun_GetMessage(BAL.TRAN_DESC,1) else BAL.TRAN_DESC END            
 + ' (V '  + CAST(BAL.POLICY_VERSION_ID AS VARCHAR) + '.0' + ')'  AS DESCRIPTION  ,            
 --CASE WHEN BAL.AMOUNT  < 0 then BAL.AMOUNT   * -1                                     
 --ELSE BAL.AMOUNT END AS TOTAL_AMOUNT  ,                
 --CASE UPDATED_FROM                                     
 --WHEN 'F' THEN isnull(AMOUNT,0) *-1                                    
 --ELSE isnull(AMOUNT,0)                                     
 --END AS TOTAL_PREMIUM             
 CASE              
 WHEN UPDATED_FROM ='F' THEN          
 CASE           
 WHEN @POLICYCURRENCYSYMBOL IS NULL OR @POLICYCURRENCYSYMBOL='' THEN           
  CAST( (isnull(AMOUNT,0) *-1)  AS NVARCHAR(50))                                  
END           
  ELSE @POLICYCURRENCYSYMBOL +' ' +  CAST(isnull(AMOUNT,0) AS NVARCHAR(50))            
END AS TOTAL_PREMIUM            
               
          
FROM ACT_CUSTOMER_BALANCE_INFORMATION BAL WITH(NOLOCK)            
 INNER JOIN POL_POLICY_PROCESS PRC with(nolock)             
 ON PRC.CUSTOMER_ID=BAL.CUSTOMER_ID            
 AND PRC.POLICY_ID =BAL.POLICY_ID            
 AND PRC.NEW_POLICY_VERSION_ID = BAL.POLICY_VERSION_ID            
 AND PRC.PROCESS_STATUS='COMPLETE'            
 AND ISNULL(PRC.REVERT_BACK,'N')<>'Y'            
 WHERE BAL.CUSTOMER_ID= @CUSTOMER_ID             
 AND BAL.POLICY_ID= @POLICY_ID            
                
  FOR XML AUTO,ELEMENTS, Root('ACT_CST_BAL_INFO')           
            
  -- Record Set 4 OCRA          
 SELECT POL_CUST.POLICY_NUMBER+' X '+ CAST(CAST((SUM(INSTALLMENT_AMOUNT)+INST_PLAN.INSTALLMENT_FEES) AS INT) AS NVARCHAR(15))          
+'0000'+(select cast(cast(((INSTALLMENT_AMOUNT)*100)as int)as nvarchar(10)) from ACT_POLICY_INSTALLMENT_DETAILS where           
CUSTOMER_ID=@CUSTOMER_ID   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@MAX_VERSION_ID      
---(SELECT MAX(POLICY_VERSION_ID) FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID)       
 and INSTALLMENT_NO=1          
) AS OCRA          
 FROM ACT_POLICY_INSTALLMENT_DETAILS AS ACT_INSTAL_PLAN_DETAIL          
LEFT OUTER JOIN ACT_POLICY_INSTALL_PLAN_DATA ACT_POL_DE WITH(NOLOCK)ON           
           
 ACT_POL_DE.CUSTOMER_ID=ACT_INSTAL_PLAN_DETAIL.CUSTOMER_ID AND ACT_POL_DE.POLICY_ID=ACT_INSTAL_PLAN_DETAIL.POLICY_ID           
AND ACT_POL_DE.POLICY_VERSION_ID=ACT_INSTAL_PLAN_DETAIL.POLICY_VERSION_ID          
          
 LEFT OUTER JOIN ACT_INSTALL_PLAN_DETAIL INST_PLAN WITH(NOLOCK)ON           
 INST_PLAN.IDEN_PLAN_ID=ACT_POL_DE.PLAN_ID          
           
INNER JOIN POL_CUSTOMER_POLICY_LIST POL_CUST WITH(NOLOCK)ON           
ACT_INSTAL_PLAN_DETAIL.CUSTOMER_ID = POL_CUST.CUSTOMER_ID  AND          
ACT_INSTAL_PLAN_DETAIL.POLICY_ID = POL_CUST.POLICY_ID  AND          
ACT_INSTAL_PLAN_DETAIL.POLICY_VERSION_ID = POL_CUST.POLICY_VERSION_ID                    
WHERE ACT_INSTAL_PLAN_DETAIL.CUSTOMER_ID=@CUSTOMER_ID AND ACT_INSTAL_PLAN_DETAIL.POLICY_ID=@POLICY_ID           
AND ACT_INSTAL_PLAN_DETAIL.POLICY_VERSION_ID=  @MAX_VERSION_ID    
---(SELECT MAX(POLICY_VERSION_ID) FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID)          
GROUP BY POL_CUST.POLICY_NUMBER,INST_PLAN.INSTALLMENT_FEES      
          
 FOR XML AUTO,ELEMENTS, Root('PRINT_OCRA')          
         
 SELECT REIN_COMAPANY_NAME, REIN_COMAPANY_ADD1,REIN_COMAPANY_STATE +' '+        
REIN_COMAPANY_ZIP AS  STATEZIP ,REIN_COMAPANY_COUNTRY  FROM MNT_REIN_COMAPANY_LIST AS MNTRCL WITH(NOLOCK)                  
INNER JOIN MNT_SYSTEM_PARAMS  AS MNTSYSPARAM WITH(NOLOCK)ON MNTRCL.REIN_COMAPANY_ID=MNTSYSPARAM.SYS_CARRIER_ID         
 FOR XML AUTO,ELEMENTS, Root('REIN_COMPANY')    
   
  
  
 DECLARE @CANCELLATION_OPTION int                                                   
 DECLARE @CANCELLATION_DATE DateTime    
 DECLARE    @DUE_DATE DateTime  
 DECLARE @MINIMUM_DUE DECIMAL(18,2)    
 DECLARE @Equity  Int,  @Flat  Int,   @ProRata Int     
 SET @Equity  = 11996    
 SET @Flat  = 11995    
 SET @ProRata = 11994    
  
 CREATE TABLE #AMOUNT_DUE     
 (    
  MINIMUM_DUE  DECIMAL(18,2),                                                
  TOTAL_DUE  DECIMAL(18,2),                                                
  AGENCY_ID  INT,                
  AGENCYCODE  VARCHAR(20),    
  PREM   Decimal(18,2) ,     
  FEE    Decimal(18,2),                
  FIRST_INS_FEE Decimal(18,2),    
  TOTAL_PREMIUM_DUE Decimal(18,2)     
 )              
 INSERT INTO #AMOUNT_DUE exec Proc_GetTotalAndMinimumDue @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID              
  --  exec Proc_GetTotalAndMinimumDue 2156,128,2,null                 
 SELECT --@TOTAL_DUE=TOTAL_DUE,   
 @MINIMUM_DUE=MINIMUM_DUE  
 --, @TOTAL_FEE=FEE, @FIRST_INS_FEE=FIRST_INS_FEE ,    
    --@TOTAL_PREMIUM_DUE  = TOTAL_PREMIUM_DUE    
 FROM #AMOUNT_DUE WITH(NOLOCK)                
 DROP TABLE #AMOUNT_DUE     
   
 CREATE TABLE #CANCELLATION_MESSAGE  
 (  
  MESSAGE_1 NTEXT,  
  WARNING_MESSAGE NTEXT,  
  )  
    
 SELECT @CANCELLATION_OPTION = CANCELLATION_OPTION, @CANCELLATION_DATE = PPP.EFFECTIVE_DATETIME, @DUE_DATE = PPP.DUE_DATE      
 FROM POL_POLICY_PROCESS PPP (nolock)     
 WHERE PPP.CUSTOMER_ID= @CUSTOMER_ID AND PPP.POLICY_ID= @POLICY_ID AND PPP.NEW_POLICY_VERSION_ID= @POLICY_VERSION_ID                 
  AND PPP.PROCESS_STATUS = 'PENDING'     
  -- print @CANCELLATION_OPTION ;   
 INSERT INTO #CANCELLATION_MESSAGE                               
 SELECT CASE WHEN @CANCELLATION_OPTION = @Equity THEN     
 --'According to our records, the scheduled payment on your policy is past due. However, payments received to date will provide coverage through '     
 dbo.fun_GetMessage(271,@Lang_id)  
   
 --+CONVERT(VARCHAR(11),ISNULL(@CANCELLATION_DATE,''),101)     
 + CONVERT(VARCHAR(11),DateAdd(day, -1, ISNULL(@CANCELLATION_DATE,'')),101)     
 -- '. If payment of $' + CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(@MINIMUM_DUE,0))),1) + ' is not received by '   
   +dbo.fun_GetMessage(272,@Lang_id) + CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(@MINIMUM_DUE,0))),1) + dbo.fun_GetMessage(273,@Lang_id)   
 + CONVERT(VARCHAR(11),ISNULL(@DUE_DATE,''),101) +                                           
 --', your policy is cancelled for non-payment of premium effective 12:01 A.M. Standard Time on '     
 + dbo.fun_GetMessage(274,@Lang_id)  
 --+ CONVERT(VARCHAR(11),DateAdd(day, 1, ISNULL(@CANCELLATION_DATE,'')),101)     
 + CONVERT(VARCHAR(11),@CANCELLATION_DATE,101)     
 --+ '. Please disregard this notice if payment has been made. '                                                     
 + dbo.fun_GetMessage(275,@Lang_id)  
             
 WHEN @CANCELLATION_OPTION = @ProRata THEN     
 --'According to our records, the scheduled payment on your policy is past due. If payment of $'   
 dbo.fun_GetMessage(276,@Lang_id)    
 -- + CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(@MINIMUM_DUE,0))),1) + ' is not received by ' + CONVERT(VARCHAR(11),ISNULL(@DUE_DATE,'') ,101)    
  + CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(@MINIMUM_DUE,0))),1) + dbo.fun_GetMessage(273,@Lang_id) + CONVERT(VARCHAR(11),ISNULL(@DUE_DATE,'') ,101)    
    
 -- + ', your policy is cancelled for non-payment of premium effective 12:01 A.M. Standard Time on '     
  + dbo.fun_GetMessage(274,@Lang_id)  
 + CONVERT(VARCHAR(11),ISNULL(@CANCELLATION_DATE,''),101) +          
 --'. Please disregard this notice if payment has been made. '    
  dbo.fun_GetMessage(275,@Lang_id)    
  WHEN @CANCELLATION_OPTION = 11995 THEN  + dbo.fun_GetMessage(276,@Lang_id)  + CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(@MINIMUM_DUE,0))),1)  +  dbo.fun_GetMessage(273,@Lang_id) +        
 CONVERT(VARCHAR(11),    
 ISNULL(@DUE_DATE,''), 101)    
 --+ ', your policy is cancelled for non-payment of premium effective 12:01 A.M. Standard Time on '     
 + dbo.fun_GetMessage(274,@Lang_id)  
 + CONVERT(VARCHAR(11),ISNULL(@CANCELLATION_DATE,''),101) +                                           
 --'. Please disregard this notice if payment has been made. '                         
   dbo.fun_GetMessage(275,@Lang_id)  
 ELSE ''                                                    
 END AS MESSAGE ,   
 --(SELECT 'WARNING: According to Michigan law, you must not operate or permit the operation of any motor vehicle to which this notice applies, or operate any other vehicle, unless the vehicle is insured as required by the law.'   
 --)  
 (SELECT + dbo.fun_GetMessage(277,@Lang_id)  
 )  
 AS WARNING_MESSAGE   
                                             
   SELECT * FROM #CANCELLATION_MESSAGE as MESSAGE  
 FOR  XML AUTO, ELEMENTS,ROOT('CANCELLATION_MESSAGE')  
 DROP TABLE #CANCELLATION_MESSAGE                                                  
           
     
           
END 
GO

