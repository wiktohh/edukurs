import ChangeThemeButton from '../ChangeThemeButton.vue';
<template>
  <v-app-bar>
    <v-toolbar-title class="title">Edukurs</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn to="/home">Home</v-btn>
    <v-btn to="/courses">All courses</v-btn>
    <v-btn to="/admin">Administration panel</v-btn>
    <v-btn to="/auth?variant=register">Logout</v-btn>
    <ChangeThemeButton />
  </v-app-bar>
</template>
<style lang="scss" scoped>
.title {
  text-transform: uppercase;
  letter-spacing: 2px;
  font-size: 1.5rem;
}
</style>
