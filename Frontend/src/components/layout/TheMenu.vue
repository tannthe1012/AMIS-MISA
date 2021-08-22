<template>
  <div class="navbar">
    <div class="logo-site">
      <div class="toggle-icon"></div>
      <div class="amis-logo"></div>
    </div>
    <div class="navbar-content">
      <router-link
        class="nav-item"
        v-for="(item, index) in menuList"
        :key="index"
        :to="item.route"
        :class="{ 'menu-active': index == indexSelected }"
        @click.native="updateMenu(index)"
      >
        <div
          class="nav-item-icon"
          :class="[item.icon, { 'icon-menu-active': index == indexSelected }]"
        ></div>
        <div
          class="nav-item-content"
          :class="[{ 'content-active': index == indexSelected }]"
        >
          {{ item.text }}
        </div>
      </router-link>
    </div>
  </div>
</template>
<script>
export default {
  name: "TheMenu",
  data() {
    return {
      indexSelected: 1,
      val: true,
      minimenu: Boolean,
      menuList: [
        {
          text: "Tổng quan",
          icon: "icon-dashboard",
          route: "/dashboard",
        },
        {
          text: "Tiền mặt",
          icon: "icon-money-cash",
          route: "/",
        },
        {
          text: "Tiền gửi",
          icon: "icon-money-deposits",
          route: "/BA",
        },
        {
          text: "Mua hàng",
          icon: "icon-buy",
          route: "/PU",
        },
        {
          text: "Bán hàng",
          icon: "icon-sell",
          route: "/SA",
        },
        {
          text: "Quản lý hóa đơn",
          icon: "icon-bill",
          route: "/IP",
        },
        {
          text: "Kho",
          icon: "icon-warehouse",
          route: "/IN",
        },
        {
          text: "Công cụ dụng cụ",
          icon: "icon-tools",
          route: "/SU",
        },
        {
          text: "Tài sản cố định",
          icon: "icon-fixed-assets",
          route: "/FA",
        },
        {
          text: "Thuế",
          icon: "icon-tax",
          route: "/TA",
        },
        {
          text: "Giá thành",
          icon: "icon-cost",
          route: "/JC",
        },
        {
          text: "Tổng hợp",
          icon: "icon-synthetic",
          route: "/GL",
        },
        {
          text: "Ngân sách",
          icon: "icon-budget",
          route: "/BUD",
        },
        {
          text: "Báo cáo",
          icon: "icon-report",
          route: "/RP",
        },
        {
          text: "Phân tích tài chính",
          icon: "icon-analysis",
          route: "/FAN",
        },
      ],
    };
  },
  watch: {
    minimenu: function () {
      this.minimenu = this.mini;
    },
  },
  props: {
    mini: Boolean,
  },
  methods: {
    /**
     * Hàm update lại menu được active
     * Created By: NTTan (20/8/2021)
     */
    updateMenu(index) {
      this.indexSelected = index;
    },
  },
};
</script>
<style lang="css" scoped>
@import "../../css/common/navbar.css";
.icon-dashboard {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -30px -1628px;
}
.icon-money-cash {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -73px -1628px;
}
.icon-money-deposits {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -117px -1628px;
}
.icon-buy {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -161px -1628px;
}
.icon-sell {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -204px -1628px;
}
.icon-bill {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -249px -1628px;
}
.icon-warehouse {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -291px -1628px;
}
.icon-tools {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -336px -1628px;
}
.icon-fixed-assets {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -378px -1630px;
}
.icon-tax {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -423px -1630px;
}
.icon-cost {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -467px -1628px;
}
.icon-synthetic {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -510px -1628px;
}
.icon-budget {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -378px -1659px;
}
.icon-report {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -547px -1628px;
}
.icon-analysis {
  background: url(../../assets/imgs/Sprites.64af8f61.svg) no-repeat -204px -1664px;
}
</style>