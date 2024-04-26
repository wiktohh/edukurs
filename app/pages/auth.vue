<template>
  <div class="main">
    <v-btn @click="backToHomePage" variant="plain" class="back-button">
      <v-icon class="arrow-icon">mdi-arrow-left</v-icon>
      {{ $t("auth.back") }}
    </v-btn>
    <div class="theme-button">
      <LanguagesButtons />
      <ChangeThemeButton />
    </div>
    <h1>
      {{
        variant === "register"
          ? $t("auth.registerTitle")
          : $t("auth.loginTitle")
      }}
    </h1>
    <div className="container">
      <v-form @submit.prevent>
        <v-text-field
          class="input"
          v-if="variant === 'register'"
          v-model="firstName"
          :rules="[validateRequired(firstName, 'First name')]"
          :label="$t('auth.firstName')"
        ></v-text-field>
        <v-text-field
          class="input"
          v-if="variant === 'register'"
          v-model="lastName"
          :rules="[validateRequired(lastName, 'Last name')]"
          :label="$t('auth.lastName')"
        ></v-text-field>
        <v-text-field
          class="input"
          v-model="email"
          :rules="[validateRequired(email, 'Email'), validateEmail(email)]"
          :label="$t('auth.email')"
        ></v-text-field>
        <v-text-field
          class="input"
          v-model="password"
          :rules="[
            validateRequired(password, 'Password'),
            validatePassword(password),
          ]"
          :label="$t('auth.password')"
          type="password"
        ></v-text-field>
        <div class="switch-variant" v-if="variant === 'login'">
          {{ $t("auth.dontHaveAccount") }}
          <NuxtLink to="/auth?variant=register">{{
            $t("auth.login")
          }}</NuxtLink>
        </div>
        <div class="switch-variant" v-else>
          {{ $t("auth.alreadyHaveAccount") }}
          <NuxtLink to="/auth?variant=login">{{
            $t("auth.register")
          }}</NuxtLink>
        </div>
        <v-btn class="mt-2" type="submit" block>{{
          variant === "register" ? $t("auth.register") : $t("auth.login")
        }}</v-btn>
      </v-form>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import { useRoute } from "vue-router";
import {
  validateEmail,
  validatePassword,
  validateRequired,
} from "~/utils/validators";

type Variant = "login" | "register";
const route = useRoute();
const router = useRouter();
const variant = ref<Variant>((route.query.variant as Variant) || "login");

const firstName = ref("");
const lastName = ref("");
const email = ref("");
const password = ref("");

function backToHomePage() {
  router.push({ path: "/" });
}

watch(
  () => route.query.variant,
  (value) => {
    variant.value = value as Variant;
  }
);
</script>
<style lang="scss" scoped>
.main {
  display: flex;
  flex-direction: column;
  height: 100vh;
  width: 100vw;
  justify-content: center;
  align-items: center;
  gap: 1rem;

  .input {
    width: 20rem;
  }

  .back-button {
    position: absolute;
    top: 1rem;
    left: 1rem;
    .arrow-icon {
      margin-right: 0.5rem;
    }
  }
  .theme-button {
    display: flex;
    gap: 0.5rem;
    position: absolute;
    top: 1rem;
    right: 1rem;
  }

  .h1 {
    text-align: center;
  }
  .switch-variant {
    display: flex;
    justify-content: center;
    align-items: center;
    text-align: center;
    gap: 0.5rem;
    margin-bottom: 10px;
  }
}
</style>
