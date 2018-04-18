
    
---------------------------------------------------------------    
--Proc Name          : dbo.[Proc_GetRatingById]    
--Created by         : AMIT k. Mishra             
--Date               :  11 October 2011           
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[Proc_GetRatingById]          
CREATE  PROCEDURE [dbo].[Proc_GetRatingById]    
@RATING_ID int   
AS           
BEGIN          
Select RATING_LOG_ID,RATING_ID,AGENCY_ID,RATED,EFFECTIVE_YEAR,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME     
From MNT_RATING_LIST_LOG Where RATING_LOG_ID=@RATING_ID
End


