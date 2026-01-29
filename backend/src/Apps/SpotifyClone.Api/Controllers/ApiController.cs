using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyClone.Api.Controllers;

[ApiController]
public abstract class ApiController(IMediator mediator) : ControllerBase
{
    protected IMediator Mediator { get; } = mediator;
}
