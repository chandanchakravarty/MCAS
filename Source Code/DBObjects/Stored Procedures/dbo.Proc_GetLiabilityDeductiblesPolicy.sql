IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLiabilityDeductiblesPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLiabilityDeductiblesPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name            : Dbo.Proc_GetLiabilityDeductiblesPolicy
Created by           : Shafi      
Date                 : 16-02-06
Purpose              :  Gets the decutibles for Personal Liability Coverage         
Revison History :          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_GetLiabilityDeductiblesPolicy       
(        
        
 @CUSTOMER_ID int,        
 @POL_ID int,        
 @POL_VERSION_ID int        
)        
        
AS        
BEGIN        
        
 DECLARE @STATEID SmallInt              
 DECLARE @LOBID NVarCHar(5)              
           
               
 SELECT @STATEID = STATE_ID,              
  @LOBID = POLICY_LOB              
 FROM POL_CUSTOMER_POLICY_LIST              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  POLICY_ID = @POL_ID AND              
  POLICY_VERSION_ID = @POL_VERSION_ID              
         
 SELECT R.LIMIT_DEDUC_AMOUNT        
 FROM MNT_COVERAGE_RANGES R        
 INNER JOIN MNT_COVERAGE C ON        
  R.COV_ID = C.COV_ID        
 WHERE C.COV_CODE = 'PL' AND        
  C.STATE_ID = @STATEID AND        
  C.LOB_ID = @LOBID  AND    
  R.LIMIT_DEDUC_TYPE = 'Limit' AND  
  R.IS_ACTIVE=1     
END        
        
    
    
  
 


GO

