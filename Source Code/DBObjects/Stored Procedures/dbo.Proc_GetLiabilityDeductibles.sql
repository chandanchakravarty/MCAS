IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLiabilityDeductibles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLiabilityDeductibles]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name          : Dbo.Proc_GetLiabilityDeductibles        
Created by          : Pradeep    
Date                 : 11/17/2005        
Purpose              :  Gets the decutibles for Personal Liability Coverage       
Revison History :        
Modified by          : Sumit Chhabra
Date                 : 21/12/2005        
Purpose              :  Added the check for active records
Used In               :   Wolverine          
    
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetLiabilityDeductibles      
(      
      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID int      
)      
      
AS      
BEGIN      
      
 DECLARE @STATEID SmallInt            
 DECLARE @LOBID NVarCHar(5)            
         
             
 SELECT @STATEID = STATE_ID,            
  @LOBID = APP_LOB            
 FROM APP_LIST            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
  APP_ID = @APP_ID AND            
  APP_VERSION_ID = @APP_VERSION_ID            
       
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

