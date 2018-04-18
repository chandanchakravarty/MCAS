IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyCoveragesWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyCoveragesWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetPolicyCoveragesWatercraft            
Created by      : Ravindra            
Date            : 03-13-2005           
Purpose         :           
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
          
--drop proc Proc_GetPolicyCoveragesWatercraft         
        
 CREATE PROCEDURE dbo.Proc_GetPolicyCoveragesWatercraft          
(              
 @CUSTOMER_ID int  ,          
 @POLICY_ID   int    ,      
 @POLICY_VERSION_ID int,  
 @CALLED_FROM varchar(10)  
)                  
AS                       
          
BEGIN                            
          
/*SELECT B.COV_ID,      
 B.COV_CODE,      
 B.COV_DES,           
 A.LIMIT_1 as COV_AMOUNT FROM POL_WATERCRAFT_COVERAGE_INFO A          
INNER JOIN MNT_COVERAGE B ON          
 A.COVERAGE_CODE_ID = B.COV_ID          
WHERE  --B.COVERAGE_TYPE = 'pl'          
  POLICY_ID = @POLICY_ID           
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID          
 AND CUSTOMER_ID = @CUSTOMER_ID          
 AND A.BOAT_ID = ( SELECT MIN(BOAT_ID)           
     FROM POL_WATERCRAFT_INFO          
     WHERE CUSTOMER_ID =@CUSTOMER_ID           
     AND POLICY_ID = @POLICY_ID           
     AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
    )                 
*/  
if (@CALLED_FROM IS NULL OR @CALLED_FROM='')  
begin  
 SELECT    
  M.COV_ID,M.COV_CODE,M.COV_DES,C.LIMIT_1 AS COV_AMOUNT   
 FROM   
  POL_WATERCRAFT_COVERAGE_INFO C JOIN POL_WATERCRAFT_INFO B  
 ON   
  C.CUSTOMER_ID = B.CUSTOMER_ID AND C.POLICY_ID = B.POLICY_ID AND C.POLICY_VERSION_ID = B.POLICY_VERSION_ID  
  AND C.BOAT_ID = B.BOAT_ID   
 JOIN   
  MNT_COVERAGE M ON C.COVERAGE_CODE_ID = M.COV_ID  
 WHERE   
  B.CUSTOMER_ID = @CUSTOMER_ID AND B.POLICY_ID = @POLICY_ID AND B.POLICY_VERSION_ID=@POLICY_VERSION_ID AND   
  C.BOAT_ID =   
   (SELECT TOP 1 BOAT_ID FROM  
   POL_WATERCRAFT_INFO B WHERE B.CUSTOMER_ID = @CUSTOMER_ID AND B.POLICY_ID = @POLICY_ID AND   
   B.POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(B.IS_ACTIVE,'Y')='Y' )  
  AND ISNULL(B.IS_ACTIVE,'Y')='Y' AND ISNULL(C.IS_ACTIVE,'Y')='Y'  
end  
else
begin
 SELECT    
  M.COV_ID,M.COV_CODE,M.COV_DES,C.LIMIT_1 AS COV_AMOUNT   
 FROM   
  APP_WATERCRAFT_COVERAGE_INFO C JOIN APP_WATERCRAFT_INFO B  
 ON   
  C.CUSTOMER_ID = B.CUSTOMER_ID AND C.APP_ID = B.APP_ID AND C.APP_VERSION_ID = B.APP_VERSION_ID  
  AND C.BOAT_ID = B.BOAT_ID   
 JOIN   
  MNT_COVERAGE M ON C.COVERAGE_CODE_ID = M.COV_ID  
 WHERE   
  B.CUSTOMER_ID = @CUSTOMER_ID AND B.APP_ID = @POLICY_ID AND B.APP_VERSION_ID=@POLICY_VERSION_ID AND   
  C.BOAT_ID =   
   (SELECT TOP 1 BOAT_ID FROM  
   APP_WATERCRAFT_INFO B WHERE B.CUSTOMER_ID = @CUSTOMER_ID AND B.APP_ID = @POLICY_ID AND   
   B.APP_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(B.IS_ACTIVE,'Y')='Y' )  
  AND ISNULL(B.IS_ACTIVE,'Y')='Y' AND ISNULL(C.IS_ACTIVE,'Y')='Y'  
end
          
End  
  
  



GO

