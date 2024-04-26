<template>
  <v-layout>
    <v-main>
      <v-container class="container">
        <div class="theme">
          <LanguagesButtons />
          <ChangeThemeButton />
        </div>
        <v-row>
          <v-col class="content">
            <div>
              <h2>{{ $t("landingPage.welcome") }}</h2>
              <p>
                {{ $t("landingPage.welcomeDescription") }}
              </p>
            </div>
            <div>
              <h2>{{ $t("landingPage.why") }}</h2>
              <p>
                {{ $t("landingPage.whyDescription") }}
              </p>
            </div>
            <div>
              <h2>{{ $t("landingPage.howWorks") }}</h2>
              <p>
                {{ $t("landingPage.howWorksDescription") }}
              </p>
            </div>
            <div class="auth-section">
              <v-btn to="/auth?variant=register">
                {{ $t("auth.register") }}</v-btn
              >
              <div>{{ $t("landingPage.or") }}</div>
              <v-btn to="/auth?variant=login"> {{ $t("auth.login") }} </v-btn>
            </div>
          </v-col>
          <v-col class="photo-section">
            <img
              class="photo"
              :src="LandingPagePhoto"
              alt="Landing page photo"
            />
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-layout>
</template>

<script lang="ts" setup>
import LandingPagePhoto from "~/assets/landing-page-photo.svg";
</script>
<style lang="scss" scoped>
.container {
  height: 100vh;
  width: 100vw;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 2rem;

  .theme {
    display: flex;
    gap: 0.5rem;
    position: absolute;
    top: 0;
    right: 0;
    margin: 1rem;
  }

  .content {
    display: flex;
    flex-direction: column;
    justify-content: center;
    gap: 2rem;
    h2 {
      font-size: 2rem;
      margin-bottom: 1rem;
    }
    p {
      font-size: 1.2rem;
    }
  }
  .auth-section {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 1rem;
  }
  .photo-section {
    display: flex;
    justify-content: center;
  }
  .photo {
    width: 100%;
    max-width: 600px;
  }
}
</style>
