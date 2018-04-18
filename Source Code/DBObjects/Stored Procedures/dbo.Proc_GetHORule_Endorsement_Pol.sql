IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_Endorsement_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_Endorsement_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------                                                                  
Proc Name                : Dbo.Proc_GetHORule_Endorsement_Pol 920,505,1,1,'d'                                                               
Created by               : Manoj Rathore                                                                  
Date                     : 9th  April 2007                 
Purpose                  : To get the Endorsement details for HO rules                  
Revison History          :                                                                  
Used In                  : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/     
-- DROP PROC Proc_GetHORule_Endorsement_Pol                                                               
CREATE proc dbo.Proc_GetHORule_Endorsement_Pol                  
(                                                                 
@CUSTOMER_ID    int,                                                                                                                          
@POLICY_ID    int,                                                                                                                          
@POLICY_VERSION_ID   int,                                                                          
@DWELLING_ID int                 
                                               
)                                                                  
as                                                                      
BEGIN           
        
DECLARE @OTHERS_LOCATION CHAR        
        
DECLARE @RENTED_OTHERS CHAR        
--ENDORSEMENT # 70 ADDITIONAL RESIDENCE RENTED TO OTHERS IS CHECKED OFF THEN         
--MAKE SURE THEY HAVE COMPLETED A LEAST ONE LINE OF DETAIL UNDER THE OTHER LOCATIONS / LIABILITY         
IF EXISTS(SELECT ENDORSEMENT_ID FROM POL_DWELLING_ENDORSEMENTS         
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID AND ENDORSEMENT_ID IN(168,218,323,324,325,326)) --HO 40(IN/MI) AND HO-70        
BEGIN        
 SET @RENTED_OTHERS='Y'        
END        
ELSE        
BEGIN        
 SET @RENTED_OTHERS='N'        
END        
   
IF(@RENTED_OTHERS='Y') -- IF ENDORSEMENT IS PRESENT CHECK FOR THE OTHER LOCATION TABLE         
BEGIN        
 IF EXISTS (SELECT LOCATION_ID FROM POL_OTHER_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID         
 and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID and IS_ACTIVE='Y')        
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

