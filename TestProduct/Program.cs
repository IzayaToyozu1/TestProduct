using Microsoft.AspNetCore.Builder;
using System;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using TestProduct.DB;
using TestProduct.Model;
using TestProduct.TestNewMethod;

namespace TestProduct
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            var connect = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddSignalR();
            

            builder.Services.AddDbContext<ApplicationContext>(
                option => option.UseSqlServer(connect));
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors();


            app.MapHub<ChatHub>("/Chat");

            app.Map("/KekaChat", appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    await next();
                });

                appBuilder.Run(async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("html/chatpage.html");
                });
            });

            app.Map("/index", appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    await next();
                });

                appBuilder.Run(async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync("html/repairRequest.html");
                });
            });

            app.MapGet("/api/GetRequestAll",(string start, string end, 
                int typeService, bool withAccess) =>
            {
                ApplicationRepairRequestContext context = 
                    new ApplicationRepairRequestContext
                        ("Server=192.168.5.85\\K21;" +
                         "Database=dbase1;" +
                         "User Id=SuperUser;" +
                         "Password=fate;" +
                          "MultipleActiveResultSets = True; " +
                            "TrustServerCertificate = True"
                         );
                DbParametrProc paramProc = new DbParametrProc(
                    new[] { "@start", "@end", "@typeService", "@WithAccess" },
                    new object[] { DateTime.Parse(start), DateTime.Parse(end), typeService, withAccess },
                    new[] { DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Boolean });
                var repeirRequsts = context.GetItems<RequestRepair>(paramProc, "Repair.GetRepairRequests");
                context.Dispose();
                return Results.Json(repeirRequsts);
            });


            #region
            //app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseMiddleware<RoutingMiddleware>();

            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/html; charset=utf-8";
            //    //var timeService = app.Services.GetService<ITimeService>();
            //    //await context.Response.WriteAsync($"Time: {timeService.GetTime()}");
            //    if (context.Request.Path == "/index")
            //    {
            //        await context.Response.SendFileAsync("html/index.html");
            //    }

            //    if (context.Request.Path == "/api/keka")
            //    {
            //        var message = "incorrect data";
            //        if (context.Request.HasJsonContentType())
            //        {
            //            var jsonoption = new JsonSerializerOptions();
            //            jsonoption.Converters.Add(new PersonConverter());
            //            var kek = await context.Request.ReadFromJsonAsync<Person>(jsonoption);
            //            if (kek != null)
            //                message = kek.ToString();
            //        }

            //        await context.Response.WriteAsJsonAsync(new { text = message });
            //    }

            //    if (context.Request.Path == "/KekaChat")
            //    {
            //        await context.Response.SendFileAsync("html/chatpage.html");
            //    }
            //});

            #endregion
            app.Run();
        }
    }

    public class Person
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} Age: {Age}";
        }
    }

    public class PersonConverter : JsonConverter<Person>
    {
        public override Person? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var personName = "Undefined";
            var personAge = 0;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        // если свойство age и оно содержит число
                        case "Keka1" when reader.TokenType == JsonTokenType.Number:
                            personAge = reader.GetInt32();  // считываем число из json
                            break;
                        // если свойство age и оно содержит строку
                        case "Keka1" when reader.TokenType == JsonTokenType.String:
                            string? stringValue = reader.GetString();
                            // пытаемся конвертировать строку в число
                            if (int.TryParse(stringValue, out int value))
                            {
                                personAge = value;
                            }
                            break;
                        case "Keka":    // если свойство Name/name
                            string? name = reader.GetString();
                            if (name!=null)
                                personName = name;
                            break;
                    }
                }
            }
            return new Person{ Name = personName, Age = personAge};
        }

        public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Keka", person.Name);
            writer.WriteNumber("Keka1", person.Age);

            writer.WriteEndObject();
        }
    }
}