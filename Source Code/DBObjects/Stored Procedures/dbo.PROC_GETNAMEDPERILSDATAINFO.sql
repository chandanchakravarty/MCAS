IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETNAMEDPERILSDATAINFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETNAMEDPERILSDATAINFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
PROC NAME      : DBO.PROC_GETNAMEDPERILSDATAINFO                      
CREATED BY       : PRADEEP KUSHWAHA                    
DATE             : 01-04-2010                      
PURPOSE       : RETRIEVING DATA FROM POL_PERILS                      
REVISON HISTORY :             
MODIFY BY       :                      
DATE             :                      
PURPOSE       :     
                   
USED IN        : EBIX ADVANTAGE                  
------------------------------------------------------------                      
DATE     REVIEW BY          COMMENTS                      
------   ------------       -------------------------*/                      
--DROP PROC DBO.PROC_GETNAMEDPERILSDATAINFO             
CREATE PROC [dbo].[PROC_GETNAMEDPERILSDATAINFO]
@PERIL_ID SMALLINT  ,
@CUSTOMER_ID INT,
@POLICY_ID INT,  
@POLICY_VERSION_ID SMALLINT                
AS                      
                      
BEGIN 
	SELECT 
	CUSTOMER_ID ,
	POLICY_ID,
	POLICY_VERSION_ID,
	PERIL_ID,
	LOCATION,
	OCCUPANCY,
	CONSTRUCTION,
	ACTIVITY_TYPE,
	VR,
	LMI,
	BUILDING,
	MRI,
	TYPE,
	LOSS,
	LOYALTY,
	PERC_LOYALTY,
	DEDUCTIBLE_OPTION,
	MULTIPLE_DEDUCTIBLE,
	CATEGORY,
	IS_ACTIVE,   
	RAWVALUES,
	REMARKS,
	PARKING_SPACES,
	CLAIM_RATIO,
	RAW_MATERIAL_VALUE,
	CONTENT_VALUE,
	ASSIST24,
	BONUS,
	LOCATION_NUMBER,
	ITEM_NUMBER,
	ACTUAL_INSURED_OBJECT,
	ORIGINAL_VERSION_ID,
	 EXCEEDED_PREMIUM

FROM POL_PERILS	WITH (NOLOCK)	
WHERE 
 CUSTOMER_ID =  @CUSTOMER_ID AND      
 POLICY_ID = @POLICY_ID AND      
 POLICY_VERSION_ID=@POLICY_VERSION_ID AND
 PERIL_ID=@PERIL_ID
   
                  
END        
    
GO

