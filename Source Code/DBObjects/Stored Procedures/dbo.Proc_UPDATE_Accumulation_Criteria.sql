IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPDATE_MNT_ACCUMULATION_CRITERIA_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPDATE_MNT_ACCUMULATION_CRITERIA_MASTER]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.UPDATE_MNT_ACCUMULATION_CRITERIA_MASTER
--Created by         : Kuldeep Saxena         
--Date               : 24 OCTOBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/        
-- drop proc dbo.[UPDATE_MNT_ACCUMULATION_CRITERIA_MASTER]      
CREATE  PROCEDURE [dbo].[UPDATE_MNT_ACCUMULATION_CRITERIA_MASTER]      
(       
  @CRITERIA_ID int,
  @CRITERIA_CODE nvarchar(25),
  @CRITERIA_DESC nvarchar(100),
  @LOB_ID INT
 
)        
AS                
BEGIN      
        
   UPDATE MNT_ACCUMULATION_CRITERIA_MASTER
	SET        
	CRITERIA_CODE=@CRITERIA_CODE,
	CRITERIA_DESC =@CRITERIA_DESC,
	LOB_ID=@LOB_ID

WHERE  CRITERIA_ID=@CRITERIA_ID          
   
     END
