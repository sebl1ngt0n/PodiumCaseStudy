import Vue from 'vue'
import App from './App.vue'
import router from './router'
import Vuelidate from 'vuelidate'
import axios from 'axios'
import { Datetime } from 'vue-datetime'
import 'vue-datetime/dist/vue-datetime.css'

Vue.config.productionTip = false;
Vue.use(Vuelidate);
Vue.use(Datetime);
Vue.use(require('vue-cookie'));
Vue.prototype.$http = axios;

new Vue({
    router,
    render: (h) => h(App),
    template: '<App/>',
    components: { App }
}).$mount('#app');





