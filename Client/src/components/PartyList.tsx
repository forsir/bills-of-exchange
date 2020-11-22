import * as React from "react";
import { Link } from "react-router-dom";
import { Party } from "./Types";

type ParamType = {
    parties: Party[];
}

export const PartyList = (params: ParamType) => {
    const { parties } = params;

    return <div>
        <table>
            <thead>
                <tr>
                    <th>id</th>
                    <th>name</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {parties.map((b, i) => <tr key={i}>
                    <td>{b.id}</td>
                    <td>{b.name}</td>
                    <td><Link to={{ pathname: `/party/${b.id}` }}>link</Link></td>
                </tr>
                )}
            </tbody>
        </table>
    </div>
};