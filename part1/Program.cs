using EmbedIO;
using EmbedIO.Actions;
using System;

namespace embediotest
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://+:9696/";
			using (var server = CreateWebServer(url))
			{
			    // Once we've registered our modules and configured them, we call the RunAsync() method.
			    server.RunAsync();

			    var browser = new System.Diagnostics.Process()
			    {
			        StartInfo = new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }
			    };
			    browser.Start();
			    // Wait for any key to be pressed before disposing of our web server.
			    // In a service, we'd manage the lifecycle of our web server using
			    // something like a BackgroundWorker or a ManualResetEvent.
			    Console.ReadKey(true);
			}
        }

        private static WebServer CreateWebServer(string url)
		{
		    return new WebServer(o => o
		        .WithUrlPrefix(url))
		        .WithModule(new ActionModule("/",
		            HttpVerbs.Any, 
		            ctx => ctx.SendStringAsync(
		                    "<h1>Hello EmbedIO!</h1>", 
		                    "text/html", 
		                    System.Text.Encoding.UTF8
		                    )));
		}
    }
}
