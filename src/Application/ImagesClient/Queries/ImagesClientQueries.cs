using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.ImagesClient.Queries
{
    public class ImagesClientQueries : IRequest<ImageClientVM>
    {
        public string querySearch { get; set; } = "Lune";
        public int pageNumber { get; set; } = 1;
    }


    public class ImagesClientQueriesHandler : IRequestHandler<ImagesClientQueries, ImageClientVM>
    {
        private readonly IImageSearchClient _httpClientConfigure;
        private const int pageSize = 50;
        private const bool autoCorrect = false;

        
        public ImagesClientQueriesHandler(IImageSearchClient imageSearchWebClient)
        {
            _httpClientConfigure=imageSearchWebClient;
        }
        public async Task<ImageClientVM> Handle(ImagesClientQueries request, CancellationToken cancellationToken)
        {
            var response = await _httpClientConfigure.ConfigureClient(request.querySearch, request.pageNumber, pageSize, autoCorrect);
            if (response.StatusCode >= HttpStatusCode.BadRequest) throw new Exception(response.ReasonPhrase);
            var result= await _httpClientConfigure.RetrieveDataClientWeb(response) ?? throw new NotFoundException("there is no images available");
          return result;
        }
    }
}
