
class Tester extends React.Component {

    render() {
        console.log('hello react');

        return (
            <div>
                <p>hello world</p>
                    
            </div>
        );
    }
}

const container = document.getElementById('secret-game');
ReactDOM.render(<Tester />, container);

