IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePenhorRuralInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePenhorRuralInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <20-Dec-2010>
-- Description:	< Delete data into POL_PENHOR_RURAL_INFO>
-- drop proc Proc_ActivateDeactivatePenhorRuralInfo
-- ============================================= 
CREATE  PROC [dbo].[Proc_ActivateDeactivatePenhorRuralInfo]    
(            
  @PENHOR_RURAL_ID INT,       
  @CUSTOMER_ID INT,        
  @POLICY_ID INT,        
  @POLICY_VERSION_ID SMALLINT,  
  @IS_ACTIVE   NCHAR(1)            
)            
AS            
BEGIN    
UPDATE POL_PENHOR_RURAL_INFO        
 SET             
    IS_ACTIVE  = @IS_ACTIVE ,
      ORIGINAL_VERSION_ID = CASE  WHEN @IS_ACTIVE='Y' THEN 0 ELSE  @POLICY_VERSION_ID END              
 WHERE            
       
  CUSTOMER_ID =  @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND    
  PENHOR_RURAL_ID = @PENHOR_RURAL_ID   
    
END        
GO

