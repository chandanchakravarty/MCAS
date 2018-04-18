IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DELETE_MNT_ACCUMULATION_CRITERIA_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DELETE_MNT_ACCUMULATION_CRITERIA_MASTER]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------
--Proc Name          : dbo.DELETE_MNT_ACCUMULATION_CRITERIA_MASTER
--Created by         : Kuldeep Saxena         
--Date               :  24 OCTOBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[DELETE_MNT_ACCUMULATION_CRITERIA_MASTER]      
CREATE  PROCEDURE [dbo].[DELETE_MNT_ACCUMULATION_CRITERIA_MASTER]      
(       
  @CRITERIA_ID int
)        
AS       
BEGIN      
DELETE FROM MNT_ACCUMULATION_CRITERIA_MASTER  WHERE CRITERIA_ID=@CRITERIA_ID
End

