 /*----------------------------------------------------------                                                              
Proc Name                : Dbo.Proc_GetHORule_LocationInfo_Pol                                                            
Created by               : Ashwani                                                              
Date                     : 02 Mar. 2006            
Purpose                  : To get the location detail for HO policy rules              
Revison History          :                                                              
Used In                  : Wolverine                                                              
------------------------------------------------------------                                                              
Date     Review By          Comments                                                              
------   ------------       -------------------------*/         
--DROP PROC Proc_GetHORule_LocationInfo_Pol 1692,76,4,4429                                                            
ALTER proc dbo.Proc_GetHORule_LocationInfo_Pol              
(                                                              
@CUSTOMER_ID    int,                                                              
@POLICY_ID    int,                                                              
@POLICY_VERSION_ID   int,              
@LOCATION_ID int          
)                                                              
as                                                                  
begin                               
 declare @INTLOC_NUM int              
 declare @LOC_NUM char            
 declare @LOC_ADD1 nvarchar(75)              
 declare @LOC_CITY nvarchar(75)              
 declare @LOC_STATE nvarchar(5)              
 declare @LOC_ZIP nvarchar(11)              
               
 if exists (select CUSTOMER_ID from POL_LOCATIONS  with(nolock)                               
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and LOCATION_ID=@LOCATION_ID)              
 begin               
  select  @intLOC_NUM=isnull(LOC_NUM,0),@LOC_ADD1=isnull(LOC_ADD1,''),@LOC_CITY=isnull(LOC_CITY,''),              
  @LOC_STATE=isnull(LOC_STATE,''),@LOC_ZIP=isnull(LOC_ZIP,'')              
 from POL_LOCATIONS    with(nolock)          
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and LOCATION_ID=@LOCATION_ID              
              
 end              
else              
begin               
 set @LOC_NUM =''              
 set @LOC_ADD1 =''              
 set @LOC_CITY =''              
 set @LOC_STATE =''              
 set @LOC_ZIP =''              
end               
 if(@intLOC_NUM=0 or @intLOC_NUM is null)            
 begin             
  set @LOC_NUM =''            
 end            
 else            
 begin             
  set @LOC_NUM='N'            
 end             
         
--"Application Details tab :13 June 2006 --If policy type is HO-4/HO-5/HO-6 then look at the Location Details Tab - Location is  --Is Seasonal - then refer to underwriter"                
Declare @POLICY_TYPE int                
Declare @INTLOCATION_TYPE int                
Declare @LOCATION_TYPE char   
DECLARE @STATE_ID SMALLINT --Added by Charles on 8-Dec-09 for Itrack 6818  
               
Select @POLICY_TYPE=ISNULL(POLICY_TYPE,''), @STATE_ID=STATE_ID from  POL_CUSTOMER_POLICY_LIST with(nolock)   --@STATE_ID Added by Charles on 8-Dec-09 for Itrack 6818        
where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                      
--get Location                
Select @INTLOCATION_TYPE =ISNULL(LOCATION_TYPE,0) from POL_LOCATIONS with(nolock)               
where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and LOCATION_ID=@LOCATION_ID                
--11401 HO-5 Replacement    
--11405 HO-4 Tenants    
--11406 HO-6 Unit Owners    
--11410 HO-5 Premier   
--11195 HO-4 Tenants    
--11149 HO-5 Replacement    
--11196 HO-6 Unit Owners              
if(@POLICY_TYPE = 11149 or @POLICY_TYPE = 11401     
or @POLICY_TYPE = 11195 or @POLICY_TYPE = 11196 or @POLICY_TYPE=11405    
or @POLICY_TYPE = 11406 or @POLICY_TYPE=11410)               
begin                 
                
if (@INTLOCATION_TYPE =11814) --Seasonal                
 begin                
  set @LOCATION_TYPE ='Y'                
 end                
 else                
 begin                
  set @LOCATION_TYPE ='N'                
 end                
            
end                
else                
begin                
 set @LOCATION_TYPE ='N'                
end                
  
--Added by Charles on 8-Dec-09 for Itrack 6818  
DECLARE @LOCATION_STATE_APP_STATE CHAR  
SET @LOCATION_STATE_APP_STATE = 'N'  
  
IF (@STATE_ID!=@LOC_STATE) AND ((@STATE_ID IS NOT NULL) AND @LOC_STATE!='')  
BEGIN  
 SET @LOCATION_STATE_APP_STATE = 'Y'  
END  
--Added till here  
---FOR DISPLAY POL TYPE AT RATE WINDOW      
DECLARE @POLICY_NAME VARCHAR(55)      
      
SELECT @POLICY_NAME = LOOKUP_VALUE_DESC      
 FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID= @POLICY_TYPE      
                         
                     
select         
 @LOC_NUM as LOC_NUM,             
 @LOC_ADD1  as LOC_ADD1,              
 @LOC_CITY as  LOC_CITY,              
 --@LOC_STATE as LOC_STATE,              
 @LOC_ZIP as LOC_ZIP,        
 @LOCATION_TYPE AS LOCATION_TYPE ,    
 @POLICY_NAME as POLICY_NAME,              
 @LOCATION_STATE_APP_STATE AS LOCATION_STATE_APP_STATE --Added by Charles on 8-Dec-09 for Itrack 6818  
end      