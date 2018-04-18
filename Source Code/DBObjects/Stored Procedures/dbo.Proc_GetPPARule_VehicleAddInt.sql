IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_VehicleAddInt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_VehicleAddInt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/* ----------------------------------------------------------                  
Proc Name   : dbo.Proc_GetPPARule_VehicleAddInt        
Created by  : Ashwaini        
Date        : 14 Nov.,2005        
Purpose     : Get the Input XML for Additional Interest                    
Revison History  :                        
 ------------------------------------------------------------                              
Date     Review By          Comments                            
                   
------   ------------       ------------------------ */                  
CREATE procedure dbo.Proc_GetPPARule_VehicleAddInt        
(        
 @CUSTOMERID int,        
 @APPID int,        
 @APPVERSIONID int,        
 @VEHICLEID int,        
 @ADDINTID int        
)            
as                 
begin         
 declare @HOLDER_ID int        
 declare @HOLDER_ADD1 nvarchar(140)        
 declare @HOLDER_CITY nvarchar(80)        
 declare @HOLDER_STATE nvarchar(10)        
 declare @HOLDER_ZIP varchar(11)        
 declare @NATURE_OF_INTEREST  nvarchar(30)   
 declare @HOLDER_NAME nvarchar(75)     
        
 if exists(select  CUSTOMER_ID  
  from  APP_ADD_OTHER_INT         
  where CUSTOMER_ID = @CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and  VEHICLE_ID=@VEHICLEID and         
   ADD_INT_ID=@ADDINTID )  
 begin   
 select @HOLDER_ID=isnull(HOLDER_ID,0)   
  from  APP_ADD_OTHER_INT         
  where CUSTOMER_ID = @CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and  VEHICLE_ID=@VEHICLEID and         
   ADD_INT_ID=@ADDINTID        
   
  if(@HOLDER_ID !=0)  
  begin   
  select @HOLDER_ID=isnull(B.HOLDER_ID,0),@HOLDER_ADD1=isnull(B.HOLDER_ADD1,''),@HOLDER_CITY=isnull(B.HOLDER_CITY,''),    
   @HOLDER_STATE=isnull(B.HOLDER_STATE,''),@HOLDER_ZIP=isnull(B.HOLDER_ZIP,''),@NATURE_OF_INTEREST=isnull(A.NATURE_OF_INTEREST,'')
   ,@HOLDER_NAME =isnull(B.HOLDER_NAME,'')        
   from  APP_ADD_OTHER_INT  A  
   left join MNT_HOLDER_INTEREST_LIST B   
   on A.HOLDER_ID=B.HOLDER_ID      
   where A.CUSTOMER_ID = @CUSTOMERID and A.APP_ID=@APPID and A.APP_VERSION_ID=@APPVERSIONID and  A.VEHICLE_ID=@VEHICLEID and         
   A.ADD_INT_ID=@ADDINTID  
  end         
  else   
  begin   
   select @HOLDER_ID=isnull(HOLDER_ID,0),@HOLDER_ADD1=isnull(HOLDER_ADD1,''),@HOLDER_CITY=isnull(HOLDER_CITY,''),    
   @HOLDER_STATE=isnull(HOLDER_STATE,''),@HOLDER_ZIP=isnull(HOLDER_ZIP,''),@NATURE_OF_INTEREST=isnull(NATURE_OF_INTEREST,'')        
  ,@HOLDER_NAME =isnull(HOLDER_NAME,'') 
   from  APP_ADD_OTHER_INT         
   where CUSTOMER_ID = @CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and  VEHICLE_ID=@VEHICLEID and         
   ADD_INT_ID=@ADDINTID   
  end        
  
 end         
 else        
 begin         
  set @HOLDER_ID=0         
  set @HOLDER_ADD1 =''        
  set @HOLDER_CITY =''        
  set @HOLDER_STATE =''        
  set @HOLDER_ZIP =''        
  set @NATURE_OF_INTEREST =''  
  set @HOLDER_NAME=''      
 end         
--        
if(@HOLDER_ID=0 )        
begin         
set @HOLDER_ID=''        
end   
select         
@HOLDER_ID as HOLDER_ID, @HOLDER_ADD1 as HOLDER_ADD1, @HOLDER_CITY as HOLDER_CITY, @HOLDER_STATE as HOLDER_STATE,        
@HOLDER_ZIP as HOLDER_ZIP,@NATURE_OF_INTEREST    as NATURE_OF_INTEREST, @HOLDER_NAME as HOLDER_NAME       
end        
    


  
  




GO

