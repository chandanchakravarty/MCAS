IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_CLAIM_COVERAGE_COV_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_CLAIM_COVERAGE_COV_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_GetMNT_CLAIM_COVERAGE_COV_ID                                      
Created by      : Mohit Agarwal                                      
Date            : 5-Dec-2007                                      
Purpose         : Fetch data in MNT_CLAIM_COVERAGE table for claim notification screen                                      
Revison History :                                      
Used In        : Wolverine                                      
------------------------------------------------------------                                      
Modified By  :            
Date   :            
Purpose  :            
------------------------------------------------------------                                                                            
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
--DROP PROC dbo.Proc_GetMNT_CLAIM_COVERAGE_COV_ID                                                      
CREATE PROC dbo.Proc_GetMNT_CLAIM_COVERAGE_COV_ID                 
(                                                 
@COV_ID_CLAIM int,  
@CLAIM_ID int,
@TOT_COV_CLAIM int=null                                                                              
)                               
AS                                                        
BEGIN                  

DECLARE @TOTAL_COV_CLAIM int

SELECT @TOTAL_COV_CLAIM = count(*) FROM MNT_CLAIM_COVERAGE WHERE CLAIM_ID = @CLAIM_ID

IF @TOT_COV_CLAIM IS NULL OR @TOT_COV_CLAIM = @TOTAL_COV_CLAIM
BEGIN      
SELECT       
  COV_ID  
FROM       
 MNT_CLAIM_COVERAGE      
WHERE      
 @COV_ID_CLAIM=COV_ID_CLAIM AND CLAIM_ID = @CLAIM_ID    
END

ELSE IF @TOT_COV_CLAIM < @TOTAL_COV_CLAIM
BEGIN
--Get Coverages                        

CREATE TABLE #CLAIM_COV          
(                 
 [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,          
 [COV_ID] [int],    
 [CLAIM_ID] [int]
)

INSERT INTO #CLAIM_COV(COV_ID, CLAIM_ID)
SELECT COV_ID, CLAIM_ID FROM MNT_CLAIM_COVERAGE WHERE CLAIM_ID = @CLAIM_ID 
AND IS_ACTIVE = 'Y' ORDER BY COV_ID_CLAIM

SELECT       
  COV_ID  
FROM       
 #CLAIM_COV      
WHERE      
 @COV_ID_CLAIM=IDENT_COL AND CLAIM_ID = @CLAIM_ID    

DROP TABLE #CLAIM_COV
 END
END         
    
  



GO

