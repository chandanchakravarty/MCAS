IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_Get_Location_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_Get_Location_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE dbo.Proc_UM_Get_Location_Details
(    
 @LOCATION_ID int   
)        
AS             

BEGIN                  

SELECT 
  CONVERT(VARCHAR(20),CLIENT_LOCATION_NUMBER) + ' - ' +            
  ISNULL(ADDRESS_1,'') + ' ' + ISNULL(ADDRESS_2,'') + ', ' +           
  ISNULL(CITY,'') + ', ' +           
         CONVERT(VARCHAR(20),ISNULL(STATE,'')) + ' ' +           
         ISNULL(ZIPCODE,'')       
       
   as Address--,           
 FROM APP_UMBRELLA_REAL_ESTATE_LOCATION 
 WHERE LOCATION_ID=@LOCATION_ID


End  
  



GO

