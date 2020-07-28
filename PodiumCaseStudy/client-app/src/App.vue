<template>
    <div id="app" class="h-100">
        <div class="container-fluid h-100">
            <div class="row h-100">
                <div class="col-md-3 vue-bg h-100 d-flex justify-content-center align-items-center">
                    <img alt="Vue logo" src="./assets/logo.png" width="100">
                </div>
                <div class="col-md-9 h-100 d-flex justify-content-center align-items-center">
                    <div class="col-md-8 rounded px-5 py-4 shadow bg-white text-left">
                        <router-view />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">

    export default {
        name: 'app'
    }

</script>

<style>
    .vue-bg {
        background: #bce5d0;
    }

    .btn-vue {
        background: #53B985;
        color: #31485D;
        font-weight: bold;
    }
</style>
