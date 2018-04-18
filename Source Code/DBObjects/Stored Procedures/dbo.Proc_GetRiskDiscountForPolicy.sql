IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRiskDiscountForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRiskDiscountForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name        : dbo.Proc_GetRiskDiscountForPolicy    
Created by       : Sumit Chhabra    
Date             : 10/02/2006                        
Purpose       :  Retrieve the sum of MVR points for a driver for Policy    
Revison History :                        
Used In  : Wolverine                         
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                         
CREATE PROCEDURE Proc_GetRiskDiscountForPolicy    
(                        
                         
 @CUSTOMER_ID INT,                        
 @POLICY_ID  INT,                        
 @POLICY_VERSION_ID SMALLINT,                        
 @DRIVER_ID INT     
)                        
AS                        
DECLARE @DATE_LICENSED DATETIME    
DECLARE @LICENCED_YEAR INT    
DECLARE @MVR_POINTS INT    
BEGIN         
SELECT @MVR_POINTS=SUM(MVR_POINTS)  FROM POL_MVR_INFORMATION A JOIN MNT_VIOLATIONS M    
ON A.VIOLATION_ID=M.VIOLATION_ID    
WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.POLICY_ID=@POLICY_ID AND A.POLICY_VERSION_ID=@POLICY_VERSION_ID AND A.DRIVER_ID=@DRIVER_ID    
    
SELECT @DATE_LICENSED=DATE_LICENSED FROM POL_DRIVER_DETAILS    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID    
    
SET @LICENCED_YEAR = DATEDIFF(YY,@DATE_LICENSED,GETDATE())                      
SELECT SUM(MVR_POINTS)  FROM POL_MVR_INFORMATION A JOIN MNT_VIOLATIONS M    
ON A.VIOLATION_ID=M.VIOLATION_ID    
WHERE A.CUSTOMER_ID=1  
  
  
IF(@LICENCED_YEAR>=3 AND  (@MVR_POINTS<=2 OR @MVR_POINTS IS NULL))    
 RETURN 1    
ELSE    
 RETURN 0    
          
END       
  



GO

