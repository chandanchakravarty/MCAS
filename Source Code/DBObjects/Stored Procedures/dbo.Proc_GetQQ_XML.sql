IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQQ_XML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQQ_XML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.Proc_GetQQ_XML        
(         
 @INSURANCE_SVC_RQ varchar(100)            
)                
AS                
BEGIN                
      
 SELECT  QQ_XML FROM ACORD_QUOTE_DETAILS WHERE INSURANCE_SVC_RQ = @INSURANCE_SVC_RQ       
              
END      
  



GO

