IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCoverageRanges]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCoverageRanges]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE        PROCEDURE Proc_GetCoverageRanges  
(  
   
 @COV_ID int
)  
  
As  
  
SELECT LIMIT_DEDUC_ID,  
       LIMIT_DEDUC_TYPE,    
       LIMIT_DEDUC_AMOUNT,  
       ISNULL(LIMIT_DEDUC_TYPE,'') + ';' + ISNULL(CONVERT(VarChar(10),LIMIT_DEDUC_AMOUNT),'') as DataValueField  
FROM MNT_COVERAGE_RANGES    
WHERE IS_ACTIVE='1' AND COV_ID = @COV_ID AND  
      LIMIT_DEDUC_TYPE =  'Limit'   
  
SELECT LIMIT_DEDUC_ID,  
       LIMIT_DEDUC_TYPE,    
       LIMIT_DEDUC_AMOUNT,  
       ISNULL(LIMIT_DEDUC_TYPE,'') + ';' + ISNULL(CONVERT(VarChar(10),LIMIT_DEDUC_AMOUNT),'') as DataValueField  
FROM MNT_COVERAGE_RANGES    
WHERE IS_ACTIVE='1' AND COV_ID = @COV_ID AND  
      LIMIT_DEDUC_TYPE =  'Deduct'   
  



GO

