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
        /// Get a collection of employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<EmployeeResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCollectionOfEmployees()
        {
            var employeesCollection = await _employeeService.GetCollectionOfEmployees();

            if (!employeesCollection.Any())
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<List<EmployeeResponseDto>>(employeesCollection);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Create an employee.
        /// </summary>
        /// <param name="employeeRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequestDto employeeRequestDto)
        {
            if (!ModelState.IsValid) // Fluent validation is a better choice :).
            {
                return BadRequest(ModelState);
            }

            var employee = await _employeeService.CreateEmployee(employeeRequestDto);

            if (employee == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<EmployeeResponseDto>(employee);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Update a employee (regular, manager or ceo).
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeUpdateRequestDto"></param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int employeeId, [FromBody] EmployeeUpdateRequestDto employeeUpdateRequestDto)
        {
            if (!ModelState.IsValid) // Fluent validation is a better choice :).
            {
                return BadRequest(ModelState);
            }

            var employee = await _employeeService.UpdateEmployee(employeeId, employeeUpdateRequestDto);

            if (employee == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<EmployeeResponseDto>(employee);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Delete an employee.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete("{employeeId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int employeeId)
        {
            var deleteEmployee = await _employeeService.DeleteEmployee(employeeId);

            if (!deleteEmployee)
            {
                return UnprocessableEntity();
            }

            return Ok(deleteEmployee);
        }

    }
}
