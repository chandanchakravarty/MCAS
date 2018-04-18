IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchSubLOBData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchSubLOBData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------  
Proc Name      : dbo.Proc_FetchSubLOBData  
Created by       : Anurag Verma  
Date             : 5/25/2005  
Purpose       : retrieving data from MNT_SUB_LOB_MASTER based on LOB_ID  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_FetchSubLOBData  
CREATE PROC [dbo].[Proc_FetchSubLOBData]  
@LOBID INT, 
@Lang_Id int=1 
AS  
BEGIN  
SELECT Sublob.SUB_LOB_ID,ISNULL(SublobM.SUB_LOB_DESC,SUBLOB.SUB_LOB_DESC)AS SUB_LOB_DESC 
FROM MNT_SUB_LOB_MASTER Sublob
left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL  SUBLOBM with(nolock)    
on SUBLOB.LOB_ID=SUBLOBM.LOB_ID     
and SUBLOB.SUB_LOB_ID=SUBLOBM.SUB_LOB_ID     
AND SUBLOBM.LOB_ID = @LOBID AND SUBLOBM.Lang_Id=@Lang_Id    
    
WHERE (SUBLOB.LOB_ID = @LOBID)   ORDER BY  SUB_LOB_DESC    
END 
    



  
GO

