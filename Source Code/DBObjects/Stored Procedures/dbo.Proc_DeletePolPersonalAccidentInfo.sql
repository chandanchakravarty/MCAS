IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolPersonalAccidentInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolPersonalAccidentInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                            
Proc Name      : dbo.[Proc_DeletePolPersonalAccidentInfo]                            
Created by     : Chetna Agarwal                          
Date           : 16-04-2010   
Modify by      : PRADEEP KUSHWAHA
Date           : 25-05-2010                           
Purpose        : Delete data from POL_PERSONAL_ACCIDENT_INFO                                                    
Used In        : Ebix Advantage                        
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
--drop proc dbo.Proc_DeletePolPersonalAccidentInfo  
  
CREATE PROC [dbo].[Proc_DeletePolPersonalAccidentInfo]  
(  
	@CUSTOMER_ID INT,
	@POLICY_ID INT,
	@POLICY_VERSION_ID SMALLINT,
	@PERSONAL_INFO_ID INT   
)  
AS  
BEGIN  
 DELETE FROM POL_DISCOUNT_SURCHARGE  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@PERSONAL_INFO_ID   
 DELETE FROM POL_PRODUCT_COVERAGES  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK_ID=@PERSONAL_INFO_ID 
 DELETE FROM POL_BENEFICIARY  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK_ID=@PERSONAL_INFO_ID 
 
 --ADDED BY PRAVEER FOR ITRACK 1040
 IF NOT EXISTS(SELECT MAIN_INSURED FROM POL_PERSONAL_ACCIDENT_INFO WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND MAIN_INSURED=@PERSONAL_INFO_ID)
 BEGIN
 DELETE FROM POL_PERSONAL_ACCIDENT_INFO  WHERE  CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND PERSONAL_INFO_ID=@PERSONAL_INFO_ID
 END

 
END
GO

