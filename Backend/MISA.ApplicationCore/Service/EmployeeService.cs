
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Service;
using MISA.Entity;


namespace MISA.ApplicationCore
{
    /// <summary>
    /// class xử lý nghiệp vụ của Employee
    /// </summary>
    public class EmployeeService: BaseService<Employee>,IEmployeeService
    {
        #region Field
        IEmployeeRepository _employeeRepository;
        #endregion
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Hàm xử lý API paging và filter
        /// </summary>
        /// <param name="employeeFilter">String để filter(SĐT, Mã nhân viên, Tên nhân viên)</param>
        /// <param name="pageIndex">Trang đang được xem</param>
        /// <param name="pageSize">Số bản ghi trong một trang</param>
        /// <returns>Obj có data là list Employee</returns>
        /// Created By: NTTAN(20/8/2021)
        public object GetEmployeeFilterPaging(string employeeFilter, int pageIndex, int pageSize)
        {
            return _employeeRepository.GetEmployeeFilterPaging(employeeFilter, pageIndex, pageSize);
        }
        /// <summary>
        /// Hàm sinh mã nhân viên mới
        /// </summary>
        /// <returns>Trả về mã nhân viên chưa có trong database</returns>
        /// Created By: NTTAN(20/8/2021)
        public string GetNewEmployeeCode()
        {
            return _employeeRepository.GetNewEmployeeCode();
        }
        /// <summary>
        /// Kiểm tra xem EmployeeCode đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode cần check</param>
        /// <returns>Employee = 1 thì đã tồn tại và bằng 0 là ngược lại</returns>
        /// Created By: NTTAN(20/8/2021)
        public Employee GetByCode(string employeeCode)
        {
            return _employeeRepository.GetByCode(employeeCode);
        }
        #endregion
    }
}
