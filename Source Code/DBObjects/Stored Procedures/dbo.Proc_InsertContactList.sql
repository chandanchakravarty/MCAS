IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertContactList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertContactList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------    
Proc Name       : dbo.ContactList    
Created by      : Ajit Singh Chahal    
Date            : 3/11/2005    
Purpose       :To insert Record in Contact List table.    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC Dbo.Proc_InsertContactList    
CREATE  PROC [dbo].[Proc_InsertContactList]    
(    
@CONTACT_CODE     nchar(20),    
@CONTACT_TYPE_ID     smallint,    
@CONTACT_SALUTATION     nvarchar(20),    
@CONTACT_POS     nvarchar(500),    
@INDIVIDUAL_CONTACT_ID     int,    
@CONTACT_FNAME     nvarchar(200), --changed by amit for itrack 1561 (TFS bug#554)   
@CONTACT_MNAME     nvarchar(15),    
@CONTACT_LNAME     nvarchar(75),    
@CONTACT_ADD1     nvarchar(70),    
@CONTACT_ADD2     nvarchar(70),    
@CONTACT_CITY     nvarchar(20),    
@CONTACT_STATE     nvarchar(5),    
@CONTACT_ZIP     nvarchar(20),    
@CONTACT_COUNTRY     nvarchar(25),    
@CONTACT_BUSINESS_PHONE    nvarchar(40),    
@CONTACT_EXT     nvarchar(5),    
@CONTACT_FAX     nvarchar(40),    
@CONTACT_MOBILE     nvarchar(40),    
@CONTACT_EMAIL     nvarchar(50),    
@CONTACT_PAGER     nvarchar(20),    
@CONTACT_HOME_PHONE     nvarchar(20),    
@CONTACT_TOLL_FREE     nvarchar(20),    
@CONTACT_NOTE     nvarchar(256),    
@CONTACT_AGENCY_ID     int,    
@IS_ACTIVE     nchar(2),    
@CREATED_BY     int,    
@CREATED_DATETIME     datetime,    
@MODIFIED_BY     int,    
@LAST_UPDATED_DATETIME     datetime,    
@CONTACT_ID int output ,
@DATE_OF_BIRTH datetime,
@CPF_CNPJ NVARCHAR(40),
@REGIONAL_IDENTIFICATION NVARCHAR(40),
@REG_ID_ISSUE_DATE datetime,
@ACTIVITY INT,
@NUMBER NVARCHAR(40),  
@DISTRICT NVARCHAR(40),   
@REG_ID_ISSUE NVARCHAR(40), 
@REGIONAL_ID_TYPE  NVARCHAR(100),
@NATIONALITY NVARCHAR(100)  
)    
AS    
	BEGIN    
		Declare @Count int    
		Set @Count= (SELECT count(CONTACT_ID) FROM MNT_CONTACT_LIST WHERE CONTACT_CODE =@CONTACT_CODE)    
		    
			if(@Count=0)    
				BEGIN    
					INSERT INTO MNT_CONTACT_LIST    
					(    
					CONTACT_CODE,    
					CONTACT_TYPE_ID,    
					CONTACT_SALUTATION,    
					CONTACT_POS,    
					INDIVIDUAL_CONTACT_ID,    
					CONTACT_FNAME,    
					CONTACT_MNAME,    
					CONTACT_LNAME,    
					CONTACT_ADD1,    
					CONTACT_ADD2,    
					CONTACT_CITY,    
					CONTACT_STATE,    
					CONTACT_ZIP,    
					CONTACT_COUNTRY,    
					CONTACT_BUSINESS_PHONE,    
					CONTACT_EXT,    
					CONTACT_FAX,    
					CONTACT_MOBILE,    
					CONTACT_EMAIL,    
					CONTACT_PAGER,    
					CONTACT_HOME_PHONE,    
					CONTACT_TOLL_FREE,    
					CONTACT_NOTE,    
					CONTACT_AGENCY_ID,    
					IS_ACTIVE,    
					CREATED_BY,    
					CREATED_DATETIME,    
					MODIFIED_BY,    
					LAST_UPDATED_DATETIME ,
					DATE_OF_BIRTH,
                    CPF_CNPJ,
                    REGIONAL_IDENTIFICATION,
                    REG_ID_ISSUE_DATE,
                     ACTIVITY,
                     NUMBER,
                     DISTRICT,
                    REG_ID_ISSUE,
                    REGIONAL_ID_TYPE,
                    NATIONALITY       
					)    
					VALUES    
					(    
					@CONTACT_CODE,    
					@CONTACT_TYPE_ID,    
					@CONTACT_SALUTATION,    
					@CONTACT_POS,    
					@INDIVIDUAL_CONTACT_ID,    
					@CONTACT_FNAME,    
					@CONTACT_MNAME,    
					@CONTACT_LNAME,    
					@CONTACT_ADD1,    
					@CONTACT_ADD2,    
					@CONTACT_CITY,    
					@CONTACT_STATE,    
					@CONTACT_ZIP,    
					@CONTACT_COUNTRY,    
					@CONTACT_BUSINESS_PHONE,    
					@CONTACT_EXT,    
					@CONTACT_FAX,    
					@CONTACT_MOBILE,    
					@CONTACT_EMAIL,    
					@CONTACT_PAGER,    
					@CONTACT_HOME_PHONE,    
					@CONTACT_TOLL_FREE,    
					@CONTACT_NOTE,    
					@CONTACT_AGENCY_ID,    
					@IS_ACTIVE,    
					@CREATED_BY,    
					@CREATED_DATETIME,    
					@MODIFIED_BY,    
					@LAST_UPDATED_DATETIME,
					@DATE_OF_BIRTH,
                    @CPF_CNPJ,
                    @REGIONAL_IDENTIFICATION,
                    @REG_ID_ISSUE_DATE,
                    @ACTIVITY,
                    @NUMBER,
                    @DISTRICT,
                    @REG_ID_ISSUE,
                    @REGIONAL_ID_TYPE,
                    @NATIONALITY      
					)    
					select @CONTACT_ID = @@identity
				END    
			ELSE    
				BEGIN    
					/*Record already exist*/    
					  SELECT @CONTACT_ID = -1 
				END    
	END    
    
  
  
GO

