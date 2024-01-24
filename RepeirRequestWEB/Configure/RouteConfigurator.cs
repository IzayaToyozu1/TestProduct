using RepairRequestDB.AdditionalProperties;
using RepairRequestDB.Model;
using System;
using RepairRequestDB;
using TestProduct.TestNewMethod;

namespace RepairRequestWEB.Configure
{
    public class RouteConfigurator
    {
        private static string? _connect;

        public static void ConfigureRoutes(WebApplication routes)
        {
            _connect = routes.Configuration.GetConnectionString("DefaultConnection");
            
            routes.MapHub<ChatHub>("/Chat");

            ConfigureCustomRoute(routes, "/RepairRequest", "html/repairRequest.html");

            ConfigureApiRoutes(routes);
        }

        private static void ConfigureCustomRoute(WebApplication? routes, string path, string filePath)
        {
            routes.Map(path, appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    await next();
                });

                appBuilder.Run(async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.SendFileAsync(filePath);
                });
            });
        }

        private static void ConfigureApiRoutes(IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/GetRequest", (string start, string end,
                int typeService, bool withAccess) =>
            {
                var context = new DBContext(_connect);
                DbParametrProc paramProc = new DbParametrProc(
                    new[] { "@start", "@end", "@typeService", "@WithAccess" },
                    new object[] { DateTime.Parse(start), DateTime.Parse(end), typeService, withAccess });
                var repeirRequsts = context.ProcGetData<RequestRepair>(paramProc);
                context.Dispose();
                return Results.Json(repeirRequsts);
            });

            routes.MapGet("/api/GetMessChatRequest", (int idRequest) =>
            {
                RequestMessage[] result;
                using (var context = new DBContext(_connect))
                {
                    var paramProc = new DbParametrProc(
                        new[] { "@id" },
                        new object[] { idRequest });
                    result = context.ProcGetData<RequestMessage>(paramProc);
                }
                return Results.Json(result);
            });

            routes.MapGet("/api/GetCommentRequest", (int idRequestRepair) =>
            {
                var context = new DBContext(_connect);
                var paramProc = new DbParametrProc(
                    new[] { "@id_RequestRepair" },
                    new object[] { idRequestRepair });
                var requestComments
                    = context.ProcGetData<RequestComment>(paramProc);
                context.Dispose();
                return Results.Json(requestComments);
            });

            routes.MapPost("/api/SetCommentRequest", (int idRequest, string comment) =>
            {
                int idUser = 0;
                var context = new DBContext(_connect);
                var paramProc = new DbParametrProc(
                    new string[3] { "@id_RequestRepair", "@Comment", "@id_user" },
                    new object[] { idRequest, comment, idUser });
                context.ProcSetData<RequestComment>(paramProc);
                context.Dispose();
            });

            routes.MapPost("/api/SetComment", (int id, bool type, string comment, int idWriter) =>
            {
                var context = new DBContext(_connect);
                var paramProc = new DbParametrProc(
                    new[] { "@id", "@type", "@comment", "@idWriter" },
                    new object[] { id, type, comment, idWriter });
                context.ProcSetData<RequestComment>(paramProc);
                context.Dispose();
            });
        }
    }
}
