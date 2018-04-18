IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchBoatInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchBoatInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*    
Proc Name      : dbo.Proc_FetchBoatInfo        
Created by       : Anurag Verma        
Date             : 5/18/2005        
Purpose       : retrieving data from APP_WATERCRAFT_INFO         
Revison History :        
Used In        : Wolverine        
        
Modified By : Anurag Verma        
Modified On : 11/10/2005        
Purpose  : adding BOAT_ID field in where clause        
      
Modified By : Vijay Arora      
Modified On : 17/10/2005        
Purpose  : change in select clause.      
*/      
--drop proc Proc_FetchBoatInfo        
CREATE PROC dbo.Proc_FetchBoatInfo        
@CUSTOMER_ID INT,        
@APP_ID INT,        
@APP_VERSION_ID INT,        
@BOATID int=null        
AS        
if @BOATID is null         
 BEGIN        
  --SELECT BOAT_ID,CONVERT(VARCHAR(4),isnull(BOAT_NO,0),1) BOAT  FROM APP_WATERCRAFT_INFO WHERE         
        
 SELECT BOAT_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS BOAT  FROM APP_WATERCRAFT_INFO WHERE         
  APP_ID=@APP_ID AND         
  APP_VERSION_ID=@APP_VERSION_ID        
  AND CUSTOMER_ID=@CUSTOMER_ID AND IS_ACTIVE = 'Y'    
  
 SELECT (ISNULL(MAKE,'')  + ' ' + ISNULL(MODEL,'') + ' ' + ISNULL(SERIAL,'') + '(' + CAST(ISNULL(YEAR,'') AS VARCHAR) + ')')  
  AS REC_VEH, REC_VEH_ID  
 FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES   
 WHERE         
  APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND CUSTOMER_ID=@CUSTOMER_ID AND ACTIVE = 'Y'    
         
 END        
else        
 begin        
 -- SELECT BOAT_ID,CONVERT(VARCHAR(4),isnull(BOAT_NO,0),1) BOAT  FROM APP_WATERCRAFT_INFO WHERE         
  SELECT BOAT_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS BOAT FROM APP_WATERCRAFT_INFO WHERE         
   APP_ID=@APP_ID AND         
  APP_VERSION_ID=@APP_VERSION_ID        
  AND CUSTOMER_ID=@CUSTOMER_ID         
  and BOAT_ID=@BOATID AND  IS_ACTIVE = 'Y'        
end        
      
  



GO

