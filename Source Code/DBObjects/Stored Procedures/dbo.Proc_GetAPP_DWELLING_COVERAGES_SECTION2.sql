IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_DWELLING_COVERAGES_SECTION2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_DWELLING_COVERAGES_SECTION2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*                              
----------------------------------------------------------                                      
Proc Name       : dbo.Proc_GetAPP_DWELLING_COVERAGES_SECTION2                                  
Created by      : Pradeep                                    
Date            : 26 May,2005                                      
Purpose         : Selects a single record from UMBRELLA_LIMITS                                      
Revison History :                                      
Used In         : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------                                     
*/                                  
                                  
-- DROP PROC Proc_GetAPP_DWELLING_COVERAGES_SECTION2 1692,252,1,1,'','S2' 
CREATE PROCEDURE Proc_GetAPP_DWELLING_COVERAGES_SECTION2                                  
(                                  
 @CUSTOMER_ID int,                                  
 @APP_ID int,                                  
 @APP_VERSION_ID smallint,                                  
 @DWELLING_ID smallint,                                
 @APP_TYPE Char(1),                              
 @COVERAGE_TYPE nchar(10)                                
)                                  
                                  
As                                  
  
DECLARE @STATEID SmallInt                                  
DECLARE @LOBID NVarCHar(5)                                  
DECLARE @APP_EFFECTIVE_DATE DateTime            
DECLARE @APP_INCEPTION_DATE DateTime                                                  
                                
  
SELECT @STATEID = STATE_ID,                                  
@LOBID = APP_LOB,  
@APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,            
@APP_INCEPTION_DATE = APP_INCEPTION_DATE                                             
FROM APP_LIST                                  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
APP_ID = @APP_ID AND                                  
APP_VERSION_ID = @APP_VERSION_ID                                  
                                
CREATE TABLE #COVERAGES                                  
(                                  
  [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                                  
  [COV_ID] [int] NOT NULL ,                                  
  [COV_CODE] VarChar(10) NOT NULL ,                                
  [COV_DESC] VarChar(500),                                    
  [LIMIT_OVERRIDE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                  
  [LIMIT_1] [decimal](18) NULL ,                                  
  [LIMIT_2] [decimal](18) NULL ,                                  
  [DEDUCT_OVERRIDE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                  
  [DEDUCTIBLE_1] [decimal](18) NULL ,                                  
  [DEDUCTIBLE_2] [decimal](18) NULL ,                                  
  [IS_SYSTEM_COVERAGE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,                                  
  [COVERAGE_ID] int,                                
  [LIMIT_TYPE] NChar(1),                                
  [DEDUCTIBLE_TYPE] NChar(1),                                
  [IS_MANDATORY] NChar(1) ,                              
  [INCLUDED] decimal(18) ,                
  [LIMIT_1_DISPLAY] NVarChar(100) ,                                  
  [LIMIT_2_DISPLAY] NVarChar(100) ,                                  
  [DEDUCTIBLE_1_DISPLAY] NVarChar(100) ,                                  
  [DEDUCTIBLE_2_DISPLAY] NVarChar(100),                
  [RANK] Decimal(7,2),                           
  [LIMIT_ID] Int,              
  [DEDUC_ID] Int   ,  
  [EFFECTIVE_FROM_DATE] datetime,  
  [EFFECTIVE_TO_DATE] datetime ,  
       
)                                  
     INSERT INTO #COVERAGES                   
 (                              
 COV_ID,                      
 COV_CODE,      
 COV_DESC,                                  
 LIMIT_OVERRIDE,                                  
 LIMIT_1,                                  
 LIMIT_2,                                  
 DEDUCT_OVERRIDE,                                  
 DEDUCTIBLE_1,                                  
 DEDUCTIBLE_2,                                    
 IS_SYSTEM_COVERAGE,                        
 COVERAGE_ID,                                
 LIMIT_TYPE,                                
 DEDUCTIBLE_TYPE,                                
 IS_MANDATORY   ,                              
 INCLUDED ,                
 LIMIT_1_DISPLAY  ,                        
 LIMIT_2_DISPLAY ,                                  
 DEDUCTIBLE_1_DISPLAY  ,                                  
 DEDUCTIBLE_2_DISPLAY ,                
 RANK ,            
 LIMIT_ID,              
 DEDUC_ID ,  
 EFFECTIVE_FROM_DATE,  
 EFFECTIVE_TO_DATE   
       
 )                                  
 SELECT                                 
 C.COV_ID,                                  
 C.COV_CODE,                                  
 C.COV_DES,                                
 AVC.LIMIT_OVERRIDE,                                  
 AVC.LIMIT_1,                                  
 AVC.LIMIT_2,                                  
 AVC.DEDUCT_OVERRIDE,                                  
 case AVC.DEDUCTIBLE_1 when -1.00 then NULL else AVC.DEDUCTIBLE_1 end  as DEDUCTIBLE_1,   --AVC.DEDUCTIBLE_1,                     
 AVC.DEDUCTIBLE_2,                                    
 AVC.IS_SYSTEM_COVERAGE,                                  
 AVC.COVERAGE_ID,                                
 C.LIMIT_TYPE,                                
 C.DEDUCTIBLE_TYPE,                                
 C.IS_MANDATORY ,                  
 CASE AVC.LIMIT_1                        
 WHEN NULL THEN C.INCLUDED                        
 ELSE  AVC.LIMIT_1                        
 END,                        
 ISNULL(Convert(VarChar(20),AVC.LIMIT_1),'') +                                   
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''),                                  
 ISNULL(Convert(VarChar(20),AVC.LIMIT_2),'') +                                   
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''),                                   
 ISNULL(Convert(VarChar(20),AVC.DEDUCTIBLE_1),'') +                                   
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''),                                   
 ISNULL(Convert(VarChar(20),AVC.DEDUCTIBLE_2),'') +                                   
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') ,                
 C.RANK ,    
 AVC.LIMIT_ID,              
 AVC.DEDUC_ID,                                
 C.EFFECTIVE_FROM_DATE AS EFFECTIVE_FROM_DATE ,  
 C.EFFECTIVE_TO_DATE  AS EFFECTIVE_TO_DATE  
 FROM MNT_COVERAGE C                                
 LEFT OUTER JOIN APP_DWELLING_SECTION_COVERAGES  AVC ON                                
 C.COV_ID = AVC.COVERAGE_CODE_ID AND                                
 CUSTOMER_ID = @CUSTOMER_ID AND                                  
 APP_ID = @APP_ID AND                                  
 APP_VERSION_ID = @APP_VERSION_ID AND                                  
 DWELLING_ID = @DWELLING_ID                                
 WHERE  STATE_ID = @STATEID AND                                  
  LOB_ID = @LOBID AND                                  
 IS_ACTIVE = 'Y' AND                                
 C.COVERAGE_TYPE=@COVERAGE_TYPE AND                                
 PURPOSE IN (1 , 3) --Purpose should either new business or both                                     
 AND C.COV_ID IN               
 (              
  SELECT C1.COV_ID                   
  FROM MNT_COVERAGE C1                  
  WHERE @APP_EFFECTIVE_DATE BETWEEN  C1.EFFECTIVE_FROM_DATE AND        
  ISNULL(C1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')  
  AND @APP_EFFECTIVE_DATE <= ISNULL(C1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
 )  
 OR   
 (   
  AVC.COVERAGE_CODE_ID IS NOT NULL   
  AND C.COVERAGE_TYPE=@COVERAGE_TYPE   
 )                   
   
                                
--Table 0                                
SELECT * FROM #COVERAGES                 
ORDER BY RANK                               
                                
--Table 1                                
--Get Coverage ranges                                
SELECT  R.COV_ID,                                
 R.LIMIT_DEDUC_ID,               
 R.LIMIT_DEDUC_TYPE,                                
 R.LIMIT_DEDUC_AMOUNT,                                
 R.LIMIT_DEDUC_AMOUNT1,                                
 ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT),'') +                                   
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                             
ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1),'') +                                   
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,                                   
 ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT),'')  +                                   
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/' +                         
 ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1),'') +                                   
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                                  
 as SplitAmount,                                          
 R.IS_DEFAULT,  
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,  
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                  
FROM MNT_COVERAGE_RANGES R                                 
INNER JOIN #COVERAGES C ON                                
 C.COV_ID = R.COV_ID                   
WHERE R.LIMIT_DEDUC_ID IN             
(            
 SELECT LIMIT_DEDUC_ID                
 FROM MNT_COVERAGE_RANGES R1                
 WHERE           
 R1.LIMIT_DEDUC_TYPE = 'Limit'   
 AND R1.IS_ACTIVE = 1  
 AND R1.COV_ID =  C.COV_ID      
 AND  (@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND  
     ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') )          
 AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
 OR ( C.LIMIT_ID = LIMIT_DEDUC_ID )          
   
 UNION                
   
 SELECT LIMIT_DEDUC_ID                
 FROM MNT_COVERAGE_RANGES R1                
 WHERE        
 R1.LIMIT_DEDUC_TYPE = 'Deduct'  
 AND R1.IS_ACTIVE = 1  
 AND R1.COV_ID =  C.COV_ID       
 AND  (@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND  
     ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') )          
 AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
 OR ( C.DEDUC_ID = LIMIT_DEDUC_ID )     
)        
ORDER BY R.LIMIT_DEDUC_AMOUNT                                
  
                              
--Table 2  
SELECT MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,                    
 MNT_COUNTRY_STATE_LIST.STATE_ID,                    
 APP_LIST.POLICY_TYPE                    
 FROM APP_LIST                                
INNER JOIN MNT_LOOKUP_VALUES ON POLICY_TYPE=LOOKUP_UNIQUE_ID                              
INNER JOIN  MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.STATE_ID =APP_LIST.STATE_ID                              
WHERE CUSTOMER_ID= @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID= @APP_VERSION_ID                                
                                
-- Table 3                                
SELECT DWELLING_LIMIT,OTHER_STRU_LIMIT,PERSONAL_PROP_LIMIT,LOSS_OF_USE,PERSONAL_LIAB_LIMIT,MED_PAY_EACH_PERSON FROM APP_DWELLING_COVERAGE                                
WHERE CUSTOMER_ID= @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID= @APP_VERSION_ID AND DWELLING_ID=@DWELLING_ID                                
                                
--Table 4                  
--Get location details for this Dwelling            
SELECT L.IS_PRIMARY            
FROM APP_LOCATIONS L            
INNER JOIN APP_DWELLINGS_INFO D ON            
 L.LOCATION_ID = D.LOCATION_ID            
WHERE D.CUSTOMER_ID= @CUSTOMER_ID AND            
  D.APP_ID=@APP_ID AND             
 D.APP_VERSION_ID= @APP_VERSION_ID AND             
 D.DWELLING_ID=@DWELLING_ID AND            
 L.CUSTOMER_ID= @CUSTOMER_ID AND            
  L.APP_ID=@APP_ID AND             
 L.APP_VERSION_ID= @APP_VERSION_ID                       
            
--Table 5          
--Get HO42 records from Section 2          
SELECT * FROM APP_DWELLING_SECTION_COVERAGES AVC          
INNER JOIN MNT_COVERAGE C ON          
 AVC.COVERAGE_CODE_ID = C.COV_ID          
WHERE AVC.CUSTOMER_ID= @CUSTOMER_ID AND           
 AVC.APP_ID=@APP_ID AND           
 AVC.APP_VERSION_ID= @APP_VERSION_ID           
 AND AVC.DWELLING_ID=@DWELLING_ID AND          
 C.STATE_ID = @STATEID AND                                  
   C.LOB_ID = @LOBID AND          
 C.IS_ACTIVE = 'Y' AND          
 C.COV_ID IN (266,267,270,271)          
    
--Table 6    
SELECT C.COV_ID,C.COV_CODE     
FROM APP_DWELLING_SECTION_COVERAGES AVC          
INNER JOIN MNT_COVERAGE C ON          
 AVC.COVERAGE_CODE_ID = C.COV_ID          
WHERE AVC.CUSTOMER_ID= @CUSTOMER_ID AND           
 AVC.APP_ID=@APP_ID AND           
 AVC.APP_VERSION_ID= @APP_VERSION_ID           
 AND AVC.DWELLING_ID=@DWELLING_ID AND    
AVC.COVERAGE_TYPE = 'S1'    
  
--Table 7  
  
/*  
280-Author/Writer  
250-Actor Or Actress  
275-Athlete  
11817-Celebrity  
432-Entertainer  
11825-Government Appointee - Federal  
11818-Government Appointee - State  
11819-Labor Leader Other Than Steward  
561-Newspaper Editor/Columnist  
11820-Newspaper Publisher  
11821-Newspaper Reporter  
11822-Public Lecturer  
11823-Public Office Holder  
602-Radio/Television Announcer  
607-Radio/Televisn Projection Op.  
11824-Radio/Television Telecaster  
*/  
  
SELECT  COUNT(APP.APPLICANT_ID) as Occupation  FROM   
CLT_APPLICANT_LIST CLT INNER JOIN APP_APPLICANT_LIST APP  
ON CLT.APPLICANT_ID=APP.APPLICANT_ID  
WHERE  
APP.CUSTOMER_ID=@CUSTOMER_ID AND   
APP.APP_ID=@APP_ID AND   
APP.APP_VERSION_ID=@APP_VERSION_ID AND   
CLT.CO_APPLI_OCCU IN(280,250,275,11817,1181,432,11825,11818,11819,561,11820,11821,11822,11823,11824,602,607)   
AND ISNULL(CLT.IS_ACTIVE,'Y') ='Y'  
  
  
  
--Table 8   
-- Get Application State  
EXEC Proc_GetApplicationState @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID    

--Added by Charles on 10-Dec-09 for Itrack 6840
--Table 9
/*Other Structures detail tab 
If Yes to If off premises is liability extended?* 
then automatically check off on the Covg Section II tab 'Additional Premises (Number of Premises) -Occupied by Insured'
Total the number of residences and put in the number in the number field*/
SELECT COUNT(CUSTOMER_ID) AS OFF_LIABILITY_EXTENDED                        
 FROM APP_OTHER_STRUCTURE_DWELLING WITH(NOLOCK)                        
 WHERE                         
 PREMISES_LOCATION = '11840' -- Off Primises                  
 AND CUSTOMER_ID = @CUSTOMER_ID                        
 AND APP_ID = @APP_ID                        
 AND APP_VERSION_ID=@APP_VERSION_ID                        
 AND DWELLING_ID = @DWELLING_ID                        
 AND ISNULL(IS_ACTIVE,'') = 'Y'   
 AND ISNULL(LIABILITY_EXTENDED,10964)=10963  
--Added till here
                        
DROP TABLE #COVERAGES                              
                              
                              
                              

GO

