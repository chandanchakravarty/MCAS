IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       : DBO.PROC_UPDATE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO               
Created by      : Pradeep Kushwaha        
Date            : 28/05/2010                  
Purpose			:To Update records in POL_PASSENGERS_PERSONAL_ACCIDENT_INFO table.                  
Revison History :                  
Used In			: Ebix Advantage                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--DROP PROC dbo.PROC_UPDATE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO       
      
CREATE PROC [dbo].[PROC_UPDATE_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]        
(      
	@CUSTOMER_ID INT,
	@POLICY_ID INT, 
	@POLICY_VERSION_ID SMALLINT, 
	@PERSONAL_ACCIDENT_ID INT,  
	@START_DATE DATETIME,
	@END_DATE DATETIME,
	@NUMBER_OF_PASSENGERS NUMERIC,
	@MODIFIED_BY INT,    
	@LAST_UPDATED_DATETIME DATETIME ,
	@CO_APPLICANT_ID INT ,
	 @EXCEEDED_PREMIUM INT = NULL 
	 -- ,	@ORIGINAL_VERSION_ID INT=NULL  
)    
AS      
BEGIN      
 DECLARE @FLAG BIT   
  EXEC PROC_CHECK_RISK_EFFDATE_AND_RISK_EXPDATE @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@START_DATE,@END_DATE,@FLAG OUT  
  IF(@FLAG=1)
  BEGIN     
UPDATE POL_PASSENGERS_PERSONAL_ACCIDENT_INFO      
SET      
	START_DATE=@START_DATE,
	END_DATE=@END_DATE,
	NUMBER_OF_PASSENGERS=@NUMBER_OF_PASSENGERS,
	MODIFIED_BY=@MODIFIED_BY,    
	LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,
	CO_APPLICANT_ID=@CO_APPLICANT_ID,
	EXCEEDED_PREMIUM = @EXCEEDED_PREMIUM
	-- ,	ORIGINAL_VERSION_ID=@ORIGINAL_VERSION_ID   
	
WHERE    
	CUSTOMER_ID= @CUSTOMER_ID AND
	POLICY_ID=@POLICY_ID AND
	POLICY_VERSION_ID=@POLICY_VERSION_ID AND
	PERSONAL_ACCIDENT_ID=@PERSONAL_ACCIDENT_ID   
	RETURN 1
	END
	ELSE
	BEGIN
	RETURN -3
	END
END 
GO

