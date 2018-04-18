IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLobList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLobList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetLobList            
CREATED BY  : Swarup       
CREATED DATE : August 16, 2007         
Purpose         :        
Revison History :            
Used In         : Wolvorine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/      
--DROP PROC dbo.Proc_GetLobList 21,2          
CREATE PROC [dbo].[Proc_GetLobList]           
(            
 @LOB nvarchar(1000) ,
 @lang_id int=1           
)            
AS            
BEGIN     
  declare @strsql varchar(1000)    
  set @strsql =' select isnull(mlv.LOB_DESC,MNT_LOB_MASTER.LOB_DESC)LOB_DESC from MNT_LOB_MASTER 
  left outer join  MNT_LOB_MASTER_MULTILINGUAL mlv on mlv.LOB_ID=MNT_LOB_MASTER.LOB_ID and mlv.LANG_ID ='+CAST( @lang_id as nvarchar)+'
  where (MNT_LOB_MASTER.LOB_ID) in ('+@LOB+') '  
           
-- select LOOKUP_VALUE_DESC from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID in (@CATEGORY)        
  exec (@strsql)    
END            
            
            
          
        
      
 
  
  
GO

