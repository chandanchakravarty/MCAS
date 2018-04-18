/****** Object:  StoredProcedure [dbo].[Proc_GetMNT_OrgCategoryNameList]    Script Date: 01/12/2015 10:20:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_OrgCategoryNameList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_OrgCategoryNameList]
GO



/****** Object:  StoredProcedure [dbo].[Proc_GetMNT_OrgCategoryNameList]    Script Date: 01/12/2015 10:20:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE Proc [dbo].[Proc_GetMNT_OrgCategoryNameList]  
(  
@OrgCategory nvarchar(100) ,
@OrgCategoryName nvarchar(100) 
)  
as  
BEGIN  
SET FMTONLY OFF;  
  
IF @OrgCategoryName=''  AND @OrgCategory=''
Begin  
select a.DeductibleId,a.OrgCategoryName,a.OrgCategory,a.DeductibleAmt,a.EffectiveFrom,a.EffectiveTo,b.OrganizationName from MNT_Deductible a left join MNT_OrgCountry b on a.OrgCategoryName = b.CountryOrgazinationCode
END 
else IF @OrgCategoryName!='' AND @OrgCategory !=''
Begin  
select a.DeductibleId,a.OrgCategoryName,a.OrgCategory,a.DeductibleAmt,a.EffectiveFrom,a.EffectiveTo,b.OrganizationName from MNT_Deductible a left join MNT_OrgCountry b on a.OrgCategoryName = b.CountryOrgazinationCode  where b.CountryOrgazinationCode = @OrgCategoryName and  a.OrgCategory=@OrgCategory
END
ELSE  
Begin  
select a.DeductibleId,a.OrgCategoryName,a.OrgCategory,a.DeductibleAmt,a.EffectiveFrom,a.EffectiveTo,b.OrganizationName from MNT_Deductible a left join MNT_OrgCountry b on a.OrgCategoryName = b.CountryOrgazinationCode  where b.CountryOrgazinationCode = @OrgCategoryName OR  a.OrgCategory=@OrgCategory
END  
  
END 


GO