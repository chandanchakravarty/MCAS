IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckSignedForm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckSignedForm]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_CheckSignedForm           
Modified By : sawrup             
Modified On : 04/05/2006           
             
Purpose     : Get value if Uninsured/Underinsured Motorist Protecion been Rejected or Lower Limits accepted              
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/             
--drop proc dbo.Proc_CheckSignedForm       
CREATE    PROC dbo.Proc_CheckSignedForm 
(            
@CUSTOMER_ID INT,                  
@APP_ID INT,                  
@APP_VERSION_ID INT ,
@CALLED_FROM VARCHAR(10)=NULL,
@RESULT INT OUT                
)
AS                  
                  
BEGIN 
IF (@CALLED_FROM IS NULL OR UPPER(@CALLED_FROM)='APP')
BEGIN
	IF EXISTS (SELECT COVERAGE_ID from APP_VEHICLE_COVERAGES
		WHERE CUSTOMER_ID=@CUSTOMER_ID 
		AND COVERAGE_CODE_ID=1007
		AND APP_ID =@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID)
	
		SET @RESULT = 1
	
	ELSE 
	
		SET @RESULT = 2
END
ELSE
BEGIN
	IF EXISTS (SELECT COVERAGE_ID from POL_VEHICLE_COVERAGES
		WHERE CUSTOMER_ID=@CUSTOMER_ID 
		AND COVERAGE_CODE_ID=1007
		AND POLICY_ID =@APP_ID AND POLICY_VERSION_ID =@APP_VERSION_ID)
	
		SET @RESULT = 1
	
	ELSE 
	
		SET @RESULT = 2
END
END 





GO

