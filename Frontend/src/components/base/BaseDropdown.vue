<template>
  <div>
    <div class="cbx">
      <div class="cbx-show" :class="{ 'error-combobox': error == true }">
        <input
          :title="errorMessage"
          type="text"
          @keydown.down="onArrowDown"
          @keydown.up="onArrowUp"
          @keydown.enter="onEnter"
          :value="inputValue"
          @change="updateValue($event.target.value)"
          ref="refinput"
          readonly
        />
        <div class="combo-action" @click="iconOnClick">
          <div class="mi-icon-16 icon-combo"></div>
        </div>
      </div>
      <div class="cbx-hide" v-show="isShowOption">
        <div
          class="cbx-select"
          v-for="(item, index) in items"
          :key="index"
          :class="{
            active: index == arrowCounter,
          }"
          @click="select(item)"
        >
          {{ item.name }}
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { api } from "../../mixins/api";
export default {
  name: "BaseDropdown",
  mixins: [api],
  data() {
    return {
      error: false,
      errorMessage: "",
      isShowOption: false,
      valueSelect: "10 bản ghi trên 1 trang",
      inputValue: "10 bản ghi trên 1 trang",
      results: [],
      arrowCounter: 1,
      items: [
        { id: 10, name: "10 bản ghi trên 1 trang", counter: 0 },
        { id: 20, name: "20 bản ghi trên 1 trang", counter: 1 },
        { id: 30, name: "30 bản ghi trên 1 trang", counter: 2 },
        { id: 50, name: "50 bản ghi trên 1 trang", counter: 3 },
        { id: 100, name: "100 bản ghi trên 1 trang", counter: 4 },
      ],
    };
  },
  /**
   * xử lí sự kiện click ra ngoài sẽ ẩn combobox
   * created By: NTTan (19/8/2021)
   */
  mounted() {
    document.addEventListener("click", this.handleClickOutside);
  },
  destroyed() {
    document.removeEventListener("click", this.handleClickOutside);
  },
  methods: {
     /**
     * Tìm từ value sang pagesize
     * Created By: NTTan(18/8/2021)
     */
    findPageSize(value) {
      var pageSize;
      this.items.forEach(function (item) {
        if (item.name == value) {
          pageSize = item.id;
        }
      });
      return pageSize;
    },
     /**
     * Tìm từ counter sang value
     * Created By: NTTan(18/8/2021)
     */
    findInputValue(counter) {
      var value;
      this.items.forEach(function (item) {
        if (item.counter == counter) {
          value = item.name;
        }
      });
      return value;
    },
    /**
     * Tìm từ value sang counter
     * Created By: NTTan(18/8/2021)
     */
    findArrowCounter(value) {
      var arrowCounter;
      this.items.forEach(function (item, index) {
        if (item.name == value) {
          arrowCounter = index;
        }
      });
      return arrowCounter;
    },
    /**
     * xử lí sự kiện click vào icon sẽ show ra các option
     * created By: NTTan (19/8/2021)
     */
    iconOnClick() {
      this.arrowCounter = this.findArrowCounter(this.inputValue);
      this.isShowOption = !this.isShowOption;
      if (this.isShowOption == true) this.$refs.refinput.focus();
    },
    /**
     * xử lí sự kiện click vào các option
     * param: dối tượng trong option select
     * created By: NTTan (19/8/2021)
     */
    select: function (item) {
      this.isShowOption = false;
      this.inputValue = `${item.name}`;
      var pageSize = this.findId(item.name, this.items);
      this.$emit("updatePageSize", pageSize);
      this.valueSelect = this.inputValue;
      this.$refs.refinput.focus();
    },
    /**
     * Gán class active vào option được chọn
     * param: đối tượng trong option select
     * created By: NTTan (19/8/2021)
     */
    checkClassActive(index) {
      if (index == this.selectedIndex) {
        return "active";
      }
    },
    /**
     * lắng nghê sự thay đổi của input
     * created By: NTTan (19/8/2021)
     */
    onChange() {
      // this.isShowOption = false;
      this.isShowOption = true;
    },
    /**
     * Hàm lí sự kiện click ra ngoài sẽ ẩn combobox
     * created By: NTTan (19/8/2021)
     */
    handleClickOutside(event) {
      if (!this.$el.contains(event.target)) {
        this.arrowCounter = -1;
        this.isShowOption = false;
      }
    },
    /**
     * Hàm lí sự kiện sử dụng keyboard ArrowDown (phím xuống)
     * created By: NTTan (19/8/2021)
     */

    onArrowDown(e) {
      e.preventDefault();
      if (this.arrowCounter < this.items.length - 1) {
        this.arrowCounter = this.arrowCounter + 1;
      } else if (this.arrowCounter == this.items.length - 1) {
        this.arrowCounter = 0;
      }
      this.inputValue = this.findInputValue(this.arrowCounter);
    },
    /**
     * Hàm lí sự kiện sử dụng keyboard ArrowUp (phím lên)
     * created By: NTTan (19/8/2021)
     */
    onArrowUp(e) {
      e.preventDefault();
      if (this.arrowCounter > 0) {
        this.arrowCounter = this.arrowCounter - 1;
      } else if (this.arrowCounter == 0) {
        this.arrowCounter = this.items.length - 1;
      } else {
        this.arrowCounter = this.items.length - 1;
      }
      this.inputValue = this.findInputValue(this.arrowCounter);
    },
    /**
     * Hàm lí sự kiện sử dụng keyboard Enter
     * created By: NTTan (19/8/2021)
     */
    onEnter() {
      this.inputValue = this.findInputValue(this.arrowCounter);
      var pageSize = this.findPageSize(this.inputValue);
      this.$emit("updatePageSize", pageSize);
      this.valueSelect = this.inputValue;
      this.isShowOption = false;
      this.$refs.refinput.focus();
    },
    /**
     * Hàm tìm id của đối tượng thông qua value là name
     * param(result) : value của input
     * param(array): list obj
     * Created By: NTTan (20/8/2021)
     */
    findId(result, array) {
      const obj = array.find((item) => item.name == result);
      return obj.id;
    },
  },
};
</script>
<style scoped>
@import "../../css/common/combobox.css";
.cbx {
  margin-bottom: 0px;
}
.cbx-hide {
  z-index: 200;
  top: -166px;
}
</style>