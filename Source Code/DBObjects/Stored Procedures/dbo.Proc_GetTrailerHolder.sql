IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTrailerHolder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTrailerHolder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetTrailerHolder  
Created by           : Anurag Verma  
Date                    : 23/05/2005  
Purpose               : TO RETRIEVE HOLDER ID AND HOLDER  NAME  
Revison History :  
Used In                :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_GetTrailerHolder
CREATE  PROC Dbo.Proc_GetTrailerHolder  
(  
@TRAILER_ID  varchar(4)  
)  
AS  
select a.holder_id, a.holder_name   
from mnt_holder_interest_list a  
where  a.holder_id not in(select holder_id from POL_WATERCRAFT_TRAILER_ADD_INT WITH(NOLOCK) WHERE TRAILER_ID= @TRAILER_ID)  
  
GO

