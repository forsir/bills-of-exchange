import * as React from "react";
import { Link, useParams } from "react-router-dom";
import * as axios from 'axios';

interface ParamTypes {
    id: string
}

export const Party = (params) => {
    let { id } = useParams<ParamTypes>();
    const [user, setUser] = React.useState();

    axios.default.get(`/api/users/${params.userId}`)
        .then(({ data: user }) => {
            console.log('user', user);

            setUser(user);
        });

    return <div>
        <h2>Hello from Party</h2>
        <br />
        <Link to="/pageB">Navigate to Page B</Link>
        <Link to="/">Back</Link>
    </div>
}
