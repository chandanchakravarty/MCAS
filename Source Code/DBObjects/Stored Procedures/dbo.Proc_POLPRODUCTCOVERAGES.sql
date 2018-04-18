IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_POLPRODUCTCOVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_POLPRODUCTCOVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--select lmi,* from POL_PERILS WHERE CUSTOMER_ID=28018

--select * from POL_PRODUCT_COVERAGES WHERE CUSTOMER_ID = 28018
--dbo.Proc_POLPRODUCTCOVERAGES 28018,5,1  

--DROP PROC dbo.Proc_POLPRODUCTCOVERAGES

CREATE PROC [dbo].[Proc_POLPRODUCTCOVERAGES]
( 
 @CUSTOMER_ID    int,                                                                                                                                                     
 @POLICY_ID    int,                                                                                                                                                      
 @POLICY_VERSION_ID   int   
 )              
AS              
BEGIN
 DECLARE @TOTAL_PREMIUM int    

--IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                   
--  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID )     
  
  SELECT  SUM(PPC.WRITTEN_PREMIUM) AS TOTALPREMIUM FROM POL_PRODUCT_COVERAGES PPC 
  INNER JOIN POL_PERILS PP ON PPC.CUSTOMER_ID = PP.CUSTOMER_ID AND PPC.POLICY_ID = PP.POLICY_ID AND PPC.POLICY_VERSION_ID = PP.POLICY_VERSION_ID 
  WHERE PPC.CUSTOMER_ID = @CUSTOMER_ID AND 
		PPC.POLICY_ID = @POLICY_ID AND 
		PPC.POLICY_VERSION_ID = @POLICY_VERSION_ID
  
  
  
 --SELECT PP.LMI AS LMI FROM POL_PRODUCT_COVERAGES PPC,POL_PERILS PP WHERE @TOTAL_PREMIUM != PP.LMI
  --FOR XML AUTO,ELEMENTS,Root('PREMIUM') 
  
  END

GO

