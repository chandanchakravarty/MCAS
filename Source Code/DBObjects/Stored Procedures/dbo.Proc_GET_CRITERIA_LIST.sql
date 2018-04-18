IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_MNT_ACCUMULATION_CRITERIA_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_CRITERIA_LIST]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.GET_CRITERIA_LIST
--Created by         : Kuldeep Saxena         
--Date               :  24 OCTOBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[GET_MNT_ACCUMULATION_CRITERIA_MASTER]      
CREATE  PROCEDURE [dbo].[GET_CRITERIA_LIST]    
(                
@LOB_ID int
)                
            
AS                
            
BEGIN            
 SELECT CRITERIA_DESC,CRITERIA_ID FROM MNT_ACCUMULATION_CRITERIA_MASTER WHERE LOB_ID=@LOB_ID
END   

