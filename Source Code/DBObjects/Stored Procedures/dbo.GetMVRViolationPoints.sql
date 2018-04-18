IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMVRViolationPoints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMVRViolationPoints]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                          
Proc Name       : dbo.GetMVRViolationPoints                                                                                          
Created by      : Sumit Chhabra                                                        
Date            : 09/08/2006                                                                                          
Purpose       : To calculate MVR points for a driver at policy level                                                        
Revison History :                
Modified by  :Pravesh k chandel      
date   :27 aug 2008      
purpose   : modify logic to fetch MVR points - change occurence date to conviction date/MVr date and points of wolverine violations                                                                                
Used In         : Wolverine                                                                                          
------------------------------------------------------------                                                                                          
Date     Review By          Comments                    
                  
Note: Whatever changes you make here should be changed in rules  sps.                  
                                                                                       
------   ------------       -------------------------            
DROP PROC [dbo].[GetMVRViolationPoints]                                                                                          
*/                
                                                                                       
create PROC [dbo].[GetMVRViolationPoints]                                                                                          
(                                                                                          
@CUSTOMER_ID int,                                                        
@APP_ID int,                                                        
@APP_VERSION_ID smallint,                                                        
@DRIVER_ID smallint,                                                        
@ACCIDENT_NUM_YEAR int,                              
@VIOLATION_NUM_YEAR int,      
@VIOLATION_NONCHARGE_NUM_YEARS int,      
@ACCIDENT_PAID_AMOUNT INT =null                                                      
)                                                                                          
AS                                                                                          
BEGIN                                                                                   
                                       
 DECLARE @APP_LOB_ID INT                                              
 DECLARE @SUM_MVR_POINTS INT                                                        
 DECLARE @SUM_MVR_POINTS_MAJOR INT                                                        
 DECLARE @APP_EFFECTIVE_DATE DATETIME            
 DECLARE @PRIOR_TWO_YEARS_DATE DATETIME       
 DECLARE @PRIOR_THREE_YEARS_DATE DATETIME           
 DECLARE @PRIOR_FIVE_YEARS_DATE DATETIME        
 DECLARE @WOLVERINECONTINOUSLYINSURED SMALLINT                                           
 DECLARE @BOAT_LOB int                                              
 DECLARE @ACCIDENT_POINTS INT                                
 DECLARE @COUNT_ACCIDENTS INT                              
 DECLARE @FIRST_ACCIDENTS CHAR(1)         
 DECLARE @MINOR_VIOLATION_REFER INT                                            
 SET @BOAT_LOB = 4                                              
 SET @ACCIDENT_POINTS = 0                                                       
 SET @SUM_MVR_POINTS = 0                                                        
 SET @SUM_MVR_POINTS_MAJOR = 0                 
 set @FIRST_ACCIDENTS ='N'      
   -- Get App Effective Date                          
   SELECT @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,@APP_LOB_ID=CAST (APP_LOB AS INT) FROM APP_LIST (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID        

   SELECT  @WOLVERINECONTINOUSLYINSURED =ISNULL(YEARS_INSU_WOL,0) FROM APP_AUTO_GEN_INFO   
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND   APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                    
        
      
---------------------------------------------------------------------------------------------------      
--    three, two ,five years previous date(start)      
----------------------------------------------------------------------------------------------------        
DECLARE @ACCIDENTEFFECTIVEDATE DATETIME      
DECLARE @ACCIDENTEFFECTIVEDAYS INT      
DECLARE @MINORVIOLATIONEFFECTIVEDATE DATETIME      
DECLARE @MINORVIOLATIONEFFECTIVEDAYS INT      
DECLARE @MAJORVIOLATIONEFFECTIVEDATE DATETIME      
DECLARE @MAJORVIOLATIONEFFECTIVEDAYS INT      
DECLARE @MVREFFECTIVEDATE DATETIME      
DECLARE @MVREFFECTIVEDAYS INT      
SET @ACCIDENTEFFECTIVEDAYS=0      
SET @MINORVIOLATIONEFFECTIVEDAYS=0      
SET @MAJORVIOLATIONEFFECTIVEDAYS=0      
SET @MVREFFECTIVEDAYS=0      
SET @ACCIDENTEFFECTIVEDATE = DATEADD(YEAR,-@ACCIDENT_NUM_YEAR,@APP_EFFECTIVE_DATE)      
SET @ACCIDENTEFFECTIVEDAYS = DATEDIFF(DAY,@ACCIDENTEFFECTIVEDATE,@APP_EFFECTIVE_DATE)      
SET @MINORVIOLATIONEFFECTIVEDATE = DATEADD(YEAR,-@VIOLATION_NUM_YEAR,@APP_EFFECTIVE_DATE)      
SET @MINORVIOLATIONEFFECTIVEDAYS = DATEDIFF(DAY,@MINORVIOLATIONEFFECTIVEDATE,@APP_EFFECTIVE_DATE)      
SET @MAJORVIOLATIONEFFECTIVEDATE = DATEADD(YEAR,-5,@APP_EFFECTIVE_DATE)      
SET @MAJORVIOLATIONEFFECTIVEDAYS = DATEDIFF(DAY,@MAJORVIOLATIONEFFECTIVEDATE,@APP_EFFECTIVE_DATE)      
SET @MVREFFECTIVEDATE = DATEADD(YEAR,-@VIOLATION_NONCHARGE_NUM_YEARS,@APP_EFFECTIVE_DATE)      
SET @MVREFFECTIVEDAYS = DATEDIFF(DAY,@MVREFFECTIVEDATE,@APP_EFFECTIVE_DATE)      
---------------------------------------------------------------------------------------------------      
--    three, two ,five years previous date(END)      
----------------------------------------------------------------------------------------------------        
                                                      
   -- Count Accidents                                         
 SELECT @COUNT_ACCIDENTS=COUNT(CUSTOMER_ID) FROM FETCH_ACCIDENT WITH (NOLOCK)                                                   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND (POLICY_ID IS NULL) AND (POLICY_VERSION_ID IS NULL) AND LOB=@APP_LOB_ID       
 AND                                          
 (      
  --(ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=@ACCIDENT_NUM_YEAR       
  --(ISNULL(DATEDIFF(DAY,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENT_NUM_YEAR*365.25       
  (
	ISNULL(DATEDIFF(DAY,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENTEFFECTIVEDAYS      
	AND                                                      
     	--(ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0      
  	(ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))>=0      
  )       
 AND (      
	  (ISNULL(DRIVER_NAME,'') = '' 
	OR (
		dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR) 
		AND dbo.piece(DRIVER_NAME,'^',2)= CAST(@APP_ID AS VARCHAR) 
		AND CAST(dbo.piece(ISNULL(DRIVER_NAME,''),'^',3) AS INT)<=CAST(@APP_VERSION_ID AS INT) 
		AND  dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)      
		AND   dbo.piece(DRIVER_NAME,'^',5)= 'APP'
		)
     OR (
		ISNULL(DRIVER_NAME,'')= dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' + CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP')                        
		)
	)      
--DRIVER_NAME=(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' + CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP'))                        
   AND 
   (CHARGEABLE=11924 or 
    (CHARGEABLE =0 AND  ISNULL(AT_FAULT,0) = CASE LOB WHEN 4 THEN ISNULL(AT_FAULT,0) ELSE 10963 END ))      
  )       
 AND  ISNULL(AT_FAULT,0)=CASE LOB WHEN 4 THEN ISNULL(AT_FAULT,0) ELSE 10963 END       
 --AND ISNULL(AMOUNT_PAID,0) > CASE LOB WHEN 4 THEN 1000.00 ELSE 999.99 END -- iTRACK 4716     
AND ISNULL(AMOUNT_PAID,0) > CASE LOB WHEN 4 THEN 1000.00 WHEN 3 THEN 499.99  ELSE 999.99 END -- iTRACK 5081     

       
                
-----added by pravesh      
IF (@COUNT_ACCIDENTS=1      
 AND      
   EXISTS(      
     SELECT CUSTOMER_ID FROM FETCH_ACCIDENT WITH (NOLOCK)                                                   
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
    (POLICY_ID IS NULL) AND (POLICY_VERSION_ID IS NULL) AND LOB=@APP_LOB_ID       
   AND                                          
     (      
   --(ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=@ACCIDENT_NUM_YEAR       
   --(ISNULL(DATEDIFF(DAY,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENT_NUM_YEAR*365.25       
   (ISNULL(DATEDIFF(DAY,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENTEFFECTIVEDAYS      
   AND                                                      
   --(ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0      
   (ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))>=0      
   )       
  AND                                    
    ((ISNULL(DRIVER_NAME,'') = '' OR       
--DRIVER_NAME=(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' + CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP'))                        
(dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR) and dbo.piece(DRIVER_NAME,'^',2)= CAST(@APP_ID AS VARCHAR) and       
CAST(dbo.piece(ISNULL(DRIVER_NAME,''),'^',3) AS INT)<=CAST(@APP_VERSION_ID AS INT) AND  dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)      
AND   dbo.piece(DRIVER_NAME,'^',5)= 'APP')
OR (
		ISNULL(DRIVER_NAME,'')= dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' + CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP')                        
		))         
  AND (CHARGEABLE=11924 or (CHARGEABLE=0 AND  AT_FAULT=10963) ))      
     AND  AT_FAULT=10963           
   AND PAID_LOSS < 2000      
  )      
   )      
 SET @FIRST_ACCIDENTS='Y'
    
-- if more than 1 loss exists below $2000 then no excuse      
IF ((SELECT COUNT(*) FROM FETCH_LOSS WITH (NOLOCK)                                                   
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                        
    (POLICY_ID IS NULL) AND (POLICY_VERSION_ID IS NULL) AND LOB=@APP_LOB_ID       
   AND                                          
     (      
   --(ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=@ACCIDENT_NUM_YEAR       
   --(ISNULL(DATEDIFF(DAY,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENT_NUM_YEAR*365.25       
   (ISNULL(DATEDIFF(DAY,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENTEFFECTIVEDAYS      
   AND                                                      
   --(ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0      
   (ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))>=0      
   )       
  AND                                    
    ((ISNULL(DRIVER_NAME,'') = '' OR       
--DRIVER_NAME=(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' + CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP'))                        
(dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR) and dbo.piece(DRIVER_NAME,'^',2)= CAST(@APP_ID AS VARCHAR) and       
CAST(dbo.piece(ISNULL(DRIVER_NAME,''),'^',3) AS INT)<=CAST(@APP_VERSION_ID AS INT) AND  dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)      
AND   dbo.piece(DRIVER_NAME,'^',5)= 'APP')
	OR (
		ISNULL(DRIVER_NAME,'')= dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' + CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP')                        
		))      
     AND (CHARGEABLE=11924 or (CHARGEABLE=0 AND  AT_FAULT=10963) ))      
     AND  AT_FAULT=10963 AND PAID_LOSS >0.00) >1 )      
 BEGIN      
  SET @FIRST_ACCIDENTS='N'      
 END       
      
---end here      
                                             
/* COMMENTED BY Pravesh      
-- IF   FIRST AT FAULT ACCIDENT THEN FORGIVE IT IF PAID AMOUNT IS LESS THAN $2000        
--SELECT * FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=1430 AND LOSS_ID=4      
DECLARE @PRIRLOSSCUSTOMER INT      
-------------------------------------------------------------------------------------      
DECLARE @PRIRLOSSCUSTOMERMORETHANTWO INT      
SELECT                       
@PRIRLOSSCUSTOMERMORETHANTWO=COUNT(CUSTOMER_ID)      
FROM APP_PRIOR_LOSS_INFO  WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID and  AMOUNT_PAID >= @ACCIDENT_PAID_AMOUNT  AND AT_FAULT= 10963       
AND CHARGEABLE = 11924  AND LOB=2   AND (datediff(day,OCCURENCE_DATE,@APP_EFFECTIVE_DATE) < 1095)       
AND  ((DRIVER_NAME = '' OR DRIVER_NAME=(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' +                                    
           CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP')))                      
IF(@PRIRLOSSCUSTOMERMORETHANTWO >0)      
 BEGIN      
 SET @PRIRLOSSCUSTOMERMORETHANTWO=1      
 END      
ELSE      
BEGIN      
SELECT                
 @PRIRLOSSCUSTOMER=COUNT(CUSTOMER_ID)      
 FROM APP_PRIOR_LOSS_INFO  WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID and  AMOUNT_PAID <= @ACCIDENT_PAID_AMOUNT  AND AT_FAULT= 10963        
   AND CHARGEABLE = 11924  AND LOB=2   AND (datediff(day,OCCURENCE_DATE,@APP_EFFECTIVE_DATE) < 1095)       
 AND  ((DRIVER_NAME = '' OR DRIVER_NAME=(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' +                                    
      CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP')))                      
      
 IF(@PRIRLOSSCUSTOMER < 2  AND @WOLVERINECONTINOUSLYINSURED >3)      
*/      
IF(@FIRST_ACCIDENTS='Y'  AND @WOLVERINECONTINOUSLYINSURED >=3 AND @APP_LOB_ID='2')      
    BEGIN      
     --SET @PRIRLOSSCUSTOMER =0;      
     SET @COUNT_ACCIDENTS =0 -- @COUNT_ACCIDENTS - @PRIRLOSSCUSTOMER      
    END      
--END      
-------------------------------------------------------------------------------------            
    -- Set Accident Points                          
  IF (@COUNT_ACCIDENTS>0)                 
   SET @ACCIDENT_POINTS = (ISNULL(@COUNT_ACCIDENTS,0) * 4 ) - 1         
         
    -- Set Two Years Prior App Effective Date            
 SET @PRIOR_TWO_YEARS_DATE =  DATEADD(DD,-@VIOLATION_NUM_YEAR * 365.25,@APP_EFFECTIVE_DATE)       
 SET @PRIOR_THREE_YEARS_DATE = DATEADD(DD,-@VIOLATION_NONCHARGE_NUM_YEARS * 365.25,@APP_EFFECTIVE_DATE)             
 SET @PRIOR_FIVE_YEARS_DATE =  DATEADD(DD,-5 * 365.5,@APP_EFFECTIVE_DATE)                
                            
 -- For WATERCRAFT            
 IF(@APP_LOB_ID=@BOAT_LOB)                  
   BEGIN                  
   SELECT                                                         
    @SUM_MVR_POINTS = SUM(ISNULL(POINTS_ASSIGNED,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )                       
   FROM                                                         
    APP_WATER_MVR_INFORMATION A WITH (NOLOCK)                                                        
   LEFT OUTER JOIN                  
    MNT_VIOLATIONS M   WITH (NOLOCK)                                                           
   ON                                                        
    A.VIOLATION_TYPE = M.VIOLATION_ID                         
   WHERE                                                        
    A.CUSTOMER_ID = @CUSTOMER_ID AND                                       
    A.APP_ID = @APP_ID AND                                                        
    A.APP_VERSION_ID = @APP_VERSION_ID AND                                          
    A.DRIVER_ID = @DRIVER_ID AND                                                        
    ISNULL(POINTS_ASSIGNED,0)>0                         
       --OCCURENCE_DATE <= @APP_EFFECTIVE_DATE AND OCCURENCE_DATE >= @PRIOR_TWO_YEARS_DATE            
 --AND A.MVR_DATE <= @APP_EFFECTIVE_DATE AND A.MVR_DATE >= @PRIOR_TWO_YEARS_DATE            
 AND DATEDIFF(DAY,A.MVR_DATE ,@APP_EFFECTIVE_DATE)/365.25 <= @VIOLATION_NUM_YEAR            
    AND A.IS_ACTIVE='Y' AND ISNULL(M.VIOLATION_CODE,0) NOT IN('10000','40000','SUSPN')   --!= '10000'      
           
    SELECT                                                         
    @MINOR_VIOLATION_REFER = SUM(ISNULL(POINTS_ASSIGNED,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )                       
   FROM                                                         
    APP_WATER_MVR_INFORMATION A WITH (NOLOCK)                                                        
   LEFT OUTER JOIN                                              
    MNT_VIOLATIONS M   WITH (NOLOCK)                                                           
   ON                                                        
    A.VIOLATION_TYPE = M.VIOLATION_ID                                                        
   WHERE                                                        
    A.CUSTOMER_ID = @CUSTOMER_ID AND                                       
    A.APP_ID = @APP_ID AND                                                        
    A.APP_VERSION_ID = @APP_VERSION_ID AND                   
    A.DRIVER_ID = @DRIVER_ID AND                                      
    ISNULL(POINTS_ASSIGNED,0)>0       
       --OCCURENCE_DATE <= @APP_EFFECTIVE_DATE AND OCCURENCE_DATE >= @PRIOR_TWO_YEARS_DATE            
 --AND A.MVR_DATE <= @APP_EFFECTIVE_DATE AND A.MVR_DATE >= @PRIOR_THREE_YEARS_DATE        
 AND DATEDIFF(DAY,A.MVR_DATE ,@APP_EFFECTIVE_DATE)/365.25 <= @VIOLATION_NONCHARGE_NUM_YEARS       
    AND A.IS_ACTIVE='Y' AND ISNULL(M.VIOLATION_CODE,0) NOT IN('10000','40000','SUSPN')      
      
   SELECT                                                         
    @SUM_MVR_POINTS_MAJOR = SUM(ISNULL(POINTS_ASSIGNED,0))                        
   FROM                                                         
    APP_WATER_MVR_INFORMATION A WITH (NOLOCK)                                                        
   LEFT OUTER JOIN                                              
    MNT_VIOLATIONS M   WITH (NOLOCK)                                                   
   ON                                                        
    A.VIOLATION_TYPE = M.VIOLATION_ID                                                        
   WHERE                                                        
    A.CUSTOMER_ID = @CUSTOMER_ID AND                                      
    A.APP_ID = @APP_ID AND                                                        
    A.APP_VERSION_ID = @APP_VERSION_ID AND                                          
    A.DRIVER_ID = @DRIVER_ID AND                                                        
    ISNULL(POINTS_ASSIGNED,0)>0                    
       --OCCURENCE_DATE <= @APP_EFFECTIVE_DATE AND OCCURENCE_DATE >= @PRIOR_FIVE_YEARS_DATE          
 --AND A.MVR_DATE <= @APP_EFFECTIVE_DATE AND A.MVR_DATE >= @PRIOR_FIVE_YEARS_DATE            
 AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE)/365.25 <= 5      
    AND A.IS_ACTIVE='Y' AND M.VIOLATION_CODE  IN('10000','40000','SUSPN')   --= '10000'                       
  END                  
 ELSE      -- For other LOBs            
   BEGIN                   
   SELECT                             
    @SUM_MVR_POINTS = SUM(ISNULL(POINTS_ASSIGNED,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )               
  -- @SUM_MVR_POINTS = SUM(ISNULL(MVR_POINTS,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )                       
   FROM                         
    APP_MVR_INFORMATION A WITH (NOLOCK)                                                        
   LEFT OUTER JOIN                                              
    MNT_VIOLATIONS M   WITH (NOLOCK)           
   ON                                                        
    A.VIOLATION_TYPE = M.VIOLATION_ID                       
    --  A.VIOLATION_ID = M.VIOLATION_ID                                      
   WHERE                                                        
    A.CUSTOMER_ID = @CUSTOMER_ID AND                                       
    A.APP_ID = @APP_ID AND                                                        
    A.APP_VERSION_ID = @APP_VERSION_ID AND                                          
    A.DRIVER_ID = @DRIVER_ID AND                                                        
    --ISNULL(POINTS_ASSIGNED,0)>0       
     NOT ISNULL(MVR_POINTS,0)< 0       
    --AND OCCURENCE_DATE <= @APP_EFFECTIVE_DATE AND OCCURENCE_DATE >= @PRIOR_TWO_YEARS_DATE            
     --AND MVR_DATE <= @APP_EFFECTIVE_DATE AND MVR_DATE >= @PRIOR_TWO_YEARS_DATE      -- MVR_DATE=conviction date      
  --AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE)/365.25 <= @VIOLATION_NUM_YEAR      
  AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE) <= @MINORVIOLATIONEFFECTIVEDAYS      
    AND A.IS_ACTIVE='Y' AND ISNULL(M.VIOLATION_CODE,'0') NOT IN('10000','40000','SUSPN')                       
 -- MINOR VIOLATION FOR REFERAL      
   SELECT                                                         
    @MINOR_VIOLATION_REFER = SUM(ISNULL(POINTS_ASSIGNED,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )                       
   FROM                         
    APP_MVR_INFORMATION A WITH (NOLOCK)                                                        
   LEFT OUTER JOIN                                              
    MNT_VIOLATIONS M   WITH (NOLOCK)           
   ON                                                        
    A.VIOLATION_TYPE = M.VIOLATION_ID                                   
   WHERE         
    A.CUSTOMER_ID = @CUSTOMER_ID AND                                       
    A.APP_ID = @APP_ID AND                                                        
    A.APP_VERSION_ID = @APP_VERSION_ID AND                                          
    A.DRIVER_ID = @DRIVER_ID AND                    
     NOT ISNULL(MVR_POINTS,0)< 0       
 --AND MVR_DATE <= @APP_EFFECTIVE_DATE AND MVR_DATE >= @PRIOR_THREE_YEARS_DATE      -- MVR_DATE=conviction date      
 --AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE)/365.25 <=@VIOLATION_NONCHARGE_NUM_YEARS      
 AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE) <=@MVREFFECTIVEDAYS      
      
    AND A.IS_ACTIVE='Y' AND ISNULL(M.VIOLATION_CODE,'0') NOT IN('10000','40000','SUSPN')           
       
   SELECT                                                         
    @SUM_MVR_POINTS_MAJOR = SUM(ISNULL(POINTS_ASSIGNED,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )                          
   -- @SUM_MVR_POINTS_MAJOR = SUM(ISNULL(MVR_POINTS,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )                          
   FROM                                                         
 APP_MVR_INFORMATION A WITH (NOLOCK)                                                        
   LEFT OUTER JOIN                                              
    MNT_VIOLATIONS M   WITH (NOLOCK)                                                           
   ON                                                        
    A.VIOLATION_TYPE = M.VIOLATION_ID                    
    --  A.VIOLATION_ID = M.VIOLATION_ID                    
   WHERE                                                        
    A.CUSTOMER_ID = @CUSTOMER_ID AND                                       
    A.APP_ID = @APP_ID AND                                                        
    A.APP_VERSION_ID = @APP_VERSION_ID AND                                          
    A.DRIVER_ID = @DRIVER_ID AND                  
    --ISNULL(POINTS_ASSIGNED,0)>0       
    NOT ISNULL(MVR_POINTS,0)<0       
    --AND OCCURENCE_DATE <= @APP_EFFECTIVE_DATE AND OCCURENCE_DATE >= @PRIOR_FIVE_YEARS_DATE        
    --AND MVR_DATE <= @APP_EFFECTIVE_DATE AND MVR_DATE >= @PRIOR_FIVE_YEARS_DATE                
 --AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE)/365.25 <=5      
 AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE) <=@MAJORVIOLATIONEFFECTIVEDAYS      
    AND A.IS_ACTIVE='Y' AND isnull(M.VIOLATION_CODE,'0') IN('10000','40000','SUSPN')                      
   END     

    
           
  -- Select Sum of all MVR Points                  
  SELECT     
	ISNULL(@SUM_MVR_POINTS,0) + ISNULL(@SUM_MVR_POINTS_MAJOR,0) AS SUM_MVR_POINTS,    
	ISNULL(@ACCIDENT_POINTS,0) AS ACCIDENT_POINTS,      
	ISNULL(@COUNT_ACCIDENTS,0) AS COUNT_ACCIDENTS ,    
	ISNULL(@MINOR_VIOLATION_REFER,0) + ISNULL(@SUM_MVR_POINTS_MAJOR,0) AS MVR_POINTS          
  	RETURN ISNULL(@SUM_MVR_POINTS,0) + ISNULL(@SUM_MVR_POINTS_MAJOR,0)                                                    
END                    
    
      
  


GO

