IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAgecnyUnderwriter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAgecnyUnderwriter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateAgecnyUnderwriter  
Created by      : Gaurav  
Date            : 25/8/2005  
Purpose         : To UPDATE RECORD IN MNT_AGENCY_LIST FOR UNDERWRITER ASSIGNEMENT  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_UpdateAgecnyUnderwriter  
(  
@intLobId  smallint,     
@strUnderWriters varchar(100),  
@strMarketeer varchar(200),
@intAgencyId  int  
)  
 AS  
BEGIN  
  
--If Exists(SELECT User_Type_Code  HOME_MARKETING

 --  FROM MNT_USER_TYPES  
  -- WHERE User_Type_Code = @User_Type_Code AND User_Type_Id <> @User_Type_Id)  
  --BEGIN  
   /*Code already exists*/  
  --return 0  
 -- END  
  --ELSE  
--BEGIN  
if(@intLobId=1)  
begin  
UPDATE MNT_AGENCY_LIST  SET  
HOME_UNDERWRITER =@strUnderWriters ,
HOME_MARKETING =@strMarketeer
 
where Agency_id=@intAgencyId  
end      
  
  
if(@intLobId=2)  
begin  
UPDATE MNT_AGENCY_LIST  SET  
PRIVATE_UNDERWRITER =@strUnderWriters , 
PRIVATE_MARKETING =@strMarketeer 
where Agency_id=@intAgencyId  
end   
    
if(@intLobId=3)  
begin  
UPDATE MNT_AGENCY_LIST  SET  
MOTOR_UNDERWRITER =@strUnderWriters  ,
MOTOR_MARKETING =@strMarketeer  
where Agency_id=@intAgencyId  
end      
  
if(@intLobId=4)  
begin  
UPDATE MNT_AGENCY_LIST  SET  
WATER_UNDERWRITER =@strUnderWriters  ,
WATER_MARKETING =@strMarketeer  
where Agency_id=@intAgencyId  
end    
  
if(@intLobId=5)  
begin  
UPDATE MNT_AGENCY_LIST  SET  
UMBRELLA_UNDERWRITER =@strUnderWriters  ,
UMBRELLA_MARKETING =@strMarketeer  
where Agency_id=@intAgencyId  
end    
  
if(@intLobId=6)  
begin  
UPDATE MNT_AGENCY_LIST  SET  
RENTAL_UNDERWRITER =@strUnderWriters  ,
RENTAL_MARKETING =@strMarketeer  
where Agency_id=@intAgencyId  
end   
  
if(@intLobId=7)  
begin  
UPDATE MNT_AGENCY_LIST  SET  
GENERAL_UNDERWRITER =@strUnderWriters  ,
GENERAL_MARKETING =@strMarketeer  
where Agency_id=@intAgencyId  
end     
   
END  
--END  



GO

