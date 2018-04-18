IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAcord_XML_TEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAcord_XML_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc dbo.Proc_GetAcord_XML_TEST  
CREATE PROC dbo.Proc_GetAcord_XML_TEST    
(     
 @INSURANCE_SVC_RQ uniqueidentifier        
)            
AS            
BEGIN            
  
 SELECT  ACORD_XML FROM ACORD_QUOTE_DETAILS WHERE INSURANCE_SVC_RQ =    
 CONVERT(uniqueidentifier,@INSURANCE_SVC_RQ)  
         
END       
  
  
  
--Proc_GetAcord_XML   '7872E68E-A416-48B5-B0D2-C15C4E8D1176'  




GO

