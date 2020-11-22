import * as React from "react";

type PropsType = {
    errorText: string;
};

export const DisplayError = (props: PropsType) => {
    let { errorText } = props;
    return <div>Error: {errorText}</div>
};