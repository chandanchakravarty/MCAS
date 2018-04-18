IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETWATERCRAFTRULE_EQUIP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETWATERCRAFTRULE_EQUIP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                                                                            
Proc Name                : Dbo.PROC_GETWATERCRAFTRULE_EQUIP   920,367,1                                                                                                                                      
Created by               : Praveen Kasana                                                                                                                                           
Date                     : 29 May 2006                                                                                                        
Purpose                  : To get the Watercraft Equipment Details info for Rules                                                                                                                                          
Revison History          :                                                                                                                                            
Used In                  : Wolverine                                                                                                                                            
------------------------------------------------------------                                                                                                                                            
Date     Review By          Comments                                                                                                                                            
------   ------------       -------------------------*/              
CREATE PROC dbo.PROC_GETWATERCRAFTRULE_EQUIP               
(                                                                                                                                                          
@CUSTOMERID    INT,                                                                                                                                                          
@APPID    INT,                                                                                                                                                          
@APPVERSIONID   INT,    
@EQUIP_ID INT                
                
                
)                
AS                
BEGIN                
                
--DECLARE @EQUIP_ID INT                
DECLARE @INT_DESCRIPTION int                
DECLARE @DESCRIPTION char                
DECLARE @INT_EQUIPMENT_TYPE int                
DECLARE @EQUIPMENT_TYPE char    
Declare @EQUIP_NO int   
Declare @INTEQUIP_TYPE int  
Declare @EQUIP_TYPE varchar(100)             
      
          
DECLARE @OTHER_DESCRIPTION_EQUIP_TYPE nvarchar(100)              
                
/*SELECT @EQUIP_ID =  EQUIP_ID FROM APP_WATERCRAFT_EQUIP_DETAILLS WHERE                
CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID  AND APP_VERSION_ID=@APPVERSIONID*/    
                
IF EXISTS (SELECT CUSTOMER_ID FROM APP_WATERCRAFT_EQUIP_DETAILLS WHERE CUSTOMER_ID=@CUSTOMERID AND                
APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND EQUIP_ID=@EQUIP_ID)                
                
BEGIN                
--SET @IS_RECORD_EXISTS ='Y'              
              
 SELECT  @INT_DESCRIPTION=ISNULL(EQUIP_TYPE,0),              
         @INT_EQUIPMENT_TYPE = ISNULL(EQUIPMENT_TYPE,0),            
         @OTHER_DESCRIPTION_EQUIP_TYPE = ISNULL(OTHER_DESCRIPTION,''),    
---------Client Top Messages   
  @EQUIP_NO=isnull(EQUIP_ID,0),  
  @INTEQUIP_TYPE=isnull(EQUIP_TYPE,0)  
           
                    
 FROM                
 APP_WATERCRAFT_EQUIP_DETAILLS WHERE CUSTOMER_ID=@CUSTOMERID AND                
 APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND EQUIP_ID=@EQUIP_ID              
      
--      
 if (@INT_DESCRIPTION<>0)              
 begin              
  set @DESCRIPTION='Y'              
 end              
  else              
 begin              
  set @DESCRIPTION=''              
 end              
               
               
 if (@INT_EQUIPMENT_TYPE<>0)              
 begin              
  set @EQUIPMENT_TYPE='Y'              
 end              
  else              
 begin              
  set @EQUIPMENT_TYPE=''          
 end          
    
--        
END                
 ELSE                
BEGIN                
      
              
 set @DESCRIPTION='N'              
 set @EQUIPMENT_TYPE='N'             
 set @OTHER_DESCRIPTION_EQUIP_TYPE='N'       
          
              
END                
              
      
--          
SELECT @EQUIP_TYPE = UPPER(LOOKUP_VALUE_DESC) FROM MNT_LOOKUP_VALUES M  
WHERE M.LOOKUP_UNIQUE_ID =@INTEQUIP_TYPE          
--             
              
SELECT                
--@IS_RECORD_EXISTS as IS_RECORD_EXISTS,    
@EQUIP_NO AS EQUIP_NO,  
@EQUIP_TYPE AS EQUIP_TYPE,              
@DESCRIPTION AS DESCRIPTION,                
@EQUIPMENT_TYPE AS EQUIPMENT_TYPE,          
CASE @INT_DESCRIPTION WHEN -1 THEN  @OTHER_DESCRIPTION_EQUIP_TYPE END AS OTHER_DESCRIPTION_EQUIP_TYPE       
                
               
            
END                
            
          
        
        
      
    
  



GO

