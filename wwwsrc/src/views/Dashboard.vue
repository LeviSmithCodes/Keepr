<template>
  <div class="dashboard container-fluid">
    <h1>WELCOME TO THE DASHBOARD</h1>
    <h5>Create a Keep:</h5>
    <div class="card">
      <div class="card-body">
        <form @submit.prevent="createKeep">
          <input type="text" placeholder="Keep Name" v-model="newKeep.name" required />
          <input type="text" placeholder="Keep Description" v-model="newKeep.description" required />
          <input type="text" placeholder="Keep Image URL" v-model="newKeep.img" required />
          Visibility:
          <div class="form-check form-check-inline">
            <input
              v-model="newKeep.isPrivate"
              class="form-check-input"
              type="radio"
              name="inlineRadioOptions"
              id="inlineRadio1"
              value="false"
              checked
            />
            <label class="form-check-label" for="inlineRadio1">Public</label>
          </div>
          <div class="form-check form-check-inline">
            <input
              v-model="newKeep.isPrivate"
              class="form-check-input"
              type="radio"
              name="inlineRadioOptions"
              id="inlineRadio2"
              value="true"
            />
            <label class="form-check-label" for="inlineRadio2">Private</label>
          </div>
          <button class="btn btn-success" type="submit">Add Keep</button>
        </form>
      </div>
    </div>
    <!-- public {{ publicKeeps }} user {{ userKeeps }} -->
    <br />
    <h5>Your Created Keeps:</h5>
    <div class="row">
      <div class="col-lg-4 col-sm-6" v-for="keep in userKeeps" :key="keep._id">
        <div class="card">
          <img class="card-img" :src="keep.img" />
          <div class="card-body">
            <h5 class="card-title">{{keep.name}}</h5>

            <p class="card-text">{{keep.description}}</p>
            <br />
            Views: {{keep.views}}
            <br />
            Shares: {{keep.shares}}
            <br />
            Keeps: {{keep.keeps}}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      newKeep: {
        name: "",
        description: "",
        img: "",
        isPrivate: false,
        views: 0,
        shares: 0,
        keeps: 0
      }
    };
  },
  mounted() {
    this.$store.dispatch("getUserKeeps");
  },
  computed: {
    userKeeps() {
      return this.$store.state.userKeeps;
    }
  },
  methods: {
    createKeep() {
      let keep = { ...this.newKeep };
      this.$store.dispatch("createKeep", keep);
      this.newKeep = {
        name: "",
        description: "",
        img: "",
        isPrivate: false,
        views: 0,
        shares: 0,
        keeps: 0
      };
    }
  }
};
</script>

<style></style>
