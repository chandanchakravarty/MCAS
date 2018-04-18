IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_MNT_FUND_TYPES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_MNT_FUND_TYPES]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.GET_MNT_FUND_TYPES
--Created by         :          
--Date               :  22 September 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[GET_MNT_FUND_TYPES]      
CREATE  PROCEDURE [dbo].[GET_MNT_FUND_TYPES]      
(       
 @FUND_TYPE_ID int
)        
AS       
BEGIN      
Select FUND_TYPE_ID,FUND_TYPE_CODE,FUND_TYPE_NAME,FUND_TYPE_SOURCE_D,FUND_TYPE_SOURCE_DO,FUND_TYPE_SOURCE_RIO,FUND_TYPE_SOURCE_RIA,IS_ACTIVE 
from MNT_FUND_TYPES 
where FUND_TYPE_ID=@FUND_TYPE_ID
End