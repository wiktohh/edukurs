<template>
  <v-app-bar>
    <v-toolbar-title class="title">Edukurs</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn to="/home">{{ $t("menu.home") }}</v-btn>
    <v-btn to="/courses">{{ $t("menu.allCourses") }}</v-btn>
    <v-btn to="/admin">{{ $t("menu.administrationPanel") }}</v-btn>
    <v-btn to="/auth?variant=register">{{ $t("auth.logout") }}</v-btn>
    <LanguagesButtons />
    <ChangeThemeButton />
  </v-app-bar>
</template>

<script>
import ChangeThemeButton from "../ChangeThemeButton.vue";
</script>

<style lang="scss" scoped>
.title {
  text-transform: uppercase;
  letter-spacing: 2px;
  font-size: 1.5rem;
}
</style>
