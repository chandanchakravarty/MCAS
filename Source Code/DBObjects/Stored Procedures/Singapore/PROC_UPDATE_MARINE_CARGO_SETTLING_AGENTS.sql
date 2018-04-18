IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_MARINE_CARGO_SETTLING_AGENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].PROC_UPDATE_MARINE_CARGO_SETTLING_AGENTS
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO      
 /*----------------------------------------------------------                    
 Proc Name       : dbo.[PROC_UPDATE_MARINE_CARGO_SETTLING_AGENTS]        
 Created by      : Avijit Goswami
 Date            : 15/03/2012
 Purpose         : 
 Revison History :                    
 Used In		 : Ebix Advantage                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------    
Drop Proc PROC_UPDATE_MARINE_CARGO_SETTLING_AGENTS*/    
Create Proc [dbo].[PROC_UPDATE_MARINE_CARGO_SETTLING_AGENTS]     
						(    
						@AGENT_ID VARCHAR(10),
						@AGENT_CODE VARCHAR(10),
						@AGENT_NAME VARCHAR(40),
						@AGENT_ADDRESS1 VARCHAR(40),
						@AGENT_ADDRESS2 VARCHAR(40),
						@AGENT_CITY VARCHAR(40),
						@AGENT_COUNTRY VARCHAR(40),
						@AGENT_SURVEY_CODE VARCHAR(40),
						@IS_ACTIVE CHAR
						)AS    
					BEGIN
					UPDATE MARINE_CARGO_SETTLING_AGENTS SET
					--AGENT_ID=@AGENT_ID,
					AGENT_CODE=@AGENT_CODE,
					AGENT_NAME=@AGENT_NAME,
					AGENT_ADDRESS1=@AGENT_ADDRESS1,
					AGENT_ADDRESS2=@AGENT_ADDRESS2,
					AGENT_CITY=@AGENT_CITY,
					AGENT_COUNTRY=@AGENT_COUNTRY,
					AGENT_SURVEY_CODE=@AGENT_SURVEY_CODE,
					IS_ACTIVE=@IS_ACTIVE     
					WHERE AGENT_ID = @AGENT_ID
    IF(@@ERROR<>0)    
    RETURN -1    
     
     
END  