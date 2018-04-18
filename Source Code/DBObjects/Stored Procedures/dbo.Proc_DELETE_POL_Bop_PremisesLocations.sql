IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DELETE_POL_BOP_PREMISESLOCATIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DELETE_POL_BOP_PREMISESLOCATIONS]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.DELETE_POL_BOP_PREMISESLOCATIONS
--Created by         : Rajeev         
--Date               :  21 NOVEMBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[DELETE_MNT_ACCUMULATION_REFERENCE]      
CREATE  PROCEDURE [dbo].[DELETE_POL_BOP_PREMISESLOCATIONS]      
(       
  @PREMLOC_ID int
)        
AS       
BEGIN      
DELETE FROM POL_BOP_PREMISESLOCATIONS  WHERE @PREMLOC_ID=@PREMLOC_ID
End

