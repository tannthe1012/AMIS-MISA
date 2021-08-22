<template>
  <label class="ms-checkbox" for="" @click="handleClickCheckbox">
    <input class="ms-checkbox-input" type="checkbox" name="" />
    <span class="ms-border-checkbox" :class="{ 'border-active': active == true }">
      <span class="ms-checkbox-inner">
        <div v-show="active == true" class="mi-icon-16 icon-checkbox-active"></div>
      </span>
    </span>
  </label>
</template>
<script>
export default {
  name: "BaseCheckBox",
  props: {
    selectAll: {
      type: Boolean,
      default: false,
    },
    selectAllCheckBox: {
        type: Boolean,
      default: false,
    }
  },
  data() {
      return {
          active: false,
      }
  },
  watch: {		
		selectAllCheckBox(newValue) {
			this.active = newValue;
		},
  },
  methods: {
    /**
     * Hàm xử lý click vào các checkbox
     * Created By: NTTan(18/8/2021)
     */
      handleClickCheckbox() {
          this.active = !this.active;
          if (this.selectAll == true) {
              this.$emit("selectAll");
          }
      }
  },
};
</script>
<style scoped>

</style>