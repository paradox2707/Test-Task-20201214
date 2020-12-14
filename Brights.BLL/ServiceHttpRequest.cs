using Brights.BLL.Abstract;
using Brights.BLL.DTO;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Brights.BLL
{
    public class ServiceHttpRequest : IServiceHttpRequest
    {
        private readonly HttpClient client;

        public ServiceHttpRequest(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<ResponseModel> RequestToUrl(string url)
        {
            url = url.Trim();
            if (url == string.Empty)
                throw new Exception("Url is empty");

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;

            var model = new ResponseModel();
            model.Url = url;

            try
            {
                var response = await client.GetAsync(url);
                model.StatusCode = (int)response.StatusCode;

                var pageContents = await response.Content.ReadAsStringAsync();
                HtmlDocument pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);
                var tagTitle = pageDocument.DocumentNode.SelectSingleNode("//head/title");
                if (tagTitle != null)
                {                    
                    var headlineText = tagTitle.InnerText;
                    model.Title = headlineText;
                }
                else
                {
                    if ((int)response.StatusCode == 200)
                        model.Title = "Title doesn`t exist";

                    else
                        model.Title = response.ReasonPhrase;
                }
                    
                
            }
            catch(Exception ex)
            {
                model.Title = ex.Message;
            }



            return model;
        }
    }
}
