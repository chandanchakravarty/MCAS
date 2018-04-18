IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DELETE_POL_SUP_FORM_RESTAURANT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DELETE_POL_SUP_FORM_RESTAURANT]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.DELETE_POL_SUP_FORM_RESTAURANT
--Created by         : Rajeev         
--Date               :  11 NOVEMBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[DELETE_MNT_ACCUMULATION_REFERENCE]      
CREATE  PROCEDURE [dbo].[DELETE_POL_SUP_FORM_RESTAURANT]      
(       
  @RESTAURANT_ID int
)        
AS       
BEGIN      
DELETE FROM POL_SUP_FORM_RESTAURANT  where RESTAURANT_ID=@RESTAURANT_ID
End

