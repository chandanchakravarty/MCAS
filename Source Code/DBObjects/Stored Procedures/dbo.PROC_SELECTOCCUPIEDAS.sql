IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SELECTOCCUPIEDAS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SELECTOCCUPIEDAS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name   : DBO.PROC_SELECTOCCUPIEDAS    
Created by  : Abhinav Agarwal          
Date        : 20 October-2010    
Purpose     :           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/   
-- DROP PROC DBO.PROC_SELECTOCCUPIEDAS
CREATE  PROC [dbo].[PROC_SELECTOCCUPIEDAS] 
(
--Added for itrack#1152/TFS#2598
@RUBRICA NVARCHAR(5)=NULL,
@OCCUPIED_ID INT=NULL,
@CALLED_FOR NVARCHAR(25)=NULL
)
AS   
BEGIN 

	IF(@CALLED_FOR='RUBRICA')
	BEGIN
		SELECT @RUBRICA=RUBRICA FROM MNT_OCCUPIED_MASTER(NOLOCK) WHERE OCCUPIED_ID=@OCCUPIED_ID
		SELECT OCCUPIED_ID,OCCUPIED_AS,RUBRICA FROM MNT_OCCUPIED_MASTER(NOLOCK)  WHERE RUBRICA =@RUBRICA
	END	
    ELSE
		SELECT OCCUPIED_ID,OCCUPIED_AS FROM MNT_OCCUPIED_MASTER(NOLOCK)  WHERE RUBRICA=@RUBRICA
END  
GO

