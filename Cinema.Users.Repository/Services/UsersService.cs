using Cinema.Users.Repository.Core;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using UserService.Messages;

namespace Cinema.Mediator.Services;

public class UsersService : UserService.UserService.UserServiceBase
{
    private readonly ILogger<UsersService> _logger;
    private readonly UsersRepository _db;
    
    public UsersService(ILogger<UsersService> logger, UsersRepository db)
    {
        _logger = logger;
        _db = db;
    }
    public override async Task<UserReply> Auth(AuthRequest request, ServerCallContext context)
    {
        var reply = new UserReply(statusCode: (int)StatusCode.OK);
        var user = _db.Data.FirstOrDefault(s => s.Username == request.Username);
        
        if (user == null)
        {
            reply.StatusCode = (int)StatusCode.NotFound;
            return reply;
        }

        reply.User = new User
        {
            Id = user.Id,
            Username = user.Username,
            Password = ByteString.CopyFrom(user.Password),
            Status = user.Status
        };
        return reply;
    }

    public override async Task<UserReply> Register(RegisterRequest request, ServerCallContext context)
    {
        var reply = new UserReply
        {
            StatusCode = (int)StatusCode.OK
        };
        try
        {
            _logger.LogInformation($"Getted to register {request.Username}");
            if (_db.Data.FirstOrDefault(s => s.Username == request.Username) != null)
            {
                reply.StatusCode = (int)StatusCode.InvalidArgument;
                _logger.LogInformation($"Same user already registrated");
                return reply;
            }

            await _db.AddDataAsync(new OnlineCinema.Domain.User()
            {
                Username = request.Username,
                Password = request.Password.ToByteArray(),
                Status = request.Status
            });
            var user = _db.Data.FirstOrDefault(user => user.Username == request.Username);
            if (user == null)
            {
                reply.StatusCode = (int)StatusCode.NotFound;
                return reply;
            }

            reply.User = new User
            {
                Id = user.Id,
                Username = user.Username,
                Password = ByteString.CopyFrom(user.Password),
                Status = user.Status
            };
            return reply;
        }
        catch(Exception e)
        {
            _logger.LogCritical($"{e.Message}");
            reply.StatusCode = (int)StatusCode.Aborted;
            return reply;
        }
    }
}