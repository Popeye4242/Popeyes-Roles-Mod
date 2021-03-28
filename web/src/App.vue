<template>
  <div class="h-100 d-flex flex-column ">
    <Navigation />
    <router-view/>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Navigation from "@/components/Navigation.vue";

export default defineComponent({
  name: 'App',
  components: {
    Navigation
  },
});
</script>


<style lang="scss">
// Your variable overrides
$body-bg: #303846;
$body-color: #f5f6f7;

@import "node_modules/bootstrap/scss/bootstrap";
@import "node_modules/bootstrap/scss/functions";
@import "node_modules/bootstrap/scss/variables";
@import "node_modules/bootstrap/scss/mixins";


#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}

body, html, #app {
  height: 100%;
}
</style>
