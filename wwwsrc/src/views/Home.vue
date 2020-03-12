<template>
  <div class="home container-fluid">
    <h1>ALL PUBLIC KEEPS</h1>
    <div class="row">
      <div class="col-lg-4 col-sm-6" v-for="keep in keeps" :key="keep._id">
        <keep-component :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import KeepComponent from "../components/Keep.vue";
export default {
  name: "home",
  mounted() {
    this.$store.dispatch("getPublicKeeps");
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    keeps() {
      return this.$store.state.publicKeeps;
    }
    // userVaults() {
    //   return this.$store.state.userVaults;
    // }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    createVaultKeep(event, KeepId) {
      let VaultId = parseInt(event.target.value);
      let vaultKeep = { VaultId, KeepId };
      this.$store.dispatch("createVaultKeep", vaultKeep);
    }
  },
  components: {
    KeepComponent
  }
};
</script>

<style scoped>
.card {
  width: 300px !important;
  margin-bottom: 2rem;
}
/* .card-img {
  max-height: 200px;
} */
</style>
