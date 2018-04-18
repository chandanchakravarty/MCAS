IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyCoveragesAuto]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyCoveragesAuto]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetPolicyCoveragesAuto          
Created by      : Ravindra          
Date            : 03-13-2005         
Purpose         :         
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/       
      
--drop proc Proc_GetPolicyCoveragesAuto 1199,273,1,'app'        
CREATE PROCEDURE dbo.Proc_GetPolicyCoveragesAuto        
(            
 @CUSTOMER_ID int  ,        
 @POLICY_ID   int    ,    
 @POLICY_VERSION_ID int,  
 @CALLED_FROM varchar(10)  
)                
AS                     
        
BEGIN                          
        
/*select B.COV_ID,      
 B.COV_CODE,      
 B.COV_DES,      
 A.LIMIT_1 as COV_AMOUNT FROM POL_VEHICLE_COVERAGES A         
        
INNER JOIN MNT_COVERAGE B ON        
 A.COVERAGE_CODE_ID = B.COV_ID        
WHERE  B.COVERAGE_TYPE = 'pl'        
 and POLICY_ID = @POLICY_ID         
 and POLICY_VERSION_ID = @POLICY_VERSION_ID         
 and CUSTOMER_ID = @CUSTOMER_ID        
 AND A.VEHICLE_ID = ( SELECT MIN(VEHICLE_ID)         
     FROM POL_VEHICLES        
     WHERE CUSTOMER_ID =@CUSTOMER_ID         
     AND POLICY_ID = @POLICY_ID         
     AND POLICY_VERSION_ID =@POLICY_VERSION_ID        
) */  
if @CALLED_FROM IS NULL OR @CALLED_FROM=''  
begin  
 SELECT M.COV_ID, M.COV_CODE,M.COV_DES,CASE WHEN C.LIMIT_1 IS NULL THEN '' ELSE convert(varchar,C.LIMIT_1) END + CASE WHEN C.LIMIT_2 IS NULL THEN '' ELSE '/' + convert(varchar,C.LIMIT_2) END AS COV_AMOUNT    
 FROM   
  POL_VEHICLES P  
 JOIN  
  POL_VEHICLE_COVERAGES C  
 ON   
  P.CUSTOMER_ID = C.CUSTOMER_ID AND  
  P.POLICY_ID = C.POLICY_ID AND  
  P.POLICY_VERSION_ID = C.POLICY_VERSION_ID AND  
  P.VEHICLE_ID = C.VEHICLE_ID  
 JOIN  
  MNT_COVERAGE M  
 ON  
  C.COVERAGE_CODE_ID = M.COV_ID  
 WHERE  
  P.CUSTOMER_ID = @CUSTOMER_ID AND  
  P.POLICY_ID = @POLICY_ID AND  
  P.POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  P.VEHICLE_ID =   
   (SELECT TOP 1 VEHICLE_ID FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  
     AND POLICY_VERSION_ID = @POLICY_VERSION_ID )  
  AND  
   ISNULL(P.IS_ACTIVE,'Y') = 'Y'   
end  
else  
begin  
 SELECT M.COV_ID, M.COV_CODE,M.COV_DES,CASE WHEN C.LIMIT_1 IS NULL THEN '' ELSE convert(varchar,C.LIMIT_1) END + CASE WHEN C.LIMIT_2 IS NULL THEN '' ELSE '/' + convert(varchar,C.LIMIT_2) END AS COV_AMOUNT   
 FROM   
  APP_VEHICLES P  
 JOIN  
  APP_VEHICLE_COVERAGES C  
 ON   
  P.CUSTOMER_ID = C.CUSTOMER_ID AND  
  P.APP_ID = C.APP_ID AND  
  P.APP_VERSION_ID = C.APP_VERSION_ID AND  
  P.VEHICLE_ID = C.VEHICLE_ID  
 JOIN  
  MNT_COVERAGE M  
 ON  
  C.COVERAGE_CODE_ID = M.COV_ID  
 WHERE  
  P.CUSTOMER_ID = @CUSTOMER_ID AND  
  P.APP_ID = @POLICY_ID AND  
  P.APP_VERSION_ID = @POLICY_VERSION_ID AND  
  P.VEHICLE_ID =   
   (SELECT TOP 1 VEHICLE_ID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@POLICY_ID  
     AND APP_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(IS_ACTIVE,'Y')='Y')  
  AND  
   ISNULL(P.IS_ACTIVE,'Y') = 'Y'      
  
end  
        
        
End          
          


GO

