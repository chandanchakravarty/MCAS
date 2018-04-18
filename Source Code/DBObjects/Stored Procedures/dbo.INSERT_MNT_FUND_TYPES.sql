IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[INSERT_MNT_FUND_TYPES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[INSERT_MNT_FUND_TYPES]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.INSERT_MNT_FUND_TYPES
--Created by         :    Abhinav Agarwal      
--Date               :  22 September 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[INSERT_MNT_FUND_TYPES]      
CREATE  PROCEDURE [dbo].[INSERT_MNT_FUND_TYPES]      
(       
  @FUND_TYPE_CODE nvarchar(20),
  @FUND_TYPE_NAME nvarchar(100),
  @FUND_TYPE_SOURCE_D BIT,
  @FUND_TYPE_SOURCE_DO BIT,
  @FUND_TYPE_SOURCE_RIO BIT,
  @FUND_TYPE_SOURCE_RIA BIT,
  @IS_ACTIVE Char(1),
  @FUND_TYPE_ID INT OUTPUT 

)        
AS       
BEGIN   

--DECLARE @FUND_TYPE_ID INT     
                  
SELECT @FUND_TYPE_ID = isnull(Max(FUND_TYPE_ID),0)+1 FROM MNT_FUND_TYPES         
IF NOT EXISTS(Select * from MNT_FUND_TYPES where FUND_TYPE_ID=@FUND_TYPE_ID)
BEGIN                 
  INSERT INTO MNT_FUND_TYPES                    
  (                 
  FUND_TYPE_ID,
  FUND_TYPE_CODE,
  FUND_TYPE_NAME,
  FUND_TYPE_SOURCE_D,
  FUND_TYPE_SOURCE_DO,
  FUND_TYPE_SOURCE_RIO,
  FUND_TYPE_SOURCE_RIA,
  IS_ACTIVE
  
   
                 
  )                  
  VALUES                  
  (      
  @FUND_TYPE_ID,
  @FUND_TYPE_CODE,
  @FUND_TYPE_NAME,
  @FUND_TYPE_SOURCE_D,
  @FUND_TYPE_SOURCE_DO,
  @FUND_TYPE_SOURCE_RIO,
  @FUND_TYPE_SOURCE_RIA,
  @IS_ACTIVE
    )     
End
End