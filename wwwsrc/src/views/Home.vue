<template>
  <div class="home container-fluid">
    <h1>All Public Keeps</h1>
    <div class="row">
      <div class="col-lg-4 col-sm-6" v-for="keep in keeps" :key="keep._id">
        <div class="card">
          <img class="card-img" :src="keep.img" />
          <div class="card-body">
            <h5 class="card-title">{{ keep.name }}</h5>
            <p class="card-text">{{ keep.description }}</p>
            <br />
            <select
              class="form-control"
              @change="createVaultKeep($event, keep.id)"
            >
              <option value selected disabled>Add to Vault</option>
              <option
                v-for="vault in userVaults"
                :value="vault.id"
                :key="vault.id"
                >{{ vault.name }}</option
              >
            </select>
            <br />
            Views: {{ keep.views }}
            <br />
            Shares: {{ keep.shares }}
            <br />
            Keeps: {{ keep.keeps }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
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
    },
    userVaults() {
      return this.$store.state.userVaults;
    }
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
  }
};
</script>

<style scoped>
.card {
  width: 300px !important;
  margin-bottom: 2rem;
}
.card-img {
  max-height: 200px;
}
</style>
