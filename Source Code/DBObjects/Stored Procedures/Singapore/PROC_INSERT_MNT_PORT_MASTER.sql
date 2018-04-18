  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_MNT_PORT_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].PROC_INSERT_MNT_PORT_MASTER
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO  
         
-- =============================================      
-- Author:  Kuldeep Saxena     
-- Create date: 14-MAR-2012     
-- Description: -  
--dROP PROC PROC_INSERT_MNT_PORT_MASTER  
--EXEC PROC_INSERT_MNT_PORT_MASTER NULL,'1','1','7',10.88,'2013-02-02','2013-02-02','1','1','1','2013-02-02'
-- =============================================      
      
CREATE PROCEDURE dbo.PROC_INSERT_MNT_PORT_MASTER  (  
 @PORT_CODE INT OUTPUT,   
 @ISO_CODE NVARCHAR(10) ,      
 @PORT_TYPE NVARCHAR(10),      
 @COUNTRY NVARCHAR(40),      
 @ADDITIONAL_WAR_RATE DECIMAL(18,2),      
 @FROM_DATE DATE,      
 @TO_DATE Date,      
 @SETTLEMENT_AGENT_CODE Nvarchar(10),  
 @SETTLING_AGENT_NAME NVARCHAR(25),  
 @IS_ACTIVE CHAR,
 @CREATED_BY INT,  
 @CREATED_DATETIME DATETIME   
 )  
AS      
      
BEGIN      

SELECT @PORT_CODE=ISNULL(MAX(PORT_CODE),0)+1 FROM MNT_PORT_MASTER  
INSERT INTO MNT_PORT_MASTER      
  (      
 PORT_CODE,      
 ISO_CODE,      
 PORT_TYPE,      
 COUNTRY,      
 ADDITIONAL_WAR_RATE,      
 FROM_DATE,      
 TO_DATE,      
 SETTLEMENT_AGENT_CODE,  
 SETTLING_AGENT_NAME, 
 IS_ACTIVE,     
 CREATED_BY,      
 CREATED_DATETIME      
   )      
      
VALUES       
 (      
 @PORT_CODE,  
 @ISO_CODE,      
 @PORT_TYPE,      
 @COUNTRY,      
 @ADDITIONAL_WAR_RATE,      
 @FROM_DATE,      
 @TO_DATE,      
 @SETTLEMENT_AGENT_CODE,  
 @SETTLING_AGENT_NAME,  
 @IS_ACTIVE,
 @CREATED_BY,  
 @CREATED_DATETIME   
  
 )      
  END