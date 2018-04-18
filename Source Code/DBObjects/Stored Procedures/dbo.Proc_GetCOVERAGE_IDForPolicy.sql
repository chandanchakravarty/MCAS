IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCOVERAGE_IDForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCOVERAGE_IDForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC Proc_GetCOVERAGE_IDForPolicy
 /*----------------------------------------------------------        
Proc Name       : Dbo.Proc_GetCOVERAGE_IDForPolicy        
Created by      : Shafi        
Date            : 16-02-06        
Purpose         :Gets the CoverageID for the passed code       
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROCEDURE [dbo].[Proc_GetCOVERAGE_IDForPolicy]      
(      
 @CUSTOMER_ID Int,      
 @POL_ID Int,      
 @POL_VERSION_ID smallint,      
 @COV_CODE VarChar(10)      
)      
AS      
      
DECLARE @STATEID SmallInt      
DECLARE @LOBID NVarCHar(5)      
DECLARE @COV_ID Int      
      
SELECT   @LOBID = POLICY_LOB,  
  
 @STATEID = STATE_ID                    
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE                    
 CUSTOMER_ID = @CUSTOMER_ID AND                    
 POLICY_ID = @POL_ID AND                    
 POLICY_VERSION_ID = @POL_VERSION_ID       
      
SELECT @COV_ID = COV_ID      
FROM MNT_COVERAGE   WITH(NOLOCK)     
WHERE STATE_ID = @STATEID AND      
 LOB_ID = @LOBID AND      
 COV_CODE = @COV_CODE AND       
 IS_ACTIVE = 'Y'     
 -- COV ID NOT FIND IN COVERAGE MAIN TABLE, SEARH IT IN EXTRA TABLE , ADDED ON 26 AUG 2010 BY P K CHANDEL
IF (ISNULL(@COV_ID,0)=0) 
BEGIN
SELECT @COV_ID = COV_ID      
FROM MNT_COVERAGE_EXTRA  WITH(NOLOCK)      
WHERE STATE_ID = @STATEID AND      
 LOB_ID = @LOBID AND      
 COV_CODE = @COV_CODE AND       
 IS_ACTIVE = 'Y'      
END  
      
RETURN ISNULL(@COV_ID,0)      
      

GO

