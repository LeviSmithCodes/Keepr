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
          <keep-component :keep="keep" :activeVault="activeVault" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import KeepComponent from "../components/Keep.vue";
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
    },
    removeKeepFromVault(keepId, vaultId) {
      let vaultKeep = { vaultId, keepId };
      this.$store.dispatch("deleteVaultKeep", vaultKeep);
    }
  },
  props: ["vaultId"],
  components: {
    KeepComponent
  }
};
</script>

<style scoped></style>
