

/****** Object:  StoredProcedure [dbo].[Proc_Insert_MNT_Deductible]    Script Date: 01/09/2015 17:51:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_MNT_Deductible]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_MNT_Deductible]
GO



/****** Object:  StoredProcedure [dbo].[Proc_Insert_MNT_Deductible]    Script Date: 01/09/2015 17:51:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Proc_Insert_MNT_Deductible]
@p_OrgCategory nvarchar(50),
@p_OrgCategoryName nvarchar(100),
@p_DeductibleAmt decimal,
@p_EffectiveFrom datetime,
@p_EffectiveTo datetime,
@p_ModifiedDate datetime,
@p_CreatedDate datetime,
@p_CreatedBy nvarchar(50),
@p_ModifiedBy nvarchar(50)
AS
BEGIN
Insert into MNT_Deductible (OrgCategory,OrgCategoryName,DeductibleAmt,EffectiveFrom,EffectiveTo,ModifiedDate,CreatedDate,CreatedBy,ModifiedBy)
values(@p_OrgCategory,@p_OrgCategoryName,@p_DeductibleAmt,@p_EffectiveFrom,@p_EffectiveTo,@p_ModifiedDate,@p_CreatedDate,@p_CreatedBy,@p_ModifiedBy)
END

GO


