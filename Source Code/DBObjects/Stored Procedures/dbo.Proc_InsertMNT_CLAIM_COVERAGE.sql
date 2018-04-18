IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertMNT_CLAIM_COVERAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertMNT_CLAIM_COVERAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                    
Proc Name       : dbo.Proc_InsertMNT_CLAIM_COVERAGE                                    
Created by      : Mohit Agarwal                                    
Date            : 5-Dec-2007                                    
Purpose         : Insert data in MNT_CLAIM_COVERAGE table for claim notification screen                                    
Revison History :                                    
Used In        : Wolverine                                    
------------------------------------------------------------                                    
Modified By  :          
Date   :          
Purpose  :          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
-- DROP PROC dbo.Proc_InsertMNT_CLAIM_COVERAGE                                    
CREATE PROC dbo.Proc_InsertMNT_CLAIM_COVERAGE  
(
@CLAIM_ID int,
@COV_DES varchar(500),
@STATE_ID varchar(10),
@LOB_ID varchar(10),
@LIMIT_1 decimal(18,2),
@DEDUCTIBLE_1 decimal(18,2),
@IS_ACTIVE char(1),
@CREATED_BY int = null,
@CREATED_DATETIME DateTime = null,
@MODIFIED_BY int = null,
@LAST_UPDATED_DATETIME DateTime = null,
@COV_ID int out
)                                 
AS                      
BEGIN               

DECLARE @COV_ID_CLAIM int

IF ISNULL(@LOB_ID,'') = '' 
    SELECT @LOB_ID=LOB_ID FROM CLM_DUMMY_POLICY WHERE @CLAIM_ID=CLAIM_ID 

IF ISNULL(@STATE_ID,'') = '' 
    SELECT @STATE_ID=DUMMY_STATE FROM CLM_DUMMY_POLICY WHERE @CLAIM_ID=CLAIM_ID 
              
 SELECT @COV_ID_CLAIM = COUNT(*)+1 FROM MNT_CLAIM_COVERAGE WHERE @CLAIM_ID=CLAIM_ID                    
 INSERT INTO MNT_CLAIM_COVERAGE                                     
 (                                    
  CLAIM_ID,
  COV_DES,
  STATE_ID,
  LOB_ID,
  LIMIT_1,
  DEDUCTIBLE_1,
  IS_ACTIVE,
  CREATED_BY,
  CREATED_DATETIME,
  MODIFIED_BY,
  LAST_UPDATED_DATETIME,
  COV_ID_CLAIM
         
 )                                    
 VALUES                                    
 (                                    
  @CLAIM_ID,
  @COV_DES,
  @STATE_ID,
  @LOB_ID,
  @LIMIT_1,
  @DEDUCTIBLE_1,
  @IS_ACTIVE,
  @CREATED_BY,
  @CREATED_DATETIME,
  @MODIFIED_BY,
  @LAST_UPDATED_DATETIME,
  @COV_ID_CLAIM
 )                                    
 
  SELECT @COV_ID=MAX(COV_ID) FROM MNT_CLAIM_COVERAGE                                  
END                                    
  




GO

