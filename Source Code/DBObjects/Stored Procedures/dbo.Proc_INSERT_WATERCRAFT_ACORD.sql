IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_WATERCRAFT_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_WATERCRAFT_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : Dbo.Proc_INSERT_WATERCRAFT_ACORD                  
Created by      : Pradeep                  
Date            : 12/21/2005               
Purpose         :Insert a watercraft information from Quick quote                 
Revison History :                  
Used In        : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
create    PROC dbo.Proc_INSERT_WATERCRAFT_ACORD                  
(                  
 @CUSTOMER_ID     int,                  
 @APP_ID     int,                  
 @APP_VERSION_ID     smallint,                  
 @BOAT_ID     smallint output,                  
 @BOAT_NO     int,                  
 --@BOAT_NAME     nvarchar(50),                  
 @BOAT_NAME     nvarchar(25),   
 @YEAR     int,                  
 --@MAKE     nvarchar(150),  
 @MAKE     nvarchar(75),  
 --@MODEL     nvarchar(150),  
 @MODEL     nvarchar(75),                  
 --@HULL_ID_NO     nvarchar(150),  
 @HULL_ID_NO     nvarchar(75),                  
 --@STATE_REG     nvarchar(10),  
 @STATE_REG     nvarchar(5),                  
 @HULL_MATERIAL     int,          
 @HULL_MATERIAL_CODE VarChar(20),                  
 @FUEL_TYPE     int,                  
 @DATE_PURCHASED     datetime =null,                  
 --@LENGTH     nvarchar(20),  
 @LENGTH     nvarchar(10),  
 
 @MAX_SPEED     decimal(10,0),               
 --@BERTH_LOC     nvarchar(200),  
 @BERTH_LOC     nvarchar(100),   
 @WATERS_NAVIGATED     varchar(250),                  
 @TERRITORY     nvarchar(50),                  
 @CREATED_BY     int,                  
 @CREATED_DATETIME     datetime,                  
 @TYPE_OF_WATERCRAFT nchar(10),                  
 @INSURING_VALUE decimal(10,2),                  
 @WATERCRAFT_HORSE_POWER int,                  
 @TWIN_SINGLE int,                
 @DESC_OTHER_WATERCRAFT varchar(150) ,              
 --@INCHES    nvarchar(20)  ,  
 @INCHES    nvarchar(10) ,      
 @DIESEL_ENGINE int,      
 @SHORE_STATION int,      
 @HALON_FIRE_EXT_SYSTEM int,      
 @LORAN_NAV_SYSTEM int,      
 @DUAL_OWNERSHIP int ,    
@REMOVE_SAILBOAT        int       
                  
)                  
AS                  
BEGIN                  
             
   DECLARE @BOAT_TYPE Int          
   DECLARE @WAT_NAV Int          
             
   --Watercraft Type          
   EXECUTE @BOAT_TYPE = Proc_GetLookupID 'WCTCD', @TYPE_OF_WATERCRAFT                                          

   EXECUTE @TERRITORY = Proc_GetLookupID 'TERR', @TERRITORY
             
   print('1')        
         
   IF ( @@ERROR <> 0 )        
   BEGIN        
 RETURN        
   END          
        
   EXECUTE @WAT_NAV = Proc_GetLookupID 'WNVC',@WATERS_NAVIGATED                                           
            
   print('2')        
          
   IF ( @@ERROR <> 0 )        
   BEGIN        
 RETURN        
   END          
         
      print('3')        
        
   EXECUTE @HULL_MATERIAL = Proc_GetLookupID '%HULL',@HULL_MATERIAL_CODE           
            
  IF ( @@ERROR <> 0 )        
   BEGIN        
 RETURN        
   END          
          
   select @BOAT_ID=isnull(max(BOAT_ID),0)+1           
 from APP_WATERCRAFT_INFO WHERE            
 CUSTOMER_ID = @CUSTOMER_ID AND                  
   APP_ID = @APP_ID AND                  
   APP_VERSION_ID = @APP_VERSION_ID          
                  
   INSERT INTO APP_WATERCRAFT_INFO                  
   (                  
     CUSTOMER_ID,                  
     APP_ID,                  
     APP_VERSION_ID,                  
     BOAT_ID,                  
     BOAT_NO,                  
     BOAT_NAME,                  
     YEAR, 
     MAKE,                  
     MODEL,                  
 HULL_ID_NO,                  
     STATE_REG,                  
     HULL_MATERIAL,                  
     FUEL_TYPE,                  
     DATE_PURCHASED,                  
     LENGTH,                  
     MAX_SPEED,                  
     BERTH_LOC,                  
     WATERS_NAVIGATED,                  
     TERRITORY,                  
     IS_ACTIVE,                  
     CREATED_BY,                  
     CREATED_DATETIME,                  
     TYPE_OF_WATERCRAFT,                  
     INSURING_VALUE ,                  
     WATERCRAFT_HORSE_POWER,                  
     TWIN_SINGLE,                
     DESC_OTHER_WATERCRAFT  ,               
     INCHES        ,      
 DIESEL_ENGINE ,      
 SHORE_STATION,      
 HALON_FIRE_EXT_SYSTEM ,   LORAN_NAV_SYSTEM ,      
      DUAL_OWNERSHIP         ,REMOVE_SAILBOAT ,
	COV_TYPE_BASIS 
   )                  
   VALUES                  
   (                  
     @CUSTOMER_ID,                  
     @APP_ID,                  
     @APP_VERSION_ID,                  
     @BOAT_ID,                  
     @BOAT_ID,                  
     @BOAT_NAME,                  
     @YEAR,                  
     @MAKE,                  
     @MODEL,                  
     @HULL_ID_NO,                  
     @STATE_REG,                  
     @HULL_MATERIAL,                  
     @FUEL_TYPE,                  
     @DATE_PURCHASED,                  
     @LENGTH,                  
     @MAX_SPEED,                  
     @BERTH_LOC,                  
     @WAT_NAV,                  
     @TERRITORY,                  
     'Y',                  
     @CREATED_BY,                  
     @CREATED_DATETIME,                  
     @BOAT_TYPE,                  
     @INSURING_VALUE ,                  
     @WATERCRAFT_HORSE_POWER,                  
     @TWIN_SINGLE,                
     @DESC_OTHER_WATERCRAFT  ,              
     @INCHES   ,      
 @DIESEL_ENGINE ,      
 @SHORE_STATION,      
 @HALON_FIRE_EXT_SYSTEM ,      
 @LORAN_NAV_SYSTEM ,      
 @DUAL_OWNERSHIP     ,@REMOVE_SAILBOAT            ,
	11758
   )                  
            
END      






GO

