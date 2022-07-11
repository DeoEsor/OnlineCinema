using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FilmsService;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace OnlineCinema.gRPC.Films;

public class FilmsClient
{
    private global::FilmsService.FilmsService.FilmsServiceClient Client { get; init; }
    private ILogger<FilmsClient> _logger;
    private readonly GrpcChannel Channel;

    public FilmsClient(ILogger<FilmsClient> logger = null)
    {
        _logger = logger;

        var httpHandler = new HttpClientHandler();

        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
		
        Channel  = GrpcChannel.ForAddress("https://localhost:7280", 
            new GrpcChannelOptions { HttpHandler = httpHandler });

        Client = new global::FilmsService.FilmsService.FilmsServiceClient(Channel);
    }

    public async Task<FilmResponse> GetFilmAsync(int id, 
                                                    CancellationToken token = default)
    {
        var request = new TittleGetByIDRequest
        {
            Id = id
        };
        var responce  = await Client.GetFilmAsync(request);
        return responce;
    }

    public async Task<FilmsResponse> GetFilmByNameAsync(string name,
                                            CancellationToken token = default)
    {
        var request = new TittleGetByNameRequest
        {
            Name = name
        };
        var response = await Client.GetFilmByNameAsync(request);

        return response;
    }

    public async Task<FilmResponse> AddFilmAsync()
    {
        throw new NotImplementedException("тут какая-то шняга");
        var request = new FilmRequest
        {
            FilmData = new Film
            {
                 
            }
        };

        var response = await Client.AddFilmAsync(request);
        return response;
    }

    public async Task<FilmResponse> UpdateFilmAsync()
    {
        throw new NotImplementedException("тут какая-то шняга");
        var request = new FilmRequest
        {
            FilmData = new Film
            {
                
            }
        };

        var responce = await Client.UpdateFilmAsync(request);
        return responce;
    }
}