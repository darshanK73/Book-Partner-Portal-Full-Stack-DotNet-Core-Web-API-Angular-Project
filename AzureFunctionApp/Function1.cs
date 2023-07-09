using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net;

namespace AzureFunctionApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Sql("dbo.titles","DefaultConnectionString")] IAsyncCollector<Title> title,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Titleview data = JsonConvert.DeserializeObject<Titleview>(requestBody);

            Title t = new Title()
            {
                title_id = data.title_id,
                title = data.title,
                type = data.type,
                pub_id = data.pub_id,
                price = data.price,
                advance = data.advance,
                royalty = data.royalty,
                ytd_sales = data.ytd_sales,
                notes = data.notes,
                pubdate = data.pubdate
            };



            await title.AddAsync(t);
            await title.FlushAsync();

            return new OkObjectResult(data.titleauthors);
        }

        [FunctionName("Function2")]
        public static async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Sql("dbo.titleauthor", "DefaultConnectionString")] IAsyncCollector<Titleauthor> titleauthor,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            List<Titleauthor> data = JsonConvert.DeserializeObject<List<Titleauthor>>(requestBody);

            foreach(Titleauthor item in data)
            {
                await titleauthor.AddAsync(item);
                await titleauthor.FlushAsync();
            }

            return new OkObjectResult("Title Added");
        }
    }
}
