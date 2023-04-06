import { NuxtAxiosInstance } from "@nuxtjs/axios";
import { Service } from "./service";

declare module "vue/types/vue" {
    interface Vue {
      $axios: NuxtAxiosInstance;
      $services: Service;
    }
  }

  declare module "@nuxt/types" {
    interface NuxtAppOptions {
      $axios: NuxtAxiosInstance;
      
    }
  
    // interface Context {
    //   $axios: NuxtAxiosInstance;
    //   $services: Service;
    // }

    
  }