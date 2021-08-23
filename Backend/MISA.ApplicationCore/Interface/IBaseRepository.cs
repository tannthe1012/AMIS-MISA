using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interface
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Hàm lấy tất cả
        /// </summary>
        /// <returns>List obj</returns>
        /// Created By: NTTan(19/8/2021)
        Task<IEnumerable<MISAEntity>> GetAll();
        /// <summary>
        /// Hàm lấy đối tượng theo id
        /// </summary>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Đối tượng với id tương ứng</returns>
        /// Created By: NTTan (19/8/2021)
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
        /// Created By: NTTan (19/8/2021)
        Task<int> Insert(MISAEntity entity);
        /// <summary>
        /// Sửa đổi dữ liệu của đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa đổi</param>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// Created By: NTTan (19/8/2021)
        Task<int> Update(MISAEntity entity);
        /// <summary>
        /// Hàm check trung lặp của 1 property của đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần kiểm tr</param>
        /// <param name="property">property cần check</param>
        /// <returns>Đối tượng lấy theo Property</returns>
        /// Created By: NTTan(19/8/2021)
        Task<MISAEntity> GetEntityByProperty(MISAEntity entity, PropertyInfo property);
    }
}
