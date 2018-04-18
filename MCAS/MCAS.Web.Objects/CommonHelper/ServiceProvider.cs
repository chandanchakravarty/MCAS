using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAS.Entity;

namespace MCAS.Web.Objects.CommonHelper
{
    public class ServiceProvider : BaseModel
    {
        public string Title { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public List<ServiceProvider> GetServiceProvider()
        {
            MCASEntities db = new MCASEntities();

            var listProvider = (from c in db.MNT_Cedant.Where(t => t.Status.Equals("1"))
                                join d in db.MNT_Country on c.Country equals d.CountryShortCode

                                select new ServiceProvider()
                                {
                                    Title = c.CedantName,
                                    Description = "",
                                    Type = "Insurer",
                                    Address1 = c.Address,
                                    Address2 = c.Address2,
                                    Address3 = c.Address3,
                                    City = c.City,
                                    State = c.State,
                                    Country = d.CountryName,
                                    PostalCode = c.PostalCode
                                }).ToList();

            var lstSurveyor = (from c in db.MNT_Adjusters.Where(t => t.Status.Equals("Active") && t.AdjusterCode.Contains("SVY"))
                               join d in db.MNT_Country on c.Country equals d.CountryShortCode

                               select new ServiceProvider()
                               {
                                   Title = c.AdjusterName,
                                   Description = "",
                                   Type = "Surveyor",
                                   Address1 = c.Address1,
                                   Address2 = c.Address2,
                                   Address3 = c.Address3,
                                   City = c.City,
                                   Country = d.CountryName,
                                   PostalCode = c.PostCode
                               }).ToList();

            listProvider.AddRange(lstSurveyor);

            var lstLawer = (from c in db.MNT_Adjusters.Where(t => t.Status.Equals("Active") && t.AdjusterCode.Contains("SOL"))
                            join d in db.MNT_Country on c.Country equals d.CountryShortCode

                            select new ServiceProvider()
                            {
                                Title = c.AdjusterName,
                                Description = "",
                                Type = "Lawyer",
                                Address1 = c.Address1,
                                Address2 = c.Address2,
                                Address3 = c.Address3,
                                City = c.City,
                                Country = d.CountryName,
                                PostalCode = c.PostCode
                            }).ToList();

            listProvider.AddRange(lstLawer);

            var lstDepot = (from c in db.MNT_DepotMaster.Where(t => t.Status.Equals("1"))
                            join d in db.MNT_Country on c.Country equals d.CountryShortCode

                            select new ServiceProvider()
                            {
                                Title = c.CompanyName,
                                Description = "",
                                Type = "Workshop",
                                Address1 = c.Address1,
                                Address2 = c.Address2,
                                Address3 = c.Address3,
                                City = c.City,
                                State = c.State,
                                Country = d.CountryName,
                                PostalCode = c.PostalCode
                            }).ToList();

            listProvider.AddRange(lstDepot);

            var lstHospital = (from c in db.MNT_Hospital.Where(t => t.Status.Equals("1"))
                               join d in db.MNT_Country on c.Country equals d.CountryShortCode

                               select new ServiceProvider()
                               {
                                   Title = c.HospitalName,
                                   Description = "",
                                   Type = "Hospital",
                                   Address1 = c.HospitalAddress,
                                   Address2 = c.HospitalAddress2,
                                   Address3 = c.HospitalAddress3,
                                   City = c.City,
                                   State = c.State,
                                   Country = d.CountryName,
                                   PostalCode = c.PostalCode
                               }).ToList();

            listProvider.AddRange(lstHospital);

            var lstAdjuster = (from c in db.MNT_Adjusters.Where(t => t.Status.Equals("Active") && t.AdjusterCode.Contains("ADJ"))
                               join d in db.MNT_Country on c.Country equals d.CountryShortCode

                               select new ServiceProvider()
                               {
                                   Title = c.AdjusterName,
                                   Description = "",
                                   Type = "Adjuster",
                                   Address1 = c.Address1,
                                   Address2 = c.Address2,
                                   Address3 = c.Address3,
                                   City = c.City,
                                   Country = d.CountryName,
                                   PostalCode = c.PostCode
                               }).ToList();

            listProvider.AddRange(lstAdjuster);



            #region Commented

            //List<ServiceProvider> listProvider = new List<ServiceProvider>()
            //{
            //    new ServiceProvider()
            //    {
            //        Title = "Insurer Name",
            //        Latitude = "18.9409388",
            //        Longitude = "72.82819189999998",
            //        Description = "Nishant Sharma dfasdfsdf",
            //        Type = "Insurer"
            //    },
            //    new ServiceProvider()
            //    {
            //        Title = "Surveyor Name",
            //        Latitude = "18.9716956",
            //        Longitude = "72.80991180000001",
            //        Description = "Samsung dfasdfsdf",
            //        Type = "Surveyor"
            //    },
            //    new ServiceProvider()
            //    {
            //        Title = "Lawyer Name",
            //        Latitude = "19.0509488",
            //        Longitude = "72.8285644",
            //        Description = "dfasdfasdfsdf",
            //        Type = "Lawyer"
            //    },new ServiceProvider()
            //    {
            //        Title = "Workshop/Depot Name",
            //        Latitude = "19.1759668",
            //        Longitude = "72.79504659999998",
            //        Description = "dfasdfsdfsdf",
            //        Type = "Workshop"
            //    },new ServiceProvider()
            //    {
            //        Title = "Hospital Name",
            //        Latitude = "19.0883595",
            //        Longitude = "72.82652380000002",
            //        Description = "dfasdfasdfsdf",
            //        Type = "Hospital"
            //    },
            //    new ServiceProvider()
            //    {
            //        Title = "Adjuster Name",
            //        Latitude = "18.9542149",
            //        Longitude = "72.81203529999993",
            //        Description = "dfsdfsdfsdfsd",
            //        Type = "Adjuster"
            //    }
            //};

            #endregion



            return listProvider;
        }
    }
}
