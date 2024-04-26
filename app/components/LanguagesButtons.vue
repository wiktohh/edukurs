<template>
  <div v-for="(language, index) in languages" :key="index">
    <v-btn icon @click="toggleLanguage(language)" class="theme-switch">
      <img :src="languageImages[language]" :alt="'Flag of ' + language" />
    </v-btn>
  </div>
</template>

<script lang="ts" setup>
import pl from "~/assets/pl.svg";
import en from "~/assets/en.svg";
const languages = ["pl", "en"];

const { locale, setLocale } = useI18n();

const languageImages = {
  pl,
  en,
};

function toggleLanguage(language: string) {
  setLocale(language);
  localStorage.setItem("locale", language);
}
</script>

<style lang="scss" scoped>
.theme-switch {
  width: 3rem;
  height: 3rem;
  padding: 0.7rem;

  img {
    width: 100%;
    height: 100%;
    border-radius: 100%;
  }
}
</style>
