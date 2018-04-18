IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DELETE_MNT_FUND_TYPES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DELETE_MNT_FUND_TYPES]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.DELETE_MNT_FUND_TYPES
--Created by         :          
--Date               :  22 September 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[DELETE_MNT_FUND_TYPES]          
CREATE  PROCEDURE [dbo].[DELETE_MNT_FUND_TYPES]      
(       
  @FUND_TYPE_ID int
)        
AS       
BEGIN      
DELETE FROM MNT_FUND_TYPES  WHERE FUND_TYPE_ID =@FUND_TYPE_ID 
End