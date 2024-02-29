using AngularProjectAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AngularProjectAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        //replace localhost with yours
                        //also add your deployed website
                        policy.WithOrigins("http://localhost:4200",
                                            // you can put in MULTIPLE urls, this is A WAY to 
                                            // solve for multiple environments! more on that later
                                            "https://MyChatRoom.com")
                        .AllowAnyMethod() // method in this context means GET, POST, PUT, DELETE, etc. 
                        .AllowAnyHeader();
                    });
            });



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UpMeetDBContext>(options =>
            {                                                    // MAKE SURE Database = the NAME OF YOUR DATABASE
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}