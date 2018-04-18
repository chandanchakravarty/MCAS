IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETRANDOMCUSTOMERCODE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETRANDOMCUSTOMERCODE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                      
Proc Name       : dbo.Proc_GetRandomCustomerCode                                                                
Created by      : Praveen kasana                                                                
Date            : 15/Feb/2008                                                                      
Purpose         : Generate Customer Code ..for Copy Client Process  
Revison History : 
modified by		: pravesh k chandel
date			: 18  april  2008
                                                                   
Used In        : Wolverine                                                                      
------------------------------------------------------------                                                                      
Date     Review By          Comments                                                                      
------   ------------       -------------------------*/                
--DROP PROC dbo.PROC_GETRANDOMCUSTOMERCODE 
CREATE PROC dbo.PROC_GETRANDOMCUSTOMERCODE   
(  
  @FIRSTNAME VARCHAR(50),  
  @LASTNAME VARCHAR(50),  
  @CUSTOMER_CODE VARCHAR(50) OUT  
)  
AS    
BEGIN  
DECLARE @VARRANDONCODE VARCHAR(30)  
DECLARE @RANDONCODE VARCHAR(30)  
SET @RANDONCODE = '0'  
  
DECLARE @S1 VARCHAR(20)  
DECLARE @S2 VARCHAR(20)  
  
  
IF(@RANDONCODE = '0')  
 SET @RANDONCODE = FLOOR(RAND() * 51)  
  
IF(LEN(isnull(@FIRSTNAME,'')) < 1 AND LEN(isnull(@LASTNAME,'')) < 1 )  
BEGIN  
 SET @CUSTOMER_CODE = ''  
END  
SET @VARRANDONCODE = SUBSTRING(isnull(@FIRSTNAME,''),0,2) + SUBSTRING(isnull(@LASTNAME,''),0,3) + @RANDONCODE  
SET @CUSTOMER_CODE =  @VARRANDONCODE  
  
END  
  
  
  
  
  
  
  
  
  
  
  
    
  



GO

