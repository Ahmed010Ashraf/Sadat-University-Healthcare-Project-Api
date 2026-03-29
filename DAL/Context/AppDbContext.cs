using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
    {
       
        //public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        //{
            
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<HospitalPayment> HospitalsPayments { get; set; }

        public DbSet<MedicalExamination> MedicalExaminations { get; set; }

        public DbSet<MedicalExaminationRequest> MedicalExaminationRequests { get; set; }

        public DbSet<Pharmacy> Pharmacies { get; set; }

        public DbSet<PharmacyPayment> PharmacyPayments { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<PrescriptionDispense> PrescriptionsDispense { get; set; }
        public DbSet<PrescriptionDispenseItem> PrescriptionsDispenseItems { get; set; }

        public DbSet<PrescriptionItem> PrescriptionsItems { get; set; }

        public DbSet<PrescriptionRequest> PrescriptionsRequests { get; set; }
    }
}
