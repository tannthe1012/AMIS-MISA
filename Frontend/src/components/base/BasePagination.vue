<template>
  <div class="ms-pagination">
    <div class="left-pagination">
      <div class="total-record">
        Tổng số: <b>{{ this.$store.state.Pagination.totalRecord }}</b> bản ghi
      </div>
    </div>
    <div class="right-pagination">
      <BaseDropdown @updatePageSize="updatePageSize" />
      <div class="navigations">
        <div
          class="pre-page"
          :class="{ untext: currentPage == 1 }"
          @click="onClickPrePage"
        >
          Trước
        </div>

        <div class="list-page">
          <div
            class="page-index"
            v-show="this.$store.state.Pagination.currentPage >= 3"
            @click="onClickFirstPage"
          >
            1
          </div>
          <div
            class="page-index"
            v-show="this.$store.state.Pagination.currentPage >= 3"
          >
            ...
          </div>
          <div
            class="page-index"
            :class="[
              { 'notosans-bold': currentPage == number },
              { 'border-page-index': currentPage == number },
            ]"
            v-for="number in listPage"
            :key="number"
            @click="updateCurrentPage(number)"
          >
            {{ number }}
          </div>
          <div
            class="page-index"
            v-show="
              this.$store.state.Pagination.currentPage <=
              this.$store.state.Pagination.totalPage - 3
            "
          >
            ...
          </div>
          <div
            class="page-index"
            v-show="
              this.$store.state.Pagination.currentPage <=
              this.$store.state.Pagination.totalPage - 3
            "
            @click="onClickLastPage"
          >
            {{ this.$store.state.Pagination.totalPage }}
          </div>
        </div>
        <div
          class="next-page"
          :class="{
            untext:
              this.$store.state.Pagination.currentPage ==
              this.$store.state.Pagination.totalPage,
          }"
          @click="onClickNextPage"
        >
          Sau
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseDropdown from "./BaseDropdown.vue";
// import { api } from "../../mixins/api";
// import employeeApi from "../../api/components/EmployeeAPI";
export default {
  name: "BasePagination",
  components: {
    BaseDropdown,
  },
  // mixins: [api],
  data() {
    return {
      currentPage: this.$store.state.Pagination.currentPage,
      listPage: [],
      lastPage: this.$store.state.Pagination.totalPage,
      totalPage: Number,
    };
  },

  async mounted() {
    (this.lastPage = this.$store.state.Pagination.totalPage),
      this.renderList(1);
  },
  methods: {
    // /**
    //  * Hàm lấy dữ liệu của table
    //  * Created By:  NTTan (17/8/2021)
    //  */
    // getEmployees() {
    //   this.$store.state.isShowLoading = true;
    //   this.$store.state.employeeList = [];
    //   let pageSize = this.$store.state.Pagination.pageSize;
    //   let pageindex = this.$store.state.Pagination.currentPage;
    //   let employeeFilter = this.$store.state.Pagination.employeeFilter;
    //   employeeApi
    //     .getEmployeesFilterPaging(pageSize, pageindex, employeeFilter)
    //     .then((response) => {
    //       this.$store.state.employeeList = response.data.Data;
    //       this.$store.state.Pagination.totalPage = response.data.TotalPage;
    //       this.$store.state.Pagination.totalRecord = response.data.TotalRecord;
    //     })
    //     .catch(function (error) {
    //       console.log(error);
    //     });
    //   this.$store.state.isShowLoading = false;
    // },
    /**
     * render các list div number page
     * param(number): current page
     * Created By: NTTan (20/8/2021)
     */
    renderList(number) {
      this.listPage = [];
      if (
        number >= this.$store.state.Pagination.totalPage - 1 &&
        number <= this.$store.state.Pagination.totalPage
      ) {
        this.renderList(this.$store.state.Pagination.totalPage - 2);
      } else {
        for (let i = 0; i <= 2; i++) {
          this.listPage.push(number + i);
        }
      }
    },
    updatePageSize(pageSize) {
      console.log("da vao toi day");
      this.$store.state.Pagination.pageSize = pageSize;
      this.currentPage = 1;
      this.$store.state.Pagination.currentPage = 1;
      this.renderList(1);
      this.$emit("getEmployees");
    },
    /**
     * Xử lý sự kiện khi Click
     * param(number): current page
     * Created By: NTTan (20/8/2021)
     */
    updateCurrentPage(number) {
      this.currentPage = number;
      this.$store.state.Pagination.currentPage = this.currentPage;
      this.renderList(number);
      this.$emit("getEmployees");
    },
    /**
     * Xử lý sự kiện nextPage
     * Created By: NTTan (20/8/2021)
     */
    onClickNextPage() {
      if (this.currentPage == this.$store.state.Pagination.totalPage) {
        return;
      }
      this.currentPage = this.currentPage + 1;
      this.renderList(this.currentPage);
      this.$store.state.Pagination.currentPage = this.currentPage;
      this.$emit("getEmployees");
    },
    /**
     * Xử lý sự kiện Prepage
     * Created By: NTTan (20/8/2021)
     */
    onClickPrePage() {
      if (this.currentPage == 1) {
        return;
      }
      this.currentPage = this.currentPage - 1;
      this.renderList(this.currentPage);
      this.$store.state.Pagination.currentPage = this.currentPage;
      this.$emit("getEmployees");
    },
    /**
     * Xử lý sự kiện khi tới Page cuối cùng
     * Created By: NTTan (20/8/2021)
     */
    onClickLastPage() {
      this.currentPage = this.$store.state.Pagination.totalPage;
      this.renderList(this.currentPage);
      this.$store.state.Pagination.currentPage = this.currentPage;
      this.$emit("getEmployees");
    },
    /**
     * Xử lý sự kiện khi tới Page đầu tiên
     * Created By: NTTan (20/8/2021)
     */
    onClickFirstPage() {
      this.currentPage = 1;
      this.renderList(this.currentPage);
      this.$store.state.Pagination.currentPage = this.currentPage;
      this.$emit("getEmployees");
    },
  },
};
</script>
<style scoped>
@import "../../css/common/colab.css";
</style>