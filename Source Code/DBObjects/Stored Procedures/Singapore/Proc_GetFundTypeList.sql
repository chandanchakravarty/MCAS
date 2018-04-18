IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetFundTypeList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetFundTypeList]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------    
--Proc Name          : dbo.[Proc_GetFundTypeList]    
--Created by         :              
--Date               :  11 October 2011           
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[Proc_GetFundTypeList]          
CREATE  PROCEDURE [dbo].[Proc_GetFundTypeList]          
          
AS           
BEGIN          
Select FUND_TYPE_ID,FUND_TYPE_CODE,FUND_TYPE_NAME,FUND_TYPE_SOURCE_D,FUND_TYPE_SOURCE_DO,FUND_TYPE_SOURCE_RIO,FUND_TYPE_SOURCE_RIA,IS_ACTIVE     
from MNT_FUND_TYPES where IS_ACTIVE<> 'N'    
End