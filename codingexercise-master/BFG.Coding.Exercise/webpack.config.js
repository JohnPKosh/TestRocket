const path = require("path");
const webpack = require('webpack');

const globalConfig = env => ({
	resolve: { extensions: [",", ".ts", ".tsx", ".js"] },
	module: {
		rules: [
			{ test: /\.tsx?$/, exclude: /node_modules/, loader: 'ts-loader' },
			{ test: /\.js$/, loader: "source-map-loader" },
			{ test: /\.css$/, use: [{ loader: 'style-loader' }, { loader: 'css-loader' }] }
		]
	},
	plugins: [new webpack.optimize.ModuleConcatenationPlugin()],
	devtool: 'source-map'
});

module.exports = env => [{
	...globalConfig(env),
	entry: {
		client: './react/index.tsx'
	},
	output: {
		path: path.join(__dirname, 'content', 'build'),
		publicPath: "/content/build/",
		chunkFilename: "[name].bundle.js",
		filename: "[name].bundle.js"
	}
}];