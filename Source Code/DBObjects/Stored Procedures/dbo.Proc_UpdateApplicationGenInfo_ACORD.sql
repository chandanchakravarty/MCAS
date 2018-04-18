IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateApplicationGenInfo_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateApplicationGenInfo_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.ApplicationGenInfo    
Created by      : Nidhi    
Date            : 4/28/2005    
Purpose       :Update    
Revison History :    
Used In        : Wolverine    
    
Modified By : Anurag Verma    
Modified On : 13/07/2005    
Purpose  : updating policy type field     
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE     PROC Dbo.Proc_UpdateApplicationGenInfo_ACORD    
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
 @STATE_CODE NVarChar(100),    
 @DIV_ID     smallint,    
 @DEPT_ID     smallint,    
 @PC_ID     smallint,    
 @BILL_TYPE     char(2),    
 @COMPLETE_APP     char(1),    
 @PROPRTY_INSP_CREDIT     char(1),    
 @INSTALL_PLAN_ID int,    
 @CHARGE_OFF_PRMIUM varchar(5),    
 @RECEIVED_PRMIUM decimal(18,2),    
 @PROXY_SIGN_OBTAINED int,    
 @POLICY_TYPE int,    
 @POLICY_TYPE_CODE NVarChar(10),    
@YEAR_AT_CURR_RESI real =null,    
@YEARS_AT_PREV_ADD varchar(250) =null    
    
)    
AS    
BEGIN    
 DECLARE @STATE_ID Int    
 DECLARE @LOB_ID SmallInt    
    
 EXECUTE @LOB_ID = Proc_GetLOBID @APP_LOB     
     
 IF ( @LOB_ID = 0 )    
 BEGIN    
  RAISERROR ('LOB not found in LOB_MASTER', 16, 1)    
   RETURN    
 END    
     
 EXECUTE @STATE_ID = Proc_GetSTATE_ID_FROM_NAME 1,@STATE_CODE    
     
 EXECUTE @POLICY_TYPE = Proc_GetLookupID_FROM_DESC 'HOPTYP',@POLICY_TYPE_CODE    
 
 SET @PROXY_SIGN_OBTAINED = 10964
	
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
 APP_LOB=@LOB_ID,    
 APP_SUBLOB=@APP_SUBLOB,    
 CSR=@CSR,    
 UNDERWRITER=@UNDERWRITER,    
 IS_UNDER_REVIEW=@IS_UNDER_REVIEW,    
 APP_AGENCY_ID=@APP_AGENCY_ID,    
 IS_ACTIVE=@IS_ACTIVE,    
 MODIFIED_BY=@MODIFIED_BY,    
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,    
 COUNTRY_ID= @COUNTRY_ID,    
 STATE_ID= @STATE_ID,    
 DIV_ID=@DIV_ID,    
 DEPT_ID=@DEPT_ID ,    
 PC_ID =@PC_ID,    
 BILL_TYPE= @BILL_TYPE,    
 COMPLETE_APP= @COMPLETE_APP ,    
 PROPRTY_INSP_CREDIT= @PROPRTY_INSP_CREDIT ,    
 INSTALL_PLAN_ID=  @INSTALL_PLAN_ID,    
 CHARGE_OFF_PRMIUM=@CHARGE_OFF_PRMIUM,    
 RECEIVED_PRMIUM=@RECEIVED_PRMIUM,    
 PROXY_SIGN_OBTAINED=@PROXY_SIGN_OBTAINED,    
 POLICY_TYPE=@POLICY_TYPE,    
YEAR_AT_CURR_RESI=@YEAR_AT_CURR_RESI,    
YEARS_AT_PREV_ADD=@YEARS_AT_PREV_ADD    
     
 WHERE    
 CUSTOMER_ID =@CUSTOMER_ID AND    
 APP_ID=@APP_ID AND    
 APP_VERSION_ID=@APP_VERSION_ID    
END  
  



GO

