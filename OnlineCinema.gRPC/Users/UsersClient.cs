using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using UserService.Messages;

namespace OnlineCinema.gRPC.Users;

public class UsersClient
{
    private UserService.UserService.UserServiceClient Client { get; init; }
    private ILogger<UsersClient> _logger;
    private GrpcChannel Channel;
    
    public UsersClient(ILogger<UsersClient> logger = null)
    {
        _logger = logger;

        var httpHandler = new HttpClientHandler();

        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
		
        Channel  = GrpcChannel.ForAddress("https://localhost:7280", 
            new GrpcChannelOptions { HttpHandler = httpHandler });

        Client = new UserService.UserService.UserServiceClient(Channel);
    }

    public async Task<UserReply> AuthAsync(string username, byte[] password, 
        CancellationToken token = default)
    {
        var request = new AuthRequest
        {
            Username = username,
            Password = ByteString.CopyFrom(password)
        };
        var response = await Client.AuthAsync(request);
        return response;
    }

    public async Task<UserReply> RegisterAsync(string username, byte[] password,
        CancellationToken token = default)
    {
        var request = new RegisterRequest
        {
            Username = username,
            Password = ByteString.CopyFrom(password)
        };
        var response = await Client.RegisterAsync(request);
        return response;
    }

    public async Task<UserReply> UpdateAsync()
    {
        throw new NotImplementedException("шнга");
    }
}