using System;
using System.Collections.Generic;
using CustomerService.Attributes;
using CustomerService.Resources;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private EFUnitOfWork _efUnitOfWork;

        public CustomerController(EFUnitOfWork efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [ValidateActionParameters]
        [HttpGet("{id}/{email}")]
        public ActionResult<string> Get(long? id, string email)
        {
            if (id == null && email == null)
            {
                ModelState.AddModelError("", Errors.noInquiryCriteria);
                return BadRequest(ModelState);
            }
            else if (id != null && email == null)
            {                
                var result = _efUnitOfWork.Customers.Find(x => x.Id == (decimal)id);

                if (result != null)
                {
                    return Ok(result);
                }
            
                ModelState.AddModelError("", Errors.invalidCustomerId);
                return BadRequest(ModelState);
            }
            else if (id == null && email != null)
            {
                    var result = _efUnitOfWork.Customers.Find(x => 
                        string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase));

                if (result != null)
                {
                    return Ok(result);
                }

                ModelState.AddModelError("", Errors.invalidEmail);
                return BadRequest(ModelState);
            }

            return "value";
        }
    }
}