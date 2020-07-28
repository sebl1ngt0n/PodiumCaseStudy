import Vue from 'vue'
import Router from 'vue-router'
import cookie from 'vue-cookie'
import Register from '@/components/Register'
import MortgageChecker from '@/components/MortgageChecker'

Vue.use(Router)

export default new Router({
    routes: [
        {
            path: '/',
            name: 'Register',
            component: Register
        },
        {
            path: '/mortgagechecker',
            name: 'MortgageChecker',
            component: MortgageChecker,
            beforeEnter: (to, from, next) => {
                var userId = cookie.get("user-id");
                if (userId == undefined) {
                    next('/');
                }
                else {
                    next();
                }
            }
        }
    ]
})