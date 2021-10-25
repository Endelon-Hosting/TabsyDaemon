using System;
using System.Collections.Generic;
using System.Net;

using Newtonsoft.Json;

using TabsyDaemon.Logging;
using TabsyDaemon.Models;

namespace TabsyDaemon.Controller
{
    public class RepoController
    {
        public static List<TabsyImage> Images { get; set; } = new List<TabsyImage>();
        public static TabsyRepo Repo { get; set; }

        public static void LoadImages()
        {
            try
            {
                string repoUrl = Environment.ImageRepo;

                if (!repoUrl.EndsWith("/"))
                    repoUrl += "/";

                Repo = JsonConvert.DeserializeObject<TabsyRepo>(
                    HttpGet(repoUrl + "manifest.json")
                );

                Logger.Info($"Using image repo: {Repo.Name} v{Repo.Version} by {Repo.Author}");

                try
                {
                    foreach(string imagename in Repo.Images)
                    {
                        TabsyImage image = JsonConvert.DeserializeObject<TabsyImage>(
                            HttpGet($"{repoUrl}{imagename}/manifest.json")
                        );

                        Logger.Info($"Loaded image: {image.Name}");

                        Images.Add(image);
                    }

                    Logger.Info($"Loaded {Images.Count} images");
                }
                catch(Exception e)
                {
                    Logger.Error($"Unhandeld exception while reading image data: {e.Message}");
                }
            }
            catch(Exception e)
            {
                Logger.Error($"Unhandled exception while reading image repo data: {e.Message}");
            }
        }

        public static string HttpGet(string address)
        {
            WebClient wc = new WebClient();
            string result = "";

            try
            {
                result = wc.DownloadString(address);
            }
            catch(ArgumentNullException)
            {
                Logger.Error("No repo url was found");
            }
            catch(WebException e)
            {
                Logger.Error($"Error loading data from {address}: {e.Message}");
            }
            catch(Exception e)
            {
                Logger.Error($"Unexpected error loading data from {address}: {e.Message}");
            }

            wc.Dispose();

            return result;
        }
    }
}
