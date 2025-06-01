/// <reference path="./.sst/platform/config.d.ts" />

export default $config({
  app(input) {
    return {
      name: "csharp-example-api",
      removal: input?.stage === "production" ? "retain" : "remove",
      protect: ["production"].includes(input?.stage),
      home: "aws",
    };
  },
  async run() {
    const infra = await import("./infra");

    return {
      ApiUrl: infra.api?.url ?? 'URL unavailable'
    }
  },
});
