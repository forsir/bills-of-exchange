import * as React from "react";
import { HashRouter, Switch, Route, BrowserRouter as Router } from "react-router-dom";
import { Party } from "./pages/party";
import { Bill } from "./pages/bill";
import { List } from "./pages/list";

export const App = () => {

    return <Router>
        <div>
            <HashRouter>
                <Switch>
                    <Route exact path="/" component={List} />
                    <Route path="/party/:partyId" component={Party} />
                    <Route path="/bill/:billId" component={Bill} />
                </Switch>
            </HashRouter>
        </div>
    </Router>
};
