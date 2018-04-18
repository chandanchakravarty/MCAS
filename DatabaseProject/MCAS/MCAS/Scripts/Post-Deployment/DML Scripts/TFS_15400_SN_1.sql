IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Claim_Reserve]') AND type in (N'U'))
 BEGIN
     DROP TABLE Claim_Reserve
 END



