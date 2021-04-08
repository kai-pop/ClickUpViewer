module.exports = {
  css: {
    loaderOptions: {
      scss: {
        prependData: '@import "./src/styles/common/common.scss";',
      },
    },
  },
  configureWebpack: {
    // Using source-map allows VS Code to correctly debug inside vue files
    devtool: "source-map",
    // Breakpoints in VS and VSCode won’t work since the source maps
    // consider ClientApp the project root, rather than its parent folder
    output: {
      devtoolModuleFilenameTemplate: (info) => {
        const resourcePath = info.resourcePath;
        return `webpack:///${resourcePath}?${info.loaders}`;
      },
    },
  },
  pages: {
    index: {
      entry: "src/main.ts",
      title: "ClickUpViewer",
    },
  },
  transpileDependencies: ["vuetify"],
};
