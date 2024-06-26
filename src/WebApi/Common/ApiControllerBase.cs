using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Common;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;
    protected ISender Sender => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
