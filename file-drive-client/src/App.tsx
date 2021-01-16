import React from 'react';
import './App.css';
import Login from './login/components/Login'
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect
} from 'react-router-dom'
import FilesTree from './files-tree/components/FilesTree';
import { UserService } from './login/logic/user-service';
import SignUp from './login/components/SignUp';
import { Snackbar } from '@material-ui/core';
import { Alert } from '@material-ui/lab';
import useErrorContext from './errors/ErrorContext';

function App() {
  const {error, setError} = useErrorContext()
  
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
      <Snackbar open={error !== null} autoHideDuration={1500} onClose={() => setError(null)}>
            <Alert severity="error">{error}</Alert>
        </Snackbar>
    </div>
  );
}

export default App;
