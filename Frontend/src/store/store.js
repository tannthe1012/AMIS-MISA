import Vue from "vue"
import Vuex from "vuex"


Vue.use(Vuex)
const storeData = {
    state: {
        statusPopup : "",
        componentError: [],
        Toast: [

        ],
        iconToast: {
            success: 'fas fa-check-circle',
            error: 'fas fa-exclamation-triangle',
            warning: 'fas fa-exclamation-circle',
        },
        employeeList: [],
        position:[],
        department:[],
        Pagination: {
            currentPage: 1,
            pageSize: 20,
            pageNumber: Number,
            employeeFilter: "",
            totalPage: 6,
            totalRecord: 255,
        },
        isShowLoading: false,

    },
    

}
const store = new Vuex.Store(storeData)
export default store


