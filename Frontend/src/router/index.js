import Vue from 'vue'
import VueRouter from 'vue-router'

import EmployeePage from '../view/dictionary/employee/EmployeePage.vue'
import Developing from '../view/dictionary/Developing.vue'

Vue.use(VueRouter);

const routes = [
  {path:"/dashboard", component: Developing},
  {path:"/", component: EmployeePage},
  {path:"/BA", component: EmployeePage},
  {path:"/PU", component: Developing},
  {path:"/SA", component: Developing},
  {path:"/IP", component: Developing},
  {path:"/IN", component: Developing},
  {path:"/SU", component: Developing},
  {path:"/FA", component: Developing},
  {path:"/TA", component: Developing},
  {path:"/JC", component: Developing},
  {path:"/GL", component: Developing},
  {path:"/BUD", component: Developing},
  {path:"/RP", component: Developing},
  {path:"/FAN", component: Developing},
];

export const router = new VueRouter({
  mode:'history',
  routes
})