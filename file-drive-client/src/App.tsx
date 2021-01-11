import React from 'react';
import './App.css';
import Login from './login/components/Login'
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect
} from 'react-router-dom'
import FilesTree from './files-tree/FilesTree';
import { UserService } from './login/logic/user-service';
import SignUp from './login/components/SignUp';

function App() {
  return (
    <div className="App">
      <Router>
        <Switch>
          <Route path="/login">
            <Login />
          </Route>
          <Route path="/tree">
            <FilesTree />
          </Route>
          <Route path="/signup">
            <SignUp />
          </Route>
        </Switch>
        {
          UserService.getCurrentUser() === null ?
            <Redirect to={'/login'} />
            : undefined
        }
      </Router>

    </div>
  );
}

export default App;
