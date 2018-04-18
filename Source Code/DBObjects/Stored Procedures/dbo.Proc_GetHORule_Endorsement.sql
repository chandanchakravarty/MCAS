IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_Endorsement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_Endorsement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                  
Proc Name                : Dbo.Proc_GetHORule_Endorsement 920,505,1,1,'d'                                                               
Created by               : Praveen Kasana                                                                  
Date                     : 20 June 2006                 
Purpose                  : To get the Endorsement details for HO rules                  
Revison History          :                                                                  
Used In                  : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/     
-- DROP PROC Proc_GetHORule_Endorsement                                                               
CREATE proc dbo.Proc_GetHORule_Endorsement                  
(                                                                  
@CUSTOMERID    int,                                                                  
@APPID    int,                                                                  
@APPVERSIONID   int,        
@DWELLINGID int,                
@DESC varchar(10)                                                    
)                                                                  
as                                                                      
BEGIN           
        
DECLARE @OTHERS_LOCATION CHAR        
        
DECLARE @RENTED_OTHERS CHAR        
--ENDORSEMENT # 70 ADDITIONAL RESIDENCE RENTED TO OTHERS IS CHECKED OFF THEN         
--MAKE SURE THEY HAVE COMPLETED A LEAST ONE LINE OF DETAIL UNDER THE OTHER LOCATIONS / LIABILITY         
IF EXISTS(SELECT ENDORSEMENT_ID FROM APP_DWELLING_ENDORSEMENTS         
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID AND ENDORSEMENT_ID IN(168,218,323,324,325,326)) --HO 40(IN/MI) AND HO-70        
BEGIN        
 SET @RENTED_OTHERS='Y'        
END        
ELSE        
BEGIN        
 SET @RENTED_OTHERS='N'        
END        
   
IF(@RENTED_OTHERS='Y') -- IF ENDORSEMENT IS PRESENT CHECK FOR THE OTHER LOCATION TABLE         
BEGIN        
 IF EXISTS (SELECT LOCATION_ID FROM APP_OTHER_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMERID and APP_ID = @APPID         
 and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID and IS_ACTIVE='Y')        
 BEGIN        
  SET @OTHERS_LOCATION='N'        
 END        
 ELSE        
 BEGIN        
  SET @OTHERS_LOCATION='Y'        
 END        
                 
END        
ELSE        
BEGIN        
SET @OTHERS_LOCATION='N'        
END        
        
 SELECT          
 @OTHERS_LOCATION as OTHERS_LOCATION          
            
END                                        

  


GO

