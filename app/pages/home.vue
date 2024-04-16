<template>
  <v-layout>
    <Header />
    <v-main>
      <v-container class="container">
        <v-row>
          <v-col cols="12">
            <h2>Hey, Wiktor</h2>
            <p>Your courses:</p>
            <div class="courses">
              <v-card class="course" v-for="course in courses" :key="course.id">
                <v-card-title>{{ course.title }}</v-card-title>
                <v-card-text>Teacher: {{ course.teacher }}</v-card-text>
                <v-card-actions>
                  <v-btn :to="'/course/' + course.id">Details</v-btn>
                </v-card-actions>
              </v-card>
            </div>
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-layout>
</template>
<script lang="ts" setup>
import Header from "~/components/Header/Header.vue";

const courses = ref([
  {
    id: 1,
    title: "Course 1",
    teacher: "Teacher 1",
    description: "Description of course 1",
  },
  {
    id: 2,
    title: "Course 2",
    teacher: "Teacher 2",
    description: "Description of course 2",
  },
  {
    id: 3,
    title: "Course 3",
    teacher: "Teacher 3",
    description: "Description of course 3",
  },
  {
    id: 4,
    title: "Course 4",
    teacher: "Teacher 4",
    description: "Description of course 4",
  },
  {
    id: 5,
    title: "Course 5",
    teacher: "Teacher 5",
    description: "Description of course 5",
  },
]);
</script>
<style lang="scss">
.container {
  width: 50%;
  h2 {
    font-size: 2.5rem;
    margin-bottom: 1rem;
  }
  p {
    font-size: 1.5rem;
    margin-bottom: 0.7rem;
  }
  .courses {
    display: flex;
    flex-wrap: wrap;
    gap: 1rem;

    .course {
      width: 100%;
    }
  }
}
</style>
