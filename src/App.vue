<template>
  <v-app>
    <v-app-bar color="primary" dark app clipped-left>
      <router-link to="/">
        <v-toolbar-title>ClickUpViewer</v-toolbar-title>
      </router-link>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn to="/DailyList" text
          ><v-icon>mdi-calendar-month</v-icon>日別実績</v-btn
        >
      </v-toolbar-items>
    </v-app-bar>

    <v-main>
      <router-view :key="$route.fullPath" />
    </v-main>

  </v-app>
</template>


<style scoped>
.v-toolbar__title {
  color: white;
  text-decoration: none;
}
</style>
<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "App",

  data: () => ({
  }),
});
</script>
