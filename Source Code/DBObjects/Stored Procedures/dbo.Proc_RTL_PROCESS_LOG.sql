IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_RTL_PROCESS_LOG]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_RTL_PROCESS_LOG]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*               
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_RTL_PROCESS_LOG                          
Modified by      : Pradeep Kushwaha                     
Date            : 01/Nov/2010                                    
Purpose         : Added to more columns ()
Revison History :                                    
Used In        : Ebix Advantage                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                    
drop proc Proc_RTL_PROCESS_LOG    
exec Proc_RTL_PROCESS_LOG     
*/                  
--drop proc dbo.Proc_RTL_PROCESS_LOG  
CREATE proc [dbo].[Proc_RTL_PROCESS_LOG]   
(  
@DEPOSIT_ID int,  
@POLICY_NUMBER varchar(20)=null,  
@ACTIVITY_DESCRIPTION varchar(1000),  
@STATUS varchar(20),  
@FILE_NAME varchar(200),  
@ADDITIONAL_INFO  varchar (1000),  
@AMOUNT varchar(10),
@OUR_NUMBER  nvarchar(13)=null,
@LATE_FEE  nvarchar(10)=null,
@COINS_CARRIER_LEADER nvarchar(10)=null,
@POLICY_HOLDER_NAME nvarchar(50)=null,
@SUSEP_CLASS_OF_BUSINESS nvarchar(10)=null,
@LEADER_POLICY_ID nvarchar(10)=null,
@LEADER_DOC_ID nvarchar(10)=null,
@BRANCH_COINS_ID nvarchar(10)=null,
@COINSURANCE_ID nvarchar(10)=null,
@STATUS_ID int=null,
@ADDITIONAL_INFO_ID int=null,
@ACTIVITY_DESCRIPTION_ID int =null,
--Co-Insurence 
@PAYMENT_DATE DATETIME=NULL,
@RISK_PREMIUM DECIMAL(25,2)=NULL,
@COMMISSION_AMOUNT DECIMAL(25,2)=NULL,
@FEE DECIMAL(25,2)=NULL,
@INTEREST DECIMAL(25,2)=NULL,
@INSTALLMENT_NO INT=NULL,
@NO_OF_INSTALLMENTS INT=NULL
)  
as   
begin  
  
SELECT @FILE_NAME = ISNULL(RTL_FILE,'') FROM ACT_CURRENT_DEPOSITS with(nolock) WHERE DEPOSIT_ID=@DEPOSIT_ID  
  
INSERT INTO RTL_PROCESS_LOG        
 (        
 [DEPOSIT_ID],[POLICY_NUMBER], [ACTIVITY_DESCRIPTION], [START_DATETIME], [STATUS], [FILE_NAME],  
 [ADDITIONAL_INFO] ,[AMOUNT],[OUR_NUMBER],[LATE_FEE],
 [COINS_CARRIER_LEADER],[POLICY_HOLDER_NAME],[SUSEP_CLASS_OF_BUSINESS],
 [LEADER_POLICY_ID],[LEADER_DOC_ID],[BRANCH_COINS_ID],[COINSURANCE_ID],[STATUS_ID],[ADDITIONAL_INFO_ID],[ACTIVITY_DESCRIPTION_ID],
 PAYMENT_DATE,RISK_PREMIUM,COMMISSION_AMOUNT,FEE,INTEREST,NO_OF_INSTALLMENTS,INSTALLMENT_NO
 )        
 VALUES        
 (        
    @DEPOSIT_ID , @POLICY_NUMBER, @ACTIVITY_DESCRIPTION, getdate(), @STATUS, @FILE_NAME,       
	@ADDITIONAL_INFO , @AMOUNT ,@OUR_NUMBER,@LATE_FEE,
	@COINS_CARRIER_LEADER,@POLICY_HOLDER_NAME,@SUSEP_CLASS_OF_BUSINESS,
	@LEADER_POLICY_ID,@LEADER_DOC_ID,@BRANCH_COINS_ID,@COINSURANCE_ID,@STATUS_ID,@ADDITIONAL_INFO_ID ,@ACTIVITY_DESCRIPTION_ID,
	@PAYMENT_DATE,@RISK_PREMIUM,@COMMISSION_AMOUNT,@FEE,@INTEREST,@NO_OF_INSTALLMENTS,@INSTALLMENT_NO)        
  
end  
  
  
  
  
  
  
  
  
  
  
GO

