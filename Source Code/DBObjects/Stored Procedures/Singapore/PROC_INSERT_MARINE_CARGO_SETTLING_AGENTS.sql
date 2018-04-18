--BEGIN TRAN
--DROP PROC PROC_INSERT_MARINE_CARGO_SETTLING_AGENTS
--GO

--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_MARINE_CARGO_SETTLING_AGENTS]') AND type in (N'P', N'PC'))
--DROP PROCEDURE dbo.PROC_INSERT_MARINE_CARGO_SETTLING_AGENTS
--GO
--SET ANSI_NULLS ON 
--GO
--SET QUOTED_IDENTIFIER ON
--GO       
-- =============================================    
-- Author:  Avijit Goswami
-- Create date: 08-march-2012   
-- Description: -    
-- =============================================    
    
CREATE PROCEDURE dbo.PROC_INSERT_MARINE_CARGO_SETTLING_AGENTS

						@AGENT_ID INT OUTPUT,
						@AGENT_CODE VARCHAR(10),
						@AGENT_NAME VARCHAR(40),
						@AGENT_ADDRESS1 VARCHAR(40),
						@AGENT_ADDRESS2 VARCHAR(40),
						@AGENT_CITY VARCHAR(40),
						@AGENT_COUNTRY VARCHAR(40),
						@AGENT_SURVEY_CODE VARCHAR(40),
						@IS_ACTIVE CHAR

				AS    
				BEGIN
				SELECT @AGENT_ID = IsNull(Max(AGENT_ID),0) + 1 FROM MARINE_CARGO_SETTLING_AGENTS 
				INSERT INTO MARINE_CARGO_SETTLING_AGENTS
				(    
				 AGENT_ID,
				 AGENT_CODE,
				 AGENT_NAME,
				 AGENT_ADDRESS1,
				 AGENT_ADDRESS2,
				 AGENT_CITY,
				 AGENT_COUNTRY,
				 AGENT_SURVEY_CODE,
				 IS_ACTIVE
				 )    
    
				VALUES     
						(  
					 @AGENT_ID,
					 @AGENT_CODE,
					 @AGENT_NAME,
					 @AGENT_ADDRESS1,
					 @AGENT_ADDRESS2,
					 @AGENT_CITY,
					 @AGENT_COUNTRY,
					 @AGENT_SURVEY_CODE,
					 @IS_ACTIVE
					)    
  
END    
GO

