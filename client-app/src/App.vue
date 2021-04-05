<template>
  <v-app>
    <v-app-bar color="primary" dark app clipped-left>
      <router-link to="/">
        <v-toolbar-title>ClickUpViewer</v-toolbar-title>
      </router-link>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn text @click.stop="tokenDialog = true"> Token </v-btn>
        <v-btn to="/DailyList" text
          ><v-icon>mdi-calendar-month</v-icon>日別実績</v-btn
        >
      </v-toolbar-items>
    </v-app-bar>

    <v-main>
      <router-view :key="$route.fullPath" />

      <v-dialog v-model="tokenDialog" max-width="290">
        <v-card>
          <v-card-title>
            <span class="headline">ClickUp Api Token</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col>
                  <form onsubmit="return false;">
                  <v-text-field label="Token"></v-text-field>
                  </form>
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">
              Close
            </v-btn>
            <v-btn color="blue darken-1" text @click="dialog = false">
              Save
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
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
    tokenDialog: false,
  }),
});
</script>
