using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace UrlMinify.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        public string minifiedUrl = string.Empty;

        //Base URL
        public string AppBaseUrl => $"{Request.Scheme}://{Request.Host.Value}/";


        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {

        }


        /// <summary>
        /// gets original url and navigate to url
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetNavigate2Async(string key)
        {
            var response = await postSimple<string>("realUrl", key);

            if (response.IsSuccessStatusCode)
            {
                var uri = response.Content.ReadAsStringAsync().Result;
                return Redirect(uri);
            }
            return Page();
        }

        /// <summary>
        /// post original url for generate unique Encrypted key
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string url) {

            var response = await postSimple<string>("minifyUrl", url);

            if (response.IsSuccessStatusCode)
            {

                minifiedUrl = response.Content.ReadAsStringAsync().Result;
            }

            return Page();
        }


        /// <summary>
        /// Simple post to Web Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Url"></param>
        /// <param name="content"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> postSimple<T>(string Url, T content, string controller = "Base")
        {

            HttpClient client = httpClientFactory.CreateClient("api");
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

            HttpResponseMessage response = await client.PostAsJsonAsync(controller + "/" + Url, content);


            return response;
        }
    }
}