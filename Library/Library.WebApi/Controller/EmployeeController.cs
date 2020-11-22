using AutoMapper;
using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Controller
{
    [ApiController]
    [Route("v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create an employee.
        /// </summary>
        /// <param name="employeeRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult CreateEmployee([FromBody] EmployeeRequestDto employeeRequestDto)
        {
            var employee = _employeeService.CreateEmployee(employeeRequestDto);

            if (employee == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<EmployeeResponseDto>(employee);

            return Ok(mappedResult);
        }

    }
}
