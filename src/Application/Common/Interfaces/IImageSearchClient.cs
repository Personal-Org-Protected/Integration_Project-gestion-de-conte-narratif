using Application.ImagesClient.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IImageSearchClient
    {
        Task<HttpResponseMessage> ConfigureClient(string querySearch, int pageNumber, int pageSize, bool autoCorrect);
        Task<ImageClientVM> RetrieveDataClientWeb(HttpResponseMessage response);
    }
}
