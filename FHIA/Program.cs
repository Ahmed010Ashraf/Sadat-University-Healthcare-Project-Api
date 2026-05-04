using BLL.Dtos;
using BLL.Dtos.errors;
using BLL.MappingProfile;
using BLL.ServiceAbstraction;
using BLL.ServiceImplementation;
using DAL.Context;
using DAL.Models;
using DAL.repositories.RepoAbstraction;
using DAL.repositories.RepoImplementation;
using FHIA.MiddleWares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;

namespace FHIA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Add permissive CORS policy (for development). Adjust for production.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT like: Bearer {your token}"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
                });
            });

            //allow dependancy injection for services and repos
            builder.Services.AddScoped(typeof(IGenericReposatory<,>), typeof(GenericReposatory<,>));
            builder.Services.AddScoped<IUOW, UOW>();
            builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
            builder.Services.AddScoped<IUserService ,UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IMedicalExaminationRequestService, MedicalExaminationRequestService>();
            builder.Services.AddScoped<IMedicalExaminationService, MedicalExaminationService>();
            builder.Services.AddScoped<IHospitalPaymentService, HospitalPaymentService>();
            builder.Services.AddScoped<IHospitalService, HospitalService>();

            builder.Services.AddScoped<IPrescriptionRequestService, PrescriptionRequestService>();
            builder.Services.AddScoped<IPrescriptionItemService, PrescriptionItemService>();
            builder.Services.AddScoped<IPrescriptionDispenseService, PrescriptionDispenseService>();
            builder.Services.AddScoped<IPrescriptionDispenseItemService, PrescriptionDispenseItemService>();
            builder.Services.AddScoped<IPharmacyService, PharmacyService>();
            builder.Services.AddScoped<IPharmacyPaymentService, PharmacyPaymentService>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();



            //configer mapping profile
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfilesAssemply).Assembly);


            //add dbcontext configurations    

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //configer jwt options
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            //configer identity 
            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                };
            });



            builder.Services.AddAuthorization();


            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new Errors
                        {
                            Key = e.Key,
                            ErrorMessages = e.Value.Errors.Select(er => er.ErrorMessage).ToList()
                        }).ToList();
                    var errorResponse = new ValidationErrors
                    {
                        Errors = errors,
                        message = "Validation Failed",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            // Enable CORS for incoming requests
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<CustomExceptionMiddleWare>();

            app.MapControllers();

            app.Run();
        }
    }
}
