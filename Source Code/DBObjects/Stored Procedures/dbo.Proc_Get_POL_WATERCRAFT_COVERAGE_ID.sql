IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POL_WATERCRAFT_COVERAGE_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POL_WATERCRAFT_COVERAGE_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : Dbo.Proc_Get_POL_WATERCRAFT_COVERAGE_ID          
Created by      : SHAFI          
Date            : 14/02/06  
Purpose         :Gets the CoverageID for the passed code for watercraft        
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- drop proc dbo.Proc_Get_POL_WATERCRAFT_COVERAGE_ID
CREATE PROCEDURE dbo.Proc_Get_POL_WATERCRAFT_COVERAGE_ID        
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
        
SELECT @STATEID = STATE_ID,        
 @LOBID = POLICY_LOB        
FROM POL_CUSTOMER_POLICY_LIST  with(nolock)      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
 POLICY_ID = @POL_ID AND        
 POLICY_VERSION_ID = @POL_VERSION_ID        
        
SELECT @COV_ID = COV_ID        
FROM MNT_COVERAGE     with(nolock)   
WHERE STATE_ID = @STATEID AND        
 LOB_ID = 4 AND        
 COV_CODE = @COV_CODE AND         
 IS_ACTIVE = 'Y'        
        
RETURN ISNULL(@COV_ID,0)        
        
        
  
        
      
    
  





GO

