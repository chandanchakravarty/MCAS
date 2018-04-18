IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCoInsuranceAndReInsuranceDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCoInsuranceAndReInsuranceDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================              
-- Author   : Pradeep Kushwaha  
-- Create date: 24-Nov-2010          
-- Description: Get Co-Insurance And Re-Insurance details 
-- DROP PROC Proc_GetCoInsuranceAndReInsuranceDetails        
-- Proc_GetCoInsuranceAndReInsuranceDetails  
-- =============================================                        
                           
CREATE PROC [dbo].[Proc_GetCoInsuranceAndReInsuranceDetails]    
 
AS                        
BEGIN 
SELECT  REIN_COMAPANY_NAME


FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK)
 
END
GO

