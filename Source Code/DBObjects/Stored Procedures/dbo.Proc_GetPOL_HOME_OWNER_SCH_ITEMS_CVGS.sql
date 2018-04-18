IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_HOME_OWNER_SCH_ITEMS_CVGS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_HOME_OWNER_SCH_ITEMS_CVGS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_GetPOL_HOME_OWNER_SCH_ITEMS_CVGS          
Created by      : Pradeep            
Date            : 8 Nov,2005              
Purpose         : Selects a single record from Policy Sch Items    
Revison History :              
Used In         : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------             
drop proc dbo.Proc_GetPOL_HOME_OWNER_SCH_ITEMS_CVGS
*/          
      
CREATE PROCEDURE Proc_GetPOL_HOME_OWNER_SCH_ITEMS_CVGS --1860,2,1,'IM'         
(          
  @CUSTOMER_ID int,          
  @POLICY_ID int,          
  @POLICY_VERSION_ID smallint,          
  @POL_TYPE Char(1)    
)          
          
As 
    
DECLARE @STATEID SmallInt          
DECLARE @LOBID NVarCHar(5)          
        
--N for New Business, R for renewal        
--DECLARE @POL_TYPE Char(1)
--SET @POL_TYPE = 'N'        
          
SELECT @STATEID = STATE_ID, @LOBID = POLICY_LOB          
FROM POL_CUSTOMER_POLICY_LIST          
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND   POLICY_VERSION_ID = @POLICY_VERSION_ID          
    
SET @LOBID = 1 
---For renewal        
DECLARE @POL_COVERAGE_COUNT int        
DECLARE @PREV_POL_VERSION_ID smallint 
        
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
 [LIMIT_TYPE] NChar(1),        
 [DEDUCTIBLE_TYPE] NChar(1),        
 [IS_MANDATORY] NChar(1) ,      
 [INCLUDED] decimal(18,2) ,
 [DETAILED_DESC] NVarChar(255 ),
 [RANK] decimal(7,2)
)          
        
IF ( @POL_TYPE = 'R')        
BEGIN        
	SELECT @PREV_POL_VERSION_ID = MAX(POLICY_VERSION_ID)        
	FROM POL_CUSTOMER_POLICY_LIST        
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND   POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID < @POLICY_VERSION_ID
	/*        
	IF ( @PREV_APP_VERSION_ID IS NULL ) BEGIN --RAISERROR('Previous application not found',16,1)        
	END        
	*/        
	 
	SELECT @POL_COVERAGE_COUNT = COUNT(*)    
	FROM POL_HOME_OWNER_SCH_ITEMS_CVGS    
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID        
     
IF ( @POL_COVERAGE_COUNT = 0 )    
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
		LIMIT_TYPE,        
		DEDUCTIBLE_TYPE,        
		IS_MANDATORY  ,      
		INCLUDED  ,
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
		C.LIMIT_TYPE,        
		C.DEDUCTIBLE_TYPE,        
		C.IS_MANDATORY  ,      
		C.INCLUDED ,
		NULL ,
		RANK     
	 FROM MNT_COVERAGE C        
	     
	 WHERE STATE_ID = @STATEID AND          
		LOB_ID = @LOBID AND    
		C.COVERAGE_TYPE = 'IM' AND --IS_ACTIVE = 'Y' AND         
		PURPOSE IN (2, 3) --Purpose should either new business or both          
		AND C.COV_ID NOT  IN        
		(        
		SELECT C.COV_ID          
		FROM MNT_COVERAGE C        
		INNER JOIN POL_HOME_OWNER_SCH_ITEMS_CVGS  AVC ON        
		C.COV_ID = AVC.CATEGORY AND---AVC.Item_ID AND     
		----AVC.CUSTOMER_ID = @CUSTOMER_ID AND       
		CUSTOMER_ID = @CUSTOMER_ID AND       
		POLICY_ID = @POLICY_ID AND          
		POLICY_VERSION_ID = @PREV_POL_VERSION_ID   ---AND  C.COVERAGE_TYPE = 'IM'  
	 )        
	 UNION        
	 SELECT         
		C.COV_ID,          
		C.COV_CODE,          
		C.COV_DES,        
		null,          
		null,          
		null,          
		--null,          
		null,          
		null,            
		null,          
		null,        
		C.LIMIT_TYPE,        
		C.DEDUCTIBLE_TYPE,        
		C.IS_MANDATORY , 
		C.INCLUDED ,
		AVC.DETAILED_DESC,
		RANK            
	 FROM MNT_COVERAGE C        
	 INNER JOIN POL_HOME_OWNER_SCH_ITEMS_CVGS  AVC ON        
		---C.COV_ID = AVC.ITEM_ID AND        
		---CUSTOMER_ID = @CUSTOMER_ID AND          
		---POLICY_ID = @POLICY_ID AND          
		---POLICY_VERSION_ID = @PREV_POL_VERSION_ID 
		C.COV_ID = AVC.CATEGORY AND            
		CUSTOMER_ID = @CUSTOMER_ID AND              
		POLICY_ID = @POLICY_ID AND              
		POLICY_VERSION_ID = @PREV_POL_VERSION_ID         
	 WHERE STATE_ID = @STATEID AND          
		LOB_ID = @LOBID AND      
		C.COVERAGE_TYPE = 'IM' AND --IS_ACTIVE = 'Y' AND         
	 	PURPOSE IN (2 , 3) --Purpose should either new business or both          
         
END    
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
		IS_SYSTEM_COVERAGE, --COVERAGE_ID,        
		LIMIT_TYPE,        
		DEDUCTIBLE_TYPE,        
		IS_MANDATORY  ,      
		INCLUDED  ,
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
		--NULL,--AVC.COVERAGE_ID,        
		C.LIMIT_TYPE,        
		C.DEDUCTIBLE_TYPE,        
		C.IS_MANDATORY  ,      
		C.INCLUDED ,
		NULL,
		C.RANK      
	 FROM MNT_COVERAGE C        
	     
	 WHERE STATE_ID = @STATEID AND          
		LOB_ID = @LOBID AND  
		C.COVERAGE_TYPE = 'IM' AND --IS_ACTIVE = 'Y' AND         
		PURPOSE IN (2, 3) --Purpose should either new business or both          
		AND C.COV_ID NOT  IN        
	 (        
	  SELECT C.COV_ID          
	  FROM MNT_COVERAGE C        
	  INNER JOIN POL_HOME_OWNER_SCH_ITEMS_CVGS  AVC ON        
		C.COV_ID = AVC.ITEM_ID 
	  WHERE      
		AVC.CUSTOMER_ID = @CUSTOMER_ID AND       
		POLICY_ID = @POLICY_ID AND          
		POLICY_VERSION_ID = @POLICY_VERSION_ID   AND  
		C.COVERAGE_TYPE = 'IM'  AND
		STATE_ID = @STATEID AND          
		LOB_ID = @LOBID
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
		--AVC.COVERAGE_ID,        
		C.LIMIT_TYPE,        
		C.DEDUCTIBLE_TYPE,        
		C.IS_MANDATORY ,      
		C.INCLUDED,
		AVC.DETAILED_DESC,
		C.RANK        
	 FROM MNT_COVERAGE C        
	 INNER JOIN POL_HOME_OWNER_SCH_ITEMS_CVGS  AVC ON        
	  C.COV_ID = AVC.ITEM_ID 
	 WHERE        
		CUSTOMER_ID = @CUSTOMER_ID AND    
		POLICY_ID = @POLICY_ID AND          
		POLICY_VERSION_ID = @POLICY_VERSION_ID    AND     
		STATE_ID = @STATEID AND          
		LOB_ID = @LOBID AND       
		C.COVERAGE_TYPE = 'IM'AND  --IS_ACTIVE = 'Y' AND         
		PURPOSE IN (2 , 3) --Purpose should either new business or both    
    
END    
    
END     
        
--For New Business        
IF ( @POL_TYPE = 'N')        
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
	--   COVERAGE_ID,        
	LIMIT_TYPE,        
	DEDUCTIBLE_TYPE,        
	IS_MANDATORY   ,      
	INCLUDED ,      
	DETAILED_DESC ,
	RANK      
 )          
 SELECT         
	C.COV_ID,          
	C.COV_CODE,          
	C.COV_DES,        
	null,          
	AVC.AMOUNT_OF_INSURANCE,          
	null,          
	null,          
	AVC.DEDUCTIBLE,          
	null,            
	null,          
	--   AVC.COVERAGE_ID,        
	C.LIMIT_TYPE,        
	C.DEDUCTIBLE_TYPE,        
	C.IS_MANDATORY ,      
	C.INCLUDED ,
	AVC.DETAILED_DESC,
	C.RANK        
 FROM MNT_COVERAGE C        
	LEFT OUTER JOIN POL_HOME_OWNER_SCH_ITEMS_CVGS  AVC ON        
	/*C.COV_ID = AVC.ITEM_ID AND        
	CUSTOMER_ID = @CUSTOMER_ID AND          
	POLICY_ID = @POLICY_ID AND          
	POLICY_VERSION_ID = @POLICY_VERSION_ID*/
	C.COV_ID = AVC.CATEGORY AND            
	CUSTOMER_ID = @CUSTOMER_ID AND              
	POLICY_ID = @POLICY_ID AND              
	POLICY_VERSION_ID = @POLICY_VERSION_ID    
         
 WHERE STATE_ID = @STATEID AND          
	LOB_ID = @LOBID AND      
	C.COVERAGE_TYPE = 'IM' AND --IS_ACTIVE = 'Y' AND        
	PURPOSE IN (1 , 3) --Purpose should either new business or both             
          
END        
        
--Table 1        
SELECT * FROM #COVERAGES        
        
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
INNER JOIN #COVERAGES C ON  C.COV_ID = R.COV_ID        
    
    
SELECT MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE FROM APP_LIST        
INNER JOIN MNT_LOOKUP_VALUES ON POLICY_TYPE=LOOKUP_UNIQUE_ID        
WHERE CUSTOMER_ID= @CUSTOMER_ID AND APP_ID=@POLICY_ID AND APP_VERSION_ID= @POLICY_VERSION_ID        
        
           
DROP TABLE #COVERAGES 


GO

