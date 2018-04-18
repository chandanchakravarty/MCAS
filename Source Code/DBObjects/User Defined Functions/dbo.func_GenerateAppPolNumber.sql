IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_GenerateAppPolNumber]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_GenerateAppPolNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE function [dbo].[func_GenerateAppPolNumber]    --19,291,'2010-11-18','POL',
(    
 @POLICY_LOB int  ,    
 @DIV_ID int  ,    
 @ISSUE_DATE DATETIME  ,  
 @CALLED_FROM NVARCHAR(10) = POL,
 @PolicyNo NVARCHAR(10)   
)  RETURNS varchar(1000)    
AS    
BEGIN      
 DECLARE @CARRIER_SUSEP_CODE NVARCHAR(5),    
  @ISSUE_YEAR VARCHAR(4),    
  @BRANCH_CODE NVARCHAR(2),    
  @SUSEP_LOB_CODE NVARCHAR(4),    
  @APP_NUMBER_SQUENCE VARCHAR(6),    
  @POL_APP_NUMBER VARCHAR(21) ,  
  @APPLICATIONNUMBER VARCHAR(21)    
SET @CARRIER_SUSEP_CODE =''  
  
--FETCHING CARRIER SUSEP CODE 
SELECT @CARRIER_SUSEP_CODE=504.5
 
--SELECT @CARRIER_SUSEP_CODE=LEFT(ISNULL(SUSEP_NUM,''),5)   
--FROM MNT_SYSTEM_PARAMS MNT WITH(NOLOCK)  
--Join MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK)  
--ON REIN.REIN_COMAPANY_ID=MNT.SYS_CARRIER_ID  
  
SET @ISSUE_YEAR = CONVERT(VARCHAR,YEAR(@ISSUE_DATE))    
    
select @BRANCH_CODE=BRANCH_CODE from MNT_DIV_LIST WITH(NOLOCK) WHERE DIV_ID=@DIV_ID    
select @SUSEP_LOB_CODE=SUSEP_LOB_CODE from MNT_LOB_MASTER WITH(NOLOCK) WHERE LOB_ID=@POLICY_LOB    
    
SET @POL_APP_NUMBER = @CARRIER_SUSEP_CODE + @ISSUE_YEAR +  LEFT(@BRANCH_CODE,2) + LEFT(@SUSEP_LOB_CODE ,4)    
    
/* OLD LOGIC

--If the LOB has pOLICY    
if exists (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)    
   WHERE POLICY_LOB=@POLICY_LOB    
   AND substring(CASE @CALLED_FROM WHEN 'APP' THEN APP_NUMBER ELSE POLICY_NUMBER END,6,4)= @ISSUE_YEAR  
  )        
BEGIN        
--   08737-2009-01-0196-000001 -aPP nUMBER FORMAT    
  --SELECT   @APP_NUMBER_SQUENCE = RIGHT('000000'+  cast(MAX(CAST(substring(  
  --CASE @CALLED_FROM  WHEN 'APP'   
  --THEN APP_NUMBER ELSE POLICY_NUMBER END,16,6) AS INT))+1 as varchar(7)),6)    
  --FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)    
  --WHERE POL_CUSTOMER_POLICY_LIST.POLICY_LOB =    @POLICY_LOB    
    
  --AND substring(CASE @CALLED_FROM WHEN 'APP' THEN APP_NUMBER ELSE POLICY_NUMBER END,6,4)= @ISSUE_YEAR  
END    
ELSE    
BEGIN     
 SELECT   @APP_NUMBER_SQUENCE =  SUBSTRING(CAST( ISNULL(MNT_LOB_MASTER.LOB_SEED,0) AS VARCHAR(7)),2,6)     
 FROM     MNT_LOB_MASTER WITH(NOLOCK)         
 WHERE LOB_ID =@POLICY_LOB     
 END
 
*/   
  
 SET @APP_NUMBER_SQUENCE = RIGHT('000000'+ @PolicyNo,6)
  
     
 --SELECT @POL_APP_NUMBER + @APP_NUMBER_SQUENCE AS APPLICATIONNUMBER    
 SET @APPLICATIONNUMBER = @POL_APP_NUMBER + @APP_NUMBER_SQUENCE   
 declare @found bit  
 set @found=0  
while(@found=0)    
begin    
 if not exists(select POLICY_NUMBER from pol_customer_policy_list with(nolock) where POLICY_NUMBER=@APPLICATIONNUMBER)   
    and   
    not exists(select APP_NUMBER from pol_customer_policy_list with(nolock) where APP_NUMBER=@APPLICATIONNUMBER)  
    begin  
    set @found=1   
 end    
 else    
  begin    
   set @APP_NUMBER_SQUENCE = replace(str(@APP_NUMBER_SQUENCE +1,6),' ','0')   
   set @APPLICATIONNUMBER = @POL_APP_NUMBER + @APP_NUMBER_SQUENCE   
  end    
    
end   
  
--SELECT @APPLICATIONNUMBER AS APPLICATIONNUMBER     
RETURN  @APPLICATIONNUMBER    
    
END     
   -- select dbo.[func_GenerateAppPolNumber](19,291,'2010-11-18','POL',11) as 'dfg'
GO

