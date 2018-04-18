IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftHolder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftHolder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetWatercraftHolder  
Created by         : Anurag Verma  
Date                    : 20/05/2005  
Purpose               : TO RETRIEVE HOLDER ID AND HOLDER  NAME  
Revison History :  
Used In                :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
----DROP PROC dbo.Proc_GetWatercraftHolder       
CREATE  PROC [dbo].[Proc_GetWatercraftHolder]  
(  
@BOAT_ID  varchar(4)  
)  
AS  
select a.holder_id, a.holder_name   
from mnt_holder_interest_list a  
where  a.holder_id not in(select holder_id from POL_WATERCRAFT_COV_ADD_INT WHERE BOAT_ID= @BOAT_ID)  
  
  
  
GO

