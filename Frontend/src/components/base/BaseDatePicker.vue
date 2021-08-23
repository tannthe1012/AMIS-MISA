<template>
  <div class="form-input">
    <label class="flex" for="name">
      <div class="ms-title-input">{{ label }}</div>
    </label>
    <DatePicker
      v-model="valueInput"
      type="date"
      tabindex="-1"
      :value-type="valueType"
      :format="display"
      :placeholder="display"
      :title-format="display"
      :input-attr="{ tabindex: tabindex }"
      :input-class="'ms-input-date'"
      ref="refinputdate"
    >
    </DatePicker>
  </div>
</template>
<script>
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";
import "vue2-datepicker/locale/vi";
export default {
  name: "BaseDatePicker",
  components: {
    DatePicker,
  },
  props: {
    value: String,
    label: String,
    tabindex: String,
    valueType: {
      type: String,
      default: "YYYY-MM-DD",
    },
    display: {
      type: String,
      default: "DD/MM/YYYY",
    },
  },
  data() {
    return {
      valueInput: this.value,
      isDisabled: {
        type: Boolean,
        required: false,
        default: false,
      },
    };
  },
  created() {
    this.valueInput = this.value;
  },
  watch: {
    /**
     * Theo dõi value từ props
     * Created By: NTTan(20/8/2021)
     */
    value: function (newDateValue) {
      this.valueInput = newDateValue;
    },

    /**
     * Theo dõi dateValue
     * Khi thay đổi, emit để thay đổi giá trị của prop value
     * Created By: NTTan(20/8/2021)
     */
    valueInput: function (newDateValue) {
      this.$emit("input", newDateValue);
    },
  },
};
</script>
<style >
@import "../../css/common/form.css";
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
.ms-form-content label {
  max-height: 16px;
  box-sizing: border-box;
}
input:hover {
  outline: 1px solid #e2e2e2;
}
.mx-datepicker {
  width: 100%;
  height: 32px;
}
/* .mx-icon-calendar{
    position: absolute;
    top: 30%;
} */
.mx-icon-calendar,
.mx-icon-clear {
  position: absolute;
  top: 38% !important;
}
.mx-btn:hover {
  border-color: #35bf22;
  color: #35bf22;
}
.mx-calendar-content .cell:hover {
  color: #111;
  background-color: #f3f8f8;
}
</style>