<template>
  <div class="content">
    <div class="header-content">
      <div class="title">Nhân viên</div>
      <button class="m-btn" v-on:click="btnAddOnClick">
        <div class="btn-text">Thêm Mới Nhân Viên</div>
      </button>
    </div>
    <div class="content-menu">
      <div class="filter-bar">
        <div class="space"></div>
        <div class="mi-input">
          <input
            type="text"
            placeholder="Tìm theo mã, tên nhân viên"
            value=""
            @input="updateEmployeeFilter($event.target.value)"
          />
          <div class="mi-icon-16 icon-search" @click="searchEmployee"></div>
        </div>
        <div class="mi-icon-24 icon-refresh" @click="refreshData"></div>
      </div>
      <BaseGrid
        @showFormDetail="showFormDetail"
        @showContextMenu="showContextMenu"
        @showPopupDelete="showPopupDelete"
        @duplicateEmployee="duplicateEmployee"
      />
      <BasePagination @getEmployees="getEmployees"/>
      <EmployeeDetail
        v-if="isShowFormDetail"
        v-bind:department="department"
        v-bind:EmployeeDetail="employeeDetail"
        @showPopupCloseForm="showPopupCloseForm"
        @saveOnClick="saveOnClick"
        @closeForm="closeForm"
        @showPopupError="showPopupError"
        @showPopupErrorExist="showPopupErrorExist"
        @saveAndShowForm="saveAndShowForm"
        ref="formdetail"
      />
      <BasePopup
        v-if="isShowPopup"
        v-bind:dataPopup="dataPopup"
        @confirmPopup="confirmPopup"
        @closePopup="closePopup"
        @closePopupAndForm="closePopupAndForm"
      />
      <div id="toast">
        <BaseToast
          v-for="toast in this.$store.state.Toast"
          :key="toast.id"
          v-bind:toast="toast"
        />
      </div>
    </div>
    <BaseLoading />
  </div>
</template>
<script>
import BaseGrid from "../../../components/base/BaseGrid.vue";
import BasePagination from "../../../components/base/BasePagination.vue";
import EmployeeDetail from "./EmployeeDetail.vue";
import BasePopup from "../../../components/base/BasePopup.vue";
import BaseToast from "../../../components/base/BaseToast.vue";
import BaseLoading from "../../../components/base/BaseLoading.vue";
import { MESSAGE } from "../../../resource/index";
import { toast } from "../../../mixins/mixin.js";
// import { api } from "../../../mixins/api.js";
import employeeApi from "../../../api/components/EmployeeAPI";
import departmentApi from "../../../api/components/DepartmentAPI";
export default {
  name: "EmployeePage",
  mixins: [toast],
  components: {
    BaseGrid,
    BasePagination,
    EmployeeDetail,
    BasePopup,
    BaseToast,
    BaseLoading,
  },
  data() {
    return {
      isShowFormDetail: false,
      emloyeeDefault: {
        EmployeeCode: "",
        EmployeeName: "",
        DateOfBirth: "",
        Gender: 0,
        DepartmentId: "",
        DepartmentName: "",
        IdentityNumber: "",
        IdentityDate: "",
        IdentityPlace: "",
        EmployeePosition: "",
        Address: "",
        BankAccountNumber: "",
        BankName: "",
        BankBranchName: "",
        BankProvinceName: "",
        PhoneNumber: "",
        TelephoneNumber: "",
        Email: "",
        EntityState: 0,
      },
      emloyeeDetail: {},
      department: [],
      dataPopup: {
        classIcon: String,
        title: String,
        buttonConfirm: String,
      },
      isShowPopup: false,
      employeeFilter: "",
    };
  },
  async created() {
    await this.getEmployees();
    await this.getDepartment();
    this.department = this.$store.state.department;
  },
  methods: {
    /**
     * Hàm sinh mã mới + 1
     * Created By: NTTan(20/8/2021)
     */
    createdNewCode() {
      var codeMax = 0;
      this.$store.state.employeeList.forEach(function (item) {
        if (Number(item.EmployeeCode.slice(2)) > codeMax) {
          codeMax = Number(item.EmployeeCode.slice(2));
        }
      });
      codeMax++;
      return MESSAGE.PREFIX_EMPLOYEECODE + codeMax;
    },
    /**
     * Hàm để show Form Thông tin nhân viên trống
     * Created By: NTTan (17/8/2021)
     */
    async btnAddOnClick() {
      this.isShowFormDetail = true;
      this.employeeDetail = this.emloyeeDefault;
      this.employeeDetail.EmployeeCode = this.createdNewCode();
    },
    /**
     * Hàm để hiện cái context Menu
     * Created By: NTTan (17/8/2021)
     */
    showContextMenu(employee) {
      this.employeeDetail = { ...employee };
    },
    /**
     * Hàm xử lí confirm Delete
     * Created By: NTTan (19/8/2021)
     */
    confirmPopup() {
      this.isShowPopup = false;
      if (this.$store.state.statusPopup == MESSAGE.STATUS_POPUP_DELETE) {
        this.deleteEmployee(this.employeeDetail);
        this.employeeDetail = {};
      } else if (this.$store.state.statusPopup == MESSAGE.STATUS_POPUP_CLOSE) {
        this.$refs.formdetail.btnSaveOnClick();
      } else if (
        this.$store.state.statusPopup == MESSAGE.STATUS_POPUP_ERROR ||
        this.$store.state.statusPopup == MESSAGE.STATUS_POPUP_EXIST
      ) {
        this.$store.state.componentError[0].$refs.refinput.focus();
        return;
      }
    },
    /**
    Hàm xử lí sự kiện đóng popup
    Created By: NTTan (19/8/2021)
     */
    closePopup() {
      this.isShowPopup = false;
    },
    /**
     * Hàm xử lí nút Hủy trong form chi tiết nhân viên
     * Created By: NTTan (18/8/2021)
     */
    closeForm() {
      this.isShowFormDetail = false;
    },
    /**
     * Hàm xử lí sự kiện với nút X trong form
     * Created By: NTTan  (17/8/2021)
     */
    showPopupCloseForm() {
      this.$store.state.statusPopup = MESSAGE.STATUS_POPUP_CLOSE;
      this.dataPopup.classIcon = MESSAGE.ICON_POPUP_QUESTION;
      this.dataPopup.title = MESSAGE.TITLE_CLOSEFORM;
      this.dataPopup.buttonConfirm = MESSAGE.CONFIRM_CLOSE_DELETE;
      this.isShowPopup = true;
    },
    /**
     * Hiện popup delete
     * Created By: NTTan (19/8/2021)
     */
    showPopupDelete(employee) {
      this.$store.state.statusPopup = MESSAGE.STATUS_POPUP_DELETE;
      this.dataPopup.classIcon = MESSAGE.ICON_POPUP_WARNING;
      this.dataPopup.title =
        MESSAGE.TITLE_PRE_DELETE +
        `${employee.EmployeeCode}` +
        MESSAGE.TITLE_LAST_DELETE;
      this.dataPopup.buttonConfirm = MESSAGE.CONFIRM_CLOSE_DELETE;
      this.isShowPopup = true;
    },
    /**
     * Hàm xử lí nhân bản
     * Created By: NTTan (20/8/2021)
     */
    async duplicateEmployee(employee) {
      this.employeeDetail = { ...employee };
      this.employeeDetail.EmployeeId = "";
      this.employeeDetail.EmployeeCode = this.createdNewCode();
      this.isShowFormDetail = true;
    },
    /**
     * Hiển thị form Detail với sự kiện chọn DBlCLICK
     * Created By: NTTan (17/8/2021)
     */
    showFormDetail(employee) {
      this.employeeDetail = { ...employee };
      this.isShowFormDetail = true;
    },
    /**
     * Hàm xử lí sự kiện khi nhấn nút Cất trong form
     * Created By: NTT(17/8/2021)
     */
    saveOnClick(employee) {
      if (employee.EmployeeId == "" || employee.EmployeeId == null) {
        delete employee.EmployeeId;
        this.postEmployee(employee);
      } else {
        this.putEmployee(employee);
      }
      this.isShowFormDetail = false;
    },
    /**
     * Post and Put và reset form
     * Created By: NTTan(17/8/2021)
     */
    async saveAndShowForm(employee) {
      if (employee.EmployeeId == "" || employee.EmployeeId == null) {
        delete employee.EmployeeId;
        await this.postEmployee(employee);
      } else {
        await this.putEmployee(employee);
      }
    },
    /**
     *  Hàm xử lý refresh Data Api
     * Created By: NTTan (19/8/2021)
     */
    refreshData() {
      this.getEmployees();
    },
    /**
     * Xử lý sự kiện close Form và Popup
     * Created By: NTTan(18/8/2021)
     */
    closePopupAndForm() {
      this.isShowPopup = false;
      this.isShowFormDetail = false;
    },
    /**
     * Xử lý show Popup error do validate form
     * Created By: NTTan(18/8/2021)
     */
    showPopupError(errorMessage) {
      this.$store.state.statusPopup = MESSAGE.STATUS_POPUP_ERROR;
      this.dataPopup.classIcon = MESSAGE.ICON_POPUP_ERROR;
      this.dataPopup.title = errorMessage;
      this.dataPopup.buttonConfirm = MESSAGE.CONFIRM_ERROR;
      this.isShowPopup = true;
    },
    /**
     * Xử lý show Popup error do validate form
     * Created By: NTTan(18/8/2021)
     */
    showPopupErrorExist(employee) {
      this.$store.state.statusPopup = MESSAGE.STATUS_POPUP_EXIST;
      this.dataPopup.classIcon = MESSAGE.ICON_POPUP_WARNING;
      this.dataPopup.title =
        MESSAGE.TITLE_PRE_EXIST +
        `${employee.EmployeeCode}` +
        MESSAGE.TITLE_LAST_EXIST;
      this.dataPopup.buttonConfirm = MESSAGE.CONFIRM_EXIST;
      this.isShowPopup = true;
    },
    /**
     * update String để filter
     * Created By: NTTan(18/8/2021)
     */
    updateEmployeeFilter(value) {
      this.employeeFilter = value;
    },
    /**
     * Xử lý sự kiện khi search
     * Created By: NTTan(18/8/2021)
     */
    searchEmployee() {
      this.$store.state.Pagination.employeeFilter = this.employeeFilter;
      this.getEmployees();
    },
    /**
     * Hàm lấy dữ liệu của table
     * Created By:  NTTan (17/8/2021)
     */
    getDepartment() {
      departmentApi
        .getAllEntities()
        .then((response) => {
          response.data.forEach((element) => {
            this.department.push({
              id: element.DepartmentId,
              name: element.DepartmentName,
            });
          });
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    /**
     * Hàm lấy dữ liệu của table
     * Created By:  NTTan (17/8/2021)
     */
    getEmployees() {
      this.$store.state.isShowLoading = true;
      this.$store.state.employeeList = [];
      let pageSize = this.$store.state.Pagination.pageSize;
      let pageindex = this.$store.state.Pagination.currentPage;
      let employeeFilter = this.$store.state.Pagination.employeeFilter;
      employeeApi
        .getEmployeesFilterPaging(pageSize, pageindex, employeeFilter)
        .then((response) => {
          this.$store.state.employeeList = response.data.Data;
          this.$store.state.Pagination.totalPage = response.data.TotalPage;
          this.$store.state.Pagination.totalRecord = response.data.TotalRecord;
        })
        .catch(function (error) {
          console.log(error);
        });
      this.$store.state.isShowLoading = false;
    },
    /**
     * Hàm thêm mới dữ liệu của table
     * Created By:  NTTan (17/8/2021)
     */
    postEmployee(employee) {
      this.$store.state.employeeList = [];
      employeeApi
        .postEntity(employee)
        .then(() => {
          this.getEmployees();
          this.addToast(MESSAGE.SUCCESS_TOAST, MESSAGE.INSERT_SUCCESS);
        })
        .catch(function (error) {
          this.addToast(MESSAGE.ERROR_TOAST, MESSAGE.INSERT_FAIL);
          console.log(error);
        });
    },
    /**
     * Hàm update dữ liệu của table
     * Created By:  NTTan (17/8/2021)
     */
    putEmployee(employee) {
      this.$store.state.employeeList = [];
      employeeApi
        .putEntity(employee.EmployeeId, employee)
        .then(() => {
          this.getEmployees();
          this.addToast(MESSAGE.SUCCESS_TOAST, MESSAGE.UPDATE_SUCCESS);
        })
        .catch(function (error) {
          this.addToast(MESSAGE.ERROR_TOAST, MESSAGE.UPDATE_FAIL);
          console.log(error);
        });
    },
    /**
     * Hàm xóa dữ liệu của table
     * Created By:  NTTan (17/8/2021)
     */
    deleteEmployee(employee) {
      this.$store.state.employeeList = [];
      employeeApi
        .deleteEntity(employee.EmployeeId)
        .then(() => {
          this.getEmployees();
          this.addToast(MESSAGE.SUCCESS_TOAST, MESSAGE.DELETE_SUCCESS);
        })
        .catch(function (error) {
          this.addToast(MESSAGE.ERROR_TOAST, MESSAGE.DELETE_FAIL);
          console.log(error);
        });
    },
  },
};
</script>
<style scoped>
@import "../../../css/common/content.css";
</style>