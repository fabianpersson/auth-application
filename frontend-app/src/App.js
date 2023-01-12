import logo from "./logo.svg";
import React, { useState } from "react";
import "@aws-amplify/ui-react/styles.css";
import LoginFormExample from "./components/Login";
import DefaultCollectionExample from "./components/Sessions";
import Signout from "./components/Signout";
import Signup from "./components/Signup";
import { Route, Routes, BrowserRouter, Navigate } from "react-router-dom";

function App() {
  const [token, setToken] = useState();
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginFormExample setToken={setToken} />} />
        <Route
          path="/sessions"
          element={
            <RequireAuth token={token}>
              <div>
                <DefaultCollectionExample token={token} />
                <Signout setToken={setToken} />
              </div>
            </RequireAuth>
          }
        />
        <Route path="/signup" element={<Signup />} />
      </Routes>
    </BrowserRouter>
  );
}

function RequireAuth({ children, token }) {
  if (token == undefined) {
    return <Navigate to="/" replace />;
  }

  return children;
}

export default App;
