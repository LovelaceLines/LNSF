import { z } from "zod";

const envSchema = z.object({
  NODE_ENV: z.string().default(import.meta.env.VITE_APP_NODE_ENV!),
  IS_PRODUCTION: z.boolean().default(import.meta.env.VITE_APP_NODE_ENV === "production"),
  IS_DEVELOPMENT: z.boolean().default(import.meta.env.VITE_APP_NODE_ENV === "development"),
  IS_STAGING: z.boolean().default(import.meta.env.VITE_APP_NODE_ENV === "staging"),
  APP_URL: z.string().url().default(import.meta.env.VITE_APP_API_URL!),
  API_URL: z.string().url().default(import.meta.env.VITE_APP_API_URL!),
});

export default envSchema.parse(import.meta.env);