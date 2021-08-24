import Vue from "vue"
import Vuex from "vuex"


Vue.use(Vuex)
const storeData = {
    state: {
        statusPopup : "",
        componentError: [],
        Toast: [],
        employeeList: [],
        Pagination: {
            currentPage: 1,
            pageSize: 20,
            pageNumber: Number,
            employeeFilter: "",
            totalPage: Number,
            totalRecord: Number,
        },

    },
    

}
const store = new Vuex.Store(storeData)
export default store


