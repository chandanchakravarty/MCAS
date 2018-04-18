IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOBDescription]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOBDescription]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------          
Proc Name       : Proc_GetLOBDesciption          
Created by      : kranti        
Date            : 21th May 2007        
Purpose         : Get LOB Description for LOB CODE        
Revison History :          
        
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       --------------------------------*/          
-- DROP PROC dbo.Proc_GetLOBDescription          
CREATE PROCEDURE [dbo].[Proc_GetLOBDescription]          
(          
 @APP_LOB  varchar(6),
 @LANG_ID int =1
)        
AS        
 select ISNULL(mlmm.lob_desc ,mlm.LOB_DESC)
 from MNT_LOB_MASTER mlm left join MNT_LOB_MASTER_MULTILINGUAL mlmm on mlm.LOB_ID=mlmm.LOB_ID and mlmm.LANG_ID=@LANG_ID 
 where lob_code = @APP_LOB     
  
  

GO

