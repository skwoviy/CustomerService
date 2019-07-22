using CustomerService.Attributes;
using CustomerService.DTO;
using CustomerService.Resources;
using DAL.UnitOfWork;
using DbContext.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [ValidateResourceParameters]
        [HttpGet("id")]
        public ActionResult<string> Get(long? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, Errors.noInquiryCriteria);
                return BadRequest(ModelState);
            }
            else
            {
                var result = _efUnitOfWork.Customers.Find(x => x.Id == (decimal)id);

                if (result.Count() > 0)
                {
                    var response = GetResponse(result);
                    return Ok(response);
                }

                ModelState.AddModelError("id", Errors.invalidCustomerId);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("email")]
        public ActionResult<string> Get(string email)
        {
            if (email == null)
            {
                ModelState.AddModelError(string.Empty, Errors.noInquiryCriteria);
                return BadRequest(ModelState);
            }
            else
            {
                var result = _efUnitOfWork.Customers.Find(x =>
                    string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase));

                if (result.Count() > 0)
                {
                    var response = GetResponse(result);
                    return Ok(response);
                }

                ModelState.AddModelError("email", Errors.invalidEmail);
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        public ActionResult<string> Get(long? id, string email)
        {
            if (id == null && email == null)
            {
                ModelState.AddModelError(string.Empty, Errors.noInquiryCriteria);
                return BadRequest(ModelState);
            }

            var result = _efUnitOfWork.Customers.Find(x => x.Id == (decimal)id);

            if (result.Count() == 0)
            {
                result = _efUnitOfWork.Customers.Find(x =>
                    string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase));

                if (result.Count() == 0)
                {
                    ModelState.AddModelError(string.Empty, Errors.notFound);
                    return BadRequest(ModelState);
                }
            }

            var response = GetResponse(result);
            return Ok(response);
        }

        private IEnumerable<CustomerDTO> GetResponse(IEnumerable<Customer> data)
        {
            var response = (from items in data
                select new CustomerDTO
                {
                    CustomerId = (long)items.Id,
                    Name = items.Name,
                    Email = items.Email,
                    Mobile = (long)items.MobileNo,
                    Transactions = from tr in items.Transaction
                        where tr.CustomerId == items.Id
                        select new TransactionDTO
                        {
                            Id = tr.Id,
                            Date = tr.Date,
                            Amount = tr.Amount,
                            Currensy = tr.Currency.Code,
                            Status = tr.Status.Name
                        }
                });

            return response;
        }
    }
}