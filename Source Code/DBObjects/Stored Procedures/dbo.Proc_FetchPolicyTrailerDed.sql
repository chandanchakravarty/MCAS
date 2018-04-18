IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyTrailerDed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyTrailerDed]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- begin tran
-- DROP PROC dbo.Proc_FetchPolicyTrailerDed   
-- go
/*                
PROC NAME       : DBO.Proc_FetchPolicyTrailerDed                    
CREATED BY      : ASFA PRAVEEN                    
DATE            : 13/JUNE/2007                    
PURPOSE         : RETRIEVING DATA FROM MNT_COVERAGE_RANGES FOR TRAILER DEDUCTIBLE FIELD                     
REVISON HISTORY :                    
USED IN         : WOLVERINE             
              
Reviewed By : Anurag Verma      
Reviewed On : 22-06-2007      
*/                  
--DROP PROC dbo.Proc_FetchPolicyTrailerDed                    
CREATE PROC dbo.Proc_FetchPolicyTrailerDed 
(   
 @CUSTOMERID INT,                                                                                
 @POLICYID    INT,                                                                                
 @POLICYVERSIONID  INT,    
 @TRAILER_TYPE  INT              
)
AS                    
BEGIN            
                                                                
 DECLARE @STATEID SmallInt                                                                  
 DECLARE @LOBID NVarCHar(5)                                                                  
 DECLARE @APP_EFFECTIVE_DATE DateTime                      
 DECLARE @APP_INCEPTION_DATE DateTime                                                               
 DECLARE @POLICY_STATUS NVARCHAR(20)                                                   
                                                                   
 SELECT @STATEID = STATE_ID,    
  @POLICY_STATUS = upper(POLICY_STATUS),                                                                  
  @LOBID = 4,                  
  @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE  ,                  
  @APP_INCEPTION_DATE = APP_INCEPTION_DATE                                                                
 FROM POL_CUSTOMER_POLICY_LIST                                                              
 WHERE CUSTOMER_ID = @CUSTOMERID AND                                                                  
  POLICY_ID = @POLICYID AND                                                                  
  POLICY_VERSION_ID = @POLICYVERSIONID                                                                  
  /*
AS                    
          
DECLARE @POLEFFECTIVEDATE NVARCHAR(100)          
DECLARE @STATEID           NVARCHAR(100)          
DECLARE @EFFECTIVETODATE NVARCHAR(100)          
     
--Trailer screen has two types of deductibles :     
All Trailers except jet ski($100,$250,$500,$1000,$2500) a    
nd Jet ski trailers($250,$500) having different deductible options.    
-------------------------------------------------------------------    
LOOKUP_UNIQUE_ID  LOOKUP_VALUE_DESC LOOKUP_VALUE_CODE    
-------------------------------------------------------------------    
11761   Ski Jet Trailer  TRSK    
--    
SELECT  @POLEFFECTIVEDATE=APP_EFFECTIVE_DATE, @STATEID=STATE_ID  FROM POL_CUSTOMER_POLICY_LIST           
 WHERE CUSTOMER_ID=@CUSTOMERID  AND POLICY_ID=@POLICYID  AND POLICY_VERSION_ID=@POLICYVERSIONID          
          
          
SELECT TOP 1 @EFFECTIVETODATE = EFFECTIVE_TO_DATE FROM MNT_COVERAGE_RANGES          
 WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)          
          
IF (@STATEID=14)          
BEGIN
IF (@TRAILER_TYPE = 11761)    
  BEGIN    
 SELECT LIMIT_DEDUC_ID,LIMIT_DEDUC_AMOUNT ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
 FROM MNT_COVERAGE_RANGES         
 WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)    
 AND LIMIT_DEDUC_ID IN (1433,1436)   
  END    
ELSE    
  BEGIN         
 IF (DATEDIFF(DAY,@POLEFFECTIVEDATE,@EFFECTIVETODATE)>0)          
  BEGIN          
   SELECT  LIMIT_DEDUC_ID,CAST(ISNULL(LIMIT_DEDUC_AMOUNT, 0) AS VARCHAR)+          
   CAST(ISNULL(LIMIT_DEDUC_AMOUNT_TEXT, '') AS VARCHAR) AS LIMIT_DEDUC_AMOUNT  ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
         
   FROM MNT_COVERAGE_RANGES           
   WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)          
   AND EFFECTIVE_TO_DATE > @POLEFFECTIVEDATE   AND LIMIT_DEDUC_ID NOT IN (1433,1436)        
   ORDER BY 1          
  END          
 ELSE          
  BEGIN          
   SELECT LIMIT_DEDUC_ID,CAST(ISNULL(LIMIT_DEDUC_AMOUNT, 0) AS VARCHAR)+          
   CAST(ISNULL(LIMIT_DEDUC_AMOUNT_TEXT, '') AS VARCHAR) AS LIMIT_DEDUC_AMOUNT  ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
         
   FROM MNT_COVERAGE_RANGES           
   WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)          
   AND LIMIT_DEDUC_ID NOT IN (1433,1436)
   ORDER BY 1          
  END        
END              
END  
IF (@STATEID=22)          
BEGIN
IF (@TRAILER_TYPE = 11761)    
  BEGIN    
 SELECT LIMIT_DEDUC_ID,LIMIT_DEDUC_AMOUNT ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
 FROM MNT_COVERAGE_RANGES         
 WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)    
 AND LIMIT_DEDUC_ID IN (1434,1437)   
  END    
ELSE    
  BEGIN         
 IF (DATEDIFF(DAY,@POLEFFECTIVEDATE,@EFFECTIVETODATE)>0)          
  BEGIN          
   SELECT  LIMIT_DEDUC_ID,CAST(ISNULL(LIMIT_DEDUC_AMOUNT, 0) AS VARCHAR)+          
   CAST(ISNULL(LIMIT_DEDUC_AMOUNT_TEXT, '') AS VARCHAR) AS LIMIT_DEDUC_AMOUNT  ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
         
   FROM MNT_COVERAGE_RANGES           
   WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)          
   AND EFFECTIVE_TO_DATE > @POLEFFECTIVEDATE   AND LIMIT_DEDUC_ID NOT IN (1434,1437)        
   ORDER BY 1          
  END          
 ELSE          
  BEGIN          
   SELECT LIMIT_DEDUC_ID,CAST(ISNULL(LIMIT_DEDUC_AMOUNT, 0) AS VARCHAR)+          
   CAST(ISNULL(LIMIT_DEDUC_AMOUNT_TEXT, '') AS VARCHAR) AS LIMIT_DEDUC_AMOUNT  ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
         
   FROM MNT_COVERAGE_RANGES           
   WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)          
   AND LIMIT_DEDUC_ID NOT IN (1434,1437)
   ORDER BY 1          
  END        
END              
END  
IF (@STATEID=49)          
BEGIN
IF (@TRAILER_TYPE = 11761)    
  BEGIN    
 SELECT LIMIT_DEDUC_ID,LIMIT_DEDUC_AMOUNT ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
 FROM MNT_COVERAGE_RANGES         
 WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)    
 AND LIMIT_DEDUC_ID IN (1435,1438)   
  END    
ELSE    
  BEGIN         
 IF (DATEDIFF(DAY,@POLEFFECTIVEDATE,@EFFECTIVETODATE)>0)          
  BEGIN          
   SELECT  LIMIT_DEDUC_ID,CAST(ISNULL(LIMIT_DEDUC_AMOUNT, 0) AS VARCHAR)+          
   CAST(ISNULL(LIMIT_DEDUC_AMOUNT_TEXT, '') AS VARCHAR) AS LIMIT_DEDUC_AMOUNT  ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
         
   FROM MNT_COVERAGE_RANGES           
   WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)          
   AND EFFECTIVE_TO_DATE > @POLEFFECTIVEDATE   AND LIMIT_DEDUC_ID NOT IN (1435,1438)        
   ORDER BY 1          
  END          
 ELSE          
  BEGIN          
   SELECT LIMIT_DEDUC_ID,CAST(ISNULL(LIMIT_DEDUC_AMOUNT, 0) AS VARCHAR)+          
   CAST(ISNULL(LIMIT_DEDUC_AMOUNT_TEXT, '') AS VARCHAR) AS LIMIT_DEDUC_AMOUNT  ,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE  
         
   FROM MNT_COVERAGE_RANGES           
   WHERE COV_ID=(SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)          
   AND LIMIT_DEDUC_ID NOT IN (1435,1438)
   ORDER BY 1          
  END        
END              
END  

SELECT @POLEFFECTIVEDATE  APP_EFFECTIVE_DATE  

  */                                                               
 ---For renewal    
 DECLARE @POL_COVERAGE_COUNT int                                                                
 DECLARE @PREV_POL_VERSION_ID smallint                                  
                  
 DECLARE @IDENT_COL INT                  
 SET  @IDENT_COL = 1                  
           
  --if(@TRAILER_TYPE = 11761) --jet ski trailer
      
  --else
    
  
--Holds effective dates of all versions of current policy                  

 DECLARE @TEMP_APP_LIST TABLE                  
 (                  
 IDENT_COL INT IDENTITY (1,1),                  
 APP_EFFECTIVE_DATE DATETIME,                  
 POLICY_VERSION_ID INT                  
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
WHERE CUSTOMER_ID = @CUSTOMERID     
AND POLICY_ID = @POLICYID                   


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
	@CURRENT_VERSION_ID = POLiCY_VERSION_ID                  
	FROM @TEMP_APP_LIST                  
	WHERE IDENT_COL = @IDENT_COL                  
	     
	/*                  
	Insert into temp table the list of all coverages which where available in all versions                  
	and which has date range between each of the effective date.                  
	Also get coverages which were applicable during any endorsement process                  
	*/                  
	--Coverage ranges                  
	INSERT INTO @TEMP_COV_RANGES                    
	SELECT LIMIT_DEDUC_ID FROM MNT_COVERAGE_RANGES R                  
	INNER JOIN MNT_COVERAGE C ON R.COV_ID = C.COV_ID                  
	WHERE @APP_EFF_DATE BETWEEN R.EFFECTIVE_FROM_DATE AND ISNULL(R.EFFECTIVE_TO_DATE,'3000-01-01 16:50:49.333')    
	AND ISNULL(R.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFF_DATE                             
	AND C.LOB_ID = @LOBID     
	AND C.STATE_ID = @STATEID     
	AND R.LIMIT_DEDUC_ID NOT IN (SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES)                  
	and R.COV_ID in (SELECT COV_ID FROM MNT_COVERAGE WHERE COV_CODE='BDEDUC' AND STATE_ID=@STATEID)  

               
 	SET @IDENT_COL = @IDENT_COL + 1                  
END  --- End While Loop                

--Insert coverages which were opted in any previous version though not applicable to that version  
INSERT INTO @TEMP_COV_RANGES                    
(  
LIMIT_DEDUC_ID  
)  
SELECT DISTINCT TRAILER_DED_ID  FROM POL_WATERCRAFT_TRAILER_INFO  
WHERE  
CUSTOMER_ID = @CUSTOMERID     
AND POLICY_ID = @POLICYID    
AND  TRAILER_DED_ID  IS NOT NULL  

 --Table 0                                                                
  --Get the State for the Policy                                   
EXEC Proc_GetPolicyState @CUSTOMERID,@POLICYID,@POLICYVERSIONID                         
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
	
	R.IS_DEFAULT,                                                           
	R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,      
	R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                                                  
	FROM MNT_COVERAGE_RANGES R 
	Inner join  MNT_COVERAGE C on C.Cov_id=R.cov_id and C.COV_CODE='BDEDUC'   and C.STATE_ID=@STATEID and C.LOB_ID=@LOBID                      
	WHERE R.IS_ACTIVE = 1                        
	AND R.LIMIT_DEDUC_ID  IN                       
	(                  
	SELECT LIMIT_DEDUC_ID                    
	FROM MNT_COVERAGE_RANGES R1                    
	WHERE R1.LIMIT_DEDUC_TYPE = 'Deduct' AND                    
	
	( ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE )   
	--  AND R.LIMIT_DEDUC_ID  IN                       
	--  (                  
	--   SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES    --Limits Available in previous version            
	--  )                  
	)                  
	ORDER BY R.RANK                    
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
 R.IS_DEFAULT,    
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,      
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                                                  
 FROM MNT_COVERAGE_RANGES R                                                                 
                               
 WHERE R.IS_ACTIVE = 1                        
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE    
 AND R.LIMIT_DEDUC_ID  IN                       
 (                  
  SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES                
 ) 
                 
 ORDER BY R.RANK                                  
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
	
	R.IS_DEFAULT,    
	R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,      
	R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                                                  
	FROM MNT_COVERAGE_RANGES R                                                                 
	WHERE R.IS_ACTIVE = 1     
	AND ISNULL(R.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE    
	AND R.LIMIT_DEDUC_ID  IN                       
	(                  
	SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES    --Limits Available in previous version            
	)       
	OR R.LIMIT_DEDUC_ID  IN      
	(    
	SELECT  TRAILER_DED_ID FROM POL_WATERCRAFT_TRAILER_INFO    
	WHERE     
	CUSTOMER_ID= @CUSTOMERID     
	AND POLICY_ID=@POLICYID     
	AND POLICY_VERSION_ID IN     
	(    
	SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS     
	WHERE     
	CUSTOMER_ID= @CUSTOMERID     
	AND POLICY_ID=@POLICYID     
	AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit    
	AND PROCESS_STATUS ='COMPLETE'    
	AND NEW_POLICY_VERSION_ID IN     
	(    
	SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS    
	WHERE    
	CUSTOMER_ID= @CUSTOMERID    
	AND POLICY_ID=@POLICYID    
	AND PROCESS_ID IN (25,18)    
	AND PROCESS_STATUS ='COMPLETE'    
	)    
	
	
	)    
	UNION    
	SELECT  TRAILER_DED_ID FROM POL_WATERCRAFT_TRAILER_INFO    
	WHERE     
	CUSTOMER_ID= @CUSTOMERID     
	AND POLICY_ID=@POLICYID     
	AND POLICY_VERSION_ID IN     
	(    
	SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS     
	WHERE     
	CUSTOMER_ID= @CUSTOMERID     
	AND POLICY_ID=@POLICYID     
	AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit    
	AND PROCESS_STATUS ='COMPLETE'    
	AND NEW_POLICY_VERSION_ID IN     
	(    
	SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS    
	WHERE    
	CUSTOMER_ID= @CUSTOMERID    
	AND POLICY_ID=@POLICYID    
	AND PROCESS_ID IN (25,18)    
	AND PROCESS_STATUS ='COMPLETE'    
	)    

	)    
	
	
	
	)   
	
	ORDER BY R.RANK                                  
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
	R.IS_DEFAULT,    
	R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,      
	R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                                                  
	FROM MNT_COVERAGE_RANGES R                                                                 
	Inner join  MNT_COVERAGE C 
	on C.Cov_id=R.cov_id 
	and C.COV_CODE='BDEDUC'   
	and C.STATE_ID=@STATEID and C.LOB_ID=@LOBID                      
	WHERE R.IS_ACTIVE = 1         
	AND R.LIMIT_DEDUC_ID  IN                       
	(                  
		SELECT LIMIT_DEDUC_ID                    
		FROM MNT_COVERAGE_RANGES R1                    
		WHERE  R1.LIMIT_DEDUC_TYPE = 'Deduct'    
		AND              
		(               
		@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND    
		ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')          
		AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')    
		)              
		
		UNION                    
	
		SELECT LIMIT_DEDUC_ID                    
		FROM MNT_COVERAGE_RANGES R1                    
		WHERE     
		R1.LIMIT_DEDUC_TYPE = 'Deduct'    
		AND              
		(               
		@APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND    
		ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                
		AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')    
		) 
		UNION 
		SELECT TRAILER_DED_ID FROM POL_WATERCRAFT_TRAILER_INFO 
		WHERE CUSTOMER_ID = @CUSTOMERID 
		AND POLICY_ID = @POLICYID 
		AND POLICY_VERSION_ID = @POLICYVERSIONID	

	)                  
	ORDER BY R.RANK                                  
END    
                          
         

        
              
END                                                                  
                                                
-- go        
--       
--     
-- exec Proc_FetchPolicyTrailerDed 2088,1,1,11760 
-- exec Proc_FetchPolicyWaterCraftTrailer 2088,1,1, 1
-- rollback tran
-- 
-- 
-- 
-- 
-- 
-- 
-- 


GO

