IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETCOMMODITYDATAINFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETCOMMODITYDATAINFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                          
PROC NAME      : DBO.[PROC_GETCOMMODITYDATAINFO]                          
CREATED BY       : PRADEEP KUSHWAHA                        
DATE             : 15-04-2010                          
PURPOSE       : RETRIEVING DATA FROM POL_COMMODITY_INFO                          
REVISON HISTORY :                 
MODIFY BY       :                          
DATE             :                          
PURPOSE       :         
                       
USED IN        : EBIX ADVANTAGE                      
------------------------------------------------------------                          
DATE     REVIEW BY          COMMENTS                          
------   ------------       -------------------------*/                          
--DROP PROC DBO.[PROC_GETCOMMODITYDATAINFO]                 
CREATE PROC [dbo].[PROC_GETCOMMODITYDATAINFO]  
@CUSTOMER_ID INT,  
@POLICY_ID INT,  
@POLICY_VERSION_ID SMALLINT, 
@COMMODITY_ID INT                           
AS                          
                          
BEGIN     
SELECT     
	CUSTOMER_ID,  
	POLICY_ID,  
	POLICY_VERSION_ID,  
	COMMODITY_ID,  
	COMMODITY_NUMBER,  
	COMMODITY,  
	DEPARTING_DATE,  
	ORIGIN_COUNTRY,  
	ORIGIN_STATE,  
	ORIGIN_CITY,  
	DESTINATION_COUNTRY,  
	DESTINATION_STATE,  
	DESTINATION_CITY,  
	REMARKS,  
	IS_ACTIVE,
	CONVEYANCE_TYPE,
	CO_APPLICANT_ID ,
	--I-TRACK-856
	ORIGN_COUNTRY,
	DEST_COUNTRY,
	ORIGN_STATE,
	DEST_STATE ,
	ORIGINAL_VERSION_ID,
	EXCEEDED_PREMIUM
FROM POL_COMMODITY_INFO  WITH (NOLOCK)   
WHERE 
	CUSTOMER_ID=@CUSTOMER_ID AND
	POLICY_ID =@POLICY_ID AND
	POLICY_VERSION_ID= @POLICY_VERSION_ID AND
	COMMODITY_ID=@COMMODITY_ID  
END            
GO

