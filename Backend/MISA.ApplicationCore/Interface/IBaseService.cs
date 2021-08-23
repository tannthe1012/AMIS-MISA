using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface
{
    public interface IBaseService<MISAEntity>
    {
        // <summary>
        /// Hàm lấy tất cả
        /// </summary>
        /// <returns>Dãy Entity </returns>
        /// Created By: NTTan(18/8/2021)
        Task<IEnumerable<MISAEntity>> GetAll();
        /// <summary>
        /// hàm xóa theo id của đối tượng
        /// </summary>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Số cột bị xóa</returns>
        /// Created By: NTTan (18/8/2021)
        Task<MISAEntity> GetById(Guid id);
        /// <summary>
        /// hàm xóa theo id của đối tượng
        /// </summary>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Số cột bị xóa</returns>
        /// Created By: NTTan (19/8/2021)
        Task<int> Delete(Guid id);
        /// <summary>
        /// Hàm thêm mới dối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Số bản ghi thay đổi trong DB</returns>
        /// Created By: NTTan (18/8/2021)
        Task<ServiceResult> Insert(MISAEntity entity);
        /// <summary>
        /// Sửa đổi dữ liệu của đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa đổi</param>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// Created By: NTTan (18/8/2021)
        Task<ServiceResult> Update(MISAEntity entity);
    }
}
