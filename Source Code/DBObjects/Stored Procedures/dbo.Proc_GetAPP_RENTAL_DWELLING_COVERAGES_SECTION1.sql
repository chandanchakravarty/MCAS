IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_RENTAL_DWELLING_COVERAGES_SECTION1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_RENTAL_DWELLING_COVERAGES_SECTION1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                    
----------------------------------------------------------                            
Proc Name       : dbo.Proc_GetAPP_DWELLING_COVERAGES_SECTION1                        
Created by      : Pradeep                          
Date            : 26 May,2005                            
Purpose         : Selects a single record from UMBRELLA_LIMITS                            
Revison History :                            
Used In         : Wolverine    
Modified By  	: Ravindra
Modified On  	: 07-10-2006  
Purpose  	:
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------                           
*/                        
--drop proc dbo.Proc_GetAPP_RENTAL_DWELLING_COVERAGES_SECTION1                              
CREATE PROCEDURE dbo.Proc_GetAPP_RENTAL_DWELLING_COVERAGES_SECTION1    
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

SELECT @STATEID = STATE_ID,                        
@LOBID = APP_LOB,
@APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE                                                
FROM APP_LIST                        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                        
APP_ID = @APP_ID AND                        
APP_VERSION_ID = @APP_VERSION_ID                        
                    
CREATE TABLE #COVERAGES                        
(                        
	[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                        
	[COV_ID] [int] NOT NULL ,                        
	[COV_CODE] VarChar(10) NOT NULL ,                      
	[COV_DESC] VarChar(100),                          
	[LIMIT_OVERRIDE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                        
	[LIMIT_1] [decimal](18, 2) NULL ,                        
	[LIMIT_2] [decimal](18, 2) NULL ,                        
	[DEDUCT_OVERRIDE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                        
	[DEDUCTIBLE_1] [decimal](18, 2) NULL ,                        
	[DEDUCTIBLE_2] [decimal](18, 2) NULL ,                        
	[IS_SYSTEM_COVERAGE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,                        
	[COVERAGE_ID] int,                      
	[LIMIT_TYPE] NChar(1),                      
	[DEDUCTIBLE_TYPE] NChar(1), 
	[ADDDEDUCTIBLE_TYPE] NChar(1),                                   
	[IS_MANDATORY] NChar(1) ,                    
	[INCLUDED] NVarChar(100),                
	[INCLUDED_TEXT] NVarChar(100) ,          
	[DEDUCTIBLE] decimal(18,0),
	[DEDUCTIBLE_TEXT] NVarChar(100),
	[RANK] Decimal(7,2),        
	[LIMIT_ID] Int,          
	[DEDUC_ID] Int,
	[ADDDEDUCTIBLE_ID] INT,
	[COVERAGE_TYPE] VARCHAR(2),
	[EFFECTIVE_FROM_DATE] datetime,
	[EFFECTIVE_TO_DATE] datetime    
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
	ADDDEDUCTIBLE_TYPE,       
	IS_MANDATORY   ,                    
	INCLUDED,                
	INCLUDED_TEXT,          
	DEDUCTIBLE ,
	DEDUCTIBLE_TEXT,
	RANK,        
	LIMIT_ID,          
	DEDUC_ID,
	ADDDEDUCTIBLE_ID,
	COVERAGE_TYPE,
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
	C.ADDDEDUCTIBLE_TYPE ,                    
	C.IS_MANDATORY ,                    
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') as INCLUDED,        
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,     
	AVC.DEDUCTIBLE,
	CONVERT(VARCHAR(20),AVC.DEDUCTIBLE) +                          
	' ' + ISNULL(AVC.DEDUCTIBLE_TEXT,'') as DEDUCTIBLE_TEXT,
	C.RANK,       
	AVC.LIMIT_ID,          
	AVC.DEDUC_ID,
	AVC.ADDDEDUCTIBLE_ID,
	C.COVERAGE_TYPE,
	C.EFFECTIVE_FROM_DATE AS EFFECTIVE_FROM_DATE ,
	C.EFFECTIVE_TO_DATE  AS EFFECTIVE_TO_DATE                       
	FROM MNT_COVERAGE C                      
	LEFT OUTER JOIN APP_DWELLING_SECTION_COVERAGES  AVC ON                                      
		C.COV_ID = AVC.COVERAGE_CODE_ID     
		AND CUSTOMER_ID = @CUSTOMER_ID     
		AND APP_ID = @APP_ID     
		AND APP_VERSION_ID = @APP_VERSION_ID     
		AND DWELLING_ID = @DWELLING_ID                                      
	WHERE    
		C.STATE_ID = @STATEID     
		AND C.LOB_ID = @LOBID  
        AND C.COVERAGE_TYPE in ('S1','S2')
		AND C.PURPOSE IN (1 , 3) --Purpose should either new business or both                                                               
		AND C.COV_ID  IN                   
		(                  
			SELECT C.COV_ID                     
			FROM MNT_COVERAGE C                    
			WHERE @APP_EFFECTIVE_DATE BETWEEN  C.EFFECTIVE_FROM_DATE AND          
			ISNULL(C.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')    
			AND @APP_EFFECTIVE_DATE <= ISNULL(C.DISABLED_DATE,'3000-03-16 16:59:06.630')    
		)    
	
		OR ( AVC.COVERAGE_CODE_ID IS NOT NULL )                     
                   
                        
                      
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
ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                          
' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                                                
ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                                 
' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,                                                 
-- ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                           
-- ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/' +                                                 
-- ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                                 
-- ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                    
-- as SplitAmount,    

Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT) + '/' +                       
Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1) as SplitAmount,                      
R.IS_DEFAULT ,
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

    UNION

    SELECT LIMIT_DEDUC_ID              
	FROM MNT_COVERAGE_RANGES R1              
	WHERE      
	R1.LIMIT_DEDUC_TYPE = 'Addded'
	AND R1.IS_ACTIVE = 1
	AND R1.COV_ID =  C.COV_ID     
	AND  (@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND
		   ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') )        
	AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')
	OR ( C.ADDDEDUCTIBLE_ID = LIMIT_DEDUC_ID )   

     
)      
ORDER BY R.LIMIT_DEDUC_AMOUNT                       
                      

--Table 2                    
SELECT MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,              
MNT_COUNTRY_STATE_LIST.STATE_ID ,              
POLICY_TYPE              
FROM APP_LIST                      
INNER JOIN MNT_LOOKUP_VALUES ON POLICY_TYPE=LOOKUP_UNIQUE_ID                    
INNER JOIN  MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.STATE_ID =APP_LIST.STATE_ID                    
WHERE CUSTOMER_ID= @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID= @APP_VERSION_ID                      

             
      
----Table 3               
--Home rating Info      
SELECT IS_UNDER_CONSTRUCTION      
FROM APP_HOME_RATING_INFO      
WHERE CUSTOMER_ID= @CUSTOMER_ID AND       
APP_ID=@APP_ID AND       
APP_VERSION_ID= @APP_VERSION_ID AND       
DWELLING_ID = @DWELLING_ID      

-- Table 4

SELECT @APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE                

-- Table 5
                                   
SELECT ISNULL(L.IS_PRIMARY,'U') AS IS_PRIMARY,ISNULL(L.LOCATION_TYPE,0) AS LOC_TYPE,
CONVERT(VARCHAR(20),ISNULL(LOC_ADD1,'')) + ' ' + CONVERT(VARCHAR(20),ISNULL(LOC_ADD2,'')) + ' ' + CONVERT(VARCHAR(20),MNTS.STATE_NAME) + ' ' + CONVERT(VARCHAR(20),ISNULL(LOC_ZIP,'')) AS ADDRESS,
(
	SELECT COUNT(LOC_COUNTY) FROM APP_LOCATIONS LOC
	WHERE LOC.LOC_COUNTY IN 
	(
		'CLAY','CRAWFORD','DAVIESS','DUBOIS','FOUNTAIN','GIBSON','GREENE','KNOX','LAWRENCE','MARTIN','MONROE',
		'MONTGOMERY','ORANGE','OWEN','PARKE','PERRY','PIKE','POSEY','PUTNAM','SPENCER','SULLIVAN','VANDERBURGH',
		'VERMILLION','VIGO','WARREN','WARRICK'
	)
	AND LOC.CUSTOMER_ID = @CUSTOMER_ID
	AND LOC.APP_ID = @APP_ID
	AND LOC.APP_VERSION_ID=@APP_VERSION_ID
	AND LOC.LOCATION_ID = D.LOCATION_ID 
	
)AS HAS_VALID_COUNTY 
FROM APP_LOCATIONS L                
INNER JOIN APP_DWELLINGS_INFO D ON                
	L.LOCATION_ID = D.LOCATION_ID  
INNER JOIN MNT_COUNTRY_STATE_LIST MNTS ON
	MNTS.STATE_ID=L.LOC_STATE
WHERE D.CUSTOMER_ID= @CUSTOMER_ID AND                
	D.APP_ID=@APP_ID AND                
	D.APP_VERSION_ID= @APP_VERSION_ID AND                 
	D.DWELLING_ID=@DWELLING_ID AND                
	L.CUSTOMER_ID= @CUSTOMER_ID AND                
	L.APP_ID=@APP_ID AND                 
	L.APP_VERSION_ID= @APP_VERSION_ID     

DROP TABLE #COVERAGES                    
                  
                
              

GO

