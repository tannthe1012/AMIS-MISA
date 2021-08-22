<template>
  <div class="modal-popup" id="modal-popup" ref="Popup">
    <div class="popup">
      <div class="content-popup">
        <div class="mi-icon-48" :class="dataPopup.classIcon"></div>
        <div class="message-popup">
          <span>{{ dataPopup.title }}</span>
        </div>
      </div>
      <div class="mess-line"></div>
      <div
        class="form-footer"
        :class="[
          { 'justify-content-center': statusPopup == `ERROR` },
          { 'justify-content-right': statusPopup == `EXIST` },
        ]"
      >
        <div class="form-footer-left" v-if="statusPopup != `ERROR` && statusPopup != `EXIST`">
          <button class="m-btn-default" @click="closePopup">Hủy</button>
        </div>
        <div class="form-footer-right">
          <button
            v-if="statusPopup == 'CLOSE'"
            class="m-btn-default"
            @click="closePopupAndForm"
          >
            Không
          </button>
          <button class="m-btn" @click="confirmPopup">
            {{ dataPopup.buttonConfirm }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "BasePopup",
  props: {
    dataPopup: {
      classIcon: String,
      title: String,
      buttonConfirm: String,
    },
  },
  data() {
    return {
      statusPopup: String,
    };
  },
  /**
   * Hàm khơi tạo sẽ lấy statusPopup ở store
   * Do v-if render HTML trước khi nhân props
   * Created By: NTTan(18/8/2021)
   */
  created() {
    this.statusPopup = this.$store.state.statusPopup;
  },
  methods: {
    /**
     * Hàm gửi yêu cầu confirm popup và gửi lên EmployeePage
     * Created By: NTTan (19/8/2021)
     */
    confirmPopup() {
      // this.statusPopup = this;
      this.$emit("confirmPopup");
    },
    /**
     * Hàm gửi yêu cầu đóng Popup và gửi lên EmployeePage
     * Created By: NTTan (19/8/2021)
     */
    closePopup() {
      this.$emit("closePopup");
    },
    /**
     * Hàm đóng cả Popup lẫn Form chi tiết và gửi lên EmployeePage
     * Lưu ý: chỉ dùng cho Popup Close Form
     * Created by: NTTan (19/8/2021)
     */
    closePopupAndForm() {
      this.$emit("closePopupAndForm");
    },
  },
};
</script>
<style scoped>
@import "../../css/common/popup.css";
.justify-content-center {
  justify-content: center !important;
}
.justify-content-right {
   /* justify-content: right !important; */
   flex-direction: row-reverse;
}

</style>
