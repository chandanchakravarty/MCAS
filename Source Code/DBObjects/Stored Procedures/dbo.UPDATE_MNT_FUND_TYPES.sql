IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPDATE_MNT_FUND_TYPES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPDATE_MNT_FUND_TYPES]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.UPDATE_MNT_FUND_TYPES
--Created by         :  Abhinav Agarwal        
--Date               :  22 September 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[UPDATE_MNT_FUND_TYPES]      
CREATE  PROCEDURE [dbo].[UPDATE_MNT_FUND_TYPES]      
(       
  @FUND_TYPE_ID int,
  @FUND_TYPE_CODE nvarchar(20),
  @FUND_TYPE_NAME nvarchar(100),
  @FUND_TYPE_SOURCE_D BIT,
  @FUND_TYPE_SOURCE_DO BIT,
  @FUND_TYPE_SOURCE_RIO BIT,
  @FUND_TYPE_SOURCE_RIA BIT
 -- @IS_ACTIVE CHAR(1)

)        
AS                
BEGIN      
                
  If Exists(select * from MNT_FUND_TYPES where FUND_TYPE_ID=@FUND_TYPE_ID)               
  BEGIN           
   UPDATE MNT_FUND_TYPES
	SET        
	FUND_TYPE_CODE= @FUND_TYPE_CODE,
	FUND_TYPE_NAME=@FUND_TYPE_NAME,
	FUND_TYPE_SOURCE_D=@FUND_TYPE_SOURCE_D,     
	FUND_TYPE_SOURCE_DO=@FUND_TYPE_SOURCE_DO,
	FUND_TYPE_SOURCE_RIO=@FUND_TYPE_SOURCE_RIO,
	FUND_TYPE_SOURCE_RIA=@FUND_TYPE_SOURCE_RIA
	--IS_ACTIVE=@IS_ACTIVE
	
WHERE  FUND_TYPE_ID=@FUND_TYPE_ID           
       
   
        
     END
	 END