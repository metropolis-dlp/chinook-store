using ChinookStore.Application.Employees.Queries.GetAllEmployees;
using Microsoft.AspNetCore.Mvc;

namespace ChinookStore.Web.Controllers;

[Route("api/v1/employees")]
public class EmployeesController : ApiControllerBase
{
    [HttpGet("")]
    public async Task<ActionResult<EmployeeListItemDto[]>> Get()
    {
        return await Sender.Send(new GetAllEmployeesQuery());
    }
}
