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
        object GetEmployeeFilterPaging(string employeeFilter, int pageIndex, int pageSize);
        public string GetNewEmployeeCode();
        Employee GetByCode(string employeeCode);
    }
}
