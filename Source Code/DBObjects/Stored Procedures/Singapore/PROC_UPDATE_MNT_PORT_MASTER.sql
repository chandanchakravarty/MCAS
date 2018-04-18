  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_MNT_PORT_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].PROC_UPDATE_MNT_PORT_MASTER
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO      
-- =============================================      
-- Author:  Kuldeep Saxena     
-- Create date: 14-MAR-2012     
-- Description: -  
--drop proc    PROC_update_MNT_PORT_MASTER
--EXEC PROC_update_MNT_PORT_MASTER 2,'1','1','77',10.88,'2013-02-02','2013-02-02','1','1','Y','1','2013-02-02'
-- =============================================    
    
CREATE PROCEDURE [dbo].PROC_UPDATE_MNT_PORT_MASTER   
   @PORT_CODE INT,   
	@ISO_CODE NVARCHAR(10) ,      
	@PORT_TYPE NVARCHAR(10),      
	@COUNTRY NVARCHAR(40),      
	@ADDITIONAL_WAR_RATE DECIMAL(18,2),      
	@FROM_DATE Date,      
	@TO_DATE Date,      
	@SETTLEMENT_AGENT_CODE Nvarchar(10),  
	@SETTLING_AGENT_NAME NVARCHAR(25),  
 
    @MODIFIED_BY int,    
    @LAST_UPDATED_DATETIME datetime    
AS    
    
BEGIN    
    
UPDATE [MNT_PORT_MASTER]    
    
SET    
    
    
	ISO_CODE=@ISO_CODE,      
	PORT_TYPE=@PORT_TYPE,      
	COUNTRY=@COUNTRY,      
	ADDITIONAL_WAR_RATE=@ADDITIONAL_WAR_RATE,      
	FROM_DATE=@FROM_DATE,      
	TO_DATE=@TO_DATE,      
	SETTLEMENT_AGENT_CODE=@SETTLEMENT_AGENT_CODE,  
	SETTLING_AGENT_NAME=@SETTLING_AGENT_NAME, 
	       
    MODIFIED_BY = @MODIFIED_BY,    
    LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME    
    
    
Where      
    
 PORT_CODE = @PORT_CODE      
END    