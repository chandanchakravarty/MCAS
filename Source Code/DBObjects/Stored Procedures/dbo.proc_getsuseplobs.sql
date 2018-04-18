IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_getsuseplobs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_getsuseplobs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------  
Proc Name: Dbo.proc_getsuseplobs  
Created by : PRAVEER PANGHAL   
Date : 09/02/2011  
Purpose  : To get SUSEPLOB  from MNT_SUSEP_LOB_MASTER table  
Revison History :  
Used In : EBIX ADVANTAGE  
------------------------------------------------------------  
Date     Review By          Comments  
DROP PROC proc_getsuseplobs  2
------   ------------       -------------------------*/  
 

 
 
 CREATE PROC [dbo].[proc_getsuseplobs]  
 (
 @LANG_ID  int=1     
 )
 AS 
 BEGIN
 SELECT ISNULL(M1.SUSEP_LOB_ID,M.SUSEP_LOB_ID)SUSEP_LOB_ID,M.SUSEP_LOB_CODE,ISNULL(M1.SUSEP_LOB_DESC,M.SUSEP_LOB_DESC) SUSEP_LOB_DESC
  from MNT_SUSEP_LOB_MASTER M WITH(NOLOCK)
LEFT  OUTER JOIN  MNT_SUSEP_LOB_MASTER_MULTILINGUAL M1 WITH(NOLOCK) 
ON  M.SUSEP_LOB_ID=M1.SUSEP_LOB_ID AND M1.LANG_ID=@LANG_ID ORDER BY SUSEP_LOB_DESC

END

 

GO

