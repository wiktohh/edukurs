<template>
  <v-btn icon @click="toggleTheme" class="theme-switch">
    <v-icon>{{ darkMode ? "mdi-weather-sunny" : "mdi-weather-night" }}</v-icon>
  </v-btn>
</template>

<script setup>
import { useTheme } from "vuetify";

const theme = useTheme();
const darkMode = ref(theme.global.name.value === "dark");

watch(
  () => theme.global.name.value,
  (value) => {
    darkMode.value = value === "dark";
  }
);

watch(
  () => theme.global.name.value,
  (value) => {
    darkMode.value = value === "dark";
  }
);

function toggleTheme() {
  theme.global.name.value =
    theme.global.name.value === "light" ? "dark" : "light";
}
</script>

<style lang="scss" scoped>
.theme-switch {
  width: max-content;
  height: max-content;
  padding: 0.7rem;
}
</style>
