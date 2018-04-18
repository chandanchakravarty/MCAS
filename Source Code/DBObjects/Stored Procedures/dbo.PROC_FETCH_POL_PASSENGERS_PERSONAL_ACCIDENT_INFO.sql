IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_FETCH_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_FETCH_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name       : DBO.[PROC_FETCH_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]                          
Created by      : PRADEEP KUSHWAHA                        
Date            : 28-05-2010                          
Purpose			: retrieving data from POL_PASSENGERS_PERSONAL_ACCIDENT_INFO                          
Revison History :                 
Modify by       : 
Date            :                  
Purpose			:         
                       
Used In			: Ebix Advantage                      
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
--DROP PROC DBO.PROC_FETCH_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO 28070,329,2,1
 
CREATE PROC [dbo].[PROC_FETCH_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]    
	@CUSTOMER_ID INT,
	@POLICY_ID INT,
	@POLICY_VERSION_ID SMALLINT,
	@PERSONAL_ACCIDENT_ID INT                           
AS                                                   
BEGIN     
SELECT
	 POLICY_ID , 
	 POLICY_VERSION_ID , 
	 CUSTOMER_ID ,
	 PERSONAL_ACCIDENT_ID,
	 START_DATE,
	 END_DATE,
	 NUMBER_OF_PASSENGERS,
	 IS_ACTIVE ,
	 CREATED_BY ,
	 CREATED_DATETIME,
	 MODIFIED_BY ,
	 LAST_UPDATED_DATETIME ,
	 CO_APPLICANT_ID,
	 ORIGINAL_VERSION_ID,
	 ISNULL(RISK_ORIGINAL_ENDORSEMENT_NO,0)AS RISK_ORIGINAL_ENDORSEMENT_NO,
	  EXCEEDED_PREMIUM
	 
FROM 
	POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)
WHERE
	CUSTOMER_ID= @CUSTOMER_ID AND
	POLICY_ID=@POLICY_ID AND
	POLICY_VERSION_ID=@POLICY_VERSION_ID AND
	PERSONAL_ACCIDENT_ID=@PERSONAL_ACCIDENT_ID   
END

GO

