import * as React from "react";
import { HashRouter, Switch, Route, BrowserRouter as Router } from "react-router-dom";
import { PartyPage } from "./pages/partyPage";
import { BillPage } from "./pages/billPage";
import { ListPage } from "./pages/listPage";

export const App = () => {

    return <Router>
        <div>
            <HashRouter>
                <Switch>
                    <Route exact path="/" component={ListPage} />
                    <Route path="/party/:partyId" component={PartyPage} />
                    <Route path="/bill/:billId" component={BillPage} />
                </Switch>
            </HashRouter>
        </div>
    </Router>
};
