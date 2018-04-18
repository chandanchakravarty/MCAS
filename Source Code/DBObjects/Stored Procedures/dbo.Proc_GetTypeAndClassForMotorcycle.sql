IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTypeAndClassForMotorcycle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTypeAndClassForMotorcycle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetTypeAndClassForMotorcyle    
Created by      : Sumit Chhabra    
Date            : 27/10/2005        
Purpose      : Fetch type and class corresponding to make and model of motorcycle    
Created by      : Sumit Chhabra    
Date            : 28/11/2005        
Purpose      	: Removed class information from being feteched
Revison History :        
Used In  : BRICS        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_GetTypeAndClassForMotorcycle    
(        
 @Manufacturer  varchar(500),    
 @Model  varchar(500)    
)        
AS       
    
    
BEGIN        
--declare @TypeId int    
--declare @ClassId varchar(10)    
    
--Get ClassId    
--select  @ClassId=lookup_unique_id from mnt_lookup_values where lookup_value_desc=(    
--select @ClassID=class from mnt_vin_motorcycle_master where  Manufacturer=@Manufacturer and model=@Model    
    
    
--Get TypeId     
--select @TypeId=lookup_unique_id from mnt_lookup_values where lookup_value_code=@ClassId and lookup_id=1188        
    
    
    
--select @TypeId as TypeId, @ClassId as ClassId    
    
select l.lookup_unique_id as TypeId from mnt_vin_motorcycle_master m join mnt_lookup_values l
on l.lookup_value_code=m.class
where m.Manufacturer=@Manufacturer and m.model=@Model  and l.lookup_id=1188
    
END    



GO

