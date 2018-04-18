IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchReinsuranceCommissionValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchReinsuranceCommissionValues]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_FetchReinsuranceCommissionValues            
Created by      : Chetna Agarwal  
Date            : 03/05/2010            
Purpose			:To FETCH Default values of commission%,ceded%
Revison History :            
Used In			: Ebix Advantage            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_FetchReinsuranceCommissionValues

CREATE PROC [dbo].[Proc_FetchReinsuranceCommissionValues]
( 
@CONTRACT_ID INT
)
AS
BEGIN
select COMMISSION from MNT_REINSURANCE_CONTRACT WITH(NOLOCK) 
where CONTRACT_ID=@CONTRACT_ID
select REIN_CEDED from MNT_REIN_LOSSLAYER WITH(NOLOCK)  
where CONTRACT_ID=@CONTRACT_ID
END



GO

