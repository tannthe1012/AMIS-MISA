import BaseAPI from "@/api/base/BaseAPI";
import BaseAPIConfig from "@/api/base/BaseAPIConfig";

/**
 * Lớp cung cấp các phương thức thao tác dữ liệu Employee (Nhân viên)
 */
class EmployeeAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "api/v1/Employees";
    this.newEmployeeCodeAPI = "NewEmployeeCode";
    this.employeeFilterAPI = "Filter";
    this.checkEmployeeCode = "CheckCodeExist";
  }

  /**
   * Phương thức lấy mã nhân viên mới
   * @returns {promise} kết quả thực hiện
   * @author pthieu (28-07-2021)
   */
  getNewEmployeeCode() {
    return BaseAPIConfig.get(`${this.controller}/${this.newEmployeeCodeAPI}`);
  }

  /**
   * Phương thức lấy dữ liệu nhân viên theo các tiêu chí lọc/phân trang
   * @param {Number} pageSize số bản ghi/trang
   * @param {Number} pageIndex offset/index bản ghi đầu tiên của trang 
   * @param {string} departmentId id phòng ban
   * @param {string} employeeFilter xâu tìm kiếm 
   * @returns {promise} kết quả thực hiện
   * @author pthieu (28-07-2021)
   */
  getEmployeesFilterPaging(pageSize, pageIndex, employeeFilter = null) {
    var parameters = {
      pageSize,
      pageIndex,
    }
    if(typeof employeeFilter !== "undefined" && employeeFilter !== null && employeeFilter.trim() !== "") {
      parameters.employeeFilter = employeeFilter.trim();
    }
    return BaseAPIConfig.get(`${this.controller}/${this.employeeFilterAPI}`, {params: parameters});
  }
  checkEmployeeCodeExist(employeeCode) {
    return BaseAPIConfig.get(`${this.controller}/${this.checkEmployeeCode}/${employeeCode}`)
  }
}

export default new EmployeeAPI();