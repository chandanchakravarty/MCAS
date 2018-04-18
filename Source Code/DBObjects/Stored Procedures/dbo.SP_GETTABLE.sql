IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GETTABLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GETTABLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC SP_GETTABLE 
(                        
@COLNAME  VARCHAR(100)                        
     
)                        
AS                        
                        
BEGIN   
 /*  
  Stored Procedure To Find the name of the Tables containing columns like  
 */  

SELECT OBJ.NAME,COL.NAME  FROM SYSCOLUMNS COL  INNER JOIN SYSOBJECTS OBJ ON COL.ID = OBJ.ID
WHERE OBJ.XTYPE = 'U' AND COL.NAME LIKE '%' + @COLNAME + '%'

END
/*  
 sp_gettable 'down'  
*/



GO

