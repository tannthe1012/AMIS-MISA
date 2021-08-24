<template>
  <div class="form-input p-l-10">
    <label class="flex ms-title-input" for="name">{{ label }}</label>
    <div class="flex">
      <label
        class="container m-r-20"
        v-for="(item, index) in option"
        :key="index"
      >
        {{ item.name }}
        <input
          type="radio"
          :tabindex="tabindex"
          :value="item.id"
          :checked="valueSelect == item.id"
          name="radio"
          @input="updateValue($event.target.value)"
        />
        <span class="checkmark"></span>
      </label>
    </div>
  </div>
</template>
<script>
export default {
  name: "BaseRadio",
  props: {
    value: {
      type: Number,
    },
    tabindex: String,
    label: String,
    type: {
      type: String,
      default: "text",
    },
    items: Array,
  },
  data() {
    return {
      option: this.items,
      valueSelect: this.value,
    };
  },
  mounted() {
    this.setDefaultValue();
  },
  watch: {
    value: function () {
      this.valueSelect = this.value;
    },
    valueSelect: function () {
      this.$emit("resultRadio", this.valueSelect);
    },
  },
  methods: {
    /**
     * Hàm updatevalue của radio
     * param: value của input 0 1 2
     * Created By: NTTan(20/8/2021)
     */
    updateValue(value) {
      this.valueSelect = Number(value);
    },
    /**
     * Hàm set default của radio
     * Created By: NTTan(20/8/2021)
     */
    setDefaultValue() {
      if (!this.value) {
        this.valueSelect = 0;
      }
    },
  },
};
</script>
<style scoped>
@import url("../../css/common/radio.css");
@font-face {
  font-family: "notosans-bold";
  src: url("../../assets/fonts/notosans-bold.3ede5aff.woff2") format("opentype");
}
.ms-title-input {
  font-size: 12px;
  white-space: nowrap;
  font-family: notosans-bold;
  cursor: grab;
}
.space-20 {
  width: 20px;
  height: 20px;
  cursor: default;
}
</style>