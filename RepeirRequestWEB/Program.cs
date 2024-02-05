using System.Text.Json;
using System.Text.Json.Serialization;
using RepairRequestDB;
using RepairRequestDB.Model;
using RepairRequestWEB.Configure;
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

           RouteConfigurator.ConfigureRoutes(app);
            
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