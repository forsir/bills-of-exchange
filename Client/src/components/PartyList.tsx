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
                <th>
                    <td>id</td>
                    <td>name</td>
                    <td></td>
                </th>
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