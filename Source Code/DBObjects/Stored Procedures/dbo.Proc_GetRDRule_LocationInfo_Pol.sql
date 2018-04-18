IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_LocationInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_LocationInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                              
Proc Name                : Dbo.Proc_GetRDRule_LocationInfo_Pol                                                            
Created by               : Ashwani                                                              
Date                     : 02 Mar 2006        
Purpose                  : To get the location detail for RD policy rules              
Revison History          :                                                              
Used In                  : Wolverine                                                              
------------------------------------------------------------                                                              
Date     Review By          Comments                                                              
------   ------------       -------------------------*/     
-- DROP PROC dbo.Proc_GetRDRule_LocationInfo_Pol                                                             
CREATE proc dbo.Proc_GetRDRule_LocationInfo_Pol              
(                                                              
@CUSTOMER_ID    int,                                                              
@POLICY_ID    int,                                                              
@POLICY_VERSION_ID   int,              
@LOCATION_ID int                          
)                                                              
as                                                                  
BEGIN                               
  DECLARE @INTLOC_NUM INT              
  DECLARE @LOC_NUM CHAR                  
  DECLARE @LOC_ADD1 NVARCHAR(75)              
  DECLARE @LOC_CITY NVARCHAR(75)              
  DECLARE @LOC_STATE NVARCHAR(5)              
  DECLARE @LOC_ZIP NVARCHAR(11)      
  DECLARE @LOC_TYPE CHAR       
  DECLARE @INTLOC_TYPE INT    
  DECLARE @RENTED_WEEKLY VARCHAR(10)              
  DECLARE @WEEKS_RENTED VARCHAR(10)                   
                
  IF EXISTS (SELECT CUSTOMER_ID FROM POL_LOCATIONS   WITH(NOLOCK)                              
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND LOCATION_ID=@LOCATION_ID)              
  BEGIN               
   SELECT  @INTLOC_NUM=ISNULL(LOC_NUM,0),@LOC_ADD1=ISNULL(LOC_ADD1,''),@LOC_CITY=ISNULL(LOC_CITY,''),              
   @LOC_STATE=ISNULL(LOC_STATE,''),@LOC_ZIP=SUBSTRING(LOC_ZIP,1,5),@INTLOC_TYPE=ISNULL(LOCATION_TYPE,0),    
   @RENTED_WEEKLY=ISNULL(RENTED_WEEKLY,''), @WEEKS_RENTED=ISNULL(WEEKS_RENTED,'')               
   FROM POL_LOCATIONS  WITH(NOLOCK)             
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND LOCATION_ID=@LOCATION_ID              
       
  END              
  ELSE              
   BEGIN               
   SET @LOC_NUM =''              
   SET @LOC_ADD1 =''              
   SET @LOC_CITY =''              
   SET @LOC_STATE =''              
   SET @LOC_ZIP =''       
   SET @LOC_TYPE=''             
  END               
  IF(@INTLOC_NUM=0 OR @INTLOC_NUM IS NULL)            
   BEGIN             
   SET @LOC_NUM =''            
   END            
  ELSE            
   BEGIN             
   SET @LOC_NUM='N'            
   END       
    ----------FOR MANDATORY ' LOCATION IS'             
   IF(@INTLOC_TYPE=0 OR @INTLOC_TYPE IS NULL)        
   BEGIN        
   SET @LOC_TYPE=''        
   END        
  ELSE        
   BEGIN        
   SET @LOC_TYPE='N'        
   END     
  -------LOCATION RULE---------              
--"Location Details Tab  --If Field Is location rented on a Weekly Basis  --is yes and the number of weeks is greater than 4 -  then risk is Declined "              
DECLARE @LOCATION_RENTED CHAR              
IF(@RENTED_WEEKLY='Y')              
BEGIN              
  IF(@WEEKS_RENTED > 4)              
 BEGIN              
 SET @LOCATION_RENTED='Y'              
 END              
  ELSE              
 BEGIN              
 SET @LOCATION_RENTED='N'              
 END   
END              
ELSE              
   BEGIN              
   SET @LOCATION_RENTED='N'       
   END              
-------------------------     
--------================STATE CHECK FOR APP and LOCATION==============          
DECLARE @APP_STATE CHAR          
DECLARE @INT_APP_STATE INT          
DECLARE @STATE_LOC_APP CHAR          
SELECT @INT_APP_STATE=STATE_ID FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)  
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID           
          
IF(@LOC_STATE<>@INT_APP_STATE)          
   BEGIN          
   SET @STATE_LOC_APP='Y'          
   END          
ELSE          
   BEGIN          
   SET @STATE_LOC_APP='N'          
   END          
        
---ZIP ACCORDING TO STATE        
DECLARE @ZIP_LOC_APP CHAR          
IF EXISTS(SELECT ZIP FROM MNT_TERRITORY_CODES WHERE STATE=@INT_APP_STATE AND  ZIP = @LOC_ZIP)        
   BEGIN         
   SET @ZIP_LOC_APP = 'N'        
   END        
ELSE        
   BEGIN        
   SET @ZIP_LOC_APP = 'Y'        
   END        
                  
 SELECT              
  @LOC_NUM AS LOC_NUM,             
  @LOC_ADD1  AS LOC_ADD1,              
  @LOC_CITY AS  LOC_CITY,              
  @LOC_STATE AS LOC_STATE,              
  @LOC_ZIP AS LOC_ZIP,      
  @LOC_TYPE AS LOC_TYPE ,    
  @LOCATION_RENTED AS LOCATION_RENTED,    
  @STATE_LOC_APP AS STATE_LOC_APP,    
  @ZIP_LOC_APP AS  ZIP_LOC_APP             
                           
END  


GO

