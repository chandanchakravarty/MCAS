IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRiskDiscount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRiskDiscount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name        : dbo.Proc_GetRiskDiscount  
Created by       : Sumit Chhabra  
Date             : 16/12/2005                      
Purpose       :  Retrieve the sum of MVR points for a driver  
Revison History :                      
Used In  : Wolverine                       
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                       
CREATE PROCEDURE Proc_GetRiskDiscount  
(                      
                       
 @CUSTOMER_ID int,                      
 @APP_ID  int,                      
 @APP_VERSION_ID smallint,                      
 @DRIVER_ID INT   
)                      
AS                      
declare @DATE_LICENSED datetime  
declare @LICENCED_YEAR INT  
declare @MVR_POINTS int  
BEGIN       
SELECT @MVR_POINTS=SUM(MVR_POINTS)  FROM APP_MVR_INFORMATION A JOIN MNT_VIOLATIONS M  
ON A.VIOLATION_ID=M.VIOLATION_ID  
WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID AND A.DRIVER_ID=@DRIVER_ID  
  
select @DATE_LICENSED=DATE_LICENSED from app_driver_details  
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DRIVER_ID=@DRIVER_ID  
  
SET @LICENCED_YEAR = DATEDIFF(YY,@DATE_LICENSED,GETDATE())                    
SELECT SUM(MVR_POINTS)  FROM APP_MVR_INFORMATION A JOIN MNT_VIOLATIONS M  
ON A.VIOLATION_ID=M.VIOLATION_ID  
WHERE A.CUSTOMER_ID=1


if(@LICENCED_YEAR>=3 and  (@MVR_POINTS<=2 or @MVR_POINTS is null))  
 return 1  
else  
 return 0  
        
END     



GO

