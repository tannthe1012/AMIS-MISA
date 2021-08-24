<template>
  <div id="modal" class="modal">
    <div class="form-employee">
      <div class="ms-form-header">
        <div class="ms-header-title">
          <div class="header-form">Thông tin nhân viên</div>
          <label class="form-label-checkbox" for="">
            <BaseCheckBox />
            <span class="slot-label">Là khách hàng</span>
          </label>
          <label class="form-label-checkbox" for="">
            <BaseCheckBox />
            <span class="slot-label">Là nhà cung cấp</span>
          </label>
        </div>
        <div class="ms-form-close">
          <div class="mi-icon-24 icon-help"></div>
          <div class="mi-icon-24 icon-close" @click="btnCloseForm"></div>
        </div>
      </div>
      <div class="ms-form-content">
        <div class="form-content">
          <div class="flex pd-b-12">
            <div class="w-1/2">
              <div class="p-r-26">
                <div class="form-row">
                  <div class="w-2/5">
                    <div class="p-r-6">
                      <BaseInput
                        v-model="currentEmployee.EmployeeCode"
                        ref="input1"
                        label="Mã"
                        :required="true"
                        tabindex="1"
                      />
                    </div>
                  </div>
                  <div class="w-3/5">
                    <BaseInput
                      v-model="currentEmployee.EmployeeName"
                      ref="input2"
                      label="Tên"
                      :required="true"
                      tabindex="2"
                    />
                  </div>
                </div>
                <div class="form-row" style="flex-direction: column">
                  <!-- <BaseInput label="Đơn vị" :required="true" /> -->
                  <label class="flex" for="name">
                    <div class="ms-title-input">Đơn vị</div>
                    <div class="ms-input-title-require">&nbsp;*</div>
                  </label>
                  <BaseComboBox
                    label="Đơn vị"
                    :required="true"
                    ref="input3"
                    v-bind:item="department"
                    v-bind:valueInput="currentEmployee.DepartmentName"
                    @result="(result) => fotmatDataCbb(result, 'department')"
                    tabindex="3"
                  />
                </div>
                <div class="form-row">
                  <BaseInput
                    v-model="currentEmployee.EmployeePosition"
                    ref="input4"
                    label="Chức danh"
                    tabindex="4"
                  />
                </div>
              </div>
            </div>
            <div class="w-1/2">
              <div class="form-row">
                <div class="w-2/5">
                  <div class="p-r-6">
                    <!-- <BaseInput
                      v-model="currentEmployee.DateOfBirth"
                      label="Ngày sinh"
                      ref="input5"
                      type="date"
                      tabindex="5"
                    /> -->
                    <BaseDatePicker
                      v-model="currentEmployee.DateOfBirth"
                      label="Ngày sinh"
                      type="date"
                      tabindex="5"
                    />
                  </div>
                </div>
                <div class="w-3/5">
                  <BaseRadio
                    v-model="currentEmployee.Gender"
                    :items="gender"
                    @resultRadio="formatDataGender"
                    label="Giới tính"
                    tabindex="6"
                  />
                </div>
              </div>
              <div class="form-row" style="margin-top: 12px">
                <div class="w-3/5">
                  <div class="p-r-6">
                    <BaseInput
                      v-model="currentEmployee.IdentityNumber"
                      ref="input7"
                      label="Số CMND"
                      tabindex="7"
                    />
                  </div>
                </div>
                <div class="w-2/5">
                  <!-- <BaseInput
                    v-model="currentEmployee.IdentityDate"
                    ref="input8"
                    label="Ngày cấp"
                    type="date"
                    tabindex="8"
                  /> -->
                  <BaseDatePicker
                    v-model="currentEmployee.IdentityDate"
                    label="Ngày cấp"
                    type="date"
                    tabindex="8"
                  />
                </div>
              </div>
              <div class="form-row">
                <BaseInput
                  v-model="currentEmployee.IdentityPlace"
                  label="Nơi cấp"
                  ref="input9"
                  tabindex="9"
                />
              </div>
            </div>
          </div>
          <div class="form-content-bottom">
            <div class="form-row">
              <BaseInput
                v-model="currentEmployee.Address"
                ref="input10"
                label="Địa chỉ"
                tabindex="10"
              />
            </div>
            <div class="form-row" style="justify-content: end">
              <div class="w-1/4">
                <div class="p-r-6">
                  <BaseInput
                    v-model="currentEmployee.PhoneNumber"
                    label="ĐT di động"
                    ref="input11"
                    tabindex="11"
                  />
                </div>
              </div>
              <div class="w-1/4">
                <div class="p-r-6">
                  <BaseInput
                    v-model="currentEmployee.TelephoneNumber"
                    label="ĐT cố định"
                    ref="input12"
                    tabindex="12"
                  />
                </div>
              </div>
              <div class="w-1/4">
                <div class="p-r-6">
                  <BaseInput
                    v-model="currentEmployee.Email"
                    label="Email"
                    type="email"
                    ref="input13"
                    tabindex="13"
                  />
                </div>
              </div>
            </div>
            <div class="form-row" style="justify-content: end">
              <div class="w-1/4">
                <div class="p-r-6">
                  <BaseInput
                    v-model="currentEmployee.BankAccountNumber"
                    label="Tài khoản ngân hàng"
                    ref="input14"
                    tabindex="14"
                  />
                </div>
              </div>
              <div class="w-1/4">
                <div class="p-r-6">
                  <BaseInput
                    v-model="currentEmployee.BankName"
                    label="Tên ngân hàng"
                    ref="input15"
                    tabindex="15"
                  />
                </div>
              </div>
              <div class="w-1/4">
                <div class="p-r-6">
                  <BaseInput
                    v-model="currentEmployee.BankBranchName"
                    label="Chi nhánh"
                    ref="input16"
                    tabindex="16"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="footer-container">
          <div class="divide"></div>
          <div class="form-footer">
            <div class="form-footer-left">
              <button class="m-btn-default" tabindex="17" @click="closeForm">
                Hủy
              </button>
            </div>
            <div class="form-footer-right">
              <button
                class="m-btn-default"
                tabindex="18"
                @click="btnSaveOnClick"
              >
                Cất
              </button>
              <button class="m-btn" tabindex="19" @click="btnSaveAndShowForm">
                Cất và Thêm
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import moment from "moment";
import BaseCheckBox from "../../../components/base/BaseCheckBox.vue";
import BaseInput from "../../../components/base/BaseInput.vue";
import BaseRadio from "../../../components/base/BaseRadio.vue";
import BaseComboBox from "../../../components/base/BaseComboBox.vue";
import BaseDatePicker from "../../../components/base/BaseDatePicker.vue";
import { MESSAGE } from "../../../resource/index";
import employeeApi from "../../../api/components/EmployeeAPI";
export default {
  name: "EmployeeDetail",
  components: {
    BaseCheckBox,
    BaseInput,
    BaseRadio,
    BaseComboBox,
    BaseDatePicker,
  },
  props: {
    EmployeeDetail: { type: Object },
    department: { type: Array },
  },
  data() {
    return {
      employeeDetail: { ...this.EmployeeDetail },
      gender: [
        { id: 0, name: "Nữ" },
        { id: 1, name: "Nam" },
        { id: 2, name: "Khác" },
      ],
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
    };
  },
  computed: {
    /**
     * Employee bind input and emit
     */
    currentEmployee: function () {
      return {
        ...this.employeeDetail,
        DateOfBirth: this.formatDateToInput(this.employeeDetail?.DateOfBirth),
        IdentityDate: this.formatDateToInput(this.employeeDetail?.IdentityDate),
      };
    },
    /**
     * Employee bất đầu để so sánh xem data đã thay đổi chưa
     */
    employeeStart: function () {
      return {
        ...this.employeeDetail,
        DateOfBirth: this.formatDateToInput(this.employeeDetail?.DateOfBirth),
        IdentityDate: this.formatDateToInput(this.employeeDetail?.IdentityDate),
      };
    },
  },
  mounted() {
    this.focusInputFrist();
  },
  methods: {
    /**
     * Hàm focus vài input đầu tiên
     * Created By: NTTan(20/8/2021)
     */
    focusInputFrist() {
      this.$refs.input1.$refs.refinput.focus();
      return;
    },
    /**
     * Xử lý đầu ra của combobox
     * param: value combobox Đơn vị
     * Created By: NTTan (20/8/2021)
     */
    fotmatDataCbb(result) {
      this.currentEmployee.DepartmentName = result;
      this.currentEmployee.DepartmentId = this.findId(result, this.department);
    },
    /**
     * Tìm id của combobox để gán vào obj để POST hoặc PUT
     * param(result): value Name trong các array
     * param(array): array obj select
     * Created By: NTTan (20/8/2021)
     */
    findId(result, array) {
      const obj = array.find((item) => item.name == result);
      return obj.id;
    },
    /**
     * Hàm format date vào các tr tương ứng
     * param: format date để bind vao input
     * Created By: NTTan (17/8/2021)
     */
    formatDateToInput(value) {
      if (value) {
        return moment(String(value)).format("YYYY-MM-DD");
      }
    },
    /***
     * Hàm format từ Gendername sang Gender
     * param: là Gendername
     * Created By: NTTan(20/8/2021)
     */
    formatDataGender(valueSelect) {
      this.currentEmployee.Gender = valueSelect;
    },
    /**
     * Hàm kiểm tra xem obj có rỗng không
     * param: obj cần xét
     * Created By: NTTan (18/8/2021)
     */
    isEmptyObject(obj) {
      return JSON.stringify(obj) === "{}";
    },
    /**
     * Hàm xử lí đóng form Gửi lên EmployeeDetail
     * Created By: NTTan(17/8/2021)
     */
    btnCloseForm() {
      var isShowPopup =
        Object.entries(this.employeeStart).toString() ===
        Object.entries(this.currentEmployee).toString();
      if (isShowPopup == true) {
        this.$emit("closeForm");
      } else {
        this.$emit("showPopupCloseForm");
      }
    },
    /**
     * Hàm thêm nhân viên
     * Created By: NTTan(17/8/2021)
     */
    async btnSaveOnClick() {
      this.$store.state.componentError = [];
      Object.entries(this.$refs).forEach((baseinput) => {
        baseinput[1].$refs.refinput.focus();
        baseinput[1].$refs.refinput.blur();
        if (baseinput[1].error) {
          this.$store.state.componentError.push(baseinput[1]);
        }
      });
      if (
        !this.employeeStart.EmployeeId &&
        this.currentEmployee.EmployeeCode != null &&
        this.currentEmployee.EmployeeCode != ""
      ) {
        var isExist = await this.checkEmployeeCodeExist(
          this.currentEmployee.EmployeeCode
        );
        if (isExist == false) {
          this.$store.state.componentError = [];
          this.$store.state.componentError.push(
            Object.entries(this.$refs)[0][1]
          );
          Object.entries(this.$refs)[0][1].$refs.refinput.title =
            MESSAGE.VALIDATE_EXIST;
          this.$emit("showPopupErrorExist", this.currentEmployee);
          return;
        }
      }
      if (this.$store.state.componentError.length == 0) {
        this.$emit("saveOnClick", this.currentEmployee);
      } else {
        var errorContext =
          this.$store.state.componentError[0].$refs.refinput.title;
        this.$emit("showPopupError", errorContext);
      }
    },
    /**
     * Hàm thêm nhân viên và reset form
     * Created By: NTTan(17/8/2021)
     */
    async btnSaveAndShowForm() {
      this.$store.state.componentError = [];

      Object.entries(this.$refs).forEach((baseinput) => {
        baseinput[1].$refs.refinput.focus();
        baseinput[1].$refs.refinput.blur();
        if (baseinput[1].error) {
          this.$store.state.componentError.push(baseinput[1]);
        }
      });
      if (
        !this.employeeStart.EmployeeId &&
        this.currentEmployee.EmployeeCode != null &&
        this.currentEmployee.EmployeeCode != ""
      ) {
        var isExist = await this.checkEmployeeCodeExist(
          this.currentEmployee.EmployeeCode
        );
        if (isExist == false) {
          this.$store.state.componentError = [];
          this.$store.state.componentError.push(
            Object.entries(this.$refs)[0][1]
          );
          Object.entries(this.$refs)[0][1].$refs.refinput.title =
            MESSAGE.VALIDATE_EXIST;
          this.$emit("showPopupErrorExist", this.currentEmployee);
          return;
        }
      }
      if (this.$store.state.componentError.length == 0) {
        this.$emit("saveAndShowForm", this.currentEmployee);
        // reset form
        this.currentEmployee.Gender = 0;
        this.currentEmployee.DateOfBirth = null;
        let newCode = this.createdNewCode(this.currentEmployee.EmployeeCode);
        this.employeeDetail = this.emloyeeDefault;
        this.employeeDetail.EmployeeCode = newCode;

        Object.entries(this.$refs).forEach((baseinput) => {
          baseinput[1].$refs.refinput.value = "";
        });

        this.focusInputFrist();
      } else {
        var errorContext =
          this.$store.state.componentError[0].$refs.refinput.title;
        this.$emit("showPopupError", errorContext);
      }
    },
    /**
     * Hàm sinh mã mới + 1
     * Created By: NTTan(20/8/2021)
     */
    createdNewCode(oldCode) {
      return MESSAGE.PREFIX_EMPLOYEECODE + (Number(oldCode.slice(2)) + 1);
    },
    /**
     * Hàm xử lí nút Hủy trong form chi tiết nhân viên
     * Created By: NTTan (18/8/2021)
     */
    closeForm() {
      this.$emit("closeForm");
    },
    /**
     * Hàm lấy dữ liệu của table
     * Created By:  NTTan (17/8/2021)
     */
    async checkEmployeeCodeExist(employeeCode) {
      let isExist;
      try {
        let res = await employeeApi.checkEmployeeCodeExist(employeeCode);
        isExist = res.data;
      } catch (e) {
        console.log(e);
      }
      return isExist;
    },
  },
};
</script>

<style scoped>
@import "../../../css/common/form.css";
.slot-label {
  white-space: nowrap;
  padding-left: 10px;
  font-size: 13px;
}
.ms-input-title-require {
  color: red;
}
.ms-title-input {
  font-size: 12px;
  white-space: nowrap;
  font-family: notosans-bold;
  cursor: grab;
}
.ms-form-content label {
  max-height: 16px;
  box-sizing: border-box;
  margin-bottom: 4px;
}
</style>