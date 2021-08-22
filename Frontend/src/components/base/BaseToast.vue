<template>
    <div class="toast" :class="toast.classtoast">
      <div class="toast-icon">
        <i :class="toast.icontoast"></i>
      </div>
      <div class="toast-text">{{toast.message}}</div>
      <div class="toast-icon toast-close" @click="removeArrayToast">
        <i class="fas fa-times"></i>
      </div>
    </div>
</template>
<script>
export default {
    name: "BaseToast",
    props: {
        toast:Object,
    },
    mounted() {
      setTimeout(() => {
        this.removeArrayToast();
      },3000); 
    },
    methods: {
      /**
       * Xóa Toast trong mảng
       * Created By: NTTan (20/8/2021)
       */
      removeArrayToast() {
        this.$store.state.Toast = this.$store.state.Toast.filter((item) => item.id != this.toast.id);
      },

    }
}
</script>
<style scoped>
@import "../../css/common/toast.css";
</style>
