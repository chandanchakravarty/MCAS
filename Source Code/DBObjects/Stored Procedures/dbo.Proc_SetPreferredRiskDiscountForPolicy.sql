IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetPreferredRiskDiscountForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetPreferredRiskDiscountForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name        : dbo.Proc_SetPreferredRiskDiscountForPolicy      
Created by       : Sumit Chhabra      
Date             : 13/02/2006                          
Purpose         :  To set preferred risk discount status for Motor  
Revison History :                          
Used In  : Wolverine                           
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                           
CREATE PROCEDURE Proc_SetPreferredRiskDiscountForPolicy      
(                          
                           
 @CUSTOMER_ID INT,                          
 @policy_ID  INT,                          
 @policy_VERSION_ID SMALLINT,                          
 @DRIVER_ID INT       
)                          
AS                          
  
DECLARE @DATE_LICENSED DATETIME    
DECLARE @LICENCED_YEAR INT    
DECLARE @MVR_POINTS INT    
DECLARE @PREFERRED_RISK VARCHAR(2)
BEGIN         
SET @PREFERRED_RISK='0'
SELECT @MVR_POINTS=ISNULL(SUM(ISNULL(MVR_POINTS,0)),0)  FROM POL_MVR_INFORMATION A JOIN MNT_VIOLATIONS M    
ON A.VIOLATION_ID=M.VIOLATION_ID    
WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.POLICY_ID=@POLICY_ID AND A.POLICY_VERSION_ID=@POLICY_VERSION_ID AND A.DRIVER_ID=@DRIVER_ID    
    
SELECT @DATE_LICENSED=DATE_LICENSED FROM POL_DRIVER_DETAILS    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID    
    
SET @LICENCED_YEAR = DATEDIFF(YY,@DATE_LICENSED,GETDATE())                        
  
IF(@LICENCED_YEAR>=3 AND  (@MVR_POINTS<=2 OR @MVR_POINTS IS NULL))       
 SET @PREFERRED_RISK='1'  
ELSE      
  SET @PREFERRED_RISK='0'  
  
 UPDATE POL_DRIVER_DETAILS SET PREFERRED_RISK=@PREFERRED_RISK WHERE  
  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID      
            
END         
    



GO

