IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DELETE_POL_SUP_FORM_SHOP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DELETE_POL_SUP_FORM_SHOP]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.DELETE_POL_SUP_FORM_SHOP
--Created by         : Rajeev         
--Date               :  11 NOVEMBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[DELETE_MNT_ACCUMULATION_REFERENCE]      
CREATE  PROCEDURE [dbo].[DELETE_POL_SUP_FORM_SHOP]      
(       
  @SHOP_ID int
)        
AS       
BEGIN      
DELETE FROM POL_SUP_FORM_SHOP  WHERE SHOP_ID=@SHOP_ID
End

