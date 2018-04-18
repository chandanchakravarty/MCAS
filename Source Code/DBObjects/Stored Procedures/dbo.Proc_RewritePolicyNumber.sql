IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_RewritePolicyNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_RewritePolicyNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*============================================================================================  
Proc Name       : dbo.Proc_RewritePolicyNumber  
Created by      : Pravesh  
Date            : 7 march 2007  
Purpose       : To rewrite the policy number while rewriting the policy by rewrite Process  
Revison History :      
modified by		:pravesh k chandel
modified date	: 18 aprl 2008
purpose			: check if new generated policy number exists	
Used In    : Wolverine/clsRewriteProcess      
===============================================================================================  
Date     Review By          Comments      
====== ==============  =========================================================================  
drop proc   dbo.Proc_RewritePolicyNumber   
*/  
create proc dbo.Proc_RewritePolicyNumber  
(  
@CUSTOMER_ID   INT,   
@POLICY_ID   INT,  
@POLICY_VERSION_ID INT,  
@NEW_POLICY_NUMBER  VARCHAR(10) OUT  
)  
as  
BEGIN  
DECLARE @tmpNEW_POLICY_NUMBER VARCHAR(10)  
DECLARE @OLD_POLICY_NUMBER VARCHAR(10)  
DECLARE @POLICY_LOB NVARCHAR(5)  
SELECT @OLD_POLICY_NUMBER=POLICY_NUMBER,@POLICY_LOB=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID   
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
  
--SELECT @NEW_POLICY_NUMBER =SUBSTRING(@OLD_POLICY_NUMBER,1,1) + '7' + SUBSTRING(@OLD_POLICY_NUMBER,3,5) + '1'  
  
declare @Rewriteseed varchar(8)  
declare @LOB_PREFIX char(1)  
select @Rewriteseed=convert(varchar,REWRITE_SEED),@LOB_PREFIX=LOB_PREFIX from  MNT_LOB_MASTER where LOB_ID= @POLICY_LOB  
 select @tmpNEW_POLICY_NUMBER=UPPER(@LOB_PREFIX) + substring(@Rewriteseed,1,1) + convert(varchar,isnull(MAX(substring(POLICY_number,3,6))+1,substring(@Rewriteseed,2,6) ))  
    FROM POL_CUSTOMER_POLICY_LIST PL INNER JOIN    
                         MNT_LOB_MASTER ON PL.POLICY_LOB = MNT_LOB_MASTER.LOB_ID    
   WHERE PL.POLICY_LOB= @POLICY_LOB   
AND substring(POLICY_number,2,1)= substring(CONVERT(VARCHAR,MNT_LOB_MASTER.REWRITE_SEED),1,1) 
-- 
 
--check if policy exists corresponding this pol number --by pravesh
declare @found bit 
set @found=0
while(@found=0)
begin
	if not exists(select policy_number from pol_customer_policy_list with(nolock) where policy_number=@tmpNEW_POLICY_NUMBER)
		set @found=1
	else
		begin
			set @tmpNEW_POLICY_NUMBER = SUBSTRING(@tmpNEW_POLICY_NUMBER, 1, 2) + CAST(ISNULL(MAX(SUBSTRING(@tmpNEW_POLICY_NUMBER, 3, 6)) + 1, cast( '000000' as varchar(7))) AS varchar(7)) 
		end

end

SET @NEW_POLICY_NUMBER=@tmpNEW_POLICY_NUMBER  
UPDATE POL_CUSTOMER_POLICY_LIST SET POLICY_NUMBER=@NEW_POLICY_NUMBER WHERE CUSTOMER_ID=@CUSTOMER_ID   
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
  

END  
 




GO

