IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetPreferredRiskDiscount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetPreferredRiskDiscount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name        : dbo.Proc_SetPreferredRiskDiscount      
Created by       : Sumit Chhabra      
Date             : 13/02/2006                          
Purpose         :  To set preferred risk discount status for Motor  
Revison History :                          
Used In  : Wolverine                           
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                           
CREATE PROCEDURE Proc_SetPreferredRiskDiscount      
(                          
                           
 @CUSTOMER_ID INT,                          
 @APP_ID  INT,                          
 @APP_VERSION_ID SMALLINT,                          
 @DRIVER_ID INT       
)                          
AS                          
  
declare @DATE_LICENSED datetime    
declare @LICENCED_YEAR INT    
declare @MVR_POINTS int    
declare @PREFERRED_RISK varchar(2)
BEGIN         
set @PREFERRED_RISK='0'
SELECT @MVR_POINTS=isnull(SUM(isnull(MVR_POINTS,0)),0)  FROM APP_MVR_INFORMATION A JOIN MNT_VIOLATIONS M    
ON A.VIOLATION_ID=M.VIOLATION_ID    
WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID AND A.DRIVER_ID=@DRIVER_ID    
    
select @DATE_LICENSED=DATE_LICENSED from app_driver_details    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DRIVER_ID=@DRIVER_ID    
    
SET @LICENCED_YEAR = DATEDIFF(YY,@DATE_LICENSED,GETDATE())                        
  
if(@LICENCED_YEAR>=3 and  (@MVR_POINTS<=2 or @MVR_POINTS is null))       
 SET @PREFERRED_RISK='1'  
ELSE      
  SET @PREFERRED_RISK='0'  
  
 UPDATE APP_DRIVER_DETAILS SET PREFERRED_RISK=@PREFERRED_RISK WHERE  
  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DRIVER_ID=@DRIVER_ID      
            
END         
    
  



GO

