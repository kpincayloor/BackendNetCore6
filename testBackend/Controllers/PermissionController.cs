using DataAccess.Entity;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace testBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PermissionController : ControllerBase
    {
        private readonly IQueryHandler<Permission> _permissionQueryHandler;        
        private readonly ICommandHandler<Permission> _permissionCommandHandler;
        public PermissionController(IQueryHandler<Permission> permissionQueryHandler, ICommandHandler<Permission> permissionCommandHandler)
        {
            this._permissionQueryHandler = permissionQueryHandler;
            this._permissionCommandHandler = permissionCommandHandler;
        }
        // GET: Permissions
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _permissionQueryHandler.GetPermissions();
            if (result.ToList().Count() == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }
        // POST: Request Permission
        [HttpPost]        
        public async Task<IActionResult> Post(Permission permission)
        {
            try
            {
                var result = await _permissionCommandHandler.Execute(permission);
                if (result != null)
                {
                    if (result.Status)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }

                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }            
        }
        // PUT: Modify Permission
        [HttpPut]
        public async Task<IActionResult> Put(Permission permission)
        {
            var result = await _permissionCommandHandler.ExecuteUpdate(permission);
            if (result.Status)
            {                
                return Ok(result);
            } else
            {
                return BadRequest(result);
            }
        }

    }
}
