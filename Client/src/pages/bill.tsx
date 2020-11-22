import * as React from "react";
import { Link, useParams } from "react-router-dom";
import * as axios from 'axios';

interface ParamTypes {
    billId: string
}

export const Bill = () => {
    let { billId } = useParams<ParamTypes>();
    const [user, setUser] = React.useState();

    console.log(billId, useParams<ParamTypes>());

    axios.default.get(`/api/users/${billId}`)
        .then(({ data: user }) => {
            console.log('user', user);

            setUser(user);
        });

    return <div>
        <h2>Hello from Bill {billId}</h2>
        <br />
        <Link to="/">Back</Link>
    </div>
}
