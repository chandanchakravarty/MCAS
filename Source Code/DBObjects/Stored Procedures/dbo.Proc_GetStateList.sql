IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetStateList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetStateList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetStateList          
CREATED BY  : Swarup     
CREATED DATE : August 16, 2007       
Purpose         :      
Revison History :          
Used In         : Wolvorine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/    
--DROP PROC dbo.Proc_GetStateList '14182',14189'        
CREATE PROC dbo.Proc_GetStateList          
(          
 @STATE nvarchar(500)          --Changed by Aditya for TFS Bug # 763
)          
AS          
BEGIN   
  declare @strsql varchar(1000)  
  set @strsql = 'select STATE_NAME from MNT_COUNTRY_STATE_LIST where STATE_ID in (' + @STATE + ')'  
         
-- select LOOKUP_VALUE_DESC from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID in (@CATEGORY)      
  exec (@strsql)  
END          
          
          
        
    
  




GO

