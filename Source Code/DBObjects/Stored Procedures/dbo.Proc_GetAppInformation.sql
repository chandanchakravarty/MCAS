IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_GetAppInformation 1692,163,1            
CREATE PROC [dbo].[Proc_GetAppInformation]                                  
(                                  
 @CUSTOMERID  int,                                  
 @APPID  int,                                  
 @APPVERSIONID int                                  
                                  
)                                  
AS                                  
BEGIN                                  
                                  
                                  
SELECT 
 case when LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME, ''))) ='' then '' 
 else LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME, ''))) + ' ' end          
 + LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME, ''))) AS CUSTOMERNAME,                                
  APP_LIST.CUSTOMER_ID,                                 
 APP_LIST.APP_ID,                                   
 APP_LIST.APP_VERSION_ID,                                
  APP_LIST.PARENT_APP_VERSION_ID,                                 
 APP_LIST.APP_STATUS,                                 
 APP_LIST.APP_NUMBER,                                   
 APP_LIST.APP_VERSION,                                 
  APP_LIST.APP_TERMS,                                 
  CONVERT(varchar(10),                                 
  APP_LIST.APP_INCEPTION_DATE, 101)                                   
 AS APP_INCEPTION_DATE,                                 
  CONVERT(varchar(10), APP_LIST.APP_EFFECTIVE_DATE, 101) AS                                 
APP_EFFECTIVE_DATE, CONVERT(varchar(10),                                   
APP_LIST.APP_EXPIRATION_DATE, 101) AS APP_EXPIRATION_DATE, APP_LIST.APP_LOB AS LOB_ID,                                   
APP_LIST.APP_SUBLOB AS SUB_LOB_ID,                                 
   APP_LIST.CSR,ISNULL(APP_LIST.PRODUCER,0) AS PRODUCER,                             
   ( MNT_USER_LIST.user_fname + ' ' +  MNT_USER_LIST.user_lname) as UNDERWRITER,                                                                 
    APP_LIST.IS_UNDER_REVIEW,                                   
APP_LIST.APP_SUBLOB,                                
    APP_LIST.APP_LOB,                                 
   APP_LIST.APP_AGENCY_ID, MNT_LOB_MASTER.LOB_DESC AS LOB,                                   
APP_LIST.STATE_ID,                                 
   MNT_USER_LIST.USER_FNAME + ' ' +                                 
   MNT_USER_LIST.USER_LNAME AS underwritername,                                
  DIV_ID,                                
  DEPT_ID,                                
   PC_ID,                                
   isnull(BILL_TYPE_ID,0) AS BILL_TYPE,             
   isnull(BILL_TYPE_ID,0) AS BILL_TYPE_ID,                                
   COMPLETE_APP,PROPRTY_INSP_CREDIT,                                
   INSTALL_PLAN_ID,                                
  CHARGE_OFF_PRMIUM,                                  
  RECEIVED_PRMIUM,                                
  PROXY_SIGN_OBTAINED,                                
  CLT_CUSTOMER_LIST.IS_ACTIVE IS_CUSTOMER_ACTIVE,                        
  APP_LIST.IS_ACTIVE AS IS_ACTIVE ,                                       
  ISNULL(APP_LIST.POLICY_TYPE,-1) POLICY_TYPE,                                
  ISNULL(APP_LIST.SHOW_QUOTE,'0') SHOW_QUOTE,                                
  YEARS_AT_PREV_ADD,                                
  case YEAR_AT_CURR_RESI when 0.0 then ' ' else Convert(varchar(10),YEAR_AT_CURR_RESI)  end YEAR_AT_CURR_RESI,                          
  APP_LIST.PIC_OF_LOC,           
    
--Commented by Charles on 18-May-10 for Policy Implementation        
--CASE WHEN ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_ID,0) = 0 THEN           
--  RTRIM(LTRIM(ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_NAME,'') + CASE  WHEN ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_NAME,'') = '' THEN '' ELSE  ', ' END +          
--  ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_ADD1,'') + CASE WHEN ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_ADD1,'') = '' THEN '' ELSE ', ' END  +  --Changed from APP_HOME_OWNER_ADD_INT.HOLDER_ADD2 inside CASE, by Charles on 15-Sep-09 for Itrack 6404            
--  ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_ADD2,'') + CASE WHEN  ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_ADD2,'') = '' THEN '' ELSE ', ' END +           
--  ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_CITY,'') + CASE WHEN ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_CITY,'') = '' THEN '' ELSE ', ' END +               
--  ISNULL(UPPER(SL.STATE_CODE),'') + ' ' +           
-- (ISNULL(APP_HOME_OWNER_ADD_INT.HOLDER_ZIP,''))))           
-- ELSE           
--  RTRIM(LTRIM(ISNULL(L.HOLDER_NAME,'') + CASE  WHEN ISNULL(L.HOLDER_NAME,'') = '' THEN '' ELSE  ', ' END +          
--  ISNULL(L.HOLDER_ADD1,'') + CASE WHEN ISNULL(L.HOLDER_ADD1,'') = '' THEN '' ELSE ', ' END  +  --Changed from L.HOLDER_ADD2 inside CASE, by Charles on 15-Sep-09 for Itrack 6404                     
--  ISNULL(L.HOLDER_ADD2,'') + CASE WHEN  ISNULL(L.HOLDER_ADD2,'') = '' THEN '' ELSE ', ' END +           
--  ISNULL(L.HOLDER_CITY,'') + CASE WHEN ISNULL(L.HOLDER_CITY,'') = '' THEN '' ELSE ', ' END +               
--  ISNULL(UPPER(SL.STATE_CODE),'') + ' ' +           
-- (ISNULL(L.HOLDER_ZIP,''))))           
          
-- END 
'' AS BILL_MORTAGAGEE,                      
 DOWN_PAY_MODE                                               
FROM APP_LIST WITH(NOLOCK)                 
INNER JOIN   MNT_LOB_MASTER WITH(NOLOCK) ON APP_LIST.APP_LOB = MNT_LOB_MASTER.LOB_ID                                 
LEFT OUTER JOIN  MNT_USER_LIST WITH(NOLOCK) ON APP_LIST.UNDERWRITER = MNT_USER_LIST.USER_ID                                             
INNER JOIN CLT_CUSTOMER_LIST WITH(NOLOCK) ON  APP_LIST.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID   
--Commented by Charles on 18-May-10 for Policy Implementation                         
--LEFT OUTER JOIN APP_HOME_OWNER_ADD_INT                 
-- ON                 
-- APP_LIST.CUSTOMER_ID = APP_HOME_OWNER_ADD_INT.CUSTOMER_ID AND                
-- APP_LIST.APP_ID = APP_HOME_OWNER_ADD_INT.APP_ID AND                
-- APP_LIST.APP_VERSION_ID = APP_HOME_OWNER_ADD_INT.APP_VERSION_ID AND                
-- APP_LIST.DWELLING_ID = APP_HOME_OWNER_ADD_INT.DWELLING_ID AND                
-- APP_LIST.ADD_INT_ID = APP_HOME_OWNER_ADD_INT.ADD_INT_ID           
          
--LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST L with(nolock)ON APP_HOME_OWNER_ADD_INT.HOLDER_ID = L.HOLDER_ID            
--LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON SL.STATE_ID = APP_HOME_OWNER_ADD_INT.HOLDER_STATE            
                              
WHERE     (APP_LIST.CUSTOMER_ID = @CUSTOMERID)   and                                 
 (APP_LIST.APP_ID=@APPID) AND                                 
 (APP_LIST.APP_VERSION_ID=@APPVERSIONID) 
 --AND ISNULL(APP_HOME_OWNER_ADD_INT.IS_ACTIVE,'Y')='Y'     --Commented by Charles on 18-May-10 for Policy Implementation            
END                                
            
            
GO

