IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetMVRViolationPointsApp]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetMVRViolationPointsApp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[fun_GetMVRViolationPointsApp]                                                                                          
(                                                                                          
@CUSTOMER_ID int,                                                        
@APP_ID int,                                                        
@APP_VERSION_ID smallint,                                                        
@DRIVER_ID smallint,                                                        
@VIOLATION_NUM_YEAR int,      
@VIOLATION_NONCHARGE_NUM_YEARS int      
)   RETURNS INT                                                                                          
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
      
SET @MINORVIOLATIONEFFECTIVEDATE = DATEADD(YEAR,-@VIOLATION_NUM_YEAR,@APP_EFFECTIVE_DATE)      
SET @MINORVIOLATIONEFFECTIVEDAYS = DATEDIFF(DAY,@MINORVIOLATIONEFFECTIVEDATE,@APP_EFFECTIVE_DATE)      
SET @MAJORVIOLATIONEFFECTIVEDATE = DATEADD(YEAR,-5,@APP_EFFECTIVE_DATE)      
SET @MAJORVIOLATIONEFFECTIVEDAYS = DATEDIFF(DAY,@MAJORVIOLATIONEFFECTIVEDATE,@APP_EFFECTIVE_DATE)      
SET @MVREFFECTIVEDATE = DATEADD(YEAR,-@VIOLATION_NONCHARGE_NUM_YEARS,@APP_EFFECTIVE_DATE)      
SET @MVREFFECTIVEDAYS = DATEDIFF(DAY,@MVREFFECTIVEDATE,@APP_EFFECTIVE_DATE)      
---------------------------------------------------------------------------------------------------      
--    three, two ,five years previous date(END)      
----------------------------------------------------------------------------------------------------        
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

      RETURN   
 ISNULL(@SUM_MVR_POINTS,0) + ISNULL(@SUM_MVR_POINTS_MAJOR,0) 
           
END                    
    
      
  




GO

