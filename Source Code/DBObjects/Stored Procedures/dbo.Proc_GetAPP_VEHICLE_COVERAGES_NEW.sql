IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_VEHICLE_COVERAGES_NEW]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_VEHICLE_COVERAGES_NEW]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_GetAPP_VEHICLE_COVERAGES_NEW                                    
/*                                                            
----------------------------------------------------------                                                                
Proc Name       : dbo.Proc_GetAPP_VEHICLE_COVERAGES_NEW                                                            
Created by      : Pradeep                                                              
Date            : 26 May,2005                                                                
Purpose         : Selects a single record from UMBRELLA_LIMITS                                                                
Revison History :                      
Created by      : Pravesh                                                              
Date            : 13 Nov,2006                                                                
Purpose         : modify Order by of 2nd table
Used In         : Wolverine               
  
Modified By  : Ravindra    
Modified On : 04-28-2006  
Purpose  : Implemented Grandfather Option                                                    
------------------------------------------------------------                                                                
Date     Review By          Comments                                                                
------   ------------       -------------------------                                                               
*/                                                            
                                                            
CREATE PROCEDURE [dbo].[Proc_GetAPP_VEHICLE_COVERAGES_NEW]                                                            
(                                                            
 @CUSTOMER_ID int,                                                            
 @APP_ID int,                                                            
 @APP_VERSION_ID smallint,                                                            
 @VEHICLE_ID smallint,                                                          
 @APP_TYPE Char(1)                                            
                                                      
)                                                            
                                                            
As                                                            
                                                            
BEGIN                        
                                                          
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
 [COV_DESC] VarChar(100),                  
 [LIMIT_OVERRIDE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                 
 [LIMIT_1] [decimal](18) NULL ,                                                            
 [LIMIT1_AMOUNT_TEXT] NVarChar(100),                                
 [LIMIT_2] [decimal](18) NULL ,                                                            
 [LIMIT2_AMOUNT_TEXT] NVarChar(100),                                                            
 [DEDUCT_OVERRIDE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                                            
 [DEDUCTIBLE_1] [decimal](18) NULL ,                                       
 [DEDUCTIBLE1_AMOUNT_TEXT] NVarChar(100),                                                               
 [DEDUCTIBLE_2] [decimal](18) NULL ,                                                       
 [DEDUCTIBLE2_AMOUNT_TEXT] NVarChar(100),                                    
 [IS_SYSTEM_COVERAGE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,                                                            
 [COVERAGE_ID] int,                                                          
 [LIMIT_TYPE] NChar(1),                                                          
 [DEDUCTIBLE_TYPE] NChar(1),                                                             
 [LIMIT_1_DISPLAY] NVarChar(100) ,                                                      
 [LIMIT_2_DISPLAY] NVarChar(100) ,                                                      
 [DEDUCTIBLE_1_DISPLAY] NVarChar(100) ,                                                      
 [DEDUCTIBLE_2_DISPLAY] NVarChar(100),                                               
 [IS_MANDATORY] Char(1),   
  [INCLUDED_TEXT] char(20),  
 [IS_LIMIT_APPLICABLE] NChar(1),                                                  
 [IS_DEDUCT_APPLICABLE] NChar(1) ,                                        
 [SIGNATURE_OBTAINED]  NChar(1) ,                                      
 [RANK] Decimal(7,2),                                    
 [COVERAGE_TYPE] NChar(10),                      
 [VEHICLE_COVERAGE_CODE_ID] Int,                      
 [IS_ACTIVE] NChar(1) ,                
 [LIMIT_ID] Int,                
 [DEDUC_ID] Int,              
 [EFFECTIVE_FROM_DATE] datetime,  
 [EFFECTIVE_TO_DATE] datetime ,  
  [ISADDDEDUCTIBLE_APP]   NChar(1),  
  [ADD_INFORMATION]       nvarchar(50)  
)                                                            
                                                           
                                       
                                                       
INSERT INTO #COVERAGES                                                            
(                                                            
 COV_ID,                                                         
 COV_CODE,                                                          
 COV_DESC,                                                            
 LIMIT_OVERRIDE,                                                            
 LIMIT_1,                             
 LIMIT1_AMOUNT_TEXT,                                                            
 LIMIT_2,                                                         
 LIMIT2_AMOUNT_TEXT,                                                               
 DEDUCT_OVERRIDE,                    
 DEDUCTIBLE_1,                                                       
 DEDUCTIBLE1_AMOUNT_TEXT,                     
 DEDUCTIBLE_2,                                                      
 DEDUCTIBLE2_AMOUNT_TEXT,                            
 IS_SYSTEM_COVERAGE,                             
 COVERAGE_ID,                                                          
 LIMIT_TYPE,             
 DEDUCTIBLE_TYPE ,               
 IS_MANDATORY ,  
  INCLUDED_TEXT,      
 LIMIT_1_DISPLAY  ,                                                      
 LIMIT_2_DISPLAY ,                      
 DEDUCTIBLE_1_DISPLAY  ,                 
 DEDUCTIBLE_2_DISPLAY,                          
 IS_LIMIT_APPLICABLE,                                                  
 IS_DEDUCT_APPLICABLE ,                                               
 SIGNATURE_OBTAINED,                                      
 RANK,                                    
 COVERAGE_TYPE ,                      
 VEHICLE_COVERAGE_CODE_ID,                      
 IS_ACTIVE,                
 LIMIT_ID,                
 DEDUC_ID,  
 EFFECTIVE_FROM_DATE,  
 EFFECTIVE_TO_DATE ,  
  ISADDDEDUCTIBLE_APP,  
  ADD_INFORMATION            
)                                                            
SELECT                                                           
 C.COV_ID,                                                            
 C.COV_CODE,                                                            
 C.COV_DES,                                                          
 AVC.LIMIT_OVERRIDE,                                                            
 AVC.LIMIT_1,                                                       
 AVC.LIMIT1_AMOUNT_TEXT,                                                            
 AVC.LIMIT_2,                                                         
 AVC.LIMIT2_AMOUNT_TEXT,                                                               
 AVC.DEDUCT_OVERRIDE,                      
 AVC.DEDUCTIBLE_1,                                                            
 AVC.DEDUCTIBLE1_AMOUNT_TEXT,                                                             
 AVC.DEDUCTIBLE_2,                                                         
 AVC.DEDUCTIBLE2_AMOUNT_TEXT,                                                            
 AVC.IS_SYSTEM_COVERAGE,                                                            
 AVC.COVERAGE_ID,                                                           
 C.LIMIT_TYPE,                                                          
 C.DEDUCTIBLE_TYPE,                                                        
 C.IS_MANDATORY ,    
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,                        
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                                                       
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                                                      
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                                       
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,                      
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +                                                       
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                                      
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                                       
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,                                                  
 C.ISLIMITAPPLICABLE,                                                  
 C.ISDEDUCTAPPLICABLE ,                                          
 AVC.SIGNATURE_OBTAINED  ,                                      
 C.RANK,     
 C.COVERAGE_TYPE,     
 AVC.COVERAGE_CODE_ID,                      
 C.IS_ACTIVE,                
 AVC.LIMIT_ID,                
 AVC.DEDUC_ID,  
 C.EFFECTIVE_FROM_DATE,  
 C.EFFECTIVE_TO_DATE ,  
  C.ISADDDEDUCTIBLE_APP,  
  AVC.ADD_INFORMATION   
     
 FROM MNT_COVERAGE C                    
  LEFT OUTER JOIN APP_VEHICLE_COVERAGES  AVC ON                                                 
  C.COV_ID = AVC.COVERAGE_CODE_ID   
AND CUSTOMER_ID = @CUSTOMER_ID  
  AND APP_ID = @APP_ID   
  AND APP_VERSION_ID = @APP_VERSION_ID  
  AND VEHICLE_ID = @VEHICLE_ID                                                 
 WHERE   
  STATE_ID = @STATEID  
  AND LOB_ID = @LOBID   
  AND PURPOSE IN (1 , 3) --purpose should either new business or both                              
  AND C.COV_ID IN                   
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
  R.LIMIT_DEDUC_AMOUNT_TEXT,                      
  R.LIMIT_DEDUC_AMOUNT1_TEXT ,     
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                                                      
   ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                                       
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,                                                       
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                                       
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/' +                                                       
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                                       
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                                                      
  as SplitAmount,                                            
  ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT),'') +                                                     
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display_Orig,                                                    
  ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1),'') +                                                     
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display_Orig,                                                     
  ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT),'')  +                                                     
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') +  '/' +                                                     
  ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1),'') +                                                     
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                                              
  as SplitAmount_Orig,                                                  
  R.IS_DEFAULT,                      
  R.IS_ACTIVE ,  
  R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,  
  R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE     
  FROM MNT_COVERAGE_RANGES R                                                           
  INNER JOIN #COVERAGES C ON  
   C.COV_ID = R.COV_ID           
  WHERE R.IS_ACTIVE = 1                  
  AND R.LIMIT_DEDUC_ID  IN                 
 (                
	 SELECT LIMIT_DEDUC_ID  FROM MNT_COVERAGE_RANGES R1 WHERE  R1.LIMIT_DEDUC_TYPE = 'Limit'   
	 AND R1.COV_ID =  C.COV_ID   AND  (@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') )          
	 AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
	 OR ( C.LIMIT_ID = LIMIT_DEDUC_ID )          
	 	   
	 UNION                
	   
	 SELECT LIMIT_DEDUC_ID  FROM MNT_COVERAGE_RANGES R1  WHERE  R1.LIMIT_DEDUC_TYPE = 'Deduct'  
	 AND R1.COV_ID =  C.COV_ID  AND  (@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND  ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') )          
	 AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
	 OR ( C.DEDUC_ID = LIMIT_DEDUC_ID )          
 ) 
                                                     
 ORDER BY R.LIMIT_DEDUC_AMOUNT,R.COV_ID,                                                   
  R.LIMIT_DEDUC_ID                                                                                    
                     
   
 DROP TABLE #COVERAGES                                   
                                                         
 --Table 2                                                       
 --Get the State for the application                                                        
 EXEC Proc_GetApplicationState @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID 
                               
 --Table 3                          
 --Get vehicle details                         
 SELECT *,'APP' AS CALLEDFROM         
 FROM APP_VEHICLES                       
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                                
 APP_ID = @APP_ID AND                                                            
    APP_VERSION_ID = @APP_VERSION_ID AND                                                            
    VEHICLE_ID = @VEHICLE_ID      
 --Table 4  
SELECT convert(int,SUM(ITEM_VALUE)) as SUM_MIS,count(customer_id) as MIS_COUNT FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES APP_MIS    
WHERE APP_MIS.CUSTOMER_ID = @CUSTOMER_ID     
 AND APP_MIS.APP_ID  = @APP_ID    
 AND APP_MIS.APP_VERSION_ID  = @APP_VERSION_ID  
 AND APP_MIS.VEHICLE_ID=@VEHICLE_ID  and ITEM_VALUE!=0  
 --Table 5  
-- select COUNT(CUSTOMER_ID) AS NO_CLAIMS from APP_PRIOR_LOSS_INFO  
--  where CUSTOMER_ID=@CUSTOMER_ID and LOSS_TYPE=9765 AND  OCCURENCE_DATE > DATEADD(YEAR,-3 ,@APP_EFFECTIVE_DATE)  
  
DECLARE @NO_CLAIMS INT,@NO_PRIOR_LOSS INT
SELECT @NO_PRIOR_LOSS = COUNT(CUSTOMER_ID)  FROM APP_PRIOR_LOSS_INFO
	WHERE CUSTOMER_ID=@CUSTOMER_ID 
	--AND LOSS_TYPE=9765 
	AND ISNULL(CLAIMS_TYPE,0)=14234
--	AND OCCURENCE_DATE >= DATEADD(DD,-3*365 ,@APP_EFFECTIVE_DATE)   
    AND OCCURENCE_DATE >= DATEADD(DD,-DATEDIFF(DAY,DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE)   
SELECT @NO_CLAIMS=COUNT(C.CUSTOMER_ID)
	FROM CLM_CLAIM_INFO C WITH (NOLOCK)            
	WHERE C.CUSTOMER_ID=@CUSTOMER_ID
	AND ISNULL(C.IS_ACTIVE,'Y')='Y' 
	AND ISNULL(PINK_SLIP_TYPE_LIST,'') like '%13005%'  -- at fault
--	AND LOSS_DATE >=DATEADD(DD,-3*365 ,@APP_EFFECTIVE_DATE)   
    AND LOSS_DATE >=DATEADD(DD,-DATEDIFF(DAY,DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE) ,@APP_EFFECTIVE_DATE)   

--TABLE 5
SELECT @NO_CLAIMS+@NO_PRIOR_LOSS AS NO_CLAIMS

DECLARE @UNDER_25_AGE SMALLINT
SET @UNDER_25_AGE=0
IF EXISTS
( SELECT DRIVER_DOB FROM APP_DRIVER_DETAILS AD WITH(NOLOCK)
--	INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ASV WITH(NOLOCK) ON ASV.CUSTOMER_ID=AD.CUSTOMER_ID AND ASV.APP_ID=AD.APP_ID 
--	AND ASV.APP_VERSION_ID=AD.APP_VERSION_ID AND ASV.DRIVER_ID=AD.DRIVER_ID 
	--AND ASV.APP_VEHICLE_PRIN_OCC_ID<>'11931' 
--	AND ASV.VEHICLE_ID=@VEHICLE_ID
	WHERE AD.CUSTOMER_ID=@CUSTOMER_ID AND AD.APP_ID=@APP_ID AND AD.APP_VERSION_ID=@APP_VERSION_ID
	AND DBO.GETAGE(DRIVER_DOB ,@APP_EFFECTIVE_DATE) < 25
)
SET @UNDER_25_AGE=1
--Table 6  
SELECT ISNULL(APPLY_PERS_UMB_POL,0) AS UMB_POL,@UNDER_25_AGE as UNDER_25_AGE FROM APP_AUTO_GEN_INFO WHERE   
 CUSTOMER_ID = @CUSTOMER_ID     
 AND APP_ID  = @APP_ID    
 AND APP_VERSION_ID  = @APP_VERSION_ID  
                                                                                                         
END                                                            
                                                          
                                                             
    



GO

