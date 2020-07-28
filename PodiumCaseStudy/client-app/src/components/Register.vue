<template>
    <form id="register-form" v-on:submit.prevent="submit">
        <div class="row">
            <div class="col-12 form-group">
                <label class="col-form-label">First Name <span class="text-danger">*</span></label>
                <input type="text" v-model.trim="$v.firstname.$model" :class="{'is-invalid': validationStatus($v.firstname)}" class="form-control">
                <div v-if="!$v.firstname.required" class="invalid-feedback">The first name field is required.</div>
                <div v-if="!$v.firstname.maxLength" class="invalid-feedback">The maxiumum surname length is 50 characters.</div>
            </div>
            <div class="col-12 form-group">
                <label class="col-form-label">Surname <span class="text-danger">*</span></label>
                <input type="text" v-model.trim="$v.surname.$model" :class="{'is-invalid': validationStatus($v.surname)}" class="form-control">
                <div v-if="!$v.surname.required" class="invalid-feedback">The surname field is required.</div>
                <div v-if="!$v.surname.maxLength" class="invalid-feedback">The maxiumum surname length is 50 characters.</div>
            </div>
            <div class="col-12 form-group">
                <label class="col-form-label">Email <span class="text-danger">*</span></label>
                <input type="email" v-model.trim="$v.email.$model" :class="{'is-invalid': validationStatus($v.email)}" class="form-control">
                <div v-if="!$v.email.required" class="invalid-feedback">The email field is required.</div>
                <div v-if="!$v.email.email" class="invalid-feedback">The email is not valid.</div>
            </div>

            <div class="col-12 form-group">
                <label class="col-form-label">Date of Birth <span class="text-danger">*</span></label>
                <datetime v-model="dateOfBirth" :class="{'is-invalid': validationStatus($v.dateOfBirth)}" input-class="form-control"></datetime>
                <div v-if="!$v.dateOfBirth.required" class="invalid-feedback">The Date of Birth field is required.</div>
            </div>

            <div class="col-12 form-group text-center">
                <button class="btn btn-vue btn-lg col-4" type="submit">Register</button>
            </div>
        </div>
    </form>
</template>

<script>
    import { required, email, maxLength } from 'vuelidate/lib/validators'
    import { Datetime } from 'vue-datetime';
    import axios from 'axios'
    import cookie from 'vue-cookie'
    import router from '@/router'

    export default {
        name: 'RegisterForm',
        components: {
            datetime: Datetime
        },
        data: function () {
            return {
                firstname: '',
                surname: '',
                email: '',
                dateOfBirth: ''
            }
        },
        validations: {
            firstname: { required, maxLength:  maxLength(50) },
            surname: { required, maxLength: maxLength(50) },
            email: { required, email },
            dateOfBirth: { required },
        },
        methods: {
            validationStatus: function (validation) {
                return typeof validation != "undefined" ? validation.$error : false;
            },
            submit: function () {
                this.$v.$touch();

                if (this.$v.$pendding || this.$v.$error) {
                    return false;
                }

                axios.post("/Register", {
                    Firstname: this.firstname,
                    Surname: this.surname,
                    Email: this.email,
                    DateOfBirth: this.dateOfBirth
                })
                    .then(function (response) {
                        var userId = response.data;
                        cookie.set("user-id", userId, 1);
                        router.push({ name: 'MortgageChecker' });
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        }
    }
</script>