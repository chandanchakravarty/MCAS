IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACTIVATE_DEACTIVATE_MNT_FUND_TYPES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ACTIVATE_DEACTIVATE_MNT_FUND_TYPES]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.ACTIVATE_DEACTIVATE_MNT_FUND_TYPES
--Created by         :          
--Date               :  22 September 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[ACTIVATE_DEACTIVATE_MNT_FUND_TYPES]      
CREATE  PROCEDURE [dbo].[ACTIVATE_DEACTIVATE_MNT_FUND_TYPES]      
(       
  @FUND_TYPE_ID int,
  @IS_ACTIVE Char(1)

)        
AS       
BEGIN      
UPDATE  MNT_FUND_TYPES     
 SET               
   IS_ACTIVE = @IS_ACTIVE             
 WHERE              
  FUND_TYPE_ID=@FUND_TYPE_ID
  
End