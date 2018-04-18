IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                            
Proc Name      : dbo.[PROC_DELETE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]                            
Created by     : PRADEEP KUSHWAHA                        
Date           : 28-05-2010   
Modify by      : 
Date           : 
Purpose        : Delete data from POL_PASSENGERS_PERSONAL_ACCIDENT_INFO                                                    
Used In        : Ebix Advantage                        
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
--DROP PROC DBO.PROC_DELETE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO  
  
CREATE PROC [dbo].[PROC_DELETE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]  
(  
	@CUSTOMER_ID INT,
	@POLICY_ID INT,
	@POLICY_VERSION_ID SMALLINT,
	@PERSONAL_ACCIDENT_ID INT   
)  
AS  
BEGIN  
 DELETE FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO         
 WHERE   
	CUSTOMER_ID= @CUSTOMER_ID AND
	POLICY_ID=@POLICY_ID AND
	POLICY_VERSION_ID=@POLICY_VERSION_ID AND
	PERSONAL_ACCIDENT_ID=@PERSONAL_ACCIDENT_ID   
	DELETE FROM POL_DISCOUNT_SURCHARGE  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@PERSONAL_ACCIDENT_ID   
	DELETE FROM POL_PRODUCT_COVERAGES  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK_ID=@PERSONAL_ACCIDENT_ID 
END

GO

