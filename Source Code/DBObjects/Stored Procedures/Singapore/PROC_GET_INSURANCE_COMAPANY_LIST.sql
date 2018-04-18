IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_INSURANCE_COMAPANY_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_INSURANCE_COMAPANY_LIST]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PROC_GET_INSURANCE_COMAPANY_LIST]                                    
                              
AS                                    
BEGIN                                    
select REIN_COMAPANY_ID,REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST 
where REIN_COMPANY_TYPE=14550 and IS_ACTIVE='Y' and REIN_COMAPANY_ID!=1
END       

