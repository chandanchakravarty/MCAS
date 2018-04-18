IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('INSERT_MNT_ACCUMULATION_CRITERIA_MASTER') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[INSERT_MNT_ACCUMULATION_CRITERIA_MASTER]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.INSERT_MNT_ACCUMULATION_CRITERIA_MASTER
--Created by         : Kuldeep Saxena         
--Date               :  24 OCTOBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[INSERT_MNT_FUND_TYPES]      
CREATE  PROCEDURE [dbo].[INSERT_MNT_ACCUMULATION_CRITERIA_MASTER]      
( 
	@CRITERIA_ID INT OUTPUT,      
  @CRITERIA_CODE nvarchar(25),
  @CRITERIA_DESC nvarchar(100),
  @LOB_ID INT,
  @IS_ACTIVE char(1)

)        
AS       
BEGIN   
           
SELECT @CRITERIA_ID = isnull(Max(CRITERIA_ID),0)+1 FROM MNT_ACCUMULATION_CRITERIA_MASTER
  INSERT INTO MNT_ACCUMULATION_CRITERIA_MASTER                 
  (                 
  CRITERIA_ID,
  CRITERIA_CODE,
  CRITERIA_DESC, 
  LOB_ID,
  IS_ACTIVE
                 
  )                  
  VALUES                  
  (      
  @CRITERIA_ID,
  @CRITERIA_CODE,
  @CRITERIA_DESC,
  @LOB_ID,
  @IS_ACTIVE
    )     
End


