    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[MNT_MASTER_DETAIL]              
Created by      : SNEHA          
Date            : 24/10/2011                      
Purpose         :                    
Revison History :                      
Used In        :                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------            
*/       
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_MASTER_DETAIL]') AND type in (N'U'))
DROP TABLE [dbo].MNT_MASTER_DETAIL
GO
/****** Object:  Table [dbo].[MNT_MASTER_DETAIL]    Script Date: 10/22/2011 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].MNT_MASTER_DETAIL
(
	[TYPE_UNIQUE_ID]	[INT] NOT NULL,	
	[TYPE_ID]			[INT] NOT NULL,
	[TYPE_CODE]			[NVARCHAR](25) NULL,
	[TYPE_NAME]			[NVARCHAR](100) NULL,
	[ADDRESS]			[NVARCHAR](50) NULL,
	[CITY]				[NVARCHAR](25) NULL,
	[COUNTRY]			[NVARCHAR](25) NULL,
	[TEL_NO_OFF]		[NVARCHAR](25) NULL,
	[MOBILE_NO]			[NVARCHAR](25) NULL,
	[E_MAIL]			[NVARCHAR](25) NULL,
	[GST]				[DECIMAL](18,2) NULL,
	[CONTACT_PERSON]	[NVARCHAR](100) NULL,
	[PROVINCE]			[NVARCHAR](25) NULL,
	[POST_CODE]			[NVARCHAR](25) NULL,
	[TEL_NO_RES]		[NVARCHAR](25) NULL,
	[FAX_NO]			[NVARCHAR](25) NULL,
	[GST_REG_NO]		[NVARCHAR](25) NULL,
	[WITHHOLDING_TAX]	[DECIMAL](18,2) NULL,
	[STATUS]			[NVARCHAR](25) NULL,
	[SOLICITOR_TYPE]	[NVARCHAR](25) NULL,
	[PRIVATE_E_MAIL]	[NVARCHAR](25) NULL,
	[SURVEYOR_SOURCE]	[NVARCHAR](25) NULL,
	[CLASSIFICATION]	[NVARCHAR](25) NULL,
	[MEMO]				[NVARCHAR](25) NULL,
	[IS_ACTIVE]         [nchar](2) NULL,
	[ADDRESS1]           [NVARCHAR](50) NULL
	CONSTRAINT [PK_MNT_MASTER_DETAIL_TYPE_UNIQUE_ID] PRIMARY KEY CLUSTERED
    ([TYPE_UNIQUE_ID]ASC)
)
 
 ---------FOR TYPE_UNIQUE_ID------
	EXEC   sp_addextendedproperty 'Caption', 'TYPE UNIQUE ID', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_UNIQUE_ID
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_UNIQUE_ID
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_UNIQUE_ID
 ---------FOR TYPE_ID------
	EXEC   sp_addextendedproperty 'Caption', 'TYPE ID', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_ID
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_ID
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_ID
 ---------FOR TYPE_CODE------
	EXEC   sp_addextendedproperty 'Caption', 'TYPE CODE', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_CODE
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_CODE
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_CODE
 ---------FOR TYPE_NAME------
	EXEC   sp_addextendedproperty 'Caption', 'TYPE NAME', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_NAME
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_NAME
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TYPE_NAME
 ---------FOR ADDRESS------
	EXEC   sp_addextendedproperty 'Caption', 'ADDRESS', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS
 ---------FOR CITY------
	EXEC   sp_addextendedproperty 'Caption', 'CITY', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CITY
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CITY
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CITY    
 ---------FOR COUNTRY------
	EXEC   sp_addextendedproperty 'Caption', 'COUNTRY', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',COUNTRY
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',COUNTRY
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',COUNTRY    
 ---------FOR TEL_NO_OFF------
	EXEC   sp_addextendedproperty 'Caption', 'TEL NO OFF', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TEL_NO_OFF
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TEL_NO_OFF
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TEL_NO_OFF     
 ---------FOR MOBILE_NO------
	EXEC   sp_addextendedproperty 'Caption', 'MOBILE NO', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',MOBILE_NO
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',MOBILE_NO
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',MOBILE_NO             
  ---------FOR E_MAIL------
	EXEC   sp_addextendedproperty 'Caption', 'E MAIL', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',E_MAIL
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',E_MAIL
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',E_MAIL             
    ---------FOR GST------
	EXEC   sp_addextendedproperty 'Caption', 'GST', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',GST
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',GST
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',GST           
    ---------FOR CONTACT_PERSON------
	EXEC   sp_addextendedproperty 'Caption', 'CONTACT PERSON', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CONTACT_PERSON
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CONTACT_PERSON
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CONTACT_PERSON       
    ---------FOR PROVINCE------
	EXEC   sp_addextendedproperty 'Caption', 'PROVINCE', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',PROVINCE
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',PROVINCE
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',PROVINCE      
    ---------FOR POST_CODE------
	EXEC   sp_addextendedproperty 'Caption', 'POST CODE', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',POST_CODE
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',POST_CODE
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',POST_CODE          
    ---------FOR TEL_NO_RES------
	EXEC   sp_addextendedproperty 'Caption', 'TEL NO RES', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TEL_NO_RES
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TEL_NO_RES
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',TEL_NO_RES     
    ---------FOR FAX_NO------
	EXEC   sp_addextendedproperty 'Caption', 'FAX NO', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',FAX_NO
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',FAX_NO
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',FAX_NO       
    ---------FOR GST_REG_NO------
	EXEC   sp_addextendedproperty 'Caption', 'GST REG NO', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',GST_REG_NO
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',GST_REG_NO
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',GST_REG_NO      
    ---------FOR WITHHOLDING_TAX------
	EXEC   sp_addextendedproperty 'Caption', 'WITHHOLDING TAX', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',WITHHOLDING_TAX
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',WITHHOLDING_TAX
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',WITHHOLDING_TAX
    ---------FOR STATUS------
	EXEC   sp_addextendedproperty 'Caption', 'STATUS', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',STATUS
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',STATUS
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',STATUS  
    ---------FOR SOLICITOR_TYPE------
	EXEC   sp_addextendedproperty 'Caption', 'SOLICITOR TYPE', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',SOLICITOR_TYPE
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',SOLICITOR_TYPE
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',SOLICITOR_TYPE      
    ---------FOR PRIVATE_E_MAIL------
	EXEC   sp_addextendedproperty 'Caption', 'PRIVATE E MAIL', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',PRIVATE_E_MAIL
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',PRIVATE_E_MAIL
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',PRIVATE_E_MAIL
    --------FOR SURVEYOR_SOURCE------
	EXEC   sp_addextendedproperty 'Caption', 'SURVEYOR SOURCE', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',SURVEYOR_SOURCE
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',SURVEYOR_SOURCE
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',SURVEYOR_SOURCE  
    ---------FOR CLASSIFICATION------
	EXEC   sp_addextendedproperty 'Caption', 'CLASSIFICATION', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CLASSIFICATION
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CLASSIFICATION
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',CLASSIFICATION      
    ---------FOR MEMO------
	EXEC   sp_addextendedproperty 'Caption', 'MEMO', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',MEMO
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',MEMO
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',MEMO      
    ---------FOR IS_ACTIVE------
	EXEC   sp_addextendedproperty 'Caption', 'IS ACTIVE', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',IS_ACTIVE
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',IS_ACTIVE
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',IS_ACTIVE
    --------FOR ADDRESS1---------------
    EXEC   sp_addextendedproperty 'Caption', 'ADDRESS', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS1
    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS1
    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS1
-----------------------

--IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='MNT_MASTER_DETAIL' AND COLUMN_NAME='ADDRESS1')
--BEGIN
--ALTER TABLE MNT_MASTER_DETAIL
--ADD ADDRESS1 [NVARCHAR](50) NULL
--END
--	EXEC   sp_addextendedproperty 'Caption', 'ADDRESS', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS1
--    EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS1
--    EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_DETAIL', 'column',ADDRESS1