<template>
  <div class="card mb-3">
    <img class="card-img" :src="keep.img" />
    <div class="card-body">
      <h5 class="card-title">{{ keep.name }}</h5>
      <button
        id="deleteKeep"
        class="btn btn-danger float-right"
        @click="deleteKeep(keep.id)"
        v-if="['dashboard'].includes($route.name)"
      >
        Delete
      </button>
      <button
        id="removeFromVault"
        class="btn btn-danger float-right"
        @click="removeKeepFromVault(keep.id, activeVault.id)"
        v-if="['vault'].includes($route.name)"
      >
        Remove from Vault
      </button>

      <p class="card-text">{{ keep.description }}</p>
      <br />
      <select
        class="form-control"
        @change="createVaultKeep($event, keep.id)"
        v-if="$auth.isAuthenticated"
      >
        <option value selected disabled>Add to Vault</option>
        <option v-for="vault in userVaults" :value="vault.id" :key="vault.id">{{
          vault.name
        }}</option>
      </select>

      <!-- <div class="dropdown">
              <button
                class="btn btn-success dropdown-toggle"
                type="button"
                id="dropdownMenuButton"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                Add to Vault
              </button>
              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                <a
                  class="dropdown-item"
                  href="#"
                  v-for="vault in userVaults"
                  :value="vault.id"
                  :key="vault.id"
                  @onclick="createVaultKeep(vault.id, keep.id)"
                  >{{ vault.name }}</a
                >
              </div>
            </div> -->
      <br />
      Views: {{ keep.views }}
      <br />
      Shares: {{ keep.shares }}
      <br />
      Keeps: {{ keep.keeps }}
    </div>
  </div>
</template>

<script>
export default {
  name: "Keep",
  mounted() {
    this.$store.dispatch("getUserVaults");
  },
  computed: {
    userVaults() {
      return this.$store.state.userVaults;
    }
  },
  methods: {
    deleteKeep(keepId) {
      // console.log("keepId from DeleteKeep is", keepId);
      this.$store.dispatch("deleteKeep", keepId);
    },
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
  props: ["keep", "activeVault"]
};
</script>

<style></style>
