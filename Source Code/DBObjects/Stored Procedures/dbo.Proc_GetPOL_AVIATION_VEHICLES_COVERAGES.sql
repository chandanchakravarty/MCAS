IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_AVIATION_VEHICLES_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_AVIATION_VEHICLES_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_GetPOL_AVIATION_VEHICLES_COVERAGES                                    
/*                                                            
----------------------------------------------------------                                                                
Proc Name       : dbo.Proc_GetPOL_AVIATION_VEHICLES_COVERAGES                                                            
Created by      : Pravesh K Chandel
Date            : 14 Jan 2010
Purpose         : Get AViation policy Coverages
Revison History :                      
------------------------------------------------------------                                                                
Date     Review By          Comments                                                                
------   ------------       -------------------------                                                               
*/                                                            
CREATE PROCEDURE Proc_GetPOL_AVIATION_VEHICLES_COVERAGES                                                            
(                                                            
 @CUSTOMER_ID int,                                                            
 @POLICY_ID int,                                                            
 @POLICY_VERSION_ID smallint,                                                            
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
 @LOBID = POLICY_LOB,            
 @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,            
 @APP_INCEPTION_DATE = APP_INCEPTION_DATE                                                             
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                         
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                            
 POLICY_ID = @POLICY_ID AND                                         
 POLICY_VERSION_ID = @POLICY_VERSION_ID   
                                                     
CREATE TABLE #COVERAGES                                                  
(                                                            
 [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                                                            
 [COV_ID] [int] NOT NULL ,                                    
 [COV_CODE] VarChar(10) NOT NULL ,  
 [COV_TYPE][nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                 
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
  [ADD_INFORMATION]       nvarchar(50) ,
  [COVERAGE_TYPE_ID] INT,	
  [RATE] DECIMAL(12,4),
  [PREMIUM] DECIMAL (18,2)
)                                                            
                                                           
                                       
                                                       
INSERT INTO #COVERAGES                                                            
(                                                            
 COV_ID,                                                         
 COV_CODE, 
 COV_TYPE,                                                         
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
  ADD_INFORMATION ,
COVERAGE_TYPE_ID,
RATE    ,
PREMIUM       
)                        
SELECT                                                           
 C.COV_ID,                                                            
 C.COV_CODE, 
 case when COV_REF_CODE is null then C.COV_DES else '' end as COV_TYPE ,	                                                           
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
  AVC.ADD_INFORMATION ,
  AVC.COVERAGE_TYPE_ID ,
  AVC.RATE ,
 AVC.WRITTEN_PREMIUM

 FROM MNT_COVERAGE C                    
  LEFT OUTER JOIN POL_AVIATION_VEHICLE_COVERAGES  AVC ON                                                 
  C.COV_ID = AVC.COVERAGE_CODE_ID   
AND CUSTOMER_ID = @CUSTOMER_ID  
  AND POLICY_ID = @POLICY_ID   
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
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
	 AND R1.COV_ID = C.COV_ID  AND  (@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND  ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') )          
	 AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')  
	 OR ( C.DEDUC_ID = LIMIT_DEDUC_ID )          
 ) 
                                                     
 ORDER BY R.LIMIT_DEDUC_AMOUNT,R.COV_ID,                                                   
  R.LIMIT_DEDUC_ID                                                                                    
                     
   
 DROP TABLE #COVERAGES  
  --Get the State for the application   
 EXEC Proc_GetPolicyState @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID 
-- TABLE 3
 SELECT LOOKUP_UNIQUE_ID AS COVERAGE_TYPE_ID,
	LOOKUP_FRAME_OR_MASONRY AS COV_ID,
	LOOKUP_VALUE_DESC AS COV_TYPE_DESC,
	null AS EFFECTIVE_FROM_DATE,
	null AS EFFECTIVE_TO_DATE
 FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID IN (1355,1356)
                                                                                                 
END                                                            
                                                             

GO

