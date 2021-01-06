import React from 'react';
import { Route, BrowserRouter as Router } from 'react-router-dom';
import Login from './pages/Login';
import Register from './pages/Register';

function App()  {
  
    return (
        <div className="App">
            <div className="col-md-8 mx-auto">
                <Router>
                    <Route exact path="/" component={Login} />
                    <Route path="/register" component={Register} />
                   
                </Router>
            </div>
        </div>
    );
}

export default App;