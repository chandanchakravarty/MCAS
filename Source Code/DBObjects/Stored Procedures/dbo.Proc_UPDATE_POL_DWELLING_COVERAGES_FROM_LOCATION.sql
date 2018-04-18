IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_POL_DWELLING_COVERAGES_FROM_LOCATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_POL_DWELLING_COVERAGES_FROM_LOCATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_UPDATE_POL_DWELLING_COVERAGES_FROM_LOCATION    
    
/*----------------------------------------------------------              
Proc Name       : Dbo.Proc_UPDATE_POL_DWELLING_COVERAGES_FROM_LOCATION      
Created by      : Pradeep              
Date            : 12/13/2005         
Purpose        :   Updates coverages on the basis of location attributes    
Revison History :              
Used In        : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROCEDURE Proc_UPDATE_POL_DWELLING_COVERAGES_FROM_LOCATION  
(            
  @CUSTOMER_ID Int,            
  @POLICY_ID Int,            
  @POLICY_VERSION_ID smallint,            
  @LOCATION_ID Int         
)            
AS            
  
BEGIN  
  
 DECLARE @STATE_ID Int  
 DECLARE @IS_PRIMARY Char  
 DECLARE @FRAUD Int  
 DECLARE @DWELLING_ID Int  
       
 --If no dwellings return  
  SELECT @DWELLING_ID = DWELLING_ID  
  FROM POL_DWELLINGS_INFO --WITH (NOLOCK)      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
   POLICY_ID = @POLICY_ID AND      
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
   LOCATION_ID = @LOCATION_ID  
   
 IF ( @DWELLING_ID IS NULL )  
 BEGIN  
  RETURN  
 END  
   
 --Get State      
 SELECT @STATE_ID = STATE_ID FROM POL_CUSTOMER_POLICY_LIST  --WITH (NOLOCK)    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID       
       
 SELECT @IS_PRIMARY = IS_PRIMARY  
 FROM POL_LOCATIONS  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
  LOCATION_ID = @LOCATION_ID  
   
   
 IF ( @STATE_ID = 14 )  
 BEGIN  
  SET @FRAUD = 158  
 END  
   
 IF ( @STATE_ID = 22 )  
 BEGIN  
  SET @FRAUD = 84  
 END  
   
 --If primary location, remove Identity Fraud Expense Coverage (HO-455)  
 IF ( @IS_PRIMARY = 'N' )  
 BEGIN  
    
  EXEC Proc_DELETE_DWELLING_COVERAGES_BY_ID_FOR_POLICY  
   @CUSTOMER_ID,  
   @POLICY_ID,  
   @POLICY_VERSION_ID,  
   @DWELLING_ID,
   @FRAUD  
   
 END  
  
         
END  



GO

