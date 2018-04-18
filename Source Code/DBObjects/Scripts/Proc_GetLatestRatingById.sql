---------------------------------------------------------------    
--Proc Name          : dbo.[Proc_GetLatestRatingById]    
--Created by         : Amit K. Mishra             
--Date               :  11 October 2011           
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[Proc_GetLatestRatingById]
CREATE PROCEDURE Proc_GetLatestRatingById    
(
@COMPANY_ID int   
)
AS           
BEGIN          
Select Top 1 RATING_ID,COMPANY_ID,COMPANY_TYPE,AGENCY_ID,RATING,
	  EFFECTIVE_YEAR,CREATED_BY,CREATED_DATETIME 
From MNT_CURRENT_RATING_LIST Where COMPANY_ID=@COMPANY_ID order by EFFECTIVE_YEAR desc
End