IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Application_Vehicle_Information]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Application_Vehicle_Information]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                  
Proc Name           : Dbo.Proc_Get_Application_Vehicle_Information                                  
Created by            : Swarup                                 
Date                     : 04/16/2007                                  
Purpose                : To get the information for creating the input xml                                    
Revison History    :                                  
Used In                 :   Creating InputXML for vehicle                                  
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
--     
      
--   

CREATE PROCEDURE dbo.Proc_Get_Application_Vehicle_Information      
       
@CUSTOMERID    int,                                                                  
@APPID    int,                                                                  
@APPVERSIONID   int                                                                  
--@VEHICLEID    int       
      
AS      
BEGIN                                               
                                                              
        
      
DECLARE  @STATENAME            nvarchar(100)              
DECLARE  @QUOTEEFFDATE            nvarchar(20)   
DECLARE  @LOBNAME            int 
DECLARE  @LOB_NAME            nvarchar(20)             
DECLARE  @VEHICLETYPEUSE nvarchar(100)               
      
select @STATENAME = upper(STATE_NAME) ,                                 
       @QUOTEEFFDATE = convert(char(10),APP_EFFECTIVE_DATE,101),                           
      	@LOBNAME = APP_LOB
FROM APP_LIST WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST WITH (NOLOCK) ON MNT_COUNTRY_STATE_LIST.STATE_ID=APP_LIST.STATE_ID                                   
WHERE CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID                                 
      
SELECT @LOB_NAME =LOB_CODE FROM MNT_LOB_MASTER WHERE LOB_ID= @LOBNAME



SELECT                                                             
                                                                  
 @VEHICLETYPEUSE = case isnull(USE_VEHICLE,'0')                                                                
         when '11332' then 'PERSONAL'                                                                    
         when '0' then 'PERSONAL'                                                                    
         end                              
 FROM APP_VEHICLES WITH (NOLOCK)                                             
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID      
      
End      
      
Select      
@LOB_NAME AS LOBNAME,     
@STATENAME as STATENAME,      
@QUOTEEFFDATE as QUOTEEFFDATE,      
isnull(@VEHICLETYPEUSE,'0') as VEHICLETYPEUSE      
      
      
      
     
  
  



GO

