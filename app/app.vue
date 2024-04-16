import ChangeThemeButton from './components/ChangeThemeButton.vue';
<template>
  <NuxtLayout>
    <v-app>
      <NuxtPage />
    </v-app>
  </NuxtLayout>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { onBeforeMount } from "vue";

const { setLocale } = useI18n();

onBeforeMount(() => {
  const savedLocale = localStorage.getItem("locale") || "en";
  if (savedLocale) {
    setLocale(savedLocale);
  }
});
</script>
