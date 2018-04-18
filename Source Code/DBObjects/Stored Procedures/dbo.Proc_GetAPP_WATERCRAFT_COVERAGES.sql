IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_WATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_WATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc  Proc_GetAPP_WATERCRAFT_COVERAGES                     
                     
/*                                    
----------------------------------------------------------                                        
Proc Name       : dbo.Proc_GetAPP_WATERCRAFT_COVERAGES                                    
Created by      : Anurag Verma                                      
Date            : 03 Oct,2005                                        
Purpose         : selects record for watercraft coverage                                  
Revison History :                                        
Used In         : Wolverine                                        
                                
Modified By : Anurag verma                                
Modified On : 14/10/2005                                
Purpose : LOBID is hardcoded for watercraft                                
    
Modified By  : Ravindra    
Modified On  : 05-16-2006    
Purpose  : Implementation of Grandfather option    

Modified By  : Pravesh K Chandel
Modified On  : 16 Jan 2009
Purpose  : Making State name in camel Case for Coverage Screen
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------                                       
*/                                    
CREATE         PROCEDURE dbo.Proc_GetAPP_WATERCRAFT_COVERAGES                                      
(                                      
 @CUSTOMER_ID int,                                      
 @APP_ID int,                                      
 @APP_VERSION_ID smallint,                                      
 @WATERCRAFT_ID   smallint,                                    
 @APP_TYPE Char(1)                                    
)                                      
                                      
As                                      
                                      
                                    
DECLARE @STATEID SmallInt                                        
DECLARE @LOBID NVarCHar(5)                                        
 DECLARE @APP_EFFECTIVE_DATE DateTime                                                
                                        
/*------  Modified BY Anurag Verma on 14/10/2005 --- hardcoded LOBID for watercraft - 4                   */                                
SELECT @STATEID = STATE_ID,                                        
 @LOBID = 4 ,    
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
 [IS_LIMIT_APPLICABLE] NChar(1),                   
 [IS_DEDUCT_APPLICABLE] NChar(1),               
 [COVERAGE_TYPE] NChar(10),                       
 [SIGNATURE_OBTAINED] NChar(1) ,    
 [LIMIT_ID] Int,                  
 [DEDUC_ID] Int,       
 [EFFECTIVE_FROM_DATE] datetime,    
 [EFFECTIVE_TO_DATE] datetime    ,
 [RANK] DECIMAL
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
	 LIMIT_1_DISPLAY  ,                                  
	 LIMIT_2_DISPLAY ,                                  
	 DEDUCTIBLE_1_DISPLAY  ,                                  
	 DEDUCTIBLE_2_DISPLAY,                            
	 IS_LIMIT_APPLICABLE,                            
	 IS_DEDUCT_APPLICABLE,              
	 COVERAGE_TYPE,                      
	 SIGNATURE_OBTAINED,    
	 LIMIT_ID,                  
	 DEDUC_ID,    
	 EFFECTIVE_FROM_DATE,    
	 EFFECTIVE_TO_DATE ,
	 RANK                                                     
 )                                        
 SELECT                                       
	C.COV_ID,                                        
	C.COV_CODE,                          
	--C.COV_DES,                                      
	CASE C.COV_CODE WHEN 'EBIUE' THEN  '## ' + C.COV_DES ELSE  C.COV_DES END COV_DES,                                      
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
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                                                     
	' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                                                    
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                                     
	' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,                    
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +                                                     
	' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                                    
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                                     
	' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,         
	C.ISLIMITAPPLICABLE,                            
	C.ISDEDUCTAPPLICABLE  ,                        
	C.COVERAGE_TYPE,              
	AVC.SIGNATURE_OBTAINED,    
	AVC.LIMIT_ID,                  
	AVC.DEDUC_ID,    
	C.EFFECTIVE_FROM_DATE,    
	C.EFFECTIVE_TO_DATE ,
	C.RANK                                             
	FROM MNT_COVERAGE C                                      
	LEFT OUTER JOIN APP_WATERCRAFT_COVERAGE_INFO  AVC ON                                      
		C.COV_ID = AVC.COVERAGE_CODE_ID     
		AND CUSTOMER_ID = @CUSTOMER_ID     
		AND APP_ID = @APP_ID     
		AND APP_VERSION_ID = @APP_VERSION_ID     
		AND BOAT_ID = @WATERCRAFT_ID                                      
	WHERE    
		C.STATE_ID = @STATEID     
		AND C.LOB_ID = @LOBID     
		AND C.PURPOSE IN (1 , 3) --Purpose should either new business or both                                                               
		AND C.IS_ACTIVE='Y'
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
SELECT * FROM #COVERAGES ORDER BY RANK                      
                
                                     
--Table 1                                     
--Get Coverage ranges                                      
	SELECT  R.COV_ID,                                      
	R.LIMIT_DEDUC_ID ,                                      
	R.LIMIT_DEDUC_TYPE,                                      
	R.LIMIT_DEDUC_AMOUNT,                                      
	R.LIMIT_DEDUC_AMOUNT1, 
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                                           
	' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                                                          
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                                           
	' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,                                                           
	ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                 
	' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + case when R.LIMIT_DEDUC_AMOUNT1 is null then '' else '/' end  +                                                           
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
	R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,    
	R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                         
	FROM MNT_COVERAGE_RANGES R                                       
	INNER JOIN #COVERAGES C ON                                      
	C.COV_ID = R.COV_ID      
	
	WHERE R.IS_ACTIVE = 1                    
	AND R.LIMIT_DEDUC_ID  IN                   
	(                  
		SELECT LIMIT_DEDUC_ID                  
		FROM MNT_COVERAGE_RANGES R1                  
		WHERE             
		R1.LIMIT_DEDUC_TYPE = 'Limit'     
		AND R1.COV_ID =  C.COV_ID       
		AND  (@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND    
		ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') )            
		AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630'
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
		ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') )            
		AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630'
	)    
	OR ( C.DEDUC_ID = LIMIT_DEDUC_ID )            
	
	)                                             
	order by R.RANK                                                     
                            
                                 
--Table 2                                   
--Get the State for the application                                    
	SELECT CS.STATE_ID,              
	--CS.STATE_NAME,            
	upper(substring(CS.STATE_NAME,1,1)) + substring(lower(CS.STATE_NAME),2,len(CS.STATE_NAME)-1) as STATE_NAME,
	A.APP_LOB  ,         
	YEAR(CONVERT(VARCHAR(20),A.APP_EFFECTIVE_DATE,109)) as APP_EFFECTIVE_DATE,                
	A.APP_EFFECTIVE_DATE as APP_EFF_DATE ,
	A.APP_LOB  as LOB_ID 
	FROM APP_LIST A              
	INNER JOIN MNT_COUNTRY_STATE_LIST CS ON              
	A.STATE_ID = CS.STATE_ID              
	WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    
	AND A.APP_ID=@APP_ID     
	AND A.APP_VERSION_ID=@APP_VERSION_ID
	AND CS.COUNTRY_ID = 1              
                                    
                    
--Table 3                   
--Get the Watercraft info from APP_WATERCRAFT_INFO                    
SELECT INSURING_VALUE,        
       TYPE_OF_WATERCRAFT,      
       ISNULL(TYPE,'') AS TYPE,            
       LENGTH,          
       YEAR ,    
       BOAT_ID,    
       MAKE,    
       MODEL,  
       COV_TYPE_BASIS  
    
FROM APP_WATERCRAFT_INFO  INNER JOIN MNT_LOOKUP_VALUES       
ON APP_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID      
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                        
   APP_ID = @APP_ID AND                                        
   APP_VERSION_ID = @APP_VERSION_ID  AND                    
   BOAT_ID = @WATERCRAFT_ID         
                     
     
DROP TABLE #COVERAGES               
                         
   
                          
                    
                      
                             
                    
                  
                
              
            
          
        
        
        
        
        
      
    
    
    
  





















GO

