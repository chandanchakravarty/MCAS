IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_GENERATE_APP_NUMBER_MIG]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_GENERATE_APP_NUMBER_MIG]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<ATUL KAUMSR SINGH>
-- Create date: <2011-03-03>
-- Description:	<CREATE APPLICATION NUMBER>
-- =============================================

-- DROP FUNCTION  [func_GENERATE_APP_NUMBER]
CREATE FUNCTION [dbo].[func_GENERATE_APP_NUMBER_MIG]
(
 @APP_LOB int,      
 @AGENCY_ID int  =NULL      
)
RETURNS VARCHAR(20)
AS
BEGIN
	declare @override_lob_prefix char(1)      
declare @APPLICATIONNUMBER varchar(12)    
declare @found bit    
    
select @override_lob_prefix=override_lob_prefix from mnt_lob_master where lob_id=@APP_LOB      
      
      
-- if LOB is overidable then check if agency supplied matches any record present in the mnt_lob_agency table against the selected lob and agency.       
declare @agency_present char(1)      
if (@override_lob_prefix ='1')      
 begin      
  if exists(select * from mnt_lob_agency where lob_id=@app_lob and agency_id=@agency_id)      
   begin      
    set @agency_present='1'      
   end      
  else      
   begin      
    set @agency_present='0'        
   end      
     
 end      
 else      
  begin      
    set @agency_present='0'        
  end      
      
      
-- Check if any application exists with the current LOB      
if exists (SELECT * FROM APP_LIST WHERE APP_LOB=@APP_LOB)      
      
 BEGIN      
-- If the LOB has overidable prefix and the  agency is present against the lob in the relation table ,,  then join with MNT_LOB_AGENCY  table to replace with overridden prefix      
if (@override_lob_prefix ='1'  and @agency_present='1')      
     begin      
    
             SELECT @APPLICATIONNUMBER=UPPER(RTRIM(MNT_LOB_AGENCY.LOB_PREFIX)) -- + REPLICATE('0', 7 - DATALENGTH(CAST(ISNULL(MAX(SUBSTRING(APP_LIST.APP_NUMBER, 2, 7)) + 1, 1000001) AS varchar(7))))  -- WILL BE REQUIRED LATER IF PADDING WITH ZERO IS REQUIRED      
                              + CAST(ISNULL(MAX(SUBSTRING(APP_LIST.APP_NUMBER, 2, 7)) + 1, cast( isnull(MNT_LOB_MASTER.LOB_SEED,0) as varchar(7))) AS varchar(7)) + 'APP' --AS APPLICATIONNUMBER      
             FROM         APP_LIST       
             INNER JOIN     MNT_LOB_MASTER ON APP_LIST.APP_LOB = MNT_LOB_MASTER.LOB_ID       
             INNER JOIN     MNT_LOB_AGENCY ON MNT_LOB_MASTER.LOB_ID = MNT_LOB_AGENCY.LOB_ID      
             WHERE     (APP_LIST.APP_LOB = @APP_LOB)       
                         AND (SUBSTRING(APP_LIST.APP_NUMBER, 1, 1) = MNT_LOB_AGENCY.LOB_PREFIX) AND ISNUMERIC(SUBSTRING(app_number,2,7))=1     
                         --- AND (MNT_LOB_AGENCY.AGENCY_ID = @AGENCY_ID)      
             GROUP BY MNT_LOB_AGENCY.LOB_PREFIX ,MNT_LOB_MASTER.LOB_SEED      
    
/*   this query will be used if the application has to be generated irrespective of the agency prefix      
SELECT     UPPER(RTRIM(MNT_LOB_AGENCY.LOB_PREFIX))               -- + REPLICATE('0', 7 - DATALENGTH(CAST(ISNULL(MAX(SUBSTRING(APP_LIST.APP_NUMBER, 2, 7)) + 1, 1000001) AS varchar(7))))  -- WILL BE REQUIRED LATER IF PADDING WITH ZERO IS REQUIRED      
                         + CAST(ISNULL(MAX(SUBSTRING(APP_LIST.APP_NUMBER, 2, 7)) + 1, cast( isnull(MNT_LOB_MASTER.LOB_SEED,0) as varchar(7))) AS varchar(7)) + 'APP' AS APPLICATIONNUMBER      
             FROM         APP_LIST       
             INNER JOIN  MNT_LOB_MASTER ON APP_LIST.APP_LOB = MNT_LOB_MASTER.LOB_ID       
             INNER JOIN  MNT_LOB_AGENCY ON MNT_LOB_MASTER.LOB_ID = MNT_LOB_AGENCY.LOB_ID      
             WHERE     (APP_LIST.APP_LOB = @APP_LOB)       
             GROUP BY MNT_LOB_AGENCY.LOB_PREFIX ,MNT_LOB_MASTER.LOB_SEED */      
    
                
end      
    else     
 begin       
  SELECT   @APPLICATIONNUMBER = UPPER(LOB_PREFIX)  -- +REPLICATE('0', 7-datalength(cast(isnull(MAX(substring(app_number,2,7) )+1,1000001) as varchar(7))) )  -- WILL BE REQUIRED LATER IF PADDING WITH ZERO IS REQUIRED      
  + cast(isnull(MAX(SUBSTRING(app_number,2,7))+1,cast( isnull(MNT_LOB_MASTER.LOB_SEED,0) as varchar(7))) as varchar(7)) +'APP' --as APPLICATIONNUMBER      
  FROM         APP_LIST INNER JOIN      
       MNT_LOB_MASTER ON APP_LIST.APP_LOB = MNT_LOB_MASTER.LOB_ID      
  WHERE APP_LIST.APP_LOB= @APP_LOB   AND ISNUMERIC(SUBSTRING(app_number,2,7))=1      
  GROUP BY  LOB_PREFIX ,MNT_LOB_MASTER.LOB_SEED      
 end         
 END      
      
else      
 -- Check if no application exists with the current LOB then number will be generated using mnt_lob_master and mnt_lob_agency (if required)      
 BEGIN      
  if (@override_lob_prefix ='1'  and @agency_present='1')      
   begin      
              
    SELECT   @APPLICATIONNUMBER=  UPPER(RTRIM(MNT_LOB_AGENCY.LOB_PREFIX)) +cast( isnull(MNT_LOB_MASTER.LOB_SEED,0) as varchar(7)) +  'APP'  --AS APPLICATIONNUMBER      
    FROM  MNT_LOB_MASTER       
    INNER JOIN MNT_LOB_AGENCY ON MNT_LOB_MASTER.LOB_ID = MNT_LOB_AGENCY.LOB_ID      
    WHERE     (MNT_LOB_MASTER.LOB_ID  = @APP_LOB) and  (MNT_LOB_AGENCY.AGENCY_ID =@AGENCY_ID)              
    GROUP BY MNT_LOB_AGENCY.LOB_PREFIX,MNT_LOB_MASTER.LOB_SEED      
   end      
  else      
   begin       
    SELECT   @APPLICATIONNUMBER = LOB_PREFIX +cast( isnull(MNT_LOB_MASTER.LOB_SEED,0) as varchar(7)) + 'APP' --as APPLICATIONNUMBER      
    FROM      MNT_LOB_MASTER       
    where LOB_ID =@APP_LOB       
   end      
 END    
     
--check if policy exists corresponding this app number by pravesh    
set @found=0    
--declare @increment int     
--set @increment=0    
    
set @APPLICATIONNUMBER=replace(@APPLICATIONNUMBER,'APP','')    
while(@found=0)    
begin    
 if not exists(select POLICY_NUMBER from pol_customer_policy_list with(nolock) where POLICY_NUMBER=@APPLICATIONNUMBER)   
    and   
    not exists(select APP_NUMBER from pol_customer_policy_list with(nolock) where APP_NUMBER=@APPLICATIONNUMBER+'APP')  
    begin  
    set @found=1   
 end    
 else    
  begin    
   set @APPLICATIONNUMBER = SUBSTRING(@APPLICATIONNUMBER, 1, 1) + CAST(ISNULL(MAX(SUBSTRING(@APPLICATIONNUMBER, 2, 7)) + 1, cast( '0000000' as varchar(7))) AS varchar(7))     
   --set @increment=@increment+1    
  end    
    
end    
    
RETURN (@APPLICATIONNUMBER + 'APP' )    
	

END

GO

