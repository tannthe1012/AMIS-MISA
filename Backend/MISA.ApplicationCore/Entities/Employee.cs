
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// Created By: NTTan (21/7/2021)
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Id nhân viên (khóa chính)
        /// </summary>                          
        [PrimaryKey]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [CheckDuplicate]
        [Required]
        [DisplayName("Mã nhân viên")]
        [MaxLength(20)]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ và tên nhân viên
        /// </summary>
        [Required]
        public string EmployeeName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Mã phòng ban của nhân viên
        /// </summary>
        [Required]
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string EmployeePosition { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string BankAccountNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string BankBranchName { get; set; }
        /// <summary>
        /// Tên tỉnh ngân hàng
        /// </summary>
        public string BankProvinceName { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        //[CheckDuplicate]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        //[CheckDuplicate]
        [DisplayName("Số điện thoại cố định")]
        public string TelephoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        #endregion


    }
}
