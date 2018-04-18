IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_POL_PRODUCT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_POL_PRODUCT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 --drop proc Proc_Delete_POL_PRODUCT_COVERAGES  
/*----------------------------------------------------------            
Proc Name   : dbo.Proc_Delete_POL_PRODUCT_COVERAGES           
Created by  : Pravesh K Chandel        
Date        : 31 March 2010          
Purpose     :    Delete Product Coverage         
Revison History  :                  
------------------------------------------------------------                        
Date     Review By          Comments                      
-----------------------------------------------------------*/      
CREATE   PROCEDURE [dbo].[Proc_Delete_POL_PRODUCT_COVERAGES]    
(     
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint,     
 @COVERAGE_ID smallint,    
 @RISK_ID smallint    
)    
    
As    
    
DECLARE @COV_ID Int    
DECLARE @END_ID smallint     
    
--SELECT @COV_ID = COVERAGE_CODE_ID    
--FROM POL_PRODUCT_COVERAGES    
--WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
--      POLICY_ID =  @POLICY_ID AND    
--      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND      
--      COVERAGE_ID =  @COVERAGE_ID AND    
--	 RISK_ID =  @RISK_ID
    
DELETE FROM POL_PRODUCT_COVERAGES    
WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
      POLICY_ID =  @POLICY_ID AND    
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND      
      COVERAGE_ID =  @COVERAGE_ID AND    
	  RISK_ID =  @RISK_ID
    
    
RETURN 1    
    
    
    
  
  

GO

