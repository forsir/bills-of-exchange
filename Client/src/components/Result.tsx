import * as React from "react";
import { DisplayError } from "./DisplayError";
import { Loading } from "./Loading";

type Props = {
    isLoading: boolean;
    errorText: string;
    children: React.ReactNode;
};

export const Result = (props: Props) => {
    let { isLoading, errorText, children } = props;
    if (errorText) {
        return <DisplayError errorText={errorText} />;
    }
    if (isLoading) {
        return <Loading />
    }
    return <div>{children}</div>;
};