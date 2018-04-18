IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AccountEnquiry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AccountEnquiry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--begin tran     
--drop proc dbo.Proc_AccountEnquiry                       
--go     
/*----------------------------------------------------------                                        
 Proc Name       : dbo.Proc_AccountEnquiry                        
 Created by      : Ravindra                         
 Date            : 2-10-2007                        
 Purpose         : Fetch Account Enquiry Data for customer                        
 Revison History :                                        
 Used In       : Wolverine                                
exec Proc_AccountEnquiry '','','R7000141','11-04-2007','1355'                        
exec Proc_AccountEnquiry '' , '' , 'H1003788' , '10-25-2006',null                        
exec Proc_AccountEnquiry @CUSTOMER_ID = 934, @POLICY_ID = 77, @POLICY_NUMBER = 'WR100009', @PAST_TRANS_DATE = '2007-09-08'                        
------------------------------------------------------------                                        
Date     Review By          Comments                
modified by : Pravesh K chandel              
modified date : 4 oct 2007              
purpose  : to show complete status of Policy              
------   ------------       -------------------------*/                                        
          
-- drop proc dbo.Proc_AccountEnquiry  23,4,1                     
CREATE PROC [dbo].[Proc_AccountEnquiry]                        
(                                        
	 @CUSTOMER_ID INT=null,                              
	 @POLICY_ID INT=null,                              
	 @POLICY_NUMBER VARCHAR(25)=NULL,                              
	 @PAST_TRANS_DATE AS DATETIME,                         
	 @Agency_Code VARCHAR(20)=NULL                             
)                                        
AS                                       
BEGIN                        



IF(ISNULL(@POLICY_NUMBER,'')<>'')                              
BEGIN                              
IF EXISTS(SELECT POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST  (NOLOCK)   WHERE POLICY_NUMBER =@POLICY_NUMBER)                        
BEGIN                        
	SELECT  @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID                          
	FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)                        
	WHERE POLICY_NUMBER=@POLICY_NUMBER                              
END                        
ELSE                        
	RETURN -1                        
END                              

        
DECLARE @CURRENT_TERM    Int,                        
		@POLICY_EFFECTIVE_DATE Datetime,                        
		@POLICY_EXPIRATION_DATE Datetime,                        
		@SUB_LOB_ID   Smallint,                        
		@CURRENT_VERSION_ID  INT      ,                        
		@POLICY_LOB   NVARCHAR(5) ,                        
		@BILL_TYPE_ID   INT ,                        
		@BILL_TYPE  char(2)  ,                      
        @SYSTEM_CODE nvarchar(12)
   -- fetching carrrier system code   
      SELECT @SYSTEM_CODE=REIN_COMAPANY_CODE FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK) 
      WHERE REIN_COMAPANY_ID =
      (SELECT SYS_CARRIER_ID FROM MNT_SYSTEM_PARAMS)      
                        
SET @CURRENT_VERSION_ID=0                              
                        
--Get Current Policy Version                              
--Commented on 21 august              
/*SELECT @CURRENT_VERSION_ID =ISNULL(MAX(NEW_POLICY_VERSION_ID),0) FROM POL_POLICY_PROCESS (NOLOCK)                             
WHERE CUSTOMER_ID=@CUSTOMER_ID                         
 AND POLICY_ID=@POLICY_ID                */              
              
--              
SELECT @CURRENT_VERSION_ID =ISNULL(MAX(POLICY_VERSION_ID),0)           
FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)                             
WHERE CUSTOMER_ID=@CUSTOMER_ID                         
AND POLICY_ID=@POLICY_ID                 
              
              
/*                  
COMMENTED BY : Pawan as discussed with Rajan for displaying latest version                  
*/                  
-- AND PROCESS_ID IN(18,25)                              
                   
       
IF(@CURRENT_VERSION_ID=0)                              
 SET @CURRENT_VERSION_ID=1                              
                              
                         
-- Fetch Policy Level Details                        
SELECT  @CURRENT_TERM = CURRENT_TERM ,            
  @POLICY_EFFECTIVE_DATE  = APP_EFFECTIVE_DATE,                        
  @POLICY_EXPIRATION_DATE = APP_EXPIRATION_DATE,                    
  @SUB_LOB_ID = ISNULL(POLICY_SUBLOB ,0),                        
  @BILL_TYPE  = BILL_TYPE,                        
  @BILL_TYPE_ID=BILL_TYPE_ID,                         
  @POLICY_LOB=POLICY_LOB                         
FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)                        
WHERE   CUSTOMER_ID = @CUSTOMER_ID                         
 AND POLICY_ID = @POLICY_ID                   
 AND POLICY_VERSION_ID = @CURRENT_VERSION_ID                         
                        
--CHECK WHETHER THE POLICY WAS UNDER DB              
DECLARE @RENEWED VARCHAR(20)              
IF EXISTS(SELECT BILL_TYPE FROM POL_CUSTOMER_POLICY_LIST (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND BILL_TYPE='DB')              
 set @RENEWED  = 'YES'              
ELSE              
 set @RENEWED  = 'NO'              
          
                        
                        
-- RecordSet 1 Policy Level Data                        
SELECT                
POLICY_NUMBER AS POLICY_NO,                              
CONVERT(VARCHAR(20),POL.APP_EFFECTIVE_DATE,101) + '-' + CONVERT(VARCHAR(20),POL.APP_EXPIRATION_DATE,101)  AS APP_TERM,                               
--Ravindra(10-27-2008):     
--isnull(CONVERT(VARCHAR(20),CLT.CUSTOMER_FIRST_NAME),'') + ' ' + isnull(CONVERT(VARCHAR(20),CLT.CUSTOMER_MIDDLE_NAME),'') + ' '    + isnull(CONVERT(VARCHAR(20),CLT.CUSTOMER_LAST_NAME),'') AS CUSTOMER_NAME,                             
RTRIM(ISNULL(CLT.CUSTOMER_FIRST_NAME,'')) + ' ' +                                             
CASE WHEN CLT.CUSTOMER_MIDDLE_NAME IS NULL THEN '' ELSE RTRIM(CLT.CUSTOMER_MIDDLE_NAME) + ' ' END +                           
ISNULL(CLT.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME ,    
    
LOB.LOB_DESC AS LOB_DESC,                        
--POL.AGENCY_ID,                        
AGEN.AGENCY_CODE AS AGENCY_ID,                        
AGEN.AGENCY_DISPLAY_NAME AS AGEN_NAME,                        
ACT.PLAN_DESCRIPTION AS PMT_PLAN,                        
--PSM.POLICY_DESCRIPTION AS STATUS, --commented by Pravesh              
dbo.fun_GetPolicyDisplayStatus(POL.CUSTOMER_ID,POL.POLICY_ID,POL.POLICY_VERSION_ID) AS STATUS,              
MNT.LOOKUP_VALUE_CODE AS  POLICY_TYPE ,                        
@BILL_TYPE AS BILL_TYPE ,              
@RENEWED AS RENEWED   ,
AGEN.AGENCY_PHONE                       
FROM POL_CUSTOMER_POLICY_LIST POL (NOLOCK)                            
INNER JOIN POL_POLICY_STATUS_MASTER PSM (NOLOCK)                          
 ON PSM.POLICY_STATUS_CODE = POL.POLICY_STATUS                        
INNER JOIN CLT_CUSTOMER_LIST CLT  (NOLOCK)                              
 ON CLT.CUSTOMER_ID=POL.CUSTOMER_ID                               
INNER JOIN MNT_LOB_MASTER LOB  (NOLOCK)                              
 ON POL.POLICY_LOB=LOB.LOB_ID                              
INNER JOIN MNT_AGENCY_LIST AGEN  (NOLOCK)                              
 ON POL.AGENCY_ID=AGEN.AGENCY_ID                              
LEFT OUTER JOIN ACT_INSTALL_PLAN_DETAIL  ACT  (NOLOCK)                         
 ON POL.INSTALL_PLAN_ID = ACT.IDEN_PLAN_ID                         
LEFT JOIN MNT_LOOKUP_VALUES MNT (NOLOCK) ON MNT.LOOKUP_UNIQUE_ID = POL.POLICY_TYPE                 
WHERE POL.CUSTOMER_ID=@CUSTOMER_ID                         
 AND POL.POLICY_ID=@POLICY_ID                         
 AND POL.POLICY_VERSION_ID=@CURRENT_VERSION_ID                    
/*                  
Added BY : Pawan , to display concerned policies to agencies, all to wolverine                  
*/                  
AND UPPER(AGEN.Agency_Code) = CASE UPPER(@Agency_Code) WHEN @SYSTEM_CODE THEN UPPER(AGEN.Agency_Code) ELSE  UPPER(@Agency_Code)  END                  
                  
                          
          
CREATE TABLE #ARREPORT                  
(                
	[IDENT_COL] [int] ,                  
	[SOURCE_ROW_ID] [int],            
	[SOURCE]    Varchar(50),            
	[PRINTED_ON_NOTICE] Char(1),            
	[SOURCE_TRAN_DATE] DateTime,             
	[SOURCE_EFF_DATE] DateTime,                                            
	[POSTING_DATE] DateTime,                                                   
	[DESCRIPTION] VarChar(100) null,         
	[VERSION_NO] Varchar(10) null,                                      
	[TOTAL_AMOUNT] [decimal] (18,2)  ,                                                     
	[TEMP_PREMIUM] [decimal](18,2) NULL ,                                                                                
	[INSF_FEE] [decimal] (18,2) NULL,                  
	[LATE_FEE] [decimal](18,2) NULL ,                                 
	[REINS_FEE] [decimal](18,2) NULL ,                   
	[NSF_FEE] [decimal](18,2) NULL,                  
	[ADJUSTED] [decimal](18,2),            
	[TYPE] VARCHAR(2),                  
	[NOTICE_URL] Varchar(400), 
	--Ravindra(12-08-2008) : For RTL View Integration
	[RTL_BATCH_NUMBER]	Varchar(8), 
	[RTL_GROUP_NUMBER]	Varchar(8), 

	[TOTAL_FEE]  Decimal(18,2),            
	[TOTAL_PREMIUM] Decimal(18,2) ,    
	TOTAL_PREMIUM_DUE Decimal(18,2)           
)                  
          
--Added against itrack #6802  
DECLARE @MAX_VERSION_ID     INT  
SET @MAX_VERSION_ID=0        

SELECT @MAX_VERSION_ID=ISNULL(MAX(NEW_POLICY_VERSION_ID),0) FROM POL_POLICY_PROCESS P with(nolock)
INNER JOIN POL_CUSTOMER_POLICY_LIST A
	ON P.CUSTOMER_ID = A.CUSTOMER_ID
	AND P.POLICY_ID = A.POLICY_ID
	AND P.NEW_POLICY_VERSION_ID = A.POLICY_VERSION_ID    
where a.BILL_TYPE != 'AB' and a.POLICY_VERSION_ID in 
(
	SELECT TOP 1 POL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST POL LEFT JOIN POL_POLICY_PROCESS PPP
	ON PPP.CUSTOMER_ID = POL.CUSTOMER_ID AND PPP.POLICY_ID = POL.POLICY_ID  AND PPP.NEW_POLICY_VERSION_ID =
	POL.POLICY_VERSION_ID
	where        
	POL.CUSTOMER_ID = A.CUSTOMER_ID AND POL.POLICY_ID = A.POLICY_ID 
	AND POL.POLICY_NUMBER=@POLICY_NUMBER
	AND
	(
		(ISNULL(PPP.PROCESS_STATUS,'') = 'COMPLETE' 	AND ISNULL(PPP.REVERT_BACK,'N') = 'N')
		OR 
		POL.POLICY_STATUS IN ( 'SUSPENDED','UISSUE','URENEW')              
	)
	ORDER BY POL.POLICY_VERSION_ID DESC            
)

IF(@MAX_VERSION_ID=0)        
	SET @MAX_VERSION_ID=1        
  
-- Fetch Policy Level Details  
SELECT  @BILL_TYPE  = BILL_TYPE  
FROM POL_CUSTOMER_POLICY_LIST with(nolock)  
WHERE CUSTOMER_ID = @CUSTOMER_ID   
AND POLICY_ID = @POLICY_ID   
AND POLICY_VERSION_ID = @MAX_VERSION_ID            

IF(@BILL_TYPE = 'DB')  
BEGIN   
INSERT INTO #ARREPORT           
EXEC Proc_GetTransactionHistory @CUSTOMER_ID , @POLICY_ID , @POLICY_NUMBER      
END

---Calculate Toatl Due on Policy IrRespective of Transaction Date
DECLARE @TOTAL_DUE_ON_POLICY DECIMAL(18,2)
SELECT @TOTAL_DUE_ON_POLICY = SUM(CASE TYPE  WHEN 'F' THEN isnull(TOTAL_PREMIUM,0) *-1     ELSE isnull(TOTAL_PREMIUM,0) END )
FROM #ARREPORT 

                        
-- Recordset 2 Account History                         
SELECT SOURCE_TRAN_DATE as SRC_DATE,                        
CONVERT(VARCHAR,SOURCE_TRAN_DATE) AS SOURCE_TRAN_DATE,                        
CONVERT(VARCHAR(20),SOURCE_EFF_DATE,101) AS SOURCE_EFF_DATE,                        
CONVERT(VARCHAR(20),POSTING_DATE,101) AS POSTING_DATE,                        
DESCRIPTION + ISNULL(VERSION_NO,'') AS DESCRIPTION,                                            
CASE WHEN TOTAL_AMOUNT  < 0 then TOTAL_AMOUNT   * -1                         
ELSE TOTAL_AMOUNT END AS TOTAL_AMOUNT  ,                         
CASE TYPE                         
-- WHEN 'D' THEN TOTAL_PREMIUM *-1  
WHEN 'F' THEN isnull(TOTAL_PREMIUM,0) *-1                        
ELSE isnull(TOTAL_PREMIUM,0)                         
END AS TOTAL_PREMIUM ,                        
INSF_FEE,LATE_FEE  ,REINS_FEE  ,                               
NSF_FEE  ,TYPE   ,TOTAL_FEE     ,TEMP_PREMIUM,                         
ISNULL(NOTICE_URL, 'NA') AS NOTICE_URL,              
ADJUSTED  , SOURCE_ROW_ID AS OPEN_ITEM_ID ,              
@POLICY_ID AS POL_ID , 
RTL_BATCH_NUMBER , RTL_GROUP_NUMBER ,
@TOTAL_DUE_ON_POLICY as TOTAL_DUE_ON_POLICY
FROM #ARREPORT       (NOLOCK)                       
WHERE SOURCE_TRAN_DATE >= @PAST_TRANS_DATE                         
ORDER BY SRC_DATE                         
                      
                        
DROP TABLE #ARREPORT                          
                        
-- Recordset 3                        
--select address of the policy holder                              
SELECT CUSTOMER_ADDRESS1, CUSTOMER_ADDRESS2, CUSTOMER_CITY, MNT.STATE_CODE AS CUSTOMER_STATE, CUSTOMER_ZIP                              
FROM CLT_CUSTOMER_LIST CLT  (NOLOCK)                             
INNER JOIN MNT_COUNTRY_STATE_LIST MNT                              
 ON MNT.STATE_ID=CLT.CUSTOMER_STATE                              
WHERE CUSTOMER_ID=@CUSTOMER_ID                              
                         
                        
-- Recordset 4                        
--select property address based on lob                              
IF(@POLICY_LOB='1' OR @POLICY_LOB='6')                              
BEGIN                         
 SELECT 'Property Address:' AS LOC_TYPE, LOC_ADD1, LOC_ADD2, LOC_CITY, MNT.STATE_CODE AS LOC_STATE,                           
 LOC_ZIP, @POLICY_LOB AS LOB_ID                         
 FROM POL_LOCATIONS POL (NOLOCK)                      
 INNER JOIN MNT_COUNTRY_STATE_LIST MNT                              
  ON MNT.STATE_ID=POL.LOC_STATE                            
 WHERE CUSTOMER_ID=@CUSTOMER_ID                              
 AND POLICY_ID = @POLICY_ID                         
 AND POLICY_VERSION_ID = @CURRENT_VERSION_ID                        
END                        
ELSE IF(@POLICY_LOB='4')                                 
BEGIN                              
          
 SELECT 'Location of WaterCraft:' AS LOC_TYPE, LOCATION_ADDRESS AS LOC_ADD1, '' AS LOC_ADD2,                         
 LOCATION_CITY AS LOC_CITY, MNT.STATE_CODE AS LOC_STATE,                               
 LOCATION_ZIP AS LOC_ZIP, @POLICY_LOB AS LOB_ID FROM POL_WATERCRAFT_INFO POL (NOLOCK)                             
 INNER JOIN MNT_COUNTRY_STATE_LIST MNT (NOLOCK)                               
 ON MNT.STATE_ID=POL.LOCATION_STATE            
 WHERE CUSTOMER_ID=@CUSTOMER_ID             
 AND POLICY_ID = @POLICY_ID                         
 AND POLICY_VERSION_ID = @CURRENT_VERSION_ID                        
END                        
ELSE                        
BEGIN                            
--  To be corrected                           
 SELECT 'Property Address:' AS LOC_TYPE, '' AS LOC_ADD1, '' AS LOC_ADD2, '' AS LOC_CITY, '' AS LOC_STATE,                        
 '' AS LOC_ZIP, @POLICY_LOB AS LOB_ID FROM POL_WATERCRAFT_INFO (NOLOCK)                             
END                              
                              
--select coverage description and limit based on lob                              
DECLARE @LIMITA As INT                              
DECLARE @COV_DESA AS VARCHAR(50)                              
DECLARE @LIMITC As INT                              
DECLARE @COV_DESC AS VARCHAR(50)                              
                        
DECLARE @DECUCTIBLE As INT ,                             
 @DEDUC_DESC AS VARCHAR(50),                        
 @LOB_DEDUC_CODE AS VARCHAR(10)                             
                              
SET @COV_DESA = ''                              
SET @COV_DESC = ''                
                        
-- Recordset 5                        
IF(@POLICY_LOB='1' OR @POLICY_LOB='6')                              
BEGIN                              
                         
-- Setting this explicitly as the COV Codes are different for Home and Rental                          
 IF(@POLICY_LOB='1')                        
  BEGIN           
  SET @LOB_DEDUC_CODE = 'APD'                        
  END                        
 ELSE                        
  BEGIN                        
  SET @LOB_DEDUC_CODE = 'APDI'                        
  END                        
                        
 SELECT  @LIMITA = ISNULL(POL.LIMIT_1,0),                         
  @COV_DESA = MNT.COV_DES                              
 FROM POL_DWELLING_SECTION_COVERAGES POL (NOLOCK)                             
 INNER JOIN MNT_COVERAGE MNT                              
 ON POL.COVERAGE_CODE_ID = MNT.COV_ID                              
 WHERE POL.CUSTOMER_ID=@CUSTOMER_ID                         
 AND MNT.COV_CODE = 'DWELL'                         
 AND POLICY_ID=@POLICY_ID                         
 AND POLICY_VERSION_ID=@CURRENT_VERSION_ID                              
                         
 SELECT @LIMITC=ISNULL(POL.LIMIT_1,0)                         
 ,@COV_DESC=MNT.COV_DES                              
 FROM POL_DWELLING_SECTION_COVERAGES POL  (NOLOCK)                            
 INNER JOIN MNT_COVERAGE MNT                              
 ON POL.COVERAGE_CODE_ID = MNT.COV_ID                              
 WHERE POL.CUSTOMER_ID=@CUSTOMER_ID                    
 AND MNT.COV_CODE = 'EBUSPP'                         
 AND POLICY_ID=@POLICY_ID                         
 AND POLICY_VERSION_ID=@CURRENT_VERSION_ID                              
                        
SELECT @DECUCTIBLE=ISNULL(POL.DEDUCTIBLE,0),                         
 @DEDUC_DESC=MNT.COV_DES        
 FROM POL_DWELLING_SECTION_COVERAGES POL (NOLOCK)                             
 INNER JOIN MNT_COVERAGE MNT                              
 ON POL.COVERAGE_CODE_ID = MNT.COV_ID                          
 WHERE POL.CUSTOMER_ID=@CUSTOMER_ID                         
 AND MNT.COV_CODE = @LOB_DEDUC_CODE                         
 AND POLICY_ID=@POLICY_ID                         
 AND POLICY_VERSION_ID=@CURRENT_VERSION_ID                              
                         
 SELECT @LIMITA AS LIMITA, @COV_DESA AS COV_DESA, @LIMITC AS LIMITC, @COV_DESC AS COV_DESC,        
  @DECUCTIBLE AS DEDUCTIBLE ,@DEDUC_DESC AS  DEDUC_DESC                         
END                              
ELSE                              
BEGIN                              
 SELECT '' AS LIMITA, '' AS COV_DESA, '' AS LIMITE, '' AS COV_DESE     ,                         
 ''  AS DEDUCTIBLE ,'' AS  DEDUC_DESC                         
END                                 
         
-- Recordset 6                        
--select bill type                   
SELECT LOOKUP_VALUE_DESC                         
FROM MNT_LOOKUP_VALUES (NOLOCK)                   
WHERE LOOKUP_UNIQUE_ID=@BILL_TYPE_ID                              
                        
                        
-- Recordset 7                              
--select Lien Holder                          
IF(@POLICY_LOB='1' OR @POLICY_LOB='6')                          
BEGIN                          
 --Itrack No.2185  Changed by Manoj Rathore 2 Aug 2007                        
SELECT 'Lien Holder:' AS HOLDER_TYPE,                        
 CASE ISNULL(POL.HOLDER_NAME,'') WHEN '' THEN MHL.HOLDER_NAME ELSE POL.HOLDER_NAME END HOLDER_NAME,              
 CASE ISNULL(POL.HOLDER_ADD1,'') WHEN '' THEN MHL.HOLDER_ADD1 ELSE POL.HOLDER_ADD1 END HOLDER_ADD1,              
 CASE ISNULL(POL.HOLDER_ADD2,'') WHEN '' THEN MHL.HOLDER_ADD2 ELSE POL.HOLDER_ADD2 END HOLDER_ADD2,              
 CASE ISNULL(POL.HOLDER_CITY,'') WHEN '' THEN MHL.HOLDER_CITY ELSE POL.HOLDER_CITY END HOLDER_CITY,                           
 CASE WHEN POL.HOLDER_STATE IS NOT NULL  THEN MNT.STATE_CODE ELSE MNT1.STATE_CODE END AS HOLDER_STATE,                             
 CASE ISNULL(POL.HOLDER_ZIP,'') WHEN '' THEN MHL.HOLDER_ZIP  ELSE POL.HOLDER_ZIP END HOLDER_ZIP,     
 'Loan/Reference No: ' + ISNULL(POL.LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER    
 FROM POL_HOME_OWNER_ADD_INT POL (NOLOCK)                             
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST MHL ON POL.HOLDER_ID=MHL.HOLDER_ID AND ISNULL(MHL.IS_ACTIVE,'N') = 'Y'                        
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT ON MNT.STATE_ID=POL.HOLDER_STATE                           
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT1 ON MNT1.STATE_ID=MHL.HOLDER_STATE                
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@CURRENT_VERSION_ID          
   AND ISNULL(POL.IS_ACTIVE,'N') = 'Y' AND (ISNULL(POL.HOLDER_NAME,'') <> '' OR ISNULL(MHL.HOLDER_NAME,'')<>'')                         
 UNION                        
 SELECT 'Lien Holder:' AS HOLDER_TYPE,                        
 CASE ISNULL(POL.HOLDER_NAME,'') WHEN '' THEN MHL.HOLDER_NAME ELSE POL.HOLDER_NAME END HOLDER_NAME,                          
 CASE ISNULL(POL.HOLDER_ADD1,'') WHEN '' THEN MHL.HOLDER_ADD1 ELSE POL.HOLDER_ADD1 END HOLDER_ADD1,                
 CASE ISNULL(POL.HOLDER_ADD2,'') WHEN '' THEN MHL.HOLDER_ADD2 ELSE POL.HOLDER_ADD2 END HOLDER_ADD2,                
 CASE ISNULL(POL.HOLDER_CITY,'') WHEN '' THEN MHL.HOLDER_CITY ELSE POL.HOLDER_CITY END HOLDER_CITY,                           
 CASE WHEN POL.HOLDER_STATE IS NOT NULL  THEN MNT.STATE_CODE ELSE MNT1.STATE_CODE END AS HOLDER_STATE,                             
 CASE ISNULL(POL.HOLDER_ZIP,'') WHEN '' THEN MHL.HOLDER_ZIP  ELSE POL.HOLDER_ZIP END HOLDER_ZIP,    
 'Loan/Reference No: ' + ISNULL(POL.LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER                        
 FROM POL_HOME_OWNER_ADD_INT POL (NOLOCK)                             
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST MHL ON POL.HOLDER_ID=MHL.HOLDER_ID AND ISNULL(MHL.IS_ACTIVE,'N') = 'Y'                        
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT ON MNT.STATE_ID=POL.HOLDER_STATE                           
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT1 ON MNT1.STATE_ID=MHL.HOLDER_STATE              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@CURRENT_VERSION_ID                         
   AND ISNULL(POL.IS_ACTIVE,'N') = 'Y' AND  (ISNULL(POL.HOLDER_NAME,'') <> '' OR ISNULL(MHL.HOLDER_NAME,'')<>'')                                      
END                          
ELSE IF(@POLICY_LOB='4')                          
BEGIN                          
 SELECT 'Lien Holder:' AS HOLDER_TYPE,HOLDER_NAME,              
 CASE ISNULL(POL.HOLDER_ADD1,'') WHEN '' THEN HOLDER_ADD1 ELSE POL.HOLDER_ADD1 END HOLDER_ADD1,              
 CASE ISNULL(POL.HOLDER_ADD2,'') WHEN '' THEN HOLDER_ADD2 ELSE POL.HOLDER_ADD2 END HOLDER_ADD2,                               
 CASE ISNULL(POL.HOLDER_CITY,'') WHEN '' THEN HOLDER_CITY ELSE POL.HOLDER_CITY END HOLDER_CITY,                         
 CASE WHEN POL.HOLDER_STATE IS NOT NULL THEN MNT.STATE_CODE END AS HOLDER_STATE,                           
 CASE ISNULL(POL.HOLDER_ZIP,'') WHEN '' THEN HOLDER_ZIP  ELSE POL.HOLDER_ZIP END HOLDER_ZIP ,    
 ' Loan/Reference No: ' + ISNULL(POL.LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER              
 FROM POL_WATERCRAFT_COV_ADD_INT POL (NOLOCK)             
 INNER JOIN MNT_COUNTRY_STATE_LIST MNT                              
  ON MNT.STATE_ID=POL.HOLDER_STATE                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@CURRENT_VERSION_ID                              
   AND ISNULL(POL.IS_ACTIVE,'N') = 'Y'  AND isnull(POL.HOLDER_NAME,'') <> ''      
        UNION                         
 SELECT 'Lien Holder:' AS HOLDER_TYPE,HOLDER_NAME,              
 CASE ISNULL(POL.HOLDER_ADD1,'') WHEN '' THEN HOLDER_ADD1 ELSE POL.HOLDER_ADD1 END HOLDER_ADD1,         
 CASE ISNULL(POL.HOLDER_ADD2,'') WHEN '' THEN HOLDER_ADD2 ELSE POL.HOLDER_ADD2 END HOLDER_ADD2,                           
 CASE ISNULL(POL.HOLDER_CITY,'') WHEN '' THEN HOLDER_CITY ELSE POL.HOLDER_CITY END HOLDER_CITY,                         
 CASE WHEN POL.HOLDER_STATE IS NOT NULL THEN MNT.STATE_CODE END AS HOLDER_STATE,                           
 CASE ISNULL(POL.HOLDER_ZIP,'') WHEN '' THEN HOLDER_ZIP  ELSE POL.HOLDER_ZIP END HOLDER_ZIP,    
 ' Loan/Reference No: ' + ISNULL(POL.LOAN_REF_NUMBER,'') AS LOAN_REF_NUMBER                        
 FROM POL_WATERCRAFT_TRAILER_ADD_INT POL (NOLOCK)                             
 INNER JOIN MNT_COUNTRY_STATE_LIST MNT                              
 ON MNT.STATE_ID=POL.HOLDER_STATE                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@CURRENT_VERSION_ID                          
   AND ISNULL(POL.IS_ACTIVE,'N') = 'Y' AND POL.HOLDER_NAME IS NOT NULL                
END                          
ELSE                          
BEGIN                          
-- SELECT 'NA' AS HOLDER_TYPE                        
 SELECT '' AS HOLDER_TYPE, HOLDER_ADD1, HOLDER_ADD2, HOLDER_CITY, MNT.STATE_CODE AS HOLDER_STATE,              
 HOLDER_ZIP FROM POL_ADD_OTHER_INT POL (NOLOCK)                          
 INNER JOIN MNT_COUNTRY_STATE_LIST MNT                              
  ON MNT.STATE_ID=POL.HOLDER_STATE                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@CURRENT_VERSION_ID                              
   AND ISNULL(POL.IS_ACTIVE,'N') = 'Y'                                
END                          
                        
                        
-- Recordset 8                          
--select other policies of the selected customer                              
 --PRINT 'Agency Wolverine'                        
 SELECT                              
 POLICY_NUMBER AS POLICY_NO,                              
 CONVERT(VARCHAR(20),POL.APP_EFFECTIVE_DATE,101) + '-' + CONVERT(VARCHAR(20),POL.APP_EXPIRATION_DATE,101)  AS APP_TERM,                               
     
--CONVERT(VARCHAR(20),CLT.CUSTOMER_FIRST_NAME) + ' ' + CONVERT(VARCHAR(20),ISNULL(CLT.CUSTOMER_MIDDLE_NAME,'')) + ' '                         
-- + CONVERT(VARCHAR(20),ISNULL(CLT.CUSTOMER_LAST_NAME,'')) AS CUSTOMER_NAME,                              
RTRIM(ISNULL(CLT.CUSTOMER_FIRST_NAME,'')) + ' ' +                                             
CASE WHEN CLT.CUSTOMER_MIDDLE_NAME IS NULL THEN '' ELSE RTRIM(CLT.CUSTOMER_MIDDLE_NAME) + ' ' END +                           
ISNULL(CLT.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME ,    
    
 LOB.LOB_DESC AS LOB_DESC,          
-- AGEN.AGENCY_CODE AS AGENCY_ID--POL.AGENCY_ID              
/*Policy Status Check : kasana*/                      
 dbo.fun_GetPolicyDisplayStatus(POL.CUSTOMER_ID,POL.POLICY_ID,POL.POLICY_VERSION_ID) AS POLICY_STATUS            
 ,AGEN.AGENCY_DISPLAY_NAME AS AGEN_NAME,ACT.PLAN_DESCRIPTION AS PMT_PLAN,'' AS STATUS                              
 FROM POL_CUSTOMER_POLICY_LIST POL (NOLOCK)                 
 INNER JOIN CLT_CUSTOMER_LIST CLT                              
 ON CLT.CUSTOMER_ID=POL.CUSTOMER_ID                               
 INNER JOIN MNT_LOB_MASTER LOB                              
 ON POL.POLICY_LOB=LOB.LOB_ID                              
 INNER JOIN MNT_AGENCY_LIST AGEN                              
 ON POL.AGENCY_ID=AGEN.AGENCY_ID                              
 LEFT OUTER JOIN ACT_INSTALL_PLAN_DETAIL  ACT ON POL.INSTALL_PLAN_ID = ACT.IDEN_PLAN_ID                              
 WHERE POL.CUSTOMER_ID=@CUSTOMER_ID --AND POLICY_STATUS = 'NORMAL'                        
 and pol.policy_id <> @policy_id             
          
/*Get Latest version of Policy Check : kasana*/             
  AND POL.POLICY_VERSION_ID IN(     
SELECT max(POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
  POL_CUSTOMER_POLICY_LIST.policy_number = pol.policy_number and          
    POLICY_ID <> @policy_id)          
 --POLICY_NUMBER = POL.POLICY_NUMBER          
          
          
                   
/*                  
Added BY : Pawan , to display concerned policies to agencies, all to wolverine                  
*/                  
AND UPPER(AGEN.Agency_Code) = CASE UPPER(@Agency_Code) WHEN @SYSTEM_CODE THEN UPPER(AGEN.Agency_Code) ELSE  UPPER(@Agency_Code)  END                      
ORDER BY POLICY_NUMBER              
END              
          
--go           

--exec Proc_AccountEnquiry 2358 , 18 ,'123452010450196000044' ,'12-07-2008','BR01'                            

--rollback tran              




GO

