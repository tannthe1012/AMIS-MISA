<template>
  <div class="ms-pagination">
    <div class="left-pagination">
      <div class="total-record">Tổng số: <b>{{this.$store.state.Pagination.totalRecord}}</b> bản ghi</div>
    </div>
    <div class="right-pagination">
      <BaseDropdown @updatePageSize="updatePageSize"/>
      <div class="navigations">
        <div class="pre-page" :class="{'untext': currentPage == 1}" @click="onClickPrePage">Trước</div>

        <div class="list-page">
          <div class="page-index" v-show="currentPage >= 3" @click="onClickFirstPage">1</div>
          <div class="page-index" v-show="currentPage >= 3">...</div>
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
          <div class="page-index" v-show="currentPage <= this.$store.state.Pagination.totalPage - 3">...</div>
          <div class="page-index" v-show="currentPage <= this.$store.state.Pagination.totalPage - 3" @click="onClickLastPage">
            {{ this.$store.state.Pagination.totalPage }}
          </div>
        </div>
        <div class="next-page" :class="{'untext': currentPage == this.$store.state.Pagination.totalPage}" @click="onClickNextPage">Sau</div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseDropdown from "./BaseDropdown.vue";
import { api } from "../../mixins/api";
export default {
  name: "BasePagination",
  components: {
    BaseDropdown,
  },
  mixins: [api],
  data() {
    return {
      currentPage: Number,
      listPage: [],
      lastPage: Number,
      totalPage: Number,
    };
  },

  async mounted() {
    await this.getAllEmployee();
    this.currentPage = this.$store.state.Pagination.currentPage;
    this.lastPage = this.$store.state.Pagination.totalPage;
    this.renderList(1);
  },
  // watch: {
  //   lastPage
  // },
  methods: {
    /**
     * render các list div number page
     * param(number): current page
     * Created By: NTTan (20/8/2021)
     */
    renderList(number) {
      this.listPage = [];
      if (number >= this.lastPage - 1 && number <= this.lastPage) {
        this.renderList(this.lastPage - 2);
      } else {
        for (let i = 0; i <= 2; i++) {
          this.listPage.push(number + i);
        }
      }
    },
    updatePageSize(pageSize) {
      this.$store.state.Pagination.pageSize = pageSize;
      this.currentPage = 1;
      this.$store.state.Pagination.currentPage = 1;
      this.renderList(1);
      this.getAllEmployee();
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
      this.getAllEmployee();
    },
    /**
     * Xử lý sự kiện nextPage
     * Created By: NTTan (20/8/2021)
     */
    onClickNextPage() {
      if (this.currentPage == this.lastPage) {
        return;
      }
      this.currentPage = this.currentPage + 1;
      this.renderList(this.currentPage);
      this.$store.state.Pagination.currentPage = this.currentPage;
      this.getAllEmployee();
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
      this.getAllEmployee();
    },
    /**
     * Xử lý sự kiện khi tới Page cuối cùng
     * Created By: NTTan (20/8/2021)
     */
    onClickLastPage() {
      this.currentPage = this.lastPage;
      this.renderList(this.currentPage);
      this.$store.state.Pagination.currentPage = this.currentPage;
      this.getAllEmployee();
    },
    /**
     * Xử lý sự kiện khi tới Page đầu tiên
     * Created By: NTTan (20/8/2021)
     */
    onClickFirstPage() {
      this.currentPage = 1;
      this.renderList(this.currentPage);
      this.$store.state.Pagination.currentPage = this.currentPage;
      this.getAllEmployee()
    },
  },
};
</script>
<style scoped>
@import "../../css/common/colab.css";

</style>