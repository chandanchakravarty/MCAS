IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMotorMVRViolationPointsPol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMotorMVRViolationPointsPol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                            
Proc Name       : dbo.GetMotorMVRViolationPointsPol                                                                                            
Created by      : Manoj Rathore                                                          
Date            : 29/04/2009                                                                                            
Purpose         : To calculate MVR points for Motorcycle a driver at Policy level                                                          
Revison History :                                                                                  
Used In         : Wolverine                                                                                            
------------------------------------------------------------                                                                                            
Date     Review By          Comments                      
                    
Note: Whatever changes you make here should be changed in rules  sps.                    
                                                                                         
------   ------------       -------------------------              
DROP PROC [dbo].[GetMotorMVRViolationPointsPol] 1701,37,1,1,3,2,3,2000                                                                                           
*/                                                                                         
                                                                                   
CREATE PROC [dbo].[GetMotorMVRViolationPointsPol]                                                                                     
(                                                                                      
@CUSTOMER_ID int,                                                    
@POLICY_ID int,                                                    
@POLICY_VERSION_ID smallint,                                                   
@DRIVER_ID smallint,                                                    
@ACCIDENT_NUM_YEAR int,                        
@VIOLATION_NUM_YEAR int,        
@VIOLATION_NONCHARGE_NUM_YEARS int,        
@ACCIDENT_PAID_AMOUNT int    =null                    
)                                                                                      
AS                                                                                      
BEGIN                                                                               
                               
 DECLARE @POLICY_LOB_ID INT                                              
 DECLARE @SUM_MVR_POINTS INT                                                        
 DECLARE @SUM_MVR_POINTS_MAJOR INT                                                          
 DECLARE @APP_EFFECTIVE_DATE DATETIME              
 DECLARE @PRIOR_TWO_YEARS_DATE DATETIME         
 DECLARE @PRIOR_THREE_YEARS_DATE DATETIME                                                         
 DECLARE @PRIOR_FIVE_YEARS_DATE DATETIME              
 DECLARE @BOAT_LOB int                                              
 DECLARE @ACCIDENT_POINTS INT                          
 DECLARE @COUNT_ACCIDENTS INT         
 DECLARE @APP_ID INT        
 DECLARE @APP_VERSION_ID SMALLINT         
 DECLARE @COUNT_ACCIDENTS_APP INT        
 DECLARE @WOLVERINECONTINOUSLYINSURED SMALLINT                                         
 DECLARE @FIRST_ACCIDENTS CHAR(1)        
 DECLARE @MINOR_VIOLATION_REFER INT          
 SET @COUNT_ACCIDENTS_APP = 0                                             
 SET @BOAT_LOB = 4                                              
 SET @SUM_MVR_POINTS = 0                                                     
 SET @SUM_MVR_POINTS_MAJOR = 0                                
 SET @ACCIDENT_POINTS = 0          
 SET @FIRST_ACCIDENTS  ='N'           
  -- Select App Effective Date                       
SELECT @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,@POLICY_LOB_ID = CAST(POLICY_LOB AS INT),@APP_ID=APP_ID,@APP_VERSION_ID=APP_VERSION_ID         
FROM POL_CUSTOMER_POLICY_LIST with(nolock)WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                
POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID          
SELECT  @WOLVERINECONTINOUSLYINSURED =ISNULL(YEARS_INSU_WOL,0)      
FROM POL_AUTO_GEN_INFO with(nolock)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND   POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
    
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
DECLARE @MISCEFFECTIVEDATE DATETIME        
DECLARE @MISCEFFECTIVEDAYS INT   
SET @ACCIDENTEFFECTIVEDAYS=0        
SET @MINORVIOLATIONEFFECTIVEDAYS=0        
SET @MAJORVIOLATIONEFFECTIVEDAYS=0        
SET @MVREFFECTIVEDAYS=0   
SET @MISCEFFECTIVEDAYS=0       
SET @ACCIDENTEFFECTIVEDATE = DATEADD(YEAR,-@ACCIDENT_NUM_YEAR,@APP_EFFECTIVE_DATE)        
SET @ACCIDENTEFFECTIVEDAYS = DATEDIFF(DAY,@ACCIDENTEFFECTIVEDATE,@APP_EFFECTIVE_DATE)        
SET @MINORVIOLATIONEFFECTIVEDATE = DATEADD(YEAR,-@VIOLATION_NUM_YEAR,@APP_EFFECTIVE_DATE)        
SET @MINORVIOLATIONEFFECTIVEDAYS = DATEDIFF(DAY,@MINORVIOLATIONEFFECTIVEDATE,@APP_EFFECTIVE_DATE)        
SET @MAJORVIOLATIONEFFECTIVEDATE = DATEADD(YEAR,-5,@APP_EFFECTIVE_DATE)        
SET @MAJORVIOLATIONEFFECTIVEDAYS = DATEDIFF(DAY,@MAJORVIOLATIONEFFECTIVEDATE,@APP_EFFECTIVE_DATE)        
SET @MVREFFECTIVEDATE = DATEADD(YEAR,-@VIOLATION_NONCHARGE_NUM_YEARS,@APP_EFFECTIVE_DATE)        
SET @MVREFFECTIVEDAYS = DATEDIFF(DAY,@MVREFFECTIVEDATE,@APP_EFFECTIVE_DATE)        
SET @MISCEFFECTIVEDATE = DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE)        
SET @MISCEFFECTIVEDAYS = DATEDIFF(DAY,@MISCEFFECTIVEDATE,@APP_EFFECTIVE_DATE)        
             
---------------------------------------------------------------------------------------------------        
--    three, two ,five years previous date(END)        
----------------------------------------------------------------------------------------------------          
    -- Count Accidents  (pol level)                                          
    SELECT @COUNT_ACCIDENTS=COUNT(CUSTOMER_ID) FROM FETCH_ACCIDENT with(nolock)                                              
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                        
    (POLICY_ID IS NULL or POLICY_ID=@POLICY_ID) --AND (POLICY_VERSION_ID IS NULL OR POLICY_VERSION_ID=@POLICY_VERSION_ID)                                     
    AND LOB=@POLICY_LOB_ID         
    AND                                    
    (        
      
    (ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENTEFFECTIVEDAYS         
    AND                                  
      
    (ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))>=0        
    )        
    AND(        
      
    (         
      
     (ISNULL(DRIVER_NAME,'') = '')         
        
       OR (  
      dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR)   
      AND dbo.piece(DRIVER_NAME,'^',2)=CAST(@POLICY_ID AS VARCHAR)   
      AND  CAST(dbo.piece(ISNULL(DRIVER_NAME,''),'^',3) AS INT)<=CAST(@POLICY_VERSION_ID AS INT)   
      AND  dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)   
      AND   dbo.piece(DRIVER_NAME,'^',5)= 'POL'  
     )        
     OR (  
      dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR)   
      AND dbo.piece(DRIVER_NAME,'^',2)=CAST(@APP_ID AS VARCHAR)   
      AND dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)         
      AND dbo.piece(DRIVER_NAME,'^',5)= 'APP')  
     )         
        OR   
      (  
     dbo.piece(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',1)   
     = dbo.piece(REPLACE(dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' +   
     CAST(@POLICY_ID AS VARCHAR) +  '^' +   
     CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' +   
     CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL'),' ',','),',',1)   
          
     AND   
        (  
      dbo.piece(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',2)   
      = dbo.piece(REPLACE(dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@POLICY_ID AS VARCHAR) +  '^' + CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL'),' ',','),',',2)        
      OR  
      dbo.piece(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',2)   
      = dbo.piece(REPLACE(dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@POLICY_ID AS VARCHAR) +  '^' + CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL'),' ',','),',',3)        
           )   
  
     AND  
       (  
      ISNULL(dbo.piece(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',3),'') =  
      CASE WHEN ISNULL(DBO.PIECE(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',3),'') !=''  
      THEN  
       ISNULL(DBO.PIECE(REPLACE(DBO.FUN_GETDRIVERNAME(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@POLICY_ID AS VARCHAR)  
        +  '^' + CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL'),' ',','),',',3),'')        
      ELSE ISNULL(DBO.PIECE(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',3),'')  
      END  
       )  
      )  
  
  
       AND (CHARGEABLE=11924 or (CHARGEABLE=0 AND  ISNULL(AT_FAULT,0)=CASE LOB WHEN 4 THEN ISNULL(AT_FAULT,0) ELSE 10963 END  ))        
    )          
    AND  ISNULL(AT_FAULT,0)  =CASE LOB WHEN 4 THEN ISNULL(AT_FAULT,0) ELSE 10963 END          
  
    AND ISNULL(AMOUNT_PAID,0) > CASE LOB WHEN 4 THEN 1000.00 WHEN 3 THEN 499.99 ELSE 999.99 END -- ITRACK 5081       
       
  
  
   -----added by pravesh        
   IF (@COUNT_ACCIDENTS=1        
    AND        
      EXISTS(        
      SELECT CUSTOMER_ID FROM FETCH_ACCIDENT with(nolock)                                              
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                        
    (POLICY_ID IS NULL or POLICY_ID=@POLICY_ID)   
     AND LOB=@POLICY_LOB_ID         
     AND                                    
    (         
       
      (ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENTEFFECTIVEDAYS        
      AND                                                
     (ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))>=0        
    )        
     AND                              
       
      (        
     ((isnull(DRIVER_NAME,'') = '')         
         
       OR   
       (  
     dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR)   
     AND dbo.piece(DRIVER_NAME,'^',2)=CAST(@POLICY_ID AS VARCHAR)   
     AND  CAST(dbo.piece(ISNULL(DRIVER_NAME,''),'^',3) AS INT)<=CAST(@POLICY_VERSION_ID AS INT)         
       AND  dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)   
     AND   dbo.piece(DRIVER_NAME,'^',5)= 'POL'  
       )        
      
       OR   
       (  
     dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR)   
     AND dbo.piece(DRIVER_NAME,'^',2)=CAST(@APP_ID AS VARCHAR)   
     AND dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)         
        AND dbo.piece(DRIVER_NAME,'^',5)= 'APP'  
       )  
     OR   
      (  
     dbo.piece(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',1)   
     = dbo.piece(REPLACE(dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' +   
     CAST(@POLICY_ID AS VARCHAR) +  '^' +   
     CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' +   
     CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL'),' ',','),',',1)   
          
     AND   
        (  
      dbo.piece(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',2)   
      = dbo.piece(REPLACE(dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@POLICY_ID AS VARCHAR) +  '^' + CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL'),' ',','),',',2)        
      OR  
      dbo.piece(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',2)   
      = dbo.piece(REPLACE(dbo.fun_getDriverName(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@POLICY_ID AS VARCHAR) +  '^' + CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL'),' ',','),',',3)        
           )   
  
     AND  
       (  
      ISNULL(dbo.piece(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',3),'') =  
      CASE WHEN ISNULL(DBO.PIECE(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',3),'') !=''  
      THEN  
       ISNULL(DBO.PIECE(REPLACE(DBO.FUN_GETDRIVERNAME(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@POLICY_ID AS VARCHAR)  
        +  '^' + CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL'),' ',','),',',3),'')        
      ELSE ISNULL(DBO.PIECE(REPLACE(ISNULL(DRIVER_NAME,''),' ',','),',',3),'')  
      END  
     )  )  
      )  
         
     AND (CHARGEABLE=11924 or (CHARGEABLE=0 AND  AT_FAULT=10963 )) )         
     AND AT_FAULT=10963          
     AND PAID_LOSS < 2000        
     )        
     )        
    SET @FIRST_ACCIDENTS='Y'        
          
   -- if more than 1 loss exists below $2000 then no excuse         
   IF((SELECT COUNT(*) FROM FETCH_LOSS with(nolock)                                              
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                        
    (POLICY_ID IS NULL or POLICY_ID=@POLICY_ID) --AND (POLICY_VERSION_ID IS NULL OR POLICY_VERSION_ID=@POLICY_VERSION_ID)                                     
     AND LOB=@POLICY_LOB_ID         
     AND                                    
    ((ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))<=@ACCIDENTEFFECTIVEDAYS        
     AND                                                
     (ISNULL(DATEDIFF(DD,OCCURENCE_DATE,@APP_EFFECTIVE_DATE),0))>=0        
     )        
     AND                              
    (((isnull(DRIVER_NAME,'') = '')         
      --OR DRIVER_NAME=(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@POLICY_ID AS VARCHAR) +  '^' +  CAST(@POLICY_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'POL')        
      OR (dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR) AND dbo.piece(DRIVER_NAME,'^',2)=CAST(@POLICY_ID AS VARCHAR) AND  CAST(dbo.piece(ISNULL(DRIVER_NAME,''),'^',3) AS INT)<=CAST(@POLICY_VERSION_ID AS INT)         
      AND  dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR) AND   dbo.piece(DRIVER_NAME,'^',5)= 'POL')        
      OR (dbo.piece(DRIVER_NAME,'^',1)=CAST(@CUSTOMER_ID AS VARCHAR) AND         
      dbo.piece(DRIVER_NAME,'^',2)=CAST(@APP_ID AS VARCHAR) AND dbo.piece(DRIVER_NAME,'^',4)=CAST(@DRIVER_ID AS VARCHAR)         
      AND dbo.piece(DRIVER_NAME,'^',5)= 'APP')        
    --DRIVER_NAME=(CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) +  '^' + CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^' + 'APP')        
    )         
     AND (CHARGEABLE=11924 or (CHARGEABLE=0 AND  AT_FAULT=10963 )) )         
     AND AT_FAULT=10963          
     AND PAID_LOSS < 2000)>1)        
    BEGIN        
     SET @FIRST_ACCIDENTS='N'        
    END         
           
---end here 
  
  ----------------------------------------------------------------------        
  --   CLAIM CHECK(END)        
  ----------------------------------------------------------------------   
       
  IF(@FIRST_ACCIDENTS='Y' AND @WOLVERINECONTINOUSLYINSURED >=3 AND @POLICY_LOB_ID='2')        
   BEGIN        
    --SET @PRIRLOSSCUSTOMER =0;        
    SET @COUNT_ACCIDENTS =  0  -- @COUNT_ACCIDENTS - @PRIRLOSSCUSTOMERPOL        
   END        
  --END        
                         
    if (@COUNT_ACCIDENTS>0)                                
     SET @ACCIDENT_POINTS = (ISNULL(@COUNT_ACCIDENTS,0) * 4 ) - 1              
                
   -- Set Two Years Prior App Effective Date              
   SET @PRIOR_TWO_YEARS_DATE =  DATEADD(DD,-@VIOLATION_NUM_YEAR * 365.25,@APP_EFFECTIVE_DATE)              
   SET @PRIOR_THREE_YEARS_DATE =  DATEADD(DD,-@VIOLATION_NONCHARGE_NUM_YEARS * 365.25,@APP_EFFECTIVE_DATE)          
   SET @PRIOR_FIVE_YEARS_DATE =  DATEADD(DD,-5 * 365.5,@APP_EFFECTIVE_DATE)   
  
  
   SELECT                
   @SUM_MVR_POINTS = SUM(ISNULL(POINTS_ASSIGNED,0))  + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))                                                    
   --@SUM_MVR_POINTS = SUM(ISNULL(MVR_POINTS,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0) )            
   FROM  POL_MVR_INFORMATION A with(nolock)                                                    
   LEFT OUTER JOIN MNT_VIOLATIONS M with(nolock)  ON  A.VIOLATION_TYPE = M.VIOLATION_ID                                           
   WHERE                                                    
   A.CUSTOMER_ID = @CUSTOMER_ID AND                                  
   A.POLICY_ID = @POLICY_ID AND                                         
   A.POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                                    
   A.DRIVER_ID = @DRIVER_ID AND                                                           
   NOT ISNULL(MVR_POINTS,0)<0         
   AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE) <= @MINORVIOLATIONEFFECTIVEDAYS        
   AND A.IS_ACTIVE='Y' AND ISNULL(M.VIOLATION_CODE,'') NOT IN('10000','40000','SUSPN')         
          
     
   SELECT                
   @MINOR_VIOLATION_REFER =  SUM(ISNULL(POINTS_ASSIGNED,0))  + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))            
   FROM POL_MVR_INFORMATION A with(nolock)                                                    
   LEFT OUTER JOIN  MNT_VIOLATIONS M with(nolock)  ON A.VIOLATION_TYPE = M.VIOLATION_ID                    
   WHERE                                  
   A.CUSTOMER_ID = @CUSTOMER_ID AND                
   A.POLICY_ID = @POLICY_ID AND                                                    
   A.POLICY_VERSION_ID = @POLICY_VERSION_ID AND                             
   A.DRIVER_ID = @DRIVER_ID AND                            
   NOT ISNULL(MVR_POINTS,0)<0        
   AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE) <=@MVREFFECTIVEDAYS        
   AND A.IS_ACTIVE='Y' AND ISNULL(M.VIOLATION_CODE,'') NOT IN('10000','40000','SUSPN')               
     
       
   SELECT                                                     
   @SUM_MVR_POINTS_MAJOR = SUM(ISNULL(POINTS_ASSIGNED,0))   + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))                                                     
   FROM  POL_MVR_INFORMATION A with(nolock)                                                    
   LEFT OUTER JOIN MNT_VIOLATIONS M with(nolock)  ON  A.VIOLATION_TYPE = M.VIOLATION_ID                                                  
     
   WHERE                                                    
   A.CUSTOMER_ID = @CUSTOMER_ID AND                           
   A.POLICY_ID = @POLICY_ID AND                                                    
   A.POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                                    
   A.DRIVER_ID = @DRIVER_ID AND                                                    
       
   NOT ISNULL(MVR_POINTS,0)<0        
   AND DATEDIFF(DAY,MVR_DATE ,@APP_EFFECTIVE_DATE) <=@MAJORVIOLATIONEFFECTIVEDAYS                
   AND A.IS_ACTIVE='Y' AND  isnull(M.VIOLATION_CODE,'') IN('10000','40000','SUSPN')   
         
   
              
                                             
    ----------------------MISC POINTS--------------------------        
                 
   DECLARE @SUMOFMISCPOINTS int                            
   DECLARE @LICUNDER3YRS VARCHAR(5)      
   SET @SUMOFMISCPOINTS = 0       
   SELECT       
   @LICUNDER3YRS  =   
   CASE                
   WHEN DATEDIFF(DAY, CONVERT(CHAR(10),isnull(adds.DATE_LICENSED,'01/01/1901'),101), @APP_EFFECTIVE_DATE) >= @MISCEFFECTIVEDAYS THEN 'N'                              
   ELSE 'Y'                  
   END      
   FROM                           
   POL_DRIVER_DETAILS adds WITH (NOLOCK)       
     
   WHERE                                         
   adds.CUSTOMER_ID=@CUSTOMER_ID AND adds.POLICY_ID=@POLICY_ID AND adds.POLICY_VERSION_ID=@POLICY_VERSION_ID       
   AND adds.DRIVER_ID=@DRIVER_ID        
  
     
   IF (@LICUNDER3YRS = 'Y')      
   BEGIN      
   SET @SUMOFMISCPOINTS     = 3                          
   END      
 END       
------------------------------------------------------------                                         
  SELECT     
 ISNULL(@SUM_MVR_POINTS,0) + ISNULL(@SUM_MVR_POINTS_MAJOR,0) AS SUM_MVR_POINTS,    
 ISNULL(@ACCIDENT_POINTS,0) AS ACCIDENT_POINTS,    
 ISNULL(@COUNT_ACCIDENTS,0) AS COUNT_ACCIDENTS,        
 ISNULL(@MINOR_VIOLATION_REFER,0) + ISNULL(@SUM_MVR_POINTS_MAJOR,0) AS MVR_POINTS,    
 ISNULL(@SUMOFMISCPOINTS,0) AS SUMOFMISCPOINTS                             
           
  --RETURN ISNULL(@SUM_MVR_POINTS,0)                                                    
                                                      
--END   

GO

