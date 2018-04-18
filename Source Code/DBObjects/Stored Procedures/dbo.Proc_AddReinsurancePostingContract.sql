IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AddReinsurancePostingContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AddReinsurancePostingContract]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_AddReinsuranceContract  
Created by      : Shafee  
Date            : 4/1/2005  
Purpose     :       
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    

create  PROC Dbo.Proc_AddReinsurancePostingContract   
(  
@GL_ID INT,
@CONTRACT_ID smallint,  
@COMMISION_APPLICABLE char(1),  
@REIN_PREMIUM_ACT int,  
@REIN_PAYMENT_ACT int,  
@REIN_COMMISION_ACT int,  
@REIN_COMMISION_RECEVABLE int  
)  
AS  
Begin  
--Generate Primary Key  
Declare @REIN_POSTING_ID smallint  
select @REIN_POSTING_ID= count(*) +1 from MNT_REINSURANCE_POSTING   
--Insert Record  
  
insert into MNT_REINSURANCE_POSTING values  
(  
 @REIN_POSTING_ID,@CONTRACT_ID,@COMMISION_APPLICABLE,@REIN_PREMIUM_ACT,  
 @REIN_PAYMENT_ACT,@REIN_COMMISION_ACT,@REIN_COMMISION_RECEVABLE,@GL_ID  
)  
  
  
  
End  
  




GO

