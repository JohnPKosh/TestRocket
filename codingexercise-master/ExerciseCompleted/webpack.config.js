module.exports = {
    devtool: "source-map",   // Needed for debug to work
    mode: "development",     // Needed because compilation complains if it's missing
    target: ["web", "es5"],  // Stops Webpack bundling ES5 output from loader with ES6 arrows, duh
    entry: "./app.tsx",
    output: {
        filename: "bundle.js",
        devtoolModuleFilenameTemplate: "[resource-path]", // Removes the webpack:/// prefix, fixes debugging in VS2017
        path: __dirname  // Puts bundle in dist by default without this
    },
    resolve: {
        extensions: [".ts", ".tsx", ".js"]
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,  // .ts and .tsx files are bundled as TypeScript
                exclude: /node_modules/,  // ..but not in node_modules
                loader: "ts-loader"
                // We also have <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked> in csproj to prevent additional
                // js output from MS Build
           }
        ]
    }
};