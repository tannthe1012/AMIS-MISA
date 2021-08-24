using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// Xử lí Filter and pagination
        /// </summary>
        /// <param name="employeeFilter">Số điện thoại, mã nhân viên , họ và tên</param>
        /// <param name="pageIndex">Page số bao nhiêu</param>
        /// <param name="pageSize">Bao nhiêu bản ghi trong một trang</param>
        /// <returns>List các bản ghi</returns>
        /// Created By: NTTan(20/8/2021)
        object GetEmployeeFilterPaging(string employeeFilter, int pageIndex, int pageSize);
        /// <summary>
        /// Hàm tự render Employeecode mới
        /// </summary>
        /// <returns>Trả về EmployeeCode chưa có trong DB</returns>
        /// Created By: NTTan (20/8/2021)
        public string GetNewEmployeeCode();
        /// <summary>
        /// Kiểm tra xem EmployeeCode đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode cần check</param>
        /// <returns>Employee = 1 thì đã tồn tại và bằng 0 là ngược lại</returns>
        /// Created By: NTTAN(20/8/2021)
        Employee GetByCode(string employeeCode);
    }
}
