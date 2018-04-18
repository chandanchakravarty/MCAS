IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_MNT_ACCUMULATION_CRITERIA_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_MNT_ACCUMULATION_CRITERIA_MASTER]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------
--Proc Name          : dbo.GET_MNT_ACCUMULATION_CRITERIA_MASTER
--Created by         : Kuldeep Saxena         
--Date               :  24 OCTOBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[GET_MNT_ACCUMULATION_CRITERIA_MASTER]      
CREATE  PROCEDURE [dbo].[GET_MNT_ACCUMULATION_CRITERIA_MASTER]      
(       
 @CRITERIA_ID int
)        
AS       
BEGIN      
Select CRITERIA_ID, CRITERIA_CODE, CRITERIA_DESC,LOB_ID,IS_ACTIVE
from MNT_ACCUMULATION_CRITERIA_MASTER
where CRITERIA_ID=@CRITERIA_ID
End