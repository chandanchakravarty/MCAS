IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCoverageCatagory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCoverageCatagory]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetCoverageCatagory        
CREATED BY  : Swarup   
CREATED DATE : August 16, 2007     
Purpose         :    
Revison History :        
Used In         : Wolvorine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/  
--DROP PROC dbo.Proc_GetCoverageCatagory '14182,14189'      
CREATE PROC dbo.Proc_GetCoverageCatagory        
(        
 @CATEGORY nvarchar(50)        
)        
AS        
BEGIN 
  declare @strsql varchar(1000)
  set @strsql = 'select LOOKUP_VALUE_DESC from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID in (' + @CATEGORY + ')'
       
-- select LOOKUP_VALUE_DESC from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID in (@CATEGORY)    
  exec (@strsql)
END        
        
        
      
    
  



GO

