IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateApplicationGenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateApplicationGenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc dbo.Proc_UpdateApplicationGenInfo    
  
CREATE   PROC dbo.Proc_UpdateApplicationGenInfo      
(      
 @CUSTOMER_ID     int,      
 @APP_ID     int,      
 @APP_VERSION_ID     smallint,      
 @PARENT_APP_VERSION_ID     smallint,      
 @APP_STATUS     nvarchar(10),      
 @APP_NUMBER     nvarchar(150),      
 @APP_VERSION     nvarchar(6),      
 @APP_TERMS     nvarchar(10),      
 @APP_INCEPTION_DATE     datetime=null,      
 @APP_EFFECTIVE_DATE     datetime,      
 @APP_EXPIRATION_DATE     datetime,      
 @APP_LOB     nvarchar(10),      
 @APP_SUBLOB     nvarchar(10),      
 @CSR     int,      
 @UNDERWRITER     int,      
 @IS_UNDER_REVIEW     nchar(2),      
 @APP_AGENCY_ID     smallint,      
 @IS_ACTIVE     nchar(2),      
 @MODIFIED_BY     int,      
 @LAST_UPDATED_DATETIME     datetime,      
 @COUNTRY_ID int,      
 @STATE_ID int,      
 @DIV_ID     smallint,      
 @DEPT_ID     smallint,      
 @PC_ID     smallint,      
 @BILL_TYPE_ID     integer =null,   
 @COMPLETE_APP     char(1),      
 @PROPRTY_INSP_CREDIT int,      
 @INSTALL_PLAN_ID int,      
 @CHARGE_OFF_PRMIUM varchar(5),      
 @RECEIVED_PRMIUM decimal(18,2),      
 @PROXY_SIGN_OBTAINED int,      
 @POLICY_TYPE int,      
 @YEAR_AT_CURR_RESI int =null,      
 @YEARS_AT_PREV_ADD varchar(250) =null,  
 @PIC_OF_LOC int,  
 @IS_HOME_EMP    bit,  
 @PRODUCER int,  
 @DOWN_PAY_MODE int =null  
      
)      
AS      
BEGIN      
DECLARE @AGENCY_BILL_MORTAGAGEE SMALLINT,@INSURED_BILL_MORTAGAGEE SMALLINT,@MORTAGAGEE_INCEPTION SMALLINT  
 SET @AGENCY_BILL_MORTAGAGEE = 11277          
 SET @INSURED_BILL_MORTAGAGEE = 11278          
 SET @MORTAGAGEE_INCEPTION = 11276 

DECLARE @BILL_TYPE varchar(2)  
SELECT @BILL_TYPE  = TYPE FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID= @BILL_TYPE_ID  
--changed by Pravesh on 3rd April 2008 by Pravesh as now it applicable only for Mortegagee since inception
--if (@BILL_TYPE_ID <> @AGENCY_BILL_MORTAGAGEE  and @BILL_TYPE_ID <> @INSURED_BILL_MORTAGAGEE and @BILL_TYPE_ID <> @MORTAGAGEE_INCEPTION) 
if (@BILL_TYPE_ID <> @MORTAGAGEE_INCEPTION) 
  begin
	UPDATE APP_LIST SET DWELLING_ID=0,ADD_INT_ID=0 WHERE CUSTOMER_ID =@CUSTOMER_ID AND      
			 APP_ID=@APP_ID AND      
 			APP_VERSION_ID=@APP_VERSION_ID  
	--update Add interest tables
	if (@APP_LOB='1' or @APP_LOB ='6')
		update APP_HOME_OWNER_ADD_INT    SET BILL_MORTAGAGEE=null
			WHERE CUSTOMER_ID =@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
 end
 UPDATE APP_LIST      
 SET       
       
 PARENT_APP_VERSION_ID=@PARENT_APP_VERSION_ID,      
 APP_STATUS=@APP_STATUS,      
 APP_NUMBER=@APP_NUMBER,      
 APP_VERSION=@APP_VERSION,      
 APP_TERMS=@APP_TERMS,      
 APP_INCEPTION_DATE=@APP_INCEPTION_DATE,      
 APP_EFFECTIVE_DATE=@APP_EFFECTIVE_DATE,      
 APP_EXPIRATION_DATE=@APP_EXPIRATION_DATE,      
 --APP_LOB=@APP_LOB,      
 APP_SUBLOB=@APP_SUBLOB,      
 CSR=@CSR,      
 UNDERWRITER=@UNDERWRITER,      
 IS_UNDER_REVIEW=@IS_UNDER_REVIEW,      
 APP_AGENCY_ID=@APP_AGENCY_ID,      
 IS_ACTIVE=@IS_ACTIVE,      
 MODIFIED_BY=@MODIFIED_BY,      
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,      
 COUNTRY_ID= @COUNTRY_ID,      
 --STATE_ID= @STATE_ID,      
 DIV_ID=@DIV_ID,      
 DEPT_ID=@DEPT_ID ,      
 PC_ID =@PC_ID,      
 BILL_TYPE_ID= @BILL_TYPE_ID,      
 BILL_TYPE= @BILL_TYPE,      
 COMPLETE_APP= @COMPLETE_APP ,      
 PROPRTY_INSP_CREDIT= @PROPRTY_INSP_CREDIT ,      
 INSTALL_PLAN_ID=  @INSTALL_PLAN_ID,      
 CHARGE_OFF_PRMIUM=@CHARGE_OFF_PRMIUM,      
 RECEIVED_PRMIUM=@RECEIVED_PRMIUM,      
 PROXY_SIGN_OBTAINED=@PROXY_SIGN_OBTAINED,      
 POLICY_TYPE=@POLICY_TYPE,      
 YEAR_AT_CURR_RESI=@YEAR_AT_CURR_RESI,      
 YEARS_AT_PREV_ADD=@YEARS_AT_PREV_ADD,  
 PIC_OF_LOC=@PIC_OF_LOC  ,  
 IS_HOME_EMP  =@IS_HOME_EMP,  
 PRODUCER=@PRODUCER,  
 DOWN_PAY_MODE=@DOWN_PAY_MODE  
       
 WHERE      
 CUSTOMER_ID =@CUSTOMER_ID AND      
 APP_ID=@APP_ID AND      
 APP_VERSION_ID=@APP_VERSION_ID      


END    
    
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  





GO

