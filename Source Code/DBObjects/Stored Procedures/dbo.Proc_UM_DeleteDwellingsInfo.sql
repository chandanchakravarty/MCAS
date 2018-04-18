IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_DeleteDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_DeleteDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE  PROC dbo.Proc_UM_DeleteDwellingsInfo      
(      
 @CUSTOMER_ID Int,      
 @APP_ID Int,      
 @APP_VERSION_ID Int,      
 @DWELLING_ID smallint      
       
       
)      
      
AS      
    
      
      
BEGIN         
       
 --Delete from Umbrella rating      
 IF EXISTS      
 (      
  SELECT DWELLING_ID 
  FROM APP_UMBRELLA_RATING_INFO      
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND      
   APP_ID = @APP_ID AND      
   APP_VERSION_ID = @APP_VERSION_ID AND      
   DWELLING_ID = @DWELLING_ID      
      
 )      
 BEGIN      
      
  DELETE FROM APP_UMBRELLA_RATING_INFO      
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND      
   APP_ID = @APP_ID AND      
   APP_VERSION_ID = @APP_VERSION_ID AND      
   DWELLING_ID = @DWELLING_ID       
 END           
 
 
     
 DELETE FROM APP_UMBRELLA_DWELLINGS_INFO      
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID AND      
  DWELLING_ID = @DWELLING_ID      
       
 IF @@ERROR <> 0      
 BEGIN      
  RETURN -1      
 END      
      
 RETURN 1      
END      




GO

