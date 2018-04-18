IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetScheduleOfUnderlyingCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetScheduleOfUnderlyingCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop PROCEDURE dbo.Proc_GetScheduleOfUnderlyingCoverages 1199,232,1,'re','tttt'     
CREATE PROCEDURE dbo.Proc_GetScheduleOfUnderlyingCoverages    
(          
 @CUSTOMER_ID int  ,      
 @APP_ID  int,      
 @APP_VERSION_ID int,      
 @POLICY_NO varchar(75),  
 @POLICY_COMPANY varchar(150) = null      
       
)              
AS                   
      
BEGIN                        
      
SELECT       
POLICY_NUMBER,      
COVERAGE_DESC AS COV_DES ,      
COVERAGE_AMOUNT AS COV_AMOUNT,      
POLICY_TEXT,    
COVERAGE_TYPE,
COV_CODE
      
 FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES  with(nolock)     
WHERE  CUSTOMER_ID=@CUSTOMER_ID      
  AND APP_ID=@APP_ID       
  AND APP_VERSION_ID=@APP_VERSION_ID      
  AND POLICY_NUMBER=@POLICY_NO   
  and POLICY_COMPANY = @POLICY_COMPANY    
      
      
End        
        



GO

