IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SELECTACTIVITYTYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SELECTACTIVITYTYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------          
Proc Name   : DBO.PROC_SELECTACTIVITYTYPE       
Created by  : Abhinav Agarwal          
Date        : 20 October-2010    
Purpose     :           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/   
-- DROP PROC DBO.PROC_SELECTACTIVITYTYPE
CREATE  PROC [dbo].[PROC_SELECTACTIVITYTYPE]           
(            
 @TYPE INT = NULL       
 )            
AS            
BEGIN            
--Modified for itrack#1152/TFS#2598
    IF(@TYPE IS NOT NULL AND @TYPE <> '')
		SELECT DISTINCT ACTIVITY_ID,ACTIVITY_DESC,TYPE,CAST(ACTIVITY_ID AS NVARCHAR(12)) +'^'+ISNULL(RUBRICA,0) ACTIVITY_ID_RUBRICA FROM MNT_ACTIVITY_MASTER(NOLOCK) WHERE TYPE=@TYPE         
	ELSE
		SELECT DISTINCT ACTIVITY_ID,ACTIVITY_DESC,TYPE,CAST(ACTIVITY_ID AS NVARCHAR(12)) +'^'+ISNULL(RUBRICA,0) ACTIVITY_ID_RUBRICA FROM MNT_ACTIVITY_MASTER (NOLOCK)     
 END       
  

GO

