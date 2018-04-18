

/****** Object:  StoredProcedure [dbo].[Proc_Get_Retention_LIMIT]    Script Date: 11/30/2011 10:51:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Retention_LIMIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Retention_LIMIT]
GO


GO

/****** Object:  StoredProcedure [dbo].[Proc_Get_Retention_LIMIT]    Script Date: 11/30/2011 10:51:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create Proc [dbo].[Proc_Get_Retention_LIMIT]    
(    
 @RETENTION_LIMIT_ID INT  
   
)    
AS    
 BEGIN     
 SELECT RL.REF_SUSEP_LOB_ID ,RL.RETENTION_LIMIT FROM MNT_RETENTION_LIMIT RL   
 WITH(NOLOCK) WHERE RETENTION_LIMIT_ID=@RETENTION_LIMIT_ID  
  
END
GO


