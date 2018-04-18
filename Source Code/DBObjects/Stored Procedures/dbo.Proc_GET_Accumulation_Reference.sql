IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_MNT_ACCUMULATION_REFERENCE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_MNT_ACCUMULATION_REFERENCE]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------
--Proc Name          : dbo.GET_MNT_ACCUMULATION_CRITERIA_MASTER
--Created by         : Kuldeep Saxena         
--Date               :  24 OCTOBER 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[GET_MNT_ACCUMULATION_REFERENCE]      
CREATE  PROCEDURE [dbo].[GET_MNT_ACCUMULATION_REFERENCE]      
(       
 @ACC_ID int
)        
AS       
BEGIN      
Select ACC_ID,ACC_REF_NO,LOB_ID,CRITERIA_ID,CRITERIA_VALUE,TREATY_CAPACITY_LIMIT,USED_LIMIT,Convert(varchar,EFFECTIVE_DATE,103) as EFFECTIVE_DATE,Convert(varchar,EXPIRATION_DATE,103) as EXPIRATION_DATE,IS_ACTIVE
from MNT_ACCUMULATION_REFERENCE
where ACC_ID=@ACC_ID
End