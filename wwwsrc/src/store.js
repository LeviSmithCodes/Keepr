import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "./router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    publicKeeps: [],
    userKeeps: []
  },
  mutations: {
    setPublicKeeps(state, publicKeeps) {
      state.publicKeeps = [];
      state.publicKeeps.push(...publicKeeps);
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = [];
      state.userKeeps.push(...userKeeps);
    }
  },
  actions: {
    setBearer({}, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    getPublicKeeps({ commit, dispatch }) {
      api.get("Keeps").then(res => {
        commit("setPublicKeeps", res.data);
      });
    },
    getUserKeeps({ commit, dispatch }) {
      api.get("Keeps/User").then(res => {
        commit("setUserKeeps", res.data);
      });
    },
    async createKeep({ commit, dispatch }, keep) {
      try {
        let res = await api.post("Keeps", keep);
      } catch (error) {
        console.warn(error.message);
      }
    }
  }
});
