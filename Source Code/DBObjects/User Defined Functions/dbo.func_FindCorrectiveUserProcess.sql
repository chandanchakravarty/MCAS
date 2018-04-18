IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_FindCorrectiveUserProcess]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_FindCorrectiveUserProcess]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
Create By : Pravesh K. Chandel  
dated  : 14 may 2007  
purpose  : to find Any Corrective user process Commited on Policy  
select dbo.func_FindCorrectiveUserProcess (736,7,7)  
drop function dbo.func_FindCorrectiveUserProcess  
*/  

create function dbo.func_FindCorrectiveUserProcess(@CUSTOMER_ID INT,@POLICY_ID INT ,@POLICY_VERSION_ID int) RETURNS Int  
as  
begin  
 declare @CORRECTIVE_USER_POLICY_VERSION_ID int  
 declare @CORRECTIVE_USER_PRE_POLICY_VERSION_ID int  
  
 set @CORRECTIVE_USER_POLICY_VERSION_ID=@POLICY_VERSION_ID  
   
 if exists(select process_id from POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID =@CUSTOMER_ID  
  AND POLICY_ID =@POLICY_ID and policy_version_id=@POLICY_VERSION_ID-1  
  AND PROCESS_ID=9 )  
  begin  
          select @CORRECTIVE_USER_POLICY_VERSION_ID=POLICY_VERSION_ID from POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID =@CUSTOMER_ID  
    AND POLICY_ID =@POLICY_ID and policy_version_id=@POLICY_VERSION_ID-1  
    AND PROCESS_ID=9   
    if (@CORRECTIVE_USER_POLICY_VERSION_ID>1)  
     begin     
      if exists(select process_id from POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID =@CUSTOMER_ID  
      AND POLICY_ID = @POLICY_ID  
      AND PROCESS_ID=9 and POLICY_VERSION_ID=@CORRECTIVE_USER_PRE_POLICY_VERSION_ID-1)  
      set @CORRECTIVE_USER_PRE_POLICY_VERSION_ID = dbo.func_FindCorrectiveUserProcess(@CUSTOMER_ID,@POLICY_ID,@CORRECTIVE_USER_PRE_POLICY_VERSION_ID-1)  
      else  
       set @CORRECTIVE_USER_PRE_POLICY_VERSION_ID=@CORRECTIVE_USER_POLICY_VERSION_ID-1  
     end  
    else  
    set @CORRECTIVE_USER_PRE_POLICY_VERSION_ID=@CORRECTIVE_USER_POLICY_VERSION_ID  
     
   --set @CORRECTIVE_USER_PRE_POLICY_VERSION_ID =  dbo.func_FindCorrectiveUserProcess(@CUSTOMER_ID,@POLICY_ID,@CORRECTIVE_USER_POLICY_VERSION_ID)    
  end  
 else  
  set @CORRECTIVE_USER_PRE_POLICY_VERSION_ID=@POLICY_VERSION_ID  
  
return @CORRECTIVE_USER_PRE_POLICY_VERSION_ID  
end  
  


GO

