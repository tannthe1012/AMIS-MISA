using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infarstructure
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>,IDisposable where MISAEntity : BaseEntity
    {
        #region Field
        // khởi tạo đối tượng kết nối database
        protected IDbConnection _dbConnection;
        // các config kết nối
        string _connectionString;
        string className;
        IConfiguration _configuration;
        #endregion
        #region Constructor
        /// <summary>
        /// Hàm khởi tạo truyền vào 1 chuỗi kết nối
        /// </summary>
        /// <param name="configuration"></param>
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("CukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            className = typeof(MISAEntity).Name;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Hàm lấy tất cả
        /// </summary>
        /// <returns>List obj</returns>
        /// Created By: NTTan(19/8/2021)
        public async Task<IEnumerable<MISAEntity>> GetAll()
        {
            _dbConnection.Open();
            var entities = await _dbConnection.QueryAsync<MISAEntity>($"Proc_Get{className}s", commandType: CommandType.StoredProcedure);
            return  entities;
        }
        /// <summary>
        /// Hàm lấy đối tượng theo id
        /// </summary>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Đối tượng với id tương ứng</returns>
        /// Created By: NTTan (19/8/2021)
        public async Task<MISAEntity> GetById(Guid id)
        {
            var className = typeof(MISAEntity).Name;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}Id", id);
            var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @{className}Id";
            var entity = await _dbConnection.QueryFirstOrDefaultAsync<MISAEntity>(sqlCommand, param: parameters);
            return entity;
        }
        /// <summary>
        /// Hàm thêm mới dối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Số bản ghi thay đổi trong DB</returns>
        /// Created By: NTTan (19/8/2021)
        public async Task<int> Insert(MISAEntity entity)
        {
            var rowEntity = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var className = typeof(MISAEntity).Name;
                    var parameters = MappingDbType(entity);
                    rowEntity = await _dbConnection.ExecuteAsync($"Proc_Insert{className}", parameters,transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();                   
                }
            }
            return rowEntity;
        }
        /// <summary>
        /// Sửa đổi dữ liệu của đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa đổi</param>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// Created By: NTTan (19/8/2021)
        public async Task<int> Update(MISAEntity entity)
        {
            var rowEntity = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var className = typeof(MISAEntity).Name;
                    var parameters = MappingDbType(entity);
                    rowEntity = await _dbConnection.ExecuteAsync($"Proc_Update{className}", parameters, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return rowEntity;
        }
        /// <summary>
        /// hàm xóa theo id của đối tượng
        /// </summary>
        /// <param name="id">id của đối tượng</param>
        /// <returns>Số cột bị xóa</returns>
        /// Created By: NTTan (19/8/2021)
        public async Task<int> Delete(Guid id)
        {
            var rowEmployee = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@{className}Id", id);
                    var sqlCommand = $"DELETE FROM {className} WHERE {className}Id = @{className}Id";
                    rowEmployee = await _dbConnection.ExecuteAsync(sqlCommand, param: parameters, transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return rowEmployee;
        }
        /// <summary>
        /// Hàm để Map các thuộc tính
        /// </summary>
        /// <typeparam name="MISAEntity">Employee hoặc Department</typeparam>
        /// <param name="entity">đối tượng</param>
        /// <returns>parameters các property map</returns>
        /// Created By: NTTan(19/8/2021)
        public DynamicParameters MappingDbType(MISAEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }
        /// <summary>
        /// Hàm check trung lặp của 1 property của đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần kiểm tr</param>
        /// <param name="property">property cần check</param>
        /// <returns>Đối tượng lấy theo Property</returns>
        /// Created By: NTTan(19/8/2021)
        public async Task<MISAEntity> GetEntityByProperty(MISAEntity entity, PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            if (entity.EntityState == Entity.EntityState.AddNew)
            {
                var query = $"SELECT * FROM {className} WHERE {propertyName} = '{propertyValue}'";
                var entityResult = await _dbConnection.QueryFirstOrDefaultAsync<MISAEntity>(query);
                return entityResult;
            }
            else if (entity.EntityState == Entity.EntityState.Update)
            {
                var keyValue = entity.GetType().GetProperty($"{className}Id").GetValue(entity);
                var query = $"SELECT * FROM {className} WHERE {propertyName} = '{propertyValue}' AND {className}id != '{keyValue}'";
                var entityResult = await _dbConnection.QueryFirstOrDefaultAsync<MISAEntity>(query);
                return entityResult;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Hàm xóa đối tượng DbConnection để đỡ tốn tài nguyên
        /// </summary>
        /// Created By: NTTan(19/8/2021)
        public void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
        #endregion
    }
}
