IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectMarketeer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectMarketeer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------          
Proc Name   : dbo.Proc_SelectMarketeer         
Created by  : Shafi          
Date        : 21 June 2006    
Purpose     :           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/   
-- DROP PROC dbo.Proc_SelectMarketeer         
CREATE PROCEDURE [dbo].[Proc_SelectMarketeer]       
AS         
BEGIN          
 
 DECLARE @CARRIER_CODE nvarchar(10)      
       
 --Added by Charles on 24-May-2010 for Itrack 51        
SELECT  @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,'') FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK)         
INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID = SYSP.SYS_CARRIER_ID 
 
 SELECT USER_ID,USER_FNAME+' '+USER_LNAME AS USER_NAME FROM MNT_USER_LIST MUL WITH(NOLOCK)   
 INNER JOIN MNT_USER_TYPES MUT WITH(NOLOCK)   
 ON MUL.USER_TYPE_ID=MUT.USER_TYPE_ID   
 WHERE USER_SYSTEM_ID=@CARRIER_CODE
 ORDER BY USER_FNAME ASC  
End    
  
  
  
  
  
  
GO

