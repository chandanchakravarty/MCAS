IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_RENTAL_DWELLING_COVERAGES_SECTION1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_RENTAL_DWELLING_COVERAGES_SECTION1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                      
----------------------------------------------------------                              
Proc Name       : dbo.Proc_GetPOL_DWELLING_COVERAGES_SECTION1                          
Created by      : SHAFI                            
Date            : 20 FEB 2006                          
Purpose         : Selects a single record from Rental Coverage section                              
Revison History :                              
Used In         : Wolverine                              
modified by 	:Pravesh 
purpose		: to Correct County Name
MODIFIED BY  : PRAVESH 
DATE			: 23 APRIL 2008
PURPOSE			: IF POLICY STATE CHANGE THEN PULL COVERAGES FOR CURRENT STATE 

------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------                             
*/                          
  
--drop proc dbo.Proc_GetPOL_RENTAL_DWELLING_COVERAGES_SECTION1                                
CREATE PROCEDURE dbo.Proc_GetPOL_RENTAL_DWELLING_COVERAGES_SECTION1                          
(                          
  @CUSTOMER_ID int,                          
  @POLICY_ID int,                          
  @POLICY_VERSION_ID smallint,                          
  @DWELLING_ID smallint,                        
  @POLICY_TYPE Char(1),                      
  @COVERAGE_TYPE nchar(10)                        
)                          
                          
As                          
                          
                        
DECLARE @STATEID SmallInt                                    
DECLARE @LOBID NVarCHar(5)                                    
DECLARE @APP_EFFECTIVE_DATE DateTime                    
DECLARE @APP_INCEPTION_DATE DateTime                                                             
DECLARE @POLICY_STATUS NVARCHAR(20)                                                 
  
SELECT  @STATEID = STATE_ID,  
 @POLICY_STATUS = POLICY_STATUS,                                                                
 @LOBID = POLICY_LOB,                
 @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE  ,                
 @APP_INCEPTION_DATE = APP_INCEPTION_DATE                                                              
FROM    POL_CUSTOMER_POLICY_LIST                                                            
WHERE   CUSTOMER_ID = @CUSTOMER_ID AND                                                                
 POLICY_ID = @POLICY_ID AND                                                                
 POLICY_VERSION_ID = @POLICY_VERSION_ID                                                                
  
                                  
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
 [ADDDEDUCTIBLE_TYPE] NChar(1),                                      
 [IS_MANDATORY] NChar(1) ,                                
 [INCLUDED] NVarChar(100),                  
 [INCLUDED_TEXT] NVarChar(100) ,                      
 [LIMIT_1_DISPLAY] NVarChar(100) ,                                    
 [LIMIT_2_DISPLAY] NVarChar(100) ,                  
 [DEDUCTIBLE_1_DISPLAY] NVarChar(100) ,                                
 [DEDUCTIBLE_2_DISPLAY] NVarChar(100),                  
 [RANK] Decimal(7,2),    
 [LIMIT_ID] Int,      
 [DEDUC_ID]  Int,  
     [DEDUCTIBLE] [decimal](18) NULL ,                   
     [DEDUCTIBLE_TEXT] nvarchar(100) NULL,  
     [ADDDEDUCTIBLE_ID] int NULL,  
 [EFFECTIVE_FROM_DATE] datetime,    
 [EFFECTIVE_TO_DATE] datetime ,  
 [COVERAGE_TYPE] nvarchar(5)  
                                    
)                                  
  
 DECLARE @IDENT_COL INT                
 SET  @IDENT_COL = 1                
         
 --Holds effective dates of all versions of current policy                
 DECLARE @TEMP_APP_LIST TABLE                
 (                
 IDENT_COL INT IDENTITY (1,1),                
 APP_EFFECTIVE_DATE DATETIME,                
 POLICY_VERSION_ID INT                
 )                
               
 --Holds coverages applicable to each version of this policy                
 DECLARE @TEMP_COV TABLE                
 (                
 COV_ID INT                
 )                
                 
 DECLARE @TEMP_COV_RANGES TABLE               
 (              
 LIMIT_DEDUC_ID INT                
 )                
                
   
-- Insert APP_EFFECTIVE_DATE of all versions of this policy in temporary table  
 INSERT INTO @TEMP_APP_LIST                
 (                
 APP_EFFECTIVE_DATE,                
 POLICY_VERSION_ID                
 )              
 SELECT APP_EFFECTIVE_DATE,POLICY_VERSION_ID                
  FROM POL_CUSTOMER_POLICY_LIST                
  WHERE CUSTOMER_ID = @CUSTOMER_ID   
 AND POLICY_ID = @POLICY_ID 
 AND STATE_ID=@STATEID                
                
 --Get all versions while renewing                
 DECLARE @APP_EFF_DATE DateTime                
 DECLARE @COV_ID Int                
 DECLARE @CURRENT_VERSION_ID Int                   
 DECLARE @END_EFFECTIVE_DATE DateTime                
                
 WHILE 1 = 1                
 BEGIN                
 IF NOT EXISTS                
   (                
     SELECT IDENT_COL FROM @TEMP_APP_LIST               
     WHERE IDENT_COL = @IDENT_COL                
        )                
   BEGIN                
     BREAK                
   END                
                  
  SELECT @APP_EFF_DATE = APP_EFFECTIVE_DATE,                
   @CURRENT_VERSION_ID = POLICY_VERSION_ID                
   FROM @TEMP_APP_LIST                
 WHERE IDENT_COL = @IDENT_COL                
                  
                   
 /*                
 Insert into temp table the list of all coverages which where available in all versions                
 and which has date range between each of the effective date.                
 Also get coverages which were applicable during any endorsement process                
 */                
 INSERT INTO @TEMP_COV                
 SELECT COV_ID FROM MNT_COVERAGE                
 WHERE @APP_EFF_DATE BETWEEN EFFECTIVE_FROM_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-01-01 16:50:49.333')   
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFF_DATE              
 AND LOB_ID = @LOBID   
 AND STATE_ID = @STATEID   
 AND COV_ID NOT IN (SELECT COV_ID FROM @TEMP_COV)                
   
 --Coverage ranges                
 INSERT INTO @TEMP_COV_RANGES                  
 SELECT LIMIT_DEDUC_ID FROM MNT_COVERAGE_RANGES R                
 INNER JOIN MNT_COVERAGE C ON R.COV_ID = C.COV_ID                
 WHERE @APP_EFF_DATE BETWEEN R.EFFECTIVE_FROM_DATE AND ISNULL(R.EFFECTIVE_TO_DATE,'3000-01-01 16:50:49.333')  
 AND ISNULL(R.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFF_DATE             
 AND C.LOB_ID = @LOBID   
 AND C.STATE_ID = @STATEID   
 AND R.LIMIT_DEDUC_ID NOT IN (SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES)                
     
 SET @IDENT_COL = @IDENT_COL + 1                
 END  --- End While Loop              
  
  
--Insert coverages which were opted in any previous version though not applicable to that version  
 INSERT INTO @TEMP_COV   
 (                  
      COV_ID  
 )    
 SELECT DISTINCT COVERAGE_CODE_ID  FROM POL_DWELLING_SECTION_COVERAGES DW
INNER JOIN MNT_COVERAGE M ON M.COV_ID=DW.COVERAGE_CODE_ID 
 WHERE  
  CUSTOMER_ID = @CUSTOMER_ID     
  AND POLICY_ID = @POLICY_ID    
  AND  COVERAGE_CODE_ID  IS NOT NULL  
  AND M.STATE_ID=@STATEID
 -- Insert Limits(included)  
 INSERT INTO @TEMP_COV_RANGES                    
 (  
   LIMIT_DEDUC_ID  
 )  
 SELECT DISTINCT LIMIT_ID  FROM POL_DWELLING_SECTION_COVERAGES  DW
INNER JOIN MNT_COVERAGE M ON M.COV_ID=DW.COVERAGE_CODE_ID 
 WHERE  
  CUSTOMER_ID = @CUSTOMER_ID     
  AND POLICY_ID = @POLICY_ID    
  AND  LIMIT_ID  IS NOT NULL  
   AND M.STATE_ID=@STATEID 
 -- Insert Deductibles(Additionals)  
 INSERT INTO @TEMP_COV_RANGES                    
 (  
   LIMIT_DEDUC_ID  
 )  
 SELECT DISTINCT DEDUC_ID  FROM POL_DWELLING_SECTION_COVERAGES DW
INNER JOIN MNT_COVERAGE M ON M.COV_ID=DW.COVERAGE_CODE_ID  
 WHERE  
  CUSTOMER_ID = @CUSTOMER_ID     
  AND POLICY_ID = @POLICY_ID    
  AND  DEDUC_ID  IS NOT NULL  
  AND M.STATE_ID=@STATEID 
 -- Insert Deductibles  
 INSERT INTO @TEMP_COV_RANGES                    
 (  
   LIMIT_DEDUC_ID  
 )  
 SELECT DISTINCT ADDDEDUCTIBLE_ID  FROM POL_DWELLING_SECTION_COVERAGES DW
 INNER JOIN MNT_COVERAGE M ON M.COV_ID=DW.COVERAGE_CODE_ID    
 WHERE  
  CUSTOMER_ID = @CUSTOMER_ID     
  AND POLICY_ID = @POLICY_ID    
  AND  DEDUC_ID  IS NOT NULL  
  AND M.STATE_ID=@STATEID   
-------------------------------------------------------------------              
--Start Of New Business  
--If new business select all coverages including Grandfathered  
IF @POLICY_STATUS = 'UISSUE' or @POLICY_STATUS = 'SUSPENDED'  
BEGIN  
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
 ADDDEDUCTIBLE_TYPE,                                   
 IS_MANDATORY   ,                                
 INCLUDED ,                  
 INCLUDED_TEXT,  
 LIMIT_1_DISPLAY  ,                          
 LIMIT_2_DISPLAY ,                                    
 DEDUCTIBLE_1_DISPLAY  ,                                    
 DEDUCTIBLE_2_DISPLAY ,                  
 RANK ,    
 LIMIT_ID,    
 DEDUC_ID ,  
 DEDUCTIBLE,  
 DEDUCTIBLE_TEXT,  
 ADDDEDUCTIBLE_ID,                                
 EFFECTIVE_FROM_DATE,    
 EFFECTIVE_TO_DATE,  
 COVERAGE_TYPE      
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
 C.ADDDEDUCTIBLE_TYPE,                                                                  
 C.IS_MANDATORY ,     
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') as INCLUDED,    
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,          
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                          
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,           
 C.RANK ,    
 AVC.LIMIT_ID,      
 AVC.DEDUC_ID,  
 AVC.DEDUCTIBLE,  
 CONVERT(VARCHAR(20),AVC.DEDUCTIBLE) +                            
 ' ' + ISNULL(AVC.DEDUCTIBLE_TEXT,'') as DEDUCTIBLE_TEXT,  
 AVC.ADDDEDUCTIBLE_ID,  
 C.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,    
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE ,  
 C.COVERAGE_TYPE                                      
 FROM MNT_COVERAGE C                                  
 LEFT OUTER JOIN POL_DWELLING_SECTION_COVERAGES  AVC ON                                  
 C.COV_ID = AVC.COVERAGE_CODE_ID AND                                  
 CUSTOMER_ID = @CUSTOMER_ID AND                                    
 POLICY_ID = @POLICY_ID AND                                    
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                    
 DWELLING_ID = @DWELLING_ID                                  
 WHERE STATE_ID = @STATEID   
 AND LOB_ID = @LOBID  
 AND C.COVERAGE_TYPE IN('S1','S2')  
 AND PURPOSE IN (1 , 3) --Purpose should either new business or both                         
 AND C.COV_ID  IN                       
 (                 
   SELECT COV_ID                     
   FROM MNT_COVERAGE C1                    
    WHERE   ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE             
   AND C1.IS_ACTIVE = 'Y'  
  )   
 OR    
 ( AVC.COVERAGE_CODE_ID IS NOT NULL  AND C.COVERAGE_TYPE IN ('S1','S2'))      
END   --- End Of New Business   
  
-- Start Of Renewal   
-- In case of renewal Select all active coverages and   
-- coverages which are visible in any previous version of policy and are not disabled  
ELSE IF @POLICY_STATUS = 'URENEW'   
BEGIN  
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
 ADDDEDUCTIBLE_TYPE,                                   
 IS_MANDATORY   ,                                
 INCLUDED ,                  
 INCLUDED_TEXT,  
 LIMIT_1_DISPLAY  ,                          
 LIMIT_2_DISPLAY ,                                    
 DEDUCTIBLE_1_DISPLAY  ,                                    
 DEDUCTIBLE_2_DISPLAY ,                  
 RANK ,    
 LIMIT_ID,    
 DEDUC_ID ,  
 DEDUCTIBLE,  
 DEDUCTIBLE_TEXT,  
 ADDDEDUCTIBLE_ID,                                
 EFFECTIVE_FROM_DATE,    
 EFFECTIVE_TO_DATE ,  
 COVERAGE_TYPE      
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
 C.ADDDEDUCTIBLE_TYPE,                                                                  
 C.IS_MANDATORY ,     
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') as INCLUDED,    
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,          
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                          
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,           
 C.RANK ,    
 AVC.LIMIT_ID,      
 AVC.DEDUC_ID,  
 AVC.DEDUCTIBLE,  
 CONVERT(VARCHAR(20),AVC.DEDUCTIBLE) +                            
     ' ' + ISNULL(AVC.DEDUCTIBLE_TEXT,'') as DEDUCTIBLE_TEXT,  
 AVC.ADDDEDUCTIBLE_ID,  
 C.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,    
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE ,  
     C.COVERAGE_TYPE                                      
 FROM MNT_COVERAGE C                                  
 LEFT OUTER JOIN POL_DWELLING_SECTION_COVERAGES  AVC ON                                  
 C.COV_ID = AVC.COVERAGE_CODE_ID AND                                  
 CUSTOMER_ID = @CUSTOMER_ID AND                                    
 POLICY_ID = @POLICY_ID AND                                    
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                    
 DWELLING_ID = @DWELLING_ID                                  
 WHERE STATE_ID = @STATEID   
 AND LOB_ID = @LOBID  
 AND C.COVERAGE_TYPE IN ('S1','S2')   
 AND PURPOSE IN (1 , 3) --Purpose should either new business or both                         
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE  
 AND C.IS_ACTIVE='Y'  
 AND C.COV_ID  IN                         
 (                   
  SELECT DISTINCT COV_ID FROM @TEMP_COV                  
 )   
 OR                  
 (AVC.COVERAGE_CODE_ID IS NOT NULL  AND C.COVERAGE_TYPE IN ('S1','S2'))       
    
END    --- End Of Renewal   
  
-- Start Of Endorsement   
-- Fetch All Active Coverages + Those Grandfathered(not disabled) but available in any previous version  
--     + Those disabled but available in base version of this policy  
ELSE IF @POLICY_STATUS = 'UENDRS'  
BEGIN  
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
 ADDDEDUCTIBLE_TYPE,                                   
 IS_MANDATORY   ,                 
 INCLUDED ,                  
 INCLUDED_TEXT,  
 LIMIT_1_DISPLAY  ,                          
 LIMIT_2_DISPLAY ,                                    
 DEDUCTIBLE_1_DISPLAY  ,                                    
 DEDUCTIBLE_2_DISPLAY ,                  
 RANK ,    
 LIMIT_ID,    
 DEDUC_ID ,  
 DEDUCTIBLE,  
 DEDUCTIBLE_TEXT,  
 ADDDEDUCTIBLE_ID,                                
 EFFECTIVE_FROM_DATE,    
 EFFECTIVE_TO_DATE ,  
        COVERAGE_TYPE       
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
 C.ADDDEDUCTIBLE_TYPE,                                                                  
 C.IS_MANDATORY ,     
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') as INCLUDED,    
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,          
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                          
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,           
 C.RANK ,    
 AVC.LIMIT_ID,      
 AVC.DEDUC_ID,  
 AVC.DEDUCTIBLE,  
 CONVERT(VARCHAR(20),AVC.DEDUCTIBLE) +                            
     ' ' + ISNULL(AVC.DEDUCTIBLE_TEXT,'') as DEDUCTIBLE_TEXT,  
 AVC.ADDDEDUCTIBLE_ID,  
 C.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,    
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE ,  
     C.COVERAGE_TYPE                                      
 FROM MNT_COVERAGE C                                  
 LEFT OUTER JOIN POL_DWELLING_SECTION_COVERAGES  AVC ON                                  
 C.COV_ID = AVC.COVERAGE_CODE_ID AND          
 CUSTOMER_ID = @CUSTOMER_ID AND                                    
 POLICY_ID = @POLICY_ID AND                                    
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                    
 DWELLING_ID = @DWELLING_ID                                  
 WHERE STATE_ID = @STATEID   
 AND LOB_ID = @LOBID  
 AND C.COVERAGE_TYPE IN ('S1','S2')  
 AND PURPOSE IN (1 , 3) --Purpose should either new business or both                         
 AND C.IS_ACTIVE='Y'  
 AND   
 (  
  C.COV_ID  IN                         
  (                   
   SELECT DISTINCT COV_ID FROM @TEMP_COV                  
  )   
 )  
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE  
 OR                  
 (AVC.COVERAGE_CODE_ID IS NOT NULL   AND C.COVERAGE_TYPE IN ('S1','S2'))  
 OR  
 C.COV_ID IN  
 (  
  SELECT  COVERAGE_CODE_ID FROM POL_DWELLING_SECTION_COVERAGES DW
 INNER JOIN MNT_COVERAGE M ON M.COV_ID=DW.COVERAGE_CODE_ID  
  WHERE   
  CUSTOMER_ID= @CUSTOMER_ID   
  AND POLICY_ID=@POLICY_ID 
	AND M.STATE_ID=@STATEID     
  AND POLICY_VERSION_ID IN   
  (  
   SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS   
   WHERE   
   CUSTOMER_ID= @CUSTOMER_ID   
   AND POLICY_ID=@POLICY_ID   
   AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit  
   AND PROCESS_STATUS ='COMPLETE'  
   AND NEW_POLICY_VERSION_ID IN   
    (  
     SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS  
     WHERE  
     CUSTOMER_ID= @CUSTOMER_ID  
     AND POLICY_ID=@POLICY_ID  
     AND PROCESS_ID IN (25,18)  
     AND PROCESS_STATUS ='COMPLETE'  
     )  
  
  )  
 )        
  
END  -- End Of Endorsment  
--If Policy is Active display only coverages which are relevant  
ELSE  
BEGIN  
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
 ADDDEDUCTIBLE_TYPE,                                   
 IS_MANDATORY   ,                                
 INCLUDED ,                  
 INCLUDED_TEXT,  
 LIMIT_1_DISPLAY  ,                          
 LIMIT_2_DISPLAY ,                                    
 DEDUCTIBLE_1_DISPLAY  ,                                    
 DEDUCTIBLE_2_DISPLAY ,                  
 RANK ,    
 LIMIT_ID,    
 DEDUC_ID ,  
 DEDUCTIBLE,  
 DEDUCTIBLE_TEXT,  
 ADDDEDUCTIBLE_ID,                                
 EFFECTIVE_FROM_DATE,    
 EFFECTIVE_TO_DATE ,  
     COVERAGE_TYPE      
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
 C.ADDDEDUCTIBLE_TYPE,                         
 C.IS_MANDATORY ,     
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') as INCLUDED,    
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,          
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                         
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +                                             
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                          
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,           
 C.RANK ,    
 AVC.LIMIT_ID,      
 AVC.DEDUC_ID,  
 AVC.DEDUCTIBLE,  
 CONVERT(VARCHAR(20),AVC.DEDUCTIBLE) +                            
    ' ' + ISNULL(AVC.DEDUCTIBLE_TEXT,'') as DEDUCTIBLE_TEXT,  
 AVC.ADDDEDUCTIBLE_ID,  
 C.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,    
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE  ,  
     C.COVERAGE_TYPE                                     
 FROM MNT_COVERAGE C                                  
 LEFT OUTER JOIN POL_DWELLING_SECTION_COVERAGES  AVC ON                                  
 C.COV_ID = AVC.COVERAGE_CODE_ID AND                          
 CUSTOMER_ID = @CUSTOMER_ID AND                                    
 POLICY_ID = @POLICY_ID AND                                    
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND     
 DWELLING_ID = @DWELLING_ID                                  
 WHERE STATE_ID = @STATEID   
 AND LOB_ID = @LOBID  
 AND C.COVERAGE_TYPE IN ('S1','S2')  
 AND PURPOSE IN (1 , 3) --Purpose should either new business or both                         
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE  
 AND C.IS_ACTIVE='Y'  
 AND C.COV_ID  IN                         
 (     
  SELECT C.COV_ID                     
  FROM MNT_COVERAGE C                    
   WHERE @APP_EFFECTIVE_DATE BETWEEN C.EFFECTIVE_FROM_DATE AND ISNULL(C.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                   
   AND @APP_EFFECTIVE_DATE <= ISNULL(C.DISABLED_DATE,'3000-03-16 16:59:06.630')  
 ) OR                  
 (AVC.COVERAGE_CODE_ID IS NOT NULL   AND C.COVERAGE_TYPE IN ('S1','S2'))  
  
END   
  
--Table 0                                                              
SELECT * FROM #COVERAGES                                            
ORDER BY RANK                                                 
  
  
--Table 1                                                             
--Get Coverage ranges    
--If new Business select all including  grandfathered but not disabled                                                            
IF @POLICY_STATUS = 'UISSUE' or @POLICY_STATUS = 'SUSPENDED'  
BEGIN  
 SELECT  R.COV_ID,                                  
 R.LIMIT_DEDUC_ID,                 
 R.LIMIT_DEDUC_TYPE,                                  
 R.LIMIT_DEDUC_AMOUNT,                                  
 R.LIMIT_DEDUC_AMOUNT1,                                  
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                      
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,    
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/' +                                             
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                      
 as SplitAmount,                                                      
 R.IS_DEFAULT,  
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,  
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                     
 FROM MNT_COVERAGE_RANGES R                                   
 INNER JOIN #COVERAGES C ON                                  
 C.COV_ID = R.COV_ID                     
 WHERE   
 R.IS_ACTIVE = 1                      
 AND R.LIMIT_DEDUC_ID  IN                     
 (                
  SELECT LIMIT_DEDUC_ID                  
  FROM MNT_COVERAGE_RANGES R1                  
  WHERE R1.LIMIT_DEDUC_TYPE = 'Limit' AND                  
  R1.COV_ID =  C.COV_ID    
  AND ( ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE )            
  OR   
  ( C.LIMIT_ID = LIMIT_DEDUC_ID )            
    
  UNION                  
    
  SELECT LIMIT_DEDUC_ID                  
  FROM MNT_COVERAGE_RANGES R1                  
  WHERE R1.LIMIT_DEDUC_TYPE = 'Deduct' AND                  
  R1.COV_ID =  C.COV_ID     AND            
  ( ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE )            
  OR ( C.DEDUC_ID = LIMIT_DEDUC_ID )    
  
  UNION                  
  
  SELECT LIMIT_DEDUC_ID                  
  FROM MNT_COVERAGE_RANGES R1                  
  WHERE R1.LIMIT_DEDUC_TYPE = 'Addded' AND                  
  R1.COV_ID =  C.COV_ID     AND            
  ( ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE )            
  OR ( C.ADDDEDUCTIBLE_ID = LIMIT_DEDUC_ID )                   
   
 )                
 ORDER BY R.LIMIT_DEDUC_AMOUNT                                  
END   
--For renewal select all effective ranges & grandfathered ragnes which are available in previous  
--version and which are not disabled  
ELSE IF @POLICY_STATUS = 'URENEW'    
BEGIN  
 SELECT  R.COV_ID,                                  
 R.LIMIT_DEDUC_ID,                 
 R.LIMIT_DEDUC_TYPE,                                  
 R.LIMIT_DEDUC_AMOUNT,                                  
 R.LIMIT_DEDUC_AMOUNT1,                                  
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                      
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,                                             
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/' +                                             
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                                            
 as SplitAmount,                                                      
 R.IS_DEFAULT,  
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,  
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                     
 FROM MNT_COVERAGE_RANGES R                                   
 INNER JOIN #COVERAGES C ON                                  
 C.COV_ID = R.COV_ID                     
 WHERE   
 R.IS_ACTIVE = 1                      
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE  
 AND R.LIMIT_DEDUC_ID  IN                     
 (                
  SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES              
  UNION              
  SELECT C.LIMIT_ID               
  WHERE C.LIMIT_ID IS NOT NULL               
  UNION              
  SELECT C.DEDUC_ID              
  WHERE C.DEDUC_ID IS NOT NULL               
  UNION    
  SELECT C.ADDDEDUCTIBLE_ID      
  WHERE C.ADDDEDUCTIBLE_ID IS NOT NULL  
 )                
  
 ORDER BY R.LIMIT_DEDUC_AMOUNT                                  
END  
  
-- Endorsment Case  
ELSE IF @POLICY_STATUS = 'UENDRS'  
BEGIN  
 SELECT  R.COV_ID,                                  
 R.LIMIT_DEDUC_ID,                 
 R.LIMIT_DEDUC_TYPE,                                  
 R.LIMIT_DEDUC_AMOUNT,                                  
 R.LIMIT_DEDUC_AMOUNT1,                                  
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                      
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,                                             
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/' +          
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                                            
 as SplitAmount,        
 R.IS_DEFAULT,  
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,  
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                     
 FROM MNT_COVERAGE_RANGES R                                   
 INNER JOIN #COVERAGES C ON                                  
 C.COV_ID = R.COV_ID            
 WHERE   
 R.IS_ACTIVE = 1                      
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE  
 AND R.LIMIT_DEDUC_ID  IN                     
 (                
  SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES              
  UNION              
  SELECT C.LIMIT_ID               
  WHERE C.LIMIT_ID IS NOT NULL               
  UNION              
  SELECT C.DEDUC_ID              
  WHERE C.DEDUC_ID IS NOT NULL               
  UNION              
  SELECT C.ADDDEDUCTIBLE_ID              
  WHERE C.ADDDEDUCTIBLE_ID IS NOT NULL               
   
 )     
 OR  R.LIMIT_DEDUC_ID  IN    
 (  
  SELECT  LIMIT_ID FROM POL_DWELLING_SECTION_COVERAGES DW
	INNER JOIN MNT_COVERAGE M ON M.COV_ID=DW.COVERAGE_CODE_ID  
  WHERE   
  CUSTOMER_ID= @CUSTOMER_ID   
  AND POLICY_ID=@POLICY_ID
	AND	 M.STATE_ID=@STATEID
  AND POLICY_VERSION_ID IN   
  (  
   SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS   
   WHERE   
   CUSTOMER_ID= @CUSTOMER_ID   
   AND POLICY_ID=@POLICY_ID   
   AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit  
   AND PROCESS_STATUS ='COMPLETE'  
   AND NEW_POLICY_VERSION_ID IN   
    (  
     SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS  
     WHERE  
     CUSTOMER_ID= @CUSTOMER_ID  
     AND POLICY_ID=@POLICY_ID  
     AND PROCESS_ID IN (25,18)  
     AND PROCESS_STATUS ='COMPLETE'  
     )  
  
  
  )  
  UNION  
  SELECT  DEDUC_ID FROM POL_DWELLING_SECTION_COVERAGES DW
	INNER JOIN MNT_COVERAGE M ON M.COV_ID=DW.COVERAGE_CODE_ID  
  WHERE   
  CUSTOMER_ID= @CUSTOMER_ID   
  AND POLICY_ID=@POLICY_ID 
  AND M.STATE_ID=@STATEID	  
  AND POLICY_VERSION_ID IN   
  (  
   SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS   
   WHERE   
   CUSTOMER_ID= @CUSTOMER_ID   
   AND POLICY_ID=@POLICY_ID   
   AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit  
   AND PROCESS_STATUS ='COMPLETE'  
   AND NEW_POLICY_VERSION_ID IN   
    (  
     SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS  
     WHERE  
     CUSTOMER_ID= @CUSTOMER_ID  
     AND POLICY_ID=@POLICY_ID  
     AND PROCESS_ID IN (25,18)  
     AND PROCESS_STATUS ='COMPLETE'  
     )  
  
  )  
  UNION  
  SELECT  ADDDEDUCTIBLE_ID FROM POL_DWELLING_SECTION_COVERAGES DW
	INNER JOIN MNT_COVERAGE M ON M.COV_ID=DW.COVERAGE_CODE_ID  
  WHERE   
  CUSTOMER_ID= @CUSTOMER_ID   
  AND POLICY_ID=@POLICY_ID 
 AND M.STATE_ID=@STATEID	  
  AND POLICY_VERSION_ID IN   
  (  
   SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS   
   WHERE   
   CUSTOMER_ID= @CUSTOMER_ID   
   AND POLICY_ID=@POLICY_ID   
   AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit  
   AND PROCESS_STATUS ='COMPLETE'  
   AND NEW_POLICY_VERSION_ID IN   
    (  
     SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS  
     WHERE  
     CUSTOMER_ID= @CUSTOMER_ID  
     AND POLICY_ID=@POLICY_ID  
     AND PROCESS_ID IN (25,18)  
     AND PROCESS_STATUS ='COMPLETE'  
     )  
  
  )  
  
 )  
 ORDER BY R.LIMIT_DEDUC_AMOUNT                                  
END  
-- In case of Active or in active policy display only Effective coverages ranges  
ELSE   
BEGIN  
 SELECT  R.COV_ID,                                  
 R.LIMIT_DEDUC_ID,                 
 R.LIMIT_DEDUC_TYPE,                                  
 R.LIMIT_DEDUC_AMOUNT,                                  
 R.LIMIT_DEDUC_AMOUNT1,                                  
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                      
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                                            
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,                                             
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/' +                  
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                             
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                                            
 as SplitAmount,                                      
 R.IS_DEFAULT,  
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,  
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                     
 FROM MNT_COVERAGE_RANGES R                                   
 INNER JOIN #COVERAGES C ON                                  
 C.COV_ID = R.COV_ID                     
 WHERE   
 R.IS_ACTIVE = 1                      
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE  
 AND R.LIMIT_DEDUC_ID  IN                     
 (                
  SELECT LIMIT_DEDUC_ID                  
  FROM MNT_COVERAGE_RANGES R1                  
  WHERE  R1.LIMIT_DEDUC_TYPE = 'Limit'  
  AND   R1.COV_ID =  C.COV_ID       
  AND            
  (             
   @APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND  
     ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')        
   AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
  )            
  OR ( C.LIMIT_ID = LIMIT_DEDUC_ID )            
   
  UNION                  
    
  SELECT LIMIT_DEDUC_ID                  
  FROM MNT_COVERAGE_RANGES R1                  
  WHERE              
  R1.LIMIT_DEDUC_TYPE = 'Deduct'  
  AND R1.COV_ID =  C.COV_ID      
  AND            
  (             
   @APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND  
     ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')              
   AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
  )            
  OR ( C.DEDUC_ID = LIMIT_DEDUC_ID )     
    
  UNION                  
    
  SELECT LIMIT_DEDUC_ID                  
  FROM MNT_COVERAGE_RANGES R1                  
  WHERE              
  R1.LIMIT_DEDUC_TYPE = 'Addded'  
  AND R1.COV_ID =  C.COV_ID      
  AND            
  (             
   @APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND  
     ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')              
   AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
)            
  OR ( C.ADDDEDUCTIBLE_ID = LIMIT_DEDUC_ID )     
 )  
 ORDER BY R.LIMIT_DEDUC_AMOUNT      
END                     
                      
                        
/*                      
SELECT MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE FROM POL_LIST                        
INNER JOIN MNT_LOOKUP_VALUES ON POLICY_TYPE=LOOKUP_UNIQUE_ID                        
WHERE CUSTOMER_ID= @CUSTOMER_ID AND POL_ID=@POL_ID AND POL_VERSION_ID= @POL_VERSION_ID                        
*/                      
--Table 2                      
SELECT MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,                
 MNT_COUNTRY_STATE_LIST.STATE_ID ,                
 POLICY_TYPE                
FROM POL_CUSTOMER_POLICY_LIST                        
INNER JOIN MNT_LOOKUP_VALUES ON POLICY_TYPE=LOOKUP_UNIQUE_ID                      
INNER JOIN  MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.STATE_ID =POL_CUSTOMER_POLICY_LIST.STATE_ID                      
WHERE CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID= @POLICY_VERSION_ID                        
                        
          
        
----Table 3                                                      
--Home rating Info        
SELECT IS_UNDER_CONSTRUCTION        
FROM POL_HOME_RATING_INFO        
WHERE CUSTOMER_ID= @CUSTOMER_ID AND         
 POLICY_ID=@POLICY_ID AND         
 POLICY_VERSION_ID= @POLICY_VERSION_ID AND      
 DWELLING_ID = @DWELLING_ID       
  
----Table 4  
SELECT @APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE                       
        
-- Table 5  
                                     
SELECT ISNULL(L.IS_PRIMARY,'U') AS IS_PRIMARY,ISNULL(L.LOCATION_TYPE,0) AS LOC_TYPE,  
CONVERT(VARCHAR(20),ISNULL(LOC_ADD1,'')) + ' ' + CONVERT(VARCHAR(20),ISNULL(LOC_ADD2,'')) + ' ' + CONVERT(VARCHAR(20),MNTS.STATE_NAME) + ' ' + CONVERT(VARCHAR(20),ISNULL(LOC_ZIP,'')) AS ADDRESS,  
(  
 SELECT COUNT(LOC_COUNTY) FROM POL_LOCATIONS LOC  
 WHERE LOC.LOC_COUNTY IN   
 (  
  'CLAY','CRAWFORD','DAVIESS','DUBOIS','FOUNTAIN','GIBSON','GREENE','KNOX','LAWRENCE','MARTIN','MONROE',  
  'MONTGOMERY','ORANGE','OWEN','PARKE','PERRY','PIKE','POSEY','PUTNAM','SPENCER','SULLIVAN','VANDERBURGH',  
  'VERMILLION','VIGO','WARREN','WARRICK'  
 )  
 AND LOC.CUSTOMER_ID = @CUSTOMER_ID  
 AND LOC.POLICY_ID = @POLICY_ID  
 AND LOC.POLICY_VERSION_ID=@POLICY_VERSION_ID  
 AND LOC.LOCATION_ID = D.LOCATION_ID   
   
)AS HAS_VALID_COUNTY   
FROM POL_LOCATIONS L                  
INNER JOIN POL_DWELLINGS_INFO D ON                  
 L.LOCATION_ID = D.LOCATION_ID    
INNER JOIN MNT_COUNTRY_STATE_LIST MNTS ON  
 MNTS.STATE_ID=L.LOC_STATE  
WHERE D.CUSTOMER_ID= @CUSTOMER_ID AND                  
 D.POLICY_ID=@POLICY_ID AND                  
 D.POLICY_VERSION_ID= @POLICY_VERSION_ID AND                   
 D.DWELLING_ID=@DWELLING_ID AND                  
 L.CUSTOMER_ID= @CUSTOMER_ID AND                  
 L.POLICY_ID=@POLICY_ID AND                   
 L.POLICY_VERSION_ID= @POLICY_VERSION_ID       
  
            
DROP TABLE #COVERAGES                      
                    
                  
                
              
            
        
        
      
    
  
  
  
  
  
  
  
  
  
  
  
  
  







GO

