IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_CRITERIA_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_CRITERIA_MASTER]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------
--Proc Name          : dbo.ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_CRITERIA_MASTER
--Created by         : Kuldeep Saxena       
--Date               : 24 October 2011  
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_CRITERIA_MASTER]      
CREATE  PROCEDURE [dbo].[ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_CRITERIA_MASTER]      
(       
  @CRITERIA_ID int,
  @IS_ACTIVE Char(1)

)        
AS       
BEGIN      
UPDATE  MNT_ACCUMULATION_CRITERIA_MASTER     
 SET               
    IS_ACTIVE = @IS_ACTIVE             
 WHERE              
    CRITERIA_ID=@CRITERIA_ID
  End