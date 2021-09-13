import React from "react";
import { render } from "react-dom";
import { Route, Switch } from "react-router";
import { HashRouter as Router } from "react-router-dom";

const List = React.lazy(() => import("./List"));
const New = React.lazy(() => import("./New"));

const App = () => {

	return <>
		<div className="page">
			<div className="inner">
				<Router basename="/">
					<Switch>
						<Route path="/new" exact>
							<React.Suspense fallback={<></>}>
								<New />
							</React.Suspense>
						</Route>
						<Route path="/" exact>
							<React.Suspense fallback={<></>}>
								<List />
							</React.Suspense>
						</Route>
					</Switch>
				</Router>
			</div>
		</div>
	</>;

};

render(<App />, document.getElementById("root"));