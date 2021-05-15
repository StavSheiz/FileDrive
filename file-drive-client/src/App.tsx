import { Snackbar } from '@material-ui/core';
import { Alert } from '@material-ui/lab';
import React from 'react';
import {
  BrowserRouter as Router,
  Redirect, Route, Switch
} from 'react-router-dom';
import './App.css';
import useErrorContext from './errors/ErrorContext';
import FilesTree from './files-tree/components/FilesTree';
import Login from './login/components/Login';
import SignUp from './login/components/SignUp';
import { UserService } from './login/logic/user-service';
function App() {
  const { error, setError } = useErrorContext()

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
          <Route path="/">
            <Redirect to={'/tree'} />
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
