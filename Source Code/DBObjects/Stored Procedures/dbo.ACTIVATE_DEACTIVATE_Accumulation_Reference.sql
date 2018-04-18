IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_REFERENCE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_REFERENCE]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------
--Proc Name          : dbo.ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_REFERENCE
--Created by         : Kuldeep Saxena       
--Date               : 24 October 2011  
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_REFERENCE]      
CREATE  PROCEDURE [dbo].[ACTIVATE_DEACTIVATE_MNT_ACCUMULATION_REFERENCE]      
(       
  @ACC_ID int,
  @IS_ACTIVE Char(1)

)        
AS       
BEGIN      
UPDATE  MNT_ACCUMULATION_REFERENCE    
 SET               
    IS_ACTIVE = @IS_ACTIVE             
 WHERE              
	ACC_ID=@ACC_ID
  
End