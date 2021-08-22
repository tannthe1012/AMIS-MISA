using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using MySqlConnector;

namespace MISA.Infarstructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Xử lí Filter and pagination
        /// </summary>
        /// <param name="employeeFilter">Số điện thoại, mã nhân viên , họ và tên</param>
        /// <param name="pageIndex">Page số bao nhiêu</param>
        /// <param name="pageSize">Bao nhiêu bản ghi trong một trang</param>
        /// <returns>List các bản ghi</returns>
        /// Created By: NTTan(20/8/2021)
        public object GetEmployeeFilterPaging(string employeeFilter, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();
            var totalRecord = 0;
            var totalPage = 0;
            parameters.Add("@EmployeeFilter", employeeFilter);
            parameters.Add("@PageIndex", pageIndex);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@TotalPage", totalPage, DbType.Int32, ParameterDirection.Output);
            parameters.Add("@TotalRecord", totalRecord, DbType.Int32, ParameterDirection.Output);
            var employees = _dbConnection.Query<Employee>("Proc_GetEmployeesFilterPaging", parameters, commandType: CommandType.StoredProcedure).ToList();
            var result = new
            {
                TotalPage = parameters.Get<int>("@TotalPage"),
                TotalRecord = parameters.Get<int>("@TotalRecord"),
                Data = employees
            };
            return result;
        }
        /// <summary>
        /// Hàm tự render Employeecode mới
        /// </summary>
        /// <returns>Trả về EmployeeCode chưa có trong DB</returns>
        /// Created By: NTTan (20/8/2021)
        public string GetNewEmployeeCode()
        {
            var result = _dbConnection.QueryFirstOrDefault<string>("Proc_GetNewEmployeeCode", commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Kiểm tra xem EmployeeCode đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode cần check</param>
        /// <returns>Employee = 1 thì đã tồn tại và bằng 0 là ngược lại</returns>
        /// Created By: NTTAN(20/8/2021)
        public Employee GetByCode(string employeeCode)
        {
            var query = $"SELECT * FROM Employee WHERE EmployeeCode = '{employeeCode}'";
            var result = _dbConnection.QueryFirstOrDefault<Employee>(query);
            return result;
        }
    }
}
