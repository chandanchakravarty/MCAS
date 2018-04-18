IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNewOrderNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNewOrderNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name           : dbo.Proc_GetNewOrderNo          
Created by          : Swarup Pal          
Date                : 25th May '07          
Purpose             : To get the new order no.         
Revison History     :          
Used In             :   Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
--DROP PROC dbo.Proc_GetNewOrderNo     
CREATE  PROC dbo.Proc_GetNewOrderNo 
(
   @STATE_ID INT = null,
   @LOB_ID varchar(20) = null
)     
  
AS          
BEGIN       
 DECLARE @RANK AS int       
 IF EXISTS (SELECT COV_ID FROM MNT_COVERAGE )       
     BEGIN    
  IF(@STATE_ID IS NOT NULL AND @LOB_ID IS NOT NULL)
	
     SELECT (isnull(MAX(CONVERT(integer,RANK)),0))+1 as RANK FROM MNT_COVERAGE        
 		WHERE STATE_ID =  @STATE_ID AND CONVERT(VARCHAR, LOB_ID) = @LOB_ID        
 ELSE
     SELECT (isnull(MAX(CONVERT(integer,RANK)),0))+1 as RANK FROM MNT_COVERAGE 
    
     END    
  ELSE    
     BEGIN     
    SELECT 1 AS RANK    
     END     
END         
    
  



GO

