<template>
  <div class="container-fluid">
    <div class="vault">
      <h1>{{ activeVault.name }}</h1>
      <h5>{{ activeVault.description }}</h5>
      <br />
      <h5>Your Vault's Keeps:</h5>
      <div class="row">
        <div
          class="col-lg-4 col-sm-6"
          v-for="keep in vaultKeeps"
          :key="keep._id"
        >
          <div class="card mb-3">
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
  </div>
</template>

<script>
export default {
  name: "vault",
  mounted() {
    this.$store.dispatch("getVaultById", this.vaultId);
    this.$store.dispatch("getKeepsByVaultId", this.vaultId);
  },
  computed: {
    activeVault() {
      return this.$store.state.activeVault[0];
    },
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
    }
  },
  methods: {
    createVaultKeep(event, KeepId) {
      let VaultId = parseInt(event.target.value);
      let vaultKeep = { VaultId, KeepId };
      this.$store.dispatch("createVaultKeep", vaultKeep);
    }
  },
  props: ["vaultId"]
};
</script>

<style scoped></style>
