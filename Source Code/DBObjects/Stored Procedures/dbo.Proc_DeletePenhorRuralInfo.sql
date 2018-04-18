IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePenhorRuralInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePenhorRuralInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <20-Dec-2010>
-- Description:	< Delete data into POL_PENHOR_RURAL_INFO>
-- drop proc Proc_DeletePenhorRuralInfo
-- ============================================= 
CREATE PROC [dbo].[Proc_DeletePenhorRuralInfo]      
(       
   
 @PENHOR_RURAL_ID INT,  
 @CUSTOMER_ID INT,        
 @POLICY_ID INT,        
 @POLICY_VERSION_ID SMALLINT    
 
)                  
AS                  
BEGIN                  
      
  DELETE FROM POL_PENHOR_RURAL_INFO       
  WHERE   
  CUSTOMER_ID =  @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND    
  PENHOR_RURAL_ID  = @PENHOR_RURAL_ID   
  DELETE FROM POL_DISCOUNT_SURCHARGE  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@PENHOR_RURAL_ID   
  DELETE FROM POL_PRODUCT_COVERAGES  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK_ID=@PENHOR_RURAL_ID 
  
END           

GO

