import React from 'react';
import logo from './logo.svg';
import './App.css';
import { LoginAPI } from './login/api/login-api';

function App() {
  LoginAPI.getUser("ShStav", "password").then(data => { console.log(data.data?.name) })
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
