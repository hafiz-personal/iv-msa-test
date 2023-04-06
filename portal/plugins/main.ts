import createService from '~/services/index'
import { Context } from '@nuxt/types';


export default function (ctx: Context, inject: any) {
    ctx.$axios.setBaseURL(ctx.env.apiBaseUrl)   
    inject('services', createService(ctx))
}