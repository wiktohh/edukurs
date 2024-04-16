<template>
  <v-layout>
    <v-main>
      <v-container class="container">
        <div class="theme">
          <ChangeThemeButton />
        </div>
        <v-row>
          <v-col class="content">
            <div>
              <h2>Welcome to Edukurs</h2>
              <p>
                Czy czujesz, że organizacja i przekazywanie materiałów w ramach
                swoich zajęć jest czasochłonna i męcząca? Poznaj Edukurs -
                innowacyjną platformę, która ułatwia przekazywanie plików,
                projektów i zadań w trakcie kursów!
              </p>
            </div>
            <div>
              <h2>Dlaczego Edukurs?</h2>
              <p>
                Edukurs został stworzony z myślą o nauczycielach i studentach,
                którzy chcą efektywniej zarządzać swoimi zadaniami i materiałami
                kursowymi.
              </p>
            </div>
            <div>
              <h2>How it works?</h2>
              <p>
                Edukurs is a platform that allows teachers to create courses and
                share materials with students. Students can easily access the
                materials and submit their projects.
              </p>
            </div>
            <div class="auth-section">
              <v-btn to="/auth?variant=register"> Zarejestruj się </v-btn>
              <div>lub</div>
              <v-btn to="/auth?variant=login"> Zaloguj się </v-btn>
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
