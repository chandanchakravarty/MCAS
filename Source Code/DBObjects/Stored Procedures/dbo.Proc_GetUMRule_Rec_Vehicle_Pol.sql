IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMRule_Rec_Vehicle_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMRule_Rec_Vehicle_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*====================================================================================================
Proc Name                : Dbo.Proc_GetUMRule_Rec_Vehicle_Pol   
Created by               : Ashwani                                                                                              
Date                     : 12 Oct,2006                                              
Purpose                  : To get the Boat Info for UM rules                                              
Revison History          :                                                                                              
Used In                  : Wolverine                                                                                              
======================================================================================================  
Date     Review By          Comments                                                                                              
=====  ==============   =============================================================================*/ 
-- drop proc dbo.Proc_GetUMRule_Rec_Vehicle_Pol 
create proc dbo.Proc_GetUMRule_Rec_Vehicle_Pol                                                                                                                                                                        
(                                                                                                                                                                            
 @CUSTOMERID    int,                                                                                                                                                                            
 @POLICYID    int,                                                                                                                                                                            
 @POLICYVERSIONID   int,                                                                                                                                                                  
 @VEHICLEID int                                                                                                                                   
)                                                                                                                                                                    
as                                                                                                                                                                                
begin        
-- Mandatory Info      
declare @COMPANY_ID_NUMBER varchar(20) -- int      
declare @YEAR varchar(20) --int      
declare @MAKE nvarchar(75)      
declare @MODEL nvarchar(75)      
declare @VEHICLE_TYPE  varchar(20) -- int      
declare @VEHICLE_MODIFIED nchar(1)      
declare @VEHICLE_MODIFIED_DETAILS  varchar(100)-- Condition      
declare @USED_IN_RACE_SPEED nchar(1)      
declare @USED_IN_RACE_SPEED_CONTEST varchar(250) -- Condition      
declare @STATE_REGISTERED nchar(2)      
declare @REC_VEH_TYPE varchar(20) -- int       
      
       
if exists(select CUSTOMER_ID from POL_UMBRELLA_RECREATIONAL_VEHICLES      
   where CUSTOMER_ID=@CUSTOMERID and POLICY_ID=@POLICYID and POLICY_VERSION_ID=@POLICYVERSIONID and REC_VEH_ID=@VEHICLEID)  begin                                                              
 select @COMPANY_ID_NUMBER=isnull(convert(varchar(20),COMPANY_ID_NUMBER),''),      
 @YEAR=isnull(convert(varchar(20),YEAR),''),@MAKE=isnull(MAKE,''),@MODEL=isnull(MODEL,''),      
 @VEHICLE_TYPE=isnull(VEHICLE_TYPE,''),@VEHICLE_MODIFIED=isnull(VEHICLE_MODIFIED,''),      
 @VEHICLE_MODIFIED_DETAILS=isnull(VEHICLE_MODIFIED_DETAILS,''),      
 @USED_IN_RACE_SPEED_CONTEST=isnull(USED_IN_RACE_SPEED_CONTEST,'') ,@USED_IN_RACE_SPEED =isnull(USED_IN_RACE_SPEED,''),      
 @STATE_REGISTERED =isnull(STATE_REGISTERED,''),@REC_VEH_TYPE = isnull(convert(varchar(20),REC_VEH_TYPE),'')      
 from POL_UMBRELLA_RECREATIONAL_VEHICLES                                                                                                                                
  where CUSTOMER_ID=@CUSTOMERID and  POLICY_ID=@POLICYID  and  POLICY_VERSION_ID=@POLICYVERSIONID   and REC_VEH_ID=@VEHICLEID                                                                                                               
end         
      
      
-- Recreational Vehicles Used to participate in any race or speed contest?If yes  Refer to underwriters       
      
 if(@USED_IN_RACE_SPEED='1')      
 set @USED_IN_RACE_SPEED='Y'      
      
-- Recreational Vehicles State Registered Field is other then Michigan or Indiana then Refer to underwriters       
 declare @RF_STATE_REGISTERED char       
  set @RF_STATE_REGISTERED='N'      
      
--if((@STATE_REGISTERED<>14 or @STATE_REGISTERED<>22) and @STATE_REGISTERED<>0)      
-- set @RF_STATE_REGISTERED='Y'      
--else if(@STATE_REGISTERED=0)      
-- set @RF_STATE_REGISTERED=''      
      
      
if(@STATE_REGISTERED='14' or @STATE_REGISTERED='22')      
 set @RF_STATE_REGISTERED='N'      
else if(@STATE_REGISTERED='0')      
 set @RF_STATE_REGISTERED=''      
else       
 set @RF_STATE_REGISTERED='Y'      
      
      
      
-- Recreational Vehicles Has this vehicle been modified If Yes Refer to underwriters       
 if(@VEHICLE_MODIFIED='1')      
 set @VEHICLE_MODIFIED='Y'      
      
-- Motorcycle Details If Type of Recreational Vehicle is Dune Buggies Refer to underwriters       
 declare @RV_DUNEBUGGIE char       
 set  @RV_DUNEBUGGIE='N'      
       
 if(@REC_VEH_TYPE='11944')      
 set @RV_DUNEBUGGIE='Y'      
      
      
      
      
select      
-- Mandatory      
 @COMPANY_ID_NUMBER as COMPANY_ID_NUMBER,      
 @YEAR as YEAR,      
 @MAKE as MAKE,      
 @MODEL as MODEL,      
 @VEHICLE_TYPE as VEHICLE_TYPE,      
 @REC_VEH_TYPE as REC_VEH_TYPE,      
 @VEHICLE_MODIFIED as VEHICLE_MODIFIED,      
 case @VEHICLE_MODIFIED when 'Y'      
 then @VEHICLE_MODIFIED_DETAILS end as VEHICLE_MODIFIED_DETAILS,      
 --Rule      
 @RF_STATE_REGISTERED as RF_STATE_REGISTERED,      
 @USED_IN_RACE_SPEED as USED_IN_RACE_SPEED,      
 case @USED_IN_RACE_SPEED when 'Y'      
 then @USED_IN_RACE_SPEED_CONTEST       
 end as USED_IN_RACE_SPEED_CONTEST,      
 @RV_DUNEBUGGIE as RV_DUNEBUGGIE      
      
end                                                                                              
      




GO

