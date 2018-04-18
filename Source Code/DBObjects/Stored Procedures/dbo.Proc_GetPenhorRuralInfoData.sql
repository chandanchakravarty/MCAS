IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPenhorRuralInfoData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPenhorRuralInfoData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <20-Dec-2010>
-- Description:	< Retrieving data from POL_PENHOR_RURAL_INFO>
-- drop proc Proc_GetPenhorRuralInfoData
-- =============================================
                
CREATE PROC [dbo].[Proc_GetPenhorRuralInfoData]      
 @PENHOR_RURAL_ID INT,    
 @CUSTOMER_ID  INT ,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID SMALLINT    
AS                            
                            
BEGIN       
SELECT       
CUSTOMER_ID,POLICY_ID,
POLICY_VERSION_ID,PENHOR_RURAL_ID,
ITEM_NUMBER,FESR_COVERAGE,
MODE,PROPERTY,
CULTIVATION,CITY,
STATE_ID,INSURED_AREA,
SUBSIDY_PREMIUM,SUBSIDY_STATE,
IS_ACTIVE,CREATED_BY,
CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,REMARKS  ,
ORIGINAL_VERSION_ID,
EXCEEDED_PREMIUM
    
FROM POL_PENHOR_RURAL_INFO   WITH(NOLOCK)   
  
WHERE   CUSTOMER_ID =  @CUSTOMER_ID AND        
		POLICY_ID = @POLICY_ID AND        
		POLICY_VERSION_ID=@POLICY_VERSION_ID  AND    
		PENHOR_RURAL_ID    = @PENHOR_RURAL_ID    
                   
END 

 
GO

