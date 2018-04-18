IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETCIVILTRANSPORTVEHICLEINFODATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETCIVILTRANSPORTVEHICLEINFODATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                          
PROC NAME      : DBO.[PROC_GETCIVILTRANSPORTVEHICLEINFODATA]                          
CREATED BY     : PRADEEP KUSHWAHA                        
DATE           : 23-04-2010                          
PURPOSE        : RETRIEVING DATA FROM POL_CIVIL_TRANSPORT_VEHICLES                          
REVISON HISTORY:                 
MODIFY BY      :                          
DATE           :                          
PURPOSE        :         
USED IN        : EBIX ADVANTAGE                      
------------------------------------------------------------                          
DATE     REVIEW BY          COMMENTS                          
------   ------------       -------------------------*/                          
--DROP PROC DBO.PROC_GETCIVILTRANSPORTVEHICLEINFODATA                 
CREATE PROC [dbo].[PROC_GETCIVILTRANSPORTVEHICLEINFODATA]    
	@VEHICLE_ID INT,  
	@CUSTOMER_ID  INT ,  
	@POLICY_ID INT,  
	@POLICY_VERSION_ID SMALLINT  
AS                          
                          
BEGIN   
DECLARE @CALLED_FROM NVARCHAR(50)  
SELECT     
   
	CUSTOMER_ID,
	POLICY_ID,
	POLICY_VERSION_ID,
	VEHICLE_ID,
	CLIENT_ORDER,
	VEHICLE_NUMBER,
	MANUFACTURED_YEAR,
	FIPE_CODE,
	CATEGORY,
	CAPACITY,
	MAKE_MODEL,
	LICENSE_PLATE,
	CHASSIS,
	MANDATORY_DEDUCTIBLE,
	FACULTATIVE_DEDUCTIBLE,
	SUB_BRANCH,
	RISK_EFFECTIVE_DATE,
	RISK_EXPIRE_DATE,
	REGION,
	COV_GROUP_CODE,
	FINANCE_ADJUSTMENT,
	REFERENCE_PROPOSASL,
	REMARKS,
	IS_ACTIVE,
	CO_APPLICANT_ID ,
	TICKET_NUMBER,
	STATE_ID,
	ZIP_CODE,
    @CALLED_FROM as CALLED_FROM,
    ORIGINAL_VERSION_ID,
	 EXCEEDED_PREMIUM
FROM POL_CIVIL_TRANSPORT_VEHICLES   WITH(NOLOCK) 

WHERE 
	 CUSTOMER_ID =  @CUSTOMER_ID AND      
	 POLICY_ID = @POLICY_ID AND      
	 POLICY_VERSION_ID=@POLICY_VERSION_ID  AND  
	 VEHICLE_ID    = @VEHICLE_ID  
   
                 
END            
GO

