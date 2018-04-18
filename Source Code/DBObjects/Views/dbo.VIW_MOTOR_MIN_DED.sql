IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VIW_MOTOR_MIN_DED]'))
DROP VIEW [dbo].[VIW_MOTOR_MIN_DED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
View Name           : dbo.VIW_MOTOR_MIN_DED  
Created by          : Pradeep  
Date                : 13 Mar 2006  
Purpose             : Gets the min ded values according to motorcycle CC  
Revison History  :  
Used In             :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
  
      
CREATE VIEW VIW_MOTOR_MIN_DED        
AS        
SELECT D.CC_RANGE1,        
 ISNULL(D.CC_RANGE2,9999999) as CC_RANGE2,        
 C.COV_DES,        
 C.STATE_ID,      
 C.LOB_ID,        
 C.COV_ID,        
 B.LIMIT_DEDUC_AMOUNT,    
 B.LIMIT_DEDUC_ID         
 FROM         
MNT_MOTORCYCLE_CC_MIN_COVERAGE A        
INNER JOIN MNT_COVERAGE_RANGES B ON        
 A.LIMIT_DEDUC_ID = B.LIMIT_DEDUC_ID        
INNER JOIN MNT_COVERAGE C ON        
 B.COV_ID = C.COV_ID        
INNER JOIN MNT_MOTORCYCLE_CC D ON        
 A.CC_ID = D.CC_ID        
        
      
    
  
  






GO

