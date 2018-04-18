IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_MNT_PORT_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_MNT_PORT_MASTER]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------    
--Proc Name          : dbo.[Proc_Get_MNT_PORT_MASTER]    
--Created by         :     Kuldeep Saxena         
--Date               :  14-Mar-2012           
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[Proc_Get_MNT_PORT_MASTER]          
CREATE  PROCEDURE [dbo].[Proc_Get_MNT_PORT_MASTER]          
 (
 @PORT_CODE INT
 )         
AS           
BEGIN          
Select PORT_CODE,    
 ISO_CODE,    
 PORT_TYPE,    
 COUNTRY,    
 ADDITIONAL_WAR_RATE,    
 CONVERT(VARCHAR(10), FROM_DATE, 103) as FROM_DATE,    
 CONVERT(VARCHAR(10), TO_DATE, 103) as TO_DATE,     
 SETTLEMENT_AGENT_CODE,
 SETTLING_AGENT_NAME    
 FROM MNT_PORT_MASTER  where PORT_CODE=@PORT_CODE   
End

