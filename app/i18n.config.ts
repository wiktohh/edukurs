import en from "./locales/en.json";
import pl from "./locales/pl.json";

export default defineI18nConfig(() => ({
  legacy: false,
  locale: "en",
  plugins: [
    {
      src: "~/plugins/i18n.ts",
      mode: "client",
    },
  ],
  messages: {
    en,
    pl,
  },
}));
