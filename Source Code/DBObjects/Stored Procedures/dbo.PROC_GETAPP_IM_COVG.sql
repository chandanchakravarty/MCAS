IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETAPP_IM_COVG]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETAPP_IM_COVG]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE PROC_GETAPP_IM_COVG 
(              
  @CUSTOMER_ID INT,              
  @APP_ID INT,              
  @APP_VERSION_ID SMALLINT
)              
              
AS            
BEGIN
	SELECT MAX(CVG.ITEM_ID) AS ITEM_ID, MAX(DEDUCTIBLE) AS DEDUCTIBLE, SUM(ISNULL(ITEM_INSURING_VALUE,0)) AS ITEM_INSURING_VALUE 
	FROM APP_HOME_OWNER_SCH_ITEMS_CVGS CVG WITH (NOLOCK)
	LEFT JOIN APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS CVGD WITH (NOLOCK)
	ON CVG.CUSTOMER_ID = CVGD.CUSTOMER_ID
	AND CVG.APP_ID = CVGD.APP_ID
	AND  CVG.APP_VERSION_ID = CVGD.APP_VERSION_ID
	AND CVG.ITEM_ID = CVGD.ITEM_ID

	WHERE 	CVG.CUSTOMER_ID = @CUSTOMER_ID AND      
		CVG.APP_ID = @APP_ID AND              
		CVG.APP_VERSION_ID = @APP_VERSION_ID 
		  
	GROUP BY CVG.ITEM_ID, DEDUCTIBLE
END
-- PROC_GETAPP_IM_COVG 920, 489,1
-- SP_HELPTEXT PROC_GETAPP_IM_COVG
-- sp_columns APP_HOME_OWNER_SCH_ITEMS_CVGS_Details
-- select * from APP_HOME_OWNER_SCH_ITEMS_CVGS_Details
			        

        

        
          
        
      
    
  
  









GO

