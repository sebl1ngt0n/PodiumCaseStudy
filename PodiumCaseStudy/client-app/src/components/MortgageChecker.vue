<template>
    <form id="mortgagechecker-form" v-on:submit.prevent="submit">
        <div class="row">
            <div class="col-12 form-group">
                <label class="col-form-label">Property Value <span class="text-danger">*</span></label>
                <input type="text" v-model.trim="$v.propVal.$model" :class="{'is-invalid': validationStatus($v.propVal)}" class="form-control">
                <div v-if="!$v.propVal.required" class="invalid-feedback">The property value field is required.</div>
                <div v-if="!$v.propVal.numeric" class="invalid-feedback">The property value should be numeric.</div>
            </div>
            <div class="col-12 form-group">
                <label class="col-form-label">Deposit <span class="text-danger">*</span></label>
                <input type="text" v-model.trim="$v.deposit.$model" :class="{'is-invalid': validationStatus($v.deposit)}" class="form-control">
                <div v-if="!$v.deposit.required" class="invalid-feedback">The deposit field is required.</div>
                <div v-if="!$v.deposit.numeric" class="invalid-feedback">The deposit value should be numeric.</div>
                <div v-if="!$v.deposit.deposit_greater" class="invalid-feedback">The deposit should be less than the property value</div>
            </div>

            <div class="col-12 form-group text-center">
                <button class="btn btn-vue btn-lg col-4" type="submit">Get Products</button>
            </div>

            <div class="col-12" id="available-products" v-if="products.length">
                <h5>Available Products</h5>
                <div v-for="product in products" class="product">
                    <div>
                        <span>Lender</span>
                        <span>{{ product.lender }}</span>
                    </div>
                    <div>
                        <span>Interest Rate</span>
                        <span>{{ product.interestRate }}%</span>
                    </div>
                    <div>
                        <span>Mortgage Type</span>
                        <span>{{ product.mortgageType }}</span>
                    </div>
                    <div class="text-center">
                        <button class="btn btn-vue">Details</button>
                    </div>
                </div>
            </div>

            <div class="col-12" id="available-products" v-if="noProducts">
                <span>No mortgage options available</span>
            </div>
        </div>
    </form>
</template>

<script>
    import { required, numeric } from 'vuelidate/lib/validators'
    import axios from 'axios'
    import cookie from 'vue-cookie'

    const deposit_greater = (value, vm) => !(value >= parseInt(vm.propVal));

    export default {
        name: 'MortgageCheckerForm',
        data: function () {
            return {
                propVal: '',
                deposit: '',
                products: [],
                noProducts: false
            }
        },
        validations: {
            propVal: { required, numeric },
            deposit: { required, numeric, deposit_greater }
        },
        methods: {
            validationStatus: function (validation) {
                return typeof validation != "undefined" ? validation.$error : false;
            },
            submit: async function () {
                this.$v.$touch();

                if (this.$v.$pendding || this.$v.$error) {
                    return false;
                }

                this.noProducts = false
                this.products = []

                var paramDict = {
                    UserId: cookie.get("user-id"),
                    PropertyValue: this.propVal,
                    Deposit: this.deposit
                };

                const getProducts = async () => {
                    try {
                        return await axios.get("/Mortgage/GetAvailableMortgages", { params: paramDict });
                    }
                    catch (error) {
                        console.error(error);
                    }
                }

                const showProducts = async () => {
                    const products = await getProducts()

                    if (products.data.length) {
                        this.products = products.data
                    }
                    else {
                        this.noProducts = true
                    }
                };

                showProducts();
            }
        }
    }
</script>

<style>
    #available-products {
        display: flex;
        flex-direction: column;
    }

        #available-products .product {
            display: flex;
            justify-content: space-between;
            padding-bottom: 5px;
            border-bottom: 1px solid #80808029;
            margin-bottom: 5px;
        }

            #available-products .product > div {
                flex-basis: 50%;
                display: flex;
                flex-direction: column;
                font-size: 12px;
            }

            #available-products .product > div span:first-child {
                font-size: 10px;
                color: grey;
                text-transform:uppercase;
            }
</style>