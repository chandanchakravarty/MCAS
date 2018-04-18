CREATE PROCEDURE [dbo].[PROC_MNT_Hospital_Save]
	@p_HospitalName [nvarchar](200),
	@p_HospitalAddress [nvarchar](300),
	@p_HospitalContactNo [nvarchar](40),
	@p_HospitalFaxNo [nvarchar](20),
	@p_ContactPersonName [nvarchar](50),
	@p_Email [nvarchar](50),
	@p_officeNo [nvarchar](20),
	@p_FaxNo [nvarchar](20),
	@p_CreatedBy [varchar](50),
	@p_CreatedDate [datetime],
	@p_ModifiedBy [varchar](50),
	@p_ModifiedDate [datetime],
	@p_HospitalAddress2 [nvarchar](300),
	@p_HospitalAddress3 [nvarchar](300),
	@p_City [nvarchar](30),
	@p_State [nvarchar](30),
	@p_Country [nvarchar](30),
	@p_PostalCode [nvarchar](30),
	@p_FirstContactPersonName [nvarchar](250),
	@p_MobileNo1 [nvarchar](30),
	@p_SecondContactPersonName [nvarchar](250),
	@p_EmailAddress2 [nvarchar](30),
	@p_OffNo2 [nvarchar](30),
	@p_MobileNo2 [nvarchar](50),
	@p_Fax2 [nvarchar](50),
	@p_HospitalType [nvarchar](2),
	@p_EffectiveFrom [datetime],
	@p_EffectiveTo [datetime],
	@p_Remarks [nvarchar](800),
	@p_Status [nvarchar](10)
WITH EXECUTE AS CALLER
AS
BEGIN  
 SET FMTONLY OFF; 
 Insert into MNT_Hospital (HospitalName,HospitalAddress,HospitalContactNo,HospitalFaxNo,ContactPersonName,Email,officeNo,FaxNo,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,HospitalAddress2,HospitalAddress3,City,[State],Country,PostalCode,FirstContactPersonName,MobileNo1,SecondContactPersonName,EmailAddress2,OffNo2,MobileNo2,Fax2,HospitalType,EffectiveFrom,EffectiveTo,Remarks,[Status])  
 values(@p_HospitalName,@p_HospitalAddress,@p_HospitalContactNo,@p_HospitalFaxNo,@p_ContactPersonName,@p_Email,@p_officeNo,@p_FaxNo,@p_CreatedBy,@p_CreatedDate,@p_ModifiedBy,@p_ModifiedDate,@p_HospitalAddress2,@p_HospitalAddress3,@p_City,@p_State,@p_Country,@p_PostalCode,@p_FirstContactPersonName,@p_MobileNo1,@p_SecondContactPersonName,@p_EmailAddress2,@p_OffNo2,@p_MobileNo2,@p_Fax2,@p_HospitalType,@p_EffectiveFrom,@p_EffectiveTo,@p_Remarks,@p_Status)  

end


