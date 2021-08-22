<template>
  <div class="loading" v-show="this.$store.state.employeeList.length == 0">
    <div class="loader">
      <object :data="loaderUrl" type=""></object>
    </div>
  </div>
</template>
<script>
export default {
  name: "BaseLoading",
  data() {
    return {
      loaderUrl: require("../../assets/loading.svg"),
    };
  },
};
</script>
<style scoped>
.loading {
  display: flex;
  align-items: center;
  /* Hidden by default */
  position: fixed;
  /* Stay in place */
  z-index: 200;
  /* Sit on top */
  /* Location of the box */
  width: 100%;
  top: 0;
  left: 0;
  /* Full width */
  height: 100%;
  /* Full height */
  overflow: auto;
  /* Enable scroll if needed */
  background-color: rgb(0, 0, 0);
  /* Fallback color */
  background-color: rgba(0, 0, 0, 0.4);
  justify-content: center;
}
</style>