using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using MISA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Service
{
    /// <summary>
    /// Class xử lý nghiệp vụ của Department
    /// </summary>
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Field
       
        IDepartmentRepository _dempartmentRepository;
        #endregion
        #region Constructor
        public DepartmentService(IDepartmentRepository dempartmentRepository) : base(dempartmentRepository)
        {
            _dempartmentRepository = dempartmentRepository;
        }
        #endregion
        #region Method
        #endregion
    }
}
