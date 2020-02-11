<template>
  <div class="dashboard container-fluid">
    <h1>WELCOME TO YOUR DASHBOARD</h1>
    <h5>Create a Vault:</h5>
    <div class="card">
      <div class="card-body">
        <form @submit.prevent="createVault">
          <input
            type="text"
            placeholder="Vault Name"
            v-model="newVault.name"
            required
          />
          <input
            type="text"
            placeholder="Vault Description"
            v-model="newVault.description"
            required
          />
          <button class="btn btn-success" type="submit">Add Vault</button>
        </form>
      </div>
    </div>
    <br />
    <h5>Your Created Vaults:</h5>
    <div class="row">
      <div
        class="col-lg-4 col-sm-6"
        v-for="vault in userVaults"
        :key="vault._id"
      >
        <div class="card mb-3">
          <div class="card-body">
            <h5 class="card-title">
              <router-link
                :to="{ name: 'vault', params: { vaultId: vault.id } }"
                >{{ vault.name }}</router-link
              >
            </h5>
            <button
              id="deleteVault"
              class="btn btn-danger float-right"
              @click="deleteVault(vault.id)"
            >
              Delete
            </button>

            <p class="card-text">{{ vault.description }}</p>
          </div>
        </div>
      </div>
    </div>

    <br />
    <h5>Create a Keep:</h5>
    <div class="card">
      <div class="card-body">
        <form @submit.prevent="createKeep">
          <input
            type="text"
            placeholder="Keep Name"
            v-model="newKeep.name"
            required
          />
          <input
            type="text"
            placeholder="Keep Description"
            v-model="newKeep.description"
            required
          />
          <input
            type="text"
            placeholder="Keep Image URL"
            v-model="newKeep.img"
            required
          />
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
        <div class="card mb-3">
          <img class="card-img" :src="keep.img" />
          <div class="card-body">
            <h5 class="card-title">{{ keep.name }}</h5>
            <button
              id="deleteKeep"
              class="btn btn-danger float-right"
              @click="deleteKeep(keep.id)"
            >
              Delete
            </button>

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
      },
      newVault: {
        name: "",
        description: ""
      }
    };
  },
  mounted() {
    this.$store.dispatch("getUserKeeps");
    this.$store.dispatch("getUserVaults");
  },
  computed: {
    userKeeps() {
      return this.$store.state.userKeeps;
    },
    userVaults() {
      return this.$store.state.userVaults;
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
    },
    deleteKeep(keepId) {
      // console.log("keepId from DeleteKeep is", keepId);
      this.$store.dispatch("deleteKeep", keepId);
    },
    createVault() {
      let vault = { ...this.newVault };
      this.$store.dispatch("createVault", vault);
      this.newVault = {
        name: "",
        description: ""
      };
    },
    deleteVault(vaultId) {
      this.$store.dispatch("deleteVault", vaultId);
    },
    createVaultKeep(event, KeepId) {
      let VaultId = parseInt(event.target.value);
      let vaultKeep = { VaultId, KeepId };
      this.$store.dispatch("createVaultKeep", vaultKeep);
    }
    //     createVaultKeep(VaultId, KeepId) {
    //   console.log("createVaultKeep says hey");
    //   let vaultKeep = { VaultId, KeepId };
    //   this.$store.dispatch("createVaultKeep", vaultKeep);
    // }
  }
};
</script>

<style></style>
