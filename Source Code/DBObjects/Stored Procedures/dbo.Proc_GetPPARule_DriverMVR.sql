IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_DriverMVR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_DriverMVR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*  ----------------------------------------------------------                      
Proc Name   : dbo.Proc_GetPPARule_DriverMVR            
Created by  : Ashwini            
Date        : 14 Nov.,2005            
Purpose     : Get the MVR info for auto rule                       
Revison History  :                            
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/                      
CREATE procedure dbo.Proc_GetPPARule_DriverMVR            
(            
 @CUSTOMERID int,            
 @APPID int,            
 @APPVERSIONID int,            
 @DRIVERID  int,                      
 @APPMVRID int         
)                
as                     
begin       
 declare @intSD_PONITS int    
 declare @SD_POINTS char    
 declare @VIOLATION_ID int    
 declare @MVR_DATE char    
 declare @VIOLATION_DES varchar(225)  
    
 select @intSD_PONITS=isnull(SUM(SD_POINTS),0) from MNT_VIOLATIONS where VIOLATION_ID in    
 (    
  select VIOLATION_ID          
  from   APP_MVR_INFORMATION             
  where  CUSTOMER_ID = @CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and  DRIVER_ID=@DRIVERID         
   and (year(getdate())- year(MVR_DATE))<3  and IS_ACTIVE='Y'  
 )        
 select @VIOLATION_ID=isnull(A.VIOLATION_ID,0),@MVR_DATE=MVR_DATE ,  
 @VIOLATION_DES=(VIOLATION_DES + ' (' + CAST(MVR_POINTS AS VARCHAR) +  '/' +  CAST(SD_POINTS AS VARCHAR)  +  ')' )         
 from   APP_MVR_INFORMATION A   
inner join MNT_VIOLATIONS B on A.VIOLATION_ID =B.VIOLATION_ID            
 where  CUSTOMER_ID = @CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and  DRIVER_ID=@DRIVERID          
  and APP_MVR_ID=@APPMVRID     
 --     
 if(@intSD_PONITS > 5)    
 begin     
  set @SD_POINTS='Y'    
 end     
 else    
 begin     
  set @SD_POINTS='N'     
 end    
 select @SD_POINTS as SD_POINTS ,@VIOLATION_ID as VIOLATION_ID,@MVR_DATE as MVR_DATE, @VIOLATION_DES as VIOLATION_DES     
end            
    
  




GO

