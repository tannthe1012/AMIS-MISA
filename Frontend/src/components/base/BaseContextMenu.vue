<template>
    <div class="contextmenu">
        
            <div class="contextmenu-tooltip" @click="duplicateEmployee">Nhân bản</div>
            <div class="contextmenu-tooltip" @click="showPopupDetele">Xóa</div>
            <div class="contextmenu-tooltip" @click="showPopupDetele">Ngừng sử dụng</div>
       
    </div>
</template>
<script>
export default {
    name: "BaseContextMenu",
    methods: {
        /**
         * Xử lí sự kiện khi nhấn nút Xóa
         * Created By: NTTan(18/8/2021)
         */
        showPopupDetele() {
            this.$emit("showPopupDelete");
        },
        /**
         * Xử lí sự kiện khi nhấn nút Sửa
         * Created By: NTTan(18/8/2021)
         */
        duplicateEmployee() {
            this.$emit("duplicateEmployee");
        }
    },
}
</script>
<style scoped>
    @import '../../css/common/contextmenu.css'
</style>