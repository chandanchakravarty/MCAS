IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_MARINE_CARGO_SETTLING_AGENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_MARINE_CARGO_SETTLING_AGENTS]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------    
--Proc Name          : dbo.[Proc_Get_MARINE_CARGO_SETTLING_AGENTS]    
--Created by         : Avijit Goswami         
--Date               : 16-Mar-2012           
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[Proc_Get_MARINE_CARGO_SETTLING_AGENTS]          
CREATE  PROCEDURE [dbo].[Proc_Get_MARINE_CARGO_SETTLING_AGENTS]          
 (
@AGENT_ID varchar(10)
)         
AS           
BEGIN          
Select 
AGENT_ID,
AGENT_CODE,
AGENT_NAME,
AGENT_ADDRESS1,
AGENT_ADDRESS2,
AGENT_CITY,
AGENT_COUNTRY,
AGENT_SURVEY_CODE
    
 FROM MARINE_CARGO_SETTLING_AGENTS  where AGENT_ID=@AGENT_ID   
End
          

