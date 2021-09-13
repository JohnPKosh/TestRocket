import * as React from "react";
import * as ReactDOM from "react-dom";

interface IGreeterProps {
    message: string;
}

interface IGreeterState {
    time: Date;
}

class Greeter extends React.Component<IGreeterProps, IGreeterState> {
    private interval: NodeJS.Timeout;

    constructor(props: IGreeterProps) {
        super(props);
        this.state = { time: new Date() };
    }

    public componentDidMount() {
        this.interval = setInterval(() => this.setState({ time: new Date() }), 500);
    }

    public componentWillUnmount() {
        clearInterval(this.interval);
    }

    public render() {
        return (
            <div>
                {this.props.message} <span>{this.state.time.toUTCString()}</span>
            </div>
        );
    }
}

ReactDOM.render(<Greeter message="The time is: " />, document.getElementById("root"));
