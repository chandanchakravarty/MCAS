IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_HOME_OWNER_SCH_ITEMS_CVGS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_HOME_OWNER_SCH_ITEMS_CVGS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- DROP PROC dbo.Proc_GetAPP_HOME_OWNER_SCH_ITEMS_CVGS 1860,2,1,0,'N','IM'             
CREATE PROCEDURE dbo.Proc_GetAPP_HOME_OWNER_SCH_ITEMS_CVGS              
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
            
--N for New Business, R for renewal            
--DECLARE @APP_TYPE Char(1) 
--SET @APP_TYPE = 'N'            
              
SELECT @STATEID = STATE_ID,@LOBID = APP_LOB              
FROM APP_LIST              
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND  APP_VERSION_ID = @APP_VERSION_ID              
        
--This SP is being used for HOME and Rental Dwelling LOB.         
--For rental, Scehduled Items are being fetched the same as for Home.        
SET @LOBID = 1  
---For renewal            
DECLARE @APP_COVERAGE_COUNT int            
DECLARE @PREV_APP_VERSION_ID smallint              
CREATE TABLE #COVERAGES              
(              
 [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,              
 [COV_ID] [int] NOT NULL ,              
 [COV_CODE] VarChar(10) NOT NULL ,            
 [COV_DESC] VarChar(100),                
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
 [INCLUDED] decimal(18,2),    
 [DETAILED_DESC] NVarChar(255),
 [RANK] decimal(7,2),              
              
)              
            
IF ( @APP_TYPE = 'R')            
BEGIN            
	SELECT @PREV_APP_VERSION_ID = MAX(APP_VERSION_ID)            
	FROM APP_LIST            
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND APP_VERSION_ID < @APP_VERSION_ID            
	     
	 /*            
	 IF ( @PREV_APP_VERSION_ID IS NULL )            
	 BEGIN            
	  --RAISERROR('Previous application not found',16,1)            
	 END            
	 */            
             
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
	   IS_MANDATORY  ,          
	   INCLUDED ,    
	   DETAILED_DESC,
	   RANK           
	               
	 )   
           
	 SELECT             
		C.COV_ID,              
		C.COV_CODE,              
		C.COV_DES,            
		NULL , --AVC.LIMIT_OVERRIDE,              
		NULL , --AVC.LIMIT_1,              
		NULL , --AVC.LIMIT_2,              
		NULL, --AVC.DEDUCT_OVERRIDE,              
		NULL,--AVC.DEDUCTIBLE_1,              
		NULL,--AVC.DEDUCTIBLE_2,                
		NULL,--AVC.IS_SYSTEM_COVERAGE,              
		NULL,--AVC.COVERAGE_ID,            
		C.LIMIT_TYPE,            
		C.DEDUCTIBLE_TYPE,            
		C.IS_MANDATORY  ,   
		C.INCLUDED,    
		NULL,
		C.RANK           
	 FROM MNT_COVERAGE C            
	/*            
	LEFT OUTER JOIN APP_VEHICLE_COVERAGES  AVC ON            
	C.COV_ID = AVC.COVERAGE_CODE_ID AND            
	CUSTOMER_ID = @CUSTOMER_ID AND     
	APP_ID = @APP_ID AND              
	APP_VERSION_ID = @APP_VERSION_ID AND              
	VEHICLE_ID = @VEHICLE_ID            
	*/            
	WHERE STATE_ID = @STATEID AND              
	LOB_ID = @LOBID AND              
	--IS_ACTIVE = 'Y' AND             
	COVERAGE_TYPE=@COVERAGE_TYPE AND          
	PURPOSE IN (2, 3) --Purpose should either new business or both              
	AND C.COV_ID NOT  IN            
	(            
	SELECT C.COV_ID              
	FROM MNT_COVERAGE C            
	INNER JOIN APP_HOME_OWNER_SCH_ITEMS_CVGS  AVC ON            
	C.COV_ID = AVC.CATEGORY AND            
	CUSTOMER_ID = @CUSTOMER_ID AND              
	APP_ID = @APP_ID AND              
	APP_VERSION_ID = @PREV_APP_VERSION_ID           
	)  
          
 	UNION            

	SELECT             
		C.COV_ID,              
		C.COV_CODE,              
		C.COV_DES,            
		null,              
		null,              
		null,              
		null,              
		null,              
		null,                
		null,              
		NULL,            
		C.LIMIT_TYPE,            
		C.DEDUCTIBLE_TYPE,            
		C.IS_MANDATORY ,          
		C.INCLUDED   ,    
		AVC.DETAILED_DESC,
		C.RANK         
	FROM MNT_COVERAGE C            
	INNER JOIN APP_HOME_OWNER_SCH_ITEMS_CVGS  AVC ON            
	C.COV_ID = AVC.CATEGORY AND            
	CUSTOMER_ID = @CUSTOMER_ID AND              
	APP_ID = @APP_ID AND              
	APP_VERSION_ID = @PREV_APP_VERSION_ID           
	    
	WHERE STATE_ID = @STATEID AND              
	LOB_ID = @LOBID AND              
	--AVC.IS_ACTIVE = 'Y' AND             
	COVERAGE_TYPE=@COVERAGE_TYPE AND           
	PURPOSE IN (2 , 3) --Purpose should either new business or both              
             
             
END            
            
--For New Business            
IF ( @APP_TYPE = 'N')            
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
	   IS_MANDATORY   ,          
	   INCLUDED ,          
	   DETAILED_DESC,
	   RANK             
	 )              
	 SELECT             
		C.COV_ID,              
		C.COV_CODE,              
		C.COV_DES,            
		null,              
		case AMOUNT_OF_INSURANCE when -1.00 then 0 else AMOUNT_OF_INSURANCE end as AMOUNT_OF_INSURANCE,              
		null,              
		null,              
		AVC.DEDUCTIBLE,              
		null,                
		null,              
		AVC.ITEM_ID,            
		C.LIMIT_TYPE,            
		C.DEDUCTIBLE_TYPE,            
		C.IS_MANDATORY ,          
		C.INCLUDED   ,    
		AVC.DETAILED_DESC,
		C.RANK         
	 FROM MNT_COVERAGE C            
		LEFT OUTER JOIN APP_HOME_OWNER_SCH_ITEMS_CVGS  AVC ON            
		C.COV_ID = AVC.CATEGORY AND            
		CUSTOMER_ID = @CUSTOMER_ID AND              
		APP_ID = @APP_ID AND              
		APP_VERSION_ID = @APP_VERSION_ID          
	             
	 WHERE STATE_ID = @STATEID AND              
		LOB_ID = @LOBID AND              
		--AVC.IS_ACTIVE = 'Y' AND            
		COVERAGE_TYPE=@COVERAGE_TYPE AND            
		PURPOSE IN (1 , 3) --Purpose should either new business or both                 
	              
END            
            
--Table 1            
SELECT * FROM #COVERAGES order by RANK 
            
--Table 2            
--Get Coverage ranges            
SELECT  R.COV_ID,            
 R.LIMIT_DEDUC_ID,          
R.LIMIT_DEDUC_TYPE,            
 R.LIMIT_DEDUC_AMOUNT,            
 R.LIMIT_DEDUC_AMOUNT1,            
 Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT) + '/' +             
 Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1) as SplitAmount,            
 R.IS_DEFAULT            
FROM MNT_COVERAGE_RANGES R             
INNER JOIN #COVERAGES C ON            
 C.COV_ID = R.COV_ID  
WHERE R.IS_ACTIVE = 1          
            
SELECT MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,APP_LIST.POLICY_TYPE, APP_LIST.STATE_ID FROM APP_LIST            
INNER JOIN MNT_LOOKUP_VALUES ON POLICY_TYPE=LOOKUP_UNIQUE_ID            
WHERE CUSTOMER_ID= @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID= @APP_VERSION_ID            
            
            
SELECT DWELLING_LIMIT,OTHER_STRU_LIMIT,PERSONAL_PROP_LIMIT,LOSS_OF_USE FROM APP_DWELLING_COVERAGE            
WHERE CUSTOMER_ID= @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID= @APP_VERSION_ID AND DWELLING_ID=@DWELLING_ID            
            
              
DROP TABLE #COVERAGES          










GO

