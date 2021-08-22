<template>
  <div class="grid gird-employee">
    <table
      style="width: calc(100vw - 100px; table-layout: fixed"
      cellspacing="0"
    >
      <colgroup>
        <col style="width: 20px" />
        <col style="width: 40px" />
        <col style="width: 120px" />
        <col style="width: 250px" />
        <col style="width: 160px" />
        <col style="width: 180px" />
        <col style="width: 180px" />
        <col style="width: 180px" />
        <col style="width: 150px" />
        <col style="width: 150px" />
        <col style="width: 250px" />
        <col style="width: 180px" />
        <col style="width: 120px" />
        <col style="width: 30px" />
        <col style="width: 20px" />
      </colgroup>
      <thead>
        <tr>
          <th class="ms-th-space-30"></th>
          <th class="ms-th-space-30-l-16">
            <BaseCheckBox :selectAll="true" @selectAll="selectAll" />
          </th>
          <th>MÃ NHÂN VIÊN</th>
          <th>TÊN NHÂN VIÊN</th>
          <th style="text-align: center">NGÀY SINH</th>
          <th>NƠI CẤP</th>
          <th>CHỨC DANH</th>
          <th>SỐ TÀI KHOẢN</th>
          <th>TÊN NGÂN HÀNG</th>
          <th>EMAIL</th>
          <th>ĐỊA CHỈ</th>
          <th>TRẠNG THÁI</th>
          <th class="ms-th-space-29">CHỨC NĂNG</th>
          <th class="ms-th-space-29-r-0"></th>
          <th class="ms-th-space-29-r--20"></th>
        </tr>
      </thead>

      <tbody v-if="this.$store.state.employeeList.length != 0">
        <tr
          v-for="employee in this.$store.state.employeeList"
          :key="employee.EmployeeId"
          @dblclick="showFormDetail(employee)"
        >
          <td class="background-white ms-td-space-25"></td>
          <td class="ms-td-space-28">
            <BaseCheckBox :selectAllCheckBox="selectAllCheckBox" />
          </td>
          <td>{{ employee.EmployeeCode }}</td>
          <td>{{ employee.EmployeeName }}</td>
          <td class="text-align-center">
            {{ formatDate(employee.DateOfBirth) }}
          </td>
          <td class="white-space" :title="employee.IdentityPlace">{{ employee.IdentityPlace }}</td>
          <td>{{ employee.EmployeePosition }}</td>
          <td>{{ employee.BankAccountNumber }}</td>
          <td>{{ employee.BankName }}</td>
          <td :title="employee.Email">{{ employee.Email }}</td>
          <td class="white-space" :title="employee.Address">{{ employee.Address }}</td>
          <td>Đang sử dụng</td>
          <td class="ms-tr-viewer ms-td-space-3">
            <div class="flex justify-end">
              <div class="ms-dropdown">
                <button
                  class="ms-dropdown-text"
                  @click="showFormDetail(employee)"
                >
                  Sửa
                </button>
                <button
                  class="ms-btn-dropdown"
                  @click="showContextMenu($event, employee)"
                >
                  <div
                    class="align-center"
                    :class="{
                      'ms-icon-focus':
                        isShowContextMenu == true &&
                        employee.EmployeeId == employeeSelect.EmployeeId,
                    }"
                  >
                    <div class="mi-icon-16 icon-dropdown"></div>
                  </div>
                </button>
              </div>
            </div>
          </td>
          <td class="background-white ms-td-space-25-r"></td>
          <td class="ms-td-space-25-r-w20"></td>
        </tr>
      </tbody>
    </table>
    <BaseContextMenu
      v-show="isShowContextMenu"
      v-bind:style="{ top: top, left: left }"
      @duplicateEmployee="duplicateEmployee"
      @showPopupDelete="showPopupDelete"
    />
  </div>
</template>
<script>
import moment from "moment";
import BaseCheckBox from "./BaseCheckBox.vue";
import BaseContextMenu from "./BaseContextMenu.vue";
export default {
  name: "BaseGrid",
  components: {
    BaseContextMenu,
    BaseCheckBox,
  },
  data() {
    return {
      employeeSelect: Object,
      selectAllCheckBox: false,
      isShowContextMenu: false,
      top: "0px",
      left: "0px",
    };
  },
  mounted() {
    document.addEventListener("click", this.handleClickOutside);
  },
  destroyed() {
    document.removeEventListener("click", this.handleClickOutside);
  },

  methods: {
    /**
     * xử lí khi click hiện contextmenu
     * param(employee): đối tượng employee để nhân bản hoặc xóa
     * Created By: NTTan (18/8/2021)
     */
    showContextMenu(e, employee) {
      e.preventDefault();
      var topPos = e.target.getBoundingClientRect().top;
      var leftPos = e.target.getBoundingClientRect().left;
      this.isShowContextMenu = true;
      if (employee.EmployeeId == this.employeeSelect.EmployeeId) {
        this.isShowContextMenu = false;
        this.employeeSelect = {};
      }

      this.top = topPos + 20 + "px";
      this.left = leftPos + 30 + "px";
      this.$emit("showContextMenu", employee);
      if (this.isShowContextMenu == true) {
        this.employeeSelect = { ...employee };
      }
    },
     /**
     * Hàm format date vào các tr tương ứng
     * Created By: NTTan (15/7/2021)
     */
    formatDate(value) {
      if (value) {
        return moment(String(value)).format("DD/MM/YYYY");
      }
    },
    /***
     * Hàm xử lý select all của chechbox
     * Created By: NTTan(17/8/2021)
     */
    selectAll() {
      this.selectAllCheckBox = !this.selectAllCheckBox;
    },
    /**
     * xử lí khi chọn vào nút Nhân bản
     * Created By: NTTan (18/8/2021)
     */
    duplicateEmployee() {
      this.isShowContextMenu = false;
      this.$emit("duplicateEmployee", this.employeeSelect);
    },
    /**
     * xử lí khi chọn vào nút xóa
     * Created By: NTTan (18/8/2021)
     */
    showPopupDelete() {
      this.isShowContextMenu = false;
      this.$emit("showPopupDelete", this.employeeSelect);
    },
    /**
     * xử lí khi dbclick vào thẻ <tr>
     * param: Dối tượng employee
     * Created By: NTTan (18/8/2021)
     */
    showFormDetail(employee) {
      this.$emit("showFormDetail", employee);
    },
    /**
     * Hàm xử lý click ra ngoài sẽ đóng context menu
     * Created By: NTTan (20/8/2021)
     */
    handleClickOutside(event) {
      if (event.target.className != "mi-icon-16 icon-dropdown") {
        this.isShowContextMenu = false;
        this.employeeSelect = {};
      }
    },
  },
};
</script>
<style scoped>
@import "../../css/common/grid.css";
.background-white {
  background-color: #fff;
}
.ms-td-space-25 {
  width: 20px;
  height: 34px;
  border: none;
  position: sticky;
  left: 0;
  z-index: 25;
}
.ms-td-space-25-r {
  width: 30px;
  height: 34px;
  border: none;
  position: sticky;
  right: 0;
  z-index: 25;
}
.ms-td-space-25-r-w20 {
  width: 20px;
  height: 34px;
  border: none;
  position: sticky;
  right: -20px;
  z-index: 25;
  background-color: #f4f5f6;
}
.ms-td-space-3 {
  position: sticky;
  right: 30px;
  z-index: 3;
  border-left: 1px solid #c7c7c7;
  border-right: none;
}
.ms-td-space-28 {
  position: sticky;
  left: 16px;
  z-index: 28;
}
.ms-th-space-30 {
  background-color: #fff;
  width: 20px;
  height: 34px;
  border: none;
  position: sticky;
  left: 0;
  z-index: 30;
}
.ms-th-space-30-l-16 {
  position: sticky;
  left: 16px;
  z-index: 30;
}
.ms-th-space-29 {
  position: sticky;
  right: 30px;
  top: 0px;
  z-index: 29;
  border-left: 1px solid #c7c7c7;
  border-right: none;
}
.ms-th-space-29-r-0 {
  background-color: #fff;
  width: 30px;
  height: 34px;
  border: none;
  position: sticky;
  right: 0;
  z-index: 29;
}
.ms-th-space-29-r--20 {
  width: 20px;
  height: 34px;
  background-color: #f4f5f6;
  border: none;
  position: sticky;
  right: -20px;
  z-index: 29;
}
.ms-icon-focus {
  outline: 1px solid #0075c0;
  z-index: 9999;
}
.white-space{
  white-space: nowrap;
  text-overflow: ellipsis;
}
</style>