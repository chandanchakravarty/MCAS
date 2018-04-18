IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_LocationInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_LocationInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------                                                                
Proc Name                : Dbo.Proc_GetRDRule_LocationInfo                                                              
Created by               : Ashwani                                                                
Date                     : 12 Dec.,2005                
Purpose                  : To get the location detail for RD rules                
Revison History          :                                                                
Used In                  : Wolverine                                                                
------------------------------------------------------------                                                                
Date     Review By          Comments                                                                
------   ------------       -------------------------*/ 
-- drop proc dbo.Proc_GetRDRule_LocationInfo                                                                
CREATE proc dbo.Proc_GetRDRule_LocationInfo                
(                                                                
@CUSTOMERID    int,                                                                
@APPID    int,                                                                
@APPVERSIONID   int,                
@LOCATIONID int,                            
@DESC varchar(10)                                                  
)                                                                
AS                                                                    
BEGIN                                 
 DECLARE @INTLOC_NUM INT                
 DECLARE @LOC_NUM CHAR                
 DECLARE @INTLOC_TYPE INT     
 DECLARE @LOC_TYPE CHAR          
 DECLARE @LOC_ADD1 NVARCHAR(75)                
 DECLARE @LOC_CITY NVARCHAR(75)                
 DECLARE @LOC_STATE NVARCHAR(5)                
 DECLARE @LOC_ZIP NVARCHAR(11)          
 DECLARE @RENTED_WEEKLY VARCHAR(10)          
 DECLARE @WEEKS_RENTED VARCHAR(10)          
          
                
                 
 IF EXISTS (SELECT CUSTOMER_ID FROM APP_LOCATIONS  WITH(NOLOCK)                                
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND LOCATION_ID=@LOCATIONID)                
 BEGIN                 
	 SELECT  @INTLOC_NUM=ISNULL(LOC_NUM,0),@LOC_ADD1=ISNULL(LOC_ADD1,''),@LOC_CITY=ISNULL(LOC_CITY,''),                
	 @LOC_STATE=ISNULL(LOC_STATE,''),@LOC_ZIP=SUBSTRING(LOC_ZIP,1,5),@RENTED_WEEKLY=ISNULL(RENTED_WEEKLY,''),          
	 @WEEKS_RENTED=ISNULL(WEEKS_RENTED,''),@INTLOC_TYPE=ISNULL(LOCATION_TYPE,0)                
	 FROM APP_LOCATIONS  WITH(NOLOCK)               
	 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND LOCATION_ID=@LOCATIONID                
   
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
--------================STATE CHECK FOR APP and LOCATION==============      
DECLARE @APP_STATE CHAR      
DECLARE @INT_APP_STATE INT      
DECLARE @STATE_LOC_APP CHAR      
SELECT @INT_APP_STATE=STATE_ID FROM APP_LIST  WITH(NOLOCK)     
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID       
      
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
IF EXISTS(SELECT ZIP FROM MNT_TERRITORY_CODES  WITH(NOLOCK) WHERE STATE=@INT_APP_STATE AND  ZIP = @LOC_ZIP)    
   BEGIN     
   SET @ZIP_LOC_APP = 'N'    
   END    
ELSE    
   BEGIN    
   SET @ZIP_LOC_APP = 'Y'    
   END    
 
SELECT                
  @LOC_NUM as LOC_NUM,             
  @LOC_ADD1  as LOC_ADD1,                
  @LOC_CITY as  LOC_CITY,                
  @LOC_STATE as LOC_STATE,                
  @LOC_ZIP as LOC_ZIP,          
  @LOCATION_RENTED as LOCATION_RENTED,      
  @STATE_LOC_APP as STATE_LOC_APP,    
  @ZIP_LOC_APP as ZIP_LOC_APP,  
  @LOC_TYPE as LOC_TYPE              
                            
END  


GO

