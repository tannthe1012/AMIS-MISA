using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using System.Data;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore;
using MISA.Entity;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Constants;

namespace MISA.CukCuk.Api.Controllers
{
    /// <summary>
    /// Api Danh sách nhân viên
    /// Created By: NTTan (17/8/2021)
    /// </summary>
    [Route("api/v1/Employees")]
    [ApiController]
    public class EmployeeController : BaseEntityController<Employee>
    {
        #region Field
        /// <summary>
        /// Đói tượng thông báo lỗi trả về
        /// </summary>
        IEmployeeService _employeeService;
        ServiceResult _serviceResult = new ServiceResult();
        #endregion
        #region constructor
        /// <summary>
        /// Hàm khởi tạo
        /// Created By: NTTan (19/8/2021)
        /// </summary>
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Hàm xử lý Filter
        /// </summary>
        /// <param name="employeeFilter">String Filter</param>
        /// <param name="pageIndex">Page hiện tại</param>
        /// <param name="pageSize">số bản ghi trong 1 page</param>
        /// <returns>StatusCode</returns>
        /// Created By: NNTan(17/8/2021)
        [HttpGet("Filter")]
        public IActionResult GetEmployeeFilterPaging(string employeeFilter, int pageIndex, int pageSize)
        {
            var result = _employeeService.GetEmployeeFilterPaging(employeeFilter, pageIndex, pageSize);
            if (result != null)
                return Ok(result);
            else
                return NoContent();
        }
        /// <summary>
        /// hàm tạo mã nhân viên mới
        /// </summary>
        /// <returns>StatusCode</returns>
        /// Created By: NNTan(17/8/2021)
        
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            var result = _employeeService.GetNewEmployeeCode();
            if (result != null)
                return Ok(result);
            return NoContent();
        }
        /// <summary>
        /// Hàm check xem mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên đã tồn tại hay chưa</param>
        /// <returns>đúng hoặc sai</returns>
        /// Created By: NNTan(17/8/2021)
        [HttpGet("CheckCodeExist/{employeeCode}")] 
        public bool GetByCode(string employeeCode)
        {
            var entity = _employeeService.GetByCode(employeeCode);
            //trả về kết quả
            if (entity != null)
                return false;
            else
                return true;
        }
        #endregion
    }
}
