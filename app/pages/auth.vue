<template>
  <div class="main">
    <h1>{{ variant === "register" ? "Rejestracja" : "Logowanie" }}</h1>
    <div className="container">
      <v-form @submit.prevent>
        <v-text-field
          v-if="variant === 'register'"
          v-model="firstName"
          :rules="[validateRequired(firstName, 'First name')]"
          label="First name"
        ></v-text-field>
        <v-text-field
          v-if="variant === 'register'"
          v-model="lastName"
          :rules="[validateRequired(lastName, 'Last name')]"
          label="Last name"
        ></v-text-field>
        <v-text-field
          v-model="email"
          :rules="[validateRequired(email, 'Email'), validateEmail(email)]"
          label="Email"
        ></v-text-field>
        <v-text-field
          v-model="password"
          :rules="[
            validateRequired(password, 'Password'),
            validatePassword(password),
          ]"
          label="Password"
          type="password"
        ></v-text-field>
        <div class="switch-variant" v-if="variant === 'login'">
          You don't have an account yet?
          <NuxtLink to="/auth?variant=register">Create an account</NuxtLink>
        </div>
        <div class="switch-variant" v-else>
          Do you already have an account?
          <NuxtLink to="/auth?variant=login">Login</NuxtLink>
        </div>
        <v-btn class="mt-2" type="submit" block>{{
          variant === "register" ? "Zarejestruj się" : "Zaloguj się"
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
const variant = ref<Variant>((route.query.variant as Variant) || "login");

const firstName = ref("");
const lastName = ref("");
const email = ref("");
const password = ref("");

watch(
  () => route.query.variant,
  (value) => {
    variant.value = value as Variant;
  }
);
</script>
<style lang="scss">
.main {
  display: flex;
  flex-direction: column;
  height: 100vh;
  width: 100vw;
  justify-content: center;
  align-items: center;
  gap: 1rem;
  .h1 {
    text-align: center;
  }
  .container {
    width: 20%;

    .switch-variant {
      display: flex;
      justify-content: center;
      gap: 0.5rem;
      margin-bottom: 10px;
    }
  }
}
</style>
